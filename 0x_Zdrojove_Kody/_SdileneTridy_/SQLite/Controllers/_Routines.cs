using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.SQLiteDBs.Controllers
{
    public class _Routines
    {
        #region Support for load row mapping reader 2 rows
        //protected internal void MapValues2Rows(System.Data.IDataReader ireader, System.Data.DataRow datarow)
        //{
        //    var columns = datarow.Table.Columns;
        //    for (int i = 0; i < columns.Count; i++)
        //    {
        //        var col = columns[i];
        //        var icol = ireader.GetOrdinal(col.ColumnName);
        //        if (icol >= 0)
        //            datarow[i] = ireader[icol];
        //        else
        //            datarow[i] = DBNull.Value;
        //    }
        //}

        //public static System.Data.DataRow LoadRowFromReader(System.Data.IDataReader ireader, System.Data.DataTable datatable)
        //{
        //    object[] values = new object[ireader.FieldCount];
        //    ireader.GetValues(values);
        //    return datatable..LoadDataRow(values, true);
        //}

        /// <summary>
        /// Importuje row do tabulky
        /// </summary>
        /// <param name="ireader">dbdatareader</param>
        /// <param name="datatable">zdrojova table, kterou nejprve maze</param>
        /// <returns>Datarow poskytnute zdrojove tabulky</returns>
        public static System.Data.DataRow LoadRowFromReader(System.Data.Common.DbDataReader reader, System.Data.DataTable datatable)
        {
            var datarow = datatable.NewRow();
            foreach (System.Data.DataColumn column in datarow.Table.Columns)
            {
                int readerColumnIndex = reader.GetOrdinal(column.ColumnName);
				if (readerColumnIndex >= 0)
				{
					datarow[column] = reader[readerColumnIndex];
				}
				else
					datarow[column] = DBNull.Value;
            }
            datatable.Rows.Add(datarow);
            datatable.AcceptChanges();
            return datarow;
        }

        #endregion
    }
}
