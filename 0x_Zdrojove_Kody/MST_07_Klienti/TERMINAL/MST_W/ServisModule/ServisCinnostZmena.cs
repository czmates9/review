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
    public partial class ServisCinnostZmena : Form
    {
        private bool pridatUkonceniStavu { get; set; }

        /// <summary>
        /// Text, který se má zobrazit ve status baru
        /// </summary>
        public string StatusBarInfoText { get; set; }

        public ServisCinnostZmena()
        {
            InitializeComponent();
        }

        public Data.ZdrojeStav.ZdrojStavRow Zdroj
        {
            get;
            set;
        }

        private Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow _CinnostNext = null;
        public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow CinnostNext
        {
            get { return _CinnostNext; }
            set { _CinnostNext = value; }
        }

        private Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow CinnostSelected
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow;

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    return null;
                }
            }
        }

        private void ServisCinnostZmena_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                InitializeGrid();

                // natahnout stavy
                FillCinnosti();
                ScannerStart();
                if (ds_servis.CZMST_Servis_Cinnost.Count == 0)
                { // nejsou zadne dalsi cinnosti => novy stav
                    PerformOK(null);
                }
                else if (ds_servis.CZMST_Servis_Cinnost.Count == 1)
                {
                    PerformOK(ds_servis.CZMST_Servis_Cinnost[0]);
                }
                else
                { // necha se vyber ...
                    // prednastavi cinnost, ktera byla posledni ... nebo ktera ma nasledovat???
                }
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


        private void FillCinnosti()
        {
            try
            {
                // TODO: Upraveno, ZdrojCinnostValue v jiných případech nesmí být stejná jako ID z dynamické tabulky typu návěsu
                // puvodni
                //SqlCEDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable dt_cinnostinext = Globals._taCiselnikCinnostNext.GetDataByID(Zdroj.CinnostID);

                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable dt_cinnostinext = null;

                //// najití všech činností podle ID
                //dt_cinnostinext = Globals._taCiselnikCinnostNext.GetDataByID(Zdroj.CinnostID);

                //// najití, zdali je vyplněná IDValue a následné vyhledání podle ní
                //var tables = dt_cinnostinext.Where(z => !z.IsIDValueNull() && z.IDValue == Zdroj.ZdrojCinnostValue);
                //if ((tables != null) && (tables.Count() > 0))
                //{
                //    dt_cinnostinext = Globals._taCiselnikCinnostNext.GetDataByIDAndIDValue(Zdroj.CinnostID, Zdroj.ZdrojCinnostValue);
                //}

				dt_cinnostinext = Globals.globalObject.Controller_servis_Ciselniky.GetDataByIDAndIDValue_CinnostNext(Zdroj.CinnostID, Zdroj.ZdrojCinnostValue);
                if(dt_cinnostinext != null && dt_cinnostinext.Count == 0)
					dt_cinnostinext = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_CinnostNext(Zdroj.CinnostID);

                List<string> list = new List<string>();                    

                // načtení veškerých cinnostIDNext
                foreach (var cnr in dt_cinnostinext)
                {
                    // DB null zde
                    if (!cnr.IsIDNextNull())
                    {
                        string cid = "'" + cnr.IDNext + "'";
                        if (!list.Contains(cid))
                            list.Add(cid);
                    }
                    else pridatUkonceniStavu = true;
                }
                // načtení všech činností, které se mají zobrazit
                if (list.Count > 0)
                {
					//var sqlcommand = Globals.globalObject.Controller_servis_Ciselniky.TaCiselnikCinnost.Connection.CreateCommand();
					//sqlcommand.CommandText = "Select * from CZMST_Servis_Cinnost " +
					//    "Where ID IN (" + String.Join(",", list.ToArray()) + ")";

					//var sqlda = new SQLiteDataAdapter(sqlcommand);
					//sqlda.Fill(ds_servis.CZMST_Servis_Cinnost);

					Globals.globalObject.Controller_servis_Ciselniky.FillByList_Cinnost(ds_servis.CZMST_Servis_Cinnost, list);
                }

                if (pridatUkonceniStavu)
                {
                    ds_servis.CZMST_Servis_Cinnost.Rows.Add(string.Empty, Fask.Localization.Localization.ServisServisCinnostZmenaDalsiStav, string.Empty, string.Empty, string.Empty, 0, 0);
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
                if (CinnostSelected == null)
                {
                    MessageBoxBig.Show("Není vybrána následující činnost!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else // potvrzení 
                {
                    if (pridatUkonceniStavu && (CinnostSelected.ID == ""))
                    {
                        if (MST_Global.ServisPozadovatPotvrzeniZmenyCinnosti && DialogResult.No == MessageBoxBig.Show("Přechod do dalšího stavu?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (MST_Global.ServisPozadovatPotvrzeniZmenyCinnosti && DialogResult.No == MessageBoxBig.Show("Následující činnost bude \n'" + CinnostSelected.Oznaceni + "'", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                        {
                            return;
                        }
                    }

                }

                PerformOK(CinnostSelected);
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
            }

        }

        private void PerformOK(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnostNext)
        {
            try
            {
                if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                    MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

                // pokud je zvolena činnost určující přechod do dalšího stavu
                if (pridatUkonceniStavu && (cinnostNext.ID == ""))
                    CinnostNext = null;
                else
                    CinnostNext = cinnostNext;

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

        private void ServisCinnostZmena_KeyDown(object sender, KeyEventArgs e)
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

                var tables = ds_servis.CZMST_Servis_Cinnost.Where(z => !z.IsBarcodeNull() && z.Barcode == kod);

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

        private void menuItem2_Click(object sender, EventArgs e)
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

        private void miScannerReactivate_Click(object sender, EventArgs e)
        {
            try
            {
                // zalogovani vsech pridelenych udalosti ... ???
                if (Program.mstw.Scanner == null)
                {
                    Logging.Log.Write("1) Application mstw.Scanner is null...");
                }
                Program.mstw.Scanner.Log_DataReady_Events();


                // Pokus o znovu aktivaci scanneru ...
                ScannerStop();
                scannserstart = true;
                ScannerStart();

                // zalogovani vsech pridelenych udalosti ... ???
                if (Program.mstw.Scanner == null)
                {
                    Logging.Log.Write("2) Application mstw.Scanner is null...");
                }
                Program.mstw.Scanner.Log_DataReady_Events();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, "Scanner reactivate", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

    }
}