using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes
{
    public class VyrPrikaz
    {

        private int _qty_Pytlu_NP = 0;
        public int QTY_Pytlu_NP
        {
            get { return _qty_Pytlu_NP; }
            set { _qty_Pytlu_NP = value; }
        }

        private string _vph_SOPNUMBER = string.Empty;
        public string VPH_SOPNUMBER
        {
            get { return _vph_SOPNUMBER; }
            set { _vph_SOPNUMBER = value; }
        }


        private int? _vpp_row_ID = null;
        public int? VPP_row_ID
        {
            get { return _vpp_row_ID; }
            set { _vpp_row_ID = value; }
        }

        private string _vpp_row_BarcodeP = string.Empty;
        public string VPP_row_BarcodeP
        {
            get { return _vpp_row_BarcodeP; }
            set { _vpp_row_BarcodeP = value; }
        }

        private string _vpp_row_ITEMNMBR = string.Empty;
        public string VPP_row_ITEMNMBR
        {
            get { return _vpp_row_ITEMNMBR; }
            set { _vpp_row_ITEMNMBR = value; }
        }

        private string _barcodeReaded = string.Empty;
        public string BarcodeReaded
        {
            get { return _barcodeReaded; }
            set { _barcodeReaded = value; }
        }

        private string _barcodeSended = string.Empty;
        public string BarcodeSended
        {
            get { return _barcodeSended; }
            set { _barcodeSended = value; }
        }
    }

    public class CalcModulo
    {
        public static string CountParity_Modulo10(string barcode)
        {
            int sumLiche = 0;
            int sumSude = 0;

            // index : hodnota
            // 0,1 : "0"
            // 2-19 : cisla
            // 20 : kontrolni cislo
            for (int i = 2; i < barcode.Length; i++)
            {
                if ((i + 1) % 2 == 0) //Sude poradove cislo
                    sumSude += int.Parse(barcode[i].ToString());
                else //je liche poradove cislo
                    sumLiche += int.Parse(barcode[i].ToString());
            }

            return ((10 - (sumLiche * 3 + sumSude) % 10) % 10).ToString();

        }
    }
}
