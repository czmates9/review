using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.Vyroba_W
{
    public class Constants
    {
		public const string PRD = ".prd";
		public const string TMP = ".tmp";

		public const string InternalState = "InternalState";
        public const string Vyroba = "Vyroba";
        public const string Production = "Production";
        public const string Baleni = "Baleni";

        public const string VyrobaPrd = Vyroba + PRD;
        public const string VyrobaPrdTmp = VyrobaPrd + TMP;

        public const string ProductionPrd = Production + PRD;
        public const string ProductionPrdTmp = ProductionPrd + TMP;

        public const string SoundChimes = "chimes.wav";
        public const string SoundDotaz = "dotaz.wav";
        public const string SoundChyba = "chyba.wav";
        public const string SoundInfo = "info.wav";

        public const string ConfigScanner = "ScannerConfig.xml";

		// Pro komunikaci s serverem
		public const string ASMX = ".asmx";
		public const string ZIP = ".zip";

		public const string SQLiteDBs = @"SQLiteDBs/";
		public const string Slash = @"/";

		public const string Vyroba_asmx = Vyroba + ASMX;
		public const string Baleni_asmx = Baleni + ASMX;

    }

    //public class UEventStatusTypes
    //{
    //    public const string SmenaLogin = "1";
    //    public const string SmenaLogout = "2";
    //    public const string PracovnikPrihlaseni = "3";
    //    public const string PracovnikOdhlaseni = "4";
    //}
}
