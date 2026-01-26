using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.Extensions;
using System.Linq;

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormOperacePotvrzeni : Form
    {

        private bool PriznakNacteneMaterialy;

		//private Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter _lstoperuserta = new Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter();
        private DateTime? lastoperationuserdt = null;


        private DelegateUpdateForm delegateUpdateForm = null;
        private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow;
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow
        {
            get { return _productionRow; }
            set
            {
                _productionRow = value;
                UpdateForm();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable _productionSDT;
        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT
        {
            get { return _productionSDT; }
            set
            {
                _productionSDT = value;
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik;

        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _pracovnik; }
            set
            {
                _pracovnik = value;
                UpdateForm();
            }
        }
        private decimal _PocetOdvedeno;
        public decimal PocetOdvedeno
        {
            get { return _PocetOdvedeno; }
            set
            {
                _PocetOdvedeno = value;

            }
        }



        private Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow _machine;
        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
                UpdateForm();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow _vpp;
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VPP
        {
            get { return _vpp; }
            set
            {
                _vpp = value;
                UpdateForm();
            }
        }


        public FormOperacePotvrzeni()
        {
            InitializeComponent();
        }

        private void FormOperacePotvrzeni_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            if (Settings.Production_Material_Enter && Settings.Production_Material_OperacePotvrzeniButton)
            {
                Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
                buttonStorno.Size = nsize;
                buttonmaterial.Size = buttonOK.Size;
                PriznakNacteneMaterialy = false;
            }
            else
            {
                buttonmaterial.Visible = false;
                Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
                buttonStorno.Size = nsize;

            }

            delegateUpdateForm = new DelegateUpdateForm(UpdateForm);
            //_lstoperuserta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
            timerDateTimeOperaceUpdate.Enabled = true;
        }

        private void FormOperacePotvrzeni_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else if (e.KeyCode == Keys.D1)
                {
                    buttonKorekce_Click(null, null);
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        public void PerformOK()
        {
			#region TaD dle Zadani od JaS dne 27.7.2020 je s toho vytvoøek koèkopes... kde èas je zadavan max 1 den...
			
			TimeSpan celkovycas;
			try
			{
				celkovycas = TimeSpan.Parse(labelCelkovyCas.Text);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				celkovycas = TimeSpan.Parse("23:59:59");
				DialogResult dr = MessageBox.Show("Celkový èas je moc velký. Bude použit maximalni povolený: " + celkovycas.ToStringHHmm() + "\n\nOpravdu chcete odvést výrobu?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.No)
				{
					return;
				}
			}

			if (celkovycas < TimeSpan.Zero)
			{
				DialogResult dr = MessageBox.Show("Celkový èas je menší než " + TimeSpan.Zero.ToStringHHmm() + "\n\nOpravdu chcete odvést výrobu?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.No) return;
			} 
			#endregion

            this.finalize();

            if (Settings.Production_Material_Enter && Settings.Production_Material_OperacePotvrzeniButton)
            {
                //tatp = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
				//tatp.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

                Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(_vpp.ITEMNMBR);


                if (!PriznakNacteneMaterialy && vyrobky.Count > 0)
                {
                    if (MessageBox.Show("Nebyly zadány materiály. " + Environment.NewLine + "Zadat materialy?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        LoadMaterialy3(vyrobky);
                    else
                        return;
                }

                if (ProductionSDT.Count == 0 && vyrobky.Count > 0)
                {
                    if (MessageBox.Show("Materiály byly zadány ale seznam je prázdný. " + Environment.NewLine + "Zadat materialy?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        LoadMaterialy3(vyrobky);
                    else
                        return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            this.timerDateTimeOperaceUpdate.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        delegate void DelegateUpdateForm();
        private void UpdateForm()
        {
            try
            {
				if (lastoperationuserdt == null)
				{
					//lastoperationuserdt = _lstoperuserta.GetLastOperationDateTime(_pracovnik.id);
					lastoperationuserdt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.GetLastOperationDateTime(_pracovnik.id);
					
				}
            }
            catch (Exception ex)
            {
                // zalogovat
                string exx = ex.Message.ToString();

            }
            // TODO : osetrit vyjimky ...
            try
            {
                try
                {
                    labelPracovnik.Text = _pracovnik.surname.Trim() + " " + _pracovnik.firstname.Trim();
                }
                catch { }
                try
                {
                    labelStroj.Text = _machine.description.Trim();
                }
                catch { }
                try { lblPolozka.Text = _vpp == null || _vpp.IsITEMDESCNull() ? "?" : _vpp.ITEMDESC.Trim(); }
                catch { }
                try
                {
                    labelPripravnyCas.Text = TimeSpan.FromMinutes(_productionRow.TIMEPREP).ToStringHHmm(); //new TimeSpan(0, _vpp.TIMEPREP, 0).ToString();
                }
                catch { }
                try
                {
                    labelJednotkovyCas.Text = TimeSpan.FromMinutes(_productionRow.TIMEUNIT).ToStringHHmm(); //new TimeSpan(0, _vpp.TIMEUNIT, 0).ToString();
                }
                catch { }
                try
                {
                    labelKusu.Text = _productionRow.qty.ToString("0.####");
                }
                catch { }
                try
                {
                    labelKorekceCasu.Text = TimeSpan.FromMinutes(_productionRow.IsTIMECORNull() ? 0 : _productionRow.TIMECOR).ToStringHHmmss(); //new TimeSpan(0,_productionRow.TIMECOR, 0).ToString();
                }
                catch { }

                try
                {
                    //labelCelkovyCas.Text = (_productionRow.TIMESTOP - Settings.LastProductionDateTime).ToString();
                    labelCelkovyCas.Text =
                        (_productionRow.TIMESTOP
                        - (lastoperationuserdt ?? Settings.LastProductionDateTime)
                        - (_productionRow.IsTIMECORNull() ? TimeSpan.FromMinutes(0) : TimeSpan.FromMinutes(_productionRow.TIMECOR))).ToStringHHmmss();
                }
                catch { }

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, this.Text);
            }
        }

        private void buttonKorekce_Click(object sender, EventArgs e)
        {
            try
            {
                FormKorekceCasu frmkorekcecasu = new FormKorekceCasu();
                frmkorekcecasu.ProductionRow = _productionRow;
                frmkorekcecasu.ShowDialog();
            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, this.Text);
                MessageBox.Show(ex.Message, this.Text);
            }

            this.UpdateForm();
        }

        private void timerDateTimeOperaceUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.IsDisposed)
                    return;

                if (_productionRow != null)
                {
                    _productionRow.TIMESTOP = DateTime.Now;
                }
               
                this.BeginInvoke(delegateUpdateForm);

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, "FormOperacePotvrzeni.timerDateTimeOperaceUpdate_Tick()");
            }
        }

        private void buttonmaterial_Click(object sender, EventArgs e)
        {

            try
            {

				//tatp = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
				//tatp.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

				Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(_vpp.ITEMNMBR);

                
                //VPP ITEMNMBR a podle toho hledam materialy
                if (!PriznakNacteneMaterialy  && vyrobky.Count > 0)
                {
                    LoadMaterialy3(vyrobky);
                }

                //Rozpad materialu
                using (FormMaterial frmMaterial = new FormMaterial())
                {
                    frmMaterial.pocetodvedeno = this.PocetOdvedeno;
                    frmMaterial.ProductionRow = this.ProductionRow;
                    frmMaterial.ProductionSDT = this.ProductionSDT;
                    frmMaterial.types = ShowTypes.Back;
                    frmMaterial.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
            }
        }


        /// <summary>
        /// Nacteni Materialu
        /// </summary>
        //private void LoadMaterialy()
        //{
        //    // TaD 16.11.2017
        //    // nacteni materialu
        //    // 1) nacteni materialu z vazebne tabulky podle ITEMNMBR vyrobku a bez alternace 
        //    // 2) nacteni vsech alternaci ktere sou vazane v vyrobku pomoci ITEMNMBR
        //    // 3) Projity všech alternace a vrati je jak tabulku alternaci pro dany vyrobek
        //    // 4) pro danou alternaci a ITEMNMBR vyrobku nalest vsechny materialy, dat na vyber v okne a pak pridat jeden vybrany material
        //    // ... 
        //    try
        //    {
        //        Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter taTP = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
        //        taTP.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf);

        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dtTP_alter = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_cons_095 = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();

        //        //psdt
        //        Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta_cons_095 = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
        //        ta_cons_095.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf);

        //        dt_cons_095.Clear();
        //        ta_cons_095.Fillalter_isNull(dt_cons_095, _vpp.ITEMNMBR);

        //        if (dt_cons_095.Count > 1)
        //        {
        //            //nacte vsechny materialy ktere nemaji alternaci
        //            FillDatasetMaterialy(dt_cons_095);
        //        }


        //        taTP.Fill_Alter_By_ITEMNMBR(dtTP_alter, _vpp.ITEMNMBR);

        //        foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow item in dtTP_alter)
        //        {
        //            //dt_cons_095.Clear();
        //            ta_cons_095.FillByALTER(dt_cons_095, _vpp.ITEMNMBR, item.alter);
        //            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row row_vybrany;// = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row();

        //            if (dt_cons_095.Count > 1)
        //            {

        //                using (FormMaterialAlternaceVyber mat_alter = new FormMaterialAlternaceVyber())
        //                {
        //                    mat_alter.dt_Material = dt_cons_095;

        //                    if (mat_alter.ShowDialog() == DialogResult.Cancel)
        //                    {
        //                        return;
        //                    }
        //                    row_vybrany = mat_alter.MaterialSelected;
        //                }

        //            }
        //            else
        //                row_vybrany = dt_cons_095[0];






        //            //dt_cons_095.Clear();
        //            //Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row row; //= dt_cons_095.NewFASK_CONS_095Row();
        //            ta_cons_095.FillByAlter_ITEMNMBT(dt_cons_095, _vpp.ITEMNMBR, item.alter, row_vybrany.ITEMNMBR);

        //            //row = dt_cons_095[0];
        //            //dt_cons_095.ImportRow(row);

        //            if (dt_cons_095.Count == 1)
        //                FillDatasetMaterialy(dt_cons_095);
        //            else
        //                return;


        //        }
        //        PriznakNacteneMaterialy = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
		//        Logging.Log.Write(ex);
        //    }
        //}


        /// <summary>
        /// Nacteni materialu Verze i s polotvarama
        /// </summary>
        //private void LoadMaterialy2()
        //{
        //    try
        //    {
        //        //Table adapter pro vayebni tabulku
        //        Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter taTP = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
        //        taTP.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf);
        //        // table asapter pro tabulku zbozi
        //        Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta_cons_095 = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
        //        ta_cons_095.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf);

        //        //datatable
        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dtTP = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_cons_095 = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();

        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_cons_095_OUT = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();
        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dtTP_ITEMNMBR = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

        //        Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow VPP_row = dtTP_ITEMNMBR.NewFASK_Vyroba_TPRow();
        //        VPP_row.ITEMNMBR_Def = _vpp.ITEMNMBR;
        //        dtTP_ITEMNMBR.AddFASK_Vyroba_TPRow(VPP_row);


        //        foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow item in dtTP_ITEMNMBR)
        //        {
        //            dtTP.Clear();
        //            taTP.Fill_Alter_By_ITEMNMBR(dtTP, item.ITEMNMBR_Def);

        //            if (dtTP.Count > 0)
        //            {
        //                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow i in dtTP)
        //                {
        //                    //dt_cons_095.Clear();
        //                    ta_cons_095.FillByALTER(dt_cons_095, _vpp.ITEMNMBR, i.alter);
        //                    Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row row_vybrany;// = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row();

        //                    if (dt_cons_095.Count == 1)
        //                    {
        //                        //Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row row = dt_cons_095_OUT.NewFASK_CONS_095Row();
        //                        row_vybrany = dt_cons_095[0];
        //                        dt_cons_095_OUT.ImportRow(row_vybrany);
        //                        continue;
        //                    }
        //                    else if (dt_cons_095.Count > 1)
        //                    {
        //                        using (FormMaterialAlternaceVyber mat_alter = new FormMaterialAlternaceVyber())
        //                        {
        //                            mat_alter.dt_Material = dt_cons_095;

        //                            if (mat_alter.ShowDialog() == DialogResult.Cancel)
        //                            {
        //                                continue;
        //                            }
        //                            row_vybrany = mat_alter.MaterialSelected;
        //                            dt_cons_095_OUT.ImportRow(row_vybrany);
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }

        //            dtTP.Clear();
        //            taTP.Fill_AlterIsNull(dtTP, item.ITEMNMBR_Def);
        //            if (dtTP.Count > 0)
        //            {
        //                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow search in dtTP)
        //                {
        //                    int? cnt = taTP.ScalarCOUNT_IDH_is_IDL(search.ID_L);
        //                    if (cnt > 0)
        //                    {
        //                        Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow itemnmbr_row = dtTP_ITEMNMBR.NewFASK_Vyroba_TPRow();
        //                        itemnmbr_row.ITEMNMBR_Def = search.ITEMNMBR_Def;
        //                        dtTP_ITEMNMBR.AddFASK_Vyroba_TPRow(itemnmbr_row);
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        ta_cons_095.FillByAlter_ITEMNMBT(dt_cons_095, search.ITEMNMBR_Def, null, search.ITEMNMBR_fol);
        //                        if(dt_cons_095.Count == 1)
        //                            dt_cons_095_OUT.ImportRow(dt_cons_095[0]);

        //                        continue;
        //                    }
        //                }
        //            }
        //            else
        //                continue;
        //        }

        //        FillDatasetMaterialy(dt_cons_095_OUT);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
		//        Logging.Log.Write(ex);
        //    }

        //}

        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter tatp;
        /// <summary>
        /// pomoci rekurze ...
        /// </summary>
        private void LoadMaterialy3(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky)
        {
        
            if (vyrobky.Count() == 0)
            {
                // TODO : upravit hlaseni error ... 
                MessageBox.Show("Nenalezeno ... ");
            }
            else if (vyrobky.Count() == 1)
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

					string tmpKoef = string.IsNullOrEmpty(rUp.koef) ? string.Empty :  rUp.koef.Trim();

					if(tmpKoef.Contains(","))
						tmpKoef = tmpKoef.Replace(",", ".");

					koefUp = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
				}
                catch
				{ }

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
                    mnozstvi = materialparams.KoeficientKumulovany * koeficient * PocetOdvedeno;

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
    }
}





