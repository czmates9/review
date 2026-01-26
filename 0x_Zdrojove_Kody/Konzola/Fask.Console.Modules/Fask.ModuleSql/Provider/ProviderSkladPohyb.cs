using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Interfaces.SkladPohyb.ISkladPohyb2,
        Fask.Interfaces.SkladPohyb.ISkladPohyb2_GetFiltrovanySkladLokace,
        Fask.Interfaces.SkladPohyb.ISkladPohyb2_GetTypPohybu
    {

        #region ISkladPohyb2_GetFiltrovanySkladLokace Members

        public Fask.Interfaces.DataSets.SkladPohyb GetFiltrovanySkladLokace(Fask.Interfaces.Filtry.SkladPohybListFiltr filtr, string tableName)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladPohyb ds = new Fask.Interfaces.DataSets.SkladPohyb();

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();

                command.CommandText = "select pohyb.*, sklad.skl_desc as SkladOznaceni, uzivatel.USERID as UzivatelLogin, uzivatel.firstname as UzivatelFirstname,uzivatel.surname as UzivatelSecondname ";//, SLS.PRAC_ID_OWNER PracID, SLS.QTY_OWNER QTY_owner ";
                command.CommandText += " , DIH.[Zakazka_ID] as DIH_Zakazka_ID ,DIH.[Paleta_ID] as DIH_Paleta_ID ,DIH.[mena_ID] as DIH_mena_ID ,DIH.[SKL_ID] as DIH_SKL_ID ";
                command.CommandText += " , SIH.[TISKARNA_NAME] as SIH_TISKARNA_NAME,SIH.[PRAC_ID] as SIH_PRAC_ID,COALESCE(DIH.ISOK, SIH.ISOK) AS DIH_ISOK, COALESCE(DIH.status, SIH.status) AS DIH_status ";
                command.CommandText += " from " + tableName + " pohyb ";
                command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = pohyb.skl_id ";
                command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " uzivatel on uzivatel.USERID = pohyb.USER_ID ";

                command.CommandText += " OUTER APPLY ( SELECT TOP (1) d.* FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DIH + " d WHERE d.CountEntries = pohyb.CountEntries ORDER BY d.CountEntries ) DIH ";
                command.CommandText += " OUTER APPLY ( SELECT TOP (1) s.* FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SIH + " s WHERE s.CountEntries = pohyb.CountEntries ORDER BY s.CountEntries ) SIH ";
                //command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SIH + " SIH on SIH.CountEntries = pohyb.CountEntries ";

                //command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_CZMST_DIH + " DIH on DIH.CountEntries = pohyb.CountEntries ";
                //command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SIH + " SIH on SIH.CountEntries = pohyb.CountEntries ";
                //command.CommandText += "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " SLS on SLS.SERLTNUM = pohyb.SERLTNUM AND SLS.SKL_ID = pohyb.SKL_ID ";
                command.CommandText += "WHERE ";
                command.CommandText += "1=1 ";      // aby se nezobrazovalo nulove mnozstvi

                // hledaní ITEMNMBR
                if (filtr.MaterialID != null && !string.IsNullOrEmpty(filtr.MaterialID.Trim()))
                {
                    //command.CommandText += "AND (pohyb.ITEMDESC like '%' +  @nazevmat + '%' or pohyb.ITEMNMBR like '%' +  @nazevmat + '%') ";
                    command.CommandText += "AND pohyb.ITEMNMBR=@nazevmat ";
                    command.Parameters.AddWithValue("@nazevmat", filtr.MaterialID.Trim());
                }

                // hledaní itemcode
                if (filtr.MaterialITEMCODE != null && !string.IsNullOrEmpty(filtr.MaterialITEMCODE.Trim()))
                {
                    //command.CommandText += "AND (pohyb.ITEMDESC like '%' +  @nazevmat + '%' or pohyb.ITEMNMBR like '%' +  @nazevmat + '%') ";
                    command.CommandText += "AND pohyb.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialITEMCODE.Trim());
                }

                // hledani podle uzivatele
                if (!string.IsNullOrEmpty(filtr.rowUzivatel.Trim()) || !string.IsNullOrEmpty(filtr.UzivatelID.Trim()))
                {
                    // po konzultaci s JaS vyhledavat pouze podle loginu ...
                    command.CommandText += "AND pohyb.USER_ID IN ( " +
                    "select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    "   where USERID=@user " +
                    ") ";

                    command.Parameters.AddWithValue("@user", !string.IsNullOrEmpty(filtr.rowUzivatel) ? filtr.rowUzivatel : filtr.UzivatelID);
                }


                // hledaní druhu pohybu
                if (!filtr.Win_data)
                {
                    string inventura = "I";
                    string prijem = "PP";
                    //command.CommandText += "AND (pohyb.ITEMDESC like '%' +  @nazevmat + '%' or pohyb.ITEMNMBR like '%' +  @nazevmat + '%') ";
                    command.CommandText += "AND (pohyb.Type != @inventura AND pohyb.Type != @prijem) ";
                    command.Parameters.AddWithValue("@inventura", inventura);
                    command.Parameters.AddWithValue("@prijem", prijem);
                }

                //if (filtr.UzivatelID != null && !string.IsNullOrEmpty(filtr.UzivatelID.Trim()))
                //{
                //    if (filtr.rowUzivatel != null)
                //    {
                //        command.CommandText += "AND pohyb.USER_ID=@user ";
                //        //da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel.ID);
                //    }
                //    else
                //    {
                //        // po konzultaci s JaS vyhledavat pouze podle loginu ...
                //        command.CommandText += "AND pohyb.USER_ID IN ( " +
                //        //"select distinct ID from CZMSTPWD " +
                //        //"   where FIRSTNAME like '%' +  @user + '%' " +
                //        //"UNION " + 
                //        //"select distinct ID from CZMSTPWD " +
                //        //"   where SECONDNAME like '%' +  @user + '%' " +
                //        //"UNION " + 
                //        "select distinct ID from CZMSTPWD " +
                //        "   where LOGIN=@user " +
                //        ") ";
                //        //da_filter.SelectCommand.Parameters.AddWithValue("@name", comboBoxUzivatel.Text);
                //    }

                //    command.Parameters.AddWithValue("@user", filtr.rowUzivatel != null ? filtr.rowUzivatel.ID.ToString() : filtr.UzivatelID);
                //}

                //// hledani podle typu pohybu
                //if ((filtr.PohybType != null && !string.IsNullOrEmpty(filtr.PohybType.Trim())) ||
                //    (filtr.rowType != null && !string.IsNullOrEmpty(filtr.rowType.Trim())))
                //{
                //    //command.CommandText += "AND pohyb.TYPE like '%' + @pohybtype + '%' ";
                //    command.CommandText += "AND pohyb.TYPE=@pohybtype ";

                //    command.Parameters.AddWithValue("@pohybtype", !string.IsNullOrEmpty(filtr.rowType.Trim()) ? filtr.rowType.Trim() : filtr.PohybType.Trim());
                //}

                //19.11.2025 MaR novy vicepolozkovy filtr
                if (!string.IsNullOrEmpty(filtr.PohybType))
                {
                    command.CommandText += " AND pohyb.DOC_ID in (" + filtr.PohybType + ") ";
                }

                if (!string.IsNullOrEmpty(filtr.PohybType))
                {
                    var types1 = filtr.PohybType.Split(',');
                    var paramNames1 = new List<string>();

                    for (int i = 0; i < types1.Length; i++)
                    {
                        var trimmed1 = types1[i].Trim();
                        var paramName1 = "@pohybtype" + i;
                        paramNames1.Add(paramName1);
                        command.Parameters.AddWithValue(paramName1, trimmed1);
                    }

                    command.CommandText += " AND pohyb.DOC_ID IN (" + string.Join(",", paramNames1) + ") ";
                }


                if (!string.IsNullOrEmpty(filtr.TypPolozky))
                {
                    var types = filtr.TypPolozky.Split(',');
                    var paramNames = new List<string>();

                    for (int i = 0; i < types.Length; i++)
                    {
                        var trimmed = types[i].Trim();
                        var paramName = "@itemtype" + i;
                        paramNames.Add(paramName);
                        command.Parameters.AddWithValue(paramName, trimmed);
                    }

                    command.CommandText += " AND pohyb.ITEMTYPE IN (" + string.Join(",", paramNames) + ") ";
                }

                // hledaní podle cisla dokumentu
                if (filtr.DocumentNumber != null && !string.IsNullOrEmpty(filtr.DocumentNumber.Trim()))
                {
                    command.CommandText += "AND pohyb.DOCUMENT_NUMBER=@dokument ";

                    command.Parameters.AddWithValue("@dokument", filtr.DocumentNumber.Trim());
                }

                // hledaní podle lokace
                if (filtr.MaterialLocncode != null && !string.IsNullOrEmpty(filtr.MaterialLocncode.Trim()))
                {
                    command.CommandText += "AND pohyb.LOCNCODE=@lokace ";

                    command.Parameters.AddWithValue("@lokace", filtr.MaterialLocncode.Trim());
                }

                // hledaní podle skladu
                if (!string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()) || !string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()))
                {
                    command.CommandText += " AND pohyb.SKL_ID=@sklad ";
                    command.Parameters.AddWithValue("@sklad", !string.IsNullOrEmpty(filtr.cbSelectedMaterialSKLID.Trim()) ? filtr.cbSelectedMaterialSKLID : filtr.MaterialSKLID);
                }
                //if (filtr.MaterialSKLID != null && !string.IsNullOrEmpty(filtr.MaterialSKLID.Trim()))
                //{
                //    command.CommandText += " AND pohyb.SKL_ID=@sklad ";
                //    //if (filtr.rowMaterialSKLID != null)
                //    //{
                //    //    command.CommandText += " AND pohyb.SKL_ID=@sklad ";
                //    //}
                //    //else
                //    //{
                //    //    command.CommandText += "AND (sklad.skl_desc like '%' + @sklad + '%' or pohyb.SKL_ID=@sklad) ";
                //    //}
                //    command.Parameters.AddWithValue("@sklad", filtr.rowMaterialSKLID != null ? filtr.rowMaterialSKLID.skl_id.Trim() : filtr.MaterialSKLID.Trim());
                //}

                // hledání podle šarže
                // sarze umoznit i prazdnou
                if (filtr.MaterialSERLTNUM != null && !string.IsNullOrEmpty(filtr.MaterialSERLTNUM.Trim()))
                //if (filtr.MaterialSERLTNUM != null) //&& !string.IsNullOrEmpty(filtr.MaterialSERLTNUM.Trim()))
                {
                    //command.CommandText += "AND pohyb.SERLTNUM like '%' + @sarze + '%' ";
                    command.CommandText += "AND pohyb.SERLTNUM=@sarze ";
                    command.Parameters.AddWithValue("@sarze", filtr.MaterialSERLTNUM.Trim());
                }

                if (filtr.MaterialPracID != null && !string.IsNullOrEmpty(filtr.MaterialPracID.Trim()))
                {
                    //command.CommandText += "AND pohyb.SERLTNUM like '%' + @sarze + '%' ";
                    command.CommandText += "AND SLS.PRAC_ID_OWNER=@MaterialPracID ";
                    command.Parameters.AddWithValue("@MaterialPracID", filtr.MaterialPracID.Trim());
                }


                // hledání podle zadaného množství
                // pokud neni vyplnene mnozstvi nebo jsou zasktrnuty vsechny porovnani, tak se nevyhledava podle mnozstvi
                //if ((StavSkladuFiltr.Mnozstvi != null) || (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviRovno && StavSkladuFiltr.MnozstviVetsi))
                //{
                //    if (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviRovno)
                //    {
                //        command.CommandText += " AND stavmat.QTYSHPPD<=@mnozstvi ";
                //    }
                //    else if (StavSkladuFiltr.MnozstviVetsi && StavSkladuFiltr.MnozstviRovno)
                //    {
                //        command.CommandText += " AND stavmat.QTYSHPPD>=@mnozstvi ";
                //    }
                //    else if (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviVetsi)
                //    {
                //        command.CommandText += " AND stavmat.QTYSHPPD<>@mnozstvi ";
                //    }
                //    else if (StavSkladuFiltr.MnozstviMensi)
                //    {
                //        command.CommandText += " AND stavmat.QTYSHPPD<@mnozstvi ";
                //    }
                //    else if (StavSkladuFiltr.MnozstviRovno)
                //    {
                //        command.CommandText += " AND stavmat.QTYSHPPD=@mnozstvi ";
                //    }
                //    else if (StavSkladuFiltr.MnozstviVetsi)
                //    {
                //        command.CommandText += " AND stavmat.QTYSHPPD>@mnozstvi ";
                //    }
                //    command.Parameters.AddWithValue("@mnozstvi", StavSkladuFiltr.Mnozstvi.Value);
                //}

                // hledání podle datumu
                if (filtr.DatumOd != null && filtr.DatumDo != null)
                {
                    command.CommandText += " AND dateeve between @datumOd and @datumDo ";
                    command.Parameters.AddWithValue("@datumOd", filtr.DatumOd);
                    command.Parameters.AddWithValue("@datumDo", filtr.DatumDo);
                }
                else
                {
                    if (filtr.DatumOd != null)
                    {
                        command.CommandText += " AND dateeve > @datumOd ";
                        command.Parameters.AddWithValue("@datumOd", filtr.DatumOd);
                    }
                    else if (filtr.DatumDo != null)
                    {
                        command.CommandText += " AND dateeve < @datumDo ";
                        command.Parameters.AddWithValue("@datumDo", filtr.DatumDo);
                    }
                }

                command.CommandText += "order by dateeve desc";

                command.Connection = connection;
                adapter.SelectCommand = command;

                ds.CZMST_Sklad_Pohyb.BeginLoadData();
                //naplnim data ...
                adapter.Fill(ds.CZMST_Sklad_Pohyb);

                // TODO: doplneni indexu nejak jinak??
                int index = ds.CZMST_Sklad_Pohyb.Count + 1;
                foreach (Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow item in ds.CZMST_Sklad_Pohyb)
                {
                    item.Index = --index;
                }

                ds.CZMST_Sklad_Pohyb.EndLoadData();
                ds.CZMST_Sklad_Pohyb.AcceptChanges();

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladPohyb2_GetTypPohybu Members

        public Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable GetTypPohybu(string tableName)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable ds = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();
            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                adapter = new System.Data.SqlClient.SqlDataAdapter();

                command.CommandText =
                    "select distinct type " +
                    "from " + tableName;

                ds.Clear();
                ds.AcceptChanges();

                command.Connection = connection;
                adapter.SelectCommand = command;

                ds.BeginLoadData();
                //naplnim data ...
                adapter.Fill(ds);

                ds.EndLoadData();
            }
            catch
            {
                throw;
            }

            return ds;
        }

        #endregion
    }
}
