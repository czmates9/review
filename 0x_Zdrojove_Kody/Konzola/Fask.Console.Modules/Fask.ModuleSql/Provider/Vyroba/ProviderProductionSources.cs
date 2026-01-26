using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyProductionSourcesList,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update_Row,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update,

        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyVazby
    {
        public Guid? GUID_Production { get; set; }

        #region IProductionSources_GetFiltrovanyProductionSourcesList Members

        #region old
        //public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProductionSourcesList(Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr)
        //{
        //    System.Data.SqlClient.SqlConnection connection = null;
        //    System.Data.SqlClient.SqlCommand command = null;
        //    System.Data.SqlClient.SqlDataAdapter adapter = null;
        //    Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


        //    adapter = new System.Data.SqlClient.SqlDataAdapter();
        //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //    command = new System.Data.SqlClient.SqlCommand();
        //    command.Connection = connection;


        //    #region command
        //    command.CommandText = "Select l.firstname, l.surname" +
        //        ", sklady.skl_desc skladName" + ", sklady.skl_carcode skladKod" +
        //        ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
        //        ", hlavicky.SOPDESC popiszakazky" +
        //        ", p.ITEMDESC nazevvyrobku, p.BarcodeP EANvyrobku, p.ITEMNMBR cislovyrobku" +
        //        ", fc.vnditnum as EANMaterialu";
        //    // TODO : ean vyrobku ??


        //    command.CommandText += ", ps.* from Production_Sources ps" +
        //    " left join Production p on p.GUID=ps.GUID_Production" +
        //    " left join CZMST093 sklady on sklady.skl_id = ps.SKL_ID" +
        //    " left join CZMST094 lokace on lokace.skl_id = ps.SKL_ID and lokace.locncode=ps.LOCNCODE" +
        //    " left join CZPRO_VPH hlavicky on hlavicky.SOPNUMBE = ps.SOPNUMBE" +
        //    " left join (select itemnmbr, Min(vnditnum) vnditnum from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " group by itemnmbr) fc on fc.itemnmbr=ps.itemnmbr";

        //    command.CommandText += " left join FASK_Logins l on l.USERID = ps.USER_ID" +
        //        " Where ";

        //    command.CommandText += "1=1 ";
        //    // číslo davky
        //    if (!string.IsNullOrEmpty(filtr.CountEntries))
        //    {
        //        command.CommandText += " AND ps.CountEntries = @CountEntries";
        //        command.Parameters.AddWithValue("@CountEntries", filtr.CountEntries.Trim());
        //    }

        //    //vyrobny prikaz
        //    if (filtr.rowVPH != null)
        //    {
        //        command.CommandText += " AND ps.SOPNUMBE = @SOPNUMBE";
        //        command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH.SOPNUMBE);

        //    }
        //    else if (filtr.SOPNUMBE.Trim().Length > 0)
        //    {
        //        command.CommandText += " AND isnull(ps.SOPNUMBE, '') IN(" +
        //            "Select SOPNUMBE from CZPRO_VPH where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
        //            " union " +
        //            "Select SOPNUMBE from CZPRO_VPH where (SOPDESC like '%' + @SOPNUMBE + '%')" +
        //            ")";
        //        command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH != null ? filtr.rowVPH.SOPNUMBE.Trim() : filtr.SOPNUMBE.Trim());

        //    }
        //    if (filtr.EAN.Trim().Length > 0)
        //    {
        //        command.CommandText += " AND (" +
        //            " polozky.BarcodeP like '%' + @EAN + '%'" +
        //            " or " +
        //            " polozky.VNDITNUM like '%' + @EAN + '%' " +
        //            ")";
        //        command.Parameters.AddWithValue("@EAN", filtr.EAN.Trim());

        //    }


        //    //sklad
        //    if (filtr.rowSKLAD != null)
        //    {
        //        command.CommandText += " AND ps.SKL_ID = @SKL_ID";
        //    }
        //    else if (filtr.SKLAD.Trim().Length > 0)
        //    {
        //        command.CommandText += " AND isnull(ps.SKL_ID, '') like '%' + @SKL_ID + '%'";
        //    }
        //    command.Parameters.AddWithValue("@SKL_ID", filtr.rowSKLAD != null ? filtr.rowSKLAD.skl_id.Trim() : filtr.SKLAD.Trim());

        //    //lokace
        //    if (filtr.rowLokace != null)
        //    {
        //        command.CommandText += " AND ps.LOCNCODE = @LOCNCODE";
        //        command.Parameters.AddWithValue("@LOCNCODE", filtr.rowLokace.LOCNCODE.Trim());
        //    }
        //    else if (filtr.LOCNCODE.Trim().Length > 0)
        //    {
        //        command.CommandText +=
        //            " AND isnull(ps.LOCNCODE, '') IN (" +
        //            "Select LOCNCODE from czmst094 where (Barcode like '%' + @BarcodeL + '%')" +
        //            " union " +
        //            "Select LOCNCODE from czmst094 where (Description like '%' + @BarcodeL + '%')" +
        //            ")"; //"like '%' + @LOCNCODE + '%'";
        //        command.Parameters.AddWithValue("@BarcodeL", filtr.LOCNCODE.Trim());
        //    }
        //    //command.Parameters.AddWithValue("@LOCNCODE", rowLokace != null ? rowLokace.LOCNCODE.Trim() : ComboBoxLOCNCODE.Text);

        //    if (filtr.VyrobaPouzivatTabulkuZbozi)
        //    {
        //        // hledaní material
        //        if (filtr.rowZASOBY != null)
        //        {
        //            command.CommandText += " AND ps.ITEMNMBR=@itemdesc";
        //            command.Parameters.AddWithValue("@itemdesc", filtr.rowZASOBY.ITEMNMBR.Trim());
        //        }
        //        else if (filtr.ZBOZI.Trim().Length > 0)
        //        {
        //            command.CommandText += " AND ps.ITEMNMBR IN (" +
        //            " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
        //            " where ITEMDESC like '%' + @itemdesc + '%'" +
        //            " union" +
        //            " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
        //            " where ITEMNMBR like '' + @itemdesc + '%' " +
        //            " )";

        //            command.Parameters.AddWithValue("@itemdesc", filtr.rowZASOBY != null ? filtr.rowZASOBY.ITEMNMBR.Trim() : filtr.ZBOZI.Trim());
        //        }


        //        // hledaní vyrobek
        //        if (filtr.rowVyrobek != null)
        //        {
        //            command.CommandText += " AND p.ITEMNMBR=@itemnmbr";
        //            command.Parameters.AddWithValue("@itemnmbr", filtr.rowVyrobek.ITEMNMBR.Trim());
        //        }
        //        else if (filtr.VYROBEK.Trim().Length > 0)
        //        {
        //            command.CommandText += " AND p.ITEMNMBR IN (" +
        //            " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
        //            " where ITEMDESC like '%' + @itemnmbr + '%'" +
        //            " union" +
        //            " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
        //            " where ITEMNMBR like '' + @itemnmbr + '%' " +
        //            " )";

        //            command.Parameters.AddWithValue("@itemnmbr", filtr.rowZASOBY != null ? filtr.rowZASOBY.ITEMNMBR.Trim() : filtr.VYROBEK.Trim());
        //        }
        //    }
        //    // Typ polozky
        //    if (!string.IsNullOrEmpty(filtr.ITEMTYPE))
        //    {
        //        command.CommandText += " AND ps.ITEMTYPE = @ITEMTYPE";
        //        command.Parameters.AddWithValue("@ITEMTYPE", filtr.ITEMTYPE.Trim());
        //    }

        //    // car. kod polozky
        //    if (!string.IsNullOrEmpty(filtr.ITEMCODE))
        //    {
        //        command.CommandText += " AND ps.ITEMCODE = @ITEMCODE";
        //        command.Parameters.AddWithValue("@ITEMCODE", filtr.ITEMCODE.Trim());
        //    }

        //    // merna jednotka
        //    if (!string.IsNullOrEmpty(filtr.MJ))
        //    {
        //        command.CommandText += " AND ps.MJ = @MJ";
        //        command.Parameters.AddWithValue("@MJ", filtr.MJ.Trim());
        //    }
        //    // seriove cislo
        //    if (!string.IsNullOrEmpty(filtr.SERLTNUM))
        //    {
        //        command.CommandText += " AND ps.SERLTNUM = @SERLTNUM";
        //        command.Parameters.AddWithValue("@SERLTNUM", filtr.SERLTNUM.Trim());
        //    }

        //    if (filtr.DatumOd && filtr.DatumDo)
        //    {
        //        command.CommandText += " AND ps.ISOK between @datumOd and @datumDo";
        //        command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
        //        command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
        //    }
        //    else
        //    {
        //        if (filtr.DatumOd)
        //        {
        //            command.CommandText += " AND ps.ISOK > @datumOd";
        //            command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
        //        }
        //        else if (filtr.DatumDo)
        //        {
        //            command.CommandText += " AND ps.ISOK < @datumDo";
        //            command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
        //        }
        //    }


        //    // hledání uživatele
        //    if (!string.IsNullOrEmpty(filtr.Uzivatel))
        //    {
        //        if (filtr.Uzivatel != null)
        //        {
        //            command.CommandText += " AND ps.USER_ID=@name";
        //        }
        //        else
        //        {
        //            command.CommandText += " AND ps.USER_ID IN (" +
        //            " select distinct id from Logins" +
        //            " where firstname like '%' + @name + '%'" +
        //            " union" +
        //            " select distinct id from Logins" +
        //            " where surname like '%' + @name + '%'" +
        //            " union" +
        //            " select distinct id from Logins" +
        //            " where id = @name" +
        //            " )";
        //        }
        //        command.Parameters.AddWithValue("@name", filtr.rowUzivatel != null ? filtr.rowUzivatel.USERID.Trim() : filtr.Uzivatel);
        //    }

        //    // ID terminal
        //    if (!string.IsNullOrEmpty(filtr.TERMINAL_ID))
        //    {
        //        command.CommandText += " AND ps.TERMINAL_ID = @TERMINAL_ID";
        //        command.Parameters.AddWithValue("@TERMINAL_ID", filtr.TERMINAL_ID.Trim());
        //    }
        //    // cislo palety
        //    if (!string.IsNullOrEmpty(filtr.NMBRPAL))
        //    {
        //        command.CommandText += " AND ps.NMBRPAL = @NMBRPAL";
        //        command.Parameters.AddWithValue("@NMBRPAL", filtr.NMBRPAL.Trim());
        //    }
        //    // typ palety
        //    if (!string.IsNullOrEmpty(filtr.TYPEPAL))
        //    {
        //        command.CommandText += " AND ps.TYPEPAL = @TYPEPAL";
        //        command.Parameters.AddWithValue("@TYPEPAL", filtr.TYPEPAL.Trim());
        //    }
        //    // vytisknuto
        //    if (!string.IsNullOrEmpty(filtr.PRINTED))
        //    {
        //        command.CommandText += " AND ps.PRINTED = @PRINTED";
        //        command.Parameters.AddWithValue("@PRINTED", filtr.PRINTED.Trim());
        //    }

        //    command.CommandText += " order by ps.ISOK desc";

        //    #endregion


        //    vyrobaDataSet1.Production_Konzola.Clear();
        //    vyrobaDataSet1.Production_Konzola.AcceptChanges();
        //    vyrobaDataSet1.Production_Konzola.BeginLoadData();
        //    adapter.SelectCommand = command;
        //    adapter.Fill(vyrobaDataSet1.Production_Sources);
        //    vyrobaDataSet1.Production_Konzola.EndLoadData();

        //    return vyrobaDataSet1;
        //}

        #endregion

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProductionSourcesList(Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr)
        {
            //Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            #region command
            command.CommandText = "Select l.firstname, l.surname" +
                ", sklady.skl_desc skladName" + ", sklady.skl_carcode skladKod" +
                ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
                ", hlavicky.SOPDESC popiszakazky" +
                //", polozky.ITEMDESC nazevvyrobku, polozky.BarcodeP EANvyrobku, polozky.VNDITNUM cislovyrobku";
                ", p.ITEMDESC nazevvyrobku, p.BarcodeP EANvyrobku, p.ITEMNMBR cislovyrobku" +
                ", fc.vnditnum as EANMaterialu";
            // TODO : ean vyrobku ??

            //TODO : EAN spotrebovaneho materialu, potreba rozsitit 
            //17.4.2018 Kod od JiS dotazeni caroveho kodu
            //select distinct x.itemnmbr, x.itemdesc, y.vnditnum, y.cz_carkod
            //from FASK_ZASOBY x
            //left join FASK_ZASOBY y on y.dex_row_id = (select top 1 dex_row_id from FASK_ZASOBY where itemnmbr=x.itemnmbr)

            command.CommandText += ", ps.* from " + Fask.SQL.Constants.Common.TABLE_Production_Sources + " ps" +
            " left join " + Fask.SQL.Constants.Common.TABLE_Production + " p on p.GUID=ps.GUID_Production" +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady on sklady.skl_id = ps.SKL_ID" +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " lokace on lokace.skl_id = ps.SKL_ID and lokace.locncode=ps.LOCNCODE" +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " hlavicky on hlavicky.SOPNUMBE = ps.SOPNUMBE" +
            " left join (select itemnmbr, Min(vnditnum) vnditnum from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " group by itemnmbr) fc on fc.itemnmbr=ps.itemnmbr";

            command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = ps.USER_ID" +
                " Where ";

            command.CommandText += "1=1 ";
            // číslo davky
            if (!string.IsNullOrEmpty(filtr.CountEntries))
            {
                command.CommandText += " AND ps.CountEntries = @CountEntries";
                command.Parameters.AddWithValue("@CountEntries", filtr.CountEntries.Trim());
            }

            //vyrobny prikaz
            if (filtr.rowVPH != null)
            {
                command.CommandText += " AND ps.SOPNUMBE = @SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH.SOPNUMBE);

            }
            else if (filtr.SOPNUMBE.Trim().Length > 0)
            {
                command.CommandText += " AND isnull(ps.SOPNUMBE, '') IN(" +
                    "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
                    " union " +
                    "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPDESC like '%' + @SOPNUMBE + '%')" +
                    ")";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH != null ? filtr.rowVPH.SOPNUMBE.Trim() : filtr.SOPNUMBE.Trim());

            }
            if (filtr.EAN.Trim().Length > 0)
            {
                command.CommandText += " AND (" +
                    " polozky.BarcodeP like '%' + @EAN + '%'" +
                    " or " +
                    " polozky.VNDITNUM like '%' + @EAN + '%' " +
                    ")";
                command.Parameters.AddWithValue("@EAN", filtr.EAN.Trim());

            }


            //sklad
            if (filtr.rowSKLAD != null)
            {
                command.CommandText += " AND ps.SKL_ID = @SKL_ID";
            }
            else if (filtr.SKLAD.Trim().Length > 0)
            {
                command.CommandText += " AND isnull(ps.SKL_ID, '') like '%' + @SKL_ID + '%'";
            }
            command.Parameters.AddWithValue("@SKL_ID", filtr.rowSKLAD != null ? filtr.rowSKLAD.skl_id.Trim() : filtr.SKLAD.Trim());

            //lokace
            if (filtr.rowLokace != null)
            {
                command.CommandText += " AND ps.LOCNCODE = @LOCNCODE";
                command.Parameters.AddWithValue("@LOCNCODE", filtr.rowLokace.LOCNCODE.Trim());
            }
            else if (filtr.LOCNCODE.Trim().Length > 0)
            {
                command.CommandText +=
                    " AND isnull(ps.LOCNCODE, '') IN (" +
                    "Select LOCNCODE from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " where (Barcode like '%' + @BarcodeL + '%')" +
                    " union " +
                    "Select LOCNCODE from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " where (Description like '%' + @BarcodeL + '%')" +
                    ")"; //"like '%' + @LOCNCODE + '%'";
                command.Parameters.AddWithValue("@BarcodeL", filtr.LOCNCODE.Trim());
            }
            //command.Parameters.AddWithValue("@LOCNCODE", rowLokace != null ? rowLokace.LOCNCODE.Trim() : ComboBoxLOCNCODE.Text);

            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                // hledaní material
                if (filtr.rowZASOBY != null)
                {
                    command.CommandText += " AND ps.ITEMNMBR=@itemdesc";
                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZASOBY.ITEMNMBR.Trim());
                }
                else if (filtr.ZBOZI.Trim().Length > 0)
                {
                    command.CommandText += " AND ps.ITEMNMBR IN (" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMDESC like '%' + @itemdesc + '%'" +
                    " union" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMNMBR like '' + @itemdesc + '%' " +
                    " )";

                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZASOBY != null ? filtr.rowZASOBY.ITEMNMBR.Trim() : filtr.ZBOZI.Trim());
                }


                // hledaní vyrobek
                if (filtr.rowVyrobek != null)
                {
                    command.CommandText += " AND p.ITEMNMBR=@itemnmbr";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.rowVyrobek.ITEMNMBR.Trim());
                }
                else if (filtr.VYROBEK.Trim().Length > 0)
                {
                    command.CommandText += " AND p.ITEMNMBR IN (" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMDESC like '%' + @itemnmbr + '%'" +
                    " union" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMNMBR like '' + @itemnmbr + '%' " +
                    " )";

                    command.Parameters.AddWithValue("@itemnmbr", filtr.rowZASOBY != null ? filtr.rowZASOBY.ITEMNMBR.Trim() : filtr.VYROBEK.Trim());
                }
            }
            // Typ polozky
            if (!string.IsNullOrEmpty(filtr.ITEMTYPE))
            {
                command.CommandText += " AND ps.ITEMTYPE = @ITEMTYPE";
                command.Parameters.AddWithValue("@ITEMTYPE", filtr.ITEMTYPE.Trim());
            }

            // car. kod polozky
            if (!string.IsNullOrEmpty(filtr.ITEMCODE))
            {
                command.CommandText += " AND ps.ITEMCODE = @ITEMCODE";
                command.Parameters.AddWithValue("@ITEMCODE", filtr.ITEMCODE.Trim());
            }

            // merna jednotka
            if (!string.IsNullOrEmpty(filtr.MJ))
            {
                command.CommandText += " AND ps.MJ = @MJ";
                command.Parameters.AddWithValue("@MJ", filtr.MJ.Trim());
            }
            // seriove cislo
            if (!string.IsNullOrEmpty(filtr.SERLTNUM))
            {
                command.CommandText += " AND ps.SERLTNUM = @SERLTNUM";
                command.Parameters.AddWithValue("@SERLTNUM", filtr.SERLTNUM.Trim());
            }

            if (filtr.DatumOd && filtr.DatumDo)
            {
                command.CommandText += " AND ps.ISOK between @datumOd and @datumDo";
                command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            }
            else
            {
                if (filtr.DatumOd)
                {
                    command.CommandText += " AND ps.ISOK > @datumOd";
                    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                }
                else if (filtr.DatumDo)
                {
                    command.CommandText += " AND ps.ISOK < @datumDo";
                    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
                }
            }


            // hledání uživatele
            if (!string.IsNullOrEmpty(filtr.Uzivatel))
            {
                if (filtr.Uzivatel != null)
                {
                    command.CommandText += " AND ps.USER_ID=@name";
                }
                else
                {
                    command.CommandText += " AND ps.USER_ID IN (" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where firstname like '%' + @name + '%'" +
                    " union" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where surname like '%' + @name + '%'" +
                    " union" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where USERID = @name" +
                    " )";
                }
                command.Parameters.AddWithValue("@name", filtr.rowUzivatel != null ? filtr.rowUzivatel.USERID.Trim() : filtr.Uzivatel);
            }

            // ID terminal
            if (!string.IsNullOrEmpty(filtr.TERMINAL_ID))
            {
                command.CommandText += " AND ps.TERMINAL_ID = @TERMINAL_ID";
                command.Parameters.AddWithValue("@TERMINAL_ID", filtr.TERMINAL_ID.Trim());
            }
            // cislo palety
            if (!string.IsNullOrEmpty(filtr.NMBRPAL))
            {
                command.CommandText += " AND ps.NMBRPAL = @NMBRPAL";
                command.Parameters.AddWithValue("@NMBRPAL", filtr.NMBRPAL.Trim());
            }
            // typ palety
            if (!string.IsNullOrEmpty(filtr.TYPEPAL))
            {
                command.CommandText += " AND ps.TYPEPAL = @TYPEPAL";
                command.Parameters.AddWithValue("@TYPEPAL", filtr.TYPEPAL.Trim());
            }
            // vytisknuto
            if (!string.IsNullOrEmpty(filtr.PRINTED))
            {
                command.CommandText += " AND ps.PRINTED = @PRINTED";
                command.Parameters.AddWithValue("@PRINTED", filtr.PRINTED.Trim());
            }

            command.CommandText += " order by ps.ISOK desc";

            #endregion


            vyrobaDataSet1.Production_Sources.Clear();
            vyrobaDataSet1.Production_Sources.AcceptChanges();
            vyrobaDataSet1.Production_Sources.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Sources);
            vyrobaDataSet1.Production_Sources.EndLoadData();

            return vyrobaDataSet1;
        }


        #endregion

        #region IProductionSources_Update_Row Members

        public void Update_Row(Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow Row)
        {
            //SQL_Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter ta = new SQL_Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);
            //ta.Update(Row);

            Database.Vyroba_Production_Sources.Update(Row, ConnectionString);
        }

        #endregion

        #region IProductionSources_Update Members

        public void Update(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt)
        {
            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);
            //ta.Update(dt.ToArray());

            Database.Vyroba_Production_Sources.Update(dt, ConnectionString);
        }

        #endregion

        #region IProductionSources_GetFiltrovanyVazby Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            #region command

            command.CommandText = "select * from Production_Sources";

            command.CommandText += " Where ";
            command.CommandText += "1=1 ";

            if (GUID_Production != null)
            {
                command.CommandText += " AND GUID_Production=@GUID_Production";
                command.Parameters.AddWithValue("@GUID_Production", GUID_Production);

            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            {
                command.CommandText += " AND ITEMNMBR=@ITEMNMBR";
                command.Parameters.AddWithValue("@ITEMNMBR", filtr.MaterialITEMNMBR.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            {
                command.CommandText += " AND ITEMNAME like @ITEMNAME + '%'";
                command.Parameters.AddWithValue("@ITEMNAME", filtr.MaterialITEMDESC.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialMJ))
            {
                command.CommandText += " AND MJ=@MJ";
                command.Parameters.AddWithValue("@MJ", filtr.MaterialMJ.Trim());
            }

            //21.11.2025 MaR pridan filtr-------------------------------------------------
            if (!string.IsNullOrEmpty(filtr.CountEntries))
            {
                command.CommandText += " AND CountEntries=@CountEntries";
                command.Parameters.AddWithValue("@CountEntries", filtr.CountEntries.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SOPNUMBE))
            {
                command.CommandText += " AND SOPNUMBE=@SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SKL_ID))
            {
                command.CommandText += " AND SKL_ID=@SKL_ID";
                command.Parameters.AddWithValue("@SKL_ID", filtr.SKL_ID.Trim());
            }

            if (filtr.PotvrzeniPriznak)
            {
                command.CommandText += " AND idVS IS NULL";
            }
            //---------------------------------------------------------------------------


            #endregion


            vyrobaDataSet1.Production_Sources_Odvod.Clear();
            vyrobaDataSet1.Production_Sources_Odvod.AcceptChanges();
            vyrobaDataSet1.Production_Sources_Odvod.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Sources_Odvod);
            vyrobaDataSet1.Production_Sources_Odvod.EndLoadData();

            return vyrobaDataSet1;
        }

        #endregion

    }
}
