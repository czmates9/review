using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using System.Threading;
using Fask.Logging;
using Konzola.Forms;
using Konzola;
using Konzola.Extensions;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using Konzola.Vyroba;
using Konzola.Forms.Spolecne;
using Konzola.Sklad;

namespace Konzola.Forms
{

    //[Flags]
    //public enum Opravneni
    //{
    //    Zadna = 0,
    //    Editace = 1,
    //    Import = 2,
    //    Archivace = 3,
    //    // Další oprávnění můžete přidat zde
    //}

    //20.11.2025 MaR oprava bitove
    [Flags]
    public enum Opravneni
    {
        Zadna = 0,          // 0000
        Editace = 1,        // 0001
        Import = 2,         // 0010
        Archivace = 4       // 0100
    }


    public partial class FormMain : Form
    {


        //[Flags]
        //public enum Opravneni
        //{
        //    Zadna = 0,             // No permissions
        //    Editace = 1,           // Permission to edit
        //    Import = 2,            // Permission to import
        //                           // Můžete přidat další oprávnění, pokud je potřeba
        //}


        /// <summary>
        /// Objekt scanneru
        /// </summary>
        public static Fask.Vyroba_P.Scanner.ScannerBase Scanner = null;

        private Fask.Interfaces.IMES provider = null;
      

        private Licence.Licensing _licence = new Licence.Licensing();
        public Licence.Licensing Licence
        {
            //get { return _licence }
            set { _licence = value; }
        }


        #region Eventy formu + konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load event Formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                this.Location = Konfigurace.Globals_Konfig_Konzola.ApplicationPosition;
                this.Icon = Properties.Resources.logo_FASK2;

                // \TODO: pridelat možnost zakazat a povolit Logovani, Všeobence všeho, a jednotlivo každu sekci
                //Log.Enable = Settings.Loging;

                InitProvider();

                if (provider == null)
                    throw new Exception("Provider není inicializován");


                switch (Konfigurace.Globals_Konfig_Konzola.ScannerType)
                {
                    case Fask.Vyroba_P.Scanner.ScannerTypes.COM:
                        Scanner = new Fask.Vyroba_P.Scanner.ScannerCOM();
                        break;
                    case Fask.Vyroba_P.Scanner.ScannerTypes.None:
                    default:
                        Scanner = new Fask.Vyroba_P.Scanner.ScannerNone();
                        break;
                }

                var PathToDS = System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, "DS_Information.xml");

                if (!System.IO.File.Exists(PathToDS))
                {

                    MessageBox.Show(this, "Soubor  :'" + PathToDS + "' neexistuje!" + Environment.NewLine + "Aplikace konzola bude ukončena!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    this.Close();
                }

                Fask.Columns.Inicializace.InitInstance = new Fask.Columns.Inicializace(PathToDS);

                loadSettings();
                loadLicence();
                UpdateToolBar();

                //TODO: MaR prihlaseni uzivatele
                LogIn();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitProvider()
        {


            #region IParametry2
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
                            if (typeof(Fask.Interfaces.Parametry.IParametry2).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.Parametry.IParametry2)providerAssemlby.CreateInstance(t.FullName);
                                if (provider != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion


        }

        /// <summary>
        /// Event který se volá před zavřením Formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.finalize();
        }


        #endregion

        #region Private metody

        /// <summary>
        /// Metoda která slouží pro načtení konfigurace menu zda se ma zobrazit anebo ne
        /// </summary>
        private void loadSettings()
        {

            Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

            #region OK

            #region System

            tsmi_pracovnici.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].AplikacePracovniciPovolit);

            #endregion

            #region Ukolovani

            tsmi_Ukolovani.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].Povolit);
            tsmi_Ukolovani_Ukoly.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].UkolyPovolit);
            tsmi_Ukolovani_PrehledUkolu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].PrehledUkoluPovolit);
            tsmi_Ukolovani_HistorieUkolu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].HistorieUkoluPovolit);


            #endregion

            #region Planovani

            tsmi_Planovani.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit);
            tsmi_Planovani_Planovani_V_V.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_Plany_VV);
            tsmi_Planovani_KapacitniPlanovani.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany);
            tsmi_Planovani_KapacitniPlanovani_MaterialProVyrobu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany_Materialy);

            PlanovaniVyrabenePolozkyToolStripMenuItem.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_VyrabenePolozky);
           

            #endregion

            #region Vyroba

            tsmi_Vyroba.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].Povolit);
            VyrobaVyrabenePolozkyToolStripMenuItem.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_VyrabenePolozky);

            #region Ciselniky

            tsmi_Vyroba_Ciselniky.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit);

            tsmi_Vyroba_Ciselniky_Skupiny.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Skupiny);
            tsmi_Vyroba_Ciselniky_Zbozi.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Zasoby);
            tsmi_Vyroba_Ciselniky_VazbaMaterialy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_VazbyMaterialy);

            tsmi_Vyroba_Ciselniky_Stroje.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Stroje);

            tsmi_Vyroba_Ciselniky_Moduly.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Moduly);
            tsmi_Vyroba_Ciselniky_Prevodniky.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Prevodniky);


            #endregion

            #region Transakce

            tsmi_Vyroba_Transakce.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit);

            tsmi_Vyroba_Transakce_VyrobniPrikazy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit_VyrobniPrikazy);
            tsmi_Vyroba_Transakce_OdvodPOHODA.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit_OdvodPohoda);

            #endregion

            #region Rozbory

            tsmi_Vyroba_Rozbory.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit);

            tsmi_Vyroba_Rozbory_PrehledPlanVyroby.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledPlanVyroby);
            tsmi_Vyroba_Rozbory_PrehledOdvod.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledOdvadeniStroju);
            tsmi_Vyroba_Rozbory_PrehledVyrobky.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledVyrobky);
            tsmi_Vyroba_Rozbory_PrehledVyrobkySNSarze.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledVyrobkySNSarze);
            tsmi_Vyroba_Rozbory_PrehledMaterialy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledMaterialy);
            tsmi_Vyroba_Rozbory_PrehledOdvod_Err.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledOdvadeniStrojuErr);

            tsmi_Vyroba_Rozbory_PrehledOdvod_Stavy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledStavyStroju);

            #endregion

            #endregion

            #region ITCast

            tsmi_ITCast.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit);

            tsmi_ITCast_IPTerminalu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_IP_Terminalu);
            tsmi_ITCast_testDisponibility.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_Test_Disp);
            tsmi_ITCast_testDisponibility2.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_TestDisp2);
            tsmi_ITCast_TypuDokladu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_TypyDokladu);
            tsmi_ITCast_Rady.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_FaskRady);

            tsmi_ITCast_Udalosti_obsluhy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguraceArchivaceZaznamu);
            tsmi_ITCast_produkty.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguraceArchivaceZaznamu_Production);
            #endregion

            #region StavSkladu

            tsmi_StavSkladu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit);
            tsmi_StavSkladu_AktualnyStavSkladu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_AktualnyStav);
            tsmi_StavSkladu_HistoriePohybu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_HistoriePohybu);

            #endregion

            #region Sklad

            tsmi_Sklady.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].Povolit);

            #region Ciselniky

            tsmi_Sklady_Ciselniky.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit);
            TSS_6.Visible = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit;

            tsmi_Sklady_Ciselniky_Sklady.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Sklady);
            tsmi_Sklady_Ciselniky_Strediska.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Strediska);
            tsmi_Sklady_Ciselniky_MapaLokaci.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_MapaLokaci);
            tsmi_Sklady_Ciselniky_VariantyLokaciSortimentu.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_VariantaLokaciMaterialu);
            tsmi_Sklady_Ciselniky_TypyLokaci.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_TypyLokaci);
            tsmi_Sklady_Ciselniky_Zasoby.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Zasoby);
            tsmi_Sklady_Ciselniky_Adresar.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Adresar);
            tsmi_Sklady_Ciselniky_Lokace.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Lokace);
            #endregion

            #region Transakce

            tsmi_Sklady_Transakce.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit);

            #region Prijem s Predlohou

            tsmi_Sklady_Transakce_Prijem.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem);
            tsmi_Sklady_Transakce_Prijem_Predloha.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Predloha);
            tsmi_Sklady_Transakce_Prijem_RizeniPriorit.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_RizeniPriorit);
            tsmi_Sklady_Transakce_Prijem_Nasnimane.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Nasnimane);

            #endregion

            #region Vydej s predlohou

            tsmi_Sklady_Transakce_Vydej.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej);
            tsmi_Sklady_Transakce_Vydej_Predloha.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Predloha);
            tsmi_Sklady_Transakce_Vydej_Priority.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_RizeniPriorit);
            tsmi_Sklady_Transakce_Vydej_Nasnimane.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Nasnimane);

            #endregion

            #region Expedice

            tsmi_Sklady_Transakce_Expedice.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice);
            tsmi_Sklady_Transakce_Expedice_DodaciListy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_DodaciList);
            tsmi_Sklady_Transakce_Expedice_BaleniPaletizace.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_BaleniaPaletiazace);

            #endregion

            #region Volny pohyb / Prodej

            tsmi_Sklady_Transakce_VolnyPohyb.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb);
            tsmi_Sklady_Transakce_VolnyPohyb_Nasnimane.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane);

            #endregion

            #region Prevod

            tsmi_Sklady_Transakce_Prevod.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prevod);

            #endregion

            #region Inventura

            tsmi_Sklady_Transakce_Inventura.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura);
            tsmi_Sklady_Transakce_Inventura_Predloha.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Predloha);
            tsmi_Sklady_Transakce_Inventura_Nasnimane.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Nasnimane);

            #endregion

            #endregion

            #region Rozbory

            tsmi_Sklady_Rozbory.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit);
            tsmi_Sklady_Rozbory_Pohyby.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Pohyby);
            tsmi_Sklady_Rozbory_Stavy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Stavy);

            tsmi_Sklady_Rozbory_Inventura.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Inventura);
            tsmi_Sklady_Rozbory_Inventura_StavInventury.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Inventura_StavInventury);


            tsmi_Sklady_Rozbory_LokMech.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_LokMech);
            tsmi_Sklady_Rozbory_LokMech_StavSklad.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_LokMech_StavSkladu);


            #endregion

            #region Importni mustek

            tsmi_Sklady_ImportMustek.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Importni_mustek[0].Povolit);

            #endregion

            #endregion


            #region Servis

            tsmi_Servis.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit);
            tsmi_Servis_Cinnosti.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Cinnosti);
            tsmi_Servis_Odberatele.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit);
            tsmi_Servis_Stavy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Stavy);
            tsmi_Servis_Okruhy.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Okruhy);
            tsmi_Servis_Vazby_StavStavNext.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_StavStavNext);
            tsmi_Servis_Vazby_CinnostCinnostNext.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_CinnostCinnostNext);
            tsmi_Servis_Vazby_OkruhZdrojSeznam.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_OkruhZdrojSeznam);
            tsmi_Servis_Vazby_DynamickeTabulky.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_DynamickeTabulky);
            tsmi_Servis_Zdroje.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Zdroje);
            tsmi_Servis_StavyZdroju.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_StavyZdroju);
            tsmi_Servis_PohybyZdroju.ResolveVisibleComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_PohybyZdroju);

            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_StavStavNext ||
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_CinnostCinnostNext ||
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_OkruhZdrojSeznam ||
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_DynamickeTabulky)
            {
                tsmi_Servis_Vazby.ResolveVisibleComponent(true);
            }
            else
                tsmi_Servis_Vazby.ResolveVisibleComponent(false);

            #endregion


            #endregion

        }


        private void loadLicence()
        {

    
            #region OK

            #region System

            //tsmi_pracovnici.ResolveEnableComponent(_licence.LicenceObjekt.);

            #endregion

            #region Ukolovani

            tsmi_Ukolovani.ResolveEnableComponent(_licence.LicenceObjekt.Ukolovani);
            //tsmi_Ukolovani_Ukoly.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].UkolyPovolit);
            //tsmi_Ukolovani_PrehledUkolu.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].PrehledUkoluPovolit);
            //tsmi_Ukolovani_HistorieUkolu.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].HistorieUkoluPovolit);


            #endregion

            #region Planovani

            tsmi_Planovani.ResolveEnableComponent( _licence.LicenceObjekt.Planovani);
            tsmi_Planovani_Planovani_V_V.ResolveEnableComponent(_licence.LicenceObjekt.Planovani_PlanyVV);
            tsmi_Planovani_KapacitniPlanovani.ResolveEnableComponent(_licence.LicenceObjekt.Planovani_Kapac);
            //tsmi_Planovani_KapacitniPlanovani_MaterialProVyrobu.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany_Materialy);


            #endregion

            #region Vyroba

            tsmi_Vyroba.ResolveEnableComponent(_licence.LicenceObjekt.Vyroba);

            #region Ciselniky

            tsmi_Vyroba_Ciselniky.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaCiselniky);

            tsmi_Vyroba_Ciselniky_Skupiny.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaCiselnikySkupiny);
            tsmi_Vyroba_Ciselniky_Zbozi.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaCiselnikyZasoby);
            tsmi_Vyroba_Ciselniky_VazbaMaterialy.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaCiselnikyVazbyMaterialy);

            #endregion

            #region Transakce

            tsmi_Vyroba_Transakce.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaTransakce);

            tsmi_Vyroba_Transakce_VyrobniPrikazy.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaTransakceVyrobnyPrikaz);
            tsmi_Vyroba_Transakce_OdvodPOHODA.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaTransakceOdvodPOHODA);

            #endregion

            #region Rozbory

            tsmi_Vyroba_Rozbory.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaRozbory);

            tsmi_Vyroba_Rozbory_PrehledPlanVyroby.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaRozboryPlanVyroby);
            tsmi_Vyroba_Rozbory_PrehledOdvod.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaRozboryOdvadeniStroju);
            tsmi_Vyroba_Rozbory_PrehledVyrobky.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaRozboryVyrobky);
            tsmi_Vyroba_Rozbory_PrehledVyrobkySNSarze.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaRozboryVyrobkySN);
            tsmi_Vyroba_Rozbory_PrehledMaterialy.ResolveEnableComponent(_licence.LicenceObjekt.VyrobaRozboryMaterialy);

            #endregion

            #endregion

            #region ITCast

            tsmi_ITCast.ResolveEnableComponent(_licence.LicenceObjekt.IT_Cast);

            tsmi_ITCast_IPTerminalu.ResolveEnableComponent(_licence.LicenceObjekt.IPTerminalu);
            tsmi_ITCast_testDisponibility.ResolveEnableComponent(_licence.LicenceObjekt.Ostatni);
            tsmi_ITCast_testDisponibility2.ResolveEnableComponent(_licence.LicenceObjekt.Ostatni);
            tsmi_ITCast_TypuDokladu.ResolveEnableComponent(_licence.LicenceObjekt.TypyDokladu);
            tsmi_ITCast_Rady.ResolveEnableComponent(_licence.LicenceObjekt.Fask_Rady);

            #endregion

            #region StavSkladu

            tsmi_StavSkladu.ResolveEnableComponent(_licence.LicenceObjekt.StavSkladu);
            //tsmi_StavSkladu_AktualnyStavSkladu.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_AktualnyStav);
            //tsmi_StavSkladu_HistoriePohybu.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_HistoriePohybu);

            #endregion

            #region Sklad

            tsmi_Sklady.ResolveEnableComponent(_licence.LicenceObjekt.Sklady);

            #region Ciselniky

            tsmi_Sklady_Ciselniky.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselniky);
            //TSS_6.Visible = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit;

            tsmi_Sklady_Ciselniky_Sklady.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikySklady);
            tsmi_Sklady_Ciselniky_Strediska.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikyStrediska);
            tsmi_Sklady_Ciselniky_MapaLokaci.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikyMapaLokaci);
            tsmi_Sklady_Ciselniky_VariantyLokaciSortimentu.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikyVariantyLokaciMaterialu);
            tsmi_Sklady_Ciselniky_TypyLokaci.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikyTypyLokaci);
            tsmi_Sklady_Ciselniky_Zasoby.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikyZasoby);
            tsmi_Sklady_Ciselniky_Adresar.ResolveEnableComponent(_licence.LicenceObjekt.SkladyCiselnikyAdresar);

            #endregion

            #region Transakce

            tsmi_Sklady_Transakce.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakce);

            #region Prijem s Predlohou

            tsmi_Sklady_Transakce_Prijem.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakcePrP);
            //tsmi_Sklady_Transakce_Prijem_Predloha.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Predloha);
            //tsmi_Sklady_Transakce_Prijem_RizeniPriorit.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_RizeniPriorit);
            //tsmi_Sklady_Transakce_Prijem_Nasnimane.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Nasnimane);

            #endregion

            #region Vydej s predlohou

            tsmi_Sklady_Transakce_Vydej.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakceVyP);
            //tsmi_Sklady_Transakce_Vydej_Predloha.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Predloha);
            //tsmi_Sklady_Transakce_Vydej_Priority.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_RizeniPriorit);
            //tsmi_Sklady_Transakce_Vydej_Nasnimane.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Nasnimane);

            #endregion

            #region Expedice

            tsmi_Sklady_Transakce_Expedice.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakceExpedice);
            //tsmi_Sklady_Transakce_Expedice_DodaciListy.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_DodaciList);
            //tsmi_Sklady_Transakce_Expedice_BaleniPaletizace.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_BaleniaPaletiazace);

            #endregion

            #region Volny pohyb / Prodej

            tsmi_Sklady_Transakce_VolnyPohyb.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakceVolnyPohyb);
            //tsmi_Sklady_Transakce_VolnyPohyb_Nasnimane.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane);

            #endregion

            #region Prevod

            tsmi_Sklady_Transakce_Prevod.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakcePrevod);

            #endregion

            #region Inventura

            tsmi_Sklady_Transakce_Inventura.ResolveEnableComponent(_licence.LicenceObjekt.SkladyTransakceInventura);
            //tsmi_Sklady_Transakce_Inventura_Predloha.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Predloha);
            //tsmi_Sklady_Transakce_Inventura_Nasnimane.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Nasnimane);

            #endregion

            #endregion

            #region Rozbory

            tsmi_Sklady_Rozbory.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozbory);
            tsmi_Sklady_Rozbory_Pohyby.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozboryPohyby);
            tsmi_Sklady_Rozbory_Stavy.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozboryStavy);

            tsmi_Sklady_Rozbory_Inventura.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozboryInv);
            tsmi_Sklady_Rozbory_Inventura_StavInventury.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozboryInv_Stav);

            tsmi_Sklady_Rozbory_LokMech.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozboryLokMech);
            tsmi_Sklady_Rozbory_LokMech_StavSklad.ResolveEnableComponent(_licence.LicenceObjekt.SkladyRozboryLokMec_Stavy);

            #endregion

            #endregion


            #region Servis

            tsmi_Servis.ResolveEnableComponent(_licence.LicenceObjekt.Servis);
            //tsmi_Servis_Cinnosti.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Cinnosti);
            //tsmi_Servis_Odberatele.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit);
            //tsmi_Servis_Stavy.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Stavy);
            //tsmi_Servis_Okruhy.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Okruhy);
            //tsmi_Servis_Vazby_StavStavNext.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_StavStavNext);
            //tsmi_Servis_Vazby_CinnostCinnostNext.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_CinnostCinnostNext);
            //tsmi_Servis_Vazby_OkruhZdrojSeznam.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_OkruhZdrojSeznam);
            //tsmi_Servis_Vazby_DynamickeTabulky.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_DynamickeTabulky);
            //tsmi_Servis_Zdroje.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Zdroje);
            //tsmi_Servis_StavyZdroju.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_StavyZdroju);
            //tsmi_Servis_PohybyZdroju.ResolveEnableComponent(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_PohybyZdroju);

            //if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_StavStavNext ||
            //    Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_CinnostCinnostNext ||
            //    Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_OkruhZdrojSeznam ||
            //    Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_DynamickeTabulky)
            //{
            //    tsmi_Servis_Vazby.ResolveEnableComponent(true);
            //}
            //else
            //    tsmi_Servis_Vazby.ResolveEnableComponent(false);

            #endregion


            #endregion

        }

        /// <summary>
        /// Metoda sloužící pro přihlašení uživatele
        /// </summary>
        private void LogIn()
        {
            try
            {
                //TODO: MaR prace s providerem 


                if (FASK.Logins.Uzivatel.Instance != null)
                {
                    FASK.Logins.Uzivatel.Instance = null;
                    UpdateFormText();
                    //MessageBox.Show(this, "Uživatel neni přihlášen. ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }


                using (FormIDPracovnikaLogin frmidprac = new FormIDPracovnikaLogin())
                {
                    DialogResult dr =  frmidprac.ShowDialog(this);

                    if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.Close();
                    }

                }
            }
            finally
            {
                UpdateFormText();
            }
        }


        /// <summary>
        /// Metoda pro zobrazeni informaci o přihlašenem/ odhlašenem uživatelovy
        /// </summary>
        private void UpdateFormText()
        {
            toolStripStatusLabelText.Text = FASK.Logins.Uzivatel.Instance != null ? "Uživatel: " + FASK.Logins.Uzivatel.Instance.FirstName + " " + FASK.Logins.Uzivatel.Instance.SurName : "Uživatel není přihlášen";
        }

        /// <summary>
        /// Metoda která ukončí vše potřebné před zavřením formu
        /// ukonči komunikaci s scannerem, uloží změnené nastavení...
        /// </summary>
        private void finalize()
        {
            try
            {


                Konfigurace.Globals_Konfig_Konzola.ApplicationPosition = this.Location;
                //Settings.Update();

                if (Scanner != null)
                {
                    Scanner.TerminateScanner();
                    Scanner = null;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda pro Update informaci do ToolBaru
        /// </summary>
        private void UpdateToolBar()
        {

            #region New

            string db1 = "DB FASK : '{0}'";
            string Prov = "Provider: '{0}'";
            string dbFASK = "Provider.DB FASK : '{0}'";
            string dbPoh = "Provider.DB POHODA : '{0}'";
            string dbAdr = "Adresa : '{0}'";

            string txt = string.Empty;



            switch (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola)
            {
                case "Fask.ModulePohodaXML.dll":
                    {
                        IDbConnection connection = new SqlConnection(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                        var dbName = connection.Database;

                        TSDDBtn_Info.DropDownItems.Add(string.Format(db1, dbName));

                        string DB_FASK = string.Empty;
                        string DB_POHODA = string.Empty;

                        if ((provider != null) && (provider is Fask.Interfaces.Parametry.IParametry2_Get_ConnectionStrings))
                        {
                            ((Fask.Interfaces.Parametry.IParametry2_Get_ConnectionStrings)provider).Get_ConnectionStrings(out DB_FASK, out DB_POHODA);
                        }
                        else
                        {
                            DB_FASK = "err";
                            DB_POHODA = "err";
                        }

                        TSDDBtn_Info.DropDownItems.Add(string.Format(Prov, "POHODA"));
                        TSDDBtn_Info.DropDownItems.Add(string.Format(dbFASK, DB_FASK));
                        TSDDBtn_Info.DropDownItems.Add(string.Format(dbPoh, DB_POHODA));
                        break;
                    }
                case "Fask.ModuleSql.dll":
                    {
                        IDbConnection connection = new SqlConnection(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                        var dbName = connection.Database;

                        TSDDBtn_Info.DropDownItems.Add(string.Format(db1, dbName));

                        TSDDBtn_Info.DropDownItems.Add(string.Format(Prov, "SQL"));

                        break;
                    }
                case "Fask.ModuleSql_API.dll":
                    {
                        string adresa = string.Empty;
                        string autorizace_DoAPI = string.Empty;
                        string aliasDB = string.Empty;
                        bool ishttps = false;
                        int timeout = 0;

                        if ((provider != null) && (provider is Fask.Interfaces.Parametry.IParametry2_Get_APIConnection))
                        {
                            ((Fask.Interfaces.Parametry.IParametry2_Get_APIConnection)provider).Get_APIConnection(
                                out adresa,
                                out autorizace_DoAPI,
                                out aliasDB,
                                out ishttps,
                                out timeout
                                );
                        }
                        else
                        {
                            adresa = "err";
                        }

                        TSDDBtn_Info.DropDownItems.Add(string.Format(Prov, "SQL pomoci API"));
                        TSDDBtn_Info.DropDownItems.Add(string.Format(dbAdr, adresa));

                        break;
                    }
                case "Fask.ModuleFirebird.dll":
                    {
                        IDbConnection connection = new SqlConnection(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                        var dbName = connection.Database;

                        TSDDBtn_Info.DropDownItems.Add(string.Format(db1, dbName));

                        TSDDBtn_Info.DropDownItems.Add(string.Format(Prov, "FireBird"));
                        break;
                    }
                default:
                    {
                        IDbConnection connection = new SqlConnection(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                        var dbName = connection.Database;

                        TSDDBtn_Info.DropDownItems.Add(string.Format(db1, dbName));
                        TSDDBtn_Info.DropDownItems.Add(string.Format(Prov, "Nenastaven"));
                        break;
                    }
            }

            #endregion

        }

        #endregion

        #region Click Eventy z menu

        #region Aplikace

        #region Prihlašeni/odhlašeni

        /// <summary>
        /// Click event pro přihlášení uživatele
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Login_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        #endregion

        /// <summary>
        /// Event pro správu přístupu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_SpravaPristupu_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin())
                {
                    MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                FASK.Logins.Editace.Form_FASK_Logins frmlist = null;

                string adresa = string.Empty;
                string autorizace_DoAPI = string.Empty;
                string aliasDB = string.Empty;
                bool ishttps = false;
                int timeout = 0;

                if ((provider != null) && (provider is Fask.Interfaces.Parametry.IParametry2_Get_APIConnection))
                {
                    ((Fask.Interfaces.Parametry.IParametry2_Get_APIConnection)provider).Get_APIConnection(
                        out adresa,
                        out autorizace_DoAPI,
                        out aliasDB,
                        out ishttps,
                        out timeout);

                    frmlist = new FASK.Logins.Editace.Form_FASK_Logins( adresa,
                         autorizace_DoAPI,
                         aliasDB,
                         ishttps,
                         timeout,
                         Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID.ToString(),
                         "Konzola");
                }
                else
                {
                    frmlist = new FASK.Logins.Editace.Form_FASK_Logins(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                }

                

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {

                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro správu pracovníkú
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_pracovnici_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //   // Konzola.Ciselniky.FormPracovniciList frmlist = new Ciselniky.FormPracovniciList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    Konzola.Ciselniky.FormPracovniciList frmlist = new Ciselniky.FormPracovniciList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);


            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }
            //    else
            //    {
            //        frmlist.Text = "Pracovníci";
            //    }

            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            //opravneni pridat MaR 16.9.2024
            //
            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                Konzola.Ciselniky.FormPracovniciList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormPracovniciList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormPracovniciList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormPracovniciList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormPracovniciList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmuziv.Text = "Pracovníci";
                }

                frmuziv.MdiParent = this;

                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        /// <summary>
        /// Event pro přístup do konfigurace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_konfigurace_Click(object sender, EventArgs e)
        {
            try
            {
                while (true)
                {
                    if (!Konzola.MySystem.LoginTest.UserLoginTest())
                        return;

                    string res = Konzola.MySystem.LoginTest.UserLoginAdminTestKonfigurace();

                    if (res == "CANCEL")
                        return;

                    PasswordDialog.ExpectedPassword = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].KonfiguraceHeslo.Trim();

                    string enteredPassword;
                    if (PasswordDialog.ShowDialog(out enteredPassword) == DialogResult.OK)
                    {
                        if (enteredPassword.Trim() != PasswordDialog.ExpectedPassword)
                        {
                            MessageBox.Show("Špatné heslo!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // opakuješ cyklus nebo návrat
                            continue;
                        }

                        // heslo je správné
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Akce byla zrušena.", "Informace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }

                using (FormConfig frmConfig = new FormConfig())
                {
                    if (sender is System.Windows.Forms.ToolStripMenuItem menuItem)
                    {
                        frmConfig.Text = menuItem.GetPathToForm();
                    }

                    if (DialogResult.OK == frmConfig.ShowDialog(this))
                        loadSettings();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateToolBar();
            }
        }



        /// <summary>
        /// Event pro zobrazení informací o aplikaci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_AboutBox_Click(object sender, EventArgs e)
        {
            try
            {

                var xx = _licence;
                string LicenceZakaznik = string.Empty;
                DateTime LicenceExpirace;
                string NazevDB = string.Empty;
                LicenceZakaznik = _licence.Licence;
                LicenceExpirace = _licence.Expiration_Date;

                string provider = Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola.ToString();

                if(provider == "Fask.ModulePohodaXML.dll")
                {

                    //Fask.ModulePohodaXML.Globals_V1.LoadConfiguration();

                    
                    string connectionString = Fask.ModulePohodaXML.Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB.ToString();

                    string pattern = @"Initial Catalog=(.*?);";
                    Match match = Regex.Match(connectionString, pattern);

                    if (match.Success)
                    {
                        NazevDB = match.Groups[1].Value;
                    }
                }
                else if(provider == "Fask.ModuleSql.dll")
                {
                    string connectionString = Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString.ToString();

                    string pattern = @"Initial Catalog=(.*?);";
                    Match match = Regex.Match(connectionString, pattern);

                    if (match.Success)
                    {
                        NazevDB = match.Groups[1].Value;
                    }


                }
                else if (provider == "Fask.ModuleSql_API.dll")
                {

                    Fask.ModuleSql_API.Globals_V1.LoadConfiguration();

                    //string connectionString = Fask.ModuleSql_API.Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB.ToString();

                    //string pattern = @"Initial Catalog=(.*?);";
                    //Match match = Regex.Match(connectionString, pattern);

                    //if (match.Success)
                    //{
                    //    NazevDB = match.Groups[1].Value;
                    //}
                }
                else
                {

                }



                using (AboutBox abox = new AboutBox(LicenceZakaznik,LicenceExpirace,provider,NazevDB))
                {
                    abox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro ukončení aplikace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_konec_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Opravdu chcete ukončit aplikaci?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Planovani

        #region Planovani Vyroba Vydej


        private void tsmi_Planovani_Planovani_V_V_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //if (!Settings.VyrobaPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;




            //    Planovani.FormPlanovaniVyrobyList form = new Planovani.FormPlanovaniVyrobyList();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    form.MdiParent = this;
            //    form.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Planovani.FormPlanovaniVyrobyList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Planovani.FormPlanovaniVyrobyList((Opravneni.Editace | Opravneni.Archivace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Planovani.FormPlanovaniVyrobyList(Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Planovani.FormPlanovaniVyrobyList(Opravneni.Import | Opravneni.Archivace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Planovani.FormPlanovaniVyrobyList(Opravneni.Import);
                }

                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Planovani.FormPlanovaniVyrobyList(Opravneni.Editace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Planovani.FormPlanovaniVyrobyList();
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region Kapacitni planovani


        private void tsmi_Planovani_KapacitniPlanovani_MaterialProVyrobu_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;



                //24.9.2019 TaD, stači odkomentovat a pokračovat v implementaci...
                Planovani.Kapacitni_Planovani.FormMaterial frmuziv = new Planovani.Kapacitni_Planovani.FormMaterial(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #endregion

        #region Vyroba

        #region čiselniky

        #region stary ciselnik skupiny
        ///// <summary>
        ///// Event pro zobrazení číselniku skupin ve vyrobe
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tsmi_Vyroba_Ciselniky_Skupiny_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (!Settings.VyrobaPovolit)
        //        //    return;

        //        if (!Konzola.MySystem.LoginTest.UserLoginTest())
        //            return;



        //        #region MaR 16.9.2024 opravneni dodelat
        //        //form = null a pak pokracuj v if
        //        Vyroba.FormSkupinyList frmuziv = null;


        //        if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace() && FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
        //        {
        //            //Vyroba.FormSkupinyList form = null;


        //            // Pokud chcete povolit editaci i import
        //            frmuziv = new FormSkupinyList(Opravneni.Editace | Opravneni.Import);


        //           // frmuziv = new Vyroba.FormSkupinyList();

        //            if (sender is System.Windows.Forms.ToolStripMenuItem)
        //            {
        //                frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
        //            }

        //            frmuziv.MdiParent = this;
        //            frmuziv.Show();
        //        }
        //        else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
        //        {
        //            // Pokud chcete povolit pouze oprávnění pro editaci
        //            frmuziv = new FormSkupinyList(Opravneni.Editace);

        //            //frmuziv = new Vyroba.FormSkupinyList();

        //            if (sender is System.Windows.Forms.ToolStripMenuItem)
        //            {
        //                frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
        //            }

        //            frmuziv.MdiParent = this;
        //            frmuziv.Show();
        //        }

        //        else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
        //        {
        //            frmuziv = new FormSkupinyList(Opravneni.Import);

        //            if (sender is System.Windows.Forms.ToolStripMenuItem)
        //            {
        //                frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
        //            }

        //            frmuziv.MdiParent = this;
        //            frmuziv.Show();
        //        }
        //        else
        //        {
        //             frmuziv = new Vyroba.FormSkupinyList();

        //            if (sender is System.Windows.Forms.ToolStripMenuItem)
        //            {
        //                frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
        //            }

        //            frmuziv.MdiParent = this;
        //            frmuziv.Show();
        //        } 

        //        #endregion



        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //} 
        #endregion


        #region novy ciselnik skupiny MaR 16.9.2024

        /// <summary>
        /// Event pro zobrazení číselníku skupin ve výrobě
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Ciselniky_Skupiny_Click(object sender, EventArgs e)
        {
            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                Vyroba.FormSkupinyList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new FormSkupinyList(Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new FormSkupinyList(Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new FormSkupinyList(Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Vyroba.FormSkupinyList();
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem menuItem)
                {
                    frmuziv.Text = menuItem.GetPathToForm();
                }

                // Zobrazení formuláře jako MDI dítě
                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                // Ošetření chyb
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }


        #endregion


        /// <summary>
        /// Event pro zobrazení číselniku zásob ve vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Ciselniky_Zbozi_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                //if (!Konzola.MySystem.LoginTest.UserLoginTest())
                //    return;


                //opravneni pridat MaR 16.9.2024
                //
                try
                {
                    // Ověření uživatelského přihlášení
                    if (!Konzola.MySystem.LoginTest.UserLoginTest())
                        return;

                    // Inicializace proměnné pro formulář
                    Konzola.Ciselniky.FormZboziList frmuziv = null;

                    // Určení oprávnění podle uživatele
                    Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                    if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                    {
                        opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                    }

                    if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                    {
                        opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                    }

                    // Pokud má uživatel obě oprávnění (Editace i Import)
                    if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                    {
                        // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                        frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Import));
                    }
                    else if (opravneni.HasFlag(Opravneni.Editace))
                    {
                        // Uživatel má pouze oprávnění k editaci
                        frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                    }
                    else if (opravneni.HasFlag(Opravneni.Import))
                    {
                        // Uživatel má pouze oprávnění k importu
                        frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                    }
                    else
                    {
                        // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                        frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                    }

                    // Nastavení názvu formuláře podle položky nabídky
                    if (sender is System.Windows.Forms.ToolStripMenuItem)
                    {
                        frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                    }
                    else
                    {
                        frmuziv.Text = "Zásoby";
                    }

                    frmuziv.MdiParent = this;

                    frmuziv.Show();
                }
                catch(Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




                #region old 16.9. 2024
                //Ciselniky.FormZboziList frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                //if (sender is System.Windows.Forms.ToolStripMenuItem)
                //{

                //    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                //}

                //frmuziv.MdiParent = this;
                //frmuziv.Show(); 
                #endregion



                #region opravneni k editaci a importu
                //Konzola.Ciselniky.FormZboziList frmlist = null;

                //if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace() && FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                //{
                //    //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);

                //    frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true, true);
                //    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                //    if (sender is System.Windows.Forms.ToolStripMenuItem)
                //    {
                //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                //    }
                //    else
                //    {
                //        frmlist.Text = "Zásoby";
                //    }

                //    frmlist.MdiParent = this;

                //    frmlist.Show();


                //}
                //else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                //{
                //    //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);

                //    frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);
                //    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                //    if (sender is System.Windows.Forms.ToolStripMenuItem)
                //    {
                //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                //    }
                //    else
                //    {
                //        frmlist.Text = "Zásoby";
                //    }

                //    frmlist.MdiParent = this;

                //    frmlist.Show();

                //}
                //else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                //{
                //    //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);

                //    frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, false, true);
                //    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                //    if (sender is System.Windows.Forms.ToolStripMenuItem)
                //    {
                //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                //    }
                //    else
                //    {
                //        frmlist.Text = "Zásoby";
                //    }

                //    frmlist.MdiParent = this;

                //    frmlist.Show();

                //}
                //else
                //{
                //    //Vyroba.Rozbory.FormOdvod_EventsList
                //    frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);


                //    if (sender is System.Windows.Forms.ToolStripMenuItem)
                //    {
                //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                //    }
                //    else
                //    {
                //        frmlist.Text = "Zásoby";
                //    }

                //    frmlist.MdiParent = this;

                //    frmlist.Show();
                //}

                #endregion


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazení číselniku vazeb materialu k vyrobku ve vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Ciselniky_VazbaMaterialy_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                Vyroba.FormVazbyMaterialy frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Vyroba.FormVazbyMaterialy((Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Vyroba.FormVazbyMaterialy(Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.FormVazbyMaterialy(Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Vyroba.FormVazbyMaterialy();
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmuziv.Text = "Zásoby";
                }

                frmuziv.MdiParent = this;

                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            #region old
            //try
            //{
            //    //if (!Settings.VyrobaPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    Vyroba.FormVazbyMaterialy form = new Vyroba.FormVazbyMaterialy();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    form.MdiParent = this;
            //    form.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        #endregion

        #region Rozbory

        /// <summary>
        /// Event pro přehled materialu na vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Rozbory_PrehledMaterialy_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                Vyroba.FormProductionSourecesList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Vyroba.FormProductionSourecesList((Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Vyroba.FormProductionSourecesList(Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.FormProductionSourecesList(Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Vyroba.FormProductionSourecesList();
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmuziv.Text = "Zásoby";
                }

                frmuziv.MdiParent = this;

                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            #region old
            ////novy form otevrit FormProductionSourecesList.cs
            ////tabulka production_sources obsahuje materialy in vyroba
            ////tabulka production_sources existuje 
            //try
            //{
            //    //if (!Settings.VyrobaPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;  

            //    // TODO : Novy dialog ...
            //    Vyroba.FormProductionSourecesList form = new Vyroba.FormProductionSourecesList();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    form.MdiParent = this;
            //    form.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro přehled vyrobku na vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Rozbory_PrehledVyrobky_Click(object sender, EventArgs e)
        {
             
            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Vyroba.FormProductionList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if(FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Vyroba.FormProductionList((Opravneni.Editace | Opravneni.Archivace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Vyroba.FormProductionList(Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.FormProductionList(Opravneni.Import | Opravneni.Archivace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.FormProductionList(Opravneni.Import);
                }

                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.FormProductionList(Opravneni.Editace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv =  new Vyroba.FormProductionList();
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            #region old
            //try
            //{
            //    //if (!Settings.VyrobaPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;



            //    #region opravneni k archivaci
            //    Vyroba.FormProductionList frmuziv = null;

            //    if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
            //    {
            //        //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //        frmuziv = new Vyroba.FormProductionList(true);
            //    }
            //    else
            //    {
            //        //Vyroba.Rozbory.FormOdvod_EventsList
            //        frmuziv = new Vyroba.FormProductionList();

            //    }
            //    #endregion

            //    //Vyroba.FormProductionList frmuziv = new Vyroba.FormProductionList();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }


            //    frmuziv.MdiParent = this;
            //    frmuziv.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }



        /// <summary>
        /// Event pro přehled vyráběných položek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_VyrabenePolozky_Main_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

               
                FormVyrabenePolozky frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new FormVyrabenePolozky((Opravneni.Editace | Opravneni.Archivace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new FormVyrabenePolozky(Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new FormVyrabenePolozky(Opravneni.Import | Opravneni.Archivace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new FormVyrabenePolozky(Opravneni.Import);
                }

                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new FormVyrabenePolozky(Opravneni.Editace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new FormVyrabenePolozky();
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }








        /// <summary>
        /// Event pro přehled vyrobku s SN/šaržema na vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Rozbory_PrehledVyrobkySNSarze_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Vyroba.FormProduction_SN_List frm = new Vyroba.FormProduction_SN_List();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frm.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                  

                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro přehled plánu výroby na vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Rozbory_PrehledPlanVyroby_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Vyroba.FormPlanovaniVyroby_Rozbor form = new Vyroba.FormPlanovaniVyroby_Rozbor();
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Event pro přehled odvodu na vyrobe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Rozbory_PrehledOdvod_Click(object sender, EventArgs e)
        {


            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Archivace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region old
            //try
            //{
            //    //frmuziv.

            //    //if (!Settings.VyrobaPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    #region MaR 16.9.2024 opravneni dodelat
            //    //form = null a pak pokracuj v if

            //    if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace() && FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
            //    {

            //    }
            //    else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
            //    {

            //    }

            //    else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
            //    {

            //    }
            //    else
            //    {

            //    }

            //    #endregion


            //    #region Archivace povolení
            //    Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

            //    if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
            //    {
            //        //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //        frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);
            //    }
            //    else
            //    {
            //        //Vyroba.Rozbory.FormOdvod_EventsList
            //        frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    } 
            //    #endregion

            //    //LogIn login =

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmuziv.MdiParent = this;
            //    frmuziv.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }


        #endregion

        #region Transakce



        /// <summary>
        /// Event pro zavolaní Vyrobního přikazu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Transakce_VyrobniPrikazy_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Vyroba.FormVyrobniPrikazList frmuziv = new Vyroba.FormVyrobniPrikazList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro odvadeni vyroby do IS pohoda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Vyroba_Transakce_OdvodPOHODA_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Vyroba.FormVazby_P_PS_List form = new Vyroba.FormVazby_P_PS_List();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #endregion

        #region Ukolovaní

        /// <summary>
        /// Event pro zobrazení ukolu na ukolovani
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Ukolovani_Ukoly_Click(object sender, EventArgs e)
        {
            //9.9.2019 TaD TODO sproznit uzivatele na ukolech
            //try
            //{
            //    if (!Settings.UkolovaniPovolit)
            //        return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    Ukolovani.FormUkolyUzivList frmlist = new Ukolovani.FormUkolyUzivList();
            //    //Ukolovani.Forms.FormUkolyUzivList frmlist = new Ukolovani.Forms.FormUkolyUzivList();
            //    //Ukolovani.Globals.Pracovnik = Globals.Pracovnik;
            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// Event pro přehled ukolu na ukolovani
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Ukolovani_PrehledUkolu_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Ukolovani.FormUkolyAktualniList frmukoluziv = new Ukolovani.FormUkolyAktualniList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmukoluziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmukoluziv.MdiParent = this;
                frmukoluziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro přehled historie ukolu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Ukolovani_HistorieUkolu_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Ukolovani.FormUkolyHistoryList frmhist = new Ukolovani.FormUkolyHistoryList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmhist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmhist.MdiParent = this;
                frmhist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //TaD ?? kod je ale nikde se nepouživalo
        //private void ukolyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!Settings.UkolovaniPovolit)
        //            return;

        //        if (!Konzola.MySystem.LoginTest.UserLoginTest())
        //            return;

        //        Ukolovani.FormUkolyList frmlist = new Ukolovani.FormUkolyList();

        //        frmlist.MdiParent = this;
        //        frmlist.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        #endregion

        #region Stav Skladu

        /// <summary>
        /// Event pro yobrayeni aktualneho stavu skladu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_StavSkladu_AktualnyStavSkladu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                StavSkladu.FormStavSkladuList frmlist = new StavSkladu.FormStavSkladuList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni historie pohybu na skladu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_StavSkladu_HistoriePohybu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                StavSkladu.FormStavSkladuHistorieList frmlist = new StavSkladu.FormStavSkladuHistorieList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Servis

        /// <summary>
        /// Event pro zobrazeni Cinnosti na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Cinnosti_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormCinnostiList frmlist = new Servis.FormCinnostiList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Event pro zobrazeni Stavu na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Stavy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormStavyList frmlist = new Servis.FormStavyList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni Okruhu na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Okruhy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormOkruhyList frmlist = new Servis.FormOkruhyList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Vazby

        /// <summary>
        /// Event pro zobrazeni StavStavNext ve vazbach na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Vazby_StavStavNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormVazbyStavStavNextList frmlist = new Servis.FormVazbyStavStavNextList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni CinnostCinnostNext ve vazbach na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Vazby_CinnostCinnostNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormVazbyCinnostCinnostNextList frmlist = new Servis.FormVazbyCinnostCinnostNextList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni OkruhZdrojSeznam ve vazbach na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Vazby_OkruhZdrojSeznam_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormVazbyOkruhZdrojSeznamList frmlist = new Servis.FormVazbyOkruhZdrojSeznamList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni DynamickeTabulky ve vazbach na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Vazby_DynamickeTabulky_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormVazbyDynTabDefDynTabList frmlist = new Servis.FormVazbyDynTabDefDynTabList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        /// <summary>
        /// Event pro zobrazeni Zdroju na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_Zdroje_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormZdrojeList frmlist = new Servis.FormZdrojeList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmlist.Text = "Zdroje";
                }

                frmlist.MdiParent = this;

                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni Stavy Zdroju na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_StavyZdroju_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormZdrojeStavyList frmlist = new Servis.FormZdrojeStavyList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni Pohyby Zdroju na Servise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_PohybyZdroju_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormZdrojePohybList frmlist = new Servis.FormZdrojePohybList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro report sestavu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Servis_ReportSestava_Click(object sender, EventArgs e)
        {
            //15.1.2018 Disable pro verzi 1.13
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit)
                    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Servis.FormReportSestava frmrepsestava = new Servis.FormReportSestava();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmrepsestava.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmrepsestava.MdiParent = this;
                frmrepsestava.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        /// <summary>
        /// Event pro zobrazeni Odberatele na Servis a Sklady
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Odberatele_Click(object sender, EventArgs e)
        {
            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormOdberateleList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormOdberateleList((Opravneni.Editace | Opravneni.Archivace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormOdberateleList(Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Import) && opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormOdberateleList(Opravneni.Import | Opravneni.Archivace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormOdberateleList(Opravneni.Import);
                }

                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormOdberateleList(Opravneni.Editace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormOdberateleList();
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region old
            //try
            //{
            //    //if (!Settings.ServisPovolit && !Settings.SkladyPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    Konzola.Ciselniky.FormOdberateleList frmlist = new Ciselniky.FormOdberateleList();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        #region Sklady

        #region Ciselniky

        /// <summary>
        /// Event pro zobrazeni ciselniku Skladu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Ciselniky_Sklady_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormSkladyList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormSkladyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormSkladyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormSkladyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormSkladyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmuziv.Text = "Sklady";
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region old
            //try
            //{
            //    //if (!Settings.SkladyPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    // 17.8.2016 PeV: zakomentovano, sjednoceni formularu
            //    //Konzola.Ciselniky.FormSkladyList frmlist = new Ciselniky.FormSkladyList();
            //    Konzola.Ciselniky.FormSkladyList frmlist = new Ciselniky.FormSkladyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }
            //    else
            //    {
            //        frmlist.Text = "Sklady";
            //    }

            //    frmlist.MdiParent = this;

            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro zobrazeni ciselniku Stredisek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Ciselniky_Strediska_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormStrediskaList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormStrediskaList((Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormStrediskaList(Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormStrediskaList(Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormStrediskaList(Opravneni.Archivace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormStrediskaList();
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            #region old
            //try
            //{
            //    //if (!Settings.SkladyPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    Konzola.Ciselniky.FormStrediskaList frmlist = new Ciselniky.FormStrediskaList();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro zobrazeni ciselniku Mapy lokaci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Ciselniky_MapaLokaci_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormLokaceMapaList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormLokaceMapaList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormLokaceMapaList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokaceMapaList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokaceMapaList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,Opravneni.Archivace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormLokaceMapaList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            #region old
            //try
            //{
            //    //if (!Settings.SkladyPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    //Konzola.Ciselniky.FormLokaceMapaList frmlist = new Ciselniky.FormLokaceMapaList();
            //    Konzola.Ciselniky.FormLokaceMapaList frmlist = new Ciselniky.FormLokaceMapaList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }
            //    else
            //    {
            //        frmlist.Text = "Mapa lokací";
            //    }

            //    frmlist.MdiParent = this;

            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro zobrazeni ciselniku Variant lokaci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Ciselniky_VariantyLokaciSortimentu_Click(object sender, EventArgs e)
        {


            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormLokaceVariantySortimentList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormLokaceVariantySortimentList((Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormLokaceVariantySortimentList(Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokaceVariantySortimentList(Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokaceVariantySortimentList(Opravneni.Archivace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormLokaceVariantySortimentList();
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            #region old
            //try
            //{
            //    //if (!Settings.SkladyPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    Konzola.Ciselniky.FormLokaceVariantySortimentList frmlist = new Ciselniky.FormLokaceVariantySortimentList();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro zobrazeni ciselniku Typu lokaci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Ciselniky_TypyLokaci_Click(object sender, EventArgs e)
        {


            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormLokaceTypyList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,(Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,(Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,Opravneni.Archivace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            #region old
            //try
            //{

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    Konzola.Ciselniky.FormLokaceTypyList frmlist = new Ciselniky.FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro zobrazeni ciselniku Zasob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Ciselniky_Zasoby_Click(object sender, EventArgs e)
        {


            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormZboziList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Archivace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region old
            //try
            //{
            //    //if (!Settings.SkladyPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    //Konzola.Ciselniky.FormZboziList frmlist = new Ciselniky.FormZboziList();





            //    //Vyroba.FormProductionList frmuziv = null;



            //    #region MaR 16.9.2024 pridat opravneni
            //   // Konzola.Ciselniky.FormZboziList frmlist =null;


            //    //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    //if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    //{
            //    //    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    //}
            //    //else
            //    //{
            //    //    frmlist.Text = "Zásoby";
            //    //}

            //    //frmlist.MdiParent = this;

            //    //frmlist.Show();



            //    #region opravneni k editaci a importu
            //    Konzola.Ciselniky.FormZboziList frmlist = null;

            //    if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace() && FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
            //    {
            //        //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);

            //        frmlist= new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,true,true);
            //        //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            //        if (sender is System.Windows.Forms.ToolStripMenuItem)
            //        {
            //            frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //        }
            //        else
            //        {
            //            frmlist.Text = "Zásoby";
            //        }

            //        frmlist.MdiParent = this;

            //        frmlist.Show();


            //    }
            //    else if(FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
            //    {
            //        //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);

            //        frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);
            //        //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            //        if (sender is System.Windows.Forms.ToolStripMenuItem)
            //        {
            //            frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //        }
            //        else
            //        {
            //            frmlist.Text = "Zásoby";
            //        }

            //        frmlist.MdiParent = this;

            //        frmlist.Show();

            //    }
            //    else if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
            //    {
            //        //frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);

            //        frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, false,true);
            //        //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            //        if (sender is System.Windows.Forms.ToolStripMenuItem)
            //        {
            //            frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //        }
            //        else
            //        {
            //            frmlist.Text = "Zásoby";
            //        }

            //        frmlist.MdiParent = this;

            //        frmlist.Show();

            //    }
            //    else
            //    {
            //        //Vyroba.Rozbory.FormOdvod_EventsList
            //        frmlist = new Ciselniky.FormZboziList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);


            //        if (sender is System.Windows.Forms.ToolStripMenuItem)
            //        {
            //            frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //        }
            //        else
            //        {
            //            frmlist.Text = "Zásoby";
            //        }

            //        frmlist.MdiParent = this;

            //        frmlist.Show();
            //    }

            //    #endregion
            //    #endregion

            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion





        }


       

        #region Transakce

        #region Prijem

        /// <summary>
        /// Event pro Predlohu na prijmu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Prijem_Predloha_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Prijem.FormDavkyPrijmuList frmlist = new Prijem.FormDavkyPrijmuList();

                #region Editace povolení
                Konzola.Prijem.FormDavkyPrijmuList frmuziv = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmuziv = new Prijem.FormDavkyPrijmuList(true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmuziv = new Prijem.FormDavkyPrijmuList(false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Event pro zobrazeni Nasnimanych položek na přijmu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Prijem_Nasnimane_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Prijem.FormPrijemNasnimaneList frmlist = new Prijem.FormPrijemNasnimaneList();

                #region Editace povolení
                Konzola.Prijem.FormPrijemNasnimaneList frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new Prijem.FormPrijemNasnimaneList(true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new Prijem.FormPrijemNasnimaneList(false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Vydej

        /// <summary>
        /// Event pro zobrazeni předlohy na vydeji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Vydej_Predloha_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Vydej.FormDavkyVydejeList frmlist = new Vydej.FormDavkyVydejeList();

                #region Editace povolení
                Konzola.Vydej.FormDavkyVydejeList frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new Vydej.FormDavkyVydejeList(true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new Vydej.FormDavkyVydejeList(false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni okna pro řizeni priorit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Vydej_Priority_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Vydej.FormVydejDavkyList frmlist = new Vydej.FormVydejDavkyList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni nasnimanych položek na vydeji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Vydej_Nasnimane_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Vydej.FormVydejNasnimaneList frmlist = new Vydej.FormVydejNasnimaneList();

                #region Editace povolení
                Konzola.Vydej.FormVydejNasnimaneList frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new Vydej.FormVydejNasnimaneList(true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new Vydej.FormVydejNasnimaneList(false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Expedice

        /// <summary>
        /// Event pro zobrazeni Dodacich listu na expedici
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Expedice_DodaciListy_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit || !Settings.SkladyTransakceExpedicePovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Expedice.FormDodaciListyList frmlist = new Expedice.FormDodaciListyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                #region Editace povolení
                Konzola.Expedice.FormDodaciListyList frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new Konzola.Expedice.FormDodaciListyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST ,true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new Konzola.Expedice.FormDodaciListyList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni buferu na expedici
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Expedice_BaleniPaletizace_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit || !Settings.SkladyTransakceExpedicePovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Expedice.FormBufferBaleniList frmlist = new Expedice.FormBufferBaleniList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                #region Editace povolení
                Konzola.Expedice.FormBufferBaleniList frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new Konzola.Expedice.FormBufferBaleniList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new Konzola.Expedice.FormBufferBaleniList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Prodej

        /// <summary>
        /// Event pro zobrazeni nasnimanych dat v Volneho pohybu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_VolnyPohyb_Nasnimane_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

               // Konzola.VolnyPohyb.FormVolnyPohybNasnimaneList frmlist = new VolnyPohyb.FormVolnyPohybNasnimaneList();

                #region Editace povolení
                Konzola.VolnyPohyb.FormVolnyPohybNasnimaneList frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new Konzola.VolnyPohyb.FormVolnyPohybNasnimaneList(true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new Konzola.VolnyPohyb.FormVolnyPohybNasnimaneList(false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Inventura

        /// <summary>
        /// Event pro zobrazeny nasnimanych dat z inventury
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Transakce_Inventura_Nasnimane_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Inventura.FormInventuraNasnimaneList frmlist = new Inventura.FormInventuraNasnimaneList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        ///Hlavicky inventury
        //private void hlavickyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!Settings.SkladyPovolit)
        //            return;

        //        if (!Konzola.MySystem.LoginTest.UserLoginTest())
        //            return;

        //        //Konzola.Inventura.FormInventuraHlavickaList frmlist = new Inventura.FormInventuraHlavickaList();
        //        Konzola.Inventura.FormInventuraHlavickaList2 frmlist = new Konzola.Inventura.FormInventuraHlavickaList2(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

        //        frmlist.MdiParent = this;
        //        frmlist.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        #endregion

        #endregion

        #region Rozbory

        /// <summary>
        /// Event pro zobrazeni pohybu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Rozbory_Pohyby_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Sklady.FormSkladPohybList frmlist = new Sklady.FormSkladPohybList();

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni stavu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Rozbory_Stavy_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Sklady.FormSkladStavLokaceList frmlist = new Sklady.FormSkladStavLokaceList();

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event pro zobrazeni Stavu Lok. Mechanizmu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Sklady_Rozbory_LokMech_StavSklad_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.SkladLokace.FormSkladLokaceStavList frmlist = new SkladLokace.FormSkladLokaceStavList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                
                    frmlist.MdiParent = this;
                    frmlist.Show();
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Event pro zobrazeni porovnaní vuči IS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void porovnani_vuci_ISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                SkladLokace.FormSKladLokace_PorovnaniVuciIS frmlist = new SkladLokace.FormSKladLokace_PorovnaniVuciIS();

                frmlist.MdiParent = this;
                frmlist.Show();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region  IT část

        /// <summary>
        /// Event pro zobrazení IP použitych terminalu a možnost vzdaleneho ovladani
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_ITCast_IPTerminalu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
                    return;

                Terminal.FormTerminalSeznam form = new Terminal.FormTerminalSeznam();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Testovaci okno ktere provede kontrolu disponibility, asi smazat, a přište použit UnityTest...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_ITCast_testDisponibility_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
                    return;

                Konzola.Terminal.TestDisp frmuziv = new Konzola.Terminal.TestDisp();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event ktery zavola okno pro editaci typu dokladu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_ITCast_TypuDokladu_Click(object sender, EventArgs e)
        {
            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
                {
                  
                        MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                 
                    return;
                }



                // Inicializace proměnné pro formulář
                Konzola.Ciselniky.FormTypyDokladuList frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormTypyDokladuList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormTypyDokladuList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormTypyDokladuList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST,Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormTypyDokladuList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                // Ošetření chyb
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //----------------------------------------------------------------------------




            #region old
            //try
            //{


            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
            //        return;

            //    Konzola.Ciselniky.FormTypyDokladuList frmlist = new Ciselniky.FormTypyDokladuList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmlist.MdiParent = this;
            //    frmlist.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        /// <summary>
        /// Event pro zobrazeni okna pro editaci vazebni tabulky Rady
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_ITCast_Rady_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
                    return;

                Konzola.Ciselniky.FormRADYList frmlist = new Ciselniky.FormRADYList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// <summary>
        /// Okno pro kontrolu disponibility, z CZMST_SE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testDisponibility2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
                    return;

                //string pocetStr = string.Empty;

                //DialogResult drPocet = Vyroba_Konzola.Forms.InputBox.Show("číslo dávky", "Zadejte číslo dávky z CZMST_SE", string.Empty, Forms.InputBox.TypeOfCode.NumericInt, false, 0, 0, false, out pocetStr);

                //if (drPocet == System.Windows.Forms.DialogResult.Cancel)
                //    return;

                //int davka = int.Parse(pocetStr);

                Terminal.TestDisp_2 form = new Terminal.TestDisp_2();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    form.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                //form.CountEntriesCurrent = davka;

                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Experiment, konfigurace providera pohoda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_konfiguracePohoda_Click(object sender, EventArgs e)
        {
            try
            {
                using (Fask.ModulePohodaXML.Konfigurace.Konfig frm = new Fask.ModulePohodaXML.Konfigurace.Konfig())
                {
                    frm.ShowDialog();
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }



        #endregion

        #endregion

        /// <summary>
        /// zabraneni pridani ikony z mdi formulare do hlavniho menu ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {

            try
            {
                if (e.Item.Text == "")
                {
                    e.Item.Visible = false;
                }
            }
            catch { }
        }

        private void tsmi_Sklady_Transakce_Prijem_RizeniPriorit_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tsmi_Sklady_Transakce_Inventura_Predloha_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Inventura.FormInv_PredlohaList frmlist = new Inventura.FormInv_PredlohaList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmi_Sklady_Rozbory_Inventura_StavInventury_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Inventura.FormInventuraPredlohaList frmlist = new Inventura.FormInventuraPredlohaList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmi_Sklady_Transakce_Inventura_Porovnani_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.Inventura.FormInventura_Porovnani frmlist = new Inventura.FormInventura_Porovnani();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void tsmi_Vyroba_Rozbory_PrehledOdvod_Err_Click(object sender, EventArgs e)
        {

            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;




                Vyroba.Rozbory.FormOdvod_EventsErrList frmuziv = new Vyroba.Rozbory.FormOdvod_EventsErrList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsmi_Vyroba_Rozbory_PrehledOdvod_Stavy_Click(object sender, EventArgs e)
        {

            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Vyroba.Rozbory.FormOdvod_MachineStateSetList frmuziv = new Vyroba.Rozbory.FormOdvod_MachineStateSetList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsmi_Sklady_ImportMustek_POHODADodavateleZasoby_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                ImportniMustky.FormImport_SKzNC_List frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new ImportniMustky.FormImport_SKzNC_List(Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new ImportniMustky.FormImport_SKzNC_List(Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new ImportniMustky.FormImport_SKzNC_List(Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new ImportniMustky.FormImport_SKzNC_List();
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem menuItem)
                {
                    frmuziv.Text = menuItem.GetPathToForm();
                }

                // Zobrazení formuláře jako MDI dítě
                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                // Ošetření chyb
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region old
            //try
            //{
            //    //if (!Settings.VyrobaPovolit)
            //    //    return;

            //    if (!Konzola.MySystem.LoginTest.UserLoginTest())
            //        return;

            //    ImportniMustky.FormImport_SKzNC_List frmuziv = new ImportniMustky.FormImport_SKzNC_List();

            //    if (sender is System.Windows.Forms.ToolStripMenuItem)
            //    {
            //        frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
            //    }

            //    frmuziv.MdiParent = this;
            //    frmuziv.Show();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        private void tsmi_ITCast_archivaceZaznamu_Click(object sender, EventArgs e)
        {
            try
            {
                //frmuziv.

                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;


                #region Archivace povolení
                Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmuziv = new Vyroba.Rozbory.FormOdvod_EventsList(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                }
                #endregion

                //LogIn login =

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmi_ITCast_archivaceProduction_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Konzola.IT_cast.FormIT_cast_ProductionArchivace frmuziv = new Konzola.IT_cast.FormIT_cast_ProductionArchivace();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void produktyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                #region opravneni k archivaci
                Vyroba.FormProductionList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmuziv = new Vyroba.FormProductionList(true);

                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmuziv = new Vyroba.FormProductionList();

                }
                #endregion

                //Vyroba.FormProductionList frmuziv = new Vyroba.FormProductionList();

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 


        private void skladyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Konzola mistra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ciselnikyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Konzola mistra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void hromadnaArchivaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Konzola mistra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void transakceSPredlohouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Settings.SkladyPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                //Konzola.Vydej.FormDavkyVydejeList frmlist = new Vydej.FormDavkyVydejeList();

                #region Editace povolení
                FormTransakceSPredlohou frmlist = null;
                //Vyroba.Rozbory.FormOdvod_EventsList frmuziv = null;

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    frmlist = new FormTransakceSPredlohou(true);
                }
                else
                {
                    //Vyroba.Rozbory.FormOdvod_EventsList
                    frmlist = new FormTransakceSPredlohou(false);

                }
                #endregion

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmlist.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmlist.MdiParent = this;
                frmlist.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmi_Sklady_Ciselniky_Click(object sender, EventArgs e)
        {

        }

        private void lokaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář

                Konzola.Ciselniky.FormLokace frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                {
                    opravneni |= Opravneni.Archivace; // Přidání oprávnění k importu
                }


                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Ciselniky.FormLokace(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, (Opravneni.Editace | Opravneni.Import));
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Ciselniky.FormLokace(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokace(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Archivace))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Ciselniky.FormLokace(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Archivace);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Ciselniky.FormLokace(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }


                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void tsmi_Vyroba_Rozbory_RozborOdvoduSSS_Click(object sender, EventArgs e)
        {

            try
            {
                //if (!Settings.VyrobaPovolit)
                //    return;

                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                Vyroba.Rozbory.Form_MachineStateSetList_RozborOdvoduSSS frmuziv = new Vyroba.Rozbory.Form_MachineStateSetList_RozborOdvoduSSS(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);

                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }

                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void tsmi_Vyroba_Ciselniky_Stroje_Click(object sender, EventArgs e)
        {
        

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                Vyroba.Ciselnik.Form_Stroje frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Vyroba.Ciselnik.Form_Stroje(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Vyroba.Ciselnik.Form_Stroje(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.Ciselnik.Form_Stroje(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Vyroba.Ciselnik.Form_Stroje(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmuziv.Text = "Stroje";
                }

                // Zobrazení formuláře jako MDI dítě
                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                // Ošetření chyb
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void tsmi_Vyroba_Ciselniky_Moduly_Click(object sender, EventArgs e)
        {

            try
            {
                // Ověření uživatelského přihlášení
                if (!Konzola.MySystem.LoginTest.UserLoginTest())
                    return;

                // Inicializace proměnné pro formulář
                Vyroba.Ciselnik.Form_Moduly frmuziv = null;

                // Určení oprávnění podle uživatele
                Opravneni opravneni = Opravneni.Zadna; // Výchozí stav bez oprávnění

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Editace())
                {
                    opravneni |= Opravneni.Editace; // Přidání oprávnění k editaci
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
                {
                    opravneni |= Opravneni.Import; // Přidání oprávnění k importu
                }

                // Pokud má uživatel obě oprávnění (Editace i Import)
                if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má obě oprávnění, můžeme předat obě oprávnění do formuláře
                    frmuziv = new Vyroba.Ciselnik.Form_Moduly(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace | Opravneni.Import);
                }
                else if (opravneni.HasFlag(Opravneni.Editace))
                {
                    // Uživatel má pouze oprávnění k editaci
                    frmuziv = new Vyroba.Ciselnik.Form_Moduly(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Editace);
                }
                else if (opravneni.HasFlag(Opravneni.Import))
                {
                    // Uživatel má pouze oprávnění k importu
                    frmuziv = new Vyroba.Ciselnik.Form_Moduly(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST, Opravneni.Import);
                }
                else
                {
                    // Uživatel nemá žádná specifická oprávnění, otevře se výchozí formulář
                    frmuziv = new Vyroba.Ciselnik.Form_Moduly(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST);
                }

                // Nastavení názvu formuláře podle položky nabídky
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    frmuziv.Text = ((System.Windows.Forms.ToolStripMenuItem)sender).GetPathToForm();
                }
                else
                {
                    frmuziv.Text = "Moduly";
                }

                // Zobrazení formuláře jako MDI dítě
                frmuziv.MdiParent = this;
                frmuziv.Show();
            }
            catch (Exception ex)
            {
                // Ošetření chyb
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}



//Matoušovina
public class PasswordDialog : Form
{
    private TextBox passwordTextBox;
    private Button okButton;
    private Label messageLabel;

    //MaR zmena hesla 12.6.2025
    // Původní řádek - nelze měnit za běhu:
    // public const string ExpectedPassword = "159149";

    // Nový řádek:
    public static string ExpectedPassword { get; set; } = "159149";

    public PasswordDialog()
    {
        this.Text = "Zadejte heslo";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new Size(300, 150);

        messageLabel = new Label
        {
            Text = "Zadejte heslo:",
            Location = new Point(10, 10),
            AutoSize = true
        };

        passwordTextBox = new TextBox
        {
            Location = new Point(10, 40),
            Width = 260,
            UseSystemPasswordChar = true
        };

        okButton = new Button
        {
            Text = "OK",
            Location = new Point(200, 70),
            Width = 70
        };

        okButton.Click += OkButton_Click;

        this.Controls.Add(messageLabel);
        this.Controls.Add(passwordTextBox);
        this.Controls.Add(okButton);
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        if (passwordTextBox.Text.Trim() == ExpectedPassword)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        else
        {
            passwordTextBox.BackColor = Color.Red;
            messageLabel.Text = "Chybné heslo!";
            messageLabel.ForeColor = Color.Red;
        }
    }

    public static DialogResult ShowDialog(out string enteredPassword)
    {
        using (var dialog = new PasswordDialog())
        {
            var result = dialog.ShowDialog();
            enteredPassword = dialog.passwordTextBox.Text;
            return result;
        }
    }
}

