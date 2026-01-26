using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;



namespace FASK.SledovaniVyroby.Module.ZZS.Forms.FileTransfer
{
    public class Uploading
    {

        public static string SendFile(string sWebAddress, string filePath_Z_kama, string filePath_kam)
        {
            WebResponse response = null;

            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");  //------------e9480b9c84c64d16a8fdd91bdc7b440e

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(sWebAddress);
                //wr.ContentLength = 
                wr.SendChunked = true;
                wr.ContentType = "multipart/form-data; boundary=" + boundary; //multipart/form-data; boundary=------------e9480b9c84c64d16a8fdd91bdc7b440e
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

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n"); //------------e9480b9c84c64d16a8fdd91bdc7b440e
                stream.Write(trailer, 0, trailer.Length);
                stream.Close();

                response = wr.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string responseData = streamReader.ReadToEnd();

                return "OK";
                //return responseData;
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



        public static string SendFileWithZip(string sWebAddress, string filePath_Z_kama, string filePath_kam)
        {
            WebResponse response = null;


            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                try
                {
                    zip.AddFile(filePath_Z_kama, "");
                    zip.Comment = "Made in FASK";
                    zip.Save(filePath_Z_kama + ".zip");
                    zip.Dispose();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }

            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");  //------------e9480b9c84c64d16a8fdd91bdc7b440e

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(sWebAddress);
                //wr.ContentLength = 
                wr.SendChunked = true;
                wr.ContentType = "multipart/form-data; boundary=" + boundary; //multipart/form-data; boundary=------------e9480b9c84c64d16a8fdd91bdc7b440e
                wr.Method = "POST";
                wr.KeepAlive = true;
                wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

                Stream stream = wr.GetRequestStream();
                stream.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(filePath_Z_kama +".zip");

                stream.Write(formitembytes, 0, formitembytes.Length);

                stream.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = string.Format(headerTemplate, "file", filePath_kam, Path.GetExtension(filePath_Z_kama + ".zip"));

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                stream.Write(headerbytes, 0, headerbytes.Length);

                FileStream fileStream = new FileStream(filePath_Z_kama + ".zip", FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    stream.Write(buffer, 0, bytesRead);
                fileStream.Close();

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n"); //------------e9480b9c84c64d16a8fdd91bdc7b440e
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

                if (File.Exists(filePath_Z_kama))
                    File.Delete(filePath_Z_kama);

                if (File.Exists(filePath_Z_kama + ".zip"))
                    File.Delete(filePath_Z_kama + ".zip");
            }
        }



/// <summary>
/// Metoda slouzici pro odesilani dat na server
/// </summary>
/// <param name="sWebAddress">Adrea web serveru + Upload.asmx kde je implemnenovane spracovani prijateho requestu</param>
/// <param name="filePath_Z_kama"> Path k souboru ktery se ma odesilat. testovane s .zip , pokud bz slo o jinou priponu mnela bz bzt definovana n MIME v IIS </param>
/// <param name="filePath_kam">cesta kam se ma soubor ulozit na sevreru. Momentalne je to napevno slozka SQLCEDB a odeslat je potreba  TerminalID/Davka.zip </param>
/// <returns></returns>
        public static string SendFileCalcTime(string sWebAddress, string filePath_Z_kama, string filePath_kam)
        {
            WebResponse response = null;

            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");  //------------e9480b9c84c64d16a8fdd91bdc7b440e

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(sWebAddress);
                //wr.ContentLength = 
                wr.SendChunked = true;
                wr.ContentType = "multipart/form-data; boundary=" + boundary; //multipart/form-data; boundary=------------e9480b9c84c64d16a8fdd91bdc7b440e
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


                
                int prevTick = Environment.TickCount;
                decimal celkovoKodeslani = fileStream.Length;
                decimal lastingSeconds;
                int errorRepeatCount = 0;
                int errorRepeatCountMax = 5;


                //while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                //{
                //    celkovoKodeslani -= bytesRead;

                //    decimal procento = ((1 - ((decimal)celkovoKodeslani / (decimal)fileStream.Length)) * 100);

                //    decimal kB = buffer.Length / 1024;
                //    decimal s = (decimal)(Environment.TickCount - prevTick) / (decimal)1000;

                //    decimal downloadspeed = kB / s;
                    
                //    prevTick = Environment.TickCount;

                //    lastingSeconds = (decimal)(celkovoKodeslani) / (decimal)(downloadspeed * 1024);


                //    //TimeSpan odhad = TimeSpan.FromMinutes(lastingSeconds);

                //    stream.Write(buffer, 0, bytesRead);
                //    Program.mstw.mbw.Zprava = String.Format("Percent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds);

                //    //System.Diagnostics.Debug.Write(String.Format("Procento:{0}% rychlost:{1} kB/s" + Environment.NewLine , procento.ToString(), downloadspeed.ToString()));
                //}

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
                            decimal s = (decimal)(Environment.TickCount - prevTick) / (decimal)1000;

                            decimal downloadspeed = kB / s;

                            prevTick = Environment.TickCount;

                            lastingSeconds = (decimal)(celkovoKodeslani) / (decimal)(downloadspeed * 1024);


                            //TimeSpan odhad = TimeSpan.FromMinutes(lastingSeconds);

                            stream.Write(buffer, 0, bytesRead);

                            //Program.mstw.mbw.Zprava = String.Format("Percent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds);
                            
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
                    //while (count != 0 && errorRepeatCount < errorRepeatCountMax);
                    while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);
                }
                while (celkovoKodeslani != 0);

                fileStream.Close();

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n"); //------------e9480b9c84c64d16a8fdd91bdc7b440e
                stream.Write(trailer, 0, trailer.Length);
                stream.Close();

                response = wr.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string responseData = streamReader.ReadToEnd();

                //Program.mstw.mbw.Zprava = "Uspešne odeslano\nZpracovávam...";
                //return responseData;
                return "OK";
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
