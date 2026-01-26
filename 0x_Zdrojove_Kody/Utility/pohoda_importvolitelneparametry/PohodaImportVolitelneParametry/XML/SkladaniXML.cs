using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PohodaImportVolitelneParametry.XML
{
    public static class SkladaniXML
    {

        public static void SkladaniXMLSeznam(string filename, string ICO)
        {

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";

            string ID = "001";
            //string ICO = "64086551";

            #region FXTS

            List<XElement> DataPackItem_FXTS = new List<XElement>();

            List<XElement> ItemUserCode_FXTS = new List<XElement>();

            ItemUserCode_FXTS.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Množství"), //v IS POHODA je to Popis
                new XAttribute("code", "Množství"), //v IS POHODA je to Zkratka
                new XAttribute("constant", "1")));// v IS POHODA je to Konstanta

            ItemUserCode_FXTS.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Seriové číslo"), //v IS POHODA je to Popis
                new XAttribute("code", "SN"),  //v IS POHODA je to Zkratka
                new XAttribute("constant", "2"))); // v IS POHODA je to Konstanta


            ItemUserCode_FXTS.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Šarže"), //v IS POHODA je to Popis
                new XAttribute("code", "Šarže"), //v IS POHODA je to Zkratka
                new XAttribute("constant", "3")));// v IS POHODA je to Konstanta

            XElement fxts = new XElement(lst + "listUserCode",
                        new XAttribute("version", "1.1"),
                        new XAttribute("code", "FXTS"),
                        new XAttribute("constants", "true"),
                        new XAttribute("name", "Hodnota typu sledování"),
                    ItemUserCode_FXTS
                            );

            DataPackItem_FXTS.Add(
                    new XElement(dat + "dataPackItem",
                                new XAttribute("version", "2.0"),
                                new XAttribute("id", "IMP" + ID),
                                fxts
                                )
                            );
            #endregion

            #region TIMEMODE

            List<XElement> DataPackItem_TIMEMODE = new List<XElement>();

            List<XElement> ItemUserCode_TIMEMODE = new List<XElement>();

            ItemUserCode_TIMEMODE.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Stop výroby"), //v IS POHODA je to Popis
                new XAttribute("code", "SV"), //v IS POHODA je to Zkratka
                new XAttribute("constant", "1")));// v IS POHODA je to Konstanta

            ItemUserCode_TIMEMODE.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Start / Stop výroby"), //v IS POHODA je to Popis
                new XAttribute("code", "SSV"),  //v IS POHODA je to Zkratka
                new XAttribute("constant", "2"))); // v IS POHODA je to Konstanta


            ItemUserCode_TIMEMODE.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "StartStop přip. StartStop výr."), //v IS POHODA je to Popis
                new XAttribute("code", "SSPSSV"), //v IS POHODA je to Zkratka
                new XAttribute("constant", "3")));// v IS POHODA je to Konstanta


            XElement TIMEMODE = new XElement(lst + "listUserCode",
                        new XAttribute("version", "1.1"),
                        new XAttribute("code", "TIMEMODE"),
                        new XAttribute("constants", "true"),
                        new XAttribute("name", "Typ sledování"),
                    ItemUserCode_TIMEMODE
                            );


            DataPackItem_TIMEMODE.Add(
                            new XElement(dat + "dataPackItem",
                                        new XAttribute("version", "2.0"),
                                        new XAttribute("id", "IMP" + ID),
                                        TIMEMODE
                                        )
                                     );

            #endregion

            #region FPV

            List<XElement> DataPackItem_FPV = new List<XElement>();

            List<XElement> ItemUserCode_FPV = new List<XElement>();

            ItemUserCode_FPV.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Zaplánován"), //v IS POHODA je to Popis
                new XAttribute("code", "A"), //v IS POHODA je to Zkratka
                new XAttribute("constant", "1")));// v IS POHODA je to Konstanta

            ItemUserCode_FPV.Add(new XElement(lst + "itemUserCode",
                new XAttribute("name", "Kompletně Zaplánován"), //v IS POHODA je to Popis
                new XAttribute("code", "B"),  //v IS POHODA je to Zkratka
                new XAttribute("constant", "2"))); // v IS POHODA je to Konstanta

            XElement FPV = new XElement(lst + "listUserCode",
                        new XAttribute("version", "1.1"),
                        new XAttribute("code", "FPV"),
                        new XAttribute("constants", "true"),
                        new XAttribute("name", "plánování výroby"),
                    ItemUserCode_FPV
                            );


            DataPackItem_FPV.Add(
                            new XElement(dat + "dataPackItem",
                                        new XAttribute("version", "2.0"),
                                        new XAttribute("id", "IMP" + ID),
                                        FPV
                                        )
                                     );

            #endregion

            XElement root = new XElement(dat + "dataPack",
                                            new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                                            new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                                            new XAttribute("id", "IMP" + ID),
                                            new XAttribute("ico", ICO),
                                            new XAttribute("application", "MST_Pohoda"),
                                            new XAttribute("version", "2.0"),
                                            new XAttribute("note", "uživatelské seznamy"),
                                            DataPackItem_FXTS,
                                            DataPackItem_TIMEMODE,
                                            DataPackItem_FPV
                                            );


            root.Save(filename);


        }


        public static void SkladaniXMLVolitelneParametry(string filename, string ICO, string ID_FXTS, string ID_TIMEMODE, string ID_FPV, bool Grafika)
        {


            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
            XNamespace prm = "http://www.stormware.cz/schema/version_2/parameter.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            string ID = "002";

            #region Zasoby

            #region Jednotlive paramety

            List<XElement> parameterDef = new List<XElement>();

            #region Bool VPr

            //
            //priklad
            //
            //< prm:parameterDef >
            //    < prm:label > Příznak sledování Prodej</ prm:label >
            //    < prm:name > VPrFDTS </ prm:name >
            //    < prm:type > boolean </ prm:type >
            //</ prm:parameterDef >

            parameterDef.Add(new XElement(prm + "parameterDef",
                              new XElement(prm + "label", "Příznak sledování Výdej"),
                              new XElement(prm + "name", "VPrFVTS"),
                              new XElement(prm + "type", "boolean")
             ));

            parameterDef.Add(new XElement(prm + "parameterDef",
                                                new XElement(prm + "label", "Příznak sledování Příjem"),
                                                new XElement(prm + "name", "VPrFPTS"),
                                                new XElement(prm + "type", "boolean")
                               ));

            parameterDef.Add(new XElement(prm + "parameterDef",
                                                new XElement(prm + "label", "Příznak sledování Prodej"),
                                                new XElement(prm + "name", "VPrFDTS"),
                                                new XElement(prm + "type", "boolean")
                               ));

            parameterDef.Add(new XElement(prm + "parameterDef",
                                                new XElement(prm + "label", "Příznak sledování Inventura"),
                                                new XElement(prm + "name", "VPrFITS"),
                                                new XElement(prm + "type", "boolean")
                                ));


            parameterDef.Add(new XElement(prm + "parameterDef",
                                                new XElement(prm + "label", "Příznak sledování Vše"),
                                                new XElement(prm + "name", "VPrFXTS"),
                                                new XElement(prm + "type", "boolean")
                               ));

            #endregion

            #region Seznam RefVpr

            //
            //Priklad
            //
            //< prm:parameterDef >
            //    < prm:label > Typ sledování Inventura</ prm:label >
            //    < prm:name > RefVPrFITS </ prm:name >
            //    < prm:type > list </ prm:type >
            //    < prm:list >
            //        < typ:id > 802 </ typ:id > 
            //    </ prm:list >   
            //</ prm:parameterDef >


            parameterDef.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Typ sledování Inventura"),
                        new XElement(prm + "name", "RefVPrFITS"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_FXTS.Trim())
                   )));

            parameterDef.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Typ sledování Prodej"),
                        new XElement(prm + "name", "RefVPrFDTS"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_FXTS.Trim())
                   )));

            parameterDef.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Typ sledování Vše"),
                        new XElement(prm + "name", "RefVPrFXTS"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_FXTS.Trim())
                   )));

            parameterDef.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Typ sledování Příjem"),
                        new XElement(prm + "name", "RefVPrFPTS"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_FXTS.Trim())
                   )));

            parameterDef.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Typ sledování Výdej"),
                        new XElement(prm + "name", "RefVPrFVTS"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_FXTS.Trim())
                   )));


            #endregion

            #region Vyroba parametry

            parameterDef.Add(new XElement(prm + "parameterDef",
                  new XElement(prm + "label", "Přípravný čas"),
                  new XElement(prm + "name", "VPrTIMEPREP"),
                  new XElement(prm + "type", "number")
                                            ));

            parameterDef.Add(new XElement(prm + "parameterDef",
                  new XElement(prm + "label", "Jednotkový čas"),
                  new XElement(prm + "name", "VPrTIMEUNIT"),
                  new XElement(prm + "type", "number")
                                            ));

            parameterDef.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Typ sledovaní"),
                        new XElement(prm + "name", "RefVPrTIMEMODE"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_TIMEMODE.Trim())
                                          )));

            #endregion

            #endregion

            #region Grafika

            List<XElement> userForm = new List<XElement>();

            //
            //Priklad
            //
            //< prm:ctrlDef >
            //< prm:name />
            //< prm:label > Volitelné parametry </ prm:label >
            //< prm:rect left = "0" top = "0" right = "759" bottom = "491" />
            //< prm:type > group </ prm:type >
            //< prm:style > 0 </ prm:style >
            //</ prm:ctrlDef >


            userForm.Add(new XElement(prm + "ctrlDef",
new XElement(prm + "name", string.Empty),
new XElement(prm + "label", "Volitelné parametry"),
new XElement(prm + "rect", new XAttribute("left", "0"), new XAttribute("top", "0"), new XAttribute("right", "759"), new XAttribute("bottom", "491")),
new XElement(prm + "type", "group"),
new XElement(prm + "style", "0")
));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "VPrFITS"),
    new XElement(prm + "label", "Příznak sledování Inventura"),
    new XElement(prm + "rect", new XAttribute("left", "33"), new XAttribute("top", "105"), new XAttribute("right", "197"), new XAttribute("bottom", "125")),
    new XElement(prm + "type", "check"),
    new XElement(prm + "style", "0")
    ));


            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "VPrFXTS"),
    new XElement(prm + "label", "Příznak sledování Vše"),
    new XElement(prm + "rect", new XAttribute("left", "33"), new XAttribute("top", "135"), new XAttribute("right", "197"), new XAttribute("bottom", "155")),
    new XElement(prm + "type", "check"),
    new XElement(prm + "style", "0")
    ));


            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", string.Empty),
    new XElement(prm + "label", "Typ sledování Vše"),
    new XElement(prm + "rect", new XAttribute("left", "215"), new XAttribute("top", "165"), new XAttribute("right", "369"), new XAttribute("bottom", "188")),
    new XElement(prm + "type", "text"),
    new XElement(prm + "style", "0")
    ));


            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "RefVPrFXTS"),
    new XElement(prm + "label", string.Empty),
    new XElement(prm + "rect", new XAttribute("left", "353"), new XAttribute("top", "165"), new XAttribute("right", "453"), new XAttribute("bottom", "186")),
    new XElement(prm + "type", "list"),
    new XElement(prm + "style", "1")
    ));


            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", string.Empty),
    new XElement(prm + "label", "Fask Typy sledování"),
    new XElement(prm + "rect", new XAttribute("left", "16"), new XAttribute("top", "24"), new XAttribute("right", "473"), new XAttribute("bottom", "212")),
    new XElement(prm + "type", "group"),
    new XElement(prm + "style", "4")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "VPrFPTS"),
    new XElement(prm + "label", "Příznak sledování Příjem"),
    new XElement(prm + "rect", new XAttribute("left", "33"), new XAttribute("top", "45"), new XAttribute("right", "197"), new XAttribute("bottom", "65")),
    new XElement(prm + "type", "check"),
    new XElement(prm + "style", "0")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "VPrFVTS"),
    new XElement(prm + "label", "Příznak sledování Výdej"),
    new XElement(prm + "rect", new XAttribute("left", "33"), new XAttribute("top", "75"), new XAttribute("right", "197"), new XAttribute("bottom", "95")),
    new XElement(prm + "type", "check"),
    new XElement(prm + "style", "0")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", string.Empty),
    new XElement(prm + "label", "Typ sledování Příjem"),
    new XElement(prm + "rect", new XAttribute("left", "213"), new XAttribute("top", "45"), new XAttribute("right", "367"), new XAttribute("bottom", "65")),
    new XElement(prm + "type", "text"),
    new XElement(prm + "style", "0")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "RefVPrFPTS"),
    new XElement(prm + "label", string.Empty),
    new XElement(prm + "rect", new XAttribute("left", "353"), new XAttribute("top", "45"), new XAttribute("right", "453"), new XAttribute("bottom", "65")),
    new XElement(prm + "type", "list"),
    new XElement(prm + "style", "1")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", string.Empty),
    new XElement(prm + "label", "Typ sledování Výdej"),
    new XElement(prm + "rect", new XAttribute("left", "213"), new XAttribute("top", "75"), new XAttribute("right", "367"), new XAttribute("bottom", "95")),
    new XElement(prm + "type", "text"),
    new XElement(prm + "style", "0")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "RefVPrFVTS"),
    new XElement(prm + "label", string.Empty),
    new XElement(prm + "rect", new XAttribute("left", "353"), new XAttribute("top", "75"), new XAttribute("right", "453"), new XAttribute("bottom", "95")),
    new XElement(prm + "type", "list"),
    new XElement(prm + "style", "1")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", string.Empty),
    new XElement(prm + "label", "Typ sledování Prodej"),
    new XElement(prm + "rect", new XAttribute("left", "213"), new XAttribute("top", "105"), new XAttribute("right", "367"), new XAttribute("bottom", "125")),
    new XElement(prm + "type", "text"),
    new XElement(prm + "style", "0")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
    new XElement(prm + "name", "RefVPrFITS"),
    new XElement(prm + "label", string.Empty),
    new XElement(prm + "rect", new XAttribute("left", "353"), new XAttribute("top", "105"), new XAttribute("right", "453"), new XAttribute("bottom", "125")),
    new XElement(prm + "type", "list"),
    new XElement(prm + "style", "1")
    ));

            userForm.Add(new XElement(prm + "ctrlDef",
new XElement(prm + "name", string.Empty),
new XElement(prm + "label", "Typ sledování Inventura"),
new XElement(prm + "rect", new XAttribute("left", "213"), new XAttribute("top", "135"), new XAttribute("right", "367"), new XAttribute("bottom", "155")),
new XElement(prm + "type", "text"),
new XElement(prm + "style", "0")
));

            userForm.Add(new XElement(prm + "ctrlDef",
new XElement(prm + "name", "RefVPrFDTS"),
new XElement(prm + "label", string.Empty),
new XElement(prm + "rect", new XAttribute("left", "353"), new XAttribute("top", "135"), new XAttribute("right", "453"), new XAttribute("bottom", "155")),
new XElement(prm + "type", "list"),
new XElement(prm + "style", "1")
));

            userForm.Add(new XElement(prm + "ctrlDef",
new XElement(prm + "name", "VPrFDTS"),
new XElement(prm + "label", "Příznak sledování Prodej"),
new XElement(prm + "rect", new XAttribute("left", "33"), new XAttribute("top", "165"), new XAttribute("right", "197"), new XAttribute("bottom", "185")),
new XElement(prm + "type", "check"),
new XElement(prm + "style", "0")
));



            #endregion

            XElement Parameter_Zas = null;

            if (Grafika)
                Parameter_Zas = new XElement(prm + "parameter", new XAttribute("version", "2.0"), new XAttribute("idsAgenda", "zasoby"), new XElement(prm + "formParameter", parameterDef), new XElement(prm + "userForm", userForm));
            else
                Parameter_Zas = new XElement(prm + "parameter", new XAttribute("version", "2.0"), new XAttribute("idsAgenda", "zasoby"), new XElement(prm + "formParameter", parameterDef));

            XElement dataPackItem_Zasoby = new XElement(dat + "dataPackItem", new XAttribute("version", "2.0"), new XAttribute("id", "IMP" + ID), Parameter_Zas);

            #endregion

            #region Objednavky

            //velke TODO, jak nastavit aby parametry byly pouze na přijatej objednavke?

            List<XElement> parameterDef_Obj = new List<XElement>();

            parameterDef_Obj.Add(new XElement(prm + "parameterDef",
                          new XElement(prm + "label", "Zaplánováno"),
                          new XElement(prm + "name", "VPrQTY"),
                          new XElement(prm + "type", "number")
                                ));

            parameterDef_Obj.Add(new XElement(prm + "parameterDef",
                        new XElement(prm + "label", "Příznak"),
                        new XElement(prm + "name", "RefVPrPVF"),
                        new XElement(prm + "type", "list"),
                        new XElement(prm + "list",
                                new XElement(typ + "id", ID_FPV.Trim())
                                          )));


            XElement Parameter_Obj = null;

            //if (Grafika)
            //    Parameter_Zas = new XElement(prm + "parameter", new XAttribute("version", "2.0"), new XAttribute("idsAgenda", "zasoby"), new XElement(prm + "itemParameter", parameterDef_Obj), new XElement(prm + "userForm", userForm));
            //else
            Parameter_Obj = new XElement(prm + "parameter", new XAttribute("version", "2.0"), new XAttribute("idsAgenda", "objednavky"), new XElement(prm + "itemParameter", parameterDef_Obj));


            XElement dataPackItem_Objednavky = new XElement(dat + "dataPackItem", new XAttribute("version", "2.0"), new XAttribute("id", "IMP" + ID), Parameter_Obj);


            #endregion


            XElement root = new XElement(dat + "dataPack",
                                            new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                                            new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                                            new XAttribute(XNamespace.Xmlns + "prm", "http://www.stormware.cz/schema/version_2/parameter.xsd"),
                                            new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                                            new XAttribute("id", "IMP" + ID),
                                            new XAttribute("ico", ICO),
                                            new XAttribute("application", "MST_Pohoda"),
                                            new XAttribute("version", "2.0"),
                                            new XAttribute("note", "Volitelné parametry"),
                                            dataPackItem_Zasoby,
                                            dataPackItem_Objednavky
                                            );

            root.Save(filename);

        }


    }
}
