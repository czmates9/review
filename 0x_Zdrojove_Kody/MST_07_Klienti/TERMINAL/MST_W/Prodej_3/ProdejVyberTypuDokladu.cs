using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Fask.MST_W.Forms;
using Fask.Graphic;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejVyberTypuDokladu : System.Windows.Forms.Form
    {
        private int cislodavky = 0;

        private Fask.SQLiteDBs.DataSets.TypDokladu typdokladuTable = null;
        private DataView typdokladuView = null;

        private DataGrid2TextBoxColumn docid;
        private DataGrid2TextBoxColumn docdesc;
        private DataGrid2TextBoxColumn doctyp;
        private DataGrid2TextBoxColumn docck;

        private Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row _typdokladu;
        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row TypDokladu
        {
            get { return _typdokladu; }
            set
            {
                _typdokladu = value;
                FindTypDokladuInView(value);
            }
        }

        private void FindTypDokladuInView(Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row value)
        {
            DataTable dt = typdokladuView.ToTable();
            DataRow[] rows = dt.Select("doc_id='" + value.doc_id + "'");
            if (rows.Length > 0)
            {
                dataGrid1.CurrentCell = new DataGridCell(dt.Rows.IndexOf(rows[0]), 0);
            }
            else
            {
                try
                {
                    dataGrid1.CurrentCell = new DataGridCell(0, 1);
                }
                catch { }
            }
        }

        private int selectedrowindex = 0;

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            try { dataGrid1.UnSelect(selectedrowindex); }
            catch { }

            try
            {
                selectedrowindex = dataGrid1.CurrentRowIndex;
                dataGrid1.Select(selectedrowindex);
            }
            catch { }

            UpdateForm();
        }

        private void UpdateForm()
        {
            _typdokladu = this.SelectedTypDokladu;
        }
        
        private Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row SelectedTypDokladu
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[typdokladuView].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row;
                }
                catch 
                {
                    return null;
                }
            }
        }

        private ProdejVyberTypuDokladu()
        {
            InitializeComponent();

            typdokladuTable = new Fask.SQLiteDBs.DataSets.TypDokladu();
            typdokladuView = new DataView(this.typdokladuTable.CZMST092);
            dataGrid1.DataSource = typdokladuView;

            InitializeDataGridView();

            MyInitializeGrid();
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }

        public ProdejVyberTypuDokladu(int cislodavky)
            : this()
        {
            this.cislodavky = cislodavky;
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = typdokladuTable.CZMST092.TableName;

            docid = new DataGrid2TextBoxColumn();
            docid.HeaderText = "ID";
            docid.MappingName = typdokladuTable.CZMST092.doc_idColumn.ColumnName;
            docid.NullText = "-";
            docid.Width = Settings.ProdejVyberTypDokladuDocIdWidth;
            ts.GridColumnStyles.Add(docid);

            docdesc = new DataGrid2TextBoxColumn();
            docdesc.HeaderText = "Popis";
            docdesc.MappingName = typdokladuTable.CZMST092.doc_descColumn.ColumnName;
            docdesc.NullText = "-";
            docdesc.Width = Settings.ProdejVyberTypDokladuDocDescWidth;
            ts.GridColumnStyles.Add(docdesc);

            doctyp = new DataGrid2TextBoxColumn();
            doctyp.HeaderText = "Typ";
            doctyp.MappingName = typdokladuTable.CZMST092.doc_typColumn.ColumnName;
            doctyp.NullText = "-";
            doctyp.Width = Settings.ProdejVyberTypDokladuDocTypWidth;
            ts.GridColumnStyles.Add(doctyp);

            docck = new DataGrid2TextBoxColumn();
            docck.HeaderText = "Èár. kód";
            docck.MappingName = typdokladuTable.CZMST092.doc_carcodeColumn.ColumnName;
            docck.NullText = "-";
            docck.Width = Settings.ProdejVyberTypDokladuDocCarKodWidth;
            ts.GridColumnStyles.Add(docck);

            dataGrid1.TableStyles.Add(ts);
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

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

        private delegate void StringDelegate(string carkod);

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            string ck = e.BarcodeData.Trim();
            if (ck != string.Empty)
                this.BeginInvoke(new StringDelegate(FindTypDokladuByBarcode), new object[] { (object)ck });
        }

        private void FindTypDokladuByBarcode(string carkod)
        {
            typdokladuView.RowFilter = string.Empty;
            if (carkod.Length > 0)
            {
                DataRow[] rows = typdokladuTable.CZMST092.Select("doc_carcode='" + carkod + "'");
                if (rows.Length == 1)
                {
                    this.TypDokladu = rows[0] as Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row;
                    PerformOK();
                }
                else if (rows.Length > 1)
                {
                    this.TypDokladu = rows[0] as Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row;
                    typdokladuView.RowFilter = "doc_carcode='" + carkod + "'";
                }
                else
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberTypuDokladuPolozkaCarKodNenalezena, carkod));
                }
            }
            if (MST_Global.OnScannerSound_Prodej_3)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            Cursor.Current = Cursors.Default;
            if (_typdokladu == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberTypuDokladuVyberteTypDokladu, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return;
            }
            this.ScannerFinalize();
            DialogResult = DialogResult.OK;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformCancel()
        {
            this.ScannerFinalize();
            DialogResult = DialogResult.Cancel;
        }

        private void ProdejVyberTypDokladu_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            panel1_Resize(null, null);

            timerLoad.Enabled = true;
            //ProdejVyberOdberatele_Shown(null, null);
        }

        private void ProdejTypDokladu_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            try
            {
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_typdokladu.Fill(typdokladuTable.CZMST092);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(@"Data\TypDokladu.sdf"))
                    MessageBoxBigTimeout.Show("Nenalzen èíselník 'TypDokladu.sdf'." + Environment.NewLine + "Pred pokraèovaním zaktualizujte èíselníky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                else
                    MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);


                //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                Logging.Log.Write(ex.Message, "ProdejVyberTypuDokladu_Shown");
            }

            // 1.zaznam z di, podivam se, jestli najdu odpovidajici zaznam z typu dokladu a pokud ano, tak ho nastavim/vyberu a pokracuji ... 
            try
            {
                var diFirstRow = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.CZMST_DI_GetFirstRecord();
                if (diFirstRow != null)
                {
                    var typdokladuFounded = typdokladuTable.CZMST092.Where(x => (x.doc_id == diFirstRow.DOC_ID) && (x.doc_id2 == diFirstRow.DOC_ID2));
                    if (typdokladuFounded.Count() > 0)
                    {
                        var typdokladu = typdokladuFounded.First();
                        TypDokladu = typdokladu;
                        PerformOK();
                        return;
                    }
                }
            }
            catch (Exception exTypDokladu)
            {
                Logging.Log.Write(exTypDokladu);
            }

            try { dataGrid1.CurrentCell = new DataGridCell(0, 1); }
            catch { }

			if (Settings.uia_prodej_VybratJeden)
			{
				if (typdokladuTable.CZMST092.Count == 1)
				{
					PerformOK();
					return;
				}
			}


			if (Settings.uia_prodej_VybratTD )
			{
				dataGrid1.UnSelectAll();

				List<DataRow> list = new List<DataRow>(1);
				var listtmp = typdokladuTable.CZMST092.Where(x => x.doc_id.Trim() == Settings.uia_prodej_docidTD.Trim() && x.doc_id2.Trim() == Settings.uia_prodej_docid2TD.Trim());
				foreach (Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row item in listtmp)
				{
					//list.Add(item);
					dataGrid1.CurrentRow = item;
					//dataGrid1.SelectedRowsSet(list);
					break;
				}
			}

            this.ScannerStart();
            Cursor.Current = Cursors.Default;
        }

        private void ProdejTypDokladu_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            ScannerFinalize();
        }

        private void ProdejTypDokladu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.F1)
            {
                najdiPolozkuCarovyKod();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void menuItemNajit_Click(object sender, EventArgs e)
        {
            najdiPolozkuCarovyKod();
        }

        private void najdiPolozkuCarovyKod()
        {
            try
            {
                this.ScannerStop();

                string ck = string.Empty;

                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejVyberTypuDokladuVlozteCarovyKod, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
                    ck = skf.Kod;
                }

                this.FindTypDokladuByBarcode(ck);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2, panel1.Height);
            buttonStorno.Width = nsize.Width;
            buttonOK.Width = nsize.Width;
        }

        private void menuItemAktualize_Click(object sender, EventArgs e)
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogTypDokladu(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);

            this.ProdejTypDokladu_Shown(null, null);
        }

        private void ProdejVyberTypuDokladu_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ProdejVyberTypuDokladu_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}