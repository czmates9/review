using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.Vyroba_P.Data
{
    public partial class VyrobaCEDataSet
    {
        public partial class CZPRO_VPHRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.SOPNUMBE.Trim() + " : " + this.SOPTYPE.Trim();// +" : " + this.SOPDESC.Trim();
            }
        }

        public partial class CZPRO_VPPRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.ITEMDESC.Trim();
            }
        }

        public partial class LoginsRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return (this.firstname.Trim() + " " + this.surname.Trim()).Trim();
            }
        }

        public partial class MachinesRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.name.Trim();
            }
        }

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
