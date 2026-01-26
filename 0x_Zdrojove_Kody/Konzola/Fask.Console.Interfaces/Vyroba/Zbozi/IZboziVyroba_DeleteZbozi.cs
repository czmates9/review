using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Zbozi
{
    public interface IZboziVyroba_DeleteZbozi : IZboziVyroba
    {
        int DeleteZbozi(string ITEMNMBR);
    }

    }

