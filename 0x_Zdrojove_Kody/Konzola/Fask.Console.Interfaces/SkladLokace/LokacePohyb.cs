using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladLokace
{
    public class LokacePohyb
    {
        /// <summary>
        /// ID pohybu, potrebuje se kvuli jedinecne identifikaci zaznamu pri logovani
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// identifikator materialu prevzaty z IS (itemnmbr)
        /// </summary>
        public string ITEMNMBR { get; set; }
        /// <summary>
        /// pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS)
        /// </summary>
        public string DOCUMENT_NUMBER { get; set; }
        /// <summary>
        /// typ pohybu (P - prijem, V - vydej, D - defragmentace/prelokovani)
        /// </summary>
        public TypeOfRecord POHYB_TYPE { get; set; }
        /// <summary>
        /// zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
        /// </summary>
        public string POHYB_SRC { get; set; }
        /// <summary>
        /// zdroj, ktery vlozil data do databaze (T - terminal -> online f. ze ctecky, S - Server -> vlozeni do DB pri zpracovani davky)
        /// </summary>
        public string SOURCE { get; set; }
        /// <summary>
        /// puvodni mnozstvi (mnozstvi, ktere bylo pri prvnim vlozeni)
        /// </summary>
        public decimal QTYSHPPD_DEF { get; set; }
        /// <summary>
        /// mnozstvi
        /// </summary>
        public decimal QTYSHPPD { get; set; }
        // QTYSHPPD_DEX pridat??
        /// <summary>
        /// sarze
        /// </summary>
        public string SERLTNUM { get; set; }
        /// <summary>
        /// zdrojovy sklad
        /// </summary>
        public string SKL_ID_SRC { get; set; }
        /// <summary>
        /// cilovy sklad
        /// </summary>
        public string SKL_ID_DST { get; set; }
        /// <summary>
        /// zdrojova lokace
        /// </summary>
        public string LOCNCODE_SRC { get; set; }
        /// <summary>
        /// cilova lokace
        /// </summary>
        public string LOCNCODE_DST { get; set; }
        /// <summary>
        /// id uzivatele, ktery provedl akci
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// id terminalu
        /// </summary>
        public int TermID { get; set; }
        /// <summary>
        /// guid pohybu (shodny se zaznamem z tabulky czmst_di, _pi, ...)
        /// </summary>
        public Guid guid { get; set; }
        /// <summary>
        /// datum a cas, kdy server zaznam zpracoval
        /// </summary>
        public DateTime dateeveS { get; set; }
        /// <summary>
        /// datum a cas, kdy terminal zaznam zpracoval
        /// </summary>
        public DateTime dateeveT { get; set; }
        /// <summary>
        /// expirace
        /// </summary>
        public DateTime? Expiration { get; set; }
        /// <summary>
        /// nazev (itemdesc)
        /// </summary>
        public string ITEMDESC { get; set; }
        /// <summary>
        /// cislo davky, ktera se zpracovala (pokud bude vyplneno, bylo to pres terminal, jinak NULL)
        /// </summary>
        public int? CountEntries { get; set; }

        /// <summary>
        /// ID pracovnika kteremu se prirazuje polozka
        /// </summary>
        public string PRAC_ID_OWNER { get; set; }

        /// <summary>
        /// pocet co se prirazuje pracovnikovy
        /// </summary>
        public decimal QTY_OWNER { get; set; }



    }
}
