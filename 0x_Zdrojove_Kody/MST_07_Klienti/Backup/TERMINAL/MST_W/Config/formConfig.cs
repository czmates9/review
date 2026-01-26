using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;
using System.Net;
using System.Xml;
using System.Diagnostics;
using Fask.Localization;

using System.Collections;
using Fask.ScannerProvider;
using Fask.Graphic;
using Fask.MST_W.RFID;

using Fask.MST_W.Extensions;

namespace Fask.MST_W.Config
{
    public partial class formConfig : System.Windows.Forms.Form
    {

		#region Roztridene

		/// <summary>
		/// Konstruktor
		/// </summary>
		public formConfig()
		{
			Cursor.Current = Cursors.WaitCursor;
			InitializeComponent();
			Cursor.Current = Cursors.Default;
		}

		#region Eventy pro checkBoxy pro variantu Tisk s Cenou/bez ceny na modulu Prijem a Prodej

		private void chk_Prodej_EtiketaTiskDotazSCenou_CheckStateChanged(object sender, EventArgs e)
		{
			if (chk_Prodej_EtiketaTiskDotazSCenou.Checked)
			{
				chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog.Enabled = true;
				chk_Prodej_EtiketaTiskDotazSCenou_Cena.Enabled = true;
				chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog_CheckStateChanged(null, null);
			}
			else
			{
				chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog.Enabled = false;
				chk_Prodej_EtiketaTiskDotazSCenou_Cena.Enabled = false;
			}

			//chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog_CheckStateChanged(null, null);

		}

		private void chk_Prijem_DialogTiskSCenou_CheckStateChanged(object sender, EventArgs e)
		{
			if (chk_Prijem_DialogTiskSCenou.Checked)
			{
				chk_Prijem_DialogTiskSCenou_ZobrazDialog.Enabled = true;
				chk_Prijem_DialogTiskSCenou_Cena.Enabled = true;
				chk_Prijem_DialogTiskSCenou_ZobrazDialog_CheckStateChanged(null, null);
			}
			else
			{
				chk_Prijem_DialogTiskSCenou_ZobrazDialog.Enabled = false;
				chk_Prijem_DialogTiskSCenou_Cena.Enabled = false;
			}

			//chk_Prijem_DialogTiskSCenou_ZobrazDialog_CheckStateChanged(null,null);
		}

		private void chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog_CheckStateChanged(object sender, EventArgs e)
		{
			if (chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog.Checked)
			{
				chk_Prodej_EtiketaTiskDotazSCenou_Cena.Enabled = false;
			}
			else
			{
				chk_Prodej_EtiketaTiskDotazSCenou_Cena.Enabled = true;

			}
		}

		private void chk_Prijem_DialogTiskSCenou_ZobrazDialog_CheckStateChanged(object sender, EventArgs e)
		{
			if (chk_Prijem_DialogTiskSCenou_ZobrazDialog.Checked)
			{
				chk_Prijem_DialogTiskSCenou_Cena.Enabled = false;
			}
			else
			{
				chk_Prijem_DialogTiskSCenou_Cena.Enabled = true;
			}

		}

		
		#endregion

		#region Scanner

		private bool scannserstart = true;
		private void ScannerFinalize()
		{
			this.ScannerStop();
			this.scannserstart = false;
		}

		private void ScannerStart()
		{
			if (!scannserstart)
				return;

			Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
			Program.mstw.EnableScanner();
		}

		private void ScannerStop()
		{
			Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
			Program.mstw.DisableScanner();
		}

		delegate void MethodInvoker();

		void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
		{
			this.BeginInvoke(
				(MethodInvoker)delegate()
				{
					textBoxScannerData.Text = DateTime.Now.ToString() + "\r\n" + e.ToString();
				}
			);
		}

		#endregion
		
		#endregion

        private void formConfig_Load(object sender, EventArgs e)
        {
            try
            {
                //Presunuto do hlavniho dialogu pro odstraneni dlouheho cekani pri konstrukci InitializeComponents...
                //using (FormAdminAccess frmAccess = new FormAdminAccess())
                //{
                //    frmAccess.Location = MySystem.FormMidLocation.GetFormLocation(frmAccess.Size);
                //    if (frmAccess.ShowDialog() != DialogResult.OK)
                //    {
                //        DialogResult = DialogResult.Cancel;
                //        return;
                //    }
                //}

                Cursor.Current = Cursors.WaitCursor;



                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                this.lblMSTWVersion.Text = version.ToString(4);
                this.lblMSTWVersion1.Text = version.ToString(4);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                try { cbSqlCe.Checked = MST_Global.SqlCe; }
                catch { }
                try { cfgTerminalID.Value = MST_Global.TerminalID; }
                catch { }

                // lokalizace
                cbLokalizacePovolit.Checked = MST_Global.LokalizacePovolit;
                cbLokalizaceVlastniPovolit.Checked = MST_Global.LokalizaceVlastniPovolit;
                // Create a List of type <string> to put our results in to.
                List<string> results = new List<string>();
                // iterate the items in the Enumerable type via ordinal reference.
                for (int i = 0; Enum.IsDefined(typeof(Localization.LocalizationSupport.LocalType), i); i++)
                    // Convert the name of the type member to a string and add it
                    //   to our List called 'result'
                    results.Add(((Localization.LocalizationSupport.LocalType)i).ToString());
                // Bind the Datasource of our combobox to the List called 'results'.
                cbLokalizaceZvolena.DataSource = results;
                cbLokalizaceZvolena.SelectedIndex = (int)MST_Global.LokalizaceZvolena;
 
                //List<Fask.MST_W.Localization.LocalizationSupport.LocalizationItem> list = new List<Fask.MST_W.Localization.LocalizationSupport.LocalizationItem>();
                //Fask.MST_W.Localization.LocalizationSupport.LocalizationItem locitem = new Fask.MST_W.Localization.LocalizationSupport.LocalizationItem(Fask.MST_W.Localization.LocalizationSupport.LocalType.Výchozí, Fask.MST_W.Localization.LocalizationSupport.LocalValue.cs);
                //list.Add(locitem);
                //list.Add(new Fask.MST_W.Localization.LocalizationSupport.LocalizationItem(Fask.MST_W.Localization.LocalizationSupport.LocalType.Èeština, Fask.MST_W.Localization.LocalizationSupport.LocalValue.cs));
                //list.Add(new Fask.MST_W.Localization.LocalizationSupport.LocalizationItem(Fask.MST_W.Localization.LocalizationSupport.LocalType.Slovenština, Fask.MST_W.Localization.LocalizationSupport.LocalValue.sk));
                //cbLokalizaceZvolena.DataSource = list;
                //// zvoleni
                //// vybrat pokud existuje, jinak default
                //try
                //{

                //    Fask.MST_W.Localization.LocalizationSupport.LocalizationItem result = list.Find(x => x.Value == (Fask.MST_W.Localization.LocalizationSupport.LocalValue)Enum.Parse(typeof(Fask.MST_W.Localization.LocalizationSupport.LocalValue), MST_Global.LokalizaceZvolena, true));
                //    cbLokalizaceZvolena.SelectedItem = result;
                //}
                //catch
                //{
                //    cbLokalizaceZvolena.SelectedItem = locitem;
                //}                

                textBoxTerminalAdminPwd.Text = Settings.AdminPwd;
                cfgTerminalServerAddress.Text = MST_Global.Adresa_API;

				cfgAPIKonstanta.Text = MST_Global.API_konstant;
				cfgAutorizaceAPI.Text = MST_Global.Autorizace_API;
				chb_ishttps.Checked = MST_Global.isHTTPS;

                #region PrintServer Factory settings
                //PrintServer
                /*
                cfgPrintServerTemplateNamePrijemPredloha.Text = MST_Global.PrintServerTemplateNamePrijemPredloha;
                cfgPrintServerTemplateNamePrijemNasnimane.Text = MST_Global.PrintServerTemplateNamePrijemNasnimane;
                cfgPrintServerTemplateNameVydejPredloha.Text = MST_Global.PrintServerTemplateNameVydejPredloha;
                cfgPrintServerTemplateNameVydejNasnimane.Text = MST_Global.PrintServerTemplateNameVydejNasnimane;
                cfgPrintServerTemplateNameVydejPalListek.Text = MST_Global.PrintServerTemplateNameVydejPalListek;
                cfgPrintServerTemplateNameProdejPredloha.Text = MST_Global.PrintServerTemplateNameProdejPredloha;
                cfgPrintServerTemplateNameProdejNasnimane.Text = MST_Global.PrintServerTemplateNameProdejNasnimane;
                cfgPrintServerTemplateNameInventuraPredloha.Text = MST_Global.PrintServerTemplateNameInventuraPredloha;
                cfgPrintServerTemplateNameInventuraNasnimane.Text = MST_Global.PrintServerTemplateNameInventuraNasnimane;
                */

                // JiS : nove PrintServer...
                // 0) nastavi hodnoty typu tiskaren do comboboxu typu tiskaren ... 
                // 1) nacist seznamy sablon
                // 2) naplnit seznamy
                // 3) nacist nastaveni do parametru ...
                // 4) naplnit nastaveni tiskaren a sablon
                PrinterFactory.PrinterFactory pFactory = PrinterFactory.PrinterFactory.Instance;
                List<string> printerTypes = new List<string>();
                // TODO : nacist seznam typu z PrinterFactory.xml
                // + ?umoznit pridani noveho typu ... 
                // + pridat zobrazeni configuracniho souboru z printerfactory.xml ... 
                //printerTypes.AddRange(new string[] {"None", "WebService", "WebService2", "Bluetooth"});
                
                printerTypes.AddRange(loadPrinterTypes().ToArray());
                //printerTypes.Add(string.None);
                //printerTypes.Add(string.Bluetooth);
                //printerTypes.Add(string.WebService);
                //printerTypes.Add(string.WebService2);
                foreach (var item in printerTypes)
                {
                    comboBoxPrinterSelection.Items.Add(item);

                    cfgPrintServerPrinterTypeInventuraNasnimane.Items.Add(item);
                    cfgPrintServerPrinterTypeInventuraPredloha.Items.Add(item);
                    cfgPrintServerPrinterTypePrijemNasnimane.Items.Add(item);
                    cfgPrintServerPrinterTypePrijemPredloha.Items.Add(item);
                    cfgPrintServerPrinterTypeProdejSoupisList.Items.Add(item);
                    cfgPrintServerPrinterTypeProdejPaletyList.Items.Add(item);
                    cfgPrintServerPrinterTypeProdejNasnimane.Items.Add(item);
                    cfgPrintServerPrinterTypeProdejPredloha.Items.Add(item);
                    cfgPrintServerPrinterTypeVydejNasnimane.Items.Add(item);
                    cfgPrintServerPrinterTypeVydejPalListek.Items.Add(item);
                    cfgPrintServerPrinterTypeVydejPredloha.Items.Add(item);
                }

                // Udalosti zmeny vybrane tiskarny a naplnenit items
                comboBoxPrinterSelection.SelectedIndexChanged += new EventHandler(comboBoxPrinterSelection_SelectedIndexChanged);

                cfgPrintServerPrinterTypeInventuraNasnimane.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeInventuraPredloha.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypePrijemNasnimane.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypePrijemPredloha.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeProdejSoupisList.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeProdejPaletyList.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeProdejNasnimane.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeProdejPredloha.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeVydejNasnimane.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeVydejPalListek.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);
                cfgPrintServerPrinterTypeVydejPredloha.SelectedIndexChanged += new EventHandler(cfgPrintServerPrinterType_SelectedIndexChanged);

                // ad 1)
                // udalosti na controlech typu tiskarny naplnuji odpovidajici hodnoty do comboboxu ... 
                printerTemplatesList = new Dictionary<string, List<string>>();
                foreach (var item in pFactory.Printers.Keys)
                {
                    try
                    {
                        Fask.PrinterProvider.IPrinterProvider pprovider = pFactory.Printers[item].PrinterProvider;
                        if (pprovider != null)
                            printerTemplatesList.Add(item, new List<string>(pprovider.GetTemplatesList()));
                        else
                            printerTemplatesList.Add(item, new List<string>());
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                    }
                }

                // ad 3), 4)
                var templates = pFactory.Templates;
                foreach (var key in templates.Keys)
                {
                    //m2p = module2print
                    Fask.PrinterFactory.ModuleToPrint m2p = templates[key];
                    switch (key)
                    {
                        case Fask.PrinterFactory.PrinterModules.PrijemPredloha:
                            cfgPrintServerPrinterTypePrijemPredloha.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNamePrijemPredloha.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.PrijemNasnimane:
                            cfgPrintServerPrinterTypePrijemNasnimane.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNamePrijemNasnimane.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.VydejPredloha:
                            cfgPrintServerPrinterTypeVydejPredloha.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameVydejPredloha.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.VydejNasnimane:
                            cfgPrintServerPrinterTypeVydejNasnimane.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameVydejNasnimane.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.VydejPaletovylistek:
                            cfgPrintServerPrinterTypeVydejPalListek.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameVydejPalListek.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejPredloha:
                            cfgPrintServerPrinterTypeProdejPredloha.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejPredloha.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejNasnimane:
                            cfgPrintServerPrinterTypeProdejNasnimane.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejNasnimane.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejSoupisHlavicka:
                            cfgPrintServerPrinterTypeProdejSoupisList.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejSoupisListHlavicka.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejSoupisRadek:
                            cfgPrintServerPrinterTypeProdejSoupisList.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejSoupisListRadek.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejSoupisPaticka:
                            cfgPrintServerPrinterTypeProdejSoupisList.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejSoupisListPaticka.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejPaletaHlavicka:
                            cfgPrintServerPrinterTypeProdejPaletyList.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejPaletyListHlavicka.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejPaletaRadek:
                            cfgPrintServerPrinterTypeProdejPaletyList.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejPaletyListRadek.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.ProdejPaletaPaticka:
                            cfgPrintServerPrinterTypeProdejPaletyList.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameProdejPaletyListPaticka.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.InventuraPredloha:
                            cfgPrintServerPrinterTypeInventuraPredloha.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameInventuraPredloha.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.InventuraNasnimane:
                            cfgPrintServerPrinterTypeInventuraNasnimane.SelectedItem = m2p.PrinterType;
                            cfgPrintServerTemplateNameInventuraNasnimane.Text = m2p.Template;
                            break;
                        case Fask.PrinterFactory.PrinterModules.TextVolny:
                            break;
                        default:
                            break;
                    }
                }

                chckPovolitPrintServer.Checked = MST_Global.PovolitPrintServer;
                #endregion

                try { cfgTerminalServiceTimeout.Text = MST_Global.ServiceTimeOut.ToString(); }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, "ServiceTimeOut");
                    //MessageBox.Show(ex.Message);
                }
                cbLog.Checked = Fask.Logging.Log.Enable;
                cbTrace.Checked = Fask.Logging.Trace2.Enable;
                cfgLogUploadInterval.Text =  MST_Global.LogUploadInterval.ToString();
                tb_trace_separator.Text = Fask.Logging.Trace2.Separator;

                tb_VelkostPamete.Text = MST_Global.MemorySize;
                cbKontrolaPamete.Checked = MST_Global.MemoryChecked;

                cbShowButtonsPanel.Checked = MST_Global.ShowPanelButtons;
                cfgTerminalStorageFolder.Text = MST_Global._Storage;

                switch (MST_Global.ServerAccess)
                {
                    case MST_Global.ServerAccessType.Credentials:
                        rbSeverAccessUserNamePassword.Checked = true;
                        break;
                    case MST_Global.ServerAccessType.Anonymous:
                    default:
                        rbSeverAccessAnonymous.Checked = true;
                        break;
                }
                txtServerAccessUsername.Text = MST_Global.ServerAccessUsername;
                txtServerAccessPassword.Text = MST_Global.ServerAccessPassword;
                txtServerAccessDomain.Text = MST_Global.ServerAccessDomain;
                chkServerAccessPreauthenticate.Checked = MST_Global.ServerAccessPreauthenticate;
                chkServerAccessAllowRedirection.Checked = MST_Global.ServerAccessAllowRedirection;
                chkServerAccessAllowDecompression.Checked = MST_Global.ServerAccessAllowDecompression;

                switch (MST_Global.ServerAccessCertificateTrust)
                {
                    case MST_Global.ServerAccessCertificatesTrustType.TrustAll:
                        rbServerAddressHTTPSCertTrustAll.Checked = true;
                        break;
                    case MST_Global.ServerAccessCertificatesTrustType.TrustQuery:
                        rbServerAddressHTTPSCertQueryTrust.Checked = true;
                        break;
                    case MST_Global.ServerAccessCertificatesTrustType.OnlyInstalled:
                    default:
                        rbServerAddressHTTPSCertOnlyInstalled.Checked = true;
                        break;
                }

                cfgTerminalDataGridScrollUp.Items.Add(Keys.Up);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.Down);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.Left);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.Right);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D0);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D1);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D2);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D3);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D4);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D5);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D6);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D7);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D8);
                cfgTerminalDataGridScrollUp.Items.Add(Keys.D9);

                cfgTerminalDataGridScrollDown.Items.Add(Keys.Up);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.Down);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.Left);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.Right);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D0);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D1);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D2);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D3);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D4);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D5);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D6);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D7);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D8);
                cfgTerminalDataGridScrollDown.Items.Add(Keys.D9);

                cfgTerminalDataGridScrollUp.SelectedItem = MST_Global.DataGridScrollUp;
                cfgTerminalDataGridScrollDown.SelectedItem = MST_Global.DataGridScrollDown;

                //povolene
                cfgProdejAllow.Checked = MST_Global.Prodej;
                cfgVydejAllowed.Checked = MST_Global.Vydej;
                cfgPrijemAllowed.Checked = MST_Global.Prijem;
                cfgInventuraAllowed.Checked = MST_Global.Inventura1;
                cfgInventura2Allowed.Checked = MST_Global.Inventura2;

                //nazvy modulov
                cfgInventuraName.Text = MST_Global.Inventura1Name;
                cfgInventura2Name.Text = MST_Global.Inventura2Name;
                cfgPrijemName.Text = MST_Global.PrijemName;
                cfgProdejName.Text = MST_Global.ProdejName;
                cfgVydejName.Text = MST_Global.VydejName;

                ScannerTypeName.Text = MST_Global.ScannerTypeName;

                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.None);
                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.Serial);
                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.Unitech_HT660);
                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.Unitech_PA600);
                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.Symbol_PT8800);
                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.Symbol_MC3000);
                //cfgScannerDevice.Items.Add(Scanner.ScannerTypes.Netcom_TT8000);
                //cfgScannerDevice.SelectedItem = MST_Global.ScannerType;

                cmbRFIDScannerDevice.Items.Add(Scanner.ScannerRFIDTypes.None);
                //cmbRFIDScannerDevice.Items.Add(Scanner.ScannerRFIDTypes.TT8000); //25.10.2016 JiS odstraneno nepouziva se..
                cmbRFIDScannerDevice.Items.Add(Scanner.ScannerRFIDTypes.MC9090);
                cmbRFIDScannerDevice.Items.Add(Scanner.ScannerRFIDTypes.MC319Z);
                cmbRFIDScannerDevice.Items.Add(Scanner.ScannerRFIDTypes.MC319Z_v2);
                cmbRFIDScannerDevice.SelectedItem = MST_Global.RFIDScannerType;

//OnScanner sound

                cbOnScannerInvetura1.Checked = MST_Global.OnScannerSound_Inventura1_sqlc;
                cbOnScannerInventura2.Checked = MST_Global.OnScannerSound_Inventura2;
                cbOnScannerExpedice.Checked = MST_Global.OnScannerSound_Expedice;
                cbOnScannerOnline.Checked = MST_Global.OnScannerSound_Online;
                cbOnScannerPrijem.Checked = MST_Global.OnScannerSound_Prijem_4;
                cbOnScannerProdej.Checked = MST_Global.OnScannerSound_Prodej_3;
                cbOnScannerVydej.Checked = MST_Global.OnScannerSound_Vydej_3;
                cbOnScannerServis.Checked = MST_Global.OnScannerSound_ServisModul;


                this.cfgcheckBoxRFID_MemoryEPC.Checked = MST_Global.RFID_memoryEPC;
                this.cfgcheckBoxRFID_MemoryRESERVED.Checked = MST_Global.RFID_memoryRESERVED;
                this.cfgcheckBoxRFID_MemoryTID.Checked = MST_Global.RFID_memoryTID;
                this.cfgcheckBoxRFID_MemoryUSER.Checked = MST_Global.RFID_memoryUSER;

                //RFID skener povolovani 
                //cfgcheckBoxRFIDActiv
                if (MST_Global.RFIDPovolitUHF == true)
                {

                    this.cfgcheckBoxRFIDActivUHF.Checked = MST_Global.RFIDPovolitUHF;
                    this.radioButtonRFIDUHFText.Checked = MST_Global.RFIDUkladatNacitatText;
                    this.radioButtonRFIDUHFHexa.Checked = !MST_Global.RFIDUkladatNacitatText;


                }
                else
                {
                    this.povolitRFIDUHFMenu(false);
                }


                if (Settings.RemovedRFIDCodes != null)
                {
                    for (int i = 0; i < Settings.RemovedRFIDCodes.Count; i++)
                    {
                        list_removedRFID.Items.Add(Settings.RemovedRFIDCodes[i]);
                    }
                }

				Kongif_Load_Servis();
				Kongif_Load_Events();
				Kongif_Load_Expedice();
				Kongif_Load_Ukoly();
				Kongif_Load_Inventura();
				Kongif_Load_Inventura2();
				Kongif_Load_Vydej();
				Kongif_Load_Prodej();
				Kongif_Load_Prijem();


                chckPovolitAktualizaciHesel.Checked = MST_Global.PovolitAktualizacePristupu;

                try
                {
                    ScannerStart();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

                try
                {
                    labelPlatformInfo.Text = "Platform type:\n" + PlatformDetection.PInvoke.GetPlatformType();
                    labelPlatformInfo.Text += "\n\n";
                    labelPlatformInfo.Text += "Oem info:\n" + PlatformDetection.PInvoke.GetOemInfo();
                }
                catch { }

                nuUIGridFont.Value = Settings.UIGridFont;
                lblCurrentCulture.Text = System.Globalization.CultureInfo.CurrentCulture.ToString();
                lblCurrentUICulture.Text = System.Globalization.CultureInfo.CurrentUICulture.ToString();
                chk_UIHideTaskBar.Checked = Settings.UIHideTaskBar;
                chk_UIHideWindowText.Checked = Settings.UIHideWindowText;
                chk_UIMultistartTest.Checked = Settings.UIMultistartTest;
                txt_UIFormatDesCisel.Text = Settings.UIFormatDesCisel;
                txt_UINumberExample_TextChanged(null, null);

				#region UI automatika

				chb_uia_enable.Checked = Settings.uia_enable;

				rb_uia_Expedice.Checked = Settings.uia_Expedice;
				rb_uia_inventura1.Checked = Settings.uia_inventura1;
				rb_uia_inventura2.Checked = Settings.uia_inventura2;
				rb_uia_prijem.Checked = Settings.uia_prijem;
				rb_uia_prodej.Checked = Settings.uia_prodej;
				rb_uia_Servis.Checked = Settings.uia_Servis;
				rb_uia_udalosti.Checked = Settings.uia_udalosti;
				rb_uia_ukoly.Checked = Settings.uia_ukoly;
				rb_uia_vydej.Checked = Settings.uia_vydej;

				chb_uia_prodej_VybratJeden.Checked = Settings.uia_prodej_VybratJeden;
				chb_uia_prodej_novaDavka.Checked = Settings.uia_prodej_novaDavka;
				chb_uia_prodej_VybratTD.Checked = Settings.uia_prodej_VybratTD;
				tb_uia_prodej_docid2TD.Text = Settings.uia_prodej_docid2TD;
				tb_uia_prodej_docidTD.Text = Settings.uia_prodej_docidTD;



				#endregion


                SystemTimeUpdate.Checked = Settings.SystemTimeUpdate;

                //Online funkce 
                chk_Online_BYZNYS_Povolit.Checked = Settings.Online_BYZNYS;
                txt_Online_BYZNYS_ConnectionString.Text = Settings.Online_BYZNYS_ConnectionString;
                txt_Online_BYZNYS_CommandTimeout.Text = Settings.Online_BYZNYS_CommandTimeout.ToString();
                chk_Online_BYZNYS_VyberPartneraKlicDefault.Checked = Settings.Online_BYZNYS_VyberPartneraKlicDefault_Povolit;
                txt_Online_BYZNYS_VyberPartneraKlicDefault.Text = Settings.Online_BYZNYS_VyberPartneraKlicDefault.ToString();

                //Parsovani kodu
                parsing_Enable.Checked = Settings.Parsing_Enabled;
                parsing_WeightCode_12.Checked = Settings.Parsing_WeightCode_12;
                parsing_WeightCode.Checked = Settings.Parsing_WeightCode;

                parsing_SABNeznamyKod.Checked = Settings.Parsing_SABNeznamyKod;
				Parsing_SAB_AustralianNorm.Checked = Settings.Parsing_SAB_AustralianNorm;
				Parsing_SAB_GS1_BALTON.Checked = Settings.Parsing_SAB_GS1_BALTON;
				Parsing_SAB_GS1_Zavorky.Checked = Settings.Parsing_SAB_GS1_Zavorky;
				Parsing_SAB_GS1_BELDICO.Checked = Settings.Parsing_SAB_GS1_BELDICO;

                parsing_BarcodeSlashSarze.Checked = Settings.Parsing_BarcodeSlashSarze;
                parsing_FenixBarcodeObal.Checked = Settings.Parsing_FenixBarcodeObal;
                parsing_HIBC.Checked = Settings.Parsing_HIBC;
                parsing_GS1.Checked = Settings.Parsing_GS1;

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }



        private List<string> loadPrinterTypes()
        {
            List<string> printerTypes= new List<string>();
            string FilePath;
            string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            FilePath = (new Uri(Path.Combine(AssemblyDirectoryPath, "PrinterFactory.xml"))).LocalPath;

            XmlDocument xmldoc = null;
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(FilePath);

                //Tiskarny
                XmlNodeList printerNodes = xmldoc.SelectNodes(@"/Settings/Printers/Printer");
                if (printerNodes.Count > 0)
                {
                    foreach (XmlElement node in printerNodes)
                    {
                        printerTypes.Add(node.Attributes["type"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "loadPrinterTypes()");
                throw ex;
            }
            finally
            {
                
            }
            return printerTypes;
        }



        private void cfgPrintServerPrinterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sender je ten, kdo se meni ... 
            ComboBox comboPrinterType = sender as ComboBox;
            if (comboPrinterType == null)
                return;

            if (comboPrinterType == cfgPrintServerPrinterTypeInventuraNasnimane)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameInventuraNasnimane);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeInventuraPredloha)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameInventuraPredloha);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypePrijemNasnimane)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNamePrijemNasnimane);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypePrijemPredloha)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNamePrijemPredloha);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeProdejSoupisList)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejSoupisListHlavicka);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeProdejSoupisList)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejSoupisListPaticka);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeProdejSoupisList)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejSoupisListRadek);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeProdejPaletyList)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejPaletyListHlavicka);
            }

            if (comboPrinterType == cfgPrintServerPrinterTypeProdejPaletyList)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejPaletyListPaticka);
            }

            if (comboPrinterType == cfgPrintServerPrinterTypeProdejPaletyList)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejPaletyListRadek);
            }

            if (comboPrinterType == cfgPrintServerPrinterTypeProdejNasnimane)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejNasnimane);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeProdejPredloha)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameProdejPredloha);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeVydejNasnimane)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameVydejNasnimane);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeVydejPalListek)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameVydejPalListek);
            }
            
            if (comboPrinterType == cfgPrintServerPrinterTypeVydejPredloha)
            {
                printerTempatesFill((string)comboPrinterType.SelectedItem, cfgPrintServerTemplateNameVydejPredloha);
            }

        }

        // Zmena vybrane tiskarny ... 
        void comboBoxPrinterSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                panelPrinterConfigControl.Controls.Clear();
                // ziska userconfig control tiskarny a zobrazi v panelu ... 
                UserControl ucConfig = PrinterFactory.PrinterFactory.Instance.GetConfigControl((string)comboBoxPrinterSelection.SelectedItem) as UserControl;
                if (ucConfig != null)
                {
                    panelPrinterConfigControl.Controls.Add(ucConfig);
                    ucConfig.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                MST_Global.SqlCe = cbSqlCe.Checked;
                MST_Global.TerminalID = Convert.ToByte(cfgTerminalID.Value);
                Settings.AdminPwd = textBoxTerminalAdminPwd.Text;
                MST_Global.Adresa_API = cfgTerminalServerAddress.Text;

				MST_Global.API_konstant = cfgAPIKonstanta.Text;
				MST_Global.Autorizace_API = cfgAutorizaceAPI.Text;
				MST_Global.isHTTPS = chb_ishttps.Checked;

                try { MST_Global.ServiceTimeOut = int.Parse(cfgTerminalServiceTimeout.Text); }
                catch { }
                //MST_Global.ScannerType = (Scanner.ScannerTypes)cfgScannerDevice.SelectedItem;
                MST_Global.ShowPanelButtons = cbShowButtonsPanel.Checked;
                MST_Global.Storage = cfgTerminalStorageFolder.Text;
                
                // lokalizace
                MST_Global.LokalizacePovolit = cbLokalizacePovolit.Checked;
                MST_Global.LokalizaceVlastniPovolit = cbLokalizaceVlastniPovolit.Checked;
                //cbLokalizaceZvolena
                //MST_Global.LokalizaceZvolena =  ((Fask.MST_W.Localization.LocalizationSupport.LocalizationItem)cbLokalizaceZvolena.SelectedItem).Value.ToString();
                //try
                //{
                //    MST_Global.LokalizaceZvolena = (Fask.MST_W.Localization.LocalizationSupport.LocalType)Enum.Parse(typeof(Fask.MST_W.Localization.LocalizationSupport.LocalType), cbLokalizaceZvolena.SelectedItem.ToString(), true);
                //}
                //catch
                //{
                //    MST_Global.LokalizaceZvolena = Fask.MST_W.Localization.LocalizationSupport.LocalType.cs;
                //}
                try
                {
                    MST_Global.LokalizaceZvolena = (Fask.Localization.LocalizationSupport.LocalType)Enum.Parse(typeof(Fask.Localization.LocalizationSupport.LocalType), cbLokalizaceZvolena.SelectedItem.ToString(), true);
                }
                catch
                {
                    MST_Global.LokalizaceZvolena = Fask.Localization.LocalizationSupport.LocalType.cs;
                }

                //PrintServer
                /*
                MST_Global.PrintServerTemplateNamePrijemPredloha = cfgPrintServerTemplateNamePrijemPredloha.Text;
                MST_Global.PrintServerTemplateNamePrijemNasnimane = cfgPrintServerTemplateNamePrijemNasnimane.Text;
                MST_Global.PrintServerTemplateNameVydejPredloha = cfgPrintServerTemplateNameVydejPredloha.Text;
                MST_Global.PrintServerTemplateNameVydejNasnimane = cfgPrintServerTemplateNameVydejNasnimane.Text;
                MST_Global.PrintServerTemplateNameVydejPalListek = cfgPrintServerTemplateNameVydejPalListek.Text;
                MST_Global.PrintServerTemplateNameProdejPredloha = cfgPrintServerTemplateNameProdejPredloha.Text;
                MST_Global.PrintServerTemplateNameProdejNasnimane = cfgPrintServerTemplateNameProdejNasnimane.Text;
                MST_Global.PrintServerTemplateNameInventuraPredloha = cfgPrintServerTemplateNameInventuraPredloha.Text;
                MST_Global.PrintServerTemplateNameInventuraNasnimane = cfgPrintServerTemplateNameInventuraNasnimane.Text;
                */

				Konfig_Save_Print();


                MST_Global.DataGridScrollDown = (Keys)cfgTerminalDataGridScrollDown.SelectedItem;
                MST_Global.DataGridScrollUp = (Keys)cfgTerminalDataGridScrollUp.SelectedItem;

                if (rbSeverAccessUserNamePassword.Checked)
                    MST_Global.ServerAccess = MST_Global.ServerAccessType.Credentials;
                else if (rbSeverAccessAnonymous.Checked)
                    MST_Global.ServerAccess = MST_Global.ServerAccessType.Anonymous;

                MST_Global.ServerAccessUsername = txtServerAccessUsername.Text.Trim();
                MST_Global.ServerAccessPassword = txtServerAccessPassword.Text.Trim();
                MST_Global.ServerAccessDomain = txtServerAccessDomain.Text.Trim();
                MST_Global.ServerAccessPreauthenticate = chkServerAccessPreauthenticate.Checked;
                MST_Global.ServerAccessAllowRedirection = chkServerAccessAllowRedirection.Checked;
                MST_Global.ServerAccessAllowDecompression = chkServerAccessAllowDecompression.Checked;

                if (rbServerAddressHTTPSCertOnlyInstalled.Checked)
                    MST_Global.ServerAccessCertificateTrust = MST_Global.ServerAccessCertificatesTrustType.OnlyInstalled;
                else if (rbServerAddressHTTPSCertTrustAll.Checked)
                    MST_Global.ServerAccessCertificateTrust = MST_Global.ServerAccessCertificatesTrustType.TrustAll;
                else if (rbServerAddressHTTPSCertQueryTrust.Checked)
                    MST_Global.ServerAccessCertificateTrust = MST_Global.ServerAccessCertificatesTrustType.TrustQuery;

                MST_Global.REZ1_PRIJ_NAME = cfg_REZ1_PRIJ_NAME.Text;
                MST_Global.REZ2_PRIJ_NAME = cfg_REZ2_PRIJ_NAME.Text;

                MST_Global.REZ1_VYDE_NAME = cfg_REZ1_VYDE_NAME.Text;
                MST_Global.REZ2_VYDE_NAME = cfg_REZ2_VYDE_NAME.Text;

                // RFID ================================================
                try { MST_Global.RFIDScannerType = (Scanner.ScannerRFIDTypes)cmbRFIDScannerDevice.SelectedItem; }
                catch { MST_Global.RFIDScannerType = Fask.MST_W.Scanner.ScannerRFIDTypes.None; }
                MST_Global.RFIDPovolitUHF = cfgcheckBoxRFIDActivUHF.Checked;
                MST_Global.RFIDUkladatNacitatText = this.radioButtonRFIDUHFText.Checked;

                MST_Global.RFID_memoryEPC = cfgcheckBoxRFID_MemoryEPC.Checked;
                MST_Global.RFID_memoryRESERVED = cfgcheckBoxRFID_MemoryRESERVED.Checked;
                MST_Global.RFID_memoryTID = cfgcheckBoxRFID_MemoryTID.Checked;
                MST_Global.RFID_memoryUSER = cfgcheckBoxRFID_MemoryUSER.Checked;


                try { MST_Global.RFIDPowerLevel = (int)this.comboBoxPowerUHF.SelectedItem; }
                catch { MST_Global.RFIDPowerLevel = 0; }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(MST_W.Main.ConfigRFCodesRemoved))
                {
                    if (Settings.RemovedRFIDCodes != null)
                    {
                        foreach (string line in Settings.RemovedRFIDCodes)
                        {
                            file.WriteLine(line);
                        }
                    }
                }

                // Tracer ================================================
                MST_Global.LogUploadInterval = int.Parse(cfgLogUploadInterval.Text);
                Fask.Logging.Trace2.Separator = tb_trace_separator.Text;

                // Pamet =================================================
                MST_Global.MemorySize = tb_VelkostPamete.Text;
                MST_Global.MemoryChecked = cbKontrolaPamete.Checked;

                // Prijem ================================================
                if (rbPrijemModelCodebook.Checked)
                    MST_Global.PrijemModel = MST_Global.MODEL_CODEBOOK;
                else if (rbPrijemModelMemory.Checked)
                    MST_Global.PrijemModel = MST_Global.MODEL_MEMORY;

                MST_Global.PovolitAktualizacePristupu = chckPovolitAktualizaciHesel.Checked;

                if (!MST_Global.Save(Main.ConfigTerminalFileName))
                    throw new Exception("Nepodaøilo se uložit globální konfiguraci terminálu");


                MST_Global.Prodej = cfgProdejAllow.Checked;
                MST_Global.ProdejName = cfgProdejName.Text;
                MST_Global.Prijem = cfgPrijemAllowed.Checked;
                MST_Global.PrijemName = cfgPrijemName.Text;
                MST_Global.Vydej = cfgVydejAllowed.Checked;
                MST_Global.vydejPovolitPreplneniPolozky = cfg_povolitPreplneniPolozky.Checked;
                MST_Global.vydejZadaniLocncodePredSN = cfg_VydejZadaniLokacePredSN.Checked;
                MST_Global.VydejName = cfgVydejName.Text;
                MST_Global.Inventura1 = cfgInventuraAllowed.Checked;
                MST_Global.Inventura1Name = cfgInventuraName.Text;
                MST_Global.Inventura2 = cfgInventura2Allowed.Checked;
                MST_Global.Inventura2Name = cfgInventura2Name.Text;

				Konfig_Save_Servis();
				Konfig_Save_MST_Global();
				Konfig_Save_Prodej();
				Konfig_Save_Prijem();
				Konfig_Save_Expedice();

                Settings.UIGridFont = Convert.ToInt32(nuUIGridFont.Value);
                Settings.UIHideTaskBar = chk_UIHideTaskBar.Checked;
                Settings.UIHideWindowText = chk_UIHideWindowText.Checked;
                Settings.UIMultistartTest = chk_UIMultistartTest.Checked;
                Settings.UIFormatDesCisel = txt_UIFormatDesCisel.Text; //zde bez trimming, pokud je tam nejake specialni formatovani ...
                Settings.SystemTimeUpdate = SystemTimeUpdate.Checked;

				#region UI automatika

				Settings.uia_enable = chb_uia_enable.Checked;

				Settings.uia_Expedice = rb_uia_Expedice.Checked;
				Settings.uia_inventura1 = rb_uia_inventura1.Checked;
				Settings.uia_inventura2 = rb_uia_inventura2.Checked;
				Settings.uia_prijem = rb_uia_prijem.Checked;
				Settings.uia_prodej = rb_uia_prodej.Checked;
				Settings.uia_Servis = rb_uia_Servis.Checked;
				Settings.uia_udalosti = rb_uia_udalosti.Checked;
				Settings.uia_ukoly = rb_uia_ukoly.Checked;
				Settings.uia_vydej = rb_uia_vydej.Checked;

				Settings.uia_prodej_novaDavka = chb_uia_prodej_novaDavka.Checked;
				Settings.uia_prodej_VybratJeden = chb_uia_prodej_VybratJeden.Checked;
				Settings.uia_prodej_VybratTD = chb_uia_prodej_VybratTD.Checked;
				Settings.uia_prodej_docid2TD = tb_uia_prodej_docid2TD.Text;
				Settings.uia_prodej_docidTD = tb_uia_prodej_docidTD.Text;

				#endregion

                //Online funkce 
                Settings.Online_BYZNYS = chk_Online_BYZNYS_Povolit.Checked;
                Settings.Online_BYZNYS_ConnectionString = txt_Online_BYZNYS_ConnectionString.Text;
                try { Settings.Online_BYZNYS_CommandTimeout = Convert.ToInt32(txt_Online_BYZNYS_CommandTimeout.Text); }
                catch { }
                Settings.Online_BYZNYS_VyberPartneraKlicDefault_Povolit = chk_Online_BYZNYS_VyberPartneraKlicDefault.Checked;
                try { Settings.Online_BYZNYS_VyberPartneraKlicDefault = Convert.ToInt32(txt_Online_BYZNYS_VyberPartneraKlicDefault.Text); }
                catch { }

                //Parsovani kodu
                Settings.Parsing_Enabled = parsing_Enable.Checked;
                Settings.Parsing_WeightCode_12 = parsing_WeightCode_12.Checked;
                Settings.Parsing_WeightCode = parsing_WeightCode.Checked;

                Settings.Parsing_SABNeznamyKod = parsing_SABNeznamyKod.Checked;
				Settings.Parsing_SAB_AustralianNorm = Parsing_SAB_AustralianNorm.Checked;
				Settings.Parsing_SAB_GS1_BALTON = Parsing_SAB_GS1_BALTON.Checked ;
				Settings.Parsing_SAB_GS1_Zavorky = Parsing_SAB_GS1_Zavorky.Checked;
				Settings.Parsing_SAB_GS1_BELDICO = Parsing_SAB_GS1_BELDICO.Checked;

                Settings.Parsing_BarcodeSlashSarze = parsing_BarcodeSlashSarze.Checked;
                Settings.Parsing_FenixBarcodeObal = parsing_FenixBarcodeObal.Checked;
                Settings.Parsing_HIBC = parsing_HIBC.Checked;
                Settings.Parsing_GS1 = parsing_GS1.Checked;

                finalize();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void finalize()
        {
            ScannerFinalize();
            RFIDScannerStop();
            PrinterConfigurationsDispose();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            if (MessageBoxBig.Show("Konfigurace nebude uložena!\n\nUkonèit konfiguraci?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
                == DialogResult.No)
                return;

            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void cfgProdejPrefix_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.BackColor = SystemColors.Window;
            try
            {
                int prefix = Convert.ToInt32(tbox.Text);
            }
            catch
            {
                tbox.BackColor = Color.MistyRose;
            }
        }

        private void cfgProdejCenaVystupPovolit_CheckStateChanged(object sender, EventArgs e)
        {
            cfgProdejCenaVystup.Enabled = cfgProdejCenaVystupPovolit.Checked;
        }

        private void cfgProdejRangeEnable_CheckStateChanged(object sender, EventArgs e)
        {
            cfgProdejPrefix.Enabled = cfgProdejRangeEnable.Checked;
            cfgProdejNextNumber.Enabled = cfgProdejRangeEnable.Checked;
        }

        private void cfgCykly_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cfgTypyZavozu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonConfigurationPasswords_Click(object sender, EventArgs e)
        {
            _WebRefernces_Globals.LoginServiceSession loginService = new Fask.MST_W._WebRefernces_Globals.LoginServiceSession();
            loginService.Timeout = MST_Global.ServiceTimeOut;
            loginService.Url = MST_Global.ServerAddress + "LoginService.asmx";
            loginService.UpdateWebServiceCredentials();

            try
            {
                Program.mstw.mbw.BeginPracujiForm("Aktualizace pøístupù");

                LoginService.StatusObject so = loginService.GetKatalogUzivatele(MST_Global.TerminalID);
                if (so.Exception)
                    throw new Exception("Aktualizace pøístupù:\n" + so.StatusText);

                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogUzivatelu(loginService);

                Program.mstw.mbw.EndPracujiForm();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            MessageBoxBig.Show("Aktualizace pøístupù úspìšnì dokonèena", Color.DarkGreen);
        }

        private void cbLog_CheckStateChanged(object sender, EventArgs e)
        {
            Fask.Logging.Log.Enable = cbLog.Checked;
        }

        private void cbTrace_CheckStateChanged(object sender, EventArgs e)
        {
            Fask.Logging.Trace2.Enable = cbTrace.Checked;
        }

        private void bScannerParameters_Click(object sender, EventArgs e)
        {
            if (Program.mstw.Scanner != null)
                Program.mstw.Scanner.ScannerSetting();
        }

        private void bScannerBarcodes_Click(object sender, EventArgs e)
        {
            if (Program.mstw.Scanner != null)
                Program.mstw.Scanner.BarcodeSetting();
        }

        private void buttonTypyPalet_Click(object sender, EventArgs e)
        {
            _WebRefernces_Globals.TypyPalet.Actualize_TypyPalet();
        }

        private void buttonScannerGet_Click(object sender, EventArgs e)
        {
            ConfigurationService.Configuration configurations = new Fask.MST_W.ConfigurationService.Configuration();
            configurations.Timeout = MST_Global.ServiceTimeOut;
            configurations.Url = MST_Global.ServerAddress + "Configuration.asmx";
            configurations.UpdateWebServiceCredentials();

            string scannerconfig = null;

            try
            {
                Program.mstw.mbw.BeginPracujiForm("Aktualizace nastavení scanneru");
                scannerconfig = configurations.GetScannerConfig();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            if (scannerconfig != null)
            {
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(MST_W.Main.ConfigScanner, false);
                    sw.Write(scannerconfig);
                    sw.Close();
                    sw = null;
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw = null;
                    }
                }
            }

            Program.mstw.mbw.EndPracujiForm();
            MessageBoxBig.Show("Aktualizace nastavení scanneru úspìšnì dokonèena\n\nAplikaci je nutné restartovat!", Color.DarkGreen);
        }

        private void buttonScannerUpdateToServer_Click(object sender, EventArgs e)
        {
            Program.mstw.mbw.BeginPracujiForm("Nahrání nastavení scanneru na server");

            ConfigurationService.Configuration configurations = new Fask.MST_W.ConfigurationService.Configuration();
            configurations.Timeout = MST_Global.ServiceTimeOut;
            configurations.Url = MST_Global.ServerAddress + "Configuration.asmx";
            configurations.UpdateWebServiceCredentials();

            StreamReader sr = null;
            string scannerconfig = null;
            try
            {
                sr = new StreamReader(Main.ConfigScanner);
                scannerconfig = sr.ReadToEnd();
                sr.Close();
                sr = null;
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex.Message, "Config:ScannerUpdateToServer");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                return;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }

            try
            {
                configurations.SetScannerConfig(scannerconfig);
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex.Message, "Config:ScannerUpdateToServer");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                return;
            }

            Program.mstw.mbw.EndPracujiForm();
            MessageBoxBig.Show("Nahrání nastavení scanneru na server úspìšnì dokonèeno.", Color.DarkGreen);
        }

        private void cfgProdejTypDokladu_CheckStateChanged(object sender, EventArgs e)
        {
            cfgProdejOdberatele.Enabled = cfgProdejStrediska.Enabled = cfgProdejDisponibilityCheck.Enabled =
                !cfgProdejTypDokladu.Checked;
        }

        private void tabPageTerminal_Click(object sender, EventArgs e)
        {

        }

        private void buttonDeleteLog_Click(object sender, EventArgs e)
        {
            if (MessageBoxBig.Show("Smazat logovací soubor?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                Logging.Log.Delete();
            }
        }

        private void formConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonStorno_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void PrinterConfigurationsDispose()
        {
            //if (PrinterFactory.PrinterFactory.Instance == null)
            //    return;
            // printerTypes.AddRange(new string[] {"None", "WebService", "WebService2", "Bluetooth"});
            //printerTypes.AddRange(loadPrinterTypes().ToArray());
            UserControl i = null;
            foreach (var item in loadPrinterTypes())
            {
                i = PrinterFactory.PrinterFactory.Instance.GetConfigControl(item);
                if (i != null)
                    i.Dispose();   
            }
            //UserControl i = null;
            //i = PrinterFactory.PrinterFactory.Instance.GetConfigControl("None");
            //if (i != null)
            //    i.Dispose();
            //i = PrinterFactory.PrinterFactory.Instance.GetConfigControl("WebService");
            //if (i != null)
            //    i.Dispose();
            //i = PrinterFactory.PrinterFactory.Instance.GetConfigControl("WebService2");
            //if (i != null)
            //    i.Dispose();
            //i = PrinterFactory.PrinterFactory.Instance.GetConfigControl("Bluetooth");
            //if (i != null)
            //    i.Dispose();
        }

        private void rbSeverAccess_CheckedChanged(object sender, EventArgs e)
        {
            txtServerAccessPassword.Enabled = rbSeverAccessUserNamePassword.Checked;
            txtServerAccessUsername.Enabled = rbSeverAccessUserNamePassword.Checked;
            txtServerAccessDomain.Enabled = rbSeverAccessUserNamePassword.Checked;
            chkServerAccessPreauthenticate.Enabled = rbSeverAccessUserNamePassword.Checked;
        }

        private void nuUIGridFont_ValueChanged(object sender, EventArgs e)
        {
            labelUIFontTest.Font = new Font(labelUIFontTest.Font.Name, Convert.ToSingle(nuUIGridFont.Value), FontStyle.Regular);
        }

        private void cfgTerminalDataGridScrollUp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        decimal descisloexample = 10470393.84344M;
        private void txt_UIFormatDesCisel_TextChanged(object sender, EventArgs e)
        {
            lbl_UIFormatDesCiselExample.BackColor = this.BackColor;
            txt_UIFormatDesCisel.BackColor = Color.White;
            try
            {
                //lbl_UIFormatDesCiselExample.Text = descisloexample.ToString(txt_UIFormatDesCisel.Text, System.Globalization.NumberFormatInfo.CurrentInfo);
                lbl_UIFormatDesCiselExample.Text = descisloexample.ToString(txt_UIFormatDesCisel.Text);
            }
            catch
            {
                txt_UIFormatDesCisel.BackColor = Color.MistyRose;
                lbl_UIFormatDesCiselExample.BackColor = Color.MistyRose;
            }
        }

        private void txt_UINumberExample_TextChanged(object sender, EventArgs e)
        {
            try
            {
                descisloexample = decimal.Parse(txt_UINumberExample.Text);
                txt_UIFormatDesCisel_TextChanged(null, null);
            }
            catch
            {
            }
        }

        private void cfgVydejItemTypeQuestion_CheckStateChanged(object sender, EventArgs e)
        {
            cfgVydejPokracovatNaJinemTerminalu.Enabled = !cfgVydejItemTypeQuestion.Checked;
        }

        private void cfgProdejStrediskoKPolozce_CheckStateChanged(object sender, EventArgs e)
        {
            cfgProdejStrediskoJednoNaDavku.Enabled = !cfgProdejStrediskoKPolozce.Checked;
        }

        private void buttonSystemTimeTest_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.SystemTimeSynchronization timesync = new Classes.SystemTimeSynchronization();
                DateTime dtServer = timesync.GetDateTimeFromServer();
                DateTime dtTerminal = DateTime.Now;
                TimeSpan tsRozdil = dtServer - dtTerminal;
                double rozdil = Math.Abs(tsRozdil.TotalMinutes);
                Cursor.Current = Cursors.Default;
                MessageBox.Show(
                    "Èas serveru  : " + dtServer.ToString("g") + "\n" +
                    "Èas terminálu: " + dtTerminal.ToString("g") + "\n" +
                    "Rozdíl minut : " + rozdil.ToString("0"), this.Text, MessageBoxButtons.OK, rozdil > 1 ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonSystemTimeSynchronize_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.SystemTimeSynchronization timesync = new Classes.SystemTimeSynchronization();
                if (timesync.Synchronize())
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Nastaven nový systémový èas: " + DateTime.Now.ToString("g"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Synchronizace èasu se nezdaøila", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region RFID UHF

        private Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2 rfidScannerInstance = null;


        /// <summary>
        /// Zapnuti rfid ctecky a cteni dat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRFIDONOFF_Click(object sender, EventArgs e)
        {
            if (rfidactive)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.RFIDScannerStop();
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    Logging.Log.Write(ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                //if ((Program.mstw.RFIDUHFScanner != null) && (Program.mstw.RFIDUHFScanner is Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2))
                //{
                //    this.rfidScannerInstance = Program.mstw.RFIDUHFScanner as Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2;
                //    this.rfidScannerInstance.RFIDScannerStarted -= new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStartedHandler(rfidScannerInstance_RFIDScannerStarted);
                //    this.rfidScannerInstance.RFIDScannerStopped -= new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStoppedHandler(rfidScannerInstance_RFIDScannerStopped);
                //    this.rfidScannerInstance.RFIDScannerStarted += new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStartedHandler(rfidScannerInstance_RFIDScannerStarted);
                //    this.rfidScannerInstance.RFIDScannerStopped += new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStoppedHandler(rfidScannerInstance_RFIDScannerStopped);
                //    this.rfidScannerInstance.TriggerEnabled = true;
                //}

                //try
                //{
                //    if (Program.mstw.RFIDUHFScanner != null)
                //    {
                //        Program.mstw.RFIDUHFScanner.RFIDTagEvent -= new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                //        Program.mstw.RFIDUHFScanner.RFIDTagEvent += new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Logging.Log.Write(ex);
                //}

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.RFIDScannerStart();
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    Logging.Log.Write(ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }

            buttonRFIDONOFF.Text = rfidactive ? "Vypnout test" : "Zapnout test";
        }

        void rfidScannerInstance_RFIDScannerStarted()
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                this.rfidactive = true;
                //UpdateUI();
            });
        }

        void rfidScannerInstance_RFIDScannerStopped()
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                this.rfidactive = false;
                //UpdateUI();
            });
        }

        //==================
        private bool rfidactive = false;
        /// <summary>
        /// Aktivace RFID skeneru
        /// </summary>
        private void RFIDScannerStart()
        {
            try
            {
                if (Program.mstw.RFIDUHFScanner != null)
                {
                    //Program.mstw.RFIDUHFScanner.DataReady += new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                    Program.mstw.RFIDUHFScanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                    Program.mstw.RFIDUHFScanner.DataReady += new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                    
                    //Program.mstw.RFIDUHFScanner.RFIDTagEvent -= new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                    //Program.mstw.RFIDUHFScanner.RFIDTagEvent += new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                    Program.mstw.RFIDUHFScanner.StartScan();
                    rfidactive = true;
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }
        /// <summary>
        /// Deaktivace RFID skeneru
        /// </summary>
        private void RFIDScannerStop()
        {
            try
            {
                if (Program.mstw.RFIDUHFScanner != null)
                {
                    Program.mstw.RFIDUHFScanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                    Program.mstw.RFIDUHFScanner.StopScan();
                    rfidactive = false;
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        void RFIDScanner_DataReady(object sender, Fask.MST_W.Scanner.RFIDTagDataEventArgs e)
        {
            RFIDProcessDataDelegate rfiddel = new RFIDProcessDataDelegate(RFIDProcessData);
            this.BeginInvoke(rfiddel, new object[] { e });
        }

        /// <summary>
        /// Pridani dat do texboxu
        /// </summary>
        /// <param name="sender">co to poslalo</param>
        /// <param name="e">obsahuje seznam polozek</param>
        void RFIDScanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerRFIDEventArgs e)
        {
            this.BeginInvoke(
                (MethodInvoker)delegate()
                {
                    textBoxRFIDScannerData.Text = "";
                    foreach (Fask.MST_W.Scanner.RFIDBarcodeData data in e.BarcodeData)
                    {
                        textBoxRFIDScannerData.Text += DateTime.Now.ToString() + "\r\n" + data.BarcodeData + "\r\n";

                        if (chck_RFIDNew.Checked)
                        {
                            if (Settings.RemovedRFIDCodes == null)
                                Settings.RemovedRFIDCodes = new List<string>();

                            if (!Settings.RemovedRFIDCodes.Contains(data.BarcodeData))
                            { // nebyl RFID kod nacten jeste
                                Settings.RemovedRFIDCodes.Add(data.BarcodeData);
                                list_removedRFID.Items.Add(data.BarcodeData);
                            }
                        }
                    }
                }
            );
        }

        #region Snimat Data RFID

        private System.Collections.Generic.List<Fask.MST_W.Scanner.RFIDTagData> nasnimaneKody = new List<Fask.MST_W.Scanner.RFIDTagData>();

        private Fask.SQLiteDBs.DataSets.Obecne dsObecne = new Fask.SQLiteDBs.DataSets.Obecne();


        private delegate void RFIDProcessDataDelegate(Fask.MST_W.Scanner.RFIDTagDataEventArgs e);
        void RFIDProcessData(Fask.MST_W.Scanner.RFIDTagDataEventArgs e)
        {
            try
            {
                nasnimaneKody.AddRange(e.TagList);
                PridejNasnimanePolozky();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }


        public bool PridejNasnimanePolozky()
        {
            while (nasnimaneKody.Count > 0)
            {
                NajdiPolozkuCarovyKod(nasnimaneKody[0]);
                try { nasnimaneKody.RemoveAt(0); }
                catch { }
            }
            //UpdateUI();

            return true;
        }


        private void NajdiPolozkuCarovyKod(Fask.MST_W.Scanner.RFIDTagData ck)
        {
            try
            {//najde pomoci car kodu a prida
                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow r = null;
                Fask.SQLiteDBs.DataSets.Obecne.RFIDDataTable dtR = new Fask.SQLiteDBs.DataSets.Obecne.RFIDDataTable();

                var result = dsObecne.RFID.Where(x => (x.ID.Equals(ck.TagID, StringComparison.CurrentCultureIgnoreCase)));



                if (result == null) //|| result == 0
                {
                    //Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chimes.wav");
                    OpenNETCF.Media.SystemSounds.Hand.Play();
                    r = dsObecne.RFID.NewRFIDRow();
                    r.ID = ck.TagID;
                    dsObecne.RFID.AddRFIDRow(r);
                }
                else
                {
                    dtR = (Fask.SQLiteDBs.DataSets.Obecne.RFIDDataTable)result.CopyToDataTable(); //.First();
                    r = dtR[0];
                }

                if (!String.IsNullOrEmpty(ck.TagID))
                    r.ID = ck.TagID;
                if (!String.IsNullOrEmpty(ck.TIDMemory))
                    r.TID = ck.TIDMemory;
                if (!string.IsNullOrEmpty(ck.EPCMemory))
                    r.EPC = ck.EPCMemory;
                if (!string.IsNullOrEmpty(ck.ReservedMemory))
                    r.RESERVED = ck.ReservedMemory;
                if (!string.IsNullOrEmpty(ck.UserMemory))
                    r.USER = ck.UserMemory;
                r.Seen += ck.CountReaded;
                r.RSSI = ck.RSSI;

                textBoxRFIDScannerData.Text += String.Format("TagID:{0}\nTIDMemory:{1}\nEPCMemory:{2}\nReservedMemory:{3}\nUserMemory:{4}\nSeen:{5}\nRSSI:{6}\n", r.ID, r.TID, r.EPC, r.RESERVED, r.USER, r.Seen, r.RSSI);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        #endregion


        private void RFIDUHFFillPowerCombobox()
        {
            if (Program.mstw.RFIDUHFScanner != null)
            {
                this.comboBoxPowerUHF.Items.Clear();
                //ziskam si vsechny ty int
                foreach (int val in Program.mstw.RFIDUHFScanner.PowerLevels)
                {
                    this.comboBoxPowerUHF.Items.Add(val); //dam tam value z te tridy
                }

                if (this.comboBoxPowerUHF.Items.Count == 0) 
                {
                    this.comboBoxPowerUHF.Items.Add(200);
                }

                //this.comboBoxPowerUHF.SelectedIndex = 4;
                try
                {
                    this.comboBoxPowerUHF.SelectedItem = MST_Global.RFIDPowerLevel;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "RFIDPowerLevel");
                }
            }
        }

        /// <summary>
        /// zmema a nastavim nove hodnoty energie - je potreba zapnout vypnout ctecku !!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPowerUHF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.mstw.RFIDUHFScanner != null)
            {
                try
                {  //vezmu a nastavim
                    int m_pwr = (int)comboBoxPowerUHF.SelectedItem; //Convert.ToInt32(comboBoxPowerUHF.Items[comboBoxPowerUHF.SelectedIndex]);
                    Program.mstw.RFIDUHFScanner.Power = m_pwr; //nastavim 
                    //MessageBox.Show("Vykon je nyní " + Program.mstw.RFIDUHFScanner.Power + "[mW]");
                    labelRFIDVykon.Text = "Výkon je " + Program.mstw.RFIDUHFScanner.Power + " [mW]";
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }
        /// <summary>
        /// Zmìna zaškrnuti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cfgcheckBoxRFIDActiv_CheckStateChanged(object sender, EventArgs e)
        {
            // TODO : revidovat nastavovani RFID ... 
            try
            {
                if (sender is CheckBox)
                {
                    CheckBox checkbox = sender as CheckBox;
                    if (jeJinyPovolen(checkbox.Name))
                    {
                        MessageBox.Show("Mùže být povolena pouze jedna èteèka RFID tagù", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (checkbox.Checked)
                    {//pokud by byl null
                        if (Program.mstw.RFIDUHFScanner == null)
                        {
                            MST_Global.RFIDPovolitUHF = checkbox.Checked;//true

                            #region init RFID
                            switch (MST_Global.RFIDScannerType)
                            {
                                //25.10.2016 - odstraneno => nepouziva se ...
                                //case Fask.MST_W.Scanner.ScannerRFIDTypes.TT8000:
                                //    RFIDUHFScanner = new Scanner.ScannerRFIDTT8000();
                                //    RFIDUHFScanner.Enable();
                                //    RFIDUHFScanner.Power = MST_Global.RFIDPowerLevel;
                                //    break;
                                case Fask.MST_W.Scanner.ScannerRFIDTypes.MC9090:
                                    Program.mstw.RFIDUHFScanner = new Scanner.ScannerRFIDMC9090();
                                    Program.mstw.RFIDUHFScanner.Enable();
                                    break;
                                case Fask.MST_W.Scanner.ScannerRFIDTypes.MC319Z:
                                    Program.mstw.RFIDUHFScanner = new Scanner.ScannerRFIDMC319Z();
                                    Program.mstw.RFIDUHFScanner.Enable();
                                    break;
                                case Fask.MST_W.Scanner.ScannerRFIDTypes.MC319Z_v2:
                                    Program.mstw.RFIDUHFScanner = new Scanner.ScannerRFIDMC319Z_v2();
                                    Program.mstw.RFIDUHFScanner.Enable();
                                    break;
                                case Fask.MST_W.Scanner.ScannerRFIDTypes.None:
                                default:
                                    Program.mstw.RFIDUHFScanner = null;
                                    break;
                            }
                            #endregion
                            
                            //Program.mstw.RFIDUHFScanner = new Scanner.ScannerRFIDTT8000();//da novy
                            
                            Program.mstw.RFIDUHFScanner.Enable();
                            if (comboBoxPowerUHF.SelectedItem != null)
                            {
                                Program.mstw.RFIDUHFScanner.Power = (int)comboBoxPowerUHF.SelectedItem;
                                povolitRFIDUHFMenu(checkbox.Checked);
                                this.RFIDUHFFillPowerCombobox();//vypnim kombobox
                            }

                        }
                        else
                        {
                            this.RFIDUHFFillPowerCombobox();//vypnim kombobox
                        }



                    }
                    else
                    {
                        MST_Global.RFIDPovolitUHF = checkbox.Checked;//false
                        povolitRFIDUHFMenu(checkbox.Checked);
                        try
                        {
                            this.RFIDScannerStop();
                            Program.mstw.RFIDUHFScanner.Disable();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }

                        Program.mstw.RFIDUHFScanner = null;//dam null
                    }

                    //MST_Global.RFID_memoryEPC = cfgcheckBoxRFID_MemoryEPC.Checked;
                    //MST_Global.RFID_memoryRESERVED = cfgcheckBoxRFID_MemoryRESERVED.Checked;
                    //MST_Global.RFID_memoryTID = cfgcheckBoxRFID_MemoryTID.Checked;
                    //MST_Global.RFID_memoryUSER = cfgcheckBoxRFID_MemoryUSER.Checked;
                }

            }
            catch (Exception exglobal)
            {
                MessageBox.Show(exglobal.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Konstrola zda je jiny povolen 
        /// </summary>
        /// <param name="jmeno"></param>
        /// <returns></returns>
        private bool jeJinyPovolen(string jmeno)
        {
            bool val = false;
            if (!(cfgcheckBoxRFIDActivUHF.Name == jmeno))
            {
                val = cfgcheckBoxRFIDActivUHF.Checked;
            }
            //pro dalsi check boxy stejne

            return val;
        }

        /// <summary>
        /// Nastaveni zda nacitat text nebo ne 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonRFIDUHF_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton radiobutton = sender as RadioButton;
                if (radiobutton.Checked && radiobutton.Text == "Text")
                {
                    MST_Global.RFIDUkladatNacitatText = true;
                    //nastavit text
                }
                else if (radiobutton.Checked && radiobutton.Text == "Hexa")
                {
                    MST_Global.RFIDUkladatNacitatText = false;
                }
            }

        }

        /// <summary>
        /// zapne vypne tlacitka pro uhf tab
        /// </summary>
        /// <param name="povolit"></param>
        private void povolitRFIDUHFMenu(bool povolit)
        {
            this.comboBoxPowerUHF.Enabled = povolit;
            this.radioButtonRFIDUHFHexa.Enabled = povolit;
            this.radioButtonRFIDUHFText.Enabled = povolit;

        }
        #endregion

        private void btnRFIDScannerParams_Click(object sender, EventArgs e)
        {
            if (Program.mstw.RFIDUHFScanner != null)
            {
                try
                {
                    ScannerStop();
                    RFIDScannerStop();

                    Program.mstw.RFIDUHFScanner.Configure();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                }
                finally
                {
                    ScannerStart();
                }
            }
            else
            {
                MessageBoxBig.Show("RFID Scanner is not set", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
            }
        }


        private void gb_RFIDDelete_Click(object sender, EventArgs e)
        {
            if (list_removedRFID.SelectedIndex >= 0 && (Settings.RemovedRFIDCodes != null))
            {
                string curItem = list_removedRFID.SelectedItem.ToString();

                list_removedRFID.Items.RemoveAt(list_removedRFID.SelectedIndex);
                Settings.RemovedRFIDCodes.Remove(curItem);
            }
        }

        private void gb_RFIDUpravit_Click(object sender, EventArgs e)
        {
            if (list_removedRFID.SelectedIndex >= 0 && (Settings.RemovedRFIDCodes != null))
            {
                string value;
                string old_value = list_removedRFID.SelectedItem.ToString();
                ScannerStop();
                if (InputBox.Show("Zadejte nový RFID kód", old_value, out value) == DialogResult.OK)
                {
                    list_removedRFID.Items[list_removedRFID.SelectedIndex] = value;
                    Settings.RemovedRFIDCodes.Remove(old_value);
                    Settings.RemovedRFIDCodes.Add(value);
                }
                ScannerStart();
            }
        }

        private void cfgProdejMnozstvi1Auto_CheckStateChanged(object sender, EventArgs e)
        {
            cfgProdejMnozstviREZ1Vypln.Enabled = !cfgProdejMnozstvi1Auto.Checked;
        }

        private void graphicButton1_Click(object sender, EventArgs e)
        {
            if (Settings.RemovedRFIDCodes == null)
            {
                Settings.RemovedRFIDCodes = new List<string>();
            }

            string value;
            ScannerStop();
            if (InputBox.Show("Zadejte nový RFID kód", "", out value) == DialogResult.OK)
            {
                if (!list_removedRFID.Items.Contains(value))
                    list_removedRFID.Items.Add(value);

                if (!Settings.RemovedRFIDCodes.Contains(value))
                    Settings.RemovedRFIDCodes.Add(value);
            }
            ScannerStart();
        }

        private void eventsOnline_CheckStateChanged(object sender, EventArgs e)
        {
            eventsOnlineConfirm.Enabled = eventsOnline.Checked;
        }

        //====================

        private void buttonActualizeTiskarny_Click(object sender, EventArgs e)
        {
            try
            {
                Program.mstw.mbw.BeginPracujiForm("Aktualizace dokladových tiskáren");

                CiselnikService.CiselnikService cservice = new Fask.MST_W.CiselnikService.CiselnikService();
                cservice.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                cservice.Timeout = MST_Global.ServiceTimeOut;
                cservice.UpdateWebServiceCredentials();
                var so = cservice.KatalogTiskarnyDBPrepare(MST_Global.TerminalID);
                if (so.Exception)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                if (!so.Finished)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                Program.mstw.mbw.EndPracujiForm();

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikTiskarnyDB);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }

            MessageBoxBig.Show("Aktualizace dokladových tiskáren úspìšnì dokonèena", Color.DarkGreen);
        }

        private void buttonActualizePracovnici_Click(object sender, EventArgs e)
        {
            try
            {
                Program.mstw.mbw.BeginPracujiForm("Aktualizace pracovníkù");

                CiselnikService.CiselnikService cservice = new Fask.MST_W.CiselnikService.CiselnikService();
                cservice.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                cservice.Timeout = MST_Global.ServiceTimeOut;
                cservice.UpdateWebServiceCredentials();

                var so = cservice.KatalogPracovniciDBPrepare(MST_Global.TerminalID, string.Empty);
                if (so.Exception)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                if (!so.Finished)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                Program.mstw.mbw.EndPracujiForm();

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikPracovniciDB);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }

            MessageBoxBig.Show("Aktualizace pracovníkù úspìšnì dokonèena", Color.DarkGreen);
        }

        private void button_Smazat_Click(object sender, EventArgs e)
        {
            textBoxRFIDScannerData.Text = String.Empty;
        }

        //FileStream fs;
        // updater;
        private Upgrade.Updater updater;

        private void button_check_Click(object sender, EventArgs e)
        {


            #region Testy
            #region TESTY
            //#if DEBUG
            //            System.Diagnostics.Stopwatch sw = new Stopwatch();
            //            sw.Start();
            //#endif
            //                #region 18.12.2017 TaD test upload data
            //            try
            //            {

            //                //Upload
            //                string URL = MST_Global.ServerAddress + "Upload.aspx";
            //                string Odkud = Main.StorageDir + "A.zip";
            //                string Kam = MST_Global.TerminalID + "\\A.zip";


            //                Program.mstw.mbw.BeginPracujiForm("Probíhá Odesilaní");

            //                Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(URL, Odkud, Kam);

            //                Program.mstw.mbw.EndPracujiForm();

            //                //Download


            //                //Program.mstw.mbw.BeginPracujiForm();
            //                //Fask.MST_W.FileTransfer.Downloading.DownloadFileFromServer(Path.Combine( Main.DataDir , "A.exe.zip"), false, true);
            //                //Program.mstw.mbw.EndPracujiForm();



            //#if DEBUG
            //                sw.Stop();
            //                TimeSpan ts = sw.Elapsed;
            //                MessageBox.Show(String.Format("Cas :{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
            //#endif


            //                #endregion
            //            }
            //            catch (Exception ex)
            //            {
            //                Logging.Log.Write(ex, "Upload to server");
            //                if (fs != null)
            //                {
            //                    fs.Dispose();
            //                    fs = null;
            //                }//throw;
            //            }

            #endregion


            #region 10.1.2018 TaD test upload data > Nejde pod CE nejse trida System.Net.WebClient

            ////Console.Write("\nPlease enter the URI to post data to : ");
            ////String uriString = Console.ReadLine();
            //String uriString = "http://localhost/MST_Win_Kom_Server_6_alfa/Default.aspx";


            //// Create a new WebClient instance.
            // WebClient myWebClient = new WebClient();

            //Console.WriteLine("\nPlease enter the fully qualified path of the file to be uploaded to the URI");
            //string fileName = Console.ReadLine();
            //Console.WriteLine("Uploading {0} to {1} ...", fileName, uriString);

            //// Upload the file to the URI.
            //// The 'UploadFile(uriString,fileName)' method implicitly uses HTTP POST method.
            //byte[] responseArray = myWebClient.UploadFile(uriString, fileName);

            //// Decode and display the response.
            //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",
            //    System.Text.Encoding.ASCII.GetString(responseArray));



            #endregion

            #endregion


            try
            {
               

                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                //System.Reflection.Assembly callingAssembly = System.Reflection.Assembly.GetCallingAssembly();
                //String fullAppName = callingAssembly.GetName().CodeBase;
                //String appPath = Path.GetDirectoryName(fullAppName);
                String updateFilePath = Path.Combine(Main.WrkDir, "update.xml");


                // TODO Dataset naèteni z xml do datasetu
                updater = new Upgrade.Updater(cfgTerminalServerAddress.Text + "upgrade/update.xml", updateFilePath);
                Fask.Upgrade.DataSet.UpdateInfo ds = new Fask.Upgrade.DataSet.UpdateInfo();
                
                //Fask.Upgrade.DataSet.UpdateInfo

                
                //Fask.Upgrade.DataSet.UpdateInfo ds = new Fask.Upgrade.DataSet.UpdateInfo(); //Fask.Upgrade.DataSet.UpdateInfo();


                updater.CheckForVersion(ds);

                if (ds.UpdateData.Count > 0)
                {
                    this.CB_MSTWNEWVersion.Items.Clear();
                }


                foreach (Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow row in ds.UpdateData.Rows)
                {
                    this.CB_MSTWNEWVersion.Items.Add(row);
                    //this.CB_MSTWNEWVersion = row;
                }

                this.CB_MSTWNEWVersion.SelectedItem = 1;

                this.button_update.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"\Application\mst_update\MSTW_Update.exe";

                if (CB_MSTWNEWVersion.SelectedItem is Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow)
                {
                    Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow row = (Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow)CB_MSTWNEWVersion.SelectedItem;

                    //System.Reflection.Assembly callingAssembly = System.Reflection.Assembly.GetCallingAssembly();
                    //String fullAppName = callingAssembly.GetName().CodeBase;
                    //String appPath = Path.GetDirectoryName(fullAppName);
                    Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                    string Argumenty = '"' + row.Version + '"' + " " + '"' + version.ToString() + '"' + " " + '"' + Main.WrkDir + '"';

                    startInfo.Arguments = Argumenty;
                    Process.Start(startInfo);

                    DialogResult = DialogResult.Yes;

                }
                else
                    return;



                //if (updater.CheckForNewVersion((Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow)this.CB_MSTWNEWVersion.SelectedItem, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) == DialogResult.Yes)
                //{
                //    finalize();
                //    this.DialogResult = DialogResult.OK;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }

        /// <summary>
        /// Rozhodovani farebne podle verze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_MSTWNEWVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow selectedItem = (Fask.Upgrade.DataSet.UpdateInfo.UpdateDataRow)this.CB_MSTWNEWVersion.SelectedItem;

            string[] substring = selectedItem.Version.Split('.');

            Version newversion = new Version(int.Parse(substring[0]), int.Parse(substring[1]), int.Parse(substring[2]), int.Parse(substring[3]));



            if (newversion > version)
            {
                //this.CB_MSTWNEWVersion.BackColor = Color.Green;
                this.CB_MSTWNEWVersion.ForeColor = Color.Green;
                this.lblMSTWVersion1.ForeColor = Color.Red;
                this.button_update.Visible = true;
            }
            else if (newversion < version)
            {
                //this.CB_MSTWNEWVersion.BackColor = Color.Red;
                this.CB_MSTWNEWVersion.ForeColor = Color.Red;
                this.lblMSTWVersion1.ForeColor = Color.Green;
                this.button_update.Visible = true;
            }
            else
            {
                //this.CB_MSTWNEWVersion.BackColor = Color.Green;
                this.CB_MSTWNEWVersion.ForeColor = Color.Green;
                this.lblMSTWVersion1.ForeColor = Color.Green;
                this.button_update.Visible = true;
            }

            //this.CB_MSTWNEWVersion.SelectedText = false;
            //this.CB_MSTWNEWVersion.Focused = false;
            //this.CB_MSTWNEWVersion.Focus();
            this.button_check.Focus();
        }


    }
}
