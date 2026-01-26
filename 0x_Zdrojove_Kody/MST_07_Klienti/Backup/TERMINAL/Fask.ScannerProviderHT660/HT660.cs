using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.ScannerProvider;

namespace Fask.ScannerProviderHT660
{
    public sealed class ScannerProviderHT660 : Fask.ScannerProvider.IScannerProvider
    {
        public bool ContinuousRead
        {
            get;
            set;
        }
        // scanner
        private USICF.USIClass Scanner;

        public ScannerProviderHT660()
        {
            //try
            //{
            //    Scanner = new USICF.USIClass(null);
            //    InitializeScanner();
            //}
            //catch
            //{
            //}
        }

        public ScannerProviderHT660(System.Windows.Forms.Form parentForm)
        {
            //try
            //{
            //    Scanner = new USICF.USIClass(parentForm);
            //    this.InitializeScanner();
            //}
            //catch
            //{
            //}
        }

        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
            //throw new NotImplementedException();
            this.Scanner = new USICF.USIClass(topLevelForm);
        }

        public void TerminateScanner()
        {
            if (this.Scanner != null)
            {
                this.Scanner.DataReady -= new USICF.USIClass.USIEventHandler(Scanner_DataReady);
                this.Scanner = null;
            }
        }

        public void InitializeScanner()
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
                        BarcodeType.Unknown, // TODO : rozlisit typy carovych kodu
                        e.BarcodeName,
                        e.BarcodeLength)
                );
            }
        }

       public event ScannerEventHandler DataReady;


        public void Enable()
        {
            if (this.Scanner != null)
            {
                this.Scanner.EnableScanner(true);
                _enabled = true;
            }
        }

		//public void Enable(bool toggleSoftTrigger)
		//{
			//if (this.Scanner != null)
			//{
				//this.Scanner.EnableScanner(true);
				//_enabled = true;
			//}
		//}

        public void Disable()
        {
            if (this.Scanner != null)
                this.Scanner.EnableScanner(false);
            _enabled = false;
        }

        public void EnableAllBarcodes()
        {
        }

        public void BarcodeSetting()
        {
            throw new Exception("The method or operation is not implemented.");
            //MsgBox.Show("Použijte globální nastavení z ovládacích panelů", "", System.Windows.Forms.MessageBoxButtons.OK, MsgBoxIcon.Critical, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        }

        public void ScannerSetting()
        {
            throw new Exception("The method or operation is not implemented.");
            //MsgBox.Show("Použijte globální nastavení z ovládacích panelů", "", System.Windows.Forms.MessageBoxButtons.OK, MsgBoxIcon.Critical, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        }

        public void ScannerSettingLoad()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public void ScannerSettingSave()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
        }

        public string ConfigScanner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public AIMTYPE AimType
        {
            set
            {
                return;
            }
            get
            {
                return AIMTYPE.UNKNOWN;
            }
        }

        public void Log_DataReady_Events()
        {
            try
            {
                Logging.Log.Write("HT660 Scanner Target InvocationList ... Start");
                foreach (var dlgt in this.DataReady.GetInvocationList())
                {
                    try
                    {
                        Logging.Log.Write(
                            String.Format("{0},{1},{2},{3}",
                            dlgt.ToString(),
                            dlgt.Method,
                            dlgt.Target.ToString(),
                            dlgt.Target.GetType().ToString()
                            )
                        );

                    }
                    catch (Exception exLogging)
                    {
                        Logging.Log.Write(exLogging);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                Logging.Log.Write("HT660 Scanner Target InvocationList ... End");
            }
        }

        public int SuccessBeepTime { get; set; }

        public Delegate[] InvocationList()
        {
            if (this.DataReady != null)
                return this.DataReady.GetInvocationList();

            return new Delegate[] { };
        }

}
}