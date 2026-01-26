// Vytvoreno pro fy FENIX

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
    public class InnovaMedical_GS1_Datum : BaseCode
        , Interfaces.ICodeBarcode
        //, Interfaces.ICodeSerltnmbr
        , Interfaces.ICodeExpiration
        , Interfaces.ICodeQuantity
        , Interfaces.ICodeProductionDate
		, Interfaces.ICodeSerialNumber
		, Interfaces.IPocetNasnimanychVariant
		, Interfaces.ICodeSarze
		, Interfaces.ICodeInterniFiremni93
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
        /// 5.2.1 DATUM VÝROBY
        /// 11 datum výroby (RRMMDD) n2+n6
        /// Datum výroby (Production Date – PROD DATE) je datum dne, ve kterém byl produkt
        /// vyroben.
        /// </summary>
        public const string ai_production_date = "11";

        /// <summary>
        /// Expiration (Expirace)
        /// 5.2.6 POUŽITELNOST DO
        /// 17 použitelnost do (RRMMDD) n2+n6
        /// Použitelnost do… (Expiration Date – USE BY či EXPIRY) označuje finální limit spotřeby
        /// či použití produktu. V sektoru zdravotnictví se používá pro vyjádření data exspirace.
        /// </summary>
        public const string ai_expiration = "17";

        ///<summary>
        ///5.1.3 VARIANTA PRODUKTU
        ///20 varianta produktu n2+n2
        ///Doplňkový kód s GS1 AI (20) označuje variantu produktu. Tato metoda odlišení
        ///variant smí být použita tehdy, když odlišnost není natolik významná, aby vyžadovala
        ///změnu GTIN. Datové pole je dvoumístné a jeho využití je ponecháno na rozhodnutí
        ///výrobce. GS1 AI (20) musí být vždy uváděn ve zřejmé souvislosti s příslušným GTIN.
        ///</summary>
        public const string ai_variantaProduktu = "20";

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

		/// <summary>
		/// 5.3.2 MNOŽSTVÍ
		/// 37 množství n2+n..8
		/// GS1 AI (37) musí být používán zásadně v kombinaci s GS1 AI (02). Jde o vyjádření
		/// množství (počtu kusů - COUNT) nejbližších nižších jednotek, které jsou obsažené
		/// v logistické jednotce.
		/// </summary>
		public const string ai_count = "37";

		/// <summary>
		///5.10.2 INTERNÍ POUŽITÍ
		///91-99 interní použití, firemní informace n2+an..30
		///GS1 aplikační identifikátory (91) až (99) jsou přiděleny pro interní použití v dané
		///společnosti a nepředpokládá se jejich zpracování mimo danou organizaci.
		///Tato interní identifikace by měla být z důvodu nejednoznačnosti vždy odstraněna
		///ještě před tím, než produkt opustí společnost.
		/// </summary>
		public const string ai_interniParam93 = "93";

		/// <summary>
		///5.1.7 DOPLŇKOVÁ IDENTIFIKACE PRODUKTU
		//GS1 AI (240) je určen ke kódování doplňkové identifikace produktu. Data jsou
		//strukturována podle uvážení výrobce. Obvykle je používán pro interní identifikaci
		//produktu, nebo jako odkaz na katalogové číslo.
		//Upozornění: GS1 AI (240) představuje pouze doplňující identifikaci a nemůže nahradit
		//GTIN. GS1 AI (240) musí být vždy vázán na příslušný GTIN.
		/// </summary>
		public const string ai_DoplnInfoProdukt240 = "240";

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
        /// SAB - rozpad GTIN14 na varianty EAN14, EAN13(EAN12), EAN8
        /// - v pripade, ze na 1.pozici bude 1-9, budeme vracet celych 14 znaku
        /// - v pripade, ze na 1. a 2. pozici bude 0, budeme vracet 13 znaku pro EAN-13 a EAN-12(podmnozina EAN-13)
        /// - v pripade, kdy bude na 1.-6. pozici 0, budeme vracet 8 znaku (EAN-8)
        /// !!! variantu pro GTIN-12 (UPC-A): this is a 12-digit number used primarily in North America, v teto variante nepodporujeme
        /// </summary>
        public string GTIN_EANs
        {
            get
            {
                try
                {
                    // gtin have to have exactly 14 chars(length 14)
                    string gtin = this.GTIN;
                    // tests
                    if (string.IsNullOrEmpty(gtin))
                        return string.Empty;
                    if (gtin.Length != 14)
                        throw new Exception(String.Format("GTIN length is not 14 chars: {0}", gtin));

                    // parsing
                    if (gtin[0] != '0') // 1. znak je 1-9 => vratit celych 14 znaku
                        return gtin;
                    if (gtin[1] != '0') // 1.znak je [0], 2. znak je [1-9] => 13 znaku => EAN13
                        return gtin.Substring(1);
                    if (gtin[2] != '0') // 1. a 2. znak je [0], 3. znak je [1-9] => EAN12(UPC-A) vratit jako EAN3 => 13 znaku
                        return gtin.Substring(1);
                    if ((gtin.Substring(0, 6) == new string('0', 6))) // 1.- 6.znak je 0 => EAN8 => vratit 8 znaku
                        return gtin.Substring(6);
                    else
                        return gtin.Substring(1);   // vrati 13 znaku => EAN13
                }
                catch (Exception exParse)
                {
                    Logging.Log.Write(exParse);
                    return string.Empty;
                }
            }
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
        /// Datum výroby
        /// </summary>
        public DateTime? PRODUCTIONDATE
        {
            get
            {
                string production_date = AIValueGet(ai_production_date);
                if (production_date != null)
                    return DateTime.Parse(production_date);
                else
                    return null;
            }
            set
            {
                AIValueSet(ai_production_date, value.Value.ToString());
            }
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

		//public string SN_or_LOT
		//{
		//    get
		//    {
		//        if (!String.IsNullOrEmpty(this.SerialNumber))
		//            return this.SerialNumber;
		//        else if (!String.IsNullOrEmpty(this.LOT))
		//            return this.LOT;
		//        else
		//            return string.Empty;
		//    }
		//}

        public int? VariantaProduktu
        {
            get
            {
                try
                {
                    string variantaProduktu = AIValueGet(ai_variantaProduktu);
                    if (variantaProduktu != null)
                        return int.Parse(variantaProduktu);
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
                    AIValueSet(ai_variantaProduktu, value.Value.ToString());
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
					AIValueSet(ai_variablecount, value.Value.ToString());
            }
        }

        /// <summary>
        /// Mnozstvi
        /// </summary>
        public int? COUNT
        {
            get
            {
                try
                {
                    string count = AIValueGet(ai_count);
                    if (count != null)
                        return int.Parse(count);
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
                    AIValueSet(ai_count, value.Value.ToString());
            }
        }

		/// <summary>
		/// INTERNÍ POUŽITÍ
		/// AI = 93
		/// </summary>
		public string Param93
		{
			get { return AIValueGet(ai_interniParam93); }
			set { AIValueSet(ai_interniParam93, value); }
		}

		/// <summary>
		/// Doplnove info produktu
		/// AI = 240
		/// </summary>
		public string DoplnInfoProdukt240
		{
			get { return AIValueGet(ai_DoplnInfoProdukt240); }
			set { AIValueSet(ai_DoplnInfoProdukt240, value); }
		}

		public int _pocetNalezenychVariant = 0;

		public InnovaMedical_GS1_Datum(List<string> codes)
            : base(codes)
        {
        }

		public static InnovaMedical_GS1_Datum Parse(string data)
        {
            return Parse(new List<string>() { data });
        }

		public static InnovaMedical_GS1_Datum Parse(List<string> list_dataFull)
        {
            try
            {
                if (list_dataFull.Count == 0)
                    return null;

				InnovaMedical_GS1_Datum gs1 = new InnovaMedical_GS1_Datum(list_dataFull);

                foreach (var dataFull in list_dataFull)
                {
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
								gs1._pocetNalezenychVariant++;
                            }

                            if ((data.Length > 0) && (data.Substring(0, 2) == ai_lot))
                            {
                                string lot = data.Substring(2);
                                if (lot.Length > 20)
                                    throw new Exception(String.Format("GS1 LOT an..20 => {0}", lot.Length));
                                gs1.LOT = lot;
                                data = data.Substring(2 + lot.Length);
								gs1._pocetNalezenychVariant++;
                            }

                            if ((data.Length > 0) && (data.Substring(0, 2) == ai_production_date)) // (11) datum vyroby
                            {
                                string productiondate_rrmmdd = data.Substring(2, 6);
                                gs1.PRODUCTIONDATE = Common.Dates.Date_RRMMDD(productiondate_rrmmdd);
                                data = data.Substring(2 + 6);
								gs1._pocetNalezenychVariant++;
                            }

                            if ((data.Length > 0) && (data.Substring(0, 2) == ai_expiration)) // (17) Pouzitelnost do (expirace)
                            {
                                string rrmmdd = data.Substring(2, 6);

								if (!string.IsNullOrEmpty(rrmmdd) && rrmmdd.Length == 6)
								{
									string YY = rrmmdd.Substring(0, 2);
									string MM = rrmmdd.Substring(2, 2);
									string DD = rrmmdd.Substring(4, 2);

									if (DD == "00")
									{
										DD = "28";
									}

									gs1.EXPIRATION = Common.Dates.Date_RRMMDD(YY + MM + DD);
									data = data.Substring(2 + 6);
									gs1._pocetNalezenychVariant++;
								}
								else
									throw new Exception("Expirace neni korektní");
                            }


                            if ((data.Length > 0) && (data.Substring(0,2) == ai_variantaProduktu))
                            {
                                string variantaProduktu = data.Substring(2, 2);
                                gs1.VariantaProduktu = int.Parse(variantaProduktu);
                                data = data.Substring(2 + 2);
								gs1._pocetNalezenychVariant++;
                            }

                            if ((data.Length > 0) && (data.Substring(0, 2) == ai_serialnumber))
                            {
                                string serialnumber = data.Substring(2);
                                if (serialnumber.Length > 20)
                                    throw new Exception(String.Format("GS1 SN an..20 => {0}", serialnumber.Length));
                                gs1.SerialNumber = serialnumber;
                                data = data.Substring(2 + serialnumber.Length);
								gs1._pocetNalezenychVariant++;
                            }

                            if ((data.Length > 0) && (data.Substring(0, 2) == ai_variablecount)) // (30) Variable count
                            {
                                string variablecount = data.Substring(2);
                                if (variablecount.Length > 8)
                                    throw new Exception(String.Format("GS1 Variable Count n..8 => Length:{0}", variablecount.Length));
                                if (variablecount.ToCharArray().Any(x => !char.IsNumber(x)))
                                    throw new Exception(String.Format("GS1 Variable Count n..8 => NotNumber"));
                                gs1.VariableCount = int.Parse(variablecount);
                                data = data.Substring(2 + variablecount.Length);
								gs1._pocetNalezenychVariant++;
                            }

                            if ((data.Length > 0) && (data.Substring(0, 2) == ai_count)) // (37) count
                            {
                                string count = data.Substring(2);
                                if (count.Length > 8)
                                    throw new Exception(String.Format("GS1 Count n..8 => Length:{0}", count.Length));
                                if (count.ToCharArray().Any(x => !char.IsNumber(x)))
                                    throw new Exception(String.Format("GS1 Count n..8 => NotNumber"));
                                gs1.COUNT = int.Parse(count);
                                data = data.Substring(2 + count.Length);
								gs1._pocetNalezenychVariant++;
                            }

							if ((data.Length > 0) && (data.Substring(0, 2) == ai_interniParam93)) // (93) Uživatelsky parametr
							{
								string param93 = data.Substring(2);
								if (param93.Length > 30)
									throw new Exception(String.Format("GS1 interní použití, firemní informace an..30 => {0}", param93.Length));
								gs1.Param93 = param93;
								data = data.Substring(2 + param93.Length);
								gs1._pocetNalezenychVariant++;
							}
							if ((data.Length > 0) && (data.Substring(0, 3) == ai_DoplnInfoProdukt240)) // (240) DOPLŇKOVÁ IDENTIFIKACE PRODUKTU
							{
								string dopPar240 = data.Substring(3);
								if (dopPar240.Length > 30)
									throw new Exception(String.Format("GS1 doplňková identifikace produktu an..30 => {0}", dopPar240.Length));
								gs1.DoplnInfoProdukt240 = dopPar240;
								data = data.Substring(3 + dopPar240.Length);
								gs1._pocetNalezenychVariant++;
							}

                            int datalengthafter = data.Length;
                            datalenghtchanged = datalengthbefore != datalengthafter;
                        }

                        // pokud neco zbyde, tak to neni fenixovy kod ... 
                        // pokud ale neco chybi, pak to nevadi (nutne...?)
                        if (data.Length > 0)
                            return null;
                    }
                }
                return gs1;
            }
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //    return null;
            //}
            catch
            {
                return null;
            }
        }

        #region ICodeBarcode Members

        public string Barcode
        {
            get
            {
                //throw new NotImplementedException();
                //return this.GTIN_13;
                return this.GTIN_EANs;  // 2.6.2020 JiS : for SAB 
            }
        }

        #endregion

		//#region ICodeSerltnmbr Members

		///// <summary>
		///// Vraci informaci, zda je v carovem kodu k dispozici Sarze vyrobku
		///// </summary>
		///// <remarks>Zvazit, zda by nemelo vracet zavisle SN_or_LOT.</remarks>
		//public string Serltnmbr
		//{
		//    get
		//    {
		//        //throw new NotImplementedException();
		//        //return this.LOT;
		//        return this.SN_or_LOT;
		//    }
		//}

		//#endregion

        #region ICodeExpiration Members

        public DateTime? Expiration
        {
            get
            {
                //throw new NotImplementedException();
                return this.EXPIRATION;
            }
        }

        #endregion

        #region ICodeQuantity Members

        public decimal? Quantity
        {
            get
            {
                if (this.COUNT.HasValue)
                    return this.COUNT;
                else if (this.VariableCount.HasValue)
                    return this.VariableCount;
                else
                    return null;
            }
        }

        #endregion

        #region ICodeProductionDate Members

        public DateTime? ProductionDate
        {
            get { return this.PRODUCTIONDATE; }
        }

        #endregion
    
		#region ICodeSerialNumber Members

        public string SN
        {
            get
            {
                var s = this.SerialNumber;

                if (String.IsNullOrEmpty(s))
                    return string.Empty;

                return s;
            }
        }

		#endregion

		#region IPocetNasnimanychVariant Members

		public int PocetNasnimanychVariant
		{
			get { return _pocetNalezenychVariant; }
		}

		#endregion

		#region ICodeSarze Members

		public string Sarze
		{
			get
			{
				var s = this.LOT;

				if (String.IsNullOrEmpty(s))
					return string.Empty;

				return s;
			}
		}

		#endregion

		#region ICodeInterniFiremni93 Members

		public string InterniParametr
		{
			get
			{
				var s = this.Param93;

				if (String.IsNullOrEmpty(s))
					return string.Empty;

				return s;
			}
		}

		#endregion
	}
}
