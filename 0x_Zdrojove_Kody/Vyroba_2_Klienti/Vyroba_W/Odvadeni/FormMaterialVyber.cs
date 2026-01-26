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
    public partial class FormMaterialVyber : Form
    {

        //private Fask.Vyroba_W.Data.VyrobaCEDataSet.FASK_CONS_095DataTable zboziDatatable = new Fask.Vyroba_W.Data.VyrobaCEDataSet.FASK_CONS_095DataTable();
		private Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable _zboziDatatable = null;
		public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable ZboziDatatable
        {
            set { _zboziDatatable = value; }
        }

        public FormMaterialVyber()
        {
            InitializeComponent();
            //TaD
            dataGrid1.Load(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString()));
            dataGrid1.BackColor = Color.PaleGreen;
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
                catch
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

        private void FormMaterialVyber_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //TaD
            fASKCONS095BindingSource.DataSource = _zboziDatatable;

            panelButtons_Resize(null, null);

            
        }

        private void finalize()
        {
            //TaD
            dataGrid1.Save(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString())); 
        }

        private void FormMaterialVyber_KeyDown(object sender, KeyEventArgs e)
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

    }
}

