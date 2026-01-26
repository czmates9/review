using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using ZXing;
using ZXing.Common;
using System.Windows.Forms;
using Fask.Logging;
using Konzola.Extensions;
using System.Reflection;
using Konzola.Vyroba.Transakce;
using QRCoder;
using Konzola.Vyroba.Rozbory;
using ZXing.Rendering;
using ZXing.Datamatrix;
using Fask.ModuleSql_API.Classes;
using System.Text;
using MST_Print_Server_ZPL_Printing;
using System.Linq;
using System.Globalization;

namespace Konzola.Vyroba
{
    public partial class FormVyrobniPrikazList : Form
    {

        #region Nazvy pro filtry

        private string _ClassName { get { return this.GetType().ToString(); } }
        private string _filtrName_VPH { get { return _ClassName + "VPH"; } }
        private string _filtrName_VPP { get { return _ClassName + "VPP"; } }

        private string _filtrName_VPH_Filtr { get { return _ClassName + "_VPH.filtr"; } }
        private string _filtrName_VPP_Filtr { get { return _ClassName + "_VPP.filtr"; } }

        #endregion

        private const string OrderByPS = "DEX_ROW_ID";

        private Fask.Interfaces.IMES providerVPH = null;
        private Fask.Interfaces.IMES providerVPP = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow SelectedVPHRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgVPH.BindingContext[this.bsVPH].Current)).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow CZPRO_VPP_selectedVPPRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgVPP.BindingContext[this.bsVPP].Current)).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> CZPRO_VPH_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();

            //    foreach (DataGridViewRow selectedRow in dgVPH.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVPH.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> CZPRO_VPP_selectedVPPRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();

            //    foreach (DataGridViewRow selectedRow in dgVPP.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVPP.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        #region Parametry filtru

        #region Parametry VPH


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.Vyroba_VPH_Filtr> filtry_VPH = new List<Fask.Interfaces.Filtry.Vyroba_VPH_Filtr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Vyroba_VPH_Filtr rowFiltr_VPH
        {
            get
            {
                try
                {
                    return tscbFiltry_VPH.SelectedItem as Fask.Interfaces.Filtry.Vyroba_VPH_Filtr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion

        #region VPP


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.Vyroba_VPP_Filtr> filtry_VPP = new List<Fask.Interfaces.Filtry.Vyroba_VPP_Filtr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Vyroba_VPP_Filtr rowFiltr_VPP
        {
            get
            {
                try
                {
                    return tscbFiltry_VPP.SelectedItem as Fask.Interfaces.Filtry.Vyroba_VPP_Filtr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion


        #endregion

        #region Eventy formu + konstruktor

        public FormVyrobniPrikazList()
        {
            InitializeComponent();
            this.dgVPH.UpdateColumnHeaderCellsByDatasource();
            this.dgVPP.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
        }

        private void FormVyrobniPrikazList_KeyDown(object sender, KeyEventArgs e)
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
            else if ((e.Control && e.KeyCode == Keys.T))
            {
                PrimyTiskZPL(); // Simuluje kliknutí na tlačítko
            }
            else if ((e.Control && e.KeyCode == Keys.R))
            {
                PrimyTiskRDLC(); // Simuluje kliknutí na tlačítko
            }
            else
                return;

            e.Handled = true;
        }

        private void FormVyrobniPrikazList_Load(object sender, EventArgs e)
        {
            try
            {
                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_ZP())
                {
                    tiskEtiketToolStripMenuItem.Enabled = true;



                }
                else
                {
                    tiskEtiketToolStripMenuItem.Enabled = false;
                    tsmiVystup.DropDownItems.Remove(tiskEtiketToolStripMenuItem);
                }



                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgVPH.LoadConfiguration(_filtrName_VPH);
                this.dgVPP.LoadConfiguration(_filtrName_VPP);

                panelButtons.LoadConfiguration(_ClassName);
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBarVPP.SetColumns(dgVPP.Columns);
                advancedDataGridViewSearchToolBarVPH.SetColumns(dgVPH.Columns);

                InitProvider();

                if (providerVPH == null)
                    throw new Exception("Provider 'VPH' není inicializován");

                if (providerVPP == null)
                    throw new Exception("Provider 'VPP' není inicializován");

                cb_OrderBy_VPP.SelectedIndex = 0;

                cb_Active.DataSource = Enum.GetValues(typeof(Fask.Interfaces.Classes.VyrobaStavPrikazu));
                cb_Active.SelectedIndex = -1;

                //// načtení konfigurace vytvořených filtrů
                this.filtry_VPH = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Vyroba_VPH_Filtr>(_filtrName_VPH_Filtr);
                tscbFiltry_VPH.ComboBox.DataSource = this.filtry_VPH;
                tscbFiltry_VPH.SelectedItem = null;
                tscbFiltry_VPH.ComboBox.DropDownWitdhAutosize();

                //// načtení konfigurace vytvořených filtrů
                this.filtry_VPP = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Vyroba_VPP_Filtr>(_filtrName_VPP_Filtr);
                tscbFiltry_VPP.ComboBox.DataSource = this.filtry_VPP;
                tscbFiltry_VPP.SelectedItem = null;
                tscbFiltry_VPP.ComboBox.DropDownWitdhAutosize();

                SetStatusLabelText_VPH(-1);
                SetStatusLabelText_VPP(-1);

                //buttonOdznacitVse_VPP_Click(null, null);


                // UpdateForm();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVyrobniPrikazList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgVPH.SaveConfiguration(_filtrName_VPH);
                this.dgVPP.SaveConfiguration(_filtrName_VPP);

                panelButtons.SaveConfiguration(_ClassName);

                this.filtry_VPH.WriteXML(_filtrName_VPH_Filtr);
                this.filtry_VPP.WriteXML(_filtrName_VPP_Filtr);

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].FormVyrobniPrikazList_SplitPoloha = splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVyrobniPrikazList_Shown(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].FormVyrobniPrikazList_SplitPoloha;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                splitContainer1.SplitterDistance = 1000;
            }
        }

        #endregion

        #region Inicializace provideru

        private void InitProvider()
        {
            #region providerVPH

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

            #endregion

            #region providerVPP

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPP.IVPP).IsAssignableFrom(t))
                            {
                                providerVPP = (Fask.Interfaces.Vyroba.VPP.IVPP)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPP.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

        }

        #endregion

        #region Update Formu  OLD

        /// <summary>
        /// Aktualizace dat.
        /// </summary>
        //private void UpdateForm()
        //{
        //    try
        //    {
        //        int FirstDisplayedScrollingRowIndex = this.dgVPH.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
        //        this.ds.Clear();
        //        //this.vyrobaDataSet1.AcceptChanges();

        //        //var adapterVPH = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
        //        //adapterVPH.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //        //adapterVPH.Fill(this.vyrobaDataSet1.CZPRO_VPH);


        //        if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill)
        //            ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(this.ds);
        //        else
        //            throw new Exception("IVPH_Fill not implementet");



        //        if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVPH.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVPH.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
        //        if (SelectedVPHRow != null)
        //        {
        //            //var adapterVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
        //            //adapterVPP.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //            ////adapterVPP.Fill(this.vyrobaDataSet1.CZPRO_VPP);
        //            //adapterVPP.FillByCountEntriesAndSOPNUMBE(this.vyrobaDataSet1.CZPRO_VPP, SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);


        //            if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)
        //                ((Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)providerVPP).FillByCountEntriesAndSOPNUMBE(this.ds, GetSearch_OrderBy(), SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
        //            else
        //                throw new Exception("IVPP_FillByCountEntriesAndSOPNUMBE not implementet");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// Aktualizace dat.
        /// </summary>
        //private void UpdateFormVpp(string itemnmbr)
        //{
        //    try
        //    {
        //        if (SelectedVPHRow != null)
        //        {
        //            //var adapterVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
        //            //adapterVPP.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //            //adapterVPP.FillByCountEntriesAndSOPNUMBE(this.vyrobaDataSet1.CZPRO_VPP, SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);

        //            if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)
        //                ((Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)providerVPP).FillByCountEntriesAndSOPNUMBE(this.ds, GetSearch_OrderBy(), SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
        //            else
        //                throw new Exception("IVPP_FillByCountEntriesAndSOPNUMBE not implementet");

        //            int index = bsVPP.Find(this.ds.CZPRO_VPP.ITEMNMBRColumn.ColumnName, itemnmbr);
        //            bsVPP.Position = index;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// Aktualizace dat.
        /// </summary>
        //private void UpdateForm(string sopnumbe)
        //{
        //    try
        //    {
        //        //int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
        //        this.ds.Clear();
        //        //this.vyrobaDataSet1.AcceptChanges();

        //        //var adapterVPH = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
        //        //adapterVPH.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //        //adapterVPH.Fill(this.vyrobaDataSet1.CZPRO_VPH);

        //        if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill)
        //            ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(this.ds);
        //        else
        //            throw new Exception("IVPH_Fill not implementet");

        //        //if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
        //        int index = bsVPH.Find(this.ds.CZPRO_VPH.SOPNUMBEColumn.ColumnName, sopnumbe);
        //        bsVPH.Position = index;
        //        if (SelectedVPHRow != null)
        //        {
        //            //var adapterVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
        //            //adapterVPP.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
        //            ////adapterVPP.Fill(this.vyrobaDataSet1.CZPRO_VPP);
        //            //adapterVPP.FillByCountEntriesAndSOPNUMBE(this.vyrobaDataSet1.CZPRO_VPP, SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);

        //            if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)
        //                ((Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)providerVPP).FillByCountEntriesAndSOPNUMBE(this.ds, GetSearch_OrderBy(), SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
        //            else
        //                throw new Exception("IVPP_FillByCountEntriesAndSOPNUMBE not implementet");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion

        #region tsmi_  Events






        private void tsmi_konec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }
        #endregion

        #region Perform Metody

        #region Tisky
        private void PerformPrint()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedVPHRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }



                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable)ds.CZPRO_VPP.Copy();

                dt.Columns.Add("BarcodeP_IMG", typeof(string));
                dt.Columns.Add("VNDITNUM_IMG", typeof(string));

                dt.Columns.Add("A_2D_DataMatrix_IMG", typeof(string));
                dt.Columns.Add("A_2D_DataMatrix_kod", typeof(string));
                string A_2D_kod = string.Empty;
                string A_2D_kod_GS1 = string.Empty;
                
                string SERLTNUM = string.Empty;
               
                string VNDITNUM = string.Empty;
                DateTime? EXPIRACE = null;

                string datumexpirace01 = string.Empty;
                string sn = string.Empty;
                dt.Columns.Add("datumExpirace", typeof(string));
                dt.Columns.Add("SN", typeof(string));

                dt.Columns.Add("QTY_Paleta", typeof(decimal));
                dt.Columns.Add("MJ_Paleta", typeof(string));


                foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow item in dt)
                {

                    if (!string.IsNullOrEmpty(item.VNDITNUM))
                    {
                        ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                        zw.Format = ZXing.BarcodeFormat.CODE_128;
                        zw.Options.Height = 50; //50
                        zw.Options.PureBarcode = true;
                        System.Drawing.Bitmap image1 = zw.Write(item.VNDITNUM);

                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        byte[] imgReportBarcode = ms.ToArray();
                        ms.Close();

                        string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                        item["VNDITNUM_IMG"] = Base64Imahe;


                        EXPIRACE = DateTime.Now;
                        if (EXPIRACE.HasValue)
                        {

                            datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                            sn = EXPIRACE.Value.ToString("yy");
                        }

                        //2D kod
                        VNDITNUM = item.VNDITNUM;
                        if (VNDITNUM.Length < 13)
                        {
                            VNDITNUM = VNDITNUM.PadLeft(13, '0');
                        }


                        if (VNDITNUM.Length == 14)
                        {
                            string WithoutCheeckDigit = VNDITNUM.Substring(0, 13);

                            if (VNDITNUM == AddCheckDigit(WithoutCheeckDigit))
                            {
                                A_2D_kod += "(01)" + AddCheckDigit(WithoutCheeckDigit);
                                A_2D_kod_GS1 += "01" + AddCheckDigit(WithoutCheeckDigit);
                            }
                            else
                            {
                                MessageBox.Show("GTIN nemá správnou strukturu, nesedí kontrolní číslice!", "ERROR");
                                throw new Exception("GTIN nemá správnou strukturu, nesedí kontrolní číslice!");
                            }

                        }
                        else if (VNDITNUM.Length == 13)
                        {
                            A_2D_kod += "(01)" + AddCheckDigit(VNDITNUM);
                            A_2D_kod_GS1 += "01" + AddCheckDigit(VNDITNUM);
                        }
                        else
                        {
                            MessageBox.Show("GTIN nemá 14 znaků", "ERROR");
                            throw new Exception("GTIN nemá 14 znaků");
                        }





                    }

                    EXPIRACE = DateTime.Now;
                    if (EXPIRACE.HasValue)
                    {

                        string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                        A_2D_kod += "(11)" + datumexpirace;
                        A_2D_kod_GS1 += "11" + datumexpirace;
                    }

                   // A_2D_kod += "(11)" + DateTime.Now.ToString() + @"\n";

                    if (!string.IsNullOrEmpty(item.BarcodeP))
                    {
                        ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                        zw.Format = ZXing.BarcodeFormat.CODE_128;
                        zw.Options.Height = 50; //50
                        zw.Options.PureBarcode = true;
                        System.Drawing.Bitmap image1 = zw.Write(item.BarcodeP);

                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        byte[] imgReportBarcode = ms.ToArray();
                        ms.Close();

                        string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                        item["BarcodeP_IMG"] = Base64Imahe;

                        //2D kod
                        A_2D_kod += "(21)" + item.BarcodeP;
                        A_2D_kod_GS1 += "21" + item.BarcodeP;
                        sn += "-" + item.BarcodeP;

                    }


                    #region 2D_Datamatrix GS1

                    // GS1 DataMatrix kód s FNC1 symbolem
                    string gs1Data = (char)29 + A_2D_kod_GS1;
                    // Generování GS1 DataMatrix
                    Bitmap gs1DataMatrixImage = GenerateGS1DataMatrix(gs1Data);
                    MemoryStream ms1 = new MemoryStream();
                    gs1DataMatrixImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);

                    byte[] imgReportBarcode1 = ms1.ToArray();
                    ms1.Close();

                    string Base64Imahe1 = Convert.ToBase64String(imgReportBarcode1);




                    item["A_2D_DataMatrix_IMG"] = Base64Imahe1;
                    item["A_2D_DataMatrix_kod"] = A_2D_kod;

                    item["datumExpirace"] = datumexpirace01;
                    item["SN"] = sn;
                    A_2D_kod = string.Empty;
                    gs1Data = string.Empty;
                    A_2D_kod_GS1 = string.Empty;
                    #endregion



                    if (item.QTYPACK > 0)
                        item["QTY_Paleta"] = item.QTYSHPPD / item.QTYPACK;
                    else
                        item["QTY_Paleta"] = 0;

                    item["MJ_Paleta"] = "Pal";

                    if (string.IsNullOrEmpty(item.ITEMMJ))
                        item.ITEMMJ = "-";
                }

                //metoda na UDI code
                //ProviderTisk tisk = new ProviderTisk();
                //string temp = string.Empty;
                //tisk.TiskMetodaEtiketa_GS1(ref dt);

                string sopdesc = SelectedVPHRow.IsSOPDESCNull() ? string.Empty : SelectedVPHRow.SOPDESC.Trim();

                PrintReport(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE, dt, sopdesc);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


      #region GS1 metody
        static string AddCheckDigit(string input)
        {
            // Výpočet check digitu
            int sumOdd = 0;
            int sumEven = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int digit = int.Parse(input[i].ToString());

                if (i % 2 == 0)
                {
                    sumEven += digit;
                }
                else
                {
                    sumOdd += digit;
                }
            }

            int totalSum = sumOdd + sumEven * 3;
            int nearestTenMultiple = (int)Math.Ceiling((double)totalSum / 10) * 10;
            int checkDigit = nearestTenMultiple - totalSum;

            // Přidání check digitu na konec vstupního čísla
            return input + checkDigit.ToString();
        }

        static Bitmap GenerateGS1DataMatrix(string gs1Data)
        {
            // Nastavení parametrů pro GS1 formát
            DatamatrixEncodingOptions encodingOptions = new DatamatrixEncodingOptions
            {
                GS1Format = true,
                Height = 300,
                Width = 300
            };

            // Vytvoření instance BarcodeWriter pro DataMatrix
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.DATA_MATRIX;
            barcodeWriter.Options = encodingOptions;

            // Generování kódu
            ZXing.Common.BitMatrix bitMatrix = barcodeWriter.Encode(gs1Data);

            // Převedení BitMatrix na Bitmap
            Bitmap bitmap = BitMatrixToBitmap(bitMatrix);

            return bitmap;
        }

        static Bitmap BitMatrixToBitmap(BitMatrix bitMatrix)
        {
            int width = bitMatrix.Width;
            int height = bitMatrix.Height;
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bitmap.SetPixel(x, y, bitMatrix[x, y] ? Color.Black : Color.White);
                }
            }

            return bitmap;
        }
        #endregion
       

        private void PrintReport(int CountEntries, string SOPNUMBE, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt, string SOPDESC = "")
        {
            try
            {
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyrobni prikaz TISK rdlc-------------------------------------");

                        foreach (var property in item.GetType().GetProperties())
                        {
                            try
                            {
                                var propertyName = property.Name;
                                object propertyValue = null;

                                try
                                {
                                    propertyValue = property.GetValue(item);
                                }
                                catch (Exception ex)
                                {

                                    propertyValue = string.Empty;
                                }

                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{propertyName}: {propertyValue}");
                            }
                            catch (Exception ex)
                            {
                                // Log the exception for the specific property
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, $"Error accessing property: {property.Name}. Exception: {ex.Message}");
                            }
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyrobni prikaz TISK rdlc-------------------------------------");
                    } 
                }



                using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
                {

                    plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Obec),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_SOPDESC", SOPDESC)
                    };

                    plr.NazevDataTable = "DataSet";
                    plr.DataTable = dt;

                    List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                    tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                    plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                    plr.Projekt = PrintReportLibrary.Projekt.MST;
                    plr.ShowPreview = true;

                    #region vybrani tiskove sablony
                    //formular na vybrani tiskove ulohy
                    DataGridViewRow dataGridView = new DataGridViewRow();



                    #region old vybrani tiskove  sablony
                    //// Vytvoření instance formuláře
                    //FormVyrobniPrikazList_Tisk form = new FormVyrobniPrikazList_Tisk();

                    //DialogResult result = form.ShowDialog();
                    //// Zobrazení formuláře
                    //if (result == DialogResult.OK)
                    //{
                    //    dataGridView = (DataGridViewRow)form.Tag;
                    //    // uživatel stiskl OK
                    //}
                    //else if (result == DialogResult.Cancel)
                    //{
                    //    // uživatel stiskl Cancel
                    //    MessageBox.Show("Není vybraná tisková šablona");
                    //    return;
                    //}
                    #endregion

                    #region new vybrani tiskove  sablony
                    // Vytvoření instance formuláře
                    FormOdvod_TiskoveSablony form = null;
                    if (!string.IsNullOrEmpty(SelectedVPHRow.SOPTYPE))
                    {
                         form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), SelectedVPHRow.SOPTYPE);

                    }
                    else
                    {
                        string klic = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC;

                         form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klic, false);

                    }


                    string path = string.Empty;
                    DialogResult result = form.ShowDialog();
                    // Zobrazení formuláře
                    if (result == DialogResult.OK)
                    {
                        //dataGridView = (DataGridViewRow)form.Tag;
                        path = form.ResultString;
                        // uživatel stiskl OK
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // uživatel stiskl Cancel
                        MessageBox.Show("Není vybraná tisková šablona");
                        return;
                    }
                    #endregion

                    //plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrintTemplates", dataGridView.Cells["Název tiskové šablony"].Value.ToString());


                    plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                    #endregion


                    //TODO ošetreny, zda existuje tiskova sestava
                    //plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VyrobnyPrikazTiskTemplate);

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Path Tisk VPH:'" + plr.Path);
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Path to PrinterDirectory:'" + MySystem.MyPath.PrintDirectory);

                    PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);


                    plr.CountEntries = CountEntries.ToString();
                    plr.HlavickaKod = SOPNUMBE;
                    plr.HlavickaKodIMG = SOPNUMBE.Trim();

                    try
                    {
                        plr.Print(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba! Je zvolena správná šablona?");
                        throw ex;
                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region VPH

        private void PerformCreateVPHRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                string sopnumbe = string.Empty;

                using (FormVPHEdit frmzb = new FormVPHEdit())
                {
                    frmzb.Text = "Nový výrobní příkaz";
                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;
                    sopnumbe = frmzb.SOPNUMBE;
                }

                PerformVyhledat_VPH();
                //UpdateForm(sopnumbe);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformEditVPHRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero výrobních příkazů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                if ((SelectedVPHRow == null) || (dgVPH.SelectedRows.Count == 0))
                {
                    MessageBox.Show("Není zvolen příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //10.10.2025 MaR logika osetreni starych dat v pameti VPH 
                //---------------------------------------------------------------------------------------------

                int pom_CountEntries_Selected = SelectedVPHRow.CountEntries;

                Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_VPH_Filtr();
                if (!CreateFilter_VPH(ref filtr))
                    return;

                Fask.Interfaces.DataSets.Vyroba dset = new Fask.Interfaces.DataSets.Vyroba();

                if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)
                    dset = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)providerVPH).GetFiltrovanyVPHList(filtr);
                else
                    throw new Exception("IVPH_GetFiltrovanyVPHList not implementet");


                var zaznam = dset.CZPRO_VPH.FirstOrDefault(x => x.CountEntries == pom_CountEntries_Selected);

                if (zaznam == null)
                {
                    MessageBox.Show("Záznam byl odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PerformVyhledat_VPH();
                    return;
                }


                bool stejny = true;
                //porovnat zaznamy
                 stejny =
    zaznam.CountEntries == SelectedVPHRow.CountEntries &&
    zaznam.SOPNUMBE == SelectedVPHRow.SOPNUMBE &&
    zaznam.SOPDESC == SelectedVPHRow.SOPDESC &&
    zaznam.BarcodeH == SelectedVPHRow.BarcodeH &&
    zaznam.SOPTYPE == SelectedVPHRow.SOPTYPE &&
    zaznam.Active == SelectedVPHRow.Active &&
    zaznam.USERID == SelectedVPHRow.USERID &&
    zaznam.DateProd == SelectedVPHRow.DateProd;


                if (stejny)
                {

                    string sopnumbe = string.Empty;

                    using (FormVPHEdit frmzb = new FormVPHEdit())
                    {
                        frmzb.Text = "Úprava výrobního příkazu";
                        frmzb.VPHrow = SelectedVPHRow;
                        frmzb.ShowDialog();

                        sopnumbe = frmzb.SOPNUMBE;
                    }

                   

                }
                else
                {
                    DialogResult odpoved;

                    //pri ano se obnovi jen ten urcity zaznam a posle se do okna upravit, jinak pri ne se tam da zaznam se starymi daty
                    odpoved = MessageBox.Show("Došlo ke změně dat, obnovit data?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (odpoved == DialogResult.Yes)
                    {
                        string sopnumbe = string.Empty;

                        using (FormVPHEdit frmzb = new FormVPHEdit())
                        {
                            frmzb.Text = "Úprava výrobního příkazu";
                            frmzb.VPHrow = zaznam;
                            frmzb.ShowDialog();

                            sopnumbe = frmzb.SOPNUMBE;
                        }
                    }
                    else
                    {
                        string sopnumbe = string.Empty;

                        using (FormVPHEdit frmzb = new FormVPHEdit())
                        {
                            frmzb.Text = "Úprava výrobního příkazu";
                            frmzb.VPHrow = SelectedVPHRow;
                            frmzb.ShowDialog();

                            sopnumbe = frmzb.SOPNUMBE;
                        }
                    }


                }

                //--------------------------------------------------------------------------------------------



             

                PerformVyhledat_VPH();
                //UpdateForm(sopnumbe);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteVPHRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero výrobních příkazů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((SelectedVPHRow == null) || (dgVPH.SelectedRows.Count == 0))
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedVPHRow.Active >= 1)
                {
                    MessageBox.Show("Je zvoleno aktivní výrobní příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Chcete odstranit příkaz " + SelectedVPHRow.SOPNUMBE.Trim() + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                int CountEntries = SelectedVPHRow.CountEntries;
                string SOPNUMBE = SelectedVPHRow.SOPNUMBE;


                if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE)
                    ((Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE)providerVPP).DeleteByCountEntriesSOPNUMBE(CountEntries, SOPNUMBE);
                else
                    throw new Exception("IVPP_DeleteByCountEntriesSOPNUMBE not implementet");

                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();
                dt.ImportRow(SelectedVPHRow);
                dt.AcceptChanges();
                dt[0].Delete();



                if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Update)
                    ((Fask.Interfaces.Vyroba.VPH.IVPH_Update)providerVPH).Update(dt);
                else
                    throw new Exception("IVPH_Update not implementet");

                //ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVyhledat_VPH()
        {
            try
            {
                DataTable dtchanged = this.ds.CZPRO_VPH.GetChanges();
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

                ProgressIndicatorVyrobekStart_VPH();

                Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_VPH_Filtr();
                if (!CreateFilter_VPH(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVPH.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_VPH.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVPH.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVPH.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop_VPH();
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region VPP

        private void PerformCreateVPPRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero výrobních příkazů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((SelectedVPHRow == null) || (dgVPH.SelectedRows.Count == 0))
                {
                    MessageBox.Show("Není zvolena hlavička výrobního příkazu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (SelectedVPHRow.Active >= 1)
                {
                    MessageBox.Show("Je zvoleno aktivní výrobní příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string itemnmbr = string.Empty;

                using (FormVPPEdit frmzb = new FormVPPEdit())
                {
                    frmzb.Text = "Nové položky pro: " + SelectedVPHRow.SOPDESC;
                    frmzb.rowVPH = SelectedVPHRow;

                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;

                    itemnmbr = frmzb.ITEMNMBR;
                }

                if (SelectedVPHRow != null)
                    PerformVyhledat_VPP(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformEditVPPRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero výrobních příkazů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //10.10.2025 MaR logika osetreni starych dat v pameti VPH a VPP
                //---------------------------------------------------------------------------------------------

                int pom_CountEntries_Selected = SelectedVPHRow.CountEntries;

                Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_VPH_Filtr();
                if (!CreateFilter_VPH(ref filtr))
                    return;

                Fask.Interfaces.DataSets.Vyroba dset = new Fask.Interfaces.DataSets.Vyroba();

                if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)
                    dset = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)providerVPH).GetFiltrovanyVPHList(filtr);
                else
                    throw new Exception("IVPH_GetFiltrovanyVPHList not implementet");


                var zaznam_VPH = dset.CZPRO_VPH.FirstOrDefault(x => x.CountEntries == pom_CountEntries_Selected);

                if (zaznam_VPH == null)
                {
                    MessageBox.Show("Výrobní příkaz byl odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PerformVyhledat_VPH();
                    return;
                }

  

                if (zaznam_VPH.Active == 1)
                {
                    MessageBox.Show("Data byla změněna, výrobní příkaz je aktivní a položky nelze upravovat.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PerformVyhledat_VPH();
                    return;
                }


                //if (SelectedVPHRow.Active >= 1)
                //{
                //    MessageBox.Show("Je zvoleno aktivní výrobní příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if (CZPRO_VPP_selectedVPPRow == null)
                {
                    MessageBox.Show("Není zvolena položka příkazu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pom_CountEntries_Selected = zaznam_VPH.CountEntries;
                string pom_SOPNUMBE_Selected = zaznam_VPH.SOPNUMBE;

                Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr_VPP = new Fask.Interfaces.Filtry.Vyroba_VPP_Filtr();
                filtr_VPP.CountEntries = pom_CountEntries_Selected;
                filtr_VPP.SOPNUMBE = pom_SOPNUMBE_Selected;


                if (!CreateFilter_VPP(ref filtr_VPP))
                    return;

                Fask.Interfaces.DataSets.Vyroba dset_VPP = new Fask.Interfaces.DataSets.Vyroba();

                if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList)
                    dset_VPP = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList)providerVPP).GetFiltrovanyVPPList(filtr_VPP);
                else
                    throw new Exception("IVPP_GetFiltrovanyVPPList not implementet");


                var zaznam_VPP = dset_VPP.CZPRO_VPP.FirstOrDefault(x =>
       x.CountEntries == pom_CountEntries_Selected &&
       x.SOPNUMBE == pom_SOPNUMBE_Selected);


                if (zaznam_VPP == null)
                {
                    MessageBox.Show("Položka byla odstraněna.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (zaznam_VPH != null)
                        PerformVyhledat_VPP(zaznam_VPH.CountEntries, zaznam_VPH.SOPNUMBE);
                    else
                        ClearVPP();
                    return;
                }

                bool stejny = false;

                try
                {
                    stejny =
                 CZPRO_VPP_selectedVPPRow.ITEMDESC == zaznam_VPP.ITEMDESC &&
                 CZPRO_VPP_selectedVPPRow.BarcodeP == zaznam_VPP.BarcodeP &&
                 CZPRO_VPP_selectedVPPRow.BarcodeT == zaznam_VPP.BarcodeT &&
                 CZPRO_VPP_selectedVPPRow.QTYSHPPD == zaznam_VPP.QTYSHPPD &&
                 CZPRO_VPP_selectedVPPRow.QTYPACK == zaznam_VPP.QTYPACK &&
                 CZPRO_VPP_selectedVPPRow.TIMEMODE == zaznam_VPP.TIMEMODE &&
                 CZPRO_VPP_selectedVPPRow.ITEMNMBR.Trim() == zaznam_VPP.ITEMNMBR.Trim() &&
                 (CZPRO_VPP_selectedVPPRow.IsITEMCODENull() ? string.Empty : CZPRO_VPP_selectedVPPRow.ITEMCODE.Trim()) ==
                 (zaznam_VPP.IsITEMCODENull() ? string.Empty : zaznam_VPP.ITEMCODE.Trim()) &&
                 CZPRO_VPP_selectedVPPRow.TIMEUNIT == zaznam_VPP.TIMEUNIT &&
                 CZPRO_VPP_selectedVPPRow.TIMEPREP == zaznam_VPP.TIMEPREP &&
                 CZPRO_VPP_selectedVPPRow.SerNumT == zaznam_VPP.SerNumT &&
                 CZPRO_VPP_selectedVPPRow.CZ_REZ1_Track == zaznam_VPP.CZ_REZ1_Track &&
                 CZPRO_VPP_selectedVPPRow.CZ_REZ2_Track == zaznam_VPP.CZ_REZ2_Track &&
                 CZPRO_VPP_selectedVPPRow.CZ_REZ3_Track == zaznam_VPP.CZ_REZ3_Track &&
                 CZPRO_VPP_selectedVPPRow.CZ_REZ4_Track == zaznam_VPP.CZ_REZ4_Track &&
                 CZPRO_VPP_selectedVPPRow.CZ_REZ5_Track == zaznam_VPP.CZ_REZ5_Track &&
                 (CZPRO_VPP_selectedVPPRow.IsWEIGHT_TARANull() ? string.Empty : CZPRO_VPP_selectedVPPRow.WEIGHT_TARA.ToString(CultureInfo.InvariantCulture)) ==
                 (zaznam_VPP.IsWEIGHT_TARANull() ? string.Empty : zaznam_VPP.WEIGHT_TARA.ToString(CultureInfo.InvariantCulture)) &&
                 (CZPRO_VPP_selectedVPPRow.IsWEIGHT_NETTONull() ? string.Empty : CZPRO_VPP_selectedVPPRow.WEIGHT_NETTO.ToString(CultureInfo.InvariantCulture)) ==
                 (zaznam_VPP.IsWEIGHT_NETTONull() ? string.Empty : zaznam_VPP.WEIGHT_NETTO.ToString(CultureInfo.InvariantCulture)) &&
                 (CZPRO_VPP_selectedVPPRow.IsWEIGHT_TOL_PLUSNull() ? string.Empty : CZPRO_VPP_selectedVPPRow.WEIGHT_TOL_PLUS.ToString(CultureInfo.InvariantCulture)) ==
                 (zaznam_VPP.IsWEIGHT_TOL_PLUSNull() ? string.Empty : zaznam_VPP.WEIGHT_TOL_PLUS.ToString(CultureInfo.InvariantCulture)) &&
                 (CZPRO_VPP_selectedVPPRow.IsWEIGHT_TOL_MINUSNull() ? string.Empty : CZPRO_VPP_selectedVPPRow.WEIGHT_TOL_MINUS.ToString(CultureInfo.InvariantCulture)) ==
                 (zaznam_VPP.IsWEIGHT_TOL_MINUSNull() ? string.Empty : zaznam_VPP.WEIGHT_TOL_MINUS.ToString(CultureInfo.InvariantCulture)) &&
                 (CZPRO_VPP_selectedVPPRow.IsQTYPACKMJNull() ? string.Empty : CZPRO_VPP_selectedVPPRow.QTYPACKMJ) ==
                 (zaznam_VPP.IsQTYPACKMJNull() ? string.Empty : zaznam_VPP.QTYPACKMJ);
                }
                catch (Exception ex)
                {

                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }



                if (stejny)
                {

                    string itemnmbr = string.Empty;

                    using (FormVPPEdit frmzb = new FormVPPEdit())
                    {
                        frmzb.Text = "Nové položky pro: " + SelectedVPHRow.SOPDESC;
                        frmzb.rowVPP = CZPRO_VPP_selectedVPPRow;
                        frmzb.rowVPH = SelectedVPHRow;
                        frmzb.ShowDialog();
                        itemnmbr = frmzb.ITEMNMBR;
                    }



                }
                else
                {
                    DialogResult odpoved;

                    //pri ano se obnovi jen ten urcity zaznam a posle se do okna upravit, jinak pri ne se tam da zaznam se starymi daty
                    odpoved = MessageBox.Show("Došlo ke změně dat, obnovit data?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (odpoved == DialogResult.Yes)
                    {
                        string itemnmbr = string.Empty;

                        using (FormVPPEdit frmzb = new FormVPPEdit())
                        {
                            frmzb.Text = "Nové položky pro: " + zaznam_VPH.SOPDESC;
                            frmzb.rowVPP = zaznam_VPP;
                            frmzb.rowVPH = zaznam_VPH;
                            frmzb.ShowDialog();
                            itemnmbr = frmzb.ITEMNMBR;
                        }
                    }
                    else
                    {
                        string itemnmbr = string.Empty;

                        using (FormVPPEdit frmzb = new FormVPPEdit())
                        {
                            frmzb.Text = "Nové položky pro: " + SelectedVPHRow.SOPDESC;
                            frmzb.rowVPP = CZPRO_VPP_selectedVPPRow;
                            frmzb.rowVPH = SelectedVPHRow;
                            frmzb.ShowDialog();
                            itemnmbr = frmzb.ITEMNMBR;
                        }
                    }


                }

                //--------------------------------------------------------------------------------------------







                //string itemnmbr = string.Empty;

                //using (FormVPPEdit frmzb = new FormVPPEdit())
                //{
                //    frmzb.Text = "Nové položky pro: " + SelectedVPHRow.SOPDESC;
                //    frmzb.rowVPP = CZPRO_VPP_selectedVPPRow;
                //    frmzb.rowVPH = SelectedVPHRow;
                //    frmzb.ShowDialog();
                //    itemnmbr = frmzb.ITEMNMBR;
                //}

                if (SelectedVPHRow != null)
                    PerformVyhledat_VPP(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
                //UpdateFormVpp(itemnmbr);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteVPPRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero výrobních příkazů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (SelectedVPHRow.Active >= 1)
                {
                    MessageBox.Show("Je zvoleno aktivní výrobní příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (CZPRO_VPP_selectedVPPRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Chcete odstranit příkaz " + CZPRO_VPP_selectedVPPRow.ITEMDESC.Trim() + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                
                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //lta.Update(vyrobaDataSet1.CZPRO_VPP);

                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt_delete = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();
                dt_delete.ImportRow(CZPRO_VPP_selectedVPPRow);

                dt_delete.Rows[0].Delete();

                CZPRO_VPP_selectedVPPRow.Delete();

                if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Update)
                    ((Fask.Interfaces.Vyroba.VPP.IVPP_Update)providerVPP).VPP_Update(dt_delete);
                else
                    throw new Exception("IVPP_Update not implementet");

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVyhledat_VPP(int CountEntries, string SOPNUMBE)
        {
            try
            {
                DataTable dtchanged = this.ds.CZPRO_VPP.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (dgVPH.SelectedRows.Count > 1)
                {
                    ClearVPP();
                    return;
                }

                if (bw_VPP.IsBusy)
                {
                    bw_VPP.CancelAsync();
                    while (bw_VPP.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart_VPP();

                Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_VPP_Filtr();

                filtr.CountEntries = CountEntries;
                filtr.SOPNUMBE = SOPNUMBE;


                if (!CreateFilter_VPP(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVPP.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_VPP.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVPP.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVPP.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop_VPP();
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearVPP()
        {
            ds.CZPRO_VPP.Clear();
            bsVPP.DataSource = this.ds;
            SetStatusLabelText_VPP(-1);
            return;
        }

        #endregion

        private void PerformCancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Potřeba materialu


        private void PerformPotrebaMaterialu()
        {
            //throw new NotImplementedException();

            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if ((SelectedVPHRow == null) || (dgVPH.SelectedRows.Count == 0))
                {
                    MessageBox.Show("Není zvolen příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt_VPH = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();

                foreach (DataGridViewRow item in dgVPH.SelectedRows)
                {

                    Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row = GetSelectedRow(item);
                    dt_VPH.ImportRow(row);
                }

                using(FormPotrebaMaterialu frm = new FormPotrebaMaterialu())
                {
                    frm.dt_VPH = dt_VPH;
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow GetSelectedRow(DataGridViewRow item)
        {
            try
            {
                return ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        #endregion

        private void tsmiOdstranitPrikaz_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Přejete si odstranit výrobní příkaz/y?", this.Text, MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            PerformDeleteVPHRecord();
            PerformVyhledat_VPH();

            //UpdateForm();
        }

        #endregion

        private void tsmiUpravitPrikaz_Click(object sender, EventArgs e)
        {
            PerformEditVPHRecord();
            //UpdateForm();
        }

        private void tsmiNovyPrikaz_Click(object sender, EventArgs e)
        {

            bool work = true;

                //PerformOK();
                PerformCreateVPHRecord();
            

         
        }

      
        private void tsmiPridatPolozku_Click(object sender, EventArgs e)
        {
            PerformCreateVPPRecord();
        }

        private void tsmiUpravitPolozku_Click(object sender, EventArgs e)
        {
            PerformEditVPPRecord();
        }

        private void tsmiOdstranitPolozku_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Přejete si odstranit položku/y?", this.Text, MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            PerformDeleteVPPRecord();
        }

        private void tsmiPridatPolozkuHromadne_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedVPHRow == null)
                {
                    MessageBox.Show("Není zvolena hlavička výrobního příkazu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (SelectedVPHRow.Active >= 1)
                {
                    MessageBox.Show("Je zvoleno aktivní výrobní příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string itemnmbr = string.Empty;

                using (Wizard.VyrobniPrikazList_VPP.Form_01_Vyber_Vyrobku fw01 = new Wizard.VyrobniPrikazList_VPP.Form_01_Vyber_Vyrobku())
                {
                    fw01._VPHrow = this.SelectedVPHRow;
                    //fw01._VPPdt = (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable)((DataRowView)(dataGridView2.BindingContext[this.bindingSourceVPP].Current)).DataView.Table;
                    fw01._VPPdt = this.ds.CZPRO_VPP;
                    if (DialogResult.Cancel == fw01.ShowDialog(this))
                        return;
                }

                //UpdateFormVpp(string.Empty);
                if (SelectedVPHRow != null)
                    PerformVyhledat_VPP(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiDuplikace_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero výrobních příkazů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((SelectedVPHRow == null) || (dgVPH.SelectedRows.Count == 0))
                {
                    MessageBox.Show("Není zvolena hlavička výrobního příkazu pro duplikaci", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string sopnumbe = string.Empty;
                Fask.Interfaces.DataSets.Vyroba ds = this.ds;
                using (FormVPHEdit frmzb = new FormVPHEdit())
                {
                    frmzb.Text = "Duplikace výrobního příkazu";
                    frmzb.Duplikace = true;
                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;

                    //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                    //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                    foreach (var item in ds.CZPRO_VPP)
                    {
                        sopnumbe = frmzb.VPHrowCreated.SOPNUMBE;

                        if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Insert)
                        {

                            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dd = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();
                            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row = dd.NewCZPRO_VPPRow();
                            row.SetRealization_StartNull();
                            //parametry radku VPP:
                            row.CountEntries = frmzb.VPHrowCreated.CountEntries;
                            row.SOPNUMBE = frmzb.VPHrowCreated.SOPNUMBE;
                            row.ITEMNMBR = item.ITEMNMBR;
                            row.ITEMTYPE = item.ITEMTYPE;
                            row.ITEMDESC = item.ITEMDESC;
                            row.ITEMMJ = item.ITEMMJ;
                            row.VNDDOCNMP = item.VNDDOCNMP;
                            row.VNDITNUM = item.VNDITNUM;
                            row.ORD = item.ORD;
                            row.BarcodeP = item.BarcodeP;
                            row.LOCNCODE = item.LOCNCODE;
                            row.QTYSHPPD = item.QTYSHPPD = (frmzb.DuplikovatMnozstvi) ? item.QTYSHPPD : 0;
                            row.QTYDOKON = item.QTYDOKON;
                            row.QTYPACK = item.QTYPACK;
                            row.QTYPACKMJ = item.QTYPACKMJ;
                            row.TIMEMODE = item.TIMEMODE;
                            row.TIMEPREP = item.TIMEPREP;
                            row.TIMEUNIT = item.TIMEUNIT;
                            row.DtProdT = item.DtProdT;
                            row.DtProdL = item.DtProdL;
                            row.SerNumT = item.SerNumT;
                            row.SerNumL = item.SerNumL;
                            row.VerT = item.VerT;
                            row.VerL = item.VerL;
                            row.TermID = item.TermID;
                            row.LSTMod = DateTime.Now;
                            row.SetRealization_StartNull();
                            row.SetRealization_StopNull();
                            row.BarcodeT = item.BarcodeT;
                            row.CZ_REZ1_Track = item.CZ_REZ1_Track;
                            row.CZ_REZ2_Track = item.CZ_REZ2_Track;
                            row.CZ_REZ3_Track = item.CZ_REZ3_Track;
                            row.CZ_REZ4_Track = item.CZ_REZ4_Track;
                            row.CZ_REZ5_Track = item.CZ_REZ5_Track;


                            if (item.IsWEIGHT_TARANull())
                                row.SetWEIGHT_TARANull();
                            else
                                row.WEIGHT_TARA =  item.WEIGHT_TARA;

                            if (item.IsWEIGHT_NETTONull())
                                row.SetWEIGHT_NETTONull();
                            else
                                row.WEIGHT_NETTO = item.WEIGHT_NETTO;

                            if (item.IsWEIGHT_TOL_PLUSNull())
                                row.SetWEIGHT_TOL_PLUSNull();
                            else
                                row.WEIGHT_TOL_PLUS = item.WEIGHT_TOL_PLUS;

                            if (item.IsWEIGHT_TOL_MINUSNull())
                                row.SetWEIGHT_TOL_MINUSNull();
                            else
                                row.WEIGHT_TOL_MINUS = item.WEIGHT_TOL_MINUS;

                            dd.AddCZPRO_VPPRow(row);

                        
                            ((Fask.Interfaces.Vyroba.VPP.IVPP_Insert)providerVPP).VPP_Insert_Row(row);

                        }
                        else
                            throw new Exception("IVPP_Insert not implementet");

                    }
                }

                PerformVyhledat_VPH();
                //UpdateForm(sopnumbe);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiPotrebaMaterialu_Click(object sender, EventArgs e)
        {
            PerformPotrebaMaterialu();
        }

        private void advancedDataGridViewSearchToolBarVPH_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgVPH.CurrentCell.ColumnIndex + 1 >= dgVPH.ColumnCount;
                bool endrow = dgVPH.CurrentCell.RowIndex + 1 >= dgVPH.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVPH.CurrentCell.ColumnIndex;
                    startRow = dgVPH.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVPH.CurrentCell.ColumnIndex + 1;
                    startRow = dgVPH.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVPH.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVPH.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVPH.CurrentCell = c;
        }

        private void advancedDataGridViewSearchToolBarVPP_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgVPH.CurrentCell.ColumnIndex + 1 >= dgVPH.ColumnCount;
                bool endrow = dgVPH.CurrentCell.RowIndex + 1 >= dgVPH.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVPH.CurrentCell.ColumnIndex;
                    startRow = dgVPH.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVPH.CurrentCell.ColumnIndex + 1;
                    startRow = dgVPH.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVPH.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVPH.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVPH.CurrentCell = c;
        }

        private void buttonVyhledatVPH_Click(object sender, EventArgs e)
        {
            NastavDatagrid();

            PerformVyhledat_VPH();
            //UpdateForm();
        }

        private void buttonVyhledatVPP_Click(object sender, EventArgs e)
        {
            NastavDatagrid();

            if (SelectedVPHRow != null)
                PerformVyhledat_VPP(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
            else
                ClearVPP();
        }


       private string GetSearch_OrderBy()
        {
            if (cb_OrderBy_VPP.SelectedItem == null)
                return string.Empty;

            string tmp = cb_OrderBy_VPP.SelectedItem.ToString();

            string OrderSearch = OrderByPS;

            if (tmp.Trim() == "sestupně (desc)")
            {
                OrderSearch += " desc";
            }
            else if (tmp.Trim() == "vzestupně (asc)")
            {
                OrderSearch += " asc";
            }

            return OrderSearch;
        }

        private void SetSearch_OrderBy(string order)
        {
            if (order == " desc")
                cb_OrderBy_VPP.SelectedItem = "sestupně (desc)";


            if (order == " asc")
                cb_OrderBy_VPP.SelectedItem = "vzestupně (asc)";

        }

        #region Oznacit/ Odznacit Vse

        private void buttonOdznacitVse_VPP_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgVPP.ClearSelection();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOdznacitVse_VPH_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgVPH.ClearSelection();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOznacitVse_VPH_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgVPH.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Filtre click metody VPH

        private void tsbVycistit_VPH_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr_VPH();
        }

        private void tsbZmena_VPH_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr_VPH();
        }

        private void tsbPridat_VPH_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr_VPH();
        }
        private void tsbOdebrat_VPH_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr_VPH();
        }

        private void tsbNastavit_VPH_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr_VPH(rowFiltr_VPH);
        }

        #endregion

        #region Filtre click metody VPP

        private void tsbVycistit_VPP_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr_VPP();
        }

        private void tsbZmena_VPP_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr_VPP();
        }

        private void tsbPridat_VPP_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr_VPP();
        }
        private void tsbOdebrat_VPP_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr_VPP();
        }

        private void tsbNastavit_VPP_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr_VPP(rowFiltr_VPP);
        }

        #endregion

        #region Filtry VPH

        /// <summary>
        /// Vytvořeni filtru pro dotazeni dat
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter_VPH(ref Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Vyroba_VPH_Filtr();

            if (cb_Active.SelectedItem != null)
            {
                int a = (int)cb_Active.SelectedItem;
                filtr.Active = (byte)a;
            }

            filtr.SOPNUMBE = tb_VyrZak.Text;

            return true;
        }


        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        public void PerformOdebratFiltr_VPH()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_VPH == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_VPH.NazevFiltru) ? string.Empty : rowFiltr_VPH.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_VPH.Remove(rowFiltr_VPH);
                this.tscbFiltry_VPH.ComboBox.DataSource = null;
                this.tscbFiltry_VPH.ComboBox.DataSource = filtry_VPH;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        public void PerformPridatFiltr_VPH()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_VPH_Filtr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + _filtrName_VPH;
                this.dgVPH.SaveConfiguration(nameFile);

                bool result = CreateFilter_VPH(ref filtr);
                filtr.NameFileDataGridView = nameFile;

                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry_VPH.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry_VPH.ComboBox.DataSource = null;
                    this.tscbFiltry_VPH.ComboBox.DataSource = filtry_VPH;
                    this.tscbFiltry_VPH.SelectedItem = filtr;
                    tscbFiltry_VPH.ComboBox.DropDownWitdhAutosize();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se uložit data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        public void PerformZmenitFiltr_VPH()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_VPH == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_VPH.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                var filtr = rowFiltr_VPH;

                string nameFile = string.Empty;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                {
                    nameFile = Guid.NewGuid().ToString() + "_" + _filtrName_VPH;
                }
                else
                    nameFile = filtr.NameFileDataGridView;

                this.dgVPH.SaveConfiguration(nameFile);

                bool result = CreateFilter_VPH(ref filtr);
                filtr.NameFileDataGridView = nameFile;

                if (!result)
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyčistí filtr
        /// </summary>
        public void PerformVycistitFiltr_VPH()
        {
            try
            {
                    cb_Active.Text =
                    string.Empty;

                tb_VyrZak.Text = string.Empty;

                try
                {

                    cb_Active.SelectedIndex = -1;
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nastavit Filter
        /// </summary>
        public void PerformNastavitFiltr_VPH(Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr_VPH();

                if (filtr.Active.HasValue)
                {
                    switch (filtr.Active.Value)
                    {
                        case 1:
                                cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni;
                                break;
                        case 0:
                                cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni;
                                break;
                        case 200:
                                cb_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen;
                                break;
                        default:
                            cb_Active.SelectedIndex = -1;
                            break;
                    }
                }


                tb_VyrZak.Text = filtr.SOPNUMBE ;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                    this.dgVPH.LoadConfiguration(_filtrName_VPH);
                else
                    this.dgVPH.LoadConfiguration(filtr.NameFileDataGridView);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Filtry VPP

        /// <summary>
        /// Vytvořeni filtru pro dotazeni dat
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter_VPP(ref Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Vyroba_VPP_Filtr();

            filtr.OrderBy = GetSearch_OrderBy();

            return true;
        }


        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        public void PerformOdebratFiltr_VPP()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_VPP == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_VPP.NazevFiltru) ? string.Empty : rowFiltr_VPP.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_VPP.Remove(rowFiltr_VPP);
                this.tscbFiltry_VPP.ComboBox.DataSource = null;
                this.tscbFiltry_VPP.ComboBox.DataSource = filtry_VPP;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        public void PerformPridatFiltr_VPP()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_VPP_Filtr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + _filtrName_VPP;
                this.dgVPP.SaveConfiguration(nameFile);

                bool result = CreateFilter_VPP(ref filtr);
                filtr.NameFileDataGridView = nameFile;

                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry_VPP.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry_VPP.ComboBox.DataSource = null;
                    this.tscbFiltry_VPP.ComboBox.DataSource = filtry_VPP;
                    this.tscbFiltry_VPP.SelectedItem = filtr;
                    tscbFiltry_VPP.ComboBox.DropDownWitdhAutosize();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se uložit data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        public void PerformZmenitFiltr_VPP()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_VPP == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_VPP.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                var filtr = rowFiltr_VPP;

                string nameFile = string.Empty;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                {
                    nameFile = Guid.NewGuid().ToString() + "_" + _filtrName_VPP;
                }
                else
                    nameFile = filtr.NameFileDataGridView;

                this.dgVPP.SaveConfiguration(nameFile);

                bool result = CreateFilter_VPP(ref filtr);
                filtr.NameFileDataGridView = nameFile;

                if (!result)
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyčistí filtr
        /// </summary>
        public void PerformVycistitFiltr_VPP()
        {
            try
            {
                cb_OrderBy_VPP.Text =
                string.Empty;


                try
                {

                    cb_OrderBy_VPP.SelectedIndex = -1;
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nastavit Filter
        /// </summary>
        public void PerformNastavitFiltr_VPP(Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr_VPP();

                SetSearch_OrderBy(filtr.OrderBy);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region BackGround Worker

        private void bw_VPH_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr = (Fask.Interfaces.Filtry.Vyroba_VPH_Filtr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba dset = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_VPH.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)
                    dset = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)providerVPH).GetFiltrovanyVPHList(filtr);
                else
                    throw new Exception("IVPH_GetFiltrovanyVPHList not implementet");

                if (bw_VPH.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = dset;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    this.ds = new Fask.Interfaces.DataSets.Vyroba();
                    bsVPH.DataSource = this.ds;
                    Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_VPH(-1);

                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.ds = new Fask.Interfaces.DataSets.Vyroba();
                    bsVPH.DataSource = this.ds;

                    SetStatusLabelText_VPH(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.ds = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (this.ds == null)
                        this.ds = new Fask.Interfaces.DataSets.Vyroba();

                    bsVPH.DataSource = this.ds;

                    if (ds.CZPRO_VPH.Count == 0)
                        SetStatusLabelText_VPH(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dgVPH.SelectedRows)
                        {
                            SetStatusLabelText_VPH(row.Index);
                        }
                    }

                    if (SelectedVPHRow != null)
                        PerformVyhledat_VPP(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
                    else
                        ClearVPP();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorVyrobekStop_VPH();
            }
        }

        private void bw_VPP_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr = (Fask.Interfaces.Filtry.Vyroba_VPP_Filtr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba dset = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_VPP.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList)
                    dset = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList)providerVPP).GetFiltrovanyVPPList(filtr);
                else
                    throw new Exception("IVPP_GetFiltrovanyVPPList not implementet");


                if (bw_VPP.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = dset;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_VPP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    //this.ds = new Fask.Interfaces.DataSets.Vyroba();
                    this.ds.CZPRO_VPP.Clear();
                    bsVPP.DataSource = this.ds;
                    Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_VPP(-1);

                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    //this.ds = new Fask.Interfaces.DataSets.Vyroba();
                    this.ds.CZPRO_VPP.Clear();
                    bsVPP.DataSource = this.ds;

                    SetStatusLabelText_VPP(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    var dset = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (dset != null)
                    {
                        this.ds.CZPRO_VPP.Clear();
                        foreach (var item in dset.CZPRO_VPP)
                        {
                            this.ds.CZPRO_VPP.ImportRow(item);
                        }
                        this.ds.CZPRO_VPP.AcceptChanges();
                    }

                    bsVPP.DataSource = this.ds;

                    if (ds.CZPRO_VPP.Count == 0)
                        SetStatusLabelText_VPP(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dgVPP.SelectedRows)
                        {
                            SetStatusLabelText_VPP(row.Index);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(_ClassName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorVyrobekStop_VPP();
                //buttonOdznacitVse_VPP_Click(null,null);
            }
        }

        #endregion

        #region Progres

        private void ProgressIndicatorVyrobekStop_VPH()
        {
            progressIndicatorVyrobek_VPH.Stop();
            progressIndicatorVyrobek_VPH.Visible = false;
        }

        private void ProgressIndicatorVyrobekStart_VPH()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorVyrobek_VPH.Location = new Point(this.dgVPH.Location.X + (this.dgVPH.Width / 2) - (progressIndicatorVyrobek_VPH.Size.Width / 2), this.dgVPH.Location.Y + (this.dgVPH.Height / 2) - (progressIndicatorVyrobek_VPH.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobek_VPH.Start();
            progressIndicatorVyrobek_VPH.Visible = true;
        }

        private void ProgressIndicatorVyrobekStop_VPP()
        {
            progressIndicatorVyrobek_VPP.Stop();
            progressIndicatorVyrobek_VPP.Visible = false;
        }

        private void ProgressIndicatorVyrobekStart_VPP()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorVyrobek_VPP.Location = new Point(this.dgVPP.Location.X + (this.dgVPP.Width / 2) - (progressIndicatorVyrobek_VPP.Size.Width / 2), this.dgVPP.Location.Y + (this.dgVPP.Height / 2) - (progressIndicatorVyrobek_VPP.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobek_VPP.Start();
            progressIndicatorVyrobek_VPP.Visible = true;
        }

        #endregion

        #region County

        private void SetStatusLabelText_VPH(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStatusLabelText_VPH(index);
                }));

                return;
            }

            tsl_VPH.Text = string.Format("{0}/{1}", index + 1, ds.CZPRO_VPH.Count);
        }

        private void NastavDatagrid()
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgVPP.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgVPP.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgVPP.DataSource is BindingSource bindingSource)
                    {
                        // Pokud je datový zdroj BindingSource
                        if (bindingSource.DataSource is DataTable dataSourceTable)
                        {
                            if (dataSourceTable.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataSourceTable.Columns[column.DataPropertyName].DataType;
                            }
                        }
                        else if (bindingSource.DataSource is DataSet dataSet)
                        {
                            // Pokud je datový zdroj DataSet
                            DataTable dataTable2 = dataSet.Tables[bindingSource.DataMember];
                            if (dataTable2.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataTable2.Columns[column.DataPropertyName].DataType;
                            }
                        }
                    }

                    //nastaveni poctu desetinnych mist pokud je sloupec typu Decimal
                    if (dataType.Name == "Decimal")
                    {
                        //column.DefaultCellStyle.Format = "N2";
                        if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist))
                        {
                            column.DefaultCellStyle.Format = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist;

                        }
                        else
                        {
                            column.DefaultCellStyle.Format = "N5";
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show("Chyba při nastavení desetinnych míst.");
            }
            #endregion
        }

        private void dgVPH_SelectionChanged(object sender, EventArgs e)
        {
            NastavDatagrid();

            foreach (DataGridViewRow row in dgVPH.SelectedRows)
            {
                SetStatusLabelText_VPH(row.Index);
            }

            if(SelectedVPHRow != null)
                PerformVyhledat_VPP(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
            else
                ClearVPP();


            //buttonOdznacitVse_VPP_Click(null, null);
        }


        private void SetStatusLabelText_VPP(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStatusLabelText_VPP(index);
                }));

                return;
            }

            tsl_VPP.Text = string.Format("{0}/{1}", index + 1, ds.CZPRO_VPP.Count);
        }

        private void dgVPP_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgVPP.SelectedRows)
            {
                SetStatusLabelText_VPP(row.Index);
            }
        }
        #endregion


        private void tsmi_Tisky_Click(object sender, EventArgs e)
        {
            PerformPrint();
            //PerformPrint_QR();
        }




        #region MaR 27.8. 2024 Tisk paletovy stitek
        //private void tsmiTisk_Click(object sender, EventArgs e)
        //{
        //    Perform_Tisk();
        //}
        //#region rozpracovano
        ////    private string PrepareDataToTisk(
        ////string strPath,
        ////Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row,
        ////int? mnozstviDoTisku
        ////)
        ////    {
        ////        try
        ////        {
        ////            string strData = string.Empty;
        ////            using (System.IO.StreamReader sr = new System.IO.StreamReader(strPath))
        ////            {
        ////                strData = sr.ReadToEnd();
        ////                sr.Close();
        ////            }

        ////            StringBuilder sbData = new StringBuilder();
        ////            sbData.Append(strData);

        ////            string GS1_KOD_1_1D = string.Empty;
        ////            string GS1_KOD_1_TX = string.Empty;
        ////            string GS1_KOD_2_1D = string.Empty;
        ////            string GS1_KOD_2_TX = string.Empty;
        ////            string SSCC = string.Empty;
        ////            string SSCC_bez_nul = string.Empty;
        ////            string WEIGHT = string.Empty;

        ////            string BarcodeP = string.Empty;
        ////            string Expiration = string.Empty;
        ////            string Serltnum = string.Empty;
        ////            string ExpirationRRMMDD = string.Empty;



        ////            string Expiration_YYYY_MM_DD = string.Empty;








        ////            if (row != null)
        ////            {


        ////                //TODO MaR 13.5.2024 doplneno pro PaV
        ////                if (!row.IsBarcodePNull())
        ////                {
        ////                    BarcodeP = row.BarcodeP;
        ////                }
        ////                else
        ////                {
        ////                    BarcodeP = " ";
        ////                }

        ////                if (!row.IsEXPIRATIONNull())
        ////                {
        ////                    Expiration = row.EXPIRATION;

        ////                    ExpirationRRMMDD = row.EXPIRATION;

        ////                    // Expiration_YYYY_MM_DD = row.EXPIRATION.Value.ToString("yyyy-MM-dd");


        ////                    string input = row.EXPIRATION;  // Tvoje vstupní data
        ////                    DateTime expirationDate = DateTime.ParseExact(input, "yyMMdd", null);
        ////                    Expiration_YYYY_MM_DD = expirationDate.ToString("yyyy-MM-dd");
        ////                    //DateTime expirationDate = DateTime.Parse(row.EXPIRATION);
        ////                    //Expiration_YYYY_MM_DD = expirationDate.ToString("yyyy-MM-dd");
        ////                }
        ////                else
        ////                {
        ////                    Expiration = " ";
        ////                    ExpirationRRMMDD = " ";

        ////                    Expiration_YYYY_MM_DD = " ";
        ////                }

        ////                if (!row.IsSERLTNUMNull())
        ////                {
        ////                    Serltnum = row.SERLTNUM;
        ////                }
        ////                else
        ////                {
        ////                    Serltnum = " ";
        ////                }

        ////                sbData.Replace("$BarcodeP$", BarcodeP);
        ////                sbData.Replace("$Expiration$", Expiration);
        ////                sbData.Replace("$ExpirationRRMMDD$", ExpirationRRMMDD);
        ////                sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
        ////                sbData.Replace("$Serltnum$", Serltnum);
        ////                //----------------------------------------------------------

        ////                if (!row.IsWEIGHTNull())
        ////                {
        ////                    WEIGHT = row.WEIGHT.ToString();
        ////                }

        ////                sbData.Replace("$WEIGHT$", WEIGHT);


        ////                if (!row.IsNMBRPALNull())
        ////                {
        ////                    SSCC = row.NMBRPAL;
        ////                    SSCC_bez_nul = SSCC.Substring(2);
        ////                }

        ////                sbData.Replace("$SSCC$", SSCC_bez_nul);

        ////                GS1_KOD_1_1D = "02" + row.BarcodeP.PadLeft(14, '0') + "37" + row.qty.ToString("0000") + "";
        ////                GS1_KOD_1_TX = "(02)" + row.BarcodeP.PadLeft(14, '0') + "(37)" + row.qty.ToString("0000") + ""; //(02) (37)
        ////                GS1_KOD_2_1D = SSCC; //SSCC neni v production
        ////                GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
        ////            }

        ////            sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
        ////            sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
        ////            sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
        ////            sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX);

        ////            sbData.Replace("$SOURCE$", "Konzola");

        ////            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

        ////            foreach (DataColumn dcol in dt.Columns)
        ////            {
        ////                string key = dcol.ColumnName;
        ////                string value = row[dcol.ColumnName].ToString();

        ////                try
        ////                {
        ////                    sbData.Replace("$" + key + "$", value.Trim());
        ////                }
        ////                catch
        ////                {
        ////                }
        ////            }

        ////            return sbData.ToString();
        ////        }
        ////        catch (System.Exception ex)
        ////        {
        ////            Fask.Logging.ExceptionHandler2.Handle(ex);
        ////            return null;
        ////        }
        ////    } 
        //#endregion

        //private void Perform_Tisk()
        //{
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        if (SelectedVPHRow == null || SelectedVPPRow == null)
        //        {
        //            MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
        //            return;
        //        }

        //        if (CZPRO_VPH_selectedRows.Count > 1 || CZPRO_VPP_selectedRows.Count > 1)
        //        {
        //            MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
        //            return;
        //        }

        //        #region MaR 6.11.2024 vypis zpl do souboru

        //        if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
        //        {

        //            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyrobni prikazy zpl-------------------------------------");

        //            foreach (DataColumn column in SelectedVPHRow.Table.Columns)
        //            {
        //                //string columnName = column.ColumnName;
        //                //object columnValue = SelectedVPHRow[columnName];
        //                //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");
        //                string columnName = string.Empty;
        //                object columnValue = string.Empty;
        //                //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");
        //                try
        //                {
        //                    columnName = column.ColumnName;
        //                    columnValue = SelectedVPHRow[columnName];
        //                }
        //                catch (Exception ex)
        //                {

        //                    columnName = column.ColumnName;
        //                    columnValue = string.Empty;
        //                }
        //                //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo,$"Název sloupce: {columnName}, Hodnota: {columnValue}");
        //                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{columnName}: {columnValue}");
        //            }

        //            foreach (DataColumn column in SelectedVPPRow.Table.Columns)
        //            {
        //                //string columnName = column.ColumnName;
        //                //object columnValue = SelectedVPPRow[columnName];
        //                string columnName = string.Empty;
        //                object columnValue = string.Empty;
        //                //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");
        //                try
        //                {
        //                    columnName = column.ColumnName;
        //                    columnValue = SelectedVPPRow[columnName];
        //                }
        //                catch (Exception ex)
        //                {

        //                    columnName = column.ColumnName;
        //                    columnValue = string.Empty;
        //                }
        //                //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");

        //                //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo,$"Název sloupce: {columnName}, Hodnota: {columnValue}");
        //                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{columnName}: {columnValue}");
        //            }

        //            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyrobni prikazy TISK zpl-------------------------------------");


        //        }
        //        #endregion

        //        if (providerTisk is Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)
        //        {

        //            #region rozpracovano
        //            var PrinerName = prepareTiskParams();

        //            if (PrinerName == null)
        //                return;

        //            int? MN_ToTisk = TiskMnozstvi(true);

        //            Dictionary<string, string> dict = PrepareDataToTisk(SelectedVPPRow);

        //            if (MN_ToTisk.HasValue)
        //            {
        //                ((Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)providerTisk).EtiketaTisk(
        //                   (int)Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID,
        //                   Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku,
        //                   PrinerName,
        //                   dict,
        //                   MN_ToTisk.Value);
        //            }
        //            else
        //                MessageBox.Show(this, "Chyba pri zadani množství", "Error", MessageBoxButtons.OK);




        //            MessageBox.Show(this, "Tisk proběhl uspešně.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            #endregion

        //        }
        //        else
        //        {
        //            //int? MN_ToTisk = TiskMnozstvi(true);
        //            //string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku);



        //            //zaslat cestu na tiskarnu old
        //            int? MN_ToTisk = TiskMnozstvi(true);
        //            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku);



        //            #region MaR 27.8.2024 vybrani tiskove ZPL
        //            //formular na vybrani tiskove ulohy
        //            DataGridViewRow dataGridView = new DataGridViewRow();


        //            //string klic = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Klic;
        //            string klic = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL;

        //            FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klic, false);

        //            string path = string.Empty;
        //            DialogResult result = form.ShowDialog();
        //            // Zobrazení formuláře
        //            if (result == DialogResult.OK)
        //            {
        //                //dataGridView = (DataGridViewRow)form.Tag;
        //                path = form.ResultString;
        //                // uživatel stiskl OK
        //            }
        //            else if (result == DialogResult.Cancel)
        //            {
        //                // uživatel stiskl Cancel
        //                MessageBox.Show("Není vybraná tisková šablona");
        //                return;
        //            }

        //            //plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrintTemplates", dataGridView.Cells["Název tiskové šablony"].Value.ToString());


        //            string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

        //            #endregion

        //            //string data = PrepareDataToTisk(strPath, rowProduct, MN_ToTisk);




        //            string data = string.Empty;

        //            data = PrepareDataToTisk(plr_Path, SelectedVPHRow, SelectedVPPRow, MN_ToTisk);



        //            Tisk(data, true);




        //            #region MaR zalohovani tisk stitky 21.8. 2024 - rozpracovano
        //            //string JSON = data;
        //            //Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
        //            //if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
        //            //{
        //            //    //SaveToFile.Save(JSON_Logs.URL, "EtiketaTisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt, true);

        //            //    SaveToFile.Save("PrintLogDirectory", "EtiketaTisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt,true);
        //            //}
        //            #endregion


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        //private TiskParams prepareTiskParams()
        //{

        //    if (Konfigurace_Tisky.Globals_Konfig_Tisk.Konfigurace.Params.Count == 1)
        //    {
        //        TiskParams tiskParams = new TiskParams();
        //        tiskParams.CONFIG_NAME = Konfigurace_Tisky.Globals_Konfig_Tisk.Konfigurace.Params[0].PrinterName; // to je vse ???
        //        return tiskParams;
        //    }
        //    else
        //    {
        //        MessageBox.Show(this, "Pro možnost volby z videro tiskaren je potřeba doimplementovat funkčnost!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return null;
        //    }
        //}

        //private int? TiskMnozstvi(bool MnozstvuAutoJedna)
        //{
        //    try
        //    {
        //        string pocetStr = string.Empty;
        //        int pocetInt = 1;

        //        if (!MnozstvuAutoJedna)
        //        {
        //            while (true)
        //            {
        //                DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
        //                if (drPocet == System.Windows.Forms.DialogResult.Cancel)
        //                    return null;

        //                try
        //                {
        //                    pocetInt = int.Parse(pocetStr);
        //                }
        //                catch (Exception exPocet)
        //                {
        //                    MessageBox.Show(exPocet.Message);
        //                    continue;
        //                }

        //                break; // vse ok ... 
        //            }
        //        }

        //        return pocetInt;

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //}

        //private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow CZPRO_VPPRow)
        //{
        //    try
        //    {

        //        while (true)
        //        {
        //            try
        //            {
        //                Dictionary<string, string> data = new Dictionary<string, string>();

        //                //skladani caroveho kodu GS1 --START---------------------
        //                //zadat key
        //                //zadat value

        //                string GS1_KOD_1_1D = string.Empty;
        //                string GS1_KOD_1_TX = string.Empty;
        //                string GS1_KOD_2_1D = string.Empty;
        //                string GS1_KOD_2_TX = string.Empty;
        //                string SSCC = string.Empty;
        //                string SSCC_bez_nul = string.Empty;
        //                string WEIGHT = string.Empty;

        //                string BarcodeP = string.Empty;
        //                string Expiration = string.Empty;
        //                string Serltnum = string.Empty;
        //                string ExpirationRRMMDD = string.Empty;


        //                string Expiration_YYYY_MM_DD = string.Empty;






        //                if (CZPRO_VPPRow != null)
        //                {

        //                    //TODO MaR 13.5.2024 doplneno pro PaV
        //                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.BarcodeP))
        //                    {
        //                        BarcodeP = CZPRO_VPPRow.BarcodeP;
        //                    }
        //                    else
        //                    {
        //                        BarcodeP = " ";
        //                    }

        //                    DateTime expirace = DateTime.Now;

        //                    //if (!productionRow_data.IsEXPIRATIONNull())
        //                    //{
        //                    Expiration = expirace.ToString("yyyy-MM-dd");

        //                    ExpirationRRMMDD = expirace.ToString("yy-MM-dd");


        //                    // string input = productionRow_data.EXPIRATION;  // Tvoje vstupní data
        //                    // DateTime expirationDate = DateTime.ParseExact(input, "yyMMdd", null);
        //                    Expiration_YYYY_MM_DD = expirace.ToString("yyyy-MM-dd");

        //                    //Expiration_YYYY_MM_DD = productionRow_data.EXPIRATION;
        //                    //}
        //                    //else
        //                    //{
        //                    //    Expiration = " ";
        //                    //    ExpirationRRMMDD = " ";
        //                    //    Expiration_YYYY_MM_DD = " ";
        //                    //}

        //                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.VNDITNUM))
        //                    {
        //                        Serltnum = CZPRO_VPPRow.VNDITNUM;
        //                    }
        //                    else
        //                    {
        //                        Serltnum = " ";
        //                    }


        //                    //------------START-DATA----------------
        //                    //dotahovat data SSCC a WEIGHT

        //                    //if (!CZPRO_VPPRow.WEIGHT_NETTO.val)
        //                    //{
        //                    //    WEIGHT = productionRow_data.WEIGHT.ToString();
        //                    //}

        //                    if (CZPRO_VPPRow["WEIGHT_NETTO"] != DBNull.Value)
        //                    {
        //                        WEIGHT = CZPRO_VPPRow.WEIGHT_NETTO.ToString();
        //                    }
        //                    else
        //                    {
        //                        WEIGHT = string.Empty; // Nebo jakákoli jiná výchozí hodnota, kterou potřebuješ.
        //                    }


        //                    if (!data.ContainsKey("WEIGHT"))
        //                        data.Add("WEIGHT", WEIGHT);

        //                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.ITEMNMBR))
        //                    {
        //                        //SSCC a WEIGHT doplnit logiku dohledani
        //                        //SSCC a WEIGHT natvrdo zadano
        //                        SSCC = CZPRO_VPPRow.ITEMNMBR;
        //                        SSCC_bez_nul = SSCC.Substring(2);
        //                    }

        //                    if (!data.ContainsKey("SSCC"))
        //                        data.Add("SSCC", SSCC_bez_nul);

        //                    //------------END-DATA----------------

        //                    GS1_KOD_1_1D = "02" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "37" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + "";
        //                    GS1_KOD_1_TX = "(02)" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "(37)" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + ""; //(02) (37)
        //                    GS1_KOD_2_1D = SSCC; //SSCC neni v production
        //                    GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
        //                }


        //                if (!data.ContainsKey("GS1_KOD_1_1D"))
        //                    data.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

        //                if (!data.ContainsKey("GS1_KOD_1_TX"))
        //                    data.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

        //                if (!data.ContainsKey("GS1_KOD_2_1D"))
        //                    data.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

        //                if (!data.ContainsKey("GS1_KOD_2_TX"))
        //                    data.Add("GS1_KOD_2_TX", GS1_KOD_2_TX);

        //                //skladani caroveho kodu GS1 --END---------------------

        //                data.Add("SOURCE", "Konzola");

        //                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();


        //                foreach (DataColumn dcol in dt.Columns)
        //                {
        //                    string key = dcol.ColumnName;
        //                    string value = CZPRO_VPPRow[dcol.ColumnName].ToString();
        //                    if (!data.ContainsKey(key))
        //                        data.Add(key, value);
        //                }


        //                return data;
        //            }
        //            catch (Exception ex)
        //            {
        //                Fask.Logging.ExceptionHandler2.Handle(ex);
        //                return null;
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


        //private string PrepareDataToTisk(
        //       string strPath,
        //       Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow CZPRO_VPHRow,
        //       Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow CZPRO_VPPRow,
        //       int? mnozstviDoTisku
        //       )
        //{
        //    try
        //    {
        //        string strData = string.Empty;
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(strPath))
        //        {
        //            strData = sr.ReadToEnd();
        //            sr.Close();
        //        }

        //        StringBuilder sbData = new StringBuilder();
        //        sbData.Append(strData);

        //        string GS1_KOD_1_1D = string.Empty;
        //        string GS1_KOD_1_TX = string.Empty;
        //        string GS1_KOD_2_1D = string.Empty;
        //        string GS1_KOD_2_TX = string.Empty;
        //        string SSCC = string.Empty;
        //        string SSCC_bez_nul = string.Empty;
        //        string WEIGHT = string.Empty;

        //        string BarcodeP = string.Empty;
        //        string Expiration = string.Empty;
        //        string Serltnum = string.Empty;
        //        string ExpirationRRMMDD = string.Empty;
        //        string Expiration_YYYY_MM_DD = string.Empty;

        //        string datumexpirace01 = string.Empty;
        //        string sn = string.Empty;



        //        #region MaR 28.8.2024, prepsani promennych na vyrobni prikazy old
        //        if (CZPRO_VPHRow != null && CZPRO_VPPRow != null)
        //        {
        //            if (!string.IsNullOrEmpty(CZPRO_VPPRow.BarcodeP))
        //            {
        //                BarcodeP = CZPRO_VPPRow.BarcodeP;
        //            }
        //            else
        //            {
        //                BarcodeP = " ";
        //            }




        //            DateTime? EXPIRACE = DateTime.Now;
        //            //EXPIRACE = DateTime.Now;

        //            if (EXPIRACE != null && EXPIRACE.HasValue)
        //            {

        //                // Expiration = EXPIRACE.Value.ToString();

        //                Expiration = EXPIRACE.Value.ToString("yyMMdd");


        //                ExpirationRRMMDD = EXPIRACE.Value.ToString("yy-MM-dd");

        //                // Expiration_YYYY_MM_DD = row.EXPIRATION.Value.ToString("yyyy-MM-dd");


        //                //string input = EXPIRACE;  // Tvoje vstupní data
        //                //DateTime expirationDate = DateTime.ParseExact(input, "yyMMdd", null);
        //                Expiration_YYYY_MM_DD = EXPIRACE.Value.ToString("yyyy-MM-dd");
        //                //DateTime expirationDate = DateTime.Parse(row.EXPIRATION);
        //                //Expiration_YYYY_MM_DD = expirationDate.ToString("yyyy-MM-dd");

        //                datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
        //                sn = EXPIRACE.Value.ToString("yy");

        //            }
        //            else
        //            {
        //                Expiration = " ";
        //                ExpirationRRMMDD = " ";

        //                Expiration_YYYY_MM_DD = " ";
        //            }

        //            #region spatne promenne, opravit!

        //            //CZPRO_VPPRow.ser

        //            //if (!row.IsSERLTNUMNull())
        //            //{
        //            //    Serltnum = row.SERLTNUM;
        //            //}
        //            //else
        //            //{
        //            //    Serltnum = " ";
        //            //}

        //            if (!string.IsNullOrEmpty(CZPRO_VPPRow.VNDITNUM))
        //            {
        //                Serltnum = CZPRO_VPPRow.VNDITNUM;
        //            }
        //            else
        //            {
        //                Serltnum = " ";
        //            }

        //            //if (!row.IsWEIGHTNull())
        //            //{
        //            //    WEIGHT = row.WEIGHT.ToString();
        //            //}


        //            if (CZPRO_VPPRow["WEIGHT_NETTO"] != DBNull.Value)
        //            {
        //                WEIGHT = CZPRO_VPPRow.WEIGHT_NETTO.ToString();
        //            }
        //            else
        //            {
        //                WEIGHT = string.Empty; // Nebo jakákoli jiná výchozí hodnota, kterou potřebuješ.
        //            }

        //            #endregion






        //            sbData.Replace("$BarcodeP$", BarcodeP);
        //            //sbData.Replace("$Expiration$", Expiration);
        //            sbData.Replace("$EXPIRATION$", Expiration);
        //            sbData.Replace("$ExpirationYYMMDD$", ExpirationRRMMDD);
        //            sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
        //            //sbData.Replace("$Serltnum$", Serltnum);
        //            sbData.Replace("$SERLTNUM$", Serltnum);
        //            //----------------------------------------------------------







        //            sbData.Replace("$WEIGHT$", WEIGHT);


        //            if (!string.IsNullOrEmpty(CZPRO_VPPRow.ITEMNMBR))
        //            {
        //                SSCC = CZPRO_VPPRow.ITEMNMBR;
        //                SSCC_bez_nul = SSCC.Substring(2);
        //            }

        //            //if (!row.IsNMBRPALNull())
        //            //{
        //            //    SSCC = row.NMBRPAL;
        //            //    SSCC_bez_nul = SSCC.Substring(2);
        //            //}

        //            sbData.Replace("$SSCC$", SSCC_bez_nul);

        //            GS1_KOD_1_1D = "02" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "37" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + "";
        //            GS1_KOD_1_TX = "(02)" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "(37)" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + ""; //(02) (37)
        //            GS1_KOD_2_1D = SSCC; //SSCC neni v production
        //            GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
        //        }

        //        sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
        //        sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
        //        sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
        //        sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX);

        //        sbData.Replace("$SOURCE$", "Konzola");

        //        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

        //        foreach (DataColumn dcol in dt.Columns)
        //        {
        //            string key = dcol.ColumnName;
        //            string value = CZPRO_VPPRow[dcol.ColumnName].ToString();

        //            try
        //            {
        //                sbData.Replace("$" + key + "$", value.Trim());
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        #endregion


        //        #region MaR 28.8.2024, prepsani promennych na vyrobni prikazy old
        //        //if (CZPRO_VPHRow != null && CZPRO_VPPRow != null)
        //        //{


        //        //    //TODO MaR 13.5.2024 doplneno pro PaV
        //        //    if (!row.IsBarcodePNull())
        //        //    {
        //        //        BarcodeP = row.BarcodeP;
        //        //    }
        //        //    else
        //        //    {
        //        //        BarcodeP = " ";
        //        //    }

        //        //    if (!row.IsEXPIRATIONNull())
        //        //    {
        //        //        Expiration = row.EXPIRATION;

        //        //        ExpirationRRMMDD = row.EXPIRATION;

        //        //        // Expiration_YYYY_MM_DD = row.EXPIRATION.Value.ToString("yyyy-MM-dd");


        //        //        string input = row.EXPIRATION;  // Tvoje vstupní data
        //        //        DateTime expirationDate = DateTime.ParseExact(input, "yyMMdd", null);
        //        //        Expiration_YYYY_MM_DD = expirationDate.ToString("yyyy-MM-dd");
        //        //        //DateTime expirationDate = DateTime.Parse(row.EXPIRATION);
        //        //        //Expiration_YYYY_MM_DD = expirationDate.ToString("yyyy-MM-dd");
        //        //    }
        //        //    else
        //        //    {
        //        //        Expiration = " ";
        //        //        ExpirationRRMMDD = " ";

        //        //        Expiration_YYYY_MM_DD = " ";
        //        //    }

        //        //    if (!row.IsSERLTNUMNull())
        //        //    {
        //        //        Serltnum = row.SERLTNUM;
        //        //    }
        //        //    else
        //        //    {
        //        //        Serltnum = " ";
        //        //    }

        //        //    sbData.Replace("$BarcodeP$", BarcodeP);
        //        //    sbData.Replace("$Expiration$", Expiration);
        //        //    sbData.Replace("$ExpirationRRMMDD$", ExpirationRRMMDD);
        //        //    sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
        //        //    sbData.Replace("$Serltnum$", Serltnum);
        //        //    //----------------------------------------------------------

        //        //    if (!row.IsWEIGHTNull())
        //        //    {
        //        //        WEIGHT = row.WEIGHT.ToString();
        //        //    }

        //        //    sbData.Replace("$WEIGHT$", WEIGHT);


        //        //    if (!row.IsNMBRPALNull())
        //        //    {
        //        //        SSCC = row.NMBRPAL;
        //        //        SSCC_bez_nul = SSCC.Substring(2);
        //        //    }

        //        //    sbData.Replace("$SSCC$", SSCC_bez_nul);

        //        //    GS1_KOD_1_1D = "02" + row.BarcodeP.PadLeft(14, '0') + "37" + row.qty.ToString("0000") + "";
        //        //    GS1_KOD_1_TX = "(02)" + row.BarcodeP.PadLeft(14, '0') + "(37)" + row.qty.ToString("0000") + ""; //(02) (37)
        //        //    GS1_KOD_2_1D = SSCC; //SSCC neni v production
        //        //    GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
        //        //}

        //        //sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
        //        //sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
        //        //sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
        //        //sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX);

        //        //sbData.Replace("$SOURCE$", "Konzola");

        //        //Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

        //        //foreach (DataColumn dcol in dt.Columns)
        //        //{
        //        //    string key = dcol.ColumnName;
        //        //    string value = row[dcol.ColumnName].ToString();

        //        //    try
        //        //    {
        //        //        sbData.Replace("$" + key + "$", value.Trim());
        //        //    }
        //        //    catch
        //        //    {
        //        //    }
        //        //} 
        //        #endregion

        //        return sbData.ToString();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //}
    
        
        
        //private void Tisk(string data, bool MnozstvuAutoJedna)
        //{
        //    try
        //    {




        //        PrintDialog printDialog1 = new PrintDialog();
        //        printDialog1.UseEXDialog = true;

        //        if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
        //            return;


        //        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, data))
        //        {
        //            throw new Exception(string.Format("Tisk etikety '{0}' se nezdařil", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku));
        //        }
        //        else
        //        {
        //            #region MaR zalohovani tisk stitky 21.8. 2024 - rozpracovano
        //            string JSON = data;
        //            Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
        //            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Ukladat_Stitky_Do_Souboru || Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].EtiketaTISKUkladat)
        //            {
        //                //SaveToFile.Save(JSON_Logs.URL, "EtiketaTisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt, true);

        //                Save_Etikety("PrintLogDirectory", "Stitek_Etiketa_Tisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
        //            }
        //            #endregion

        //            #region old
        //            //string JSON = data;
        //            //Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
        //            //if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Ukladat_Stitky_Do_Souboru || Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].EtiketaTISKUkladat)
        //            //{
        //            //    //SaveToFile.Save(JSON_Logs.URL, "EtiketaTisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt, true);

        //            //    SaveToFile.Save_Etikety("PrintLogDirectory", "EtiketaTisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt, true);
        //            //}
        //            #endregion

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        //public static void Save_Etikety(string SubPath, string Zdroj, string Obsah, Guid G, string Pripona)
        //{

        //    string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Zdroj + "_" + G.ToString() + Pripona;
        //    if (string.IsNullOrEmpty(Obsah))
        //    {
        //        string c = Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path, "ETIKETY" + @"\" + FileName);
        //        string msg = string.Format("Halo tady je prazdny soubor, proč?" + Environment.NewLine +
        //            "SubPath: {0}" + Environment.NewLine +
        //            "Zdroj: {1}" + Environment.NewLine +
        //            "G: {2}" + Environment.NewLine +
        //            "Pripona: {3}" + Environment.NewLine,
        //            SubPath,
        //            Zdroj,
        //            G,
        //            Pripona
        //            );
        //        ExceptionHandler2.Handle("", c);
        //    }


        //    string cesta = Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path, SubPath + @"\" + FileName);
        //    ExceptionHandler2.Handle(Obsah, cesta);

        //}


        #endregion




        #region MaR 14.11.2024 Tisk ZPL a RDLC
        private void tiskEtiketToolStripMenuItem_Click(object sender, EventArgs e)
        {
             //Perform_Tisk();

            Perform_Tisk_Selected(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Vychozi_Tiskarna
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Pocet_Vytisku);

        }

        private void tiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformPrint(false
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);

            //Perform_Tisk_Selected(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
            //   , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
            //   , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);
        }

        private void PrimyTiskZPL()
        {
            //MaR 13.11.2024 zde bude primy tisk ZPL sablon s ord 0 a loginId
            //tisk
            Perform_Tisk_Selected_Primy_Tisk(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Vychozi_Tiskarna
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Pocet_Vytisku);




        }

        private void PrimyTiskRDLC()
        {
            //MaR 13.11.2024 zde bude primy tisk ZPL sablon s ord 0 a loginId
            //tisk
            PerformPrint(true
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);

            //Perform_Tisk_Selected_Primy_Tisk(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
            //    , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
            //    , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);

        }

        private void Perform_Tisk_Selected_Primy_Tisk(string klicTyp, string NazevVychoziTiskarny, string pocetVytisku)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZPRO_VPP_selectedVPPRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZPRO_VPP_selectedVPPRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start ciselniky/zasoby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

                        foreach (DataColumn column in item.Table.Columns)
                        {
                            string columnName = string.Empty;
                            object columnValue = string.Empty;
                            //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");
                            try
                            {
                                columnName = column.ColumnName;
                                columnValue = item[columnName];
                            }
                            catch (Exception ex)
                            {

                                columnName = column.ColumnName;
                                columnValue = string.Empty;
                            }
                            //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo,$"Název sloupce: {columnName}, Hodnota: {columnValue}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{columnName}: {columnValue}");
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END ciselniky/zasoby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


                    }

                    index_zaznamu++;
                }


                #endregion

                if (providerTisk is Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)
                {

                    var PrinerName = prepareTiskParams();

                    if (PrinerName == null)
                        return;

                    int? MN_ToTisk = TiskMnozstvi(true);



                    foreach (var item in CZPRO_VPP_selectedVPPRows)
                    {
                        Dictionary<string, string> dict = PrepareDataToTisk(item);

                        if (MN_ToTisk.HasValue)
                        {
                            ((Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)providerTisk).EtiketaTisk(
                               (int)Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID,
                               Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku,
                               PrinerName,
                               dict,
                               MN_ToTisk.Value);
                        }
                        else
                            MessageBox.Show(this, "Chyba pri zadani množství", "Error", MessageBoxButtons.OK);
                    }


                    MessageBox.Show(this, "Tisk proběhl uspešně.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //pocet vytisku do tiskarny
                    int MN_ToTisk = 1;

                    if (!string.IsNullOrEmpty(pocetVytisku))
                    {
                        MN_ToTisk = int.Parse(pocetVytisku);
                    }

                    //primy tisk cesta k sablone
                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);
                    string path_sablona = form.Prime_Tisky_Path();

                    if (string.IsNullOrEmpty(path_sablona))
                    {
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);

                    //odeslani dat do tiskarny
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, CZPRO_VPP_selectedVPPRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
        {
            try
            {

                #region selected rows

                foreach (var item in FASK_ZASOBY_selected_Rows)
                {
                    string data = PrepareDataToTisk(plr_Path, item, MN_ToTisk);


                    if (string.IsNullOrEmpty(data))
                    {
                        MessageBox.Show("Špatná šablona pro tisk!", "Cesta k šabloně", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    for (int i = 0; i < MN_ToTisk; i++)
                    {
                        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(nazevVychTiskarny, data))
                        {
                            throw new Exception(string.Format("Tisk etikety '{0}' se nezdařil", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku));
                        }
                    }


                }


                #endregion





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PerformPrint(bool primyTisk, string klicTyp, string nazevVychTiskarny, string pocetVytisku)
        {
            try
            {

                #region xxx
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZPRO_VPP_selectedVPPRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (SelectedVPHRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dgVPH.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (CZPRO_VPP_selectedVPPRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                // Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable)ds.CZPRO_VPP.Copy();

                #region dodatečné proměnné


                dt.Columns.Add("BIOMAG_00_BarcodeP_IMG", typeof(string));
                dt.Columns.Add("BIOMAG_00_VNDITNUM_IMG", typeof(string));

                dt.Columns.Add("BIOMAG_00_A_2D_DataMatrix_IMG", typeof(string));
                dt.Columns.Add("BIOMAG_00_A_2D_DataMatrix_kod", typeof(string));
                string BIOMAG_00_A_2D_kod = string.Empty;
                string BIOMAG_00_A_2D_kod_GS1 = string.Empty;

                string BIOMAG_00_SERLTNUM = string.Empty;

                string BIOMAG_00_VNDITNUM = string.Empty;
                DateTime? BIOMAG_00_EXPIRACE = null;

                string BIOMAG_00_datumexpirace01 = string.Empty;
                string BIOMAG_00_sn = string.Empty;
                dt.Columns.Add("BIOMAG_00_datumExpirace", typeof(string));
                dt.Columns.Add("BIOMAG_00_SN", typeof(string));

                dt.Columns.Add("BIOMAG_00_QTY_Paleta", typeof(decimal));
                dt.Columns.Add("BIOMAG_00_MJ_Paleta", typeof(string));


                foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow item in dt)
                {

                    if (!item.IsVNDITNUMNull() || !string.IsNullOrEmpty(item.VNDITNUM))
                    {
                        ZXing.BarcodeWriter BIOMAG_00_zw = new ZXing.BarcodeWriter();
                        BIOMAG_00_zw.Format = ZXing.BarcodeFormat.CODE_128;
                        BIOMAG_00_zw.Options.Height = 50; //50
                        BIOMAG_00_zw.Options.PureBarcode = true;
                        System.Drawing.Bitmap BIOMAG_00_image1 = BIOMAG_00_zw.Write(item.VNDITNUM);

                        System.IO.MemoryStream BIOMAG_00_ms = new System.IO.MemoryStream();
                        BIOMAG_00_image1.Save(BIOMAG_00_ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        byte[] BIOMAG_00_imgReportBarcode = BIOMAG_00_ms.ToArray();
                        BIOMAG_00_ms.Close();

                        string BIOMAG_00_Base64Imahe = Convert.ToBase64String(BIOMAG_00_imgReportBarcode);

                        item["BIOMAG_00_VNDITNUM_IMG"] = BIOMAG_00_Base64Imahe;


                        BIOMAG_00_EXPIRACE = DateTime.Now;
                        if (BIOMAG_00_EXPIRACE.HasValue)
                        {

                            BIOMAG_00_datumexpirace01 = BIOMAG_00_EXPIRACE.Value.ToString("yyyy-MM-dd");
                            BIOMAG_00_sn = BIOMAG_00_EXPIRACE.Value.ToString("yy");
                        }

                        //2D kod
                        BIOMAG_00_VNDITNUM = item.VNDITNUM;
                        if (BIOMAG_00_VNDITNUM.Length < 13)
                        {
                            BIOMAG_00_VNDITNUM = BIOMAG_00_VNDITNUM.PadLeft(13, '0');
                        }


                        if (BIOMAG_00_VNDITNUM.Length == 14)
                        {
                            string WithoutCheeckDigit = BIOMAG_00_VNDITNUM.Substring(0, 13);

                            if (BIOMAG_00_VNDITNUM == AddCheckDigit(WithoutCheeckDigit))
                            {
                                BIOMAG_00_A_2D_kod += "(01)" + AddCheckDigit(WithoutCheeckDigit);
                                BIOMAG_00_A_2D_kod_GS1 += "01" + AddCheckDigit(WithoutCheeckDigit);
                            }
                            else
                            {
                                MessageBox.Show("GTIN nemá správnou strukturu, nesedí kontrolní číslice!", "ERROR");
                                throw new Exception("GTIN nemá správnou strukturu, nesedí kontrolní číslice!");
                            }

                        }
                        else if (BIOMAG_00_VNDITNUM.Length == 13)
                        {
                            BIOMAG_00_A_2D_kod += "(01)" + AddCheckDigit(BIOMAG_00_VNDITNUM);
                            BIOMAG_00_A_2D_kod_GS1 += "01" + AddCheckDigit(BIOMAG_00_VNDITNUM);
                        }
                        else
                        {
                            MessageBox.Show("GTIN nemá 14 znaků", "ERROR");
                            throw new Exception("GTIN nemá 14 znaků");
                        }
                    }
                  
                    BIOMAG_00_EXPIRACE = DateTime.Now;
                    if (BIOMAG_00_EXPIRACE.HasValue)
                    {

                        string BIOMAG_00_datumexpirace = BIOMAG_00_EXPIRACE.Value.ToString("yyMMdd");
                        BIOMAG_00_A_2D_kod += "(11)" + BIOMAG_00_datumexpirace;
                        BIOMAG_00_A_2D_kod_GS1 += "11" + BIOMAG_00_datumexpirace;
                    }

                    // A_2D_kod += "(11)" + DateTime.Now.ToString() + @"\n";

                    if (!string.IsNullOrEmpty(item.BarcodeP))
                    {
                        ZXing.BarcodeWriter BIOMAG_00_zw = new ZXing.BarcodeWriter();
                        BIOMAG_00_zw.Format = ZXing.BarcodeFormat.CODE_128;
                        BIOMAG_00_zw.Options.Height = 50; //50
                        BIOMAG_00_zw.Options.PureBarcode = true;
                        System.Drawing.Bitmap BIOMAG_00_image1 = BIOMAG_00_zw.Write(item.BarcodeP);

                        System.IO.MemoryStream BIOMAG_00_ms = new System.IO.MemoryStream();
                        BIOMAG_00_image1.Save(BIOMAG_00_ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        byte[] BIOMAG_00_imgReportBarcode = BIOMAG_00_ms.ToArray();
                        BIOMAG_00_ms.Close();

                        string BIOMAG_00_Base64Imahe = Convert.ToBase64String(BIOMAG_00_imgReportBarcode);

                        item["BIOMAG_00_BarcodeP_IMG"] = BIOMAG_00_Base64Imahe;

                        //2D kod
                        //if (item.BarcodeP.Length < 5)
                        //{
                        //    string message_ex = string.Format("BIOMAG: GTIN nemá správnou strukturu, prvek BarcodeP:{0} musí mít více než 4 znaky!", item.BarcodeP);
                        //    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, message_ex);
                        //}

                        BIOMAG_00_A_2D_kod += "(21)" + item.BarcodeP;
                        BIOMAG_00_A_2D_kod_GS1 += "21" + item.BarcodeP;
                        BIOMAG_00_sn += "-" + item.BarcodeP;

                    }


                    #region 2D_Datamatrix GS1

                    // GS1 DataMatrix kód s FNC1 symbolem
                    string BIOMAG_00_gs1Data = (char)29 + BIOMAG_00_A_2D_kod_GS1;
                    // Generování GS1 DataMatrix
                    Bitmap BIOMAG_00_gs1DataMatrixImage = GenerateGS1DataMatrix(BIOMAG_00_gs1Data);
                    MemoryStream BIOMAG_00_ms1 = new MemoryStream();
                    BIOMAG_00_gs1DataMatrixImage.Save(BIOMAG_00_ms1, System.Drawing.Imaging.ImageFormat.Bmp);

                    byte[] BIOMAG_00_imgReportBarcode1 = BIOMAG_00_ms1.ToArray();
                    BIOMAG_00_ms1.Close();

                    string BIOMAG_00_Base64Imahe1 = Convert.ToBase64String(BIOMAG_00_imgReportBarcode1);




                    item["BIOMAG_00_A_2D_DataMatrix_IMG"] = BIOMAG_00_Base64Imahe1;
                    item["BIOMAG_00_A_2D_DataMatrix_kod"] = BIOMAG_00_A_2D_kod;

                    item["BIOMAG_00_datumExpirace"] = BIOMAG_00_datumexpirace01;
                    item["BIOMAG_00_SN"] = BIOMAG_00_sn;
                    BIOMAG_00_A_2D_kod = string.Empty;
                    BIOMAG_00_gs1Data = string.Empty;
                    BIOMAG_00_A_2D_kod_GS1 = string.Empty;
                    #endregion



                    if (item.QTYPACK > 0)
                        item["BIOMAG_00_QTY_Paleta"] = item.QTYSHPPD / item.QTYPACK;
                    else
                        item["BIOMAG_00_QTY_Paleta"] = 0;

                    item["BIOMAG_00_MJ_Paleta"] = "Pal";

                    if (string.IsNullOrEmpty(item.ITEMMJ))
                        item.ITEMMJ = "-";
                }

                //metoda na UDI code
                //ProviderTisk tisk = new ProviderTisk();
                //string temp = string.Empty;
                //tisk.TiskMetodaEtiketa_GS1(ref dt);

                string BIOMAG_00_sopdesc = SelectedVPHRow.IsSOPDESCNull() ? string.Empty : SelectedVPHRow.SOPDESC.Trim();

                #endregion


                #region reseni BIOMAG_ 8.1.2025

              
                dt.Columns.Add("BarcodeP_IMG", typeof(string));
                dt.Columns.Add("VNDITNUM_IMG", typeof(string));

                dt.Columns.Add("A_2D_DataMatrix_IMG", typeof(string));
                dt.Columns.Add("A_2D_DataMatrix_kod", typeof(string));
                string A_2D_kod = string.Empty;
                

                string SERLTNUM = string.Empty;

                string VNDITNUM = string.Empty;
                DateTime? EXPIRACE = null;

                string datumexpirace01 = string.Empty;
                string sn = string.Empty;
                dt.Columns.Add("datumExpirace", typeof(string));
                dt.Columns.Add("SN", typeof(string));

                dt.Columns.Add("QTY_Paleta", typeof(decimal));
                dt.Columns.Add("MJ_Paleta", typeof(string));

                string A_2D_kod_GS1 = string.Empty;

                #region kontrola dat UDI MaR 7.2.2025
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].kontrolaDatUDI)
                {
                    foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow item in dt)
                    {

                        char prefixNumber_char = '0'; // Převod int na char

                        

                        if (item.IsQTYPACKMJNull() || string.IsNullOrEmpty(item.QTYPACKMJ))
                        {
                            prefixNumber_char = '0';
                        }
                        else
                        {
                            prefixNumber_char = item.QTYPACKMJ[0];
                        }




                        if (!item.IsVNDITNUMNull() || !string.IsNullOrEmpty(item.VNDITNUM))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.VNDITNUM);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["VNDITNUM_IMG"] = Base64Imahe;


                            EXPIRACE = DateTime.Now;
                            if (EXPIRACE.HasValue)
                            {

                                datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                                sn = EXPIRACE.Value.ToString("yy");
                            }


                            //2D kod

                            VNDITNUM = item.VNDITNUM;
                            if (VNDITNUM.Length < 13)
                            {
                                VNDITNUM = VNDITNUM.PadLeft(12, '0');
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                            }
                            else if (VNDITNUM.Length > 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }
                            else if (VNDITNUM.Length == 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }

                            A_2D_kod += "(01)" + AddCheckDigit(VNDITNUM);
                            A_2D_kod_GS1 += "01" + AddCheckDigit(VNDITNUM);



                        }

                        EXPIRACE = DateTime.Now;
                        if (EXPIRACE.HasValue)
                        {

                            string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                            A_2D_kod += "(11)" + datumexpirace;
                            A_2D_kod_GS1 += "11" + datumexpirace;
                        }

                        // A_2D_kod += "(11)" + DateTime.Now.ToString() + @"\n";
                   
                        if (!string.IsNullOrEmpty(item.BarcodeP))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.BarcodeP);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["BarcodeP_IMG"] = Base64Imahe;

                            //2D kod
                            A_2D_kod += "(21)" + item.BarcodeP;

                            if (item.BarcodeP.Length < 5)
                            {
                                string message_ex = string.Format("GTIN nemá správnou strukturu, prvek BarcodeP:{0} musí mít více než 4 znaky!", item.BarcodeP) ;                        
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,message_ex);
                            }

                            A_2D_kod_GS1 += "21" + item.BarcodeP;
                            sn += "-" + item.BarcodeP;

                        }


                        #region 2D_Datamatrix GS1

                        // GS1 DataMatrix kód s FNC1 symbolem
                        string gs1Data = (char)29 + A_2D_kod_GS1;
                        // Generování GS1 DataMatrix
                        Bitmap gs1DataMatrixImage = GenerateGS1DataMatrix(gs1Data);
                        MemoryStream ms1 = new MemoryStream();
                        gs1DataMatrixImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);

                        byte[] imgReportBarcode1 = ms1.ToArray();
                        ms1.Close();

                        string Base64Imahe1 = Convert.ToBase64String(imgReportBarcode1);




                        item["A_2D_DataMatrix_IMG"] = Base64Imahe1;
                        item["A_2D_DataMatrix_kod"] = A_2D_kod;

                        item["datumExpirace"] = datumexpirace01;
                        item["SN"] = sn;
                        A_2D_kod = string.Empty;
                        gs1Data = string.Empty;
                        A_2D_kod_GS1 = string.Empty;
                        #endregion



                        if (item.QTYPACK > 0)
                            item["QTY_Paleta"] = item.QTYSHPPD / item.QTYPACK;
                        else
                            item["QTY_Paleta"] = 0;

                        item["MJ_Paleta"] = "Pal";

                        if (string.IsNullOrEmpty(item.ITEMMJ))
                            item.ITEMMJ = "-";
                    }

                }
                else
                {
                    foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow item in dt)
                    {

                        char prefixNumber_char = '0'; // Převod int na char



                        if (item.IsQTYPACKMJNull() || string.IsNullOrEmpty(item.QTYPACKMJ))
                        {
                            prefixNumber_char = '0';
                        }
                        else
                        {
                            prefixNumber_char = item.QTYPACKMJ[0];
                        }




                        if (!item.IsVNDITNUMNull() || !string.IsNullOrEmpty(item.VNDITNUM))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.VNDITNUM);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["VNDITNUM_IMG"] = Base64Imahe;


                            EXPIRACE = DateTime.Now;
                            if (EXPIRACE.HasValue)
                            {

                                datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                                sn = EXPIRACE.Value.ToString("yy");
                            }


                            //2D kod

                            VNDITNUM = item.VNDITNUM;
                            if (VNDITNUM.Length < 13)
                            {
                                VNDITNUM = VNDITNUM.PadLeft(12, '0');
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                            }
                            else if (VNDITNUM.Length > 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }
                            else if (VNDITNUM.Length == 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }

                            A_2D_kod += "(01)" + AddCheckDigit(VNDITNUM);
                            A_2D_kod_GS1 += "01" + AddCheckDigit(VNDITNUM);



                        }

                        EXPIRACE = DateTime.Now;
                        if (EXPIRACE.HasValue)
                        {

                            string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                            A_2D_kod += "(11)" + datumexpirace;
                            A_2D_kod_GS1 += "11" + datumexpirace;
                        }

                        // A_2D_kod += "(11)" + DateTime.Now.ToString() + @"\n";

                        if (!string.IsNullOrEmpty(item.BarcodeP))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.BarcodeP);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["BarcodeP_IMG"] = Base64Imahe;

                            //2D kod
                            A_2D_kod += "(21)" + item.BarcodeP;

                            if (item.BarcodeP.Length < 5)
                            {
                                string message_ex = string.Format("GTIN nemá správnou strukturu, prvek BarcodeP:{0} musí mít více než 4 znaky!", item.BarcodeP);
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, message_ex);
                            }

                            A_2D_kod_GS1 += "21" + item.BarcodeP;
                            sn += "-" + item.BarcodeP;

                        }


                        #region 2D_Datamatrix GS1

                        // GS1 DataMatrix kód s FNC1 symbolem
                        string gs1Data = (char)29 + A_2D_kod_GS1;
                        // Generování GS1 DataMatrix
                        Bitmap gs1DataMatrixImage = GenerateGS1DataMatrix(gs1Data);
                        MemoryStream ms1 = new MemoryStream();
                        gs1DataMatrixImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);

                        byte[] imgReportBarcode1 = ms1.ToArray();
                        ms1.Close();

                        string Base64Imahe1 = Convert.ToBase64String(imgReportBarcode1);




                        item["A_2D_DataMatrix_IMG"] = Base64Imahe1;
                        item["A_2D_DataMatrix_kod"] = A_2D_kod;

                        item["datumExpirace"] = datumexpirace01;
                        item["SN"] = sn;
                        A_2D_kod = string.Empty;
                        gs1Data = string.Empty;
                        A_2D_kod_GS1 = string.Empty;
                        #endregion



                        if (item.QTYPACK > 0)
                            item["QTY_Paleta"] = item.QTYSHPPD / item.QTYPACK;
                        else
                            item["QTY_Paleta"] = 0;

                        item["MJ_Paleta"] = "Pal";

                        if (string.IsNullOrEmpty(item.ITEMMJ))
                            item.ITEMMJ = "-";
                    }
                }
                #endregion
               
                //metoda na UDI code
                //ProviderTisk tisk = new ProviderTisk();
                //string temp = string.Empty;
                //tisk.TiskMetodaEtiketa_GS1(ref dt);

                string sopdesc = SelectedVPHRow.IsSOPDESCNull() ? string.Empty : SelectedVPHRow.SOPDESC.Trim();




                #endregion




                foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row in CZPRO_VPP_selectedVPPRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow newRow = dt.NewCZPRO_VPPRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZPRO_VPPRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }
                #endregion


                //pocet vytisku do tiskarny
                int MN_ToTisk = 1;
                if (!string.IsNullOrEmpty(pocetVytisku))
                {
                    MN_ToTisk = int.Parse(pocetVytisku);
                }
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    LogDataTableContent(dt);
                }
                //PrintReport_RDLC(dt, primyTisk, path_sablona, nazevVychTiskarny, MN_ToTisk);


                //PrintReport(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE, dt, sopdesc);

                PrintReport_RDLC_new(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE, dt, primyTisk, nazevVychTiskarny, MN_ToTisk,klicTyp, sopdesc);


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        private void LogDataTableContent(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "Tabulka je prázdná nebo null.");
                    return;
                }
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/transakce/vyrobni prikazy TISK rdlc-------------------------------------");

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        string columnName = column.ColumnName;
                        string value = row[column] != DBNull.Value ? row[column].ToString() : "NULL";
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{columnName}: {value}");
                    }
                   // Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "--------------------------------");
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/transakce/vyrobni prikazy TISK rdlc-------------------------------------");

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }



        private void PrintReport_RDLC_new(int CountEntries, string SOPNUMBE, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt, bool primyTisk, string nazevVychTiskarny, int pocetVytisku,string klicTyp, string SOPDESC = "")
        {
            try
            {
                #region 11.12.2024 logovani nova logika, tahle je stara 
                //if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                //{
                //    foreach (var item in dt)
                //    {
                //        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/transakce/vyrobni prikazy TISK rdlc-------------------------------------");


                //        foreach (var property in item.GetType().GetProperties())
                //        {
                //            try
                //            {
                //                var propertyName = property.Name;
                //                object propertyValue = null;

                //                try
                //                {
                //                    propertyValue = property.GetValue(item);
                //                }
                //                catch (Exception ex)
                //                {

                //                    propertyValue = string.Empty;
                //                }

                //                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{propertyName}: {propertyValue}");
                //            }
                //            catch (Exception ex)
                //            {
                //                // Log the exception for the specific property
                //                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, $"Error accessing property: {property.Name}. Exception: {ex.Message}");
                //            }
                //        }

                //        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/transakce/vyrobni prikazy TISK rdlc-------------------------------------");
                //    }
                //}

                #endregion


                #region 3.2.2025 MaR OLD
                using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
                {

                    plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Obec),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_SOPDESC", SOPDESC)
                    };

                    plr.NazevDataTable = "DataSet";
                    plr.DataTable = dt;

                    List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                    tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                    plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                    plr.Projekt = PrintReportLibrary.Projekt.MST;
                    #region old 10.12.2024
                    //plr.ShowPreview = true;

                    PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);


                    if (!primyTisk)
                    {
                        plr.ShowPreview = true;
                    }
                    else
                    {
                        plr.PrinterName = nazevVychTiskarny;
                    }

                    #region vyber tiskove RDLC podle typu RD
                    string path_sablona = string.Empty;
                    if (!primyTisk)
                    {


                        FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);



                        DialogResult result = form.ShowDialog();
                        // Zobrazení formuláře
                        if (result == DialogResult.OK)
                        {
                            //dataGridView = (DataGridViewRow)form.Tag;
                            path_sablona = form.ResultString;
                            // uživatel stiskl OK
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            // uživatel stiskl Cancel
                            //MessageBox.Show("Není vybraná tisková šablona");
                            return;
                        }
                    }
                    else
                    {
                        FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);
                        path_sablona = form.Prime_Tisky_Path();


                    }

                    if (string.IsNullOrEmpty(path_sablona))
                    {
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);
                    #endregion




                    plr.CountEntries = CountEntries.ToString();
                    plr.HlavickaKod = SOPNUMBE;
                    plr.HlavickaKodIMG = SOPNUMBE.Trim();

                    //try
                    //{
                    //    plr.Print(this);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Chyba! Je zvolena správná šablona?");
                    //    throw ex;
                    //}



                    try
                    {
                        for (int i = 0; i < pocetVytisku; i++)
                        {
                            plr.Print(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba při pokusu o tisk.");
                        // Ošetření výjimky při tisku
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        // Volání metody PrintReport znovu (rekurze)
                        //PrintReport(dt, primyTisk, path_sablona, nazevVychTiskarny, pocetVytisku);
                    }


                    #endregion



                    #region new 10.12.2024
                    //if (!primyTisk)
                    //{
                    //    plr.ShowPreview = true;
                    //}
                    //else
                    //{
                    //    plr.PrinterName = nazevVychTiskarny;
                    //}

                    //plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);

                    //PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);

                    //try
                    //{
                    //    for (int i = 0; i < pocetVytisku; i++)
                    //    {
                    //        plr.Print(this);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Chyba při pokusu o tisk.");
                    //    // Ošetření výjimky při tisku
                    //    Fask.Logging.ExceptionHandler2.Handle(ex);

                    //    // Volání metody PrintReport znovu (rekurze)
                    //    //PrintReport(dt, primyTisk, path_sablona, nazevVychTiskarny, pocetVytisku);
                    //}
                    #endregion




                }

                #endregion

                #region 3.2.2025 MaR NEW
                //    using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
                //    {
                //        try
                //        {
                //            plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                //            {
                //new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Firma),
                //new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Adresa),
                //new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_PSC),
                //new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Obec),
                //new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString()),
                //new Microsoft.Reporting.WinForms.ReportParameter("Param_SOPDESC", SOPDESC)
                //            };

                //            plr.NazevDataTable = "DataSet";
                //            plr.DataTable = dt;
                //            plr.typedata = new List<PrintReportLibrary.TypeData> { PrintReportLibrary.TypeData.DataTable };
                //            plr.Projekt = PrintReportLibrary.Projekt.MST;

                //            PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);

                //            string path_sablona = string.Empty;
                //            if (!primyTisk)
                //            {
                //                using (var form = new FormOdvod_TiskoveSablony(this.Text,
                //                    FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false,
                //                    FASK.Logins.Uzivatel.Instance.UserID, 0))
                //                {
                //                    if (form.ShowDialog() == DialogResult.OK)
                //                    {
                //                        path_sablona = form.ResultString;
                //                    }
                //                    else
                //                    {
                //                        MessageBox.Show("Není vybraná tisková šablona", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                        return;
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                path_sablona = new FormOdvod_TiskoveSablony(this.Text,
                //                    FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false,
                //                    FASK.Logins.Uzivatel.Instance.UserID, 0).Prime_Tisky_Path();
                //            }

                //            if (string.IsNullOrWhiteSpace(path_sablona))
                //            {
                //                MessageBox.Show("Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //                return;
                //            }

                //            plr.Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);
                //            plr.CountEntries = CountEntries.ToString();
                //            plr.HlavickaKod = SOPNUMBE;
                //            plr.HlavickaKodIMG = SOPNUMBE.Trim();

                //            for (int i = 0; i < pocetVytisku; i++)
                //            {
                //                plr.Print(this);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("Chyba při pokusu o tisk.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            Fask.Logging.ExceptionHandler2.Handle(ex);
                //        }
                //    }

                #endregion




            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start ciselniky/zasoby TISK rdlc-------------------------------------");

                        foreach (var property in item.GetType().GetProperties())
                        {
                            try
                            {
                                var propertyName = property.Name;
                                object propertyValue = null;

                                try
                                {
                                    propertyValue = property.GetValue(item);
                                }
                                catch (Exception ex)
                                {

                                    propertyValue = string.Empty;
                                }

                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{propertyName}: {propertyValue}");
                            }
                            catch (Exception ex)
                            {
                                // Log the exception for the specific property
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, $"Error accessing property: {property.Name}. Exception: {ex.Message}");
                            }
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END ciselniky/zasoby TISK rdlc-------------------------------------");
                    }
                }

                #endregion

                using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
                {

                    plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Obec),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString()),
                   // new Microsoft.Reporting.WinForms.ReportParameter("Param_SOPDESC", SOPDESC)
                    };

                    plr.NazevDataTable = "DataSet";
                    plr.DataTable = dt;

                    List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                    tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                    plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                    plr.Projekt = PrintReportLibrary.Projekt.MST;

                    if (!primyTisk)
                    {
                        plr.ShowPreview = true;
                    }
                    else
                    {
                        plr.PrinterName = nazevVychTiskarny;
                    }

                    plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);

                    PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);

                    try
                    {
                        for (int i = 0; i < pocetVytisku; i++)
                        {
                            plr.Print(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba při pokusu o tisk.");
                        // Ošetření výjimky při tisku
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        // Volání metody PrintReport znovu (rekurze)
                        //PrintReport(dt, primyTisk, path_sablona, nazevVychTiskarny, pocetVytisku);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        private void Perform_Tisk_Selected(string klicTyp, string NazevVychoziTiskarny, string pocetVytisku)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZPRO_VPP_selectedVPPRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

            

                if (providerTisk is Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)
                {

                    var PrinerName = prepareTiskParams();

                    if (PrinerName == null)
                        return;

                    int? MN_ToTisk = TiskMnozstvi(true);



                    foreach (var item in CZPRO_VPP_selectedVPPRows)
                    {
                        Dictionary<string, string> dict = PrepareDataToTisk(item);

                        if (MN_ToTisk.HasValue)
                        {
                            ((Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)providerTisk).EtiketaTisk(
                               (int)Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID,
                               Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku,
                               PrinerName,
                               dict,
                               MN_ToTisk.Value);
                        }
                        else
                            MessageBox.Show(this, "Chyba pri zadani množství", "Error", MessageBoxButtons.OK);
                    }


                    MessageBox.Show(this, "Tisk proběhl uspešně.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    //pocet vytisku do tiskarny
                    int MN_ToTisk = 1;

                    if (!string.IsNullOrEmpty(pocetVytisku))
                    {
                        MN_ToTisk = int.Parse(pocetVytisku);
                    }

                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);

                    string path = string.Empty;
                    DialogResult result = form.ShowDialog();
                    // Zobrazení formuláře
                    if (result == DialogResult.OK)
                    {
                        //dataGridView = (DataGridViewRow)form.Tag;
                        path = form.ResultString;
                        // uživatel stiskl OK
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // uživatel stiskl Cancel
                        // MessageBox.Show("Není vybraná tisková šablona");
                        return;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, CZPRO_VPP_selectedVPPRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> CZPRO_VPPRow_selected_Rows)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;



                #region selected rows

                foreach (var item in CZPRO_VPPRow_selected_Rows)
                {
                    string data = PrepareDataToTisk(plr_Path, item, MN_ToTisk);


                    if (string.IsNullOrEmpty(data))
                    {
                        MessageBox.Show("Špatná šablona pro tisk!", "Cesta k šabloně", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    for (int i = 0; i < MN_ToTisk; i++)
                    {
                        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, data))
                        {
                            throw new Exception(string.Format("Tisk etikety '{0}' se nezdařil", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku));
                        }
                    }


                }

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private TiskParams prepareTiskParams()
        {

            if (Konfigurace_Tisky.Globals_Konfig_Tisk.Konfigurace.Params.Count == 1)
            {
                TiskParams tiskParams = new TiskParams();
                tiskParams.CONFIG_NAME = Konfigurace_Tisky.Globals_Konfig_Tisk.Konfigurace.Params[0].PrinterName; // to je vse ???
                return tiskParams;
            }
            else
            {
                MessageBox.Show(this, "Pro možnost volby z vicero tiskaren je potřeba doimplementovat funkčnost!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        private int? TiskMnozstvi(bool MnozstvuAutoJedna)
        {
            try
            {
                string pocetStr = string.Empty;
                int pocetInt = 1;

                if (!MnozstvuAutoJedna)
                {
                    while (true)
                    {
                        DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
                        if (drPocet == System.Windows.Forms.DialogResult.Cancel)
                            return null;

                        try
                        {
                            pocetInt = int.Parse(pocetStr);
                        }
                        catch (Exception exPocet)
                        {
                            MessageBox.Show(exPocet.Message);
                            continue;
                        }

                        break; // vse ok ... 
                    }
                }

                return pocetInt;

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private string PrepareDataToTisk(
            string strPath,
            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow CZPRO_VPPRow,
            int? mnozstviDoTisku
            )
        {
            try
            {
                string strData = string.Empty;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(strPath))
                {
                    strData = sr.ReadToEnd();
                    sr.Close();
                }

                StringBuilder sbData = new StringBuilder();
                sbData.Append(strData);

                //dodatecne pridani a modifikace promenne pro tisk na ZPL
                string GS1_KOD_1_1D = string.Empty;
                string GS1_KOD_1_TX = string.Empty;
                string GS1_KOD_2_1D = string.Empty;
                string GS1_KOD_2_TX = string.Empty;
                string SSCC = string.Empty;
                string SSCC_bez_nul = string.Empty;
                string WEIGHT = string.Empty;
                string BarcodeP = string.Empty;
                string Expiration = string.Empty;
                string Serltnum = string.Empty;
                string ExpirationRRMMDD = string.Empty;
                string Expiration_YYYY_MM_DD = string.Empty;
                string datumexpirace01 = string.Empty;
                string sn = string.Empty;
                string SERLTNUM_GTIN14 = string.Empty;
                string VNDITNUM = string.Empty;
                char prefixNumber_char = '0'; // Převod int na char


                if (SelectedVPHRow != null && CZPRO_VPPRow != null)
                {
                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.BarcodeP))
                    {
                        BarcodeP = CZPRO_VPPRow.BarcodeP;
                    }
                    else
                    {
                        BarcodeP = " ";
                    }

                    DateTime? EXPIRACE = DateTime.Now;
                   
                    if (EXPIRACE != null && EXPIRACE.HasValue)
                    {

                        Expiration = EXPIRACE.Value.ToString("yyMMdd");

                        ExpirationRRMMDD = EXPIRACE.Value.ToString("yy-MM-dd");
                        Expiration_YYYY_MM_DD = EXPIRACE.Value.ToString("yyyy-MM-dd");

                        datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                        sn = EXPIRACE.Value.ToString("yy");

                    }
                    else
                    {
                        Expiration = " ";
                        ExpirationRRMMDD = " ";

                        Expiration_YYYY_MM_DD = " ";
                    }

                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.VNDITNUM))
                    {
                        Serltnum = CZPRO_VPPRow.VNDITNUM;
                    }
                    else
                    {
                        Serltnum = " ";
                    }


                    if (CZPRO_VPPRow["WEIGHT_NETTO"] != DBNull.Value)
                    {
                        WEIGHT = CZPRO_VPPRow.WEIGHT_NETTO.ToString();
                    }
                    else
                    {
                        WEIGHT = string.Empty; // Nebo jakákoli jiná výchozí hodnota, kterou potřebuješ.
                    }

                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.ITEMNMBR))
                    {
                        SSCC = CZPRO_VPPRow.ITEMNMBR;

                        int pocetZnaku = CZPRO_VPPRow.ITEMNMBR.Length;

                        if (pocetZnaku >= 2)
                            SSCC_bez_nul = SSCC.Substring(2);
                        else
                            SSCC_bez_nul = CZPRO_VPPRow.ITEMNMBR;



                        #region reseni BIOMAG_ 8.1.2025

                        

                        if (string.IsNullOrEmpty(CZPRO_VPPRow.QTYPACKMJ))
                        {
                            prefixNumber_char = '0';
                        }
                        else
                        {
                            prefixNumber_char = CZPRO_VPPRow.QTYPACKMJ[0];
                        }


                        if (!string.IsNullOrEmpty(CZPRO_VPPRow.VNDITNUM))
                        {

                            VNDITNUM = CZPRO_VPPRow.VNDITNUM;
                            if (VNDITNUM.Length < 13)
                            {
                                VNDITNUM = VNDITNUM.PadLeft(12, '0');
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                            }
                            else if (VNDITNUM.Length > 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }
                            else if (VNDITNUM.Length == 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }

                            SERLTNUM_GTIN14 = AddCheckDigit(VNDITNUM);
                        }
                       
                        #endregion



                    }

                    GS1_KOD_1_1D = "02" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "37" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + "";
                    GS1_KOD_1_TX = "(02)" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "(37)" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + ""; //(02) (37)
                    GS1_KOD_2_1D = SSCC; //SSCC neni v production
                    GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
                }

                //promenne na vystup
                sbData.Replace("$SERLTNUM_GTIN14$", SERLTNUM_GTIN14);
                sbData.Replace("$BarcodeP$", BarcodeP);
                sbData.Replace("$EXPIRATION$", Expiration);
                sbData.Replace("$ExpirationYYMMDD$", ExpirationRRMMDD);
                sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
                sbData.Replace("$SERLTNUM$", Serltnum);
                sbData.Replace("$WEIGHT$", WEIGHT);
                sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
                sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
                sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
                sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX);
                sbData.Replace("$SSCC$", SSCC_bez_nul);

                sbData.Replace("$SOURCE$", "Konzola");

                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

                //promenna pro logovani tisku ZPL
                int index = 0;


                foreach (DataColumn dcol in dt.Columns)
                {
                    string key = dcol.ColumnName;
                    string value = CZPRO_VPPRow[dcol.ColumnName].ToString();

                    #region 11.12.2024 ZPL tisk logovani
                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {

                        if (index==0)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/transakce/vyrobni prikazy TISK zpl zaznam: " + "-------------------------------------");


                            //11.12.2024 pridani promennych na vypis
                            string key1 = "BarcodeP";
                            string key2 = "EXPIRATION";
                            string key3 = "ExpirationYYMMDD";
                            string key4 = "Expiration_YYYY_MM_DD";
                            string key5 = "SERLTNUM";
                            string key6 = "WEIGHT";
                            string key7 = "GS1_KOD_1_1D";
                            string key8 = "GS1_KOD_1_TX";
                            string key9 = "GS1_KOD_2_1D";
                            string key10 = "GS1_KOD_2_TX";
                            string key11 = "SSCC";
                            string key12 = "SERLTNUM_GTIN14";


                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key1}: {BarcodeP}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key2}: {Expiration}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key3}: {ExpirationRRMMDD}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key4}: {Expiration_YYYY_MM_DD}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key5}: {Serltnum}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key6}: {WEIGHT}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key7}: {GS1_KOD_1_1D}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key8}: {GS1_KOD_1_TX}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key9}: {GS1_KOD_2_1D}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key10}: {GS1_KOD_2_TX}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key11}: {SSCC_bez_nul}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key12}: {SERLTNUM_GTIN14}");
                        }

                       Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key}: {value}");
                        
                        index++;
                        if (index == dt.Columns.Count)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/transakce/vyrobni prikazy TISK zpl zaznam: " + "-------------------------------------");
                        }

                    }

                    #endregion

                    try
                    {
                        sbData.Replace("$" + key + "$", value.Trim());
                    }
                    catch
                    {
                    }
                }

                return sbData.ToString();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow productionRow_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();

                        #region MaR 11.11. 2024 nepotrebne
                        //string GS1_KOD_1_1D = string.Empty;
                        //string GS1_KOD_1_TX = string.Empty;
                        //string GS1_KOD_2_1D = string.Empty;
                        //string GS1_KOD_2_TX = string.Empty;
                        //string SSCC = string.Empty;
                        //string SSCC_bez_nul = string.Empty;
                        //string WEIGHT = string.Empty;

                        //string BarcodeP = string.Empty;
                        //string Expiration = string.Empty;
                        //string Serltnum = string.Empty;
                        //string ExpirationRRMMDD = string.Empty;


                        //string Expiration_YYYY_MM_DD = string.Empty;



                        //if (productionRow_data != null)
                        //{



                        //    if (!productionRow_data.IsSERLTNUMNull())
                        //    {
                        //        Serltnum = productionRow_data.SERLTNUM;
                        //    }
                        //    else
                        //    {
                        //        Serltnum = " ";
                        //    }


                        //    //------------START-DATA----------------
                        //    //dotahovat data SSCC a WEIGHT

                        //    if (!productionRow_data.IsWEIGHTNull())
                        //    {
                        //        WEIGHT = productionRow_data.WEIGHT.ToString();
                        //    }

                        //    if (!data.ContainsKey("WEIGHT"))
                        //        data.Add("WEIGHT", WEIGHT);



                        //    if (!data.ContainsKey("SSCC"))
                        //        data.Add("SSCC", SSCC_bez_nul);

                        //    //------------END-DATA----------------

                        //}


                        //if (!data.ContainsKey("GS1_KOD_1_1D"))
                        //    data.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

                        //if (!data.ContainsKey("GS1_KOD_1_TX"))
                        //    data.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

                        //if (!data.ContainsKey("GS1_KOD_2_1D"))
                        //    data.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

                        //if (!data.ContainsKey("GS1_KOD_2_TX"))
                        //    data.Add("GS1_KOD_2_TX", GS1_KOD_2_TX); 
                        #endregion

                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = productionRow_data[dcol.ColumnName].ToString();
                            if (!data.ContainsKey(key))
                                data.Add(key, value);
                        }


                        return data;
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        return null;
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string PrepareDataToTisk(
       string strPath,
       Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow CZPRO_VPHRow,
       Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow CZPRO_VPPRow,
       int? mnozstviDoTisku
       )
        {
            try
            {
                string strData = string.Empty;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(strPath))
                {
                    strData = sr.ReadToEnd();
                    sr.Close();
                }

                StringBuilder sbData = new StringBuilder();
                sbData.Append(strData);

                string GS1_KOD_1_1D = string.Empty;
                string GS1_KOD_1_TX = string.Empty;
                string GS1_KOD_2_1D = string.Empty;
                string GS1_KOD_2_TX = string.Empty;
                string SSCC = string.Empty;
                string SSCC_bez_nul = string.Empty;
                string WEIGHT = string.Empty;

                string BarcodeP = string.Empty;
                string Expiration = string.Empty;
                string Serltnum = string.Empty;
                string ExpirationRRMMDD = string.Empty;
                string Expiration_YYYY_MM_DD = string.Empty;

                string datumexpirace01 = string.Empty;
                string sn = string.Empty;



                #region MaR 28.8.2024, prepsani promennych na vyrobni prikazy old
                if (CZPRO_VPHRow != null && CZPRO_VPPRow != null)
                {
                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.BarcodeP))
                    {
                        BarcodeP = CZPRO_VPPRow.BarcodeP;
                    }
                    else
                    {
                        BarcodeP = " ";
                    }




                    DateTime? EXPIRACE = DateTime.Now;
                    //EXPIRACE = DateTime.Now;

                    if (EXPIRACE != null && EXPIRACE.HasValue)
                    {

                        // Expiration = EXPIRACE.Value.ToString();

                        Expiration = EXPIRACE.Value.ToString("yyMMdd");


                        ExpirationRRMMDD = EXPIRACE.Value.ToString("yy-MM-dd");

                        // Expiration_YYYY_MM_DD = row.EXPIRATION.Value.ToString("yyyy-MM-dd");


                        //string input = EXPIRACE;  // Tvoje vstupní data
                        //DateTime expirationDate = DateTime.ParseExact(input, "yyMMdd", null);
                        Expiration_YYYY_MM_DD = EXPIRACE.Value.ToString("yyyy-MM-dd");
                        //DateTime expirationDate = DateTime.Parse(row.EXPIRATION);
                        //Expiration_YYYY_MM_DD = expirationDate.ToString("yyyy-MM-dd");

                        datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                        sn = EXPIRACE.Value.ToString("yy");

                    }
                    else
                    {
                        Expiration = " ";
                        ExpirationRRMMDD = " ";

                        Expiration_YYYY_MM_DD = " ";
                    }

                    #region spatne promenne, opravit!

                    //CZPRO_VPPRow.ser

                    //if (!row.IsSERLTNUMNull())
                    //{
                    //    Serltnum = row.SERLTNUM;
                    //}
                    //else
                    //{
                    //    Serltnum = " ";
                    //}

                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.VNDITNUM))
                    {
                        Serltnum = CZPRO_VPPRow.VNDITNUM;
                    }
                    else
                    {
                        Serltnum = " ";
                    }

                    //if (!row.IsWEIGHTNull())
                    //{
                    //    WEIGHT = row.WEIGHT.ToString();
                    //}


                    if (CZPRO_VPPRow["WEIGHT_NETTO"] != DBNull.Value)
                    {
                        WEIGHT = CZPRO_VPPRow.WEIGHT_NETTO.ToString();
                    }
                    else
                    {
                        WEIGHT = string.Empty; // Nebo jakákoli jiná výchozí hodnota, kterou potřebuješ.
                    }

                    #endregion


                    #region reseni BIOMAG_ 8.1.2025

                    string SERLTNUM_GTIN14 = string.Empty;
                    string VNDITNUM = string.Empty;
                    char prefixNumber_char = '0'; // Převod int na char

                    if (string.IsNullOrEmpty(CZPRO_VPPRow.QTYPACKMJ))
                    {
                        prefixNumber_char = '0';
                    }
                    else
                    {
                        prefixNumber_char = CZPRO_VPPRow.QTYPACKMJ[0];
                    }


                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.VNDITNUM))
                    {

                        VNDITNUM = CZPRO_VPPRow.VNDITNUM;
                        if (VNDITNUM.Length < 13)
                        {
                            VNDITNUM = VNDITNUM.PadLeft(12, '0');
                            VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                        }
                        else if (VNDITNUM.Length > 13)
                        {
                            VNDITNUM = VNDITNUM.Substring(0, 12);
                            VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                        }
                        else if (VNDITNUM.Length == 13)
                        {
                            VNDITNUM = VNDITNUM.Substring(0, 12);
                            VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                        }

                        SERLTNUM_GTIN14 = AddCheckDigit(VNDITNUM);
                    }
                    sbData.Replace("$SERLTNUM_GTIN14$", SERLTNUM_GTIN14);
                    #endregion



                    sbData.Replace("$BarcodeP$", BarcodeP);
                    //sbData.Replace("$Expiration$", Expiration);
                    sbData.Replace("$EXPIRATION$", Expiration);
                    sbData.Replace("$ExpirationYYMMDD$", ExpirationRRMMDD);
                    sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
                    //sbData.Replace("$Serltnum$", Serltnum);
                    sbData.Replace("$SERLTNUM$", Serltnum);
                    //----------------------------------------------------------







                    sbData.Replace("$WEIGHT$", WEIGHT);


                    if (!string.IsNullOrEmpty(CZPRO_VPPRow.ITEMNMBR))
                    {
                        SSCC = CZPRO_VPPRow.ITEMNMBR;
                        SSCC_bez_nul = SSCC.Substring(2);
                    }

                    //if (!row.IsNMBRPALNull())
                    //{
                    //    SSCC = row.NMBRPAL;
                    //    SSCC_bez_nul = SSCC.Substring(2);
                    //}

                    sbData.Replace("$SSCC$", SSCC_bez_nul);

                    GS1_KOD_1_1D = "02" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "37" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + "";
                    GS1_KOD_1_TX = "(02)" + CZPRO_VPPRow.BarcodeP.PadLeft(14, '0') + "(37)" + CZPRO_VPPRow.QTYSHPPD.ToString("0000") + ""; //(02) (37)
                    GS1_KOD_2_1D = SSCC; //SSCC neni v production
                    GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
                }

                sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
                sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
                sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
                sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX);

                sbData.Replace("$SOURCE$", "Konzola");


               





                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

                foreach (DataColumn dcol in dt.Columns)
                {
                    string key = dcol.ColumnName;
                    string value = CZPRO_VPPRow[dcol.ColumnName].ToString();

                    try
                    {
                        sbData.Replace("$" + key + "$", value.Trim());
                    }
                    catch
                    {
                    }
                }
                #endregion


                return sbData.ToString();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }




        public static void Save_Etikety(string SubPath, string Zdroj, string Obsah, Guid G, string Pripona)
        {

            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Zdroj + "_" + G.ToString() + Pripona;
            if (string.IsNullOrEmpty(Obsah))
            {
                string c = Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path, "ETIKETY" + @"\" + FileName);
                string msg = string.Format("Halo tady je prazdny soubor, proč?" + Environment.NewLine +
                    "SubPath: {0}" + Environment.NewLine +
                    "Zdroj: {1}" + Environment.NewLine +
                    "G: {2}" + Environment.NewLine +
                    "Pripona: {3}" + Environment.NewLine,
                    SubPath,
                    Zdroj,
                    G,
                    Pripona
                    );
                ExceptionHandler2.Handle("", c);
            }


            string cesta = Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path, SubPath + @"\" + FileName);
            ExceptionHandler2.Handle(Obsah, cesta);

        }
        #endregion

    }
}
