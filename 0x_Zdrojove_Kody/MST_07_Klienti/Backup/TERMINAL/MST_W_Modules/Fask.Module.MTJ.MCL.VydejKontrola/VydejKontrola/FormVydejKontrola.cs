using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Module.MTJ.MCL.VydejKontrola.Forms;
using System.Threading;
using System.IO;

namespace Fask.Module.MTJ.MCL.VydejKontrola.VydejKontrola
{
    public partial class FormVydejKontrola : Form
    {
        private DataSets.VydejKontrolaDSTableAdapters.fask_Vydej_KontrolaTableAdapter ta_vydej_kontrola = new Fask.Module.MTJ.MCL.VydejKontrola.DataSets.VydejKontrolaDSTableAdapters.fask_Vydej_KontrolaTableAdapter();


        private void Dataset2SqlCommitChanges()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int updated = ta_vydej_kontrola.Update(vydejKontrolaDS);
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool Dataset2SqlCommitChangesTry()
        {
            do
            {
                if (vydejKontrolaDS.HasChanges())
                    Dataset2SqlCommitChanges();

                if (vydejKontrolaDS.HasChanges())
                {
                    if (DialogResult.No == MessageBoxBig.Show("Změny nebyly zapsány na server!\nNelze pokračovat...\nOpakovat pokus o zapsání?", "Uložení dat", MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical))
                        return false;
                }
                else
                    return true;

            } while (true);

        }

        public FormVydejKontrola()
        {
            InitializeComponent();

            ta_vydej_kontrola.Connection = new System.Data.SqlClient.SqlConnection(Globals.Configuration.SQLConnectionString);
        }

        private void FormVydejKontrola_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            dataGrid1.Load(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));

            UpdateStatusBar();

            ScannerStart();

            this.BeginInvoke((ThreadStart)delegate() {
                ZmenCisloDokladu();
            });
        }

        private bool _scanner_enable = true;
        private void ScannerStart()
        {
            if (!_scanner_enable)
                return;

            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.Enable();
            }
            catch (Exception exScanner)
            {
                Logging.Log.Write(exScanner);
            }
        }

        private void ScannerStop()
        {
            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.Disable();
            }
            catch (Exception exScanner)
            {
                Logging.Log.Write(exScanner);
            }
        }

        private void ScannerFinalize()
        {
            this._scanner_enable = false;
            this.ScannerStop();
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke((System.Threading.ThreadStart)delegate() 
                { 
                    Scanner_DataReady_Handler(e.BarcodeData.Trim()); 
                });
        }

        void Scanner_DataReady_Handler(string barcode)
        {
            PridejPolozkuSOverenim(barcode);
        }

        private void PridejPolozkuSOverenim(string barcode)
        {
            DateTime barcodeDatetime = DateTime.Now;
            string barcodeCheck = string.Empty;
            DateTime barcodeCheckDatetime = DateTime.Now;

            if (String.IsNullOrEmpty(this.Faktura))
            {
                if (!ZmenCisloDokladu())
                    return;
            }

            try
            {
                ScannerStop();

                #region Puvodni kod overeni ...
                //string barcodeCheck = string.Empty;
                //do
                //{
                //    using (SejmiKodForm skf = new SejmiKodForm("Kontrola zboží", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, barcodeCheck))
                //    {
                //        if (skf.ShowDialog() == DialogResult.Cancel)
                //            return;
                //        barcodeCheck = skf.Kod;
                //        barcodeCheckDatetime = DateTime.Now;
                //    }
                //    if (barcode != barcodeCheck)
                //    {
                //        MySystem.Audio.PlaySound(Path.Combine(Globals.SoundDir, Globals.Configuration.ZvukNeShoda));
                //        if (Globals.Configuration.DialogNeShoda)
                //        {
                //            MessageBoxBig.Show("Kódy se neshodují", "Kontrola zboží", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                //        }
                //        continue;
                //    }
                //    else
                //    {
                //        MySystem.Audio.PlaySound(Path.Combine(Globals.SoundDir, Globals.Configuration.ZvukShoda));
                //        break;
                //    }
                //} while (true); // :) nekonecny cyklus ...

                #endregion

                using (FormHledani frmHledani = new FormHledani())
                {
                    frmHledani.BarcodeFind = barcode;

                    if (frmHledani.ShowDialog() == DialogResult.Cancel)
                        return;

                    barcodeCheckDatetime = frmHledani.BarcodeFindedDateTime;
                    barcodeCheck = frmHledani.BarcodeFinded;
                }

                // pokud se shoduji, tak to ulozim ... 

                vydejKontrolaDS.fask_Vydej_Kontrola.Addfask_Vydej_KontrolaRow(
                    this.Faktura,
                    barcode,
                    barcodeCheck,
                    barcodeDatetime,
                    barcodeCheckDatetime,
                    Globals.UserID.ToString(),
                    Globals.TermID.ToString(),
                    Guid.NewGuid()
                    );

                Dataset2SqlCommitChanges();

                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Přidej položku", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }

            // znovu zavolani tohoto okna ...
            this.BeginInvoke((ThreadStart)delegate()
            {
                PridejPolozkuSOverenim(barcode);
            });
        }

        private string _faktura = string.Empty;
        public string Faktura
        {
            get { return _faktura; }
            set
            {
                _faktura = value;
                vydejKontrolaDS.fask_Vydej_Kontrola.Clear();
                UpdateStatusBar();
            }
        }

        private void UpdateStatusBar()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("F: " + Faktura);
            sb.Append(" P: " + vydejKontrolaDS.fask_Vydej_Kontrola.Count.ToString() + (vydejKontrolaDS.HasChanges() ? "*" : string.Empty));

            statusBar1.Text = sb.ToString();
        }

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformKonec();
        }

        private void PerformKonec()
        {
            if (!Dataset2SqlCommitChangesTry())
            {
                if (DialogResult.No == MessageBoxBig.Show("Nepodařilo se uložit data!\nChcete ukončit práci a data neukládat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning))
                {
                    return;
                }
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {
            ScannerStop();
            dataGrid1.Save(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));
        }

        private bool ZmenCisloDokladu()
        {
            try
            {
                ScannerStop();

                if (!Dataset2SqlCommitChangesTry())
                    return false;

                string doklad = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm(
                    "Číslo dokladu"
                    , SejmiKodForm.TypeOfCode.AlphaNumeric
                    , 0
                    , false
                    , false
                    , doklad
                    , true
                    , false
                    , false
                    , SystemColors.Control))
                {
                    if (DialogResult.Cancel == skf.ShowDialog())
                        return false;

                    Faktura = skf.Kod;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return false;
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemFaktura_Click(object sender, EventArgs e)
        {
            ZmenCisloDokladu();
        }

        private void FormVydejKontrola_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                PerformKonec();
            else if (e.KeyCode == Keys.F1)
                ZmenCisloDokladu();
            else
                return;

            e.Handled = true;
        }

        private void menuItemDataOdeslat_Click(object sender, EventArgs e)
        {
            Dataset2SqlCommitChangesTry();
        }

    }    
}