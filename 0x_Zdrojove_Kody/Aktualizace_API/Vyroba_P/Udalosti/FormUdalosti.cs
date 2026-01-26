using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Udalosti
{
    public partial class FormUdalosti : System.Windows.Forms.Form
    {

        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.StatusTypesTableAdapter stta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.StatusTypesTableAdapter();
        private Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesDataTable stdt = new Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesDataTable();
        //private DataView dvStatusTypes = null;

        public Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesRow SelectedStatusTypesRow
        {
            get
            {
                //return ((DataRowView)(dataGrid1.BindingContext[dataGrid1.DataSource].Current)).Row as Data.VyrobaCEDataSet.StatusTypesRow;
                try
                {
                    return (statusTypesBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesRow;
                }
                catch 
                {
                    return null;
                }
            }
        }

        public bool FindStatusTypesRowByStatuID(string statusid)
        {
            //DataTable tmpdt = dvStatusTypes.ToTable(false, new string[] { stdt.statusidColumn.ColumnName });
            //DataRow[] tmprows = tmpdt.Select(stdt.statusidColumn.ColumnName + "='" + statusid + "'");

            //if (tmprows.Length > 0)
            //{
            //    dataGrid1.CurrentRowIndex = tmpdt.Rows.IndexOf(tmprows[0]);
            //}

            int index = statusTypesBindingSource.Find("statusid", statusid);
            statusTypesBindingSource.Position = index;

            return index > 0;
        }

        public FormUdalosti()
        {
            InitializeComponent();

            //stta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_StatusTypes(stdt);
            //dvStatusTypes = new DataView(stdt);

            //dataGrid1.DataSource = dvStatusTypes;
            statusTypesBindingSource.DataSource = stdt;

            FindStatusTypesRowByStatuID(Settings.UEventPracovnikPrihlaseni);

            InitializeDataGridTableStyle();
        }

        //private DataGridTextBoxColumn dgcolstatusdesc = null;
        //private DataGridTextBoxColumn dgcolstatusid = null;

        private void InitializeDataGridTableStyle()
        {
            //DataGridTableStyle dgts = new DataGridTableStyle();
            //dgts.MappingName = stdt.TableName;

            //dgcolstatusdesc = new DataGridTextBoxColumn();
            //dgcolstatusdesc.MappingName = stdt.statusdescColumn.ColumnName;
            //dgcolstatusdesc.Width = Settings.UdalostiStatusDescWidth;
            //dgcolstatusdesc.HeaderText = "Událost";
            //dgts.GridColumnStyles.Add(dgcolstatusdesc);

            //dgcolstatusid = new DataGridTextBoxColumn();
            //dgcolstatusid.MappingName = stdt.statusidColumn.ColumnName;
            //dgcolstatusid.Width = Settings.UdalostiStatusIDWidth;
            //dgcolstatusid.HeaderText = "ID";
            //dgts.GridColumnStyles.Add(dgcolstatusid);

            //dataGrid1.TableStyles.Add(dgts);
            //dataGrid1.RowHeaderWidth = Settings.UdalostiStatusRowHeight;
        }

        private void FormUdalosti_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //// TODO: This line of code loads data into the 'vyrobaCEDataSet.StatusTypes' table. You can move, or remove it, as needed.
            //this.statusTypesTableAdapter.Fill(this.vyrobaCEDataSet.StatusTypes);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            ScannerStart();
            //dataGrid1.Focus();
            dataGridView1.Focus();
        }

        private void dataGrid1_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            //Data.VyrobaCEDataSet.StatusTypesRow strow = SelectedStatusTypesRow;
            //if (strow == null)
            //    textBoxKod.Text = string.Empty;
            //else
            //    textBoxKod.Text = strow.statusid;
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            string carkod = e.BarcodeData.Trim();
            if (carkod.Length == 0)
                return;

            //this.Kod = e.BarcodeData.Trim();
            if (FindStatusTypesRowByStatuID(carkod))
                this.PerformOK();
            else
                FlexibleMessageBox.Show(this, "Událost s ID '" + carkod + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); 
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void SettingsSave()
        {
            //Settings.UdalostiStatusDescWidth = dgcolstatusdesc.Width;
            //Settings.UdalostiStatusIDWidth = dgcolstatusid.Width;
            //Settings.UdalostiStatusRowHeight = //dataGrid1.RowHeaderWidth;
        }

        private void finalize()
        {
            ScannerStop();
            SettingsSave();
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormUdalosti_KeyDown(object sender, KeyEventArgs e)
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
    }
}

