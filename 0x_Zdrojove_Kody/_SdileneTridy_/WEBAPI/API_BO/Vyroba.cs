using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.WEBAPI.API_BusinessObjects
{

    public class FASK_Events
    {
        public FASK_Events_row[] rows { get; set; }
    }

    public class FASK_Events_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int id{ get; set; }
		public string loginid{ get; set; }
		public string machineid{ get; set; }
		public DateTime? dateeve{ get; set; }
		public decimal qty{ get; set; }
		public decimal qtyReal{ get; set; }
		public string description{ get; set; }
		public string barcodeReaded{ get; set; }
		public string barcodeSended{ get; set; }
		public string zakazka{ get; set; }
		public string popis{ get; set; }
		public Guid? faskGUID{ get; set; }
		public string reportType{ get; set; }
		public DateTime? isProcessed{ get; set; }
		public string IDO{ get; set; }
		public string scan1{ get; set; }
		public string scan2{ get; set; }
		public string scan3{ get; set; }
		public string sensor{ get; set; }
		public string material{ get; set; }
		public Guid? productionGuid { get; set; }
		public string VPH{ get; set; }
		public int? VPPol{ get; set; }
		public string EAN_IS{ get; set; }
		public string IS_ID{ get; set; }
		public string NMBRPAL{ get; set; }
		public int? status{ get; set; }
		public string ITEMDESC { get; set; }
		public decimal QTYPACK{ get; set; }
		public string PackType{ get; set; }
		public decimal? WEIGHT{ get; set; }
        public byte BarcodeT { get; set; }
        public string REZ_1 { get; set; }
        public string REZ_2 { get; set; }
        public string REZ_3 { get; set; }
        public string REZ_4 { get; set; }
        public string REZ_5 { get; set; }

    }


    public class FASK_Events_11_2023
    {
        public FASK_Events_row_11_2023[] rows { get; set; }
    }

    public class FASK_Events_row_11_2023
    {
        public System.Data.DataRowState RowState { get; set; }

        public int? id { get; set; }
    
        public string loginid { get; set; }
        public string machineid { get; set; }
        public DateTime? dateeve { get; set; }
        public decimal? qty { get; set; }
        public decimal? qtyReal { get; set; }
        public string description { get; set; }
        public string barcodeReaded { get; set; }
        public string barcodeSended { get; set; }
        public string zakazka { get; set; }
        public string popis { get; set; }
        public Guid? faskGUID { get; set; }
        public string reportType { get; set; }
        public DateTime? isProcessed { get; set; }
        public string IDO { get; set; }
        public string scan1 { get; set; }
        public string scan2 { get; set; }
        public string scan3 { get; set; }
        public string sensor { get; set; }
        public string material { get; set; }
        public Guid? productionGuid { get; set; }
        public string VPH { get; set; }
        public int? VPPol { get; set; }
        public string EAN_IS { get; set; }
        public string IS_ID { get; set; }
        public string NMBRPAL { get; set; }
        public int? status { get; set; }
        public decimal? QTYPACK { get; set; }
        public string PackType { get; set; }
        public decimal? WEIGHT { get; set; }
        public byte? BarcodeT { get; set; }
        public string REZ_1 { get; set; }
        public string REZ_2 { get; set; }
        public string REZ_3 { get; set; }
        public string REZ_4 { get; set; }
        public string REZ_5 { get; set; }

        public string ITEMDESC { get; set; }

    }









    public class BO_CallProcedura
	{
		public string result { get; set; }
		public int? CountEntries { get; set; }

	}

	public class BO_Material
	{
		public string[] material { get; set; }

	}



    #region Výrobní příkazy

    #region VPH
    public class BO_CZPRO_VPH
    {
        public BO_CZPRO_VPH_row[] rows { get; set; }
    }

    public class BO_CZPRO_VPH_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int CountEntries { get; set; }

        public string SOPNUMBE { get; set; }

        public string SOPTYPE { get; set; }

        public string SOPDESC { get; set; }

        public string VNDDOCNMH { get; set; }

        public string BarcodeH { get; set; }

        public string LOCNCODE { get; set; }

        public short DateProd { get; set; }

        public string Rez1 { get; set; }

        public string Rez2 { get; set; }

        public byte TermID { get; set; }

        public System.DateTime LSTMod { get; set; }

        public int DEX_ROW_ID { get; set; }

        public byte Active { get; set; }

        public int USERID { get; set; }

    }
    #endregion

    #region VPP

    public class BO_CZPRO_VPP
    {
        public BO_CZPRO_VPP_row[] rows { get; set; }
    }

    public class BO_CZPRO_VPP_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int CountEntries { get; set; }
        public string SOPNUMBE { get; set; }
        public string ITEMNMBR { get; set; }
        public string ITEMTYPE { get; set; }
        public string ITEMDESC { get; set; }
        public string ITEMMJ { get; set; }
        public string VNDDOCNMP { get; set; }
        public string VNDITNUM { get; set; }
        public int ORD { get; set; }
        public string BarcodeP { get; set; }
        public string LOCNCODE { get; set; }
        public decimal QTYSHPPD { get; set; }
        public decimal? QTYPACK { get; set; }
        public string QTYPACKMJ { get; set; }
        public float TIMEPREP { get; set; }
        public float TIMEUNIT { get; set; }
        public byte DtProdT { get; set; }
        public short DtProdL { get; set; }
        public byte SerNumT { get; set; }
        public short SerNumL { get; set; }
        public byte VerT { get; set; }
        public short VerL { get; set; }
        public byte TermID { get; set; }
        public System.DateTime LSTMod { get; set; }
        public int DEX_ROW_ID { get; set; }
        public decimal QTYODVEDENO { get; set; }
        public decimal CNTODVEDENO { get; set; }
        public int TIMEMODE { get; set; }
        public byte BarcodeT { get; set; }
        public decimal QTYDOKON { get; set; }
        public string ITEMCODE { get; set; }
        public DateTime? Realization_Start { get; set; }
        public DateTime? Realization_Stop { get; set; }
        public byte CZ_REZ1_Track { get; set; }
        public byte CZ_REZ2_Track { get; set; }
        public byte CZ_REZ3_Track { get; set; }
        public byte CZ_REZ4_Track { get; set; }
        public byte CZ_REZ5_Track { get; set; }
        public decimal WEIGHT_TARA { get; set; }
        public decimal WEIGHT_NETTO { get; set; }
        public decimal WEIGHT_TOL_PLUS { get; set; }
        public decimal WEIGHT_TOL_MINUS { get; set; }

    }

    #endregion

    #endregion

    #region Vyroba

    #region Production

    public class BO_Production
    {
        public BO_Production_row[] rows { get; set; }
    }

    public class BO_Production_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int? CountEntries { get; set; }
        public string SOPNUMBE { get; set; }
        public string ITEMNMBR { get; set; }
        public string ITEMTYPE { get; set; }
        public string ITEMMJ { get; set; }
        public int? ORD { get; set; }
        public int? TIMEMODE { get; set; }
        public System.DateTime? TIMEPREPSTART { get; set; }
        public System.DateTime? TIMEPREPSTOP { get; set; }
        public float? TIMEPREP { get; set; }
        public float? TIMEUNIT { get; set; }
        public System.DateTime? TIMESTART { get; set; }
        public System.DateTime? TIMESTOP { get; set; }
        public System.DateTime? TIMECORSTART { get; set; }
        public System.DateTime? TIMECORSTOP { get; set; }
        public float? TIMECOR { get; set; }
        public int? TIMECRID { get; set; }
        public int id { get; set; }
        public string loginid { get; set; }
        public string machineid { get; set; }
        public string operationid { get; set; }
        public System.DateTime dateeve { get; set; }
        public decimal qty { get; set; }
        public decimal qtyReal { get; set; }
        public decimal? QTYPACK { get; set; }
        public string QTYPACKMJ { get; set; }
        public string description { get; set; }
        public string BarcodeP { get; set; }
        public string UserID { get; set; }
        public byte TermID { get; set; }
        public System.DateTime? ISOK { get; set; }
        public System.Guid GUID { get; set; }
        public System.Guid? SOUBEHGUID { get; set; }
        public decimal? qtyOld { get; set; }
        public string idVS { get; set; }
        public System.DateTime? dateedit { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string ITEMDESC { get; set; }
        public string operationName { get; set; }
        public string machineName { get; set; }
        public System.Guid? CORRGUID { get; set; }
        public string groupName { get; set; }
        public byte? TIMECRIDTYPE { get; set; }
        public string SKL_ID { get; set; }
        public string skladName { get; set; }
        public string LOCNCODE { get; set; }
        public string lokaceKod { get; set; }
        public string lokaceName { get; set; }
        public string popiszakazky { get; set; }
        public string SERLTNUM { get; set; }
        public string EXPIRATION { get; set; }
        public string NMBRPAL { get; set; }
        public string TYPEPAL { get; set; }
        public string PackType { get; set; }
        public int? status { get; set; }
        public decimal? WEIGHT { get; set; }
        public Guid? STORNOGUID { get; set; }
        public string REZ_1 { get; set; }
        public string REZ_2 { get; set; }
        public string REZ_3 { get; set; }
        public string REZ_4 { get; set; }
        public string REZ_5 { get; set; }
        public decimal? WEIGHT_OLD { get; set; }

    }
    #endregion

    #region Groups

    public class BO_Groups
    {
        public BO_Groups_row[] rows { get; set; }
    }

    public class BO_Groups_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }


    }
    #endregion

    #region Machines

    public class BO_Machines
    {
        public BO_Machines_row[] rows { get; set; }
    }

    public class BO_Machines_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }


    }
    #endregion

    #region Operations

    public class BO_Operations
    {
        public BO_Operations_row[] rows { get; set; }
    }

    public class BO_Operations_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }


    }
    #endregion

    #region CZMST093

    public class BO_CZMST093
    {
        public BO_CZMST093_row[] rows { get; set; }
    }

    public class BO_CZMST093_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string skl_id { get; set; }
        public string skl_desc { get; set; }
        public string skl_typ { get; set; }
        public string skl_carcode { get; set; }
        public int DEX_ROW_ID { get; set; }


    }
    #endregion

    #region CZMST094

    public class BO_CZMST094
    {
        public BO_CZMST094_row[] rows { get; set; }
    }

    public class BO_CZMST094_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string SKL_ID { get; set; }
        public string LOCNCODE { get; set; }
        public string TYPE { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public int DEX_ROW_ID { get; set; }


    }
    #endregion

    #region Corrects

    public class BO_Corrects
    {
        public BO_Corrects_row[] rows { get; set; }
    }

    public class BO_Corrects_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int id { get; set; }
        public string desc { get; set; }
        public Single? TMFrom { get; set; }
        public Single? TMTo { get; set; }
        public byte Production { get; set; }
        public byte? ProductionType { get; set; }


    }
    #endregion

    #region VMachinesOperations

    public class BO_VMachinesOperations
    {
        public BO_VMachinesOperations_row[] rows { get; set; }
    }

    public class BO_VMachinesOperations_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string machineid { get; set; }
        public string operationid { get; set; }

    }
    #endregion

    #region Fask_EventsErr
    public class BO_FASK_EventsErr
    {
        public BO_FASK_EventsErr_row[] rows { get; set; }
    }

    public class BO_FASK_EventsErr_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int id { get; set; }
        public string loginid { get; set; }
        public string machineid { get; set; }
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



    }

    #endregion

    #region Fask_MachineStateSet
    public class BO_MachineStateSet
    {
        public BO_MachineStateSet_row[] rows { get; set; }
    }

    public class BO_MachineStateSet_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string description { get; set; }
        public string IP { get; set; }
        public DateTime DateModified { get; set; }
        public int? S0 { get; set; }
        public int? S1 { get; set; }
        public int? S2 { get; set; }
        public int? S3 { get; set; }
        public int? S4 { get; set; }
        public int? S5 { get; set; }
        public int? S6 { get; set; }
        public int? S7 { get; set; }
        public int? S8 { get; set; }
        public int? S9 { get; set; }
        public int? S10 { get; set; }
        public int? S11 { get; set; }
        public int? counter_0 { get; set; }
        public int? counter_1 { get; set; }
        public int? counter_2 { get; set; }
        public int? counter_3 { get; set; }
        public int? counter_4 { get; set; }
        public int? counter_5 { get; set; }
        public int? counter_6 { get; set; }
        public int? counter_7 { get; set; }
        public int? counter_8 { get; set; }
        public int? counter_9 { get; set; }
        public int? counter_10 { get; set; }
        public int? counter_11 { get; set; }
        public int ID_group { get; set; }

    }

    #endregion

    #region FASK_FORMULARE
    public class BO_FASK_FORMULARE
    {
        public BO_FASK_FORMULARE_row[] rows { get; set; }
    }

    public class BO_FASK_FORMULARE_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int? id { get; set; }

        public string nazev_okna { get; set; }
        public string nazev { get; set; }
        public int? ord { get; set; }
        public string typ { get; set; }
        public string loginid { get; set; }
        public string machineid { get; set; }
        public string formular { get; set; }

    }

    #endregion

    #endregion

    #region Sklady

    #region ciselniky

    #region CZMST090
    public class BO_CZMST090
    {
        public BO_CZMST090_row[] rows { get; set; }
    }

    public class BO_CZMST090_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string odb_id { get; set; }
        public string odb_desc { get; set; }
        public string odb_typ { get; set; }
        public string odb_carcode { get; set; }
        public int DEX_ROW_ID { get; set; }

        public string odb_ico { get; set; }
        public string mena_id { get; set; }
        public string odb_misto { get; set; }
        public string odb_ulice { get; set; }

        public string odb_cisloOr { get; set; }
        public string odb_psc { get; set; }
        public string odb_dic { get; set; }
        public bool odb_Odberatel { get; set; }
        public bool odb_Dodavatel { get; set; }

    }

    #endregion

    #region CZMST091
    public class BO_CZMST091
    {
        public BO_CZMST091_row[] rows { get; set; }
    }

    public class BO_CZMST091_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string str_id { get; set; }
        public string str_desc { get; set; }
        public string str_typ { get; set; }
        public string str_carcode { get; set; }
        public int DEX_ROW_ID { get; set; }

        public string skl_id { get; set; }
        public string odb_id { get; set; }
       

    }

    #endregion

    #region CZMST_SkladLokace_LokaceTypy
    public class BO_CZMST_SkladLokace_LokaceTypy
    {
        public BO_CZMST_SkladLokace_LokaceTypy_row[] rows { get; set; }
    }

    public class BO_CZMST_SkladLokace_LokaceTypy_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string TYPE { get; set; }
        public string Description { get; set; }
        public bool IS_RECEIVE { get; set; }
        public bool IS_DEFAULT { get; set; }
        public bool IS_NORMAL { get; set; }


    }

    #endregion

    #region CZMST_SkladLokace_Mapa
    public class BO_CZMST_SkladLokace_Mapa
    {
        public BO_CZMST_SkladLokace_Mapa_row[] rows { get; set; }
    }

    public class BO_CZMST_SkladLokace_Mapa_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string TYPE { get; set; }
        public string Description { get; set; }
        public bool IS_RECEIVE { get; set; }
        public bool IS_DEFAULT { get; set; }
        public bool IS_NORMAL { get; set; }


    }

    #endregion

    #region CZMST_SkladLokace_LokaceVariantySortiment
    public class BO_CZMST_SkladLokace_LokaceVariantySortiment
    {
        public BO_CZMST_SkladLokace_LokaceVariantySortiment_row[] rows { get; set; }
    }

    public class BO_CZMST_SkladLokace_LokaceVariantySortiment_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string ITEMNMBR { get; set; }
        public string SKL_ID { get; set; }
        public string LOCNCODE { get; set; }
        public string TYPE { get; set; }
        public int UserID { get; set; }
        public int TermID { get; set; }
        public string ZboziITEMDESC { get; set; }
        public string ZboziITEMCODE { get; set; }
        public string ZboziCZ_CarKod { get; set; }
        public bool TypIS_RECEIVE { get; set; }

        public bool TypIS_DEFAULT { get; set; }
        public bool TypIS_NORMAL { get; set; }
        public int DEX_ROW_ID { get; set; }
        public string ZboziVNDITNUM { get; set; }

    }

    #endregion

    #endregion

    #region Transakce

    #region Volny pohyb

    public class BO_CZMST_DI
    {
        public BO_CZMST_DI_row[] rows { get; set; }
    }

    public class BO_CZMST_DI_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int CountEntries { get; set; }

        public string VNDITNUM { get; set; }

        public string CZ_CarKod { get; set; }

        public string ODB_ID { get; set; }

        public string STR_ID { get; set; }

        public string DOC_ID { get; set; }

        public string DOC_ID2 { get; set; }

        public string SKL_ID { get; set; }

        public string PRAC_ID { get; set; }

        public string ITEMNMBR { get; set; }

        public string ITEMCODE { get; set; }

        public string LOCNCODE { get; set; }

        public string MJ { get; set; }

        public decimal QTYSHPPD { get; set; }

        public decimal QTYSHPPDMJ { get; set; }

        public decimal QTYPACK { get; set; }

        public string SERLTNUM { get; set; }

        public decimal TAXAMPIE { get; set; }

        public decimal AMOUNPIE { get; set; }

        public byte? WITHTAX { get; set; }

        public byte? PRICEX { get; set; }

        public string mena_ID { get; set; }

        public decimal? TAXAMPIEM { get; set; }

        public decimal? AMOUNPIEM { get; set; }

        public string mena_IDM { get; set; }

        public string REZ_1 { get; set; }

        public string REZ_2 { get; set; }

        public string REZ_3 { get; set; }

        public string REZ_4 { get; set; }

        public int? USER_ID { get; set; }

        public string DATEDONE { get; set; }

        public string TIMEDONE { get; set; }

        public int DEX_ROW_ID { get; set; }

        public Guid? GUID { get; set; }

        public byte INPUT_MODE { get; set; }

        public int ID_TERMINAL { get; set; }

        public string LOCNCODEDEST { get; set; }

        public string SKL_ID_DEST { get; set; }

        public DateTime? EXPIRACE { get; set; }

    }

    #endregion


    #endregion

    #endregion

    #region Terminaly
    #region CZMST_TERMINAL_ALL
    public class BO_CZMST_TERMINAL_ALL
    {
        public BO_CZMST_TERMINAL_ALL_row[] rows { get; set; }
    }

    public class BO_CZMST_TERMINAL_ALL_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public int ID_TERMINAL { get; set; }
        public string IP { get; set; }
        public DateTime DATEREQ { get; set; }
        public string DB_TYPE { get; set; }


    }
    #endregion


    #endregion

    #region IT_cast

    #region procedura archivace Fask_Events

    public class BO_Fask_Events_Archivace
    {
        public int pocetZaznamu { get; set; }
        public DateTime? Od { get; set; }
        public DateTime? Do { get; set; }

    }


    #endregion

    #region procedura archivace Production

    public class BO_Production_Archivace
    {
        public int pocetZaznamu { get; set; }
        public DateTime? Od { get; set; }
        public DateTime? Do { get; set; }

    }


    #endregion

    #endregion
}

