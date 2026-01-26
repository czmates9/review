using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Vyroba_P.WebServiceVyroba
{
    public class CustomVyroba : _WebRefernces_Globals.WebServiceVyrobaSession
    {

        //public Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet OpenedCorrection2(string userID, string MachineID)
        //{
        //    string data = OpenedCorrection_test(userID, MachineID);
        //    Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet vds = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet>(data);

        //    return vds;
        //}
        public Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet Production_OpenedCorrection2(string userID, string MachineID)
        {
            string data = Production_OpenedCorrection_Json(userID, MachineID);
            Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet vds = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet>(data);

            return vds;
        }

        public Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet Production_OpenedProduction2(string userID, string MachineID)
        {
            string data = Production_OpenedProduction_Json(userID, MachineID);
            Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet vds = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet>(data);

            return vds;
        }
    }
}