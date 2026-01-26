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
    public partial class FormVPPEdit : Form
    {
        /// <summary>
        /// hlavička, která se má použít.
        /// </summary>
        public Production.DataServices.VyrobaDataSet.CZPRO_VPHRow rowVPH { get; set; }
        /// <summary>
        /// položka, která se má upravit.
        /// </summary>
        public Production.DataServices.VyrobaDataSet.CZPRO_VPPRow rowVPP { get; set; }

        private Production.DataServices.KonzolaDataSet.FASK_CONS_095Row row095
        {
            get
            {
                try
                {
                    return ((DataRowView)(comboBoxZbozi.SelectedItem)).Row as Production.DataServices.KonzolaDataSet.FASK_CONS_095Row;

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public FormVPPEdit()
        {
            InitializeComponent();
        }

        private void FormVPPEdit_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            FormVPPEdit_Resize(null, null);

            // naplnění combo boxu sledování času
            comboBoxTypSledovaniCasu.Items.Add("Stop výroby");
            comboBoxTypSledovaniCasu.Items.Add("Start/Stop výroby");
            comboBoxTypSledovaniCasu.Items.Add("Start/Stop příprava, start/stop výroba");

            // načtení zboží do combo boxu
            var adapter = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
            adapter.Connection.ConnectionString = Globals.ConnectionString;
            var dataASC = adapter.GetDataByITEMDESCasc();
            comboBoxZbozi.DataSource = dataASC;
            comboBoxZbozi.DisplayMember = "ITEMDESC";
            comboBoxZbozi.ValueMember = "ITEMNMBR";
        
            // je úprava záznamu, dojde k načtení dat
            if (rowVPP != null)
            {
                comboBoxZbozi.Enabled = false;
                comboBoxZbozi.SelectedValue = rowVPP.ITEMNMBR;
                textBoxCarovyKod.Text = rowVPP.BarcodeP;
                textBoxPozadovaneMnozstvi.Text = rowVPP.QTYSHPPD.ToString();
                comboBoxTypSledovaniCasu.SelectedIndex = rowVPP.TIMEMODE;
                textBoxCisloZbozi.Enabled = false;
                textBoxCisloZbozi.Text = rowVPP.ITEMNMBR.Trim();
                textBoxJednotkovyCas.Text = rowVPP.TIMEUNIT.ToString(CultureInfo.InvariantCulture);
                textBoxPripravnyCas.Text = rowVPP.TIMEPREP.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                comboBoxTypSledovaniCasu.SelectedItem = null;
                comboBoxZbozi.SelectedItem = null;
            }
        }

        private void FormVPPEdit_KeyDown(object sender, KeyEventArgs e)
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

                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                // je úprava záznamu
                if (rowVPP != null)
                {
                    rowVPP.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    rowVPP.BarcodeP = textBoxCarovyKod.Text.Trim();
                    rowVPP.QTYSHPPD = Convert.ToDecimal(textBoxPozadovaneMnozstvi.Text);
                    rowVPP.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    rowVPP.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    rowVPP.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);

                    lta.Update(rowVPP);
                    //VPHrow.CountEntries = Convert.ToInt32(textBoxId.Text.Trim());
                    //VPHrow.SOPNUMBE = textBoxId.Text.Trim();
                    //VPHrow.SOPDESC = textBoxPopisZakazky.Text.Trim();
                    //VPHrow.BarcodeH = textBoxCarovyKod.Text.Trim();
                    //lta.Update(VPHrow);
                }
                else   // nový záznam
                {
                    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim());
                    //var selectedZbozi = (Production.DataServices.KonzolaDataSet.FASK_CONS_095Row) comboBoxZbozi.SelectedItem;
                    // načtení zboží
                    Production.DataServices.KonzolaDataSet konzoladataset = new Production.DataServices.KonzolaDataSet();
                    var zboziTableAdapter = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                    zboziTableAdapter.Connection.ConnectionString = Globals.ConnectionString;
                    zboziTableAdapter.Fill(konzoladataset.FASK_CONS_095);
                    var dtZbozi = konzoladataset.FASK_CONS_095.Where(x => x.ITEMNMBR.Trim() == comboBoxZbozi.SelectedValue.ToString().Trim());


                    DataServices.VyrobaDataSet dataset = new DataServices.VyrobaDataSet();

                    DataServices.VyrobaDataSet.CZPRO_VPPRow newRow = dataset.CZPRO_VPP.NewCZPRO_VPPRow();
                    newRow.CountEntries = rowVPH.CountEntries;
                    newRow.SOPNUMBE = rowVPH.SOPNUMBE;
                    newRow.ITEMNMBR = dtZbozi.First().ITEMNMBR.Trim();      // id zboží
                    newRow.ITEMTYPE = string.Empty;
                    newRow.ITEMDESC = dtZbozi.First().ITEMDESC.Trim();      // popis zboží
                    newRow.ITEMMJ = dtZbozi.First().MJ.Trim();
                    //newRow.SetVNDDOCNMPNull();
                    newRow.VNDDOCNMP = "NULL";
                    newRow.VNDITNUM = "0";
                    newRow.ORD = 1;
                    //newRow.BarcodeP = dtZbozi.First().CZ_CarKod;        // čár. kód zboží
                    newRow.BarcodeP = textBoxCarovyKod.Text.Trim();        // čár. kód zboží
                    //newRow.SetLOCNCODENull();
                    newRow.LOCNCODE = string.Empty;
                    newRow.QTYSHPPD = Convert.ToDecimal(textBoxPozadovaneMnozstvi.Text.Trim());      // požadované množství
                    newRow.QTYDOKON = 0;
                    newRow.QTYPACK = dtZbozi.First().QTYPACK;
                    newRow.QTYPACKMJ = string.Empty;
                    newRow.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    newRow.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    newRow.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    newRow.DtProdT = 0;
                    newRow.DtProdL = 0;
                    newRow.SerNumT = 0;
                    newRow.SerNumL = 0;
                    newRow.VerT = 0;
                    newRow.VerL = 0;
                    newRow.TermID = 0;
                    newRow.LSTMod = DateTime.Now;

                    lta.Insert(
                        newRow.CountEntries, 
                        newRow.SOPNUMBE, 
                        newRow.ITEMNMBR, 
                        newRow.ITEMTYPE, 
                        newRow.ITEMDESC, 
                        newRow.ITEMMJ, 
                        newRow.VNDDOCNMP, 
                        newRow.VNDITNUM, 
                        newRow.ORD, 
                        newRow.BarcodeP, 
                        newRow.LOCNCODE, 
                        newRow.QTYSHPPD, 
                        newRow.QTYDOKON, 
                        newRow.QTYPACK, 
                        newRow.QTYPACKMJ, 
                        newRow.TIMEMODE, 
                        newRow.TIMEPREP, 
                        newRow.TIMEUNIT, 
                        newRow.DtProdT, 
                        newRow.DtProdL, 
                        newRow.SerNumT, 
                        newRow.SerNumL, 
                        newRow.VerT, 
                        newRow.VerL, 
                        newRow.TermID, 
                        DateTime.Now);
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
                if (comboBoxTypSledovaniCasu.SelectedIndex == -1)
                    throw new Exception("Zvolte typ sledování času");

                if (comboBoxZbozi.SelectedIndex == -1)
                    throw new Exception("Zvolte zboží, které chcete použít");

                if (string.IsNullOrEmpty(textBoxPozadovaneMnozstvi.Text.Trim()))
                {
                    textBoxPozadovaneMnozstvi.Focus();
                    textBoxPozadovaneMnozstvi.SelectAll();
                    throw new Exception("Musíte zadat požadované množství");
                }

                if (string.IsNullOrEmpty(textBoxCarovyKod.Text.Trim()))
                {
                    textBoxCarovyKod.Focus();
                    textBoxCarovyKod.SelectAll();
                    throw new Exception("Musíte zadat požadované čárový kód");
                }
                
                //vytváří se nový záznam, kontrola již existujícího záznamu
                //Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter adapter = new DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                //adapter.Connection.ConnectionString = Globals.ConnectionString;
                //DataServices.VyrobaDataSet.CZPRO_VPPDataTable vppdatatable = new DataServices.VyrobaDataSet.CZPRO_VPPDataTable();
                //adapter.Fill(vppdatatable);
                //if (rowVPP == null)
                //{
                //    var testExistence = vppdatatable.Where(x => (x.SOPNUMBE.Trim() == rowVPH.SOPNUMBE.Trim()) && (x.CountEntries == rowVPH.CountEntries) && (x.ITEMNMBR.Trim() == comboBoxZbozi.SelectedValue.ToString().Trim()));
                //    if (testExistence.Count() > 0)
                //    {
                //        throw new Exception("Zvolené zboží je již součástí zvoleného výrobního příkazu");
                //    }
                //}

                if (comboBoxZbozi.SelectedValue.ToString().Trim() != textBoxCisloZbozi.Text.Trim())
                {
                    textBoxCisloZbozi.Focus();
                    textBoxCisloZbozi.SelectAll();
                    throw new Exception("Čísla zboží se od sebe liší");
                }

                // testovani, zdali je jiz carovy kod v VPP
                Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter adapter = new DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                adapter.Connection.ConnectionString = Globals.ConnectionString;
                DataServices.VyrobaDataSet.CZPRO_VPPDataTable vppdatatable = new DataServices.VyrobaDataSet.CZPRO_VPPDataTable();
                adapter.Fill(vppdatatable);
                // kontrola, jestli zbozi jiz neni pridane (pouze u vytvareni noveho zaznamu)
                if (rowVPP == null)
                {
                    var testExistenceVPP = vppdatatable.Where(x => x.CountEntries == rowVPH.CountEntries && x.SOPNUMBE.Trim() == rowVPH.SOPNUMBE.Trim() && x.ITEMNMBR.Trim() == row095.ITEMNMBR.Trim());
                    if (testExistenceVPP.Count() > 0)
                    {
                        textBoxCisloZbozi.Focus();
                        textBoxCisloZbozi.SelectAll();
                        throw new Exception("Zvolené zboží již je součástí výrobního příkazu");
                    }
                }

                var testExistenceBarCode = vppdatatable.Where(x => (x.BarcodeP.Trim() == textBoxCarovyKod.Text.Trim()) && (x.CountEntries == rowVPH.CountEntries) && (x.SOPNUMBE.Trim() == rowVPH.SOPNUMBE.Trim()));
                if (testExistenceBarCode.Count() > 0)
                {
                    if (rowVPP == null)
                    {
                        textBoxCarovyKod.Focus();
                        textBoxCarovyKod.SelectAll();
                        throw new Exception("Čárový kód již existuje");
                    }
                    else
                    { //editace => hledat 
                        if (testExistenceBarCode.Where(f => f.ITEMNMBR.Trim() != rowVPP.ITEMNMBR.Trim()).Count() > 0)
                        {
                            textBoxCarovyKod.Focus();
                            textBoxCarovyKod.SelectAll();
                            throw new Exception("Čárový kód již existuje");
                        }
                    }
                }                

                decimal qutypack;
                bool status = Decimal.TryParse(textBoxPozadovaneMnozstvi.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out qutypack);
                if (!status)
                {
                    textBoxPozadovaneMnozstvi.Focus();
                    textBoxPozadovaneMnozstvi.SelectAll();
                    throw new Exception("Požadované množství musí být číslo");
                }

                if (qutypack < 0)
                {
                    textBoxPozadovaneMnozstvi.Focus();
                    textBoxPozadovaneMnozstvi.SelectAll();
                    throw new Exception("Požadované množství musí být větší, nebo rovno 0");
                }

                float timeprep;
                status = float.TryParse(textBoxJednotkovyCas.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeprep);
                if (!status)
                {
                    textBoxJednotkovyCas.Focus();
                    textBoxJednotkovyCas.SelectAll();
                    throw new Exception("Jednotkový čas musí být číslo");
                }

                if (timeprep < 0)
                {
                    textBoxJednotkovyCas.Focus();
                    textBoxJednotkovyCas.SelectAll();
                    throw new Exception("Jednotkový čas musí být větší, nebo rovn 0");
                }

                status = float.TryParse(textBoxPripravnyCas.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeprep);
                if (!status)
                {
                    textBoxPripravnyCas.Focus();
                    textBoxPripravnyCas.SelectAll();
                    throw new Exception("Přípravný čas musí být číslo");
                }

                if (timeprep < 0)
                {
                    textBoxPripravnyCas.Focus();
                    textBoxPripravnyCas.SelectAll();
                    throw new Exception("Přípravný čas musí být větší, nebo rovn 0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void FormVPPEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormVPPEdit_Shown(object sender, EventArgs e)
        {            
        }

        private void comboBoxZbozi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // je úprava záznamu
                if (rowVPP != null)
                    return;

                // automatické vyplnění dat podle navoleného druhu zboží
                if (comboBoxZbozi.SelectedIndex != -1)
                {
                    textBoxCarovyKod.Text = row095.CZ_CarKod.Trim();
                    textBoxCisloZbozi.Text = row095.ITEMNMBR.Trim();
                    textBoxJednotkovyCas.Text = row095.TIMEUNIT.ToString(CultureInfo.InvariantCulture);
                    textBoxPripravnyCas.Text = row095.TIMEPREP.ToString(CultureInfo.InvariantCulture);
                    comboBoxTypSledovaniCasu.SelectedIndex = row095.TIMEMODE;
                }
                else
                {
                    textBoxCarovyKod.Text = string.Empty;
                    textBoxCisloZbozi.Text = string.Empty;
                    textBoxJednotkovyCas.Text = string.Empty;
                    textBoxPripravnyCas.Text = string.Empty;
                    comboBoxTypSledovaniCasu.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void textBoxCisloZbozi_Leave(object sender, EventArgs e)
        {
            try
            {
                comboBoxZbozi.SelectedValue = textBoxCisloZbozi.Text.Trim();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
