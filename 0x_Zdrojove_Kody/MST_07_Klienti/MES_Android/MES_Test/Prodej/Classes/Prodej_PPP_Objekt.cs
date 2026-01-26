using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Prodej.Classes
{
    public class Prodej_PPP_Objekt
    {

        private Volajici_ProdejPridatPolozku _volajici = Volajici_ProdejPridatPolozku.Unknow;
        public Volajici_ProdejPridatPolozku Volajici
        {
            get { return _volajici; }
            set { _volajici = value; }
        }


        private Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row _zbozi = null;
        public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row Zbozi
        {
            get { return _zbozi; }
            set
            {
                _zbozi = value;
            }
        }

        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row _odberatel = null;
        public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row Odberatel
        {
            get { return _odberatel; }
            set
            {
                _odberatel = value;
            }
        }

        private string _serltnum = string.Empty;
        public string Serltnum
        {
            get
            {
                return _serltnum;
            }
            set
            {
                _serltnum = value;
            }
        }

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


        //TODO : udelat nejak logiku na scenner
        private bool _scannerEnable;
        /// <summary>
        /// Povolit prazdny vstup
        /// </summary>
        public bool ScannerEnable
        {
            get { return this._scannerEnable; }
            set { this._scannerEnable = value; }
        }

    }
}