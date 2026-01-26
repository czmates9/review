using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fask.Server.Interfaces.DataSets;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba_PostFaskEventsRow : IMES
    {
        Fask.WEBAPI.API_BusinessObjects.FASK_Events_row GetFASKEventsRow(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        void SetFaskEventsRow(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_object);
        void UpdateProductionRow(Guid? g, decimal? vaha);

        int Update_StatusByGuid(Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus FE_object);

        Fask.WEBAPI.API_BusinessObjects.FASK_Events_row GetFASK_Events_BySSCC(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        Fask.WEBAPI.API_BusinessObjects.FASK_Events_row Vrat_zaznam_By_SSCC(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        Fask.WEBAPI.API_BusinessObjects.FASK_Events_row Get_FE_Row(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        Fask.WEBAPI.API_BusinessObjects.FASK_Events Get_FE_List(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable FE_archivace_Rows_data(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        int FE_archivace_Rows_deaktivace_nakladka(Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable data_rows);

        int FE_archivace_Rows_deaktivace_vykladka(Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable data_rows);

        Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable Konfigurace_ADAM_data(int ID_group);

        Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable GetFASKEventsRows(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr);

        int GetStatus(string ID);




        Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable Konfigurace_Mericich_Zarizeni_00(string IP);


    }
}
