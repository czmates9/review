using Fask.MST_W_Server.Konfigurace.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server.Konfigurace.Stranky
{
    public partial class Konfigurace_WebConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

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

            //Classes.Globals_Konfig_WebConfig.LoadConfiguration();
            //CreateTabulkyAll();



            //Server.Transfer(@"..\..\Default.aspx"); // Tady byla chyba s načitavanim css a js
            Response.Redirect(@"..\..\Default.aspx");
        }

        private void CreateTabulkyAll()
        {
            this.CreateTableHelper(ConnectionString, Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0]);
            this.CreateTableHelper(Providers, Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0]);
            this.CreateTableHelper(Logs, Classes.Globals_Konfig_WebConfig.Konfigurace.Logs[0]);
            this.CreateTableHelper(Directories, Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0]);
            this.CreateTableHelper(PaletovyListek, Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0]);
            this.CreateTableHelper(Licenses, Classes.Globals_Konfig_WebConfig.Konfigurace.Licenses[0]);
            this.CreateTableHelper(Image, Classes.Globals_Konfig_WebConfig.Konfigurace.Image[0]);
            this.CreateTableHelper(Vyroba, Classes.Globals_Konfig_WebConfig.Konfigurace.Vyroba[0]);

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

                Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                Classes.SaveTabulky st = new SaveTabulky();
                st.SaveTabulkyAll(this.Request.Form, Classes.Globals_Konfig_WebConfig.Konfigurace);
                Classes.Globals_Konfig_WebConfig.SaveConfiguration();
                st.Restart(this.GetType().ToString());

                #endregion

                #region Nove ukladani

                ////1. Načtu stavaciji konfiguraci
                ////2. Cyklicky projdu každu tabulku
                ////3. projit každy parametr v ni


                ////4. najit či sa nachazi v requestu
                ////pokud ano tak provest validaci a uložit zmenu


                //Dictionary<string, string> FormResponse = new Dictionary<string, string>();

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

                //    FormResponse[Table + "." + Column] = val[0];
                //}

                //Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

                //foreach (DataTable Tabulka in Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Tables)
                //{

                //    string TableName = Tabulka.TableName;

                //    foreach (DataColumn Stloupec in Tabulka.Columns)
                //    {

                //        string ColumnName = Stloupec.ColumnName;

                //        Type TypObjektu = Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName].GetType();

                //        switch (TypObjektu.Name)
                //        {
                //            case "String":
                //                try
                //                {
                //                    string val = FormResponse[TableName + "." + ColumnName];
                //                    Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = val;
                //                }
                //                catch (Exception ex)
                //                {
                //                    Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = string.Empty;
                //                }
                //                break;

                //            case "Boolean":
                //                try
                //                {
                //                    string val = FormResponse[TableName + "." + ColumnName];
                //                    Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = val == "on" ? true : false;
                //                }
                //                catch (Exception ex)
                //                {
                //                    Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = false;
                //                }
                //                break;
                //            case "Int32":
                //                try
                //                {
                //                    string val = FormResponse[TableName + "." + ColumnName];
                //                    Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = Int32.Parse(val);
                //                }
                //                catch (Exception ex)
                //                {
                //                    Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = string.Empty;
                //                }
                //                break;
                //            default:
                //                Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[TableName].Rows[0][ColumnName] = string.Empty;
                //                break;
                //        }
                //    }
                //}

                //Classes.Globals_Konfig_WebConfig.SaveConfiguration(); 
                #endregion

                #region Puvodne ukladani
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
                //        Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[Table].Rows[0][Column] = val[0] == "on" ? true : false;
                //    else if (Typ == "TextBox")
                //        Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[Table].Rows[0][Column] = val[0];
                //    else if (Typ == "TextBoxInt32")
                //        Classes.Globals_Konfig_WebConfig.Konfigurace.Tables[Table].Rows[0][Column] = Int32.Parse(val[0]);

                //}

                //Classes.Globals_Konfig_WebConfig.SaveConfiguration(); 
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}