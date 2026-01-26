using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FASK.SledovaniVyroby.IScannerProvider;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes
{
    public class Sensor
    {
        private DateTime activatedtime;
        public DateTime ActivatedTime
        {
            get { return this.activatedtime; } 
        }
        private bool active = false;
        public bool Active
        {
            get { return this.active; }
        }

        public Sensor(DateTime activatedtime, bool active)
        {
            this.activatedtime = activatedtime;
            this.active = active;
        }

        public override string ToString()
        {            
            //return base.ToString();
            return this.activatedtime.ToString() + " A:" + this.active.ToString();
        }
    }
}
