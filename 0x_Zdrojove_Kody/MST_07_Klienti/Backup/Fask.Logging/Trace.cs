using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;

namespace Fask.Logging
{
    //public class Trace
    //{
    //    public static readonly object lock_file_in_use = new object();

    //    public static string Directory { get; set; }
    //    public static string Filename { get; set; }
    //    public static string FilePath { get; private set; }
    //    public static bool Enable { get; set; }
    //    public static string Separator { get; set; }

    //    private static System.Diagnostics.Stopwatch time_elapsed = new System.Diagnostics.Stopwatch();

    //    //OpenNETCF.Diagnostics.Stopwatch sp = new OpenNETCF.Diagnostics.Stopwatch();

    //    private static DateTime? time_start = null;
    //    private static DateTime? time_stop = null;

    //    static Trace()
    //    {
    //        Directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
    //        Filename = "trace.txt";
    //        FilePath = Path.Combine(Directory, Filename);
    //    }

    //    public class Message
    //    {
    //        public Message(
    //            string module,
    //            string object_type,
    //            string operation_name,
    //            string description
    //        )
    //        {
    //            this.module = module;
    //            this.object_type = object_type;
    //            this.operation_name = operation_name;
    //            this.description = description;
    //        }

    //        private string module;
    //        private string object_type;
    //        private string operation_name;
    //        private string description;
    //        public DateTime? time_start;
    //        public DateTime? time_stop;
    //        public long time_elapsed_ms;

    //        public string toCSV(string separator)
    //        {
    //            string time_format = "yyyy-MM-dd HH:mm:ss";
    //            string[] values = { 
    //                time_start != null ? ((DateTime)time_start).ToString(time_format) : "",
    //                time_stop != null ? ((DateTime)time_stop).ToString(time_format) : "",
    //                time_elapsed_ms.ToString(),
    //                module,
    //                object_type, 
    //                operation_name, 
    //                description
    //            };

    //            return String.Join(separator, values);
    //        }
    //    }

    //    public static void StartTimer()
    //    {
    //        if (!Enable)
    //            return;
    //        time_elapsed.Start();
    //        time_start = DateTime.Now;
    //    }

    //    private static void StopTimer()
    //    {
    //        if (!Enable)
    //            return;
    //        time_elapsed.Stop();
    //        time_stop = DateTime.Now;
    //    }

    //    private static void ResetTimer()
    //    {
    //        if (!Enable)
    //            return;
    //        time_elapsed.Reset();
    //        time_start = time_stop = null;
    //    }

    //    public static void Write(Message msg)
    //    {
    //        if (!Enable)
    //            return;

    //        if (time_stop == null)
    //            StopTimer();
    //        msg.time_start = time_start;
    //        msg.time_stop = time_stop;
    //        msg.time_elapsed_ms = time_elapsed.ElapsedTicks / TimeSpan.TicksPerMillisecond;

    //        using (StreamWriter w = File.AppendText(FilePath))
    //        {
    //            w.WriteLine(msg.toCSV(Separator));
    //            w.Flush();
    //        }
    //        ResetTimer();
    //    }

    //}



    public class Trace2
    {

        #region cesta k souboru

        // TracingDataFileDirectory

        private static string TracingDataFileDirectory
        {
            get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase); }
        }

        #endregion

        #region Promenne

        private static string _dirlog = Trace2.TracingDataFileDirectory;
        public static string Directory
        {
            get { return _dirlog; }
            set { _dirlog = value; }
        }

        private static string _filelog = "trace.txt";
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

        public static string FilePath
        {
            get { return System.IO.Path.Combine(_dirlog, _filelog); }
        }

        public static string Separator { get; set; }

        public static readonly object lock_file_in_use = new object();


        #endregion



        public static void Write(string Status, string message, TracId h)
        {
            if (!Logging.Trace2.Enable)
                return;


            h.Stop();
            WriteToFile(Status, message, h);
            h.StopToStart();
        }

        private static void WriteToFile(string Status, string message, TracId h)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                if (!System.IO.Directory.Exists(_dirlog))
                    System.IO.Directory.CreateDirectory(_dirlog);


                if (!System.IO.File.Exists(System.IO.Path.Combine(_dirlog, _filelog)))
                {
                    sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);

                    string[] values = { 
                                  "Status",
                                  "TimeStart",
                                  "TimeStop",
                                  "TickStart",
                                  "TickStop",
                                  "DivTime",
                                  "UserId",
                                  "TerminalId",
                                  "FormName",
                                  "MetodaName",
                                  "Davka",
                                  "message",
                                  "Identificator"                                  
                              };

                    sw.WriteLine(String.Join(Trace2.Separator, values));
                }
                else
                    sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);

                //sw = new System.IO.StreamWriter(_dirlog, true);
                //sw.WriteLine(
                //    DateTime.Now.ToString() + ": " + (context.Length != 0 ? "(" + context + ") " : context)
                //    + ((h != null) ? h.ToString() : string.Empty)
                //    + "\r\n" + message);
                sw.WriteLine(toCSV(Trace2.Separator, Status, message, h));
            }
            catch (Exception ex)
            {
                string pom = ex.Message;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
                sw = null;
            }
        }

        private static string toCSV(string separator, string Status, string message, TracId h)
        {
            // Write// "Status", "DateStart","DateStop","TickStart", "TickStop", "Message"  a cele TrackID
            // TrackID // "UserID", "TerminalID", "Form", "Nazev metody" , "davka",  generuje sa >> "GUID"

            //string time_format = "yyyy-MM-dd HH:mm:ss";
            string[] values = { 
                                  Status,
                                  h.TimeStart.ToString(),
                                  h.TimeStop.ToString(),
                                  h.TickStart.ToString(),
                                  h.TickStop.ToString(),
                                  WriteDateDiv(h.TickStart,h.TickStop),
                                  h.UserId.ToString(),
                                  h.TerminalId.ToString(),
                                  h.FormName,
                                  h.MetodaName,
                                  h.Davka.ToString(),
                                  message,
                                  h.Identificator.ToString()                                  
                              };

            return String.Join(separator, values);
        }

        private static string WriteDateDiv(int Start, int Stop)
        {


            int time = Stop - Start;
            return String.Format("{0}", time);


        }




    }

}