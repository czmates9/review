using System;
using System.Windows.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemGenerovatSNVybercs : System.Windows.Forms.Form
    {
        public enum GenerovatSN
        {
            Vlastni,
            Zakaznicke
        }

        private GenerovatSN _GenerovatSN = GenerovatSN.Vlastni;
        public GenerovatSN GenerovatxSN
        {
            get { return _GenerovatSN; }
            set { _GenerovatSN = value; }
        }


        public PrijemGenerovatSNVybercs()
        {
            InitializeComponent();
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _GenerovatSN = GenerovatSN.Vlastni;
            PerformOK();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _GenerovatSN = GenerovatSN.Zakaznicke;
            PerformOK();
        }

        private void PrijemGenerovatSNVybercs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V || e.KeyCode == Keys.D1)
            {
                _GenerovatSN = GenerovatSN.Vlastni;
                PerformOK();
            }
            else if (e.KeyCode == Keys.Z || e.KeyCode == Keys.D2)
            {
                _GenerovatSN = GenerovatSN.Zakaznicke;
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PrijemGenerovatSNVybercs_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            _GenerovatSN = GenerovatSN.Zakaznicke;
            PerformOK();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            _GenerovatSN = GenerovatSN.Vlastni;
            PerformOK();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _GenerovatSN = GenerovatSN.Vlastni;
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _GenerovatSN = GenerovatSN.Zakaznicke;
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }
    }
}