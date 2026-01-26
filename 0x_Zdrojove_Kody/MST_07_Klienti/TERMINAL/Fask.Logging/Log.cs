using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web.Services.Protocols;
using System.Data;

namespace Fask.Logging {
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
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
        public static string FilePath
        {
            get { return System.IO.Path.Combine(_dirlog, _filelog); }
        }
		private static bool _doLogging = false;
        public static bool Enable
        {
            get { return _doLogging; }
            set { _doLogging = value; }
        }
        /// <summary>
        /// Zapnuti nebo vypnuti rozsireneho logovani - musi byt zapnute logovani zakladni, aby bylo mono logovat rozsirene
        /// </summary>
        private static bool _doAdvancedLogging = false;
        public static bool EnableAdvanced
        {
            get { return _doAdvancedLogging; }
            set { _doAdvancedLogging = value; }
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

        //public static void Write(System.Data.SqlServerCe.SqlCeException sqlCeException, string context)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("Type          :" + sqlCeException.GetType().ToString());
        //    sb.AppendLine("Message       :" + sqlCeException.Message == null ? "null" : sqlCeException.Message);
        //    sb.AppendLine("NativeError   :" + sqlCeException.NativeError.ToString());
        //    sb.AppendLine("Source        :" + sqlCeException.Source == null ? "null" : sqlCeException.Source);
        //    sb.AppendLine("HResult       :" + sqlCeException.HResult.ToString());

        //    if (sqlCeException.Errors != null)
        //    {
        //        sb.AppendLine("Errors");
        //        foreach (System.Data.SqlServerCe.SqlCeError item in sqlCeException.Errors)
        //        {
        //            sb.AppendLine("   Message: " + item.Message == null ? "null" : item.Message);
        //        }
        //    }
            
        //    if (sqlCeException.InnerException != null)
        //    {
        //        sb.AppendLine("InnerException");
        //        sb.AppendLine("   Message: " + sqlCeException.InnerException.Message == null ? "null" : sqlCeException.InnerException.Message);
        //    }
        //    WriteToFile(sb.ToString() + "\n" + sqlCeException.StackTrace, context);
        //}

        //public static void Write(System.Data.SQLite.SQLiteException sqlException, string context)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("Type          :" + sqlException.GetType().ToString());
        //    sb.AppendLine("Message       :" + sqlException.Message == null ? "null" : sqlException.Message);
        //    sb.AppendLine("ErrorCode     :" + sqlException.ErrorCode.ToString());

        //    if (sqlException.InnerException != null)
        //    {
        //        sb.AppendLine("InnerException");
        //        sb.AppendLine("   Message: " + sqlException.InnerException.Message == null ? "null" : sqlException.InnerException.Message);
        //    }
        //    WriteToFile(sb.ToString() + "\n" + sqlException.StackTrace, context);
        //}

        public static void Write(Exception ex, string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + ex.GetType().ToString());
            sb.AppendLine("Message :" + ex.Message == null ? "null" : ex.Message);
            WriteToFile(sb.ToString() + "\n" + ex.StackTrace, context);
        }

        public static void Write(Exception ex, DataSet dataset, string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + ex.GetType().ToString());
            sb.AppendLine("Message :" + ex.Message == null ? "null" : ex.Message);

            if (dataset != null)
            {
                foreach (DataTable table in dataset.Tables)
                {
                    sb.Append(WriteTable(table));
                }
            }
            else sb.Append("Dataset: is null");

            WriteToFile(sb.ToString() + "\n" + ex.StackTrace, context);
        }

        //Zapsani zaznnamu v ramci rozsireneho logovani
        public static void WriteAdvanced(string message, string context)
        {
            if (!_doAdvancedLogging)
                return;

            try
            {
                WriteToFile(message, context);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepodarilo se zapsat informace do rozsireneho logu! Doporucujeme poznamenat/sdelit tuto informaci. Duvod: " + ex.Message + ". Pokud by dana situace nastavala casto (vzdy), je mozne rozsirene logovani vypnmout v nastaveni, ale nepredpoklada se to a neni to zadouci (korektni) stav", "Write Log Error");
            }

        }

        public static void WriteDebug(string message, string context)
        {
#if DEBUG
            Write(message, context);
#endif
        }

		public static void Write(string message, string context) {
            if (!_doLogging)
                return;

            WriteToFile(message, context);
		}

        public static void Write(DataSet dataset, string context)
        {
            if (!_doLogging)
                return;
            if (dataset == null)
                return;

            StringBuilder sb = new StringBuilder();

            foreach (DataTable table in dataset.Tables)
            {
                sb.Append(WriteTable(table));
            }

            WriteToFile(sb.ToString(), context);
        }

        public static string WriteTable(DataTable table)
        {
            if (table == null)
                return string.Empty;
            
            StringBuilder sb = new StringBuilder();
            StringBuilder sbrow = new StringBuilder();
            try
            {
                sb.Append("Table:" + table.TableName
                           + " (rows count:" + table.Rows.Count.ToString() + ")");
                sb.Append(Environment.NewLine);
                if (table.Rows.Count> 0)//  (data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow dr in table.Rows)//.Rows[0])
                    {
                        foreach (DataColumn dc in table.Columns)
                        {
                            //sb.Append(dc.ColumnName + ":" + dr[dc.ColumnName].ToString() + ";");
                            sbrow.Append(dc.ColumnName + ":" + dr[dc.ColumnName].ToString() + ";");

                            //if (!printData.ContainsKey(dc.ColumnName))
                            //    printData.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                        }
                        sb.AppendLine(sbrow.ToString());
                        sbrow.Length = 0;
                        sbrow.Capacity = 0;
                    }
                }
            }
            catch
            {
            }
            return sb.ToString();
        }


        private static void WriteToFile(string message, string context)
        {
                System.IO.StreamWriter sw = null;
                try
                {
                    sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);
                    sw.WriteLine(DateTime.Now.ToString() + ": " + (context.Length != 0 ? "(" + context + ") " : context) + message);
                }
                catch (Exception exWrite)
                {
                    string m = exWrite.Message; //?? co ted?
                    //MessageBox.Show(exWrite.Message, "logging", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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
