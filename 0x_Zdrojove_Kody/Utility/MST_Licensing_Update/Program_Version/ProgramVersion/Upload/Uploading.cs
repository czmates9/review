using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ProgramVersion.Upload
{
   public class Uploading
    {

       public static string SendFile(string sWebAddress, string filePath_Z_kama, string filePath_kam)
       {
           WebResponse response = null;
           try
           {
               string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
               byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
               HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(sWebAddress);
               //wr.ContentLength = 
               wr.SendChunked = true;
               wr.ContentType = "multipart/form-data; boundary=" + boundary;
               wr.Method = "POST";
               wr.KeepAlive = true;
               wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
               Stream stream = wr.GetRequestStream();
               stream.Write(boundarybytes, 0, boundarybytes.Length);
               byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(filePath_Z_kama);
               stream.Write(formitembytes, 0, formitembytes.Length);
               stream.Write(boundarybytes, 0, boundarybytes.Length);
               string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
               string header = string.Format(headerTemplate, "file", filePath_kam, Path.GetExtension(filePath_Z_kama));
               byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
               stream.Write(headerbytes, 0, headerbytes.Length);

               FileStream fileStream = new FileStream(filePath_Z_kama, FileMode.Open, FileAccess.Read);
               byte[] buffer = new byte[4096];
               int bytesRead = 0;
               while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                   stream.Write(buffer, 0, bytesRead);
               fileStream.Close();

               byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
               stream.Write(trailer, 0, trailer.Length);
               stream.Close();

               response = wr.GetResponse();
               Stream responseStream = response.GetResponseStream();
               StreamReader streamReader = new StreamReader(responseStream);
               string responseData = streamReader.ReadToEnd();
               return responseData;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
           finally
           {
               if (response != null)
                   response.Close();
           }
       }


    }
}
