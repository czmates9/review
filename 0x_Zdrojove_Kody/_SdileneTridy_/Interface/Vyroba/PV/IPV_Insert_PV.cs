using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Insert_PV : IPV
    {

        bool Insert(string OBJ_NMBR,
                    string OBJ_DESC,
                    string OBJ_TYPE,
                    string OBJ_COMPANY,
                    DateTime? OBJ_DATE_FROM,
                    DateTime? OBJ_DATE_TO,
                    int? OBJ_ORD,
                    int? OBJ_ITEM_ORD,
                    string ITEMNMBR,
                    string ITEMDESC,
                    string ITEMCODE,
                    decimal QTY,
                    DateTime? DATE_ZAPLANOVANI,
                    bool VP_PRPS,
                    decimal? VP_PRPS_QTY,
                    string VP_PRPS_SOPNUMBE,
                    decimal? VP_PRDCT_QTY,
                    int? USERID,
            int? Ref_PVH);
    }
}
