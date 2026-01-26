using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing
{
    public class Config
    {
        public bool WeightCode = false;
		public bool WeightCode_12 = false;
		//public bool BarcodeSlashSarze = false;
		//public bool FenixBarcodeObal = false;
		//public bool FenixHIBC = false;
		//public bool FenixGS1 = false;

        public Config()
        {
        }

        public Config(
            bool WeightCode,
			bool WeightCode_12
			//,bool BarcodeSlashSarze,
			//bool FenixBarcodeObal,
			//bool FenixHIBC,
			//bool FenixGS1
            )
        {
            this.WeightCode = WeightCode;
			this.WeightCode_12 = WeightCode_12;
			//this.BarcodeSlashSarze = BarcodeSlashSarze;
			//this.FenixBarcodeObal = FenixBarcodeObal;
			//this.FenixHIBC = FenixHIBC;
			//this.FenixGS1 = FenixGS1;
        }
    }
}
