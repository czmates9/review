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
    public partial class Form_PlanovaniVarianty : Form
    {
        #region Parametry

        private Fask.Interfaces.IMES provider = null;

        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANIRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_Varianty.BindingContext[bs_Varianty].Current)).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANIRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow _radek_Parametry = null;
        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow Radek_Parametry
        {
            set { _radek_Parametry = value; }
            get { return _radek_Parametry; }
        }

        #endregion

        #region Eventy formu

        public Form_PlanovaniVarianty()
        {
            InitializeComponent();

            this.dg_Varianty.UpdateColumnHeaderCellsByDatasource();
            buttonsPanel1.Menu = menuStrip1;
        }

        private void Form_PlanovaniVarianty_Load(object sender, EventArgs e)
        {
            this.dg_Varianty.LoadConfiguration(this.GetType().ToString());

            buttonsPanel1.LoadConfiguration(this.GetType().ToString());
            buttonsPanel1.Init();

            advancedDataGridViewSearchToolBar1.SetColumns(dg_Varianty.Columns);

            // inicializace providera
            InitProvider();

            if (provider == null)
                throw new Exception("Provider 'Vyroba' není inicializován");

            PerformInicializaceParametru();
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


        private void tsmi_vybratVariantu_Click(object sender, EventArgs e)
        {
            if (!PerformVybrat())
                return;
            DialogResult = DialogResult.OK;
        }


        private void tsmi_novaVarianta_Click(object sender, EventArgs e)
        {
            PerformNova();
            PerformRefresh();
        }

        private void tsmi_smazatVariantu_Click(object sender, EventArgs e)
        {
            PerformSmazat();
            PerformRefresh();
        }

        private void tsmi_upravitVariantu_Click(object sender, EventArgs e)
        {
            PerformUpravit();
            PerformRefresh();
        }

        private void tsmi_duplikaceVarianty_Click(object sender, EventArgs e)
        {
            PerformDuplikace();
            PerformRefresh();
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

                int FirstDisplayedScrollingRowIndex = this.dg_Varianty.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Varianty.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Varianty.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Varianty.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (System.Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformNova()
        {
            try
            {
                var G = Guid.NewGuid();

                using (Form_PlanovaniVarianty_Params frm = new Form_PlanovaniVarianty_Params())
                {
                    frm.SetGuid(G);
                    frm.Flag_New = true;

                    DialogResult dres = frm.ShowDialog();

                    if (dres != DialogResult.OK)
                        return;

                }

                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název varianty", "Zadejte název varianty", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;


                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertVariantu))
                    ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertVariantu)provider).InsertVariantu(G, nazev);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_InsertVariantu.");

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }

        private void PerformSmazat()
        {

            try
            {
                var dr =  MessageBox.Show(this, "Opravdu smazat?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr != DialogResult.Yes)
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Neni vybrán žadný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_DeleteVariantu))
                    ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_DeleteVariantu)provider).DeleteVariantu(SelectedRow.GUID);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_DeleteVariantu.");


            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            
        }

        private void PerformUpravit()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Neni vybrán žadný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (Form_PlanovaniVarianty_Params frm = new Form_PlanovaniVarianty_Params())
                {
                    frm.SetGuid(SelectedRow.GUID);
                    frm.Flag_New = false;

                    DialogResult dres = frm.ShowDialog();

                    if (dres != DialogResult.OK)
                        return;

                }

                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název varianty", "Zadejte název varianty", SelectedRow.DESC, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;


                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateVariantu))
                    ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateVariantu)provider).UpdateVariantu(SelectedRow.GUID, nazev);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_UpdateVariantu.");

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }

        private void PerformDuplikace()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Neni vybrán žadný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Guid G_new = Guid.NewGuid();
                Guid G =  SelectedRow.GUID;



                Fask.Interfaces.DataSets.Vyroba_Planovani ds = null;

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetStav_ParamsNastaveni))
                    ds = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetStav_ParamsNastaveni)provider).GetStav_ParamsNastaveni(G);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_GetStav_ParamsNastaveni.");

                if (ds != null)
                {

                    string nazev = string.Empty;
                    DialogResult dr = Forms.InputBox.Show("Název varianty", "Zadejte název varianty", SelectedRow.DESC, false, out nazev);
                    if (dr != System.Windows.Forms.DialogResult.OK)
                        return;


                    if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertVariantu))
                        ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertVariantu)provider).InsertVariantu(G_new, nazev);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_InsertVariantu.");

                    ds.FASK_PLANOVANI_PARAMS_Nastaveni.ToList().ForEach(x => x.GUID_PLANOVANI = G_new);

                    if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertParams))
                        ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertParams)provider).InsertParams(ds);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_InsertParams.");

                }


            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }

        private bool PerformVybrat()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Neni vybrán žadný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetRow_Parametry))
                    Radek_Parametry = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetRow_Parametry)provider).GetRow_Parametry(SelectedRow.GUID);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_GetRow_Parametry.");

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        private void PerformInicializaceParametru()
        {
            try
            {
                Guid G = Guid.NewGuid();

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_InitEmptyParams))
                    ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_InitEmptyParams)provider).InitEmptyParams(G);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_InitEmptyParams.");

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
                bool endcol = dg_Varianty.CurrentCell.ColumnIndex + 1 >= dg_Varianty.ColumnCount;
                bool endrow = dg_Varianty.CurrentCell.RowIndex + 1 >= dg_Varianty.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_Varianty.CurrentCell.ColumnIndex;
                    startRow = dg_Varianty.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_Varianty.CurrentCell.ColumnIndex + 1;
                    startRow = dg_Varianty.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_Varianty.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_Varianty.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_Varianty.CurrentCell = c;
        }

        #endregion

        #region BW Vyhledavani

        private void bw_Varianty_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bw_Varianty.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.DataSets.Vyroba_Planovani ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.PV.IPV_Navrh_FillStav))
                    ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_FillStav)provider).FillStav(ds);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_FillStav.");

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
                ds_Varianty = (Fask.Interfaces.DataSets.Vyroba_Planovani)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    bs_Varianty.DataSource = ds_Varianty;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    bs_Varianty.DataSource = ds_Varianty;
                }
                else
                {
                    bs_Varianty.DataSource = ds_Varianty;
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
                this.progressIndicator1.Location = new Point(this.dg_Varianty.Location.X + (this.dg_Varianty.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_Varianty.Location.Y + (this.dg_Varianty.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }


        #endregion

        #region Datagridview vyber stavu radiobutton

        //private void dg_Varianty_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0)
        //        return;

        //    if (e.ColumnIndex == Stav.Index)
        //    {
        //        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dg_Varianty.Rows[e.RowIndex].Cells[Stav.Index];
        //        cell.Value = true;
        //        radioButtonChanged();
        //    }
        //}

        //private void dg_Varianty_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.ColumnIndex == Stav.Index && e.RowIndex >= 0)
        //    {
        //        e.PaintBackground(e.ClipBounds, true);

        //        Rectangle rectRadioButton = new Rectangle();
        //        rectRadioButton.Width = 14;
        //        rectRadioButton.Height = 14;
        //        rectRadioButton.X = e.CellBounds.X + (e.CellBounds.Width - rectRadioButton.Width) / 2;
        //        rectRadioButton.Y = e.CellBounds.Y + (e.CellBounds.Height - rectRadioButton.Height) / 2;

        //        ButtonState buttonState;

        //        if (e.Value == DBNull.Value || (bool)(e.Value) == false)
        //        {
        //            buttonState = ButtonState.Normal;
        //        }
        //        else
        //        {
        //            buttonState = ButtonState.Checked;
        //        }

        //        ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, buttonState);

        //        e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);

        //        e.Handled = true;
        //    }
        //}

        //private void dg_Varianty_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        //{
        //    radioButtonChanged();
        //}

        //private void radioButtonChanged()
        //{
        //    if (dg_Varianty.CurrentCell.ColumnIndex == Stav.Index)
        //    {
        //        foreach (DataGridViewRow row in dg_Varianty.Rows)
        //        {
        //            if (row.Index != dg_Varianty.CurrentCell.RowIndex)
        //            {
        //                row.Cells[Stav.Index].Value = false;
        //            }
        //        }
        //    }
        //}



        #endregion


    }
}
