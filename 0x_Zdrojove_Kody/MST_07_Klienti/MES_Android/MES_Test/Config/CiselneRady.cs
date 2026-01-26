using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MES_Android.Config
{
    public sealed class CiselneRady
    {
        private string _filename;

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public CiselneRady()
        {
            //TaD TODO tohle je nutno co nejrychleji predelat
            this._filename = Classes.DataInfo_Static.ConfigProdejXML;
            if (!System.IO.File.Exists(this._filename))
                CreateFile();
        }

        public CiselneRady(string filename)
        {
            this._filename = filename;
            if (!System.IO.File.Exists(this._filename))
                CreateFile();
        }

        public bool ProdejSetDeleted(string cislodavky)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(_filename);
                XmlNode prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Range");
                if (prodejnode != null)
                {
                    int prefix = int.Parse(prodejnode.Attributes["Prefix"].Value);
                    int next = int.Parse(prodejnode.Attributes["Next"].Value);
                    string prefixformat = prodejnode.Attributes["PrefixFormat"].Value;
                    string nextformat = prodejnode.Attributes["NextFormat"].Value;

                    //davka = prefix.ToString(prefixformat) + next.ToString(nextformat);
                    //next++;
                    //prodejnode.Attributes["Next"].Value = next.ToString();

                    string strprefix = prefix.ToString();
                    string strnumber = cislodavky.Substring(strprefix.Length);
                    int number = int.Parse(strnumber);

                    XmlAttribute xmlattDeletedNumber = xmldoc.CreateAttribute("Number");
                    xmlattDeletedNumber.Value = number.ToString();

                    XmlElement xmlelemDeleted = xmldoc.CreateElement("Deleted");
                    xmlelemDeleted.Attributes.Append(xmlattDeletedNumber);

                    prodejnode.AppendChild(xmlelemDeleted);

                    xmldoc.Save(_filename);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public string ProdejGetNext()
        {
            string davka = "";

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(_filename);
                XmlNode prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Range");
                if (prodejnode != null)
                {
                    XmlNodeList xmlnodesdeleted = prodejnode.SelectNodes("./Deleted");
                    XmlNode xmlnodedeleted = null;
                    foreach (XmlNode xmlnode in xmlnodesdeleted)
                    {
                        if (xmlnode.Attributes.Count <= 0 || xmlnode.Attributes["Number"] == null)
                        {
                            continue;
                        }

                        if (xmlnodedeleted == null) xmlnodedeleted = xmlnode;

                        if (int.Parse(xmlnode.Attributes["Number"].Value) <
                            int.Parse(xmlnodedeleted.Attributes["Number"].Value)
                            )
                        {
                            xmlnodedeleted = xmlnode;
                        }
                    }

                    int prefix = int.Parse(prodejnode.Attributes["Prefix"].Value);
                    int next = xmlnodedeleted == null ?
                        int.Parse(prodejnode.Attributes["Next"].Value) :
                        int.Parse(xmlnodedeleted.Attributes["Number"].Value);
                    string prefixformat = prodejnode.Attributes["PrefixFormat"].Value;
                    string nextformat = prodejnode.Attributes["NextFormat"].Value;

                    davka = prefix.ToString(prefixformat) + next.ToString(nextformat);
                    if (xmlnodedeleted == null)
                    {
                        next++;
                        prodejnode.Attributes["Next"].Value = next.ToString();
                    }
                    else
                    {
                        prodejnode.RemoveChild(xmlnodedeleted);
                    }
                    xmldoc.Save(_filename);
                }
            }
            catch { }

            return davka;
        }

        public void CreateFile()
        {

            List<XAttribute> xAttributes = new List<XAttribute>();
            
            xAttributes.Add(new XAttribute("Next", "01"));
            xAttributes.Add(new XAttribute("NextFormat", "0000"));
            xAttributes.Add(new XAttribute("Prefix", Config.Settings.TerminalID.ToString()));
            xAttributes.Add(new XAttribute("PrefixFormat", "0000"));


            XElement root = new XElement("Config", 
                new XElement("Modules",
                new XElement("Prodej",
                new XElement("Range",
                xAttributes))));

            root.Save(this._filename);

        }
    }
}