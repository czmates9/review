using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Ciselniky
{
    public partial class FormRADYEdit : Form
    {
        private Fask.Interfaces.IMES provider = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Rady.FASK_RADYRow returnrow { get; set; }
        /// <summary>
        /// Sklad, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Rady.FASK_RADYRow Radyrow { get; set; }

        public Dictionary<string, string> Napoveda = null;

        public FormRADYEdit()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormRADYEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormRADYEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (provider == null)
                    throw new Exception("Provider 'Rady' není inicializován");

                LoadData();

                if (!NapovedaClass.NapovedacTor(this.Name + ".xml", out Napoveda))
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,"TypDokladu, napoveda se nenačetla");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.Rady.IRady2).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Interfaces.Ciselniky.Rady.IRady2)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    provider.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            // je úprava záznamu, dojde k načtení dat
            if (Radyrow != null)
            {
                tb_Modul.Text = Radyrow.Modul; ;
                tb_Modul_ID2.Text = Radyrow.IsModul_ID2Null() ? string.Empty : Radyrow.Modul_ID2;
                tb_Modul_ID.Text = Radyrow.IsModul_IDNull() ? string.Empty : Radyrow.Modul_ID; ;
                tb_Modul_Funkce.Text = Radyrow.IsModul_FunkceNull() ? string.Empty : Radyrow.Modul_Funkce; ;

                if(!Radyrow.IsPlatnostOdNull())
                {
                        dtp_Od.Value = Radyrow.PlatnostOd;
                        dtp_Od.Checked = true;
                }

                if (!Radyrow.IsPlatnostDoNull())
                {
                        dtp_Do.Value = Radyrow.PlatnostDo;
                        dtp_Do.Checked = true;
                }


                if (!Radyrow.IsDefaultNull())
                    chB_Default.Checked = Radyrow.Default;

                tb_ID.Text = Radyrow.ID.ToString();

                tb_Vloz_Stredisko.Text = Radyrow.IsVloz_StrediskoNull() ? string.Empty : Radyrow.Vloz_Stredisko; ;
                tb_Rada_Nazev.Text = Radyrow.IsRada_NazevNull() ? string.Empty : Radyrow.Rada_Nazev; ;
                tb_Filtr_UserID.Text = Radyrow.IsFiltr_UserIDNull() ? string.Empty : Radyrow.Filtr_UserID; ;
                tb_Vloz_Zakazka.Text = Radyrow.IsVloz_ZakazkaNull() ? string.Empty : Radyrow.Vloz_Zakazka; ;
                tb_Rada_ID.Text = Radyrow.IsRada_IDNull() ? string.Empty : Radyrow.Rada_ID.ToString();
                tb_Rada_Count.Text = Radyrow.IsRada_CountNull() ? string.Empty : Radyrow.Rada_Count.ToString();
                tb_Filtr_SkladID.Text = Radyrow.IsFiltr_SkladIDNull() ? string.Empty : Radyrow.Filtr_SkladID; ;
                tb_Vloz_Cinnost.Text = Radyrow.IsVloz_CinnostNull() ? string.Empty : Radyrow.Vloz_Cinnost; ;
                tb_Rada_Prefix.Text = Radyrow.IsRada_PrefixNull() ? string.Empty : Radyrow.Rada_Prefix; ;

                chB_Kontrola_Disponability.Checked = Radyrow.IsKontrola_DisponabilityNull() ? false : Radyrow.Kontrola_Disponability;
                cb_Vyber_Typ_Prevodka.SelectedText = Radyrow.IsVyber_Typ_PrevodkaNull() ? "" : Radyrow.Vyber_Typ_Prevodka.ToString();
                tb_Prodej_Prijemka_Tisk_Tiskarna.Text = Radyrow.IsProdej_Prijemka_Tisk_TiskarnaNull() ? string.Empty : Radyrow.Prodej_Prijemka_Tisk_Tiskarna;
                tb_Prodej_Vydejka_Tisk_Tiskarna.Text = Radyrow.IsProdej_Vydejka_Tisk_TiskarnaNull() ? string.Empty : Radyrow.Prodej_Vydejka_Tisk_Tiskarna;
                tb_Prodej_Prevodka_Tisk_Tiskarna.Text = Radyrow.IsProdej_Prevodka_Tisk_TiskarnaNull() ? string.Empty : Radyrow.Prodej_Prevodka_Tisk_Tiskarna;
                tb_Prodej_Prijemka_Tisk_ID_sablona.Text = Radyrow.IsProdej_Prijemka_Tisk_ID_sablonaNull() ? string.Empty : Radyrow.Prodej_Prijemka_Tisk_ID_sablona.ToString();
                tb_Prodej_Vydejka_Tisk_ID_sablona.Text = Radyrow.IsProdej_Vydejka_Tisk_ID_sablonaNull() ? string.Empty : Radyrow.Prodej_Vydejka_Tisk_ID_sablona.ToString();
                tb_Prodej_Prevodka_Tisk_ID_sablona.Text = Radyrow.IsProdej_Prevodka_Tisk_ID_sablonaNull() ? string.Empty : Radyrow.Prodej_Prevodka_Tisk_ID_sablona.ToString();


            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
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
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!ValidateData())
                    return;

                // porovnani s existujicim zaznamem
                // TODO: do transakce s editaci/pridanim??
                if (Radyrow == null)
                {
                    Fask.Interfaces.DataSets.Rady ds2 = new Fask.Interfaces.DataSets.Rady();
                    Fask.Interfaces.DataSets.Rady.FASK_RADYRow Row = ds2.FASK_RADY.NewFASK_RADYRow();


                    Row.Modul = tb_Modul.Text;
                    Row.Modul_ID2 = tb_Modul_ID2.Text;
                    Row.Modul_ID = tb_Modul_ID.Text;
                    Row.Modul_Funkce = tb_Modul_Funkce.Text;

                    if (dtp_Do.Checked)
                    {
                        Row.PlatnostDo = dtp_Do.Value;
                    }

                    
                    if (dtp_Od.Checked)
                    {
                        Row.PlatnostOd = dtp_Od.Value;
                    }                    
                    

                    Row.Default = chB_Default.Checked;
                    //Row.ID = int.Parse(tb_ID.Text);
                    Row.Vloz_Stredisko = tb_Vloz_Stredisko.Text;
                    Row.Rada_Nazev = tb_Rada_Nazev.Text;
                    Row.Filtr_UserID = tb_Filtr_UserID.Text;
                    Row.Vloz_Zakazka = tb_Vloz_Zakazka.Text;

                    if (string.IsNullOrEmpty(tb_Rada_ID.Text))
                        Row.SetRada_IDNull();
                    else
                        Row.Rada_ID = int.Parse(tb_Rada_ID.Text);

                    if (string.IsNullOrEmpty(tb_Rada_Count.Text))
                        Row.SetRada_CountNull();
                    else
                        Row.Rada_Count = int.Parse(tb_Rada_Count.Text);
                   
                    
                    Row.Filtr_SkladID = tb_Filtr_SkladID.Text;
                    Row.Vloz_Cinnost = tb_Vloz_Cinnost.Text;
                    Row.Rada_Prefix = tb_Rada_Prefix.Text;


                    Row.Kontrola_Disponability = chB_Kontrola_Disponability.Checked;

                    if (string.IsNullOrEmpty(cb_Vyber_Typ_Prevodka.Text))
                        Row.SetVyber_Typ_PrevodkaNull();
                    else
                        Row.Vyber_Typ_Prevodka = byte.Parse(cb_Vyber_Typ_Prevodka.Text);

                    if (string.IsNullOrEmpty(tb_Prodej_Prijemka_Tisk_Tiskarna.Text))
                        Row.SetProdej_Prijemka_Tisk_TiskarnaNull();
                    else
                        Row.Prodej_Prijemka_Tisk_Tiskarna = tb_Prodej_Prijemka_Tisk_Tiskarna.Text;

                    if (string.IsNullOrEmpty(tb_Prodej_Vydejka_Tisk_Tiskarna.Text))
                        Row.SetProdej_Vydejka_Tisk_TiskarnaNull();
                    else
                        Row.Prodej_Vydejka_Tisk_Tiskarna = tb_Prodej_Vydejka_Tisk_Tiskarna.Text;

                    if (string.IsNullOrEmpty(tb_Prodej_Prevodka_Tisk_Tiskarna.Text))
                        Row.SetProdej_Prevodka_Tisk_TiskarnaNull();
                    else
                        Row.Prodej_Prevodka_Tisk_Tiskarna = tb_Prodej_Prevodka_Tisk_Tiskarna.Text;

                    if (string.IsNullOrEmpty(tb_Prodej_Prijemka_Tisk_ID_sablona.Text))
                        Row.SetProdej_Prijemka_Tisk_ID_sablonaNull();
                    else
                        Row.Prodej_Prijemka_Tisk_ID_sablona = int.Parse(tb_Prodej_Prijemka_Tisk_ID_sablona.Text);

                    if (string.IsNullOrEmpty(tb_Prodej_Vydejka_Tisk_ID_sablona.Text))
                        Row.SetProdej_Vydejka_Tisk_ID_sablonaNull();
                    else
                        Row.Prodej_Vydejka_Tisk_ID_sablona = int.Parse(tb_Prodej_Vydejka_Tisk_ID_sablona.Text);

                    if (string.IsNullOrEmpty(tb_Prodej_Prevodka_Tisk_ID_sablona.Text))
                        Row.SetProdej_Prevodka_Tisk_ID_sablonaNull();
                    else
                        Row.Prodej_Prevodka_Tisk_ID_sablona = int.Parse(tb_Prodej_Prevodka_Tisk_ID_sablona.Text);


                    if ((provider != null) && (provider is Fask.Interfaces.Ciselniky.Rady.IRady2_Insert))
                        ((Fask.Interfaces.Ciselniky.Rady.IRady2_Insert)provider).Insert(Row);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IRady2_Insert");


                }
                else
                {

                    Radyrow.Modul = tb_Modul.Text;
                    Radyrow.Modul_ID2 = tb_Modul_ID2.Text;
                    Radyrow.Modul_ID = tb_Modul_ID.Text;
                    Radyrow.Modul_Funkce = tb_Modul_Funkce.Text;

                    if (dtp_Do.Checked)
                    {
                        Radyrow.PlatnostDo = dtp_Do.Value;
                    }
                    else
                    {
                        Radyrow.SetPlatnostDoNull();
                    }


                    if (dtp_Od.Checked)
                    {
                        Radyrow.PlatnostOd = dtp_Od.Value;
                    }
                    else
                    {
                        Radyrow.SetPlatnostOdNull();
                    }

                    Radyrow.Default = chB_Default.Checked;
                    //Radyrow.ID = int.Parse(tb_ID.Text);
                    Radyrow.Vloz_Stredisko = tb_Vloz_Stredisko.Text;
                    Radyrow.Rada_Nazev = tb_Rada_Nazev.Text;
                    Radyrow.Filtr_UserID = tb_Filtr_UserID.Text;
                    Radyrow.Vloz_Zakazka = tb_Vloz_Zakazka.Text;

                    if (string.IsNullOrEmpty(tb_Rada_ID.Text))
                        Radyrow.SetRada_IDNull();
                    else
                        Radyrow.Rada_ID = int.Parse(tb_Rada_ID.Text);

                    if (string.IsNullOrEmpty(tb_Rada_Count.Text))
                        Radyrow.SetRada_CountNull();
                    else
                        Radyrow.Rada_Count = int.Parse(tb_Rada_Count.Text);

                    Radyrow.Filtr_SkladID = tb_Filtr_SkladID.Text;
                    Radyrow.Vloz_Cinnost = tb_Vloz_Cinnost.Text;
                    Radyrow.Rada_Prefix = tb_Rada_Prefix.Text;

                    Radyrow.Kontrola_Disponability = chB_Kontrola_Disponability.Checked;

                    if (string.IsNullOrEmpty(cb_Vyber_Typ_Prevodka.Text))
                        Radyrow.SetVyber_Typ_PrevodkaNull();
                    else
                        Radyrow.Vyber_Typ_Prevodka = byte.Parse(cb_Vyber_Typ_Prevodka.Text);

                    if (string.IsNullOrEmpty(tb_Prodej_Prijemka_Tisk_Tiskarna.Text))
                        Radyrow.SetProdej_Prijemka_Tisk_TiskarnaNull();
                    else
                        Radyrow.Prodej_Prijemka_Tisk_Tiskarna = tb_Prodej_Prijemka_Tisk_Tiskarna.Text;

                    if (string.IsNullOrEmpty(tb_Prodej_Vydejka_Tisk_Tiskarna.Text))
                        Radyrow.SetProdej_Vydejka_Tisk_TiskarnaNull();
                    else
                        Radyrow.Prodej_Vydejka_Tisk_Tiskarna = tb_Prodej_Vydejka_Tisk_Tiskarna.Text;

                    if (string.IsNullOrEmpty(tb_Prodej_Prevodka_Tisk_Tiskarna.Text))
                        Radyrow.SetProdej_Prevodka_Tisk_TiskarnaNull();
                    else
                        Radyrow.Prodej_Prevodka_Tisk_Tiskarna = tb_Prodej_Prevodka_Tisk_Tiskarna.Text;

                    if (string.IsNullOrEmpty(tb_Prodej_Prijemka_Tisk_ID_sablona.Text))
                        Radyrow.SetProdej_Prijemka_Tisk_ID_sablonaNull();
                    else
                        Radyrow.Prodej_Prijemka_Tisk_ID_sablona = int.Parse(tb_Prodej_Prijemka_Tisk_ID_sablona.Text);

                    if (string.IsNullOrEmpty(tb_Prodej_Vydejka_Tisk_ID_sablona.Text))
                        Radyrow.SetProdej_Vydejka_Tisk_ID_sablonaNull();
                    else
                        Radyrow.Prodej_Vydejka_Tisk_ID_sablona = int.Parse(tb_Prodej_Vydejka_Tisk_ID_sablona.Text);

                    if (string.IsNullOrEmpty(tb_Prodej_Prevodka_Tisk_ID_sablona.Text))
                        Radyrow.SetProdej_Prevodka_Tisk_ID_sablonaNull();
                    else
                        Radyrow.Prodej_Prevodka_Tisk_ID_sablona = int.Parse(tb_Prodej_Prevodka_Tisk_ID_sablona.Text);


                    if ((provider != null) && (provider is Fask.Interfaces.Ciselniky.Rady.IRady2_Update))
                        ((Fask.Interfaces.Ciselniky.Rady.IRady2_Update)provider).Update(Radyrow);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IRady2_Update");

                    this.returnrow = Radyrow;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                //if (string.IsNullOrEmpty(tb_Modul.Text.Trim()))
                //    errorProvider1.SetError(tb_Modul, "Musíte zadat id skladu");

                int tmp;

                if (!string.IsNullOrEmpty(tb_Rada_ID.Text))
                {
                    if (!int.TryParse(tb_Rada_ID.Text, out tmp))
                    {
                        errorProvider1.SetError(tb_Rada_ID, "Musíte zadat celé číslo");
                    }
                }


                if (!string.IsNullOrEmpty(tb_Rada_Count.Text))
                {

                    if (!int.TryParse(tb_Rada_Count.Text, out tmp))
                    {
                        errorProvider1.SetError(tb_Rada_Count, "Musíte zadat celé číslo");
                    } 
                }


                if (!string.IsNullOrEmpty(tb_Prodej_Prevodka_Tisk_ID_sablona.Text))
                {

                    if (!int.TryParse(tb_Prodej_Prevodka_Tisk_ID_sablona.Text, out tmp))
                    {
                        errorProvider1.SetError(tb_Prodej_Prevodka_Tisk_ID_sablona, "Musíte zadat celé číslo");
                    }
                }

                if (!string.IsNullOrEmpty(tb_Prodej_Prijemka_Tisk_ID_sablona.Text))
                {

                    if (!int.TryParse(tb_Prodej_Prijemka_Tisk_ID_sablona.Text, out tmp))
                    {
                        errorProvider1.SetError(tb_Prodej_Prijemka_Tisk_ID_sablona, "Musíte zadat celé číslo");
                    }
                }

                if (!string.IsNullOrEmpty(tb_Prodej_Vydejka_Tisk_ID_sablona.Text))
                {

                    if (!int.TryParse(tb_Prodej_Vydejka_Tisk_ID_sablona.Text, out tmp))
                    {
                        errorProvider1.SetError(tb_Prodej_Vydejka_Tisk_ID_sablona, "Musíte zadat celé číslo");
                    }
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void FormRADYEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void FormRADYEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    //text = TextBoxToTextManual( tb);
                    text = TextBoxToTextAutomat(tb);
               }
                else if (sender is ComboBox)
                {
                    ComboBox tb = ((ComboBox)sender);
                    //text = TextBoxToTextManual( tb);
                    text = ComboBoxToTextAutomat(tb);
                }


            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }

        private string TextBoxToTextAutomat(TextBox tb)
        {

            string Text = string.Empty;
            string NameTB = tb.Name;

            string NameColumn = NameTB.Replace("tb_", "");

            if (Napoveda.ContainsKey(NameColumn.Trim()))
            {
                Text = Napoveda[NameColumn];
            }

            return Text;
        }

        private string ComboBoxToTextAutomat(ComboBox tb)
        {

            string Text = string.Empty;
            string NameTB = tb.Name;

            string NameColumn = NameTB.Replace("cb_", "");

            if (Napoveda.ContainsKey(NameColumn.Trim()))
            {
                Text = Napoveda[NameColumn];
            }

            return Text;
        }

    }
}
