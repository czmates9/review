using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_Insert_VazbyAddVyrobky : IVazby2
    {
        int Insert_VazbyAddVyrobky(
            string ID_H, 
            string ID_L, 
            string ITEMNMBR_Def, 
            string DESC_Def, 
            string MJ_Def, 
            string ITEMNMBR_fol, 
            string DESC_Fol, 
            string MJ_Fol, 
            string koef, 
            string ID_USER, 
            DateTime? dateedit, 
            string alter, 
            string PUO);
    }
}
