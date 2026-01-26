using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using System.IO;

namespace Fask.Aktualizace_API.Tisk
{
    public partial class FormSablonaVyber : Form
    {

        public void TiskSablonaSet(string itemtype)
        {
            try
            {
                sablonyBindingSource.Filter = "ITEMTYPE='" + itemtype.Trim() + "'";

                int index = sablonyBindingSource.Find(tiskSablony.Sablony.ITEMTYPEColumn.ColumnName, itemtype);
                sablonyBindingSource.Position = index;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Fask.SQLiteDBs.DataSets.TiskSablony.SablonyRow TiskSablona
        {
            get
            {
                try
                {
                    return (sablonyBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.TiskSablony.SablonyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.SQLiteDBs.DataSets.TiskSablony TiskSablony
        {
            get { return tiskSablony; }
        }

        public FormSablonaVyber()
        {
            InitializeComponent();

            LoadTiskSablony();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormBaseButtonOKStorno_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
        }

        public void LoadTiskSablony()
        {
            tiskSablony.ReadXml(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Settings.ConfigTiskSablony));
        }

        private void FormBaseButtonOKStorno_KeyDown(object sender, KeyEventArgs e)
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
            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
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
    }
}

