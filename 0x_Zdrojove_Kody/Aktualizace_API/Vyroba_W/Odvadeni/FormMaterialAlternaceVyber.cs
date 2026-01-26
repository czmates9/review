using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.Forms;
using System.Linq;

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormMaterialAlternaceVyber : Form
    {

        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta_cons_095;

        //private Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();
        private Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable _dt_Material = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_Material
        {
            set {
                //vyrobaCEDataSet.FASK_CONS_095 = value;
                _dt_Material = value;
                foreach (var item in _dt_Material)
                {
                    vyrobaCEDataSet.FASK_CONS_095.ImportRow(item);
                }
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow[] alternativy = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow[] Alternativy
        {
            set
            {
                alternativy = value;
                // naplneni z dat ciselniku zbozi ... 
                vyrobaCEDataSet.FASK_CONS_095.Clear();
                //ta_cons_095.ClearBeforeFill = false;
                foreach (var alt in alternativy)
                {
					Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByITEMNMBR_CZMST_095(false,vyrobaCEDataSet.FASK_CONS_095, alt.ITEMNMBR_fol);
                }
            }
        }

        public FormMaterialAlternaceVyber()
        {
            InitializeComponent();
            //TaD
            dataGrid1.Load(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString()));
            dataGrid1.BackColor = Color.PaleGreen;

            //ta_cons_095 = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
			//ta_cons_095.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

        }

        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow AlternativaSelected
        {
            get
            {
                var alt = alternativy.Where(x => x.ITEMNMBR_fol == MaterialSelected.ITEMNMBR.Trim());
                if (alt.Count() == 0)
                    return null;
                else
                    return alt.First();
            }
        }

        //TaD
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row MaterialSelected
        {
            get
            {
                try
                {
                    return ((DataRowView)this.fASKCONS095BindingSource.Current).Row as Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row;

                }
                catch(Exception ex)
                {
                    return null;
                }
            }
            set
            {
                //((DataRowView)this.productionSourcesBindingSource.Current).
                //this.productionSourcesBindingSource
                int index = ((DataView)this.fASKCONS095BindingSource.List).Table.Rows.IndexOf(value);
                if (index > 0)
                {
                    int i = this.dataGrid1.CurrentRowIndex;
                    this.fASKCONS095BindingSource.Position = index;
                    this.dataGrid1.UnSelect(i);
                    this.dataGrid1.Select(index);
                }
            }
        }


        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormMaterialAlternaceVyber_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //TaD
            //fASKCONS095BindingSource.DataSource = _dt_Material;

            panelButtons_Resize(null, null);

            ScannerStart();
        }

        private void finalize()
        {
            this.ScannerFinalize();
            //TaD
            dataGrid1.Save(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString()));
           
        }

        private void FormMaterialAlternaceVyber_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        public void PerformOK()
        {
            //TaD
            if (this.MaterialSelected == null)
            {
                MessageBox.Show("Není vybrán materiál", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }

            this.finalize();
            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void menuItemAkceOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void menuItemAkceStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            HledejCarkod();
        }


        private void HledejCarkod()
        {
            try
            {
                ScannerStop();
                string bkod = string.Empty;

                using (FormInputKod fik = new FormInputKod())
                {
                    fik.Text = "Èár. kód operace";
                    if (DialogResult.Cancel == fik.ShowDialog())
                        return;

                    bkod = fik.Kod;
                }

                PridejMaterial(bkod);
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
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void ScannerEventHandlerMethod(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            try
            {
                
                ScannerStop();

                string bcode = e.BarcodeData.Trim();

                if (bcode.Length == 0)
                    return;

                PridejMaterial(bcode);

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void PridejMaterial(string bcode)
        {
            // akce s pridanim materialu ...
            var materialy = vyrobaCEDataSet.FASK_CONS_095.Where(x => x.CZ_CarKod.Trim() == bcode.Trim() ||
                                                    x.VNDITNUM.Trim() == bcode.Trim()
                                                    );

            if (materialy.Count() == 1) 
            {
                this.MaterialSelected = materialy.First();
                PerformOK();
                return;
            }
            else if(materialy.Count() < 1)
                MessageBox.Show("Èárový kód materiálu :'" + bcode + "'./n Nenalezen.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else if (materialy.Count() > 1)
            {
                MessageBox.Show("Èárový kód materiálu :'" + bcode + "'./n Nalezeno veší množství.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                fASKCONS095BindingSource.DataSource = materialy;
            }
        }
        #endregion


    }
}

