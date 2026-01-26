using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MST_W_Licensing
{
    public partial class Form_Main : Form
    {
        //Musi byt stejne jako je v Fask.MST_W.License.Licensing.licensepassword
        private const string licensepassword = "fask!pro159";

        public Form_Main()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //if (saveFileDialogLicence.ShowDialog() == DialogResult.Cancel)
                //    return;

                //string file = saveFileDialogLicence.FileName;
                string file = txtFilename.Text.Trim();

                string licence = txtZakaznik.Text.Trim();

                licence = Fask.Encryption.RijndaelWrapper.Encrypt(licence, licensepassword);

                StreamWriter sw = new StreamWriter(file);
                sw.Write(licence);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;

                MessageBox.Show("License file saved to :\n" + (new FileInfo(file)).FullName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            txtZakaznik.Focus();
        }

        private void Form_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            else if (e.KeyCode == Keys.Enter)
                buttonGenerate_Click(null, null);
            else
                return;

            e.Handled = true;
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                string file = txtFilename.Text.Trim();

                string license = File.OpenText(file).ReadToEnd();

                license = Fask.Encryption.RijndaelWrapper.Decrypt(license, licensepassword);

                txtZakaznik.Text = license;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
    }
}