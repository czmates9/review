using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Configuration
{
    /// <summary>
    /// Rozhrani pro praci s nastavenim modulu
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Windows.Forms.Control GetConfiguration();

        /// <summary>
        /// Ulozi konfiguraci
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}
