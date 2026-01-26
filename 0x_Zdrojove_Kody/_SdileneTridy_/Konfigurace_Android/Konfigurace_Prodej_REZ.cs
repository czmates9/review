using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{
    public class Konfigurace_Prodej_REZ
    {

        private bool _cislo = true;
        [Popis("Je číslo ")]
        public bool Cislo { get => _cislo; set => _cislo = value; }

        private bool _povinne = true;
        [Popis("Povinné ")]
        public bool Povinne { get => _povinne; set => _povinne = value; }

        private bool _pamatovat = true;
        [Popis("Pamatovat ")]
        public bool Pamatovat { get => _pamatovat; set => _pamatovat = value; }

        private string _PROD_NAME = "REZ";
        [Popis("REZ1 Název ")]
        public string PROD_NAME { get => _PROD_NAME; set => _PROD_NAME = value; }

        //private bool _isVisible = true;
        //[Popis("Viditelné ")]
        //public bool isVisible { get => _isVisible; set => _isVisible = value; }

    }
}