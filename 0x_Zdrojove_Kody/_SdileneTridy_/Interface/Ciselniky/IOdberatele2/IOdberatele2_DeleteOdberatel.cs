using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_DeleteOdberatel : IOdberatele2
    {
        /// <summary>
        /// Smaže odberatele.
        /// </summary>
        /// <param name="id">ID odberatele.</param>
        /// <returns></returns>
        bool DeleteOdberatel(string id);
    }
}
