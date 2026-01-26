using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_TiskoveSablony
{
    public interface IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony : IOdvod_TiskoveSablony
    {
        Fask.Interfaces.DataSets.Vyroba TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr);
       string TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony_Path(Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr);
    }
}
