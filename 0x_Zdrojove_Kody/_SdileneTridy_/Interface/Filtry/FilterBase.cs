using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public abstract class FilterBase
    {
        public override string ToString()
        {
            return string.IsNullOrEmpty(_NazevFiltru) ? string.Empty : _NazevFiltru.Trim();
        }



        public string _NazevFiltru = string.Empty; 
        public string NazevFiltru 
        {
            get { return _NazevFiltru; }
            set { _NazevFiltru = value; }
        }

        public string NameFileDataGridView { get; set; }
    }
}
