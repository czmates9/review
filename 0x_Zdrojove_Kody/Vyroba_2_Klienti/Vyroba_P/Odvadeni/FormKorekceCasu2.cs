using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_P.MySystem;
using Fask.Vyroba_P.Extensions;
using Fask.Vyroba_P.Forms;
using JR.Utils.GUI.Forms;
using System.Linq;
using Fask.Vyroba_P.Korekce;

namespace Fask.Vyroba_P.Odvadeni
{
    public partial class FormKorekceCasu2 : Form
    {       

        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter crrta = null;
        private Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable crrdt = null;

        public DateTime LastOperationUser { get; set; }

        private Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow _correction;
        //public Data.VyrobaCEDataSet.CorrectsRow Correction
        //{
        //    get { return _correction; }
        //    set
        //    {
        //        _correction = value;
        //        UpdateForm();
        //    }
        //}
        /// <summary>
        /// Typ korekci k zobrazeni (NULL, 0=zpozdeni, 1=uspora)
        /// </summary>
        private int? _correctionType = null;
        /// <summary>
        /// Typ korekce k zobrazeni (NULL, 0=zpozdeni, 1=uspora)
        /// </summary>
        /// <remarks>Muze byt zmeneno, lokalne pokud dojde k prelomu mezi -K > +K</remarks>
        public int? CorrectionType
        {
            get { return _correctionType; }
            set
            {
                _correctionType = value;
                FillCorrects();
                UpdateFormProduction();
                UpdateForm();
            }
        }

        public Korekce.KorekceCasy _korekceCasy = null;

        private System.Collections.Generic.List<Korekce.Korekce> _korekce = new List<Fask.Vyroba_P.Korekce.Korekce>();
        public System.Collections.Generic.List<Korekce.Korekce> Korekce
        {
            get { return _korekce; }
            set { _korekce = value; }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow;
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow
        {
            get { return _productionRow; }
            set
            {
                _productionRow = value;
                FillCorrects();
                UpdateFormProduction();
                UpdateForm();
            }
        }



        public FormKorekceCasu2()
        {
            InitializeComponent();

            //crrta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
            //crrta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormKorekceCasu2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            dateTimePickerDelka.Value = DateTime.Today;
            
            FillCorrects();
            UpdateFormProduction();
            UpdateForm();
            UpdateDelkaKorekce();
            ScannerStart();
        }

        private void FormBaseButtonOKStorno_KeyDown(object sender, KeyEventArgs e)
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
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        public void PerformOK()
        {
            //1.9.2016 JiS - korekce se nenastavi zde, ale v nadrezenem formu.
            // zde se provede pouze verifikace zadane korekce ...

            //if (_correction == null)
            //{
            //    if (FlexibleMessageBox.Show("Není vybrán dùvod korekce\n\nChcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            //        == DialogResult.No)
            //        return;
            //    //_productionRow.TIMECRID = int.MinValue;
            //    _productionRow.SetTIMECRIDNull();
            //    //_productionRow.TIMECOR = 0;
            //    _productionRow.SetTIMECORNull();
            //    _productionRow.SetTIMECORSTARTNull();
            //    _productionRow.SetTIMECORSTOPNull();
            //}
            //else
            //{
            //    _productionRow.TIMECRID = _correction.id;
            //    //_productionRow.TIMECOR = Convert.ToInt32(((TimeSpan)(dateTimePickerKonec.Value - dateTimePickerZacatek.Value)).TotalMinutes);
            //    _productionRow.TIMECOR = Convert.ToSingle(((TimeSpan)(dateTimePickerDelka.Value - DateTime.Today)).TotalMinutes);
            //    _productionRow.TIMECORSTOP = DateTime.Now;
            //    _productionRow.TIMECORSTART = _productionRow.TIMECORSTOP - TimeSpan.FromMinutes(_productionRow.TIMECOR);
            //}

            if (_correction == null)
            {
                FlexibleMessageBox.Show(this, "Není vybrán dùvod korekce", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                return;
            }

            // test na delku zadane korekce ... (musi byt vetsi nez 0!)
            if ((dateTimePickerDelka.Value - DateTime.Today) <= TimeSpan.FromMinutes(0))
            {
                FlexibleMessageBox.Show(this, "Délka korekce musí být zadána!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            #region Test na delky korekci ...

            //DateTime lKorekceStartLast = DateTime.Now;
            DateTime lKorekceStartLast = DateTime.Now; // toto je nize spravne nahrazeno ...
            // 12.10.2016 JiS
            // Delky korekci je nutne pocitat ruzne pro Timemode=0 a timemode=[1,2]
            if (this._productionRow.TIMEMODE == 0)
                lKorekceStartLast = this.LastOperationUser;
            else if ((this._productionRow.TIMEMODE == 1) || (this._productionRow.TIMEMODE == 2))
            {
                lKorekceStartLast = this._productionRow.TIMESTART;
            }

            // 27.9.2016 JiS : korekce typu uspora nejsou zohledneny pri testu na stop
            // => protoze uspory mohou byt vetsi nez je celkova delka operace
            var lkorekceorder = _korekce.Where(x => (x.type ?? 0) == 0).OrderByDescending(x => x.stop);
            if (lkorekceorder.Count() > 0)
            {
                lKorekceStartLast = lkorekceorder.First().stop;
            }

            // test aby casy neprekrocily hranice odvodu...
            DateTime lStart = lKorekceStartLast;
            DateTime lStop = lKorekceStartLast + (dateTimePickerDelka.Value - DateTime.Today);

            // test typu korekci 
            // a) (NULL, 0=zpozdeni) : pak provest kontrolu na delku korekci a radit je za sebe
            // b) (1=uspora) : kontrolu neprovadet, budou vzdy zadany od posledni akce uzivatele

            if (_correction.IsProductionTypeNull() || _correction.ProductionType == 0)
            {
                if (_productionRow.TIMESTOP < lStop)
                {
                    FlexibleMessageBox.Show(this, "Korekce je vìtší než STOP operace!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (_correction.ProductionType == 1)
            {
                // usporu necham vzdy pridat ...
            }
            else
            {
                // jinak ji necham pridat ...
            }

            _korekce.Add(new Fask.Vyroba_P.Korekce.Korekce(_correction.id, _correction.desc, lStart, lStop, _correction.IsProductionTypeNull() ? null : (int?)_correction.ProductionType, textBoxNote.Text));
            #endregion

            ScannerStop();
            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            ScannerStop();
            this.DialogResult = DialogResult.Cancel;
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

        private void FillCorrects()
        {
            cbDuvod.BeginUpdate();
            cbDuvod.Items.Clear();

            try
            {
                crrdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByProduction_Corrects(1);
                //var crrs = crrdt.Where(c => _correctionType.HasValue ? _correctionType.Value == c.ProductionType : c.IsProductionTypeNull());
                //var crrs = crrdt.Where(Func<Fask.Vyroba_P.Data.VyrobaCEDataSet.CorrectsRow,bool>delegate(Fask.Vyroba_P.Data.VyrobaCEDataSet.CorrectsRow){});
                var crrs = crrdt.Where(x => (!x.IsProductionTypeNull() ? x.ProductionType : 0) == (!_correctionType.HasValue ? 0 : _correctionType.Value));
                //foreach (Data.VyrobaCEDataSet.CorrectsRow crrrow in crrdt)
                foreach (var crrrow in crrs)
                {
                    cbDuvod.Items.Add(crrrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            cbDuvod.EndUpdate();
        }

        private void cbDuvod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Zmena(nastaveni) korekce casu
            _correction = cbDuvod.SelectedItem as Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow;
            UpdateForm();
        }

        private void UpdateForm()
        {
            try { dateTimePickerDelka.MaxDate = DateTime.Today + new TimeSpan(0, 23, 59, 59, 999); }
            catch { }
            try { dateTimePickerDelka.MinDate = DateTime.Today; }
            catch { }

            if (_correction != null)
            {
                if (!_correction.IsTMFromNull())
                {
                    dateTimePickerDelka.MinDate = DateTime.Today + TimeSpan.FromMinutes(_correction.TMFrom);
                }

                if (!_correction.IsTMToNull())
                {
                    dateTimePickerDelka.MaxDate = DateTime.Today + TimeSpan.FromMinutes(_correction.TMTo);
                }
            }
        }
    
        private void UpdateFormProduction()
        {
            if (_productionRow != null)
            {
                try
                {
                    dateTimePickerDelka.Value = DateTime.Today + TimeSpan.FromMinutes(_productionRow.TIMECOR);
                }
                catch { }

                try
                {
                    if (_korekce.Count == 0) // nic se nedeje ...
                        return;

                    // 26.9.2016 JiS / zmena zpusobu zadavani korekci...
                    // ani pri timemode=0 nevybirat korekci => je mozne jich zadavat vice za sebou ...

                    //if (_productionRow.TIMEMODE == 0) // 0 = stop 
                    //{
                    //    DataRow[] rows = crrdt.Select("id=" + _korekce[0].id);
                    //    if (rows.Length == 0)
                    //    {
                    //        cbDuvod.SelectedItem = null;
                    //        return;
                    //    }

                    //    var crrrows = crrdt.Where(x => x.id == _korekce[0].id);
                    //    if (crrrows.Count() > 0)
                    //    {
                    //        cbDuvod.SelectedItem = crrrows.First();
                    //    }
                    //}
                    //else
                    //{ // v ostatnich pripadech se nenastavuje ...
                    //}
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            string data = e.BarcodeData;

            var corrections = crrdt.Where(s => s.desc == data);
            if (corrections.Count() > 0)
            {
                cbDuvod.SelectedItem = corrections.First();
                dateTimePickerDelka.Focus();
            }
            else
            {
                cbDuvod.SelectedItem = null;
                cbDuvod.Focus();
            }

            //// najití indexu v combo boxu a následné nastavení focusu
            //int index = cbDuvod.FindStringExact(data);
            //if (index != -1)
            //{
            //    cbDuvod.SelectedIndex = index;
            //    dateTimePickerDelka.Focus();
            //}
            //else
            //{
            //    cbDuvod.SelectedIndex = -1;
            //    cbDuvod.Focus();
            //}
        }

        void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void timerAktualizaceDelkyKorekce_Tick(object sender, EventArgs e)
        {
            UpdateDelkaKorekce();
        }

        private void UpdateDelkaKorekce()
        {
            try
            {
                if (this._correction != null)
                    return;

                if (_korekceCasy == null)
                {
                    _korekceCasy = new KorekceCasy();
                    _korekceCasy._korekce = _korekce;
                    _korekceCasy._lstOperationUser = this.LastOperationUser;
                    _korekceCasy._productionRow = this.ProductionRow;                    
                }
                _korekceCasy.Calculate();

                //if (_productionRow != null)
                //{
                //    //_productionRow.TIMESTOP = DateTime.Now; <= toto stale nastavuje timer v predchozim formu ...
                //    TimeSpan lTSDelka = _productionRow.TIMESTOP - this.LastOperationUser;
                //    //TimeSpan lTSDelkaKorekci = TimeSpan.Zero;
                //    //this._korekce.ForEach(x => lTSDelkaKorekci = lTSDelkaKorekci + x.delka);
                //    TimeSpan lTSDelkaKorekci = _korekce.SumKorekce();
                //    TimeSpan lTSRozdilOdNormy = (lTSDelka - lTSDelkaKorekci) - TimeSpan.FromMinutes(_productionRow.TIMEUNIT * Convert.ToDouble(_productionRow.qty));
                //    dateTimePickerDelka.Value = DateTime.Today + ((lTSRozdilOdNormy < TimeSpan.Zero) ? lTSRozdilOdNormy.Negate() : lTSRozdilOdNormy);
                //}

                dateTimePickerDelka.Value = DateTime.Today + ((_korekceCasy.lTSRozdilOdNormy < TimeSpan.Zero) ? _korekceCasy.lTSRozdilOdNormy.Negate() : _korekceCasy.lTSRozdilOdNormy);

                CorrectionsCheckTypeChange();

            }
            catch 
            {
            }
        }

        private void CorrectionsCheckTypeChange()
        {
            // test na zmenu typu korekci ...
            if ((_korekceCasy.lTSRozdilOdNormy > TimeSpan.Zero) && ((CorrectionType ?? 0) != 0))
            { // CorrectionType (NULL, 0 = zpozdeni, 1=uspora)
                // pokud je rozdil od normy vetsi a correctiontype!=0, pak doslo ke zmene typu korekci ...
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.CorrectionType = 0; // nastavi se zpozdeni => automaticky dojde k natazeni novych korekci ...
                });
            }
        }

        //private void textBoxDelka_LostFocus(object sender, EventArgs e)
        //{
        //    TimeSpan time = TimeSpan.Zero;
        //    try
        //    {
        //        textBoxDelka.BackColor = SystemColors.Window;
        //        time = TimeSpan.Parse(this.textBoxDelka.Text);
        //    }
        //    catch
        //    {
        //        textBoxDelka.BackColor = Color.MistyRose;
        //        return;
        //    }

        //    try
        //    {
        //        dateTimePickerKonec.Value = dateTimePickerZacatek.Value + time;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex.Message, this.Text);
        //    }
        //}
    }
}

