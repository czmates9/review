using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICommDatabase
{
    public interface ISQLDatabase
    {
        void LoadConfiguration();
        void LoadConfiguration(string FileName);

        #region FASK_Events

        DSVyroba.FASK_EventsDataTable GetFASK_Events_ByStatus( int MachineID, int Status, List<string> descFilter);

        Fask.WEBAPI.API_BusinessObjects.FASK_Events_row SledovaniVyroby_data(int MachineID, Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events);

        bool SV_ZmenaStatusu(int MachineID, int status);

        bool FaskEvents_WriteToRow(int MachineID, Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o, int StatusNew, string desc);

        bool FaskEvents_Update(int MachineID, Guid? G, int StatusNew);

        #region Archivace

        int DeaktivaceFE(int MachineID, int Status, DSVyroba.FASK_Events_archivaceDataTable _dataKArchivaci, int BocediID);

        DSVyroba.FASK_Events_archivaceDataTable LoadFE(int MachineID, Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events);

        #endregion

        #endregion

        #region FASK_Logins

        bool Fill_FASK_Logins(DSVyroba.FASK_LoginsDataTable dataTable, int MachineID, bool ClearBeforeFill = true);

        #endregion

        #region FASK_Machine

        bool Fill_FASK_Machine(DSVyroba.FASK_MachinesDataTable dataTable, int MachineID, bool ClearBeforeFill = true);

        #endregion

        #region FASK_MachineType

        bool Fill_FASK_MachineType(DSVyroba.FASK_MachineTypeDataTable dataTable, int MachineID, bool ClearBeforeFill = true);

        #endregion

        #region CZPRO_VPP

        DSVyroba.CZPRO_VPPDataTable getVyroba_CZPRO_VPP(int MachineID);

        #endregion

        #region CZPRO_VPH

        DSVyroba.CZPRO_VPHDataTable getVyroba_CZPRO_VPH(int MachineID);

        #endregion

        #region FASK_Events

        int FASK_EventsInsert
            (
            string loginid,
            string machineid,
            System.DateTime dateeve,
            decimal qty,
            decimal qtyReal,
            string description,
            string barcodeReaded,
            string barcodeSended,
            string zakazka,
            string popis,
            System.Guid faskGUID,
            string reportType,
            string IDO,
            string scan1,
            string scan2,
            string scan3,
            string sensor,
            string material,
            string VPH,
            int? VPPol,
            string EAN_IS,
            string IS_ID,
            int? status,
            string NMBRPAL,
            global::System.Guid? productionGuid,
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

         int? FASK_Events_CountGUID( Guid Guid, int MachineID);

        #endregion

        #region FASK_UserEvents

         int? FASK_UserEvents_CountGUID( Guid Guid, int MachineID);

         bool FASK_UserEventsInsert(
            string loginid,
            string machineid,
            System.DateTime dateeve,
            string statusid,
            System.Guid faskGUID,
            string rez_1,
            string rez_2
            );
     

        #endregion

        #region FASK_Operations

       bool Fill_FASK_Operations( DSVyroba.FASK_OperationsDataTable dataTable, int MachineID, bool ClearBeforeFill);

        #endregion

        #region FASK_Operations_Next

        bool Fill_FASK_Operations_Next( DSVyroba.FASK_Operations_NextDataTable dataTable, int MachineID, bool ClearBeforeFill);

        #endregion

        #region Pomocne metody z AGRO modulu

        string ReturnSarze(int MachineID, string smenaID, string userID, string linkaID);

        bool ReturnID(int MachineID, string inID);

        bool ReturnHeslo(int MachineID, string inHESLO, string inID);

        #endregion

        #region FASK_EventsErr

        void SV_Logs_insert(int MachineID ,DSVyroba.FASK_EventsErrDataTable bo);

        #endregion
    }
}
