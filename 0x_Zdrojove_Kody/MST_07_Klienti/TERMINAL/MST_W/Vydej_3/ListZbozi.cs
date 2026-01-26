using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.Graphic;
using Fask.MST_W.Classes;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class ListZbozi : System.Windows.Forms.Form
    {
        //Promenna pro vybranne zbozi, carovy kod a cislo polozky
        public DataRow vybraneZbozi = null;
        private string cz_carkod = null;
        private string vnditnum = null;
        //Promenne pro priznak zda se ma vyhledavat jen scannerem a pro sloupce/sloupec, dle kterych se ma hleda
        private bool vybiratZboziJenScannerem;
        private string vyhledavatZboziDleSloupce;
        //Promenna pro uchovavani metody vyberu - 0 by nemela zustat
        public byte input_mode = 0;
        //Tabulka s aktualnim vyberem
        Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable globalTable;

        public ListZbozi(bool scannerOnly, string findByColumns)
            : this(string.Empty, string.Empty, scannerOnly, findByColumns)
        {
            /*
             * Konstruktor pro pripady, kdy se form hodi jen pro zobrazeni
             * nacteneho zbozi do datagridu, tzn nedojde k pouziti promennych 
             * barcode a vnditnum, tzn funkce nactiZbozi.
             */
        }

        //Konstruktor
        public ListZbozi(string barcodeParam, string vnditnumParam, bool scannerOnly, string findByColumns)
        {
            //Inicializace komponent
            InitializeComponent();

            //Carovy kod, cislo polozky
            cz_carkod = barcodeParam;
            vnditnum = vnditnumParam;

            //Priznak scanneru a sloupce
            vybiratZboziJenScannerem = scannerOnly;
            vyhledavatZboziDleSloupce = findByColumns;

            //Prebirani udalosti
            KeyPreview = true;
        }

        //Inicializuje datagrid
        private void initializeDataGrid()
        {
            dgZbozi.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            dgZbozi.Font = new Font(dgZbozi.Font.Name, Settings.UIGridFont, dgZbozi.Font.Style);
            dgZbozi.Load(Path.Combine(Main.ConfigDir, GetType().ToString()));
        }

        //Nacteni zbozi dle zadaneho caroveho kodu
        public bool nactiZbozi()
        {
            //Kontrola souboru
            if (!System.IO.File.Exists(Main.CiselnikZboziDB)) throw new ApplicationException("Nejprve je nutné stáhnout číselník zboží.");

            try
            {

                //Kurzor
                Cursor.Current = Cursors.WaitCursor;

                globalTable = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
                Vydej.vydejInstance.globalObject.controller_zbozi.FillBySpecificCondition(globalTable, vyhledavatZboziDleSloupce, vnditnum, cz_carkod);

                //Kurzor zpet
                Cursor.Current = Cursors.Default;

                //Zobrazeni
                if (globalTable.Rows.Count == 0) 
                    throw new ApplicationException(string.Format(Fask.Localization.Localization.Vydej3ListZboziNenalezenoZboziPodleCK, cz_carkod, vnditnum));
                else 
                    showTable(globalTable);

                //OK
                return true;
            }
            catch (Exception ex)
            {
                //Kurzor zpet - v pripade chyby
                Cursor.Current = Cursors.Default;
                //Zobrazeni chyby a zalogovani
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Logging.Log.Write(ex);

                //KO
                return false;
            }
        }

        //Zobrazeni tabulky do datagridu
        public void showTable(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table)
        {
            //Zobrazeni
            dgZbozi.DataSource = new DataView(table);
            //Vzhled vychozi
            InitializeDataGridView(table);
            //Vzhled ulozeny
            initializeDataGrid();
            //Ulozeni tabulky, pokud to uz neni ona tabulka
            if (!table.Equals(globalTable))
            {
                globalTable = table.Copy() as Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable;
            }
        }

        //Vyfiltrovani polozek z tabulky
        private void filterTable()
        {
            try
            {
                //Select dle nastaveni
                string select = string.Empty;
                if (vyhledavatZboziDleSloupce == "CZ_CARKOD,VNDITNUM")
                {
                    select = "cz_carkod='" + cz_carkod + "' OR vnditnum='" + vnditnum + "'";
                }
                else if (vyhledavatZboziDleSloupce == "CZ_CARKOD")
                {
                    select = "cz_carkod='" + cz_carkod + "'";
                }
                else if (vyhledavatZboziDleSloupce == "VNDITNUM")
                {
                    select = "vnditnum='" + vnditnum + "'";
                }

                //Kurzor
                Cursor.Current = Cursors.WaitCursor;

                ((DataView)dgZbozi.DataSource).RowFilter = select;
                int rows = ((DataView)dgZbozi.DataSource).Count;

                ////V tabulce urcite neco je - musim udelat kopii, protoze radky jsou odkazy (lokalni kopie zanikne s funkci,globalni jede dal)
                //Fask.MST_W.SqlCEDBs.DataSets.Zbozi.CZMST095DataTable localCopy = globalTable.Copy() as Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable;
                //Fask.MST_W.SqlCEDBs.DataSets.Zbozi.CZMST095Row[] rows = (Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row[])localCopy.Select(select);

                ////Odstraneni starych radku a vlozeni novych - casove asi nejschudnejsi varianta
                //if (rows.Length > 0)
                //{
                //    globalTable.Rows.Clear();
                //    foreach (Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row dr in rows)
                //    {
                //        globalTable.Rows.Add(dr.ItemArray);
                //    }
                //}

                //Kuroz zpet
                Cursor.Current = Cursors.Default;

                //Po vyfiltrovani muze byt nekolik moznosti - zadna polozka, 1 a vice
                if (rows > 1)
                {
                    //Pokud je vice, povolim zadavat rucne
                    vybiratZboziJenScannerem = false;
                    //showTable(globalTable);
                }
                else if (rows == 0)
                {
                    //Pokud tam neni nic, vse odstranil select - zobrazim hlasku, vracim data a zobrazim data
                    MessageBox.Show(Fask.Localization.Localization.Vydej3ListZboziNenalezenoZbozi, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    //showTable(globalTable);

                    ((DataView)dgZbozi.DataSource).RowFilter = string.Empty;
                }
                else
                {
                    //Kontrola, zda se muze vybirat necim jinacim nez scannerem
                    if (!InputModeChecker.checkInputMode(vybiratZboziJenScannerem, input_mode))
                    {
                        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListZboziPolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        //showTable(localCopy);

                        ((DataView)dgZbozi.DataSource).RowFilter = string.Empty;
                    }
                    else
                    {
                        //Prave jedna polozka - vybrana, konec
                        vybraneZbozi = dgZbozi.CurrentRow;
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                //Kurzor zpet - v pripade chyby
                Cursor.Current = Cursors.Default;
                //Zobrazeni chyby a zalogovani
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Logging.Log.Write(ex);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        //Vybrat
        private void menuItemVybrat_Click(object sender, EventArgs e)
        {
            //2 moznosti pro vyber jakoby enterem - jedna z menu = alternativa enteru
            input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
            chooseItem();
        }

        //Vlastni vyber oznacene polozky
        private void chooseItem()
        {
            try
            {
                //Kontrola, zda se muze vybirat necim jinacim nez scannerem
                if (!InputModeChecker.checkInputMode(vybiratZboziJenScannerem, input_mode))
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListZboziPolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                //Kontrola
                if (dgZbozi.CurrentRow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListZboziJeTrebaVybratPolozku, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                //Vlozeni do aktualniho radku
                vybraneZbozi = dgZbozi.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Logging.Log.Write(ex);
            }

            //Ukonceni v pripade uspesneho vyberu
            DialogResult = DialogResult.OK;
        }

        //Zpet
        private void menuItemZpet_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //Hleadni dle ck
        private void menuItemHledatCK_Click(object sender, EventArgs e)
        {
            input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
            NajdiPolozku();
        }

        //Nalezeni polozky 
        private string EANKod = string.Empty;
        private void NajdiPolozku()
        {
            try
            {
                ScannerStop();
                if (DialogResult.Cancel == Forms.InputBox.Show(Fask.Localization.Localization.Vydej3ListZboziZadejteCarovyKod, EANKod, out EANKod, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric))
                {
                    return;
                }

                //Vystup nastaven do obou promennych
                cz_carkod = vnditnum = EANKod;
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Vydej3ListZboziHledaniCarovyKod, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                ScannerStart();
            }

            //Nakonec protrideni seznamu
            filterTable();
        }

        //Uzavreni formu
        private void ListZbozi_Closing(object sender, CancelEventArgs e)
        {
            //Ulozeni nastaveni dg
            dgZbozi.Save(Path.Combine(Main.ConfigDir, GetType().ToString()));
            //Vypnuti scanneru
            ScannerFinalize();
        }

        //Inicializace gridu
        private void InitializeDataGridView(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable _katalogZbozi)
        {
            dgZbozi.TableStyles.Clear();

            //Styl
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = _katalogZbozi.TableName;

            DataGrid2TextBoxColumn dgNazev = new DataGrid2TextBoxColumn();
            dgNazev.HeaderText = "Název";
            dgNazev.MappingName = _katalogZbozi.ITEMDESCColumn.ColumnName;
            dgNazev.NullText = "-";
            dgNazev.Width = 150;
            dgNazev.Grid = dgZbozi;
            dgNazev.SelectionShow = false;
            ts.GridColumnStyles.Add(dgNazev);

            DataGrid2TextBoxColumn dg1 = new DataGrid2TextBoxColumn();
            dg1.HeaderText = "Položka";
            dg1.MappingName = _katalogZbozi.ITEMNMBRColumn.ColumnName;
            dg1.NullText = "-";
            dg1.Width = 50;
            dg1.Grid = dgZbozi;
            dg1.SelectionShow = false;
            ts.GridColumnStyles.Add(dg1);

            //Na odberatele se nehraje
            //
            //DataGrid2NumberBoxColumn dgCena = new DataGrid2NumberBoxColumn();
            //try
            //{
            //    dgCena.HeaderText = "Cena";
            //    dgCena.MappingName = _katalogZbozi.Columns["PRICE" + _odberatel.odb_typ.Trim()].ColumnName;
            //    dgCena.NullText = "-";
            //    dgCena.Width = 50;
            //    dgCena.Alignment = StringAlignment.Far;
            //    ts.GridColumnStyles.Add(dgCena);
            //}
            //catch { }

            DataGrid2TextBoxColumn dgCarKod = new DataGrid2TextBoxColumn();
            dgCarKod.HeaderText = "Č.k.";
            dgCarKod.MappingName = _katalogZbozi.VNDITNUMColumn.ColumnName;
            dgCarKod.NullText = "-";
            dgCarKod.Width = 150;
            dgCarKod.Grid = dgZbozi;
            dgCarKod.SelectionShow = false;
            ts.GridColumnStyles.Add(dgCarKod);

            DataGrid2TextBoxColumn dg2 = new DataGrid2TextBoxColumn();
            dg2.HeaderText = "Č.k. vlastní";
            dg2.MappingName = _katalogZbozi.CZ_CarKodColumn.ColumnName;
            dg2.NullText = "-";
            dg2.Width = 50;
            dg2.Grid = dgZbozi;
            dg2.SelectionShow = false;
            ts.GridColumnStyles.Add(dg2);

            DataGrid2TextBoxColumn dg3 = new DataGrid2TextBoxColumn();
            dg3.HeaderText = "Lokace";
            dg3.MappingName = _katalogZbozi.LOCNCODEColumn.ColumnName;
            dg3.NullText = "-";
            dg3.Width = 50;
            dg3.Grid = dgZbozi;
            dg3.SelectionShow = false;
            ts.GridColumnStyles.Add(dg3);

            DataGrid2NumberBoxColumn dg4 = new DataGrid2NumberBoxColumn();
            dg4.HeaderText = "Množství";
            dg4.MappingName = _katalogZbozi.QTYColumn.ColumnName;
            dg4.NullText = "-";
            dg4.Width = 50;
            dg4.Alignment = StringAlignment.Far;
            dg4.Grid = dgZbozi;
            dg4.SelectionShow = false;
            ts.GridColumnStyles.Add(dg4);

            DataGrid2NumberBoxColumn dg5 = new DataGrid2NumberBoxColumn();
            dg5.HeaderText = "Balení";
            dg5.MappingName = _katalogZbozi.QTYPACKColumn.ColumnName;
            dg5.NullText = "-";
            dg5.Width = 50;
            dg5.Grid = dgZbozi;
            dg5.SelectionShow = false;
            ts.GridColumnStyles.Add(dg5);

            DataGrid2TextBoxColumn dg6 = new DataGrid2TextBoxColumn();
            dg6.HeaderText = "Sklad ID";
            dg6.MappingName = _katalogZbozi.SKL_IDColumn.ColumnName;
            dg6.NullText = "-";
            dg6.Width = 50;
            dg6.Grid = dgZbozi;
            dg6.SelectionShow = false;
            ts.GridColumnStyles.Add(dg6);

            DataGrid2TextBoxColumn dg7 = new DataGrid2TextBoxColumn();
            dg7.HeaderText = "MJ";
            dg7.MappingName = _katalogZbozi.MJColumn.ColumnName;
            dg7.NullText = "-";
            dg7.Width = 50;
            dg7.Grid = dgZbozi;
            dg7.SelectionShow = false;
            ts.GridColumnStyles.Add(dg7);

            DataGrid2TextBoxColumn dg8 = new DataGrid2TextBoxColumn();
            dg8.HeaderText = "DMJ";
            dg8.MappingName = _katalogZbozi.DMJColumn.ColumnName;
            dg8.NullText = "-";
            dg8.Width = 50;
            dg8.Grid = dgZbozi;
            dg8.SelectionShow = false;
            ts.GridColumnStyles.Add(dg8);

            DataGrid2TextBoxColumn dg9 = new DataGrid2TextBoxColumn();
            dg9.HeaderText = "REZ1";
            dg9.MappingName = _katalogZbozi.REZ1Column.ColumnName;
            dg9.NullText = "-";
            dg9.Width = 50;
            dg9.Grid = dgZbozi;
            dg9.SelectionShow = false;
            ts.GridColumnStyles.Add(dg9);

            dgZbozi.TableStyles.Add(ts);
        }

        //Stisk klaves
        private void ListZbozi_KeyDown(object sender, KeyEventArgs e)
        {
            //Klavesy pro posuv v datagridu jsou preposlany datagridu
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) return;

            if (e.KeyCode == Keys.Enter)
            {
                menuItemVybrat_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                menuItemZpet_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                menuItemHledatCK_Click(null, null);
            }
            else
            {
                return; //not handled ...
            }

            //Zpracovano
            e.Handled = true;
        }

        //Rozlisemmi a styl pri nacteni
        private void ListZbozi_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            //Styl ramce
            FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //Velikost
            Size = Forms.FormLocation.ScreenResolution;
            //Spusteni scanneru
            ScannerStart();
        }

        //Prace se scannerem nize - prevzato z vydeje, list polozek
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

        private void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            //Nstaveni aktualnich kodu na nacteny
            cz_carkod = vnditnum = e.BarcodeData.Trim();
            //Zaznamenani vstupniho modu
            input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);
            //Vyhledani pomoci modifikovanych promennych
            this.BeginInvoke(new DelegateNoParams(filterTable));
        }

        delegate void DelegateNoParams();
    }
}