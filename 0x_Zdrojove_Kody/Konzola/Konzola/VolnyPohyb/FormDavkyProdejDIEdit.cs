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

namespace Konzola.VolnyPohyb
{
    public partial class FormDavkyProdejDIEdit : Form
    {
        private Fask.Interfaces.IMES providerDI = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Prodej.CZMST_DIRow rowDavkaEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        //public Fask.Interfaces.DataSets.Vydej.CZMST_SERow returnrow { get; set; }

        public FormDavkyProdejDIEdit()
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

                if (providerDI == null)
                    throw new Exception("Provider 'Predloha Vydej' není inicializován");

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

                #region TODO MaR 22.2. 2024 dodelat!!! - odkomentuj
                rowDavkaEdit.CountEntries = int.Parse(textBox_CountEntries.Text);
                rowDavkaEdit.ODB_ID = textBox_ODB_ID.Text;
                rowDavkaEdit.STR_ID = textBox_STR_ID.Text;
                rowDavkaEdit.DOC_ID = textBox_DOC_ID.Text;
                rowDavkaEdit.DOC_ID2 = textBox_DOC_ID2.Text;
                rowDavkaEdit.PRAC_ID = textBox_PRAC_ID.Text;
                rowDavkaEdit.ITEMCODE = textBox_ITEMCODE.Text;
                rowDavkaEdit.QTYSHPPDMJ = decimal.Parse(textBox_QTYSHPPDMJ.Text);


                if(string.IsNullOrEmpty(textBox_SERLTNUM.Text))
                rowDavkaEdit.SERLTNUM = string.Empty;
                else
                    rowDavkaEdit.SERLTNUM = textBox_SERLTNUM.Text;


                if (!string.IsNullOrEmpty(textBox_TAXAMPIE.Text))
                    rowDavkaEdit.TAXAMPIE = decimal.Parse(textBox_TAXAMPIE.Text);
                rowDavkaEdit.AMOUNPIE = decimal.Parse(textBox_AMOUNPIE.Text);
                rowDavkaEdit.WITHTAX = byte.Parse(textBox_WITHTAX.Text);
                rowDavkaEdit.PRICEX = byte.Parse(textBox_PRICEX.Text);
                rowDavkaEdit.mena_ID = textBox_mena_ID.Text;
                if(!string.IsNullOrEmpty(textBox_TAXAMPIEM.Text))
                rowDavkaEdit.TAXAMPIEM =  decimal.Parse(textBox_TAXAMPIEM.Text);
                if (!string.IsNullOrEmpty(textBox_AMOUNPIEM.Text))
                    rowDavkaEdit.AMOUNPIEM = decimal.Parse(textBox_AMOUNPIEM.Text);
                rowDavkaEdit.mena_IDM = textBox_mena_IDM.Text;
                rowDavkaEdit.REZ_1 =  textBox_REZ_1.Text;
                rowDavkaEdit.REZ_2 = textBox_REZ_2.Text;
                rowDavkaEdit.REZ_3 =  textBox_REZ_3.Text;
                rowDavkaEdit.REZ_4 =  textBox_REZ_4.Text;
                rowDavkaEdit.USER_ID = int.Parse(textBox_USERID.Text);
                rowDavkaEdit.DATEDONE = textBox_DATEDONE.Text;
                rowDavkaEdit.TIMEDONE = textBox_TIMEDONE.Text;
                rowDavkaEdit.INPUT_MODE = byte.Parse(textBox_INPUT_MODE.Text);
                rowDavkaEdit.ID_TERMINAL = int.Parse(textBox_ID_TERMINAL.Text);
                rowDavkaEdit.LOCNCODEDEST = textBox_LOCNCODEDEST.Text;
                rowDavkaEdit.SKL_ID_DEST = textBox_SKL_ID_DEST.Text;
                if (!string.IsNullOrEmpty(textBox_EXPIRACE.Text))
                    rowDavkaEdit.EXPIRACE = DateTime.Parse( textBox_EXPIRACE.Text);
                rowDavkaEdit.ITEMNMBR = textBox_ITEMNMBR.Text;
                rowDavkaEdit.VNDITNUM = textBox_VNDITNUM.Text;
                //rowDavkaEdit.ORD = int.Parse(textBox_ORD.Text);
                rowDavkaEdit.CZ_CarKod = textBox_CZ_CarKod.Text;
                rowDavkaEdit.SKL_ID = textBox_SKL_ID.Text;
                rowDavkaEdit.LOCNCODE = textBox_LOCNCODE.Text;
                rowDavkaEdit.MJ = textBox_MJ.Text;
                rowDavkaEdit.QTYSHPPD = decimal.Parse(textBox_QTYSHPPD.Text);
                rowDavkaEdit.QTYPACK = decimal.Parse(textBox_QTYPACK.Text);

                if (!string.IsNullOrEmpty(textBox_WEIGHT.Text))
                    rowDavkaEdit.WEIGHT = decimal.Parse(textBox_WEIGHT.Text);
                if (!string.IsNullOrEmpty(textBox_NMBRPAL.Text))
                    rowDavkaEdit.NMBRPAL = textBox_NMBRPAL.Text;
                if (!string.IsNullOrEmpty(textBox_TYPEPAL.Text))
                    rowDavkaEdit.TYPEPAL = textBox_TYPEPAL.Text;
                if (!string.IsNullOrEmpty(textBox_PRINTED.Text))
                    rowDavkaEdit.PRINTED = byte.Parse(textBox_PRINTED.Text);
                if (!string.IsNullOrEmpty(textBox_AttributeToSN.Text))
                    rowDavkaEdit.AttributeToSN = textBox_AttributeToSN.Text;




                #endregion
                //newSERow.DEX_ROW_ID = int.Parse(textBox_DEX_ROW_ID.Text);

                bool result;
                // je úprava záznamu
                if (rowDavkaEdit != null)
                {
                    //((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).UpdateUzivatel(newUzivatelRow);

                    if ((providerDI != null) && (providerDI is Fask.Interfaces.Prodej.IProdej2_UpdateDI))
                        result = ((Fask.Interfaces.Prodej.IProdej2_UpdateDI)providerDI).UpdateDI(rowDavkaEdit);
                    else
                        throw new NotImplementedException("Provider neimplementuje IProdej2_UpdateDI.");
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
                    errorProvider1.SetError(textBox_CountEntries, "Musíte zadat číslo dávky");
                }
                if (string.IsNullOrEmpty(textBox_ITEMNMBR.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ITEMNMBR, "Musíte zadat číslo položky");
                }
                if (string.IsNullOrEmpty(textBox_MJ.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_MJ, "Musíte zadat měrnou jednotku");
                }
                if (string.IsNullOrEmpty(textBox_QTYSHPPD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYSHPPD, "Musíte zadat množství");
                }
                if (string.IsNullOrEmpty(textBox_QTYSHPPDMJ.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYSHPPDMJ, "Musíte zadat množství jednotek");
                }
                //if (string.IsNullOrEmpty(textBox_SERLTNUM.Text.Trim()))
                //{
                //    errorProvider1.SetError(textBox_SERLTNUM, "Musíte zadat Sn/výr.číslo");
                //}
                if (string.IsNullOrEmpty(textBox_DEX_ROW_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_DEX_ROW_ID, "Musíte zadat pořadí záznamu v databázi");
                }
                if (string.IsNullOrEmpty(textBox_INPUT_MODE.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_INPUT_MODE, "Musíte zadat způsob zápisu");
                }
                if (string.IsNullOrEmpty(textBox_ID_TERMINAL.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ID_TERMINAL, "Musíte zadat číslo terminálu");
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
                
                if (providerDI == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Prodej.IProdej2).IsAssignableFrom(t))
                            {
                                providerDI = (Fask.Interfaces.Prodej.IProdej2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerDI != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerDI.InitProvider();
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
                    #region TODO MaR 22.2. 2024 dodelat!!! - odkomentuj
                    textBox_CountEntries.Text = rowDavkaEdit.CountEntries.ToString();
                    textBox_ODB_ID.Text = rowDavkaEdit.IsODB_IDNull() ? string.Empty : rowDavkaEdit.ODB_ID;
                    textBox_STR_ID.Text = rowDavkaEdit.IsSTR_IDNull() ? string.Empty : rowDavkaEdit.STR_ID;
                    textBox_DOC_ID.Text = rowDavkaEdit.IsDOC_IDNull() ? string.Empty : rowDavkaEdit.DOC_ID;
                    textBox_DOC_ID2.Text = rowDavkaEdit.IsDOC_ID2Null() ? string.Empty : rowDavkaEdit.DOC_ID2;
                    textBox_PRAC_ID.Text = rowDavkaEdit.IsPRAC_IDNull() ? string.Empty : rowDavkaEdit.PRAC_ID;
                    textBox_ITEMCODE.Text = rowDavkaEdit.IsITEMCODENull() ? string.Empty : rowDavkaEdit.IsITEMCODENull() ? string.Empty : rowDavkaEdit.ITEMCODE;
                    textBox_QTYSHPPDMJ.Text = rowDavkaEdit.QTYSHPPDMJ.ToString();
                    textBox_SERLTNUM.Text = rowDavkaEdit.SERLTNUM;
                    textBox_TAXAMPIE.Text = rowDavkaEdit.IsTAXAMPIENull() ? string.Empty : rowDavkaEdit.TAXAMPIE.ToString();
                    textBox_AMOUNPIE.Text = rowDavkaEdit.IsAMOUNPIENull() ? string.Empty : rowDavkaEdit.AMOUNPIE.ToString();
                    textBox_WITHTAX.Text = rowDavkaEdit.IsWITHTAXNull() ? string.Empty : rowDavkaEdit.WITHTAX.ToString();
                    textBox_PRICEX.Text = rowDavkaEdit.IsPRICEXNull() ? string.Empty : rowDavkaEdit.PRICEX.ToString();
                    textBox_mena_ID.Text = rowDavkaEdit.Ismena_IDNull() ? string.Empty : rowDavkaEdit.mena_ID;
                    textBox_TAXAMPIEM.Text = rowDavkaEdit.IsTAXAMPIEMNull() ? string.Empty : rowDavkaEdit.TAXAMPIEM.ToString();
                    textBox_AMOUNPIEM.Text = rowDavkaEdit.IsAMOUNPIEMNull() ? string.Empty : rowDavkaEdit.AMOUNPIEM.ToString();
                    textBox_mena_IDM.Text = rowDavkaEdit.Ismena_IDMNull() ? string.Empty : rowDavkaEdit.mena_IDM;
                    textBox_REZ_1.Text = rowDavkaEdit.IsREZ_1Null() ? string.Empty : rowDavkaEdit.REZ_1;
                    textBox_REZ_2.Text = rowDavkaEdit.IsREZ_2Null() ? string.Empty : rowDavkaEdit.REZ_2;
                    textBox_REZ_3.Text = rowDavkaEdit.IsREZ_3Null() ? string.Empty : rowDavkaEdit.REZ_3;
                    textBox_REZ_4.Text = rowDavkaEdit.IsREZ_4Null() ? string.Empty : rowDavkaEdit.REZ_4;
                    textBox_USERID.Text = rowDavkaEdit.USER_ID.ToString();
                    textBox_DATEDONE.Text = rowDavkaEdit.IsDATEDONENull() ? string.Empty : rowDavkaEdit.DATEDONE;
                    textBox_TIMEDONE.Text = rowDavkaEdit.IsTIMEDONENull() ? string.Empty : rowDavkaEdit.TIMEDONE;
                    textBox_INPUT_MODE.Text = rowDavkaEdit.INPUT_MODE.ToString();
                    textBox_ID_TERMINAL.Text = rowDavkaEdit.ID_TERMINAL.ToString();
                    textBox_LOCNCODEDEST.Text = rowDavkaEdit.IsLOCNCODEDESTNull() ? string.Empty : rowDavkaEdit.LOCNCODEDEST;
                    textBox_SKL_ID_DEST.Text = rowDavkaEdit.IsSKL_ID_DESTNull() ? string.Empty : rowDavkaEdit.SKL_ID;

                    textBox_EXPIRACE.Text = rowDavkaEdit.IsEXPIRACENull() ? string.Empty : rowDavkaEdit.EXPIRACE.ToString();


                    textBox_ITEMNMBR.Text = rowDavkaEdit.ITEMNMBR;
                    textBox_VNDITNUM.Text = rowDavkaEdit.IsVNDITNUMNull() ? string.Empty : rowDavkaEdit.VNDITNUM;
                    textBox_CZ_CarKod.Text = rowDavkaEdit.IsCZ_CarKodNull() ? string.Empty : rowDavkaEdit.CZ_CarKod;
                    textBox_SKL_ID.Text = rowDavkaEdit.IsSKL_IDNull() ? string.Empty : rowDavkaEdit.SKL_ID;
                    textBox_LOCNCODE.Text = rowDavkaEdit.IsLOCNCODENull() ? string.Empty : rowDavkaEdit.LOCNCODE;
                    textBox_MJ.Text = rowDavkaEdit.MJ;
                    textBox_QTYSHPPD.Text = rowDavkaEdit.QTYSHPPD.ToString();
                    textBox_QTYPACK.Text = rowDavkaEdit.IsQTYPACKNull() ? string.Empty : rowDavkaEdit.QTYPACK.ToString();
                    textBox_USERID.Text = rowDavkaEdit.IsUSER_IDNull() ? string.Empty : rowDavkaEdit.USER_ID.ToString();
                    textBox_DEX_ROW_ID.Text = rowDavkaEdit.DEX_ROW_ID.ToString();

                    textBox_WEIGHT.Text = rowDavkaEdit.IsWEIGHTNull() ? string.Empty : rowDavkaEdit.WEIGHT.ToString();
                    textBox_NMBRPAL.Text = rowDavkaEdit.IsNMBRPALNull() ? string.Empty : rowDavkaEdit.NMBRPAL.ToString();
                    textBox_TYPEPAL.Text = rowDavkaEdit.IsTYPEPALNull() ? string.Empty : rowDavkaEdit.TYPEPAL.ToString();
                    textBox_PRINTED.Text = rowDavkaEdit.IsPRINTEDNull() ? string.Empty : rowDavkaEdit.PRINTED.ToString();
                    textBox_AttributeToSN.Text = rowDavkaEdit.IsAttributeToSNNull() ? string.Empty : rowDavkaEdit.AttributeToSN.ToString();

                    textBox_ITEMDESC.Text = rowDavkaEdit.IsITEMDESCNull() ? string.Empty : rowDavkaEdit.ITEMDESC.ToString();

                    #endregion
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
                    else if (textBox_SERLTNUM == tb)  // oznaceni
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
