using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JR.Utils.GUI.Forms;
using System.Threading;
using Fask.Logging;
using Fask.Aktualizace_API.Extensions;
using System.Reflection;
using Fask.Aktualizace_API.ServerAccess;

namespace Fask.Aktualizace_API.OtevreneProdukce
{
    public partial class ucOpenedProduction : UserControl
    {
        //[System.ComponentModel.ReadOnly(true)]
        //[Category("FASK")]
        //[DisplayName("LALALALAL")]
        //public Label l1
        //{
        //    get { return label1; }
        //}
        /// <summary>
        /// DatagridView slouzici k zobrazeni zaznamu.
        /// </summary>
        [Category("FASK")]
        public DataGridView DataGridView1
        {
            get
            {
                return dataGridView1;
            }
        }

        private global::System.Threading.Timer timerAutoUpdate = null;
        //private global::System.Threading.Timer timerFilterResetInterval = null;
        System.Threading.Thread threadUpdateData = null;
        private bool filterActive = false;      // je aktivní filtr
        private string filtrOsoba = string.Empty;
        private DateTime? filtrDatumOd = null;
        private DateTime? filtrDatumDo = null;
        private string filtrZakazka = string.Empty;
        private string filtrVyrobniPrikaz = string.Empty;
        private Object lockUpDownTest = new Object();
        public static bool _updateInProgress = false;

        public ucOpenedProduction()
        {
            InitializeComponent();                  
        }

        private void updateData(object state)
        {
            try
            {
                lock (lockUpDownTest)
                {
                    if (_updateInProgress)
                    {
                        timerUpdateStart(2000);
                        return;
                    }
                    _updateInProgress = true;
                }
                // filtr je vypnutý
                if (!filterActive)
                {
                    filtrDatumDo = null;
                    filtrDatumOd = null;
                    filtrOsoba = string.Empty;
                    filtrZakazka = string.Empty;
                    filtrVyrobniPrikaz = string.Empty;
                }
                // start vlákna pro aktualizaci dat
                threadUpdateData = new Thread(new ThreadStart(timerUpdateDataThreadStart));
                threadUpdateData.Name = "threadUpdateData_" + DateTime.Now.TimeOfDay.ToString();
                threadUpdateData.IsBackground = true;
                threadUpdateData.Priority = ThreadPriority.Lowest;
                threadUpdateData.Start();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void timerUpdateStart(int nextrunmiliseconds)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { timerUpdateStart(nextrunmiliseconds); });
                return;
            }

            timerAutoUpdate.Change(nextrunmiliseconds, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);
        }

        public void aktualizaceDat()
        {
            //buttonFiltrZrusit_Click(new object(), new EventArgs());
            timerAutoUpdate.Change(0, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);    // okamžité načtení dat
            //updateData(null);
        }


        private void timerUpdateDataThreadStart()
        {
            // neodeslana data
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter tapro = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
            //tapro.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
            // databaze s historii
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionHistTableAdapter taproHist = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionHistTableAdapter();
            //taproHist.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD);
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter taLogins = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter();
            //taLogins.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter taMachines = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
            //taMachines.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.OperationsTableAdapter taOperations = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.OperationsTableAdapter();
            //taOperations.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            // 5.10.2016 JiS - 
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter taCorrects = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
            //taCorrects.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            try
            {
                //Fask.Vyroba_P.WebServiceVyroba.VyrobaCEDataSet dataCE = new Fask.Vyroba_P.WebServiceVyroba.VyrobaCEDataSet();
                Fask.SQLiteDBs.DataSets.Vyroba dataCE = new Fask.SQLiteDBs.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba dataCE2 = new Fask.SQLiteDBs.DataSets.Vyroba();
                //Data.VyrobaCEDataSet.ProductionDataTable pdtAll = tapro.GetData();
                WebServiceVyroba.VyrobaDataSet dsVweb = null;

                // naplnění datasetu neodeslanými daty
                if(Settings.ModulNedokonceneZakazkyPouzitFiltrNaUzivatele && Globals.Pracovnik != null)
                    dataCE2.ProductionHist.Merge(Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserID_Production(Globals.Pracovnik.id), false, MissingSchemaAction.Ignore);
                else
                    dataCE2.ProductionHist.Merge(Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetData_Production(), false, MissingSchemaAction.Ignore);

                //dataCE.Production.Merge(pdtAll, false, MissingSchemaAction.Ignore);
                if (Settings.ModulNedokonceneZakazkyPouzitDataZeServeru)
                {
                    try
                    {
                        // stažení dat ze serveru
                        dsVweb = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Production_AllOpenedProductions();
                        //dsVweb = vyrobaS.Production_AllOpenedProductions((Settings.ModulNedokonceneZakazkyPouzitFiltrNaUzivatele && Globals.Pracovnik != null) ? Globals.Pracovnik.id : null , Settings.ModulNedokonceneZakazkyPouzitFiltrNaStroj ? Settings.MachineID : null);
                    }
                    catch (Exception exWeb)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exWeb);
                        //FlexibleMessageBox.Show(this, exWeb.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if ((dsVweb != null) && (dsVweb.Production.Count > 0))
                    {
                        //dataCE.ProductionHist.Merge(dsVweb.Production, false, MissingSchemaAction.Ignore);
                        dataCE2.ProductionHist.Merge(dsVweb.Production, false, MissingSchemaAction.Ignore);
                    }
                }
                
                //dataCE.ProductionHist.AcceptChanges();
                dataCE2.ProductionHist.AcceptChanges();
                // naplneni aktualnimi daty
                //dataCE.ProductionHist.Merge(tapro.GetData(), false, MissingSchemaAction.Ignore);
                dataCE2.ProductionHist.Merge(Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetData_Production(), false, MissingSchemaAction.Ignore);

                // nacteni vsech záznamu (jak ze serveru, tak z neodeslanych dat)
                var ccList = from c in dataCE2.ProductionHist
                             where !c.IsSOUBEHGUIDNull()
                             select c;

                // nacteni ukoncenych zaznamu
                var bannedCCList = from c in dataCE2.ProductionHist
                                   where (!c.IsSOUBEHGUIDNull() && !c.IsTIMESTOPNull()) || (!c.IsSOUBEHGUIDNull()  && !c.IsTIMEPREPSTOPNull())
                                   select c;
                //var finalList = ccList.Except(bannedCCList);
                // odstraneni zaznamu, ktere jiz byly odvedeny
                var finalList = from r in ccList
                                where !bannedCCList.Any(b => b.SOUBEHGUID == r.SOUBEHGUID)
                                select r;

                dataCE2.AcceptChanges();
                // naplneni datasetu neukoncenymi zakazkami
                //finalList.ToList<Fask.Vyroba_P.Data.VyrobaCEDataSet.ProductionHistRow>().ForEach(i => dataCE.ProductionHist.ImportRow(i as Fask.Vyroba_P.Data.VyrobaCEDataSet.ProductionHistRow));

                // je povolen neustale aktivni filtr na stroj
                if (Settings.ModulNedokonceneZakazkyPouzitFiltrNaStroj)
                {
                    finalList = finalList.Where(x => x.machineid == Settings.MachineID);
                }

                // je povolen neustale aktivni filtr na prihlaseneho uzivatele
                if (Settings.ModulNedokonceneZakazkyPouzitFiltrNaUzivatele && Globals.Pracovnik != null)
                {
                    finalList = finalList.Where(x => x.UserID == Globals.Pracovnik.id);
                }


                finalList.ToList<Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistRow>().ForEach(i => dataCE.ProductionHist.ImportRow(i));

                dataCE.AcceptChanges();

                // naplnění tabulky Logins
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_Logins(dataCE.Logins);

                // naplneni tabulky Machines
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_Machines(dataCE.Machines);

                // naplnění názvu operace
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_Operations(dataCE.Operations);

                // naplneni nazvu korekci
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_Corrects(dataCE.Corrects);

                // doplnění jména
                foreach (var item in dataCE.Logins)
                {
                    dataCE.ProductionHist
                        .Where(p => p.UserID == item.id)
                        .ToList()
                        .ForEach(pr => 
                            { 
                                pr.firstname = item.firstname; 
                                pr.surname = item.surname; 
                            });
                }

                // doplnění názvu stroje
                foreach (var item in dataCE.Machines)
                {
                    dataCE.ProductionHist
                        .Where(p => !p.IsmachineidNull() && p.machineid == item.id)
                        .ToList()
                        .ForEach(pr =>
                        {
                            pr.machineName = item.name;
                        });
                }

                // doplnění názvu operace
                foreach (var item in dataCE.Operations)
                {
                    dataCE.ProductionHist
                        .Where(p => !p.IsoperationidNull() && p.operationid == item.id)
                        .ToList()
                        .ForEach(pr =>
                        {
                            pr.operationName = item.name;
                        });
                }

                foreach (var item in dataCE.Corrects)
                {
                    dataCE.ProductionHist
                        .Where(p => !p.IsTIMECRIDNull() && p.TIMECRID == item.id)
                        .ToList()
                        .ForEach(pr =>
                            {
                                pr.TIMECRIDName = item.desc;
                            });
                }

                //query.copy
                //IEnumerable<DataRow> objDatarow = query.AsEnumerable();

                this.BeginInvoke(
                    (MethodInvoker)delegate() { updateDataGrid(dataCE); });
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                _updateInProgress = false;
            }
        }

        private void ucHistory_Resize(object sender, EventArgs e)
        {

        }

        private void buttonFiltrOsoba_Click(object sender, EventArgs e)
        {
            try
            {
                using (Odvadeni.FormIDPracovnika fidprac = new Fask.Aktualizace_API.Odvadeni.FormIDPracovnika())
                {
                    if (fidprac.ShowDialog(this) == DialogResult.Cancel)
                        return;
                    //pracovnik = fidprac.Pracovnik;
                    //idPracovnik = pracovnik.id;
                    filtrOsoba = fidprac.Pracovnik.id.Trim();
                }
                filterActive = true;
                timerAutoUpdate.Change(0, Timeout.Infinite);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulNedokonceneZakazkyFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }            
        }

        private void ucHistory_Load(object sender, EventArgs e)
        {
            try
            {

                toolStripAktivniFiltry.Text = string.Empty;

                if (!DesignMode)
                {
                    //updateData(null);
                    //timerAutoUpdate = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(updateData), this, Settings.TimerUploadInterval, Settings.TimerUploadInterval);
                    // začne po 3 sec. od spuštění
                    timerAutoUpdate = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(updateData), this, 3000, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);
                    // timer filter je vypnutý
                    //timerFilterResetInterval = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(resetFilter), this, Timeout.Infinite, Timeout.Infinite);
                    // změna fontu
                    this.Font = new Font(this.Font.FontFamily, Settings.ModulNedokonceneZakazkyFontSize, this.Font.Style);
                    // změna fontu v řádcích datagridu
                    foreach (DataGridViewColumn item in dataGridView1.Columns)
                    {
                        item.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, Settings.ModulNedokonceneZakazkyFontRowSize, dataGridView1.Font.Style);
                    }
                    //dataGridView1.Refresh();
                    buttonFiltrOsoba.Font = new Font(buttonFiltrOsoba.Font.FontFamily, Settings.ModulNedokonceneZakazkyFontSize, buttonFiltrOsoba.Font.Style);
                    buttonFiltrZakazka.Font = new Font(buttonFiltrZakazka.Font.FontFamily, Settings.ModulNedokonceneZakazkyFontSize, buttonFiltrZakazka.Font.Style);
                    buttonFiltrZrusit.Font = new Font(buttonFiltrZrusit.Font.FontFamily, Settings.ModulNedokonceneZakazkyFontSize, buttonFiltrZrusit.Font.Style);
                    buttonFiltrVyrobniPrikaz.Font = new Font(buttonFiltrZrusit.Font.FontFamily, Settings.ModulNedokonceneZakazkyFontSize, buttonFiltrZrusit.Font.Style);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }      
        }

        private void buttonFiltrCas_Click(object sender, EventArgs e)
        {
            try
            {
                using (Odvadeni.FormDatumOdDo fdatum = new Fask.Aktualizace_API.Odvadeni.FormDatumOdDo())
                {
                    if (fdatum.ShowDialog(this) == DialogResult.Cancel)
                        return;
                    //pracovnik = fidprac.Pracovnik;
                    //idPracovnik = pracovnik.id;
                    filtrDatumOd = fdatum.DatumOd;
                    filtrDatumDo = fdatum.DatumDo;
                }
                filterActive = true;
                timerAutoUpdate.Change(0, Timeout.Infinite);    // okamžité načtení dat
                //updateData(null);
                ////this.bindingSourceProduction.Filter = "dateeve >= " + filtrDatumDo.Value.ToString(;
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulNedokonceneZakazkyFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonFiltrZrusit_Click(object sender, EventArgs e)
        {
            try
            {
                filtrDatumDo = null;
                filtrDatumOd = null;
                filtrOsoba = string.Empty;
                filtrZakazka = string.Empty;
                // aktualizace kvůli zrušení filtrů
                filterActive = false;
                filtrVyrobniPrikaz = string.Empty;
                toolStripAktivniFiltry.Text = string.Empty;
                //timerAutoUpdate.Change(0, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);    // okamžité načtení dat
                timerAutoUpdate.Change(0, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);    // okamžité načtení dat
                //this.bindingSourceProductionHist.RemoveFilter();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            
        }

        private void buttonFiltrZakazka_Click(object sender, EventArgs e)
        {
            try
            {
                // zadani zakazky
                using (OdvadeniNadop.FormVyberZakazka fzakazka = new Fask.Aktualizace_API.OdvadeniNadop.FormVyberZakazka())
                {
                    fzakazka.Text = "Zakázka :";
                    if (fzakazka.ShowDialog(this) == DialogResult.Cancel)
                        return;
                    filtrZakazka = fzakazka.Zakazka.SOPNUMBE;
                }
                filterActive = true;
                timerAutoUpdate.Change(0, Timeout.Infinite);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulNedokonceneZakazkyFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void updateDataGrid(Fask.SQLiteDBs.DataSets.Vyroba dataCE)
        {
            try
            {
                

                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate() { updateDataGrid(dataCE); });
                    return;
                }

                //Point p = Point.Empty;
                int FirstDisplayedScrollingRowIndex = 0;
                //int SelectedRowIndex = 0;
                try
                {
                    FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                    //DataGridViewCell currentCell = this.dataGridView1.CurrentCell;

                    //if (this.dataGridView1.SelectedRows.Count > 0) SelectedRowIndex = this.dataGridView1.SelectedRows[0].Index; //Save Current Selected Row Index
                    //p = new Point(currentCell.ColumnIndex, currentCell.RowIndex);                    
                }
                catch
                {
                }
                List<string> list = new List<string>();

                if (!string.IsNullOrEmpty(filtrOsoba))
                    list.Add("Osoba");
                if (filtrDatumOd != null && filtrDatumDo != null)
                    list.Add("Čas");
                if (!string.IsNullOrEmpty(filtrZakazka))
                    list.Add("Zakázka");
                if (!string.IsNullOrEmpty(filtrVyrobniPrikaz))
                    list.Add("Výrobní příkaz");

                toolStripAktivniFiltry.Text = string.Join(", ", list.ToArray());

                this.vyrobaCEDataSet1 = dataCE;
                this.vyrobaCEDataSet1.AcceptChanges();
                this.bindingSourceProductionHist.DataSource = this.vyrobaCEDataSet1.ProductionHist;

                // pokud je zapnutý filtr, vypne se auto update a zapne se reset interval
                if (list.Count > 0 && filterActive)
                {
                    bindingSourceProductionHist.RemoveFilter();
                    filterActive = false;
                    // první interval je resetovací délka a následně automatická aktualizace
                    timerAutoUpdate.Change(Settings.ModulNedokonceneZakazkyFilterResetInterval, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);
                    bool isAnd = false;

                    if (!string.IsNullOrEmpty(filtrOsoba))
                    {
                        isAnd = true;
                        this.bindingSourceProductionHist.Filter = "UserID=" + filtrOsoba + " ";
                    }
                    if (filtrDatumOd != null && filtrDatumDo != null)
                    {                        
                        if(isAnd)
                        {
                            this.bindingSourceProductionHist.Filter += "and dateeve >= #" + (filtrDatumOd.Value.ToString("s") + "#").Replace("T", " ") + " AND dateeve <= #" + (filtrDatumDo.Value.ToString("s") + "#").Replace("T", " ") + " ";
                        }
                        else
                        {
                            this.bindingSourceProductionHist.Filter += "dateeve >= #" + (filtrDatumOd.Value.ToString("s") + "#").Replace("T", " ") + " AND dateeve <= #" + (filtrDatumDo.Value.ToString("s") + "#").Replace("T", " ") + " ";
                            isAnd = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(filtrZakazka))
                    {
                        this.bindingSourceProductionHist.Filter += isAnd ? ("and SOPNUMBE='" + filtrZakazka + "' ") : ("SOPNUMBE='" + filtrZakazka + "' ");
                        isAnd = true;
                        //this.bindingSourceProduction.Filter += "SOPNUMBE=" + filtrZakazka;
                    }

                    if (!string.IsNullOrEmpty(filtrVyrobniPrikaz))
                    {
                        this.bindingSourceProductionHist.Filter += isAnd ? ("and BarcodeP='" + filtrVyrobniPrikaz + "' ") : ("BarcodeP='" + filtrVyrobniPrikaz + "' ");
                        //this.bindingSourceProduction.Filter += "SOPNUMBE=" + filtrZakazka;
                    }
                }
                else   // možná nemusí
                {
                    timerAutoUpdate.Change(Settings.ModulNedokonceneZakazkyAutoUpdateInterval, Settings.ModulNedokonceneZakazkyAutoUpdateInterval);
                    this.bindingSourceProductionHist.RemoveFilter();
                }
                try
                {
                    if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
                    //if ((this.dataGridView1.Rows.Count - 1) >= SelectedRowIndex) this.dataGridView1.Rows[SelectedRowIndex].Selected = true; //Restore Selected Row
                    //this.dataGridView1.CurrentCell = this.dataGridView1[p.X, p.Y];
                }
                catch
                {
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }

        private void buttonFiltrVyrobniPrikaz_Click(object sender, EventArgs e)
        {
            try
            {
                // zadani zakazky
                using (Forms.FormInputKod fzakazka = new Forms.FormInputKod())
                {
                    fzakazka.Text = "Výrobní příkaz :";
                    if (fzakazka.ShowDialog(this) == DialogResult.Cancel)
                        return;
                    filtrVyrobniPrikaz = fzakazka.Kod.Trim();
                }
                filterActive = true;
                timerAutoUpdate.Change(0, Timeout.Infinite);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulNedokonceneZakazkyFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
