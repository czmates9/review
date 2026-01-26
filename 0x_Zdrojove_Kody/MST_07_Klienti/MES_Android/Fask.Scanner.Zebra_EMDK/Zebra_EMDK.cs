using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Fask.Interfaces;

using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;

namespace Fask.Scanner.Zebra_EMDK
{
    public class Zebra_EMDK : Java.Lang.Object, IScanner, EMDKManager.IEMDKListener
    {
        ////Assign the profile name used in EMDKConfig.xml  
        //private String profileName = "test";

        ////Declare a variable to store ProfileManager object  
        //private ProfileManager mProfileManager = null;

        EMDKManager emdkManager = null;
        BarcodeManager barcodeManager = null;
        Symbol.XamarinEMDK.Barcode.Scanner scanner = null;

        public event IScanner.ScannerEventHandler ScannerEvent;
        public event IScanner.StatusEventHandler StatusEvent;

        public Context context;

        public Zebra_EMDK(Context context)
        {
            this.context = context;

            #region EMDK

            EMDKResults results = EMDKManager.GetEMDKManager(context, this);
            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                StatusEvent?.Invoke(this, new StatusEventArgs("Status: EMDKManager object creation failed ..."));
            }
            else
            {
                StatusEvent?.Invoke(this, new StatusEventArgs("Status: EMDKManager object creation succeeded ..."));
            }

            #endregion
        }


        #region IScanner

        public void StartScanner()
        {
            if (this.emdkManager != null)
            {

                if (barcodeManager == null)
                {
                    try
                    {

                        //Get the feature object such as BarcodeManager object for accessing the feature.
                        barcodeManager = (BarcodeManager)this.emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);

                        scanner = barcodeManager?.GetDevice(BarcodeManager.DeviceIdentifier.Default);

                        if (scanner != null)
                        {

                            ////Attahch the Data Event handler to get the data callbacks.
                            scanner.Data += Scanner_Data;

                            ////Attach Scanner Status Event to get the status callbacks.
                            scanner.Status += Scanner_Status;

                            scanner.Enable();

                            if (scanner.IsEnabled && !scanner.IsReadPending)
                            {
                                SetScannerConfig();
                            }
                        }
                        else
                        {
                            StatusEvent?.Invoke(this, new StatusEventArgs("Failed to enable scanner.\n"));
                        }
                    }
                    catch (ScannerException e)
                    {
                        StatusEvent?.Invoke(this, new StatusEventArgs("Error: " + e.Message));
                    }
                    catch (Exception ex)
                    {
                        StatusEvent?.Invoke(this, new StatusEventArgs("Error: " + ex.Message));
                    }
                }
            }
        }


        public void StopScanner()
        {
            if (this.emdkManager != null)
            {

                if (scanner != null)
                {
                    try
                    {
                        //bool flag = true;
                        //do
                        //{
                        //    try
                        //    {
                        scanner.CancelRead();
                        scanner.Disable();
                        scanner.Data -= Scanner_Data;
                        scanner.Status -= Scanner_Status;
                        scanner.Release();
                        //flag = false;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //    } 

                        //} while (flag);


                    }
                    catch (ScannerException ex)
                    {
                        //Android.Util.Log.Debug("EMDK scanner", "Exception:" + e.Result.Description);
                        StatusEvent?.Invoke(this, new StatusEventArgs("EMDK scanner Error: " + ex.Message));
                    }
                }

                if (barcodeManager != null)
                {
                    this.emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }
                barcodeManager = null;
                scanner = null;
            }
        }

        private void SetScannerConfig()
        {
            try
            {
                var config = scanner?.GetConfig();
                if (config != null)
                {
                    config.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                    config.ScanParams.DecodeLEDFeedback = true;
                    config.ReaderParams.ReaderSpecific.ImagerSpecific.PicklistEx = ScannerConfig.PicklistEx.Disabled;
                    scanner?.SetConfig(config);
                }
            }
            catch (System.Exception ex)
            {
                ;
            }
        }

        #endregion

        #region EMDK

        public void OnClosed()
        {
            #region Trasovani

            string s = string.Format("{0},{1},{2},True" + System.Environment.NewLine,
    DateTime.Now.ToLongTimeString(),
    "OnClosed",
    context.GetType().FullName
    );
            string PathDir = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "Android_MES");
            string ZivotnyCyklusDir = System.IO.Path.Combine(PathDir, "ZivotnyCyklus.txt");
            Fask.Logging.ExceptionHandler2.Handle(s, ZivotnyCyklusDir);

            #endregion

            KillScanner();
        }

        public void OnOpened(EMDKManager emdkManager)
        {

            #region Trasovani

            string s = string.Format("{0},{1},{2},True" + System.Environment.NewLine,
    DateTime.Now.ToLongTimeString(),
    "OnOpend",
    context.GetType().FullName
    );
            string PathDir = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "Android_MES");
            string ZivotnyCyklusDir = System.IO.Path.Combine(PathDir, "ZivotnyCyklus.txt");
            Fask.Logging.ExceptionHandler2.Handle(s, ZivotnyCyklusDir);

            #endregion

            try
            {
                if (this.emdkManager == null)
                {
                    this.emdkManager = emdkManager;

                    var x = this.emdkManager.ToString();
                    LogName_EMDKManager(x, "OnOpened");

                    //mProfileManager = (ProfileManager)emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Profile);
                    StartScanner();
                }
            }
            catch (Exception ex)
            {
                LogName_EMDKManager(ex.Message, "OnOpened-Exception");
                throw ex;
            }
        }

        new public void Dispose()
        {
            base.Dispose();
        }

        public void KillScanner()
        {
            try
            {
                //Clean up the emdkManager
                if (this.emdkManager != null)
                {
                    //EMDK: Release the EMDK manager object
                    var x = this.emdkManager.ToString();
                    LogName_EMDKManager(x, "KillScanner");
                    this.emdkManager.Release();
                    this.emdkManager = null;
                }
            }
            catch (Exception ex)
            {
                LogName_EMDKManager(ex.Message, "KillScanner-Exception");
                throw ex;
            }
        }

        private void LogName_EMDKManager(string NameEMDK, string source)
        {
            string s = string.Format("{0},{1},{2},{3}" + System.Environment.NewLine,
                DateTime.Now.ToLongTimeString(),
                NameEMDK,
                ((Activity)context).LocalClassName,
                source
                );

            string PathDir = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "EMDK_TEST");
            string ZivotnyCyklusDir = System.IO.Path.Combine(PathDir, "EMDK_Name.txt");
            Fask.Logging.ExceptionHandler2.Handle(s, ZivotnyCyklusDir);
        }

        #endregion

        #region Event metody

        void Scanner_Data(object sender, Symbol.XamarinEMDK.Barcode.Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success))
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();

                foreach (ScanDataCollection.ScanData data in scanData)
                {
                    ScannerEvent?.Invoke(this, new ScannerEventArgs(data.Data, data.LabelType.ToString()));
                }
            }
        }

        void Scanner_Status(object sender, Symbol.XamarinEMDK.Barcode.Scanner.StatusEventArgs e)
        {
            String statusStr = "";

            //EMDK: The status will be returned on multiple cases. Check the state and take the action.
            StatusData.ScannerStates state = e.P0.State;

            if (state == StatusData.ScannerStates.Idle)
            {
                statusStr = "Scanner is idle and ready to submit read.";
                try
                {
                    if (scanner != null)
                    {
                        if (scanner.IsEnabled && !scanner.IsReadPending)
                        {
                            //SetScannerConfig();

                            if (scanner != null)
                                scanner.Read();
                        }
                    }
                }
                catch (ScannerException e1)
                {
                    statusStr = e1.Message;
                }
            }
            if (state == StatusData.ScannerStates.Waiting)
            {
                statusStr = "Waiting for Trigger Press to scan";
            }
            if (state == StatusData.ScannerStates.Scanning)
            {
                statusStr = "Scanning in progress...";
            }
            if (state == StatusData.ScannerStates.Disabled)
            {
                statusStr = "Scanner disabled";
            }
            if (state == StatusData.ScannerStates.Error)
            {
                statusStr = "Error occurred during scanning";

            }

            StatusEvent?.Invoke(this, new StatusEventArgs(statusStr));
        }

        #endregion
    }
}
