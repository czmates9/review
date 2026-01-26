using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface ICiselniky : 
        ISkladLokace_LokaceVariantySortiment2,
        ISkladLokace_LokaceVariantySortiment2_DeleteSkladLokace_LokaceVariantySortiment,
        ISkladLokace_LokaceVariantySortiment2_GetFiltrovanySkladLokace_LokaceVariantySortiment,
        ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortiment,
        ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr,
        ISkladLokace_LokaceVariantySortiment2_InsertSkladLokace_LokaceVariantySortiment,
        ISkladLokace_LokaceVariantySortiment2_InsertSkladLokace_LokaceVariantySortiment_Row,
        ISkladLokace_LokaceVariantySortiment2_UpdateSkladLokace_LokaceVariantySortiment,

        //IUzivatele2,
        //IUzivatele2_DeleteUzivatel,
        //IUzivatele2_GetFiltrovaneUzivatele,
        //IUzivatele2_GetUzivatelByID,
        //IUzivatele2_GetUzivatele,
        //IUzivatele2_InsertUzivatel,
        //IUzivatele2_UpdateUzivatel,

        ISklady2,
        ISklady2_GetFiltrovaneSklady,
        ISklady2_GetSkladByID,
        ISklady2_GetSklady,
        ISklady2_InsertSklad,
        ISklady2_UpdateSklad,
        ISklady2_DeleteSklad,

        IZbozi2,
        IZbozi2_DeleteZbozi,
        IZbozi2_GetFiltrovaneZbozi,
        IZbozi2_GetZbozi,
        IZbozi2_GetZboziByID,
        IZbozi2_InsertZbozi,
        IZbozi2_UpdateZbozi,

        IStrediska2,
        IStrediska2_DeleteStredisko,
        IStrediska2_GetStrediska,
        IStrediska2_InsertStredisko,
        IStrediska2_UpdateStredisko,
        IStrediska2_GetStrediskoByID,

        ISkladLokace_Mapa2,
        ISkladLokace_Mapa2_DeleteSkladLokace_Mapa,
        ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa,
        ISkladLokace_Mapa2_GetSkladLokace_Mapa,
        ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode,
        ISkladLokace_Mapa2_InsertSkladLokace_Mapa,
        ISkladLokace_Mapa2_UpdateSkladLokace_Mapa,

        IPracovnici2,
        IPracovnici2_DeletePracovnici,
        IPracovnici2_GetFiltrovanePracovniky,
        IPracovnici2_GetPracovnici,
        IPracovnici2_GetPracovnikByID,
        IPracovnici2_InsertPracovnici,
        IPracovnici2_UpdatePracovnici,

        IOdberatele2,
        IOdberatele2_DeleteOdberatel,
        IOdberatele2_GetOdberatelByID,
        IOdberatele2_GetOdberatele,
        IOdberatele2_InsertOdberatel,
        IOdberatele2_UpdateOdberatel,

        ISkladLokace_LokaceTypy2,
        ISkladLokace_LokaceTypy2_DeleteSkladLokace_LokaceTypy,
        ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy,
        ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy,
        ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType,
        ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy,
        ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy
    {
    }
}
