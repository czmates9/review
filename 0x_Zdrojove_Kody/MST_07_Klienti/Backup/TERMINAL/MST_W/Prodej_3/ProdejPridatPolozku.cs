using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prodej_3
{

	public enum Volajici_ProdejPridatPolozku
	{
		LOCNCODE,
		QTY,
		SERLTNUM,
		Unknow
	}

    public partial class ProdejPridatPolozku : SejmiKodForm
    {


		private Volajici_ProdejPridatPolozku _volajici = Volajici_ProdejPridatPolozku.Unknow;
		public Volajici_ProdejPridatPolozku Volajici
		{
			get { return _volajici; }
			set { _volajici = value; }
		}


        private Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row _zbozi = null;
        public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row Zbozi 
        {
            get {return _zbozi;}
            set
            {
                _zbozi = value;
                UpdateForm();
            }
        }
        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row _odberatel = null;
        public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row Odberatel
        {
            get {return _odberatel;}
            set
            {
                _odberatel = value;
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

        public ProdejPridatPolozku(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel, Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._odberatel = odberatel;
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
        /// <param name="odberatel">odberatel polozky</param>
        /// <param name="zbozi">vydavana polozka</param>
        public ProdejPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel
            , Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi 
        )
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._odberatel = odberatel;
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
        /// <param name="odberatel">odberatel polozky</param>
        /// <param name="zbozi">vydavana polozka</param>
        public ProdejPridatPolozku(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel
            , Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, bool povolitScanner
        )
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni, povolitScanner)
        {
            InitializeComponent();
            MyInitializeCompoment();
            this._odberatel = odberatel;
            this._zbozi = zbozi;
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

            this.menuItem2.Enabled = Prodej.Globals.OnlinePocetKusu;
            this.menuItem3.Enabled = Prodej.Globals.OnlinePocetKusuSklad;
            this.menuItem4.Enabled = Prodej.Globals.OnlineDetailPolozka;

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
            labelLOCNCODE.Text = "-";

            try
            {
                labelCZCarKod.Text = _zbozi.IsVNDITNUMNull() ? string.Empty : (_zbozi.VNDITNUM.Trim() + " (" + (_zbozi.IsCZ_CarKodNull() ? "-" :  _zbozi.CZ_CarKod.Trim()) + ")");
                labelItemnmbr.Text = (_zbozi.IsITEMCODENull()? "-" : _zbozi.ITEMCODE.Trim()) + " (" + _zbozi.ITEMNMBR.Trim() + ")";
                labelNazev.Text = _zbozi.IsITEMDESCNull() ? "-" : _zbozi.ITEMDESC.Trim();
                try { labelPRICE.Text = ((decimal)_zbozi["PRICE" + _odberatel.odb_typ.Trim()]).ToString(Settings.UIFormatDesCisel); }
                catch { labelPRICE.Text = "-"; }
                labelQTY.Text = _zbozi.QTY.ToString(Settings.UIFormatDesCisel);
                labelQTYPACK.Text = _zbozi.QTYPACK.ToString(Settings.UIFormatDesCisel);
                labelSNTrack.Text = _zbozi.CZ_SerNum_Track > 0 ? "Ano" : "Ne";
                labelTAXRATE.Text = _zbozi.IsTAXRATENull() ? "-" : _zbozi.TAXRATE.ToString(Settings.UIFormatDesCisel) + " %";
                labelMJ.Text = _zbozi.MJ.Trim();
                //labelSerltnum.Text = _serltnum.Trim();  // Ta.D. 20170822 pridano vypisovani serioveho cisla
                labelSerltnum.Text = String.IsNullOrEmpty(_serltnum) ? "-" : _serltnum.Trim();
                labelWEIGHT.Text = _zbozi.IsWEIGHTNull() ? "-" : _zbozi.WEIGHT.ToString(Settings.UIFormatDesCisel);
                labelLOCNCODE.Text = _zbozi.IsLOCNCODENull() ? "-" : _zbozi.LOCNCODE.Trim();

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
                string item = _zbozi.ITEMNMBR.Trim();
                string itemdesc = _zbozi.ITEMDESC.Trim();
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
                string item = _zbozi.ITEMNMBR.Trim();
                string lokace = _zbozi.LOCNCODE.Trim();
                string itemdesc = _zbozi.ITEMDESC.Trim();
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
                if (_zbozi == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejPridatPolozkuNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                string item = _zbozi.ITEMNMBR.Trim();
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
            if ((this.Zbozi != null) && (code is Parsing.Codes.Interfaces.ICodeWeight) && (code is Parsing.Codes.Interfaces.ICodeItemnmbr))
            {
                if (this.Zbozi.ITEMNMBR.Trim() != (((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr ?? string.Empty))
                {
                    MessageBoxBig.Show("Není stejné zboží.");
                    return false;
                }

                // TODO : jak spravne nastavit mnozstvi?
                this.Kod =
                    (((Parsing.Codes.Interfaces.ICodeWeight)code).Weight ?? 0
                    / (this.Zbozi.IsWEIGHTNull() || (this.Zbozi.WEIGHT == 0) ? 1 : this.Zbozi.WEIGHT)
                    / (this.Zbozi.IsQTYPACKNull() || (this.Zbozi.QTYPACK == 0) ? 1 : this.Zbozi.QTYPACK)
                    ).ToString(Settings.UIFormatDesCisel);

                return true;
            }

            //Fask.Parsing.Codes.BarcodeSlashSarze bsscode = Parsing.ParsingFactory.Parse(this.Kod) as Fask.Parsing.Codes.BarcodeSlashSarze;

			#region TaD 16.10.2020 uprava logiky, tohle je nevyhovujici, když GS1 kod neobsahuje EAN ale pouze šaržu

			//if ((this.Zbozi != null) && (code is Parsing.Codes.Interfaces.ICodeBarcode) && (code is Parsing.Codes.Interfaces.ICodeSerltnmbr))
			//{
			//    string barcode = ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode ?? string.Empty;
			//    if ((this.Zbozi.VNDITNUM != barcode) || (this.Zbozi.CZ_CarKod != barcode))
			//    {
			//        MessageBoxBig.Show("Není stejné zboží.");
			//        return false;
			//    }

			//    this.Kod = ((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr ?? string.Empty;
			//    return true;
			//}

			#endregion	
			
			#region TaD 27.10.2020 Taky nevyhovujici
			
			//if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSerltnmbr))
			//{
			//    this.Kod = ((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr ?? string.Empty;
			//    return true;
			//}

			//if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeQuantity))
			//{
			//    this.Kod = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue ? string.Empty : ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Settings.UIFormatDesCisel);
			//    return true;
			//} 

			#endregion

			#region TaD Novy kod


			switch (this._volajici)
			{
				case Volajici_ProdejPridatPolozku.LOCNCODE:
					return base.isRightCode();
				case Volajici_ProdejPridatPolozku.QTY:
					if (!KodIsQTY(code))
						return false;
					break;
				case Volajici_ProdejPridatPolozku.SERLTNUM:
					if (!KodIsSERLTNUM(code))
						return false;
					break;
				case Volajici_ProdejPridatPolozku.Unknow:
				default:
					return base.isRightCode();
			}
			
			#endregion

            return base.isRightCode();
        }

		private bool KodIsQTY(Fask.Parsing.Codes.BaseCode code)
		{
			try
			{
				if (!Fask.MST_W.Prodej.Globals.PovolitParsovaniMnozstvi)
					return true;

				if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeQuantity))
				{
					string QTYtmp = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue ? string.Empty : ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Settings.UIFormatDesCisel);

					if (string.IsNullOrEmpty(QTYtmp))
					{
						return true;
					}

					if ((code is Parsing.Codes.Interfaces.IPocetNasnimanychVariant))
					{
						int pocet = ((Parsing.Codes.Interfaces.IPocetNasnimanychVariant)code).PocetNasnimanychVariant;

						if (pocet > 1)
						{
							this.Kod = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Settings.UIFormatDesCisel);
							return true;
						}
					}

					string msg = string.Format("Nalezen parsovaný kód s výsledkem: '{0}'" + 
						Environment.NewLine + 
						"Pùvodní kód je: '{1}'" +
						Environment.NewLine + 
						"Pøejete si použít parsovaný kód? ",
						QTYtmp,
						this.Kod
						);

					DialogResult dr = MessageBoxBig.Show(msg, "Dotaz", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1);

					if (dr == DialogResult.Yes)
					{
						this.Kod = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue ? string.Empty : ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Settings.UIFormatDesCisel);
						return true;
					}
					else if ((dr == DialogResult.Cancel))
					{
						return false;
					}
					else return true;
				}
				else
				{
					//Nic se nedeje
					return true;
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}

		private bool KodIsSERLTNUM(Fask.Parsing.Codes.BaseCode code)
		{
			try
			{
				if (!Fask.MST_W.Prodej.Globals.PovolitParsovaniSarze)
					return true;


				if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSarze))
				{
					if (string.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
					{
						return true;
					}

					if ((code is Parsing.Codes.Interfaces.IPocetNasnimanychVariant))
					{
						int pocet = ((Parsing.Codes.Interfaces.IPocetNasnimanychVariant)code).PocetNasnimanychVariant;

						if (pocet > 1)
						{
							this.Kod = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
							return true;
						}
					}

					string msg = string.Format("Nalezen parsovaný kód s výsledkem: '{0}'" +
						Environment.NewLine +
						"Pùvodní kód je: '{1}'" +
						Environment.NewLine +
						"Pøejete si použít parsovaný kód? ",
						((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze,
						this.Kod
						);

					DialogResult dr = MessageBoxBig.Show(msg, "Dotaz", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1);

					if (dr == DialogResult.Yes)
					{
						this.Kod = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
						return true;
					}
					else if ((dr == DialogResult.Cancel))
					{
						return false;
					}
					else return true;
				}
				else if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSerialNumber))
				{
					if (string.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN))
					{
						return true;
					}

					if ((code is Parsing.Codes.Interfaces.IPocetNasnimanychVariant))
					{
						int pocet = ((Parsing.Codes.Interfaces.IPocetNasnimanychVariant)code).PocetNasnimanychVariant;

						if (pocet > 1)
						{
							this.Kod = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;
							return true;
						}
					}

					string msg = string.Format("Nalezen parsovaný kód s výsledkem: '{0}'" +
						Environment.NewLine +
						"Pùvodní kód je: '{1}'" +
						Environment.NewLine +
						"Pøejete si použít parsovaný kód? ",
						((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN,
						this.Kod
						);

					DialogResult dr = MessageBoxBig.Show(msg, "Dotaz", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1);

					if (dr == DialogResult.Yes)
					{
						this.Kod = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;
						return true;
					}
					else if ((dr == DialogResult.Cancel))
					{
						return false;
					}
					else return true;
				}
				else
				{
					//Nic se nedeje
					return true;
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}
    }
}