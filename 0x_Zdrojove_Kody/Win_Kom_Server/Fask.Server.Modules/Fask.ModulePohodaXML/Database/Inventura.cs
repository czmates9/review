using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Fask.SQL.Database
{
    class Inventura
    {

        public static Datasets.DatabasePohoda.SKzRow SKzRow(int ID)
        {
            Datasets.DatabasePohoda.SKzDataTable polozky = null;
            try
            {

				polozky = Database.Pohoda.SKz_GetDataByID(ID);
                return polozky.Count > 0 ? polozky[0] : null;
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

        public static Datasets.DatabasePohoda.ADRow AD(int ID)
        {
            Datasets.DatabasePohoda.ADDataTable dodavatele = null;
            try
            {

				dodavatele = Database.Pohoda.AD_GetDataByID(ID);
                return dodavatele.Count > 0 ? dodavatele[0] : null;
            }
            catch (SqlException sqlex)
            {
				
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Inventura", System.Reflection.MethodBase.GetCurrentMethod().Name, sqlex);
                return null;
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Inventura", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
            }
        }

    }
}
