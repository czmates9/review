using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Prodej
{
    public interface IProdej
    {
        // ************** Metody pro zpracovani dat ************ //
        StatusObject Prodej_Process(Davka davka, Terminal terminal, User uzivatel, Fask.DataSets.ProdejData data);
        bool Prodej_AfterProcessedAction(Davka davka);
        StatusOverPohyb Over_Pohyb(ProdejPohyb prodejPohyb);

        // ************** Online metody pro navrat dat ************* //
        Fask.Server.Interfaces.DataSets.Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty);

        // ************** Online kontrolni metody ************* //
        StatusOverLokace Prodej_Online_OverLokace(string itemnmbr, string serltnum, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType);

        // ************** Online Tisk ************* //
        Fask.Server.Interfaces.Classes.StatusResult Prodej_ProcessTiskSoupis(Fask.DataSets.ProdejData data, out Fask.Server.Interfaces.DataSets.DSValues dataHeader, out List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, out Fask.Server.Interfaces.DataSets.DSValues dataFooter);
    }
}
