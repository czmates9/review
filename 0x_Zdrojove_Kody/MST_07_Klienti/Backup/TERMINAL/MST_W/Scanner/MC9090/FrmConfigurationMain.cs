using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID2;
using System.Collections;

namespace Fask.MST_W.Scanner.MC9090
{
    public partial class FrmConfigurationMain : System.Windows.Forms.Form
    {
        private Symbol.RFID2.IRFIDReader deviceReader = null;

        public FrmConfigurationMain(Symbol.RFID2.IRFIDReader deviceReader)
        {
            InitializeComponent();
            this.deviceReader = deviceReader;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnResize(e);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
        }

        private void finalize()
        {
        }

         public void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }
 
        private void bOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }


        bool VerifyOnDemand()
        {
            if (deviceReader.ReaderStatus == ReaderStatus.ONLINE)
            {
                if (deviceReader.ReadMode != ReadMode.ONDEMAND)
                {
                    MessageBox.Show("Exit Autnonmous Mode");
                    return (false);
                }
                else
                {
                    return (true);
                }
            }
            return (false);
        }

        private void gbAntena_Click(object sender, EventArgs e)
        {
            if ((!VerifyOnDemand()))
            {
                return;
            }

            try
            {
                AntennaConfig antConfig;
                int antennaIndex;
                FrmSetAntenna f_setAntenna = new FrmSetAntenna(deviceReader.Antennas);

                f_setAntenna.m_txMax = deviceReader.MaxTxPower;
                f_setAntenna.m_txMin = deviceReader.MinTxPower;

                f_setAntenna.ShowDialog();
                if (f_setAntenna.setAntennaConfig)
                {
                    antConfig = f_setAntenna.SetAntenna;
                    antennaIndex = f_setAntenna.ConfigAntennaIndex;
                    deviceReader.SetAntennaConfiguration(antennaIndex, antConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void gbReader_Click(object sender, EventArgs e)
        {
            FrmReaderSettings readerSettings = new FrmReaderSettings();
            readerSettings.StartingQ = deviceReader.Gen2Settings[0].StartingQ;

            if (readerSettings.ShowDialog() == DialogResult.OK)
            {

                foreach (Gen2Parameters tempParameters in deviceReader.Gen2Settings)
                {
                    tempParameters.StartingQ = readerSettings.StartingQ;
                }
            }
        }

        private void gbReaderInfo_Click(object sender, EventArgs e)
        {
            Hashtable htReaderInfo = new Hashtable();
            htReaderInfo.Add("DeviceSerialNumber", deviceReader.ReaderInfo.DeviceSerialNumber);
            htReaderInfo.Add("DeviceModelNumber", deviceReader.ReaderInfo.DeviceModelNumber);
            htReaderInfo.Add("ManufacturerName", deviceReader.ReaderInfo.ManufacturerName);
            htReaderInfo.Add("ManufactureDate", deviceReader.ReaderInfo.ManufactureDate);
            htReaderInfo.Add("HardwareVersion", deviceReader.ReaderInfo.HardwareVersion);
            htReaderInfo.Add("BootLoaderVersion", deviceReader.ReaderInfo.BootLoaderVersion);
            htReaderInfo.Add("FirmwareVersion", deviceReader.ReaderInfo.FirmwareVersion);
            htReaderInfo.Add("SymbolSDKVersion", deviceReader.SDKVersionNumber);
            htReaderInfo.Add("Model", deviceReader.Model.ToString());

            FrmReaderInfo reader = new FrmReaderInfo(htReaderInfo);
            reader.ShowDialog();
        }

        private void gbAntenaInfo_Click(object sender, EventArgs e)
        {
            try
            {
                FrmReadAntennaInfo antInfo = new FrmReadAntennaInfo(deviceReader.Antennas);
                antInfo.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void gbCapabilities_Click(object sender, EventArgs e)
        {
            FrmCapabilties readerCapabilies = new FrmCapabilties();
            readerCapabilies.Capabilty = deviceReader.ReaderCapability;
            readerCapabilies.ShowDialog();
        }
    }
}