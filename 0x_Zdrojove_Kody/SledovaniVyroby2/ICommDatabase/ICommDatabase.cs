using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ICommDatabase
{
    public interface ICommDatabase
    {
        void LoadConfiguration();

        void LoadConfiguration(string FileName);

        DSVyroba GetMachines();

        DSVyroba GetEvents();

        int Update_FASK_Events(object data);

        DSVyroba GetUserEvents();

        int DeleteEvent(int rowID);

        int DeleteUserEvent(int rowID);

        bool LogUser(string name, string pass);

        //Vlozeni do tabulky FASK_UserEvents
        bool UserEventsInsert(string loginid, string machineid, string statusID, string localConnection, string rez1, string rez2);

        //Vlozeni do tabulky FASK_Events
        bool EventsInsert(
            string loginid, 
            ref SqlTransaction transaction, 
            string connectionString, 
            decimal qty, 
            decimal qtyreal, 
            string description, 
            string barcodeReaded, 
            string barcodeSended, 
            string zakazka,
            string popis, 
            string reportType, 
            string IDO, 
            string scan1, 
            string scan2, 
            string scan3, 
            string sensor, 
            string material, 
            string machineID,
            string VPH,
            int? VPPol,
            string EAN_IS,
            string IS_ID,
            string NMBRPAL,
            int? status,
            decimal QTYPACK,
            string PackType,
            decimal? WEIGHT,
            byte BarcodeT,
            string REZ_1,
            string REZ_2,
            string REZ_3,
            string REZ_4,
            string REZ_5
            );


        //Vlozeni do tabulky uchovaavjici aktualni stav aplikace
        void CurrentStateInsert(DateTime dt, ref SqlTransaction transaction, string connectionString, string machine, bool operationFree, string operationBarcode, string scan1res, string scan2res, string scan3res, string sensorValue, string orderNumber, string material, string polozka);

        int EventsErrInsert(string loginid, string LocalConnectionString, decimal qty, decimal qtyreal, string description, string barcodeReaded, string barcodeSended, string zakazka, string popis, string reportType, string IDO, string scan1, string scan2, string scan3, string sensor, string material);

        #region FASK_UserEventsErr

        int UserEventsErrInsert(int id, string loginid, string machineid, string statusID, string localConnection);

        #endregion

        #region CZPRO_VPH

        int Update_CZPRO_VPH(object data);

        int Delete_CZPRO_VPH();

        bool Exist_VPH(string SOPNUMBE);

        #endregion

        #region CZPRO_VPP

        int Update_CZPRO_VPP(object data);

        int Delete_CZPRO_VPP();

        DSVyroba.CZPRO_VPPRow Get_VPP(string BarcodeP, string SOPNUMBE);

        #endregion

        #region FASK_Logins

        int Update_FASK_Logins(object data);

        int Delete_FASK_Logins();

        #endregion

        #region FASK_Machines

        int Update_FASK_Machines(object data);

        int Delete_FASK_Machines();

        #endregion

        #region FASK_MachineType

        int Update_FASK_MachineType(object data);

        int Delete_FASK_MachineType();

        #endregion

        #region FASK_Operations_Next

        int Update_FASK_Operations_Next(object data);

        int Delete_FASK_Operations_Next();

        #endregion

        #region FASK_Operations

        int Update_FASK_Operations(object data);

        int Delete_FASK_Operations();

        #endregion

        
    }

    public enum StatusTypesEnum
    {
        Login = 101,                //Přilogování uživatele
        Logout = 102,       	    //Odlogování uživatele
        AppStarted = 103,           //Aplikace spuštěna
        AppEnded = 104,             //Aplikace ukončena
        BarCodeRead = 201,          //Načtení čárového kódu
        BarCodeParse = 202,         //Rozdělení čárového kódu
        BarCodeBuild = 203,         //Sestavení čárového kódu
        BarCodeSendToPort = 204,    //Odeslání čárového kódu na vystup
        BarCodeSendToPrint = 205,   //Odeslání čárového kódu k tisku
        ConfirmQTYCanceled = 304,   //Potvrzení odvedených kusů - Zrušeno
        ConfirmQTYOK = 305,         //Potvrzení odvedených kusů - OK
        ConfirmMaterialCanceled = 306,  //Potvrzeni nesouhlasu materialu - Zruseno
        ConfirmMaterialOk = 307,    //Potvrzeni nesouhlasu materialu - Ok
        ConfirmOrderCanceled = 308, //Potvrzeni neaktualnosti zakazky - Zruseno
        ConfirmOrderOk = 309,       //Potvrzeni neaktualnosti zakazky - Ok
        //INFO: potvrzeni tisku se nyni nezobrazuje - zakomentovano, takze se neloguje.
        //ConfirmPrintCanceled = 306, //Potvrzení tisku - Zrušeno
        //ConfirmPrintOK = 307,       //Potvrzení tisku - OK
        //INFO: Pridane kody rady 40x pro counter
        InstructionReaded = 401,    //Precteni instrukce z citace
        InstructionSended = 402,    //Odeslani instrukce k citaci
        AdminDatabaseSynchUsr = 900,//Synchronizace administratorskych dat
        DatabaseSynchApp = 901,     //Synchronizace databáze - Aplikací
        DatabaseSynchUsr = 902,     //Synchronizace databáze - Uživatelem
        ConfigAppChanged = 903,     //Změna nastavení aplikace
        ConfigModChanged = 904      //Změna nastavení modulu
    }

    public class clsMachine
    {
        //ID
        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        //Typ
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        //Nazev
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        //Popis
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        //Koeficient
        private decimal _koeficient;
        public decimal Koeficient
        {
            get { return _koeficient; }
            set { _koeficient = value; }
        }

        public override string ToString()
        {
            //return base.ToString();
            return "<" + this.ID + "> " + this.Name;
        }

    }
}
