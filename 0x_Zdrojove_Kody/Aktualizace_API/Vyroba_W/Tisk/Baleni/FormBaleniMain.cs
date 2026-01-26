using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Fask.Vyroba_W.ServerAccess;

namespace Fask.Vyroba_W.Tisk.Baleni
{
    public partial class FormBaleniMain : Form
    {
        public FormBaleniMain(Color backgroundColor)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex, "FormBaleniMain");
            }
        }

        private void FormBaleniMain_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            ScannerStart();
        }

        private void FormBaleniMain_KeyDown(object sender, KeyEventArgs e)
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

        private void btnTisk_Click(object sender, EventArgs e)
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
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Enable();
            }
            catch (Exception)
            {
				//Logging.Log.Write(ex);
                //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
                return;
            }
        }

        private void ScannerStop()
        {
            try
            {
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }


        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.MST_W.Scanner.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.MST_W.Scanner.ScannerEventArgs e)
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

        private void ExitModule()
        {
            try
            {
                //if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                if (MessageBox.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;

                ScannerFinalize();

                this.Close();
            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex, "ExitModule");
                MessageBox.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
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
                dsPrintData = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.baleniService.GetBaleniData(objednavkacislo, out balikcislo);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Fask.Vyroba_W.Tisk.Baleni.FormBaleniMain, GetBaleniData");
                MessageBox.Show("N\n" + ex.Message, "WS Balení", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (dsPrintData == null || dsPrintData.Tables.Count == 0 || dsPrintData.Tables[0].Rows.Count == 0)
            {
                tbKodObjednavky.Focus();
                tbKodObjednavky.SelectAll();
				Logging.Log.Write("Žádná data k tisku,objednávka: " + objednavkacislo, "Fask.Vyroba_W.Tisk.Baleni.FormBaleniMain, GetBaleniData");
                MessageBox.Show("Žádná data k tisku.", "WS Balení", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            bool printed;
            
            // tisk
            while (true)
            {
                try
                {
                    //printed = BaleniTisk.Print(dsPrintData, Fask.PrinterFactory.PrinterModules.Baleni, Globals.Configuration.PotvrzovaniPoctuVytisku, string.IsNullOrEmpty(Globals.Configuration.PocetVytisku) ? (int?)null : Convert.ToInt32(Globals.Configuration.PocetVytisku));//2);
                    printed = BaleniTisk.Print(Settings.TiskBaleniSablona, Settings.TiskBaleniTiskarna, dsPrintData, Settings.TiskPotvrzovaniPoctuVytisku, string.IsNullOrEmpty(Settings.TiskPocetVytisku) ? (int?)null : Convert.ToInt32(Settings.TiskPocetVytisku));//2);
                    if (!printed)
                    {
                        DialogResult drPrint = MessageBox.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
						Logging.Log.Write("dsPrintData", "Chyba tisku, opakovat?:" + drPrint.ToString() + ",UID:" + (Globals.Pracovnik == null ? string.Empty : Globals.Pracovnik.id) + ",ULogin:" + (Globals.PracovnikVedouciSmeny == null ? string.Empty : Globals.PracovnikVedouciSmeny.id) + ",TID:" + Settings.TerminalID.ToString() + ",objednavka cislo:" + objednavkacislo + ",balik cislo:" + balikcislo);
                        if (drPrint == DialogResult.Yes)
                            continue;
                        else
                            return;
                    }
                    break;
                }
                catch (Exception ex)
                {
					Logging.Log.Write(ex, "Fask.Vyroba_W.Tisk.Baleni.FormBaleniMain,  Tisk");
                    if (MessageBox.Show(ex.Message + "\nOpakovat?", "Balení chyba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                        == DialogResult.Yes)
                        continue;
                    else
                        return;
                }
            }

            DateTime printedtime = DateTime.Now;    // datum tisku
            Fask.Vyroba_W.WebServiceVyrobaBaleni.StatusBaleni statusBaleni;

            // poslani potvrzeni, ze tisk probehl uspesne
            while (true)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
					statusBaleni = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.baleniService.BaleniDataCommit(Globals.Pracovnik == null ? string.Empty : Globals.Pracovnik.id, Settings.TerminalID, objednavkacislo, balikcislo, printedtime);
                    Cursor.Current = Cursors.Default;
                    switch (statusBaleni.Result)
                    {
                        case Fask.Vyroba_W.WebServiceVyrobaBaleni.STATUS.OK:
                            break;
                        case Fask.Vyroba_W.WebServiceVyrobaBaleni.STATUS.ERROR:
                            throw new Exception(statusBaleni.Message);
                        default:
                            throw new Exception("Neočekávaná chyba, nepodařilo se potvrdit vytisknutí.");
                    }
                }
                catch (Exception exWebSetHmotnost)
                {
                    Cursor.Current = Cursors.Default;
					//Logging.Log.Write(exWebSetHmotnost, "Fask.Vyroba_W.Tisk.Baleni.FormBaleniMain,  Potvrzeni tisku");
                    DialogResult dr = MessageBox.Show(exWebSetHmotnost.Message + "\nOpakovat?", "WS Balení - chyba potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    // zalogovani datasetu + potrebnych dat
					Logging.Log.Write(exWebSetHmotnost, "dsPrintData : Chyba potvrzeni tisku, opakovat?:" + dr.ToString() + ",UID:" + (Globals.Pracovnik == null ? string.Empty : Globals.Pracovnik.id) + ",ULogin:" + (Globals.PracovnikVedouciSmeny == null ? string.Empty : Globals.PracovnikVedouciSmeny.id) + ",TID:" + Settings.TerminalID.ToString() + ",objednavka cislo:" + objednavkacislo + ",balik cislo:" + balikcislo + ",printed time:" + printedtime.ToString());

                    if(dr == DialogResult.Yes)
                        continue;
                    else
                        return;
                }
                break;
            }

            MessageBox.Show("Tisk proběhl úspěšně" + 
            "\nObj.č.: '"+ objednavkacislo + "'" + 
            "\nBal.č.: " + balikcislo + 
            "\nVytištěno: " + printedtime.ToString()
            , this.Text
            , MessageBoxButtons.OK
            , MessageBoxIcon.Asterisk
            , MessageBoxDefaultButton.Button1);
            tbKodObjednavky.Text = string.Empty;
        }
    }
}