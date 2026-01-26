using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using Fask.Graphic;
using Fask.MST_W.Prodej_3;
using Fask.MST_W.Classes;
using System.Data.SQLite;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemZalokovaniList : Form
    {
        //private Fask.MST_W._WebRefernces_Globals.LokaceServiceSession wsLokace = null;
        private Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry;

        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter _pi_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter _pe_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
        //private System.Data.SQLite.SQLiteConnection _davkasqlceconnection = null;
        private Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow prijmovalokace;
        /// <summary>
        /// Locncode, ktere se autoamticky pouzije
        /// </summary>
        private string locncodeNaDavku = string.Empty;
        
        //private string _davka = string.Empty;
        private bool filtrZobrazitVse = true;

        public PrijemZalokovaniList(
            //string davka, 
            Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry, 
            Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow prijmovalokace
            )
        {
            InitializeComponent(); 
            try
            {
                //this._davka = davka;
                this.prijemDataParametry = prijemDataParametry;
                this.prijmovalokace = prijmovalokace;

                //_davkasqlceconnection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(Main.StorageDir, _davka + "." + Main.Ext_Prijem));
                //_pi_ta.Connection = _davkasqlceconnection;
                //_pi_ta.Fill(prijem.CZMST_PI);
                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Fill_PI(prijem.CZMST_PI);

                //_pe_ta.Connection = _davkasqlceconnection;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ProdejVyberPaletyList load");
            }
        }

        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow _SelectedPI
        {
            get
            {
                try
                {
                    return (bs_prijem.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow;
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

                Cursor.Current = Cursors.WaitCursor;
                InitializeDataGridView();

                miNastavitLokaci.Enabled = Prijem_4.Globals.LokaceNaDavkuPovolit;
                if (!Prijem_4.Globals.LokaceNaDavkuPovolit && menuItem1.MenuItems.Contains(this.miNastavitLokaci))
                    menuItem1.MenuItems.Remove(this.miNastavitLokaci);

                // nacteni z konfigurace posledni aktivni filtr
                filtrZobrazitVse = Settings.PrijemZalokovaniListFiltrVse;

                InitializeGrid();

                ScannerStart();
                panelButtons_Resize(null, null);
                SetFilter();

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
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle tsN = new DataGridTableStyle();
            tsN.MappingName = prijem.CZMST_PI.TableName;

            DataGrid2TextBoxColumn nazevN = new DataGrid2TextBoxColumn();
            nazevN.HeaderText = "Objednávka";
            nazevN.MappingName = prijem.CZMST_PI.PONUMBERColumn.ColumnName;
            nazevN.NullText = "-";
            nazevN.Width = 50;
            tsN.GridColumnStyles.Add(nazevN);

            DataGrid2TextBoxColumn polozkaC = new DataGrid2TextBoxColumn();
            polozkaC.HeaderText = "Položka Č.";
            polozkaC.MappingName = prijem.CZMST_PI.ITEMNMBRColumn.ColumnName;
            polozkaC.NullText = "-";
            polozkaC.Width = 50;
            tsN.GridColumnStyles.Add(polozkaC);

            DataGrid2TextBoxColumn dgN1 = new DataGrid2TextBoxColumn();
            dgN1.HeaderText = "Čár. kód";
            dgN1.MappingName = prijem.CZMST_PI.CZ_CarKodColumn.ColumnName;
            dgN1.NullText = "-";
            dgN1.Width = 50;
            tsN.GridColumnStyles.Add(dgN1);

            DataGrid2TextBoxColumn dgN1a = new DataGrid2TextBoxColumn();
            dgN1a.HeaderText = "SN";
            dgN1a.MappingName = prijem.CZMST_PI.SERLTNUMColumn.ColumnName;
            dgN1a.NullText = "-";
            dgN1a.Width = 50;
            tsN.GridColumnStyles.Add(dgN1a);

            DataGrid2TextBoxColumn dgN2 = new DataGrid2TextBoxColumn();
            dgN2.HeaderText = "Množství";
            dgN2.MappingName = prijem.CZMST_PI.QTYSHPPDColumn.ColumnName;
            dgN2.NullText = "-";
            dgN2.Width = 50;
            tsN.GridColumnStyles.Add(dgN2);

            DataGrid2NumberBoxColumn dgN3 = new DataGrid2NumberBoxColumn();
            dgN3.HeaderText = "Balení";
            dgN3.MappingName = prijem.CZMST_PI.QTYPACKColumn.ColumnName;
            dgN3.NullText = "-";
            dgN3.Width = 50;
            dgN3.Alignment = StringAlignment.Far;
            tsN.GridColumnStyles.Add(dgN3);

            DataGrid2NumberBoxColumn dgN4 = new DataGrid2NumberBoxColumn();
            dgN4.HeaderText = "Lokace";
            dgN4.MappingName = prijem.CZMST_PI.LOCNCODEColumn.ColumnName;
            dgN4.NullText = "-";
            dgN4.Width = 50;
            dgN4.Alignment = StringAlignment.Far;
            tsN.GridColumnStyles.Add(dgN4);

            DataGrid2NumberBoxColumn dgN5 = new DataGrid2NumberBoxColumn();
            dgN5.HeaderText = "Sklad ID";
            dgN5.MappingName = prijem.CZMST_PI.SKL_IDColumn.ColumnName;
            dgN5.NullText = "-";
            dgN5.Width = 50;
            dgN5.Alignment = StringAlignment.Far;
            tsN.GridColumnStyles.Add(dgN5);

            dataGrid1.TableStyles.Add(tsN);
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
                miZobrazitVse_Click(null, null);
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (Prijem_4.Globals.LokaceNaDavkuPovolit)
                    miNastavitLokaci_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            try
            {
                if (Globals.PrijemDialogOpusteniZalokovani)
                {
                    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemZalokovaniListKonecDotaz, Fask.Localization.Localization.Prijem4PrijemZalokovaniListZalokovani, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                            == DialogResult.No)
                    {
                        dataGrid1.Focus();
                        return;
                    }
                }

                finalize();
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void finalize()
        {
            ScannerFinalize();
            Settings.PrijemZalokovaniListFiltrVse = filtrZobrazitVse;
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
                ScannerStop();
                SetFilter();

                bool filterbySerltnum = true;   // filtrovat podle sarze
                Fask.SQLiteDBs.DataSets.Prijem ds_prijem = new Fask.SQLiteDBs.DataSets.Prijem();

                // zobrazit vse
                if (filtrZobrazitVse)
                {
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillBySerltnum_PI(ds_prijem.CZMST_PI, kod);

                    // polozka nenalezena podle serioveho cisla, hleda se podle caroveho kodu
                    if (ds_prijem.CZMST_PI.Count == 0)
                    {
                        Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByBarcode_PI(ds_prijem.CZMST_PI, kod);
                        filterbySerltnum = false;
                    }
                }
                else
                {
                    // zobrazit nezalokovane (locncode == string.empty nebo prijmovalokace.locncode)
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillBySerltnumLocncode_PI(ds_prijem.CZMST_PI, kod, (prijmovalokace == null || prijmovalokace.IsLOCNCODENull()) ? string.Empty : prijmovalokace.LOCNCODE);

                    if (ds_prijem.CZMST_PI.Count == 0)
                    {
                        Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByBarcodeLocncode_PI(ds_prijem.CZMST_PI, kod, (prijmovalokace == null || prijmovalokace.IsLOCNCODENull()) ? string.Empty : prijmovalokace.LOCNCODE);
                        filterbySerltnum = false;
                    }
                }

                bs_prijem.Filter = string.Empty;

                if (ds_prijem.CZMST_PI.Count == 0)
                {
                    // vyhledat podle caroveho kodu!!
                    if (filterbySerltnum)
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemZalokovaniListNenalezenZaznamSSarzi, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    else
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemZalokovaniListNenalezenZaznamSCarKod, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    SetFilter();
                    return;
                }
                else if (ds_prijem.CZMST_PI.Count > 1)
                {
                    
                    bs_prijem.Filter = filterbySerltnum ? ("SERLTNUM='" + kod + "'") : ("(CZ_CarKod='" + kod + "' OR VNDITNUM='" + kod + "')");
                    if (!filtrZobrazitVse)
                    {
                        if (prijmovalokace != null && !prijmovalokace.IsLOCNCODENull())
                            bs_prijem.Filter += " AND (LOCNCODE='' OR LOCNCODE='" + prijmovalokace.LOCNCODE.Trim() + "')";
                        else
                            bs_prijem.Filter += " AND LOCNCODE=''";

                        sbInfo.Text = "Z:N,F:" + kod;                        
                    }
                    else
                    {
                        sbInfo.Text = "Z:V,F:" + kod;
                    }
                    
                    if (filterbySerltnum)
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemZalokovaniListNalezenoViceZaznamuSSarzi, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    else
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemZalokovaniListNalezenoViceZaznamuSCarKod, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs_prijem.Filter = filterbySerltnum ? ("SERLTNUM='" + kod + "'") : ("(CZ_CarKod='" + kod + "' OR VNDITNUM='" + kod + "')");
                    if (!filtrZobrazitVse)
                    {
                        if (prijmovalokace != null && !prijmovalokace.IsLOCNCODENull())
                            bs_prijem.Filter += " AND (LOCNCODE='' OR LOCNCODE='" + prijmovalokace.LOCNCODE.Trim() + "')";
                        else
                            bs_prijem.Filter += " AND LOCNCODE=''";
                    }

                    PerformOK();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
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
            //SQLiteTransaction trans = null;
            //SQLiteConnection conn = null;

            try
            {
                ScannerStop();

                if (_SelectedPI == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemZalokovaniListNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                // dotahnuti informace o polozce
                var pedt = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByKey_PE(_SelectedPI.PONUMBER, _SelectedPI.IsITEMNMBRNull() ? string.Empty : _SelectedPI.ITEMNMBR, _SelectedPI.ORD);
                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow perow = null;

                if (pedt != null && pedt.Count > 0)
                    perow = pedt.First();

                decimal qty = 0;
                if (Globals.ZalokovaniPozadovatZadaniMnozstvi) //sledovano na mnozstvi
                {
                    using (PrijemPridatPolozku ppp = new PrijemPridatPolozku(perow))
                    {
                        ppp.Text = Fask.Localization.Localization.Prijem4PrijemZalokovaniListZalokovaniMnozstvi;
                        ppp.Popis = _SelectedPI.QTYPACK > 0 ? Fask.Localization.Localization.Prijem4PrijemListMnozstviBaleni : Fask.Localization.Localization.Prijem4PrijemListMnozstvi; //"Množství" + (PERow.QTYPACK > 0 ? " balení" : string.Empty);
                        ppp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                        ppp.Serltnum = string.Empty;
                        ppp.Len = 0;
                        ppp.CheckLen = false;
                        ppp.AllowEmpty = false;
                        ppp.Kod = _SelectedPI.QTYSHPPDMJ.ToString(Settings.UIFormatDesCisel);     // string.Empty;     // TODO: predvyplnit mnozstvi??
                        ppp.ScannerOff = !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

                        if (ppp.ShowDialog() == DialogResult.Cancel)
                            return;

                        qty = decimal.Parse(ppp.Kod);
                    }

                    // mnozstvi nesmi byt vetsi nez puvodni zadane
                    if (qty > _SelectedPI.QTYSHPPDMJ)
                    {
                        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemZalokovaniListPolozkuNejdePreplnit, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                }

                string skl_id = string.Empty;
                string locncode = string.Empty;
                bool onlineKontrola = true;     // urcuje, zdali se ma provest online overeni lokace (protoze kontrola probiha take v PrijemVyberLokaceList)
                Fask.MST_W.PrijemService.Obecne ds = null;
                
                // lokace na davku, automaticky se pouzije ...
                if(miNastavitLokaci.Checked && !string.IsNullOrEmpty(locncodeNaDavku))
                {
                    // TODO: prejmenovat tlacitka ... (implementace do stare verze??)
                    locncode = locncodeNaDavku;
                }
                else if (Prijem_4.Globals.DoporuceneLokace)  // je zapnuto vyplnovani doporucenych lokaci
                {
                    ds = OnlineGetDoporuceneLokace(_SelectedPI.ITEMNMBR, _SelectedPI.SERLTNUM, _SelectedPI.IsSKL_IDNull() ? string.Empty : _SelectedPI.SKL_ID);
                    if (ds != null)
                    {
                        // vratily se nejake zaznamy
                        if (ds.Lokace.Count > 0)
                        {
                            using (PrijemVyberLokaceList pvpl = new PrijemVyberLokaceList(ds, _SelectedPI.SERLTNUM, _SelectedPI.ITEMNMBR, Prijem_4.Globals.OverovatLokaci, skl_id, _SelectedPI.QTYSHPPD))
                            {
                                if (pvpl.ShowDialog() == DialogResult.OK)
                                {
                                    //locncode = pvpl._lokaceRow.LOCNCODE;
                                    locncode = pvpl.ResLocncode;
                                    onlineKontrola = false;
                                }
                                else
                                    return;
                            }
                        }
                        else
                        {

                            // nebyla nalezena doporucena lokace, vybrani rucne
                            using (PrijemZadejLokaci skf = new PrijemZadejLokaci(perow))
                            {
                                skf.Popis = MST_Global.LC_NAME;
                                skf.Text = Fask.Localization.Localization.Prijem4PrijemZalokovaniListZalokovaniLokace;
                                skf.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
								skf.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                                //skf.Len = Globals.LOCNCODE_LEN;
                                //skf.CheckLen = true;
                                skf.AllowEmpty = false;
                                skf.Kod = _SelectedPI.LOCNCODE;

                                if (skf.ShowDialog() == DialogResult.Cancel)
                                    return;

                                locncode = skf.Kod;
                            }
                        }
                    }
                    else
                    {
                        // data se nepodarila nacist ze serveru
                        DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemZalokovaniListDoporucLokaceVyberRucne, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                        if (dr == DialogResult.No)
                            return;
                        else
                        {
                            // vybrani rucne
                            using (PrijemZadejLokaci skf = new PrijemZadejLokaci(perow))
                            {
                                skf.Popis = MST_Global.LC_NAME;
                                skf.Text = Fask.Localization.Localization.Prijem4PrijemZalokovaniListZalokovaniLokace;
                                skf.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
								skf.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                                //skf.Len = Globals.LOCNCODE_LEN;
                                //skf.CheckLen = true;
                                skf.AllowEmpty = false;
                                skf.Kod = _SelectedPI.LOCNCODE;

                                if (skf.ShowDialog() == DialogResult.Cancel)
                                    return;

                                locncode = skf.Kod;
                            }
                        }
                    }
                }
                else
                {
                    // vypnute doporucene lokace
                    // vybrani rucne
                    using (PrijemZadejLokaci skf = new PrijemZadejLokaci(perow))
                    {
                        skf.Popis = MST_Global.LC_NAME;
                        skf.Text = Fask.Localization.Localization.Prijem4PrijemZalokovaniListZalokovaniLokace;
                        skf.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
						skf.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                        //skf.Len = Globals.LOCNCODE_LEN;
                        //skf.CheckLen = true;
                        skf.AllowEmpty = false;
                        // 17.6.2016 PeV: neni potreba, dialog se nezobrazuje ...
                        //if (!string.IsNullOrEmpty(locncodeNaDavku))    // druhe a vice zadani lokace, pokud je povolena lokace na davku ... automaticky pouzit
                        //    skf.Kod = locncodeNaDavku;
                        //else   // jinak predvyplnit locncode z predlohy
                        skf.Kod = (perow == null || perow.IsLOCNCODENull()) ? string.Empty : perow.LOCNCODE.Trim();

                        if (skf.ShowDialog() == DialogResult.Cancel)
                            return;

                        locncode = skf.Kod;
                    }
                }

                // overovat lokaci
                if (Prijem_4.Globals.OverovatLokaci && onlineKontrola)
                {
                    Fask.MST_W.PrijemService.StatusOverLokace so = OnlineOverLokace(_SelectedPI.ITEMNMBR, _SelectedPI.SERLTNUM, locncode, _SelectedPI.IsSKL_IDNull() ? string.Empty : _SelectedPI.SKL_ID, _SelectedPI.QTYSHPPD);
                    if (so != null)
                    {
                        switch (so.State)
                        {
                            case Fask.MST_W.PrijemService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
                                break;
                            case Fask.MST_W.PrijemService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
                                DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemZalokovaniListDoporucPoradiPorusenoPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                                if (dr == DialogResult.No)
                                    return;
                                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", "poradi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prijem", null, "", _SelectedPI.ITEMNMBR.Trim(), locncode, ""));
                                break;
                            case Fask.MST_W.PrijemService.STATUSOverLokace.ERROR:
                                MessageBoxBig.Show(so.Message.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                return;
                            default: // neni mozne pokracovat
                                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemZalokovaniListNeniMozneUlozitNaLokaci, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                return;
                        }
                    }
                    else
                    {
                        // chyba komunikace se serverem
                        DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListChybaKomunikacePokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #region lokace, nepouziva se
                //// TODO: zmenit Guid po aktualizaci lokace?? -> mel by se ...
                //Guid newguid = Guid.NewGuid();
                //// online ulozeni do lokacniho mechanismu
                //if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
                //{
                //    //lokaceService
                //    Cursor.Current = Cursors.WaitCursor;
                //    Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
                //    pohybrow.ITEMNMBR = _SelectedPI.ITEMNMBR;
                //    pohybrow.DOCUMENT_NUMBER = _SelectedPI.PONUMBER;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS)
                //    pohybrow.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.D;  // prijem
                //    pohybrow.POHYB_SRC = "P";   // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
                //    pohybrow.SOURCE = "T";
                //    pohybrow.QTYSHPPD = (decimal)_SelectedPI.QTYSHPPD;
                //    pohybrow.SERLTNUM = _SelectedPI.SERLTNUM;
                //    pohybrow.SKL_ID_SRC = _SelectedPI.IsSKL_IDNull() ? string.Empty : _SelectedPI.SKL_ID;
                //    pohybrow.SKL_ID_DST = _SelectedPI.IsSKL_IDNull() ? string.Empty : _SelectedPI.SKL_ID;
                //    pohybrow.LOCNCODE_SRC = _SelectedPI.IsLOCNCODENull() ? string.Empty : _SelectedPI.LOCNCODE;
                //    pohybrow.LOCNCODE_DST = locncode;       // lokace, kam se prevadi ...
                //    pohybrow.UserID = MST_Global.UserID;
                //    pohybrow.TermID = MST_Global.TerminalID;
                //    pohybrow.guid = newguid;
                //    //pohybrow.dateeveS = ...   // datum serveru se vyplnuje az na serveru
                //    pohybrow.Expiration = null;
                //    pohybrow.ITEMDESC = string.Empty;   // TODO: dotáhnout název/rozšířit czmst_pi o itemdesc?
                //    pohybrow.CountEntries = _SelectedPI.CountEntries;
                //    pohybrow.dateeveT = DateTime.Now;   // datum terminalu

                //    try
                //    {
                //        Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".MoveItem - start", "LocationLog");
                //        Classes.LokaceLog.writeBody(pohybrow);

                //        Fask.MST_W.LokaceService.StatusLokace sl = wsLokace.MoveItem(pohybrow);
                //        Cursor.Current = Cursors.Default;
                //        switch (sl.State)
                //        {
                //            case Fask.MST_W.LokaceService.States.OK:
                //                break;
                //            case Fask.MST_W.LokaceService.States.ERROR:
                //                MessageBoxBig.Show("Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                //                return;
                //            default:
                //                MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                //                return;
                //        }

                //        Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".MoveItem - end", "LocationLog");
                //    }
                //    catch (Exception ex)
                //    {
                //        Logging.Log.WriteDebug(ex.Message);
                //        Cursor.Current = Cursors.Default;
                //        if (MessageBoxBig.Show(ex.Message + "\nPřejete si přesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) != DialogResult.Yes)
                //        {
                //            //Promenna ridici cyklus
                //            bool state = true;

                //            //Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
                //            while (state)
                //            {
                //                try
                //                {
                //                    // Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
                //                    // Volani sluzby pro odstraneni a kontrola navratveho stavu.
                //                    Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

                //                    Fask.MST_W.LokaceService.StatusLokace sl = wsLokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
                //                    DialogResult dr = DialogResult.No;
                //                    switch (sl.State)
                //                    {
                //                        case Fask.MST_W.LokaceService.States.OK:
                //                            dr = DialogResult.Yes;
                //                            break;
                //                        case Fask.MST_W.LokaceService.States.ERROR:
                //                            dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
                //                            return;
                //                        default:
                //                            dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
                //                            return;
                //                    }

                //                    // pokud ho chce ulozit, odejde z cyklu
                //                    if (dr == DialogResult.Yes)
                //                        break;
                //                }
                //                //Nejaka online chyba
                //                catch (Exception exex)
                //                {
                //                    Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
                //                    Cursor.Current = Cursors.Default;
                //                    if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
                //                    {
                //                        //Ukonceni cyklu - chce zaznam ulozit
                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion

                //conn = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Connection;
                
                //conn.Open();

				//trans = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Connection.BeginTransaction(IsolationLevel.Serializable);
                //_pi_ta.Transaction2 = trans;
                //Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Ta_pi.Transaction2 = trans; //?

                if (Globals.ZalokovaniPozadovatZadaniMnozstvi && (qty != _SelectedPI.QTYSHPPDMJ))
                {
                    // vytvorit novy zaznam s novym mnozstvim pokud je ruzne od puvodniho, jinak pouze zmenit lokaci ...
                    decimal mnozstvi = qty * (_SelectedPI.QTYPACK > 0 ? _SelectedPI.QTYPACK : 1);
                    DateTime dtnow = DateTime.Now;
                    Guid g = Guid.NewGuid();
                    
                    // vlozeni na novou lokaci
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Insert_PI(
                        _SelectedPI.CountEntries,
                        _SelectedPI.PONUMBER,
                        _SelectedPI.ORD,
                        _SelectedPI.IsITEMNMBRNull() ? string.Empty : _SelectedPI.ITEMNMBR,
                        _SelectedPI.IsVNDDOCNMNull() ? string.Empty : _SelectedPI.VNDDOCNM,
                        _SelectedPI.IsVNDITNUMNull() ? string.Empty : _SelectedPI.VNDITNUM,
                        locncode,       // _SelectedPI.IsLOCNCODENull() ? string.Empty : _SelectedPI.LOCNCODE,
                        mnozstvi,       // _SelectedPI.QTYSHPPD,
                        _SelectedPI.QTYPACK,
                        _SelectedPI.SERLTNUM,
                        _SelectedPI.IsKOD_SWNull() ? string.Empty : _SelectedPI.KOD_SW,
                        _SelectedPI.IsDAT_VYROBYNull() ? string.Empty : _SelectedPI.DAT_VYROBY,
                        dtnow.ToString("yyyyMMdd"),
                        dtnow.ToString("HHmmss"),
                        _SelectedPI.IsCZ_CarKodNull() ? string.Empty : _SelectedPI.CZ_CarKod,
                        _SelectedPI.IsREZ_1Null() ? string.Empty : _SelectedPI.REZ_1,
                        _SelectedPI.IsREZ_2Null() ? string.Empty : _SelectedPI.REZ_2,
                        MST_Global.UserID,
                        g,
                        _SelectedPI.INPUT_MODE,
                        MST_Global.TerminalID,
                        _SelectedPI.IsMJNull() ? string.Empty : _SelectedPI.MJ,
                        qty,
                        _SelectedPI.IsSKL_IDNull() ? string.Empty : _SelectedPI.SKL_ID,
                        _SelectedPI.IsWEIGHTNull() ? (decimal?)null : _SelectedPI.WEIGHT,
                        _SelectedPI.IsNMBRPALNull() ? string.Empty : _SelectedPI.NMBRPAL,
                        _SelectedPI.IsTYPEPALNull() ? string.Empty : _SelectedPI.TYPEPAL,
                        _SelectedPI.IsITEMCODENull() ? string.Empty : _SelectedPI.ITEMCODE,
                        _SelectedPI.IsDEX_ROW_IDNull() ? 0 : _SelectedPI.DEX_ROW_ID,
                        _SelectedPI.IsExpiraceNull() ? (DateTime?)null : _SelectedPI.Expirace,
						_SelectedPI.IsAttributeToSNNull() ? null : _SelectedPI.AttributeToSN 
                        );

                    Guid g2 = Guid.NewGuid();
                    // TODO: smazat zaznam podle GUID a nasledne ho opetovne pouzit?? (kvuli lokacnimu mechanismu -> prevody/defregmentace)
                    // zbytek na puvodni lokaci
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Insert_PI(
                        _SelectedPI.CountEntries,
                        _SelectedPI.PONUMBER,
                        _SelectedPI.ORD,
                        _SelectedPI.IsITEMNMBRNull() ? string.Empty : _SelectedPI.ITEMNMBR,
                        _SelectedPI.IsVNDDOCNMNull() ? string.Empty : _SelectedPI.VNDDOCNM,
                        _SelectedPI.IsVNDITNUMNull() ? string.Empty : _SelectedPI.VNDITNUM,
                        _SelectedPI.IsLOCNCODENull() ? string.Empty : _SelectedPI.LOCNCODE,
                        _SelectedPI.QTYSHPPD - mnozstvi,       // _SelectedPI.QTYSHPPD,
                        _SelectedPI.QTYPACK,
                        _SelectedPI.SERLTNUM,
                        _SelectedPI.IsKOD_SWNull() ? string.Empty : _SelectedPI.KOD_SW,
                        _SelectedPI.IsDAT_VYROBYNull() ? string.Empty : _SelectedPI.DAT_VYROBY,
                        dtnow.ToString("yyyyMMdd"),
                        dtnow.ToString("HHmmss"),
                        _SelectedPI.IsCZ_CarKodNull() ? string.Empty : _SelectedPI.CZ_CarKod,
                        _SelectedPI.IsREZ_1Null() ? string.Empty : _SelectedPI.REZ_1,
                        _SelectedPI.IsREZ_2Null() ? string.Empty : _SelectedPI.REZ_2,
                        MST_Global.UserID,
                        g2,
                        _SelectedPI.INPUT_MODE,
                        MST_Global.TerminalID,
                        _SelectedPI.IsMJNull() ? string.Empty : _SelectedPI.MJ,
                        _SelectedPI.QTYSHPPDMJ - qty,   // zadane mnozstvi 
                        _SelectedPI.IsSKL_IDNull() ? string.Empty : _SelectedPI.SKL_ID,
                        _SelectedPI.IsWEIGHTNull() ? (decimal?)null : _SelectedPI.WEIGHT,
                        _SelectedPI.IsNMBRPALNull() ? string.Empty : _SelectedPI.NMBRPAL,
                        _SelectedPI.IsTYPEPALNull() ? string.Empty : _SelectedPI.TYPEPAL,
                        _SelectedPI.IsITEMCODENull() ? string.Empty : _SelectedPI.ITEMCODE,
                        _SelectedPI.IsDEX_ROW_IDNull() ? 0 : _SelectedPI.DEX_ROW_ID,
                        _SelectedPI.IsExpiraceNull() ? (DateTime?)null : _SelectedPI.Expirace,
						_SelectedPI.IsAttributeToSNNull() ? null : _SelectedPI.AttributeToSN 
                        );

                    // odstraneni puvodniho zaznamu
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Delete_PI(_SelectedPI.guid);
                }
                else  // stara funkcionalita ... ANC, pripadne presun vseho
                {
                    // pouzit novy GUID pro presun?? (kvuli lokacnimu mechanismu)
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.UpdateLocncodeByGuid_PI(locncode, _SelectedPI.guid);
                }

				//if (trans != null)
				//    trans.Commit();

                prijem.CZMST_PI.Clear();
                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Fill_PI(prijem.CZMST_PI);
                prijem.CZMST_PI.AcceptChanges();
            }
            catch (Exception ex)
            {
				//try
				//{
				//    if (trans != null)
				//        trans.Rollback();
				//}
				//catch { }

                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
				//if ((conn != null) && ((conn.State & ConnectionState.Open) == System.Data.ConnectionState.Open))
				//    conn.Close();

                dataGrid1.Focus();

                SetFilter();
                ScannerStart();
            }
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

        private void miZobrazitVse_Click(object sender, EventArgs e)
        {
            filtrZobrazitVse = !filtrZobrazitVse;
            if (filtrZobrazitVse)
                ZobrazVse();
            else
                ZobrazNezalokovane();
        }

        private void ZobrazVse()
        {
            try
            {
                filtrZobrazitVse = true;
                SetFilter();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemZalokovaniList, miZobrazitVse");
            }
        }

        private void SetFilter()
        {
            try
            {
                if (filtrZobrazitVse)
                {
                    //sbInfo.Text = "Zobrazit: Vše";
                    sbInfo.Text = "Z:V";    // Fask.Localization.Localization.Prijem4PrijemZalokovaniListZobrazitVse;
                    bs_prijem.Filter = string.Empty;
                }
                else
                {
                    //sbInfo.Text = "Zobrazit: Nezalokované";
                    sbInfo.Text = "Z:N";    // Fask.Localization.Localization.Prijem4PrijemZalokovaniListZobrazitNezalokovane;
                    if(prijmovalokace != null)
                        bs_prijem.Filter = "Locncode='' Or Locncode='" + prijmovalokace.LOCNCODE + "'";
                    else
                        bs_prijem.Filter = "Locncode=''";
                }

                if (miNastavitLokaci.Checked && !string.IsNullOrEmpty(locncodeNaDavku))
                {
                    sbInfo.Text += ",L:" + locncodeNaDavku;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemZalokovaniList, ChangeFilter");
            }
        }

        private void ZobrazNezalokovane()
        {
            try
            {
                filtrZobrazitVse = false;
                SetFilter();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemZalokovaniList, miZobrazitNezadane");
            }
        }

        private Fask.MST_W.PrijemService.Obecne OnlineGetDoporuceneLokace(string itemnmbr, string serltnum, string skl_id)
        {
            Fask.MST_W.PrijemService.Obecne ds = new Fask.MST_W.PrijemService.Obecne();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ds = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_GetDoporuceneLokace(itemnmbr, skl_id, serltnum);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Prijem.PrijemZalokovaniList, OnlineGetDoporuceneLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                return null;
            }

            return ds;
        }

        private Fask.MST_W.PrijemService.StatusOverLokace OnlineOverLokace(string itemnmbr, string serltnum, string locncode, string skl_id, decimal qtyshppd)
        {
            Fask.MST_W.PrijemService.StatusOverLokace so;
            try
            {
                so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_OverLokace(serltnum, itemnmbr, locncode, qtyshppd, skl_id);
                //if (so.State != 0)  // nastala chyba
                //{
                //    throw new Exception(so.Error);
                //}
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemZalokovaniList, OnlineGenerateSerltnum");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }

            return so;
        }

        private void miNastavitLokaci_Click(object sender, EventArgs e)
        {
            PerformNastavitLokaci();
        }

        /// <summary>
        /// Slouzi k vyberu lokace, ktera se bude automaticky pouzivat misto lokace, ktera je nastavena na predloze
        /// </summary>
        private void PerformNastavitLokaci()
        {
            try
            {
                ScannerStop();

                miNastavitLokaci.Checked = !miNastavitLokaci.Checked;
                if (miNastavitLokaci.Checked)
                {
                    #region rucni zadani lokace
                    using (PrijemZadejLokaci pzl = new PrijemZadejLokaci())
                    {
                        // nastaveni lokace, ktera se bude pouzivat (ne jen predvyplnovat ...
                        pzl.Popis = MST_Global.LC_NAME;
                        pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
						pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                        //skf.Len = Globals.LOCNCODE_LEN;
                        //skf.CheckLen = true;
                        pzl.AllowEmpty = false;
                        pzl.Kod = locncodeNaDavku;
                        //pzl.perow = PERow;

                        if (pzl.ShowDialog() == DialogResult.Cancel)
                        {
                            miNastavitLokaci.Checked = false;
                            return;
                        }

                        locncodeNaDavku = pzl.Kod;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemZalokovaniList, miNastavitLokaci_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                SetFilter();
                ScannerStart();
            }
        }
    }
}