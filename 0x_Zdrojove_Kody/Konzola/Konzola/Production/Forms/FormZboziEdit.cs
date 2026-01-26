using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Globalization;

namespace Production.Forms
{
    public partial class FormZboziEdit : Form
    {
        /// <summary>
        /// Zboží, který se má upravit.
        /// </summary>
        public Production.DataServices.KonzolaDataSet.FASK_CONS_095Row zbozirow { get; set; }
        /// <summary>
        /// Uživatel, který zboží upravuje
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Konzola.FASK_CONS_LoginsRow loginrow { get; set; }
        public FormZboziEdit()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                // je úprava záznamu
                if (zbozirow != null)
                {
                    zbozirow.ITEMDESC = textBoxNazevPolozky.Text.Trim();
                    zbozirow.CZ_CarKod = textBoxCarovyKodPolozky.Text.Trim();
                    zbozirow.QTYPACK = Convert.ToDecimal(textBoxMnozstviVBaleni.Text);
                    zbozirow.loginid = loginrow != null ? loginrow.id : string.Empty;
                    zbozirow.MJ = textBoxMernaJednotka.Text.Trim();
                    zbozirow.LSTMod = DateTime.Now;
                    zbozirow.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    zbozirow.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    zbozirow.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                }
                else   // nový záznam
                {
                    //DataServices.VyrobaDataSet.LoginsDataTable loginsdatatable = new DataServices.VyrobaDataSet.LoginsDataTable();
                    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim());
                    DataServices.KonzolaDataSet dataset = new DataServices.KonzolaDataSet();
                    
                    DataServices.KonzolaDataSet.FASK_CONS_095Row newZboziRow = dataset.FASK_CONS_095.NewFASK_CONS_095Row();
                    newZboziRow.ITEMNMBR = textBoxCisloPolozky.Text.Trim();
                    newZboziRow.ITEMDESC = textBoxNazevPolozky.Text.Trim();
                    newZboziRow.CZ_CarKod = textBoxCarovyKodPolozky.Text.Trim();
                    newZboziRow.QTY = 0;
                    newZboziRow.QTYPACK = Convert.ToDecimal(textBoxMnozstviVBaleni.Text);
                    newZboziRow.MJ = textBoxMernaJednotka.Text.Trim(); ;
                    newZboziRow.CZ_SerNum_Track = 0;
                    newZboziRow.CZ_SerNum_Delka = 0;
                    newZboziRow.TIMEFROM = DateTime.Now;
                    newZboziRow.TIMETO = DateTime.Now;
                    newZboziRow.LSTMod = DateTime.Now;
                    newZboziRow.loginid = loginrow != null ? loginrow.id : string.Empty;
                    newZboziRow.VNDITNUM = string.Empty;
                    newZboziRow.LOCNCODE = string.Empty;
                    newZboziRow.SKL_ID = string.Empty;
                    newZboziRow.DMJ = string.Empty;
                    newZboziRow.TAXRATE = 21;
                    newZboziRow.PRICE0 = 0;
                    newZboziRow.PRICE1 = 0;
                    newZboziRow.PRICE2 = 0;
                    newZboziRow.PRICE3 = 0;
                    newZboziRow.PRICE4 = 0;
                    newZboziRow.PRICE5 = 0;
                    newZboziRow.REZ1 = string.Empty;
                    newZboziRow.ITEMCODE = string.Empty;
                    newZboziRow.ODB_ID = string.Empty;
                    newZboziRow.TIMEFROM = DateTime.Now;
                    newZboziRow.TIMETO = DateTime.Now;
                    newZboziRow.LSTMod = DateTime.Now;
                    newZboziRow.CZ_Rez1_Track = 0;
                    newZboziRow.CZ_Rez2_Track = 0;
                    newZboziRow.CZ_Rez3_Track = 0;
                    newZboziRow.CZ_Rez4_Track = 0;
                    newZboziRow.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    newZboziRow.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    newZboziRow.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    
                    zbozirow = newZboziRow;
                    //lta.Insert(
                    //    newZboziRow.ITEMNMBR,
                    //    newZboziRow.ITEMDESC,
                    //    newZboziRow.VNDITNUM,
                    //    newZboziRow.CZ_CarKod,
                    //    newZboziRow.LOCNCODE,
                    //    newZboziRow.SKL_ID,
                    //    newZboziRow.QTY,
                    //    newZboziRow.QTYPACK,
                    //    newZboziRow.MJ,
                    //    newZboziRow.DMJ,
                    //    newZboziRow.TAXRATE,
                    //    newZboziRow.PRICE0,
                    //    newZboziRow.PRICE1,
                    //    newZboziRow.PRICE2,
                    //    newZboziRow.PRICE3,
                    //    newZboziRow.PRICE4,
                    //    newZboziRow.PRICE5,
                    //    newZboziRow.CZ_SerNum_Track,
                    //    newZboziRow.CZ_SerNum_Delka,
                    //    newZboziRow.CZ_Rez1_Track,
                    //    newZboziRow.CZ_Rez2_Track,
                    //    newZboziRow.CZ_Rez3_Track,
                    //    newZboziRow.CZ_Rez4_Track,
                    //    newZboziRow.REZ1,
                    //    newZboziRow.ITEMCODE,
                    //    newZboziRow.ODB_ID,                        
                    //    newZboziRow.TIMEPREP,
                    //    newZboziRow.TIMEUNIT,
                    //    newZboziRow.TIMEFROM,
                    //    newZboziRow.TIMETO,
                    //    newZboziRow.LSTMod,
                    //    newZboziRow.loginid,
                    //    newZboziRow.TIMEMODE );

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
                if (string.IsNullOrEmpty(textBoxCisloPolozky.Text.Trim()))
                    throw new Exception("Musíte zadat číslo položky");

                if (zbozirow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter adapter = new DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                    adapter.Connection.ConnectionString = Globals.ConnectionString;
                    DataServices.KonzolaDataSet.FASK_CONS_095DataTable zbozidatatable = new DataServices.KonzolaDataSet.FASK_CONS_095DataTable();
                    adapter.Fill(zbozidatatable);

                    var zbozi = zbozidatatable.Where(x => x.ITEMNMBR.Trim() == textBoxCisloPolozky.Text.Trim());
                    if (zbozi.Count() > 0)
                    {
                        textBoxCisloPolozky.Focus();
                        textBoxCisloPolozky.SelectAll();
                        throw new Exception("Položka s tímto číslem již existuje");
                    }
                }

                if (string.IsNullOrEmpty(textBoxNazevPolozky.Text.Trim()))
                {
                    textBoxNazevPolozky.Focus();
                    throw new Exception("Musíte zadat název položky");
                }
                if (string.IsNullOrEmpty(textBoxCarovyKodPolozky.Text.Trim()))
                {
                    textBoxCarovyKodPolozky.Focus();
                    throw new Exception("Musíte zadat čárový kód položky");
                }
                if (string.IsNullOrEmpty(textBoxMnozstviVBaleni.Text.Trim()))
                {
                    textBoxMnozstviVBaleni.Focus();
                    throw new Exception("Musíte množství v balení");
                }

                decimal qutypack;
                bool status = Decimal.TryParse(textBoxMnozstviVBaleni.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out qutypack);
                if (!status)
                {
                    textBoxMnozstviVBaleni.Focus();
                    textBoxMnozstviVBaleni.SelectAll();
                    throw new Exception("Množství v balení musí být číslo");
                }

                if (qutypack < 0)
                {
                    textBoxMnozstviVBaleni.Focus();
                    textBoxMnozstviVBaleni.SelectAll();
                    throw new Exception("Požadované množství v balení musí být větší než 0");
                }

                //if(string.IsNullOrEmpty(textBoxMernaJednotka.Text.Trim()))
                //{
                //    textBoxMernaJednotka.Focus();
                //    textBoxMernaJednotka.SelectAll();
                //    throw new Exception("Měrná jednotka musí být vyplněna");                    
                //}

                if (textBoxMernaJednotka.Text.Trim().Length > 5)
                {
                    textBoxMernaJednotka.Focus();
                    textBoxMernaJednotka.SelectAll();
                    throw new Exception("Měrná jednotka může mít maximálně 5 znaků");                    
                }
                
                float timeprep;
                status = float.TryParse(textBoxJednotkovyCas.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeprep);
                if (!status)
                {
                    textBoxJednotkovyCas.Focus();
                    throw new Exception("Jednotkový čas musí být číslo");
                }

                if (timeprep < 0)
                {
                    textBoxJednotkovyCas.Focus();
                    textBoxJednotkovyCas.SelectAll();
                    throw new Exception("Jednotkový čas musí být větší, nebo rovno 0");
                }

                status = float.TryParse(textBoxPripravnyCas.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeprep);
                if (!status)
                {
                    textBoxPripravnyCas.Focus();
                    throw new Exception("Přípravný čas musí být číslo");
                }

                if (timeprep < 0)
                {
                    textBoxPripravnyCas.Focus();
                    textBoxPripravnyCas.SelectAll();
                    throw new Exception("Přípravný čas musí být větší, nebo rovno 0");
                }

                if (comboBoxTypSledovaniCasu.SelectedIndex == -1)
                    throw new Exception("Zvolte typ sledování času");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FormZboziEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void FormZboziEdit_Shown(object sender, EventArgs e)
        {            
        }

        private void FormZboziEdit_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            FormZboziEdit_Resize(null, null);
            // naplnění combo boxu sledování času
            comboBoxTypSledovaniCasu.Items.Add("Stop výroby");
            comboBoxTypSledovaniCasu.Items.Add("Start/Stop výroby");
            comboBoxTypSledovaniCasu.Items.Add("Start/Stop příprava, start/stop výroba");

            comboBoxTypSledovaniCasu.SelectedItem = null;

            // je úprava záznamu, dojde k načtení dat
            if (zbozirow != null)
            {
                textBoxCisloPolozky.Enabled = false;
                textBoxCisloPolozky.Text = zbozirow.ITEMNMBR.Trim();
                textBoxNazevPolozky.Text = zbozirow.ITEMDESC.Trim();
                textBoxCarovyKodPolozky.Text = zbozirow.CZ_CarKod.Trim();
                textBoxMnozstviVBaleni.Text = zbozirow.QTYPACK.ToString().Trim();
                comboBoxTypSledovaniCasu.SelectedIndex = zbozirow.TIMEMODE;
                //textBoxPripravnyCas.Text = zbozirow.TIMEPREP.ToString(CultureInfo.InvariantCulture.NumberFormat);
                textBoxPripravnyCas.Text = zbozirow.TIMEPREP.ToString(CultureInfo.InvariantCulture);
                textBoxJednotkovyCas.Text = zbozirow.TIMEUNIT.ToString(CultureInfo.InvariantCulture);
                textBoxMernaJednotka.Text = zbozirow.MJ.Trim();
            }
        }

        private void FormZboziEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }
    }
}
