using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;
using System.Reflection;
using System.IO;
using System.Xml;
using Fask.Logging;
using System.Threading;

namespace Fask.MST_W.Scanner
{
    public class ScannerRFIDMC319Z_v2 : ScannerBaseRFID
    {
        private bool _isTriggerEnabled = false;
        /// <summary>
        /// pri ukonceni dialogu se musi explicitne vypnout, jinak to bude stale cist ...
        /// </summary>
        public bool TriggerEnabled
        {
            get { return this._isTriggerEnabled; }
            set { this._isTriggerEnabled = value; }
        }

        [Obsolete("Toto je treba predelat", false)]
        public override event ScannerEventRFIDHandler DataReady;
        public override event RFIDTagHandler RFIDTagEvent;

        public delegate void RFIDScannerStartedHandler();
        public delegate void RFIDScannerStoppedHandler();

        public event RFIDScannerStartedHandler RFIDScannerStarted;
        public event RFIDScannerStoppedHandler RFIDScannerStopped;

        private bool _isReading = false;
        public bool IsReading
        {
            get { return _isReading; }
        }

        protected void OnRFIDScannerStarted()
        {
            if (RFIDScannerStarted != null)
            {
                try
                {
                    RFIDScannerStarted();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
        }

        protected void OnRFIDScannerStopped()
        {
            if (RFIDScannerStopped != null)
            {
                try
                {
                    RFIDScannerStopped();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
        }

        RFIDReader deviceReader = null;

        List<RFIDTagData> rfidTagDatas = new List<RFIDTagData>(); //sem se budou ukladat kody nactene rfid scannerem ...
        //Dictionary<string, RFIDTagData> rfidTagDatas = new Dictionary<string, RFIDTagData>(); //sem se budou ukladat kody nactene rfid scannerem ...
        System.Threading.Timer timer = null; //new Timer(new TimerCallback(ProcessTags), null, Timeout.Infinite, Timeout.Infinite);

        public ScannerRFIDMC319Z_v2()
        {
            this.InitializeScanner();
        }

        public override bool Enabled
        {
            get
            {
                if (deviceReader == null)
                    return false;

                return deviceReader.IsConnected;
                //return deviceReader.ReaderStatus == ReaderStatus.ONLINE;
            }
        }


        protected override void InitializeScanner()
        {
            TerminateScanner();

            //deviceReader = new RFIDReader();
            deviceReader = new RFIDReader("localhost", 5084, 0);

            SetupEvents();

            timer = new Timer(new TimerCallback(ProcessTags), null, Timeout.Infinite, Timeout.Infinite);
        }


        private void SetupEvents()
        {
            //deviceReader.TagEvent += new ReaderEventHandler(deviceReader_TagEvent);

            this.Enable();
            deviceReader.Events.ReadNotify += new Symbol.RFID3.Events.ReadNotifyHandler(Events_ReadNotify);
            deviceReader.Events.StatusNotify += new Symbol.RFID3.Events.StatusNotifyHandler(Events_StatusNotify);
            deviceReader.Events.NotifyAccessStartEvent = true;
            deviceReader.Events.NotifyAccessStopEvent = true;
            deviceReader.Events.NotifyAntennaEvent = true;
            deviceReader.Events.NotifyBufferFullEvent = true;
            deviceReader.Events.NotifyBufferFullWarningEvent = true;
            deviceReader.Events.NotifyEASAlarmEvent = true;
            deviceReader.Events.NotifyGPIEvent = true;
            deviceReader.Events.NotifyHandheldTriggerEvent = true;
            deviceReader.Events.NotifyInventoryStartEvent = true;
            deviceReader.Events.NotifyInventoryStopEvent = true;
            deviceReader.Events.NotifyReaderDisconnectEvent = true;
            deviceReader.Events.NotifyReaderExceptionEvent = true;

            // todo : pridat vyber session modu do konfigurace ... 
            ushort[] antIDs = deviceReader.Config.Antennas.AvailableAntennas;
            Antennas.SingulationControl singulationControl = deviceReader.Config.Antennas[antIDs[0]].GetSingulationControl();
            singulationControl.Session = SESSION.SESSION_S0;
            deviceReader.Config.Antennas[antIDs[0]].SetSingulationControl(singulationControl);

            TagStorageSettings tss = deviceReader.Config.GetTagStorageSettings();
            tss.DiscardTagsOnInventoryStop = true;
            tss.MaxSizeMemoryBank = 2 * 200;
            tss.MaxTagIDLength = 2 * 200;
            tss.TagFields = TAG_FIELD.ALL_TAG_FIELDS;
            deviceReader.Config.SetTagStorageSettings(tss);

            TriggerInfo triggerInfo = new TriggerInfo();
            triggerInfo.EnableTagEventReport = true;
            triggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;
            triggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;
            triggerInfo.TagEventReportInfo.ReportNewTagEvent = TAG_EVENT_REPORT_TRIGGER.IMMEDIATE;
            triggerInfo.TagEventReportInfo.ReportTagBackToVisibilityEvent = TAG_EVENT_REPORT_TRIGGER.IMMEDIATE;
            triggerInfo.TagEventReportInfo.ReportTagInvisibleEvent = TAG_EVENT_REPORT_TRIGGER.IMMEDIATE;

        }

        void Events_StatusNotify(object sender, Symbol.RFID3.Events.StatusEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(e.StatusEventData.StatusEventType.ToString());
            //throw new NotImplementedException();
#endif
            Symbol.RFID3.Events.StatusEventData eventData = e.StatusEventData;
            switch (eventData.StatusEventType)
            {

                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.HANDHELD_TRIGGER_EVENT:
                    if (!this._isTriggerEnabled)
                        break;

                    //TriggerInfo triggerInfo = m_TriggerForm.getTriggerInfo();
                    if (eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED 
                        //&&triggerInfo.StartTrigger.Type == START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE
                        )
                    {
                        // Lets start the inventory upon GPI event even if the StartTrigger is configured as immediate
                        processUIOrGPIEvent(eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED);
                    }
                    if (eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED 
                        //&& triggerInfo.StopTrigger.Type == STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_IMMEDIATE
                        )
                    {
                        processUIOrGPIEvent(eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED);
                    }
                    break;

                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT:
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT:
                    // vyvolat udalost startu ...
                    OnRFIDScannerStarted();
                    _isReading = true;
                    break;
                
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT:
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT:
                    // vyvolat udalost zastaveni ...
                    OnRFIDScannerStopped();
                    _isReading = false;
                    break;

                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ANTENNA_EVENT:
                    //Log.Write("RFID reader antena :" + eventData.AntennaEventData.AntennaID.ToString() + "=" + eventData.AntennaEventData.AntennaEvent.ToString());
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT:
                    this.deviceReader.Actions.PurgeTags();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT:
                    this.deviceReader.Actions.PurgeTags();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT:
                    Logging.Log.Write("RFID reader disconnected :" + eventData.DisconnectionEventData.DisconnectEventInfo.ToString());
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.GPI_EVENT:
                    //Log.Write("RFID GPI :" + eventData.GPIEventData.GPIEvent.ToString());
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.NXP_EAS_ALARM_EVENT:
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT:
                    Log.Write("RFID reader exception :" + eventData.ReaderExceptionEventData.ReaderExceptionEventInfo);
                    break;
                default:
                    break;
            }

        }

        private void processUIOrGPIEvent(bool startRead)
        {
            try
            {
                if (this.deviceReader != null && this.deviceReader.IsConnected)
                {
                    if (startRead)
                    {
                        StartScan();
                    }
                    else
                    {
                        StopScan();
                    }
                }
                else
                {
                    notifyUser("Please connect to a reader", "Read Operation");
                }
            }
            catch (OperationFailureException ex)
            {
                notifyUser(ex.VendorMessage, "Read Operation");
            }
        }

        private void ProcessTags(object state)
        {
            try
            {
                List<RFIDTagData> rfidTagDatasTmp;
                //Dictionary<string, RFIDTagData> rfidTagDatasTmp;
                lock (rfidTagDatas)
                {
                    rfidTagDatasTmp = rfidTagDatas;
                    rfidTagDatas = new List<RFIDTagData>();
                    //rfidTagDatas = new Dictionary<string, RFIDTagData>();
                }

                if (rfidTagDatasTmp.Count > 0)
                {
                    // Vyvolani udalosti nacteni kodu...
                    if (this.RFIDTagEvent != null)
                    {
                        this.RFIDTagEvent(this, new RFIDTagDataEventArgs(rfidTagDatasTmp));
                        //this.RFIDTagEvent(this, new RFIDTagDataEventArgs(rfidTagDatasTmp.Values.ToList()));
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private int tags2read = 100;
        private int tsleep = 500;
        void Events_ReadNotify(object sender, Symbol.RFID3.Events.ReadEventArgs args)
        {
            try
            {
                // uspani threadu kvuli problemu s nacitanim tagu (jinak obcas nenacital ... nejaky problem s threadem)
                Thread.Sleep(tsleep);

                Symbol.RFID3.TagData[] Tags = deviceReader.Actions.GetReadTags(tags2read);

                //if (args != null)
                if (Tags != null)
                {
                    //Tags = args.Reader.Tags;
                    lock (rfidTagDatas)
                    {

                        foreach (TagData tag in Tags)
                        {
#if DEBUG
                            //System.Diagnostics.Debug.WriteLine(e.StatusEventData.StatusEventType.ToString());
                            System.Diagnostics.Debug.WriteLine(tag.OpCode.ToString() + " , " + tag.OpStatus.ToString() + " ,TagID:" + tag.TagID.ToString());
                            //throw new NotImplementedException();
#endif

                            /* 
                             * Display all inventories tags or tags on which 
                             * Read access operation was successful
                             */
                            if ((tag.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE)
                                ||
                                (
                                tag.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ
                                &&
                                tag.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS
                                )
                               )
                            {
                                #region Nova verze nacitani RFID tagu ...
                                {
                                    //lock (rfidTagDatas)
                                    //{
                                    ////var tagexist = rfidTagDatas.Where(x => x.TagID == tag.TagID);
                                    //RFIDTagData rdata;
                                    ////if (rfidTagDatas.Exists(x => x.TagID == tag.TagID))
                                    //if (rfidTagDatas.ContainsKey(tag.TagID))
                                    //{
                                    //    //rdata = rfidTagDatas.First();
                                    //    rdata = rfidTagDatas[tag.TagID];
                                    //}
                                    //else
                                    //{
                                    //    rdata = new RFIDTagData();
                                    //    //rfidTagDatas.Add(rdata);
                                    //    rfidTagDatas.Add(tag.TagID, rdata);
                                    //}

                                    if (
                                        (Settings.RemovedRFIDCodes != null)
                                        &&
                                        (Settings.RemovedRFIDCodes.Contains(tag.TagID))
                                        )
                                    {
                                        continue; // tento kod je v seznamu odstranenych kodu
                                    }

                                    RFIDTagData rdata = new RFIDTagData();

                                    //if (rdata.TagID != tag.TagID)
                                    //    rdata.TagID = tag.TagID;

                                    rdata.TagID = tag.TagID;

                                    OpenNETCF.Media.SystemSounds.Beep.Play();

                                    switch (tag.MemoryBank)
                                    {
                                        case MEMORY_BANK.MEMORY_BANK_EPC:
                                            if (rdata.EPCMemory != tag.MemoryBankData)
                                                rdata.EPCMemory = tag.MemoryBankData;
                                            break;
                                        case MEMORY_BANK.MEMORY_BANK_RESERVED:
                                            if (rdata.ReservedMemory != tag.MemoryBankData)
                                                rdata.ReservedMemory = tag.MemoryBankData;
                                            break;
                                        case MEMORY_BANK.MEMORY_BANK_TID:
                                            if (rdata.TIDMemory != tag.MemoryBankData)
                                                rdata.TIDMemory = tag.MemoryBankData;
                                            break;
                                        case MEMORY_BANK.MEMORY_BANK_USER:
                                            if (rdata.UserMemory != tag.MemoryBankData)
                                                rdata.UserMemory = tag.MemoryBankData;
                                            break;
                                        default:
                                            // ???
                                            break;
                                    }
                                    //rdata.CountReaded = tag.TagSeenCount;
                                    rdata.CountReaded = 1;
                                    rdata.RSSI = tag.PeakRSSI;
                                    rdata.OpCode = tag.OpCode.ToString();
                                    rdata.OpStatus = tag.OpStatus.ToString();

                                    rfidTagDatas.Add(rdata);
                                    //}
                                }
                                #endregion
                            }
                            else if (
                                tag.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE
                                && tag.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS
                                )
                            {
                                //string ttttid = tag.TagID;
                                //Log.Write(ttttid);
                            }
                            else
                            {
                            }
                        }
                    } // end lock ...

                    // uspani threadu kvuli problemu s nacitanim tagu (jinak obcas nenacital ... nejaky problem s threadem)
                    //Thread.Sleep(tsleep);
                }
            }
            catch //(Exception ex)
            {
                //Log.Write(ex);
            }

        }

        public override void TerminateScanner()
        {
            this.Disable();
            deviceReader = null;
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        public override void Enable()
        {
            if (deviceReader != null && !deviceReader.IsConnected)
                deviceReader.Connect();
        }

        public override void Disable()
        {
            if (deviceReader != null && deviceReader.IsConnected)
                deviceReader.Disconnect();
        }

        //private bool memoryEPC = true;
        //private bool memoryUSER = true;
        //private bool memoryRESERVED = false;
        //private bool memoryTID = true;

        /// <summary>
        /// True: provadi jen inventory operace, False: dela operaci s dohledanim TID ...
        /// </summary>
        private bool doInventory = true;
        /// <summary>
        /// Rozlisuje, zda se vola jen inentory operace pri vyhledavani tagu
        /// nebo se i dohledava TID ...
        /// </summary>
        public bool A_DoInventory
        {
            get { return doInventory; }
            set { doInventory = value; }
        }

        public override void StartScan()
        {
            if (this._isReading)
                return;

            timer.Change(1000, 1000);
            if (deviceReader != null)
            {
                //deviceReader.ReaderCapabilities.
                //deviceReader.ReadMode = ReadMode.AUTONOMOUS;

                deviceReader.Actions.TagAccess.OperationSequence.DeleteAll();

                if (!AllMemories || (!MST_Global.RFID_memoryEPC && !MST_Global.RFID_memoryUSER && !MST_Global.RFID_memoryRESERVED && !MST_Global.RFID_memoryTID))
                {
                    if (doInventory)
                    {
                        deviceReader.Actions.Inventory.Perform();
                    }
                    else
                    {
                        TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                        op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                        op.ReadAccessParams = new TagAccess.ReadAccessParams();
                        op.ReadAccessParams.AccessPassword = 0;
                        op.ReadAccessParams.ByteCount = 0;
                        op.ReadAccessParams.ByteOffset = 0;
                        op.ReadAccessParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;

                        deviceReader.Actions.TagAccess.OperationSequence.Add(op);

                        deviceReader.Actions.TagAccess.OperationSequence.PerformSequence();
                    }
                }
                else
                {
                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    if (MST_Global.RFID_memoryTID)
                    {
                        op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                        op.ReadAccessParams.AccessPassword = 0;
                        op.ReadAccessParams.ByteCount = 0;
                        op.ReadAccessParams.ByteOffset = 0;
                        op.ReadAccessParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
                        deviceReader.Actions.TagAccess.OperationSequence.Add(op);
                    }

                    if (MST_Global.RFID_memoryEPC)
                    {
                        op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                        op.ReadAccessParams.AccessPassword = 0;
                        op.ReadAccessParams.ByteCount = 0;
                        op.ReadAccessParams.ByteOffset = 0;
                        op.ReadAccessParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
                        deviceReader.Actions.TagAccess.OperationSequence.Add(op);
                    }

                    if (MST_Global.RFID_memoryUSER)
                    {
                        op = new TagAccess.Sequence.Operation();
                        op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                        op.ReadAccessParams.AccessPassword = 0;
                        op.ReadAccessParams.ByteCount = 0;
                        op.ReadAccessParams.ByteOffset = 0;
                        op.ReadAccessParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
                        deviceReader.Actions.TagAccess.OperationSequence.Add(op);
                    }

                    if (MST_Global.RFID_memoryRESERVED)
                    {
                        op = new TagAccess.Sequence.Operation();
                        op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                        op.ReadAccessParams.AccessPassword = 0;
                        op.ReadAccessParams.ByteCount = 0;
                        op.ReadAccessParams.ByteOffset = 0;
                        op.ReadAccessParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
                        deviceReader.Actions.TagAccess.OperationSequence.Add(op);
                    }

                    deviceReader.Actions.TagAccess.OperationSequence.PerformSequence();
                }
            }
            //Thread.Sleep(100); //nejake zpozdeni ... ???
        }

        public override void StopScan()
        {
            if (!this._isReading)
                return;

            if (deviceReader != null)
            {
                //deviceReader.ReadMode = ReadMode.ONDEMAND;

                try
                {
                    if (deviceReader.Actions.TagAccess.OperationSequence.Length <= 0)
                        deviceReader.Actions.Inventory.Stop();
                    else
                        deviceReader.Actions.TagAccess.OperationSequence.StopSequence();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }

                try
                {
                    deviceReader.Actions.PurgeTags();
                }
                catch
                {
                }
            }
            timer.Change(500, Timeout.Infinite);
        }

        public override int Power
        {
            get
            {
                return 0;
            }
            set
            {
                ;
            }
        }

        public override List<int> PowerLevels
        {
            get { return new List<int>(); }
        }

        public override void SetSource(string sourceName)
        {
            ;
        }

        public override List<string> GetSources()
        {
            return new List<string>();
        }

        public override void Configure()
        {
            //using (MC9090.FrmConfigurationMain frmMain = new MC9090.FrmConfigurationMain(deviceReader))
            //{
            //    frmMain.ShowDialog();
            //}

            //try
            //{
            //    string configpath = ConfigurationPath();
            //    //this.CreateFile(configpath);
            //    this.ConfigurationSave(configpath);
            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.Message, "RFID configuration");
            //}
        }

        public bool TIDRead(string tagid, out string data)
        {
            try
            {
                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
                rp.AccessPassword = 0;
                rp.ByteCount = 0;
                rp.ByteOffset = 0;
                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);
                data = tagData.MemoryBankData;

#if DEBUG
                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
#endif

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RESERVEDWrite(string tagid, string data)
        {
            try
            {
                //byte[] dataBytes = Encoding.Convert(Encoding.Default, Encoding.ASCII, Encoding.Default.GetBytes(data));
                byte[] dataBytes = Fask.MST_W.RFID.Routines_v2.StringHex2Byte(data);
                Symbol.RFID3.TagAccess.WriteAccessParams wp = new TagAccess.WriteAccessParams();
                wp.AccessPassword = 0;
                wp.ByteOffset = 0;
                wp.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
                wp.WriteData = dataBytes;
                wp.WriteDataLength = Convert.ToUInt32(dataBytes.Length);
                deviceReader.Actions.TagAccess.WriteWait(tagid, wp, null);

                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
                rp.AccessPassword = 0;
                rp.ByteCount = 0;
                rp.ByteOffset = 0;
                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);

#if DEBUG
                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
#endif

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RESERVEDRead(string tagid, out string data)
        {
            try
            {
                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
                rp.AccessPassword = 0;
                rp.ByteCount = 0;
                rp.ByteOffset = 0;
                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);
                data = tagData.MemoryBankData;

#if DEBUG
                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
#endif

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool USERWrite(string tagid, string data)
        {
            try
            {
                //byte[] dataBytes = Encoding.Convert(Encoding.Default, Encoding.ASCII, Encoding.Default.GetBytes(data));
                byte[] dataBytes = Fask.MST_W.RFID.Routines_v2.StringHex2Byte(data);
                Symbol.RFID3.TagAccess.WriteAccessParams wp = new TagAccess.WriteAccessParams();
                wp.AccessPassword = 0;
                wp.ByteOffset = 0;
                wp.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
                wp.WriteData = dataBytes;
                wp.WriteDataLength = Convert.ToUInt32(dataBytes.Length);
                deviceReader.Actions.TagAccess.WriteWait(tagid, wp, null);

                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
                rp.AccessPassword = 0;
                rp.ByteCount = 0;
                rp.ByteOffset = 0;
                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);

#if DEBUG
                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
#endif

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool USERRead(string tagid, out string data)
        {
            try
            {
                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
                rp.AccessPassword = 0;
                rp.ByteCount = 0;
                rp.ByteOffset = 0;
                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);
                data = tagData.MemoryBankData;                

#if DEBUG
                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
#endif

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool EPCRead(string tagid, out string data)
        {
            try
            {
                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
                rp.AccessPassword = 0;
                rp.ByteCount = 0;
                rp.ByteOffset = 0;
                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);
                data = tagData.MemoryBankData;

#if DEBUG
                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
#endif

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// zapise data ...
        /// </summary>
        /// <param name="tagid">id tagu v hex string</param>
        /// <param name="data">data do epc memory v hex string</param>
        /// <param name="accesspassword">access password v hex string [max 16 znaku]</param>
        /// <returns></returns>
        public bool EPCWrite(string tagid, string datahex, string accesspassword, out string newtagid)
        {
            // TODO : udelat zvlast nastaveni vlastnosti tagu ...
            // zde pouze data ... (bez prvnich 32bitu tagu ... 
            try
            {
                string crchex = datahex.Substring(0, 4);
                string pchex = datahex.Substring(4, 4);
                string epcdatahex = datahex.Substring(8);

                //byte[] dataBytes = Encoding.Convert(Encoding.Default, Encoding.ASCII, Encoding.Default.GetBytes(data));
                byte[] dataBytes = Fask.MST_W.RFID.Routines_v2.StringHex2Byte(pchex + epcdatahex); // ulozim jak PC, tak EPC data...

                Symbol.RFID3.TagAccess.WriteAccessParams wp = new TagAccess.WriteAccessParams();
                if (string.IsNullOrEmpty(accesspassword))
                    wp.AccessPassword = 0;
                else
                    wp.AccessPassword = Convert.ToUInt32(accesspassword, 16); // z hexa ...
                wp.ByteOffset = 2; // 2 prvni byty jsou crc ... to preskakuji ...
                wp.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
                wp.WriteData = dataBytes;
                wp.WriteDataLength = Convert.ToUInt32(dataBytes.Length);                
                deviceReader.Actions.TagAccess.WriteWait(
                    tagid
                    , wp
                    , null);

                //Symbol.RFID3.AccessFilter accfilter = new AccessFilter();
                //accfilter.TagPatternA.
                //deviceReader.Actions.TagAccess.BlockWriteEvent(
                //    wp, null, null
                //    );


//                TagAccess.ReadAccessParams rp = new TagAccess.ReadAccessParams();
//                rp.AccessPassword = 0;
//                rp.ByteCount = 0;
//                rp.ByteOffset = 0;
//                rp.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
//                TagData tagData = deviceReader.Actions.TagAccess.ReadWait(tagid, rp, null);

//#if DEBUG
//                System.Diagnostics.Debug.WriteLine(tagData.OpCode.ToString());
//                System.Diagnostics.Debug.WriteLine(tagData.OpStatus.ToString());
//                System.Diagnostics.Debug.WriteLine(tagData.MemoryBank.ToString() + " : " + tagData.MemoryBankData);
//#endif
                newtagid = epcdatahex.ToUpper();
                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool KillTag(string tagid, uint killpwd)
        {
            try
            {
                Symbol.RFID3.TagAccess.KillAccessParams kap = new TagAccess.KillAccessParams();
                kap.KillPassword = killpwd;

                deviceReader.Actions.TagAccess.KillWait(tagid, kap, null);

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool KillPwdWrite(string tagid, uint accesspwd, string newkillpwdHex)
        {
            try
            {
                Symbol.RFID3.TagAccess.WriteSpecificFieldAccessParams wsfa = new TagAccess.WriteSpecificFieldAccessParams();
                wsfa.AccessPassword = accesspwd;
                wsfa.WriteData = Fask.MST_W.RFID.Routines_v2.StringHex2Byte(newkillpwdHex.PadLeft(8, '0'));
                wsfa.WriteDataLength = (uint)wsfa.WriteData.Length;

                deviceReader.Actions.TagAccess.WriteKillPasswordWait(tagid, wsfa, null);

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool AccessPwdWrite(string tagid, uint accesspwd, string newacesspwdHex)
        {
            try
            {
                Symbol.RFID3.TagAccess.WriteSpecificFieldAccessParams wsfa = new TagAccess.WriteSpecificFieldAccessParams();
                wsfa.AccessPassword = accesspwd;
                wsfa.WriteData = Fask.MST_W.RFID.Routines_v2.StringHex2Byte(newacesspwdHex.PadLeft(8, '0'));
                wsfa.WriteDataLength = (uint)wsfa.WriteData.Length;

                deviceReader.Actions.TagAccess.WriteAccessPasswordWait(tagid, wsfa, null);

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool EPCUnlock(string tagid, uint accesspwd)
        {
            try
            {
                //LOCK_PRIVILEGE[] lArray = new LOCK_PRIVILEGE[]({});
                //Symbol.RFID3.LOCK_PRIVILEGE lPriv = LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE;

                Symbol.RFID3.TagAccess.LockAccessParams lp = new TagAccess.LockAccessParams();
                lp.AccessPassword = accesspwd;
                lp.LockPrivilege = new LOCK_PRIVILEGE[]{
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE
                };
                lp.LockPrivilege[lp.EPCMemory] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_UNLOCK;

                deviceReader.Actions.TagAccess.LockWait(tagid, lp, null);

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool EPCLock(string tagid, uint accesspwd)
        {
            try
            {
                //LOCK_PRIVILEGE[] lArray = new LOCK_PRIVILEGE[]({});
                //Symbol.RFID3.LOCK_PRIVILEGE lPriv = LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE;

                Symbol.RFID3.TagAccess.LockAccessParams lp = new TagAccess.LockAccessParams();
                lp.AccessPassword = accesspwd;
                lp.LockPrivilege = new LOCK_PRIVILEGE[]{
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE,
                    LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE
                };
                lp.LockPrivilege[lp.EPCMemory] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_READ_WRITE;

                deviceReader.Actions.TagAccess.LockWait(tagid, lp, null);

                return true;
            }
            catch (Symbol.RFID3.InvalidUsageException iue)
            {
                throw new Exception(iue.Message + "\n" + iue.VendorMessage, iue);
            }
            catch (Symbol.RFID3.OperationFailureException ofe)
            {
                throw new Exception(ofe.Message + "\n" + ofe.StatusDescription + "\n" + ofe.VendorMessage, ofe);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        internal void notifyUser(string notificationMessage, string notificationSource)
        {
            System.Windows.Forms.MessageBox.Show(notificationMessage, notificationSource);
        }

    }
}
