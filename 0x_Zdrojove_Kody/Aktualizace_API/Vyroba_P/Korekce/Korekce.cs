using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Aktualizace_API.Korekce
{
    public class Korekce
    {
        //public Korekce()
        //{
        //    start = DateTime.Now;
        //    stop = DateTime.Now;
        //}

        //public Korekce(int id, string nazev)
        //    : this()
        //{
        //    this.id = id;
        //    this.nazev = nazev;
        //}

        //public Korekce(DateTime start, TimeSpan delka)
        //{
        //    this.start = start;
        //    this.delka = delka;
        //}

        public Korekce(int id, string nazev, DateTime start, TimeSpan delka, int? type)
        {
            this.id = id;
            this.nazev = nazev;
            this.start = start;
            this.delka = delka;
            this.type = type;
        }

        public Korekce(int id, string nazev, DateTime start, DateTime stop, int? type, string note)
        {
            this.id = id;
            this.nazev = nazev;
            this.start = start;
            this.stop = stop;
            this.type = type;
            this.note = note;
        }

        public DateTime dateeve = DateTime.Now;
        public int id = 2;
        public string nazev = string.Empty;
        public DateTime start;
        public DateTime stop;
        public TimeSpan delka
        {
            get { return stop - start; }
            set { stop = start + value; }
        }
        public int? type;
        public string note;
    }
}
