using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JR.Utils.GUI.Forms;
using Fask.Vyroba_P.Extensions;
using System.Linq;

namespace Fask.Vyroba_P.Odvadeni
{
    public partial class FormOperacePotvrzeni2 : Form
    {
        private Korekce.KorekceCasy _korekceCasy = new Fask.Vyroba_P.Korekce.KorekceCasy();

        //private Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter _lstoperuserta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
        private DateTime? lastoperationuserdt = null;

        private List<Korekce.Korekce> _korekce = new List<Fask.Vyroba_P.Korekce.Korekce>();
        /// <summary>
        /// Seznam nadefinovanych korekci ...
        /// </summary>
        public List<Korekce.Korekce> Korekce
        {
            get { return _korekce; }
            set { _korekce = value; }
        }

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

        [DefaultValue(0)]
        public decimal PocetZbyva { get; set; }


        public decimal ZbyvaKusu { get; set; }
        
        public FormOperacePotvrzeni2()
        {
            InitializeComponent();

            this.Width = Settings.FormPotvrzeniWidth;
            this.Height = Settings.FormPotvrzeniHeight;
            //this.Size = Settings.FormOperacePotvrzeniSize;

            panelDetail2Casy.Visible = 
                buttonKorekce.Enabled = 
                buttonKorekce.Visible = Settings.OdvadeniZobrazovatCasy;
        }

        private void FormOperacePotvrzeni2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            panelButtons_Resize(null, null);

            delegateUpdateForm = new DelegateUpdateForm(UpdateForm);
            //_lstoperuserta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
            timerDateTimeOperaceUpdate.Enabled = true;

            this.label1.Focus();
            //timerDateTimeOperaceUpdate_Tick(null, null); //okamzite spocitani dat pro zobrazeni ...
            UpdateForm();
            if (Settings.CorrectsEnable)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    KorekceAutoShow();
                });
            }
            else
            {
                buttonKorekce.Enabled = Settings.CorrectsEnable;
            }
        }

        private void KorekceAutoShow()
        {
            try
            {
                _korekceCasy.Validate();

                // Pokud neni nastaveno rucni zadani korekce uspora, tak se automaticky vlozi 1. vyskyt uspory bez zobrazeni
                if (!Settings.CorrectsUsporaZadaniEnable && (_korekceCasy.lTSRozdilOdNormy < TimeSpan.Zero))
                {
                    KorekceAutoAddUspora();
                    return;
                }
            }
            //catch (Exception ex)
            catch (Exception ex)
            {
                // Pokud neni nastaveno rucni zadani korekce uspora, tak se automaticky vlozi 1. vyskyt uspory bez zobrazeni
                if (ex is Korekce.KorekceExceptionUspora)
                {
                    if (!Settings.CorrectsUsporaZadaniEnable)
                    {
                        KorekceAutoAddUspora();
                        return;
                    }
                }

                {
                    // pokud vyjimka, tak se nepodarila validace ... 
                    buttonKorekce_Click(null, null);
                }
            }
        }

        private void KorekceAutoAddUspora()
        {
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter crrta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
            //crrta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable crrdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByProduction_Corrects(1);
            //var crrs = crrdt.Where(c => _correctionType.HasValue ? _correctionType.Value == c.ProductionType : c.IsProductionTypeNull());
            //var crrs = crrdt.Where(Func<Fask.Vyroba_P.Data.VyrobaCEDataSet.CorrectsRow,bool>delegate(Fask.Vyroba_P.Data.VyrobaCEDataSet.CorrectsRow){});
            var crrs = crrdt.Where(x => (!x.IsProductionTypeNull() ? x.ProductionType : 0) == (1)); // hledam korekce typu uspora ...
            if (crrs.Count() == 0) // Neexistuje zadna korekce typu "Uspora" => co s tim ???
            { // zatim to necham zobrazit pomoci buttonKorekce_Click ...
                buttonKorekce_Click(null, null);
            }
            else // Existuje nejaka korekce typu uspora
            {
                var crr = crrs.First();
                // vytvorit korekci a ulozit ...
                TimeSpan crrDelka = this._korekceCasy.lTSRozdilOdNormy;
                if (crrDelka < TimeSpan.Zero)
                    crrDelka = crrDelka.Negate();

                Fask.Vyroba_P.Korekce.Korekce kuspora = new Fask.Vyroba_P.Korekce.Korekce(
                    crr.id,
                    crr.desc,
                    this._productionRow.TIMEMODE == 0 ? (lastoperationuserdt ?? Settings.LastProductionDateTime) : this._productionRow.TIMESTART,
                    crrDelka,
                    1);
                this._korekce.Add(kuspora);

                this.finalize();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FormOperacePotvrzeni2_KeyDown(object sender, KeyEventArgs e)
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
                else if (e.KeyCode == Keys.F1)
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
            if (Settings.CorrectsEnable)
            {
                //TimeSpan celkovycas = TimeSpan.Parse(labelCelkovyCas.Text);
                if (_korekceCasy.lTSCelkovyCas < TimeSpan.Zero)
                {
                    DialogResult dr = FlexibleMessageBox.Show(this, "Celkový èas je menší než " + TimeSpan.Zero.ToString() + "\n\nOpravdu chcete odvést výrobu?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No) return;
                }


                // Validate vyhazuje vyjimky a tady se na to reaguje zobrazenim textu vyjimky ...
                try
                {
                    if (!_korekceCasy.Validate())
                        return;
                }
                catch (Exception ex)
                {
                    DialogResult drkorekce = FlexibleMessageBox.Show(this,
                        ex.Message + "\nZadat korekce?",
                        this.Text + " (Validace korekcí)",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1
                        );
                    if (drkorekce == DialogResult.Yes)
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.KorekceAutoShow();
                        });

                    return;
                }
            }

            this.finalize();
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
            Settings.FormPotvrzeniHeight = this.Height;
            Settings.FormPotvrzeniWidth = this.Width;
            //Settings.FormOperacePotvrzeniSize = this.Size;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Handle_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            Handle_KeyDown(null, new KeyEventArgs(Keys.Escape));
        }

        delegate void DelegateUpdateForm();
        private void UpdateForm()
        {
            labelKusu.BackColor = SystemColors.Control;
            labelRozdilOdNormy.BackColor = SystemColors.Control;

            try
            {
                if (lastoperationuserdt == null)
                    //lastoperationuserdt = _lstoperuserta.GetLastOperationDateTime(_pracovnik.id);
                    lastoperationuserdt = Fask.SQLiteDBs.DataSets.InternalState.GetInternalStateLstOperationUser(_pracovnik.id);
            }
            catch
            {
            }

            try
            {
                try
                {
                    labelPracovnik.Text = _pracovnik.surname.Trim() + " " + _pracovnik.firstname.Trim();
                }
                catch { }
                try
                {
                    labelZbyvaKusu.Text = ZbyvaKusu.ToString("0.");
                }
                catch { }
                try
                {
                    labelStroj.Text = _machine == null ? string.Empty : _machine.description.Trim();
                }
                catch { }
                try
                {
                    lblPolozka.Text = _vpp == null || _vpp.IsITEMDESCNull() ? "?" : _vpp.ITEMDESC.Trim();
                }
                catch { }

                labelVyrobniPrikaz.Text = _productionRow.SOPNUMBE.Trim();

                try
                {
                    labelKusu.Text = _productionRow.qty.ToString("0.####");
                }
                catch { }

                //Kontrola pre/pod teceni o 1 rad...
                //double radproduction = Math.Log10(Convert.ToDouble(_productionRow.qty));
                //double radpredloha = Math.Log10(Convert.ToDouble(PocetZbyva));
                //if (Math.Abs(radproduction - radpredloha) < 1) //rad prekrocen...
                //{
                if (!Mathematics.Checks.KontrolaRadu(Convert.ToDouble(_productionRow.qty), Convert.ToDouble(PocetZbyva)))
                { //Rad je rozdilny ...
                    labelKusu.BackColor = Color.MistyRose;
                    labelKusu.Text += " (" + PocetZbyva.ToString("0.####") + ")";
                }

                // TODO : pokud start a stop jsou v ruznych dnech zobrazit i datum?...
                try
                {
                    labelStartCas.Text = _productionRow.IsTIMESTARTNull() ? "nenastaveno" : _productionRow.TIMESTART.ToLongTimeString();
                }
                catch { }
                try
                {
                    labelStopCas.Text = _productionRow.IsTIMESTOPNull() ? "nenastaveno" : _productionRow.TIMESTOP.ToLongTimeString();
                }
                catch { }

                try
                {
                    labelPracovnikPoslCasOperace.Text = (lastoperationuserdt ?? Settings.LastProductionDateTime).ToLongTimeString();
                }
                catch { }

                // Zahajeni vypoctu

                _korekceCasy._lstOperationUser = lastoperationuserdt ?? Settings.LastProductionDateTime;
                _korekceCasy._korekce = this._korekce;
                _korekceCasy._productionRow = this._productionRow;
                _korekceCasy.Calculate();

                labelPripravnyCas.Text = _korekceCasy.lTSPripravnyCas.ToStringHHmmss();
                labelJednotkovyCas.Text = _korekceCasy.lTSJednotka.ToStringHHmmss();
                labelPredpokladanyCas.Text = _korekceCasy.lTSPredpokladNormy.ToStringHHmmss();
                labelKorekceCasu.Text = _korekceCasy.lTSKorekceSuma.ToStringHHmmss();
                labelKorekceCasu.Text += " (" + _korekce.Count + ")" +
                    " [Z:+" + _korekceCasy.lTSKorekceSumaZpozdeni.ToStringHHmmss() +
                    ", U:-" + _korekceCasy.lTSKorekceSumaUspora.ToStringHHmmss() +
                    "]";
                labelCelkovyCas.Text = _korekceCasy.lTSCelkovyCas.ToStringHHmmss();
                labelRozdilOdNormy.Text = _korekceCasy.lTSRozdilOdNormy.ToStringHHmmss();

                if (Settings.CorrectsCheckEnable)
                {
                    //if (_korekceCasy.lTSRozdilOdNormy > _korekceCasy.lTSRozdilPovolenZProcent)
                    //{
                    //    labelRozdilOdNormy.BackColor = Color.MistyRose;
                    //}
                    try
                    {
                        _korekceCasy.Validate();
                    }
                    //catch (Exception ex)
                    catch
                    {
                        if (_korekceCasy.lTSRozdilOdNormy > TimeSpan.Zero)
                            labelRozdilOdNormy.BackColor = Color.MistyRose;
                        else
                            labelRozdilOdNormy.BackColor = Color.LightGreen;
                    }

                    labelRozdilOdNormy.Text += " Max.rozdíl:" + 
                        (!Settings.CorrectsCheckPercentEnable ? string.Empty : " " + _korekceCasy.lTSRozdilPovolenZProcent.ToStringHHmmss() + " (" + Settings.CorrectsCheckPercentValue + "%)") +
                        (!Settings.CorrectsCheckMinimumEnable ? string.Empty : " " + _korekceCasy.lTSRozdilPovolenZMinimum.ToStringHHmmss())
                        ;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void buttonKorekce_Click(object sender, EventArgs e)
        {
            if (!Settings.CorrectsEnable)
            {
                FlexibleMessageBox.Show("Není povoleno zadávat korekce", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult drKokekce = DialogResult.None;

            if (!Settings.OdvadeniZobrazovatCasy)
                return;

            try
            {
                FormKorekceCasu2 frmkorekcecasu = new FormKorekceCasu2();
                frmkorekcecasu.ProductionRow = _productionRow;
                // TODO : upravit test na typ korekce ...
                frmkorekcecasu.CorrectionType = _korekceCasy.lTSRozdilOdNormy > TimeSpan.Zero ? 0 : 1;
                frmkorekcecasu.Korekce = this._korekce; //je odkaz na objekt ... form toto plni samostane...
                frmkorekcecasu.LastOperationUser = lastoperationuserdt ?? Settings.LastProductionDateTime;
                drKokekce = frmkorekcecasu.ShowDialog(this);
                //if (frmkorekcecasu.ShowDialog(this) == DialogResult.OK)
                //{
                //    this._korekce = frmkorekcecasu.Korekce;
                //}
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
            }

            this.UpdateForm();

            if (drKokekce != DialogResult.Cancel)
            {
                // show auto korekce ...
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    KorekceAutoShow();
                });
            }
        }

        private void timerDateTimeOperaceUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_productionRow != null)
                {
                    _productionRow.TIMESTOP = DateTime.Now;
                }

                this.BeginInvoke(delegateUpdateForm);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Rozpad materialu
            using (Materialy.FormMaterial frmMaterial = new Materialy.FormMaterial())
            {
                frmMaterial.ProductionRow = this._productionRow;
                frmMaterial.ProductionSDT = this._productionSDT;
                frmMaterial.mnozstviVyrobku = this._productionRow.qty;
                frmMaterial.types = Materialy.ShowTypes.Back;
                if (frmMaterial.ShowDialog() == DialogResult.Cancel)
                {
                    //return DialogResult.Cancel;
                }
            }
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            if (Settings.Production_Material_Enter && Settings.Production_Material_OperacePotvrzeniButton)
            {
                Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
                buttonStorno.Size = nsize;
                buttonmaterial.Size = buttonOK.Size;
            }
            else
            {
                buttonmaterial.Visible = false;
                Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
                buttonStorno.Size = nsize;

            }
        }
    }
}

