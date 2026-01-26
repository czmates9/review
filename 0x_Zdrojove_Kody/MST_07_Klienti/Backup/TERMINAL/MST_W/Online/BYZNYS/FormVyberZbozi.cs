using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Fask.MST_W;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;

namespace Fask.MST_W.Online.BYZNYS
{
    public partial class FormVyberZbozi : System.Windows.Forms.Form
    {
        enum Volby { Klic = 1, Nazev, Cislo };

        private Volby volba = Volby.Cislo;
        public DatabaseOnline.SKLADRow ZBOZI_Selected
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[sKLADBindingSource].Current as DataRowView).Row as Fask.MST_W.Online.BYZNYS.DatabaseOnline.SKLADRow;
                }
                catch (SqlException sqlex)
                {
                    Fask.Logging.Log.Write("CHYBA:\n" + sqlex.Message);
                    Fask.MST_W.Forms.MessageBoxBig.Show("CHYBA: " + sqlex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                    return null;
                } 
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex);
                    return null;
                }
            }
        }

        public FormVyberZbozi()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            this.dataGrid1.InitializeTableGridColumnStyles();
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);

            if (PlatformDetection.Platform.IsWinCE() || PlatformDetection.Platform.IsCENET())
            {
                this.mainMenu1.Dispose();
                this.mainMenu1 = null;
            }
            else
            {
                panelButtons.Hide();
            }

            sKLADTableAdapter.Connection.ConnectionString = Settings.Online_BYZNYS_ConnectionString;
            Cursor.Current = Cursors.Default;
        }

        private void Vyhledej()
        {
            t_Hodnota.BackColor = SystemColors.Window;
            if (t_Hodnota.Text.Trim().Length == 0)
            {
                t_Hodnota.BackColor = Color.MistyRose;
                t_Hodnota.Focus();
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rb_Klic_Mat.Checked)
                {
                    sKLADTableAdapter.FillByKlicMa(databaseOnline.SKLAD, Convert.ToInt32(t_Hodnota.Text));
                }
                else if (rb_Nazev.Checked)
                {
                    sKLADTableAdapter.FillByNazevMat(databaseOnline.SKLAD, t_Hodnota.Text);
                }
                else if (rb_Cislo.Checked)
                {
                    sKLADTableAdapter.FillByCisloMat(databaseOnline.SKLAD, t_Hodnota.Text);
                }
                else
                {
                    return;
                }


                tabControl1.SelectedIndex = tabControl1.TabPages.IndexOf(tabPageList);

                dataGrid1.Focus();
                dataGrid1.CurrentRowIndex = 0; //nastavi se na 1. polozku

            }
            catch (System.Data.SqlClient.SqlException sqlex)
            {
                Fask.Logging.Log.Write(sqlex.Message, sqlex.Procedure);
                Cursor.Current = Cursors.Default;
                Fask.MST_W.Forms.MessageBoxBig.Show(sqlex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                Cursor.Current = Cursors.Default;
                Fask.MST_W.Forms.MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void gb_Zpet_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }        

        private void gb_OK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformCancel()
        {
            if (tabControl1.SelectedIndex > 0)
            {
                tabControl1.SelectedIndex = 0;
                activateHodnota();
                return;
            }            
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            Settings.Online_BYZNYS_PartnerHodnotaLast = this.t_Hodnota.Text;
        }

        private void PerformYes()
        {
            finalize();
            DialogResult = DialogResult.Yes; // => vybrano scannerem cislo objednavky
        }

        private void PerformOK()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                Vyhledej();
                return;
            }

            if (this.ZBOZI_Selected == null)
            {
                Fask.MST_W.Forms.MessageBoxBig.Show("Zboží nebylo vybráno!", this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void FormVyberZbozi_KeyDown(object sender, KeyEventArgs e)
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

        private void activateHodnota()
        {
            t_Hodnota.Focus();
            t_Hodnota.SelectAll();
        }

        private void FormVyberZbozi_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Fask.MST_W.Forms.FormLocation.ScreenResolution;
            this.panelButtons_Resize(null, null);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));

            try
            {
                volba = (Volby)(Enum.Parse(typeof(Volby), Settings.Online_BYZNYS_ZboziVolbaLast, true));
            }
            catch { }
            if (volba == Volby.Cislo) rb_Cislo.Checked = true;
            else if (volba == Volby.Klic) rb_Klic_Mat.Checked = true;
            else if (volba == Volby.Nazev) rb_Nazev.Checked = true;

            this.t_Hodnota.Text = Settings.Online_BYZNYS_ZboziHodnotaLast;
            this.t_Hodnota.SelectAll();
            this.t_Hodnota.Focus();

            ScannerStart();
            Cursor.Current = Cursors.Default;
        }

        #region Scanner start stop
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

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }
        delegate void ScannerEventHandlerCall(ScannerEventArgs e);
        private void OnScannerEvent(ScannerEventArgs e)
        {

            try
            {
                ScannerStop();
                string ck = e.BarcodeData.Trim();
                if (ck.Length > 0)
                {
                    t_Hodnota.Text = ck;
                    Vyhledej();
                }

            }
            finally
            {
                ScannerStart();
                if (MST_Global.OnScannerSound_Online)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new ScannerEventHandlerCall(OnScannerEvent), new object[] { e });
        }
        #endregion

        private void rb_GotFocus(object sender, EventArgs e)
        {
            activateHodnota();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            bStorno.Size = bNew;
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                activateHodnota();
            else if (tabControl1.SelectedIndex == 1)
                dataGrid1.Focus();
        }
    }
}