// Vytvoreno pro fy FENIX

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Parsing.Codes
{
    public class HIBC : BaseCode
        , Interfaces.ICodeItemnmbr
        , Interfaces.ICodeQuantity
        , Interfaces.ICodeSerltnmbr
        , Interfaces.ICodeExpiration
    {
        public override string Nazev => this.GetType().Name;

        // Datamatrix
        public string SUPPLIER_CODE;
        public string MAT_ID;
        public int? QTY;
        public DateTime? DATE;
        public string LOT;
        public string SERIALNUMBER;

        public string SN_or_LOT
        {
            get
            {
                if (!String.IsNullOrEmpty(this.SERIALNUMBER))
                    return this.SERIALNUMBER;
                else if (!String.IsNullOrEmpty(this.LOT))
                    return this.LOT;
                else
                    return string.Empty;
            }
        }

        public HIBC(List<string> codes)
            : base(codes)
        {
        }

        public static HIBC Parse(List<string> codes)
        {
            if (codes.Count != 1)
                return null;

            var data = codes.First();
            return Parse(data);
        }

        public static Task<BaseCode> ParseAsync(List<string> codes)
        {
            TaskCompletionSource<BaseCode> tcs = new TaskCompletionSource<BaseCode>();

            Task.Run(() => {

                if (codes.Count != 1)
                    tcs.SetResult(null);

                var data = codes.First();
                var code = Parse(data);
                tcs.SetResult((BaseCode)code);

            });

            return tcs.Task;
        }

        /// <summary>
        /// Parsovani HIBC kodu pro FENIX
        /// </summary>
        /// <param name="data">carkod ve formatu:
        /// +E2492616643091/$$0422617001663 - MAT ID: 26-166-43-09, expirace 11.04.2022, lot 61700166, +E249 je tuším kód výrobce, 1/$$ bude  separátor, nebo separátory, konečná 3ka netuším, tipuju na kontrolní součet.
        /// </param>
        /// <returns></returns>
        public static HIBC Parse(string data)
        {
            try
            {
                HIBC hibc = new HIBC(new List<string>() { data });

                // implementace dle normy HIBC 2.5 (2015)
                var dataSplit1 = data.Split(@"/".ToCharArray());
                //if (dataSplit1.Length != 4)
                //    return null;

                //vic jak 2 nekaceptuji ... 
                if (dataSplit1.Length != 2)
                    return null;

                string primary = dataSplit1[0];
                string secondary = dataSplit1[1];

                hibc.SUPPLIER_CODE = primary.Substring(0, 5);
                hibc.MAT_ID = primary.Substring(5, primary.Length - 5 - 1);

                //When combining the Primary and Secondary Code into a single symbol (known as concatenation), a forward slash (/) is
                //used as a delimiter between the primary and secondary data. In addition, the primary data Link Character, the plus (+) at the
                //start of the secondary data, and the secondary data Link Character are omitted. Only one Check Character at the end of the
                //symbol will be used which will check the entire data string.
                //For example:
                //+ A 9 9 9 1 2 3 4 5 / $ $ 5 200 0 1 5 1 0 X 3 3
                //Where:
                //  +       HIBC Supplier Labeling flag
                //  A999    LIC
                //  1234    Product ID
                //  5       Unit of Measure
                //  /       Data delimiter (to separate the primary from secondary data)
                //  $$5     Exp Date Flag
                //  20015   Expiry Date is 15 day of year 2020 (15 January 2020) in the YYJJJ format (Julian Date format)
                //  10X3    Lot Number
                //  3       3 is the Check Character

                //Quantity/Date/Lot or Serial Number Reference
                //Identifier

                //Numeric: If the first character is numeric, then R is a
                //fixed 5-digit Julian date. No quantity or
                //Lot/Batch or Serial Number is present (See Note 2)

                //$: If the first character is a “$” and the second character
                //is alphanumeric, then the Quantity and Date fields are
                //not used.

                //$+: If the first two characters are "$+" and the third
                //character is alphanumeric then a serial number only
                //follows. This format is included for backward compatibility
                //only. It is recommended that "$$+7" be used to indicate a
                //serial number follows.

                //$$: If the first two characters are “$$” followed by a digit,
                //then the digit specifies quantity and Date Field format.
                //For use with lot numbers, not serial numbers.

                //$$+: If the first three characters are “$$+” followed by
                //digit, then the digit specifies quantity and date field format.
                //For use with serial numbers, not lot numbers. See
                //Appendix E1.2

                //If the first character is a number then the 5-digit Julian
                //Date format follows. This format is included for backward
                //compatibility only. It is recommend that "$$7" be used to
                //indicate the Julian Date format followed by a lot/batch.

                string datetmp;
                string lottmp;
                string serialtmp;

                if (char.IsDigit(secondary.Substring(0, 1)[0]))
                { // 5-digit Julian date... //YYJJJ
                    datetmp = secondary.Substring(0, secondary.Length - 1);
                    hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1);
                    return hibc;
                }

                if ((secondary.Substring(0, 1)[0] == '$') && (char.IsLetterOrDigit(secondary.Substring(1, 1)[0])))
                { // Quantity and Date fields are not used
                    lottmp = secondary.Substring(1);
                    hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                    return hibc;
                }

                if (secondary.StartsWith(@"$+") && (char.IsLetterOrDigit(secondary.Substring(2, 1)[0])))
                { // serial number only follows
                    serialtmp = secondary.Substring(2);
                    hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1);
                    return hibc;
                }

                #region if (secondary.StartsWith(@"$$") && (char.IsDigit(secondary.Substring(2, 1)[0])))
                if (secondary.StartsWith(@"$$") && (char.IsDigit(secondary.Substring(2, 1)[0])))
                { // digit specifies quantity and Date Field format
                    char format = secondary.Substring(2, 1)[0];
                    string secondary_last_00 = secondary.Substring(2); //zbyvajici cast
                    string secondary_last_01 = secondary.Substring(3); //zbyvajici cast

                    //If there is a two character lot number flag “$$”, or a three character serial number flag “$$+”, following the leading “+”, then
                    //the first digit following will specify the Quantity and Date Field formats:
                    //The digits 0 through 7 indicate that the Quantity Field is null and specify the Date Format:
                    //0, 1 First digit of month in MMYY (month/year) Date format
                    //2 MMDDYY (month/day/year) Date follows
                    //3 YYMMDD (year/month/day) Date follows
                    //4 YYMMDDHH (year/month/day/hour G.M.T.) Date follows
                    //5 YYJJJ (year/Julian day) Date follows
                    //6 YYJJJHH (year/Julian day/hour G.M.T.) Date follows
                    //7 Date Field is null, Lot Field follows
                    //The digits 8 and 9 specify the Quantity Field format. The first digit following the Quantity Field should be 0 through 7
                    //to define the Date Field format as defined above.
                    //8 Two digit Quantity Field follows
                    //9 Five digit Quantity Field follows
                    if (format == '0' || format == '1')
                    {
                        datetmp = secondary_last_00.Substring(0, 4); //MMYY
                        lottmp = secondary_last_00.Substring(4); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(0, 2)), 0);
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '2')
                    {
                        datetmp = secondary_last_00.Substring(0, 6); //MMDDYY
                        lottmp = secondary_last_00.Substring(6); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)));
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '3')
                    {
                        datetmp = secondary_last_00.Substring(0, 6); //YYMMDD
                        lottmp = secondary_last_00.Substring(6); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)));
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '4')
                    {
                        datetmp = secondary_last_00.Substring(0, 8); //YYMMDDHH
                        lottmp = secondary_last_00.Substring(8); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2))).AddHours(int.Parse(datetmp.Substring(6, 2)));
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '5')
                    {
                        datetmp = secondary_last_00.Substring(0, 5); //YYJJJ
                        lottmp = secondary_last_00.Substring(5); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1);
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '6')
                    {
                        datetmp = secondary_last_00.Substring(0, 7); //YYJJJHH
                        lottmp = secondary_last_00.Substring(7); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1).AddHours(int.Parse(datetmp.Substring(5, 2)));
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '7')
                    {
                        //string datetmp = secondary_last_00.Substring(0, 7); //Nodate
                        lottmp = secondary_last_00.Substring(1); //do konce
                        //hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1).AddHours(int.Parse(datetmp.Substring(5, 2)));
                        hibc.LOT = lottmp.Substring(0, lottmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    #region 8 Two digit Quantity Field follows
                    if (format == '8')
                    {
                        //+$$ 8 QQ ...
                        int quantity = int.Parse(secondary_last_01.Substring(0, 2));
                        hibc.QTY = quantity;
                        string secondary_last_02 = secondary_last_01.Substring(2);
                        char date_format = secondary_last_02[0];
                        switch (date_format)
                        {
                            //+$$ 8 QQ 2 MMDDYY LOT L C +$$82420928053C001LC
                            case '2':
                                datetmp = secondary_last_02.Substring(1, 6);
                                lottmp = secondary_last_02.Substring(7);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 8 QQ 3 YYMMDD LOT L C +$$82430509283C001LC
                            case '3':
                                datetmp = secondary_last_02.Substring(1, 6);
                                lottmp = secondary_last_02.Substring(7);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 8 QQ 4 YYMMDDHH LOT L C +$$8244050928223C001LC
                            case '4':
                                datetmp = secondary_last_02.Substring(1, 8);
                                lottmp = secondary_last_02.Substring(9);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2))).AddHours(int.Parse(datetmp.Substring(6, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 8 QQ 5 YYJJJ LOT L C +$$8245052713C001LC
                            case '5':
                                datetmp = secondary_last_02.Substring(1, 5);
                                lottmp = secondary_last_02.Substring(6);
                                // upravit
                                //hibc.EXPIRATION = new DateTime(int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(6, 2)), 0, 0);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 8 QQ 6 YYJJJHH LOT L C +$$824605271223C001LC
                            case '6':
                                datetmp = secondary_last_02.Substring(1, 7);
                                lottmp = secondary_last_02.Substring(8);
                                // upravit
                                //hibc.EXPIRATION = new DateTime(int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(6, 2)), 0, 0);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1).AddHours(int.Parse(datetmp.Substring(5, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 8 QQ 7 LOT L C +$$82473C001LC
                            case '7':
                                lottmp = secondary_last_02.Substring(1);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 8 QQ MMYY LOT L C +$$82409053C001LC
                            //+$$ 8 QQ L C +$$824LC
                            default:
                                if (secondary_last_02.Length <= 1) //neobsahuje expiraci ani sarzi ... 
                                    return hibc;
                                datetmp = secondary_last_02.Substring(0, 4);
                                lottmp = secondary_last_02.Substring(4);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(0, 2)), 0);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                        }
                    }
                    #endregion

                    #region 9 Five digit Quantity Field follows
                    if (format == '9')
                    {
                        //+$$ 9 QQQQQ ...
                        int quantity = int.Parse(secondary_last_01.Substring(0, 5));
                        hibc.QTY = quantity;
                        string secondary_last_02 = secondary_last_01.Substring(5);
                        char date_format = secondary_last_02[0];
                        switch (date_format)
                        {
                            //+$$ 9 QQQQQ 2 MMDDYY LOT L C +$$82420928053C001LC
                            case '2':
                                datetmp = secondary_last_02.Substring(1, 6);
                                lottmp = secondary_last_02.Substring(7);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 9 QQQQQ 3 YYMMDD LOT L C +$$82430509283C001LC
                            case '3':
                                datetmp = secondary_last_02.Substring(1, 6);
                                lottmp = secondary_last_02.Substring(7);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 9 QQQQQ 4 YYMMDDHH LOT L C +$$8244050928223C001LC
                            case '4':
                                datetmp = secondary_last_02.Substring(1, 8);
                                lottmp = secondary_last_02.Substring(9);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(6, 2)), 0, 0);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 9 QQQQQ 5 YYJJJ LOT L C +$$8245052713C001LC
                            case '5':
                                datetmp = secondary_last_02.Substring(1, 5);
                                lottmp = secondary_last_02.Substring(6);
                                // upravit
                                //hibc.EXPIRATION = new DateTime(int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(6, 2)), 0, 0);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 9 QQQQQ 6 YYJJJHH LOT L C +$$824605271223C001LC
                            case '6':
                                datetmp = secondary_last_02.Substring(1, 7);
                                lottmp = secondary_last_02.Substring(8);
                                // upravit
                                //hibc.EXPIRATION = new DateTime(int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(6, 2)), 0, 0);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1).AddHours(int.Parse(datetmp.Substring(5, 2)));
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 9 QQQQQ 7 LOT L C +$$82473C001LC
                            case '7':
                                lottmp = secondary_last_02.Substring(1);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                            //+$$ 9 QQQQQ MMYY LOT L C +$$82409053C001LC
                            //+$$ 9 QQQQQ L C +$$900100LC
                            default:
                                if (secondary_last_02.Length <= 1) //neobsahuje expiraci ani sarzi ... 
                                    return hibc;
                                datetmp = secondary_last_02.Substring(0, 4);
                                lottmp = secondary_last_02.Substring(4);
                                hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(0, 2)), 0);
                                hibc.LOT = lottmp.Substring(0, lottmp.Length - 1);
                                return hibc;
                                //break;
                        }
                    }
                    #endregion

                }
                #endregion

                #region if (secondary.StartsWith(@"$$+") && (char.IsDigit(secondary.Substring(3, 1)[0])))
                if (secondary.StartsWith(@"$$+") && (char.IsDigit(secondary.Substring(3, 1)[0])))
                { // digit specifies quantity and date field format
                    char format = secondary.Substring(2, 1)[0];
                    string secondary_last_00 = secondary.Substring(2); //zbyvajici cast
                    string secondary_last_01 = secondary.Substring(3); //zbyvajici cast

                    //If there is a two character lot number flag “$$”, or a three character serial number flag “$$+”, following the leading “+”, then
                    //the first digit following will specify the Quantity and Date Field formats:
                    //The digits 0 through 7 indicate that the Quantity Field is null and specify the Date Format:
                    //0, 1 First digit of month in MMYY (month/year) Date format
                    //2 MMDDYY (month/day/year) Date follows
                    //3 YYMMDD (year/month/day) Date follows
                    //4 YYMMDDHH (year/month/day/hour G.M.T.) Date follows
                    //5 YYJJJ (year/Julian day) Date follows
                    //6 YYJJJHH (year/Julian day/hour G.M.T.) Date follows
                    //7 Date Field is null, Lot Field follows
                    if (format == '0' || format == '1')
                    {
                        datetmp = secondary_last_00.Substring(0, 4); //MMYY
                        serialtmp = secondary_last_00.Substring(4); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(0, 2)), 0);
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '2')
                    {
                        datetmp = secondary_last_00.Substring(0, 6); //MMDDYY
                        serialtmp = secondary_last_00.Substring(6); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(4, 2)), int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)));
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '3')
                    {
                        datetmp = secondary_last_00.Substring(0, 6); //YYMMDD
                        serialtmp = secondary_last_00.Substring(6); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2)));
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '4')
                    {
                        datetmp = secondary_last_00.Substring(0, 8); //YYMMDDHH
                        serialtmp = secondary_last_00.Substring(8); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), int.Parse(datetmp.Substring(2, 2)), int.Parse(datetmp.Substring(4, 2))).AddHours(int.Parse(datetmp.Substring(6, 2)));
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '5')
                    {
                        datetmp = secondary_last_00.Substring(0, 5); //YYJJJ
                        serialtmp = secondary_last_00.Substring(5); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1);
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '6')
                    {
                        datetmp = secondary_last_00.Substring(0, 7); //YYJJJHH
                        serialtmp = secondary_last_00.Substring(7); //do konce
                        hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1).AddHours(int.Parse(datetmp.Substring(5, 2)));
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }

                    if (format == '7')
                    {
                        //string datetmp = secondary_last_00.Substring(0, 7); //Nodate
                        serialtmp = secondary_last_00.Substring(1); //do konce
                        //hibc.DATE = new DateTime((DateTime.Now.Year / 100) * 100 + int.Parse(datetmp.Substring(0, 2)), 1, 1).AddDays(int.Parse(datetmp.Substring(2, 3)) - 1).AddHours(int.Parse(datetmp.Substring(5, 2)));
                        hibc.SERIALNUMBER = serialtmp.Substring(0, serialtmp.Length - 1); //ale musi byt o jedno mensi ... 
                        return hibc;
                    }
                }
                #endregion

                return null;
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

        #region ICodeItemnmbr Members

        public string Itemnmbr
        {
            get
            {
                //throw new NotImplementedException();
                return this.MAT_ID;
            }
        }

        #endregion

        #region ICodeQuantity Members

        public decimal? Quantity
        {
            get
            {
                //throw new NotImplementedException();
                return this.QTY;
            }
        }

        #endregion

        #region ICodeSerltnmbr Members

        public string Serltnmbr
        {
            get
            {
                //throw new NotImplementedException();
                return this.SN_or_LOT;
            }
        }

        #endregion

        #region ICodeExpiration Members

        public DateTime? Expiration
        {
            get
            {
                //throw new NotImplementedException();
                return this.DATE;
            }
        }

        #endregion
    }
}
