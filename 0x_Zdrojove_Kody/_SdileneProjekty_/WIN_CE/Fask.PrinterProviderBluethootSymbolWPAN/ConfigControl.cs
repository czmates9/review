using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fask.PrinterProviderBluetoothSymbolWPAN
{
    public partial class ConfigControl : UserControl
    {
        private Symbol.WPAN.Bluetooth.Bluetooth b = null;
        public Symbol.WPAN.Bluetooth.Bluetooth Bloetooth
        {
            get { return b; }
            set
            {
                if (b != null)
                    b.RemoteDevices.RefreshNotify -= new Symbol.WPAN.Bluetooth.RemoteDevices.RefreshNotifyEventHandler(RemoteDevices_RefreshNotify);

                b = value;
                b.RemoteDevices.RefreshNotify += new Symbol.WPAN.Bluetooth.RemoteDevices.RefreshNotifyEventHandler(RemoteDevices_RefreshNotify);
            }
        }

        public ConfigControl()
        {
            InitializeComponent();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.GetType().ToString(), "Test", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        private void buttonDiscover_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();

                if (buttonDiscover.Enabled)
                    b.RemoteDevices.Refresh();

                //ButtonDiscoveryEnable(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bluetooth discovery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        void RemoteDevices_RefreshNotify(object sender, Symbol.WPAN.Bluetooth.RefreshNotifyEventArgs eRefreshNotifyEventArg)
        {
            switch (eRefreshNotifyEventArg.Status)
            {
                case Symbol.WPAN.Bluetooth.RefreshStatus.CANCELLED:
                    StatusBarUpdate("Discovery cancelled");
                    ButtonDiscoveryEnable(true);
                    break;
                case Symbol.WPAN.Bluetooth.RefreshStatus.COMPLETED:
                    StatusBarUpdate("Discovery completed");
                    ButtonDiscoveryEnable(true);
                    break;
                case Symbol.WPAN.Bluetooth.RefreshStatus.ERROR:
                    StatusBarUpdate("Discovery error ... " + eRefreshNotifyEventArg.Result.ToString());
                    ButtonDiscoveryEnable(true); 
                    break;
                case Symbol.WPAN.Bluetooth.RefreshStatus.INPROGRESS:
                    StatusBarUpdate("Disovery in progress ...");
                    if (eRefreshNotifyEventArg.RemoteDevice != null)
                        UpdateRemoteDeviceList(eRefreshNotifyEventArg.RemoteDevice);
                    ButtonDiscoveryEnable(false);
                    break;
                case Symbol.WPAN.Bluetooth.RefreshStatus.STARTED:
                    StatusBarUpdate("Discovery started ...");
                    ButtonDiscoveryEnable(false);
                    break;
                default:
                    break;
            }
        }

        private void StatusBarUpdate(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.StatusBarUpdate(text); });
                return;
            }

            statusBar.Text = text;
        }

        private void ButtonDiscoveryEnable(bool enable)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.ButtonDiscoveryEnable(enable); });
                return;
            }

            buttonDiscover.Enabled = enable;
        }

        delegate void MethodInvoker();
        private void UpdateRemoteDeviceList(Symbol.WPAN.Bluetooth.RemoteDevice remoteDevice)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.UpdateRemoteDeviceList(remoteDevice); });
                return;
            }

            ListViewItem li = new ListViewItem();
            li.Tag = remoteDevice;
            li.Text = remoteDevice.Name;
            li.SubItems.Add(remoteDevice.Address);
            li.SubItems.Add(remoteDevice.ServiceName);
            li.SubItems.Add(remoteDevice.IsPaired.ToString());
            li.SubItems.Add(remoteDevice.LocalComPort.ToString());
            li.SubItems.Add(remoteDevice.ClassOfDevice.ToString());
            listView1.Items.Add(li);
        }

        private void buttonUseDevice_Click(object sender, EventArgs e)
        {
            Symbol.WPAN.Bluetooth.RemoteDevice rdevice = null;
            if (listView1.SelectedIndices.Count <= 0)
                return;
            else
                rdevice = listView1.Items[listView1.SelectedIndices[0]].Tag as Symbol.WPAN.Bluetooth.RemoteDevice;            

            if (rdevice == null)
                return;

            textBoxAddress.Text = rdevice.Address;
            textBoxName.Text = rdevice.Name;

        }


    }
}
