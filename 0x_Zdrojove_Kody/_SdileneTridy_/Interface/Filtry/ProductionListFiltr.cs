using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class ProductionListFiltr : FilterBase
    {

        public ProductionListFiltr() { }

        /// <summary>
        /// Pouzivani tabulky zbozi
        /// </summary>
        public bool VyrobaPouzivatTabulkuZbozi;

        //public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow rowVPH;
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable rowVPH;

        /// <summary>
        /// Vyrobni prikaz
        /// </summary>
        public string VyrobniPrikaz;



        //public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBYRow rowZbozi;
        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable rowZbozi;

        /// <summary>
        /// Zadane bud ITEMNMBR anebo ITEMDESC
        /// </summary>
        public string Zbozi_itemdesc;

        public string Uzivatel;
        //public FASK.Logins.DataSets.Pristupy.FASK_LoginsRow  rowUzivatel;
        public FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable rowUzivatel;

        public string Skupina;
        //public Fask.Interfaces.DataSets.Vyroba.GroupsRow rowGroups;
        public Fask.Interfaces.DataSets.Vyroba.GroupsDataTable rowGroups;

        public string Stroj;
        //public Fask.Interfaces.DataSets.Vyroba.MachinesRow rowMachine;
        public Fask.Interfaces.DataSets.Vyroba.MachinesDataTable rowMachine;

        public string Operace;
        //public Fask.Interfaces.DataSets.Vyroba.OperationsRow rowOperation;
        public Fask.Interfaces.DataSets.Vyroba.OperationsDataTable rowOperation;

        //public bool DatumDo;
        //public bool DatumOd;
        public DateTime? DatumDoValue;
        public DateTime? DatumOdValue;

        public bool OdvadeniVse;
        public bool KorekceVse;
        public bool OdvadeniNedokoncene;
        public bool KorekceNedokoncene;

        public string DATEEVE_TimeVariant;

        public bool PouzeNeschvalene;

        public string TerminalID { get; set; }
    }
}
