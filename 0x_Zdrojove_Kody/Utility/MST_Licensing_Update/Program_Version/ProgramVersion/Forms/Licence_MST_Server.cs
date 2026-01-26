using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.Xml;

namespace ProgramVersion.LicenceServer
{
    public partial class Licence_MST_Server : Form
    {
        private const string licensepassword = "fask!pro159";
        private string pathToLicence = string.Empty;

        public Licence_MST_Server()
        {
            InitializeComponent();
        }




        private void button4_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;


            try
            {
                //Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "ini files (*.ini)|*.ini";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = "licence.ini";


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                  CreateLicense(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateLicense(string pathLicence)
        {
            try
            {
                //string pathLicence = Path.Combine(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath, "licence.ini");

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


                string license = ProgramVersion.LicenceClasses.Licensing.GetFileContents(pathLicence);



                license =  LicenceClasses.RijndaelWrapper.Encrypt(license, licensepassword);

                using (StreamWriter w = File.AppendText(pathLicence))
                {
                    string EntrPlusLicence = "\n" + license;
                    w.Write(EntrPlusLicence);
                }

                MessageBox.Show("Licence byla úspěšně vytvořena!\nUmístění:" + (new FileInfo(pathLicence)).FullName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Nastal problém při vytváření licence: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void chkLicenceNeomezena_CheckedChanged(object sender, EventArgs e)
        {
            dtExpiration.Enabled = !chkLicenceNeomezena.Checked;
        }

        private void Licence_MST_Server_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK1;
        }



        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                //validace pocet terminalu
                if (string.IsNullOrEmpty(txtNumberTerminal.Text.Trim()))
                    {
                        errorProvider1.SetError(txtNumberTerminal, "Počet terminálú neni vyplněn");
                    }

                //validace pocet terminalu
                if (string.IsNullOrEmpty(txtCompany.Text.Trim()))
                {
                    errorProvider1.SetError(txtCompany, "Společnost neni vyplněna");
                }

                //validace pocet terminalu
                if (string.IsNullOrEmpty(txtContact.Text.Trim()))
                {
                    errorProvider1.SetError(txtContact, "Kontakt na FASK neni vyplněn");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return IsAllValid();
        }


        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in tabPage1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "ini files (*.ini)|*.ini";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = "licence.ini";
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
                Log.Write(ex);
                MessageBox.Show("Nepovedlo se načíst všechny informace z licence, některá informace chybí!");
                return;
            }
        }

        private void txtNumberTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                !char.IsControl(e.KeyChar) 
                && !char.IsDigit(e.KeyChar)
                //&& (e.KeyChar != ',')
                //&& (!NumberFormatInfo.CurrentInfo.NumberDecimalSeparator.Contains(e.KeyChar))
                )
            {
                e.Handled = true;
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

                license = LicenceClasses.Licensing.GetFileContents(pathToLicence);

                //xdoc.Load(pathToLicence);

                license = LicenceClasses.RijndaelWrapper.Encrypt(license, licensepassword);

                using (StreamWriter w = File.AppendText(pathToLicence))
                {
                    string EntrPlusLicence = "\n" + license;
                    w.Write(EntrPlusLicence);
                }

                MessageBox.Show("Licence byla úspěšně vytvořena!\nUmístění:" + (new FileInfo(pathToLicence)).FullName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "ini files (*.ini)|*.ini";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = "licence.ini";
                // openFileDialog1.Title = "Select a Cursor File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    CheckLicense(openFileDialog1.FileName);
                }

                txtLicencePath.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }


        private void CheckLicense(string PathLicence) 
        {
            XmlDocument doc = new XmlDocument();

            string license = LicenceClasses.Licensing.GetFileContents(PathLicence);
            string podpis = string.Empty;
            license = GetXML(license, out podpis);
            doc.LoadXml(license);

            string numberTerminal = doc.GetElementsByTagName("numberTerminal")[0].InnerText;
            string company = doc.GetElementsByTagName("company")[0].InnerText;
            string contact = doc.GetElementsByTagName("contact")[0].InnerText;
            string expiration = doc.GetElementsByTagName("expiration")[0].InnerText;
            string created = doc.GetElementsByTagName("created")[0].InnerText;



            //test podpisu
            license = LicenceClasses.RijndaelWrapper.Encrypt(license, licensepassword);
            if (license != podpis)
            {
                button2.BackColor = Color.Red;
                MessageBox.Show("Licence je nevalidní!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

               

            //test zda aktualni cas neni mensi nez cas vytvoreni licence
            DateTime vytvoreniLicence = DateTime.ParseExact(created, "dd.MM.yyyy", null);
            if (vytvoreniLicence > DateTime.Now)
            {
                button2.BackColor = Color.Red;
                MessageBox.Show("Licence je nevalidní!" + created  , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            
            }

            //test expirace licence
            if (isExpirated(expiration))
            {
                button2.BackColor = Color.Red;
                MessageBox.Show("Vypršela platnost licence!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            button2.BackColor = Color.Green;
        
        }

        private static string GetXML(string x, out string podpis)
        {
            podpis = string.Empty;
            if (x.LastIndexOf("\n") > 0)
            {
                podpis = x.Substring(x.LastIndexOf('\n') + 1);
                return x.Substring(0, x.LastIndexOf("\n"));
            }
            else
            {
                return x;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.BackColor = System.Drawing.SystemColors.Control;
        }

        private static bool isExpirated(string p)
        {
            try
            {
                // Pokud je prazdne, tak je licence casove neomezena ...
                if (String.IsNullOrEmpty(p))
                    return false;

                DateTime dateLicence = DateTime.ParseExact(p, "dd.MM.yyyy", null);

                if (dateLicence > DateTime.Now)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return false;
            }
        }
    }
}
