using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Vyroba_Konzola.Extensions;
using Vyroba_Konzola;
using System.Reflection;

namespace Vyroba_Konzola.Ciselniky
{
    /// <summary>
    /// TODO: Odstranit, ale poresit pridavani ukolu uzivatelum
    /// </summary>
    public partial class FormUzivateleSelect : Form
    {

        
            

            private Fask.Console.Interfaces.IVyrobaKonzola providerUzivatele = null;
            private Fask.Console.Interfaces.IVyrobaKonzola providerUkol = null;



        /// <summary>
        /// Vybraný úkol
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Ukolovani.CZMSTPWDRow rowUzivatel
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bindingSource1].Current)).Row as Fask.Console.Interfaces.DataSets.Ukolovani.CZMSTPWDRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public DataGridViewSelectedRowCollection rowsSelected
        {
            get
            {
                try
                {
                    return dataGridView1.SelectedRows;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybraný úkol
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Ukolovani.CZ_UKOLRow rowUkol
        {
            get;
            set;
        }
        

        public FormUzivateleSelect()
        {
            InitializeComponent();
            UpdateForm();
        }

        private void FormUzivateleList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());

                InitProvider();

                if (providerUkol == null)
                    throw new Exception("Provider 'Ukolovani' není inicializován");

                if (providerUzivatele == null)
                    throw new Exception("Provider 'Uzivatel' není inicializován");

            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if (providerUkol == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Console.Interfaces.Ukolovani.IUkolovani).IsAssignableFrom(t))
                                {
                                    providerUkol = (Fask.Console.Interfaces.Ukolovani.IUkolovani)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerUkol != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    // nastaveni connection stringu
                    if ((providerUkol != null) && (providerUkol is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerUkol).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Ukol");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Console.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
                                {
                                    providerUzivatele = (Fask.Console.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerUzivatele != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    // nastaveni connection stringu
                    if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerUkol).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Uziv");
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
                int rowID = 0;
                if (rowUzivatel != null)
                    rowID = rowUzivatel.ID;

                //var lta = new Production.DataServices.UkolovaniDatasetTableAdapters.CZMSTPWDTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //lta.Fill(this.ukolovaniDataset1.CZMSTPWD);

                Fask.Console.Interfaces.DataSets.Uzivatele uz;

                if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatele))
                    uz = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatele)providerUzivatele).GetUzivatele();
                else
                    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatele.");


                this.ukolovaniDataset1.CZMSTPWD.Clear();

                foreach (var item in uz.CZMSTPWD)
                {
                    this.ukolovaniDataset1.CZMSTPWD.ImportRow(item);
                }


                int pos = this.bindingSource1.Find("ID", rowID);
                this.bindingSource1.Position = pos;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleList_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();            
        }

        private void PerformOK()
        {
            //System.Data.SqlClient.SqlTransaction trx = null;
            //var taUkolUziv = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();

            try
            {
                if (rowUzivatel == null)
                {
                    MessageBox.Show("Není vybrán uživatel pro přiřazení úkolu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                
                //taUkolUziv.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //taUkolUziv.Connection.Open();
                // transakce
                //trx = taUkolUziv.Connection.BeginTransaction(IsolationLevel.Serializable);
                //taUkolUziv.MyTransaction = trx;

                foreach (DataGridViewRow dgview in rowsSelected)
                {
                    Fask.Console.Interfaces.DataSets.Ukolovani.CZMSTPWDRow item = ((DataRowView)dgview.DataBoundItem).Row as Fask.Console.Interfaces.DataSets.Ukolovani.CZMSTPWDRow;
                    Fask.Console.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable dtUkolUziv;
                    //var dtUkolUziv = taUkolUziv.GetDataByUkolIDAndUserID(rowUkol.ID, item.ID);

                    if ((providerUzivatele != null) && (providerUzivatele is Fask.Console.Interfaces.Ukolovani.IUkolovani_GetDataByUkolIDAndUserID_CZ_UKOL_UZIV))
                        dtUkolUziv = ((Fask.Console.Interfaces.Ukolovani.IUkolovani_GetDataByUkolIDAndUserID_CZ_UKOL_UZIV)providerUzivatele).GetDataByUkolIDAndUserID(rowUkol.ID, item.ID);
                    else
                        throw new NotImplementedException("Provider neimplementuje IUkolovani_GetDataByUkolIDAndUserID_CZ_UKOL_UZIV.");



                    if (dtUkolUziv.Count > 0)
                    {
                        MessageBox.Show("Uživatel '" + item.ToString() + "' již má přiřazen daný úkol", this.Text, MessageBoxButtons.OK);
                        //if (trx != null)
                        //    trx.Rollback();
                        return;
                    }
                    else
                    {

                        if ((providerUkol != null) && (providerUkol is Fask.Console.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL_UZIV))
                        {
                            ((Fask.Console.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL_UZIV)providerUkol).Insert( rowUkol.ID, item.ID, rowUkol.State, null, null, null, null, null);
                    }
                        else
                            throw new NotImplementedException("Provider neimplementuje IUkolovani_Insert_CZ_UKOL_UZIV.");

                        

                        //taUkolUziv.InsertQuery(
                        //rowUkol.ID,
                        //item.ID,
                        //rowUkol.State,
                        //null,
                        //null,
                        //null,
                        //null,
                        //null);
                    }
                    
                }


                //if (trx != null)
                //    trx.Commit();

                this.DialogResult = DialogResult.OK;

                // kontrola, zdali již nebyl přiřazen
                //var taUkolUziv = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
                //taUkolUziv.Connection.ConnectionString = Properties.Settings.Default.Ukolovani_ConnectionString;
                //var dtUkolUziv = taUkolUziv.GetDataByUkolIDAndUserID(rowUkol.ID,rowUzivatel.ID);
                //if (dtUkolUziv.Count > 0)
                //{
                //    MessageBox.Show("Zvolený uživatel již má přiřazen daný úkol", this.Text, MessageBoxButtons.OK);
                //    return;
                //}

                //this.DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                //try
                //{
                //    if (trx != null)
                //        trx.Rollback();
                //}
                //catch (Exception ex2)
                //{
                //    Log.Write(ex2);
                //}
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //if ((taUkolUziv.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //    taUkolUziv.Connection.Close();
            }
            
        }

        private void FormUzivateleList_Resize(object sender, EventArgs e)
        {

        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dataGridView1.SaveConfiguration(this.GetType().ToString());
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
            }
            catch (Exception)
            {
            }
        }
    }
}
