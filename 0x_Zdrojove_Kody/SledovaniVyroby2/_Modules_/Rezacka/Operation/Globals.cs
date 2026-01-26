using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    /// <summary>
    /// Trida s globalnimi promennymi pro volne operace
    /// </summary>
    class FreeGlobals:TogetherGlobals
    {
    }

    /// <summary>
    /// Trida s globalnimi promennymi pro vyrobni operace
    /// </summary>
    class ManuGlobals:TogetherGlobals
    {   
    }

    /// <summary>
    /// Trida s globalnimi promennymi pro obe operace
    /// </summary>
    class TogetherGlobals
    {
        /// <summary>
        /// Inicializace
        /// </summary>
        public void init()
        {
            _zakazka = _material = _polozka = string.Empty;
        }

        private string _zakazka = string.Empty;
        /// <summary>
        /// Zakazka
        /// </summary>
        public string Zakazka
        {
            get { return _zakazka; }
            set { _zakazka = value; }
        }

        private string _material = string.Empty;
        /// <summary>
        /// Material
        /// </summary>
        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        private string _polozka = string.Empty;
        /// <summary>
        /// Polozka
        /// </summary>
        public string Polozka
        {
            get { return _polozka; }
            set { _polozka = value; }
        }

        private uint _counter = 0;
        /// <summary>
        /// Polozka
        /// </summary>
        public uint Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }

        private uint _pastCounter = 0;
        /// <summary>
        /// Polozka
        /// </summary>
        public uint PastCounter
        {
            get { return _pastCounter; }
            set { _pastCounter = value; }
        }

        private bool _clearFlag = false;
        /// <summary>
        /// Priznak, ze se nulovalo
        /// </summary>
        public bool ClearFlag
        {
            get { return _clearFlag; }
            set { _clearFlag = value; }
        }
    }
}
