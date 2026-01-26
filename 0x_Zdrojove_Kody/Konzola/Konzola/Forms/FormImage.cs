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
    public partial class FormImage : Form
    {

        private string filepath = string.Empty;
        private Image OriginalImg;

        //public InputBox()
        //{
        //    InitializeComponent();
        //}

        protected FormImage()
        {
            InitializeComponent();

            //this.Location = MySystem.FormMidLocation.GetFormLocation(this.Size);
        }

        protected FormImage(string filepath)
            : this()
        {
            try
            {
                this.filepath = filepath;
                if(!System.IO.File.Exists(filepath))
                    throw new Exception("Soubor neexistuje");

                OriginalImg = Image.FromFile(@filepath);
                this.pictureBox1.Image = OriginalImg;
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

        public static DialogResult Show(string filepath)
        {
            using (FormImage form = new FormImage(filepath))
            {
                filepath = string.Empty;
                DialogResult dr = form.ShowDialog();
                return dr;
            }
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
