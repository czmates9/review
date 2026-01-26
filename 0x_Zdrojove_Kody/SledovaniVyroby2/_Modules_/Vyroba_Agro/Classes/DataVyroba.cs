using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using FASK.SledovaniVyroby.IScannerProvider;
using FASK.SledovaniVyroby.Module.Vyroba_Agro;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.DataSets;
using FASK.SledovaniVyroby.ErrorLog;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class DataVyroba
    {

        // TODO : pracovnici, sarze ...
        #region Pracovnici
        public List<string> _pracovnici = new List<string>();
        public void PracovnikSet(string pracovnik)
        {
            if (!_pracovnici.Contains(pracovnik))
                _pracovnici.Add(pracovnik);
        }

        public void PracovniciClear()
        {
            this._pracovnici.Clear();
        }

        public List<string> PracovniciGet()
        {
            return this._pracovnici;
        }
        #endregion

        #region Sarze
        //vyroba.SetSarze();
        public string _sarze = string.Empty;
        public void SarzeClear()
        {
            this._sarze = string.Empty;
        }
        public string SarzeSet(string sarze)
        {
            this._sarze = sarze;
            return this._sarze;
        }

        /// <summary>
        /// Pouze Nacte sarzi z centralni databaze ale nenastavi
        /// </summary>
        /// <returns></returns>
        public string SarzeGenerate()
        {
            return DatabaseCentral.ReturnSarze(this._smenaID, _pracovnici.First(), Logging.LogConfig.MachineID.Trim());
        }

        /// <summary>
        /// Pouze Nacte sarzi ID z centralni databaze ale nenastavi
        /// </summary>
        /// <returns></returns>
        public bool ReturnID(string inID)
        {
            return DatabaseCentral.ReturnID(inID);
        }

        /// <summary>
        /// Pouze Nacte sarzi HESLO z centralni databaze ale nenastavi
        /// </summary>
        /// <returns></returns>
        public bool ReturnHeslo(string inHESLO,string inID)
        {
            return DatabaseCentral.ReturnHeslo(inHESLO,inID);
        }



        /// <summary>
        /// Nacte sarzi z centralni databaze a nastavi automaticky
        /// </summary>
        /// <returns></returns>
        public string SarzeGenerateAndSet()
        {
            this._sarze = SarzeGenerate();
            return this._sarze;
        }
        public string Sarze
        {
            get { return this._sarze; }
        }
        #endregion

        public string GetStateInternal()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("_barcodeActual=" + this._barcodeActual);
                sb.AppendLine("_codeNoReadCnt=" + this._codeNoReadCnt);
                sb.AppendLine("_codeReadCnt=" + this._codeReadCnt);
                sb.AppendLine("_linkaStateBeforeEvent=" + this._linkaStateBeforeEvent);
                sb.AppendLine("_list_NoRead_Count=" + this._list_NoRead_Count);
                sb.AppendLine("_pocetPruchoduProhaz=" + this._pocetPruchoduProhaz);
                sb.AppendLine("_prohazZapnut=" + this._prohazZapnut);
                sb.AppendLine("_prohazZnak=" + this._prohazZnak);
                sb.AppendLine("_scannerCodeNoReadCount=" + this._scannerCodeNoReadCount);
                sb.AppendLine("_scannerCodeReadCount=" + this._scannerCodeReadCount);
                sb.AppendLine("_smenaID=" + this._smenaID);
                sb.AppendLine("_stavHoukacky=" + this._stavHoukacky);
                sb.AppendLine("_stavVyroba=" + this._stavVyroba.ToString());
                sb.AppendLine("_stavVyrobaPredchozi=" + this._stavVyrobaPredchozi.ToString());
                sb.AppendLine("_zahaleniLinky=" + this._zahaleniLinky);
                sb.AppendLine("ackSensor=" + this.ackSensor);
                sb.AppendLine("actualPytel=" + this.actualPytel);
                sb.AppendLine("sensorsCount=" + this.sensorsCount);
                sb.AppendLine("cidla.Count=" + this.cidla.Count);
                sb.AppendLine("scanner.SensorsCnt=" + this.scanner.SensorsCnt);
                sb.AppendLine("scanner.SensorStateActual=" + this.scanner.SensorStateActual);
                sb.AppendLine("this.scanner.SensorStatePrevious=" + this.scanner.SensorStatePrevious);
                for (int i = 0; i < this.cidla.Count; i++)
                {
                    sb.AppendLine("[" + i + "].SensorsCnt=" + this.cidla[i].SensorsCnt);
                    sb.AppendLine("[" + i + "].SensorStateActual=" + this.cidla[i].SensorStateActual);
                    sb.AppendLine("[" + i + "].SensorStatePrevious=" + this.cidla[i].SensorStatePrevious);
                }
                sb.AppendLine("codeNew=" + this.codeNew);
                sb.AppendLine("codeReadResultActual=" + this.codeReadResultActual);
                sb.AppendLine("codeReadResultPrevious=" + this.codeReadResultPrevious);
                sb.AppendLine("dtDebugWriteLast=" + this.dtDebugWriteLast.ToString());
                sb.AppendLine("dtIdleLast=" + this.dtIdleLast.ToString());
                sb.AppendLine("dtNoReadLast=" + this.dtNoReadLast.ToString());
                sb.AppendLine("dtPytelReadOld=" + this.dtPytelReadOld.ToString());
                sb.AppendLine("dtReadLast=" + this.dtReadLast.ToString());
                sb.AppendLine("dtSaveLast=" + this.dtSaveLast.ToString());
                sb.AppendLine("error_id_code=" + this.error_id_code);
                sb.AppendLine("hlidaniHoukacka=" + this.hlidaniHoukacka);
                sb.AppendLine("MaxPocetNepruchoduHoukacka=" + this.MaxPocetNepruchoduHoukacka);
                sb.AppendLine("PocetNepruchoduHoukacka=" + this.PocetNepruchoduHoukacka);
                sb.AppendLine("Pracovnici:");
                for (int i = 0; i < this._pracovnici.Count; i++)
                {
                    sb.AppendLine(String.Format("{0}: id={1}", i, this._pracovnici[i]));
                }
                return sb.ToString();

            }
            catch (Exception ex)
            {
                FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                return ex.Message;
            }
        }

        //public FASK.SledovaniVyroby.IScannerProvider.IScannerProvider Scanner = null;

        public enum HoukackaStav { OFF = 0, ON, PRUCHOD, NEPRUCHOD, RESET, SET_MAX, STAV, VYPNOUT_HLIDANI, ZAPNOUT_HLIDANI };

        public enum LinkaStav { LINKA_ON = 0, LINKA_OFF, LINKA_STAV };
        public enum VyrobaStavy { 
            Main = 0, 
            Event11, 
            Event36, 
            LogIDSmena, 
            LogIDPracovnik, 
            ZadatEAN, 
            Odhlaseni, 
            ZmenaProhaz, 
            UlozeniEAN, 
            Vyrobek, 
            VlozKod, 
            VlozPocet, 
            VlozPocetEAN,
            SarzeHeslo,
            SarzeHodnota,
            SarzeID
        };

        private Timer timer;
        public Code codeReadResultActual;
        public Code codeReadResultPrevious;
        private Adam60XX adam;

        private DateTime dtReadLast = DateTime.Now;
        private DateTime dtNoReadLast = DateTime.Now;
        private DateTime dtIdleLast = DateTime.Now;
        public DateTime dtSaveLast = DateTime.Now;

        private DataSets.Pytel pytle = new Vyroba_Agro.DataSets.Pytel();
        public Data.FASK_EventsRow aktualDataRow = null;

        public int _pocetPruchoduProhaz = 0;
        private int ackSensor = 0; //cislo potvrzovaciho cidla
        private int sensorsCount = 0;
        private string codeNew = string.Empty;

        /*
         * id = 1 ... zmen do prohaz, prekrocena nastavena hodnota pytlu, ktere mohou projit bez prohazu, pokud je cidlo
         *              na prohaz aktivni
         * id = 2 ... Více nepřečtených kódů než je limit! nacteno vice noread kodu nez je nastaveno
         * id = 3 ... Linka zahálí! 
         * id = 4 ... Scan bez dat!\n" + Udalosti.event36(Udalosti.Event36Stav.COUNT, 0) + "/" + Udalosti.event36(Udalosti.Event36Stav.GET_TIME, 0);
         * id = 5 ... "Více nepotvrzených! Čidlo nepotvrdilo ČK.\n" + Udalosti.event11(Udalosti.Event11Stav.COUNT, 0) + "/" + Udalosti.event11(Udalosti.Event11Stav.GET_TIME, 0);
         *        
         */
        public int error_id_code = 0;

        /// <summary>
        /// Pocita pocet NoRead ze scanneru dokud neni zapsano do lokalniho prehledu odvodu
        /// </summary>
        /// <remarks>Vzdy po InsertDataToDataset se musi vynulovat</remarks>
        public decimal _list_NoRead_Count = 0;

        private DataSets.Pytel.PytelRow actualPytel = null;
        public DataSets.Pytel.PytelRow ActualPytel
        {
            get
            {
                return actualPytel;
            }
        }

        //id prihlaseneho uzivatele (smeny)...
        private string _smenaID = AgroConfig.config.Agro[0].OdvodMimoSmenuID.ToString();

        public event StavZmenaEventHandler ZmenaStavu;
        public event WarningEventHandler VarovaniZmena;
        public event ProhazEventHandler ProhazZmena;

        public delegate void WarningEventHandler(string hlaska);
        public delegate void ProhazEventHandler(string hlaska, string znak);
        public delegate void StavZmenaEventHandler(VyrobaStavy newStav);

        private VyrobaStavy _stavVyrobaPredchozi = VyrobaStavy.Main;
        public VyrobaStavy StavVyrobaPredchozi
        {
            get
            {
                return _stavVyrobaPredchozi;
            }
            set
            {
                _stavVyrobaPredchozi = value;
            }
        }

        private VyrobaStavy _stavVyroba = VyrobaStavy.Main;
        public VyrobaStavy StavVyroba
        {
            get
            {
                return _stavVyroba;
            }
            set
            {
                _stavVyrobaPredchozi = _stavVyroba; //ulozime si predchozi stav...

                _stavVyroba = value;
                ZmenaStavu(_stavVyroba);
            }
        }

        private Data vyrobaDataHistoryInMemory = null;

        private bool _linkaStateBeforeEvent = false;
        public bool LinkaStateBeforeEvent
        {
            get
            {
                return _linkaStateBeforeEvent;
            }
            set
            {
                _linkaStateBeforeEvent = value;
            }
        }

        //pocet celkovych nactenych carovych kodu...
        private int _scannerCodeReadCount = 0;
        private int _scannerCodeNoReadCount = 0;

        //aktualni (potvrzeny) car. kod
        private string _barcodeActual;
        public string BarcodeActual
        {
            get
            {
                return _barcodeActual;
            }
        }

        #region CodeReadCnt Synchronization
        // pocet nactenych a potvrzenych kodu
        private decimal _codeReadCnt;
        public decimal CodeReadCnt
        {
            get
            {
                return _codeReadCnt;
            }
            //menit hodnotu muze jen tento objekt ...
            //private 
            set
            {
                _codeReadCnt = value;
            }
        }
        public decimal CodeReadCntGetAndReset()
        {
            decimal codereadcnt = _codeReadCnt;
            _codeReadCnt = 0;
            return codereadcnt;
        }
        #endregion

        #region CodeNoReadCnt Synchronization
        //pocet nenactenych a potvrzenych kodu
        private decimal _codeNoReadCnt;
        public decimal CodeNoReadCnt
        {
            get
            {
                return _codeNoReadCnt;
            }
            // menit muze jen tento objekt
            //private 
            set
            {
                _codeNoReadCnt = value;
            }
        }
        public decimal CodeNoReadCntGetAndReset()
        {
            decimal codenoreadcnt = _codeNoReadCnt;
            _codeNoReadCnt = 0;
            return codenoreadcnt;
        }
        #endregion

        private bool _zahaleniLinky = false; //pokud je nastaveno na true, tak linka zahali


        private string _prohazZnak = "";
        public string ProhazZnak
        {
            get
            {
                return _prohazZnak;
            }
            set
            {
                _prohazZnak = value;
                if (ProhazZmena != null)
                    ProhazZmena(_prohazZapnut ? "Zapnuto" : "Vypnuto", _prohazZnak);
            }
        }

        private bool _prohazZapnut = false;
        public bool ProhazZapnut
        {
            get
            {
                return _prohazZapnut;
            }
            set
            {
                _prohazZapnut = value;
                if (ProhazZmena != null)
                    ProhazZmena(_prohazZapnut ? "Zapnuto" : "Vypnuto", _prohazZnak);
            }
        }

        public List<Cidlo> cidla;
        public Cidlo scanner;

        public DataVyroba(
            int sensors_count
            , int ack_sensor
//            , FASK.SledovaniVyroby.IScannerProvider.IScannerProvider sc
            )
        {
            VynulovaniPromennych();

            //this.Scanner = sc;

            this.vyrobaDataHistoryInMemory = new Data();

            this.dtIdleLast = DateTime.Now;
            //this._historyListNoReadCount = 0;

            this.cidla = new List<Cidlo>();

            this.ackSensor = ack_sensor;
            this.sensorsCount = sensors_count;

            for (int i = 0; i < sensors_count; i++)
            {
                cidla.Add(new Cidlo());
            }

            this.scanner = new Cidlo();


            LoadPytle();

            timer = new System.Threading.Timer(TimerCallback, null, System.Threading.Timeout.Infinite, 1000);
            timer.Change(0, 1000);
        }

        public void setAdam(Adam60XX ad)
        {
            this.adam = ad;

            if (adam.GetProhazSensorState())
                ProhazZnak = "!";
            else
                ProhazZnak = "";

        }

        private void OffTimer()
        {
            if (timer != null)
                timer.Change(System.Threading.Timeout.Infinite, 1000);
        }

        private void OnTimer()
        {
            if (timer != null)
                timer.Change(1000, 1000);
        }

        private void LoadPytle()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                string filename = AgroConfig.config.Agro[0].PathToPytleConfigFile;//@"C:\FASK\SVN\Nadop\SledovaniVyroby\!Build!\Vyroba\Debug\Pytle.xml";
                xmldoc.Load(filename);

                string nodeValue = string.Empty;

                XmlElement configNode = xmldoc.SelectSingleNode("/Pytle/pytel") as XmlElement;

                while (configNode != null)
                {
                    pytle._Pytel.AddPytelRow(configNode.Attributes["nazev"].Value, int.Parse(configNode.Attributes["minDelay"].Value), int.Parse(configNode.Attributes["maxDelay"].Value));

                    configNode = (XmlElement)configNode.NextSibling;
                }

            }
            catch (Exception exc)
            {
                Log.WriteException(exc);
            }
        }

        private DateTime dtDebugWriteLast = DateTime.Now;

        private void TimerCallback(object state)
        {

            OffTimer();

            TestZahaleniLinky();

            TestUdalost11();

            TestUdalost36();

            TestProhaz();

            TestAutoUlozeniDat();

            OnTimer();
        }

        private void TestAutoUlozeniDat()
        {
           TimeSpan rozdil = DateTime.Now - dtSaveLast;
            //int sekund = ((cas - dtSaveLast).Minutes * 60) + (cas - dtSaveLast).Seconds;
           double sekund = rozdil.TotalSeconds;

            if (sekund > AgroConfig.config.Agro[0].IntervalUkladaniDat)
            {
                if ((CodeReadCnt + CodeNoReadCnt > 0) && (_barcodeActual == string.Empty))
                {
                    if (_stavVyroba != VyrobaStavy.UlozeniEAN)
                    {
                        SetLinkaState(false);
                        rizeniHoukacky(HoukackaStav.ON);
                        StavVyroba = VyrobaStavy.UlozeniEAN;
                    }
                }
                else
                {
                    if (_stavVyroba != VyrobaStavy.UlozeniEAN)
                    {
                        // TODO : logovat ulozeni do aplikace
                        // + pocet pred ulozenim
                        // + pocet po ulozeni
                        // => muze byt problem v synchronizaci vlaken a prepisu hodnot poctu ... ???
                        Database.InsertNewEvents(_smenaID, CodeReadCntGetAndReset(), CodeNoReadCntGetAndReset(), "automaticke ulozeni", _barcodeActual, "", "", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);
                        //CodeReadCnt = 0;
                        //CodeNoReadCnt = 0;

                        dtSaveLast = DateTime.Now;
                    }
                }
            }
        }

        private void TestProhaz()
        {
            if (_pocetPruchoduProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
            {
                bool cidlo = adam.GetProhazSensorState();
                if (!cidlo)
                {
                    rizeniHoukacky(HoukackaStav.OFF);
                    //ProhazZapnut = false;
                    ProhazZnak = "";
                    _pocetPruchoduProhaz = 0;
                    VarovaniZmena("-");

                    error_id_code = 0;
                }
                else
                {
                    ProhazZnak = "!";
                    rizeniHoukacky(HoukackaStav.ON);		//zapne houkacku
                    error_id_code = 1;
                    VarovaniZmena("Změň do prohaz!");
                    return;
                }
            }

        }

        private void TestUdalost36()
        {
            if (Udalosti.event36(Udalosti.Event36Stav.STATE, 0) > 0)
            {
                if (_stavVyroba != VyrobaStavy.Event36)
                {
                    _linkaStateBeforeEvent = adam.rizeniLinky(LinkaStav.LINKA_STAV); //Zapamatuje si stav linky

                    adam.rizeniLinky(LinkaStav.LINKA_OFF);			//vypne linku
                    rizeniHoukacky(HoukackaStav.ON);		//zapne houkacku

                    Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_EVENT36_OCURED, "","");

                    StavVyroba = VyrobaStavy.Event36;

                }
            }
        }

        void TestUdalost11()
        {
            //zmena stavu menu - nastaly udalosti 11 xkrat za ycasu
            if (Udalosti.event11(Udalosti.Event11Stav.STATE, 0) > 0)
            {
                if (_stavVyroba != VyrobaStavy.Event11)
                {
                    //nastavuje se pouze pokud jiz neni ve stavu 11
                    _linkaStateBeforeEvent = adam.rizeniLinky(LinkaStav.LINKA_STAV); //Zapamatuje si stav linky

                    adam.rizeniLinky(LinkaStav.LINKA_OFF);			//vypne linku
                    rizeniHoukacky(HoukackaStav.ON);		//zapne houkacku

                    //printf("Vice nepotvrz.:\n%d/%d E-konec", event11(EVENT11_COUNT), event11(EVENT11_GET_TIME));
                    Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_EVENT11_OCURED,"","");

                    StavVyroba = VyrobaStavy.Event11;
                }
            }
        }


        public string GetSmenaId()
        {
            return this._smenaID;
        }

        public int GetPocetPruchodProhaz()
        {
            return this._pocetPruchoduProhaz;
        }

        public void SetActualBarcode(string code)
        {
            this._barcodeActual = code;
        }

        public void SetStavVyroba(VyrobaStavy novyStav)
        {
            this.StavVyroba = novyStav;
        }

        public VyrobaStavy GetStavVyroba()
        {
            return this.StavVyroba;
        }

        public void SetSmenaId(string idsmena)
        {
            if (CodeReadCnt + CodeNoReadCnt > 0)
            {   //pokud neco v bufferu bylo, pak zapisa
                Database.InsertNewEvents(_smenaID, CodeReadCntGetAndReset(), CodeNoReadCntGetAndReset(), "mimo smenu", _barcodeActual, "", "zadna", "", "", "", "", "", "", "", _sarze);
                //CodeReadCnt = 0;
                //CodeNoReadCnt = 0;
                dtSaveLast = DateTime.Now;
            }

            this._smenaID = idsmena;

            VynulovaniPromennych();

            // po prihlaseni smeny je prohaz vyply, ale protoze je dotaz na EAN, je stopla i linka
            adam.rizeniLinky(LinkaStav.LINKA_OFF);
            ProhazZapnut = false;
        }

        private void VynulovaniPromennych()
        {

            this._barcodeActual = string.Empty;
            this.CodeReadCnt = this.CodeNoReadCnt = 0;
            this.codeReadResultActual = this.codeReadResultPrevious = Code.NoData;
            this._pocetPruchoduProhaz = 0;
            this.codeNew = string.Empty;

            if (adam != null)
            {
                if (adam.GetProhazSensorState())
                    ProhazZnak = "!";
                else
                    ProhazZnak = "";
            }

            dtIdleLast = dtNoReadLast = dtPytelReadOld = dtReadLast = DateTime.Now;

            ProhazZapnut = false;

        }

        public Data GetDataVyrobaHistory()
        {
            return vyrobaDataHistoryInMemory;
        }

        /// <summary>
        /// potvrzovaci cidlo
        /// </summary>
        public void SensorAck(bool activated)
        {
            //cidla[ackSensor].SensorsCnt++; // JiS: <toto se deje v nadrazene funkci, az po provedeni pruchodu odvodem

            algoritmusOdvodVyrobva(string.Empty, Code.NoData, activated);

            algoritmusOdvodVyrobva(string.Empty, Code.NoData, false);
        }

        private void CancelSensorsAndCodeStatus()
        {
            cidla[ackSensor].setSensorStatePreviousDeactive();
            codeReadResultPrevious = Code.NoData;
            dtIdleLast = DateTime.Now;
        }

        private void HlidaniProhaz()
        {
            bool cidlo = adam.GetProhazSensorState();
            if (cidlo && !_prohazZapnut)
            {
                ProhazZnak = "!";
                _pocetPruchoduProhaz++;
            }

            if (!cidlo)
            {
                ProhazZnak = "";
                _pocetPruchoduProhaz = 0;
            }


        }

        DateTime dtPytelReadOld = DateTime.MinValue;

        private void ZjisteniNazvuPytle()
        {
            DateTime dtPytelReadNew = DateTime.Now;
            if (cidla[ackSensor].SensorStatePrevious == true) //melo by to byt OK...
            {
                TimeSpan delay_current = dtPytelReadNew - dtPytelReadOld;  //casovy rozdil mezi poslednim a novym sepnutim

                for (int i = 0; i < pytle._Pytel.Count; i++)
                {
                    if (((delay_current.TotalMilliseconds) >= pytle._Pytel[i].MinDelay) && ((delay_current.TotalMilliseconds) <= pytle._Pytel[i].MaxDelay))
                    {
                        actualPytel = pytle._Pytel[i];
                        break;
                    }
                }
            }

            dtPytelReadOld = dtPytelReadNew;
        }

        private void algoritmusOdvodVyrobva(string code, Code readResult, bool sensorActivated)
        {
            string CodeNewTemp = code;

            codeReadResultActual = readResult;

            if (sensorActivated)
                cidla[ackSensor].setSensorStateActualActive();
            else
                cidla[ackSensor].setSensorStateActualDeactive();


            /*************************** ODVOD VYROBY MIMO SMENU *****************************/

            try
            {
                if (StavVyroba == VyrobaStavy.LogIDSmena || StavVyroba == VyrobaStavy.LogIDPracovnik) /*_smenaID == AgroConfig.IDUserNotLogged*/
                {   //uzivatel se jeste neprihlasil, odvod mimo smenu...
                    if (codeReadResultActual != Code.NoData)
                    {
                        if (_barcodeActual != CodeNewTemp)
                        {
                            if (CodeReadCnt > 0)
                            {
                                Database.InsertNewEvents(_smenaID, CodeReadCntGetAndReset(), CodeNoReadCntGetAndReset(), "mimo smenu", _barcodeActual, CodeNewTemp, "zadna", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);
                                //CodeNoReadCnt = CodeReadCnt = 0;
                                dtSaveLast = DateTime.Now;
                            }
                            _barcodeActual = CodeNewTemp;
                        }
                        CodeReadCnt++;
                    }

                    if (cidla[ackSensor].SensorStateActual)
                        CodeNoReadCnt++;

                    return;
                }


                /*************************** ODVOD VYROBY MIMO SMENU *****************************/


                if (cidla[ackSensor].SensorStateActual == true)
                    ZjisteniNazvuPytle(); //zkratka pytle

                /***************************Logika hlidani odvodu - Zacatek**********************/
                /***********Osetreni stavu z predchoziho pruchodu - Zacatek**************/
                if (cidla[ackSensor].SensorStatePrevious == true)
                {   //Predchozi stav: cidlo zaznamenalo pruchod pytle
                    if (codeReadResultPrevious == Code.NoData)
                    {
                        //kod bez zaznamu
                        //ceka na prichod dat ke zparovani
                        //pokud cidlo zaznamena opet pruchod, pak se jedna o FALESNY_PRUCHOD(u11)
                    }
                    else if (codeReadResultPrevious == Code.Read)
                    {   //kod nacten a potvrzen cidlem => odvod + 1 nebo novy kod
                        if (_barcodeActual == string.Empty) //Kod nebyl dosud inicializovan
                        {
                            _barcodeActual = codeNew;
                            CodeReadCnt++;

                            aktualDataRow = InsertDataToDataset(_smenaID, "", 1, _list_NoRead_Count, "sensor", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);
                            _list_NoRead_Count = 0;
                            //Database.InsertNewEvents(_smenaID, DateTime.Now, 1, CodeNoReadCnt, "sensor", _barcodeActual, barcodeNew, "", "", "", DateTime.Now, "", "", "", "", "", "");
                        }
                        else
                        { //Kod jiz byl inicializovan
                            if (_barcodeActual == codeNew)
                            {//kody jsou stejne, tak zvysit counter odvedenych
                                CodeReadCnt++;

                                aktualDataRow = InsertDataToDataset(_smenaID, "", 1, 0, "sensor", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);
                                //Database.InsertNewEvents(_smenaID, DateTime.Now, 1, 0, "sensor", _barcodeActual, barcodeNew, "", "", "", DateTime.Now, "", "", "", "", "", "");
                            }
                            else
                            {//nejsou stejne => kod byl inicializovan, zmena vyrobku

                                Database.InsertNewEvents(_smenaID, CodeReadCntGetAndReset(), CodeNoReadCntGetAndReset(), "prisel novy kod, stary ulozeni", _barcodeActual, codeNew, "zadna", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);
                                this.CodeReadCnt++; // o jedna zvysit X ne prirazovat 1 !!!
                                //this.CodeNoReadCnt = 0;
                                dtSaveLast = DateTime.Now; //posledni ulozeni

                                aktualDataRow = InsertDataToDataset(_smenaID, "", 1, 0, "novy kod, nejsou stejne", codeNew, _barcodeActual, "", "", "", "", "", "", "", "", _sarze);

                                _barcodeActual = codeNew;

                                rizeniHoukacky(HoukackaStav.ZAPNOUT_HLIDANI);
                            }
                        }

                        HlidaniProhaz();

                        //Zrusit priznaky stavu cidla odvodu a nacteneho kodu
                        CancelSensorsAndCodeStatus();
                        dtIdleLast = DateTime.Now;
                    }
                    else if (codeReadResultPrevious == Code.NoRead)
                    {   //prisel NoRead kod, cidlo potvrzuje => pruchod pytle + 1

                        //Zvysit citac noreadcnt, kod zustava dal stejny jako puvodni (inicializovany nebo neicializovany)
                        CodeNoReadCnt++;

                        //Pokud je aktualni kod jiz nacten, tak zapisu hodnoty
                        // +1 noread
                        if (!String.IsNullOrEmpty(_barcodeActual))
                            aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 1, "sensor", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);
                        else
                            _list_NoRead_Count++;

                        HlidaniProhaz();

                        //Zrusit priznaky stavu cidla odvodu a nacteneho kodu
                        CancelSensorsAndCodeStatus();

                        dtIdleLast = DateTime.Now;
                    }
                    else
                    {
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_SPATNE_CTENI, "", "");
                    }
                }
                else if (cidla[ackSensor].SensorStatePrevious == false)
                {   //Predchozi stav: cidlo bez pruchodu

                    if (codeReadResultPrevious == Code.NoData)
                    {   //kod bez zaznamu
                        //nic se nedeje...
                    }
                    else if (codeReadResultPrevious == Code.Read)
                    {
                        // cidlo nema pruchod, kod nacten ze scanneru
                        // ceka se na prichod signalu z cidla
                        // pokud prijde dalsi Code_Read, tak se jedna o FALESNE_CTENI(u12)
                    }
                    else if (codeReadResultPrevious == Code.NoRead)
                    {// cidlo nema pruchod, kod prisel NoRead
                        // pytel	 : ceka na pruchod cidlem, pak se jedna o CodeNoReadCnt+1
                        // necistota : prijde dalsi NoRead
                        // zablik    : prijde dalsi NoRead nebo Read

                        // jak rozpoznat necistotu od zabliku?
                        // => zablik by mel byt osetren na urovni timeoutu scanneru pro NoRead
                        //    TODO : definovat odpovidajici timeout pro zabliky na scanneru
                        // => necistota : je vzdy typu NoRead
                        // ?? TODO : co v pripade, ze je pred pytlem necistota a ck. se nenacte korektne pred pruchodem potvrzovaciho cidla
                    }
                    else
                    {
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_SPATNE_CTENI, "", "");
                        codeReadResultPrevious = Code.NoData;
                    }
                }
                /***********Osetreni stavu z predchoziho pruchodu - Konec****************/


                /***********Osetreni stavu cidel z aktualniho pruchodu - Zacatek***************/
                if (cidla[ackSensor].SensorStateActual == true)
                {
                    VypnoutZahaleni();

                    if (cidla[ackSensor].SensorStatePrevious == true) //cidlo melo drive pruchod
                    {
                        //udalost falesny pruchod //LOG udalost 36
                        //Zvysit citac noreadcnt, kod zustava dal stejny jako puvodni (inicializovany nebo neicializovany)
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_PHOTO_NOT_READ, "", "");

                        Udalosti.event36(Udalosti.Event36Stav.ADD, 0);
                    }

                    cidla[ackSensor].setSensorStatePreviousActive();
                }

                /***********Osetreni stavu cidel z aktualniho pruchodu - Konec***************/

                /***********Osetreni stavu scanneru z aktualniho pruchodu - Zacatek***************/
                if (codeReadResultActual == Code.NoData)
                { //nic se nedeje, pokracuje se dal...

                }
                else if (codeReadResultActual == Code.Read)
                {// nacten kod
                    #region Nacten Kod
                    if (_stavVyroba == VyrobaStavy.UlozeniEAN)
                    {//CEKA SE NA VLOZENI EANU PRI UKLADANI !
                        rizeniHoukacky(HoukackaStav.OFF);

                        _barcodeActual = CodeNewTemp;

                        _list_NoRead_Count = CodeNoReadCnt;

                        if (_stavVyrobaPredchozi == VyrobaStavy.Odhlaseni)
                        {
                            aktualDataRow = InsertDataToDataset(_smenaID, "", CodeReadCnt, _list_NoRead_Count, "odhlaseni", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);
                            /*
                            if (showHistory)
                            {
                                Zmena();
                                showHistory = false;
                                return;
                            }

                            showHistory = true;
                            */
                            Classes.Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_ODHLASENI_SMENY, "", "");
                            Classes.Database.InsertNewEvents(_smenaID, CodeReadCnt, CodeNoReadCnt, "odhlaseni", _barcodeActual, "", "", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);
                            // Odeslani notifikace o konci smeny
                            NotificationMail.SendEmailOdhlaseniSmeny("DataVyroba()|CodeRead|UlozeniEAN|Odhlaseni", _smenaID, GetDataVyrobaHistory());

                            CodeReadCnt = 0;
                            CodeNoReadCnt = 0;
                            dtSaveLast = DateTime.Now;

                            ClearDataHistory();//zaroven ulozi pocet sepnuti cidel a pocet nactenych CK..

                            rizeniHoukacky(DataVyroba.HoukackaStav.OFF);//vypnuti houkacky
                            rizeniHoukacky(DataVyroba.HoukackaStav.RESET);

                            // nemelo by toto tady byt take ???
                            SetLinkaState(false); // vypne linku 

                            SetPocetPruchodProhaz(0);

                            SetStavVyroba(DataVyroba.VyrobaStavy.LogIDSmena);

                        }
                        else if (_stavVyrobaPredchozi == VyrobaStavy.ZmenaProhaz)
                        {
                            if (!_prohazZapnut)
                            {
                                SetLinkaState(false);
                                ProhazZapnut = true;
                                Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_ZAPNOUT_PROHAZ, "", "");

                            }
                            else
                            {
                                SetLinkaState(true);
                                ProhazZapnut = false;
                                Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_VYPNOUT_PROHAZ, "", "");
                            }

                            aktualDataRow = InsertDataToDataset(_smenaID, "", CodeReadCnt, _list_NoRead_Count, "prohaz", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);

                            Database.InsertNewEvents(_smenaID, CodeReadCnt, CodeNoReadCnt, "zmena prohaz", _barcodeActual, "", "", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);
                            dtSaveLast = DateTime.Now;
                            CodeNoReadCnt = 0;
                            CodeReadCnt = 0;
                            _list_NoRead_Count = 0;

                            StavVyroba = VyrobaStavy.Main;
                        }
                        else if (_stavVyrobaPredchozi == VyrobaStavy.Vyrobek)
                        {
                            _pocetPruchoduProhaz = 0;

                            SetLinkaState(false);
       
                            rizeniHoukacky(HoukackaStav.OFF);
                            rizeniHoukacky(HoukackaStav.RESET);

                            aktualDataRow = InsertDataToDataset(_smenaID, "", CodeReadCnt, _list_NoRead_Count, "Vyrobek", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);

                            Database.InsertNewEvents(_smenaID, CodeReadCnt, CodeNoReadCnt, "zmena vyrobek", _barcodeActual, "", "", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);

                            dtSaveLast = DateTime.Now;
                            CodeReadCnt = 0;
                            CodeNoReadCnt = 0;
                            _barcodeActual = string.Empty;
                            _list_NoRead_Count = 0;

                            StavVyroba = VyrobaStavy.ZadatEAN;
                        }
                        else
                        {
                            SetLinkaState(true);
                            // CHECK : co toto jak toto ???
                            aktualDataRow = InsertDataToDataset(_smenaID, "", CodeReadCnt, _list_NoRead_Count, "else", _barcodeActual, "", "", "", "", "", "", "", "", "", _sarze);
                            //Database.InsertNewEvents(_smenaID, DateTime.Now, 0, CodeNoReadCnt, "else", _barcodeActual, "", "", "", "", DateTime.Now, "", "", "", "", "", "");

                            _list_NoRead_Count = 0;
                            StavVyroba = VyrobaStavy.Main;
                        }

                        CodeReadCnt = 0;
                        CodeNoReadCnt = 0;
                        _stavVyrobaPredchozi = VyrobaStavy.Main;
                        dtSaveLast = DateTime.Now;
                    }

                    _scannerCodeReadCount++; // inkrementujeme celkovy pocet nactenych kodu 

                    if (StavVyroba != VyrobaStavy.Event11 || StavVyroba != VyrobaStavy.Event36)
                    {
                        VarovaniZmena("-");
                        error_id_code = 0;
                        rizeniHoukacky(HoukackaStav.PRUCHOD);
                        rizeniHoukacky(HoukackaStav.OFF);
                    }

                    VypnoutZahaleni();

                    if (codeReadResultPrevious == Code.NoData) //ok prevest
                    {
                    }
                    else if (codeReadResultPrevious == Code.Read) //dalsi cteni bez potvrzeni
                    {   // udalost FALESNE_CTENI (u11), log
                        Log.Write("U11");
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_FALESNE_CTENI, "","");
                        Udalosti.event11(Udalosti.Event11Stav.ADD, 0);
                    }
                    else if (codeReadResultPrevious == Code.NoRead)
                    {   // udalost FALESNE_CTENI (u12) nebo zablik
                        Log.Write("U12 - Read, No Read");
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_NO_UDALOST13, "","");
                    }

                    codeReadResultPrevious = codeReadResultActual;
                    //barcodeNew = CodeNew;
                    codeNew = CodeNewTemp;
                    dtReadLast = DateTime.Now;
                    #endregion
                }
                else if (codeReadResultActual == Code.NoRead)
                {//kod nenacten

                    if (codeReadResultPrevious == Code.NoData && cidla[ackSensor].SensorStateActual == false && cidla[ackSensor].SensorStatePrevious == false)
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_BEZ_CTENI, "","");

                    if (rizeniHoukacky(HoukackaStav.NEPRUCHOD) == HoukackaStav.ON)
                    {
                        if (_stavVyroba != VyrobaStavy.UlozeniEAN)
                        {
                            error_id_code = 2;
                            VarovaniZmena("Více nepřečtených kódů než je limit!");
                        }

                        //log houkacka spustena
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_SPUSTENA_HOUKACKA, "","");
                    }

                    _scannerCodeNoReadCount++; //pocet neprectenych codu ...

                    VypnoutZahaleni();

                    if (codeReadResultPrevious == Code.NoData) //ok prevest
                    {   //ponechat puvodni nacteny kod
                    }
                    else if (codeReadResultPrevious == Code.Read) //dalsi cteni bez potvrzeni
                    {   // udalost FALESNE_CTENI (u12)
                        Log.Write("U12 - No Read, Read");
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_FALESNY_NEPRUCHOD, "","");
                        codeReadResultActual = codeReadResultPrevious; // nastavime aby se neprepsal predchozi read no_readem
                    }
                    else if (codeReadResultPrevious == Code.NoRead)
                    {  // pravdepodobne zablik nebo udalost FALESNE_CTENI (u14)
                        Log.Write("U14 - No Read, No Read");
                        Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_NO_UDALOST14, "","");
                    }

                    codeReadResultPrevious = codeReadResultActual;
                    dtNoReadLast = DateTime.Now;
                }
                else if (codeReadResultActual == Code.BadRead)
                { // spatne cteni ze scanneru
                    VypnoutZahaleni();
                    Log.Write("BadRead");
                    Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_CODE_BADREAD, "","");

                }
                else
                {
                    VypnoutZahaleni();
                    Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_SPATNE_CTENI, "","");
                }
                /***********Osetreni stavu scanneru z aktualniho pruchodu - Konec***************/
                /**************************Logika hlidani odvodu - Konec*************************/
            }
            catch (Exception ex)
            {
                Log.Write("Algoritumus " + ex.Message); 
                return;
            }
        }

        public void SetPocetPruchodProhaz(int count)
        {
            _pocetPruchoduProhaz = count;
        }

        public void ScannerActivate(string code, Code codeReadResult)
        {
            algoritmusOdvodVyrobva(code, codeReadResult, false);

            algoritmusOdvodVyrobva(string.Empty, Code.NoData, false);
        }

        private void TestZahaleniLinky()
        {
            // CHECK : ne dtSaveLast, ale Iddle last !!!
            //TimeSpan rozdil = DateTime.Now - dtSaveLast;
            TimeSpan rozdil = DateTime.Now - dtIdleLast;
            double sekund = rozdil.TotalSeconds;

            if ((!_zahaleniLinky) && (sekund > AgroConfig.config.Agro[0].ZahaleniStartSekund))
            {
                _zahaleniLinky = true;

                Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_ZAHALENI_ZAHAJENO,"","");

                error_id_code = 3;
                VarovaniZmena("Linka zahálí!");

                // CHECK : Proc neco nulovat??? jde jen o priznak zahaleni ...
                //// vratime vse do pocatecniho stavu
                //cidla[ackSensor].setSensorStateActualDeactive();
                //cidla[ackSensor].setSensorStatePreviousDeactive();
                //codeReadResultActual = Code.NoData;
                //codeReadResultPrevious = Code.NoData;
            }

            // TODO : pokud je rozdil mensi, tak zahaleni vypnout ...
        }

        private void VypnoutZahaleni()
        {
            if (_zahaleniLinky)
            {
                Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_ZAHALENI_UKONCENO,"","");
                _zahaleniLinky = false;
                VarovaniZmena("-"); 
                error_id_code = 0;
                dtIdleLast = DateTime.Now;
            }
        }

        public void SetLinkaState(bool on)
        {
            if (on)
                adam.rizeniLinky(LinkaStav.LINKA_ON);
            else
                adam.rizeniLinky(LinkaStav.LINKA_OFF);
        }

        #region Rizeni Houkacky
        
        public void vypnutiHoukacky()
        {
            if (_pocetPruchoduProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
            {
                rizeniHoukacky(HoukackaStav.OFF);
                _pocetPruchoduProhaz = 0;
            }
            else
            {
                rizeniHoukacky(HoukackaStav.OFF);

                if (_barcodeActual == string.Empty)
                {
                    // kod je neinicializovany, zepta se obsluhy, jestli ho chce dodatecne zadat rucne
                    StavVyroba = VyrobaStavy.VlozKod;
                }
            }
        }

        private HoukackaStav _stavHoukacky = HoukackaStav.OFF;
        private int MaxPocetNepruchoduHoukacka = AgroConfig.config.Agro[0].PocetNepruchoduSepnutiReleHoukacky;
        private int PocetNepruchoduHoukacka = 0;
        private bool hlidaniHoukacka = true;

        public HoukackaStav rizeniHoukacky(HoukackaStav command)
        {
            switch (command)
            {
                case HoukackaStav.ON:
                    if (_stavHoukacky == HoukackaStav.OFF) //vypinani houkacky trva moc dlouho...
                        adam.zapniHoukacku();
                    _stavHoukacky = HoukackaStav.ON;
                    break;
                case HoukackaStav.OFF:
                    if (_stavHoukacky == HoukackaStav.ON) //vypinani houkacky trva moc dlouho...
                        adam.vypniHoukacku();
                    _stavHoukacky = HoukackaStav.OFF;
                    break;
                case HoukackaStav.PRUCHOD:
                    //hlidani = true; // po pruchodu zapne automaticky hlidani
                    PocetNepruchoduHoukacka = 0;
                    if (_stavHoukacky == HoukackaStav.ON)
                        adam.vypniHoukacku();
                    _stavHoukacky = HoukackaStav.OFF;
                    break;
                case HoukackaStav.NEPRUCHOD:
                    if (hlidaniHoukacka && (_stavHoukacky == HoukackaStav.OFF))
                    {
                        PocetNepruchoduHoukacka++;
                        if (PocetNepruchoduHoukacka > MaxPocetNepruchoduHoukacka)
                        {
                            adam.zapniHoukacku();
                            _stavHoukacky = HoukackaStav.ON;
                        }
                    }
                    break;
                case HoukackaStav.RESET:
                    PocetNepruchoduHoukacka = 0;
                    break;
                case HoukackaStav.SET_MAX:
                    MaxPocetNepruchoduHoukacka = 0;
                    break;
                case HoukackaStav.STAV:
                    break;  // pouze dotaz na stav
                case HoukackaStav.ZAPNOUT_HLIDANI:
                    hlidaniHoukacka = true;
                    break;
                case HoukackaStav.VYPNOUT_HLIDANI:
                    hlidaniHoukacka = false;
                    break;
            }
            return _stavHoukacky;
        }
        #endregion

        #region Vkladani mnozstvi do lokalni databaze pro zobrazeni
        /// <summary>
        /// Vlozi zaznam do lokalni databaze
        /// </summary>
        /// <returns>Vraci existujici nebo novy zaznam</returns>
        public Data.FASK_EventsRow InsertDataToDataset(string loginid, string machineid, decimal qty, decimal qtyReal, string description, string barcodeReaded, string barcodeSended,
            string zakazka, string popis, string reporttype, string ido, string scan1, string scan2, string scan3, string sensor, string material)
        {
            try
            {
                //for (int i = 0; i < vyrobaData.FASK_Events.Count; i++)
                //{
                //    if (vyrobaData.FASK_Events[i].barcodeReaded == barcodeReaded)
                //    {
                //        vyrobaData.FASK_Events[i].qty += qty;
                //        vyrobaData.FASK_Events[i].qtyReal += qtyReal;
                //        return vyrobaData.FASK_Events[i];
                //    }
                //}

                var items = vyrobaDataHistoryInMemory.FASK_Events.Where(x => x.barcodeReaded == barcodeReaded && x.material == material);
                if (items.Count() > 0)
                {
                    var item = items.First();
                    item.qty += qty;
                    item.qtyReal += qtyReal;
                    return item;
                }

                // Pokud nebylo nalezeno, tak ho vlozi ...
                return vyrobaDataHistoryInMemory.FASK_Events.AddFASK_EventsRow(loginid, machineid, DateTime.Now, qty, qtyReal, description, barcodeReaded, barcodeSended,
                    zakazka, popis, new Guid(), reporttype, DateTime.Now, ido, scan1, scan2, scan3, sensor, material);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                return null;
            }
            //}
        }

        /// <summary>
        /// Odecte z lokalni databaze s poctem kusu a eanu odpovidajici pocet.
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="qty"></param>
        /// <param name="qtyReal"></param>
        /// <param name="barcodeReaded"></param>
        /// <returns>True: pokud existuje a odecte, False: pokud neexistuje, neodecte</returns>
        public bool InsertMinusQtyToDataset(string loginid, decimal qty, decimal qtyReal, string barcodeReaded, string material)
        {
            //for (int i = 0; i < vyrobaData.FASK_Events.Count; i++)
            //{
            //    if (vyrobaData.FASK_Events[i].barcodeReaded == barcodeReaded)
            //    {
            //        vyrobaData.FASK_Events[i].qty -= qty;
            //        vyrobaData.FASK_Events[i].qtyReal -= qtyReal;
            //        return true;
            //    }
            //}

            var items = vyrobaDataHistoryInMemory.FASK_Events.Where(x => x.barcodeReaded == barcodeReaded && x.material == material);
            if (items.Count() > 0)
            {
                var item = items.First();
                item.qty -= qty;
                item.qtyReal -= qtyReal;
                return true;
            }

            return false;
        }
        #endregion

        public void CloseTimer()
        {
            this.timer = null;
        }

        public void inkrementSensorAcitvatedCount(int sensor)
        {
            //zvedne se jen v pripade, ze je to index sensoru, ktery je v listu, jinak by doslo k vyjimce :(
            if (sensor < sensorsCount)
            {
                cidla[sensor].SensorsCnt++;
#if DEBUG
                //Console.WriteLine("cidlo[" + sensor + "]=" + cidla[sensor].SensorsCnt);
                System.Diagnostics.Debug.WriteLine("cidlo[" + sensor + "]=" + cidla[sensor].SensorsCnt);
#endif
            }
        }

        public void inkrementScannerAcitvatedCount()
        {
            //zvedne se jen v pripade, ze je to index sensoru, ktery je v listu, jinak by doslo k vyjimce :(
                scanner.SensorsCnt++;
#if DEBUG
                System.Diagnostics.Debug.WriteLine("scanner=" + scanner.SensorsCnt);
#endif
        }        


        #region Saving Data
        /// <summary>
        /// Ulozi a vynuluje lokalni data, citace sensoru, citac poctu car.kodu
        /// </summary>
        public void ClearDataHistory()
        {
            saveFASKEventsData();
            saveSensorsCount();
            saveScannerCodeCount();
        }

        /// <summary>
        /// Ulozi do udadlosti obsluhy pocet nactenych car.kodu a vynuluje citac
        /// </summary>
        private void saveScannerCodeCount()
        {
            Classes.Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_CODEREAD_COUNT, _scannerCodeReadCount.ToString(),"");
            Classes.Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_CODENOREAD_COUNT, _scannerCodeNoReadCount.ToString(),"");
            _scannerCodeReadCount = 0;    //nastavime na 0 pocet nactenych kodu
            _scannerCodeNoReadCount = 0;  //nastavime na 0 pocet nenactenych kodu
        }

        /// <summary>
        /// Ulozi do udalosti obsluhy pocty v lokalnim datasetu carovykod a jeho pocet
        /// <remarks>Slouzi pro verifikaci hodnot nactenych a ulozenych do Events ... (40=carovykod, 41=pocet</remarks>
        /// </summary>
        private void saveFASKEventsData()
        {
            foreach (var item in vyrobaDataHistoryInMemory.FASK_Events)
            {
                Classes.Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_FASK_EVENTS_DATA_BARCODE, item.barcodeReaded.Trim(),"");
                Classes.Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_FASK_EVENTS_DATA_COUNT_READ, item.qty.ToString("0"),"");
                Classes.Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_FASK_EVENTS_DATA_COUNT_NOREAD, item.qtyReal.ToString("0"),"");
            }

            vyrobaDataHistoryInMemory.FASK_Events.Clear();
            this.aktualDataRow = null;
        }

        /// <summary>
        /// Ulozi stav sledovanych cidel (4) a vynuluje citace ...
        /// </summary>
        private void saveSensorsCount()
        {
            Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_PHOTO1_COUNT,cidla[0].SensorsCnt.ToString(),"");
            Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_PHOTO2_COUNT, cidla[1].SensorsCnt.ToString(), "");
            Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_PHOTO3_COUNT, cidla[2].SensorsCnt.ToString(), "");
            Database.InsertNewUserEvents(_smenaID, AgroConfig.LOG_PHOTO4_COUNT, cidla[3].SensorsCnt.ToString(), "");

            cidla[0].SensorsCnt = 0;
            cidla[1].SensorsCnt = 0;
            cidla[2].SensorsCnt = 0;
            cidla[3].SensorsCnt = 0;
        }

        public void SaveActualDataLogScreen()
        {
            if (CodeReadCnt + CodeNoReadCnt > 0)
            {   //pokud neco v bufferu bylo, pak zapis
                Database.InsertNewEvents(_smenaID, CodeReadCntGetAndReset(), CodeNoReadCntGetAndReset(), "mimo smenu", _barcodeActual, "", "zadna", "", "", "", "", "", "", "", _sarze);
            }
        }

        #endregion


    }
}