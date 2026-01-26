using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Console.Interfaces.API_BO
{
    /// <summary>
    /// CZMST091
    /// </summary>
    public class Strediska_CSV00_row
    {
        [CsvHelper.Configuration.Attributes.Name("Index")]
        public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID")]
        public string str_id { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Popis")]
        public string str_desc { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ")]
        public string str_typ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod")]
        public string str_carcode { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Sklad ID")]
        public string skl_id { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Odberatel ID")]
        public string odb_id { get; set; }


    }
}
