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

namespace ProgramVersion.LicenceKonzola
{
    public partial class Licence_MST_Konzola : Form
    {
        //private const string licensepassword = "fask!pro159";
        //private string pathToLicence = string.Empty;

        public Licence_MST_Konzola()
        {
            InitializeComponent();

            txtContact.Text = "info@fask.cz";
            chkLicenceNeomezena.Checked = true;

#if DEBUG
            txtCompany.Text = "FASK";
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

                saveFileDialog1.Filter = Settings.LicenseKonzolaPriponaFile;
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = Settings.LicenseKonzolaNameFile;


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

            license = LicenceClasses.RijndaelWrapper.Encrypt(license, Settings.LicenseKonzolaPassword);

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

                string company = txtCompany.Text;
                string note = txtContact.Text;
                DateTime dt = dtExpiration.Value;

                XDocument xdoc = new XDocument(
                    new XElement("License",
                        new XElement("company", company),
                        new XElement("contact", note),
                        new XElement("created", DateTime.Now.ToString("dd.MM.yyyy")),
                        new XElement("expiration", chkLicenceNeomezena.Checked ? Settings.LicenseKonzolaExpiredDate.ToString("dd.MM.yyyy") : dt.ToString("dd.MM.yyyy")),
                        new XElement("Ukolovani", panel_Licence1.Ukolovani),
                        new XElement("Planovani", panel_Licence1.Planovani),
                        new XElement("Planovani_PlanyVV", panel_Licence1.Planovani_PlanyVV),
                        new XElement("Planovani_Kapac", panel_Licence1.Planovani_Kapac),
                        new XElement("Vyroba", panel_Licence1.Vyroba),
                        new XElement("VyrobaCiselniky", panel_Licence1.VyrobaCiselniky),
                        new XElement("VyrobaCiselnikySkupiny", panel_Licence1.VyrobaCiselnikySkupiny),
                        new XElement("VyrobaCiselnikyZasoby", panel_Licence1.VyrobaCiselnikyZasoby),
                        new XElement("VyrobaCiselnikyVazbyMaterialy", panel_Licence1.VyrobaCiselnikyVazbyMaterialy),
                        new XElement("VyrobaRozbory", panel_Licence1.VyrobaRozbory),
                        new XElement("VyrobaRozboryPlanVyroby", panel_Licence1.VyrobaRozboryPlanVyroby),
                        new XElement("VyrobaRozboryOdvadeniStroju", panel_Licence1.VyrobaRozboryOdvadeniStroju),
                        new XElement("VyrobaRozboryVyrobky", panel_Licence1.VyrobaRozboryVyrobky),
                        new XElement("VyrobaRozboryVyrobkySN", panel_Licence1.VyrobaRozboryVyrobkySN),
                        new XElement("VyrobaRozboryMaterialy", panel_Licence1.VyrobaRozboryMaterialy),
                        new XElement("VyrobaTransakce", panel_Licence1.VyrobaTransakce),
                        new XElement("VyrobaTransakceOdvodPOHODA", panel_Licence1.VyrobaTransakceOdvodPOHODA),
                        new XElement("VyrobaTransakceVyrobnyPrikaz", panel_Licence1.VyrobaTransakceVyrobnyPrikaz),
                        new XElement("Sklady", panel_Licence1.Sklady),
                        new XElement("SkladyCiselniky", panel_Licence1.SkladyCiselniky),
                        new XElement("SkladyCiselnikySklady", panel_Licence1.SkladyCiselnikySklady),
                        new XElement("SkladyCiselnikyStrediska", panel_Licence1.SkladyCiselnikyStrediska),
                        new XElement("SkladyCiselnikyMapaLokaci", panel_Licence1.SkladyCiselnikyMapaLokaci),
                        new XElement("SkladyCiselnikyVariantyLokaciMaterialu", panel_Licence1.SkladyCiselnikyVariantyLokaciMaterialu),
                        new XElement("SkladyCiselnikyTypyLokaci", panel_Licence1.SkladyCiselnikyTypyLokaci),
                        new XElement("SkladyCiselnikyZasoby", panel_Licence1.SkladyCiselnikyZasoby),
                        new XElement("SkladyCiselnikyAdresar", panel_Licence1.SkladyCiselnikyAdresar),
                        new XElement("SkladyTransakce", panel_Licence1.SkladyTransakce),
                        new XElement("SkladyTransakcePrP", panel_Licence1.SkladyTransakcePrP),
                        new XElement("SkladyTransakceVyP", panel_Licence1.SkladyTransakceVyP),
                        new XElement("SkladyTransakceExpedice", panel_Licence1.SkladyTransakceExpedice),
                        new XElement("SkladyTransakceVolnyPohyb", panel_Licence1.SkladyTransakceVolnyPohyb),
                        new XElement("SkladyTransakcePrevod", panel_Licence1.SkladyTransakcePrevod),
                        new XElement("SkladyTransakceInventura", panel_Licence1.SkladyTransakceInventura),
                        new XElement("SkladyRozbory", panel_Licence1.SkladyRozbory),
                        new XElement("SkladyRozboryPohyby", panel_Licence1.SkladyRozboryPohyby),
                        new XElement("SkladyRozboryStavy", panel_Licence1.SkladyRozboryStavy),
                        new XElement("SkladyRozboryInv", panel_Licence1.SkladyRozboryInv),
                        new XElement("SkladyRozboryInv_Stav", panel_Licence1.SkladyRozboryInv_Stav),
                        new XElement("SkladyRozboryLokMech", panel_Licence1.SkladyRozboryLokMech),
                        new XElement("SkladyRozboryLokMec_Stavy", panel_Licence1.SkladyRozboryLokMec_Stavy),
                        new XElement("StavSkladu", panel_Licence1.StavSkladu),
                        new XElement("Servis", panel_Licence1.Servis),
                        new XElement("IT_Cast", panel_Licence1.IT_Cast),
                        new XElement("IPTerminalu", panel_Licence1.IPTerminalu),
                        new XElement("IPTerminalu", panel_Licence1.IPTerminalu),
                        new XElement("TypyDokladu", panel_Licence1.TypyDokladu),
                        new XElement("Fask_Rady", panel_Licence1.Fask_Rady),
                        new XElement("Ostatni", panel_Licence1.Ostatni)
                        ));

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

        private void Licence_MST_Konzola_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK1;

            CreateLicenseMenu();

            chkLicenceNeomezenaRead.ForeColor = Color.Gray; // Read-only appearance

            Panel_Licence_Read.All_SetGray();
            Panel_Licence_Read.All_AutoCheck();
            Panel_Licence_Read.ClearAll();

        }

        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

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
                XmlNodeList companyList = doc.GetElementsByTagName("company");
                XmlNodeList contactList = doc.GetElementsByTagName("contact");
                XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
                XmlNodeList createdList = doc.GetElementsByTagName("created");
                XmlNodeList Ukolovani = doc.GetElementsByTagName("Ukolovani");
                XmlNodeList Planovani = doc.GetElementsByTagName("Planovani");
                XmlNodeList Planovani_PlanyVV = doc.GetElementsByTagName("Planovani_PlanyVV");
                XmlNodeList Planovani_Kapac = doc.GetElementsByTagName("Planovani_Kapac");
                XmlNodeList Vyroba = doc.GetElementsByTagName("Vyroba");
                XmlNodeList VyrobaCiselniky = doc.GetElementsByTagName("VyrobaCiselniky");
                XmlNodeList VyrobaCiselnikySkupiny = doc.GetElementsByTagName("VyrobaCiselnikySkupiny");
                XmlNodeList VyrobaCiselnikyZasoby = doc.GetElementsByTagName("VyrobaCiselnikyZasoby");
                XmlNodeList VyrobaCiselnikyVazbyMaterialy = doc.GetElementsByTagName("VyrobaCiselnikyVazbyMaterialy");
                XmlNodeList VyrobaRozbory = doc.GetElementsByTagName("VyrobaRozbory");
                XmlNodeList VyrobaRozboryPlanVyroby = doc.GetElementsByTagName("VyrobaRozboryPlanVyroby");
                XmlNodeList VyrobaRozboryOdvadeniStroju = doc.GetElementsByTagName("VyrobaRozboryOdvadeniStroju");
                XmlNodeList VyrobaRozboryVyrobky = doc.GetElementsByTagName("VyrobaRozboryVyrobky");
                XmlNodeList VyrobaRozboryVyrobkySN = doc.GetElementsByTagName("VyrobaRozboryVyrobkySN");
                XmlNodeList VyrobaRozboryMaterialy = doc.GetElementsByTagName("VyrobaRozboryMaterialy");
                XmlNodeList VyrobaTransakce = doc.GetElementsByTagName("VyrobaTransakce");
                XmlNodeList VyrobaTransakceOdvodPOHODA = doc.GetElementsByTagName("VyrobaTransakceOdvodPOHODA");
                XmlNodeList VyrobaTransakceVyrobnyPrikaz = doc.GetElementsByTagName("VyrobaTransakceVyrobnyPrikaz");
                XmlNodeList Sklady = doc.GetElementsByTagName("Sklady");
                XmlNodeList SkladyCiselniky = doc.GetElementsByTagName("SkladyCiselniky");
                XmlNodeList SkladyCiselnikySklady = doc.GetElementsByTagName("SkladyCiselnikySklady");
                XmlNodeList SkladyCiselnikyStrediska = doc.GetElementsByTagName("SkladyCiselnikyStrediska");
                XmlNodeList SkladyCiselnikyMapaLokaci = doc.GetElementsByTagName("SkladyCiselnikyMapaLokaci");
                XmlNodeList SkladyCiselnikyVariantyLokaciMaterialu = doc.GetElementsByTagName("SkladyCiselnikyVariantyLokaciMaterialu");
                XmlNodeList SkladyCiselnikyTypyLokaci = doc.GetElementsByTagName("SkladyCiselnikyTypyLokaci");
                XmlNodeList SkladyCiselnikyZasoby = doc.GetElementsByTagName("SkladyCiselnikyZasoby");
                XmlNodeList SkladyCiselnikyAdresar = doc.GetElementsByTagName("SkladyCiselnikyAdresar");
                XmlNodeList SkladyTransakce = doc.GetElementsByTagName("SkladyTransakce");
                XmlNodeList SkladyTransakcePrP = doc.GetElementsByTagName("SkladyTransakcePrP");
                XmlNodeList SkladyTransakceVyP = doc.GetElementsByTagName("SkladyTransakceVyP");
                XmlNodeList SkladyTransakceExpedice = doc.GetElementsByTagName("SkladyTransakceExpedice");
                XmlNodeList SkladyTransakceVolnyPohyb = doc.GetElementsByTagName("SkladyTransakceVolnyPohyb");
                XmlNodeList SkladyTransakcePrevod = doc.GetElementsByTagName("SkladyTransakcePrevod");
                XmlNodeList SkladyTransakceInventura = doc.GetElementsByTagName("SkladyTransakceInventura");
                XmlNodeList SkladyRozbory = doc.GetElementsByTagName("SkladyRozbory");
                XmlNodeList SkladyRozboryPohyby = doc.GetElementsByTagName("SkladyRozboryPohyby");
                XmlNodeList SkladyRozboryStavy = doc.GetElementsByTagName("SkladyRozboryStavy");
                XmlNodeList SkladyRozboryInv = doc.GetElementsByTagName("SkladyRozboryInv");
                XmlNodeList SkladyRozboryInv_Stav = doc.GetElementsByTagName("SkladyRozboryInv_Stav");
                XmlNodeList SkladyRozboryLokMech = doc.GetElementsByTagName("SkladyRozboryLokMech");
                XmlNodeList SkladyRozboryLokMec_Stavy = doc.GetElementsByTagName("SkladyRozboryLokMec_Stavy");
                XmlNodeList StavSkladu = doc.GetElementsByTagName("StavSkladu");
                XmlNodeList Servis = doc.GetElementsByTagName("Servis");
                XmlNodeList IT_Cast = doc.GetElementsByTagName("IT_Cast");
                XmlNodeList IPTerminalu = doc.GetElementsByTagName("IPTerminalu");
                XmlNodeList TypyDokladu = doc.GetElementsByTagName("TypyDokladu");
                XmlNodeList Fask_Rady = doc.GetElementsByTagName("Fask_Rady");
                XmlNodeList Ostatni = doc.GetElementsByTagName("Ostatni");

                string company = companyList[0].InnerText;
                string contact = contactList[0].InnerText;
                string expiration = expirationList[0].InnerText;
                string created = createdList[0].InnerText;

               
                Panel_Licence_Read.Ukolovani= bool.Parse(Ukolovani[0].InnerText);
                Panel_Licence_Read.Planovani= bool.Parse(Planovani[0].InnerText);
                Panel_Licence_Read.Planovani_PlanyVV= bool.Parse(Planovani_PlanyVV[0].InnerText);
                Panel_Licence_Read.Planovani_Kapac= bool.Parse(Planovani_Kapac[0].InnerText);
                Panel_Licence_Read.Vyroba= bool.Parse(Vyroba[0].InnerText);
                Panel_Licence_Read.VyrobaCiselniky= bool.Parse(VyrobaCiselniky[0].InnerText);
                Panel_Licence_Read.VyrobaCiselnikySkupiny= bool.Parse(VyrobaCiselnikySkupiny[0].InnerText);
                Panel_Licence_Read.VyrobaCiselnikyZasoby= bool.Parse(VyrobaCiselnikyZasoby[0].InnerText);
                Panel_Licence_Read.VyrobaCiselnikyVazbyMaterialy= bool.Parse(VyrobaCiselnikyVazbyMaterialy[0].InnerText);
                Panel_Licence_Read.VyrobaRozbory= bool.Parse(VyrobaRozbory[0].InnerText);
                Panel_Licence_Read.VyrobaRozboryPlanVyroby= bool.Parse(VyrobaRozboryPlanVyroby[0].InnerText);
                Panel_Licence_Read.VyrobaRozboryOdvadeniStroju= bool.Parse(VyrobaRozboryOdvadeniStroju[0].InnerText);
                Panel_Licence_Read.VyrobaRozboryVyrobky= bool.Parse(VyrobaRozboryVyrobky[0].InnerText);
                Panel_Licence_Read.VyrobaRozboryVyrobkySN= bool.Parse(VyrobaRozboryVyrobkySN[0].InnerText);
                Panel_Licence_Read.VyrobaRozboryMaterialy= bool.Parse(VyrobaRozboryMaterialy[0].InnerText);
                Panel_Licence_Read.VyrobaTransakce= bool.Parse(VyrobaTransakce[0].InnerText);
                Panel_Licence_Read.VyrobaTransakceOdvodPOHODA= bool.Parse(VyrobaTransakceOdvodPOHODA[0].InnerText);
                Panel_Licence_Read.VyrobaTransakceVyrobnyPrikaz= bool.Parse(VyrobaTransakceVyrobnyPrikaz[0].InnerText);
                Panel_Licence_Read.Sklady= bool.Parse(Sklady[0].InnerText);
                Panel_Licence_Read.SkladyCiselniky= bool.Parse(SkladyCiselniky[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikySklady= bool.Parse(SkladyCiselnikySklady[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikyStrediska= bool.Parse(SkladyCiselnikyStrediska[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikyMapaLokaci= bool.Parse(SkladyCiselnikyMapaLokaci[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikyVariantyLokaciMaterialu= bool.Parse(SkladyCiselnikyVariantyLokaciMaterialu[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikyTypyLokaci= bool.Parse(SkladyCiselnikyTypyLokaci[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikyZasoby= bool.Parse(SkladyCiselnikyZasoby[0].InnerText);
                Panel_Licence_Read.SkladyCiselnikyAdresar= bool.Parse(SkladyCiselnikyAdresar[0].InnerText);
                Panel_Licence_Read.SkladyTransakce= bool.Parse(SkladyTransakce[0].InnerText);
                Panel_Licence_Read.SkladyTransakcePrP= bool.Parse(SkladyTransakcePrP[0].InnerText);
                Panel_Licence_Read.SkladyTransakceVyP= bool.Parse(SkladyTransakceVyP[0].InnerText);
                Panel_Licence_Read.SkladyTransakceExpedice= bool.Parse(SkladyTransakceExpedice[0].InnerText);
                Panel_Licence_Read.SkladyTransakceVolnyPohyb= bool.Parse(SkladyTransakceVolnyPohyb[0].InnerText);
                Panel_Licence_Read.SkladyTransakcePrevod= bool.Parse(SkladyTransakcePrevod[0].InnerText);
                Panel_Licence_Read.SkladyTransakceInventura= bool.Parse(SkladyTransakceInventura[0].InnerText);
                Panel_Licence_Read.SkladyRozbory= bool.Parse(SkladyRozbory[0].InnerText);
                Panel_Licence_Read.SkladyRozboryPohyby= bool.Parse(SkladyRozboryPohyby[0].InnerText);
                Panel_Licence_Read.SkladyRozboryStavy= bool.Parse(SkladyRozboryStavy[0].InnerText);
                Panel_Licence_Read.SkladyRozboryInv= bool.Parse(SkladyRozboryInv[0].InnerText);
                Panel_Licence_Read.SkladyRozboryInv_Stav= bool.Parse(SkladyRozboryInv_Stav[0].InnerText);
                Panel_Licence_Read.SkladyRozboryLokMech= bool.Parse(SkladyRozboryLokMech[0].InnerText);
                Panel_Licence_Read.SkladyRozboryLokMec_Stavy= bool.Parse(SkladyRozboryLokMec_Stavy[0].InnerText);
                Panel_Licence_Read.StavSkladu= bool.Parse(StavSkladu[0].InnerText);
                Panel_Licence_Read.Servis= bool.Parse(Servis[0].InnerText);
                Panel_Licence_Read.IT_Cast= bool.Parse(IT_Cast[0].InnerText);
                Panel_Licence_Read.IPTerminalu= bool.Parse(IPTerminalu[0].InnerText);
                Panel_Licence_Read.TypyDokladu= bool.Parse(TypyDokladu[0].InnerText);
                Panel_Licence_Read.Fask_Rady= bool.Parse(Fask_Rady[0].InnerText);
                Panel_Licence_Read.Ostatni= bool.Parse(Ostatni[0].InnerText);

                txtCompanyRead.Text = company;
                txtContactRead.Text = contact;
                dtExpirationRead.Value = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);

                if (dtExpirationRead.Value == Settings.LicenseKonzolaExpiredDate)
                    chkLicenceNeomezenaRead.Checked = true;
                else
                    chkLicenceNeomezenaRead.Checked = false;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Nepovedlo se načíst všechny informace z licence, některá informace chybí!");
                throw ex;
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
                openFileDialog1.Filter = Settings.LicenseKonzolaPriponaFile;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = Settings.LicenseKonzolaNameFile;
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



                string decryptLicense = ProgramVersion.LicenceClasses.RijndaelWrapper.Decrypt(licenseInFile, Settings.LicenseKonzolaPassword);
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

        private void smazatCastProCteniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDataRead();
        }

        private void ClearDataRead()
        {
            txtCompanyRead.Text = string.Empty;
            txtContactRead.Text = string.Empty;
            chkLicenceNeomezenaRead.Checked = false;
            dtExpirationRead.Value = DateTime.Now;

            btn_checkLicence.BackColor = System.Drawing.SystemColors.Control;

            Panel_Licence_Read.ClearAll();

            txtLicencePath.Text = string.Empty;

        }

        private void podepsatLicenciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = Settings.LicenseKonzolaPriponaFile;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = Settings.LicenseKonzolaNameFile;
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

        private void btn_UNselectAll_Click(object sender, EventArgs e)
        {
            panel_Licence1.ClearAll();
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            panel_Licence1.SelectAll();
        }

    }
}
