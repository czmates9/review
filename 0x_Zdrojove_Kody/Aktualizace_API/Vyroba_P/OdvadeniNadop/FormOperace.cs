using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Aktualizace_API.Forms;
using System.IO;
using Fask.Aktualizace_API.Extensions;
using JR.Utils.GUI.Forms;
using System.Linq;

namespace Fask.Aktualizace_API.OdvadeniNadop
{
    public partial class FormOperace : Form
    {

        Fask.SQLiteDBs.DataSets.Vyroba dsVyroba = null;
        Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik = null;
        Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine = null;
        Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow zakazka = null;
        Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtProduction = null;

        Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow idoperation = null;
        /// <summary>
        /// Vybrana operace nebo operace z historie
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow Operace
        {
            get { return idoperation; }
            private set { idoperation = value; }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _production = null;
        /// <summary>
        /// Data produkce pro ulozeni ...
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow Production
        {
            get { return _production; }
            set { _production = value; }
        }

        //WebServiceVyroba.Vyroba vyrobaS = null;

        string textform = string.Empty;

        private void UpdateTextForm()
        {
            this.Text = textform;
            if (idpracovnik != null)
                this.Text += ", P:" + idpracovnik.ToString();

            if (idmachine != null)
                this.Text += ", M:" + idmachine.name.Trim();
        }

        private FormOperace()
        {
            InitializeComponent();

            this.textform = this.Text;

            //vyrobaS = new Fask.Vyroba_P.WebServiceVyroba.Vyroba();
            //vyrobaS.Url = Settings.WebServiceAddressVyroba + Constants.Vyroba_asmx;
            //vyrobaS.Timeout = Settings.ProductionOnlineTimeout; 

        }

        public FormOperace(
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik 
            , Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine
            , Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow zakazka
            , Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtProduction
            )
            : this()
        {
            this.idpracovnik = idpracovnik;
            this.idmachine = idmachine;
            this.zakazka = zakazka;
            this.dtProduction = dtProduction;
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            ScannerStart();

            OperationsLoad();

            CheckProductionState();

            UpdateForm();
        }

        /// <summary>
        /// Provede zjisteni aktualniho stavu zakazky a operace
        /// </summary>
        private void CheckProductionState()
        {
            try
            {
                var dtProductionByDateEveDesc = dtProduction.OrderByDescending(s => s.dateeve);
                if (dtProductionByDateEveDesc.Count() <= 0)
                    return;
                var pFirst = dtProductionByDateEveDesc.First();

                if (pFirst.SOPNUMBE != zakazka.SOPNUMBE 
                    //jina zakazka zahajena, alee neukoncena ...
                    && !pFirst.IsTIMESTARTNull() 
                    && pFirst.IsTIMESTOPNull()
                    )
                {
                    FlexibleMessageBox.Show(this, "Neukonèena zakázka: " + pFirst.SOPNUMBE.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    PerformCancel();
                    return;
                }

                if (pFirst.IsTIMESTOPNull() && !pFirst.IsTIMESTARTNull())
                {
                    _production.TIMESTART = pFirst.TIMESTART;
                    // Otevreno => umoznit pouze ukonceni / zobrazit odpovidajici operaci
                    foreach (Button b in tlOperations.Controls)
                    {
                        b.Enabled = ((Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow)b.Tag).id == pFirst.operationid;
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void UpdateForm()
        {
            // Provede aktualizaci zobrazeni stavu dle zjisteneho stavu zakazky a operace
            labelZakazka.Text = "Zakázka: " + zakazka.SOPNUMBE.Trim() + " - " + zakazka.SOPDESC.Trim();
            labelInfo.Text = "Info: " +
                "Pracovník " + (this.idpracovnik == null ? "?" : this.idpracovnik.firstname.Trim() + " " + this.idpracovnik.surname) +
                ", " +
                "Pracovištì " + (this.idmachine == null ? "?" : this.idmachine.name.Trim());

            UpdateTextForm();
        }

        private List<Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow> _operationsForMachine = null;
        private void OperationsLoad()
        {
            try
            {
                dsVyroba = new Fask.SQLiteDBs.DataSets.Vyroba();

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.OperationsTableAdapter tao = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.OperationsTableAdapter();
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.VMachinesOperationsTableAdapter tavmo = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.VMachinesOperationsTableAdapter();

                //tao.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));
                //tavmo.Connection = tao.Connection;

                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_Operations(dsVyroba.Operations);
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByMachineID_VMachinesOperations(dsVyroba.VMachinesOperations, idmachine.id);

                var operationsForMachine =                    
                    from o in dsVyroba.Operations
                    join vmo in dsVyroba.VMachinesOperations on o.id equals vmo.operationid
                    orderby o.name
                    select o;

                _operationsForMachine = operationsForMachine.ToList();
                int oCount = operationsForMachine.Count();
                int nN = (int)Math.Ceiling(Math.Sqrt(oCount));
                tlOperations.RowCount = nN;
                tlOperations.ColumnCount = nN;

                tlOperations.RowStyles.Clear();
                for (int i = 0; i < nN; i++)
                {
                    tlOperations.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / nN));
                }
                tlOperations.ColumnStyles.Clear();
                for (int i = 0; i < nN; i++)
                {
                    tlOperations.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / nN));
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
                foreach (var item in operationsForMachine.ToList())
                {
                    Button b = new Button();
                    b.Text = item.name;
                    b.Parent = tlOperations;
                    b.Dock = DockStyle.Fill;
                    b.Click += new EventHandler(b_Click);
                    b.Tag = item;
                    tlOperations.SetColumn(b, nC);
                    tlOperations.SetRow(b, nR);

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
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow operace = b.Tag as Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow;

            if (operace == null)
            {
                FlexibleMessageBox.Show(this, "Operace není nastavena!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.idoperation = operace;

            this.PerformOK();
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

            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.OperationsRow oFinded = _operationsForMachine.Find(o => o.id == e.BarcodeData.Trim());
                if (oFinded == null)
                {
                    FlexibleMessageBox.Show(this, "Operace s  kódem '" + e.BarcodeData.Trim() + "' nenalezena.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                    this.idoperation = oFinded;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormOperace_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        private void FormOperace_Shown(object sender, EventArgs e)
        {
        }

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            // Dotaz na potrvzeni akce ...
            if (this.idoperation == null)
            {
                FlexibleMessageBox.Show(this, "Není vybrána operace!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_production.IsTIMESTARTNull())
            { // zahajeni operace
                if (DialogResult.No == FlexibleMessageBox.Show(this, "Zahájit operaci '" + this.idoperation.name + "' ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }
                _production.TIMESTART = DateTime.Now;
            }
            else
            { // ukonceni operace
                if (DialogResult.No == FlexibleMessageBox.Show(this, "Ukonèit operaci '" + this.idoperation.name + "' ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }
                _production.TIMESTOP = DateTime.Now;
            }

            _production.operationid = this.idoperation.id;

            //Odvod dokoncen => zastavit scanner a konec
            ScannerStop();
            DialogResult = DialogResult.OK;
        }

    }
}