using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace MTOM_Library
{
	/// <summary>
	/// A class to download a file from a web server using WSE 3.0 web services, with the MTOM standard.
	/// To use this class, drag/drop an instance onto a windows form, and create event handlers for ProgressChanged
	/// and RunWorkerCompleted.  
	/// Make sure to specify the RemoteFileName and LocalSaveFolder before you call RunWorkerAsync() to begin the download
	/// </summary>
	public class FileTransferDownload : FileTransferBase
	{
		public string RemoteFileName;
        //public string LocalFileName;
        public string LocalSaveFolder;

		/// <summary>
		/// Start the upload operation synchronously.
		/// The argument must be the start position, usually 0
		/// </summary>
		public void RunWorkerSync(DoWorkEventArgs e)
		{
			OnDoWork(e);
			base.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(e.Result, null, false));
		}

		/// <summary>
		/// This method starts the download process. It supports cancellation, reporting progress, and exception handling.
		/// The argument is the start position, usually 0
		/// </summary>
		protected override void OnDoWork(DoWorkEventArgs e)
		{
			base.OnDoWork(e);

			this.Offset = Int64.Parse(e.Argument.ToString());
			int numIterations = 0;	// this is used with a modulus of the sampleInterval to check if the chunk size should be adjusted.  it is started at 1 so that the first check will be skipped because it may involve an initial delay in connecting to the web service
			
			this.LocalFilePath = this.LocalSaveFolder.TrimEnd('\\') + "\\" + LocalFileName;
			if(Offset == 0 && File.Exists(this.LocalFilePath))   // create a new empty file
				File.Create(this.LocalFilePath).Close();

			long FileSize = this.WebService.GetFileSize(TerminalID, this.RemoteFileName);   // the file is on the server and we need to know how big it is before we start downloading, in order to give accurate feedback to the user.
			string FileSizeDescription = CalcFileSize(FileSize);   // e.g. "2.4 Gb" instead of 24000000000000000 bytes etc.
			
			// open a file stream for the file we will write to in the start-up folder
			using(FileStream fs = new FileStream(LocalFilePath, FileMode.OpenOrCreate, FileAccess.Write))
			{
				fs.Seek(Offset, SeekOrigin.Begin);

				// download the chunks from the web service one by one, until all the bytes have been read, meaning the entire file has been downloaded.
				while(Offset < FileSize && !this.CancellationPending)
				{
					if(this.AutoSetChunkSize)
					{
						int currentIntervalMod = numIterations % this.ChunkSizeSampleInterval;
						if(currentIntervalMod == 0)
							this.StartTime = DateTime.Now;	// used to calculate the time taken to transfer the first 5 chunks
						else if(currentIntervalMod == 1)
							this.CalcAndSetChunkSize();
					}
					try
					{
						// although the DownloadChunk returns a byte[], it is actually sent using MTOM because of the configuration settings. 
						byte[] Buffer = this.WebService.DownloadChunk(TerminalID, this.RemoteFileName, this.Offset, ChunkSize);
						fs.Write(Buffer, 0, Buffer.Length);
						this.Offset += Buffer.Length;	// save the offset position for resume
					}
					catch(Exception ex)
					{
						// swallow the exception and try again
						Debug.WriteLine("Exception: " + ex.ToString());

						if(NumRetries++ >= MaxRetries)	// too many retries, bail out
						{
							fs.Close();
							throw new Exception("Error occurred during upload, too many retries.\r\n" + ex.Message);
						}
					}
					// update the user interface by reporting progress.
					string SummaryText = String.Format("Transferred {0} / {1}", CalcFileSize(Offset), FileSizeDescription);
					this.ReportProgress((int)(((decimal)Offset / (decimal)FileSize) * 100), SummaryText);
					numIterations++;
				}
			}


			// the user may select to have the local file hash-verified against the server file
			if(this.IncludeHashVerification)
			{
				this.ReportProgress(99, "Checking file hash...");

				// do the file hashing asynchronously if the client and server machines are different, otherwise it is torturing the harddrive doing a byte-level sequential read on two files at the same time :(  much faster just to let one file finish and then hash the other.
				if(this.ws.Url.ToLower().Contains("localhost"))
				{
					// synchronous implementation
					this.CheckLocalFileHash();
					
					// request the server hash
					this.RemoteFileHash = this.WebService.CheckFileHash(this.TerminalID, this.RemoteFileName);
				}
				else
				{
					// asynchronous implementation

					// start calculating the local hash
					this.HashThread = new Thread(new ThreadStart(this.CheckLocalFileHash));
					this.HashThread.Start();

					// request the server hash
					this.WebService.Timeout = 1000 * 60 * 10;	// 10 mins, big files takes ages to hash
					this.RemoteFileHash = this.WebService.CheckFileHash(this.TerminalID, this.RemoteFileName);
					this.WebService.Timeout = 1000 * 30;	// reset back to 30 seconds

					// wait for the local hash to complete
					this.HashThread.Join();	
				}
				
				if(this.LocalFileHash == RemoteFileHash)
					e.Result = String.Format("Hashes match exactly\r\nLocal hash:  {0}\r\nServer hash: {1}", LocalFileHash, RemoteFileHash);
				else    // could throw an exception here if you want.  different hashes = corrupt download!
					e.Result = String.Format("Hashes are different!\r\nLocal hash:  {0}\r\nServer hash: {1}", LocalFileHash, RemoteFileHash);
			}

            // TODO : odmaz stazene db ze serveru ... 
            try
            {
                if (!this.WebService.Delete(this.TerminalID, this.RemoteFileName))
                    e.Result = "Nepodaøilo se smazat soubor pøedlohy " + this.RemoteFileName + " ze serveru";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
	}
}
