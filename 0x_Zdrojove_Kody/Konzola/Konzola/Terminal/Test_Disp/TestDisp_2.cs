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

namespace Konzola.Terminal
{
    public partial class TestDisp_2 : Form
    {
        private Fask.Interfaces.IMES providerVydej = null;


        //private int? _countEntriesCurrent = null;
        //public int? CountEntriesCurrent
        //{
        //    set { _countEntriesCurrent = value; }
        //    get { return _countEntriesCurrent; }
        //}


        public Fask.Interfaces.DataSets.Vydej.VydejKontrolaRow  SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_OUT.BindingContext[bs_OUT].Current)).Row as Fask.Interfaces.DataSets.Vydej.VydejKontrolaRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public TestDisp_2()
        {
            InitializeComponent();
            this.dg_IN.UpdateColumnHeaderCellsByDatasource();
            this.dg_OUT.UpdateColumnHeaderCellsByDatasource();
        }

        private void TestDisp_2_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                this.dg_IN.LoadConfiguration(this.GetType().ToString());
                this.dg_OUT.LoadConfiguration(this.GetType().ToString());

                advancedDataGridViewSearchToolBar_IN.SetColumns(dg_IN.Columns);
                advancedDataGridViewSearchToolBar_OUT.SetColumns(dg_OUT.Columns);



                // inicializace providera
                InitProvider();

                //RefreshData();

                comboBox1.DataSource = Enum.GetValues(typeof(Fask.Interfaces.Classes.TypZdrojeDat));


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void TestDisp_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_IN.SaveConfiguration(this.GetType().ToString());
                this.dg_OUT.SaveConfiguration(this.GetType().ToString());

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion


        private void RefreshData()
        {
            //Fask.POHODA.Disponibility.ValidateData dsDisp = new Fask.POHODA.Disponibility.ValidateData();
            //Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable DTOut = new Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable();


                Fask.Interfaces.Classes.TypZdrojeDat status; 
                Enum.TryParse<Fask.Interfaces.Classes.TypZdrojeDat>(comboBox1.SelectedValue.ToString(), out status);

                string ce = string.Empty;

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    ce = textBox1.Text;
                }

            

            if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_KontrolaDavky_TEST))
                ((Fask.Interfaces.Vydej.IVydej2_KontrolaDavky_TEST)providerVydej).KontrolaDavky_TEST(ce, out ds_OUT, out ds_IN, status);
            else
                throw new NotImplementedException("Provider neimplementuje IVydej2_KontrolaDavky_TEST.");

            bs_OUT.DataSource = ds_OUT.VydejKontrola;
            bs_IN.DataSource = ds_IN.DataDisp;
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
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            PerformCancel();
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

        private void buttonExportOznacene_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportSelectedRowsVisibleColumnsToExcel(string.Empty);
                this.dg_OUT.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportAllRowsVisibleColumnsToExcel(string.Empty);
                this.dg_OUT.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dg_Vydej_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewRow row in dg_OUT.Rows)
            {

               #region Puvodny
                ////decimal Navrh = decimal.Parse(row.Cells["navrhDataGridViewTextBoxColumn"].Value.ToString().Trim());
                ////decimal Sklad = decimal.Parse(row.Cells["skladDataGridViewTextBoxColumn"].Value.ToString().Trim());

                //decimal zbude = decimal.Parse(row.Cells["zBUDEDataGridViewTextBoxColumn"].Value.ToString().Trim());

                //decimal SKz = decimal.Parse(row.Cells["sTAVSKLADDataGridViewTextBoxColumn"].Value.ToString().Trim());
                //decimal pozadovano = decimal.Parse(row.Cells["qTYSHPPDDataGridViewTextBoxColumn"].Value.ToString().Trim());


                //object tmp = row.Cells["rEZJADataGridViewTextBoxColumn"].Value;
                //decimal? RezJA = null;

                //if (tmp != null)
                //{
                //    string tmpString = tmp.ToString();

                //    if (!string.IsNullOrEmpty(tmpString))
                //    {
                //        RezJA = decimal.Parse(tmpString.Trim());
                //    }
                //}

                //if ((SKz >= 0) && (zbude >= 0) && (SKz >= zbude))
                ////if (SKz >= 0)
                //{
                //    //Tady projde každa položka ktera je disponibilny
                //    row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green

                //    //if (zbude == 0)
                //    if (SKz == 0)
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange

                //}
                //else
                //{
                //    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                //    //info = new InfoValidace("ERR", item);
                //    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                //    //break;
                //} 
                #endregion

                Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow radek = (Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow)((DataRowView)row.DataBoundItem).Row;

                #region 8.1.2020 ZdD + JaS uprava podminek

                if (radek.IsREZ_JANull() || string.IsNullOrEmpty(radek.REZ_JA))
                {

                    if ((radek.STAV_SKLAD >= 0) && (radek.ZBUDE >= 0) && (radek.STAV_SKLAD >= radek.ZBUDE))
                    {
                        //Tady projde každa položka ktera je disponibilny
                        row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green

                        //if (zbude == 0)
                        if (radek.STAV_SKLAD == 0)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange

                    }
                    else
                    {
                        //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                        //info = new InfoValidace("ERR", item);
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                        //break;
                    }

                }
                else
                {
                    if ((radek.STAV_SKLAD >= 0))
                    {
                        //Tady projde každa položka ktera je disponibilny
                        row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green

                        //if (zbude == 0)
                        if (radek.STAV_SKLAD == 0)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange

                    }
                    else
                    {
                        //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                        //info = new InfoValidace("ERR", item);
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                        //break;
                    }
                }

                #endregion


            }
        }

        private void dg_Vydej_SelectionChanged(object sender, EventArgs e)
        {
            this.dg_OUT.ClearSelection();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dg_OUT.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                int ID = SelectedRow.DEX_ROW_ID;

                decimal pocetInt = SelectedRow.ZADAT;
                string pocetStr;

                DialogResult drPocet = Konzola.Forms.InputBox.Show("Editace množství", "Zadejte množství", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericDecimal, false, 0, 0, false, out pocetStr);

                if (drPocet == System.Windows.Forms.DialogResult.Cancel)
                    return;

                decimal Pocet = decimal.Parse(pocetStr);

                if (SelectedRow.QTY_OBJ_Pohoda < Pocet)
                {
                    MessageBox.Show("Je zakázáno přeplnění.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_UpdateSE_QTY_ByDEXROWID))
                    ((Fask.Interfaces.Vydej.IVydej2_UpdateSE_QTY_ByDEXROWID)providerVydej).UpdateSE_QTY_ByDEXROWID(Pocet, ID);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_UpdateSE_QTY_ByDEXROWID.");


                RefreshData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //private void buttonSmazat_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (SelectedRow == null)
        //        {
        //            MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
        //            return;
        //        }


        //        if (dg_Vydej.SelectedRows.Count > 1)
        //        {
        //            MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
        //            return;
        //        }



        //        if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_DeleteSE))
        //            ((Fask.Interfaces.Vydej.IVydej2_DeleteSE)providerVydej).DeleteSE(SelectedRow.DEX_ROW_ID);
        //        else
        //            throw new NotImplementedException("Provider neimplementuje IVydej2_DeleteSE.");


        //        RefreshData();

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshData();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancedDataGridViewSearchToolBar_IN_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_IN.CurrentCell.ColumnIndex + 1 >= dg_IN.ColumnCount;
                bool endrow = dg_IN.CurrentCell.RowIndex + 1 >= dg_IN.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_IN.CurrentCell.ColumnIndex;
                    startRow = dg_IN.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_IN.CurrentCell.ColumnIndex + 1;
                    startRow = dg_IN.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_IN.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_IN.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_IN.CurrentCell = c;

        }

        private void advancedDataGridViewSearchToolBar_OUT_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_OUT.CurrentCell.ColumnIndex + 1 >= dg_OUT.ColumnCount;
                bool endrow = dg_OUT.CurrentCell.RowIndex + 1 >= dg_OUT.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_OUT.CurrentCell.ColumnIndex;
                    startRow = dg_OUT.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_OUT.CurrentCell.ColumnIndex + 1;
                    startRow = dg_OUT.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_OUT.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_OUT.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_OUT.CurrentCell = c;

        }


    }
}
