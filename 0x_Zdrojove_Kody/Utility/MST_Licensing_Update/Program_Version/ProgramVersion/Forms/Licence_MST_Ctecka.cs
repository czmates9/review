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

namespace ProgramVersion.LicenceCtecka
{
    public partial class Licence_MST_Ctecka : Form
    {
        //private const string licensepassword = "fask!pro159";
        //private string pathToLicence = string.Empty;

        public Licence_MST_Ctecka()
        {
            InitializeComponent();

            //rb_Licence_PodpisSouboru.Checked = true;

#if DEBUG
            txtCompany.Text = "KTO";
            txtContact.Text = "Fask.cz";
            txtTerminalID.Text = "21";
            txtTypeTerminal.Text = "SQLite";
            chkLicenceNeomezena.Checked = true;
            cb_Prijem_New.Checked = true;
            cb_Prodej_New.Checked = true;
            cb_Vydej_New.Checked = true;

#endif
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            try
            {
                //Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = Settings.LicenseCteckaPriponaFile;
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = Settings.LicenseCteckaNameFile;


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                        CreateLicense_vXML(saveFileDialog1.FileName);
                        PodepsatLicenci_PodpisVxml(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PodepsatLicenci_PodpisVxml(string p)
        {
            string license = string.Empty;
            XmlDocument xdoc = new XmlDocument();

            license = LicenceClasses.Licensing.GetFileContents(p);

            if (license.Contains("<Authority>"))
            {
                MessageBox.Show("Soubor uz obsahuje podpis, nelze jej upravit!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            xdoc.Load(p);

            license = LicenceClasses.RijndaelWrapper.Encrypt(license, Settings.LicenseCteckaPassword);

            XmlNode newElem = xdoc.CreateNode("element", "Authority", "");
            newElem.InnerText = license;
            xdoc.DocumentElement.AppendChild(newElem);

            xdoc.Save(p);
        }

        private void CreateLicense_vXML(string pathLicence)
        {
            try
            {
                //string pathLicence = Path.Combine(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath, "licence.ini");

                //if (File.Exists(pathLicence))
                //{
                //    if (MessageBox.Show("Licenční soubor v tomto umístění již existuje. Chcete jej přepsat?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //        return;
                //}

                string numberTerminal = txtTerminalID.Text;
                string typeTerminal = txtTypeTerminal.Text;
                string company = txtCompany.Text;
                string note = txtContact.Text;
                DateTime dt = dtExpiration.Value;

                XDocument xdoc = new XDocument( 
                    new XElement("License",
                        new XElement("numberTerminal", numberTerminal),
                        new XElement("typeTerminal", typeTerminal),
                        new XElement("company", company), 
                        new XElement("contact", note), 
                        new XElement("created", DateTime.Now.ToString("dd.MM.yyyy")),
                        new XElement("expiration", chkLicenceNeomezena.Checked ? Settings.LicenseCteckaExpiredDate.ToString("dd.MM.yyyy") : dt.ToString("dd.MM.yyyy")),
                        new XElement("Inventura1", rb_Inventura1_New.Checked.ToString()),
                        new XElement("Inventura2", rb_Inventura2_New.Checked.ToString()),
                        new XElement("Events", cb_Events_New.Checked.ToString()),
                        new XElement("Expedice", cb_Expedice_New.Checked.ToString()),
                        new XElement("Prijem", cb_Prijem_New.Checked.ToString()),
                        new XElement("Prodej", cb_Prodej_New.Checked.ToString()),
                        new XElement("Servis", cb_Servis_New.Checked.ToString()),
                        new XElement("Tasks", cb_Tasks_New.Checked.ToString()),
                        new XElement("Vydej", cb_Vydej_New.Checked.ToString()),
                        new XElement("PaletoveListky", cb_PaletoveListky_New.Checked.ToString())
                                 )
                                              );

                xdoc.Save(pathLicence);

                //MessageBox.Show("Licence byla úspěšně vytvořena!\nUmístění:" + (new FileInfo(pathLicence)).FullName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

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

        private void Licence_MST_Ctecka_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK1;

            CreateLicenseMenu();

            //chkLicenceNeomezenaRead.ForeColor = Color.Gray; // Read-only appearance
            //rb_Inventura1.ForeColor = Color.Gray;
            //rb_Inventura2.ForeColor = Color.Gray;
            //cb_Events.ForeColor = Color.Gray;
            //cb_Expedice.ForeColor = Color.Gray;
            //cb_Prijem.ForeColor = Color.Gray;
            //cb_Prodej.ForeColor = Color.Gray;
            //cb_Servis.ForeColor = Color.Gray;
            //cb_Tasks.ForeColor = Color.Gray;
            //cb_Vydej.ForeColor = Color.Gray;
            
            chkLicenceNeomezenaRead.AutoCheck = false;      // Read-only behavior
            rb_Inventura1.AutoCheck = false;
            rb_Inventura2.AutoCheck = false;
            cb_Events.AutoCheck = false;
            cb_Expedice.AutoCheck = false;
            cb_Prijem.AutoCheck = false;
            cb_Prodej.AutoCheck = false;
            cb_Servis.AutoCheck = false;
            cb_Tasks.AutoCheck = false;
            cb_Vydej.AutoCheck = false;
            cb_PaletoveListky.AutoCheck = false;

        }

        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                //validace pocet terminalu
                if (string.IsNullOrEmpty(txtTerminalID.Text.Trim()))
                    {
                        errorProvider1.SetError(txtTerminalID, "ID terminálú neni vyplněno");
                    }

                if (string.IsNullOrEmpty(txtTypeTerminal.Text.Trim()))
                {
                    errorProvider1.SetError(txtTypeTerminal, "Typ terminálú neni vyplněn");
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

        private void UpdateUI2(XmlDocument doc)
        {
            try
            {
                XmlNodeList numberTerminalList = doc.GetElementsByTagName("numberTerminal");
                XmlNodeList typeTerminalList = doc.GetElementsByTagName("typeTerminal");
                XmlNodeList companyList = doc.GetElementsByTagName("company");
                XmlNodeList contactList = doc.GetElementsByTagName("contact");
                XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
                XmlNodeList createdList = doc.GetElementsByTagName("created");

                XmlNodeList node_Inventura1 = doc.GetElementsByTagName("Inventura1");
                XmlNodeList node_Inventura2 = doc.GetElementsByTagName("Inventura2");
                XmlNodeList node_Events = doc.GetElementsByTagName("Events");
                XmlNodeList node_Expedice = doc.GetElementsByTagName("Expedice");
                XmlNodeList node_Prijem = doc.GetElementsByTagName("Prijem");
                XmlNodeList node_Prodej = doc.GetElementsByTagName("Prodej");
                XmlNodeList node_Servis = doc.GetElementsByTagName("Servis");
                XmlNodeList node_Tasks = doc.GetElementsByTagName("Tasks");
                XmlNodeList node_Vydej = doc.GetElementsByTagName("Vydej");
                XmlNodeList node_PaletoveListky = doc.GetElementsByTagName("PaletoveListky");

                string numberTerminal = numberTerminalList[0].InnerText;
                string typeTerminal = typeTerminalList[0].InnerText;
                string company = companyList[0].InnerText;
                string contact = contactList[0].InnerText;
                string expiration = expirationList[0].InnerText;
                string created = createdList[0].InnerText;

                rb_Inventura1.Checked = bool.Parse(node_Inventura1[0].InnerText);
                rb_Inventura2.Checked = bool.Parse(node_Inventura2[0].InnerText);
                cb_Events.Checked = bool.Parse(node_Events[0].InnerText);
                cb_Expedice.Checked = bool.Parse(node_Expedice[0].InnerText);
                cb_Prijem.Checked = bool.Parse(node_Prijem[0].InnerText);
                cb_Prodej.Checked = bool.Parse(node_Prodej[0].InnerText);
                cb_Servis.Checked = bool.Parse(node_Servis[0].InnerText);
                cb_Tasks.Checked = bool.Parse(node_Tasks[0].InnerText);
                cb_Vydej.Checked = bool.Parse(node_Vydej[0].InnerText);
                cb_PaletoveListky.Checked = bool.Parse(node_PaletoveListky[0].InnerText);

                txtCompanyRead.Text = company;
                txtContactRead.Text = contact;
                txtNumberTerminalRead.Text = numberTerminal;
                txtTypeTerminalRead.Text = typeTerminal;

                dtExpirationRead.Value = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);

                if (dtExpirationRead.Value == Settings.LicenseCteckaExpiredDate)
                    chkLicenceNeomezenaRead.Checked = true;
                else
                    chkLicenceNeomezenaRead.Checked = false;

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

        private void TestLicence(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = Settings.LicenseCteckaPriponaFile;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = Settings.LicenseCteckaNameFile;
                // openFileDialog1.Title = "Select a Cursor File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int state = CheckLicense_vXML(openFileDialog1.FileName);

                    if (state == 0) 
                        btn_checkLicence.BackColor = Color.Green;
                    else
                        btn_checkLicence.BackColor = Color.Red;
                }

                txtLicencePath.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Slouzi pro overeni zadaneho klice s klicem, ktery se nachazi v souboru "License.xml"
        /// </summary>
        /// <param name="path">Cesta k souboru s licenci (s klicem)</param>
        /// <returns> integer:
        /// 0 - vse ok, klice jsou stejne
        /// -1 - soubor license.xml nenalezen v zadane LcicencePath
        /// -2 - neznama chyba
        /// -3 - licence neni podepsana (chybi Authority node)
        /// -4 - zadany klic (key) neodpovida licenci v license.xml
        /// </returns>
        /// 
        private int CheckLicense_vXML(string path)
        {
            try
            {
                //licensepassword = ;

                string licenseInFile = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                string license = string.Empty;

                //string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), txtFileOvereni.Text.Trim());


                if (!File.Exists(path))
                    return -1;


                xdoc.Load(path);

                XmlNodeList xmlnode = xdoc.GetElementsByTagName("License");

                for (int i = 0; i < xmlnode.Count; i++)
                {
                    XmlAttributeCollection xmlattrc = xmlnode[i].Attributes;

                    for (int a = 0; a < xmlnode[i].ChildNodes.Count; a++)
                    {
                        if (xmlnode[i].ChildNodes[a].Name == "Authority")
                        {
                            licenseInFile = xmlnode[i].ChildNodes[a].InnerText;
                        }
                    }
                }



                string decryptLicense = ProgramVersion.LicenceClasses.RijndaelWrapper.Decrypt(licenseInFile, Settings.LicenseCteckaPassword);
                string fileLicence = ProgramVersion.LicenceClasses.Licensing.GetFileContents(path);
                int index = fileLicence.IndexOf("  <Authority>");

                if (index <= 0)
                    return -3;

                fileLicence = fileLicence.Remove(index, licenseInFile.Length + 27);

                if (fileLicence == decryptLicense)
                {
                    UpdateUI2(xdoc);
                    return 0; 
                }
                else
                    return -4;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return -2;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_checkLicence.BackColor = System.Drawing.SystemColors.Control;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    CreateLicenseMenu();
                    break;
                case 1:
                    ReadLicenseMenu();
                    break;
                default:
                    break;
            }
            
        }

        private void ReadLicenseMenu()
        {
           // throw new NotImplementedException();
            AbouttoolStripMenuItem.Visible = true;
            zobrazitNapoveduToolStripMenuItem.Visible = true;
            napovedaToolStripMenuItem.Visible = true;

            otestovatLicenciToolStripMenuItem.Visible = true;
            podepsatLicenciToolStripMenuItem.Visible = true;
            moznostiToolStripMenuItem.Visible = true;

            upravyToolStripMenuItem.Visible = true;
            smazatCastProCteníToolStripMenuItem.Visible = true;
            odznacInventuryToolStripMenuItem.Visible = false;

            souborToolStripMenuItem.Visible = false;
        }

        private void CreateLicenseMenu()
        {
            AbouttoolStripMenuItem.Visible = true;
            zobrazitNapoveduToolStripMenuItem.Visible = true;
            napovedaToolStripMenuItem.Visible = true;

            otestovatLicenciToolStripMenuItem.Visible = false;
            podepsatLicenciToolStripMenuItem.Visible = false;
            moznostiToolStripMenuItem.Visible = false;

            upravyToolStripMenuItem.Visible = true;
            smazatCastProCteníToolStripMenuItem.Visible = false;
            odznacInventuryToolStripMenuItem.Visible = true;

            souborToolStripMenuItem.Visible = false;


        }

        /// <summary>
        /// vraci hodnoty podle typu trvani
        /// </summary>
        /// <param name="p"></param>
        /// <returns>
        /// -1 pokud je casovo neomedzena
        /// -2 platna
        /// -3 pokud uz cas vyprsel
        /// -4 exception
        /// </returns>
        private static int isExpirated(string p)
        {
            try
            {
                // Pokud je prazdne, tak je licence casove neomezena ...
                if (String.IsNullOrEmpty(p))
                    return -1;

                DateTime dateLicence = DateTime.ParseExact(p, "dd.MM.yyyy", null);

                if (dateLicence > DateTime.Now)
                    return -2;
                else
                    return -3;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return -4;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (about a = new about())
            {
                a.ShowDialog();
            }
        }

        private void smazatČástProČteníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDataRead();
        }

        private void ClearDataRead()
        {
            rb_Inventura1.Checked = false;
            rb_Inventura2.Checked = false;
            cb_Events.Checked = false;
            cb_Expedice.Checked = false;
            cb_Prijem.Checked = false;
            cb_Prodej.Checked = false;
            cb_Servis.Checked = false;
            cb_Tasks.Checked = false;
            cb_Vydej.Checked = false;
            cb_PaletoveListky.Checked = false;
            txtCompanyRead.Text = string.Empty;
            txtContactRead.Text = string.Empty;
            txtNumberTerminalRead.Text = string.Empty;
            txtTypeTerminalRead.Text = string.Empty;
            txtLicencePath.Text = string.Empty;

            chkLicenceNeomezenaRead.Checked = false;

            dtExpirationRead.Value = DateTime.Now;

            btn_checkLicence.BackColor = System.Drawing.SystemColors.Control;
        }

        private void odznačInventuryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rb_Inventura2_New.Checked = false;
            rb_Inventura1_New.Checked = false;
        }

        private void podepsatLicenciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = Settings.LicenseCteckaPriponaFile;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = Settings.LicenseCteckaNameFile;
                // openFileDialog1.Title = "Select a Cursor File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PodepsatLicenci_PodpisVxml(openFileDialog1.FileName);
                }

                txtLicencePath.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //
        }
    }
}
