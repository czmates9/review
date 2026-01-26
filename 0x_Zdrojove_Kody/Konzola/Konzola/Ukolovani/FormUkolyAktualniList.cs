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
using System.Reflection;
namespace Konzola.Ukolovani
{
    public partial class FormUkolyAktualniList : Form
    {
        //private Fask.Interfaces.IVyrobaKonzola providerUziv = null;
        private Fask.Interfaces.IMES providerUkol = null;

        /// <summary>
        /// Zvolený ukol.
        /// </summary>
        private Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVRow rowUkolUziv
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgUkolovani.BindingContext[this.bsUkolovani].Current)).Row as Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private FASK.Logins.DataSets.Pristupy.FASK_LoginsRow rowUzivatel
        {
            get
            {
                try
                {
                    return comboBoxUzivatel.SelectedItem as FASK.Logins.DataSets.Pristupy.FASK_LoginsRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Seznam veškerých stavů včetně barev
        /// </summary>
        private Dictionary<string, Color> DictionaryStateColor = new Dictionary<string, Color>();

        private Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow rowUkol
        {
            get
            {
                try
                {
                    return comboBoxUkol.SelectedItem as Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATERow rowStav
        {
            get
            {
                try
                {
                    return comboBoxStav.SelectedItem as Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATERow;
                }
                catch
                {
                    return null;
                }
            }
        }





        #region Eventy formu

        public FormUkolyAktualniList()
        {
            InitializeComponent();

            this.dgUkolovani.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormUkolyAktualniList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgUkolovani.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgUkolovani.Columns);


                InitProvider();

                //if (providerUziv == null)
                //    throw new Exception("Provider 'Uzivatel' není inicializován");

                if (providerUkol == null)
                    throw new Exception("Provider 'Ukolovani' není inicializován");

                dateTimePickerDatumDo.Value = dateTimePickerDatumOd.Value = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;
            

                // nacteni comboboxu uzivatelu
                //var adapterUzivatel = new Production.DataServices.UkolovaniDatasetTableAdapters.CZMSTPWDTableAdapter();
                ////adapterUzivatel.Connection.ConnectionString = Globals.ConnectionString;
                //adapterUzivatel.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapterUzivatel.Fill(this.ukolovaniDataset1.CZMSTPWD);

                //Fask.Interfaces.DataSets.Uzivatele ds;

                //if ((providerUziv != null) && (providerUziv is Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele))
                //    ds = ((Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele)providerUziv).GetUzivatele();
                //else
                //    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Fill_CZ_UKOL");

                //this.ukolovaniDataset1.CZMSTPWD.Clear();

                //foreach (var item in ds.CZMSTPWD)
                //{
                //    this.ukolovaniDataset1.CZMSTPWD.AddCZMSTPWDRow(
                //        item.LOGIN,
                //        item.PASSWD,
                //        item.ID,
                //        item.ADM,
                //        item.FIRSTNAME,
                //        item.SECONDNAME,
                //        item.HASH,
                //        item.EAN,
                //        item.CODE);
                //}


                var dtUzivateleData = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();

                //comboBoxUzivatel.DataSource = this.vyrobaDataSet1.Logins;
                comboBoxUzivatel.Items.AddRange(dtUzivateleData.Select(null, "SECONDNAME asc, FIRSTNAME asc"));
                comboBoxUzivatel.SelectedItem = null;

                // nacteni comboboxu ukolu
                //var adapterUkol = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
                ////adapterUkol.Connection.ConnectionString = Globals.ConnectionString;
                //adapterUkol.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapterUkol.Fill(this.ukolovaniDataset1.CZ_UKOL);

                if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL))
                    ((Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL)providerUkol).Fill_CZ_UKOL(this.dsUkolovani);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Fill_CZ_UKOL");

                comboBoxUkol.Items.AddRange(this.dsUkolovani.CZ_UKOL.Select(null, "Description asc"));
                comboBoxUkol.SelectedItem = null;

                // naplnění priorit
                var test = this.dsUkolovani.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                comboBoxPriorita.DataSource = test;
                comboBoxPriorita.ValueMember = "Priority";
                comboBoxPriorita.SelectedItem = null;
                //comboBoxPriorita.Items.AddRange(test);
                //comboBoxPriorita.Items.AddRange(this.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList() );

                // naplneni comboboxu stav
                //var daState = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter();
                ////daState.Connection.ConnectionString = Globals.ConnectionString;
                //daState.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //daState.Fill(this.ukolovaniDataset1.CZ_UKOL_STATE);

                Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable dt;

                if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_GetCZ_UKOL_STATE))
                    dt = ((Fask.Interfaces.Ukolovani.IUkolovani_GetCZ_UKOL_STATE)providerUkol).GetCZ_UKOL_STATE();
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_GetCZ_UKOL_STATE");


                foreach (var item in dt)
                {
                    this.dsUkolovani.CZ_UKOL_STATE.ImportRow(item);
                }


                comboBoxStav.Items.AddRange(this.dsUkolovani.CZ_UKOL_STATE.Select(null, "Description asc"));
                comboBoxStav.SelectedItem = null;
                //var taState = daState.GetData();
                foreach (Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATERow item in this.dsUkolovani.CZ_UKOL_STATE)
                {
                    DictionaryStateColor.Add(item.State, item.IsColorNull() ? Color.White : ColorTranslator.FromHtml(item.Color));
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // TODO: This line of code loads data into the 'ukolovani_nekuptoDataSet.CZ_UKOL_UZIV_HIST' table. You can move, or remove it, as needed.
            //this.cZ_UKOL_UZIV_HISTTableAdapter.Fill(this.ukolovani_nekuptoDataSet.CZ_UKOL_UZIV_HIST);
        }

        private void FormUkolyAktualniList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormUkolyAktualniList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgUkolovani.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());
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

            //try
            //{
            //    if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
            //    {
            //        if (providerUziv == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //        {
            //            //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
            //            Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //            Type[] types = providerAssemlby.GetTypes();
            //            foreach (Type t in types)
            //            {
            //                try
            //                {
            //                    //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
            //                    if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
            //                    {
            //                        providerUziv = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
            //                        if (providerUziv != null)
            //                            break;
            //                    }
            //                }
            //                catch { }
            //            }
            //            //return config;
            //        }

            //        // nastaveni connection stringu
            //        // nastaveni connection stringu
            //        if ((providerUziv != null) && (providerUziv is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //            ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUkol).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.Ukol");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.dsUkolovani.CZ_UKOL_UZIV_HIST.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                System.Data.SqlClient.SqlDataAdapter da_filter = new System.Data.SqlClient.SqlDataAdapter();
                da_filter.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da_filter.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection();
                //da_filter.SelectCommand.Connection.ConnectionString = Globals.ConnectionString;
                da_filter.SelectCommand.Connection.ConnectionString = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString;

                //da_filter.SelectCommand.CommandText = "Select u.Name UkolName, u.Description UkolDescription, u.Priority UkolPriority, l.FIRSTNAME UserFirstname, l.SECONDNAME UserSecondname, l.LOGIN UserLogin ,uhist.* from CZ_UKOL_UZIV uhist ";
                da_filter.SelectCommand.CommandText = "Select u.Name UkolName, u.Description UkolDescription, u.Priority UkolPriority, u.DateCreated UkolDateCreated, u.DateFrom UkolDateFrom, u.DateTo UkolDateTo, l.FIRSTNAME UserFirstname, l.SECONDNAME UserSecondname, l.LOGIN UserLogin ,uhist.* from CZ_UKOL_UZIV uhist ";
                da_filter.SelectCommand.CommandText += "left join" + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = uhist.UserID ";
                da_filter.SelectCommand.CommandText += "left join CZ_UKOL u on u.ID = uhist.UkolID ";
                da_filter.SelectCommand.CommandText += "where ";
                da_filter.SelectCommand.CommandText += "1=1 ";

                // hledání uživatele
                if (comboBoxUzivatel.SelectedItem != null)
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.UserID=@name ";
                    //da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel.ID);
                }
                else
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.UserID IN (" +
                    " select distinct ID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where FIRSTNAME like '%' + @name + '%'" +
                    " union" +
                    " select distinct ID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where SECONDNAME like '%' + @name + '%'" +
                    " ) ";
                    //da_filter.SelectCommand.Parameters.AddWithValue("@name", comboBoxUzivatel.Text);
                }
                da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel != null ? rowUzivatel.USERID.ToString() : comboBoxUzivatel.Text);

                // hledání úkolu
                if (comboBoxUkol.SelectedItem != null)
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.UkolID=@ukol ";
                    //da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel.ID);
                }
                else
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.UkolID IN (" +
                    " select distinct ID from CZ_Ukol" +
                    " where Name like '%' + @ukol + '%'" +
                    " ) ";
                    //da_filter.SelectCommand.Parameters.AddWithValue("@name", comboBoxUzivatel.Text);
                }
                da_filter.SelectCommand.Parameters.AddWithValue("@ukol", rowUkol != null ? rowUkol.ID.ToString() : comboBoxUkol.Text);

                // hledání podle stavu
                if (comboBoxStav.SelectedItem != null)
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.State=@stav ";
                }
                else
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.State IN (" +
                    " select distinct State from CZ_UKOL_STATE" +
                    " where STATE like '%' + @Stav + '%'" +
                    " ) ";
                }
                da_filter.SelectCommand.Parameters.AddWithValue("@stav", rowStav != null ? rowStav.State : comboBoxStav.Text);

                // hledání podle priority
                if(!string.IsNullOrEmpty(comboBoxPriorita.Text.Trim()))
                {
                    da_filter.SelectCommand.CommandText += "AND u.Priority IN (" +
                        " select distinct Priority from CZ_Ukol" +
                        " where Priority=@priorita" +
                        " ) ";
                    da_filter.SelectCommand.Parameters.AddWithValue("@priorita", comboBoxPriorita.Text.Trim());
                }

                // hledání podle datumu
                if (dateTimePickerDatumOd.Checked && dateTimePickerDatumDo.Checked)
                {
                    da_filter.SelectCommand.CommandText += "AND uhist.DateChanged between @datumOd and @datumDo ";
                    da_filter.SelectCommand.Parameters.AddWithValue("@datumOd", dateTimePickerDatumOd.Value);
                    da_filter.SelectCommand.Parameters.AddWithValue("@datumDo", dateTimePickerDatumDo.Value);
                }
                else
                {
                    if (dateTimePickerDatumOd.Checked)
                    {
                        da_filter.SelectCommand.CommandText += "AND uhist.DateChanged > @datumOd ";
                        da_filter.SelectCommand.Parameters.AddWithValue("@datumOd", dateTimePickerDatumOd.Value);
                    }
                    else if (dateTimePickerDatumDo.Checked)
                    {
                        da_filter.SelectCommand.CommandText += "AND uhist.DateChanged < @datumDo ";
                        da_filter.SelectCommand.Parameters.AddWithValue("@datumDo", dateTimePickerDatumDo.Value);
                    }
                }

                da_filter.SelectCommand.CommandText += "order by ID desc";

                int FirstDisplayedScrollingRowIndex = this.dgUkolovani.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                this.dsUkolovani.CZ_UKOL_UZIV.Clear();
                this.dsUkolovani.CZ_UKOL_UZIV.AcceptChanges();

                this.dsUkolovani.CZ_UKOL_UZIV.BeginLoadData();

                da_filter.Fill(this.dsUkolovani.CZ_UKOL_UZIV);

                this.dsUkolovani.CZ_UKOL_UZIV.EndLoadData();
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgUkolovani.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgUkolovani.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
                ChangeStateTextColor();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void buttonKonec_Click(object sender, EventArgs e)
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

        private void ChangeStateTextColor()
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].PovolitBarevneZvyrazneniStavu)
                    return;

                // najiti indexu sloupce, ktery obsahuje aktualni stav
                int? index = null;
                for (int i = 0; i < dgUkolovani.ColumnCount; i++)
                {
                    if (dgUkolovani.Columns[i].DataPropertyName == "State")
                    {
                        if (dgUkolovani.Columns[i].Visible)
                        {
                            index = i;
                        }
                        break;
                    }
                }
                // data byla nalezena
                if (index.HasValue)
                {
                    foreach (DataGridViewRow item in dgUkolovani.Rows)
                    {
                        string value = item.Cells[index.Value].Value.ToString();
                        if (DictionaryStateColor.ContainsKey(value))
                        {
                            //item.Cells[index.Value].Style.ForeColor = DictionaryStateColor[value];
                            item.Cells[index.Value].Style.BackColor = DictionaryStateColor[value];
                        }
                        //Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATERow rowState = taState.FindByState(value);
                        //item.Cells[index.Value].Style.ForeColor = //rowState.IsColorNull() ? Color.Black : ColorTranslator.FromHtml(rowState.Color);
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                ChangeStateTextColor();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgUkolovani.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoCSVOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgUkolovani.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExcelVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgUkolovani.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExceOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dgUkolovani.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dgUkolovani.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dgUkolovani.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgUkolovani.CurrentCell.ColumnIndex + 1 >= dgUkolovani.ColumnCount;
                bool endrow = dgUkolovani.CurrentCell.RowIndex + 1 >= dgUkolovani.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgUkolovani.CurrentCell.ColumnIndex;
                    startRow = dgUkolovani.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgUkolovani.CurrentCell.ColumnIndex + 1;
                    startRow = dgUkolovani.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgUkolovani.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgUkolovani.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgUkolovani.CurrentCell = c;









        }

    }
}
