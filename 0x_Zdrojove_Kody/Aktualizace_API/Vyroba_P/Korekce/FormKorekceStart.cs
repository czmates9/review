using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Aktualizace_API.MySystem;
using Fask.Aktualizace_API.Extensions;
using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;
using System.Linq;

namespace Fask.Aktualizace_API.Korekce
{
    public partial class FormKorekceStart : Form
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


        public FormKorekceStart()
        {
            InitializeComponent();

            //crrta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
            //crrta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
        }


        private void FormBaseButtonOKStorno_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            FillCorrects();
            UpdateFormProduction();
            UpdateForm();
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
            if (_correction == null)
            {
                return;
            }

            DateTime dtnow = DateTime.Now;
            _productionRow.TIMECRID = _correction.id;
            //_productionRow.TIMECOR = Convert.ToSingle(tsDelka.TotalMinutes);
            //_productionRow.TIMECORSTOP = dtStop;
            _productionRow.TIMECORSTART = dtnow;
            _productionRow.dateeve = dtnow;

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
            try
            {
                // pripravit table layout panel pro korekce ...

                crrdt = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByProduction_Corrects(0);
                //foreach (Data.VyrobaCEDataSet.CorrectsRow crrrow in crrdt)
                //{
                //    cbDuvod.Items.Add(crrrow);
                //}
                var corrections = crrdt.Select(x => x);
                int oCount = corrections.Count();
                int nN = (int)Math.Ceiling(Math.Sqrt(oCount));
                
                tlCorrections.Controls.Clear();
                tlCorrections.RowCount = nN;
                tlCorrections.ColumnCount = nN;

                tlCorrections.RowStyles.Clear();
                for (int i = 0; i < nN; i++)
                {
                    tlCorrections.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / nN));
                }
                tlCorrections.ColumnStyles.Clear();
                for (int i = 0; i < nN; i++)
                {
                    tlCorrections.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / nN));
                }

                //foreach (RowStyle itemrow in tlOperations.RowStyles)
                //{
                //    itemrow.SizeType = SizeType.Percent;
                //    itemrow.Height = 100F / nN;
                //}

                //foreach (ColumnStyle itemrow in tlOperations.ColumnStyles)
                //{
                //    itemrow.SizeType = SizeType.Percent;
                //    itemrow.Width = 100F / nN;
                //}

                int nR, nC;
                nR = nC = 0;
                foreach (var item in corrections.ToList())
                {
                    Button b = new Button();
                    b.Text = item.desc.Trim();
                    b.Parent = tlCorrections;
                    b.Dock = DockStyle.Fill;
                    b.Click += new EventHandler(b_Click);
                    b.Tag = item;
                    tlCorrections.SetColumn(b, nC);
                    tlCorrections.SetRow(b, nR);

                    nC++;
                    if (nC >= nN)
                    {
                        nC = 0;
                        nR++;
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow correction = b.Tag as Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow;

            if (correction == null)
            {
                FlexibleMessageBox.Show(this, "Korekce není nastavena!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Correction = correction;

            this.PerformOK();
        }

        private void UpdateForm()
        {
        }

        private void UpdateFormProduction()
        {
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
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
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;
            
            string data = e.BarcodeData;

            var corrections = crrdt.Where(s => s.desc == data);
            if (corrections.Count() > 0)
            {
                this.Correction = corrections.First();
            }
            else
            {
                this.Correction = null;
            }            
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

    }
}

