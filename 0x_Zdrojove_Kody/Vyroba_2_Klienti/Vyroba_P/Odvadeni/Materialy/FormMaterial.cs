using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Fask.Vyroba_P.Extensions;
using Fask.SQLiteDBs.DataSets;
using Fask.Logging;
using Fask.Vyroba_P.Forms;
using JR.Utils.GUI.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Fask.Vyroba_P.Odvadeni.Materialy
{
    #region typ pro zobrazeni
    public enum ShowTypes
    {
        _Unknown,
        Back,
        StornoOK
    }
    #endregion

    public partial class FormMaterial : Form
    {
        #region  promenne
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter taZbozi = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();

        private string _ITEMNMBR_def = string.Empty;
        private decimal koefMaterial = 0;
        public decimal mnozstviVyrobku = 0;
        private decimal? zbyvaMaterial = null;
        private decimal? zadanoMaterial = null;
        bool jineVlakno = false;




        private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow = null;
        /// <summary>
        /// Aktualni vyroba
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow
        {
            set { 
                _productionRow = value;
                _ITEMNMBR_def = _productionRow.ITEMNMBR;
            }
        }


        private Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable _productionSDT = null;
        /// <summary>
        ///  Materialy
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT
        {
            set { _productionSDT = value; }
            get { return _productionSDT; }
        }

        private ShowTypes _types = ShowTypes._Unknown;
        /// <summary>
        ///  typ zobrazeni
        /// </summary>
        public ShowTypes types
        {
            set { _types = value; }
        }

        #endregion

        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow MaterialSelected
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[this.productionSourcesBindingSource].Current)).Row as Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow;
                }
                catch
                {
                    return null;
                }
            }

        }

        public FormMaterial()
        {
            InitializeComponent();

            //taZbozi.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));
            // Voláme metodu pro nastavení velikosti tlačítka
            SetButtonWidth();

        }

        private void SetButtonWidth_old()
        {
            // Získáme šířku primárního monitoru
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Nastavíme šířku tlačítka na 80% šířky monitoru
            int buttonWidth = (int)(screenWidth * 0.33);

            // Nastavíme šířku tlačítka
            buttonStorno.Location = new Point(0 * buttonWidth, buttonStorno.Location.Y);
            buttonStorno.MinimumSize = new Size(buttonWidth, 0);


            btn_vlozit.Location = new Point(1 * buttonWidth, buttonStorno.Location.Y);
            //btn_vlozit.Location = new Point(buttonStorno.Size.Width, buttonStorno.Location.Y);
            //btn_vlozit.Size = new Size(buttonStorno.Size.Width/2, buttonStorno.Size.Height);
            
            buttonStorno.MaximumSize = new Size(buttonWidth/2, 0);
            btn_vlozit.Dock = DockStyle.Fill;

            btn_ostatni.Location = new Point(2 * buttonWidth, buttonStorno.Location.Y);
            //btn_ostatni.Location = new Point(buttonStorno.Size.Width + btn_vlozit.Size.Width, buttonStorno.Location.Y);
            //btn_ostatni.Size = btn_vlozit.Size;
           
            btn_ostatni.MaximumSize = new Size(buttonWidth / 2, 0);
            btn_ostatni.Dock = DockStyle.Fill;

            //btn_vlozit.Width = buttonWidth;
            buttonOK.Location = new Point(3* buttonWidth, buttonStorno.Location.Y);
            buttonOK.MaximumSize = new Size(buttonWidth, 0);
            //buttonOK.Width = buttonWidth;
        }

        private void SetButtonWidth()
        {
            int screenWidth = Screen.FromControl(this).Bounds.Width;

            // Pro přesnější výpočet používáme decimal
            decimal buttonWidthDecimal = (decimal)screenWidth * 0.25m;

            // Zaokrouhlujeme na nejbližší celé číslo
            int buttonWidth = (int)Math.Round(buttonWidthDecimal);

            buttonStorno.Dock = DockStyle.None;
            btn_vlozit.Dock = DockStyle.None;
            btn_ostatni.Dock = DockStyle.None;
            buttonOK.Dock = DockStyle.None;

            buttonStorno.Location = new Point(0, buttonStorno.Location.Y);
            buttonStorno.Size = new Size(buttonWidth, buttonStorno.Size.Height);
            buttonStorno.MaximumSize = new Size(buttonWidth, buttonStorno.Size.Height);

            btn_vlozit.Location = new Point(buttonWidth, buttonStorno.Location.Y);
            btn_vlozit.Size = new Size(buttonWidth, buttonStorno.Size.Height);
            btn_vlozit.MaximumSize = new Size(buttonWidth, buttonStorno.Size.Height);

            btn_ostatni.Location = new Point(2 * buttonWidth, buttonStorno.Location.Y);
            btn_ostatni.Size = new Size(buttonWidth, buttonStorno.Size.Height);
            btn_ostatni.MaximumSize = new Size(buttonWidth, buttonStorno.Size.Height);

            buttonOK.Location = new Point(3 * buttonWidth, buttonStorno.Location.Y);
            buttonOK.Size = new Size(buttonWidth, buttonStorno.Size.Height);
            buttonOK.MaximumSize = new Size(buttonWidth, buttonStorno.Size.Height);
        }




        #region Akce se zbozim (materialem)

        private bool NajdiZbozi(string barcode, Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable, bool pokracuj=false)
        {
            try
            {
                if(pokracuj)
                {
                     Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByITEMNMBR_CZMST_095(true, zboziDatatable, barcode);

                }
                else
                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByBarcode_CZMST_095(zboziDatatable, barcode);

                return zboziDatatable.Count > 0;
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        //zakomentovano MaR 22.10.2024
        //private bool NajdiSklad(string barcode, Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable)
        //{
        //    try
        //    {
        //            Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.SKL_ID_ITEMNMBR_CZMST_095( zboziDatatable, barcode);

               
        //        return zboziDatatable.Count > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        FlexibleMessageBox.Show(ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        //        return false;
        //    }
        //}

        private void PridejZbozi(Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable, bool pokracuj = false, bool nevydavat=false, string bcode = null)
        {
            // 1) pokud je jedno zbozi => vybrat a pokracovat
            //    pokud je vice, tak nechat vybrat

            // 2) vlozit do datasetu pro zobrazeni ...
            bool preskocit = false;
            

            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row zboziRow = null;
                if (zboziDatatable.Count == 0)
                {
                    throw new Exception("Pocet polozek je 0!!!");
                }
                else if (zboziDatatable.Count == 1)
                {
                 
                        zboziRow = zboziDatatable[0];
                   
                }
                else
                {
                    using (FormMaterialVyber fmv = new FormMaterialVyber())
                    {
                        fmv.ZboziDatatable = zboziDatatable;
                        //fmv.fask_cons_095datatable = zboziDatatable;
                       
                        if (fmv.ShowDialog() == DialogResult.Cancel)
                            return;
                        //TaD
                        zboziRow = fmv.MaterialSelected;
                    }
                }

                
              

                decimal mnozstvi = 1;
                string sarzeHodnota = string.Empty;

                if (!nevydavat)
                {
                    if (zadanoMaterial.HasValue && zadanoMaterial.Value == 0)
                    {
                        DialogResult dialogResult = FlexibleMessageBox.Show("Materiál má příznak 'nevydávat', přesto vydat?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                        if (DialogResult.Yes == dialogResult)
                        {
                            preskocit = false;
                        }
                        else
                        {
                            preskocit = true;
                            return;
                        } 
                    }

                    if (!preskocit)
                    {
                        jineVlakno = true;

                        //MaR zadat podbarveni 2.9.2024
                        using (FormInputQuantity fiq = new Fask.Vyroba_P.Odvadeni.FormInputQuantity())
                        {

                            fiq.Podbarveni = true;

                            fiq.Owner = this;
                            fiq.Kod = string.Empty;
                            fiq.Text = "Množství";
                            if (!zboziRow.IsITEMDESCNull())
                                fiq.nazev = zboziRow.ITEMDESC.Trim();
                            if (zboziRow.CZ_SerNum_Track == 2)
                            {
                                fiq.sarzeEnable = true;
                                fiq.SarzeMAT_ID = bcode;
                            }
                            // fiq.Mnozstvi = ProductionRow.qty;
                            // fiq.nazev = "";
                            //fiq.Material = nPS.ITEMNAME.Trim();
                            while (true)
                            {
                                if (DialogResult.Cancel == fiq.ShowDialog())
                                    return;
                                try
                                {
                                    mnozstvi = decimal.Parse(fiq.Kod);
                                    if (zboziRow.CZ_SerNum_Track == 2)
                                        sarzeHodnota = fiq.Sarze;
                                }
                                catch (Exception exParse)
                                {
                                    FlexibleMessageBox.Show(exParse.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                    fiq.Kod = fiq.Kod;
                                    continue;
                                }

                                #region old code
                                //if((mnozstvi + zbyvaMaterial) > (_productionRow.qty * koefMaterial))
                                //{
                                //  DialogResult dialogResult =  MessageBox.Show("Přeplnit materiál?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                                //    if (DialogResult.Yes == dialogResult)
                                //    {
                                //        zbyvaMaterial = 0;
                                //        break;
                                //    }

                                //}
                                //else
                                //break; 
                                #endregion



                                if ((zbyvaMaterial.HasValue && mnozstvi > zbyvaMaterial.Value) || (mnozstvi) > (_productionRow.qty * koefMaterial))
                                {
                                    DialogResult dialogResult = FlexibleMessageBox.Show("Přeplnit materiál?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                                    if (DialogResult.Yes == dialogResult)
                                    {
                                        zbyvaMaterial = null;
                                        break;
                                    }

                                }
                                else
                                    break;
                            }
                        }
                        jineVlakno = false;
                    }
                }
                else
                {
                    mnozstvi = 0;

                    if (zadanoMaterial.HasValue && zadanoMaterial.Value == 0)
                    {
                        preskocit = true;

                        FlexibleMessageBox.Show("Materiál již má příznak 'nevydávat'!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                        
                    }
                    else if(zadanoMaterial.HasValue && zadanoMaterial.Value > 0)
                    {
                        preskocit = true;

                        FlexibleMessageBox.Show("Nelze dát příznak 'nevydávat', materiál se již spotřebovává!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                    }
                    else
                    {
                        preskocit = false;
                    }

                }

                if (!preskocit)
                {

                    var nPS = this.ProductionSDT.NewProduction_SourcesRow();

                    if (!_productionRow.IsSKL_IDNull())
                        nPS.SKL_ID = _productionRow.SKL_ID;

                    // TODO : doplnit odpovidajici hodnoty ... 
                    if (!_productionRow.IsCountEntriesNull())
                        nPS.CountEntries = _productionRow.CountEntries;
                    if (!_productionRow.IsSOPNUMBENull())
                        nPS.SOPNUMBE = _productionRow.SOPNUMBE;
                    if (!zboziRow.IsITEMDESCNull())
                        nPS.ITEMNAME = zboziRow.ITEMDESC.Trim();
                    nPS.ITEMNMBR = zboziRow.ITEMNMBR.Trim();
                    nPS.ITEMTYPE = string.Empty;
                    if (!zboziRow.IsITEMCODENull())
                        nPS.ITEMCODE = zboziRow.ITEMCODE.Trim();
                    if (!zboziRow.IsLOCNCODENull())
                        nPS.LOCNCODE = zboziRow.LOCNCODE.Trim();
                    nPS.MJ = zboziRow.MJ.Trim();
                    nPS.GUID_Production = _productionRow.GUID;
                    nPS.GUID = Guid.NewGuid();
                    nPS.USER_ID = _productionRow.UserID;
                    nPS.TERMINAL_ID = _productionRow.TermID;
                    //if (!zboziRow.IsWEIGHTNull())
                    //    nPS.WEIGHT = zboziRow.WEIGHT;
                    nPS.NMBRPAL = string.Empty;
                    nPS.TYPEPAL = string.Empty;
                    nPS.PRINTED = 0;

                    nPS.QTYSHPPD = mnozstvi * (zboziRow.QTYPACK == 0 ? 1 : zboziRow.QTYPACK);
                    nPS.QTYSHPPDMJ = mnozstvi;
                    nPS.QTYPACK = zboziRow.QTYPACK;
                    // TODO : zadani serioveho cisla/sarze ???
                    // TODO : + kontrola ???
                    if (zboziRow.CZ_SerNum_Track == 2)
                        nPS.SERLTNUM = sarzeHodnota;
                    else
                        nPS.SERLTNUM = string.Empty;


                    // TODO : dialog vyberu skladu
                    if (Settings.Production_Material_Source_SKLID_Enter)
                    {

                        #region MaR zakomentovano 21.10.2024
                        //if (!zboziRow.IsSKL_IDNull() && !String.IsNullOrEmpty(zboziRow.SKL_ID.Trim()))
                        //{ // zadani skladu z vybrane polozky ...
                        //  // TODO overeni???
                        //    nPS.SKL_ID = zboziRow.SKL_ID;
                        //}
                        //else
                        //{ //zadani skladu rucne

                        //    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
                        //    //taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                        //    //Zadani ciloveho skladu a cilove lokace ...
                        //    using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                        //    {
                        //        fik.Text = "Zadejte zdrojový sklad";
                        //        if (!nPS.IsSKL_IDNull()
                        //            && !string.IsNullOrEmpty(nPS.SKL_ID))
                        //        {
                        //            var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(nPS.SKL_ID);
                        //            if (listSklady.Count() > 0)
                        //                fik.Kod = listSklady.First().skl_carcode;
                        //        }
                        //        else
                        //            fik.Kod = Settings.Production_Material_Source_SKLID;

                        //        while (true)
                        //        {
                        //            fik.Kod = fik.Kod;
                        //            if (DialogResult.Cancel == fik.ShowDialog())
                        //                return;
                        //            // Test na existenci id cil. skladu ...
                        //            var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                        //            if (listSklady.Count() == 0)
                        //            {
                        //                if (DialogResult.Cancel == FlexibleMessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                        //                    return;
                        //            }
                        //            else
                        //            {
                        //                fik.Kod = listSklady.First().skl_id.Trim();
                        //                break;
                        //            }
                        //        }
                        //        nPS.SKL_ID = fik.Kod;
                        //    }
                        //} 
                        #endregion


                        using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                        {
                            fik.Text = "Zadejte zdrojový sklad";
                            #region old MaR 21.10.2024
                            //if (!nPS.IsSKL_IDNull()
                            //                     && !string.IsNullOrEmpty(nPS.SKL_ID))
                            //{
                            //    var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(nPS.SKL_ID);
                            //    if (listSklady.Count() > 0)
                            //        fik.Kod = listSklady.First().skl_carcode;
                            //}
                            //else
                            //    fik.Kod = Settings.Production_Material_Source_SKLID; 
                            #endregion

                            #region novy MaR 21.10.2024

                            if (Settings.rB_production_Material_Source_SKLID_data)
                            {
                                //if (!nPS.IsSKL_IDNull()
                                //                     && !string.IsNullOrEmpty(nPS.SKL_ID))
                                //{
                                //    var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(nPS.SKL_ID);
                                //    if (listSklady.Count() > 0)
                                //        fik.Kod = listSklady.First().skl_carcode;
                                //}
                                //else
                                //{
                                //    fik.Kod = string.Empty;
                                //}

                                if (!zboziRow.IsSKL_IDNull() && !String.IsNullOrEmpty(zboziRow.SKL_ID.Trim()))
                                { // zadani skladu z vybrane polozky ...
                                  // TODO overeni???
                                    var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(zboziRow.SKL_ID);
                                    if (listSklady.Count() > 0)
                                        fik.Kod = listSklady.First().skl_carcode;

                                    //fik.Kod = zboziRow.SKL_ID;
                                }
                                else
                                {
                                    fik.Kod = string.Empty;
                                }


                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                                {
                                    fik.Kod = Settings.Production_Material_Source_SKLID;
                                }
                                else
                                {
                                    fik.Kod = string.Empty;
                                }
                            }


                            //if (!string.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                            //{
                            //    fik.Kod = Settings.Production_Material_Source_SKLID;
                            //} 
                            #endregion

                            while (true)
                            {
                                fik.Kod = fik.Kod;
                                if (DialogResult.Cancel == fik.ShowDialog())
                                    return;
                                // Test na existenci id cil. skladu ...
                                var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                                if (listSklady.Count() == 0)
                                {
                                    //if (DialogResult.Cancel == FlexibleMessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                    //    return;
                                    CustomMessageBox.Show("Sklad '" + fik.Kod + "' neexistuje v číselníku skladů!", "Zdrojový sklad", 26);

                                   // FlexibleMessageBox.Show("Sklad '" + fik.Kod + "' neexistuje v číselníku skladů!", "Cílový sklad", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        
                                }
                                else
                                {
                                   // fik.Kod = listSklady.First().skl_id.Trim();
                                    fik.Kod = listSklady.First().skl_carcode;
                                    break;
                                }
                            }
                            nPS.SKL_ID = fik.Kod;
                        }

                    }
                    else
                    {


                        #region old MaR 21.102024
                        //if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                        //    nPS.SKL_ID = Settings.Production_Material_Source_SKLID;

                        #endregion


                        if (Settings.rB_production_Material_Source_SKLID_data)
                        {
                            if (!zboziRow.IsSKL_IDNull() && !String.IsNullOrEmpty(zboziRow.SKL_ID.Trim()))
                            { // zadani skladu z vybrane polozky ...
                              // TODO overeni???
                                var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(zboziRow.SKL_ID);
                                if (listSklady.Count() > 0)
                                    nPS.SKL_ID = listSklady.First().skl_carcode;
                                else
                                    nPS.SKL_ID = string.Empty;
                                // nPS.SKL_ID = zboziRow.SKL_ID;
                            }
                            else
                            {
                                nPS.SKL_ID = string.Empty;
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                            {

                                var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(Settings.Production_Material_Source_SKLID);
                                if (listSklady.Count() > 0)
                                    nPS.SKL_ID = listSklady.First().skl_carcode;
                                else
                                {
                                   // FlexibleMessageBox.Show("Sklad '" + Settings.Production_Material_Source_SKLID + "' neexistuje v číselníku skladů!", "Cílový sklad", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                    CustomMessageBox.Show("Sklad '" + Settings.Production_Material_Source_SKLID + "' neexistuje v číselníku skladů!", "Zdrojový sklad", 26);

                                    nPS.SKL_ID = Settings.Production_Material_Source_SKLID;
                                }


                                //nPS.SKL_ID = Settings.Production_Material_Source_SKLID;
                            }
                            else
                            {
                                nPS.SKL_ID = string.Empty;
                            }
                        }


                    }

                    // TODO : dialog vyberu lokace dle skladu
                    if (Settings.Production_Material_Source_LOCNCODE_Enter)
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
                        //taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                        //zadani cilove lokace
                        using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                        {
                            fik.Text = "Zadejte zdroj. lokaci"; // "Zadejte zdrojovou lokaci"
                            if (!nPS.IsSKL_IDNull()
                                && !nPS.IsLOCNCODENull()
                                && !string.IsNullOrEmpty(nPS.SKL_ID)
                                && !string.IsNullOrEmpty(nPS.LOCNCODE)
                                )
                            {
                                var listLokace = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(nPS.SKL_ID, nPS.LOCNCODE);
                                if (listLokace.Count() > 0)
                                    fik.Kod = listLokace.First().Barcode;
                            }
                            else
                                fik.Kod = Settings.Production_Material_Source_LOCNCODE;

                            while (true)
                            {
                                fik.Kod = fik.Kod;
                                if (DialogResult.Cancel == fik.ShowDialog())
                                    return;

                                // Test na existenci id cil. lokace ...
                                var listLokace = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(nPS.SKL_ID, fik.Kod);
                                if (listLokace.Count() == 0)
                                {
                                    if (DialogResult.Cancel == FlexibleMessageBox.Show("Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                        return;
                                }
                                else
                                {
                                    fik.Kod = listLokace.First().LOCNCODE.Trim();
                                    break;
                                }
                            }

                            nPS.LOCNCODE = fik.Kod;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_LOCNCODE))
                            nPS.LOCNCODE = Settings.Production_Material_Source_LOCNCODE;
                    }


                    //this.PSDataTable.AddProduction_SourcesRow(nPS);
                    //this._productionSDT.AddProduction_SourcesRow(nPS);
                    this.ProductionSDT.AddProduction_SourcesRow(nPS);


                    //this.MaterialSelected = nPS;

                }
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(ex.Message, "Pridani", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
            }

            if(pokracuj)
            {
                Vloz_Material();
            }
        }




        #endregion

        private void FormMaterial_Load(object sender, EventArgs e)
        {           

            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);

            this.dataGridView1.LoadConfiguration(this.GetType().ToString());

            
            ScannerStart(); //----MaR 17.1. 2024

            #region ???
            //foreach (var i in _productionSDT)
            //{
            //    try
            //    {
            //        var row = this.VyrobaDatasetPS.Production_Sources.NewProduction_SourcesRow();

            //        if (i.IsCountEntriesNull())
            //            row.SetCountEntriesNull();
            //        else
            //            row.CountEntries = i.CountEntries;



            //        if (i.IsGUID_ProductionNull())
            //            row.SetGUID_ProductionNull();
            //        else
            //            row.GUID_Production = i.GUID_Production;

            //        if (i.IsGUIDNull())
            //            row.SetGUIDNull();
            //        else
            //            row.GUID = i.GUID;

            //        if (i.IsITEMCODENull())
            //            row.SetITEMCODENull();
            //        else
            //            row.ITEMCODE = i.ITEMCODE.Trim();

            //        if (i.IsITEMNAMENull())
            //            row.SetITEMNAMENull();
            //        else
            //            row.ITEMNAME = i.ITEMNAME.Trim();

            //        if (i.IsITEMNMBRNull())
            //            row.SetITEMNMBRNull();
            //        else
            //            row.ITEMNMBR = i.ITEMNMBR.Trim();

            //        if (i.IsITEMTYPENull())
            //            row.SetITEMTYPENull();
            //        else
            //            row.ITEMTYPE = i.ITEMTYPE.Trim();

            //        if (i.IsLOCNCODENull())
            //            row.SetLOCNCODENull();
            //        else
            //            row.LOCNCODE = i.LOCNCODE.Trim();

            //        if (i.IsNMBRPALNull())
            //            row.SetNMBRPALNull();
            //        else
            //            row.NMBRPAL = i.NMBRPAL.Trim();

            //        if (i.IsPRINTEDNull())
            //            row.SetPRINTEDNull();
            //        else
            //            row.PRINTED = i.PRINTED;

            //        if (i.IsQTYPACKNull())
            //            row.SetQTYPACKNull();
            //        else
            //            row.QTYPACK = i.QTYPACK;

            //        if (i.IsSKL_IDNull())
            //            row.SetSKL_IDNull();
            //        else
            //            row.SKL_ID = i.SKL_ID.Trim();

            //        if (i.IsSOPNUMBENull())
            //            row.SetSOPNUMBENull();
            //        else
            //            row.SOPNUMBE = i.SOPNUMBE.Trim();

            //        if (i.IsTYPEPALNull())
            //            row.SetTYPEPALNull();
            //        else
            //            row.TYPEPAL = i.TYPEPAL.Trim();

            //        if (i.IsUSER_IDNull())
            //            row.SetUSER_IDNull();
            //        else
            //            row.USER_ID = i.USER_ID.Trim();

            //        if (i.IsWEIGHTNull())
            //            row.SetWEIGHTNull();
            //        else
            //            row.WEIGHT = i.WEIGHT;


            //        row.DEX_ROW_ID = i.DEX_ROW_ID;
            //        row.MJ = i.MJ.Trim();
            //        row.QTYSHPPD = i.QTYSHPPD;
            //        row.QTYSHPPDMJ = i.QTYSHPPDMJ;
            //        row.SERLTNUM = i.SERLTNUM.Trim();
            //        row.TERMINAL_ID = i.TERMINAL_ID;


            //        this.VyrobaDatasetPS.Production_Sources.AddProduction_SourcesRow(row);


            //    }
            //    catch (Exception ex)
            //    { MessageBox.Show(ex.Message, this.Text); }
            //}
            #endregion

            productionSourcesBindingSource.DataSource = this.ProductionSDT;

            if (_types == ShowTypes.StornoOK)
            {
                panelButtons_Resize(null, null);
            }
            else if (_types == ShowTypes.Back)
            {
                buttonOK.Visible = false;
                toolStripMenuItemOK.Text = "Pokračovat";
                buttonStorno.Text = "Pokračovat";
                toolStripMenuItemStorno.Visible = false;
            }     
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelbutton.Width / 2, panelbutton.Height);
            buttonOK.Size = nsize;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformStorno();
        }

        public void PerformOK()
        {
            this.finalize();
            this.DialogResult = DialogResult.OK;
        }

        public void PerformStorno()
        {
            this.finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            this.ScannerFinalize();

            this.dataGridView1.SaveConfiguration(this.GetType().ToString());
            #region ???
            //foreach (var i in VyrobaDatasetPS.Production_Sources)
            //{
            //    try
            //    {
                   
            //        //var row = this.VyrobaDatasetPS.Production_Sources.NewProduction_SourcesRow();
            //        var row = this._productionSDT.NewProduction_SourcesRow();

            //        if (i.IsCountEntriesNull())
            //           row.SetCountEntriesNull();
            //        else
            //            row.CountEntries = i.CountEntries;



            //        if (i.IsGUID_ProductionNull())
            //            row.SetGUID_ProductionNull();
            //        else
            //            row.GUID_Production = i.GUID_Production;

            //        if (i.IsGUIDNull())
            //            row.SetGUIDNull();
            //        else
            //            row.GUID = i.GUID;

            //        if (i.IsITEMCODENull())
            //            row.SetITEMCODENull();
            //        else
            //            row.ITEMCODE = i.ITEMCODE;

            //        if (i.IsITEMNAMENull())
            //            row.SetITEMNAMENull();
            //        else
            //            row.ITEMNAME = i.ITEMNAME;

            //        if (i.IsITEMNMBRNull())
            //            row.SetITEMNMBRNull();
            //        else
            //            row.ITEMNMBR = i.ITEMNMBR;

            //        if (i.IsITEMTYPENull())
            //            row.SetITEMTYPENull();
            //        else
            //            row.ITEMTYPE = i.ITEMTYPE;

            //        if (i.IsLOCNCODENull())
            //            row.SetLOCNCODENull();
            //        else
            //            row.LOCNCODE = i.LOCNCODE;

            //        if (i.IsNMBRPALNull())
            //            row.SetNMBRPALNull();
            //        else
            //            row.NMBRPAL = i.NMBRPAL;

            //        if (i.IsPRINTEDNull())
            //            row.SetPRINTEDNull();
            //        else
            //            row.PRINTED = i.PRINTED;

            //        if (i.IsQTYPACKNull())
            //            row.SetQTYPACKNull();
            //        else
            //            row.QTYPACK = i.QTYPACK;

            //        if (i.IsSKL_IDNull())
            //            row.SetSKL_IDNull();
            //        else
            //            row.SKL_ID = i.SKL_ID;

            //        if (i.IsSOPNUMBENull())
            //            row.SetSOPNUMBENull();
            //        else
            //            row.SOPNUMBE = i.SOPNUMBE;

            //        if (i.IsTYPEPALNull())
            //            row.SetTYPEPALNull();
            //        else
            //            row.TYPEPAL = i.TYPEPAL;

            //        if (i.IsUSER_IDNull())
            //            row.SetUSER_IDNull();
            //        else
            //            row.USER_ID = i.USER_ID;

            //        if (i.IsWEIGHTNull())
            //            row.SetWEIGHTNull();
            //        else
            //            row.WEIGHT = i.WEIGHT;
                    
    
            //        row.DEX_ROW_ID = i.DEX_ROW_ID;
            //        row.MJ = i.MJ;
            //        row.QTYSHPPD = i.QTYSHPPD;
            //        row.QTYSHPPDMJ = i.QTYSHPPDMJ;
            //        row.SERLTNUM = i.SERLTNUM;
            //        row.TERMINAL_ID = i.TERMINAL_ID;


            //        try
            //        {
            //            this._productionSDT.AddProduction_SourcesRow(row);
            //        }
            //        catch(Exception ex)
            //        {
            //        //Vhodi vznimku kdyz existuje prvek s stejnim GUID jak uz v seznamu je
            //        }
            //        //this.VyrobaDatasetPS.Production_Sources.AddProduction_SourcesRow(row);
            //    }
            //    catch (Exception ex)
            //    { MessageBox.Show(ex.Message, this.Text); }
            //}

            //this._productionSDT.AcceptChanges();
            ////VyrobaDatasetPS.Production_Sources.AcceptChanges();
            #endregion
        }

        #region Scanner car.kodu
        bool scannerefinalized = false;
        private void ScannerFinalize()
        {
            scannerefinalized = true;
            ScannerStop();
        }

        private void ScannerStart()
        {
            if (scannerefinalized)
                return;

            try
            {
                Forms.FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                Forms.FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                Forms.FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                Forms.FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                Forms.FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            if (!jineVlakno)
            {
                try
                {
                    ScannerStop();

                    string bcode = e.BarcodeData.Trim();

                    if (bcode.Length == 0)
                        return;

                    NajdiAPridejZbozi(bcode);

                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
                finally
                {
                    ScannerStart();
                }
            }


        }

        void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            try
            {
                if (!jineVlakno)
                    this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });

            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion



        private void NajdiAPridejZbozi(string bcode, bool pokracuj=false, bool nevydavat=false)
        {
            // akce s pridanim materialu ...
            Fask.SQLiteDBs.DataSets.Vyroba zboziDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable = zboziDataSet.FASK_CONS_095;
            if (NajdiZbozi(bcode, zboziDatatable,pokracuj))
            {
                PridejZbozi(zboziDatatable, pokracuj, nevydavat,bcode);
            }
            else
            {
                FlexibleMessageBox.Show("Zboží '" + bcode + "' nenalezeno", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
       

        private void FormMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (_types == ShowTypes.StornoOK)
            {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        PerformOK();
                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        PerformStorno();
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        PerformDelete();
                    }
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            }
            else if (_types == ShowTypes.Back)
            {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        PerformStorno();
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        PerformDelete();
                    }
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            }

        }

        private void PerformDelete()
        {
            var vybranymat = this.MaterialSelected;
            if (vybranymat != null)
            {
                if (DialogResult.No == FlexibleMessageBox.Show(
                    "Odstranit ?\n" + vybranymat.ITEMNAME.Trim() + "\n" + vybranymat.QTYSHPPD.ToString() + " " + vybranymat.MJ.Trim()
                    , this.Text
                    , MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1
                    ))
                    return;
                //MaR 21.2.2024
                //this.VyrobaDatasetPS.Production_Sources.RemoveProduction_SourcesRow(vybranymat);
                this._productionSDT.RemoveProduction_SourcesRow(vybranymat);


            }
        }


        private void HledejCarkod()
        {
            try
            {
                ScannerStop();
                string bkod = string.Empty;

                using (Fask.Vyroba_P.Forms.FormInputKod fik = new  Fask.Vyroba_P.Forms.FormInputKod())
                {
                    fik.Text = "Čár. kód operace";
                    if (DialogResult.Cancel == fik.ShowDialog())
                        return;

                    bkod = fik.Kod;
                }

                NajdiAPridejZbozi(bkod);
            }
            catch (Exception ex)
            {
                //zalogovat
                string exx = ex.Message.ToString();
            }
            finally
            {
                ScannerStart();
            }
        }


        private void toolStripMenuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformStorno();
        }

        private void toolStripMenuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void toolStripMenuItemSmazat_Click(object sender, EventArgs e)
        {
            this.PerformDelete();
        }

        private void toolStripMenuItemCarKod_Click(object sender, EventArgs e)
        {
            HledejCarkod();
        }

        private void btn_vlozit_Click(object sender, EventArgs e)
        {

            ////// Použití vlastního MessageBoxu
            //using (var customMessageBox = new FormDialog(20, "Chcete zobrazit jen zbývající materiály?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //{
            //    if (customMessageBox.ShowDialog() == DialogResult.Yes)
            //    {
            //        // Uživatel stiskl Yes
            //    }
            //    else
            //    {
            //        return;
            //        // Uživatel stiskl No
            //    }
            //}


            Vloz_Material();
        }

        private void Vloz_Material()
        {
            // akce s pridanim materialu ...
            Fask.SQLiteDBs.DataSets.Vyroba zboziDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable zboziDatatable = zboziDataSet.FASK_Vyroba_TP;

            if (NajdiZbozi_TP(_ITEMNMBR_def, out zboziDatatable))
            {
                PridejZbozi_TP(zboziDatatable);
            }
            else
            {
                FlexibleMessageBox.Show("Zboží '" + _ITEMNMBR_def + "' nenalezeno", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);


                //// Použití vlastního MessageBoxu
                //using (var customMessageBox = new FormDialog(20, "Zboží '" + _ITEMNMBR_def + "' nenalezeno", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                //{
                //    customMessageBox.ShowDialog();
                   
                //}


            }


        }

        private bool NajdiZbozi_TP(string barcode, out Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable zboziDatatable)
        {
            try
            {
                zboziDatatable = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(barcode);

                return zboziDatatable.Count > 0;
            }
            catch (Exception ex)
            {
                zboziDatatable = null;
                 FlexibleMessageBox.Show(ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //using (var customMessageBox = new FormDialog(20, ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                //{
                //    customMessageBox.ShowDialog();

                //}


                return false;
            }
        }

        private bool NajdiMaterial_TP(string barcode, out Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable zboziDatatable)
        {
            try
            {
                zboziDatatable = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBy_ITEMNMBR_fol_IDHNULL_FASK_Vyroba_TP(barcode);

                return zboziDatatable.Count > 0;
            }
            catch (Exception ex)
            {
                zboziDatatable = null;
                FlexibleMessageBox.Show(ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //using (var customMessageBox = new FormDialog(20, ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                //{
                //    customMessageBox.ShowDialog();

                //}
                return false;
            }
        }

        private void PridejZbozi_TP(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable zboziDatatable)
        {
            // 1) pokud je jedno zbozi => vybrat a pokracovat
            //    pokud je vice, tak nechat vybrat

            // 2) vlozit do datasetu pro zobrazeni ...

            zadanoMaterial = null;

            try
            {


                #region dopocitani poctu spotrebovanych materialu
                decimal pocetMat = 0;
                // Název sloupce, ve kterém hledáte hodnotu
                string nazevSloupce = "TEMNMBR";
                string nazevSloupcePorovnat = string.Empty;
                string nazevSloupceHledat = string.Empty;

                // Získání názvů sloupců
                List<string> nazvySloupcu = new List<string>();

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    nazevSloupcePorovnat = column.Name;
                    if (nazevSloupcePorovnat.Contains(nazevSloupce))
                    {
                        nazevSloupceHledat = nazevSloupcePorovnat;
                    }

                    nazvySloupcu.Add(column.Name);
                }



                if (dataGridView1.Rows.Count > 0)
                {
                  

                    // Hodnota, kterou hledáte
                    string hledanaHodnota = _ITEMNMBR_def;

                    // Projdeme všechny řádky v DataGridView
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        pocetMat = 0;

                        // Zkontrolujeme, zda řádek není prázdný
                        if (!row.IsNewRow)
                        {


                            // Získáme hodnotu v daném sloupci pro aktuální řádek
                            DataGridViewCell cell = row.Cells[nazevSloupceHledat];

                            if (cell != null && cell.Value != null)
                            {
                                // Získáme hodnotu buňky
                                object hodnotaSloupce = cell.Value;


                                foreach (DataGridViewRow rowSum in dataGridView1.Rows)
                                {
                                    // Zkontrolujeme, zda řádek není prázdný
                                    if (!rowSum.IsNewRow)
                                    {
                                        if(rowSum.Cells[nazevSloupceHledat].Value.ToString().Trim() == hodnotaSloupce.ToString().Trim())
                                        {
                                            object x_hodnota = rowSum.Cells["qTYSHPPDDataGridViewTextBoxColumn"].Value;
                                            string x_hodnota2 = x_hodnota.ToString().Trim();
                                            pocetMat += decimal.Parse(x_hodnota2);
                                            //pocetMat += decimal.Parse(x_hodnota2, CultureInfo.InvariantCulture);
                                        }
                                    }
                                }

                                // Projdeme všechny řádky v DataTable a nastavíme hodnotu sloupce "X" na 20
                                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rowPridat in zboziDatatable.Rows)
                                {

                                    rowPridat.Mn_vyrobku = mnozstviVyrobku;
                                    rowPridat.Predpis_Mn = rowPridat.Mn_vyrobku * decimal.Parse(rowPridat.koef.Trim(), CultureInfo.InvariantCulture);// decimal.Parse(rowPridat.koef);

                                    if(rowPridat.ITEMNMBR_fol.ToString().Trim() == cell.Value.ToString().Trim())
                                    {
                                        rowPridat.Zadane_Mn = pocetMat;
                                        rowPridat.Zbyva_Mn = rowPridat.Predpis_Mn - rowPridat.Zadane_Mn;
                                    }
                                    //if(row.)

                                }



                            }
                        }
                    }
                }
                #endregion



                // Projdeme všechny řádky v DataTable a nastavíme hodnotu sloupce "X" na 20
                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in zboziDatatable.Rows)
                {

                    row.Mn_vyrobku = mnozstviVyrobku;

                    //string row.koef = "3.25";
                    decimal result = decimal.Parse(row.koef.Trim(), CultureInfo.InvariantCulture);
                    row.Predpis_Mn = row.Mn_vyrobku * result;

                    //if (decimal.TryParse(row.koef.Trim(), out decimal decimalValue))
                    //{
                    //    row.Predpis_Mn = row.Mn_vyrobku * decimalValue;
                    //}
                    //else
                    //{
                    //    //chyba parsovani
                    //    return;
                    //}

                    //row.Predpis_Mn = row.Mn_vyrobku * decimal.Parse(row.koef);// int.Parse(row.koef);
                    //if(row.)

                }


                #region zobrazeni nevydanych materialu



                DialogResult dialogResult = FlexibleMessageBox.Show("Chcete zobrazit jen zbývající materiály?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == dialogResult)
                {
                    // Seznam pro shromáždění řádků, které chcete odstranit
                    List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow> rowsToRemove = new List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow>();

                    foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in zboziDatatable.Rows)
                    {
                        if (!row.IsZbyva_MnNull() && row.Zbyva_Mn < 0 || !row.IsZadane_MnNull() && row.Zadane_Mn == 0)
                        {
                            // Přidat řádek do seznamu pro odstranění
                            rowsToRemove.Add(row);
                        }
                    }

                    // Odstranit shromážděné řádky po skončení iterace
                    foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rowToRemove in rowsToRemove)
                    {
                        zboziDatatable.Rows.Remove(rowToRemove);
                    }
                }

                //using (var customMessageBox = new FormDialog(20, "Chcete zobrazit jen zbývající materiály?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                //{
                //    //customMessageBox.ShowDialog();
                ////}
                //    if (DialogResult.Yes == customMessageBox.ShowDialog())
                //    {
                //        // Seznam pro shromáždění řádků, které chcete odstranit
                //        List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow> rowsToRemove = new List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow>();

                //        foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in zboziDatatable.Rows)
                //        {
                //            if (!row.IsZbyva_MnNull() && row.Zbyva_Mn <= 0)
                //            {
                //                // Přidat řádek do seznamu pro odstranění
                //                rowsToRemove.Add(row);
                //            }
                //        }

                //        // Odstranit shromážděné řádky po skončení iterace
                //        foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rowToRemove in rowsToRemove)
                //        {
                //            zboziDatatable.Rows.Remove(rowToRemove);
                //        }
                //    }

                //}



                #endregion

            }
            catch (Exception ex)
            {

                throw ex;
            }

            bool nevydavat = false;

            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow zboziRow = null;
                if (zboziDatatable.Count == 0)
                {
                    throw new Exception("Pocet polozek je 0!!!");
                }
                else if (zboziDatatable.Count == 1)
                {

                    if (Settings.Production_Material_AUTO_vyber)
                    {

                        zboziRow = zboziDatatable[0];
                    }
                    else
                    {
                        using (FormMaterialVyberTP fmv = new FormMaterialVyberTP())
                        {
                            jineVlakno = true;
                            SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable materialDatatable = new SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
                            // zboziDatatable.
                            fmv.ZboziDatatable = zboziDatatable;
                            //fmv.fask_cons_095datatable = zboziDatatable;
                           
                            if (fmv.ShowDialog() == DialogResult.Cancel)
                            {
                                jineVlakno = false;
                                if (Settings.Production_Material_Dopocist)
                                {
                                    if (fmv.ListNMBR != null && fmv.ListNMBR.Count > 0)
                                    {
                                        // Vytvoření a inicializace kolekce řetězců
                                        List<string> seznamITEMNMBR_fol = new List<string>();

                                        seznamITEMNMBR_fol = fmv.ListNMBR;
                                        //logika dopoctu
                                        PridejMaterialDopocet(seznamITEMNMBR_fol);
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }

                            //TaD
                            jineVlakno = false;
                            zboziRow = fmv.MaterialSelected;
                            nevydavat = fmv.nevydavat;
                        }
                    }
                }
                else
                {
                    using (FormMaterialVyberTP fmv = new FormMaterialVyberTP())
                    {
                        jineVlakno = true;
                        SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable materialDatatable = new SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
                        // zboziDatatable.
                        fmv.ZboziDatatable = zboziDatatable;
                        //fmv.fask_cons_095datatable = zboziDatatable;

                        if (fmv.ShowDialog() == DialogResult.Cancel)
                        {
                            jineVlakno = false;
                            if (Settings.Production_Material_Dopocist)
                            {
                                if (fmv.ListNMBR != null && fmv.ListNMBR.Count > 0)
                                {
                                    // Vytvoření a inicializace kolekce řetězců
                                    List<string> seznamITEMNMBR_fol = new List<string>();

                                    seznamITEMNMBR_fol = fmv.ListNMBR;
                                    //logika dopoctu
                                    PridejMaterialDopocet(seznamITEMNMBR_fol);
                                    return; 
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                           
                        //TaD
                        zboziRow = fmv.MaterialSelected;
                        nevydavat = fmv.nevydavat;

                        jineVlakno = false;
                    }
                }

                string material_ITEMNMBR_fol = zboziRow.ITEMNMBR_fol;

                koefMaterial = decimal.Parse(zboziRow.koef.Trim(), CultureInfo.InvariantCulture);// decimal.Parse(zboziRow.koef);

                if(!zboziRow.IsZbyva_MnNull())
                zbyvaMaterial = zboziRow.Zbyva_Mn;

                if (!zboziRow.IsZadane_MnNull())
                    zadanoMaterial = zboziRow.Zadane_Mn;


                NajdiAPridejZbozi(material_ITEMNMBR_fol,true, nevydavat);

                #region OLD
                //var nPS = this.ProductionSDT.NewProduction_SourcesRow();



                //// TODO : doplnit odpovidajici hodnoty ... 
                //if (!_productionRow.IsCountEntriesNull())
                //    nPS.CountEntries = _productionRow.CountEntries;
                //if (!_productionRow.IsSOPNUMBENull())
                //    nPS.SOPNUMBE = _productionRow.SOPNUMBE;
                //if (!zboziRow.IsITEMDESCNull())
                //    nPS.ITEMNAME = zboziRow.ITEMDESC.Trim();
                //nPS.ITEMNMBR = zboziRow.ITEMNMBR.Trim();
                //nPS.ITEMTYPE = string.Empty;
                //if (!zboziRow.IsITEMCODENull())
                //    nPS.ITEMCODE = zboziRow.ITEMCODE.Trim();
                //if (!zboziRow.IsLOCNCODENull())
                //    nPS.LOCNCODE = zboziRow.LOCNCODE.Trim();
                //nPS.MJ = zboziRow.MJ.Trim();
                //nPS.GUID_Production = _productionRow.GUID;
                //nPS.GUID = Guid.NewGuid();
                //nPS.USER_ID = _productionRow.UserID;
                //nPS.TERMINAL_ID = _productionRow.TermID;
                ////if (!zboziRow.IsWEIGHTNull())
                ////    nPS.WEIGHT = zboziRow.WEIGHT;
                //nPS.NMBRPAL = string.Empty;
                //nPS.TYPEPAL = string.Empty;
                //nPS.PRINTED = 0;


                //decimal mnozstvi = 1;
                //using (FormInputQuantity fiq = new Fask.Vyroba_P.Odvadeni.FormInputQuantity())
                //{
                //    fiq.Owner = this;
                //    fiq.Kod = string.Empty;
                //    fiq.Text = "Množství";
                //    //fiq.Material = nPS.ITEMNAME.Trim();
                //    while (true)
                //    {
                //        if (DialogResult.Cancel == fiq.ShowDialog())
                //            return;
                //        try
                //        {
                //            mnozstvi = decimal.Parse(fiq.Kod);
                //        }
                //        catch (Exception exParse)
                //        {
                //            MessageBox.Show(exParse.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                //            fiq.Kod = fiq.Kod;
                //            continue;
                //        }
                //        break;
                //    }
                //}

                //nPS.QTYSHPPD = mnozstvi * (zboziRow.QTYPACK == 0 ? 1 : zboziRow.QTYPACK);
                //nPS.QTYSHPPDMJ = mnozstvi;
                //nPS.QTYPACK = zboziRow.QTYPACK;
                //// TODO : zadani serioveho cisla/sarze ???
                //// TODO : + kontrola ???
                //nPS.SERLTNUM = string.Empty;

                //// TODO : dialog vyberu skladu
                //if (Settings.Production_Material_Source_SKLID_Enter)
                //{
                //    if (!zboziRow.IsSKL_IDNull() && !String.IsNullOrEmpty(zboziRow.SKL_ID.Trim()))
                //    { // zadani skladu z vybrane polozky ...
                //        // TODO overeni???
                //        nPS.SKL_ID = zboziRow.SKL_ID;
                //    }
                //    else
                //    { //zadani skladu rucne

                //        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
                //        //taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                //        //Zadani ciloveho skladu a cilove lokace ...
                //        using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                //        {
                //            fik.Text = "Zadejte zdrojový sklad";
                //            if (!nPS.IsSKL_IDNull()
                //                && !string.IsNullOrEmpty(nPS.SKL_ID))
                //            {
                //                var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(nPS.SKL_ID);
                //                if (listSklady.Count() > 0)
                //                    fik.Kod = listSklady.First().skl_carcode;
                //            }
                //            else
                //                fik.Kod = Settings.Production_Material_Source_SKLID;

                //            while (true)
                //            {
                //                fik.Kod = fik.Kod;
                //                if (DialogResult.Cancel == fik.ShowDialog())
                //                    return;
                //                // Test na existenci id cil. skladu ...
                //                var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                //                if (listSklady.Count() == 0)
                //                {
                //                    if (DialogResult.Cancel == MessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                //                        return;
                //                }
                //                else
                //                {
                //                    fik.Kod = listSklady.First().skl_id.Trim();
                //                    break;
                //                }
                //            }
                //            nPS.SKL_ID = fik.Kod;
                //        }
                //    }
                //}
                //else
                //{
                //    if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                //        nPS.SKL_ID = Settings.Production_Material_Source_SKLID;
                //}

                //// TODO : dialog vyberu lokace dle skladu
                //if (Settings.Production_Material_Source_LOCNCODE_Enter)
                //{
                //    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
                //    //taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                //    //zadani cilove lokace
                //    using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                //    {
                //        fik.Text = "Zadejte zdroj. lokaci"; // "Zadejte zdrojovou lokaci"
                //        if (!nPS.IsSKL_IDNull()
                //            && !nPS.IsLOCNCODENull()
                //            && !string.IsNullOrEmpty(nPS.SKL_ID)
                //            && !string.IsNullOrEmpty(nPS.LOCNCODE)
                //            )
                //        {
                //            var listLokace = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(nPS.SKL_ID, nPS.LOCNCODE);
                //            if (listLokace.Count() > 0)
                //                fik.Kod = listLokace.First().Barcode;
                //        }
                //        else
                //            fik.Kod = Settings.Production_Material_Source_LOCNCODE;

                //        while (true)
                //        {
                //            fik.Kod = fik.Kod;
                //            if (DialogResult.Cancel == fik.ShowDialog())
                //                return;

                //            // Test na existenci id cil. lokace ...
                //            var listLokace = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(nPS.SKL_ID, fik.Kod);
                //            if (listLokace.Count() == 0)
                //            {
                //                if (DialogResult.Cancel == MessageBox.Show("Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                //                    return;
                //            }
                //            else
                //            {
                //                fik.Kod = listLokace.First().LOCNCODE.Trim();
                //                break;
                //            }
                //        }

                //        nPS.LOCNCODE = fik.Kod;
                //    }
                //}
                //else
                //{
                //    if (!String.IsNullOrEmpty(Settings.Production_Material_Source_LOCNCODE))
                //        nPS.LOCNCODE = Settings.Production_Material_Source_LOCNCODE;
                //}


                ////this.PSDataTable.AddProduction_SourcesRow(nPS);
                ////this._productionSDT.AddProduction_SourcesRow(nPS);
                //this.ProductionSDT.AddProduction_SourcesRow(nPS);
                #endregion


                //this.MaterialSelected = nPS;

            }
            catch (Exception ex)
            {
                jineVlakno = false;
                FlexibleMessageBox.Show(ex.Message, "Pridani", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //using (var customMessageBox = new FormDialog(20, ex.Message, "Pridani", MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                //{
                //    customMessageBox.ShowDialog();
                //}

            }
            finally
            {
            }

          
        }




        private void PridejMaterialDopocet(List<string> seznamITEMNMBR_fol)
        {
            bool maSarzi = false;

            if(seznamITEMNMBR_fol.Count > 0)
            {


                foreach (var item in seznamITEMNMBR_fol)
                {

                    string material = string.Empty;
                    material = item;
                   if( MaterialOverSarzi(material))
                    {
                        maSarzi = true;
                        break;
                    }

                }

                if(maSarzi)
                {


                    if(Settings.Production_Material_Doplnit_Sarze)
                    {
                       // DialogResult dir = FlexibleMessageBox.Show("Vložit přednastavenou šarži materiálu?", "Příznak šarže", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        DialogResult dir = FlexibleMessageBox.Show(this, "Vložit přednastavenou šarži materiálu?", "Příznak šarže", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, null, "Ano", "Storno");


                        if (dir == DialogResult.Yes)
                        {
                            #region kontrola prednastavene sarze
                            //if (Settings.OdvadeniKontrolaSarze)
                            //{
                            //    (int returnValue, string outputMessage) = CheckSerial(Settings.Production_Material_Hodnota_Sarze);

                            //    if (returnValue != 0)
                            //    {
                            //        FlexibleMessageBox.Show(outputMessage + " -- Materiály nebyly dopočítány!", "Ověření přednastavená šarže", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        return;
                            //    }

                            //} 
                            #endregion


                            foreach (var item in seznamITEMNMBR_fol)
                            {
                                string material = string.Empty;
                                material = item;
                                PridejMaterial(material);
                            }
                        }
                        else if (dir == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        DialogResult dir = FlexibleMessageBox.Show(this, "Materiály bez šarže nebudou přeneseny." + Environment.NewLine + Environment.NewLine + "Pokračovat?", "Příznak šarže", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, null, "Ano", "Ne");

                        //FlexibleMessageBox.Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, Color color, string button1Text, string button2Text)


                        //DialogResult dir = FlexibleMessageBox.ShowWithButtons("Materiály bez šarže nebudou přeneseny." + Environment.NewLine + Environment.NewLine + "Pokračovat?", "Příznak šarže", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, "Ano", "Ne");


                        if (dir == DialogResult.Yes)
                        {
                            foreach (var item in seznamITEMNMBR_fol)
                            {
                                string material = string.Empty;
                                material = item;
                                PridejMaterial(material, true);
                            }
                        }
                        else if (dir == DialogResult.No)
                        {
                            return;
                        }
                    }

                 


                }
                else
                {
                    foreach (var item in seznamITEMNMBR_fol)
                    {
                        string material = string.Empty;
                        material = item;
                        PridejMaterial(material);
                    }
                }

              
            }
        }

        private bool MaterialOverSarzi(string materialBcode)
        {
            bool maSarzi = false;
            // akce s pridanim materialu ...
            Fask.SQLiteDBs.DataSets.Vyroba zboziDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable = zboziDataSet.FASK_CONS_095;
            if (NajdiZbozi(materialBcode, zboziDatatable, true))
            {
                if (zboziDatatable.Count > 0)
                {

                    foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row row in zboziDatatable)
                    {
                       if(row.CZ_SerNum_Track == 2)
                        {
                            return true;
                        }
                    }
                }
            }

            return maSarzi;
        }

        private void PridejMaterial(string materialBcode, bool prizankSarzeNezapisovat = false)
        {
            bool zapis = false;
            // akce s pridanim materialu ...
            Fask.SQLiteDBs.DataSets.Vyroba zboziDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable = zboziDataSet.FASK_CONS_095;
            if (NajdiZbozi(materialBcode, zboziDatatable,true))
            {
                if(zboziDatatable.Count > 0)
                {
                   
                    foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row row in zboziDatatable)
                    {
                        if(prizankSarzeNezapisovat && row.CZ_SerNum_Track == 2)
                        {
                            //nezapisuje se automaticky odecet materialu
                        }
                        else
                        {

                            ZapisMaterial(row, materialBcode);
                        }
                    }
                }
            }
        }




        private void ZapisMaterial(Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row zboziRow, string kod)
        {

            bool zapis = true;
            #region navrh
            try
            {
                if (zapis)
                {
                    decimal mnozstvi = 0;
                    string sarzeHodnota = Settings.Production_Material_Hodnota_Sarze;

                    Fask.SQLiteDBs.DataSets.Vyroba zboziDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
                    Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable zboziDatatable = zboziDataSet.FASK_Vyroba_TP;

                    if (NajdiMaterial_TP(kod, out zboziDatatable))
                    {
                        foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in zboziDatatable.Rows)
                        {

                            row.Mn_vyrobku = mnozstviVyrobku;
                            row.Predpis_Mn = row.Mn_vyrobku * decimal.Parse(row.koef.Trim(), CultureInfo.InvariantCulture);// int.Parse(row.koef);
                            mnozstvi = row.Predpis_Mn;


                            //if(row.)

                        }
                    }






                    var nPS = this.ProductionSDT.NewProduction_SourcesRow();



                    // TODO : doplnit odpovidajici hodnoty ... 
                    if (!_productionRow.IsCountEntriesNull())
                        nPS.CountEntries = _productionRow.CountEntries;
                    if (!_productionRow.IsSOPNUMBENull())
                        nPS.SOPNUMBE = _productionRow.SOPNUMBE;
                    if (!zboziRow.IsITEMDESCNull())
                        nPS.ITEMNAME = zboziRow.ITEMDESC.Trim();
                    nPS.ITEMNMBR = zboziRow.ITEMNMBR.Trim();
                    nPS.ITEMTYPE = string.Empty;
                    if (!zboziRow.IsITEMCODENull())
                        nPS.ITEMCODE = zboziRow.ITEMCODE.Trim();
                    if (!zboziRow.IsLOCNCODENull())
                        nPS.LOCNCODE = zboziRow.LOCNCODE.Trim();
                    nPS.MJ = zboziRow.MJ.Trim();
                    nPS.GUID_Production = _productionRow.GUID;
                    nPS.GUID = Guid.NewGuid();
                    nPS.USER_ID = _productionRow.UserID;
                    nPS.TERMINAL_ID = _productionRow.TermID;
                    //if (!zboziRow.IsWEIGHTNull())
                    //    nPS.WEIGHT = zboziRow.WEIGHT;
                    nPS.NMBRPAL = string.Empty;
                    nPS.TYPEPAL = string.Empty;
                    nPS.PRINTED = 0;

                    nPS.QTYSHPPD = mnozstvi * (zboziRow.QTYPACK == 0 ? 1 : zboziRow.QTYPACK);
                    nPS.QTYSHPPDMJ = mnozstvi;
                    nPS.QTYPACK = zboziRow.QTYPACK;
                    // TODO : zadani serioveho cisla/sarze ???
                    // TODO : + kontrola ???
                    if (zboziRow.CZ_SerNum_Track == 2)
                        nPS.SERLTNUM = sarzeHodnota;
                    else
                        nPS.SERLTNUM = string.Empty;

                    // TODO : dialog vyberu skladu
                    if (Settings.Production_Material_Source_SKLID_Enter)
                    {
                        if (!zboziRow.IsSKL_IDNull() && !String.IsNullOrEmpty(zboziRow.SKL_ID.Trim()))
                        { // zadani skladu z vybrane polozky ...
                          // TODO overeni???
                            nPS.SKL_ID = zboziRow.SKL_ID;
                        }
                        else
                        { //zadani skladu rucne

                            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
                            //taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                            //Zadani ciloveho skladu a cilove lokace ...
                            using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                            {
                                fik.Text = "Zadejte zdrojový sklad";
                                if (!nPS.IsSKL_IDNull()
                                    && !string.IsNullOrEmpty(nPS.SKL_ID))
                                {
                                    var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(nPS.SKL_ID);
                                    if (listSklady.Count() > 0)
                                        fik.Kod = listSklady.First().skl_carcode;
                                }
                                else
                                    fik.Kod = Settings.Production_Material_Source_SKLID;

                                while (true)
                                {
                                    fik.Kod = fik.Kod;
                                    if (DialogResult.Cancel == fik.ShowDialog())
                                        return;
                                    // Test na existenci id cil. skladu ...
                                    var listSklady = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                                    if (listSklady.Count() == 0)
                                    {
                                        if (DialogResult.Cancel == FlexibleMessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                            return;
                                    }
                                    else
                                    {
                                        fik.Kod = listSklady.First().skl_id.Trim();
                                        break;
                                    }
                                }
                                nPS.SKL_ID = fik.Kod;
                            }
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                            nPS.SKL_ID = Settings.Production_Material_Source_SKLID;
                    }

                    // TODO : dialog vyberu lokace dle skladu
                    if (Settings.Production_Material_Source_LOCNCODE_Enter)
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
                        //taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                        //zadani cilove lokace
                        using (Vyroba_P.Forms.FormInputKod fik = new Vyroba_P.Forms.FormInputKod())
                        {
                            fik.Text = "Zadejte zdroj. lokaci"; // "Zadejte zdrojovou lokaci"
                            if (!nPS.IsSKL_IDNull()
                                && !nPS.IsLOCNCODENull()
                                && !string.IsNullOrEmpty(nPS.SKL_ID)
                                && !string.IsNullOrEmpty(nPS.LOCNCODE)
                                )
                            {
                                var listLokace = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(nPS.SKL_ID, nPS.LOCNCODE);
                                if (listLokace.Count() > 0)
                                    fik.Kod = listLokace.First().Barcode;
                            }
                            else
                                fik.Kod = Settings.Production_Material_Source_LOCNCODE;

                            while (true)
                            {
                                fik.Kod = fik.Kod;
                                if (DialogResult.Cancel == fik.ShowDialog())
                                    return;

                                // Test na existenci id cil. lokace ...
                                var listLokace = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(nPS.SKL_ID, fik.Kod);
                                if (listLokace.Count() == 0)
                                {
                                    if (DialogResult.Cancel == FlexibleMessageBox.Show("Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                        return;
                                }
                                else
                                {
                                    fik.Kod = listLokace.First().LOCNCODE.Trim();
                                    break;
                                }
                            }

                            nPS.LOCNCODE = fik.Kod;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_LOCNCODE))
                            nPS.LOCNCODE = Settings.Production_Material_Source_LOCNCODE;
                    }


                    //this.PSDataTable.AddProduction_SourcesRow(nPS);
                    //this._productionSDT.AddProduction_SourcesRow(nPS);
                    this.ProductionSDT.AddProduction_SourcesRow(nPS);


                    //this.MaterialSelected = nPS;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            #endregion
        }

        private string connectionString; // Místo tohoto vložte skutečný připojovací řetězec k vaší databázi

        public (int, string) CheckSerial(string serialId, string ITEMNMBR_fol = null)
        {
            try
            {
                connectionString = Settings.Connection_DB;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Chyba připojení do databáze, nezadáno správné připojení!");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("checkSerial", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Přidání parametrů k proceduře
                        command.Parameters.AddWithValue("@serialId", serialId);
                        command.Parameters.AddWithValue("@matid", ITEMNMBR_fol);

                        // Vytvoření parametru pro návratovou hodnotu (int)
                        SqlParameter returnParameter = command.Parameters.Add("@returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Otevření spojení a provedení procedury
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Zpracování výsledků procedury
                        // int returnValue = (int)returnParameter.Value;

                        // Výsledky SELECT budou dostupné pomocí SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int isBlocked = reader.GetInt32(0); // předpokládám, že první sloupec je @isBlocked
                                    string message = reader.GetString(1); // předpokládám, že druhý sloupec je zpráva
                                    return (isBlocked, message);
                                }
                            }
                        }

                        // Pokud procedura nevrátila žádné výsledky, vrátíme pouze hodnotu procedury
                        return (-1, "Procedura ověření šarže nevrátila žádné výsledky");
                    }
                }

            }
            catch (Exception ex)
            {
                // Zde můžete zachytit a zpracovat chyby
                //Console.WriteLine("Chyba při provádění procedury: " + ex.Message);
                return (-1, "Chyba při provádění procedury ověření šarže" + ex.Message);
            }

        }
    }
}
