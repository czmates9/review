using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Konfigurace
{
    public class FASK_Dictionery
    {
        public List<DictionaryPropertyGridAdapter> L => new List<DictionaryPropertyGridAdapter>() { _pohodaInfo, _connectionStrings };

        public Dictionary<string, object> PohodaInfo = new Dictionary<string, object>();

        [Description("Konfigurace PohodaInfo"), Category("PohodaInfo")]
        private DictionaryPropertyGridAdapter _pohodaInfo => new DictionaryPropertyGridAdapter(PohodaInfo);

        public Dictionary<string, object> ConnectionStrings = new Dictionary<string, object>();

        [Description("Konfigurace PohodaInfo"), Category("PohodaInfo")]
        private DictionaryPropertyGridAdapter _connectionStrings => new DictionaryPropertyGridAdapter(ConnectionStrings);

        


    }
}
