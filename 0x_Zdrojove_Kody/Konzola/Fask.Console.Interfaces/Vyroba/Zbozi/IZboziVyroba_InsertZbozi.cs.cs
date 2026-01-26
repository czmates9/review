using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Zbozi
{
    public interface IZboziVyroba_InsertZbozi : IZboziVyroba
    {
                int InsertZbozi(
                    string ITEMNMBR, 
                    string ITEMDESC, 
                    string VNDITNUM, 
                    string CZ_CarKod, 
                    string LOCNCODE, 
                    string SKL_ID, 
                    decimal QTY, 
                    decimal? QTYPACK, 
                    string MJ, 
                    string DMJ, 
                    decimal? TAXRATE, 
                    decimal? PRICE0, 
                    decimal? PRICE1, 
                    decimal? PRICE2, 
                    decimal? PRICE3, 
                    decimal? PRICE4, 
                    decimal? PRICE5, 
                    byte CZ_SerNum_Track, 
                    short CZ_SerNum_Delka, 
                    byte CZ_Rez1_Track, 
                    byte CZ_Rez2_Track, 
                    byte CZ_Rez3_Track, 
                    byte CZ_Rez4_Track, 
                    string REZ1, 
                    string ITEMCODE, 
                    string ODB_ID, 
                    float TIMEPREP, 
                    float TIMEUNIT, 
                    DateTime? TIMEFROM, 
                    DateTime? TIMETO, 
                    System.DateTime LSTMod, 
                    string loginid, 
                    int TIMEMODE);
    }

    }

