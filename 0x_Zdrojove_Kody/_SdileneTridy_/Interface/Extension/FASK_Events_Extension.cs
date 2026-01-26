using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Extension
{
    public static class FASK_Events_Extension
    {

        

            public static Fask.WEBAPI.API_BusinessObjects.FASK_Events DataSetToBO(this Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt)
            {
                try
                {
                Fask.WEBAPI.API_BusinessObjects.FASK_Events listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.FASK_Events(); //vytvoreni tabulky
                    List<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row> rowList = new List<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row>(); //vytvoreni docasne tabulky

                    foreach (var item in dt)
                    {
                    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row row = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row(); //docasny radek

                    #region Fask_Events

                    row.id = item.id;
                    row.loginid = item.loginid;
                    row.machineid = item.machineid;
                    row.dateeve = item.dateeve;
                    row.qty = item.qty;
                    row.qtyReal = item.qtyReal;
                    row.description = item.IsdescriptionNull() ? string.Empty : (string.IsNullOrEmpty(item.description) ? string.Empty : item.description.Trim());
                    row.barcodeReaded = item.barcodeReaded;
                    row.barcodeSended = item.barcodeSended;
                    row.zakazka = item.IszakazkaNull() ? string.Empty : (string.IsNullOrEmpty(item.zakazka) ? string.Empty : item.zakazka.Trim());
                    row.faskGUID = item.faskGUID;
                    row.reportType = item.reportType;
                    row.isProcessed = item.IsisProcessedNull() ? (DateTime?)null : item.isProcessed; //
                    row.IDO = item.IsIDONull() ? string.Empty : (string.IsNullOrEmpty(item.IDO) ? string.Empty : item.IDO.Trim());
                    row.scan1 = item.Isscan1Null() ? string.Empty : (string.IsNullOrEmpty(item.scan1) ? string.Empty : item.scan1.Trim());
                    row.scan2 = item.Isscan2Null() ? string.Empty : (string.IsNullOrEmpty(item.scan2) ? string.Empty : item.scan2.Trim());
                    row.scan3 = item.Isscan3Null() ? string.Empty : (string.IsNullOrEmpty(item.scan3) ? string.Empty : item.scan3.Trim());
                    row.sensor = item.IssensorNull() ? string.Empty : (string.IsNullOrEmpty(item.sensor) ? string.Empty : item.sensor.Trim());
                    row.material = item.IsmaterialNull() ? string.Empty : (string.IsNullOrEmpty(item.material) ? string.Empty : item.material.Trim());
                    row.VPH = item.IsVPHNull() ? string.Empty : (string.IsNullOrEmpty(item.VPH) ? string.Empty : item.VPH.Trim());
                    row.VPPol = item.IsVPPolNull() ? (int?)null : item.VPPol;
                    row.EAN_IS = item.IsEAN_ISNull() ? string.Empty : (string.IsNullOrEmpty(item.EAN_IS) ? string.Empty : item.EAN_IS.Trim());
                    row.IS_ID = item.IsIS_IDNull() ? string.Empty : (string.IsNullOrEmpty(item.IS_ID) ? string.Empty : item.IS_ID.Trim());
                    row.NMBRPAL = item.IsNMBRPALNull() ? string.Empty : (string.IsNullOrEmpty(item.NMBRPAL) ? string.Empty : item.NMBRPAL.Trim());
                    row.status = item.IsstatusNull() ? (int?)null : item.status;
                    row.productionGuid = item.IsproductionGuidNull() ? (Guid?)null : item.productionGuid;
                    row.popis = item.IspopisNull() ? null : item.popis;
                    row.QTYPACK = item.QTYPACK;
                    row.PackType = item.IsPackTypeNull() ? string.Empty : (string.IsNullOrEmpty(item.PackType) ? string.Empty : item.PackType.Trim());
                    row.WEIGHT = item.IsWEIGHTNull() ? (decimal?)null : item.WEIGHT;
                    row.ITEMDESC = item.IsITEMDESCNull() ? string.Empty : (string.IsNullOrEmpty(item.ITEMDESC) ? string.Empty : item.ITEMDESC.Trim());

                    #endregion


                    rowList.Add(row); //pridani docasneho radku do docasne tabulky

                    }

                    listObjektu_FZ.rows = rowList.ToArray(); // prekopirovani docasne tabulky a konvertovani na celou obalku

                    return listObjektu_FZ;
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }



            public static Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable BOToDataSet(this Fask.WEBAPI.API_BusinessObjects.FASK_Events bo)
            {

                try
                {
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                    foreach (var item in bo.rows.ToList<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row>())
                    {
                        var x = dt.NewFASK_EventsRow();

                    #region kontrola a plneni datasetu

                    x.id = item.id;
                    x.loginid = item.loginid;
                    x.machineid = item.machineid;
                    x.dateeve = item.dateeve.HasValue ? item.dateeve.Value : DateTime.MinValue;
                    x.qty = item.qty;
                    x.qtyReal = item.qtyReal;
                    x.description = string.IsNullOrEmpty(item.description) ? string.Empty : item.description.Trim();
                    x.barcodeReaded = item.barcodeReaded;
                    x.barcodeSended = item.barcodeSended;
                    x.zakazka = string.IsNullOrEmpty(item.zakazka) ? string.Empty : item.zakazka.Trim();
                    x.faskGUID = item.faskGUID.HasValue ? item.faskGUID.Value : Guid.Empty;
                    x.reportType = item.reportType;
                    if (item.isProcessed.HasValue)
                    {
                        x.isProcessed = item.isProcessed.Value;
                    }
                    else
                    {
                        x.SetisProcessedNull();
                    }
                    x.IDO = string.IsNullOrEmpty(item.IDO) ? string.Empty : item.IDO.Trim();
                    x.scan1 = string.IsNullOrEmpty(item.scan1) ? string.Empty : item.scan1.Trim();
                   x.scan2 = string.IsNullOrEmpty(item.scan2) ? string.Empty : item.scan2.Trim();
                   x.scan3 = string.IsNullOrEmpty(item.scan3) ? string.Empty : item.scan3.Trim();
                    x.sensor = string.IsNullOrEmpty(item.sensor) ? string.Empty : item.sensor.Trim();
                    x.material = string.IsNullOrEmpty(item.material) ? string.Empty : item.material.Trim();
                    x.VPH = string.IsNullOrEmpty(item.VPH) ? string.Empty : item.VPH.Trim();
                    if (item.VPPol.HasValue)
                    {
                        x.VPPol = item.VPPol.Value;
                    }
                    else
                    {
                        x.SetVPPolNull();
                    }
                   x.EAN_IS = string.IsNullOrEmpty(item.EAN_IS) ? string.Empty : item.EAN_IS.Trim();
                   x.IS_ID = string.IsNullOrEmpty(item.IS_ID) ? string.Empty : item.IS_ID.Trim();
                    x.NMBRPAL = string.IsNullOrEmpty(item.NMBRPAL) ? string.Empty : item.NMBRPAL.Trim();
                    if (item.status.HasValue)
                    {
                        x.status = item.status.Value;
                    }
                    else
                    {
                        x.SetstatusNull();
                    }
                   if (item.productionGuid.HasValue)
                    {
                        x.productionGuid = item.productionGuid.Value;
                    }
                    else
                    {
                        x.SetproductionGuidNull();
                    }
                    x.popis = string.IsNullOrEmpty(item.popis) ? string.Empty : item.popis.Trim();
                    x.QTYPACK = item.QTYPACK;
                    x.PackType = string.IsNullOrEmpty(item.PackType) ? string.Empty : item.PackType.Trim();
                   if (item.WEIGHT.HasValue)
                    {
                        x.WEIGHT = item.WEIGHT.Value;
                    }
                    else
                    {
                        x.SetWEIGHTNull();
                    }
                    x.ITEMDESC = string.IsNullOrEmpty(item.ITEMDESC) ? string.Empty : item.ITEMDESC.Trim();
                   
                    #endregion


                    dt.AddFASK_EventsRow(x);


                    }

                    dt.AcceptChanges();

                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
        

    }
}
