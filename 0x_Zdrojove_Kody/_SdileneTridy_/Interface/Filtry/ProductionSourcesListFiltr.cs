using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class ProductionSourcesListFiltr
    {

        public ProductionSourcesListFiltr() { }

        /// <summary>
        /// cislo davky
        /// </summary>
        public string CountEntries;

        /// <summary>
        /// predloha hlavicky
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow rowVPH;


        public string SOPNUMBE;
        
        /// <summary>
        /// kod
        /// </summary>
        public string EAN;
        
        
        /// <summary>
        /// Sklad
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZMST093Row rowSKLAD;
        public string SKLAD;
        
        /// <summary>
        /// Lokace
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.CZMST094Row rowLokace;
        public string LOCNCODE;

        /// <summary>
        /// Pouzivani tabulky zbozi
        /// </summary>
        public bool VyrobaPouzivatTabulkuZbozi;

        /// <summary>
        /// Zadane bud ITEMNMBR anebo ITEMDESC z tabulky zbozi ZASOBY
        /// </summary>
        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowZASOBY;
        public string ZBOZI;

        /// <summary>
        /// Vyrobek
        /// </summary>
        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowVyrobek;
        public string VYROBEK;


        public string ITEMTYPE;
        public string ITEMCODE;
        public string   MJ;
        public string SERLTNUM;



        /// <summary>
        /// Datumy
        /// </summary>
        public bool DatumDo;
        public bool DatumOd;
        public DateTime DatumDoValue;
        public DateTime DatumOdValue;

        /// <summary>
        /// uzivatel
        /// </summary>
        public string Uzivatel;
        public FASK.Logins.DataSets.Pristupy.FASK_LoginsRow rowUzivatel;


        public string TERMINAL_ID;
        public string NMBRPAL;
        public string TYPEPAL;
        public string PRINTED;
    }
}
