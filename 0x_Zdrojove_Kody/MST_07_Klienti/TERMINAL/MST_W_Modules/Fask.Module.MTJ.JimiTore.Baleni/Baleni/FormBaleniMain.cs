using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Module.MTJ.JimiTore.Baleni.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni.Baleni
{
    public partial class FormBaleniMain : Form
    {
        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni wsBaleni = null;
        public FormBaleniMain(Color backgroundColor)
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
                Logging.Log.Write(ex, "FormBaleniMain");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void btnTisk_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("1) vyhledat data online");
                //MessageBox.Show("2) poskladat etiketu");
                //MessageBox.Show("3) poslat na tiskarnu");
                //MessageBox.Show("4) poslat online info o tisku");
                //MessageBox.Show("5) zobrazit informaci o dokonceni");
                PerformOK();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
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
                //Logging.Log.Write(ex);
                //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
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

                if (!string.IsNullOrEmpty(barcode))
                {
                    ProcessBaleni(barcode);
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            ScannerStart();
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

                this.Close();
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

                if(!string.IsNullOrEmpty(tbKodObjednavky.Text.Trim()))
                    ProcessBaleni(tbKodObjednavky.Text);
            }
            catch (Exception ex)
            {
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

        private void ProcessBaleni(string objednavkacislo)
        {
            //MessageBox.Show("1) vyhledat data online");
            //MessageBox.Show("2) poskladat etiketu");  // PaV
            //MessageBox.Show("3) poslat na tiskarnu");
            //MessageBox.Show("4) poslat online info o tisku");
            //MessageBox.Show("5) zobrazit informaci o dokonceni");

            DataSet dsPrintData = new DataSet();
            int balikcislo = 0; // cislo baliku, ktere vraci procedura, ktera ziskava data pro tisk            

            // ziskani dat pro tisk online
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dsPrintData = wsBaleni.GetBaleniData(objednavkacislo, out balikcislo);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "MTJ.JimiTore.Baleni.FormMain, GetBaleniData");
                MessageBoxBig.Show("N\n" + ex.Message, "WS Balení", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            if (dsPrintData == null || dsPrintData.Tables.Count == 0 || dsPrintData.Tables[0].Rows.Count == 0)
            {
                tbKodObjednavky.Focus();
                tbKodObjednavky.SelectAll();
                Logging.Log.Write("Žádná data k tisku,objednávka: " + objednavkacislo, "MTJ.JimiTore.Baleni.FormMain, GetBaleniData");
                MessageBoxBig.Show("Žádná data k tisku.", "WS Balení", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            bool printed;
            
            // tisk
            while (true)
            {
                try
                {
                    printed = BaleniTisk.Print(dsPrintData, Fask.PrinterFactory.PrinterModules.Baleni, Globals.Configuration.PotvrzovaniPoctuVytisku, string.IsNullOrEmpty(Globals.Configuration.PocetVytisku) ? (int?)null : Convert.ToInt32(Globals.Configuration.PocetVytisku));//2);
                    if (!printed)
                    {
                        DialogResult drPrint = MessageBoxBig.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                        Logging.Log.Write(dsPrintData, "Chyba tisku, opakovat?:" + drPrint.ToString() + ",UID:" + Globals.UserID + ",ULogin:" + Globals.UserLogin + ",TID:" + Globals.TermID.ToString() + ",objednavka cislo:" + objednavkacislo + ",balik cislo:" + balikcislo);
                        if (drPrint == DialogResult.Yes)
                            continue;
                        else
                            return;
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "MTJ.JimiTore.Baleni.FormMain,  Tisk");
                    if (MessageBoxBig.Show(ex.Message + "\nOpakovat?", "Balení chyba", MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                        == DialogResult.Yes)
                        continue;
                    else
                        return;
                }
            }

            DateTime printedtime = DateTime.Now;    // datum tisku
            Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusBaleni statusBaleni;

            // poslani potvrzeni, ze tisk probehl uspesne
            while (true)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    statusBaleni = wsBaleni.BaleniDataCommit(Globals.UserLogin, Globals.TermID, objednavkacislo, balikcislo, printedtime);
                    Cursor.Current = Cursors.Default;
                    switch (statusBaleni.Result)
                    {
                        case Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.OK:
                            break;
                        case Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR:
                            throw new Exception(statusBaleni.Message);
                        default:
                            throw new Exception("Neočekávaná chyba, nepodařilo se potvrdit vytisknutí.");
                    }
                }
                catch (Exception exWebSetHmotnost)
                {
                    Cursor.Current = Cursors.Default;
                    //Logging.Log.Write(exWebSetHmotnost, "MTJ.JimiTore.Baleni.FormMain,  Potvrzeni tisku");
                    DialogResult dr = MessageBoxBig.Show(exWebSetHmotnost.Message + "\nOpakovat?", "WS Balení - chyba potvrzení", MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                    // zalogovani datasetu + potrebnych dat
                    Logging.Log.Write(exWebSetHmotnost, dsPrintData, "Chyba potvrzeni tisku, opakovat?:" + dr.ToString() + ",UID:" + Globals.UserID + ",ULogin:" + Globals.UserLogin + ",TID:" + Globals.TermID.ToString() + ",objednavka cislo:" + objednavkacislo + ",balik cislo:" + balikcislo + ",printed time:" + printedtime.ToString());

                    if(dr == DialogResult.Yes)
                        continue;
                    else
                        return;
                }
                break;
            }

            MessageBoxBig.Show("Tisk proběhl úspěšně" + 
            "\nObj.č.: '"+ objednavkacislo + "'" + 
            "\nBal.č.: " + balikcislo + 
            "\nVytištěno: " + printedtime.ToString()
            , this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
            tbKodObjednavky.Text = string.Empty;
        }
    }
}