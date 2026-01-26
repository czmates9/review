using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam Uzivatelu.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Uzivatele GetUzivatele();

        /// <summary>
        /// Vraci uzivatele podle ID.
        /// </summary>
        /// <param name="id">ID uzivatele.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow GetUzivatelByID(int id);

        /// <summary>
        /// Vraci filtrovane uzivatele.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Console.Interfaces.DataSets.Uzivatele GetFiltrovaneUzivatele(Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr);

        /// <summary>
        /// Smaže uzivatele.
        /// </summary>
        /// <param name="id">ID uzivatele.</param>
        /// <returns></returns>
        bool DeleteUzivatel(string id);

        /// <summary>
        /// Aktualizuje informace o Uzivateli.
        /// </summary>
        /// <param name="zdrojRow">Uzivatel, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateUzivatel(Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow);

        /// <summary>
        /// Vloží do DB nový záznam uzivatele.
        /// </summary>
        /// <param name="zdrojRow">Uzivatel, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertUzivatel(Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow);
    }
}
