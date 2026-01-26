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
    public partial class FormDavkyVydejeEdit : Form
    {
        private Fask.Interfaces.IMES providerSE = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vydej.CZMST_SERow rowDavkaEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        //public Fask.Interfaces.DataSets.Vydej.CZMST_SERow returnrow { get; set; }

        public FormDavkyVydejeEdit()
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

                if (providerSE == null)
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

                rowDavkaEdit.CountEntries = int.Parse(textBox_CountEntries.Text);
                rowDavkaEdit.SOPNUMBE = textBox_SOPNUMBE.Text;
                rowDavkaEdit.ITEMNMBR = textBox_ITEMNMBR.Text;
                rowDavkaEdit.ITEMTYPE = textBox_ITEMTYPE.Text;
                rowDavkaEdit.ITEMDESC = textBox_ITEMDESC.Text;
                rowDavkaEdit.VNDDOCNM = textBox_VNDDOCNM.Text;
                rowDavkaEdit.VNDITNUM = textBox_VNDITNUM.Text;
                rowDavkaEdit.ORD = int.Parse(textBox_ORD.Text);
                rowDavkaEdit.CZ_CarKod = textBox_CZ_CarKod.Text;
                rowDavkaEdit.SKL_ID = textBox_SKL_ID.Text;
                rowDavkaEdit.LOCNCODE = textBox_LOCNCODE.Text;
                rowDavkaEdit.MJ = textBox_MJ.Text;
                rowDavkaEdit.QTYSHPPD = decimal.Parse(textBox_QTYSHPPD.Text);
                rowDavkaEdit.QTYPACK = decimal.Parse(textBox_QTYPACK.Text);
                rowDavkaEdit.CZ_DatVyr_Track = byte.Parse(textBox_CZ_DatVyr_Track.Text);
                rowDavkaEdit.CZ_DatVyr_Delka = short.Parse(textBox_CZ_DatVyr_Delka.Text);
                rowDavkaEdit.CZ_SerNum_Track = byte.Parse(textBox_CZ_SerNum_Track.Text);
                rowDavkaEdit.CZ_SerNum_Delka = short.Parse(textBox_CZ_SerNum_Delka.Text);
                rowDavkaEdit.CZ_SW_Track = byte.Parse(textBox_CZ_SW_Track.Text);
                rowDavkaEdit.CZ_SW_Delka = short.Parse(textBox_CZ_SW_Delka.Text);


                rowDavkaEdit.CZ_Doslo = byte.Parse(textBox_CZ_Doslo.Text);


                rowDavkaEdit.Note = textBox_Note.Text;
                rowDavkaEdit.TYPEPAL = textBox_TYPEPAL.Text;
                if(!string.IsNullOrEmpty(textBox_QTYPAL.Text))
                rowDavkaEdit.QTYPAL = decimal.Parse(textBox_QTYPAL.Text);
                rowDavkaEdit.PRIORITY = byte.Parse(textBox_PRIORITY.Text);
                if (!string.IsNullOrEmpty(textBox_PRINTED.Text))
                    rowDavkaEdit.PRINTED = byte.Parse(textBox_PRINTED.Text);
                if (!string.IsNullOrEmpty(textBox_USERID.Text))
                    rowDavkaEdit.USERID = int.Parse(textBox_USERID.Text);
                //newSERow.DEX_ROW_ID = int.Parse(textBox_DEX_ROW_ID.Text);

                if (!string.IsNullOrEmpty(textBox_Weight.Text))
                    rowDavkaEdit.WEIGHT = decimal.Parse(textBox_Weight.Text);



                


                bool result;
                // je úprava záznamu
                if (rowDavkaEdit != null)
                {
                    //((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).UpdateUzivatel(newUzivatelRow);

                    if ((providerSE != null) && (providerSE is Fask.Interfaces.Vydej.IVydej2_UpdateSE))
                        result = ((Fask.Interfaces.Vydej.IVydej2_UpdateSE)providerSE).UpdateSE(rowDavkaEdit);
                    else
                        throw new NotImplementedException("Provider neimplementuje IVydej2_UpdateSE.");
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
                    errorProvider1.SetError(textBox_CountEntries, "Musíte zadat číslo dávky");//*
                }
                if (string.IsNullOrEmpty(textBox_SOPNUMBE.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_SOPNUMBE, "Musíte zadat číslo dokladu");//*
                }
                if (string.IsNullOrEmpty(textBox_ORD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ORD, "Musíte zadat číslo řádku dokladu");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_CarKod.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_CarKod, "Musíte zadat vlastní čárový kód");//*
                }
                if (string.IsNullOrEmpty(textBox_MJ.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_MJ, "Musíte zadat měrnou jednotku");//*
                }
                if (string.IsNullOrEmpty(textBox_QTYSHPPD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYSHPPD, "Musíte zadat množství");//*
                }
                if (string.IsNullOrEmpty(textBox_QTYPACK.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYPACK, "Musíte zadat množství v balení");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_DatVyr_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_DatVyr_Track, "Musíte zadat příznak sledování data výroby");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_DatVyr_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_DatVyr_Delka, "Musíte zadat počet znaků data výroby");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_SerNum_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SerNum_Track, "Musíte zadat příznak sledování SN");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_SerNum_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SerNum_Delka, "Musíte zadat počet znaků výr.čísla");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_SW_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SW_Track, "Musíte zadat příznak sledování - atributu");//*
                }
                if (string.IsNullOrEmpty(textBox_CZ_SW_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SW_Delka, "Musíte zadat počet znaků atributu");//*
                }


                string input = textBox_CZ_Doslo.Text.Trim();
                int value;

                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out value))
                {
                    errorProvider1.SetError(textBox_CZ_Doslo, "Zadejte celé číslo.");
                }
                else if (!(value < 200 || value == 201 || value == 255))
                {
                    errorProvider1.SetError(textBox_CZ_Doslo, "Nepovolená hodnota příznak řidící dávky");
                }
                else
                {
                    errorProvider1.SetError(textBox_CZ_Doslo, "");
                }





                if (string.IsNullOrEmpty(textBox_DEX_ROW_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_DEX_ROW_ID, "Musíte zadat pořadí záznamu v databázi");//*
                }
                if (string.IsNullOrEmpty(textBox_ITEMTYPE.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ITEMTYPE, "Musíte zadat typ položka");//
                }
                if (string.IsNullOrEmpty(textBox_PRIORITY.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_PRIORITY, "Musíte zadat prioritu");//
                }

                if (string.IsNullOrEmpty(textBox_Weight.Text))
                {
                    if (textBox_Weight.Text == string.Empty)
                    {

                    }
                    else
                    {

                        errorProvider1.SetError(textBox_Weight, "Musíte zadat váhu");//
                    }
                }




                // validace id
                #region old
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



                //if (string.IsNullOrEmpty(textBox_ITEMDESC.Text.Trim()))
                //{
                //    errorProvider1.SetError(textBox_ITEMDESC, "Musíte zadat příjmení");
                //}

                //// kontrola unikatnosti loginu
                //if (string.IsNullOrEmpty(textBox_SOPNUMBE.Text.Trim()))
                //{
                //    errorProvider1.SetError(textBox_SOPNUMBE, "Musíte zadat LOGIN");
                //}
                //else
                //{
                //    Fask.Interfaces.Filtry.UzivateleListFiltr filtr = new Fask.Interfaces.Filtry.UzivateleListFiltr();
                //    filtr.UserLogin = textBox_SOPNUMBE.Text.Trim();


                //} 
                #endregion
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
                
                if (providerSE == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vydej.IVydej2).IsAssignableFrom(t))
                            {
                                providerSE = (Fask.Interfaces.Vydej.IVydej2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSE != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSE.InitProvider();
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

                    textBox_CountEntries.Text = rowDavkaEdit.CountEntries.ToString();
                    textBox_SOPNUMBE.Text = rowDavkaEdit.SOPNUMBE.Trim();
                    textBox_ITEMNMBR.Text = rowDavkaEdit.IsITEMNMBRNull() ? string.Empty : rowDavkaEdit.ITEMNMBR.Trim();
                    textBox_ITEMTYPE.Text = rowDavkaEdit.ITEMTYPE.Trim();
                    textBox_ITEMDESC.Text = rowDavkaEdit.IsITEMDESCNull() ? string.Empty : rowDavkaEdit.ITEMDESC.Trim();
                    textBox_VNDDOCNM.Text = rowDavkaEdit.IsVNDDOCNMNull() ? string.Empty : rowDavkaEdit.VNDDOCNM.Trim();
                    textBox_VNDITNUM.Text = rowDavkaEdit.IsVNDITNUMNull() ? string.Empty : rowDavkaEdit.VNDITNUM.Trim();
                    textBox_ORD.Text = rowDavkaEdit.ORD.ToString();
                    textBox_CZ_CarKod.Text = rowDavkaEdit.CZ_CarKod.Trim();
                    textBox_SKL_ID.Text = rowDavkaEdit.IsSKL_IDNull() ? string.Empty : rowDavkaEdit.SKL_ID.Trim();
                    textBox_LOCNCODE.Text = rowDavkaEdit.IsLOCNCODENull() ? string.Empty : rowDavkaEdit.LOCNCODE.Trim();
                    textBox_MJ.Text = rowDavkaEdit.MJ.Trim();
                    textBox_QTYSHPPD.Text = rowDavkaEdit.QTYSHPPD.ToString();
                    textBox_QTYPACK.Text = rowDavkaEdit.QTYPACK.ToString();
                    textBox_CZ_DatVyr_Track.Text = rowDavkaEdit.CZ_DatVyr_Track.ToString();
                    textBox_CZ_DatVyr_Delka.Text = rowDavkaEdit.CZ_DatVyr_Delka.ToString();
                    textBox_CZ_SerNum_Track.Text = rowDavkaEdit.CZ_SerNum_Track.ToString();
                    textBox_CZ_SerNum_Delka.Text = rowDavkaEdit.CZ_SerNum_Delka.ToString();
                    textBox_CZ_SW_Track.Text = rowDavkaEdit.CZ_SW_Track.ToString();
                    textBox_CZ_SW_Delka.Text = rowDavkaEdit.CZ_SW_Delka.ToString();
                    textBox_CZ_Doslo.Text = rowDavkaEdit.CZ_Doslo.ToString();
                    textBox_Note.Text = rowDavkaEdit.IsNoteNull() ? string.Empty : rowDavkaEdit.Note.Trim();
                    textBox_TYPEPAL.Text = rowDavkaEdit.IsTYPEPALNull() ? string.Empty : rowDavkaEdit.TYPEPAL.Trim();
                    textBox_QTYPAL.Text = rowDavkaEdit.IsQTYPALNull() ? string.Empty : rowDavkaEdit.QTYPAL.ToString();
                    textBox_PRIORITY.Text = rowDavkaEdit.PRIORITY.ToString();
                    textBox_PRINTED.Text = rowDavkaEdit.IsPRINTEDNull() ? string.Empty : rowDavkaEdit.PRINTED.ToString();
                    textBox_USERID.Text = rowDavkaEdit.IsUSERIDNull() ? string.Empty : rowDavkaEdit.USERID.ToString();
                    textBox_DEX_ROW_ID.Text = rowDavkaEdit.DEX_ROW_ID.ToString();

                    textBox_Weight.Text = rowDavkaEdit.IsWEIGHTNull() ? string.Empty : rowDavkaEdit.WEIGHT.ToString();
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
                    else if (textBox_CZ_SerNum_Track == tb)  // oznaceni
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
