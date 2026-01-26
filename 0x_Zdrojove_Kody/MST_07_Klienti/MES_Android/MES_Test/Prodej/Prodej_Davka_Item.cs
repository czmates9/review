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

namespace MES_Android.Prodej
{
    public class Prodej_Davka_Item
    {

        private int? _cisloDavky;
        public int? CisloDavky 
        {
            get { return _cisloDavky; }
            set { _cisloDavky = value; } 
        }


        private int _polozek = 0;
        public int PocetRadku 
        {
            get { return _polozek; }
            set { _polozek = value; } 
        }

        private byte? _cfg_lok_mech = 0;
        public byte? cfg_lok_mech
        {
            get { return _cfg_lok_mech; }
            set { _cfg_lok_mech = value; }
        }

        public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row Odberatel { get; set; }
        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row TypDokladu { get; set; }
        public Fask.SQLiteDBs.DataSets.Strediska.CZMST091Row Stredisko { get; set; }
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row SkladZdroj { get; set; }
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row SkladCil { get; set; }
        public Fask.SQLiteDBs.DataSets.Meny.CZMST097Row Mena { get; set; }

        public MES_Android.Classes.Paleta NMBRPAL { get; set; }


        public Prodej_Davka_Item()
        {

        }
    }
}