using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Fask.MST_W.Extensions
{
    public static class DataSetExtensions
    {
        /// <summary>
        /// Moves all values from 1. row of all tables into dictionary as ColumnaName:RowValue
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="dict"></param>
        public static void Transform2Dictionary(this DataSet ds, Dictionary<string, string> dict)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataTable dtab in ds.Tables)
                {
                    foreach (DataColumn dcol in dtab.Columns)
                    {
                        if (!dict.Keys.Contains(dcol.ColumnName.Trim()))
                            dict.Add(dcol.ColumnName.Trim(), dtab.Rows[0][dcol].ToString());
                    }
                }
            }
        }
    }
}
