using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.BarCodePars;
using FASK.SledovaniVyroby.ModuleIfc;
using Database;
using System.Data.SqlClient;
using Fask.Logging;
using ICommDatabase;

// module name : Module.DCDIdeal.InkJet.frmMain

namespace Module.DCDIdeal.InkJet
{
    public partial class frmMain : Form, IModuleConnector
    {
        public string log_hlaska = string.Empty;

        /// <summary>
        /// databaze pro ulozeni dat ... pouzito pro definici max delky ... pole
        /// </summary>
        private readonly ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();

        //Format barkodu a parsery
        private const string defaultBarcodeFormat = "nnnnnnnnaaaabbbbtttkkk_zzzzzzzzppppoo";

        //Zakazani nacteni caroveho kodu ze scanneru, pokud je zobrazeno okno pro potvrzeni
        private bool datareadEnable = true;

        private NotifyIcon notifyIconState;

        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        public frmMain()
        {
            InitializeComponent();

            try
            {
                InitializePorts();
            }
            catch { }

            InitBarcode();
            InitTextBoxes();
        }

        private void InitBarcode()
        {
            if (Config.config.Separators.Rows.Count == 0)
            {
                Config.config.Separators.AddSeparatorsRow("\x0004");
                Config.Save();
            }

            textBoxBarcodeOutput.Text = Config.config.Separators[0].DataOutEnd;
        }

        private void nastaveniToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void buttonStartStopIN_Click(object sender, EventArgs e)
        {
            SerialPortINChangeState();
        }

        private void SerialPortINChangeState()
        {
            try
            {
                if (serialPortIN.IsOpen)
                    serialPortIN.Close();
                else
                {
                    serialPortIN.Encoding = Encoding.GetEncoding(1250); // Encoding.ASCII;
                    serialPortIN.BaudRate = int.Parse(Config.config.Communication[0].SP_IN_BaudRate);
                    serialPortIN.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), Config.config.Communication[0].SP_IN_Parity);
                    serialPortIN.PortName = Config.config.Communication[0].SP_IN_PortName;
                    serialPortIN.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), Config.config.Communication[0].SP_IN_StopBits);
                    serialPortIN.DataBits = int.Parse(Config.config.Communication[0].SP_IN_DataBits);

                    serialPortIN.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }

            if (serialPortIN.IsOpen)
                buttonStartStopIN.BackColor = Color.Green;
            else
                buttonStartStopIN.BackColor = Color.Red;
        }

        private void buttonStartStopOUT_Click(object sender, EventArgs e)
        {
            SerialPortOUTChangeState();
        }

        private void SerialPortOUTChangeState()
        {
            try
            {
                if (serialPortOUT.IsOpen)
                    serialPortOUT.Close();
                else
                {
                    //serialPortOUT.Encoding = Encoding.ASCII;
                    serialPortOUT.Encoding = Encoding.GetEncoding(1250);
                    serialPortOUT.BaudRate = int.Parse(Config.config.Communication[0].SP_OUT_BaudRate);
                    serialPortOUT.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), Config.config.Communication[0].SP_OUT_Parity);
                    serialPortOUT.PortName = Config.config.Communication[0].SP_OUT_PortName;
                    serialPortOUT.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), Config.config.Communication[0].SP_OUT_StopBits);
                    serialPortOUT.DataBits = int.Parse(Config.config.Communication[0].SP_OUT_DataBits);

                    serialPortOUT.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }

            if (serialPortOUT.IsOpen)
                buttonStartStopOUT.BackColor = Color.Green;
            else
                buttonStartStopOUT.BackColor = Color.Red;
        }

        private void frmVrtacka_Load(object sender, EventArgs e)
        {
        }

        private void InitializePorts()
        {
            if (Config.config.Communication.Rows.Count == 0)
            {
                Config.config.Communication.AddCommunicationRow(
                serialPortIN.PortName, serialPortOUT.PortName,
                serialPortOUT.DataBits.ToString(), serialPortIN.DataBits.ToString(),
                serialPortOUT.Parity.ToString(), serialPortIN.Parity.ToString(),
                serialPortOUT.StopBits.ToString(), serialPortIN.StopBits.ToString(),
                serialPortOUT.BaudRate.ToString(), serialPortIN.BaudRate.ToString()
                );
                Config.Save();
            }

            try
            {
                txt_spinBaudRate.Text = Config.config.Communication[0].SP_IN_BaudRate; //serialPortIN.BaudRate.ToString();
                txt_spinParity.Text = Config.config.Communication[0].SP_IN_Parity; //serialPortIN.Parity.ToString();
                txt_spinPort.Text = Config.config.Communication[0].SP_IN_PortName; //serialPortIN.PortName;
                txt_spinStopBits.Text = Config.config.Communication[0].SP_IN_StopBits; //serialPortIN.StopBits.ToString();
                txt_spinDataBits.Text = Config.config.Communication[0].SP_IN_DataBits; //serialPortIN.DataBits.ToString();
            }
            catch
            {
            }

            try
            {
                txt_spoutBaudRate.Text = Config.config.Communication[0].SP_OUT_BaudRate; //serialPortOUT.BaudRate.ToString();
                txt_spoutParity.Text = Config.config.Communication[0].SP_OUT_Parity; //serialPortOUT.Parity.ToString();
                txt_spoutPort.Text = Config.config.Communication[0].SP_OUT_PortName; //serialPortOUT.PortName;
                txt_spoutStopBits.Text = Config.config.Communication[0].SP_OUT_StopBits; //serialPortOUT.StopBits.ToString();
                txt_spoutDataBits.Text = Config.config.Communication[0].SP_OUT_DataBits; //serialPortOUT.DataBits.ToString();
            }
            catch
            {
            }

            SerialPortINChangeState();
            SerialPortOUTChangeState();
        }

        /// <summary>
        /// Nastavi maximalne dlzky podla nastavenia ciaroveho kodu
        /// </summary>
        private void InitTextBoxes()
        {
            //txtBarcode.MaxLength = barcodeParser.Length();

            //txtNazevProgramu.MaxLength = barcodeParser.Length('n');
            //txtRozmerA.MaxLength = barcodeParser.Length('a');
            //txtRozmerB.MaxLength = barcodeParser.Length('b');
            //txtTloustka.MaxLength = barcodeParser.Length('t');
            //txtPocetKusu.MaxLength = barcodeParser.Length('k');
            //txtZakazka.MaxLength = barcodeParser.Length('z');
            //txtPozice.MaxLength = barcodeParser.Length('p');
            //txtPoradi.MaxLength = barcodeParser.Length('o');

        }

        delegate void DataReceivedDelegate(string data);

        private void DataReceived(string data)
        {
            if (datareadEnable) //Pokud neni zobrazeno okno pro potvrzeni, tak muzu nacist dalsi kod
            {
                txtBarcode.Text = data;
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeRead, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                if (DataParse(data))
                    DataSend();
            }
            else //jinak co?
            {
            }
        }

        private void DataClear()
        {
            //txtNazevProgramu.Clear();
            //txtRozmerA.Clear();
            //txtRozmerB.Clear();
            //txtTloustka.Clear();

            //txtPocetKusu.Clear();

            //txtZakazka.Clear();
            //txtPozice.Clear();
            //txtPoradi.Clear();
            ////txtPoznamka.Clear();

            //cbTypOdvodu.SelectedItem = null;

            lblDataValue.Text =
                lblData01.Text =
                lblData02.Text =
                lblData03.Text =
                lblData04.Text =
                lblData05.Text =
                lblData06.Text =
                lblData07.Text = 
                lblData08.Text =
                lblData09.Text =
                string.Empty;
        }

        //private void DataParse(string data)
        //{
        //    DataClear();            

           
        //    // Edit ToO
        //    //8 – název programu
        //    //4 – rozměr A
        //    //4 -  rozměr B
        //    //3 – tloušťka
        //    //3 – kusy ( počet kusů v  celém výrobním příkazu)
        //    //1 – nevyužito – mezera
        //    //14 - číslo prvku (8 zn. zakázka+4 zn. pozice+2 zn. pořadí)
        //    //37 celkem

            
        //    try
        //    {
        //        txtNazevProgramu.Text = data.Substring(barcodeParser.GetIndexOfFirst('n'), barcodeParser.Length('n'));
        //        txtRozmerA.Text = data.Substring(barcodeParser.GetIndexOfFirst('a'), barcodeParser.Length('a'));
        //        txtRozmerB.Text = data.Substring(barcodeParser.GetIndexOfFirst('b'), barcodeParser.Length('b'));
        //        txtTloustka.Text = data.Substring(barcodeParser.GetIndexOfFirst('t'), barcodeParser.Length('t'));

        //        try 
        //        {
        //            int kusu = int.Parse(data.Substring(barcodeParser.GetIndexOfFirst('k'), barcodeParser.Length('k')));
        //            txtPocetKusu.Text = kusu.ToString();
        //        }
        //        catch { }

        //        try { txtZakazka.Text = data.Substring(barcodeParser.GetIndexOfFirst('z'), barcodeParser.Length('z'));; }
        //        catch { }
        //        try { txtPozice.Text = data.Substring(barcodeParser.GetIndexOfFirst('p'), barcodeParser.Length('p'));; }
        //        catch { }
        //        try { txtPoradi.Text = data.Substring(barcodeParser.GetIndexOfFirst('o'), barcodeParser.Length('o'));; }
        //        catch { }

        //        cbTypOdvodu.SelectedItem = cbTypOdvodu.Items[0];

        //        Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeParse, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        //    }
        //    catch (Exception ex)
        //    {
        //        notifyIconState.ShowBalloonTip(2500, ex.Source, ex.Message, ToolTipIcon.Warning);
        //        Log.WriteException(ex);
        //        //MessageBox.Show(ex.Message, ex.Source);
        //    }
        //}
        private bool DataParse(string data)
        {
            //Log.WriteException(String.Format("Parsovana data {0}", data));
            log_hlaska = String.Format("Parsovana data {0}", data);
            ExceptionHandler2.Handle(log_hlaska, "Log_DCDIdealInkJet", "txt");

            try
            {
                //lblData01.Text = String.Format("{0}", data);

                var dd = DataDecode.Parse(data);

                if (dd?.Value == null)
                {
                    string strNeplatnaData = String.Format("Data nejsou platná:'{0}'", data);
                    //Log.WriteException(strNeplatnaData);
                    ExceptionHandler2.Handle(strNeplatnaData, "Log_DCDIdealInkJet", "txt");
                    throw new Exception(strNeplatnaData);
                    //return false;
                }

                DataClear();

                //toolStripStatusLabel1.BackColor = Color.Green;
                //toolStripStatusLabel1.Text = "Načteno: " + dd.Value;

                lblDataValue.Text = dd.Value;
                if (dd.Texty.Count > 0) lblData01.Text = dd.Texty[0];
                if (dd.Texty.Count > 1) lblData02.Text = dd.Texty[1];
                if (dd.Texty.Count > 2) lblData03.Text = dd.Texty[2];
                if (dd.Texty.Count > 3) lblData04.Text = dd.Texty[3];
                if (dd.Texty.Count > 4) lblData05.Text = dd.Texty[4];
                if (dd.Texty.Count > 5) lblData06.Text = dd.Texty[5];
                if (dd.Texty.Count > 6) lblData07.Text = dd.Texty[6];

                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeParse, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                return true;
            }
            catch (Exception ex)
            {
                notifyIconState.ShowBalloonTip(2500, ex.Source, ex.Message, ToolTipIcon.Warning);

                //toolStripStatusLabel1.BackColor = Color.Yellow;
                //toolStripStatusLabel1.Text = ex.Message;

                //MessageBox.Show(ex.Message, ex.Source);
                return false;
            }
        }


        private string DataToString()
        {
            string result = string.Empty;

            //result = lblDataHeader.Text;
            //result = String.Format("{0}{1}{2}{3}", lblData08.Text, (char)0x0D, lblData09.Text, (char)0x04);
            //result = String.Format("{0}{1}{2}{3}", lblData08.Text, " ", lblData09.Text, (char)0x04);
            string dataoutend = string.Empty;
            try
            {
                dataoutend = Config.config.Separators[0].DataOutEnd;
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex.Message);
                ExceptionHandler2.Handle(ex);
            }

            result = String.Format("{0}{1}", 
                lblDataValue.Text, 
                dataoutend
                );

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeBuild, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            return result;
        }

        /// <summary>
        /// Nahradi retazev v dalsom retazci
        /// </summary>
        /// <param name="startIndex">index, od ktoreho nahradzame</param>
        /// <param name="newVal">novy retazec</param>
        /// <param name="sourceString">retazec v ktorom nahradzame</param>
        /// <returns>novy retazec</returns>
        private string ReplaceSubstring(int startIndex, string newVal, String sourceString)
        {
            char[] arr = sourceString.ToCharArray();

            if (startIndex + newVal.Length <= sourceString.Length && startIndex >= 0)
            {
                for (int i = 0; i < newVal.Length; i++)
                {
                    arr[startIndex + i] = newVal[i];
                }

                string res = string.Empty;

                foreach (char ch in arr)
                {
                    res += ch;   
                }

                sourceString = res;
                return sourceString;
            }

            return string.Empty;
        }

        private void serialPortIN_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            while (serialPortIN.BytesToRead > 0)
            {
                string data = string.Empty;
                try
                {
                    data = serialPortIN.ReadLine();
                }
                catch
                {
                    data = serialPortIN.ReadExisting();
                }

                data = data.Trim();
                //data = Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(data));

                this.BeginInvoke(new DataReceivedDelegate(DataReceived), new object[] { data });
            }

        }

        private void buttonReadParams_Click(object sender, EventArgs e)
        {
            DataParse(txtBarcode.Text);
        }

        private void buttonSendParams_Click(object sender, EventArgs e)
        {
            DataSend();
        }

        //private void SendData()
        //{
        //    try
        //    {
        //        datareadEnable = false;

        //        string readeddata = txtBarcode.Text;
        //        string datatosend = DataToString();

        //        serialPortOUT.WriteLine(datatosend + "\r");
        //        Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeSendToPort, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

        //        frmPotvrzeniKusu pks = new frmPotvrzeniKusu(this);
        //        //pks.StartPosition = FormStartPosition.CenterParent;
        //        int povodneKusy = 0;
        //        try
        //        {
        //            povodneKusy = pks.PocetKusu = int.Parse(txtPocetKusu.Text);
        //        }
        //        catch
        //        {
        //            pks.PocetKusu = 0;
        //        }

        //        pks.txtZakazka.Text = txtZakazka.Text;
        //        pks.txtPozice.Text = txtPozice.Text;
        //        pks.txtPoradi.Text = txtPoradi.Text;

        //        if (pks.ShowDialog() == DialogResult.Cancel)
        //        {
        //            Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfirmQTYCanceled, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        //            return;
        //        }
        //        else
        //        {
        //            Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfirmQTYOK, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        //        }

        //        //Ulozeni odeslanych dat na server ... 
        //        Database.Vyroba vyroba = new Database.Vyroba();
        //        vyroba.FASK_Events.AddFASK_EventsRow(
        //            LogConfig.LoginID,
        //            LogConfig.MachineID,
        //            DateTime.Now,
        //            decimal.Parse(povodneKusy.ToString()),
        //            (decimal)pks.PocetKusu,
        //            pks.Poznamka,
        //            readeddata,
        //            datatosend,
        //            this.txtZakazka.Text.Trim() + this.txtPozice.Text.Trim() + this.txtPoradi.Text.Trim(),
        //            string.Empty,                    
        //            Guid.NewGuid(),
        //            cbTypOdvodu.SelectedItem.ToString(),
        //            //Nepotrebna data pro vrtacku
        //            null,null,null,null,null, null
        //            );

        //        Database.VyrobaTableAdapters.FASK_EventsTableAdapter eta = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
        //        eta.Connection.ConnectionString = LogConfig.SqlConnectionStringLocal;
        //        eta.Update(vyroba);

        //        DataClear();

        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message, "Výroba");
        //        notifyIconState.ShowBalloonTip(5000, "Chyba načtení", ex.Message + "\n" + ex.Source, ToolTipIcon.Error);
        //    }
        //    finally
        //    {
        //        datareadEnable = true;
        //    }
        //}

        private void DataSend()
        {
            try
            {
                datareadEnable = false;

                string readeddata = txtBarcode.Text;
                string datatosend = DataToString();

                //Log.WriteException(String.Format("Odesilana data {0}", datatosend));

                log_hlaska = String.Format("Odesilana data {0}", datatosend);
                ExceptionHandler2.Handle(log_hlaska, "Log_DCDIdealInkJet", "txt");

                if (String.IsNullOrEmpty(datatosend))
                {
                    throw new Exception("Data k odeslání jsou prázdná");
                }

                //datatosend = Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(datatosend));

                serialPortOUT.WriteLine(datatosend);

                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeSendToPort, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                //Ulozeni odeslanych dat na server ... 
                //var eventsrow = vyroba.FASK_Events.AddFASK_EventsRow(
                //    LogConfig.LoginID,
                //    LogConfig.MachineID,
                //    DateTime.Now,
                //    1,
                //    1,
                //    string.Empty,
                //    readeddata.Substring(Math.Max(0, readeddata.Length - vyroba.FASK_Events.barcodeReadedColumn.MaxLength)),
                //    datatosend.Substring(Math.Max(0, datatosend.Length - vyroba.FASK_Events.barcodeSendedColumn.MaxLength)),
                //    string.Empty,
                //    string.Empty,
                //    Guid.NewGuid(),
                //    string.Empty,
                //    //Nepotrebna data pro vrtacku
                //    null, null, null, null, null, null
                //    );

                SqlTransaction sqlTransaction = null;
                Database.Classes.Vyroba_Local.EventsInsert(
                    DateTime.Now,
                    ref sqlTransaction,
                    LogConfig.SqlConnectionStringLocal,
                    LogConfig.LoginID,
                    LogConfig.MachineID,
                    string.Empty,
                    readeddata.Substring(Math.Max(0, readeddata.Length - vyroba.FASK_Events.barcodeReadedColumn.MaxLength)),
                    false,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
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

                // docasne zakomentovano ... ???
                //Database.VyrobaTableAdapters.FASK_EventsTableAdapter eta = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
                //eta.Connection.ConnectionString = LogConfig.SqlConnectionStringLocal;
                //eta.Update(vyroba);

                // data ponecham abych videl co posledni bylo odeslano ... :)
                //DataClear();

                notifyIconState.ShowBalloonTip(2500, "Odeslání", datatosend, ToolTipIcon.Info);
                
                //toolStripStatusLabel1.BackColor = Color.Green;
                //toolStripStatusLabel1.Text = "Odesláno: " + datatosend;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Výroba");
               // Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                notifyIconState.ShowBalloonTip(5000, "Chyba odeslání", ex.Message + "\n" + ex.Source, ToolTipIcon.Error);

                //toolStripStatusLabel1.BackColor = Color.Red;
                //toolStripStatusLabel1.Text = "Chyba odeslání: " + ex.Message;

            }
            finally
            {
                datareadEnable = true;
            }
        }

        private void buttonSaveSPIN_Click(object sender, EventArgs e)
        {
            Config.config.Communication[0].SP_IN_BaudRate = txt_spinBaudRate.Text;
            Config.config.Communication[0].SP_IN_DataBits = txt_spinDataBits.Text;
            Config.config.Communication[0].SP_IN_Parity = txt_spinParity.Text;
            Config.config.Communication[0].SP_IN_PortName = txt_spinPort.Text;
            Config.config.Communication[0].SP_IN_StopBits = txt_spinStopBits.Text;

            Config.Save();

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        private void buttonSaveSPOUT_Click(object sender, EventArgs e)
        {
            Config.config.Communication[0].SP_OUT_BaudRate = txt_spoutBaudRate.Text;
            Config.config.Communication[0].SP_OUT_DataBits = txt_spoutDataBits.Text;
            Config.config.Communication[0].SP_OUT_Parity = txt_spoutParity.Text;
            Config.config.Communication[0].SP_OUT_PortName = txt_spoutPort.Text;
            Config.config.Communication[0].SP_OUT_StopBits = txt_spoutStopBits.Text;

            Config.Save();

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        private void frmVrtacka_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPortIN.IsOpen)
                serialPortIN.Close();

            if (serialPortOUT.IsOpen)
                serialPortOUT.Close();
        }

        bool serialINOpenedLastState = true;
        bool serialOUTOpenedLastState = true;

        /// <summary>
        /// Zatvori porty
        /// </summary>
        public void ClosePorts()
        {
            if (serialPortIN.IsOpen)
            {
                serialPortIN.Close();
                serialINOpenedLastState = true;
            }
            else
                serialINOpenedLastState = false;

            if (serialPortOUT.IsOpen)
            {
                serialPortOUT.Close();
                serialOUTOpenedLastState = true;
            }
            else
                serialOUTOpenedLastState = false;
        }

        /// <summary>
        /// Vrati porty do stavu pred zatvorenim
        /// </summary>
        public void ReturnPortsToPreviousState()
        {
            if (serialINOpenedLastState && !serialPortIN.IsOpen)
                serialPortIN.Open();

            if (serialOUTOpenedLastState && !serialPortOUT.IsOpen)
                serialPortOUT.Open();
        }

        private void buttonBarcodeSaveOutput_Click(object sender, EventArgs e)
        {
            Config.config.Separators[0].DataOutEnd = textBoxBarcodeOutput.Text;
            //InitTextBoxes();
            Config.Save();
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }

        #region IModuleConnector Members


        public bool IsReadyToClose(out string message)
        {
            //throw new NotImplementedException();
            message = "ok";
            return true;
        }

        public bool IsReadyToShow(out string message)
        {
            //throw new NotImplementedException();
            message = "ok";
            return true;
        }


        #endregion

    }
}