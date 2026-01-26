using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.Module.MTJ.JimiTore.Baleni
{
    public partial class VyberObjednavkyList : Form
    {
        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni wsBaleni = null;
        private Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej dsVydej = new Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej();
        private BindingSource bs;
        // typ dokladu CZMST092.doc_id

        public VyberObjednavkyList()
        {
            InitializeComponent();
            Globals.Configuration = new Fask.Module.MTJ.JimiTore.Baleni.DataSets.Configuration();
            try
            {
                if (System.IO.File.Exists(Globals.ConfigurationFile))
                    Globals.Configuration.ReadXml(Globals.ConfigurationFile);

            }
            catch
            {
            }

            try
            {
                wsBaleni = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni();
                wsBaleni.Url = Globals.ServerAddress + "Baleni.asmx";
                wsBaleni.Timeout = Globals.ServerTimeout;


                bs = new BindingSource();
                bs.DataSource = dsVydej.Objednavka;
                dataGrid1.DataSource = bs;

                AktualizovatObjednavky();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "VyberObjednavkyList load");
            }
        }

        public Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ObjednavkaRow objednavkaRow
        {
            get
            {
                try
                {
                    //return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as SqlCEDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableRow;
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ObjednavkaRow;
                    //return null;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            //Fask.Localization.LocalizationExtensionForm.Localize(this);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;

            CreateGridStyles();

            InitializeGrid();

            ScannerStart();
            panelButtons_Resize(null, null);

            dataGrid1.Focus();
            try
            {
                dataGrid1.CurrentRowIndex = 0;
            }
            catch
            {
            }            
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsVydej.Objednavka.TableName; // ds_servis.CZMST_Servis_Dynamic_Table.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Objednávka"; 
            dg.MappingName = dsVydej.Objednavka.SOPNUMBEColumn.ColumnName; 
            dg.NullText = "-";
            dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Název";
            dg.MappingName = dsVydej.Objednavka.ITEMDESCColumn.ColumnName; 
            dg.NullText = "-";
            dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Č.k. "; 
            dg.MappingName = dsVydej.Objednavka.CZ_CarKodColumn.ColumnName; 
            dg.NullText = "-";
            dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            // TODO: dodelat ...
            //this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            //this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));
        }

        private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
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

            this.dataGrid1.Save(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));
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
                //Logging.Log.Write(ex);
                //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
                return;
            }
            EnableScanner();
        }

        public void EnableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Enable();
        }
        public void DisableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Disable();
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
                DisableScanner();
            }
            catch
            {
            }
        }


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            findCode(barcode);
        }

        private void findCode(string barcode)
        {
            try
            {
                ScannerStop();

                bs.Filter = string.Empty;
                string selectcmd = "CZ_CarKod = '" + barcode + "'";
                var tables = dsVydej.Objednavka.Select(selectcmd);

                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show(string.Format("Záznam s kódem '{0}' nebyl nalezen!", barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bs.Filter = "CZ_CarKod='" + barcode + "'";
                    MessageBoxBig.Show(string.Format("Nalezeno více záznamů s šarží '{0}'", barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs.Filter = "CZ_CarKod='" + barcode + "'";
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }
        #endregion scanner

        

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (objednavkaRow == null)
                {
                    MessageBoxBig.Show("", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                finalize();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void miDynamickaTabulka_Click(object sender, EventArgs e)
        {
        }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore OnlineGetObjednavky()
        {
            try
            {
                //Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni
                return wsBaleni.GetObjednavky();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Baleni.VyberObjednavkyList, OnlineGetObjednavky");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            AktualizovatObjednavky();
        }

        private void AktualizovatObjednavky()
        {
            try
            {
                Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore objednavky = OnlineGetObjednavky();
                if (objednavky != null)
                {
                    dsVydej.Objednavka.Clear();
                    foreach (Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ObjednavkaRow objednavka in objednavky.Objednavka)
                    {
                        Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ObjednavkaRow row = dsVydej.Objednavka.NewObjednavkaRow();
                        row.SOPNUMBE = objednavka.SOPNUMBE;
                        row.CZ_CarKod = objednavka.IsCZ_CarKodNull() ? null : objednavka.CZ_CarKod;
                        row.ITEMDESC = objednavka.IsITEMDESCNull() ? null : objednavka.ITEMDESC;

                        dsVydej.Objednavka.AddObjednavkaRow(row);
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Baleni.VyberObjednavkyList, AktualizovatObjednavky");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }
    }
}