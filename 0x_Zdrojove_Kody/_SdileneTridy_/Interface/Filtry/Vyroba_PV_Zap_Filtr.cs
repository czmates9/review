using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Vyroba_PV_Zap_Filtr : FilterBase
    {


        /// <summary>
        /// Nezaplanovane
        /// </summary>
        public bool Nezaplanovane { get; set; }

        public DateTime OD_DatumOD { get; set; }
        public DateTime OD_DatumDO { get; set; }

        public bool OD_DatumOD_Check { get; set; }
        public bool OD_DatumDO_Check { get; set; }

        public DateTime DO_DatumOD { get; set; }
        public DateTime DO_DatumDO { get; set; }

        public bool DO_DatumOD_Check { get; set; }
        public bool DO_DatumDO_Check { get; set; }

        public DateTime ZAP_DatumOD { get; set; }
        public DateTime ZAP_DatumDO { get; set; }

        public bool ZAP_DatumOD_Check { get; set; }
        public bool ZAP_DatumDO_Check { get; set; }

        public string OBJ { get; set; }

        public string Kod { get; set; }

        public string Firma { get; set; }
        public string FormaUhrady { get; set; }

        public string UserParam_1 { get; set; }
        public string UserParam_2 { get; set; }
        public string UserParam_3 { get; set; }
        public string UserParam_4 { get; set; }
        public string UserParam_5 { get; set; }

        
    }
}
