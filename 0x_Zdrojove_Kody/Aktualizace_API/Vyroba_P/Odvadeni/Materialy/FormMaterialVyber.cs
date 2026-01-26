using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Fask.Aktualizace_API.Extensions;

namespace Fask.Aktualizace_API.Odvadeni.Materialy
{
    public partial class FormMaterialVyber : Form
    {
        
        private Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable _zboziDatatable = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable ZboziDatatable
        {
            set { _zboziDatatable = value; }
        }

        public FormMaterialVyber()
        {
            InitializeComponent();
            //TaD
            
            dataGridView1.BackColor = Color.PaleGreen;
        }

        //TaD

        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row MaterialSelected 
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[this.fASKCONS095BindingSource].Current)).Row as Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row;
                }
                catch
                {
                    return null;
                }
            }
        
        }


        private void FormMaterialVyber_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);

            this.dataGridView1.LoadConfiguration(this.GetType().ToString());

            fASKCONS095BindingSource.DataSource = _zboziDatatable;
            panelButtons_Resize(null, null);
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void finalize()
        {
            this.dataGridView1.SaveConfiguration(this.GetType().ToString());
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

            if (this.dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Je možné přidávat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                return;
            }

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

        private void toolStripMenuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void toolStripMenuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }



    }
}
