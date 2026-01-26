using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes
{
    public class Cidlo
    {
        // pocet sepnuti cidla
        private int _SensorsCnt;
        public int SensorsCnt
        {
            get
            {
                return _SensorsCnt;
            }
            //set
            //{
            //    _SensorsCnt = value;
            //}
        }

        public Cidlo()
        {
            _SensorsCnt = 0;
        }

        public virtual void IncremetSensorCount()
        {
            this._SensorsCnt++;
        }

        public virtual void Reset()
        {
            this._SensorsCnt = 0;
        }
    }
}
