using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PohodaImportVolitelneParametry.Database
{
    class Pohoda
    {

        #region FXTS
        public static DataSets.PohodaDB sVPUL_GetData_ByFXTS()
        {
            DataSets.PohodaDB ds = new DataSets.PohodaDB();
            sVPUL_Fill_ByFXTS(ds);
            return ds;
        }

        public static void sVPUL_Fill_ByFXTS(DataSets.PohodaDB ds)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT ID, Sel, UsrAgID, IDS, SText, UseConstID, Oznacil, Ucetni, Creator, Pozn, NullCheck_IDS FROM sVPUL where IDS='FXTS'";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Settings.ConnectionStringPohoda);

                da.Fill(ds.sVPUL);
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog("Pohoda Tabulky Definice SQL Dotazu", " XXX_Fill", ex.Message);
                //Log.writeErrorData(dataTable);
            }

        }

        #endregion

        #region IDS and UsrAgID

        public static DataSets.PohodaDB sVPUL_GetData_ByIDS_UsrAgID(string IDS)
        {
            DataSets.PohodaDB ds = new DataSets.PohodaDB();
            sVPUL_Fill_ByIDS_UsrAgID(ds, IDS);
            return ds;
        }

        public static void sVPUL_Fill_ByIDS_UsrAgID(DataSets.PohodaDB ds, string IDS)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT UsrAgID FROM sVPUL where IDS='" + IDS.Trim() + "'";
                //da.SelectCommand.Parameters.AddWithValue("?", XXX);

                da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Settings.ConnectionStringPohoda);

                da.Fill(ds.sVPUL_UsrAgID);
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog("Pohoda Tabulky Definice SQL Dotazu", " XXX_Fill", ex.Message);
                //Log.writeErrorData(dataTable);
            }

        }

        #endregion

    }
}
