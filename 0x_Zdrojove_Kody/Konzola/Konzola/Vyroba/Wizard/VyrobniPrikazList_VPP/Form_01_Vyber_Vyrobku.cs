using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Konzola.Extensions;
using Fask.Logging;
using System.Reflection;

namespace Konzola.Vyroba.Wizard.VyrobniPrikazList_VPP
{
    public partial class Form_01_Vyber_Vyrobku : Form
    {

        private Fask.Interfaces.IMES providerVPP = null;

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow _VPHrow { set; get; }
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable _VPPdt { set; get; }

        #region Eventy formu


        public Form_01_Vyber_Vyrobku()
        {
            InitializeComponent();

            this.dgVyrobkyProPridani.UpdateColumnHeaderCellsByDatasource();
        }

        private void Form_01_Vyber_Vyrobku_Load(object sender, EventArgs e)
        {
            this.dgVyrobkyProPridani.LoadConfiguration(this.GetType().ToString() + "_01");

            advancedDataGridViewSearchToolBar1.SetColumns(dgVyrobkyProPridani.Columns);

            InitProvider();

            if (providerVPP == null)
                throw new Exception("Provider 'VPP' není inicializován");

            Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();
            // TODO: This line of code loads data into the 'dsVyrobkyProPridani.FASK_ZASOBY' table. You can move, or remove it, as needed.
            this.fASK_ZASOBYTableAdapter.Connection = new System.Data.SqlClient.SqlConnection( Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
            this.fASK_ZASOBYTableAdapter.Fill(this.dsVyrobkyProPridani.FASK_ZASOBY);

            // po natazeni nastavit u vsech polozek barcodep na hodnotu vnditnum defaultne ...
            this.dsVyrobkyProPridani.FASK_ZASOBY.ToList().ForEach(
                x =>
                {
                    x.VNDITNUM = x.VNDITNUM.Trim();
                });

            foreach (var i in _VPPdt)
            {
                var polozky = this.dsVyrobkyProPridani.FASK_ZASOBY.Where(x => x.ITEMNMBR.Trim() == i.ITEMNMBR.Trim() && x.VNDITNUM.Trim() == i.VNDITNUM.Trim());
                if (polozky.Count() > 0)
                    polozky.ToList().ForEach(x => {
                        x.Vyrobit = i.QTYSHPPD;
                        x.TIMEMODE = i.TIMEMODE;
                        x.TIMEPREP = i.TIMEPREP;
                        x.TIMEUNIT = i.TIMEUNIT;
                        x.BarcodeP = i.BarcodeP.Trim();
                    });
            }

            this.dsVyrobkyProPridani.TypSledovaniCasu.AddTypSledovaniCasuRow("Stop výroby", 0);
            this.dsVyrobkyProPridani.TypSledovaniCasu.AddTypSledovaniCasuRow("Start/Stop výroby", 1);
            this.dsVyrobkyProPridani.TypSledovaniCasu.AddTypSledovaniCasuRow("Start/Stop příprava, start/stop výroba", 2);

            this.toolStripStatusLabel_Error.Text = string.Empty;
        }

        private void Form_01_Vyber_Vyrobku_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dgVyrobkyProPridani.SaveConfiguration(this.GetType().ToString() + "_01");
        }

        private void Form_01_Vyber_Vyrobku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }

        #endregion

        #region Init Providers
        
        private void InitProvider()
        {
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

        #region DataGridView metody
        
        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgVyrobkyProPridani.CurrentCell.ColumnIndex + 1 >= dgVyrobkyProPridani.ColumnCount;
                bool endrow = dgVyrobkyProPridani.CurrentCell.RowIndex + 1 >= dgVyrobkyProPridani.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVyrobkyProPridani.CurrentCell.ColumnIndex;
                    startRow = dgVyrobkyProPridani.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVyrobkyProPridani.CurrentCell.ColumnIndex + 1;
                    startRow = dgVyrobkyProPridani.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVyrobkyProPridani.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVyrobkyProPridani.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVyrobkyProPridani.CurrentCell = c;





        }

        #endregion

        #region tsmi event click

        private void tsmi_Zrusit_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmi_PotvrditVyber_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        #endregion

        #region Perform Metody

        private void PerformCancel()
        {
            if (DialogResult.No == MessageBox.Show(this, "Zrušit hromadné vložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void PerformOK()
        {
            try
            {
                foreach (var dtZbozi in this.dsVyrobkyProPridani.FASK_ZASOBY.Where(x => !x.IsVyrobitNull()))
                {
                    var polozky = _VPPdt.Where(x => x.ITEMNMBR.Trim() == dtZbozi.ITEMNMBR.Trim() && x.VNDITNUM.Trim() == dtZbozi.VNDITNUM.Trim());
                    if (polozky.Count() > 0)
                    { // aktualizovat vyrobit
                        polozky.ToList().ForEach(x => {
                            x.QTYSHPPD = dtZbozi.Vyrobit;
                            x.TIMEMODE = dtZbozi.TIMEMODE;
                            x.TIMEPREP = (float)dtZbozi.TIMEPREP;
                            x.TIMEUNIT = (float)dtZbozi.TIMEUNIT;
                            x.BarcodeP = dtZbozi.BarcodeP.Trim();
                        });
                    }
                    else
                    { // vlozit novy zaznam ...
                        var rowVPH = _VPHrow;
                        var newRow = _VPPdt.NewCZPRO_VPPRow();
                        newRow.CountEntries = rowVPH.CountEntries;
                        newRow.SOPNUMBE = rowVPH.SOPNUMBE;
                        newRow.ITEMNMBR = dtZbozi.ITEMNMBR.Trim();      // id zboží
                        newRow.ITEMTYPE = string.Empty;
                        newRow.ITEMDESC = dtZbozi.ITEMDESC.Trim();      // popis zboží
                        newRow.ITEMMJ = dtZbozi.MJ.Trim();
                        newRow.SetVNDDOCNMPNull();
                        newRow.VNDITNUM = dtZbozi.VNDITNUM.Trim();
                        newRow.ORD = 1;
                        newRow.BarcodeP = dtZbozi.BarcodeP.Trim();        // čár. kód zboží
                        newRow.LOCNCODE = string.Empty;
                        newRow.QTYSHPPD = dtZbozi.Vyrobit;
                        newRow.QTYDOKON = 0;
                        newRow.QTYPACK = dtZbozi.QTYPACK;
                        newRow.QTYPACKMJ = string.Empty;
                        newRow.TIMEMODE = dtZbozi.TIMEMODE;
                        newRow.TIMEPREP = (float)dtZbozi.TIMEPREP;
                        newRow.TIMEUNIT = (float)dtZbozi.TIMEUNIT;
                        newRow.DtProdT = 0;
                        newRow.DtProdL = 0;
                        newRow.SerNumT = 0;
                        newRow.SerNumL = 0;
                        newRow.VerT = 0;
                        newRow.VerL = 0;
                        newRow.TermID = 0;
                        newRow.LSTMod = DateTime.Now;
                        _VPPdt.AddCZPRO_VPPRow(newRow);

                    }
                }

                if (providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Update)
                    ((Fask.Interfaces.Vyroba.VPP.IVPP_Update)providerVPP).VPP_Update(_VPPdt);
                else
                    throw new Exception("IVPP_Update not implementet");

                DialogResult = System.Windows.Forms.DialogResult.OK;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region tstb_CarkodFilter

        private void tstb_CarkodFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tstb_Mnozstvi.SelectAll();
                tstb_Mnozstvi.Focus();
            }
            else
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }

        private void tstb_CarkodFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string ck = tstb_CarkodFilter.Text.Trim();
                bsVyrobkyProPridani.Filter =
                    "VNDITNUM like '" + ck + "%' " +
                    "OR CZ_CarKod like '" + ck + "%' " +
                    "OR BarcodeP like '" + ck + "%' " +
                    "";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel_Error.Text = ex.Message;
            }
        }

        #endregion

        #region tstb_Mnozstvi

        private void tstb_Mnozstvi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tsb_VyrobitUpdate_Click(null, null);
            }
            else
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;

        }

        #endregion

        #region tsb_VyrobitUpdate

        private void tsb_VyrobitUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var radek = ((bsVyrobkyProPridani.Current as DataRowView).Row as dsVyrobkyProPridani.FASK_ZASOBYRow);
                var vyrobit = decimal.Parse(tstb_Mnozstvi.Text);

                if (radek.IsVyrobitNull())
                    radek.Vyrobit = vyrobit;
                else
                    radek.Vyrobit += vyrobit;

                tstb_CarkodFilter.SelectAll();
                tstb_CarkodFilter.Focus();
            }
            catch (Exception ex)
            {
                toolStripStatusLabel_Error.Text = ex.Message;
            }
        } 

        #endregion
    }
}
