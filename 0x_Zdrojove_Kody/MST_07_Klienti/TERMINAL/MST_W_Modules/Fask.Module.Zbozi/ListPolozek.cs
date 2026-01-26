using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Graphic;
using System.IO;

namespace Fask.Module.Zbozi
{
    public partial class ListPolozek : Form
    {
        private DataSets.Zbozi _katalogZbozi = new Fask.Module.Zbozi.DataSets.Zbozi();
        private DataSets.Zbozi.CZMST095Row _zbozi = null;
        private DataView _katalogZboziView = null;

        public enum ZobrazeniTyp
        {
            List,
            Detail,
            Unknown
        }


        private ZobrazeniTyp _zobrazeni = ZobrazeniTyp.Unknown;
        public ZobrazeniTyp Zobrazeni
        {
            get { return _zobrazeni; }
            set
            {
                if (_zobrazeni != value)
                {
                    _zobrazeni = value;
                    switch (_zobrazeni)
                    {
                        case ZobrazeniTyp.Detail:
                            panelList.Hide();
                            panelDetail.Show();
                            panelDetail.Dock = DockStyle.Fill;
                            dataGrid1.Focus();
                            break;
                        case ZobrazeniTyp.List:
                        default:
                            _zobrazeni = ZobrazeniTyp.List;
                            panelDetail.Hide();
                            panelList.Show();
                            panelList.Dock = DockStyle.Fill;
                            dataGrid1.Focus();
                            break;
                    }
                }
            }
        }


        public DataSets.Zbozi.CZMST095Row SelectedZbozi
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[_katalogZboziView].Current as DataRowView).Row as DataSets.Zbozi.CZMST095Row;
                }
                catch
                {
                    return null;
                }
            }
        }


        public ListPolozek()
        {
            InitializeComponent();

            //this.dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            //this.dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            Globals.Configuration = new Fask.Module.Zbozi.DataSets.Configuration();
            if (File.Exists(Globals.ConfigurationFile))
                Globals.Configuration.ReadXml(Globals.ConfigurationFile);

            InitializeDataGridView();

            MyInitializeGrid();
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = _katalogZbozi.CZMST095.TableName;

            //DataGrid2TextBoxColumn dg = new DataGrid2TextBoxColumn();
            //dg.HeaderText = "";
            //dg.MappingName = _katalogZbozi.CZMST095..ColumnName;
            //dg.NullText = "-";
            //dg.Width = 50;
            //ts.GridColumnStyles.Add(dg);

            DataGrid2TextBoxColumn dgNazev = new DataGrid2TextBoxColumn();
            dgNazev.HeaderText = "Název";
            dgNazev.MappingName = _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dgNazev.NullText = "-";
            dgNazev.Width = 150;
            ts.GridColumnStyles.Add(dgNazev);

            DataGrid2TextBoxColumn dg1 = new DataGrid2TextBoxColumn();
            dg1.HeaderText = "Položka č.";
            dg1.MappingName = _katalogZbozi.CZMST095.ITEMNMBRColumn.ColumnName;
            dg1.NullText = "-";
            dg1.Width = 50;
            ts.GridColumnStyles.Add(dg1);

            DataGrid2TextBoxColumn dgCarKod = new DataGrid2TextBoxColumn();
            dgCarKod.HeaderText = "Č.k.";
            dgCarKod.MappingName = _katalogZbozi.CZMST095.VNDITNUMColumn.ColumnName;
            dgCarKod.NullText = "-";
            dgCarKod.Width = 150;
            ts.GridColumnStyles.Add(dgCarKod);

            DataGrid2TextBoxColumn dg2 = new DataGrid2TextBoxColumn();
            dg2.HeaderText = "Č.k. vlastní";
            dg2.MappingName = _katalogZbozi.CZMST095.CZ_CarKodColumn.ColumnName;
            dg2.NullText = "-";
            dg2.Width = 50;
            ts.GridColumnStyles.Add(dg2);

            DataGrid2TextBoxColumn dg3 = new DataGrid2TextBoxColumn();
            dg3.HeaderText = "Lokace";
            dg3.MappingName = _katalogZbozi.CZMST095.LOCNCODEColumn.ColumnName;
            dg3.NullText = "-";
            dg3.Width = 50;
            ts.GridColumnStyles.Add(dg3);

            DataGrid2TextBoxColumn dg10 = new DataGrid2TextBoxColumn();
            dg10.HeaderText = "Kód položky";
            dg10.MappingName = _katalogZbozi.CZMST095.ITEMCODEColumn.ColumnName;
            dg10.NullText = "-";
            dg10.Width = 50;
            ts.GridColumnStyles.Add(dg10);

            DataGrid2NumberBoxColumn dg4 = new DataGrid2NumberBoxColumn();
            dg4.HeaderText = "Množství";
            dg4.MappingName = _katalogZbozi.CZMST095.QTYColumn.ColumnName;
            dg4.NullText = "-";
            dg4.Width = 50;
            dg4.Alignment = StringAlignment.Far;
            ts.GridColumnStyles.Add(dg4);

            DataGrid2NumberBoxColumn dg5 = new DataGrid2NumberBoxColumn();
            dg5.HeaderText = "Balení";
            dg5.MappingName = _katalogZbozi.CZMST095.QTYPACKColumn.ColumnName;
            dg5.NullText = "-";
            dg5.Width = 50;
            ts.GridColumnStyles.Add(dg5);

            DataGrid2TextBoxColumn dg6 = new DataGrid2TextBoxColumn();
            dg6.HeaderText = "Sklad ID";
            dg6.MappingName = _katalogZbozi.CZMST095.SKL_IDColumn.ColumnName;
            dg6.NullText = "-";
            dg6.Width = 50;
            ts.GridColumnStyles.Add(dg6);

            DataGrid2TextBoxColumn dg7 = new DataGrid2TextBoxColumn();
            dg7.HeaderText = "MJ";
            dg7.MappingName = _katalogZbozi.CZMST095.MJColumn.ColumnName;
            dg7.NullText = "-";
            dg7.Width = 50;
            ts.GridColumnStyles.Add(dg7);

            DataGrid2TextBoxColumn dg8 = new DataGrid2TextBoxColumn();
            dg8.HeaderText = "DMJ";
            dg8.MappingName = _katalogZbozi.CZMST095.DMJColumn.ColumnName;
            dg8.NullText = "-";
            dg8.Width = 50;
            ts.GridColumnStyles.Add(dg8);

            DataGrid2TextBoxColumn dg9 = new DataGrid2TextBoxColumn();
            dg9.HeaderText = "REZ1";
            dg9.MappingName = _katalogZbozi.CZMST095.REZ1Column.ColumnName;
            dg9.NullText = "-";
            dg9.Width = 50;
            ts.GridColumnStyles.Add(dg9);

            DataGrid2TextBoxColumn dg11 = new DataGrid2TextBoxColumn();
            dg11.HeaderText = "Sklad Název";
            dg11.MappingName = _katalogZbozi.CZMST095.SKL_DESCColumn.ColumnName;
            dg11.NullText = "-";
            dg11.Width = 50;
            ts.GridColumnStyles.Add(dg11);

            //Zobrazeni cen ...
            DataGrid2NumberBoxColumn dgPrice = new DataGrid2NumberBoxColumn();
            dgPrice.HeaderText = "Cena 0";
            dgPrice.MappingName = _katalogZbozi.CZMST095.PRICE0Column.ColumnName;
            dgPrice.NullText = "-";
            dgPrice.Width = 50;
            ts.GridColumnStyles.Add(dgPrice);

            dgPrice = new DataGrid2NumberBoxColumn();
            dgPrice.HeaderText = "Cena 1";
            dgPrice.MappingName = _katalogZbozi.CZMST095.PRICE1Column.ColumnName;
            dgPrice.NullText = "-";
            dgPrice.Width = 50;
            ts.GridColumnStyles.Add(dgPrice);

            dgPrice = new DataGrid2NumberBoxColumn();
            dgPrice.HeaderText = "Cena 2";
            dgPrice.MappingName = _katalogZbozi.CZMST095.PRICE2Column.ColumnName;
            dgPrice.NullText = "-";
            dgPrice.Width = 50;
            ts.GridColumnStyles.Add(dgPrice);

            dgPrice = new DataGrid2NumberBoxColumn();
            dgPrice.HeaderText = "Cena 3";
            dgPrice.MappingName = _katalogZbozi.CZMST095.PRICE3Column.ColumnName;
            dgPrice.NullText = "-";
            dgPrice.Width = 50;
            ts.GridColumnStyles.Add(dgPrice);

            dgPrice = new DataGrid2NumberBoxColumn();
            dgPrice.HeaderText = "Cena 4";
            dgPrice.MappingName = _katalogZbozi.CZMST095.PRICE4Column.ColumnName;
            dgPrice.NullText = "-";
            dgPrice.Width = 50;
            ts.GridColumnStyles.Add(dgPrice);

            dgPrice = new DataGrid2NumberBoxColumn();
            dgPrice.HeaderText = "Cena 5";
            dgPrice.MappingName = _katalogZbozi.CZMST095.PRICE5Column.ColumnName;
            dgPrice.NullText = "-";
            dgPrice.Width = 50;
            ts.GridColumnStyles.Add(dgPrice);

            dataGrid1.TableStyles.Add(ts);
        }

        public static string ConfigDir { get { return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\", "Config"); } }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles();
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, 10, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(ConfigDir, this.GetType().ToString() + "_1"));
        }

        private void MyInitializeForm()
        {
            try
            {
                DataGridTableStyle ts = this.dataGrid1.TableStyles[0];
                this.lP0.Text = ts.GridColumnStyles[_katalogZbozi.CZMST095.PRICE0Column.ColumnName].HeaderText + " :";
                this.lP1.Text = ts.GridColumnStyles[_katalogZbozi.CZMST095.PRICE1Column.ColumnName].HeaderText + " :";
                this.lP2.Text = ts.GridColumnStyles[_katalogZbozi.CZMST095.PRICE2Column.ColumnName].HeaderText + " :";
                this.lP3.Text = ts.GridColumnStyles[_katalogZbozi.CZMST095.PRICE3Column.ColumnName].HeaderText + " :";
                this.lP4.Text = ts.GridColumnStyles[_katalogZbozi.CZMST095.PRICE4Column.ColumnName].HeaderText + " :";
                this.lP5.Text = ts.GridColumnStyles[_katalogZbozi.CZMST095.PRICE5Column.ColumnName].HeaderText + " :";
            }
            catch //(Exception e)
            {
            }
        }

        private void UpdateForm()
        {
            //Update list
            _zbozi = this.SelectedZbozi;
            labelNazev.Text = "-";
            labelVNDITNUM.Text = "-";
            labelCZCarKod.Text = "-";
            labelItemnmbr.Text = "-";
            labelSklad.Text = "-";
            labelPRICE0.Text = "-";
            labelPRICE1.Text = "-";
            labelPRICE2.Text = "-";
            labelPRICE3.Text = "-";
            labelPRICE4.Text = "-";
            labelPRICE5.Text = "-";

            labelQTY.Text = "-";
            labelQTYPACK.Text = "-";
            labelSNTrack.Text = "-";
            labelTAXRATE.Text = "-";
            labelMJ.Text = "-";

            try
            {
                labelNazev.Text = _zbozi.ITEMDESC;
                labelVNDITNUM.Text = _zbozi.VNDITNUM;

                labelCZCarKod.Text = _zbozi.CZ_CarKod;
                labelItemnmbr.Text = _zbozi.IsITEMCODENull() ? "-" : _zbozi.ITEMCODE + " (" + _zbozi.ITEMNMBR.Trim() + ")";

                labelSklad.Text =
                    (_zbozi.IsSKL_DESCNull() ? "" : _zbozi.SKL_DESC.Trim()) +
                    "(" + (_zbozi.IsSKL_IDNull() ? "-" : _zbozi.SKL_ID.Trim()) + ")";

                labelQTY.Text = _zbozi.QTY.ToString();
                labelQTYPACK.Text = _zbozi.QTYPACK.ToString();
                labelSNTrack.Text = _zbozi.CZ_SerNum_Track == 0 ? "Ne" : "Ano";
                labelTAXRATE.Text = _zbozi.TAXRATE.ToString() + " %";
                labelMJ.Text = _zbozi.MJ.Trim();

                labelPRICE0.Text = _zbozi.IsPRICE0Null() ? "-" : _zbozi.PRICE0.ToString();
                labelPRICE1.Text = _zbozi.IsPRICE1Null() ? "-" : _zbozi.PRICE1.ToString();
                labelPRICE2.Text = _zbozi.IsPRICE2Null() ? "-" : _zbozi.PRICE2.ToString();
                labelPRICE3.Text = _zbozi.IsPRICE3Null() ? "-" : _zbozi.PRICE3.ToString();
                labelPRICE4.Text = _zbozi.IsPRICE4Null() ? "-" : _zbozi.PRICE4.ToString();
                labelPRICE5.Text = _zbozi.IsPRICE5Null() ? "-" : _zbozi.PRICE5.ToString();

            }
            catch
            {
            }
        }


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

            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
                return;
            }
            EnableScanner();
        }

        public void EnableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Enable();
        }
        public void DisableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Disable();
        }

        private void ScannerStop()
        {
            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch
            {
            }
            try
            {
                DisableScanner();
            }
            catch
            {
            }
        }


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            findCode(barcode);
        }

        private void findCode(string barcode)
        {
            try
            {
                ScannerStop();

                if (barcode != string.Empty)
                {
                    DataSets.Zbozi.CZMST095DataTable dt = new Fask.Module.Zbozi.DataSets.Zbozi.CZMST095DataTable();
                    using (DataSets.ZboziTableAdapters.CZMST095TableAdapter ta = new Fask.Module.Zbozi.DataSets.ZboziTableAdapters.CZMST095TableAdapter())
                    {
                        ta.Connection.ConnectionString = "Data source=" + Globals.CiselnikKatalogZboziDB;
                        dt = ta.GetDataByCarKod(barcode);
                    }

                    if (dt.Count <= 0)
                    {
                        MessageBoxBig.Show("Položka nenalezena!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    UpdateSkladNazev(dt);
                    _katalogZboziView = new DataView(dt);
                    this.dataGrid1.DataSource = _katalogZboziView;

                    UpdateForm();

                    if (dt.Count == 1) // automaticky jen pokud je jeden ...
                    {
                        //OnlineCheckHmotnost(this.SelectedZbozi);
                        //OnlineShowLocations(this.SelectedZbozi);
                        MakeEnterActions();
                    }
                }

            }
            finally
            {
                ScannerStart();
            }
        }

        private void UpdateSkladNazev(Fask.Module.Zbozi.DataSets.Zbozi.CZMST095DataTable dt)
        {
            using (DataSets.SkladyTableAdapters.CZMST093TableAdapter ta = new Fask.Module.Zbozi.DataSets.SkladyTableAdapters.CZMST093TableAdapter())
            {
                ta.Connection.ConnectionString = "Data source=" + Globals.CiselnikKatalogSkladyDB;
                try
                {
                    ta.Connection.Open();
                    foreach (DataSets.Zbozi.CZMST095Row zrow in dt)
                    {
                        zrow.SKL_DESC = ta.Get_Skl_desc(zrow.SKL_ID);
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex);
                }
                finally
                {
                    if ((ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                        ta.Connection.Close();
                }
            }
        }

        private void ListPolozek_Load(object sender, EventArgs e)
        {
            zobrazeniList();

            this.dataGrid1.KeyScrollDown = Keys.Down;
            this.dataGrid1.KeyScrollUp = Keys.Up;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            menuItemFunkceHmotnost.Checked = Globals.Configuration.OnlineHmotnost;
            menuItemFunkceLokace.Checked = Globals.Configuration.OnlineLokace;

            MyInitializeForm();

            ScannerStart();
        }

        private void miDetail_Click(object sender, EventArgs e)
        {
            zobrazeniDetail();

        }
        private void zobrazeniList()
        {
            Zobrazeni = ZobrazeniTyp.List;
        }

        private void menuItemDetail_Click(object sender, EventArgs e)
        {
            zobrazeniDetail();
        }

        private void zobrazeniDetail()
        {
            Zobrazeni = ZobrazeniTyp.Detail;
        }

        private void miList_Click(object sender, EventArgs e)
        {
            zobrazeniList();
        }

        private void miHledatDleNazvu_Click(object sender, EventArgs e)
        {
            FindName();
        }

        private void menuItemHledatDleKodu_Click(object sender, EventArgs e)
        {
            FindOznaceni();
        }

        string findname = string.Empty;
        private void FindName()
        {
            ScannerStop();

            DataSets.Zbozi.CZMST095DataTable dt = new Fask.Module.Zbozi.DataSets.Zbozi.CZMST095DataTable();

            using (DataSets.ZboziTableAdapters.CZMST095TableAdapter ta = new Fask.Module.Zbozi.DataSets.ZboziTableAdapters.CZMST095TableAdapter())
            {
                ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.CiselnikKatalogZboziDB);

                try
                {
                    if (InputBox.Show("Zadejte název položky", findname, out findname) != DialogResult.OK)
                        return;

                    string filtr = "%" + findname + "%";

                    object cnt = ta.CountByNazev(filtr);

                    if (cnt == null)
                    {
                        //MessageBoxBig.Show("null!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    if (cnt is DBNull)
                    {
                        //MessageBoxBig.Show("DBNULL!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    if (Convert.ToInt32(cnt) > 100)
                    {
                        MessageBoxBig.Show("Více než 100 záznamů!\nUpřesněte filtr...", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }

                    ta.FillByNazev(dt, filtr);

                    if (dt.Count <= 0)
                    {
                        MessageBoxBig.Show("Položka nenalezena!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    UpdateSkladNazev(dt);
                    _katalogZboziView = new DataView(dt);
                    this.dataGrid1.DataSource = _katalogZboziView;

                    UpdateForm();

                    if (dt.Count == 1) // automaticky jen pokud je jedno zbozi ...
                    {
                        //OnlineCheckHmotnost(this.SelectedZbozi);
                        //OnlineShowLocations(this.SelectedZbozi);
                        MakeEnterActions();
                    }

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                }
                finally
                {
                    ScannerStart();
                }
            }
        }

        string findoznaceni = string.Empty;
        private void FindOznaceni()
        {
            ScannerStop();
            object cnt = null;
            try
            {
                if (InputBox.Show("Zadejte označení položky", findoznaceni, out findoznaceni) != DialogResult.OK)
                    return;

                string filtr = "%" + findoznaceni + "%";

                DataSets.Zbozi.CZMST095DataTable dt = new Fask.Module.Zbozi.DataSets.Zbozi.CZMST095DataTable();

                using (DataSets.ZboziTableAdapters.CZMST095TableAdapter ta = new Fask.Module.Zbozi.DataSets.ZboziTableAdapters.CZMST095TableAdapter())
                {
                    ta.Connection.ConnectionString = "Data source=" + Globals.CiselnikKatalogZboziDB;

                    cnt = ta.CountByPolozkaCode(filtr);


                    if (cnt == null)
                    {
                        //MessageBoxBig.Show("null!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    if (cnt is DBNull)
                    {
                        //MessageBoxBig.Show("DBNULL!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    if (Convert.ToInt32(cnt) > 100)
                    {
                        MessageBoxBig.Show("Více než 100 záznamů!\nUpřesněte filtr...", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }

                    ta.FillByPolozkaCode(dt, filtr);
                }

                if (dt.Count <= 0)
                {
                    MessageBoxBig.Show("Položka nenalezena!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                UpdateSkladNazev(dt);
                _katalogZboziView = new DataView(dt);
                this.dataGrid1.DataSource = _katalogZboziView;

                UpdateForm();

                if (dt.Count == 1) // automaticky jen pokud je jedno zbozi ...
                {
                    //OnlineCheckHmotnost(this.SelectedZbozi);
                    //OnlineShowLocations(this.SelectedZbozi);
                    MakeEnterActions();
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void miKonec_Click(object sender, EventArgs e)
        {
            ExitModule();
        }

        private void ExitModule()
        {
            if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;

            ScannerFinalize();

            this.dataGrid1.Save(Path.Combine(ConfigDir, this.GetType().ToString() + "_1"));

            Globals.Configuration.WriteXml(Globals.ConfigurationFile);

            this.Close();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void ListPolozek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ExitModule();
            }
            if (e.KeyCode == Keys.Enter)
            { // provest akci ...
              // Hmotnost
                MakeEnterActions();
            }
            else if (e.KeyCode == Keys.F1)
            {
                FindBarcode();
            }
            else if (e.KeyCode == Keys.F2)
            {
                FindName();
            }
            else if (e.KeyCode == Keys.F3)
            {
                FindOznaceni();
            }
            else if (e.KeyCode == Keys.F5)
            {
                zobrazeniList();
            }
            else if (e.KeyCode == Keys.F6)
            {
                zobrazeniDetail();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void MakeEnterActions()
        {
            try
            {
                ScannerStop();

                // Online hmotnost
                OnlineCheckHmotnost(this.SelectedZbozi);

                // Online zobrazeni lokaci vybraneho znozi
                OnlineShowLocations(this.SelectedZbozi);

            }
            catch (Exception ex)
            {
				Fask.Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void OnlineShowLocations(DataSets.Zbozi.CZMST095Row zbozi)
        {
            if (!Globals.Configuration.OnlineLokace)
                return;

            try
            {
                ScannerStop();

                if (zbozi == null)
                {
                    MessageBox.Show("Není vybráno zboží");
                    return;
                }

                using (ZobrazeniLokaciList zll = new ZobrazeniLokaciList(zbozi))
                {
                    string origtext = zll.Text;
                    zll.Owner = this;
                    zll.Text = origtext;
                    zll.ShowDialog();

                    // zobrazeni me, pokud bych byl minimalizovany...???
                    this.Show();
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }

        }

        private void OnlineCheckHmotnost(DataSets.Zbozi.CZMST095Row zbozi)
        {
            if (!Globals.Configuration.OnlineHmotnost)
                return;

            if (zbozi == null)
            {
                MessageBox.Show("Není vybráno zboží");
                return;
            }

            WebServiceHmotnost.Hmotnost wsHmotnost = new Fask.Module.Zbozi.WebServiceHmotnost.Hmotnost();
            wsHmotnost.Url = Globals.ServerAddress + "Hmotnost.asmx";
            wsHmotnost.Timeout = Globals.ServerTimeout;
            // TODO : dalsi parametry ... ???

            decimal? hmotnost = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                hmotnost = wsHmotnost.GetHmotnost(zbozi.ITEMNMBR);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBox.Show(ex.Message, "WS Hmotnost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            if (hmotnost.HasValue)
            {
                DialogResult dlgResHmotnostZmena = MessageBox.Show(String.Format(" = {0}kg\nChcete změnit?", hmotnost.Value), "Hmotnost", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dlgResHmotnostZmena == DialogResult.No)
                    return;
            }

            string valueNew = string.Empty;
            string valueOld = (hmotnost.HasValue ? hmotnost.Value.ToString() : string.Empty);

            while (true)
            {
                DialogResult dlgHmotnostNew = InputBox.Show("Hmotnost", valueOld, out valueNew, false);
                if (dlgHmotnostNew == DialogResult.Cancel)
                    return;
                try
                {
                    if (String.IsNullOrEmpty(valueNew))
                        hmotnost = null;
                    else
                        hmotnost = decimal.Parse(valueNew);
                }
                catch (Exception exValueNew)
                {
                    MessageBox.Show(exValueNew.Message, "Hmotnost chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    continue;
                }
                
                break; // ukonci zadavani a pokracuje. ...
            }

            // ulozeni hmotnosti na server...

            while (true)
            {
                try
                {
                    bool saved = wsHmotnost.SetHmotnost(zbozi.ITEMNMBR, hmotnost);
                    if (saved)
                        break;
                    else
                        throw new Exception("Uložení hmotnosti se nezdařilo.");
                }
                catch (Exception exWebSetHmotnost)
                {
                    if (MessageBox.Show("Opakovat?\n" + exWebSetHmotnost.Message, "Hmotnost chyba", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
                        == DialogResult.Yes)
                        continue;
                    else
                        return;
                }
                //break;
            }

            // vse ok ... tak se konci ...
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateForm();
        }

        private void miHledatDleCK_Click(object sender, EventArgs e)
        {
            FindBarcode();
        }

        string findcode = string.Empty;
        private void FindBarcode()
        {
            try
            {
                ScannerStop();

                if (InputBox.Show("Zadejte čárový kód položky", findcode, out findcode) != DialogResult.OK)
                    return;

                findCode(findcode);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemFunkceHmotnost_Click(object sender, EventArgs e)
        {
            menuItemFunkceHmotnost.Checked = !menuItemFunkceHmotnost.Checked;
            Globals.Configuration.OnlineHmotnost = menuItemFunkceHmotnost.Checked;
        }

        private void menuItemFunkceLokace_Click(object sender, EventArgs e)
        {
            menuItemFunkceLokace.Checked = !menuItemFunkceLokace.Checked;
            Globals.Configuration.OnlineLokace = menuItemFunkceLokace.Checked;
        }

    }
}