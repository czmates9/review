using Fask.ModuleSql_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModuleSql
{
    public partial class Provider :         
        // nove interfacy ... 
        Fask.Interfaces.Parametry.IParametry2,
        Fask.Interfaces.Parametry.IParametry2_ConnectionString

    {
        /// <summary>
        /// Vstupni bod ...
        /// </summary>
        public Provider()
        {
            //try
            //{
            //    Fask.Server.Interfaces.Configuration.IConfiguration iconfig = this as Fask.Server.Interfaces.Configuration.IConfiguration;
            //    if (iconfig != null)
            //        iconfig.LoadConfiguration();

            //}
            //catch (Exception ex)
            //{
            //    // TODO : zalogovat chybu, poslat chybu logovacim mechanismem ... 
            //    throw ex;
            //}
        }

        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        public string ConnectionString { get; set; }




    }
}
