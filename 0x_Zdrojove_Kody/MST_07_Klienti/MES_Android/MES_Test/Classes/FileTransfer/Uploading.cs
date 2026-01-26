using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace MES_Android.Classes.FileTransfer
{
    public class Uploading
    {
        public enum Co
        {
            Log,
            Prijem,
            Vydej,
            Events,
            Inventura1,
            Inventura2,
            Prodej,
            Servis,
            unknow
        }

        /// <summary>
        /// Metoda slouzici pro odesilani dat na server
        /// </summary>
        /// <param name="sWebAddress">Adrea web serveru + Upload.asmx kde je implemnenovane zpracovani prijateho requestu</param>
        /// <param name="filePath_Z_kama"> Path k souboru ktery se ma odesilat. testovane s .zip , pokud bz slo o jinou priponu mnela bz bzt definovana n MIME v IIS </param>
        /// <param name="filePath_kam">cesta kam se ma soubor ulozit na sevreru. Momentalne je to napevno slozka SQLCEDB a odeslat je potreba  TerminalID/Davka.zip </param>
        /// <returns></returns>
        public static string SendFileCalcTime(string sWebAddress, string filePath_Z_kama, string filePath_kam, Co Co)
        {
            HttpWebRequest request = null;
            Stream streamRequest = null;
            FileStream fileStream = null;

            //WebResponse response = null;
            //Stream responseStream = null;
            StreamReader streamResponseReader = null;

            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");  //------------e9480b9c84c64d16a8fdd91bdc7b440e

                request = (HttpWebRequest)WebRequest.Create(sWebAddress);
                //wr.ContentLength = 
                request.SendChunked = true;
                request.ContentType = "multipart/form-data; boundary=" + boundary; //multipart/form-data; boundary=------------e9480b9c84c64d16a8fdd91bdc7b440e
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                request.Timeout = Config.Settings.TimeOut;

                streamRequest = request.GetRequestStream();
                streamRequest.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(filePath_Z_kama);

                streamRequest.Write(formitembytes, 0, formitembytes.Length);

                streamRequest.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = string.Format(headerTemplate, "file", filePath_kam, Path.GetExtension(filePath_Z_kama));

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                streamRequest.Write(headerbytes, 0, headerbytes.Length);

                fileStream = new FileStream(filePath_Z_kama, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[4096];
                int bytesRead = 0;



                int prevTick = System.Environment.TickCount;
                decimal celkovoKodeslani = fileStream.Length;
                decimal lastingSeconds;
                int errorRepeatCount = 0;
                int errorRepeatCountMax = 5;

                do
                {
                    do
                    {
                        try
                        {
                            bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                            celkovoKodeslani -= bytesRead;

                            decimal procento = ((1 - ((decimal)celkovoKodeslani / (decimal)fileStream.Length)) * 100);

                            decimal kB = buffer.Length / 1024;
                            decimal s = (decimal)(System.Environment.TickCount - prevTick) / (decimal)1000;

                            if (s == 0)
                                s = 1;

                            decimal downloadspeed = kB / s;

                            prevTick = System.Environment.TickCount;

                            lastingSeconds = (decimal)(celkovoKodeslani) / (decimal)(downloadspeed * 1024);

                            //TimeSpan odhad = TimeSpan.FromMinutes(lastingSeconds);

                            streamRequest.Write(buffer, 0, bytesRead);


                            //dialog.Zprava = String.Format("Procenta: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds);
                            Classes.ProgressDialog_Infinity.Message = String.Format("Procenta: {0:##0.00}% \nRychlost: {1:####0.00}kB/s\nOdhad: {2:##0.00}s", procento, downloadspeed, lastingSeconds);

                            errorRepeatCount = 0;

                        }
                        catch (WebException webEx)
                        {
                            errorRepeatCount++;
                            if (errorRepeatCount >= errorRepeatCountMax)
                                throw new Exception("Chyba při Odesílaní dat", webEx);
                        }
                        catch (Exception ex)
                        {
                            errorRepeatCount++;
                            if (errorRepeatCount >= errorRepeatCountMax)
                                throw new Exception("Chyba při Odesílaní dat", ex);
                        }
                    }
                    while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);
                }
                while (celkovoKodeslani != 0);

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n"); //------------e9480b9c84c64d16a8fdd91bdc7b440e
                streamRequest.Write(trailer, 0, trailer.Length);
                streamRequest.Close();
                streamRequest = null;

                //response = wr.GetResponse();
                //responseStream = response.GetResponseStream();
                streamResponseReader = new StreamReader(request.GetResponse().GetResponseStream());
                string responseData = streamResponseReader.ReadToEnd();
                streamResponseReader.Close();
                streamResponseReader.Dispose();
                streamResponseReader = null;

                //dialog.Zprava = "Úspěšně odesláno: " + Co.ToString() + "\nProbíhá zpracování...";
                Classes.ProgressDialog_Infinity.Message = "Úspěšně odesláno: " + Co.ToString() + "\nProbíhá zpracování...";
                

                //return responseData;
                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
            finally
            {
                try
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                        fileStream = null;
                    }
                }
                catch (Exception exFileStream)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exFileStream);
                }

                try
                {
                    if (streamResponseReader != null)
                    {
                        streamResponseReader.Close();
                        streamResponseReader = null;
                    }
                }
                catch (Exception exStreamReader)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exStreamReader);
                }

                try
                {
                    if ((streamRequest != null))
                    {
                        streamRequest.Close();
                        streamRequest = null;
                    }
                }
                catch (Exception exStream)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exStream);
                }

                if (File.Exists(filePath_Z_kama))
                {
                    File.Delete(filePath_Z_kama);
                }
            }
        }

    }
}