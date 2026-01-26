using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.Data.SQLite;
using System.IO;

namespace Fask.MST_W.ServisModule
{
    public partial class ServisStavyZmena : Form
    {
        /// <summary>
        /// Text, který se má zobrazit ve status baru
        /// </summary>
        public string StatusBarInfoText { get; set; }

        public ServisStavyZmena()
        {
            InitializeComponent();
        }

        public Data.ZdrojeStav.ZdrojStavRow Zdroj
        {
            get;
            set;
        }

        public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavRow StavNext
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavRow;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        private void ServisStavyZmena_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                InitializeGrid();

                // natahnout stavy
                FillStavy();
                ScannerStart();
                panelButtons_Resize(null, null);

                statusBarInfo.Text = StatusBarInfoText != null ? StatusBarInfoText : string.Empty;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }


        private void FillStavy()
        {
            try
            {
                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavNextDataTable dt_stavynext = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_StavNext(Zdroj.StavID);
                List<string> listStavu = new List<string>();

                foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavNextRow snr in dt_stavynext)
                {
                    string sid = "'" + snr.IDNext + "'";
                    if (!listStavu.Contains(sid))
                        listStavu.Add(sid);
                }

				//var stavycommand = Globals.globalObject.Controller_servis_Ciselniky.TaCiselnikStav.Connection.CreateCommand();
				//stavycommand.CommandText = "Select * from CZMST_Servis_Stav " +
				//    "Where ID IN (" + String.Join(",", listStavu.ToArray()) + ")";

				////SqlCeDataAdapter sqlda = new SqlCeDataAdapter(stavycommand);
				//var sqlda = new SQLiteDataAdapter(stavycommand);
				//sqlda.Fill(ds_servis.CZMST_Servis_Stav);

				Globals.globalObject.Controller_servis_Ciselniky.FillByList_Stav(ds_servis.CZMST_Servis_Stav, listStavu);

                // Dotazeni nazvu cinnosti ...
                foreach (var item in ds_servis.CZMST_Servis_Stav)
                {
                    if(!item.IsIDCinnostNull())
						item.CinnostNazev = Globals.globalObject.Controller_servis_Ciselniky.Oznaceni_Cinnost(item.IDCinnost);
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void finalize()
        {
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PerformOK()
        {
            try
            {
                if (StavNext == null)
                {
                    MessageBoxBig.Show("Není vybrán následující stav!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else // potvrzení 
                {
                    //MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "notify.wav"));
                    if (MST_Global.ServisPozadovatPotvrzeniZmenyStavu && DialogResult.No == MessageBoxBig.Show("Následující stav bude\n'" + StavNext.Oznaceni + "'", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, true))
                    {
                        return;
                    }

                    Zdroj.StavID = StavNext.ID;
                    Zdroj.CinnostID = StavNext.IsIDCinnostNull() ? null : StavNext.IDCinnost;
                    Zdroj.ZdrojCinnostType = null;
                    Zdroj.ZdrojCinnostValue = null;
                }
                if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                    MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void ServisStavyZmena_KeyDown(object sender, KeyEventArgs e)
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
            //Char.IsDigit(
            e.Handled = true;
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void menuItemCancel_Click(object sender, EventArgs e)
        {
            PerformCancel();
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

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
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
        /// <param name="kod"></param>
        private void najdipolozku(string kod)
        {
            try
            {
                bsServis.Filter = string.Empty;

                var tables = ds_servis.CZMST_Servis_Stav.Where(z => !z.IsBarcodeNull() && z.Barcode == kod);

                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show("Nenalezen záznam s kódem '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bsServis.Filter = "Barcode='" + kod + "'";
                    MessageBoxBig.Show("Nalezeno více záznamů s kódem '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                { // nalezen prave jeden ...
                    bsServis.Filter = "Barcode='" + kod + "'";
                    PerformOK();
                }
                //dataGrid1.Select(0);
                //DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_ServisModul)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    int number = Convert.ToInt32(e.KeyChar.ToString());
                    dataGrid1.CurrentRowIndex = number - 1;
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if(DialogResult.No == MessageBoxBig.Show("Opravdu se chcete vrátit o krok zpět?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
                return;

                finalize();
                this.DialogResult = DialogResult.Retry;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {

        }

        private void ok_but_Click(object sender, EventArgs e)
        {

        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {
            PerformCancel();
        }

    }
}