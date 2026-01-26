using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{
    public class Konfigurace_Ostatni
    {

        private bool _generovatNenalezenouDavku = false;
        [Popis("Vytvoření nové dávky generováním z externího zdroje ")]
        public bool GenerovatNenalezenouDavku { get => _generovatNenalezenouDavku; set => _generovatNenalezenouDavku = value; }
    }
}