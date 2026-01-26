using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Vyroba_Konzola;
using System.Reflection;

namespace Vyroba_Konzola.Ciselniky
{
    public partial class FormUzivateleEdit : Form
    {
        private Fask.Console.Interfaces.IVyrobaKonzola providerUzivatele = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow rowUzivatelEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow returnrow { get; set; }

        public FormUzivateleEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerUzivatele == null)
                    throw new Exception("Provider 'Uživatelé' není inicializován");

                LoadData();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
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
                
                // je úprava záznamu
                if (rowUzivatelEdit != null)
                {   
                    // kontrola, zdali se mezitím nezměnil ...
                    //var rowusertest = ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatelByID(rowUzivatelEdit.ID);
                    Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow rowusertest;

                    if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID))
                        rowusertest = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID)providerUzivatele).GetUzivatelByID(rowUzivatelEdit.ID);
                    else
                        throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatelByID.");


                    // zaznam nalezen
                    if (rowusertest != null)
                    {
                        // kontrola, zdali se zaznam nezmenil od posledni upravy
                        Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWD_COMPAREDataTable dtCmpUziv = new Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWD_COMPAREDataTable();
                        dtCmpUziv.ImportRow(rowUzivatelEdit);
                        dtCmpUziv.ImportRow(rowusertest);

                        IEqualityComparer<Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWD_COMPARERow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(dtCmpUziv[0], dtCmpUziv[1]);
                        if (!isMatch)
                        {
                            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                this.DialogResult = DialogResult.Cancel;
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }
                }

                Fask.Console.Interfaces.DataSets.Uzivatele ds2 = new Fask.Console.Interfaces.DataSets.Uzivatele();
                Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow newUzivatelRow = ds2.CZMSTPWD.NewCZMSTPWDRow();
                newUzivatelRow.ID = Convert.ToInt32(textBoxID.Text);
                newUzivatelRow.LOGIN = textBoxLOGIN.Text.Trim();
                newUzivatelRow.PASSWD = textBoxPASSWD.Text.Trim();
                newUzivatelRow.ADM = Convert.ToInt16(checkBoxADM.Checked);
                newUzivatelRow.FIRSTNAME = textBoxFIRSTNAME.Text.Trim();
                newUzivatelRow.SECONDNAME = textBoxSECONDNAME.Text.Trim();
                if(!string.IsNullOrEmpty(textBoxHASH.Text.Trim()))
                    newUzivatelRow.HASH = textBoxHASH.Text.Trim();
                if(!string.IsNullOrEmpty(textBoxEAN.Text.Trim()))
                    newUzivatelRow.EAN = textBoxEAN.Text.Trim();
                newUzivatelRow.CODE = string.Empty;

                // je úprava záznamu
                if (rowUzivatelEdit != null)
                {
                    //((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).UpdateUzivatel(newUzivatelRow);

                    if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_UpdateUzivatel))
                         ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_UpdateUzivatel)providerUzivatele).UpdateUzivatel(newUzivatelRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IUzivatele2_UpdateUzivatel.");
                }
                else   // nový záznam
                {
                    //((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).InsertUzivatel(newUzivatelRow);

                    if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_InsertUzivatel))
                        ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_InsertUzivatel)providerUzivatele).InsertUzivatel(newUzivatelRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IUzivatele2_InsertUzivatel.");
              
                }

                ds2.CZMSTPWD.AddCZMSTPWDRow(newUzivatelRow);

                // nejde pouzit, protoze pri opetovne editaci zaznamu by byl problem s porovnanim s aktualnim v DB (char x varchar)
                //returnrow = newUzivatelRow;

                // opetovne nacteni uzivatele
                //returnrow = ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatelByID(newUzivatelRow.ID);

                if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID))
                    returnrow  = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID)providerUzivatele).GetUzivatelByID(newUzivatelRow.ID);
                else
                    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatelByID.");

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
                errorProvider1.Clear();
                // LOGIN - musi byt unikatni
                // PASSWD - musi byt vyplneno
                // ID - musi byt unikatni - primarni klic
                // ADM - zaskrnuty/nezaskrnuty
                // FIRSTNAME - musi byt vyplneno
                // SECONDNAME - musi byt vyplneno
                // HASH ?? neresim zatim
                // EAN ?? neresim zatim
                // CODE ?? neresim zatim

                if (string.IsNullOrEmpty(textBoxPASSWD.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxPASSWD, "Musíte zadat heslo");
                }

                // validace id
                int id;
                bool status = Int32.TryParse(textBoxID.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id);
                if (!status)
                {
                    errorProvider1.SetError(textBoxID, "ID musí být číslo");
                }
                else
                {

                    if (id < 1)
                    {
                        errorProvider1.SetError(textBoxID, "ID musí být kladné číslo");
                    }
                    else
                    {
                        // kontrola jedinečnosti id
                        //Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow testRow = ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatelByID(id);
                        Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow testRow;

                        if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID))
                            testRow = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID)providerUzivatele).GetUzivatelByID(id);
                        else
                            throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatelByID.");


                        // editace zaznamu a zaznam se stejnym ID uz je v DB
                        if (rowUzivatelEdit == null && testRow != null)
                        {
                            errorProvider1.SetError(textBoxID, "Uživatel s ID: " + id + " již existuje");
                            
                        }
                    }
                }

                // kontrola jmena
                //if (string.IsNullOrEmpty(textBoxFIRSTNAME.Text.Trim()))
                //{
                //    errorProvider1.SetError(textBoxFIRSTNAME, "Musíte zadat jméno");
                //}

                if (string.IsNullOrEmpty(textBoxSECONDNAME.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxSECONDNAME, "Musíte zadat příjmení");
                }

                // kontrola unikatnosti loginu
                if (string.IsNullOrEmpty(textBoxLOGIN.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxLOGIN, "Musíte zadat LOGIN");
                }
                else
                {
                    Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr = new Fask.Console.Interfaces.Classes.UzivateleListFiltr();
                    filtr.UserLogin = textBoxLOGIN.Text.Trim();

                    Fask.Console.Interfaces.DataSets.Uzivatele uzivatele;

                   // var uzivatele = ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetFiltrovaneUzivatele(filtr);



                    if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetFiltrovaneUzivatele))
                        uzivatele = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetFiltrovaneUzivatele)providerUzivatele).GetFiltrovaneUzivatele(filtr);
                    else
                        throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetFiltrovaneUzivatele.");


                    if (uzivatele.CZMSTPWD.Count > 0)
                    {
                        // nový záznam
                        if (rowUzivatelEdit == null)
                        {
                            errorProvider1.SetError(textBoxLOGIN, "Zvolený LOGIN již existuje");
                        }
                        else
                        {
                            // kontrola, zdali nebyl nalezen jiný uživatel
                            if (uzivatele.CZMSTPWD.First().ID != rowUzivatelEdit.ID)
                            {
                                errorProvider1.SetError(textBoxLOGIN, "Zvolený LOGIN již existuje");
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
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

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (string.IsNullOrEmpty(Settings.ProviderKonzola))
                    return;
                
                if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Console.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
                            {
                                providerUzivatele= (Fask.Console.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerUzivatele != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                // nastaveni connection stringu
                //if (providerUzivatele!= null)
                //    ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                    ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
              


                
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Uzivatele");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // je úprava záznamu, dojde k načtení dat
                if (rowUzivatelEdit != null)
                {
                    textBoxLOGIN.Text = rowUzivatelEdit.LOGIN.Trim();
                    textBoxPASSWD.Text = rowUzivatelEdit.PASSWD.Trim();
                    textBoxID.Enabled = false;
                    textBoxID.Text = rowUzivatelEdit.ID.ToString();
                    checkBoxADM.Checked = rowUzivatelEdit.IsADMNull() ? false : Convert.ToBoolean(rowUzivatelEdit.ADM);
                    textBoxFIRSTNAME.Text = rowUzivatelEdit.FIRSTNAME.Trim();
                    textBoxSECONDNAME.Text = rowUzivatelEdit.SECONDNAME.Trim();
                    textBoxHASH.Text = rowUzivatelEdit.IsHASHNull() ? string.Empty : rowUzivatelEdit.HASH.Trim();
                    textBoxEAN.Text = rowUzivatelEdit.IsEANNull() ? string.Empty : rowUzivatelEdit.EAN.Trim();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {            
        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxHeslo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPrijmeni_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxJmeno_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelButtons_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }
    }
}
