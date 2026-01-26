using System;

namespace Fask.MST_W.Vydej_3
{
    public partial class PotvrditMnozstviForm : System.Windows.Forms.Form
    {

        public PotvrditMnozstviForm(decimal nacist, decimal nacteno)
        {
            this.nacist_l.Text += nacist.ToString();
            this.nacteno_l.Text += nacteno.ToString();
            InitializeComponent();
        }

        private void PotvrditMnozstviForm_Load(object sender, EventArgs e)
        {
            this.Size = Forms.FormLocation.ScreenResolution;
        }
    }
}