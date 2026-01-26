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

namespace Konzola.Vydej
{
    public partial class FormDavkyVydejSIEdit : Form
    {
        private Fask.Interfaces.IMES providerSI = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vydej.CZMST_SIRow rowDavkaEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        //public Fask.Interfaces.DataSets.Vydej.CZMST_SERow returnrow { get; set; }

        public FormDavkyVydejSIEdit()
        {
            InitializeComponent();            
        }

        private void FormDavkyVydejeEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormDavkyVydejeEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerSI == null)
                    throw new Exception("Provider 'Nasnimane vydej' není inicializován");

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

        private void FormDavkyVydejeEdit_KeyDown(object sender, KeyEventArgs e)
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


                //Fask.Interfaces.DataSets.Vydej ds2 = new Fask.Interfaces.DataSets.Vydej();
                //Fask.Interfaces.DataSets.Vydej.CZMST_SERow newSERow = ds2.CZMST_SE.NewCZMST_SERow();

                rowDavkaEdit.ODBER_ID = textBox_ODBER_ID.Text;
                rowDavkaEdit.TYPEPAL = textBox_TYPEPAL.Text;
                rowDavkaEdit.NMBRPAL = textBox_NMBRPAL.Text;
                if (!string.IsNullOrEmpty(textBox_PRINTED.Text))
                    rowDavkaEdit.PRINTED = byte.Parse(textBox_PRINTED.Text);

                rowDavkaEdit.ITEMCODE = textBox_ITEMCODE.Text;

                if (!string.IsNullOrEmpty(textBox_WEIGHT.Text))
                    rowDavkaEdit.WEIGHT = decimal.Parse(textBox_WEIGHT.Text);
                if (!string.IsNullOrEmpty(textBox_Expirace.Text))
                    rowDavkaEdit.Expirace = DateTime.Parse(textBox_Expirace.Text);
                //if (!string.IsNullOrEmpty(textBox_CZ_Doslo.Text))
                //    rowDavkaEdit.CZ_Doslo = byte.Parse(textBox_CZ_Doslo.Text);
                rowDavkaEdit.SOPNUMBE = textBox_SOPNUMBE.Text;

                rowDavkaEdit.CountEntries = int.Parse(textBox_CountEntries.Text);
               
                rowDavkaEdit.ITEMNMBR = textBox_ITEMNMBR.Text;
                //rowDavkaEdit.ITEMTYPE = textBox_ITEMTYPE.Text;
                //rowDavkaEdit.ITEMDESC = textBox_QTYSHPPDMJ.Text;
                rowDavkaEdit.VNDDOCNM = textBox_VNDDOCNM.Text;
                rowDavkaEdit.VNDITNUM = textBox_VNDITNUM.Text;
                rowDavkaEdit.ORD = int.Parse(textBox_ORD.Text);
                rowDavkaEdit.CZ_CarKod = textBox_CZ_CarKod.Text;
                rowDavkaEdit.SKL_ID = textBox_SKL_ID.Text;
                rowDavkaEdit.LOCNCODE = textBox_LOCNCODE.Text;
                rowDavkaEdit.MJ = textBox_MJ.Text;
                rowDavkaEdit.QTYSHPPD = decimal.Parse(textBox_QTYSHPPD.Text);
                rowDavkaEdit.QTYPACK = decimal.Parse(textBox_QTYPACK.Text);
                rowDavkaEdit.QTYSHPPDMJ = decimal.Parse(textBox_QTYSHPPDMJ.Text);
                rowDavkaEdit.SERLTNUM = textBox_SERLTNUM.Text;
                rowDavkaEdit.KOD_SW = textBox_KOD_SW.Text;
                rowDavkaEdit.DAT_VYROBY = textBox_DAT_VYROBY.Text;
                rowDavkaEdit.DATEDONE = textBox_DATEDONE.Text;
                rowDavkaEdit.TIMEDONE = textBox_TIMEDONE.Text;
                rowDavkaEdit.INPUT_MODE = byte.Parse(textBox_INPUT_MODE.Text);
                rowDavkaEdit.REZ_1 = textBox_REZ_1.Text;// textBox_Note.Text;
                rowDavkaEdit.REZ_2 = textBox_REZ_2.Text;// textBox_TYPEPAL.Text;
                rowDavkaEdit.ID_TERMINAL = int.Parse(textBox_ID_TERMINAL.Text);
                //rowDavkaEdit.PRIORITY = byte.Parse(textBox_PRIORITY.Text);
                //rowDavkaEdit.PRINTED = byte.Parse(textBox_PRINTED.Text);
                rowDavkaEdit.USER_ID = int.Parse(textBox_USER_ID.Text);
                //rowDavkaEdit.DEX_ROW_ID = int.Parse(textBox_DEX_ROW_ID.Text);

                bool result;
                // je úprava záznamu
                if (rowDavkaEdit != null)
                {
                    //((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).UpdateUzivatel(newUzivatelRow);

                    if ((providerSI != null) && (providerSI is Fask.Interfaces.Vydej.IVydej2_UpdateSI))
                        result = ((Fask.Interfaces.Vydej.IVydej2_UpdateSI)providerSI).UpdateSI(rowDavkaEdit);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPrijem2_UpdatePI.");
                }
                //else   // nový záznam
                //{
                //    //((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).InsertUzivatel(newUzivatelRow);



                //    if ((providerSE != null) && (providerSE is Fask.Interfaces.Vydej.IVydej2_InsertSE))
                //        result = ((Fask.Interfaces.Vydej.IVydej2_InsertSE)providerSE).InsertSE(rowDavkaEdit);
                //    else
                //        throw new NotImplementedException("Provider neimplementuje IVydej2_InsertSE.");

                //}

                //ds2.CZMST_SE.AddCZMST_SERow(rowDavkaEdit);



                // nejde pouzit, protoze pri opetovne editaci zaznamu by byl problem s porovnanim s aktualnim v DB (char x varchar)
                //returnrow = newUzivatelRow;

                // opetovne nacteni uzivatele
                //returnrow = ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatelByID(newUzivatelRow.ID);

                //Fask.Interfaces.Filtry.VydejDavkyFiltr filter = new Fask.Interfaces.Filtry.VydejDavkyFiltr();
                //filter.CountEntries = newSERow.CountEntries.ToString().Trim();
                //filter.Sopnumbe = newSERow.SOPNUMBE.Trim();

                //Fask.Interfaces.DataSets.Vydej data;

                //if ((providerSE != null) && (providerSE is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky))
                //    data = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky)providerSE).GetFiltrovaneDavky(filter);
                //else
                //    throw new NotImplementedException("Provider neimplementuje IVydej2_GetFiltrovaneDavky.");


                //this.returnrow = data.CZMST_SE[0];

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

                //CountEntries
                //SOPNUMBE
                //ITEMNMBR
                //ITEMTYPE
                //ITEMDESC
                //VNDDOCNM
                //VNDITNUM
                //ORD
                //CZ_CarKod
                //SKL_ID
                //LOCNCODE
                //MJ
                //QTYSHPPD
                //QTYPACK
                //CZ_DatVyr_Track
                //CZ_DatVyr_Delka
                //CZ_SerNum_Track
                //CZ_SerNum_Delka
                //CZ_SW_Track
                //CZ_SW_Delka
                //CZ_Doslo
                //Note
                //TYPEPAL
                //QTYPAL
                //PRIORITY
                //PRINTED
                //USERID
                //DEX_ROW_ID

                if (string.IsNullOrEmpty(textBox_CountEntries.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CountEntries, "Musíte zadat číslo dávky");//
                }
                if (string.IsNullOrEmpty(textBox_SOPNUMBE.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_SOPNUMBE, "Musíte zadat číslo dokladu");//
                }
                if (string.IsNullOrEmpty(textBox_ORD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ORD, "Musíte zadat číslo řádku dokladu");//
                }
                if (string.IsNullOrEmpty(textBox_MJ.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_MJ, "Musíte zadat měrnou jednotku");//
                }
                if (string.IsNullOrEmpty(textBox_QTYSHPPD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYSHPPD, "Musíte zadat množství");//
                }
                if (string.IsNullOrEmpty(textBox_QTYSHPPDMJ.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYSHPPDMJ, "Musíte zadat množství jednotek");//
                }
                if (string.IsNullOrEmpty(textBox_QTYPACK.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYPACK, "Musíte zadat množství v balení");//
                }
                if (string.IsNullOrEmpty(textBox_SERLTNUM.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_SERLTNUM, "Musíte zadat SN/výr.číslo");//
                }
                if (string.IsNullOrEmpty(textBox_USER_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_USER_ID, "Musíte zadat ID uživatele");//
                }
                if (string.IsNullOrEmpty(textBox_DEX_ROW_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_DEX_ROW_ID, "Musíte zadat pořadí záznamu v databázi");//
                }
                if (string.IsNullOrEmpty(textBox_INPUT_MODE.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_INPUT_MODE, "Musíte zadat způsob zápisu");//
                }
                if (string.IsNullOrEmpty(textBox_ID_TERMINAL.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ID_TERMINAL, "Musíte zadat ID terminálu");//
                }

                //// validace id
                //int id;
                //bool status = Int32.TryParse(textBox_CountEntries.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id);
                //if (!status)
                //{
                //    errorProvider1.SetError(textBox_CountEntries, "ID musí být číslo");
                //}
                //else
                //{

                //    if (id < 1)
                //    {
                //        errorProvider1.SetError(textBox_CountEntries, "ID musí být kladné číslo");
                //    }
                //    else
                //    {
                //    }
                //}



              
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
                
                if (providerSI == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vydej.IVydej2).IsAssignableFrom(t))
                            {
                                providerSI = (Fask.Interfaces.Vydej.IVydej2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSI != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSI.InitProvider();
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
                if (rowDavkaEdit != null)
                {

                    textBox_ORD.Enabled = false;

                    textBox_ODBER_ID.Text= rowDavkaEdit.IsODBER_IDNull() ? string.Empty : rowDavkaEdit.ODBER_ID;
                    textBox_TYPEPAL.Text= rowDavkaEdit.IsTYPEPALNull() ? string.Empty : rowDavkaEdit.TYPEPAL;
                    textBox_NMBRPAL.Text= rowDavkaEdit.IsNMBRPALNull() ? string.Empty : rowDavkaEdit.NMBRPAL;
                    textBox_PRINTED.Text= rowDavkaEdit.IsPRINTEDNull() ? string.Empty : rowDavkaEdit.PRINTED.ToString();
                    textBox_ITEMCODE.Text= rowDavkaEdit.IsITEMCODENull() ? string.Empty : rowDavkaEdit.ITEMCODE;
                    textBox_WEIGHT.Text= rowDavkaEdit.IsWEIGHTNull() ? string.Empty : rowDavkaEdit.WEIGHT.ToString();
                    textBox_Expirace.Text= rowDavkaEdit.IsExpiraceNull() ? string.Empty : rowDavkaEdit.Expirace.ToString();
                    //textBox_CZ_Doslo.Text = rowDavkaEdit.IsCZ_DosloNull() ? string.Empty :  rowDavkaEdit.CZ_Doslo.ToString();

                    textBox_CountEntries.Text = rowDavkaEdit.CountEntries.ToString();
                    textBox_SOPNUMBE.Text = rowDavkaEdit.SOPNUMBE.Trim();
                    textBox_ITEMNMBR.Text = rowDavkaEdit.IsITEMNMBRNull() ? string.Empty : rowDavkaEdit.ITEMNMBR;
                    textBox_QTYSHPPDMJ.Text = rowDavkaEdit.QTYSHPPDMJ.ToString();
                    textBox_VNDDOCNM.Text = rowDavkaEdit.IsVNDDOCNMNull() ? string.Empty : rowDavkaEdit.VNDDOCNM;
                    textBox_VNDITNUM.Text = rowDavkaEdit.IsVNDITNUMNull() ? string.Empty : rowDavkaEdit.VNDITNUM;
                    textBox_ORD.Text = rowDavkaEdit.ORD.ToString();
                    textBox_CZ_CarKod.Text = rowDavkaEdit.IsCZ_CarKodNull() ? string.Empty : rowDavkaEdit.CZ_CarKod;
                    textBox_SKL_ID.Text = rowDavkaEdit.IsSKL_IDNull() ? string.Empty : rowDavkaEdit.SKL_ID;
                    textBox_LOCNCODE.Text = rowDavkaEdit.IsLOCNCODENull() ? string.Empty : rowDavkaEdit.LOCNCODE;
                    textBox_MJ.Text = rowDavkaEdit.MJ.Trim();
                    textBox_QTYSHPPD.Text = rowDavkaEdit.QTYSHPPD.ToString();
                    textBox_QTYPACK.Text = rowDavkaEdit.QTYPACK.ToString();
                   // textBox_CZ_DatVyr_Track.Text = rowDavkaEdit.CZ_DatVyr_Track.ToString();
                    textBox_SERLTNUM.Text = rowDavkaEdit.SERLTNUM;
                    textBox_KOD_SW.Text = rowDavkaEdit.IsKOD_SWNull() ? string.Empty : rowDavkaEdit.KOD_SW;
                    textBox_DAT_VYROBY.Text = rowDavkaEdit.IsDAT_VYROBYNull() ? string.Empty : rowDavkaEdit.DAT_VYROBY;
                    textBox_DATEDONE.Text = rowDavkaEdit.IsDATEDONENull() ? string.Empty : rowDavkaEdit.DATEDONE;
                    textBox_TIMEDONE.Text = rowDavkaEdit.IsTIMEDONENull() ? string.Empty : rowDavkaEdit.TIMEDONE;
                    textBox_INPUT_MODE.Text = rowDavkaEdit.INPUT_MODE.ToString();
                    textBox_REZ_1.Text = rowDavkaEdit.IsREZ_1Null() ? string.Empty : rowDavkaEdit.REZ_1;
                    textBox_REZ_2.Text = rowDavkaEdit.IsREZ_2Null() ? string.Empty : rowDavkaEdit.REZ_2;
                    textBox_ID_TERMINAL.Text = rowDavkaEdit.ID_TERMINAL.ToString();
                    textBox_USER_ID.Text = rowDavkaEdit.USER_ID.ToString();
                    textBox_DEX_ROW_ID.Text = rowDavkaEdit.DEX_ROW_ID.ToString();


                    textBox_GUID.Text = rowDavkaEdit.IsGUIDNull() ? string.Empty : rowDavkaEdit.GUID.ToString();
                    textBox_ITEMTYPE.Text = rowDavkaEdit.IsITEMTYPENull() ? string.Empty : rowDavkaEdit.ITEMTYPE;
                    textBox_ITEMDESC.Text = rowDavkaEdit.IsITEMDESCNull() ? string.Empty : rowDavkaEdit.ITEMDESC;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDavkyVydejeEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormDavkyVydejeEdit_KeyDown_1(object sender, KeyEventArgs e)
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

        private void textBox_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    
                    if (textBox_CountEntries == tb)
                    {
                        text = "";
                    }
                    else if (textBox_KOD_SW == tb)  // oznaceni
                    {
                        text = "";
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }

        }



    }
}
