using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.MST_W.Forms
{
    public partial class SejmiSNHromadneForm : System.Windows.Forms.Form
    {
        //private bool povolitScanner = true;
     
        //private bool scannerOff = false;
        ///// <summary>
        ///// Aktivni scanner
        ///// </summary>
        //public bool ScannerOff
        //{
        //    get { return this.scannerOff; }
        //    set
        //    {
        //        this.scannerOff = value;
        //        // 27.10.2017 JiS -> toto je problematicke ... zapinani a vypinani scanneru pouze pri Load/PerformOK(Cancel,...)
        //        //ScannerStop();
        //        //ScannerStart();
        //    }
        //}

        //private bool _scannerCheckOnly = false;
        //public bool ScannerCheckOnly
        //{
        //    get { return _scannerCheckOnly; }
        //    set
        //    {
        //        _scannerCheckOnly = value;
        //        if (_scannerCheckOnly)
        //        {
        //            //this.kod_tb.ReadOnly = true;
        //            this.ok_but.Enabled = false;
        //            if (scannerOff) ScannerOff = false;
        //        }
        //        else
        //        {
        //            //this.kod_tb.ReadOnly = false;
        //            this.ok_but.Enabled = true;
        //        }
        //    }
        //}

        public SejmiSNHromadneForm() //: this ("", TypeOfCode.AlphaNumeric)
        {
            InitializeComponent();

            tb_Od.Focus();
        }


        delegate void UpdateUIDelegate(string barcode);
        protected virtual void UpdateUI(string barcode)
        {

            if (tb_Od.Focused)
            {
                this.Kod_OD = barcode;
                this.tb_Do.Focus();
            }
            else if (tb_Do.Focused)
            {
                this.Kod_DO = barcode;
                //this.tb_Pocet.Focus();


            }
            //else if (tb_Pocet.Focused)
            //{
            //    this.Kod_Pocet = barcode;
            //    this.tb_Pocet.Focus();
            //}
            
            if (MST_Global.OnScannerSound_Forms)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
            //ScannerStart();
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new UpdateUIDelegate(UpdateUI), new object[] { e.BarcodeData.Trim() });
        }

        public string LabelNazev
        {
            get { return lbl_Nazev.Text.Trim(); }
            set
            {
                lbl_Nazev.Text = value.Trim();
                //lbl_Nazev = lbl_Nazev.Text;
            }
        }

        private decimal _nacteno;
        public decimal Nacteno
        {
            get { return _nacteno; }
            set { _nacteno = value; }
        }

        private decimal _predloha;
        public decimal Predloha
        {
            get { return _predloha; }
            set { _predloha = value; }
        }

        private decimal _zbiva;
        public decimal Zbiva
        {
            get { return _zbiva; }
            set { _zbiva = value; }
        }



        private string _kodPrefix = string.Empty;
        /// <summary>
        /// Hodnota Prefixu
        /// </summary>
        public string KodPrefix
        {
            get { return _kodPrefix.Trim(); }
            set
            {
                _kodPrefix = value.Trim();
                //_kodOD = tb_Od.Text;
            }
        }

        private int _kodOd_Start;
        /// <summary>
        /// Hodnota Prefixu
        /// </summary>
        public int KodOd_Start
        {
            get { return _kodOd_Start; }
            set
            {
                _kodOd_Start = value;
                //_kodOD = tb_Od.Text;
            }
        }

        private string _kodOD = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod_OD
        {
            get { return tb_Od.Text.Trim(); }
            set { tb_Od.Text = value.Trim();
                _kodOD = tb_Od.Text; }
        }


        private string _kodDO = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod_DO
        {
            get { return tb_Do.Text.Trim(); }
            set
            {
                tb_Do.Text = value.Trim();
                _kodDO = tb_Do.Text;
            }
        }

        private string _kod_Pocet = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod_Pocet
        {
            get { return tb_Pocet.Text.Trim(); }
            set
            {
                tb_Pocet.Text = value.Trim();
                _kod_Pocet = tb_Pocet.Text;
            }
        }


        private void SejmiKodForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;
            e.Handled = true;

            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {// stiskl Enter, provede kontrolu kodu
                Vypocitaj();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
                return;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void Vypocitaj()
        {
            Logging.TracId id = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "Vypocitaj");
            Logging.Trace2.Write("Vypoèet", "Start", id); 

            ///Promenna v konfiguraci ktera urcuje pocet v SN od konce
            int N = MST_Global.Vydej_HromadneSN_N;

            string Format = string.Empty;

            for (int i = 0; i < N; i++)
            {
                Format += "0";
            }


            try
            {

                if (string.IsNullOrEmpty(tb_Od.Text))
                {
                    //throw new Exception("Nenalezeno SN Od ktereho se ma poèitat.");
                    throw new Exception("Nenalezeno poèáteèní SN");
                    //return;
                }

                string sub_start;
                try
                {
                    sub_start = Kod_OD.Substring(Kod_OD.Length - N);

                }
                catch (Exception)
                {
                    //throw new Exception("Nesedi zadana do delka N poèet znaku jak numeric");
                    throw new Exception(string.Format("Min. poèet znakù je '{0}'",N ));
                }

                string prefixStart = tb_Od.Text.Substring(0, tb_Od.Text.Length - N);
                KodPrefix = prefixStart;

                int start;
                int end;

                try
                {
                    start = int.Parse(sub_start);
                    KodOd_Start = start;
                }
                catch (Exception ex)
                {
                    //throw new Exception("Konec SN neni int");
                    throw new Exception(string.Format("Posledních '{0}' znakù není èíslo", N));
                    
                }

                if (!string.IsNullOrEmpty(tb_Do.Text))
                {
                    string sub_end;
                    try
                    {
                        sub_end = Kod_DO.Substring(Kod_DO.Length - N);

                    }
                    catch (Exception)
                    {
                        //throw new Exception("Nesedi zadana od delka N poèet znaku jak numeric");
                        throw new Exception(string.Format("Min. poèet znakù je '{0}'", N));
                    }

                    try
                    {
                        end = int.Parse(sub_end);
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception("Konec SN neni int");
                        throw new Exception(string.Format("Posledních {0} znakù není èíslo", N));
                    }


                    
                    string prefixEND = tb_Do.Text.Substring(0, tb_Od.Text.Length - N);

                    if(prefixStart.Trim() != prefixEND.Trim())
                        //throw new Exception("nesedi prefixy SN");
                        throw new Exception("Prefix SN Od a SN Do musí byt totožne");
                    

                    int count = (end - start) + 1;

                    if (count <= 0)
                    {
                        //throw new Exception("Nelze zadat zaporny nebo nulovy poèet SN");
                        throw new Exception("Poèáteèní SN je vìtší než koncové SN");
                    }

                    if (count > _zbiva)
                    {
                        //MessageBoxBig.Show("Zadan vetší poèet generovanych kodu než zbíva vykrit...", "Error", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        Kod_Pocet = string.Empty;
                        //throw new Exception("Zadan vetší poèet generovanych kodu než zbíva vykrit...");
                        throw new Exception("Zadán vìtší poèet než je požadováno");
                    }
                    else
                    {
                        Kod_Pocet = count.ToString();
                    }

                }
                else if (!string.IsNullOrEmpty(tb_Pocet.Text))
                {
                    try
                    {
                        end = int.Parse(tb_Pocet.Text);
                    }
                    catch (Exception ex)
                    { //throw new Exception("pocet neni int"); 
                        throw new Exception(string.Format("Posledních '{0}' znakù není èíslo", N));
                    }

                    if (end == 0)
                    {
                        throw new Exception("Nelze zadat poèet Nula!");
                    }


                    if (end > _zbiva)
                    {
                        //MessageBoxBig.Show("", "Error", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        tb_Pocet.Focus();
                        tb_Pocet.SelectAll();
                        //throw new Exception("Zadan vetší poèet generovanych kodu než zbíva vykrit...");
                        throw new Exception("Zadán vìtší poèet než je požadováno");
                        //return;

                    }
                    
                    int count = (end + start) - 1;

                    Kod_DO = prefixStart + count.ToString(Format);

                }
                else
                {
                    //throw new Exception("neni zadany koncovy dopoèet");
                    throw new Exception("Zadej poèet nebo koncové SN");
                }

                Logging.Trace2.Write("Vypoèet", "End", id);

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
        }

        private void SejmiKodForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            //if(povolitScanner)
                ScannerStart();

                lbl_Pocet.Text = " P " + _predloha.ToString(Settings.UIFormatDesCisel) + "   N " + _nacteno.ToString(Settings.UIFormatDesCisel) + "   Z " + _zbiva.ToString(Settings.UIFormatDesCisel)  + " ";


            panelButtons_Resize(null, null);
        }

        //private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
           // this.scannserstart = false;
        }

        // Jedna se o vice použivany dialog tak nemužeme zakazat start skeneru
        private void ScannerStart()
        {
            //if (!scannserstart)
            //    return;

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

        private void SejmiKodForm_Closing(object sender, CancelEventArgs e)
        {
            //if(povolitScanner)
            ScannerFinalize();
            //Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

     
        private void ok_but_Click(object sender, EventArgs e)
        {
            //if (_scannerCheckOnly) //neumozni potvrzeni enterem...
            //    return;

            if (string.IsNullOrEmpty(tb_Od.Text))
            {
                MessageBoxBig.Show("Nenalezeno SN èislo OD" + Environment.NewLine + "Pro vypoèet stlaète enter.", "Warning", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(tb_Do.Text))
            {
                MessageBoxBig.Show("Nenalezeno SN èislo DO" + Environment.NewLine + "Pro vypoèet stlaète enter.", "Warning", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(tb_Pocet.Text))
            {
                MessageBoxBig.Show("Nenalezeno poèet SN" + Environment.NewLine + "Pro vypoèet stlaète enter.", "Warning", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
                PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        protected virtual void PerformOK()
        {
            //if (povolitScanner)
            this.ScannerFinalize();

            this.DialogResult = DialogResult.OK;
        }

        protected virtual void PerformCancel()
        {
           // if (povolitScanner)
                this.ScannerFinalize();

            this.DialogResult = DialogResult.Cancel;
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }


        protected virtual void finalize()
        {
        }

        private void SejmiSNHromadneForm_Resize(object sender, EventArgs e)
        {
            //this.Height;
            //this.Width;

            //Size s = new Size(this.Width /2, this.Height);
            panelLeft.Size = new Size(this.Width / 2, panelLeft.Height);




        }

    }
}