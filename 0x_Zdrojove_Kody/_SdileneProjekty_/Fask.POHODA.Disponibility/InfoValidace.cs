using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.POHODA.Disponibility
{
    public class InfoValidace
    {

        public InfoValidace(string Status, Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow Radek)
        {
            this.Status = Status;
            this.RadekVydej = Radek;
            DTVydej = null;
        }


        public InfoValidace(string Status, Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable DT)
        {
            this.Status = Status;
            this.RadekVydej = null;
            DTVydej = DT;
        }


        public string Status;
        public Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow RadekVydej;
        public Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable DTVydej;
        
    }
}
