using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Module.Inside.Inside_mobile.CommonConstant
{
    public static class DocumentTypeCode
    {
        /*
         * AT doklady
         */
        public const string TRADE_IN_COMMISSION = "TIC";
        public const string TRADE_IN_ORDER = "TIO";
        public const string TRADE_IN_AGREEMENT = "TIA";
        public const string TRADE_IN_BOOKING = "TIB";
        public const string TRADE_IN_STOCK_MOVEMENT = "TIS";
        public const string TRADE_IN_DELIVERY_NOTE = "TID";
        public const string TRADE_IN_INVOICE = "TII";
        public const string TRADE_OUT_COMMISSION = "TOC";
        public const string TRADE_OUT_ORDER = "TOO";
        public const string TRADE_OUT_AGREEMENT = "TOA";
        public const string TRADE_OUT_BOOKING = "TOB";
        public const string TRADE_OUT_STOCK_MOVEMENT = "TOS";
        public const string TRADE_OUT_DELIVERY_NOTE = "TOD";
        public const string TRADE_OUT_INVOICE = "TOI";
        public const string PRODUCTION_COMMISSION = "PC";
        public const string PRODUCTION_ORDER = "PO";
        public const string PRODUCTION_AGREEMENT = "PA";
        public const string PRODUCTION_BOOKING = "PB";
        public const string PRODUCTION_STOCK_MOVEMENT = "PS";
        public const string PRODUCTION_DELIVERY_NOTE = "PD";
        public const string PRODUCTION_INVOICE = "PI";
        public const string INTERNAL_COMMISSION = "IC";
        public const string INTERNAL_ORDER = "IO";
        public const string INTERNAL_AGREEMENT = "IA";
        public const string INTERNAL_BOOKING = "IB";
        public const string INTERNAL_STOCK_MOVEMENT = "IS";
        public const string INTERNAL_DELIVERY_NOTE = "ID";
        public const string INTERNAL_INVOICE = "II";

        /*
         * Ostatní doklady
         */
        public const string STOCKTAKING = "S";
        public const string FINANCIAL_BANK_STATEMENT = "FBS";
        public const string FINANCIAL_BANK_ORDER = "FBO";
        public const string FINANCIAL_CASH = "FC";
        public const string FINANCIAL_CASH_INPUT = "FCI";
        public const string FINANCIAL_CASH_OUTPUT = "FCO";
        public const string FINANCIAL_INTERNAL = "FI";

        /*
         * Nove moduly pro FASK ???
         */
        public const string KOOPOUT = "KOOPOUT";
        public const string KOOPIN = "KOOPIN";
        public const string EXP = "EXP";
    }
}
