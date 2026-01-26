using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Console.Interfaces.API_BO
{
    public class Odberatele_CSV00_row
    {
        [CsvHelper.Configuration.Attributes.Name("Index")]
        public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID")]
        public string odb_id { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Oznaceni")]
        public string odb_desc { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ")]
        public string odb_typ { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod")]
        public string odb_carcode { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ICO")]
        public string odb_ico { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID meny")]
        public string mena_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Misto sidla")]
        public string odb_misto { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Ulice")]
        public string odb_ulice { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Orientacni cislo")]
        public string odb_cisloOr { get; set; }
        [CsvHelper.Configuration.Attributes.Name("PSC")]
        public string odb_psc { get; set; }
        [CsvHelper.Configuration.Attributes.Name("DIC")]
        public string odb_dic { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Odberatel")]
        public bool? odb_Odberatel { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Dodavatel")]
        public bool? odb_Dodavatel { get; set; }




    }
}
