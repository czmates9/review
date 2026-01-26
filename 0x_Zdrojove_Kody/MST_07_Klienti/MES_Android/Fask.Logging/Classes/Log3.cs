using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fask.Logging
{
	internal class Log3 : ILog2
	{

		private string _pathFile = null; //Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Log");

		/// <summary>
		/// Nazvy souboru
		/// </summary>
		private string FileName_Trace = "Log_Trace.txt";
		private string FileName_Debug = "Log_Debug.txt";
		private string FileName_Info = "Log_Info.txt";
		private string FileName_Warn = "Log_Warn.txt";
		private string FileName_Error = "Log_Error.txt";
		private string FileName_Fatal = "Log_Fatal.txt";

		private string FileName_PrintInfo = "Log_PrintInfo.txt";
		private string FileName_License = "Log_License.txt";
		private string FileName_Location = "LocationLog.txt";
		
		


		#region Log Text anebo Exception
		/// <summary>
		/// Slouží pro zapis Textu podle levelu
		/// </summary>
		/// <param name="level"></param>
		/// <param name="Message"></param>
		/// <returns></returns>
		public bool write(LogLevel level, string Message)
		{

			switch (level)
			{
				case LogLevel.Trace:
					return SaveToFile(Message, FileName_Trace);
				case LogLevel.Debug:
					return SaveToFile(Message, FileName_Debug);
				case LogLevel.Info:
					return SaveToFile(Message, FileName_Info);
				case LogLevel.Warn:
					return SaveToFile(Message, FileName_Warn);
				case LogLevel.Error:
					return SaveToFile(Message, FileName_Error);
				case LogLevel.Fatal:
					return SaveToFile(Message, FileName_Fatal);
				case LogLevel.PrintInfo:
					return SaveToFile(Message, FileName_PrintInfo);
				case LogLevel.Licence:
					return SaveToFile(Message, FileName_License);
				case LogLevel.Location:
					return SaveToFile(Message, FileName_Location);
			}

			return false;


		}

		/// <summary>
		/// Zaloguje Vynimku
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public bool write(Exception ex)
		{

			StringBuilder sbError = new StringBuilder();

			sbError.AppendLine(string.Format("{0}:{1}\n{2}", ex.Message, ex.Source, ex.StackTrace));
			string errmsg = sbError.ToString();

			return SaveToFile(errmsg, FileName_Error);
		}

		/// <summary>
		/// Zaloguje Vinimku a modulem a Metodou
		/// </summary>
		/// <param name="ex"></param>
		/// <param name="Modul"></param>
		/// <param name="Metoda"></param>
		/// <returns></returns>
		public bool write(Exception ex, string Modul, string Metoda)
		{
			StringBuilder sbError = new StringBuilder();


			sbError.AppendLine(string.Format("{0}.{1}:\n{2}\n{3}\n{4}", Modul, Metoda, ex.Message, ex.Source, ex.StackTrace));
			string errmsg = sbError.ToString();

			return SaveToFile(errmsg, FileName_Error);
		}

		/// <summary>
		/// Zaloguje správu a modulem a Metodou do konkretniho souboru
		/// </summary>
		/// <param name="level"></param>
		/// <param name="Modul"></param>
		/// <param name="Metoda"></param>
		/// <param name="Message"></param>
		/// <returns></returns>
		public bool write(LogLevel level, string Modul, string Metoda, string Message)
		{
			return write(level, Modul + "." + Metoda + ":\n" + Message);
		}

		#endregion

		#region WriteToCustomFile

		public bool writeToFile(string message, string PathToFile)
		{
			System.IO.StreamWriter sw = null;
			try
			{
				string dirpath = System.IO.Path.GetDirectoryName(PathToFile);
				if (!System.IO.Directory.Exists(dirpath))
					System.IO.Directory.CreateDirectory(dirpath);

				if (PathToFile == null || PathToFile == string.Empty)
					return true;

				sw = new StreamWriter(PathToFile, true);
				sw.Write(message);
				sw.Close();
				sw = null;

				return true;
			}
			catch(Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw = null;
				}
			}
		}

		#endregion

		#region Save to File
		/// <summary>
		/// Slouží pro zapis do souboru
		/// </summary>
		/// <param name="MSG">Zpáva pro zapis</param>
		/// <param name="FileName">Nazev souboru</param>
		/// <returns></returns>
		private bool SaveToFile(string MSG, string FileName)
		{

			System.IO.StreamWriter sw = null;
			try
			{
				string logpath = Path.Combine(_pathFile, FileName);

				string dirpath = System.IO.Path.GetDirectoryName(logpath);
				if (!System.IO.Directory.Exists(dirpath))
					System.IO.Directory.CreateDirectory(dirpath);


				if (logpath == null || logpath == string.Empty)
					return true;

				sw = new StreamWriter(logpath, true);
				sw.WriteLine(DateTime.Now.ToString("G") + " : " + MSG.Trim());
				sw.Close();
				sw = null;

				//TODO : Odeslani emailu
				//SendEmail(errormessage);

				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw = null;
				}
			}

		}
		#endregion

		#region DataSets a DataTable

		/// <summary>
		/// Slouží pro zalogovani chyby v Datasetu
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public bool write(System.Data.DataSet ds)
		{
			foreach (System.Data.DataTable item in ds.Tables)
			{
				write(item);
			}

			return true;
		}

		/// <summary>
		/// SLouži pro zalogovani chyby v DataTable
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public bool write(System.Data.DataTable table)
		{
			try
			{
				if (table.HasErrors)
				{
					string msg = string.Empty;
					msg = "Has errors: " + table.HasErrors.ToString() + Environment.NewLine;
					msg += "Table Name:" + table.TableName + Environment.NewLine;
					if (table.HasErrors)
					{
						//TODO : Uklada chyby Rows... sou ješte chyby Columns??
						foreach (System.Data.DataRow row in table.Rows)
						{
							if (row.HasErrors)
								msg += "\t " + row.RowError + Environment.NewLine;
						}
					}

					SaveToFile(msg, FileName_Error);
				}
			}
			catch (Exception ex)
			{
				write(ex);
				return false;
			}

			return true;
		}

		public void SetPath(string Path)
		{
			this._pathFile = System.IO.Path.Combine(Path, "Logs");
		}


		#endregion




		#region ILog2 Members


		public bool DeleteLog(LogLevel level)
		{
			try
			{
				switch (level)
				{
					case LogLevel.Trace:
						DeleteFile(FileName_Trace);
						break;
					case LogLevel.Debug:
						DeleteFile(FileName_Debug);
						break;
					case LogLevel.Info:
						DeleteFile(FileName_Info);
						break;
					case LogLevel.Warn:
						DeleteFile(FileName_Warn);
						break;
					case LogLevel.Error:
						DeleteFile(FileName_Error);
						break;
					case LogLevel.Fatal:
						DeleteFile(FileName_Fatal);
						break;
					case LogLevel.PrintInfo:
						DeleteFile(FileName_PrintInfo);
						break;
					case LogLevel.Licence:
						DeleteFile(FileName_License);
						break;
					default:
						break;
				}

				return true;

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}
		}

		private void DeleteFile(string FileName)
		{
			string logpath = Path.Combine(this._pathFile, FileName);

			if (File.Exists(logpath))
				File.Delete(logpath);
		}

		#endregion

		#region ILog2 Members


		public string write(byte[] data, string specification)
		{

			string errordatapath = Path.Combine(_pathFile, "ErrorDataFileDirectory");

			if (!System.IO.Directory.Exists(errordatapath))
				System.IO.Directory.CreateDirectory(errordatapath);

			string PathFile = Path.Combine(errordatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data");

			FileStream fs = null;
			try
			{
				

				fs = new FileStream(
					PathFile,
					FileMode.Create,
					FileAccess.Write
					);
				fs.Write(data, 0, data.Length);

			}
			finally
			{
				if (fs != null)
				{
					fs.Flush();
					fs.Close();
					fs = null;
				}
			}

			return PathFile;
		}

		#endregion

		#region ILog2 Members


		public bool write_Email(string Message)
		{
			//SendEmail()
			return write(Logging.LogLevel.Info, Message);
		}

		#endregion
	}
}
