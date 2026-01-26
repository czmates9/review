using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus
{
    public partial class KeyboardUC : UserControl
    {
        //private KeyboardText _keyboardTextDefault = new KeyboardText();
        private KeyboardText _keyboardText = null;
        [Browsable(false)]
        public KeyboardText KeyboardText
        {
            get { return this._keyboardText; }
            set
            {
                this._keyboardText = value;
                if (this._keyboardText != null)
                {
                    KeyboardTextInitialize();
                }
            }
        }

        private Dictionary<DataVyroba.VyrobaStavy, KeyboardText> _KeyboardsTextStavy = new Dictionary<DataVyroba.VyrobaStavy, KeyboardText>();
        //[Browsable(false)]
        //public Dictionary<DataVyroba.VyrobaStavy, KeyboardText> KeyboardsTextStavy
        //{
        //    get { return this._KeyboardsTextStavy; }
        //    set { this._KeyboardsTextStavy = value; }
        //}

        private void KeyboardTextInitialize()
        {
            try
            {
                // clear bindings
                this.BtnEnter.DataBindings.Clear();
                this.btn19.DataBindings.Clear();
                this.btnF1.DataBindings.Clear();
                this.btnF2.DataBindings.Clear();
                this.btnF3.DataBindings.Clear();
                this.btnF4.DataBindings.Clear();
                this.btnF5.DataBindings.Clear();

                if (this._keyboardText != null)
                {
                    this.BtnEnter.DataBindings.Add("Text", this._keyboardText, "BtnEnter", false, DataSourceUpdateMode.OnPropertyChanged);
                    this.btn19.DataBindings.Add("Text", this._keyboardText, "Btn19", false, DataSourceUpdateMode.OnPropertyChanged);
                    this.btnF1.DataBindings.Add("Text", this._keyboardText, "BtnF1", false, DataSourceUpdateMode.OnPropertyChanged);
                    this.btnF2.DataBindings.Add("Text", this._keyboardText, "BtnF2", false, DataSourceUpdateMode.OnPropertyChanged);
                    this.btnF3.DataBindings.Add("Text", this._keyboardText, "BtnF3", false, DataSourceUpdateMode.OnPropertyChanged);
                    this.btnF4.DataBindings.Add("Text", this._keyboardText, "BtnF4", false, DataSourceUpdateMode.OnPropertyChanged);
                    this.btnF5.DataBindings.Add("Text", this._keyboardText, "BtnF5", false, DataSourceUpdateMode.OnPropertyChanged);
                }

            }
            catch (Exception exKeyboardInitialize)
            {
               // Exceptions.Handler.ErrorHandle(exKeyboardInitialize.Message, "KeyboardUC", false);
                ExceptionHandler2.Handle(exKeyboardInitialize.Message, "KeyboardUC", false);
            }
        }

        public event System.EventHandler ButtonClick;
        private void OnButtonClick(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(sender, e);
            }
        }

        public KeyboardUC()
        {
            InitializeComponent();

            //this.AutoSize = true;
            //this.Dock = DockStyle.Fill;

            this.KeyboardText = new KeyboardText();

            #region Nastaveni klavesoveho rozlozeni pro jednotlive stavy vyroby ...
            // Nastaveni klavesoveho rozlozeni pro jednotlive stavy vyroby ...
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Main,
                new KeyboardText("Vložit kód", "Změna", "Prohaz", "Odhlásit", "Odečíst", "Šarže", "Servis")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.LogIDSmena,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.LogIDPracovnik,
                new KeyboardText(null, null, null, null, null, "Další", "OK")
                );

            // 16.5.2019 JiS v AGRO docasne zrusen F1 - dobry EAN, kvuli problemu se scannerem na lince 1.
            //this._KeyboardsTextStavy.Add(
            //    DataVyroba.VyrobaStavy.ZadatEAN,
            //    new KeyboardText("F1", "F2", "F3", null, null, null, null)
            //    );
            // => na toto ...
            //this._KeyboardsTextStavy.Add(
            //    DataVyroba.VyrobaStavy.ZadatEAN,
            //    new KeyboardText(null, "F2", "F3", null, null, null, null)
            //    );
            // 24.7.2019 JiS, obnovena puvodni funkcnost
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.ZadatEAN,
                new KeyboardText("F1", "F2", "F3", null, null, null, null)
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Odhlaseni,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.ZmenaProhaz,
                new KeyboardText(null, null, null, null, null, "Ne", "Ano")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.UlozeniEAN,
                new KeyboardText(null, null, null, null, null, null, "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Vyrobek,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.VlozKod,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.VlozPocet,
                new KeyboardText("Akt. kód", "Zadat kód", null, null, null, null, "Zpět")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.VlozPocetEAN,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.SarzeID,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.SarzeHeslo,
                new KeyboardText(null, null, null, null, null, "Zpět", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.SarzeHodnota,
                new KeyboardText("Klávesnice", null, null, null, "Generovat", "Zpět", "OK")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Event11,
                new KeyboardText(null, null, null, null, null, null, "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Event36,
                new KeyboardText(null, null, null, null, null, null, "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.VPH_Zadani,
                new KeyboardText(null, null, null, null, null, "Smazat", "OK")
                );
            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.VPP_VlozitKod,
                new KeyboardText(null, null, null, null, null, "Smazat", "OK")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Zombie,
                new KeyboardText(null, null, null, null, null, null, "Pokračovat")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Pytle,
                new KeyboardText(null, null, null, null, null, null, "Další")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_Palety,
                new KeyboardText(null, null, null, null, null, "Předchozí", "Další")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_PotvrzeniPalety_NP,
                new KeyboardText(null, null, null, null, null, "Předchozí", "Ano")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.Zadani_PoslednaPaleta_PotvrzeniPalety_UP,
                new KeyboardText(null, null, null, null, null, "Předchozí", "Ano")
                );

            this._KeyboardsTextStavy.Add(
                DataVyroba.VyrobaStavy.SERVIS,
                new KeyboardText(null, null, null, null, null, null, "Pokračovat")
                );

            #endregion

        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnButtonClick(sender, e);
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "KeyboardUC", false);
                ExceptionHandler2.Handle(ex.Message, "KeyboardUC", false);
            }
        }

        internal void vyroba_ZmenaStavu(DataVyroba.VyrobaStavy novystav)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { vyroba_ZmenaStavu(novystav); }));
                //this.BeginInvoke((MethodInvoker)delegate() { vyroba_ZmenaStavu(novystav); });
                return;
            }

            try
            {
                var stavkeyboardtext = this._KeyboardsTextStavy[novystav];
                this.KeyboardText = stavkeyboardtext;
                return;
            }
            catch (Exception exStavKeyboard)
            {
                //Exceptions.Handler.ErrorHandle("KeyboardText not found in StavyKeyboards\n" + exStavKeyboard.Message, "KeyboardUC", false);
                ExceptionHandler2.Handle("KeyboardText not found in StavyKeyboards\n" + exStavKeyboard.Message, "KeyboardUC", false);
                this.KeyboardText.BtnEnter = exStavKeyboard.Message;
            }

            #region Old code
            //this.KeyboardTextInitialize();
            //if (novystav == DataVyroba.VyrobaStavy.Main)
            //{
            //    this.KeyboardText.BtnF1 = "Vložit kód";
            //    this.KeyboardText.BtnF2 = "Změna";
            //    this.KeyboardText.BtnF3 = "Prohaz";
            //    this.KeyboardText.BtnF4 = "Odhlásit";
            //    this.KeyboardText.BtnF5 = "Odečíst";
            //    this.KeyboardText.Btn19 = "Šarže";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.LogIDSmena)
            //{
            //    //this.lblText.Text = "Vložte ID směny";
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.LogIDPracovnik)
            //{
            //    //this.lblText.Text = "Vložte ID pracovníka";
            //    this.KeyboardText.BtnEnter = "OK";
            //    this.KeyboardText.Btn19 = "Další";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.ZadatEAN)
            //{
            //    this.KeyboardText.BtnF1 = "F1";
            //    this.KeyboardText.BtnF2 = "F2";
            //    this.KeyboardText.BtnF3 = "F3";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Odhlaseni)
            //{
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.ZmenaProhaz)
            //{
            //    this.KeyboardText.Btn19 = "Ne";
            //    this.KeyboardText.BtnEnter = "Ano";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.UlozeniEAN)
            //{
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Vyrobek)
            //{
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.VlozKod)
            //{
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Event36)
            //{
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.Event11)
            //{
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.VlozPocet)
            //{
            //    this.KeyboardText.BtnF1 = "Akt. kód";
            //    this.KeyboardText.BtnF2 = "Zadat kód";
            //    this.KeyboardText.BtnEnter = "Zpět";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.VlozPocetEAN)
            //{
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            ////23.8.2017 Ta.D. pridani ID uzivatele pri zadavani sarze   
            //else if (novystav == DataVyroba.VyrobaStavy.SarzeID)
            //{
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.SarzeHeslo)
            //{
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else if (novystav == DataVyroba.VyrobaStavy.SarzeHodnota)
            //{
            //    this.KeyboardText.BtnF5 = "Generovat";
            //    this.KeyboardText.Btn19 = "Zpět";
            //    this.KeyboardText.BtnEnter = "OK";
            //}
            //else
            //{
            //    this.KeyboardText.BtnEnter = "dodelat";
            //}
            #endregion
        }
    }
}
