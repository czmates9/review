using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_UpdateEdit : IVazby2
    {

        void UpdateEdit(string koef, string ID_USER, DateTime? dateedit, string alternaiva, string ITEMNMBR_Def, string ITEMNMBR_fol);

    }
}
