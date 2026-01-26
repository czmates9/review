using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.Columns
{
	public static class XXX
	{

		#region SQL script

		//...

		#endregion

		private static string TableName = "AAA";
		public static Dictionary<string, ColumnType> ColumnsInfo_AAA = new Dictionary<string, ColumnType>();

		#region c'tor

		static XXX()
		{

            #region SQL
            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "XXX.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (dr["TABLE_NAME"] == TableName)
            //        {
            //            ColumnsInfo_AAA.AddIfNotExists(dr);
            //            continue;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //} 
            #endregion
		}

		#endregion

	}
}

