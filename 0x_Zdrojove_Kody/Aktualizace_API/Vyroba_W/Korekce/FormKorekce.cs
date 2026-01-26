using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.MySystem;
using Fask.Vyroba_W.Extensions;

namespace Fask.Vyroba_W.Korekce
{
    public partial class FormKorekce : Form
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

        /// <summary>
        /// Minimalni cas korekce, pod kterou nesmi klesnout ...
        /// </summary>
        private DateTime? _corretionMinimumDateTime = null;
        /// <summary>
        /// Minimalni cas korekce, pod kterou nesmi klesnout ...
        /// </summary>
        public DateTime? CorretionMinimumDateTime
        {
            get { return _corretionMinimumDateTime; }
            set { _corretionMinimumDateTime = value; }
        }


        public FormKorekce()
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
                MessageBox.Show("Není vybrán dùvod korekce.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;

                //if (MessageBox.Show("Není vybrán dùvod korekce\n\nChcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                //    == DialogResult.No)
                //    return;
                ////_productionRow.TIMECRID = int.MinValue;
                //_productionRow.SetTIMECRIDNull();
                ////_productionRow.TIMECOR = 0;
                //_productionRow.SetTIMECORNull();
                //_productionRow.SetTIMECORSTARTNull();
                //_productionRow.SetTIMECORSTOPNull();
            }
            else
            {
                TimeSpan tsDelka = dateTimePickerDelka.Value - DateTime.Today;
                if (tsDelka.TotalMinutes <= 0)
                {
                    MessageBox.Show("Délka korekce nesmí být 0 minut!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    return;
                }

                DateTime dtStop = DateTime.Now;
                DateTime dtStart = dtStop - tsDelka;

                if (CorretionMinimumDateTime.HasValue) //test na minimum korekce
                {
                    if (dtStart < CorretionMinimumDateTime)
                    {
                        MessageBox.Show(
                            "Délka korekce je vìtší než je povoleno!\n"+
                            " Maximum: " + ((TimeSpan)(DateTime.Now - CorretionMinimumDateTime.Value)).ToStringHHmm()
                            , "Korekce"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Exclamation
                            , MessageBoxDefaultButton.Button1
                            );
                        return;
                    }
                }

                _productionRow.TIMECRID = _correction.id;
                //_productionRow.TIMECOR = Convert.ToInt32(((TimeSpan)(dateTimePickerKonec.Value - dateTimePickerZacatek.Value)).TotalMinutes);
                _productionRow.TIMECOR = Convert.ToSingle(tsDelka.TotalMinutes);
                _productionRow.TIMECORSTOP = dtStop;
                _productionRow.TIMECORSTART = dtStart;
                _productionRow.dateeve = DateTime.Now;
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
                crrdt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByProduction_Corrects(0);
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
            //if (_productionRow != null)
            //{
            //    try
            //    {
            //        dateTimePickerDelka.Value = DateTime.Today + TimeSpan.FromMinutes(_productionRow.TIMECOR);
            //    }
            //    catch { }

            //    try
            //    {
            //        if (_productionRow.IsTIMECRIDNull())
            //            return;
            //        DataRow[] rows = crrdt.Select("id=" + _productionRow.TIMECRID);
            //        if (rows.Length == 0) {
            //            cbDuvod.SelectedItem = null;
            //            return;
            //        }

            //        Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow crrrow = rows[0] as Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow;
            //        cbDuvod.SelectedItem = crrrow;
            //    }
            //    catch (Exception ex)
            //    {
			//        Logging.Log.Write(ex.Message, this.Text);
            //    }
            //}

            if (CorretionMinimumDateTime.HasValue)
            {
                try
                {
                    dateTimePickerDelka.Value = DateTime.Today + (DateTime.Now - CorretionMinimumDateTime.Value);
                }
                catch (Exception ex)
                {
					Logging.Log.Write(ex);
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

