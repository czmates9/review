using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Scanner.MC9090
{
    public partial class FrmReaderSettings : System.Windows.Forms.Form
    {
        public FrmReaderSettings()
        {
            InitializeComponent();
            numUpDownStartingQ.Value = startingQ;
        }

        private byte startingQ = 6;

        public byte StartingQ
        {
            get
            {
                return startingQ;
            }
            set
            {
                startingQ = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            startingQ = Convert.ToByte(numUpDownStartingQ.Value);
        }

        private void ReaderSettings_Load(object sender, EventArgs e)
        {
            if (startingQ < 0 || startingQ > 15)
            {
                startingQ = 6;
                MessageBox.Show("Valid range for StartingQ is 0 to 15.\n Setting the value to defaultvalue 6");
            }
            numUpDownStartingQ.Value = startingQ;
        }
    }
}