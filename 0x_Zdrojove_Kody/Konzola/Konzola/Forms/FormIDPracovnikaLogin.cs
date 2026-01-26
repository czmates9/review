using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using Fask.Vyroba_P;
using System.Reflection;
using System.IO;
using Konzola.Extensions;

namespace Konzola.Forms
{
    public partial class FormIDPracovnikaLogin : Form
    {
        //private Fask.Interfaces.IVyrobaKonzola providerKonzola = null;

        private Fask.Interfaces.IMES provider_uzivatele = null;

        public FormIDPracovnikaLogin()
        {
            InitializeComponent();
        }



        private void InitProvider()
        {
            #region IUzivatele2
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (provider_uzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Uzivatele.IUzivatele2).IsAssignableFrom(t))
                            {
                                provider_uzivatele = (Fask.Interfaces.Uzivatele.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                                if (provider_uzivatele != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                }

                provider_uzivatele.InitProvider();

                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }

        private void textBoxID_Activate()
        {
            textBoxID.SelectAll();
            textBoxID.Focus();
        }

        ///// <summary>
        ///// Inicializace providera.
        ///// </summary>
        //private void InitProvider()
        //{
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
        //        {
        //            if (providerKonzola == null) //inicializace se provede pouze pokud nebyla provedena ... 
        //            {
        //                //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
        //                Assembly providerAssemlby = Assembly.LoadFrom(Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
        //                Type[] types = providerAssemlby.GetTypes();
        //                foreach (Type t in types)
        //                {
        //                    try
        //                    {
        //                        //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
        //                        if (typeof(Fask.Interfaces.Konzola.IKonzola2).IsAssignableFrom(t))
        //                        {
        //                            providerKonzola = (Fask.Interfaces.Konzola.IKonzola2)providerAssemlby.CreateInstance(t.FullName);
        //                            if (providerKonzola != null)
        //                                break;
        //                        }
        //                    }
        //                    catch { }
        //                }
        //                //return config;
        //            }

        //            // nastaveni connection stringu
        //            if ((providerKonzola != null) && (providerKonzola is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
        //                ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerKonzola).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //            //providerKonzola.GetUzivatel("1", "1");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.Konzola");
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void ScannerStart()
        //{
        //    try
        //    {
        //        FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.Enable();
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void ScannerStop()
        //{
        //    try
        //    {
        //        FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.Disable();
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    if (e.BarcodeData.Trim().Length == 0)
        //        return;

        //    this.textBoxID.Text = e.BarcodeData.Trim();

        //    if (textBoxHeslo.Text.Trim().Length == 0)
        //    {
        //        textBoxHeslo.Focus();
        //        textBoxHeslo.SelectAll();
        //        return;
        //    }

        //    this.PerformOK();
        //}

        //void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        //}

        public void PerformCancel()
        {
            //ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        //public void PerformOK()
        //{
        //    //ScannerStop();
        //    try
        //    {


        //        if (providerKonzola == null)
        //            throw new Exception("Provider není inicializován");

        //        Fask.Interfaces.DataSets.Konzola.FASK_LoginsRow user = null;

        //        if (string.IsNullOrEmpty(textBoxID.Text.Trim()))
        //            throw new Exception("ID uživatele není vyplněno");

        //        if (string.IsNullOrEmpty(textBoxHeslo.Text.Trim()))
        //            throw new Exception("Heslo uživatele není vyplněno");

        //        if ((providerKonzola != null) && (providerKonzola is Fask.Interfaces.Konzola.IKonzola2_GetUzivatel))
        //            user = ((Fask.Interfaces.Konzola.IKonzola2_GetUzivatel)providerKonzola).GetUzivatel(textBoxID.Text.Trim());
        //        else
        //            throw new NotImplementedException("Provider neimplementuje IKonzola2_GetUzivatel.");


        //        if (user == null)
        //            throw new Exception("Zadaný uživatel neexistuje");

        //        if (user.psswd != textBoxHeslo.Text.Trim())
        //            throw new Exception("Heslo uživatele " + user.firstname.Trim() + " " + user.surname.Trim() + " není zadáno správně");

        //        Globals.Pracovnik = user;

        //        if ((providerKonzola != null) && (providerKonzola is Fask.Interfaces.Konzola.IKonzola2_GetUzivatelskaOpravneni))
        //            Globals.PracovnikOpravneni = ((Fask.Interfaces.Konzola.IKonzola2_GetUzivatelskaOpravneni)providerKonzola).GetUzivatelskaOpravneni(textBoxID.Text.Trim());
        //        else
        //            throw new NotImplementedException("Provider neimplementuje IKonzola2_GetUzivatelskaOpravneni.");

        //    }
        //    catch (Exception ex)
        //    {
        //        // chyba, nastavit na null
        //        Globals.Pracovnik = null;
        //        Globals.PracovnikOpravneni = null;

        //        Fask.Logging.ExceptionHandler2.Handle(ex, "Prihlaseni uzivatele");
        //        FlexibleMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        //        //ScannerStart();
        //        textBoxID_Activate();
        //        return;
        //    }
        //    DialogResult = DialogResult.OK;
        //}


        public void PerformOK()
        {

            try
            {


                if (string.IsNullOrEmpty(textBoxID.Text.Trim()))
                    throw new Exception("ID uživatele není vyplněno");

                if (string.IsNullOrEmpty(textBoxHeslo.Text.Trim()))
                    throw new Exception("Heslo uživatele není vyplněno");


                if ((provider_uzivatele != null) && (provider_uzivatele is Fask.Interfaces.Uzivatele.IUzivatele2_OverUzivatele))
                {

                    //TODO MaR pozor na posledni dva parametry, je z konfigurace + natvrdo !!
                    string TID_pom = string.Empty;
                    string TID_typ_pom = "Konzola";
                    try
                    {
                       if( !Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].IsTerminalIDNull())
                        {
                            TID_pom = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID.ToString();
                        }

                    }
                    catch (Exception ex)
                    {

                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }

                    ((Fask.Interfaces.Uzivatele.IUzivatele2_OverUzivatele)provider_uzivatele).OverUzivatele(textBoxID.Text.Trim(), textBoxHeslo.Text.Trim(),TID_pom,TID_typ_pom);
                }
                else
                {
                    throw new Exception("Provider neinicializovan!!");
                }
            }
            catch (Exception ex)
            {
                // chyba, nastavit na null
                //Globals.Pracovnik = null;
                //Globals.PracovnikOpravneni = null;

                Fask.Logging.ExceptionHandler2.Handle(ex);
                FlexibleMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                //ScannerStart();
                textBoxID_Activate();
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void FormIDPracovnikaLogin_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
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

        private void FormIDPracovnikaLogin_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK2;
            FormIDPracovnikaLogin_Resize(null, null);
            textBoxID_Activate();
            InitProvider();
            //ScannerStart();
        }

        private void buttonStorno_Click_1(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void FormIDPracovnikaLogin_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormIDPracovnikaLogin_Shown(object sender, EventArgs e)
        {
#if DEBUG            
            textBoxID.Text = "0";
            textBoxHeslo.Text = "1";
#endif
        }
    }
}
