using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModulePohodaXML
{
    //public class Doklad
    //{
    //    public string docid;
    //    public string funkce;
    //    public string idsradatext;
    //    public string docid2;
    //    public string sklid;

    //    public Doklad(string doc, string fce, string ids, string doc2, string sklid)
    //    {
    //        this.docid = doc;
    //        this.funkce = fce;
    //        this.idsradatext = ids;
    //        this.docid2 = doc2;
    //        this.sklid = sklid;
    //    }
    //}

    public class Doklad
    {
        public string docid;
        public string funkce;
        public string idsradatext;
        public string docid2;
        public string sklid;

        public string Stredisko0;
        public string Stredisko1;
        public string Cinnost;
        public string Zakazka;

        public bool Kontrola_Disponability;
        public byte? Vyber_Typ_Prevodka;

        public int? Prodej_Prijemka_Tisk_ID_sablona;
        public int? Prodej_Vydejka_Tisk_ID_sablona;
        public int? Prodej_Prevodka_Tisk_ID_sablona;

        public string Prodej_Prijemka_Tisk_Tiskarna;
        public string Prodej_Vydejka_Tisk_Tiskarna;
        public string Prodej_Prevodka_Tisk_Tiskarna;

        public bool Import_Doklad_IS;


        public Doklad(
            string doc,
            string fce,
            string ids,
            string doc2,
            string sklid,
            string stredisko0,
            string stredisko1,
            string cinost,
            string zakazka,
            bool kontrola_Disponability,
            byte vyber_Typ_Prevodka,
            int? prodej_Prijemka_Tisk_ID_sablona,
            int? prodej_Vydejka_Tisk_ID_sablona,
            int? prodej_Prevodka_Tisk_ID_sablona,
            string prodej_Prijemka_Tisk_Tiskarna,
            string prodej_Vydejka_Tisk_Tiskarna,
            string prodej_Prevodka_Tisk_Tiskarna,
            bool Import_Doklad_IS
            )
        {
            this.docid = doc;
            this.funkce = fce;
            this.idsradatext = ids;
            this.docid2 = doc2;
            this.sklid = sklid;
            this.Stredisko0 = stredisko0;
            this.Stredisko1 = stredisko1;
            this.Cinnost = cinost;
            this.Zakazka = zakazka;
            this.Kontrola_Disponability = kontrola_Disponability;
            this.Vyber_Typ_Prevodka = vyber_Typ_Prevodka;
            this.Prodej_Prijemka_Tisk_ID_sablona = prodej_Prijemka_Tisk_ID_sablona;
            this.Prodej_Vydejka_Tisk_ID_sablona = prodej_Vydejka_Tisk_ID_sablona;
            this.Prodej_Prevodka_Tisk_ID_sablona = prodej_Prevodka_Tisk_ID_sablona;

            this.Prodej_Prijemka_Tisk_Tiskarna = prodej_Prijemka_Tisk_Tiskarna;
            this.Prodej_Vydejka_Tisk_Tiskarna = prodej_Vydejka_Tisk_Tiskarna;
            this.Prodej_Prevodka_Tisk_Tiskarna = prodej_Prevodka_Tisk_Tiskarna;
            this.Import_Doklad_IS = Import_Doklad_IS;

        }

        public Doklad(Fask.Rady.DS_Rady.FASK_RADYRow Row)
        {
            if (Row != null)
            {

                this.docid = Row.IsModul_IDNull() ? string.Empty : Row.Modul_ID;
                this.funkce = Row.IsModul_FunkceNull() ? string.Empty : Row.Modul_Funkce;
                this.idsradatext = Row.IsRada_IDNull() ? string.Empty : Row.Rada_ID.ToString();
                this.docid2 = Row.IsModul_ID2Null() ? string.Empty : Row.Modul_ID2;
                this.sklid = Row.IsFiltr_SkladIDNull() ? string.Empty : Row.Filtr_SkladID;
                this.Stredisko0 = Row.IsVloz_Stredisko0Null() ? string.Empty : Row.Vloz_Stredisko0;
                this.Stredisko1 = Row.IsVloz_Stredisko1Null() ? string.Empty : Row.Vloz_Stredisko1;
                this.Cinnost = Row.IsVloz_CinnostNull() ? string.Empty : Row.Vloz_Cinnost;
                this.Zakazka = Row.IsVloz_ZakazkaNull() ? string.Empty : Row.Vloz_Zakazka;
                this.Kontrola_Disponability = Row.IsKontrola_DisponabilityNull() ? false : Row.Kontrola_Disponability;
                this.Vyber_Typ_Prevodka = Row.IsVyber_Typ_PrevodkaNull() ? null : (byte?)Row.Vyber_Typ_Prevodka;

                this.Prodej_Prijemka_Tisk_ID_sablona = Row.IsProdej_Prijemka_Tisk_ID_sablonaNull() ? (int?)null : Row.Prodej_Prijemka_Tisk_ID_sablona;
                this.Prodej_Vydejka_Tisk_ID_sablona = Row.IsProdej_Vydejka_Tisk_ID_sablonaNull() ? (int?)null : Row.Prodej_Vydejka_Tisk_ID_sablona;
                this.Prodej_Prevodka_Tisk_ID_sablona = Row.IsProdej_Prevodka_Tisk_ID_sablonaNull() ? (int?)null : Row.Prodej_Prevodka_Tisk_ID_sablona;

                this.Prodej_Prijemka_Tisk_Tiskarna = Row.IsProdej_Prijemka_Tisk_TiskarnaNull() ? null : Row.Prodej_Prijemka_Tisk_Tiskarna;
                this.Prodej_Vydejka_Tisk_Tiskarna = Row.IsProdej_Vydejka_Tisk_TiskarnaNull() ? null : Row.Prodej_Vydejka_Tisk_Tiskarna;
                this.Prodej_Prevodka_Tisk_Tiskarna = Row.IsProdej_Prevodka_Tisk_TiskarnaNull() ? null : Row.Prodej_Prevodka_Tisk_Tiskarna;

                this.Import_Doklad_IS = Row.Import_Doklad_IS;
            }

        }
    }
}
