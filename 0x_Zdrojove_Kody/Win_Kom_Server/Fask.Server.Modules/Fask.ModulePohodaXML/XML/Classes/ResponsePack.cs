using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL.XML.Classes
{
    /// <summary>
    /// Pohoda response pack version 2.0
    /// </summary>
    public class ResponsePack
    {
        public string version { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public string note { get; set; }
        public string programVersion { get; set; }

        public List<ResponsePackItem> responsePackItems { get; set; }        

        public static ResponsePack ParseResponse(string filename) {
            ResponsePack responsePack = new ResponsePack();

            System.Xml.XmlDocument xmldoc = null;
            try
            {
                xmldoc = new System.Xml.XmlDocument();
                xmldoc.LoadXml(filename);

                //xmldoc.DocumentElement
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.XML.Classes.ResponsePack", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (xmldoc != null)
                {
                    //xmldoc.   // zavira se nejak ???
                }
            }


            return responsePack;
        }
    }
}
