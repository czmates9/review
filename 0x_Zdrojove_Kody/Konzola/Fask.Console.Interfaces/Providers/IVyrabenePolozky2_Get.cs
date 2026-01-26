using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.DataSets;

namespace Fask.Interfaces.Providers
{
    public interface IVyrabenePolozky2_Get : IVyrabenePolozky2
    {


        /// <summary>
        /// Vraci sloucene data z VPH a VPP 
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Hlavni GetVyrabenePolozkyFiltrovane(Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr);

        ///// <summary>
        ///// Vraci sloucene data z VPH a VPP podle zadaneho filtru
        ///// </summary>
        ///// <param name="filtr"></param>
        ///// <returns></returns>
        //Fask.Interfaces.DataSets.Hlavni GetFiltrovane(Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr);

    }
}
