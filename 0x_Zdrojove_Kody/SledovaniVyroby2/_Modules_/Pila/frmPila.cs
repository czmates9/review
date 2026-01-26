using System;
using System.Drawing;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.ModuleIfc;
using FASK.SledovaniVyroby.LabelPrint;
using Fask.Emailing;
using System.Data;
using System.IO;

namespace FASK.SledovaniVyroby.Module.Pila
{
    public partial class frmPila : Form, IModuleConnector
    {

        //Zakazani nacteni caroveho kodu ze scanneru, pokud je zobrazeno okno pro potvrzeni
        private bool datareadEnable = true;

        //Promenne pro praci s databazi - inicializace v konstruktoru
        private Database.Vyroba vyroba = null;
        Database.VyrobaTableAdapters.FASK_EventsTableAdapter eta = null;

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Konstruktor
        public frmPila()
        {
            //Komponenty
            InitializeComponent();

            //Porty
            try { InitializePorts(); }
            catch { }

            //Inicializace tiskoveho serveru
            InitPrintServer();
            //Instance datasetu
            vyroba = new Database.Vyroba();
            eta = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
        }

        //Start-stop vstupni seriovy port
        private void buttonStartStopIN_Click_1(object sender, EventArgs e)
        {
            SerialPortINChangeState();
        }

        //Zmena stavu vstupniho serioveho portu
        private void SerialPortINChangeState()
        {
            try
            {
                //Pokud jsou otevrene - uzavru
                if (serialPortIN.IsOpen)
                {
                    serialPortIN.Close();
                }
                //Jinak nastavim a otevru
                else
                {
                    serialPortIN.BaudRate = int.Parse(PilaConfig.config.Pila[0].SP_IN_BaudRate);
                    serialPortIN.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), PilaConfig.config.Pila[0].SP_IN_Parity);
                    serialPortIN.PortName = PilaConfig.config.Pila[0].SP_IN_PortName;
                    serialPortIN.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), PilaConfig.config.Pila[0].SP_IN_StopBits);
                    serialPortIN.DataBits = int.Parse(PilaConfig.config.Pila[0].SP_IN_DataBits);

                    serialPortIN.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }

            //Nastaveni barvy
            if (serialPortIN.IsOpen) buttonStartStopIN.BackColor = Color.Green;
            else buttonStartStopIN.BackColor = Color.Red;
        }

        //Start-stop vystupni seriovy port
        private void buttonStartStopOUT_Click_1(object sender, EventArgs e)
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
                    serialPortOUT.BaudRate = int.Parse(PilaConfig.config.Pila[0].SP_OUT_BaudRate);
                    serialPortOUT.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), PilaConfig.config.Pila[0].SP_OUT_Parity);
                    serialPortOUT.PortName = PilaConfig.config.Pila[0].SP_OUT_PortName;
                    serialPortOUT.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), PilaConfig.config.Pila[0].SP_OUT_StopBits);
                    serialPortOUT.DataBits = int.Parse(PilaConfig.config.Pila[0].SP_OUT_DataBits);

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

        //Inicializace poru
        private void InitializePorts()
        {
            //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
            if (PilaConfig.config.Pila.Rows.Count == 0)
            {
                PilaConfig.config.Pila.AddPilaRow(
                serialPortIN.PortName, serialPortOUT.PortName,
                serialPortOUT.DataBits.ToString(), serialPortIN.DataBits.ToString(),
                serialPortOUT.Parity.ToString(), serialPortIN.Parity.ToString(),
                serialPortOUT.StopBits.ToString(), serialPortIN.StopBits.ToString(),
                serialPortOUT.BaudRate.ToString(), serialPortIN.BaudRate.ToString()
                );
                //Ulozeni
                PilaConfig.Save();
            }

            try
            {
                //Vlozeni do textboxuu
                txt_spinBaudRate.Text = PilaConfig.config.Pila[0].SP_IN_BaudRate; //serialPortIN.BaudRate.ToString();
                txt_spinParity.Text = PilaConfig.config.Pila[0].SP_IN_Parity; //serialPortIN.Parity.ToString();
                txt_spinPort.Text = PilaConfig.config.Pila[0].SP_IN_PortName; //serialPortIN.PortName;
                txt_spinStopBits.Text = PilaConfig.config.Pila[0].SP_IN_StopBits; //serialPortIN.StopBits.ToString();
                txt_spinDataBits.Text = PilaConfig.config.Pila[0].SP_IN_DataBits; //serialPortIN.DataBits.ToString();
            }
            catch { }

            try
            {
                //Vlozeni do textboxuu
                txt_spoutBaudRate.Text = PilaConfig.config.Pila[0].SP_OUT_BaudRate; //serialPortOUT.BaudRate.ToString();
                txt_spoutParity.Text = PilaConfig.config.Pila[0].SP_OUT_Parity; //serialPortOUT.Parity.ToString();
                txt_spoutPort.Text = PilaConfig.config.Pila[0].SP_OUT_PortName; //serialPortOUT.PortName;
                txt_spoutStopBits.Text = PilaConfig.config.Pila[0].SP_OUT_StopBits; //serialPortOUT.StopBits.ToString();
                txt_spoutDataBits.Text = PilaConfig.config.Pila[0].SP_OUT_DataBits; //serialPortOUT.DataBits.ToString();
            }
            catch { }

            //Zmena stavu obou portu
            SerialPortINChangeState();
            SerialPortOUTChangeState();
        }

        //Delegat
        delegate void DataReceivedDelegate(string data);

        //Prijem dat
        private void DataReceived(string data)
        {
            //Pokud neni zobrazeno okno pro potvrzeni, tak muzu nacist dalsi kod
            if (datareadEnable) 
            {
                //Kopie CK
                txtBarcode.Text = data;
                //A vyprazdneni textboxu
                txtItemNumber.Clear(); txtOrderNumber.Clear(); txtProfileLength.Clear();
                //Log-event
                Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeRead, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
                //Parsovani - pokud dobre dopadne - odesilani
                if (DataParse(data))
                {
                    //Odeslani
                    SendData();
                }
                else
                {
                    //Zobrazeni chyby parsovani
                    showDataParseError(); 
                }
            }
            //Jinak co? - zatim nedela nic!
            else 
            {
                notifyIconState.ShowBalloonTip(5000, "Chyba pøi zpracování èárového kódu", "Aplikace je zaneprázdnìna", ToolTipIcon.Error);
                Log.Write("Aplikace je zaneprázdnìna", "Chyba pøi zpracování èárového kódu");
            }
        }

        //Parsovani dat z CK
        private bool DataParse(string data)
        {
            //Format CK: rozmer, cislo zakazky, cislo polozky
            //Parsovani jednotlivych casti CK
            //
            //Pokud neobsahuje carku - spatny ck
            if (!data.Contains(",")) return false;

            //Rozdeleni podle carky
            string[] barcodeItems = data.Split(new char[] { ',' });

            //Pokud neobsahuje 3 casti, spatny CK
            if (barcodeItems.Length != 3) return false;

            //Kontrola jednotlivych polozek
            if (!checkBarcodeItems(barcodeItems[0], barcodeItems[1], barcodeItems[2])) return false;

            //Pokud se kod dostal az sem, je vse vporadku - zobrazim
            txtProfileLength.Text = barcodeItems[0];
            txtOrderNumber.Text = barcodeItems[1];
            txtItemNumber.Text = barcodeItems[2];

            //Log-event
            Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeParse, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            //Ok
            return true;
        }

        //Prevod nactenych dat na stringy a kontrola
        private string CreateOutputBarCode()
        {
            //Casti vystupniho retez
            const string constant = "1";
            const string separator = ",";
            const string rozmerpostfix = "0";

            //Vsechny casti musi byt v poradku
            string size = txtProfileLength.Text.Trim();
            if (!checkBarcodeItems(size, txtOrderNumber.Text.Trim(), txtItemNumber.Text.Trim()))
            {
                return string.Empty;
            }

            //Retezec pro vysledek - 1,rozmer,rozmer
            string result = constant + separator + 
                size + rozmerpostfix + separator + 
                size + rozmerpostfix;

            //log-event
            Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeBuild, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            return result.Trim();
        }

        //Prijeti dat
        private void serialPortIN_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string data = string.Empty;
            //try
            //{
            //    data = serialPortIN.ReadLine();
            //}
            //catch 
            //{
            //    data = serialPortIN.ReadExisting();
            //}

            while (serialPortIN.BytesToRead > 0)
            {
                data += serialPortIN.ReadExisting();
            }

            data = data.Trim();

            this.BeginInvoke(new DataReceivedDelegate(DataReceived), new object[] { data });

        }

        //Nacteni parametru CK
        private void buttonReadParams_Click(object sender, EventArgs e)
        {
            if (!DataParse(txtBarcode.Text)) showDataParseError(); 
        }

        //Chyba pri parsovani vstupniho CK
        private void showDataParseError()
        {
            notifyIconState.ShowBalloonTip(5000, "Chyba pøi zpracování èárového kódu", "Chybný formát", ToolTipIcon.Error);
            Log.Write("Chybný formát:" + txtBarcode.Text.Trim(), "Chyba pøi zpracování èárového kódu");
        }

        //Chyba pri pokusu odeslat nacteny CK
        private void showDataSentError()
        {
            notifyIconState.ShowBalloonTip(5000, "Chyba pøi zpracování èárového kódu", "Jednotlivé položky nejsou správnì vyplnìny", ToolTipIcon.Error);
            Log.Write("Jednotlivé položky nejsou správnì vyplnìny:" + txtProfileLength.Text.Trim() + ',' + txtOrderNumber.Text.Trim() + ',' + txtItemNumber.Text.Trim(), "Chyba pøi zpracování èárového kódu");
        }

        //Zobrazeni chyby vzniknuvsi vyjimkou
        private void showExceptionError(Exception ex)
        {
            notifyIconState.ShowBalloonTip(5000, ex.Source, ex.Message, ToolTipIcon.Error);
        }

        //Odeslani CK
        private void buttonSendParams_Click(object sender, EventArgs e)
        {
            SendData();
        }

        //Odeslani dat
        private void SendData()
        {
            try
            {
                //Neni mozne nacitat CK
                datareadEnable = false;

                //Vstup
                string readeddata = txtBarcode.Text.Trim();
                //Vystup
                string datatosend = CreateOutputBarCode();
                
                //Kontrola dat k odeslani - pokud je prazdny string, koncim
                if (datatosend == string.Empty)
                {
                    showDataSentError();
                    return;
                }

                //Odeslani na OUT
                //serialPortOUT.WriteLine(datatosend + "\r");
                serialPortOUT.Write(datatosend);
                //Log
                Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeSendToPort, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                /*
                //Dialog pro potvrzeni
                frmPotvrzeniKusu pks = new frmPotvrzeniKusu(this);

                //Vychozim poctem kusu je jeden kus
                int puvodniKusy = pks.PocetKusu = 1;
                pks.txtProfileLength.Text = txtProfileLength.Text;
                pks.txtOrderNumber.Text = txtOrderNumber.Text;
                pks.txtItemNumber.Text = txtItemNumber.Text;

                pks.TopLevel = true;
                pks.TopMost = true;

                //Zobrazeni dialogu - stisk CANCEL
                if (pks.ShowDialog() == DialogResult.Cancel)
                {
                    Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfirmQTYCanceled, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
                    return;
                }
                //Zobraeni dialogu - stisk OK
                else
                {
                    Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfirmQTYOK, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
                }
                 */

                addEventsRow(LogConfig.LoginID,
                    LogConfig.MachineID,
                    DateTime.Now,
                    (decimal)1,
                    //(decimal)puvodniKusy,
                    (decimal)1,
                    //(decimal)pks.PocetKusu,
                    string.Empty,
                    //pks.Poznamka,
                    readeddata,
                    datatosend,
                    txtOrderNumber.Text,
                    string.Empty,
                    Guid.NewGuid(),
                    "O");
                
                //Tiskneme
                printData((decimal)1, (decimal)1, readeddata, datatosend, vyroba);
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
            finally
            {
                datareadEnable = true;
            }
        }

        //Obstarani jedne vznikle vyjimky
        private void procureException(Exception ex)
        {
            try
            {
                //Zobrazeni
                showExceptionError(ex);
                //Zalogovani
                Log.WriteException(ex);
                //Email
                sendEmail(ex);
            }
            //Pri chybe jen zobrazime a logujeme - oravdepodobne vzinkla chyba pri emailu
            catch(Exception exx)
            {
                //Zobrazeni
                showExceptionError(exx);
                //Logovani
                Log.WriteException(exx);
            }
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
            Email.SendEmailMessage();
        }

        //Tisk etikety
        private void printData(decimal puvodniKusy, decimal pocetKusu, string nactenaData, string odeslanaData, Database.Vyroba vyroba)
        {
            //Potvrzeni kusu probehlo uspesne - dialog pro tisk
            //frmPotvrzeniTisku tiskDialog = new frmPotvrzeniTisku(int.Parse(pocetKusu.ToString()).ToString(),txtOrderNumber.Text,txtItemNumber.Text,txtProfileLength.Text);

            //Tisk
            while (true)
            {
                //Log
                Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeSendToPrint, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                try
                {
                    //Vytvoreni 'slovniku' pro tisk
                    DateTime printTime = DateTime.Now;
                    Guid printGuid = Guid.NewGuid();

                    //Vytvoreni radku do tabulky FASK_Events
                    Database.Vyroba.FASK_EventsRow eventsrow = vyroba.FASK_Events.NewFASK_EventsRow();
                    eventsrow.loginid = LogConfig.LoginID;
                    eventsrow.machineid = LogConfig.MachineID;
                    eventsrow.dateeve = printTime;
                    eventsrow.qty = pocetKusu;
                    //eventsrow.qtyReal = decimal.Parse(tiskDialog.PocetKusu);
                    eventsrow.qtyReal = (decimal)1;
                    //eventsrow.description = tiskDialog.Poznamka;
                    eventsrow.description = string.Empty;
                    eventsrow.barcodeReaded = nactenaData;
                    //Uprava radku pro tisk
                    eventsrow.barcodeSended = editBarcodeSended(odeslanaData);
                    eventsrow.zakazka = txtOrderNumber.Text;
                    eventsrow.popis = eventsrow.barcodeSended;
                    eventsrow.faskGUID = printGuid;
                    eventsrow.reportType = "P";

                    //Inicializace
                    bpac.DocumentClass document = new bpac.DocumentClass();
                    if (!document.Open(PilaConfig.config.Print_Server[0].Template))
                    {
                        throw new ApplicationException("Nepodaøilo se otevøít šablonu - " + (PilaConfig.config.Print_Server[0].Template == string.Empty ? "prázdný soubor." : "soubor - " + PilaConfig.config.Print_Server[0].Template + '.'));
                    }


                    //Dynamicke vlozeni z radku
                    foreach (DataColumn c in vyroba.FASK_Events.Columns)
                    {
                        try { document.GetObject(c.ColumnName).Text = (eventsrow[c.ColumnName] is DBNull ? string.Empty : eventsrow[c.ColumnName].ToString()); }
                        catch { /*Pro objekty ktere nejsou v sablone vyvola vyjimku*/ }
                    }

                    //Start tisku
                    if (!document.StartPrint("Fask - pila label printing", bpac.PrintOptionConstants.bpoDefault))
                    {
                        throw new ApplicationException("Nepodaøilo se odstartovat tisk.");
                    }

                    //Asi tisk
                    if (!document.PrintOut(int.Parse(pocetKusu.ToString()), bpac.PrintOptionConstants.bpoDefault))
                    {
                        throw new ApplicationException("Chyba v prùbìhu tisku.");
                    }

                    //Ukonceni tisku
                    if (!document.EndPrint())
                    {
                        throw new ApplicationException("Nepodaøilo se ukonèit tisk.");
                    }

                    //Uzavreni sablony
                    if (!document.Close())
                    { 
                        //Nepodarilo se uzavrit sablonu - co uz
                    }

                    //addEventsRow(
                    //        LogConfig.LoginID,
                    //        LogConfig.MachineID,
                    //        printTime,
                    //        pocetKusu,
                    //        (decimal)1,
                    //        string.Empty,
                    //        nactenaData,
                    //        eventsrow.barcodeSended,
                    //        txtOrderNumber.Text,
                    //        string.Empty,
                    //        printGuid,
                    //        "P"
                    //        );

                    eventsrow.popis = string.Empty;
                    addEventsRow(eventsrow);

                    //Kurzor zpet
                    Cursor = Cursors.Arrow;
                    //Konec
                    break;
                    
                }
                //Neco se nepovedlo, zeptam se na opakovani
                catch (Exception ex)
                {
                    Cursor = Cursors.Arrow;
                    //Zpracovani
                    procureException(ex);
                    //Opetovny dotaz
                    if (MessageBox.Show("Operace se nezdaøila z dùvodu:\n" + ex.Message + "\n\nPøejete si operaci opakovat?", "Pila", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) continue;
                    else break;
                }
            }
        }

        //Upravi carovy kod - vymeni , za -
        private string editBarcodeSended(string odeslanaData)
        {
            const string separator = "-";
            return txtOrderNumber.Text.Trim() + separator + txtItemNumber.Text.Trim() + separator + txtProfileLength.Text.Trim();
        }

        //Pridani zaznamu do databaze - z jednotlivych polozek
        private void addEventsRow(string login, string masina, DateTime cas, decimal puvodniKusy, decimal pocetKusu, string pozanamka, string nactenaData, string dataKodeslani, string zakazka, string popis, Guid guid, string typZaznamu)
        {
            //Ulozeni odeslanych dat do pameti ... 
            vyroba.FASK_Events.AddFASK_EventsRow(
                login,
                masina,
                cas,
                puvodniKusy,
                pocetKusu,
                pozanamka,
                nactenaData,
                dataKodeslani,
                zakazka,
                popis,
                guid,
                typZaznamu,
                //Doplneni nepouzivanych sloupcu v pile na NULL
                null, null, null, null, null,null
                );

            //Commit
            commitChanges();
        }

        //Promitnuti ulozenych dat do pameti na server
        private void commitChanges()
        {
            eta.Connection.ConnectionString = LogConfig.SqlConnectionStringLocal;
            eta.Update(vyroba);
        }

        //Pridani zaznamu do databaze - z radku
        private void addEventsRow(Database.Vyroba.FASK_EventsRow row)
        {
            //Ulozeni odeslanych dat do pameti ... 
            vyroba.FASK_Events.AddFASK_EventsRow(row);

            //Commit
            commitChanges();
        }

        //Ulozeni infa o vstupnim portu
        private void buttonSaveSPIN_Click_1(object sender, EventArgs e)
        {
            PilaConfig.config.Pila[0].SP_IN_BaudRate = txt_spinBaudRate.Text;
            PilaConfig.config.Pila[0].SP_IN_DataBits = txt_spinDataBits.Text;
            PilaConfig.config.Pila[0].SP_IN_Parity = txt_spinParity.Text;
            PilaConfig.config.Pila[0].SP_IN_PortName = txt_spinPort.Text;
            PilaConfig.config.Pila[0].SP_IN_StopBits = txt_spinStopBits.Text;
            
            //Ulozeni do xml
            PilaConfig.Save();

            //Log-event
            Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        //Ulozeni infa o vystupnim portu
        private void buttonSaveSPOUT_Click_1(object sender, EventArgs e)
        {
            PilaConfig.config.Pila[0].SP_OUT_BaudRate = txt_spoutBaudRate.Text;
            PilaConfig.config.Pila[0].SP_OUT_DataBits = txt_spoutDataBits.Text;
            PilaConfig.config.Pila[0].SP_OUT_Parity = txt_spoutParity.Text;
            PilaConfig.config.Pila[0].SP_OUT_PortName = txt_spoutPort.Text;
            PilaConfig.config.Pila[0].SP_OUT_StopBits = txt_spoutStopBits.Text;

            //Ulozeni do xml
            PilaConfig.Save();

            //Log-event
            Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        //Uzavreni formu
        private void frmPila_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Uzavreni vstupu pokud je otevren
            if (serialPortIN.IsOpen) serialPortIN.Close();

            //Uzavrei vystupu pokud je otevren
            if (serialPortOUT.IsOpen) serialPortOUT.Close();
        }

        //Minuly stav portuu
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

        //Ulozeni informaci o tiskovem serveru
        private void buttonPrintServerSave_Click(object sender, EventArgs e)
        {
            //Kopie
            string tmp = txt_template.Text;
            string pname = txt_printerName.Text;
            string paddr = txt_printerAddress.Text;

            //INFO: po uprave staci sablona
            //Musi byt vyplneny vsechny hodnoty
            //if (tmp == string.Empty || pname == string.Empty || paddr == string.Empty || 0 == num_timeout.Value)
            if(tmp == string.Empty)
            {
                //MessageBox.Show("Nastavení tiskového serveru se nezdaøilo. Je tøeba vyplnit všechny položky a timeout musí být vìtší než nula.", "Pila", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Nastavení tiskového serveru se nezdaøilo. Je tøeba vyplnit název šablony.", "Pila", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Zapsani novych hodnot do tabulky
                PilaConfig.config.Print_Server[0].Template = tmp;
                PilaConfig.config.Print_Server[0].PrinterName = pname;
                PilaConfig.config.Print_Server[0].PrinterAddress = paddr;
                PilaConfig.config.Print_Server[0].Timeout = num_timeout.Value.ToString();

                //A ulozeni konfiguracniho xml souboru
                PilaConfig.Save();

                //Log
                Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
            }
        }

        //Inicializace polozek pro tiskovy server
        private void InitPrintServer()
        {
            try
            {
                //V tabulce s konfiguraci neni zadny radek - nebylo nic nacteno
                if (PilaConfig.config.Print_Server.Rows.Count == 0)
                {
                    //Vlozim radek s prazdnymi hodnotami - tiskovy server jeste nebyl nastaven
                    PilaConfig.config.Print_Server.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, num_timeout.Value.ToString() });
                    //A ulozim - uz vzdy bude dostupny xml -> alespone jeden radek v tabulce
                    PilaConfig.Save();
                }
                //V tabulce by mel byt prave jeden radek - nacteny z xml souboru
                else
                {
                    txt_printerAddress.Text = PilaConfig.config.Print_Server[0].PrinterAddress;
                    txt_printerName.Text = PilaConfig.config.Print_Server[0].PrinterName;
                    txt_template.Text = PilaConfig.config.Print_Server[0].Template;
                    num_timeout.Value = int.Parse(PilaConfig.config.Print_Server[0].Timeout);
                }
            }
            catch (Exception ex)
            {
                procureException(ex);
            }
        }

        //Opakovat tisk zobrazenych parametru
        private void buttonPrintParams_Click(object sender, EventArgs e)
        {
            repeatPrint();
        }

        //Opakovat tisk
        private void repeatPrint()
        {
            //Kontrola nactenych polozek
            string data = string.Empty;
            if ((data = CreateOutputBarCode()) == string.Empty)
            {
                showDataSentError();
                return;
            }

            //Jdeme tisknout
            printData((decimal)1, (decimal)1, txtBarcode.Text.Trim(), data, vyroba);
        }

        //Kontrola jednotlivych casti CK
        bool checkBarcodeItems(string delkaProfilu, string cisloZakazky, string cisloPolozky)
        {
            //Nic nesmi byt prazdne
            if (delkaProfilu == string.Empty || cisloZakazky == string.Empty || cisloPolozky == string.Empty) return false;

            //Pokud jedna z casti neni cislo a nebo je mensi nez 0, chyba
            int sizeMinimum = 0; Int64 dummy;
            //Pokud by nestacil rozsah Intu64 bylo by nejlepsi udelat funkci na kontrolu, zda je kazdy prvek cislo a TryParse na Double stejnym zpusobem - za predpokladu ze CK obsahuje jen cela cisla.
            if (!Int64.TryParse(delkaProfilu, out dummy) || dummy < sizeMinimum || !Int64.TryParse(cisloZakazky, out dummy) || dummy < sizeMinimum || !Int64.TryParse(cisloPolozky, out dummy) || dummy < sizeMinimum) return false;

            //OK
            return true;
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }
    }
}