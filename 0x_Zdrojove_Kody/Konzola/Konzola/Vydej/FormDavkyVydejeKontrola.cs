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

namespace Konzola.Vydej
{
    public partial class FormDavkyVydejeKontrola : Form
    {
        private Fask.Interfaces.IMES providerVydej = null;


        private int _countEntriesCurrent;
        public int CountEntriesCurrent
        {
            set { _countEntriesCurrent = value; }
            get { return _countEntriesCurrent; }
        }


        public Fask.Interfaces.DataSets.Vydej.VydejKontrolaRow  SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_Vydej.BindingContext[bs_Vydej].Current)).Row as Fask.Interfaces.DataSets.Vydej.VydejKontrolaRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormDavkyVydejeKontrola()
        {
            InitializeComponent();

            this.dg_Vydej.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormDavkyVydejeKontrola_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                this.dg_Vydej.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dg_Vydej.Columns);

                // inicializace providera
                InitProvider();

                RefreshData();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void FormDavkyVydejeKontrola_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_Vydej.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());


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
            if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_KontrolaDavky))
                this.ds_Vydej = ((Fask.Interfaces.Vydej.IVydej2_KontrolaDavky)providerVydej).KontrolaDavky(this.CountEntriesCurrent);
            else
                throw new NotImplementedException("Provider neimplementuje IVydej2_KontrolaDavky.");

            bs_Vydej.DataSource = ds_Vydej.VydejKontrola;
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



        private void dg_Vydej_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewRow row in dg_Vydej.Rows)
            {

                //decimal Navrh = decimal.Parse(row.Cells["navrhDataGridViewTextBoxColumn"].Value.ToString().Trim());
                //decimal Sklad = decimal.Parse(row.Cells["skladDataGridViewTextBoxColumn"].Value.ToString().Trim());

                decimal zbude = decimal.Parse(row.Cells["zBUDEDataGridViewTextBoxColumn"].Value.ToString().Trim());

                decimal SKz = decimal.Parse(row.Cells["sTAVSKLADDataGridViewTextBoxColumn"].Value.ToString().Trim());
                decimal pozadovano = decimal.Parse(row.Cells["qTYSHPPDDataGridViewTextBoxColumn"].Value.ToString().Trim());


                object tmp = row.Cells["rEZJADataGridViewTextBoxColumn"].Value;
                decimal? RezJA = null;

                if (tmp != null)
                {
                    string tmpString = tmp.ToString();

                    if (!string.IsNullOrEmpty(tmpString))
                    {
                        RezJA = decimal.Parse(tmpString.Trim());
                    }
                }


                #region 8.7.2019 TaD Old
                //if (RezJA.HasValue)
                //{

       

                //    if (pozadovano > SKz)
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                //    else if (pozadovano == SKz)
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange
                //    else
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green
                //}
                //else
                //{
                //    if (zbude < 0)
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                //    else if (zbude == 0)
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange
                //    else
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green
                //} 
                #endregion

                #region  8.7.2019 TaD New

                //if ((SKz >= 0) && (zbude >= 0) && (SKz >= zbude))
                //{
                //    //tady projde každa položka ktera je disponibilny
                //    row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green

                //    //if (zbude == 0)
                //    if (SKz == 0)
                //        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange

                //}
                //else
                //{
                //    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                //    //info = new infovalidace("err", item);
                //    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                //    //break;
                //}

                #endregion

                #region  5.11.2019 JaS podminka , 26.11.2019 Uprava podminky spet na puvodnu


                if ((SKz >= 0) && (zbude >= 0) && (SKz >= zbude))
                //if (SKz >= 0)
                {
                    //Tady projde každa položka ktera je disponibilny
                    row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green

                    //if (zbude == 0)
                    if (SKz == 0)
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange

                }
                else
                {
                    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                    //info = new InfoValidace("ERR", item);
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                    //break;
                }

                #endregion

            }

        }

        private void dg_Vydej_SelectionChanged(object sender, EventArgs e)
        {
            this.dg_Vydej.ClearSelection();
        }

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_Vydej.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_Vydej.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_Vydej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_Vydej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_Vydej.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_Vydej.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiAktualizovat_Click(object sender, EventArgs e)
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

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            try
            {

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dg_Vydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }



                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_DeleteSE))
                    ((Fask.Interfaces.Vydej.IVydej2_DeleteSE)providerVydej).DeleteSE(SelectedRow.DEX_ROW_ID);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_DeleteSE.");


                RefreshData();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiUpravitMnozstvi_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dg_Vydej.SelectedRows.Count > 1)
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

                if (Pocet < 0)
                {
                    MessageBox.Show("Je zakázána záporná hodnota.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].PredlohaKontrolaPreplneni)
                {
                    if (SelectedRow.QTY_OBJ_Pohoda < Pocet)
                    {
                        MessageBox.Show("Je zakázáno přeplnění.", this.Text, MessageBoxButtons.OK);
                        return;
                    }
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

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_Vydej.CurrentCell.ColumnIndex + 1 >= dg_Vydej.ColumnCount;
                bool endrow = dg_Vydej.CurrentCell.RowIndex + 1 >= dg_Vydej.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_Vydej.CurrentCell.ColumnIndex;
                    startRow = dg_Vydej.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_Vydej.CurrentCell.ColumnIndex + 1;
                    startRow = dg_Vydej.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_Vydej.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_Vydej.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_Vydej.CurrentCell = c;



        }

    }
}
