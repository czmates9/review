using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql_API
{

    public partial class Provider :
      Fask.Interfaces.Parametry.IParametry2,
      Fask.Interfaces.Parametry.IParametry2_Get_APIConnection,
      Fask.Interfaces.Parametry.IParametry2_Terminal_ID
    {

        public string _terminal_ID;
        public string Terminal_ID { get => _terminal_ID; set => _terminal_ID = value; }

        public void Get_APIConnection(out string adresa, out string autorizace_DoAPI, out string aliasDB, out bool ishttps, out int timeout)
        {
            Globals_V1.LoadConfiguration();

            adresa = Globals_V1.Konfigurace.Nastaveni[0].Adresa; //   "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka 
            autorizace_DoAPI = Globals_V1.Konfigurace.Nastaveni[0].Autorizace_DoAPI; //"MDox",
            aliasDB = Globals_V1.Konfigurace.Nastaveni[0].AliasDB; // "api",
            ishttps = Globals_V1.Konfigurace.Nastaveni[0].isHTTPS; // false,
            timeout = Globals_V1.Konfigurace.Nastaveni[0].API_TimeOut; //10000

        }
    }
}
