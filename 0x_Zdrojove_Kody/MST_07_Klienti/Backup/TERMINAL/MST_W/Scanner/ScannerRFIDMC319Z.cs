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
    public class ScannerRFIDMC319Z : ScannerBaseRFID
    {
        public override event ScannerEventRFIDHandler DataReady;
        public override event RFIDTagHandler RFIDTagEvent;

        RFIDReader deviceReader = null;
        List<RFIDBarcodeData> barCodes = new List<RFIDBarcodeData>(); //sem se budou ukladat kody nactene rfid scannerem ...
        List<RFIDTagData> rfidTagDatas = new List<RFIDTagData>(); //sem se budou ukladat kody nactene rfid scannerem ...

        System.Threading.Timer timer = null; //new Timer(new TimerCallback(ProcessTags), null, Timeout.Infinite, Timeout.Infinite);

        public ScannerRFIDMC319Z()
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
                        
        }

        void Events_StatusNotify(object sender, Symbol.RFID3.Events.StatusEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(e.StatusEventData.StatusEventType.ToString());
            //throw new NotImplementedException();
#endif
        }

        private void ProcessTags(object state)
        {
            try
            {
                List<RFIDBarcodeData> barCodesTmp;
                lock (barCodes)
                {
                    barCodesTmp = barCodes;
                    barCodes = new List<RFIDBarcodeData>();
                }

                if (barCodesTmp.Count > 0)
                {
                    // Vyvolani udalosti nacteni kodu...
                    if (this.DataReady != null)
                    {
                        this.DataReady(
                            this,
                            new ScannerRFIDEventArgs(barCodesTmp));
                    }
                }

                List<RFIDTagData> rfidTagDatasTmp;
                lock (rfidTagDatas)
                {
                    rfidTagDatasTmp = rfidTagDatas;
                    rfidTagDatas = new List<RFIDTagData>();
                }

                if (rfidTagDatasTmp.Count > 0)
                {
                    // Vyvolani udalosti nacteni kodu...
                    if (this.RFIDTagEvent != null)
                    {
                        this.RFIDTagEvent(this, new RFIDTagDataEventArgs(rfidTagDatasTmp));
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private int tsleep = 100;
        void Events_ReadNotify(object sender, Symbol.RFID3.Events.ReadEventArgs args)
        {

        //    throw new NotImplementedException();
        //}

        //void deviceReader_TagEvent(object sender, ReaderEventArgs args)
        //{
            Symbol.RFID3.TagData[] Tags = deviceReader.Actions.GetReadTags(1000);
//#if DEBUG
//            System.Diagnostics.Debug.WriteLine(Tags == null ? "Tags null" : ("Tags: " + Tags.Count()));
//#endif
            // uspani threadu kvuli problemu s nacitanim tagu (jinak obcas nenacital ... nejaky problem s threadem)
            Thread.Sleep(tsleep);

            try
            {
                //if (args != null)
                if (Tags != null)
                {
                    //Tags = args.Reader.Tags;

                    foreach (TagData tag in Tags)
                    {
                        #region Stara verze nacitani RFID tagu ...
                        {
                            string tag_ID = null;
                            string nactenyTag = string.Empty;//prazdny

                            tag_ID = tag.TagID;

                            try
                            {
                                // TODO : upravit praci s obsahem tagu z retezce na retezec ...
                                //nactenyTag = tag_ID;
                                if (MST_Global.RFIDUkladatNacitatText)
                                {
                                    byte[] tagID = RFID.Routines.StringHexToByteHex(tag_ID);
                                    nactenyTag = RFID.Routines.ByteasciiArrayToString(tagID);
                                }
                                else
                                {
                                    //nactenyTag = RFID.Routines.ByteHexArrayToStringHex(tagID);
                                    nactenyTag = tag_ID.TrimStart(new char[] { '0' });
                                }
                            }
                            catch
                            {
                            }

                            if (nactenyTag != string.Empty)
                            {
                                // TODO: 20151218 PeV: poresit delku nacteneho kodu, tato zmena provedena pouze kvuli prezentaci Husky (serltnum ma 21 znaku ...)
								//if (nactenyTag.Length > 21)
								//    nactenyTag = nactenyTag.Substring(nactenyTag.Length - 21, 21);

                                // pokud je kod v seznamu ignorovanych tak se neprida do seznamu
                                if (
                                    (Settings.RemovedRFIDCodes != null)
                                    &&
                                    (Settings.RemovedRFIDCodes.Contains(nactenyTag))
                                   )
                                {
                                    continue;
                                }


                                RFIDBarcodeData bdata = new RFIDBarcodeData(nactenyTag, (uint)(0), string.Empty, (uint)tag_ID.Length);
                                lock (barCodes)
                                {
                                    if (!barCodes.Contains(bdata))
                                        barCodes.Add(bdata);
                                }
                            }
                        }
                        #endregion

                        #region Nova verze nacitani RFID tagu ...
                        {
                            lock (rfidTagDatas)
                            {
                                //var tagexist = rfidTagDatas.Where(x => x.TagID == tag.TagID);
                                RFIDTagData rdata;
                                if (rfidTagDatas.Exists(x => x.TagID == tag.TagID))
                                {
                                    rdata = rfidTagDatas.First();
                                }
                                else
                                {
                                    rdata = new RFIDTagData();
                                    rfidTagDatas.Add(rdata);
                                }
                                if (rdata.TagID != tag.TagID)
                                    rdata.TagID = tag.TagID;
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
                                        // CHECK : !!! tagid je primo soucasti tag struktury ... 
                                        // TODO : toto spise jen pro overeni ???
                                        if (rdata.TagID != tag.MemoryBankData)
                                            rdata.TagID = tag.MemoryBankData;
                                        break;
                                    case MEMORY_BANK.MEMORY_BANK_USER:
                                        if (rdata.UserMemory != tag.MemoryBankData)
                                            rdata.UserMemory = tag.MemoryBankData;
                                        break;
                                    default:
                                        // ???
                                        break;
                                }
                                rdata.CountReaded++;
                                rdata.OpCode = tag.OpCode.ToString();
                                rdata.OpStatus = tag.OpStatus.ToString();
                            }
                        }
                        #endregion
                    }
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

        public override void StartScan()
        {
            timer.Change(1000, 1000);
            if (deviceReader != null)
                //deviceReader.ReadMode = ReadMode.AUTONOMOUS;
                deviceReader.Actions.Inventory.Perform();
        }

        public override void StopScan()
        {
            if (deviceReader != null)
                //deviceReader.ReadMode = ReadMode.ONDEMAND;
                deviceReader.Actions.Inventory.Stop();
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

        //public override List<string> RemovedCodes
        //{
        //    get;
        //    set;
        //}

    }
}
