using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Fask.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.IRFIDProvider;
using Symbol.RFID3;

namespace RFID_Zebra_fx9600_Provider
{
    public class RFID_Zebra_fx9600_Provider : FASK.SledovaniVyroby.IRFIDProvider.IRFIDProvider
    {
        public string log_hlaska = string.Empty;

        RFIDReader m_ReaderAPI = null;
        LocationInfo locationInfo;
        internal bool m_IsConnected;
        private string connect_IP = null;
        private uint connect_Port = 0;

        private int? _cisloLinky = null;
        private string _volajici = null;

        private bool m_IsBlockWrite;
        public event RFIDHandler DataReady;
        public event RFID_ZEBRA_Handler DataReadyZEBRA;

        private AntennaInfo antennaInfo = new AntennaInfo();

        private string Port = string.Empty;

        List<string> list = new List<string>();

        private List<Item_Tag> item_Tags = null;

        private int _count_Write_Pruchody;
        public int Count_Write_Pruchody
        {
            get { return _count_Write_Pruchody; }
            set { _count_Write_Pruchody = value; }
        }

        internal AccessOperationResult m_AccessOpResult;
        //internal TagAccess.WriteAccessParams m_WriteParams;
        public RFID_Zebra_fx9600_Provider()
        {
            m_IsConnected = false;
            //connect_IP = "192.168.1.122";
            //connect_Port = "5084";
            m_AccessOpResult = new AccessOperationResult();


        }

        internal class AccessOperationResult
        {
            public RFIDResults m_Result;
            public String m_VendorMessage;
            public String m_StatusDescription;
            public ACCESS_OPERATION_CODE m_OpCode;

            public AccessOperationResult()
            {
                m_Result = RFIDResults.RFID_NO_ACCESS_IN_PROGRESS;
                m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
            }
        }

        internal string m_SelectedTagID = null;



   


        private void locateTag(string tagID, string operace)
        {
            try

            {

                m_ReaderAPI.Actions.TagLocationing.Perform(tagID, antennaInfo);

                Thread.Sleep(1000);

                m_ReaderAPI.Actions.TagLocationing.Stop();

                TagData[] tagData = m_ReaderAPI.Actions.GetReadTags(1000);

                if (tagData != null)

                {

                    for (int nIndex1 = 0; nIndex1 < tagData.Length; nIndex1++)

                    {

                        if (tagData[nIndex1].ContainsLocationInfo)

                        {

                            Console.WriteLine("Relative Distance:" + tagData[nIndex1].LocationInfo.RelativeDistance.ToString());

                        }

                    }

                }

            }

            catch (OperationFailureException ofe)

            {

                Console.WriteLine(ofe.ToString());
            
}
        }

        /// <summary>
        /// Lokalizace Tagu v dosahu zvolene anteny
        /// </summary>
        /// <param name="tagId">ID tagu</param>
        /// <param name="cisloAnteny">Cislo anteny, ktera lokalizuje</param>
        /// <param name="dobaLokalizace">cas po ktery se lokalizuje</param>
        /// <returns>true pokud je dany tag v dosahu</returns>
        public bool PerformTagLocationing(string tagId, int cisloAnteny,int dobaLokalizace)
        {
            bool tagFound = false;

            try
            {
                // Inicializace antény pro lokalizaci tagu.
                //AntennaInfo antennaInfo = new AntennaInfo(1, true); // Nastavte číslo antény a další parametry podle potřeby.
                ushort[] antennaList = new ushort[1] { (ushort)cisloAnteny };
                AntennaInfo antennaInfo = new AntennaInfo(antennaList);
                // Nastavení parametrů pro lokalizaci pouze pro daný tag.
                //TagData tagFilter = new TagData(tagId);
                TagData[] tagData = null;

                // Spuštění lokalizace tagu po dobu 4 sekund.
                m_ReaderAPI.Actions.TagLocationing.Perform(tagId, antennaInfo);
                Thread.Sleep(dobaLokalizace); // Čekání 4 sekundy.
                m_ReaderAPI.Actions.TagLocationing.Stop();

                // Získání dat o přečtených štítcích.
                tagData = m_ReaderAPI.Actions.GetReadTags(1000);

                // Zkontrolujte, zda byl tag nalezen.
                if (tagData != null)
                {
                    foreach (TagData tag in tagData)
                    {
                        if (tag.ContainsLocationInfo)
                        {
                            tagFound = true;
                            break;
                        }
                    }
                }
            }
            catch (OperationFailureException ofe)
            {
#if DEBUG
                Console.WriteLine(ofe.ToString()); 
#endif
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message); 
#endif
            }

            return tagFound;
        }


        public void Start()
        {
            try
            {
                if (m_ReaderAPI == null)
                {
                    #region zkouska pro framework 4.7

                    m_ReaderAPI = new RFIDReader(connect_IP, connect_Port, 5000);
                    if (m_ReaderAPI.IsConnected)
                        m_ReaderAPI.Disconnect(); 
                    #endregion

                    //m_ReaderAPI = new RFIDReader(connect_IP, connect_Port, 0); puvodni verze
                    m_ReaderAPI.Connect();
                    m_ReaderAPI.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
                    m_ReaderAPI.Events.AttachTagDataWithReadEvent = false;
                    m_ReaderAPI.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);
                    m_ReaderAPI.Events.NotifyGPIEvent = true;
                    m_ReaderAPI.Events.NotifyBufferFullEvent = true;
                    m_ReaderAPI.Events.NotifyBufferFullWarningEvent = true;
                    m_ReaderAPI.Events.NotifyReaderDisconnectEvent = true;
                    m_ReaderAPI.Events.NotifyReaderExceptionEvent = true;
                    m_ReaderAPI.Events.NotifyAccessStartEvent = true;
                    m_ReaderAPI.Events.NotifyAccessStopEvent = true;
                    m_ReaderAPI.Events.NotifyInventoryStartEvent = true;
                    m_ReaderAPI.Events.NotifyInventoryStopEvent = true;
                    m_ReaderAPI.Events.NotifyAntennaEvent = true;

                    m_IsConnected = true;
                    item_Tags = new List<Item_Tag>();
                }
            }
            catch (Exception ex)
            {
                m_ReaderAPI = null;

                //Log.Write("RFID m_ReaderAPI.Connect() failed");
                //Log.Write(ex);

                ExceptionHandler2.Handle(ex.ToString(), "Log_RFID_Zebra_fx9600_Provider", "txt");
                ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public void Stop()
        {

            try
            {
                if (m_ReaderAPI != null)
                {
                    if (m_ReaderAPI.IsConnected)
                        m_ReaderAPI.Disconnect();

#if !DEBUG
                    //try
                    //{
                    //    m_ReaderAPI.Dispose();


                    //}
                    //catch (Exception ex)
                    //{
                    //    ExceptionHandler2.Handle(ex.ToString(), "Log_RFID_Zebra_fx9600_Provider", "txt");
                    //    ExceptionHandler2.Handle(ex);
                    //} 
#endif

                    
                   
                    


                    m_ReaderAPI = null;
                    m_IsConnected = false;


                    item_Tags.Clear();
                    item_Tags = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex.ToString(), "Log_RFID_Zebra_fx9600_Provider", "txt");
                ExceptionHandler2.Handle(ex);
            }
        }

        public bool isOpen()
        {
            return this.m_IsConnected;
        }

        public string Info_DLL()
        {
            string text = string.Empty;

            if (null != m_ReaderAPI)
            {
                text = ("DLL Version: " + m_ReaderAPI.VersionInfo.Version);


            

            }

            return text;
        }

        public void init(string IP, uint PORT)
        {
            this.connect_IP = IP;
            this.connect_Port = PORT;
        }



        private Symbol.RFID3.AntennaInfo m_AntennaList = null;

        public Symbol.RFID3.AntennaInfo getInfo()
        {
            return m_AntennaList;
        }


        public void Start_Read_tags(List<ushort> anteny, int? cisloLinky, string volajici)
        {


            try
            {
                AntennaInfo antennaInfo_x = new AntennaInfo();

                antennaInfo_x = SetAnntena(anteny);

                //var xx =  antennaInfo_x.AntennaID;

                TriggerInfo triggerInfo = new TriggerInfo();
                //triggerInfo.
                PostFilter postFilter = new PostFilter();
                //postFilter.


                if (m_IsConnected)
                {
                    Stop_Read_tags(); //--pouzit pri nekorektnim vypnuti cteni
                    _volajici = volajici;
                    _cisloLinky = cisloLinky;
                    item_Tags.Clear();

                    m_ReaderAPI.Actions.Inventory.Perform(null, null, antennaInfo_x);




                    //m_ReaderAPI.Actions.Inventory.Perform(
                    //           m_PostFilterForm.getFilter(),
                    //           m_TriggerForm.getTriggerInfo(),
                    //           m_AntennaInfoForm.getInfo());


                }
            }
            catch (Exception ex)
            {

               // Log.Write(string.Format("CATCH-RFID--Start_Read_tags: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }
        }


        #region RFID sprava MaR doplneno 14.5.2024
        //private void myUpdateRead(Events.ReadEventData eventData)
        //{
        //    int index = 0;
        //    ListViewItem item;

        //    Symbol.RFID3.TagData[] tagData = m_ReaderAPI.Actions.GetReadTags(1000);
        //    if (tagData != null)
        //    {
        //        for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
        //        {
        //            if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
        //                (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
        //                tagData[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))
        //            {
        //                Symbol.RFID3.TagData tag = tagData[nIndex];
        //                string tagID = tag.TagID;
        //                bool isFound = false;

        //                lock (m_TagTable.SyncRoot)
        //                {
        //                    isFound = m_TagTable.ContainsKey(tagID);
        //                    if (!isFound)
        //                    {
        //                        tagID += ((uint)tag.MemoryBank + tag.MemoryBankDataOffset);
        //                        isFound = m_TagTable.ContainsKey(tagID);
        //                    }
        //                }

        //                if (isFound)
        //                {
        //                    uint count = 0;
        //                    item = (ListViewItem)m_TagTable[tagID];
        //                    try
        //                    {
        //                        count = uint.Parse(item.SubItems[2].Text) + tagData[nIndex].TagSeenCount;
        //                        m_TagTotalCount += tagData[nIndex].TagSeenCount;
        //                    }
        //                    catch (FormatException fe)
        //                    {
        //                        functionCallStatusLabel.Text = fe.Message;
        //                        break;
        //                    }
        //                    item.SubItems[1].Text = tag.AntennaID.ToString();
        //                    item.SubItems[2].Text = count.ToString();
        //                    item.SubItems[3].Text = tag.PeakRSSI.ToString();

        //                    string memoryBank = tag.MemoryBank.ToString();
        //                    index = memoryBank.LastIndexOf('_');
        //                    if (index != -1)
        //                    {
        //                        memoryBank = memoryBank.Substring(index + 1);
        //                    }
        //                    if (tag.MemoryBankData.Length > 0 && !memoryBank.Equals(item.SubItems[5].Text))
        //                    {
        //                        item.SubItems[5].Text = tag.MemoryBankData;
        //                        item.SubItems[6].Text = memoryBank;
        //                        item.SubItems[7].Text = tag.MemoryBankDataOffset.ToString();

        //                        lock (m_TagTable.SyncRoot)
        //                        {
        //                            m_TagTable.Remove(tagID);
        //                            m_TagTable.Add(tag.TagID + tag.MemoryBank.ToString()
        //                                + tag.MemoryBankDataOffset.ToString(), item);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    item = new ListViewItem(tag.TagID);
        //                    ListViewItem.ListViewSubItem subItem;

        //                    subItem = new ListViewItem.ListViewSubItem(item, tag.AntennaID.ToString());
        //                    item.SubItems.Add(subItem);

        //                    subItem = new ListViewItem.ListViewSubItem(item, tag.TagSeenCount.ToString());
        //                    m_TagTotalCount += tag.TagSeenCount;
        //                    item.SubItems.Add(subItem);

        //                    subItem = new ListViewItem.ListViewSubItem(item, tag.PeakRSSI.ToString());
        //                    item.SubItems.Add(subItem);
        //                    subItem = new ListViewItem.ListViewSubItem(item, tag.PC.ToString("X"));
        //                    item.SubItems.Add(subItem);

        //                    subItem = new ListViewItem.ListViewSubItem(item, "");
        //                    item.SubItems.Add(subItem);
        //                    subItem = new ListViewItem.ListViewSubItem(item, "");
        //                    item.SubItems.Add(subItem);
        //                    subItem = new ListViewItem.ListViewSubItem(item, "");
        //                    item.SubItems.Add(subItem);

        //                    inventoryList.BeginUpdate();
        //                    inventoryList.Items.Add(item);
        //                    inventoryList.EndUpdate();

        //                    lock (m_TagTable.SyncRoot)
        //                    {
        //                        m_TagTable.Add(tagID, item);
        //                    }
        //                }
        //            }
        //        }
        //        totalTagValueLabel.Text = m_TagTable.Count + "(" + m_TagTotalCount + ")";
        //    }
        //} 
        #endregion



        public void Stop_Read_tags()
        {
            try
            {
                if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                {
                    m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence();
                }
                else
                {
                    //ukonceni cteni z anten
                    m_ReaderAPI.Actions.Inventory.Stop();
                }
            }
            catch (Exception ex)
            {
                if(m_ReaderAPI==null)
                    ExceptionHandler2.Handle(LogLevel.Error,"m_ReaderAPI-objekt neni inicializovan");
                else if (m_ReaderAPI.Actions == null)
                    ExceptionHandler2.Handle(LogLevel.Error, "m_ReaderAPI.Actions-objekt neni inicializovan");
                else if (m_ReaderAPI.Actions.TagAccess == null)
                    ExceptionHandler2.Handle(LogLevel.Error, "m_ReaderAPI.Actions.TagAccess-objekt neni inicializovan");
                else if (m_ReaderAPI.Actions.TagAccess.OperationSequence == null)
                    ExceptionHandler2.Handle(LogLevel.Error, "m_ReaderAPI.Actions.TagAccess.OperationSequence-objekt neni inicializovan");
                else if (m_ReaderAPI.Actions.Inventory == null)
                    ExceptionHandler2.Handle(LogLevel.Error, "m_ReaderAPI.Actions.Inventory.Actions-objekt neni inicializovan");
                // Log.Write(string.Format("CATCH-RFID--Stop_Read_tags: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }

           // _cisloLinky = null; //odkomentovat? 17.1.2022
            //item_Tags.Clear();
            //_cisloLinky = null;
        }

        public bool Write_tags(string EPC_zdroj, string EPC_cil, List<ushort> anteny)
        {
            var m_WriteParams = new TagAccess.WriteAccessParams();
            AntennaInfo antennaInfo_x = new AntennaInfo();
            antennaInfo_x = SetAnntena(anteny);
            int delka_EPC;
            bool correct = false;

            delka_EPC = EPC_cil.Length;


            //------------Anteny parametry, vysilaci vykon-----START----------------//

            //Antennas antena = m_ReaderAPI.Config.Antennas;

            ////var ant_Notify = m_ReaderAPI.Events.NotifyAntennaEvent;


            ////var ant = antena.AvailableAntennas;

            //if (antena != null && antena.Length > 0 )
            //{
            //    var antconf = antena[1].GetConfig();
            //    var antconf_pow = antconf.TransmitPowerIndex; 
            //}


            //------------Anteny parametry, vysilaci vykon-----END----------------//

            //pozor na delku EPC, kdyz je 20 tak ushort lenght = 10
            if (delka_EPC == 24)
            {
                try
                {
                   // ushort length = Convert.ToUInt16(EPC_cil);
                    ushort length = ushort.Parse("12");

                    
                    
                    m_WriteParams.AccessPassword = 0;
                    // m_WriteParams.MemoryBank = (MEMORY_BANK)this.memBank_CB.SelectedIndex;
                    m_WriteParams.MemoryBank = (MEMORY_BANK)MEMORY_BANK.MEMORY_BANK_EPC;
                   // m_WriteParams.MemoryBank = (MEMORY_BANK)1;
                    m_WriteParams.ByteOffset = 4;
                    m_WriteParams.WriteDataLength = length;

                    byte[] writeData = new byte[m_WriteParams.WriteDataLength];
                    for (int index = 0; index < m_WriteParams.WriteDataLength; index += 2)
                    {
                        writeData[index] = byte.Parse(EPC_cil.Substring(index * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                        writeData[index + 1] = byte.Parse(EPC_cil.Substring((index + 1) * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    m_WriteParams.WriteData = writeData;
                    m_SelectedTagID = EPC_zdroj;

                    
                }
                catch (Exception ex)
                {
                    correct = false;
                    //Log.Write(ex);
                    ExceptionHandler2.Handle(ex);
                    // m_AppForm.functionCallStatusLabel.Text = ex.Message;
                    // writeButton.Enabled = true;

                }


                try
                {
                    // m_AccessOpResult.m_OpCode = (ACCESS_OPERATION_CODE)accessEvent.Argument;
                    
                    if (m_AccessOpResult == null)
                    {
                        //Log.Write("Objekt m_AccessOpResult is null");
                        ExceptionHandler2.Handle("Objekt m_AccessOpResult is null", "Log_RFID_Zebra_fx9600_Provider", "txt");
                    }
                        

                    m_AccessOpResult.m_Result = RFIDResults.RFID_API_SUCCESS;


                    if (m_SelectedTagID != String.Empty)
                    {
                        if (m_ReaderAPI == null)
                        {
                            //Log.Write("Objekt m_ReaderAPI is null");
                            ExceptionHandler2.Handle("Objekt m_ReaderAPI is null", "Log_RFID_Zebra_fx9600_Provider", "txt");
                            correct = false;
                        }
                        else if (m_ReaderAPI.Actions == null)
                        {
                            //Log.Write("Objekt m_ReaderAPI.Actions is null");
                            ExceptionHandler2.Handle("Objekt m_ReaderAPI.Actions is null", "Log_RFID_Zebra_fx9600_Provider", "txt");
                            correct = false;
                        }
                        else if (m_ReaderAPI.Actions.TagAccess == null)
                        {
                            //Log.Write("Objekt m_ReaderAPI.Actions.TagAccess is null");
                            ExceptionHandler2.Handle("Objekt m_ReaderAPI.Actions.TagAccess is null", "Log_RFID_Zebra_fx9600_Provider", "txt");
                            correct = false;
                        }
                        else
                        {
                            if (m_WriteParams == null)
                            {
                                //Log.Write("Objekt m_WriteParams is null");
                                ExceptionHandler2.Handle("Objekt m_WriteParams is null", "Log_RFID_Zebra_fx9600_Provider", "txt");
                                correct = false;
                            }
                            else
                            {

                                if (antennaInfo_x == null)
                                {
                                    //Log.Write("Objekt antennaInfo_x is null");
                                    ExceptionHandler2.Handle("Objekt antennaInfo_x is null", "Log_RFID_Zebra_fx9600_Provider", "txt");
                                    //correct = false;
                                }

                                int pocet = 0;
                                do
                                {
                                    pocet++;
                                    try
                                    {
                                            m_ReaderAPI.Actions.TagAccess.WriteWait(
                                    m_SelectedTagID, m_WriteParams, antennaInfo_x);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        //Log.Write(string.Format("WRITE/WAIT_ERROR pruchod: {0}", pocet));
                                        log_hlaska = string.Format("WRITE/WAIT_ERROR pruchod: {0}", pocet);
                                        ExceptionHandler2.Handle(log_hlaska, "Log_RFID_Zebra_fx9600_Provider", "txt");
                                        ExceptionHandler2.Handle(ex);
                                        //Log.Write(ex.Message);
                                        //Log.Write(ex.StackTrace);

                                        //if (ex.InnerException != null)
                                        //{
                                        //    Log.Write(ex.InnerException.Message);
                                        //    Log.Write(ex.InnerException.StackTrace);


                                        //    if (ex.InnerException.InnerException != null)
                                        //    {
                                        //        Log.Write(ex.InnerException.InnerException.Message);
                                        //        Log.Write(ex.InnerException.InnerException.StackTrace);
                                        //    }
                                        //}

                                        //throw ex;
                                    } 
                                } while (pocet < _count_Write_Pruchody);

                                correct = true;
                            }
                        }

                        
                    }
                    else
                    {
                        // functionCallStatusLabel.Text = "Enter Tag-Id";
                        correct = false;
                    }
                   // }
                }
                catch (OperationFailureException ofe)
                {
                    correct = false;
                    m_AccessOpResult.m_Result = ofe.Result;
                    m_AccessOpResult.m_StatusDescription = ofe.StatusDescription;
                    m_AccessOpResult.m_VendorMessage = ofe.VendorMessage;
                    //Log.Write("OperationFailureException");
                    //Log.Write(ofe.Message);
                    //Log.Write(ofe.StackTrace);
                    //Log.Write(ofe.InnerException);
                    ExceptionHandler2.Handle(ofe);
                    //Log.Write("RFID pri zapisu do tagu nastala chyba - (writewait - out of range??)");

                }
                catch (InvalidUsageException ex)
                {
                    correct = false;
                    m_AccessOpResult.m_Result = RFIDResults.RFID_API_PARAM_ERROR;
                    m_AccessOpResult.m_StatusDescription = ex.Info;
                    m_AccessOpResult.m_VendorMessage = ex.VendorMessage;

                    //Log.Write("InvalidUsageException");
                    //Log.Write(ex.Message);
                    //Log.Write(ex.StackTrace);
                    //Log.Write(ex.InnerException);
                    ExceptionHandler2.Handle(ex);

                }
                //accessEvent.Result = m_AccessOpResult;
                //var x = m_AccessOpResult;

            }
            return correct;

            // throw new NotImplementedException();
        }


        public AntennaInfo SetAnntena(List<ushort> anteny)
        {
            AntennaInfo antennaInfo_x = new AntennaInfo();

            if (anteny != null)
            {
                int pocet = 0;
                ushort[] antena_ID_zapis = new ushort[4];
                int pom = 0;

                foreach (ushort item in anteny)
                {
                        antena_ID_zapis[pom] = item;
                        pocet++;
                        pom++;

                }
               

                //--------vytvoreni pole ID anten
                ushort[] antena_ID = new ushort[pocet];
                for (int i = 0; i < pocet; i++)
                {
                    antena_ID[i] = antena_ID_zapis[i];
                    
                }

                antennaInfo_x.AntennaID = antena_ID;



            }
            else
            {
                antennaInfo_x = null; 
            }

            

         return antennaInfo_x;

        }


        #region Eventy metody



        public void Events_StatusNotify(object sender, Events.StatusEventArgs statusEventArgs)
        {
            var data = statusEventArgs.StatusEventData;
        }

        private void Events_ReadNotify(object sender, Events.ReadEventArgs readEventArgs)
        {
            NowRead();
        }

        private void NowRead()
        {

            try
            {
                bool FlagNew = false;
                //if (InvokeRequired)
                //{
                //    BeginInvoke(new Action(() =>
                //    {
                //        NowRead();
                //    }));
                //    return;
                //}


                //if (item_Tags != null)
                //{
                //    item_Tags.Clear();
                //    item_Tags = null;
                //}


                //item_Tags = new List<Item_Tag>();

                Symbol.RFID3.TagData[] tagData = m_ReaderAPI.Actions.GetReadTags(1000);
                if (tagData != null)
                {
                    for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                    {
                        if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
                           (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                            tagData[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))
                        {
                            //inicializace a naplneni promenne tag daty z RFID
                            Symbol.RFID3.TagData tag = tagData[nIndex];



                            //------vyber tagu podle GS1 normy SSCC == 20znaku
                            if (tag.TagID.Length == 24)
                            {
                                string tagID = tag.TagID;
                                string tagAntena = tag.AntennaID.ToString();
                                //List<Classes.Item_Tag> tmpList = new List<Classes.Item_Tag>();

                                Item_Tag newTag = new Item_Tag()
                                {
                                    EPC = tagID,
                                    ID_Antena = tagAntena
                                };

                                #region Varianta 1 

                                //zapis do listu item_Tags pokud uz tag zapsan
                                var y = item_Tags.Where(x => x.EPC == newTag.EPC && x.ID_Antena == newTag.ID_Antena).ToList();

                                if (y.Count == 0)
                                {
                                    item_Tags.Add(newTag);
                                    FlagNew = true;
                                }

                                #endregion


                            }
                        }
                    }

                    if (FlagNew)
                    {

                        if (this.DataReadyZEBRA != null)
                        {
                            DataReadyZEBRA(this, new FASK.SledovaniVyroby.IRFIDProvider.RFID_ZABRA_EventArgs(item_Tags, _cisloLinky, _volajici));
                        } 
                    }
                }


            }
            catch (System.Exception ex)
            {
                var x = ex.Message;
            }
        }


        #endregion

    }
}
