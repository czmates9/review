using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.Module.Inside
{
    public partial class FormPrevodPalety : Form
    {       

        public FormPrevodPalety()
        {
            InitializeComponent();
        }

        private void finalize()
        {
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            txtPaleta.Focus();
            txtPaleta.SelectAll();
        }



        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Inside_mobile.mobile mobile = null;
            string sessionID = null;
            try
            {
                List<Inside_mobile.mobileATDocument> mDocuments = new List<Fask.Module.Inside.Inside_mobile.mobileATDocument>();

                Inside_mobile.mobileATDocument mDocument = new Fask.Module.Inside.Inside_mobile.mobileATDocument();
                mDocument.id = 0;
                mDocument.docNumberInt = txtPaleta.Text.Trim();
                if (rbEXP.Checked)
                    mDocument.transactionType = rbEXP.Text;
                else if (rbKOOPIN.Checked)
                    mDocument.transactionType = rbKOOPIN.Text;
                else if (rbKOOPOUT.Checked)
                    mDocument.transactionType = rbKOOPOUT.Text;
                else
                    throw new Exception("Neni zadan typ transakce");
                mDocument.description = "Import z terminalu";
                mDocument.year = DateTime.Now.Year;

                mDocuments.Add(mDocument);

                //komunikace s Inside ...

                mobile = new Fask.Module.Inside.Inside_mobile.mobile();
                mobile.Url = Properties.Resources.ServerAddress;
                mobile.Timeout = int.Parse(Properties.Resources.ServerTimeout);

                sessionID = mobile.beginSession(
                    Properties.Resources.UserDatabase,
                    Globals.UserLogin,
                    mobile.getHash(Globals.UserPwd)
                    );
                if (sessionID == null)
                    throw new Exception("Invalid username or password");

                mobile.saveATDocuments(sessionID, mDocuments.ToArray());

                mobile.endSession(sessionID);

                MessageBox.Show("OK ulozeno");

                txtPaleta.Text = string.Empty;
                txtPaleta.Focus();
                txtPaleta.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            finally
            {

            }

            finalize();
            DialogResult = DialogResult.OK;
        }
    }
}