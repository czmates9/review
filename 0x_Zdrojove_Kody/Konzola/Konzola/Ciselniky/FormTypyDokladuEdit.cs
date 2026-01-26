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
    public partial class FormTypyDokladuEdit : Form
    {
        private Fask.Interfaces.IMES provider = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row rowTypDokladuEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row returnrow { get; set; }

        public Dictionary<string, string> Napoveda = null;

        public FormTypyDokladuEdit()
        {
            InitializeComponent();            
        }

        private void FormTypyDokladuEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormTypyDokladuEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (provider == null)
                    throw new Exception("Provider 'Typ Dokladu' není inicializován");

                LoadData();
                if (!NapovedaClass.NapovedacTor(this.Name + ".xml", out Napoveda))
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,"TypDokladu, napoveda se nenačetla");
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

        private void FormTypyDokladuEdit_KeyDown(object sender, KeyEventArgs e)
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


                #region Typy Dokladu

                if (rowTypDokladuEdit == null)
                {

                    Fask.Interfaces.DataSets.TypyDokladu ds2 = new Fask.Interfaces.DataSets.TypyDokladu();
                    Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row Row = ds2.CZMST092.NewCZMST092Row();

                    Row.doc_id2 = textBox_doc_id2.Text;
                    Row.doc_desc = textBox_doc_desc.Text;
                    Row.doc_id = textBox_doc_id.Text;
                    Row.doc_carcode = textBox_doc_carcode.Text;
                    Row.doc_typ = textBox_doc_typ.Text;
                    //Row.cfg_disp_param1 = textBox_cfg_disp_param1.Text;
                    Row.cfg_lokace_ciselnik = string.IsNullOrEmpty(textBox_cfg_lokace_ciselnik.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace_ciselnik.Text);
                    Row.cfg_prac = string.IsNullOrEmpty(textBox_cfg_prac.Text) ? (byte)0 : byte.Parse(textBox_cfg_prac.Text);
                    //Row.cfg_disp_proc = textBox_cfg_disp_proc.Text;
                    Row.cfg_tisk = string.IsNullOrEmpty(textBox_cfg_tisk.Text) ? (byte)0 : byte.Parse(textBox_cfg_tisk.Text);
                    Row.cfg_lokace = string.IsNullOrEmpty(textBox_cfg_lokace.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace.Text);
                    Row.cfg_str = string.IsNullOrEmpty(textBox_cfg_str.Text) ? (byte)0 : byte.Parse(textBox_cfg_str.Text);
                    Row.cfg_lokace_dest = string.IsNullOrEmpty(textBox_cfg_lokace_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace_dest.Text);
                    Row.LOCNCODE = textBox_LOCNCODE.Text;
                    Row.cfg_mena_id = string.IsNullOrEmpty(textBox_cfg_mena_id.Text) ? (byte)0 : byte.Parse(textBox_cfg_mena_id.Text);
                    Row.cfg_prevod_sklad = string.IsNullOrEmpty(textBox_cfg_prevod_sklad.Text) ? (byte)0 : byte.Parse(textBox_cfg_prevod_sklad.Text);
                    Row.cfg_onl_over_lokace = string.IsNullOrEmpty(textBox_cfg_onl_over_lokace.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_over_lokace.Text);
                    Row.cfg_palety = string.IsNullOrEmpty(textBox_cfg_palety.Text) ? (byte)0 : byte.Parse(textBox_cfg_palety.Text);
                    Row.cfg_onl_dop_pal = string.IsNullOrEmpty(textBox_cfg_onl_dop_pal.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_dop_pal.Text);
                    Row.cfg_mn2sn = string.IsNullOrEmpty(textBox_cfg_mn2sn.Text) ? (byte)0 : byte.Parse(textBox_cfg_mn2sn.Text);
                    //Row.DEX_ROW_ID = string.IsNullOrEmpty(XXX.Text) ? (byte)0 : byte.Parse(textBox_DEX_ROW_ID.Text);
                    Row.cfg_tisk_soupis = string.IsNullOrEmpty(textBox_cfg_tisk_soupis.Text) ? (byte)0 : byte.Parse(textBox_cfg_tisk_soupis.Text);
                    Row.cfg_disp = string.IsNullOrEmpty(textBox_cfg_disp.Text) ? (byte)0 : byte.Parse(textBox_cfg_disp.Text);
                    //Row.cfg_disp_param2 = textBox_cfg_disp_param2.Text;
                    Row.SKL_ID = textBox_SKL_ID.Text;
                    Row.cfg_paleta_id = string.IsNullOrEmpty(textBox_cfg_paleta_id.Text) ? (byte)0 : byte.Parse(textBox_cfg_paleta_id.Text);
                    Row.cfg_odb = string.IsNullOrEmpty(textBox_cfg_odb.Text) ? (byte)0 : byte.Parse(textBox_cfg_odb.Text);
                    Row.cfg_zakazka_id = string.IsNullOrEmpty(textBox_cfg_zakazka_id.Text) ? (byte)0 : byte.Parse(textBox_cfg_zakazka_id.Text);
                    Row.cfg_onl_palety_generovat = string.IsNullOrEmpty(textBox_cfg_onl_palety_generovat.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_palety_generovat.Text);
                    Row.cfg_lok_mech_pohyb_type = textBox_cfg_lok_mech_pohyb_type.Text;
                    Row.cfg_onl_dop_lokace_dest = string.IsNullOrEmpty(textBox_cfg_onl_dop_lokace_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_dop_lokace_dest.Text);
                    Row.cfg_mnozstvi_ze_zbozi = string.IsNullOrEmpty(textBox_cfg_mnozstvi_ze_zbozi.Text) ? (byte)0 : byte.Parse(textBox_cfg_mnozstvi_ze_zbozi.Text);
                    Row.cfg_lok_mech_online_pohyby = string.IsNullOrEmpty(textBox_cfg_lok_mech_online_pohyby.Text) ? (byte)0 : byte.Parse(textBox_cfg_lok_mech_online_pohyby.Text);
                    Row.cfg_lok_mech = string.IsNullOrEmpty(textBox_cfg_lok_mech.Text) ? (byte)0 : byte.Parse(textBox_cfg_lok_mech.Text);
                    Row.cfg_tisk_palety = string.IsNullOrEmpty(textBox_cfg_tisk_palety.Text) ? (byte)0 : byte.Parse(textBox_cfg_tisk_palety.Text);
                    Row.cfg_skl_id_dest_prevzit = string.IsNullOrEmpty(textBox_cfg_skl_id_dest_prevzit.Text) ? (byte)0 : byte.Parse(textBox_cfg_skl_id_dest_prevzit.Text);
                    Row.cfg_sklady = string.IsNullOrEmpty(textBox_cfg_sklady.Text) ? (byte)0 : byte.Parse(textBox_cfg_sklady.Text);
                    Row.cfg_onl_over_lokace_dest = string.IsNullOrEmpty(textBox_cfg_onl_over_lokace_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_over_lokace_dest.Text);
                    Row.cfg_generovat_sn = string.IsNullOrEmpty(textBox_cfg_generovat_sn.Text) ? (byte)0 : byte.Parse(textBox_cfg_generovat_sn.Text);
                    Row.cfg_predvyplnit_mnozstvi = string.IsNullOrEmpty(textBox_cfg_predvyplnit_mnozstvi.Text) ? (byte)0 : byte.Parse(textBox_cfg_predvyplnit_mnozstvi.Text);
                    Row.cfg_delka_SN = string.IsNullOrEmpty(textBox_cfg_delka_SN.Text) ? (byte)0 : byte.Parse(textBox_cfg_delka_SN.Text);
                    Row.predvyplnit_locncodedest = textBox_predvyplnit_locncodedest.Text;
                    Row.cfg_sklady_zmena = string.IsNullOrEmpty(textBox_cfg_sklady_zmena.Text) ? (byte)0 : byte.Parse(textBox_cfg_sklady_zmena.Text);
                    Row.cfg_lokace_dest_ciselnik = string.IsNullOrEmpty(textBox_cfg_lokace_dest_ciselnik.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace_dest_ciselnik.Text);
                    Row.cfg_parsovat_ck = string.IsNullOrEmpty(textBox_cfg_parsovat_ck.Text) ? (byte)0 : byte.Parse(textBox_cfg_parsovat_ck.Text);
                    Row.cfg_skl_id_dest = string.IsNullOrEmpty(textBox_cfg_skl_id_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_skl_id_dest.Text);
                    Row.cfg_sn_na_davku = string.IsNullOrEmpty(textBox_cfg_sn_na_davku.Text) ? (byte)0 : byte.Parse(textBox_cfg_sn_na_davku.Text);
                    Row.predvyplnit_skl_id_dest = textBox_predvyplnit_skl_id_dest.Text;

                    Row.cfg_disp_dest = string.IsNullOrEmpty(textBox_cfg_disp_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_disp_dest.Text);
                    Row.cfg_Navrh = string.IsNullOrEmpty(textBox_cfg_Navrh.Text) ? (byte)0 : byte.Parse(textBox_cfg_Navrh.Text);
                    Row.cfg_FIFO_FEFO_check = string.IsNullOrEmpty(textBox_cfg_FIFO_FEFO_check.Text) ? (byte)0 : byte.Parse(textBox_cfg_FIFO_FEFO_check.Text);
                    Row.cfg_sarze_ONOFF = string.IsNullOrEmpty(textBox_cfg_sarze_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_sarze_ONOFF.Text);
                    Row.cfg_sn_ONOFF = string.IsNullOrEmpty(textBox_cfg_sn_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_sn_ONOFF.Text);
                    Row.cfg_expirace_ONOFF = string.IsNullOrEmpty(textBox_cfg_expirace_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_expirace_ONOFF.Text);
                    Row.cfg_AttributeToSN_ONOFF = string.IsNullOrEmpty(textBox_cfg_AttributeToSN_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_AttributeToSN_ONOFF.Text);


                    if ((provider != null) && (provider is Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Insert))
                        ((Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Insert)provider).Insert(Row);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci ITypyDokladu2_Insert");

                }
                else
                {

                    rowTypDokladuEdit.doc_id2 = textBox_doc_id2.Text;
                    rowTypDokladuEdit.doc_desc = textBox_doc_desc.Text;
                    rowTypDokladuEdit.doc_id = textBox_doc_id.Text;
                    rowTypDokladuEdit.doc_carcode = textBox_doc_carcode.Text;
                    rowTypDokladuEdit.doc_typ = textBox_doc_typ.Text;
                   // rowTypDokladuEdit.cfg_disp_param1 = textBox_cfg_disp_param1.Text;
                    rowTypDokladuEdit.cfg_lokace_ciselnik = string.IsNullOrEmpty(textBox_cfg_lokace_ciselnik.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace_ciselnik.Text);
                    rowTypDokladuEdit.cfg_prac = string.IsNullOrEmpty(textBox_cfg_prac.Text) ? (byte)0 : byte.Parse(textBox_cfg_prac.Text);
                   // rowTypDokladuEdit.cfg_disp_proc = textBox_cfg_disp_proc.Text;
                    rowTypDokladuEdit.cfg_tisk = string.IsNullOrEmpty(textBox_cfg_tisk.Text) ? (byte)0 : byte.Parse(textBox_cfg_tisk.Text);
                    rowTypDokladuEdit.cfg_lokace = string.IsNullOrEmpty(textBox_cfg_lokace.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace.Text);
                    rowTypDokladuEdit.cfg_str = string.IsNullOrEmpty(textBox_cfg_str.Text) ? (byte)0 : byte.Parse(textBox_cfg_str.Text);
                    rowTypDokladuEdit.cfg_lokace_dest = string.IsNullOrEmpty(textBox_cfg_lokace_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace_dest.Text);
                    rowTypDokladuEdit.LOCNCODE = textBox_LOCNCODE.Text;
                    rowTypDokladuEdit.cfg_mena_id = string.IsNullOrEmpty(textBox_cfg_mena_id.Text) ? (byte)0 : byte.Parse(textBox_cfg_mena_id.Text);
                    rowTypDokladuEdit.cfg_prevod_sklad = string.IsNullOrEmpty(textBox_cfg_prevod_sklad.Text) ? (byte)0 : byte.Parse(textBox_cfg_prevod_sklad.Text);
                    rowTypDokladuEdit.cfg_onl_over_lokace = string.IsNullOrEmpty(textBox_cfg_onl_over_lokace.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_over_lokace.Text);
                    rowTypDokladuEdit.cfg_palety = string.IsNullOrEmpty(textBox_cfg_palety.Text) ? (byte)0 : byte.Parse(textBox_cfg_palety.Text);
                    rowTypDokladuEdit.cfg_onl_dop_pal = string.IsNullOrEmpty(textBox_cfg_onl_dop_pal.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_dop_pal.Text);
                    rowTypDokladuEdit.cfg_mn2sn = string.IsNullOrEmpty(textBox_cfg_mn2sn.Text) ? (byte)0 : byte.Parse(textBox_cfg_mn2sn.Text);
                    //rowTypDokladuEdit.DEX_ROW_ID = string.IsNullOrEmpty(XXX.Text) ? (byte)0 : byte.Parse(textBox_DEX_ROW_ID.Text);
                    rowTypDokladuEdit.cfg_tisk_soupis = string.IsNullOrEmpty(textBox_cfg_tisk_soupis.Text) ? (byte)0 : byte.Parse(textBox_cfg_tisk_soupis.Text);
                    rowTypDokladuEdit.cfg_disp = string.IsNullOrEmpty(textBox_cfg_disp.Text) ? (byte)0 : byte.Parse(textBox_cfg_disp.Text);
                   // rowTypDokladuEdit.cfg_disp_param2 = textBox_cfg_disp_param2.Text;
                    rowTypDokladuEdit.SKL_ID = textBox_SKL_ID.Text;
                    rowTypDokladuEdit.cfg_paleta_id = string.IsNullOrEmpty(textBox_cfg_paleta_id.Text) ? (byte)0 : byte.Parse(textBox_cfg_paleta_id.Text);
                    rowTypDokladuEdit.cfg_odb = string.IsNullOrEmpty(textBox_cfg_odb.Text) ? (byte)0 : byte.Parse(textBox_cfg_odb.Text);
                    rowTypDokladuEdit.cfg_zakazka_id = string.IsNullOrEmpty(textBox_cfg_zakazka_id.Text) ? (byte)0 : byte.Parse(textBox_cfg_zakazka_id.Text);
                    rowTypDokladuEdit.cfg_onl_palety_generovat = string.IsNullOrEmpty(textBox_cfg_onl_palety_generovat.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_palety_generovat.Text);
                    rowTypDokladuEdit.cfg_lok_mech_pohyb_type = textBox_cfg_lok_mech_pohyb_type.Text;
                    rowTypDokladuEdit.cfg_onl_dop_lokace_dest = string.IsNullOrEmpty(textBox_cfg_onl_dop_lokace_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_dop_lokace_dest.Text);
                    rowTypDokladuEdit.cfg_mnozstvi_ze_zbozi = string.IsNullOrEmpty(textBox_cfg_mnozstvi_ze_zbozi.Text) ? (byte)0 : byte.Parse(textBox_cfg_mnozstvi_ze_zbozi.Text);
                    rowTypDokladuEdit.cfg_lok_mech_online_pohyby = string.IsNullOrEmpty(textBox_cfg_lok_mech_online_pohyby.Text) ? (byte)0 : byte.Parse(textBox_cfg_lok_mech_online_pohyby.Text);
                    rowTypDokladuEdit.cfg_lok_mech = string.IsNullOrEmpty(textBox_cfg_lok_mech.Text) ? (byte)0 : byte.Parse(textBox_cfg_lok_mech.Text);
                    rowTypDokladuEdit.cfg_tisk_palety = string.IsNullOrEmpty(textBox_cfg_tisk_palety.Text) ? (byte)0 : byte.Parse(textBox_cfg_tisk_palety.Text);
                    rowTypDokladuEdit.cfg_skl_id_dest_prevzit = string.IsNullOrEmpty(textBox_cfg_skl_id_dest_prevzit.Text) ? (byte)0 : byte.Parse(textBox_cfg_skl_id_dest_prevzit.Text);
                    rowTypDokladuEdit.cfg_sklady = string.IsNullOrEmpty(textBox_cfg_sklady.Text) ? (byte)0 : byte.Parse(textBox_cfg_sklady.Text);
                    rowTypDokladuEdit.cfg_onl_over_lokace_dest = string.IsNullOrEmpty(textBox_cfg_onl_over_lokace_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_onl_over_lokace_dest.Text);
                    rowTypDokladuEdit.cfg_generovat_sn = string.IsNullOrEmpty(textBox_cfg_generovat_sn.Text) ? (byte)0 : byte.Parse(textBox_cfg_generovat_sn.Text);
                    rowTypDokladuEdit.cfg_predvyplnit_mnozstvi = string.IsNullOrEmpty(textBox_cfg_predvyplnit_mnozstvi.Text) ? (byte)0 : byte.Parse(textBox_cfg_predvyplnit_mnozstvi.Text);
                    rowTypDokladuEdit.cfg_delka_SN = string.IsNullOrEmpty(textBox_cfg_delka_SN.Text) ? (byte)0 : byte.Parse(textBox_cfg_delka_SN.Text);
                    rowTypDokladuEdit.predvyplnit_locncodedest = textBox_predvyplnit_locncodedest.Text;
                    rowTypDokladuEdit.cfg_sklady_zmena = string.IsNullOrEmpty(textBox_cfg_sklady_zmena.Text) ? (byte)0 : byte.Parse(textBox_cfg_sklady_zmena.Text);
                    rowTypDokladuEdit.cfg_lokace_dest_ciselnik = string.IsNullOrEmpty(textBox_cfg_lokace_dest_ciselnik.Text) ? (byte)0 : byte.Parse(textBox_cfg_lokace_dest_ciselnik.Text);
                    rowTypDokladuEdit.cfg_parsovat_ck = string.IsNullOrEmpty(textBox_cfg_parsovat_ck.Text) ? (byte)0 : byte.Parse(textBox_cfg_parsovat_ck.Text);
                    rowTypDokladuEdit.cfg_skl_id_dest = string.IsNullOrEmpty(textBox_cfg_skl_id_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_skl_id_dest.Text);
                    rowTypDokladuEdit.cfg_sn_na_davku = string.IsNullOrEmpty(textBox_cfg_sn_na_davku.Text) ? (byte)0 : byte.Parse(textBox_cfg_sn_na_davku.Text);
                    rowTypDokladuEdit.predvyplnit_skl_id_dest = textBox_predvyplnit_skl_id_dest.Text;

                    rowTypDokladuEdit.cfg_disp_dest = string.IsNullOrEmpty(textBox_cfg_disp_dest.Text) ? (byte)0 : byte.Parse(textBox_cfg_disp_dest.Text);
                    rowTypDokladuEdit.cfg_Navrh = string.IsNullOrEmpty(textBox_cfg_Navrh.Text) ? (byte)0 : byte.Parse(textBox_cfg_Navrh.Text);
                    rowTypDokladuEdit.cfg_FIFO_FEFO_check = string.IsNullOrEmpty(textBox_cfg_FIFO_FEFO_check.Text) ? (byte)0 : byte.Parse(textBox_cfg_FIFO_FEFO_check.Text);
                    rowTypDokladuEdit.cfg_sarze_ONOFF = string.IsNullOrEmpty(textBox_cfg_sarze_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_sarze_ONOFF.Text);
                    rowTypDokladuEdit.cfg_sn_ONOFF = string.IsNullOrEmpty(textBox_cfg_sn_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_sn_ONOFF.Text);
                    rowTypDokladuEdit.cfg_expirace_ONOFF = string.IsNullOrEmpty(textBox_cfg_expirace_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_expirace_ONOFF.Text);
                    rowTypDokladuEdit.cfg_AttributeToSN_ONOFF = string.IsNullOrEmpty(textBox_cfg_AttributeToSN_ONOFF.Text) ? (byte)0 : byte.Parse(textBox_cfg_AttributeToSN_ONOFF.Text);



                    if ((provider != null) && (provider is Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Update))
                        ((Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Update)provider).Update(rowTypDokladuEdit);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci ITypyDokladu2_Update");

                    this.returnrow = rowTypDokladuEdit;
 
                }

                this.DialogResult = DialogResult.OK;

                #endregion

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

                if (string.IsNullOrEmpty(textBox_doc_id.Text.Trim()))
                {
                    errorProvider1.SetError(textBox_doc_id, "Musíte zadat ID");
                }

                //if (string.IsNullOrEmpty(textBox_doc_id2.Text.Trim()))
                //{
                //    errorProvider1.SetError(textBox_doc_id2, "Musíte zadat ID2");
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
                
                if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2)providerAssemlby.CreateInstance(t.FullName);
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

        private void LoadData()
        {
            try
            {
                // je úprava záznamu, dojde k načtení dat
                if (rowTypDokladuEdit != null)
                {
                    textBox_doc_id2.Text =  string.IsNullOrEmpty(rowTypDokladuEdit.doc_id2) ? string.Empty : rowTypDokladuEdit.doc_id2;
                    textBox_doc_desc.Text = rowTypDokladuEdit.Isdoc_descNull() ? string.Empty : rowTypDokladuEdit.doc_desc;
                    textBox_doc_id.Text =   string.IsNullOrEmpty(rowTypDokladuEdit.doc_id) ? string.Empty : rowTypDokladuEdit.doc_id;
                    textBox_doc_carcode.Text = rowTypDokladuEdit.Isdoc_carcodeNull() ? string.Empty : rowTypDokladuEdit.doc_carcode;
                    textBox_doc_typ.Text = rowTypDokladuEdit.Isdoc_typNull() ? string.Empty : rowTypDokladuEdit.doc_typ;
                   // textBox_cfg_disp_param1.Text = rowTypDokladuEdit.Iscfg_disp_param1Null() ? string.Empty : rowTypDokladuEdit.cfg_disp_param1;
                    textBox_cfg_lokace_ciselnik.Text =  rowTypDokladuEdit.Iscfg_lokace_ciselnikNull() ? string.Empty : rowTypDokladuEdit.cfg_lokace_ciselnik.ToString();
                    textBox_cfg_prac.Text = rowTypDokladuEdit.cfg_prac.ToString(); 
                    //textBox_cfg_disp_proc.Text = rowTypDokladuEdit.Iscfg_disp_procNull() ? string.Empty : rowTypDokladuEdit.cfg_disp_proc; //**
                    textBox_cfg_tisk.Text =  rowTypDokladuEdit.cfg_tisk.ToString();
                    textBox_cfg_lokace.Text = rowTypDokladuEdit.Iscfg_lokaceNull() ? string.Empty : rowTypDokladuEdit.cfg_lokace.ToString();
                    textBox_cfg_str.Text =  rowTypDokladuEdit.cfg_str.ToString();
                    textBox_cfg_lokace_dest.Text = rowTypDokladuEdit.cfg_lokace_dest.ToString();
                    textBox_LOCNCODE.Text =  rowTypDokladuEdit.LOCNCODE;
                    textBox_cfg_mena_id.Text = rowTypDokladuEdit.Iscfg_mena_idNull() ? string.Empty : rowTypDokladuEdit.cfg_mena_id.ToString();
                    textBox_cfg_prevod_sklad.Text =  rowTypDokladuEdit.cfg_prevod_sklad.ToString();
                    textBox_cfg_onl_over_lokace.Text = rowTypDokladuEdit.Iscfg_onl_over_lokaceNull() ? string.Empty : rowTypDokladuEdit.cfg_onl_over_lokace.ToString();
                    textBox_cfg_palety.Text =  rowTypDokladuEdit.cfg_palety.ToString();
                    textBox_cfg_onl_dop_pal.Text = rowTypDokladuEdit.Iscfg_onl_dop_palNull()? string.Empty: rowTypDokladuEdit.cfg_onl_dop_pal.ToString();
                    textBox_cfg_mn2sn.Text =  rowTypDokladuEdit.cfg_mn2sn.ToString();
                    textBox_DEX_ROW_ID.Text =  rowTypDokladuEdit.DEX_ROW_ID.ToString();
                    textBox_cfg_tisk_soupis.Text = rowTypDokladuEdit.Iscfg_tisk_soupisNull()? string.Empty: rowTypDokladuEdit.cfg_tisk_soupis.ToString();
                    textBox_cfg_disp.Text = rowTypDokladuEdit.cfg_disp.ToString();
                    //textBox_cfg_disp_param2.Text = rowTypDokladuEdit.Iscfg_disp_param2Null()?string.Empty: rowTypDokladuEdit.cfg_disp_param2 ;
                    textBox_SKL_ID.Text = rowTypDokladuEdit.IsSKL_IDNull()? string.Empty: rowTypDokladuEdit.SKL_ID;
                    textBox_cfg_paleta_id.Text = rowTypDokladuEdit.cfg_paleta_id.ToString();
                    textBox_cfg_odb.Text =  rowTypDokladuEdit.cfg_odb.ToString();
                    textBox_cfg_zakazka_id.Text =  rowTypDokladuEdit.cfg_zakazka_id.ToString();
                    textBox_cfg_onl_palety_generovat.Text = rowTypDokladuEdit.Iscfg_onl_palety_generovatNull()? string.Empty: rowTypDokladuEdit.cfg_onl_palety_generovat.ToString();
                    textBox_cfg_lok_mech_pohyb_type.Text = rowTypDokladuEdit.Iscfg_lok_mech_pohyb_typeNull()?string.Empty: rowTypDokladuEdit.cfg_lok_mech_pohyb_type;
                    textBox_cfg_onl_dop_lokace_dest.Text = rowTypDokladuEdit.Iscfg_onl_dop_lokace_destNull()?string.Empty: rowTypDokladuEdit.cfg_onl_dop_lokace_dest.ToString();
                    textBox_cfg_mnozstvi_ze_zbozi.Text = rowTypDokladuEdit.Iscfg_mnozstvi_ze_zboziNull()?string.Empty: rowTypDokladuEdit.cfg_mnozstvi_ze_zbozi.ToString();
                    textBox_cfg_lok_mech_online_pohyby.Text = rowTypDokladuEdit.Iscfg_lok_mech_online_pohybyNull()?string.Empty: rowTypDokladuEdit.cfg_lok_mech_online_pohyby.ToString();
                    textBox_cfg_lok_mech.Text = rowTypDokladuEdit.Iscfg_lok_mechNull()?string.Empty: rowTypDokladuEdit.cfg_lok_mech.ToString();
                    textBox_cfg_tisk_palety.Text = rowTypDokladuEdit.Iscfg_tisk_paletyNull()?string.Empty: rowTypDokladuEdit.cfg_tisk_palety.ToString();
                    textBox_cfg_skl_id_dest_prevzit.Text = rowTypDokladuEdit.Iscfg_skl_id_dest_prevzitNull()?string.Empty: rowTypDokladuEdit.cfg_skl_id_dest_prevzit.ToString();
                    textBox_cfg_sklady.Text = rowTypDokladuEdit.Iscfg_skladyNull()?string.Empty: rowTypDokladuEdit.cfg_sklady.ToString();
                    textBox_cfg_onl_over_lokace_dest.Text = rowTypDokladuEdit.Iscfg_onl_over_lokace_destNull()?string.Empty: rowTypDokladuEdit.cfg_onl_over_lokace_dest.ToString();
                    textBox_cfg_generovat_sn.Text = rowTypDokladuEdit.Iscfg_generovat_snNull()?string.Empty: rowTypDokladuEdit.cfg_generovat_sn.ToString();
                    textBox_cfg_predvyplnit_mnozstvi.Text = rowTypDokladuEdit.Iscfg_predvyplnit_mnozstviNull()?string.Empty: rowTypDokladuEdit.cfg_predvyplnit_mnozstvi.ToString();
                    textBox_cfg_delka_SN.Text = rowTypDokladuEdit.Iscfg_delka_SNNull()?string.Empty: rowTypDokladuEdit.cfg_delka_SN.ToString();
                    textBox_predvyplnit_locncodedest.Text = rowTypDokladuEdit.Ispredvyplnit_locncodedestNull()?string.Empty: rowTypDokladuEdit.predvyplnit_locncodedest ;
                    textBox_cfg_sklady_zmena.Text = rowTypDokladuEdit.Iscfg_sklady_zmenaNull()?string.Empty: rowTypDokladuEdit.cfg_sklady_zmena.ToString();
                    textBox_cfg_lokace_dest_ciselnik.Text = rowTypDokladuEdit.Iscfg_lokace_ciselnikNull()?string.Empty: rowTypDokladuEdit.cfg_lokace_ciselnik.ToString();
                    textBox_cfg_parsovat_ck.Text = rowTypDokladuEdit.Iscfg_parsovat_ckNull()?string.Empty: rowTypDokladuEdit.cfg_parsovat_ck.ToString();
                    textBox_cfg_skl_id_dest.Text = rowTypDokladuEdit.Iscfg_skl_id_destNull()?string.Empty: rowTypDokladuEdit.cfg_skl_id_dest.ToString();
                    textBox_cfg_sn_na_davku.Text = rowTypDokladuEdit.Iscfg_sn_na_davkuNull()?string.Empty: rowTypDokladuEdit.cfg_sn_na_davku.ToString();
                    textBox_predvyplnit_skl_id_dest.Text = rowTypDokladuEdit.Ispredvyplnit_skl_id_destNull()?string.Empty: rowTypDokladuEdit.predvyplnit_skl_id_dest  ;

                    textBox_cfg_disp_dest.Text = rowTypDokladuEdit.cfg_disp_dest.ToString();
                    textBox_cfg_Navrh.Text = rowTypDokladuEdit.Iscfg_NavrhNull() ? string.Empty : rowTypDokladuEdit.cfg_Navrh.ToString();
                    textBox_cfg_FIFO_FEFO_check.Text = rowTypDokladuEdit.Iscfg_FIFO_FEFO_checkNull() ? string.Empty : rowTypDokladuEdit.cfg_FIFO_FEFO_check.ToString();
                    textBox_cfg_sarze_ONOFF.Text = rowTypDokladuEdit.Iscfg_sarze_ONOFFNull() ? string.Empty : rowTypDokladuEdit.cfg_sarze_ONOFF.ToString();
                    textBox_cfg_sn_ONOFF.Text = rowTypDokladuEdit.Iscfg_sn_ONOFFNull() ? string.Empty : rowTypDokladuEdit.cfg_sn_ONOFF.ToString();
                    textBox_cfg_expirace_ONOFF.Text = rowTypDokladuEdit.Iscfg_expirace_ONOFFNull() ? string.Empty : rowTypDokladuEdit.cfg_expirace_ONOFF.ToString();
                    textBox_cfg_AttributeToSN_ONOFF.Text = rowTypDokladuEdit.Iscfg_AttributeToSN_ONOFFNull() ? string.Empty : rowTypDokladuEdit.cfg_AttributeToSN_ONOFF.ToString();



                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormTypyDokladuEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormTypyDokladuEdit_KeyDown_1(object sender, KeyEventArgs e)
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

                    #region Verze manual

                    //text = TextBoxToTextManual(tb); 
                    
                    #endregion

                    #region Verze automat

                    text = TextBoxToTextAutomat(tb); 

                    #endregion
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }

        }

        private string TextBoxToTextAutomat(TextBox tb)
        {
            
            string Text = string.Empty;
            string NameTB = tb.Name;

            string NameColumn =  NameTB.Replace("textBox_", "");

            if (Napoveda.ContainsKey(NameColumn.Trim()))
            {
                Text = Napoveda[NameColumn];
            }
            
            return Text;
        }



        private string TextBoxToTextManual(TextBox tb)
        {
            string text = string.Empty;
            if (textBox_doc_id2 == tb)
            {
                text = "";
            }
            else if (textBox_doc_desc == tb)
            {
                text = "";
            }
            else if (textBox_doc_id == tb)
            {
                text = "";
            }
            else if (textBox_doc_carcode == tb)
            {
                text = "";
            }
            else if (textBox_doc_typ == tb)
            {
                text = "";
            }
            //else if (textBox_cfg_disp_param1 == tb)
            //{
            //    text = "";
            //}
            else if (textBox_cfg_lokace_ciselnik == tb)
            {
                text = "";
            }
            else if (textBox_cfg_prac == tb)
            {
                text = "";
            }
            //else if (textBox_cfg_disp_proc == tb)
            //{
            //    text = "";
            //}
            else if (textBox_cfg_tisk == tb)
            {
                text = "";
            }
            else if (textBox_cfg_lokace == tb)
            {
                text = "";
            }
            else if (textBox_cfg_str == tb)
            {
                text = "";
            }
            else if (textBox_cfg_lokace_dest == tb)
            {
                text = "";
            }
            else if (textBox_LOCNCODE == tb)
            {
                text = "";
            }
            else if (textBox_cfg_mena_id == tb)
            {
                text = "";
            }
            else if (textBox_cfg_prevod_sklad == tb)
            {
                text = "";
            }
            else if (textBox_cfg_onl_over_lokace == tb)
            {
                text = "";
            }
            else if (textBox_cfg_palety == tb)
            {
                text = "";
            }
            else if (textBox_cfg_onl_dop_pal == tb)
            {
                text = "";
            }
            else if (textBox_cfg_mn2sn == tb)
            {
                text = "";
            }
            else if (textBox_DEX_ROW_ID == tb)
            {
                text = "";
            }
            else if (textBox_cfg_tisk_soupis == tb)
            {
                text = "";
            }
            else if (textBox_cfg_disp == tb)
            {
                text = "";
            }
            //else if (textBox_cfg_disp_param2 == tb)
            //{
            //    text = "";
            //}
            else if (textBox_SKL_ID == tb)
            {
                text = "";
            }
            else if (textBox_cfg_paleta_id == tb)
            {
                text = "";
            }
            else if (textBox_cfg_odb == tb)
            {
                text = "";
            }
            else if (textBox_cfg_zakazka_id == tb)
            {
                text = "";
            }
            else if (textBox_cfg_onl_palety_generovat == tb)
            {
                text = "";
            }
            else if (textBox_cfg_lok_mech_pohyb_type == tb)
            {
                text = "";
            }
            else if (textBox_cfg_onl_dop_lokace_dest == tb)
            {
                text = "";
            }
            else if (textBox_cfg_mnozstvi_ze_zbozi == tb)
            {
                text = "";
            }
            else if (textBox_cfg_lok_mech_online_pohyby == tb)
            {
                text = "";
            }
            else if (textBox_cfg_lok_mech == tb)
            {
                text = "";
            }
            else if (textBox_cfg_tisk_palety == tb)
            {
                text = "";
            }
            else if (textBox_cfg_skl_id_dest_prevzit == tb)
            {
                text = "";
            }
            else if (textBox_cfg_sklady == tb)
            {
                text = "";
            }
            else if (textBox_cfg_onl_over_lokace_dest == tb)
            {
                text = "";
            }
            else if (textBox_cfg_generovat_sn == tb)
            {
                text = "";
            }
            else if (textBox_cfg_predvyplnit_mnozstvi == tb)
            {
                text = "";
            }
            else if (textBox_cfg_delka_SN == tb)
            {
                text = "";
            }
            else if (textBox_predvyplnit_locncodedest == tb)
            {
                text = "";
            }
            else if (textBox_cfg_sklady_zmena == tb)
            {
                text = "";
            }
            else if (textBox_cfg_lokace_dest_ciselnik == tb)
            {
                text = "";
            }
            else if (textBox_cfg_parsovat_ck == tb)
            {
                text = "";
            }
            else if (textBox_cfg_skl_id_dest == tb)
            {
                text = "";
            }
            else if (textBox_cfg_sn_na_davku == tb)
            {
                text = "";
            }
            else if (textBox_predvyplnit_skl_id_dest == tb)
            {
                text = "";
            }
            else if (textBox_cfg_disp_dest == tb)
            {
                text = "";
            }
            else if (textBox_cfg_Navrh == tb)
            {
                text = "";
            }
            else if (textBox_cfg_FIFO_FEFO_check == tb)
            {
                text = "";
            }
            else if (textBox_cfg_sarze_ONOFF == tb)
            {
                text = "";
            }
            else if (textBox_cfg_sn_ONOFF == tb)
            {
                text = "";
            }
            else if (textBox_cfg_expirace_ONOFF == tb)
            {
                text = "";
            }
            else if (textBox_cfg_AttributeToSN_ONOFF == tb)
            {
                text = "";
            }
            return text;
        }
    }
}
