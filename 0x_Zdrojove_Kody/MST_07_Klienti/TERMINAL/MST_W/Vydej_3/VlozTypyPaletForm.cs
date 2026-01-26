using System;
using System.Linq;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class VlozTypyPaletForm : System.Windows.Forms.Form
    {
        public string sopnumbe
        {
            get
            {
                return this.dfDoklad.Data;
            }
            set
            {
                this.dfDoklad.Data = value;
            }
        }
        private string rez1 = "";
        public string REZ1
        {
            get
            {
                return rez1;
            }
        }

        public VlozTypyPaletForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            if (ctvrtPalety_tb.Text == "")
                ctvrtPalety_tb.Text = "0";
            if (pulpalety_tb.Text == "")
                pulpalety_tb.Text = "0";
            if (euroPalety_tb.Text == "")
                euroPalety_tb.Text = "0";
            if (nevratnePalety_tb.Text == "")
                nevratnePalety_tb.Text = "0";
            if (kartony_tb.Text == "")
                kartony_tb.Text = "0";

            try
            {
                rez1 = "C" + Convert.ToInt32(ctvrtPalety_tb.Text).ToString("00") +
                    "P" + Convert.ToInt32(pulpalety_tb.Text).ToString("00") +
                    "E" + Convert.ToInt32(euroPalety_tb.Text).ToString("00") +
                    "N" + Convert.ToInt32(nevratnePalety_tb.Text).ToString("00") +
                    "K" + Convert.ToInt32(kartony_tb.Text).ToString("00");
            }
            catch
            {
                MessageBoxBig.Show("Poèet má špatný formát. Vkládejte pouze èísla 0-99.", "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void VlozTypyPaletForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ok_but_Click(null, null);
            if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
            else
                return;

            e.Handled = true;
        }

        private void VlozTypyPaletForm_Load(object sender, EventArgs e)
        {
            this.Size = Forms.FormLocation.ScreenResolution;

            try
            {
                TextBox[] textboxeses = this.panelData.Controls.OfType<TextBox>().ToArray();
                textboxeses[Settings.VlozTypyPaletFormDefault].Focus();
            }
            catch //(Exception ex)
            {
            }
        }

        private void VlozTypyPaletForm_Activated(object sender, EventArgs e)
        {
            Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        }



    }
}