using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// třída která nese infomace o objednávce
	/// </summary>
    public class Objednavka
    {
		/// <summary>
		/// ID Objednávky
		/// </summary>
        public string ID { get; set; }

		/// <summary>
		/// číslo objednávky
		/// </summary>
        public string CisloDavky { get; set; }
    }
}
