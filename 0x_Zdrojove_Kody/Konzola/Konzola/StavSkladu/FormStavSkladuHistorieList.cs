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

namespace Konzola.StavSkladu
{
    /// <summary>
    /// MD Let
    /// </summary>
    public partial class FormStavSkladuHistorieList : Form
    {
        private Fask.Interfaces.IMES providerStavSkladu = null;
        private Color buttonBackColor = Color.FromKnownColor(KnownColor.ActiveCaption); //Color.Aqua;

        /// <summary>
        /// Zvoleny uzivatel v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.StavSkladu.CZMSTPWDRow rowUzivatel
        {
            get
            {
                try
                {
                    return comboBoxUzivatel.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.CZMSTPWDRow;
                }
                catch
                {
                    return null;
                }
            }
        }

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
        /// Zvolena zdrojova lokace v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialLOCNCODESRT
        {
            get
            {
                try
                {
                    return comboBoxMaterialLOCNCODESRC.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.get_os_msRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolena cilova lokace v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialLOCNCODEDST
        {
            get
            {
                try
                {
                    return comboBoxMaterialLOCNCODEDST.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.get_os_msRow;
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
        private Fask.Interfaces.DataSets.StavSkladu.CZMST093Row rowMaterialSKLIDSRC
        {
            get
            {
                try
                {
                    return comboBoxMaterialSKLIDSRC.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.CZMST093Row;
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
        private Fask.Interfaces.DataSets.StavSkladu.CZMST093Row rowMaterialSKLIDDST
        {
            get
            {
                try
                {
                    return comboBoxMaterialSKLIDDST.SelectedItem as Fask.Interfaces.DataSets.StavSkladu.CZMST093Row;
                }
                catch
                {
                    return null;
                }
            }
        }


        public FormStavSkladuHistorieList()
        {
            InitializeComponent();
            this.dgStavSkladu.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormStavSkladuHistorieList_Load(object sender, EventArgs e)
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

        private void FormStavSkladuHistorieList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormStavSkladuHistorieList_FormClosing(object sender, FormClosingEventArgs e)
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


        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.StavSkladu ds = new Fask.Interfaces.DataSets.StavSkladu();

                // naplneni comboboxu uzivatelu
                //((Fask.Interfaces.StavSkladu.IStavSkladu)providerStavSkladu).getUzivatele(ref ds);

                if ((providerStavSkladu != null) && (providerStavSkladu is Fask.Interfaces.StavSkladu.IStavSkladu2_getUzivatele))
                    ((Fask.Interfaces.StavSkladu.IStavSkladu2_getUzivatele)providerStavSkladu).getUzivatele(ref ds);
                else
                    throw new NotImplementedException("Provider neimplementuje IStavSkladu2_getUzivatele.");

                // naplneni zbylych comboboxu
                //((Fask.Interfaces.StavSkladu.IStavSkladu)providerStavSkladu).GetStavSkladu(ref ds);

                if ((providerStavSkladu != null) && (providerStavSkladu is Fask.Interfaces.StavSkladu.IStavSkladu2_GetStavSkladu))
                    ((Fask.Interfaces.StavSkladu.IStavSkladu2_GetStavSkladu)providerStavSkladu).GetStavSkladu(ref ds);
                else
                    throw new NotImplementedException("Provider neimplementuje IStavSkladu2_GetStavSkladu.");

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
                comboBoxUzivatel.Items.AddRange(ds.CZMSTPWD.Select(null, "SECONDNAME asc"));
                comboBoxUzivatel.SelectedItem = null;

                // naplneni comboboxu itemnmbr
                var dtMaterialID = ds.get_os_ms.GroupBy(g => g.ITEMNMBR).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                // trim nazvu
                foreach (var item in dtMaterialID)
                {
                    item.ITEMDESC = item.ITEMDESC.Trim();
                }
                comboBoxMaterialID.DataSource = dtMaterialID;
                comboBoxMaterialID.ValueMember = "ITEMDESC";
                comboBoxMaterialID.SelectedItem = null;

                // naplneni lokaci
                var dtMaterialLOCNCODE = ds.get_os_ms.GroupBy(g => g.LOCNCODE).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                var dtMaterialLOCNCODE2 = ds.get_os_ms.GroupBy(g => g.LOCNCODE).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                foreach (var item in dtMaterialLOCNCODE)
                {
                    item.LOCNCODE = item.LOCNCODE.Trim();
                }
                comboBoxMaterialLOCNCODESRC.DataSource = dtMaterialLOCNCODE;
                comboBoxMaterialLOCNCODESRC.ValueMember = "LOCNCODE";
                comboBoxMaterialLOCNCODESRC.SelectedItem = null;

                // naplneni cilove lokace                
                foreach (var item in dtMaterialLOCNCODE2)
                {
                    item.LOCNCODE = item.LOCNCODE.Trim();
                }
                comboBoxMaterialLOCNCODEDST.DataSource = dtMaterialLOCNCODE2;
                comboBoxMaterialLOCNCODEDST.ValueMember = "LOCNCODE";
                comboBoxMaterialLOCNCODEDST.SelectedItem = null;

                // naplneni zdrojoveho skladu
                //providerStavSkladu.GetSklady(ref dsStavSkladu);
                comboBoxMaterialSKLIDSRC.Items.AddRange(ds.CZMST093.Select(null, "skl_desc asc"));
                comboBoxMaterialSKLIDSRC.SelectedItem = null;

                // naplneni ciloveho skladu
                comboBoxMaterialSKLIDDST.Items.AddRange(ds.CZMST093.Select(null, "skl_desc asc"));
                comboBoxMaterialSKLIDDST.SelectedItem = null;
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
                DataTable dtchanged = this.dsStavSkladu.get_os_ms_hist.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;

                }

                Fask.Interfaces.Filtry.StavSkladuHistorieFiltr filtr = new Fask.Interfaces.Filtry.StavSkladuHistorieFiltr();
                filtr.rowUzivatel = rowUzivatel;
                filtr.UzivatelID = comboBoxUzivatel.Text;
                filtr.rowMaterialID = rowMaterialID;
                filtr.MaterialID = comboBoxMaterialID.Text;
                filtr.rowMaterialLOCNCODESRC = rowMaterialLOCNCODESRT;
                filtr.MaterialLocncodeSRC = comboBoxMaterialLOCNCODESRC.Text;
                filtr.rowMaterialLOCNCODEDST = rowMaterialLOCNCODEDST;
                filtr.MaterialLocncodeDST = comboBoxMaterialLOCNCODEDST.Text;
                filtr.rowMaterialSKLIDSRC = rowMaterialSKLIDSRC;
                filtr.MaterialSKLIDSRC = comboBoxMaterialSKLIDSRC.Text;
                filtr.rowMaterialSKLIDDST = rowMaterialSKLIDDST;
                filtr.MaterialSKLIDDSTPresnaShoda = cbMaterialSKLIDDSTPresnaShoda.Checked;
                filtr.MaterialSKLIDDST = comboBoxMaterialSKLIDDST.Text;
                filtr.MaterialSERLTNUM = comboBoxMaterialSERLTNUM.Text;
                filtr.DatumOd = dateTimePickerDatumOd.Checked ? dateTimePickerDatumOd.Value : (DateTime?)null;
                filtr.DatumDo = dateTimePickerDatumDo.Checked ? dateTimePickerDatumDo.Value : (DateTime?)null;
                
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

                //((Fask.Interfaces.StavSkladu.IStavSkladu)providerStavSkladu).GetFiltrovanyStavSkladuHistorie(filtr, ref dsStavSkladu);

                if ((providerStavSkladu != null) && (providerStavSkladu is Fask.Interfaces.StavSkladu.IStavSkladu2_GetFiltrovanyStavSkladuHistorie))
                    ((Fask.Interfaces.StavSkladu.IStavSkladu2_GetFiltrovanyStavSkladuHistorie)providerStavSkladu).GetFiltrovanyStavSkladuHistorie(filtr, ref dsStavSkladu);
                else
                    throw new NotImplementedException("Provider neimplementuje IStavSkladu2_GetFiltrovanyStavSkladuHistorie.");


                this.dsStavSkladu.get_os_ms_hist.EndLoadData();
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
