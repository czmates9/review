using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using FASK.SledovaniVyroby.IScannerProvider;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.DataSets;
using FASK.SledovaniVyroby.ErrorLog;
using System.Windows.Forms;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes
{
    public class DataVyroba
    {
        #region Udalosti odvadeni vyroby

        public delegate void EventHandlerZmenaStavu(VyrobaStavy newStav);
        public delegate void EventHandlerZmenaWarning(string hlaska);
        public delegate void EventHandlerZmenaProhazovani(string hlaska, string znak);

        public event EventHandlerZmenaStavu ZmenaStavu;
        public event EventHandlerZmenaWarning ZmenaWarning;
        public event EventHandlerZmenaProhazovani ZmenaProhazovani;

        private int MachineID_Int = 0;
        private FASK.Palety_SSCC.SQLite.Classes.SSCC_Generator _Generator = new Palety_SSCC.SQLite.Classes.SSCC_Generator();

        private bool Flag_PosleniPaleta_je_NP = false;
        private bool Flag_PosleniPaletaOdhlaseniZadat = true;
        private bool Flag_OtevrenyDialog_PosleniPaleta = false;
        private bool Flag_PO_PredcasnyOdjezd = false;

        private VyrPrikaz _vp = null;

        private frmPosledniPaleta frmPP = null;

        /// <summary>
        /// Vyvola udalost Zmena Stavu
        /// </summary>
        /// <param name="stavvyroby"></param>
        private void OnZmenaStavu(VyrobaStavy stavvyroby)
        {
            if (ZmenaStavu != null)
            {
                ZmenaStavu(stavvyroby);
            }
        }

        /// <summary>
        /// Vyvola udalost VarovaniZmena
        /// </summary>
        /// <param name="hlaska"></param>
        private void OnZmenaWarning(string hlaska)
        {
            if (ZmenaWarning != null)
            {
                ZmenaWarning(hlaska);
            }
        }

        /// <summary>
        /// Vyvola udalost ProhazZmena
        /// </summary>
        /// <param name="hlaska"></param>
        /// <param name="znak"></param>
        private void OnZmenaProhazovani(string hlaska, string znak)
        {
            if (ZmenaProhazovani != null)
            {
                ZmenaProhazovani(hlaska, znak);
            }
        }

        #endregion

        #region UI Componenty pro zobrazovani vyroby

        private frmMainAgroVyroba _mainForm = null;
        public frmMainAgroVyroba MainForm
        {
            get { return this._mainForm; }
            set
            {
                this._mainForm = value;
                if (this._mainForm != null)
                {
                    // TODO : nalinkovani udalosti
                    this.ZmenaStavu += new EventHandlerZmenaStavu(this._mainForm.vyroba_ZmenaStavu);
                }
            }
        }

        private InformationUC _InformationUC = null;
        public InformationUC InformationUC
        {
            get { return this._InformationUC; }
            set
            {
                this._InformationUC = value;
                if (_InformationUC != null)
                {
                    // TODO : nalinkovani udalosti
                    this.ZmenaStavu += new EventHandlerZmenaStavu(this._InformationUC.vyroba_ZmenaStavu);
                    this.ZmenaWarning += new EventHandlerZmenaWarning(this._InformationUC.vyroba_ZmenaWarning);
                    this.ZmenaProhazovani += new EventHandlerZmenaProhazovani(this._InformationUC.vyroba_ProhazZmena);
                }
            }
        }

        private KeyboardUC _KeyboardUC = null;
        public KeyboardUC KeyboardUC
        {
            get { return this._KeyboardUC; }
            set
            {
                this._KeyboardUC = value;
                if (this._KeyboardUC != null)
                {
                    // TODO : nalinkovani udalosti
                    this.ZmenaStavu += new EventHandlerZmenaStavu(this._KeyboardUC.vyroba_ZmenaStavu);
                    this._KeyboardUC.ButtonClick += new EventHandler(StavVyroba_KeyboardUC_ButtonClick);
                }
            }
        }

        #endregion

        #region Konsturkutor DataVyroba
        /// <summary>
        /// Konstruktor pro odvod vyroby
        /// </summary>
        /// <param name="sensors_count">Pocet cidel, ktera se budou sledovat (DI)</param>
        /// <param name="potvrzovacicidlocislo">Index cidla, ktere se pouziva jako potvrzovaci</param>
        public DataVyroba(
            int sensors_count
            //,int potvrzovacicidlocislo
            )
        {
            StavVyrobaStackInitialize();

            VynulovaniPromennych();

            this._vyrobaDataHistoryInMemory = new Data();

            this.dtIdleLast = DateTime.Now;

            this._cidla_vstupni = new List<Cidlo>();

            //this._sensorAck = potvrzovacicidlocislo;

            //this._sensorsCount = sensors_count;

            for (int i = 0; i < sensors_count; i++)
            {
                _cidla_vstupni.Add(new Cidlo());
            }

            this._cidlo_scanner = new CidloScanner();

            MachineID_Int = int.Parse(Logging.LogConfig.MachineID.Trim());
            this._SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);

            LoadPytle();

            timerTesty = new System.Threading.Timer(TimerTestyCallback, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            //timerTesty.Change(1000, System.Threading.Timeout.Infinite);
            TimerTestyOn();
        }
        #endregion

        #region Pracovnici
        private List<string> _pracovnici = new List<string>();
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
        private int sarzehodnotamaxlength = 50;
        public string _sarze = string.Empty;
        public string Sarze
        {
            get { return this._sarze; }
        }

        public void SarzeClear()
        {
            this.SarzeSet(string.Empty);
        }

        public string SarzeSet(string sarze)
        {
            this._sarze = sarze;
            if (this.InformationUC != null)
                this.InformationUC.LblSarze = this._sarze;
            return this._sarze;
        }

        /// <summary>
        /// Nacte sarzi z centralni databaze a nastavi automaticky
        /// </summary>
        /// <returns></returns>
        public string SarzeGenerateAndSet()
        {
            //this._sarze = SarzeGenerate();
            //return this._sarze;

            ExceptionHandler2.Handle("SarzeGenerateAndSet()" + this.SarzeSet(SarzeGenerate()), "Sarze", "txt");
            return this.SarzeSet(SarzeGenerate());
        }

        /// <summary>
        /// Pouze Nacte sarzi z centralni databaze ale nenastavi
        /// </summary>
        /// <returns></returns>
        public string SarzeGenerate()
        {

            return Database.Classes.Vyroba_Remote.ReturnSarze(
            Logging.LogConfig.SqlConnectionStringGlobal,
            int.Parse(Logging.LogConfig.MachineID),
            this._smenaID, 
            _pracovnici.First(), 
            Logging.LogConfig.MachineID.Trim()
            );
        }

        /// <summary>
        /// Pouze Nacte sarzi ID z centralni databaze ale nenastavi
        /// </summary>
        /// <returns></returns>
        public bool ReturnID(string inID)
        {
            return Database.Classes.Vyroba_Remote.ReturnID(
                    Logging.LogConfig.SqlConnectionStringGlobal,
                    int.Parse(Logging.LogConfig.MachineID),
                    inID
                    );
        }

        private string _loginNaZmenuSarze;

        /// <summary>
        /// Pouze Nacte sarzi HESLO z centralni databaze ale nenastavi
        /// </summary>
        /// <returns></returns>
        public bool ReturnHeslo(string inHESLO,string inID)
        {
            return Database.Classes.Vyroba_Remote.ReturnHeslo(
                Logging.LogConfig.SqlConnectionStringGlobal,
                int.Parse(Logging.LogConfig.MachineID),
                inHESLO,
                inID
                );
        }

        #endregion

        #region Vyroby prikaz

        public string _vph_sopnumbe = string.Empty;
        public string VPH_SOPNUMBE
        {
            get { return this._vph_sopnumbe; }
        }

        public void VyrobniPrikazClear()
        {
            this.VyrobniPrikazSet(string.Empty);
        }

        public string VyrobniPrikazSet(string sopnumbe)
        {
            this._vph_sopnumbe = sopnumbe;
            if (this.InformationUC != null)
                this.InformationUC.LblVyrPrikaz = this._vph_sopnumbe;
            return this._vph_sopnumbe;
        }

        #endregion

        #region Vyroby prikaz Položka

        public ICommDatabase.DSVyroba.CZPRO_VPPRow _vpp_row = null;
        public ICommDatabase.DSVyroba.CZPRO_VPPRow VPP_Row
        {
            get { return this._vpp_row; }
        }

        public void PolozkaVyrPrikazuClear()
        {
            this.PolozkaVyrPrikazuSet(null);
        }

        public ICommDatabase.DSVyroba.CZPRO_VPPRow PolozkaVyrPrikazuSet(ICommDatabase.DSVyroba.CZPRO_VPPRow Row)
        {
            this._vpp_row = Row;
            if (this.InformationUC != null)
            {
                //this.InformationUC.LblPolozkaVyrPrikaz = string.Format("{0}({1})({2})", this._vpp_row.ITEMDESC, this._vpp_row.ITEMNMBR, this._vpp_row.BarcodeP);
                this.InformationUC.LblPolozkaVyrPrikaz = this._vpp_row.IsITEMDESCNull() ? "---" : this._vpp_row.ITEMDESC;
            }

            return this._vpp_row;
        }

        #endregion

        #region Stavy Vyroby, Houkacky, Linky, ...

        /// <summary>
        /// Vraci interni stav pro zalohovani ve forme textoveho retezce
        /// </summary>
        /// <returns></returns>
        public string GetStateInternal()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("_odvodVyroby.BarcodeActual=" + this._odvodVyroby.BarcodeActual);
                sb.AppendLine("_odvodVyroby._codeNoReadCnt=" + this._odvodVyroby._codeNoReadCnt);
                sb.AppendLine("_odvodVyroby._codeReadCnt=" + this._odvodVyroby._codeReadCnt);
                //sb.AppendLine("_linkaStateBeforeEvent=" + this._linkaStateBeforeEvent);
                sb.AppendLine("_pocetPruchoduProhaz=" + this._PocetPruchoduZmenDoProhaz);
                sb.AppendLine("_prohazZapnut=" + this._prohazZapnut);
                sb.AppendLine("_prohazZnak=" + this._prohazZnak);
                sb.AppendLine("_smenaID=" + this.SmenaID);
                sb.AppendLine("_stavHoukacky=" + this._HoukackaStav);
                //sb.AppendLine("_stavVyroba=" + this._stavVyroba.ToString());
                //sb.AppendLine("_stavVyrobaPredchozi=" + this._stavVyrobaPredchozi.ToString());
                // vypis stackVyrobaStav
                sb.AppendLine("_stavVyrobaStack:" + this.StavVyrobaStackToString());
                sb.AppendLine("_zahaleniLinky=" + this._zahaleniLinky);
                //sb.AppendLine("_sensorAck=" + this._sensorAck);
                sb.AppendLine("_actualPytel=" + (this._actualPytel == null ? "NULL" : this._actualPytel.Nazev + "(" + this._actualPytel.MinDelay + ";" + this._actualPytel.MaxDelay + ")"));
                sb.AppendLine("_cidlo_scanner.SensorsCnt=" + this._cidlo_scanner.SensorsCnt);
                //sb.AppendLine("_sensorsCount=" + this._sensorsCount);
                sb.AppendLine("_cidla_vstupni.Count=" + this._cidla_vstupni.Count);
                for (int i = 0; i < this._cidla_vstupni.Count; i++)
                {
                    sb.AppendLine("[" + i + "].SensorsCnt=" + this._cidla_vstupni[i].SensorsCnt);
                }
                var divalueslast = adam.DIValuesLast;
                sb.AppendLine("DI Values Count = " + divalueslast.Count());
                for (int i = 0; i < divalueslast.Length; i++)
                {
                    sb.AppendLine("[" + i + "].DIValue=" + divalueslast[i].ToString());
                }
                var distatuslast = adam.DIStatusLast;
                sb.AppendLine("DI Status Count = " + distatuslast.Count());
                for (int i = 0; i < distatuslast.Length; i++)
                {
                    sb.AppendLine("[" + i + "].DIStatus=" + distatuslast[i].ToString());
                }
                var dostatuslast = adam.DOStatusLast;
                sb.AppendLine("DO Status Count = " + dostatuslast.Count());
                for (int i = 0; i < dostatuslast.Length; i++)
                {
                    sb.AppendLine("[" + i + "].DOStatus=" + dostatuslast[i].ToString());
                }
                sb.AppendLine("ReadResult=" + this.Vyroba_Scan.ReadResult);
                
                sb.AppendLine("dtIdleLast=" + this.dtIdleLast.ToString());
                sb.AppendLine("dtNoReadLast=" + this.dtNoReadLast.ToString());
                sb.AppendLine("dtPytelRead=" + this.dtPytelReadLast.ToString());
                sb.AppendLine("dtReadLast=" + this.dtReadLast.ToString());
                sb.AppendLine("dtSaveLast=" + this.dtSaveLast.ToString());

                sb.AppendLine("ErrorIDCode=" + this.InformationUC.InfoIDCode);

                sb.AppendLine("hlidaniHoukacka=" + this._HoukackaHlidani);
                sb.AppendLine("MaxPocetNepruchoduHoukacka=" + this._HoukackaPocetNepruchoduMax);
                sb.AppendLine("PocetNepruchoduHoukacka=" + this._HoukackaPocetNepruchodu);
                sb.AppendLine("Pracovnici:");
                for (int i = 0; i < this._pracovnici.Count; i++)
                {
                    sb.AppendLine(String.Format("{0}: id={1}", i, this._pracovnici[i]));
                }
                return sb.ToString();

            }
            catch (Exception ex)
            {
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// Stavy houkacky
        /// </summary>
        public enum HoukackaStav
        {
            OFF = 0,
            ON,
            PRUCHOD,
            NEPRUCHOD,
            RESET,
            SET_MAX,
            STAV,
            VYPNOUT_HLIDANI,
            ZAPNOUT_HLIDANI
        };

        ///// <summary>
        ///// Stavy linky
        ///// </summary>
        //public enum LinkaStav
        //{
        //    LINKA_ON = 0,
        //    LINKA_OFF,
        //    LINKA_STAV
        //};

        /// <summary>
        /// Stavy vyroby
        /// </summary>
        public enum VyrobaStavy { 
            Main = 0 
            ,LogIDSmena = 1
            ,LogIDSmenaPotvrzeni = 2
            ,LogIDPracovnik = 3
            ,LogIDPracovnikPotvrzeni = 4
            ,ZadatEAN = 5
            ,Odhlaseni = 6
            ,ZmenaProhaz = 7
            ,UlozeniEAN = 8
            ,Vyrobek = 9
            ,VlozKod = 10
            ,VlozPocet = 11
            ,VlozPocetEAN = 12
            ,SarzeHeslo = 13
            ,SarzeHodnota = 14
            ,SarzeID = 15
            ,Event11 = 16
            ,Event36 = 17
            ,VPH_Zadani = 18
            ,VPP_VlozitKod = 19
            ,Zombie = 20
            , Zadani_PoslednaPaleta_Pytle = 21
            , Zadani_PoslednaPaleta_Palety = 22
            , Zadani_PoslednaPaleta_PotvrzeniPalety_NP = 23
            , Zadani_PoslednaPaleta_PotvrzeniPalety_UP = 24
            , SERVIS = 25
        };
        #endregion

        #region ADAM 
        /// <summary>
        /// hw pro sledovani cidel a ovladani zarizeni
        /// </summary>
        private ADAM.ADAM_60XX adam;
        [System.ComponentModel.Browsable(false)]
        public ADAM.ADAM_60XX Adam
        {
            get { return adam; }
            set
            {
                this.adam = value;

                //if (adam.GetProhazSensorState())
                //    ProhazZnak = "!";
                //else
                //    ProhazZnak = "";

            }
        }
        #endregion

        #region Casy akci pro vyhodnocovani ...
        
        // TODO : casy proverit pouziti a udelat z totho nejakou tridu casu ... ???
        /// <summary>
        /// Cas posledniho uspesneho cteni
        /// </summary>
        /// <remarks>slouzi ke zjistovani stavu cteni</remarks>
        private DateTime dtReadLast = DateTime.Now;         // TODO : ? proverit k cemu je
        /// <summary>
        /// Cas posledniho neuspesneho cteni scanneru
        /// </summary>
        /// <remarks>slouzi ke zjistovani stavu cteni</remarks>
        private DateTime dtNoReadLast = DateTime.Now;       // TODO : ? proverit k cemu je
        /// <summary>
        /// Cas posledni akce, tedy prichod cteni scanner nebo zmena stavu cidel linky?
        /// </summary>
        private DateTime dtIdleLast = DateTime.Now;         // cas posledni akce, slouzi pro zjisteni zahaleni
        /// <summary>
        /// Cas posledniho ulozeni vyroby
        /// </summary>
        public DateTime dtSaveLast = DateTime.Now;          // cas posledniho ulozeni vyroby
        /// <summary>
        /// Cas ktery slouzi pro vyhodnocovani velikosti pytlu, ktere prochazeji cidlem(linkou)
        /// </summary>
        private DateTime dtPytelReadLast = DateTime.Now;        // posledni cas aktivace potrvzovaciho cidla scanneru
        ///// <summary>
        ///// Cas debugu ???
        ///// </summary>
        //private DateTime dtDebugWriteLast = DateTime.Now;

        #endregion

        #region Velikosti Pytlu

        /// <summary>
        /// Velikosti pytlu dle casovych prubehu pro nalezeni aktualpytel
        /// </summary>
        private DataSets.Pytel _pytle = new Vyroba_Agro_Modbus.DataSets.Pytel();
        /// <summary>
        /// aktualni velikost pytle
        /// </summary>
        private DataSets.Pytel.PytelRow _actualPytel = null;
        /// <summary>
        /// Aktualni velikost pytle
        /// </summary>
        public DataSets.Pytel.PytelRow ActualPytel
        {
            get
            {
                return _actualPytel;
            }
        }

        /// <summary>
        /// Nacteni velikosti pytlu z XML souboru definice
        /// </summary>
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
                    _pytle._Pytel.AddPytelRow(configNode.Attributes["nazev"].Value, int.Parse(configNode.Attributes["minDelay"].Value), int.Parse(configNode.Attributes["maxDelay"].Value));

                    configNode = (XmlElement)configNode.NextSibling;
                }

            }
            catch (Exception exc)
            {
                //Log.WriteException(exc);
                ExceptionHandler2.Handle(exc);
            }
        }

        /// <summary>
        /// Zjisteni nazvu pytle
        /// </summary>
        private void ZjisteniNazvuPytle()
        {
            // zjisteni nazvu pytle :
            // - zjistuje se dle casu aktivace cidla pruchodu
            // - pokud je cas cidla pruchodu vetsi nez zapamatovany, pak porovnam rozdil a rozohodnu dle casu o co jde ... 
            // - nakonec nastavim cas pruchodu na aktualni ... 

            if ((Vyroba_SensorPytel.Active) && (Vyroba_SensorPytel.ActivatedTime > dtPytelReadLast)) //melo by to byt OK...
            {
                TimeSpan delay_current = Vyroba_SensorPytel.ActivatedTime - dtPytelReadLast;  //casovy rozdil mezi poslednim a novym sepnutim
                var delay_current_ms = delay_current.TotalMilliseconds;

                var pytel = _pytle._Pytel.Where(x => x.MinDelay <= delay_current_ms && delay_current_ms <= x.MaxDelay);

                if (pytel.Count() > 0)
                {
                    this._actualPytel = pytel.First();
                }

                dtPytelReadLast = Vyroba_SensorPytel.ActivatedTime;
            }
        }

        #endregion

        #region Timer Testy

        /// <summary>
        /// timer pro provadeni testu stavu
        /// </summary>
        private System.Threading.Timer timerTesty;

        /// <summary>
        /// Vypnuti timeruTestu
        /// </summary>
        private void TimerTestyOff()
        {
            if (timerTesty != null)
                //timerTesty.Change(System.Threading.Timeout.Infinite, 1000); // TODO : ??? vypnuti ma dalsi spusteni 1000ms???
                timerTesty.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Zapnuti timeru testu
        /// </summary>
        private void TimerTestyOn()
        {
            if (timerTesty != null)
                timerTesty.Change(1000, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Ukonceni timeru a zruseni objektu
        /// </summary>
        public void TimerTestyClose()
        {
            TimerTestyOff();
            this.timerTesty = null;
        }

        /// <summary>
        /// Akce pri spusteni timerutesty.
        /// Provadeni testu stavu linky ...
        /// </summary>
        /// <param name="state"></param>
        private void TimerTestyCallback(object state)
        {
            try
            {
                System.Threading.Thread.CurrentThread.Name = "TimerTesty " + DateTime.Now.ToString();

                TimerTestyOff();

                TestZahaleniLinky();

                TestUdalost11();

                TestUdalost36();

                //TODO MaR 11.7.2023 zakomentovani hlasky zmnen prohaz
                //TestProhaz();

                TestAutoUlozeniDat();
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "DataVyroba.TimerTestyCallback()", false);
                ExceptionHandler2.Handle(ex.Message, "DataVyroba.TimerTestyCallback()", false);
            }
            finally
            {
                TimerTestyOn();
            }
        }

        #endregion

        #region Prohazovani
        /// <summary>
        /// interni promenna pro pocitani poctu pruchodu k prepnuti do prohazovani
        /// </summary>
        private int _PocetPruchoduZmenDoProhaz = 0;
        /// <summary>
        /// Pocet pruchodu, kdy je linka zastavena. Pokud presanhne nastaveny pocet, pak byse melo zobrazit varovani k prepnuti odvadeni do prohazovani
        /// </summary>
        public int PocetPruchoduZmenProhaz
        {
            get { return _PocetPruchoduZmenDoProhaz; }
            set { _PocetPruchoduZmenDoProhaz = value; }
        }

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
                OnZmenaProhazovani(_prohazZapnut ? "Zapnuto" : "Vypnuto", _prohazZnak);
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
                OnZmenaProhazovani(_prohazZapnut ? "Zapnuto" : "Vypnuto", _prohazZnak);
            }
        }

        #endregion

        #region Zahaleni Linky
        /// <summary>
        /// Stav zahaleni linky
        /// </summary>
        private bool _zahaleniLinky = false; //pokud je nastaveno na true, tak linka zahali
        
        /// <summary>
        /// Stav zahaleni linky
        /// </summary>
        public bool ZahaleniLinky
        {
            get { return this._zahaleniLinky; }
            set { this._zahaleniLinky = value; }
        }

        /// <summary>
        /// Ukonceni zahaleni linky => nastaveni vystupu adama, log do userevents
        /// </summary>
        private void VypnoutZahaleni()
        {
            if (_zahaleniLinky)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZAHALENI_UKONCENO, "", "");
                _zahaleniLinky = false;
                this.OnZmenaWarning("-");
                this.InformationUC.InfoIDCode = 0;
                dtIdleLast = DateTime.Now;
            }
        }

        #endregion

        #region Rizeni Stavu Linky
        /// <summary>
        /// Spusteni / Zastaveni Linky => respektive aktivace vystupu ADAMA
        /// </summary>
        /// <param name="on"></param>
        public void SetLinkaState(bool onState)
        {
            Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_SET_LINKA_STATE, onState.ToString(), "");
            
            //if (on)
            //    adam.rizeniLinky(LinkaStav.LINKA_ON);
            //else
            //    adam.rizeniLinky(LinkaStav.LINKA_OFF);

            if (adam != null)
                adam.rizeniLinky(onState);
        }
        #endregion

        #region Rizeni Stavu Servis
        /// <summary>
        /// Spusteni / Zastaveni Linky => respektive aktivace vystupu ADAMA
        /// </summary>
        /// <param name="on"></param>
        public void SetServisState(bool onState)
        {
            Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_SET_SERVIS_STATE, onState.ToString(), "");

            if (adam != null)
                adam.rizeniServis(onState);
        }
        #endregion

        #region Rizeni Houkacky

        private HoukackaStav _HoukackaStav = HoukackaStav.OFF;
        private bool _HoukackaHlidani = true;
        private int _HoukackaPocetNepruchoduMax = AgroConfig.config.Agro[0].PocetNepruchoduSepnutiReleHoukacky;
        private int _HoukackaPocetNepruchodu = 0;

        public HoukackaStav HoukackaRizeni(HoukackaStav command)
        {

            //AGRO_HOUKACKA - TaD Zakomentovane stavy krome ON OFF a  STAV
            // pro testovani, podle JaS se nic jine nepouživá


            switch (command)
            {
                case HoukackaStav.ON:
                    if (_HoukackaStav == HoukackaStav.OFF) //vypinani houkacky trva moc dlouho...
                        adam.zapniHoukacku();
                    _HoukackaStav = HoukackaStav.ON;
                    break;
                case HoukackaStav.OFF:
                    if (_HoukackaStav == HoukackaStav.ON) //vypinani houkacky trva moc dlouho...
                        adam.vypniHoukacku();
                    _HoukackaStav = HoukackaStav.OFF;
                    break;
                //case HoukackaStav.PRUCHOD:
                //    //hlidani = true; // po pruchodu zapne automaticky hlidani
                //    _HoukackaPocetNepruchodu = 0;
                //    if (_HoukackaStav == HoukackaStav.ON)
                //        adam.vypniHoukacku();
                //    _HoukackaStav = HoukackaStav.OFF;
                //    break;
                //case HoukackaStav.NEPRUCHOD:
                //    if (_HoukackaHlidani && (_HoukackaStav == HoukackaStav.OFF))
                //    {
                //        _HoukackaPocetNepruchodu++;
                //        if (_HoukackaPocetNepruchodu > _HoukackaPocetNepruchoduMax)
                //        {
                //            adam.zapniHoukacku();
                //            _HoukackaStav = HoukackaStav.ON;
                //        }
                //    }
                //    break;
                //case HoukackaStav.RESET:
                //    _HoukackaPocetNepruchodu = 0;
                //    break;
                //case HoukackaStav.SET_MAX:
                //    _HoukackaPocetNepruchoduMax = 0;
                //    break;
                case HoukackaStav.STAV:
                    break;  // pouze dotaz na stav
                //case HoukackaStav.ZAPNOUT_HLIDANI:
                //    _HoukackaHlidani = true;
                //    break;
                //case HoukackaStav.VYPNOUT_HLIDANI:
                //    _HoukackaHlidani = false;
                //    break;
            }
            return _HoukackaStav;
        }

        //AGRO_HOUKACKA - Ale tohle se ani nikde nepouživalo
        // Ale toto neni pouzito ... ???
        //public void HoukackaVypnout()
        //{
        //    if (this._PocetPruchoduZmenDoProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
        //    {
        //        HoukackaRizeni(HoukackaStav.OFF);
        //        _PocetPruchoduZmenDoProhaz = 0;
        //    }
        //    else
        //    {
        //        HoukackaRizeni(HoukackaStav.OFF);
        //        if (_odvodVyroby.BarcodeActual == string.Empty)
        //        {
        //            // kod je neinicializovany, zepta se obsluhy, jestli ho chce dodatecne zadat rucne
        //            StavVyroba = VyrobaStavy.VlozKod;
        //        }
        //    }
        //}

        #endregion

        #region Rizeni Stavu paletizatoru
        /// <summary>
        /// Pousteni palety z paletizatoru => respektive aktivace vystupu ADAMA
        /// </summary>
        /// <param name="on"></param>
        public void SetPaletizatorState(bool onState)
        {
            Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_SET_PALETIZATOR_STATE, onState.ToString(), "");

            if (adam != null)
                adam.rizeniPaletizatoru(onState);
        }

        #endregion

        #region Smena
        //id prihlaseneho uzivatele (smeny)...
        private string _smenaID = AgroConfig.config.Agro[0].OdvodMimoSmenuID.ToString();
        public string SmenaID
        {
            get { return this._smenaID; }
            set
            {
                this.VyrobaActualDataSaveAndResetActualBarcode(true, Constants.Common.STAV_prihlaseniSmeny, getBarcodeSended(), string.Empty, null);

                this._smenaID = value;

                VynulovaniPromennych();

                // po prihlaseni smeny je prohaz vyply, ale protoze je dotaz na EAN, je stopla i linka
                //adam.rizeniLinky(LinkaStav.LINKA_OFF);
                SetLinkaState(false);
                ProhazZapnut = false;

                // po nastaveni smeny se provede reset counteru na adam
                if (adam != null)
                    adam.ClearCounters();

            }
        }
        #endregion

        #region Event11
        private void Event11_Occured()
        {
            // TODO : 16.5.2019 JiS v AGRO : docasne potlaceno
            //Udalosti.event11(Udalosti.Event11Stav.ADD, 0);
        }
        #endregion

        #region Event36
        private void Event36_Occured()
        {
            // TODO : 16.5.2019 JiS v AGRO : docasne potlaceno
            //Udalosti.event36(Udalosti.Event36Stav.ADD, 0);
        }
        #endregion

        #region Aktualni Stavy odvadeni vyroby

        #region Stavy vyroby
        /// <summary>
        /// Nove nahrazuje pamatovani si jen predchoziho stavu...
        /// - pokud vznikne prechod do jineho stavu, nez je prvni nahore, tak musi dojit k nejakym akcim
        /// ----------
        /// Prace se stackem stavu vyroby ... 
        /// Zaklad je, ze prvni in je Main nad nej se pridavaji stavy,
        /// pokud se z nejakeho stavu prechazi do Main, tak se zrusi vsechny z vrchu az do zakladniho Main
        /// Jsou stavy, ktere se generuji na zaklade udalosti a ty museji prejit do predchoziho stavu, pokud nebyl Main => Event36, Event11, ...
        /// </summary>
        private System.Collections.Generic.Stack<VyrobaStavy> _stavVyrobaStack = new Stack<VyrobaStavy>();
        /// <summary>
        /// Inicializace stacku do Main
        /// </summary>
        private void StavVyrobaStackInitialize()
        {
            _stavVyrobaStack.Clear();
            _stavVyrobaStack.Push(VyrobaStavy.Main);
        }

        /// <summary>
        /// Pokusi se odstranit z vrchu zasobniku stav. Podminkou je, ze stav na vrchu je stejny, ktery chci odebrat
        /// </summary>
        /// <param name="stav"></param>
        /// <returns>Vraci stav, ktery po odebrani zustal na vrchu zasobniku</returns>
        private VyrobaStavy StavVyrobaStackRemoveTop(VyrobaStavy stav)
        {
            if (_stavVyrobaStack.Peek() == stav)
                _stavVyrobaStack.Pop();

            return _stavVyrobaStack.Peek();
        }

        /// <summary>
        /// Vraci aktualni stav na vrchu zasobniku
        /// </summary>
        /// <returns></returns>
        private VyrobaStavy StavVyrobaStackGet()
        {
            if ((_stavVyrobaStack == null) || (_stavVyrobaStack.Count == 0))
                return VyrobaStavy.Main;
            return _stavVyrobaStack.Peek();
        }

        /// <summary>
        /// Nastaveni noveho stavu
        /// </summary>
        /// <param name="novyStav"></param>
        private void StavVyrobaStackSet(VyrobaStavy novyStav)
        {
            //if (!_stavVyrobaStack.Contains(novyStav))
            //{
            //    _stavVyrobaStack.Push(novyStav);
            //}

            if (novyStav == VyrobaStavy.Main)
            { // na main byse mel nastavit jen v pripade, ze je vse vyreseno ... 
                if (!_stavVyrobaStack.Contains(VyrobaStavy.Main))
                { // problem a log error
                    //Exceptions.Handler.ErrorHandle("_stavVyrobaStack prechod do stavu Main, ale Main neexistuje : \n" + this.GetStateInternal(), "StavVyrobaStackSetStav", false);
                    ExceptionHandler2.Handle("_stavVyrobaStack prechod do stavu Main, ale Main neexistuje : \n" + this.GetStateInternal(), "StavVyrobaStackSetStav", false);
                }

                // pokud nastavuji main, tak vse by melo se vratit na zacatek do main a main bude zase prvni...
                StavVyrobaStackInitialize(); // toto vycistni a nastavi main jako prvni...
            }
            else
            {
                // pokud neni main, tak nastavim novy stav
                //, ale pokud nastavuji stav, ktery je nahore, tak ho jen ponecham...
                //, respektive pokud chci nastavit stav, ktery je jiz v zasobniku, tak ho budu ignorovat a zustane aktivni, pouze stavajici stav ... ???

                //if (_stavVyrobaStack.Peek() != novyStav)
                //{
                //    _stavVyrobaStack.Push(novyStav);
                //}
                ////else
                ////{
                ////    ; // nic nedelam, nahore je stav, ktery nastavuji ...
                ////}

                if (!_stavVyrobaStack.Contains(novyStav))
                {
                    _stavVyrobaStack.Push(novyStav);
                }

                //// stack se musi upravit tak, aby novystav byl nahore ... 
                //var oldstackreverted = _stavVyrobaStack.Reverse().ToArray();
                //_stavVyrobaStack.Clear();
                //foreach (var os in oldstackreverted)
                //{
                //    if (os != novyStav)
                //        _stavVyrobaStack.Push(os);
                //}
                //_stavVyrobaStack.Push(novyStav);
            }
        }
        /// <summary>
        /// vraci stack jako string pro zalogovani...
        /// </summary>
        /// <returns></returns>
        private string StavVyrobaStackToString()
        {
            string strStavVyrobaStack = "|";
            if (_stavVyrobaStack != null)
                _stavVyrobaStack.ToList().ForEach(x => strStavVyrobaStack += x.ToString() + "|");
            return strStavVyrobaStack;
        }

        #region JiS : 10.5.2019 - zaveden Stack Stavu Vyroby a tedy rizeni tohoto se meni na stack ...
        //private VyrobaStavy _stavVyrobaPredchozi = VyrobaStavy.Main;
        //public VyrobaStavy StavVyrobaPredchozi
        //{
        //    get
        //    {
        //        return _stavVyrobaPredchozi;
        //    }
        //    private set
        //    {
        //        _stavVyrobaPredchozi = value;
        //    }
        //}
        //private VyrobaStavy _stavVyroba = VyrobaStavy.Main;
        #endregion

        public VyrobaStavy StavVyroba
        {
            get
            {
                //return _stavVyroba;
                return StavVyrobaStackGet();
            }
            set
            {
                //this.StavVyrobaSet(value);

                //Action<VyrobaStavy> action = new Action<VyrobaStavy>(StavVyrobaSet);
                //action.BeginInvoke(value, null, null);
                
                // aby nastavaly udalosti synchronne po sobe jak natanou v hlavnim vlakne ...
                this.MainForm.BeginInvoke((MethodInvoker)delegate()
                {
                    this.StavVyrobaSet(value);
                });
            }
        }

        private void StavVyrobaSet(VyrobaStavy novyStav)
        {    
            try
            {
                // TODO : rizeni stavu vyroby ... X udalosti 11 a 36 ... ??? 
                //_stavVyrobaPredchozi = _stavVyroba; //ulozime si predchozi stav...
                //_stavVyroba = novyStav;
                this.StavVyrobaStackSet(novyStav);

                if (novyStav == this.StavVyroba) // nastaveni na novy stav dojde jen pokud se neco meni ...
                {
                    switch (this.StavVyroba)
                    {
                        case VyrobaStavy.Main:
                            this.StavVyroba_Main(null);
                            break;
                        case VyrobaStavy.LogIDSmena:
                            this.StavVyroba_LogIDSmena(null);
                            break;
                        case VyrobaStavy.LogIDSmenaPotvrzeni:
                            //this.StavVyroba_LogIDSmena
                            break;
                        case VyrobaStavy.LogIDPracovnik:
                            this.StavVyroba_LogIDPracovnik(null);
                            break;
                        case VyrobaStavy.LogIDPracovnikPotvrzeni:
                            //this.StavVyroba_LogIDPracovnik
                            break;
                        case VyrobaStavy.ZadatEAN:
                            this.StavVyroba_ZadatEAN(null);
                            break;
                        case VyrobaStavy.Odhlaseni:
                            this.StavVyroba_Odhlaseni(null);
                            break;
                        case VyrobaStavy.ZmenaProhaz:
                            this.StavVyroba_ZmenaProhaz(null);
                            break;
                        case VyrobaStavy.UlozeniEAN:
                            this.StavVyroba_UlozeniEAN(null);
                            break;
                        case VyrobaStavy.Vyrobek:
                            this.StavVyroba_Vyrobek(null);
                            break;
                        case VyrobaStavy.VlozKod:
                            this.StavVyroba_VlozKod(null);
                            break;
                        case VyrobaStavy.VlozPocet:
                            this.StavVyroba_VlozPocet(null);
                            break;
                        case VyrobaStavy.VlozPocetEAN:
                            this.StavVyroba_VlozPocetEAN(null);
                            break;
                        case VyrobaStavy.SarzeHeslo:
                            this.StavVyroba_SarzeHeslo(null);
                            break;
                        case VyrobaStavy.SarzeHodnota:
                            this.StavVyroba_SarzeHodnota(null);
                            break;
                        case VyrobaStavy.SarzeID:
                            this.StavVyroba_SarzeID(null);
                            break;
                        case VyrobaStavy.Event11:
                            this.StavVyroba_Event11(null);
                            break;
                        case VyrobaStavy.Event36:
                            this.StavVyroba_Event36(null);
                            break;
                        case VyrobaStavy.VPH_Zadani:
                            this.StavVyroba_VPH_Zadani(null);
                            break;
                        case VyrobaStavy.VPP_VlozitKod:
                            this.StavVyroba_VPP_VlozitKod(null);
                            break;
                        case VyrobaStavy.Zombie:
                            this.StavVyroba_Zombie(null);
                            break;
                        case VyrobaStavy.SERVIS:
                            this.StavVyroba_SERVIS(null);
                            break;
                        default:
                            //
                            break;
                    }

                    //// udalost vyvolani zmeny stavu
                    //if (_stavVyroba != _stavVyrobaPredchozi)
                    //    OnZmenaStavu(_stavVyroba);

                    //OnZmenaStavu(novyStav);
                    //OnZmenaStavu(this.StavVyroba); // musi se tam poslat aktualni stav na vrchu zasobniku, protoze se mohou navzajem prekrytvat ... 
                }

                OnZmenaStavu(this.StavVyroba); // musi se tam poslat aktualni stav na vrchu zasobniku, protoze se mohou navzajem prekrytvat ... 
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
               // Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavVyroba_Set", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavVyroba_Set", false);
            }

        }

        #endregion

        /// <summary>
        /// Data v pameti
        /// </summary>
        private Data _vyrobaDataHistoryInMemory = null;
        public Data VyrobaDataHistoryInMemory
        {
            get { return this._vyrobaDataHistoryInMemory; }
        }

        /// <summary>
        /// Aktualni zaznam z pameti, ktery se prave vyrabi(odvadi)
        /// </summary>
        private Data.FASK_EventsRow _aktualDataRow = null;
        /// <summary>
        /// Aktualni radek tabulky z pameti, ktery se aktualne odvadi
        /// </summary>
        public Data.FASK_EventsRow AktualDataRow
        {
            get { return this._aktualDataRow; }
        }

        /// <summary>
        /// Stav odvadeni vyroby
        /// </summary>
        public Odvod _odvodVyroby = new Odvod();

        private bool _showHistory = true;                   // TODO : udelat dalsi stav pro toto ???
        private bool _showMessageBox = true;                // TODO : udelat dalsit stav pro totot
        private bool _repeatInsertSmena = true;             // TODO : udelat dalsit stav pro totot
        private bool _repeatInsertEAN = true;               // TODO : udelat dalsit stav pro totot
        private string _tempEAN = string.Empty;             // TODO : udelat dalsit stav pro totot
        private string _countOdecistKusy = string.Empty;    // TODO : udelat dalsit stav pro totot

        public string _SSCC = string.Empty;

        /// <summary>
        /// Pocet sledovanych sensoru od indexu 0 do sensorCount-1
        /// </summary>
        //private int _sensorsCount = 0;

        /// <summary>
        /// index potvrzovaciho cidla do pole Cidel
        /// </summary>
        //private int _sensorAck = 0; //cislo potvrzovaciho cidla - index

        /// <summary>
        /// Seznam cidel pro hlidani interniho stavu
        /// </summary>
        public List<Cidlo> _cidla_vstupni;
        /// <summary>
        /// Zvedne pocet zaznamenanych zmen cidla sensoru
        /// </summary>
        /// <param name="sensor">index sensoru v poli(listu) od 0</param>
        public void inkrementSensorAcitvatedCount(int sensor)
        {
            //zvedne se jen v pripade, ze je to index sensoru, ktery je v listu, jinak by doslo k vyjimce :(
            //if (sensor < _sensorsCount)
            if (sensor < _cidla_vstupni.Count)
            {
                //_cidla_vstupni[sensor].SensorsCnt++;
                _cidla_vstupni[sensor].IncremetSensorCount();
            }
        }

        /// <summary>
        /// Cidlo scanneru
        /// </summary>
        public CidloScanner _cidlo_scanner;

        /// <summary>
        /// Zvedne pocet zaznamenanych aktivaci scanneru. Pouze Read a NoRead
        /// </summary>
        /// <param name="code"></param>
        public void inkrementScannerAcitvatedCount(Code code)
        {
            // zveda pocet scanu
            //_cidlo_scanner.SensorsCnt++;
            _cidlo_scanner.IncrementScannerReadOrNORead(code);
        }

        #endregion

        // TODO : proverit k cemu toto je ? pamatuje stav linky pred udalosti ... ???
        #region ??? Stav linky BeforeEvent ???
        //private bool _linkaStateBeforeEvent = false;
        //public bool LinkaStateBeforeEvent
        //{
        //    get
        //    {
        //        return _linkaStateBeforeEvent;
        //    }
        //    set
        //    {
        //        _linkaStateBeforeEvent = value;
        //    }
        //}
        #endregion

        #region Testovani jake tlacitko klavesnice bylo aktivovano
        /// <summary>
        /// Test tlacitka, ktere bylo stisknuto a zmena textu v infoactnim textu na novou hodnotu
        /// </summary>
        /// <param name="btn"></param>
        /// <remarks>Maximalni delka vstupu textoveho pole je z konfigurace hodnota : MaxEANLength</remarks>
        internal void NumKeyPressedCheck(Button btn)
        {
            NumKeyPressedCheck(btn, AgroConfig.MaxEANLength);
        }

        internal void NumKeyPressedCheck(Button btn, int maxTextLength)
        {
            // nastavi maximalni delku pro vstup do textoveho pole informacniho panelu
            this.InformationUC.TxtTextMaxLength = maxTextLength;

            if (btn == null)
                return;

            if (btn.Name == "button13")
            {
                if (this._InformationUC.TxtText.Length > 0)
                    this._InformationUC.TxtText = this._InformationUC.TxtText.Substring(0, this._InformationUC.TxtText.Length - 1);
            }
            else if (btn.Name == "button11")
                this._InformationUC.TxtText = "";
            else if (this._InformationUC.TxtText.Length >= maxTextLength)
                return;
            else if (btn.Name == "button12")
                this._InformationUC.TxtText += "0";
            else if (btn.Name == "button1")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button2")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button3")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button4")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button5")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button6")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button7")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button8")
                this._InformationUC.TxtText += btn.Text;
            else if (btn.Name == "button9")
                this._InformationUC.TxtText += btn.Text;

        }
        #endregion

        #region Kliknuti na akcni tlacitko -> reseni zmeny stavu + zmena stavu z jineho stavu

        /// <summary>
        /// Zmena ze stavu LogIDSmena => (LogIDSmena, LogIDPracovnik)
        /// </summary>
        /// <param name="buttonName"></param>
        private void StavVyroba_LogIDSmena(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.LogIDSmena.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Vložte ID směny";
                this.InformationUC.TxtText = (Convert.ToInt32(AgroConfig.config.Agro[0].PosledniPrihlasenaSmena) + 1).ToString();
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.LogIDSmena;

                // nastaveni 
                this._repeatInsertSmena = true;

                // TODO : nastaveni linky, houkacka
                this.SetLinkaState(false);

                // vynulovani pracovniku, protoze se zacina nova smena 
                this.PracovniciClear();

                return;
            }

            if (btn.Name == "btn19")
            {
                this._InformationUC.TxtText = this._smenaID = "";
                this._repeatInsertSmena = true;
                this._InformationUC.LblWarning = "-";
                this._InformationUC.LblText = "Vložte ID směny";
            }
            else if (btn.Name == "BtnEnter")
            {
                #region testovani smeny a opakovane zadani smeny
                if (this._InformationUC.TxtText.Length <= 0)
                {
                    this._InformationUC.LblWarning = "Musíte zadat ID směny!";
                    return;
                }

                if (this._repeatInsertSmena)
                {
                    this._smenaID = this._InformationUC.TxtText;

                    this._InformationUC.TxtText = "";
                    this._InformationUC.LblText = "Zadejte znovu ID směny.";
                    this._repeatInsertSmena = false;
                    return;
                }

                if (this._smenaID != this._InformationUC.TxtText)
                {
                    this._InformationUC.LblWarning = "Směny se neshodují! Opakujte zadání.";
                    return;
                }
                #endregion

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PRIHLASENI_SMENY, this._smenaID, "");

                this.StavVyroba = (DataVyroba.VyrobaStavy.LogIDPracovnik);
            }
        }

        /// <summary>
        /// Zmena ze stavu LogIDPracovnik => (LogIDPracovnik, ZadatEAN)
        /// </summary>
        /// <param name="buttonName"></param>
        private void StavVyroba_LogIDPracovnik(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.LogIDPracovnik.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Vložte ID pracovníka";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.LogIDPracovnik;

                // v tomto stavu je linka vypnuta ... 
                this.SetLinkaState(false);
                return;
            }

            if (btn.Name == "BtnEnter")
            {
                if (this._InformationUC.TxtText.Length <= 0)
                {
                    this._InformationUC.LblWarning = "Musíte zadat ID pracovníka!";
                    return;
                }

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PRIHLASENI_PRACOVNIKA, this._InformationUC.TxtText, "");

                //Ulozeni posledni prihlasene smeny do xml
                AgroConfig.config.Agro[0].PosledniPrihlasenaSmena = this._smenaID;
                AgroConfig.Save();

                this.SmenaID = this._smenaID;
                this.PracovnikSet(this._InformationUC.TxtText);
                try
                {
                    this.SarzeGenerateAndSet();
                }
                catch (Exception exSarzeGenerate)
                {
                    //Log.Write(exSarzeGenerate.Message.ToString());
                    ExceptionHandler2.Handle(exSarzeGenerate);
                    this._InformationUC.LblSarze = "?";
                }
                //this.StavVyroba = (DataVyroba.VyrobaStavy.ZadatEAN);
                this.StavVyroba = (DataVyroba.VyrobaStavy.VPH_Zadani);
            }
            else if (btn.Name == "btn19")
            {
                if (this._InformationUC.TxtText.Length <= 0)
                {
                    this._InformationUC.LblWarning = "Musíte zadat ID pracovníka!";
                    return;
                }

                this.PracovnikSet(this._InformationUC.TxtText);
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PRIHLASENI_PRACOVNIKA, this._InformationUC.TxtText, "");

                this._InformationUC.TxtText = string.Empty;
            }
        }

        /// <summary>
        /// Zmena ze stavu ZadatEAN => (Main, VlozKod)
        /// </summary>
        /// <param name="buttonName"></param>
        private void StavVyroba_ZadatEAN(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.ZadatEAN.ToString(), "");

                // Information uc nastavit

                // 16.5.2019 JiS v AGRO, docasne zruseno F1 kvuli problemu se scannerem ...
                // this.InformationUC.LblText = "Vložte EAN kód.\nF1: Dobrý, F2: Ručně, F3: Nezadávat";
                //this.InformationUC.LblText = "Vložte EAN kód.\n--: -----, F2: Ručně, F3: Nezadávat";
                // 24.7.2019 JiS, obnovena puvodni funkcnost
                this.InformationUC.LblText = "Vložte EAN kód.\nF1: Dobrý, F2: Ručně, F3: Nezadávat";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.ZadatEAN;

                // vypnuti linky
                this.SetLinkaState(false);
                return;
            }

            // 16.5.2019 JiS v AGRO : docasne potlaceno, problem se scannerem 
            // 24.7.2019 JiS, obnovena puvodni funkcnost
            if (btn.Name == "btnF1") // dobry EAN
            { //dobry EAN
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZMENA_VYROBKU, "dobry EAN", this._InformationUC.TxtText);

                //AGRO_HOUKACKA - Zap. Hlidani
                //this.HoukackaRizeni(DataVyroba.HoukackaStav.ZAPNOUT_HLIDANI);

                // zapnuti/vypnuti je v nastaveni stavu Main ...
                //if (!this.ProhazZapnut)
                //    this.SetLinkaState(true); 

                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
            else 
                if (btn.Name == "btnF2") // zadani EANu
            {// EAN zada rucne
                ////AGRO_HOUKACKA - Zap. Hlidani
                //this.HoukackaRizeni(DataVyroba.HoukackaStav.VYPNOUT_HLIDANI);

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_VYZVA_VLOZIT_EAN, "Vyzva zadat EAN rucne", "");

                this.StavVyroba = (DataVyroba.VyrobaStavy.VlozKod);
            }
            else if (btn.Name == "btnF3")   // bez EANu
            {// bez EANu -> bude ho muset zadat pozdeji .. .???

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZMENA_VYROBKU, "bez EANu", Convert.ToString(0));
                
                //AGRO_HOUKACKA - Vyp. Hlidani
                //this.HoukackaRizeni(DataVyroba.HoukackaStav.VYPNOUT_HLIDANI);

                // je to az ve stavu main 
                //if (!this.ProhazZapnut)
                //    this.SetLinkaState(true);

                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
        }

        /// <summary>
        /// Zmena ze stavu Main => (Odhlaseni, ZmenaProhaz, Vyrobek, VlozPocet, SarzeID)
        /// </summary>
        /// <param name="buttonName">null = nastavit tento stav, jinak akce</param>
        private void StavVyroba_Main(Button btn)
        {
            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Main.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Odvod výroby";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.Main;

                // vlastne vsechny stavy mimo Main by meli vypinat linku, protoze to jsou uzivatelske vstupy
                // nastaveni linky, zda je v prohazu, ci nikoli ...
                //this.SetLinkaState(this.ProhazZapnut);
                this.SetLinkaState(true);

                return;
            }

            if (btn.Name == "btnF4")    // odhlaseni
            {
                this.StavVyroba = (DataVyroba.VyrobaStavy.Odhlaseni);
            }
            else if (btn.Name == "btnF3") // zmena prohazovani
            {
                this.StavVyroba = (DataVyroba.VyrobaStavy.ZmenaProhaz);
            }
            else if (btn.Name == "btnF2") // vyrobek
            {
                //Posleni paleta
                //TODO
                this.StavVyroba = (DataVyroba.VyrobaStavy.Vyrobek);

            }
            else if (btn.Name == "btnF1") // vloz kod
            {
                //AGRO_HOUKACKA - OFF
                //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);

                if (this._odvodVyroby.BarcodeActual == string.Empty)
                {// kod je neinicializovany, zepta se obsluhy, jestli ho chce dodatecne zadat rucne
                    this.StavVyroba = (DataVyroba.VyrobaStavy.VlozKod);
                }
            }
            else if (btn.Name == "btnF5") // vloz pocet
            {
                //this.SetLinkaState(false);
                this.StavVyroba = (DataVyroba.VyrobaStavy.VlozPocet);
            }
            else if (btn.Name == "btn19") // zadani sarze
            {
                //Log.Write("Sarze vypni linku.");
                //this.SetLinkaState(false);
                this.StavVyroba = (DataVyroba.VyrobaStavy.SarzeID);
            }
            else if(btn.Name == "btnF6")
            {
                var stav = HoukackaRizeni(HoukackaStav.STAV);

                if(stav == HoukackaStav.ON)
                    HoukackaRizeni(HoukackaStav.OFF);

                else if (stav == HoukackaStav.OFF)
                    HoukackaRizeni(HoukackaStav.ON);

            }
            else if (btn.Name == "BtnEnter")
            {
                this.StavVyroba = DataVyroba.VyrobaStavy.SERVIS;

            }
        }

        /// <summary>
        /// Zmena ze stavu Odhlaseni => (Main, UlozeniEAN, LogIDSmena)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_Odhlaseni(Button btn)
        {
            if (btn == null)
            {

                if (Flag_PosleniPaletaOdhlaseniZadat)
                {
                    int PocPytPal;
                    int PocPalet;
                    string PackType;
                    string PP_Button = "X";
                    var res = PosledniPaleta(out PocPytPal, out PocPalet, out PackType);
                    if (res == DialogResult.No)
                    {

// MERGE - TaD 14.10.2022 

                        ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        //Log.Write("Posledni paleta, DialogResult:" + res.ToString());

                        if(Flag_PO_PredcasnyOdjezd)
                            Flag_PosleniPaleta_je_NP = false;
                        else
                            Flag_PosleniPaleta_je_NP = true;

                        Flag_PO_PredcasnyOdjezd = false;

                       // Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        // ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        // Flag_PosleniPaleta = true;

                        PP_Button = "N";
                        _vp = new VyrPrikaz()
                        {
                            VPH_SOPNUMBER = _vph_sopnumbe,
                            VPP_row_BarcodeP = _vpp_row.BarcodeP,
                            VPP_row_ID = _vpp_row.DEX_ROW_ID,
                            VPP_row_ITEMNMBR = _vpp_row.ITEMNMBR,
                            BarcodeReaded = _odvodVyroby.BarcodeActual,
                            BarcodeSended = getBarcodeSended(),
                            QTY_Pytlu_NP = PocPytPal
                        };

                        // TODO tady sepnout rele na N sekund podle konfigurace
                        OdeslaniPalety();
                    }
                    else if (res == DialogResult.Yes)
                    {

// MERGE - TaD 14.10.2022 

                        // Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                         Flag_PosleniPaleta_je_NP = false;

                    //    // Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                    //     ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                    //     Flag_PosleniPaleta = false;

                        PP_Button = "A";
                    }
                    else
                    {
                        //Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        PP_Button = "F";
                        //throw new Exception("Nesmi nastat!!!");
                    }

                    _SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);

                    if (PackType == "UP")
                    {
                        VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta_ZZ, getBarcodeSended(), 0, 0, null, PocPalet, Constants.Common.STAV_ZaverecnyZaznam, PP_Button, PocPytPal);
                    }
                    else
                    {
                        VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta3, getBarcodeSended(), PocPytPal, 0, 0, PocPalet, PackType, PP_Button, PocPytPal);
                    }


                    VyrobaActualDataSaveAndResetActualBarcode(true, Constants.Common.STAV_odhlaseni_PosledniPaleta, getBarcodeSended(), string.Empty, null);

                    _SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);
                    Flag_PosleniPaletaOdhlaseniZadat = false;
                    this.StavVyroba = this.StavVyroba;
                    return;

                }
                else
                {
                    
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Odhlaseni.ToString(), "");

                    // Information uc nastavit
                    this.InformationUC.LblText = "Opravdu chcete odhlásit směnu?";
                    this.InformationUC.TxtText = string.Empty;
                    this.InformationUC.LblWarning = string.Empty;
                    this.InformationUC.InfoIDCode = (int)VyrobaStavy.Odhlaseni;

                    // pri odhlasovani vypinam linku ...
                    this.SetLinkaState(false);

                    Flag_PosleniPaletaOdhlaseniZadat = true;

                    return;
                }
            }

            if (btn.Name == "btn19")
            {
                //this.SetLinkaState(true);// zapneme linku => pri prechodu do stavu main se linka aktivuje podle stavu prohazovani ...
                //this.StavVyroba = (DataVyroba.VyrobaStavy.Main);

                this._showHistory = true;   //nastavi vyzadovani zobrazeni historie ...

                var predchoziStav = this.StavVyrobaStackRemoveTop(VyrobaStavy.Odhlaseni);
                this.StavVyroba = this.StavVyroba;
            }
            else if (btn.Name == "BtnEnter")
            {
                if ((this._odvodVyroby.BarcodeActual == string.Empty) && (this._odvodVyroby.CodeAllCount > 0))
                {
                    //this.SetLinkaState(false); // linka se vypina pri zmene stavu do ulozeniean
                    
                    //AGRO_HOUKACKA - ON
                    //this.HoukackaRizeni(DataVyroba.HoukackaStav.ON);

                    this.StavVyroba = (DataVyroba.VyrobaStavy.UlozeniEAN);
                }
                else
                {
                    if (this.StavVyroba != DataVyroba.VyrobaStavy.UlozeniEAN)
                    {
                        // ??? 
                        //if (vyroba._list_NoRead_Count > 0)
                        //{   //pokud je nejaky potvrzeny no_Read kod, tak jej ulozime do listu
                        //    vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.odvodVyroby.CodeReadCnt, vyroba._list_NoRead_Count, "sensor", vyroba.odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);
                        //    vyroba._list_NoRead_Count = 0;
                        //}
                    }

                    if (this._showHistory)
                    {
                        this._mainForm.operace_Zmena();
                        this._showHistory = false;
                        return;
                    }

                    this._showHistory = true;

                    // ??? co je totok ???
                    if (this.StavVyroba != DataVyroba.VyrobaStavy.UlozeniEAN)
                    {
                        // userevent odhlaseni smeny 
                        Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ODHLASENI_SMENY, "", "");

                        this.VyrobaActualDataSaveAndResetActualBarcode(true, Constants.Common.STAV_odhlaseni, getBarcodeSended(), string.Empty, null);
                        //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "odhlaseni", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                        //this._odvodVyroby.Reset();
                        //this.dtSaveLast = DateTime.Now;
                        
                        // Odeslani notifikace o konci smeny
                        NotificationMail.SendEmailOdhlaseniSmeny("InformationUC.ButtonClick()|Odhlaseni|btnEnter", this.SmenaID, this.VyrobaDataHistoryInMemory);

                        this.VyrobaSaveAndClearDataHistory();//zaroven ulozi pocet sepnuti cidel a pocet nactenych CK..

                        //AGRO_HOUKACKA - OFF + RESET
                        // houkacka
                        //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);//vypnuti houkacky
                        //this.HoukackaRizeni(DataVyroba.HoukackaStav.RESET);

                        // linka
                        //this.SetLinkaState(false); // vypne linku - az ve stavu main

                        // promenne 
                        this.PocetPruchoduZmenProhaz = 0;

                        // nastavit odvod mimo smenu ...
                        this.SmenaID = AgroConfig.config.Agro[0].OdvodMimoSmenuID.ToString();

                        _mainForm.ClearInfoLabels();

                        // novy stav
                        this.StavVyroba = (DataVyroba.VyrobaStavy.LogIDSmena); // nastavi stav na prihlaseni smeny ...

                    }
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu ZmenaProhaz => (Main, UlozeniEAN)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_ZmenaProhaz(Button btn)
        {
            //TODO MaR 26.6. 2023 - metoda obsluha tlacitka prohazovani, nastaveni globalni promenne ZmenaProhaz

            //TODO MaR 26.6. 2023 - prvotni inicializace tlacitek pro prohazovani

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.ZmenaProhaz.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Opravdu chcete " + (this.ProhazZapnut ? "vypnout" : "zapnout") + " prohaz?";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.ZmenaProhaz;

                //vypnuti linky pri zmene do/z prohazovani ...
                this.SetLinkaState(false);
                return;
            }

            if (btn.Name == "btn19")
            {
                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
            else if (btn.Name == "BtnEnter")
            {
                if (this.PocetPruchoduZmenProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
                {
                    //AGRO_HOUKACKA - OFF
                    //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
                    this.PocetPruchoduZmenProhaz = 0;
                }

                if (this._odvodVyroby.BarcodeActual == string.Empty && (this._odvodVyroby.CodeAllCount > 0))
                {
                    if (this.StavVyroba != DataVyroba.VyrobaStavy.UlozeniEAN)
                    {

                        // this.SetLinkaState(false); => ve stavu ulozeniean se linka vypina ...
                        //AGRO_HOUKACKA - ON
                        //this.HoukackaRizeni(DataVyroba.HoukackaStav.ON);
                        this.StavVyroba = (DataVyroba.VyrobaStavy.UlozeniEAN);
                    }
                }
                else
                {

                    //TODO MaR 11.7. 2023 ulozeni do lokalni databaze //zakomentovano
                    //this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_zmenaProhaz, getBarcodeSended(), string.Empty, null);
                    //TODO MaR 11.7. 2023 ulozeni do lokalni databaze ze je rezim prohazovani

                    //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "zmena prohaz", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //this._odvodVyroby.Reset();
                    //this.dtSaveLast = DateTime.Now;

                    if (!this.ProhazZapnut)
                    {// prohazovani
                        //this.SetLinkaState(false); //az ve stavu main
                        this.ProhazZapnut = true;

                        //TODO MaR 11.7. 2023 pridani tagu zda je rezim prohazovani
                        _odvodVyroby.CodeProhazZapnut(this.ProhazZapnut);
                        //TODO MaR 11.7. 2023 pridani ulozeni do lokalni databaze zda je rezim prohazovani
                        this.VyrobaActualDataSaveAndResetActualBarcode(Constants.Common.STAV_zmenaProhazON, this._vyroba2_Scan.ReadBarcode, true);

                        Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZAPNOUT_PROHAZ, "", "");
                    }
                    else
                    {

                        //this.SetLinkaState(true); //az ve stavu main
                        this.ProhazZapnut = false;


                        //TODO MaR 11.7. 2023 pridani ulozeni do lokalni databaze zda je rezim prohazovani
                        this.VyrobaActualDataSaveAndResetActualBarcode(Constants.Common.STAV_zmenaProhazOFF, this._vyroba2_Scan.ReadBarcode, true);

                        Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_VYPNOUT_PROHAZ, "", "");
                    }

                    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu UlozeniEAN => (UlozeniEAN, Main, LogIDSmena, ZadatEAN)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_UlozeniEAN(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.UlozeniEAN.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "!!! Vložte EAN kód !!!";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.UlozeniEAN;

                // inicializace lokalnich promennych
                this._tempEAN = string.Empty;
                this._repeatInsertEAN = true;

                // vypinam linku pri zde ...
                this.SetLinkaState(false); 

                return;
            }

            if (btn.Name == "BtnEnter")
            {
                if (this.InformationUC.TxtText.Length == AgroConfig.MaxEANLength && this.InformationUC.TxtText.ToCharArray().All(c => char.IsDigit(c)))
                {
                    if (this._repeatInsertEAN)
                    {
                        this._tempEAN = this.InformationUC.TxtText;
                        this.InformationUC.LblText = "Opakujte zadání EAN kódu.";
                        this._repeatInsertEAN = false;
                        this.InformationUC.TxtText = "";
                        return;
                    }

                    if (this._tempEAN != this.InformationUC.TxtText)
                    {
                        this.InformationUC.LblText = "!!! Vložte EAN kód !!!";
                        this.InformationUC.TxtText = "";
                        this.InformationUC.LblWarning = "Zadané kódy se neshodují!\nOpakujte jejich zadání!";
                        this._repeatInsertEAN = true;
                        return;
                    }


                    this._odvodVyroby.BarcodeActual = this.InformationUC.TxtText;
                    if ((_aktualDataRow != null) && (String.IsNullOrEmpty(_aktualDataRow.barcodeReaded)))
                        _aktualDataRow.barcodeReaded = _odvodVyroby.BarcodeActual; // pokud neni nastaven carovy kod, tak se nastavi ...
                    _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_rucni, _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                    //AGRO_HOUKACKA - OFF
                    //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);

                    // zde koncim tento stav a prechazim do predchoziho...
                    VyrobaStavy stavVyrobaPredchozi = StavVyrobaStackRemoveTop(VyrobaStavy.UlozeniEAN);
                    this.StavVyroba = this.StavVyroba;
                    // zde by jiz nemelo nic nasledovat ...

                    #region Old bad code, protoze duplikuje funkcnost jinych stavu ...
                    //if (stavVyrobaPredchozi == DataVyroba.VyrobaStavy.Odhlaseni)
                    //{
                    //    if (this._showHistory)
                    //    {
                    //        this._mainForm.operace_Zmena();
                    //        this._showHistory = false;
                    //        return;
                    //    }

                    //    this._showHistory = true;

                    //    Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ODHLASENI_SMENY, "", "");

                    //    this.VyrobaActualDataSaveAndResetActualBarcode("odhlaseni", "");
                    //    //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "odhlaseni", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //    //this._odvodVyroby.Reset();
                    //    //this.dtSaveLast = DateTime.Now;

                    //    // Odeslani notifikace o konci smeny
                    //    NotificationMail.SendEmailOdhlaseniSmeny("InformationUC.ButtonClick()|UlozeniEAN|btnEnter", this.SmenaID, this.VyrobaDataHistoryInMemory);

                    //    this.VyrobaSaveAndClearDataHistory();//zaroven ulozi pocet sepnuti cidel a pocet nactenych CK..

                    //    //this.SetLinkaState(false); //vypne linku ...

                    //    this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);   //vypnuti houkacky
                    //    this.HoukackaRizeni(DataVyroba.HoukackaStav.RESET); //

                    //    this.PocetPruchoduZmenProhaz = 0;

                    //    this.StavVyroba = (DataVyroba.VyrobaStavy.LogIDSmena); // nastavi na prihlaseni smeny
                    //}
                    //else if (stavVyrobaPredchozi == DataVyroba.VyrobaStavy.ZmenaProhaz)
                    //{
                    //    this.InformationUC.LblText = "Opravdu chcete " + (this.ProhazZapnut ? "vypnout" : "zapnout") + " prohaz?";

                    //    if (!this.ProhazZapnut)
                    //    {// prohazovani
                    //        //this.SetLinkaState(false);
                    //        this.ProhazZapnut = true;

                    //        Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZAPNOUT_PROHAZ, "", "");
                    //    }
                    //    else
                    //    {
                    //        //this.SetLinkaState(true);
                    //        this.ProhazZapnut = false;

                    //        Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_VYPNOUT_PROHAZ, "", "");
                    //    }

                    //    //vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.odvodVyroby.CodeReadCnt, vyroba._list_NoRead_Count, "zmena prohazu - ean", vyroba.odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);

                    //    this.VyrobaActualDataSaveAndNoResetActualBarcode("zmena prohaz", "");
                    //    //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "zmena prohaz", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //    //this._odvodVyroby.Reset();
                    //    //this.dtSaveLast = DateTime.Now;

                    //    //vyroba._list_NoRead_Count = 0;

                    //    this.StavVyroba = (DataVyroba.VyrobaStavy.Main); //=> prechod na hlavni menu
                    //}
                    //else if (stavVyrobaPredchozi == DataVyroba.VyrobaStavy.Vyrobek)
                    //{
                    //    this.VyrobaActualDataSaveAndResetActualBarcode("zmena vyrobek", "");
                    //    //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "zmena vyrobek", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //    //this._odvodVyroby.Reset();
                    //    //this.dtSaveLast = DateTime.Now;

                    //    this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
                    //    this.HoukackaRizeni(DataVyroba.HoukackaStav.RESET);

                    //    //this.SetLinkaState(false);

                    //    this.PocetPruchoduZmenProhaz = 0;

                    //    this.StavVyroba = (DataVyroba.VyrobaStavy.ZadatEAN);
                    //}
                    //else
                    //{
                    //    //vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.odvodVyroby.CodeReadCnt, vyroba._list_NoRead_Count, "zmena vyrobek - else", vyroba.odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);
                    //    this.VyrobaActualDataSaveAndNoResetActualBarcode("zmena vyrobek - else", "");
                    //    //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "zmena vyrobek - else", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //    //this._odvodVyroby.Reset();
                    //    //this.dtSaveLast = DateTime.Now;

                    //    // TODO : rizeni houkazky

                    //    //this.SetLinkaState(true);

                    //    //vyroba._list_NoRead_Count = 0;
                    //    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                    //}
                    ////this._odvodVyroby.Reset();
                    ////this.StavVyrobaPredchozi = DataVyroba.VyrobaStavy.Main;
                    ////this.dtSaveLast = DateTime.Now;
                    #endregion
                }
                else
                    this.InformationUC.LblWarning = "Špatný EAN kód!\nOpakujte zadání.";
            }
        }

        /// <summary>
        /// Zmena ze stavu Vyrobek => (Main, UlozeniEAN, ZadatEAN)
        /// </summary>
        /// <param name="btn"></param>
        //private void StavVyroba_Vyrobek(Button btn)
        //{
        //    if (btn == null)
        //    {
        //        Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Vyrobek.ToString(), "");

        //        // Information uc nastavit
        //        this.InformationUC.LblText = "Chcete změnit výrobek?";
        //        this.InformationUC.TxtText = string.Empty;
        //        this.InformationUC.LblWarning = string.Empty;
        //        this.InformationUC.InfoIDCode = (int)VyrobaStavy.Vyrobek;

        //        //vypnuti linky
        //        this.SetLinkaState(false);
        //        return;
        //    }

        //    if (btn.Name == "btn19")
        //    {
        //        this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
        //    }
        //    else if (btn.Name == "BtnEnter")
        //    {
        //        if ((this._odvodVyroby.BarcodeActual == string.Empty) && (this._odvodVyroby.CodeAllCount > 0))
        //        {
        //            //this.SetLinkaState(false);
        //            //AGRO_HOUKACKA - ON
        //            //this.HoukackaRizeni(DataVyroba.HoukackaStav.ON);
        //            this.StavVyroba = (DataVyroba.VyrobaStavy.UlozeniEAN);
        //        }
        //        else
        //        {

        //            decimal? PocPytPal = null;
        //            decimal? PocPalet = null;

        //            using (frmPosledniPaleta frm = new frmPosledniPaleta())
        //            {
        //                frm.EAN = _vpp_row.BarcodeP;
        //                frm.ITEMDESC = _vpp_row.ITEMDESC;
        //                frm.VPP_pol = _vpp_row.ITEMNMBR;
        //                frm.VPH_SOPNUMBE = _vph_sopnumbe;

        //                frm.WindowState = FormWindowState.Maximized;

        //                var dr = frm.ShowDialog();

        //                if (dr == DialogResult.OK)
        //                {
        //                    PocPytPal = frm.QTY_PytluNaPalete;
        //                    PocPalet = frm.QTY_Palet;
        //                }
        //                else
        //                    return;

        //            }

        //            if (PocPytPal.HasValue)
        //            {
        //                VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta, getBarcodeSended(), PocPytPal.Value, 0, (int)PocPalet);
        //                Flag_PosleniPaleta = true;
        //            }
        //            else
        //                return;


        //            //listData = save_queue_data(listData, Code, &CodeReadCnt, &CodeNoReadCnt, rizeni_prohazu(PROHAZ_STAV), &id_zaznam, pfentry.id);
        //            this.VyrobaActualDataSaveAndResetActualBarcode(true, Constants.Common.STAV_Vyrobek_zmenaVyrobku, getBarcodeSended());
        //            //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "zmena vyrobku", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
        //            //this._odvodVyroby.Reset();
        //            //this.dtSaveLast = DateTime.Now;

        //            this.PocetPruchoduZmenProhaz = 0;
        //            //this.SetLinkaState(false);

        //            //AGRO_HOUKACKA - OFF + RESET
        //            //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
        //            //this.HoukackaRizeni(DataVyroba.HoukackaStav.RESET);

        //            this.StavVyroba = (DataVyroba.VyrobaStavy.VPH_Zadani);
        //        }
        //    }
        //}

        /// <summary>
        /// Zmena ze stavu Vyrobek => (Main, UlozeniEAN, ZadatEAN)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_Vyrobek(Button btn)
        {
            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Vyrobek.ToString(), "");

                //vypnuti linky
                this.SetLinkaState(false);

                if ((this._odvodVyroby.BarcodeActual == string.Empty) && (this._odvodVyroby.CodeAllCount > 0))
                {
                    //this.SetLinkaState(false);
                    //AGRO_HOUKACKA - ON
                    //this.HoukackaRizeni(DataVyroba.HoukackaStav.ON);
                    this.StavVyroba = (DataVyroba.VyrobaStavy.UlozeniEAN);
                }
                else
                {
                    int PocPytPal;
                    int PocPalet;
                    string PackType;
                    string PP_Button = "X";
                    var res = PosledniPaleta(out PocPytPal, out PocPalet, out PackType);

                    if (res == DialogResult.No)
                    {

// MERGE - TaD 14.10.2022 

                        // ked je NP
                        //Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        
                        if (Flag_PO_PredcasnyOdjezd)
                            Flag_PosleniPaleta_je_NP = false;
                        else
                            Flag_PosleniPaleta_je_NP = true;

                        Flag_PO_PredcasnyOdjezd = false;
                        PP_Button = "N";

                        //Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        // ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        // Flag_PosleniPaleta = true;
                        //  PP_Button = "N";


                        _vp = new VyrPrikaz()
                        {
                            VPH_SOPNUMBER = _vph_sopnumbe,
                            VPP_row_BarcodeP = _vpp_row.BarcodeP,
                            VPP_row_ID = _vpp_row.DEX_ROW_ID,
                            VPP_row_ITEMNMBR = _vpp_row.ITEMNMBR,
                            BarcodeReaded = _odvodVyroby.BarcodeActual,
                            BarcodeSended = getBarcodeSended(),
                            QTY_Pytlu_NP = PocPytPal
                        };

                        // TODO tady sepnout rele na N sekund podle konfigurace
                        OdeslaniPalety();
                    }
                    else if (res == DialogResult.Yes)
                    {


// MERGE - TaD 14.10.2022 

                        // ked je UP
                        //Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        Flag_PosleniPaleta_je_NP = false;

                        // //Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        // ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        // Flag_PosleniPaleta = false;

                         PP_Button = "A";
                    }
                    else
                    {
                        //Log.Write("Posledni paleta, DialogResult:" + res.ToString());
                        ExceptionHandler2.Handle("Posledni paleta, DialogResult:" + res.ToString(), "Log_Vyroba_Agro_Modbus_2", "txt");
                        //throw new Exception("Nesmi nastat!!!");
                        PP_Button = "F";
                    }

                    _SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);

                    if (PackType == "UP")
                        VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta_ZZ, getBarcodeSended(), 0, 0, null, PocPalet, Constants.Common.STAV_ZaverecnyZaznam, PP_Button, PocPytPal);
                    else
                        VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta1, getBarcodeSended(), PocPytPal, 0,0, PocPalet, PackType, PP_Button, PocPytPal);


                    //VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta1, getBarcodeSended(), PocPytPal, 0, PocPalet, PackType);

                   
                    //Flag_PosleniPaletaOdhlaseniZadat = false;
                    VyrobaActualDataSaveAndResetActualBarcode(true, Constants.Common.STAV_Vyrobek_zmenaVyrobku, getBarcodeSended(), string.Empty, null);

                    _SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);
                    this.PocetPruchoduZmenProhaz = 0;
                    this.StavVyroba = (VyrobaStavy.VPH_Zadani); 
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu VlozKod => (ZadatEAN, Main, )
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_VlozKod(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.VlozKod.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Zadejte kód ručně.";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.VlozKod;

                this._tempEAN = string.Empty;
                this._repeatInsertEAN = true;

                //vypnuti linky
                this.SetLinkaState(false);
                return;
            }

            if (btn.Name == "btn19")
            {
                // rusim akci, vracim se zpet do predchoziho stavu ... 
                VyrobaStavy stavVyrobaPredchozi = StavVyrobaStackRemoveTop(VyrobaStavy.VlozKod);
                this.StavVyroba = this.StavVyroba;
                //zde by dal nemel pokracovat ... 

                #region Old bad code, protoze duplikuje funkcnost jinych stavu ...
                //if (stavVyrobaPredchozi == DataVyroba.VyrobaStavy.ZadatEAN)
                //{
                //    this.StavVyroba = (DataVyroba.VyrobaStavy.ZadatEAN);
                //}
                //else
                //    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                #endregion
            }
            else if (btn.Name == "BtnEnter")
            {
                //this.SetLinkaState(false); // vypne linku

                if (this.InformationUC.TxtText.Length != AgroConfig.MaxEANLength)
                {
                    this.InformationUC.LblWarning = "Špatný kód! Opakujte zadání\n a potvrďte jej stiskem 'OK'!";
                }
                else
                {
                    if (this._repeatInsertEAN)
                    {
                        this._tempEAN = this.InformationUC.TxtText;
                        this.InformationUC.LblText = "Opakujte zadání EAN kódu.";
                        this.InformationUC.TxtText = string.Empty;
                        this._repeatInsertEAN = false;
                        return;
                    }

                    if (this._tempEAN != this.InformationUC.TxtText)
                    {
                        this.InformationUC.LblText = "Zadejte kód.";
                        this.InformationUC.TxtText = "";
                        this.InformationUC.LblWarning = "Zadané kódy se neshodují! Opakujte jejich zadání!";
                        this._repeatInsertEAN = true;
                        return;
                    }


                    //if (vyroba.BarcodeActual == string.Empty)
                    //    vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", 0, vyroba.CodeNoReadCnt, "vlozen kod", txtText.Text, "", "", "", "", "", "", "", "", "", "");
                    //Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), DateTime.Now, 0, vyroba.CodeNoReadCnt, "vlozen kod", txtText.Text, "", "", "", "", DateTime.Now, "", "", "", "", "", "");

                    this._odvodVyroby.BarcodeActual = this.InformationUC.TxtText;
                    if ((_aktualDataRow != null) && (String.IsNullOrEmpty(_aktualDataRow.barcodeReaded)))
                        _aktualDataRow.barcodeReaded = _odvodVyroby.BarcodeActual; // pokud neni nastaven carovy kod, tak se nastavi ...
                    _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_rucni, _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);

                    VyrobaStavy stavVyrobaPredchozi = StavVyrobaStackRemoveTop(VyrobaStavy.VlozKod);
                    this.StavVyroba = VyrobaStavy.Main;
                    // provedlo se vsechno co melo, pokracuji do main ... 

                    #region Old code
                    //if (stavVyrobaPredchozi == DataVyroba.VyrobaStavy.ZadatEAN)
                    //{
                    //    Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZMENA_VYROBKU, this._odvodVyroby.BarcodeActual, "");

                    //    //this._aktualDataRow = this.InsertDataToDataset(this.SmenaID, "", 0, 0, "vlozen kod", this._odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", this._sarze, 0, 0, 0);                        

                    //    // TODO : rizeni houkacka

                    //    //if (!this.ProhazZapnut)
                    //    //    this.SetLinkaState(true);

                    //    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                    //}
                    //else
                    //{
                    //    //if (!this.ProhazZapnut)
                    //    //    this.SetLinkaState(true);


                    //    //this._aktualDataRow = this.InsertDataToDataset(this.SmenaID, "", 0, this._odvodVyroby.CodeNoReadCnt, "vlozen kod", this._odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", this._sarze, 0, 0, 0);
                    //    //this._aktualDataRow = this.InsertDataToDataset(this.SmenaID, "", 0, 0, "vlozen kod", this._odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", this._sarze, 0, 0, 0);

                    //    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                    //}
                    #endregion
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu VlozPocet => (Main)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_VlozPocet(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.VlozPocet.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Zadejte počet kusů.";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.VlozPocet;

                //vypnuti linky
                this.SetLinkaState(false);

                return;
            }

            if (btn.Name == "BtnEnter")
            {
                //this.SetLinkaState(true);
                this._showMessageBox = true;
                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
            else if (btn.Name == "btnF1")
            {
                if (this._odvodVyroby.BarcodeActual == string.Empty)
                {
                    //this.SetLinkaState(true);
                    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                    return;
                }

                if (this.InformationUC.TxtText.Length <= 0)
                {
                    this.InformationUC.LblText = "Napište počet kusů!";
                }
                else
                {
                    if (this._showMessageBox)
                    {
                        this.InformationUC.LblText = "Opravdu chcete odečíst " + this.InformationUC.TxtText + " kusů\n od kódu " + this._odvodVyroby.BarcodeActual + "?";
                        this._showMessageBox = false;
                        this.KeyboardUC.KeyboardText.BtnF1 = "Ano";
                        this.KeyboardUC.KeyboardText.BtnF2 = "-";
                        return;
                    }

                    var result = this.InsertMinusQtyToDataset(this.SmenaID, decimal.Parse(this.InformationUC.TxtText), 0, this._odvodVyroby.BarcodeActual, this._sarze);
                    if (result == null)
                    {
                        this.InformationUC.LblWarning = "Nelze odečíst zadané kusy!\nKód ještě nebyl načten!";
                        this.InformationUC.TxtText = "";
                        this._showMessageBox = true;
                        return;
                    }

                    decimal odecet = decimal.Parse(this.InformationUC.TxtText);
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_EAN_MINUS_COUNT, this.InformationUC.TxtText, "");
                    Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        -odecet,                                                // QTY 
                        0,                                                      // QTYREAL
                        Constants.Common.STAV_odecet,                           // description 
                        this._odvodVyroby.BarcodeActual,                        // BarcodeReaded 
                        getBarcodeSended(),                                     // BarcodeSended 
                        "",                                                     // Zakazka
                        this.ProhazZapnut.ToString(),                           // popis 
                        "",                                                     //reporttype 
                        "",                                                     //IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        "",                                                     // sensor 
                        this._sarze,                                            // material
                        _vph_sopnumbe,                                          // VPH
                        _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID ,           // VPP
                        _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                        _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                        _SSCC,                                                  // NMBRPAL
                        null,                                                   // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        "",                                                     // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                        );
                    // ulozeni ihned po odectu...
                    this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_ulozeniPoOdectu, getBarcodeSended(), string.Empty, null);

                    //this.SetLinkaState(true);
                    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                }
            }
            else if (btn.Name == "btnF2")
            {
                if (this.InformationUC.TxtText.Length <= 0)
                {
                    this.InformationUC.LblText = "Napište počet kusů!";
                }
                else
                {
                    this._countOdecistKusy = this.InformationUC.TxtText;

                    this.StavVyroba = (DataVyroba.VyrobaStavy.VlozPocetEAN);
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu VlozPocetEAN => (Main, VlozPocetEAN)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_VlozPocetEAN(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.VlozPocetEAN.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Vložte EAN kód!";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.VlozPocetEAN;

                //vypnuti linky
                this.SetLinkaState(false);

                return;
            }

            if (btn.Name == "btn19")
            {
                //this.SetLinkaState(true);
                this._showMessageBox = true;
                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
            else if (btn.Name == "BtnEnter")
            {

                if (this.InformationUC.TxtText.Length != AgroConfig.MaxEANLength)
                {
                    this.InformationUC.LblText = "Špatný EAN kód!";
                }
                else
                {
                    if (this._showMessageBox)
                    {
                        this.InformationUC.LblText = "Opravdu chcete odečíst " + this._countOdecistKusy + " kusů\nod kódu " + this.InformationUC.TxtText + "?";
                        this.InformationUC.LblWarning = "-";
                        this._showMessageBox = false;
                        return;
                    }

                    var result = this.InsertMinusQtyToDataset(this.SmenaID, decimal.Parse(this._countOdecistKusy), 0, this.InformationUC.TxtText, this._sarze);
                    if (result == null)
                    {
                        this.InformationUC.LblWarning = "Zadaný kód nebyl nalezen!\nOpakujte zadání kódu.";
                        this.InformationUC.TxtText = "";
                        this._showMessageBox = true;
                        return;
                    }

                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_EAN_MINUS_COUNT, this._countOdecistKusy, "");
                    Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        -decimal.Parse(this._countOdecistKusy),                 // QTY 
                        0,                                                      // QTYREAL
                        Constants.Common.STAV_odecetKusy,                       // description 
                        this.InformationUC.TxtText,                             // BarcodeReaded 
                        getBarcodeSended(this.InformationUC.TxtText),           // BarcodeSended 
                        "",                                                     // Zakazka
                        this.ProhazZapnut.ToString(),                           // popis 
                        "",                                                     //reporttype 
                        "",                                                     //IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        "",                                                     // sensor 
                        this._sarze,                                            // material
                        _vph_sopnumbe,                                          // VPH
                        _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                        _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                        _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                        _SSCC,                                                  // NMBRPAL
                        null,                                                   // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        "",                                                     // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                        );

                    //ulozeni ihned po odectu...
                    this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_ulozeniPoOdectuEAN, getBarcodeSended(), string.Empty, null);


                    //this.SetLinkaState(true);
                    this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu SarzeID => (Main, SarzeHeslo)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_SarzeID(Button btn)
        {
            NumKeyPressedCheck(btn, sarzehodnotamaxlength);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.SarzeID.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Zadejte ID ke změně šarže.";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.SarzeID;

                // vypnuti linky
                this.SetLinkaState(false);

                return;
            }

            if (btn.Name == "btn19")
            { // zpet (storno)
                //zalogovat pokus o zmenu sarze
                //Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_SARZE_ZMENA_HESLO, txtText.Text,"");
                //Log.Write("Sarze zapni linku do main");
                //this.SetLinkaState(true);
                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
            else if (btn.Name.ToUpper() == "BtnEnter".ToUpper())
            {

                //zalogovani LOG_SARZE_ZMENA_ID ale neexistuje
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_SARZE_ZMENA_ID, this.InformationUC.TxtText, "");

                if (!this.ReturnID(this.InformationUC.TxtText))
                {
                    //zalogovat ze se ID neshoduje
                    this.InformationUC.LblWarning = "ID se neshoduje...";
                }
                else
                {
                    this.InformationUC.TxtTextPasswordChar = '*';
                    this._loginNaZmenuSarze = this.InformationUC.TxtText;
                    this.StavVyroba = (DataVyroba.VyrobaStavy.SarzeHeslo);
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu SarzeHeslo => (SarzeID, SarzeHodnota)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_SarzeHeslo(Button btn)
        {
            NumKeyPressedCheck(btn, sarzehodnotamaxlength);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.SarzeHeslo.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Zadejte heslo ke změně šarže.";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.SarzeHeslo;

                //vypinam linku
                this.SetLinkaState(false);

                return;
            }

            if (btn.Name == "btn19")
            { // zpet (storno)
                //Log.Write("Sarze vypni linku do sarzeID");
                //this.SetLinkaState(false);
                this.InformationUC.TxtTextPasswordChar = '\0';
                this.StavVyroba = (DataVyroba.VyrobaStavy.SarzeID);
            }
            else if (btn.Name.ToUpper() == "BtnEnter".ToUpper())
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_SARZE_ZMENA_HESLO, this.InformationUC.TxtText, "");

                if (!this.ReturnHeslo(this.InformationUC.TxtText, this._loginNaZmenuSarze))
                {
                    this.InformationUC.LblWarning = "Heslo se neshoduje...";
                }
                else
                {
                    this._loginNaZmenuSarze = string.Empty;
                    this.InformationUC.TxtTextPasswordChar = '\0';
                    this.StavVyroba = (DataVyroba.VyrobaStavy.SarzeHodnota);
                }
            }
        }

        /// <summary>
        /// Zmena ze stavu SarzeHodnota => (Main, SarzeHodnota[Generate])
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_SarzeHodnota(Button btn)
        {
            NumKeyPressedCheck(btn, sarzehodnotamaxlength);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.SarzeHodnota.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Zadejte šarži.";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.SarzeHodnota;

                // vypnuti linky
                this.SetLinkaState(false);
                
                return;
            }

            if (btn.Name == "btn19")
            { // zpet (storno)
                //Log.Write("Sarze zapni linku do main");
                //this.SetLinkaState(true);
                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
            else if (btn.Name.ToUpper() == this.KeyboardUC.btnF1.Name.ToUpper())
            {
                OSK.Toggle();
                this.InformationUC.TxtTextFocus();
            }
            else if (btn.Name.ToUpper() == "BtnF5".ToUpper())
            { // Generovat
                this.InformationUC.TxtText = this.SarzeGenerate();
            }
            else if (btn.Name == "BtnEnter")
            {
                // zalogovala sa zmena sarze, ulozit hodnotu ??kam??
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_SARZE_ZMENA_HODNOTA, this.InformationUC.TxtText, ""); // dodat rez1 aktualnz pracovnik

                //vyroba.SetLinkaState(true); // zapne sa na konci

                #region po vytvoreni novej sarze, odeslat stara data ze starou sarzi pric

                //Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ODHLASENI_SMENY, "", "");
                this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_zmenaSarze, getBarcodeSended(), string.Empty, null);
                //Classes.Database.InsertNewEvents(this.SmenaID, this._odvodVyroby.CodeReadCntGetAndReset(), this._odvodVyroby.CodeNoReadCntGetAndReset(), "zmena sarze", this._odvodVyroby.BarcodeActual, "", "", this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                //this._odvodVyroby.Reset();
                //this.dtSaveLast = DateTime.Now;

                this.SarzeSet(this.InformationUC.TxtText);
                //Log.Write("Sarze zmenena, zapni linku do main");
                //this.SetLinkaState(true); // vypne linku 
                #endregion

                this.StavVyroba = (DataVyroba.VyrobaStavy.Main);
            }
        }

        /// <summary>
        /// Zmena ze stavu Event36 => (Main)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_Event36(Button btn)
        {
            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Event36.ToString(), "");
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_EVENT36_OCURED, Udalosti.event36(Udalosti.Event36Stav.COUNT, 0).ToString(), Udalosti.event36(Udalosti.Event36Stav.GET_TIME, 0).ToString());

                // Information uc nastavit
                this.InformationUC.LblText = "Událost 36: čidlo";
                this.InformationUC.LblWarning = "Scan bez dat! Scanner neposlal ČK.\n" + Udalosti.event36(Udalosti.Event36Stav.COUNT, 0) + "/" + Udalosti.event36(Udalosti.Event36Stav.GET_TIME, 0);
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.Event36;

                //vypnuti linky
                this.SetLinkaState(false);

                //AGRO_HOUKACKA - ON
                //houkacka
                //HoukackaRizeni(HoukackaStav.ON);

                return;
            }

            if (btn.Name == "BtnEnter")
            {
                //this.SetLinkaState(this.LinkaStateBeforeEvent);
                //AGRO_HOUKACKA - OFF
                //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
                Udalosti.event36(Udalosti.Event36Stav.RESET, 0);

                // vraci stav do posledniho pred touto udalosti ...
                this.StavVyrobaStackRemoveTop(VyrobaStavy.Event36);
                this.StavVyroba = this.StavVyroba;

                //// vraci stav do hlavniho procesu ...
                //this.StavVyroba = VyrobaStavy.Main;

            }
        }

        /// <summary>
        /// Zmena ze stavu Event11 => (Main)
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_Event11(Button btn)
        {
            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Event11.ToString(), "");
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_EVENT11_OCURED, Udalosti.event11(Udalosti.Event11Stav.COUNT, 0).ToString(), Udalosti.event11(Udalosti.Event11Stav.GET_TIME, 0).ToString());

                // Information uc nastavit
                this.InformationUC.LblText = "Událost 11: scanner";
                this.InformationUC.LblWarning = "Více nepotvrzených! Čidlo nepotvrdilo ČK.\n" + Udalosti.event11(Udalosti.Event11Stav.COUNT, 0) + "/" + Udalosti.event11(Udalosti.Event11Stav.GET_TIME, 0);
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.Event11;

                // vypnuti linky
                this.SetLinkaState(false);
                //AGRO_HOUKACKA - ON
                // rizeni houkacky
                //HoukackaRizeni(HoukackaStav.ON);		//zapne houkacku

                return;
            }

            if (btn.Name == "BtnEnter")
            {


                //this.SetLinkaState(this.LinkaStateBeforeEvent);
                //AGRO_HOUKACKA - OFF
                //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
                Udalosti.event11(Udalosti.Event11Stav.RESET, 0);

                // vraci stav do posledniho pred touto udalosti ...
                this.StavVyrobaStackRemoveTop(VyrobaStavy.Event11);
                this.StavVyroba = this.StavVyroba;
                
                ////Vraci stav do hlavniho procesu ... 
                //this.StavVyroba = VyrobaStavy.Main;
            }
        }

        /// <summary>
        /// Zadaní čísla vyrobniho příkazu
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_VPH_Zadani(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.VPH_Zadani.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Vložte číslo výrobního příkazu";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.VPH_Zadani;

                //TODO MaR 27.10.2023 vypnutí režim prohazování
                //----------------------------------------
                if (this.ProhazZapnut)
                    this.VyrobaActualDataSaveAndResetActualBarcode(Constants.Common.STAV_zmenaProhazOFF, this._vyroba2_Scan.ReadBarcode, true);


                this.ProhazZapnut = false;
                _odvodVyroby.CodeProhazCntGetAndReset();
                _odvodVyroby.CodeProhazZapnutAndReset();
                //----------------------------------------

                this.InformationUC.TxtTextFocus();

                // v tomto stavu je linka vypnuta ... 
                this.SetLinkaState(false);
                return;
            }

            if (btn.Name == "BtnEnter")
            {
                if (this._InformationUC.TxtText.Length <= 0)
                {
                    this._InformationUC.LblWarning = "Musíte zadat číslo výrobního příkazu!";
                    return;
                }

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZADANI_VPH, this._InformationUC.TxtText, "");

                //Zde je zadano číslo vyrobiho přizak, a muže se  s nim pracovat
                var SOPNUMBE = this._InformationUC.TxtText;

                if(!Classes.Database_AGRO2.Exist_VPH(SOPNUMBE))
                {
                    this.InformationUC.LblText = "Vložte číslo výrobního příkazu";
                    this.InformationUC.TxtText = string.Empty;
                    this.InformationUC.LblWarning = "Výrobní příkaz nenalezen!";
                }
                else
                {
                    VyrobniPrikazSet(this.InformationUC.TxtText);
                    this.StavVyroba = (DataVyroba.VyrobaStavy.VPP_VlozitKod);
                }
            }
            else if (btn.Name == "btn19")
            {
                this.InformationUC.LblText = "Vložte číslo výrobního příkazu";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
            }
        }

        /// <summary>
        /// Zadani položky vyrobniho přikazu
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_VPP_VlozitKod(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.VPP_VlozitKod.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Vložte kód položky z výrobního příkazu";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.VPP_VlozitKod;

                this.InformationUC.TxtTextFocus();

                // v tomto stavu je linka vypnuta ... 
                this.SetLinkaState(false);
                return;
            }

            if (btn.Name == "BtnEnter")
            {
                if (this._InformationUC.TxtText.Length <= 0)
                {
                    this._InformationUC.LblWarning = "Musíte zadat kód položky z výrobního příkazu!";
                    return;
                }

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZADANI_VPP, this._InformationUC.TxtText, "");

                //Zde je zadan kód položky z vyrobiho přizak, a muže se  s nim pracovat
                var BarcodeP = this._InformationUC.TxtText;
                var row = Classes.Database_AGRO2.Get_VPP(BarcodeP, VPH_SOPNUMBE);

                if (row == null)
                {
                    this.InformationUC.LblText = "Vložte kód položky z výrobního příkazu";
                    this.InformationUC.TxtText = string.Empty;
                    this.InformationUC.LblWarning = "Položka nenalezena!";
                }
                else
                {
                    PolozkaVyrPrikazuSet(row);
                    this.StavVyroba = (DataVyroba.VyrobaStavy.ZadatEAN);
                }
            }
            else if (btn.Name == "btn19")
            {
                this.InformationUC.LblText = "Vložte kód položky z výrobního příkazu";
                this.InformationUC.TxtText = string.Empty;
                this.InformationUC.LblWarning = string.Empty;
            }
        }

        /// <summary>
        /// Zombie varianta, když se načitava špatny kod
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_Zombie(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.Zombie.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "Pokračovat?";
                this.InformationUC.LblWarning = "Nasnímaný č. kód neodpovídá výř. příkazu!";
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.Zombie;

                // v tomto stavu je linka vypnuta ... 
                this.SetLinkaState(false);
                return;
            }

            if (btn.Name == "BtnEnter")
            {
                _FujToKod = 0;
                this.InformationUC.LblWarning = string.Empty;
                this.SetLinkaState(true);
                HoukackaRizeni(HoukackaStav.OFF);

                VyrobaStavy stavVyrobaPredchozi = StavVyrobaStackRemoveTop(VyrobaStavy.Zombie);
                this.StavVyroba = this.StavVyroba;
            }

        }

        /// <summary>
        /// SERVIS varianta slouží pro servis
        /// </summary>
        /// <param name="btn"></param>
        private void StavVyroba_SERVIS(Button btn)
        {
            this.NumKeyPressedCheck(btn);

            if (btn == null)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_STAV_NASTAVEN, VyrobaStavy.SERVIS.ToString(), "");

                // Information uc nastavit
                this.InformationUC.LblText = "SERVIS!";
                this.InformationUC.LblWarning = "Jste ve stavu servisu!";
                this.InformationUC.InfoIDCode = (int)VyrobaStavy.SERVIS;

                // 1. Sepnout Rele
                this.SetServisState(true);
                //2. Ulozit status
                VyrobaActualDataSave(Constants.Common.STAV_RezimServisStart);

                return;
            }
            else if (btn.Name == "btnF6")
            {
                var stav = HoukackaRizeni(HoukackaStav.STAV);

                if (stav == HoukackaStav.ON)
                    HoukackaRizeni(HoukackaStav.OFF);

                else if (stav == HoukackaStav.OFF)
                    HoukackaRizeni(HoukackaStav.ON);

            }
            else if (btn.Name == "BtnEnter")
            {
                this.InformationUC.LblWarning = string.Empty;

                // 1. Vypnout Rele
                this.SetServisState(false);
                //2. Ulozit status
                VyrobaActualDataSave(Constants.Common.STAV_RezimServisStop);

                VyrobaStavy stavVyrobaPredchozi = StavVyrobaStackRemoveTop(VyrobaStavy.SERVIS);
                this.StavVyroba = this.StavVyroba;
            }

        }


        /// <summary>
        /// NEJEDNA SE O METODU STATOVEHO AUTOMATU
        /// je to pomocna metoda pro poslendi paletu
        /// </summary>
        /// <param name="PocPytPal"></param>
        /// <param name="PocPalet"></param>
        /// <returns></returns>
        private DialogResult PosledniPaleta(out int PocPytPal, out int PocPalet, out string PackType)
        {
            try
            {
                PocPytPal = 0;
                PocPalet = 0;
                PackType = string.Empty;

                DialogResult dr;

                if(frmPP != null)
                    frmPP = null;

                using (frmPP = new frmPosledniPaleta(this))
                {
                    frmPP.EAN = _vpp_row.BarcodeP;
                    frmPP.ITEMDESC = _vpp_row.ITEMDESC;
                    frmPP.VPP_pol = _vpp_row.ITEMNMBR;
                    frmPP.VPH_SOPNUMBE = _vph_sopnumbe;
                    frmPP.VPP_QTYPACK = VPP_Row.QTYPACK;


                    frmPP.WindowState = FormWindowState.Maximized;
                    Flag_OtevrenyDialog_PosleniPaleta = true;
                    dr = frmPP.ShowDialog();
                    Flag_OtevrenyDialog_PosleniPaleta = false;
                    PocPytPal = frmPP.QTY_PytluNaPalete;
                    PocPalet = frmPP.QTY_Palet;
                    PackType = frmPP.PackType;
                    Flag_PO_PredcasnyOdjezd = frmPP.Flag_PO_PredcasnyOdjezd;
                }

                return dr;

            }
            finally
            {
                frmPP = null;
            }
        }

        private void OdeslaniPalety()
        {
            SetPaletizatorState(true);

            System.Timers.Timer runonce = new System.Timers.Timer(AgroConfig.config.Agro[0].CasSepnutiProRelePaletizatorAdam);
            runonce.Elapsed += (s, e) => { SetPaletizatorState(false); };
            runonce.AutoReset = false;
            runonce.Start();
        }

        /// <summary>
        /// Provedeni kliku na funkcni klavesu ... 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void StavVyroba_KeyboardUC_ButtonClick(object sender, EventArgs e)
    {
        if (sender is Button)
        {
            Button btn = (Button)sender;
            this.MainForm.BeginInvoke((MethodInvoker)delegate() { VyrobaButtonClick(btn); });
        }

    }

        void VyrobaButtonClick(Button btn)
        {
            try
            {
                // Proc je toto tady ????
                // 9.4.2019 JiS -> zakomentovano ... po kliku se resetuje stav ... ???? bez logovani ... ????
                //if ((vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].SensorStatePrevious == true)
                //    ||
                //    (vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].SensorStateActual == true))
                //{
                //    //vracime se do pocatecniho stavu
                //    vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].setSensorStateActualDeactive();
                //    vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].setSensorStatePreviousDeactive();
                //    vyroba.codeReadResultActual = FASK.SledovaniVyroby.IScannerProvider.Code.NoData;
                //    vyroba.codeReadResultPrevious = FASK.SledovaniVyroby.IScannerProvider.Code.NoData;
                //}

                // Zalogovani kliku na tlacitko ... 
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_BUTTON_CLICK, this.StavVyroba.ToString(), String.Format("{0}/{1}", btn.Name, btn.Text));

                if (btn.Name == "btnF6") // Houkacka
                {
                    if (this._PocetPruchoduZmenDoProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
                    {
                        //AGRO_HOUKACKA - OFF
                        //this.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
                        this._PocetPruchoduZmenDoProhaz = 0;
                    }
                }

                switch (this.StavVyroba)
                {
                    case VyrobaStavy.Event11:
                        this.StavVyroba_Event11(btn);
                        break;
                    case VyrobaStavy.Event36:
                        this.StavVyroba_Event36(btn);
                        break;
                    case VyrobaStavy.Main:
                        this.StavVyroba_Main(btn);
                        break;
                    case VyrobaStavy.LogIDSmena:
                        this.StavVyroba_LogIDSmena(btn);
                        break;
                    case VyrobaStavy.LogIDSmenaPotvrzeni:
                        // TODO : stav k pridani
                        break;
                    case VyrobaStavy.LogIDPracovnik:
                        this.StavVyroba_LogIDPracovnik(btn);
                        break;
                    case VyrobaStavy.LogIDPracovnikPotvrzeni:
                        // TODO : stav k pridani
                        break;
                    case VyrobaStavy.ZadatEAN:
                        this.StavVyroba_ZadatEAN(btn);
                        break;
                    case VyrobaStavy.Odhlaseni:
                        this.StavVyroba_Odhlaseni(btn);
                        break;
                    case VyrobaStavy.ZmenaProhaz:
                        this.StavVyroba_ZmenaProhaz(btn);
                        break;
                    case VyrobaStavy.UlozeniEAN:
                        this.StavVyroba_UlozeniEAN(btn);
                        break;
                    case VyrobaStavy.Vyrobek:
                        this.StavVyroba_Vyrobek(btn);
                        break;
                    case VyrobaStavy.VlozKod:
                        this.StavVyroba_VlozKod(btn);
                        break;
                    case VyrobaStavy.VlozPocet:
                        this.StavVyroba_VlozPocet(btn);
                        break;
                    case VyrobaStavy.VlozPocetEAN:
                        this.StavVyroba_VlozPocetEAN(btn);
                        break;
                    case VyrobaStavy.SarzeHeslo:
                        this.StavVyroba_SarzeHeslo(btn);
                        break;
                    case VyrobaStavy.SarzeHodnota:
                        this.StavVyroba_SarzeHodnota(btn);
                        break;
                    case VyrobaStavy.SarzeID:
                        this.StavVyroba_SarzeID(btn);
                        break;
                    case VyrobaStavy.VPH_Zadani:
                        this.StavVyroba_VPH_Zadani(btn);
                        break;
                    case VyrobaStavy.VPP_VlozitKod:
                        this.StavVyroba_VPP_VlozitKod(btn);
                        break;
                    case VyrobaStavy.Zombie:
                        this.StavVyroba_Zombie(btn);
                        break;
                    case VyrobaStavy.SERVIS:
                        this.StavVyroba_SERVIS(btn);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exceptionOdvod)
            {
                //Log.Write(exceptionOdvod.Message.ToString());
               // Exceptions.Handler.ErrorHandle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavVyroba_KeyboardUC_ButtonClick", false);
                ExceptionHandler2.Handle(exceptionOdvod.Message + "\n" + exceptionOdvod.StackTrace, "StavVyroba_KeyboardUC_ButtonClick", false);

            }
        }

        #endregion

        #region Testovani stavu vyroby

        private void HlidaniProhaz()
        {
            bool cidlo = adam.GetProhazSensorState();
            if (cidlo && !this.ProhazZapnut)
            {
                ProhazZnak = "!";
                PocetPruchoduZmenProhaz++;
            }

            if (!cidlo)
            {
                ProhazZnak = "";
                PocetPruchoduZmenProhaz = 0;
            }
        }

        public string Time2Save()
        {
            TimeSpan rozdil = DateTime.Now - dtSaveLast;
            //int sekund = ((cas - dtSaveLast).Minutes * 60) + (cas - dtSaveLast).Seconds;
            double sekund = rozdil.TotalSeconds;
            return (AgroConfig.config.Agro[0].IntervalUkladaniDat - (int)sekund).ToString();
        }

        private void TestAutoUlozeniDat()
        {
           TimeSpan rozdil = DateTime.Now - dtSaveLast;
            //int sekund = ((cas - dtSaveLast).Minutes * 60) + (cas - dtSaveLast).Seconds;
           double sekund = rozdil.TotalSeconds;

            if (sekund > AgroConfig.config.Agro[0].IntervalUkladaniDat)
            {
                if ((_odvodVyroby.CodeAllCount > 0) && (_odvodVyroby.BarcodeActual == string.Empty))
                {
                    if (StavVyrobaStackGet() != VyrobaStavy.UlozeniEAN)
                    {
                        //SetLinkaState(false);
                        //AGRO_HOUKACKA - ON
                        //HoukackaRizeni(HoukackaStav.ON);
                        StavVyroba = VyrobaStavy.UlozeniEAN;
                    }
                }
                else
                {
                    if (StavVyrobaStackGet() != VyrobaStavy.UlozeniEAN)
                    {
                        // TODO : logovat ulozeni do aplikace
                        // + pocet pred ulozenim
                        // + pocet po ulozeni
                        // => muze byt problem v synchronizaci vlaken a prepisu hodnot poctu ... ???
                        // zde toto musi probehnout synchronne ... 
                        // v hlavnim vlakne
                        this.MainForm.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_automatickeUlozeni, getBarcodeSended(), string.Empty, null);
                        });                        
                        //Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), "automaticke ulozeni", _odvodVyroby.BarcodeActual, "", "", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);
                        //_odvodVyroby.Reset();
                        //dtSaveLast = DateTime.Now;
                    }
                }
            }
        }

        private void TestProhaz()
        {
            if (_PocetPruchoduZmenDoProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
            {
                bool cidlo = adam.GetProhazSensorState();
                if (!cidlo)
                {
                    //AGRO_HOUKACKA - OFF
                    //HoukackaRizeni(HoukackaStav.OFF);
                    //ProhazZapnut = false;
                    ProhazZnak = "";
                    _PocetPruchoduZmenDoProhaz = 0;
                    OnZmenaWarning("-");

                    this.InformationUC.InfoIDCode = 0;
                }
                else
                {
                    ProhazZnak = "!";
                    //AGRO_HOUKACKA - ON
                    //HoukackaRizeni(HoukackaStav.ON);		//zapne houkacku
                    this.InformationUC.InfoIDCode = 1;

                    //TODO MaR 11.7.2023 zakomentovani hlasky zmnen prohaz
                    OnZmenaWarning("Změň do prohaz!");



                    return;
                }
            }

        }

        private void TestUdalost36()
        {
            if (Udalosti.event36(Udalosti.Event36Stav.STATE, 0) > 0)
            {
                //if (StavVyrobaStackGet() != VyrobaStavy.Event36)
                //{
                //    //_linkaStateBeforeEvent = adam.rizeniLinky(LinkaStav.LINKA_STAV); //Zapamatuje si stav linky

                //    adam.rizeniLinky(LinkaStav.LINKA_OFF);			//vypne linku
                //    HoukackaRizeni(HoukackaStav.ON);		//zapne houkacku

                //    Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_EVENT36_OCURED, "","");

                //    StavVyroba = VyrobaStavy.Event36;

                //}
                this.StavVyroba = VyrobaStavy.Event36;
            }
        }

        private void TestUdalost11()
        {
            //zmena stavu menu - nastaly udalosti 11 xkrat za ycasu
            if (Udalosti.event11(Udalosti.Event11Stav.STATE, 0) > 0)
            {
                //if (StavVyrobaStackGet() != VyrobaStavy.Event11)
                //{
                //    //nastavuje se pouze pokud jiz neni ve stavu 11
                //    //_linkaStateBeforeEvent = adam.rizeniLinky(LinkaStav.LINKA_STAV); //Zapamatuje si stav linky

                //    adam.rizeniLinky(LinkaStav.LINKA_OFF);			//vypne linku
                //    HoukackaRizeni(HoukackaStav.ON);		//zapne houkacku

                //    //printf("Vice nepotvrz.:\n%d/%d E-konec", event11(EVENT11_COUNT), event11(EVENT11_GET_TIME));
                //    Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_EVENT11_OCURED, "", "");

                //    StavVyroba = VyrobaStavy.Event11;
                //}

                StavVyroba = VyrobaStavy.Event11;

            }
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

                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ZAHALENI_ZAHAJENO, "", "");

                this.InformationUC.InfoIDCode = 3;
                OnZmenaWarning("Linka zahálí!");

                // CHECK : Proc neco nulovat??? jde jen o priznak zahaleni ...
                //// vratime vse do pocatecniho stavu
                //cidla[ackSensor].setSensorStateActualDeactive();
                //cidla[ackSensor].setSensorStatePreviousDeactive();
                //codeReadResultActual = Code.NoData;
                //codeReadResultPrevious = Code.NoData;
            }

            // TODO : pokud je rozdil mensi, tak zahaleni vypnout ...
        }

        #endregion

        #region Algoritmus odvadeni vyroby

        /// <summary>
        /// potvrzovaci cidlo
        /// </summary>
        public void SensorPytelAck()
        {
            //algoritmusOdvodVyrobaOld(string.Empty, Code.NoData, true);
            //algoritmusOdvodVyrobaOld(string.Empty, Code.NoData, false);
            //Flag_PosleniPaletaOdhlaseniZadat = true;
            algoritmusOdvodVyroba_2_SensorPytelActivated();
        }

        /// <summary>
        /// cidlo pocitani palet
        /// </summary>
        public void SensorPaletaAck(int sensor)
        {

            if ( Flag_OtevrenyDialog_PosleniPaleta)
            {
                VyrobaActualDataSave(Constants.Common.STAV_PO_predcasny_odjezd);
                frmPP.Flag_PO_PredcasnyOdjezd = true;
    }
            else
            {

                if (Flag_PosleniPaleta_je_NP)
                {
                    Flag_PosleniPaleta_je_NP = false;
                    decimal cnt = _odvodVyroby.CodeNoReadCntGetAndReset();
                    this.MainForm.BeginInvoke((MethodInvoker)delegate ()
                    {

                        this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_posledniPaleta2, getBarcodeSended(), -cnt, cnt, _vp);
                        _vp = null;
                    });
                }
                else
                {
                    // nakonec se zvedne interni citac sepnuti vsech cidel pro dany senzor
                    inkrementSensorAcitvatedCount(sensor);
                    algoritmusOdvodVyroba_2_SensorPaletaActivated();

                    this.MainForm.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_automatickeUlozeniPaleta, getBarcodeSended(), Constants.Common.STAV_UplnaPaleta, 0);

                    });

                    _SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);
                }
            }
            

            // 8.7.2021 JiS : automaticke ulozeni vyroby po prichodu signalu o palete a po zpracovani teto informace
            //this.MainForm.BeginInvoke((MethodInvoker)delegate()
            //{
            //    this.VyrobaActualDataSaveAndResetActualBarcode(false, Constants.Common.STAV_automatickeUlozeniPaleta, getBarcodeSended());
            //});

            //_SSCC = _Generator.Get_Next_SSCC(MachineID_Int, 1, MachineID_Int);

        }

        public void ScannerActivate(string code, Code codeReadResult)
        {
            //algoritmusOdvodVyrobaOld(code, codeReadResult, false);
            //algoritmusOdvodVyrobaOld(string.Empty, Code.NoData, false);

            algoritmusOdvodVyroba_2_ScannerActivated(codeReadResult, code);
        }


        private Scan _vyroba2_Scan = new Scan(DateTime.Now, Code.NoData, string.Empty);
        public Scan Vyroba_Scan
        {
            get { return _vyroba2_Scan; }
        }

        private Sensor _vyroba2_SensorPytel = new Sensor(DateTime.Now, false);
        public Sensor Vyroba_SensorPytel
        {
            get { return _vyroba2_SensorPytel; }
        }

        private Sensor _vyroba2_SensorPaleta = new Sensor(DateTime.Now, false);
        public Sensor Vyroba_SensorPaleta
        {
            get { return _vyroba2_SensorPaleta; }
        }

        private void algoritmusOdvodVyroba_2_ScannerActivated(Code readResult, string code)
        { 
            // toto nastane, pouze pokud dojde k aktivaci scanneru
            // tedy ve dvou (2) stavech a to Read(scanner aktivovan a nacten c.k.) nebo NoRead (scanner aktivovan a nenacten c.k.)
            // 
            // vzhledem k tomu, ze jde o hlavni vlakno, tak toto se zpracovava v poradi jak prijde a je treba brat v ohled aktualni stav cidla potvrzeni
            // Pokud je udalost zpracovana, pak se stavy cidla i scanneru vynuluji

            if(true)
            {
                if (readResult == Code.Read)
                {
                    if(!code.All(Char.IsDigit))
                    {
                        code = _odvodVyroby.BarcodeActual;
                    }
                }
            }


            Scan scan = new Scan(DateTime.Now, readResult, code);

            switch (scan.ReadResult)
            {
                case Code.Read:
                    // kod nacten => nastavit stav
                    if (_vyroba2_Scan.ReadResult == Code.NoData)
                    { // predchozi byl odveden/resetovan, tak nastavim tento a zpracuji
                        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_ScanReadOK , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                    }
                    else if (_vyroba2_Scan.ReadResult == Code.Read)
                    { // kod nacten
                        // pokud se ale lisi, tak doslo ke zmene vyrobku a musim provest akce
                        // => ulozeni dat
                        // nasledne pustim do vyhodnoceni
                        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_ScanReadNavic , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 1, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                        this.Event11_Occured();
                    }
                    else if (_vyroba2_Scan.ReadResult == Code.NoRead)
                    { // kod nenacten, pustim dal k vyhodnoceni
                        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_ScanReadNavic , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 1, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                        this.Event11_Occured();
                    }

                    if (_vpp_row != null)
                    {
                        if (!_vpp_row.IsBarcodeTNull() && _vpp_row.BarcodeT == 1)
                        {
                            Kontrola_OdvadeniVyroby(scan);
                        } 
                    }

                    _vyroba2_Scan = scan;                    
                    algoritmusOdvodVyroba_2();

                    break;
                case Code.NoRead:
                    // kod nenacten => nastavit stav
                    if (_vyroba2_Scan.ReadResult == Code.NoData)
                    { // predchozi byl odveden/resetovan, tak nastavim tento a zpracuji
                        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_ScanNOReadOK , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                    }
                    else if (_vyroba2_Scan.ReadResult == Code.Read)
                    { // v predchozim byl kod nacten
                        // => pravdepodobne se priradi predchozimu kodu
                        // - pokud nebyl jeste nacten c.k., tak timto se priradi
                        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_ScanNOReadNavic , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 1, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                        this.Event11_Occured();
                    }
                    else if (_vyroba2_Scan.ReadResult == Code.NoRead)
                    { // v predchozim nebyl nacten a ted take neni nacten 
                        // => zvysi se kounter nenacteni
                        // pokud bude aktivni sensor, tak odvod
                        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_ScanNOReadNavic, _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 1, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                        this.Event11_Occured();
                    }

                    if (_vpp_row != null)
                    {
                        if (!_vpp_row.IsBarcodeTNull() && _vpp_row.BarcodeT == 1)
                        {
                            Kontrola_OdvadeniVyroby_NORead(scan);
                        }
                    }


                    _vyroba2_Scan = scan;                    
                    algoritmusOdvodVyroba_2();

                    break;
                case Code.BadRead:
                    // spatne cteni => zalogovat, pripadne reagovat udalosti
                    break;
                case Code.TooLong:
                    // prilis dlouhy => zalogovat, pripadne reagovat udalosti
                    break;
                case Code.NoData:
                default:
                    // zde se nic nebude dit, protoze NoData = nic se nedelo
                    break;
            }

        }

        private int _FujToKod = 0;

        private void Kontrola_OdvadeniVyroby(Scan scan)
        {
            try
            {
                //bool Odvadeni_Kontrola_Pouzit = true;

                //int Odvadeni_Var_1_pocet = 1;
                //bool Odvadeni_Var_1_pouzit = true;
                //bool Odvadeni_Var_1_pokracuj = true;

                //int Odvadeni_Var_2_pocet = 3;
                //bool Odvadeni_Var_2_pouzit = true;
                //bool Odvadeni_Var_2_pokracuj = true;

                //int Odvadeni_Var_3_pocet = 5;
                //bool Odvadeni_Var_3_pouzit = true;
                //bool Odvadeni_Var_3_pokracuj = true;


                if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Kontrola_Pouzit && _vpp_row != null)
                {
                    string NasnimanyKod = scan.ReadBarcode.Trim();

                    if (NasnimanyKod != _vpp_row.BarcodeP.Trim())
                    {
                        _FujToKod++;
                    }
                    else if (NasnimanyKod == _vpp_row.BarcodeP.Trim())
                    {
                        _FujToKod = 0;
                        HoukackaRizeni(HoukackaStav.OFF);
                        this.InformationUC.LblWarning = string.Empty;
                    }


                    if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pocet == _FujToKod)
                    {
                        if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pouzit)
                        {
                            //Log.Write("Spatny Kod: Houkacka");
                            ExceptionHandler2.Handle("Spatny Kod: Houkacka", "Log_Vyroba_Agro_Modbus_2", "txt");
                            //HoukackaRizeni(HoukackaStav.ON);
                        }
                    }
                    else if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pocet == _FujToKod)
                    {
                        if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pouzit)
                        {
                            //Log.Write("Spatny Kod: houkacka + hlaska");
                            ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska", "Log_Vyroba_Agro_Modbus_2", "txt");
                            HoukackaRizeni(HoukackaStav.ON);
                            this.InformationUC.LblWarning = "Nasnímaný č. kód neodpovídá výř. příkazu!";
                        }
                    }
                    else if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pocet == _FujToKod)
                    {
                        if (AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pouzit)
                        {

// MERGE - TaD 14.10.2022 
                            VyrobaActualDataSave(Constants.Common.STAV_SSK);
                            //Log.Write("Spatny Kod: houkacka + hlaska + stop linka");
                            ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska + stop linka", "Log_Vyroba_Agro_Modbus_2", "txt");

                            // VyrobaActualDataSave("System kontroly kodu 3");
                            // //Log.Write("Spatny Kod: houkacka + hlaska + stop linka");
                            // ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska + stop linka", "Log_Vyroba_Agro_Modbus_2", "txt");

                            HoukackaRizeni(HoukackaStav.ON);
                            this.InformationUC.LblWarning = "Nasnímaný č. kód neodpovídá výř. příkazu!";
                            this.SetLinkaState(false);
                            this.StavVyroba = VyrobaStavy.Zombie;

                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void Kontrola_OdvadeniVyroby_NORead(Scan scan)
        {
            try
            {
                //bool Odvadeni_Kontrola_Pouzit = true;

                //int Odvadeni_Var_1_pocet = 1;
                //bool Odvadeni_Var_1_pouzit = true;
                //bool Odvadeni_Var_1_pokracuj = true;

                //int Odvadeni_Var_2_pocet = 3;
                //bool Odvadeni_Var_2_pouzit = true;
                //bool Odvadeni_Var_2_pokracuj = true;

                //int Odvadeni_Var_3_pocet = 5;
                //bool Odvadeni_Var_3_pouzit = true;
                //bool Odvadeni_Var_3_pokracuj = true;


                if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Kontrola_Pouzit && _vpp_row != null)
                {
                    string NasnimanyKod = scan.ReadBarcode.Trim();

                    if (NasnimanyKod != _vpp_row.BarcodeP.Trim())
                    {
                        _FujToKod++;
                    }
                    else if (NasnimanyKod == _vpp_row.BarcodeP.Trim())
                    {
                        _FujToKod = 0;
                        HoukackaRizeni(HoukackaStav.OFF);
                        this.InformationUC.LblWarning = string.Empty;
                    }


                    if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pocet == _FujToKod)
                    {
                        if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pouzit)
                        {
                            //Log.Write("Spatny Kod: Houkacka");
                            ExceptionHandler2.Handle("Spatny Kod: Houkacka", "Log_Vyroba_Agro_Modbus_2", "txt");
                            //HoukackaRizeni(HoukackaStav.ON);
                        }
                    }
                    else if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pocet == _FujToKod)
                    {
                        if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pouzit)
                        {
                            //Log.Write("Spatny Kod: houkacka + hlaska");
                            ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska", "Log_Vyroba_Agro_Modbus_2", "txt");
                            HoukackaRizeni(HoukackaStav.ON);
                            this.InformationUC.LblWarning = "Nasnímaný č. kód neodpovídá výř. příkazu!";
                        }
                    }
                    else if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pocet == _FujToKod)
                    {
                        if (AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pouzit)
                        {
                            VyrobaActualDataSave(Constants.Common.STAV_SSK_NORead);
                            //Log.Write("Spatny Kod: houkacka + hlaska + stop linka");
                            ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska + stop linka", "Log_Vyroba_Agro_Modbus_2", "txt");
                            HoukackaRizeni(HoukackaStav.ON);
                            this.InformationUC.LblWarning = "Nasnímaný č. kód neodpovídá výř. příkazu!";
                            this.SetLinkaState(false);
                            this.StavVyroba = VyrobaStavy.Zombie;

                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }


        private void algoritmusOdvodVyroba_2_SensorPytelActivated()
        {
            Sensor sensor = new Sensor(DateTime.Now, true);


            if (Flag_OtevrenyDialog_PosleniPaleta)
            {
// MERGE - TaD 14.10.2022 
                VyrobaActualDataSave(Constants.Common.STAV_PP_predcasne_spusteni);
                //Log.Write("Spatny Kod: houkacka + hlaska + stop linka");
                ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska + stop linka", "Log_Vyroba_Agro_Modbus_2", "txt");


                // VyrobaActualDataSave("PP predcasne spusteni");
                // //Log.Write("Spatny Kod: houkacka + hlaska + stop linka");
                // ExceptionHandler2.Handle("Spatny Kod: houkacka + hlaska + stop linka", "Log_Vyroba_Agro_Modbus_2", "txt");

                HoukackaRizeni(HoukackaStav.ON);
                this.InformationUC.LblWarning = "Nasnímaný č. kód neodpovídá výř. příkazu!";
                this.SetLinkaState(false);
            }


            // pokud dojde k aktivaci cidla, tak jde pouze o jeden stav
            if (_vyroba2_SensorPytel.Active)
            {
                // doslo k druhe aktivaci cidla => nepotvrzeno ctenim a tedy jde o "Falesne potvrzeni", ale stav ponechavam aktivni, 
                // falesne potvrzeni, stav nechavam aktivni
                // provedeni zalogovani tohoto stavu

                // TODO : udalost obsluhy, falsene cteni
                // TODO : pocitani techto stavu pro pripadne zobrazeni hlaseni o tomto stavu
                _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_CidloNavic , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 1, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                this.Event36_Occured();
            }
            else
            {
                _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, Constants.Common.STAV_CidloOK, _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
            }

            // drive nebyl aktivni, tak ho aktivuji
            _vyroba2_SensorPytel = sensor;            
            // a provedu vyhodnoceni stavu
            algoritmusOdvodVyroba_2();
        }

        private void algoritmusOdvodVyroba_2_SensorPaletaActivated()
        {
            Sensor sensor = new Sensor(DateTime.Now, true);

            //// pokud dojde k aktivaci cidla, tak jde pouze o jeden stav
            //if (_vyroba2_SensorPaleta.Active)
            //{
            //    // doslo k druhe aktivaci cidla => nepotvrzeno ctenim a tedy jde o "Falesne potvrzeni", ale stav ponechavam aktivni, 
            //    // falesne potvrzeni, stav nechavam aktivni
            //    // provedeni zalogovani tohoto stavu

            //    // TODO : udalost obsluhy, falsene cteni
            //    // TODO : pocitani techto stavu pro pripadne zobrazeni hlaseni o tomto stavu
            //    _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, "Cidlo paleta navic", _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0);
            //    this.Event36_Occured();
            //}
            //else
            //{
            //    _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 0, "Cidlo paleta ok", _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0);
            //}

            // drive nebyl aktivni, tak ho aktivuji
            _vyroba2_SensorPaleta = sensor;
            // a provedu vyhodnoceni stavu
            algoritmusOdvodVyroba_2();
        }

        private void algoritmusOdvodVyroba_2()
        {
            // metoda je volana synchronne z hlavniho vlakna pres begininvoke, takze je sekvencni
            // v prvnim kroku se od 8.2019 pocita pruchod jen pres potvrzovaci cidlo
            //
            // tedy scanner jen meni pripadne vyrobek => funkcnost prevzata z metody ZmenaVyrobku
            // zmena vyrobku se testuje jako prvni, 
            // nasleduje pocitani zmeny stavu cidla
            
            // provede zpracovani stavu 
            // v podstate pouze napocitani odvodu, pripadne prirazeni car.kodu...

            VypnoutZahaleni();

            ZjisteniNazvuPytle();

            #region Odvod, sensor aktivni a scan proveden
            // TODO : docasne zmena algoritmu, kvuli problemu se scannerem => zmena pouze na odvod pres aktivaci cidla ... 

            // pokud je car.kod nacten, a lisi se od aktualniho, tak provest "zmenu vyrobku"
            if (_vyroba2_Scan.ReadResult == Code.Read)
            {
                // nacteny kod se lisi od aktualniho odvodu 
                // - Prazdny muze byt => pak se jen priradi, protoze prazdny se pozaduje prirazeni rucne
                // - ulozeni aktualniho stavu odvodu
                // - zmena car.kodu
                if (this._vyroba2_Scan.ReadBarcode != this._odvodVyroby.BarcodeActual)
                {
                    // Pokud je stav Read + Načteny kod je jiny než ten v pameti


                    if (!String.IsNullOrEmpty(this._odvodVyroby.BarcodeActual))
                    {   // ulozeni, jen pokud car.kod odvodu je nastaven
                        this.VyrobaActualDataSaveAndResetActualBarcode(Constants.Common.STAV_Vyrobek_zmenaVyrobkuSCAN, this._vyroba2_Scan.ReadBarcode);
                    }

                    // zmena odvodu na novy car.kod
                    if(string.IsNullOrEmpty(this._odvodVyroby.BarcodeActual))
                        this._odvodVyroby.BarcodeActual = this._vyroba2_Scan.ReadBarcode;

                    // nalezeni odpovidajiciho zaznamu z aktual datarow prehledu historie
                    if ((_aktualDataRow != null) && (String.IsNullOrEmpty(_aktualDataRow.barcodeReaded)))
                        _aktualDataRow.barcodeReaded = _odvodVyroby.BarcodeActual; // pokud neni nastaven carovy kod, tak se nastavi ...
                    // ulozi zaznam a vrati odpovidajici klici
                    this._aktualDataRow = InsertDataToDataset(this.SmenaID, "", 0, 0, Constants.Common.STAV_Vyrobek_zmenaVyrobkuSCAN, _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                }
            }


            if (this._vyroba2_SensorPytel.Active)
            {
                if ((_aktualDataRow != null) && (String.IsNullOrEmpty(_aktualDataRow.barcodeReaded)))
                    _aktualDataRow.barcodeReaded = _odvodVyroby.BarcodeActual; // pokud neni nastaven carovy kod, tak se nastavi ...
                 //_odvodVyroby.CodeNoReadCnt++;

                //_odvodVyroby.CodeNoReadCnt++; //TODO MaR 11.7.2013 pocitani poctu pytlu?
                if (this.ProhazZapnut)
                {
                    _odvodVyroby.CodeProhazCntIncrement(1);
                    //_odvodVyroby.CodeProhazZapnut(this.ProhazZapnut);
                }

                _odvodVyroby.CodeNoReadCntIncrement(1);
                _aktualDataRow = InsertDataToDataset(this.SmenaID, "", 0, 1, Constants.Common.STAV_sensorOdvod , _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 0, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                _vyroba2_SensorPytel = new Sensor(DateTime.Now, false);
            }

            if (this._vyroba2_SensorPaleta.Active)
            {
                if ((_aktualDataRow != null) && (String.IsNullOrEmpty(_aktualDataRow.barcodeReaded)))
                    _aktualDataRow.barcodeReaded = _odvodVyroby.BarcodeActual; // pokud neni nastaven carovy kod, tak se nastavi ...

                //_odvodVyroby.CodeNoReadCntIncrement(1);
                _aktualDataRow = InsertDataToDataset(this.SmenaID, "", 0, 0, Constants.Common.STAV_sensorPaleta, _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0, 1, "", null, "", "", "", null, 0, "", 0, _vpp_row == null ? (byte)0 : _vpp_row.BarcodeT, null, null, null, null, null);
                _vyroba2_SensorPaleta = new Sensor(DateTime.Now, false);
            }

            //if (_vyroba2_Sensor.Active && (_vyroba2_Scan.ReadResult == Code.Read || _vyroba2_Scan.ReadResult == Code.NoRead))
            //{ // zde se provede vyhodnoceni / parovani
            //    // pokud je/byl sensor aktivni a scanner neco poslal, tak inkrementovat

            //    if (_vyroba2_Scan.ReadResult == Code.Read)
            //    { // scanner Read 
            //        //kod nacten a potvrzen cidlem => odvod + 1 nebo novy kod
            //        if (String.IsNullOrEmpty(_odvodVyroby.BarcodeActual))
            //        { // Kod nebyl dosud inicializovan
            //            _odvodVyroby.BarcodeActual = _vyroba2_Scan.ReadBarcode;
            //            _odvodVyroby.CodeReadCnt++;
            //            if ((_aktualDataRow != null) && (String.IsNullOrEmpty(_aktualDataRow.barcodeReaded)))
            //                _aktualDataRow.barcodeReaded = _odvodVyroby.BarcodeActual; // pokud neni nastaven carovy kod, tak se nastavi ...
            //            _aktualDataRow = InsertDataToDataset(this.SmenaID, "", 1, 0, "sensor", _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0);
            //            //Database.InsertNewEvents(_smenaID, DateTime.Now, 1, CodeNoReadCnt, "sensor", BarcodeActual, barcodeNew, "", "", "", DateTime.Now, "", "", "", "", "", "");
            //        }
            //        else if (_odvodVyroby.BarcodeActual == _vyroba2_Scan.ReadBarcode)
            //        {//kody jsou stejne, tak zvysit counter odvedenych
            //            // odvodVyroby.BarcodeActual = _vyroba2_Scan.readbarcode; // kody jsou stejne, neni treba to menit ...
            //            _odvodVyroby.CodeReadCnt++;
            //            _aktualDataRow = InsertDataToDataset(this.SmenaID, "", 1, 0, "sensor", _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0);
            //        }
            //        else //if (odvodVyroby.BarcodeActual != _vyroba2_Scan.readbarcode)
            //        {//nejsou stejne => kod byl inicializovan, zmena vyrobku
            //            this.VyrobaActualDataSaveAndResetActualBarcode("prisel novy kod, stary ulozeni", _vyroba2_Scan.ReadBarcode);
            //            //Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), "prisel novy kod, stary ulozeni", _odvodVyroby.BarcodeActual, _vyroba2_Scan.ReadBarcode, "zadna", _prohazZapnut.ToString(), "", "", "", "", "", "", _sarze);

            //            this._odvodVyroby.BarcodeActual = _vyroba2_Scan.ReadBarcode;
            //            this._odvodVyroby.CodeReadCnt++;
            //            _aktualDataRow = InsertDataToDataset(_smenaID, "", 1, 0, "novy kod, nejsou stejne", _odvodVyroby.BarcodeActual, _odvodVyroby.BarcodePrevious, "", "", "", "", "", "", "", "", _sarze, 0, 0, 0);
                        
            //            dtSaveLast = DateTime.Now; //posledni ulozeni ??? PROC ??? => oddaleni automatickeho ulozeni 
            //        }
            //    }
            //    else if (_vyroba2_Scan.ReadResult == Code.NoRead)
            //    { // scanner aktivovan, ale kod nenacten, tedy nemam platny carovy kod ...
            //        // => zvyseni CodeNoReadCnt
            //        // odvodVyroby.BarcodeActual = _vyroba2_Scan.readbarcode; // toto ne, protoze jinak bych prisel o informaci spravneho nacteneho kodu
            //        _odvodVyroby.CodeNoReadCnt++;
            //        _aktualDataRow = InsertDataToDataset(_smenaID, "", 0, 1, "cidlo:A, scan:NR", _odvodVyroby.BarcodeActual, "", "", "", "", "", "", "", "", "", _sarze, 0, 0, 0);
            //    }

            //    _vyroba2_Scan = new Scan(DateTime.Now, Code.NoData, string.Empty);
            //    _vyroba2_Sensor = new Sensor(DateTime.Now, false);

            //}
            #endregion

            HlidaniProhaz();    //testuje hlidani prohazovani ...

        }

        #endregion

        #region Vkladani mnozstvi do lokalni databaze pro zobrazeni

        /// <summary>
        /// Vlozi zaznam do lokalni databaze
        /// </summary>
        /// <param name="barcodeReaded">aktualni carovy kod</param>
        /// <param name="barcodeSended">predchozi carovy kod</param>
        /// <returns>Vraci existujici nebo novy zaznam</returns>
        public Data.FASK_EventsRow InsertDataToDataset(
            string loginid, 
            string machineid, 
            decimal qty, 
            decimal qtyReal, 
            string description, 
            string barcodeReaded, 
            string barcodeSended,
            string zakazka, 
            string popis, 
            string reporttype, 
            string ido, 
            string scan1, 
            string scan2, 
            string scan3, 
            string sensor, 
            string material, 
            int nocntsensor, 
            int noncntscanread, 
            int noncntscannoread,
            int pocetPalet,
            string VPH,
            int? VPPol,
            string EAN_IS,
            string IS_ID,
            string NMBRPAL,
            int? status,
            decimal QTYPACK,
            string PackType,
            decimal? WEIGHT,
            byte BarcodeT,
            string REZ_1,
            string REZ_2,
            string REZ_3,
            string REZ_4,
            string REZ_5
            )
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


                //TODO 19.10.2021 TaD poznamka
                // tady asi mnel byt aj vyrobny prikaz...


                var items = _vyrobaDataHistoryInMemory.FASK_Events.Where(x => x.barcodeReaded == barcodeReaded && x.material == material);
                if (items.Count() > 0)
                {
                    var item = items.First();
                    item.qty += qty;
                    item.qtyReal += qtyReal;
                    item.NoCntSensor += nocntsensor;
                    item.NoCntScanRead += noncntscanread;
                    item.NoCntScanNoRead += noncntscannoread;
                    item.pocetPalet += pocetPalet;
                    return item;
                }

                // Pokud nebylo nalezeno, tak ho vlozi ...

                var row = _vyrobaDataHistoryInMemory.FASK_Events.NewFASK_EventsRow();

                row.loginid = loginid;
                row.machineid = machineid;
                row.dateeve = DateTime.Now;
                row.qty = qty;
                row.qtyReal = qtyReal;
                row.description = description;
                row.barcodeReaded = barcodeReaded;
                row.barcodeSended = barcodeSended;
                row.zakazka = zakazka;
                row.popis = popis;
                row.faskGUID = new Guid();
                row.reportType = reporttype;
                row.isProcessed = DateTime.Now;
                row.IDO = ido;
                row.scan1 = scan1;
                row.scan2 = scan2;
                row.scan3 = scan3;
                row.sensor = sensor;
                row.material = material;
                row.NoCntSensor = nocntsensor;
                row.NoCntScanRead = noncntscanread;
                row.NoCntScanNoRead = noncntscannoread;
                row.pocetPalet = pocetPalet;

                row.VPH = VPH;
                if (VPPol.HasValue)
                    row.VPPol = VPPol.Value;
                else
                    row.SetVPPolNull();

                row.EAN_IS = EAN_IS;
                row.IS_ID = IS_ID;
                row.NMBRPAL = NMBRPAL;

                if (status.HasValue)
                    row.status = status.Value;
                else
                    row.SetstatusNull();

                row.QTYPACK = QTYPACK;
                row.PackType = PackType;

                if (WEIGHT.HasValue)
                    row.WEIGHT = WEIGHT.Value;
                else
                    row.WEIGHT = 0;


                row.BarcodeT = BarcodeT;

                if (string.IsNullOrEmpty(REZ_1))
                    row.SetREZ_1Null();
                else
                    row.REZ_1 = REZ_1.Trim();

                if (string.IsNullOrEmpty(REZ_2))
                    row.SetREZ_2Null();
                else
                    row.REZ_2 = REZ_2.Trim();

                if (string.IsNullOrEmpty(REZ_3))
                    row.SetREZ_3Null();
                else
                    row.REZ_3 = REZ_3.Trim();

                if (string.IsNullOrEmpty(REZ_4))
                    row.SetREZ_4Null();
                else
                    row.REZ_4 = REZ_4.Trim();

                if (string.IsNullOrEmpty(REZ_5))
                    row.SetREZ_5Null();
                else
                    row.REZ_5 = REZ_5.Trim();


                _vyrobaDataHistoryInMemory.FASK_Events.AddFASK_EventsRow(row);

                return row;


                //return _vyrobaDataHistoryInMemory.FASK_Events.AddFASK_EventsRow(
                //    loginid,
                //    machineid,
                //    DateTime.Now,
                //    qty,
                //    qtyReal,
                //    description,
                //    barcodeReaded,
                //    barcodeSended,
                //    zakazka,
                //    popis,
                //    new Guid(),
                //    reporttype,
                //    DateTime.Now,
                //    ido,
                //    scan1,
                //    scan2,
                //    scan3,
                //    sensor,
                //    material,
                //    nocntsensor,
                //    noncntscanread,
                //    noncntscannoread,
                //    pocetPalet,
                //    VPH,
                //    VPPol,
                //    EAN_IS,
                //    IS_ID,
                //    NMBRPAL,
                //    status
                //    );
            }
            catch (Exception ex)
            {
               // Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
                _vyrobaDataHistoryInMemory.AcceptChanges();
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
        public Data.FASK_EventsRow InsertMinusQtyToDataset(string loginid, decimal qty, decimal qtyReal, string barcodeReaded, string material)
        {
            try
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

                var items = _vyrobaDataHistoryInMemory.FASK_Events.Where(x => x.barcodeReaded == barcodeReaded && x.material == material);
                if (items.Count() > 0)
                {
                    var item = items.First();
                    item.qty -= qty;
                    item.qtyReal -= qtyReal;
                    return item;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
                _vyrobaDataHistoryInMemory.AcceptChanges();
            }
        }
        #endregion

        #region Ukladani dat

        private void VynulovaniPromennych()
        {
            this._PocetPruchoduZmenDoProhaz = 0;

            if (adam != null)
            {
                if (adam.GetProhazSensorState()) // TODO : zmenit na zjisteni stavu cidla pro prohazovani
                    ProhazZnak = "!";
                else
                    ProhazZnak = "";
            }

            dtIdleLast = dtNoReadLast = dtPytelReadLast = dtReadLast = DateTime.Now;

            //ProhazZapnut = false;

        }

        /// <summary>
        /// Ulozi a vynuluje lokalni data, citace sensoru, citac poctu car.kodu
        /// </summary>
        public void VyrobaSaveAndClearDataHistory()
        {
            VyrobaDataHistoryInMemorySave2UserEventsAndClear();
            VyrobaSensorsCountSave2UserEventsAndClear();
            VyrobaScannerCodeCountSave2UserEventsAndClear();
        }

        /// <summary>
        /// Ulozi do udalosti obsluhy pocty v lokalnim datasetu carovykod a jeho pocet
        /// <remarks>Slouzi pro verifikaci hodnot nactenych a ulozenych do Events ... (40=carovykod, 41=pocet</remarks>
        /// </summary>
        private void VyrobaDataHistoryInMemorySave2UserEventsAndClear()
        {
            foreach (var item in _vyrobaDataHistoryInMemory.FASK_Events)
            {
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_FASK_EVENTS_DATA_BARCODE, item.barcodeReaded.Trim(),"");
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_FASK_EVENTS_DATA_COUNT_READ, item.qty.ToString("0"), "");
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_FASK_EVENTS_DATA_COUNT_NOREAD, item.qtyReal.ToString("0"), "");
            }

            _vyrobaDataHistoryInMemory.FASK_Events.Clear();
            _vyrobaDataHistoryInMemory.AcceptChanges();
            this._aktualDataRow = null;
        }

        /// <summary>
        /// Ulozi stav sledovanych cidel (4) a vynuluje citace ...
        /// </summary>
        private void VyrobaSensorsCountSave2UserEventsAndClear()
        {
            // maximalne 6 cidel ...
            if (_cidla_vstupni.Count > 0)
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PHOTO1_COUNT, _cidla_vstupni[0].SensorsCnt.ToString(), "");
            if (_cidla_vstupni.Count > 1)
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PHOTO2_COUNT, _cidla_vstupni[1].SensorsCnt.ToString(), "");
            if (_cidla_vstupni.Count > 2)
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PHOTO3_COUNT, _cidla_vstupni[2].SensorsCnt.ToString(), "");
            if (_cidla_vstupni.Count > 3)
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PHOTO4_COUNT, _cidla_vstupni[3].SensorsCnt.ToString(), "");
            if (_cidla_vstupni.Count > 4)
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PHOTO5_COUNT, _cidla_vstupni[4].SensorsCnt.ToString(), "");
            if (_cidla_vstupni.Count > 5)
                Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_PHOTO6_COUNT, _cidla_vstupni[5].SensorsCnt.ToString(), "");

            foreach (var cidlo_vstupni in _cidla_vstupni)
            {
                cidlo_vstupni.Reset();
            }
        }

        /// <summary>
        /// Ulozi do udadlosti obsluhy pocet nactenych car.kodu a vynuluje citac
        /// </summary>
        private void VyrobaScannerCodeCountSave2UserEventsAndClear()
        {
            Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_CODE_COUNT, _cidlo_scanner.SensorsCnt.ToString(), "");
            Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_CODE_READ_COUNT, _cidlo_scanner.ScannerReadCount.ToString(), "");
            Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_CODE_NOREAD_COUNT, _cidlo_scanner.ScannerNoReadCount.ToString(), "");

            _cidlo_scanner.Reset();
        }

        /// <summary>
        /// Slouzi pro ulozeni pri ukoncovani modulu / aplikace 
        /// </summary>
        public void VyrobaActualDataSave()
        {
            this.VyrobaActualDataSaveAndResetActualBarcode(true, Constants.Common.STAV_konecAplikace, getBarcodeSended(), string.Empty, null);
        }

        /// <summary>
        /// Slouzi k synchronizacnimu pristupu pro ulozeni dat, nic nemuze ulozit data drive, nez to provede tento mechanismus
        /// </summary>
        private object _VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject = new object();
        

        /// <summary>
        /// Ulozeni aktualniho stavu odvodu vyroby do lokalni databaze, pouze nulovani aktualniho poctu aktualniho caroveho kodu
        /// </summary>
        /// <param name="odvodVyrobyReset">zda se ma resetovat carovy kod odvodu</param>
        public void VyrobaActualDataSaveAndResetActualBarcode(bool odvodVyrobyReset, string description, string barcodeSended, string PackType, int? status)
        {
            try
            {
                lock (_VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject)
                {
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeReadCnt.ToString() + "," + _odvodVyroby.CodeNoReadCnt.ToString());
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //if (_odvodVyroby.CodeAllCount > 0)
                    //{   //pokud neco v bufferu bylo, pak zapis
                    //    Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //}

                    int pocetPytlu = 0;
                    int pocetP = 0;
                    int QTYpacktmp = 0;

                    try
                    { pocetPytlu = (int)(AktualDataRow.qty + AktualDataRow.qtyReal); }
                    catch (Exception ex) 
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                    }


                    try
                    { pocetP = AktualDataRow.IspocetPaletNull() ? 0 : AktualDataRow.pocetPalet; }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                    }

                    try
                    { QTYpacktmp = (int)(VPP_Row.IsQTYPACKMJNull() ? 0 : VPP_Row.QTYPACK); }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                    }



                    int x = pocetPytlu - (pocetP * QTYpacktmp);
                    var ZbyvaPocet = string.Format("Δ:{0}", x);

                    // 22.6.2021 - Ulozim i nuly ... 
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    // scan1=counter0+3, scan2=counter1+3, scan3=counter2+3
                    bool ulozeni = false;

                    //TODO MaR 1.8. 2023 ukladani bez resetu
                    if (!odvodVyrobyReset && description == Constants.Common.STAV_automatickeUlozeni)
                    {
                         ulozeni = Classes.Database_AGRO2.InsertNewEvents(
                                               this.SmenaID,                                           // LoginID 
                                               _odvodVyroby.CodeReadCntGet(),                  // QTY 
                                               _odvodVyroby.CodeNoReadCntGet(),                // QTYREAL
                                               description,                                            // description 
                                               _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                                               barcodeSended,                                          // BarcodeSended 
                                               ZbyvaPocet,                                             // Zakazka
                                                                                                       //TODO MaR 11.7.2023 zakomentovano a nahrazeno novym zapisem 
                                                                                                       //this.ProhazZapnut.ToString(),                           // popis 
                                               this.ProhazZapnut ? this.ProhazZapnut.ToString() : _odvodVyroby.CodeProhazZapnutGet().ToString(),      // popis
                                               "",                                                     // reporttype 
                                               "",                                                     // IDO
                                               _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                                               _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                                               _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                                                                                                       //TODO MaR 11.7. 2023 pridan pocet pytlu z prohazovani
                                                                                                       //"",                                                     // sensor 
                                               _odvodVyroby.CodeProhazCntGet().ToString(),     // sensor 
                                               this._sarze,                                            // material
                                               _vph_sopnumbe,                                          // VPH
                                               _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                                               _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                                               _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                                               _SSCC,                                                  // NMBRPAL
                                               status,                                                 // status
                                               _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                                               PackType,                                               // PackType
                                               0,                                                      // WEIGHT
                                               _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                                               null,                                                   // REZ_1
                                               null,                                                   // REZ_2
                                               null,                                                   // REZ_3
                                               null,                                                   // REZ_4
                                               null                                                    // REZ_5
                                               );

                    }
                    else
                    {
                        ulozeni = Classes.Database_AGRO2.InsertNewEvents(
                                                this.SmenaID,                                           // LoginID 
                                                _odvodVyroby.CodeReadCntGetAndReset(),                  // QTY 
                                                _odvodVyroby.CodeNoReadCntGetAndReset(),                // QTYREAL
                                                description,                                            // description 
                                                _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                                                barcodeSended,                                          // BarcodeSended 
                                                ZbyvaPocet,                                             // Zakazka
                                                                                                        //TODO MaR 11.7.2023 zakomentovano a nahrazeno novym zapisem 
                                                                                                        //this.ProhazZapnut.ToString(),                           // popis 
                                                this.ProhazZapnut ? this.ProhazZapnut.ToString() : _odvodVyroby.CodeProhazZapnutAndReset().ToString(),      // popis
                                                "",                                                     // reporttype 
                                                "",                                                     // IDO
                                                _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                                                _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                                                _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                                                                                                        //TODO MaR 11.7. 2023 pridan pocet pytlu z prohazovani
                                                                                                        //"",                                                     // sensor 
                                                _odvodVyroby.CodeProhazCntGetAndReset().ToString(),     // sensor 
                                                this._sarze,                                            // material
                                                _vph_sopnumbe,                                          // VPH
                                                _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                                                _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                                                _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                                                _SSCC,                                                  // NMBRPAL
                                                status,                                                 // status
                                                _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                                                PackType,                                               // PackType
                                                0,                                                      // WEIGHT
                                                _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                                                null,                                                   // REZ_1
                                                null,                                                   // REZ_2
                                                null,                                                   // REZ_3
                                                null,                                                   // REZ_4
                                                null                                                    // REZ_5
                                                );
                    }



                    dtSaveLast = DateTime.Now;
                    if (odvodVyrobyReset)
                        _odvodVyroby.Reset();
                }
            }
            catch (Exception exSave)
            {
                //Exceptions.Handler.ErrorHandle(exSave.Message, "VyrobaActualDataSave", false);
                ExceptionHandler2.Handle(exSave.Message, "VyrobaActualDataSave", false);
            }
        }

        #region Scanner

        public void VyrobaActualDataSaveAndResetActualBarcode( string description, string barcodeSended)
        {
            try
            {
                lock (_VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject)
                {
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeReadCnt.ToString() + "," + _odvodVyroby.CodeNoReadCnt.ToString());
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //if (_odvodVyroby.CodeAllCount > 0)
                    //{   //pokud neco v bufferu bylo, pak zapis
                    //    Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //}

                    int pocetPytlu = 0;
                    int pocetP = 0;
                    int QTYpacktmp = 0;

                    try
                    { pocetPytlu = (int)(AktualDataRow.qty + AktualDataRow.qtyReal); }
                    catch (Exception ex) 
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                        Fask.Logging.ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                    }


                    try
                    { pocetP = AktualDataRow.IspocetPaletNull() ? 0 : AktualDataRow.pocetPalet; }
                    catch (Exception ex) 
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                        Fask.Logging.ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                    }

                    try
                    { QTYpacktmp = (int)(VPP_Row.IsQTYPACKMJNull() ? 0 : VPP_Row.QTYPACK); }
                    catch (Exception ex) 
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                        Fask.Logging.ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                    }



                    int x = pocetPytlu - (pocetP * QTYpacktmp);
                    var ZbyvaPocet = string.Format("Δ:{0}", x);

                    // 22.6.2021 - Ulozim i nuly ... 
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    // scan1=counter0+3, scan2=counter1+3, scan3=counter2+3
                   bool stavUlozeni = Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        //_odvodVyroby.CodeReadCntGetAndReset(),                // QTY 
                        //_odvodVyroby.CodeNoReadCntGetAndReset(),              // QTYREAL
                        _odvodVyroby.CodeReadCnt,                               // QTY 
                        _odvodVyroby.CodeNoReadCnt,                             // QTYREAL
                        description,                                            // description 
                        _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                        barcodeSended,                                          // BarcodeSended 
                        ZbyvaPocet,                                             // Zakazka
                        this.ProhazZapnut.ToString(),                           // popis 
                        "",                                                     // reporttype 
                        "",                                                     // IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        "",                                                     // sensor 
                        this._sarze,                                            // material
                        _vph_sopnumbe,                                          // VPH
                        _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                        _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                        _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                        _SSCC,                                                  // NMBRPAL
                        null,                                                   // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        string.Empty,                                           // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                        );

                    dtSaveLast = DateTime.Now;
                    //if (odvodVyrobyReset)
                    //    _odvodVyroby.Reset();
                }
            }
            catch (Exception exSave)
            {
                ExceptionHandler2.Handle(exSave.Message, "VyrobaActualDataSave", false);
            }
        }


        #region uklodani do lokalni databaze prohazovani

        public void VyrobaActualDataSaveAndResetActualBarcode(string description, string barcodeSended, bool prohaz)
        {
            try
            {
                lock (_VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject)
                {
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeReadCnt.ToString() + "," + _odvodVyroby.CodeNoReadCnt.ToString());
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //if (_odvodVyroby.CodeAllCount > 0)
                    //{   //pokud neco v bufferu bylo, pak zapis
                    //    Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //}

                    int pocetPytlu = 0;
                    int pocetP = 0;
                    int QTYpacktmp = 0;

                    try
                    { pocetPytlu = (int)(AktualDataRow.qty + AktualDataRow.qtyReal); }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                        Fask.Logging.ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                    }


                    try
                    { pocetP = AktualDataRow.IspocetPaletNull() ? 0 : AktualDataRow.pocetPalet; }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                        Fask.Logging.ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                    }

                    try
                    { QTYpacktmp = (int)(VPP_Row.IsQTYPACKMJNull() ? 0 : VPP_Row.QTYPACK); }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                        Fask.Logging.ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                    }



                    int x = pocetPytlu - (pocetP * QTYpacktmp);
                    var ZbyvaPocet = string.Format("Δ:{0}", x);

                    // 22.6.2021 - Ulozim i nuly ... 
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    // scan1=counter0+3, scan2=counter1+3, scan3=counter2+3
                    bool stavUlozeni = Classes.Database_AGRO2.InsertNewEvents(
                         this.SmenaID,                                           // LoginID 
                                                                                 //_odvodVyroby.CodeReadCntGetAndReset(),                // QTY 
                                                                                 //_odvodVyroby.CodeNoReadCntGetAndReset(),              // QTYREAL
                         _odvodVyroby.CodeReadCnt,                               // QTY 
                         _odvodVyroby.CodeNoReadCnt,                             // QTYREAL
                         description,                                            // description 
                         _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                         barcodeSended,                                          // BarcodeSended 
                         ZbyvaPocet,                                             // Zakazka
                         this.ProhazZapnut.ToString(),                           // popis 
                         "",                                                     // reporttype 
                         "",                                                     // IDO
                         _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                         _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                         _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                         _odvodVyroby.CodeProhazCntGet().ToString(),     // sensor 
                         this._sarze,                                            // material
                         _vph_sopnumbe,                                          // VPH
                         _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                         _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                         _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                         //_SSCC,                                                  // NMBRPAL
                          string.Empty,                                                  // NMBRPAL
                         null,                                                   // status
                         _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                         string.Empty,                                           // PackType
                         0,                                                      // WEIGHT
                         _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                         null,                                                   // REZ_1
                         null,                                                   // REZ_2
                         null,                                                   // REZ_3
                         null,                                                   // REZ_4
                         null                                                    // REZ_5
                         );

                    dtSaveLast = DateTime.Now;
                    //if (odvodVyrobyReset)
                    //    _odvodVyroby.Reset();
                }
            }
            catch (Exception exSave)
            {
                ExceptionHandler2.Handle(exSave.Message, "VyrobaActualDataSave", false);
            }
        }
        #endregion



        #endregion


        public void VyrobaActualDataSaveAndResetActualBarcode(bool odvodVyrobyReset, string description, string barcodeSended, decimal qty, decimal qtyreal,int? status, int? PocetPalet = null, string PackType = "", string PP_Button = "-", int zadanyPocetPytlu = 0)
        {
            try
            {
                lock (_VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject)
                {
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeReadCnt.ToString() + "," + _odvodVyroby.CodeNoReadCnt.ToString());
                    //Classes.Database.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //if (_odvodVyroby.CodeAllCount > 0)
                    //{   //pokud neco v bufferu bylo, pak zapis
                    //    Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    //}

                    // 22.6.2021 - Ulozim i nuly ... 
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());
                    //Classes.Database.InsertNewEvents(this.SmenaID, _odvodVyroby.CodeReadCntGetAndReset(), _odvodVyroby.CodeNoReadCntGetAndReset(), description, _odvodVyroby.BarcodeActual, barcodeSended, string.Empty, this.ProhazZapnut.ToString(), "", "", "", "", "", "", this._sarze);
                    // scan1=counter0+3, scan2=counter1+3, scan3=counter2+3


                    int pocetPytlu = 0;
                    int pocetP = 0;
                    int QTYpacktmp = 0;
                    string p1 = "-";
                    string p2 = "-";

                    try
                    { pocetPytlu = (int)(AktualDataRow.qty + AktualDataRow.qtyReal); }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                    }


                    try
                    { pocetP = AktualDataRow.IspocetPaletNull() ? 0 : AktualDataRow.pocetPalet; }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                    }

                    try
                    { QTYpacktmp = (int)(VPP_Row.IsQTYPACKMJNull() ? 0 : VPP_Row.QTYPACK); }
                    catch (Exception ex) 
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                    }


                    try
                    {  p1 = PocetPalet.HasValue ? PocetPalet.Value.ToString() : "-"; }
                    catch (Exception ex)
                    { 
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode p1", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode p1", false);
                    }


                    try
                    {  p2 = AktualDataRow.IspocetPaletNull() ? "-" : AktualDataRow.pocetPalet.ToString(); }
                    catch (Exception ex) 
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode p2", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode p2", false);
                    }

                    //int x = (pocetPytlu - (pocetP * QTYpacktmp)) + zadanyPocetPytlu;
                    int x = pocetPytlu - ((pocetP * QTYpacktmp) + zadanyPocetPytlu);
                    string pal =  string.Format("{0},Δ:{1}/r:{2}/a:{3}", PP_Button, x , p1, p2);
                    string sscc_local = _SSCC;

                    //if (PackType == "NP" || PackType == "ZZ")
                    //    sscc_local = _SSCC;


                    Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        qty,                                                    // QTY 
                        qtyreal,                                                // QTYREAL
                        description,                                            // description 
                        _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                        barcodeSended,                                          // BarcodeSended 
                        pal,                                                    // Zakazka
                        //TODO MaR 11.7.2023 zakomentovano a nahrazeno novym zapisem 
                        //this.ProhazZapnut.ToString(),                           // popis 
                        _odvodVyroby.CodeProhazZapnutAndReset().ToString(),      // popis
                        "",                                                     //reporttype 
                        "",                                                     //IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        //TODO MaR 11.7. 2023 pridan pocet pytlu z prohazovani
                        //"",                                                     // sensor 
                        _odvodVyroby.CodeProhazCntGetAndReset().ToString(),     // sensor 
                        this._sarze,                                            // material
                        _vph_sopnumbe,                                          // VPH
                        _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                        _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                        _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                        sscc_local,                                             // NMBRPAL
                        status,                                                 // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        PackType,                                               // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                        );

                    dtSaveLast = DateTime.Now;

                    if (odvodVyrobyReset)
                        _odvodVyroby.Reset();
                }
            }
            catch (Exception exSave)
            {
                //Exceptions.Handler.ErrorHandle(exSave.Message, "VyrobaActualDataSave", false);
                ExceptionHandler2.Handle(exSave.Message, "VyrobaActualDataSave", false);
            }
        }

        public void VyrobaActualDataSaveAndResetActualBarcode(
            bool odvodVyrobyReset, 
            string description, 
            string barcodeSended, 
            decimal qty, 
            decimal qtyreal,
            VyrPrikaz prikaz = null)
        {
            try
            {
                lock (_VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject)
                {
                    int pocetPytlu = 0;
                    int pocetP = 0;
                    int QTYpacktmp = 0;
                    int zadanyPocetPytlu = 0;

                    try
                    { pocetPytlu = (int)(AktualDataRow.qty + AktualDataRow.qtyReal); }
                    catch (Exception ex) 
                    { 
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                    }


                    try
                    { pocetP = AktualDataRow.IspocetPaletNull() ? 0 : AktualDataRow.pocetPalet; }
                    catch (Exception ex)
                    {
                       // Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                    }

                    try
                    { QTYpacktmp = (int)(VPP_Row.IsQTYPACKMJNull() ? 0 : VPP_Row.QTYPACK); }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                    }

                    try
                    { zadanyPocetPytlu = prikaz.QTY_Pytlu_NP; }
                    catch (Exception ex) 
                    { 
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode zadanyPocetPytlu", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode zadanyPocetPytlu", false); 
                    }


                    

                    //int x = (pocetPytlu - (pocetP * QTYpacktmp)) + zadanyPocetPytlu;
                    int x = pocetPytlu - ((pocetP * QTYpacktmp) + zadanyPocetPytlu);
                    var ZbyvaPocet = string.Format("Δ:{0}", x);

                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());

                    if (prikaz == null)
                    {
                            Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        qty,                                                    // QTY 
                        qtyreal,                                                // QTYREAL
                        description,                                            // description 
                        _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                        barcodeSended,                                          // BarcodeSended 
                        ZbyvaPocet,                                                     // Zakazka
                        this.ProhazZapnut.ToString(),                           // popis 
                        "",                                                     //reporttype 
                        "",                                                     //IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        "",                                                     // sensor 
                        this._sarze,                                            // material
                        _vph_sopnumbe,                                          // VPH
                        _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                        _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                        _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                        _SSCC,                                                  // NMBRPAL
                        null,                                                    // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        string.Empty,                    // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                        ); 
                    }
                    else
                    {
                Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        qty,                                                    // QTY 
                        qtyreal,                                                // QTYREAL
                        description,                                            // description 
                        prikaz.BarcodeReaded,                                   // BarcodeReaded 
                        prikaz.BarcodeSended,                                   // BarcodeSended 
                        "",                                                     // Zakazka
                        this.ProhazZapnut.ToString(),                           // popis 
                        "",                                                     //reporttype 
                        "",                                                     //IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        "",                                                     // sensor 
                        this._sarze,                                            // material
                        prikaz.VPH_SOPNUMBER,                                   // VPH
                        prikaz.VPP_row_ID,                                      // VPP
                        prikaz.VPP_row_BarcodeP,                                // EAN_IS
                        prikaz.VPP_row_ITEMNMBR,                                // IS_ID
                        _SSCC,                                                  // NMBRPAL
                        null,                                                   // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        string.Empty,                                           // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                                );
                    }

                    dtSaveLast = DateTime.Now;

                    if (odvodVyrobyReset)
                        _odvodVyroby.Reset();
                }
            }
            catch (Exception exSave)
            {
               // Exceptions.Handler.ErrorHandle(exSave.Message, "VyrobaActualDataSave", false);
                ExceptionHandler2.Handle(exSave.Message, "VyrobaActualDataSave", false);
            }
        }

        public void VyrobaActualDataSave( string description)
        {
            try
            {
                lock (_VyrobaActualDataSaveAndResetActualBarcodeSynchronizationObject)
                {

                    int pocetPytlu = 0;
                    int pocetP = 0;
                    int QTYpacktmp = 0;

                    try
                    { pocetPytlu = (int)(AktualDataRow.qty + AktualDataRow.qtyReal); }
                    catch (Exception ex)
                    {
                       // Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetPytlu", false);
                    }


                    try
                    { pocetP = AktualDataRow.IspocetPaletNull() ? 0 : AktualDataRow.pocetPalet; }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode pocetP", false);
                    }

                    try
                    { QTYpacktmp = (int)(VPP_Row.IsQTYPACKMJNull() ? 0 : VPP_Row.QTYPACK); }
                    catch (Exception ex)
                    {
                        //Exceptions.Handler.ErrorHandle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                        ExceptionHandler2.Handle(ex.Message, "VyrobaActualDataSaveAndResetActualBarcode QTYpacktmp", false);
                    }


                    int x = pocetPytlu - (pocetP * QTYpacktmp);
                    var ZbyvaPocet = string.Format("Δ:{0}", x);
                    // 22.6.2021 - Ulozim i nuly ... 
                    Classes.Database_AGRO2.InsertNewUserEvents(this.SmenaID, AgroConfig.LOG_ULOZENI_ODVOD_VYROBA, _odvodVyroby.BarcodeActual, _odvodVyroby.CodeAllCount.ToString());

                    Classes.Database_AGRO2.InsertNewEvents(
                        this.SmenaID,                                           // LoginID 
                        0,                                                      // QTY 
                        0,                                                      // QTYREAL
                        description,                                            // description 
                        _odvodVyroby.BarcodeActual,                             // BarcodeReaded 
                        getBarcodeSended(),                                     // BarcodeSended 
                        ZbyvaPocet,                                             // Zakazka
                        this.ProhazZapnut.ToString(),                           // popis 
                        string.Empty,                                           //reporttype 
                        string.Empty,                                           //IDO
                        _cidla_vstupni[0].SensorsCnt.ToString(),                // "", // scan1 - prubezna hondota cidla 0
                        _cidla_vstupni[1].SensorsCnt.ToString(),                // "", // scan2 - prubezna hondota cidla 1
                        _cidla_vstupni[2].SensorsCnt.ToString(),                // "", // scan3 - prubezna hondota cidla 2
                        "",                                                     // sensor 
                        this._sarze,                                            // material
                        _vph_sopnumbe,                                          // VPH
                        _vpp_row == null ? -1 : _vpp_row.DEX_ROW_ID,            // VPP
                        _vpp_row == null ? string.Empty : _vpp_row.BarcodeP,    // EAN_IS
                        _vpp_row == null ? string.Empty : _vpp_row.ITEMNMBR,    // IS_ID
                        string.Empty,                                           // NMBRPAL
                        null,                                                   // status
                        _vpp_row == null ? 0 : _vpp_row.QTYPACK,                // QTYPACK
                        string.Empty,                                           // PackType
                        0,                                                      // WEIGHT
                        _vpp_row == null ? (byte)1 : _vpp_row.BarcodeT,         // BarcodeT
                        null,                                                   // REZ_1
                        null,                                                   // REZ_2
                        null,                                                   // REZ_3
                        null,                                                   // REZ_4
                        null                                                    // REZ_5
                        );

                    dtSaveLast = DateTime.Now;
                }
            }
            catch (Exception exSave)
            {
                //Exceptions.Handler.ErrorHandle(exSave.Message, "VyrobaActualDataSave", false);
                ExceptionHandler2.Handle(exSave.Message, "VyrobaActualDataSave", false);
            }
        }

        #endregion

        #region Metoda která vrací BarcodeSended

        private string getBarcodeSended(string kod = null)
        {

            int SettingsKod = AgroConfig.config.Agro[0].ZapisovanyKodDoBarcodeSended; //EAN_IS
            //int kod = 1;

            if (SettingsKod == 0)
            {
                return _vpp_row == null ? string.Empty : _vpp_row.BarcodeP;
            }
            else if (SettingsKod == 1)
            {
                if (kod != null)
                    return kod;
                else
                    return _odvodVyroby.BarcodeActual;
            }
            else
                return string.Empty;
        }

        #endregion

    }
}