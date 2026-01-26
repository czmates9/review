using Fask.API_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fask.Server.Interfaces.DataSets;

namespace Fask.Server.Interfaces.Vyroba
{
    public interface IVyroba_PostFaskEventsRow : IVyroba
    {
        FASK_Events GetFASKEventsRow(Filtr_FASK_Events filtr);

        void SetFaskEventsRow(FASK_Events FE_object);

        int Update_StatusByGuid(Filtr_UpdateStatus FE_object);

        FASK_Events GetFASK_Events_BySSCC(Filtr_FASK_Events filtr);

        FASK_Events Vrat_zaznam_By_SSCC(Filtr_FASK_Events filtr);

        FASK_Events Get_FE_Row(Filtr_FASK_Events filtr);

        List<FASK_Events> Get_FE_List(Filtr_FASK_Events filtr);

        Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable FE_archivace_Rows_data(Filtr_FASK_Events filtr);

        int FE_archivace_Rows_deaktivace_nakladka(Fask.Server.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable data_rows);

        int FE_archivace_Rows_deaktivace_vykladka(Fask.Server.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable data_rows);

        DataSets.Vyroba.MachinesDefinitionDataTable Konfigurace_ADAM_data();

        DataSets.Vyroba.FASK_EventsDataTable GetFASKEventsRows(Filtr_FASK_Events filtr);
    }
}
