using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Karumi.Dexter;
using EDMTDev.ZXingXamarinAndroid;
using Fask.Interfaces;
using ZXing;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace Fask.Scanner.Camera_ZXing
{
    public class ScannerDialog : DialogFragment, IScanner
    {
        public ZXingScannerView scannerView;

        public event IScanner.ScannerEventHandler ScannerEvent;

        #pragma warning disable // Tenhle event je zde povinne implemntovat, z duvodi Interface, a pomoci pragrma je zde potvačeny warning
        public event IScanner.StatusEventHandler StatusEvent;
        #pragma warning enable

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view1;

            try
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                view1 = inflater.Inflate(Resource.Layout.ZXing_Layout, container, false);

                //view init
                scannerView = view1.FindViewById<ZXingScannerView>(Resource.Id.zxscan);

                List<BarcodeFormat> formats = new List<BarcodeFormat>();

                formats.Add(BarcodeFormat.QR_CODE);
                formats.Add(BarcodeFormat.CODE_128);
                formats.Add(BarcodeFormat.DATA_MATRIX);
                formats.Add(BarcodeFormat.EAN_13);
                formats.Add(BarcodeFormat.UPC_A);

                scannerView.SetFormats(formats);

                scannerView.SetResultHandler(new MyResultHandler(this));

                StartScanner();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return view1;

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            try
            {

                Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
                base.OnActivityCreated(savedInstanceState);
                Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public void KillScanner()
        {
            try
            {
                scannerView.StopCamera();
                scannerView.Dispose();
                scannerView = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StartScanner()
        {
            try
            {
                if (scannerView != null)
                {
                    scannerView.StartCamera();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void StopScanner()
        {
            try
            {
                if (scannerView != null)
                {
                    scannerView.StopCamera();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowKod(ZXing.Result rawResult)
        {
            try
            {
                ScannerEvent?.Invoke(this, new ScannerEventArgs(rawResult.Text, rawResult.BarcodeFormat.ToString()));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}