using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Classes_Vyroba
{
    public enum BlokaceTyp
    {
        /// <summary>
        /// nastavuje se priznak terminalID na cislo terminalu, ktery o blokaci zada
        /// </summary>
        Blokovat,
        /// <summary>
        /// nastavuje se priznak terminalID na hodnotu 0
        /// </summary>
        OdBlokovat,
        /// <summary>
        /// Nastavuje se priznak terminalID na hodnotu cislo terminalu + 100
        /// </summary>
        Uzavrit
    }

    public struct VyrobniPrikazHlavicka
    {
        public int COUNTENTRIES;
        public string SOPNUMBE;

        public VyrobniPrikazHlavicka(int countentries, string sopnumbe)
        {
            this.COUNTENTRIES = countentries;
            this.SOPNUMBE = sopnumbe;
        }
    }

    public enum ProductionState
    {
        Unknown,
        Priprava_Start,
        Priprava_Stop,
        Odvod_Start,
        Odvod_Stop,
        Korekce_Start,
        Korekce_Stop
    }

    public class ProductionObject
    {
        public string operaceID;
        public string terminalID;
        public ProductionState stavOperace;
        public DateTime casOperace;
        public decimal mnozstviVyrobeno;
        public decimal mnozstviZmetek;
        public string popisZmetek;
    }
}
