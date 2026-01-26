using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advantech.Adam;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;
using System.Threading;
using FASK.SledovaniVyroby.ErrorLog;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    public class Adam60XX
    {
        public delegate void Adam60XXTimerWorkingHandler();
        public event Adam60XXTimerWorkingHandler Adam60XXTimerWorkingEvent;
        protected void OnAdam60XXTimerWorking()
        {
            if (Adam60XXTimerWorkingEvent != null)
                Adam60XXTimerWorkingEvent();
        }

        private AdamSocket adamModbusDI;
        //private AdamSocket adamModbusDO;

        private bool[] bData = null;
        private bool[] bDataLast = null; //1. inicializace pri null ... posledni stav cidel, zda jsou activated (nahrazuje se promenne DIXSensorActivated...
        private bool[] RLData;

        private System.Threading.Timer timer;
        //private int ackDISensor = 0; // ??? nejak nahradit ...???
        private int prohazSensorID = 4;
        private bool prohazSensorState = false;

        private bool setDO = false;
        private int[] dataToSetDO = new int[] { };

        private long[] lHigh = new long[] { 100, 100, 100, 100, 100, 100 };
        private long[] lLow = new long[] { 100, 100, 100, 100, 100, 100 };

        //private bool DI0SensorActivated = false;
        //private bool DI1SensorActivated = false;
        //private bool DI2SensorActivated = false;
        //private bool DI3SensorActivated = false;
        //private bool DI4SensorActivated = false;

        private int _timerPeriod = 450;
        private int _timeoutTCP = 100;

        private int releLinka = 2;
        private int releHoukacka = 3;

        private string ipAddress = string.Empty;
        private int adamPort = 502;// modbus TCP port is 502

        public Adam60XX(string ipAddress, int ackDI, int timerPeriod, int timeouttcp, int cisloReleLinka, int cisloReleHoukacka, int minHighLevelWidth, int cisloReleProhaz)
        {
            this.ipAddress = ipAddress;					
            //ackDISensor = ackDI; //k cemu je to dobre ???

            this.prohazSensorID = cisloReleProhaz;

            lHigh[0] = lHigh[1] = lHigh[2] = lHigh[3] = lHigh[4] = lHigh[5] = minHighLevelWidth;

            this.releLinka = cisloReleLinka;
            this.releHoukacka = cisloReleHoukacka;

            this._timeoutTCP = timeouttcp;
            this._timerPeriod = timerPeriod;

            adamModbusDI = new AdamSocket();
            adamModbusDI.SetTimeout(_timeoutTCP, _timeoutTCP, _timeoutTCP); // set timeout for TCP      
            adamModbusDI.AdamSeriesType = AdamType.Adam6000;

            //adamModbusDO = new AdamSocket();
            //adamModbusDO.SetTimeout(2000, 2000, 2000); // set timeout for TCP      
            //adamModbusDO.AdamSeriesType = AdamType.Adam6000; 
            //m_Adam6000Type = Adam6000Type.Adam6060;

            if (!adamModbusDI.Connect(ipAddress, System.Net.Sockets.ProtocolType.Tcp, adamPort))
                MessageBox.Show("ADAMDI: Connect to " + ipAddress + " failed", "Error");
            else
            {
                if (!adamModbusDI.DigitalInput().SetDigitalFilterMiniSignalWidth(lHigh, lLow))
                    MessageBox.Show("ADAMDI: Nepodařilo se nastavit digitalní filtr na vstupní porty", "Error");
            }

            //if (!adamModbusDO.Connect(ipAddress, System.Net.Sockets.ProtocolType.Tcp, adamPort))
            //    MessageBox.Show("ADAMDO: Connect to " + ipAddress + " failed", "Error");

            timer = new System.Threading.Timer(TimerCallback, null, 0, _timerPeriod);
            //timer = new System.Threading.Timer(TimerCallback, null, _timerPeriod, System.Threading.Timeout.Infinite);
        }

        private void OffTimer()
        {
            if (timer != null)
                timer.Change(System.Threading.Timeout.Infinite, _timerPeriod);
                //timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void OnTimer()
        {
            if (timer != null)
                timer.Change(_timerPeriod, _timerPeriod);
                //timer.Change(_timerPeriod, System.Threading.Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            // TODO : add adamworking event ...
            OnAdam60XXTimerWorking();

            OffTimer();

            RefreshDIO();

            OnTimer();
        }

        public bool[] getData()
        {
            return bData;
        }

        public bool[] getRLData()
        {
            return RLData;
        }

        public void close()
        {
            OffTimer();
            timer = null;
            adamModbusDI.Disconnect(); // disconnect slave
            //adamModbusDO.Disconnect();
        }

        public void zapniHoukacku()
        {
            SetSingleCoil(releHoukacka, 1);
        }

        public void vypniHoukacku()
        {
            SetSingleCoil(releHoukacka, 0);
        }

        public bool GetProhazSensorState()
        {
            return this.prohazSensorState;
        }
        
        private bool _LinkaActualStav = false;
        public bool rizeniLinky(DataVyroba.LinkaStav stav)
        {
            if (stav == DataVyroba.LinkaStav.LINKA_ON && (_LinkaActualStav == false))
            {
                Log.Write("zapnilinku");
                SetSingleCoil(releLinka, 1);
                _LinkaActualStav = true;
            }
            else if (stav == DataVyroba.LinkaStav.LINKA_OFF && (_LinkaActualStav == true))
            {
                Log.Write("vypniLinku");
                SetSingleCoil(releLinka, 0);
                _LinkaActualStav = false;
            }

            return _LinkaActualStav;

        }

        private void RefreshDIO()
        {
            try
            {
                int iDiStart = 1, iDoStart = 17;
                int iChTotal = 12;
                bool[] bDiData, bDoData;

                //if (AgroConfig.config.Agro[0].DebugLoging)
                //    Log.WriteAdamCSV(1, 0, 0);

                if (adamModbusDI.Modbus().ReadCoilStatus(iDoStart, 6, out bDoData))
                {
                    RLData = new bool[6];
                    Array.Copy(bDoData, 0, RLData, 0, 6);
                }
                else
                    Log.Write("Nepodarilo se nacist stavy RELE z ADAMa. Adam error code: " + adamModbusDI.LastError.ToString());

                if (adamModbusDI.Modbus().ReadInputStatus(iDiStart, 6, out bDiData))
                {
                    if (bDataLast == null)
                    {
                        bDataLast = new bool[iChTotal];
                        //for (int i = 0; i < bDataLast.Length; i++)
                        //{
                        //    bDataLast[i] = false;
                        //}
                    }
                    //if (bData != null)
                    //    Array.Copy(bData, 0, bDataLast, 0, 6);
                    bData = new bool[iChTotal];
                    Array.Copy(bDiData, 0, bData, 0, 6);

                    //if (AgroConfig.config.Agro[0].DebugLoging)
                    //Log.WriteAdamCSV(0, 1, bData[ackDISensor] ? 1 : 0);

                    for (int iSensor = 0; (iSensor < bData.Length) && (iSensor < bDataLast.Length); iSensor++)
                    { // zjisti a nastavi stav pro kazdy senzor...
                      // v pripade zmeny, zavola udalost adameventhandler ...
                        //if (bData[ackDISensor] && !DI0SensorActivated)
                        if (bData[iSensor] && !bDataLast[iSensor])
                        {
                            //DI0SensorActivated = true;
                            bDataLast[iSensor] = true;

                            //if (DataReady != null)
                            //    DataReady.BeginInvoke(new AdamEventHandlerArgs(ackDISensor), null, null);
                            DataReady(new AdamEventHandlerArgs(iSensor));
                        }
                        else if (!bData[iSensor])
                        { //NEW: v nule(false) je cidlo neaktivni...
                            //DI0SensorActivated = false;
                            bDataLast[iSensor] = false;
                        }
                    }


                    #region pocet sepnuti vsech cidel - ToDo
                    /*
                if (bData[1] && !DI1SensorActivated)
                {
                    DI1SensorActivated = true;
                    DataReady(new AdamEventHandlerArgs(1));
                }
                else if (!bData[1])
                    DI1SensorActivated = false;


                if (bData[2] && !DI2SensorActivated)
                {
                    DI2SensorActivated = true;
                    DataReady(new AdamEventHandlerArgs(2));
                }
                else if (!bData[2])
                    DI2SensorActivated = false;

                if (bData[3] && !DI3SensorActivated)
                {
                    DI3SensorActivated = true;
                    DataReady(new AdamEventHandlerArgs(3));
                }
                else if (!bData[3])
                    DI3SensorActivated = false;*/
                    #endregion

                    //1 - linka aktivni
                    //0 - prohaz
                    this.prohazSensorState = !bData[prohazSensorID];
                }
                else
                {
                 //   if (AgroConfig.config.Agro[0].DebugLoging)
//Log.WriteAdamCSV(0, 1, adamModbusDI.LastError == Advantech.Common.ErrorCode.Socket_Recv_Fail ? -1 : -2);

                    Log.Write("Nepodarilo se nacist data z ADAMa. Adam error code: " + adamModbusDI.LastError.ToString());

                    if (!adamModbusDI.Connected)
                        adamModbusDI.Connect(ipAddress, System.Net.Sockets.ProtocolType.Tcp, adamPort);
                }

                SetDO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void SetDO()
        {
            if (setDO)
            {
                if (dataToSetDO.Length > 0)
                {
                    bool onOff = dataToSetDO[1] == 1 ? true : false;
                    int iStart = 16 + dataToSetDO[0];

                   // if (AgroConfig.config.Agro[0].DebugLoging)
                   //     Log.WriteAdamCSV(2, 0, 0);

                    if (adamModbusDI.Modbus().ForceSingleCoil(iStart, onOff))
                    {
                   //     if (AgroConfig.config.Agro[0].DebugLoging)
                    //        Log.WriteAdamCSV(0, 2, 1);
                    }
                    else
                    {
                        Log.Write("ForceSingleCoil: on/off:" + dataToSetDO[1] + " start:" + iStart + ",False - ForceSingleCoil, Adam error code: " + adamModbusDI.LastError.ToString());

                       // if (AgroConfig.config.Agro[0].DebugLoging)
                      //      Log.WriteAdamCSV(0, 2, 0);
                    }

                    setDO = false;
                }
            }
        }

        private void SetSingleCoil(int index, int onOff)
        {
            int[] param = new int[] { index, onOff }; 

            setDO = true;
            dataToSetDO = (int[])param;
            /*
            Thread t = new Thread(new ParameterizedThreadStart(ForceSingleCoil));
            t.Start(n);*/
        }

        /*
        private void ForceSingleCoil(object param)
        {
            try
            {

                int[] n = (int[])param;
                bool onOff = n[1] == 1 ? true : false;

                int iStart = 16 + n[0];

                if(iStart == 17)

                if (AgroConfig.config.Agro[0].DebugLoging)
                    Log.WriteAdamCSV(1, 0, 0);

                //Log.Write("ForceSingleCoil: on/off:" + n[1] + " start:" + iStart);

                if (adamModbusDI.Modbus().ForceSingleCoil(iStart, onOff))
                    Log.Write("True - ForceSingleCoil");
                else
                    Log.Write("False - ForceSingleCoil, Adam error code: " + adamModbusDI.LastError.ToString());

            }
            catch (Exception ex)
            {
                Log.Write("ForceSingleCoil:" + ex.Message);
            }
        }
        */

        public delegate void AdamEventHandler(AdamEventHandlerArgs e);

        public class AdamEventHandlerArgs : EventArgs
        {
            public AdamEventHandlerArgs(int data)
            {
                this._data = data;
            }

            private int _data = 0;
            public int Data
            {
                get { return _data; }
            }

            public override string ToString()
            {
                try
                {
                    return this._data + "\r\n";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public event AdamEventHandler DataReady;
    }
}