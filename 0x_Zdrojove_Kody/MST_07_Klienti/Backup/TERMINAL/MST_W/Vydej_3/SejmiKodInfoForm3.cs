using System;
using System.Data;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class SejmiKodInfoForm3 : SejmiKodForm
    {

        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow = null;
        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow viRow = null;
        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow sesnRow = null;

        //Promenna pro nazev sloupce, jehoz hodnota se ma brat
        public string returnValueColumnName = string.Empty;
        private bool zobrazitAlternativniLokace = false;
        private bool zobrazitAlternativyZbozi = false;

        //Hodnoty nastaveni od aktualniho modulu
        //Odpovida konfiguraci, toto vychozi hodnoty - dalsi moznosti jsou uvedene sloupce samostatne
        public string vyhledavatZboziDleSloupce = "CZ_CARKOD,VNDITNUM";
        public bool vybiratZboziJenScannerem = false;

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
        public SejmiKodInfoForm3(string popis, TypeOfCode typeOfCode, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow)
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
        public SejmiKodInfoForm3(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow)
            : this(popis, typeOfCode, len, checkLen, allowEmpty, "", veRow, null, null)
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
        public SejmiKodInfoForm3(
            string popis,
            TypeOfCode typeOfCode,
            decimal len,
            bool checkLen,
            bool allowEmpty,
            string retezecKPredvyplneni,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow veRow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow viRow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow sesnRow)
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            InitializeComponent();
            this.veRow = veRow;
            this.viRow = viRow;
            this.sesnRow = sesnRow;
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
                UpateMyForm();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void UpateMyForm()
        {
            if (veRow != null)
            {
                ItemNmbr_l.Text = veRow.ITEMNMBR.Trim() + " (" + veRow.CZ_CarKod.Trim() + ")";
                ItemDesc_l.Text = veRow.ITEMDESC.Trim();
				CZ_CarKod_l.Text = veRow.IsVNDITNUMNull() ? string.Empty : veRow.VNDITNUM.Trim();
                baleni_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoForm3Baleni + " " + veRow.QTYPACK.ToString(Settings.UIFormatDesCisel);
                baleni_l.Visible = (veRow.QTYPACK > 0);
                nacist_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoForm3Nacist + " " + veRow.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                nacteno_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoForm3Nacteno + " " + Fask.MST_W.Vydej_3.ListPolozek3.Instance.Nacteno(veRow.ITEMNMBR, veRow.SOPNUMBE, veRow.ORD).ToString(Settings.UIFormatDesCisel);
                Note_l.Text = string.IsNullOrEmpty(veRow.Note) ? string.Empty : veRow.Note.Trim();
            }
            if (viRow != null)
            {
                string serltnum = viRow.SERLTNUM.Trim();
                SERLTNUM_l.Visible = serltnum.Length > 0;
                SERLTNUM_l.Text = MST_Global.SNName + ": ";
                if (veRow != null && veRow.CZ_SerNum_Track == 2)
                    SERLTNUM_l.Text = Fask.Localization.Localization.Vydej3SejmiKodInfoForm3Sarze + " ";
                SERLTNUM_l.Text += serltnum;
            }
            else
            {
                SERLTNUM_l.Visible = false;
            }

            if (sesnRow != null)
            {
                this.SERLTNUM_l.Text = sesnRow.SERLNMBR.Trim();
                this.nacist_l.Text += "(" + sesnRow.QTY.ToString(Settings.UIFormatDesCisel) + ")";
                if (sesnRow.IsNactenoNull())
                    this.nacteno_l.Text += "(0)";
                else
                    this.nacteno_l.Text += "(" + sesnRow.Nacteno.ToString(Settings.UIFormatDesCisel) + ")";
            }

            btnZobrazitAlternativy.Visible = zobrazitAlternativniLokace;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MyInitializeComponent();
        }

        //Zviditelneni/schovani buttonu
        public bool btnZobrazitAlternativyVisible
        {
            set
            {
                btnZobrazitAlternativy.Visible = value;
                zobrazitAlternativyZbozi = value;
            }
        }

        public bool btnZobrazitAlternativniLokaceVisible
        {
            get
            {
                return zobrazitAlternativniLokace;
            }
            set
            {
                //btnZobrazitAlternativy.Visible = value;
                zobrazitAlternativniLokace = value;
            }
        }

        //Clik na tlacitko pro zobrazeni alternativ
        private void btnZobrazitAlternativy_Click(object sender, EventArgs e)
        {
            if (zobrazitAlternativyZbozi)
            {
                zobrazitAlternativy();
            }
            else if (zobrazitAlternativniLokace)
            {
                zobrazitLokace();
            }
        }

        private void zobrazitLokace()
        {
            try
            {
                ScannerStop();

                using (VydejZobrazeniLokaciList lokacelist = new VydejZobrazeniLokaciList(veRow, viRow))
                {
                    lokacelist.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                kod_tb.Focus();
                ScannerStart();
            }
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
                if (zobrazitAlternativyZbozi)
                {
                    zobrazitAlternativy();
                }
                else if (zobrazitAlternativniLokace)
                {
                    zobrazitLokace();
                }
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

        private void SejmiKodInfoForm3_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);
            }
            catch
            {

                throw;
            }
        }
    }
}