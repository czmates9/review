using Fask.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Database
{
    public class Vyroba_FASK_Vyroba_TP
    {


        #region Public metoda

        private static Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps = null;
        private static int _countEntries;
        private static string _SKL_ID;
        private static string _USER_ID;

        private static decimal PocetOdvedeno;
        //private static Guid ProductionGUID;

        internal static Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable GET_PS_from_TP(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow VPP_Row)
        {
            //1. Poznám CountEntries(číslo výrobního příkazu) a SKL_ID (Idenfikator skladu)

            //2. je potřeba dotahnot seznam z Production
            //3. Nasledne foreach projit všechny Vyrobky a k nim dotahnut Materialy
            //4. Dotahovane materialy cpat do tabulky ProductionSources
            //5. Použit rekurzivnu metodu z Vyroba_W

            dt_ps = new Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable();
            dt_ps.Clear();

            _countEntries = VPP_Row.CountEntries;


            //var dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);

            //if ((dt_p != null) && (dt_p.Count > 0))
            //{

            //foreach (var item in dt_p)
            //{
            //if (VPP_Row.IsTIMESTOPNull())
            //            continue;

            PocetOdvedeno = VPP_Row.QTYSHPPD;
            //ProductionGUID = VPP_Row.GUID;

            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky = GetDataBy_itemnmbrDef_IDHNULL(VPP_Row.ITEMNMBR);

            //VPP ITEMNMBR a podle toho hledam materialy
            if (vyrobky.Count > 0)
            {
                LoadMaterialy3(vyrobky);
            }
            //}
            //}
            //else
            //{
            //    return null;
            //}

            return dt_ps;

        }

        public static int Update(System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction transaction, string ITEMNMBR_fol, string DESC_Fol, string MJ_Fol, int ID)
        {

            using (var comm = conn.CreateCommand())
            {
                comm.Transaction = transaction;
                comm.CommandText = "UPDATE FASK_Vyroba_TP SET " +
                    " ITEMNMBR_fol = @ITEMNMBR_fol, " +
                    " DESC_Fol = @DESC_Fol, " +
                    " MJ_Fol = @MJ_Fol " +
                    " WHERE (ID = @ID)";


                comm.Parameters.Add(new SqlParameter()
                { ParameterName = "@ITEMNMBR_fol", DbType = DbType.String, SourceColumn = "ITEMNMBR_fol", Value = ITEMNMBR_fol == null ? (object)DBNull.Value : ITEMNMBR_fol });

                comm.Parameters.Add(new SqlParameter()
                { ParameterName = "@DESC_Fol", DbType = DbType.String, SourceColumn = "DESC_Fol", Value = DESC_Fol == null ? (object)DBNull.Value : DESC_Fol });

                comm.Parameters.Add(new SqlParameter()
                { ParameterName = "@MJ_Fol", DbType = DbType.String, SourceColumn = "MJ_Fol", Value = MJ_Fol == null ? (object)DBNull.Value : MJ_Fol });

                comm.Parameters.Add(new SqlParameter()
                { ParameterName = "@ID", DbType = DbType.Int32, SourceColumn = "ID", Value = ID  });


                comm.CommandType = CommandType.Text;

                return comm.ExecuteNonQuery();

            }

        }



        #endregion

        #region Private metody

        internal static Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable GetDataBy_itemnmbrDef_IDHNULL(string ITEMNMBR)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

            Globals_V1.LoadConfiguration();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                //da.SelectCommand.CommandTimeout
                da.SelectCommand.CommandText = "SELECT ITEMNMBR_Def, DESC_Def, MJ_Def, DESC_Fol, MJ_Fol, koef, dateedit, ID, ID_USER, ITEMNMBR_fol, ID_L, ID_H, [alter] FROM FASK_Vyroba_TP where ( itemnmbr_def= @ITEMNMBR ) and (id_h is null)";
                da.SelectCommand.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR);

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("SQL Dotaz GetDataBy_itemnmbrDef_IDHNULL", " Vyroba FASK_Vyroba_TP by ITEMNMBR ", ex);
                Fask.Logging.ExceptionHandler2.Handle(dt);
            }

            return dt;

        }

        internal static Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable GetDataByIDH(string ID_L)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

            Globals_V1.LoadConfiguration();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                //da.SelectCommand.CommandTimeout
                da.SelectCommand.CommandText = "SELECT ITEMNMBR_Def, DESC_Def, MJ_Def, DESC_Fol, MJ_Fol, koef, dateedit, ID, ID_USER, ITEMNMBR_fol, ID_L, ID_H, [alter] FROM FASK_Vyroba_TP where id_h=@id_h";

                da.SelectCommand.Parameters.AddWithValue("@id_h", ID_L);

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("SQL Dotaz GetDataByIDH", " Vyroba FASK_Vyroba_TP by  id_h ", ex);
                Fask.Logging.ExceptionHandler2.Handle(dt);
            }

            return dt;
        }

        private static void FillByITEMNMBR(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable dt_FASK_ZASOBY, string p)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMNMBR=@itemnmbr ";

                command.Parameters.AddWithValue("@itemnmbr", p);



                adapter.SelectCommand = command;
                adapter.Fill(dt_FASK_ZASOBY);

                //return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Rekurze

        /// <summary>
        /// pomoci rekurze ...
        /// </summary>
        private static void LoadMaterialy3(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky)
        {
            bool PriznakNacteneMaterialy;

            if (vyrobky.Count() == 0)
            {
                // TODO : upravit hlaseni error ... 
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Nenalezeno ... ");
            }
            else if (vyrobky.Count() == 1)
            {
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
            }
            else
            { // TODO : Vyber, ktery z vyrobku / variant vyrobku ... 
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
            }
        }

        #region Parametry materialu pro rekurzi ...
        /// <summary>
        /// Slouzi pro interni vypocty v ramci rekurzivniho pruchodu stromu TP
        /// </summary>
        private class materialParams
        {
            public materialParams()
            {
            }

            public materialParams(decimal koef)
            {
                this.KoeficientSet(koef);
            }

            public materialParams(materialParams mP, decimal koef)
            {
                if (mP == null)
                {
                    mP = new materialParams();
                }

                this.KoeficientSet(koef);
            }

            public void KoeficientSet(decimal koef)
            {
                this.KoeficientNadrazeny = koef;
                this.KoeficientKumulovany *= koef;
            }

            /// <summary>
            /// Koeficient nadrazeneho uzlu
            /// </summary>
            public decimal KoeficientNadrazeny = 1;
            /// <summary>
            /// Kumulovany koeficient od 1.uzlu az do aktualni urovne...
            /// </summary>
            public decimal KoeficientKumulovany = 1;
        }

        #endregion

        /// <summary>
        /// Rekurzivni volani a 
        /// </summary>
        /// <param name="rUp"></param>
        /// <returns></returns>
        private static bool isMaterial(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPRow rUp, materialParams materialparams)
        {

            // predavani parametru materialu z vyssi urovne ... 
            if (materialparams == null)
            {
                materialparams = new materialParams();
            }
            else
            {
                decimal koefUp = 1;
                try
                {

                    string tmpKoef = string.IsNullOrEmpty(rUp.koef) ? string.Empty : rUp.koef.Trim();

                    if (tmpKoef.Contains(","))
                        tmpKoef = tmpKoef.Replace(",", ".");

                    koefUp = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                { }

                materialparams = new materialParams(materialparams, koefUp);
            }

            if ((rUp == null) || (rUp.IsID_LNull()))
                return false;

            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable rDowns = GetDataByIDH(rUp.ID_L);


            if (rDowns.Count() == 0)
            {
                return true;
            }
            else
            {
                // alternace a jen nektere ... 
                // vyberu jestli jsou alternace na teto urovni .. 
                var rAlt = rDowns.GroupBy(x => x.IsalterNull() ? string.Empty : x.alter).OrderBy(x => x.Key);
                if (rAlt.Count() == 0)
                {
                    return false;
                }

                foreach (var alternace in rAlt)
                {
                    if (String.IsNullOrEmpty(alternace.Key))
                    {
                        foreach (var rDown in alternace)
                        {
                            bool isM = isMaterial(rDown, materialparams);

                            if (isM)
                            { // vlozim data
                                FillDatasetMaterialy(rDown, materialparams);
                            }
                        }
                    }
                    else
                    { // alternace

                        var RowFirst = alternace.ToArray().First();

                        throw new Exception("Data pro výdejku nelze připravit. Položka:" + RowFirst.DESC_Def + "(" + RowFirst.ITEMNMBR_Def + ")" + "' obsahuje alternace!");
                        //!!!! ALTERNACE NESMI NASTAT!!!!
                    }
                }
            }


            return false;
        }

        private static void FillDatasetMaterialy(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPRow rTP, materialParams materialparams)
        {
            //// TaD
            ////Pridani materialu do tabulky production sources
            //// na zaklade jednoh radku v tabulke FASK_Vyroba_TP
            ////


            Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable dt_FASK_ZASOBY = new Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable();

            FillByITEMNMBR(dt_FASK_ZASOBY, rTP.ITEMNMBR_fol);

            if ((dt_FASK_ZASOBY == null) || (dt_FASK_ZASOBY.Count == 0))
            {
                throw new Exception("Nebylo nalezeno zboží : '" + rTP.ITEMNMBR_Def + "'");
            }

            else if (dt_FASK_ZASOBY.Count > 1)
            {
                //Tato varianta by nemnela nastat
                throw new Exception("Bylo nalezeno vic řádku zboží : '" + rTP.ITEMNMBR_Def + "'");
            }




            // predavany parametr je tabulka zbozi materialu pridavaneho 

            _SKL_ID = null;
            _USER_ID = null;

            try
            {
                foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow i in dt_FASK_ZASOBY)
                {
                    Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row = dt_ps.NewProduction_SourcesRow();
                    //Data.VyrobaCEDataSet.Production_SourcesRow row = _productionSDT.NewProduction_SourcesRow();

                    row.CountEntries = _countEntries;
                    row.SOPNUMBE = string.Empty;
                    row.ITEMNAME = i.ITEMDESC.Trim();
                    row.ITEMNMBR = i.ITEMNMBR.Trim();
                    row.ITEMTYPE = string.Empty;

                    if (i.IsITEMCODENull())
                        row.SetITEMCODENull();
                    else
                        row.ITEMCODE = i.ITEMCODE.Trim();

                    row.MJ = i.MJ.Trim();

                    decimal mnozstvi = 1;
                    decimal koeficient = 1;
                    try
                    {
                        string tmpKoef = string.IsNullOrEmpty(rTP.koef) ? string.Empty : rTP.koef.Trim();

                        if (tmpKoef.Contains(","))
                            tmpKoef = tmpKoef.Replace(',', '.');

                        koeficient = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch { }
                    //mnozstvi = (decimal.Parse(rTP.koef) * PocetOdvedeno);
                    mnozstvi = materialparams.KoeficientKumulovany * koeficient * PocetOdvedeno;

                    row.QTYSHPPD = mnozstvi * (i.QTYPACK == 0 ? 1 : i.QTYPACK);
                    row.QTYSHPPDMJ = mnozstvi;
                    row.QTYPACK = i.QTYPACK;

                    row.SERLTNUM = string.Empty;
                    row.SKL_ID = _SKL_ID;
                    row.LOCNCODE = string.Empty;
                    row.GUID = Guid.NewGuid();
                    row.GUID_Production = Guid.NewGuid();
                    row.USER_ID = _USER_ID;
                    row.TERMINAL_ID = 9999;
                    row.NMBRPAL = string.Empty;
                    row.TYPEPAL = string.Empty;
                    row.PRINTED = 0;

                    dt_ps.AddProduction_SourcesRow(row);
                }



            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                throw ex;
            }
        }


        #endregion


        #endregion

        public static void DeleteMaterial(string CS, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM FASK_Vyroba_TP WHERE (ID = @ID)";

                        comm.Parameters.AddWithValue("@ID", ID);

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public static void DeleteVyrobek(string CS, string ITEMNMBR)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM FASK_Vyroba_TP WHERE (ITEMNMBR_Def = @ITEMNMBR)";

                        comm.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR);

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }
    }
}
