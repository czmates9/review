using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.DataSets;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Prodej
{
    public interface IProdej
    {
        // ************** Metody pro zpracovani dat ************ //
        StatusObject Prodej_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, User uzivatel, Fask.DataSets.ProdejData data);
        bool Prodej_AfterProcessedAction(Davka davka);
        StatusOverPohyb Over_Pohyb(ProdejPohyb prodejPohyb);

        // ************** Online metody pro navrat dat ************* //
        Fask.Server.Interfaces.DataSets.Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty);
       

        // ************** Online kontrolni metody ************* //
        StatusOverLokace Prodej_Online_OverLokace(string itemnmbr, string serltnum, DateTime? Expirace, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType);

        // ************** Online Tisk ************* //
        Fask.Server.Interfaces.Classes.StatusResult Prodej_ProcessTiskSoupis(Fask.DataSets.ProdejData data, out Fask.Server.Interfaces.DataSets.DSValues dataHeader, out List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, out Fask.Server.Interfaces.DataSets.DSValues dataFooter);


		//************** Tedtovaci Data *************************//
        /// <summary>
        /// Metoda která implementuje přimo zapis a praci nad IS
        /// Vytažena ven z duvodu přimeho volani pro testy anebo servisny zasah
        /// </summary>
        /// <param name="davka"></param>
        /// <param name="data"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
		string Prodej_Import_to_IS(Davka davka, Fask.DataSets.ProdejData data, Fask.Server.Interfaces.Classes.User uzivatel);


		//******************Dohedani Dodavatele*************************//
		Odberatele Prodej_GetDodavatele_External(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod);
		Odberatele Prodej_GetDodavatele_ExternalByICO(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad,  string ICO);

 
		Fask.Server.Interfaces.Classes.StatusInfo Prodej_Disponibilita(Disponibilita disponibilita);


		Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt Online_UniverzalnyDotazNaCokoliv(Fask.Server.Interfaces.Classes_OnlineKomunikace.VstupniObjekt ObjektIN);

        Fask.Server.Interfaces.Classes.STATUS GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest);


    }
}
