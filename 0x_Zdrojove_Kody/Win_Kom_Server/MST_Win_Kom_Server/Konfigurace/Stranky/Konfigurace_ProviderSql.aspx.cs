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
    public partial class Konfigurace_ProviderSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Fask.ModuleSql.Globals.LoadConfiguration();
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
            this.CreateTableHelper(ConnectionString, Fask.ModuleSql.Globals.Konfigurace.ConnectionString[0]);
            this.CreateTableHelper(Inventura, Fask.ModuleSql.Globals.Konfigurace.Inventura[0]);
            this.CreateTableHelper(Inventura2, Fask.ModuleSql.Globals.Konfigurace.Inventura2[0]);
            this.CreateTableHelper(Prijem, Fask.ModuleSql.Globals.Konfigurace.Prijem[0]);
            this.CreateTableHelper(Vydej, Fask.ModuleSql.Globals.Konfigurace.Vydej[0]);
            this.CreateTableHelper(Prodej, Fask.ModuleSql.Globals.Konfigurace.Prodej[0]);
            this.CreateTableHelper(Servis, Fask.ModuleSql.Globals.Konfigurace.Servis[0]);
            this.CreateTableHelper(Informations, Fask.ModuleSql.Globals.Konfigurace.Informations[0]);
            this.CreateTableHelper(LokMech, Fask.ModuleSql.Globals.Konfigurace.LokMech[0]);

        }

        private void SaveTabulkyAll()
        {

            try
            {
                #region Objektove ukladani

                Fask.ModuleSql.Globals.LoadConfiguration();
                Classes.SaveTabulky st = new SaveTabulky();
                st.SaveTabulkyAll(this.Request.Form, Fask.ModuleSql.Globals.Konfigurace);
                Fask.ModuleSql.Globals.SaveConfiguration();
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