using Konzola.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Android
{

    /// <summary>
    /// Formular zabezpecujici obecne nastaveni konfigurace pro terminaly
    /// </summary>
    public partial class Form_AndroidKonfig : Form
    {
        #region Parametry

        private Fask.Interfaces.IMES providerTerminal = null;
        private string _IP = null;
        private int _ID_TERMINAL = 0;
        private DateTime _dateTime = DateTime.MinValue;
        private MES_Android.Konfigurace _data_konfigurace = null; 

        #endregion

        #region inicializace provideru

        /// <summary>
        /// Inicializace providera Production
        /// </summary>
        private void InitProvider()
        {
            #region Productions
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerTerminal == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Terminal.ITerminal).IsAssignableFrom(t))
                            {
                                providerTerminal = (Fask.Interfaces.Terminal.ITerminal)providerAssemlby.CreateInstance(t.FullName);
                                if (providerTerminal != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerTerminal.InitProvider();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }


        #endregion

        #region metody a konstruktor formulare
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="ip">IP adressa</param>
        /// <param name="id_terminal">ID terminalu</param>
        /// <param name="date">cas</param>
        /// <param name="data_IN">data konfigurace</param>
        public Form_AndroidKonfig(string ip, int id_terminal, DateTime date, MES_Android.Konfigurace data_IN)
        {

            try
            {
                _data_konfigurace = data_IN;
                //rozparsovani dat:

                InitializeComponent();
                _IP = ip;
                _ID_TERMINAL = id_terminal;
                _dateTime = date;


                //this.dgVyrobek.UpdateColumnHeaderCellsByDatasource();
                panelButtons.Menu = menuStrip1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// form load konfigurace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_AndroidKonfig_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                //InitializeComponent();
                InitProvider();

                if (providerTerminal == null)
                    throw new Exception("Provider 'Terminal' není inicializován");

                // načtení konfigurace datagridu z nastavení aplikace
                //this.dgVyrobek.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                InitAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region Inicializace prvku konfigurace ve formulari
        /// <summary>
        /// inicializace prvku konfigurace ve formu
        /// </summary>
        private void InitAll()
        {
            Init_Ostatni();
            Init_Vyroba();
            Init_Prodej();
            Init_Prodej_REZ();
            Init_Prodej_Price();
            

            Init_Univerzalni_Lokalizace(_data_konfigurace.GetType(), tabPage4);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Vyroba.GetType(), tabPage1);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Prodej.GetType(), tabPage7);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Prodej.REZ1.GetType(), tabPage15);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Prodej.REZ2.GetType(), tabPage16);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Prodej.REZ3.GetType(), tabPage17);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Prodej.REZ4.GetType(), tabPage18);
            Init_Univerzalni_Lokalizace(_data_konfigurace.Prodej.Cena.GetType(), tabPage11);

        }

        #region pomocne metody
        private void Init_Univerzalni_Lokalizace(Type type, TabPage tp)
        {
            try
            {
                System.Reflection.BindingFlags BF =
                         System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic;

                var fieldValues = type.GetFields(BF);

                foreach (var variable in fieldValues)
                {
                    string MenoPromenne = RemoveVata(variable.Name);
                    var propInfo = type.GetProperty(MenoPromenne);
                    var x = propInfo?.GetCustomAttributes(true);

                    if (x != null)
                    {
                        foreach (object item in x)
                        {
                            MES_Android.PopisAttribute arr = (MES_Android.PopisAttribute)item;


                            foreach (Control c in tp.Controls)
                            {

                                if (c.Name.EndsWith(MenoPromenne))
                                {
                                    c.Text = arr.Popis;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string RemoveVata(string txt)
        {
            string MenoPromenne = string.Empty;

            if (txt.StartsWith("_"))
                MenoPromenne = txt.Substring(1);
            else
                MenoPromenne = txt;

            return MenoPromenne.Substring(0, 1).ToUpper() + MenoPromenne.Substring(1);
        } 
        #endregion

        #region Skladovka

        private void Init_Prodej()
        {
            chb_Skladovka_Prodej_1_AktualizaceZboziPredVyberemDavky.Checked = _data_konfigurace.Prodej.AktualizaceZboziPredVyberemDavky; //bool
            var x2 = _data_konfigurace.Prodej.Cena;
            chb_Skladovka_Prodej_2_DialogNasnimanaLokace.Checked = _data_konfigurace.Prodej.DialogNasnimanaLokace; //bool
            chb_Skladovka_Prodej_3_DialogTisk.Checked = _data_konfigurace.Prodej.DialogTisk; //bool
            chb_Skladovka_Prodej_4_DialogUspesnehoOdeslaniDavky.Checked = _data_konfigurace.Prodej.DialogUspesnehoOdeslaniDavky; //bool
            chb_Skladovka_Prodej_5_DisponibilityHlaska.Checked = _data_konfigurace.Prodej.DisponibilityHlaska; //bool
            chb_Skladovka_Prodej_6_DisponibilityZvuk.Checked = _data_konfigurace.Prodej.DisponibilityZvuk; //bool
            chb_Skladovka_Prodej_7_EtiketaTiskDotazSCenou.Checked = _data_konfigurace.Prodej.EtiketaTiskDotazSCenou; //bool
            chb_Skladovka_Prodej_8_EtiketaTiskDotazSCenou_Cena.Checked = _data_konfigurace.Prodej.EtiketaTiskDotazSCenou_Cena; //bool
            chb_Skladovka_Prodej_9_EtiketaTiskDotazSCenou_ZobrazDialog.Checked = _data_konfigurace.Prodej.EtiketaTiskDotazSCenou_ZobrazDialog; //bool

            chb_Skladovka_Prodej_10_EtiketaTisk_PrebiratMnozstvi.Checked = _data_konfigurace.Prodej.EtiketaTisk_PrebiratMnozstvi; //bool
            chb_Skladovka_Prodej_11_ExistenceNasnimanePolozky.Checked = _data_konfigurace.Prodej.ExistenceNasnimanePolozky; //bool
            chb_Skladovka_Prodej_12_FiltrCiselnikSkladu.Checked = _data_konfigurace.Prodej.FiltrCiselnikSkladu; //bool
            chb_Skladovka_Prodej_13_FiltrCiselnikSkladuOnlyOne.Checked = _data_konfigurace.Prodej.FiltrCiselnikSkladuOnlyOne; //bool
            chb_Skladovka_Prodej_14_FiltrDodavatele.Checked = _data_konfigurace.Prodej.FiltrDodavatele; //bool
            /*string*/
            tB_Skladovka_Prodej_1_.Text = _data_konfigurace.Prodej.GridViewRowCount.ToString();
            chb_Skladovka_Prodej_15_KontrolaStavuSkladu.Checked = _data_konfigurace.Prodej.KontrolaStavuSkladu; //bool
            chb_Skladovka_Prodej_16_Mnozstvi1Auto.Checked = _data_konfigurace.Prodej.Mnozstvi1Auto; //bool
            chb_Skladovka_Prodej_17_MnozstviREZ1Vypln.Checked = _data_konfigurace.Prodej.MnozstviREZ1Vypln; //bool
            chb_Skladovka_Prodej_18_NacistSkladIDOnline.Checked = _data_konfigurace.Prodej.NacistSkladIDOnline; //bool

            chb_Skladovka_Prodej_19_Odberatel.Checked = _data_konfigurace.Prodej.Odberatel; //bool
            chb_Skladovka_Prodej_20_OverovatPohyb.Checked = _data_konfigurace.Prodej.OverovatPohyb; //bool
            chb_Skladovka_Prodej_21_PolozkyVyberJenScannerem.Checked = _data_konfigurace.Prodej.PolozkyVyberJenScannerem; //bool
            chb_Skladovka_Prodej_22_PolozkyVyhledatPomociSarze.Checked = _data_konfigurace.Prodej.PolozkyVyhledatPomociSarze; //bool
            chb_Skladovka_Prodej_23_PovolitNovouPolozku.Checked = _data_konfigurace.Prodej.PovolitNovouPolozku; //bool
            chb_Skladovka_Prodej_24_PovolitPrintServer.Checked = _data_konfigurace.Prodej.PovolitPrintServer; //bool
            chb_Skladovka_Prodej_25_PovolitZadaniMnozstviScannerem.Checked = _data_konfigurace.Prodej.PovolitZadaniMnozstviScannerem; //bool
            chb_Skladovka_Prodej_26_PracovniciKPolozce.Checked = _data_konfigurace.Prodej.PracovniciKPolozce; //bool
            chb_Skladovka_Prodej_27_PracovniciText.Checked = _data_konfigurace.Prodej.PracovniciText; //bool
            tB_Skladovka_Prodej_2_.Text = _data_konfigurace.Prodej.PredvyplneneMnozstviEtikety;

            chb_Skladovka_Prodej_28_RangeEnable.Checked = _data_konfigurace.Prodej.RangeEnable; //bool
            var x32 = _data_konfigurace.Prodej.REZ1;
            var x33 = _data_konfigurace.Prodej.REZ2;
            var x34 = _data_konfigurace.Prodej.REZ3;
            var x35 = _data_konfigurace.Prodej.REZ4;
            tB_Skladovka_Prodej_3_.Text = _data_konfigurace.Prodej.SkladID;
            tB_Skladovka_Prodej_4_.Text = _data_konfigurace.Prodej.SoundDisponibility;
            tB_Skladovka_Prodej_5_.Text = _data_konfigurace.Prodej.SoundExpedice;
            tB_Skladovka_Prodej_6_.Text = _data_konfigurace.Prodej.SoundSklad;
            tB_Skladovka_Prodej_7_.Text = _data_konfigurace.Prodej.SoundSkladExpedice;

            tB_Skladovka_Prodej_8_.Text = _data_konfigurace.Prodej.SoundUspesneVlozeni;
            chb_Skladovka_Prodej_29_StrediskoKPolozce.Checked = _data_konfigurace.Prodej.StrediskoKPolozce; //bool
            chb_Skladovka_Prodej_30_StrediskoText.Checked = _data_konfigurace.Prodej.StrediskoText; //bool
            chb_Skladovka_Prodej_31_ZadaniLocncodePredSN.Checked = _data_konfigurace.Prodej.ZadaniLocncodePredSN; //bool
            chb_Skladovka_Prodej_32_ZobrazitDialogZadaniMnozstviParsovanehoKodu.Checked = _data_konfigurace.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu; //bool
            chb_Skladovka_Prodej_33_ZobrazovatReport.Checked = _data_konfigurace.Prodej.ZobrazovatReport; //bool //33x

        }
        private void Init_Prodej_REZ()
        {
            try
            {
                if (_data_konfigurace.Prodej.REZ1 != null)
                {
                    chb_Skladovka_Prodej_REZ1_1_Cislo.Checked = _data_konfigurace.Prodej.REZ1.Cislo; //bool
                    chb_Skladovka_Prodej_REZ1_2_Pamatovat.Checked = _data_konfigurace.Prodej.REZ1.Pamatovat;  //bool
                    chb_Skladovka_Prodej_REZ1_3_Povinne.Checked = _data_konfigurace.Prodej.REZ1.Povinne; //bool
                    tB_Skladovka_Prodej_REZ1_1_.Text = _data_konfigurace.Prodej.REZ1.PROD_NAME;


                }
                if (_data_konfigurace.Prodej.REZ2 != null)
                {
                    chb_Skladovka_Prodej_REZ2_1_Cislo.Checked = _data_konfigurace.Prodej.REZ2.Cislo; //bool
                    chb_Skladovka_Prodej_REZ2_2_Pamatovat.Checked = _data_konfigurace.Prodej.REZ2.Pamatovat;  //bool
                    chb_Skladovka_Prodej_REZ2_3_Povinne.Checked = _data_konfigurace.Prodej.REZ2.Povinne; //bool
                    tB_Skladovka_Prodej_REZ2_1_.Text = _data_konfigurace.Prodej.REZ2.PROD_NAME;

                }
                if (_data_konfigurace.Prodej.REZ3 != null)
                {
                    chb_Skladovka_Prodej_REZ3_1_Cislo.Checked = _data_konfigurace.Prodej.REZ3.Cislo; //bool
                    chb_Skladovka_Prodej_REZ3_2_Pamatovat.Checked = _data_konfigurace.Prodej.REZ3.Pamatovat;  //bool
                    chb_Skladovka_Prodej_REZ3_3_Povinne.Checked = _data_konfigurace.Prodej.REZ3.Povinne; //bool
                    tB_Skladovka_Prodej_REZ3_1_.Text = _data_konfigurace.Prodej.REZ3.PROD_NAME;


                }
                if (_data_konfigurace.Prodej.REZ4 != null)
                {
                    chb_Skladovka_Prodej_REZ4_1_Cislo.Checked = _data_konfigurace.Prodej.REZ4.Cislo; //bool
                    chb_Skladovka_Prodej_REZ4_2_Pamatovat.Checked = _data_konfigurace.Prodej.REZ4.Pamatovat;  //bool
                    chb_Skladovka_Prodej_REZ4_3_Povinne.Checked = _data_konfigurace.Prodej.REZ4.Povinne; //bool
                    tB_Skladovka_Prodej_REZ4_1_.Text = _data_konfigurace.Prodej.REZ4.PROD_NAME;


                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //throw;
            }
        }
        private void Init_Prodej_Price()
        {

            try
            {

                if (_data_konfigurace.Prodej.Cena != null)
                {
                    chb_Skladovka_Prodej_Price_1_Price0IsWithTax.Checked = _data_konfigurace.Prodej.Cena.Price0IsWithTax; //bool
                    chb_Skladovka_Prodej_Price_2_Price1IsWithTax.Checked = _data_konfigurace.Prodej.Cena.Price1IsWithTax;
                    chb_Skladovka_Prodej_Price_3_Price2IsWithTax.Checked = _data_konfigurace.Prodej.Cena.Price2IsWithTax;
                    chb_Skladovka_Prodej_Price_4_Price3IsWithTax.Checked = _data_konfigurace.Prodej.Cena.Price3IsWithTax;
                    chb_Skladovka_Prodej_Price_5_Price4IsWithTax.Checked = _data_konfigurace.Prodej.Cena.Price4IsWithTax;
                    chb_Skladovka_Prodej_Price_6_Price5IsWithTax.Checked = _data_konfigurace.Prodej.Cena.Price5IsWithTax;
                    chb_Skladovka_Prodej_Price_7_PriceIsWithTax.Checked = _data_konfigurace.Prodej.Cena.PriceIsWithTax;
                    chb_Skladovka_Prodej_Price_8_PriceIsWithTaxEnable.Checked = _data_konfigurace.Prodej.Cena.PriceIsWithTaxEnable;

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //throw;
            }
        }

        #endregion

        #region Vyroba
        private void Init_Vyroba()
        {
            /*string*/
            tB_Vyroba_1_.Text = _data_konfigurace.Vyroba.LastProductionUserID;
            /*TimeSpan*/
            tB_Vyroba_12_.Text = _data_konfigurace.Vyroba.LoginUserTimeOut.ToString();
            chb_Vyroba_2_OdvadeniPotvrzeniOperace.Checked = _data_konfigurace.Vyroba.OdvadeniPotvrzeniOperace;
            chb_Vyroba_3_OdvadeniPrehled.Checked = _data_konfigurace.Vyroba.OdvadeniPrehled;
            chb_Vyroba_4_OdvadeniSledovatCastecneOdvody.Checked = _data_konfigurace.Vyroba.OdvadeniSledovatCastecneOdvody;
            chb_Vyroba_5_Odvadeni_CasNecinnosti.Checked = _data_konfigurace.Vyroba.Odvadeni_CasNecinnosti;
            chb_Vyroba_6_Odvadeni_PamatovatBarcodeP.Checked = _data_konfigurace.Vyroba.Odvadeni_PamatovatBarcodeP;
            chb_Vyroba_7_Odvadeni_production_NeupozornovatNaVetsiPocet.Checked = _data_konfigurace.Vyroba.Odvadeni_production_NeupozornovatNaVetsiPocet;
            chb_Vyroba_8_Odvadeni_production_onlyPositive.Checked = _data_konfigurace.Vyroba.Odvadeni_production_onlyPositive;
            chb_Vyroba_9_Odvadeni_production_VyplnovatMnozstvi.Checked = _data_konfigurace.Vyroba.Odvadeni_production_VyplnovatMnozstvi;
            /*string*/
            tB_Vyroba_2_.Text = _data_konfigurace.Vyroba.Production_Destination_LOCNCODE;
            chb_Vyroba_10_Production_Destination_LOCNCODE_Enter.Checked = _data_konfigurace.Vyroba.Production_Destination_LOCNCODE_Enter;
            /*string*/
            tB_Vyroba_3_.Text = _data_konfigurace.Vyroba.Production_Destination_SKLID;
            chb_Vyroba_11_Production_Destination_SKLID_Enter.Checked = _data_konfigurace.Vyroba.Production_Destination_SKLID_Enter;
            /*string*/
            tB_Vyroba_4_.Text = _data_konfigurace.Vyroba.Production_MachineID_Preset;
            chb_Vyroba_12_Production_Material_Enter.Checked = _data_konfigurace.Vyroba.Production_Material_Enter;
            chb_Vyroba_13_Production_Material_OperacePotvrzeniButton.Checked = _data_konfigurace.Vyroba.Production_Material_OperacePotvrzeniButton;
            chb_Vyroba_14_Production_Material_PoVyrobe.Checked = _data_konfigurace.Vyroba.Production_Material_PoVyrobe;
            chb_Vyroba_15_Production_Material_PredVyrobou.Checked = _data_konfigurace.Vyroba.Production_Material_PredVyrobou;
            /*string*/
            tB_Vyroba_5_.Text = _data_konfigurace.Vyroba.Production_Material_Source_LOCNCODE;
            chb_Vyroba_16_Production_Material_Source_LOCNCODE_Enter.Checked = _data_konfigurace.Vyroba.Production_Material_Source_LOCNCODE_Enter;
            /*string*/
            tB_Vyroba_6_.Text = _data_konfigurace.Vyroba.Production_Material_Source_SKLID;
            chb_Vyroba_17_Production_Material_Source_SKLID_Enter.Checked = _data_konfigurace.Vyroba.Production_Material_Source_SKLID_Enter;
            chb_Vyroba_18_Production_Odeslat_Po_Odvedeni.Checked = _data_konfigurace.Vyroba.Production_Odeslat_Po_Odvedeni;
            chb_Vyroba_19_Production_Odeslat_Po_Odvedeni_Dotaz.Checked = _data_konfigurace.Vyroba.Production_Odeslat_Po_Odvedeni_Dotaz;
            chb_Vyroba_20_Production_Sarze_SN_Enable.Checked = _data_konfigurace.Vyroba.Production_Sarze_SN_Enable;
            chb_Vyroba_21_Production_SSCC_Generovani_auto.Checked = _data_konfigurace.Vyroba.Production_SSCC_Generovani_auto;
            chb_Vyroba_22_Production_Tisk_Etiketa_Enable.Checked = _data_konfigurace.Vyroba.Production_Tisk_Etiketa_Enable;
            chb_Vyroba_23_Production_Tisk_MnozstviJednaAutomaticky.Checked = _data_konfigurace.Vyroba.Production_Tisk_MnozstviJednaAutomaticky;
            /*string*/
            tB_Vyroba_7_.Text = _data_konfigurace.Vyroba.Production_Tisk_MnozstviPredvyplnit;
            chb_Vyroba_24_Production_Tisk_Paletovylistek_Enable.Checked = _data_konfigurace.Vyroba.Production_Tisk_Paletovylistek_Enable;
            /*TimeSpan*/
            tB_Vyroba_13_.Text = _data_konfigurace.Vyroba.Production_UserMaxTimeSpanNoAction.ToString();
            chb_Vyroba_25_StopPripravaStartVyrobaIhned.Checked = _data_konfigurace.Vyroba.StopPripravaStartVyrobaIhned;
            chb_Vyroba_26_StopVyrobaPoStartVyrobaIhned.Checked = _data_konfigurace.Vyroba.StopVyrobaPoStartVyrobaIhned;
            chb_Vyroba_27_UEventPracovnikLoginEnabled.Checked = _data_konfigurace.Vyroba.UEventPracovnikLoginEnabled;
            /*string*/
            tB_Vyroba_8_.Text = _data_konfigurace.Vyroba.UEventPracovnikOdhlaseni;
            /*string*/
            tB_Vyroba_9_.Text = _data_konfigurace.Vyroba.UEventPracovnikPrihlaseni;
            /*string*/
            tB_Vyroba_10_.Text = _data_konfigurace.Vyroba.UEventSmenaLogin;
            /*string*/
            tB_Vyroba_11_.Text = _data_konfigurace.Vyroba.UEventSmenaLogout;
            chb_Vyroba_28_VyberZakazkyPoPrihlaseni.Checked = _data_konfigurace.Vyroba.VyberZakazkyPoPrihlaseni;
            chb_Vyroba_29_Vyroba_Online.Checked = _data_konfigurace.Vyroba.Vyroba_Online;
            /*int*/
            tB_Vyroba_14_.Text = _data_konfigurace.Vyroba.Vyroba_SSCC_Sequence.ToString();

        }

        #endregion

        private void Init_Ostatni()
        {

            chb_Ostatni_1_ExportZbozi.Checked = _data_konfigurace.ExportZbozi;
            chb_Ostatni_2_ExportStrediska.Checked = _data_konfigurace.ExportStrediska;
            chb_Ostatni_3_ExportSklady.Checked = _data_konfigurace.ExportSklady;
            chb_Ostatni_4_ExportPracovnici.Checked = _data_konfigurace.ExportPracovnici;
            chb_Ostatni_5_ExportOdberatele.Checked = _data_konfigurace.ExportOdberatele;
            chb_Ostatni_6_ExportMeny.Checked = _data_konfigurace.ExportMeny;

        }

        #endregion

        #region Atribut reader

        public string GetPopis(Type t)
        {
            Type helpType = typeof(MES_Android.PopisAttribute);
            MES_Android.PopisAttribute[] helpers =
            (MES_Android.PopisAttribute[])t.GetCustomAttributes(helpType, false);

            if (helpers.Length == 1)
                return helpers[0].Popis;
            else
                return null;
        }


        #endregion

        #region interni metody - logika
        private bool DeleteKonfigFile(int tID)
        {
            bool vysledek = false;

            //kontrola zda ma vubec cenu vykonavat logiku
            if (tID != -1)
            {
                try
                {
                    bool startDohledavani = true;

                    if (startDohledavani)
                    {
                        //ziskam konfiguraci ze servru -- metoda volani na server + vraceni dat ke konfiguraci
                        //string StatusCode = null;
                        //string kon = null;

                        #region volani SERVER metody
                        //-----------------------------ziskat data ze serveru !!!---------------------------------------

                        if ((providerTerminal != null) && providerTerminal is Fask.Interfaces.Terminal.ITerminal_DeleteFileTerminalKonfigurace_MESAndroid)
                            vysledek = ((Fask.Interfaces.Terminal.ITerminal_DeleteFileTerminalKonfigurace_MESAndroid)providerTerminal).DeleteFileTerminalKonfigurace_MESAndroid(tID);
                        else
                            throw new Exception("ITerminal_GetTerminalKonfigurace not implementet");
                        #endregion

                    }


                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    // logovat chybu?
                    vysledek = false;
                }
            }

            return vysledek;
        } 
        #endregion

        #region buttons click methods

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tsmiSmazatSouborSERVER_Click(object sender, EventArgs e)
        {
            int TID = -1;
            TID = _ID_TERMINAL;

            if (DeleteKonfigFile(TID))
            {
                //soubor konfigurace na SERVERu byl smazan
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                //soubor konfigurace na SERVERu nebyl smazan
                // chyba!!
                MessageBox.Show(this, string.Format("Pro danný terminál ID: '{0}' nebyl smazán soubor konfigurace!", TID), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
        }

        private void tsmiVychozíiodnoty_Click(object sender, EventArgs e)
        {
            MES_Android.Konfigurace data_konfigurace = null;

            data_konfigurace = new MES_Android.Konfigurace();

            _data_konfigurace = data_konfigurace;

            Form_AndroidKonfig_Load(null, null);

            MessageBox.Show(this, string.Format("Pro terminál ID:'{0}' byla nastavena výchozí konfigurace!", _ID_TERMINAL), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            return;
        }
        #endregion
    }
}
