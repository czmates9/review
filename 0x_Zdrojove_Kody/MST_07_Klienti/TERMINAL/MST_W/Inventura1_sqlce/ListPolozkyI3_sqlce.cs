using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;

namespace Fask.MST_W.Inventura1_sqlce
{
    public partial class ListPolozkyI3_sqlce : System.Windows.Forms.Form
    {
        private Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable i3dt = null;
        public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable I3DT
        {
            set
            {
                this.i3dt = value; 
                dataGrid1.DataSource = this.i3dt;
            }
        }

        public void FindMJInView(string itemnmbr, string dmj)
        {
            if (String.IsNullOrEmpty(itemnmbr) || String.IsNullOrEmpty(dmj))
                return;

            if (this.i3dt == null)
                return;

            int index = 0;
            foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row rowi3 in this.i3dt)
            {
                if (itemnmbr == rowi3.ITEMNMBR && dmj.Contains(rowi3.MJ))
                {
                    index = this.i3dt.Rows.IndexOf(rowi3);
                    break;
                }
            }

            this.dataGrid1.CurrentRowIndex = index;
        }

        public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row I3Selected
        {
            get
            {
                Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row = null;
                try
                {
                    i3row = ((DataRowView)(dataGrid1.BindingContext[i3dt].Current)).Row as Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row;
                }
                catch { }
                return i3row;
            }
        }

        public ListPolozkyI3_sqlce()
        {
            InitializeComponent();
        }

        public ListPolozkyI3_sqlce(Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable i3dt)
            : this()
        {
            this.I3DT = i3dt;
        }

        private void menuItemStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void menuItemVyber_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void finalize()
        {
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PerformOK()
        {
            if (this.I3Selected == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkyI3SqlceNeniVybranZaznam, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            finalize();
            this.DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void ListPolozkyI3_sqlce_KeyDown(object sender, KeyEventArgs e)
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

        private void ListPolozkyI3_sqlce_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            MyInitializeGrid();            
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

    }
}