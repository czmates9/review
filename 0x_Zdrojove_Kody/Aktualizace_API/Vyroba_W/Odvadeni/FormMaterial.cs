using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.Forms;
using System.Linq;

namespace Fask.Vyroba_W.Odvadeni
{
    public enum ShowTypes
    {
        _Unknown,
        Back,
        StornoOK
    }

    public partial class FormMaterial : Form
    {
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter taZbozi = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();

        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter taTP = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();


        private decimal _pocetodvedeno;
        public decimal pocetodvedeno 
        {
            set { _pocetodvedeno = value; }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow = null;
        /// <summary>
        /// Aktualni vyroba
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow
        {
            set {  _productionRow = value; }
        }


        private Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable _productionSDT = null;
        /// <summary>
        ///  Materialy
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT
        {
            set { _productionSDT = value; }
        }

        private ShowTypes _types = ShowTypes._Unknown;
        /// <summary>
        ///  typ zobrazeni
        /// </summary>
        public ShowTypes types
        {
            set { _types = value; }
        }



        public FormMaterial()
        {
            InitializeComponent();

			//taZbozi.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));
			//taTP.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

            dataGrid1.Load(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString()));

            
        }

        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow MaterialSelected
        {
            get
            {
                try
                {
                    return ((DataRowView)this.productionSourcesBindingSource.Current).Row as Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow;

                }
                catch
                {
                    return null;
                }
            }
            set
            {
                //((DataRowView)this.productionSourcesBindingSource.Current).
                //this.productionSourcesBindingSource
                int index = ((DataView)this.productionSourcesBindingSource.List).Table.Rows.IndexOf(value);
                if (index > 0)
                {
                    int i = this.dataGrid1.CurrentRowIndex;
                    this.productionSourcesBindingSource.Position = index;
                    this.dataGrid1.UnSelect(i);
                    this.dataGrid1.Select(index);
                }
            }
        }

        #region Scanner car.kodu 
        bool scannerefinalized = false;
        private void ScannerFinalize()
        {
            scannerefinalized = true;
            ScannerStop();
        }

        private void ScannerStart()
        {
            if (scannerefinalized)
                return;

            try
            {
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void ScannerEventHandlerMethod(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            try
            {
                ScannerStop();

                string bcode = e.BarcodeData.Trim();

                if (bcode.Length == 0)
                    return;

                NajdiAPridejZbozi(bcode);

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void NajdiAPridejZbozi(string bcode)
        {
            // akce s pridanim materialu ...
            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable;
            if (NajdiZbozi(bcode, out zboziDatatable))
            {
                PridejZbozi(zboziDatatable);
            }
            else
            {
                MessageBox.Show("Zboží '" + bcode + "' nenalezeno", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        #region Akce se zbozim (materialem)

        private bool NajdiZbozi(string barcode, out Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable)
        {
            zboziDatatable = (new Fask.SQLiteDBs.DataSets.Vyroba()).FASK_CONS_095;

            try
            {
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByBarcode_CZMST_095(zboziDatatable, barcode);

                return zboziDatatable.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Zbozi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private void PridejZbozi(Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable zboziDatatable)
        {
            // 1) pokud je jedno zbozi => vybrat a pokracovat
            //    pokud je vice, tak nechat vybrat

            // 2) vlozit do datasetu pro zobrazeni ...

            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row zboziRow = null;
                if (zboziDatatable.Count == 0)
                {
                    throw new Exception("Pocet polozek je 0!!!");
                }
                else if (zboziDatatable.Count == 1)
                {
                    zboziRow = zboziDatatable[0];
                }
                else
                {
                    using (FormMaterialVyber fmv = new FormMaterialVyber())
                    {
                        fmv.ZboziDatatable = zboziDatatable;
                        if (fmv.ShowDialog() == DialogResult.Cancel)
                            return;
                        //TaD
                        zboziRow = fmv.MaterialSelected;
                    }
                }

                


                #region Rozpad polotovaru na jednotlive materialy

                // TODO : skontrolovat zda je nactena polozka material nebo zda ma definici jak polotovar
                // -pokud je to to material tak pokracovat
                // pokud je to polotovar tak ,predat zbozirow a PSDT jak parametr a naplnit ho materialama ktere obsahuje 

				Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dtTP = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(zboziRow.ITEMNMBR);


                if (dtTP.Count > 0)
                {
                    if (this._pocetodvedeno == 0) 
                    {

                        using (FormInputQuantity2 fiq = new FormInputQuantity2())
                        {
                            fiq.Owner = this;
                            fiq.Kod = string.Empty;
                            fiq.Text = "Množství polotovaru";
                            fiq.Nadpis = "Množství použitého polotovaru :";
                            fiq.Material = dtTP.First().ITEMNMBR_Def.Trim();

                            while (true)
                            {
                                if (DialogResult.Cancel == fiq.ShowDialog())
                                    return;
                                try
                                {
                                    this._pocetodvedeno = decimal.Parse(fiq.Kod);
                                }
                                catch (Exception exParse)
                                {
                                    MessageBox.Show(exParse.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                    fiq.Kod = fiq.Kod;
                                    continue;
                                }
                                break;
                            }
                        }
                    
                    }

                    LoadMaterialy3(dtTP);
                    

                }
                #endregion

                //Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow nPS = vyrobaCEDataSet.Production_Sources.NewProduction_SourcesRow();
                //var nPS = vyrobaCEDataSet.Production_Sources.NewProduction_SourcesRow();
                //var nPS = new Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow();
                //Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow ddd = new Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow();

                else
                {

                    var nPS = this._productionSDT.NewProduction_SourcesRow();
                    // TODO : doplnit odpovidajici hodnoty ... 
                    if (!_productionRow.IsCountEntriesNull())
                        nPS.CountEntries = _productionRow.CountEntries;
                    if (!_productionRow.IsSOPNUMBENull())
                        nPS.SOPNUMBE = _productionRow.SOPNUMBE;
                    if (!zboziRow.IsITEMDESCNull())
                        nPS.ITEMNAME = zboziRow.ITEMDESC.Trim();
                    nPS.ITEMNMBR = zboziRow.ITEMNMBR.Trim();
                    nPS.ITEMTYPE = string.Empty;    // TODO : odstranit ???
                    if (!zboziRow.IsITEMCODENull())
                        nPS.ITEMCODE = zboziRow.ITEMCODE.Trim();
                    if (!zboziRow.IsLOCNCODENull())
                        nPS.LOCNCODE = zboziRow.LOCNCODE.Trim(); // TODO : jak se sklady ???
                    nPS.MJ = zboziRow.MJ.Trim();
                    nPS.GUID_Production = _productionRow.GUID;
                    nPS.GUID = Guid.NewGuid();
                    nPS.USER_ID = _productionRow.UserID; // TODO : upravit USERID na string? ???
                    nPS.TERMINAL_ID = _productionRow.TermID;
                    //if (!zboziRow.IsWEIGHTNull())
                    //    nPS.WEIGHT = zboziRow.WEIGHT;
                    nPS.NMBRPAL = string.Empty;
                    nPS.TYPEPAL = string.Empty;
                    nPS.PRINTED = 0;


                    decimal mnozstvi = 1;
                    using (FormInputQuantity2 fiq = new FormInputQuantity2())
                    {
                        fiq.Owner = this;
                        fiq.Kod = string.Empty;
                        fiq.Text = "Množství";
                        fiq.Nadpis = "Množství použitého materiálu :";
                        fiq.Material = nPS.ITEMNAME.Trim();
                        while (true)
                        {
                            if (DialogResult.Cancel == fiq.ShowDialog())
                                return;
                            try
                            {
                                mnozstvi = decimal.Parse(fiq.Kod);
                            }
                            catch (Exception exParse)
                            {
                                MessageBox.Show(exParse.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                fiq.Kod = fiq.Kod;
                                continue;
                            }
                            break;
                        }
                    }

                    nPS.QTYSHPPD = mnozstvi * (zboziRow.QTYPACK == 0 ? 1 : zboziRow.QTYPACK);
                    nPS.QTYSHPPDMJ = mnozstvi;
                    nPS.QTYPACK = zboziRow.QTYPACK;
                    // TODO : zadani serioveho cisla/sarze ???
                    // TODO : + kontrola ???
                    nPS.SERLTNUM = string.Empty;

                    // TODO : dialog vyberu skladu
                    if (Settings.Production_Material_Source_SKLID_Enter)
                    {
                        if (!zboziRow.IsSKL_IDNull() && !String.IsNullOrEmpty(zboziRow.SKL_ID.Trim()))
                        { // zadani skladu z vybrane polozky ...
                            // TODO overeni???
                            nPS.SKL_ID = zboziRow.SKL_ID;
                        }
                        else
                        { //zadani skladu rucne

                            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
							//taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                            //Zadani ciloveho skladu a cilove lokace ...
                            using (FormInputKod fik = new FormInputKod())
                            {
                                fik.Text = "Zadejte zdrojový sklad";
                                if (!nPS.IsSKL_IDNull()
                                    && !string.IsNullOrEmpty(nPS.SKL_ID))
                                {
									var listSklady = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(nPS.SKL_ID);
                                    if (listSklady.Count() > 0)
                                        fik.Kod = listSklady.First().skl_carcode;
                                }
                                else
                                    fik.Kod = Settings.Production_Material_Source_SKLID;

                                while (true)
                                {
                                    fik.Kod = fik.Kod;
                                    if (DialogResult.Cancel == fik.ShowDialog())
                                        return;
                                    // Test na existenci id cil. skladu ...
									var listSklady = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                                    if (listSklady.Count() == 0)
                                    {
                                        if (DialogResult.Cancel == MessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                            return;
                                    }
                                    else
                                    {
                                        fik.Kod = listSklady.First().skl_id.Trim();
                                        break;
                                    }
                                }
                                nPS.SKL_ID = fik.Kod;
                            }
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                            nPS.SKL_ID = Settings.Production_Material_Source_SKLID;
                    }

                    // TODO : dialog vyberu lokace dle skladu
                    if (Settings.Production_Material_Source_LOCNCODE_Enter)
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
						//taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                        //zadani cilove lokace
                        using (FormInputKod fik = new FormInputKod())
                        {
                            fik.Text = "Zadejte zdroj. lokaci"; // "Zadejte zdrojovou lokaci"
                            if (!nPS.IsSKL_IDNull()
                                && !nPS.IsLOCNCODENull()
                                && !string.IsNullOrEmpty(nPS.SKL_ID)
                                && !string.IsNullOrEmpty(nPS.LOCNCODE)
                                )
                            {
								var listLokace = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(nPS.SKL_ID, nPS.LOCNCODE);
                                if (listLokace.Count() > 0)
                                    fik.Kod = listLokace.First().Barcode;
                            }
                            else
                                fik.Kod = Settings.Production_Material_Source_LOCNCODE;

                            while (true)
                            {
                                fik.Kod = fik.Kod;
                                if (DialogResult.Cancel == fik.ShowDialog())
                                    return;

                                // Test na existenci id cil. lokace ...
								Fask.SQLiteDBs.DataSets.Vyroba.CZMST094DataTable listLokace = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(nPS.SKL_ID, fik.Kod);
                                if (listLokace.Count() == 0)
                                {
                                    if (DialogResult.Cancel == MessageBox.Show("Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                        return;
                                }
                                else
                                {
                                    fik.Kod = listLokace.First().LOCNCODE.Trim();
                                    break;
                                }
                            }

                            nPS.LOCNCODE = fik.Kod;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_LOCNCODE))
                            nPS.LOCNCODE = Settings.Production_Material_Source_LOCNCODE;
                    }
                    //TaD 12.9.2017
                    this._productionSDT.AddProduction_SourcesRow(nPS);
                    this.MaterialSelected = nPS;
                }

                
                //vyrobaCEDataSet.Production_Sources.AddProduction_SourcesRow(nPS);
                //Globals.psdt.AddProduction_SourcesRow(nPS);
                
                //CurrencyManager cm = (CurrencyManager)(this.dataGrid1.BindingContext[this.dataGrid1.DataSource]);
                //this.dataGrid1.Select((this.productionSourcesBindingSource.List).IndexOf(nPS));
                //DataView dv = ((DataView)this.productionSourcesBindingSource.List);                
                //foreach (DataRowView i in dv)
                //{
                //    if (i.Row == nPS)
                //        this.dataGrid1.CurrentRowIndex = 
                //}
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pridani", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
            }
        }

        #endregion


        #region Hledani pomoci rekurze

        private bool PriznakNacteneMaterialy;

        /// <summary>
        /// pomoci rekurze ...
        /// </summary>
        private void LoadMaterialy3(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky)
        {

            if (vyrobky.Count() == 1)
            {
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
                //if (!isMaterial(vyrobky.First(), null))
                //    PriznakNacteneMaterialy = true;
                //else
                //    PriznakNacteneMaterialy = false;
            }
            else
            { // TODO : Vyber, ktery z vyrobku / variant vyrobku ... 
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
                //if (!isMaterial(vyrobky.First(), null))
                //    PriznakNacteneMaterialy = true;
                //else
                //    PriznakNacteneMaterialy = false;
            }
        }

        #region Parametry materialu pro rekurzi ...
        /// <summary>
        /// Slouzi pro interni vypocty v ramci rekurzivniho pruchodu stromu TP
        /// </summary>
        private class materialParams
        {
            public materialParams()
            {
            }

            public materialParams(decimal koef)
            {
                this.KoeficientSet(koef);
            }

            public materialParams(materialParams mP, decimal koef)
            {
                if (mP == null)
                {
                    mP = new materialParams();
                }

                this.KoeficientSet(koef);
            }

            public void KoeficientSet(decimal koef)
            {
                this.KoeficientNadrazeny = koef;
                this.KoeficientKumulovany *= koef;
            }

            /// <summary>
            /// Koeficient nadrazeneho uzlu
            /// </summary>
            public decimal KoeficientNadrazeny = 1;
            /// <summary>
            /// Kumulovany koeficient od 1.uzlu az do aktualni urovne...
            /// </summary>
            public decimal KoeficientKumulovany = 1;
        }

        #endregion

        /// <summary>
        /// Rekurzivni volani a 
        /// </summary>
        /// <param name="rUp"></param>
        /// <returns></returns>
        private bool isMaterial(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rUp, materialParams materialparams)
        {

            // predavani parametru materialu z vyssi urovne ... 
            if (materialparams == null)
            {
                materialparams = new materialParams();
            }
            else
            {
                decimal koefUp = 1;
				try {

					string tmpKoef = string.IsNullOrEmpty(rUp.koef) ? string.Empty : rUp.koef.Trim();

					if (tmpKoef.Contains(","))
						tmpKoef = tmpKoef.Replace(',', '.');

					koefUp = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
				}
                catch { }

                materialparams = new materialParams(materialparams, koefUp);
            }

            if ((rUp == null) || (rUp.IsID_LNull()))
                return false;

			var rDowns = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByIDH_FASK_Vyroba_TP(rUp.ID_L);


            if (rDowns.Count() == 0)
            {
                return true;
            }
            else
            {
                // alternace a jen nektere ... 
                // vyberu jestli jsou alternace na teto urovni .. 
                var rAlt = rDowns.GroupBy(x => x.IsalterNull() ? string.Empty : x.alter).OrderBy(x => x.Key);
                if (rAlt.Count() == 0)
                {
                    return false;
                }

                foreach (var alternace in rAlt)
                {
                    if (String.IsNullOrEmpty(alternace.Key))
                    {
                        foreach (var rDown in alternace)
                        {
                            bool isM = isMaterial(rDown, materialparams);

                            if (isM)
                            { // vlozim data
                                FillDatasetMaterialy(rDown, materialparams);
                            }
                        }
                    }
                    else
                    { // alternace
                        // dat na vyber z alternativnich
                        using (FormMaterialAlternaceVyber mat_alter = new FormMaterialAlternaceVyber())
                        {
                            mat_alter.Alternativy = alternace.ToArray();

                            if (mat_alter.ShowDialog() == DialogResult.Cancel)
                            {
                                continue;
                            }
                            var row_vybrana_alt = mat_alter.AlternativaSelected;

                            bool isM = isMaterial(row_vybrana_alt, materialparams);
                            if (isM)
                            {
                                FillDatasetMaterialy(row_vybrana_alt, materialparams);
                            }
                        }
                    }
                }

                //foreach (var rDown in rDowns)
                //{
                //    bool isM = isMaterial(rDown);

                //    if (isM)
                //    { // vlozim data
                //    }
                //}
            }


            return false;
        }


        /// <summary>
        /// vyplneni datasetu materialu
        /// </summary>
        /// <param name="dt_cons_095"></param>
        /// 
        /// 
        /// 

        private void FillDatasetMaterialy(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rTP, materialParams materialparams)
        {
            // TaD
            //Pridani materialu do tabulky production sources
            // na zaklade jednoh radku v tabulke FASK_Vyroba_TP
            //
            // 1. naèteni z tabulky zbozi(fask_CONS_095) vsechny nalezene zaznamy odpovidajici ITEMNMBR pomoci FillByITEMNMBR
            // 2. kontrola zda byl nalezen jeden material nebo víc, mužou byt rozdilne èar kody, mnozstvi, seriove cislo ...
            // 3. pokud neni nalezen zadny metoda se ukonci
            // 4. pokud je nalezeno vyc jak jeden tak se zavla okno s vyberem a to smaže datatable a vrati pouze jeden radek
            // 5. pokud je jen jeden radek tak se jde rovnou plnit
            // (plneni jednoh radku z tabulky pomoci freach je historicka zalezitost kdy se plnila najednou cela tabulka)


            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_cons_095 = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();

            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta_cons_095 = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
			//ta_cons_095.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

			Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByITEMNMBR_CZMST_095(true,dt_cons_095, rTP.ITEMNMBR_fol);

            if ((dt_cons_095 == null) || (dt_cons_095.Count == 0))
                return;


            else if (dt_cons_095.Count > 1)
            {
                using (FormMaterialAlternaceVyber mat_alter = new FormMaterialAlternaceVyber())
                {
                    //mat_alter.Alternativy = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow[] { rTP };

                    mat_alter.dt_Material = dt_cons_095;

                    if (mat_alter.ShowDialog() == DialogResult.Cancel)
                    {
                        // TODO : urcite???
                        return;
                    }
                    var row_vybrany_095 = mat_alter.MaterialSelected;
                    dt_cons_095.Clear();
                    dt_cons_095.ImportRow(row_vybrany_095);
                }
            }




            // predavany parametr je tabulka zbozi materialu pridavaneho 

            try
            {
                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row i in dt_cons_095)
                {

                    Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow row = _productionSDT.NewProduction_SourcesRow();

                    row.CountEntries = _productionRow.CountEntries;
                    row.SOPNUMBE = _productionRow.SOPNUMBE.Trim();
                    row.ITEMNAME = i.ITEMDESC.Trim();
                    row.ITEMNMBR = i.ITEMNMBR.Trim();
                    row.ITEMTYPE = string.Empty;

                    if (i.IsITEMCODENull())
                        row.SetITEMCODENull();
                    else
                        row.ITEMCODE = i.ITEMCODE.Trim();

                    row.MJ = i.MJ.Trim();

                    decimal mnozstvi = 1;
                    decimal koeficient = 1;
                    try 
					{

						string tmpKoef = string.IsNullOrEmpty(rTP.koef) ? string.Empty : rTP.koef.Trim();

						if (tmpKoef.Contains(","))
							tmpKoef = tmpKoef.Replace(',', '.');

						koeficient = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);

					}
                    catch { }
                    //mnozstvi = (decimal.Parse(rTP.koef) * PocetOdvedeno);
                    mnozstvi = materialparams.KoeficientKumulovany * koeficient * _pocetodvedeno;

                    row.QTYSHPPD = mnozstvi * (i.QTYPACK == 0 ? 1 : i.QTYPACK);
                    row.QTYSHPPDMJ = mnozstvi;
                    row.QTYPACK = i.QTYPACK;


                    // TODO : seriove cisla
                    row.SERLTNUM = string.Empty;

                    //Zadavani skladu Material konfiguracne
                    if (Settings.Production_Material_Source_SKLID_Enter)
                    {
                        // kontrola zda je SKL_ID zadano a ci neni prazdne v FASK_CONS_095
                        if (!i.IsSKL_IDNull() && !String.IsNullOrEmpty(i.SKL_ID.Trim()))
                        { // zadani skladu z vybrane polozky ...
                            // TODO overeni???
                            row.SKL_ID = i.SKL_ID;
                        }
                        else
                        { //zadani skladu rucne

                            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
							//taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                            //Zadani ciloveho skladu a cilove lokace ...
                            using (Vyroba_W.Forms.FormInputKod2 fik = new Vyroba_W.Forms.FormInputKod2())
                            {
                                fik.Text = "Zadejte zdrojový sklad";
                                fik.Nazev = "Zadejte zdrojový sklad";
                                fik.Mnozstvi = row.QTYSHPPD.ToString();
                                fik.Sklad = null;
                                fik.Lokace = null;
                                fik.Material = row.ITEMNAME;


                                if (!row.IsSKL_IDNull()
                                    && !string.IsNullOrEmpty(row.SKL_ID))
                                {
									Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable listSklady = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(row.SKL_ID);

                                    if (listSklady.Count > 0)
                                        fik.Kod = listSklady[0].skl_carcode;
                                }
                                else
                                    fik.Kod = Settings.Production_Material_Source_SKLID;

                                while (true)
                                {
                                    fik.Kod = fik.Kod;
                                    if (DialogResult.Cancel == fik.ShowDialog())
                                        return;

                                    // Test na existenci id cil. skladu ...
									var listSklady = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                                    if (listSklady.Count == 0)
                                    {
                                        if (DialogResult.Cancel == MessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cíloví sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                            return;
                                    }
                                    else
                                    {
                                        fik.Kod = listSklady[0].skl_id.Trim();

                                        break;
                                    }
                                }
                                row.SKL_ID = fik.Kod;

                            }
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_SKLID))
                            row.SKL_ID = Settings.Production_Material_Source_SKLID;
                    }

                    // TODO : dialog vyberu lokace dle skladu
                    if (Settings.Production_Material_Source_LOCNCODE_Enter)
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
						//taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                        //zadani cilove lokace
                        using (Vyroba_W.Forms.FormInputKod2 fik = new Vyroba_W.Forms.FormInputKod2())
                        {
                            fik.Text = "Zadejte zdrojovu lokaci";
                            fik.Nazev = "Zadejte zdrojovou lokaci";
                            fik.Mnozstvi = row.QTYSHPPD.ToString();
                            fik.Sklad = row.SKL_ID;
                            fik.Lokace = null;
                            fik.Material = row.ITEMNAME;

                            if (!row.IsSKL_IDNull()
                                && !row.IsLOCNCODENull()
                                && !string.IsNullOrEmpty(row.SKL_ID)
                                && !string.IsNullOrEmpty(row.LOCNCODE)
                                )
                            {
								var listLokace = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(row.SKL_ID, row.LOCNCODE);
                                if (listLokace.Count > 0)
                                    fik.Kod = listLokace[0].Barcode;
                            }
                            else
                                fik.Kod = Settings.Production_Material_Source_LOCNCODE;

                            while (true)
                            {
                                fik.Kod = fik.Kod;
                                if (DialogResult.Cancel == fik.ShowDialog())
                                    return;

                                // Test na existenci id cil. lokace ...
								var listLokace = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(row.SKL_ID, fik.Kod);
                                if (listLokace.Count == 0)
                                {
                                    if (DialogResult.Cancel == MessageBox.Show("Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                        return;
                                }
                                else
                                {
                                    fik.Kod = listLokace[0].LOCNCODE.Trim();

                                    break;
                                }
                            }
                            row.LOCNCODE = fik.Kod;

                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Settings.Production_Material_Source_LOCNCODE))
                            row.LOCNCODE = Settings.Production_Material_Source_LOCNCODE;
                    }

                    row.GUID = Guid.NewGuid();
                    row.GUID_Production = _productionRow.GUID;
                    row.USER_ID = _productionRow.UserID;
                    row.TERMINAL_ID = _productionRow.TermID;
                    //row.WEIGHT = 
                    row.NMBRPAL = string.Empty;
                    row.TYPEPAL = string.Empty;
                    row.PRINTED = 0;

                    _productionSDT.AddProduction_SourcesRow(row);
                }

                //_productionSDT.AcceptChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
            }
        }   

        #endregion

        private void FormMaterial_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;


            if (_types == ShowTypes.StornoOK)
            {
                panelButtons_Resize(null, null);
            }
            else if (_types == ShowTypes.Back) 
            {
                buttonOK.Visible = false;
                menuItemAkceOK.Text = "Pokraèovat";
                //menuItemStorno.Enabled = false;
                buttonStorno.Text = "Pokraèovat";
                menuItemAkce.MenuItems.Remove(menuItemStorno);
            }

            
            productionSourcesBindingSource.DataSource = _productionSDT;


            //dataGrid1.ScrollBarHorizontal = new HScrollBar();
            //aktivace scanneru
            ScannerStart();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelbutton.Width / 2, panelbutton.Height);
            buttonStorno.Size = nsize;
        }

        private void finalize()
        {
            this.ScannerFinalize();
            dataGrid1.Save(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString())); 
        }

        private void FormMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (_types == ShowTypes.StornoOK)
            {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        PerformOK();
                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        PerformStorno();
                    }
                    else if (e.KeyCode == Keys.Back)
                    {
                        PerformDelete();
                    }
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            }
            else if (_types == ShowTypes.Back)
            {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        PerformStorno();
                    }
                    else if (e.KeyCode == Keys.Back)
                    {
                        PerformDelete();
                    }
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            }

        }

        private void PerformDelete()
        {
            var vybranymat = this.MaterialSelected;
            if (vybranymat != null)
            {
                if (DialogResult.No == MessageBox.Show(
                    "Odstranit ?\n" + vybranymat.ITEMNAME.Trim() + "\n" + vybranymat.QTYSHPPD.ToString() + " " + vybranymat.MJ.Trim()
                    , this.Text
                    , MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1
                    ))
                    return;
                //TaD 12.9.2017
                this._productionSDT.RemoveProduction_SourcesRow(vybranymat);
                //vyrobaCEDataSet.Production_Sources.RemoveProduction_SourcesRow(vybranymat);
                //Globals.psdt.RemoveProduction_SourcesRow(vybranymat);

            }
        }

        public void PerformOK()
        {
            this.finalize();
            this.DialogResult = DialogResult.OK;
        }

        public void PerformStorno()
        {
            this.finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void menuItemAkceOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void menuItemAkceSmazat_Click(object sender, EventArgs e)
        {
            this.PerformDelete();
        }

        private void menuItemHledatCarkod_Click(object sender, EventArgs e)
        {
            HledejCarkod();
        }

        private void HledejCarkod()
        {
            try
            {
                ScannerStop();
                string bkod = string.Empty;

                using (FormInputKod fik = new FormInputKod())
                {
                    fik.Text = "Èár. kód operace";
                    if (DialogResult.Cancel == fik.ShowDialog())
                        return;

                    bkod = fik.Kod;
                }

                NajdiAPridejZbozi(bkod);
            }
            catch (Exception ex)
            {
                //zalogovat
                string exx = ex.Message.ToString();
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformStorno();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformStorno();
        }

		private void panelbutton_Resize(object sender, EventArgs e)
		{
			Size nsize = new Size(panelbutton.Width / 2, panelbutton.Height);
			buttonOK.Size = nsize;
		}
    }
}

