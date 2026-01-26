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
    public partial class FormVPHEdit : Form
    {
        /// <summary>
        /// hlavička, která se má upravit.
        /// </summary>
        public Production.DataServices.VyrobaDataSet.CZPRO_VPHRow VPHrow { get; set; }

        /// <summary>
        /// Hlavička, která byla vytvořena.
        /// </summary>
        public Production.DataServices.VyrobaDataSet.CZPRO_VPHRow VPHrowCreated { get; set; }
        /// <summary>
        /// Jedná se o duplikace, zobrazí se dotaz pro duplikování množství.
        /// </summary>
        public bool Duplikace = false;
        public bool DuplikovatMnozstvi = false;

        public FormVPHEdit()
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
                
                if (Duplikace)
                {
                    DialogResult dr = MessageBox.Show("Chcete duplikovat i množství? Pokud ne, bude veškeré množství nastaveno na hodnotu 0.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(dr == DialogResult.Yes)
                        DuplikovatMnozstvi = true;
                    else
                        DuplikovatMnozstvi = false;
                }

                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                // je úprava záznamu
                if (VPHrow != null)
                {
                    // TODO: zakázat změnu SOPNUMBE a CountEntries
                    //VPHrow.CountEntries = Convert.ToInt32(textBoxId.Text.Trim());
                    //VPHrow.SOPNUMBE = textBoxId.Text.Trim();
                    VPHrow.SOPDESC = textBoxPopisZakazky.Text.Trim();
                    VPHrow.BarcodeH = textBoxCarovyKod.Text.Trim();
                    lta.Update(VPHrow);
                }
                else   // nový záznam
                {
                    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim());
                    DataServices.VyrobaDataSet dataset = new DataServices.VyrobaDataSet();

                    DataServices.VyrobaDataSet.CZPRO_VPHRow newRow = dataset.CZPRO_VPH.NewCZPRO_VPHRow();
                    newRow.CountEntries = Convert.ToInt32(textBoxId.Text.Trim());
                    newRow.SOPNUMBE = textBoxId.Text.Trim();
                    newRow.SOPDESC = textBoxPopisZakazky.Text.Trim();
                    newRow.BarcodeH = textBoxCarovyKod.Text.Trim();
                    newRow.SOPTYPE = string.Empty;
                    newRow.VNDDOCNMH = string.Empty;
                    newRow.LOCNCODE = string.Empty;
                    newRow.DateProd = 15;
                    newRow.Rez1 = string.Empty;
                    newRow.Rez2 = string.Empty;
                    newRow.TermID = 0;
                    newRow.LSTMod = DateTime.Now;

                    VPHrowCreated = newRow;
                    lta.Insert(
                        newRow.CountEntries, 
                        newRow.SOPNUMBE, 
                        newRow.SOPTYPE, 
                        newRow.SOPDESC, 
                        newRow.VNDDOCNMH, 
                        newRow.BarcodeH, 
                        newRow.LOCNCODE, 
                        newRow.DateProd, 
                        newRow.Rez1, 
                        newRow.Rez2, 
                        newRow.TermID, 
                        newRow.LSTMod);
                }
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
                if (string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    throw new Exception("Musíte zadat číslo výrobní zakázky");

                Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter adapter = new DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                adapter.Connection.ConnectionString = Globals.ConnectionString;
                DataServices.VyrobaDataSet.CZPRO_VPHDataTable vphdatatable = new DataServices.VyrobaDataSet.CZPRO_VPHDataTable();
                adapter.Fill(vphdatatable);

                if (VPHrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id                    
                    var logins = vphdatatable.Where(x => x.SOPNUMBE.Trim() == textBoxId.Text.Trim());
                    if (logins.Count() > 0)
                    {
                        textBoxId.Focus();
                        textBoxId.SelectAll();
                        throw new Exception("Zadané číslo výrobní zakázky již existuje");
                    }

                    int sopnumbe;
                    bool status = Int32.TryParse(textBoxId.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out sopnumbe);
                    if (!status)
                    {
                        textBoxId.Focus();
                        textBoxId.SelectAll();
                        throw new Exception("Číslo výrobní zakázky musí být číslo");
                    }

                    if (sopnumbe < 1)
                    {
                        textBoxId.Focus();
                        textBoxId.SelectAll();
                        throw new Exception("Číslo výrobní zakázky musí být větší než 0");
                    }
                }

                //if (string.IsNullOrEmpty(textBoxPopisZakazky.Text.Trim()))
                //{
                //    textBoxPopisZakazky.Focus();
                //    throw new Exception("Musíte zadat popis zakázky");
                //}
                if (string.IsNullOrEmpty(textBoxCarovyKod.Text.Trim()))
                {
                    textBoxCarovyKod.Focus();
                    throw new Exception("Musíte zadat čárový kód");
                }

                // kontrola jedinečnosti čár. kódu
                var findBarcode = vphdatatable.Where(x => x.BarcodeH.Trim() == textBoxCarovyKod.Text.Trim());
                if (findBarcode.Count() > 0)
                {
                    // nový záznam
                    if (VPHrow == null)
                    {
                        textBoxCarovyKod.Focus();
                        textBoxCarovyKod.SelectAll();
                        throw new Exception("Výrobní příkaz s tímto čárovým kódem již existuje");
                    }
                    else
                    {  // editace stávajícího a čár. kód se liší od původního
                        if (textBoxCarovyKod.Text.Trim() != VPHrow.BarcodeH.Trim())
                        {
                            textBoxCarovyKod.Focus();
                            textBoxCarovyKod.SelectAll();
                            throw new Exception("Výrobní příkaz s tímto čárovým kódem již existuje");
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FormVPHEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormVPHEdit_Resize(null, null);

                //je úprava záznamu, dojde k načtení dat
                if (VPHrow != null)
                {
                    textBoxId.Enabled = false;
                    textBoxId.Text = VPHrow.SOPNUMBE.Trim();
                    textBoxPopisZakazky.Text = VPHrow.SOPDESC.Trim();
                    textBoxCarovyKod.Text = VPHrow.BarcodeH.Trim();
                }
                else textBoxId.Text = DateTime.Now.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Write(ex);
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

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            textBoxCarovyKod.Text = textBoxId.Text;
        }
    }
}
