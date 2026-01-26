using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;


namespace Fask.Upgrade
{
  public class TransferManager
  {
    public delegate void TransferProgress(int progress);

    private TransferProgress transferProgressDelegate;
    private volatile bool abortTransfer = false;

    public TransferManager()
    {
    }

    public void AddObserver(Notification ev)
    {
      ev.AbortUpdateEvent += new Notification.AbortUpdate(Notification_AbortUpdateEvent);
    }
    
    void Notification_AbortUpdateEvent()
    {
      this.abortTransfer = true;
    }

    public bool downloadFile(String url, out Stream s, String path, TransferProgress del)
    {
      this.transferProgressDelegate = del;
      byte[] buffer = new byte[4096];
      FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

      WebRequest wr = WebRequest.Create(url);
      wr.Proxy = System.Net.GlobalProxySelection.Select;
      wr.Timeout = 1000;
      try
      {
          Stopwatch stopWatch = new Stopwatch();
          stopWatch.Start();

        using (WebResponse response = wr.GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {

            int count = 0;
            int dataRead = 0;
            do
            {
              count = responseStream.Read(buffer, 0, buffer.Length);
              fileStream.Write(buffer, 0, count);

              float progress = ((float)dataRead / (float)response.ContentLength) * 100.0f;
              OnProgress((int)progress);

              dataRead += count;
            } while (count != 0 && !abortTransfer);
          }
        }
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
      }
      catch (WebException wex)
      {
         
        Logging.Log.Write(wex,"No Connection to upgrade server...");
        fileStream.Close();
        s = null;
        throw (wex);
      }
      s = fileStream;
      return true;
    }

    protected void OnProgress(int progress)
    {
      if (transferProgressDelegate != null)
      {
        transferProgressDelegate(progress);
      }
    }

      #region Upload data to server



    /// <summary>
    /// Metoda na nahrani dat na server
    /// </summary>
    /// <param name="url">Adresa mista kam se maji nahrát data</param>
    /// <param name="path"> cesta k souboru co se ma poslat </param>
    /// <returns>true pokud je uspech</returns>
    public string UploadFile(String url, String path)
    {
        try
        {
            System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();
            nvc.Add("id", "TTR");
            nvc.Add("btn-submit-photo", "Upload");
            HttpUploadFile(url, path, "file", "image/jpeg", nvc);


            //FileInfo fInfo = new FileInfo(path);
             
            ////long numBytes = fInfo.Length;

            ////FileStream fStream = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read);

            ////BinaryReader br = new BinaryReader(fStream);

            ////byte[] bdata = br.ReadBytes((int)numBytes);

            ////br.Close();

            ////fStream.Close();
            //Uri uri = new Uri(url);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.AbsolutePath);
            //request.Method = "POST";
            //request.KeepAlive = true;
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            //request.ContentType = "multipart/form-data; boundary=" + boundary;
            
            ////request.AllowWriteStreamBuffering = true;

            //request.ContentLength = fInfo.Length; // znam? skusit
            ////request.SendChunked = true;

            //Stream requestStream = request.GetRequestStream();

            //using (FileStream fileStream = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read))
            //{
            //    byte[] buffer = new byte[1024];
            //    int bytesRead = 0;
            //    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            //    {
            //        requestStream.Write(buffer, 0, bytesRead);
            //    }
            //    fileStream.Close();
            //}


            ////requestStream.Write(bdata, 0, bdata.Length);
            ////requestStream.Close();

            //using (WebResponse response = request.GetResponse())
            //using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //{
            //    return reader.ReadToEnd();
            //};

        }
        catch (Exception ex )
        {
            Logging.Log.Write(ex,"Upload data");
            return "BAD";
            //throw;
        }


        #region puvodny pokus 
        //Stream s = null;

        //try
        //{
        //    WebRequest wr = WebRequest.Create(url);
        //    wr.Proxy = System.Net.GlobalProxySelection.Select;
        //    wr.Timeout = 1000;
        //    wr.Credentials = CredentialCache.DefaultCredentials;
        //    ((HttpWebRequest)wr).UserAgent = "MSTW_06";
        //    wr.Method = "POST";
        //    //wr.ContentType = "application/zip";
        //    wr.ContentType = "multipart/form-data";
        //    byte[] array = FileToByteArray(path);
        //    wr.ContentLength = array.Length;
        //    s = wr.GetRequestStream();
        //    s.Write(array, 0, array.Length);
        //    s.Close();
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine(ex.Message.ToString());
        //    return false;
        //}
        //finally
        //{
        //    if (s != null)
        //    {
        //        s.Close();
        //        s = null;
        //    }
        //}

        #endregion
        return "OK";
    }

    public static void HttpUploadFile(string url, string file, string paramName, string contentType, System.Collections.Specialized.NameValueCollection nvc)
    {
        Debug.Write(string.Format("Uploading {0} to {1}", file, url));
        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //wr.AllowWriteStreamBuffering = false;
        //wr.SendChunked = true;
        FileInfo fInfo = new FileInfo(file);
        wr.ContentLength = fInfo.Length;
        Stream rs = wr.GetRequestStream();

        string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        foreach (string key in nvc.Keys)
        {
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string formitem = string.Format(formdataTemplate, key, nvc[key]);
            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            rs.Write(formitembytes, 0, formitembytes.Length);
        }
        rs.Write(boundarybytes, 0, boundarybytes.Length);

        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string header = string.Format(headerTemplate, paramName, file, contentType);
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        rs.Write(headerbytes, 0, headerbytes.Length);

        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[4096];
        int bytesRead = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            rs.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();

        byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);
        rs.Close();

        WebResponse wresp = null;
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            Debug.Write(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
        }
        catch (Exception ex)
        {
            Debug.Write("Error uploading file {0}", ex.Message.ToString());
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
            }
        }
        finally
        {
            wr = null;
        }
    }


    //public static byte[] FileToByteArray(string filename)
    //{
    //    //Logging.Trace.StartTimer();
    //    FileStream fs = null;
    //    byte[] data = new byte[0];
    //    try
    //    {
    //        long fileLen = (new FileInfo(filename)).Length;
    //        data = new byte[fileLen];
    //        fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
    //        int bytesRead = fs.Read(data, 0, (int)fileLen);
    //        if ((long)bytesRead < fileLen)
    //        {
    //            System.Windows.Forms.MessageBox.Show("bytesRead < fileLen : " + filename, "FileOperations, LoadDB");
    //            //Logging.Log.Write("bytesRead < fileLen : " + filename, "FileOperations, LoadDB");
    //        }


    //    }
    //    finally
    //    {
    //        if (fs != null)
    //        {
    //            fs.Close();
    //            fs = null;
    //        }
    //    }
    //    //Logging.Trace.Write(new Logging.Trace.Message("Vydej", "MySystem.FileOperations", "DBLoad", "nacitani dat vydejky"));
    //    return data;
    //}


      #endregion

  }
}
