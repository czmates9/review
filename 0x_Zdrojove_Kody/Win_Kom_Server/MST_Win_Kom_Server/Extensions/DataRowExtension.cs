using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Extensions
{
    public static class DataRowExtension
    {
        /// <summary>
        /// Trimuje vsechny stringove sloupce
        /// </summary>
        /// <param name="dataRow"></param>
        public static void TrimStringColumns(this DataRow dataRow)
        {
            dataRow.BeginEdit();
            var columns = dataRow.Table.Columns;
            foreach (DataColumn column in columns)
            {
                object val = dataRow[column];
                if (val is string)
                {
                    dataRow[column] = ((string)val).Trim();
                }
            }
            dataRow.EndEdit();
        }

        /// <summary>
        /// Trimuje vsechny stringove sloupce
        /// </summary>
        /// <param name="dataTable"></param>
        public static void TrimStringColumns(this DataTable dataTable)
        {
            dataTable.BeginLoadData();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow.TrimStringColumns();
            }
            dataTable.EndLoadData();
        }

        /// <summary>
        /// Trimuje vsechny stringove sloupce
        /// </summary>
        /// <param name="dataSet"></param>
        public static void TrimStringColumns(this DataSet dataSet)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                dataTable.TrimStringColumns();
            }
        }
    }
}