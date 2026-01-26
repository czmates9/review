using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Vyroba_PV_Filtr : FilterBase
    {


        /// <summary>
        /// sopnumbe
        /// </summary>
        public string OBJ { get; set; }


        /// <summary>
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }


        public string Firma { get; set; }

        public string Kod { get; set; }


        public string SOPNUMBE { get; set; }


        public DateTime DatumOD { get; set; }
        public DateTime DatumDO { get; set; }

        public bool DatumOD_Check { get; set; }
        public bool DatumDO_Check { get; set; }

        public bool NEzaplanovane { get; set; }
        public bool zaplanovane { get; set; }


        public DateTime? Zaplanovano_DO { get; set; }
        public DateTime? Zaplanovano_OD { get; set; }

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? Dateeve_TimeVariant { get; set; }
        public string Zaplanovano_TimeVariant { get; set; }
    }
}
