using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{
    public class Konfigurace_Vyroba
    {
        private bool _generovatNenalezenouDavku = false;
        [Popis("Vytvoření nové dávky generováním z externího zdroje ")]
        public bool GenerovatNenalezenouDavku { get => _generovatNenalezenouDavku; set => _generovatNenalezenouDavku = value; }

        private bool _uEventPracovnikLoginEnabled = true;
        [Popis("Povolit přihlášení pracovníka ")]
        public bool UEventPracovnikLoginEnabled { get => _uEventPracovnikLoginEnabled; set => _uEventPracovnikLoginEnabled = value; }

        private TimeSpan _loginUserTimeOut = TimeSpan.Parse("9:00");
        [Popis("Pracovník přihlášení timeout ")]
        public TimeSpan LoginUserTimeOut { get => _loginUserTimeOut; set => _loginUserTimeOut = value; }

        private bool _loginUserTimeOut_Enable = true;
        [Popis("Povolit logiku zadávani času ")]
        public bool LoginUserTimeOut_Enable { get => _loginUserTimeOut_Enable; set => _loginUserTimeOut_Enable = value; }             


        private string _lastProductionUserID = string.Empty;
        [Popis("Posledny uživatel ID ")]
        public string LastProductionUserID { get => _lastProductionUserID; set => _lastProductionUserID = value; }

        
        private string _production_MachineID_Preset = string.Empty;
        [Popis("Stroj ID ")]
        public string Production_MachineID_Preset { get => _production_MachineID_Preset; set => _production_MachineID_Preset = value; }

        private bool _vyroba_Online = false;
        [Popis("Online dotazy a zápisy ")]
        public bool Vyroba_Online { get => _vyroba_Online; set => _vyroba_Online = value; }

        
        private bool _production_Sarze_SN_Enable = false;
        [Popis("Povolit sledovaní SN/šarže ")]
        public bool Production_Sarze_SN_Enable { get => _production_Sarze_SN_Enable; set => _production_Sarze_SN_Enable = value; }

        
        private bool _odvadeni_CasNecinnosti = false;
        [Popis("Dotazovat se na nečinnost ")]
        public bool Odvadeni_CasNecinnosti { get => _odvadeni_CasNecinnosti; set => _odvadeni_CasNecinnosti = value; }

        
        private bool _stopPripravaStartVyrobaIhned = false;
        [Popis("Start výroby ihned po přípravě ")]
        public bool StopPripravaStartVyrobaIhned { get => _stopPripravaStartVyrobaIhned; set => _stopPripravaStartVyrobaIhned = value; }

        
        private bool _odvadeniSledovatCastecneOdvody = false;
        [Popis("Sledovat částečné odvody ")]
        public bool OdvadeniSledovatCastecneOdvody { get => _odvadeniSledovatCastecneOdvody; set => _odvadeniSledovatCastecneOdvody = value; }

        
        private bool _odvadeniPrehled = false;
        [Popis("Zobrazovat přehled ")]
        public bool OdvadeniPrehled { get => _odvadeniPrehled; set => _odvadeniPrehled = value; }

        
        private bool _stopVyrobaPoStartVyrobaIhned = false;
        [Popis("Stop výroby ihned po Startu ")]
        public bool StopVyrobaPoStartVyrobaIhned { get => _stopVyrobaPoStartVyrobaIhned; set => _stopVyrobaPoStartVyrobaIhned = value; }

        
        private bool _production_Material_Enter = false;
        [Popis("Zadávat použité materiály výrobku ")]
        public bool Production_Material_Enter { get => _production_Material_Enter; set => _production_Material_Enter = value; }

        
        private bool _production_Material_OperacePotvrzeniButton = false;
        [Popis("V prúběhu výroby ")]
        public bool Production_Material_OperacePotvrzeniButton { get => _production_Material_OperacePotvrzeniButton; set => _production_Material_OperacePotvrzeniButton = value; }

        
        private bool _production_Material_PredVyrobou = false;
        [Popis("Před výrobou ")]
        public bool Production_Material_PredVyrobou { get => _production_Material_PredVyrobou; set => _production_Material_PredVyrobou = value; }

        
        private bool _odvadeni_production_onlyPositive = true;
        [Popis("Pouze kladné počty množství ")]
        public bool Odvadeni_production_onlyPositive { get => _odvadeni_production_onlyPositive; set => _odvadeni_production_onlyPositive = value; }

        
        private bool _odvadeni_production_VyplnovatMnozstvi = true;
        [Popis("Odvadení předvypnit množství ")]
        public bool Odvadeni_production_VyplnovatMnozstvi { get => _odvadeni_production_VyplnovatMnozstvi; set => _odvadeni_production_VyplnovatMnozstvi = value; }

        
        private bool _odvadeni_production_NeupozornovatNaVetsiPocet = true;
        [Popis("Upozorňovat na \"převyrobené\" množství ")]
        public bool Odvadeni_production_NeupozornovatNaVetsiPocet { get => _odvadeni_production_NeupozornovatNaVetsiPocet; set => _odvadeni_production_NeupozornovatNaVetsiPocet = value; }

        
        private bool _production_Material_PoVyrobe = false;
        [Popis("Po výrobě ")]
        public bool Production_Material_PoVyrobe { get => _production_Material_PoVyrobe; set => _production_Material_PoVyrobe = value; }

        
        private bool _production_Tisk_Etiketa_Enable = false;
        [Popis("Povolit Tisk etikety ")]
        public bool Production_Tisk_Etiketa_Enable { get => _production_Tisk_Etiketa_Enable; set => _production_Tisk_Etiketa_Enable = value; }

        
        private bool _production_Tisk_MnozstviJednaAutomaticky = false;
        [Popis("Tisk množství 1 auto ")]
        public bool Production_Tisk_MnozstviJednaAutomaticky { get => _production_Tisk_MnozstviJednaAutomaticky; set => _production_Tisk_MnozstviJednaAutomaticky = value; }

        
        private string _production_Tisk_MnozstviPredvyplnit = "1";
        [Popis("Tisk množství předvyplnit ")]
        public string Production_Tisk_MnozstviPredvyplnit { get => _production_Tisk_MnozstviPredvyplnit; set => _production_Tisk_MnozstviPredvyplnit = value; }

        
        private bool _production_Tisk_Paletovylistek_Enable = false;
        [Popis("Povolit Tisk paletový lístek ")]
        public bool Production_Tisk_Paletovylistek_Enable { get => _production_Tisk_Paletovylistek_Enable; set => _production_Tisk_Paletovylistek_Enable = value; }

        
        private bool _odvadeniPotvrzeniOperace = false;
        [Popis("Zobrazovat potvrzení Operace ")]
        public bool OdvadeniPotvrzeniOperace { get => _odvadeniPotvrzeniOperace; set => _odvadeniPotvrzeniOperace = value; }

      
        private bool _production_Destination_LOCNCODE_Enter = false;
        [Popis("Zadávat cílovou lokaci ")]
        public bool Production_Destination_LOCNCODE_Enter { get => _production_Destination_LOCNCODE_Enter; set => _production_Destination_LOCNCODE_Enter = value; }

   
        private bool _production_Destination_SKLID_Enter = false;
        [Popis("Zadávat cílový sklad ")]
        public bool Production_Destination_SKLID_Enter { get => _production_Destination_SKLID_Enter; set => _production_Destination_SKLID_Enter = value; }

      
        private TimeSpan _production_UserMaxTimeSpanNoAction = TimeSpan.Parse("00:10");
        [Popis("Maximální doba nečinnosti uživatele ")]
        public TimeSpan Production_UserMaxTimeSpanNoAction { get => _production_UserMaxTimeSpanNoAction; set => _production_UserMaxTimeSpanNoAction = value; }

      
        private string _production_Destination_SKLID = string.Empty;
        [Popis("Cílový sklad ID ")]
        public string Production_Destination_SKLID { get => _production_Destination_SKLID; set => _production_Destination_SKLID = value; }
        
       
        private string _production_Destination_LOCNCODE = string.Empty;
        [Popis("Cílová lokace ID ")]
        public string Production_Destination_LOCNCODE { get => _production_Destination_LOCNCODE; set => _production_Destination_LOCNCODE = value; }

      
        private bool _vyberZakazkyPoPrihlaseni = false;
        [Popis("Výběr zakázky po přihlášení ")]
        public bool VyberZakazkyPoPrihlaseni { get => _vyberZakazkyPoPrihlaseni; set => _vyberZakazkyPoPrihlaseni = value; }

        
        private bool _production_Material_Source_SKLID_Enter = false;
        [Popis("Zadávat zdrojový sklad materiálů ")]
        public bool Production_Material_Source_SKLID_Enter { get => _production_Material_Source_SKLID_Enter; set => _production_Material_Source_SKLID_Enter = value; }

     
        private string _production_Material_Source_SKLID = string.Empty;
        [Popis("Cílový sklad ID ")]
        public string Production_Material_Source_SKLID { get => _production_Material_Source_SKLID; set => _production_Material_Source_SKLID = value; }

     
        private bool _production_Material_Source_LOCNCODE_Enter = false;
        [Popis("Zadávat zdrojovou lokaci materiálů ")]
        public bool Production_Material_Source_LOCNCODE_Enter { get => _production_Material_Source_LOCNCODE_Enter; set => _production_Material_Source_LOCNCODE_Enter = value; }

       
        private string _production_Material_Source_LOCNCODE = string.Empty;
        [Popis("Cílová lokace ID ")]
        public string Production_Material_Source_LOCNCODE { get => _production_Material_Source_LOCNCODE; set => _production_Material_Source_LOCNCODE = value; }

     
        private string _uEventSmenaLogin = "1";
        [Popis("ID UEvent Smena LogIn ")]
        public string UEventSmenaLogin { get => _uEventSmenaLogin; set => _uEventSmenaLogin = value; }

      
        private string _uEventSmenaLogout = "2";
        [Popis("ID UEvent Smena LogOut ")]
        public string UEventSmenaLogout { get => _uEventSmenaLogout; set => _uEventSmenaLogout = value; }

      
        private string _uEventPracovnikPrihlaseni = "3";
        [Popis("ID UEvent pracovník přihlášení ")]
        public string UEventPracovnikPrihlaseni { get => _uEventPracovnikPrihlaseni; set => _uEventPracovnikPrihlaseni = value; }

       
        private string _uEventPracovnikOdhlaseni = "4";
        [Popis("ID UEvent pracovník odhlášení ")]
        public string UEventPracovnikOdhlaseni { get => _uEventPracovnikOdhlaseni; set => _uEventPracovnikOdhlaseni = value; }

      
        private int _vyroba_SSCC_Sequence = 1;
        [Popis("ID SSCC Sequence ")]
        public int Vyroba_SSCC_Sequence { get => _vyroba_SSCC_Sequence; set => _vyroba_SSCC_Sequence = value; }

       
        private bool _production_SSCC_Generovani_auto = false;
        [Popis("SSCC Generovani auto ")]
        public bool Production_SSCC_Generovani_auto { get => _production_SSCC_Generovani_auto; set => _production_SSCC_Generovani_auto = value; }

        
        private bool _odvadeni_PamatovatBarcodeP = false;
        [Popis("Pamatovat posleny Kód ")]
        public bool Odvadeni_PamatovatBarcodeP { get => _odvadeni_PamatovatBarcodeP; set => _odvadeni_PamatovatBarcodeP = value; }

        
        private bool _production_Odeslat_Po_Odvedeni = false;
        [Popis("Odeslat data po odvedeni auto ")]
        public bool Production_Odeslat_Po_Odvedeni { get => _production_Odeslat_Po_Odvedeni; set => _production_Odeslat_Po_Odvedeni = value; }

        
        private bool _production_Odeslat_Po_Odvedeni_Dotaz = true;
        [Popis("Dotaz před odeslánim dat:")]
        public bool Production_Odeslat_Po_Odvedeni_Dotaz { get => _production_Odeslat_Po_Odvedeni_Dotaz; set => _production_Odeslat_Po_Odvedeni_Dotaz = value; }

        private bool _odvadeni_production_ZadatVahu = false;
        [Popis("Požadovat váhu:")]
        public bool Odvadeni_production_ZadatVahu { get => _odvadeni_production_ZadatVahu; set => _odvadeni_production_ZadatVahu = value; }

        private string _odvadeni_production_Vaha_IP = "10.0.0.1";
        [Popis("IP Váhy:")]
        public string Odvadeni_production_Vaha_IP { get => _odvadeni_production_Vaha_IP; set => _odvadeni_production_Vaha_IP = value; }

        private int _odvadeni_production_Vaha_PORT = 10001;
        [Popis("Port Váhy:")]
        public int Odvadeni_production_Vaha_PORT { get => _odvadeni_production_Vaha_PORT; set => _odvadeni_production_Vaha_PORT = value; }




    }
}