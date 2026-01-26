using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Konzola.Terminal
{
    public partial class FormVNC : Form
    {

        public static System.Drawing.Imaging.PixelFormat VNC_PixelFormat
        {
            get
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();
                string val = string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.VNC[0].VNC_PixelFormat) ?
                    "Format16bppRgb555" :
                    Konfigurace.Globals_Konfig_Konzola.Konfigurace.VNC[0].VNC_PixelFormat;

                System.Drawing.Imaging.PixelFormat Form = (System.Drawing.Imaging.PixelFormat)Enum.Parse(typeof(System.Drawing.Imaging.PixelFormat), val, true);
                return Form;
            }
            set 
            {
                //SetValue("VNC_PixelFormat", value.ToString());
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.VNC[0].VNC_PixelFormat = value.ToString();
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
        }

        private string _IP = "192.168.1.1";
        private int _Port = 5900;

        public FormVNC(string ip, int port)
        {
            InitializeComponent();
            _IP = ip;
            _Port = port;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                vncControl1.PixelFormat = (System.Drawing.Imaging.PixelFormat)this.cb_Format.SelectedItem;
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void FormVNC_Load(object sender, EventArgs e)
        {

            this.cb_Format.DataSource = Enum.GetValues(typeof(System.Drawing.Imaging.PixelFormat));
            this.cb_Format.SelectedItem = FormVNC.VNC_PixelFormat;


            string vncPassword = string.Empty;

            if (vncControl1.Client.IsConnected)
            {
                vncControl1.Client.Close();
            }
            else
            {
                //if (string.IsNullOrEmpty(Config.Host))
                //{
                //    MessageBox.Show(this, "Hostname isn't set.", "Hostname", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //if (Config.Port < 1 || Config.Port > 65535)
                //{
                //    MessageBox.Show(this, "Port must be between 1 and 65535.", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                DialogResult dr = Forms.InputBox.Show("VNC Heslo", "Zadejte heslo. Pokud nemáte VNC zaheslováno mužete pokračovat bez zadani hesla.", string.Empty, true, out vncPassword);
                if (dr != System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
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

                            vncControl1.Client.Connect(_IP, _Port, options);
                        }
                        finally { Cursor = Cursors.Default; }
                    }
                    catch (RemoteViewing.Vnc.VncException ex)
                    {
                        MessageBox.Show(this,
                                        "Connection failed (" + ex.Reason.ToString() + ").",
                                        "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        DialogResult = System.Windows.Forms.DialogResult.Abort;
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(this,
                                        "Connection failed (" + ex.SocketErrorCode.ToString() + ").",
                                        "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        DialogResult = System.Windows.Forms.DialogResult.Abort;
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

        private void vncControl1_Closed(object sender, EventArgs e)
        {
            this.Text = "Close";
        }

        private void vncControl1_Connected(object sender, EventArgs e)
        {
            this.Text = "Connect";
        }

        private void vncControl1_ConnectionFailed(object sender, EventArgs e)
        {
            ///MessageBox.Show(this, "Komunikace nebyla navázána!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void FormVNC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (vncControl1.Client.IsConnected)
                vncControl1.Client.Close();

            FormVNC.VNC_PixelFormat = (System.Drawing.Imaging.PixelFormat)this.cb_Format.SelectedItem;
        }
    }
}
