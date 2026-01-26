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
using System.Threading;
using static MES_Android.MessageBox;
using static MES_Android.Prodej.Logika.Prodej_SberDat_LogikaAsync;

namespace MES_Android.Prodej
{
    public class ProdejPridatPolozku : SejmiKodForm
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

        public ProdejPridatPolozku(string popis, Android.Text.InputTypes typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel
            , Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, bool povolitScanner)
        {
            //Takhle vše asi...
            // a kedže tohle dedí z SejmiKod, tak asi to ma neco navíc?
            //base.Popis = popis;

        }

        public ProdejPridatPolozku(string popis, Android.Text.InputTypes typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni
            , Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel
            , Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi)
        {

        }


        public DialogResult ShowDialog(Prodej_SberDat _CurrentContext)
        {
            return base.ShowDialog(_CurrentContext);
        }

    }
}