using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

namespace Fask.Localization
{
    public class LocalizationSupport
    {
        //private static System.Collections.Specialized.NameValueCollection localizationData = null;        
        private static System.Collections.Generic.Dictionary<string, object> localizationData = null;        

        /// <summary>
        /// Typy lokalizace (defaulní je cs)
        /// </summary>
        public enum LocalType
        {
            cs,
            sk
        }

        /// <summary>
        /// Nacteni lokalizace ze souboru Localization.resx a jeho obdob z adresare Lokalizace.
        /// </summary>
        public static void InitLocalizationData()
        {
            string filename = string.Empty;
            try
            {
                // nacteni stringu z lokalizacniho adresare ... 
                // postup nacitani lokalizace:
                // 1) Localization.sk.FASK.resx
                // 2) Localization.sk.resx
                // 3) Localization.resx
                // 4) Lokalizace nastavena primo v Localization.resx, ktery je primo v projektu
                //localizationData = new System.Collections.Specialized.NameValueCollection();
                localizationData = new Dictionary<string,object>();
                if (Globals.LokalizacePovolit && Globals.LokalizaceVlastniPovolit)
                {
                    string[] filenames = System.IO.Directory.GetFiles(Globals.LocalizationDir, "Localization." + Globals.LokalizaceZvolena.ToString() + ".*.resx");
                    if (filenames.Count() > 0)
                    {
                        filename = filenames.First();
                        LoadFormLocalizationDataFromFile(localizationData, filename);
                    }

                    filename = "Localization." + Globals.LokalizaceZvolena.ToString() + ".resx";
                    LoadFormLocalizationDataFromFile(localizationData, filename);

                    filename = "Localization.resx";
                    LoadFormLocalizationDataFromFile(localizationData, filename);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "LocalizationSupport.InitLocalizationData");
            }
        }

        /// <summary>
        /// Nacteni dat z lokalizacniho souboru (pokud existuje)
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        //public static NameValueCollection LoadFormLocalizationDataFromFiles(List<string> filenames)
        public static Dictionary<string, object> LoadFormLocalizationDataFromFiles(List<string> filenames)
        {
            //NameValueCollection collection = new NameValueCollection();
            Dictionary<string, object> collection = new Dictionary<string, object>();

            try
            {
                foreach (var filename in filenames)
                {
                    bool result = LoadFormLocalizationDataFromFile(collection, filename);
                    if (!result)
                        Logging.Log.Write("Problem se ctenim lokalizacniho souboru: " + filename);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "LocalizationSupport.LoadFormLocalizationDataFromFiles");
            }

            return collection;
        }


        /// <summary>
        /// Naplneni kolekce daty, probiha kontrola na duplicitu.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        //public static bool LoadFormLocalizationDataFromFile(NameValueCollection collection, string filename)
        public static bool LoadFormLocalizationDataFromFile(System.Collections.Generic.Dictionary<string, object> collection, string filename)
        {
            try
            {
                //Logging.TracId id = new Fask.Logging.TracId(null, null, null, "LocalizationSupport", "LoadFormLocalizationDataFromFile() pro soubor : " + filename);
                //Logging.Trace2.Write("Start-LoadData", "Nacteni dat z resx soubor" + filename, id);
                //NameValueCollection collection = new NameValueCollection();

                string filepath = System.IO.Path.Combine(Globals.LocalizationDir, filename);
                if (File.Exists(filepath))
                {
                    //m_settingsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Settings.xml");
                    System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                    //Logging.Trace2.Write("Start-LoadData", "Nacteni xdoc.Load(" + filename + ")", id);
                    xdoc.Load(filepath);
                    //Logging.Trace2.Write("Stop-LoadData", "Nacteni xdoc.Load(" + filename + ")", id);
                    XmlElement root = xdoc.DocumentElement;
                    //foreach (XmlNode node in root.SelectNodes("/configuration/appSettings/add"))
                    //Logging.Trace2.Write("Start-LoadData", "Pred Foreach nacteni souboru z:" + filename, id);
                    foreach (XmlNode node in root.SelectNodes("/root/data"))
                    {
                        // kontrola na existenci klice
                        try
                        {
                            //object key = collection[node.Attributes["name"].Value];

                            if (!collection.ContainsKey(node.Attributes["name"].Value))
                            {
                                //}
                                //catch (KeyNotFoundException)
                                //{
                                if (node.HasChildNodes && node.ChildNodes.Count >= 2)   // TODO: Prepsat nejak normalne, aby se z XML souboru ukladala primo 'Value'
                                {
                                    //node.LastChild = (object)(new XmlNode())
                                    //Logging.Trace2.Write("Start-LoadData", "Pridani do kolekce pro soubor: " + filename, id);
                                    collection.Add(node.Attributes["name"].Value, node.ChildNodes[1].InnerText);
                                    //Logging.Trace2.Write("Stop-LoadData", "Pridani do kolekce pro soubor: " + filename, id);
                                }
                                else
                                {
                                    //Logging.Trace2.Write("Start-LoadData", "Parsovani a pridani do kolekce pro soubor: " + filename, id);
                                    ParseTypeValue(collection, node);
                                    //Logging.Trace2.Write("Stop-LoadData", "Parsovani a pridani do kolekce pro soubor: " + filename, id);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        //object key = collection[node.Attributes["name"].Value];
                        //if (key == null)  // klic neexistuje, je mozne ho pridat ...
                        //{
                        //}
                    }
                    //Logging.Trace2.Write("Stop-LoadData", "Pred Foreach nacteni souboru z:" + filename, id);

                    //foreach (XmlNode node in root.SelectNodes("/root/data"))
                    //{
                    //    // kontrola na existenci klice
                    //    string key = collection[node.Attributes["name"].Value];
                    //    if (key == null)  // klic neexistuje, je mozne ho pridat ...
                    //    {
                    //        foreach (XmlNode item in node.SelectNodes("value"))
                    //        {
                    //            collection.Add(node.Attributes["name"].Value, item.Attributes["value"].Value);  
                    //        }

                    //        //collection.Add(node.Attributes["name"].Value, node.Attributes["value"].Value);
                    //    }
                    //}
                   //Logging.Trace2.Write("Stop-LoadData", "Nacteni dat z resx soubor" + filename, id);
                    return true;
                }
                else  // soubor neexistuje, neni treba nacitat ...
                    return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "LocalizationSupport.LoadFormLocalizationDataFromFile");
            }

            return false;
        }

        private static void ParseTypeValue(Dictionary<string, object> collection, XmlNode node)
        {
#if DEBUG
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
#endif
            try
            {
#if DEBUG
                //Logging.TracId id = new Fask.Logging.TracId(null, null, null, "LocalizationSupport", "ParseTypeValue");
                //Logging.Trace2.Write("Start-Parse", "ParseTypeValue Zacatek pred promennima", id);

                //sw.Start();
                //System.Diagnostics.Debug.WriteLine("Cas Start celkovy: " + sw.ElapsedMilliseconds.ToString() + " ms");
               // System.Diagnostics.Debug.WriteLine("Cas Start zapis_prommenych: " + sw.ElapsedMilliseconds.ToString() + " ms");
#endif
                XmlAttribute nname = node.Attributes["name"];
                XmlAttribute ntype = node.Attributes["type"];
                string v = node.ChildNodes[0].InnerText;
                string n = nname.Value;
#if DEBUG
                //Logging.Trace2.Write("Stop-Parse", "ParseTypeValue Konec pred promennima", id);
               // System.Diagnostics.Debug.WriteLine("Cas Stop zapis_prommenych: " + sw.ElapsedMilliseconds.ToString() + " ms");
                //System.Diagnostics.Debug.WriteLine("Cas Start podminka_ntype: " + sw.ElapsedMilliseconds.ToString() + " ms");
#endif


                if (ntype != null)
                {
#if DEBUG
                    //Logging.Trace2.Write("Start-Parse", "Type t = Type.GetType(ntype.Value)", id);
                    //System.Diagnostics.Debug.WriteLine("Cas Stop podminka_ntype: " + sw.ElapsedMilliseconds.ToString() + " ms");
                    //System.Diagnostics.Debug.WriteLine("Cas Start Type.GetType(): " + sw.ElapsedMilliseconds.ToString() + " ms");
#endif
                    //string stype = ntype.Value;
                    Type t = Type.GetType(ntype.Value);
#if DEBUG
                    //System.Diagnostics.Debug.WriteLine("Cas Start Type.GetType(): " + sw.ElapsedMilliseconds.ToString() + " ms");
                    //Logging.Trace2.Write("Stop-Parse", "Type t = Type.GetType(ntype.Value)", id);
                    //Logging.Trace2.Write("Start-Parse", "Type a = typeof(System.Windows.Forms.AnchorStyles)", id);
                    //System.Diagnostics.Debug.WriteLine("Cas start typeof(System.Windows.Forms.AnchorStyles): " + sw.ElapsedMilliseconds.ToString() + "ms");
                    //Type a = typeof(System.Windows.Forms.AnchorStyles);
                    //System.Diagnostics.Debug.WriteLine("Cas stop typeof(System.Windows.Forms.AnchorStyles): " + sw.ElapsedMilliseconds.ToString() + "ms");
                    //Logging.Trace2.Write("Stop-Parse", "Type a = typeof(System.Windows.Forms.AnchorStyles)", id);
#endif
                    if (t == typeof(System.Windows.Forms.AnchorStyles))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Windows.Forms.AnchorStyles: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Windows.Forms.AnchorStyles", id);
#endif
                        collection.Add(n, (AnchorStyles)Enum.Parse(typeof(AnchorStyles), v.Trim(), true));
#if DEBUG
                       // System.Diagnostics.Debug.WriteLine("Cas stop System.Windows.Forms.AnchorStyles: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Windows.Forms.AnchorStyles", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Int16))
                    {
#if DEBUG
                       // System.Diagnostics.Debug.WriteLine("Cas start System.Int16: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Int16", id);
#endif
                        collection.Add(n, System.Int16.Parse(v));
#if DEBUG
                       // System.Diagnostics.Debug.WriteLine("Cas stop System.Int16: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Int16", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.UInt16))
                    {
#if DEBUG
//System.Diagnostics.Debug.WriteLine("Cas start System.UInt16: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.UInt16", id);
#endif
                        collection.Add(n, System.UInt16.Parse(v));
#if DEBUG
                       // System.Diagnostics.Debug.WriteLine("Cas stop System.UInt16: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.UInt16", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Int32))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Int32: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Int32", id);
#endif
                        collection.Add(n, System.Int32.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Int32: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Int32", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.UInt32))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.UInt32: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.UInt32", id);
#endif
                        collection.Add(n, System.UInt32.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.UInt32: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.UInt32", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Int64))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Int64: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Int64", id);
#endif
                        collection.Add(n, System.Int64.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Int64: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Int64", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.UInt64))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.UInt64: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.UInt64", id);
#endif
                        collection.Add(n, System.UInt64.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.UInt64: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.UInt64", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.SByte))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.SByte: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.SByte", id);
#endif
                        collection.Add(n, System.SByte.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.SByte: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.SByte", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Byte))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Byte: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Byte", id);
#endif
                        collection.Add(n, System.Byte.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Byte: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Byte", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Single))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Single: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Single", id);
#endif
                        collection.Add(n, System.Single.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Single: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Single", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Double))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Double: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Double", id);
#endif
                        collection.Add(n, System.Double.Parse(v));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Double: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Double", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Char))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Char: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Char", id);
#endif
                        collection.Add(n, (Char)v[0]);
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Char: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Char", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Decimal))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Decimal: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Decimal", id);
#endif
                        collection.Add(n, System.Decimal.Parse(v));
#if DEBUG
                        ///System.Diagnostics.Debug.WriteLine("Cas stop System.Decimal: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Decimal", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Boolean))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Boolean: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Boolean", id);
#endif
                        collection.Add(n, System.Boolean.Parse(v));
#if DEBUG
                        ///System.Diagnostics.Debug.WriteLine("Cas stop System.Boolean: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Boolean", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Windows.Forms.DockStyle))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Windows.Forms.DockStyle: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Windows.Forms.DockStyle", id);
#endif
                        collection.Add(n, (DockStyle)Enum.Parse(typeof(DockStyle), v.Trim(), true));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Windows.Forms.DockStyle: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Windows.Forms.DockStyle", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Drawing.Font))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Drawing.Font: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Drawing.Font", id);
#endif
                        Font f = v.String2Font();

                       if (f != null)
                           collection.Add(n, f);

#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Drawing.Font: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Drawing.Font", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Drawing.Point))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Drawing.Point: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Drawing.Point", id);
#endif
                        Point? p = v.String2Point();

                        if (p != null)
                            collection.Add(n, (Point)p);

#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Drawing.Point: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Drawing.Point", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Drawing.Size))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Drawing.Size: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Drawing.Size", id);
#endif
                        Size? s = v.String2Size();

                        if (s != null)
                            collection.Add(n, (Size)s);
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Drawing.Size: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Drawing.Size", id);
#endif
                        return;
                    }
//                    else if (t == typeof(System.Drawing.Color))
//                    {

                        
//#if DEBUG
//                        //System.Diagnostics.Debug.WriteLine("Cas start System.Drawing.Size: " + sw.ElapsedMilliseconds.ToString() + " ms");
//                        //Logging.Trace2.Write("Start-Parse", "System.Drawing.Size", id);
//#endif
//                        Color? s = v.String2Color();

//                        if (s != null)
//                            collection.Add(n, (Color)s);
                        
//#if DEBUG
//                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Drawing.Size: " + sw.ElapsedMilliseconds.ToString() + " ms");
//                        //Logging.Trace2.Write("Stop-Parse", "System.Drawing.Size", id);
//#endif
//                        return;
//                    }
                    else if (t == typeof(System.Drawing.ContentAlignment))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Drawing.ContentAlignment: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Drawing.ContentAlignment", id);
#endif
                        collection.Add(n, (System.Drawing.ContentAlignment)Enum.Parse(typeof(System.Drawing.ContentAlignment), v.Trim(), true));
#if DEBUG
                        ///System.Diagnostics.Debug.WriteLine("Cas stop System.Drawing.ContentAlignment: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Drawing.ContentAlignment", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Windows.Forms.HorizontalAlignment))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Windows.Forms.HorizontalAlignment: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Windows.Forms.HorizontalAlignment", id);
#endif
                        collection.Add(n, (System.Windows.Forms.HorizontalAlignment)Enum.Parse(typeof(System.Windows.Forms.HorizontalAlignment), v.Trim(), true));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Windows.Forms.HorizontalAlignment: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Windows.Forms.HorizontalAlignment", id);
#endif
                        return;
                    }
                    else if (t == typeof(System.Windows.Forms.PictureBoxSizeMode))
                    {
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas start System.Windows.Forms.PictureBoxSizeMode: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Start-Parse", "System.Windows.Forms.PictureBoxSizeMode", id);
#endif
                        collection.Add(n, (System.Windows.Forms.PictureBoxSizeMode)Enum.Parse(typeof(System.Windows.Forms.PictureBoxSizeMode), v.Trim(), true));
#if DEBUG
                        //System.Diagnostics.Debug.WriteLine("Cas stop System.Windows.Forms.PictureBoxSizeMode: " + sw.ElapsedMilliseconds.ToString() + " ms");
                        //Logging.Trace2.Write("Stop-Parse", "System.Windows.Forms.PictureBoxSizeMode", id);
#endif
                        return;
                    }
                    else
                    {
                        /// ... logovat ... 
                        Logging.Log.Write("Lokalizace,ParseTypeValue: " + t.ToString() + " chyby. Pridat!!");
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Lokalizace,ParseTypeValue chyba.");
                return;
            }
            finally 
            {
#if DEBUG
                //System.Diagnostics.Debug.WriteLine("Cas Stop celkovy: " + sw.ElapsedMilliseconds.ToString() + " ms");
                //Logging.Trace2.Write("Stop-Parse", "Type t = Type.GetType(ntype.Value);", id);
                //sw.Stop();
                //sw = null;
#endif
            }
        }

        public static object GetValue(string key)// , string defalutValue)
        {
            try
            {
                return localizationData[key];

                //string val = localizationData[key];
                //return val;
                //if (val == null)
                //    SetValue(key, defalutValue);

                //return (val != null ? val : defalutValue);
            }
            catch
            {
                //SetValue(key, defalutValue);
                return null;
            }
        }

        //private static bool ContainsKey(NameValueCollection collection, string key)
        //{
        //    if (collection.Get(key) == null)
        //    {
        //        return collection.AllKeys.Contains(key);
        //    }

        //    return true;
        //}

        //public enum LocalType
        //{
        //    Výchozí,
        //    Čeština,
        //    Slovenština
        //}
        //public class LocalizationItem
        //{
        //    public LocalType Type { get; set; }
        //    public LocalValue Value { get; set; }

        //    public override string ToString()
        //    {
        //        return Type.ToString() + " (" + Value.ToString() + ")";
        //    }

        //    public LocalizationItem(LocalType type, LocalValue value)
        //    {
        //        this.Type = type;
        //        this.Value = value;
        //    }
        //}

    }
}
