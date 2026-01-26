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

namespace Konzola.Tisk
{
    public partial class FormTiskovaSablonaEdit : Form
    {

        private Fask.Interfaces.IMES providerTiskovaSablona = null;

        /// <summary>
        /// hlavička, která se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow fASK_FORMULARERow { get; set; }

        /// <summary>
        /// Hlavička, která byla vytvořena.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow fASK_FORMULARERowCreated { get; set; }
        

        private bool InsertNewRow = true;

        public FormTiskovaSablonaEdit()
        {
            InitializeComponent();
            this.InsertNewRow = false;
        }

        public FormTiskovaSablonaEdit(bool newInsert):base()
        {
            InitializeComponent();
            if (newInsert)
            {
                this.InsertNewRow = true;
            }
            else
            {
                this.InsertNewRow = false;
            }
        }

        private void FormTiskovaSablonaEdit_KeyDown(object sender, KeyEventArgs e)
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

                #region old
                //if (Duplikace)
                //{
                //    DialogResult dr = MessageBox.Show("Chcete duplikovat i množství? Pokud ne, bude veškeré množství nastaveno na hodnotu 0.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if(dr == DialogResult.Yes)
                //        DuplikovatMnozstvi = true;
                //    else
                //        DuplikovatMnozstvi = false;
                //}

                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                // je úprava záznamu

                //if (VPHrow != null)
                //{
                //    SOPNUMBE = VPHrow.SOPNUMBE;
                //    VPHrow.SOPDESC = tB_Nazev.Text.Trim();
                //    VPHrow.BarcodeH = tB_Ord.Text.Trim();
                //    //VPHrow.Active = (byte)(checkBoxAktivni.Checked ? 1 : 0);
                //    //VPHrow.Active = (byte)cb_Active.SelectedItem;
                //    int a = (int)cb_Active.SelectedItem;
                //    VPHrow.Active = (byte)a;

                //    VPHrow.USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);

                //    //lta.Update(VPHrow);

                //    if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)
                //    {
                //        //TODO MaR overit
                //        //Fask.Interfaces.DataSets.Vyroba dataset_tmp = new Fask.Interfaces.DataSets.Vyroba();
                //        ////dataset_tmp.CZPRO_VPH.AddCZPRO_VPHRow(VPHrow);
                //        //dataset_tmp.CZPRO_VPH.ImportRow(VPHrow);
                //        //((Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)providerVPH).Update_Row(dataset_tmp.CZPRO_VPH.First());

                //        //Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();
                //        //dt.ImportRow(VPHrow);
                //        //dt.AcceptChanges();

                //        VPHrow.AcceptChanges();
                //        VPHrow.SetModified();

                //        // odkomentovat!!
                //        ((Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)providerVPH).Update_Row(VPHrow);
                //    }
                //    else
                //        throw new Exception("IVPH_Update_Row not implementet");
                //}
                //else   // nový záznam
                //{
                //    SOPNUMBE = tB_NazevOkna.Text.Trim();
                //    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim());
                //    Fask.Interfaces.DataSets.Vyroba dataset = new Fask.Interfaces.DataSets.Vyroba();

                //    Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow newRow = dataset.CZPRO_VPH.NewCZPRO_VPHRow();
                //    newRow.CountEntries = Convert.ToInt32(tB_NazevOkna.Text.Trim());
                //    newRow.SOPNUMBE = tB_NazevOkna.Text.Trim();
                //    newRow.SOPDESC = tB_Nazev.Text.Trim();
                //    newRow.BarcodeH = tB_Ord.Text.Trim();
                //    newRow.SOPTYPE = string.Empty;
                //    newRow.VNDDOCNMH = string.Empty;
                //    newRow.LOCNCODE = string.Empty;
                //    newRow.DateProd = 15;
                //    newRow.Rez1 = string.Empty;
                //    newRow.Rez2 = string.Empty;
                //    newRow.TermID = 0;
                //    newRow.LSTMod = DateTime.Now;
                //    //newRow.Active = checkBoxAktivni.Checked ? (byte)1 : (byte)0;
                //    int a = (int)cb_Active.SelectedItem;
                //    newRow.Active = (byte)a;

                //    newRow.USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);

                //    VPHrowCreated = newRow;

                //    if (InsertNewRow)
                //    {
                //        if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Insert)
                //        {
                //            //MaR zmeny
                //            dataset.CZPRO_VPH.AddCZPRO_VPHRow(newRow);
                //            ((Fask.Interfaces.Vyroba.VPH.IVPH_Insert)providerVPH).Insert(newRow);
                //        }
                //        else
                //            throw new Exception("IVPH_Insert not implementet");
                //    }
                //} 
                #endregion

                bool zmena = false;
                int zapisOK = 0;
                if (fASK_FORMULARERow != null)
                {
                    
                    //tB_NazevOkna.Enabled = false;
                    //tB_NazevOkna.Text = fASK_FORMULARERow.Isnazev_oknaNull() ? string.Empty : fASK_FORMULARERow.nazev_okna;
                        if(tB_Nazev.Text != (fASK_FORMULARERow.IsnazevNull() ? string.Empty : fASK_FORMULARERow.nazev))
                        {
                            fASK_FORMULARERow.nazev = tB_Nazev.Text;
                            zmena = true;
                        }
                        if (tB_Ord.Text != fASK_FORMULARERow.ord.ToString())
                        {
                            fASK_FORMULARERow.ord = Int32.Parse(tB_Ord.Text);
                            zmena = true;
                        }
                        if (tB_Typ.Text != (fASK_FORMULARERow.IstypNull() ? string.Empty : fASK_FORMULARERow.typ))
                        {

                        if(tB_Typ.Text.Length > 2)
                        {
                            MessageBox.Show("Délka Typ nesmí být větší než 2 znaky.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                            fASK_FORMULARERow.typ = tB_Typ.Text;
                            zmena = true;
                        }
                        if (tB_Loginid.Text != (fASK_FORMULARERow.IsloginidNull() ? string.Empty : fASK_FORMULARERow.loginid))
                        {
                            fASK_FORMULARERow.loginid = tB_Loginid.Text;
                            zmena = true;
                        }
                        if (tB_Machineid.Text != (fASK_FORMULARERow.IsmachineidNull() ? string.Empty : fASK_FORMULARERow.machineid))
                        {
                            fASK_FORMULARERow.machineid = tB_Machineid.Text;
                            zmena = true;
                        }
                        if (tB_FormalCesta.Text != (fASK_FORMULARERow.IsformularNull() ? string.Empty : fASK_FORMULARERow.formular))
                        {
                            fASK_FORMULARERow.formular = tB_FormalCesta.Text;
                            zmena = true;
                        }
                   

                    if(zmena)
                    {
                        //TODO MaR 17.10.2023 zde bude upload tabulky

                        InitProvider();

                        if (providerTiskovaSablona == null)
                            throw new Exception("Provider 'TiskovaSablona' není inicializován");


                        if ((providerTiskovaSablona != null) && providerTiskovaSablona is Fask.Interfaces.Tisky.ITisk_TiskovaSablona)
                        {
                           

                            if (InsertNewRow)
                            {
                                //insert zaznamu
                                zapisOK = ((Fask.Interfaces.Tisky.ITisk_TiskovaSablona)providerTiskovaSablona).TiskovaSablonaInsert_DB(fASK_FORMULARERow);

                            }
                            else
                            { 
                                //editace zaznamu
                                fASK_FORMULARERow.AcceptChanges();
                                fASK_FORMULARERow.SetModified();
                                zapisOK = ((Fask.Interfaces.Tisky.ITisk_TiskovaSablona)providerTiskovaSablona).TiskovaSablonaEdit_DB(fASK_FORMULARERow);

                            }

                        }
                        else
                            throw new Exception("TiskovaSablonaEdit_DB not implementet");
                    }


                    //tB_Nazev.Text = fASK_FORMULARERow.IsnazevNull() ? string.Empty : fASK_FORMULARERow.nazev;
                    //tB_Ord.Text = fASK_FORMULARERow.ord.ToString();
                    //tB_Typ.Text = fASK_FORMULARERow.IstypNull() ? string.Empty : fASK_FORMULARERow.typ;
                    //tB_Loginid.Text = fASK_FORMULARERow.IsloginidNull() ? string.Empty : fASK_FORMULARERow.loginid;
                    //tB_Machineid.Text = fASK_FORMULARERow.IsmachineidNull() ? string.Empty : fASK_FORMULARERow.machineid;
                    //tB_FormalCesta.Text = fASK_FORMULARERow.IsformularNull() ? string.Empty : fASK_FORMULARERow.formular;
                }

                if (InsertNewRow)
                {
                    if (zapisOK == 0)
                    {
                        MessageBox.Show("Záznam nebyl vytvořen.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (zapisOK < 0)
                    {
                        MessageBox.Show("Záznam nebyl vytvořen. Chyba na straně komunikačního serveru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (zapisOK == 0)
                    {
                        MessageBox.Show("Záznam nebyl editován.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (zapisOK < 0)
                    {
                        MessageBox.Show("Záznam nebyl editován. Chyba na straně komunikačního serveru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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

                if (string.IsNullOrEmpty(tB_Nazev.Text.Trim()))
                    errorProvider1.SetError(tB_Nazev, "Musíte zadat název formuláře.");

                if (string.IsNullOrEmpty(tB_Ord.Text.Trim()))
                    errorProvider1.SetError(tB_Ord, "Musíte zadat ord formuláře.");

                #region old

                ////var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                ////adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //Fask.Interfaces.DataSets.Vyroba vphdatatable = new Fask.Interfaces.DataSets.Vyroba();
                ////adapter.Fill(vphdatatable);

                //if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill)
                //    ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(vphdatatable);
                //else
                //    throw new Exception("IVPH_Fill not implementet");

                //if (VPHrow == null && (string.IsNullOrEmpty(errorProvider1.GetError(tB_NazevOkna))))
                //{
                //    // vytváří se nový záznam, kontrola existence id                    
                //    var logins = vphdatatable.CZPRO_VPH.Where(x => x.SOPNUMBE.Trim() == tB_NazevOkna.Text.Trim());
                //    if (logins.Count() > 0)
                //    {
                //        if (SlucovatPrikaze)
                //        {
                //            DialogResult dr = MessageBox.Show("Zadané číslo výrobní zakázky již existuje. Pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //            if (dr != System.Windows.Forms.DialogResult.Yes)
                //            {
                //                tB_NazevOkna.Focus();
                //                errorProvider1.SetError(tB_NazevOkna, "Zadané číslo výrobní zakázky již existuje");
                //            }
                //        }
                //        else
                //        {
                //            tB_NazevOkna.Focus();
                //            errorProvider1.SetError(tB_NazevOkna, "Zadané číslo výrobní zakázky již existuje");
                //        }
                //    }

                //    int sopnumbe;
                //    bool status = Int32.TryParse(tB_NazevOkna.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out sopnumbe);
                //    if (!status)
                //    {
                //        tB_NazevOkna.Focus();
                //        errorProvider1.SetError(tB_NazevOkna, "Číslo výrobní zakázky musí být číslo");
                //    }

                //    if (sopnumbe < 1)
                //    {
                //        tB_NazevOkna.Focus();
                //        errorProvider1.SetError(tB_NazevOkna, "Číslo výrobní zakázky musí být větší než 0 a menší než 2147483647");
                //    }
                //}

                ////if (string.IsNullOrEmpty(textBoxPopisZakazky.Text.Trim()))
                ////{
                ////    textBoxPopisZakazky.Focus();
                ////    throw new Exception("Musíte zadat popis zakázky");
                ////}
                //if (string.IsNullOrEmpty(tB_Ord.Text.Trim()))
                //{
                //    tB_Ord.Focus();
                //    errorProvider1.SetError(tB_Ord, "Musíte zadat čárový kód");
                //}

                // kontrola jedinečnosti čár. kódu
                //if (string.IsNullOrEmpty(errorProvider1.GetError(tB_Ord)))
                //{
                //    var findBarcode = vphdatatable.CZPRO_VPH.Where(x => x.BarcodeH.Trim() == tB_Ord.Text.Trim());
                //    if (findBarcode.Count() > 0)
                //    {
                //        // nový záznam
                //        if (VPHrow == null)
                //        {
                //            if (SlucovatPrikaze)
                //            {
                //                DialogResult dr = MessageBox.Show("Výrobní příkaz s tímto čárovým kódem již existuje. Pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //                if (dr != System.Windows.Forms.DialogResult.Yes)
                //                {
                //                    tB_Ord.Focus();
                //                    errorProvider1.SetError(tB_Ord, "Výrobní příkaz s tímto čárovým kódem již existuje");
                //                }
                //            }
                //            else
                //            {
                //                tB_Ord.Focus();
                //                errorProvider1.SetError(tB_Ord, "Výrobní příkaz s tímto čárovým kódem již existuje");
                //            }
                //        }
                //        else
                //        {  // editace stávajícího a čár. kód se liší od původního
                //            if (tB_Ord.Text.Trim() != VPHrow.BarcodeH.Trim())
                //            {
                //                tB_Ord.Focus();
                //                errorProvider1.SetError(tB_Ord, "Výrobní příkaz s tímto čárovým kódem již existuje");
                //            }
                //        }
                //    }
                //} 
                #endregion
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

        private void FormTiskovaSablonaEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormTiskovaSablonaEdit_Resize(null, null);
                #region old

                //InitProvider();

                //if (providerVPH == null)
                //    throw new Exception("Provider 'VPH' není inicializován");

                //btn_VPH.Visible = SlucovatPrikaze;

                //cb_Active.DataSource = Enum.GetValues(typeof(Fask.Interfaces.Classes.VyrobaStavPrikazu));

                ////je úprava záznamu, dojde k načtení dat
                //if (VPHrow != null)
                //{
                //    tB_NazevOkna.Enabled = false;
                //    tB_NazevOkna.Text = VPHrow.SOPNUMBE.Trim();
                //    tB_Nazev.Text = VPHrow.IsSOPDESCNull() ? string.Empty : VPHrow.SOPDESC.Trim();
                //    tB_Ord.Text = VPHrow.BarcodeH.Trim();
                //    //heckBoxAktivni.Checked = VPHrow.Active != 0;

                //    if (((int)VPHrow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni))
                //    {
                //        cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni;
                //    }

                //    if (((int)VPHrow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni))
                //    {
                //        cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni;
                //    }

                //    if (((int)VPHrow.Active) == ((int)Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen))
                //    {
                //        cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen;
                //    }
                //}
                //else tB_NazevOkna.Text = DateTime.Now.ToString("yyMMdd"); 
                #endregion

                if (fASK_FORMULARERow != null)
                {
                    tB_NazevOkna.Enabled = false;
                    tB_NazevOkna.Text = fASK_FORMULARERow.Isnazev_oknaNull() ? string.Empty : fASK_FORMULARERow.nazev_okna;
                    tB_Nazev.Text = fASK_FORMULARERow.IsnazevNull() ? string.Empty : fASK_FORMULARERow.nazev;
                    tB_Ord.Text = fASK_FORMULARERow.ord.ToString();
                    tB_Typ.Text = fASK_FORMULARERow.IstypNull() ? string.Empty : fASK_FORMULARERow.typ;
                    tB_Loginid.Text = fASK_FORMULARERow.IsloginidNull() ? string.Empty : fASK_FORMULARERow.loginid;
                    tB_Machineid.Text = fASK_FORMULARERow.IsmachineidNull() ? string.Empty : fASK_FORMULARERow.machineid;
                    tB_FormalCesta.Text = fASK_FORMULARERow.IsformularNull() ? string.Empty : fASK_FORMULARERow.formular;
                }
                //else tB_NazevOkna.Text = DateTime.Now.ToString("yyMMdd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }            
        }

        private void InitProvider()
        {
            #region providerTiskovaSablona

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerTiskovaSablona == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Tisky.ITisky2).IsAssignableFrom(t))
                            {
                                providerTiskovaSablona = (Fask.Interfaces.Tisky.ITisky2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerTiskovaSablona != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerTiskovaSablona.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

        }

        private void FormTiskovaSablonaEdit_Shown(object sender, EventArgs e)
        {            
        }

        private void FormTiskovaSablonaEdit_Resize(object sender, EventArgs e)
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
            tB_Ord.Text = tB_NazevOkna.Text;
        }

     

    }
}
