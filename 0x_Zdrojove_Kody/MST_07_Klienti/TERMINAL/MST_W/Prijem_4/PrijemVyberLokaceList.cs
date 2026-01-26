using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemVyberLokaceList : Form
    {
        // nactena data z online metody
        private Fask.MST_W.PrijemService.Obecne ds;
        private BindingSource bs;
        private string Serltnum;    // sarze
        private string Itemnmbr;    // cislo polozky
        private decimal qtyshppd;
        public string ResLocncode;  // zadana lokace
        private bool overovatLokaci = false;    // online overeni lokace
        private string skl_id;      // id skladu

        public PrijemVyberLokaceList(Fask.MST_W.PrijemService.Obecne ds, string Serltnum, string itemnmbr, bool overovatLokaci, string skl_id, decimal qtyshppd)
        {
            InitializeComponent(); 
            try
            {
                this.Serltnum = Serltnum;
                this.Itemnmbr = itemnmbr;
                this.overovatLokaci = overovatLokaci;
                this.ds = ds;
                this.skl_id = skl_id;
                this.qtyshppd = qtyshppd;
                bs = new BindingSource();
                bs.DataSource = ds.Lokace;
                dataGrid1.DataSource = bs;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrijemVyberLokaceList load");
            }
        }

        // vybrany radek v seznamu doporucenych lokaci
        private Fask.MST_W.PrijemService.Obecne.LokaceRow _lokaceRow
        {
            get
            {
                try
                {
                    //return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableRow;
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as Fask.MST_W.PrijemService.Obecne.LokaceRow;
                    //return null;

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    return null;
                }
            }
        }

        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

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

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = ds.Palety.TableName; // ds_servis.CZMST_Servis_Dynamic_Table.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = ds.Palety.IndexColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Palety.IndexColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Lokace"; //ds.Palety.QTYSHPPDColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds.Palety.LOCNCODEColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
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
            ScannerFinalize();

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
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
                string selectcmd = "LOCNCODE = '" + kod + "'";
                var tables = ds.Palety.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    // 14.4.2016 PeV - nezobrazuje se dialog, protoze se pocita, ze uzivatel 
                    //MessageBoxBig.Show("Nenalezena lokace '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    // najit online
                    // overovat lokaci
                    if (overovatLokaci)
                    {
                        if (!onlineOverLokaci(kod))
                            return;
                    }
                    ResLocncode = kod;

                    finalize();
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNalezenoViceLokaci, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                }

                PerformOK();

                //finalize();
                //DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Prijem_4)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (_lokaceRow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNeniVybranaLokace, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                // overovat lokaci
                if(overovatLokaci)
                {
                    // online overeni lokace
                    if (!onlineOverLokaci(_lokaceRow.LOCNCODE))
                        return;

                    //Fask.MST_W.PrijemService.StatusOverLokace so = OnlineOverLokace(Itemnmbr, Serltnum, _lokaceRow.LOCNCODE);
                    //if (so != null)
                    //{
                    //    switch (so.State)
                    //    {
                    //        case 0: // vse v poradku, mozno pokracovat
                    //            break;
                    //        case 1: // poruseno doporucene poradi, mozno pokracovat
                    //            DialogResult dr = MessageBoxBig.Show("Bylo porušeno doporučené pořadí materiálu. \nPřesto pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                    //            if (dr == DialogResult.No)
                    //                return;
                    //            Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", "poradi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prijem", null, "", Itemnmbr.Trim(), _lokaceRow.LOCNCODE.Trim(), ""));
                    //            break;
                    //        default: // neni mozne pokracovat
                    //            MessageBoxBig.Show("Není možné uložit na tuto lokaci.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    //            return;
                    //    }
                    //}
                    //else
                    //{
                    //    // chyba komunikace se serverem
                    //    DialogResult dr = MessageBoxBig.Show("Nepodařilo se ověřit umístění materiálu. \nPřesto pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                    //    if (dr == DialogResult.No)
                    //        return;
                    //}
                }

                ResLocncode = _lokaceRow.LOCNCODE;

                finalize();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private bool onlineOverLokaci(string locncode)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Fask.MST_W.PrijemService.StatusOverLokace so = OnlineOverLokace(Itemnmbr, Serltnum, locncode, qtyshppd);
                Cursor.Current = Cursors.Default;
                if (so != null)
                {
                    switch (so.State)
                    {
                        case Fask.MST_W.PrijemService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
                            break;
                        case Fask.MST_W.PrijemService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
                            DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListPorusenoDoporucPoradiPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                            if (dr == DialogResult.No)
                                return false;
                            Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", "poradi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prijem", null, "", Itemnmbr.Trim(), _lokaceRow.LOCNCODE.Trim(), ""));
                            break;
                        case Fask.MST_W.PrijemService.STATUSOverLokace.ERROR:
                            MessageBoxBig.Show(so.Message.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            return false;
                        default: // neni mozne pokracovat
                            MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNejdeUlozitNaLokaci, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            return false;
                    }
                }
                else
                {
                    // chyba komunikace se serverem
                    DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListChybaKomunikacePokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                    if (dr == DialogResult.No)
                        return false;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }

            return true;
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

        private Fask.MST_W.PrijemService.StatusOverLokace OnlineOverLokace(string itemnmbr, string serltnum, string locncode, decimal qtyshppd)
        {
            Fask.MST_W.PrijemService.StatusOverLokace so;
            try
            {
                so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_OverLokace(serltnum, itemnmbr, locncode, qtyshppd, skl_id);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemVyberLokaceList, OnlineOverLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
            return so;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            PerformVyberLokace();
        }

        private void PerformVyberLokace()
        {
            string locncode = string.Empty;
            try
            {
                ScannerStop();

                using (SejmiKodForm skf = new SejmiKodForm())
                {
                    skf.Popis = MST_Global.LC_NAME;
                    skf.Text = Fask.Localization.Localization.Prijem4PrijemZalokovaniListZalokovaniLokace;
                    skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                    //skf.MaxLength = (int)SqlCEDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                    //skf.Len = Globals.LOCNCODE_LEN;
                    //skf.CheckLen = true;
                    skf.AllowEmpty = false;
                    skf.Kod = string.Empty;

                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    locncode = skf.Kod;
                }

                // overovat lokaci
                if (overovatLokaci)
                {
                    // online overeni lokace
                    if (!onlineOverLokaci(locncode))
                        return;
                }

                ResLocncode = locncode;

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemVyberLokaceList, OnlineOverLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }
    }
}