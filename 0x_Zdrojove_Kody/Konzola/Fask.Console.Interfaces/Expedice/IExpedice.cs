using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Expedice
{
    public interface IExpedice : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam vsech hlavicek vydeje.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Expedice GetExpedicePolozky();

        /// <summary>
        /// Vraci hlavicky podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Expedice GetFiltrovaneExpedicePolozky(Fask.Console.Interfaces.Classes.ExpediceFormDodaciListyListFiltr filtr);
    }
}
