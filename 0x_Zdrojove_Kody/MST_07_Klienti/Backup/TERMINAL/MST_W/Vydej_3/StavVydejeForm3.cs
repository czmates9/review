using System;
using System.Windows.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class StavVydejeForm3 : System.Windows.Forms.Form
    {
        public StavVydejeForm3(decimal nasnimat, decimal nasnimano, int pol_nasnimat, int pol_nasnimano)
        {
            InitializeComponent();
            this.polPoz_l.Text = pol_nasnimat.ToString();
            this.polNasnim_l.Text = pol_nasnimano.ToString();
            this.polZbyva_l.Text = System.Convert.ToString(pol_nasnimat - pol_nasnimano);
            this.mnozPoz_l.Text = nasnimat.ToString(Settings.UIFormatDesCisel);
            this.mnozNasnim_l.Text = nasnimano.ToString(Settings.UIFormatDesCisel);
            this.mnozZbyva_l.Text = ((decimal)(nasnimat - nasnimano)).ToString(Settings.UIFormatDesCisel);
            this.KeyPreview = true;
        }

        private void StavVydejeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || (e.KeyCode == Keys.Escape))
            {
                this.DialogResult = DialogResult.OK;
            }

            e.Handled = true;
        }

        private void ok_but_Click(object sender, EventArgs e)
        {

        }

        private void StavVydejeForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
        }

    }
}