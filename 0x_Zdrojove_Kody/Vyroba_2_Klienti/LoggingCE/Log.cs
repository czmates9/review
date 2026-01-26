using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web.Services.Protocols;

namespace Fask.LoggingCE {
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Log {
        private static string _dirlog = "/";
        public static string Directory
        {
            get { return _dirlog; }
            set { _dirlog = value; }
        }
		private static string _filelog = "log.txt";
        public static string File
        {
            get { return _filelog; }
            set { _filelog = value; }
        }
		private static bool _doLoggingCECE = false;
        public static bool Enable
        {
            get { return _doLoggingCECE; }
            set { _doLoggingCECE = value; }
        }

        static Log()
        {
            _dirlog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        public static void WriteDebug(string message)
        {
            WriteDebug(message, string.Empty);
        }

        public static void Write(string message)
        {
            Write(message, string.Empty);
        }

        public static void Write(WebException webex)
        {
            Write(webex, "WebException");
        }

        public static void Write(TypeLoadException tlex)
        {
            Write(tlex, string.Empty);
        }

        public static void Write(TypeLoadException tlex, string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + tlex.GetType().ToString());
            sb.AppendLine("Message :" + tlex.Message == null ? "null" : tlex.Message);
            if (tlex is DllNotFoundException)
                sb.AppendLine("DllNotFoundException:" + ((DllNotFoundException)tlex).Message);
            else if (tlex is EntryPointNotFoundException)
                sb.AppendLine("EntryPointNotFoundException:" + ((EntryPointNotFoundException)tlex).Message);

            //??? Vyhazuje vyjimku NullReferenceException i kdyz je Response testovany na null ????
            //sb.AppendLine("Response:" + webex.Response == null ? "null" : webex.Response.ToString());
            if (tlex.InnerException != null)
            {
                sb.AppendLine("InnerException");
                sb.AppendLine("   Message: " + tlex.InnerException.Message == null ? "null" : tlex.InnerException.Message);
            }

            WriteToFile(sb.ToString() + "\n" + tlex.StackTrace, context);
        }

        public static void Write(WebException webex, string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + webex.GetType().ToString());
            sb.AppendLine("Message :" + webex.Message == null ? "null" : webex.Message);
            sb.AppendLine("Satus   :" + webex.Status == null ? "null" : webex.Status.ToString());
            //??? Vyhazuje vyjimku NullReferenceException i kdyz je Response testovany na null ????
            //sb.AppendLine("Response:" + webex.Response == null ? "null" : webex.Response.ToString());
            if (webex.InnerException != null)
            {
                sb.AppendLine("InnerException");
                sb.AppendLine("   Message: " + webex.InnerException.Message == null ? "null" : webex.InnerException.Message);
            }

            WriteToFile(sb.ToString() + "\n" + webex.StackTrace, context);
        }
        public static void Write(SoapException soapException, string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + soapException.GetType().ToString());
            sb.AppendLine("Message :" + soapException.Message == null ? "null" : soapException.Message);
            sb.AppendLine("Actor   :" + soapException.Actor == null ? "null" : soapException.Actor);
            sb.AppendLine("Role    :" + soapException.Role == null ? "null" : soapException.Role);
            if (soapException.InnerException != null)
            {
                sb.AppendLine("InnerException");
                sb.AppendLine("   Message: " + soapException.InnerException.Message == null ? "null" : soapException.InnerException.Message);
            }

            WriteToFile(sb.ToString() + "\n" + soapException.StackTrace, context);
        }

        public static void Write(Exception ex)
        {
            Write(ex, "Exception");
        }

        public static void Write(Exception ex, string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + ex.GetType().ToString());
            sb.AppendLine("Message :" + ex.Message == null ? "null" : ex.Message);
            WriteToFile(sb.ToString() + "\n" + ex.StackTrace, context);
        }

        public static void WriteDebug(string message, string context)
        {
#if DEBUG
            Write(message, context);
#endif
        }

		public static void Write(string message, string context) {
            if (!_doLoggingCECE)
                return;

            WriteToFile(message, context);
		}

        private static void WriteToFile(string message, string context)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + (context.Length != 0 ? "(" + context + ") " : context) + message);
            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

		public static void Backup()
		{
			try 
			{
                System.IO.File.Move(System.IO.Path.Combine(_dirlog, _filelog), "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
			} 
			catch {}
		}

		public static void Delete()
		{
            System.IO.File.Delete(System.IO.Path.Combine(_dirlog, _filelog));
		}

        //// TODO : shrink log file ...
        //public static void Shrink()
        //{
        //    long maxsize = 1024 * 1000; //1MB
        //    long restsize = 1024 * 10; //10KB
        //    string logfile = System.IO.Path.Combine(_dirlog, _filelog);
        //    System.IO.FileInfo fi = new System.IO.FileInfo(logfile);
        //    if (fi.Length < (maxsize))
        //        return;
        //    string restdata = string.Empty;
        //    System.IO.StreamReader sr = fi.OpenText();
        //    if (sr.BaseStream.CanSeek)
        //    {
        //        sr.BaseStream.Seek(restsize, System.IO.SeekOrigin.End);
        //        sr.ReadLine(); //na dalsi radek
        //        restdata = sr.ReadToEnd();
        //    }
        //    sr.Close();
        //    System.IO.StreamWriter sw = fi.CreateText();
        //}

	}
}
