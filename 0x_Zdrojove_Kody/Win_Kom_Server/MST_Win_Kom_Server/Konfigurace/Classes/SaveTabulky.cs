using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Konfigurace.Classes
{
    public class SaveTabulky
    {
        private const string _fileName = "ZmenaKonfigurace.txt";

        public SaveTabulky()
        {

        }

        public void SaveTabulkyAll(System.Collections.Specialized.NameValueCollection Formular, DataSet Konfigurace)
        {
            try
            {
                //1. Načtu stavaciji konfiguraci
                //2. Cyklicky projdu každu tabulku
                //3. projit každy parametr v ni
                //4. najit či sa nachazi v requestu
                //pokud ano tak provest validaci a uložit zmenu


                Dictionary<string, string> FormResponse = new Dictionary<string, string>();

                foreach (string Key in Formular.Keys)
                {
                    //Debug.WriteLine(Key);

                    if (!Key.StartsWith("_ctl0:ContentPlaceHolder1:"))
                        continue;

                    string CorectID = Key.Replace("_ctl0:ContentPlaceHolder1:", "");

                    string[] arr = CorectID.Split('-');

                    if (arr.Length != 3)
                        continue;

                    string Typ = arr[0].Trim();
                    string Table = arr[1].Trim();
                    string Column = arr[2].Trim();

                    string[] val = Formular.GetValues(Key);

                    if (val.Length < 1)
                        continue;

                    FormResponse[Table + "." + Column] = val[0];
                }

                foreach (DataTable Tabulka in Konfigurace.Tables)
                {

                    string TableName = Tabulka.TableName;

                    foreach (DataColumn Stloupec in Tabulka.Columns)
                    {

                        string ColumnName = Stloupec.ColumnName;

                        Type TypObjektu = Konfigurace.Tables[TableName].Rows[0][ColumnName].GetType();

                        switch (TypObjektu.Name)
                        {
                            case "String":
                                try
                                {
                                    string val = string.Empty;
                                    try
                                    {
                                        val = FormResponse[TableName + "." + ColumnName];
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " Chyba je u FormResponse[" +TableName + "." + ColumnName + "]");
                                        throw ex;
                                    }

                                    //Tady je možnot zašifrovat ukladany řetezec
                                    if (Classes.Sifrovani.Sifruj)
                                    {
                                        //třeba...
                                        if (ColumnName.StartsWith(Classes.Sifrovani.Prefix))
                                        {
                                            Konfigurace.Tables[TableName].Rows[0][ColumnName] = Classes.Sifrovani.Sifruj_Do_Base64(val);
                                        }
                                        else 
                                        {
                                            Konfigurace.Tables[TableName].Rows[0][ColumnName] = val;
                                        }

                                    }
                                    else
                                    {
                                        Konfigurace.Tables[TableName].Rows[0][ColumnName] = val;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Konfigurace.Tables[TableName].Rows[0][ColumnName] = string.Empty;
                                    Logging.ExceptionHandler2.Handle(ex);
                                }
                                break;

                            case "Boolean":
                                try
                                {
                                    string val = string.Empty;
                                    try
                                    {
                                        val = FormResponse[TableName + "." + ColumnName];
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " Chyba je u FormResponse[" + TableName + "." + ColumnName + "]");
                                        throw ex;
                                    }

                                    Konfigurace.Tables[TableName].Rows[0][ColumnName] = val == "on" ? true : false;
                                }
                                catch (Exception ex)
                                {
                                    Konfigurace.Tables[TableName].Rows[0][ColumnName] = false;
                                    Logging.ExceptionHandler2.Handle(ex);
                                }
                                break;
                            case "Int32":
                                try
                                {
                                    string val = string.Empty;
                                    try
                                    {
                                        val = FormResponse[TableName + "." + ColumnName];
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " Chyba je u FormResponse[" + TableName + "." + ColumnName + "]");
                                        throw ex;
                                    }
                                    Konfigurace.Tables[TableName].Rows[0][ColumnName] = Int32.Parse(val);
                                }
                                catch (Exception ex)
                                {
                                    Konfigurace.Tables[TableName].Rows[0][ColumnName] = string.Empty;
                                    Logging.ExceptionHandler2.Handle(ex);
                                }
                                break;
                            default:
                                Konfigurace.Tables[TableName].Rows[0][ColumnName] = string.Empty;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Restart(string Kdo)
        {
            SaveToFile(Kdo);
        }

        private bool SaveToFile(string MSG)
        {

            System.IO.StreamWriter sw = null;
            try
            {
                string logpath = Path.Combine(MyPath.Path.BinDirectory, _fileName);

                string dirpath = System.IO.Path.GetDirectoryName(logpath);
                if (!System.IO.Directory.Exists(dirpath))
                    System.IO.Directory.CreateDirectory(dirpath);


                if (logpath == null || logpath == string.Empty)
                    return true;

                sw = new StreamWriter(logpath, true);
                sw.WriteLine(DateTime.Now.ToString("G") + " : " + MSG.Trim());
                sw.Close();
                sw = null;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
            }

        }
    }
}