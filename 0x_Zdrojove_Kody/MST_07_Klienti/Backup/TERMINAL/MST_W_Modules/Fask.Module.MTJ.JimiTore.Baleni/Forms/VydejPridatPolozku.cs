using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Module.MTJ.JimiTore.Baleni.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni.Forms
{
    public partial class VydejPridatPolozku : SejmiKodForm
    {
        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ZboziRow _zbozi = null;
        public Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ZboziRow Zbozi
        {
            get { return _zbozi; }
            set
            {
                _zbozi = value;
                UpdateForm();
            }
        }


        //public VydejPridatPolozku(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel, Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi)
        //{
        //    InitializeComponent();
        //    MyInitializeCompoment();
        //    this._odberatel = odberatel;
        //    this._zbozi = zbozi;
        //}

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
        //public VydejPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
        //    , Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel
        //    , Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi 
        //)
        //    : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        //{
        //    InitializeComponent();
        //    MyInitializeCompoment();
        //    this._odberatel = odberatel;
        //    this._zbozi = zbozi;
        //}

        /// <summary>
        /// Dialog pro pridani polozky
        /// </summary>
        /// <param name="popis">Popis nad zadavacim polem</param>
        /// <param name="typeOfCode">Typ vkladaneho kodu</param>
        /// <param name="len">Pozadovana delka kodu</param>
        /// <param name="checkLen">Kontrolovat delku kodu</param>
        /// <param name="allowEmpty">Povolit prazdny vstup</param>
        /// <param name="retezecKPredvyplneni">Predvyplneny retezec v zadavacim poli</param>
        /// <param name="zbozi">vydavana polozka</param>
        public VydejPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ZboziRow zbozi, bool povolitScanner
        )
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni, povolitScanner, false, false, null)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._zbozi = zbozi;
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
        /// <param name="zbozi">vydavana polozka</param>
        /// <param name="celaCisla">Urcuje se, zdali musi byt zadana cela cisla</param>
        public VydejPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ZboziRow zbozi, bool povolitScanner, bool celaCisla, bool povolitZapornaCisla, Color? buttonColor
        )
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni, povolitScanner, celaCisla, povolitZapornaCisla, buttonColor)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._zbozi = zbozi;
        }

        private void MyInitializeCompoment()
        {
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;


            //kod_tb.Location = new Point(kod_tb.Location.X, panelButtons.Location.Y - kod_tb.Height - 5);
            //popis_l.Location = new Point(popis_l.Location.X, kod_tb.Location.Y - popis_l.Height - 5);
            panelKod.Dock = DockStyle.Bottom;
        }

        private void ProdejPridatPolozku_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            //this.menuItem2.Enabled = Prodej.Globals.OnlinePocetKusu;
            //this.menuItem3.Enabled = Prodej.Globals.OnlinePocetKusuSklad;
            //this.menuItem4.Enabled = Prodej.Globals.OnlineDetailPolozka;

            UpdateForm();
        }

        private void UpdateForm()
        {
            labelCZCarKod.Text = "-";
            labelItemnmbr.Text = "-";
            labelNazev.Text = "-";
            labelQTY.Text = "-";
            labelMJ.Text = "-";
            labelMJ.Text = "-";
            labelDelka.Text = "-";

            try
            {
                //Settings.UIFormatDesCisel
                labelCZCarKod.Text = _zbozi.IsCZ_CarKodNull() ? "-" : _zbozi.CZ_CarKod.Trim();
                labelNazev.Text = _zbozi.IsnazevNull() ? "-" : _zbozi.nazev.Trim();
                labelItemnmbr.Text = _zbozi.matid.Trim();
                labelQTY.Text = _zbozi.IsmnozstviNull() ? "-" : _zbozi.mnozstvi.ToString("N");
                labelMJ.Text = _zbozi.IsmjNull() ? "-" : _zbozi.mj.Trim();
                labelDelka.Text = _zbozi.IsdelkaNull() ? "-" : _zbozi.delka.ToString();

                kod_tb.Focus();
            }
            catch
            {
                kod_tb.Focus();
            }
        }

        private void ProdejPridatPolozku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                //if (Prodej.Globals.OnlinePocetKusu)
                //    DetailPocetKusu();
            }
            else if (e.KeyCode == Keys.F10)
            {
                //if (Prodej.Globals.OnlinePocetKusuSklad)
                //    DetailPocetKusuLokace();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

    }
}