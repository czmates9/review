using System;
using System.Windows.Forms;
using Fask.ScannerProvider;

namespace Fask.MST_W.Vydej_3
{
    public partial class PolozkaNasnimDetail : System.Windows.Forms.Form
    {
		private Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow chosenRow = null;

        #region Constructors
        private PolozkaNasnimDetail()
        {
            InitializeComponent();
        }

        public PolozkaNasnimDetail(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow sirow)
            : this()
        {
            this.chosenRow = sirow;
        }
        #endregion

        #region Functions
        private void updateForm()
        {
            try
            {
                ItemNmbr_l.Data = chosenRow.ITEMNMBR + " (" + chosenRow.CZ_CarKod.Trim() + ")";
                try
                {
                    ItemDesc_l.Data = chosenRow["ITEMDESC"].ToString().Trim();
                }
                catch { }
				CZ_CarKod_l.Data = chosenRow.IsVNDITNUMNull() ? string.Empty : chosenRow.VNDITNUM;
                baleni_l.Data = chosenRow.QTYPACK.ToString(Settings.UIFormatDesCisel);
                //nacist_l.Data = chosenRow.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                nacteno_l.Data = chosenRow.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                //decimal nactenokusu = Vydej_3.ListPolozek3.Instance.Nacteno(chosenRow.ITEMNMBR, chosenRow.SOPNUMBE, chosenRow.ORD);
                //nacteno_l.Data = nactenokusu.ToString(Settings.UIFormatDesCisel);
                //nacteno_l.ForeColor = (nactenokusu == chosenRow.QTYSHPPD) ? Color.Green : SystemColors.ControlText;

                //if (chosenRow.CZ_SerNum_Track == 0)
                //    snimatSN_l.Data = "NE";
                //else if (chosenRow.CZ_SerNum_Track == 1)
                //    snimatSN_l.Data = "ANO";
                //else
                //    snimatSN_l.Data = "Šarže";
                ////snimatSW_l.Text = "Snímat " + Program.vydejForm.SWCode + ": " +
                //snimatSW_l.Data = (chosenRow.CZ_SW_Track == 1 ? "ANO" : "NE");
                ////snimatDV_l.Text = "Snímat " + Program.vydejForm.DVCode + ": " +
                //snimatDV_l.Data = (chosenRow.CZ_DatVyr_Track == 1 ? "ANO" : "NE");

                //sn_l.Data = chosenRow.CountEntries.ToString();
                sn_l.Data = chosenRow.SERLTNUM.Trim();
                sklad_l.Data = chosenRow.LOCNCODE;
				lokace_l.Data = chosenRow.IsVNDITNUMNull() ? string.Empty : chosenRow.VNDITNUM.Trim(); //VNDITNUM je tam schvalne !!!
            }
            catch
            {
                ItemNmbr_l.Data = "-";
                ItemDesc_l.Data = "-";
                CZ_CarKod_l.Data = "-";
                baleni_l.Data = "-";
                nacteno_l.Data = "-";
                sn_l.Data = "-";
                sklad_l.Data = "-";
                lokace_l.Data = "-";
            }
        }
        #endregion

        #region Events

        private void PolozkaNasnimDetail_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                //ok_but.Visible = MST_Global.ShowPanelButtons;
                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                updateForm();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, this.GetType().ToString());
            }

        }

        #endregion

        void PerformOK()
        {
            ScannerFinalize();
            this.DialogResult = DialogResult.OK;
        }

        private void PolozkaNasnimDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void hlavniMenu_but_Click_1(object sender, EventArgs e)
        {
            PerformOK();
        }

        #region Scanner start stop
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

        delegate void ScannerEventHandlerCall(string e);

        private void UpdateUI(ScannerEventArgs e)
        {
            //string ck = e.BarcodeData.Trim();

            //if (ck.Length > 0)
            //{
            //    this.TXT_zadane_SN.Text = ck;
            //}
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            //if (e.BarcodeData.Trim().Length > 0)
            //    this.BeginInvoke(new ScannerEventHandlerCall(dohledejPolozku), new object[] { e.BarcodeData });

            //if (IsNumber(TXT_pocet_SN2.Text))
            //    BTN_generate_zazkaznicke_SN_Click(sender, e);
        }
        #endregion

    }

}