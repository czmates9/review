using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Výdej.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Vydej.IVydej
    {

        private string TABLE_CZMST_SE = "CZMST_SE";
        private string TABLE_CZMST_SI = "CZMST_SI";
        private string TABLE_CZMST_SE_SN = "CZMST_SE_SN";
        private string TABLE_CZMST_SIH = "CZMST_SIH";
        /*
        private System.Data.SqlClient.SqlCommand CreateSEUpdate(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand updtComSE = new System.Data.SqlClient.SqlCommand();
            updtComSE.CommandText = "UPDATE " + TABLE_CZMST_SE +
                    " SET CZ_Doslo = @CZ_Doslo " +
                    " WHERE (DEX_ROW_ID = @DEX_ROW_ID) " +
                    " AND (CountEntries = @CountEntries)" +
                    "";
            updtComSE.Parameters.Add("@CZ_Doslo", SqlDbType.TinyInt, 1, "CZ_Doslo");
            updtComSE.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DEX_ROW_ID", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DEX_ROW_ID", System.Data.DataRowVersion.Original, null));
            updtComSE.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountEntries", SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CountEntries", System.Data.DataRowVersion.Original, null));
            updtComSE.Connection = conn;
            return updtComSE;
        }

        private System.Data.SqlClient.SqlCommand CreateSESelect(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand selComSE = new System.Data.SqlClient.SqlCommand();
            selComSE.CommandText = "SELECT CountEntries, CZ_Doslo, DEX_ROW_ID FROM " + TABLE_CZMST_SE;
            selComSE.Connection = conn;
            return selComSE;
        }

        private void CreateSEAdapterTableMAp(System.Data.SqlClient.SqlDataAdapter xDataAdapterSE)
        {
            xDataAdapterSE.TableMappings.AddRange(
                new System.Data.Common.DataTableMapping[] {
                    new System.Data.Common.DataTableMapping("Table", "CZMST_SE", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("CountEntries", "CountEntries"),
                        new System.Data.Common.DataColumnMapping("CZ_Doslo", "CZ_Doslo"),
                        new System.Data.Common.DataColumnMapping("DEX_ROW_ID", "DEX_ROW_ID")
                    })
                });

        }

        private SqlCommand createSIHDelet(SqlConnection conn)
        {

            System.Data.SqlClient.SqlCommand xDeleteCommandSIH = new System.Data.SqlClient.SqlCommand();
            xDeleteCommandSIH.CommandText =
     "DELETE FROM " + TABLE_CZMST_SIH +
     " WHERE (CountEntries = @CountEntries)";
            xDeleteCommandSIH.Connection = conn;
            xDeleteCommandSIH.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountEntries", SqlDbType.Int, 4, "CountEntries"));

            return xDeleteCommandSIH;
        }

        private void CreateSIAdapterTableMap(System.Data.SqlClient.SqlDataAdapter xDataAdapterSI)
        {

            xDataAdapterSI.TableMappings.AddRange(
   new System.Data.Common.DataTableMapping[] {
                    new System.Data.Common.DataTableMapping("Table", "CZMST_SI", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("CountEntries", "CountEntries"),
                        new System.Data.Common.DataColumnMapping("SOPNUMBE", "SOPNUMBE"),
                        new System.Data.Common.DataColumnMapping("ITEMNMBR", "ITEMNMBR"),
                        new System.Data.Common.DataColumnMapping("ORD", "ORD"),
                        new System.Data.Common.DataColumnMapping("VNDDOCNM", "VNDDOCNM"),
                        new System.Data.Common.DataColumnMapping("VNDITNUM", "VNDITNUM"),
                        new System.Data.Common.DataColumnMapping("CZ_CarKod", "CZ_CarKod"),
                        new System.Data.Common.DataColumnMapping("LOCNCODE", "LOCNCODE"),
                        new System.Data.Common.DataColumnMapping("QTYSHPPD", "QTYSHPPD"),
                        new System.Data.Common.DataColumnMapping("QTYPACK", "QTYPACK"),
                        new System.Data.Common.DataColumnMapping("SERLTNUM", "SERLTNUM"),
                        new System.Data.Common.DataColumnMapping("KOD_SW", "KOD_SW"),
                        new System.Data.Common.DataColumnMapping("DAT_VYROBY", "DAT_VYROBY"),
                        new System.Data.Common.DataColumnMapping("REZ_1", "REZ_1"),
                        new System.Data.Common.DataColumnMapping("ODBER_ID", "ODBER_ID"),
                        new System.Data.Common.DataColumnMapping("DATEDONE", "DATEDONE"),
                        new System.Data.Common.DataColumnMapping("TIMEDONE", "TIMEDONE"),
                        new System.Data.Common.DataColumnMapping("USER_ID", "USER_ID"),
                        new System.Data.Common.DataColumnMapping("DEX_ROW_ID", "DEX_ROW_ID"),
                        //nove
                        new System.Data.Common.DataColumnMapping("TYPEPAL", "TYPEPAL"),
                        new System.Data.Common.DataColumnMapping("NMBRPAL", "NMBRPAL"),
                        new System.Data.Common.DataColumnMapping("PRINTED", "PRINTED"),
                        new System.Data.Common.DataColumnMapping("GUID", "GUID"),
                        new System.Data.Common.DataColumnMapping("REZ_2", "REZ_2"),
                        new System.Data.Common.DataColumnMapping("INPUT_MODE", "INPUT_MODE"),
                        new System.Data.Common.DataColumnMapping("ID_TERMINAL", "ID_TERMINAL")
                    })
                });
        }

        private System.Data.SqlClient.SqlCommand createSIInsert(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand inserSICom = new System.Data.SqlClient.SqlCommand();
            inserSICom.CommandText =
                   @"INSERT INTO " + TABLE_CZMST_SI +
                   " (CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDDOCNM, VNDITNUM, CZ_CarKod, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, REZ_1, ODBER_ID, DATEDONE, TIMEDONE, USER_ID" +
                   (Properties.Settings.Default.DexRowIdInsert ? ", DEX_ROW_ID" : "") + ", TYPEPAL, NMBRPAL, PRINTED" +
                   ", GUID" +
                   ", REZ_2, INPUT_MODE, ID_TERMINAL" +
                   ") VALUES (@CountEntries, @SOPNUMBE, @ITEMNMBR, @ORD, @VNDDOCNM, @VNDITNUM, @CZ_CarKod, @LOCNCODE, @QTYSHPPD, @QTYPACK, @SERLTNUM, @KOD_SW, @DAT_VYROBY, @REZ_1, @ODBER_ID, @DATEDONE, @TIMEDONE, @USER_ID" +
                   (Properties.Settings.Default.DexRowIdInsert ? ", @DEX_ROW_ID" : "") + ", @TYPEPAL, @NMBRPAL, @PRINTED" +
                   ", @GUID" +
                   ", @REZ_2, @INPUT_MODE, @ID_TERMINAL" +
                   ")";

            inserSICom.Connection = conn;

            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountEntries", SqlDbType.Int, 4, "CountEntries"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOPNUMBE", SqlDbType.VarChar, 17, "SOPNUMBE"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ITEMNMBR", SqlDbType.VarChar, 31, "ITEMNMBR"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ORD", SqlDbType.Int, 4, "ORD"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VNDDOCNM", SqlDbType.VarChar, 21, "VNDDOCNM"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VNDITNUM", SqlDbType.VarChar, 31, "VNDITNUM"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CZ_CarKod", SqlDbType.VarChar, 31, "CZ_CarKod"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LOCNCODE", SqlDbType.VarChar, 11, "LOCNCODE"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@QTYSHPPD", SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(18)), ((System.Byte)(5)), "QTYSHPPD", System.Data.DataRowVersion.Current, null));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@QTYPACK", SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(18)), ((System.Byte)(5)), "QTYPACK", System.Data.DataRowVersion.Current, null));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SERLTNUM", SqlDbType.VarChar, 21, "SERLTNUM"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KOD_SW", SqlDbType.VarChar, 11, "KOD_SW"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DAT_VYROBY", SqlDbType.VarChar, 11, "DAT_VYROBY"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@REZ_1", SqlDbType.VarChar, 15, "REZ_1"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ODBER_ID", SqlDbType.VarChar, 12, "ODBER_ID"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DATEDONE", SqlDbType.VarChar, 8, "DATEDONE"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TIMEDONE", SqlDbType.VarChar, 6, "TIMEDONE"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", SqlDbType.Int, 4, "USER_ID"));

            if (Properties.Settings.Default.DexRowIdInsert)
            {
                inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DEX_ROW_ID", SqlDbType.Int, 4, "DEX_ROW_ID"));
            }

            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TYPEPAL", SqlDbType.NChar, 4, "TYPEPAL"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NMBRPAL", SqlDbType.NVarChar, 20, "NMBRPAL"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRINTED", SqlDbType.Bit, 1, "PRINTED"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID", SqlDbType.UniqueIdentifier, 4, "GUID"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@REZ_2", SqlDbType.VarChar, 20, "REZ_2"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@INPUT_MODE", SqlDbType.TinyInt, 1, "INPUT_MODE"));
            inserSICom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID_TERMINAL", SqlDbType.Int, 4, "ID_TERMINAL"));

            return inserSICom;
        }

        private System.Data.SqlClient.SqlCommand createSIHInsert(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand xInsertCommandSIH = new System.Data.SqlClient.SqlCommand();
            xInsertCommandSIH.CommandText =
                    @"INSERT INTO " + TABLE_CZMST_SIH +
                    " (CountEntries, TISKARNA_NAME, PRAC_ID) " +
                    " VALUES (?, ?, ?)";

            xInsertCommandSIH.Connection = conn;

            xInsertCommandSIH.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountEntries", SqlDbType.Int, 4, "CountEntries"));
            xInsertCommandSIH.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TISKARNA_NAME", SqlDbType.NVarChar, 30, "TISKARNA_NAME"));
            xInsertCommandSIH.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRAC_ID", SqlDbType.NVarChar, 30, "PRAC_ID"));

            return xInsertCommandSIH;
        }

        private System.Data.SqlClient.SqlCommand createSIHSelect(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand xSelectCommandSIH = new System.Data.SqlClient.SqlCommand();
            xSelectCommandSIH.CommandText = "SELECT CountEntries, TISKARNA_NAME, PRAC_ID FROM " + TABLE_CZMST_SIH;
            xSelectCommandSIH.Connection = conn;

            return xSelectCommandSIH;
        }

        private System.Data.SqlClient.SqlCommand createSISelect(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand xSelectCommandSI = new System.Data.SqlClient.SqlCommand();
            xSelectCommandSI.CommandText =
"SELECT CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDDOCNM, VNDITNUM, CZ_CarKod, LOCN" +
"CODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, REZ_1, ODBER_ID, DATEDONE" +
", TIMEDONE, USER_ID, DEX_ROW_ID, TYPEPAL, NMBRPAL, PRINTED" +
" GUID, REZ_2, INPUT_MODE, ID_TERMINAL " +
" FROM " + TABLE_CZMST_SI;
            xSelectCommandSI.Connection = conn;

            return xSelectCommandSI;
        }

        private System.Data.SqlClient.SqlCommand createSIDelete(System.Data.SqlClient.SqlConnection conn)
        {
            System.Data.SqlClient.SqlCommand xDeleteCommandSI = new System.Data.SqlClient.SqlCommand();
            xDeleteCommandSI.CommandText =
                    "DELETE FROM " + TABLE_CZMST_SI +
                    " WHERE (GUID = @GUID)";
            xDeleteCommandSI.Connection = conn;

            xDeleteCommandSI.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID", SqlDbType.UniqueIdentifier, 4, "GUID"));

            return xDeleteCommandSI;
        }
        */

        #region IVydej Members

        public Fask.Server.Interfaces.Classes.StatusInfo Vydej_GenerateDavka(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
          
            Globals.LoadConfiguration();
            StatusInfo si = new StatusInfo();
            si.Description = "Vydej_GenerateDavka start";
            si.ID = 0;


            if (objednavka.ID == "prelokovani")
            {
                //logika
                Globals.LoadConfiguration();


                string stav = Classes.Vydej.Export_Prelokovani_SQL_Vydej(objednavka, sklad);

                //rozhodnuti na vysledny stav
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    if (!string.IsNullOrEmpty(objednavka.CisloDavky) && int.TryParse(objednavka.CisloDavky, out int id))
                    {
                        si.ID = id;
                    }
                    else
                    {
                        si.ID = -1; // nebo jiná defaultní hodnota / error handling
                    }

                    //25.9.2025 MaR zakomentoval aby nehazelo vyjimku
                    //si.ID = int.Parse(objednavka.CisloDavky);

                    si.Description = "OK";
                    si.InnerException = null;
                }

            }
            // 2) nepodporovany typ transakce
            else
            {
                //si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
                si.Description = "Transakce '" + objednavka.ID + "' nenalezena.";
                if (sklad != null)
                    si.Description += "\nSklad " + sklad.ID;
                si.InnerException = new Exception(si.Description);
                si.ID = -10;
                throw new Exception(si.Description);
            }

            return si;





        }

        public Fask.DataSets.Vydejky Vydej_GetVydejky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, Fask.Server.Interfaces.Classes.User user)
        {
            try
            {
                Globals.LoadConfiguration();

                string select = string.Empty;
                if (item.Type == "X" || item.Type == "x")
                {
                    item.Type = string.Empty;

                    select =
                    " SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
                    //" SELECT distinct A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
                    " FROM " + TABLE_CZMST_SE + " A " +
                    " INNER JOIN " +
                    " ( " +
                    "	SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
                    "	FROM ( " +
                    "		SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                    "		FROM " + TABLE_CZMST_SE + " " +
                    "       WHERE QTYPACK=0" +
                    "       AND " +
                    "       SKL_ID LIKE '" + SQLInjection.Filter(sklad.ID) + "%' " +
                    "       AND " +
                    "       ITEMTYPE LIKE '" + item.Type + "' " +
                    "       AND " +
                    "       (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
                    "		GROUP BY COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                    "	) X " +
                    "	GROUP BY X.COUNTENTRIES, X.SOPNUMBE " +
                    " ) B ON  " +
                    " B.COUNTENTRIES=A.COUNTENTRIES " +
                    " AND B.SOPNUMBE=A.SOPNUMBE " +
                    " INNER JOIN " +
                    " ( " +
                    "	SELECT COUNTENTRIES, SOPNUMBE, " +
                    "       SUM(QTYSHPPD) AS QTYSHPPDSUM" +
                    "	FROM " + TABLE_CZMST_SE + " " +
                    "   WHERE QTYPACK=0" +
                    "   AND " +
                    "   SKL_ID LIKE '" + SQLInjection.Filter(sklad.ID) + "%' " +
                    "   AND " +
                    "   ITEMTYPE LIKE '" + item.Type + "' " +
                    "   AND " +
                    "   (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
                    "	GROUP BY COUNTENTRIES, SOPNUMBE " +
                    " ) C ON " +
                    " C.COUNTENTRIES=A.COUNTENTRIES  " +
                    " AND C.SOPNUMBE=A.SOPNUMBE " +
                    " LEFT OUTER JOIN " +
                    " ( " +
                    "   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM " + TABLE_CZMST_SI +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE " +
                    " ) D ON " +
                    " D.COUNTENTRIES=A.COUNTENTRIES " +
                    " and D.SOPNUMBE=A.SOPNUMBE " +
                    //konec rozpracovanosti vydejky
                    " WHERE " +
                    //" A.CountEntries not in (Select CountEntries from " + TABLE_CZMST_SI + ") " +
                    //" and " +
                    //" A.CZ_Doslo<=0 " + 
                    " (A.CZ_DOSLO<=0 OR A.CZ_DOSLO=" + terminal.ID + ")" +
                    " AND " +
                    " A.SKL_ID LIKE '" + SQLInjection.Filter(sklad.ID) + "%' " +
                    " AND " +
                    " A.ITEMTYPE LIKE '" + item.Type + "' " +
                    " AND " +
                    " (A.USERID=" + user.ID + " OR A.USERID IS NULL) " +
                    " GROUP BY A.PRIORITY, A.COUNTENTRIES, A.SOPNUMBE, B.CNTITEMS, C.QTYSHPPDSUM, D.ROZPRACOVANO " +
                    (Globals.Konfigurace.Vydej[0].SeznamDavekRazeni == string.Empty ? Globals.Konfigurace.Vydej[0].SeznamDavekRazeni : " ORDER BY " + Globals.Konfigurace.Vydej[0].SeznamDavekRazeni);

                }
                else
                {
                    select =
                                        " SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
                                        //" SELECT distinct A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
                                        " FROM " + TABLE_CZMST_SE + " A " +
                                        " INNER JOIN " +
                                        " ( " +
                                        "	SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
                                        "	FROM ( " +
                                        "		SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                                        "		FROM " + TABLE_CZMST_SE + " " +
                                        "       WHERE QTYPACK=0" +
                                        "       AND " +
                                        "       SKL_ID LIKE '" + SQLInjection.Filter(sklad.ID) + "%' " +
                                        "       AND " +
                                        "       ITEMTYPE LIKE '" + SQLInjection.Filter(item.Type) + "%' " +
                                        "       AND " +
                                        "       (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
                                        "		GROUP BY COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                                        "	) X " +
                                        "	GROUP BY X.COUNTENTRIES, X.SOPNUMBE " +
                                        " ) B ON  " +
                                        " B.COUNTENTRIES=A.COUNTENTRIES " +
                                        " AND B.SOPNUMBE=A.SOPNUMBE " +
                                        " INNER JOIN " +
                                        " ( " +
                                        "	SELECT COUNTENTRIES, SOPNUMBE, " +
                                        "       SUM(QTYSHPPD) AS QTYSHPPDSUM" +
                                        "	FROM " + TABLE_CZMST_SE + " " +
                                        "   WHERE QTYPACK=0" +
                                        "   AND " +
                                        "   SKL_ID LIKE '" + SQLInjection.Filter(sklad.ID) + "%' " +
                                        "   AND " +
                                        "   ITEMTYPE LIKE '" + SQLInjection.Filter(item.Type) + "%' " +
                                        "   AND " +
                                        "   (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
                                        "	GROUP BY COUNTENTRIES, SOPNUMBE " +
                                        " ) C ON " +
                                        " C.COUNTENTRIES=A.COUNTENTRIES  " +
                                        " AND C.SOPNUMBE=A.SOPNUMBE " +
                                        " LEFT OUTER JOIN " +
                                        " ( " +
                                        "   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM " + TABLE_CZMST_SI +
                                        "   GROUP BY COUNTENTRIES, SOPNUMBE " +
                                        " ) D ON " +
                                        " D.COUNTENTRIES=A.COUNTENTRIES " +
                                        " and D.SOPNUMBE=A.SOPNUMBE " +
                                        //konec rozpracovanosti vydejky
                                        " WHERE " +
                                        //" A.CountEntries not in (Select CountEntries from " + TABLE_CZMST_SI + ") " +
                                        //" and " +
                                        //" A.CZ_Doslo<=0 " + 
                                        " (A.CZ_DOSLO<=0 OR A.CZ_DOSLO=" + terminal.ID + ")" +
                                        " AND " +
                                        " A.SKL_ID LIKE '" + SQLInjection.Filter(sklad.ID) + "%' " +
                                        " AND " +
                                        " A.ITEMTYPE LIKE '" + SQLInjection.Filter(item.Type) + "%' " +
                                        " AND " +
                                        " (A.USERID=" + user.ID + " OR A.USERID IS NULL) " +
                                        " GROUP BY A.PRIORITY, A.COUNTENTRIES, A.SOPNUMBE, B.CNTITEMS, C.QTYSHPPDSUM, D.ROZPRACOVANO " +
                                        (Globals.Konfigurace.Vydej[0].SeznamDavekRazeni == string.Empty ? Globals.Konfigurace.Vydej[0].SeznamDavekRazeni : " ORDER BY " + Globals.Konfigurace.Vydej[0].SeznamDavekRazeni);

                }






                System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB);


                Fask.DataSets.Vydejky volnevydejky = new Fask.DataSets.Vydejky();
                dataAdapter.Fill(volnevydejky, volnevydejky.Hlavicky.TableName);

                #region Rozsireni o dotazeni informace do infa z existujiciho pohledu detailu. vic neni mozne
                try
                {
                    if (Globals.Konfigurace.Vydej[0].Info1Allow)
                    {
                        foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
                        {
                            try
                            {
                                Fask.Server.Interfaces.Classes.Objednavka obj = new Fask.Server.Interfaces.Classes.Objednavka();
                                obj.ID = hrow.SOPNUMBE;
                                DataSet ds = Vydej_Detail(obj);

                                hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][Globals.Konfigurace.Vydej[0].Info1Field]);

                               //hrow.Info1 = "MaR test";
                            }
                            catch (Exception ex)
                            {
                                //Log.writeErrorLog(ex.Message);
                                throw ex;
                              
                            }
                        }
                    }
                    else if (Globals.Konfigurace.Vydej[0].VydejkaDetail2Hlavicka)
                    {
                        //detail dle cisla cisla objednavky
                        DataSet dsdetail = Vydej_Detail(new Fask.Server.Interfaces.Classes.Objednavka());
                        if (dsdetail != null)
                        {
                            if (dsdetail.Tables.Count > 0)
                            {
                                //DataColumn[] dcols = new DataColumn[ds.Tables[0].Columns.Count];
                                //ds.Tables[0].Columns.CopyTo(dcols, 0);
                                //volnevydejky.Hlavicky.Columns.AddRange(dcols);
                                foreach (DataColumn dcol in dsdetail.Tables[0].Columns)
                                {
                                    volnevydejky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                                }
                            }

                            if (volnevydejky.Hlavicky.Count > 0)
                            {
                                foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
                                {
                                    try
                                    {
                                        Fask.Server.Interfaces.Classes.Objednavka obj = new Fask.Server.Interfaces.Classes.Objednavka();
                                        obj.ID = hrow.SOPNUMBE;
                                        dsdetail = Vydej_Detail(obj);
                                        //hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][vydejInfo1field]);
                                        foreach (DataColumn dcol in dsdetail.Tables[0].Columns)
                                        {
                                            DataColumn dcolhrow = hrow.Table.Columns[dcol.ColumnName];
                                            hrow.SetField<object>(dcolhrow, dsdetail.Tables[0].Rows[0][dcol]);
                                        }
                                    }
                                    catch
                                    { }
                                }
                            }
                        }
                       

                        //detail dle cisla cisla davky
                        DataSet dsdetaildavka = Vydej_DetailDavka(new Fask.Server.Interfaces.Classes.Davka());
                        if (dsdetaildavka != null)
                        {
                            if (dsdetaildavka.Tables.Count > 0)
                            {
                                //DataColumn[] dcols = new DataColumn[ds.Tables[0].Columns.Count];
                                //ds.Tables[0].Columns.CopyTo(dcols, 0);
                                //volnevydejky.Hlavicky.Columns.AddRange(dcols);
                                foreach (DataColumn dcol in dsdetaildavka.Tables[0].Columns)
                                {
                                    volnevydejky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                                }
                            }

                            if (volnevydejky.Hlavicky.Count > 0)
                            {
                                foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
                                {
                                    try
                                    {
                                        Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                                        davka.ID = int.Parse(hrow.CountEntries);
                                        dsdetaildavka = Vydej_DetailDavka(davka);
                                        //hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][vydejInfo1field]);
                                        foreach (DataColumn dcol in dsdetaildavka.Tables[0].Columns)
                                        {
                                            DataColumn dcolhrow = hrow.Table.Columns[dcol.ColumnName];
                                            hrow.SetField<object>(dcolhrow, dsdetaildavka.Tables[0].Rows[0][dcol]);
                                        }
                                    }
                                    catch
                                    { }
                                }
                            }
                        }

                        volnevydejky.AcceptChanges();
                    }
                    else
                    {
                        //hodnota ze selectu s parametrem SOPNUMBER vstup, vystup textove pole

                        foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
                        {
                            try
                            {
                                
                                if (string.IsNullOrEmpty( Globals.Konfigurace.Vydej[0].VydejkaDetail))
                                    break;

                                string dataText = string.Empty;


                                //logika vyberu parametru
                                //z hrow vybrat jmeno sloupce a porovnat s oriznutym 1.znak a mala pismena v promenne: Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName

                                string paramName = Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName;

                                // Trim the first character and convert to lowercase
                                string trimmedParamName = paramName.Substring(1)/*.ToLower()*/;

                                
                                if (hrow.Table.Columns.Contains(trimmedParamName))
                                {
                                    string columnValue = hrow[trimmedParamName]?.ToString();
                                    dataText = Vydej_Detail_text(columnValue);
                                }
                                hrow.Info1 = dataText;


                                #region old
                                //if (Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName == "CountEntries")
                                //{
                                //    dataText = Vydej_Detail_text(hrow.CountEntries);
                                //}
                                //else if (Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName == "SOPNUMBE")
                                //{
                                //    dataText = Vydej_Detail_text(hrow.SOPNUMBE);
                                //} 
                                #endregion
                                // Fask.Server.Interfaces.Classes.Objednavka obj = new Fask.Server.Interfaces.Classes.Objednavka();
                                // obj.ID = hrow.SOPNUMBE;


                                // hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][Globals.Konfigurace.Vydej[0].Info1Field]);


                            }
                            catch (Exception ex)
                            {
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "ParamName:"+ Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName + " neodpovida parametru vstupu do SQL");
                                //throw new Exception("Chyba při získávání detailu z databáze.", ex);
                                //throw ex;

                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    //Log.writeErrorLog(ex.Message);
                    throw ex;
                }
                #endregion

                volnevydejky.AcceptChanges();

                return volnevydejky;
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog(ex.Message + "(Sklad: " + prefixskladu + ")");
                throw ex;
            }
        }

        #region MaR 3.9.2024 stara metody plneni dat vydejky

        //public Fask.DataSets.Vydej Vydej_GetVydejka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        //{
        //    try
        //    {
        //        Globals.LoadConfiguration();

        //        string select = string.Empty;
        //        if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
        //        {
        //            select = "SELECT * FROM " + TABLE_CZMST_SE +
        //            " where CountEntries=" + davka.ID +
        //            " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //            " order by dex_row_id" +
        //            "";
        //        }
        //        else
        //        {
        //            select = "SELECT * FROM " + TABLE_CZMST_SE +
        //            " where CountEntries=" + davka.ID +
        //            " and ITEMTYPE like '" + SQLInjection.Filter(item.Type) + "%' " +
        //            " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //            " order by dex_row_id" +
        //            "";
        //        }


        //        System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB));

        //        Fask.DataSets.Vydej vydej = new Fask.DataSets.Vydej();

        //        xda.Fill(vydej, vydej.CZMST_SE.TableName);

        //        foreach (Fask.DataSets.Vydej.CZMST_SERow sr in vydej.CZMST_SE)
        //        {
        //            sr.CZ_Doslo = (byte)terminal.ID;

        //            if (sr.ITEMTYPE.Trim() == "P")
        //            {

        //                //update zaznam v PE
        //                //var y = serow.CountEntries;
        //                //var x =  serow.CZ_Doslo;

        //                Database.Prijem.UpdateCzDosloByCountEntries(sr.CZ_Doslo, sr.CountEntries);

        //                //dohledat zaznam v tabhulce PE a zmenit CZ_doslo

        //            }
        //            else if (sr.ITEMTYPE.Trim() == "I")
        //            {

        //                //update zaznam v PE
        //                //var y = serow.CountEntries;
        //                //var x =  serow.CZ_Doslo;

        //                Database.Inventura.UpdateCzDosloByCountEntries(sr.CZ_Doslo, sr.CountEntries);

        //                //dohledat zaznam v tabhulce PE a zmenit CZ_doslo

        //            }


        //        }

        //        // Dotazeni dat z SI => pouze v pripade, ze se nepouziva vychystavani vice terminaly!!!
        //        // Slouzi k poslani davky k pozdejsimu zpracovani na jinem terminalu ...
        //        if (item.Type.Trim().Length == 0)
        //        {
        //            string selectSI =
        //                "SELECT * FROM " + TABLE_CZMST_SI +
        //                " where CountEntries=" + davka.ID;

        //            System.Data.SqlClient.SqlDataAdapter xdaSI = new System.Data.SqlClient.SqlDataAdapter(selectSI, new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB));
        //            xdaSI.Fill(vydej, vydej.CZMST_SI.TableName);
        //        }

        //        //Dotazeni predlohy seriovych cisel
        //        try
        //        {
        //            string selectSESN =
        //                "Select * from " + TABLE_CZMST_SE_SN +
        //                " where CountEntries=" + davka.ID;


        //            System.Data.SqlClient.SqlDataAdapter xdaSESN = new System.Data.SqlClient.SqlDataAdapter(selectSESN, new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB));
        //            xdaSESN.Fill(vydej, vydej.CZMST_SE_SN.TableName);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return vydej;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region MaR 3.9.2024 nova metoda plneni dat vydejky
        public Fask.DataSets.Vydej Vydej_GetVydejka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start(); 

            try
            {

                //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 0");


                Globals.LoadConfiguration();

                string select = "SELECT * FROM " + TABLE_CZMST_SE +
                                " WHERE CountEntries = @CountEntries " +
                                " AND (cz_doslo <= 0 OR cz_doslo = @TerminalID)";

                if (!Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                {
                    select += " AND ITEMTYPE LIKE @ItemType ";
                }

                select += " ORDER BY dex_row_id";

              //  Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 1");

                using (var connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                using (var command = new System.Data.SqlClient.SqlCommand(select, connection))
                using (var adapter = new System.Data.SqlClient.SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@CountEntries", davka.ID);
                    command.Parameters.AddWithValue("@TerminalID", terminal.ID);
                    if (!Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                    {
                        command.Parameters.AddWithValue("@ItemType", SQLInjection.Filter(item.Type) + "%");
                    }

                    Fask.DataSets.Vydej vydej = new Fask.DataSets.Vydej();
                    //    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 2");

                    connection.Open();
                    adapter.Fill(vydej, vydej.CZMST_SE.TableName);

                    //    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 3");

                    foreach (var sr in vydej.CZMST_SE)
                    {
                        sr.CZ_Doslo = (byte)terminal.ID;

                      
                                    
                    }

                    //    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 3a");

                    var filterdatagrouped = vydej.CZMST_SE.Select(x => new { x.CountEntries, x.ITEMTYPE, x.CZ_Doslo }).GroupBy(y => new { y.CountEntries, y.ITEMTYPE, y.CZ_Doslo });

                    //   Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 3b");

                    foreach (var group in filterdatagrouped)
                    {
                        if (group.Key.ITEMTYPE.Trim() == "P")
                        {
                            Database.Prijem.UpdateCzDosloByCountEntries(group.Key.CZ_Doslo, group.Key.CountEntries);
                        }
                        else if (group.Key.ITEMTYPE.Trim() == "I")
                        {
                            Database.Inventura.UpdateCzDosloByCountEntries(group.Key.CZ_Doslo, group.Key.CountEntries);
                        }
                    }
                    //   Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 3c");

                    //   Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 4");

                    if (item.Type.Trim().Length == 0)
                    {
                        string selectSI = "SELECT * FROM " + TABLE_CZMST_SI + " WHERE CountEntries = @CountEntries";
                        using (var commandSI = new System.Data.SqlClient.SqlCommand(selectSI, connection))
                        using (var adapterSI = new System.Data.SqlClient.SqlDataAdapter(commandSI))
                        {
                            commandSI.Parameters.AddWithValue("@CountEntries", davka.ID);
                            adapterSI.Fill(vydej, vydej.CZMST_SI.TableName);
                        }
                    }

                    //   Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 5");

                    try
                    {
                        string selectSESN = "SELECT * FROM " + TABLE_CZMST_SE_SN + " WHERE CountEntries = @CountEntries";
                        using (var commandSESN = new System.Data.SqlClient.SqlCommand(selectSESN, connection))
                        using (var adapterSESN = new System.Data.SqlClient.SqlDataAdapter(commandSESN))
                        {
                            commandSESN.Parameters.AddWithValue("@CountEntries", davka.ID);
                            adapterSESN.Fill(vydej, vydej.CZMST_SE_SN.TableName);
                        }

                    }
                    catch (Exception ex)
                    {
                        // Log the exception (avoid rethrowing without handling)
                        // Logger.Error(ex);
                        throw;
                    }

                    //   Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 6");

                    return vydej;
                }

                //  Logging.ExceptionHandler2.Handle(Logging.LogLevel.Trace, nameof(Vydej_GetVydejka) + " : " + sw.ElapsedMilliseconds + " : 7");

            }
            catch (Exception ex)
            {
                // Consider logging the exception instead of just rethrowing it
                // Logger.Error(ex);
                throw;
            }
        }


        #endregion

        #region 15.9.2025 MaR predelani na podminku inventury
        //public bool Vydej_GetVydejkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        //{
        //    Globals.LoadConfiguration();

        //    string selectCount = string.Empty;
        //    string update = string.Empty;
        //    string inventura = "I";

        //    ////15.9.2025 pridani podminky DavkaTerminalVice
        //    ////kdyz bude itemType == I a soucasne v sekci konfiguracniho souboru sekci Inventura prvek DavkaTerminalVice == true, nezmeni CZDoslo z 0 na cislo terminalu, zustane zachovany..
        //     //if (item.Type == "I" && Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
        //    if (Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
        //    {
        //        if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_SE +
        //                " where CountEntries=" + davka.ID +
        //                //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";

        //        }
        //        else
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_SE +
        //                " where CountEntries=" + davka.ID +
        //                " and ITEMTYPE like '" + SQLInjection.Filter(item.Type) + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";


        //        }

        //        SqlConnection xconn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
        //        SqlCommand xcommAllowed = new SqlCommand(selectCount, xconn);

        //        try
        //        {
        //            xconn.Open();
        //            object r = xcommAllowed.ExecuteScalar();
        //            if (r == null)
        //                throw new Exception("Dávka nenalezena.");
        //            if (r is int && ((int)r) <= 0)
        //                throw new Exception("Dávka se již zpracovává.");


        //        }
        //        catch (Exception ex)
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Dávka: " + davka.ID.Value.ToString() + ", Terminál ID:" + terminal.ID.ToString() + ", ITEMTYPE:" + item.Type + ")");
        //            Fask.Logging.ExceptionHandler2.Handle(ex);
        //            //throw ex;
        //            return false;
        //        }
        //        finally
        //        {
        //            if (xconn.State == ConnectionState.Open)
        //                xconn.Close();
        //        }

        //        //nic se nenastavuje
        //        return true;
        //    }
        //    else
        //    {
        //        if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_SE +
        //                " where CountEntries=" + davka.ID +
        //                //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";

        //            update =
        //                "Update " + TABLE_CZMST_SE +
        //                " set cz_doslo=" + terminal.ID +
        //                " where countentries=" + davka.ID +
        //                //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
        //                " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";
        //        }
        //        else
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_SE +
        //                " where CountEntries=" + davka.ID +
        //                " and ITEMTYPE like '" + SQLInjection.Filter(item.Type) + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";

        //            update =
        //                "Update " + TABLE_CZMST_SE +
        //                " set cz_doslo=" + terminal.ID +
        //                " where countentries=" + davka.ID +
        //                " and ITEMTYPE like '" + SQLInjection.Filter(item.Type) + "%' " +
        //                " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";
        //        }

        //        SqlConnection xconn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
        //        SqlCommand xcommAllowed = new SqlCommand(selectCount, xconn);
        //        SqlCommand xcomm = new SqlCommand(update, xconn);

        //        SqlTransaction itrans = null;

        //        int res = 0;
        //        try
        //        {
        //            xconn.Open();
        //            itrans = xconn.BeginTransaction(IsolationLevel.Serializable);

        //            xcommAllowed.Transaction = itrans;
        //            object r = xcommAllowed.ExecuteScalar();
        //            if (r == null)
        //                throw new Exception("Dávka nenalezena.");
        //            if (r is int && ((int)r) <= 0)
        //                throw new Exception("Dávka se již zpracovává.");

        //            xcomm.Transaction = itrans;
        //            res = xcomm.ExecuteNonQuery();
        //            if (itrans != null)
        //                itrans.Commit();

        //        }
        //        catch (Exception ex)
        //        {
        //            if (itrans != null)
        //                itrans.Rollback();

        //            throw ex;

        //        }
        //        finally
        //        {
        //            if (xconn.State == ConnectionState.Open)
        //                xconn.Close();
        //        }

        //        return (res > 0);
        //    }



        //} 
        #endregion


        public bool Vydej_GetVydejkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            Globals.LoadConfiguration();

            string selectCount = string.Empty;
            string update = string.Empty;
            string inventura = "I";

            ////15.9.2025 pridani podminky DavkaTerminalVice
            ////kdyz bude itemType == I a soucasne v sekci konfiguracniho souboru sekci Inventura prvek DavkaTerminalVice == true, nezmeni CZDoslo z 0 na cislo terminalu, zustane zachovany..
            // Předpoklady:
            // string TABLE_CZMST_SE;
            // var terminal.ID (int), davka.ID (int), item.Type (string), string inventura = "I";

            string selectCountSql;
            string updateSql;

            if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
            {
                selectCountSql =
                    $"SELECT COUNT(CountEntries) AS davka FROM {TABLE_CZMST_SE} " +
                    "WHERE CountEntries = @countEntries " +
                    "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) ";// +
                                                                       //"  AND ITEMTYPE <> @inventura;";

                //     //if (item.Type == "I" && Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                if (Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                {
                    
                    updateSql =
                                      $"UPDATE {TABLE_CZMST_SE} " +
                                      "SET cz_doslo = @terminalId " +
                                      "WHERE CountEntries = @countEntries " +
                                      "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) " +
                                      "  AND ITEMTYPE <> @inventura;";
                }
                else
                {
                    updateSql =
                                      $"UPDATE {TABLE_CZMST_SE} " +
                                      "SET cz_doslo = @terminalId " +
                                      "WHERE CountEntries = @countEntries " +
                                      "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) ";// +
                                     // "  AND ITEMTYPE <> @inventura;";
                }
                  
            }
            else
            {
                selectCountSql =
                    $"SELECT COUNT(CountEntries) AS davka FROM {TABLE_CZMST_SE} " +
                    "WHERE CountEntries = @countEntries " +
                    "  AND ITEMTYPE LIKE @itemTypeLike " +
                    "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) ";// +
                                                                       //"  AND ITEMTYPE <> @inventura;";

                //     //if (item.Type == "I" && Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                if (Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                {


                    updateSql =
                                     $"UPDATE {TABLE_CZMST_SE} " +
                                     "SET cz_doslo = @terminalId " +
                                     "WHERE CountEntries = @countEntries " +
                                     "  AND ITEMTYPE LIKE @itemTypeLike " +
                                     "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) " +
                                     "  AND ITEMTYPE <> @inventura;";
                }
                else
                {
                    updateSql =
                                  $"UPDATE {TABLE_CZMST_SE} " +
                                  "SET cz_doslo = @terminalId " +
                                  "WHERE CountEntries = @countEntries " +
                                  "  AND ITEMTYPE LIKE @itemTypeLike " +
                                  "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) ";// +
                                  //"  AND ITEMTYPE <> @inventura;";
                }

             
            }

            int res = 0;

            using (var xconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
            using (var xcommAllowed = new SqlCommand(selectCountSql, xconn))
            using (var xcomm = new SqlCommand(updateSql, xconn))
            {
                xconn.Open();
                using (var tran = xconn.BeginTransaction(IsolationLevel.Serializable))
                {
                    xcommAllowed.Transaction = tran;
                    xcomm.Transaction = tran;

                    // Společné parametry
                    xcommAllowed.Parameters.Add("@countEntries", SqlDbType.Int).Value = davka.ID;
                    xcomm.Parameters.Add("@countEntries", SqlDbType.Int).Value = davka.ID;

                    xcommAllowed.Parameters.Add("@terminalId", SqlDbType.Int).Value = terminal.ID;
                    xcomm.Parameters.Add("@terminalId", SqlDbType.Int).Value = terminal.ID;

                    xcommAllowed.Parameters.Add("@inventura", SqlDbType.NVarChar, 50).Value = inventura;
                    xcomm.Parameters.Add("@inventura", SqlDbType.NVarChar, 50).Value = inventura;

                    // Parametr pro LIKE jen v případě, že se používá
                    if (!Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                    {
                        // SQLInjection.Filter(item.Type) nahrazuje parametrizace + přidání %
                        string itemTypeLike = (item?.Type ?? string.Empty) + "%";

                        xcommAllowed.Parameters.Add("@itemTypeLike", SqlDbType.NVarChar, 100).Value = itemTypeLike;
                        xcomm.Parameters.Add("@itemTypeLike", SqlDbType.NVarChar, 100).Value = itemTypeLike;
                    }

                    try
                    {
                        object r = xcommAllowed.ExecuteScalar();
                        int cnt = (r == null || r == DBNull.Value) ? 0 : Convert.ToInt32(r);

                        if (cnt <= 0)
                            throw new Exception("Dávka se již zpracovává nebo nebyla nalezena.");

                        res = xcomm.ExecuteNonQuery();
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }


            return (res > 0);

        }

        #region MaR 3.9.2024 ukladani dat do SQL pomale zpracovani
        /// <summary>
        /// Metoda sloužící pro zpracovaní, Uložení do SQL serveru a prace nad IS
        /// </summary>
        /// <param name="davka"></param>
        /// <param name="terminal"></param>
        /// <param name="sklad"></param>
        /// <param name="item"></param>
        /// <param name="vydejdata"></param>
        /// <param name="processVydejState"></param>
        /// <returns></returns>
        public StatusObject Vydej_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, Fask.DataSets.Vydej vydejdata, Fask.Server.Interfaces.Vydej.ProcessState processVydejState, string itemtype)
        {
            Globals.LoadConfiguration();
            //string guidDavka = vydejdata.CZMST_SEH[0].GUID.ToString();
            string guidDavka = string.Empty;
            if (vydejdata.CZMST_SEH.Count > 0)
                guidDavka = vydejdata.CZMST_SEH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();
            //string filePath = Path.Combine(Properties.Settings.Default.PathStateDataFile, guidDavka);
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals.Konfigurace.Vydej[0].StatusObjectsDirectory, guidDavka));

            StatusObject so = new StatusObject(filePath);

            //zjistit zda soubor s danym guid existuje
            if (File.Exists(filePath))
            { //soubor jiz existuje
                so = StatusObject.Load(filePath);
                if (!so.Exception)
                    return so;
            }



            SqlTransaction xTrans1 = null;

            if (vydejdata == null)
                return so;

            vydejdata.AcceptChanges();

            //System.Data.SqlClient.SqlDataAdapter xDataAdapterSI = new System.Data.SqlClient.SqlDataAdapter();
            //System.Data.SqlClient.SqlDataAdapter xDataAdapterSIH = new System.Data.SqlClient.SqlDataAdapter();
            //System.Data.SqlClient.SqlDataAdapter xDataAdapterSE = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlConnection conn = null;



            try
            {


                so.Write("connection");

                conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                conn.Open();



                //so.Write("init adapters");

                //xDataAdapterSI.InsertCommand = createSIInsert(conn);
                //xDataAdapterSI.SelectCommand = createSISelect(conn);
                //xDataAdapterSI.DeleteCommand = createSIDelete(conn);

                //CreateSIAdapterTableMap(xDataAdapterSI);

                //xDataAdapterSIH.SelectCommand = createSIHSelect(conn);
                //xDataAdapterSIH.InsertCommand = createSIHInsert(conn);
                //xDataAdapterSIH.DeleteCommand = createSIHDelet(conn);

                //xDataAdapterSE.SelectCommand = CreateSESelect(conn);
                //xDataAdapterSE.UpdateCommand = CreateSEUpdate(conn);

                //CreateSEAdapterTableMAp(xDataAdapterSE);

                xTrans1 = conn.BeginTransaction();

                if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Uvolnit) //uvolnit davku
                {
                    foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE.Rows)
                    {
                        if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
                        {
                            serow["CZ_Doslo"] = 0;
                        }
                    }

                    SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter seta = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                    seta.Connection = conn;
                    seta.Transaction = xTrans1;
                    seta.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));

                    //xDataAdapterSE.SelectCommand.Transaction = xTrans1;
                    //xDataAdapterSE.UpdateCommand.Transaction = xTrans1;
                    //xDataAdapterSE.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));
                }
                else if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Zpracovat || processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
                {
                    byte terminalid = (byte)terminal.ID;
                    bool allowInsertData = true;

                    if (Globals.Konfigurace.Inventura[0].DavkaTerminalVice && itemtype == "I")
                    {
                        //15.9.2025 MaR
                        //stale true
                        //bool allowInsertData = true;
                    }
                    else
                    {
                        string xselectcommand =
                                                    "Select Count(*) as number from " + TABLE_CZMST_SE +
                                                    " where countentries=" + davka.ID +
                                                    //" and cz_doslo>0 and cz_doslo<100" + 
                                                    " and cz_doslo=" + terminalid +
                                                    "";

                        //bool allowInsertData = true;

                        System.Data.SqlClient.SqlCommand xselect = new System.Data.SqlClient.SqlCommand(xselectcommand, conn, xTrans1);
                        object datacount = xselect.ExecuteScalar();
                        if (datacount != null && ((int)datacount) == 0)
                        {
                            allowInsertData = false;
                        }
                    }



                    if (allowInsertData)
                    {
                        so.Write("insert");
                        //Data se ulozi
                        if (Globals.Konfigurace.Vydej[0].DexRowIdInsert)
                        {
                            int dexrowid = 0;
                            System.Data.SqlClient.SqlCommand xdexrowidmax = new System.Data.SqlClient.SqlCommand("Select MAX(DEX_ROW_ID) from " + TABLE_CZMST_SI, conn, xTrans1);
                            object maxdexrowid = xdexrowidmax.ExecuteScalar();
                            if (maxdexrowid != null && !(maxdexrowid is System.DBNull))
                                dexrowid = (int)maxdexrowid;

                            vydejdata.CZMST_SI.Columns["DEX_ROW_ID"].ReadOnly = false;

                            foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                            {
                                sirow.DEX_ROW_ID = ++dexrowid;
                            }
                        }

                        if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
                            terminalid = 0;
                        else //if (processVydejState == ProcessVydejState.ZpracovatAPokracovat)
                            terminalid += 100;

                        foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE)
                        {
                            if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
                            {
                                serow.CZ_Doslo = terminalid;

                                //menit PE na zaklade SE
                                //dohledat a zmenit zaznam nad tabulkou PE

                                //vstup do PE pokud v SE bude P
                                if (serow.ITEMTYPE.Trim() == "P")
                                {

                                    //update zaznam v PE
                                    //var y = serow.CountEntries;
                                    //var x =  serow.CZ_Doslo;

                                    Database.Prijem.UpdateCzDosloByCountEntries(serow.CZ_Doslo, serow.CountEntries);

                                    //dohledat zaznam v tabhulce PE a zmenit CZ_doslo

                                }
                                else if (serow.ITEMTYPE.Trim() == "I")
                                {

                                    //update zaznam v PE
                                    //var y = serow.CountEntries;
                                    //var x =  serow.CZ_Doslo;

                                    Database.Inventura.UpdateCzDosloByCountEntries(serow.CZ_Doslo, serow.CountEntries);

                                    //dohledat zaznam v tabhulce PE a zmenit CZ_doslo

                                }


                            }
                        }

                        vydejdata.CZMST_SI.AcceptChanges();

                        #region Smazani starych dat davky z vystupu
                        if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                        {
                            System.Data.SqlClient.SqlCommand xdeleteolddata = new System.Data.SqlClient.SqlCommand("Delete from " + TABLE_CZMST_SI + " where countentries=" + davka.ID, conn, xTrans1);
                            int ndeleted = xdeleteolddata.ExecuteNonQuery();

                            xdeleteolddata.CommandText = "Delete from " + TABLE_CZMST_SIH + " where countentries=" + davka.ID;
                            ndeleted = xdeleteolddata.ExecuteNonQuery();
                        }
                        #endregion

                        foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                        {
                            sirow.SetAdded();
                        }
                        foreach (Fask.DataSets.Vydej.CZMST_SIHRow sihrow in vydejdata.CZMST_SIH)
                        {
                            sihrow.SetAdded();
                        }
                        so.Write("insert 2");

                        SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter seta = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                        seta.Connection = conn;
                        seta.Transaction = xTrans1;
                        seta.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));

                        SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter sita = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter();
                        sita.Connection = conn;
                        sita.Transaction = xTrans1;
                        sita.Update(vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added));

                        SQL_Datasets.VydejTableAdapters.CZMST_SIHTableAdapter sihta = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SIHTableAdapter();
                        sihta.Connection = conn;
                        sihta.Transaction = xTrans1;
                        sihta.Update(vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added));


                        #region lokace
                        if (vydejdata.Parametry.Count > 0 && !vydejdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && vydejdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
                        {
                            foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                            {
                                string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
                                SqlCommand countCommand = new SqlCommand(countCommandText, conn, xTrans1);
                                countCommand.Parameters.Clear();
                                countCommand.Parameters.AddWithValue("@guid", sirow.GUID);

                                int guidcount = (int)countCommand.ExecuteScalar();
                                if (guidcount % 2 == 0)
                                {
                                    DateTime dtnow = DateTime.Now;
                                    Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                                    pohybrow.ITEMNMBR = sirow.ITEMNMBR;
                                    pohybrow.DOCUMENT_NUMBER = sirow.SOPNUMBE;
                                    pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.V;
                                    pohybrow.POHYB_SRC = "V";
                                    pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                                    pohybrow.QTYSHPPD = sirow.QTYSHPPD;
                                    pohybrow.SERLTNUM = sirow.SERLTNUM;
                                    pohybrow.SKL_ID_SRC = sirow.IsSKL_IDNull() ? string.Empty : sirow.SKL_ID;
                                    pohybrow.SKL_ID_DST = string.Empty;
                                    pohybrow.LOCNCODE_SRC = sirow.IsLOCNCODENull() ? string.Empty : sirow.LOCNCODE;
                                    pohybrow.LOCNCODE_DST = string.Empty;
                                    pohybrow.UserID = sirow.USER_ID;
                                    pohybrow.TermID = terminal.ID;
                                    pohybrow.guid = sirow.GUID;
                                    pohybrow.Expiration = sirow.IsExpiraceNull() ? (DateTime?)null : sirow.Expirace;
                                    pohybrow.ITEMDESC = string.Empty;   // dotahnout nazev??
                                    pohybrow.CountEntries = sirow.CountEntries;
                                    pohybrow.dateeveS = dtnow;
                                    if (sirow.IsDATEDONENull() || sirow.IsTIMEDONENull())
                                        pohybrow.dateeveS = dtnow;
                                    else
                                        pohybrow.dateeveT = DateTime.ParseExact(sirow.DATEDONE + " " + sirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                    ProcessVydej(pohybrow, new SqlCommand(), conn, xTrans1, new SqlDataAdapter());

                                }
                            }
                        }
                        #endregion

                        //Nahrani puvodnich a novych dat 
                        //xDataAdapterSI.SelectCommand.Transaction = xTrans1;
                        //xDataAdapterSI.InsertCommand.Transaction = xTrans1;
                        //xDataAdapterSI.Update(vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added));


                        //xDataAdapterSIH.SelectCommand.Transaction = xTrans1;
                        //xDataAdapterSIH.InsertCommand.Transaction = xTrans1;
                        //xDataAdapterSIH.DeleteCommand.Transaction = xTrans1;
                        //xDataAdapterSIH.Update(vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added));

                        //zapis o provedeni do prehlohy
                        //xDataAdapterSE.SelectCommand.Transaction = xTrans1;
                        //xDataAdapterSE.UpdateCommand.Transaction = xTrans1;
                        //xDataAdapterSE.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));
                    }
                }

                so.Write("commit");
                if (xTrans1 != null)
                    xTrans1.Commit();
            }
            catch (Exception ex)
            {
                so.Exception = true;
                so.Write(ex.Message);

                if (xTrans1 != null)
                    xTrans1.Rollback();

                throw ex;
            }
            finally
            {
                if (conn != null && (conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();
            }

            #region Action after data processed
            bool aDP_Action_Asynch = Globals.Konfigurace.Vydej[0].AfterDataProcessed_Action_Asynchronous;
            if (aDP_Action_Asynch)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Vydej_AfterProcessedActionAsync));
                //thread.Start(vydejdata.CZMST_SE[0].CountEntries);
                thread.Start(davka);
            }
            else
            {
                if (!Prijem_AfterProcessedAction(davka))
                {
                    so.Exception = true;
                    so.Write("chyba");
                    return so;
                }
            }
            #endregion

            so.SetOK();
            return so;
        }

        #endregion

        #region MaR 3.9.2024 ukladani dat do SQL rychlejsi zpracovani umela inteligence


        //public StatusObject Vydej_Process(
        //    Fask.Server.Interfaces.Classes.Davka davka,
        //    Fask.Server.Interfaces.Classes.Terminal terminal,
        //    Fask.Server.Interfaces.Classes.Sklad sklad,
        //    Fask.Server.Interfaces.Classes.Item item,
        //    Fask.DataSets.Vydej vydejdata,
        //    Fask.Server.Interfaces.Vydej.ProcessState processVydejState)
        //{
        //    Globals.LoadConfiguration();

        //    string guidDavka = (vydejdata.CZMST_SEH.Count > 0)
        //        ? vydejdata.CZMST_SEH[0].GUID.ToString()
        //        : Guid.NewGuid().ToString();

        //    string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Vydej[0].StatusObjectsDirectory, guidDavka);
        //    StatusObject so = new StatusObject(filePath);

        //    if (File.Exists(filePath))
        //    {
        //        so = StatusObject.Load(filePath);
        //        if (!so.Exception)
        //            return so;
        //    }

        //    using (var conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
        //    {
        //        conn.Open();
        //        using (var xTrans1 = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                so.Write("connection");

        //                if (vydejdata == null)
        //                    return so;

        //                vydejdata.AcceptChanges();

        //                if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Uvolnit)
        //                {
        //                    foreach (var serow in vydejdata.CZMST_SE.Rows.OfType<Fask.DataSets.Vydej.CZMST_SERow>())
        //                    {
        //                        if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
        //                        {
        //                            serow["CZ_Doslo"] = 0;
        //                        }
        //                    }

        //                    var seta = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter
        //                    {
        //                        Connection = conn,
        //                        Transaction = xTrans1
        //                    };
        //                    seta.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));
        //                }
        //                else if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Zpracovat || processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
        //                {
        //                    byte terminalid = (byte)terminal.ID;

        //                    string xselectcommand = $"SELECT COUNT(*) FROM {TABLE_CZMST_SE} WHERE countentries={davka.ID} AND cz_doslo={terminalid}";

        //                    var xselect = new System.Data.SqlClient.SqlCommand(xselectcommand, conn, xTrans1);
        //                    object datacount = xselect.ExecuteScalar();
        //                    bool allowInsertData = datacount != null && (int)datacount > 0;

        //                    if (allowInsertData)
        //                    {
        //                        so.Write("insert");

        //                        if (Globals.Konfigurace.Vydej[0].DexRowIdInsert)
        //                        {
        //                            int dexrowid = 0;
        //                            var xdexrowidmax = new System.Data.SqlClient.SqlCommand($"SELECT MAX(DEX_ROW_ID) FROM {TABLE_CZMST_SI}", conn, xTrans1);
        //                            object maxdexrowid = xdexrowidmax.ExecuteScalar();
        //                            if (maxdexrowid != null && !(maxdexrowid is System.DBNull))
        //                                dexrowid = (int)maxdexrowid;

        //                            vydejdata.CZMST_SI.Columns["DEX_ROW_ID"].ReadOnly = false;

        //                            foreach (var sirow in vydejdata.CZMST_SI.Rows.OfType<Fask.DataSets.Vydej.CZMST_SIRow>())
        //                            {
        //                                sirow.DEX_ROW_ID = ++dexrowid;
        //                            }
        //                        }

        //                        if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
        //                            terminalid = 0;
        //                        else
        //                            terminalid += 100;

        //                        foreach (var serow in vydejdata.CZMST_SE.Rows.OfType<Fask.DataSets.Vydej.CZMST_SERow>())
        //                        {
        //                            if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
        //                            {
        //                                serow.CZ_Doslo = terminalid;

        //                                if (serow.ITEMTYPE.Trim() == "P")
        //                                {
        //                                    Database.Prijem.UpdateCzDosloByCountEntries(serow.CZ_Doslo, serow.CountEntries);
        //                                }
        //                                else if (serow.ITEMTYPE.Trim() == "I")
        //                                {
        //                                    Database.Inventura.UpdateCzDosloByCountEntries(serow.CZ_Doslo, serow.CountEntries);
        //                                }
        //                            }
        //                        }

        //                        vydejdata.CZMST_SI.AcceptChanges();

        //                        if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
        //                        {
        //                            using (var xdeleteolddata = new System.Data.SqlClient.SqlCommand($"DELETE FROM {TABLE_CZMST_SI} WHERE countentries={davka.ID}", conn, xTrans1))
        //                            {
        //                                xdeleteolddata.ExecuteNonQuery();
        //                            }
        //                            using (var xdeleteolddata = new System.Data.SqlClient.SqlCommand($"DELETE FROM {TABLE_CZMST_SIH} WHERE countentries={davka.ID}", conn, xTrans1))
        //                            {
        //                                xdeleteolddata.ExecuteNonQuery();
        //                            }
        //                        }

        //                        foreach (var sirow in vydejdata.CZMST_SI.Rows.OfType<Fask.DataSets.Vydej.CZMST_SIRow>())
        //                        {
        //                            sirow.SetAdded();
        //                        }
        //                        foreach (var sihrow in vydejdata.CZMST_SIH.Rows.OfType<Fask.DataSets.Vydej.CZMST_SIHRow>())
        //                        {
        //                            sihrow.SetAdded();
        //                        }

        //                        var seta = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter
        //                        {
        //                            Connection = conn,
        //                            Transaction = xTrans1
        //                        };
        //                        seta.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));

        //                        var sita = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter
        //                        {
        //                            Connection = conn,
        //                            Transaction = xTrans1
        //                        };
        //                        sita.Update(vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added));

        //                        var sihta = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SIHTableAdapter
        //                        {
        //                            Connection = conn,
        //                            Transaction = xTrans1
        //                        };
        //                        sihta.Update(vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added));

        //                        #region lokace
        //                        if (vydejdata.Parametry.Count > 0 && !vydejdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && vydejdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
        //                        {
        //                            foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
        //                            {
        //                                string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
        //                                SqlCommand countCommand = new SqlCommand(countCommandText, conn, xTrans1);
        //                                countCommand.Parameters.Clear();
        //                                countCommand.Parameters.AddWithValue("@guid", sirow.GUID);

        //                                int guidcount = (int)countCommand.ExecuteScalar();
        //                                if (guidcount % 2 == 0)
        //                                {
        //                                    DateTime dtnow = DateTime.Now;
        //                                    Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
        //                                    pohybrow.ITEMNMBR = sirow.ITEMNMBR;
        //                                    pohybrow.DOCUMENT_NUMBER = sirow.SOPNUMBE;
        //                                    pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.V;
        //                                    pohybrow.POHYB_SRC = "V";
        //                                    pohybrow.SOURCE = "S";      // doplneni ze serveru ...
        //                                    pohybrow.QTYSHPPD = sirow.QTYSHPPD;
        //                                    pohybrow.SERLTNUM = sirow.SERLTNUM;
        //                                    pohybrow.SKL_ID_SRC = sirow.IsSKL_IDNull() ? string.Empty : sirow.SKL_ID;
        //                                    pohybrow.SKL_ID_DST = string.Empty;
        //                                    pohybrow.LOCNCODE_SRC = sirow.IsLOCNCODENull() ? string.Empty : sirow.LOCNCODE;
        //                                    pohybrow.LOCNCODE_DST = string.Empty;
        //                                    pohybrow.UserID = sirow.USER_ID;
        //                                    pohybrow.TermID = terminal.ID;
        //                                    pohybrow.guid = sirow.GUID;
        //                                    pohybrow.Expiration = sirow.IsExpiraceNull() ? (DateTime?)null : sirow.Expirace;
        //                                    pohybrow.ITEMDESC = string.Empty;   // dotahnout nazev??
        //                                    pohybrow.CountEntries = sirow.CountEntries;
        //                                    pohybrow.dateeveS = dtnow;
        //                                    if (sirow.IsDATEDONENull() || sirow.IsTIMEDONENull())
        //                                        pohybrow.dateeveS = dtnow;
        //                                    else
        //                                        pohybrow.dateeveT = DateTime.ParseExact(sirow.DATEDONE + " " + sirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

        //                                    ProcessVydej(pohybrow, new SqlCommand(), conn, xTrans1, new SqlDataAdapter());

        //                                }
        //                            }
        //                        }
        //                        #endregion
        //                    }
        //                    //xTrans1.Commit();



        //                }
        //                so.Write("commit");
        //                if (xTrans1 != null)
        //                    xTrans1.Commit();

        //                so.Write("Ok");
        //            }
        //            catch (Exception ex)
        //            {
        //                xTrans1.Rollback();
        //                so.Write($"Error: {ex.Message}");
        //            }
        //        }
        //    }

        //    so.SetOK();
        //    return so;
        //}


        #endregion

        private void Vydej_AfterProcessedActionAsync(object davka)
        {
            try
            {
                Fask.Server.Interfaces.Classes.Davka d = (Fask.Server.Interfaces.Classes.Davka)davka;
                Vydej_AfterProcessedAction(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Vydej_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();
                string aDP_Action = Globals.Konfigurace.Vydej[0].AfterDataProcessed_Action;
                string aDP_Action_P1 = Globals.Konfigurace.Vydej[0].AfterDataProcessed_Action_P1;
                string aDP_Action_P2 = Globals.Konfigurace.Vydej[0].AfterDataProcessed_Action_P2;
                if (aDP_Action.Length != 0)
                {
                    Routines.AfterProcessAction.Execute(Globals.Konfigurace.ConnectionString[0].FASKDB, TABLE_CZMST_SI, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StatusObject Vydej_Finish_Vydejka(Davka davka, Terminal terminal, string password)
        {
            throw new NotImplementedException();
        }

        public StatusObject Vydej_Storno_Vydejka(Davka davka, Terminal terminal, string password)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IVydej Members

        public StatusObject TEST_ImportVydejka_Do_IS(int countEntries, int userID, string SKL_ID, string note)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IVydej Members

        public StatusObject TEST_ImportFaktura_Do_IS(int countEntries, int userID, string SKL_ID, string note)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IVydej Members

        public System.Data.DataSet Vydej_Detail(Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            try
            {
                Globals.LoadConfiguration();

                if (Globals.Konfigurace.Vydej[0].VydejkaDetail == string.Empty)
                    return null;

                System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Vydej[0].VydejkaDetail_CommandType);


                if (dbCommand.CommandType == CommandType.StoredProcedure)
                {
                    dbCommand.CommandText = Globals.Konfigurace.Vydej[0].VydejkaDetail;
                    dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName, objednavka.ID));
                }
                else if (dbCommand.CommandType == CommandType.Text)
                {

                    //MaR 4.11.2024 uprava zakomentovano
                   dbCommand.CommandText = "Select * from " + Globals.Konfigurace.Vydej[0].VydejkaDetail + " where " + Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName + "='" + objednavka.ID + "'";

                   //  dbCommand.CommandText = "select Firma, Firma2, Utvar, Utvar2, Jmeno, Jmeno2, Ulice, Ulice2, PSC, PSC2, Obec, Obec2, ICO, DIC from StwPh_04535667_2020.dbo.OBJ where Cislo = '102000001'"; 


                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
                }

                dbCommand.Connection = dbconnection;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);
                dbda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public string Vydej_Detail_text(string input)
        {
            try
            {
                Globals.LoadConfiguration();
                string dataText = string.Empty;
                //System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();

                if (Globals.Konfigurace.Vydej[0].VydejkaDetail == string.Empty)
                    return string.Empty;


                using (System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                {


                    using (System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand())
                    {

                        dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Vydej[0].VydejkaDetail_CommandType);


                        if (dbCommand.CommandType == CommandType.StoredProcedure)
                        {
                            dbCommand.CommandText = Globals.Konfigurace.Vydej[0].VydejkaDetail;
                            dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName, input));

                            // Define an output parameter for the stored procedure
                            SqlParameter outputParam = new SqlParameter("@OutputParam", SqlDbType.VarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            dbCommand.Parameters.Add(outputParam);
                        }
                        else if (dbCommand.CommandType == CommandType.Text)
                        {

                            //MaR 4.11.2024 uprava zakomentovano
                           // dbCommand.CommandText = "Select * from " + Globals.Konfigurace.Vydej[0].VydejkaDetail + " where " + Globals.Konfigurace.Vydej[0].VydejkaDetail_paramName + "='" + objednavka.ID + "'";


                            dbCommand.CommandText = "SELECT [Note] FROM [CZMST_SE] WHERE SOPNUMBE = @SOPNUMBE";
                            dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOPNUMBE", input));

                        }
                        else
                        {
                            throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
                        }


                        dbCommand.Connection = dbconnection;
                        //dbCommand.CommandText = "SELECT [Note] FROM [CZMST_SE] WHERE SOPNUMBE = @SOPNUMBER";
                        //dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOPNUMBER", SOPNUMBER));

                        // Otevření připojení
                        dbconnection.Open();

                        // Execute the command
                        if (dbCommand.CommandType == CommandType.StoredProcedure)
                        {
                            // Execute the stored procedure and retrieve the output parameter
                            dbCommand.ExecuteNonQuery();
                            var result = dbCommand.Parameters["@OutputParam"].Value;
                            if (result != null && result != DBNull.Value)
                            {
                                dataText = result.ToString();
                            }
                        }
                        else
                        {
                            var result = dbCommand.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                dataText = result.ToString();
                            }
                        }
                    }
                }

                return dataText;
            }
            catch (Exception ex)
            {
                // Přidejte logování chyby, pokud je to potřeba, namísto přímého přeposlání výjimky
                throw new Exception("Chyba při získávání detailu z databáze.", ex);
            }
        }




        public System.Data.DataSet Vydej_DetailDavka(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();

                if (Globals.Konfigurace.Vydej[0].VydejkaDetailDavka == string.Empty)
                    return null;

                System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Vydej[0].VydejkaDetailDavka_CommandType);


                if (dbCommand.CommandType == CommandType.StoredProcedure)
                {
                    dbCommand.CommandText = Globals.Konfigurace.Vydej[0].VydejkaDetailDavka;
                    dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Globals.Konfigurace.Vydej[0].VydejkaDetailDavka_paramName, davka.ID));
                }
                else if (dbCommand.CommandType == CommandType.Text)
                {
                    dbCommand.CommandText = "Select * from " + Globals.Konfigurace.Vydej[0].VydejkaDetailDavka + " where " + Globals.Konfigurace.Vydej[0].VydejkaDetailDavka_paramName + "='" + davka.ID + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
                }


                dbCommand.Connection = dbconnection;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);
                dbda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataSet Vydej_DetailPolozka(Item item)
        {
            try
            {
                Globals.LoadConfiguration();

                string vydejkadetailpolozka = Globals.Konfigurace.Vydej[0].vydejkadetailpolozka;
                string vydejkadetailpolozka_paramname = Globals.Konfigurace.Vydej[0].vydejkadetailpolozka_paramname;


                if (vydejkadetailpolozka == string.Empty)
                    return null;

                System.Data.SqlClient.SqlConnection xconn = null;
                System.Data.SqlClient.SqlCommand xcomm = null;
                xconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new SqlCommand();
                xcomm.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Vydej[0].vydejkadetailpolozka_CommandType);
                if (xcomm.CommandType == CommandType.StoredProcedure)
                {
                    xcomm.CommandText = vydejkadetailpolozka;
                    xcomm.Parameters.Add(new SqlParameter(vydejkadetailpolozka_paramname, item.ID));
                }
                else if (xcomm.CommandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + vydejkadetailpolozka + " where " + vydejkadetailpolozka_paramname + "='" + item.ID + "'";
                }

                else
                {
                    throw new Exception("Neznámý typ příkazu: " + xcomm.CommandType.ToString());
                }

                xcomm.Connection = xconn;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter xda = new SqlDataAdapter();
                xda.SelectCommand = xcomm;
                xda.Fill(data);

                return data;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (VydejkaDetail:" + item.ID + ")");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
        }

        #endregion

        #region Online metody
        public Fask.Server.Interfaces.DataSets.Vydej_Items_Online Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
        {
            // TODO : implementovat
            throw new Exception("Not implemented");
        }

        public StatusOverExpirace Vydej_Online_Expirace_Verify(string itemnmbr, string sklad_id, string serltnum, DateTime? expirace)
        {
            // TODO : implementovat
            return new StatusOverExpirace()
            {
                Message = "OK",
                State = StatusOverExpiraceState.OK
            };
        }

        public bool Vydej_Online_Expirace_Confirm(byte terminalID, string userID, string password)
        {
            // TODO : implementovat
            return true;
        }

        public StatusInfo Vydej_GenerateData_CZMST094(Objednavka objednavka, Sklad sklad)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
