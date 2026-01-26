using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Konzola.Servis.Print
{
    public partial class FormPrintZdrojStav_Etiketa : Form
    {
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow _ZdrojStavRow
        {
            set;
            private get;
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow _ZdrojRow
        {
            private get;
            set;
        }

        public FormPrintZdrojStav_Etiketa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
