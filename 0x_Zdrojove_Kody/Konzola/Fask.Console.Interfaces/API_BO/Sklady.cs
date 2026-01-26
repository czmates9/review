using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Console.Interfaces.API_BO
{
    /// <summary>
    /// CZMST093
    /// </summary>
    public class Sklady_CSV00_row
    {
        [CsvHelper.Configuration.Attributes.Name("Index")]
        public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID skladu")]
        public string skl_id { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Nazev skladu")]
        public string skl_desc { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ")]
        public string skl_typ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod")]
        public string skl_carcode { get; set; }


    }
}
