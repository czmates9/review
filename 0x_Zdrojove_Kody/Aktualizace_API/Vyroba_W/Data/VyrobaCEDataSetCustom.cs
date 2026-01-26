using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.Vyroba_W.Data
{
    public partial class VyrobaCEDataSet
    {
        public partial class CorrectsRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.desc;
            }
        }
    }
}
