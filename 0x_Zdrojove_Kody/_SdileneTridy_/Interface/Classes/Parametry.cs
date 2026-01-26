using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Classes
{
    public class Parametry
    {
        private bool _POHODA_E1;
        public bool POHODA_E1
        {
            get { return _POHODA_E1; }
            set { _POHODA_E1 = value; }
        }

        private bool _PovolZaporneZasoby;
        public bool PovolZaporneZasoby
        {
            get { return _PovolZaporneZasoby; }
            set { _PovolZaporneZasoby = value; }
        }



        public Parametry(bool POHODA_E1, bool PovolZaporneZasoby)
        {
            _POHODA_E1 = POHODA_E1;
            _PovolZaporneZasoby = PovolZaporneZasoby;
        }


    }
}
