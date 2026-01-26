using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace Fask.ModuleFirebird
{
    public partial class Provider : Fask.Interfaces.StavSkladu.IStavSkladu2,
        Fask.Interfaces.StavSkladu.IStavSkladu2_GetFiltrovanyStavSkladu,
        Fask.Interfaces.StavSkladu.IStavSkladu2_GetFiltrovanyStavSkladuHistorie,
        Fask.Interfaces.StavSkladu.IStavSkladu2_GetSklady,
        Fask.Interfaces.StavSkladu.IStavSkladu2_GetStavSkladu,
        Fask.Interfaces.StavSkladu.IStavSkladu2_getUzivatele
    {

        public string ConnectionString { get; set; }

        //public Fask.Interfaces.DataSets.StavSkladu GetStavSkladu(Fask.Interfaces.Filtry.StavSkladuListFiltr StavSkladuFiltr, ref Fask.Interfaces.DataSets.StavSkladu StavSkladu)
        public void GetFiltrovanyStavSkladu(Fask.Interfaces.Filtry.StavSkladuListFiltr StavSkladuFiltr, ref Fask.Interfaces.DataSets.StavSkladu StavSkladu)
        {
            FirebirdSql.Data.FirebirdClient.FbConnection connection = null;
            FirebirdSql.Data.FirebirdClient.FbCommand command = null;
            FbDataAdapter adapter = null;

            try
            {
                connection = new FirebirdSql.Data.FirebirdClient.FbConnection(ConnectionString);
                command = new FbCommand();
                adapter = new FbDataAdapter();

                command.CommandText = "select stavmat.*, sklad.skl_desc as skl_desc from get_os_ms as stavmat ";
                command.CommandText += "left join CZMST093 sklad on sklad.skl_id = stavmat.skl_id ";
                command.CommandText += "WHERE ";
                command.CommandText += "stavmat.qtyshppd>0 ";      // aby se nezobrazovalo nulove mnozstvi

                // hledaní ITEMNMBR
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialID.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialID != null)
                    {
                        command.CommandText += " AND stavmat.ITEMNMBR=@nazevmat ";
                    }
                    else
                    {
                        command.CommandText += "AND (stavmat.ITEMDESC like @nazevmat or stavmat.ITEMNMBR like @nazevmat or stavmat.ITEMCODE like @nazevmat)";
                    }
                    command.Parameters.AddWithValue("@nazevmat", StavSkladuFiltr.rowMaterialID != null ? StavSkladuFiltr.rowMaterialID.ITEMNMBR.Trim() : ("%" + StavSkladuFiltr.MaterialID.Trim() + "%"));
                }

                // hledaní podle lokace
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialLocncode.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialLOCNCODE != null)
                    {
                        command.CommandText += " AND stavmat.LOCNCODE=@lokace ";
                    }
                    else
                    {
                        command.CommandText += "AND stavmat.LOCNCODE like @lokace ";
                    }
                    command.Parameters.AddWithValue("@lokace", StavSkladuFiltr.rowMaterialLOCNCODE != null ? StavSkladuFiltr.rowMaterialLOCNCODE.LOCNCODE.Trim() : ("%" + StavSkladuFiltr.MaterialLocncode.Trim() + "%"));
                }

                // hledaní podle skladu
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialSKLID.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialSKLID != null)
                    {
                        command.CommandText += " AND stavmat.SKL_ID=@sklad ";
                    }
                    else
                    {
                        command.CommandText += "AND sklad.skl_desc like @sklad ";
                    }
                    command.Parameters.AddWithValue("@sklad", StavSkladuFiltr.rowMaterialSKLID != null ? StavSkladuFiltr.rowMaterialSKLID.skl_id.Trim() : ("%" + StavSkladuFiltr.MaterialSKLID.Trim() + "%"));
                }

                // hledání podle šarže
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialSERLTNUM.Trim()))
                {
                    command.CommandText += "AND stavmat.SERLTNUM like @sarze ";
                    command.Parameters.AddWithValue("@sarze", "%" + StavSkladuFiltr.MaterialSERLTNUM.Trim() + "%");
                }

                // hledání podle zadaného množství
                // pokud neni vyplnene mnozstvi nebo jsou zasktrnuty vsechny porovnani, tak se nevyhledava podle mnozstvi
                if ((StavSkladuFiltr.Mnozstvi != null) || (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviRovno && StavSkladuFiltr.MnozstviVetsi))
                {
                    if (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviRovno)
                    {
                        command.CommandText += " AND stavmat.QTYSHPPD<=@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviVetsi && StavSkladuFiltr.MnozstviRovno)
                    {
                        command.CommandText += " AND stavmat.QTYSHPPD>=@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviVetsi)
                    {
                        command.CommandText += " AND stavmat.QTYSHPPD<>@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviMensi)
                    {
                        command.CommandText += " AND stavmat.QTYSHPPD<@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviRovno)
                    {
                        command.CommandText += " AND stavmat.QTYSHPPD=@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviVetsi)
                    {
                        command.CommandText += " AND stavmat.QTYSHPPD>@mnozstvi ";
                    }
                    command.Parameters.AddWithValue("@mnozstvi", StavSkladuFiltr.Mnozstvi.Value);
                }

                // hledání podle datumu
                if (StavSkladuFiltr.ExpiraceOd != null && StavSkladuFiltr.ExpiraceDo != null)
                {
                    command.CommandText += " AND EXPIRATION between @datumOd and @datumDo ";
                    command.Parameters.AddWithValue("@datumOd", StavSkladuFiltr.ExpiraceOd); //.Value);
                    command.Parameters.AddWithValue("@datumDo", StavSkladuFiltr.ExpiraceDo); // dateTimePickerDatumDo.Value);
                }
                else
                {
                    if (StavSkladuFiltr.ExpiraceOd != null)
                    {
                        command.CommandText += " AND EXPIRATION > @datumOd ";
                        command.Parameters.AddWithValue("@datumOd", StavSkladuFiltr.ExpiraceOd);//dateTimePickerDatumOd.Value);
                        //command.Parameters.AddWithValue("@datumOd", dateTimePickerDatumOd.Value.ToString("dd.MM.yyyy, HH.mm.ss.fff") );
                    }
                    else if (StavSkladuFiltr.ExpiraceDo != null)
                    {
                        command.CommandText += " AND EXPIRATION < @datumDo ";
                        command.Parameters.AddWithValue("@datumDo", StavSkladuFiltr.ExpiraceDo);//dateTimePickerDatumDo.Value);
                        //command.Parameters.AddWithValue("@datumDo", dateTimePickerDatumDo.Value.ToString("dd.MM.yyyy, HH.mm.ss.fff"));
                    }
                }

                StavSkladu.get_os_ms.Clear();
                StavSkladu.get_os_ms.AcceptChanges();

                command.Connection = connection;
                adapter.SelectCommand = command;

                StavSkladu.get_os_ms.BeginLoadData();
                //naplnim data ...
                adapter.Fill(StavSkladu.get_os_ms);

                StavSkladu.get_os_ms.EndLoadData();
            }
            catch 
            {                
                throw;
            }
        }

        public void GetFiltrovanyStavSkladuHistorie(Fask.Interfaces.Filtry.StavSkladuHistorieFiltr StavSkladuFiltr, ref Fask.Interfaces.DataSets.StavSkladu StavSkladu)
        {
            FirebirdSql.Data.FirebirdClient.FbConnection connection = null;
            FirebirdSql.Data.FirebirdClient.FbCommand command = null;
            FbDataAdapter adapter = null;

            try
            {
                connection = new FirebirdSql.Data.FirebirdClient.FbConnection(ConnectionString);
                command = new FbCommand();
                adapter = new FbDataAdapter();

                command.CommandText = "select hist.*, users.SECONDNAME as username, stavmat.ITEMDESC as itemdesc, sklad.skl_desc as skl_src_desc, sklad2.skl_desc as skl_dst_desc from get_os_ms_hist as hist ";
                command.CommandText += "left join CZMSTPWD users on users.ID = hist.USERID ";
                command.CommandText += "left join CZMST093 sklad on sklad.skl_id = hist.SKL_ID_SRC ";
                command.CommandText += "left join CZMST093 sklad2 on sklad2.skl_id = hist.SKL_ID_DST ";
                command.CommandText += "left outer join ( select distinct ITEMNMBR, ITEMDESC from GET_OS_MS ) as stavmat on stavmat.ITEMNMBR = hist.ITEMNMBR ";     // vyhozeni duplicit
                command.CommandText += "WHERE ";
                command.CommandText += "1=1 ";        // osetreni, aby byly nasledne vsude AND

                // hledání uživatele
                if (!string.IsNullOrEmpty(StavSkladuFiltr.UzivatelID.Trim()))
                {
                    if (StavSkladuFiltr.rowUzivatel != null)
                    {
                        command.CommandText += "AND hist.UserID=@user ";
                        //da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel.ID);
                    }
                    else
                    {
                        command.CommandText += "AND hist.UserID IN ( " +
                        "select distinct ID from CZMSTPWD " +
                        "where SECONDNAME like @user" +
                        ") ";
                        //da_filter.SelectCommand.Parameters.AddWithValue("@name", comboBoxUzivatel.Text);
                    }
                    //command.Parameters.Add(new FbParameter("@user", rowUzivatel != null ? rowUzivatel.ID.ToString() : comboBoxUzivatel.Text)); //CommandText.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel != null ? rowUzivatel.ID.ToString() : comboBoxUzivatel.Text);
                    command.Parameters.AddWithValue("@user", StavSkladuFiltr.rowUzivatel != null ? StavSkladuFiltr.rowUzivatel.ID.ToString() : ("%" + StavSkladuFiltr.UzivatelID + "%"));
                }

                // hledaní ITEMNMBR
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialID.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialID != null)
                    {
                        command.CommandText += " AND hist.ITEMNMBR=@nazevmat ";
                    }
                    else
                    {
                        command.CommandText += "AND hist.ITEMNMBR IN (" +
                        " select distinct ITEMNMBR from get_os_ms" +
                            " where (ITEMDESC like @nazevmat or ITEMNMBR like @nazevmat or ITEMCODE like @nazevmat)" +
                            //" where (ITEMDESC like '%" + comboBoxMaterialID.Text + "%')" +
                        " ) ";
                    }
                    command.Parameters.AddWithValue("@nazevmat", StavSkladuFiltr.rowMaterialID != null ? StavSkladuFiltr.rowMaterialID.ITEMNMBR.Trim() : ("%" + StavSkladuFiltr.MaterialID + "%"));
                }

                // hledaní podle zdrojové lokace
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialLocncodeSRC.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialLOCNCODESRC != null)
                    {
                        command.CommandText += " AND hist.LOCNCODE_SRC=@lokacesrc ";
                    }
                    else
                    {
                        command.CommandText += "AND hist.LOCNCODE_SRC like @lokacesrc ";
                    }
                    command.Parameters.AddWithValue("@lokacesrc", StavSkladuFiltr.rowMaterialLOCNCODESRC != null ? StavSkladuFiltr.rowMaterialLOCNCODESRC.LOCNCODE.Trim() : ("%" + StavSkladuFiltr.MaterialLocncodeSRC + "%"));
                }

                // hledaní podle cílové lokace
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialLocncodeDST.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialLOCNCODEDST != null)
                    {
                        command.CommandText += " AND hist.LOCNCODE_DST=@lokacedst ";
                    }
                    else
                    {
                        command.CommandText += "AND hist.LOCNCODE_DST like @lokacedst ";
                    }
                    command.Parameters.AddWithValue("@lokacedst", StavSkladuFiltr.rowMaterialLOCNCODEDST != null ? StavSkladuFiltr.rowMaterialLOCNCODEDST.LOCNCODE.Trim() : ("%" + StavSkladuFiltr.MaterialLocncodeDST + "%"));
                }

                // hledání podle šarže
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialSERLTNUM.Trim()))
                {
                    command.CommandText += "AND hist.SERLTNUM like @sarze ";
                    command.Parameters.AddWithValue("@sarze", "%" + StavSkladuFiltr.MaterialSERLTNUM + "%");
                }

                // hledaní podle zdrojového skladu
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialSKLIDSRC.Trim()))
                {
                    if (StavSkladuFiltr.rowMaterialSKLIDSRC != null)
                    {
                        command.CommandText += "AND hist.SKL_ID_SRC=@skladsrc ";
                    }
                    else
                    {
                        command.CommandText += "AND sklad.skl_desc like @skladsrc ";
                    }
                    command.Parameters.AddWithValue("@skladsrc", StavSkladuFiltr.rowMaterialSKLIDSRC != null ? StavSkladuFiltr.rowMaterialSKLIDSRC.skl_id.Trim() : ("%" + StavSkladuFiltr.MaterialSKLIDSRC + "%"));
                }

                // hledaní podle zdrojového skladu
                if (!string.IsNullOrEmpty(StavSkladuFiltr.MaterialSKLIDDST.Trim()) || StavSkladuFiltr.MaterialSKLIDDSTPresnaShoda)
                {
                    // !! command.CommandText += "AND sklad2.skl_desc like @skladdst "; !!
                    if (StavSkladuFiltr.MaterialSKLIDDSTPresnaShoda)
                    {
                        command.CommandText += "AND hist.skl_id_dst=@skladdst ";
                        command.Parameters.AddWithValue("@skladdst", StavSkladuFiltr.MaterialSKLIDDST);
                    }
                    else if (StavSkladuFiltr.rowMaterialSKLIDDST != null)
                    {
                        command.CommandText += "AND hist.SKL_ID_DST=@skladdst ";
                        command.Parameters.AddWithValue("@skladdst", StavSkladuFiltr.rowMaterialSKLIDDST != null ? StavSkladuFiltr.rowMaterialSKLIDDST.skl_id.Trim() : ("%" + StavSkladuFiltr.MaterialSKLIDDST + "%"));
                    }
                    else
                    {
                        command.CommandText += "AND sklad2.skl_desc like @skladdst ";
                        command.Parameters.AddWithValue("@skladdst", StavSkladuFiltr.rowMaterialSKLIDDST != null ? StavSkladuFiltr.rowMaterialSKLIDDST.skl_id.Trim() : ("%" + StavSkladuFiltr.MaterialSKLIDDST + "%"));
                    }
                    //command.Parameters.AddWithValue("@skladdst", StavSkladuFiltr.rowMaterialSKLIDDST != null ? StavSkladuFiltr.rowMaterialSKLIDDST.skl_id.Trim() : ("%" + StavSkladuFiltr.MaterialSKLIDDST + "%"));

                    //if (StavSkladuFiltr.rowMaterialSKLIDDST != null)
                    //{
                    //    command.CommandText += "AND hist.SKL_ID_DST=@skladdst ";
                    //}
                    //else
                    //{
                    //    if(StavSkladuFiltr.MaterialSKLIDDSTPresnaShoda)
                    //        command.CommandText += "AND a like @skladdst ";
                    //    else
                    //        command.CommandText += "AND sklad2.skl_desc like @skladdst ";    
                    //}
                    //command.Parameters.AddWithValue("@skladdst", StavSkladuFiltr.rowMaterialSKLIDDST != null ? StavSkladuFiltr.rowMaterialSKLIDDST.skl_id.Trim() : ("%" + StavSkladuFiltr.MaterialSKLIDDST + "%"));
                }

                // hledání podle zadaného množství
                // pokud neni vyplnene mnozstvi nebo jsou zasktrnuty vsechny porovnani, tak se nevyhledava podle mnozstvi
                if ((StavSkladuFiltr.Mnozstvi != null) || (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviRovno && StavSkladuFiltr.MnozstviVetsi))
                {
                    if (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviRovno)
                    {
                        command.CommandText += " AND hist.QTYSHPPD<=@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviVetsi && StavSkladuFiltr.MnozstviRovno)
                    {
                        command.CommandText += " AND hist.QTYSHPPD>=@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviMensi && StavSkladuFiltr.MnozstviVetsi)
                    {
                        command.CommandText += " AND hist.QTYSHPPD<>@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviMensi)
                    {
                        command.CommandText += " AND hist.QTYSHPPD<@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviRovno)
                    {
                        command.CommandText += " AND hist.QTYSHPPD=@mnozstvi ";
                    }
                    else if (StavSkladuFiltr.MnozstviVetsi)
                    {
                        command.CommandText += " AND hist.QTYSHPPD>@mnozstvi ";
                    }
                    command.Parameters.AddWithValue("@mnozstvi", StavSkladuFiltr.Mnozstvi.Value);
                }

                // hledání podle datumu
                if (StavSkladuFiltr.DatumOd != null && StavSkladuFiltr.DatumDo != null)
                {
                    command.CommandText += " AND hist.dateeve between @datumOd and @datumDo ";
                    command.Parameters.AddWithValue("@datumOd", StavSkladuFiltr.DatumOd);
                    command.Parameters.AddWithValue("@datumDo", StavSkladuFiltr.DatumDo);
                }
                else
                {
                    if (StavSkladuFiltr.DatumOd != null)
                    {
                        command.CommandText += " AND hist.dateeve > @datumOd ";
                        command.Parameters.AddWithValue("@datumOd", StavSkladuFiltr.DatumOd);
                        //command.Parameters.AddWithValue("@datumOd", dateTimePickerDatumOd.Value.ToString("dd.MM.yyyy, HH.mm.ss.fff") );
                    }
                    else if (StavSkladuFiltr.DatumDo != null)
                    {
                        command.CommandText += " AND hist.dateeve < @datumDo ";
                        command.Parameters.AddWithValue("@datumDo", StavSkladuFiltr.DatumDo);
                        //command.Parameters.AddWithValue("@datumDo", dateTimePickerDatumDo.Value.ToString("dd.MM.yyyy, HH.mm.ss.fff"));
                    }
                }

                //command.CommandText += "and stavmat.ITEMDESC like '%Scanner%' ";
                command.CommandText += "order by hist.dateeve desc, hist.ID desc";

                StavSkladu.get_os_ms_hist.Clear();
                StavSkladu.get_os_ms_hist.AcceptChanges();

                command.Connection = connection;
                adapter.SelectCommand = command;

                StavSkladu.get_os_ms_hist.BeginLoadData();
                //naplnim data ...
                adapter.Fill(StavSkladu.get_os_ms_hist);

                StavSkladu.get_os_ms_hist.EndLoadData();
            }
            catch 
            {                
                throw;
            }
        }


        public void GetStavSkladu(ref Fask.Interfaces.DataSets.StavSkladu dsStavSkladu)
        {
            FirebirdSql.Data.FirebirdClient.FbConnection connection = null;
            FirebirdSql.Data.FirebirdClient.FbCommand command = null;
            FbDataAdapter adapter = null;

            try
            {
                connection = new FirebirdSql.Data.FirebirdClient.FbConnection(ConnectionString);
                command = new FbCommand();
                adapter = new FbDataAdapter();

                command.CommandText = "select * from get_os_ms";
                command.Connection = connection;
                adapter.SelectCommand = command;

                dsStavSkladu.get_os_ms.BeginLoadData();
                //naplnim data ...
                adapter.Fill(dsStavSkladu.get_os_ms);

                dsStavSkladu.get_os_ms.EndLoadData();
            }
            catch 
            {                
                throw;
            }
        }


        public void getUzivatele(ref Fask.Interfaces.DataSets.StavSkladu dsUzivatele)
        {
            FirebirdSql.Data.FirebirdClient.FbConnection connection = null;
            FirebirdSql.Data.FirebirdClient.FbCommand command = null;
            FbDataAdapter adapter = null;

            try
            {
                connection = new FirebirdSql.Data.FirebirdClient.FbConnection(ConnectionString);
                command = new FbCommand();
                adapter = new FbDataAdapter();

                command.CommandText = "select * from czmstpwd";
                command.Connection = connection;
                adapter.SelectCommand = command;

                dsUzivatele.CZMSTPWD.BeginLoadData();
                //naplnim data ...
                adapter.Fill(dsUzivatele.CZMSTPWD);

                dsUzivatele.CZMSTPWD.EndLoadData();                
            }
            catch
            {
                throw;
            }
        }

        public void GetSklady(ref Fask.Interfaces.DataSets.StavSkladu StavSkladu)
        {
            FirebirdSql.Data.FirebirdClient.FbConnection connection = null;
            FirebirdSql.Data.FirebirdClient.FbCommand command = null;
            FbDataAdapter adapter = null;

            try
            {
                connection = new FirebirdSql.Data.FirebirdClient.FbConnection(ConnectionString);
                command = new FbCommand();
                adapter = new FbDataAdapter();

                command.CommandText = "select * from czmst093";
                command.Connection = connection;
                adapter.SelectCommand = command;

                StavSkladu.CZMST093.BeginLoadData();
                //naplnim data ...
                adapter.Fill(StavSkladu.CZMST093);

                StavSkladu.CZMST093.EndLoadData();
            }
            catch
            {
                throw;
            }
        }
    }
}
