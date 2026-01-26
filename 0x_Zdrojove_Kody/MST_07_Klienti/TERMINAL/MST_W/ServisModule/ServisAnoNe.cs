using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.ServisModule
{
    public partial class ServisAnoNe : Form
    {
        /// <summary>
        /// Výsledná volba (zdali se zvolilo ano nebo ne)
        /// </summary>
        public bool Volba { get; set; }

        /// <summary>
        /// Text, který se má zobrazit ve status baru
        /// </summary>
        public string StatusBarInfoText { get; set; }

        public ServisAnoNe(string statusbartext, string popis)
        {
            InitializeComponent();
            if (statusbartext != null)
                this.Text = statusbartext;
            tbText.Text = popis;
        }

        private void btnAno_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void btnNe_Click(object sender, EventArgs e)
        {
            PerformNo();
        }

        private void miAno_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void miNe_Click(object sender, EventArgs e)
        {
            PerformNo();
        }

        private void miPrerusit_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                    MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

                Volba = true;
                DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformNo()
        {
            try
            {
                if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                    MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "chimes.wav"));

                Volba = false;
                DialogResult = DialogResult.No;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void ServisAnoNe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
                return;

            e.Handled = true;
        }

        private void ServisAnoNe_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            panelButtons_Resize(null, null);
            statusBarInfo.Text = StatusBarInfoText != null ? StatusBarInfoText : string.Empty;
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 3, panelButtons.Height);
            btnNe.Size = nsize;
            btnStorno.Size = nsize;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.No == MessageBoxBig.Show("Opravdu se chcete vrátit o krok zpět?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
                    return;

                this.DialogResult = DialogResult.Retry;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
    }
}