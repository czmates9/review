using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Classes;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : Fask.Interfaces.Inventura.IInventura2,
        Fask.Interfaces.Inventura.IInventura2_GetHlavicky,
        Fask.Interfaces.Inventura.IInventura2_GetHlavickaByID,
        Fask.Interfaces.Inventura.IInventura2_GetFiltrovaneNasnimane,
         Fask.Interfaces.Inventura.IInventura2_GetFiltrovanaPredloha,
        Fask.Interfaces.Inventura.IInventura2_GenerateInventura,
        Fask.Interfaces.Inventura.IInventura2_ImportInventura,
        Fask.Interfaces.Inventura.IInventura2_GetFiltrovanaINVPredloha,
        Fask.Interfaces.Inventura.IInventura2_GetCompare_I4,
        Fask.Interfaces.Inventura.IInventura2_GetCompare_I123
    {

        private string TABLE_CZMST_I1 = "CZMST_I1";


        #region IInventura2_GetHlavicky Members

        public Fask.Interfaces.DataSets.Inventura GetHlavicky()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();

                // dopocitani rozdilu mnozstvi v davce
                command.CommandText =
                    "select i1.*, isnull(N.I4SUMQTY, 0) as Nasnimano, i1.QUANTITY - isnull(N.I4SUMQTY, 0) as Stav " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_I1 + " i1 " +
                    "left join ( " +
                    "   SELECT i4.CountEntries I4DAVKA, i4.itemnmbr I4ITEM, SUM(i4.QUANTITY) I4SUMQTY " +
                    "   from " + Fask.SQL.Constants.Common.TABLE_CZMST_I4 + " i4  " +
                    "   group by I4.countentries, I4.ITEMNMBR " +
                    "   ) as N " +
                    "   ON N.I4ITEM=i1.ITEMNMBR " ;

                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "and N.I4DAVKA=@countentries ";
                }   
                    
                   command.CommandText += "WHERE 1=1 ";


                                // hledaní ITEMNMBR
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
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
                    "select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    "   where USERID=@user " +
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

        #region IInventura2_GenerateInventura Members

        public Fask.Interfaces.Classes.StatusInfo GenerateInventura()
        {
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();
            bool succes = false;

            Globals_V1.LoadConfiguration();

            succes = Classes.Inventura.LoadInventura();

            if (succes)
            {
                si.ID = 0;
                si.Description = "Inventura úspěšně exportována";
                return si;
            }
            else
            {
                si.ID = -1;
                si.Description = "Export inventury se nezdařil";
                return si;
            }
        }

        #endregion

        #region IInventura2_ImportInventura Members

        public Fask.Interfaces.Classes.StatusInfo ImportInventura(string CountEntries)
        {
            SqlConnection sqlcon = null;
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();

            Globals_V1.LoadConfiguration();

            try
            {
           //     string statusFile = (new Uri(System.IO.Path.Combine(
           //System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
           //@"..\ExportImport\Inventura1Export.so"))).LocalPath;

           //     so = new StatusObject(statusFile);
           //     if (so.Exists)
           //     {
           //         lblUkoncitText.Text = "Export inventury se nezdařil - jiz probiha";
           //         return;
           //     }

                // nacteni poctu neuzavrenych zaznamu inventury
                string selectTermID = "select COUNT(*) from " + TABLE_CZMST_I1 + " where TerminalID<100 and CountEntries=" + CountEntries.Trim();
                Globals_V1.LoadConfiguration();
                sqlcon = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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

                //so.Write("probiha export");
                bool succes = false;


                string updatei1 = "Update " + TABLE_CZMST_I1 + " set TerminalID= TerminalID + 100 where CountEntries=" + CountEntries.Trim();


                //MST_Pohoda.LoadConfiguration();



                succes = Classes.Inventura.ImportInventura(int.Parse(CountEntries.Trim()), true);

                //sqlcon = new SqlConnection(Globals.ConnectionString);
                //sqlcon.Open();

                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, sqlcon);


                int rows = command.ExecuteNonQuery();

                if (succes)
                {
                    //lblUkoncitText.Text = "Inventura úspěšně importována";
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

                //if (so != null)
                //    so.Delete();

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
                Globals_V1.LoadConfiguration();

                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();


                command.CommandText = "SELECT I1.CountEntries, I1.CE_Orig, I1.ITEMNMBR, I1.CZ_CarKod, I1.ITEMDESC, I1.LOCNCODE, I1.SKL_ID, I1.QUANTITY, I1.DMJ, I1.DATEDONE, I1.IntegerValue, I1.TIMESPRT, I1.CZ_SerNum_Track, I1.CZ_SerNum_Find, I1.DEX_ROW_ID, I1.TerminalID, I1.O_TID, I1.REZ_1, I1.REZ_2, I1.ITEMCODE, I1.CZ_REZ1_Track, I1.CZ_REZ2_Track, I1.CZ_Expirace_Track";
                command.CommandText += ", Sklad.SKL_DESC";

                if (filtr.ZobrazitAlternativnyCaroveKody)
                {
                    command.CommandText += ", I3.CE_Orig as I3_CE_Orig" +
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
                    command.CommandText += ", I2.CE_Orig as I2_CE_Orig" +
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
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
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

                if (filtr.LOCNCODE != null && !string.IsNullOrEmpty(filtr.LOCNCODE.Trim()))
                {
                    // cislo davky
                    command.CommandText += "AND I1.LOCNCODE=@LOCNCODE ";
                    command.Parameters.AddWithValue("@LOCNCODE", filtr.LOCNCODE);
                }

                if(filtr.ZobrazitZakladni)
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
                else if(filtr.ZobrazitSarze)
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
                            command.CommandText += " order by I1.CountEntries, I1.ITEMNMBR, I2_Expirace  desc ";
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
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
    }
}
