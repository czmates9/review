using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
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
        public Production.DataServices.VyrobaDataSet.CZPRO_VPHRow rowVPH;


        public string SOPNUMBE;
        
        /// <summary>
        /// kod
        /// </summary>
        public string EAN;
        
        
        /// <summary>
        /// Sklad
        /// </summary>
        public Production.DataServices.VyrobaDataSet.CZMST093Row rowSKLAD;
        public string SKLAD;
        
        /// <summary>
        /// Lokace
        /// </summary>
        public Production.DataServices.VyrobaDataSet.CZMST094Row rowLokace;
        public string LOCNCODE;

        /// <summary>
        /// Pouzivani tabulky zbozi
        /// </summary>
        public bool VyrobaPouzivatTabulkuZbozi;

        /// <summary>
        /// Zadane bud ITEMNMBR anebo ITEMDESC z tabulky zbozi 095
        /// </summary>
        public Production.DataServices.KonzolaDataSet.FASK_CONS_095Row rowZbozi;
        public string ZBOZI;

        /// <summary>
        /// Vyrobek
        /// </summary>
        public Production.DataServices.KonzolaDataSet.FASK_CONS_095Row rowVyrobek;
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
        public Production.DataServices.VyrobaDataSet.LoginsRow rowUzivatel;


        public string TERMINAL_ID;
        public string NMBRPAL;
        public string TYPEPAL;
        public string PRINTED;
    }
}
