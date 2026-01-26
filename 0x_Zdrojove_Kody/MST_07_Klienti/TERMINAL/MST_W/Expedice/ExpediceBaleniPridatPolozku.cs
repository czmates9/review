using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Expedice
{
    public partial class ExpediceBaleniPridatPolozku : SejmiKodForm
    {
        private Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow _polozka = null;
        public Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow Polozka
        {
            get {return _polozka;}
            set
            {
                _polozka = value;
                UpdateForm();
            }
        }

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

        /// <summary>
        /// Dialog pro pridani polozky
        /// </summary>
        /// <param name="popis">Popis nad zadavacim polem</param>
        /// <param name="typeOfCode">Typ vkladaneho kodu</param>
        /// <param name="len">Pozadovana delka kodu</param>
        /// <param name="checkLen">Kontrolovat delku kodu</param>
        /// <param name="allowEmpty">Povolit prazdny vstup</param>
        /// <param name="retezecKPredvyplneni">Predvyplneny retezec v zadavacim poli</param>
        /// <param name="odberatel">odberatel polozky</param>
        /// <param name="zbozi">vydavana polozka</param>
        public ExpediceBaleniPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow polozka
        )
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._polozka = polozka;
        }

        /// <summary>
        /// Dialog pro pridani polozky
        /// </summary>
        /// <param name="popis">Popis nad zadavacim polem</param>
        /// <param name="typeOfCode">Typ vkladaneho kodu</param>
        /// <param name="len">Pozadovana delka kodu</param>
        /// <param name="checkLen">Kontrolovat delku kodu</param>
        /// <param name="allowEmpty">Povolit prazdny vstup</param>
        /// <param name="retezecKPredvyplneni">Predvyplneny retezec v zadavacim poli</param>
        /// <param name="odberatel">odberatel polozky</param>
        /// <param name="zbozi">vydavana polozka</param>
        public ExpediceBaleniPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow polozka, bool povolitScanner
        )
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni, povolitScanner)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._polozka = polozka;
        }

        private void MyInitializeCompoment()
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;



            //kod_tb.Location = new Point(kod_tb.Location.X, panelButtons.Location.Y - kod_tb.Height - 5);
            //popis_l.Location = new Point(popis_l.Location.X, kod_tb.Location.Y - popis_l.Height - 5);
            panelKod.Dock = DockStyle.Bottom;
        }

        private void ProdejPridatPolozku_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.Size = Forms.FormLocation.ScreenResolution;

            UpdateForm();
        }

        private void UpdateForm()
        {
            labelCZCarKod.Text = "-";
            labelItemnmbr.Text = "-";
            labelNazev.Text = "-";
            labelPRICE.Text = "-";
            labelQTY.Text = "-";
            labelQTYPACK.Text = "-";
            labelSNTrack.Text = "-";
            labelTAXRATE.Text = "-";
            labelMJ.Text = "-";
            labelSerltnum.Text = "-";
            labelWEIGHT.Text = "-";

            try
            {
                labelCZCarKod.Text = _polozka.IsVNDITNUMNull() ? string.Empty : (_polozka.VNDITNUM.Trim() + " (" + (_polozka.IsCZ_CarKodNull() ? "-" :  _polozka.CZ_CarKod.Trim()) + ")");
                //labelItemnmbr.Text = (_polozka.IsITEMCODENull()? "-" : _polozka.ITEMCODE.Trim()) + " (" + _polozka.ITEMNMBR.Trim() + ")";
                labelItemnmbr.Text = _polozka.ITEMNMBR.Trim();
                labelNazev.Text = _polozka.IsITEMDESCNull() ? "-" : _polozka.ITEMDESC.Trim();
                labelPRICE.Text = "-";
                labelQTY.Text = _polozka.QTY.ToString(Settings.UIFormatDesCisel);
                labelQTYPACK.Text = _polozka.QTYPACK.ToString(Settings.UIFormatDesCisel);
                labelSNTrack.Text = _polozka.CZ_SerNum_Track > 0 ? "Ano" : "Ne";
                //labelTAXRATE.Text = _polozka.IsTAXRATENull() ? "-" : _polozka.TAXRATE.ToString(Settings.UIFormatDesCisel) + " %";
                labelTAXRATE.Text = "-";
                labelMJ.Text = _polozka.MJ.Trim();
                labelSerltnum.Text = _serltnum.Trim();
                labelWEIGHT.Text = _polozka.IsWEIGHTNull() ? "-" : _polozka.WEIGHT.ToString(Settings.UIFormatDesCisel);
                kod_tb.Focus();
            }
            catch
            {
                kod_tb.Focus();
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            DetailPocetKusu();
        }


        private void DetailPocetKusu()
        {
            try
            {
                this.ScannerStop();
                string item = _polozka.ITEMNMBR.Trim();
                string itemdesc = _polozka.ITEMDESC.Trim();
                using (Informations.OnLinePocetKusuSklad pkusu = new Fask.MST_W.Informations.OnLinePocetKusuSklad(item, itemdesc))
                {
                    pkusu.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void DetailPocetKusuLokace()
        {
            try
            {
                this.ScannerStop();
                string item = _polozka.ITEMNMBR.Trim();
                string lokace = _polozka.LOCNCODE.Trim();
                string itemdesc = _polozka.ITEMDESC.Trim();
                using (Informations.OnLinePocetKusuSklad pkusu = new Fask.MST_W.Informations.OnLinePocetKusuSklad(item, lokace, itemdesc))
                {
                    pkusu.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void DetailItemnumber()
        {
            try
            {
                this.ScannerStop();
                if (_polozka == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejPridatPolozkuNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                string item = _polozka.ITEMNMBR.Trim();
                string dokl = string.Empty;
                using (Informations.OnLineItemumberGrid detailpolozka = new Fask.MST_W.Informations.OnLineItemumberGrid(item, dokl))
                {
                    detailpolozka.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            DetailPocetKusuLokace();
        }

        private void menuItem4_Click_1(object sender, EventArgs e)
        {
            DetailItemnumber();
        }

        private void ProdejPridatPolozku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (Prodej.Globals.OnlinePocetKusu)
                    DetailPocetKusu();
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (Prodej.Globals.OnlinePocetKusuSklad)
                    DetailPocetKusuLokace();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        protected override bool isRightCode()
        {
            // TODO : dalsi kontroly ??? + TEST !!!
            //Fask.Parsing.Codes.WeightCode wcode = Parsing.ParsingFactory.Parse(this.Kod) as Fask.Parsing.Codes.WeightCode;
            var code = Parsing.ParsingFactory.Parse(this.Kod, Settings.Parsing_Config);
            if ((this.Polozka != null) && (code is Parsing.Codes.Interfaces.ICodeWeight) && (code is Parsing.Codes.Interfaces.ICodeItemnmbr))
            {
                if (this.Polozka.ITEMNMBR.Trim() != (((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr ?? string.Empty))
                {
                    MessageBox.Show("Není stejná položka.");
                    return false;
                }

                // TODO : jak spravne nastavit mnozstvi?
                this.Kod =
                    (((Parsing.Codes.Interfaces.ICodeWeight)code).Weight ?? 0
                    / (this.Polozka.IsWEIGHTNull() || (this.Polozka.WEIGHT == 0) ? 1 : this.Polozka.WEIGHT)
                    / (this.Polozka.IsQTYPACKNull() || (this.Polozka.QTYPACK == 0) ? 1 : this.Polozka.QTYPACK)
                    ).ToString(Settings.UIFormatDesCisel);

                return true;
            }

            return base.isRightCode();
        }

    }
}