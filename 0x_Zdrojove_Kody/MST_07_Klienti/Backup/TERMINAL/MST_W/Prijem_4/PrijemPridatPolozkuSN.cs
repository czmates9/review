using System;
using System.Drawing;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemPridatPolozkuSN : SejmiKodFormDropdown
    {
        private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow _pe = null;
        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PE
        {
            get { return _pe; }
            set { _pe = value; }
        }

        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow PI { get; set; }

        private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable _pesn_dt = null;
        /// <summary>
        /// Predloha seriovych cisel pouzitelnych pro prijem z existujicich nebo zalozeni noveho ... 
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable PESN
        {
            get { return _pesn_dt; }
            set
            {
                _pesn_dt = value;
                if (_pesn_dt == null)
                    return;

                try
                {
                    this.kod_tb.BeginUpdate();
                    this.kod_tb.Items.Clear();
                    foreach (var item in _pesn_dt)
                    {
                        if (!this.kod_tb.Items.Contains(item.SERLNMBR.Trim()))
                            this.kod_tb.Items.Add(item.SERLNMBR.Trim());
                    }
                }
                catch (Exception e)
                {
                    Logging.Log.Write(e);
                }
                finally
                {
                    this.kod_tb.EndUpdate();
                }
            }
        }

        public PrijemPridatPolozkuSN(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe)
            : base()
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._pe = pe;
        }

        public PrijemPridatPolozkuSN(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe)
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._pe = pe;
        }

        private void MyInitializeCompoment()
        {
            this.Size = Forms.FormLocation.ScreenResolution;
            //kod_tb.Location = new Point(kod_tb.Location.X, panelButtons.Location.Y - kod_tb.Height - 5);
            //popis_l.Location = new Point(popis_l.Location.X, kod_tb.Location.Y - popis_l.Height - 5);
            panelKod.Dock = DockStyle.Bottom;

            //kod je potreba nastavit na dropdown box ... zmenim radeji base sejmikod(novy pro dropdown ...)
        }

        private void PrijemPridatPolozkuSN_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            this.menuItemTisk.Enabled = MST_Global.PovolitPrintServer;
            UpdateForm();
                       
            Logging.Log.WriteDebug("Load", "PrijemPridatPolozku");
        }

        private void UpdateForm()
        {
            labelCZCarKod.Text =
            labelItemnmbr.Text =
            labelNazev.Text =
            labelQTY.Text =
            labelQTYPACK.Text =
            labelNacteno.Text =
            labelMJ.Text = "-";
            try
            {
                labelCZCarKod.Text = _pe.VNDITNUM.Trim();
                labelItemnmbr.Text = _pe.ITEMNMBR.Trim() + " (" + _pe.CZ_CarKod.Trim() + ")";
                labelNazev.Text = _pe.ITEMDESC.Trim();
                labelQTY.Text = _pe.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                //labelQTY.Text = _pe.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                labelQTYPACK.Text = _pe.QTYPACK.ToString(Settings.UIFormatDesCisel);
                //labelNacteno.Text = Prijem_3.PrijemList.Instance.Nacteno(_pe.ITEMNMBR, _pe.PONUMBER, _pe.ORD).ToString("0.00");
                labelNacteno.Text = _pe.Nasnimano.ToString(Settings.UIFormatDesCisel);
                //labelNacteno.Text = Nacteno.ToString(Settings.UIFormatDesCisel);
                labelMJ.Text = _pe.MJ.Trim();
            }
            catch
            {
            }
        }


        
        private void menuItemTiskPredloha_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                //PrijemTisk.Print(_pe, MST_Global.PrintServerTemplateNamePrijemPredloha, Convert.ToInt32(this.Kod));
                //bool vytisteno = PrijemTisk.Print(_pe, MST_Global.PrintServerTemplateNamePrijemPredloha, null);
                bool vytisteno = PrijemTisk.Print(_pe, PrinterFactory.PrinterModules.PrijemPredloha, null);
                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", _pe.CountEntries, _pe.PONUMBER, _pe.ITEMNMBR, vytisteno.ToString(), null));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void PrijemPridatPolozkuSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            e.Handled = true;

            if (e.KeyCode == Keys.F1)
            {
                menuItemTiskPredloha_Click(null, null);
            }
            else
            {
                e.Handled = false;
            }            
        }

        protected override void PerformOK()
        {
            //test zda existuje kod v seznamu predloh, pokud ne, tak se zepta
            // TODO : konfiguracne nastavit, zda se ma ptat ... ???
            if (Prijem_4.Globals.KontrolovatSNsPredlohou)
            {
				string Kod = string.IsNullOrEmpty(this.kod_tb.Text) ? string.Empty : this.kod_tb.Text.Trim();

                if (!this.kod_tb.Items.Contains(Kod))
                {
                    if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemPrijemPridatPolozkuSNKodNeniVPredlozeUlozitDotaz, this.kod_tb.Text.Trim()), Fask.Localization.Localization.Prijem4PrijemPrijemPridatPolozkuSNDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red)
                        == DialogResult.No)
                        return;
                }
            }

            base.PerformOK();
        }

    }
}