using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.MySystem;

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormKorekceCasu : Form
    {

		//private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter crrta = null;
		private Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable crrdt = null;

		private Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow _correction;
		public Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow Correction
        {
            get { return _correction; }
            set
            {
                _correction = value;
                UpdateForm();
            }
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



        public FormKorekceCasu()
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

        private void FormBaseButtonOKStorno_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            dateTimePickerDelka.Value = DateTime.Today;
            FillCorrects();
            UpdateFormProduction();
            UpdateForm();
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
            if (_correction == null)
            {
                if (MessageBox.Show("Není vybrán dùvod korekce\n\nChcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                    return;
                //_productionRow.TIMECRID = int.MinValue;
                _productionRow.SetTIMECRIDNull();
                //_productionRow.TIMECOR = 0;
                _productionRow.SetTIMECORNull();
                _productionRow.SetTIMECORSTARTNull();
                _productionRow.SetTIMECORSTOPNull();
            }
            else
            {
                _productionRow.TIMECRID = _correction.id;
                //_productionRow.TIMECOR = Convert.ToInt32(((TimeSpan)(dateTimePickerKonec.Value - dateTimePickerZacatek.Value)).TotalMinutes);
                _productionRow.TIMECOR = Convert.ToSingle(((TimeSpan)(dateTimePickerDelka.Value - DateTime.Today)).TotalMinutes);
                _productionRow.TIMECORSTOP = DateTime.Now;
                _productionRow.TIMECORSTART = _productionRow.TIMECORSTOP - TimeSpan.FromMinutes(_productionRow.TIMECOR);
            }

            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
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
				crrdt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByProduction_Corrects(1);
				foreach (Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow crrrow in crrdt)
                {
                    cbDuvod.Items.Add(crrrow);
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, this.Text);
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
                    if (_productionRow.IsTIMECRIDNull())
                        return;
                    DataRow[] rows = crrdt.Select("id=" + _productionRow.TIMECRID);
                    if (rows.Length == 0) {
                        cbDuvod.SelectedItem = null;
                        return;
                    }

					Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow crrrow = rows[0] as Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow;
                    cbDuvod.SelectedItem = crrrow;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, this.Text);
                }
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

