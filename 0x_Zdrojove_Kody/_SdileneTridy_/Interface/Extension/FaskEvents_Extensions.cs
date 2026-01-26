using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Extension
{
    public static class FaskEvents_Extensions
    {
        public static List<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row> ToList_FASK_Events(this Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt)
        {

            try
            {
                List<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row> lst = new List<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row>();

                int cnt = 0;

                foreach (var item in dt.OrderByDescending(x => x.dateeve))
                {

                    cnt++;
                    if (cnt > 5)
                        break;

                    lst.Add(new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row() { 
                        
						id = item.id,
						loginid = item.loginid,
						machineid = item.machineid,
						dateeve = item.dateeve,
						qty = item.qty,
						qtyReal = item.qtyReal,
						description = item.description,
						barcodeReaded = item.barcodeReaded,
						barcodeSended = item.barcodeSended,
						zakazka = item.zakazka,
						popis = item.popis,
						faskGUID = item.faskGUID,
						reportType = item.reportType,
						isProcessed = item.IsisProcessedNull() ? (DateTime?)null : item.isProcessed,
						IDO = item.IDO,
						scan1 = item.scan1,
						scan2 = item.scan2,
						scan3 = item.scan3,
						sensor = item.sensor,
						material = item.material,
						VPH = item.VPH,
						VPPol = item.IsVPPolNull() ? (int?)null : item.VPPol,
						EAN_IS = item.EAN_IS,
						IS_ID = item.IS_ID,
						NMBRPAL = item.NMBRPAL,
						status = item.IsstatusNull() ? (int?)null : item.status,
						productionGuid = item.IsproductionGuidNull() ? (Guid?)null : item.productionGuid,
						QTYPACK = item.QTYPACK,
						PackType = item.PackType,
						WEIGHT = item.IsWEIGHTNull() ? (decimal?)null : item.WEIGHT,
						ITEMDESC = item.ITEMDESC
					});


                }

                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
