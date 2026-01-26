using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using FirebirdSql.Data.FirebirdClient;
using Konzola.Extensions;
using System.Reflection;

namespace Konzola.StavSkladu
{
    /// <summary>
    /// MD Let
    /// </summary>
    public partial class FormStavSkladuList : Form
    {
        private Fask.Interfaces.IMES providerStavSkladu = null;
        private Color buttonBackColor = Color.FromKnownColor(KnownColor.ActiveCaption); //Color.Aqua;

        /// <summary>
        /// Zvolene ID materialu v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialID
        {
            get
            {
                try
                {
                    return comboBoxMaterialID.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.get_os_msRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolena lokace v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialLOCNCODE
        {
            get
            {
                try
                {
                    return comboBoxMaterialLOCNCODE.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.get_os_msRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvoleny sklad v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.StavSkladu.CZMST093Row rowMaterialSKLID
        {
            get
            {
                try
                {
                    return comboBoxMaterialSKLID.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.CZMST093Row;
                }
                catch
                {
                    return null;
                }
            }
        }


        public FormStavSkladuList()
        {
            InitializeComponent();
            this.dgStavSkladu.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormStavSkladuList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgStavSkladu.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgStavSkladu.Columns);


                // inicializace providera
                InitProvider();

                if (providerStavSkladu == null)
                    throw new Exception("Provider není inicializován");

                System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                loadThread.IsBackground = true;
                loadThread.Start();

                //Fask.Interfaces.DataSets.StavSkladu ds = new Fask.Interfaces.DataSets.StavSkladu();
                //providerStavSkladu.GetStavSkladu(ref ds);
                //// naplneni skladu
                //providerStavSkladu.GetSklady(ref ds);

                //// naplneni comboboxu ITEMDESC
                //var dtMaterialID = ds.get_os_ms.GroupBy(g => g.ITEMNMBR).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                //foreach (var item in dtMaterialID)
                //{
                //    item.ITEMDESC = item.ITEMDESC.Trim();
                //}
                //comboBoxMaterialID.DataSource = dtMaterialID;
                //comboBoxMaterialID.ValueMember = "ITEMDESC";
                //comboBoxMaterialID.SelectedItem = null;

                //// naplneni lokaci
                //var dtMaterialLOCNCODE = ds.get_os_ms.GroupBy(g => g.LOCNCODE).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                //foreach (var item in dtMaterialLOCNCODE)
                //{
                //    item.LOCNCODE = item.LOCNCODE.Trim();
                //}
                //comboBoxMaterialLOCNCODE.DataSource = dtMaterialLOCNCODE;
                //comboBoxMaterialLOCNCODE.ValueMember = "LOCNCODE";
                //comboBoxMaterialLOCNCODE.SelectedItem = null;
                
                //comboBoxMaterialSKLID.Items.AddRange(ds.CZMST093.Select(null, "skl_desc asc"));
                //comboBoxMaterialSKLID.SelectedItem = null;

                // nastavení času
                dateTimePickerDatumDo.Value = dateTimePickerDatumOd.Value = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormStavSkladuList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormStavSkladuList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgStavSkladu.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// nacteni dat v jinem vlakne
        /// </summary>
        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.StavSkladu ds = new Fask.Interfaces.DataSets.StavSkladu();
                
                //((Fask.Interfaces.StavSkladu.IStavSkladu)providerStavSkladu).GetStavSkladu(ref ds);

                if ((providerStavSkladu != null) && (providerStavSkladu is Fask.Interfaces.StavSkladu.IStavSkladu2_GetStavSkladu))
                    ((Fask.Interfaces.StavSkladu.IStavSkladu2_GetStavSkladu)providerStavSkladu).GetStavSkladu(ref ds);
                else
                    throw new NotImplementedException("Provider neimplementuje IStavSkladu2_GetStavSkladu.");

                // naplneni skladu
                //((Fask.Interfaces.StavSkladu.IStavSkladu)providerStavSkladu).GetSklady(ref ds);

                if ((providerStavSkladu != null) && (providerStavSkladu is Fask.Interfaces.StavSkladu.IStavSkladu2_GetSklady))
                    ((Fask.Interfaces.StavSkladu.IStavSkladu2_GetSklady)providerStavSkladu).GetSklady(ref ds);
                else
                    throw new NotImplementedException("Provider neimplementuje IStavSkladu2_GetSklady.");

                // navrat do hlavniho vlakna
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        PopulateUI(ds);
                    }));
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
        }

        private void PopulateUI(Fask.Interfaces.DataSets.StavSkladu ds)
        {
            try
            {
                // naplneni comboboxu ITEMDESC
                var dtMaterialID = ds.get_os_ms.GroupBy(g => g.ITEMNMBR).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                foreach (var item in dtMaterialID)
                {
                    item.ITEMDESC = item.ITEMDESC.Trim();
                }
                comboBoxMaterialID.DataSource = dtMaterialID;
                comboBoxMaterialID.ValueMember = "ITEMDESC";
                comboBoxMaterialID.SelectedItem = null;

                // naplneni lokaci
                var dtMaterialLOCNCODE = ds.get_os_ms.GroupBy(g => g.LOCNCODE).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                foreach (var item in dtMaterialLOCNCODE)
                {
                    item.LOCNCODE = item.LOCNCODE.Trim();
                }
                comboBoxMaterialLOCNCODE.DataSource = dtMaterialLOCNCODE;
                comboBoxMaterialLOCNCODE.ValueMember = "LOCNCODE";
                comboBoxMaterialLOCNCODE.SelectedItem = null;

                comboBoxMaterialSKLID.Items.AddRange(ds.CZMST093.Select(null, "skl_desc asc"));
                comboBoxMaterialSKLID.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
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
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerStavSkladu == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.StavSkladu.IStavSkladu2).IsAssignableFrom(t))
                                {
                                    providerStavSkladu = (Fask.Interfaces.StavSkladu.IStavSkladu2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerStavSkladu != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerStavSkladu.InitProvider();

                    
                }
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

        private void PerformOK()
        {
            try
            {
                if (providerStavSkladu == null)
                    throw new Exception("Provider není inicializován");

                DataTable dtchanged = this.dsStavSkladu.get_os_ms.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                // nacteni dat, podle kterych se bude filtrovat
                Fask.Interfaces.Filtry.StavSkladuListFiltr filtr = new Fask.Interfaces.Filtry.StavSkladuListFiltr();
                filtr.rowMaterialID = rowMaterialID;
                filtr.MaterialID = comboBoxMaterialID.Text;
                filtr.rowMaterialLOCNCODE = rowMaterialLOCNCODE;
                filtr.MaterialLocncode = comboBoxMaterialLOCNCODE.Text;
                filtr.rowMaterialSKLID = rowMaterialSKLID;
                filtr.MaterialSKLID = comboBoxMaterialSKLID.Text;
                filtr.MaterialSERLTNUM = comboBoxMaterialSERLTNUM.Text;
                filtr.ExpiraceOd = dateTimePickerDatumOd.Checked ? dateTimePickerDatumOd.Value : (DateTime?) null;
                filtr.ExpiraceDo = dateTimePickerDatumDo.Checked ? dateTimePickerDatumDo.Value : (DateTime?) null;
                decimal mnozstvi;
                // zadane mnozstvi je cislo
                if (!string.IsNullOrEmpty(tbMnozstvi.Text.Trim()))
                {
                    if (Decimal.TryParse(tbMnozstvi.Text, out mnozstvi))
                    {
                        filtr.Mnozstvi = mnozstvi;
                        filtr.MnozstviMensi = (btnQtySmaller.BackColor == buttonBackColor) ? true : false;
                        filtr.MnozstviVetsi = (btnQtyBigger.BackColor == buttonBackColor) ? true : false;
                        filtr.MnozstviRovno = (btnQtyEquals.BackColor == buttonBackColor) ? true : false;
                    }
                    else
                    {
                        tbMnozstvi.Focus();
                        tbMnozstvi.SelectAll();
                        throw new Exception("Chybný formát zadaného množství");
                        //filtr.Mnozstvi = null;
                    }
                }
                else
                    filtr.Mnozstvi = null;

                int FirstDisplayedScrollingRowIndex = this.dgStavSkladu.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                //naplnim data ...
                //((Fask.Interfaces.StavSkladu.IStavSkladu)providerStavSkladu).GetFiltrovanyStavSkladu(filtr, ref this.dsStavSkladu);

                if ((providerStavSkladu != null) && (providerStavSkladu is Fask.Interfaces.StavSkladu.IStavSkladu2_GetFiltrovanyStavSkladu))
                    ((Fask.Interfaces.StavSkladu.IStavSkladu2_GetFiltrovanyStavSkladu)providerStavSkladu).GetFiltrovanyStavSkladu(filtr, ref this.dsStavSkladu);
                else
                    throw new NotImplementedException("Provider neimplementuje IStavSkladu2_GetFiltrovanyStavSkladu.");


                //this.dsStavSkladu.get_os_ms.EndLoadData();
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgStavSkladu.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgStavSkladu.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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

        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgStavSkladu.SelectAll();
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
                this.dgStavSkladu.ClearSelection();
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

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void btnQtySmaller_Click(object sender, EventArgs e)
        {
            btnQtySmaller.BackColor = (btnQtySmaller.BackColor == buttonBackColor) ? default(Color) : buttonBackColor;
        }

        private void btnQtyEquals_Click(object sender, EventArgs e)
        {
            btnQtyEquals.BackColor = (btnQtyEquals.BackColor == buttonBackColor) ? default(Color) : buttonBackColor;
        }

        private void btnQtyBigger_Click(object sender, EventArgs e)
        {
            btnQtyBigger.BackColor = (btnQtyBigger.BackColor == buttonBackColor) ? default(Color) : buttonBackColor;
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgStavSkladu.CurrentCell.ColumnIndex + 1 >= dgStavSkladu.ColumnCount;
                bool endrow = dgStavSkladu.CurrentCell.RowIndex + 1 >= dgStavSkladu.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgStavSkladu.CurrentCell.ColumnIndex;
                    startRow = dgStavSkladu.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgStavSkladu.CurrentCell.ColumnIndex + 1;
                    startRow = dgStavSkladu.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgStavSkladu.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgStavSkladu.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgStavSkladu.CurrentCell = c;
        }

    }
}
