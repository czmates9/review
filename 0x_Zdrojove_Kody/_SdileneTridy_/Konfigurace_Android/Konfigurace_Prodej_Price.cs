using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{
    public class Konfigurace_Prodej_Price
    {
        private bool _price0IsWithTax = true;
        [Popis("Základní cena s daní ")]
        public bool Price0IsWithTax { get => _price0IsWithTax; set => _price0IsWithTax = value; }

        private bool _price1IsWithTax = true;
        [Popis("Cenová hladina 1 s daní ")]
        public bool Price1IsWithTax { get => _price1IsWithTax; set => _price1IsWithTax = value; }

        private bool _price2IsWithTax = true;
        [Popis("Cenová hladina 2 s daní ")]
        public bool Price2IsWithTax { get => _price2IsWithTax; set => _price2IsWithTax = value; }

        private bool _price3IsWithTax = true;
        [Popis("Cenová hladina 3 s daní ")]
        public bool Price3IsWithTax { get => _price3IsWithTax; set => _price3IsWithTax = value; }

        private bool _price4IsWithTax = true;
        [Popis("Cenová hladina 4 s daní ")]
        public bool Price4IsWithTax { get => _price4IsWithTax; set => _price4IsWithTax = value; }

        private bool _price5IsWithTax = true;
        [Popis("Cenová hladina 5 s daní ")]
        public bool Price5IsWithTax { get => _price5IsWithTax; set => _price5IsWithTax = value; }


        private bool _priceIsWithTaxEnable = true;
        [Popis("Povolit ")]
        public bool PriceIsWithTaxEnable { get => _priceIsWithTaxEnable; set => _priceIsWithTaxEnable = value; }

        private bool _priceIsWithTax = true;
        [Popis("Výstupní cena s daní ")]
        public bool PriceIsWithTax { get => _priceIsWithTax; set => _priceIsWithTax = value; }
    }
}