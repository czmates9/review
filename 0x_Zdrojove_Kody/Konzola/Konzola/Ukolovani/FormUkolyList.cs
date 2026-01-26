using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Konzola.Extensions;
using Konzola;
using System.Reflection;

namespace Konzola.Ukolovani
{
    public partial class FormUkolyList : Form
    {

        private Fask.Interfaces.IMES providerUkol = null;
        /// <summary>
        /// Vybraný úkol
        /// </summary>
        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow rowUkol
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgUkoly.BindingContext[bsUkoly].Current)).Row as Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        public FormUkolyList()
        {
            InitializeComponent();

            this.dgUkoly.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;

        }

        private void FormUkolyList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgUkoly.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgUkoly.Columns);



                InitProvider();

                if (providerUkol == null)
                    throw new Exception("Provider 'Ukolovani' není inicializován");

                buttonVyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void FormUkolyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgUkoly.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUkolyList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerUkol == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ukolovani.IUkolovani).IsAssignableFrom(t))
                                {
                                    providerUkol = (Fask.Interfaces.Ukolovani.IUkolovani)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerUkol != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerUkol.InitProvider();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aktualizace dat po aktualizaci.
        /// </summary>
        private void UpdateForm()
        {
            try
            {
                //var lta = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //lta.Fill(this.ukolovaniDataset1.CZ_UKOL);

                if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL))
                    ((Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL)providerUkol).Fill_CZ_UKOL(this.dsUkoly);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Fill_CZ_UKOL");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Performy
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

        private void PerformCreateRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (FormUkolyEdit frmukol = new FormUkolyEdit())
                {
                    frmukol.Text = "Nový úkol";
                    if (frmukol.ShowDialog(this) != DialogResult.OK)
                        return;

                    if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL))
                    {
                        ((Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL)providerUkol).Insert(
                        frmukol.rowUkolNew.Name,
                        string.IsNullOrEmpty(frmukol.rowUkolNew.Description) ? null : frmukol.rowUkolNew.Description,
                        frmukol.rowUkolNew.IsCodeNull() ? null : frmukol.rowUkolNew.Code,
                        frmukol.rowUkolNew.CreatorID,
                        frmukol.rowUkolNew.DateCreated,
                        frmukol.rowUkolNew.IsDateFromNull() ? null : (DateTime?)frmukol.rowUkolNew.DateFrom,
                        frmukol.rowUkolNew.IsDateToNull() ? null : (DateTime?)frmukol.rowUkolNew.DateTo,
                        frmukol.rowUkolNew.State,
                        frmukol.rowUkolNew.IsKindNull() ? null : frmukol.rowUkolNew.Kind,
                        frmukol.rowUkolNew.IsTypeNull() ? null : frmukol.rowUkolNew.Type,
                        frmukol.rowUkolNew.Priority,
                        frmukol.rowUkolNew.IsPartnerIDNull() ? null : frmukol.rowUkolNew.PartnerID);
                    }
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Insert_CZ_UKOL");


                    //var lta = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
                    //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                    //lta.Insert(
                    //    frmukol.rowUkolNew.Name,
                    //    //frmukol.rowUkolNew.isde
                    //    string.IsNullOrEmpty(frmukol.rowUkolNew.Description) ? null : frmukol.rowUkolNew.Description,
                    //    //frmukol.rowUkolNew.Description,
                    //    frmukol.rowUkolNew.IsCodeNull() ? null : frmukol.rowUkolNew.Code,
                    //    frmukol.rowUkolNew.CreatorID,
                    //    frmukol.rowUkolNew.DateCreated,
                    //    frmukol.rowUkolNew.IsDateFromNull() ? null : (DateTime?) frmukol.rowUkolNew.DateFrom,
                    //    frmukol.rowUkolNew.IsDateToNull() ? null : (DateTime?)frmukol.rowUkolNew.DateTo,
                    //    frmukol.rowUkolNew.State,
                    //    frmukol.rowUkolNew.IsKindNull() ? null : frmukol.rowUkolNew.Kind,
                    //    frmukol.rowUkolNew.IsTypeNull() ? null : frmukol.rowUkolNew.Type,
                    //    frmukol.rowUkolNew.Priority,
                    //    frmukol.rowUkolNew.IsPartnerIDNull() ? null : frmukol.rowUkolNew.PartnerID);
                    ////lta.Insert(frmuziv.newgroupsrow.id, frmuziv.newgroupsrow.name, frmuziv.newgroupsrow.description);

                    //lta.Fill(ukolovaniDataset1.CZ_UKOL);

                    if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL))
                        ((Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL)providerUkol).Fill_CZ_UKOL(this.dsUkoly);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Fill_CZ_UKOL");
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowUkol == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormUkolyEdit frmukol = new FormUkolyEdit())
                {
                    frmukol.rowUkolEdit = rowUkol;
                    frmukol.Text = "Úprava úkolu";
                    if (frmukol.ShowDialog(this) != DialogResult.OK)
                        return;
                    //var lta = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
                    //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                    ////lta.Delete(rowUkol.ITEMNMBR);
                    //lta.Update(frmukol.rowUkolEdit);


                    if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Update_Row_CZ_UKOL))
                        ((Fask.Interfaces.Ukolovani.IUkolovani_Update_Row_CZ_UKOL)providerUkol).UpdateRow(frmukol.rowUkolEdit);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Update_CZ_UKOL");


                    //lta.Update(this.vyrobaDataSet1.Production);
                    this.dsUkoly.AcceptChanges();
                    //UpdateForm();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowUkol == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //MessageBox.Show("Chcete smazat uživatele " + rowUkol.firstname + " " + rowUkol.surname + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MessageBox.Show("Opravdu chcete odstranit úkol " + rowUkol.Description.Trim() + " a jeho veškeré přidělení uživatelům?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                // odstraneni navaznosti
                //var taUkol = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
                //taUkol.Connection.ConnectionString = Properties.Settings.Default.Ukolovani_ConnectionString;
                //taUkol.Delete(rowUkol.ID);

                //var taUkolUziv = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
                //taUkolUziv.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //taUkolUziv.DeleteByUkolID(rowUkol.ID);

                if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_DeleteByUkolID_CZ_UKOL_UZIV))
                    ((Fask.Interfaces.Ukolovani.IUkolovani_DeleteByUkolID_CZ_UKOL_UZIV)providerUkol).DeleteByUkolID(rowUkol.ID);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_DeleteByUkolID_CZ_UKOL_UZIV");



                // odstraneni ukolu
                rowUkol.Delete();


                //var lta = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                ////lta.Delete(rowUkol.id);
                //lta.Update(this.ukolovaniDataset1.CZ_UKOL);

                if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Update_CZ_UKOL))
                    ((Fask.Interfaces.Ukolovani.IUkolovani_Update_CZ_UKOL)providerUkol).Update(this.dsUkoly.CZ_UKOL);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_DeleteByUkolID_CZ_UKOL_UZIV");




                this.dsUkoly.AcceptChanges();
                //UpdateForm();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion

        #region Eventy menu click
        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }
        
        #endregion

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgUkoly.CurrentCell.ColumnIndex + 1 >= dgUkoly.ColumnCount;
                bool endrow = dgUkoly.CurrentCell.RowIndex + 1 >= dgUkoly.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgUkoly.CurrentCell.ColumnIndex;
                    startRow = dgUkoly.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgUkoly.CurrentCell.ColumnIndex + 1;
                    startRow = dgUkoly.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgUkoly.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgUkoly.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgUkoly.CurrentCell = c;



        }
    }
}
