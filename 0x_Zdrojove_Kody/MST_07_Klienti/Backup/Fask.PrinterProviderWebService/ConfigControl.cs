using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fask.PrinterProviderWebService
{
    public partial class ConfigControl : UserControl
    {

        public PrintServerTiskService.Tisk printServerTiskService { get; set; }
        public PrintServerTestService.Test printServerTestService { get; set; }
		public PrintServerTiskTestService.TiskTest printServerTiskTestService { get; set; }

        public ConfigControl()
        {
            InitializeComponent();
        }

        private void buttonPrinters_Click(object sender, EventArgs e)
        {
            try
            {
                comboBoxPrinterName.Items.Clear();
                textBoxInfo.Text = string.Empty;

                DataTable dtPrinters = printServerTiskTestService.Printers();

                textBoxInfo.Text = "Printers: " + dtPrinters.Rows.Count;
                foreach (DataRow dr in dtPrinters.Rows)
                {
                    comboBoxPrinterName.Items.Add(dr["Name"]);
                    textBoxInfo.Text += "" + dr["Name"] + " : " + dr["Valid"] + "\r\n";
                }
            }
            catch (Exception ex)
            {
                textBoxInfo.Text += "\r\n" + ex.Message;
            }
        }

        private void buttonTemplates_Click(object sender, EventArgs e)
        {
            try
            {
                comboBoxTemplate.Items.Clear();
                textBoxInfo.Text = string.Empty;

                DataTable dtLabels = printServerTestService.Labels();

                textBoxInfo.Text = "Labels: " + dtLabels.Rows.Count;
                foreach (DataRow dr in dtLabels.Rows)
                {
                    comboBoxTemplate.Items.Add(dr["Name"]);
                    textBoxInfo.Text += "" + dr["Name"] + "\r\n";
                }
            }
            catch (Exception ex)
            {
                textBoxInfo.Text += "\r\n" + ex.Message;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            buttonPrinters_Click(null, null);
            buttonTemplates_Click(null, null);
        }

        private void buttonHelloWorld_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxInfo.Text = string.Empty;
                string s = printServerTestService.HelloWorld();
                textBoxInfo.Text += s;
            }
            catch (Exception ex)
            {
                textBoxInfo.Text += "\r\n" + ex.Message;
            }
        }

        private void buttonWriteLog_Click(object sender, EventArgs e)
        {
            try
            {
                //textBoxInfo.Text = string.Empty;
                bool ok = printServerTestService.WriteTo_Log_Server_Info_File(textBoxInfo.Text);
                textBoxInfo.Text += "\r\n" + "WriteToLog : " + ok.ToString();
            }
            catch (Exception ex)
            {
                textBoxInfo.Text += "\r\n" + ex.Message;
            }
        }

        private void buttonWindowsIdentity_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxInfo.Text = string.Empty;
                string s = printServerTestService.WindowsIdentity();
                textBoxInfo.Text += "\r\n" + s;
            }
            catch (Exception ex)
            {
                textBoxInfo.Text += "\r\n" + ex.Message;
            }
        }

    }
}
