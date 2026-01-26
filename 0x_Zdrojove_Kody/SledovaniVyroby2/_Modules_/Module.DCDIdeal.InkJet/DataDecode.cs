using Fask.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.DCDIdeal.InkJet
{
    public class DataDecode
    {
        private const char _separator_fields = ',';
        private const char _separator_keyvalue = '=';
        private const char _separator_value = '"';

        public string Value { get; private set; }
        public List<string> Texty { get; private set; }
        public Dictionary<string, string> Hodnoty { get; private set; }

        public DataDecode()
        {
            //this.Value = string.Empty;
            this.Texty = new List<string>();
            this.Hodnoty = new Dictionary<string, string>();
        }

        public static DataDecode Parse(string dataFull)
        {
            DataDecode dataDecode = new DataDecode();

            //rozdelit na key=value
            var datas = dataFull.Split(_separator_fields);

            foreach (var d in datas)
            {
                if (d.Trim().Length > 0)
                    dataDecode.Texty.Add(d.Trim());

                try
                {
                    if (!d.Contains(_separator_keyvalue))
                        continue;

                    // rozdelit na key a value pair
                    var data = d.Split(_separator_keyvalue);
                    if (data.Length == 2) // hledana hodnota
                    {
                        if (data[0] == "ST[1]")
                            dataDecode.Value = data[1].Trim(_separator_value);

                        dataDecode.Hodnoty[data[0]] = data[1];
                    }

                }
                catch (Exception exParse)
                {
                    //Log.WriteException(exParse.Message);
                    ExceptionHandler2.Handle(exParse);
                    dataDecode = new DataDecode();
                }

            }

            return dataDecode;
        }
    }
}
