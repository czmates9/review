using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Module.MTJ.JimiTore.Baleni.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni.Kontrola
{
    public partial class FormVyberObjednavky : Form
    {
        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni wsBaleni = null;
        public string Objednavka
        {
            get
            {
                return this.tbKodObjednavky.Text;
            }
        }

        public FormVyberObjednavky(Color backgroundColor)
        {
            try
            {
                InitializeComponent();

                Globals.Configuration = new Fask.Module.MTJ.JimiTore.Baleni.DataSets.Configuration();
                try
                {
                    if (System.IO.File.Exists(Globals.ConfigurationFile))
                        Globals.Configuration.ReadXml(Globals.ConfigurationFile);

                    if (Globals.Configuration.PovolitPodbarveniTlacitek)
                        this.panel1.BackColor = backgroundColor;
                }
                catch
                {
                }

                wsBaleni = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni();
                wsBaleni.Url = Globals.ServerAddress + "Baleni.asmx";
                wsBaleni.Timeout = Globals.ServerTimeout;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "FormVyberObjednavkyMain");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch (Exception)
            {
                return;
            }
            EnableScanner();
        }

        public void EnableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Enable();
        }
        public void DisableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Disable();
        }

        private void ScannerStop()
        {
            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch
            {
            }
            try
            {
                DisableScanner();
            }
            catch
            {
            }
        }


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            findCode(barcode);
        }

        private void findCode(string barcode)
        {
            try
            {
                ScannerStop();

                tbKodObjednavky.Text = barcode;

                if (!string.IsNullOrEmpty(tbKodObjednavky.Text.Trim()))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka status = OverObjednavka(tbKodObjednavky.Text);
                    Cursor.Current = Cursors.Default;
                    if (status.Result == Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR)
                    {
                        MessageBoxBig.Show((string.IsNullOrEmpty(status.Message) ? "Chyba při ověřování objednávky" : status.Message), "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        tbKodObjednavky.SelectAll();
                        tbKodObjednavky.Focus();
                        return;
                    }
                    else
                    {
                        ScannerFinalize();
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                ScannerStart();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "FormMain_Load");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void ExitModule()
        {
            try
            {
                if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    return;

                ScannerFinalize();

                if (Globals.Configuration != null)
                    Globals.Configuration.WriteXml(Globals.ConfigurationFile);

                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ExitModule");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ExitModule();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PerformOK()
        {
            try
            {
                ScannerStop();

                if (!string.IsNullOrEmpty(tbKodObjednavky.Text.Trim()))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka status = OverObjednavka(tbKodObjednavky.Text);
                    Cursor.Current = Cursors.Default;
                    if (status.Result == Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR)
                    {
                        MessageBoxBig.Show((string.IsNullOrEmpty(status.Message) ? "Chyba při ověřování objednávky" : status.Message), "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        tbKodObjednavky.SelectAll();
                        tbKodObjednavky.Focus();
                        return;
                    }
                    else
                    {
                        // Result == OK, pokračovat ...

                        ScannerFinalize();
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            ExitModule();
        }

        /// <summary>
        /// Online overeni objednavky.
        /// </summary>
        /// <param name="objednavkacislo">Zadany kod objednavky.</param>
        /// <returns>Status OK, ERROR (+ message v pripade chyby)</returns>
        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka OverObjednavka(string objednavkacislo)
        {
            Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka status = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka();
            try
            {
                return wsBaleni.Kontrola_OverObjednavka(Globals.UserLogin, Globals.TermID, Objednavka);
            }
            catch (Exception ex)
            {
                status.Result = Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR;
                status.Message = ex.Message;
                Logging.Log.Write(ex.Message, "MTJ.JimiTore.Baleni.FormMain, OverObjednavka");
                return status;
            }
        }

        private void btnVybrat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformOK();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }
    }
}