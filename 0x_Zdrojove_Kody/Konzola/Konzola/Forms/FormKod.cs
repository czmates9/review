using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;

namespace Konzola.Forms
{
    public partial class FormKod : Form
    {
        protected FormKod()
        {
            InitializeComponent();
        }

        protected FormKod(Bitmap Image)
            : this()
        {
            try
            {
                this.pictureBox1.Image = Image;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                this.DoubleBuffered = true;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        public static DialogResult Show(Bitmap img, string KodNazev)
        {
            if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin() || FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT())
            {
                using (FormKod form = new FormKod(img))
                {
                    form.label_NazevKodu.Text = KodNazev;
                    DialogResult dr = form.ShowDialog();
                    return dr;
                }
            }
            else
                return DialogResult.OK;
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void PerformOK()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
        }
    }
}
