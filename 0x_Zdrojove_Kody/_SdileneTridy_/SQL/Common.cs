using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.SQL.Constants
{
    public static class Common
    {
        public const string application_K = "FASK_MES_Konzola"; // Konstantnz nazev aplikace při importi do IS POHODA
        public const string application_S = "FASK_MES_Server"; // Konstantnz nazev aplikace při importi do IS POHODA


        //Vydej
        public const string TABLE_CZMST_SE = "CZMST_SE";
        public const string TABLE_CZMST_SE_SN = "CZMST_SE_SN";
        public const string TABLE_CZMST_SI = "CZMST_SI";
        public const string TABLE_CZMST_SIH = "CZMST_SIH";
        public const string TABLE_CZMST_SI_BV = "CZMST_SI_BV";


        //vyroba
        public const string TABLE_PRODUCTION = "Production";

        // Uzivatele ke vsemu
        public const string TABLE_FASK_LOGINS = "FASK_Logins"; // ciselnik uzivatelu
        public const string TABLE_FASK_LOGINS_AUTH = "FASK_Logins_Auth"; // ciselnik uzivatelskych opravneni
        public const string TABLE_FASK_AGENDA = "FASK_AGENDA"; // ciselnik agend pro práva


        // ciselniky
        public const string TABLE_CZMST090 = "CZMST090"; // ciselnik odberatelu
        public const string TABLE_CZMST091 = "CZMST091"; // ciselnik stredisek
        public const string TABLE_CZMST092 = "CZMST092"; // ciselnik Typu dokladu
        public const string TABLE_CZMST093 = "CZMST093"; // ciselnik skladu
        public const string TABLE_CZMST094 = "CZMST094"; // ciselnik lokaci
        public const string TABLE_FASK_ZASOBY = "FASK_ZASOBY"; // ciselnik zbozi
        public const string TABLE_FASK_ZASOBY_PARAMETRY = "FASK_ZASOBY_PARAMETRY"; // ciselnik zbozi parametry
        public const string TABLE_CZMST096 = "CZMST096"; // ciselnik pracovniku
        //public const string TABLE_CZMSTPWD = "CZMSTPWD"; // ciselnik uzivatelu
        public const string TABLE_CZMST_SKLADLOKACE_LOKACETYPY = "CZMST_SkladLokace_LokaceTypy";   // ciselnik typu lokaci lokaci v mape skladu
        public const string TABLE_CZMST_SKLADLOKACE_LOKACEVARIANTYSORTIMENT = "CZMST_SkladLokace_LokaceVariantySortiment"; // ciselnik variant lokaci

        public const string TABLE_FASK_RADY = "FASK_RADY"; // ciselnik řady


        // Import Mustek tabulka pro IS POHODA
        public const string TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC = "FASK_ZASOBY_IMPORT_POHODA_SKzNC";


        // lokacni mechanismus
        public const string TABLE_CZMST_SKLADLOKACE_MAPA = "CZMST_SkladLokace_Mapa";   // ciselnik lokaci v mape skladu
        public const string TABLE_CZMST_SKLADLOKACE_STAV = "CZMST_SkladLokace_Stav";   // aktualni stav skladu
        public const string TABLE_CZMST_SKLADLOKACE_STAVPOHYB = "CZMST_SkladLokace_StavPohyb";   // pohyby lokacniho mechanismu

        // Inventury
        public const string TABLE_CZMST_I1 = "CZMST_I1";
        public const string TABLE_CZMST_I1H = "CZMST_I1H";
        public const string TABLE_CZMST_I4 = "CZMST_I4";
        public const string TABLE_CZMST_I3 = "CZMST_I3";

        // Prijem
        public const string TABLE_CZMST_PE = "CZMST_PE";
        public const string TABLE_CZMST_PI = "CZMST_PI";

        //Prodej
        public const string TABLE_CZMST_DI = "CZMST_DI";
        public const string TABLE_CZMST_DIH = "CZMST_DIH";

        // Servis
        public const string TABLE_CZMST_SERVIS_CINNOST = "CZMST_Servis_Cinnost";
        public const string TABLE_CZMST_SERVIS_CINNOSTNEXT = "CZMST_Servis_CinnostNext";
        public const string TABLE_CZMST_SERVIS_STAV = "CZMST_Servis_Stav";
        public const string TABLE_CZMST_SERVIS_OKRUH = "CZMST_Servis_Okruh";
        public const string TABLE_CZMST_SERVIS_STAVNEXT = "CZMST_Servis_StavNext";
        public const string TABLE_CZMST_SERVIS_ZDROJ = "CZMST_Servis_Zdroj";
        public const string TABLE_CZMST_SERVIS_ZDROJSTAV = "CZMST_Servis_ZdrojStav";
        public const string TABLE_CZMST_SERVIS_ZDROJSEZNAM = "CZMST_Servis_ZdrojSeznam";
        public const string TABLE_CZMST_SERVIS_ZDROJPOHYB = "CZMST_Servis_ZdrojPohyb";
        public const string TABLE_CZMST_SERVIS_DYNAMIC_TABLE_DEFINITION = "CZMST_Servis_Dynamic_Table_Definition";

        // Expedice
        public const string TABLE_CZMST_EXPEDICE_POLOZKY = "CZMST_Expedice_Polozky";
        public const string TABLE_CZMST_EXPEDICE_HLAVICKA = "CZMST_Expedice_Hlavicka";

        //Expedice Baleni
        public const string TABLE_CZMST_EXPEDICE_Baleni_Buffer = "CZMST_Expedice_Baleni_Buffer";
        

        //Odvod Vyroby
        public const string TABLE_FASK_Events = "FASK_Events"; // Udalosti odvodu vyroby

        //Odvod Vyroby chyby
        public const string TABLE_FASK_EventsErr = "FASK_EventsErr"; // Udalosti odvodu vyroby

        //Odvod Vyroby stavy
        public const string TABLE_MachineStateSet = "MachineStateSet"; // Udalosti odvodu vyroby
        public const string TABLE_MachineStateSetHistory = "MachineStateSetHistory"; // Udalosti odvodu vyroby
        public const string TABLE_MachinesDefinition = "MachinesDefinition"; // Udalosti odvodu vyroby

        //Odvod Vyroby tiskove sablony
        public const string TABLE_FASK_FORMULARE = "FASK_FORMULARE"; // Udalosti odvodu vyroby

        //Ukolovani
        public const string TABLE_CZ_UKOL = "CZ_UKOL";
        public const string TABLE_CZ_UKOL_STATE = "CZ_UKOL_STATE";
        public const string TABLE_CZ_UKOL_UZIV = "CZ_UKOL_UZIV";
        public const string TABLE_CZ_UKOL_UZIV_HIST = "CZ_UKOL_UZIV_HIST";
        public const string TABLE_CZ_UKOL_COMPARE = "CZ_UKOL_COMPARE";
        public const string TABLE_CZMSTPWD_COMPARE = "CZMSTPWD_COMPARE";
        public const string TABLE_CZ_UKOL_UZIV_COMPARE = "CZ_UKOL_UZIV_COMPARE";
        //public const string TABLE_CZMSTPWD = "CZMSTPWD";

        //Vyroba
        public const string TABLE_Production_Sources = "Production_Sources";
        public const string TABLE_Production = "Production";
        public const string TABLE_CZPRO_VPH = "CZPRO_VPH";
        public const string TABLE_CZPRO_VPP = "CZPRO_VPP";
        public const string TABLE_FASK_Vyroba_TP = "FASK_Vyroba_TP";
        public const string TABLE_FASK_Vyroba_PVP = "FASK_Vyroba_PVP";
        public const string TABLE_FASK_Vyroba_PVH = "FASK_Vyroba_PVH";

        public const string TABLE_Operations = "Operations";
        public const string TABLE_Machines = "Machines";
        public const string TABLE_FASK_Machines = "FASK_Machines";

        public const string TABLE_VLoginsGroups = "VLoginsGroups";
        public const string TABLE_Groups = "Groups";
        public const string TABLE_Corrects = "Corrects";
        public const string TABLE_VMachinesOperations = "VMachinesOperations";




        //Planovani
        public const string TABLE_FASK_PLANOVANI_PARAMS_Name = "FASK_PLANOVANI_PARAMS_Name";
        public const string TABLE_FASK_PLANOVANI_PARAMS = "FASK_PLANOVANI_PARAMS";
        public const string TABLE_FASK_PLANOVANI = "FASK_PLANOVANI";
        public const string TABLE_FASK_PLANOVANI_View = "FASK_PLANOVANI_View";
    }
}
