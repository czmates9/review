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

namespace Konzola.Vyroba.Ciselnik
{
    public partial class FormModifikace_TP : Form
    {


        #region Parametry
        protected Fask.Interfaces.IMES provider = null;
        protected Fask.Interfaces.IMES providerSklad = null;
        protected Fask.Interfaces.IMES providerP = null;
        

        protected Fask.Interfaces.Classes.ZOBRAZENI_TYP _zobrazeni = Fask.Interfaces.Classes.ZOBRAZENI_TYP.UNKNOWN;
        public Fask.Interfaces.Classes.ZOBRAZENI_TYP Zobrazeni
        {
            get
            {
                return _zobrazeni;
            }
            set
            {
                if (_zobrazeni != value)
                {
                    _zobrazeni = value;
                    switch (_zobrazeni)
                    {
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST:
                            panelButtonsZobrazeniVyber.Hide();
                            panelButtonsZobrazeniList.Show();
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            tsFiltry.Visible = true;
                            tsFiltry.Enabled = true;
                            break;
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER:
                            panelButtonsZobrazeniList.Hide();
                            panelButtonsZobrazeniVyber.Show();
                            menuStrip2.Items.Remove(tsmiMenuList);
                            menuStrip2.Items.Remove(tsmiExport);
                            menuStrip2.Items.Remove(tsmiModifikovat);
                            tsFiltry.Visible = false;
                            tsFiltry.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.VazbyModifikace_TP> filtry = new List<Fask.Interfaces.Filtry.VazbyModifikace_TP>();

        private Fask.Interfaces.Filtry.VazbyModifikace_TP rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.VazbyModifikace_TP;
                }
                catch
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Kolekce veškerých zvolených sloupců.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dg_Modifikace_TP.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Sklad
        private Fask.Interfaces.DataSets.Sklady dsSklady = new Fask.Interfaces.DataSets.Sklady();


        private Fask.Interfaces.DataSets.Sklady.CZMST093Row rowSklad
        {
            get
            {
                try
                {
                    return cb_Sklad.SelectedItem as Fask.Interfaces.DataSets.Sklady.CZMST093Row;
                }
                catch
                {
                    return null;
                }
            }
        }



        private string rowSKL_IDText()
        {
            return rowSklad == null ? (string.IsNullOrEmpty(cb_Sklad.Text) ? string.Empty : cb_Sklad.Text) : rowSklad.skl_id.ToString();
        }
        
        #endregion

        #region Vyrobek

        private Fask.Interfaces.DataSets.Vyroba dsVyroba = new Fask.Interfaces.DataSets.Vyroba();


        private Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow rowP
        {
            get
            {
                try
                {
                    return cb_Vyrobek.SelectedItem as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow;
                }
                catch
                {
                    return null;
                }
            }
        }



        private string rowPText()
        {
            return rowP == null ? (string.IsNullOrEmpty(cb_Vyrobek.Text) ? string.Empty : cb_Vyrobek.Text) : rowP.id.ToString();
        }

        #endregion


        #endregion

        #region c'tor + Load + eventy Formu

        public FormModifikace_TP()
        {
            InitializeComponent();
            this.dg_Modifikace_TP.UpdateColumnHeaderCellsByDatasource();
        }

        public FormModifikace_TP(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : this()
        {

            this.dg_Modifikace_TP.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void FormUzivateleList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_Modifikace_TP.LoadConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init(); 
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dg_Modifikace_TP.Columns);

                InitProvider();
                //PerformVyhledat();

                Fask.Interfaces.DataSets.Sklady dsSkladdata = null;

                if ((providerSklad != null) && (providerSklad is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady))
                {
                    dsSkladdata = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady)providerSklad).GetSklady();
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci ISklady2_GetSklady");
                }


                Fask.Interfaces.DataSets.Vyroba dsPdata = new Fask.Interfaces.DataSets.Vyroba();

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();

                //TODO 9.9.2019 dotahnout materialy

                //if (providerP is Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)
                //    dsPdata = ((Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)providerP).GetFiltrovanyProductionVazby(filtr);
                //else
                //    throw new Exception("IProduction_GetFiltrovanyProductionVazby not implementet");

                PopulateUI(dsSkladdata, dsPdata);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateUI(Fask.Interfaces.DataSets.Sklady dsSkladyData, Fask.Interfaces.DataSets.Vyroba dsPdata)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    PopulateUI(dsSkladyData, dsPdata);
                }));
                return;
            }

            ui_comboBoxSKLIDActualize(dsSkladyData);
            ui_comboBox_P_Actualize(dsPdata);

        }

        private void ui_comboBoxSKLIDActualize(Fask.Interfaces.DataSets.Sklady dsSkladyData)
        {
            // zajisteni zachovani vybranych hodnot

            var oldItems = cb_Sklad.Items;
            var oldSelectedItem = cb_Sklad.SelectedItem;
            var oldText = cb_Sklad.Text;

            cb_Sklad.Items.Clear();
            cb_Sklad.Items.AddRange(dsSkladyData.CZMST093.Select(null, "skl_desc asc"));

            if (oldItems.Count == 0)
                cb_Sklad.SelectedItem = null;
            if (!String.IsNullOrEmpty(oldText))
                cb_Sklad.Text = oldText;
            if (oldSelectedItem != null)
            {
                var osi = oldSelectedItem as Fask.Interfaces.DataSets.Sklady.CZMST093Row;
                if (osi != null)
                {
                    //var row = (Fask.Interfaces.DataSets.Sklady.CZMST093Row[])dsSkladyData.CZMST093.Select("skl_id=" + osi.skl_id);
                    var rows = dsSkladyData.CZMST093.Where(x => x.skl_id == osi.skl_id);
                    if (rows.Count() > 0)
                    {
                        cb_Sklad.SelectedItem = rows.First();
                    }
                }
            }

        }

        private void ui_comboBox_P_Actualize(Fask.Interfaces.DataSets.Vyroba dsP)
        {
            // zajisteni zachovani vybranych hodnot

            var oldItems = cb_Vyrobek.Items;
            var oldSelectedItem = cb_Vyrobek.SelectedItem;
            var oldText = cb_Vyrobek.Text;

            cb_Vyrobek.Items.Clear();
            cb_Vyrobek.Items.AddRange(dsP.Production_Konzola.Select(null, "id asc"));

            if (oldItems.Count == 0)
                cb_Vyrobek.SelectedItem = null;
            if (!String.IsNullOrEmpty(oldText))
                cb_Vyrobek.Text = oldText;
            if (oldSelectedItem != null)
            {
                var osi = oldSelectedItem as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow;
                if (osi != null)
                {
                    var rows = dsP.Production_Konzola.Where(x => x.id == osi.id);
                    if (rows.Count() > 0)
                    {
                        cb_Vyrobek.SelectedItem = rows.First();
                    }
                }
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
                    PerformVyhledat();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }


        private void FormZboziSelect_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_Modifikace_TP.Location.X + (this.dg_Modifikace_TP.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_Modifikace_TP.Location.Y + (this.dg_Modifikace_TP.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion

        #region Eventy

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_Modifikace_TP.SaveConfiguration(this.GetType().ToString());

                panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());

                WaithToEndThread();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
            catch
            {
            }
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        private void tsmi_ZamenZdrojCil_Click(object sender, EventArgs e)
        {

           DialogResult dr = MessageBox.Show(this, "Bude provedena záměna materiálů u všech výrobků!", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

           if (dr == System.Windows.Forms.DialogResult.Yes)
           {
               PerformZamenitZdrojCil();
           }
        }

        #region Filtre

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        private void tsbPridat_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
        }

        private void tsbOdebrat_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        #endregion
        #endregion

        #region Protected metody

        /// <summary>
        /// Inicializace providera
        /// </summary>
        public void InitProvider()
        {
            #region Vazby
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
                            if (typeof(Fask.Interfaces.Vazby.IVazby2).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.Vazby.IVazby2)providerAssemlby.CreateInstance(t.FullName);
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
            #endregion

            #region Sklady
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerSklad == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                            {
                                providerSklad = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSklad != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSklad.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
            #endregion

            #region productions
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Production.IProduction).IsAssignableFrom(t))
                            {
                                providerP = (Fask.Interfaces.Vyroba.Production.IProduction)providerAssemlby.CreateInstance(t.FullName);
                                if (providerP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerP.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
            #endregion
        }

        /// <summary>
        /// MEtoda pro Vybrat material
        /// </summary>
        public void PerformOK() { }

        /// <summary>
        /// Metoda na čekani dobehnuti vlakna
        /// </summary>
        public void WaithToEndThread()
        {
            //// cekani na dobehnuti vlakna
            //try
            //{
            //    if (bwLoadZbozi.IsBusy)
            //    {
            //        bwLoadZbozi.CancelAsync();
            //        while (bwLoadZbozi.IsBusy)
            //        {
            //            Application.DoEvents();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //}
        }


        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        public void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.ds_Modifikace_TP.Modifikace_TP.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Modifikace_TP.IsBusy)
                {
                    bw_Modifikace_TP.CancelAsync();
                    while (bw_Modifikace_TP.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.VazbyModifikace_TP filtr = new Fask.Interfaces.Filtry.VazbyModifikace_TP();
                if (!CreateFilter_Vyrobek(ref filtr))
                {
                    ProgressIndicatorStop();
                    return;
                }

                int FirstDisplayedScrollingRowIndex = this.dg_Modifikace_TP.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Modifikace_TP.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Modifikace_TP.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Modifikace_TP.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Metoda pro zameneni materialu z Zdroje do Cile
        /// </summary>
        private void PerformZamenitZdrojCil()
        {

            if (this.dg_Modifikace_TP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Neni vybrán žádný materál pro záměnu.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }


            try
            {


                if (bw_ZamenZdrojCil.IsBusy)
                {
                    bw_ZamenZdrojCil.CancelAsync();
                    while (bw_ZamenZdrojCil.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable dt = GetVybraneZeleneRadky();

                if (dt.Count < 1)
                {
                    MessageBox.Show("Neni vybrán žádný zelený materál pro záměnu.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ProgressIndicatorStop();
                    return;
                }

                int FirstDisplayedScrollingRowIndex = this.dg_Modifikace_TP.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_ZamenZdrojCil.RunWorkerAsync(dt);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Modifikace_TP.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Modifikace_TP.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }

        private Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable GetVybraneZeleneRadky()
        {
            Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable();


            foreach (DataGridViewRow row in dg_Modifikace_TP.SelectedRows)
            {
                Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow radek = (Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow)((DataRowView)row.DataBoundItem).Row;


                if (row.DefaultCellStyle.BackColor == Color.LightGreen)
                {
                    //Tohle by mnel byt korektní řadek pro zaměnu...
                    dt.ImportRow(radek);
                }
            }

            return dt;
        }


        private bool CreateFilter_Vyrobek(ref Fask.Interfaces.Filtry.VazbyModifikace_TP filtr)
        {

            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VazbyModifikace_TP();



            

            if(string.IsNullOrEmpty(rowSKL_IDText()))
            {
                MessageBox.Show(this, "ID Skladu je povinny udaj!" + Environment.NewLine + "Hledání nebude provedeno!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                return false;
            }
            else
            {
                filtr.SKL_ID = rowSKL_IDText();
            }

            //if (cb_Sklad.SelectedItem != null)
            //{
            //    filtr.SKL_ID = cb_Sklad.Text.Trim();
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(cb_Sklad.Text))
            //    {
            //        filtr.SKL_ID = cb_Sklad.Text.Trim();
            //    }
            //    else
            //    {
            //        MessageBox.Show(this, "ID Skladu je povinny udaj!" + Environment.NewLine + "Hledání nebude provedeno!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);

            //        return false;
            //    }
            //}

            filtr.ITEMNMBR = rowPText();

            //if (cb_Vyrobek.SelectedItem != null)
            //{
            //    filtr.ITEMNMBR = cb_Vyrobek.Text.Trim();
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(cb_Vyrobek.Text))
            //    {
            //        filtr.ITEMNMBR = cb_Vyrobek.Text.Trim();
            //    }
            //    else
            //    {
            //        //MessageBox.Show(this, "ID Skladu je povinny udaj!", "Warning");
            //    }
            //}

            filtr.Shodne = rb_Shodne.Checked;
            filtr.NEShodne = rb_NEShodne.Checked; 

            return true;

        }

        #region Filtry

        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        public void PerformOdebratFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr.NazevFiltru) ? string.Empty : rowFiltr.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry.Remove(rowFiltr);
                this.tscbFiltry.ComboBox.DataSource = null;
                this.tscbFiltry.ComboBox.DataSource = filtry;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        public void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.VazbyModifikace_TP filtr = new Fask.Interfaces.Filtry.VazbyModifikace_TP();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                bool result = CreateFilter_Vyrobek(ref filtr);
                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry.ComboBox.DataSource = null;
                    this.tscbFiltry.ComboBox.DataSource = filtry;
                    this.tscbFiltry.SelectedItem = filtr;
                    tscbFiltry.ComboBox.DropDownWitdhAutosize();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se uložit data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        public void PerformZmenitFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.VazbyModifikace_TP filtr = rowFiltr;

                if (!CreateFilter_Vyrobek(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyčistí filtr
        /// </summary>
        public void PerformVycistitFiltr()
        {
            try
            {
                cb_Sklad.SelectedItem =
                    cb_Vyrobek.SelectedItem = null;

                cb_Sklad.Text =
                    cb_Vyrobek.Text = string.Empty;


                rb_Shodne.Checked = false;
                rb_NEShodne.Checked = false; 

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nastavit Filter
        /// </summary>
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.VazbyModifikace_TP filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                if (!string.IsNullOrEmpty(filtr.SKL_ID))
                {
                    cb_Sklad.Text = filtr.SKL_ID.Trim();
                }

                if (!string.IsNullOrEmpty(filtr.ITEMNMBR))
                {
                    cb_Vyrobek.Text = filtr.ITEMNMBR.Trim();
                }

                rb_Shodne.Checked = filtr.Shodne;
                rb_NEShodne.Checked = filtr.Shodne; 

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Ostatni metody

        protected void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        protected void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_Modifikace_TP.Location.X + (this.dg_Modifikace_TP.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_Modifikace_TP.Location.Y + (this.dg_Modifikace_TP.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        #endregion

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_Modifikace_TP.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_Modifikace_TP.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_Modifikace_TP.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_Modifikace_TP.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_Modifikace_TP.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_Modifikace_TP.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region BackGround workery

        #region Vyhledat

        private void bw_Modifikace_TP_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.VazbyModifikace_TP filtr = (Fask.Interfaces.Filtry.VazbyModifikace_TP)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_Modifikace_TP.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vazby.IVazby2_Modifikace_TP_GetFiltrovaneData))
                {
                    ds = ((Fask.Interfaces.Vazby.IVazby2_Modifikace_TP_GetFiltrovaneData)provider).GetFiltrovaneData(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IVazby2_Modifikace_TP_GetFiltrovaneData");
                }

                if (bw_Modifikace_TP.CancellationPending)
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

        private void bw_Modifikace_TP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_Modifikace_TP = new Fask.Interfaces.DataSets.Vyroba();
                    bs_Modifikace_TP.DataSource = ds_Modifikace_TP;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_Modifikace_TP = new Fask.Interfaces.DataSets.Vyroba();
                    bs_Modifikace_TP.DataSource = ds_Modifikace_TP;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_Modifikace_TP = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (ds_Modifikace_TP == null)
                        ds_Modifikace_TP = new Fask.Interfaces.DataSets.Vyroba();

                    bs_Modifikace_TP.DataSource = ds_Modifikace_TP;
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

        #endregion

        #region Zamenit

        private void bw_ZamenZdrojCil_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable ds = (Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable)e.Argument;


                if (bw_ZamenZdrojCil.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.Classes.StatusInfo si = null;

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vazby.IVazby2_Modifikace_TP_SetZamenZdrojCil))
                {
                    si = ((Fask.Interfaces.Vazby.IVazby2_Modifikace_TP_SetZamenZdrojCil)provider).SetZamenZdrojCil(ds);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IVazby2_Modifikace_TP_SetZamenZdrojCil");
                }

                if (bw_ZamenZdrojCil.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = si;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bw_ZamenZdrojCil_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();
                try
                {

                    si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                }
                catch
                { }

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    //handle the cancelled
                }
                else
                {
                    // uspesne dokonceno ...

                    if (si.ID < 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    PerformVyhledat();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                if (ex.InnerException != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex.InnerException);
                    MessageBox.Show(ex.Message + ex.InnerException.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                ProgressIndicatorStop();
            }

        }

        #endregion

        #endregion

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_Modifikace_TP.CurrentCell.ColumnIndex + 1 >= dg_Modifikace_TP.ColumnCount;
                bool endrow = dg_Modifikace_TP.CurrentCell.RowIndex + 1 >= dg_Modifikace_TP.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_Modifikace_TP.CurrentCell.ColumnIndex;
                    startRow = dg_Modifikace_TP.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_Modifikace_TP.CurrentCell.ColumnIndex + 1;
                    startRow = dg_Modifikace_TP.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_Modifikace_TP.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_Modifikace_TP.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_Modifikace_TP.CurrentCell = c;
        }

        private void dg_Modifikace_TP_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            #region pokus 1
            //var grupa = ds_Modifikace_TP.Modifikace_TP.GroupBy(x => new
            //{
            //    x.ITEMNMBR_Def,
            //    x.DESC_Def,
            //    x.ITEMNMBR_fol,
            //    x.ITEMNMBR,
            //    x.DESC_Fol,
            //    x.ITEMDESC,
            //    x.ITEMCODE_Fol,
            //    x.ITEMCODE,
            //    x.VNDITNUM_Fol,
            //    x.VNDITNUM,
            //    x.MJ_Fol,
            //    x.MJ,
            //    x.SKL_ID_Fol,
            //    x.SKL_ID
            //});


            //foreach (var item in grupa)
            //{
            //    if (item.Count() > 1)
            //    {

            //        foreach (var item2 in item)
            //        {
            //            foreach (DataGridViewRow row in dg_Modifikace_TP.Rows)
            //            {
            //                Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow radek = (Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow)((DataRowView)row.DataBoundItem).Row;

            //                if (item2 == radek)
            //                {
            //                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
            //                }
            //            }
            //        }
            //    }
            //} 
            #endregion

            #region pokus 2

            //var duplicates = ds_Modifikace_TP.Modifikace_TP.AsEnumerable().GroupBy(r => new {r.DESC_Fol ,r.ITEMDESC}).Where(gr => gr.Count() > 1);

            //foreach (var item in duplicates)
            //{
            //    if (item.Count() > 1)
            //    {

            //        foreach (var item2 in item)
            //        {
            //            foreach (DataGridViewRow row in dg_Modifikace_TP.Rows)
            //            {
            //                Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow radek = (Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow)((DataRowView)row.DataBoundItem).Row;

            //                if (item2 == radek)
            //                {
            //                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            #region Pokus 3

            foreach (DataGridViewRow row in dg_Modifikace_TP.Rows)
            {
                Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow radek = (Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow)((DataRowView)row.DataBoundItem).Row;

                if (radek.PocetVyskytu > 1)
                {
                    //Varinata kdy je duplicita materialu
                    row.DefaultCellStyle.BackColor = Color.Crimson; // Color.FromArgb(255, 100, 123); // red
                }
                else if  ( (radek.IsSKL_IDNull()) || (string.IsNullOrEmpty(radek.SKL_ID)))
                {
                    row.DefaultCellStyle.BackColor = Color.SandyBrown; // Color.FromArgb(244, 164, 96); // SandyBrown
                }
                else if (radek.SKL_ID.Trim() == radek.SKL_ID_Fol.Trim())
                {
                    //varianta kdy je sklad zdroj totožny sklad cil
                    row.DefaultCellStyle.BackColor = Color.Yellow; //Color.FromArgb(255, 255, 0); // Yellow
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; //Color.FromArgb(144, 238, 144); // LightGreen
                    //row.DefaultCellStyle.BackColor = SystemColors.Control;
                }
            }


            #endregion
        }

        private void rb_Shodne_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Shodne.Checked)
                rb_NEShodne.Checked = false;
        }

        private void rb_NEShodne_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_NEShodne.Checked)
                rb_Shodne.Checked = false;

        }

    }
}
