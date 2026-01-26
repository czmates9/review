using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_ImportPrijemkaPohoda_Zdroj_P_TP : IProduction
    {
        /// <summary>
        /// Provede import Prijemky do Pohody na zaklade vydane objednavky a prijmovych dat
        /// Parovani pres SOPNUMBE a ORD => prijemka 
        /// </summary>
        /// <param name="countEntries">cislo davky, ktera se importovala</param>
        /// <returns>OK kdyz vse v poradku, jinak text chyby ... </returns>
        string Production_ImportPrijemkaPohoda_Zdroj_P_TP(int countEntries,string SKL_ID, string userID, string Vydejka);
    }
}
