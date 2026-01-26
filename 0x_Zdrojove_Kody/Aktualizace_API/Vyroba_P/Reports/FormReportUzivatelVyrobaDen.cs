using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Fask.Aktualizace_API.Extensions;
using System.IO;
using Fask.Aktualizace_API.ServerAccess;

namespace Fask.Aktualizace_API.Reports
{
    public partial class FormReportUzivatelVyrobaDen : Form
    {
        public FormReportUzivatelVyrobaDen()
        {
            InitializeComponent();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormReportUzivatelVyrobaDen_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);

            UpdateFormInformations();
        }

        private void FormReportUzivatelVyrobaDen_KeyDown(object sender, KeyEventArgs e)
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

        private void UpdateFormInformations()
        {
            // Todo : test zda je pracovnik prihlasen ...
            try
            {

                var reportuserday = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Get_Report_UserDay(Globals.Pracovnik.id, Globals._PracovnikLoginDateTime.Value, DateTime.Now);

                if (Globals.Pracovnik != null)
                {
                    this.lPracovnik.Text = Globals.Pracovnik.ToString();
                    this.lOsobniCislo.Text = Globals.Pracovnik.id;
                }
                else
                {
                    this.lPracovnik.Text = "-";
                    this.lOsobniCislo.Text = "-";
                }

                lDatum.Text = DateTime.Now.ToString("G");

                lPraceNaStrojich.Text = String.Join(", ", reportuserday.Machines);

                lOdvedenoNormovanyCas.Text = TimeSpan.FromHours((double)reportuserday.TimeNorm).ToStringHHmm();

                lSkutecnyCas.Text = TimeSpan.FromHours((double)reportuserday.TimeReal).ToStringHHmm();

                lKorekce.Text = TimeSpan.FromHours((double)reportuserday.TimeCorrects).ToStringHHmm();

                #region Test na neukoncenou operaci ...
                // Zjisteni posledni akce uzivatele 
                // a) ze serveru
                WebServiceVyroba.VyrobaDataSet dsR_LastProductionUserMachine = null;
                try
                {
                    dsR_LastProductionUserMachine = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProductionLastAction(Globals.Pracovnik.id, Settings.MachineID, false, 1);
                }
                catch (Exception ews)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ews);
                }
                //Dohledani posledni 2 akci production
                //b) z lokalu 
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUserMachine = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserIDMachineID_Production(Globals.Pracovnik.id, Settings.MachineID);

                bool neukoncenaProdukce = false;
                DateTime neukoncenaProdukceCas = DateTime.MinValue;
                string neukoncenaProdukceText = string.Empty;

                Fask.SQLiteDBs.DataSets.Vyroba dsTmp = new Fask.SQLiteDBs.DataSets.Vyroba();
                dsTmp.Production.PrimaryKey = new DataColumn[] { dsTmp.Production.GUIDColumn };
                
                //dsTmp.Production.Columns.Remove(dsTmp.Production.TIMECRIDTYPEColumn);
                dsR_LastProductionUserMachine.Production.Columns.Remove(dsR_LastProductionUserMachine.Production.TIMECRIDTYPEColumn);

                if (dsR_LastProductionUserMachine != null)
                    dsTmp.Production.Merge(dsR_LastProductionUserMachine.Production);
                if (dtL_LastProductionUserMachine != null)
                    dsTmp.Merge(dtL_LastProductionUserMachine);

                var lastRowsOrdered = dsTmp.Production.Where(x=>x.TIMEMODE>0).OrderByDescending(x => x.dateeve);
                if (lastRowsOrdered.Count() > 0)
                {
                    var lastRow = lastRowsOrdered.First();
                    if ((lastRow.TIMEMODE > 0) && ((!lastRow.IsTIMESTARTNull() && lastRow.IsTIMESTOPNull()) || (!lastRow.IsTIMEPREPSTARTNull() && lastRow.IsTIMEPREPSTOPNull())))
                    {
                        neukoncenaProdukce = true;
                        neukoncenaProdukceCas = lastRow.dateeve;
                        neukoncenaProdukceText = "'" + lastRow.SOPNUMBE + "\n:" + lastRow.ITEMNMBR + "\n>" + lastRow.BarcodeP;
                    }
                }

                if (neukoncenaProdukce)
                {
                    labelNeukoncenaProdukce.Text = "Neukonèená produkce:\n" + neukoncenaProdukceText;
                }
                else
                {
                    labelNeukoncenaProdukce.Text = string.Empty;
                }


                #endregion

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void buttonObnovit_Click(object sender, EventArgs e)
        {
            UpdateFormInformations();
        }

    }
}

