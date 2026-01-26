using Konzola.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Planovani.Planovani_Detail
{
    public partial class Form_Planovani_Detail : Form
    {
        #region Parametry
        
        private Fask.Interfaces.IMES provider = null;

        private string _itemcode = string.Empty;
        public string ITEMCODE 
        {
            get { return _itemcode; }
            set { _itemcode = value; }
        }

        private string _itemnmbr = string.Empty;
        public string ITEMNMBR
        {
            get { return _itemnmbr; }
            set { _itemnmbr = value; }
        }

        #endregion

        #region Eventy formu

        public Form_Planovani_Detail()
        {
            InitializeComponent();

            this.dg_detail.UpdateColumnHeaderCellsByDatasource();

        }

        private void Form_Planovani_Detail_Load(object sender, EventArgs e)
        {

            this.dg_detail.LoadConfiguration(this.GetType().ToString());

            advancedDataGridViewSearchToolBar1.SetColumns(dg_detail.Columns);

            // inicializace providera
            InitProvider();

            if (provider == null)
                throw new Exception("Provider 'Vyroba' není inicializován");

            PerformRefresh();
        }

        #endregion

        #region Perform metody

        private void PerformRefresh()
        {
            try
            {

                ProgressIndicatorStart();

                int FirstDisplayedScrollingRowIndex = this.dg_detail.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_detail.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_detail.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_detail.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (System.Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Inicializce provideru

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.PV.IPV).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.Vyroba.PV.IPV)providerAssemlby.CreateInstance(t.FullName);
                                if (provider != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                provider.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion


        #region AdvanceDataGridView searchToolBar

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = this.dg_detail.CurrentCell.ColumnIndex + 1 >= this.dg_detail.ColumnCount;
                bool endrow = this.dg_detail.CurrentCell.RowIndex + 1 >= this.dg_detail.RowCount;

                if (endcol && endrow)
                {
                    startColumn = this.dg_detail.CurrentCell.ColumnIndex;
                    startRow = this.dg_detail.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : this.dg_detail.CurrentCell.ColumnIndex + 1;
                    startRow = this.dg_detail.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = this.dg_detail.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = this.dg_detail.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                this.dg_detail.CurrentCell = c;
        }

        #endregion

        #region BW Vyhledavani

        private void bw_detail_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bw_detail.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.DataSets.Vyroba_Planovani ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetDetailStav))
                    ds = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetDetailStav)provider).GetDetailStav(_itemcode);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_FillDetailStav.");

                if (bw_detail.CancellationPending)
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

        private void bw_detail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ds_detail = (Fask.Interfaces.DataSets.Vyroba_Planovani)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    bs_detail.DataSource = ds_detail;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    bs_detail.DataSource = ds_detail;
                }
                else
                {
                    bs_detail.DataSource = ds_detail;
                }

                dg_detail.ClearSelection();

                foreach (DataGridViewRow item in dg_detail.Rows)
                {
                    var radek = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.Planovani_DetailRow;

                    if (radek.ITEMNMBR.Trim() == _itemnmbr.Trim())
                    {
                        item.Selected = true;
                    }
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
                this.progressIndicator1.Location = new Point(this.dg_detail.Location.X + (this.dg_detail.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_detail.Location.Y + (this.dg_detail.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        #endregion

    }
}
