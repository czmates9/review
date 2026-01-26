using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.StavSkladu
{
    /// <summary>
    /// Pouziva se pouze v MD Let
    /// </summary>
    public interface IStavSkladu : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vrací záznamy podle zadaného filtru.
        /// </summary>
        /// <param name="StavSkladuFiltr">Filtr záznamů.</param>
        /// <returns>Vyfiltrovaný stav skladu.</returns>
        void GetFiltrovanyStavSkladu(Fask.Console.Interfaces.Classes.StavSkladuListFiltr StavSkladuFiltr, ref Fask.Console.Interfaces.DataSets.StavSkladu StavSkladu);

        /// <summary>
        /// Vrací záznamy historie pohybů podle zadaného filtru.
        /// </summary>
        /// <param name="StavSkladuFiltr">Filtr záznamů.</param>
        /// <param name="StavSkladu">Vyfiltrovaná historie pohybů.</param>
        void GetFiltrovanyStavSkladuHistorie(Fask.Console.Interfaces.Classes.StavSkladuHistorieFiltr StavSkladuFiltr, ref Fask.Console.Interfaces.DataSets.StavSkladu StavSkladu);

        /// <summary>
        /// Vraci vsechny zaznamy stavu skladu.
        /// </summary>
        /// <returns>Dataset s kompletne nactenym stavem skladu.</returns>
        void GetStavSkladu(ref Fask.Console.Interfaces.DataSets.StavSkladu StavSkladu);

        /// <summary>
        /// Vraci vsechny zaznamy uzivatelu z tabulky czmstpwd.
        /// </summary>
        /// <returns>Dataset se vsemi uzivateli.</returns>
        void getUzivatele(ref Fask.Console.Interfaces.DataSets.StavSkladu StavSkladu);

        /// <summary>
        /// Vraci vsechny sklady z tabulky czmst093.
        /// </summary>
        /// <returns>Dataset se vsemi sklady.</returns>
        void GetSklady(ref Console.Interfaces.DataSets.StavSkladu StavSkladu);
    }
}
