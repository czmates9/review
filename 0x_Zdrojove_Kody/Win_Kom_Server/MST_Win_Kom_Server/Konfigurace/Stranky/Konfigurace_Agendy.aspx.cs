using Fask.MST_W_Server.Konfigurace.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server.Konfigurace.Stranky
{
    public partial class Konfigurace_Agendy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Classes.Globals_Konfig_Agendy.LoadConfiguration();

                    CreateTabulkyAll();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void Button_Save_Click(object sender, EventArgs e)
        {
            SaveTabulkyAll();

            //Classes.Globals_Konfig_Agendy.LoadConfiguration();
            //CreateTabulkyAll();

            //Server.Transfer(@"..\..\Default.aspx"); // Tady byla chyba s načitavanim css a js
            Response.Redirect(@"..\..\Default.aspx");
        }

        private void CreateTabulkyAll()
        {
            this.CreateTableHelper(Inventura1Parametry, Classes.Globals_Konfig_Agendy.Konfigurace.Inventura1Parametry[0]);
            this.CreateTableHelper(Inventura2Parametry, Classes.Globals_Konfig_Agendy.Konfigurace.Inventura2Parametry[0]);
            this.CreateTableHelper(PrijemParametry, Classes.Globals_Konfig_Agendy.Konfigurace.PrijemParametry[0]);
            this.CreateTableHelper(VydejParametry, Classes.Globals_Konfig_Agendy.Konfigurace.VydejParametry[0]);
            this.CreateTableHelper(ServisParametry, Classes.Globals_Konfig_Agendy.Konfigurace.ServisParametry[0]);

        }

        private void CreateTableHelper(Table t, System.Data.DataRow Radek)
        {

            try
            {
                string tableName = Radek.Table.TableName;

                //1. Vytvořit tabulku pomoci foreach a naplnit datama
                foreach (System.Data.DataColumn column in Radek.Table.Columns)
                {
                    string lok = string.Empty;
                    string Com = string.Empty;
                    try
                    {
                        var a = this.GetLocalResource_Value_Comment(tableName + "." + column.ColumnName);

                        lok = a.Value;
                        Com = a.Comment;
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    t.CreateTableFromDTable(column, lok, Com, Radek);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveTabulkyAll()
        {

            try
            {

                #region Objektove ukladani

                Classes.Globals_Konfig_Agendy.LoadConfiguration();
                Classes.SaveTabulky st = new SaveTabulky();
                st.SaveTabulkyAll(this.Request.Form, Classes.Globals_Konfig_Agendy.Konfigurace);
                Classes.Globals_Konfig_Agendy.SaveConfiguration();
                st.Restart(this.GetType().ToString());
                #endregion

                #region Puvodne
                //foreach (string Key in this.Request.Form.Keys)
                //{
                //    Debug.WriteLine(Key);

                //    if (!Key.StartsWith("_ctl0:ContentPlaceHolder1:"))
                //        continue;

                //    string CorectID = Key.Replace("_ctl0:ContentPlaceHolder1:", "");

                //    string[] arr = CorectID.Split('-');

                //    if (arr.Length != 3)
                //        continue;

                //    string Typ = arr[0].Trim();
                //    string Table = arr[1].Trim();
                //    string Column = arr[2].Trim();

                //    string[] val = this.Request.Form.GetValues(Key);

                //    if (val.Length < 1)
                //        continue;

                //    if (Typ == "CheckBox")
                //        Classes.Globals_Konfig_Agendy.Konfigurace.Tables[Table].Rows[0][Column] = val[0] == "on" ? true : false;
                //    else if (Typ == "TextBox")
                //        Classes.Globals_Konfig_Agendy.Konfigurace.Tables[Table].Rows[0][Column] = val[0];
                //    else if (Typ == "TextBoxInt32")
                //        Classes.Globals_Konfig_Agendy.Konfigurace.Tables[Table].Rows[0][Column] = Int32.Parse(val[0]);

                //}

                //Classes.Globals_Konfig_Agendy.SaveConfiguration(); 
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}