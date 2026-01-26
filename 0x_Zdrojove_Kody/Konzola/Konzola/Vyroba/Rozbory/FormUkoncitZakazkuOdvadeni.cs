using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;

namespace Konzola.Vyroba
{
    public partial class FormUkoncitZakazkuOdvadeni : Form
    {
        private PRODUCTIONTYPE _producttype;
        private enum PRODUCTIONTYPE
        {
            ZAKAZKA,
            KOREKCE
        }

        /// <summary>
        /// Uživatelem vybrané datum.
        /// </summary>
        public DateTime Datum
        {
            get
            {
                return dateTimePickerTIME.Value;
            }
            //set { }
        }

        public Decimal Mnozstvi
        {
            get
            {
                return Convert.ToDecimal(textBoxQty.Text);
            }
            //set { }
        }

        /// <summary>
        /// Záznam, kterému se bude přidávat ukončení.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow rowProduction { get; set; }

        public FormUkoncitZakazkuOdvadeni()
        {
            InitializeComponent();
        }

        private void FormVPHEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
                errorProvider1.Clear();
                bool status;
                // kontrola zakazek
                if (_producttype == PRODUCTIONTYPE.ZAKAZKA)
                {
                    // kontrola TIMESTOP
                    if (rowProduction.TIMESTART >= dateTimePickerTIME.Value)
                    {
                        errorProvider1.SetError(dateTimePickerTIME, "Čas ukončení zakázky musí být větší než čas zahájení zakázky");
                    }

                    // kontrola Qty
                    if (string.IsNullOrEmpty(textBoxQty.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxQty, "Počet kusů musí být vyplněn");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        decimal _qty;
                        status = Decimal.TryParse(textBoxQty.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxQty, "Počet kusů musí být číslo");
                        }
                        else
                        {
                            if (_qty < 0)
                            {
                                errorProvider1.SetError(textBoxQty, "Počet kusů musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }
                else
                {
                    if (_producttype == PRODUCTIONTYPE.KOREKCE)
                    {
                        // kontrola TIMECORSTOP
                        if (rowProduction.TIMECORSTART >= dateTimePickerTIME.Value)
                        {
                            errorProvider1.SetError(dateTimePickerTIME, "Čas ukončení korekce musí být větší než čas zahájení korekce");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void FormVPHEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormVPHEdit_Resize(null, null);

                //je úprava záznamu, dojde k načtení dat
                if (rowProduction != null)
                {                    
                    if (!rowProduction.IsTIMESTARTNull())
                    {
                        dateTimePickerTIMESTART.Value = rowProduction.TIMESTART;
                        _producttype = PRODUCTIONTYPE.ZAKAZKA;
                        textBoxQty.Text = rowProduction.qty.ToString();
                    }
                    else  // korekce, neni treba vyplnovat mnozstvi
                    {
                        if (!rowProduction.IsTIMECORSTARTNull())
                        {
                            dateTimePickerTIMESTART.Value = rowProduction.TIMECORSTART;
                            _producttype = PRODUCTIONTYPE.KOREKCE;
                            labelQty.Visible = false;
                            textBoxQty.Visible = false;
                            textBoxQty.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Zvolený záznam nelze dokončit", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Není zvolen záznam", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }            
        }

        private void FormVPHEdit_Shown(object sender, EventArgs e)
        {            
        }

        private void FormVPHEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }
    }
}
