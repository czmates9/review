using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FASK.SledovaniVyroby.Main.ErrorLog
{
	/// <summary>
	/// Logovani chyb do suboru
	/// </summary>
	public class Logging {
        private static string _dirlog = "/";
        public static string Directory
        {
            get { return _dirlog; }
            set { _dirlog = value; }
        }
		private static string _filelog = "log.txt";
        public static string LogFile
        {
            get { return _filelog; }
            set { _filelog = value; }
        }
		private static bool _doLogging = true;
        public static bool Enable
        {
            get { return _doLogging; }
            set { _doLogging = value; }
        }

        static Logging()
        {
            
            _dirlog = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath;
        }

        public static void WriteException(Exception ex)
        {
            Write(ex.Message + "\n" + ex.StackTrace, ex.Source);
        }

        public static void Write(string message)
        {
            Write(message, string.Empty);
        }

		public static void Write(string message, string context) {
            if (!_doLogging)
                return;

			System.IO.StreamWriter sw = null;
			try {
                sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);
				sw.WriteLine( DateTime.Now.ToString() + ": " + (context.Length != 0 ? "(" + context + ") " : context) + message);
			} 
			catch(Exception ex) {
                
			}
			finally {
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

        public static string GetLog()
        {
            string res = string.Empty;

            StreamReader re = null;

            try
            {
                re = File.OpenText(System.IO.Path.Combine(_dirlog, _filelog));
                res = re.ReadToEnd();
            }
            catch { return res; }
            finally
            {
                if (re != null)
                    re.Close();
            }

            return res;
        }

        public static void ClearLog()
        {
            if (!_doLogging)
                return;
            
            System.IO.StreamWriter sw = null;
            try
            {
                File.Delete(System.IO.Path.Combine(_dirlog, _filelog));

                sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);
                sw.WriteLine(string.Empty);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }

        }
	}

}
