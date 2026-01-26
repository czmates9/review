using Fask.MST_W_Server.Konfigurace.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Fask.Module.ABRA.SAB;

namespace Fask.MST_W_Server.Konfigurace.Stranky
{
    public partial class Konfigurace_ProviderABRA_SAB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Globals_V1.LoadConfiguration();
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

            //Fask.ModuleSql.Globals.LoadConfiguration();
            //CreateTabulkyAll();

            //Server.Transfer(@"..\..\Default.aspx"); // Tady byla chyba s načitavanim css a js
            Response.Redirect(@"..\..\Default.aspx");
        }


        private void CreateTabulkyAll()
        {
            this.CreateTableHelper(ConnectionString, Globals_V1.Konfigurace.ConnectionStrings[0]);
            this.CreateTableHelper(WEBAPI, Globals_V1.Konfigurace.WEBAPI[0]);
            this.CreateTableHelper(ID_Rady, Globals_V1.Konfigurace.ID_Rady[0]);
            this.CreateTableHelper(ProcesniRizeni, Globals_V1.Konfigurace.Procesni_Rizeni[0]);
            this.CreateTableHelper(Vydej, Globals_V1.Konfigurace.Vydej[0]);
            this.CreateTableHelper(Inventura, Globals_V1.Konfigurace.Inventura1[0]);
            this.CreateTableHelper(Prijem, Globals_V1.Konfigurace.Prijem[0]);
            this.CreateTableHelper(Prodej, Globals_V1.Konfigurace.Prodej[0]);
            this.CreateTableHelper(ExportZasoby, Globals_V1.Konfigurace.ExportZasoby[0]);
            
            
        }

        private void SaveTabulkyAll()
        {

            try
            {
                #region Objektove ukladani

                Globals_V1.LoadConfiguration();
                Classes.SaveTabulky st = new SaveTabulky();
                st.SaveTabulkyAll(this.Request.Form, Globals_V1.Konfigurace);
                Globals_V1.SaveConfiguration();
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
                //        Fask.ModuleSql.Globals.Konfigurace.Tables[Table].Rows[0][Column] = val[0] == "on" ? true : false;
                //    else if (Typ == "TextBox")
                //        Fask.ModuleSql.Globals.Konfigurace.Tables[Table].Rows[0][Column] = val[0];
                //    else if (Typ == "TextBoxInt32")
                //        Fask.ModuleSql.Globals.Konfigurace.Tables[Table].Rows[0][Column] = Int32.Parse(val[0]);

                //}

                //Fask.ModuleSql.Globals.SaveConfiguration(); 
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

                        //lok = (string)GetLocalResourceObject(column.ColumnName);
                        lok = a.Value;
                        Com = a.Comment;
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Nenastaven :" + tableName + "." + column.ColumnName);
                    }
                    t.CreateTableFromDTable(column, lok, Com, Radek);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}