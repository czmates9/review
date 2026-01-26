using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fask.Module.Ingres.SAD.Classes
{
    /// <summary>
    /// Jaka data se maji zobrazit.
    /// </summary>
    public enum ZOBRAZENI_DAT
    {
        /// <summary>
        /// Zobrazeni pouze aktualnich dat
        /// </summary>
        Aktualni,
        /// <summary>
        /// Zobrazeni pouze archivnich dat
        /// </summary>
        Archivni,
        /// <summary>
        /// Zobrazeni aktualnich a archivnich dat
        /// </summary>
        Aktualni_a_Archivni
    }

    /// <summary>
    /// typ vypoctu stavu
    /// </summary>
    public enum VYPOCET_STAVU
    {
        /// <summary>
        /// Vypocet dle polozky.
        /// </summary>
        Polozky,
        /// <summary>
        /// Vypocet dle polozky a lokace
        /// </summary>
        Polozky_a_Lokace,
        /// <summary>
        /// Vypocet podle polozky a sarze
        /// </summary>
        Polozka_a_Sarze
    }

    /// <summary>
    /// Slouzi k navratovemu statusu pri vkladani variant lokaci pro material z inventury.
    /// </summary>
    public enum INVENTURA_PLNENI_VARIANT_STATUS
    {
        OK,
        ERROR,
        ERROR_VICE_LOKACI
    }

    public enum EXPORT_DAT
    {
        VSE,
        OZNACENE
    }

    /// <summary>
    /// Slouží k nastaveni typu zobrazeni Formu
    /// </summary>
    public enum ZOBRAZENI_TYP
    {
        LIST,
        VYBER,
        UNKNOWN,
        POHLED
    }

    public enum TypZdrojeDat
    {
        CZMST_SE,
        CZMST_SI,
        CZMST_DI,
        ProductionSources
    }


    public enum TypPolozky
    {
        Vsechny = 0,

        Karta = 1,

        Textova = 2,

        Sluzba = 3,

        Komplet = 4,

        Vyrobek = 5,

        Souprava = 6
    }

    public enum ZaplanovanyDoklad
    {
        Vyroba,

        Vydej
    }

    public enum VyrobaStavPrikazu
    {
        Aktivni = 1,
        Neaktivni = 0,
        Ukoncen = 200
    }
}
