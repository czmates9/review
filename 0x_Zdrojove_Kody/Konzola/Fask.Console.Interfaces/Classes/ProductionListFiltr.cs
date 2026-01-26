using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class ProductionListFiltr
    {

        public ProductionListFiltr() { }

        /// <summary>
        /// Pouzivani tabulky zbozi
        /// </summary>
        public bool VyrobaPouzivatTabulkuZbozi;

        public Production.DataServices.VyrobaDataSet.CZPRO_VPHRow rowVPH;

        /// <summary>
        /// Vyrobni prikaz
        /// </summary>
        public string VyrobniPrikaz;



        public Production.DataServices.KonzolaDataSet.FASK_CONS_095Row rowZbozi;

        /// <summary>
        /// Zadane bud ITEMNMBR anebo ITEMDESC
        /// </summary>
        public string Zbozi_itemdesc;

        public string Uzivatel;
        public Production.DataServices.VyrobaDataSet.LoginsRow rowUzivatel;

        public string Skupina;
        public Production.DataServices.VyrobaDataSet.GroupsRow rowGroups;

        public string Stroj;
        public Production.DataServices.VyrobaDataSet.MachinesRow rowMachine;

        public string Operace;
        public Production.DataServices.VyrobaDataSet.OperationsRow rowOperation;

        public bool DatumDo;
        public bool DatumOd;
        public DateTime DatumDoValue;
        public DateTime DatumOdValue;

        public bool OdvadeniVse;
        public bool KorekceVse;
        public bool OdvadeniNedokoncene;
        public bool KorekceNedokoncene;
    }
}
