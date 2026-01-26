using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace MES_Android
{
    public class KonfiguraceTexty
    {
        private bool _exportZbozi = false;
        [Text("Zapnout export číselniku zásob ")]
        public bool ExportZbozi { get => _exportZbozi; set => _exportZbozi = value; }

        private bool _exportOdberatele = false;
        [Text("Zapnout export číselniku odběratelů ")]
        public bool ExportOdberatele { get => _exportOdberatele; set => _exportOdberatele = value; }

        private bool _exportSklady = false;
        [Text("Zapnout export číselniku skladů ")]
        public bool ExportSklady { get => _exportSklady; set => _exportSklady = value; }

        private bool _exportMeny = false;
        [Text("Zapnout export číselniku měn ")]
        public bool ExportMeny { get => _exportMeny; set => _exportMeny = value; }

        private bool _exportStrediska = false;
        [Text("Zapnout export číselniku středisek ")]
        public bool ExportStrediska { get => _exportStrediska; set => _exportStrediska = value; }

        private bool _exportPracovnici = false;
        [Text("Zapnout export číselniku pracovníků ")]
        public bool ExportPracovnici { get => _exportPracovnici; set => _exportPracovnici = value; }


       // private Konfigurace_Prodej _prodej = new Konfigurace_Prodej();
        //public Konfigurace_Prodej Prodej { get => _prodej; set => _prodej = value; }

        //private Konfigurace_Vyroba _vyroba = new Konfigurace_Vyroba();
        //public Konfigurace_Vyroba Vyroba { get => _vyroba; set => _vyroba = value; }

    }

    [AttributeUsage(AttributeTargets.All)]
    public class TextAttribute : Attribute
    {
        public readonly string Text;

        public TextAttribute(string text) 
        {
            this.Text = text;
        }        
    }
}