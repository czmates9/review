using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Fask.ModulePohodaXML.XML.Classes
{
    public class Response2
    {
        private XmlDocument xmldoc = null;
        /// <summary>
        /// vraci nacteny xml document
        /// </summary>
        public XmlDocument XmlDocument
        {
            get { return xmldoc; }
        }

        private string _filename = string.Empty;
        /// <summary>
        /// nazev souboru s xml odpovedi pohody
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                Load();
            }
        }

        private XmlNamespaceManager _xmlNamespaceManager = null;

        /// <summary>
        /// provede nacteni xmldocumentu s odpovedi z pohody
        /// </summary>
        /// <param name="filename">nazev souboru s odpovedi z pohody ... </param>
        public void Load(string filename)
        {
            this.Filename = filename;
        }
        private void Load()
        {
            //nacte xmldom ze souboru ...
            xmldoc = new XmlDocument();

            //zajimave...http://stackoverflow.com/questions/310669/why-does-c-sharp-xmldocument-loadxmlstring-fail-when-an-xml-header-is-included
            xmldoc.Load(this._filename);
            //xmldoc.LoadXml(this._filename)

            _xmlNamespaceManager = new XmlNamespaceManager(xmldoc.NameTable);
            _xmlNamespaceManager.AddNamespace("rsp", "http://www.stormware.cz/schema/version_2/response.xsd");
            _xmlNamespaceManager.AddNamespace("rdc", "http://www.stormware.cz/schema/version_2/documentresponse.xsd");

            //zakladni parsovani status a cislo dokladu(pokud existuje, resp. neni error)
            //jestli neni lepsi nepouzit LINQ...
            XmlNodeList xmlnodesresponsepackitems = xmldoc.SelectNodes("//rsp:responsePackItem", _xmlNamespaceManager);
            if (xmlnodesresponsepackitems.Count > 0) //neco tam je ... 
            {
                if ((xmlnodesresponsepackitems[0].Attributes["state"] != null)
                    && (xmlnodesresponsepackitems[0].Attributes["state"].Value == "ok"))
                {
                    Status = "OK";
                }
                else
                {
                    if (xmlnodesresponsepackitems[0].Attributes["note"] != null)
                    {
                        Status = xmlnodesresponsepackitems[0].Attributes["note"].Value;
                    }
                    else
                    {
                        Status = "Neznámá chyba XML Importu.";
                    }

                    // 17.6.2016 PeV: uprava, aby bylo mozne odeslat davku v pripade, ze data se jiz dostala do pohody (napr. nastal timeout na terminalu)
                    //Status += "\nID=" + xmlnodesresponsepackitems[0].Attributes["id"].Value;
                    //Logging.Log.writeErrorLog("Provider.PohodaXML", "Response.Load()", Status);
                    string statusLog = Status + "\nID=" + xmlnodesresponsepackitems[0].Attributes["id"].Value;
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"Fask.ModulePohodaXML.XML.Classes.Response2" , "Load()", statusLog);
                    if (Status == "Importovaný záznam již existuje.")
                    {
                        Status = "OK2";
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML.XML.Classes.Response2", "Load()", "Status změněn na: " + Status);
                    }
                }
            }
            else
            {
                Status = "ResponsePackItem v odpovedi nenalezena";
            }

            XmlNodeList xmlnodesproducedDetails = xmldoc.SelectNodes("//rdc:producedDetails", _xmlNamespaceManager);
            if (xmlnodesproducedDetails.Count > 0) //neco tam je ... 
            {
                if (xmlnodesproducedDetails[0].HasChildNodes)
                {
                    for (int i = 0; i < xmlnodesproducedDetails[0].ChildNodes.Count; i++)
                    {
                        if (xmlnodesproducedDetails[0].ChildNodes[i].Name == "rdc:number")
                        {
                            DocumentNumber = xmlnodesproducedDetails[0].ChildNodes[i].InnerText;
                            break;
                        }
                    }
                }
                else
                {
                    DocumentNumber = string.Empty;
                }
            }
            else
            {
                DocumentNumber = string.Empty;
            }
        }
        public string DocumentNumber { get; set; }
        public string Status { get; set; }
        public string CisloDokladuPrijemka
        {
            get
            {
                string cislodokladu = string.Empty;

                XmlNode xmldetails = xmldoc.SelectSingleNode("//rdc:producedDetails", _xmlNamespaceManager);
                if (xmldetails == null)
                    return cislodokladu;

                XmlNode xmlnumber = xmldetails.SelectSingleNode("//rdc:number", _xmlNamespaceManager);
                if (xmlnumber == null)
                    return cislodokladu;
                else
                    cislodokladu = xmlnumber.InnerText;

                return cislodokladu;
            }
        }

        public Response2()
        {
        }

        public Response2(string filename)
            : this()
        {
            Filename = filename;
        }
    }
}
