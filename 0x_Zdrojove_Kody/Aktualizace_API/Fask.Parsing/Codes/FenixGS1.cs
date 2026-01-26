using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
    /// <summary>
    /// Hodnoty aplikacnich identifikatoru pro GS1
    /// Jednotlive narodni normy se mohou lisit ... 
    /// </summary>
    public class FenixGS1 : BaseCode
    {
        /// <summary>
        /// GTIN (Carovy kod jednotky vyrobce)
        /// 5.1.1 GTIN – GLOBÁLNÍ ČÍSLO OBCHODNÍ POLOŽKY
        /// 01 GTIN – globální číslo obchodní položky
        /// Global Trade Item Number
        /// n2+n14
        /// </summary>
        public const string ai_gtin = "01";
        /// <summary>
        /// LOT (Sarze)
        /// 5.1.5 ČÍSLO DÁVKY, ŠARŽE
        /// 10 číslo dávky, šarže n2+an..20
        /// GS1 aplikační identifikátor (10) (Batch nebo Lot Number – BATCH/LOT) udává, že
        /// zakódovaná data představují číslo dávky/šarže.
        /// GS1 AI (10) propojuje příslušnou obchodní položku s informací důležitou zejména pro
        /// její sledovatelnost. Jeho tvorba je v plné kompetenci emitenta a může představovat
        /// číslo šarže, pracovní směny, stroje, času, interní číslo a podobně. Číslo dávky/šarže v
        /// tomto formátu může být vytvořeno pomocí alfanumerických znaků a může mít
        /// jakoukoliv délku až do maxima dvaceti znaků.
        /// GS1 AI (10) musí být vždy ve spojení s GTIN, ke kterému se váže.
        /// </summary>
        public const string ai_lot = "10";
        /// <summary>
        /// Expiration (Expirace)
        /// 5.2.6 POUŽITELNOST DO
        /// 17 použitelnost do (RRMMDD) n2+n6
        /// Použitelnost do… (Expiration Date – USE BY či EXPIRY) označuje finální limit spotřeby
        /// či použití produktu. V sektoru zdravotnictví se používá pro vyjádření data exspirace.
        /// </summary>
        public const string ai_expiration = "17";

        /// <summary>
        /// 5.1.6 SÉRIOVÉ ČÍSLO
        /// 21 sériové číslo n2+an..20
        /// GS1 AI (21) je přidělován pro identifikaci sériového čísla. Je unikátním kódem
        /// přiděleným výrobcem konkrétnímu produktu na celou dobu jeho životnosti. V
        /// kombinaci s GTIN jednoznačně identifikuje každý jednotlivý produkt.
        /// GS1 AI (21) musí být vždy vázán na příslušný GTIN.
        /// </summary>
        public const string ai_serialnumber = "21";

        /// <summary>
        /// 5.3.1 PROMĚNNÉ MNOŽSTVÍ
        /// 30 proměnné množství n2+n..8
        /// Proměnné množství (Variable Count – VAR. COUNT) se používá výlučně v případě
        /// doplnění informace o počtu kusů logistické jednotky o proměnném obsahu.
        /// GS1 AI (30) musí být vždy vázán na příslušný GTIN.
        /// </summary>
        public const string ai_variablecount = "30";

        // obsahuje(muze obsahovat)
        // - Klic = AI (aplikacni identifikator) => seznam viz. GS1
        // - Hodnota = hodnota pro dane AI
        public Dictionary<string, string> AIList = new Dictionary<string,string>();

        private string AIValueGet(string ai)
        {
            if (AIList.ContainsKey(ai))
                return AIList[ai];
            return null;
        }

        private void AIValueSet(string ai, string ai_value)
        {
            if (AIList.ContainsKey(ai))
                AIList[ai] = ai_value;
            else
                AIList.Add(ai, ai_value);
        }

        /// <summary>
        /// Rozkoduje z GTIN hodnotu pro EAN(GTIN_13)
        /// </summary>
        public string GTIN_13
        {
            get
            {
                string gtin = this.GTIN;
                if (string.IsNullOrEmpty(gtin))
                    return string.Empty;
                if (gtin.Length > 13) //respektive by mel byt presne 14
                    return gtin.Substring(1, 13);
                if (gtin.Length < 13)
                    return gtin.PadLeft(13, '0');

                // zde je presne 13 znaku ... 
                return gtin;
            }
        }

        /// <summary>
        /// GTIN (14 znaku)
        /// </summary>
        public string GTIN
        {
            get { return AIValueGet(ai_gtin); }
            set { AIValueSet(ai_gtin, value); }
        }

        /// <summary>
        /// Sarze
        /// AI = 10
        /// </summary>
        public string LOT
        {
            get { return AIValueGet(ai_lot); }
            set { AIValueSet(ai_lot, value); }
        }

        /// <summary>
        /// Pouzitelnost do (RRMMDD)
        /// </summary>
        public DateTime? EXPIRATION
        {
            get
            {
                try
                {
                    string expiration = AIValueGet(ai_expiration);
                    if (expiration != null)
                        return DateTime.Parse(expiration);
                    else
                        return null;
                }
                catch 
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                    AIValueSet(ai_expiration, value.Value.ToString());                
            }
        }
        /// <summary>
        /// Serial Number
        /// </summary>
        public string SerialNumber
        {
            get { return AIValueGet(ai_serialnumber); }
            set { AIValueSet(ai_serialnumber, value); }
        }

        public string SN_or_LOT
        {
            get
            {
                if (!String.IsNullOrEmpty(this.SerialNumber))
                    return this.SerialNumber;
                else if (!String.IsNullOrEmpty(this.LOT))
                    return this.LOT;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Promenne mnozstvi
        /// </summary>
        public int? VariableCount
        {
            get
            {
                try
                {
                    string variablecount = AIValueGet(ai_variablecount);
                    if (variablecount != null)
                        return int.Parse(variablecount);
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                    AIValueSet(ai_serialnumber, value.Value.ToString());
            }
        }

        public static FenixGS1 Parse(string dataFull)
        {
            try
            {
                FenixGS1 gs1 = new FenixGS1();

                List<string> dataSplit = dataFull.Split(new char[] { (char)29 }).ToList();

                foreach (string dataSplitItem in dataSplit)
                {
                    string data = dataSplitItem;
                    // parsovani treba resit sloziteji ... ???
                    bool datalenghtchanged = true;
                    while ((data.Length > 2) && (datalenghtchanged))
                    {
                        int datalengthbefore = data.Length;

                        if ((data.Length > 0) && (data.Substring(0, 2) == ai_gtin))
                        {
                            gs1.GTIN = data.Substring(2, 14);
                            data = data.Substring(2 + 14);
                        }

                        if ((data.Length > 0) && (data.Substring(0, 2) == ai_expiration))
                        {
                            string rrmmdd = data.Substring(2, 6);                            
                            gs1.EXPIRATION = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(rrmmdd.Substring(0, 2)), int.Parse(rrmmdd.Substring(2, 2)), int.Parse(rrmmdd.Substring(4, 2)));
                            data = data.Substring(2 + 6);
                        }

                        if ((data.Length > 0) && (data.Substring(0, 2) == ai_lot))
                        {
                            string lot = data.Substring(2);
                            if (lot.Length > 20)
                                throw new Exception(String.Format("GS1 LOT an..20 => {0}", lot.Length));
                            gs1.LOT = lot;
                            data = data.Substring(2 + lot.Length);
                        }

                        if ((data.Length > 0) && (data.Substring(0, 2) == ai_serialnumber))
                        {
                            string serialnumber = data.Substring(2);
                            if (serialnumber.Length > 20)
                                throw new Exception(String.Format("GS1 SN an..20 => {0}", serialnumber.Length));
                            gs1.SerialNumber = serialnumber;
                            data = data.Substring(2 + serialnumber.Length);
                        }

                        if ((data.Length > 0) && (data.Substring(0, 2) == ai_variablecount))
                        {
                            string variablecount = data.Substring(2);
                            if (variablecount.Length > 8)
                                throw new Exception(String.Format("GS1 SN n..8 => Length:{0}", variablecount.Length));
                            if (variablecount.ToCharArray().Any(x => !char.IsNumber(x)))
                                throw new Exception(String.Format("GS1 SN n..8 => NotNumber"));
                            gs1.VariableCount = int.Parse(variablecount);
                            data = data.Substring(2 + variablecount.Length);
                        }

                        int datalengthafter = data.Length;
                        datalenghtchanged = datalengthbefore != datalengthafter;
                    }

                    // pokud neco zbyde, tak to neni fenixovy kod ... 
                    // pokud ale neco chybi, pak to nevadi (nutne...?)
                    if (data.Length > 0)
                        return null;
                }
                return gs1;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null;
            }
        }
    }
}
