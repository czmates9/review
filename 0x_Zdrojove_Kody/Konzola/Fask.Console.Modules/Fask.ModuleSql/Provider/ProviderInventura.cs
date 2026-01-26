using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Classes;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Interfaces.Inventura.IInventura2,
        Fask.Interfaces.Inventura.IInventura2_GetHlavicky,
        Fask.Interfaces.Inventura.IInventura2_GetHlavickaByID,
        Fask.Interfaces.Inventura.IInventura2_GetFiltrovaneNasnimane,
        Fask.Interfaces.Inventura.IInventura2_GetFiltrovanaPredloha,
        Fask.Interfaces.Inventura.IInventura2_SloucitInventury,
        Fask.Interfaces.Inventura.IInventura2_RozdelitInventury,
        Fask.Interfaces.Inventura.IInventura2_Generate_FromFile_Inventura,
        Fask.Interfaces.Inventura.IInventura2_Import_ToXML_Inventura,
        Fask.Interfaces.Inventura.IInventura2_GetFiltrovanaINVPredloha,

        Fask.Interfaces.Inventura.IInventura2_GetCompare_I4,
        Fask.Interfaces.Inventura.IInventura2_GetCompare_I123,
        Fask.Interfaces.Inventura.IInventura2_UpdateI1
    {

        #region IInventura2_GetHlavicky Members

        public Fask.Interfaces.DataSets.Inventura GetHlavicky()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();
            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();

                command.CommandText =
                    "select * " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_I1H;

                ds.Clear();
                ds.AcceptChanges();

                command.Connection = connection;
                adapter.SelectCommand = command;

                //naplnim data ...
                adapter.Fill(ds.CZMST_I1H);
            }
            catch
            {
                throw;
            }

            return ds;
        }

        #endregion

        #region IInventura2_GetHlavickaByID Members

        public Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow GetHlavickaByID(int countentries)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_I1H + " " +
                    "where " +
                    "CountEntries=@id ";

                adapter.SelectCommand = command;
                command.Parameters.AddWithValue("@id", countentries);
                adapter.Fill(ds.CZMST_I1H);

                if (ds.CZMST_I1H.Count > 0)
                    return ds.CZMST_I1H.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IInventura2_GetFiltrovanaPredloha Members

        public Fask.Interfaces.DataSets.Inventura GetFiltrovanaPredloha(Fask.Interfaces.Filtry.InventuraPredlohaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();

                
                if (filtr.ZobrazitAlternativnyCaroveKody)
                {

                    command.CommandText = "select " +
                        "i1.CountEntries, i1.ITEMNMBR, i3.VNDITNUM as CZ_CarKod , i1.ITEMDESC, i1.LOCNCODE, i1.SKL_ID, i1.QUANTITY, i3.MJ as DMJ, i1.DATEDONE, i1.IntegerValue, i1.TIMESPRT, i1.CZ_SerNum_Track, i1.CZ_SerNum_Find, i1.DEX_ROW_ID, i1.TerminalID, i1.O_TID, i1.REZ_1, i1.REZ_2, i1.ITEMCODE, " +
                        " isnull(N.I4SUMQTY, 0) as Nasnimano, i1.QUANTITY - isnull(N.I4SUMQTY, 0) as Stav " +
                        "from " + Fask.SQL.Constants.Common.TABLE_CZMST_I1 + " i1 " +
                        "left join ( " +
                        " SELECT i4.CountEntries I4DAVKA, i4.itemnmbr I4ITEM, SUM(i4.QUANTITY) I4SUMQTY " +
                        " from " + Fask.SQL.Constants.Common.TABLE_CZMST_I4 + " i4  " +
                        " group by I4.countentries, I4.ITEMNMBR " +
                        " ) as N " +
                        " ON N.I4ITEM=i1.ITEMNMBR " +
                        " left join " + Fask.SQL.Constants.Common.TABLE_CZMST_I3 + " as i3 ON i3.ITEMNMBR = i1.ITEMNMBR ";

                }
                else
                {

                    command.CommandText =
                        "select i1.*, isnull(N.I4SUMQTY, 0) as Nasnimano, i1.QUANTITY - isnull(N.I4SUMQTY, 0) as Stav " +
                        "from " + Fask.SQL.Constants.Common.TABLE_CZMST_I1 + " i1 " +
                        "left join ( " +
                        "   SELECT i4.CountEntries I4DAVKA, i4.itemnmbr I4ITEM, SUM(i4.QUANTITY) I4SUMQTY " +
                        "   from " + Fask.SQL.Constants.Common.TABLE_CZMST_I4 + " i4  " +
                        "   group by I4.countentries, I4.ITEMNMBR " +
                        "   ) as N " +
                        "   ON N.I4ITEM=i1.ITEMNMBR ";

                }

                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "and N.I4DAVKA=@countentries ";
                }

                command.CommandText += "WHERE 1=1 ";

                // hledaní CountEntries
                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    // cislo davky
                    command.CommandText += "AND i1.CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }


                // hledaní ITEMNMBR
                if (filtr.MaterialID != null && !string.IsNullOrEmpty(filtr.MaterialID.Trim()))
                {
                    //command.CommandText += "AND (pohyb.ITEMDESC like '%' +  @nazevmat + '%' or pohyb.ITEMNMBR like '%' +  @nazevmat + '%') ";
                    command.CommandText += "AND i1.ITEMNMBR=@nazevmat ";
                    command.Parameters.AddWithValue("@nazevmat", filtr.MaterialID.Trim());
                    //if (StavSkladuFiltr.rowMaterialID != null)
                    //{
                    //    command.CommandText += " AND stavmat.ITEMNMBR=@nazevmat ";
                    //}
                    //else
                    //{
                    //    command.CommandText += "AND (stavmat.ITEMDESC like @nazevmat or stavmat.ITEMNMBR like @nazevmat or stavmat.ITEMCODE like @nazevmat)";
                    //}
                    //command.Parameters.AddWithValue("@nazevmat", StavSkladuFiltr.rowMaterialID != null ? StavSkladuFiltr.rowMaterialID.ITEMNMBR.Trim() : ("%" + StavSkladuFiltr.MaterialID.Trim() + "%"));
                }

                // hledaní podle lokace
                if (filtr.MaterialLocncode != null && !string.IsNullOrEmpty(filtr.MaterialLocncode.Trim()))
                {
                    command.CommandText += "AND i1.LOCNCODE=@lokace ";

                    command.Parameters.AddWithValue("@lokace", filtr.MaterialLocncode.Trim());
                }

                // hledaní podle skladu
                if (!string.IsNullOrEmpty(filtr.rowMaterialSKLID.Trim()) || !string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()))
                {
                    command.CommandText += " AND i1.SKL_ID=@sklad ";
                    command.Parameters.AddWithValue("@sklad", !string.IsNullOrEmpty(filtr.rowMaterialSKLID.Trim()) ? filtr.rowMaterialSKLID : filtr.MaterialSKLID);
                }
                //if (filtr.MaterialSKLID != null && !string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()))
                //{
                //    command.CommandText += " AND i1.SKL_ID=@sklad ";
                //    command.Parameters.AddWithValue("@sklad", filtr.rowMaterialSKLID != null ? filtr.rowMaterialSKLID.skl_id.Trim() : filtr.MaterialSKLID.Trim());
                //}

                // zobrazeni pouze nenasnimanych polozek
                if (filtr.ZobrazitPouzeNenasnimane)
                {
                    command.CommandText += " AND isnull(N.I4SUMQTY, 0)=0 ";
                }

                command.CommandText += "order by DATEDONE desc";

                //ds.CZMST_Sklad_Pohyb.Clear();
                //ds.CZMST_Sklad_Pohyb.AcceptChanges();

                command.Connection = connection;
                adapter.SelectCommand = command;

                ds.CZMST_I1.BeginLoadData();
                //naplnim data ...
                adapter.Fill(ds.CZMST_I1);

                ds.CZMST_I1.EndLoadData();
                ds.CZMST_I1.AcceptChanges();

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IInventura2_GetFiltrovaneNasnimane Members

        public Fask.Interfaces.DataSets.Inventura GetFiltrovaneNasnimane(Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();

                command.CommandText = "select i4.*, uzivatel.USERID as UzivatelLogin, uzivatel.FIRSTNAME as UzivatelJmeno,uzivatel.surname as UzivatelPrijmeni, i1.QUANTITY as Pozadovano, i1.ITEMDESC as MaterialOznaceni ";
                command.CommandText += "from " + Fask.SQL.Constants.Common.TABLE_CZMST_I4 + " i4 ";
                command.CommandText += "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_I1 + " i1 on i1.itemnmbr = i4.itemnmbr and i1.CountEntries=i4.CountEntries and i1.SKL_ID=i4.SKL_ID ";
                command.CommandText += "left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " uzivatel on uzivatel.USERID = i4.USERID ";
                command.CommandText += "WHERE ";

                // cislo davky
                command.CommandText += "i4.CountEntries=@countentries ";
                command.Parameters.AddWithValue("@countentries", filtr.CountEntries);

                // hledani podle uzivatele
                if (!string.IsNullOrEmpty(filtr.rowUzivatel.Trim()) || !string.IsNullOrEmpty(filtr.UzivatelID.Trim()))
                {
                    // po konzultaci s JaS vyhledavat pouze podle loginu ...
                    command.CommandText += "AND i4.USERID IN ( " +
                    "select distinct ID from CZMSTPWD " +
                    "   where LOGIN=@user " +
                    ") ";

                    command.Parameters.AddWithValue("@user", !string.IsNullOrEmpty(filtr.rowUzivatel) ? filtr.rowUzivatel : filtr.UzivatelID);
                }
                //if (filtr.UzivatelID != null && !string.IsNullOrEmpty(filtr.UzivatelID.Trim()))
                //{
                //    if (filtr.rowUzivatel != null)
                //    {
                //        command.CommandText += "AND i4.USERID=@user ";
                //        //da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel.ID);
                //    }
                //    else
                //    {
                //        // po konzultaci s JaS vyhledavat pouze podle loginu ...
                //        command.CommandText += "AND i4.USERID IN ( " +
                //        "select distinct ID from CZMSTPWD " +
                //        "   where LOGIN=@user " +
                //        ") ";
                //        //da_filter.SelectCommand.Parameters.AddWithValue("@name", comboBoxUzivatel.Text);
                //    }

                //    command.Parameters.AddWithValue("@user", filtr.rowUzivatel != null ? filtr.rowUzivatel.ID.ToString() : filtr.UzivatelID);
                //}

                // hledaní ITEMNMBR
                if (filtr.MaterialID != null && !string.IsNullOrEmpty(filtr.MaterialID.Trim()))
                {
                    //command.CommandText += "AND (pohyb.ITEMDESC like '%' +  @nazevmat + '%' or pohyb.ITEMNMBR like '%' +  @nazevmat + '%') ";
                    command.CommandText += "AND i4.ITEMNMBR=@nazevmat ";
                    command.Parameters.AddWithValue("@nazevmat", filtr.MaterialID.Trim());
                    //if (StavSkladuFiltr.rowMaterialID != null)
                    //{
                    //    command.CommandText += " AND stavmat.ITEMNMBR=@nazevmat ";
                    //}
                    //else
                    //{
                    //    command.CommandText += "AND (stavmat.ITEMDESC like @nazevmat or stavmat.ITEMNMBR like @nazevmat or stavmat.ITEMCODE like @nazevmat)";
                    //}
                    //command.Parameters.AddWithValue("@nazevmat", StavSkladuFiltr.rowMaterialID != null ? StavSkladuFiltr.rowMaterialID.ITEMNMBR.Trim() : ("%" + StavSkladuFiltr.MaterialID.Trim() + "%"));
                }

                // hledaní podle lokace
                if (filtr.MaterialLocncode != null && !string.IsNullOrEmpty(filtr.MaterialLocncode.Trim()))
                {
                    command.CommandText += "AND i4.LOCNCODE=@lokace ";

                    command.Parameters.AddWithValue("@lokace", filtr.MaterialLocncode.Trim());
                }

                // hledaní podle skladu
                if (!string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()) || !string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()))
                {
                    command.CommandText += " AND i4.SKL_ID=@sklad ";
                    command.Parameters.AddWithValue("@sklad", !string.IsNullOrEmpty(filtr.rowMaterialSKLID.Trim()) ? filtr.rowMaterialSKLID : filtr.MaterialSKLID);
                }
                //if (filtr.MaterialSKLID != null && !string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()))
                //{
                //    command.CommandText += " AND i4.SKL_ID=@sklad ";
                //    command.Parameters.AddWithValue("@sklad", filtr.rowMaterialSKLID != null ? filtr.rowMaterialSKLID.skl_id.Trim() : filtr.MaterialSKLID.Trim());
                //}

                command.CommandText += "order by DATEDONE desc, TIMEDONE desc";

                //ds.CZMST_Sklad_Pohyb.Clear();
                //ds.CZMST_Sklad_Pohyb.AcceptChanges();

                command.Connection = connection;
                adapter.SelectCommand = command;

                ds.CZMST_I1.BeginLoadData();
                //naplnim data ...
                adapter.Fill(ds.CZMST_I4);

                ds.CZMST_I1.EndLoadData();
                ds.CZMST_I1.AcceptChanges();

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IInventura2_SloucitInventury Members
        public Fask.Interfaces.Classes.StatusInfo SloucitInventury(List<string> CountEntries, string sloucenaI)
        {
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlTransaction trans = null;

            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            command = new System.Data.SqlClient.SqlCommand();

            try
            {
                command.Connection = connection;
                command.Connection.Open();
                trans = command.Connection.BeginTransaction();

                // TODO : testy na existenci davky, pripadne dotaz na prepsani/smazani existujicich dat predlohy ... ???
                string deletecommand =
                    "Delete [czmst_i1h] where countentries = " + sloucenaI + ";" +
                    //"Delete [czmst_i1p] where countentries = " + sloucenaI + ";" +
                    "Delete [czmst_i1] where countentries = " + sloucenaI + ";" +
                    "Delete [czmst_i2] where countentries = " + sloucenaI + ";" +
                    "Delete [czmst_i3] where countentries = " + sloucenaI + ";" +
                    "";

                string czmst_i1h =
                    "INSERT INTO [CZMST_I1H]" +
                    " ([CountEntries] ,[Description] ,[State]) VALUES " +
                    " (" + sloucenaI + ",'Sloučené : " + string.Join(",", CountEntries) + "',0)";

                string czmst_i1_sn =
                    "INSERT INTO CZMST_I1 " +
                    "  SELECT distinct " + sloucenaI + " [CountEntries]" +
                    //"      ,[CountEntries] [CE_Orig]" +
                    "      ,NULL [CE_Orig]" +   // ceorig je potlaceno
                    "      ,[ITEMNMBR]" +
                    "      ,[CZ_CarKod]" +
                    "      ,[ITEMDESC]" +
                    "      ,[LOCNCODE]" +
                    "      ,[SKL_ID]" +
                    "      ,[QUANTITY]" +
                    "      ,[DMJ]" +
                    //"      ,[DATEDONE]" +
                    "      ,getdate() [DATEDONE]" +
                    "      ,[IntegerValue]" +
                    "      ,[TIMESPRT]" +
                    "      ,[CZ_SerNum_Track]" +
                    "      ,[CZ_SerNum_Find]" +
                    //"      ,[DEX_ROW_ID]" +
                    "      ,[TerminalID]" +
                    "      ,[O_TID]" +
                    "      ,[REZ_1]" +
                    "      ,[REZ_2]" +
                    "      ,[ITEMCODE]" +
                    "      ,[CZ_REZ1_Track]" +
                    "      ,[CZ_REZ2_Track]" +
                    "  FROM [CZMST_I1]" +
                    "  WHERE [CountEntries] IN (" + String.Join(",", CountEntries) + ")" +
                    "   AND [CZ_SerNum_Track] > 0 " + // 1 sn , 2 sarze
                    "";

                string czmst_i1_mn =
                    "INSERT INTO CZMST_I1 " +
                    "  SELECT distinct " + sloucenaI + " [CountEntries]" +
                    "      ,[CountEntries] [CE_Orig]" +
                    "      ,[ITEMNMBR]" +
                    "      ,[CZ_CarKod]" +
                    "      ,[ITEMDESC]" +
                    "      ,[LOCNCODE]" +
                    "      ,[SKL_ID]" +
                    "      ,[QUANTITY]" +
                    "      ,[DMJ]" +
                    //"      ,[DATEDONE]" +
                    "      ,getdate() [DATEDONE]" +
                    "      ,[IntegerValue]" +
                    "      ,[TIMESPRT]" +
                    "      ,[CZ_SerNum_Track]" +
                    "      ,[CZ_SerNum_Find]" +
                    //"      ,[DEX_ROW_ID]" +
                    "      ,[TerminalID]" +
                    "      ,[O_TID]" +
                    "      ,[REZ_1]" +
                    "      ,[REZ_2]" +
                    "      ,[ITEMCODE]" +
                    "      ,[CZ_REZ1_Track]" +
                    "      ,[CZ_REZ2_Track]" +
                    "  FROM [CZMST_I1]" +
                    "  WHERE [CountEntries] IN (" + String.Join(",", CountEntries) + ")" +
                    "   AND [CZ_SerNum_Track] = 0 " +
                    "";


                string czmst_i2 =
                    "INSERT INTO CZMST_I2 " +
                    " SELECT distinct " + sloucenaI + " [CountEntries]" +
                    "  ,[CountEntries] [CE_Orig]" +
                    "  ,[ITEMNMBR]" +
                    "  ,[SERLNMBR]" +
                    "  ,[QTY]" +
                    //                "  ,[DEX_ROW_ID]" +
                    " FROM [CZMST_I2]" +
                    "  WHERE [CountEntries] IN (" + String.Join(",", CountEntries) + ")";

                string czmst_i3 =
                    "INSERT INTO CZMST_I3 " +
                    " SELECT distinct " + sloucenaI + " [CountEntries]" +
                    //"  ,[CountEntries] [CE_Orig]" +
                    "      ,NULL [CE_Orig]" +
                    "      ,[ITEMNMBR]" +
                    "      ,[CZ_CarKod]" +
                    "      ,[QTYPACK]" +
                    "      ,[MJ]" +
                    "      ,[VENDORID]" +
                    "      ,[VNDITNUM]" +
                    "      ,[VENDNAME]" +
                    "      ,[WEIGHT]" +
                    //                "      ,[DEX_ROW_ID]" +
                    "  FROM [CZMST_I3]" +
                    "  WHERE [CountEntries] IN (" + String.Join(",", CountEntries) + ")";

                command.Transaction = trans;

                command.CommandText = deletecommand;
                int deleted = command.ExecuteNonQuery();

                command.CommandText = czmst_i1h;
                int pzi1h = command.ExecuteNonQuery();

                command.CommandText = czmst_i1_mn;
                int pzi1mn = command.ExecuteNonQuery();

                command.CommandText = czmst_i1_sn;
                int pzi1sn = command.ExecuteNonQuery();

                command.CommandText = czmst_i2;
                int pzi2 = command.ExecuteNonQuery();

                command.CommandText = czmst_i3;
                int pzi3 = command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

            }
            catch (Exception ex)
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                {
                }

                throw ex;
            }
            finally
            {
                if ((connection != null) && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            si.Description = "Vytvořena dávka číslo : " + sloucenaI;
            return si;
        }
        #endregion

        #region IInventura2_RozdelitInventury Members
        public Fask.Interfaces.Classes.StatusInfo RozdelitInventury(List<string> CountEntries)
        {
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlTransaction trans = null;

            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            command = new System.Data.SqlClient.SqlCommand();

            try
            {
                command.Connection = connection;
                command.Connection.Open();
                trans = command.Connection.BeginTransaction();

                string insert_i4 =
                    "INSERT INTO CZMST_I4 " +
                    " SELECT [CE_Orig] [CountEntries]" +
                    "      ,NULL [CE_Orig]" +
                    "      ,[ITEMNMBR]" +
                    "      ,[CZ_CarKod]" +
                    "      ,[LOCNCODE]" +
                    "      ,[SKL_ID]" +
                    "      ,[VNDITNUM]" +
                    "      ,[MJ]" +
                    "      ,[QUANTITY]" +
                    "      ,[QUANTITYMJ]" +
                    "      ,[QTYPACK]" +
                    "      ,[SERLNMBR]" +
                    "      ,[DATEDONE]" +
                    "      ,[TIMEDONE]" +
                    "      ,[USERID]" +
                    //                    "      ,[DEX_ROW_ID]" +
                    "      ,[GUID]" +
                    "      ,[O_Checked]" +
                    "      ,[INPUT_MODE]" +
                    "      ,[ID_TERMINAL]" +
                    "      ,[ITEMCODE]" +
                    "      ,[REZ_1]" +
                    "      ,[REZ_2]" +
                    "      ,[WEIGHT]" +
                    "  FROM [CZMST_I4]" +
                    "   WHERE [CE_Orig] is not NULL and CountEntries in (" + string.Join(",", CountEntries) + ")" +
                    "";

                // po oddeleni, smazu ze zdroje ...
                // pak zustavaji neprirazene zaznamy ...
                string i4_delete =
                    "DELETE CZMST_I4 " +
                    "   WHERE [CE_Orig] is not NULL and CountEntries in (" + string.Join(",", CountEntries) + ")" +
                    "";

                command.Transaction = trans;

                command.CommandText = insert_i4;
                int pzi4 = command.ExecuteNonQuery();

                command.CommandText = i4_delete;
                int pi4delete = command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

            }
            catch (Exception ex)
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                {
                }

                throw ex;
            }
            finally
            {
                if ((connection != null) && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            si.Description = "Dávka(y) rozdělena ...";
            return si;
        }
        #endregion

        #region IInventura2_Import_FromFile_Inventura Members

        public Fask.Interfaces.Classes.StatusInfo Genetare_FromFile_Inventura(Fask.Interfaces.Inventura.TypeFile typSouboru, string CountEntries, string Desc, string Path)
        {
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();

            SQL_Datasets.InventuraTableAdapters.CZMST_I1TableAdapter ta = new SQL_Datasets.InventuraTableAdapters.CZMST_I1TableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            Fask.ModuleSql.SQL_Datasets.InventuraTableAdapters.CZMST_I1HTableAdapter ta1H = new SQL_Datasets.InventuraTableAdapters.CZMST_I1HTableAdapter();
            ta1H.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            Fask.ModuleSql.SQL_Datasets.InventuraTableAdapters.CZMST_I3TableAdapter ta3 = new SQL_Datasets.InventuraTableAdapters.CZMST_I3TableAdapter();
            ta3.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);



            int INT_CountEntries = int.Parse(CountEntries);


            object CountE = ta.Get_CountEntries_ByCountEntries(INT_CountEntries);

            if ((CountE != null) && (CountE is int))
            {
                si.ID = -1;
                si.Description = string.Format("Dávka '{0}' již existuje v předloze.", (int)CountE);

                return si;
            }


            ta1H.Insert(INT_CountEntries, Desc, 0);
            



            if (typSouboru == Fask.Interfaces.Inventura.TypeFile.XML)
            {

                string Node_Hlavni = "STOCK";
                string Node_Row = "ITEM";


                #region pro CZMST_I1
                string Node_ITEMNMBR = "ID";
                string Node_CZ_CarKod = "CODE";
                string Node_ITEMDESC = "NAME";

                string Node_LOCNCODE = "LOCNCODE"; // neni v XML od chevron

                string Node_SKL_ID = "STORE_CODE";
                string Node_QUANTITY = "QUANTITY";
                string Node_DMJ = "UNIT";


                string Node_DATEDONE = "DATEDONE";// neni v XML od chevron
                string Node_IntegerValue = "IntegerValue";// neni v XML od chevron
                string Node_TIMESPRT = "TIMESPRT";// neni v XML od chevron
                string Node_CZ_SerNum_Track = "CZ_SerNum_Track";// neni v XML od chevron
                string Node_CZ_SerNum_Find = "CZ_SerNum_Find";// neni v XML od chevron
                string Node_TerminalID = "TerminalID";// neni v XML od chevron
                string Node_O_TID = "O_TID";// neni v XML od chevron
                string Node_REZ_1 = "REZ_1";// neni v XML od chevron
                string Node_REZ_2 = "REZ_2";// neni v XML od chevron
                string Node_ITEMCODE = "ITEMCODE";// neni v XML od chevron
                string Node_CZ_REZ1_Track = "CZ_REZ1_Track";// neni v XML od chevron
                string Node_CZ_REZ2_Track = "CZ_REZ2_Track";// neni v XML od chevron 
                
                #endregion

                #region pro CZMST I3

                string Node_QTYPACK = "QTYPACK";
                string Node_VENDORID = "VENDORID";
                string Node_VNDITNUM = "VNDITNUM";
                string Node_VENDNAME = "VENDNAME";
                string Node_WEIGHT = "WEIGHT";


                #endregion


                XDocument root = XDocument.Load(Path);

                XElement Pack = root.Element(Node_Hlavni); // Hlavni note
                var elementy = Pack.Elements(Node_Row); // Obal pro jednotlive řadky

                foreach (XElement item in elementy)
                {





                    #region pro CZMST_I1
		            
                    
                    string ITEMNMBR = item.Element(Node_ITEMNMBR) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_ITEMNMBR).Value) ? string.Empty : item.Element(Node_ITEMNMBR).Value.Trim());
                    string CZ_CarKod = item.Element(Node_CZ_CarKod) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_CZ_CarKod).Value) ? string.Empty : item.Element(Node_CZ_CarKod).Value.Trim());
                    string ITEMDESC = item.Element(Node_ITEMDESC) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_ITEMDESC).Value) ? string.Empty : item.Element(Node_ITEMDESC).Value.Trim());


                    string LOCNCODE = item.Element(Node_LOCNCODE) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_LOCNCODE).Value) ? string.Empty : item.Element(Node_LOCNCODE).Value.Trim());

                    string SKL_ID = item.Element(Node_SKL_ID) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_SKL_ID).Value) ? string.Empty : item.Element(Node_SKL_ID).Value.Trim());
                    decimal QUANTITY = decimal.Parse(item.Element(Node_QUANTITY) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_QUANTITY).Value) ? "0" : item.Element(Node_QUANTITY).Value.Trim()));
                    string DMJ = item.Element(Node_DMJ) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_DMJ).Value) ? string.Empty : item.Element(Node_DMJ).Value.Trim());


                    System.DateTime DATEDONE = DateTime.Now;

                    if ((item.Element(Node_DATEDONE) != null) && (!string.IsNullOrEmpty(item.Element(Node_DATEDONE).Value)))
                    {
                        if (!DateTime.TryParse(item.Element(Node_DATEDONE).Value.Trim(), out DATEDONE))
                        {
                            DATEDONE = DateTime.Now;
                        }
                    }

                    //System.DateTime DATEDONE = DateTime.Parse(item.Element(Node_QUANTITY) == null ? DateTime.Now : (string.IsNullOrEmpty(item.Element(Node_QUANTITY).Value) ? DateTime.Now.ToString() : ));


                    short IntegerValue = short.Parse(item.Element(Node_IntegerValue) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_IntegerValue).Value) ? "0" : item.Element(Node_IntegerValue).Value.Trim()));
                    short TIMESPRT = short.Parse(item.Element(Node_TIMESPRT) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_TIMESPRT).Value) ? "0" : item.Element(Node_TIMESPRT).Value.Trim()));
                    byte CZ_SerNum_Track = byte.Parse(item.Element(Node_CZ_SerNum_Track) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_CZ_SerNum_Track).Value) ? "0" : item.Element(Node_CZ_SerNum_Track).Value.Trim()));
                    byte CZ_SerNum_Find = byte.Parse(item.Element(Node_CZ_SerNum_Find) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_CZ_SerNum_Find).Value) ? "0" : item.Element(Node_CZ_SerNum_Find).Value.Trim()));
                    byte TerminalID = byte.Parse(item.Element(Node_TerminalID) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_TerminalID).Value) ? "0" : item.Element(Node_TerminalID).Value.Trim()));
                    byte? O_TID = byte.Parse(item.Element(Node_O_TID) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_O_TID).Value) ? "0" : item.Element(Node_O_TID).Value.Trim()));
                    string REZ_1 = item.Element(Node_REZ_1) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_REZ_1).Value) ? string.Empty : item.Element(Node_REZ_1).Value.Trim());
                    string REZ_2 = item.Element(Node_REZ_2) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_REZ_2).Value) ? string.Empty : item.Element(Node_REZ_2).Value.Trim());
                    string ITEMCODE = item.Element(Node_ITEMCODE) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_ITEMCODE).Value) ? string.Empty : item.Element(Node_ITEMCODE).Value.Trim());
                    byte CZ_REZ1_Track = byte.Parse(item.Element(Node_CZ_REZ1_Track) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_CZ_REZ1_Track).Value) ? "0" : item.Element(Node_CZ_REZ1_Track).Value.Trim()));
                    byte CZ_REZ2_Track = byte.Parse(item.Element(Node_CZ_REZ2_Track) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_CZ_REZ2_Track).Value) ? "0" : item.Element(Node_CZ_REZ2_Track).Value.Trim()));
                    
                    #endregion

                    #region pro CZMST_I3 ktere nejsou v I1

                    //int CountEntries = 0; 
                    //string ITEMNMBR= ""; 
                    //string CZ_CarKod= ""; 
                    decimal? QTYPACK= decimal.Parse(item.Element(Node_QTYPACK) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_QTYPACK).Value) ? "0" : item.Element(Node_QTYPACK).Value.Trim()));
                    //string MJ= "";
                    string VENDORID=item.Element(Node_VENDORID) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_VENDORID).Value) ? string.Empty : item.Element(Node_VENDORID).Value.Trim());
                    string VNDITNUM= item.Element(Node_VNDITNUM) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_VNDITNUM).Value) ? string.Empty : item.Element(Node_VNDITNUM).Value.Trim());
                    string VENDNAME = item.Element(Node_VENDNAME) == null ? string.Empty : (string.IsNullOrEmpty(item.Element(Node_VENDNAME).Value) ? string.Empty : item.Element(Node_VENDNAME).Value.Trim());
                    decimal? WEIGHT = decimal.Parse(item.Element(Node_WEIGHT) == null ? "0" : (string.IsNullOrEmpty(item.Element(Node_WEIGHT).Value) ? "0" : item.Element(Node_WEIGHT).Value.Trim()));


                    #endregion




                    ta.Insert(
                    INT_CountEntries,
                    ITEMNMBR,
                    CZ_CarKod,
                    ITEMDESC,
                    LOCNCODE,
                    SKL_ID,
                    QUANTITY,
                    DMJ,
                    DATEDONE,
                    IntegerValue,
                    TIMESPRT,
                    CZ_SerNum_Track,
                    CZ_SerNum_Find,
                    TerminalID,
                    O_TID,
                    REZ_1,
                    REZ_2,
                    ITEMCODE,
                    CZ_REZ1_Track,
                    CZ_REZ2_Track);

                    ta3.Insert(
                        INT_CountEntries,
                        ITEMNMBR,
                        CZ_CarKod,
                        QTYPACK,
                        DMJ,
                        VENDORID,
                        VNDITNUM,
                        VENDNAME,
                        WEIGHT);



                }

       
            }

            si.ID = 1;
            si.Description = "OK";

            return si;
        }

        #endregion

        #region IInventura2_Import_ToXML_Inventura Members

        private string TABLE_CZMST_I1 = "CZMST_I1";

        public Fask.Interfaces.Classes.StatusInfo ImportToXMLInventura(string CountEntries, string Path)
        {
            SqlConnection sqlcon = null;
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();

            try
            {


                // nacteni poctu neuzavrenych zaznamu inventury
                string selectTermID = "select COUNT(*) from " + TABLE_CZMST_I1 + " where TerminalID<100 and CountEntries=" + CountEntries.Trim();

                sqlcon = new SqlConnection(ConnectionString);
                sqlcon.Open();

                // kontrola, zdali jiz byla inventura uzavrena
                System.Data.SqlClient.SqlCommand selectCommand = new System.Data.SqlClient.SqlCommand(selectTermID, sqlcon);
                int count = (int)selectCommand.ExecuteScalar();

                if (count == 0)
                {
                    si.ID = 0;
                    si.Description = "Inventura již byla ukončena!!";
                    return si;
                }

                bool succes = false;


                string updatei1 = "Update " + TABLE_CZMST_I1 + " set TerminalID= TerminalID + 100 where CountEntries=" + CountEntries.Trim();


                succes = Classes.Inventura.Import_ToXML_Inventura(int.Parse(CountEntries.Trim()), true, ConnectionString, Path);


                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, sqlcon);


                int rows = command.ExecuteNonQuery();

                if (succes)
                {
                    si.ID = 0;
                    si.Description = "Inventura úspěšně importována";
                    return si;
                }
                else
                {
                    si.ID = -1;
                    si.Description = "Import inventury se nezdařil";
                    return si;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();



            }
        }


        #endregion

        #region IInventura2_GetFiltrovanaINVPredloha Members

        public Inventura GetFiltrovanaINVPredloha(Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();


                command.CommandText = "SELECT I1.CountEntries, I1.CE_Orig, I1.ITEMNMBR, I1.CZ_CarKod, I1.ITEMDESC, I1.LOCNCODE, I1.SKL_ID, I1.QUANTITY, I1.DMJ, I1.DATEDONE, I1.IntegerValue, I1.TIMESPRT, I1.CZ_SerNum_Track, I1.CZ_SerNum_Find, I1.DEX_ROW_ID, I1.TerminalID, I1.O_TID, I1.REZ_1, I1.REZ_2, I1.ITEMCODE, I1.CZ_REZ1_Track, I1.CZ_REZ2_Track, I1.CZ_Expirace_Track";
                command.CommandText += ", Sklad.SKL_DESC";

                if (filtr.ZobrazitAlternativnyCaroveKody)
                {
                    command.CommandText +=  ", I3.CE_Orig as I3_CE_Orig" +
                                            ", I3.CZ_CarKod as I3_CZ_CarKod" +
                                            ", I3.QTYPACK as I3_QTYPACK" +
                                            ", I3.MJ as I3_MJ" +
                                            ", I3.VENDORID as I3_VENDORID" +
                                            ", I3.VNDITNUM as I3_VNDITNUM" +
                                            ", I3.VENDNAME as I3_VENDNAME" +
                                            ", I3.WEIGHT as I3_WEIGHT" +
                                            ", I3.DEX_ROW_ID as I3_DEX_ROW_ID";
                }

                if (filtr.ZobrazitSarze)
                {
                    command.CommandText +=  ", I2.CE_Orig as I2_CE_Orig" +
                                            " , I2.SERLNMBR as I2_SERLNMBR" +
                                            " , I2.QTY as I2_QTY" +
                                            " , I2.DEX_ROW_ID as I2_DEX_ROW_ID" +
                                            " , I2.Expirace as I2_Expirace";
                            
                }

                command.CommandText += " FROM CZMST_I1 as I1";
                command.CommandText += " LEFT JOIN CZMST093 as Sklad ON Sklad.SKL_ID = I1.SKL_ID";


                if (filtr.ZobrazitAlternativnyCaroveKody)
                {
                    command.CommandText += " LEFT JOIN CZMST_I3 as I3 ON I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries";
                }

                if (filtr.ZobrazitSarze)
                {
                    command.CommandText += " LEFT JOIN CZMST_I2 as I2 ON I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries";
                }

                command.CommandText += " WHERE 1=1 ";

                // hledaní CountEntries
                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    // cislo davky
                    command.CommandText += "AND I1.CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }

                if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
                {
                    // cislo davky
                    command.CommandText += "AND I1.ITEMNMBR=@ITEMNMBR ";
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.CountEntries);
                }

                if (filtr.SKL_ID != null && !string.IsNullOrEmpty(filtr.SKL_ID.Trim()))
                {
                    // cislo davky
                    command.CommandText += "AND I1.SKL_ID=@SKL_ID ";
                    command.Parameters.AddWithValue("@SKL_ID", filtr.SKL_ID);
                }

                if (filtr.LOCNCODE != null && !string.IsNullOrEmpty(filtr.LOCNCODE.Trim()))
                {
                    // cislo davky
                    command.CommandText += "AND I1.LOCNCODE=@LOCNCODE ";
                    command.Parameters.AddWithValue("@LOCNCODE", filtr.LOCNCODE);
                }

                if (filtr.ZobrazitZakladni)
                {

                    switch (filtr.alternativaRazeni)
                    {
                        case 0:
                            command.CommandText += " ";
                            break;
                        case 1:
                            command.CommandText += " order by I1.CountEntries, I1.ITEMNMBR  desc ";
                            break;
                        case 2:
                            command.CommandText += " ";
                            break;
                        case 3:
                            command.CommandText += " ";
                            break;
                        default:
                            break;
                    }


                }
                else if (filtr.ZobrazitSarze)
                {
                    switch (filtr.alternativaRazeni)
                    {
                        case 0:
                            command.CommandText += " ";
                            break;
                        case 1:
                            command.CommandText += " order by I1.CountEntries, I1.ITEMNMBR, I2_SERLNMBR  desc ";
                            break;
                        case 2:
                            command.CommandText += " order by I1.CountEntries, I1.ITEMNMBR, I2_SERLNMBR, I2_Expirace  desc ";
                            break;
                        case 3:
                            command.CommandText += " ";
                            break;
                        default:
                            break;
                    }
                }
                else if (filtr.ZobrazitAlternativnyCaroveKody)
                {
                    switch (filtr.alternativaRazeni)
                    {
                        case 0:
                            command.CommandText += " ";
                            break;
                        case 1:
                            command.CommandText += " ";
                            break;
                        case 2:
                            command.CommandText += " ";
                            break;
                        case 3:
                            command.CommandText += " ";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (filtr.alternativaRazeni)
                    {
                        case 0:
                            command.CommandText += " ";
                            break;
                        case 1:
                            command.CommandText += " ";
                            break;
                        case 2:
                            command.CommandText += " ";
                            break;
                        case 3:
                            command.CommandText += " ";
                            break;
                        default:
                            break;
                    }
                }

                command.Connection = connection;
                adapter.SelectCommand = command;

                ds.CZMST_I1.BeginLoadData();
                //naplnim data ...
                adapter.Fill(ds.CZMST_I1_Predloha);

                ds.CZMST_I1_Predloha.EndLoadData();
                ds.CZMST_I1_Predloha.AcceptChanges();

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IInventura2_GetCompare_I4 Members

        public Inventura_Compare GetCompare_I4(Fask.Interfaces.Filtry.InventuraCompare filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Inventura_Compare ds = new Inventura_Compare();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;


                command.CommandText =
                                    " SELECT " +
                                    " CountEntries, " +
                                    " ITEMNMBR, " +
                                    " ITEMDESC, " +
                                    " ITEMCODE, " +
                                    " VNDITNUM, " +
                                    " SKL_ID, " +
                                    " SKL_DESC, " +
                                    " QUANTITY, " +
                                    " MJ, " +
                                    " SERLNMBR, " +
                                    " Expirace, " +
                                    " status " +
                                    " FROM FASK_Get_InventuraCompare_I4 (" +
                                    "'" + filtr.CountEntries + "'" +
                                    "," + (filtr.V_0 ? "1" : "0") +
                                    "," + (filtr.V_1 ? "1" : "0") +
                                    "," + (filtr.V_2 ? "1" : "0") +
                                    "," + (filtr.V_3 ? "1" : "0") +
                                    "," + (filtr.V_4 ? "1" : "0") +
                                    ") ";

                command.CommandText += " WHERE 1 = 1 ";


                if (!string.IsNullOrEmpty(filtr.SklID))
                {

                    command.CommandText += " AND SKL_ID = '";
                    command.CommandText += filtr.SERLTNUM.Trim();
                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMCODE))
                {

                    command.CommandText += " AND ITEMCODE like '";

                    if (filtr.ITEMCODE_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMCODE.Trim();


                    if (filtr.ITEMCODE_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMDESC))
                {

                    command.CommandText += " AND ITEMDESC like '";

                    if (filtr.ITEMDESC_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMDESC.Trim();


                    if (filtr.ITEMDESC_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.SERLTNUM))
                {

                    command.CommandText += " AND SERLNMBR like '";

                    if (filtr.SERLTNUM_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.SERLTNUM.Trim();


                    if (filtr.SERLTNUM_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }


                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_I4);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IInventura2_GetCompare_I123 Members

        public Inventura_Compare GetCompare_I123(Fask.Interfaces.Filtry.InventuraCompare filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Inventura_Compare ds = new Inventura_Compare();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;


                command.CommandText =
                                    " SELECT " +
                                    " CountEntries, " +
                                    " ITEMNMBR, " +
                                    " ITEMDESC, " +
                                    " ITEMCODE, " +
                                    " VNDITNUM, " +
                                    " SKL_ID, " +
                                    " SKL_DESC, " +
                                    " QUANTITY, " +
                                    " MJ, " +
                                    " SERLNMBR, " +
                                    " Expirace, " +
                                    " status " +
                                    " FROM FASK_Get_InventuraCompare_I123 (" +
                                    "'" + filtr.CountEntries + "'" +
                                    "," + (filtr.V_0 ? "1" : "0") +
                                    "," + (filtr.V_1 ? "1" : "0") +
                                    "," + (filtr.V_2 ? "1" : "0") +
                                    "," + (filtr.V_3 ? "1" : "0") +
                                    "," + (filtr.V_4 ? "1" : "0") +
                                    ") ";

                command.CommandText += " WHERE 1 = 1 ";


                if (!string.IsNullOrEmpty(filtr.SklID))
                {

                    command.CommandText += " AND SKL_ID = '";
                    command.CommandText += filtr.SERLTNUM.Trim();
                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMCODE))
                {

                    command.CommandText += " AND ITEMCODE like '";

                    if (filtr.ITEMCODE_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMCODE.Trim();


                    if (filtr.ITEMCODE_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMDESC))
                {

                    command.CommandText += " AND ITEMDESC like '";

                    if (filtr.ITEMDESC_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMDESC.Trim();


                    if (filtr.ITEMDESC_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.SERLTNUM))
                {

                    command.CommandText += " AND SERLNMBR like '";

                    if (filtr.SERLTNUM_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.SERLTNUM.Trim();


                    if (filtr.SERLTNUM_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }


                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_I123);

                return ds;
            }
            catch
            {
                throw;
            }
        }



        #endregion

        #region I1

        public bool UpdateI1(Inventura.CZMST_I1_PredlohaRow I1Row)
        {
            return Database.Inventura_CZMST_I1.Update(I1Row, ConnectionString) > 0;
            //throw new NotImplementedException();
        }

        #endregion
    }


}
