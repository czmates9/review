using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Vydej
{
    public interface IVydej : IWebModule
    {
        // ************** Metody pro pripravu predlohy **************** //
        /// <summary>
        /// Generuje data predlohy
        /// </summary>
        /// <param name="objednavka"></param>
        /// <returns></returns>
        StatusInfo Vydej_GenerateDavka(Objednavka objednavka, Sklad sklad);
        // ************** Metody pro pripravu lokačního mechanizmu CZMST094 **************** //
        StatusInfo Vydej_GenerateData_CZMST094(Objednavka objednavka, Sklad sklad);

        // ************** Metody pro zisakni predlohy **************** //
        Fask.DataSets.Vydejky Vydej_GetVydejky(Terminal terminal, Sklad sklad, Item item, User user);
        Fask.DataSets.Vydej Vydej_GetVydejka(Davka davka, Terminal terminal, Sklad sklad, Item item);
        bool Vydej_GetVydejkaReceived(Davka davka, Terminal terminal, Sklad sklad, Item item);

        // ************** Metody pro zpracovani dat ************ //
        StatusObject Vydej_Process(
            Davka davka, Terminal terminal, Sklad sklad, Item item,
            Fask.DataSets.Vydej vydejdata, 
            ProcessState processVydejState,
            string itemtype);
        bool Vydej_AfterProcessedAction(Davka davka);

        // ************** Doplnujici metody ********* //
        StatusObject Vydej_Storno_Vydejka(Davka davka, Terminal terminal, string password);
        StatusObject Vydej_Finish_Vydejka(Davka davka, Terminal terminal, string password);

        // ************** Online metody ********* //
        /// <summary>
        /// Returns materials for combination of parameters itemnmbr(itemnumber) | skl_id(id sklad) | serltnum(sarze materialu)
        /// at least one param have to be specified, but may be all of them.
        /// this results at more algorithms to be defined inside this function
        /// </summary>
        /// <param name="itemnmbr">internal Item number</param>
        /// <param name="skl_id">internal id sklad</param>
        /// <param name="serltnum">serialnumber of lot of item to find</param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.Vydej_Items_Online Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum);
        /// <summary>
        /// Returns status of expiration if it's possible to use based on some future parameters at IS
        /// </summary>
        /// <param name="itemnmbr">internal Item number</param>
        /// <param name="skl_id">internal id sklad</param>
        /// <param name="serltnum">serialnumber of lot of item to find</param>
        /// <param name="expirace">expiration of lot of item to find</param>
        /// <returns></returns>
        StatusOverExpirace Vydej_Online_Expirace_Verify(string itemnmbr, string sklad_id, string serltnum, DateTime? expirace);
        
        /// <summary>
        /// verifies password for expiration is confirmed
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns>True - confirmed, False - not confirmed</returns>
        bool Vydej_Online_Expirace_Confirm(byte terminalID, string userID, string password);


        // ************** Metody pro dotazeni informaci ********* //
        DataSet Vydej_Detail(Objednavka objednavka);
        DataSet Vydej_DetailDavka(Davka davka);
        DataSet Vydej_DetailPolozka(Item item);
        

        //**************** testovacidata***************************//
        StatusObject TEST_ImportVydejka_Do_IS(int countEntries, int userID, string SKL_ID, string note);
        StatusObject TEST_ImportFaktura_Do_IS(int countEntries, int userID, string SKL_ID, string note);

        

    }
}
