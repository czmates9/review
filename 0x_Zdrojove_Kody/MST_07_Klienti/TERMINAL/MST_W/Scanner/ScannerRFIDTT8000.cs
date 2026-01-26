using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using com.caen.RFIDLibrary;
using tt7000Client;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Collections;
using Fask.MST_W.Forms;
namespace Fask.MST_W.Scanner
{
    class ScannerRFIDTT8000 : ScannerBaseRFID
    {
        public override event ScannerEventRFIDHandler DataReady;
        public override event RFIDTagHandler RFIDTagEvent;

        //promenne
        #region promenne
        private IOExpander m_IO = new IOExpander();  //zapina vypina ctectu
        private CAENRFIDReader m_RFIDReader = new CAENRFIDReader();
        private com.caen.RFIDLibrary.CAENRFIDTag[] m_RFIDTags = new CAENRFIDTag[0];
        //vrati posledni naskenovane tagy
        public com.caen.RFIDLibrary.CAENRFIDTag[] RFIDTags
        {
            get { return m_RFIDTags; }
        }
        private CAENRFIDReceiver m_RFIDReceiver = null;
        private CAENRFIDLogicalSource m_Source0 = null;
        private CAENRFIDTrigger m_CurrentReadTrigger = null;
        private CAENRFIDChannel m_CurrentChannel = null;
        private CAENRFIDTrigger m_CurrentNotifyTrigger = null;
        bool _bcontscan = false; // skenuji ?
        private bool m_b_connected = false; //jsem pripojenej

        private System.Windows.Forms.Timer timer1;//casovac na sken
        #endregion

        public enum PowerSettings : int
        {
            Power10 = 10,
            Power25 = 25,
            Power50 = 50,
            Power100 = 100,
            Power200 = 200,
            Power300 = 300,
            Power400 = 400,
            Power500 = 500
        }

        //protected int power = 200;

        public override int Power
        {
            get
            {
                try
                {
                    if (m_RFIDReader != null)
                        return m_RFIDReader.GetPower();
                    else
                        return power;

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    return 0;
                }
            }
            set
            {
                if (m_RFIDReader != null)
                {
                    try
                    {
                        m_RFIDReader.SetPower(value);
                        power = value;
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex, "RFIDPowerLevel");
                        //MessageBox.Show(ex.Message, "RFIDPowerLevel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }

        public override List<int> PowerLevels
        {
            get { return powerLevels; }
        }
        public ScannerRFIDTT8000()
        {
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer1.Interval = 1700;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            InitializeScanner();
            //this.Enable();
        }

        ~ScannerRFIDTT8000()
        {
            if (enabled)
            {
                this.Disable();
            }
        }

        protected override void InitializeScanner()
        {
            powerLevels.Add(10);
            powerLevels.Add(25);
            powerLevels.Add(50);
            powerLevels.Add(100);
            powerLevels.Add(200);
            powerLevels.Add(300);
            powerLevels.Add(400);
            powerLevels.Add(500);
        }

        private bool enabled = false;
        public override bool Enabled
        {
            get { return enabled; }
        }


        public override void TerminateScanner()
        {
            Disable();
        }

        #region Zapinani vypinani skeneru
        /// <summary>
        /// Zapne RFID ctectu
        /// </summary>
        public override void Enable()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                m_IO.SwitchRFID(false);//vypne
                System.Threading.Thread.Sleep(1000);//wait sec
                m_IO.SwitchRFID(true);//zapne
                System.Threading.Thread.Sleep(1000);
                RFID_Conn();//pripojeni
                enabled = true;//povoleno true
            }
            catch (Exception excp)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(excp);
                throw excp;
                //MessageBox.Show(excp.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
/// <summary>
/// Pripojeni ke ctetce
/// </summary>
        private void RFID_Conn()
        {
            try
            {
                this.m_RFIDReader.ConnectRS232("COM5", 19200); //pripojeni pres seriovej posr

                // CAENRFIDEventMode mode;
                try
                {
                    m_RFIDReader.SetProtocol(CAENRFIDProtocol.CAENRFID_MULTYPROTOCOL); //bude multiprotocol
                    //CAENRFIDEventMode mode = this.m_RFIDReader.GetEventMode();
                }
                catch
                {

                }
                m_RFIDReader.SetPower(MST_Global.RFIDPowerLevel);
                //int power = m_RFIDReader.GetPower();

                // CAENRFIDProtocol protocol = this.m_RFIDReader.GetProtocol();

                CAENRFIDLogicalSource[] logical_sources = this.m_RFIDReader.GetSources();
                if (logical_sources.Length == 0)
                    throw new Exception("No logical sources");
                this.m_Source0 = logical_sources[0];
            }
            catch (Exception excp)
            {
                Logging.Log.Write(excp);
                throw excp;
                //MessageBox.Show(excp.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //this.Invoke(new EventHandler(m_disconnect_button_Click));
            }
            //MessageBox.Show("Connected", "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            this.m_b_connected = true;
            //this.StartScan();
        }

        /// <summary>
        /// Vypne ctecku
        /// </summary>
        public override void Disable()
        {
            try
            {
                StopScan();
                this.m_RFIDReader.Disconnect();
                this.m_b_connected = false;
                m_IO.SwitchRFID(false); ;
                enabled = false;
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                StopScan();
            }
        } 
        #endregion

        /// <summary>
        /// Zapne kontinualni skenovani
        /// </summary>
        public override void StartScan()
        {
            //sken aktivni a casovac taky
            _bcontscan = true;
            timer1.Enabled = true;
        }
        /// <summary>
        /// Zastavi skenovani
        /// </summary>
        public override void StopScan()
        {
            _bcontscan = false;
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //m_inventory_button_Click(sender, e);
            try
            {
                prectiHodnoty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Precte hodnoty tagu jednou pokud neni 
        /// aktivni casovac
        /// </summary>
        public void PrectiTagyJednou()
        {
            if (!timer1.Enabled)
            {
                prectiHodnoty();
            }
        }

        bool _reader_busy = false; //pomoc prom jestli prave ctu
        /// <summary>
        /// Cteni hodnot ze skeneru- kazdou sekundu jsou nacteny data
        /// </summary>
        private void prectiHodnoty()
        {
            bool text = MST_Global.RFIDUkladatNacitatText; 
            if (!_reader_busy)
                try
                {
                    _reader_busy = true;
                    //this.m_inventory_listBox.Items.Clear();
                    //CAENRFIDLogicalSource[] logical_sources = this.m_RFIDReader.GetSources();
                    //if (logical_sources.Length == 0)
                    //    throw new Exception("No logical sources");
                    //this.m_Source0 = logical_sources[0];
                    // this.m_RFIDTags= logical_sources[0].Inventory();
                    this.m_RFIDTags = m_Source0.InventoryTag();

                    if (this.m_RFIDTags != null) //neco jsem precetl
                    {//vytovrim si objekto pro kody
                        List<RFIDBarcodeData> barCodes = new List<RFIDBarcodeData>(); //sem se budou ukladat kody
                        for (int i = 0; i < this.m_RFIDTags.Length; i++) //Udelam si objekty
                        {
                            if (text)
                            {
                                barCodes.Add(new RFIDBarcodeData(RFID.Routines.ByteasciiArrayToString(this.m_RFIDTags[i].GetId()), Convert.ToUInt32(this.m_RFIDTags[i].GetType()), string.Empty, (uint)this.m_RFIDTags[i].GetLength()));
                            }
                            else
                            {
                                barCodes.Add(new RFIDBarcodeData(RFID.Routines.ByteHexArrayToStringHex(this.m_RFIDTags[i].GetId()), Convert.ToUInt32(this.m_RFIDTags[i].GetType()), string.Empty, (uint)this.m_RFIDTags[i].GetLength()));
                            }
                        }
                        if (this.m_RFIDTags.Length != 0)
                        {
                            if (this.DataReady != null)
                            {
                                this.DataReady(
                                    this,
                                    new ScannerRFIDEventArgs(barCodes));
                            }
                        }
                    }
                }
                catch (Exception excp)
                {
                    //StopScan();
                    //MessageBox.Show(excp.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    try
                    {
                        this.StopScan();
                        this.Disable();
                        this.Enable();
                        this.StartScan();
                    }
                    catch //(Exception ex)
                    {
                        MessageBoxBig.Show(excp.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    }
                }
                finally
                {
                    _reader_busy = false;
                }
        }

        #region Nastaveni ctecky
        /// <summary>
        /// Vrati seznam zdroju
        /// Poznamka: funguje pouze droj jedna - pokud bude asi vice
        /// zarizeni pripojeno pujdou  dalsi
        /// </summary>
        /// <returns> seznam zdroju</returns>
        public override List<String> GetSources()
        {
            List<String> listOfNamesOfSources = new List<string>();
            CAENRFIDLogicalSource[] logical_sources;
            try
            {
                logical_sources = this.m_RFIDReader.GetSources();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return listOfNamesOfSources;
            }
            for (int i = 0; i < logical_sources.Length; i++)
            {
                listOfNamesOfSources.Add(logical_sources[i].GetName());
            }
            return listOfNamesOfSources;
        }

        /// <summary>
        /// Nastatavim novy zdroj
        /// </summary>
        /// <param name="sourceName"></param>
        public override void SetSource(String sourceName)
        {
            CAENRFIDLogicalSource[] logical_sources;
            try
            {
                logical_sources = this.m_RFIDReader.GetSources();
                CAENRFIDLogicalSource source = logical_sources.Single(p => p.GetName() == sourceName);
                this.m_Source0 = source; //nastavim
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

        }

        #region Zmena power za behu-neni pouzita
        ///// <summary>
        ///// Nastaveni power pro ctectu- hodnoty z dema
        ///// </summary>
        ///// <param name="power">energie pro čtečku</param>
        //public override void SetPower(int power)
        //{
        //    if (!b_rfid_conn_starting)
        //    {
        //        try
        //        {
        //            int m_pwr = (int)power;
        //            m_RFIDReader.SetPower(m_pwr);
        //            m_RFIDReader.Disconnect();
        //            RFID_Conn();
        //        }
        //        catch (Exception exp)
        //        {
        //            MessageBox.Show(exp.Message);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Vrati hodnoty energie ctecky
        ///// </summary>
        ///// <returns>hodnota energie</returns>
        //public override int GetPower()
        //{
        //    if (!b_rfid_conn_starting)
        //    {
        //        try
        //        {
        //            return m_RFIDReader.GetPower();
        //        }
        //        catch (Exception exp)
        //        {
        //            MessageBox.Show(exp.Message);
        //            return 0;
        //        }

        //    }
        //    return 0;
        //} 
        #endregion
        
        #endregion

        /* Poznámky ke cteni a zapisu!!
         * Cteni pro tag G2 TID je adresa 0 a legth 4  vraci to pak E2-00-60-03
         * Cteni pro tag G2 EPC je od adresa 4 a lengh 12 stejna jako tagy co tam jsou -- bude pro zapis
         * Cteni tag G2 EPC muzu asi vetsinu rozsahu
        */
        public enum TypyTagu : short
        {
            G2RESERVER = 0,
            G2EPC = 1,
            G2TID = 2,
            G2USER = 3
        }

        #region Cteni tagu a zapis tagu
        /// <summary>
        /// Cteni tagu potrebuje tag, adresu a delku co ma cist
        /// </summary>
        /// <param name="adresa">Adresa od ktere cist</param>
        /// <param name="delka">Tag</param>
        public string CtejiTagu(CAENRFIDTag tag)
        {
            byte[] read = null;
            string nactenyTag = string.Empty;//prazdny
            int add = 4; //adresa bude 4
            int len = 12; //delka nactenych bude 12
            bool str = MST_Global.RFIDUkladatNacitatText;
            try
            {

                read = tag.GetSource().ReadTagData_EPC_C1G2(tag, (short) TypyTagu.G2EPC, (short)add, (short)len);
                if (str)
                {
                    nactenyTag = RFID.Routines.ByteasciiArrayToString(read); //prevedu tag
                }
                else
                {
                    nactenyTag = RFID.Routines.ByteHexArrayToStringHex(read);

                }
                  return nactenyTag;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return nactenyTag;
            }
            // RFIDBarcodeData(System.BitConverter.ToString(this.m_RFIDTags[i].GetId()),Convert.ToUInt32(this.m_RFIDTags[i].GetType()),string.Empty,(uint)this.m_RFIDTags[i].GetLength()));
        }

        /// <summary>
        /// Zapis tagu  potrebu tag na ktery se bude zapisovat a strProzapis
        /// Podle toho co se ma zapisovat bude bus zapisovat chary v Asci ! max 12
        /// nebo hex hodnoty a to bude 24 cisel - jedna hodnota je treba FF nebo 12 atd. 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="strProZapis"></param>
        public void ZapisTagu(CAENRFIDTag tag, string strProZapis)
        {
            if (strProZapis.Length == 0)
            {
                MessageBox.Show("Zprava neobsahuje znaky", "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            int add = 4; //adresa bude 4
            int len = 12; //delka nactenych bude 12
            bool text = MST_Global.RFIDUkladatNacitatText;
            try
            {
                if (text)
                {
                    if (strProZapis.Length > len)
                    {
                        MessageBox.Show("Zprava ma vice nez 12 znaku!! Zadejte menší", "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    byte[] pomocpp = RFID.Routines.StringToByteAscii(strProZapis);

                    tag.GetSource().WriteTagData_EPC_C1G2(tag, (short)TypyTagu.G2EPC, (short)add, (short)pomocpp.Length, pomocpp);
                }
                else
                {//zde bude pro hexa
                    if (strProZapis.Length > 24)
                    {
                        MessageBox.Show("Zprava ma vice nez 24 znaku!! Zadejte menší", "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    byte[] pole = RFID.Routines.StringHexValueToByte(strProZapis);
                    //odeslu
                    tag.GetSource().WriteTagData_EPC_C1G2(tag, (short)TypyTagu.G2EPC, (short)add, (short)pole.Length, pole);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RFID scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

        } 
        #endregion


        public override void Configure()
        {
            MessageBoxBig.Show("Not implemented", this.GetType().ToString(), MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        public override List<string> RemovedCodes
        {
            get;
            set;
        }

    }
}
