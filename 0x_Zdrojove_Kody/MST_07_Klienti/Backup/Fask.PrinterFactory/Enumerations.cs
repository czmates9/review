using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.PrinterFactory
{
    //public enum PrinterTypes
    //{
    //    None,
    //    WebService,
    //    Bluetooth,
    //    WebService2
    //}

    public enum PrinterModules
    {
        PrijemPredloha,
        PrijemNasnimane,
		PrijemPaletovylistek,
		PrijemPaletaHlavicka,
		PrijemPaletaPaticka,
		PrijemPaletaRadek,

        VydejPredloha,
        VydejNasnimane,
        VydejPaletovylistek,
        VydejPaletaHlavicka,
        VydejPaletaPaticka,
        VydejPaletaRadek,
        VydejSoupiskaHlavicka,
        VydejSoupiskaPaticka,
        VydejSoupiskaRadek,

        ProdejPredloha,
        ProdejNasnimane,
        ProdejSoupisHlavicka,
        ProdejSoupisRadek,
        ProdejSoupisPaticka,
        ProdejPaletaHlavicka,
        ProdejPaletaRadek,
        ProdejPaletaPaticka,


        InventuraPredloha,
        InventuraNasnimane,


        TextVolny,
        Baleni,         // JimiTore
        PrijemZbytku,    // JimiTore
        JimiTorePrijem,

        ExpediceSoupisHlavicka,
        ExpediceSoupisRadek,
        ExpediceSoupisPaticka,
        ExpedicePaletaHlavicka,
        ExpedicePaletaRadek,
        ExpedicePaletaPaticka,

		VyrobaOdvedene
    }
}
