using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_RozdelitInventury : IInventura2
    {
        // ************** Metody pro pripravu predlohy **************** //
        /// <summary>
        /// rozdeli inventuru do puvodnich
        /// </summary>
        /// <returns></returns>
        StatusInfo RozdelitInventury(List<string> CountEntries);
    }
}
