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

using System.IO;

namespace MES_Android
{
    public class Konfigurace_Singleton
    {
        private static Konfigurace _instance = null;
        public static Konfigurace Instance
        {
            get
            {
                if (_instance == null)
                {

                    if (File.Exists(Classes.DataInfo_Static.Konfigurace_JSON))
                    {
                        string JSON = File.ReadAllText(Classes.DataInfo_Static.Konfigurace_JSON);
                        Konfigurace konf = Newtonsoft.Json.JsonConvert.DeserializeObject<Konfigurace>(JSON);

                        if(konf != null)
                        {
                            return konf;
                        }
                    }

                    string s = "Konfigurace nenalezena!";
                    Fask.Logging.ExceptionHandler2.Handle(new Exception(s));
                    throw new Exception(s);
                }
                return _instance;
            }
        }
    }
}