using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Globalization;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Vyroba
{
    public partial class FormProductionSourcesEdit2 : Form
    {

        //private Fask.Interfaces.IVyrobaKonzola providerLogins = null;
        private Fask.Interfaces.IMES providerSklady = null;
        private Fask.Interfaces.IMES providerLokace = null;
        private Fask.Interfaces.IMES providerPS = null;
        
        

        #region TableAdapters
        private Fask.Interfaces.DataSets.Vyroba dsVyroba = new Fask.Interfaces.DataSets.Vyroba();
        //Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter taLogins;
        //Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter taVPH;
        //Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter taVPP;
        //Production.DataServices.VyrobaDataSetTableAdapters.MachinesTableAdapter taMachines;
        //Production.DataServices.VyrobaDataSetTableAdapters.OperationsTableAdapter taOperations;
        //Production.DataServices.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter tavMachinesOperations;
        //Production.DataServices.VyrobaDataSetTableAdapters.CorrectsTableAdapter taCorrects;
        //private void InitializeTableAdapters()
        //{
            // naplnění daty pro kontrolu
            //taLogins = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
            //taLogins.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            //taLogins.Fill(ds.Logins);

            //    taVPH = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
            //    taVPH.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //    //taVPH.Fill(ds.CZPRO_VPH);

            //    taVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
            //    taVPP.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //    //taVPP.Fill(ds.CZPRO_VPP);                

            //    taMachines = new Production.DataServices.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            //    taMachines.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //    //taMachines.Fill(ds.Machines);

            //    taOperations = new Production.DataServices.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            //    taOperations.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //    //taOperations.Fill(ds.Operations);

            //    tavMachinesOperations = new Production.DataServices.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter();
            //    tavMachinesOperations.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            //    taCorrects = new Production.DataServices.VyrobaDataSetTableAdapters.CorrectsTableAdapter();
            //    taCorrects.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            //Production.DataServices.VyrobaDataSetTableAdapters.CZMST093TableAdapter taSklady = new Production.DataServices.VyrobaDataSetTableAdapters.CZMST093TableAdapter();
            //taSklady.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //taSklady.Fill(dsVyroba.CZMST093);

            //Production.DataServices.VyrobaDataSetTableAdapters.CZMST094TableAdapter taLokace = new Production.DataServices.VyrobaDataSetTableAdapters.CZMST094TableAdapter();
            //taLokace.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //taLokace.Fill(dsVyroba.CZMST094);
        //}
        #endregion



        /// <summary>
        /// záznam, který se bude upravovat
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow rowProductSources { get; set; }

        /// <summary>
        /// dotazeny Radek z ZASOBY
        /// </summary>
        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow RowCKod_ZASOBY { get; set; }


        public FormProductionSourcesEdit2()
        {
            InitializeComponent();

            //InitializeTableAdapters();

        }


        private void FormProductionEdit_Load(object sender, EventArgs e)
        {
            try
            {

                InitProvider();

                //if (providerLogins == null)
                //    throw new Exception("Provider 'Logins' není inicializován");

                if (providerLokace == null)
                    throw new Exception("Provider 'Lokace' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");


                if (providerLokace is Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)
                    ((Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)providerLokace).Fill(dsVyroba);
                else
                    throw new Exception("ILokace2_Fill not implementet");

                if (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)
                    ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)providerSklady).Sklady_Vyroba_Fill(dsVyroba);
                else
                    throw new Exception("ISklady2_Vyroba_Fill not implementet");



                LoadProductionRow();

                FormProductionEdit_Resize(null, null);

               
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InitProvider()
        {
            #region Sklady
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                                {
                                    providerSklady = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerSklady != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerSklady.InitProvider();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            #endregion

            #region Lokace
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerLokace == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.Lokace.ILokace2).IsAssignableFrom(t))
                                {
                                    providerLokace = (Fask.Interfaces.Ciselniky.Lokace.ILokace2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerLokace != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerLokace.InitProvider();
                 

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            //#region Logins
            //try
            //{
            //    if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
            //    {
            //        if (providerLogins == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //        {
            //            //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
            //            Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //            Type[] types = providerAssemlby.GetTypes();
            //            foreach (Type t in types)
            //            {
            //                try
            //                {
            //                    if (typeof(Fask.Interfaces.Vyroba.Login.ILogin).IsAssignableFrom(t))
            //                    {
            //                        providerLogins = (Fask.Interfaces.Vyroba.Login.ILogin)providerAssemlby.CreateInstance(t.FullName);
            //                        if (providerLogins != null)
            //                            break;
            //                    }
            //                }
            //                catch { }
            //            }
            //            //return config;
            //        }

            //        // nastaveni connection stringu
            //        //if (providerSklady != null)
            //        //    ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //        if ((providerLogins != null) && (providerLogins is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //            ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerLogins).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;


            //    }
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.Lokace");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //#endregion

            #region PRoduction Sources

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerPS == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.ProductionSources.IProductionSources).IsAssignableFrom(t))
                            {
                                providerPS = (Fask.Interfaces.Vyroba.ProductionSources.IProductionSources)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPS != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPS.InitProvider();
           
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

        }



        /// <summary>
        /// Naplneni textboxu a datetimepickeru.
        /// </summary>
        private void LoadProductionRow()
        {
            //TaD 4.9.2017
            // načtení dat
            textBoxCountEntries.Text = rowProductSources.IsCountEntriesNull() ? string.Empty : rowProductSources.CountEntries.ToString();
            textBoxSOPNUMBE.Text = rowProductSources.IsSOPNUMBENull() ? string.Empty : rowProductSources.SOPNUMBE.Trim();
            textBoxITEMNAME.Text = rowProductSources.IsITEMNAMENull() ? string.Empty : rowProductSources.ITEMNAME;
            textBoxITEMNMBR.Text = rowProductSources.IsITEMNMBRNull() ? string.Empty : rowProductSources.ITEMNMBR.Trim();
            textBoxITEMTYPE.Text = rowProductSources.IsITEMTYPENull() ? string.Empty : rowProductSources.ITEMTYPE;
            textBoxITEMCODE.Text = rowProductSources.IsITEMCODENull() ? string.Empty : rowProductSources.ITEMCODE.Trim();
            textBoxMJ.Text = string.IsNullOrEmpty(rowProductSources.MJ) ? string.Empty : rowProductSources.MJ;
            textBoxQTYSHPPD.Text = string.IsNullOrEmpty(rowProductSources.QTYSHPPD.ToString()) ? string.Empty : rowProductSources.QTYSHPPD.ToString();
            textBoxQTYSHPPDMJ.Text = string.IsNullOrEmpty(rowProductSources.QTYSHPPDMJ.ToString()) ? string.Empty : rowProductSources.QTYSHPPDMJ.ToString();
            textBoxQTYPACK.Text = rowProductSources.IsQTYPACKNull() ? string.Empty : rowProductSources.QTYPACK.ToString();
            textBoxSERLTNUM.Text = string.IsNullOrEmpty(rowProductSources.SERLTNUM.ToString()) ? string.Empty : rowProductSources.SERLTNUM.ToString();
            textBoxGUID_Production.Text = rowProductSources.IsGUID_ProductionNull() ? string.Empty : rowProductSources.GUID_Production.ToString();
            textBoxGUID.Text = rowProductSources.IsGUIDNull() ? string.Empty : rowProductSources.GUID.ToString();
            textBoxUSER_ID.Text = rowProductSources.IsUSER_IDNull() ? string.Empty : rowProductSources.USER_ID.Trim();
            textBoxTERMINAL_ID.Text = string.IsNullOrEmpty(rowProductSources.TERMINAL_ID.ToString()) ? string.Empty : rowProductSources.TERMINAL_ID.ToString();
            textBoxDEX_ROW_ID.Text = string.IsNullOrEmpty(rowProductSources.DEX_ROW_ID.ToString()) ? string.Empty : rowProductSources.DEX_ROW_ID.ToString();
            textBoxWEIGHT.Text = rowProductSources.IsWEIGHTNull() ? string.Empty : rowProductSources.WEIGHT.ToString();
            textBoxNMBRPAL.Text = rowProductSources.IsNMBRPALNull() ? string.Empty : rowProductSources.NMBRPAL.ToString();
            textBoxTYPEPAL.Text = rowProductSources.IsTYPEPALNull() ? string.Empty : rowProductSources.TYPEPAL.ToString();
            textBoxPRINTED.Text = rowProductSources.IsPRINTEDNull() ? string.Empty : rowProductSources.PRINTED.ToString();
            textBoxISOK.Text = rowProductSources.IsISOKNull() ? null : rowProductSources.ISOK.ToString(@"dd\/MM\/yyyy HH:mm");

            textBoxidVS.Text = rowProductSources.IsidVSNull() ? string.Empty : rowProductSources.idVS.Trim();
            textBoxdateedit.Text = rowProductSources.IsdateeditNull() ? string.Empty : rowProductSources.dateedit.ToString(@"dd\/MM\/yyyy HH:mm");

            textBoxpopiszakazky.Text = rowProductSources.IspopiszakazkyNull() ? string.Empty : rowProductSources.popiszakazky.Trim() ;
            textBoxnazevvyrobku.Text = rowProductSources.IsnazevvyrobkuNull() ? string.Empty : rowProductSources.nazevvyrobku.Trim(); ;
            textBoxEANvyrobku.Text = rowProductSources.IsEANvyrobkuNull() ? string.Empty : rowProductSources.EANvyrobku.Trim(); ;
            textBoxcislovyrobku.Text = rowProductSources.IscislovyrobkuNull() ? string.Empty : rowProductSources.cislovyrobku.Trim();


            //TaD 5.9.2017 nacteni Lokace a sklady comboboxu
            ComboBoxSKL_ID.Items.Clear();
            ComboBoxSKL_ID.Items.AddRange(dsVyroba.CZMST093.ToArray());
            if (!rowProductSources.IsSKL_IDNull() && !string.IsNullOrEmpty(rowProductSources.SKL_ID.Trim()))
            {
                var sklady = dsVyroba.CZMST093.Where(x => x.skl_id.Trim() == rowProductSources.SKL_ID.Trim());
                if (sklady.Count() > 0)
                    ComboBoxSKL_ID.SelectedItem = sklady.First();
            }

            ComboBoxLOCNCODE.Items.Clear();
            ComboBoxLOCNCODE.Items.AddRange(dsVyroba.CZMST094.Where(x => !rowProductSources.IsSKL_IDNull() && x.SKL_ID.Trim() == rowProductSources.SKL_ID.Trim()).ToArray());
            if (!rowProductSources.IsLOCNCODENull() && !string.IsNullOrEmpty(rowProductSources.LOCNCODE.Trim()))
            {
                var lokace = dsVyroba.CZMST094.Where(x => x.SKL_ID.Trim() == rowProductSources.SKL_ID.Trim() && x.LOCNCODE.Trim() == rowProductSources.LOCNCODE.Trim());
                if (lokace.Count() > 0)
                    ComboBoxLOCNCODE.SelectedItem = lokace.First();
            }



            ////TaD 4.9.2017
            //// vypnutí tlačítek
            //textBoxCountEntries.Enabled = !rowProduct.IsCountEntriesNull() ;
            //textBoxSOPNUMBE.Enabled = !rowProduct.IsSOPNUMBENull() ;
            //textBoxITEMNAME.Enabled = !rowProduct.IsITEMNAMENull() ;
            //textBoxITEMNMBR.Enabled = !rowProduct.IsITEMNMBRNull() ;
            //textBoxITEMTYPE.Enabled = !rowProduct.IsITEMTYPENull() ;
            //textBoxITEMCODE.Enabled = !rowProduct.IsITEMCODENull() ;
            //textBoxMJ.Enabled = !string.IsNullOrEmpty(rowProduct.MJ) ;
            //textBoxQTYSHPPD.Enabled = !string.IsNullOrEmpty(rowProduct.QTYSHPPD.ToString()) ;
            //textBoxQTYSHPPDMJ.Enabled = !string.IsNullOrEmpty(rowProduct.QTYSHPPDMJ.ToString()) ;
            //textBoxQTYPACK.Enabled = !rowProduct.IsQTYPACKNull();
            //textBoxSERLTNUM.Enabled = !string.IsNullOrEmpty(rowProduct.SERLTNUM.ToString()) ;
            //textBoxGUID_Production.Enabled = !rowProduct.IsGUID_ProductionNull() ;
            //textBoxGUID.Enabled = !rowProduct.IsGUIDNull() ;
            //textBoxUSER_ID.Enabled = !rowProduct.IsUSER_IDNull() ;
            //textBoxTERMINAL_ID.Enabled = !string.IsNullOrEmpty(rowProduct.TERMINAL_ID.ToString()) ;
            ////textBoxDEX_ROW_ID.Enabled = string.IsNullOrEmpty(rowProduct.DEX_ROW_ID.ToString()) ;
            //textBoxWEIGHT.Enabled = !rowProduct.IsWEIGHTNull() ;
            //textBoxNMBRPAL.Enabled = !rowProduct.IsNMBRPALNull();
            //textBoxTYPEPAL.Enabled = !rowProduct.IsTYPEPALNull(); 
            //textBoxPRINTED.Enabled = !rowProduct.IsPRINTEDNull() ;
            //dateTimePickerISOK.Enabled = !rowProduct.IsISOKNull() ;

            // jak toto poresit ...???
            //textBoxSkladID.Enabled = false;
            //textBoxLokaceID.Enabled = false;




        }

        private void FormProductionEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                //Production.DataServices.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter taProduction_Sources = new Production.DataServices.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();
                //taProduction_Sources.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                // warning nalezeno vice zakazek v soubehu, chcete upravit cas pouze u teto, nebo u vsech? (zobrazit warning)
                //List<Fask.Interfaces.DataSets.Vyroba.ProductionRow> resSoubeh = new List<DataServices.VyrobaDataSet.ProductionRow>();
                //Production.DataServices.VyrobaDataSet dsV = new Production.DataServices.VyrobaDataSet();
                // radky, ktere patri k sobe
                //List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow> prRows = new List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow>();

                DateTime dtNow = DateTime.Now;

                rowProductSources.idVS = FASK.Logins.Uzivatel.Instance.UserID != null ? FASK.Logins.Uzivatel.Instance.UserID : string.Empty;
                rowProductSources.dateedit = dtNow;

                rowProductSources.CountEntries = int.Parse(textBoxCountEntries.Text.Trim());
                rowProductSources.SOPNUMBE = textBoxSOPNUMBE.Text.Trim();
                rowProductSources.ITEMNAME = textBoxITEMNAME.Text.Trim();
                rowProductSources.ITEMNMBR = textBoxITEMNMBR.Text.Trim();
                rowProductSources.ITEMTYPE = textBoxITEMTYPE.Text.Trim();
                //rowProduct.SKL_ID = ComboBoxSKL_ID.Text.Trim();
                rowProductSources.ITEMCODE = textBoxITEMCODE.Text.Trim();
                //rowProduct.LOCNCODE =ComboBoxLOCNCODE.Text.Trim();
                rowProductSources.MJ = textBoxMJ.Text.Trim();
                rowProductSources.QTYPACK = decimal.Parse(textBoxQTYPACK.Text.Trim());
                rowProductSources.QTYSHPPD = decimal.Parse(textBoxQTYSHPPD.Text.Trim());
                rowProductSources.QTYSHPPDMJ = decimal.Parse(textBoxQTYSHPPDMJ.Text.Trim());
                rowProductSources.SERLTNUM = textBoxSERLTNUM.Text.Trim();
                rowProductSources.USER_ID = textBoxUSER_ID.Text.Trim();
                rowProductSources.TERMINAL_ID = int.Parse(textBoxTERMINAL_ID.Text.Trim());
                rowProductSources.cislovyrobku = textBoxcislovyrobku.Text.Trim();

                //rowProduct.idVS = textBoxidVS.Text;
                
                //if (string.IsNullOrEmpty(textBoxdateedit.Text))
                //    rowProduct.SetdateeditNull();
                //else
                //    rowProduct.dateedit = DateTime.Parse(textBoxdateedit.Text.Trim());
                    

                if (string.IsNullOrEmpty(textBoxWEIGHT.Text))
                    rowProductSources.SetWEIGHTNull();
                else
                    rowProductSources.WEIGHT = decimal.Parse(textBoxWEIGHT.Text.Trim());

                rowProductSources.NMBRPAL = textBoxNMBRPAL.Text.Trim();
                rowProductSources.TYPEPAL = textBoxTYPEPAL.Text.Trim();
                rowProductSources.PRINTED = Convert.ToByte(textBoxPRINTED.Text.Trim());


                if (string.IsNullOrEmpty(textBoxISOK.Text))
                    rowProductSources.SetISOKNull();
                else
                    rowProductSources.ISOK = DateTime.Parse(textBoxISOK.Text);


                if (ComboBoxSKL_ID.SelectedItem != null)
                {
                    var sklad = ComboBoxSKL_ID.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                    if (sklad != null)
                        rowProductSources.SKL_ID = (sklad).skl_id.Trim();
                }

                if (ComboBoxLOCNCODE.SelectedItem != null)
                {
                    var lokace = ComboBoxLOCNCODE.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;
                    if (lokace != null)
                        rowProductSources.LOCNCODE = lokace.LOCNCODE.Trim();
                }

                rowProductSources.popiszakazky =   textBoxpopiszakazky.Text.Trim() ;
                rowProductSources.nazevvyrobku = textBoxnazevvyrobku.Text.Trim(); ;
                rowProductSources.EANvyrobku = textBoxEANvyrobku.Text.Trim(); ;



                //taProduction_Sources.Update(rowProduct);

                if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update_Row)
                    ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update_Row)providerPS).Update_Row(rowProductSources);
                else
                    throw new Exception("IProductionSources_Update_Row not implementet");



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

                Fask.Interfaces.DataSets.Vyroba dsV = new Fask.Interfaces.DataSets.Vyroba();

                bool status;
                int _countEntries = 0;
                // validace COUNTENTRIES, enabled se nemusi zatim kontrolovat
                if (!rowProductSources.IsCountEntriesNull())
                {
                    if (string.IsNullOrEmpty(textBoxCountEntries.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxCountEntries, "Číslo dávky musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        status = Int32.TryParse(textBoxCountEntries.Text, out _countEntries);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxCountEntries, "Číslo dávky musí být číslo");
                        }
                    }
                }

                //validace MJ
                if (!string.IsNullOrEmpty(rowProductSources.MJ))
                {
                    if (string.IsNullOrEmpty(textBoxMJ.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxMJ, "Měrná jednotka zakázky musí být vyplněna");
                    }
                }


                // validace SOPNUMBE
                if (!rowProductSources.IsSOPNUMBENull())
                {
                    if (string.IsNullOrEmpty(textBoxSOPNUMBE.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxSOPNUMBE, "Číslo výrobní zakázky musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        decimal _qty;
                        status = Decimal.TryParse(textBoxSOPNUMBE.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxSOPNUMBE, "Číslo výrobní zakázky musí být číslo");
                        }
                        else
                        {
                            if (_qty < 0)
                            {
                                errorProvider1.SetError(textBoxSOPNUMBE, "Číslo výrobní zakázky musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola QTYSHPPD
                if (!string.IsNullOrEmpty(rowProductSources.QTYSHPPD.ToString()))
                {
                    if (string.IsNullOrEmpty(textBoxQTYSHPPD.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxQTYSHPPD, "Požadované množství v balení musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        decimal _qtyp;
                        status = Decimal.TryParse(textBoxQTYSHPPD.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qtyp);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxQTYSHPPD, "Požadované množství v balení musí být číslo");
                        }
                        else
                        {
                            if (_qtyp < 0)
                            {
                                errorProvider1.SetError(textBoxQTYSHPPD, "Požadované množství v balení musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola QTYSHPPDMJ
                if (!string.IsNullOrEmpty(rowProductSources.QTYSHPPDMJ.ToString()))
                {
                    if (string.IsNullOrEmpty(textBoxQTYSHPPDMJ.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxQTYSHPPDMJ, "Požadované množství mj v balení musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        decimal _qtymj;
                        status = Decimal.TryParse(textBoxQTYSHPPDMJ.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qtymj);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxQTYSHPPDMJ, "Požadované množství mj v balení musí být číslo");
                        }
                        else
                        {
                            if (_qtymj < 0)
                            {
                                errorProvider1.SetError(textBoxQTYSHPPDMJ, "Požadované množství mj v balení musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }



                // kontrola UserID
                if (string.IsNullOrEmpty(textBoxUSER_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxUSER_ID, "Id uživatele musí být vyplněno");
                }
                else
                {
                    //var _validUserid = taLogins.GetDataByID(textBoxUSER_ID.Text.Trim());

                    //Fask.Interfaces.DataSets.Vyroba.LoginsDataTable _validUserid;

                    //if (providerLogins is Fask.Interfaces.Vyroba.Login.ILogin_GetDataByID)
                    //    _validUserid = ((Fask.Interfaces.Vyroba.Login.ILogin_GetDataByID)providerLogins).GetDataByID(textBoxUSER_ID.Text.Trim());
                    //else
                    //    throw new Exception("ILogin_GetDataByID not implementet");

                    var _validUserid = FASK.Logins.Uzivatel.Instance.Komunikace.GetLoginsByID(textBoxUSER_ID.Text.Trim());


                    //var _validLoginid = ds.Logins.Where(x => x.id == textBoxLoginid.Text.Trim());
                    // nenalezen
                    if (_validUserid.Count() == 0)
                    {
                        errorProvider1.SetError(textBoxUSER_ID, "Id uživatele nebylo nalezeno");
                    }
                }

                // kontrola TermID
                if (string.IsNullOrEmpty(textBoxTERMINAL_ID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxTERMINAL_ID, "Id terminálu musí být vyplněno");
                }
                else
                {
                    byte _termid;
                    status = Byte.TryParse(textBoxTERMINAL_ID.Text, out _termid);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxTERMINAL_ID, "Id terminálu musí být číslo");
                    }
                }

                //sklad
                var sklad = ComboBoxSKL_ID.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                if (!rowProductSources.IsSKL_IDNull())
                { // je zadan sklad, tak se bude menit ...
                    if (sklad == null)
                    {
                        errorProvider1.SetError(ComboBoxSKL_ID, "Sklad není nastaven");
                    }
                }
                //lokace
                if (!rowProductSources.IsLOCNCODENull() && !string.IsNullOrEmpty(rowProductSources.LOCNCODE))
                {
                    var lokace = ComboBoxLOCNCODE.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;
                    if (lokace == null)
                    {
                        errorProvider1.SetError(ComboBoxLOCNCODE, "Lokace není nastavena");
                    }
                    if (sklad == null)
                    {
                        errorProvider1.SetError(ComboBoxSKL_ID, "Sklad není nastaven");
                    }

                    if (lokace != null && sklad != null)
                    {
                        if (sklad.skl_id.Trim() != lokace.SKL_ID.Trim())
                        {
                            errorProvider1.SetError(ComboBoxLOCNCODE, "Sklad vybrané lokace neodpovídá vybranému skladu");
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return IsAllValid();
        }

        private void FormProductionEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //PerformOK();
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

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }


        //private void textBoxSklad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // aktualizovat polozky lokaci a pokusit se nechat vybrat lokaci se stejnym kodem ... na nove ...
        //        var lokaceAkt = ComboBoxLOCNCODE.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;

        //        var sklad = ComboBoxSKL_ID.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
        //        if (sklad == null)
        //            return;

        //        ComboBoxLOCNCODE.Items.Clear();
        //        ComboBoxLOCNCODE.Items.AddRange(dsVyroba.CZMST094.Where(x => x.SKL_ID == sklad.skl_id).ToArray());




        //        var lokaceNove = dsVyroba.CZMST094.Where(x => x.SKL_ID == sklad.skl_id && x.Barcode == lokaceAkt.Barcode);
        //        if (lokaceNove.Count() > 0)
        //            ComboBoxLOCNCODE.SelectedItem = lokaceNove.First();

        //    }
        //    catch
        //    {
        //    }
        //}

        private void textBoxSklad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 1) Zapamatovat si původně zvolenou lokaci (barcode), pokud tam nějaká byla
                string puvodniBarcode = null;
                if (ComboBoxLOCNCODE.SelectedItem is Fask.Interfaces.DataSets.Vyroba.CZMST094Row lokaceAkt)
                {
                    puvodniBarcode = lokaceAkt.Barcode;
                }

                // 2) Získat vybraný sklad
                var sklad = ComboBoxSKL_ID.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                if (sklad == null || dsVyroba == null || dsVyroba.CZMST094 == null)
                    return;

                // 3) Vyfiltrovat lokace pro daný sklad
                var lokaceProSklad = dsVyroba.CZMST094
                    .Where(x => x.SKL_ID == sklad.skl_id)
                    .ToArray();

                ComboBoxLOCNCODE.BeginUpdate();
                ComboBoxLOCNCODE.Items.Clear();
                ComboBoxLOCNCODE.Items.AddRange(lokaceProSklad);

                // 4) Pokusit se znovu vybrat lokaci se stejným barcode, pokud existuje
                if (!string.IsNullOrEmpty(puvodniBarcode))
                {
                    var lokaceNove = lokaceProSklad.FirstOrDefault(x => x.Barcode == puvodniBarcode);
                    if (lokaceNove != null)
                    {
                        ComboBoxLOCNCODE.SelectedItem = lokaceNove;
                    }
                    else if (lokaceProSklad.Length > 0)
                    {
                        // pokud se stejný barcode nenašel, vyber první lokaci
                        ComboBoxLOCNCODE.SelectedIndex = 0;
                    }
                }
                else if (lokaceProSklad.Length > 0)
                {
                    // pokud nebyla žádná původní lokace, vyber první
                    ComboBoxLOCNCODE.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // Tady si případně zaloguj chybu, ale neschovávej ji potichu
                // např.: LogError(ex);
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                ComboBoxLOCNCODE.EndUpdate();
            }
        }


        private void carovyKodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HledatCarovyKod();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HledatCarovyKod();
        }

        private void HledatCarovyKod()
        {
            using (FormProductionEdit2_CarKod fpeck = new FormProductionEdit2_CarKod())
            {
                fpeck.Text = "Změna matriálu";
                if (fpeck.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                RowCKod_ZASOBY = fpeck.Row_ZASOBY;

            }

            textBoxITEMNMBR.Text = RowCKod_ZASOBY.ITEMNMBR.Trim();
            textBoxITEMNAME.Text = RowCKod_ZASOBY.ITEMDESC.Trim();
        }

    }
}
