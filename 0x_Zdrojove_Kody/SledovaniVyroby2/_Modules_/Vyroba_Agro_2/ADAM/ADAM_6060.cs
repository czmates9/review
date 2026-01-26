using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advantech.Adam;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;
using System.Threading;
using FASK.SledovaniVyroby.ErrorLog;
using System.Diagnostics;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.ADAM
{
    public class ADAM_6060
    {

        public enum AdamCommunication
        {
            Modbus,
            P2P
        }

        public delegate void Adam60XXTimerWorkingHandler();
        public event Adam60XXTimerWorkingHandler Adam60XXTimerWorkingEvent;
        protected void OnAdam60XXTimerWorking()
        {
            if (Adam60XXTimerWorkingEvent != null)
                Adam60XXTimerWorkingEvent();
        }

        private AdamSocket adamModbusDI;
        //private AdamSocket adamModbusDO;
        private AdamP2P adamP2P;

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

        public ADAM_6060(AdamCommunication adamcommunication, string ipAddress, int ackDI, int timerPeriod, int timeouttcp, int cisloReleLinka, int cisloReleHoukacka, int minHighLevelWidth, int cisloReleProhaz)
        {
            this.ipAddress = ipAddress;
            //ackDISensor = ackDI; //k cemu je to dobre ???

            this.prohazSensorID = cisloReleProhaz;

            lHigh[0] = lHigh[1] = lHigh[2] = lHigh[3] = lHigh[4] = lHigh[5] = minHighLevelWidth;

            this.releLinka = cisloReleLinka;
            this.releHoukacka = cisloReleHoukacka;

            this._timeoutTCP = timeouttcp;
            this._timerPeriod = timerPeriod;

            //adamModbusDO = new AdamSocket();
            //adamModbusDO.SetTimeout(2000, 2000, 2000); // set timeout for TCP      
            //adamModbusDO.AdamSeriesType = AdamType.Adam6000; 
            //m_Adam6000Type = Adam6000Type.Adam6060;

            switch (adamcommunication)
            {
                case AdamCommunication.P2P:
                    adamP2P = new AdamP2P(1000, 1000);
                    adamP2P.GetDataEvent += new GetP2PDataCallback(adamP2P_GetDataEvent);
                    adamP2P.Start_P2P_Server();
                    break;
                case AdamCommunication.Modbus:
                default:
                    adamModbusDI = new AdamSocket();
                    adamModbusDI.SetTimeout(_timeoutTCP, _timeoutTCP, _timeoutTCP); // set timeout for TCP      
                    adamModbusDI.AdamSeriesType = AdamType.Adam6000;

                    if (!adamModbusDI.Connect(ipAddress, System.Net.Sockets.ProtocolType.Tcp, adamPort))
                    {
                        MessageBox.Show("ADAMDI: Connect to " + ipAddress + " failed", "Error");
                    }
                    else
                    {
                        if (!adamModbusDI.DigitalInput().SetDigitalFilterMiniSignalWidth(lHigh, lLow))
                            MessageBox.Show("ADAMDI: Nepodařilo se nastavit digitalní filtr na vstupní porty", "Error");
                    }

                    timer = new System.Threading.Timer(TimerCallback, null, 0, _timerPeriod);
                    //timer = new System.Threading.Timer(TimerCallback, null, _timerPeriod, System.Threading.Timeout.Infinite);

                    break;
            }
        }

        byte[] dataLast = null;
        byte[] dataActual = null;
        byte[] dataCOSFlag = null;
        void adamP2P_GetDataEvent(P2P_Config config)
        {
            // inicializace 
            dataActual = config.Data;
            if (dataLast == null)
                dataLast = dataActual;

            dataCOSFlag = config.COS_Flag;

            string dataActualString = string.Empty;
            dataActual.ToList().ForEach( x => dataActualString += x.ToString());

            string dataLastString = string.Empty;
            dataLast.ToList().ForEach(x => dataLastString += x.ToString());

            string dataCOSFlagString = string.Empty;
            dataCOSFlag.ToList().ForEach(x => dataCOSFlagString += x.ToString());

            Debug.WriteLine("=======");
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + "adamP2P:" + " dataActual  : " + dataActualString);
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + "adamP2P:" + " dataLast    : " + dataLastString);
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + "adamP2P:" + " dataCOSFlag : " + dataCOSFlagString);

            for (int i = 0; i < dataActual.Length; i++)
			{
                if (dataActual[i] != dataLast[i])
                { // doslo ke zmene stavu
                    // ale na jakem cidle?
                    byte x = dataActual[i];
                    byte y = dataLast[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if (((x & 1) == 1) == ((y & 1) == 0))
                        {
                            DataReady(new AdamEventHandlerArgs(i * 8 + j));
                        }
                        x >>= 1;
                        y >>= 1;
                    }
                }
			}


            dataLast = dataActual;

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

        private DateTime lastcall = DateTime.Now;
        private void TimerCallback(object state)
        {
#if DEBUG
            DateTime lastcallTmp = DateTime.Now;
            TimeSpan lastcallDealy = lastcallTmp - lastcall;
            lastcall = lastcallTmp;
            System.Threading.Thread.CurrentThread.Name = "Adam60XX : " + lastcall.ToString("hh:mm:ss.fff");
            //System.Diagnostics.Debug.WriteLine("Adam60XX : " + lastcall.ToString("hh:mm:ss.fff"));
            System.Diagnostics.Debug.WriteLine("Adam60XX [ms]: " + Thread.CurrentThread.ManagedThreadId.ToString("X") + " " + lastcallDealy.TotalMilliseconds);
#endif

            // TODO : add adamworking event ...
            OnAdam60XXTimerWorking();

            OffTimer();

            RefreshDIO();

            OnTimer();

#if DEBUG
            System.Diagnostics.Debug.WriteLine("Adam60XX end");
#endif
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
            if (adamModbusDI != null)
                adamModbusDI.Disconnect(); // disconnect slave

            if (adamP2P != null)
                adamP2P.Stop_P2P_Server();
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
                {
#if !DEBUG
                    Log.Write("Nepodarilo se nacist stavy RELE z ADAMa. Adam error code: " + adamModbusDI.LastError.ToString());
#endif
                }

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
                    {   // zjisti a nastavi stav pro kazdy senzor...
                        // v pripade zmeny, zavola udalost adameventhandler ...
                        //if (bData[iSensor] && !bDataLast[iSensor])
                        //{
                        //    bDataLast[iSensor] = true;
                        //    DataReady(new AdamEventHandlerArgs(iSensor));
                        //}
                        //if (bData[iSensor])
                        //{
                        //     if (!bDataLast[iSensor])
                        //         DataReady(new AdamEventHandlerArgs(iSensor));

                        if (bData[iSensor] && !bDataLast[iSensor])
                            DataReady(new AdamEventHandlerArgs(iSensor));
                        bDataLast[iSensor] = bData[iSensor];

                    }

                    //1 - linka aktivni
                    //0 - prohaz
                    this.prohazSensorState = !bData[prohazSensorID];
                }
                else
                {
                    //   if (AgroConfig.config.Agro[0].DebugLoging)
                    //Log.WriteAdamCSV(0, 1, adamModbusDI.LastError == Advantech.Common.ErrorCode.Socket_Recv_Fail ? -1 : -2);

#if !DEBUG
                    Log.Write("Nepodarilo se nacist data z ADAMa. Adam error code: " + adamModbusDI.LastError.ToString());
#endif

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
        public event AdamEventHandler DataReady;
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
    }
}