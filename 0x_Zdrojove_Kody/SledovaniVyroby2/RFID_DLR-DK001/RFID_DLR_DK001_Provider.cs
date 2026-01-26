using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.datalogic.DLRFIDLibrary;
using System.Threading;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.IRFIDProvider;
using Fask.Logging;

namespace FASK.RFID_DLR_DK001
{
    public class RFID_DLR_DK001_Provider : FASK.SledovaniVyroby.IRFIDProvider.IRFIDProvider
    {

        private bool isopen = false;
        private DLRFIDReader MyReader;

        private string Port = string.Empty;

        List<string> list = new List<string>();


        private int _count_Write_Pruchody;
        public int Count_Write_Pruchody 
        { 
            get { return _count_Write_Pruchody; }  
            set { _count_Write_Pruchody = value; }
        }

        public RFID_DLR_DK001_Provider() 
        {
            MyReader = new DLRFIDReader();

            Port = LogConfig.config.RFID[0].ComPort;

            isopen = false;
        }



        #region Nacteni Dat 

        void MyReader_EventHandler(object Sender, DLRFIDEventArgs Event)
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



        private void OnData(List<string> data)
        {
            if (this.DataReady != null)
            {
                DataReady(this, new FASK.SledovaniVyroby.IRFIDProvider.RFIDEventArgs(data));
            }
        }
        #endregion

        #region IRFIDProvider Members



        public void Start()
        {
            try
            {
                DLRFIDLogicalSource LS0;
                byte[] Mask = new byte[4];
                MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);
                MyReader.DLRFIDEvent += new DLRFIDEventHandler(MyReader_EventHandler);
                MyReader.Connect(DLRFIDPort.DLRFID_RS232, Port);
                LS0 = MyReader.GetSource("Source_0");
                LS0.SetReadCycle(0);
                LS0.EventInventoryTag(Mask, 0x0, 0x0, 0x06);

                this.isopen = true;
            }
            catch (Exception ex)
            {
                MyReader.Disconnect();
                //MessageBox.Show(ex.Message);
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
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
                MyReader.InventoryAbort();
                MyReader.Disconnect();
                this.isopen = false;
            }
            catch (Exception ex)
            {
                MyReader.Disconnect();
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                this.isopen = false;
            }
            finally
            {
                MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);
            }
        }

        public bool isOpen()
        {

            return this.isopen;
        }

        public void init(string IP, uint PORT)
        {
            //throw new NotImplementedException();
        }

        public void Start_Read_tags(List<ushort> anteny, int? cisloLinky, string volajici)
        {
           // throw new NotImplementedException();
        }

        public void Stop_Read_tags()
        {
           // throw new NotImplementedException();
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
        public event RFID_ZEBRA_Handler DataReadyZEBRA;

        #endregion
    }
}
