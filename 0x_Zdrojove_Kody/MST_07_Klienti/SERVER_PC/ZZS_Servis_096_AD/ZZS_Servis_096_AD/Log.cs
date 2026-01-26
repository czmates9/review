using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZZS_Servis_096_AD
{
    public class Log
    {


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
        private static bool _doLogging = false;
        public static bool Enable
        {
            get { return _doLogging; }
            set { _doLogging = value; }
        }

        static Log()
        {

            _dirlog = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath;
        }

        public static void Write(string message)
        {
            Write(message, string.Empty);
        }

        public static void Write(string message, string context)
        {
            if (!_doLogging)
                return;

            System.IO.StreamWriter sw = null;
            try
            {
                sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + (context.Length != 0 ? "(" + context + ") " : context) + message);
            }
            catch
            {
                ;
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
            catch { }
        }

        public static void Delete()
        {
            System.IO.File.Delete(System.IO.Path.Combine(_dirlog, _filelog));
        }

    }
}
