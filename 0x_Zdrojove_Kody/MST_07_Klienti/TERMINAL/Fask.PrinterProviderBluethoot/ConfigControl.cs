using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fask.PrinterProviderBluetooth
{
    public partial class ConfigControl : UserControl
    {
        public ConfigControl()
        {
            InitializeComponent();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.GetType().ToString(), "Test", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
    }
}
