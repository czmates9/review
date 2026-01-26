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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inside_mobile.mobile mobile = null;
            string sessionID = null;
            try
            {
                mobile = new Fask.Module.Inside.Inside_mobile.mobile();
                mobile.Url = Properties.Resources.ServerAddress;
                mobile.Timeout = int.Parse(Properties.Resources.ServerTimeout);

                string pwdhash = mobile.getHash(Globals.UserPwd);

                sessionID = mobile.beginSession(
                    Properties.Resources.UserDatabase,
                    Globals.UserLogin,
                    mobile.getHash(Globals.UserPwd)
                    );
                if (sessionID == null)
                    throw new Exception("Invalid username or password");

                Inside_mobile.dmFilterCondition rootFilter = new Fask.Module.Inside.Inside_mobile.dmFilterCondition();

                Inside_mobile.dmFilterCondition docType = Inside_mobile.StubUtil.prepareFilter("documentType.code1");
                docType.stringValues = new string[] { Inside_mobile.CommonConstant.DocumentTypeCode.TRADE_IN_ORDER };
                
                rootFilter.children = new Fask.Module.Inside.Inside_mobile.dmFilterCondition[] { docType };

                //Inside_mobile.codeBookItem[] codeBookItems = mobile.getCodeBookItems(sessionID, "Article", -1, 1);                
                Inside_mobile.mobileATDocument[] documents = mobile.getATDocuments(
                    sessionID,
                    rootFilter,
                    null,
                    true,
                    -1,
                    -1);

                mobile.endSession(sessionID);

                MessageBox.Show(documents.Length.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FormPrevodPalety frmPrevodPalety = new FormPrevodPalety())
            {
                frmPrevodPalety.ShowDialog();
            }

            this.Show();
        }
    }
}