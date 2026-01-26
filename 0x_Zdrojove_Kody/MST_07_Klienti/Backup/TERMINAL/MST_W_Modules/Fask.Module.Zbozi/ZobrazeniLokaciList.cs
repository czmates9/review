using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.Module.Zbozi
{
    public partial class ZobrazeniLokaciList : Form
    {
        // nactena data z online metody
        private WebServiceLokace.LokaceService wsLokace = null;
        private WebServiceLokace.Location ds = new WebServiceLokace.Location();
        private DataSets.Zbozi.CZMST095Row zbozi = null;

        private BindingSource bs;

        public ZobrazeniLokaciList(DataSets.Zbozi.CZMST095Row _zbozi)
        {
            InitializeComponent(); 
            try
            {
                this.zbozi = _zbozi;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, this.GetType() + ".const()");
            }
        }

        /// <summary>
        /// Uzivatelem vybrana lokace.
        /// </summary>
        public WebServiceLokace.Location.CZMST_SkladLokace_MapaRow PrijmovaLokace { get; set; }

        // vybrany radek v seznamu doporucenych lokaci
        private WebServiceLokace.Location.CZMST_SkladLokace_StavRow _vybranaLokace
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as WebServiceLokace.Location.CZMST_SkladLokace_StavRow;
                }
                catch (Exception ex)
                {
					Fask.Logging.Log.Write(ex);
                    return null;
                }
            }
        }

        private void ZobrazeniLokaciList_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;

                CreateGridStyles();

                InitializeGrid();

                ScannerStart();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }

            try
            {
                //// online nacteni dat
                //PerformUpdate();
                System.Threading.Timer timer = new System.Threading.Timer(
                    new System.Threading.TimerCallback(PerformUpdateTimer),
                    null,
                    200,
                    System.Threading.Timeout.Infinite
                    );
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = ds.CZMST_SkladLokace_Stav.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Položka Č.";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.ITEMNMBRColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Lokace";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.LOCNCODEColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Množství";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.QTYSHPPDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Sklad ID";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.SKL_IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);
            

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles();
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, 10, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(ListPolozek.ConfigDir, this.GetType().ToString()));
        }

        private void ZobrazeniLokaciList_KeyDown(object sender, KeyEventArgs e)
        {
            // enter zatim nic neudela ...
            //if (e.KeyCode == Keys.Enter)
            //{
            //    //PerformOK();
            //    PerformCancel();
            //}
            //else 
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.F1)
            {
                miNajitLokaci_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                miAktualizovat_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            finalize(); 
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(ListPolozek.ConfigDir, this.GetType().ToString()));
        }

        #region scanner
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return;
            }
            try
            {
                Globals.Scanner.Enable();
            }
            catch 
            {                
            }
        }

        private void ScannerStop()
        {
            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch
            {
            }
            try
            {
                Globals.Scanner.Disable();
            }
            catch
            {
            }
        }

        delegate void DelegateString(string kod);
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                if (kod.Length <= 0)
                    return;

                this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod">barcode</param>
        private void najdipolozku(string kod)
        {
            try
            {
                bs.Filter = string.Empty;
                string selectcmd = "LOCNCODE = '" + kod + "'";
                var tables = ds.CZMST_SkladLokace_Stav.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show("Lokace '" + kod + "' nebyla nalezena v seznamu.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                    MessageBoxBig.Show(string.Format("Nalezeno více lokací pro kód '{0}'", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            zpet_but_Click(null, null);
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private WebServiceLokace.Location OnlineGetLokace(DataSets.Zbozi.CZMST095Row _zbozi)
        {
            WebServiceLokace.Location lokace;
            try
            {
                wsLokace = new WebServiceLokace.LokaceService();
                wsLokace.Url = Globals.ServerAddress + "Lokace.asmx";
                wsLokace.Timeout = Globals.ServerTimeout;
                // TODO : dalsi parametry ... ??? credentials ...
                //wsLokace.UpdateWebServiceCredentials();

                // TODO: konfiguracne pocet zaznamu ...

				//TaD 19.3.2019 Pridano TRUE pro parameter show empty, kde je pridana do SQL dotazu podminka  (ShowEmpty ? string.Empty : "and QTYSHPPD<>0 ")  na strane serveru

                lokace = wsLokace.ShowMaterial(
					_zbozi.ITEMNMBR, 
					string.Empty, 
					_zbozi.IsSKL_IDNull() ? string.Empty : _zbozi.SKL_ID, 
					null,
					true);  
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Vydej.VydejZobrazeniLokaciList, OnlineGetPrijmoveLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
            return lokace;
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Vydej.VydejZobrazeniLokaciList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private delegate void MethodInvoker();
        private void PerformUpdateTimer(object status)
        {
            try
            {
                this.BeginInvoke(
            (MethodInvoker)delegate() { PerformUpdate(); }
            );

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //ds.CZMST_SkladLokace_Mapa.Clear();
                ds = OnlineGetLokace(this.zbozi);
                if (ds != null)
                {
                    bs = new BindingSource();
                    bs.DataSource = ds.CZMST_SkladLokace_Stav;
                    dataGrid1.DataSource = bs;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení lokací se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            try
            {
                dataGrid1.Focus();
                dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
            }
            catch
            {
            }
        }

        string lastlookupLokace = string.Empty;
        private void miNajitLokaci_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = string.Empty;
                DialogResult dlgRes = InputBox.Show("Najít lokaci:", lastlookupLokace.Trim(), out kod);
                if (dlgRes == DialogResult.Cancel)
                    return;
                lastlookupLokace = kod;

                najdipolozku(kod);                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Vydej.VydejZobrazeniLokaciList, Najit lokaci");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }
    }
}