using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Console.Interfaces.DataObalka
{
    class DataObalka_VyrabenePolozky : DataSet
    {
        public DataTable VyrabenePolozky { get; private set; }

        public DataObalka_VyrabenePolozky()
        {
            VyrabenePolozky = new DataTable("VyrabenePolozky");

            // Sloupce z tabulky CZPRO_VPH
            VyrabenePolozky.Columns.Add("VPH_CountEntries", typeof(int));
            VyrabenePolozky.Columns.Add("VPH_SOPNUMBE", typeof(string));
            VyrabenePolozky.Columns.Add("SOPTYPE", typeof(string));
            VyrabenePolozky.Columns.Add("SOPDESC", typeof(string));
            VyrabenePolozky.Columns.Add("VNDDOCNMH", typeof(string));
            VyrabenePolozky.Columns.Add("BarcodeH", typeof(string));
            VyrabenePolozky.Columns.Add("VPH_LOCNCODE", typeof(string));
            VyrabenePolozky.Columns.Add("DateProd", typeof(short));
            VyrabenePolozky.Columns.Add("Rez1", typeof(string));
            VyrabenePolozky.Columns.Add("Rez2", typeof(string));
            VyrabenePolozky.Columns.Add("VPH_TermID", typeof(byte));
            VyrabenePolozky.Columns.Add("VPH_LSTMod", typeof(DateTime));
            VyrabenePolozky.Columns.Add("VPH_DEX_ROW_ID", typeof(int));
            VyrabenePolozky.Columns.Add("Active", typeof(byte));
            VyrabenePolozky.Columns.Add("USERID", typeof(int));

            // Sloupce z tabulky CZPRO_VPP
            VyrabenePolozky.Columns.Add("VPP_CountEntries", typeof(int));
            VyrabenePolozky.Columns.Add("ITEMNMBR", typeof(string));
            VyrabenePolozky.Columns.Add("ITEMTYPE", typeof(string));
            VyrabenePolozky.Columns.Add("ITEMDESC", typeof(string));
            VyrabenePolozky.Columns.Add("ITEMMJ", typeof(string));
            VyrabenePolozky.Columns.Add("VNDDOCNMP", typeof(string));
            VyrabenePolozky.Columns.Add("VNDITNUM", typeof(string));
            VyrabenePolozky.Columns.Add("ORD", typeof(int));
            VyrabenePolozky.Columns.Add("BarcodeP", typeof(string));
            VyrabenePolozky.Columns.Add("VPP_LOCNCODE", typeof(string));
            VyrabenePolozky.Columns.Add("QTYSHPPD", typeof(decimal));
            VyrabenePolozky.Columns.Add("QTYDOKON", typeof(decimal));
            VyrabenePolozky.Columns.Add("QTYPACK", typeof(decimal));
            VyrabenePolozky.Columns.Add("QTYPACKMJ", typeof(string));
            VyrabenePolozky.Columns.Add("TIMEMODE", typeof(int));
            VyrabenePolozky.Columns.Add("TIMEPREP", typeof(float));
            VyrabenePolozky.Columns.Add("TIMEUNIT", typeof(float));
            VyrabenePolozky.Columns.Add("DtProdT", typeof(byte));
            VyrabenePolozky.Columns.Add("DtProdL", typeof(short));
            VyrabenePolozky.Columns.Add("SerNumT", typeof(byte));
            VyrabenePolozky.Columns.Add("SerNumL", typeof(short));
            VyrabenePolozky.Columns.Add("VerT", typeof(byte));
            VyrabenePolozky.Columns.Add("VerL", typeof(short));
            VyrabenePolozky.Columns.Add("VPP_TermID", typeof(byte));
            VyrabenePolozky.Columns.Add("VPP_LSTMod", typeof(DateTime));
            VyrabenePolozky.Columns.Add("VPP_DEX_ROW_ID", typeof(int));
            VyrabenePolozky.Columns.Add("Realization_Start", typeof(DateTime));
            VyrabenePolozky.Columns.Add("Realization_Stop", typeof(DateTime));
            VyrabenePolozky.Columns.Add("BarcodeT", typeof(byte));
            VyrabenePolozky.Columns.Add("CZ_REZ1_Track", typeof(byte));
            VyrabenePolozky.Columns.Add("CZ_REZ2_Track", typeof(byte));
            VyrabenePolozky.Columns.Add("CZ_REZ3_Track", typeof(byte));
            VyrabenePolozky.Columns.Add("CZ_REZ4_Track", typeof(byte));
            VyrabenePolozky.Columns.Add("CZ_REZ5_Track", typeof(byte));
            VyrabenePolozky.Columns.Add("WEIGHT_TARA", typeof(decimal));
            VyrabenePolozky.Columns.Add("WEIGHT_NETTO", typeof(decimal));
            VyrabenePolozky.Columns.Add("WEIGHT_TOL_PLUS", typeof(decimal));
            VyrabenePolozky.Columns.Add("WEIGHT_TOL_MINUS", typeof(decimal));

            Tables.Add(VyrabenePolozky);
        }


    }
}
