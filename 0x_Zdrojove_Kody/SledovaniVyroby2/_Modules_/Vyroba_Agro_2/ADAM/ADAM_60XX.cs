using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.ADAM
{
    public class ADAM_60XX
    {
        #region Udalosti zmeny stavu cidel
        public delegate void AdamEventHandler(AdamEventHandlerArgs e);
        public event AdamEventHandler DataReady;
        private void OnDataReady(AdamEventHandlerArgs e)
        {
            if (DataReady != null)
            {
                DataReady(e);
            }
            else
            {
                ;
            }
        }
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
        #endregion

        #region Nastaveni typu ADAMa
        // TODO : Inicializace adama na ruzne typy ???
        // parametry vstupu/vystupu adama
        private int iDiStart = 1; 
        private int iDiTotal = 6;
        private int iDoStart = 17; 
        private int iDoTotal = 6;
        //private int iChTotal = 12;

        #endregion

        #region Interni promenne
        private string ipAddress;
        private int adamPort = 502;// modbus TCP port is 502
        private int adamPortP2P = 1025; // defaultni UDP port, na kterem posloucha lokalni UDP p2p sluzba adama => musi byt stejne nakonfigurovano na ADAMovi
        private int timerPeriod;
        private int timeouttcp;

        //public bool minLevelWidth_Enabled = false;
        private long minLevelWidth;

        private int cisloReleLinka;
        private int cisloReleHoukacka;
        private int cisloRelePaletizator;
        private int cisloReleServis;

        //private int cisloSensorPotvrzovaci = 1;
        //private int cisloSensorPalety = 2;
        private int cisloSensorProhaz = 4;
        private bool sensorProhazState = false;

        //private static Advantech.Adam.AdamInformation adamInformation;
        private static Advantech.Adam.AdamSocket adamTCP;
        private static bool adamP2P_started = false;
        private static Advantech.Adam.AdamP2P adamP2P;

        //// *** Stavy *** //
        //private bool _LinkaActualStav = false;

        #endregion

        #region Konstruktor
        /// <summary>
        /// Konstuktor zarizeni ADAM
        /// </summary>
        /// <param name="ipAddress">IP Adresa zarizeni</param>
        /// <param name="ackDI">Index potvrzovaciho cidla DI</param>
        /// <param name="timerPeriod">obnovovaci cas v ms pro timer</param>
        /// <param name="timeouttcp">timeout komunikace TCP/MODBUS</param>
        /// <param name="cisloReleLinka">Index rele pro ovladani chodu linky</param>
        /// <param name="cisloReleHoukacka">Index rele pro ovladani houkacky</param>
        /// <param name="minLevelWidth">Minimalni delka pulsu, ktera je prohlasena jako 0/1 pro nastaveni do HW Adam</param>
        /// <param name="cisloSensorProhaz">Index cidla prohazovani/aktivity linky</param>
        public ADAM_60XX(
            string ipAddress, 
            int portP2P,
            //int cisloSensorPotvrzovaci, 
            //int cisloSensorPalety,
            int timerPeriod, 
            int timeouttcp, 
            int cisloReleLinka, 
            int cisloReleHoukacka,
            int cisloRelePaletizator,
            int cisloReleServis,
            long minLevelWidth, 
            int cisloSensorProhaz
            )
        {
            this.ipAddress = ipAddress;
            this.adamPortP2P = portP2P;
            this.minLevelWidth = minLevelWidth * 10; // protoze adam to uvadi v hodnotach 0.1ms

            this.cisloReleLinka = cisloReleLinka;
            this.cisloReleHoukacka = cisloReleHoukacka;
            this.cisloRelePaletizator = cisloRelePaletizator;
            //this.cisloSensorPotvrzovaci = cisloSensorPotvrzovaci;
            //this.cisloSensorPalety = cisloSensorPalety;
            this.cisloSensorProhaz = cisloSensorProhaz;

            this.cisloReleServis = cisloReleServis;

            this.timeouttcp = timeouttcp;
            this.timerPeriod = timerPeriod;

            // inicializace modulu
            InitializeTCP();

            // Peer 2 Peer
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
            if (adamTCP == null)
            {
                InitializeTCP();
            }

            if (!adamTCP.Connected)
            {
                adamTCP.Disconnect();

                if (!adamTCP.Connect(this.ipAddress, System.Net.Sockets.ProtocolType.Tcp, this.adamPort))
                {
                   // Exceptions.Handler.ErrorHandle("Connect to " + ipAddress + " failed", "adamTCP", false);
                    ExceptionHandler2.Handle("Connect to " + ipAddress + " failed", "adamTCP", false);
                }
            }

            if (adamP2P == null)
            {
                InitializeP2P();
            }
        }
        public void Start()
        {
            try
            {
                Connect();

                //if (adamTCP.Connected && this.minLevelWidth_Enabled)
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
                            //Exceptions.Handler.ErrorHandle("Nepodařilo se nastavit digitální filtry", "adamTCP", true);
                            ExceptionHandler2.Handle("Nepodařilo se nastavit digitální filtry", "adamTCP", true);
                        }
                    }
                    else
                    { // pokud se nepodari zjistit aktualni stav, tak to necham jak to je ... 
                        // TODO : zalogovat do errorlogu
                        //Exceptions.Handler.ErrorHandle("Nepodařilo se načíst digitální filtry", "adamTCP", true);
                        ExceptionHandler2.Handle("Nepodařilo se načíst digitální filtry", "adamTCP", true);
                    }
                }

            }
            catch (Exception exTCP)
            {
                //Exceptions.Handler.ErrorHandle(exTCP.Message, "adamTCP", true);
                ExceptionHandler2.Handle(exTCP.Message, "adamTCP", true);
                //Exceptions.Handler.ErrorHandle(adamTCP.LastError.ToString(), "adamTCP");
                ExceptionHandler2.Handle(adamTCP.LastError.ToString(), "adamTCP", false);
            }          	

            // spusteni P2P 
            try
            {
                adamP2P.GetDataEvent -= new Advantech.Adam.GetP2PDataCallback(adamP2P_GetDataEvent);
                adamP2P.GetDataEvent += new Advantech.Adam.GetP2PDataCallback(adamP2P_GetDataEvent);

                if (adamP2P.Start_P2P_Server(adamPortP2P))
                {
                    adamP2P_started = true;
                }
                else
                {
                   // Exceptions.Handler.ErrorHandle("Nepodařilo se spustit P2P server sledování stavů", "adamP2P", true);
                    ExceptionHandler2.Handle("Nepodařilo se spustit P2P server sledování stavů", "adamP2P", true);
                }
            }
            catch (Exception exP2P)
            {
               // Exceptions.Handler.ErrorHandle("Nepodařilo se spustit sledování stavů !!!\n" + exP2P.Message, "adamP2P", true);
                ExceptionHandler2.Handle("Nepodařilo se spustit sledování stavů !!!\n" + exP2P.Message, "adamP2P", true);
            }

            timerSetDOsAndReadDIaDOStatusOn();

            // po spusteni vynuluji citace
            ClearCounters();

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
                    ExceptionHandler2.Handle("Nepodařilo se zastavit P2P server sledování stavů", "adamP2P", true);
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
        //private ulong _getdataevent_count = 0;
        //private ulong _getdataevent_021_count = 0;
        //private System.Collections.Queue adamP2P_EventQueue = new System.Collections.Queue();

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

                cidlaStateActual = config.Data;

                //adamP2P_EventQueue.Enqueue(cidlaStateActual);
                //_getdataevent_count++;

                if (cidlaStatePrevious == null)
                {
                    //cidlaStatePrevious = cidlaStateActual;
                    cidlaStatePrevious = new byte[cidlaStateActual.Length];
                }

                // zjisteni se provede, pouze pokud jsou delky poli stejne... => jinak problem a chyba
                if (cidlaStateActual.Length == cidlaStatePrevious.Length)
                {
                    for (int i = 0; i < cidlaStateActual.Length; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            bool stateActual = (((cidlaStateActual[i] >> j) & 0x01) == 0x01);
                            bool statePrevious = (((cidlaStatePrevious[i] >> j) & 0x01) == 0x01);
                            if (!statePrevious && stateActual) // pouze zmena z 0 => 1
                            {
                                //_getdataevent_021_count++;
                                //DataReady(new AdamEventHandlerArgs(i * 8 + j)); // cislo cidla, ktere se zmenilo ...
                                OnDataReady(new AdamEventHandlerArgs(i * 8 + j)); // cislo cidla, ktere se zmenilo ...
                            }
                        }
                    }
                }
                else
                {
                    //Exceptions.Handler.ErrorHandle("Délky polí stavů čidel jsou různé. Nelze provést porovnání změny stavu", "adamP2P", false);
                    ExceptionHandler2.Handle("Délky polí stavů čidel jsou různé. Nelze provést porovnání změny stavu", "adamP2P", false);
                }

                try
                {
                    //1 - linka aktivni
                    //0 - prohaz
                    this.SetProhazSensorState(config.Data);
                }
                catch (Exception ex)
                { // pokud nastane vyjimka, tak zaloguji ... 
                    //Exceptions.Handler.ErrorHandle("Chyba ve výpočtu sensoru stavu prohazování: " + ex.Message, "adamP2P", false);
                    ExceptionHandler2.Handle("Chyba ve výpočtu sensoru stavu prohazování: " + ex.Message, "adamP2P", false);
                }

                // nakonec do prev dat actual
                cidlaStatePrevious = cidlaStateActual;

            }
            finally
            {
#if DEBUG
                sw.Stop();
                System.Diagnostics.Debug.WriteLine("sw: " + sw.Elapsed.ToString() + ", adam_packageNum: " + config.PackageNum + ", adam_data: " + Support.Utils.Dump(config.Data.ToList(), null), "adamP2P_GetDataEvent");
#endif
            }
        }
        #endregion

        #region Modbus komunikace

        private void SetSingleCoil(int i_iCoilIndex, bool b_On)
        {
            //Connect();

            //// zacina se na indexu 17 pro 1.DO
            //if (!adamTCP.Modbus().ForceSingleCoil(17 + i_iCoilIndex, b_On))
            //{
            //    Exceptions.Handler.ErrorHandle("ForceSingleCoil: on/off:" + b_On + " iCoilIndex:" + i_iCoilIndex + " doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);

            //    // if (AgroConfig.config.Agro[0].DebugLoging)
            //    //      Log.WriteAdamCSV(0, 2, 0);
            //}

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
                //Exceptions.Handler.ErrorHandle(ex.Message, "ADAM60XX: SetSingleCoil", false);
                ExceptionHandler2.Handle(ex.Message, "ADAM60XX: SetSingleCoil", false);
            }
        }

        public void ClearCounters()
        {
            try
            {
                Connect();

                // pro adam6060 6di
                //bool[] resetCounters = new bool[18];
                bool[] resetCounters = Enumerable.Repeat(true, 6 * 4).ToArray();
                //bool[] resetCounters = Enumerable.Repeat(true, 1).ToArray();

                if (!adamTCP.Modbus().ForceMultiCoils(33, resetCounters))
                {
                    //Exceptions.Handler.ErrorHandle("ADAM60XX: ForceMultiCoils(33, resetCounters): doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                    ExceptionHandler2.Handle("ADAM60XX: ForceMultiCoils(33, resetCounters): doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                }
            }
            catch (Exception exClearCounters)
            {
                //Exceptions.Handler.ErrorHandle(exClearCounters.Message + "\n" + exClearCounters.StackTrace, "Adam: ClearCounters", false);
                ExceptionHandler2.Handle(exClearCounters.Message + "\n" + exClearCounters.StackTrace, "Adam: ClearCounters", false);
            }
        }

        #endregion

        #region Zjistovani stavu dotazem pres TCP/Modbus

        private bool[] _DIStatusLast = new bool[] { };
        public bool[] DIStatusLast
        {
            get { return this._DIStatusLast; }
        }
        /// <summary>
        /// Vraci pole datovych poli vstupnich/vystupnich? cidel
        /// </summary>
        /// <returns></returns>
        private bool[] getDIStatus()
        {
            bool[] bDiData;
            bool[] cidlaData = new bool[iDiTotal];

            //if (AgroConfig.config.Agro[0].DebugLoging)
            //    Log.WriteAdamCSV(1, 0, 0);

            Connect();

            if (adamTCP.Modbus().ReadInputStatus(iDiStart, iDiTotal, out bDiData))
            {
                cidlaData = new bool[bDiData.Length];
                Array.Copy(bDiData, 0, cidlaData, 0, iDiTotal);

                this.SetProhazSensorState(cidlaData);
            }
            else
            {
                //Exceptions.Handler.ErrorHandle("Nepodařilo se načíst stavy Vstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                ExceptionHandler2.Handle("Nepodařilo se načíst stavy Vstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
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
                //Exceptions.Handler.ErrorHandle("Nepodařilo se načíst stavy Výstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                ExceptionHandler2.Handle("Nepodařilo se načíst stavy Výstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
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
                //Exceptions.Handler.ErrorHandle("Nepodařilo se načíst stavy Výstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                ExceptionHandler2.Handle("Nepodařilo se načíst stavy Výstupů z ADAMa. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
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

        /// <summary>
        /// Vraci stav cidla detekujici stav "prohazovani"
        /// </summary>
        /// <remarks>Prohazovani znamena, ze balicka je zastavena, ale pas k nakladacce(otocny mechanizmus pytlu skladani na paletu) je aktivni</remarks>
        /// <returns></returns>
        public bool GetProhazSensorState()
        {
            return this.sensorProhazState;
        }

        private void SetProhazSensorState(bool[] data)
        {
            this.sensorProhazState = !data[this.cisloSensorProhaz];
        }

        private void SetProhazSensorState(byte[] data)
        {
            this.sensorProhazState = !(((data[0] >> this.cisloSensorProhaz) & 0x01) == 0x01); //!bData[prohazSensorID];
        }

        /// <summary>
        /// Aktivuje / Deaktivuje releLinky dle aktualniho a pozadovaneho stavu linky
        /// </summary>
        /// <param name="stav"></param>
        /// <returns></returns>
        public bool rizeniLinky(bool on)
        {
            SetSingleCoil(this.cisloReleLinka, on);
            return on;
        }

        /// <summary>
        /// Aktivuje / Deaktivuje releLinky dle aktualniho a pozadovaneho stavu linky
        /// </summary>
        /// <param name="stav"></param>
        /// <returns></returns>
        public bool rizeniPaletizatoru(bool on)
        {
            if (cisloRelePaletizator >= 0)
            {
                SetSingleCoil(this.cisloRelePaletizator, on); 
            }
            return on;
        }

        /// <summary>
        /// Aktivuje / Deaktivuje releLinky dle aktualniho a pozadovaneho stavu linky
        /// </summary>
        /// <param name="stav"></param>
        /// <returns></returns>
        public bool rizeniServis(bool on)
        {
            SetSingleCoil(this.cisloReleServis, on);
            return on;
        }

        //public bool rizeniLinky(DataVyroba.LinkaStav stav)
        //{
        //    if (stav == DataVyroba.LinkaStav.LINKA_ON && (_LinkaActualStav == false))
        //    {
        //        //ErrorLog.Log.Write("zapnilinku");
        //        SetSingleCoil(this.cisloReleLinka, true);
        //        _LinkaActualStav = true;
        //    }
        //    else if (stav == DataVyroba.LinkaStav.LINKA_OFF && (_LinkaActualStav == true))
        //    {
        //        //ErrorLog.Log.Write("vypniLinku");
        //        SetSingleCoil(this.cisloReleLinka, false);
        //        _LinkaActualStav = false;
        //    }

        //    return _LinkaActualStav;

        //}

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
						//Exceptions.Handler.ErrorHandle("ADAM60XX: timerSetDOsAndReadDIaDOStatusCallBack: ForceMultiCoils(17, _DOs2Set): doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                        ExceptionHandler2.Handle("ADAM60XX: timerSetDOsAndReadDIaDOStatusCallBack: ForceMultiCoils(17, _DOs2Set): doesn't succeeded. LastError: " + adamTCP.LastError.ToString(), "adamTCP", false);
                    }
					else
					{
						lDOsSet = false;
					}
				}
                #endregion

                // nacteni stavu Vstupu
                this.getDIStatus();

                // nacteni stavu Vystupu
                this.getDOStatus();

                // nacteni stavu Vstupnich hodnot
                this.getDIValues();

            }
            catch (Exception ex)
            {
               // Exceptions.Handler.ErrorHandle(ex.Message, "ADAM60XX: timerSetDOsAndReadDIaDOStatusCallBack", false);
                ExceptionHandler2.Handle(ex.Message, "ADAM60XX: timerSetDOsAndReadDIaDOStatusCallBack", false);
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
    }
}
