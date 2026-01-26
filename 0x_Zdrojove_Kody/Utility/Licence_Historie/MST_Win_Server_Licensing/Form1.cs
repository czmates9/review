using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace MST_Win_Server_Licensing
{
    public partial class Form1 : Form
    {
        private const string licensepassword = "fask!pro159";
        private string pathToLicence = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void nahrátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stream myStream = null;
            string path = string.Empty;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // openFileDialog1.Filter = "Cursor Files|*.cur";
            // openFileDialog1.Title = "Select a Cursor File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                path = openFileDialog1.FileName;
            else
                return;

            XmlDocument doc = new XmlDocument();

            doc.Load(path);

            UpdateUI(doc);

        }

        private void UpdateUI(XmlDocument doc)
        {
            XmlNodeList numberTerminalList = doc.GetElementsByTagName("numberTerminal");
            XmlNodeList companyList = doc.GetElementsByTagName("company");
            XmlNodeList contactList = doc.GetElementsByTagName("contact");
            XmlNodeList encrypritionList = doc.GetElementsByTagName("encryprition");
            XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
            XmlNodeList createdList = doc.GetElementsByTagName("created");

            string numberTerminal = numberTerminalList[0].InnerText;
            string company = companyList[0].InnerText;
            string contact = contactList[0].InnerText;
            string expiration = expirationList[0].InnerText;
            string encryprition = encrypritionList[0].InnerText;
            string created = createdList[0].InnerText;

            txtCompany.Text = company;
            txtContact.Text = contact;
            txtNumberTerminal.Text = numberTerminal;
            dtExpiration.Value = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);
        }

        private void UpdateUI2(XmlDocument doc)
        {
            try
            {
                XmlNodeList numberTerminalList = doc.GetElementsByTagName("numberTerminal");
                XmlNodeList companyList = doc.GetElementsByTagName("company");
                XmlNodeList contactList = doc.GetElementsByTagName("contact");
                XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
                XmlNodeList createdList = doc.GetElementsByTagName("created");

                string numberTerminal = numberTerminalList[0].InnerText;
                string company = companyList[0].InnerText;
                string contact = contactList[0].InnerText;
                string expiration = expirationList[0].InnerText;
                string created = createdList[0].InnerText;

                txtCompanyRead.Text = company;
                txtContactRead.Text = contact;
                txtNumberTerminalRead.Text = numberTerminal;
                if (String.IsNullOrEmpty(expiration))
                    chkLicenceNeomezena.Checked = true;
                else
                {
                    dtExpirationRead.Value = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);
                    chkLicenceNeomezena.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepovedlo se načíst všechny informace z licence, některá informace chybí!");
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                // openFileDialog1.Filter = "Cursor Files|*.cur";
                // openFileDialog1.Title = "Select a Cursor File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pathToLicence = openFileDialog1.FileName;

                    XmlDocument xdoc = new XmlDocument();

                    xdoc.Load(pathToLicence);

                    UpdateUI2(xdoc);

                }

                txtLicencePath.Text = pathToLicence;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string license = string.Empty;
                //XmlDocument xdoc = new XmlDocument();

                if (!File.Exists(pathToLicence))
                {
                    MessageBox.Show("Zadaný soubor neexistuje!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                license = GetFileContents(pathToLicence);

                //xdoc.Load(pathToLicence);

                license = RijndaelWrapper.Encrypt(license, licensepassword);

                using (StreamWriter w = File.AppendText(pathToLicence))
                {
                    w.Write("\n" + license);
                }

                MessageBox.Show("Licence byla úspěšně vytvořena!\nUmístění:" + (new FileInfo(pathToLicence)).FullName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                /*
                XmlNode newElem = xdoc.CreateNode("element", "encryprition", "");
                newElem.InnerText = license;
                xdoc.DocumentElement.AppendChild(newElem);

                xdoc.Save(pathToLicence);*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }


        public static string GetFileContents(string FileName)
        {
            try
            {
                return GetFileContents(FileName, 5000);
            }
            catch { throw; }
        }

        public static string GetFileContents(string FileName, int TimeOut)
        {
            StreamReader Reader = null;
            int StartTime = System.Environment.TickCount;
            try
            {
                bool Opened = false;
                while (!Opened)
                {
                    try
                    {
                        if (System.Environment.TickCount - StartTime >= TimeOut)
                            throw new System.IO.IOException("File opening timed out");
                        Reader = File.OpenText(FileName);
                        Opened = true;
                    }
                    catch (System.IO.IOException e)
                    {
                        throw e;
                    }
                }
                string Contents = Reader.ReadToEnd();
                Reader.Close();
                return Contents;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (Reader != null)
                {
                    Reader.Close();
                    Reader.Dispose();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string pathLicence = Path.Combine(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath, "licence.ini");

                if (File.Exists(pathLicence))
                {
                    if (MessageBox.Show("Licenční soubor v tomto umístění již existuje. Chcete jej přepsat?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                string numberTerminal = txtNumberTerminal.Text;
                string company = txtCompany.Text;
                string note = txtContact.Text;
                DateTime dt = dtExpiration.Value;

                XDocument xdoc = new XDocument(
        new XElement("License",
            new XElement("numberTerminal", numberTerminal),
            new XElement("company", company),
            new XElement("contact", note),
            new XElement("created", DateTime.Now.ToString("dd.MM.yyyy")),
                  new XElement("expiration", chkLicenceNeomezena.Checked ? string.Empty : dt.ToString("dd.MM.yyyy"))
        )
    );

                xdoc.Save(pathLicence);


                string license = GetFileContents(pathLicence);



                license = RijndaelWrapper.Encrypt(license, licensepassword);

                using (StreamWriter w = File.AppendText(pathLicence))
                {
                    w.Write("\n" + license);
                }

                /*
               XmlDocument doc = new XmlDocument();

               doc.Load(pathLicence);
                XmlNode newElem = doc.CreateNode("element", "encryprition", "");
                newElem.InnerText = license;
                doc.DocumentElement.AppendChild(newElem);

                doc.Save(pathLicence);
                */
                MessageBox.Show("Licence byla úspěšně vytvořena!\nUmístění:" + (new FileInfo(pathLicence)).FullName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Nastal problém při vytváření licence: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void txtNumberTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
    && !char.IsDigit(e.KeyChar)
    && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void chkLicenceNeomezena_CheckedChanged(object sender, EventArgs e)
        {
            dtExpiration.Enabled = !chkLicenceNeomezena.Checked;
        }
    }
}
