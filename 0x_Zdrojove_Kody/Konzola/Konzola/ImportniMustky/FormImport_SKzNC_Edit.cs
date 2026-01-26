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
using Fask.Interfaces.DataSets_Import;
using Konzola.ImportniMustky.Extensions;

namespace Konzola.ImportniMustky
{
    public partial class FormImport_SKzNC_Edit : Form
    {

        /// <summary>
        /// Provider pro komunikaci
        /// </summary>
        private Fask.Interfaces.IMES provider = null;

        /// <summary>
        /// Zboží, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow rowImportEdit { get; set; }
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormImport_SKzNC_Edit()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Event pro Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        /// <summary>
        /// Metoda pro zrušení okna
        /// </summary>
        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Event Load pro Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormImport_SKzNC_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormImport_SKzNC_Edit_Resize(null, null);

                InitProvider();

                if (provider == null)
                    throw new Exception("Provider není inicializován");

                if (rowImportEdit != null)
                {
                    TextBox_DEX_ROW_ID.Visible = true;
                    TextBox_DEX_ROW_ID.ReadOnly = true;
                    label3.Visible = true;

                    TextBox_RefAg.ReadOnly = true;
                    TextBox_IDS_SKz.ReadOnly = true;
                    TextBox_ID_sSklad.ReadOnly = true;
                }
                else
                {
                    TextBox_DEX_ROW_ID.Visible = false;
                    label3.Visible = false;
                }

                LoadData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region inicaliyace provideru


        private void InitProvider()
        {

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.ImportnyMustky.IImportnyMustky).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.ImportnyMustky.IImportnyMustky)providerAssemlby.CreateInstance(t.FullName);
                                if (provider != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                provider.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion



        private void LoadData()
        {


            try
            {
                // je úprava záznamu, dojde k načtení dat
                if (rowImportEdit != null)
{
                    TextBox_DEX_ROW_ID.Text = rowImportEdit.DEX_ROW_ID.ToString();
                    cbox_DefDod.Checked = rowImportEdit.DefDod;

                    if (rowImportEdit.IsRefAgNull())
                        TextBox_RefAg.Text = string.Empty;
                    else
                        TextBox_RefAg.Text = rowImportEdit.RefAg.ToString();

                    if (rowImportEdit.IsIDS_SKzNull())
                        TextBox_IDS_SKz.Text = string.Empty;
                    else
                        TextBox_IDS_SKz.Text = rowImportEdit.IDS_SKz;

                    if (rowImportEdit.IsID_sSkladNull())
                        TextBox_ID_sSklad.Text = string.Empty;
                    else
                        TextBox_ID_sSklad.Text = rowImportEdit.ID_sSklad.ToString();

                    if (rowImportEdit.IsRefADNull())
                        TextBox_RefAD.Text = string.Empty;
                    else
                        TextBox_RefAD.Text = rowImportEdit.RefAD.ToString();

                    if (rowImportEdit.IsFirmaNull())
                        TextBox_Firma.Text = string.Empty;
                    else
                        TextBox_Firma.Text = rowImportEdit.Firma;

                    if (rowImportEdit.IsNakupCNull())
                        TextBox_NakupC.Text = string.Empty;
                    else
                        TextBox_NakupC.Text = rowImportEdit.NakupC.ToString();

                    if (rowImportEdit.IsRefCMNull())
                        TextBox_RefCM.Text = string.Empty;
                    else
                        TextBox_RefCM.Text = rowImportEdit.RefCM.ToString();

                    if (rowImportEdit.IsCmKursNull())
                        TextBox_CmKurs.Text = string.Empty;
                    else
                        TextBox_CmKurs.Text = rowImportEdit.CmKurs.ToString();

                    if (rowImportEdit.IsEANNull())
                        TextBox_EAN.Text = string.Empty;
                    else
                        TextBox_EAN.Text = rowImportEdit.EAN;

                    if (rowImportEdit.IsMJEANNull())
                        TextBox_MJEAN.Text = string.Empty;
                    else
                        TextBox_MJEAN.Text = rowImportEdit.MJEAN;

                    if (rowImportEdit.IsMJkoefEANNull())
                        TextBox_MJkoefEAN.Text = string.Empty;
                    else
                        TextBox_MJkoefEAN.Text = rowImportEdit.MJkoefEAN.ToString();

                    if (rowImportEdit.IsPoznNull())
                        TextBox_Pozn.Text = string.Empty;
                    else
                        TextBox_Pozn.Text = rowImportEdit.Pozn;

                }
                else
                {
                    
                }
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
                decimal test;

               
                if (string.IsNullOrEmpty(TextBox_RefAg.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_RefAg, "Musíte zadat Odkaz na položku");
                }

                if (string.IsNullOrEmpty(TextBox_ID_sSklad.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_ID_sSklad, "Musíte zadat Odkaz na sklad");
                }

                if (string.IsNullOrEmpty(TextBox_IDS_SKz.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_IDS_SKz, "Musíte zadat Kód karty");
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

        private void FormImport_SKzNC_Edit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormImport_SKzNC_Edit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                //TODO ulozeni editace/vlozeni...
          
                if (rowImportEdit == null)
                {
                    Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel ds2 = new Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel();
                    
                    Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow newIMPORTRow = ds2.FASK_ZASOBY_IMPORT_POHODA_SKzNC.NewFASK_ZASOBY_IMPORT_POHODA_SKzNCRow();

                    //newIMPORTRow.DEX_ROW_ID = TextBox_DEX_ROW_ID.Text;
                    newIMPORTRow.DefDod = cbox_DefDod.Checked;
                    newIMPORTRow.RefAg = int.Parse(TextBox_RefAg.Text);
                    newIMPORTRow.IDS_SKz = TextBox_IDS_SKz.Text;
                    newIMPORTRow.ID_sSklad = int.Parse(TextBox_ID_sSklad.Text);


                    if (string.IsNullOrEmpty(TextBox_RefAD.Text.Trim()))
                        newIMPORTRow.SetRefADNull();
                    else
                        newIMPORTRow.RefAD = int.Parse(TextBox_RefAD.Text);

                    if (string.IsNullOrEmpty(TextBox_Firma.Text.Trim()))
                        newIMPORTRow.SetFirmaNull();
                    else
                        newIMPORTRow.Firma = TextBox_Firma.Text;

                    if (string.IsNullOrEmpty(TextBox_NakupC.Text.Trim()))
                        newIMPORTRow.SetNakupCNull();
                    else
                        newIMPORTRow.NakupC = decimal.Parse(TextBox_NakupC.Text);

                    if (string.IsNullOrEmpty(TextBox_RefCM.Text.Trim()))
                        newIMPORTRow.SetRefCMNull();
                    else
                        newIMPORTRow.RefCM = int.Parse(TextBox_RefCM.Text);


                    if (string.IsNullOrEmpty(TextBox_CmKurs.Text.Trim()))
                        newIMPORTRow.SetCmKursNull();
                    else
                        newIMPORTRow.CmKurs = decimal.Parse(TextBox_CmKurs.Text);

                    if (string.IsNullOrEmpty(TextBox_EAN.Text.Trim()))
                        newIMPORTRow.SetEANNull();
                    else
                        newIMPORTRow.EAN = TextBox_EAN.Text;

                    if (string.IsNullOrEmpty(TextBox_MJEAN.Text.Trim()))
                        newIMPORTRow.SetMJEANNull();
                    else
                        newIMPORTRow.MJEAN = TextBox_MJEAN.Text;

                    if (string.IsNullOrEmpty(TextBox_MJkoefEAN.Text.Trim()))
                        newIMPORTRow.SetMJkoefEANNull();
                    else
                        newIMPORTRow.MJkoefEAN = decimal.Parse(TextBox_MJkoefEAN.Text);

                    if (string.IsNullOrEmpty(TextBox_Pozn.Text.Trim()))
                        newIMPORTRow.SetPoznNull();
                    else
                        newIMPORTRow.Pozn = TextBox_Pozn.Text;

                    Valid_Status(newIMPORTRow);

                    ds2.FASK_ZASOBY_IMPORT_POHODA_SKzNC.AddFASK_ZASOBY_IMPORT_POHODA_SKzNCRow(newIMPORTRow);

                    

                    if ((provider != null) && (provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC))
                        ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC)provider).UpdateImportPohoda_SKzNC(newIMPORTRow);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IImportnyMustky_UpdateImportPohoda_SKzNC");

                }


                else
                {

                    rowImportEdit.DefDod = cbox_DefDod.Checked;
                    rowImportEdit.RefAg = int.Parse(TextBox_RefAg.Text);
                    rowImportEdit.IDS_SKz = TextBox_IDS_SKz.Text;
                    rowImportEdit.ID_sSklad = int.Parse(TextBox_ID_sSklad.Text);


                    if (string.IsNullOrEmpty(TextBox_RefAD.Text.Trim()))
                        rowImportEdit.SetRefADNull();
                    else
                        rowImportEdit.RefAD = int.Parse(TextBox_RefAD.Text);

                    if (string.IsNullOrEmpty(TextBox_Firma.Text.Trim()))
                        rowImportEdit.SetFirmaNull();
                    else
                        rowImportEdit.Firma = TextBox_Firma.Text;

                    if (string.IsNullOrEmpty(TextBox_NakupC.Text.Trim()))
                        rowImportEdit.SetNakupCNull();
                    else
                        rowImportEdit.NakupC = decimal.Parse(TextBox_NakupC.Text);

                    if (string.IsNullOrEmpty(TextBox_RefCM.Text.Trim()))
                        rowImportEdit.SetRefCMNull();
                    else
                        rowImportEdit.RefCM = int.Parse(TextBox_RefCM.Text);


                    if (string.IsNullOrEmpty(TextBox_CmKurs.Text.Trim()))
                        rowImportEdit.SetCmKursNull();
                    else
                        rowImportEdit.CmKurs = decimal.Parse(TextBox_CmKurs.Text);

                    if (string.IsNullOrEmpty(TextBox_EAN.Text.Trim()))
                        rowImportEdit.SetEANNull();
                    else
                        rowImportEdit.EAN = TextBox_EAN.Text;

                    if (string.IsNullOrEmpty(TextBox_MJEAN.Text.Trim()))
                        rowImportEdit.SetMJEANNull();
                    else
                        rowImportEdit.MJEAN = TextBox_MJEAN.Text;

                    if (string.IsNullOrEmpty(TextBox_MJkoefEAN.Text.Trim()))
                        rowImportEdit.SetMJkoefEANNull();
                    else
                        rowImportEdit.MJkoefEAN = decimal.Parse(TextBox_MJkoefEAN.Text);

                    if (string.IsNullOrEmpty(TextBox_Pozn.Text.Trim()))
                        rowImportEdit.SetPoznNull();
                    else
                        rowImportEdit.Pozn = TextBox_Pozn.Text;

                    Valid_Status(rowImportEdit);

                    if ((provider != null) && (provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC))
                        ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC)provider).UpdateImportPohoda_SKzNC(rowImportEdit);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IImportnyMustky_UpdateImportPohoda_SKzNC");

                } 

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Valid_Status(ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow Row)
        {

            Row.Status_Err = 0;

            #region IDS_SKz
            if (!string.IsNullOrEmpty(Row.IDS_SKz))
            {
                bool state = true;
                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                    state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)provider).EXIST("IDS", "SKz", (object)Row.IDS_SKz);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                if (!state)
                    Row.Status_Err += (int)MyErrorEnum.IDS_SKz_NOT_EXIST_POHODA;
            }
            else
            {
                Row.Status_Err += (int)MyErrorEnum.IDS_SKz_isEMPTY;
            }
            #endregion


            #region ID_sSklad
            if (!Row.IsID_sSkladNull())
            {
                bool state = true;
                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                    state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)provider).EXIST("ID", "sSklad", (object)Row.ID_sSklad);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                if (!state)
                    Row.Status_Err += (int)MyErrorEnum.ID_sSklad_NOT_EXIST_POHODA;
            }
            else
            {
                Row.Status_Err += (int)MyErrorEnum.ID_sSklad_isEMPTY;
            }
            #endregion

            #region RefAD
            if (!Row.IsRefADNull())
            {
                bool state = true;
                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                    state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)provider).EXIST("ID", "AD", (object)Row.RefAD);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                if (!state)
                    Row.Status_Err += (int)MyErrorEnum.RefAD_NOT_EXIST_POHODA;
            }
            #endregion

            #region RefCM
            if (!Row.IsRefCMNull())
            {
                bool state = true;
                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                    state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)provider).EXIST("ID", "sCMeny", (object)Row.RefCM);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                if (!state)
                    Row.Status_Err += (int)MyErrorEnum.RefCM_NOT_EXIST_POHODA;
            }
            #endregion


            #region RefAg
            if (!Row.IsRefAgNull())
            {
                bool state = true;
                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                    state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)provider).EXIST("ID", "SKz", (object)Row.RefAg);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                if (!state)
                    Row.Status_Err += (int)MyErrorEnum.RefAg_NOT_EXIST_POHODA;
            }
            #endregion

        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

    }
}
