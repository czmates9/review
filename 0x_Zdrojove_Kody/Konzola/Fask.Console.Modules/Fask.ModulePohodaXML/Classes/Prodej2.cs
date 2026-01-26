using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace Fask.ModulePohodaXML.Classes
{
    //public enum TypDokladu
    //{
    //    vyd,
    //    pri,
    //    pro,
    //    objv,
    //    objvcm,
    //    objvm,
    //    objbezodb,
    //    objp,
    //    objpcm,
    //    objpm,
    //    expedice,
    //    pre
    //}

    /// <summary>
    /// Enum ktere definuje typy podporovanych dokladu
    /// </summary>
    public enum TypDokladu
    {
        vyd_k,        //1
        pri_k,        //0
        pro_k,        //2
        objv_k,       //3
        objvcm,     //4
        objvm,      //5
        objbezodb,  //6
        objp,       //7
        objpcm,     //8
        objpm,      //9
        expedice,   //10
        pre_k,        //11
        Unknow
    }

    public static class Prodej2
    {

        public const string prodej_unknown = XML.MST_Pohoda._unknown + ".xml";
        public const string prodej_import_vydejka = XML.MST_Pohoda._import_vydejka + ".xml";
        public const string prodej_import_prijemka = XML.MST_Pohoda._import_prijemka + ".xml";
        public const string prodej_import_prodejka = XML.MST_Pohoda._import_prodejka + ".xml";
        public const string prodej_import_prevodka = XML.MST_Pohoda._import_prevodka + ".xml";
        public const string prodej_import_objp = XML.MST_Pohoda._import_objednavka_prijata + ".xml";
        public const string prodej_import_objv = XML.MST_Pohoda._import_objednavka_vydana + ".xml";

        /// <summary>
        /// Metoda sloužíci pro vraceni nazvu souboru podle typu dokladu
        /// </summary>
        /// <param name="typDoklad"> Enum Typ Dokladu</param>
        /// <returns></returns>
        public static string FilenameComposeTypDoklad(TypDokladu typDoklad)
        {
            //string filename = string.Empty;

            try
            {
                //if (typDoklad == null)
                //    return XML.MST_Pohoda.FilenameCompose(prodej_unknown);


                switch (typDoklad)
                {
                    case TypDokladu.vyd_k:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_vydejka);
                    case TypDokladu.pri_k:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_prijemka);
                    case TypDokladu.pro_k:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_prodejka);
                    case TypDokladu.objv_k:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objv);
                    case TypDokladu.objvcm:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objv);
                    case TypDokladu.objvm:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objv);
                    case TypDokladu.objbezodb:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objv);
                    case TypDokladu.objp:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objp);
                    case TypDokladu.objpcm:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objp);
                    case TypDokladu.objpm:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_objp);
                    case TypDokladu.expedice:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_vydejka);
                    case TypDokladu.pre_k:
                        return XML.MST_Pohoda.FilenameCompose(prodej_import_prevodka);
                    default:
                        return XML.MST_Pohoda.FilenameCompose(prodej_unknown);
                }

                //return XML.MST_Pohoda.FilenameCompose(prodej_unknown);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return XML.MST_Pohoda.FilenameCompose(prodej_unknown);
            }


        }

        private static XElement Vypln_AttributeToSN(XNamespace XName, XNamespace typ, Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row)
        {
            XElement ele = null;
            List<XElement> listVPrParams = new List<XElement>();

            var attr = Database.Pohoda.SKzVC_AttributeToSN(row.ITEMNMBR, row.SERLTNUM);

            if (attr.Count == 1)
            {
                var radek = attr.First();

                if (!radek.IsVPrSarzeKSNNull() && !string.IsNullOrEmpty(radek.VPrSarzeKSN))
                {


                    List<XElement> listVPr = new List<XElement>();
                    var name = new XElement(typ + "name", "VPrSarzeKSN");
                    var textValue = new XElement(typ + "textValue", radek.VPrSarzeKSN);
                    listVPr.Add(name);
                    listVPr.Add(textValue);

                    listVPrParams.Add(new XElement(typ + "parameter", listVPr));
                }


                if (!radek.IsVPrExspiraceKSNNull())
                {
                    List<XElement> listVPrEXP = new List<XElement>();
                    var nameEXP = new XElement(typ + "name", "VPrExspiraceKSN");
                    var textValueEXP = new XElement(typ + "datetimeValue", XmlConvert.ToString(radek.VPrExspiraceKSN, "yyyy-MM-dd"));
                    listVPrEXP.Add(nameEXP);
                    listVPrEXP.Add(textValueEXP);

                    listVPrParams.Add(new XElement(typ + "parameter", listVPrEXP));

                }

                ele = new XElement(XName + "parameters", listVPrParams);

            }

            return ele;
        }


        /// <summary>
        /// TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Pre_XML(string filename)
        {
            return "OK";
        }

        /// <summary>
        /// TODO predelat na lepsi reakci
        /// </summary>
        /// <param name="filename">cesta k souboru</param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        //internal static string LoadResponse_Pri_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
        internal static string LoadResponse_Pri_XML(string filename)
        {
            //string file = Globals.PathToInputDirectory + "Response\\" + filename;

            //TODO Predelat na lepe.... Kdyz se 

            //XML.MST_Pohoda.UpdateCreatorSKPP(uzivatel, filename);


            return "OK";
            //throw new NotImplementedException();
        }

        /// <summary>
        /// TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Objv_XML(string filename)
        {
            return "OK";
        }

        /// <summary>
        /// TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Pro_XML(string filename)
        {
            return "OK";
        }

        /// <summary>
        /// TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Vyd_XML(string filename)
        {
            return "OK";
        }

        /// <summary>
        /// Metoda pro Vytvořeni requestu Prijemky z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="note">poznamka</param>
        /// <param name="dt_DI"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Pri_XML(string filename, string note, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt_DI, Doklad typDoklad)
        {
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            try
            {
                //Fask.DataSets.ProdejData.CZMST_DIRow Row_DI;
                int objednavka = dt_DI[0].CountEntries;

                //var data = dt_DI.OrderBy(x => x.DEX_ROW_ID);
                //Row_DI = data.First();

                string partnerID = string.Empty;
                foreach (Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row in dt_DI)
                {
                    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
                    {
                        //Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
                        partnerID = row.ODB_ID.Trim();
                        break;
                    }
                }


                int? YEAR = Database.Pohoda.GetYearByPrelom();
                string idsradadokladu = Database.Pohoda.GetIDRadyByYearSText(typDoklad, YEAR);

                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(pri + "text", "MST Vytvořeno z volného pohybu"));

                if (!string.IsNullOrEmpty(note))
                {
                    listHeader.Add(new XElement(pri + "note", note));
                }

                listHeader.Add(new XElement(pri + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

                if (!string.IsNullOrEmpty(partnerID))
                {
                    XElement IDPartnerFakturacni;

                    IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

                    listHeader.Add(new XElement(pri + "partnerIdentity", IDPartnerFakturacni));
                }

                //MaR zakomentoval 23.6.2025
                //if (idsradadokladu != null)
                //{
                //    listHeader.Add(new XElement(pri + "number", new XElement(typ + "id", idsradadokladu)));
                //}

                if (!string.IsNullOrEmpty(idsradadokladu))
                {
                    listHeader.Add(new XElement(pri + "number", new XElement(typ + "id", idsradadokladu)));
                }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in dt_DI)
                {
                    List<XElement> prijemkaItem = new List<XElement>();

                    prijemkaItem.Add(new XElement(pri + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
                    prijemkaItem.Add(new XElement(pri + "unit", row.MJ.Trim()));
                    prijemkaItem.Add(new XElement(pri + "coefficient", 1));
                    prijemkaItem.Add(new XElement(pri + "discountPercentage", 0));

                    if (row.SERLTNUM.Trim().Length > 0)
                        prijemkaItem.Add(new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        prijemkaItem.Add(new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    XElement polozka = new XElement(pri + "prijemkaItem", prijemkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                #region Summary

                List<XElement> vydejkaSummary = new List<XElement>();


                #endregion



                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "pri", "http://www.stormware.cz/schema/version_2/prijemka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "PRI_K" + objednavka.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import prijemky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "PRI_K" + objednavka.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(pri + "prijemka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(pri + "prijemkaHeader", listHeader),
                                    new XElement(pri + "prijemkaDetail", listItem),
                                    new XElement(pri + "prijemkaSummary", vydejkaSummary)
                                        )));

                root.Save(filename);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Metoda pro Vytvořeni requestu Vydejky z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="p">poznamka</param>
        /// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Vyd_XML(string filename, string p, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
        {

            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
            XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

            try
            {

                int objednavka = cZMST_DIDataTable[0].CountEntries;

                string partnerID = string.Empty;
                foreach (Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row in cZMST_DIDataTable)
                {
                    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
                    {
                        partnerID = row.ODB_ID.Trim();
                        break;
                    }
                }

                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(vyd + "text", "MST Vytvořeno z volneho pohybu"));
                listHeader.Add(new XElement(vyd + "note", objednavka));

                listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

                if (!string.IsNullOrEmpty(partnerID))
                {
                    XElement IDPartnerFakturacni;

                    IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

                    listHeader.Add(new XElement(vyd + "partnerIdentity", IDPartnerFakturacni));
                }


                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in cZMST_DIDataTable)
                {
                    List<XElement> VydejkaItem = new List<XElement>();

                    VydejkaItem.Add(new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
                    VydejkaItem.Add(new XElement(vyd + "unit", row.MJ.Trim()));
                    VydejkaItem.Add(new XElement(vyd + "coefficient", 1));
                    VydejkaItem.Add(new XElement(vyd + "discountPercentage", 0));

                    if (row.SERLTNUM.Trim().Length > 0)
                        VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


                    if (!row.IsEXPIRACENull())
                    {
                        VydejkaItem.Add(new XElement(vyd + "expirationDate", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd")));
                    }

                    #region AttributeToSN

                    var ele = Vypln_AttributeToSN(vyd, typ, row);

                    if (ele != null)
                        VydejkaItem.Add(ele);

                    #endregion

                    XElement polozka = new XElement(vyd + "vydejkaItem", VydejkaItem);
                    listItem.Add(polozka);
                }

                #endregion

                #region Summary

                List<XElement> Summary = new List<XElement>();


                #endregion

                XElement VydjkaElement = null;

                if (typDoklad.Prodej_Vydejka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Vydejka_Tisk_Tiskarna))
                {

                    List<XElement> print = new List<XElement>();
                    List<XElement> printSettings = new List<XElement>();

                    //Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
                    printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Vydejka_Tisk_ID_sablona.Value.ToString())));

                    //pocet vytisku projistotu je napevno jeden...
                    printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

                    //dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
                    printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Vydejka_Tisk_Tiskarna.Trim()));

                    print.Add(new XElement(prn + "printerSettings", printSettings));


                    VydjkaElement = new XElement(vyd + "vydejka",
                               new XAttribute("version", "2.0"),
                               new XElement(vyd + "vydejkaHeader", listHeader),
                               new XElement(vyd + "vydejkaDetail", listItem),
                               new XElement(vyd + "vydejkaSummary", Summary),
                               new XElement(vyd + "print", print)
                                   );
                }
                else
                {
                    VydjkaElement = new XElement(vyd + "vydejka",
                               new XAttribute("version", "2.0"),
                               new XElement(vyd + "vydejkaHeader", listHeader),
                               new XElement(vyd + "vydejkaDetail", listItem),
                               new XElement(vyd + "vydejkaSummary", Summary)
                                   );
                }

                XElement root = new XElement(dat + "dataPack",
                                            new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                                            new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
                                            new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                                            new XAttribute("id", "VYD_K" + objednavka.ToString()),
                                            new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                                            new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                                            new XAttribute("version", "2.0"),
                                            new XAttribute("note", "Import vydejky"),
                                            new XElement(dat + "dataPackItem",
                                                new XAttribute("id", "VYD_K" + objednavka.ToString()),
                                                new XAttribute("version", "2.0"),
                                            VydjkaElement));

                root.Save(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Metoda pro Vytvořeni requestu Prodejky z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="p">poznamka</param>
        /// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Pro_XML(string filename, string p, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
        {

            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace pro = "http://www.stormware.cz/schema/version_2/prodejka.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            try
            {

                #region Grupovani DI

                //if (Properties.Settings.Default.Prodej_GrupujDataPrijemka)
                //{


                //    //List<IGrouping<a, Fask.DataSets.ProdejData.CZMST_DIRow>>
                //    var DIList = cZMST_DIDataTable.GroupBy(x => new 
                //    { 
                //        x.CountEntries,
                //        x.VNDITNUM,
                //        x.CZ_CarKod,
                //        x.ODB_ID,
                //        x.SKL_ID,
                //        x.ITEMNMBR,
                //        x.

                //    }).ToList();

                //}


                #endregion



                Fask.Interfaces.DataSets.Prodej.CZMST_DIRow Row_DI;


                var data = cZMST_DIDataTable.OrderBy(x => x.DEX_ROW_ID);
                Row_DI = data.First();

                string partnerID = string.Empty;
                foreach (Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row in cZMST_DIDataTable)
                {
                    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
                    {
                        //Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
                        partnerID = row.ODB_ID.Trim();
                        break;
                    }
                }


                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(pro + "text", "MST Vytvořeno z volneho pohybu"));

                if (!string.IsNullOrEmpty(p))
                {
                    listHeader.Add(new XElement(pro + "note", p));
                }

                listHeader.Add(new XElement(pro + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

                if (!string.IsNullOrEmpty(partnerID))
                {
                    XElement IDPartnerFakturacni;

                    IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

                    listHeader.Add(new XElement(pro + "partnerIdentity", IDPartnerFakturacni));
                }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in cZMST_DIDataTable)
                {
                    List<XElement> prodejkaItem = new List<XElement>();

                    prodejkaItem.Add(new XElement(pro + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
                    prodejkaItem.Add(new XElement(pro + "unit", row.MJ.Trim()));
                    prodejkaItem.Add(new XElement(pro + "coefficient", 1));
                    prodejkaItem.Add(new XElement(pro + "discountPercentage", 0));

                    if (row.SERLTNUM.Trim().Length > 0)
                        prodejkaItem.Add(new XElement(pro + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        prodejkaItem.Add(new XElement(pro + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    XElement polozka = new XElement(pro + "prodejkaItem", prodejkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                #region Summary

                List<XElement> Summary = new List<XElement>();


                #endregion



                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "pro", "http://www.stormware.cz/schema/version_2/prodejka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "PRO_K" + Row_DI.DEX_ROW_ID.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import prodejky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "PRO_K" + Row_DI.DEX_ROW_ID.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(pro + "prodejka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(pro + "prodejkaHeader", listHeader),
                                    new XElement(pro + "prodejkaDetail", listItem),
                                    new XElement(pro + "prodejkaSummary", Summary)
                                        )));

                root.Save(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;


        }


        /// <summary>
        /// Metoda pro Vytvořeni requestu Prevodky z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="p">poznamka</param>
        /// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Pre_XML(string filename, string p, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
        {
            #region OLD

            //tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //tw.WriteLine("<dat:dataPack id=\"pre" + davkacislo + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import Prevodky\"");
            //tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
            //tw.WriteLine("xmlns:pre=\"http://www.stormware.cz/schema/version_2/prevodka.xsd\"");
            //tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

            //tw.WriteLine("<dat:dataPackItem id=\"PRE" + davkacislo + "\" version=\"2.0\">");
            //tw.WriteLine("  <pre:prevodka version=\"2.0\">");

            //// hlavicka
            //tw.WriteLine("      <pre:prevodkaHeader>");
            //tw.WriteLine("          <pre:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</pre:date>");
            ////tw.WriteLine("          <pre:numberOrder></pre:numberOrder>");
            //tw.WriteLine("          <pre:store>");
            //tw.WriteLine("              <typ:id>" + SKL_ID + "</typ:id>");
            //tw.WriteLine("          </pre:store>");
            //tw.WriteLine("          <pre:text>" + note + "</pre:text>");

            ////partner
            ////if (partnerID != string.Empty)
            ////{
            ////    tw.WriteLine("          <pre:partnerIdentity>");
            ////    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
            ////    tw.WriteLine("          </pre:partnerIdentity>");
            ////}

            //tw.WriteLine("          <pre:activity>");
            //tw.WriteLine("              <typ:id>" + "1" + "</typ:id>");
            //tw.WriteLine("          </pre:activity>");

            ////tw.WriteLine("          <pre:note>" + "nacteno z xml (terminal)" + "</pre:note>");
            //tw.WriteLine("          <pre:intNote>" + "nacteno z mobilniho terminalu" + "</pre:intNote>");
            //tw.WriteLine("      </pre:prevodkaHeader>");

            ////polozky 
            //tw.WriteLine("      <pre:prevodkaDetail>");

            //foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in dt_si)
            //{
            //    tw.WriteLine("      <pre:prevodkaItem>");
            //    tw.WriteLine("          <pre:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</pre:quantity>");
            //    //tw.WriteLine("          <pre:unit>" + row.MJ.Trim() + "</pre:unit>");
            //    //tw.WriteLine("          <pre:coefficient>1</pre:coefficient>");
            //    //tw.WriteLine("          <pre:discountPercentage>0</pre:discountPercentage>");
            //    tw.WriteLine("          <pre:stockItem>");
            //    tw.WriteLine("              <typ:stockItem>");
            //    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
            //    tw.WriteLine("              </typ:stockItem>");

            //    //if (row.SERLTNUM.Trim().Length > 0)
            //    //    tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

            //    tw.WriteLine("          </pre:stockItem>");
            //    tw.WriteLine("      </pre:prevodkaItem>");
            //}

            //tw.WriteLine("      </pre:prevodkaDetail>");
            //tw.WriteLine("  </pre:prevodka>");
            //tw.WriteLine("</dat:dataPackItem>");
            //tw.WriteLine("</dat:dataPack>"); 
            #endregion

            #region NEew

            string SKL_ID = cZMST_DIDataTable[0].SKL_ID_DEST;
            int objednavka = cZMST_DIDataTable[0].CountEntries;

            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace pre = "http://www.stormware.cz/schema/version_2/prevodka.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            try
            {
                //Fask.DataSets.ProdejData.CZMST_DIRow Row_DI;

                //var data = cZMST_DIDataTable.OrderBy(x => x.DEX_ROW_ID);
                //Row_DI = data.First();

                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(pre + "text", "MST Vytvořeno z volného pohybu"));

                if (!string.IsNullOrEmpty(p))
                {
                    listHeader.Add(new XElement(pre + "note", p));
                }

                listHeader.Add(new XElement(pre + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                listHeader.Add(new XElement(pre + "store", new XElement(typ + "id", SKL_ID)));
                listHeader.Add(new XElement(pre + "activity", new XElement(typ + "id", 1)));

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in cZMST_DIDataTable)
                {
                    List<XElement> PrevodkaItem = new List<XElement>();

                    PrevodkaItem.Add(new XElement(pre + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
                    //PrevodkaItem.Add(new XElement(pre + "unit", row.MJ.Trim()));
                    //PrevodkaItem.Add(new XElement(pre + "coefficient", 1));
                    //PrevodkaItem.Add(new XElement(pre + "discountPercentage", 0));

                    if (row.SERLTNUM.Trim().Length > 0)
                        PrevodkaItem.Add(new XElement(pre + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        PrevodkaItem.Add(new XElement(pre + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    XElement polozka = new XElement(pre + "prevodkaItem", PrevodkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                #region Summary

                //List<XElement> Summary = new List<XElement>();


                #endregion



                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "pre", "http://www.stormware.cz/schema/version_2/prevodka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "PRE_K" + objednavka.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import prevodky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "PRE_K" + objednavka.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(pre + "prevodka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(pre + "prevodkaHeader", listHeader),
                                    new XElement(pre + "prevodkaDetail", listItem)
                                        )));

                //new XElement(pre + "prevodkaSummary", Summary)

                root.Save(filename);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;

            #endregion
        }

        /// <summary>
        /// Metoda pro Vytvořeni requestu Objednavka vydana z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="p">poznamka</param>
        /// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Objv_XML(string filename, string p, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
        {
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            try
            {
                int objednavka = cZMST_DIDataTable[0].CountEntries;

                string partnerID = "1";// string.Empty;
                foreach (Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row in cZMST_DIDataTable)
                {
                    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
                    {
                        partnerID = row.ODB_ID.Trim();
                        break;
                    }
                }


                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(ord + "orderType", "issuedOrder"));
                listHeader.Add(new XElement(ord + "text", "MST Vytvořeno z volneho pohybu"));
                listHeader.Add(new XElement(ord + "note", objednavka));

                listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

                if (!string.IsNullOrEmpty(partnerID))
                {
                    XElement IDPartnerFakturacni;

                    IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

                    listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
                }
                else
                {
                    XElement IDPartnerFakturacni;

                    IDPartnerFakturacni = new XElement(typ + "id", "1");

                    listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
                }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in cZMST_DIDataTable)
                {
                    List<XElement> prodejkaItem = new List<XElement>();

                    prodejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
                    prodejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
                    prodejkaItem.Add(new XElement(ord + "coefficient", 1));
                    prodejkaItem.Add(new XElement(ord + "discountPercentage", 0));

                    if (row.SERLTNUM.Trim().Length > 0)
                        prodejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        prodejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    XElement polozka = new XElement(ord + "orderItem", prodejkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                #region Summary

                List<XElement> Summary = new List<XElement>();


                #endregion

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "OBJV_K" + objednavka.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import objv"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "OBJV_K" + objednavka.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(ord + "order",
                                    new XAttribute("version", "2.0"),
                                    new XElement(ord + "orderHeader", listHeader),
                                    new XElement(ord + "orderDetail", listItem),
                                    new XElement(ord + "orderSummary", Summary)
                                        )));

                root.Save(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;


        }

    }
}
