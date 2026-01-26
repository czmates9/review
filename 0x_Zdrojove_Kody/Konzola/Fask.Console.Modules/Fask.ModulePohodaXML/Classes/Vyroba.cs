using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using Fask.Interfaces.Classes;
using System.Data;

namespace Fask.ModulePohodaXML.Classes
{
    public static class Vyroba
    {

        public static void VypocetZustatek(Fask.Interfaces.DataSets.Vyroba vyr)
        {

            try
            {

                IEnumerable<IGrouping<string, Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_KP_MatProVyrRow>> grupa = vyr.FASK_Vyroba_KP_MatProVyr.GroupBy(x => x.ITEMNMBR);

                decimal? Zustatek = null;

                foreach (IGrouping<string, Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_KP_MatProVyrRow> item in grupa)
                {

                    foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_KP_MatProVyrRow row in item)
                    {
                        if (!Zustatek.HasValue)
                        {
                            Zustatek = row.QTY;
                        }
                        else
                        {
                            decimal QTYloc = row.IsQTYNull() ? 0 : row.QTY;

                            if (row.znamenko.Trim() == "+")
                            {
                                Zustatek = Zustatek + QTYloc;
                            }
                            else if (row.znamenko.Trim() == "-")
                            {
                                Zustatek = Zustatek - QTYloc;
                            }
                        }

                        row.Zustatek = (decimal)Zustatek;
                    }

                    Zustatek = null;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }


        #region Import Vyroba

        internal static bool CreateRequest_Import_Vyroba_XML(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p, string file, int countEntries, string SKL_ID, string note)
        {
            // konstanta pro tostrin() cisel na invariantni format ...
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            try
            {


                #region TypDokladu

                Fask.Rady.DS_Rady.FASK_RADYRow rowtmp = null;
                Doklad typDoklad = null;
                Fask.Rady.NumericalSeries NS = null;

                try
                {
                    Globals_V1.LoadConfiguration();
                    NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                    rowtmp = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Vyr, SKL_ID, string.Empty, false, string.Empty, string.Empty);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", " FASK Rady", ex);

                    if (NS == null)
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt NS", " Objekt NS is NULL");

                    if (rowtmp == null)
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt row", " Objekt row is NULL");
                }

                typDoklad = new Doklad(rowtmp);


                #endregion

                Globals_V1.LoadConfiguration();

                DateTime? datumdokladu = null;

                string text = null;//pohoda_obj.IsSTextNull() ? null : pohoda_obj.SText;
                string poznamka = null;//pohoda_obj.IsPoznNull() ? null : pohoda_obj.Pozn;
                string poznamka2 = null;//pohoda_obj.IsPozn2Null() ? null : pohoda_obj.Pozn2;
                StringBuilder poznB = new StringBuilder();

                if (!String.IsNullOrEmpty(poznamka))
                    poznB.AppendLine(poznamka);

                if (!String.IsNullOrEmpty(note))
                    poznB.AppendLine(note);


                string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + file;
                DateTime date = DateTime.Now;

                var data = dt_p.OrderBy(x => x.id);
                var row_id = data.First();

                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace vyr = "http://www.stormware.cz/schema/version_2/vyroba.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                #region Header

                text = "Vytvořeno MES FASK";

                List<XElement> listHeader = new List<XElement>();

                if (text != null) { listHeader.Add(new XElement(vyr + "text", text)); }

                if (poznB.Length > 0) { listHeader.Add(new XElement(vyr + "note", poznB.ToString())); }

                listHeader.Add(new XElement(vyr + "intNote", (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "Načteno pomocí konzole z výrobního přikazu č.:" + countEntries.ToString()));

                if (datumdokladu.HasValue) { listHeader.Add(new XElement(vyr + "date", XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd"))); }

                listHeader.Add(new XElement(vyr + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                listHeader.Add(new XElement(vyr + "time", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));

                listHeader.Add(new XElement(vyr + "dateOfReceipt", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                listHeader.Add(new XElement(vyr + "timeOfReceipt", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));


                //21.7.2020 TaD Doplnení :
                //Zdrojove středisko
                //Cilove středisko
                //činnost
                //zakazka

                if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Stredisko0))
                {
                    listHeader.Add(new XElement(vyr + "centreSource", new XElement(typ + "id", typDoklad.Stredisko0.Trim()))); // Zdrojove středisko
                }

                if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Stredisko1))
                {
                    listHeader.Add(new XElement(vyr + "centreDestination", new XElement(typ + "id", typDoklad.Stredisko1.Trim()))); // Cilove středisko
                }

                if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Cinnost))
                {
                    listHeader.Add(new XElement(vyr + "activity", new XElement(typ + "id", typDoklad.Cinnost.Trim()))); // činnost
                }

                if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Zakazka))
                {
                    listHeader.Add(new XElement(vyr + "contract", new XElement(typ + "id", typDoklad.Zakazka.Trim()))); // Zakazka
                }


                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in dt_p)
                {
                    if (row.IsTIMESTOPNull())
                        continue;

                    XElement itemElement;

                    itemElement = new XElement(vyr + "vyrobaItem",
                    new XElement(vyr + "quantity", ((float)row.qty).ToString(nfi)),
                    new XElement(vyr + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    //TODO SN/šarže
                    //new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim()));

                    listItem.Add(itemElement);
                }


                #endregion


                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "vyr", "http://www.stormware.cz/schema/version_2/vyroba.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "Vyr" + row_id.id.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import Vyroby"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "Vyr" + row_id.id.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(vyr + "vyroba",
                                    new XAttribute("version", "2.0"),
                                    new XElement(vyr + "vyrobaHeader", listHeader),
                                    new XElement(vyr + "vyrobaDetail", listItem)
                                        )));

                root.Save(filename);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            return true;
        }

        internal static string LoadResponse_Import_Vyroba_XML(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p, string respfilename, int countEntries, string SKL_ID)
        {
            try
            {
                string number = string.Empty;
                //string filename = Globals.PathToInputDirectory + "Response\\" + file;
                string filename = respfilename;

                if (!File.Exists(filename))
                    throw new Exception("Response soubor '" + respfilename + "' neexistuje!");

                //Pouzite namespacy
                XNamespace rdc = "http://www.stormware.cz/schema/version_2/documentresponse.xsd";
                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
                XNamespace vyr = "http://www.stormware.cz/schema/version_2/vyroba.xsd";

                // nacteni hodnot z odpovedi ...
                XDocument root = XDocument.Load(filename);

                XElement responsePack = root.Element(rsp + "responsePack");
                XAttribute state = responsePack.Attribute("state");
                if (state.Value != "ok")
                {
                    XAttribute note = responsePack.Attribute("note");
                    throw new Exception("Nepodařilo se získat data Vyroby.\nStatus:" + state.Value + "\nChyba:" + note.Value);
                }
                XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
                if (responsePackItem != null)
                {
                    XAttribute responsePackItemState = responsePackItem.Attribute("state");
                    if (responsePackItemState.Value.Trim() != "ok")
                    {
                        XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                        throw new Exception("Nepodařilo se získat data Vyroby.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
                    }

                    XElement VyrobaResponse = responsePackItem.Element(vyr + "vyrobaResponse");
                    if (VyrobaResponse != null)
                    {
                        XAttribute VyrobaState = responsePackItem.Attribute("state");
                        if (VyrobaState.Value.Trim() != "ok")
                        {
                            XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                            throw new Exception("Nepodařilo se získat data Vyroby.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
                        }

                        XElement ImportDetails = VyrobaResponse.Element(rdc + "importDetails");
                        if (ImportDetails != null)
                        {
                            XElement Detail = ImportDetails.Element(rdc + "detail");
                            if (Detail != null)
                            {
                                XElement S = Detail.Element(rdc + "state");
                                if (S != null && S.Value == "warning")
                                {
                                    XElement Note = Detail.Element(rdc + "note");
                                    number += "Nastal v response warning:'" + Note.Value;
                                }
                                if (S != null && S.Value == "error")
                                {
                                    XElement Note = Detail.Element(rdc + "note");
                                    throw new Exception("Nastal v response error:'" + Note.Value);
                                }
                            }

                        }
                    }
                }

                XElement prijemkaResponse =
                    root.Element(rsp + "responsePack").
                        Element(rsp + "responsePackItem").
                            Element(vyr + "vyrobaResponse");

                //XAttribute prijemkaResponseState = prijemkaResponse.Attribute("state");

                XElement dokladnumber =
                    prijemkaResponse.Element(rdc + "producedDetails").
                    Element(rdc + "number");

                number = string.Format("{0}" + Environment.NewLine + number, dokladnumber.Value);



                // koretne ulozeno a muzu dokoncit ... 
                // dokonceni => ISOK = datetime.now
                DateTime isOK = DateTime.Now;
                dt_p.ToList().ForEach(x => x.ISOK = isOK);


                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


    }
}
