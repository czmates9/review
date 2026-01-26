using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Rady.IRady2,
        Fask.Interfaces.Ciselniky.Rady.IRady2_GetFiltrovaneData,
        Fask.Interfaces.Ciselniky.Rady.IRady2_Delete,
        Fask.Interfaces.Ciselniky.Rady.IRady2_Insert,
         Fask.Interfaces.Ciselniky.Rady.IRady2_Update
    {
        #region IRady2_GetFiltrovaneData Members

        public Fask.Interfaces.DataSets.Rady GetFiltrovaneData(Fask.Interfaces.Filtry.RadyListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Rady ds = new Fask.Interfaces.DataSets.Rady();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    " select * from " + Fask.SQL.Constants.Common.TABLE_FASK_RADY +
                    " where " +
                    " 1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.ID))
                {
                    command.CommandText += "and ID=@ID ";
                    command.Parameters.AddWithValue("@ID", filtr.ID);


                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_RADY);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IRady2_Update Members

        public bool Update(Fask.Interfaces.DataSets.Rady.FASK_RADYRow Row)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.RadyTableAdapters.FASK_RADYTableAdapter ta = new Pohoda_DataSets.RadyTableAdapters.FASK_RADYTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                ta.Update(Row.Default,
                    Row.IsPlatnostOdNull() ? (DateTime?)null : Row.PlatnostOd,
                    Row.IsPlatnostDoNull() ? (DateTime?)null : Row.PlatnostDo,
                    Row.Modul,
                    Row.Modul_ID,
                    Row.Modul_ID2,
                    Row.Modul_Funkce,
                    Row.IsRada_IDNull() ? (int?)null : Row.Rada_ID,
                    Row.Rada_Nazev,
                    Row.Rada_Prefix,
                    Row.IsRada_CountNull() ? (int?)null : Row.Rada_Count,
                    Row.Filtr_SkladID,
                    Row.Filtr_UserID,
                    Row.Vloz_Stredisko,
                    Row.Vloz_Cinnost,
                    Row.Vloz_Zakazka,
                    Row.ID);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IRady2_Insert Members

        public bool Insert(Fask.Interfaces.DataSets.Rady.FASK_RADYRow Row)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.RadyTableAdapters.FASK_RADYTableAdapter ta = new Pohoda_DataSets.RadyTableAdapters.FASK_RADYTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                ta.Insert(  Row.Default,
                    Row.IsPlatnostOdNull() ? (DateTime?)null : Row.PlatnostOd,
                    Row.IsPlatnostDoNull() ? (DateTime?)null : Row.PlatnostDo,
                            Row.Modul,
                            Row.Modul_ID,
                            Row.Modul_ID2,
                            Row.Modul_Funkce,
                            Row.IsRada_IDNull() ? (int?)null : Row.Rada_ID,
                            Row.Rada_Nazev,
                            Row.Rada_Prefix,
                            Row.IsRada_CountNull() ? (int?)null : Row.Rada_Count,
                            Row.Filtr_SkladID,
                            Row.Filtr_UserID,
                            Row.Vloz_Stredisko,
                            Row.Vloz_Cinnost,
                            Row.Vloz_Zakazka);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IRady2_Detete Members

        public bool Delete(int ID)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.RadyTableAdapters.FASK_RADYTableAdapter ta = new Pohoda_DataSets.RadyTableAdapters.FASK_RADYTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                ta.Delete(ID);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
