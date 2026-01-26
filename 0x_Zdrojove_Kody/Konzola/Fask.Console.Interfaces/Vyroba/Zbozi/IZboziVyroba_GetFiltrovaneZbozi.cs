using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Zbozi
{
    public interface IZboziVyroba_GetFiltrovaneZbozi : IZboziVyroba
    {
        Fask.Console.Interfaces.DataSets.Vyroba GetFiltrovaneZbozi(Fask.Console.Interfaces.Classes.ZboziVyrobaListFiltr filtr);
   
    }
}
