using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Vyroba
{
    public partial class FormVPHEdit : Form
    {

        private Fask.Interfaces.IMES providerVPH = null;

        /// <summary>
        /// hlavička, která se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow VPHrow { get; set; }

        /// <summary>
        /// Hlavička, která byla vytvořena.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow VPHrowCreated { get; set; }
        /// <summary>
        /// Jedná se o duplikace, zobrazí se dotaz pro duplikování množství.
        /// </summary>
        public bool Duplikace = false;
        public bool DuplikovatMnozstvi = false;
        public string SOPNUMBE = string.Empty;

        public bool InsertNewRow = true;

        public bool SlucovatPrikaze = false;

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

                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                // je úprava záznamu
                if (VPHrow != null)
                {
                    SOPNUMBE = VPHrow.SOPNUMBE;
                    VPHrow.SOPDESC = textBoxPopisZakazky.Text.Trim();
                    VPHrow.BarcodeH = textBoxCarovyKod.Text.Trim();
                    VPHrow.SOPTYPE = tB_Typ.Text;
                    //VPHrow.Active = (byte)(checkBoxAktivni.Checked ? 1 : 0);
                    //VPHrow.Active = (byte)cb_Active.SelectedItem;
                    int a = (int)cb_Active.SelectedItem;
                    VPHrow.Active = (byte)a;

                    VPHrow.USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);






                    short.TryParse(tB_DateProd.Text, out short result);
                    VPHrow.DateProd = result;
                    //lta.Update(VPHrow);

                    if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)
                    {
                        //TODO MaR overit
                        //Fask.Interfaces.DataSets.Vyroba dataset_tmp = new Fask.Interfaces.DataSets.Vyroba();
                        ////dataset_tmp.CZPRO_VPH.AddCZPRO_VPHRow(VPHrow);
                        //dataset_tmp.CZPRO_VPH.ImportRow(VPHrow);
                        //((Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)providerVPH).Update_Row(dataset_tmp.CZPRO_VPH.First());

                        //Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();
                        //dt.ImportRow(VPHrow);
                        //dt.AcceptChanges();

                        VPHrow.AcceptChanges();
                        VPHrow.SetModified();

                        // odkomentovat!!
                        ((Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)providerVPH).Update_Row(VPHrow); //prvni zmena MaR 27.1.2025
                    }
                    else
                        throw new Exception("IVPH_Update_Row not implementet");
                }
                else   // nový záznam
                {
                    SOPNUMBE = textBoxId.Text.Trim();
                    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim());
                    Fask.Interfaces.DataSets.Vyroba dataset = new Fask.Interfaces.DataSets.Vyroba();

                    Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow newRow = dataset.CZPRO_VPH.NewCZPRO_VPHRow();
                    newRow.CountEntries = Convert.ToInt32(textBoxId.Text.Trim());
                    newRow.SOPNUMBE = textBoxId.Text.Trim();
                    newRow.SOPDESC = textBoxPopisZakazky.Text.Trim();
                    newRow.BarcodeH = textBoxCarovyKod.Text.Trim();
                    newRow.SOPTYPE = string.IsNullOrEmpty(tB_Typ.Text) ? string.Empty : tB_Typ.Text;
                    newRow.VNDDOCNMH = string.Empty;
                    newRow.LOCNCODE = string.Empty;

                    //od TaD 24.1.2025
                    //newRow.DateProd = 15;

                    short.TryParse(tB_DateProd.Text, out short result);
                    newRow.DateProd = result;

                    


                    newRow.Rez1 = string.Empty;
                    newRow.Rez2 = string.Empty;
                    newRow.TermID = 0;
                    newRow.LSTMod = DateTime.Now;
                    //newRow.Active = checkBoxAktivni.Checked ? (byte)1 : (byte)0;
                    int a = (int)cb_Active.SelectedItem;
                    newRow.Active = (byte)a;

                    newRow.USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);

                    VPHrowCreated = newRow;

                    if (InsertNewRow)
                    {
                        if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Insert)
                        {
                            //MaR zmeny
                            dataset.CZPRO_VPH.AddCZPRO_VPHRow(newRow);
                            ((Fask.Interfaces.Vyroba.VPH.IVPH_Insert)providerVPH).Insert(newRow);  // druha zmena MaR 27.1.2025
                        }
                        else
                            throw new Exception("IVPH_Insert not implementet"); 
                    }
                }

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

                if (string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    errorProvider1.SetError(textBoxId, "Musíte zadat číslo výrobní zakázky");

                //var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                //adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                Fask.Interfaces.DataSets.Vyroba vphdatatable = new Fask.Interfaces.DataSets.Vyroba();
                //adapter.Fill(vphdatatable);

                if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill)
                    ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(vphdatatable);
                else
                    throw new Exception("IVPH_Fill not implementet");

                if (VPHrow == null && (string.IsNullOrEmpty(errorProvider1.GetError(textBoxId))))
                {
                    // vytváří se nový záznam, kontrola existence id                    
                    var logins = vphdatatable.CZPRO_VPH.Where(x => x.SOPNUMBE.Trim() == textBoxId.Text.Trim());
                    if (logins.Count() > 0)
                    {
                        if (SlucovatPrikaze)
                        {
                            DialogResult dr = MessageBox.Show("Zadané číslo výrobní zakázky již existuje. Pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dr != System.Windows.Forms.DialogResult.Yes)
                            {
                                textBoxId.Focus();
                                errorProvider1.SetError(textBoxId, "Zadané číslo výrobní zakázky již existuje");
                            }
                        }
                        else
                        {
                            textBoxId.Focus();
                            errorProvider1.SetError(textBoxId, "Zadané číslo výrobní zakázky již existuje");
                        }
                    }

                    int sopnumbe;
                    bool status = Int32.TryParse(textBoxId.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out sopnumbe);
                    if (!status)
                    {
                        textBoxId.Focus();
                        errorProvider1.SetError(textBoxId, "Číslo výrobní zakázky musí být číslo");
                    }

                    if (sopnumbe < 1)
                    {
                        textBoxId.Focus();
                        errorProvider1.SetError(textBoxId, "Číslo výrobní zakázky musí být větší než 0 a menší než 2147483647");
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
                    errorProvider1.SetError(textBoxCarovyKod, "Musíte zadat čárový kód");
                }

                // kontrola jedinečnosti čár. kódu
                if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxCarovyKod)))
                {
                    var findBarcode = vphdatatable.CZPRO_VPH.Where(x => x.BarcodeH.Trim() == textBoxCarovyKod.Text.Trim());
                    if (findBarcode.Count() > 0)
                    {
                        // nový záznam
                        if (VPHrow == null)
                        {
                            if (SlucovatPrikaze)
                            {
                                DialogResult dr = MessageBox.Show("Výrobní příkaz s tímto čárovým kódem již existuje. Pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (dr != System.Windows.Forms.DialogResult.Yes)
                                {
                                    textBoxCarovyKod.Focus();
                                    errorProvider1.SetError(textBoxCarovyKod, "Výrobní příkaz s tímto čárovým kódem již existuje");
                                }
                            }
                            else
                            {
                                textBoxCarovyKod.Focus();
                                errorProvider1.SetError(textBoxCarovyKod, "Výrobní příkaz s tímto čárovým kódem již existuje");
                            }
                        }
                        else
                        {  // editace stávajícího a čár. kód se liší od původního
                            if (textBoxCarovyKod.Text.Trim() != VPHrow.BarcodeH.Trim())
                            {
                                textBoxCarovyKod.Focus();
                                errorProvider1.SetError(textBoxCarovyKod, "Výrobní příkaz s tímto čárovým kódem již existuje");
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(tB_Typ.Text) && tB_Typ.Text.Length > 2)
                {
                    errorProvider1.SetError(tB_Typ, "Typ výrobního příkazu nesmí být delší jak 2 znaky.");
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


                InitProvider();

                if (providerVPH == null)
                    throw new Exception("Provider 'VPH' není inicializován");

                btn_VPH.Visible = SlucovatPrikaze;

                cb_Active.DataSource = Enum.GetValues(typeof(Fask.Interfaces.Classes.VyrobaStavPrikazu));

                //je úprava záznamu, dojde k načtení dat
                if (VPHrow != null)
                {
                    textBoxId.Enabled = false;
                    textBoxId.Text = VPHrow.SOPNUMBE.Trim();
                    textBoxPopisZakazky.Text = VPHrow.IsSOPDESCNull() ? string.Empty : VPHrow.SOPDESC.Trim();
                    textBoxCarovyKod.Text = VPHrow.BarcodeH.Trim();
                    tB_Typ.Text = string.IsNullOrEmpty(VPHrow.SOPTYPE) ? string.Empty : VPHrow.SOPTYPE.Trim();
                    //heckBoxAktivni.Checked = VPHrow.Active != 0;

                    if (((int)VPHrow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni))
                    {
                        cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni;
                    }

                    if (((int)VPHrow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni))
                    {
                        cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni;
                    }

                    if (((int)VPHrow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen))
                    {
                        cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen;
                    }

                    tB_DateProd.Text = VPHrow.DateProd.ToString();


                }
                else textBoxId.Text = DateTime.Now.ToString("yyMMdd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }            
        }

        private void InitProvider()
        {
            #region providerVPH

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPH == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPH.IVPH).IsAssignableFrom(t))
                            {
                                providerVPH = (Fask.Interfaces.Vyroba.VPH.IVPH)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPH != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPH.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

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

            bool work = true;

            work = ValidaceVstupUzivatel();

            if (work)
            {
                //PerformOK();
                this.PerformOK();
            }

            
        }

        private bool ValidaceVstupUzivatel()
        {

            bool vysledek = true;

            // Validace vstupu OD
            if (!string.IsNullOrWhiteSpace(tB_DateProd.Text))
            {
                if (short.TryParse(tB_DateProd.Text, out short result))
                {
                    if (result < 0)
                    {
                        // Pokud je číslo záporné, zobrazí dialog a vrátí false
                        MessageBox.Show("Zadaná hodnota Očekávane datum výroby musí být kladné celé číslo do 32 767.", "Neplatná hodnota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        vysledek = false;
                        return vysledek;
                    }
                }
                else
                {
                    // musí být kladné celé číslo do 32 767.
                    // Pokud není platné celé číslo smallint, zobrazí dialog a vrátí false
                    MessageBox.Show("Zadaná hodnota Očekávane datum výroby musí být kladné celé číslo do 32 767.", "Neplatná hodnota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vysledek = false;
                    return vysledek;
                }
            }

            return vysledek;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            textBoxCarovyKod.Text = textBoxId.Text;
        }

        private void btn_VPH_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new Vyroba.FormVyrobniPrikaz_VPH_Seznam())
                {
                    DialogResult dr = frm.ShowDialog();

                    if (dr != System.Windows.Forms.DialogResult.OK)
                        return;

                    bool AktivniVPH = frm.SelectedRow.Active != 0;

                    if (AktivniVPH)
                    {
                        MessageBox.Show("Vyrobní příkaz nelze použit protože je již aktivní!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        VPHrow = frm.SelectedRow;
                        textBoxId.Text = frm.SelectedRow.SOPNUMBE.Trim();
                        textBoxPopisZakazky.Text = frm.SelectedRow.IsSOPDESCNull() ? string.Empty : frm.SelectedRow.SOPDESC.Trim();
                        textBoxCarovyKod.Text = frm.SelectedRow.BarcodeH.Trim();
                        tB_Typ.Text = string.IsNullOrEmpty(frm.SelectedRow.SOPTYPE) ? string.Empty : frm.SelectedRow.SOPTYPE.Trim();
                        //checkBoxAktivni.Checked = frm.SelectedRow.Active != 0;
                        //cb_Active.SelectedItem = frm.SelectedRow.Active;

                        if (((int)frm.SelectedRow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni))
                        {
                            cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni;
                        }

                        if (((int)frm.SelectedRow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni))
                        {
                            cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni;
                        }

                        if (((int)frm.SelectedRow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen))
                        {
                            cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen;
                        }

                        //textBoxId.Enabled = false;
                        textBoxPopisZakazky.Enabled = false;
                        textBoxCarovyKod.Enabled = false;
                        //checkBoxAktivni.Enabled = false;
                        cb_Active.Enabled = false;
                    }
                }



            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

    }
}
