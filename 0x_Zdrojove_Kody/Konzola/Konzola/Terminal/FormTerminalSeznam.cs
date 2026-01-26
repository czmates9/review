using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;
using Konzola.Android;
using RestSharp;
using System.Net;
using Fask.RestSharp.API;
using FASK.Logins.WEBAPI;
using Fask.ModuleSql_API;
using FASK.Logins.Komunikace;

namespace Konzola.Terminal
{
    public partial class FormTerminalSeznam : Form
    {
        private Fask.Interfaces.IMES providerTerminal = null;
        //private bool priznakViceRadku = false;
        private object lock_priznakViceRadku = new object(); //locker pro Nakladka_data 

        public Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_Terminaly.BindingContext[bs_Terminaly].Current)).Row as Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public FormTerminalSeznam()
        {
            InitializeComponent();
            this.dg_Terminaly.UpdateColumnHeaderCellsByDatasource();
        }

        #region Update

        private void ProgressIndicatorVyrobekStop()
        {
            progressIndicatorUpdate.Stop();
            progressIndicatorUpdate.Visible = false;
        }

        private void ProgressIndicatorVyrobekStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorUpdate.Location = new Point(this.dg_Terminaly.Location.X + (this.dg_Terminaly.Width / 2) - (progressIndicatorUpdate.Size.Width / 2), this.dg_Terminaly.Location.Y + (this.dg_Terminaly.Height / 2) - (progressIndicatorUpdate.Size.Height / 2));
            }
            catch { }
            progressIndicatorUpdate.Start();
            progressIndicatorUpdate.Visible = true;
        }

        #endregion

        private void bw_Load_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ProductionListFiltr filtr = (Fask.Interfaces.Filtry.ProductionListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Terminal ds = new Fask.Interfaces.DataSets.Terminal();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_Load.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((providerTerminal != null) && providerTerminal is Fask.Interfaces.Terminal.ITerminal_GetTerminalAll)
                    ds = ((Fask.Interfaces.Terminal.ITerminal_GetTerminalAll)providerTerminal).GetTerminalAll();
                else
                    throw new Exception("ITerminal_GetTerminalAll not implementet");
                //providerVazby.ITEMNMBR_Materialy = String.Empty;

                if (bw_Load.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_Load_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    this.dsTerminaly = new Fask.Interfaces.DataSets.Terminal();
                    bs_Terminaly.DataSource = this.dsTerminaly;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.dsTerminaly = new Fask.Interfaces.DataSets.Terminal();
                    bs_Terminaly.DataSource = this.dsTerminaly;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.dsTerminaly = (Fask.Interfaces.DataSets.Terminal)e.Result;
                    if (this.dsTerminaly == null)
                        this.dsTerminaly = new Fask.Interfaces.DataSets.Terminal();

                    bs_Terminaly.DataSource = this.dsTerminaly;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorVyrobekStop();
            }
        }

        private void FormTerminalSeznam_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                this.dg_Terminaly.LoadConfiguration(this.GetType().ToString());

                advancedDataGridViewSearchToolBar1.SetColumns(dg_Terminaly.Columns);

                InitProvider();

                if (providerTerminal == null)
                    throw new Exception("Provider 'Terminal' není inicializován");

                PerformVyhledat();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicializace providera Production
        /// </summary>
        private void InitProvider()
        {
            #region Productions
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerTerminal == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Terminal.ITerminal).IsAssignableFrom(t))
                            {
                                providerTerminal = (Fask.Interfaces.Terminal.ITerminal)providerAssemlby.CreateInstance(t.FullName);
                                if (providerTerminal != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                if(providerTerminal != null)
                    providerTerminal.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }

        private void PerformVyhledat()
        {

            try
            {
                DataTable dtchanged = this.dsTerminaly.CZMST_TERMINAL_ALL.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Load.IsBusy)
                {
                    bw_Load.CancelAsync();
                    while (bw_Load.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                //Fask.Interfaces.Filtry.ProductionListFiltr filtr = new Fask.Interfaces.Filtry.ProductionListFiltr();
                //if (!CreateFilter(ref filtr))
                //    return;

                int FirstDisplayedScrollingRowIndex = this.dg_Terminaly.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Load.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Terminaly.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Terminaly.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_aktualizovat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        private void btn_VNC_Click(object sender, EventArgs e)
        {
            //1. ping zda je online??
            //2. pokusit se pripojit zda je zapnute VNC
            //3. vyžadat vzdalene zapnuti VNC????
            if (dg_Terminaly.SelectedRows.Count > 1)
            {
                MessageBox.Show(this, "Byl vybrán větší počet řádků než jeden!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (dg_Terminaly.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Nebyl vybrán žádný řádek!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            string IP = SelectedRow.IP;

            if (VNCPing.PingHost(IP))
            {
                //OK je dopingnutelna tak ze mužu pokusit pripojit...

                using (FormVNC frm = new FormVNC(IP, 5900))
                {
                    frm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show(this, "Zařízení s IP:'" + IP + "' momentalne neni dostupné.", "Info", MessageBoxButtons.OK , MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_Terminaly.CurrentCell.ColumnIndex + 1 >= dg_Terminaly.ColumnCount;
                bool endrow = dg_Terminaly.CurrentCell.RowIndex + 1 >= dg_Terminaly.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_Terminaly.CurrentCell.ColumnIndex;
                    startRow = dg_Terminaly.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_Terminaly.CurrentCell.ColumnIndex + 1;
                    startRow = dg_Terminaly.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_Terminaly.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_Terminaly.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_Terminaly.CurrentCell = c;
        }

        private void FormTerminalSeznam_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dg_Terminaly.SaveConfiguration(this.GetType().ToString());
        }

        private void btn_konf_andr_Click(object sender, EventArgs e)
        {
            try
            {
                if (dg_Terminaly.SelectedRows.Count > 1)
                {
                    MessageBox.Show(this, "Byl vybrán větší počet řádků než jeden!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (dg_Terminaly.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Nebyl vybrán žádný řádek!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                #region logika

                MES_Android.Konfigurace data_konfigurace = null;
                //1. ping zda je online??
                //2. pokusit se pripojit zda je zapnute VNC
                //3. vyžadat vzdalene zapnuti VNC????

                string IP = SelectedRow.IP;
                int ID_TERMINAL = SelectedRow.ID_TERMINAL;
                DateTime dat = SelectedRow.DATEREQ;
                string db_type = string.Empty;

                if (!SelectedRow.IsDB_TYPENull())
                {
                    db_type = SelectedRow.DB_TYPE;
                }

                // je typu android? tak muzu pokracovat, jinak hlaska!
                if (db_type == "MES_Android")
                {
                    //ziskam konfiguraci ze servru -- metoda volani na server + vraceni dat ke konfiguraci
                    string StatusCode;
                    string kon;

                    if ((providerTerminal != null) && providerTerminal is Fask.Interfaces.Terminal.ITerminal_GetTerminalKonfigurace_MESAndroid)
                        StatusCode = ((Fask.Interfaces.Terminal.ITerminal_GetTerminalKonfigurace_MESAndroid)providerTerminal).GetTerminalKonfigurace_MESAndroid(ID_TERMINAL, out kon);
                    else
                        throw new Exception("ITerminal_GetTerminalKonfigurace not implementet");

                    if (string.IsNullOrEmpty(StatusCode))
                    {
                        string msg = "Na serveru nebyla nalezena konfigurace pro Váš terminál.";
                        MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (StatusCode == "Created")
                    {
                        string msg = "Na serveru nebyla nalezena konfigurace pro Váš terminál. POZOR, byla vytvořená výchozí!";
                        MessageBox.Show(this, msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    data_konfigurace = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.Konfigurace>(kon);

                }
                else if (db_type == "test")
                {

                }
                else
                {
                    //neni android, ukoncuji proces
                    MessageBox.Show(this, string.Format("Nelze otevřít editaci konfigurace: '{0}'", db_type), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                //vezmu ID, poslu na server .. ten mi vrati 200 OK a konfiguraci na serveru nebo 202 CREATED, vrati konfiguraci ale jen defaultni nebo to hodi 404 chybu

                if (data_konfigurace != null)
                {
                    using (Form_AndroidKonfig frm = new Form_AndroidKonfig(IP, ID_TERMINAL, dat, data_konfigurace))
                    {
                        frm.WindowState = FormWindowState.Maximized;
                        // frm.Show();

                        var dr = frm.ShowDialog();
                        if (dr == DialogResult.OK || dr == DialogResult.Abort)
                        {
                            MessageBox.Show(this, string.Format("Pro danný terminál ID: '{0}' správa konfigurace proběhla v pořádku.", ID_TERMINAL), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            //return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, string.Format("Pro danný terminál ID: '{0}' nebyla dohledána konfigurace!", ID_TERMINAL), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                #endregion

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btn_konf_validace_Click(object sender, EventArgs e)
        {
            bool priznakPrubehuValidace = false;
            try
            {
                MES_Android.Konfigurace data_konfigurace = new MES_Android.Konfigurace();

                int countSelectedRows = 0;

                countSelectedRows = dg_Terminaly.SelectedRows.Count;

                if (countSelectedRows == 0)
                {
                    MessageBox.Show(this, "Nebyl vybrán žádný řádek!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (countSelectedRows == 1)
                {


                    string IP = SelectedRow.IP;
                    int ID_TERMINAL = SelectedRow.ID_TERMINAL;
                    DateTime dat = SelectedRow.DATEREQ;
                    string db_type = string.Empty;

                    if (!SelectedRow.IsDB_TYPENull())
                    {
                        db_type = SelectedRow.DB_TYPE;
                    }

                    // je typu android? tak muzu pokracovat, jinak hlaska!
                    if (db_type == "MES_Android")
                    {
                        //validace dat konfigurace:                     
                        if ((providerTerminal != null) && providerTerminal is Fask.Interfaces.Terminal.ITerminal_GetTerminalKonfigurace_MESAndroid_Validace)
                            priznakPrubehuValidace = ((Fask.Interfaces.Terminal.ITerminal_GetTerminalKonfigurace_MESAndroid_Validace)providerTerminal).GetTerminalKonfigurace_MESAndroid_Validace(ID_TERMINAL);
                        else
                            throw new Exception("ITerminal_GetTerminalKonfigurace_MESAndroid_Validace not implementet");
                                             
                        if (priznakPrubehuValidace)
                        {
                            string msg = "Aktualizace a validace konfigurace proběhla v pořádku!!";
                            MessageBox.Show(this, msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string msg = "Aktualizace a validace konfigurace selhala!!";
                            MessageBox.Show(this, msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return; 
                    }
                    else
                    {
                        MessageBox.Show(this, "Vybraná konfigurace není typu MES_Android!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }

                }
                else
                {
                    //cyklem projit radky a ziskat data

                    //validace dat konfigurace
                    return; //---------------------POZOR DODELAT !!!!

                }

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
