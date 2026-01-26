using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID2;

namespace Fask.MST_W.Scanner.MC9090
{
    public partial class FrmCapabilties : System.Windows.Forms.Form
    {
        public FrmCapabilties()
        {
            InitializeComponent();
        }

        public ReaderCapability Capabilty;
        private void FrmCapabilties_Load(object sender, EventArgs e)
        {
            if (Capabilty.AutoModeSupported == false)
                lblAutoMode1.Text = "Not Supported";
            
            if (Capabilty.GPIOSupported == false)
                lblGPIO1.Text = "Not Supported";
            
            if (Capabilty.HeartBeatSupported == false)
                lblHeartBeat1.Text = "Not Supported";
            
            if (Capabilty.HoppingSupported == false)
                lblHopping1.Text = "Not Supported";

            if (Capabilty.LoggingSupported == false)
                lblLogging1.Text = "Not Supported";
            
            if (Capabilty.NTPClientSupported == false)
                lblNTPClient1.Text = "Not Supported";
            
            if (Capabilty.OnDemandSupported == false)
                lblOnDemand1.Text = "Not Supported"; 
            
            if (Capabilty.RFSurveySupported == false)
                lblRFSurvey1.Text = "Not Supported";
            
            if (Capabilty.UTCClockSupported == false)
                lblUTCCLock1.Text = "Not Supported";

            if (Capabilty.TriggerSupported == false)
                lblTrigger1.Text = "Not Supported";

            if (Capabilty.SensorSupported == false)
                lblSensor1.Text = "Not Supported";
        }    
    }
}