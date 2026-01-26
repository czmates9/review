using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
    class Lokace
    {
		public static int Count_STAVPOHYB(IngresConnection con, Guid guid)
		{

			try
			{
				using (var com = con.CreateCommand())
				{
					com.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " WHERE guid=@guid";
					com.CommandType = CommandType.Text;

					com.Parameters.Add(new IngresParameter() { ParameterName = "@guid", DbType = System.Data.DbType.String, Value = guid, SourceVersion = DataRowVersion.Current });


					con.Open();

					object tmp = com.ExecuteScalar();

					if ((tmp != null) && (tmp is int?))
						return (int)tmp;
					else
						return 0;
				}
			}
			catch (IngresException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return 0;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return 0;
			}
		}


	}
}
