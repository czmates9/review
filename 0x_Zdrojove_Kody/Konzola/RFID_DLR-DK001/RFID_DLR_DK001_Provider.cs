using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.datalogic.DLRFIDLibrary;
using System.Threading;




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

            Konzola.Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

            MyReader = new DLRFIDReader();


            Port = Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].ComPort;


            //Fask.Logging.ExceptionHandler2.Handle("C'tor RFID_DLR_DK001_Provider");
            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "C'tor RFID_DLR_DK001_Provider");

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
                DataReady(this, new IRFIDProvider.RFIDEventArgs(data));
            }
        }
        #endregion

        #region IRFIDProvider Members



        public void Start()
        {
            try
            {
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Start RFID_DLR_DK001_Provider");
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
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
                MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "InventoryAbort RFID_DLR_DK001_Provider");
                MyReader.InventoryAbort();

                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "MyReader.Disconnect RFID_DLR_DK001_Provider");
                MyReader.Disconnect();
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "End RFID_DLR_DK001_Provider");
                this.isopen = false;
            }
            catch (Exception ex)
            {
                MyReader.Disconnect();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                this.isopen = false;
            }
            finally
            {
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Finnaly RFID_DLR_DK001_Provider");
                MyReader.DLRFIDEvent -= new DLRFIDEventHandler(MyReader_EventHandler);
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
