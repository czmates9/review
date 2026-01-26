using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Konzola;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Ciselniky
{
    public partial class FormPracovniciEdit  : Form
    {
        private Fask.Interfaces.IMES providerPracovnici = null;

        /// <summary>
        /// Pracovnik, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Pracovnici.CZMST096Row rowPracovnikEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Interfaces.DataSets.Pracovnici.CZMST096Row returnrow { get; set; }

        public FormPracovniciEdit ()
        {
            InitializeComponent();            
        }

        private void FormPracovniciEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormPracovniciEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerPracovnici == null)
                    throw new Exception("Provider 'Uživatelé' není inicializován");

                LoadData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

        private void FormPracovniciEdit_KeyDown(object sender, KeyEventArgs e)
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
                if (rowPracovnikEdit != null)
                {   
                    // kontrola, zdali se mezitím nezměnil ...
                    //var rowusertest = ((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).GetPracovnikByID(rowPracovnikEdit.prac_id);
                    Fask.Interfaces.DataSets.Pracovnici.CZMST096Row rowusertest;

                    if ((providerPracovnici != null) && (providerPracovnici is Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID))
                        rowusertest = ((Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID)providerPracovnici).GetPracovnikByID(rowPracovnikEdit.prac_id);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPracovnici2_GetPracovnikByID.");


                    // zaznam nalezen
                    if (rowusertest != null)
                    {
                        // kontrola, zdali se zaznam nezmenil od posledni upravy
                        Fask.Interfaces.DataSets.Pracovnici.CZMST096DataTable dtCmpUziv = new Fask.Interfaces.DataSets.Pracovnici.CZMST096DataTable();
                        dtCmpUziv.ImportRow(rowPracovnikEdit);
                        dtCmpUziv.ImportRow(rowusertest);

                        IEqualityComparer<Fask.Interfaces.DataSets.Pracovnici.CZMST096Row> comparer = DataRowComparer.Default;
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

                Fask.Interfaces.DataSets.Pracovnici ds2 = new Fask.Interfaces.DataSets.Pracovnici();
                Fask.Interfaces.DataSets.Pracovnici.CZMST096Row newPracovnikRow = ds2.CZMST096.NewCZMST096Row();
                newPracovnikRow.prac_id = textBoxID.Text.Trim();
                newPracovnikRow.prac_desc = textBoxDesc.Text.Trim();
                newPracovnikRow.prac_typ = textBoxTyp.Text.Trim();
                newPracovnikRow.prac_carcode = textBoxCarKode.Text.Trim();



                // je úprava záznamu
                if (rowPracovnikEdit != null)
                {
                    //((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).UpdatePracovnici(newPracovnikRow);


                    if ((providerPracovnici != null) && (providerPracovnici is Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_UpdatePracovnici))
                        ((Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_UpdatePracovnici)providerPracovnici).UpdatePracovnici(newPracovnikRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPracovnici2_UpdatePracovnici.");
                
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).InsertPracovnici(newPracovnikRow);


                    if ((providerPracovnici != null) && (providerPracovnici is Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_InsertPracovnici))
                        ((Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_InsertPracovnici)providerPracovnici).InsertPracovnici(newPracovnikRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPracovnici2_InsertPracovnici.");
                
                }

                ds2.CZMST096.AddCZMST096Row(newPracovnikRow);

                // nejde pouzit, protoze pri opetovne editaci zaznamu by byl problem s porovnanim s aktualnim v DB (char x varchar)
                //returnrow = newUzivatelRow;

                // opetovne nacteni uzivatele
                //returnrow = ((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).GetPracovnikByID(newPracovnikRow.prac_id);

                if ((providerPracovnici != null) && (providerPracovnici is Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID))
                    returnrow = ((Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID)providerPracovnici).GetPracovnikByID(newPracovnikRow.prac_id);
                else
                    throw new NotImplementedException("Provider neimplementuje IPracovnici2_GetPracovnikByID.");


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
                // ID - musi byt 

                if (string.IsNullOrEmpty(textBoxID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxID, "Musíte zadat ID");
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
                if (string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;
                
                if (providerPracovnici == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2).IsAssignableFrom(t))
                            {
                                providerPracovnici= (Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPracovnici != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPracovnici.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // je úprava záznamu, dojde k načtení dat
                if (rowPracovnikEdit != null)
                {
                    textBoxDesc.Text = rowPracovnikEdit.prac_desc.Trim();
                    textBoxTyp.Text = rowPracovnikEdit.prac_typ.Trim();
                    textBoxID.Text = rowPracovnikEdit.prac_id.ToString();
                    textBoxCarKode.Text = rowPracovnikEdit.prac_carcode.Trim();
                    
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPracovniciEdit_Shown(object sender, EventArgs e)
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

        private void FormPracovniciEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }
    }
}
