using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;

namespace Fask.ModulePohodaXML.Database
{
    class Inventura
    {

        public static  Pohoda_DataSets.DatabasePohoda.SKzRow SKzRow(int ID)
        {
            Pohoda_DataSets.DatabasePohoda.SKzDataTable polozky = null;
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

        public static Pohoda_DataSets.DatabasePohoda.ADRow AD(int ID)
        {
            Pohoda_DataSets.DatabasePohoda.ADDataTable dodavatele = null;
            try
            {

                dodavatele = Database.Pohoda.AD_GetDataByID(ID);
                return dodavatele.Count > 0 ? dodavatele[0] : null;
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
    }
}
