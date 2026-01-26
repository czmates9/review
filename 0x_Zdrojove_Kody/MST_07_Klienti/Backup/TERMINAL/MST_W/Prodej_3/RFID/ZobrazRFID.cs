// TODO : parsovani hodnot z pameti tagu ...

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Fask.MST_W.Forms;
using System.IO;
using Fask.ScannerProvider;
using Fask.Graphic;
using Fask.MST_W.RFID;

namespace Fask.MST_W.Prodej_3.RFID
{
    public partial class ZobrazRFID : System.Windows.Forms.Form
    {

		private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _sklad = null;

        #region Pomocne veci-dialog pro naplneni polozky, vybrana polozka
        /// <summary>
        /// Nasnimane kody RFID scannerem.
        /// </summary>
        private System.Collections.Generic.List<Fask.MST_W.Scanner.RFIDTagData> nasnimaneKody = new List<Fask.MST_W.Scanner.RFIDTagData>();
        /// <summary>
        /// Povoli zadat pouze zadany pocet nactenych tagu. Pokud jich je vic nebo min, dojde k zobrazeni upozorneni a nepusti to dal ...
        /// </summary>
        public bool A_OmezitPocetNactenychZaznamu = false;

        private int _pocetZaznamu;
        /// <summary>
        /// Pocet kodu, ktere se maji nacist.
        /// </summary>
        public int A_PocetZaznamu
        {
            get
            {
                return _pocetZaznamu;
            }
            set
            {
                _pocetZaznamu = value;
            }
        }

        /// <summary>
        /// Vrati vybrarou polozku ze seznamu pokud neni tak null
        /// </summary>
        public ZobrazitRFID.PolozkyRow A_VybranaPolozka
        {
            get
            {
                try
                {
                    return (bsPolozkyBindingSource.Current as DataRowView).Row as ZobrazitRFID.PolozkyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

		private Fask.SQLiteDBs.DataSets.Obecne dsObecne = null;
        /// <summary>
        /// Vyledna nasnimana data.
        /// </summary>
		public Fask.SQLiteDBs.DataSets.Obecne A_DS_Nasnimane
        {
            get
            {
                return dsObecne;
            }
            set
            {
                dsObecne = value;
                // provest vypocet ... 
                foreach (var i in dsObecne.RFID)
                {

                    #region Trapny pokud o Lokacny mechanizmus



                    dsZobrazitRFID.Polozky.AddPolozkyRow(
                        String.Empty,
                        String.Empty,
                        i.ID,
                        String.Empty,//sgtin96.EAN.Trim(),
                        1 // proste je tag jeden kus ...
                        );


                    #endregion

                    #region 14.2.2018 TaD

                    ////MST_W.RFID.EPC_SGTIN96 sgtin96 = new EPC_SGTIN96(i.EPC);

                    //string itemdesc = string.Empty;
                    //string itemnmbr = string.Empty;
                    //string CZ_CarKod = string.Empty;

                    //Fask.MST_W.SqlCEDBs.DataSets.Zbozi.CZMST095DataTable zbozi = _ta_zbozi.GetDataBySERLTNUMsklad(i.ID.Trim(), _sklad.skl_id.Trim());
                    //if (zbozi.Count() > 0)
                    //{
                    //    itemdesc = zbozi.First().ITEMDESC.Trim();
                    //    itemnmbr = zbozi.First().ITEMNMBR.Trim();
                    //    CZ_CarKod = zbozi.First().CZ_CarKod.Trim();
                    //}
                    //else
                    //    itemdesc = "";

                    //dsZobrazitRFID.Polozky.AddPolozkyRow(
                    //    string.IsNullOrEmpty(itemdesc) ? string.Empty : itemdesc,
                    //    string.IsNullOrEmpty(itemnmbr) ? string.Empty : itemnmbr,
                    //    i.ID,
                    //    CZ_CarKod,//sgtin96.EAN.Trim(),
                    //    1 // proste je tag jeden kus ...
                    //    );


                    #endregion

                    #region puvodny kod

                    //MST_W.RFID.EPC_SGTIN96 sgtin96 = new EPC_SGTIN96(i.EPC);
                    //MST_W.RFID.USER_512b user512b = new USER_512b(i.USER);

                    //var shoda = dsZobrazitRFID.Polozky.Where(
                    //    x => (String.Equals(x.Ean.Trim(), sgtin96.EAN.Trim())) 
                    //        && (String.Equals(x.Serialnmbr.Trim(), user512b.SerltNumber.Trim()))
                    //        && (String.Equals(x.Itemnmbr.Trim(), user512b.ItemNumber.Trim()))
                    //    );
                    //if (shoda.Count() > 0)
                    //{
                    //    shoda.First().mnozstvi++;
                    //}
                    //else
                    //{
                    //    // pokus o dohledani nazvu polozky z ciselniku ...
                    //    string itemdesc = "";
                    //    var zbozi = _ta_zbozi.GetDataByPolozkacisloSkladEquals(
                    //        user512b.ItemNumber.Trim(), _sklad.skl_id.Trim());
                    //    if (zbozi.Count() > 0)
                    //        itemdesc = zbozi.First().ITEMDESC.Trim();
                    //    else
                    //        itemdesc = "";

                    //    dsZobrazitRFID.Polozky.AddPolozkyRow(
                    //        !string.IsNullOrEmpty(itemdesc) ? itemdesc : user512b.ItemDesc, 
                    //        user512b.ItemNumber.Trim(),
                    //        user512b.SerltNumber.Trim(),
                    //        sgtin96.EAN.Trim(),
                    //        1 // proste je tag jeden kus ...
                    //        );
                    //}
#endregion
                }
            }
        }

        public ZobrazitRFID A_DS_ZobrazitRFID
        {
            get { return dsZobrazitRFID; }
            
        }

        //tlacitka polozkyBindingSource.
        /// <summary>
        /// Pri stisknu tlatitek - nefunguje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnimatRFID_KeyDown(object sender, KeyEventArgs e)
        {
            //pokud stiskl excape
            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec();
            }//enter
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Back)
            {
                SmazRadekZTabulky(this.A_VybranaPolozka);
            }
            else if (e.KeyCode == Keys.D1)
            {
                menuItemZobrazeniRezim_Click(null, null);
            }
            else
                return;
            //jak dojde sem tak se neco pouzilo z podminek krom posledni
            e.Handled = true;

        }

        #endregion
        #region inicializace
        /// <summary>
        /// Konstruktor.
        /// </summary>
		public ZobrazRFID(Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _sklad)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            InitializeDataGridView();
            MyGridInitialize();

            this._sklad = _sklad;

            Cursor.Current = Cursors.Default;
        }

        private void MyGridInitialize()
        {
            this.dataGrid2Polozky.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid2Polozky.Font = new Font(this.dataGrid2Polozky.Font.Name, Settings.UIGridFont, this.dataGrid2Polozky.Font.Style);
            this.dataGrid2Polozky.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void MyGridSave()
        {
            this.dataGrid2Polozky.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }


        private void InitializeDataGridView()
        {
            //DataGridTableStyle ts = new DataGridTableStyle();
            //ts.MappingName = dsObecne.RFID.TableName;       // _katalogZbozi.CZMST095.TableName;

            //DataGrid2TextBoxColumn dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "ID";
            //dg.MappingName = dsObecne.RFID.IDColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 150;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "Tag ID";
            //dg.MappingName = dsObecne.RFID.TIDColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 150;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "EPC";
            //dg.MappingName = dsObecne.RFID.EPCColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 150;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "USER";
            //dg.MappingName = dsObecne.RFID.USERColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 150;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "RESERVED";
            //dg.MappingName = dsObecne.RFID.RESERVEDColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 150;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "TID Len";
            //dg.MappingName = dsObecne.RFID.TIDLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "EPC Len";
            //dg.MappingName = dsObecne.RFID.EPCLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "USER Len";
            //dg.MappingName = dsObecne.RFID.USERLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "RESERVED Len";
            //dg.MappingName = dsObecne.RFID.RESERVEDLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "Seen";
            //dg.MappingName = dsObecne.RFID.SeenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            //dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "RSSI";
            //dg.MappingName = dsObecne.RFID.RSSIColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            //dataGrid2Polozky.TableStyles.Add(ts);
        }

        /// <summary>
        /// Vola se pri load jen nastavi jmeno okna atd a veci pro data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnimatRFID_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            //Cursor.Current = Cursors.WaitCursor;
            //z list polozky
            //this.Text += " " + MST_Global.Inventura2Name.Trim();
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            //menuItemInfo.Enabled = SelectedSI != null ? true : false;
            //this.ShowData(ShowDataType.Nalezene);

            this.dataGrid2Polozky.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid2Polozky.KeyScrollUp = MST_Global.DataGridScrollUp;
            this.dataGrid2Polozky.Focus();

            //Nastaveni default sortu podle casu nacteni od nejnovejsiho po nejstarsi...
            //pokud ovsem neni nastaven jiny uzivatelsky sort...
            //tedy pouze pri 1.inicializaci ...
            //if (this.dataGrid2Polozky.Sort == string.Empty)
            //{
            //    this.dataGrid2Polozky.Sort = "CASNACTENO desc";
            //}

            //Cursor.Current = Cursors.Default;


            #region nastaveni vychoziho zobrazeni panelu ...
            panelList.Dock = DockStyle.Fill;
            //panelDetail.Dock = DockStyle.Fill;

            panelList.Show();
            //panelDetail.Hide();

            dataGrid2Polozky.Focus();

            #endregion

        }
        #endregion




        #region Zpracovani polozek a odstraneni z tabulky

        /// <summary>
        /// Odstrani radek z tabulky Id jsou stejna
        /// </summary>
        /// <param name="mrow">radek co se bude mazat</param>
        private void SmazRadekZTabulky(ZobrazitRFID.PolozkyRow mrow)
        {
            // listPolozkyDS.Polozky.
            //.ID
            try
            {
                if (mrow == null)
                    return;
                //Fask.MST_W.Inventura2.ListPolozkyDS.PolozkyRow rowForDelete = listPolozkyDS.Polozky.FindByID(mrow.ID);
                //listPolozkyDS.Polozky.RemovePolozkyRow(rowForDelete);
                dsZobrazitRFID.Polozky.RemovePolozkyRow(mrow);
                dsZobrazitRFID.Polozky.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
            finally
            {
                UpdateUI();
            }
        }

        #endregion

        #region Metody na ukonceni dialogu
        /// <summary>
        /// Stiskn tlacitka na konec
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemKonec_Click(object sender, EventArgs e)
        {
            PerformKonec();//pokud chci otazku
        }

        /// <summary>
        /// Metoda pro ukonceni
        /// </summary>
        /// <param name="question"></param>
        private void PerformKonec()
        {

            // TODO : Priat ze ma naskenovane polozky!!
            if (MessageBoxBig.Show("Opravdu ukončit snímání RFID?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            MyGridSave();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        /// <summary>
        /// Smaze vybranou polozku z tabulky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            SmazRadekZTabulky(A_VybranaPolozka);
        }

        private void UpdateStatusBar()
        {
            string status = string.Empty;
            if (A_OmezitPocetNactenychZaznamu)
                status = string.Format("{0} z {1}", dsObecne.RFID.Count, A_PocetZaznamu);
            else
                status = string.Format("{0}", dsObecne.RFID.Count);

            this.statusBar1.Text = status;
        }


        private void SnimatRFID_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void SnimatRFID_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void SnimatRFID_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
        }

        private void miPotvrdit_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void SwitchZobrazeni()
        {
            if (panelList.Visible)
            {
                panelList.Hide();
                //panelDetail.Show();
            }
            else
            {
                panelList.Show();
                //panelDetail.Hide();
            }

            dataGrid2Polozky.Focus();
        }

        private void menuItemZobrazeniRezim_Click(object sender, EventArgs e)
        {
            SwitchZobrazeni();
        }

        private void dataGrid2Polozky_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateStatusBar();
        }

    }
}