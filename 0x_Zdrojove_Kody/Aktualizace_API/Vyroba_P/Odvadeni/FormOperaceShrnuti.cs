using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JR.Utils.GUI.Forms;
using Fask.Aktualizace_API.Extensions;

namespace Fask.Aktualizace_API.Odvadeni
{
    public partial class FormOperaceShrnuti : Form
    {
        //private Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter _lstoperuserta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
        private DateTime? lastoperationuserdt = null;

        private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow;
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow
        {
            get { return _productionRow; }
            set
            {
                _productionRow = value;
                //UpdateForm();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _pracovnik; }
            set
            {
                _pracovnik = value;
                //UpdateForm();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow _machine;
        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
                //UpdateForm();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow _vpp;
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VPP
        {
            get { return _vpp; }
            set
            {
                _vpp = value;
                //UpdateForm();
            }
        }

        public decimal ZbyvaKusu { get; set; }

        public FormOperaceShrnuti()
        {
            InitializeComponent();

            this.Width = Settings.FormPotvrzeniWidth;
            this.Height = Settings.FormPotvrzeniHeight;
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormOperaceShrnuti_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            //delegateUpdateForm = new DelegateUpdateForm(UpdateForm);
            //_lstoperuserta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
            //timerDateTimeOperaceUpdate.Enabled = true;

            //this.label1.Focus();
            UpdateForm();
        }

        private void FormOperaceShrnuti_KeyDown(object sender, KeyEventArgs e)
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

        public void PerformCancel()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            //TimeSpan celkovycas = TimeSpan.Parse(labelCelkovyCas.Text);
            //if (celkovycas < TimeSpan.Zero)
            //{
            //    DialogResult dr = FlexibleMessageBox.Show("Celkový čas je menší než " + TimeSpan.Zero.ToString() + "\n\nOpravdu chcete odvést výrobu?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            //    if (dr == DialogResult.No) return;
            //}
            //this.finalize();
            this.DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateForm()
        {

            //labelKusu.BackColor = SystemColors.Control;

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
                try
                {
                    TimeSpan ts = TimeSpan.FromMinutes(_productionRow.TIMEPREP); //new TimeSpan(0, _vpp.TIMEPREP, 0).ToString();
                    labelPripravnyCas.Text = ts.ToStringHHmmss();
                }
                catch 
                {
                    labelPripravnyCas.Text = "00:00:00";
                }

                try
                {
                    TimeSpan ts = TimeSpan.FromMinutes(_productionRow.TIMEUNIT); //new TimeSpan(0, _vpp.TIMEUNIT, 0).ToString();
                    labelJednotkovyCas.Text = ts.ToStringHHmmss();
                    labelVyrobniPrikaz.Text = _productionRow.SOPNUMBE.Trim();
                }
                catch { }
                //try
                //{
                //    labelKusu.Text = _productionRow.qty.ToString("0.####");
                //}
                //catch { }

                //Kontrola pre/pod teceni o 1 rad...
                //double radproduction = Math.Log10(Convert.ToDouble(_productionRow.qty));
                //double radpredloha = Math.Log10(Convert.ToDouble(PocetZbyva));
                //if (Math.Abs(radproduction - radpredloha) < 1) //rad prekrocen...
                //{
                //if (!Mathematics.Checks.KontrolaRadu(Convert.ToDouble(_productionRow.qty), Convert.ToDouble(PocetZbyva)))
                //{ //Rad je rozdilny ...
                //    labelKusu.BackColor = Color.MistyRose;
                //    labelKusu.Text += " (" + PocetZbyva.ToString("0.####") + ")";
                //}

                try
                {
                    TimeSpan ts = TimeSpan.FromMinutes(_productionRow.IsTIMECORNull() ? 0 : _productionRow.TIMECOR); //new TimeSpan(0,_productionRow.TIMECOR, 0).ToString();
                    labelKorekceCasu.Text = ts.ToStringHHmmss();
                }
                catch { }

                try
                {
                    labelStartCas.Text = _productionRow.IsTIMESTARTNull() ? "0" : _productionRow.TIMESTART.ToLongTimeString();
                }
                catch { }
                try
                {
                    labelStopCas.Text = _productionRow.IsTIMESTOPNull() ? "0" : _productionRow.TIMESTOP.ToLongTimeString();
                }
                catch { }

                try
                {
                    //labelCelkovyCas.Text = (_productionRow.TIMESTOP - Settings.LastProductionDateTime).ToString();                    
                    TimeSpan ts =
                        (_productionRow.TIMESTOP
                        - (lastoperationuserdt ?? Settings.LastProductionDateTime)
                        - (_productionRow.IsTIMECORNull() ? TimeSpan.FromMinutes(0) : TimeSpan.FromMinutes(_productionRow.TIMECOR)));
                    //labelCelkovyCas.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
                    labelCelkovyCas.Text = ts.ToStringHHmmss();
                }
                catch 
                {
                    labelCelkovyCas.Text = "0";
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            Handle_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            Handle_KeyDown(null, new KeyEventArgs(Keys.Escape));
        }
    }
}
