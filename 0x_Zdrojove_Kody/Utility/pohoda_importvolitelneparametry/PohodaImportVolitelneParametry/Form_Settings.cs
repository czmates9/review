using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PohodaImportVolitelneParametry
{
    public partial class Form_Settings : Form
    {
        public Form_Settings()
        {
            InitializeComponent();
            btnOK.Size = new Size(panelButtons.Width / 2, panelButtons.Height);
        }

        private void Form_Settings_Load(object sender, EventArgs e)
        {
            try
            {
                tb_Catalog.Text = Settings.Catalog;
                tb_ICO.Text = Settings.ICO;

                tb_Data_Source.Text = Settings.Data_Source;
                tb_Domain.Text = Settings.Comunicator_Drive_Mapping_Domain;
                tb_Letter.Text = Settings.Communicator_Drive_Mapping_Letter;
                tb_LoginPohoda.Text = Settings.Pohoda_Login;
                tb_LoginSQL.Text = Settings.SQL_Login;
                tb_LoginUNC.Text = Settings.Comunicator_Drive_Mapping_User;
                tb_PathPohoda.Text = Settings.PathToPohodaEXE;
                cb_Provider.SelectedText = Settings.Provider;
                tb_UNCPath.Text = Settings.Comunicator_Drive_Mapping_UNCPath;
            }
            catch (Exception ex)
            {
                Log.Logging.Write(ex);
            }

        }

        private void btnStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void Form_Settings_Resize(object sender, EventArgs e)
        {
            btnOK.Size = new Size(panelButtons.Width / 2, panelButtons.Height);
        }

        private void PerformOK()
        {

            try
            {
                if (!ValidateData())
                {
                    return;
                }



                try
                {
                    Settings.Catalog = tb_Catalog.Text;
                    Settings.ICO = tb_ICO.Text;

                    Settings.Data_Source = tb_Data_Source.Text;
                    Settings.Comunicator_Drive_Mapping_Domain = tb_Domain.Text;
                    Settings.Communicator_Drive_Mapping_Letter = tb_Letter.Text;
                    Settings.Pohoda_Login = tb_LoginPohoda.Text;
                    Settings.SQL_Login = tb_LoginSQL.Text;
                    Settings.Comunicator_Drive_Mapping_User = tb_LoginUNC.Text;
                    Settings.PathToPohodaEXE = tb_PathPohoda.Text;
                    Settings.Provider = cb_Provider.Text;
                    Settings.Comunicator_Drive_Mapping_UNCPath = tb_UNCPath.Text;

                    if (!string.IsNullOrEmpty(tb_Heslo1Pohoda.Text.Trim()) && !string.IsNullOrEmpty(tb_Heslo2Pohoda.Text.Trim()))
                    {
                        Settings.Pohoda_Heslo = tb_Heslo1Pohoda.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(tb_Heslo1SQL.Text.Trim()) && !string.IsNullOrEmpty(tb_Heslo2SQL.Text.Trim()))
                    {
                        Settings.SQL_Heslo = tb_Heslo1SQL.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(tb_Heslo1UNC.Text.Trim()) && !string.IsNullOrEmpty(tb_Heslo2UNC.Text.Trim()))
                    {
                        Settings.Comunicator_Drive_Mapping_Password = tb_Heslo1UNC.Text.Trim();
                    }

                    Settings.Update();

                }
                catch (Exception ex)
                {
                    Log.Logging.Write(ex);
                }


            }
            catch (Exception ex)
            {
                Log.Logging.Write(ex);
            }

            DialogResult = DialogResult.OK;
        }

        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                if (!string.IsNullOrEmpty(tb_Heslo1SQL.Text))
                {
                    if (string.IsNullOrEmpty(tb_Heslo2SQL.Text))
                    {
                        errorProvider1.SetError(tb_Heslo1SQL, "Zadejte druhé heslo");
                    }
                    else
                    {
                        if (tb_Heslo1SQL.Text.Trim() != tb_Heslo2SQL.Text.Trim())
                        {
                            errorProvider1.SetError(tb_Heslo1SQL, "Hesla nejsou totožná!");
                            errorProvider1.SetError(tb_Heslo2SQL, "Hesla nejsou totožná!");
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(tb_Heslo2SQL.Text))
                {
                    errorProvider1.SetError(tb_Heslo1SQL, "Zadejte první heslo!");
                }
                else
                {
                    if (string.IsNullOrEmpty(Settings.SQL_Heslo))
                    {
                        errorProvider1.SetError(tb_Heslo1SQL, "Zadejte heslo. V konfiguraci neni žadne uložene!");
                    }
                }


                if (!string.IsNullOrEmpty(tb_Heslo1Pohoda.Text))
                {
                    if (string.IsNullOrEmpty(tb_Heslo2Pohoda.Text))
                    {
                        errorProvider1.SetError(tb_Heslo2Pohoda, "Zadejte druhé heslo");
                    }
                    else
                    {
                        if (tb_Heslo1Pohoda.Text.Trim() != tb_Heslo2Pohoda.Text.Trim())
                        {
                            errorProvider1.SetError(tb_Heslo1Pohoda, "Hesla nejsou totožná!");
                            errorProvider1.SetError(tb_Heslo2Pohoda, "Hesla nejsou totožná!");
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(tb_Heslo2Pohoda.Text))
                {
                    errorProvider1.SetError(tb_Heslo1Pohoda, "Zadejte první heslo!");
                }
                else
                {
                    if (string.IsNullOrEmpty(Settings.Pohoda_Heslo))
                    {
                        errorProvider1.SetError(tb_Heslo1Pohoda, "Zadejte heslo. V konfiguraci neni žadne uložene!");
                    }
                }

                if (!string.IsNullOrEmpty(tb_Heslo1UNC.Text))
                {
                    if (string.IsNullOrEmpty(tb_Heslo2UNC.Text))
                    {
                        errorProvider1.SetError(tb_Heslo1UNC, "Zadejte druhé heslo");
                    }
                    else
                    {
                        if (tb_Heslo1UNC.Text.Trim() != tb_Heslo2UNC.Text.Trim())
                        {
                            errorProvider1.SetError(tb_Heslo1UNC, "Hesla nejsou totožná!");
                            errorProvider1.SetError(tb_Heslo2UNC, "Hesla nejsou totožná!");
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(tb_Heslo2UNC.Text))
                {
                    errorProvider1.SetError(tb_Heslo1UNC, "Zadejte první heslo!");
                }
                else
                {
                    if (string.IsNullOrEmpty(Settings.Comunicator_Drive_Mapping_Password) && !string.IsNullOrEmpty(tb_Letter.Text))
                    {
                        errorProvider1.SetError(tb_Heslo1UNC, "Zadejte heslo. V konfiguraci neni žadne uložene!");
                    }
                }

                if (!string.IsNullOrEmpty(tb_Letter.Text))
                {

                    if (string.IsNullOrEmpty(tb_UNCPath.Text))
                    {
                        errorProvider1.SetError(tb_UNCPath, "Cesta je povinny udaj!");
                    }

                    if (string.IsNullOrEmpty(tb_Domain.Text))
                    {
                        errorProvider1.SetError(tb_Domain, "Domena je povinny udaj!");
                    }

                    if (string.IsNullOrEmpty(tb_LoginUNC.Text))
                    {
                        errorProvider1.SetError(tb_LoginUNC, "Login je povinny udaj!");
                    }

                }

                if (string.IsNullOrEmpty(tb_ICO.Text))
                {
                    errorProvider1.SetError(tb_ICO, "ICO je povinny udaj!");
                }

                if (string.IsNullOrEmpty(tb_Catalog.Text))
                {
                    errorProvider1.SetError(tb_Catalog, "Nazev DB je povinny udaj!");
                }


                if (string.IsNullOrEmpty(cb_Provider.Text))
                {
                    errorProvider1.SetError(cb_Provider, "Provider je povinny udaj!");
                }

                if (string.IsNullOrEmpty(tb_Data_Source.Text))
                {
                    errorProvider1.SetError(tb_Data_Source, "Instance DB je povinny udaj!");
                }

                if (string.IsNullOrEmpty(tb_PathPohoda.Text))
                {
                    errorProvider1.SetError(tb_PathPohoda, "Cesta k pohode je povinny udaj!");
                }
                else
                {
                    bool existPOHODA = System.IO.File.Exists(tb_PathPohoda.Text);

                    if (!existPOHODA)
                    {
                        errorProvider1.SetError(tb_PathPohoda, "Soubor :'" + tb_PathPohoda.Text  + "' neexistuje!");
                    }
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
            foreach (Control c in this.panelMain.Controls)
            {
                if (c is Label)
                    continue;



                if (c is GroupBox)
                {
                    foreach (Control co in c.Controls)
                    {
                        if (co is TextBox)
                        {
                            if (errorProvider1.GetError(co) != "")
                                return false;
                        }
                    }
                }
                else
                {
                    if (c is TextBox)
                    {
                        if (errorProvider1.GetError(c) != "")
                            return false;
                    }
                    else if (c is ComboBox)
                    {
                        if (errorProvider1.GetError(c) != "")
                            return false;
                    }

                }
            }

            return true;
        }

        private void tb_ICO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
