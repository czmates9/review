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
using Fask.Interfaces.DataSets;

namespace Konzola.Planovani
{
    public partial class FormPlanovaniVyroby_Zaplanovani : Form
    {

        #region Parametry

        private Fask.Interfaces.IMES providerPV_Z = null;

        private List<Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr> filtry = new List<Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr>();


        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region c'tor a eventy formu

        public FormPlanovaniVyroby_Zaplanovani()
        {
            InitializeComponent();

            this.dg_PV_Z.UpdateColumnHeaderCellsByDatasource();
            this.dg_PV_Z_H.UpdateColumnHeaderCellsByDatasource();
            // >>> přidáno – barvení řádků podle PLAN_FLAG
            this.dg_PV_Z.CellFormatting += dg_PV_Z_CellFormatting;
        }

        private void FormPlanovaniVyroby_Zaplanovani_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                this.dg_PV_Z.LoadConfiguration(this.GetType().ToString() + "_Pol");
                this.dg_PV_Z_H.LoadConfiguration(this.GetType().ToString() + "_H");

                ADGVSTB_Pol.SetColumns(dg_PV_Z.Columns);
                ADGVSTB_H.SetColumns(dg_PV_Z_H.Columns);

                rb_CheckedChanged(null,null);

                // inicializace providera
                InitProvider();

                //tabControl1_SelectedIndexChanged(null, null);

                

                if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyroby_Zaplanovani_TabPage))
                {
                    string tb1 = tabPage1.Name.Trim();
                    string tb2 = tabPage2.Name.Trim();

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyroby_Zaplanovani_TabPage.Trim() == tb1)
                    {
                        tabControl1.SelectedTab = tabPage1;
                    }
                    else if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyroby_Zaplanovani_TabPage.Trim() == tb2)
                    {
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        // Neznam...
                    }

                }


                PerformRefreshData();

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void dg_PV_Z_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Chceme barvit celé řádky, takže stačí vyhodnotit jednou na řádek
            if (e.RowIndex < 0)
                return;

            var grid = (DataGridView)sender;
            var rowView = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (rowView == null)
                return;

            var dataRow = rowView.Row as Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniRow;
            if (dataRow == null || dataRow.IsPLAN_FLAGNull())
                return;

            // pokud chceš barvit jen když je zaškrtnuto "Jen nezaplánované",
            // můžeš tady testovat checkbox na formuláři, např. chb_NEzap.Checked
            // if (!chb_NEzaplanovane.Checked) return;

            // reset na default (pro případ, že se PLAN_FLAG změní)
            grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = grid.DefaultCellStyle.BackColor;

            switch (dataRow.PLAN_FLAG)
            {
                case 1: // je ve shodném množství -> zeleně
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    break;

                case 2: // je v rozdílném množství -> žlutě
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                    break;

                case 0:
                default:
                    // není v našich tabulkách -> bez podbarvení
                    // už je resetnuté na default, nic neděláme
                    break;
            }
        }

        private void FormPlanovaniVyroby_Zaplanovani_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.dg_PV_Z.SaveConfiguration(this.GetType().ToString() + "_Pol");
            this.dg_PV_Z_H.SaveConfiguration(this.GetType().ToString() + "_H");

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyroby_Zaplanovani_TabPage = tabControl1.SelectedTab.Name.Trim();
            Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();

            this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
        }

        #endregion

        #region Inicializace provideru

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerPV_Z == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.PV.IPV).IsAssignableFrom(t))
                            {
                                providerPV_Z = (Fask.Interfaces.Vyroba.PV.IPV)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPV_Z != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPV_Z.InitProvider();

            
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        #region Click Eventy

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                PerformRefreshData();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Nacist_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    PerformLoadData();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    PerformLoadData();
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    PerformLoadData_Hlavicky();
                }
                else
                {
                    //Neznamy...
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region PerformMetody

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

        private void PerformRefreshData_old20251201()
        {
            try
            {
                Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filter = new Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr();

                if (!CreateFilter(ref filter))
                    return;


                if ((providerPV_Z != null) && (providerPV_Z is Fask.Interfaces.Vyroba.PV.IPV_GetZaplanovani))
                    this.ds_PV_Z = ((Fask.Interfaces.Vyroba.PV.IPV_GetZaplanovani)providerPV_Z).GetZaplanovani(filter);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_GetZaplanovani.");

                if (ds_PV_Z != null)
                {
                    //bs_PV_Z.DataSource = ds_PV_Z.PV_Zaplanovani;
                    bs_PV_Z.DataSource = ds_PV_Z;

                    var dt_Grup = ds_PV_Z.PV_Zaplanovani.GroupBy(x => new
                    {
                        OBJ_NMBR = (x.IsOBJ_NMBRNull() ? string.Empty : x.OBJ_NMBR),
                        OBJ_DESC = (x.IsOBJ_DESCNull() ? string.Empty : x.OBJ_DESC),
                        OBJ_TYPE = (x.IsOBJ_TYPENull() ? string.Empty : x.OBJ_TYPE),
                        OBJ_COMPANY = (x.IsOBJ_COMPANYNull() ? string.Empty : x.OBJ_COMPANY),
                        OBJ_DATE_FROM = (x.IsOBJ_DATE_FROMNull() ? DateTime.MinValue : x.OBJ_DATE_FROM),
                        OBJ_DATE_TO = (x.IsOBJ_DATE_TONull() ? DateTime.MinValue : x.OBJ_DATE_TO),
                        OBJ_ORD = (x.IsOBJ_ORDNull() ? -1 : x.OBJ_ORD),
                        OBJ_ForUh = (x.IsOBJ_ForUhNull() ? -1 : x.OBJ_ForUh),
                        OBJ_ForUh_IDS = (x.IsOBJ_ForUh_IDSNull() ? string.Empty : x.OBJ_ForUh_IDS),
                        UserParam_1 = (x.IsUserParam_1Null() ? string.Empty : x.UserParam_1),
                        UserParam_2 = (x.IsUserParam_2Null() ? string.Empty : x.UserParam_2),
                        OBJ_DATE_ZAPL = (x.IsOBJ_DATE_ZAPLNull() ? DateTime.MinValue : x.OBJ_DATE_ZAPL)
                    });


                    foreach (var item in dt_Grup)
                    {

                        var row = ds_PV_Z.PV_Zaplanovani_Hlavicky.NewPV_Zaplanovani_HlavickyRow();

                        row.OBJ_NMBR = item.Key.OBJ_NMBR;
                        row.OBJ_DESC = item.Key.OBJ_DESC;
                        row.OBJ_TYPE = item.Key.OBJ_TYPE;
                        row.OBJ_COMPANY = item.Key.OBJ_COMPANY;

                        if (item.Key.OBJ_DATE_FROM == DateTime.MinValue)
                        {
                            row.SetOBJ_DATE_FROMNull();
                        }
                        else
                        {
                            row.OBJ_DATE_FROM = item.Key.OBJ_DATE_FROM;
                        }

                        if (item.Key.OBJ_DATE_TO == DateTime.MinValue)
                        {
                            row.SetOBJ_DATE_TONull();
                        }
                        else
                        {
                            row.OBJ_DATE_TO = item.Key.OBJ_DATE_TO;
                        }

                        if (item.Key.OBJ_DATE_ZAPL == DateTime.MinValue)
                        {
                            row.SetOBJ_DATE_ZAPLNull();
                        }
                        else
                        {
                            row.OBJ_DATE_ZAPL = item.Key.OBJ_DATE_ZAPL;
                        }

                        row.OBJ_ORD = item.Key.OBJ_ORD;
                        row.QTY = item.Sum(x => x.QTY);

                        row.QTY_Zaplanovano = item.Sum(x => Convert.ToDecimal(x.IsQTY_ZaplanovanoNull() ? "0" : x.QTY_Zaplanovano)).ToString();
                        row.QTY_Zbyva = item.Sum(x => Convert.ToDecimal(x.IsQTY_ZbyvaNull() ? "0" : x.QTY_Zbyva)).ToString();

                        row.OBJ_ForUh = item.Key.OBJ_ForUh;
                        row.OBJ_ForUh_IDS = item.Key.OBJ_ForUh_IDS;

                        row.UserParam_1 = item.Key.UserParam_1;
                        row.UserParam_2 = item.Key.UserParam_2;

                        ds_PV_Z.PV_Zaplanovani_Hlavicky.AddPV_Zaplanovani_HlavickyRow(row);

                    }

                    bs_PV_Z_H.DataSource = ds_PV_Z;


                }
                else
                {
                    MessageBox.Show(this, "Nastala chyba při otevírání seznamu obchodních požadavkú!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    PerformCancel();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PerformRefreshData()
        {
            try
            {
                Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filter = new Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr();

                if (!CreateFilter(ref filter))
                    return;

                if (bwZaplanovani.IsBusy)
                    return; // nebo případně zobrazit info, že už se načítá

                Cursor.Current = Cursors.WaitCursor;

                // UI zamrazit jen vizuálně
                this.Enabled = false; // volitelné, pokud nechceš aby do toho uživatel šahal

                bwZaplanovani.RunWorkerAsync(filter);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void PerformLoadData()
        {
            try
            {
                if (this.dg_PV_Z.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na zaplánování", this.Text, MessageBoxButtons.OK);
                    return;
                }


                //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

                //sw.Start();

                DateTime? DATE_ZAPLANOVANI = (DateTime?)DateTime.Now;
                int? USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);
                bool VP_PRPS = false;
                decimal? VP_PRPS_QTY = null;
                string VP_PRPS_SOPNUMBE = string.Empty;
                decimal? VP_PRDCT_QTY = null;

                Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniDataTable dttmp = new Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniDataTable();

                foreach (DataGridViewRow item in this.dg_PV_Z.SelectedRows)
                {
                    Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniRow row = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniRow;
                    dttmp.ImportRow(row);
                }

                var tmp = Metoda_pro_OrderBy(dttmp);


                // 🔔 Kontrola, jestli už je některá z vybraných objednávek načtená (PLAN_FLAG = 1)
                var uzNactene = tmp
                    .Where(x => !x.IsPLAN_FLAGNull() && x.PLAN_FLAG == 1)
                    .ToList();

                if (uzNactene.Any())
                {
                    // první OBJ_NMBR, která je už načtená
                    string prvniObj = uzNactene
                        .Where(x => !x.IsOBJ_NMBRNull())
                        .Select(x => x.OBJ_NMBR)
                        .FirstOrDefault() ?? "<neznámé číslo>";

                    // MessageBox "always on top" – používám DefaultDesktopOnly
                    DialogResult dotaz = MessageBox.Show(
                        $"Objednávka {prvniObj} je již načtena.\n" +
                        $"Chceš přesto pokračovat v načítání vybraných záznamů?",
                        "Objednávka již existuje",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                    if (dotaz == DialogResult.No)
                        return; // ukončit načítání úplně
                }


                foreach (var row in tmp)
                {
                    //Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniRow row = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniRow;
                    

                    //if (row.IsQTY_ZaplanovanoNull())
                    //{

                    string OBJ_NMBR = row.IsOBJ_NMBRNull() ? string.Empty : row.OBJ_NMBR;
                    string OBJ_DESC = row.IsOBJ_DESCNull() ? string.Empty : row.OBJ_DESC;
                    string OBJ_TYPE = row.IsOBJ_TYPENull() ? string.Empty : row.OBJ_TYPE;
                    string OBJ_COMPANY = row.IsOBJ_COMPANYNull() ? string.Empty : row.OBJ_COMPANY;
                    DateTime? OBJ_DATE_FROM = row.IsOBJ_DATE_FROMNull() ? null : (DateTime?)row.OBJ_DATE_FROM;
                    DateTime? OBJ_DATE_TO = row.IsOBJ_DATE_TONull() ? null : (DateTime?)row.OBJ_DATE_TO;
                    int? OBJ_ORD = row.IsOBJ_ORDNull() ? null : (int?)row.OBJ_ORD;
                    int? OBJ_ITEM_ORD = row.IsOBJ_ITEM_ORDNull() ? null : (int?)row.OBJ_ITEM_ORD;
                    string ITEMNMBR = row.ITEMNMBR;
                    string ITEMDESC = row.IsITEMDESCNull() ? string.Empty : row.ITEMDESC;
                    string ITEMCODE = row.IsITEMCODENull() ? string.Empty : row.ITEMCODE;

                    //decimal JizZaplanovano = row.IsQTY_ZaplanovanoNull() ? 0 : decimal.Parse(row.QTY_Zaplanovano);
                    //decimal QTY = row.QTY - JizZaplanovano;
                    decimal QTY = row.QTY;

                    int? Ref_PVH = null;



                    bool state;

                    if ((providerPV_Z != null) && providerPV_Z is Fask.Interfaces.Vyroba.PV.IPV_Insert_PV)
                    {
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Insert_PV)providerPV_Z).Insert(
                                                                                                        OBJ_NMBR,
                                                                                                        OBJ_DESC,
                                                                                                        OBJ_TYPE,
                                                                                                        OBJ_COMPANY,
                                                                                                        OBJ_DATE_FROM,
                                                                                                        OBJ_DATE_TO,
                                                                                                        OBJ_ORD,
                                                                                                        OBJ_ITEM_ORD,
                                                                                                        ITEMNMBR,
                                                                                                        ITEMDESC,
                                                                                                        ITEMCODE,
                                                                                                        QTY,
                                                                                                        DATE_ZAPLANOVANI,
                                                                                                        VP_PRPS,
                                                                                                        VP_PRPS_QTY,
                                                                                                        VP_PRPS_SOPNUMBE,
                                                                                                        VP_PRDCT_QTY,
                                                                                                        USERID,
                                                                                                        Ref_PVH);
                    }
                    else
                        throw new Exception("IPV_Insert_PV not implementet");

                    if (!state)
                    {
                        throw new Exception(string.Format("položka '{0}'({1}). Chyba pri Inserte... ", row.ITEMNMBR, row.ITEMDESC));
                    }

                    //27.11.2025 MaR preskocit pokud neni Pohoda E1
                    if ((providerPV_Z != null) && providerPV_Z is Fask.Interfaces.Vyroba.PV.IPV_Edit_Zaplanovane)
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Edit_Zaplanovane)providerPV_Z).Edit_Zaplanovane(row.OBJ_ITEM_ORD, QTY, 1);
                    else
                        throw new Exception("IPV_Edit_Zaplanovane not implementet");
                    //}

                }

                //sw.Stop();

                //TimeSpan time = sw.Elapsed;

                //string a = string.Format("{0},{1}", time.Seconds, time.Milliseconds);

                PerformRefreshData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void PerformLoadData_Hlavicky()
        {
            try
            {
                if (this.dg_PV_Z_H.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na zaplánování", this.Text, MessageBoxButtons.OK);
                    return;
                }


                DateTime? DATE_ZAPLANOVANI = (DateTime?)DateTime.Now;
                int? USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);
                string VP_PRPS_SOPNUMBE = string.Empty;
                bool VP_PRPS = false;
                decimal? VP_PRPS_QTY = null;
                decimal? VP_PRDCT_QTY = null;

                Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniDataTable dttmp = new Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniDataTable();


                foreach (DataGridViewRow val in this.dg_PV_Z_H.SelectedRows)
                {
                    Fask.Interfaces.DataSets.Vyroba_Planovani.PV_Zaplanovani_HlavickyRow xxx = ((val.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.PV_Zaplanovani_HlavickyRow;

                    var whereDT = ds_PV_Z.PV_Zaplanovani.Where(x =>
                    x.OBJ_NMBR == xxx.OBJ_NMBR
                    && x.OBJ_ORD == xxx.OBJ_ORD
                    );


                    foreach (var row in whereDT)
                    {
                        dttmp.ImportRow(row);
                    }
                }

              
                var tmp = Metoda_pro_OrderBy(dttmp);


                // 🔔 Kontrola, jestli už je některá z vybraných objednávek načtená (PLAN_FLAG = 1)
                var uzNactene = tmp
                    .Where(x => !x.IsPLAN_FLAGNull() && x.PLAN_FLAG == 1)
                    .ToList();

                if (uzNactene.Any())
                {
                    // první OBJ_NMBR, která je už načtená
                    string prvniObj = uzNactene
                        .Where(x => !x.IsOBJ_NMBRNull())
                        .Select(x => x.OBJ_NMBR)
                        .FirstOrDefault() ?? "<neznámé číslo>";

                    // MessageBox "always on top" – používám DefaultDesktopOnly
                    DialogResult dotaz = MessageBox.Show(
                        $"Objednávka {prvniObj} je již načtena.\n" +
                        $"Chceš přesto pokračovat v načítání vybraných záznamů?",
                        "Objednávka již existuje",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                    if (dotaz == DialogResult.No)
                        return; // ukončit načítání úplně
                }



                foreach (var row in tmp)
                {

                    string OBJ_NMBR = row.IsOBJ_NMBRNull() ? string.Empty : row.OBJ_NMBR;
                    string OBJ_DESC = row.IsOBJ_DESCNull() ? string.Empty : row.OBJ_DESC;
                    string OBJ_TYPE = row.IsOBJ_TYPENull() ? string.Empty : row.OBJ_TYPE;
                    string OBJ_COMPANY = row.IsOBJ_COMPANYNull() ? string.Empty : row.OBJ_COMPANY;
                    DateTime? OBJ_DATE_FROM = row.IsOBJ_DATE_FROMNull() ? null : (DateTime?)row.OBJ_DATE_FROM;
                    DateTime? OBJ_DATE_TO = row.IsOBJ_DATE_TONull() ? null : (DateTime?)row.OBJ_DATE_TO;
                    int? OBJ_ORD = row.IsOBJ_ORDNull() ? null : (int?)row.OBJ_ORD;
                    int? OBJ_ITEM_ORD = row.IsOBJ_ITEM_ORDNull() ? null : (int?)row.OBJ_ITEM_ORD;
                    string ITEMNMBR = row.ITEMNMBR;
                    string ITEMDESC = row.IsITEMDESCNull() ? string.Empty : row.ITEMDESC;
                    string ITEMCODE = row.IsITEMCODENull() ? string.Empty : row.ITEMCODE;

                    decimal QTY = row.QTY;

                    int? Ref_PVH = null;

                    bool state;

                    if ((providerPV_Z != null) && providerPV_Z is Fask.Interfaces.Vyroba.PV.IPV_Insert_PV)
                    {
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Insert_PV)providerPV_Z).Insert(
                                                                                                        OBJ_NMBR,
                                                                                                        OBJ_DESC,
                                                                                                        OBJ_TYPE,
                                                                                                        OBJ_COMPANY,
                                                                                                        OBJ_DATE_FROM,
                                                                                                        OBJ_DATE_TO,
                                                                                                        OBJ_ORD,
                                                                                                        OBJ_ITEM_ORD,
                                                                                                        ITEMNMBR,
                                                                                                        ITEMDESC,
                                                                                                        ITEMCODE,
                                                                                                        QTY,
                                                                                                        DATE_ZAPLANOVANI,
                                                                                                        VP_PRPS,
                                                                                                        VP_PRPS_QTY,
                                                                                                        VP_PRPS_SOPNUMBE,
                                                                                                        VP_PRDCT_QTY,
                                                                                                        USERID,
                                                                                                        Ref_PVH);
                    }
                    else
                        throw new Exception("IPV_Insert_PV not implementet");

                    if (!state)
                    {
                        throw new Exception(string.Format("položka '{0}'({1}). Chyba pri Inserte... ", row.ITEMNMBR, row.ITEMDESC));
                    }

                    //27.11.2025 MaR zapisovat jen pokud je Pohoda E1
                    if ((providerPV_Z != null) && providerPV_Z is Fask.Interfaces.Vyroba.PV.IPV_Edit_Zaplanovane)
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Edit_Zaplanovane)providerPV_Z).Edit_Zaplanovane(row.OBJ_ITEM_ORD, QTY, 1);
                    else
                        throw new Exception("IPV_Edit_Zaplanovane not implementet");


                }


                PerformRefreshData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private OrderedEnumerableRowCollection<Vyroba_Planovani.PV_ZaplanovaniRow> Metoda_pro_OrderBy(Vyroba_Planovani.PV_ZaplanovaniDataTable dttmp)
        {
            OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Vyroba_Planovani.PV_ZaplanovaniRow> tmp = null;

            if (rb_Zapis.Checked)
            {
                if (chb_Zapis.Checked)
                {
                    //tmp = dttmp.OrderByDescending(x => x.OBJ_DATE_ZAPL).OrderBy(x => x.OBJ_NMBR).OrderBy(x => x.OBJ_ITEM_ORD);
                    tmp = dttmp.OrderByDescending(x => x.OBJ_DATE_ZAPL).ThenByDescending(x => x.OBJ_NMBR).ThenByDescending(x => x.OBJ_ITEM_ORD);
                }
                else
                {
                    //tmp = dttmp.OrderBy(x => x.OBJ_DATE_ZAPL).OrderBy(x => x.OBJ_NMBR).OrderBy(x => x.OBJ_ITEM_ORD);
                    tmp = dttmp.OrderBy(x => x.OBJ_DATE_ZAPL).ThenBy(x => x.OBJ_NMBR).ThenBy(x => x.OBJ_ITEM_ORD);
                }
            }
            if (rb_Od.Checked)
            {
                if (chb_Od.Checked)
                {
                    //tmp = dttmp.OrderByDescending(x => x.OBJ_DATE_FROM).OrderBy(x => x.OBJ_NMBR).OrderBy(x => x.OBJ_ITEM_ORD);
                    tmp = dttmp.OrderByDescending(x => x.IsOBJ_DATE_FROMNull() ? DateTime.MinValue : x.OBJ_DATE_FROM).ThenByDescending(x => x.OBJ_NMBR).ThenByDescending(x => x.OBJ_ITEM_ORD);                   
                }
                else
                {
                    //tmp = dttmp.OrderBy(x => x.OBJ_DATE_FROM).OrderBy(x => x.OBJ_NMBR).OrderBy(x => x.OBJ_ITEM_ORD);
                    tmp = dttmp.OrderBy(x => x.IsOBJ_DATE_FROMNull() ? DateTime.MinValue : x.OBJ_DATE_FROM).ThenBy(x => x.OBJ_NMBR).ThenBy(x => x.OBJ_ITEM_ORD);
                }
            }
            if (rb_Do.Checked)
            {
                if (chb_Do.Checked)
                {
                    //tmp = dttmp.OrderByDescending(x => x.OBJ_DATE_TO).OrderBy(x => x.OBJ_NMBR).OrderBy(x => x.OBJ_ITEM_ORD);
                    tmp = dttmp.OrderByDescending(x => x.IsOBJ_DATE_TONull() ? DateTime.MaxValue : x.OBJ_DATE_TO).ThenByDescending(x => x.OBJ_NMBR).ThenByDescending(x => x.OBJ_ITEM_ORD);
                }
                else
                {
                    //tmp = dttmp.OrderBy(x => x.OBJ_DATE_TO).OrderBy(x => x.OBJ_NMBR).OrderBy(x => x.OBJ_ITEM_ORD);
                    tmp = dttmp.OrderBy(x => x.IsOBJ_DATE_TONull() ? DateTime.MaxValue : x.OBJ_DATE_TO).ThenByDescending(x => x.OBJ_NMBR).ThenByDescending(x => x.OBJ_ITEM_ORD);
                }
            }

            return tmp;
        }



        #endregion

        #region ChechBox event na zmenu stavu

        private void cbNezaplanovane_CheckedChanged(object sender, EventArgs e)
        {
            PerformRefreshData();
        }

        #endregion

        #region AdvanceDatagridView

        private void ADGVSTB_Pol_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PV_Z.CurrentCell.ColumnIndex + 1 >= dg_PV_Z.ColumnCount;
                bool endrow = dg_PV_Z.CurrentCell.RowIndex + 1 >= dg_PV_Z.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PV_Z.CurrentCell.ColumnIndex;
                    startRow = dg_PV_Z.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PV_Z.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PV_Z.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PV_Z.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PV_Z.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PV_Z.CurrentCell = c;


        }

        private void ADGVSTB_H_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PV_Z_H.CurrentCell.ColumnIndex + 1 >= dg_PV_Z_H.ColumnCount;
                bool endrow = dg_PV_Z_H.CurrentCell.RowIndex + 1 >= dg_PV_Z_H.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PV_Z_H.CurrentCell.ColumnIndex;
                    startRow = dg_PV_Z_H.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PV_Z_H.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PV_Z_H.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PV_Z_H.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PV_Z_H.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PV_Z_H.CurrentCell = c;
        }


        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TabPage tab = tabControl1.SelectedTab;

                //if (tab.Name == tabPage1.Name) // přehled
                //{
                //    btn_Nacist.Enabled = true;
                //}
                //else if (tab.Name == tabPage2.Name) // jen součty
                //{
                //    btn_Nacist.Enabled = false;
                //}
                //else
                //{
                //    //Neznamy...
                //}

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        #region FILTRY


        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr();

            filtr.OBJ = tb_SOPNUMBE.Text.Trim();

            filtr.DO_DatumOD = dtp_DO_DatumOd.Value.Date;
            filtr.DO_DatumDO = dtp_DO_DatumDo.Value.Date;

            filtr.OD_DatumOD = dtp_OD_DatumOd.Value.Date;
            filtr.OD_DatumDO = dtp_OD_DatumDo.Value.Date;

            filtr.ZAP_DatumOD = dtp_ZAP_DatumOd.Value.Date;
            filtr.ZAP_DatumDO = dtp_ZAP_DatumDo.Value.Date;

            filtr.DO_DatumDO_Check = dtp_DO_DatumDo.Checked;
            filtr.DO_DatumOD_Check = dtp_DO_DatumOd.Checked;

            filtr.OD_DatumOD_Check = dtp_OD_DatumOd.Checked;
            filtr.OD_DatumDO_Check = dtp_OD_DatumDo.Checked;

            filtr.ZAP_DatumOD_Check = dtp_ZAP_DatumOd.Checked;
            filtr.ZAP_DatumDO_Check = dtp_ZAP_DatumDo.Checked;

            filtr.Firma = tb_Firma.Text.Trim();
            filtr.Kod = tb_Kod.Text.Trim();
            filtr.FormaUhrady = tb_FormaUhrady.Text;

            filtr.Nezaplanovane = cbNezaplanovane.Checked;

            return true;
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

                Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr();
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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                tb_SOPNUMBE.Text = filtr.OBJ;
                dtp_DO_DatumOd.Value = filtr.DO_DatumOD;
                dtp_DO_DatumDo.Value = filtr.DO_DatumDO;

                dtp_OD_DatumOd.Value = filtr.OD_DatumOD;
                dtp_OD_DatumDo.Value = filtr.OD_DatumDO;

                dtp_ZAP_DatumOd.Value = filtr.ZAP_DatumOD;
                dtp_ZAP_DatumDo.Value = filtr.ZAP_DatumDO;

                dtp_DO_DatumOd.Checked = filtr.DO_DatumOD_Check;
                dtp_DO_DatumDo.Checked = filtr.DO_DatumDO_Check;

                dtp_OD_DatumOd.Checked = filtr.OD_DatumOD_Check;
                dtp_OD_DatumDo.Checked = filtr.OD_DatumDO_Check;

                dtp_ZAP_DatumOd.Checked = filtr.ZAP_DatumOD_Check;
                dtp_ZAP_DatumDo.Checked = filtr.ZAP_DatumDO_Check;

                tb_Firma.Text = filtr.Firma;
                tb_Kod.Text = filtr.Kod;
                tb_FormaUhrady.Text = filtr.FormaUhrady;

                cbNezaplanovane.Checked = filtr.Nezaplanovane;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                tb_SOPNUMBE.Text = string.Empty;

                dtp_DO_DatumOd.Value = DateTime.Now;
                dtp_DO_DatumDo.Value = DateTime.Now;
                dtp_OD_DatumOd.Value = DateTime.Now;
                dtp_OD_DatumDo.Value = DateTime.Now;
                dtp_ZAP_DatumOd.Value = DateTime.Now;
                dtp_ZAP_DatumDo.Value = DateTime.Now;


                tb_Firma.Text = string.Empty;
                tb_Kod.Text = string.Empty;
                tb_FormaUhrady.Text = string.Empty;

                dtp_OD_DatumOd.Checked = false;
                dtp_OD_DatumDo.Checked = false;
                dtp_DO_DatumOd.Checked = false;
                dtp_DO_DatumDo.Checked = false;
                dtp_ZAP_DatumOd.Checked = false;
                dtp_ZAP_DatumDo.Checked = false;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            chb_Zapis.Enabled = false;
            chb_Od.Enabled = false;
            chb_Do.Enabled = false;


            if (rb_Zapis.Checked)
                chb_Zapis.Enabled = true;
            else if (rb_Od.Checked)
                chb_Od.Enabled = true;
            else if (rb_Do.Checked)
                chb_Do.Enabled = true;


        }

        private void bwZaplanovani_DoWork(object sender, DoWorkEventArgs e)
        {
            var filter = (Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr)e.Argument;

            var ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

            if ((providerPV_Z != null) && (providerPV_Z is Fask.Interfaces.Vyroba.PV.IPV_GetZaplanovani))
                ds = ((Fask.Interfaces.Vyroba.PV.IPV_GetZaplanovani)providerPV_Z).GetZaplanovani(filter);
            else
                throw new NotImplementedException("Provider neimplementuje IPV_GetZaplanovani.");

            if (ds == null)
            {
                e.Result = null;
                return;
            }

            // --- groupování přesunuto sem (do backgroundu) ---
            ds.PV_Zaplanovani_Hlavicky.Clear();

            var dt_Grup = ds.PV_Zaplanovani.GroupBy(x => new
            {
                OBJ_NMBR = (x.IsOBJ_NMBRNull() ? string.Empty : x.OBJ_NMBR),
                OBJ_DESC = (x.IsOBJ_DESCNull() ? string.Empty : x.OBJ_DESC),
                OBJ_TYPE = (x.IsOBJ_TYPENull() ? string.Empty : x.OBJ_TYPE),
                OBJ_COMPANY = (x.IsOBJ_COMPANYNull() ? string.Empty : x.OBJ_COMPANY),
                OBJ_DATE_FROM = (x.IsOBJ_DATE_FROMNull() ? DateTime.MinValue : x.OBJ_DATE_FROM),
                OBJ_DATE_TO = (x.IsOBJ_DATE_TONull() ? DateTime.MinValue : x.OBJ_DATE_TO),
                OBJ_ORD = (x.IsOBJ_ORDNull() ? -1 : x.OBJ_ORD),
                OBJ_ForUh = (x.IsOBJ_ForUhNull() ? -1 : x.OBJ_ForUh),
                OBJ_ForUh_IDS = (x.IsOBJ_ForUh_IDSNull() ? string.Empty : x.OBJ_ForUh_IDS),
                UserParam_1 = (x.IsUserParam_1Null() ? string.Empty : x.UserParam_1),
                UserParam_2 = (x.IsUserParam_2Null() ? string.Empty : x.UserParam_2),
                OBJ_DATE_ZAPL = (x.IsOBJ_DATE_ZAPLNull() ? DateTime.MinValue : x.OBJ_DATE_ZAPL)
            });

            foreach (var item in dt_Grup)
            {
                var row = ds.PV_Zaplanovani_Hlavicky.NewPV_Zaplanovani_HlavickyRow();

                row.OBJ_NMBR = item.Key.OBJ_NMBR;
                row.OBJ_DESC = item.Key.OBJ_DESC;
                row.OBJ_TYPE = item.Key.OBJ_TYPE;
                row.OBJ_COMPANY = item.Key.OBJ_COMPANY;

                if (item.Key.OBJ_DATE_FROM == DateTime.MinValue)
                    row.SetOBJ_DATE_FROMNull();
                else
                    row.OBJ_DATE_FROM = item.Key.OBJ_DATE_FROM;

                if (item.Key.OBJ_DATE_TO == DateTime.MinValue)
                    row.SetOBJ_DATE_TONull();
                else
                    row.OBJ_DATE_TO = item.Key.OBJ_DATE_TO;

                if (item.Key.OBJ_DATE_ZAPL == DateTime.MinValue)
                    row.SetOBJ_DATE_ZAPLNull();
                else
                    row.OBJ_DATE_ZAPL = item.Key.OBJ_DATE_ZAPL;

                row.OBJ_ORD = item.Key.OBJ_ORD;
                row.QTY = item.Sum(x => x.QTY);

                row.QTY_Zaplanovano = item
                    .Sum(x => Convert.ToDecimal(x.IsQTY_ZaplanovanoNull() ? "0" : x.QTY_Zaplanovano))
                    .ToString();

                row.QTY_Zbyva = item
                    .Sum(x => Convert.ToDecimal(x.IsQTY_ZbyvaNull() ? "0" : x.QTY_Zbyva))
                    .ToString();

                row.OBJ_ForUh = item.Key.OBJ_ForUh;
                row.OBJ_ForUh_IDS = item.Key.OBJ_ForUh_IDS;

                row.UserParam_1 = item.Key.UserParam_1;
                row.UserParam_2 = item.Key.UserParam_2;

                ds.PV_Zaplanovani_Hlavicky.AddPV_Zaplanovani_HlavickyRow(row);
            }

            e.Result = ds;
        }

        private void bwZaplanovani_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                this.Enabled = true;

                if (e.Error != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var ds = e.Result as Fask.Interfaces.DataSets.Vyroba_Planovani;

                if (ds == null)
                {
                    MessageBox.Show(this, "Nastala chyba při otevírání seznamu obchodních požadavkú!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    PerformCancel();
                    return;
                }

                this.ds_PV_Z = ds;

                this.SuspendLayout();
                dg_PV_Z_H.SuspendLayout();
                dg_PV_Z.SuspendLayout();

                // detail
                bs_PV_Z.DataSource = ds_PV_Z;
                bs_PV_Z.DataMember = "PV_Zaplanovani";

                // master
                bs_PV_Z_H.DataSource = ds_PV_Z;
                bs_PV_Z_H.DataMember = "PV_Zaplanovani_Hlavicky";
            }
            finally
            {
                dg_PV_Z.ResumeLayout();
                dg_PV_Z_H.ResumeLayout();
                this.ResumeLayout();
            }
        }

    }
}
