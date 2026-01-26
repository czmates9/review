using Fask.Logging;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;
using FASK.SledovaniVyroby.ModuleIfc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    public partial class frmPosledniPaleta : Form, IModuleConnector
    {


        #region Parametry IN


        public string EAN
        {
            set
            {
                lb_EAN.Text = string.Format("EAN: {0}", value);
            }
        }

        public string ITEMDESC
        {
            set
            {
                lb_ITEMDESC.Text = string.Format("Název výrobku: {0}", value);
            }
        }

        public string VPP_pol
        {
            set
            {
                lb_VPP_pol.Text = string.Format("Pol. VP: {0}", value);
            }
        }

        public string VPH_SOPNUMBE
        {
            set
            {
                lb_VPH_SOPNUMBE.Text = string.Format("Číslo VP: {0}", value);
            }
        }

        private decimal _qtypack = 0;
        public decimal VPP_QTYPACK
        {
            set
            {
                _qtypack = value;
            }
        }

        private DataVyroba _parent;

        #endregion

        #region Parametry OUT


        public int QTY_PytluNaPalete
        {
            get
            {
                try
                {
                    return int.Parse(tb_pocetPytluNaPalete.Text);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    return 0;
                }
            }
        }

        public int QTY_Palet
        {
            get
            {
                try
                {
                    return int.Parse(tb_pocetPalet.Text);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    return 0;
                }
            }
        }

        public string PackType
        {
            get 
            {
                try
                {
                    return button1.Text;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    return string.Empty;
                }
            }
        }

        #endregion

        private bool _flag_PO_PredcasnyOdjezd = false;
        public bool Flag_PO_PredcasnyOdjezd
        {
            get { return _flag_PO_PredcasnyOdjezd; }
            set { _flag_PO_PredcasnyOdjezd = value; }
        }

        private System.Collections.Generic.Stack<Classes.DataVyroba.VyrobaStavy> _stavPoslednaPaletaStack = new Stack<Classes.DataVyroba.VyrobaStavy>();

        private void StavVyrobaStackInitialize()
        {
            _stavPoslednaPaletaStack.Clear();
            _stavPoslednaPaletaStack.Push(Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety);
        }

        private Classes.DataVyroba.VyrobaStavy stav
        {
            //get { return stav; }
            //set
            //{
            //    stav = value;
            //    keyboardUC1.vyroba_ZmenaStavu(stav);
            //}

            get
            {
                return StavVyrobaStackGet();
            }
            set
            {
                var novyStav = value;
                this.StavVyrobaStackSet(novyStav);
                keyboardUC1.vyroba_ZmenaStavu(novyStav);
            }
        }

        /// <summary>
        /// Vraci aktualni stav na vrchu zasobniku
        /// </summary>
        /// <returns></returns>
        private Classes.DataVyroba.VyrobaStavy StavVyrobaStackGet()
        {
            if ((_stavPoslednaPaletaStack == null) || (_stavPoslednaPaletaStack.Count == 0))
                return Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety;
            return _stavPoslednaPaletaStack.Peek();
        }

        private void StavVyrobaStackSet(Classes.DataVyroba.VyrobaStavy novyStav)
        {
            //if (!_stavVyrobaStack.Contains(novyStav))
            //{
            //    _stavVyrobaStack.Push(novyStav);
            //}

            if (novyStav == Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety)
            { // na main byse mel nastavit jen v pripade, ze je vse vyreseno ... 
                if (!_stavPoslednaPaletaStack.Contains(Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety))
                { // problem a log error
                    //Exceptions.Handler.ErrorHandle("_stavVyrobaStack prechod do stavu Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety, ale Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety neexistuje : ", "StavVyrobaStackSetStav", false);
                    ExceptionHandler2.Handle("_stavVyrobaStack prechod do stavu Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety, ale Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety neexistuje : ", "StavVyrobaStackSetStav", false);
                }

                // pokud nastavuji main, tak vse by melo se vratit na zacatek do main a main bude zase prvni...
                StavVyrobaStackInitialize(); // toto vycistni a nastavi main jako prvni...
            }
            else
            {
                if (!_stavPoslednaPaletaStack.Contains(novyStav))
                {
                    _stavPoslednaPaletaStack.Push(novyStav);
                }
            }
        }

        #region Eventy formu

        public frmPosledniPaleta(DataVyroba dv)
        {
            InitializeComponent();
            _parent = dv;
        }

        private void frmPosledniPaleta_Load(object sender, EventArgs e)
        {
            keyboardUC1.btnF6.Text = "";

            keyboardUC1.ButtonClick += new EventHandler(KeyboardUC_ButtonClick);
            //keyboardUC1.vyroba_ZmenaStavu(Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Pytle);

            stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Pytle;

            tb_pocetPytluNaPalete.Focus();
            tb_pocetPytluNaPalete.BackColor = Color.LightGreen;
        } 

        #endregion

        #region IModuleConnector Members


        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }

        public void ClosePorts()
        {
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;
            return true;
        }

        public void ReturnPortsToPreviousState()
        {
        }

        #endregion

        void KeyboardUC_ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;
                this.BeginInvoke((MethodInvoker)delegate () { VyrobaButtonClick(btn); });
            }
        }

        #region Stavovy automat

        void VyrobaButtonClick(Button btn)
        {
            try
            {
                switch (stav)
                {
                    case Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Pytle:
                        this.StavPosledniPalety_Pytle(btn);
                        break;
                    case Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety:
                        this.StavPosledniPalety_Palety(btn);
                        break;
                    case Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_PotvrzeniPalety_NP:
                        this.StavPosledniPalety_PotvrzeniPalety_NP(btn);
                        break;
                    case Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_PotvrzeniPalety_UP:
                        this.StavPosledniPalety_PotvrzeniPalety_UP(btn);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
               // Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "KeyboardUC_ButtonClick", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "KeyboardUC_ButtonClick", false);

            }
        }

  
        private void StavPosledniPalety_Pytle(Button btn)
        {
            try
            {
                if (button1.Text == "UP")
                    tb_pocetPytluNaPalete.Text = "0";
                else
                    NumKeyPressedCheck(btn, tb_pocetPytluNaPalete, 6);

                if (btn.Name == "BtnEnter") // Další
                {

                    if (button1.Text == "NP")
                    {
                        if (_qtypack != 0)
                        {
                            if (string.IsNullOrEmpty(tb_pocetPytluNaPalete.Text))
                            {
                                label1.Text = "Musíte zadat počet pytlu!";
                                label1.ForeColor = Color.Red;
                                return;
                            }
                            else if (decimal.Parse(tb_pocetPytluNaPalete.Text) >= _qtypack)
                            {
                                label1.Text = "Musíte zadat počet pytlu menší jak: " + _qtypack.ToString();
                                label1.ForeColor = Color.Red;
                                return;
                            }
                            else if (decimal.Parse(tb_pocetPytluNaPalete.Text) == 0)
                            {
                                label1.Text = "Musíte zadat počet pytlu větší jak 0";
                                label1.ForeColor = Color.Red;
                                return;
                            }
                            else
                            {
                                label1.Text = "\"Poslední paleta\"";
                                label1.ForeColor = Color.Black;
                            }
                        } 
                    }

                    tb_pocetPalet.BackColor = Color.LightGreen;
                    tb_pocetPytluNaPalete.BackColor = Color.FromArgb(255, 255, 255, 255);
                    stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety;
                }
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
               // Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Pytle", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Pytle", false);
            }
        }

        private void StavPosledniPalety_Palety(Button btn)
        {
            try
            {
                NumKeyPressedCheck(btn, tb_pocetPalet, 6);

                if (btn.Name == "btn19") // Předchozí
                {
                    tb_pocetPytluNaPalete.BackColor = Color.LightGreen;
                    tb_pocetPalet.BackColor = Color.FromArgb(255, 255, 255, 255);
                    stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Pytle;
                }
                if (btn.Name == "BtnEnter") // Další
                {

                        if (string.IsNullOrEmpty(tb_pocetPalet.Text))
                        {
                            label1.Text = "Musíte zadat počet palet!";
                            label1.ForeColor = Color.Red;
                            return;
                        }

                    
                    if (button1.Text == "NP")
                    {
                        tb_pocetPytluNaPalete.BackColor = Color.FromArgb(255, 255, 255, 255);
                        tb_pocetPalet.BackColor = Color.FromArgb(255, 255, 255, 255);
                        label1.Text = "\"??? Aktivovat odjezd NP  ???\"";
                        label1.ForeColor = Color.Red;

                        stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_PotvrzeniPalety_NP;
                    }
                    if (button1.Text == "UP")
                    {
                        tb_pocetPytluNaPalete.BackColor = Color.FromArgb(255, 255, 255, 255);
                        tb_pocetPalet.BackColor = Color.FromArgb(255, 255, 255, 255);
                        label1.Text = "\"!!! Odešla poslední paleta? !!!\"";
                        label1.ForeColor = Color.Red;

                        stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_PotvrzeniPalety_UP;
                    }

                }
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
                //Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Palety", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Palety", false);
            }
        }


        private void StavPosledniPalety_PotvrzeniPalety_NP(Button btn)
        {
            try
            {
                NumKeyPressedCheck(btn, tb_pocetPalet, 6);

                if (btn.Name == "btn19") // Předchozí
                {
                    tb_pocetPalet.BackColor = Color.LightGreen;
                    tb_pocetPytluNaPalete.BackColor = Color.FromArgb(255, 255, 255, 255);
                    label1.Text = "\"Poslední paleta\"";
                    label1.ForeColor = Color.Black;
                    stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety;
                }
                if (btn.Name == "BtnEnter") // Ano aktivovat odjezd NP
                {
                    if (!ValidateData())
                        return;

                    DialogResult = DialogResult.No;

                }
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
                //Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Palety", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Palety", false);
            }
        }

        private void StavPosledniPalety_PotvrzeniPalety_UP(Button btn)
        {
            try
            {
                NumKeyPressedCheck(btn, tb_pocetPalet, 6);

                if (btn.Name == "btn19") // Předchozí
                {
                    tb_pocetPalet.BackColor = Color.LightGreen;
                    tb_pocetPytluNaPalete.BackColor = Color.FromArgb(255, 255, 255, 255);
                    label1.Text = "\"Poslední paleta\"";
                    label1.ForeColor = Color.Black;
                    stav = Classes.DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety;
                }
                if (btn.Name == "BtnEnter") // Ano
                {
                    if (!ValidateData())
                        return;

                    DialogResult = DialogResult.Yes;

                }
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
                //Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Palety", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavPosledniPalety_Palety", false);
            }
        }


        #endregion

        #region Vstup z klavesnice

        internal void NumKeyPressedCheck(Button btn, TextBox tb, int maxTextLength)
        {
            // nastavi maximalni delku pro vstup do textoveho pole informacniho panelu
            //this.InformationUC.TxtTextMaxLength = maxTextLength;

            //TextBox tb = null;

            //if (Flag_pocetPytluNaPalete)
            //    tb = tb_pocetPytluNaPalete;
            //else if (Flag_pocetPalet)
            //    tb = tb_pocetPalet;

            if (btn == null)
                return;

            if (btn.Name == "button13")
            {
                if (tb.Text.Length > 0)
                    tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
            }
            else if (btn.Name == "button11")
            {
                tb.Text = "";
            }
            else if (tb.Text.Length >= maxTextLength)
                return;
            else if (btn.Name == "button12")
                tb.Text += "0";
            else if (btn.Name == "button1")
                tb.Text += btn.Text;
            else if (btn.Name == "button2")
                tb.Text += btn.Text;
            else if (btn.Name == "button3")
                tb.Text += btn.Text;
            else if (btn.Name == "button4")
                tb.Text += btn.Text;
            else if (btn.Name == "button5")
                tb.Text += btn.Text;
            else if (btn.Name == "button6")
                tb.Text += btn.Text;
            else if (btn.Name == "button7")
                tb.Text += btn.Text;
            else if (btn.Name == "button8")
                tb.Text += btn.Text;
            else if (btn.Name == "button9")
                tb.Text += btn.Text;

        } 

        #endregion

        #region Validace zadanych dat

        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(tb_pocetPalet.Text.Trim()))
                {
                    errorProvider1.SetError(tb_pocetPalet, "Musíte zadat počet");
                }

                if (string.IsNullOrEmpty(tb_pocetPytluNaPalete.Text.Trim()))
                {
                    errorProvider1.SetError(tb_pocetPytluNaPalete, "Musíte zadat počet");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (stav == DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety || stav == DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Pytle)
                {
                    if (button1.Text == "NP")
                    {
                        button1.Text = "UP";
                        button1.BackColor = Color.LimeGreen;
                        tb_pocetPytluNaPalete.Text = "0";
                    }
                    else if (button1.Text == "UP")
                    {
                        button1.Text = "NP";
                        button1.BackColor = Color.Tomato;
                        tb_pocetPytluNaPalete.Text = string.Empty;
                    } 
                }
            }
            catch (Exception ex)
            {
                //ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        public bool IsReadyToShow(out string message)
        {
            //throw new NotImplementedException();
            message = "ok";
            return true;
        }

    }
}
