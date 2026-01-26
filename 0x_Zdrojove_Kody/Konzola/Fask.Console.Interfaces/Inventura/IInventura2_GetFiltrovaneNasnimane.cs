using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GetFiltrovaneNasnimane : IInventura2
    {
        /// <summary>
        /// Vraci nasnimana data vcetne dopocitani pozadovaneho a nasnimaneho mnozstvi.
        /// </summary>
        /// <param name="filtr">Zvoleny filtr.</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Inventura GetFiltrovaneNasnimane(Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr);
    }
}
