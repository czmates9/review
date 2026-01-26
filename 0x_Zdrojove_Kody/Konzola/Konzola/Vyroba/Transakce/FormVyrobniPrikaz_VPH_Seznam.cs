using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;



namespace Konzola.Vyroba
{
    public partial class FormVyrobniPrikaz_VPH_Seznam : Form
    {

        private Fask.Interfaces.IMES providerVPH = null;

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_VPH.BindingContext[bs_VPH].Current)).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        public FormVyrobniPrikaz_VPH_Seznam()
        {
            InitializeComponent();
            this.dg_VPH.UpdateColumnHeaderCellsByDatasource();
        }

        private void FormVyrobniPrikaz_VPH_Seznam_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                this.dg_VPH.LoadConfiguration(this.GetType().ToString());

                advancedDataGridViewSearchToolBar1.SetColumns(dg_VPH.Columns);

                // inicializace providera
                InitProvider();

                if (providerVPH == null)
                    throw new Exception("Provider 'VPH' není inicializován");

                PerformVyhledat();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void FormVyrobniPrikaz_VPH_Seznam_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dg_VPH.SaveConfiguration(this.GetType().ToString());
        }


        #region Konec

        private void PerformCancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPH == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPH.IVPH).IsAssignableFrom(t))
                            {
                                providerVPH = (Fask.Interfaces.Vyroba.VPH.IVPH)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPH != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPH.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_VPH.CurrentCell.ColumnIndex + 1 >= dg_VPH.ColumnCount;
                bool endrow = dg_VPH.CurrentCell.RowIndex + 1 >= dg_VPH.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_VPH.CurrentCell.ColumnIndex;
                    startRow = dg_VPH.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_VPH.CurrentCell.ColumnIndex + 1;
                    startRow = dg_VPH.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_VPH.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_VPH.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_VPH.CurrentCell = c;


        }

        private void dg_VPH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
            }
            catch
            {
            }
        }

        private void PerformOK()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.ds_VPH.CZPRO_VPH.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_VPH.IsBusy)
                {
                    bw_VPH.CancelAsync();
                    while (bw_VPH.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                //Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
                //if (!CreateFilter(ref filtr))
                //    return;

                int FirstDisplayedScrollingRowIndex = this.dg_VPH.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                //bw_VPH.RunWorkerAsync(filtr);
                bw_VPH.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_VPH.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_VPH.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_VPH_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Fask.Interfaces.Filtry.ZboziListFiltr filtr = (Fask.Interfaces.Filtry.ZboziListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_VPH.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((providerVPH != null) && (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill))
                {
                   ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(ds);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IVPH_Fill");
                }

                if (bw_VPH.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bw_VPH_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_VPH = new Fask.Interfaces.DataSets.Vyroba();
                    bs_VPH.DataSource = ds_VPH;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    ds_VPH = new Fask.Interfaces.DataSets.Vyroba();
                    bs_VPH.DataSource = ds_VPH;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_VPH = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (ds_VPH == null)
                        ds_VPH = new Fask.Interfaces.DataSets.Vyroba();

                    bs_VPH.DataSource = ds_VPH;
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        private void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        private void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_VPH.Location.X + (this.dg_VPH.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_VPH.Location.Y + (this.dg_VPH.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVybrat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        


    }
}
