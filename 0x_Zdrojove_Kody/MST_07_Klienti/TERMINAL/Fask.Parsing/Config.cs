using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing
{
	public class Config
	{
		public bool WeightCode_12 = false;
		public bool WeightCode = false;
		public bool SABNeznamyKod = false;
		public bool BarcodeSlashSarze = false;
		public bool FenixBarcodeObal = false;
		public bool HIBC = false;
		public bool GS1 = false;
		public bool SAB_AustralianNorm = false;
        public bool SAB_GS1_Zavorky = false;
		public bool SAB_GS1_BALTON = false;
		public bool InnovaMedical_GS1_Datum = false;
		public bool SAB_GS1_BELDICO = false;

		public Config()
		{
		}

		public Config(
			bool WeightCode_12,
			bool WeightCode,
			bool SABNeznamyKod,
			bool BarcodeSlashSarze,
			bool FenixBarcodeObal,
			bool HIBC,
			bool GS1,
            bool SAB_AustralianNorm,
            bool SAB_GS1_Zavorky,
			bool SAB_GS1_BALTON,
			bool InnovaMedical_GS1_Datum,
			bool SAB_GS1_BELDICO
			)
		{
			this.WeightCode_12 = WeightCode_12;
			this.WeightCode = WeightCode;
			this.SABNeznamyKod = SABNeznamyKod;
			this.BarcodeSlashSarze = BarcodeSlashSarze;
			this.FenixBarcodeObal = FenixBarcodeObal;
			this.HIBC = HIBC;
			this.GS1 = GS1;
			this.SAB_AustralianNorm = SAB_AustralianNorm;
            this.SAB_GS1_Zavorky = SAB_GS1_Zavorky;
			this.SAB_GS1_BALTON = SAB_GS1_BALTON;
			this.InnovaMedical_GS1_Datum = InnovaMedical_GS1_Datum;
			this.SAB_GS1_BELDICO = SAB_GS1_BELDICO;
	}
}
}
