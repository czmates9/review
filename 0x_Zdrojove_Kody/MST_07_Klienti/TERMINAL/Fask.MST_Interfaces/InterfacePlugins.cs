using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MST_Interfaces
{
    /// <summary>   
    /// Rozhraní všech pluginů.    
    /// </summary>   
    public interface IPluginBase
    {
        void Load(IApplicationBase app);

        bool Tiskni(string[] SN, Fask.MST_W.TiskData zaznamy, string idterminal);

        string Nazev
        {
            get;
        }

        string Titulek
        {
            get;
        }
    }
}
