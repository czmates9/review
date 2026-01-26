using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;
using System.Data.SqlClient;
using Fask.ModulePohodaXML.Pohoda_DataSets;
using Fask.Interfaces.DataSets;

namespace Fask.ModulePohodaXML.Database
{
    class Pohoda
    {


        #region Pro každou tabulku ručne definovane dotazy
        // Uprava z duvodu měneni struktur pod rukama ze strany Pohody se prešlo k tomuto spusobu komunikace s DB

        #region Fill UNIVERSAL

        private static void Fill_Universal(System.Data.DataTable dt, string SQL)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                using (var con = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB))
                {
                    using (var com = con.CreateCommand())
                    {
                        com.CommandText = SQL;
                        com.CommandType = System.Data.CommandType.Text;

                        using (var ada = new System.Data.OleDb.OleDbDataAdapter())
                        {
                            ada.SelectCommand = com;
                            ada.Fill(dt);

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        #endregion


        #region Template


        //public static DatabasePohoda.XXX_DataTable XXX_GetData(int? RefSKz, string Cislo)
        //{
        //    DatabasePohoda.XXX_DataTable dataTable = new DatabasePohoda.XXX_DataTable();
        //    XXX_Fill(dataTable, RefSKz, Cislo);
        //    return dataTable;
        //}

        //public static void XXX_Fill(DatabasePohoda.XXX_DataTable dataTable, string XXX)
        //{

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    try
        //    {
        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = @"";
        //        da.SelectCommand.Parameters.AddWithValue("?", XXX);

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

        //        da.Fill(dataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.write("Pohoda Tabulky Definice SQL Dotazu", " XXX_Fill", ex.Message);
        //        Log.Write(dataTable);
        //    }

        //}



        #endregion

        #region OBJpol

        #region OBJPol Get/Fill  DataBy_RefAg_RefSKz

        /// <summary>
        /// Get Data z DB Pohoda z tabulky OBJPol where Cislo a RefSKz
        /// </summary>
        /// <param name="RefSKz">Reference na stav skladu</param>
        /// <param name="Cislo">SOPNUMBE číslo objednavky</param>
        /// <returns>Tabulka OBJPol</returns>
        public static  Pohoda_DataSets.DatabasePohoda.OBJpolDataTable OBJPol_GetDataBy_RefAg_RefSKz(int? RefSKz, string Cislo)
        {
            DatabasePohoda.OBJpolDataTable dataTable = new DatabasePohoda.OBJpolDataTable();
            OBJPol_FillBy_RefAg_RefSKz(dataTable, RefSKz, Cislo);
            return dataTable;
        }

        /// <summary>
        /// Fill Data z DB Pohoda z tabulky OBJPol where Cislo a RefSKz
        /// </summary>
        /// <param name="dataTable">Tabulka OBJPol</param>
        /// <param name="RefSKz">Reference na stav skladu</param>
        /// <param name="Cislo">SOPNUMBE číslo objednavky</param>
        public static void OBJPol_FillBy_RefAg_RefSKz(DatabasePohoda.OBJpolDataTable dataTable, int? RefSKz, string Cislo)
        {
            //this._commandCollection[1].CommandText = @"SELECT p.ID, p.RefAg, p.RefSKz, p.RefSKz0, p.RefPol, p.RelAgID, p.SText, p.Pozn, p.Kod, p.VCislo, p.SKzVC, p.Mnozstvi, p.Dodano, p.DodBefor, p.MJ, p.MJKoef, p.KcJedn, p.Sleva, p.RelSzDPH, p.ProcentoDPH, p.SDph, p.Kc, p.KcDPH, p.CmJedn, p.Cm, p.CmDPH, p.RelPk, p.PDP, p.MOSSDruh, p.RelTypPolEET, p.DICPover, p.RefStr, p.RefCin, p.CisloZAK, p.DatCreate, p.DatSave, p.OrderFld FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                //da.SelectCommand.CommandTimeout
                da.SelectCommand.CommandText = @"SELECT p.ID, p.Mnozstvi, p.Dodano, p.KcJedn, p.RelSzDPH, p.ProcentoDPH, p.CmJedn FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", RefSKz);
                da.SelectCommand.Parameters.AddWithValue("?", Cislo);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_FillBy_RefAg_RefSKz", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region OBJPol Get/Fill ByRefAg

        /// <summary>
        /// Get Data z DB Pohoda z tabulky OBJPol where RefAg
        /// </summary>
        /// <param name="RefAg">Reference na OBJ</param>
        /// <returns>Tabulka OBJPol</returns>
        public static DatabasePohoda.OBJpolDataTable OBJPol_GetDataByRefAg(int? RefAg)
        {
            DatabasePohoda.OBJpolDataTable dataTable = new DatabasePohoda.OBJpolDataTable();
            OBJPol_FillByRefAg(dataTable, RefAg);
            return dataTable;
        }

        /// <summary>
        /// Fill Data z DB Pohoda z tabulky OBJPol where RefAg 
        /// </summary>
        /// <param name="dataTable">Tabulka OBJPol</param>
        /// <param name="RefAg">Reference na OBJ</param>
        public static void OBJPol_FillByRefAg(DatabasePohoda.OBJpolDataTable dataTable, int? RefAg)
        {
            //this._commandCollection[1].CommandText = @"SELECT p.ID, p.RefAg, p.RefSKz, p.RefSKz0, p.RefPol, p.RelAgID, p.SText, p.Pozn, p.Kod, p.VCislo, p.SKzVC, p.Mnozstvi, p.Dodano, p.DodBefor, p.MJ, p.MJKoef, p.KcJedn, p.Sleva, p.RelSzDPH, p.ProcentoDPH, p.SDph, p.Kc, p.KcDPH, p.CmJedn, p.Cm, p.CmDPH, p.RelPk, p.PDP, p.MOSSDruh, p.RelTypPolEET, p.DICPover, p.RefStr, p.RefCin, p.CisloZAK, p.DatCreate, p.DatSave, p.OrderFld FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                da.SelectCommand.CommandText = @"SELECT ID, RefSKz, Mnozstvi, Dodano, MJ, MJKoef, KcJedn, Sleva, RelSzDPH,  SDph, CmJedn, RefStr, RefCin, CisloZAK FROM dbo.OBJpol WHERE (RefAg = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", RefAg);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_FillByRefAg", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pohodaDS"></param>
        public static int OBJPol_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

            da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
            da.UpdateCommand.CommandType = System.Data.CommandType.Text;


            da.UpdateCommand.Connection = connection;
            da.UpdateCommand.Transaction = Trans;
            //da.UpdateCommand.CommandTimeout
            da.UpdateCommand.CommandText = @"UPDATE OBJpol" +
                " SET " +
                //" RefSKz = ? ," +
                //" Mnozstvi = ? ," +
                //" Dodano = ? ," +
                //" MJ = ? ," +
                //" MJKoef = ? ," +
                //" KcJedn = ? ," +
                //" Sleva = ? ," +
                //" RelSzDPH = ? ," +
                //" SDph = ? ," +
                //" CmJedn = ? ," +
                //" RefStr = ? ," +
                //" RefCin = ? ," +
                //" CisloZAK = ? ," +
                //" ProcentoDPH = ? " +
                //" WHERE (ID = ?)";
                " Dodano = ? " +
                " WHERE (ID = ?)";

            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefSKz", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Mnozstvi", System.Data.OleDb.OleDbType.Double));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Dodano", System.Data.OleDb.OleDbType.Double));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("MJ", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("MJKoef", System.Data.OleDb.OleDbType.Double));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("KcJedn", System.Data.OleDb.OleDbType.Currency));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Sleva", System.Data.OleDb.OleDbType.Double));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RelSzDPH", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("SDph", System.Data.OleDb.OleDbType.Binary));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CmJedn", System.Data.OleDb.OleDbType.Currency));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefStr", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefCin", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CisloZAK", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ProcentoDPH", System.Data.OleDb.OleDbType.Double));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));			

            //da.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Dodano", global::System.Data.OleDb.OleDbType.Double, 8, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Dodano", global::System.Data.DataRowVersion.Current, false, null));
            //da.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ID", global::System.Data.OleDb.OleDbType.Integer, 4, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ID", global::System.Data.DataRowVersion.Original, false, null));

            var pDodano = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Dodano", System.Data.OleDb.OleDbType.Double));
            pDodano.SourceColumn = "Dodano";

            var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
            pID.SourceColumn = "ID";
            pID.SourceVersion = System.Data.DataRowVersion.Original;

            int result = 0;
            try
            {
                result = da.Update(pohodaDS.OBJpol);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_Update", ex);
                //Log.Write(dataTable);
            }
            return result;
        }

        #endregion

        #region OBJPol_Update_VPrQTY Vyroba Zaplanovani

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pohodaDS"></param>
        public static int OBJPol_Update_VPrQTY(decimal? QTY, int ORD_OBJpol )
        {

            Globals_V1.LoadConfiguration();

                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {
                da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
                da.UpdateCommand.CommandType = System.Data.CommandType.Text;
                //da.UpdateCommand.CommandTimeout
                da.UpdateCommand.CommandText = @"UPDATE OBJpol" +
                " SET " +
                " VPrQTY = ? " +
                " WHERE (ID = ?)";

                da.UpdateCommand.Parameters.AddWithValue("?", QTY);
                da.UpdateCommand.Parameters.AddWithValue("?", ORD_OBJpol);


                da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                da.UpdateCommand.Connection.Open();
                result = da.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_Update_VPrQTY", ex);
                //Log.Write(dataTable);
            }
            finally
            {
                da.UpdateCommand.Connection.Close();
            }

            return result;

        }

        #endregion

        #region Update Vyroba Zaplanovani

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pohodaDS"></param>
        public static int OBJPol_Update_RefVPrPVF(int? RefVPrPVF, int ORD_OBJpol)
        {

            Globals_V1.LoadConfiguration();

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {
                da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
                da.UpdateCommand.CommandType = System.Data.CommandType.Text;
                //da.UpdateCommand.CommandTimeout
                da.UpdateCommand.CommandText = @"UPDATE OBJpol" +
                " SET " +
                " RefVPrPVF = ? " +
                " WHERE (ID = ?)";

                da.UpdateCommand.Parameters.AddWithValue("?", RefVPrPVF);
                da.UpdateCommand.Parameters.AddWithValue("?", ORD_OBJpol);


                da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                da.UpdateCommand.Connection.Open();
                result = da.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_Update_RefVPrPVF", ex);
                //Log.Write(dataTable);
            }
            finally
            {
                da.UpdateCommand.Connection.Close();
            }

            return result;

        }

        #endregion


        #region OBJPol_Get_VPrQTY
        public static decimal? OBJPol_Get_VPrQTY(int ORD_OBJpol)
        {
            decimal? tmp = null;
            try
            {

                Globals_V1.LoadConfiguration();
                //System.Data.OleDb.OleDbConnection SQLCON = 
                //SQLCON.Open();

                System.Data.OleDb.OleDbCommand SQLCommand = new System.Data.OleDb.OleDbCommand();
                SQLCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                SQLCommand.CommandType = System.Data.CommandType.Text;

                SQLCommand.CommandText = "SELECT VPrQTY FROM OBJpol WHERE (ID = ?)";
                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.Integer).Value = ORD_OBJpol;

                SQLCommand.Connection.Open();

                object result = SQLCommand.ExecuteScalar();

                SQLCommand.Connection.Close();

                if ((result != null) && (result is double))
                {
                    tmp = Convert.ToDecimal((double)result);

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_Get_VPrQTY", ex);
            }

            return tmp;
        }
        
        #endregion


        #endregion

        #region OBJ

        /// <summary>
        /// Metoda sloužici pro Update uživatele ktery provedl zaznam
        /// </summary>
        /// <param name="Creator">Tvořitel</param>
        /// <param name="Original_Cislo">SOPNUMBE číslo objednavky</param>
        /// <returns></returns>
        public static int OBJ_UpdateCreatorByCislo(string Creator, string Original_Cislo)
        {
            //this._commandCollection[3].CommandText = "UPDATE       OBJ\r\nSET                Creator = ?\r\nWHERE        (Cislo = ?)";

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {
                da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
                da.UpdateCommand.CommandType = System.Data.CommandType.Text;
                //da.UpdateCommand.CommandTimeout
                da.UpdateCommand.CommandText = @"UPDATE OBJ SET Creator = ? WHERE (Cislo = ?)";

                da.UpdateCommand.Parameters.AddWithValue("?", Creator);
                da.UpdateCommand.Parameters.AddWithValue("?", Original_Cislo);


                da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                da.UpdateCommand.Connection.Open();
                result = da.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJ_UpdateCreatorByCislo", ex);
                //Log.Write(dataTable);
            }
            finally
            {
                da.UpdateCommand.Connection.Close();
            }
            return result;

        }

        /// <summary>
        /// Metoda pro vraceni dat z OBJ
        /// </summary>
        /// <param name="Cislo">SOPNUMBE číslo objednavky</param>
        /// <returns></returns>
        public static DatabasePohoda.OBJDataTable OBJ_GetDataByCislo(string Cislo)
        {
            DatabasePohoda.OBJDataTable dataTable = new DatabasePohoda.OBJDataTable();
            OBJ_FillByCislo(dataTable, Cislo);
            return dataTable;
        }

        /// <summary>
        /// Metoda pro vraceni dat z OBJ 
        /// </summary>
        /// <param name="dataTable">Tabulka OBJ pro naplneni</param>
        /// <param name="Cislo">SOPNUMBE číslo objednavky</param>
        /// <returns>počet nalezenych radku</returns>
        public static int OBJ_FillByCislo(DatabasePohoda.OBJDataTable dataTable, string Cislo)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int response = 0;
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                //da.SelectCommand.CommandTimeout
                da.SelectCommand.CommandText = @"SELECT ID, RefCin, RefStr, CisloZAK, SText, HistSzDPH,RefCM, CmKurs, Vyrizeno, BDodano, TrvalyDok, RefAD, Firma, Utvar, Jmeno, Ulice, PSC, Obec, ICO, DIC, ICDPH, Email, Firma2, Utvar2, Jmeno2, Ulice2, PSC2, Obec2, Email2, Fax, Pozn, Pozn2, DICRegDPHEU FROM dbo.OBJ WHERE (Cislo = ?)";

                da.SelectCommand.Parameters.AddWithValue("?", Cislo);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                response = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJ_FillByCislo", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }
            return response;
        }

        /// <summary>
        /// Update Tabulky OBJ
        /// </summary>
        /// <param name="pohodaDS"></param>
        public static int OBJ_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

            da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
            //da.UpdateCommand.Transaction = Trans;
            da.UpdateCommand.CommandType = System.Data.CommandType.Text;
            da.UpdateCommand.Connection = connection;
            da.UpdateCommand.Transaction = Trans;
            //da.UpdateCommand.CommandTimeout
            da.UpdateCommand.CommandText = @"UPDATE OBJ" +
                " SET " +
                //" RefCin = ? ," +
                //" RefStr = ? ," +
                //" CisloZAK = ? ," +
                //" SText = ? ," +
                //" HistSzDPH = ? ," +
                //" RefCM = ? ," +
                //" CmKurs = ? ," +
                //" Vyrizeno = ? ," +
                //" BDodano = ? ," +
                //" TrvalyDok = ? ," +
                //" RefAD = ? ," +
                //" Firma = ? ," +
                //" Utvar = ? ," +
                //" Jmeno = ? ," +
                //" Ulice = ? ," +
                //" PSC = ? ," +
                //" Obec = ? ," +
                //" ICO = ? ," +
                //" DIC = ? ," +
                //" ICDPH = ? ," +
                //" Email = ? ," +
                //" Firma2 = ? ," +
                //" Utvar2 = ? ," +
                //" Jmeno2 = ? ," +
                //" PSC2 = ? ," +
                //" Obec2 = ? ," +
                //" Email2 = ? ," +
                //" Fax = ? ," +
                //" Pozn = ? ," +
                //" Pozn2 = ? ," +
                //" DICRegDPHEU = ? " +
                //" WHERE (ID = ?)";

                " Vyrizeno = ? ," +
                " BDodano = ? " +
                " WHERE (ID = ?)";

            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefCin", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefStr", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CisloZAK", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("SText", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("HistSzDPH", System.Data.OleDb.OleDbType.Binary));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefCM", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CmKurs", System.Data.OleDb.OleDbType.Double));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Vyrizeno", System.Data.OleDb.OleDbType.Binary));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("BDodano", System.Data.OleDb.OleDbType.Binary));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("TrvalyDok", System.Data.OleDb.OleDbType.Binary));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefAD", System.Data.OleDb.OleDbType.Integer));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Firma", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Jmeno", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Ulice", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("PSC", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Obec", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ICO", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIC", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ICDPH", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Email", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Firma2", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Utvar2", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Jmeno2", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("PSC2", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Obec2", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Email2", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Fax", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Pozn", System.Data.OleDb.OleDbType.LongVarWChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Pozn2", System.Data.OleDb.OleDbType.LongVarWChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("DICRegDPHEU", System.Data.OleDb.OleDbType.VarChar));
            //da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));


            var pVyrizeno = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Vyrizeno", System.Data.OleDb.OleDbType.Boolean));
            pVyrizeno.SourceColumn = "Vyrizeno";

            var pBDodano = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("BDodano", System.Data.OleDb.OleDbType.Boolean));
            pBDodano.SourceColumn = "BDodano";

            var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
            pID.SourceColumn = "ID";
            pID.SourceVersion = System.Data.DataRowVersion.Original;

            int result = 0;
            try
            {
                result = da.Update(pohodaDS.OBJ);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJ_Update", ex);
                Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
            }
            return result;
        }

        #endregion

        #region sCRady

        //public virtual DatabasePohoda.sCRadyDataTable sCRady_GetDataBy_RokDokladObsahtextu(global::System.Nullable<int> Rok, global::System.Nullable<int> RelCrAg, string SText) {
        //public virtual int sCRady_FillBy_RokDokladObsahtextu(DatabasePohoda.sCRadyDataTable dataTable, global::System.Nullable<int> Rok, global::System.Nullable<int> RelCrAg, string SText) {


        public static DatabasePohoda.sCRadyDataTable sCRady_GetDataBy_RokDokladObsahtextu(int? Rok, int? RelCrAg, string SText)
        {
            DatabasePohoda.sCRadyDataTable dataTable = new DatabasePohoda.sCRadyDataTable();
            sCRady_FillBy_RokDokladObsahtextu(dataTable, Rok, RelCrAg, SText);
            return dataTable;
        }


        public static int sCRady_FillBy_RokDokladObsahtextu(DatabasePohoda.sCRadyDataTable dataTable, int? Rok, int? RelCrAg, string SText)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                //da.SelectCommand.CommandTimeout
                da.SelectCommand.CommandText = @"SELECT ID, IDS FROM sCRady" +
                    " WHERE (Rok = ?) AND (RelCrAg = ?) AND (SText LIKE ?)";

                da.SelectCommand.Parameters.AddWithValue("?", Rok);
                da.SelectCommand.Parameters.AddWithValue("?", RelCrAg);
                da.SelectCommand.Parameters.AddWithValue("?", SText);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                result = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCRady_FillBy_RokDokladObsahtextu", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }
            return result;
        }

        #endregion

        #region sPrelom

        public static DatabasePohoda.sPrelomDataTable sPrelom_GetData(string UsIDS)
        {
            DatabasePohoda.sPrelomDataTable dataTable = new DatabasePohoda.sPrelomDataTable();
            sPrelom_Fill(dataTable, UsIDS);
            return dataTable;
        }

        public static int sPrelom_Fill(DatabasePohoda.sPrelomDataTable dataTable, string UsIDS)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                //da.SelectCommand.CommandTimeout
                da.SelectCommand.CommandText = @"SELECT ID, UsIDS, IsPrelom, UsRok FROM sPrelom" +
                    " WHERE (UsIDS = ?)";

                da.SelectCommand.Parameters.AddWithValue("?", UsIDS);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                result = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sPrelom_Fill", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }
            return result;
        }

        #endregion

        #region OBJCislo

        //public virtual int Fill(DatabasePohoda.OBJCisloDataTable dataTable, string Cislo)

        //public static int OBJCislo_Fill(DatabasePohoda.OBJCisloDataTable dataTable, string Cislo)
        //{
        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    int result = 0;
        //    try
        //    {

        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        string Command = string.Empty;

        //        Command += @"SELECT OBJ.Cislo AS OBJ_Cislo, SKz.ID AS SKz_ID, SKz.Nazev AS SKz_Nazev, SKz.EAN AS SKz_EAN, SKz.IDS AS SKz_IDS, SKz.RefSklad AS SKz_RefSklad, OBJpol.Mnozstvi AS OBJ_Mnozstvi, OBJpol.MJ AS OBJPol_MJ,OBJpol.MJKoef AS OBJpol_MJKoef, SKz.RelSKzVC AS SKz_RelSKzVC, OBJpol.ID AS OBJpol_ID, SKz.SText AS SKz_SText ";

        //        if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
        //        {
        //            Command += ", SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS ";
        //        }

        //        Command += " FROM dbo.OBJ LEFT OUTER JOIN";
        //        Command += " dbo.OBJpol ON OBJpol.RefAg = OBJ.ID LEFT OUTER JOIN";
        //        Command += " dbo.SKz ON SKz.ID = OBJpol.RefSKz";
        //        Command += " WHERE (OBJ.Cislo = ?)";

        //        da.SelectCommand.CommandText = Command;

        //        #region Original command
        //        //            SELECT        OBJ.Cislo AS OBJ_Cislo, SKz.ID AS SKz_ID, SKz.Nazev AS SKz_Nazev, SKz.EAN AS SKz_EAN, SKz.IDS AS SKz_IDS, SKz.RefSklad AS SKz_RefSklad, OBJpol.Mnozstvi AS OBJ_Mnozstvi, OBJpol.MJ AS OBJPol_MJ, 
        //        //                         OBJpol.MJKoef AS OBJpol_MJKoef, SKz.RelSKzVC AS SKz_RelSKzVC, OBJpol.ID AS OBJpol_ID, SKz.SText AS SKz_SText, SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, 
        //        //                         SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, 
        //        //                         SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS
        //        //FROM            dbo.OBJ LEFT OUTER JOIN
        //        //                         dbo.OBJpol ON OBJpol.RefAg = OBJ.ID LEFT OUTER JOIN
        //        //                         dbo.SKz ON SKz.ID = OBJpol.RefSKz
        //        //WHERE        (OBJ.Cislo = ?) 
        //        #endregion

        //        da.SelectCommand.Parameters.AddWithValue("?", Cislo);

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

        //        result = da.Fill(dataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " OBJCislo_Fill", ex.Message);
        //        Log.Write(dataTable);
        //    }

        //    return result;
        //}


        #endregion

        #region SKzParametry


        #region by ID
        public static DatabasePohoda.SKzParametryDataTable SKzParametry_GetDataByParamNameSKzID(string IDS, int? ID)
        {
            DatabasePohoda.SKzParametryDataTable dataTable = new DatabasePohoda.SKzParametryDataTable();
            SKzParametry_FillByParamNameSKzID(dataTable, IDS, ID);
            return dataTable;
        }

        public static int SKzParametry_FillByParamNameSKzID(DatabasePohoda.SKzParametryDataTable dataTable, string IDS, int? ID)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {

                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT skz.ID AS zID, skz.IDS AS zIDS, srp.ValText AS pValText, sp.ID AS pID, " +
                    "sp.IDS AS pIDS, sp.SText AS pSText, sp.Delka AS pDelka, sp.RelTyp AS pRelTyp " +
                    "FROM ((SKz skz LEFT OUTER JOIN SkRefParam srp ON srp.RefAg = skz.ID) LEFT OUTER JOIN SkParam sp ON sp.ID = srp.RefParam) " +
                    "WHERE (sp.IDS = ?) AND (skz.ID = ?)";


                da.SelectCommand.Parameters.AddWithValue("?", IDS);
                da.SelectCommand.Parameters.AddWithValue("?", ID);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                result = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzParametry_FillByParamNameSKzID", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

            return result;
        }
        
        #endregion


        #endregion

        #region SKPPpol

        //public virtual int FillByRefAg(DatabasePohoda.SKPPpolDataTable dataTable, global::System.Nullable<int> RefAg) {

        public static int SKPPpol_FillByRefAg(DatabasePohoda.SKPPpolDataTable dataTable, int? RefAg)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {

                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT ID, RefPol, RelAgID, Mnozstvi, RefSKz FROM SKPPpol where RefAg=?";


                da.SelectCommand.Parameters.AddWithValue("?", RefAg);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                result = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPPpol_FillByRefAg", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

            return result;
        }

        public static int SKPPpol_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

            da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
            da.UpdateCommand.CommandType = System.Data.CommandType.Text;
            da.UpdateCommand.Connection = connection;
            da.UpdateCommand.Transaction = Trans;
            da.UpdateCommand.CommandText = @"UPDATE SKPPpol" +
                " SET " +
                " RefPol = ? ," +
                " RelAgID = ? " +
                " WHERE (ID = ?)";


            var pRefPol = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefPol", System.Data.OleDb.OleDbType.Integer));
            pRefPol.SourceColumn = "RefPol";

            var pRelAgID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RelAgID", System.Data.OleDb.OleDbType.Integer));
            pRelAgID.SourceColumn = "RelAgID";

            var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
            pID.SourceColumn = "ID";
            pID.SourceVersion = System.Data.DataRowVersion.Original;

            int result = 0;
            try
            {
                result = da.Update(pohodaDS.SKPPpol);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPPpol_Update", ex);
                Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
            }
            return result;
        }

        #endregion

        #region SKzBuf

        public static int SKzBuf_FillByRefSkz(DatabasePohoda.SKzBufDataTable dataTable, int? RefSKz)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {

                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT ID, RefSKz, ObjedP, ObjedV FROM SKzBuf WHERE (RefSKz = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", RefSKz);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                result = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzBuf_FillByRefSkz", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

            return result;
        }

        public static int SKzBuf_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

            da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
            //da.UpdateCommand.Transaction = Trans;
            da.UpdateCommand.CommandType = System.Data.CommandType.Text;
            da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            da.UpdateCommand.Connection = connection;
            da.UpdateCommand.Transaction = Trans;
            da.UpdateCommand.CommandText = @"UPDATE SKzBuf" +
                " SET " +
                " RefSKz = ? ," +
                " ObjedP = ? ," +
                " ObjedV = ? " +
                " WHERE (ID = ?)";


            var pRefSKz = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefSKz", System.Data.OleDb.OleDbType.Integer));
            pRefSKz.SourceColumn = "RefSKz";

            var pObjedP = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedP", System.Data.OleDb.OleDbType.Double));
            pObjedP.SourceColumn = "ObjedP";

            var pObjedV = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedV", System.Data.OleDb.OleDbType.Double));
            pObjedV.SourceColumn = "ObjedV";

            var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
            pID.SourceColumn = "ID";
            pID.SourceVersion = System.Data.DataRowVersion.Original;

            int result = 0;
            try
            {
                result = da.Update(pohodaDS.SKzBuf);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzBuf_Update", ex);
                Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
            }
            return result;
        }

        #endregion

        #region SKzObjedP


        //public static int SKzObjedP_FillByID(DatabasePohoda.SKzObjedPDataTable dataTable, int ID)
        //{
        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    int result = 0;
        //    try
        //    {

        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = "SELECT ID, ObjedP FROM SKz WHERE (ID = ?)";


        //        da.SelectCommand.Parameters.AddWithValue("?", ID);

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

        //        result = da.Fill(dataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedP_FillByID", ex.Message);
        //        Log.Write(dataTable);
        //    }

        //    return result;
        //}

        //public static int SKzObjedP_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        //{
        //    if (connection == null)
        //        throw new Exception("Neni nastaven objekt connection");

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

        //    da.UpdateCommand = new System.Data.OleDb.OleDbCommand();

        //    da.UpdateCommand.CommandType = System.Data.CommandType.Text;
        //    da.UpdateCommand.Connection = connection;
        //    da.UpdateCommand.Transaction = Trans;
        //    da.UpdateCommand.CommandText = @"UPDATE SKz" +
        //        " SET " +
        //        " ObjedP = ? " +
        //        " WHERE (ID = ?)";


        //    var pObjedP = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedP", System.Data.OleDb.OleDbType.Double));
        //    pObjedP.SourceColumn = "ObjedP";

        //    var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
        //    pID.SourceColumn = "ID";
        //    pID.SourceVersion = System.Data.DataRowVersion.Original;

        //    int result = 0;
        //    try
        //    {
        //        result = da.Update(pohodaDS.SKz);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedP_Update", ex.Message);
        //        Log.Write(pohodaDS);
        //    }
        //    return result;
        //}



        #endregion

        #region SKzObjedV


        public static int SKzObjedV_FillByID(DatabasePohoda.SKzObjedVDataTable dataTable, int ID)
        {
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {

                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT ID, ObjedV FROM SKz WHERE (ID = ?)";

                da.SelectCommand.Parameters.AddWithValue("?", ID);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                result = da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedV_FillByID", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

            return result;
        }

        public static int SKzObjedV_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

            da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
            //da.UpdateCommand.Transaction = Trans;
            da.UpdateCommand.CommandType = System.Data.CommandType.Text;
            da.UpdateCommand.Connection = connection;
            da.UpdateCommand.Transaction = Trans;
            da.UpdateCommand.CommandText = @"UPDATE SKz" +
                " SET " +
                " ObjedV = ? " +
                " WHERE (ID = ?)";


            var pObjedV = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedV", System.Data.OleDb.OleDbType.Double));
            pObjedV.SourceColumn = "ObjedV";

            var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
            pID.SourceColumn = "ID";
            pID.SourceVersion = System.Data.DataRowVersion.Original;

            int result = 0;
            try
            {
                result = da.Update(pohodaDS.SKz);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedV_Update", ex);
                Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
            }
            return result;
        }



        #endregion

        #region SKPVpol

        //public static int SKPVpol_FillByRefAg(DatabasePohoda.SKPVpolDataTable dataTable, int? RefAg)
        //{
        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    int result = 0;
        //    try
        //    {

        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = "SELECT ID, RefPol, RelAgID, Mnozstvi, RefSKz FROM SKPVpol where RefAg=?";


        //        da.SelectCommand.Parameters.AddWithValue("?", RefAg);

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

        //        result = da.Fill(dataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " SKPVpol_FillByRefAg", ex.Message);
        //        Log.Write(dataTable);
        //    }

        //    return result;
        //}

        //public static int SKPVpol_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
        //{
        //    if (connection == null)
        //        throw new Exception("Neni nastaven objekt connection");

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

        //    da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
        //    //da.UpdateCommand.Transaction = Trans;
        //    da.UpdateCommand.CommandType = System.Data.CommandType.Text;
        //    da.UpdateCommand.Connection = connection;
        //    da.UpdateCommand.Transaction = Trans;
        //    da.UpdateCommand.CommandText = @"UPDATE SKPVpol" +
        //        " SET " +
        //        " RefPol = ? ," +
        //        " RelAgID = ? " +
        //        " WHERE (ID = ?)";


        //    var pRefPol = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefPol", System.Data.OleDb.OleDbType.Integer));
        //    pRefPol.SourceColumn = "RefPol";

        //    var pRelAgID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RelAgID", System.Data.OleDb.OleDbType.Integer));
        //    pRelAgID.SourceColumn = "RelAgID";

        //    var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
        //    pID.SourceColumn = "ID";
        //    pID.SourceVersion = System.Data.DataRowVersion.Original;

        //    int result = 0;
        //    try
        //    {
        //        result = da.Update(pohodaDS.SKPPpol);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " SKPVpol_Update", ex.Message);
        //        Log.Write(pohodaDS);
        //    }
        //    return result;
        //}

        #endregion

        #region AD

        public static DatabasePohoda.ADDataTable AD_GetData()
        {
            DatabasePohoda.ADDataTable dataTable = new DatabasePohoda.ADDataTable();
            AD_Fill(dataTable);
            return dataTable;
        }

        public static void AD_Fill(DatabasePohoda.ADDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, Cislo, Firma, Jmeno, Ulice, PSC, Obec, ICO, DIC, P1, P2, P3, P4, P5, P6, RefCM FROM AD";

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " AD_Fill", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        public static DatabasePohoda.ADDataTable AD_GetDataByID(int ID)
        {
            DatabasePohoda.ADDataTable dataTable = new DatabasePohoda.ADDataTable();
            AD_FillByID(dataTable, ID);
            return dataTable;
        }

        public static void AD_FillByID(DatabasePohoda.ADDataTable dataTable, int ID)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, Cislo, Firma, ICO FROM AD where ID=?";
                da.SelectCommand.Parameters.AddWithValue("?", ID);
                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " AD_FillByID", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }



        #endregion

        #region pPK_Predkontace

        //public static DatabasePohoda.pPK_PredkontaceDataTable pPK_Predkontace_GetDataByID(int ID)
        //{
        //    DatabasePohoda.pPK_PredkontaceDataTable dataTable = new DatabasePohoda.pPK_PredkontaceDataTable();
        //    pPK_Predkontace_FillByID(dataTable, ID);
        //    return dataTable;
        //}

        //public static void pPK_Predkontace_FillByID(DatabasePohoda.pPK_PredkontaceDataTable dataTable, int ID)
        //{

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    try
        //    {
        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = @"SELECT p.IDS FROM dbo.pPK AS p LEFT OUTER JOIN dbo.AD AS a ON a.RelPkFV = p.ID WHERE (a.ID = ?)";
        //        da.SelectCommand.Parameters.AddWithValue("?", ID);

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

        //        da.Fill(dataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " pPK_Predkontace_Fill", ex.Message);
        //        Log.Write(dataTable);
        //    }

        //}

        #endregion

        #region Kontrola

        public static DatabasePohoda.KontrolaDataTable Kontrola_GetData_ITEMNMBR_SOPNUMBE(string Cislo, int ID)
        {
            Globals_V1.LoadConfiguration();
            DatabasePohoda.KontrolaDataTable dataTable = new DatabasePohoda.KontrolaDataTable();
            Kontrola_Fill_ITEMNMBR_SOPNUMBE(dataTable, Cislo, ID);
            return dataTable;
        }

        public static void Kontrola_Fill_ITEMNMBR_SOPNUMBE(DatabasePohoda.KontrolaDataTable dataTable, string Cislo, int ID)
        {

            Globals_V1.LoadConfiguration();

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT o.Rezer AS OBJ_Rezer, s.Rezer AS SKz_Rezer, s.StavZ AS SKz_StavZ, s.ObjedP AS SKz_ObjedP " +
                                                "FROM dbo.OBJ AS o LEFT OUTER JOIN dbo.OBJpol AS p ON p.RefAg = o.ID " +
                                                "LEFT OUTER JOIN dbo.SKz AS s ON s.ID = p.RefSKz " +
                                                "WHERE (o.Cislo = ?) AND (s.ID = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", Cislo);
                da.SelectCommand.Parameters.AddWithValue("?", ID);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Kontrola_Fill_ITEMNMBR_SOPNUMBE", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region SKMPPolVazba


        public static DatabasePohoda.SKMPPolVazbaDataTable SKMPPolVazba_GetData_RefAg_RefSKz(int? RefAg, int? RefSKz)
        {
            DatabasePohoda.SKMPPolVazbaDataTable dataTable = new DatabasePohoda.SKMPPolVazbaDataTable();
            SKMPPolVazba_FillBy_RefAg_RefSKz(dataTable, RefAg, RefSKz);
            return dataTable;
        }

        public static void SKMPPolVazba_FillBy_RefAg_RefSKz(DatabasePohoda.SKMPPolVazbaDataTable dataTable, int? RefAg, int? RefSKz)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT RefSKz, RefSKz1 from SKMPpol " +
                                                "WHERE RefAg=? and RefSKz=? " +
                                                "GROUP BY RefSKz, RefSKz1";

                da.SelectCommand.Parameters.AddWithValue("?", RefAg);
                da.SelectCommand.Parameters.AddWithValue("?", RefSKz);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKMPPolVazba_FillBy_RefAg_RefSKz", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region SKzNC


        public static DatabasePohoda.SKzNCDataTable SKzNC_GetDataSKzID(int? RefAg)
        {
            DatabasePohoda.SKzNCDataTable dataTable = new DatabasePohoda.SKzNCDataTable();
            SKzNC_FillBySKzID(dataTable, RefAg);
            return dataTable;
        }

        public static void SKzNC_FillBySKzID(DatabasePohoda.SKzNCDataTable dataTable, int? RefAg)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT RefAg, Firma, EAN, MJEAN FROM SKzNC WHERE (RefAg = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", RefAg);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzNC_FillBySKzID", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region sSklad

        public static DatabasePohoda.sSkladDataTable sSklad_GetData()
        {
            DatabasePohoda.sSkladDataTable dataTable = new DatabasePohoda.sSkladDataTable();
            sSklad_Fill(dataTable);
            return dataTable;
        }

        public static void sSklad_Fill(DatabasePohoda.sSkladDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, IDS, SText, Reklam, Pozn FROM sSklad";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sSklad_Fill", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }



        #endregion

        #region SKzAlternatives

        #region Fill a GetData
        public static DatabasePohoda.SKzAlternativesDataTable SKzAlternatives_GetData()
        {
            DatabasePohoda.SKzAlternativesDataTable dataTable = new DatabasePohoda.SKzAlternativesDataTable();
            SKzAlternatives_Fill(dataTable);
            return dataTable;
        }

        public static void SKzAlternatives_Fill(DatabasePohoda.SKzAlternativesDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, SKz.RefSklad, SKzNC.RefAD AS NCRefAD FROM (SKz INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg)";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_Fill", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region Fill a GetData by Aktivni

        public static DatabasePohoda.SKzAlternativesDataTable SKzAlternatives_GetDataByAktivni()
        {
            DatabasePohoda.SKzAlternativesDataTable dataTable = new DatabasePohoda.SKzAlternativesDataTable();
            SKzAlternatives_FillByAktivni(dataTable);
            return dataTable;
        }

        public static void SKzAlternatives_FillByAktivni(DatabasePohoda.SKzAlternativesDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                    "FROM (SKz INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg) " +
                    "WHERE (SKz.Odbyt <> 0)";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_FillByAktivni", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region ExportovatPouzeAktivniPolozky a Zbozi_DotahovatAlternativniDodavatele


        /// <summary>
        /// SKzAlternatives_FillBy_ExportovatPouzeAktivniPolozky_DotahovatAlternativniDodavatele
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="ExportSkladFilter"></param>
        /// <param name="ExportTypFilter"></param>
        public static void  SKzAlternatives_FillBy_EPAP_DAD(DatabasePohoda.SKzAlternativesDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                                "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                                "FROM SKz ((" +
                                "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg ) " +
                                "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                                "WHERE (SKz.Odbyt <> 0)  AND (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_FillBy_EPAP_DAD", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region Zbozi_DotahovatAlternativniDodavatele


        /// <summary>
        /// SKzAlternatives_FillBy_DotahovatAlternativniDodavatele
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="ExportSkladFilter"></param>
        /// <param name="ExportTypFilter"></param>
        public static void SKzAlternatives_FillBy_DAD(DatabasePohoda.SKzAlternativesDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                                "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                                "FROM SKz ((" +
                                "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg) " +
                                "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                                "WHERE (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_FillBy_DAD", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #endregion

        #region sCMeny

        #region GetData a Fill  Aktivni
        public static DatabasePohoda.sCMenyDataTable sCMeny_GetDataByAktivni()
        {
            DatabasePohoda.sCMenyDataTable dataTable = new DatabasePohoda.sCMenyDataTable();
            sCMeny_FillByAktivni(dataTable);
            return dataTable;
        }

        public static void sCMeny_FillByAktivni(DatabasePohoda.sCMenyDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, Sel, Pouzit, Kod, IDS, Zeme, Mnozstvi, DenEUR, DatDen, KoefEUR, Oznacil, Ucetni, Creator, Pozn, NullCheck_Kod FROM sCMeny WHERE (Pouzit = 1)";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCMeny_FillByAktivni", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        public static DatabasePohoda.sCMenyDataTable sCMeny_GetDataByKod(string Kod)
        {
            DatabasePohoda.sCMenyDataTable dataTable = new DatabasePohoda.sCMenyDataTable();
            sCMeny_FillByKod(dataTable, Kod);
            return dataTable;
        }

        public static void sCMeny_FillByKod(DatabasePohoda.sCMenyDataTable dataTable, string Kod)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, Sel, Pouzit, Kod, IDS, Zeme, Mnozstvi, DenEUR, DatDen, KoefEUR, Oznacil, Ucetni, Creator, Pozn, NullCheck_Kod FROM sCMeny WHERE (Kod = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", Kod);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCMeny_FillByKod", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region FakturaCislo


        public static DatabasePohoda.FakturaCisloDataTable FakturaCislo_GetDataByNoMSTParams(string Cislo)
        {
            DatabasePohoda.FakturaCisloDataTable dataTable = new DatabasePohoda.FakturaCisloDataTable();
            FakturaCislo_FillByNoMSTParams(dataTable, Cislo);
            return dataTable;
        }

        public static void FakturaCislo_FillByNoMSTParams(DatabasePohoda.FakturaCisloDataTable dataTable, string Cislo)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                string command = string.Empty;

                command += @"SELECT FA.ID AS FA_ID, FA.RelCR AS FA_RelCR, FA.Cislo AS FA_Cislo, FA.VarSym AS FA_VarSym, FA.SText AS FA_SText, FApol.ID AS FApol_ID, FApol.SText AS FApol_SText, FApol.Mnozstvi AS FApol_Mnozstvi, ";
                command += @"FApol.Prenes AS FApol_Prenes, FApol.MJ AS FApol_MJ, FApol.MJKoef AS FApol_MJKoef, FApol.Kod AS FApol_Kod, SKz.ID AS SKz_ID, SKz.IDS AS SKz_IDS, SKz.EAN AS SKz_EAN, SKz.RelSKzVC AS SKz_RelSKzVC, ";
                command += @"SKz.MJ2 AS Skz_MJ2, SKz.MJ3 AS Skz_MJ3, SKz.MJ2Koef AS Skz_MJ2Koef, SKz.MJ3Koef AS Skz_MJ3Koef, SKz.RefSklad AS Skz_RefSklad, SKz.SText AS SKz_SText ";

                if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    command += " ,SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS ";
                }

                command += "FROM ((FA LEFT OUTER JOIN FApol ON FApol.RefAg = FA.ID) LEFT OUTER JOIN SKz ON SKz.ID = FApol.RefSKz) WHERE (FA.Cislo = ?)";

                da.SelectCommand.CommandText = command;
                da.SelectCommand.Parameters.AddWithValue("?", Cislo);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " FakturaCislo_FillByNoMSTParams", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region SKPP


        public static DatabasePohoda.SKPPDataTable SKPP_GetDataByCislo(string Cislo)
        {
            DatabasePohoda.SKPPDataTable dataTable = new DatabasePohoda.SKPPDataTable();
            SKPP_FillByCislo(dataTable, Cislo);
            return dataTable;
        }

        public static void SKPP_FillByCislo(DatabasePohoda.SKPPDataTable dataTable, string Cislo)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID FROM SKPP where cislo=?";

                da.SelectCommand.Parameters.AddWithValue("?", Cislo);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPP_FillByCislo", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        public static int SKPP_UpdateCreatorByCislo(string Creator, string Original_Cislo)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            int result = 0;
            try
            {
                da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
                da.UpdateCommand.CommandType = System.Data.CommandType.Text;
                //da.UpdateCommand.CommandTimeout
                da.UpdateCommand.CommandText = @"UPDATE SKPP SET Creator = ? WHERE (Cislo = ?)";

                da.UpdateCommand.Parameters.AddWithValue("?", Creator);
                da.UpdateCommand.Parameters.AddWithValue("?", Original_Cislo);


                da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                da.UpdateCommand.Connection.Open();
                result = da.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPP_UpdateCreatorByCislo", ex);
                //Log.Write(dataTable);
            }
            finally
            {
                da.UpdateCommand.Connection.Close();
            }
            return result;

        }


        #endregion

        #region SKPV


        //public static DatabasePohoda.SKPVDataTable SKPV_GetDataByCislo(string Cislo)
        //{
        //    DatabasePohoda.SKPVDataTable dataTable = new DatabasePohoda.SKPVDataTable();
        //    SKPV_FillByCislo(dataTable, Cislo);
        //    return dataTable;
        //}

        //public static void SKPV_FillByCislo(DatabasePohoda.SKPVDataTable dataTable, string Cislo)
        //{

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    try
        //    {
        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = @"SELECT ID FROM SKPV where cislo=?";
        //        da.SelectCommand.Parameters.AddWithValue("?", Cislo);

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

        //        da.Fill(dataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " SKPV_FillByCislo", ex.Message);
        //        Log.Write(dataTable);
        //    }

        //}


        //public static int SKPV_UpdateCreatorByCislo(string Creator, string Original_Cislo)
        //{

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    int result = 0;
        //    try
        //    {
        //        da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
        //        da.UpdateCommand.CommandType = System.Data.CommandType.Text;
        //        //da.UpdateCommand.CommandTimeout
        //        da.UpdateCommand.CommandText = @"UPDATE SKPV SET Creator = ? WHERE (Cislo = ?)";

        //        da.UpdateCommand.Parameters.AddWithValue("?", Creator);
        //        da.UpdateCommand.Parameters.AddWithValue("?", Original_Cislo);


        //        da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
        //        da.UpdateCommand.Connection.Open();
        //        result = da.UpdateCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write("Pohoda Tabulky Definice SQL Dotazu", " SKPV_UpdateCreatorByCislo", ex.Message);
        //        //Log.Write(dataTable);
        //    }
        //    finally
        //    {
        //        da.UpdateCommand.Connection.Close();
        //    }
        //    return result;

        //}


        #endregion

        #region SKz

        #region Aktivni

        public static DatabasePohoda.SKzDataTable SKz_GetDataByAktivniPolozky()
        {
            DatabasePohoda.SKzDataTable dataTable = new DatabasePohoda.SKzDataTable();
            SKz_FillByAktivniPolozky(dataTable);
            return dataTable;
        }

        public static void SKz_FillByAktivniPolozky(DatabasePohoda.SKzDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, RefStruct, RelSKzVC, IDS, EAN, Nazev, MJ, StavZ, RefSklad, RefAD, MJ2, MJ3, MJ2Koef, MJ3Koef, Hmotnost " +
                "FROM SKz WHERE (Odbyt <> 0)";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByAktivniPolozky", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region by ID

        public static DatabasePohoda.SKzDataTable SKz_GetDataByID(int? ID)
        {
            DatabasePohoda.SKzDataTable dataTable = new DatabasePohoda.SKzDataTable();
            SKz_FillByID(dataTable, ID);
            return dataTable;
        }

        public static void SKz_FillByID(DatabasePohoda.SKzDataTable dataTable, int? ID)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                string comand = string.Empty;

                comand += "SELECT ID, RefSklad, RefStruct, RelSKzVC, IDS, EAN, Nazev, MJ, MJ2, MJ3, MJ2Koef, MJ3Koef, StavZ, RefAD, Hmotnost, RelSkTyp ";

                if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    comand += ", VPrFVTS, VPrFPTS, VPrFITS, VPrFDTS, VPrFXTS, RefVPrFVTS, RefVPrFPTS, RefVPrFITS, RefVPrFDTS, RefVPrFXTS, VPrCZExpTrackIS, VPrCZSerNumTrIS ";
                }

                comand += "FROM SKz WHERE (ID = ?)";

                da.SelectCommand.CommandText = comand;
                da.SelectCommand.Parameters.AddWithValue("?", ID);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByID", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region Optimalize

        public static DatabasePohoda.SKzDataTable SKz_GetDataByOptimalize()
        {
            DatabasePohoda.SKzDataTable dataTable = new DatabasePohoda.SKzDataTable();
            SKz_FillByOptimalize(dataTable);
            return dataTable;
        }

        public static void SKz_FillByOptimalize(DatabasePohoda.SKzDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, RefStruct, RelSKzVC, IDS, EAN, Nazev, MJ, StavZ, RefSklad, RefAD, MJ2, MJ3, MJ2Koef, MJ3Koef, Hmotnost FROM SKz";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByOptimalize", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region ExportovatPouzeAktivniPolozky a Zbozi_DotahovatAlternativniDodavatele


        /// <summary>
        /// SKzAlternatives_FillBy_ExportovatPouzeAktivniPolozky_DotahovatAlternativniDodavatele
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="ExportSkladFilter"></param>
        /// <param name="ExportTypFilter"></param>
        public static void SKz_FillBy_EPAP_DAD(DatabasePohoda.SKzDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
                            "SKz.MJ2Koef, SKz.MJ3Koef, SKz.Hmotnost ";

                if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    da.SelectCommand.CommandText += " ,SKz.RefVPrFXTS , SKz.RefVPrFDTS , SKz.RefVPrFITS , SKz.RefVPrFPTS , SKz.RefVPrFVTS , SKz.VPrFXTS , SKz.VPrFDTS , SKz.VPrFITS , SKz.VPrFPTS , SKz.VPrFVTS ";
                }

                da.SelectCommand.CommandText +=
                            "FROM SKz " +
                            "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                            "WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillBy_EPAP_DAD", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region Zbozi_DotahovatAlternativniDodavatele


        /// <summary>
        /// SKzAlternatives_FillBy_DotahovatAlternativniDodavatele
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="ExportSkladFilter"></param>
        /// <param name="ExportTypFilter"></param>
        public static void SKz_FillBy_DAD(DatabasePohoda.SKzDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
                            "SKz.MJ2Koef, SKz.MJ3Koef, SKz.Hmotnost " +
                            "FROM SKz " +
                            "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                            "WHERE (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillBy_DAD", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region GetVNakupByID

        public static decimal? SKz_GetVNakupByID(int? ID)
        {

           decimal? tmp = null;
            try
            {

                //System.Data.OleDb.OleDbConnection SQLCON = 
                //SQLCON.Open();

                System.Data.OleDb.OleDbCommand SQLCommand = new System.Data.OleDb.OleDbCommand();
                SQLCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                SQLCommand.CommandType = System.Data.CommandType.Text;

                SQLCommand.CommandText = "SELECT VNakup FROM SKz WHERE (ID = ?)";
                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.Integer).Value = ID;

                SQLCommand.Connection.Open();

                object result = SQLCommand.ExecuteScalar();

                SQLCommand.Connection.Close();

                if ((result != null) && (result is decimal))
                {
                    tmp = (decimal)result;

                }

                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_GetVNakupByID", ex);
            }

            return tmp;
        }

        #endregion

        #region Get_Kc_From_SKPV_By_Cislo_ID





        public static decimal? Get_Kc_From_SKPV_By_Cislo_ID(string Cislo, int? ID)
        {

            decimal? tmp = null;
            try
            {

                //System.Data.OleDb.OleDbConnection SQLCON = 
                //SQLCON.Open();

                System.Data.OleDb.OleDbCommand SQLCommand = new System.Data.OleDb.OleDbCommand();
                SQLCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                SQLCommand.CommandType = System.Data.CommandType.Text;

                
                //SQLCommand.CommandText = "SELECT VNakup FROM SKz WHERE (ID = ?)";

               SQLCommand.CommandText =  "select Vpol.Kc, Vpol.SText from SKPVpol as Vpol " +
                "left join SKPV as V ON v.ID = Vpol.RefAG " +
                "where V.Cislo = ? " +
                "AND Vpol.RefSKz  = ? ";

                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.VarChar).Value = Cislo;
                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.Integer).Value = ID;
                

                SQLCommand.Connection.Open();

                object result = SQLCommand.ExecuteScalar();

                SQLCommand.Connection.Close();

                if ((result != null) && (result is decimal))
                {
                    tmp = (decimal)result;

                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Get_Kc_From_SKPV_By_Cislo_ID", ex);
            }

            return tmp;
        }



        #endregion

        #region Filtr typ položky

        public static DatabasePohoda.SKz_FiltrTypDataTable SKz_Get_FiltrTypByID(string TypMaterialu)
        {
            DatabasePohoda.SKz_FiltrTypDataTable dataTable = new DatabasePohoda.SKz_FiltrTypDataTable();
            SKz_Fill_FiltrTypByID(dataTable, TypMaterialu);
            return dataTable;
        }

        public static void SKz_Fill_FiltrTypByID(DatabasePohoda.SKz_FiltrTypDataTable dataTable, string TypMaterialu)
        {
            try
            {

                using (var con = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB))
                {
                    using (var com = con.CreateCommand())
                    {
                        com.CommandType = System.Data.CommandType.Text;

                        string comand = string.Empty;

                        comand += "SELECT ID, RelSkTyp FROM SKz ";

                        if (!string.IsNullOrEmpty(TypMaterialu))
                        {
                            comand += " WHERE RelSkTyp = '" + TypMaterialu + "'";
                        }

                        com.CommandText = comand;

                        using (var da = new System.Data.OleDb.OleDbDataAdapter())
                        {
                            da.SelectCommand = com;
                            da.Fill(dataTable);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FiltrTyp_ByID", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }


        #endregion

        #region Stav Zasob

        public static DatabasePohoda.SKz_StavZasobDataTable SKz_GetDataBy_StavZasob()
        {
            DatabasePohoda.SKz_StavZasobDataTable dataTable = new DatabasePohoda.SKz_StavZasobDataTable();
            SKz_FillBy_StavZasob(dataTable);
            return dataTable;
        }

        public static void SKz_FillBy_StavZasob(DatabasePohoda.SKz_StavZasobDataTable dataTable)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                Globals_V1.LoadConfiguration();

                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.StavZ" +
                " FROM SKz " +
                " INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                " WHERE (s.IDS IN (" + Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter + "))";

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByAktivniPolozky", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region Get ID Skladu z ID polozku

        public static string SKz_GetIDSkladuByIDPolozky(string ID)
        {

            string tmp = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                    return string.Empty;
                //System.Data.OleDb.OleDbConnection SQLCON = 
                //SQLCON.Open();

                System.Data.OleDb.OleDbCommand SQLCommand = new System.Data.OleDb.OleDbCommand();
                SQLCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                SQLCommand.CommandType = System.Data.CommandType.Text;

                SQLCommand.CommandText = "SELECT RefSklad FROM SKz WHERE (ID = ?)";
                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.Integer).Value = int.Parse(ID); // Tady muže nastat chyba parsovana... ale ID v IS pohoda je INT tak by nemnelo

                SQLCommand.Connection.Open();

                object result = SQLCommand.ExecuteScalar();

                SQLCommand.Connection.Close();

                if (result != null) 
                {
                    tmp = result.ToString();

                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_GetVNakupByID", ex);
            }

            return tmp;
        }

        #endregion

        #region SKz_StavDetail

        internal static Vyroba_Planovani SKz_GetStavDetail(string iTEMCODE)
        {
            Vyroba_Planovani ds = new Vyroba_Planovani();
            SKz_FillStavDetail(ds, iTEMCODE);
            return ds;
        }

        public static void SKz_FillStavDetail(Vyroba_Planovani ds, string iTEMCODE)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                Globals_V1.LoadConfiguration();

                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT Z.ID as ITEMNMBR, Z.IDS as ITEMCODE, Z.RefSklad as SKL_ID, S.SText as SKL_Desc,S.IDS as SKL_Skratka, Z.StavZ as QTY_Stav FROM Skz as Z " + 
                    @" LEFT JOIN SSklad as S ON S.ID = Z.RefSklad" +
                    @" WHERE Z.IDS = '" + iTEMCODE.Trim() + "'";

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(ds, ds.Planovani_Detail.TableName);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillStavDetail", ex);
                Fask.Logging.ExceptionHandler2.Handle(ds);
            }

        }

        #endregion

        #region Get by IDS and sSklad

        public static int? SKz_Get_ID_byIDS_sSklad(string IDS, int sSklad)
        {

            try
            {
                System.Data.OleDb.OleDbCommand SQLCommand = new System.Data.OleDb.OleDbCommand();
                SQLCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                SQLCommand.CommandType = System.Data.CommandType.Text;

                SQLCommand.CommandText = "SELECT ID FROM SKz WHERE (IDS = ?) AND (RefSklad=?)";
                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.VarChar).Value = IDS; 
                SQLCommand.Parameters.Add("?", System.Data.OleDb.OleDbType.Integer).Value = sSklad; 

                SQLCommand.Connection.Open();

                object result = SQLCommand.ExecuteScalar();

                SQLCommand.Connection.Close();

                if (result != null)
                {
                    return (int)result;
                }

                return null;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_Get_ID_byIDS_sSklad", ex);
                return null;
            }

        }


        #endregion

        #endregion

        #region  sSTR



        public static DatabasePohoda.sSTRDataTable sSTR_GetData()
        {
            DatabasePohoda.sSTRDataTable dataTable = new DatabasePohoda.sSTRDataTable();
            sSTR_Fill(dataTable);
            return dataTable;
        }

        public static void sSTR_Fill(DatabasePohoda.sSTRDataTable dataTable)
        {
            string pom = Globals_V1.LoadConfiguration();

            if (pom != "OK")
                throw new Exception("Nezdařilo se načteni konfigurace.");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"select ID, SText, IDS from sSTR ";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sSTR_Fill", ex);
                Fask.Logging.ExceptionHandler2.Handle(dataTable);
            }

        }

        #endregion

        #region SKzINV

        public static int SKzINV_UpdateQuery(
            System.Data.OleDb.OleDbConnection connection, 
            System.Data.OleDb.OleDbTransaction Trans, 
            decimal? StavZsk, 
            decimal? StavZroz, 
            int? Original_RefSKz, 
            int? Original_RefSklad, 
            int? Original_RefAg)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");



            using (var com = connection.CreateCommand())
            {

                com.CommandType = System.Data.CommandType.Text;


                com.Connection = connection;
                com.Transaction = Trans;

                com.CommandText = "UPDATE SKzInv SET " +
                    "StavZsk = ?, StavZroz = ? - StavZ, Audit = 1 " +
                    " WHERE (RefSKz = ?) AND (RefSklad = ?) AND (RefAg = ?)";

                com.Parameters.AddWithValue("?", StavZsk.HasValue ? StavZsk : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", StavZroz.HasValue ? StavZroz : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", Original_RefSKz.HasValue ? Original_RefSKz : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", Original_RefSklad.HasValue ? Original_RefSklad : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", Original_RefAg.HasValue ? Original_RefAg : (object)DBNull.Value);

                int result = 0;
                try
                {
                 result = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzINV_UpdateQuery", ex);
                    //Log.Write(dataTable);
                }
                return result;
            }
        }

        public static int SKzINV_UpdateQueryNotAudit(
                    System.Data.OleDb.OleDbConnection connection,
                    System.Data.OleDb.OleDbTransaction Trans,
                    decimal? StavZsk,
                    decimal? StavZroz,
                    int? Original_RefSKz,
                    int? Original_RefSklad,
                    int? Original_RefAg)
        {
            if (connection == null)
                throw new Exception("Neni nastaven objekt connection");



            using (var com = connection.CreateCommand())
            {

                com.CommandType = System.Data.CommandType.Text;


                com.Connection = connection;
                com.Transaction = Trans;

                com.CommandText = "UPDATE SKzInv " + 
                    "SET StavZsk = ?, StavZroz = ? - StavZ, Audit = 1" +
                    " WHERE (RefSKz = ?) AND (Audit <> 1) AND (RefSklad = ?) AND (RefAg = ?)";


                com.Parameters.AddWithValue("?", StavZsk.HasValue ? StavZsk : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", StavZroz.HasValue ? StavZroz : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", Original_RefSKz.HasValue ? Original_RefSKz : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", Original_RefSklad.HasValue ? Original_RefSklad : (object)DBNull.Value);
                com.Parameters.AddWithValue("?", Original_RefAg.HasValue ? Original_RefAg : (object)DBNull.Value);

                int result = 0;
                try
                {
                    result = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzINV_UpdateQuery", ex);
                    //Log.Write(dataTable);
                }
                return result;
            }
        }



        public static Pohoda_DataSets.Inventura.SKzInvSeznamyPolDataTable SKzInvSeznamyPol()
        {
            try
            {


                Pohoda_DataSets.Inventura.SKzInvSeznamyPolDataTable polozky = new Pohoda_DataSets.Inventura.SKzInvSeznamyPolDataTable();
                Fill_Universal(polozky, "SELECT ID, Sel, RefAg, RefInvPol, RefSKz, RefSKz0, SText, Pozn, Kod, VCislo, SKzVC, Mnozstvi, MJ, MJKoef, BPrenes, OrderFld FROM SKzInvSeznamyPol");
                return polozky;


            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Inventura", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
            }
        }

        public static Pohoda_DataSets.Inventura.SKzInvDataTable SKzInv()
        {

            try
            {
                Pohoda_DataSets.Inventura.SKzInvDataTable polozky = new Pohoda_DataSets.Inventura.SKzInvDataTable();
                Fill_Universal(polozky, @"SELECT SI.ID, SI.UsrOrder, SI.Sel, SI.RefAg, SI.RefSKz, SI.RelAgID, SI.RefDokl, SI.RelSkTyp, SI.RelSkDruh, SI.AUcet, SI.RelZcTp, SI.RefStruct, SI.RefSklad, SI.RelSKzVC, SI.IDS, SI.EAN, SI.Nazev, SI.SText, SI.MJ, SI.StavZ, SI.StavZsk, " +
                         " SI.StavZroz, SI.VNakup, SI.VNakupC, SI.VNakupJ, SI.PLU, SI.Pozn, SI.Audit, SI.Prenes, SI.PrenesInvSPol, SI.Oznacil, SI.Ucetni, SI.Creator, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, " +
                         " SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, " +
                         " SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS FROM dbo.SKzInv AS SI LEFT OUTER JOIN dbo.SKz ON SKz.ID = SI.RefSKz");
                return polozky;

            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        public static Pohoda_DataSets.Inventura.SKzInvDataTable SKzInvNezauct()
        {
            try
            {

                Pohoda_DataSets.Inventura.SKzInvDataTable polozky = new Pohoda_DataSets.Inventura.SKzInvDataTable();
                Fill_Universal(polozky, @"SELECT SKzInv.ID, SKzInv.RefAg, SKzInv.RefSKz, SKzInv.RefSklad, SKzInv.RelSKzVC, SKzInv.IDS, SKzInv.EAN, SKzInv.Nazev, SKzInv.MJ, SKzInv.StavZ, SKzInv.PLU, SKz.VPrFXTS AS SKz_VPrFXTS, " +
                        " SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, SKz.VPrFDTS AS SKz_VPrFDTS, " +
                        " SKz.VPrFITS AS SKz_VPrFITS, SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS FROM dbo.SKzInv INNER JOIN dbo.SKzInvLst ON SKzInv.RefAg = SKzInvLst.ID LEFT OUTER JOIN dbo.SKz ON SKz.ID = SKzInv.RefSKz " +
                        " WHERE (SKzInvLst.Zauct <= 0)");
                return polozky;


            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        public static Pohoda_DataSets.Inventura.SKzInvAlternativniDodavateleDataTable SKzInvNezauctAlternativy()
        {
            try
            {

                Pohoda_DataSets.Inventura.SKzInvAlternativniDodavateleDataTable polozky = new Pohoda_DataSets.Inventura.SKzInvAlternativniDodavateleDataTable();
                Fill_Universal(polozky, @"SELECT SKzInv.IDS, SKzInv.EAN, SKzInv.Nazev, SKzInv.MJ, SKzInv.PLU, SKzNC.RefAD AS NCDodavatelID, SKzNC.Firma AS NCFirma, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, SKzInv.RefAg, SKzInv.RefSKz FROM (( SKzInv INNER JOIN SKz ON SKzInv.RefSKz = SKz.ID ) INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg )");
                return polozky;

            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        public static Pohoda_DataSets.Inventura.SKzInvLstDataTable SKzInvList()
        {
            try
            {
                Pohoda_DataSets.Inventura.SKzInvLstDataTable polozky = new Pohoda_DataSets.Inventura.SKzInvLstDataTable();
                Fill_Universal(polozky, "SELECT ID, UsrOrder, Sel, RefSklad, Datum, SText, Zauct, OldDt, Pozn, DatCreate, DatSave, Oznacil, Ucetni, Creator FROM SKzInvLst");
                return polozky;

            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        internal static Pohoda_DataSets.Inventura.SKzInvVCDataTable SKzInvVCNezauct()
        {
            try
            {
                Pohoda_DataSets.Inventura.SKzInvVCDataTable polozky = new Pohoda_DataSets.Inventura.SKzInvVCDataTable();

                string s = 
                    " SELECT SKzInvVC.ID, SKzInvVC.RefAg, SKzInvVC.RefSKz, SKzInvVC.RefSklad, SKzInvVC.VCislo, SKzInvVC.StavZ, SKzVC.DatExp " +
                    " FROM SKzInvVC " +
                    " INNER JOIN SKzInvLst ON SKzInvVC.RefAg = SKzInvLst.ID " +
                    " LEFT JOIN SKzVC ON SKzVC.RefAg = SKzInvVC.RefSKz AND SKzVC.VCislo = SKzInvVC.VCislo" + 
                    " WHERE ( SKzInvLst.Zauct <= 0 ) " ;

                Fill_Universal(polozky, s);
                return polozky;

            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }


        #endregion

        #endregion


        #region 21.12.2018 Dotazi volny pohyb Rady

        public static int? GetYearByPrelom()
        {
            int? YEAR = DateTime.Now.Year;

            Pohoda_DataSets.DatabasePohoda.sPrelomDataTable dt_prelom = Database.Pohoda.sPrelom_GetData(Globals_V1.Konfigurace.PohodaInfo[0].Login_IDS_pohoda);

            if ((dt_prelom != null) && (dt_prelom.Count > 0))
            {
                if (dt_prelom[0].IsPrelom)
                {
                    YEAR = dt_prelom[0].UsRok + 1;
                }
                else
                {
                    YEAR = dt_prelom[0].UsRok;
                }

            }
            return YEAR;
        }

        public static string GetIDRadyByYearSText(Doklad typDoklad, int? YEAR)
        {
            string idsradadokladu = string.Empty;
            Pohoda_DataSets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
                YEAR,
                26, //prijem
                "%" + typDoklad.idsradatext + "%");

            if (dt_crady != null && dt_crady.Count > 0)
            {
                idsradadokladu = dt_crady[0].ID.ToString();
            }
            return idsradadokladu;
        }

        #endregion

        #region Get_AttributeToSN

        public static DatabasePohoda.SKzVCDataTable SKzVC_AttributeToSN(string ITEMNMBR, string SERLTNUM)
        {
            DatabasePohoda.SKzVCDataTable polozky = null;
            try
            {
                polozky = new DatabasePohoda.SKzVCDataTable();

                string SQL;
                SQL = @"select ID, RefAg, VCislo, StavVC, " +
                    " RelSKzVc, DatExp ";

                if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    SQL += ", VPrSarzeKSN, VPrExspiraceKSN ";
                }

                SQL += " from SKzVC where 1 = 1";

                if (!string.IsNullOrEmpty(ITEMNMBR))
                {
                    SQL += " AND RefAg = '" + ITEMNMBR.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(SERLTNUM))
                {
                    SQL += " AND VCislo = '" + SERLTNUM.Trim() + "' ";
                }


                Fill_Universal(polozky, SQL);

                return polozky;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }


        #endregion

        /// <summary>
        /// Testuje, zda cislo je existujici cislo faktury
        /// </summary>
        /// <param name="cislo">cislo faktury</param>
        /// <returns>True: pokud existuje faktura s timto cislem</returns>
        public static bool FA_Exists(string cislo)
        {
            Globals_V1.LoadConfiguration();
            // TODO : test na radu dokladu ??? 
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select Cislo from FA where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleCommand.Connection.Open();

                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                    return false; // neexistuje cislo faktury
                else
                    return true; // existuje cislo faktury

            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }


        /// <summary>
        /// Zjistuje, zda jsou na dokladu pouze polozky skladu
        /// </summary>
        /// <param name="cislo">Cislo faktury, ktera se ma proverit</param>
        /// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
        /// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
        /// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
        public static bool FA_JedenSklad(string cislo, string sklad)
        {
            // SQL => test, zda na dokladu existuji polozky z jinych skladu
            // => vraci pocet jinych skladu nez je urceny z polozek dokladu
            // TODO: co pokud je textova polozka?
            // => nema sklad a nebude nalezena ...
            //1) vrati ID dokladu ...
            // Select ID from FA where Cislo=? 
            //2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
            //select count(RefSklad) as JineSklady
            //from SKz
            //where ID in (
            //    select RefSKz
            //    from FApol
            //    where RefAg in (
            //        select ID from FA 
            //        where Cislo='162000002'
            //        )
            //    )
            //and RefSklad <> 1
            Globals_V1.LoadConfiguration();
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                //Log.Write(" .1");
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select ID from FA where Cislo=?",
                    oleConnection
                    );

                oleCommand.Parameters.AddWithValue("?", cislo);
                //Log.Write(" .2");
                oleConnection.Open();
                //Log.Write(" .3");
                object o = oleCommand.ExecuteScalar();
                //Log.Write(" .4");
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    throw new Exception("Neexistuje číslo faktury '" + cislo + "'");
                }

                //Log.Write(" .5");
                int FA_ID = (int)o; // toto musi byt ID Objednavky
                oleCommand.Parameters.Clear();
                //Log.Write(" .6");
                oleCommand.CommandText =
                    "Select count(RefSklad) as JineSklady " +
                    "from SKz " +
                    "where ID in ( " +
                    "    select RefSKz " +
                    "    from FApol " +
                    "    where RefAg=? " +
                    "    ) " +
                    "and RefSklad <> " + sklad;
                //Log.Write(" .7");
                oleCommand.Parameters.AddWithValue("?", FA_ID);
                //Log.Write(" .8");
                o = oleCommand.ExecuteScalar();
                //Log.Write(" .9");
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    return true;
                }
                else
                {
                    //Log.Write(" .10");
                    int pocetPolozekZJinychSkladu = (int)o;
                    //Log.Write(" .11");
                    if (pocetPolozekZJinychSkladu > 0)
                    {
                        throw new Exception("Faktura '" + cislo + "' obsahuje položky z více skladů!");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                //Log.Write(" .12");
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
                //Log.Write(" .13");
            }
            //Log.Write(" .14");
        }


        // dale jak rozlisit prijem/vydej ???
        public static bool Prevod_Exists(string cislo)
        {
            Globals_V1.LoadConfiguration();
            // TODO : test na radu dokladu ??? 
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select Cislo from SKMP where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleConnection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                    return false; // neexistuje cislo prevodky
                else
                    return true; // existuje cislo prevodky
            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }


        /// <summary>
        /// Zjistuje, zda jsou na dokladu pouze polozky jednoho zdrojoveho skladu
        /// </summary>
        /// <param name="cislo">Cislo prevodky, ktera se ma proverit</param>
        /// <param name="sklad">Cislo zdrojoveho skladu, ktery a jen ma byt na dokladu</param>
        /// <returns>Exception(False):jestlize obsahuje polozky jineho zdrojoveho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
        /// <exception>Vyjimka: jestlize obsahuje polozky jineho zdrojoveho skladu</exception>
        public static bool Prevod_JedenZdrojSklad(string cislo, string sklad)
        {
            // SQL => test, zda na dokladu existuji polozky z jinych skladu
            // => vraci pocet jinych skladu nez je urceny z polozek dokladu
            // TODO: co pokud je textova polozka?
            // => nema sklad a nebude nalezena ...
            //1) vrati ID obj ...
            // Select ID from SKMP where Cislo=? 
            //2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
            //select count(RefSklad) as JineSklady
            //from SKz
            //where ID in (
            //    select RefSKz
            //    from SKMPpol
            //    where RefAg in (
            //        select ID from SKMP 
            //        where Cislo='162000002'
            //        )
            //    )
            //and RefSklad <> 1
            Globals_V1.LoadConfiguration();
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select ID from SKMP where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleConnection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    throw new Exception("Neexistuje číslo převodky '" + cislo + "'");
                }

                oleCommand.Parameters.Clear();
                int SKMP_ID = (int)o; // toto musi byt ID Objednavky
                oleCommand.Parameters.Clear();
                oleCommand.CommandText =
                    "Select count(RefSklad) as JineSklady " +
                    "from SKz " +
                    "where ID in ( " +
                    "    select RefSKz " +
                    "    from SKMPpol " +
                    "    where RefAg=? " +
                    "    ) " +
                    "and RefSklad <> " + sklad;
                oleCommand.Parameters.AddWithValue("?", SKMP_ID);
                o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    return true;
                }
                else
                {
                    int pocetPolozekZJinychSkladu = (int)o;
                    if (pocetPolozekZJinychSkladu > 0)
                    {
                        throw new Exception("Převodka '" + cislo + "' obsahuje položky z jiných skladů!");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }

        /// <summary>
        /// Testuje, zda cislo je existujici cislo prodejky
        /// </summary>
        /// <param name="cislo">cislo prodejky</param>
        /// <returns>True: pokud existuje prodejka s timto cislem</returns>
        public static bool Prodejka_Exists(string cislo)
        {
            Globals_V1.LoadConfiguration();
            // TODO : test na radu dokladu ??? 
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select Cislo from PH where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleCommand.Connection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                    return false; // neexistuje cislo faktury
                else
                    return true; // existuje cislo faktury

            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }

        /// <summary>
        /// Zjistuje, zda jsou na dokladu pouze polozky pozadovaneho skladu
        /// </summary>
        /// <param name="cislo">Cislo dokladu, ktery se ma proverit</param>
        /// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
        /// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
        /// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
        public static bool Prodejka_JedenSklad(string cislo, string sklad)
        {
            // SQL => test, zda na dokladu existuji polozky z jinych skladu
            // => vraci pocet jinych skladu nez je urceny z polozek dokladu
            // TODO: co pokud je textova polozka?
            // => nema sklad a nebude nalezena ...
            //1) vrati ID dokladu ...
            // Select ID from SKPV where Cislo=? 
            //2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
            //select count(RefSklad) as JineSklady
            //from SKz
            //where ID in (
            //    select RefSKz
            //    from PHpol
            //    where RefAg in (
            //        select ID from PH 
            //        where Cislo='16PH00001'
            //        )
            //    )
            //and RefSklad <> 1
            Globals_V1.LoadConfiguration();
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select ID from PH where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleConnection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    throw new Exception("Neexistuje číslo výdejky '" + cislo + "'");
                }

                int PRODEJKA_ID = (int)o; // toto musi byt ID Objednavky
                oleCommand.Parameters.Clear();
                oleCommand.CommandText =
                    "Select count(RefSklad) as JineSklady " +
                    "from SKz " +
                    "where ID in ( " +
                    "    select RefSKz " +
                    "    from PHpol " +
                    "    where RefAg=? " +
                    "    ) " +
                    "and RefSklad <> " + sklad;
                oleCommand.Parameters.AddWithValue("?", PRODEJKA_ID);
                o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    return true;
                }
                else
                {
                    int pocetPolozekZJinychSkladu = (int)o;
                    if (pocetPolozekZJinychSkladu > 0)
                    {
                        throw new Exception("Prodejka '" + cislo + "' obsahuje položky z více skladů!");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }

        #region Vydejka

        /// <summary>
        /// Testuje, zda cislo je existujici cislo vydejky
        /// </summary>
        /// <param name="cislo">cislo vydejky</param>
        /// <returns>True: pokud existuje vydejka s timto cislem</returns>
        public static bool Vydejka_Exists(string cislo)
        {
            Globals_V1.LoadConfiguration();
            // TODO : test na radu dokladu ??? 
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select Cislo from SKPV where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleCommand.Connection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                    return false; // neexistuje cislo faktury
                else
                    return true; // existuje cislo faktury

            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }

        /// <summary>
        /// Zjistuje, zda jsou na dokladu pouze polozky pozadovaneho skladu
        /// </summary>
        /// <param name="cislo">Cislo vydejky, ktera se ma proverit</param>
        /// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
        /// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
        /// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
        public static bool Vydejka_JedenSklad(string cislo, string sklad)
        {
            // SQL => test, zda na dokladu existuji polozky z jinych skladu
            // => vraci pocet jinych skladu nez je urceny z polozek dokladu
            // TODO: co pokud je textova polozka?
            // => nema sklad a nebude nalezena ...
            //1) vrati ID dokladu ...
            // Select ID from SKPV where Cislo=? 
            //2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
            //select count(RefSklad) as JineSklady
            //from SKz
            //where ID in (
            //    select RefSKz
            //    from SKPVpol
            //    where RefAg in (
            //        select ID from SKPV 
            //        where Cislo='16SV00001'
            //        )
            //    )
            //and RefSklad <> 1
            Globals_V1.LoadConfiguration();
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select ID from SKPV where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleConnection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    throw new Exception("Neexistuje číslo výdejky '" + cislo + "'");
                }

                int VYDEJKA_ID = (int)o; // toto musi byt ID Objednavky
                oleCommand.Parameters.Clear();
                oleCommand.CommandText =
                    "Select count(RefSklad) as JineSklady " +
                    "from SKz " +
                    "where ID in ( " +
                    "    select RefSKz " +
                    "    from SKPVpol " +
                    "    where RefAg=? " +
                    "    ) " +
                    "and RefSklad <> " + sklad;
                oleCommand.Parameters.AddWithValue("?", VYDEJKA_ID);
                o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    return true;
                }
                else
                {
                    int pocetPolozekZJinychSkladu = (int)o;
                    if (pocetPolozekZJinychSkladu > 0)
                    {
                        throw new Exception("Výdejka '" + cislo + "' obsahuje položky z více skladů!");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }

        #endregion

        internal static bool PrjateObjednavky_Exists(string cislo)
        {
            Globals_V1.LoadConfiguration();
            // TODO : test na radu dokladu ??? 
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select Cislo from OBJ where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleCommand.Connection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                    return false; // neexistuje cislo faktury
                else
                    return true; // existuje cislo faktury

            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }

        internal static bool PrjateObjednavky_JedenSklad(string cislo, string sklad)
        {
            Globals_V1.LoadConfiguration();
            System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            try
            {
                System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
                    "Select ID from OBJ where Cislo=?",
                    oleConnection
                    );
                oleCommand.Parameters.AddWithValue("?", cislo);
                oleConnection.Open();
                object o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    throw new Exception("Neexistuje číslo výdejky '" + cislo + "'");
                }

                int VYDEJKA_ID = (int)o; // toto musi byt ID Objednavky
                oleCommand.Parameters.Clear();
                oleCommand.CommandText =
                    "Select count(RefSklad) as JineSklady " +
                    "from SKz " +
                    "where ID in ( " +
                    "    select RefSKz " +
                    "    from OBJpol " +
                    "    where RefAg=? " +
                    "    ) " +
                    "and RefSklad <> " + sklad;
                oleCommand.Parameters.AddWithValue("?", VYDEJKA_ID);
                o = oleCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                {
                    return true;
                }
                else
                {
                    int pocetPolozekZJinychSkladu = (int)o;
                    if (pocetPolozekZJinychSkladu > 0)
                    {
                        throw new Exception("Výdejka '" + cislo + "' obsahuje položky z více skladů!");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    oleConnection.Close();
            }
        }


    }
}
