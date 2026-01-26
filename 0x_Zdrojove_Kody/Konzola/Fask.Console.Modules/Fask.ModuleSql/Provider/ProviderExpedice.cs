using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Interfaces.Expedice.IExpedice2,
        Fask.Interfaces.Expedice.IExpedice2_GetExpedicePolozky,
        Fask.Interfaces.Expedice.IExpedice2_GetFiltrovaneExpedicePolozky,
        Fask.Interfaces.Expedice.IExpedice2_GetBufferPolozky
    {
        #region IExpedice2_GetExpedicePolozky Members

        public Fask.Interfaces.DataSets.Expedice GetExpedicePolozky()
        {
            try
            {
                Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();
                return GetFiltrovaneExpedicePolozky(filtr);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IExpedice2_GetFiltrovaneExpedicePolozky Members

        public Fask.Interfaces.DataSets.Expedice GetFiltrovaneExpedicePolozky(Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Expedice ds = new Fask.Interfaces.DataSets.Expedice();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select polozky.*, hlavicka.PrepravceID, hlavicka.PrepravceSPZ, hlavicka.Rozpracovano, uzivatel.USERID as UserLogin, uzivatel.FIRSTNAME as UserFIRSTNAME, uzivatel.SECONDNAME as UserSECONDNAME, hlavicka.TermID as TermID, hlavicka.UserID as UserID, hlavicka.DateFinished as HlavickaDateFinished, hlavicka.DateCreated as HlavickaDateCreated from " + Fask.SQL.Constants.Common.TABLE_CZMST_EXPEDICE_POLOZKY + " polozky " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_EXPEDICE_HLAVICKA + " hlavicka on hlavicka.ID=polozky.IDH " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " uzivatel on uzivatel.USERID=hlavicka.UserID " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.ITEMNMBR))
                {
                    command.CommandText += "and polozky.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.ITEMNMBR);
                }

                //if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                //{
                //    command.CommandText += "and zbozi.ITEMDESC like '%' + @nazev + '%' ";
                //    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                //}

                if (!string.IsNullOrEmpty(filtr.NMBRPAL))
                {
                    command.CommandText += "and polozky.NMBRPAL=@nmbrpal ";
                    command.Parameters.AddWithValue("@nmbrpal", filtr.NMBRPAL);
                }

                if (!string.IsNullOrEmpty(filtr.Rozpracovano))
                {
                    command.CommandText += "and hlavicka.Rozpracovano=@rozpracovano ";
                    command.Parameters.AddWithValue("@rozpracovano", filtr.Rozpracovano);
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_Expedice_Polozky);

                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IExpedice2_GetBufferPolozky Members

        public Fask.Interfaces.DataSets.Expedice GetBufferPolozky()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Expedice ds = new Fask.Interfaces.DataSets.Expedice();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_EXPEDICE_Baleni_Buffer +
                    //"left join " + Fask.SQL.Constants.Common.TABLE_CZMST_EXPEDICE_HLAVICKA + " hlavicka on hlavicka.ID=polozky.IDH " +
                    //"left join " + Fask.SQL.Constants.Common.TABLE_CZMSTPWD + " uzivatel on uzivatel.ID=hlavicka.UserID " +
                    " where" +
                    " 1=1 "
                    ;

                //if (!string.IsNullOrEmpty(filtr.ITEMNMBR))
                //{
                //    command.CommandText += "and polozky.ITEMNMBR=@itemnmbr ";
                //    command.Parameters.AddWithValue("@itemnmbr", filtr.ITEMNMBR);
                //}

                //if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                //{
                //    command.CommandText += "and zbozi.ITEMDESC like '%' + @nazev + '%' ";
                //    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                //}

                //if (!string.IsNullOrEmpty(filtr.NMBRPAL))
                //{
                //    command.CommandText += "and polozky.NMBRPAL=@nmbrpal ";
                //    command.Parameters.AddWithValue("@nmbrpal", filtr.NMBRPAL);
                //}

                //if (!string.IsNullOrEmpty(filtr.Rozpracovano))
                //{
                //    command.CommandText += "and hlavicka.Rozpracovano=@rozpracovano ";
                //    command.Parameters.AddWithValue("@rozpracovano", filtr.Rozpracovano);
                //}

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_Expedice_Baleni_Buffer);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
