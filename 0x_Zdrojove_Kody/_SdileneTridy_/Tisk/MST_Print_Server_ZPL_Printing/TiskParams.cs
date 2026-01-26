using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MST_Print_Server_ZPL_Printing
{
    public class TiskParams
    {
        /// <summary>
        /// IP adresa tiskarny na kterou se bude tisknout
        /// </summary>
        public string CONFIG_IP = "";

        /// <summary>
        /// Port tiskarny
        /// </summary>
        public string CONFIG_IP_PORT = "";

        /// <summary>
        /// Jmeno tiskarny na kterou se tiskne
        /// </summary>
        public string CONFIG_NAME = "";

        /// <summary>
        /// Nastaveni COM portu tiskarny
        /// </summary>
        public string CONFIG_COM = "";

        /// <summary>
        /// Typ výtisku (A4, A5 a pod.) - tisk soupisu
        /// </summary>
        public string CONFIG_TYPE = string.Empty;

        /// <summary>
        /// výška papíru - tisk soupisu
        /// </summary>
        public int HEIGHT_PAPER_SIZE = 0;

        /// <summary>
        /// šířka papíru - tisk soupisu
        /// </summary>
        public int WIDTH_PAPER_SIZE = 0;

        /// <summary>
        /// Nazev pouziteho formatu ...
        /// </summary>
        public string PAPER_KIND = "Custom"; // defaultne bude custom ...

        public TiskParams()
        {
        }

        public TiskParams(string ip, string ip_port, string name, string com_port)
        {
            this.CONFIG_IP = ip;
            this.CONFIG_IP_PORT = ip_port;
            this.CONFIG_NAME = name;
            this.CONFIG_COM = com_port;
        }
        

    }
}
