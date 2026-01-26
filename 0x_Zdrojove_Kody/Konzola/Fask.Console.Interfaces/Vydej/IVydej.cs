using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vydej
{
    public interface IVydej : IVyrobaKonzola
    {

                /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam vsech hlavicek vydeje.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Vydej GetHlavicky();

        /// <summary>
        /// Vraci hlavicky podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Vydej GetFiltrovaneHlavicky(Fask.Console.Interfaces.Classes.VydejHlavickyFiltr filtr);

        /// <summary>
        /// vraci hlavicku podle CountEntries (nevraci se podle SOPNUMBE, kdyby nahodou doslo k stornu davky a byla tam vicekrat ...)
        /// </summary>
        /// <param name="sopnumbe"></param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Vydej.HlavickyRow GetHlavickaByCountEntries(int CountEntries);

        /// <summary>
        /// Zmeni prioritu davky podle cisla davky (kdyby byla napr. stornovana, ...).
        /// Zmena se uskutecni pouze v pripade, ze davka ma nastaven priznak CZ_Doslo = 0.
        /// </summary>
        /// <param name="countentries">Cislo davky.</param>
        /// <param name="priority">Priorita.</param>
        /// <returns></returns>
        bool UpdatePriorityDavka(int countentries, byte priority);

        /// <summary>
        /// Zmeni prioritu davky na osobu podle cisla davky (kdyby byla napr. stornovana, ...).
        /// Zmena se uskutecni pouze v pripade, ze davka ma nastaven priznak CZ_Doslo = 0.
        /// </summary>
        /// <param name="countentries">Cislo davky.</param>
        /// <param name="userID">ID uzivatele. Pokud je null, nastavi se na null.</param>
        /// <returns></returns>
        bool UpdatePriorityDavka(int countentries, int? userID);
    }
}
