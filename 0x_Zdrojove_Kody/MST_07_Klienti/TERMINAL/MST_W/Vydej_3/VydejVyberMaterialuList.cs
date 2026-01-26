using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Vydej_3
{
    public partial class VydejVyberMaterialuList : Form
    {
        // nactena data z online metody
        public Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow SelectedItem = null;

        private Fask.MST_W.VydejService.Vydej_Items_Online ds;
        private BindingSource bs;

        public string Serltnum = string.Empty;
        public string Locncode = string.Empty;
		public string ITEMNMBR = string.Empty;
        public decimal Qty = 0;

        private HledaniEnum typHledani = HledaniEnum.SERLTNUM;
        private HledaniEnum TypHledani
        {
            get
            {
                return typHledani;
            }
            set
            {
                typHledani = value;
                update_statusbar();
            }
        }

        private void update_statusbar()
        {
            StringBuilder sb = new StringBuilder();

            // Typ hledani
            sb.Append("H:");
            switch (typHledani)
            {
                case HledaniEnum.LOCNCODE:
                    sb.Append("L");
                    break;
                case HledaniEnum.SERLTNUM:
                default:
                    sb.Append("S");
                    break;
            }

            // Filtr quantity
            sb.Append(" Q:");
            sb.Append(miFiltrMnozstviVetsiNula.Checked ? ">" : "*");

            // celkove informace
            sb.Append(" C:");
            sb.Append(this.bs.Count);
            sb.Append("/");
            sb.Append(this.ds.Items.Count);

            statusBar1.Text = sb.ToString();
        }

        public enum HledaniEnum
        {
            SERLTNUM,
            LOCNCODE
		}

        public VydejVyberMaterialuList(Fask.MST_W.VydejService.Vydej_Items_Online ds)
        {
            InitializeComponent(); 
            try
            {
                // todo : odstranit / vyloucit sarze jiz nactene                
                var sita = Vydej.vydejInstance.globalObject.controller_vydej.SI_GetData();
                List<Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow> mo_to_delete = new List<Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow>();
                var materialy_lokal = sita.GroupBy(x => new { x.ITEMNMBR, x.SERLTNUM });
                foreach (Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow mo in ds.Items)
                {
                    if (materialy_lokal.Any(x => 
                        x.Key.ITEMNMBR.Trim() == mo.Itemnmbr.Trim() 
                        && x.Key.SERLTNUM.Trim() == mo.Serltnum.Trim() 
                        && x.Sum( y => y.QTYSHPPD) >= mo.Qty
                        ))
                        mo_to_delete.Add(mo);
                }
                foreach (Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow mo in mo_to_delete)
                {
                    mo.Delete();
                }

                this.ds = ds;
                bs = new BindingSource();
                bs.DataSource = ds.Items;
                dataGrid1.DataSource = bs;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "VydejVyberMaterialuList load");
            }
        }

        public Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow _itemRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void VydejVyberMaterialuList_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                TypHledani = Settings.VydejVyberMaterialuListTypHledani;
                filtrMnozstviVetsiNula_Set(Settings.VydejVyberMaterialuFiltrMnozstviNula);

                CreateGridStyles();

                InitializeGrid();

                panelButtons_Resize(null, null);

                dataGrid1.Focus();
                try
                {
                    dataGrid1.CurrentRowIndex = 0;
                }
                catch
                {
                }
               
                ScannerStart();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = ds.Items.TableName; // ds_servis.CZMST_Servis_Dynamic_Table.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = ds.Items.IndexColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.IndexColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Název"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.ItemdescColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Položka č.";//ds.Palety.ITEMNMBRColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.ItemnmbrColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "SN"; //ds.Palety.SERLTNUMColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.SerltnumColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            //dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            var dgN = new Fask.Graphic.DataGrid2NumberBoxColumn();
            dgN.HeaderText = "Množství"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dgN.MappingName = ds.Items.QtyColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dgN.NullText = "-";
            //dg.SelectionShow = true;
            dgN.Width = 50;
            ts.GridColumnStyles.Add(dgN);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Lokace"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.LocncodeColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Sklad ID"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.Skl_IdColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Expirace"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.ExpirationColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            dg.Format = Main.dateFormatRRMMDD;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Příjem"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Items.PrijemColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            dg.Format = Main.datetimeFormatDMYYYYHmmssfff;
            ts.GridColumnStyles.Add(dg);

            //dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            //dg.HeaderText = "Řazení"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            //dg.MappingName = ds.CZMST_SkladLokace_Stav.RAZENIColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            //dg.NullText = "-";
            ////dg.SelectionShow = true;
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void VydejVyberMaterialuList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.D1)
            {
                miVyhledavaniPrepnuti_Click(null, null);
            }
            else if (e.KeyCode == Keys.D2)
            {
                filtrMnozstviVetsiNula_Change();
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            finalize(); 
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            try
            {
                ScannerFinalize();

                this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
                Settings.VydejVyberMaterialuListTypHledani = TypHledani;
                Settings.VydejVyberMaterialuFiltrMnozstviNula = filtrMnozstviVetsiNul_Get();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ProdejVyberMaterialuList - finalize");
            }
        }

        #region scanner
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }


        delegate void DelegateString(string kod);
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                if (kod.Length <= 0)
                    return;

                this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod"></param>
        private void najdipolozku(string kod)
        {
            try
            {
                update_bs_filters(kod);
                
                string selectcmd = this.bs.Filter ?? string.Empty;
                var tables = ds.Items.Select(selectcmd);

                if (tables.Count() == 0)
                {
                    // zaznam nebyl nalezen, zobrazit dotaz, zdali pouzit vybranou lokaci/sarzi
                    switch (typHledani)
                    {
                        case HledaniEnum.LOCNCODE:
                            if (MessageBoxBig.Show(string.Format("Nenalezen záznam na lokaci '{0}'.\nZvolit načtenou lokaci a pokračovat?", kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
                            {
                                update_bs_filters(null);
                                return;
                            }

                            Locncode = kod;
                            break;
                        case HledaniEnum.SERLTNUM:
                        default:
                            if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListZaznamNenalezenPokracovatDotaz, kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
                            {
                                update_bs_filters(null);
                                return;
                            }

                            Serltnum = kod;
                            break;
                    }
                    
                    finalize();
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (tables.Count() > 1)
                {
                    switch (typHledani)
                    {
                        case HledaniEnum.LOCNCODE:
                            MessageBoxBig.Show(string.Format("Nalezeno více záznamů na lokaci '{0}'", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                            break;
                        case HledaniEnum.SERLTNUM:
                        default:
                            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListNalezenoViceZaznamu, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                            break;
                    }

                    //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListNalezenoViceZaznamu, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                // overovat lokaci
                //if (Prodej.Globals.OverovatLokaci)
                //{
                //    if (!onlineOverLokaci(_paletyRow.ITEMNMBR, _paletyRow.SERLTNUM, _paletyRow.LOCNCODE))
                //        return;
                //}

                SelectedItem = _itemRow;

                Locncode = _itemRow.Locncode;
                Serltnum = _itemRow.Serltnum;
                Qty = _itemRow.Qty;
				ITEMNMBR = _itemRow.Itemnmbr;

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Prodej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        //private bool onlineOverLokaci(string itemnmbr, string serltnum, string locncode)
        //{
        //    try
        //    {
        //        //Fask.MST_W.ProdejService.StatusOverLokace so = OnlineOverLokace(_paletyRow.ITEMNMBR, _paletyRow.SERLTNUM, _paletyRow.LOCNCODE);
        //        Fask.MST_W.ProdejService.StatusOverLokace so = OnlineOverLokace(itemnmbr, serltnum, locncode);
        //        if (so != null)
        //        {
        //            switch (so.State)
        //            {
        //                case 0: // vse v poradku, mozno pokracovat
        //                    break;
        //                case 1: // poruseno doporucene poradi, mozno pokracovat
        //                    DialogResult dr = MessageBoxBig.Show("Bylo porušeno doporučené pořadí materiálu. \nPřesto pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
        //                    if (dr == DialogResult.No)
        //                        return false;
        //                    Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", "poradi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prodej", null, "", _paletyRow.ITEMNMBR.Trim(), _paletyRow.SERLTNUM.Trim(), ""));
        //                    break;
        //                default: // neni mozne pokracovat
        //                    MessageBoxBig.Show("Není možné uložit na tuto lokaci.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //                    return false;
        //            }
        //        }
        //        else  // nic se nenacetlo
        //        {
        //            // chyba komunikace se serverem
        //            DialogResult dr = MessageBoxBig.Show("Nepodařilo se online ověřit šarži materiálu. \nPřesto pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
        //            if (dr == DialogResult.No)
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //        return false;
        //    }
        //    return true;
        //}

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (_itemRow == null)
                {
                    //MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListNeniVybranaPaleta, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                SelectedItem = _itemRow;

                Locncode = _itemRow.Locncode;
                Serltnum = _itemRow.Serltnum;
                Qty = _itemRow.Qty;
				ITEMNMBR = _itemRow.Itemnmbr;

                finalize();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private void miVyhledavaniPrepnuti_Click(object sender, EventArgs e)
        {
            try
            {
                // prepnuti modu vyhledavani scannerem
                switch (typHledani)
                {
                    case HledaniEnum.SERLTNUM:
                        TypHledani = HledaniEnum.LOCNCODE;
                        break;
                    case HledaniEnum.LOCNCODE:
                        TypHledani = HledaniEnum.SERLTNUM;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "VydejVyberMaterialuList - prepnuti vyhledavani");
            }
        }

        private void miFiltrMnozstviVetsiNula_Click(object sender, EventArgs e)
        {
            filtrMnozstviVetsiNula_Change();
        }

        private void filtrMnozstviVetsiNula_Change()
        {
            filtrMnozstviVetsiNula_Set(!miFiltrMnozstviVetsiNula.Checked);
        }

        private void filtrMnozstviVetsiNula_Set(bool value)
        {
            miFiltrMnozstviVetsiNula.Checked = value;
            
            update_bs_filters(null);

            this.update_statusbar();
        }

        private bool filtrMnozstviVetsiNul_Get()
        {
            return miFiltrMnozstviVetsiNula.Checked;
        }

        /// <summary>
        /// aktualizuje filtry zobrazeni polozek
        /// </summary>
        /// <param name="kod"></param>
        private void update_bs_filters(string kod)
        {
            string strFilterQty = string.Empty;

            if (miFiltrMnozstviVetsiNula.Checked)
                strFilterQty = "QTY > 0";

            string strFilterSearch = string.Empty;
            if (!string.IsNullOrEmpty(kod))
            {
                switch (typHledani)
                {
                    case HledaniEnum.LOCNCODE:
                        strFilterSearch = "LOCNCODE='" + kod + "'";
                        break;
                    case HledaniEnum.SERLTNUM:
                    default:
                        strFilterSearch = "SERLTNUM='" + kod + "'";
                        break;
                }
            }

            string strFilter = strFilterQty;
            strFilter += (strFilter.Length > 0 && strFilterSearch.Length > 0 ? " AND " : string.Empty);
            strFilter += strFilterSearch;

            this.bs.Filter = strFilter;

            update_statusbar();
        }

        //private Fask.MST_W.ProdejService.StatusOverLokace OnlineOverLokace(string itemnmbr, string serltnum, string locncode)
        //{
        //    Fask.MST_W.ProdejService.StatusOverLokace so;
        //    try
        //    {
        //        ProdejService.ProdejService prodejService = new Fask.MST_W.ProdejService.ProdejService();
        //        prodejService.Url = MST_Global.ServerAddress + "Prodej.asmx";
        //        prodejService.Timeout = MST_Global.ServiceTimeOut;
        //        prodejService.UpdateWebServiceCredentials();

        //        so = prodejService.Online_OverLokace(itemnmbr, serltnum, locncode, doc_id);
        //        //if (so.State != 0)  // nastala chyba
        //        //{
        //        //    throw new Exception(so.Error);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex.Message, "Prijem.ProdejList, OnlineGenerateSerltnum");
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //        return null;
        //    }

        //    return so;
        //}
    }
}