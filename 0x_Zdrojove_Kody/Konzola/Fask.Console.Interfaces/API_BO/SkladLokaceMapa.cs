using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Console.Interfaces.API_BO
{
    /// <summary>
    /// CZMST_SkladLokace_Mapa
    /// </summary>
    public class SkladLokaceMapa_CSV00_row
    {
        [CsvHelper.Configuration.Attributes.Name("Index")]
        public int DEX_ROW_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("ID skladu")]
        public string SKL_ID { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Lokace")]
        public string LOCNCODE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Typ lokace")]
        public string TYPE { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Car. kod lokace")]
        public string Barcode { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Oznaceni lokace")]
        public string Description { get; set; }

        [CsvHelper.Configuration.Attributes.Name("Oznaceni skladu")]
        public string SkladOznaceni { get; set; }

    }
}
