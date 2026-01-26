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
using System.Xml;

namespace MST_W_Licensing
{
    public partial class Form_Main : Form
    {
        //Musi byt stejne jako je v Fask.MST_W.License.Licensing.licensepassword
        private string licensepassword = "fask!pro159";

        public Form_Main()
        {
            InitializeComponent();

            txtHeslo.Text = licensepassword;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                licensepassword = txtHeslo.Text.Trim();

                string license = string.Empty;
                XmlDocument xdoc = new XmlDocument();


                string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), txtFilename.Text.Trim());

                if (!File.Exists(path))
                {

                    path = txtFilename.Text.Trim();
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Zadany soubor neexistuje!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                license = GetFileContents(path);

                if (license.Contains("<Authority>"))
                {
                    MessageBox.Show("Soubor uz obsahuje podpis, nelze jej upravit!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                
                xdoc.Load(path);
                /*
                XmlNodeList xmlnode = xdoc.GetElementsByTagName("License");

                for (int i = 0; i < xmlnode.Count; i++)
                {
                    XmlAttributeCollection xmlattrc = xmlnode[i].Attributes;

                    for (int a = 0; a < xmlnode[i].ChildNodes.Count; a++)
                    {
                        if (xmlnode[i].ChildNodes[a].Name == "Authority")
                        {
                            MessageBox.Show("Soubor uz obsahuje podpis, nelze jej upravit!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        license += xmlnode[i].ChildNodes[a].InnerText;
                    }
                }
                */

                license = Fask.Encryption.RijndaelWrapper.Encrypt(license, licensepassword);

                XmlNode newElem = xdoc.CreateNode("element", "Authority", "");
                newElem.InnerText = license;
                xdoc.DocumentElement.AppendChild(newElem);

                xdoc.Save(path);

                MessageBox.Show("Licence uspesne pridana!", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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
            txtFilename.Focus();
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                licensepassword = txtHeslo.Text.Trim();

                string licenseInFile = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                string license = string.Empty;

                string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), txtFileOvereni.Text.Trim());

                if (!File.Exists(path))
                {
                    path = txtFilename.Text.Trim();
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Zadany soubor neexistuje!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                }
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



                string decryptLicense = Fask.Encryption.RijndaelWrapper.Decrypt(licenseInFile, licensepassword);
                string fileLicence = GetFileContents(path);
                int index = fileLicence.IndexOf("  <Authority>");
                fileLicence = fileLicence.Remove(index, licenseInFile.Length + 27);

                if (fileLicence != decryptLicense)
                {
                    MessageBox.Show("Licence v souboru neodpovida parametrum! Je automaticky zavolana policie.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (fileLicence == decryptLicense)
                    MessageBox.Show("Licence je korektní!", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                else
                    MessageBox.Show("Licence neni urcena pro tento terminal!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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

    }
}