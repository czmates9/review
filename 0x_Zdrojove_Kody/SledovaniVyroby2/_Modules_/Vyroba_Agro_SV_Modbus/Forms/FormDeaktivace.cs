using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.Forms
{
    public partial class FormDeaktivace : Form
    {
        public FormDeaktivace()
        {
            InitializeComponent();
        }

        private void btn_ano_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btn_ne_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
