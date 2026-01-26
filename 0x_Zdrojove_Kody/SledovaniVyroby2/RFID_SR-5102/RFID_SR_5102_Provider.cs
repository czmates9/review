using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReaderB;
using System.Threading;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.IRFIDProvider;
using Fask.Logging;

namespace FASK.RFID_SR_5102
{
    public enum ComPorty 
    { 
        COM1 = 1,
        COM2 = 2,
        COM3 = 3,
        COM4 = 4,
        COM5 = 5,
        COM6 = 6,
        COM7 = 7,
        COM8 = 8,
        COM9 = 9,
        COM10 = 10,
        COM11 = 11,
        COM12 = 12,
    }

    public enum Baudrate
    {
        _9600bps = 0,
        _19200bps = 1,
        _38400bps = 2,
        _56000bps = 4,
        _57600bps = 5,
        _115200bps = 6
    }


    public class RFID_SR_5102_Provider : FASK.SledovaniVyroby.IRFIDProvider.IRFIDProvider
    {

        /// <summary>
        /// Com port
        /// </summary>
        private ComPorty _port = ComPorty.COM1;

        /// <summary>
        /// Adresa zařizeni
        /// </summary>
        private byte _fComAdr = 0xFF; //  broadcasting address 

        /// <summary>
        /// Rychlost komunikace
        /// </summary>
        private Baudrate _fBaud = Baudrate._57600bps;

        /// <summary>
        /// Index vytvoreneho portu
        /// </summary>
        private int _frmcomportindex;

        /// <summary>
        /// Index otevriteho portu portu
        /// </summary>
        private int? _fOpenComIndex;

        /// <summary>
        /// nese v sebe informaci ze ci je InventoryMod aktivny
        /// </summary>
        public bool fIsInventoryScan;

        private bool isopen = false;

        //public event ScannerEventRFIDHandler DataReady;
        //public event RFIDTagHandler RFIDTagEvent;


        private System.Threading.Timer timer;

        private int _count_Write_Pruchody;
        public int Count_Write_Pruchody
        {
            get { return _count_Write_Pruchody; }
            set { _count_Write_Pruchody = value; }
        }

        #region TID

        public bool EnableTID = false;
            public byte AdrTID = 0;
            public byte LenTID = 0;
            public byte TIDFlag = 0;

#endregion


        /// <summary>
        /// C'tor
        /// </summary>
            public RFID_SR_5102_Provider()
            {
                try
                {
                    string CmdComAddr = LogConfig.config.RFID[0].Address;
                    FASK.RFID_SR_5102.ComPorty port = (FASK.RFID_SR_5102.ComPorty)Enum.Parse(typeof(FASK.RFID_SR_5102.ComPorty), LogConfig.config.RFID[0].ComPort);
                    FASK.RFID_SR_5102.Baudrate baud = (FASK.RFID_SR_5102.Baudrate)Enum.Parse(typeof(FASK.RFID_SR_5102.Baudrate), LogConfig.config.RFID[0].Baudrate);


                    this._port = port;

                    if (string.IsNullOrEmpty(CmdComAddr))
                        CmdComAddr = "FF"; //broadcasting address 

                    this._fComAdr = Convert.ToByte(CmdComAddr, 16); // $FF;
                    this._fBaud = baud;

                    timer = new Timer(new TimerCallback(TimerTick), null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                }
                catch (Exception ex)
                {
                    //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
            }


        public string OpenPort(bool auto)
        {
            try
            {
                if (auto)
                {
                    int port = 0;

                    int openresult = StaticClassReaderB.AutoOpenComPort(ref port, ref this._fComAdr, Convert.ToByte((int)this._fBaud), ref this._frmcomportindex);
                    this._fOpenComIndex = this._frmcomportindex;

                    return ReturnValueDefition(openresult);

                }
                else
                {

                    for (int i = 6; i >= 0; i++)
                    {
                        int local_port = (int)this._port;
                        byte local_comadr = this._fComAdr;
                        byte local_baud = Convert.ToByte((int)this._fBaud);

                        int openresult = StaticClassReaderB.OpenComPort(local_port, ref local_comadr, local_baud, ref this._frmcomportindex);
                        this._fOpenComIndex = this._frmcomportindex;

                        return ReturnValueDefition(openresult);

                    }
                }

                //timer = new Timer(new TimerCallback(ProcessTags), null, Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                return "Error from Exception: " + ex.Message;
                //throw;
            }

            //sem by to nemnelo nikdy dojit
            return "";

        }

        public string ClosePort()
        {
            try
            {
                if (this._fOpenComIndex != null)
                {
                    int fCmdRet = StaticClassReaderB.CloseSpecComPort((int)this._port);
                    this._fOpenComIndex = null;
                    return ReturnValueDefition(fCmdRet);
                }
                else
                    return "Port is not open.";

                //if (timer != null)
                //{
                //    timer.Dispose();
                //    timer = null;
                //}
            }
            catch (Exception ex)
            {
                return "Error from Exception: " + ex.Message;
            }
        }

        //public void StartStopRead() 
        //{

        //    if (fIsInventoryScan)
        //    {
        //        timer.Change(Timeout.Infinite, Timeout.Infinite);
        //    }
        //    else 
        //    {
        //        timer.Change(1000, 1000);
        //    }
        
        //}


        //private void ProcessTags(object state)
        //{
        //    try
        //    {
        //        List<string> barCodesTmp;
        //        lock (barCodes)
        //        {
        //            barCodesTmp = barCodes;
        //            barCodes = new List<string>();
        //        }

        //        if (barCodesTmp.Count > 0)
        //        {
        //            // Vyvolani udalosti nacteni kodu...
        //            if (this.DataReady != null)
        //            {
        //                this.DataReady( this, new ScannerRFIDEventArgs(barCodesTmp));
        //            }
        //        }

        //        List<string> rfidTagDatasTmp;
        //        lock (rfidTagDatas)
        //        {
        //            rfidTagDatasTmp = rfidTagDatas;
        //            rfidTagDatas = new List<string>();
        //        }

        //        if (rfidTagDatasTmp.Count > 0)
        //        {
        //            // Vyvolani udalosti nacteni kodu...
        //            if (this.RFIDTagEvent != null)
        //            {
        //                this.RFIDTagEvent(this, new RFIDTagDataEventArgs(rfidTagDatasTmp));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Write(ex);
        //    }
        //}

        public void TimerTick(object state)
        {
            var list = Inventory();
            OnData(list);
        }

        private void OnData(List<string> data)
        {
            if (this.DataReady != null)
            {
                DataReady(this, new FASK.SledovaniVyroby.IRFIDProvider.RFIDEventArgs(data));
            }
        }

        public List<string> Inventory()
        {
            //int i;
            int CardNum = 0;
            int Totallen = 0;
            int EPClen;
            int pozice;
            byte[] EPC = new byte[5000];
            int CardIndex;
            string temps;
            string sEPC;
            this.fIsInventoryScan = true;
            //EPC_DataSet.EPC_DataTableDataTable dt_epc = new EPC_DataSet.EPC_DataTableDataTable();
            List<string> list = new List<string>();

            int fCmdRet = StaticClassReaderB.Inventory_G2(ref this._fComAdr , AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum,  this._frmcomportindex);
            
            
            if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))
            {
                byte[] daw = new byte[Totallen];
                Array.Copy(EPC, daw, Totallen);
                temps = ByteArrayToHexString(daw);     
                pozice = 0;

                  
                if (CardNum == 0)
                {
                    this.fIsInventoryScan = false;
                    return null;
                }

                //pokud je nacteno vic EPC
                
                for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                {
                    //EPC_DataSet.EPC_DataTableRow row = dt_epc.NewEPC_DataTableRow();

                    EPClen = daw[pozice];
                    sEPC = temps.Substring(pozice * 2 + 2, EPClen * 2);
                    pozice = pozice + EPClen + 1;
                    if (sEPC.Length != EPClen * 2)
                        break;

                    if(sEPC != null)
                        list.Add(sEPC);
                }
            }

            this.fIsInventoryScan = false;

            return list;
        }

        /// <summary>
        /// konvertovani bytearray na HEX(string)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ByteArrayToHexString(byte[] data)
        {
             StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }

        #region Dekodovani return kodu na txt 
 
        private string ReturnValueDefition(int value) 
        {

            switch (value)
            {
                case 0x00:
                    return "OK";
                case 0x01:
                    return "Return before Inventory finished";
                case 0x02:
                    return "the Inventory-scan-time overflow";
                case 0x03:
                    return "More Data";
                case 0x04:
                    return "Reader module MCU is Full";
                case 0x05:
                    return "Access password error";
                case 0x09:
                    return "Destroy password error";
                case 0x0a:
                    return "Destroy password error cann’t be Zero";
                case 0x0b:
                    return "Tag Not Support the command";
                case 0x0c:
                    return "Use the commmand,Access Password Cann’t be Zero";
                case 0x0d:
                    return "Tag is protected,cannot set it again";
                case 0x0e:
                    return "Tag is unprotected,no need to reset it";
                case 0x10:
                    return "There is some locked bytes,write fail";
                case 0x11:
                    return "can not lock it";
                case 0x12:
                    return "is locked,cannot lock it again";
                case 0x13:
                    return "Save Fail,Can Use Before Power";
                case 0x14:
                    return "Cannot adjust";
                case 0x15:
                    return "Return before Inventory finished";
                case 0x16:
                    return "Inventory-Scan-Time overflow ";
                case 0x17:
                    return "More Data";
                case 0x18:
                    return "Reader module MCU is full";
                case 0x19:
                    return "Not Support Command Or AccessPassword Cannot be Zero";
                case 0xf9:
                    return "Command execute error";
                case 0xfa:
                    return "Get Tag,Poor Communication,Inoperable";
                case 0xfb:
                    return "No Tag Operable";
                case 0xfc:
                    return "Tag Return ErrorCode";
                case 0xfd:
                    return "Command length wrong";
                case 0xfe:
                    return "Illegal command";
                case 0xff:
                    return "Parameter Error";
                case 0x30:
                    return "Communication error";
                case 0x31:
                    return "CRC checksummat error";
                case 0x32:
                    return "Return data length error";
                case 0x33:
                    return "Communication busy";
                case 0x34:
                    return "Busy,command is being executed";
                case 0x35:
                    return "ComPort Opened";
                case 0x36:
                    return "ComPort Closed";
                case 0x37:
                    return "Invalid Handle";
                case 0x38:
                    return "Invalid Port ";
                case 0xee:
                    return "Return command error";
                default:
                    return "Default Message...";
            }


        }

        private string ErrorCodeDefinition(int value)
        {
            switch (value)
            {
                case 0x00:
                    return "Other Error";
                case 0x03:
                    return "Memory out or pc not support";
                case 0x04:
                    return "Memory Locked and unwritable";
                case 0x0b:
                    return "No Power,memory write operation cannot be executed ";
                case 0x0f:
                    return "Not Special Error,tag not support special errorcode";
                default:
                    return "Default error";
            }
        }

        #endregion


        #region IRFIDProvider Members

        public void Start()
        {

            try
            {
                string status = OpenPort(false);
                if (status != "OK")
                {
                    //FASK.SledovaniVyroby.ErrorLog.Log.WriteException("RFID Start: " + status);
                    string log_hlaska = string.Format("RFID Start: " + status);
                    ExceptionHandler2.Handle(log_hlaska, "Log_RFID_SR_5102_Provider", "txt");

                    timer.Dispose();
                    isopen = false;
                }
                else
                {
                    timer.Change(0, 100);
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                //Timer_Test_.Enabled = false;
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                isopen = false;
                //FlexibleMessageBox.Show(ex.Message, "EXCEPTION");
            }
        }

        public void Stop()
        {
            try
            {
                string status = ClosePort();

                if (status != "OK" && status != "Port is not open.")
                {
                    timer.Dispose();
                    isopen = false;
                    //FASK.SledovaniVyroby.ErrorLog.Log.WriteException("RFID Stop: " + status);
                    string log_hlaska = string.Format("RFID Stop: " + status);
                    ExceptionHandler2.Handle(log_hlaska, "Log_RFID_SR_5102_Provider", "txt");
                    //buttonRFID.BackColor = Color.Red;
                    //FlexibleMessageBox.Show("Error:" + status, this.Text);
                }

                timer.Dispose();
                isopen = false;
                //buttonRFID.Text = "Start Read";
                //buttonRFID.BackColor = Color.Red;

            }
            catch (Exception ex)
            {
                timer.Dispose();
                isopen = false;
                //buttonRFID.Text = "Start Read";
                //buttonRFID.BackColor = Color.Red;
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                //FlexibleMessageBox.Show(ex.Message, "EXCEPTION");
            }
        }

        #region Puvodnz kod z RFID_Data

        //private void OpenRFID()
        //{
        //    string adresa = LogConfig.config.RFID_SR_5102[0].Address;
        //    RFID_SR_5102.ComPorty port = (RFID_SR_5102.ComPorty)Enum.Parse(typeof(RFID_SR_5102.ComPorty), LogConfig.config.RFID_SR_5102[0].ComPort);
        //    RFID_SR_5102.Baudrate baud = (RFID_SR_5102.Baudrate)Enum.Parse(typeof(RFID_SR_5102.Baudrate), LogConfig.config.RFID_SR_5102[0].Baudrate);

        //    RFID.Start();

        //    if (rfid == null)
        //        rfid = new RFID_SR_5102.RFID_SR_5102(port, adresa, baud);

        //    try
        //    {
        //        string status = rfid.OpenPort(false);
        //        if (status != "OK")
        //        {
        //            buttonRFID.Text = "Start Read";
        //            buttonRFID.BackColor = Color.Red;
        //            Timer_Test_.Enabled = false;
        //            tssl_DI.Text = "Error: " + status;
        //            //FlexibleMessageBox.Show("Error: " + status, this.Text);
        //        }
        //        else
        //        {
        //            Timer_Test_.Enabled = true;
        //            buttonRFID.Text = "Stop Read";
        //            buttonRFID.BackColor = Color.Green;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Timer_Test_.Enabled = false;
        //        buttonRFID.Text = "Start Read";
        //        buttonRFID.BackColor = Color.Red;
        //        ErrorLog.Log.WriteException(ex);
        //        FlexibleMessageBox.Show(ex.Message, "EXCEPTION");
        //    }
        //}

        //private void CloseRFID()
        //{
        //    try
        //    {
        //        string status = rfid.ClosePort();

        //        if (status != "OK" && status != "Port is not open.")
        //        {
        //            Timer_Test_.Enabled = false;
        //            buttonRFID.Text = "Start Read";
        //            buttonRFID.BackColor = Color.Red;
        //            FlexibleMessageBox.Show("Error:" + status, this.Text);
        //        }

        //        Timer_Test_.Enabled = false;
        //        buttonRFID.Text = "Start Read";
        //        buttonRFID.BackColor = Color.Red;

        //    }
        //    catch (Exception ex)
        //    {
        //        Timer_Test_.Enabled = false;
        //        buttonRFID.Text = "Start Read";
        //        buttonRFID.BackColor = Color.Red;
        //        ErrorLog.Log.WriteException(ex);
        //        FlexibleMessageBox.Show(ex.Message, "EXCEPTION");
        //    }
        //}

        #endregion


        public bool isOpen() 
        {

            return this.isopen;
        }

        public void init(string IP, uint PORT)
        {
            //ujthrow new NotImplementedException();
        }

   


        public void Start_Read_tags(List<ushort> anteny, int? cisloLinky, string volajici)
        {
            //throw new NotImplementedException();
        }

        public void Stop_Read_tags()
        {
            //throw new NotImplementedException();
        }

        public bool Write_tags(string EPC_zdroj, string EPC_cil, List<ushort> anteny)
        {
            // throw new NotImplementedException();
            return true;
        }

        public string Info_DLL()
        {
            throw new NotImplementedException();
        }

        public bool PerformTagLocationing(string tagId, int cisloAnteny, int dobaLokalizace)
        {
            throw new NotImplementedException();
        }

        public event FASK.SledovaniVyroby.IRFIDProvider.RFIDHandler DataReady;
        //{
        //    add { throw new NotImplementedException(); }
        //    remove { throw new NotImplementedException(); }
        //}

        #endregion

        public event RFID_ZEBRA_Handler DataReadyZEBRA;
    }
}
