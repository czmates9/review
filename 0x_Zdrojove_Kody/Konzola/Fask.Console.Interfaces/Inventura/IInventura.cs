using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Inventura
{
    public interface IInventura : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam inventurnich davek v tabulce CZMST_I1H
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Inventura GetHlavicky();

        /// <summary>
        /// Vraci cislo davky podle ID
        /// </summary>
        /// <param name="countentries"></param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Inventura.CZMST_I1HRow GetHlavickaByID(int countentries);

        /// <summary>
        /// Vraci data predlohy vcetne poctu nasnimanych polozek a pripadneho rozdilu ve vystupnich datech (czmst_i4)
        /// </summary>
        /// <param name="filtr">Zvoleny filtr.</param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Inventura GetFiltrovanaPredloha(Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr);

        /// <summary>
        /// Vraci nasnimana data vcetne dopocitani pozadovaneho a nasnimaneho mnozstvi.
        /// </summary>
        /// <param name="filtr">Zvoleny filtr.</param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Inventura GetFiltrovaneNasnimane(Fask.Console.Interfaces.Classes.InventuraNasnimaneListFiltr filtr);
    }
}
