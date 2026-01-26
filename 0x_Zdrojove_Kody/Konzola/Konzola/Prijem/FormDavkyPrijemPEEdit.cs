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

namespace Konzola.Prijem
{
    public partial class FormDavkyPrijemPEEdit : Form
    {
        private Fask.Interfaces.IMES providerPE = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Prijem.CZMST_PERow rowDavkaEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        //public Fask.Interfaces.DataSets.Vydej.CZMST_SERow returnrow { get; set; }

        public FormDavkyPrijemPEEdit()
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

                if (providerPE == null)
                    throw new Exception("Provider 'Predloha prijem' není inicializován");

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
                rowDavkaEdit.PONUMBER = textBox_PONUMBER.Text;
                rowDavkaEdit.ITEMNMBR = textBox_ITEMNMBR.Text;
               // rowDavkaEdit.ITEMTYPE = textBox_ITEMTYPE.Text;
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
                rowDavkaEdit.CZ_REZ1_Track = byte.Parse(textBox_CZ_REZ1_Track.Text);// textBox_Note.Text;
                rowDavkaEdit.CZ_REZ2_Track = byte.Parse(textBox_CZ_REZ2_Track.Text);// textBox_TYPEPAL.Text;
                //rowDavkaEdit.QTYPAL = decimal.Parse(textBox_QTYPAL.Text);
                //rowDavkaEdit.PRIORITY = byte.Parse(textBox_PRIORITY.Text);
                //rowDavkaEdit.PRINTED = byte.Parse(textBox_PRINTED.Text);
                //rowDavkaEdit.USERID = int.Parse(textBox_USERID.Text);
               // rowDavkaEdit.DEX_ROW_ID = int.Parse(textBox_DEX_ROW_ID.Text);

                bool result;
                // je úprava záznamu
                if (rowDavkaEdit != null)
                {
                    //((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).UpdateUzivatel(newUzivatelRow);

                    if ((providerPE != null) && (providerPE is Fask.Interfaces.Prijem.IPrijem2_UpdatePE))
                        result = ((Fask.Interfaces.Prijem.IPrijem2_UpdatePE)providerPE).UpdatePE(rowDavkaEdit);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPrijem2_UpdatePE.");
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
                if (string.IsNullOrEmpty(textBox_PONUMBER.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_PONUMBER, "Musíte zadat číslo dokladu");
                }
                if (string.IsNullOrEmpty(textBox_ORD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_ORD, "Musíte zadat číslo řádku dokladu");
                }
                if (string.IsNullOrEmpty(textBox_CZ_CarKod.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_CarKod, "Musíte zadat vlastní čárový kód");
                }
                if (string.IsNullOrEmpty(textBox_MJ.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_MJ, "Musíte zadat měrnou jednotku");
                }
                if (string.IsNullOrEmpty(textBox_QTYSHPPD.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYSHPPD, "Musíte zadat množství");
                }
                if (string.IsNullOrEmpty(textBox_QTYPACK.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_QTYPACK, "Musíte zadat množství v balení");
                }
                if (string.IsNullOrEmpty(textBox_CZ_DatVyr_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_DatVyr_Track, "Musíte zadat příznak sledování data výroby");
                }
                if (string.IsNullOrEmpty(textBox_CZ_DatVyr_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_DatVyr_Delka, "Musíte zadat počet znaků data výroby");
                }
                if (string.IsNullOrEmpty(textBox_CZ_SerNum_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SerNum_Track, "Musíte zadat příznak sledování SN");
                }
                if (string.IsNullOrEmpty(textBox_CZ_SerNum_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SerNum_Delka, "Musíte zadat počet znaků výr.čísla");
                }
                if (string.IsNullOrEmpty(textBox_CZ_SW_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SW_Track, "Musíte zadat příznak sledování - atributu");
                }
                if (string.IsNullOrEmpty(textBox_CZ_SW_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_SW_Delka, "Musíte zadat počet znaků atributu");
                }
                if (string.IsNullOrEmpty(textBox_CZ_Doslo.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_Doslo, "Musíte zadat příznak řídící dávky");
                }
                if (string.IsNullOrEmpty(textBox_DEX_ROW_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_DEX_ROW_ID, "Musíte zadat pořadí záznamu v databázi");
                }
                if (string.IsNullOrEmpty(textBox_CZ_REZ1_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_REZ1_Track, "Musíte zadat příznak rezervy 1");
                }
                if (string.IsNullOrEmpty(textBox_CZ_REZ2_Track.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_CZ_REZ2_Track, "Musíte zadat příznak rezervy 2");
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
                
                if (providerPE == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Prijem.IPrijem2).IsAssignableFrom(t))
                            {
                                providerPE = (Fask.Interfaces.Prijem.IPrijem2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPE != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPE.InitProvider();
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
                    textBox_PONUMBER.Text = rowDavkaEdit.PONUMBER.Trim();
                    textBox_ITEMNMBR.Text = rowDavkaEdit.ITEMNMBR.Trim();
                    textBox_ITEMDESC.Text = rowDavkaEdit.ITEMDESC.Trim();
                    textBox_VNDDOCNM.Text = rowDavkaEdit.VNDDOCNM.Trim();
                    textBox_VNDITNUM.Text = rowDavkaEdit.VNDITNUM.Trim();
                    textBox_ORD.Text = rowDavkaEdit.ORD.ToString();
                    textBox_CZ_CarKod.Text = rowDavkaEdit.CZ_CarKod.Trim();
                    textBox_SKL_ID.Text = rowDavkaEdit.SKL_ID.Trim();
                    textBox_LOCNCODE.Text = rowDavkaEdit.LOCNCODE.Trim();
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
                    textBox_CZ_REZ1_Track.Text = rowDavkaEdit.CZ_REZ1_Track.ToString();
                    textBox_CZ_REZ2_Track.Text = rowDavkaEdit.CZ_REZ2_Track.ToString();
                    textBox_DEX_ROW_ID.Text = rowDavkaEdit.DEX_ROW_ID.ToString();
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
