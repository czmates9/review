using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.MySystem
{
    public class AppConfiguration
    {
        public string NameConfiguratin { get; set; }

        public string DataFolderPath { get; set; }


        public AppConfiguration(string name, string path)
        {
            this.NameConfiguratin = name;
            this.DataFolderPath = path;
        }

        public override string ToString()
        {
            return NameConfiguratin;
        }
    }
}
