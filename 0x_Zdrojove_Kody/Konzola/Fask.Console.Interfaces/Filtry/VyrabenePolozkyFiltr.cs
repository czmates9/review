using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Filtry
{
   public class VyrabenePolozkyFiltr : FilterBase
    {


        public VyrabenePolozkyFiltr() { }


        //public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow rowVPH;
        public Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable VyrabenePolozky_row;

        public byte? Active { get; set; }

        public string CountEntries;
        public string CountEntries_VPP;
        public string SOPNUMBE;
        public string DateProd;
        public string ITEMDESC;
        public string VNDITNUM;

        //public bool DatumDo;
        //public bool DatumOd;
        public DateTime? DatumDoValue;
        public DateTime? DatumOdValue;

        public bool OdvadeniVse;
        public bool KorekceVse;
        public bool OdvadeniNedokoncene;
        public bool KorekceNedokoncene;


        public bool JenNezrealizovane;

        public string DATEEVE_TimeVariant;

        public bool PouzeNeschvalene;

        public string TerminalID { get; set; }

        public short? DateProd_OD;
        public short? DateProd_DO;
        public bool Aktivni { get; set; }
        public bool Neaktivni { get; set; }
        public bool Ukonceno { get; set; }




    }
}
