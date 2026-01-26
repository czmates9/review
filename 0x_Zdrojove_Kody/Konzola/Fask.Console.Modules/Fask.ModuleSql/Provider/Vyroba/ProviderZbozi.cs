using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModuleSql
{
    public partial class Provider :        
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_GetFaskCons095,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_GetFiltrovaneZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_InsertZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_DeleteZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_UpdateZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_UpdateZboziTable
    {
        #region IZboziVyroba

        #region IZboziVyroba_GetFaskCons095 Members

        public Fask.Console.Interfaces.DataSets.Vyroba GetFaskCons095()
        {
            Console.Interfaces.Classes.ZboziVyrobaListFiltr filtr = new Console.Interfaces.Classes.ZboziVyrobaListFiltr();
            Fask.Console.Interfaces.DataSets.Vyroba ds = this.GetFiltrovaneZbozi(filtr);

            //var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
            //lta.Connection.ConnectionString = this.ConnectionString;
            //lta.Fill(ds.FASK_CONS_095);

            return ds;
        }

        #endregion

        #region IZboziVyroba_GetFiltrovaneZbozi Members
        public Fask.Console.Interfaces.DataSets.Vyroba GetFiltrovaneZbozi(Console.Interfaces.Classes.ZboziVyrobaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Console.Interfaces.DataSets.Vyroba ds = new Fask.Console.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_CONS_095 + " zbozi " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.MaterialID))
                {
                    command.CommandText += "and zbozi.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.MaterialID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                {
                    command.CommandText += "and zbozi.ITEMDESC like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialItemcode))
                {
                    command.CommandText += "and zbozi.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialItemcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialBarcode))
                {
                    command.CommandText += "and (zbozi.CZ_CarKod=@barcode or zbozi.VNDITNUM=@barcode) ";
                    command.Parameters.AddWithValue("@barcode", filtr.MaterialBarcode);
                }

                if (filtr.ZobrazitDuplicitniCaroveKody)
                {
                    command.CommandText +=
                        "and zbozi.VNDITNUM IN ( " +
                        "   SELECT VNDITNUM " +
                        "   FROM " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_ZASOBY + " " +
                        "   where VNDITNUM <> '' " +
                        "   group by VNDITNUM " +
                        "   HAVING COUNT(*) > 1" +
                        ") "
                        ;
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_CONS_095);

                return ds;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region IZboziVyroba_InsertZbozi Members

        public int InsertZbozi(string ITEMNMBR, string ITEMDESC, string VNDITNUM, string CZ_CarKod, string LOCNCODE, string SKL_ID, decimal QTY, decimal? QTYPACK, string MJ, string DMJ, decimal? TAXRATE, decimal? PRICE0, decimal? PRICE1, decimal? PRICE2, decimal? PRICE3, decimal? PRICE4, decimal? PRICE5, byte CZ_SerNum_Track, short CZ_SerNum_Delka, byte CZ_Rez1_Track, byte CZ_Rez2_Track, byte CZ_Rez3_Track, byte CZ_Rez4_Track, string REZ1, string ITEMCODE, string ODB_ID, float TIMEPREP, float TIMEUNIT, DateTime? TIMEFROM, DateTime? TIMETO, DateTime LSTMod, string loginid, int TIMEMODE)
        {
            SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            return ta.Insert(
                 ITEMNMBR,
                 ITEMDESC,
                 VNDITNUM,
                 CZ_CarKod,
                 LOCNCODE,
                 SKL_ID,
                 QTY,
                 QTYPACK,
                 MJ,
                 DMJ,
                 TAXRATE,
                 PRICE0,
                 PRICE1,
                 PRICE2,
                 PRICE3,
                 PRICE4,
                 PRICE5,
                 CZ_SerNum_Track,
                 CZ_SerNum_Delka,
                 CZ_Rez1_Track,
                 CZ_Rez2_Track,
                 CZ_Rez3_Track,
                 CZ_Rez4_Track,
                 REZ1,
                 ITEMCODE,
                 ODB_ID,
                 TIMEPREP,
                 TIMEUNIT,
                 TIMEFROM,
                 TIMETO,
                 LSTMod,
                 loginid,
                 TIMEMODE
                );
        }

        #endregion

        #region IZboziVyroba_UpdateZbozi Members

        public int UpdateZbozi(System.Data.DataRow dataRow)
        {
            SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new SqlConnection(ConnectionString);

            return ta.Update(dataRow);
        }

        #endregion

        #region IZboziVyroba_DeleteZbozi Members

        public int DeleteZbozi(string ITEMNMBR)
        {
            SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            return ta.Delete(ITEMNMBR);
        }

        #endregion

        #region IZboziVyroba_UpdateZboziTable Members

        public int UpdateZboziTable(Console.Interfaces.DataSets.Vyroba.FASK_CONS_095DataTable dt)
        {
            SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new SQL_Datasets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new SqlConnection(ConnectionString);
            return ta.Update(dt.ToArray());
        }

        #endregion


        #endregion


    }
}
