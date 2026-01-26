using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejVyberMaterialuList : Form
    {
        // nactena data z online metody
        public Fask.MST_W.ProdejService.Location.CZMST_SkladLokace_StavRow SelectedPaleta = null;

        private Fask.MST_W.ProdejService.Location ds;
        private BindingSource bs;
        // typ dokladu CZMST092.doc_id
        private string doc_id;

        public string Serltnum = string.Empty;
        public string Locncode = string.Empty;
        public decimal Qtyshppd = 0;

        private HledaniEnum typHledani = HledaniEnum.SERLTNUM;
        private HledaniEnum TypHledani
        {
            get
            {
                return typHledani;
            }
            set
            {
                switch (value)
                {
                    case HledaniEnum.LOCNCODE:
                        statusBar1.Text = "H:L";
                        break;
                    case HledaniEnum.SERLTNUM:
                    default:
                        statusBar1.Text = "H:S";
                        break;
                }
                typHledani = value;
            }
        }

        public enum HledaniEnum
        {
            SERLTNUM,
            LOCNCODE
        }

        public ProdejVyberMaterialuList(Fask.MST_W.ProdejService.Location ds, string doc_id)
        {
            InitializeComponent(); 
            try
            {
                this.doc_id = doc_id;
                this.ds = ds;
                bs = new BindingSource();
                bs.DataSource = ds.CZMST_SkladLokace_Stav;
                dataGrid1.DataSource = bs;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ProdejVyberMaterialuList load");
            }
        }

        public Fask.MST_W.ProdejService.Location.CZMST_SkladLokace_StavRow _paletyRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as Fask.MST_W.ProdejService.Location.CZMST_SkladLokace_StavRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                TypHledani = Settings.ProdejVyberMaterialuListTypHledani;
                CreateGridStyles();

                InitializeGrid();

                ScannerStart();
                panelButtons_Resize(null, null);

                dataGrid1.Focus();
                try
                {
                    dataGrid1.CurrentRowIndex = 0;
                }
                catch
                {
                }
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
            ts.MappingName = ds.CZMST_SkladLokace_Stav.TableName; // ds_servis.CZMST_Servis_Dynamic_Table.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = ds.CZMST_SkladLokace_Stav.IndexColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.IndexColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Název"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.ITEMDESCColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Položka č.";//ds.Palety.ITEMNMBRColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.ITEMNMBRColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "SN"; //ds.Palety.SERLTNUMColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.SERLTNUMColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Množství"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.QTYSHPPDColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Lokace"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.LOCNCODEColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Sklad ID"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.SKL_IDColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Expirace"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.CZMST_SkladLokace_Stav.EXPIRATIONColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
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
                Settings.ProdejVyberMaterialuListTypHledani = TypHledani;
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
                bs.Filter = string.Empty;
                string selectcmd = string.Empty;
                switch (typHledani)
                {                    
                    case HledaniEnum.LOCNCODE:
                        selectcmd = "LOCNCODE = '" + kod + "'";
                        break;
                    case HledaniEnum.SERLTNUM:
                    default:
                        selectcmd = "SERLTNUM = '" + kod + "'";
                        break;
                }

                //selectcmd = "SERLTNUM = '" + kod + "'";
                var tables = ds.CZMST_SkladLokace_Stav.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    //DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListZaznamNenalezenPokracovatDotaz, kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                    //if (dr == DialogResult.No)
                    //    return;

                    // zaznam nebyl nalezen, zobrazit dotaz, zdali pouzit vybranou lokaci/sarzi
                    switch (typHledani)
                    {
                        case HledaniEnum.LOCNCODE:
                            if (MessageBoxBig.Show(string.Format("Nenalezen záznam na lokaci '{0}'.\nZvolit načtenou lokaci a pokračovat?", kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
                                return;

                            Locncode = kod;
                            break;
                        case HledaniEnum.SERLTNUM:
                        default:
                            if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListZaznamNenalezenPokracovatDotaz, kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
                                return;

                            Serltnum = kod;
                            break;
                    }
                    
                    finalize();
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (tables.Count() > 1)
                {
                    //bs.Filter = "SERLTNUM='" + kod + "'";
                    switch (typHledani)
                    {
                        case HledaniEnum.LOCNCODE:
                            bs.Filter = "LOCNCODE='" + kod + "'";
                            MessageBoxBig.Show(string.Format("Nalezeno více záznamů na lokaci '{0}'", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                            break;
                        case HledaniEnum.SERLTNUM:
                        default:
                            bs.Filter = "SERLTNUM='" + kod + "'";
                            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListNalezenoViceZaznamu, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                            break;
                    }

                    //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListNalezenoViceZaznamu, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    //bs.Filter = "SERLTNUM='" + kod + "'";
                    switch (typHledani)
                    {
                        case HledaniEnum.LOCNCODE:
                            bs.Filter = "LOCNCODE='" + kod + "'";
                            break;
                        case HledaniEnum.SERLTNUM:
                        default:
                            bs.Filter = "SERLTNUM='" + kod + "'";
                            break;
                    }
                }

                // overovat lokaci
                //if (Prodej.Globals.OverovatLokaci)
                //{
                //    if (!onlineOverLokaci(_paletyRow.ITEMNMBR, _paletyRow.SERLTNUM, _paletyRow.LOCNCODE))
                //        return;
                //}

                SelectedPaleta = _paletyRow;

                Locncode = _paletyRow.IsLOCNCODENull() ? string.Empty : _paletyRow.LOCNCODE;
                Serltnum = _paletyRow.SERLTNUM;
                Qtyshppd = _paletyRow.QTYSHPPD;

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
                if (_paletyRow == null)
                {
                    //MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberPaletyListNeniVybranaPaleta, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                SelectedPaleta = _paletyRow;

                Locncode = _paletyRow.IsLOCNCODENull() ? string.Empty : _paletyRow.LOCNCODE;
                Serltnum = _paletyRow.SERLTNUM;
                Qtyshppd = _paletyRow.QTYSHPPD;

                finalize();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void miDynamickaTabulka_Click(object sender, EventArgs e)
        {
        }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {

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
                Logging.Log.Write(ex, "ProdejVyberMaterialuList - prepnuti vyhledavani");
            }
        }

        //private Fask.MST_W.ProdejService.StatusOverLokace OnlineOverLokace(string itemnmbr, string serltnum, string locncode)
        //{
        //    Fask.MST_W.ProdejService.StatusOverLokace so;
        //    try
        //    {
        //        _WebRefernces_Globals.ProdejServiceSession prodejService = new Fask.MST_W._WebRefernces_Globals.ProdejServiceSession();
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