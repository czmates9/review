using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Fask.WEBAPI.API_BusinessObjects
{

    #region FASK_ZASOBY_ALL
    public class FASK_ZASOBY_ALL
    {
        public FASK_ZASOBY_ALL_row[] rows { get; set; }
    }

    public class FASK_ZASOBY_ALL_row
    {

        public System.Data.DataRowState RowState { get; set; }
        public int DEX_ROW_ID_Zbozi { get; set; }

        [CsvHelper.Configuration.Attributes.Name("nevim_99")]
        public string ITEMNMBR { get; set; }

        public string ITEMDESC { get; set; }

        public string ITEMCODE { get; set; }

        public string VNDITNUM { get; set; }

        public string CZ_CarKod { get; set; }

        public string LOCNCODE { get; set; }

        public string SKL_ID { get; set; }

        public decimal QTY { get; set; }

        public decimal? QTYPACK { get; set; }

        public string MJ { get; set; }

        public string DMJ { get; set; }

        public decimal? TAXRATE { get; set; }

        public decimal? PRICE0 { get; set; }

        public decimal? PRICE1 { get; set; }

        public decimal? PRICE2 { get; set; }

        public decimal? PRICE3 { get; set; }

        public decimal? PRICE4 { get; set; }

        public decimal? PRICE5 { get; set; }

        public byte CZ_SerNum_Track { get; set; }

        public short CZ_SerNum_Delka { get; set; }

        public byte CZ_Rez1_Track { get; set; }

        public byte CZ_Rez2_Track { get; set; }

        public byte CZ_Rez3_Track { get; set; }

        public byte CZ_Rez4_Track { get; set; }

        public string REZ1 { get; set; }

        public string REZ2 { get; set; }

        public string REZ3 { get; set; }

        public string REZ4 { get; set; }

        public string ODB_ID { get; set; }

        public string mena_ID { get; set; }

        public string SERLTNUM { get; set; }

        public decimal? WEIGHT { get; set; }

        public DateTime? TIMEFROM { get; set; }

        public DateTime? TIMETO { get; set; }

        public DateTime? LSTMod { get; set; }

        public string loginid { get; set; }

        public bool? VPrFVTS { get; set; }

        public bool? VPrFPTS { get; set; }

        public bool? VPrFDTS { get; set; }

        public bool? VPrFITS { get; set; }

        public bool? VPrFXTS { get; set; }

        public int? RefVPrFVTS { get; set; }

        public int? RefVPrFPTS { get; set; }

        public int? RefVPrFDTS { get; set; }

        public int? RefVPrFITS { get; set; }

        public int? RefVPrFXTS { get; set; }

        public double? VPrTIMEPREP { get; set; }

        public double? VPrTIMEUNIT { get; set; }

        public int? RefVPrTIMEMODE { get; set; }

        public int? DEX_ROW_ID_PARAMETRY { get; set; }

        public string SKL_DESC { get; set; }

        public string ITEMTYPE { get; set; }

        public string Vetev1 { get; set; }

        public string Vetev2 { get; set; }

        public string Vetev3 { get; set; }

        public string Vetev4 { get; set; }

        public string Vetev5 { get; set; }

        public string Vetev6 { get; set; }

        public string Vetev7 { get; set; }

        public string ITEMTYPE_Text { get; set; }

        /// <summary>
        /// pozor atribut je v DB not null!!
        /// </summary>
        public byte? CZ_Expirace_Track { get; set; }
        public DateTime? EXPIRACE { get; set; }


    }
    #endregion

    #region FASK_ZASOBY


    #region prevodnik

    public class NullableByteWithDefaultConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return (byte?)0;
            }
            return byte.Parse(text, CultureInfo.GetCultureInfo("cs-CZ") /*CultureInfo.InvariantCulture*/);
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return "0";
            }
            return ((byte?)value).ToString();
        }
    }
    #endregion

    public class FASK_ZASOBY_CSV00_row
    {
        [CsvHelper.Configuration.Attributes.Name("DEX_ROW_ID")]
        public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cislo polozky")]
        public string ITEMNMBR { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Oznaceni polozky")]
        public string ITEMDESC { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Kod polozky")]
        public string ITEMCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod dodavatele")]
        public string VNDITNUM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod")]
        public string CZ_CarKod { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Lokace")]
        public string LOCNCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID skladu")]
        public string SKL_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Mnozstvi")]
        public decimal QTY { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Mnozstvi baleni")]
        public decimal? QTYPACK { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Merna jednotka")]
        public string MJ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doporucena merna jednotka")]
        public string DMJ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Vyse DPH")]
        public decimal? TAXRATE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina")]
        public decimal? PRICE0 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 1")]
        public decimal? PRICE1 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 2")]
        public decimal? PRICE2 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 3")]
        public decimal? PRICE3 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 4")]
        public decimal? PRICE4 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 5")]
        public decimal? PRICE5 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ sledovani")]
        public byte CZ_SerNum_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Delka serioveho cisla")]
        public short CZ_SerNum_Delka { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ1")]
        public byte CZ_Rez1_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ2")]
        public byte CZ_Rez2_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ3")]
        public byte CZ_Rez3_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ4")]
        public byte CZ_Rez4_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 1")]
        public string REZ1 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 2")]
        public string REZ2 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 3")]
        public string REZ3 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 4")]
        public string REZ4 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID odberatele")]
        public string ODB_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("mena_ID")]
        public string mena_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Sarze")]
        public string SERLTNUM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Vaha")]
        public decimal? WEIGHT { get; set; }

        [CsvHelper.Configuration.Attributes.Name("TIMEFROM")]
        public DateTime? TIMEFROM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("TIMETO")]
        public DateTime? TIMETO { get; set; }

        [CsvHelper.Configuration.Attributes.Name("LSTMod")]
        public DateTime? LSTMod { get; set; }

        [CsvHelper.Configuration.Attributes.Name("loginid")]
        public string loginid { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Nazev skladu")]
        public string SKL_DESC { get; set; }

        [CsvHelper.Configuration.Attributes.Name("priznak sledovani exspirace")]
        //[TypeConverter(typeof(NullableByteWithDefaultConverter))] //nejde -- zvol jiny zpusob !!
        public byte? CZ_Expirace_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Datum exspirace")]
        public DateTime? EXPIRACE { get; set; }
    }


    public class FASK_ZASOBY_CSV_row
    {

        //[CsvHelper.Configuration.Attributes.Name("nevim_00")]
        //public System.Data.DataRowState RowState { get; set; }

        //[CsvHelper.Configuration.Attributes.Name("DEX_ROW_ID")]
        //public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cislo polozky")]
        public string ITEMNMBR { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Oznaceni polozky")]
        public string ITEMDESC { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Kod polozky")]
        public string ITEMCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod dodavatele")]
        public string VNDITNUM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod")]
        public string CZ_CarKod { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Lokace")]
        public string LOCNCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID skladu")]
        public string SKL_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Mnozstvi")]
        public decimal QTY { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Mnozstvi baleni")]
        public decimal? QTYPACK { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Merna jednotka")]
        public string MJ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doporucena merna jednotka")]
        public string DMJ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Vyse DPH")]
        public decimal? TAXRATE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina")]
        public decimal? PRICE0 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 1")]
        public decimal? PRICE1 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 2")]
        public decimal? PRICE2 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 3")]
        public decimal? PRICE3 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 4")]
        public decimal? PRICE4 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 5")]
        public decimal? PRICE5 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ sledovani")]
        public byte CZ_SerNum_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Delka serioveho cisla")]
        public short CZ_SerNum_Delka { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ1")]
        public byte CZ_Rez1_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ2")]
        public byte CZ_Rez2_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ3")]
        public byte CZ_Rez3_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ4")]
        public byte CZ_Rez4_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 1")]
        public string REZ1 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 2")]
        public string REZ2 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 3")]
        public string REZ3 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 4")]
        public string REZ4 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID odberatele")]
        public string ODB_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("mena_ID")]
        public string mena_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Sarze")]
        public string SERLTNUM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Vaha")]
        public decimal? WEIGHT { get; set; }

        [CsvHelper.Configuration.Attributes.Name("TIMEFROM")]
        public DateTime? TIMEFROM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("TIMETO")]
        public DateTime? TIMETO { get; set; }

        [CsvHelper.Configuration.Attributes.Name("LSTMod")]
        public DateTime? LSTMod { get; set; }

        [CsvHelper.Configuration.Attributes.Name("loginid")]
        public string loginid { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Nazev skladu")]
        public string SKL_DESC { get; set; }


    }


    public class FASK_ZASOBY
    {
        public FASK_ZASOBY_row[] rows { get; set; }
    }

    public class FASK_ZASOBY_row
    {

        [CsvHelper.Configuration.Attributes.Name("nevim_00")]
        public System.Data.DataRowState RowState { get; set; }

        [CsvHelper.Configuration.Attributes.Name("nevim_01")]
        public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cislo polozky")]
        public string ITEMNMBR { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Oznaceni polozky")]
        public string ITEMDESC { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Kod polozky")]
        public string ITEMCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod dodavatele")]
        public  string VNDITNUM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod")]
        public  string CZ_CarKod { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Lokace")]
        public  string LOCNCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID skladu")]
        public  string SKL_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Mnozstvi")]
        public  decimal QTY { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Mnozstvi baleni")]
        public  decimal? QTYPACK { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Merna jednotka")]
        public  string MJ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doporucena merna jednotka")]
        public  string DMJ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Vyse DPH")]
        public  decimal? TAXRATE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina")]
        public  decimal? PRICE0 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 1")]
        public  decimal? PRICE1 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 2")]
        public  decimal? PRICE2 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 3")]
        public  decimal? PRICE3 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 4")]
        public  decimal? PRICE4 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Cenova hladina 5")]
        public  decimal? PRICE5 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ sledovani")]
        public  byte CZ_SerNum_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Delka serioveho cisla")]
        public  short CZ_SerNum_Delka { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ1")]
        public  byte CZ_Rez1_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ2")]
        public  byte CZ_Rez2_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ3")]
        public  byte CZ_Rez3_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnovat hodnotu REZ4")]
        public  byte CZ_Rez4_Track { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 1")]
        public  string REZ1 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 2")]
        public  string REZ2 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 3")]
        public  string REZ3 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Doplnkova hodnota 4")]
        public  string REZ4 { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID odberatele")]
        public  string ODB_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("mena_ID")]
        public  string mena_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Sarze")]
        public  string SERLTNUM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Vaha")]
        public  decimal? WEIGHT { get; set; }

        [CsvHelper.Configuration.Attributes.Name("TIMEFROM")]
        public  DateTime? TIMEFROM { get; set; }

        [CsvHelper.Configuration.Attributes.Name("TIMETO")]
        public  DateTime? TIMETO { get; set; }

        [CsvHelper.Configuration.Attributes.Name("LSTMod")]
        public  DateTime? LSTMod { get; set; }

        [CsvHelper.Configuration.Attributes.Name("loginid")]
        public  string loginid { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Nazev skladu")]
        public  string SKL_DESC { get; set; }

       
    }
    #endregion

    #region FASK_ZASOBY_PARAMETRY
    public class FASK_ZASOBY_PARAMETRY
    {
        public FASK_ZASOBY_PARAMETRY_row[] rows { get; set; }
    }

    public class FASK_ZASOBY_PARAMETRY_row
    {
        public System.Data.DataRowState RowState { get; set; }
        public int DEX_ROW_ID { get; set; }

        public string ITEMNMBR { get; set; }

        public bool VPrFVTS { get; set; }

        public bool VPrFPTS { get; set; }

        public bool VPrFDTS { get; set; }

        public bool VPrFITS { get; set; }

        public bool VPrFXTS { get; set; }

        public int? RefVPrFVTS { get; set; }

        public int? RefVPrFPTS { get; set; }

        public int? RefVPrFDTS { get; set; }

        public int? RefVPrFITS { get; set; }

        public int? RefVPrFXTS { get; set; }

        public double? VPrTIMEPREP { get; set; }

        public double? VPrTIMEUNIT { get; set; }

        public int? RefVPrTIMEMODE { get; set; }

       


    }
    #endregion
}