
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
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Vyroba
{
    public partial class FormVPPEdit : Form
    {

        private Fask.Interfaces.IMES providerVPP = null;

        /// <summary>
        /// hlavička, která se má použít.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow rowVPH { get; set; }
        /// <summary>
        /// položka, která se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow rowVPP { get; set; }

        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow SelectedRow { get; set; }
        

       

        public string ITEMNMBR = string.Empty;

        public FormVPPEdit()
        {
            InitializeComponent();
        }

        private void FormVPPEdit_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            FormVPPEdit_Resize(null, null);
            panelButtons_Resize(null, null);

            InitProvider();

            if (providerVPP == null)
                throw new Exception("Provider 'VPP' není inicializován");


            // naplnění combo boxu sledování času

            comboBoxTypSledovaniCasu.Items.Add(Constants.SV);
            comboBoxTypSledovaniCasu.Items.Add(Constants.SSV);
            comboBoxTypSledovaniCasu.Items.Add(Constants.SSPSSV);

            comboBoxTypSledovaniCasu.SelectedIndex = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VPPEdit_TIMEMODE;


            // je úprava záznamu, dojde k načtení dat
            if (rowVPP != null)
            {
                //comboBoxZbozi.Enabled = false;
                //comboBoxZbozi.SelectedValue = rowVPP.ITEMNMBR;
                TextBoxZbozi.Text = rowVPP.ITEMDESC;
                TextBoxZbozi.Enabled = false;
                textBoxCarovyKod.Text = rowVPP.BarcodeP;
                textBoxBarcodeT.Text = rowVPP.BarcodeT.ToString();

                textBoxPozadovaneMnozstvi.Text = rowVPP.QTYSHPPD.ToString();
                textBoxQTYPACK.Text = rowVPP.QTYPACK.ToString();
                comboBoxTypSledovaniCasu.SelectedIndex = rowVPP.TIMEMODE;

                textBoxCisloZbozi.Enabled = false;
                textBoxCisloZbozi.Text = rowVPP.ITEMNMBR.Trim();

                textBoxITEMCODE.Enabled = false;
                textBoxITEMCODE.Text = rowVPP.IsITEMCODENull() ? string.Empty : rowVPP.ITEMCODE.Trim();

                textBoxJednotkovyCas.Text = rowVPP.TIMEUNIT.ToString(CultureInfo.InvariantCulture);
                textBoxPripravnyCas.Text = rowVPP.TIMEPREP.ToString(CultureInfo.InvariantCulture);
                button1.Visible = false;
                tb_TypSledovani.Text = rowVPP.SerNumT.ToString();

                chb_CZ_Rez_1_Track.Checked = rowVPP.CZ_REZ1_Track > 0 ? true : false;
                chb_CZ_Rez_2_Track.Checked = rowVPP.CZ_REZ2_Track > 0 ? true : false;
                chb_CZ_Rez_3_Track.Checked = rowVPP.CZ_REZ3_Track > 0 ? true : false;
                chb_CZ_Rez_4_Track.Checked = rowVPP.CZ_REZ4_Track > 0 ? true : false;
                chb_CZ_Rez_5_Track.Checked = rowVPP.CZ_REZ5_Track > 0 ? true : false;

                tb_weight_tara.Text = rowVPP.IsWEIGHT_TARANull() ? string.Empty : rowVPP.WEIGHT_TARA.ToString(CultureInfo.InvariantCulture);
                tb_weight_netto.Text = rowVPP.IsWEIGHT_NETTONull() ? string.Empty : rowVPP.WEIGHT_NETTO.ToString(CultureInfo.InvariantCulture);
                tb_weight_tol_plus.Text = rowVPP.IsWEIGHT_TOL_PLUSNull() ? string.Empty : rowVPP.WEIGHT_TOL_PLUS.ToString(CultureInfo.InvariantCulture);
                tb_weight_tol_minus.Text = rowVPP.IsWEIGHT_TOL_MINUSNull() ? string.Empty : rowVPP.WEIGHT_TOL_MINUS.ToString(CultureInfo.InvariantCulture);

                tB_QTYPACKMJ.Text= rowVPP.IsQTYPACKMJNull() ? string.Empty : rowVPP.QTYPACKMJ;

            }
            else
            {
                //comboBoxTypSledovaniCasu.SelectedItem = null;
               // comboBoxZbozi.SelectedItem = null;
            }
        }

        private void FormVPPEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VPPEdit_TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void InitProvider()
        {
            #region providerVPP

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPP.IVPP).IsAssignableFrom(t))
                            {
                                providerVPP = (Fask.Interfaces.Vyroba.VPP.IVPP)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPP.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
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

        #region 9.1.2025 MaR
        private void PerformOK()
        {
            try
            {

                if (!ValidateData())
                    return;

                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                // je úprava záznamu
                if (rowVPP != null)
                {
                    ITEMNMBR = rowVPP.ITEMNMBR;
                    //rowVPP.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    rowVPP.BarcodeP = textBoxCarovyKod.Text.Trim();
                    rowVPP.QTYSHPPD = Convert.ToDecimal(textBoxPozadovaneMnozstvi.Text);
                    rowVPP.QTYPACK = Convert.ToDecimal(textBoxQTYPACK.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);

                    rowVPP.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    rowVPP.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    rowVPP.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);

                    rowVPP.SerNumT = byte.Parse(tb_TypSledovani.Text);

                    rowVPP.BarcodeT = byte.Parse(textBoxBarcodeT.Text);

                    rowVPP.CZ_REZ1_Track = chb_CZ_Rez_1_Track.Checked ? (byte)1 : (byte)0;
                    rowVPP.CZ_REZ2_Track = chb_CZ_Rez_2_Track.Checked ? (byte)1 : (byte)0;
                    rowVPP.CZ_REZ3_Track = chb_CZ_Rez_3_Track.Checked ? (byte)1 : (byte)0;
                    rowVPP.CZ_REZ4_Track = chb_CZ_Rez_4_Track.Checked ? (byte)1 : (byte)0;
                    rowVPP.CZ_REZ5_Track = chb_CZ_Rez_5_Track.Checked ? (byte)1 : (byte)0;

                    if (!string.IsNullOrEmpty(tb_weight_tara.Text))
                        rowVPP.WEIGHT_TARA = Convert.ToDecimal(tb_weight_tara.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        rowVPP.SetWEIGHT_TARANull();

                    if (!string.IsNullOrEmpty(tb_weight_netto.Text))
                        rowVPP.WEIGHT_NETTO = Convert.ToDecimal(tb_weight_netto.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        rowVPP.SetWEIGHT_NETTONull();

                    if (!string.IsNullOrEmpty(tb_weight_tol_plus.Text))
                        rowVPP.WEIGHT_TOL_PLUS = Convert.ToDecimal(tb_weight_tol_plus.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        rowVPP.SetWEIGHT_TOL_PLUSNull();

                    if (!string.IsNullOrEmpty(tb_weight_tol_minus.Text))
                        rowVPP.WEIGHT_TOL_MINUS = Convert.ToDecimal(tb_weight_tol_minus.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        rowVPP.SetWEIGHT_TOL_MINUSNull();


                    // MaR 10.2.2025
                    if (!string.IsNullOrEmpty(tB_QTYPACKMJ.Text))
                    {
                        if (tB_QTYPACKMJ.Text.Length > 5)
                        {
                            throw new Exception("Počet znaků MJ balení omezen na 5.");
                        }

                        rowVPP.QTYPACKMJ = tB_QTYPACKMJ.Text;
                    }
                    else
                    {
                        rowVPP.SetQTYPACKMJNull();
                    }





                    if ((providerVPP != null) && providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row)
                        ((Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row)providerVPP).VPP_Update_Row(rowVPP);
                    else
                        throw new Exception("IVPP_Update_Row not implementet");

                }
                else   // nový záznam
                {

                    if (SelectedRow == null)
                        return;

                    Fask.Interfaces.DataSets.Vyroba dataset = new Fask.Interfaces.DataSets.Vyroba();

                    Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow newRow = dataset.CZPRO_VPP.NewCZPRO_VPPRow();
                    newRow.CountEntries = rowVPH.CountEntries;
                    newRow.SOPNUMBE = rowVPH.SOPNUMBE;
                    newRow.ITEMNMBR = SelectedRow.ITEMNMBR.Trim();      // id zboží
                    ITEMNMBR = SelectedRow.ITEMNMBR.Trim();
                    newRow.ITEMTYPE = string.Empty;
                    newRow.ITEMDESC = SelectedRow.ITEMDESC.Trim();      // popis zboží
                    newRow.ITEMMJ = SelectedRow.MJ.Trim();
                    //newRow.SetVNDDOCNMPNull();
                    newRow.VNDDOCNMP = "NULL";

                    newRow.VNDITNUM = SelectedRow.IsVNDITNUMNull() ? string.Empty : SelectedRow.VNDITNUM.Trim();

                    newRow.ORD = 1;
                    //newRow.BarcodeP = selectedZbozi.CZ_CarKod;        // čár. kód zboží
                    newRow.BarcodeP = textBoxCarovyKod.Text.Trim();        // čár. kód zboží
                    //newRow.SetLOCNCODENull();
                    newRow.LOCNCODE = string.Empty;
                    newRow.QTYSHPPD = Convert.ToDecimal(textBoxPozadovaneMnozstvi.Text.Trim());      // požadované množství
                    newRow.QTYDOKON = 0;
                    newRow.QTYPACK = Convert.ToDecimal(textBoxQTYPACK.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);



                    ////newRow.QTYPACKMJ = string.Empty;
                    //if (!string.IsNullOrEmpty(tB_QTYPACKMJ.Text))
                    //    newRow.QTYPACKMJ = tB_QTYPACKMJ.Text;
                    //else
                    //    newRow.QTYPACKMJ = string.Empty; // newRow.SetQTYPACKMJNull();

                    // MaR 10.2.2025
                    if (!string.IsNullOrEmpty(tB_QTYPACKMJ.Text))
                    {
                        if (tB_QTYPACKMJ.Text.Length > 5)
                        {
                            throw new Exception("Počet znaků MJ balení omezen na 5.");
                        }

                        newRow.QTYPACKMJ = tB_QTYPACKMJ.Text;
                    }
                    else
                    {
                        newRow.SetQTYPACKMJNull();
                    }



                    newRow.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                    newRow.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    newRow.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    newRow.DtProdT = 0;
                    newRow.DtProdL = 0;
                    newRow.SerNumT = byte.Parse(tb_TypSledovani.Text); ;
                    newRow.SerNumL = 0;
                    newRow.VerT = 0;
                    newRow.VerL = 0;
                    newRow.TermID = 0;
                    newRow.LSTMod = DateTime.Now;
                    newRow.BarcodeT = byte.Parse(textBoxBarcodeT.Text);

                    newRow.SetRealization_StartNull();
                    newRow.SetRealization_StopNull();

                    newRow.CZ_REZ1_Track = chb_CZ_Rez_1_Track.Checked ? (byte)1 : (byte)0;
                    newRow.CZ_REZ2_Track = chb_CZ_Rez_2_Track.Checked ? (byte)1 : (byte)0;
                    newRow.CZ_REZ3_Track = chb_CZ_Rez_3_Track.Checked ? (byte)1 : (byte)0;
                    newRow.CZ_REZ4_Track = chb_CZ_Rez_4_Track.Checked ? (byte)1 : (byte)0;
                    newRow.CZ_REZ5_Track = chb_CZ_Rez_5_Track.Checked ? (byte)1 : (byte)0;

                    if (!string.IsNullOrEmpty(tb_weight_tara.Text))
                        newRow.WEIGHT_TARA = Convert.ToDecimal(tb_weight_tara.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        newRow.SetWEIGHT_TARANull();

                    if (!string.IsNullOrEmpty(tb_weight_netto.Text))
                        newRow.WEIGHT_NETTO = Convert.ToDecimal(tb_weight_netto.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        newRow.SetWEIGHT_NETTONull();

                    if (!string.IsNullOrEmpty(tb_weight_tol_plus.Text))
                        newRow.WEIGHT_TOL_PLUS = Convert.ToDecimal(tb_weight_tol_plus.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        newRow.SetWEIGHT_TOL_PLUSNull();

                    if (!string.IsNullOrEmpty(tb_weight_tol_minus.Text))
                        newRow.WEIGHT_TOL_MINUS = Convert.ToDecimal(tb_weight_tol_minus.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    else
                        newRow.SetWEIGHT_TOL_MINUSNull();

                    dataset.CZPRO_VPP.AddCZPRO_VPPRow(newRow);

                    if ((providerVPP != null) && providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Insert)
                    {
                        ((Fask.Interfaces.Vyroba.VPP.IVPP_Insert)providerVPP).VPP_Insert_Row(newRow);
                    }
                    else
                        throw new Exception("IVPP_Update_Row not implementet");


                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {

                // MaR 10.2.2025 logika validace kontrola dat UDI :)

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].kontrolaDatUDI)
                {
                    // pokud se kontrolují data UDI, tak se prvek řeší
                    if (!string.IsNullOrEmpty(tB_QTYPACKMJ.Text))
                    {
                        string udiValue = tB_QTYPACKMJ.Text.Trim();

                        // Očekáváme pouze jedno číslo (0–9)
                        if (udiValue.Length != 1 || !char.IsDigit(udiValue[0]))
                        {
                            throw new ArgumentException("Neplatný formát UDI kódu měrné jednotky balení. Očekává se pouze jedna číslice (0-9).");
                        }
                    }
                }





                if (comboBoxTypSledovaniCasu.SelectedIndex == -1)
                    throw new Exception("Zvolte typ sledování času");


                if (string.IsNullOrEmpty(textBoxQTYPACK.Text.Trim()))
                {
                    textBoxQTYPACK.Focus();
                    textBoxQTYPACK.SelectAll();
                    throw new Exception("Musíte zadat přepočtový koeficient");
                }

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
                    throw new Exception("Musíte zadat požadovaný čárový kód");
                }

                if (string.IsNullOrEmpty(textBoxBarcodeT.Text.Trim()))
                {
                    textBoxBarcodeT.Focus();
                    textBoxBarcodeT.SelectAll();
                    throw new Exception("Musíte zadat! Bud 0 anebo 1");
                }
                
                //rowVPP.BarcodeT = byte.Parse(tb_TypSledovani.Text);

                if (string.IsNullOrEmpty(TextBoxZbozi.Text.Trim()))
                {
                    TextBoxZbozi.Focus();
                    TextBoxZbozi.SelectAll();
                    throw new Exception("Musíte zadat název zboží.");
                }

                
                //vytváří se nový záznam, kontrola již existujícího záznamu
                //Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter adapter = new DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                //adapter.Connection.ConnectionString = Properties.Settings.Default.Production_ConncetionString;
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



                // testovani, zdali je jiz carovy kod v VPP
                //var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                //adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                Fask.Interfaces.DataSets.Vyroba vppdatatable = new Fask.Interfaces.DataSets.Vyroba();
                //adapter.Fill(vppdatatable);


                if ((providerVPP != null) && providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Fill)
                    ((Fask.Interfaces.Vyroba.VPP.IVPP_Fill)providerVPP).VPP_Fill(vppdatatable);
                else
                    throw new Exception("IVPP_Fill not implementet");

                // kontrola, jestli zbozi jiz neni pridane (pouze u vytvareni noveho zaznamu)
                if (rowVPP == null)
                {
                    var testExistenceVPP = vppdatatable.CZPRO_VPP.Where(x => x.CountEntries == rowVPH.CountEntries && x.SOPNUMBE.Trim() == rowVPH.SOPNUMBE.Trim() && x.ITEMNMBR.Trim() == textBoxCisloZbozi.Text.Trim());
                    if (testExistenceVPP.Count() > 0)
                    {
                        textBoxCisloZbozi.Focus();
                        textBoxCisloZbozi.SelectAll();
                        throw new Exception("Zvolené zboží již je součástí výrobního příkazu");
                    }
                }

                var testExistenceBarCode = vppdatatable.CZPRO_VPP.Where(x => (x.BarcodeP.Trim() == textBoxCarovyKod.Text.Trim()) && (x.CountEntries == rowVPH.CountEntries) && (x.SOPNUMBE.Trim() == rowVPH.SOPNUMBE.Trim()));
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

                decimal qtypack;
                bool statuss = Decimal.TryParse(textBoxQTYPACK.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out qtypack);
                if (!statuss)
                {
                    textBoxQTYPACK.Focus();
                    textBoxQTYPACK.SelectAll();
                    throw new Exception("Požadovaný koeficient musí být číslo");
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
            //Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            //buttonStorno.Size = nsize;
        }

        private void FormVPPEdit_Shown(object sender, EventArgs e)
        {            
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (Ciselniky.FormZboziList frmzbozi = new Ciselniky.FormZboziList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frmzbozi.Text = "Výběr zboží";

                    frmzbozi.VyhledatVLoad = true;
                    frmzbozi.Nastav_Filtr_ITEMNMBR = textBoxCisloZbozi.Text;
                    frmzbozi.Nastav_Filtr_ITEMDESC = TextBoxZbozi.Text;
                    frmzbozi.Nastav_Filtr_ITEMCODE = textBoxITEMCODE.Text;

                    if (frmzbozi.ShowDialog(this) != DialogResult.OK)
                        return;

                    SelectedRow = frmzbozi.FASK_ZASOBY_selectedRow;

                    TextBoxZbozi.Text = SelectedRow.IsITEMDESCNull() ? string.Empty : SelectedRow.ITEMDESC.Trim();
                    textBoxCisloZbozi.Text = SelectedRow.ITEMNMBR.Trim(); 
                    textBoxITEMCODE.Text = SelectedRow.IsITEMCODENull() ? string.Empty : SelectedRow.ITEMCODE.Trim(); 

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].VPPEdit_MOD_CarKod1)
                    {
                        textBoxCarovyKod.Text = SelectedRow.IsVNDITNUMNull() ? string.Empty : SelectedRow.VNDITNUM.Trim();
                    }
                    else if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].VPPEdit_MOD_CarKod2)
                    {
                        if (!SelectedRow.IsVNDITNUMNull() && !string.IsNullOrEmpty(SelectedRow.VNDITNUM))
                        {
                            textBoxCarovyKod.Text = SelectedRow.VNDITNUM.Trim();
                        }
                        else
                        {
                            textBoxCarovyKod.Text = SelectedRow.ITEMNMBR.Trim();
                        }
                    }

                    tb_TypSledovani.Text = SelectedRow.CZ_SerNum_Track.ToString();

                    textBoxJednotkovyCas.Text = SelectedRow.IsVPrTIMEUNITNull() ? "0" : SelectedRow.VPrTIMEUNIT.ToString(CultureInfo.InvariantCulture);
                    textBoxPripravnyCas.Text = SelectedRow.IsVPrTIMEPREPNull() ? "0" : SelectedRow.VPrTIMEPREP.ToString(CultureInfo.InvariantCulture);
                    comboBoxTypSledovaniCasu.SelectedIndex = SelectedRow.IsRefVPrTIMEMODENull() ? Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VPPEdit_TIMEMODE : SelectedRow.RefVPrTIMEMODE;

                    textBoxQTYPACK.Text = SelectedRow.IsQTYPACKNull() ? "0" : SelectedRow.QTYPACK.ToString(CultureInfo.InvariantCulture);

                    textBoxBarcodeT.Text = "1";





                    tB_QTYPACKMJ.Text = SelectedRow.DMJ;

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generovaní dle SN

        private void button_GenerateSN_Click(object sender, EventArgs e)
        {
            try
            {
                var gl = PerformOK_GenerateSN();

                if (gl == null)
                    return;

                gl.N = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].FormVyrobniPrikaz_VPP_GenerovaniSN_N;

                GenerateSN(ref gl);

                Cursor.Current = Cursors.WaitCursor;

                bwGenerovaniSN.RunWorkerAsync(gl);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                Cursor.Current = Cursors.Default;
            }
        }

        private void GenerateSN(ref Fask.Interfaces.Classes.GenerovaniSN gl)
        {

            try
            {
                using (FormVyrobniPrikaz_VPP_GenerovaniSN frmGen = new FormVyrobniPrikaz_VPP_GenerovaniSN(gl.QTYSHPPD))
                {
                    frmGen.Text = "Zadani parametru pro SN";

                    if (frmGen.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    gl.POCET = int.Parse(frmGen.Kod_Pocet);
                    gl.PREFIX = frmGen.KodPrefix;
                    gl.OD = frmGen.KodOd_Start;
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return;
            }

        }

        private Fask.Interfaces.Classes.GenerovaniSN PerformOK_GenerateSN()
        {
            Fask.Interfaces.Classes.GenerovaniSN gsn = new Fask.Interfaces.Classes.GenerovaniSN();

            try
            {
                if (!ValidateData())
                    return null;

                if (SelectedRow == null)
                    return null;

                gsn.CountEntries = rowVPH.CountEntries;
                gsn.SOPNUMBE = rowVPH.SOPNUMBE;
                gsn.ITEMNMBR = SelectedRow.ITEMNMBR.Trim();
                gsn.ITEMDESC = SelectedRow.ITEMDESC.Trim();
                gsn.ITEMMJ = SelectedRow.MJ.Trim();
                gsn.VNDITNUM = SelectedRow.IsVNDITNUMNull() ? string.Empty : SelectedRow.VNDITNUM.Trim();
                gsn.QTYSHPPD = Convert.ToDecimal(textBoxPozadovaneMnozstvi.Text.Trim());
                gsn.QTYPACK = Convert.ToDecimal(textBoxQTYPACK.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                gsn.TIMEMODE = comboBoxTypSledovaniCasu.SelectedIndex;
                gsn.TIMEPREP = float.Parse(textBoxPripravnyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                gsn.TIMEUNIT = float.Parse(textBoxJednotkovyCas.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                gsn.SerNumT = byte.Parse(tb_TypSledovani.Text);
                gsn.BarcodeT = byte.Parse(textBoxBarcodeT.Text);

                gsn.CZ_Rez1_Track = chb_CZ_Rez_1_Track.Checked ? (byte)1 : (byte)0;
                gsn.CZ_Rez2_Track = chb_CZ_Rez_2_Track.Checked ? (byte)1 : (byte)0;
                gsn.CZ_Rez3_Track = chb_CZ_Rez_3_Track.Checked ? (byte)1 : (byte)0;
                gsn.CZ_Rez4_Track = chb_CZ_Rez_4_Track.Checked ? (byte)1 : (byte)0;
                gsn.CZ_Rez5_Track = chb_CZ_Rez_5_Track.Checked ? (byte)1 : (byte)0;

                if (!string.IsNullOrEmpty(tb_weight_tara.Text))
                    gsn.WEIGHT_TARA = Convert.ToDecimal(tb_weight_tara.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                else
                    gsn.WEIGHT_TARA = null;

                if (!string.IsNullOrEmpty(tb_weight_netto.Text))
                    gsn.WEIGHT_NETTO = Convert.ToDecimal(tb_weight_netto.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                else
                    gsn.WEIGHT_NETTO = null;

                if (!string.IsNullOrEmpty(tb_weight_tol_plus.Text))
                    gsn.WEIGHT_TOL_PLUS = Convert.ToDecimal(tb_weight_tol_plus.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                else
                    gsn.WEIGHT_TOL_PLUS = null;

                if (!string.IsNullOrEmpty(tb_weight_tol_minus.Text))
                    gsn.WEIGHT_TOL_MINUS = Convert.ToDecimal(tb_weight_tol_minus.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                else
                    gsn.WEIGHT_TOL_MINUS = null;

                //newRow.QTYPACKMJ = string.Empty;
                if (!string.IsNullOrEmpty(tB_QTYPACKMJ.Text))
                    gsn.QTYPACKMJ = tB_QTYPACKMJ.Text;
                else
                    gsn.QTYPACKMJ = string.Empty; // newRow.SetQTYPACKMJNull();



            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return gsn;
        }


        #endregion


        #region BackGround Worker

        private void bwGenerovaniSN_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Classes.GenerovaniSN gl = (Fask.Interfaces.Classes.GenerovaniSN)e.Argument;

                if (bwGenerovaniSN.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if ((providerVPP != null) && (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_GenerovatSN_CZPRO_VPP))
                    ((Fask.Interfaces.Vyroba.VPP.IVPP_GenerovatSN_CZPRO_VPP)providerVPP).GenerovatSN_CZPRO_VPP(gl);
                else
                    throw new NotImplementedException("Provider neimplementuje IVPP_GenerovatSN_CZPRO_VPP.");

                if (bwGenerovaniSN.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bwGenerovaniSN_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            { 
                if (e.Error != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    // use it on the UI thread
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //ProgressIndicatorStop();
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            var W = panelButtons.Width;
            var H = panelButtons.Height;

            try
            {
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].VyrobniPrikazy_GenerovatSN)
                {
                    buttonStorno.Size = new Size(W / 3, H);
                    button_GenerateSN.Size = new Size(W / 3, H);
                }
                else
                {
                    button_GenerateSN.Enabled = false;
                    button_GenerateSN.Visible = false;
                    buttonStorno.Size = new Size(W / 2, H);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
    }
}
