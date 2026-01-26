using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.WEBAPI.API_BusinessObjects
{


    #region Logs

    public class BO_Logs
    {
        public BO_Logs_row[] rows { get; set; }
    }

    public class BO_Logs_row
    {
        public System.Data.DataRowState RowState { get; set; }
        public int id { get; set; }
        public string loginid { get; set; }
        public DateTime dateeve { get; set; }
        public decimal qty { get; set; }
        public decimal qtyReal { get; set; }
        public string description { get; set; }
        public string barcodeReaded { get; set; }
        public string barcodeSended { get; set; }
        public string zakazka { get; set; }
        public string popis { get; set; }
        public Guid faskGUID { get; set; }
        public string reportType { get; set; }
        public DateTime? isProcessed { get; set; }
        public string IDO { get; set; }
        public string scan1 { get; set; }
        public string scan2 { get; set; }
        public string scan3 { get; set; }
        public string sensor { get; set; }
        public string machineid { get; set; }

        public string material { get; set; }
        public Guid? productionGuid { get; set; }
        public string VPH
        { get; set; }
        public int? VPPol
        { get; set; }
        public string EAN_IS
        { get; set; }
        public string IS_ID
        { get; set; }
        public string NMBRPAL
        { get; set; }
        public int? status
        { get; set; }
        public decimal? QTYPACK
        { get; set; }
        public string PackType
        { get; set; }
        public decimal? WEIGHT
        { get; set; }
        public byte? BarcodeT
        { get; set; }
        public string REZ_1
        { get; set; }
        public string REZ_2
        { get; set; }
        public string REZ_3
        { get; set; }
        public string REZ_4
        { get; set; }
        public string REZ_5
        { get; set; }






    }
    #endregion

}