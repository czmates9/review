using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace MES_Android
{
    public class Konfigurace
    {
        private bool _exportZbozi = false;
        [Popis("Zapnout export číselniku zásob ")]
        public bool ExportZbozi { get => _exportZbozi; set => _exportZbozi = value; }

        private bool _exportOdberatele = false;
        [Popis("Zapnout export číselniku odběratelů ")]
        public bool ExportOdberatele { get => _exportOdberatele; set => _exportOdberatele = value; }

        private bool _exportSklady = false;
        [Popis("Zapnout export číselniku skladů ")]
        public bool ExportSklady { get => _exportSklady; set => _exportSklady = value; }

        private bool _exportMeny = false;
        [Popis("Zapnout export číselniku měn ")]
        public bool ExportMeny { get => _exportMeny; set => _exportMeny = value; }

        private bool _exportStrediska = false;
        [Popis("Zapnout export číselniku středisek ")]
        public bool ExportStrediska { get => _exportStrediska; set => _exportStrediska = value; }

        private bool _exportPracovnici = false;
        [Popis("Zapnout export číselniku pracovníků ")]
        public bool ExportPracovnici { get => _exportPracovnici; set => _exportPracovnici = value; }

        private bool _exporttUzivatele = false;
        [Popis("Zapnout export číselniku uživatelů ")]
        public bool ExportUzivatele { get => _exporttUzivatele; set => _exporttUzivatele = value; }

        private bool _exportPohyby = false;
        [Popis("Zapnout export číselniku Typu dokladů ")]
        public bool ExportPohyby { get => _exportPohyby; set => _exportPohyby = value; }

        private bool _exportLokace = false;
        [Popis("Zapnout export číselniku lokací ")]
        public bool ExportLokace { get => _exportLokace; set => _exportLokace = value; }

        private bool _generovatUzivatele = false;
        [Popis("Zapnout generování číselniku uživatelů ")]
        public bool GenerovatUzivatele { get => _generovatUzivatele; set => _generovatUzivatele = value; }
        
        private bool _generovatPohyby = false;
        [Popis("Zapnout generování číselniku Pohyby ")]
        public bool GenerovatPohyby { get => _generovatPohyby; set => _generovatPohyby = value; }
       
        private bool _generovatZbozi = false;
        [Popis("Zapnout generování číselniku Zbozi ")]
        public bool GenerovatZbozi { get => _generovatZbozi; set => _generovatZbozi = value; }
       
        private bool _generovatOdberatele = false;
        [Popis("Zapnout generování číselniku Odberatele ")]
        public bool GenerovatOdberatele { get => _generovatOdberatele; set => _generovatOdberatele = value; }
       
        private bool _generovatSklady = false;
        [Popis("Zapnout generování číselniku Sklady ")]
        public bool GenerovatSklady { get => _generovatSklady; set => _generovatSklady = value; }
       
        private bool _generovatMeny = false;
        [Popis("Zapnout generování číselniku Meny ")]
        public bool GenerovatMeny { get => _generovatMeny; set => _generovatMeny = value; }
       
        private bool _generovatStrediska = false;
        [Popis("Zapnout generování číselniku Strediska ")]
        public bool GenerovatStrediska { get => _generovatStrediska; set => _generovatStrediska = value; }
       
        private bool _generovatPracovnici = false;
        [Popis("Zapnout generování číselniku Pracovnici ")]
        public bool GenerovatPracovnici { get => _generovatPracovnici; set => _generovatPracovnici = value; }
       
        private bool _generovatLokace = false;
        [Popis("Zapnout generování číselniku Lokace ")]
        public bool GenerovatLokace { get => _generovatLokace; set => _generovatLokace = value; }


        private Konfigurace_Prodej _prodej = new Konfigurace_Prodej();
        public Konfigurace_Prodej Prodej { get => _prodej; set => _prodej = value; }

        private Konfigurace_Vyroba _vyroba = new Konfigurace_Vyroba();
        public Konfigurace_Vyroba Vyroba { get => _vyroba; set => _vyroba = value; }

        private Konfigurace_Ostatni _ostatni = new Konfigurace_Ostatni();
        public Konfigurace_Ostatni Ostatni { get => _ostatni; set => _ostatni = value; }

        private Konfigurace_Inventura _inventura = new Konfigurace_Inventura();
        public Konfigurace_Inventura Inventura { get => _inventura; set => _inventura = value; }

        private Konfigurace_Vydej _vydej = new Konfigurace_Vydej();
        public Konfigurace_Vydej Vydej { get => _vydej; set => _vydej = value; }

        private Konfigurace_Prijem _prijem = new Konfigurace_Prijem();
        public Konfigurace_Prijem Prijem { get => _prijem; set => _prijem = value; }

        private Konfigurace_Expedice _expedice = new Konfigurace_Expedice();
        public Konfigurace_Expedice Expedice { get => _expedice; set => _expedice = value; }

    }

    [AttributeUsage(AttributeTargets.All)]
    public class PopisAttribute : Attribute
    {
        public readonly string Popis;

        public PopisAttribute(string popis) 
        {
            this.Popis = popis;
        }        
    }
}