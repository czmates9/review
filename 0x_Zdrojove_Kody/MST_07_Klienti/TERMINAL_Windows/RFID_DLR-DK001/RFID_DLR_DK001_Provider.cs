using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.datalogic.DLRFIDLibrary;
using System.Threading;
using FASK.MST_WINDOWS.Logging;


namespace FASK.RFID_DLR_DK001
{
    public class RFID_DLR_DK001_Provider : IRFIDProvider.IRFIDProvider
    {

        private bool isopen = false;
        private DLRFIDReader MyReader;

        private string Port = string.Empty;

        List<string> list = new List<string>();


        public RFID_DLR_DK001_Provider() 
        {
            //MyReader = new DLRFIDReader();

            Port = FASK.MST_WINDOWS.Main.Configuration.Config.RFID_ComPort;

            FASK.MST_WINDOWS.ErrorLog.Log.WriteException("C'tor RFID_DLR_DK001_Provider");

            isopen = false;
        }



        #region Nacteni Dat 

        void MyReader_EventHandler(object Sender, DLRFIDEventArgs Event)
        {
            
            try
            {
                list.Clear();

                foreach (DLRFIDNotify n in Event.Data)
                {
                    byte[] tmp = n.getTagID();
                    //textBox1.Text = BitConverter.ToString(tmp) + Environment.NewLine;

                    list.Add(BitConverter.ToString(tmp).Replace("-", ""));
                    //ntag++;
                }

                OnData(list);
            }
            catch (Exception ex)
            {
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException(ex);
            }
        } 



        private void OnData(List<string> data)
        {
            if (this.DataReady != null)
            {
                DataReady(this, new IRFIDProvider.RFIDEventArgs(data));
            }
        }
        #endregion

        #region IRFIDProvider Members



        public void Start()
        {
            try
            {

                GC.Collect();
                if (MyReader != null)
                {
                    Stop();
                }
                MyReader = new DLRFIDReader();
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException("Start RFID_DLR_DK001_Provider");
                DLRFIDLogicalSource LS0;
                byte[] Mask = new byte[4];
                MyReader.Connect(DLRFIDPort.DLRFID_RS232, Port);

                MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);
                MyReader.DLRFIDEvent += new DLRFIDEventHandler(MyReader_EventHandler);

                LS0 = MyReader.GetSource("Source_0");
                LS0.SetReadCycle(0);
                LS0.EventInventoryTag(
                    Mask
                    , 0x0
                    , 0x0
                    , (short)(DLRFIDLogicalSource.InventoryFlag.FRAMED | DLRFIDLogicalSource.InventoryFlag.CONTINUOS)
                    //, (short)DLRFIDLogicalSource.InventoryFlag.CONTINUOS
                    );

                this.isopen = true;
            }
            catch (Exception ex)
            {
                MyReader.Disconnect();
                //MessageBox.Show(ex.Message);
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException(ex);
                this.isopen = false;

            }
            finally
            {
            }
        }

        public void Stop()
        {
            try
            {
                if (MyReader != null)
                {
                    
                    FASK.MST_WINDOWS.ErrorLog.Log.WriteException("InventoryAbort RFID_DLR_DK001_Provider");
                    MyReader.InventoryAbort();
                    MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);

                    FASK.MST_WINDOWS.ErrorLog.Log.WriteException("MyReader.Disconnect RFID_DLR_DK001_Provider");
                    MyReader.Disconnect();
                    FASK.MST_WINDOWS.ErrorLog.Log.WriteException("End RFID_DLR_DK001_Provider"); 
                }
                MyReader = null;
                this.isopen = false;
            }
            catch (Exception ex)
            {
                if (MyReader != null)
                {
                    MyReader.Disconnect();
                    MyReader = null;
                }
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException(ex);
                this.isopen = false;
            }
            finally
            {
                if (MyReader != null)
                {
                    MyReader = null;
                }
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException("Finnaly RFID_DLR_DK001_Provider");
                //MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);
            }
        }

        public bool isOpen()
        {

            return this.isopen;
        }

        public event IRFIDProvider.RFIDHandler DataReady;

        #endregion


    }
}
