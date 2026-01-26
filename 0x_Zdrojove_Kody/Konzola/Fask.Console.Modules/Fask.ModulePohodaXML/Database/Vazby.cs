using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;
using System.Data;

namespace Fask.ModulePohodaXML.Database
{
    class Vazby
    {

        public static bool Update_FASK_Vyroba_TP_MaterialyZamena(List<DataProZamenu> list)
        {

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                trans = connection.BeginTransaction(IsolationLevel.Serializable);

                //Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter ta_TP = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();

                //ta_TP.Connection = connection;

                //trans = connection.BeginTransaction(IsolationLevel.Serializable);
                //ta_TP.MyTransaction = trans;

                //foreach (DataProZamenu item in list)
                //{
                //    ta_TP.Update_Materialy(item._ITEMNMBR, item._DESC, item._MJ, item._ID_Zdroj);
                //}

                foreach (DataProZamenu item in list)
                {
                    Database.Vyroba_FASK_Vyroba_TP.Update(
                        connection,
                        trans,
                        item._ITEMNMBR, 
                        item._DESC, 
                        item._MJ, 
                        item._ID_Zdroj
                        );
                }

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vazby", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }

                throw ex;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

    }

    class DataProZamenu
    {
        public int _ID_Zdroj;

        public string _ITEMNMBR;
        public string _DESC;
        public string _MJ;


        public DataProZamenu(int ID_Zdroj, string ITEMNMBR, string DESC, string MJ)
        {
            _ID_Zdroj = ID_Zdroj;
            _ITEMNMBR = ITEMNMBR;
            _DESC = DESC;
            _MJ = MJ;
        }
    }
}
