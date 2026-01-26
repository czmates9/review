using Fask.Interfaces.DataSets_Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Data.SqlClient;


namespace Fask.ModulePohodaXML.Classes
{
    public class Importni_Mustek
    {
        internal static bool CreateRequest_Import_DodavateleZasob_XML(ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable dt, string file)
        {
            #region VZOR

            // ** VZOR ** //
            //< dat:dataPack
            //xmlns:dat = "http://www.stormware.cz/schema/version_2/data.xsd"
            //xmlns: sup = "http://www.stormware.cz/schema/version_2/supplier.xsd"
            //xmlns: typ = "http://www.stormware.cz/schema/version_2/type.xsd"
            //id = "001"
            //ico = "12345678"
            //application = "StwTest"
            //version = "2.0"
            //note = "Požadavek na import datavatele na zásoby" >
            //< dat:dataPackItem id = "Do001" version = "2.0" >
            //< !--Dodavatel na zásobě -->
            //< sup:supplier version = "2.0" >
            //< sup:stockItem >
            //< typ:stockItem >
            //< typ:ids > B04 </ typ:ids >
            //</ typ:stockItem >
            //</ sup:stockItem >
            //< sup:suppliers >
            //< sup:supplierItem default = "true" >
            //< sup:refAd >
            //< typ:id > 2 </ typ:id >
            //< typ:ids > ABC Audit s.r.o.</ typ:ids >
            //</ sup:refAd >
            //< sup:orderCode > A1 </ sup:orderCode >
            //< sup:orderName > A - zasoba </ sup:orderName >
            //< sup:purchasingPrice > 1968 </ sup:purchasingPrice >
            //< sup:rate > 0.0 </ sup:rate >
            //< sup:payVAT > false </ sup:payVAT >
            //< sup:ean > 11112228 </ sup:ean >
            //< sup:printEAN > true </ sup:printEAN >
            //< sup:unitEAN > ks </ sup:unitEAN >
            //< sup:unitCoefEAN > 1.0 </ sup:unitCoefEAN >
            //< sup:deliveryTime > 12 </ sup:deliveryTime >
            //< sup:minQuantity > 2.0 </ sup:minQuantity >
            //< sup:note > fdf </ sup:note >
            //</ sup:supplierItem >
            //< sup:supplierItem default = "false" >
            //< sup:refAd >
            //< typ:id > 15 </ typ:id >
            //< typ:ids > INTEAK spol.s r. o.</ typ:ids >
            //</ sup:refAd >
            //< sup:orderCode > I1 </ sup:orderCode >
            //< sup:orderName > I - zasoba </ sup:orderName >
            //< sup:purchasingPrice > 500 </ sup:purchasingPrice >
            //< sup:rate > 0.0 </ sup:rate >
            //< sup:payVAT > false </ sup:payVAT >
            //< sup:ean > 212121212 </ sup:ean >
            //< sup:printEAN > true </ sup:printEAN >
            //< sup:unitEAN > ks </ sup:unitEAN >
            //< sup:unitCoefEAN > 1.0 </ sup:unitCoefEAN >
            //< sup:deliveryTime > 12 </ sup:deliveryTime >
            //< sup:minQuantity > 2.0 </ sup:minQuantity >
            //< sup:note > aasn </ sup:note >
            //</ sup:supplierItem >
            //</ sup:suppliers >
            //</ sup:supplier >
            //</ dat:dataPackItem >
            //</ dat:dataPack >

            #endregion

            try
            {

                // konstanta pro tostrin() cisel na invariantni format ...
                System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;
                string filename = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, file);

                string ID_req = "DodavateleZasob" + Guid.NewGuid();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace sup = "http://www.stormware.cz/schema/version_2/supplier.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                List<XElement> ListSupplierItem = new List<XElement>();

                foreach (var item in dt.GroupBy(x => x.RefAg))
                {
                    XElement itemnmbr_Stock = new XElement(typ + "stockItem",
                            new XElement(typ + "id", item.Key));
                    var itemnmbr = new XElement(sup + "stockItem", itemnmbr_Stock);

                    List<XElement> ListSuppliers = new List<XElement>();

                    foreach (var row in item)
                    {

                        XAttribute defAtr;

                        if (row.DefDod)
                            defAtr = new XAttribute("default", "true");
                        else
                            defAtr = new XAttribute("default", "false");


                        List<XElement> polozky = new List<XElement>();

                        if (!row.IsRefADNull())
                            polozky.Add(new XElement(sup + "refAd", new XElement(typ + "id", row.RefAD.ToString())));

                        if (!row.IsNakupCNull())
                            polozky.Add(new XElement(sup + "purchasingPrice", row.NakupC.ToString(nfi)));

                        if (!row.IsRefCMNull())
                            polozky.Add(new XElement(sup + "currency", new XElement(typ + "id", row.RefCM.ToString())));

                        if (!row.IsCmKursNull())
                            polozky.Add(new XElement(sup + "rate", row.CmKurs.ToString(nfi)));

                        if (!row.IsEANNull())
                            polozky.Add(new XElement(sup + "ean", row.EAN));

                        if (!row.IsMJEANNull())
                            polozky.Add(new XElement(sup + "unitEAN", row.MJEAN));

                        if (!row.IsMJkoefEANNull())
                            polozky.Add(new XElement(sup + "unitCoefEAN", row.MJkoefEAN.ToString(nfi)));

                        if (!row.IsPoznNull())
                            polozky.Add(new XElement(sup + "note", row.Pozn));

                        ListSuppliers.Add(new XElement(sup + "supplierItem",
                            defAtr,
                            polozky
                            ));

                    }

                    XElement suppliers = new XElement(sup + "suppliers", ListSuppliers);

                    ListSupplierItem.Add(new XElement(sup + "supplier", new XAttribute("version", "2.0"), itemnmbr, suppliers));

                }

                XElement root = new XElement(dat + "dataPack",
                new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                new XAttribute(XNamespace.Xmlns + "sup", "http://www.stormware.cz/schema/version_2/supplier.xsd"),
                new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                new XAttribute("id", ID_req),
                new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                new XAttribute("version", "2.0"),
                new XAttribute("note", "Fask import Dodavatele zasob"),

            new XElement(dat + "dataPackItem",
                new XAttribute("id", ID_req),
                new XAttribute("version", "2.0"),
                ListSupplierItem
                    ));

                root.Save(filename);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

			return true;
		}

        internal static string LoadResponse_Import_DodavateleZasob_XML(ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable dt, string respfilename)
        {
			return "OK";
        }
    }
}
