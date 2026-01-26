using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace RemoteViewing.Example
{
    public partial class Form1 : Form
    {
        public const string ConfigFilename = "Config.json";
        public Config Config { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void vncControl1_Closed(object sender, EventArgs e)
        {
            btnConnect.Text = "Close";
        }

        private void vncControl1_Connected(object sender, EventArgs e)
        {
            btnConnect.Text = "Connect";
        }

        private void vncControl1_ConnectionFailed(object sender, EventArgs e)
        {
           
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            String vncPassword;
            try
            {
                vncPassword = Config.GetUnprotectedPassword();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to get the protect password from config file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (vncControl1.Client.IsConnected)
            {
                vncControl1.Client.Close();
            }
            else
            {
                if (string.IsNullOrEmpty(Config.Host))
                {
                    MessageBox.Show(this, "Hostname isn't set.", "Hostname", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Config.Port < 1 || Config.Port > 65535)
                {
                    MessageBox.Show(this, "Port must be between 1 and 65535.", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var options = new RemoteViewing.Vnc.VncClientConnectOptions();
                if (!string.IsNullOrEmpty(vncPassword))
                {
                    options.Password = vncPassword.ToCharArray();
                }

                try
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        try
                        {

                            vncControl1.Client.Connect(Config.Host, Config.Port, options);
                        }
                        finally { Cursor = Cursors.Default; }
                    }
                    catch (RemoteViewing.Vnc.VncException ex)
                    {
                        MessageBox.Show(this,
                                        "Connection failed (" + ex.Reason.ToString() + ").",
                                        "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(this,
                                        "Connection failed (" + ex.SocketErrorCode.ToString() + ").",
                                        "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    vncControl1.Focus();
                }
                finally
                {
                    if (options.Password != null)
                    {
                        Array.Clear(options.Password, 0, options.Password.Length);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.PixelFormat = (System.Drawing.Imaging.PixelFormat)cb_Format.SelectedItem;
            Config.Save(ConfigFilename);
        }

        private void cb_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                vncControl1.PixelFormat = (System.Drawing.Imaging.PixelFormat)this.cb_Format.SelectedItem;
            }
            catch (Exception ex)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config = Config.ReadFromFile(ConfigFilename);

            if (Config.Password != null)
            {
                Config.ProtectPassword();
                Config.Save(ConfigFilename);
            }

            this.cb_Format.DataSource = Enum.GetValues(typeof(System.Drawing.Imaging.PixelFormat));
            this.cb_Format.SelectedItem = Config.PixelFormat;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            vncControl1.ScaleFactor = (float)numericUpDown1.Value;
        }
    }
}
