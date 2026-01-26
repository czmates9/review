using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.SQL.Database
{
    class Spolecne
    {
        public static string GET_ITEMDESC_by_ITEMCODE(string ITEMNMBR)
        {
            try
            {

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (System.Data.SqlClient.SqlCommand sqlcommand = con.CreateCommand())
                    {
                     
                        sqlcommand.CommandText = "SELECT ITEMDESC FROM " + Constants.Common.TABLE_FASK_ZASOBY + " WHERE ITEMNMBR=@itemnmbr";
                        sqlcommand.Parameters.AddWithValue("@itemnmbr", ITEMNMBR); ;
                        sqlcommand.CommandType = System.Data.CommandType.Text;
                        sqlcommand.Connection.Open();
                        object retO = sqlcommand.ExecuteScalar();

                        if (retO != null && retO is string)
                        {
                            sqlcommand.Connection.Close();
                            return retO as string;
                        }
                        else
                            return string.Empty;
                    } 
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }
    }
}
