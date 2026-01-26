using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.Aktualizace_API.Scanner
{
    public class ScannerNone : ScannerBase
    {
        protected override void InitializeScanner()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void TerminateScanner()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void Enable()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void Disable()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override event ScannerEventHandler DataReady;

        public override void EnableAllBarcodes()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSettingSave()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSettingLoad()
        {
            //throw new Exception("The method or operation is not implemented.");
        }
    }
}
