using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Scanner
{
    public sealed class ScannerHT660 : ScannerBase
    {
        // scanner
        private USICF.USIClass Scanner;

        public ScannerHT660()
        {
            try
            {
                Scanner = new USICF.USIClass(null);
                InitializeScanner();
            }
            catch
            {
            }
        }

        public ScannerHT660(System.Windows.Forms.Form parentForm)
        {
            try
            {
                Scanner = new USICF.USIClass(parentForm);
                InitializeScanner();
            }
            catch
            {
            }
        }

        public override void TerminateScanner()
        {
            if (this.Scanner != null)
            {
                this.Scanner.DataReady -= new USICF.USIClass.USIEventHandler(Scanner_DataReady);
                this.Scanner = null;
            }
        }

        protected override void InitializeScanner()
        {
            //base.InitializeScanner();
            if (this.Scanner != null)
                this.Scanner.DataReady += new USICF.USIClass.USIEventHandler(Scanner_DataReady);
        }

        void Scanner_DataReady(object sender, USICF.USIEventArgs e)
        {
            if (this.DataReady != null)
            {
                this.DataReady(
                    sender,
                    new ScannerEventArgs(
                        e.BarcodeData,
                        e.BarcodeType,
                        e.BarcodeName,
                        e.BarcodeLength)
                );
            }
        }

        public override event ScannerEventHandler DataReady;


        public override void Enable()
        {
            if (this.Scanner != null)
                this.Scanner.EnableScanner(true);
        }

        public override void Disable()
        {
            if (this.Scanner != null)
                this.Scanner.EnableScanner(false);
        }

        public override void EnableAllBarcodes()
        {
        }

        public override void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            MessageBox.Show("Použijte globální nastavení z ovládacích panelù", "", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        public override void ScannerSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            MessageBox.Show("Použijte globální nastavení z ovládacích panelù", "", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        public override void ScannerSettingLoad()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSettingSave()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
