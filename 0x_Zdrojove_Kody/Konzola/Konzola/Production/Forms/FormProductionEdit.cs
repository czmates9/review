using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;

namespace Production.Forms
{
    public partial class FormProductionEdit : Form
    {
        /// <summary>
        /// záznam, který se bude upravovat
        /// </summary>
        public Production.DataServices.VyrobaDataSet.ProductionRow rowProduct { get; set; }

        public FormProductionEdit()
        {
            InitializeComponent();
        }

        private void FormProductionEdit_Load(object sender, EventArgs e)
        {
            try 
	        {	        
		        textBoxId.Text = Globals.Pracovnik.id.Trim();
                textBoxJmeno.Text = Globals.Pracovnik.firstname + " " + Globals.Pracovnik.surname;
                textBoxMnozstviStare.Text = rowProduct.qty.ToString();
                FormProductionEdit_Resize(null, null);
            }
	        catch (Exception ex)
	        {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);                
	        }
        }

        private void FormProductionEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;
                rowProduct.qtyOld = rowProduct.qty;
                rowProduct.qty = Convert.ToDecimal(textBoxMnozstviNove.Text);
                rowProduct.dateedit = DateTime.Now;
                rowProduct.idVS = Globals.Pracovnik.id;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                decimal qty;
                bool status = Decimal.TryParse(textBoxMnozstviNove.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out qty);
                if (!status)
                {
                    textBoxMnozstviNove.Focus();
                    textBoxMnozstviNove.SelectAll();
                    throw new Exception("Nově zadané množství musí být číslo");
                }

                if (qty < 1)
                {
                    textBoxMnozstviNove.Focus();
                    textBoxMnozstviNove.SelectAll();
                    throw new Exception("Množství musí být větší než 0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FormProductionEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }
    }
}
