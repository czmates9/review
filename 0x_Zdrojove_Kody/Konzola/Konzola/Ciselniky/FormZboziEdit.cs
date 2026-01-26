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
    public partial class FormZboziEdit : Form
    {
        private enum FXTS
        {
            Mnozstvi = 0,
            SN = 1,
            Sarze = 2
        }

        private enum TypySledovaniVyroba
        {
            SSPSSV = 0,
            SSV = 1,
            SV = 2
        }

        #region Def. hodnoty

        bool? VPrFVTS_default = null;
        bool? VPrFPTS_default = null;
        bool? VPrFDTS_default = null;
        bool? VPrFITS_default = null;
        bool? VPrFXTS_default = null;

        int? RefVPrFVTS_default = null;
        int? RefVPrFPTS_default = null;
        int? RefVPrFDTS_default = null;
        int? RefVPrFITS_default = null;
        int? RefVPrFXTS_default = null;

        double? VPrTIMEPREP_default = null;
        //double? VPrTIMEUNIT_default = null;
        int? RefVPrTIMEMODE_default = null;

        #endregion


        private Fask.Interfaces.IMES providerZbozi = null;

        /// <summary>
        /// Zboží, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow rowZboziEdit { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormZboziEdit()
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
        private void FormZboziEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormZboziEdit_Resize(null, null);


                    // inicializace providera
                InitProvider();

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zbozi' není inicializován");

                Fask.Interfaces.Classes.Parametry par = null;

                if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry))
                    par = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry)providerZbozi).GetParametry();

                if (par != null)
                {
                    groupBox1.Enabled = !par.POHODA_E1;
                }
                



                ComboBox_RefVPrFDTS.Items.Add(string.Empty);
                ComboBox_RefVPrFITS.Items.Add(string.Empty);
                ComboBox_RefVPrFPTS.Items.Add(string.Empty);
                ComboBox_RefVPrFVTS.Items.Add(string.Empty);
                ComboBox_RefVPrFXTS.Items.Add(string.Empty);

                
                foreach (var item in Enum.GetValues(typeof(FXTS)))
                {
                    ComboBox_RefVPrFDTS.Items.Add(item);
                    ComboBox_RefVPrFITS.Items.Add(item);
                    ComboBox_RefVPrFPTS.Items.Add(item);
                    ComboBox_RefVPrFVTS.Items.Add(item);
                    ComboBox_RefVPrFXTS.Items.Add(item);
                }

                ComboBox_RefVPrFDTS.SelectedItem = string.Empty;
                ComboBox_RefVPrFITS.SelectedItem = string.Empty;
                ComboBox_RefVPrFPTS.SelectedItem = string.Empty;
                ComboBox_RefVPrFVTS.SelectedItem = string.Empty;
                ComboBox_RefVPrFXTS.SelectedItem = string.Empty;

                ComboBox_RefVPrTIMEMODE.Items.Add(string.Empty);
                foreach (var item in Enum.GetValues(typeof(TypySledovaniVyroba)))
                {
                    ComboBox_RefVPrTIMEMODE.Items.Add(item);
                }

                ComboBox_RefVPrTIMEMODE.SelectedItem = string.Empty;

                LoadData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (providerZbozi == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
                            {
                                providerZbozi = (Fask.Interfaces.Ciselniky.Zbozi.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerZbozi != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerZbozi.InitProvider();


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
                if (rowZboziEdit != null)
                {

                    #region Parametry
                    //TextBox_DEX_ROW_ID_Zbozi; //OK
                    //TextBox_ITEMNMBR;         //OK
                    //TextBox_ITEMDESC;         //OK  
                    //TextBox_ITEMCODE;         //OK
                    //TextBox_VNDITNUM;         //OK
                    //TextBox_CZ_CarKod;        //OK
                    //TextBox_LOCNCODE;         //OK
                    //TextBox_SKL_ID;           //OK
                    //TextBox_QTY;              //OK
                    //TextBox_QTYPACK;          //OK
                    //TextBox_MJ;               //OK
                    //TextBox_DMJ;              //OK
                    //TextBox_TAXRATE;          //OK
                    //TextBox_PRICE0;           //OK
                    //TextBox_PRICE1;           //OK
                    //TextBox_PRICE2;           //OK
                    //TextBox_PRICE3;           //OK
                    //TextBox_PRICE4;           //OK
                    //TextBox_PRICE5;           //OK
                    //TextBox_CZ_SerNum_Track;  //OK
                    //TextBox_CZ_SerNum_Delka;  //OK
                    //TextBox_CZ_Rez1_Track;    //OK
                    //TextBox_CZ_Rez2_Track;    //OK
                    //TextBox_CZ_Rez3_Track;    //OK
                    //TextBox_CZ_Rez4_Track;    //OK
                    //TextBox_REZ1;             //OK
                    //TextBox_REZ2;             //OK
                    //TextBox_REZ3;             //OK
                    //TextBox_REZ4;             //OK
                    //TextBox_ODB_ID;           //OK
                    //TextBox_mena_ID;          //OK
                    //TextBox_SERLTNUM;         //OK
                    //TextBox_WEIGHT;           //OK
                    //DTP_TIMEFROM;         //
                    //DTP_TIMETO;           //
                    //DTP_LSTMod;           //
                    //TextBox_loginid;          //

                    //CheckBox_VPrFVTS;          //
                    //CheckBox_VPrFPTS;          //
                    //CheckBox_VPrFDTS;          //
                    //CheckBox_VPrFITS;          //
                    //CheckBox_VPrFXTS;          //
                    //ComboBox_RefVPrFVTS;       //
                    //ComboBox_RefVPrFPTS;       //
                    //ComboBox_RefVPrFDTS;       //
                    //ComboBox_RefVPrFITS;       //
                    //ComboBox_RefVPrFXTS;       //

                    //TextBox_VPrTIMEPREP;      //
                    //TextBox_VPrTIMEUNIT;      //
                    //ComboBox_RefVPrTIMEMODE;   //

                    //TextBox_DEX_ROW_ID_PARAMETRY; //OK

                    #endregion

                    #region FASK_ZASOBY

                    TextBox_ITEMNMBR.Text = rowZboziEdit.ITEMNMBR;
                    TextBox_ITEMDESC.Text = rowZboziEdit.IsITEMDESCNull() ? string.Empty : rowZboziEdit.ITEMDESC;
                    TextBox_ITEMCODE.Text = rowZboziEdit.IsITEMCODENull() ? string.Empty : rowZboziEdit.ITEMCODE;

                    TextBox_VNDITNUM.Text = rowZboziEdit.IsVNDITNUMNull() ? string.Empty : rowZboziEdit.VNDITNUM;
                    TextBox_CZ_CarKod.Text = rowZboziEdit.IsCZ_CarKodNull() ? string.Empty : rowZboziEdit.CZ_CarKod;

                    TextBox_LOCNCODE.Text = rowZboziEdit.IsLOCNCODENull() ? string.Empty : rowZboziEdit.LOCNCODE;
                    TextBox_SKL_ID.Text = rowZboziEdit.IsSKL_IDNull() ? string.Empty : rowZboziEdit.SKL_ID;

                    TextBox_QTY.Text = rowZboziEdit.QTY.ToString();
                    TextBox_QTYPACK.Text = rowZboziEdit.IsQTYPACKNull() ? string.Empty : rowZboziEdit.QTYPACK.ToString();

                    TextBox_MJ.Text = rowZboziEdit.MJ;
                    TextBox_DMJ.Text = rowZboziEdit.DMJ;

                    TextBox_TAXRATE.Text = rowZboziEdit.IsTAXRATENull() ? string.Empty : rowZboziEdit.TAXRATE.ToString();
                    TextBox_PRICE0.Text = rowZboziEdit.IsPRICE0Null() ? string.Empty : rowZboziEdit.PRICE0.ToString();
                    TextBox_PRICE1.Text = rowZboziEdit.IsPRICE1Null() ? string.Empty : rowZboziEdit.PRICE1.ToString();
                    TextBox_PRICE2.Text = rowZboziEdit.IsPRICE2Null() ? string.Empty : rowZboziEdit.PRICE2.ToString();
                    TextBox_PRICE3.Text = rowZboziEdit.IsPRICE3Null() ? string.Empty : rowZboziEdit.PRICE3.ToString();
                    TextBox_PRICE4.Text = rowZboziEdit.IsPRICE4Null() ? string.Empty : rowZboziEdit.PRICE4.ToString();
                    TextBox_PRICE5.Text = rowZboziEdit.IsPRICE5Null() ? string.Empty : rowZboziEdit.PRICE5.ToString();

                    TextBox_CZ_SerNum_Track.Text = rowZboziEdit.CZ_SerNum_Track.ToString();
                    TextBox_CZ_SerNum_Delka.Text = rowZboziEdit.CZ_SerNum_Delka.ToString();

                    TextBox_CZ_Rez1_Track.Text = rowZboziEdit.CZ_Rez1_Track.ToString();
                    TextBox_CZ_Rez2_Track.Text = rowZboziEdit.CZ_Rez2_Track.ToString();
                    TextBox_CZ_Rez3_Track.Text = rowZboziEdit.CZ_Rez3_Track.ToString();
                    TextBox_CZ_Rez4_Track.Text = rowZboziEdit.CZ_Rez4_Track.ToString();

                    TextBox_REZ1.Text = rowZboziEdit.IsREZ1Null() ? string.Empty : rowZboziEdit.REZ1;
                    TextBox_REZ2.Text = rowZboziEdit.IsREZ2Null() ? string.Empty : rowZboziEdit.REZ2;
                    TextBox_REZ3.Text = rowZboziEdit.IsREZ3Null() ? string.Empty : rowZboziEdit.REZ3;
                    TextBox_REZ4.Text = rowZboziEdit.IsREZ4Null() ? string.Empty : rowZboziEdit.REZ4;

                    TextBox_DEX_ROW_ID_Zbozi.Text = rowZboziEdit.DEX_ROW_ID_Zbozi.ToString();
                    TextBox_DEX_ROW_ID_PARAMETRY.Text = rowZboziEdit.IsDEX_ROW_ID_PARAMETRYNull() ? string.Empty : rowZboziEdit.DEX_ROW_ID_PARAMETRY.ToString();

                    TextBox_ODB_ID.Text = rowZboziEdit.IsODB_IDNull() ? string.Empty : rowZboziEdit.ODB_ID;
                    //TextBox_mena.Text = rowZboziEdit.Ismena_IDNull() ? string.Empty : rowZboziEdit.mena_ID;
                    TextBox_SERLTNUM.Text = rowZboziEdit.IsSERLTNUMNull() ? string.Empty : rowZboziEdit.SERLTNUM;
                    TextBox_WEIGHT.Text = rowZboziEdit.IsWEIGHTNull() ? string.Empty : rowZboziEdit.WEIGHT.ToString();

                    //Nové

                    if (rowZboziEdit.IsTIMEFROMNull())
                        DTP_TIMEFROM.Checked = false;
                    else
                    {
                        DTP_TIMEFROM.Checked = true;
                        DTP_TIMEFROM.Value = rowZboziEdit.TIMEFROM;
                    }

                    if (rowZboziEdit.IsTIMETONull())
                        DTP_TIMETO.Checked = false;
                    else
                    {
                        DTP_TIMETO.Checked = true;
                        DTP_TIMETO.Value = rowZboziEdit.TIMETO;
                    }

                    if (!rowZboziEdit.IsLSTModNull())
                        DTP_LSTMod.Value = rowZboziEdit.LSTMod;


                    TextBox_loginid.Text = rowZboziEdit.IsloginidNull() ? string.Empty : rowZboziEdit.loginid.ToString();


                   

                    #endregion

                    #region FASK_ZASOBY_PARAMETRY

                    VPrFVTS_default = CheckBox_VPrFVTS.Checked = rowZboziEdit.IsVPrFVTSNull() ? false : rowZboziEdit.VPrFVTS;
                    VPrFPTS_default = CheckBox_VPrFPTS.Checked = rowZboziEdit.IsVPrFPTSNull() ? false : rowZboziEdit.VPrFPTS;
                    VPrFDTS_default = CheckBox_VPrFDTS.Checked = rowZboziEdit.IsVPrFDTSNull() ? false : rowZboziEdit.VPrFDTS;
                    VPrFITS_default = CheckBox_VPrFITS.Checked = rowZboziEdit.IsVPrFITSNull() ? false : rowZboziEdit.VPrFITS;
                    VPrFXTS_default = CheckBox_VPrFXTS.Checked = rowZboziEdit.IsVPrFXTSNull() ? false : rowZboziEdit.VPrFXTS;
                    

                    if (rowZboziEdit.IsRefVPrFVTSNull())
                        ComboBox_RefVPrFVTS.SelectedItem = string.Empty;
                    else
                    {
                        ComboBox_RefVPrFVTS.SelectedItem = (FXTS)rowZboziEdit.RefVPrFVTS;
                        RefVPrFVTS_default = rowZboziEdit.RefVPrFVTS;
                    }


                    if (rowZboziEdit.IsRefVPrFPTSNull())
                        ComboBox_RefVPrFPTS.SelectedItem = string.Empty;
                    else
                    {
                        ComboBox_RefVPrFPTS.SelectedItem = (FXTS)rowZboziEdit.RefVPrFPTS;
                        RefVPrFPTS_default = rowZboziEdit.RefVPrFPTS;
                    }

                    if (rowZboziEdit.IsRefVPrFDTSNull())
                        ComboBox_RefVPrFDTS.SelectedItem = string.Empty;
                    else
                    {
                        ComboBox_RefVPrFDTS.SelectedItem = (FXTS)rowZboziEdit.RefVPrFDTS;
                        RefVPrFDTS_default = rowZboziEdit.RefVPrFDTS;
                    }

                    if (rowZboziEdit.IsRefVPrFITSNull())
                        ComboBox_RefVPrFITS.SelectedItem = string.Empty;
                    else
                    {
                        ComboBox_RefVPrFITS.SelectedItem = (FXTS)rowZboziEdit.RefVPrFITS;
                        RefVPrFITS_default = rowZboziEdit.RefVPrFITS;
                    }

                    if (rowZboziEdit.IsRefVPrFXTSNull())
                        ComboBox_RefVPrFXTS.SelectedItem = string.Empty;
                    else
                    {
                        ComboBox_RefVPrFXTS.SelectedItem = (FXTS)rowZboziEdit.RefVPrFXTS;
                        RefVPrFXTS_default = rowZboziEdit.RefVPrFXTS;
                    }


                    if(!rowZboziEdit.IsVPrTIMEPREPNull())
                    {
                        TextBox_VPrTIMEPREP.Text = rowZboziEdit.VPrTIMEPREP.ToString();
                        VPrTIMEPREP_default = rowZboziEdit.VPrTIMEPREP;
                    }
                        
                    if(!rowZboziEdit.IsVPrTIMEUNITNull())
                    {
                        TextBox_VPrTIMEUNIT.Text = rowZboziEdit.VPrTIMEUNIT.ToString();
                        VPrTIMEPREP_default = rowZboziEdit.VPrTIMEUNIT;
                    }

           
                    if (rowZboziEdit.IsRefVPrTIMEMODENull())
                        ComboBox_RefVPrTIMEMODE.SelectedItem = string.Empty;
                    else
                    {
                        ComboBox_RefVPrTIMEMODE.SelectedItem = (TypySledovaniVyroba)rowZboziEdit.RefVPrTIMEMODE;
                        RefVPrTIMEMODE_default = rowZboziEdit.RefVPrTIMEMODE;
                    }


                    #endregion

                }
                else
                {

                    

                    VPrFVTS_default = false;
                    VPrFPTS_default = false;
                    VPrFDTS_default = false;
                    VPrFITS_default = false;
                    VPrFXTS_default = false;

                    RefVPrFVTS_default = null;
                    RefVPrFPTS_default = null;
                    RefVPrFDTS_default = null;
                    RefVPrFITS_default = null;
                    RefVPrFXTS_default = null;

                    VPrTIMEPREP_default = null;
                    //VPrTIMEUNIT_default = null;
                    RefVPrTIMEMODE_default = null;

                    TextBox_CZ_Rez1_Track.Text = "0";
                    TextBox_CZ_Rez2_Track.Text = "0";
                    TextBox_CZ_Rez3_Track.Text = "0";
                    TextBox_CZ_Rez4_Track.Text = "0";

                    TextBox_CZ_SerNum_Delka.Text = "0";
                    TextBox_CZ_SerNum_Track.Text = "0";

                    TextBox_SKL_ID.Text = "1";           
                    TextBox_QTY.Text = "0";              
                    TextBox_QTYPACK.Text = "0";          
                    TextBox_MJ.Text = "ks";               
            
                    TextBox_TAXRATE.Text = "0";    
                    
                    TextBox_PRICE0.Text = "0";           
                    TextBox_PRICE1.Text = "0";           
                    TextBox_PRICE2.Text = "0";           
                    TextBox_PRICE3.Text = "0";           
                    TextBox_PRICE4.Text = "0";   
                    TextBox_PRICE5.Text = "0";

                    TextBox_REZ1.Text = "0";
                    TextBox_REZ2.Text = "0";
                    TextBox_REZ3.Text = "0";
                    TextBox_REZ4.Text = "0";
                    
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

                if (string.IsNullOrEmpty(TextBox_ITEMNMBR.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_ITEMNMBR, "Musíte zadat čislo položky");
                }

                if (string.IsNullOrEmpty(TextBox_QTY.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_QTY, "Musíte zadat množství");
                }
                else
                {
                    try { test = decimal.Parse(TextBox_QTY.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_QTY, ex.Message); }
                }

                if (string.IsNullOrEmpty(TextBox_QTYPACK.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_QTYPACK, "Musíte zadat aspon 0 jak default");
                }
                else
                {
                    try { test = decimal.Parse(TextBox_QTYPACK.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_QTYPACK, ex.Message); }
                }

                if (string.IsNullOrEmpty(TextBox_MJ.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_MJ, "Musíte zadat MJ");

                  
                }
                else
                {
                    if (TextBox_MJ.Text.Length > 5)
                    {

                        // MessageBox.Show("Parametr Měrná jednotka překračuje rozsah znaků(5 znaků je max)!", this.Text, MessageBoxButtons.OK);

                        // FlexibleMessageBox.Show("Parametr Měrná jednotka překračuje rozsah znaků(5 znaků je max)!", "Nelze upravit záznam!", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        // Vytvoření nového dialogového okna MessageBox
                        //FlexibleMessageBox.Show("Parametr Měrná jednotka překračuje rozsah znaků (maximálně 5 znaků)!", "Nelze upravit záznam!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        errorProvider1.SetError(TextBox_MJ, "Maximálně 5 znaků!");

                    }
                }

                ///Tato kontrola by mnela byt ale kedže otravuje, a pro exporte se DMJ nedotahuje tak neni...
                //if (string.IsNullOrEmpty(TextBox_DMJ.Text.Trim()))
                //{
                //    errorProvider1.SetError(TextBox_DMJ, "Musíte zadat DMJ");
                //}



                if (string.IsNullOrEmpty(TextBox_CZ_SerNum_Track.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_CZ_SerNum_Track, "Musíte zadat CZ_SerNum_Track");
                }

                if (string.IsNullOrEmpty(TextBox_CZ_SerNum_Delka.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_CZ_SerNum_Delka, "Musíte zadat CZ_SerNum_Delka");
                }

                if (string.IsNullOrEmpty(TextBox_CZ_Rez1_Track.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_CZ_Rez1_Track, "Musíte zadat CZ_Rez1_Track");
                }

                if (string.IsNullOrEmpty(TextBox_CZ_Rez2_Track.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_CZ_Rez2_Track, "Musíte zadat CZ_Rez2_Track");
                }

                if (string.IsNullOrEmpty(TextBox_CZ_Rez3_Track.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_CZ_Rez3_Track, "Musíte zadat CZ_Rez3_Track");
                }

                if (string.IsNullOrEmpty(TextBox_CZ_Rez4_Track.Text.Trim()))
                {
                    errorProvider1.SetError(TextBox_CZ_Rez4_Track, "Musíte zadat CZ_Rez4_Track");
                }


                if (!string.IsNullOrEmpty(TextBox_PRICE0.Text))
                {
                    try { test = decimal.Parse(TextBox_PRICE0.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_PRICE0, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_PRICE1.Text))
                {
                    try { test = decimal.Parse(TextBox_PRICE1.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_PRICE1, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_PRICE2.Text))
                {
                    try { test = decimal.Parse(TextBox_PRICE2.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_PRICE2, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_PRICE3.Text))
                {
                    try { test = decimal.Parse(TextBox_PRICE3.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_PRICE3, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_PRICE4.Text))
                {
                    try { test = decimal.Parse(TextBox_PRICE4.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_PRICE4, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_PRICE5.Text))
                {
                    try { test = decimal.Parse(TextBox_PRICE5.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_PRICE5, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_QTYPACK.Text))
                {
                    try { test = decimal.Parse(TextBox_QTYPACK.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_QTYPACK, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_TAXRATE.Text))
                {
                    try { test = decimal.Parse(TextBox_TAXRATE.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_TAXRATE, ex.Message); }
                }

                if (!string.IsNullOrEmpty(TextBox_WEIGHT.Text))
                {
                    try { test = decimal.Parse(TextBox_WEIGHT.Text); }
                    catch (Exception ex) { errorProvider1.SetError(TextBox_WEIGHT, ex.Message); }
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

        private void FormZboziEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormZboziEdit_KeyDown(object sender, KeyEventArgs e)
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
                #region Insert
                if (rowZboziEdit == null)
                {
                    Fask.Interfaces.DataSets.Zbozi ds2 = new Fask.Interfaces.DataSets.Zbozi();
                    

                    #region FASK_ZASOBY

                    Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow newZboziRow = ds2.FASK_ZASOBY_KONZOLA.NewFASK_ZASOBY_KONZOLARow();

                    newZboziRow.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();
                    newZboziRow.ITEMDESC = string.IsNullOrEmpty(TextBox_ITEMDESC.Text.Trim()) ? string.Empty : TextBox_ITEMDESC.Text.Trim();
                    newZboziRow.ITEMCODE = string.IsNullOrEmpty(TextBox_ITEMCODE.Text.Trim()) ? string.Empty : TextBox_ITEMCODE.Text.Trim();

                    newZboziRow.VNDITNUM = string.IsNullOrEmpty(TextBox_VNDITNUM.Text.Trim()) ? string.Empty : TextBox_VNDITNUM.Text.Trim();
                    newZboziRow.CZ_CarKod = string.IsNullOrEmpty(TextBox_CZ_CarKod.Text.Trim()) ? string.Empty : TextBox_CZ_CarKod.Text.Trim();

                    newZboziRow.LOCNCODE = string.IsNullOrEmpty(TextBox_LOCNCODE.Text.Trim()) ? string.Empty : TextBox_LOCNCODE.Text.Trim();
                    newZboziRow.SKL_ID = string.IsNullOrEmpty(TextBox_SKL_ID.Text.Trim()) ? null : TextBox_SKL_ID.Text.Trim();

                    newZboziRow.QTY = decimal.Parse(TextBox_QTY.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_QTYPACK.Text.Trim()))
                        newZboziRow.QTYPACK = 0;
                    else
                        newZboziRow.QTYPACK = decimal.Parse(TextBox_QTYPACK.Text.Trim());



                    newZboziRow.MJ = TextBox_MJ.Text.Trim();
                    newZboziRow.DMJ = TextBox_DMJ.Text.Trim();

                    if (string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim()))
                        newZboziRow.SetTAXRATENull();
                    else
                        newZboziRow.TAXRATE = decimal.Parse(TextBox_TAXRATE.Text.Trim());


                    if (string.IsNullOrEmpty(TextBox_PRICE0.Text.Trim()))
                        newZboziRow.PRICE0 = 0;
                    else
                        newZboziRow.PRICE0 = decimal.Parse(TextBox_PRICE0.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE1.Text.Trim()))
                        newZboziRow.PRICE1 = 0;
                    else
                        newZboziRow.PRICE1 = decimal.Parse(TextBox_PRICE1.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE2.Text.Trim()))
                        newZboziRow.PRICE2 = 0;
                    else
                        newZboziRow.PRICE2 = decimal.Parse(TextBox_PRICE2.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE3.Text.Trim()))
                        newZboziRow.PRICE3 = 0;
                    else
                        newZboziRow.PRICE3 = decimal.Parse(TextBox_PRICE3.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE4.Text.Trim()))
                        newZboziRow.PRICE4 = 0;
                    else
                        newZboziRow.PRICE4 = decimal.Parse(TextBox_PRICE4.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE5.Text.Trim()))
                        newZboziRow.PRICE5 = 0;
                    else
                        newZboziRow.PRICE5 = decimal.Parse(TextBox_PRICE5.Text.Trim());


                    newZboziRow.CZ_SerNum_Track = byte.Parse(TextBox_CZ_SerNum_Track.Text.Trim());
                    newZboziRow.CZ_SerNum_Delka = short.Parse(TextBox_CZ_SerNum_Delka.Text.Trim());

                    newZboziRow.CZ_Rez1_Track = byte.Parse(TextBox_CZ_Rez1_Track.Text.Trim());
                    newZboziRow.CZ_Rez2_Track = byte.Parse(TextBox_CZ_Rez2_Track.Text.Trim());
                    newZboziRow.CZ_Rez3_Track = byte.Parse(TextBox_CZ_Rez3_Track.Text.Trim());
                    newZboziRow.CZ_Rez4_Track = byte.Parse(TextBox_CZ_Rez4_Track.Text.Trim());

                    newZboziRow.REZ1 = string.IsNullOrEmpty(TextBox_REZ1.Text.Trim()) ? "0" : TextBox_REZ1.Text.Trim();
                    newZboziRow.REZ2 = string.IsNullOrEmpty(TextBox_REZ2.Text.Trim()) ? "0" : TextBox_REZ2.Text.Trim();
                    newZboziRow.REZ3 = string.IsNullOrEmpty(TextBox_REZ3.Text.Trim()) ? "0" : TextBox_REZ3.Text.Trim();
                    newZboziRow.REZ4 = string.IsNullOrEmpty(TextBox_REZ4.Text.Trim()) ? "0" : TextBox_REZ4.Text.Trim();


                    newZboziRow.ODB_ID = string.IsNullOrEmpty(TextBox_ODB_ID.Text.Trim()) ? null : TextBox_ODB_ID.Text.Trim();
                    //newZboziRow.mena_ID = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? null : TextBox_loginid.Text.Trim();
                    newZboziRow.SERLTNUM = string.IsNullOrEmpty(TextBox_SERLTNUM.Text.Trim()) ? string.Empty : TextBox_SERLTNUM.Text.Trim();

                    if (string.IsNullOrEmpty(TextBox_WEIGHT.Text.Trim()))
                        newZboziRow.SetWEIGHTNull();
                    else
                        newZboziRow.WEIGHT = decimal.Parse(TextBox_WEIGHT.Text.Trim());


                    if (DTP_TIMEFROM.Checked)
                        newZboziRow.TIMEFROM = DTP_TIMEFROM.Value;
                    else
                        newZboziRow.SetTIMEFROMNull();

                    if (DTP_TIMETO.Checked)
                        newZboziRow.TIMETO = DTP_TIMETO.Value;
                    else
                        newZboziRow.SetTIMETONull();

                    newZboziRow.LSTMod = DateTime.Now;

                    newZboziRow.loginid = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? string.Empty : TextBox_loginid.Text.Trim();

                    //MaR zmeny
                    ds2.FASK_ZASOBY_KONZOLA.AddFASK_ZASOBY_KONZOLARow(newZboziRow);

                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi))
                        ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi)providerZbozi).InsertZbozi(newZboziRow);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi");

                    #endregion

                    #region FASK_ZASOBY_PARAMETRY

                    #region Porovnani def a aktual

                    bool RefVPrFVTS_default_tmp = false;
                    bool RefVPrFPTS_default_tmp = false;
                    bool RefVPrFDTS_default_tmp = false;
                    bool RefVPrFITS_default_tmp = false;
                    bool RefVPrFXTS_default_tmp = false;

                    RefVPrFVTS_default_tmp = (ComboBox_RefVPrFVTS.SelectedItem != (!RefVPrFVTS_default.HasValue ? (object)string.Empty : RefVPrFVTS_default.Value));
                    RefVPrFPTS_default_tmp = (ComboBox_RefVPrFPTS.SelectedItem != (!RefVPrFPTS_default.HasValue ? (object)string.Empty : RefVPrFPTS_default.Value));
                    RefVPrFDTS_default_tmp = (ComboBox_RefVPrFDTS.SelectedItem != (!RefVPrFDTS_default.HasValue ? (object)string.Empty : RefVPrFDTS_default.Value));
                    RefVPrFITS_default_tmp = (ComboBox_RefVPrFITS.SelectedItem != (!RefVPrFITS_default.HasValue ? (object)string.Empty : RefVPrFITS_default.Value));
                    RefVPrFXTS_default_tmp = (ComboBox_RefVPrFXTS.SelectedItem != (!RefVPrFXTS_default.HasValue ? (object)string.Empty : RefVPrFXTS_default.Value));


                    #endregion


                    if (
                        CheckBox_VPrFVTS.Checked != VPrFVTS_default ||
                        CheckBox_VPrFPTS.Checked != VPrFPTS_default ||
                        CheckBox_VPrFDTS.Checked != VPrFDTS_default ||
                        CheckBox_VPrFITS.Checked != VPrFITS_default ||
                        CheckBox_VPrFXTS.Checked != VPrFXTS_default ||
                        RefVPrFVTS_default_tmp ||
                        RefVPrFPTS_default_tmp ||
                        RefVPrFDTS_default_tmp ||
                        RefVPrFITS_default_tmp ||
                        RefVPrFXTS_default_tmp
                        )
                    {

                        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_PARAMETRY_KONZOLARow newZboziParamRow = ds2.FASK_ZASOBY_PARAMETRY_KONZOLA.NewFASK_ZASOBY_PARAMETRY_KONZOLARow();

                        //newZboziParamRow.DEX_ROW_ID;
                        newZboziParamRow.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();

                        newZboziParamRow.VPrFVTS = CheckBox_VPrFVTS.Checked;
                        newZboziParamRow.VPrFPTS = CheckBox_VPrFPTS.Checked;
                        newZboziParamRow.VPrFDTS = CheckBox_VPrFDTS.Checked;
                        newZboziParamRow.VPrFITS = CheckBox_VPrFITS.Checked;
                        newZboziParamRow.VPrFXTS = CheckBox_VPrFXTS.Checked;

                        if ((ComboBox_RefVPrFVTS.SelectedItem != null) && (ComboBox_RefVPrFVTS.SelectedItem is FXTS))
                            newZboziParamRow.RefVPrFVTS = (int)ComboBox_RefVPrFVTS.SelectedItem;
                        else
                            newZboziParamRow.SetRefVPrFVTSNull();

                        if ((ComboBox_RefVPrFPTS.SelectedItem != null) && (ComboBox_RefVPrFPTS.SelectedItem is FXTS))
                            newZboziParamRow.RefVPrFPTS = (int)ComboBox_RefVPrFPTS.SelectedItem;
                        else
                            newZboziParamRow.SetRefVPrFPTSNull();


                        if ((ComboBox_RefVPrFDTS.SelectedItem != null) && (ComboBox_RefVPrFDTS.SelectedItem is FXTS))
                            newZboziParamRow.RefVPrFDTS = (int)ComboBox_RefVPrFDTS.SelectedItem;
                        else
                            newZboziParamRow.SetRefVPrFDTSNull();

                        if ((ComboBox_RefVPrFITS.SelectedItem != null) && (ComboBox_RefVPrFITS.SelectedItem is FXTS))
                            newZboziParamRow.RefVPrFITS = (int)ComboBox_RefVPrFITS.SelectedItem;
                        else
                            newZboziParamRow.SetRefVPrFITSNull();

                        if ((ComboBox_RefVPrFXTS.SelectedItem != null) && (ComboBox_RefVPrFXTS.SelectedItem is FXTS))
                            newZboziParamRow.RefVPrFXTS = (int)ComboBox_RefVPrFXTS.SelectedItem;
                        else
                            newZboziParamRow.SetRefVPrFXTSNull();


                        if (string.IsNullOrEmpty(TextBox_VPrTIMEPREP.Text.Trim()))
                            newZboziParamRow.SetVPrTIMEPREPNull();
                        else
                            newZboziParamRow.VPrTIMEPREP = double.Parse(TextBox_VPrTIMEPREP.Text.Trim());

                        if (string.IsNullOrEmpty(TextBox_VPrTIMEUNIT.Text.Trim()))
                            newZboziParamRow.SetVPrTIMEUNITNull();
                        else
                            newZboziParamRow.VPrTIMEUNIT = double.Parse(TextBox_VPrTIMEUNIT.Text.Trim());

                        if ((ComboBox_RefVPrTIMEMODE.SelectedItem != null) && (ComboBox_RefVPrTIMEMODE.SelectedItem is TypySledovaniVyroba))
                            newZboziParamRow.RefVPrTIMEMODE = (int)ComboBox_RefVPrTIMEMODE.SelectedItem;
                        else
                            newZboziParamRow.SetRefVPrTIMEMODENull();


                        if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams))
                            ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams)providerZbozi).InsertZboziParams(newZboziParamRow);
                        else
                            throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZboziParams");


                    }
                    #endregion

                }
                #endregion
                #region Update
                else
                {

                    #region FASK_ZASOBY

                    rowZboziEdit.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();
                    rowZboziEdit.ITEMDESC = string.IsNullOrEmpty(TextBox_ITEMDESC.Text) ? string.Empty : TextBox_ITEMDESC.Text.Trim();
                    rowZboziEdit.ITEMCODE = string.IsNullOrEmpty(TextBox_ITEMCODE.Text.Trim()) ? string.Empty : TextBox_ITEMCODE.Text.Trim();

                    rowZboziEdit.VNDITNUM = string.IsNullOrEmpty(TextBox_VNDITNUM.Text) ? string.Empty : TextBox_VNDITNUM.Text.Trim();
                    rowZboziEdit.CZ_CarKod = string.IsNullOrEmpty(TextBox_CZ_CarKod.Text) ? string.Empty : TextBox_CZ_CarKod.Text.Trim();
                    rowZboziEdit.LOCNCODE = string.IsNullOrEmpty(TextBox_LOCNCODE.Text) ? string.Empty : TextBox_LOCNCODE.Text.Trim();
                    rowZboziEdit.SKL_ID = string.IsNullOrEmpty(TextBox_SKL_ID.Text) ? null : TextBox_SKL_ID.Text.Trim();
                    rowZboziEdit.QTY = decimal.Parse(TextBox_QTY.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_QTYPACK.Text))
                        rowZboziEdit.SetQTYPACKNull();
                    else
                        rowZboziEdit.QTYPACK = decimal.Parse(TextBox_QTYPACK.Text.Trim());

                    rowZboziEdit.MJ = TextBox_MJ.Text.Trim();
                    rowZboziEdit.DMJ = TextBox_DMJ.Text.Trim();

                    if (string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim()))
                        rowZboziEdit.SetTAXRATENull();
                    else
                        rowZboziEdit.TAXRATE = decimal.Parse(TextBox_TAXRATE.Text.Trim());


                    if (string.IsNullOrEmpty(TextBox_PRICE0.Text.Trim()))
                        rowZboziEdit.SetPRICE0Null();
                    else
                        rowZboziEdit.PRICE0 = decimal.Parse(TextBox_PRICE0.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE1.Text.Trim()))
                        rowZboziEdit.SetPRICE1Null();
                    else
                        rowZboziEdit.PRICE1 = decimal.Parse(TextBox_PRICE1.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE2.Text.Trim()))
                        rowZboziEdit.SetPRICE2Null();
                    else
                        rowZboziEdit.PRICE2 = decimal.Parse(TextBox_PRICE2.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE3.Text.Trim()))
                        rowZboziEdit.SetPRICE3Null();
                    else
                        rowZboziEdit.PRICE3 = decimal.Parse(TextBox_PRICE3.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE4.Text.Trim()))
                        rowZboziEdit.SetPRICE4Null();
                    else
                        rowZboziEdit.PRICE4 = decimal.Parse(TextBox_PRICE4.Text.Trim());

                    if (string.IsNullOrEmpty(TextBox_PRICE5.Text.Trim()))
                        rowZboziEdit.SetPRICE5Null();
                    else
                        rowZboziEdit.PRICE5 = decimal.Parse(TextBox_PRICE5.Text.Trim());


                    rowZboziEdit.CZ_SerNum_Track = byte.Parse(TextBox_CZ_SerNum_Track.Text.Trim());
                    rowZboziEdit.CZ_SerNum_Delka = short.Parse(TextBox_CZ_SerNum_Delka.Text.Trim());
                    rowZboziEdit.CZ_Rez1_Track = byte.Parse(TextBox_CZ_Rez1_Track.Text.Trim());
                    rowZboziEdit.CZ_Rez2_Track = byte.Parse(TextBox_CZ_Rez2_Track.Text.Trim());
                    rowZboziEdit.CZ_Rez3_Track = byte.Parse(TextBox_CZ_Rez3_Track.Text.Trim());
                    rowZboziEdit.CZ_Rez4_Track = byte.Parse(TextBox_CZ_Rez4_Track.Text.Trim());
                    rowZboziEdit.REZ1 = string.IsNullOrEmpty(TextBox_REZ1.Text.Trim()) ? null : TextBox_REZ1.Text.Trim();
                    rowZboziEdit.REZ2 = string.IsNullOrEmpty(TextBox_REZ2.Text.Trim()) ? null : TextBox_REZ2.Text.Trim();
                    rowZboziEdit.REZ3 = string.IsNullOrEmpty(TextBox_REZ3.Text.Trim()) ? null : TextBox_REZ3.Text.Trim();
                    rowZboziEdit.REZ4 = string.IsNullOrEmpty(TextBox_REZ4.Text.Trim()) ? null : TextBox_REZ4.Text.Trim();

                    rowZboziEdit.ODB_ID = string.IsNullOrEmpty(TextBox_ODB_ID.Text.Trim()) ? null : TextBox_ODB_ID.Text.Trim();
                    //rowZboziEdit.mena_ID = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? null : TextBox_loginid.Text.Trim();
                    rowZboziEdit.SERLTNUM = string.IsNullOrEmpty(TextBox_SERLTNUM.Text.Trim()) ? null : TextBox_SERLTNUM.Text.Trim();


                    if (string.IsNullOrEmpty(TextBox_WEIGHT.Text.Trim()))
                        rowZboziEdit.SetWEIGHTNull();
                    else
                        rowZboziEdit.WEIGHT = decimal.Parse(TextBox_WEIGHT.Text.Trim());

                    if (DTP_TIMEFROM.Checked)
                        rowZboziEdit.TIMEFROM = DTP_TIMEFROM.Value;
                    else
                        rowZboziEdit.SetTIMEFROMNull();

                    if (DTP_TIMETO.Checked)
                        rowZboziEdit.TIMETO = DTP_TIMETO.Value;
                    else
                        rowZboziEdit.SetTIMETONull();

                    rowZboziEdit.LSTMod = DateTime.Now;

                    rowZboziEdit.loginid = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? string.Empty : TextBox_loginid.Text.Trim();


                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi))
                        ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi)providerZbozi).UpdateZbozi(rowZboziEdit);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_UpdateZbozi");

                    #endregion

                    #region FASK_ZASOBY_PARAMETRY

                    #region Porovnani def a aktual

                    bool RefVPrFVTS_default_tmp = false;
                    bool RefVPrFPTS_default_tmp = false;
                    bool RefVPrFDTS_default_tmp = false;
                    bool RefVPrFITS_default_tmp = false;
                    bool RefVPrFXTS_default_tmp = false;

                    RefVPrFVTS_default_tmp = (ComboBox_RefVPrFVTS.SelectedItem != (!RefVPrFVTS_default.HasValue ? (object)string.Empty : RefVPrFVTS_default.Value));
                    RefVPrFPTS_default_tmp = (ComboBox_RefVPrFPTS.SelectedItem != (!RefVPrFPTS_default.HasValue ? (object)string.Empty : RefVPrFPTS_default.Value));
                    RefVPrFDTS_default_tmp = (ComboBox_RefVPrFDTS.SelectedItem != (!RefVPrFDTS_default.HasValue ? (object)string.Empty : RefVPrFDTS_default.Value));
                    RefVPrFITS_default_tmp = (ComboBox_RefVPrFITS.SelectedItem != (!RefVPrFITS_default.HasValue ? (object)string.Empty : RefVPrFITS_default.Value));
                    RefVPrFXTS_default_tmp = (ComboBox_RefVPrFXTS.SelectedItem != (!RefVPrFXTS_default.HasValue ? (object)string.Empty : RefVPrFXTS_default.Value));


                    #endregion


                    if (
                        CheckBox_VPrFVTS.Checked != VPrFVTS_default ||
                        CheckBox_VPrFPTS.Checked != VPrFPTS_default ||
                        CheckBox_VPrFDTS.Checked != VPrFDTS_default ||
                        CheckBox_VPrFITS.Checked != VPrFITS_default ||
                        CheckBox_VPrFXTS.Checked != VPrFXTS_default ||
                        RefVPrFVTS_default_tmp ||
                        RefVPrFPTS_default_tmp ||
                        RefVPrFDTS_default_tmp ||
                        RefVPrFITS_default_tmp ||
                        RefVPrFXTS_default_tmp
                        )
                    {

                      
                        //newZboziParamRow.DEX_ROW_ID;
                        //rowZboziEdit.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();

                        rowZboziEdit.VPrFVTS = CheckBox_VPrFVTS.Checked;
                        rowZboziEdit.VPrFPTS = CheckBox_VPrFPTS.Checked;
                        rowZboziEdit.VPrFDTS = CheckBox_VPrFDTS.Checked;
                        rowZboziEdit.VPrFITS = CheckBox_VPrFITS.Checked;
                        rowZboziEdit.VPrFXTS = CheckBox_VPrFXTS.Checked;

                        if ((ComboBox_RefVPrFVTS.SelectedItem != null) && (ComboBox_RefVPrFVTS.SelectedItem is FXTS))
                            rowZboziEdit.RefVPrFVTS = (int)ComboBox_RefVPrFVTS.SelectedItem;
                        else
                            rowZboziEdit.SetRefVPrFVTSNull();


                        if ((ComboBox_RefVPrFPTS.SelectedItem != null) && (ComboBox_RefVPrFPTS.SelectedItem is FXTS))
                            rowZboziEdit.RefVPrFPTS = (int)ComboBox_RefVPrFPTS.SelectedItem;
                        else
                            rowZboziEdit.SetRefVPrFPTSNull();


                        if ((ComboBox_RefVPrFDTS.SelectedItem != null) && (ComboBox_RefVPrFDTS.SelectedItem is FXTS))
                            rowZboziEdit.RefVPrFDTS = (int)ComboBox_RefVPrFDTS.SelectedItem;
                        else
                            rowZboziEdit.SetRefVPrFDTSNull();


                        if ((ComboBox_RefVPrFITS.SelectedItem != null) && (ComboBox_RefVPrFITS.SelectedItem is FXTS))
                            rowZboziEdit.RefVPrFITS = (int)ComboBox_RefVPrFITS.SelectedItem;
                        else
                            rowZboziEdit.SetRefVPrFITSNull();


                        if ((ComboBox_RefVPrFXTS.SelectedItem != null) && (ComboBox_RefVPrFXTS.SelectedItem is FXTS))
                            rowZboziEdit.RefVPrFXTS = (int)ComboBox_RefVPrFXTS.SelectedItem;
                        else
                            rowZboziEdit.SetRefVPrFXTSNull();


                        if (string.IsNullOrEmpty(TextBox_VPrTIMEPREP.Text.Trim()))
                            rowZboziEdit.SetVPrTIMEPREPNull();
                        else
                            rowZboziEdit.VPrTIMEPREP = double.Parse(TextBox_VPrTIMEPREP.Text.Trim());

                        if (string.IsNullOrEmpty(TextBox_VPrTIMEUNIT.Text.Trim()))
                            rowZboziEdit.SetVPrTIMEUNITNull();
                        else
                            rowZboziEdit.VPrTIMEUNIT = double.Parse(TextBox_VPrTIMEUNIT.Text.Trim());


                        if ((ComboBox_RefVPrTIMEMODE.SelectedItem != null) && (ComboBox_RefVPrTIMEMODE.SelectedItem is TypySledovaniVyroba))
                            rowZboziEdit.RefVPrTIMEMODE = (int)ComboBox_RefVPrTIMEMODE.SelectedItem;
                        else
                            rowZboziEdit.SetRefVPrTIMEMODENull();


                        if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams))
                            ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams)providerZbozi).UpdateZboziParams(rowZboziEdit);
                        else
                            throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_UpdateZboziParams");
                    }
                    #endregion


                } 
                #endregion



                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}
