using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class SejmiKodHromadneInfoForm : SejmiKodForm
    {

        //public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable SETable = null;
        public Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky = new Fask.MST_W.Vydej_3.ListPolozekVydej.ListPolozekRow[0];
        //public Vydej_2.ListPolozekVydej.ListPolozekRow polozka = null;

        /// <summary>
        /// Vraci aktualni vybranou polozku v pohledu
        /// </summary>
        public Vydej_3.ListPolozekVydej.ListPolozekRow PolozkaAktualniVybrana
        {
            get
            {
                try
                {
                    return dataGrid21.BindingContext[dataGrid21.DataSource].Current as Vydej_3.ListPolozekVydej.ListPolozekRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.SQLiteDBs.DataSets.Vydej VydejParametry { get; set; }

        //public SejmiKodInfoForm()
        //    : this()
        //{
        //    InitializeComponent();
        //    MyInitializeComponent();
        //}

        //public string REZ_2
        //{
        //    get
        //    {
        //        if (NeshodnyGet())
        //            return "Neshodny";
        //        else
        //            return "Shodny";
        //    }
        //    set
        //    {
        //        switch (value)
        //        {
        //            case "Neshodny": 
        //                NeshodnySet(true);
        //                break;
        //            case "Shodny": 
        //            default:
        //                NeshodnySet(false);
        //                break;
        //        }
        //    }
        //}

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="veRow">Radek tabulky, ktery se zobrazi</param>
        public SejmiKodHromadneInfoForm(string popis, TypeOfCode typeOfCode, Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky)//SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable SETable)
            : this(popis, typeOfCode, 0, false, false, polozky)
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
        public SejmiKodHromadneInfoForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky)//SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable SETable)
            : this(popis, typeOfCode, len, checkLen, allowEmpty, "", polozky)
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
        public SejmiKodHromadneInfoForm(
            string popis,
            TypeOfCode typeOfCode,
            decimal len,
            bool checkLen,
            bool allowEmpty,
            string retezecKPredvyplneni,
            //SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable SETable)
            Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky)
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            InitializeComponent();
            InitializeGridStyles();
            //this.SETable = SETable;
            this.polozky = polozky;
            MyInitializeComponent();
            this.kod_tb.Text = retezecKPredvyplneni;
            this.kod_tb.SelectAll();
        }

        private void InitializeGridStyles()
        {
            System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
            Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnSOPNUMBE;
            Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnMnozstvi;
            Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnZbyva;
            Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnNasnimano;

            dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            dataGridTextBoxColumnSOPNUMBE = new Fask.Graphic.DataGrid2TextBoxColumn();
            dataGridTextBoxColumnMnozstvi = new Fask.Graphic.DataGrid2NumberBoxColumn();
            dataGridTextBoxColumnZbyva = new Fask.Graphic.DataGrid2NumberBoxColumn();
            dataGridTextBoxColumnNasnimano = new Fask.Graphic.DataGrid2NumberBoxColumn();

            dataGridTableStyle1.MappingName = polozky.GetType().Name;

            dataGrid21.TableStyles.Add(dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            dataGridTableStyle1.GridColumnStyles.Add(dataGridTextBoxColumnSOPNUMBE);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridTextBoxColumnMnozstvi);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridTextBoxColumnZbyva);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridTextBoxColumnNasnimano);
            // 
            // dataGridTextBoxColumnSOPNUMBE
            // 
            dataGridTextBoxColumnSOPNUMBE.Alignment = System.Drawing.StringAlignment.Near;
            dataGridTextBoxColumnSOPNUMBE.Format = "";
            dataGridTextBoxColumnSOPNUMBE.HeaderText = "Obj.è.";
            dataGridTextBoxColumnSOPNUMBE.LineAlignment = System.Drawing.StringAlignment.Near;
            dataGridTextBoxColumnSOPNUMBE.MappingName = "SOPNUMBE";
            dataGridTextBoxColumnSOPNUMBE.SelectionShow = false;
            dataGridTextBoxColumnSOPNUMBE.Tag = "";
            dataGridTextBoxColumnSOPNUMBE.Width = 100;
            // 
            // dataGridTextBoxColumnMnozstvi
            // 
            dataGridTextBoxColumnMnozstvi.Alignment = System.Drawing.StringAlignment.Far;
            dataGridTextBoxColumnMnozstvi.Format = Settings.UIFormatDesCisel;
            dataGridTextBoxColumnMnozstvi.HeaderText = "Množství";
            dataGridTextBoxColumnMnozstvi.LineAlignment = System.Drawing.StringAlignment.Near;
            dataGridTextBoxColumnMnozstvi.MappingName = "Mnozstvo";
            dataGridTextBoxColumnMnozstvi.SelectionShow = false;
            dataGridTextBoxColumnMnozstvi.Tag = "";
            dataGridTextBoxColumnMnozstvi.Width = 70;
            // 
            // dataGridTextBoxColumnZbyva
            // 
            dataGridTextBoxColumnZbyva.Alignment = System.Drawing.StringAlignment.Far;
            dataGridTextBoxColumnZbyva.Format = Settings.UIFormatDesCisel;
            dataGridTextBoxColumnZbyva.HeaderText = "Zbývá";
            dataGridTextBoxColumnZbyva.LineAlignment = System.Drawing.StringAlignment.Near;
            dataGridTextBoxColumnZbyva.MappingName = "Ostava";
            dataGridTextBoxColumnZbyva.SelectionShow = false;
            dataGridTextBoxColumnZbyva.Tag = "";
            dataGridTextBoxColumnZbyva.Width = 70;
            // 
            // dataGridTextBoxColumnNasnimano
            // 
            dataGridTextBoxColumnNasnimano.Alignment = System.Drawing.StringAlignment.Far;
            dataGridTextBoxColumnNasnimano.Format = Settings.UIFormatDesCisel;
            dataGridTextBoxColumnNasnimano.HeaderText = "Nasnímáno";
            dataGridTextBoxColumnNasnimano.LineAlignment = System.Drawing.StringAlignment.Near;
            dataGridTextBoxColumnNasnimano.MappingName = "PocetNasnim";
            dataGridTextBoxColumnNasnimano.SelectionShow = false;
            dataGridTextBoxColumnNasnimano.Tag = "";
            dataGridTextBoxColumnNasnimano.Width = 70;

            this.dataGrid21.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid21.Font = new Font(this.dataGrid21.Font.Name, Settings.UIGridFont, this.dataGrid21.Font.Style);
            this.dataGrid21.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));

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
                panelData.BringToFront(); //abyse to dobre zobrazilo ... ???

                //if (SETable != null)
                if (polozky != null)
                {
                    this.dataGrid21.DataSource = polozky;

                    bool findContinue = true; //slouzi pro indikaci pokracovani hledani, pokud predchozi neuspeje ...

                    int index = 0;
                    if (findContinue)
                    {
                        //najde prvni nezadanou a tu zobrazi ...
                        for (int i = 0; i < polozky.Length; i++)
                        {
                            index = i;
                            if (polozky[i].PocetNasnim == 0)
                            {
                                findContinue = false;
                                break;
                            }
                        }
                    }

                    if (findContinue)
                    {
                        //najde prvni neuplnou a tu zobrazi ...
                        for (int i = 0; i < polozky.Length; i++)
                        {
                            index = i;
                            if (polozky[i].Ostava > 0)
                            {
                                findContinue = false;
                                break;
                            }
                        }
                    }

                    this.dataGrid21.CurrentRowIndex = index;

                    UpdateForm();
                    UpdateFormKodInit();
                    this.kod_tb.Focus();
                    this.kod_tb.SelectAll();
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void UpdateForm()
        {
            try
            {
                Vydej_3.ListPolozekVydej.ListPolozekRow polozka = this.PolozkaAktualniVybrana;
                //ItemNmbr_l.Text = SETable[0].ITEMNMBR;
                // [25.6.2013 JiS] - odstraneno ze zobrazeni ...
                //ItemNmbr_l.Data = polozka.Itemnmbr; //SETable[0].ITEMNMBR;

                //ItemDesc_l.Text = SETable[0].ITEMDESC;
                ItemDesc_l.Data = polozka.Nazov; //SETable[0].ITEMDESC;
                //CZ_CarKod_l.Text = SETable[0].VNDITNUM.Trim() + "(" + SETable[0].CZ_CarKod.Trim() + ")";
                //baleni_l.Text = "Balení: " + SETable[0].QTYPACK.ToString(Settings.UIFormatDesCisel);
                //baleni_l.Visible = (SETable[0].QTYPACK > 0);
                //nacist_l.Text = "Celkem naèíst: " + CelkovePozadovaneMnozstvi.ToString(Settings.UIFormatDesCisel);
                //nacist_l.Data = CelkovePozadovaneMnozstvi.ToString(Settings.UIFormatDesCisel);
                //nacist_l.Data = polozka.Ostava.ToString(Settings.UIFormatDesCisel);
                nacist_l.Data = polozka.Mnozstvo.ToString(Settings.UIFormatDesCisel);
                //nacist_l.Text = "Naèíst: " + SETable.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                //nacteno_l.Text = "Naèteno: " + this.NactenoMnozstvi.ToString(Settings.UIFormatDesCisel);
                //nacteno_l.Data = this.NactenoMnozstvi.ToString(Settings.UIFormatDesCisel);
                nacteno_l.Data = polozka.PocetNasnim.ToString(Settings.UIFormatDesCisel);
                // + Fask.MST_W.Vydej_3.ListPolozek3.Instance.Nacteno(veRow.ITEMNMBR, veRow.SOPNUMBE, veRow.ORD).ToString(Settings.UIFormatDesCisel);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void UpdateFormKodInit()
        {
            if (VydejParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT)
            {
                if (VydejParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA)
                    this.kod_tb.Text = 1.ToString(Settings.UIFormatDesCisel);
                else if (VydejParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI)
                {
                    Vydej_3.ListPolozekVydej.ListPolozekRow polozka = this.PolozkaAktualniVybrana;
                    //ToDoS: jak pracovat s qtypack...
                    //decimal mn = (((decimal)SETable.Compute("Sum(QTYSHPPD)", "") - Nacteno) / (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1));
                    //decimal mn = ((Nacist - Nacteno));
                    decimal mn = polozka.Ostava;
                    if (mn > 0)
                        this.kod_tb.Text = mn.ToString(Settings.UIFormatDesCisel);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MyInitializeComponent();
        }

        //private void toolBarExtended_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        //{
        //    if (e.Button == toolBarButtonNeshodne)
        //    {
        //        NeshodnySwitch();
        //    }
        //}

        //private void NeshodnySwitch()
        //{
        //    if (toolBarButtonNeshodne.Pushed)
        //        NeshodnySetNeshodny();
        //    else
        //        NeshodnySetShodny();
        //}

        //private void NeshodnySet(bool neshodny)
        //{
        //    toolBarButtonNeshodne.Pushed = neshodny;
        //    NeshodnySwitch();
        //}

        //private bool NeshodnyGet()
        //{
        //    return toolBarButtonNeshodne.Pushed;
        //}

        //private void NeshodnySetNeshodny()
        //{
        //    toolBarButtonNeshodne.ImageIndex = 1;
        //    toolBarButtonNeshodne.ToolTipText = "Neshodný";
        //}
        //private void NeshodnySetShodny()
        //{
        //    toolBarButtonNeshodne.ImageIndex = 0;
        //    toolBarButtonNeshodne.ToolTipText = "Shodný";
        //}

        protected override void PerformOK()
        {
            decimal mnozstviZadane = 0;
            try
            {
                mnozstviZadane = decimal.Parse(this.Kod);
            }
            catch (Exception e)
            {
                MessageBoxBig.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            this.finalize();
            base.PerformOK();
        }

        protected override void PerformCancel()
        {
            this.finalize();
            base.PerformCancel();
        }

        protected override void finalize()
        {
            base.finalize();
            this.dataGrid21.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void dataGrid21_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            UpdateForm();
            UpdateFormKodInit();
        }

        private void dataGrid21_CurrentCellChanged(object sender, EventArgs e)
        {
            UpdateForm();
            UpdateFormKodInit();
        }
    }
}

