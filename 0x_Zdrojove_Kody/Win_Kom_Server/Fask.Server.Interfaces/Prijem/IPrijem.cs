using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Prijem
{
    public interface IPrijem //: IMST
    {
        // ************** Metody pro pripravu predlohy **************** //
        /// <summary>
        /// Generuje data predlohy
        /// </summary>
        /// <param name="objednavka"></param>
        /// <returns></returns>
        StatusInfo Prijem_GenerateDavka(Objednavka objednavka, Sklad sklad);

        // ************** Metody pro zisakni predlohy **************** //
        Fask.DataSets.PrijemDavky Prijem_GetPrijemky(Terminal terminal, Sklad sklad, Item item);
        Fask.DataSets.Prijem Prijem_GetPrijemka(Davka davka, Terminal terminal, Sklad sklad, Item item);
        bool Prijem_GetPrijemkaReceived(Davka davka, Terminal terminal, Sklad sklad, Item item);

        Fask.Server.Interfaces.DataSets.Obecne Prijem_GetPrijemky_External(Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod);

        // ************** Metody pro zpracovani dat ************ //
        StatusObject Prijem_Process(
            Davka davka, Terminal terminal, Sklad sklad, Item item,
            Fask.DataSets.Prijem prijemdata,
            ProcessState processPrijemState);
        bool Prijem_AfterProcessedAction(Davka davka);

        // ************** Metody pro dotazeni informaci ********* //
        DataSet Prijem_DetailDavka(Davka davka);
        DataSet Prijem_Detail(Objednavka objednavka, Sklad sklad);
        DataSet Prijem_Detail_Polozka(Objednavka objednavka, Sklad sklad, Item polozka);

        // ************** Doplnujici metody ********* //
        StatusObject Prijem_Storno_Prijemka(Davka davka, Terminal terminal, string password);
        StatusObject Prijem_Finish_Prijemka(Davka davka, Terminal terminal, string password);

        // ************** Online zapisy stavu prijmu ************* //
        StatusObject Prijem_Online_Add(Davka davka, Terminal terminal, Fask.DataSets.Prijem prijemRows);
        StatusObject Prijem_Online_Del(Davka davka, Terminal terminal, Fask.DataSets.Prijem prijemRows);
        StatusObject Prijem_Online_Quantity(Davka davka, Terminal terminal, ref Fask.DataSets.Prijem prijemRows);

        // ************** Online kontrolni metody ************* //
        StatusOverLokace Prijem_Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id);



        //DataSet Prijem_GetSkladExpedice(string ITEMNMBR, string nasnimano, string odvedeno, string SERLTNUM);

        bool Prijem_GetSkladExpedice(
                                            string ITEMNMBR,
                                            decimal MnozstviZadane,
                                            decimal MnozstviNasnimane,
                                            out decimal MnozstviDodavatelePozadovano,
                                            out decimal MnozstviDodavateleDodano,
                                            out decimal MnozstviDodavateleDodat,
                                            out decimal MnozstviOdberateliPozadovano,
                                            out decimal MnozstviOdberatelumDodano,
                                            out decimal MnozstviOdberatelumDodat,
                                            out decimal Vysledek
                                            );

        // ******************************************************************* //

        string Online_GenerateSerltnum(string itemnmbr, string skl_id, string oldSerltnum);
        Fask.Server.Interfaces.DataSets.Obecne Online_GetDoporuceneLokace(string itemnmbr, string skl_id, string serltnum);
        Fask.Server.Interfaces.DataSets.Obecne Online_GetNezrealizovanePrijemky();


        /****TESTOVACI *************************************************************************/
        string TEST_ImportPrijem_Do_IS(int countEntries, string SKL_ID, string PONUMBER, string note);

    }
}
