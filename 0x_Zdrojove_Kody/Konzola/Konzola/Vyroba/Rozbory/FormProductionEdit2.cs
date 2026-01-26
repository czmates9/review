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
    [Obsolete("Stara funkcionalita, nove se pouziva 'FormProductionEdit3', kde je mozno editovat vice dat")]
    public partial class FormProductionEdit2 : Form
    {
        //private Fask.Interfaces.IVyrobaKonzola providerLogins = null;
        private Fask.Interfaces.IMES providerSklad = null;
        private Fask.Interfaces.IMES providerLokace = null;

        private Fask.Interfaces.IMES providerVPH = null;
        private Fask.Interfaces.IMES providerVPP = null;
        private Fask.Interfaces.IMES providerMachines = null;

        private Fask.Interfaces.IMES providerOperations = null;
        private Fask.Interfaces.IMES providerVMachinesOperations = null;
        private Fask.Interfaces.IMES providerCorrects = null;

        private Fask.Interfaces.IMES providerP = null;

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
        //    // naplnění daty pro kontrolu
        //    //taLogins = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
        //    //taLogins.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

        //    //taLogins.Fill(ds.Logins);

        //    //taVPH = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
        //    //taVPH.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //    //taVPH.Fill(ds.CZPRO_VPH);

        //    //taVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
        //    //taVPP.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //    //taVPP.Fill(ds.CZPRO_VPP);                

        //    //taMachines = new Production.DataServices.VyrobaDataSetTableAdapters.MachinesTableAdapter();
        //    //taMachines.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //    //taMachines.Fill(ds.Machines);

        //    //taOperations = new Production.DataServices.VyrobaDataSetTableAdapters.OperationsTableAdapter();
        //    //taOperations.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //    //taOperations.Fill(ds.Operations);

        //    //tavMachinesOperations = new Production.DataServices.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter();
        //    //tavMachinesOperations.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

        //    //taCorrects = new Production.DataServices.VyrobaDataSetTableAdapters.CorrectsTableAdapter();
        //    //taCorrects.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

        //    //Production.DataServices.VyrobaDataSetTableAdapters.CZMST093TableAdapter taSklady = new Production.DataServices.VyrobaDataSetTableAdapters.CZMST093TableAdapter();
        //    //taSklady.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //    //taSklady.Fill(dsVyroba.CZMST093);

        //    //Production.DataServices.VyrobaDataSetTableAdapters.CZMST094TableAdapter taLokace = new Production.DataServices.VyrobaDataSetTableAdapters.CZMST094TableAdapter();
        //    //taLokace.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //    //taLokace.Fill(dsVyroba.CZMST094);
        //}
        #endregion

        private PRODUCTIONTYPE _producttype;
        public enum PRODUCTIONTYPE
        {
            ODVOD_START,
            ODVOD_STOP,
            KOREKCE_NEVAZANA_START,
            KOREKCE_NEVAZANA_STOP,
            KOREKCE_VAZANA_START,
            KOREKCE_VAZANA_STOP
        }

        /// <summary>
        /// záznam, který se bude upravovat
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow rowProduct { get; set; }

        /// <summary>
        /// dotazeny Radek z ZASOBY
        /// </summary>
        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow RowCKod_ZASOBY { get; set; }

        /// <summary>
        /// Datum bylo upraveno podle najiteho paroveho zaznamu
        /// </summary>
        private bool dateedit;


        public FormProductionEdit2()
        {
            InitializeComponent();

            //InitializeTableAdapters();

        }


        private void FormProductionEdit_Load(object sender, EventArgs e)
        {
            try 
	        {

                #region Inicializace Provider
                InitProvider();

                //if (providerLogins == null)
                //    throw new Exception("Provider 'Logins' není inicializován");

                if (providerSklad == null)
                    throw new Exception("Provider 'Sklad' není inicializován");

                if (providerLokace == null)
                    throw new Exception("Provider 'Lokace' není inicializován");

                if (providerVPH == null)
                    throw new Exception("Provider 'VPH' není inicializován");

                if (providerVPP == null)
                    throw new Exception("Provider 'VPP' není inicializován");

                if (providerMachines == null)
                    throw new Exception("Provider 'Machines' není inicializován");

                if (providerOperations == null)
                    throw new Exception("Provider 'Operations' není inicializován");

                if (providerVMachinesOperations == null)
                    throw new Exception("Provider 'VMachinesOperations' není inicializován");

                if (providerCorrects == null)
                    throw new Exception("Provider 'Corrects' není inicializován");

                if (providerP == null)
                    throw new Exception("Provider 'Production' není inicializován"); 


                
                #endregion

                if ((providerLokace != null) && providerLokace is Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)
                    ((Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)providerLokace).Fill(dsVyroba);
                else
                    throw new Exception("ILokace2_Fill not implementet");

                if ((providerSklad != null) &&providerSklad is Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)
                    ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)providerSklad).Sklady_Vyroba_Fill(dsVyroba);
                else
                    throw new Exception("ISklady2_Vyroba_Fill not implementet");


                // zjištění o jaký typ záznamu se jedná
                // jedná se o odvod
                if (!rowProduct.IsTIMESTARTNull())
                {
                    _producttype = PRODUCTIONTYPE.ODVOD_START;
                    if (!rowProduct.IsTIMESTOPNull())
                    {
                        _producttype = PRODUCTIONTYPE.ODVOD_STOP;
                    }
                }
                else // jedná se o korekci
                {   
                    // start korekce vazane
                    if (!rowProduct.IsTIMECORSTARTNull() && !rowProduct.IsCountEntriesNull())
                    {
                        _producttype = PRODUCTIONTYPE.KOREKCE_VAZANA_START;
                        // stop korekce vazane
                        if (!rowProduct.IsTIMECORSTOPNull())
                        {
                            _producttype = PRODUCTIONTYPE.KOREKCE_VAZANA_STOP;
                        }
                    }
                    else
                    {
                        // start nevazane korekce
                        if (!rowProduct.IsTIMECORSTARTNull())
                        {
                            _producttype = PRODUCTIONTYPE.KOREKCE_NEVAZANA_START;
                            if (!rowProduct.IsTIMECORSTOPNull())
                            {
                                _producttype = PRODUCTIONTYPE.KOREKCE_NEVAZANA_STOP;
                            }
                        }
                    }

                }

                LoadProductionRow();
        
                //textBoxId.Text = Globals.Pracovnik.id.Trim();
                //textBoxJmeno.Text = Globals.Pracovnik.firstname + " " + Globals.Pracovnik.surname;
                //textBoxMnozstviStare.Text = rowProduct.qty.ToString();
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
                    if (providerSklad == null) //inicializace se provede pouze pokud nebyla provedena ... 
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
                                    providerSklad = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerSklad != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerSklad.InitProvider();
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
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.ILogin");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //#endregion

            #region providerVPH

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPH == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPH.IVPH).IsAssignableFrom(t))
                            {
                                providerVPH = (Fask.Interfaces.Vyroba.VPH.IVPH)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPH != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPH.InitProvider();

            }

            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerVPP

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPP.IVPP).IsAssignableFrom(t))
                            {
                                providerVPP = (Fask.Interfaces.Vyroba.VPP.IVPP)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPP.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerMachines

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerMachines == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Machines.IMachines).IsAssignableFrom(t))
                            {
                                providerMachines = (Fask.Interfaces.Vyroba.Machines.IMachines)providerAssemlby.CreateInstance(t.FullName);
                                if (providerMachines != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerMachines.InitProvider();
            
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerOperations

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerOperations == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Operations.IOperations).IsAssignableFrom(t))
                            {
                                providerOperations = (Fask.Interfaces.Vyroba.Operations.IOperations)providerAssemlby.CreateInstance(t.FullName);
                                if (providerOperations != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }
           
                providerOperations.InitProvider();
           
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerVMachinesOperations

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVMachinesOperations == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations).IsAssignableFrom(t))
                            {
                                providerVMachinesOperations = (Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVMachinesOperations != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVMachinesOperations.InitProvider();
               
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerCorrects

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerCorrects == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Corrects.ICorrects).IsAssignableFrom(t))
                            {
                                providerCorrects = (Fask.Interfaces.Vyroba.Corrects.ICorrects)providerAssemlby.CreateInstance(t.FullName);
                                if (providerCorrects != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerCorrects.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerCorrects

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Production.IProduction).IsAssignableFrom(t))
                            {
                                providerP = (Fask.Interfaces.Vyroba.Production.IProduction)providerAssemlby.CreateInstance(t.FullName);
                                if (providerP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerP.InitProvider();
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
            // načtení dat
            textBoxCountEntries.Text = rowProduct.IsCountEntriesNull() ? string.Empty : rowProduct.CountEntries.ToString();
            textBoxSOPNUMBE.Text = rowProduct.IsSOPNUMBENull() ? string.Empty : rowProduct.SOPNUMBE.Trim();
            textBoxITEMNMBR.Text = rowProduct.IsITEMNMBRNull() ? string.Empty : rowProduct.ITEMNMBR.Trim();
            textBoxITEMNMBR.Enabled = false;
            textBoxITEMDESC.Text = rowProduct.IsITEMDESCNull() ? string.Empty : rowProduct.ITEMDESC.Trim();
            textBoxITEMDESC.Enabled = false;
            textBoxITEMTYPE.Text = rowProduct.IsITEMTYPENull() ? string.Empty : rowProduct.ITEMTYPE;
            textBoxITEMMJ.Text = rowProduct.IsITEMMJNull() ? string.Empty : rowProduct.ITEMMJ.Trim();
            textBoxORD.Text = rowProduct.IsORDNull() ? string.Empty : rowProduct.ORD.ToString();
            textBoxTIMEMODE.Text = rowProduct.IsTIMEMODENull() ? string.Empty : rowProduct.TIMEMODE.ToString();
            dateTimePickerTIMEPREPSTART.Value = rowProduct.IsTIMEPREPSTARTNull() ? DateTime.Now : rowProduct.TIMEPREPSTART;
            dateTimePickerTIMEPREPSTOP.Value = rowProduct.IsTIMEPREPSTOPNull() ? DateTime.Now : rowProduct.TIMEPREPSTOP;
            textBoxTIMEPREP.Text = rowProduct.IsTIMEPREPNull() ? string.Empty : rowProduct.TIMEPREP.ToString();
            textBoxTIMEUNIT.Text = rowProduct.IsTIMEUNITNull() ? string.Empty : rowProduct.TIMEUNIT.ToString();
            dateTimePickerTIMESTART.Value = rowProduct.IsTIMESTARTNull() ? DateTime.Now : rowProduct.TIMESTART;
            dateTimePickerTIMESTOP.Value = rowProduct.IsTIMESTOPNull() ? DateTime.Now : rowProduct.TIMESTOP;
            dateTimePickerTIMECORSTART.Value = rowProduct.IsTIMECORSTARTNull() ? DateTime.Now : rowProduct.TIMECORSTART;
            dateTimePickerTIMECORSTOP.Value = rowProduct.IsTIMECORSTOPNull() ? DateTime.Now : rowProduct.TIMECORSTOP;
            textBoxTIMECOR.Text = rowProduct.IsTIMECORSTOPNull() ? string.Empty : rowProduct.TIMECOR.ToString();
            textBoxTIMECRID.Text = rowProduct.IsTIMECRIDNull() ? string.Empty : rowProduct.TIMECRID.ToString();
            textBoxId.Text = rowProduct.id.ToString();
            textBoxId.Enabled = false;
            textBoxLoginid.Text = rowProduct.loginid.Trim();
            textBoxMachineid.Text = rowProduct.IsmachineidNull() ? string.Empty : rowProduct.machineid.Trim();
            textBoxOperationid.Text = rowProduct.IsoperationidNull() ? string.Empty : rowProduct.operationid.Trim();
            dateTimePickerDateeve.Value = rowProduct.dateeve;
            dateTimePickerDateeve.Enabled = false;
            textBoxQty.Text = rowProduct.qty.ToString();
            textBoxQtyReal.Text = rowProduct.qtyReal.ToString();
            textBoxQTYPACK.Text = rowProduct.IsQTYPACKNull() ? string.Empty : rowProduct.QTYPACK.ToString();
            textBoxQTYPACKMJ.Text = rowProduct.IsQTYPACKMJNull() ? string.Empty : rowProduct.QTYPACKMJ.Trim();
            textBoxDescription.Text = rowProduct.IsdescriptionNull() ? string.Empty : rowProduct.description.Trim();
            textBoxBarcodeP.Text = rowProduct.IsBarcodePNull() ? string.Empty : rowProduct.BarcodeP.Trim();
            textBoxUserID.Text = rowProduct.UserID.Trim();
            textBoxTermID.Text = rowProduct.TermID.ToString();
            dateTimePickerISOK.Value = rowProduct.IsISOKNull() ? DateTime.Now : rowProduct.ISOK;
            textBoxGUID.Text = rowProduct.GUID.ToString();
            textBoxGUID.Enabled = false;
            textBoxSOUBEHGUID.Text = rowProduct.IsSOUBEHGUIDNull() ? string.Empty : rowProduct.SOUBEHGUID.ToString();
            textBoxSOUBEHGUID.Enabled = false;
            textBoxCORRGUID.Text = rowProduct.IsCORRGUIDNull() ? string.Empty : rowProduct.CORRGUID.ToString();
            textBoxCORRGUID.Enabled = false;
            textBoxQtyOld.Text = rowProduct.IsqtyOldNull() ? string.Empty : rowProduct.qtyOld.ToString();
            textBoxidVS.Text = rowProduct.IsidVSNull() ? string.Empty : rowProduct.idVS.Trim();
            textBoxidVS.Enabled = false;
            dateTimePickerDateedit.Value = rowProduct.IsdateeditNull() ? DateTime.Now : rowProduct.dateedit;

            textBoxSklad.Items.Clear();
            textBoxSklad.Items.AddRange(dsVyroba.CZMST093.ToArray());
            if (!rowProduct.IsSKL_IDNull() && !string.IsNullOrEmpty(rowProduct.SKL_ID.Trim()))
            {
                var sklady = dsVyroba.CZMST093.Where( x => x.skl_id.Trim() == rowProduct.SKL_ID.Trim());
                if (sklady.Count() > 0)
                    textBoxSklad.SelectedItem = sklady.First();
            }

            textBoxLokace.Items.Clear();
            textBoxLokace.Items.AddRange(dsVyroba.CZMST094.Where(x => !rowProduct.IsSKL_IDNull() && x.SKL_ID.Trim() == rowProduct.SKL_ID.Trim()).ToArray());
            if (!rowProduct.IsLOCNCODENull() && !string.IsNullOrEmpty(rowProduct.LOCNCODE.Trim()))
            {
                var lokace = dsVyroba.CZMST094.Where(x => x.SKL_ID.Trim() == rowProduct.SKL_ID.Trim() && x.LOCNCODE.Trim() == rowProduct.LOCNCODE.Trim());
                if (lokace.Count() > 0)
                    textBoxLokace.SelectedItem = lokace.First();
            }

            // vypnutí tlačítek
            textBoxCountEntries.Enabled = !rowProduct.IsCountEntriesNull();
            textBoxSOPNUMBE.Enabled = !rowProduct.IsSOPNUMBENull();
            textBoxITEMNMBR.Enabled = !rowProduct.IsITEMNMBRNull();
            textBoxITEMDESC.Enabled = !rowProduct.IsITEMDESCNull();
            textBoxITEMTYPE.Enabled = !rowProduct.IsITEMTYPENull();
            textBoxITEMMJ.Enabled = !rowProduct.IsITEMMJNull();
            textBoxORD.Enabled = !rowProduct.IsORDNull();
            textBoxTIMEMODE.Enabled = !rowProduct.IsTIMEMODENull();
            dateTimePickerTIMEPREPSTART.Enabled = !rowProduct.IsTIMEPREPSTARTNull();
            dateTimePickerTIMEPREPSTOP.Enabled = !rowProduct.IsTIMEPREPSTOPNull();
            textBoxTIMEPREP.Enabled = !rowProduct.IsTIMEPREPNull();
            textBoxTIMEUNIT.Enabled = !rowProduct.IsTIMEUNITNull();
            dateTimePickerTIMESTART.Enabled = !rowProduct.IsTIMESTARTNull();
            dateTimePickerTIMESTOP.Enabled = !rowProduct.IsTIMESTOPNull();
            dateTimePickerTIMECORSTART.Enabled = !rowProduct.IsTIMECORSTARTNull();
            dateTimePickerTIMECORSTOP.Enabled = !rowProduct.IsTIMECORSTOPNull();
            // nastaveno vždy na false, při potvrzení se dopočítá
            textBoxTIMECOR.Enabled = false;
            //textBoxTIMECOR.Enabled = !rowProduct.IsTIMECORSTOPNull();
            textBoxTIMECRID.Enabled = !rowProduct.IsTIMECRIDNull();
            //textBoxId.Text = rowProduct.id.ToString();
            //textBoxId.Enabled = false;
            //textBoxLoginid.Text = rowProduct.loginid.Trim();
            textBoxMachineid.Enabled = !rowProduct.IsmachineidNull();
            textBoxOperationid.Enabled = !rowProduct.IsoperationidNull();
            //dateTimePickerDateeve.Value = rowProduct.dateeve;
            //textBoxQty.Text = rowProduct.qty.ToString();
            //textBoxQtyReal.Text = rowProduct.qtyReal.ToString();
            textBoxQTYPACK.Enabled = !rowProduct.IsQTYPACKNull();
            textBoxQTYPACKMJ.Enabled = !rowProduct.IsQTYPACKMJNull();
            textBoxDescription.Enabled = !rowProduct.IsdescriptionNull();
            textBoxBarcodeP.Enabled = !rowProduct.IsBarcodePNull();
            //textBoxUserID.Text = rowProduct.UserID.Trim();
            //textBoxTermID.Text = rowProduct.TermID.ToString();
            dateTimePickerISOK.Enabled = !rowProduct.IsISOKNull();
            //textBoxGUID.Text = rowProduct.GUID.ToString();
            //textBoxGUID.Enabled = false;
            //textBoxSOUBEHGUID.Text = rowProduct.IsSOUBEHGUIDNull() ? string.Empty : rowProduct.SOUBEHGUID.ToString();
            //textBoxSOUBEHGUID.Enabled = false;
            //textBoxCORRGUID.Text = rowProduct.IsCORRGUIDNull() ? string.Empty : rowProduct.CORRGUID.ToString();
            //textBoxCORRGUID.Enabled = false;
            textBoxQtyOld.Enabled = false;
            //textBoxidVS.Enabled = rowProduct.IsidVSNull() ? string.Empty : rowProduct.idVS.Trim();
            //textBoxidVS.Enabled = false;
            dateTimePickerDateedit.Enabled = false;

            // jak toto poresit ...???
            //textBoxSkladID.Enabled = false;
            //textBoxLokaceID.Enabled = false;

            // najití datumu STOP
            FindTimeStop();
            //// odvadeni
            //if ((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
            //{

            //}

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


                //var taProduction = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                //taProduction.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                // warning nalezeno vice zakazek v soubehu, chcete upravit cas pouze u teto, nebo u vsech? (zobrazit warning)
                //List<Fask.Interfaces.DataSets.Vyroba.ProductionRow> resSoubeh = new List<DataServices.VyrobaDataSet.ProductionRow>();
                Fask.Interfaces.DataSets.Vyroba dsV = new Fask.Interfaces.DataSets.Vyroba();
                // radky, ktere patri k sobe
                List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> prRows = new List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();
                // zakazka v soubehu
                if (!rowProduct.IsSOUBEHGUIDNull())
                {
                    //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID));
                    //taProduction.FillBySOUBEHGUID(dsV.Production, rowProduct.SOUBEHGUID);

                    if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID))
                        ((Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID)providerP).Production_FillBySOUBEHGUID(dsV, rowProduct.SOUBEHGUID);
                    else
                        throw new Exception("IProduction_FillBySOUBEHGUID not implementet");
                }
                else
                {
                    // korekce
                    if (!rowProduct.IsCORRGUIDNull())
                    {
                        //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.CORRGUID));
                        //taProduction.FillByCORRGUID(dsV.Production, rowProduct.CORRGUID);

                        if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID))
                            ((Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID)providerP).Production_FillByCORRGUID(dsV, rowProduct.CORRGUID);
                        else
                            throw new Exception("IProduction_FillByCORRGUID not implementet");

                        prRows.AddRange(dsV.Production_Konzola);
                    }
                    else
                    {
                        // samotny radek (nemelo by sem dojit)
                        //resSoubeh.Add(rowProduct);
                        dsV.Production_Konzola.ImportRow(rowProduct);
                        prRows.Add(rowProduct);
                    }
                }


                bool upravitCasUJedneZakazky = false;
                // kontrola TIMESTART nebo TIMESTOP, jestli byl zadan a zmenen
                if((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
                {
                    // vice zakazek
                    if ((!rowProduct.IsTIMESTOPNull() && (rowProduct.TIMESTOP != dateTimePickerTIMESTOP.Value)) ||
                        (!rowProduct.IsTIMESTARTNull() && (rowProduct.TIMESTART != dateTimePickerTIMESTART.Value)))
                    {
                        var zakazky = dsV.Production_Konzola.Where(x => !x.IsTIMESTOPNull());
                        if (zakazky.Count() > 1)
                        {
                            DialogResult dr = MessageBox.Show("Nalezeno více zakázek v souběhu, chcete upravit čas pouze u této zakázky?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                upravitCasUJedneZakazky = true;
                            }
                            else if (dr == DialogResult.No)
                            {
                                upravitCasUJedneZakazky = false;
                            }
                            else return;
                        }
                    }

                    //// kontrola TIMESTOP, jestli byl zadan a zmenen
                    //if (!rowProduct.IsTIMESTOPNull() && (rowProduct.TIMESTOP != dateTimePickerTIMESTOP.Value))
                    //{                        
                    //    // nalezeno vice zakazek 
                    //    //if(dsV.Production.Count > 2)
                    //    //{
                    //    //    if (DialogResult.Yes == MessageBox.Show("Nalezeno více zakázek v souběhu, chcete upravit čas pouze u této zakázky?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    //    //    {                                
                    //    //        upravitCasUJedneZakazky = true;
                    //    //    }
                    //    //    else 
                    //    //        upravitCasUJedneZakazky = false;
                    //    //}
                    //}
                    //else
                    //{
                    //    // kontrola, zdali byl zmenen timestart
                    //    if (!rowProduct.IsTIMESTARTNull() && (rowProduct.TIMESTART != dateTimePickerTIMESTART.Value))
                    //    {
                    //        //if (dsV.Production.Count > 2)
                    //        //{
                    //        //    if (DialogResult.Yes == MessageBox.Show("Nalezeno více zakázek v souběhu, chcete upravit čas pouze u této zakázky?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    //        //    {
                    //        //        upravitCasUJedneZakazky = true;
                    //        //    }
                    //        //    else 
                    //        //        upravitCasUJedneZakazky = false;
                    //        //}
                    //    }
                    //}
                }

                
                //prRows.Add(rowProduct);
                //prRows.Contains(rowProduct);
                if ((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
                {
                    // nadop
                    if (rowProduct.IsITEMNMBRNull())
                    {
                        prRows.AddRange(dsV.Production_Konzola.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE)).ToList());
                    }
                    else  // zbytek
                    {
                        prRows.AddRange(dsV.Production_Konzola.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE) && (x.ITEMNMBR == rowProduct.ITEMNMBR)).ToList());
                    }
                    //dsV.Production.Where(
                }

                // datum editace
                DateTime dtNow = DateTime.Now;
                foreach (var item in dsV.Production_Konzola)
                {
                    item.idVS = FASK.Logins.Uzivatel.Instance.UserID != null ? FASK.Logins.Uzivatel.Instance.UserID : string.Empty;
                    item.dateedit = dtNow;
                    item.UserID = textBoxUserID.Text.Trim();
                    item.TermID = Convert.ToByte(textBoxTermID.Text);
                    item.loginid = textBoxLoginid.Text.Trim();
                    if (!item.IsmachineidNull() && textBoxMachineid.Enabled)
                        item.machineid = textBoxMachineid.Text.Trim();
                    if (!item.IsoperationidNull() && textBoxOperationid.Enabled)
                        item.operationid = textBoxOperationid.Text.Trim();

                    if (!item.IsTIMESTOPNull())
                    {
                        if (textBoxSklad.SelectedItem != null)
                        {
                            var sklad = textBoxSklad.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                            if (sklad != null)
                                item.SKL_ID = (sklad).skl_id.Trim();
                        }

                        if (textBoxLokace.SelectedItem != null)
                        {
                            var lokace = textBoxLokace.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;
                            if (lokace != null)
                                item.LOCNCODE = lokace.LOCNCODE.Trim();
                        }

                    }

                    // uprava aktualniho zaznamu
                    //if (rowProduct.id == item.id)
                    // uprava souvisejicich zaznamu (napr. soucasne korekce start/stop, pripadne zakazka start/stop, pripadne nalezena zakazka v soubehu a jeji ekvivalent)
                    if(prRows.Contains(item))
                    {
                        // porovnani, jestli dany sloupec je null a soucasne zapla jeho komponenta (textbox a pod.)
                        // kvuli parovym udajum, aby se napr. pri editaci zaznamu pouze s TIMESTOP needitovat cas u zaznamu s TIMESTART
                        if (!item.IsCountEntriesNull() && textBoxCountEntries.Enabled)
                            item.CountEntries = Convert.ToInt32(textBoxCountEntries.Text);
                        if (!item.IsSOPNUMBENull() && textBoxSOPNUMBE.Enabled)
                            item.SOPNUMBE = textBoxSOPNUMBE.Text.Trim();
                        if (!item.IsITEMNMBRNull() && textBoxITEMNMBR.Enabled)
                            item.ITEMNMBR = textBoxITEMNMBR.Text.Trim();
                        if (!item.IsITEMDESCNull() && textBoxITEMDESC.Enabled)
                            item.ITEMDESC = textBoxITEMDESC.Text.Trim();
                        if (!item.IsITEMTYPENull() && textBoxITEMTYPE.Enabled)
                            item.ITEMTYPE = textBoxITEMTYPE.Text.Trim();
                        if (!item.IsITEMMJNull() && textBoxITEMMJ.Enabled)
                            item.ITEMMJ = textBoxITEMMJ.Text.Trim();
                        if (!item.IsORDNull() && textBoxORD.Enabled)
                            item.ORD = Convert.ToInt32(textBoxORD.Text);
                        if (!item.IsTIMEMODENull() && textBoxTIMEMODE.Enabled)
                            item.TIMEMODE = Convert.ToInt32(textBoxTIMEMODE.Text);
                        if (!item.IsTIMEPREPSTARTNull() && dateTimePickerTIMEPREPSTART.Enabled)
                            item.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                        if (!item.IsTIMEPREPSTOPNull() && dateTimePickerTIMEPREPSTOP.Enabled)
                            item.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                        if (!item.IsTIMEPREPNull() && textBoxTIMEPREP.Enabled)
                            item.TIMEPREP = float.Parse(textBoxTIMEPREP.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                        if (!item.IsTIMEUNITNull() && textBoxTIMEUNIT.Enabled)
                            item.TIMEUNIT = float.Parse(textBoxTIMEUNIT.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                        if (!item.IsTIMESTARTNull() && dateTimePickerTIMESTART.Enabled)
                            item.TIMESTART = dateTimePickerTIMESTART.Value;
                        if (!item.IsTIMESTOPNull() && dateTimePickerTIMESTOP.Enabled)
                            item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        if (!item.IsTIMECORSTARTNull() && dateTimePickerTIMECORSTART.Enabled)
                            item.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                        if (!item.IsTIMECORSTOPNull() && dateTimePickerTIMECORSTOP.Enabled)
                            item.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;
                        // korekce ukoncena
                        if (!item.IsTIMECORNull()) // && textBoxTIMECOR.Enabled)
                        {
                            // bylo mozne zmenit upravit casy
                            if (rowProduct.id == item.id)
                            {
                                item.TIMECOR = (float)(dateTimePickerTIMECORSTOP.Value - dateTimePickerTIMECORSTART.Value).TotalMinutes;
                            }
                            else
                            {
                                // nebylo mozne upravit stop cas
                                item.TIMECOR = (float)(item.TIMECORSTOP - dateTimePickerTIMECORSTART.Value).TotalMinutes;
                            }
                        }

                        if (!item.IsTIMECRIDNull() && textBoxTIMECRID.Enabled)
                            item.TIMECRID = Convert.ToInt32(textBoxTIMECRID.Text);
                        // id neupravovat                                                
                        // dateeve neupravovat
                        // pokud vybrany zaznam je ukonceni odvodu, nastavi se zvolene mnozstvi, jinak 0
                        item.qty = ((item.id == rowProduct.id) && (_producttype == PRODUCTIONTYPE.ODVOD_STOP)) ? Convert.ToDecimal(textBoxQty.Text) : 0;
                        item.qtyReal = ((item.id == rowProduct.id) && (_producttype == PRODUCTIONTYPE.ODVOD_STOP)) ? Convert.ToDecimal(textBoxQtyReal.Text) : 0;
                        if (!item.IsQTYPACKNull() && textBoxQTYPACK.Enabled)
                            item.QTYPACK = Convert.ToDecimal(textBoxQTYPACK.Text);
                        if (!item.IsQTYPACKMJNull() && textBoxQTYPACKMJ.Enabled)
                            item.QTYPACKMJ = textBoxQTYPACKMJ.Text.Trim();
                        if (!item.IsdescriptionNull() && textBoxDescription.Enabled)
                            item.description = textBoxDescription.Text.Trim();
                        if (!item.IsBarcodePNull() && textBoxBarcodeP.Enabled)
                            item.BarcodeP = textBoxBarcodeP.Text.Trim();                        
                        if (!item.IsISOKNull() && dateTimePickerISOK.Enabled)
                            item.ISOK = dateTimePickerISOK.Value;
                        // GUID
                        // SOUBEHGUID
                        // CORRGUID
                        // porovnani qtyOld oproti predchozimu stavu
                        if ((_producttype == PRODUCTIONTYPE.ODVOD_STOP) && (item.IsqtyOldNull() && (item.id == rowProduct.id)))
                            item.qtyOld = Convert.ToDecimal(textBoxQty.Text);
                        //if(!rowProduct.IsqtyOldNull() && (rowProduct.qtyOld != Convert.ToDecimal(textBoxQtyOld.Text)))
                        //{
                        //    // TODO: vypnout editaci puvodniho mnozstvi a datum editace
                        //    item.qtyOld = Convert.ToDecimal(textBoxQtyOld.Text);
                        //}                        
                    }
                    else  // uprava jineho zaznamu, upravuji se pouze casy
                    {
                        if (!item.IsTIMEPREPSTARTNull() && dateTimePickerTIMEPREPSTART.Enabled)
                            item.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                        if (!item.IsTIMEPREPSTOPNull() && dateTimePickerTIMEPREPSTOP.Enabled)
                            item.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                        if (!item.IsTIMECORSTARTNull() && dateTimePickerTIMECORSTART.Enabled)
                            item.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                        if (!item.IsTIMECORSTOPNull() && dateTimePickerTIMECORSTOP.Enabled)
                            item.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;
                        if (!upravitCasUJedneZakazky)
                        {
                            if (!item.IsTIMESTARTNull() && dateTimePickerTIMESTART.Enabled)
                                item.TIMESTART = dateTimePickerTIMESTART.Value;
                            if (!item.IsTIMESTOPNull() && dateTimePickerTIMESTOP.Enabled)
                                item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        }
                        // ma se upravit cas u vsech zakazek
                        //if (!upravitCasUJedneZakazky)
                        //{
                        //    if (!item.IsTIMESTARTNull())
                        //        item.TIMESTART = dateTimePickerTIMESTART.Value;
                        //    if (!item.IsTIMESTOPNull())
                        //        item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        //}
                        //else // uprava casu pouze u jedne zakazky ... hleda se parova
                        //{
                        //    if ((item.CountEntries == rowProduct.CountEntries) && (item.SOPNUMBE == rowProduct.SOPNUMBE))
                        //    {
                        //        if (!item.IsTIMESTARTNull())
                        //            item.TIMESTART = dateTimePickerTIMESTART.Value;
                        //        if (!item.IsTIMESTOPNull())
                        //            item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        //    }
                        //}

                    }                   
                }


                if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_Update))
                    ((Fask.Interfaces.Vyroba.Production.IProduction_Update)providerP).Production_Update(dsV.Production_Konzola);
                else
                    throw new Exception("IProduction_Update not implementet");

                //taProduction.Update(dsV.Production);
                dsV.AcceptChanges();
                

                //rowProduct.qtyOld = rowProduct.qty;
                ////rowProduct.qty = Convert.ToDecimal(textBoxMnozstviNove.Text);
                
                //rowProduct.dateedit = DateTime.Now;
                //rowProduct.idVS = Globals.Pracovnik.id;

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

                int Production_QTYPACKMJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Production["QTYPACKMJ"].MaxLength;

                bool status;
                int _countEntries = 0;
                // validace COUNTENTRIES, enabled se nemusi zatim kontrolovat
                if (!rowProduct.IsCountEntriesNull())
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

                // validace SOPNUMBE
                if (!rowProduct.IsSOPNUMBENull())
                {
                    if (string.IsNullOrEmpty(textBoxSOPNUMBE.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxSOPNUMBE, "Číslo výrobní zakázky musí být vyplněno");
                    }
                }

                // kontrola, zdali existuje zaznam v VPH
                if (!rowProduct.IsCountEntriesNull() && !rowProduct.IsSOPNUMBENull())
                {
                    if(string.IsNullOrEmpty(errorProvider1.GetError(textBoxCountEntries)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxSOPNUMBE)))
                    {
                        //var _validCountEntriesSOPNUMBE = taVPH.GetDataByCountEntriesSOPNUMBE(Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text);

                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable _validCountEntriesSOPNUMBE;

                        if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE)
                            _validCountEntriesSOPNUMBE = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE)providerVPH).GetDataByCountEntriesSOPNUMBE(Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text);
                        else
                            throw new Exception("IVPH_GetDataByCountEntriesSOPNUMBE not implementet");


                        if (_validCountEntriesSOPNUMBE.Count() == 0)
                        {
                            errorProvider1.SetError(textBoxSOPNUMBE, "Výrobní příkaz s číslem dávky: " + textBoxCountEntries.Text.Trim() + " a číslem výrobní zakázky: " + textBoxSOPNUMBE.Text.Trim() + " nebyl nalezen");
                            errorProvider1.SetError(textBoxCountEntries, "Výrobní příkaz s číslem dávky: " + textBoxCountEntries.Text.Trim() + " a číslem výrobní zakázky: " + textBoxSOPNUMBE.Text.Trim() + " nebyl nalezen");
                            //throw new Exception("Výrobní příkaz s číslem dávky: " + textBoxCountEntries.Text.Trim() + " a číslem výrobní zakázky: " + textBoxSOPNUMBE.Text.Trim() + " nebyl nalezen");
                        }
                    }
                }

                // kontrola, zdali existuje zaznam v VPP
                if (!rowProduct.IsITEMNMBRNull())
                {
                    if (string.IsNullOrEmpty(textBoxITEMNMBR.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxITEMNMBR, "Číslo položky musí být vyplněno");
                    }
                    else
                    {
                        if(string.IsNullOrEmpty(errorProvider1.GetError(textBoxCountEntries)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxSOPNUMBE)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxITEMNMBR)))
                        {

                            //taVPP.FillByCountEntriesSOPNUMBEITEMNMBR(ds.CZPRO_VPP, Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim());
                            //var _validCountEntriesSOPNUMBEITEMNMBR = taVPP.GetDataByCountEntriesSOPNUMBEITEMNMBR(Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim());


                            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable _validCountEntriesSOPNUMBEITEMNMBR;

                            if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR)
                                _validCountEntriesSOPNUMBEITEMNMBR = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR)providerVPP).GetDataByCountEntriesSOPNUMBEITEMNMBR(Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim());
                            else
                                throw new Exception("IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR not implementet");




                            if (_validCountEntriesSOPNUMBEITEMNMBR.Count() == 0)
                            {
                                errorProvider1.SetError(textBoxITEMNMBR, "Položka s číslem: " + textBoxITEMNMBR.Text.Trim() + " nebyla nalezena");
                            }
                        }
                    }
                }

                if (!rowProduct.IsORDNull())
                {
                    if (string.IsNullOrEmpty(textBoxORD.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxORD, "Pořadí položky musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        int _ORD;
                        status = Int32.TryParse(textBoxORD.Text, out _ORD);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxORD, "Pořadí položky musí být číslo");
                        }
                        else
                        {
                            if (_ORD < 0)
                            {
                                errorProvider1.SetError(textBoxORD, "Pořadí položky musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola timemode
                if (!rowProduct.IsTIMEMODENull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMEMODE.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMEMODE, "Typ sledování času musí být vyplněn");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        int _timemode;
                        status = Int32.TryParse(textBoxTIMEMODE.Text, out _timemode);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMEMODE, "Typ sledování času musí být číslo");
                        }
                        else
                        {
                            if((_timemode < 0) || (_timemode > 2))
                            {
                                errorProvider1.SetError(textBoxTIMEMODE, "Typ sledování času musí být v rozmezí 0 až 2");
                            }
                        }
                    }
                }

                // kontrola TIMEPREPSTART
                if(!rowProduct.IsTIMEPREPSTARTNull() && !rowProduct.IsTIMEPREPSTOPNull())
                {
                    if (dateTimePickerTIMEPREPSTART.Value >= dateTimePickerTIMEPREPSTOP.Value)
                    {
                        errorProvider1.SetError(dateTimePickerTIMEPREPSTOP, "Stop čas přípravy musí být větší než Start čas přípravy");
                    }
                }                

                // kontrola TIMESTART
                if (!rowProduct.IsTIMESTARTNull())
                {
                    if (dateedit || !rowProduct.IsTIMESTOPNull())
                    {
                        if (dateTimePickerTIMESTART.Value >= dateTimePickerTIMESTOP.Value)
                        {
                            errorProvider1.SetError(dateTimePickerTIMESTOP, "Čas ukončení musí být větší než čas zahájení");
                        }
                    }
                }

                // kontrola TIMECORSTART
                if (!rowProduct.IsTIMECORSTARTNull())
                {
                    if (dateedit || !rowProduct.IsTIMECORSTOPNull())
                    {
                        if (dateTimePickerTIMECORSTART.Value >= dateTimePickerTIMECORSTOP.Value)
                        {
                            errorProvider1.SetError(dateTimePickerTIMECORSTOP, "Čas ukončení korekce musí být větší než čas zahájení korekce");
                        }
                    }
                }

                // kontrola loginid (id vedouciho smeny)
                if (!string.IsNullOrEmpty(textBoxLoginid.Text.Trim()))
                {
                    //var _validLoginid = taLogins.GetDataByID(textBoxLoginid.Text.Trim());

                    //Fask.Interfaces.DataSets.Vyroba.LoginsDataTable _validUserid;

                    //if (providerLogins is Fask.Interfaces.Vyroba.Login.ILogin_GetDataByID)
                    //    _validUserid = ((Fask.Interfaces.Vyroba.Login.ILogin_GetDataByID)providerLogins).GetDataByID(textBoxUserID.Text.Trim());
                    //else
                    //    throw new Exception("ILogin_GetDataByID not implementet");

                    var _validUserid = FASK.Logins.Uzivatel.Instance.Komunikace.GetLoginsByID(textBoxUserID.Text.Trim());

                    //var _validLoginid = ds.Logins.Where(x => x.id == textBoxLoginid.Text.Trim());
                    // nenalezen
                    if (_validUserid.Count() == 0)
                    {
                        errorProvider1.SetError(textBoxLoginid, "Vedoucí směny nebyl nalezen");
                    }
                    else
                    {
                        byte VS = FASK.Logins.Uzivatel.Instance.GetUzivatele_VedouciSmeny(_validUserid.First());
                        // kontrola, zdali je uživatel vedoucí směny
                        //if (_validUserid.First().VS != 1)
                        if (VS != 1)
                        {
                            errorProvider1.SetError(textBoxLoginid, "Uživatel " + _validUserid.First().firstname.Trim() + " " + _validUserid.First().surname.Trim() + " není vedoucí směny");
                        }

                    }
                }

                // kontrola machineid
                if (!rowProduct.IsmachineidNull())
                {
                    if (string.IsNullOrEmpty(textBoxMachineid.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxMachineid, "Id stroje musí být vyplněno");
                    }
                    else
                    {
                        //var _validMachineid = taMachines.GetDataByID(textBoxMachineid.Text.Trim());


                        Fask.Interfaces.DataSets.Vyroba.MachinesDataTable _validMachineid;

                        if (providerMachines is Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID)
                            _validMachineid = ((Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID)providerMachines).Machines_GetDataByID(textBoxMachineid.Text.Trim());
                        else
                            throw new Exception("IMachines_GetDataByID not implementet");



                        //var _validMachineid = ds.Machines.Where(x => x.id == textBoxMachineid.Text.Trim());
                        if (_validMachineid.Count() == 0)
                        {
                            errorProvider1.SetError(textBoxMachineid, "Id stroje nebylo nalezeno");
                        }
                    }
                }

                // kontrola operationid
                if (!rowProduct.IsoperationidNull())
                {
                    if (string.IsNullOrEmpty(textBoxOperationid.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxOperationid, "Id operace musí být vyplněno");
                    }
                    else
                    {
                        //var _validOperationid = taOperations.GetDataByID(textBoxOperationid.Text.Trim());


                        Fask.Interfaces.DataSets.Vyroba.OperationsDataTable _validOperationid;

                        if (providerOperations is Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID)
                            _validOperationid = ((Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID)providerOperations).Operations_GetDataByID(textBoxOperationid.Text.Trim());
                        else
                            throw new Exception("IOperations_GetDataByID not implementet");

                        //var _validOperationid = ds.Operations.Where(x => x.id == textBoxOperationid.Text.Trim());
                        if (_validOperationid.Count() == 0)
                        {
                            errorProvider1.SetError(textBoxOperationid, "Id stroje nebylo nalezeno");
                        }
                    }
                }


                // kontrola machineid a operationid
                if (!rowProduct.IsmachineidNull() && !rowProduct.IsoperationidNull())
                {
                    if (string.IsNullOrEmpty(textBoxMachineid.Text.Trim()) && (string.IsNullOrEmpty(textBoxOperationid.Text.Trim())))
                    {
                        //var _validMachineidOperationid = tavMachinesOperations.GetDataByMachineIDoperationID(textBoxMachineid.Text.Trim(), textBoxOperationid.Text.Trim());


                        Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable _validMachineidOperationid;

                        if (providerVMachinesOperations is Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID)
                            _validMachineidOperationid = ((Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID)providerVMachinesOperations).VMachinesOperations_GetDataByMachineIDoperationID(textBoxMachineid.Text.Trim(), textBoxOperationid.Text.Trim());
                        else
                            throw new Exception("IVMachinesOperations_GetDataByMachineIDoperationID not implementet");




                        if (_validMachineidOperationid.Count == 0)
                        {
                            errorProvider1.SetError(textBoxMachineid, "K id stroje " + textBoxMachineid.Text.Trim() + " nebyla nalezena operace s id " + textBoxOperationid.Text.Trim());
                        }
                    }
                }
                
                // kontrola QTYPACKMJ
                if (!rowProduct.IsQTYPACKMJNull())
                {
                    if (textBoxQTYPACKMJ.Text.Trim().Length > Production_QTYPACKMJ_MaxLength)
                    {
                        errorProvider1.SetError(textBoxQTYPACKMJ, "Měrná jednotka balení může mít maximálně " + Production_QTYPACKMJ_MaxLength + " znaků");
                    }
                    // empty muze byt
                    
                }

                // kontrola qty                
                if (string.IsNullOrEmpty(textBoxQty.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxQty, "Počet kusů musí být vyplněn");
                }
                else   // kontrola, zdali je cislo
                {
                    decimal _qty;
                    status = Decimal.TryParse(textBoxQty.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxQty, "Počet kusů musí být číslo");
                    }
                    else
                    {
                        if (_qty < 0)
                        {
                            errorProvider1.SetError(textBoxQty, "Počet kusů musí být větší než nebo rovno 0");
                        }
                    }
                }

                // kontrola qtyReal                
                if (string.IsNullOrEmpty(textBoxQtyReal.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxQtyReal, "Počet kusů sejmuto musí být vyplněn");
                }
                else   // kontrola, zdali je cislo
                {
                    decimal _qty;
                    status = Decimal.TryParse(textBoxQtyReal.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxQtyReal, "Počet kusů sejmuto musí být číslo");
                    }
                    else
                    {
                        if (_qty < 0)
                        {
                            errorProvider1.SetError(textBoxQtyReal, "Počet kusů sejmuto musí být větší než nebo rovno 0");
                        }
                    }
                }

                // kontrola QTYPACK
                if (!rowProduct.IsQTYPACKNull())
                {
                    if (string.IsNullOrEmpty(textBoxQTYPACK.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxQTYPACK, "Množství v balení musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        decimal _qty;
                        status = Decimal.TryParse(textBoxQTYPACK.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxQTYPACK, "Množství v balení musí být číslo");
                        }
                        else
                        {
                            if (_qty < 0)
                            {
                                errorProvider1.SetError(textBoxQTYPACK, "Množství v balení musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola UserID
                if(string.IsNullOrEmpty(textBoxUserID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxUserID, "Id uživatele musí být vyplněno");
                }
                else
                {
                   //var _validUserid = taLogins.GetDataByID(textBoxUserID.Text.Trim());
                    //Fask.Interfaces.DataSets.Vyroba.LoginsDataTable _validUserid;

                    //if (providerLogins is Fask.Interfaces.Vyroba.Login.ILogin_GetDataByID)
                    //    _validUserid = ((Fask.Interfaces.Vyroba.Login.ILogin_GetDataByID)providerLogins).GetDataByID(textBoxUserID.Text.Trim());
                    //else
                    //    throw new Exception("ILogin_GetDataByID not implementet");

                    var _validUserid = FASK.Logins.Uzivatel.Instance.Komunikace.GetLoginsByID(textBoxUserID.Text.Trim());

                    //var _validLoginid = ds.Logins.Where(x => x.id == textBoxLoginid.Text.Trim());
                    // nenalezen
                    if (_validUserid.Count() == 0)
                    {
                        errorProvider1.SetError(textBoxUserID, "Id uživatele nebylo nalezeno");
                    }
                }

                // kontrola TermID
                if (string.IsNullOrEmpty(textBoxTermID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxTermID, "Id terminálu musí být vyplněno");
                }
                else
                {
                    byte _termid;
                    status = Byte.TryParse(textBoxTermID.Text, out _termid);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxTermID, "Id terminálu musí být číslo");
                    }
                }

                // kontrola TIMEUNIT
                if (!rowProduct.IsTIMEUNITNull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMEUNIT.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMEUNIT, "Jednotkový čas musí být vyplněn");
                    }
                    else
                    {
                        float timeunit;
                        status = float.TryParse(textBoxTIMEUNIT.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeunit);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMEUNIT, "Jednotkový čas musí být číslo");
                        }
                        else
                        {
                            if (timeunit < 0)
                            {
                                errorProvider1.SetError(textBoxTIMEUNIT, "Jednotkový čas musí být větší, nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola TIMEPREP
                if (!rowProduct.IsTIMEPREPNull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMEPREP.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMEPREP, "Přípravný čas musí být vyplněn");
                    }
                    else
                    {
                        float timeprep;
                        status = float.TryParse(textBoxTIMEPREP.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeprep);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMEPREP, "Přípravný čas musí být číslo");
                        }
                        else
                        {
                            if (timeprep < 0)
                            {
                                errorProvider1.SetError(textBoxTIMEPREP, "Přípravný čas musí být větší, nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola TIMECRID, 0 - nesouvisejici se zakazkou, 1 - souvisejici
                if (!rowProduct.IsTIMECRIDNull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMECRID.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMECRID, "Id korekce musí být vyplněno");
                    }
                    else  // nějaký text je vyplněn
                    {
                        int _timecrid;
                        status = Int32.TryParse(textBoxTIMECRID.Text, out _timecrid);
                        // není vyplněno číslo
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMECRID, "Id korekce musí být číslo");
                        }
                        else
                        {
                            //var _validTimecrid = taCorrects.GetDataByID(_timecrid);

                            Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable _validTimecrid;

                            if (providerCorrects is Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID)
                                _validTimecrid = ((Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID)providerCorrects).Corrects_GetDataByID(_timecrid);
                            else
                                throw new Exception("ICorrects_GetDataByID not implementet");


                            // id korekce nenalezeno
                            if (_validTimecrid.Count == 0)
                            {
                                errorProvider1.SetError(textBoxTIMECRID, "Id korekce nebylo nalezeno");
                            }
                            else
                            {
                                // korekce vazana k vyrobe
                                if ((_producttype == PRODUCTIONTYPE.KOREKCE_VAZANA_START) || (_producttype == PRODUCTIONTYPE.KOREKCE_VAZANA_STOP))
                                {

                                    if (_validTimecrid.First().Production != 1)
                                    {
                                        errorProvider1.SetError(textBoxTIMECRID, "Zvolená korekce není vázaná k výrobě, i když má být");
                                    }
                                }
                                else  // nevazana korekce
                                {
                                    if (_validTimecrid.First().Production != 0)
                                    {
                                        errorProvider1.SetError(textBoxTIMECRID, "Zvolená korekce je vázaná k výrobě, i když nemá být");
                                    }
                                }
                            }
                        }
                    }
                }

                // kontrola description
                //if (!rowProduct.IsdescriptionNull())

                // kontrola BarcodeP
                if (!rowProduct.IsBarcodePNull())
                {
                    if (string.IsNullOrEmpty(textBoxBarcodeP.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxBarcodeP, "Čáč. kód položky musí být vyplněn");
                    }
                    else
                    {
                        // pokud neni chyba v potrebnych hodnotach, probehne validace
                        if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxCountEntries)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxSOPNUMBE)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxITEMNMBR)))
                        {
                            //var _validBarcodeP = taVPP.GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(Convert.ToInt32(textBoxCountEntries.Text.Trim()), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim(), textBoxBarcodeP.Text.Trim());


                            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable _validBarcodeP;

                            if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP)
                                _validBarcodeP = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP)providerVPP).GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(Convert.ToInt32(textBoxCountEntries.Text.Trim()), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim(), textBoxBarcodeP.Text.Trim());
                            else
                                throw new Exception("IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP not implementet");


                            if (_validBarcodeP.Count == 0)
                            {
                                errorProvider1.SetError(textBoxBarcodeP, "Položka s čár. kódem " + textBoxBarcodeP.Text.Trim() + " nebyla nalezena");
                            }
                        }
                    }
                }

                var sklad = textBoxSklad.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;                    
                if (!rowProduct.IsSKL_IDNull())
                { // je zadan sklad, tak se bude menit ...
                    if (sklad == null)
                    {
                        errorProvider1.SetError(textBoxSklad, "Sklad není nastaven");
                    }
                }

                if (!rowProduct.IsLOCNCODENull())
                {
                    var lokace = textBoxLokace.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;
                    if (lokace == null)
                    {
                        errorProvider1.SetError(textBoxLokace, "Lokace není nastavena");
                    }
                    if (sklad == null)
                    {
                        errorProvider1.SetError(textBoxSklad, "Sklad není nastaven");
                    }

                    if (lokace != null && sklad != null)
                    {
                        if (sklad.skl_id.Trim() != lokace.SKL_ID.Trim())
                        {
                            errorProvider1.SetError(textBoxLokace, "Sklad vybrané lokace neodpovídá vybranému skladu");
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

        /// <summary>
        /// Najití a vyplnění datumu podle všech zakázek/korekcí.
        /// </summary>
        private void FindTimeStop()
        {
            try
            {
                //var taProduction = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                //taProduction.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                //List<Fask.Interfaces.DataSets.Vyroba.ProductionRow> prRows = new List<DataServices.VyrobaDataSet.ProductionRow>();

                Fask.Interfaces.DataSets.Vyroba dsV = new Fask.Interfaces.DataSets.Vyroba();

                // není vyplněno datum stop, hledá se aktuální
                if (rowProduct.IsTIMECORSTOPNull() && rowProduct.IsTIMESTOPNull())
                {                    
                    // kontrola TIMESTART
                    if (!rowProduct.IsSOUBEHGUIDNull())
                    {
                        //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID));
                        //taProduction.FillBySOUBEHGUID(dsV.Production, rowProduct.SOUBEHGUID);

                        if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID))
                            ((Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID)providerP).Production_FillBySOUBEHGUID(dsV, rowProduct.SOUBEHGUID);
                        else
                            throw new Exception("IProduction_FillBySOUBEHGUID not implementet");
                    }
                    else
                    {
                        // korekce
                        if (!rowProduct.IsCORRGUIDNull())
                        {
                            //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.CORRGUID));
                            //taProduction.FillByCORRGUID(dsV.Production, rowProduct.CORRGUID);
                            //prRows.AddRange(dsV.Production);

                            if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID))
                                ((Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID)providerP).Production_FillByCORRGUID(dsV, rowProduct.CORRGUID);
                            else
                                throw new Exception("IProduction_FillByCORRGUID not implementet");
                        }
                    }

                    var _timestop = dsV.Production_Konzola.Where(x => !x.IsTIMESTOPNull() || !x.IsTIMECORSTOPNull());
                    // nalezena nejaka ukoncena korekce/zakazka
                    if (_timestop.Count() > 0)
                    {
                        var record = _timestop.First();
                        // timestop vyplnen
                        if (!record.IsSOUBEHGUIDNull() && !record.IsTIMESTOPNull())
                        {
                            dateedit = true;
                            dateTimePickerTIMESTOP.Value = record.TIMESTOP;
                        }
                        else
                        {
                            // timecorstop vyplnen
                            if (!record.IsCORRGUIDNull() && !record.IsTIMECORSTOPNull())
                            {
                                dateedit = true;
                                dateTimePickerTIMECORSTOP.Value = record.TIMECORSTOP;
                            }
                        }
                        
                    }
                    //if ((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
                    //{
                    //    // nadop
                    //    if (rowProduct.IsITEMNMBRNull())
                    //    {
                    //        prRows.AddRange(dsV.Production.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE)).ToList());
                    //    }
                    //    else  // zbytek
                    //    {
                    //        prRows.AddRange(dsV.Production.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE) && (x.ITEMNMBR == rowProduct.ITEMNMBR)).ToList());
                    //    }
                    //    //dsV.Production.Where(
                    //}
                }

                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        private void textBoxSklad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // aktualizovat polozky lokaci a pokusit se nechat vybrat lokaci se stejnym kodem ... na nove ...
                var lokaceAkt = textBoxLokace.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;

                var sklad = textBoxSklad.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                if (sklad == null)
                    return;

                textBoxLokace.Items.Clear();
                textBoxLokace.Items.AddRange(dsVyroba.CZMST094.Where(x => x.SKL_ID == sklad.skl_id).ToArray());

                if (lokaceAkt != null)
                {
                    var lokaceNove = dsVyroba.CZMST094.Where(x => x.SKL_ID == sklad.skl_id && x.Barcode == lokaceAkt.Barcode);
                    if (lokaceNove.Count() > 0)
                        textBoxLokace.SelectedItem = lokaceNove.First();
                    else
                        textBoxLokace.SelectedIndex = 0;
                }
                else 
                {
                    textBoxLokace.SelectedIndex = 0;
                }

            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HledatCarovyKod();
        }

        private void čárovýKódToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HledatCarovyKod();
        }

        private void HledatCarovyKod()
        {
            using(FormProductionEdit2_CarKod fpeck = new FormProductionEdit2_CarKod())
            {
                fpeck.Text = "Změna výrobku";
                if (fpeck.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                RowCKod_ZASOBY = fpeck.Row_ZASOBY;
            
            }

            textBoxITEMNMBR.Text = RowCKod_ZASOBY.ITEMNMBR.Trim();
            textBoxITEMDESC.Text = RowCKod_ZASOBY.ITEMDESC.Trim();
            textBoxBarcodeP.Text = RowCKod_ZASOBY.VNDITNUM.Trim();
        }
    }
}
