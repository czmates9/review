using System;
using System.ComponentModel;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemPridatPolozku : SejmiKodForm
    {
        private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow _pe = null;
        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PE
        {
            get { return _pe; }
            set { _pe = value; }
        }

        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow _pi { get; set; }

        private string _serltnum = string.Empty;
        public string Serltnum
        {
            get
            {
                return _serltnum;
            }
            set
            {
                _serltnum = value;
            }
        }

        public PrijemPridatPolozku(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe)
            : base()
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._pe = pe;
        }

        public PrijemPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
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
        }

        private void PrijemPridatPolozku_Load(object sender, EventArgs e)
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
            labelMJ.Text = 
            labelWEIGHT.Text = 
            labelSerltnum.Text = "-";
            
            try
            {
                labelCZCarKod.Text = (_pe.IsVNDITNUMNull() ? string.Empty : _pe.VNDITNUM.Trim() ) + " (" + _pe.CZ_CarKod.Trim() + ")";
                labelItemnmbr.Text = (_pe.IsITEMCODENull() ? "-" : _pe.ITEMCODE.Trim()) + " (" + _pe.ITEMNMBR.Trim() + ")";
                labelWEIGHT.Text = _pe.IsWEIGHTNull() ? "-" : _pe.WEIGHT.ToString(Settings.UIFormatDesCisel);
                //labelCZCarKod.Text = _pe.VNDITNUM.Trim();
                //labelItemnmbr.Text = _pe.ITEMNMBR.Trim() + " (" + _pe.CZ_CarKod.Trim() + ")";
                labelNazev.Text = _pe.ITEMDESC.Trim();
                labelQTY.Text = _pe.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                labelQTYPACK.Text = _pe.QTYPACK.ToString(Settings.UIFormatDesCisel);
                //labelNacteno.Text = Prijem_3.PrijemList.Instance.Nacteno(_pe.ITEMNMBR, _pe.PONUMBER, _pe.ORD).ToString("0.00");
                labelNacteno.Text = _pe.Nasnimano.ToString(Settings.UIFormatDesCisel);
                //labelNacteno.Text = Nacteno.ToString(Settings.UIFormatDesCisel);
                labelMJ.Text = _pe.MJ.Trim();
                labelSerltnum.Text = _serltnum.Trim();
            }
            catch
            {
            }
        }


        
        private void menuItemTiskPredloha_Click(object sender, EventArgs e)
        {
            if (!MST_Global.PovolitPrintServer)
                return;

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

        private void PrijemPridatPolozku_KeyDown(object sender, KeyEventArgs e)
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

        protected override bool isRightCode()
        {
            //Fask.Parsing.Codes.WeightCode wcode = Parsing.ParsingFactory.Parse(this.Kod) as Fask.Parsing.Codes.WeightCode;
            var code = Parsing.ParsingFactory.Parse(this.Kod, Settings.Parsing_Config);
            if ((this._pe != null) && (code is Parsing.Codes.Interfaces.ICodeWeight) && (code is Parsing.Codes.Interfaces.ICodeItemnmbr))
            {
                if (this._pe.ITEMNMBR.Trim() != (((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr ?? string.Empty))
                {
                    MessageBox.Show("Není stejné zboží.");
                    return false;
                }

                this.Kod =
                    (((Parsing.Codes.Interfaces.ICodeWeight)code).Weight ?? 0
                    / (this._pe.IsWEIGHTNull() || (this._pe.WEIGHT == 0) ? 1 : this._pe.WEIGHT)
                    / ((this._pe.QTYPACK == 0) ? 1 : this._pe.QTYPACK)
                    ).ToString(Settings.UIFormatDesCisel);

                return true;
            }

            return base.isRightCode();
        }

        private void PrijemPridatPolozku_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}