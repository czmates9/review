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
    public partial class FormLokaceCZMST094Edit : Form
    {
        private Fask.Interfaces.IMES providerLokaceCZMST094 = null;

        /// <summary>
        /// Pracovnik, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST094Row rowLokace_CZMST094Edit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST094Row returnrow { get; set; }

        public FormLokaceCZMST094Edit()
        {
            InitializeComponent();            
        }

        private void FormLokaceCZMST094Edit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormLokaceCZMST094Edit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerLokaceCZMST094 == null)
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

        private void FormLokaceCZMST094Edit_KeyDown(object sender, KeyEventArgs e)
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
                
                //// je úprava záznamu
                //if (rowLokace_CZMST094Edit != null)
                //{   
                //    // kontrola, zdali se mezitím nezměnil ...
                //    //var rowusertest = ((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).GetPracovnikByID(rowPracovnikEdit.prac_id);
                //    Fask.Interfaces.DataSets.Pracovnici.CZMST096Row rowusertest;

                //    if ((providerLokaceCZMST094 != null) && (providerLokaceCZMST094 is Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID))
                //        rowusertest = ((Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID)providerLokaceCZMST094).GetPracovnikByID(rowLokace_CZMST094Edit.prac_id);
                //    else
                //        throw new NotImplementedException("Provider neimplementuje IPracovnici2_GetPracovnikByID.");


                //    // zaznam nalezen
                //    if (rowusertest != null)
                //    {
                //        // kontrola, zdali se zaznam nezmenil od posledni upravy
                //        Fask.Interfaces.DataSets.Pracovnici.CZMST096DataTable dtCmpUziv = new Fask.Interfaces.DataSets.Pracovnici.CZMST096DataTable();
                //        dtCmpUziv.ImportRow(rowLokace_CZMST094Edit);
                //        dtCmpUziv.ImportRow(rowusertest);

                //        IEqualityComparer<Fask.Interfaces.DataSets.Pracovnici.CZMST096Row> comparer = DataRowComparer.Default;
                //        bool isMatch = comparer.Equals(dtCmpUziv[0], dtCmpUziv[1]);
                //        if (!isMatch)
                //        {
                //            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //            {
                //                this.DialogResult = DialogResult.Cancel;
                //                return;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        this.DialogResult = DialogResult.Cancel;
                //        return;
                //    }
                //}

                Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();
                Fask.Interfaces.DataSets.SkladLokace.CZMST094Row newSkladLokaceRow = ds2.CZMST094.NewCZMST094Row();
                //newSkladLokaceRow.prac_id = tB_SKL_ID.Text.Trim();
                //newSkladLokaceRow.prac_desc = tB_Desc.Text.Trim();
                //newSkladLokaceRow.prac_typ = tB_TYPE.Text.Trim();
                //newSkladLokaceRow.prac_carcode = tB_Barcode.Text.Trim();

                if (string.IsNullOrEmpty(tB_SKL_ID.Text.Trim()))
                {
                    newSkladLokaceRow.SKL_ID = string.Empty;
                }
                else
                {
                    newSkladLokaceRow.SKL_ID = tB_SKL_ID.Text.Trim();
                }

                newSkladLokaceRow.LOCNCODE = tB_LOCNCODE.Text.Trim();

                if (string.IsNullOrEmpty(tB_TYPE.Text.Trim()))
                {
                    newSkladLokaceRow.TYPE = string.Empty;
                }
                else
                {
                    newSkladLokaceRow.TYPE = tB_TYPE.Text.Trim();
                }
                if (string.IsNullOrEmpty(tB_Desc.Text.Trim()))
                {
                    newSkladLokaceRow.Description = string.Empty;
                }
                else
                {
                    newSkladLokaceRow.Description = tB_Desc.Text.Trim();
                }
                if (string.IsNullOrEmpty(tB_Barcode.Text.Trim()))
                {
                    newSkladLokaceRow.Barcode = string.Empty;
                }
                else
                {
                    newSkladLokaceRow.Barcode = tB_Barcode.Text.Trim();
                }


                // je úprava záznamu
                if (rowLokace_CZMST094Edit != null)
                {
                    //((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).UpdatePracovnici(newPracovnikRow);


                    if ((providerLokaceCZMST094 != null) && (providerLokaceCZMST094 is Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_UpdateSkladLokace))
                        ((Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_UpdateSkladLokace)providerLokaceCZMST094).UpdateSkladLokace_CZMST094(newSkladLokaceRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_CZMST094_UpdateSkladLokace.");
                
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).InsertPracovnici(newPracovnikRow);


                    if ((providerLokaceCZMST094 != null) && (providerLokaceCZMST094 is Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_InsertSkladLokace))
                        ((Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_InsertSkladLokace)providerLokaceCZMST094).InsertSkladLokace_CZMST094(newSkladLokaceRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_CZMST094_InsertSkladLokace.");
                
                }

                ds2.CZMST094.AddCZMST094Row(newSkladLokaceRow);

                // nejde pouzit, protoze pri opetovne editaci zaznamu by byl problem s porovnanim s aktualnim v DB (char x varchar)
                //returnrow = newUzivatelRow;

                // opetovne nacteni uzivatele
                //returnrow = ((Fask.Interfaces.Ciselniky.IPracovnici)providerPracovnici).GetPracovnikByID(newPracovnikRow.prac_id);

                //if ((providerLokaceCZMST094 != null) && (providerLokaceCZMST094 is Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID))
                //    returnrow = ((Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID)providerLokaceCZMST094).GetPracovnikByID(newSkladLokaceRow.prac_id);
                //else
                //    throw new NotImplementedException("Provider neimplementuje IPracovnici2_GetPracovnikByID.");


                if ((providerLokaceCZMST094 != null) && (providerLokaceCZMST094 is Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_GetSkladLokaceByLOCNCODE))
                    returnrow = ((Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_GetSkladLokaceByLOCNCODE)providerLokaceCZMST094).GetSkladLokaceByLOCNCODE_CZMST094(newSkladLokaceRow.LOCNCODE);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace_CZMST094_GetSkladLokaceByLOCNCODE.");

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

                if (string.IsNullOrEmpty(tB_LOCNCODE.Text.Trim()))
                {
                    errorProvider1.SetError(tB_LOCNCODE, "Musíte zadat lokaci");
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
                
                if (providerLokaceCZMST094 == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094).IsAssignableFrom(t))
                            {
                                providerLokaceCZMST094= (Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094)providerAssemlby.CreateInstance(t.FullName);
                                if (providerLokaceCZMST094 != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerLokaceCZMST094.InitProvider();

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
                if (rowLokace_CZMST094Edit != null)
                {
                    tB_LOCNCODE.Enabled = false;
                    tB_SKL_ID.Enabled = false;


                    if (rowLokace_CZMST094Edit.IsDescriptionNull())
                    {

                    }
                    else
                    {
                        tB_Desc.Text = rowLokace_CZMST094Edit.Description.Trim();
                    }

                    if (rowLokace_CZMST094Edit.IsTYPENull())
                    {

                    }
                    else
                    {
                        tB_TYPE.Text = rowLokace_CZMST094Edit.TYPE.Trim();
                    }

                    if (rowLokace_CZMST094Edit.IsSKL_IDNull())
                    {

                    }
                    else
                    {
                        tB_SKL_ID.Text = rowLokace_CZMST094Edit.SKL_ID.Trim();
                    }

                    if (rowLokace_CZMST094Edit.IsBarcodeNull())
                    {

                    }
                    else
                    {
                        tB_Barcode.Text = rowLokace_CZMST094Edit.Barcode.Trim();
                    }

                    tB_LOCNCODE.Text = rowLokace_CZMST094Edit.LOCNCODE.Trim();

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

        private void FormLokaceCZMST094Edit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }
    }
}
