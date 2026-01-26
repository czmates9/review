using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Vyroba_P.Tisk
{
    public partial class FormDictionaryModify : Form
    {
        private System.Collections.Generic.Dictionary<string, object> dictParamsSablona = null;
        private System.Collections.Generic.Dictionary<string, object> dictParams = null;

        private FormDictionaryModify()
        {
            InitializeComponent();
        }

        public FormDictionaryModify(
            System.Collections.Generic.Dictionary<string, object> dictParamsSablona,
            System.Collections.Generic.Dictionary<string, object> dictParams)
            : this()
        {
            this.dictParams = dictParams;
            this.dictParamsSablona = dictParamsSablona;

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            foreach (string key in dictParamsSablona.Keys)
            {
                dataGridView1.Rows.Add(new object[] { key, dictParamsSablona[key] });
            }
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

            CheckInputValues();
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
            try
            {
                if (!CheckInputValues())
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }           

            this.DialogResult = DialogResult.OK;
        }

        private bool CheckInputValues()
        {
            foreach (DataGridViewRow dvrow in dataGridView1.Rows)
            {
                if (dvrow.Cells["colHodnota"].Value == null || dvrow.Cells["colHodnota"].Value.ToString().Trim() == string.Empty)
                {
                    dataGridView1.Focus();
                    dataGridView1.CurrentCell = dvrow.Cells["colHodnota"];
                    return false;
                }
                dictParamsSablona[(string)dvrow.Cells["colKlic"].Value] = dvrow.Cells["colHodnota"].Value;
            }
            return true;
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

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow dvrow in dataGridView1.Rows)
            {
                if (dvrow.Cells["colHodnota"].Value == null || dvrow.Cells["colHodnota"].Value.ToString().Trim() == string.Empty)
                    dvrow.DefaultCellStyle.BackColor = Color.MistyRose;
                else
                    dvrow.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
            }
        }
    }
}

