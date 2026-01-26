using RestSharp;
using FASK.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Fask.Logging;

namespace FASK.ADAM
{
    public class ADAM_60XX : ModulBase
    {
        #region Udalosti zmeny stavu cidel
        public const string MType = "ADAM60XX";
        public delegate void AdamEventHandler(AdamEventHandlerArgs e);
        public event AdamEventHandler DataReady_NabeznaHrana;
        public event AdamEventHandler DataReady_SestupnaHrana;
        private bool[] state_AuxFlagsLst = null;
        private bool[] state_AuxFlagsAct = null;
        private bool[] state_AuxFlags = null;
        private string errorAct = string.Empty;
        private string errorLst = string.Empty;
        public bool[] actualState_DI = null;


        private void OnDataReady_NabeznaHrana(AdamEventHandlerArgs e)
        {
            if (DataReady_NabeznaHrana != null)
            {
                DataReady_NabeznaHrana(e);
            }
            else
            {
                ;
            }
        }

        private void OnDataReady_SestupnaHrana(AdamEventHandlerArgs e)
        {
            if (DataReady_SestupnaHrana != null)
            {
                DataReady_SestupnaHrana(e);
            }
            else
            {
                ;
            }
        }

        public class AdamEventHandlerArgs : EventArgs
        {
            public AdamEventHandlerArgs(int data, bool[] DI, string IP)
            {
                this._data = data;
                this._di = DI;
                this._ip = IP;


            }

            private string _ip = string.Empty;
            public string IP
            {
                get { return _ip; }
            }

            private int _data = 0;
            public int Data
            {
                get { return _data; }
            }

            private bool[] _di;
            public bool[] DI {
                get { return _di; }
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
        #endregion

        #region Nastaveni typu ADAMa
        // TODO : Inicializace adama na ruzne typy ???
        // parametry vstupu/vystupu adama
        private int iDiStart = 1;
        private int iDiTotal = 12;
        private int iDoStart = 17;
        private int iDoTotal = 2;
        //private int iChTotal = 12;

        //private int iDiStart = 1;
        //private int iDiTotal = 8;
        //private int iDoStart = 17;
        //private int iDoTotal = 8;
        //private int iChTotal = 16;

        #endregion

        #region Interni promenne
        //private string ipAddress;
        //public string IP_Address {
        //    get { return ipAddress; }
        //}


        private int adamPort = 502;// modbus TCP port is 502
        //private int adamPortP2P = 1025; // defaultni UDP port, na kterem posloucha lokalni UDP p2p sluzba adama => musi byt stejne nakonfigurovano na ADAMovi
        private int timerPeriod;
        private int timeouttcp;


        //public bool minLevelWidth_Enabled = false;
        private long minLevelWidth;

        private int cisloReleLinka;
        private int cisloReleHoukacka;
        private int cisloRelePaletizator;
        private int cisloReleServis;

        //private static Advantech.Adam.AdamInformation adamInformation;
        //TODO: odkomentovat
        private Advantech.Adam.AdamSocket adamTCP = null; 

        private bool adamP2P_started = false;
        private Advantech.Adam.AdamP2P adamP2P = null;

        //vyber komunikace P2P a MODBUS
        private bool communication_TCP = false;
        private bool communication_P2P = false;


        //// *** Stavy *** //
        //private bool _LinkaActualStav = false;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor adam
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="portP2P"></param>
        /// <param name="timerPeriod"></param>
        /// <param name="timeouttcp"></param>
        /// <param name="minLevelWidth"></param>
        /// <param name="TCP_communication"></param>
        /// <param name="P2P_communication"></param>
        public ADAM_60XX(
            string ipAddress,
            int portP2P,
            //int cisloSensorPotvrzovaci, 
            //int cisloSensorPalety,
            int timerPeriod,
            int timeouttcp,
            long minLevelWidth,
            bool TCP_communication,
            bool P2P_communication
            ) : base(ipAddress, portP2P)
        {
            //this.ipAddress = ipAddress;
            //this.adamPortP2P = portP2P;
            this.minLevelWidth = minLevelWidth * 10; // protoze adam to uvadi v hodnotach 0.1ms

            this.timeouttcp = timeouttcp;
            this.timerPeriod = timerPeriod;

            this.communication_TCP = TCP_communication;
            this.communication_P2P = P2P_communication;

            // inicializace modulu
            if (communication_TCP)
                InitializeTCP();

            // Peer 2 Peer
            if (communication_P2P)
                InitializeP2P();

                timerSetDOsAndReadDIaDOStatus = new System.Threading.Timer(new System.Threading.TimerCallback(timerSetDOsAndReadDIaDOStatusCallBack));
            _DOs2Set = Enumerable.Repeat(false, 6).ToArray(); //adam6060 6x
            timerSetDOsAndReadDIaDOStatusOn();
        }

        #endregion

        #region Start / Stop / Connect / InitializeAdam / Stav pripojeni
        private void InitializeP2P()
        {
            if (adamP2P == null)
            {
                adamP2P = new Advantech.Adam.AdamP2P(1000, 1000);
            }
        }

        private void InitializeTCP()
        {
            //TCP pro dotazovani            
            adamTCP = new Advantech.Adam.AdamSocket();
            adamTCP.SetTimeout(this.timeouttcp, this.timeouttcp, this.timeouttcp); // set timeout for TCP      
            adamTCP.AdamSeriesType = Advantech.Adam.AdamType.Adam6000;
        }

        private void Connect()
        {
            // pripojeni k ADAM
            if (adamTCP == null && communication_TCP)
            {
                InitializeTCP();
            }

            if (adamTCP != null)
            {
                if (!adamTCP.Connected)
                {
                    adamTCP.Disconnect();

                    if (!adamTCP.Connect(this.IP, System.Net.Sockets.ProtocolType.Tcp, this.adamPort))
                    {
                        //Exceptions.Handler.ErrorHandle("Connect to " + ipAddress + " failed", "adamTCP", false);
                    }
                } 
            }

            if (adamP2P == null && communication_P2P)
            {
                InitializeP2P();
            }
        }
        public void Start()
        {

            try
            {
                Connect();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }

            try
            {
                //if (adamTCP.Connected && this.minLevelWidth_Enabled)
                if (adamTCP !=null && communication_TCP)
                {
                    if (adamTCP.Connected && this.minLevelWidth > 0)
                    {
                        var digitalInput = adamTCP.DigitalInput();
                        long[] o_lHigh = new long[] { };
                        long[] o_lLow = new long[] { };
                        if (digitalInput.GetDigitalFilterMiniSignalWidth(out o_lHigh, out o_lLow))
                        {
                            //o_lHigh.ToList().ForEach(x => x = this.minLevelWidth);
                            //o_lLow.ToList().ForEach(x => x = this.minLevelWidth);
                            for (int i = 0; i < o_lHigh.Length; i++)
                            {
                                o_lHigh[i] = this.minLevelWidth;
                            }
                            for (int i = 0; i < o_lLow.Length; i++)
                            {
                                o_lLow[i] = this.minLevelWidth;
                            }

                            if (!digitalInput.SetDigitalFilterMiniSignalWidth(o_lHigh, o_lLow))
                            {
                                // Exceptions.Handler.ErrorHandle("Nepodařilo se nastavit digitální filtry", "adamTCP", true);
                            }
                        }
                        else
                        { // pokud se nepodari zjistit aktualni stav, tak to necham jak to je ... 
                          // TODO : zalogovat do errorlogu
                          // Exceptions.Handler.ErrorHandle("Nepodařilo se načíst digitální filtry", "adamTCP", true);
                        }
                    }

                    timerSetDOsAndReadDIaDOStatusOn();

                    // po spusteni vynuluji citace
                    ClearCounters();
                }

            }
            catch (Exception exTCP)
            {
                //Exceptions.Handler.ErrorHandle(exTCP.Message, "adamTCP", true);
                // Exceptions.Handler.ErrorHandle(adamTCP.LastError.ToString(), "adamTCP");
                ExceptionHandler2.Handle(exTCP);
            }

            //spusteni P2P
            try
            {
                if (adamP2P != null && communication_P2P)
                {
                    adamP2P.GetDataEvent -= new Advantech.Adam.GetP2PDataCallback(adamP2P_GetDataEvent);
                    adamP2P.GetDataEvent += new Advantech.Adam.GetP2PDataCallback(adamP2P_GetDataEvent);

                    if (adamP2P.Start_P2P_Server(this.PORT_P2P)) // pouzit ADAM knihovnu
                    {
                        adamP2P_started = true;
                    }
                    else
                    {
                        // Exceptions.Handler.ErrorHandle("Nepodařilo se spustit P2P server sledování stavů", "adamP2P", true);
                    } 
                }
            }
            catch (Exception exP2P)
            {
                // Exceptions.Handler.ErrorHandle("Nepodařilo se spustit sledování stavů !!!\n" + exP2P.Message, "adamP2P", true);
                ExceptionHandler2.Handle(exP2P);
            }

      

        }
        public void Stop()
        {
            if (adamP2P != null)
            {
                if (adamP2P.Stop_P2P_Server())
                {
                    adamP2P_started = false;
                }
                else
                {
                   // Exceptions.Handler.ErrorHandle("Nepodařilo se zastavit P2P server sledování stavů", "adamP2P", true);
                }
                adamP2P.GetDataEvent -= new Advantech.Adam.GetP2PDataCallback(adamP2P_GetDataEvent);
            }

            timerSetDOsAndReadDIaDOStatusOff();

            if ((adamTCP != null) && (adamTCP.Connected))
                adamTCP.Disconnect();

        }

        public bool AdamTCP_Connected
        {
            get
            {
                if ((adamTCP != null) && (adamTCP.Connected))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AdamP2P_Started
        {
            get
            {
                return adamP2P_started;
            }
        }

        #endregion

        #region Zpracovani P2P komunikace
        private byte[] cidlaStatePrevious;
        private byte[] cidlaStateActual;


        void adamP2P_GetDataEvent(Advantech.Adam.P2P_Config config)
        {
#if DEBUG
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
#endif
            try
            {
#if DEBUG
                sw.Start();
#endif
                List<bool> tmp_bool = new List<bool>();

                //Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> P2P event",this.ipAddress));

                cidlaStateActual = config.Data;

                if (cidlaStatePrevious == null)
                {
                    cidlaStatePrevious = new byte[cidlaStateActual.Length];
                }

                // zjisteni se provede, pouze pokud jsou delky poli stejne... => jinak problem a chyba
                if (cidlaStateActual.Length == cidlaStatePrevious.Length)
                {
                    //Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> P2P cyklus", this.ipAddress));
                    for (int i = 0; i < cidlaStateActual.Length; i++) // prochazeni bytu..
                    {
                        for (int j = 0; j < 8; j++)//prochazi primo bity v bytech
                        {
                            bool stateActual = (((cidlaStateActual[i] >> j) & 0x01) == 0x01); //jak zjistit co je pri mo na danem bitu???
                            bool statePrevious = (((cidlaStatePrevious[i] >> j) & 0x01) == 0x01);
                            tmp_bool.Add(stateActual);

                            if (!statePrevious && stateActual) // pouze zmena z 0 => 1
                            {
                               // Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> P2P nabezna >> {1}", this.ipAddress, i * 8 + j));
                                OnDataReady_NabeznaHrana(new AdamEventHandlerArgs(i * 8 + j, _DIStatusLast,IP)); // cislo cidla, ktere se zmenilo ...
                            }
                            else// pouze zmena z 1 => 0
                            {
                                // Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> P2P sestupna >> {1}", this.ipAddress, i * 8 + j));
                                OnDataReady_SestupnaHrana(new AdamEventHandlerArgs(i * 8 + j, _DIStatusLast,IP));
                            }
                        }
                    }
                    actualState_DI = tmp_bool.ToArray();

                }
                else
                {
                   //Exceptions.Handler.ErrorHandle("Délky polí stavů čidel jsou různé. Nelze provést porovnání změny stavu", "adamP2P", false);
                }

                // nakonec do prev dat actual
                cidlaStatePrevious = cidlaStateActual;

            }
            finally
            {
#if DEBUG
                sw.Stop();
                //System.Diagnostics.Debug.WriteLine("sw: " + sw.Elapsed.ToString() + ", adam_packageNum: " + config.PackageNum + ", adam_data: " + Support.Utils.Dump(config.Data.ToList(), null), "adamP2P_GetDataEvent");
#endif
            }
        }
        #endregion

        #region Modbus komunikace

        private void SetSingleCoil(int i_iCoilIndex, bool b_On)
        {

            // 11.4.2019 JiS
            // provede se nastaveni hodnoty do bufferu pro aktivaci
            // aktivace probiha v casovem intervalu zadanem konfiguraci ...
            try
            {
                _DOs2Set[i_iCoilIndex] = b_On;
                lock (this._DOsSet_SynchronizedObject)
                {
                    this._DOsSet = true;
                    timerSetDOsAndReadDIaDOStatusOnImmediate();
                }
            }
            catch (Exception ex)
            {
                //  Exceptions.Handler.ErrorHandle(ex.Message, "ADAM60XX: SetSingleCoil", false);
                ExceptionHandler2.Handle(ex);
            }
        }

        public void ClearCounters()
        {
            try
            {
                Connect();

                bool[] resetCounters = Enumerable.Repeat(true, 6 * 4).ToArray();

                if (!adamTCP.Modbus().ForceMultiCoils(33, resetCounters))
                {
                  //  Exceptions.Handler.ErrorHandle("ADAM60XX: ForceMultiCoils(33, resetCounters): doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                }
            }
            catch (Exception exClearCounters)
            {
                ExceptionHandler2.Handle(exClearCounters);
                // Exceptions.Handler.ErrorHandle(exClearCounters.Message + "\n" + exClearCounters.StackTrace, "Adam: ClearCounters", false);
            }
        }

        #endregion

        #region Zjistovani stavu dotazem pres TCP/Modbus

        private bool[] _DIStatusLast = new bool[] { };
        public bool[] DIStatusLast
        {
            get { return this._DIStatusLast; }
        }

       // private bool[] _DIStatusLast_H = new bool[] { };

        private bool[] _DI_H = new bool[] { };

        /// <summary>
        /// Vraci pole datovych poli vstupnich/vystupnich? cidel
        /// </summary>
        /// <returns></returns>
        public bool[] getDIStatus()
        {
            bool[] bDiData;
            bool[] cidlaData = new bool[iDiTotal];

            Connect();

            if (adamTCP.Modbus().ReadInputStatus(iDiStart, iDiTotal, out bDiData))
            {
                cidlaData = new bool[bDiData.Length];
                Array.Copy(bDiData, 0, cidlaData, 0, iDiTotal);
            }
            else
            {
              //  Exceptions.Handler.ErrorHandle("Nepodařilo se načíst stavy Vstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
            }

            _DIStatusLast = new bool[cidlaData.Length];
            Array.Copy(cidlaData, _DIStatusLast, cidlaData.Length);
            return cidlaData;
        }

        private bool[] _DOStatusLast = new bool[] { };
        public bool[] DOStatusLast
        {
            get { return this._DOStatusLast; }
        }

        /// <summary>
        /// Vraci pole datovych poli stavu releovych/DO vystupu?
        /// </summary>
        /// <returns></returns>
        public bool[] getDOStatus()
        {
            bool[] bDoData;
            bool[] releData = new bool[iDoTotal];

            //if (AgroConfig.config.Agro[0].DebugLoging)
            //    Log.WriteAdamCSV(1, 0, 0);

            Connect();

            if (adamTCP.Modbus().ReadCoilStatus(iDoStart, iDoTotal, out bDoData))
            {
                releData = new bool[bDoData.Length];
                Array.Copy(bDoData, 0, releData, 0, iDoTotal);
            }
            else
            {
              //  Exceptions.Handler.ErrorHandle("Nepodařilo se načíst stavy Výstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
            }

            _DOStatusLast = new bool[releData.Length];
            Array.Copy(releData, _DOStatusLast, releData.Length);
            return releData;
        }

        private int[] _DIValuesLast = new int[] { };
        public int[] DIValuesLast
        {
            get { return _DIValuesLast; }
        }
        /// <summary>
        /// Vraci hodnoty na vstupu cidel
        /// </summary>
        /// <returns></returns>
        private int[] getDIValues()
        {
            // adam6052 = 8di/8do
            // adam6060 = 6di/6do (6relays)

            int iStart = 1;
            int iTotal = 6 * 2;

            int[] bDiValues;
            int[] iValues = new int[iTotal / 2];

            //if (AgroConfig.config.Agro[0].DebugLoging)
            //    Log.WriteAdamCSV(1, 0, 0);

            Connect();

            if (!adamTCP.Modbus().ReadHoldingRegs(iStart, iTotal, out bDiValues))
            {
               // Exceptions.Handler.ErrorHandle("Nepodařilo se načíst stavy Výstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                return iValues;
            }

            for (int i = 0; i < bDiValues.Length / 2; i++)
            {
                //iValues[i] = (((int)bDiValues[i+1]) << 8 + (int)bDiValues[i]);
                iValues[i] = bDiValues[2 * i];
            }
            _DIValuesLast = new int[iValues.Length];
            Array.Copy(iValues, _DIValuesLast, iValues.Length);
            return iValues;
        }

        #endregion

        #region Nastavovani DO vystupnich hodnot a cteni stavu DO a DI v casovem intervalu

        /// <summary>
        /// Objekt urceny pro synchronizaci pristupu k promenne _DOsSet, ktera urcuje, zda se nastaveni ma provest ... 
        /// </summary>
        private object _DOsSet_SynchronizedObject = new object();
        /// <summary>
        /// Priznak, zda ma timer nastavit priznak pro ulozeni hodnot
        /// </summary>
        private bool _DOsSet = false;
        private bool[] _DOs2Set;
        private System.Threading.Timer timerSetDOsAndReadDIaDOStatus = null;


        private void timerSetDOsAndReadDIaDOStatusOnImmediate()
        {
            if (timerSetDOsAndReadDIaDOStatus != null)
                timerSetDOsAndReadDIaDOStatus.Change(10, System.Threading.Timeout.Infinite);
        }

        private void timerSetDOsAndReadDIaDOStatusOn()
        {
            if (timerSetDOsAndReadDIaDOStatus != null)
                timerSetDOsAndReadDIaDOStatus.Change(1000, System.Threading.Timeout.Infinite);
        }

        private void timerSetDOsAndReadDIaDOStatusOff()
        {
            if (timerSetDOsAndReadDIaDOStatus != null)
                timerSetDOsAndReadDIaDOStatus.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void timerSetDOsAndReadDIaDOStatusDispose()
        {
            if (timerSetDOsAndReadDIaDOStatus != null)
            {
                timerSetDOsAndReadDIaDOStatusOff();
                timerSetDOsAndReadDIaDOStatus.Dispose();
                timerSetDOsAndReadDIaDOStatus = null;
            }
        }

        /// <summary>
        /// Priznak, zda bezi provadeni / callback
        /// </summary>
        private bool timerSetDOsAndReadDIaDOStatusCallBackIsRunning = false;
        private object timerSetDOsAndReadDIaDOStatusCallBackIsRunningSynchronizationObject = new object();
        /// <summary>
        /// Provede nastaveni vystupnich cidel HW zarizeni
        /// </summary>
        /// <param name="state"></param>
        private void timerSetDOsAndReadDIaDOStatusCallBack(object state)
        {
            System.Threading.Thread.CurrentThread.Name = "timerSetDOsAndReadDIaDOStatus " + DateTime.Now.ToString();

            lock (this.timerSetDOsAndReadDIaDOStatusCallBackIsRunningSynchronizationObject)
            {
                if (this.timerSetDOsAndReadDIaDOStatusCallBackIsRunning)
                    return;
            }

            // lokalni promenna, ktera rika, jestli se ma nastavit
            // a v pripade vyjimky, pak zda znovu nastavit na znovu provedeni ulozeni v dalsim taktu ... 
            bool lDOsSet = false;
            try
            {
                lock (this.timerSetDOsAndReadDIaDOStatusCallBackIsRunningSynchronizationObject)
                {
                    this.timerSetDOsAndReadDIaDOStatusCallBackIsRunning = true;
                }
                timerSetDOsAndReadDIaDOStatusOff();

                #region Zapsani stavu pozadovanych vystupu
                if ((_DOs2Set != null) && (_DOsSet))
                {
                    lock (this._DOsSet_SynchronizedObject)
                    {
                        lDOsSet = _DOsSet;
                        _DOsSet = false;
                    }

                    Connect();

                    // pro adam6060 6di
                    if (!adamTCP.Modbus().ForceMultiCoils(17, _DOs2Set))
                    {
                      //  Exceptions.Handler.ErrorHandle("ADAM60XX: timerSetDOsAndReadDIaDOStatusCallBack: ForceMultiCoils(17, _DOs2Set): doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                    }
                    else
                    {
                        lDOsSet = false;
                    }
                }
                #endregion



                if (communication_TCP)
                {

                    // nacteni stavu Vstupu
                    this.getDIStatus();

                    // nacteni stavu Vystupu
                    this.getDOStatus();

                    // nacteni stavu Vstupnich hodnot
                    this.getDIValues();

                    if (!communication_P2P)
                    {
                        int senzor;
                        int state_Hrana = Zmena_Stavu_ModBus(out senzor);
                        if (state_Hrana == 1)
                        {
                            OnDataReady_NabeznaHrana(new AdamEventHandlerArgs(senzor, _DIStatusLast, IP));
                        }
                        else if (state_Hrana == 2)
                        {
                            OnDataReady_SestupnaHrana(new AdamEventHandlerArgs(senzor, _DIStatusLast, IP));
                        } 
                    }
                }

            }
            catch (Exception ex)
            {
                // Exceptions.Handler.ErrorHandle(ex.Message, "ADAM60XX: timerSetDOsAndReadDIaDOStatusCallBack", false);
                ExceptionHandler2.Handle(ex);
            }
            finally
            {
                if (lDOsSet) // melo se neco nastavit, ale nepovedlo se to, tak to dam znovu k nastaveni
                {
                    lock (this._DOsSet_SynchronizedObject)
                    {
                        _DOsSet = true;
                    }
                }

                lock (this.timerSetDOsAndReadDIaDOStatusCallBackIsRunningSynchronizationObject)
                {
                    this.timerSetDOsAndReadDIaDOStatusCallBackIsRunning = false;
                }
                timerSetDOsAndReadDIaDOStatusOn();
            }
        }

        #endregion

        #region Porovnani stavu

        public int Zmena_Stavu_ModBus(out int senzor)
        {
            senzor = 0;
            try
            {

                if (_DIStatusLast == null)
                    return 0; //neni zmena

                if (_DI_H.Length == 0)
                    return 0;
                
                if (_DI_H.Length == _DIStatusLast.Length)
                {

                    if (_DIStatusLast.Length > 0 && _DI_H.Length > 0) //mohla byt zmena
                    {
                        for (int i = 0; i < _DIStatusLast.Length; i++)
                        {
                            if (_DIStatusLast[i] != _DI_H[i])
                            {
                                if (_DIStatusLast[i] && !_DI_H[i])
                                {
                                    //Nabezna
                                    senzor = i;
                                    return 1;
                                }
                                else if (!_DIStatusLast[i] && _DI_H[i])
                                {
                                    //Sestupna
                                    senzor = i;
                                    return 2;
                                }
                                else
                                {
                                    ;
                                }
                            }
                        }

                        //if (errorLst != errorAct)
                        //    zmena = true;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }

              return 0;
            }
            catch (Exception ex)
            {
                // Log.Write(string.Format("CATCH--Zmena_Stavu_ModBus: {0} ", ex));
                ExceptionHandler2.Handle(ex);
                return 0;
            }
            finally
            {
                _DI_H = _DIStatusLast;
            }

        }


        #endregion

        #region ulozeni stavu do databaze
#if false

        public bool SaveStates()
        {
            // TODO : ulozit stav stroju, pokud se lisi od posledniho ... 
            //1. doslo ke zmene stavu?
            if (state_AuxFlagsAct == null)
                return false; //neni zmena

            bool zmena = false;
            if (state_AuxFlagsLst != null) //mohla byt zmena
            {
                // overeni zda se neco zmenilo ... 
                for (int i = 0; i < state_AuxFlagsAct.Length; i++)
                {
                    if ((state_AuxFlagsLst.Length >= (i + 1))
                        && state_AuxFlagsLst[i] != state_AuxFlagsAct[i])
                    {
                        zmena = true;
                        break; //doslo ke zmene, jinak pokracuji dal
                    }
                }
                if (errorLst != errorAct)
                    zmena = true;
            }
            else
            {
                zmena = true; //neni predchozi stav, tak to musi byt prvni po spusteni sluzby => ulozit
            }

            if (!zmena)
                return false; //nedoslo k ulozeni ... 

            //2. zjistit zda existuje nebo ne
            //VyrobaSSTableAdapters.MachineStateSetTableAdapter madapter = new VyrobaSSTableAdapters.MachineStateSetTableAdapter();
            SledovaniVyroby.Module.Vyroba_SV.SQL.AdamDataSet mdataset = new SledovaniVyroby.Module.Vyroba_SV.SQL.AdamDataSet();
            SledovaniVyroby.Module.Vyroba_SV.SQL.AdamDatabase.FillByIP(mdataset.MachineStateSet, ipAddress);

            SledovaniVyroby.Module.Vyroba_SV.SQL.AdamDataSet.MachineStateSetRow mrow = null;
            if (mdataset.MachineStateSet.Count > 0)
            {
                mrow = mdataset.MachineStateSet[0];
            }
            else
            {
                mrow = mdataset.MachineStateSet.NewMachineStateSetRow();
                mrow.IP = ipAddress;
                mrow.DateModified = DateTime.Now;
                mdataset.MachineStateSet.AddMachineStateSetRow(mrow);
            }

            //3. naplnit stav
            mrow.DateModified = DateTime.Now;
            mrow.S1 = Convert.ToInt32(state_AuxFlagsAct[0]);
            mrow.S2 = Convert.ToInt32(state_AuxFlagsAct[1]);
            mrow.S3 = Convert.ToInt32(state_AuxFlagsAct[2]);
            mrow.S4 = Convert.ToInt32(state_AuxFlagsAct[3]);
            mrow.S5 = Convert.ToInt32(state_AuxFlagsAct[4]);
            mrow.S6 = Convert.ToInt32(state_AuxFlagsAct[5]);
            mrow.S7 = Convert.ToInt32(state_AuxFlagsAct[6]);
            mrow.S8 = Convert.ToInt32(state_AuxFlagsAct[7]);
            mrow.S9 = Convert.ToInt32(state_AuxFlagsAct[8]);
            mrow.S10 = Convert.ToInt32(state_AuxFlagsAct[9]);
            mrow.S11 = Convert.ToInt32(state_AuxFlagsAct[10]);
            mrow.S12 = Convert.ToInt32(state_AuxFlagsAct[11]);

            errorLst = errorAct;        // TODO: Změna i při naběhnutí spojení s Adamem
            mrow.LastError = errorAct;

            //4.aktualizovat
            SQL.AdamDatabase.Update(mrow);

            state_AuxFlagsLst = state_AuxFlagsAct;

            return true;

        }

        public bool SaveStates2()
        {
            // TODO : ulozit stav stroju, pokud se lisi od posledniho ... 
            //1. doslo ke zmene stavu?
            if (DIStatusLast == null)
                return false; //neni zmena

            //bool zmena = false;
            //if (DIStatusLast.Length > 0 && _DIStatusLast_H.Length > 0) //mohla byt zmena
            //{
            //    // overeni zda se neco zmenilo ... 
            //    for (int i = 0; i < DIStatusLast.Length; i++)
            //    {
            //        if (DIStatusLast[i] != _DIStatusLast_H[i])
            //        {
            //            zmena = true;
            //            break; //doslo ke zmene, jinak pokracuji dal
            //        }
            //    }

            //    if (errorLst != errorAct)
            //        zmena = true;
            //}
            //else
            //{
            //    zmena = true; //neni predchozi stav, tak to musi byt prvni po spusteni sluzby => ulozit
            //}

            //if (!zmena)
            //    return false; //nedoslo k ulozeni ... 

            //2. zjistit zda existuje nebo ne
            //VyrobaSSTableAdapters.MachineStateSetTableAdapter madapter = new VyrobaSSTableAdapters.MachineStateSetTableAdapter();
            SQL.AdamDataSet mdataset = new SQL.AdamDataSet();
            //zapis do databaze 
            SQL.AdamDatabase.FillByIP(mdataset.MachineStateSet, ipAddress);

            SQL.AdamDataSet.MachineStateSetRow mrow = null;
            if (mdataset.MachineStateSet.Count > 0)
            {
                mrow = mdataset.MachineStateSet[0];
            }
            else
            {
                mrow = mdataset.MachineStateSet.NewMachineStateSetRow();
                mrow.IP = ipAddress;
                mrow.DateModified = DateTime.Now;
                mdataset.MachineStateSet.AddMachineStateSetRow(mrow);
            }

            //3. naplnit stav
            mrow.DateModified = DateTime.Now;
            mrow.S1 = Convert.ToInt32(DIStatusLast[0]);
            mrow.S2 = Convert.ToInt32(DIStatusLast[1]);
            mrow.S3 = Convert.ToInt32(DIStatusLast[2]);
            mrow.S4 = Convert.ToInt32(DIStatusLast[3]);
            mrow.S5 = Convert.ToInt32(DIStatusLast[4]);
            mrow.S6 = Convert.ToInt32(DIStatusLast[5]);
            mrow.S7 = Convert.ToInt32(DIStatusLast[6]);
            mrow.S8 = Convert.ToInt32(DIStatusLast[7]);
#if true
            mrow.S9 = Convert.ToInt32(DIStatusLast[8]);
            mrow.S10 = Convert.ToInt32(DIStatusLast[9]);
            mrow.S11 = Convert.ToInt32(DIStatusLast[10]);
            mrow.S12 = Convert.ToInt32(DIStatusLast[11]);
#endif

            errorLst = errorAct;        // TODO: Změna i při naběhnutí spojení s Adamem
            mrow.LastError = errorAct;

            //4.aktualizovat
            SQL.AdamDatabase.Update(mrow);

            //state_AuxFlagsLst = state_AuxFlagsAct;
            //_DIStatusLast_H = DIStatusLast;

            return true;

        }

        public bool SaveStates3()
        {
            try
            {
                // API komunikace
                MachineStateSet o = new MachineStateSet();
                IRestResponse restResponse;
                string param = "SledovaniVyroby_dataADAM_zapis";
                string JSON = "";


                //// TODO : ulozit stav stroju, pokud se lisi od posledniho ... 
                ////1. doslo ke zmene stavu?
                //if (DIStatusLast == null)
                //    return false; //neni zmena

                //bool zmena = false;
                //if (DIStatusLast.Length > 0 && _DIStatusLast_H.Length > 0) //mohla byt zmena
                //{
                //    // overeni zda se neco zmenilo ... 
                //    for (int i = 0; i < DIStatusLast.Length; i++)
                //    {
                //        if (DIStatusLast[i] != _DIStatusLast_H[i])
                //        {
                //            zmena = true;
                //            break; //doslo ke zmene, jinak pokracuji dal
                //        }
                //    }

                //    if (errorLst != errorAct)
                //        zmena = true;
                //}
                //else
                //{
                //    zmena = true; //neni predchozi stav, tak to musi byt prvni po spusteni sluzby => ulozit
                //}

                //if (!zmena)
                //    return false; //nedoslo k ulozeni ... 

                //3. naplnit stav

                errorLst = errorAct;        // TODO: Změna i při naběhnutí spojení s Adamem

                //4.aktualizovat
                o.IP = ipAddress;
                o.DateModified = DateTime.Now;
                o.LastError = errorAct;
                o.S1 = Convert.ToInt32(DIStatusLast[0]);
                o.S2 = Convert.ToInt32(DIStatusLast[1]);
                o.S3 = Convert.ToInt32(DIStatusLast[2]);
                o.S4 = Convert.ToInt32(DIStatusLast[3]);
                o.S5 = Convert.ToInt32(DIStatusLast[4]);
                o.S6 = Convert.ToInt32(DIStatusLast[5]);
                o.S7 = Convert.ToInt32(DIStatusLast[6]);
                o.S8 = Convert.ToInt32(DIStatusLast[7]);
#if true
                o.S9 = Convert.ToInt32(DIStatusLast[8]);
                o.S10 = Convert.ToInt32(DIStatusLast[9]);
                o.S11 = Convert.ToInt32(DIStatusLast[10]);
                o.S12 = Convert.ToInt32(DIStatusLast[11]);
#endif


                //5.zapis do DB pres IIS
                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

                if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "nakladka"))
                {
                    throw new Exception("Komunikace s IIS AGRO se nezdařila");
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    //return "OK";
                }
                else
                {
                    throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
                }

                //_DIStatusLast_H = DIStatusLast;

                return true;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return false;
            }


        }

        private bool tmpTEST = false;

        public void SaveStates4(ADAM.ADAM_60XX.AdamEventHandlerArgs e)
        {
            try
            {
                //Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> Save to DB", this.ipAddress));

                //this.getDIStatus();

                // API komunikace
                MachineStateSet o = new MachineStateSet();
                IRestResponse restResponse;
                string param = "SledovaniVyroby_dataADAM_zapis";
                string JSON = "";

                //if (tmpTEST)
                //{
                //    // TODO : ulozit stav stroju, pokud se lisi od posledniho ... 
                //    //1. doslo ke zmene stavu?
                //    if (e.DI == null)
                //        return; //neni zmena

                //    if (_DI_H.Length != 0 && _DI_H.Length == e.DI.Length)
                //    {
                //        bool zmena = false;
                //        if (e.DI.Length > 0 && e.DI.Length > 0) //mohla byt zmena
                //        {
                //            // overeni zda se neco zmenilo ... 
                //            for (int i = 0; i < e.DI.Length; i++)
                //            {
                //                if (e.DI[i] != _DI_H[i])
                //                {
                //                    zmena = true;
                //                    break; //doslo ke zmene, jinak pokracuji dal
                //                }
                //            }

                //            if (errorLst != errorAct)
                //                zmena = true;
                //        }
                //        else
                //        {
                //            zmena = true; //neni predchozi stav, tak to musi byt prvni po spusteni sluzby => ulozit
                //        }

                //        if (!zmena)
                //            return; //nedoslo k ulozeni ... 
                //    } 
                //}



                //3. naplnit stav

                errorLst = errorAct;        // TODO: Změna i při naběhnutí spojení s Adamem

                //4.aktualizovat
                o.IP = ipAddress;
                o.DateModified = DateTime.Now;
                o.LastError = errorAct;
                o.S1 = Convert.ToInt32(e.DI[0]);
                o.S2 = Convert.ToInt32(e.DI[1]);
                o.S3 = Convert.ToInt32(e.DI[2]);
                o.S4 = Convert.ToInt32(e.DI[3]);
                o.S5 = Convert.ToInt32(e.DI[4]);
                o.S6 = Convert.ToInt32(e.DI[5]);
                o.S7 = Convert.ToInt32(e.DI[6]);
                o.S8 = Convert.ToInt32(e.DI[7]);
#if true
                o.S9 = Convert.ToInt32(e.DI[8]);
                o.S10 = Convert.ToInt32(e.DI[9]);
                o.S11 = Convert.ToInt32(e.DI[10]);
                o.S12 = Convert.ToInt32(e.DI[11]);
#endif


                //5.zapis do DB pres IIS
                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

                if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "nakladka"))
                {
                    throw new Exception("Komunikace s IIS AGRO se nezdařila");
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    //return "OK";
                }
                else
                {
                    throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
                }

                //_DI_H = e.DI;

                return;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return;
            }


        }

#endif

        #endregion

        public bool PingHost(string nameOrAddress)
        {
            bool pingable = false;

            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }

        public override bool ReadStates()
        {
            try
            {
                    bool result = adamTCP.Configuration().GetGCL_AuxFlagStatus(out state_AuxFlags);
                    if (result)
                    {
                        state_AuxFlagsLst = state_AuxFlagsAct;
                        state_AuxFlagsAct = state_AuxFlags;
                    }
                    else throw new Exception(IP + " GetGCL_AuxFlagStatus failed");             
                
                return result;

            }
            catch (Exception ex)
            {
                // Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if (adamTCP != null)      // načtení typu chyby
                    errorAct = adamTCP.LastError.ToString();
           
            }
        }

        /// <summary>
        /// Zapne vystupni rele houkacky
        /// </summary>
        public void zapniHoukacku()
        {
            SetSingleCoil(this.cisloReleHoukacka, true);
        }

        /// <summary>
        /// Vypne vystupni rele houkacky
        /// </summary>
        public void vypniHoukacku()
        {
            SetSingleCoil(this.cisloReleHoukacka, false);
        }

    }
}

