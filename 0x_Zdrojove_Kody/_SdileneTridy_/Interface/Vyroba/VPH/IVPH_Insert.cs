using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPH
{
    public interface IVPH_Insert : IVPH
    {

        //void Insert_Row(
        //    int CountEntries, 
        //    string SOPNUMBE, 
        //    string SOPTYPE,
        //    string SOPDESC,
        //    string VNDDOCNMH,
        //    string BarcodeH,
        //    string LOCNCODE,
        //    short DateProd,
        //    string Rez1,
        //    string Rez2,
        //    byte TermID,
        //    DateTime LSTMod,
        //    byte Active);

        void Insert(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row);

    }
}
