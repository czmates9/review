using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes;
using FASK.SledovaniVyroby.ErrorLog;
using Vyroba_Agro_Modbus;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus
{
    public partial class InformationUC : UserControl
    {
        [Browsable(false)]
        public static InfoForm infoForm = null;

        public void TxtTextFocus()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { this.TxtTextFocus(); }));
                return;
            }

            this.txtText.Focus();
        }

        [Browsable(false)]
        public string TxtText
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.TxtText = value; }));
                    return;
                }

                this.txtText.Text = value;
            }
            get
            {
                return this.txtText.Text;
            }
        }

        [Browsable(false)]
        public char TxtTextPasswordChar
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.TxtTextPasswordChar = value; }));
                    return;
                }

                this.txtText.PasswordChar = value;
            }
            get
            {
                return this.txtText.PasswordChar;
            }
        }

        [Browsable(false)]
        public int TxtTextMaxLength
        {
            get { return this.txtText.MaxLength; }
            set
            {
                if (this.txtText.MaxLength != value)
                {
                    this.txtText.MaxLength = value;
                }
            }
        }


        [Browsable(false)]
        public string LblText
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.LblText = value; }));
                    return;
                }

                this.lblText.Text = value;
            }
            get
            {
                return this.lblText.Text;
            }
        }

        [Browsable(false)]
        public string LblWarning
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.LblWarning = value; }));
                    return;
                }

                this.lblWarning.Text = value;
            }
            get
            {
                return this.lblWarning.Text;
            }
        }

        [Browsable(false)]
        public string LblSarze
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.LblSarze = value; }));
                    return;
                }

                this.lblSarze.Text = value;
            }
            get
            {
                return this.lblSarze.Text;
            }
        }

        [Browsable(false)]
        public string LblVyrPrikaz
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.LblVyrPrikaz = value; }));
                    return;
                }

                this.lblVyrPrikazVPH.Text = value;
            }
            get
            {
                return this.lblVyrPrikazVPH.Text;
            }
        }

        [Browsable(false)]
        public string LblPolozkaVyrPrikaz
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { this.LblPolozkaVyrPrikaz = value; }));
                    return;
                }

                this.lblVyrPrikazVPP.Text = value;
            }
            get
            {
                return this.lblVyrPrikazVPP.Text;
            }
        }


        #region Error ID Code
        /// <summary>
        ///id = 1 ... zmen do prohaz, prekrocena nastavena hodnota pytlu, ktere mohou projit bez prohazu, pokud je cidlo
        ///             na prohaz aktivni
        ///id = 2 ... Více nepřečtených kódů než je limit! nacteno vice noread kodu nez je nastaveno
        ///id = 3 ... Linka zahálí! 
        ///id = 4 ... Scan bez dat!\n" + Udalosti.event36(Udalosti.Event36Stav.COUNT, 0) + "/" + Udalosti.event36(Udalosti.Event36Stav.GET_TIME, 0);
        ///id = 5 ... "Více nepotvrzených! Čidlo nepotvrdilo ČK.\n" + Udalosti.event11(Udalosti.Event11Stav.COUNT, 0) + "/" + Udalosti.event11(Udalosti.Event11Stav.GET_TIME, 0);                 
        /// </summary>
        private int _InfoIDCode = 0;
        /// <summary>
        /// ID kodu stavu vyroby pro zobrazeni napovedy ...
        /// </summary>
        /// <see cref="_ErrorIDCode"/>
        public int InfoIDCode
        {
            get { return this._InfoIDCode; }
            set { this._InfoIDCode = value; }
        }
        #endregion

        public InformationUC()
        {
            InitializeComponent();
        }

        public void vyroba_ProhazZmena(string hlaska, string znak)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { vyroba_ProhazZmena(hlaska, znak); }));
                return;
            }

            this.lblProhazovani.Text = hlaska;
            //TODO MaR 11.7.2023 zmena barvy textu
            if(hlaska == "Vypnuto")
            this.lblProhazovani.ForeColor = Color.Green;
            else
                this.lblProhazovani.ForeColor = Color.Red;

            //this.lblProhazSymbol.Text = znak;
            this.lblProhazSymbol.Text = string.Empty;
        }

        public void vyroba_ZmenaWarning(string hlaska)
        {
            if (this.InvokeRequired)
            {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                this.BeginInvoke(new MethodInvoker(() => { vyroba_ZmenaWarning(hlaska); }));
                return;
            }

            this.lblWarning.Text = hlaska;
        }

        internal void vyroba_ZmenaStavu(DataVyroba.VyrobaStavy novystav)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { vyroba_ZmenaStavu(novystav); }));
                //this.BeginInvoke((MethodInvoker)delegate() { vyroba_ZmenaStavu(novystav); });
                return;
            }

            #region Old code
            //ErrorIDCode = 0;

            //if (novystav == DataVyroba.VyrobaStavy.LogIDPracovnik)
            //{
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.LogIDSmena)
            //{
            //    //this.lblText.Text = "Vložte ID směny";
            //    //this.txtText.Text = (Convert.ToInt32(AgroConfig.config.Agro[0].PosledniPrihlasenaSmena) + 1).ToString();
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.ZadatEAN)
            //{
            //    //this.lblText.Text = "Vložte EAN kód.\nF1: Dobrý, F2: Ručně, F3: Nezadávat";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Main)
            //{
            //    //this.lblText.Text = "Odvod výroby";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Odhlaseni)
            //{
            //    //this.lblText.Text = "Opravdu chcete odhlásit směnu?";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.ZmenaProhaz)
            //{
            //    //this.lblText.Text = "Opravdu chcete " + (vyroba.ProhazZapnut ? "vypnout" : "zapnout") + " prohaz?";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.UlozeniEAN)
            //{
            //    //this.lblText.Text = "!!! Vložte EAN kód !!!";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Vyrobek)
            //{
            //    //this.lblText.Text = "Chcete změnit výrobek?";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.VlozKod)
            //{
            //    //this.lblText.Text = "Zadejte kód ručně.";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Event36)
            //{
            //    //this.lblWarning.Text = "Scan bez dat!\n" + Udalosti.event36(Udalosti.Event36Stav.COUNT, 0) + "/" + Udalosti.event36(Udalosti.Event36Stav.GET_TIME, 0);
            //    //ErrorIDCode = 4;
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Event11)
            //{
            //    //this.lblWarning.Text = "Více nepotvrzených! Čidlo nepotvrdilo ČK.\n" + Udalosti.event11(Udalosti.Event11Stav.COUNT, 0) + "/" + Udalosti.event11(Udalosti.Event11Stav.GET_TIME, 0);
            //    //ErrorIDCode = 5;
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.VlozPocet)
            //{
            //    //this.lblText.Text = "Zadejte počet kusů.";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.VlozPocetEAN)
            //{
            //    //this.lblText.Text = "Vložte EAN kód!";
            //}
            ////23.8.2017 Ta.D. pridani ID uzivatele pri zadavani sarze   
            //else if (novystav == DataVyroba.VyrobaStavy.SarzeID)
            //{
            //    //this.txtText.Text = string.Empty;
            //    //this.lblText.Text = "Zadejte ID ke změně šarže.";
            //    //this.lblWarning.Text = string.Empty;
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.SarzeHeslo)
            //{
            //    //this.txtText.Text = string.Empty;
            //    //this.lblText.Text = "Zadejte heslo ke změně šarže.";
            //    //this.lblWarning.Text = string.Empty;
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.SarzeHodnota)
            //{
            //    //this.lblText.Text = "Zadejte šarži.";
            //}
            //else
            //{
            //    this.lblText.Text = "?";
            //}
            #endregion
        }

        private void lblWarning_Click(object sender, EventArgs e)
        {
            if (infoForm == null)
            {                
                infoForm = new InfoForm();
                infoForm.TopLevel = true; 
            }

            // TODO : vyresit klik na varning a ziskani chyboveho kodu ...
            infoForm.setID(this.InfoIDCode);
            infoForm.Show();
            infoForm.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OSK.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OSK.Close();
        }
    }
}