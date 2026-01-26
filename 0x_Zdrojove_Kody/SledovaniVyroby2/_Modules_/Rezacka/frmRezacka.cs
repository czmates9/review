using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.ModuleIfc;
using FASK.SledovaniVyroby.CountersIfc;
using Fask.Emailing;
//using Database.VyrobaTableAdapters;
using System.Reflection;
using System.IO;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Threading;
using Fask.Logging;
using ICommDatabase;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    public partial class frmRezacka : Form, IModuleConnector
    {
        //TODO: prace s citacem by mela byt vzdy v try catch bloku, protoze v knihovne uz neni zachytavani vyjimek!
        //Promenna pro praci s citacem
        ICounterConnector counterConnector = null;

#if DEBUG
        //Promenna, ktera udava, zda je pripojen citac - pokud je true, program pracuje, ale nekomunikuje s citacem - funkce jsou preskoceny, nedochazi k vyjimkam
        const bool witoutCounter = true;
#else
        //V releasu pracuji s citacem!
        const bool witoutCounter = false;
#endif


        //Pripojeni k databazi
        SqlConnection sqlConnection = null;

        //Pomocna operace se kterou se pracuje (nacitani, kopie...) a vzdy aktualni hodnota sensoru
        Operation operation = null;
        string sensorValue = ((int)0).ToString();

        //Zobrazene operace - jedna vyrobni a jedna volna
        OperationUC manuOper = null;
        OperationUC freeOper = null;

        //Zobrazeni historie
        //Panel pnl = new Panel();
        HistoryUC history = new HistoryUC();

        //Promenne pro jednotlive operace - tzn volne a vyrobni (1 a 1)
        FreeGlobals freeGlobals = new FreeGlobals();
        ManuGlobals manuGlobals = new ManuGlobals();

        //Semafor pro synchronizaci vraceni vysledku a prebirani hodnot a promenna pro vysledky
        Semaphore s = new Semaphore(0, 1);
        private static string sharedResult = string.Empty;

        //Ikona zmeny stavu
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Konstruktor
        public frmRezacka()
        {
            //Inicializace komponent
            InitializeComponent();
            //Inicializace portu
            try { InitializePorts(); }
            catch { }
            //Inicializace citace
            try
            {
                InitializeCounter();
                //Inicializace nastaveni
                InitializeSettings();
                //Inicializace pripojeni k DB
                sqlConnection = new SqlConnection();
                //Inicializace historie - pokud se nepovede, nevadi
                InitializeHistory(); 
                //Univerzalni operace pro praci
                operation = initOperation(false);
                //A dve komponenty - vyrobni a volna operace
                manuOper = initOperationUC(false);
                freeOper = initOperationUC(true);
                //Pokusim se obnovit stav operace - pokud se to nepovede, standardni inicializace
                restoreState();
                //Vlozeni operaci do formu - kazdem pripade
                insertOperationToForm(manuOper);
                insertOperationToForm(freeOper);
                //Vlozeni historie do formu
                insertHistoryToForm();
                //Vlozeni informaci do formu
                //insertInfoToForm();
                //Inicializace informaci
                //InitializeInfo();
                /*
                //Vlozeni panelu
                pnlOperations.Controls.Add(pnl);
                pnl.BringToFront();
                pnl.Dock = DockStyle.Fill;
                pnl.Show();
                 */
            }
            catch (Exception ex)
            {
                ClosePorts();
                procureException(ex);
            }
        }

        //Nastaveni prvku gui dle nacteneho nastaveni
        private void InitializeSettings()
        {
            //Nastaveni podle konfigurace
            try
            {
                chbHistory.Checked = RezackaConfig.config.Other[0].AllowHistory;
                numNumberObHistooryRecords.Value = RezackaConfig.config.Other[0].NumberOfHistoryRecords;
                numHistoryHeight.Value = RezackaConfig.config.Other[0].HeightHistory;
                numBMDecimalPlaces.Value = RezackaConfig.config.Other[0].BMDecimalPlaces;

                //Nacteni nastavenych operaci u kterych se prehlasuje
                for (int i = 0; i < RezackaConfig.config.LoginOperations.Rows.Count; i++)
                {
                    
                    txtLoginOperations.Text += ((i == 0 ? string.Empty : ", ") + RezackaConfig.config.LoginOperations[i].CKOP);
                }
            }
            catch { /*Pokud se nepodari, nic se nedeje*/ }
        }

        //Inicializuje informace ohledne beznych metru
        private void InitializeInfo()
        {
            //Koeficient
            //info.txtKoeficient.Text = LogConfig.Koeficient.ToString();
            //Pokud se nacetly nejake zakazky - obnovila se, musi se donacit pocet impulsu, ktere u teto zakazky byly
            //TODO: je treba poresit obnovu zakazky - donacteni vsech impulsu v ramci ni.

        }

        //Vlozi panel s historii do formu
        private void insertHistoryToForm()
        {
            history.Dock = DockStyle.Bottom;
            history.SendToBack();

            //Vlozeni do panelu
            pnlOperations.Controls.Add(history);
            //pnl.Controls.Add(history);
        }

        //Inicializace komponenty zobrazujici historii
        private void InitializeHistory()
        {
            //Nacteni historie
            history.readHistoricalOperation(int.Parse(numNumberObHistooryRecords.Value.ToString()));

            //Zobrazeni a sirka
            history.Visible = chbHistory.Checked;
            history.Height = int.Parse(numHistoryHeight.Value.ToString());

            //Sirka sloupcu
            history.loadColumnsWidth();
        }

        //Pokusi se obnovit stav
        private void restoreState()
        {
            //INFO: pri obnovovani stavu vznikaji smerem k db 2 pripojeni - jedno nyni, do tabulky fask_currentstate a druhe v evalRestore->readInformationAboutOperation
            SqlConnection connection = new SqlConnection();

            try
            {
                //Pripojeni
                connection.ConnectionString = LogConfig.SqlConnectionStringLocal;
                connection.Open();

                //Prikaz
                SqlCommand cmd = new SqlCommand(Queries.restoreOperations, connection);

                //Parametry pro vyrobni operaci
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                cmd.Parameters.Add(new SqlParameter("@operationtype", "M"));

                //Adapter
                SqlDataReader reader = cmd.ExecuteReader();

                //Vyhodnoceni
                if (!evalRestore(reader, manuOper))
                {
                    manuOper.setCurrentOperation(manuOper.getCurrentOperation(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    //showInitOPeration(manuOper, manuOper.getCurrentOperation());
                }

                //Parametry pro volnu operaci
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                cmd.Parameters.Add(new SqlParameter("@operationtype", "F"));

                //Adapter
                reader = cmd.ExecuteReader();

                if (!evalRestore(reader, freeOper))
                {
                    freeOper.setCurrentOperation(freeOper.getCurrentOperation(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    //showInitOPeration(freeOper, freeOper.getCurrentOperation());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Uzaviram spojeni
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        //Vyhodnoti, zda se podarilo neco obnovit
        private bool evalRestore(SqlDataReader reader, OperationUC opUC)
        {
            //Tabulka
            DataTable table = new DataTable();
            table.Load(reader);

            //Kontrola zda se bude obnovovat - nebude jen v pripade prazdne db, tzn 0 radku, nebo pokud byla posledni operaci init
            if (table.Rows.Count != 1) return false;
            
            //Posledni byla init - nyni zrejme nemozne
            string opBarcode = (table.Rows[0]["barcode"]).ToString().Trim();
            if (opBarcode == string.Empty) return false;

            //Posledni byla jina nez init - obnovim
            if (!readInformationAboutOperation(opBarcode))
            {
                throw new ApplicationException("Nepodaøilo se obnovit stav aplikace - naèíst informace k operaci " + opBarcode + ".");
            }

            //Pokud byla koncova - zobrazim jakoby init
            if (operation.KONEC) return false;

            //Nactu budouci operace
            if (!readNextOperations(operation.IDO))
            {
                throw new ApplicationException("Nepodaøilo se obnovit stav aplikace - naèíst následující operace k operaci " + operation.IDO + ".");
            }

            //Ulozeni zakazky a materialu
            string zakazka = string.Empty, material = string.Empty, polozka = string.Empty;
            if(operation.VOLNA)
            {
                zakazka = freeGlobals.Zakazka = (table.Rows[0]["zakazka"]).ToString().Trim();
                material = freeGlobals.Material = (table.Rows[0]["material"]).ToString().Trim();
                polozka = freeGlobals.Polozka = (table.Rows[0]["polozka"]).ToString().Trim();
                //Nastaveni citacu na nactenou hodnotu - naposledy ulozena hodnota
                freeGlobals.PastCounter = freeGlobals.Counter = uint.Parse((table.Rows[0]["sensor"]).ToString().Trim());
            }
            else
            {
                zakazka = manuGlobals.Zakazka = (table.Rows[0]["zakazka"]).ToString().Trim();
                material = manuGlobals.Material = (table.Rows[0]["material"]).ToString().Trim();
                polozka = manuGlobals.Polozka = (table.Rows[0]["polozka"]).ToString().Trim();
                //Nastaveni citacu na nactenou hodnotu - naposledy ulozena hodnota
                manuGlobals.PastCounter = manuGlobals.Counter = uint.Parse((table.Rows[0]["sensor"]).ToString().Trim());
            }

            //Nastaveni operace
            opUC.setCurrentOperation(operation, (table.Rows[0]["scan1"]).ToString().Trim(), (table.Rows[0]["scan2"]).ToString().Trim(), (table.Rows[0]["scan3"]).ToString().Trim(), zakazka, material, polozka);

            //Zobrazeni
            showBM(opUC, uint.Parse((table.Rows[0]["sensor"]).ToString().Trim()));

            //Dotazeni infa
            if(operation.VOLNA) opUC.readAdditionalInformation(freeGlobals.Zakazka, freeGlobals.Material);
            else opUC.readAdditionalInformation(manuGlobals.Zakazka, manuGlobals.Material);

            //Finalizer
            finalizeOperation();

            //Ok
            return true;
        }

        //Zobrazeni operace do formu rezacky
        private void insertOperationToForm(OperationUC oper)
        {
            //Text komponenty
            string freeOperationText = "Aktuální volná operace";
            string manuOperationText = "Aktuální výrobní operace";

            //Nastaveni
            if (oper.getCurrentOperation().VOLNA)
            {
                oper.Dock = DockStyle.Right;
                oper.ComponentText = freeOperationText;
            }
            else
            {
                oper.Dock = DockStyle.Left;
                oper.ComponentText = manuOperationText;
            }

            //Vlozeni
            pnlOperations.Controls.Add(oper);
        }

        //Inicializuje operaci - do budoucna jakakoliv nutna inicializace
        private Operation initOperation(bool free)
        {
            return new Operation(free);
        }

        //Inicializace komponent - do budoucna jakakoliv nutna inicializace
        private OperationUC initOperationUC(bool free)
        {
            OperationUC opUc = new OperationUC(free,statusLabel);
            opUc.btnReadParams.Click += new EventHandler(buttonReadParams_Click);
            opUc.txtOperationBarcode.KeyDown += new KeyEventHandler(txtOperationBarcode_KeyDown);
            return opUc;
        }

        //Inicializace citace
        private void InitializeCounter()
        {
            //Musi byt jeden radek v tabulce s nastavenim
            if (RezackaConfig.config.Counter.Rows.Count > 0)
            {
                string ass = (string)RezackaConfig.config.Counter.Rows[0]["Assembly"];
                string obj = (string)RezackaConfig.config.Counter.Rows[0]["Object"];

                //Dynamicke nacteni
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                string binPath = (new Uri(Path.GetDirectoryName(executingAssembly.CodeBase))).AbsolutePath;
                Assembly assembly = Assembly.LoadFrom(Path.Combine(binPath, ass));
                counterConnector = assembly.CreateInstance(obj) as ICounterConnector;

                //A zobrazeni
                txtAssembly.Text = ass;
                txtObject.Text = obj;
            }
        }

        //Vypnuti x Zapnuti vstupniho SP (scanner)
        private void buttonStartStopIN_Click(object sender, EventArgs e)
        {
            //Zmena stavu
            SerialPortINChangeState();
        }

        //Zmena stavu vstupniho SP
        private void SerialPortINChangeState()
        {
            try
            {
                //Pokud je otevreny, ..
                if (serialPortIN.IsOpen)
                {
                    //.. uzavreme
                    serialPortIN.Close();
                }
                //Jinak
                else
                {
                    //Nacteme parametry
                    serialPortIN.BaudRate = int.Parse(RezackaConfig.config.Rezacka[0].SP_IN_BaudRate);
                    serialPortIN.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), RezackaConfig.config.Rezacka[0].SP_IN_Parity);
                    serialPortIN.PortName = RezackaConfig.config.Rezacka[0].SP_IN_PortName;
                    serialPortIN.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), RezackaConfig.config.Rezacka[0].SP_IN_StopBits);
                    serialPortIN.DataBits = int.Parse(RezackaConfig.config.Rezacka[0].SP_IN_DataBits);

                    //a otevreme
                    serialPortIN.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }

            //Zmena barvy
            if (serialPortIN.IsOpen) buttonStartStopIN.BackColor = Color.Green;
            else buttonStartStopIN.BackColor = Color.Red;
        }

        //Vypnuti x Zapnuti vystupniho SP (citac)
        private void buttonStartStopOUT_Click(object sender, EventArgs e)
        {
            SerialPortOUTChangeState();
        }

        //Zmena stavu vystupniho serioveho portu
        private void SerialPortOUTChangeState()
        {
            try
            {
                //Pokud je otevreny uzavru
                if (serialPortOUT.IsOpen)
                {
                    serialPortOUT.Close();
                }
                //Jinak nastavim a otevru
                else
                {
                    serialPortOUT.BaudRate = int.Parse(RezackaConfig.config.Rezacka[0].SP_OUT_BaudRate);
                    serialPortOUT.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), RezackaConfig.config.Rezacka[0].SP_OUT_Parity);
                    serialPortOUT.PortName = RezackaConfig.config.Rezacka[0].SP_OUT_PortName;
                    serialPortOUT.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), RezackaConfig.config.Rezacka[0].SP_OUT_StopBits);
                    serialPortOUT.DataBits = int.Parse(RezackaConfig.config.Rezacka[0].SP_OUT_DataBits);

                    serialPortOUT.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }

            //Nastaveni barvy
            if (serialPortOUT.IsOpen) buttonStartStopOUT.BackColor = Color.Green;
            else buttonStartStopOUT.BackColor = Color.Red;
        }

        //Inicializace portu
        //Inicializace poru
        private void InitializePorts()
        {
            //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
            if (RezackaConfig.config.Rezacka.Rows.Count == 0)
            {
                RezackaConfig.config.Rezacka.AddRezackaRow(
                serialPortIN.PortName, serialPortOUT.PortName,
                serialPortOUT.DataBits.ToString(), serialPortIN.DataBits.ToString(),
                serialPortOUT.Parity.ToString(), serialPortIN.Parity.ToString(),
                serialPortOUT.StopBits.ToString(), serialPortIN.StopBits.ToString(),
                serialPortOUT.BaudRate.ToString(), serialPortIN.BaudRate.ToString()
                );
                //Ulozeni
                RezackaConfig.Save();
            }

            try
            {
                //Vlozeni do textboxuu
                txt_spinBaudRate.Text = RezackaConfig.config.Rezacka[0].SP_IN_BaudRate; //serialPortIN.BaudRate.ToString();
                txt_spinParity.Text = RezackaConfig.config.Rezacka[0].SP_IN_Parity; //serialPortIN.Parity.ToString();
                txt_spinPort.Text = RezackaConfig.config.Rezacka[0].SP_IN_PortName; //serialPortIN.PortName;
                txt_spinStopBits.Text = RezackaConfig.config.Rezacka[0].SP_IN_StopBits; //serialPortIN.StopBits.ToString();
                txt_spinDataBits.Text = RezackaConfig.config.Rezacka[0].SP_IN_DataBits; //serialPortIN.DataBits.ToString();
            }
            catch { }

            try
            {
                //Vlozeni do textboxuu
                txt_spoutBaudRate.Text = RezackaConfig.config.Rezacka[0].SP_OUT_BaudRate; //serialPortOUT.BaudRate.ToString();
                txt_spoutParity.Text = RezackaConfig.config.Rezacka[0].SP_OUT_Parity; //serialPortOUT.Parity.ToString();
                txt_spoutPort.Text = RezackaConfig.config.Rezacka[0].SP_OUT_PortName; //serialPortOUT.PortName;
                txt_spoutStopBits.Text = RezackaConfig.config.Rezacka[0].SP_OUT_StopBits; //serialPortOUT.StopBits.ToString();
                txt_spoutDataBits.Text = RezackaConfig.config.Rezacka[0].SP_OUT_DataBits; //serialPortOUT.DataBits.ToString();
            }
            catch { }

            //Zmena stavu obou portu
            SerialPortINChangeState();
            SerialPortOUTChangeState();
        }

        //Delagat pro asynchronni volani pri prijeti dat
        delegate void DataReceivedDelegate(string data);

        //Prijem dat - scanner
        private void ScannerDataReceived(string data)
        {
            //Log-event
            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeRead, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            //Kontrola nacteneho kodu
            checkLoadedBarcode(data);
        }

        //Prijeti dat na portu - scanner
        private void serialPortIN_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //Nacteni dat
            string data = string.Empty;
            try { data = serialPortIN.ReadLine(); }
            catch { data = serialPortIN.ReadExisting(); }

            //Vyvolani metody formu
            BeginInvoke(new DataReceivedDelegate(ScannerDataReceived), data.Trim());
        }

        //Prijeti dat na portu - citac
        private void serialPortOUT_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //Pridavam zpet LF, aby byla odpoved kompletni
            string data = string.Empty;
            try { data = serialPortOUT.ReadLine() + '\n'; }
            catch { data = serialPortIN.ReadExisting(); }

            //Log-event
            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.InstructionReaded, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            //Ulozim hodnotu do sdilene promenne
            sharedResult = data;

            //Odemknu semafor, hlavni vlakno muze pokracovat (bud je stopnute, nebo se na nej prepne a bude cekat -> tzn. po release uz nemusi)
            s.Release();
        }

        //INFO: asynchronni operace se nyni nepouziva!
        /*
         * Asynchronni volani s dotazem na databazi:
         * 1.click na tlacitko buttonReadParams_Click vyvola readInformationAboutOperation funkci
         * 2.readInformationAboutOperation pripravi sql prikaz, zaregistruje calback funkci (handle) handlerReadInformationAboutOperation a asynchronne zacne s vykonavanim
         * 3.funkce handlerReadInformationAboutOperation provede dokonceni a uzavreni vseho nutneho a Invoke (vyvola formularovou metodu) procReadInformationAboutOperation
         * 4.procReadInformationAboutOperation zpracuje to co se nacetlo pomoci readeru.
         */

        //Stisk klavesy na textboxu
        private void txtOperationBarcode_KeyDown(object sender, KeyEventArgs e)
      {
            //Informace musely prijit od jedne z komponent -> kontrola a pretypovani
            if (!((((TextBox)sender).Parent.Parent.Parent) is OperationUC)) return;
            OperationUC op = (((TextBox)sender).Parent.Parent.Parent as OperationUC);

            //Stisk enteru nad nejakym vstupem
            if (e.KeyCode == Keys.Enter && op.OperationBarcode != string.Empty)
            {
                //Je nutne predat tlacitko, kvuli naslednemu pretypovani
                buttonReadParams_Click(op.btnReadParams, e);
            }
        }

        //Nacteni parametru CK = rucni vlozeni operace
        private void buttonReadParams_Click(object sender, EventArgs e)
        {
            try
            {
                //Informace musely prijit od jedne z komponent -> kontrola a pretypovani
                if (!((((Button)sender).Parent.Parent.Parent) is OperationUC)) return;
                OperationUC op = ((Button)sender).Parent.Parent.Parent as OperationUC;

                //Pamatuji si, zda se ma jednat o operaci volnou, nebo ne
                bool wasFree = op.getCurrentOperation().VOLNA;

                //Nactu informace k operaci, jejiz kod je vyplnen
                if (!readInformationAboutOperation(op.OperationBarcode))
                {
                    //Refresh se nepovedl;
                    return;
                }

                //Pokud operace byla volna, tak i nasledujici musi byt volna a naopak,
                //pokud operace byla vyrobni, i nasledujici musi byt
                if ((wasFree && !operation.VOLNA) || (!wasFree && operation.VOLNA))
                {
                    showApplicationError("Ovìøení zadané operace.", "Není možné míchat volné a výrobní operace");
                    return;
                }

                //Vyhodnotim, zda operace muze nasledovat
                if (!evaluateOperation())
                {
                    return;
                }

                //Zpracovani operace
                procureOperation();
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
        }

        //Zpracovani operace
        private void procureOperation()
        {
            try
            {
                //Nactu budouci operace
                if (!readNextOperations(operation.IDO))
                {
                    //Refresh se nepovedl;
                    return;
                }

                //Kontrola, zda ma operace nejake nasledujici, nebo je koncova - pokud ani jedo, chyba
                if (!operation.KONEC && operation.NEXTOPERATIONS == string.Empty)
                {
                    showApplicationError("Ovìøení následující operace", "Operace " + operation.IDO + " nemá žádnou následující operaci a není operací koncovou.");
                    return;
                }

                //Kontrola, zda k operaci nema byt naskenovano cislo zakazky a pritom nema definovan nazev procedury
                if (operation.SCANZAKAZKA > 0 && operation.SPZAKAZKA == string.Empty)
                {
                    showApplicationError("Zpracování následující operace", "Operace " + operation.IDO + " má nastaven pøíznak scanování zakázky a nemá nastavenu proceduru pro její ovìøení.");
                    return;
                }

                //Kontrola, zda k operaci nema byt kontrolovan material a pritom nema definovanu proceduru
                if (operation.KONTROLAMAT && operation.SPMATERIAL == string.Empty)
                {
                    showApplicationError("Zpracování následující operace", "Operace " + operation.IDO + " má nastaven kontroly materiálu a nemá nastavenu proceduru pro jeho ovìøení.");
                    return;
                }

                //Nejprve se naskenuje cislo zakazky, pokud je to dano operaci
                scanOrderNumber(operation.SCANZAKAZKA);

                //Podle hodnoty sensoru rozhoduje jestli se nacte a vynuluje po zacatku, nebo pred
                sensorController();
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
        }

        //Ridi prubeh programu - kdy se scanuje, uklada apod
        private void sensorController()
        {
            string scan1res = string.Empty, scan2res = string.Empty, scan3res = string.Empty;

            //Nepracovat
            if (operation.SENSOR == 0 || (operation.SENSOR & 1) > 0)
            {
                //Dodatecna scanovani
                scanBarcodes(operation.SCAN1, operation.SCAN2, operation.SCAN3, out scan1res, out scan2res, out scan3res);

                //Overeni zakazky - pokud se nepovede, konec
                if (!checkOrder(scan1res, scan2res, scan3res)) return;

                //Overeni materialu - pokud se nepovede, konec
                if (!checkMaterial(scan1res, scan2res, scan3res)) return;

                //Nacteni hodnoty sensoru - musim mit vzdy aktualni hodnotu
                servingCounterRead();

                //Zobrazeni operaci, pripadne navrat do puvodniho stavu
                showOperation(scan1res, scan2res, scan3res);

                //Donacteni hlavicek
                readHeaders();

                //Prehlaseni uzivatele
                changeUser(operation.CK, operation.LOGIN);

                //Finalizer - muze se vratit do initu
                finalizeOperation();

                //Uz se s nicim nekombinuje
                return;
            }

            //Nacist a vynulovat PRED zahajenim operace
            if ((operation.SENSOR & 2) > 0)
            {
                //Nacteni
                servingCounterRead();

                //Dodatecna scanovani
                scanBarcodes(operation.SCAN1, operation.SCAN2, operation.SCAN3, out scan1res, out scan2res, out scan3res);

                //Overeni zakazky - pokud se nepovede, konec
                if (!checkOrder(scan1res, scan2res, scan3res)) return;

                //Overeni materialu - pokud se nepovede, konec
                if (!checkMaterial(scan1res, scan2res, scan3res)) return;
            }

            //Nacist a vynulovat PO (+- nacist musim pred zapsanim do db) zahajenim operace
            if (((operation.SENSOR & 4) > 0))
            {
                //Dodatecna scanovani
                scanBarcodes(operation.SCAN1, operation.SCAN2, operation.SCAN3, out scan1res, out scan2res, out scan3res);

                //Overeni zakazky - pokud se nepovede, konec
                if (!checkOrder(scan1res, scan2res, scan3res)) return;

                //Overeni materialu - pokud se nepovede, konec
                if (!checkMaterial(scan1res, scan2res, scan3res)) return;

                //Nacteni
                servingCounterRead();
            }

            //Zobrazeni operaci - zapsani do db
            showOperation(scan1res, scan2res, scan3res);

            //Nulovani - pokud vysla zmena uzivatele
            servingCounterClear();

            //Donacteni hlavicek
            readHeaders();

            //Prehlaseni uzivatele
            changeUser(operation.CK, operation.LOGIN);

            //Finalizer - muze se vratit do initu
            finalizeOperation();
        }

        //Zmena uzivatele dana operaci
        private void changeUser(string opck, bool login)
        {
            try
            {
                //Uzavreme porty
                ClosePorts();
                //Proverim, zda se ma prelogovavat dle priznaku
                if (login)
                {
                    //Pokud ano, snazim se o prelogovani
                    //S hodnoou, kterou vrati login
                    while (!LogConfig.LogIn(true)) /*EMPTY*/ ;
                }
                //Pokud ne, proverim nastaveni
                else
                {
                    //Je li aktualni operace obsazena v nactenych hodnotach
                    for (int i = 0; i < RezackaConfig.config.LoginOperations.Rows.Count; i++)
                    {
                        //Pokud je obsazena, prehlasim
                        if (RezackaConfig.config.LoginOperations[i].CKOP == opck)
                        {
                            //S hodnoou, kterou vrati login
                            while (!LogConfig.LogIn(true)) /*EMPTY*/ ;
                            break;
                        }
                    }
                }
            }
            finally
            {
                //Vratime porty do predchoziho stavu
                ReturnPortsToPreviousState();
            }
        }

        //Overeni hodnoty zakazku - dotaz pomoci procedury
        private bool checkOrder(string scan1res, string scan2res, string scan3res)
        {
            //Dostal se az sem, takze je vse v poradku, ale neni nastaveno jmeno procedury, nebo
            //je nastaveno jmeno procedury, ale nechtel scanovani
            if (operation.SCANZAKAZKA == 0 || operation.SPZAKAZKA == string.Empty) return true;

            bool ret = false;
            SqlDataReader reader = null;
            try
            {
                //Pripojeni ke globalni databazi
                sqlConnection.ConnectionString = LogConfig.SqlConnectionStringGlobal;
                sqlConnection.Open();

                //Prikaz
                SqlCommand cmd = new SqlCommand(operation.SPZAKAZKA, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                cmd.Parameters.Add(new SqlParameter("@ido", operation.IDO));
                cmd.Parameters.Add(new SqlParameter("@zakazka", operation.VOLNA ? freeGlobals.Zakazka : manuGlobals.Zakazka));
                cmd.Parameters.Add(new SqlParameter("@scan1", (scan1res == string.Empty) ? null : scan1res));
                cmd.Parameters.Add(new SqlParameter("@scan2", (scan2res == string.Empty) ? null : scan2res));
                cmd.Parameters.Add(new SqlParameter("@scan3", (scan3res == string.Empty) ? null : scan3res));
                cmd.Parameters.Add(new SqlParameter("@sensor", operation.SENSOR));

                //Reader
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //Tabulka
                DataTable table = new DataTable();
                table.Load(reader);

                //Kotrola
                if (table.Rows.Count != 1) throw new ApplicationException("Procedura " + operation.SPZAKAZKA + " vrátila neoèekávaný poèet øádkù (" + table.Rows.Count + ")");

                //vyhodnoceni statusid a statusinfo
                ret = evaluateOrderState((int)table.Rows[0]["STATUSID"], table.Rows[0]["STATUSINFO"].ToString(), table.Rows[0]["POLOZKA"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Uzavreni readeru
                closeReader(reader);
            }

            //Navrat
            return ret;
        }

        //Overeni hodnoty materialu - dotaz pomoci procedury
        private bool checkMaterial(string scan1res, string scan2res, string scan3res)
        {
            //Dostal se az sem, takze je vse v poradku, ale neni nastaveno jmeno procedury, nebo
            //je nastaveno jmeno procedury, ale nechtel scanovani
            if (!operation.KONTROLAMAT || operation.SPMATERIAL == string.Empty) return true;

            bool ret = false;
            SqlDataReader reader = null;
            try
            {
                //Pripojeni ke globalni databazi
                sqlConnection.ConnectionString = LogConfig.SqlConnectionStringGlobal;
                sqlConnection.Open();

                //Prikaz
                SqlCommand cmd = new SqlCommand(operation.SPMATERIAL, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                cmd.Parameters.Add(new SqlParameter("@ido", operation.IDO));
                cmd.Parameters.Add(new SqlParameter("@zakazka", operation.VOLNA ? freeGlobals.Zakazka : manuGlobals.Zakazka));
                cmd.Parameters.Add(new SqlParameter("@scan1", (scan1res == string.Empty) ? null : scan1res));
                cmd.Parameters.Add(new SqlParameter("@scan2", (scan2res == string.Empty) ? null : scan2res));
                cmd.Parameters.Add(new SqlParameter("@scan3", (scan3res == string.Empty) ? null : scan3res));
                cmd.Parameters.Add(new SqlParameter("@sensor", operation.SENSOR));

                //Reader
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //Tabulka
                DataTable table = new DataTable();
                table.Load(reader);

                //Kotrola
                if (table.Rows.Count != 1) throw new ApplicationException("Procedura " + operation.SPMATERIAL + " vrátila neoèekávaný poèet øádkù (" + table.Rows.Count + ")");

                //vyhodnoceni statusid a statusinfo
                ret = evaluateMaterialState((int)table.Rows[0]["STATUSID"], table.Rows[0]["STATUSINFO"].ToString(), table.Rows[0]["MATERIAL"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Uzavreni readeru
                closeReader(reader);
            }

            //Navrat
            return ret;
        }

        //Vyhodnoceni navratove hodnoty pri kontrole zakazky a navratu polozky
        private bool evaluateOrderState(int statusID, string statusINFO, string polozka)
        {
            //statusID
            //-1 =  fatal error
            //0  =  OK
            //neco jineho - viz statusINFO
            if (statusID > 0)
            {
                if (MessageBox.Show(statusINFO + (polozka != String.Empty ? " (" + polozka + ")." : ".") + "\n\nPøejete si pøesto pokraèovat?", "Ovìøení zakázky.", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfirmOrderOk, LogConfig.LoginID, LogConfig.LoginID, LogConfig.SqlConnectionStringLocal);
                    //Ulozim zkratku polozky
                    if (operation.VOLNA) freeGlobals.Polozka = polozka ; else manuGlobals.Polozka = polozka;
                    return true;
                }
                else
                {
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfirmOrderCanceled, LogConfig.LoginID, LogConfig.LoginID, LogConfig.SqlConnectionStringLocal);
                    return false;
                }
            }
            else if (statusID < 0)
            {
                MessageBox.Show(statusINFO + " (" + polozka + ").", "Ovìøení zakázky.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Ulozim cislo materialu
            if (operation.VOLNA) freeGlobals.Polozka = polozka; else manuGlobals.Polozka = polozka;

            //statusID == 0
            return true;
        }

        //Vyhodnoceni navratove hodnoty pri kontrole materialu
        private bool evaluateMaterialState(int statusID, string statusINFO, string material)
        {
            //statusID
            //-1 =  fatal error
            //0  =  OK
            //neco jineho - viz statusINFO
            if (statusID > 0)
            {
                if (MessageBox.Show(statusINFO + (material != String.Empty ? " (" +material+ ")." : ".") + "\n\nPøejete si pøesto pokraèovat?", "Ovìøení materiálu k zakázce.", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfirmMaterialOk, LogConfig.LoginID, LogConfig.LoginID, LogConfig.SqlConnectionStringLocal);
                    //Ulozim cislo materialu
                    if (operation.VOLNA) freeGlobals.Material = material; else manuGlobals.Material = material;
                    return true;
                }
                else 
                {
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfirmMaterialCanceled, LogConfig.LoginID, LogConfig.LoginID, LogConfig.SqlConnectionStringLocal);
                    return false;
                }
            }
            else if (statusID < 0)
            {
                MessageBox.Show(statusINFO + " (" + material + ").", "Ovìøení materiálu k zakázce.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Ulozim cislo materialu
            if (operation.VOLNA) freeGlobals.Material = material; else manuGlobals.Material = material;

            //statusID == 0
            return true;
        }

        //Donacteni hlavicky a rozsirenych informaci k operaci
        private void readHeaders()
        {
            if (operation.VOLNA) freeOper.readAdditionalInformation(freeGlobals.Zakazka, freeGlobals.Material);
            else manuOper.readAdditionalInformation(manuGlobals.Zakazka, manuGlobals.Material);
        }

        //Precteni hodnoty citace
        private void servingCounterRead()
        {
            //Budeme pracovat s citacem dle promenne
            if (witoutCounter)
            {
                //Jakoby nactena hodnota
                sensorValue = ((uint)90).ToString();
                return;
            }
            #pragma warning disable
            //Vzdy aktualni instrukce
            string instruction;

            //Nacteni
            readCounterValue(out instruction);

            //Kontrola navratu - prevedeni na normalni cislo - z +000060 na 60
            sensorValue = int.Parse(checkCounterResultValue(instruction)).ToString();
            #pragma warning enable
        }

        //Vynulovani hodnoty citace
        private void servingCounterClear()
        {
            //Budeme pracovat s citacem dle promenne
            if (witoutCounter)
            {
                //Priznak, ze bylo mazano
                freeGlobals.ClearFlag = manuGlobals.ClearFlag = true;
                return;
            }

            //instrukce
            string instruction;

            //Vynulovani
            clearCounterValue(out instruction);
            sensorValue = ((int)0).ToString();

            //Kontrola navratu
            checkCounterResultValue(instruction);

            //Ulozeni stavu - pred mazanim sensoru
            //SqlTransaction trans = null;
            //saveOperationState(ref trans);

            //Priznak, ze bylo mazano
            freeGlobals.ClearFlag = manuGlobals.ClearFlag = true;
        }

        //Ulozi aktualni stav aplikace do db
        private void saveOperationState(DateTime dt, ref SqlTransaction transaction, bool operationFree, string operationBarcode, string scan1res, string scan2res, string scan3res, string zakazka, string material, string polozka, uint increment)
        {
            //Ulozeni radku
            addCurrentStatusRow(dt, ref transaction, operationFree, operationBarcode, scan1res, scan2res, scan3res, zakazka, material, polozka, increment);
        }

        //Ulozi aktualni stav aplikace do db
        /*
        private void saveOperationState(ref SqlTransaction transaction)
        {
            //V pripade tohoto ukladani musim zjistit, scany - podle operace, a globalni promenne
            string scan1res, scan2res, scan3res, zakazka, material, polozka;
            if (operation.VOLNA)
            {
                freeOper.getScans(out scan1res, out scan2res, out scan3res);
                freeOper.getGlobals(out zakazka, out material, out polozka);
            }
            else
            {
                manuOper.getScans(out scan1res, out scan2res, out scan3res);
                manuOper.getGlobals(out zakazka, out material, out polozka);
            }

            //Ulozeni radku
            addCurrentStatusRow(DateTime.Now, ref transaction, operation.VOLNA, operation.CK, scan1res, scan2res, scan3res,zakazka, material, polozka);
        }
         */

        //Finalizer - muze se vratit do initu
        private void finalizeOperation()
        {
            if (operation.VOLNA && operation.KONEC)
            {
                //showInitOperation(freeOper, new Operation(true));
                freeOper.setCurrentOperation(true);
                freeGlobals.init();
            }
            else if (!operation.VOLNA && operation.KONEC)
            {
                //showInitOperation(manuOper, new Operation(false));
                manuOper.setCurrentOperation(false);
                manuGlobals.init();
            }
        }

        //Overi odpoved citace na danou instrukci
        private string checkCounterResultValue(string instruction)
        {
            string note = string.Empty, procResult = string.Empty;
            if (!counterConnector.processAnswer(sharedResult, instruction, ref note, ref procResult))
            {
                //Vyvola vyjimku
                throw new ApplicationException("Chyba pøi komunikaci s èítaèem: result(" + procResult + "), note(" + note + ")");
            }

            //Nekdy je nutne mit vysledek
            return procResult;
        }

        //Nacte hodnotu sensoru - vraci jako cislo prevedeno na retezec
        private void readCounterValue(out string instruction)
        {
            //Inicializace instrukce
            instruction = string.Empty;

            //Testuji, jen pokud mam citac
            if (counterConnector != null)
            {
                //Odesilam pomoci funkce
                sendDataToOUT(counterConnector.Value);
                //Vracim instrukci
                instruction = counterConnector.Value;
            }
            //Neinicializovan citac
            else
            {
                throwNotInitializationCounterException();
            }
        }

        //Vynuluje hodnotu sensoru
        private void clearCounterValue(out string instruction)
        {
            //Inicializace instrukce
            instruction = string.Empty;

            //Testuji, jen pokud mam citac
            if (counterConnector != null)
            {
                //Zapamatuji reset a odesilam pomoci funkce
                sendDataToOUT(counterConnector.Reset);
                //Vracim instrukci
                instruction = counterConnector.Reset;
            }
            //Neinicializovan citac
            else
            {
                throwNotInitializationCounterException();
            }
        }

        //Overi, zda nacitany kod je v poradku, tzn muze nasledovat za soucasnym!
        private void checkLoadedBarcode(string barcode)
        {
            try
            {
                //Nacteni informaci o BUDOUCI operaci
                if (!readInformationAboutOperation(barcode))
                {
                    //Musi se podarit, jinak nemuzu prejit do nasledujicicho stavu!
                    return;
                }

                //Overeni, zda operace muze nasledovat za soucasnou
                if (!evaluateOperation())
                {
                    return;
                }

                //Zpracovani operace
                procureOperation();
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
        }

        private bool evaluateOperation()
        {
            //Pokud se jedna o volnou operaci - kontroluje se ve vetvi volnych
            if (operation.VOLNA)
            {
                //Kontrola, zda muze nasledujici volna operace nasledovat za soucasnou volnou operaci
                if (!fsm.evaluateState(freeOper.getCurrentOperation(), operation))
                {
                    string IDO = freeOper.getCurrentOperation().IDO;
                    showApplicationError("Ovìøení následující volné operace", "Volná operace " + operation.IDO + " nemùže následovat " + (IDO == string.Empty ? "po výchozím stavu aplikace" : " za volnou operací " + IDO) + ".");
                    return false;
                }
            }
            //Jinak se kontroluje ve vetvi vyrobnich
            else
            {
                //Kontrola, zda muze nasledujici vzrobni operace nasledovat ya soucasnou vzrobni operaci
                if (!fsm.evaluateState(manuOper.getCurrentOperation(), operation))
                {
                    string IDO = manuOper.getCurrentOperation().IDO;
                    showApplicationError("Ovìøení následující výrobní operace", "Výrobní operace " + operation.IDO + " nemùže následovat " + (IDO == string.Empty ? "po výchozím stavu aplikace" : " za výrobní operací " + IDO) + ".");
                    return false;
                }
            }

            //Ok
            return true;
        }

        //Scanovani cisla zakazky
        private void scanOrderNumber(byte scanZakazka)
        { 
            //Nacita se, jen pokud je cislo vetsi nez 0 - stejna kontrola je delana i ve funkci scanBarcode, ale
            //zde je to lepsi z hlediska vykonu
            if (scanZakazka > 0)
            {
                try
                {
                    //Zavru port - bude slouzit dialogu, kde se udela KOPIE
                    closeSerialPort(serialPortIN);

                    //Inicializace dialogu
                    string msg = (scanZakazka == 255 ? "Zadejte kód zakázky." : "Zadejte kód zakázky o délce $LEN$ znakù.");
                    frmScanBarcode scanCode = new frmScanBarcode(serialPortIN);
                    

                    //Scanovani a ulozeni do odpovidajici globals
                    if (operation.VOLNA)
                    {
                        freeGlobals.Zakazka = scanBarcode(scanCode, scanZakazka, msg.Replace("$LEN$", scanZakazka.ToString()));
                    }
                    else
                    {
                        manuGlobals.Zakazka = scanBarcode(scanCode, scanZakazka, msg.Replace("$LEN$", scanZakazka.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    procureException(ex);
                }
                finally
                {
                    //Otevru port - opet si jej otevru
                    openSerialPort(serialPortIN);
                }
            }
        }

        //Skenovani dodatecnych kodu
        private void scanBarcodes(byte scan1, byte scan2, byte scan3, out string scan1res, out string scan2res, out string scan3res)
        {
            //Dialog
            frmScanBarcode scanCode = null;

            //Inicializace vystupu
            scan1res = scan2res = scan3res = string.Empty;

            try
            {
                //Zavru port - bude slouzit dialogu, kde se udela KOPIE
                closeSerialPort(serialPortIN);

                //Inicializace dialogu
                string msg = "Zadejte èárový kód o délce $LEN$ znakù.";
                scanCode = new frmScanBarcode(serialPortIN);

                //Scanovani
                scan1res = scanBarcode(scanCode, scan1, (scan1 == 255 ? "Zadejte èárový kód." : msg.Replace("$LEN$", scan1.ToString())));
                scan2res = scanBarcode(scanCode, scan2, (scan2 == 255 ? "Zadejte èárový kód." : msg.Replace("$LEN$", scan2.ToString())));
                scan3res = scanBarcode(scanCode, scan3, (scan3 == 255 ? "Zadejte èárový kód." :msg.Replace("$LEN$", scan3.ToString())));
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
            finally
            {
                //Otevru port - opet si jej otevru
                openSerialPort(serialPortIN);
            }
        }

        //Uzavreni serioveho portu
        private void closeSerialPort(SerialPort port)
        {
            if (port != null && port.IsOpen) port.Close();
        }

        //otevreni serioveho portu
        private void openSerialPort(SerialPort port)
        {
            if (port != null && !port.IsOpen) port.Open();
        }

        //Naskenovani jednoho dodatecneho caroveho kodu
        private string scanBarcode(frmScanBarcode scanCode, byte flag, string msg)
        {
            if (flag > 0)
            {
                scanCode.LabelMessage = msg;
                scanCode.BarcodeLength = flag;
                scanCode.ShowDialog();
                return scanCode.Barcode;
            }

            return string.Empty;
        }

        //Prechod do vychoziho stavu
        private void showInitOperation(OperationUC opUc, Operation op)
        {
            //Skryji datagridy - v init jsou prazdne
            opUc.hideDatagridsPanels();
            //Pokracuji s nastavenim init operace
            setOperation(opUc, op, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        //Zobrazi operaci tam kam ma
        private void showOperation(string scan1res, string scan2res, string scan3res)
        {
            if (operation.VOLNA) setOperation(freeOper, operation, scan1res, scan2res, scan3res, freeGlobals.Zakazka, freeGlobals.Material, freeGlobals.Polozka);
            else setOperation(manuOper, operation, scan1res, scan2res, scan3res, manuGlobals.Zakazka, manuGlobals.Material, manuGlobals.Polozka);
        }

        //Obecne nastaveni operace do komponenty
        private void setOperation(OperationUC opUc, Operation op, string scan1res, string scan2res, string scan3res, string zakazka, string material, string polozka)
        {
            //Transakce
            SqlTransaction trans = null;

            //Zapamatuji minulou operaci i se scany
            Operation past = new Operation(opUc.getCurrentOperation());
            string pastScan1, pastScan2, pastScan3;
            opUc.getScans(out pastScan1, out pastScan2, out pastScan3);
            string pastZakazka, pastMaterial, pastItemAbbr;
            opUc.getGlobals(out pastZakazka,out pastMaterial,out pastItemAbbr);

            try
            {
                //Nastavim = zobrazim - tim je pro aplikaci kompletni nacteni operace
                opUc.setCurrentOperation(op, scan1res, scan2res, scan3res, zakazka, material, polozka);

                //Vypocet prirustku citace
                uint increment = countIncrement(op, uint.Parse(sensorValue));

                //Vypocet hodnoty, ktera se ma zobrazit
                uint value = countCounterValue(uint.Parse(sensorValue), op, increment, uint.Parse(sensorValue));

                //Ulozeni udalosti - presel do noveho stavu
                DateTime dt = DateTime.Now;
                addEventsRow(dt, ref trans, op.NAZEV, op.CK, op.VOLNA, op.IDO, scan1res, scan2res, scan3res, material, zakazka, increment, value);

                //Ulozeni stavu - presel do noveho stavu 
                saveOperationState(dt, ref trans, op.VOLNA, op.CK, scan1res, scan2res, scan3res, zakazka, material, polozka, value);

                //Vse se povedlo - potvrdim
                if (trans != null) trans.Commit();

                //Vlozeni do historie
                history.addRecord(dt.ToString(), op.IDO, op.NAZEV, zakazka, material, scan1res, scan2res, scan3res, value.ToString(), int.Parse(numNumberObHistooryRecords.Value.ToString()));

                //Zobrazenni beznych metru
                showBM(opUc, value);

            }
            catch (Exception ex)
            {
                //Navraceni
                try { if (trans != null) trans.Rollback(); }
                catch (Exception exx)
                {
                    //Log.Write("Nepodarilo se provest operaci rollback " + exx.Message);
                    ExceptionHandler2.Handle(exx);
                    ExceptionHandler2.Handle("Ukonceni hlavni aplikace", "Log_Rezacka", "txt");
                }
                opUc.setCurrentOperation(past, pastScan1, pastScan2, pastScan3,pastZakazka,pastMaterial,pastItemAbbr);
                throw ex;
            }
        }

        //Zobrazeni beznych metru
        private void showBM(OperationUC opUc, uint value)
        {
            int num = int.Parse(numBMDecimalPlaces.Value.ToString());
            opUc.txtNumberBM.Text = (value * LogConfig.Koeficient).ToString("N" + num);
        }

        //Vypocita  celkovy pocet pulsu
        private uint countCounterValue(uint p, Operation op, uint increment, uint count)
        {
            //Ulozeni
            if (op.VOLNA)
            {
                //Pokud bylo nulovanu, prictu jen pocet, pokud nulovano nebylo, prictu pocet - minuly pocet
                freeGlobals.Counter += increment;

                //Ulozim aktualni hodnotu jako minulou
                freeGlobals.PastCounter = count;

                //Vysledek
                return freeGlobals.Counter;
            }
            else
            {
                //Pokud bylo nulovanu, prictu jen pocet, pokud nulovano nebylo, prictu pocet - minuly pocet
                manuGlobals.Counter += increment;

                //Ulozim aktualni hodnotu jako minulou
                manuGlobals.PastCounter = count;

                //Vysledek
                return manuGlobals.Counter;
            }
        }

        //Vypocita inkrement citace
        private uint countIncrement(Operation op, uint count)
        {
            //Kontrola,zda se skenovala zakazka
            if (op.SCANZAKAZKA > 0)
            {
                //Jestli ano, vynuluji citace, jedu novou zakazku
                if (op.VOLNA)
                {
                    freeGlobals.Counter = freeGlobals.PastCounter = 0;
                }
                else
                {
                    manuGlobals.Counter = manuGlobals.PastCounter = 0;
                }
            }

            //Vypocet prirustku pro dany vyrovni proces
            uint value = 0;
            if (op.VOLNA)
            {
                value = (freeGlobals.ClearFlag ? count : (count - freeGlobals.PastCounter));
                freeGlobals.ClearFlag = false;
            }
            else
            {
                value = (manuGlobals.ClearFlag ? count : (count - manuGlobals.PastCounter));
                manuGlobals.ClearFlag = false;
            }

            return value;
        }

        //Nacitani parametru operace z databaze a zobrazeni
        private bool readInformationAboutOperation(string operationBarcode)
        {
            //Data reader
            SqlDataReader reader = null;

            try
            {
                //Musi byt vyplnen nejaky kod operace
                if (operationBarcode != string.Empty)
                {
                    //Nastavim a otevru spojeni
                    sqlConnection.ConnectionString = LogConfig.SqlConnectionStringLocal;
                    sqlConnection.Open();
                    //Inicializace prikazu
                    SqlCommand giao = new SqlCommand(Queries.getInformationsAboutOperations, sqlConnection);
                    giao.Parameters.Clear();
                    giao.Parameters.Add(new SqlParameter("@ck", operationBarcode));
                    giao.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                    //Vykonani
                    reader = giao.ExecuteReader(CommandBehavior.CloseConnection);
                    //Zpracovani
                    return procReadInformationAboutOperation(reader, operationBarcode);
                }
                else
                {
                    showApplicationError("Naètení parametrù operace", "Neplatný èárový kód operace");
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Muze byt uz otevren reader
                closeReader(reader);
                //uzavreni spojeni, pokud je otevrene a zpracovani vyjimky
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
                procureException(ex);
                //Ko
                return false;
            }
        }

        //Zpracovani nactenych informaci k operaci
        private bool procReadInformationAboutOperation(SqlDataReader reader, string operationBarcode)
        {
            try
            {
                //Nacteni dat do objektu reprezentuji tabulku
                DataTable data = new DataTable();
                data.Load(reader);

                //Kontrola, zda je operace v db prave 1x
                if (data.Rows.Count != 1)
                {
                    showApplicationError("Naètení parametrù operace", "Èárový kód '" + operationBarcode + "' se v databázi nevyskytuje  právì jednou, ale " + data.Rows.Count.ToString() + "-krát.");
                    return false;
                }

                //Vlozeni do tridy reprezentujici operaci
                operation.CK = operationBarcode;
                operation.IDO = (string)data.Rows[0]["IDO"];
                operation.NAZEV = (string)data.Rows[0]["NAZEV"];
                operation.START = (bool)ColValue(data.Columns["START"].DataType, data.Rows[0]["START"]);
                operation.KONEC = (bool)ColValue(data.Columns["KONEC"].DataType, data.Rows[0]["KONEC"]);
                operation.VOLNA = (bool)ColValue(data.Columns["VOLNA"].DataType, data.Rows[0]["VOLNA"]);
                operation.SCAN1 = (byte)ColValue(data.Columns["SCAN1"].DataType, data.Rows[0]["SCAN1"]);
                operation.SCAN2 = (byte)ColValue(data.Columns["SCAN2"].DataType, data.Rows[0]["SCAN2"]);
                operation.SCAN3 = (byte)ColValue(data.Columns["SCAN3"].DataType, data.Rows[0]["SCAN3"]);
                operation.SENSOR = (byte)ColValue(data.Columns["SENSOR"].DataType, data.Rows[0]["SENSOR"]);
                operation.SPHLAVICKA = (string)ColValue(data.Columns["SPHLAVICKA"].DataType, data.Rows[0]["SPHLAVICKA"]);
                operation.SPINFO = (string)ColValue(data.Columns["SPINFO"].DataType, data.Rows[0]["SPINFO"]);
                operation.SCANZAKAZKA = (byte)ColValue(data.Columns["SCANZAKAZKA"].DataType, data.Rows[0]["SCANZAKAZKA"]);
                operation.SPZAKAZKA = (string)ColValue(data.Columns["SPZAKAZKA"].DataType, data.Rows[0]["SPZAKAZKA"]);
                operation.KONTROLAMAT = (bool)ColValue(data.Columns["KONTROLAMAT"].DataType, data.Rows[0]["KONTROLAMAT"]);
                operation.SPMATERIAL = (string)ColValue(data.Columns["SPMATERIAL"].DataType, data.Rows[0]["SPMATERIAL"]);
                operation.LOGIN = (bool)ColValue(data.Columns["LOGIN"].DataType, data.Rows[0]["LOGIN"]);

            }
            catch (Exception ex)
            {
                procureException(ex);
                return false;
            }
            finally
            {
                //Uzavreme reader, cimz se uzavre i pripojeni
                closeReader(reader);
            }

            //OK
            return true;
        }

        //Uzavre data reader - pokud je nenulovy
        private void closeReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed) reader.Close();
        }

        //Synchronni donacteni nasledujicichc moznych operaci
        private bool readNextOperations(string operationCode)
        {
            //DataReader
            SqlDataReader reader = null;

            try
            {
                //Nastavim a otevru spojeni
                sqlConnection.ConnectionString = LogConfig.SqlConnectionStringLocal;
                sqlConnection.Open();
                //Inicializace prikazu
                SqlCommand giao = new SqlCommand(Queries.getNextOperations, sqlConnection);
                giao.Parameters.Clear();
                giao.Parameters.Add(new SqlParameter("@ido", operationCode));
                giao.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                //Vykonani
                reader = giao.ExecuteReader(CommandBehavior.CloseConnection);
                //Zpracovani
                return procReadNextOperations(reader);
            }
            catch (Exception ex)
            {
                //Pokusim se pro jistotu i uzavrit reader
                closeReader(reader);
                //Uzavreni spojeni, pokud je otevrene (napr reder se epocedlo inicializovat,ale spojeni je open) a zpracovani vyjimky
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
                procureException(ex);
                //Ko
                return false;
            }
        }

        //Zpracovani synchronne nactenych nasledujicichc operaci
        private bool procReadNextOperations(SqlDataReader reader)
        {
            try
            {
                //Nacteni dat do objektu reprezentuji tabulku
                DataTable data = new DataTable();
                data.Load(reader);

                //Pruchod pres radky
                string nextOps = string.Empty;
                foreach (DataRow dr in data.Rows)
                {
                    nextOps += ((string)dr["IDO_NEXT"] + ", ");
                }

                //Odstranneni koncoveho nezadouciho ", "
                if (nextOps != string.Empty) nextOps = nextOps.Substring(0, nextOps.Length - 2);

                //Vlozeni do tridy reprezentujici operaci
                operation.NEXTOPERATIONS = nextOps;
            }
            catch (Exception ex)
            {
                procureException(ex);
                return false;
            }
            finally
            {
                //Uzavreni readeru -> uzavreni spojeni
                closeReader(reader);
            }

            //Ok
            return true;
        }

        //Kontroluje, zda neni hodnota ve sloupci null, pokud ano, vraci urcitou hodnotu urciteho typu (Byte->0, Boolean->false), jinak vraci hodnotu
        private object ColValue(Type column, object value)
        {
            switch (column.ToString())
            {
                case "System.Byte": if (value is DBNull) return (byte)0; break;
                case "System.Boolean": if (value is DBNull) return false; break;
                case "System.String": if (value is DBNull) return string.Empty; break;
                default: throw new ApplicationException("Neoèekávaný typ sloupce v tabulce FASK_Operations");
            }

            //Navracim hodnotu
            return value;
        }

        //Delegate pro volani metody formu pro zpracovani vyjimky z jinych vlaken
        private delegate void procureExceptionDelegate(Exception ex, bool sendEm);

        //Obstarani jedne vznikle vyjimky - parametry jsou vyjimka a priznak, zda se ma odesilat email
        private void procureException(Exception ex)
        {
            try
            {
                //Zobrazeni
                showExceptionError(ex);
                //Zalogovani
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                //Email
                sendEmail(ex);
            }
            //Pri chybe jen zobrazime a logujeme - oravdepodobne vzinkla chyba pri emailu
            catch (Exception exx)
            {
                //Zobrazeni
                showExceptionError(exx);
                //Logovani
               // Log.WriteException(exx);
                ExceptionHandler2.Handle(ex);
            }
        }

        //Zobrazeni chyby vzniknuvsi nejakou chybou v logice aplikace
        private void showApplicationError(string errorTitle, string errorMessage)
        {
            notifyIconState.ShowBalloonTip(5000, errorTitle, errorMessage, ToolTipIcon.Error);
           // Log.Write(errorMessage);
            ExceptionHandler2.Handle(errorMessage, "Log_Rezacka", "txt");
        }

        //Zobrazeni chyby vzniknuvsi vyjimkou
        private void showExceptionError(Exception ex)
        {
            notifyIconState.ShowBalloonTip(5000, ex.Source, ex.Message, ToolTipIcon.Error);
        }

        //Metoda pro odeslani emailu.
        private void sendEmail(Exception ex)
        {
            //Nacteni konfigurace z konfiguracniho souboru
            Email.LoadConfiguration();
            //Vlozeni Tela zpravy
            Email.Body = ex.Source + " : " + ex.Message;
            //Vytvoreni zpravy
            Email.CreateEmailMessage();
            //Odeslani
            Email.SendEmailMessageAsynch();
        }

        //Ulozeni informaci o nastaveni vstupniho SP - scanner
        private void buttonSaveSPIN_Click(object sender, EventArgs e)
        {
            RezackaConfig.config.Rezacka[0].SP_IN_BaudRate = txt_spinBaudRate.Text;
            RezackaConfig.config.Rezacka[0].SP_IN_DataBits = txt_spinDataBits.Text;
            RezackaConfig.config.Rezacka[0].SP_IN_Parity = txt_spinParity.Text;
            RezackaConfig.config.Rezacka[0].SP_IN_PortName = txt_spinPort.Text;
            RezackaConfig.config.Rezacka[0].SP_IN_StopBits = txt_spinStopBits.Text;

            RezackaConfig.Save();

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        //Ulozeni informaci o nastaveni vystupniho SP - counter
        private void buttonSaveSPOUT_Click(object sender, EventArgs e)
        {
            RezackaConfig.config.Rezacka[0].SP_OUT_BaudRate = txt_spoutBaudRate.Text;
            RezackaConfig.config.Rezacka[0].SP_OUT_DataBits = txt_spoutDataBits.Text;
            RezackaConfig.config.Rezacka[0].SP_OUT_Parity = txt_spoutParity.Text;
            RezackaConfig.config.Rezacka[0].SP_OUT_PortName = txt_spoutPort.Text;
            RezackaConfig.config.Rezacka[0].SP_OUT_StopBits = txt_spoutStopBits.Text;

            //Ulozeni do xml
            RezackaConfig.Save();

            //Log-event
            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        //Ukonceni formu
        private void frmRezacka_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Uzavreni vstupu pokud je otevren
            if (serialPortIN.IsOpen) serialPortIN.Close();

            //Uzavrei vystupu pokud je otevren
            if (serialPortOUT.IsOpen) serialPortOUT.Close();

            //Ulozeni sirky radku v historii, pokud je povolena
            history.saveColumnsWidth();
        }

        //Minuly stav portu
        bool serialINOpenedLastState = true;
        bool serialOUTOpenedLastState = true;

        //Uzavre porty
        public void ClosePorts()
        {
            //IN
            if (serialPortIN.IsOpen)
            {
                serialPortIN.Close();
                serialINOpenedLastState = true;
            }
            else
            {
                serialINOpenedLastState = false;
            }

            //OUT
            if (serialPortOUT.IsOpen)
            {
                serialPortOUT.Close();
                serialOUTOpenedLastState = true;
            }
            else
            {
                serialOUTOpenedLastState = false;
            }
        }

        //Vrati porty do stavu pred uzavrenim
        public void ReturnPortsToPreviousState()
        {
            //IN
            if (serialINOpenedLastState && !serialPortIN.IsOpen) serialPortIN.Open();

            //OUT
            if (serialOUTOpenedLastState && !serialPortOUT.IsOpen) serialPortOUT.Open();
        }

        //Posle data na dany seriovy port
        private void sendDataToOUT(string data)
        {
            try
            {
                //Zapis
                serialPortOUT.Write(data);
                //Log-event
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.InstructionSended, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
                //Po zapsani dat na vystup uzamknu semafor na dobu maximalne nutnou na precteni z portu + zapis (rezie apod)
                s.WaitOne(serialPortOUT.ReadTimeout + serialPortOUT.WriteTimeout);
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
        }

        //Zobrazi informaci o tom, ze citac neni inicializovan
        private void throwNotInitializationCounterException()
        {
            throw new ApplicationException("Chyba pøi inicializaci èítaèe - èítaè není inicializován");
        }

        //Nacteni citace dle vyplnene specifikace v nastaveni
        private void btnLoadCounter_Click_1(object sender, EventArgs e)
        {
            //Ulozim hodnoty
            btnSaveCounter_Click_1(sender, e);
            //Nactu jako pri konstrukci formu
            InitializeCounter();
        }

        //Ulozeni informaci o citaci
        private void btnSaveCounter_Click_1(object sender, EventArgs e)
        {
            string ass = txtAssembly.Text.Trim();
            string obj = txtObject.Text.Trim();

            //Vse musi byt vyplneno
            if (ass == string.Empty || obj == string.Empty)
            {
                MessageBox.Show("Nastavení èítaèe se nezdaøilo. Je tøeba vyplnit všechny položky.", "Øezaèka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Ok ukladam nove hodnoty
            else
            {
                //Log - event
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                //Ulozeni do tabulky
                RezackaConfig.config.Counter.Rows.Clear();
                RezackaConfig.config.Counter.Rows.Add(new object[] { ass, obj });

                //A do xml
                RezackaConfig.Save();
            }
        }

        //Vlozi zaznam do databaze o zmene stavu
        private void addEventsRow(DateTime dt, ref SqlTransaction transaction, string operationName, string operationBarcode, bool operationFree, string operationCode, string scan1result, string scan2result, string scan3result, string material, string zakazka, uint increment, uint sensortotal)
        {
            Database.Classes.Vyroba_Local.EventsInsert(
                dt, 
                ref transaction, 
                LogConfig.SqlConnectionStringLocal, 
                LogConfig.LoginID, 
                LogConfig.MachineID, 
                operationName, 
                operationBarcode, 
                operationFree, 
                operationCode, 
                scan1result, 
                scan2result, 
                scan3result, 
                increment.ToString(), 
                zakazka, 
                material, 
                sensortotal.ToString(),
                string.Empty,
                null,
                string.Empty,
                string.Empty,
                string.Empty,
                null,
                0,
                string.Empty,
                null,
                1,
                null,
                null,
                null,
                null,
                null
                );
        }

        //Vlozi zaznam do stavove tabulky - pri zmene stavu a kdykoli je to treba
        private void addCurrentStatusRow(DateTime dt, ref SqlTransaction transaction, bool operationFree, string operationBarcode, string scan1res, string scan2res, string scan3res, string zakazka, string material, string polozka, uint increment)
        {
            Database.Classes.Vyroba_Local.CurrentStateInsert(dt, ref transaction, LogConfig.SqlConnectionStringLocal, LogConfig.MachineType, operationFree, operationBarcode, scan1res, scan2res, scan3res, increment.ToString(), zakazka, material, polozka);
        }

        //Zmena nastaveni historie
        private void chbHistory_CheckedChanged(object sender, EventArgs e)
        {
            //Povoleni nebo zakazani historie
            history.Allow = chbHistory.Checked;
        }

        //Ulozeni ostatniho nastaveni
        private void btnSaveOther_Click(object sender, EventArgs e)
        {
            //Nastaveni hodnot - zatim nejsou zadne nastavene
            if (RezackaConfig.config.Other.Rows.Count == 0)
            {
                RezackaConfig.config.Other.Rows.Add(new object[] { chbHistory.Checked, numNumberObHistooryRecords.Value, numHistoryHeight.Value, numBMDecimalPlaces.Value });
            }
            //Zmena
            else
            {
                RezackaConfig.config.Other[0].AllowHistory = chbHistory.Checked;
                RezackaConfig.config.Other[0].NumberOfHistoryRecords = numNumberObHistooryRecords.Value;
                RezackaConfig.config.Other[0].HeightHistory = numHistoryHeight.Value;
                RezackaConfig.config.Other[0].BMDecimalPlaces = numBMDecimalPlaces.Value;
            }

            //Odstraneni starych hodnot
            RezackaConfig.config.LoginOperations.Rows.Clear();
            //Ulozeni hodnot z textboxu pro prihlasovaci operace
            string[] array = txtLoginOperations.Text.Trim().Split(new char[] { ',' });
            foreach (string ck in array)
            {
                if (ck.Trim() != string.Empty)
                {
                    RezackaConfig.config.LoginOperations.Rows.Add(new object[] { ck.Trim() });
                }
            }

            //Zobrazeni - promitne se az v pripade ukladani
            history.Visible = chbHistory.Checked;

            //Nacteni - v pripade, ze se menila hodnota
            history.readHistoricalOperation(int.Parse(numNumberObHistooryRecords.Value.ToString()));

            //Zmena sirky
            history.Height = int.Parse(numHistoryHeight.Value.ToString());

            //Nastaveni sirek
            history.loadColumnsWidth();

            //Ulozeni do xml
            RezackaConfig.Save();

            //Log-event
            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        //Status label modulu
        //INFO: pozor na jeho pouzzivani - predavan a po konstrukci modulu, pred jeho zobrazenim!
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel 
        {
            set { statusLabel = value; }
        }

        #region IModuleConnector Members


        public bool IsReadyToClose(out string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        public bool IsReadyToShow(out string message)
        {
            //throw new NotImplementedException();
            message = "ok";
            return true;
        }
    }
}