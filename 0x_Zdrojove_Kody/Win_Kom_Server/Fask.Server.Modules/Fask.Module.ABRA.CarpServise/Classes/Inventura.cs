using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.CarpServise.Classes
{
    public static class Inventura
    {

        public static bool LoadInventura(out int PocetInventur)
        {
            //TODO tady implementovat dotaženi z ABRY k nam
            //Treba naplnit CZMST_I1 a CZMST_I1H
            PocetInventur = 0;

            Globals_V1.LoadConfiguration();
            SqlTransaction trans = null;
            SqlConnection connection = null;

            try
            {
                SQL_Datasets.Inventura InventuraDS = new SQL_Datasets.Inventura();

                ABRA_Datasets.Inventura.CZMST_I1DataTable dt_polozky = Database.ABRA.PolozkyInventur();
                ABRA_Datasets.Inventura.CZMST_I1HDataTable dt_inventury = Database.ABRA.SeznamInventur();

                if (dt_polozky == null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Neni zadna inventura na stahnuti");
                    return false;
                }

                string CZ_CarKod;
                string ITEMNMBR;
                string LOCNCODE;
                string ITEMDESC;
                string SKL_ID;
                string MJ;
                string ITEMCODE;
                byte CZ_SerNum_Track;

                SQL_Datasets.Inventura inventura = new SQL_Datasets.Inventura();

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "LoadInventura CS: " + Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                
                connection.Open();
                trans = connection.BeginTransaction();

                SQL_Datasets.InventuraTableAdapters.CZMST_I1TableAdapter CZMST_I1TableAdapter = new SQL_Datasets.InventuraTableAdapters.CZMST_I1TableAdapter();
                SQL_Datasets.InventuraTableAdapters.CZMST_I1HTableAdapter CZMST_I1HTableAdapter = new SQL_Datasets.InventuraTableAdapters.CZMST_I1HTableAdapter();
                SQL_Datasets.InventuraTableAdapters.CZMST_I3TableAdapter CZMST_I3TableAdapter = new SQL_Datasets.InventuraTableAdapters.CZMST_I3TableAdapter();

                CZMST_I1TableAdapter.Connection = trans.Connection;
                CZMST_I1HTableAdapter.Connection = trans.Connection;
                CZMST_I3TableAdapter.Connection = trans.Connection;


                CZMST_I1TableAdapter.Transaction = trans;
                CZMST_I1HTableAdapter.Transaction = trans;
                CZMST_I3TableAdapter.Transaction = trans;

                //CZMST_I1TableAdapter.DeleteQuery();
                //CZMST_I1HTableAdapter.DeleteQuery();
                //CZMST_I3TableAdapter.DeleteQuery();

                int Description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1H["Description"].MaxLength;

                int ID = Database.Inventura.CZMSTI1H_MAX_CountEntries();

                foreach (ABRA_Datasets.Inventura.CZMST_I1HRow row in dt_inventury)
                {
                    PocetInventur++;

                    string text = "-";

                    if (!row.IsDescriptionNull())
                    {
                        text = row.Description;
                        if (text.Length > Description_MaxLength)
                            text = text.Remove(Description_MaxLength);
                    }
                    inventura.CZMST_I1H.AddCZMST_I1HRow(ID++, text, 0);
                }

                

                int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["ITEMNMBR"].MaxLength;
                int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["MJ"].MaxLength;
                int VENDNAME_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["VENDNAME"].MaxLength;
                int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMDESC"].MaxLength;
                int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMCODE"].MaxLength;

                foreach (ABRA_Datasets.Inventura.CZMST_I1Row row in dt_polozky)
                {
                    int CountEntries = 0;

                    foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
                    {
                        if (item.Description.Trim() == row.CountEntries.Trim())
                        {
                            CountEntries = item.CountEntries;
                            break;
                        }
                    }

                    CZ_CarKod = row.IsCZ_CarKodNull() ? string.Empty : row.CZ_CarKod;
                    ITEMDESC = row.IsITEMDESCNull() ? string.Empty : row.ITEMDESC;
                    ITEMNMBR = row.IsITEMNMBRNull() ? string.Empty : row.ITEMNMBR;
                    LOCNCODE = string.Empty;

                    SKL_ID = row.IsSKL_IDNull() ? string.Empty : row.SKL_ID;
                    MJ = row.IsMJNull() ? string.Empty : row.MJ;
                    ITEMCODE = row.IsITEMCODENull() ? string.Empty : row.ITEMCODE;

                    CZ_SerNum_Track = 0;


                    if (ITEMNMBR.Length > ITEMNMBR_MaxLength)
                        ITEMNMBR = ITEMNMBR.Remove(ITEMNMBR_MaxLength);


                    if (ITEMDESC.Length > ITEMDESC_MaxLength)
                        ITEMDESC = ITEMDESC.Remove(ITEMDESC_MaxLength);

                    if (ITEMCODE.Length > ITEMCODE_MaxLength)
                        ITEMCODE = ITEMCODE.Remove(ITEMCODE_MaxLength);

                    inventura.CZMST_I1.AddCZMST_I1Row(
                        CountEntries,
                        ITEMNMBR,
                        CZ_CarKod,
                        ITEMDESC,
                        LOCNCODE,
                        SKL_ID,
                        (decimal)row.QUANTITY,
                        MJ,
                        DateTime.Now,
                        0,
                        0,
                        CZ_SerNum_Track,
                        0,
                        0,
                        0,
                        "",
                        "",
                        ITEMCODE);

                    inventura.CZMST_I3.AddCZMST_I3Row(
                        CountEntries, 
                        ITEMNMBR,
                        CZ_CarKod, 
                        (decimal)0, 
                        MJ, 
                        string.Empty,
                        CZ_CarKod, 
                        string.Empty,
                        0);
                }

                CZMST_I1TableAdapter.Update(inventura.CZMST_I1);
                CZMST_I1HTableAdapter.Update(inventura.CZMST_I1H);
                CZMST_I3TableAdapter.Update(inventura.CZMST_I3);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();

                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }


        public static bool ImportInventura(int countentries, bool inv_edn)
        {
            //TODO tady implementovat nataženi do ABRY spatky

            return true;

        }

    }
}
