using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.ScannerProvider
{
    public interface IScannerProvider
    {
        bool ContinuousRead { get; set; }
        int SuccessBeepTime { get; set; }
        AIMTYPE AimType { get; set; }
        bool Enabled { get; }
        string ConfigScanner { get; set; }

        //ScannerProvider.IScannerProvider Scanner { get; set; }

        void SetForm(System.Windows.Forms.Form topLevelForm);

        void InitializeScanner();
        void TerminateScanner();
        void Enable();
	//void Enable(bool toggleSoftTrigger);
        void Disable();
        event ScannerEventHandler DataReady;
        void EnableAllBarcodes();

        void ScannerSetting();
        void BarcodeSetting();

        void ScannerSettingSave();
        void ScannerSettingLoad();
        void Log_DataReady_Events();

        Delegate[] InvocationList();
    }

    public delegate void ScannerEventHandler(object sender, ScannerEventArgs e);

    public enum ScannerTypes
    {
        None,
        Unitech_HT660,
        Symbol_MC3090,
        PSION
    }

    public enum BarcodeType
    {
        Unknown = 0,
        EAN128 = 1,
        EAN13 = 2,
        EAN8 = 3,
        CODE128 = 4,
        CODE32 = 5,
        CODE39 = 6,
        CODE93 = 7,
        I2OF5 = 8,
		UPCA = 9
    }

    public enum AIMTYPE
    {
        UNKNOWN = -1,
        // Summary:
        //     Dual-stage trigger based aiming; The standard triggering mode that remains
        //     idle until the trigger is pressed.  Once the trigger is pressed a decode
        //     session is started. The decode session remains active until a barcode is
        //     decoded, the BeamTimer is expired or the trigger is released.
        TRIGGER = 0,
        //
        // Summary:
        //     Timed hold aim type; The scan status is idle until the trigger is pressed.
        //     Once pressed an aiming session is started for a time specified by AimDuration,
        //     when this time expires a decode session is started.  The decode session will
        //     remain active until the BeamTimer expires, the trigger is released or a barcode
        //     is decoded.
        TIMED_HOLD = 1,
        //
        // Summary:
        //     Timed release aim type; The scan status is idle until the trigger is pressed.
        //     Once pressed an aiming session is started and will continue until the trigger
        //     is released.If the AimDuration is expired when the trigger is released then
        //     a decode session will be started for a remaining time equal to BeamTimer
        //     or a barcode is decoded.
        TIMED_RELEASE = 2,
        //
        // Summary:
        //     Press and release aim type; the scan status goes from idle to scanning by
        //     pressing and releasing the trigger. The decode session will remain active
        //     until the BeamTimer expired or a barcode is decoded.  This is not a valid
        //     setting for the laser barcode readers.
        PRESS_AND_RELEASE = 3,
        //
        // Summary:
        //     Presentaion aim type; Appears idle until motion is detected in front of imager
        //     window at which time illumination is turned on along with the aiming pattern
        //     and a decode is attempted.  Currently only the MK500 imager device supports
        //     this feature and works only when the soft trigger is enabled.
        //PRESENTATION = 4,
        //
        // Summary:
        //     Trigger continuously; In this mode once the trigger is pulled the user can
        //     continue scanning barcodes without releasing the trigger as long as new reads
        //     are submitted as soon as the earlier read is satisfied. This mode is useful
        //     when the user wants to perform rapid scanning. To provide better control
        //     over this feature we have added the two new reader parameters (SameSymbolTimeout,
        //     DifferentSymbolTimeout) that are associated with continuous reads. These
        //     reader parameters are available in both IMAGER_SPECIFIC and LASER_SPECIFIC
        //     classes.  NOTE: The following must be considered when using this AIM_TYPE_CONTINUOUS_READ
        //     mode.  1. After each successful read, the application will have to submit
        //     a new read for rapid triggering. 2. It is recommended that the Picklist mode
        //     be enabled for the imager-class scanners.  3. When using this mode, the IMAGER_SPECIFIC.VFFeedback
        //     parameter will be ignored and no viewfinder feedback will be provided.  It
        //     is similar to setting IMAGER_SPECIFIC.VFFeedback to VIEWFINDER_FEEDBACK.VIEWFINDER_MODE_DISABLED
        //     4. If the IMAGER_SPECIFIC.VFMode parameter is set to VIEWFINDER_MODE.VIEWFINDER_MODE_DYNAMIC_RETICLE,
        //     then this continuous read mode will be ignored
        CONTINUOUS_READ = 5,
    }

    public class ScannerEventArgs : EventArgs
    {
        public ScannerEventArgs(string sBarcodeData, BarcodeType nBarcodeType, string sBarcodeName, uint nBarcodeLength)
        {
            barcodedata = sBarcodeData;
            barcodetype = nBarcodeType;
            barcodename = sBarcodeName;
            barcodelength = nBarcodeLength;
        }

        private string barcodedata = string.Empty;
        public string BarcodeData
        {
            get { return barcodedata; }
        }
        private uint barcodelength = 0;
        public uint BarcodeLength
        {
            get { return barcodelength; }
        }
        private string barcodename = string.Empty;
        public string BarcodeName
        {
            get { return barcodename; }
        }
        private BarcodeType barcodetype = BarcodeType.Unknown;
        public BarcodeType BarcodeType
        {
            get { return barcodetype; }
        }

        public override string ToString()
        {
            try
            {
                return
                    this.BarcodeData.Trim() + "\r\n" +
                    this.BarcodeLength + "\r\n" +
                    this.BarcodeName.Trim() + "\r\n" +
                    this.BarcodeType.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
