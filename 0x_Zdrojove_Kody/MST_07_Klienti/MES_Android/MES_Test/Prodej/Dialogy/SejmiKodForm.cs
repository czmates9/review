using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static MES_Android.MessageBox;

namespace MES_Android.Prodej
{
    public class SejmiKodForm : IDisposable
    {
        #region Revitalizovane parametry

        private Android.Text.InputTypes _typeOfCode;
        public Android.Text.InputTypes CodeType
        {
            get { return this._typeOfCode; }
            set { _typeOfCode = value; }
        }

        private string _popis;
        public string Popis
        {
            get { return this._popis; }
            set { _popis = value; }
        }

        private string _text;
        public string Text
        {
            get { return this._text; }
            set { _text = value; }
        }

        private string _kod;
        public string Kod
        {
            get { return this._kod; }
            set { _kod = value; }
        }

        //TODO : udelat nejak logiku na AllowEmpty
        private bool _allowEmpty;
        /// <summary>
        /// Povolit prazdny vstup
        /// </summary>
        public bool AllowEmpty
        {
            get { return this._allowEmpty; }
            set { this._allowEmpty = value; }
        }

        #endregion


        private decimal len;
        /// <summary>
        /// Delka vstupni hodnoty, ktera se bude kontrolovat
        /// </summary>
        public decimal Len
        {
            get { return len; }
            set { len = value; }
        }



        private bool checkLen;
        /// <summary>
        /// Kontrolovat delku vstupni hodnoty
        /// </summary>
        public bool CheckLen
        {
            get { return this.checkLen; }
            set { this.checkLen = value; }
        }

       
     

        private DialogResult _ConfirmBoxResult = DialogResult.None;
        private bool _IsCurrentlyInConfirmProcess = true;

        public DialogResult ShowDialog(AppCompatActivity _CurrentContext)
        {
            if (_CurrentContext != null)
            {
                EventHandler<DialogClickEventArgs> callbackA = OnConfirmCallBackA;
                EventHandler<DialogClickEventArgs> callbackB = OnConfirmCallBackB;

                Action messageBoxDelegate = () => AddDialog.Show(_CurrentContext, _popis, _text, _kod, _typeOfCode, callbackA, callbackB);

                _CurrentContext.RunOnUiThread(messageBoxDelegate);

                while (_IsCurrentlyInConfirmProcess)
                {
                    Thread.Sleep(1000);
                }
            }

            AddDialog.Dispose();
            return _ConfirmBoxResult;
        }

        private void OnConfirmCallBackA(object sender, DialogClickEventArgs e)
        {
            _kod = AddDialog.nameEditText.Text;
            _ConfirmBoxResult = DialogResult.OK;
            _IsCurrentlyInConfirmProcess = false;
        }

        private void OnConfirmCallBackB(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Cancel;
            _IsCurrentlyInConfirmProcess = false;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

}