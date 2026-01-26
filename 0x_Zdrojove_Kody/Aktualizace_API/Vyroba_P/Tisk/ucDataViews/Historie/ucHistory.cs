/* Modul lokalni historie odvodu a korekci 
 * ---------------------------------------
 * vyuziva se duplicitni kopie a ulozeni dat do ProductionHist.sdf
 * Zobrazuje lokalni historii odvedenych dat tohoto stroje
 */

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
using Fask.Aktualizace_API.Extensions;
using System.Reflection;
using Fask.Logging;
using Fask.Aktualizace_API.ServerAccess;

namespace Fask.Aktualizace_API.ucDataViews.Historie
{
    public partial class ucHistory : UserControl
    {
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

        #region Prace s filtry
        private bool filterActive = false;      // je aktivní filtr
        private bool filtrNedokonceneZakazky = false;
        private string filtrOsoba = string.Empty;
        private string filtrStroj = string.Empty;
        private DateTime? filtrDatumOd = null;
        private DateTime? filtrDatumDo = null;
        private string filtrZakazka = string.Empty;
        #endregion

        private Object lockUpDownTest = new Object();
        public static bool _updateInProgress = false;

        #region vybraný řádek
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistRow SelectedRow
        {
            get
            {
                try
                {
                    // return ((dg_vyberVP.BindingContext[bs_vyberVP].Current)) as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable;
                    return ((DataRowView)(dataGridView1.BindingContext[bindingSourceProductionHist].Current)).Row as Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistRow;
                }
                catch (Exception ex)
                {

                    ExceptionHandler2.Handle(ex);
                    return null;
                }
            }
        } 
        #endregion


        public ucHistory()
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
                    filtrNedokonceneZakazky = false;
                    filtrStroj = string.Empty;
                    filterActive = false;
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
                timerAutoUpdate.Change(nextrunmiliseconds, Settings.ModulPrehledOdvoduAutoUpdateInterval);
        }

        private void timerUpdateDataThreadStart()
        {
            // neodeslana data
            //Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter tapro = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
            //tapro.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf);
            // databaze s historii
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionHistTableAdapter taproHist = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionHistTableAdapter();
            //taproHist.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD);
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter taLogins = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter();
            //taLogins.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter taMachines = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
            //taMachines.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.OperationsTableAdapter taOperations = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.OperationsTableAdapter();
            //taOperations.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            // 5.10.2016 JiS - pridani nazvu korekci ...
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter taCorrects = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
            //taCorrects.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

            try
            {
                //Fask.Vyroba_P.WebServiceVyroba.VyrobaCEDataSet dataCE = new Fask.Vyroba_P.WebServiceVyroba.VyrobaCEDataSet();
                Fask.SQLiteDBs.DataSets.Vyroba dataCE = new Fask.SQLiteDBs.DataSets.Vyroba();
                // nastavit primarni klic na guid ...
                dataCE.ProductionHist.PrimaryKey = new DataColumn[] { dataCE.ProductionHist.GUIDColumn };
                //Data.VyrobaCEDataSet.ProductionDataTable pdtAll = tapro.GetData();
                WebServiceVyroba.VyrobaDataSet dsVweb = null;

                // naplnění datasetu neodeslanými daty
                //dataCE.Production.Merge(tapro.GetData(), false, MissingSchemaAction.Ignore);
                //dataCE.Production.Merge(pdtAll, false, MissingSchemaAction.Ignore);

                if (Settings.ModulPrehledOdvoduPouzitDataZeServeru)
                {
                    try
                    {
                        WebServiceVyroba.FiltersHistory filters = new Fask.Aktualizace_API.WebServiceVyroba.FiltersHistory();
                        filters.filtrDatumDo = filtrDatumDo;
                        filters.filtrDatumOd = filtrDatumOd;
                        filters.filtrNedokonceneZakazky = filtrNedokonceneZakazky;
                        filters.filtrOsoba = filtrOsoba;
                        filters.filtrStroj = filtrStroj;
                        filters.filtrZakazka = filtrZakazka;

                        // stažení dat ze serveru
                        //dsVweb = vyrobaS.Production_History(filtrOsoba, Settings.MachineID, filtrZakazka, filtrDatumOd, filtrDatumDo, Settings.ModulPrehledPocetHodinHistorie);
                        dsVweb = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Production_HistoryFilter(filters, Settings.ModulPrehledPocetHodinHistorieServer);
                    }
                    catch (Exception exWeb)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exWeb);
                        //FlexibleMessageBox.Show(this, exWeb.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dsVweb != null)
                    {
                        //dataCE.Production.Merge(dsVweb.Production, false, MissingSchemaAction.Ignore);
                        dataCE.ProductionHist.Merge(dsVweb.Production, false, MissingSchemaAction.Ignore);
                    }
                }

                //dataCE.AcceptChanges();
                //taproHist.DeleteQuery();
                //foreach (Fask.Vyroba_P.WebServiceVyroba.VyrobaCEDataSet.ProductionRow item in dataCE.Production)
                //{
                //    item.SetAdded();
                //}
                //int pom = taproHist.Update(dataCE.Production.Select());                

                // naplnění selectem podle zvoleného filtru
                #region OLD dotaz...
                //System.Data.SqlServerCe.SqlCeDataAdapter da_filter = new System.Data.SqlServerCe.SqlCeDataAdapter();
                //da_filter.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand();
                //da_filter.SelectCommand.Connection = new System.Data.SqlServerCe.SqlCeConnection();
                //da_filter.SelectCommand.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD);
                //da_filter.SelectCommand.CommandText = "select * from Production ";
                //// test
                ////da_filter.SelectCommand.CommandText += "where SOUBEHGUID not IN(select distinct SOUBEHGUID from Production Where SOUBEHGUID is not null and TIMESTOP is not null)";
                //// filtruje se podle datumu, je třeba načíst všechna data
                //if (filtrDatumDo == null && filtrDatumOd == null)
                //{
                //    da_filter.SelectCommand.CommandText += "where dateeve > @datum ";
                //    da_filter.SelectCommand.Parameters.AddWithValue("@datum", DateTime.Now.AddHours(-Settings.ModulPrehledPocetHodinHistorie));
                //}

                //if (filtrNedokonceneZakazky)
                //{
                //    if (filtrDatumDo != null && filtrDatumOd != null)
                //    {
                //        da_filter.SelectCommand.CommandText += "where ";
                //    }
                //    else
                //        da_filter.SelectCommand.CommandText += "and ";

                //    da_filter.SelectCommand.CommandText += "" +
                //    //    "Select * " +
                //    //"From Production " +
                //    "SOUBEHGUID not IN (" +
                //    "select distinct SOUBEHGUID from Production " +
                //    "where " +
                //    "(SOUBEHGUID is not null and TIMESTOP is not null) " +
                //    "or " +
                //    "(SOUBEHGUID is not null and TIMEPREPSTOP is not null) " +
                //    ")"
                //    ;
                //} 
                #endregion

                //System.Data.SqlServerCe.SqlCeDataAdapter da_filter2 = new System.Data.SqlServerCe.SqlCeDataAdapter();
                //Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable dtProdHist2 = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable();
                // filtr na nedokoncene korekce
                //if (filtrNedokonceneZakazky)
                //{
                //    da_filter2.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand();
                //    da_filter2.SelectCommand.Connection = new System.Data.SqlServerCe.SqlCeConnection();
                //    da_filter2.SelectCommand.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD);
                //    da_filter2.SelectCommand.CommandText = "select * from Production ";
                //    // test
                //    //da_filter.SelectCommand.CommandText += "where SOUBEHGUID not IN(select distinct SOUBEHGUID from Production Where SOUBEHGUID is not null and TIMESTOP is not null)";
                //    // filtruje se podle datumu, je třeba načíst všechna data
                //    if (filtrDatumDo == null && filtrDatumOd == null)
                //    {
                //        da_filter2.SelectCommand.CommandText += "where dateeve > @datum ";
                //        da_filter2.SelectCommand.Parameters.AddWithValue("@datum", DateTime.Now.AddHours(-Settings.ModulPrehledPocetHodinHistorie));
                //    }


                //    if (filtrDatumDo != null && filtrDatumOd != null)
                //    {
                //        da_filter2.SelectCommand.CommandText += "where ";
                //    }
                //    else
                //        da_filter2.SelectCommand.CommandText += "and ";

                //    da_filter2.SelectCommand.CommandText += "" +
                //        //    "Select * " +
                //        //"From Production " +
                //    "TIMESTART is null and CORRGUID not IN (" +
                //    "select distinct CORRGUID from Production " +
                //    "where " +
                //    "(CORRGUID is not null and TIMECORSTOP is not null) " +
                //    ")"
                //    ;
                //}

                //dataCE.ProductionHist.Clear();
                dataCE.ProductionHist.AcceptChanges();
                // naplneni aktualnimi daty
                //da_filter.Fill(dataCE.ProductionHist);
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__ProductionHist_PRD.FillProduction_HistoryFilter_Production(
                    dataCE.ProductionHist,
                    Settings.ModulPrehledPocetHodinHistorie,
                    filtrDatumDo,
                    filtrDatumOd,
                    filtrNedokonceneZakazky
                    );

                // naplneni i neukoncenymi korekcemi
                if (filtrNedokonceneZakazky)
                {
                    //da_filter2.Fill(dtProdHist2);
                    var dtProdHist2 = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__ProductionHist_PRD.GetProduction_HistoryFilter2_Production( Settings.ModulPrehledPocetHodinHistorie, filtrDatumDo, filtrDatumOd );
                    dataCE.ProductionHist.Merge(dtProdHist2);
                }
                // naplnění tabulky Logins
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Fill_Logins(dataCE.Logins);

                // naplneni tabulkzy Machines
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
                //this.vyrobaCEDataSet1.ProductionHist.Select("CORRGUID not IN .....");
                this.BeginInvoke((MethodInvoker)delegate() { updateDataGrid(dataCE); });
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
                timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulPrehledOdvoduFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }            
        }

        private void buttonStroj_Click(object sender, EventArgs e)
        {
            try
            {
                using (Odvadeni.FormIDMachine fidstroj = new Fask.Aktualizace_API.Odvadeni.FormIDMachine())
                {
                    if (fidstroj.ShowDialog(this) == DialogResult.Cancel)
                        return;
                    filtrStroj = fidstroj.Machine.id.Trim();
                }
                filterActive = true;
                timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulPrehledOdvoduFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }            
        }


        private void ucHistory_Load(object sender, EventArgs e)
        {

            //TODO MaR 30.8 2023 zneviditelneni filtru panelu
           // tableLayoutPanel1.Visible = false;


            try
            {
                toolStripAktivniFiltry.Text = string.Empty;

                if (!DesignMode)
                {
                    try
                    {
                        // odstraneni starsich zaznamu
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprohist = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                        //taprohist.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD);
                        int rowsDeleted = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__ProductionHist_PRD.DeleteOlderThanDateeve_Production(DateTime.Now.AddDays(-Settings.ModulPrehledOdvoduMaxDaysInHistory));
                        //int rowsDeleted = taprohist.DeleteOlderThanDateeve(DateTime.Now.AddHours(-6));
                        Logging.ExceptionHandler2.Handle(LogLevel.Info,"Bylo odstraneno " + rowsDeleted + " zaznamu. Interval je nastaven na: " + Settings.ModulPrehledOdvoduMaxDaysInHistory + " dnu");

                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    }

                    //updateData(null);
                    //timerAutoUpdate = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(updateData), this, Settings.TimerUploadInterval, Settings.TimerUploadInterval);
                    // začne po 3 sec. od spuštění
                    timerAutoUpdate = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(updateData), this, 3000, Settings.ModulPrehledOdvoduAutoUpdateInterval);
                    // timer filter je vypnutý
                    //timerFilterResetInterval = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(resetFilter), this, Timeout.Infinite, Timeout.Infinite);

                    // změna fontu
                    this.Font = new Font(this.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, this.Font.Style);
                    // změna fontu v řádcích datagridu
                    foreach (DataGridViewColumn  item in dataGridView1.Columns)
                    {
                        item.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, Settings.ModulPrehledOdvoduFontRowSize, dataGridView1.Font.Style);
                    }
                    
                    buttonFiltrOsoba.Font = new Font(buttonFiltrOsoba.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrOsoba.Font.Style);
                    buttonFiltrStroj.Font = new Font(buttonFiltrStroj.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrStroj.Font.Style);
                    buttonFiltrCas.Font = new Font(buttonFiltrCas.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrCas.Font.Style);
                    buttonFiltrZakazka.Font = new Font(buttonFiltrZakazka.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrZakazka.Font.Style);
                    buttonNedokonceneZakazky.Font = new Font(buttonFiltrZakazka.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrZakazka.Font.Style);
                    buttonFiltrPrednastavit.Font = new Font(buttonFiltrPrednastavit.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrPrednastavit.Font.Style);
                    buttonFiltrZrusit.Font = new Font(buttonFiltrZrusit.Font.FontFamily, Settings.ModulPrehledOdvoduFontSize, buttonFiltrZrusit.Font.Style);

                    //dataGridView1.Refresh();
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
                timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                //updateData(null);
                ////this.bindingSourceProduction.Filter = "dateeve >= " + filtrDatumDo.Value.ToString(;
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulPrehledOdvoduFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void AktualizaceDat()
        {
            //timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
            updateData(null);
        }

        private void buttonFiltrZrusit_Click(object sender, EventArgs e)
        {


            try
            {
                filtrDatumDo = null;
                filtrDatumOd = null;
                filtrOsoba = string.Empty;
                filtrZakazka = string.Empty;
                filtrNedokonceneZakazky = false;
                // aktualizace kvůli zrušení filtrů
                filterActive = false;
                toolStripAktivniFiltry.Text = string.Empty;
                //timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
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
                timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulPrehledOdvoduFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Aktualizuje zobrazeni datagridu pro zobrazeni + vypocty ...
        /// </summary>
        /// <param name="dataCE"></param>
        private void updateDataGrid(Fask.SQLiteDBs.DataSets.Vyroba dataCE)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate() { updateDataGrid(dataCE); });
                    return;
                }
                // test zrychleni datagridu
                dataGridView1.SuspendLayout();
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
                    list.Add(string.Format("O=[{0}]", filtrOsoba));
                if (!string.IsNullOrEmpty(filtrStroj))
                    list.Add(string.Format("S=[{0}]", filtrStroj));
                if (filtrDatumOd != null && filtrDatumDo != null)
                    list.Add(string.Format("Č=[{0}-{1}]", filtrDatumOd.ToString(), filtrDatumDo.ToString()));
                if (!string.IsNullOrEmpty(filtrZakazka))
                    list.Add(string.Format("Z=[{0}]", filtrZakazka));
                if (filtrNedokonceneZakazky)
                    list.Add("Nedokončené zakázky");

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
                    timerAutoUpdate.Change(Settings.ModulPrehledOdvoduFilterResetInterval, Settings.ModulPrehledOdvoduAutoUpdateInterval);
                    bool isAnd = false;

                    if (!string.IsNullOrEmpty(filtrOsoba))
                    {
                        this.bindingSourceProductionHist.Filter += (isAnd ? "and " : "") + "UserID='" + filtrOsoba + "' ";
                        isAnd = true;
                    }

                    if (!string.IsNullOrEmpty(filtrStroj))
                    {
                        this.bindingSourceProductionHist.Filter += (isAnd ? "and " : "") + "MachineID='" + filtrStroj + "' ";
                        isAnd = true;
                    }

                    if (filtrDatumOd != null && filtrDatumDo != null)
                    {
                        this.bindingSourceProductionHist.Filter += (isAnd ? "and " : "") + "dateeve >= #" + (filtrDatumOd.Value.ToString("s") + "#").Replace("T", " ") + " AND dateeve <= #" + (filtrDatumDo.Value.ToString("s") + "#").Replace("T", " ") + " ";
                        isAnd = true;
                    }

                    if (!string.IsNullOrEmpty(filtrZakazka))
                    {
                        this.bindingSourceProductionHist.Filter += (isAnd ? "and " : "") + "SOPNUMBE='" + filtrZakazka + "' ";
                        isAnd = true;
                    }
                }
                else   // možná nemusí
                {
                    timerAutoUpdate.Change(Settings.ModulPrehledOdvoduAutoUpdateInterval, Settings.ModulPrehledOdvoduAutoUpdateInterval);
                    this.bindingSourceProductionHist.RemoveFilter();
                }


                updateStatusBar();

                try
                {
                    if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex))
                        //Restore Scroll Index
                        this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; 
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
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Active filter >>" +  (string.IsNullOrEmpty(bindingSourceProductionHist.Filter) ? string.Empty : bindingSourceProductionHist.Filter));
            }
            finally
            {
                dataGridView1.ResumeLayout(false);
            }
        }

        private void updateStatusBar()
        {
            toolStripCounts.Text = string.Format(
                "Celkem {0}"
                ,this.bindingSourceProductionHist.Count
                );

            //this.bindingSourceProductionHist.
            Fask.SQLiteDBs.DataSets.Vyroba dataCE = new Fask.SQLiteDBs.DataSets.Vyroba();
            DataTable dataCEProductionHist = (((DataView)bindingSourceProductionHist.List).ToTable());

            dataCE.ProductionHist.Merge(dataCEProductionHist);
            // vypocet celkovych hodnot : 
            // b) celkem normovany cas
            decimal tNorm = dataCE.ProductionHist.Where(p => !p.IsTIMESTOPNull()).Sum(p => Convert.ToDecimal(p.TIMEUNIT) * p.qty + Convert.ToDecimal(p.TIMEPREP)) / 60;
            //decimal tKors = Convert.ToDecimal(dataCE.ProductionHist.Where(p => !p.IsTIMECORSTOPNull()).Sum(p => (p.TIMECORSTOP - p.TIMECORSTART).TotalHours));
            decimal tKorsKladne = Convert.ToDecimal(dataCE.ProductionHist.Where(p => !p.IsTIMECORSTOPNull() && (p.IsTIMECRIDTYPENull() || p.TIMECRIDTYPE == 0)).Sum(p => (p.TIMECORSTOP - p.TIMECORSTART).TotalHours));
            decimal tKorsZaporne = Convert.ToDecimal(dataCE.ProductionHist.Where(p => !p.IsTIMECORSTOPNull() && (!p.IsTIMECRIDTYPENull() && p.TIMECRIDTYPE == 1)).Sum(p => (p.TIMECORSTOP - p.TIMECORSTART).TotalHours));
            decimal tKors = tKorsKladne - tKorsZaporne;
            decimal tCas = 0;
            DateTime? lastOdvodStop = null;
            string lastUserID = null;
            string lastMachineID = null;
            var odvody = dataCE.ProductionHist.Where(p => !p.IsTIMESTOPNull()).OrderBy(p => p.UserID).ThenBy(p => p.machineid).ThenBy(p => p.TIMESTOP);
            foreach (var odvod in odvody)
            {
                // inicializace
                if (String.IsNullOrEmpty(lastUserID) || lastUserID != odvod.UserID)
                {
                    lastUserID = odvod.UserID;
                    lastOdvodStop = null;
                }
                if (String.IsNullOrEmpty(lastMachineID) || lastMachineID != odvod.machineid)
                {
                    lastMachineID = odvod.machineid;
                    lastOdvodStop = null;
                }

                if (!lastOdvodStop.HasValue)
                    lastOdvodStop = odvod.TIMESTOP;

                if (!odvod.IsTIMESTARTNull())
                {
                    tCas += Convert.ToDecimal((odvod.TIMESTOP - odvod.TIMESTART).TotalHours);
                }
                else
                {
                    tCas += Convert.ToDecimal((odvod.TIMESTOP - lastOdvodStop.Value).TotalHours);
                }

                lastOdvodStop = odvod.TIMESTOP;
            }

            decimal tCelkem = tNorm + tKors;
            //toolStripOdvedenoCelkem.Text = String.Format("Norma={0:0}, Korekce={1:0}, Skut.={2:0}", tNorm, tKors, tCas);
            // JiS : je to zaokrouhlene, tak je to k nicemu ... 
            toolStripOdvedenoCelkem.Text = String.Format(
                "Norma={0}, Korekce={1}(+{2}, -{3}), Skut.={4}", 
                TimeSpan.FromHours(Convert.ToDouble(tNorm)).ToStringHHmmss(),
                TimeSpan.FromHours(Convert.ToDouble(tKors)).ToStringHHmmss(),
                TimeSpan.FromHours(Convert.ToDouble(tKorsKladne)).ToStringHHmmss(),
                TimeSpan.FromHours(Convert.ToDouble(tKorsZaporne)).ToStringHHmmss(),
                TimeSpan.FromHours(Convert.ToDouble(tCas)).ToStringHHmmss());

//#if DEBUG
//            string filename = "productionHistView.xml";
//            if (File.Exists(filename))
//                File.Delete(filename);
//            this.vyrobaCEDataSet1.ProductionHist.DefaultView.RowFilter = this.bindingSourceProductionHist.Filter;
//            DataTable dt = this.vyrobaCEDataSet1.ProductionHist.DefaultView.ToTable();
//            dt.WriteXml(filename, XmlWriteMode.IgnoreSchema, true);
//#endif
        }

        private void buttonNedokonceneZakazky_Click(object sender, EventArgs e)
        {
            try
            {
                filtrNedokonceneZakazky = true;
                filterActive = true;
                timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                //updateData(null);
                //timerAutoUpdate.Change(Timeout.Infinite, Timeout.Infinite);
                //timerFilterResetInterval.Change(Settings.ModulPrehledOdvoduFilterResetInterval, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonFiltrPrehled_Click(object sender, EventArgs e)
        {
            try
            {
                using (ucDataViews.Historie.dlgFilterSelector dlgfs = new Fask.Aktualizace_API.ucDataViews.Historie.dlgFilterSelector())
                {
                    if (DialogResult.Cancel == dlgfs.ShowDialog(this))
                        return;
                    
                    if (dlgfs.Cas == Casy.Denni)
                    {
                        filtrDatumOd = DateTime.Today;
                        filtrDatumDo = DateTime.Now;
                    }
                    else if (dlgfs.Cas == Casy.Mesicni)
                    {
                        filtrDatumOd = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                        filtrDatumDo = DateTime.Now;
                    }
                    else //if (dlgfs.Cas == Casy.DleFiltru)
                    {
                    }

                    if (dlgfs.Osoba == Osoba.Prihlaseny)
                    {
                        if (Globals.Pracovnik != null)
                            filtrOsoba = Globals.Pracovnik.id.Trim();
                        else
                        {
                            // zadat pracovnika
                            using (Odvadeni.FormIDPracovnika fidprac = new Fask.Aktualizace_API.Odvadeni.FormIDPracovnika())
                            {
                                if (fidprac.ShowDialog(this) == DialogResult.Cancel)
                                    return;
                                filtrOsoba = fidprac.Pracovnik.id.Trim();
                            }
                        }
                    }
                    else //if (dlgfs.Osoba == Osoba.DleFiltru)
                    {
                    }

                    if (dlgfs.Stroj == Stroj.Nastaveny)
                    {
                        if (!String.IsNullOrEmpty(Settings.MachineID))
                            filtrStroj = Settings.MachineID;
                        else
                        {
                            using(Odvadeni.FormIDMachine fidmachine = new Fask.Aktualizace_API.Odvadeni.FormIDMachine())
                            {
                                if (fidmachine.ShowDialog(this) == DialogResult.Cancel)
                                    return;
                                filtrStroj = fidmachine.Machine.id.Trim();
                            }
                        }
                    }
                    else //if (dlgfs.Stroj == Stroj.DleFiltru)
                    {
                    }

                    filterActive = true;
                    timerAutoUpdate.Change(0, Settings.ModulPrehledOdvoduAutoUpdateInterval);    // okamžité načtení dat
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void buttonStatistikaOdvadeni_Click(object sender, EventArgs e)
        {
            try
            {
                using (Fask.Aktualizace_API.Forms.FormPrehledFiltr ff = new Fask.Aktualizace_API.Forms.FormPrehledFiltr())
                {
                    ff.filter.filtrDatumDo = filtrDatumDo;
                    ff.filter.filtrDatumOd = filtrDatumOd;
                    ff.filter.filtrOsoba = filtrOsoba;
                    ff.filter.filtrZakazka = filtrZakazka;
                    ff.filter.filtrStroj = filtrStroj;
                    ff.filter.filtrNedokonceneZakazky = filtrNedokonceneZakazky;

                    ff.ShowDialog();
                }
            }
            catch (Exception ex) 
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


    }
}
