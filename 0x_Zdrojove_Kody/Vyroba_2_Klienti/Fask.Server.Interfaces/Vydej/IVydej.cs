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

        // ************** Metody pro zisakni predlohy **************** //
        Fask.DataSets.Vydejky Vydej_GetVydejky(Terminal terminal, Sklad sklad, Item item, User user);
        Fask.DataSets.Vydej Vydej_GetVydejka(Davka davka, Terminal terminal, Sklad sklad, Item item);
        bool Vydej_GetVydejkaReceived(Davka davka, Terminal terminal, Sklad sklad, Item item);

        // ************** Metody pro zpracovani dat ************ //
        StatusObject Vydej_Process(
            Davka davka, Terminal terminal, Sklad sklad, Item item,
            Fask.DataSets.Vydej vydejdata, 
            ProcessState processVydejState);
        bool Vydej_AfterProcessedAction(Davka davka);

        // ************** Doplnujici metody ********* //
        StatusObject Vydej_Storno_Vydejka(Davka davka, Terminal terminal, string password);
        StatusObject Vydej_Finish_Vydejka(Davka davka, Terminal terminal, string password);

        // ************** Metody pro dotazeni informaci ********* //
        DataSet Vydej_Detail(Objednavka objednavka);
        DataSet Vydej_DetailDavka(Davka davka);


    }
}
