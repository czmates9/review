using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class Cidlo
    {
        // pocet sepnuti cidla
        private int sensorsCnt;
        public int SensorsCnt
        {
            get
            {
                return sensorsCnt;
            }
            set
            {
                sensorsCnt = value;
            }
        }

        // aktualni stav sepnuti cidla
        private bool sensorStateActual;
        public bool SensorStateActual
        {
            get
            {
                return sensorStateActual;
            }
        }

        private bool sensorStatePrevious;
        public bool SensorStatePrevious
        {
            get
            {
                return sensorStatePrevious;
            }
        }

        public Cidlo()
        {
            sensorStateActual = sensorStatePrevious = false;
            sensorsCnt = 0;
        }

        public void setSensorStatePreviousActive()
        {
            this.sensorStatePrevious = true;
        }

        public void setSensorStatePreviousDeactive()
        {
            this.sensorStatePrevious = false;
        }

        public void setSensorStateActualActive()
        {
            this.sensorStateActual = true;
        }

        public void setSensorStateActualDeactive()
        {
            this.sensorStateActual = false;
        }
    }
}
