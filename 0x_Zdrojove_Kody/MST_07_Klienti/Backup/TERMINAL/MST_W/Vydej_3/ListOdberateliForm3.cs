using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using System.Linq;

namespace Fask.MST_W.Vydej_3
{
    /// <summary>
    /// Formular pro listovani seznamem odberatelu
    /// </summary>
    public partial class ListOdberateliForm3 : System.Windows.Forms.Form
    {
        public ListOdberateliForm3()
        {
            InitializeComponent();
            this.KeyPreview = true;
            ciselnik = new Fask.SQLiteDBs.DataSets.Odberatele();
        }

        private int odberatelIndex = 0;

        private string chosenID = null;
        public string ChosenID
        {
            get
            {
                return chosenID;
            }
        }

        private Fask.SQLiteDBs.DataSets.Odberatele ciselnik;
        public Fask.SQLiteDBs.DataSets.Odberatele Ciselnik
        {
            set
            {
                ciselnik = value;                
            }
        }

        void ScannerMethod(string barcode)
        {
            try
            {
                string kod = barcode.Trim();
                // TODO : dohledavat odberatele podle odb_carcode, alternativne podle odb_id dle konfigurace terminalu...
                //SqlCEDBs.DataSets.Odberatele.CZMST090Row[] odberateleFound =
                //    (Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row[])(ciselnik.CZMST090.Select("odb_carcode='" + kod + "' or odb_id='" + kod + "'", null, DataViewRowState.CurrentRows));
                var odberatleFound = ciselnik.CZMST090.Where(x => x.odb_carcode == kod || x.odb_id == kod);
                //if (odberateleFound.Length == 0)
                if (odberatleFound.Count() == 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListOdberateliForm3OdberatelNenalezen, Fask.Localization.Localization.Vydej3ListOdberateliForm3Informace, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                //chosenID = odberateleFound[0].odb_id.Trim();
                chosenID = odberatleFound.First().odb_id.Trim();
                PerformOK();
            }
            catch { }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            string kod = e.BarcodeData.Trim();
            if (kod.Length > 0)
                //this.BeginInvoke(new ScannerMethodDelegate(ScannerMethod), new object[] { kod });
                this.BeginInvoke((Action)delegate()
                {
                    ScannerMethod(kod);
                });
        }

        private void updateForm()
        {
            //if (ciselnik.CZMST090.Rows.Count == 0)
            if (ciselnik.CZMST090.Count == 0)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListOdberateliForm3CiselnikOdberateluJePrazdny, Fask.Localization.Localization.Vydej3ListOdberateliForm3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                PerformCancel();
                return;
            }
            chosenID = ciselnik.CZMST090[odberatelIndex].odb_id;
            this.ID_l.Text = chosenID;
            this.popis_l.Text = ciselnik.CZMST090[odberatelIndex].odb_desc.Trim();
            this.lbl_CarKod.Text = ciselnik.CZMST090[odberatelIndex].odb_carcode.Trim();
        }

        private void ListOdberateliForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Rocker Up
                // Up
                odberatelIndex = 0;
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Rocker Down
                // Down
                odberatelIndex = ciselnik.CZMST090.Rows.Count - 1;
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
                if (odberatelIndex > 0)
                    odberatelIndex--;
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
                if (odberatelIndex < ciselnik.CZMST090.Rows.Count - 1)
                    odberatelIndex++;
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                PerformOK();
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
                return;
            }
            else
            {
                e.Handled = false;
                return;
            }

            updateForm();
        }

        private void ListOdberateliForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            updateForm();

            ScannerStart();
        }

        private void ListOdberateliForm_Closing(object sender, CancelEventArgs e)
        {
            ScannerFinalize();
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

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

        void PerformOK()
        {
            ScannerFinalize();
            DialogResult = DialogResult.OK;
        }

        void PerformCancel()
        {
            ScannerFinalize();
            DialogResult = DialogResult.Cancel;
        }

    }
}