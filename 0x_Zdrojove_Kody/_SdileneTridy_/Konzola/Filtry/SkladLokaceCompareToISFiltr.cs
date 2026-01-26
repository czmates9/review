using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class SkladLokaceCompareToISFiltr : FilterBase
    {


     
        /// <summary>
        /// Sklad materialu
        /// </summary>
        public string SklID { get; set; }

        public bool V_0 { get; set; }
        public bool V_1 { get; set; }
        public bool V_2 { get; set; }
        public bool V_3 { get; set; }
        public bool V_4 { get; set; }
        public bool V_5 { get; set; }
        public bool V_6 { get; set; }


        public string ITEMCODE { get; set; }
        public bool ITEMCODE_L { get; set; }
        public bool ITEMCODE_R { get; set; }



        public string ITEMDESC { get; set; }
        public bool ITEMDESC_L { get; set; }
        public bool ITEMDESC_R { get; set; }


        public string SERLTNUM { get; set; }
        public bool SERLTNUM_L { get; set; }
        public bool SERLTNUM_R { get; set; }


    }
}
