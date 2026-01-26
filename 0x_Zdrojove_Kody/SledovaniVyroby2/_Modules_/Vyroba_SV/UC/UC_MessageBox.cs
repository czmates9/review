using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_SV.UC
{
    public partial class UC_MessageBox : UserControl
    {
        public bool ano = false;
        public bool ne = false;

        public UC_MessageBox()
        {
            InitializeComponent();
        }

        private void btn_ano_Click(object sender, EventArgs e)
        {
            ano = true;
        }

        private void btn_ne_Click(object sender, EventArgs e)
        {
            ne = true;
        }
    }
}
