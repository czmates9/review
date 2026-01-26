using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Module.MTJ.JimiTore.Baleni.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni.Kontrola
{
    public partial class NactenePolozkyList : Form
    {
        private string objednavka = string.Empty;
        private Color? backgroundColor = null;

        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni wsBaleni = null;

        public Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.X425_CTRLRow zboziRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsKontrola].Current)).Row as Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.X425_CTRLRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public NactenePolozkyList(string objednavka, Color backgroundColor)
        {
            try
            {
                InitializeComponent();

                this.objednavka = objednavka;

                UpdateStatusBar();

                Globals.Configuration = new Fask.Module.MTJ.JimiTore.Baleni.DataSets.Configuration();
                
                if (System.IO.File.Exists(Globals.ConfigurationFile))
                    Globals.Configuration.ReadXml(Globals.ConfigurationFile);

                if (Globals.Configuration.PovolitPodbarveniTlacitek)
                {
                    zpet_but.BackColor = backgroundColor;
                    this.backgroundColor = backgroundColor;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "NactenePolozkyList");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }

            try
            {
                wsBaleni = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni();
                wsBaleni.Url = Globals.ServerAddress + "Baleni.asmx";
                wsBaleni.Timeout = Globals.ServerTimeout;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "NactenePolozkyList load");
            }
        }

        private void UpdateStatusBar()
        {
            try
            {
                this.sbinfo.Text = "OBJ: " + objednavka;
            }
            catch (Exception ex)
            {
                this.sbinfo.Text = ex.Message;
            }
        }
       
        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            try
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

                //bsKontrola.DataSource = dsKontrola.Zbozi;
                //dataGrid1.DataSource = bsKontrola;

                dataGrid1.Focus();
                try
                {
                    dataGrid1.CurrentRowIndex = 0;
                }
                catch
                {
                }

                // nacteni zaznamu
                timerLoad.Enabled = true;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsKontrola.X425_CTRL.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Položka č.";
            dg.MappingName = dsKontrola.X425_CTRL.MAT_IDColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);
            
            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Plán. množství";
            dg.MappingName = dsKontrola.X425_CTRL.MN_PLANColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Vydané množství";
            dg.MappingName = dsKontrola.X425_CTRL.MN_VYDEJColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Název";
            dg.MappingName = dsKontrola.X425_CTRL.NAZEV_MATColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Globals.UIFormatDesCisel);
            // 8, 10
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Globals.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));
        }

        private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    PerformOK();
            //}
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            //else if (e.KeyCode == Keys.Back)
            //{
            //    PerformDelete();
            //}
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
            try
            {
                DialogResult dr = MessageBoxBig.Show("Opravdu chcete ukončit kontrolu?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                if (dr != DialogResult.Yes)
                    return;

                finalize();
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PerformCancel");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
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
            catch (Exception)
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

                //PerformPridatPolozku(barcode);
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
            //PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            //Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            //ok_but.Size = nsize;
        }

        private void miSmazat_Click(object sender, EventArgs e)
        {
            //PerformDelete();
        }

        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore OnlineGetPolozky()
        {
            try
            {
                return wsBaleni.Kontrola_GetZbozi(Globals.UserLogin, Globals.TermID, objednavka);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                Logging.Log.Write(ex.Message, "PrijemZbytku.NactenePolozkyList, OnlineGetPolozky");

                return null;
            }
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Kontrola.NactenePolozkyList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dsKontrola = OnlineGetPolozky();
                if (dsKontrola != null)
                {
                    bsKontrola = new BindingSource();
                    bsKontrola.DataSource = dsKontrola.X425_CTRL;
                    dataGrid1.DataSource = bsKontrola;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení záznamů se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení záznamů se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
    }
}