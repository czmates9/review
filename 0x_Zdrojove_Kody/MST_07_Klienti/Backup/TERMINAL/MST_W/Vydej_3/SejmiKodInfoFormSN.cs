using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class SejmiKodInfoFormSN : SejmiKodFormDropdown
    {

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow = null;
		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow viRow = null;

        //Promenna pro nazev sloupce, jehoz hodnota se ma brat
        public string returnValueColumnName = string.Empty;

        //Hodnoty nastaveni od aktualniho modulu
        //Odpovida konfiguraci, toto vychozi hodnoty - dalsi moznosti jsou uvedene sloupce samostatne
        public string vyhledavatZboziDleSloupce = "CZ_CARKOD,VNDITNUM";
        public bool vybiratZboziJenScannerem = false;

        private Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNDataTable _sesn_dt = null;
        /// <summary>
        /// Predloha seriovych cisel pouzitelnych pro prijem z existujicich nebo zalozeni noveho ... 
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNDataTable SESN
        {
            get { return _sesn_dt; }
            set
            {
                _sesn_dt = value;
                if (_sesn_dt == null)
                    return;

                try
                {
                    this.kod_tb.BeginUpdate();
                    this.kod_tb.Items.Clear();
                    foreach (var item in _sesn_dt)
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

        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow SESNSelected
        {
            get;
            set;
        }

        //public SejmiKodInfoForm()
        //    : this()
        //{
        //    InitializeComponent();
        //    MyInitializeComponent();
        //}

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="veRow">Radek tabulky, ktery se zobrazi</param>
        public SejmiKodInfoFormSN(string popis, TypeOfCode typeOfCode, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow)
            : this(popis, typeOfCode, 0, false, false, veRow)
        {
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        /// <param name="veRow">Radek tabulky, ktery se zobrazi</param>
        public SejmiKodInfoFormSN(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow)
            : this(popis, typeOfCode, len, checkLen, allowEmpty, "", veRow, null)
        {
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        /// <param name="retezecKPredvyplneni">Retezec, ktery se predvyplni do policka 'kod'</param>
        /// <param name="veRow">Radek tabulky, ktery se zobrazi</param>
        public SejmiKodInfoFormSN(
            string popis,
            TypeOfCode typeOfCode,
            decimal len,
            bool checkLen,
            bool allowEmpty,
            string retezecKPredvyplneni,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow viRow)
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            InitializeComponent();
            this.veRow = veRow;
            this.viRow = viRow;
            MyInitializeComponent();
            this.kod_tb.Text = retezecKPredvyplneni;
            this.kod_tb.SelectAll();
        }

        private void MyInitializeComponent()
        {
            try
            {
                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;
                //Vydej.Vydej.CZMST_SEDataTable[0].
                //popis_l.Location = new Point(3, 169);
                //kod_tb.Location = new Point(3, 192);
                panelKod.Dock = DockStyle.Bottom;
                UpdateMyForm();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                //this.kod_tb.SelectedIndexChanged += new EventHandler(kod_tb_SelectedIndexChanged);
                this.kod_tb.TextChanged -= new EventHandler(kod_tb_TextChanged);
                this.kod_tb.TextChanged += new EventHandler(kod_tb_TextChanged);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

        }

        private void UpdateMyForm()
        {
            if (veRow != null)
            {
                ItemNmbr_l.Text = veRow.ITEMNMBR.Trim() + " (" + veRow.CZ_CarKod.Trim() + ")";
                ItemDesc_l.Text = veRow.ITEMDESC.Trim();
				CZ_CarKod_l.Text = veRow.IsVNDITNUMNull() ? string.Empty : veRow.VNDITNUM.Trim();
                baleni_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoFormSNBaleni + " " + veRow.QTYPACK.ToString(Settings.UIFormatDesCisel);
                baleni_l.Visible = (veRow.QTYPACK > 0);
                nacist_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoFormSNNacist + " " + veRow.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                nacteno_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoFormSNNacteno + " " + Fask.MST_W.Vydej_3.ListPolozek3.Instance.Nacteno(veRow.ITEMNMBR, veRow.SOPNUMBE, veRow.ORD).ToString(Settings.UIFormatDesCisel);
            }
            if (viRow != null)
            {
                string serltnum = viRow.SERLTNUM.Trim();
                SERLTNUM_l.Visible = serltnum.Length > 0;
                SERLTNUM_l.Text = MST_Global.SNName + ": ";
                if (veRow != null && veRow.CZ_SerNum_Track == 2)
                    SERLTNUM_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoFormSNSarze + " ";
                SERLTNUM_l.Text += serltnum;
            }
            else
            {
                SERLTNUM_l.Visible = false;
            }

            if (SESNSelected != null)
            {
                this.SERLTNUM_l.Text = SESNSelected.SERLNMBR.Trim();
                this.nacist_l.Text += "(" + SESNSelected.QTY.ToString(Settings.UIFormatDesCisel) + ")";
                if (SESNSelected.IsNactenoNull())
                    this.nacteno_l.Text += "(0)";
                else
                    this.nacteno_l.Text += "(" + SESNSelected.Nacteno.ToString(Settings.UIFormatDesCisel) + ")";
            }
        }

        //void kod_tb_SelectedIndexChanged(object sender, EventArgs e)
        void kod_tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SERLTNUM_l.Visible = true;

                if (_sesn_dt != null)
                {

                    Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow[] sesnrows = (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow[])_sesn_dt.Select("SERLNMBR='" + this.kod_tb.Text.Trim() + "'");
                    if (sesnrows.Length > 0)
                    {
                        SESNSelected = sesnrows[0]; //proste prvni nalezeny ... vzdy by mel byt jeden ...
                    }
                    else
                    {
                        SESNSelected = null;
                    }
                }
                else
                    SESNSelected = null;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            UpdateMyForm();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MyInitializeComponent();
        }

        //Zviditelneni/schovani buttonu
        public bool btnZobrazitAlternativyVisible
        {
            set { btnZobrazitAlternativy.Visible = value; }
        }

        //Clik na tlacitko pro zobrazeni alternativ
        private void btnZobrazitAlternativy_Click(object sender, EventArgs e)
        {
            zobrazitAlternativy();
        }

        //Vlastni nacteni/zobrazeni
        private void zobrazitAlternativy()
        {
            //Carovy kod ke kteremu se alternativy vztahuji
            string barcode = veRow.CZ_CarKod.Trim();
			string vnditnum = veRow.IsVNDITNUMNull() ? string.Empty : veRow.VNDITNUM.Trim();

            //Dialog se seznamem zbozi
            using (ListZbozi lz = new ListZbozi(barcode, vnditnum, vybiratZboziJenScannerem, vyhledavatZboziDleSloupce))
            {
                //Pokud se povede nacis zbozi, zobrazim dialog
                if (lz.nactiZbozi())
                {
                    //Zastavim scanner
                    ScannerStop();
                    //Zobrazim
                    DialogResult dr = lz.ShowDialog();
                    //Zapnu scanner
                    ScannerStart();

                    //Pokud vyber byl uspesny, zobrazim hodnotu sloupce
                    if (dr == DialogResult.OK)
                    {
                        vybraneZbozi = lz.vybraneZbozi;
                        inputMode = lz.input_mode;
                        try
                        {
                            Kod = vybraneZbozi[returnValueColumnName].ToString().Trim();
                            //Kod v poradku vybran, koncim
                            PerformOK();
                        }
                        catch (Exception ex)
                        {
                            //Napr dany sloupec neexistuje
                            MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            Logging.Log.Write(ex);
                        }
                    }
                }
            }
        }

        //Stisknuti klavesy na formu
        private void SejmiKodInfoForm3_KeyDown(object sender, KeyEventArgs e)
        {
            //Nactu a zobrazim vsecky alternativy
            if (e.KeyCode == Keys.F1)
            {
                zobrazitAlternativy();
            }
        }

        //Nulovani vybraneho zbozi
        public void vynulujVybraneZbozi()
        {
            vybraneZbozi = null;
        }

        //Prommena pro vybrany radek
        private DataRow vybraneZbozi = null;
        public DataRow VybraneZbozi
        {
            get { return vybraneZbozi; }
        }

        //Promenna pro typ vstupu
        private byte inputMode = 0;
        public byte InputMode
        {
            get { return inputMode; }
            set { inputMode = value; }
        }

        protected override void PerformOK()
        {
            //test zda existuje kod v seznamu predloh, pokud ne, tak se zepta
            // TODO : konfiguracne nastavit, zda se ma ptat ... ???
            if (MST_Global.VydejPovolitKontrolaSNPredloha)
            {
                if (!this.kod_tb.Items.Contains(this.kod_tb.Text))
                {
                    if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3SejmiKodInfoFormSNKodNeniVPredlozeUlozitDotaz, this.kod_tb.Text.Trim()), Fask.Localization.Localization.Vydej3SejmiKodInfoFormSNDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red)
                        == DialogResult.No)
                        return;
                }
            }

            base.PerformOK();
        }

        private void SejmiKodInfoFormSN_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);
            }
            catch { }
        }
    }
}