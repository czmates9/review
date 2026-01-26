using Fask.Interfaces.DataSets;
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

namespace Konzola.Planovani.Planovani_Navrhar
{
    public partial class Form_PlanovaniVarianty_Params : Form
    {
        #region Parametry

        private Fask.Interfaces.IMES provider = null;

        private Guid _guid = Guid.Empty;
        public void SetGuid(Guid guid)
        {
            _guid = guid;
        }

        private bool _flag_New = false;
        public bool Flag_New
        {
            set { _flag_New = value; }
            get { return _flag_New; }
        }

        #endregion

        #region Eventy formu

        public Form_PlanovaniVarianty_Params()
        {
            InitializeComponent();

            this.dg_Params.UpdateColumnHeaderCellsByDatasource();
            buttonsPanel1.Menu = menuStrip1;
        }

        private void Form_PlanovaniVarianty_Params_Load(object sender, EventArgs e)
        {
            this.dg_Params.LoadConfiguration(this.GetType().ToString());

            buttonsPanel1.LoadConfiguration(this.GetType().ToString());
            buttonsPanel1.Init();

            advancedDataGridViewSearchToolBar1.SetColumns(dg_Params.Columns);

            // inicializace providera
            InitProvider();

            if (provider == null)
                throw new Exception("Provider 'Vyroba' není inicializován");

            PerformRefresh();
            
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

        #region Menu click eventy

        private void tsmi_Konec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmi_Ulozit_Click(object sender, EventArgs e)
        {
            PerformSave();

            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Perform Metody

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformRefresh()
        {
            try
            {

                ProgressIndicatorStart();

                int FirstDisplayedScrollingRowIndex = this.dg_Params.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Varianty.RunWorkerAsync(_flag_New);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Params.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Params.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (System.Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformSave()
        {
            try
            {
                if (_flag_New)
                {
                    if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertParams))
                        ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertParams)provider).InsertParams(ds_Params);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_InsertParams.");
                }
                else
                {
                    if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateParams))
                        ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateParams)provider).UpdateParams(ds_Params);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_UpdateParams.");
                }


            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
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
                bool endcol = dg_Params.CurrentCell.ColumnIndex + 1 >= dg_Params.ColumnCount;
                bool endrow = dg_Params.CurrentCell.RowIndex + 1 >= dg_Params.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_Params.CurrentCell.ColumnIndex;
                    startRow = dg_Params.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_Params.CurrentCell.ColumnIndex + 1;
                    startRow = dg_Params.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_Params.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_Params.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_Params.CurrentCell = c;
        }

        #endregion

        #region BW Vyhledavani

        private void bw_Varianty_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                bool InitNew =  (bool)e.Argument;

                if (bw_Varianty.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.DataSets.Vyroba_Planovani ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

                if (InitNew)
                {
                    if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_InitEmptyParams))
                        ds = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_InitEmptyParams)provider).InitEmptyParams(_guid);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_InitEmptyParams.");

                }
                else
                {
                    if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetStav_ParamsNastaveni))
                        ds = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetStav_ParamsNastaveni)provider).GetStav_ParamsNastaveni(_guid);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_GetStav.");
                }

                if (bw_Varianty.CancellationPending)
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

        private void bw_Varianty_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ds_Params = (Fask.Interfaces.DataSets.Vyroba_Planovani)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    bs_Params.DataSource = ds_Params;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    bs_Params.DataSource = ds_Params;
                }
                else
                {
                    bs_Params.DataSource = ds_Params;
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
                this.progressIndicator1.Location = new Point(this.dg_Params.Location.X + (this.dg_Params.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_Params.Location.Y + (this.dg_Params.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        #endregion


        void dg_Params_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dg_Params.IsCurrentCellDirty)
            {
                dg_Params.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dg_Params_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dg_Params.Columns[e.ColumnIndex].Name == "valueDataGridViewCheckBoxColumn")
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dg_Params.Rows[e.RowIndex].Cells["valueDataGridViewCheckBoxColumn"];
                var x = !(Boolean)checkCell.Value;
                dg_Params.Invalidate();

                CellValueChanged(e, x);
            }
        }


        private void CellValueChanged(DataGridViewCellEventArgs e, bool Stav)
        {

            string Name_Value = ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni.ValueColumn.ColumnName;
            string Name_Original = ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni.ColumnName_OriginalColumn.ColumnName;

            string Name_Sluc = ds_Params.FASK_PLANOVANI_PARAMS.SlucovaniColumn.ColumnName;

            string Name_SlucOdber = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoOdberatelyColumn.ColumnName;
            string Name_SlucMN = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstviColumn.ColumnName;

            string Name_SlucMN_OBJ = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstvi_ObjednavkaColumn.ColumnName;

            string Name_SlucMN10 = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstvi_10Column.ColumnName;
            string Name_SlucMN20 = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstvi_20Column.ColumnName;
            string Name_SlucMN30 = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstvi_30Column.ColumnName;
            string Name_SlucMN40 = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstvi_40Column.ColumnName;
            string Name_SlucMN50 = ds_Params.FASK_PLANOVANI_PARAMS.Slucovani_PoMnozstvi_50Column.ColumnName;


            if (ds_Params == null)
                return;

            if (ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni == null)
                return;

            if (ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni.Count == 0)
                return;

            int Index_ROW = e.RowIndex;
            int Index_Column = e.ColumnIndex;

            var Row = ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni[Index_ROW];
            var NazevStlopce = ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni.Columns[Index_Column].ColumnName;

            if (NazevStlopce != Name_Value)
                return;

            //bool StavValue = (bool)Row[Name_Value];
            string Param = Row[Name_Original].ToString();

            if (Param == Name_Sluc)
            {
                if (!Stav)
                {
                    //ChangeStav_RowByOriginal(false, Name_SlucMN);
                    //ChangeStav_RowByOriginal(false, Name_SlucOdber);
                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(false, Name_SlucOdber);
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                }
            }
            else if (Param == Name_SlucOdber)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                }
                else
                {

                }
            }
            else if (Param == Name_SlucMN)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucOdber);
                    ChangeStav_RowByOriginal(true, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);

                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                }
            }
            else if (Param == Name_SlucMN_OBJ)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucOdber);
                    ChangeStav_RowByOriginal(true, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);

                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                }
            }
            else if (Param == Name_SlucMN10)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(true, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                }
            }
            else if (Param == Name_SlucMN20)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(true, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                }
            }
            else if (Param == Name_SlucMN30)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(true, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                }
            }
            else if (Param == Name_SlucMN40)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN50);
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(true, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                }
            }
            else if (Param == Name_SlucMN50)
            {
                if (!Stav)
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN10);
                    ChangeStav_RowByOriginal(false, Name_SlucMN20);
                    ChangeStav_RowByOriginal(false, Name_SlucMN30);
                    ChangeStav_RowByOriginal(false, Name_SlucMN40);
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(true, Name_SlucMN_OBJ);
                    ChangeStav_RowByOriginal(true, Name_Sluc);
                }
                else
                {
                    ChangeStav_RowByOriginal(false, Name_SlucMN);
                    ChangeStav_RowByOriginal(false, Name_Sluc);
                    ChangeStav_RowByOriginal(false, Name_SlucMN_OBJ);
                }
            }
        }

        private void ChangeStav_RowByOriginal(bool stav, string Name_XXX)
        {
            Vyroba_Planovani.FASK_PLANOVANI_PARAMS_NastaveniRow row = ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni.Where(x => x.ColumnName_Original == Name_XXX).First();
            row.Value = stav;
            //ds_Params.FASK_PLANOVANI_PARAMS_Nastaveni.AcceptChanges();
        }
    }
}
