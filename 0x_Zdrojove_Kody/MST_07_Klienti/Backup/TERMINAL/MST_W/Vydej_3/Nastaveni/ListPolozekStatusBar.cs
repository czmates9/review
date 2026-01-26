using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fask.MST_W.Vydej_3.Nastaveni
{
    public partial class ListPolozekStatusBar : System.Windows.Forms.Form
    {
        public enum VydejStatusBarMnozstvi
        {
            Prazdne,
            PolozekPozadovano,
            PolozekNasnimano,
            PolozekZbyva,
            MnozstviPozadovano,
            MnozstviNasnimano,
            MnozstviZbyva
        }

        public ListPolozekStatusBar()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnResize(e);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            //Load nastaveni
            chkPole1.Checked = Settings.VydejStatusBarPole1Allow;
            chkPole2.Checked = Settings.VydejStatusBarPole2Allow;
            chkPole3.Checked = Settings.VydejStatusBarPole3Allow;
            chkPole4.Checked = Settings.VydejStatusBarPole4Allow;

            txtPostfixPole1.Text = Settings.VydejStatusBarPole1Postfix;
            txtPostfixPole2.Text = Settings.VydejStatusBarPole2Postfix;
            txtPostfixPole3.Text = Settings.VydejStatusBarPole3Postfix;
            txtPostfixPole4.Text = Settings.VydejStatusBarPole4Postfix;

            txtPrefixPole1.Text = Settings.VydejStatusBarPole1Prefix;
            txtPrefixPole2.Text = Settings.VydejStatusBarPole2Prefix;
            txtPrefixPole3.Text = Settings.VydejStatusBarPole3Prefix;
            txtPrefixPole4.Text = Settings.VydejStatusBarPole4Prefix;

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.Prazdne);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.Prazdne);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.Prazdne);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.Prazdne);

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.PolozekPozadovano);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.PolozekPozadovano);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.PolozekPozadovano);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.PolozekPozadovano);

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.PolozekNasnimano);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.PolozekNasnimano);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.PolozekNasnimano);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.PolozekNasnimano);

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.PolozekZbyva);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.PolozekZbyva);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.PolozekZbyva);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.PolozekZbyva);

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.MnozstviPozadovano);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.MnozstviPozadovano);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.MnozstviPozadovano);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.MnozstviPozadovano);

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.MnozstviNasnimano);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.MnozstviNasnimano);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.MnozstviNasnimano);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.MnozstviNasnimano);

            cmbPole1.Items.Add(VydejStatusBarMnozstvi.MnozstviZbyva);
            cmbPole2.Items.Add(VydejStatusBarMnozstvi.MnozstviZbyva);
            cmbPole3.Items.Add(VydejStatusBarMnozstvi.MnozstviZbyva);
            cmbPole4.Items.Add(VydejStatusBarMnozstvi.MnozstviZbyva);

            cmbPole1.SelectedItem = Settings.VydejStatusBarPole1Value;
            cmbPole2.SelectedItem = Settings.VydejStatusBarPole2Value;
            cmbPole3.SelectedItem = Settings.VydejStatusBarPole3Value;
            cmbPole4.SelectedItem = Settings.VydejStatusBarPole4Value;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            bStorno.Size = bNew;
        }

        private void finalize()
        {
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            finalize();

            Settings.VydejStatusBarPole1Value = (VydejStatusBarMnozstvi)cmbPole1.SelectedItem;
            Settings.VydejStatusBarPole2Value = (VydejStatusBarMnozstvi)cmbPole2.SelectedItem;
            Settings.VydejStatusBarPole3Value = (VydejStatusBarMnozstvi)cmbPole3.SelectedItem;
            Settings.VydejStatusBarPole4Value = (VydejStatusBarMnozstvi)cmbPole4.SelectedItem;

            Settings.VydejStatusBarPole1Allow = chkPole1.Checked;
            Settings.VydejStatusBarPole2Allow = chkPole2.Checked;
            Settings.VydejStatusBarPole3Allow = chkPole3.Checked;
            Settings.VydejStatusBarPole4Allow = chkPole4.Checked;

            Settings.VydejStatusBarPole1Postfix = txtPostfixPole1.Text;
            Settings.VydejStatusBarPole2Postfix = txtPostfixPole2.Text;
            Settings.VydejStatusBarPole3Postfix = txtPostfixPole3.Text;
            Settings.VydejStatusBarPole4Postfix = txtPostfixPole4.Text;

            Settings.VydejStatusBarPole1Prefix = txtPrefixPole1.Text;
            Settings.VydejStatusBarPole2Prefix = txtPrefixPole2.Text;
            Settings.VydejStatusBarPole3Prefix = txtPrefixPole3.Text;
            Settings.VydejStatusBarPole4Prefix = txtPrefixPole4.Text;


            DialogResult = DialogResult.OK;
        }

        private void bStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void ListPolozekStatusBar_Activated(object sender, EventArgs e)
        {
            Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        }
    }
}