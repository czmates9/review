using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Fask.Tracing
{
    public class Trac
    {
        //private static string _dirlog = "/";
        //private static string _dirlog = Fask.MyPath.Path.TracingDataFileDirectory;
        //public static string Directory
        //{
        //    get { return _dirlog; }
        //    set { _dirlog = value; }
        //}

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

        public static void Write(string message)
        {
            if (!_doLogging)
                return;

            WriteToFile(message, string.Empty, null);
        }

        public static void Write(string message, string context)
        {
            if (!_doLogging)
                return;

            WriteToFile(message, context, null);
        }

        public static void Write(string message, TracId h) 
        {
            if (!_doLogging)
                return;

            WriteToFile(message, h.Context ?? string.Empty, h);
        }

        public static void Write(string message, string context, TracId h)
        {
            if (!_doLogging)
                return;

            WriteToFile(message, context, h);
        }

        public static void Write(Exception ex, TracId h)
        {
            if (!_doLogging)
                return;

            Write(ex, h.Context ?? string.Empty, h);
        }

        public static void Write(Exception ex, string context, TracId h)
        {
            if (!_doLogging)
                return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type    :" + ex.GetType().ToString());
            sb.AppendLine("Message :" + ex.Message == null ? "Exception" : ex.Message);
            WriteToFile(sb.ToString() + "\n" + ex.StackTrace, context, h);
        }

        /// <summary>
        /// Výpis všech tabulek z datasetu.
        /// </summary>
        /// <param name="vydej"></param>
        public static void Write(DataSet vydej, TracId h)
        {
            if (!_doLogging)
                return;

            //Write(vydej, string.Empty, h);
            Write(vydej, h.Context ?? string.Empty, h);
        }

        /// <summary>
        /// Výpis všech tabulek z datasetu a kontextu.
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="context"></param>
        public static void Write(DataSet dataset, string context, TracId h)
        {
            if (!_doLogging)
                return;

            StringBuilder sb = new StringBuilder();

            if (dataset != null)
            {
                sb.AppendLine("Dataset, tables count(" + dataset.Tables.Count.ToString()
                    + "), has errors: " + dataset.HasErrors.ToString());

                foreach (DataTable table in dataset.Tables)
                {
                    sb.Append(WriteTable(table));
                }
            }
            else sb.AppendLine("Dataset is null");

            WriteToFile(sb.ToString(), context, h);
        }

        /// <summary>
        /// Vypsání parametrů z DataTable (název, počet záznamů, změny v tabulce, chyby).
        /// </summary>
        /// <param name="table">DataTable, z které chceme vypsat parametry.</param>
        public static void Write(DataTable table, TracId h)
        {
            if (!_doLogging)
                return;

            WriteToFile(WriteTable(table), h.Context ?? string.Empty, h);
        }

        /// <summary>
        /// Výpis parametrů tabulky.
        /// </summary>
        /// <param name="table">Tabulka, která se má vypsat.</param>
        /// <returns>Výpis.</returns>
        private static string WriteTable(DataTable table)
        {
            if (table == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            int added, deleted, detached, modified, unchanged;
            added = deleted = detached = modified = unchanged = 0;
            try
            {
                sb.AppendLine("Table: " + table.TableName
                            + " (rows count: " + table.Rows.Count.ToString() + ")");

                //// nastaly změny
                //if (table.GetChanges() != null)
                //{
                //    if (table.GetChanges(DataRowState.Added) != null)
                //        added = table.GetChanges(DataRowState.Added).Rows.Count;
                //    if (table.GetChanges(DataRowState.Deleted) != null)
                //        deleted = table.GetChanges(DataRowState.Deleted).Rows.Count;
                //    if (table.GetChanges(DataRowState.Detached) != null)
                //        detached = table.GetChanges(DataRowState.Detached).Rows.Count;
                //    if (table.GetChanges(DataRowState.Modified) != null)
                //        modified = table.GetChanges(DataRowState.Modified).Rows.Count;
                //    if (table.GetChanges(DataRowState.Unchanged) != null)
                //        unchanged = table.GetChanges(DataRowState.Unchanged).Rows.Count;
                //}

                var drows = table.Select();
                added = drows.Count(x => x.RowState == DataRowState.Added);
                deleted = drows.Count(x => x.RowState == DataRowState.Deleted);
                detached = drows.Count(x => x.RowState == DataRowState.Detached);
                modified = drows.Count(x => x.RowState == DataRowState.Modified);
                unchanged = drows.Count(x => x.RowState == DataRowState.Unchanged);

                sb.AppendLine("Changes (Added, Deleted, Detached, Modified, Unchanged): "
                    + added.ToString() + "/"
                    + deleted.ToString() + "/"
                    + detached.ToString() + "/"
                    + modified.ToString() + "/"
                    + unchanged.ToString());

                // výpis chyb, pokud nějaké nastaly
                if (table.HasErrors)
                {
                    sb.AppendLine("Has errors: " + table.HasErrors.ToString());
                    if (table.HasErrors)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (row.HasErrors)
                                sb.AppendLine("\t " + row.RowError);
                        }
                    }
                }
            }
            catch
            {
            }

            //return sb.ToString().TrimEnd('\r', '\n');
            return sb.ToString();
        }

        /// <summary>
        /// Výpis tabulky a kontextu.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="context"></param>
        public static void Write(DataTable table, string context, TracId h)
        {
            if (!_doLogging)
                return;

            Write(WriteTable(table), context, h);
        }

        /// <summary>
        /// Zápis do souboru.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        private static void WriteToFile(string message, string context, TracId h)
        {
            System.IO.StreamWriter sw = null;
            try
            {                
                if (!System.IO.Directory.Exists(Fask.MyPath.Path.TracingDataFileDirectory))
                    System.IO.Directory.CreateDirectory(Fask.MyPath.Path.TracingDataFileDirectory);

                string tmpPath = System.IO.Path.Combine(Fask.MyPath.Path.TracingDataFileDirectory, _filelog);
                

                sw = new System.IO.StreamWriter(tmpPath, true);
                //sw = new System.IO.StreamWriter(_dirlog, true);
                #region old txt format
                //sw.WriteLine(
                //    DateTime.Now.ToString("o") + 
                //    ": " + (context.Length != 0 ? "(" + context + ") " : context) 
                //    + ((h != null) ? h.ToString() : string.Empty)
                //    + "\r\n" + message); 
                #endregion

                #region new csv format
                sw.WriteLine(
                         DateTime.Now.ToString("o")
                         + "; " + h.Context.ToString()
                         + "; " + h.Identificator.ToString()
                         + "; " + h.TerminalId.ToString()
                         + "; " + h.UserId.ToString()
                         + "; " + h.Davka.ToString()
                         + "; " + message
                         ); 
                #endregion
            }
            catch(Exception ex)
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
    }
}
