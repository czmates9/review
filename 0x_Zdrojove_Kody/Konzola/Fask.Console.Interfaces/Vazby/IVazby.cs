using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Production.DataServices;

namespace Fask.Console.Interfaces.Vazby
{

    /// <summary>
    /// Interface lokacniho mechanismu.
    /// </summary>
    public interface IVazby : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// ITEMNMBR pro vyhledavani vazeb materialu k vyrobku
        /// </summary>
        string ITEMNMBR_Def { get; set; }

        /// <summary>
        /// rowvyrobek_ID_L pro vyhledavani vazeb materialu k vyrobku
        /// </summary>
        string rowvyrobek_ID_L { get; set; }


        Production.DataServices.VyrobaDataSet GetFiltrovanyVazbyMaterialy(Fask.Console.Interfaces.Classes.VazbyMaterialyFiltr filtr);

        Production.DataServices.VyrobaDataSet GetFiltrovanyVazbyVyrobky(Fask.Console.Interfaces.Classes.VazbyMaterialyFiltr filtr);

        Production.DataServices.KonzolaDataSet GetFiltrovanyVazbyAddVyrobky(Fask.Console.Interfaces.Classes.VazbyMaterialyFiltr filtr);

    }
}
