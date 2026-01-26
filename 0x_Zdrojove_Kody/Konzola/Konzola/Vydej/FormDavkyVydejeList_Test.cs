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
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

//using Fask.AdvancedButtonsPanel.Extension;

namespace Konzola.Vydej
{
    public partial class FormDavkyVydejeList_Test : Form
    {
        private Fask.Interfaces.IMES providerVydej = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;

        private List<Fask.Interfaces.Filtry.VydejDavkyFiltr> filtry = new List<Fask.Interfaces.Filtry.VydejDavkyFiltr>();

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Vydej.CZMST_SERow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bsVydej].Current)).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SERow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.VydejDavkyFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.VydejDavkyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Menu

        //ContextMenu mnu = new ContextMenu();
        //MenuItem mnuEdit = new MenuItem("Dávka->Upravit");
        //MenuItem mnuDelete = new MenuItem("Dávka->Smazat");
        //MenuItem mnuGenerate = new MenuItem("Dávka->Generovat");

        //MenuItem mnuMerge = new MenuItem("Dávka->Sloučit");
        //MenuItem mnuRelease = new MenuItem("Dávka->Uvolnit");
        //MenuItem mnuBatchControl = new MenuItem("Dávka->Kontrola dávky");

        //MenuItem mnuPrint = new MenuItem("Výstup->Report tisk vše");
        //MenuItem mnuExportExcelAll = new MenuItem("Výstup->Excel exportovat vše");
        //MenuItem mnuExportExcelSelect = new MenuItem("Výstup->Excel exportovat označené");

        
        //MenuItem mnuEnd = new MenuItem("Menu->Konec");




        #endregion
        
        #region Init Buttons

        //Button btn_Davka_Edit = new Button();
        //Button btn_Davka_Delete = new Button();
        //Button btn_Davka_Generate = new Button();

        //Button btn_Davka_Merge = new Button();
        //Button btn_Davka_Release = new Button();

        //Button btn_Vystup_ReportTiskVse = new Button();
        //Button btn_Vystup_ExcelExportovatVse = new Button();
        //Button btn_Vystup_ExcelExportovatOznacene = new Button();
        //Button btn_Menu_KontrolaDavky = new Button();
        //Button btn_Menu_Konec = new Button();
        

        //private void InitButtonsShow(string fileName)
        //{


        //    if (Settings.DataGridVazatNaUzivatele && Globals.Pracovnik != null)
        //        fileName = Globals.Pracovnik.id.Trim() + fileName;

        //    fileName = Path.Combine(MySystem.MyPath.ConfigButtonDirectory, fileName + ".xml");

        //    if (!File.Exists(fileName))
        //        return;

        //    IDictionary<string, bool> Buttons;

        //    using (var streamWriter = new StreamReader(fileName))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });

        //        Buttons = ((item[])serializer.Deserialize(streamWriter)).ToDictionary(i => i.id, i => i.value);

        //    }

        //    //using (var streamReader = new StreamReader(fileName))
        //    //{
        //    //    var xmlSerializer = new XmlSerializer(typeof(IDictionary<string, bool>));
        //    //    Buttons = (IDictionary<string, bool>)xmlSerializer.Deserialize(streamReader);
        //    //}


        //    foreach (var item in Buttons)
        //    {

        //        switch (item.Key)
        //        {
        //            case "mnuEdit":
        //                mnuEdit.Checked = item.Value;
        //                break;
        //            case "mnuDelete":
        //                mnuDelete.Checked = item.Value;
        //                break;
        //            case "mnuGenerate":
        //                mnuGenerate.Checked = item.Value;
        //                break;
        //            case "mnuMerge":
        //                mnuMerge.Checked = item.Value;
        //                break;
        //            case "mnuRelease":
        //                mnuRelease.Checked = item.Value;
        //                break;
        //            case "mnuPrint":
        //                mnuPrint.Checked = item.Value;
        //                break;
        //            case "mnuExportExcelAll":
        //                mnuExportExcelAll.Checked = item.Value;
        //                break;
        //            case "mnuExportExcelSelect":
        //                mnuExportExcelSelect.Checked = item.Value;
        //                break;
        //            case "mnuBatchControl":
        //                mnuBatchControl.Checked = item.Value;
        //                break;
        //            case "mnuEnd":
        //                mnuEnd.Checked = item.Value;
        //                break;
        //            default:
        //                break;
        //        }
                
        //    }

        //    UpdateButtons();

        //}


        //private void SaveButtonsShow(string fileName)
        //{
        //    if (Settings.DataGridVazatNaUzivatele && Globals.Pracovnik != null)
        //        fileName = Globals.Pracovnik.id.Trim() + fileName;

        //    fileName = Path.Combine(MySystem.MyPath.ConfigButtonDirectory, fileName + ".xml");

        //    IDictionary<string, bool> Buttons = new Dictionary<string, bool>();


        //    Buttons.Add("mnuEdit", mnuEdit.Checked);
        //    Buttons.Add("mnuDelete", mnuDelete.Checked);
        //    Buttons.Add("mnuGenerate", mnuGenerate.Checked);

        //    Buttons.Add("mnuMerge", mnuMerge.Checked);
        //    Buttons.Add("mnuRelease", mnuRelease.Checked);

        //    Buttons.Add("mnuPrint", mnuPrint.Checked);
        //    Buttons.Add("mnuExportExcelAll", mnuExportExcelAll.Checked);
        //    Buttons.Add("mnuExportExcelSelect", mnuExportExcelSelect.Checked);

        //    Buttons.Add("mnuBatchControl", mnuBatchControl.Checked);
        //    Buttons.Add("mnuEnd", mnuEnd.Checked);


            

        //    using (var streamWriter = new StreamWriter(fileName))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });
        //        serializer.Serialize(streamWriter,
        //      Buttons.Select(kv => new item() { id = kv.Key, value = kv.Value }).ToArray());
        //    }


        //    //using (var streamWriter = new StreamWriter(fileName))
        //    //{
        //    //    var xmlSerializer = new XmlSerializer(typeof(IDictionary<string, bool>));
        //    //    xmlSerializer.Serialize(streamWriter, Buttons);
        //    //}
        //}


        //private void InitButtons()
        //{

        //    btn_Davka_Edit.Text = "Upravit dávku";
        //    btn_Davka_Edit.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Davka_Edit.Dock = DockStyle.Top;
        //    //btn_Davka_Edit.Location = new Point(5, 5);
        //    btn_Davka_Edit.Click += new EventHandler(upravitToolStripMenuItem_Click);


        //    btn_Davka_Delete.Text = "Smazat dávku";
        //    btn_Davka_Delete.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Davka_Delete.Dock = DockStyle.Top;
        //    btn_Davka_Delete.Click += new EventHandler(smazatToolStripMenuItem_Click);


        //    btn_Davka_Generate.Text = "Generovat dávku";
        //    btn_Davka_Generate.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Davka_Generate.Dock = DockStyle.Top;
        //    btn_Davka_Generate.Click += new EventHandler(generovatToolStripMenuItem_Click);


        //    btn_Davka_Merge.Text = "Sloučit dávku";
        //    btn_Davka_Merge.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Davka_Merge.Dock = DockStyle.Top;
        //    btn_Davka_Merge.Click += new EventHandler(sloučitToolStripMenuItem_Click);

        //    btn_Davka_Release.Text = "Uvolnit dávku";
        //    btn_Davka_Release.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Davka_Release.Dock = DockStyle.Top;
        //    btn_Davka_Release.Click += new EventHandler(uvolnitToolStripMenuItem_Click);


        //    btn_Vystup_ReportTiskVse.Text = "Report tisk vše";
        //    btn_Vystup_ReportTiskVse.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Vystup_ReportTiskVse.Dock = DockStyle.Top;
        //    btn_Vystup_ReportTiskVse.Click += new EventHandler(Tisk_tsmi_Click);

        //    btn_Vystup_ExcelExportovatVse.Text = "Excel exportovat vše";
        //    btn_Vystup_ExcelExportovatVse.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Vystup_ExcelExportovatVse.Dock = DockStyle.Top;
        //    btn_Vystup_ExcelExportovatVse.Click += new EventHandler(ExcelExportovatVseToolStripMenuItem_Click);

        //    btn_Vystup_ExcelExportovatOznacene.Text = "Excel exportovat označené";
        //    btn_Vystup_ExcelExportovatOznacene.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Vystup_ExcelExportovatOznacene.Dock = DockStyle.Top;
        //    btn_Vystup_ExcelExportovatOznacene.Click += new EventHandler(ExcelExportovatOznaceneToolStripMenuItem_Click);

        //    btn_Menu_KontrolaDavky.Text = "Skontrolovat dávku";
        //    btn_Menu_KontrolaDavky.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Menu_KontrolaDavky.Dock = DockStyle.Top;
        //    btn_Menu_KontrolaDavky.Click += new EventHandler(Kontrola_tsmi_Click);

        //    btn_Menu_Konec.Text = "Konec";
        //    btn_Menu_Konec.Size = new System.Drawing.Size(panel2.Width, panel2.Width);
        //    btn_Menu_Konec.Dock = DockStyle.Top;
        //    btn_Menu_Konec.Click += new EventHandler(konecToolStripMenuItem_Click);

        //}



        #endregion

        public FormDavkyVydejeList_Test()
        {
            InitializeComponent();
            buttonsPanel1.Menu = menuStrip1;
        }


        private void ExcelExportAll()
        {

            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportAllRowsVisibleColumnsToExcel(string.Empty);
                this.dataGridView1.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExcelExportSelect()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;
                //dataGridView1.ExportSelectedRowsVisibleColumnsToExcel(string.Empty);
                this.dataGridView1.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPohybyList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;


                // načtení konfigurace datagridu z nastavení aplikace
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());
                //InitButtonsShow(this.GetType().ToString());

                buttonsPanel1.LoadConfiguration(this.GetType().ToString());
                buttonsPanel1.Init();

                //buttonsPanel1.Menu = menuStrip1;

                // TODO načteni konfigurace BTNS cez extension metodu...

                //buttonsPanel1.INIT();
                //Fask.AdvancedButtonsPanel.ButtonsPanel btn = new Fask.AdvancedButtonsPanel.ButtonsPanel();
                //btn.LoadConfiguration();
               

                // LoadConfiguration(this.GetType().ToString());

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VydejDavkyFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerVydej == null)
                    throw new Exception("Provider 'Vydej' není inicializován");

                //if (providerUzivatele == null)
                //    throw new Exception("Provider 'Uživatelé' není inicializován");

                advancedDataGridViewSearchToolBar1.SetColumns(dataGridView1.Columns);

                #region


                //InitButtons();


                //mnuEdit.Click += new EventHandler(mnuEdit_Click);
                //mnuDelete.Click += new EventHandler(mnuDelete_Click);
                //mnuGenerate.Click += new EventHandler(mnuGenerate_Click);

                //mnuMerge.Click += new EventHandler(mnuMerge_Click);
                //mnuRelease.Click += new EventHandler(mnuRelease_Click);

                //mnuPrint.Click += new EventHandler(mnuPrint_Click);
                //mnuExportExcelAll.Click += new EventHandler(mnuExportExcelAll_Click);
                //mnuExportExcelSelect.Click += new EventHandler(mnuExportExcelSelect_Click);

                //mnuBatchControl.Click += new EventHandler(mnuBatchControl_Click);
                //mnuEnd.Click += new EventHandler(mnuEnd_Click);

                ////mnu.MenuItems.AddRange(new MenuItem[] { mnuEdit, mnuDelete, mnuGenerate, mnuMerge, mnuRelease , });

                //mnu.MenuItems.Add(mnuEdit);
                //mnu.MenuItems.Add(mnuDelete);
                //mnu.MenuItems.Add(mnuGenerate);
                //mnu.MenuItems.Add(mnuMerge);
                //mnu.MenuItems.Add(mnuRelease);

                //mnu.MenuItems.Add("-");

                //mnu.MenuItems.Add(mnuPrint);
                //mnu.MenuItems.Add(mnuExportExcelAll);
                //mnu.MenuItems.Add(mnuExportExcelSelect);

                //mnu.MenuItems.Add("-");

                //mnu.MenuItems.Add(mnuBatchControl);

                //mnu.MenuItems.Add("-");

                //mnu.MenuItems.Add(mnuEnd);

                //panel2.ContextMenu = mnu;

                #endregion

                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshPanelButton()
        {


        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVydej == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vydej.IVydej2).IsAssignableFrom(t))
                            {
                                providerVydej = (Fask.Interfaces.Vydej.IVydej2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVydej != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVydej.InitProvider();

                //if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                //{
                //    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                //    Type[] types = providerAssemlby.GetTypes();
                //    foreach (Type t in types)
                //    {
                //        try
                //        {
                //            if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
                //            {
                //                providerUzivatele = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                //                if (providerUzivatele != null)
                //                    break;
                //            }
                //        }
                //        catch { }
                //    }
                //    //return config;
                //}

                //// nastaveni connection stringu
                ////if (providerUzivatele != null)
                ////    ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
                //    ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();

            filtr.CountEntries = cbCountEntries.Text.Trim();
            filtr.Sopnumbe = cbSopnumbe.Text.Trim();
            filtr.ITEMNMBR = cbITEMNMBR.Text.Trim();

            filtr.UvolneneDavky = chB_Uvolnene.Checked;
            filtr.NEUvolneneDavky = chB_NEUvolnene.Checked;
            filtr.SpracovaneDavky = chB_Spracovane.Checked;
            filtr.StazeneDavky = chB_Stazene.Checked;
            filtr.MrtveDavky = chB_Mrtve.Checked;

            return true;
        }

        int? Index = null;

        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.dsVydej.Hlavicky.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadVydejHlavicky.IsBusy)
                {
                    bwLoadVydejHlavicky.CancelAsync();
                    while (bwLoadVydejHlavicky.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                if (SelectedRow != null)
                    Index = SelectedRow.DEX_ROW_ID;


                bwLoadVydejHlavicky.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    this.dataGridView1.Focus();
            //}
        }

        private void FormPohybyList_KeyDown(object sender, KeyEventArgs e)
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
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPohybyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dataGridView1.SaveConfiguration(this.GetType().ToString());
                //SaveButtonsShow(this.GetType().ToString());
                buttonsPanel1.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void FormSkladPohybList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dataGridView1.Location.X + (this.dataGridView1.Width / 2) - (progressIndicator1.Size.Width / 2), this.dataGridView1.Location.Y + (this.dataGridView1.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            try
            {
                // 11.8.2016 PeV: zakomentovano automaticke vyhledavani pri otevreni formulare
                //PerformOK();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bwSkladPohyb_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //bwLoadSkladPohyb.CancelAsync();
                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = (Fask.Interfaces.Filtry.VydejDavkyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vydej ds = new Fask.Interfaces.DataSets.Vydej();

                if (bwLoadVydejHlavicky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky))
                    ds = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky)providerVydej).GetFiltrovaneDavky(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetFiltrovaneDavky.");



                if (bwLoadVydejHlavicky.CancellationPending)
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

        private void bwSkladPohyb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsVydej = new Fask.Interfaces.DataSets.Vydej();
                    bsVydej.DataSource = dsVydej;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsVydej = new Fask.Interfaces.DataSets.Vydej();
                    bsVydej.DataSource = dsVydej;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                    if (dsVydej == null)
                        dsVydej = new Fask.Interfaces.DataSets.Vydej();

                    bsVydej.DataSource = dsVydej;



                    if (Index != null)
                    {
                        var polozky = dsVydej.CZMST_SE.Where(x => x.DEX_ROW_ID == Index);
                        if (polozky.Count() > 0)
                        {
                            var polozka = polozky.First();

                            int index = ((System.Data.DataView)bsVydej.List).Table.Rows.IndexOf(polozka);
                            bsVydej.Position = index;
                        }
                        Index = null;
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
                this.progressIndicator1.Location = new Point(this.dataGridView1.Location.X + (this.dataGridView1.Width / 2) - (progressIndicator1.Size.Width / 2), this.dataGridView1.Location.Y + (this.dataGridView1.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        private void PerformZmenitFiltr()
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

                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbOdebrat_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
        }

        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        private void PerformOdebratFiltr()
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

        private void tsbPridat_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                bool result = CreateFilter(ref filtr);
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

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // countentries
                cbCountEntries.Text = filtr.CountEntries.ToString();

                // sopnumbe
                cbSopnumbe.Text = filtr.Sopnumbe;
                cbITEMNMBR.Text = filtr.ITEMNMBR;
                chB_Uvolnene.Checked = filtr.UvolneneDavky;
                chB_NEUvolnene.Checked = filtr.NEUvolneneDavky;

                chB_Spracovane.Checked = filtr.SpracovaneDavky;
                chB_Stazene.Checked = filtr.StazeneDavky;
                chB_Mrtve.Checked = filtr.MrtveDavky;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (SelectedRow != null)
                        sortedID = SelectedRow.CountEntries.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsVydej.Find(dsVydej.Hlavicky.CountEntriesColumn.ColumnName, sortedID);
                this.bsVydej.Position = pos;
            }
            catch { }
        }

        private enum Vyber
        {
            Storno,
            Odstranit_prirazeni,
            Zmenit_uzivatele
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                cbCountEntries.Text = string.Empty;
                cbSopnumbe.Text = string.Empty;
                cbITEMNMBR.Text = string.Empty;
                chB_NEUvolnene.Checked = true;
                chB_Uvolnene.Checked = true;
                chB_Spracovane.Checked = true;
                chB_Stazene.Checked = true;
                chB_Mrtve.Checked = true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generovat data




        private void GenerateDavka(int? CountEntries, string SKL_ID)
        {
            try
            {

                if (bwExport.IsBusy)
                {
                    bwExport.CancelAsync();
                    while (bwExport.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                // Okno s zadanim čisla....


                DataDavkyGenerate data = new DataDavkyGenerate();
                string DescSklad = string.Empty;

                Fask.Interfaces.Classes.Sklad sklad = new Fask.Interfaces.Classes.Sklad();
                Fask.Interfaces.DataSets.Sklady.CZMST093Row RowSklad;

                if (CountEntries != null)
                {
                    sklad.ID = SKL_ID.Trim();

                    if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                        RowSklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerVydej).GetSkladByID(SKL_ID.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                    DescSklad = RowSklad.skl_desc.Trim();


                }
                else
                {

                    using (Ciselniky.FormSkladyList frmSklad = new Ciselniky.FormSkladyList(false, null, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                    {
                        frmSklad.Text = "Výběr Sklad";

                        if (frmSklad.ShowDialog(this) != DialogResult.OK)
                        {
                            ProgressIndicatorStop();
                            return;
                        }

                        sklad.ID = frmSklad.CZMST093_selectedRow.skl_id.Trim();
                        DescSklad = frmSklad.CZMST093_selectedRow.skl_desc.Trim();
                    }
                }

                data.Davka = CountEntries;

                string TextOut = CountEntries == null ? string.Format("Zadejte číslo objednávky." + Environment.NewLine + "Pro sklad: '{0}'.", DescSklad) : string.Format("Zadejte číslo objednávky." + Environment.NewLine + "Pro sklad: '{0}'." + Environment.NewLine + "Sloučení do dávky: '{1}'", DescSklad, CountEntries);

                string sopnumber = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Číslo objednávky", TextOut, string.Empty, false, out sopnumber);
                if (dr != System.Windows.Forms.DialogResult.OK)
                {
                    ProgressIndicatorStop();
                    return;
                }

                Fask.Interfaces.Classes.Objednavka objednavka = new Fask.Interfaces.Classes.Objednavka();

                objednavka.ID = sopnumber;
                objednavka.CisloDavky = string.Empty;

                data.sklad = sklad;
                data.objednavka = objednavka;

                bwExport.RunWorkerAsync(data);

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                Fask.Interfaces.Classes.StatusInfo si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error

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


                    chB_Mrtve.Checked = false;
                    chB_NEUvolnene.Checked = true;
                    chB_Spracovane.Checked = false;
                    chB_Stazene.Checked = false;
                    chB_Uvolnene.Checked = false;


                    PerformOK();
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

        private void bwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataDavkyGenerate data = (DataDavkyGenerate)e.Argument;

                if (bwExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);

                Fask.Interfaces.Classes.StatusInfo si;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GenerateDavka))
                    si = ((Fask.Interfaces.Vydej.IVydej2_GenerateDavka)providerVydej).Vydej_GenerateDavka(data.objednavka, data.sklad, data.Davka);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GenerateDavka.");



                if (bwLoadVydejHlavicky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = si;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormDavkyVydejeEdit frmuziv = new FormDavkyVydejeEdit())
                {
                    frmuziv.rowDavkaEdit = SelectedRow;
                    frmuziv.Text = "Úprava Davky";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //this.SelectedRow.CountEntries = frmuziv.returnrow.CountEntries;
                    //this.SelectedRow.SOPNUMBE = frmuziv.returnrow.SOPNUMBE;
                    //this.SelectedRow.ITEMNMBR = frmuziv.returnrow.ITEMNMBR;
                    //this.SelectedRow.ITEMTYPE = frmuziv.returnrow.ITEMTYPE;
                    //this.SelectedRow.ITEMDESC = frmuziv.returnrow.ITEMDESC;
                    //this.SelectedRow.VNDDOCNM = frmuziv.returnrow.VNDDOCNM;
                    //this.SelectedRow.VNDITNUM = frmuziv.returnrow.VNDITNUM;
                    //this.SelectedRow.ORD = frmuziv.returnrow.ORD;
                    //this.SelectedRow.CZ_CarKod = frmuziv.returnrow.CZ_CarKod;
                    //this.SelectedRow.SKL_ID = frmuziv.returnrow.SKL_ID;
                    //this.SelectedRow.LOCNCODE = frmuziv.returnrow.LOCNCODE;
                    //this.SelectedRow.MJ = frmuziv.returnrow.MJ;
                    //this.SelectedRow.QTYSHPPD = frmuziv.returnrow.QTYSHPPD;
                    //this.SelectedRow.QTYPACK = frmuziv.returnrow.QTYPACK;
                    //this.SelectedRow.CZ_DatVyr_Track = frmuziv.returnrow.CZ_DatVyr_Track;
                    //this.SelectedRow.CZ_DatVyr_Delka = frmuziv.returnrow.CZ_DatVyr_Delka;
                    //this.SelectedRow.CZ_SerNum_Track = frmuziv.returnrow.CZ_SerNum_Track;
                    //this.SelectedRow.CZ_SerNum_Delka = frmuziv.returnrow.CZ_SerNum_Delka;
                    //this.SelectedRow.CZ_SW_Track = frmuziv.returnrow.CZ_SW_Track;
                    //this.SelectedRow.CZ_SW_Delka = frmuziv.returnrow.CZ_SW_Delka;
                    //this.SelectedRow.CZ_Doslo = frmuziv.returnrow.CZ_Doslo;
                    //this.SelectedRow.Note = frmuziv.returnrow.Note;
                    //this.SelectedRow.TYPEPAL = frmuziv.returnrow.TYPEPAL;
                    //this.SelectedRow.QTYPAL = frmuziv.returnrow.QTYPAL;
                    //this.SelectedRow.PRIORITY = frmuziv.returnrow.PRIORITY;
                    //this.SelectedRow.PRINTED = frmuziv.returnrow.PRINTED;
                    //this.SelectedRow.USERID = frmuziv.returnrow.USERID;
                    ////this.SelectedRow.DEX_ROW_ID = frmuziv.returnrow.DEX_ROW_ID;

                    //this.dsVydej.CZMST_SE.AcceptChanges();
                }

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UvolnitDavku()
        {
            //
            //Uvolnovat podle SOPNUMBER?
            //uvolnovat řadek po řadku?
            // vyskakovaci okno ktere vypiše seznam všech SOPNUMBER vyexportovanych s možnosti uvolnit?
            //....
            try
            {

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam uvolnení", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_UvolnitDavku))
                    ((Fask.Interfaces.Vydej.IVydej2_UvolnitDavku)providerVydej).Vydej_UvolnitDavku(SelectedRow.CountEntries);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_UvolnitDavku.");


                PerformOK();

            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteDavka()
        {
            try
            {

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro smazani", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_DeleteSE))
                    ((Fask.Interfaces.Vydej.IVydej2_DeleteSE)providerVydej).DeleteSE(SelectedRow.DEX_ROW_ID);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_DeleteSE.");

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SloucitDavku()
        {
            try
            {

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro sloučení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRow.CZ_Doslo == 0)
                {
                    MessageBox.Show("Nelze sloučit data do již uvolněné dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if ((SelectedRow.CZ_Doslo > 0) && (SelectedRow.CZ_Doslo < 100))
                {
                    MessageBox.Show("Nelze sloučit data do již stažené dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if ((SelectedRow.CZ_Doslo > 100) && (SelectedRow.CZ_Doslo < 200))
                {
                    MessageBox.Show("Nelze sloučit data do již spracované dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRow.CZ_Doslo == 201)
                {
                    MessageBox.Show("Nelze sloučit data do již stornované dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                GenerateDavka(SelectedRow.CountEntries, SelectedRow.SKL_ID);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonKontrola_Click(object sender, EventArgs e)
        {
            PerformCheck();
        }

        private void PerformCheck()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormDavkyVydejeKontrola frmuziv = new FormDavkyVydejeKontrola())
                {
                    frmuziv.CountEntriesCurrent = SelectedRow.CountEntries;
                    frmuziv.Text = string.Format("Číslo dávky: {0} ", SelectedRow.CountEntries);
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;
                }

                //PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void PerformPrint()
        {


            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                Fask.Interfaces.DataSets.Vydej ds;
                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();

                filtr.CountEntries = SelectedRow.CountEntries.ToString();
                filtr.MrtveDavky = true;
                filtr.NEUvolneneDavky = true;
                filtr.SpracovaneDavky = true;
                filtr.StazeneDavky = true;
                filtr.UvolneneDavky = true;


                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky))
                    ds = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky)providerVydej).GetFiltrovaneDavky(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetFiltrovaneDavky.");

                IEnumerable<IGrouping<string, Fask.Interfaces.DataSets.Vydej.CZMST_SERow>> grup = ds.CZMST_SE.GroupBy(x => x.SOPNUMBE);
                //List<string> SOPNUMBERList = new List<string>();

                Fask.Interfaces.DataSets.Vydej.CZMST_SERow First = null;
                //bool first = true;
                string SOPNUMBE;
                string VNDDOCNM;

                var gruplist = grup.ToList();
                First = gruplist[0].ToList()[0];

                if (gruplist.Count > 1)
                {
                    SOPNUMBE = "-";
                    VNDDOCNM = "-";
                }
                else
                {
                    SOPNUMBE = First.SOPNUMBE.Trim();
                    VNDDOCNM = First.VNDDOCNM.Trim();
                }


                //First = gruplist[0].ToList()[0];

                //foreach (IGrouping<string, Fask.Interfaces.DataSets.Vydej.CZMST_SERow> item in grup)
                //{
                //    item.ToArray();
                //    if (!string.IsNullOrEmpty(item.Key))
                //    {
                //        if (first)
                //        {
                //            var a = item.GetEnumerator();
                //            First = a.Current;
                //            first = false;
                //        }
                //        SOPNUMBERList.Add(item.Key.Trim()); 
                //    }

                //}



                //if (SOPNUMBERList.Count > 1)
                //    NUMOBJ = "-";
                //else
                //    NUMOBJ = SOPNUMBERList[0].Trim();

                Fask.Interfaces.DataSets.Vydej dsAdresa;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetAdresaOdberatel))
                    dsAdresa = ((Fask.Interfaces.Vydej.IVydej2_GetAdresaOdberatel)providerVydej).GetAdresaOdberatel(First.SOPNUMBE);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetAdresaOdberatel.");

                //Fask.Interfaces.DataSets.Vydej.Adresy_OdberatelRow AdresaRow = null;
                Fask.Interfaces.DataSets.Vydej.OBJ_AdresyRow OBJAdresaRow = null;


                string ICO = string.Empty;
                string DIC = string.Empty;
                string fax = string.Empty;


                string Firma = string.Empty;
                string Ulice = string.Empty;
                string PSC = string.Empty;
                string Obec = string.Empty;
                string tel = string.Empty;
                string email = string.Empty;

                if ((dsAdresa != null) && (dsAdresa.OBJ_Adresy.Count > 0))
                {
                    OBJAdresaRow = dsAdresa.OBJ_Adresy.First();

                    ICO = OBJAdresaRow.IsICONull() ? "-" : OBJAdresaRow.ICO.Trim();
                    DIC = OBJAdresaRow.IsDICNull() ? "-" : OBJAdresaRow.DIC.Trim();
                    fax = OBJAdresaRow.IsFaxNull() ? "-" : OBJAdresaRow.Fax.Trim();


                                        if (
                        !OBJAdresaRow.IsFirma2Null() ||
                        !OBJAdresaRow.IsUlice2Null() ||
                        !OBJAdresaRow.IsPSC2Null() ||
                        !OBJAdresaRow.IsObec2Null() ||
                        !OBJAdresaRow.IsTel2Null() ||
                        !OBJAdresaRow.IsEmail2Null()
                        )
                    {

                        Firma = OBJAdresaRow.IsFirma2Null() ? "-" : OBJAdresaRow.Firma2.Trim();
                        Ulice = OBJAdresaRow.IsUlice2Null() ? "-" : OBJAdresaRow.Ulice2.Trim();
                        PSC = OBJAdresaRow.IsPSC2Null() ? "-" : OBJAdresaRow.PSC2.Trim();
                        Obec = OBJAdresaRow.IsObec2Null() ? "-" : OBJAdresaRow.Obec2.Trim();
                        tel = OBJAdresaRow.IsTel2Null() ? "-" : OBJAdresaRow.Tel2.Trim();
                        email = OBJAdresaRow.IsEmail2Null() ? "-" : OBJAdresaRow.Email2.Trim();
                    }
                    else
                    {
                        Firma = OBJAdresaRow.IsFirmaNull() ? "-" : OBJAdresaRow.Firma.Trim();
                        Ulice = OBJAdresaRow.IsUliceNull() ? "-" : OBJAdresaRow.Ulice.Trim();
                        PSC = OBJAdresaRow.IsPSCNull() ? "-" : OBJAdresaRow.PSC.Trim();
                        Obec = OBJAdresaRow.IsObecNull() ? "-" : OBJAdresaRow.Obec.Trim();
                        tel = OBJAdresaRow.IsTelNull() ? "-" : OBJAdresaRow.Tel.Trim();
                        email = OBJAdresaRow.IsEmailNull() ? "-" : OBJAdresaRow.Email.Trim();
                    }



                }

                Fask.Interfaces.DataSets.Vydej dstmp = new Fask.Interfaces.DataSets.Vydej();
                dstmp.Adresy_Odberatel.AddAdresy_OdberatelRow(  Firma ,
                                                                Ulice ,
                                                                PSC ,
                                                                Obec,
                                                                ICO ,
                                                                DIC,
                                                                tel,
                                                                fax,
                                                                email);


                PrintReport(ds, SelectedRow.CountEntries.ToString(), First.SOPNUMBE, SOPNUMBE, VNDDOCNM, dstmp.Adresy_Odberatel.First());


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReport(Fask.Interfaces.DataSets.Vydej ds, string CountEntries, string SOPNUMBE_IMG, string SOPNUMBE, string VNDDOCNM, Fask.Interfaces.DataSets.Vydej.Adresy_OdberatelRow AdresaRow)
        {
            using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
            {

                plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_Obec),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_ICO", "64086551"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_DIC", "DE259853840"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Telefon", "+42059663713,"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Fax", "+420596637130"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Email", "info@i-tec.cz"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_WWW", "www.i-tec.cz"),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Firma", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_FirmaNull() ? "-" :  AdresaRow.Param_Odberatel_Firma))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Adresa", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_AdresaNull() ? "-" :  AdresaRow.Param_Odberatel_Adresa))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_PSC", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_PSCNull() ? "-" :  AdresaRow.Param_Odberatel_PSC))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Obec", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_ObecNull() ? "-" :  AdresaRow.Param_Odberatel_Obec))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_ICO", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_ICONull() ? "-" :  AdresaRow.Param_Odberatel_ICO))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_DIC", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_DICNull() ? "-" :  AdresaRow.Param_Odberatel_DIC))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Telefon", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_TelefonNull() ? "-" :  AdresaRow.Param_Odberatel_Telefon))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Fax", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_FaxNull()  ? "-" :  AdresaRow.Param_Odberatel_Fax))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Email", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_EmailNull() ? "-" :  AdresaRow.Param_Odberatel_Email))),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_WWW", "www.imgramko.cz"),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PrijatyDoklad", VNDDOCNM),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString())

                    
                    //new Microsoft.Reporting.WinForms.ReportParameter("DatumOd",date.First().Modified == null ? "-" : date.First().Modified.ToShortDateString() ),
                    //new Microsoft.Reporting.WinForms.ReportParameter("DatumDo",date.Last().Modified == null ? "-" : date.Last().Modified.ToShortDateString()),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Status" ,status)


                   
                };

                plr.NazevDataTable = "DataSet";
                plr.DataTable = ds.CZMST_SE;

                List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                plr.Projekt = PrintReportLibrary.Projekt.MST;
                plr.ShowPreview = true;
                plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].DavkyTemplate);

                plr.CountEntries = CountEntries;
                plr.HlavickaKod = SOPNUMBE;
                plr.HlavickaKodIMG = SOPNUMBE_IMG.Trim();

                plr.Print(this);

            }
        }

        #region Menu Event

        private void ExcelExportovatVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelExportAll();
        }

        private void ExcelExportovatOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelExportSelect();
        }

        private void upravitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void Kontrola_tsmi_Click(object sender, EventArgs e)
        {
            PerformCheck();
        }

        private void Tisk_tsmi_Click(object sender, EventArgs e)
        {
            PerformPrint();
        }

        private void smazatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDavka();
        }

        private void generovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDavka(null, string.Empty);
        }

        private void sloučitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SloucitDavku();
        }

        private void uvolnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UvolnitDavku();
        }

        #endregion


        #region RightMenu Event

        ////        mnuEdit.Click += new EventHandler(mnuEdit_Click);
        ////mnuDelete.Click += new EventHandler(mnuDelete_Click);
        ////mnuGenerate.Click += new EventHandler(mnuGenerate_Click);

        //private void mnuEdit_Click(object sender, EventArgs e)
        //{
        //    mnuEdit.Checked = !mnuEdit.Checked;
        //    UpdateButtons();
        //}

        //private void mnuDelete_Click(object sender, EventArgs e)
        //{
        //    mnuDelete.Checked = !mnuDelete.Checked;
        //    UpdateButtons();
        //}

        //private void mnuGenerate_Click(object sender, EventArgs e)
        //{
        //    mnuGenerate.Checked = !mnuGenerate.Checked;
        //    UpdateButtons();
        //}


        //private void mnuMerge_Click(object sender, EventArgs e)
        //{
        //    mnuMerge.Checked = !mnuMerge.Checked;
        //    UpdateButtons();
        //}

        //private void mnuRelease_Click(object sender, EventArgs e)
        //{
        //    mnuRelease.Checked = !mnuRelease.Checked;
        //    UpdateButtons();
        //}

        //private void mnuPrint_Click(object sender, EventArgs e)
        //{
        //    mnuPrint.Checked = !mnuPrint.Checked;
        //    UpdateButtons();
        //}

        //private void mnuExportExcelAll_Click(object sender, EventArgs e)
        //{
        //    mnuExportExcelAll.Checked = !mnuExportExcelAll.Checked;
        //    UpdateButtons();
        //}

        //private void mnuExportExcelSelect_Click(object sender, EventArgs e)
        //{
        //    mnuExportExcelSelect.Checked = !mnuExportExcelSelect.Checked;
        //    UpdateButtons();
        //}

        //private void mnuBatchControl_Click(object sender, EventArgs e)
        //{
        //    mnuBatchControl.Checked = !mnuBatchControl.Checked;
        //    UpdateButtons();
        //}

        //private void mnuEnd_Click(object sender, EventArgs e)
        //{
        //    mnuEnd.Checked = !mnuEnd.Checked;
        //    UpdateButtons();
        //}






        //private void UpdateButtons()
        //{

        //    // TODO 
        //    // 1. ukladat informace o nastaveni buttons do nejakeho xml souboru a nasledne pri Load načist
        //    // NECO JE 2. Dynamicky počitat pozice tlačite aby šli za sebou
        //    // NECO JE 3. Mnet nejaku hierarchii v jakem pořadi se budou zobrazovat

        //    panel2.Controls.Clear();

        //    GroupBox separe = new GroupBox();
        //    separe.Text = string.Empty;
        //    separe.Size = new Size(panel2.Width, 3);
        //    separe.BackColor = Color.Black;
        //    separe.Dock = DockStyle.Top;


        //    if (mnuEnd.Checked) { panel2.Controls.Add(btn_Menu_Konec); }

        //    //if (mnuDelete.Checked ||
        //    //    mnuEdit.Checked ||
        //    //    mnuGenerate.Checked ||
        //    //    mnuMerge.Checked ||
        //    //    mnuRelease.Checked ||
        //    //    mnuPrint.Checked ||
        //    //    mnuExportExcelAll.Checked ||
        //    //    mnuExportExcelSelect.Checked ||
        //    //    mnuBatchControl.Checked)
        //    //{
        //    //    panel2.Controls.Add(separe);
        //    //}


        //    if (mnuExportExcelSelect.Checked) { panel2.Controls.Add(btn_Vystup_ExcelExportovatOznacene); }

        //    if (mnuExportExcelAll.Checked) { panel2.Controls.Add(btn_Vystup_ExcelExportovatVse); }

        //    if (mnuPrint.Checked) { panel2.Controls.Add(btn_Vystup_ReportTiskVse); }


        //    //if (mnuDelete.Checked ||
        //    //    mnuEdit.Checked ||
        //    //    mnuGenerate.Checked ||
        //    //    mnuMerge.Checked ||
        //    //    mnuRelease.Checked ||
        //    //    mnuBatchControl.Checked)
        //    //{
        //    //    panel2.Controls.Add(separe);
        //    //}

        //    if (mnuBatchControl.Checked) { panel2.Controls.Add(btn_Menu_KontrolaDavky); }

        //    //if (mnuDelete.Checked ||
        //    //    mnuEdit.Checked ||
        //    //mnuGenerate.Checked ||
        //    //mnuMerge.Checked ||
        //    //mnuRelease.Checked)
        //    //{
        //    //    panel2.Controls.Add(separe);
        //    //}

        //    if (mnuRelease.Checked) { panel2.Controls.Add(btn_Davka_Release); }
        //    if (mnuMerge.Checked) { panel2.Controls.Add(btn_Davka_Merge); }
        //    if (mnuGenerate.Checked) { panel2.Controls.Add(btn_Davka_Generate); }
        //    if (mnuDelete.Checked) { panel2.Controls.Add(btn_Davka_Delete); }
        //    if (mnuEdit.Checked) { panel2.Controls.Add(btn_Davka_Edit); }

        //}

        #endregion

        #region Move

        //private Point pointMouse = new Point();
        private Control ctrlMoved = new Control();
        //private bool bMoving = false;


        #endregion
        
        private void buttonVyhledat_MouseDown(object sender, MouseEventArgs e)
        {
            ////if not left mouse button, exit
            //if (e.Button != MouseButtons.Middle)
            //{
            //    return;
            //}
            //// save cursor location
            //pointMouse = e.Location;
            ////remember that we're moving
            //bMoving = true;

        }

        private void buttonVyhledat_MouseUp(object sender, MouseEventArgs e)
        {
            //bMoving = false;

        }

        private void buttonVyhledat_MouseMove(object sender, MouseEventArgs e)
        {
            ////if not being moved or left mouse button not used, exit
            //if (!bMoving || e.Button != MouseButtons.Middle)
            //{
            //    return;
            //}
            ////get control reference
            //ctrlMoved = (Control)sender;
            ////set control's position based upon mouse's position change
            //ctrlMoved.Left += e.X - pointMouse.X;
            //ctrlMoved.Top += e.Y - pointMouse.Y;
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dataGridView1.CurrentCell.ColumnIndex + 1 >= dataGridView1.ColumnCount;
                bool endrow = dataGridView1.CurrentCell.RowIndex + 1 >= dataGridView1.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dataGridView1.CurrentCell.ColumnIndex;
                    startRow = dataGridView1.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dataGridView1.CurrentCell.ColumnIndex + 1;
                    startRow = dataGridView1.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dataGridView1.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dataGridView1.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dataGridView1.CurrentCell = c;
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
    }
}

