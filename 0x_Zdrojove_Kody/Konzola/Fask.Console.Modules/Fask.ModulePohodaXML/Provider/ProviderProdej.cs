using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using System.Windows.Forms;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : Fask.Interfaces.Prodej.IProdej2,
        Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky,
        Fask.Interfaces.Prodej.IProdej2_ImportDavka,
        Fask.Interfaces.Prodej.IProdej2_UpdateDI
    {


        #region IProdej2_ImportDavka Members

        public Fask.Interfaces.Classes.StatusInfo ImportDavka(Fask.Interfaces.Classes.Objednavka objednavka, Fask.Interfaces.Classes.Sklad sklad, bool Grupuj)
        {

            Fask.Interfaces.Classes.StatusInfo so = new Fask.Interfaces.Classes.StatusInfo();

            Fask.Interfaces.DataSets.Prodej data = new Fask.Interfaces.DataSets.Prodej();

            if (Grupuj)
            {

                data = Prodej_GetGroupDavky(objednavka.CisloDavky);
            }
            else 
            {
                var f = new Fask.Interfaces.Filtry.ProdejFiltr();
                f.CountEntries = objednavka.CisloDavky;
                data = Prodej_GetFiltrovaneDavky(f);
            }
            
            

            Fask.Interfaces.Classes.Davka d = new Fask.Interfaces.Classes.Davka();


                if (data.CZMST_DI.Count > 0)
                {
                    so.Description  = "Generovani Dokladu";
                    string vysledek = this.Prodej_Import_Pohoda_XML(data);

                    if (vysledek != "OK")
                    {
                        so.Description = vysledek;
                        return so;
                    }
                }

                return so;
            
        }



        #endregion

        #region IProdej2_GetFiltrovaneDavky Members

        #region old MaR 26.5 2025
        public Fask.Interfaces.DataSets.Prodej Prodej_GetFiltrovaneDavkyOld(Fask.Interfaces.Filtry.ProdejFiltr filtr)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prodej dsProdej = new Fask.Interfaces.DataSets.Prodej();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
                    " WHERE" +
                    " 1=1 ";


                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "AND CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }


                if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
                {
                    command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
                }

                command.CommandText += "order by";
                command.CommandText += " CountEntries";
                command.CommandText += " , DEX_ROW_ID";


                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsProdej, dsProdej.CZMST_DI.TableName);

                return dsProdej;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Prodej Prodej_GetFiltrovaneDavky(Fask.Interfaces.Filtry.ProdejFiltr filtr)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prodej dsProdej = new Fask.Interfaces.DataSets.Prodej();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                var sb = new StringBuilder();

                //27.8.2025 MaR zakomentoval
                //sb.Append("SELECT DI.*, FZ2.ITEMDESC ");
                //sb.Append("FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI + " DI ");
                //sb.Append("LEFT JOIN FASK_ZASOBY FZ2 ON DI.ITEMNMBR = FZ2.ITEMNMBR AND DI.QTYPACK = FZ2.QTYPACK ");

                sb.Append("WITH Z AS (");
                sb.Append("    SELECT");
                sb.Append("        FZ2.ITEMNMBR,");
                sb.Append("        FZ2.QTYPACK,");
                sb.Append("        FZ2.ITEMDESC,");
                sb.Append("        ROW_NUMBER() OVER (");
                sb.Append("            PARTITION BY FZ2.ITEMNMBR, FZ2.QTYPACK");
                sb.Append("            ORDER BY FZ2.QTYPACK ASC");
                sb.Append("        ) AS rn");
                sb.Append("    FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " AS FZ2");
                sb.Append(" )");
                sb.Append(" SELECT DI.*, Z.ITEMDESC");
                sb.Append(" FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI + " AS DI");
                sb.Append(" LEFT JOIN Z");
                sb.Append("    ON Z.ITEMNMBR = DI.ITEMNMBR");
                sb.Append("   AND Z.QTYPACK  = DI.QTYPACK");
                sb.Append("   AND Z.rn = 1 ");

                sb.Append("WHERE 1=1 ");



                if (!string.IsNullOrWhiteSpace(filtr.CountEntries))
                {
                    sb.Append("AND DI.CountEntries = @countentries ");
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMNMBR))
                {
                    sb.Append("AND DI.ITEMNMBR = @ITEMNMBR ");
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMDESC))
                {
                    sb.Append("AND FZ2.ITEMDESC LIKE @itemdesc ");
                    command.Parameters.AddWithValue("@itemdesc", "%" + filtr.ITEMDESC.Trim() + "%");
                }


                if (!string.IsNullOrWhiteSpace(filtr.ITEMCODE))
                {
                    var hodnoty = filtr.ITEMCODE
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@itemcode{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND DI.ITEMCODE IN (" + string.Join(", ", hodnoty.Select(h => h.ParamName)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }




                if (!string.IsNullOrWhiteSpace(filtr.DOC_ID))
                {
                    var hodnoty = filtr.DOC_ID
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@docid{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND DI.DOC_ID IN (" + string.Join(", ", hodnoty.Select(h => h.ParamName)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtr.DOC_ID2))
                {
                    var hodnoty = filtr.DOC_ID2
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@docid2{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND DI.DOC_ID2 IN (" + string.Join(", ", hodnoty.Select(h => h.ParamName)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }



                if (filtr.USERID != null && filtr.USERID.Count > 0)
                {
                    var paramNames = new List<string>();
                    for (int i = 0; i < filtr.USERID.Count; i++)
                    {
                        string paramName = "@userid" + i;
                        paramNames.Add(paramName);
                        command.Parameters.AddWithValue(paramName, filtr.USERID[i]);
                    }

                    sb.Append("AND DI.USER_ID IN (" + string.Join(", ", paramNames) + ") ");
                }

                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Count > 0)
                {
                    var paramNames = new List<string>();
                    for (int i = 0; i < filtr.ID_TERMINAL.Count; i++)
                    {
                        string paramName = "@terminal" + i;
                        paramNames.Add(paramName);
                        command.Parameters.AddWithValue(paramName, filtr.ID_TERMINAL[i]);
                    }

                    sb.Append("AND DI.ID_TERMINAL IN (" + string.Join(", ", paramNames) + ") ");
                }

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND DI.DATEDONE BETWEEN @DATEDONE_OD AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND DI.DATEDONE BETWEEN '19990101' AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    sb.Append("AND DI.DATEDONE BETWEEN @DATEDONE_OD AND '25000101' ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant) && !filtr.Dateeve_TimeVariant.Contains("unknow"))
                {
                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    var TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(
                        typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                    int cislo = (int)TimeVar;

                    if (cislo != 0 && cislo < 8)
                    {
                        sb.Append("AND DI.TIMEDONE >= @TIME_TV ");
                        command.Parameters.AddWithValue("@TIME_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss"));
                    }

                    sb.Append("AND DI.DATEDONE >= @dateeve_TV ");
                    command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd"));
                }

                sb.Append("ORDER BY DI.CountEntries ASC, DI.DEX_ROW_ID ASC");

                command.CommandText = sb.ToString();
                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsProdej, dsProdej.CZMST_DI.TableName);

                return dsProdej;
            }
            catch
            {
                throw;
            }
        }



        #endregion

        #endregion

        public bool UpdateDI(Prodej.CZMST_DIRow DIRow)
        {

            return Fask.ModulePohodaXML.Database.Sklady_CZMST_DI.Update(DIRow, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB) > 0;

            //throw new NotImplementedException();
        }


        #region Dotaženi sgrupovanych dat s DI

        public Fask.Interfaces.DataSets.Prodej Prodej_GetGroupDavky(string CountEntries)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prodej dsProdej = new Fask.Interfaces.DataSets.Prodej();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                            "SELECT " +
                            "CountEntries, VNDITNUM, CZ_CarKod, ODB_ID, SKL_ID, ITEMNMBR, " +
                            "SUM(QTYSHPPD) AS QTYSHPPD, " +
                            "SUM(QTYSHPPDMJ) AS QTYSHPPDMJ, " +
                            "SUM(QTYPACK) AS QTYPACK, " +
                            "'' AS SERLTNUM, " +
                            "USER_ID, " +
                            "MAX(DEX_ROW_ID) AS DEX_ROW_ID, " +
                            "CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, " +
                            "0 AS INPUT_MODE , ID_TERMINAL, MJ, DOC_ID" + 

                    " FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
                    " WHERE" +
                    " 1=1 ";

                command.CommandText += " AND CountEntries = " + CountEntries;

                command.CommandText += " group by CountEntries, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ODB_ID, DOC_ID";

                command.CommandText += " order by";
                command.CommandText += " CountEntries";
                command.CommandText += " , DEX_ROW_ID";


                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsProdej, dsProdej.CZMST_DI.TableName);



//                select 
//CountEntries,
//VNDITNUM, 
//CZ_CarKod,
//ODB_ID,
//SKL_ID,
//ITEMNMBR,
//SUM(QTYSHPPD) AS QTYSHPPD, 
//SUM(QTYSHPPDMJ) AS QTYSHPPDMJ,
//SUM(QTYPACK) AS QTYPACK,
//'' AS SERLTNUM,
//USER_ID, 
//MAX(DEX_ROW_ID) AS DEX_ROW_ID, 
//CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID,
//0 AS INPUT_MODE

//from CZMST_DI

//WHERE (CountEntries = '110024')
//group by CountEntries, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ODB_ID



                return dsProdej;
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        #endregion


        #region Metoda pro import do Pohody
       
        
        
        private string Prodej_Import_Pohoda_XML(Fask.Interfaces.DataSets.Prodej data)
        {
            Globals_V1.LoadConfiguration();

            //Doklad typDoklad = null;
            //string dokladID = data.CZMST_DI[0].IsDOC_IDNull() ? "" : data.CZMST_DI[0].DOC_ID.Trim();
            //string dokladID2 = data.CZMST_DI[0].IsDOC_ID2Null() ? "" : data.CZMST_DI[0].DOC_ID2.Trim();
            //string skladID = data.CZMST_DI[0].IsSKL_IDNull() ? "" : data.CZMST_DI[0].SKL_ID.Trim();
            
            //for (int i = 0; i < Globals.listDokladu.Count; i++)
            //{
            //    if ((dokladID == Globals.listDokladu[i].docid) && (dokladID2 == Globals.listDokladu[i].docid2) && (skladID == Globals.listDokladu[i].sklid))
            //    {
            //        typDoklad = Globals.listDokladu[i];
            //        break;
            //    }
            //}


            //Classes.TypDokladu td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), typDoklad.funkce, true);

            #region TypDokladu
            Classes.TypDokladu td = Classes.TypDokladu.Unknow;
            Fask.Rady.DS_Rady.FASK_RADYRow row = null;
            Doklad typDoklad = null;
            Fask.Rady.NumericalSeries NS = null;

            string dokladID = data.CZMST_DI[0].IsDOC_IDNull() ? string.Empty : data.CZMST_DI[0].DOC_ID.Trim();
            string dokladID2 = data.CZMST_DI[0].IsDOC_ID2Null() ? string.Empty : data.CZMST_DI[0].DOC_ID2.Trim();
            string skladID = data.CZMST_DI[0].IsSKL_IDNull() ? string.Empty : data.CZMST_DI[0].SKL_ID.Trim();

            try
            {
                Globals_V1.LoadConfiguration();
                NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);

                td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), row.Modul_Funkce, true);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", " Rada,dotaženi pro DOC_ID:  " + dokladID, ex);

                if (NS == null)
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt NS", " Objekt NS is NULL");

                if (row == null)
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt row", " Objekt row is NULL");

                if (row.Modul_Funkce == null)
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt Modul_Funkce", " Objekt Modul_Funkce is NULL");

            }

            typDoklad = new Doklad(row);


            #endregion

            string file = Classes.Prodej2.FilenameComposeTypDoklad(td);
            string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + file;

            string Status = "OK";


            switch (td)
            {
                case Classes.TypDokladu.vyd_k:
                    Status = Prodej_Import_Vyd_Pohoda(filename, data, typDoklad);
                    break;
                case Classes.TypDokladu.pri_k:
                    Status = Prodej_Import_Pri_Pohoda(filename, data, typDoklad);
                    break;
                case Classes.TypDokladu.pro_k:
                    Status = Prodej_Import_Pro_Pohoda(filename, data, typDoklad);
                    break;
                case Classes.TypDokladu.objv_k:
                    Status = Prodej_Import_Objv_Pohoda(filename, data, typDoklad);
                    break;
                //case Classes.TypDokladu.objvcm:
                //    Status = Prodej_Import_Objvcm_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                //case Classes.TypDokladu.objvm:
                //    Status = Prodej_Import_Objvm_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                //case Classes.TypDokladu.objbezodb:
                //    Status = Prodej_Import_Objbezodb_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                //case Classes.TypDokladu.objp:
                //    Status = Prodej_Import_Objp_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                //case Classes.TypDokladu.objpcm:
                //    Status = Prodej_Import_Objpcm_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                //case Classes.TypDokladu.objpm:
                //    Status = Prodej_Import_Objpm_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                //case Classes.TypDokladu.expedice:
                //    Status = Prodej_Import_Expedice_Pohoda(filename, data, typDoklad, uzivatel);
                //    break;
                case Classes.TypDokladu.pre_k:
                    Status = Prodej_Import_Pre_Pohoda(filename, data, typDoklad);
                    break;
                default:
                    MessageBox.Show("Typ dokladu není určen pro export", "Neznámý typ dokladu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 break;
            }

            return Status;

        }


        #region Importy jednotlivych dokladu

        /// <summary>
        /// Metoda pro import Vydejky
        /// </summary>
        /// <param name="filename">Cesta k requestu</param>
        /// <param name="data">data z CZMST_DI</param>
        /// <param name="uzivatel">Uzivatel</param>
        /// <returns>bud OK anebo chybu</returns>
        private string Prodej_Import_Vyd_Pohoda(string filename, Fask.Interfaces.DataSets.Prodej data, Doklad typDoklad)
        {
            try
            {

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Classes.Prodej2.CreateRequest_Vyd_XML(filename, "FASK Import - Vyd_k XML", data.CZMST_DI, typDoklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Classes.Prodej2.LoadResponse_Vyd_XML(filename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metoda pro import Prodejka
        /// </summary>
        /// <param name="filename">Cesta k requestu</param>
        /// <param name="data">data z CZMST_DI</param>
        /// <param name="uzivatel">Uzivatel</param>
        /// <returns>bud OK anebo chybu</returns>
        private string Prodej_Import_Pro_Pohoda(string filename, Fask.Interfaces.DataSets.Prodej data, Doklad typDoklad)
        {
            try
            {

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Classes.Prodej2.CreateRequest_Pro_XML(filename, "FASK Import - Pro_k XML", data.CZMST_DI, typDoklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Classes.Prodej2.LoadResponse_Pro_XML(filename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Metoda pro import Objv (??Objednavka vydana)
        /// </summary>
        /// <param name="filename">Cesta k requestu</param>
        /// <param name="data">data z CZMST_DI</param>
        /// <param name="uzivatel">Uzivatel</param>
        /// <returns>bud OK anebo chybu</returns>
        private string Prodej_Import_Objv_Pohoda(string filename, Fask.Interfaces.DataSets.Prodej data, Doklad typDoklad)
        {
            try
            {

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Classes.Prodej2.CreateRequest_Objv_XML(filename, "FASK Import - Objv_k XML", data.CZMST_DI, typDoklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Classes.Prodej2.LoadResponse_Objv_XML(filename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Metoda pro import Prijemky
        /// </summary>
        /// <param name="filename">Cesta k requestu</param>
        /// <param name="data">data z CZMST_DI</param>
        /// <param name="uzivatel">Uzivatel</param>
        /// <returns>bud OK anebo chybu</returns>
        public string Prodej_Import_Pri_Pohoda(string filename, Fask.Interfaces.DataSets.Prodej data, Doklad typDoklad)
        {
            try
            {

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Classes.Prodej2.CreateRequest_Pri_XML(filename, "FASK Import - Pri_k XML", data.CZMST_DI, typDoklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Classes.Prodej2.LoadResponse_Pri_XML(filename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Metoda pro import Prevodka
        /// </summary>
        /// <param name="filename">Cesta k requestu</param>
        /// <param name="data">data z CZMST_DI</param>
        /// <param name="uzivatel">Uzivatel</param>
        /// <returns>bud OK anebo chybu</returns>
        private string Prodej_Import_Pre_Pohoda(string filename, Fask.Interfaces.DataSets.Prodej data, Doklad typDoklad)
        {
            try
            {

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Classes.Prodej2.CreateRequest_Pre_XML(filename, "FASK Import - Pre_k XML", data.CZMST_DI, typDoklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Classes.Prodej2.LoadResponse_Pre_XML(filename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #endregion

        #endregion



    }



}
