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

// module name : Module.Rapol.InkJet.frmMain

namespace Module.Rapol.InkJet
{
    public partial class frmMain : Form, IModuleConnector
    {
        /// <summary>
        /// databaze pro ulozeni dat ... pouzito pro definici max delky ... pole
        /// </summary>
        private readonly ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();

        //Format barkodu a parsery
        private const string defaultBarcodeFormat = "nnnnnnnnaaaabbbbtttkkk_zzzzzzzzppppoo";
        BarCodeParser  barcodeParser;
        BarCodeParser barcodeParserOutput;

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
            if (VrtackaConfig.config.Vrtacka_Barcode.Rows.Count == 0)
            {
                VrtackaConfig.config.Vrtacka_Barcode.AddVrtacka_BarcodeRow(defaultBarcodeFormat, defaultBarcodeFormat);
                VrtackaConfig.Save();
            }

            barcodeParser = new BarCodeParser(VrtackaConfig.config.Vrtacka_Barcode[0].BarcodeInput);
            barcodeParserOutput = new BarCodeParser(VrtackaConfig.config.Vrtacka_Barcode[0].BarcodeOutput);
            textBoxBarcodeInput.Text = VrtackaConfig.config.Vrtacka_Barcode[0].BarcodeInput;
            textBoxBarcodeOutput.Text = VrtackaConfig.config.Vrtacka_Barcode[0].BarcodeOutput;
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
                    serialPortIN.BaudRate = int.Parse(VrtackaConfig.config.Vrtacka[0].SP_IN_BaudRate);
                    serialPortIN.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), VrtackaConfig.config.Vrtacka[0].SP_IN_Parity);
                    serialPortIN.PortName = VrtackaConfig.config.Vrtacka[0].SP_IN_PortName;
                    serialPortIN.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), VrtackaConfig.config.Vrtacka[0].SP_IN_StopBits);
                    serialPortIN.DataBits = int.Parse(VrtackaConfig.config.Vrtacka[0].SP_IN_DataBits);

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
                    serialPortOUT.BaudRate = int.Parse(VrtackaConfig.config.Vrtacka[0].SP_OUT_BaudRate);
                    serialPortOUT.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), VrtackaConfig.config.Vrtacka[0].SP_OUT_Parity);
                    serialPortOUT.PortName = VrtackaConfig.config.Vrtacka[0].SP_OUT_PortName;
                    serialPortOUT.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), VrtackaConfig.config.Vrtacka[0].SP_OUT_StopBits);
                    serialPortOUT.DataBits = int.Parse(VrtackaConfig.config.Vrtacka[0].SP_OUT_DataBits);

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
            if (VrtackaConfig.config.Vrtacka.Rows.Count == 0)
            {
                VrtackaConfig.config.Vrtacka.AddVrtackaRow(
                serialPortIN.PortName, serialPortOUT.PortName,
                serialPortOUT.DataBits.ToString(), serialPortIN.DataBits.ToString(),
                serialPortOUT.Parity.ToString(), serialPortIN.Parity.ToString(),
                serialPortOUT.StopBits.ToString(), serialPortIN.StopBits.ToString(),
                serialPortOUT.BaudRate.ToString(), serialPortIN.BaudRate.ToString()
                );
                VrtackaConfig.Save();
            }

            try
            {
                txt_spinBaudRate.Text = VrtackaConfig.config.Vrtacka[0].SP_IN_BaudRate; //serialPortIN.BaudRate.ToString();
                txt_spinParity.Text = VrtackaConfig.config.Vrtacka[0].SP_IN_Parity; //serialPortIN.Parity.ToString();
                txt_spinPort.Text = VrtackaConfig.config.Vrtacka[0].SP_IN_PortName; //serialPortIN.PortName;
                txt_spinStopBits.Text = VrtackaConfig.config.Vrtacka[0].SP_IN_StopBits; //serialPortIN.StopBits.ToString();
                txt_spinDataBits.Text = VrtackaConfig.config.Vrtacka[0].SP_IN_DataBits; //serialPortIN.DataBits.ToString();
            }
            catch
            {
            }

            try
            {
                txt_spoutBaudRate.Text = VrtackaConfig.config.Vrtacka[0].SP_OUT_BaudRate; //serialPortOUT.BaudRate.ToString();
                txt_spoutParity.Text = VrtackaConfig.config.Vrtacka[0].SP_OUT_Parity; //serialPortOUT.Parity.ToString();
                txt_spoutPort.Text = VrtackaConfig.config.Vrtacka[0].SP_OUT_PortName; //serialPortOUT.PortName;
                txt_spoutStopBits.Text = VrtackaConfig.config.Vrtacka[0].SP_OUT_StopBits; //serialPortOUT.StopBits.ToString();
                txt_spoutDataBits.Text = VrtackaConfig.config.Vrtacka[0].SP_OUT_DataBits; //serialPortOUT.DataBits.ToString();
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

            lblDataHeader.Text =
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
            string log_hlaska = String.Format("Parsovana data {0}", data);
            ExceptionHandler2.Handle(log_hlaska, "Log_RapolInkJet", "txt");

            try
            {
                //lblData01.Text = String.Format("{0}", data);

                var dd = DataDecode.Parse(data);

                if (dd?.Header == null)
                {
                    string strNeplatnaData = String.Format("Data nejsou platná:'{0}'", data);
                    //Log.WriteException(strNeplatnaData);
                    ExceptionHandler2.Handle(strNeplatnaData, "Log_RapolInkJet", "txt");
                    //throw new Exception(strNeplatnaData);
                    return false;
                }

                DataClear();

                lblDataHeader.Text = dd.Header;
                lblData01.Text = dd.Texty[0];
                lblData02.Text = dd.Texty[1];
                lblData03.Text = dd.Texty[2];
                lblData04.Text = dd.Texty[3];
                lblData05.Text = dd.Texty[4];
                lblData06.Text = dd.Texty[5];
                lblData07.Text = dd.Texty[6];

                string tmptxt = DeleteDate(dd.Texty[7]); 

                lblData08.Text = tmptxt;
                
                lblData09.Text = dd.Texty[8];

                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeParse, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                return true;
            }
            catch (Exception ex)
            {
                notifyIconState.ShowBalloonTip(2500, ex.Source, ex.Message, ToolTipIcon.Warning);
                //MessageBox.Show(ex.Message, ex.Source);
                return false;
            }
        }

        private string DeleteDate(string v)
        {
            try
            {
                string tmpout = string.Empty;

                char separ = ' ';


                string[] separeList = v.Split(separ);


                ;

                for (int i = separeList.Length-1; i > -1; i--)
                {
                    string tmp = separeList[i];


                    if (!string.IsNullOrEmpty(tmp))
                    {
                        DateTime date;

                        if (DateTime.TryParse(tmp, out date))
                        {
                            continue;
                        }

                        tmpout = tmp + separ + tmpout;
                    }
                }




                return tmpout;

            }
            catch (Exception ex)
            {
                notifyIconState.ShowBalloonTip(2500, ex.Source, ex.Message, ToolTipIcon.Warning);
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                return null;
                //MessageBox.Show(ex.Message, ex.Source);
            }
        }


        //private string DataToString()
        //{

        //    // Edit ToO
        //    //8 – název programu
        //    //4 – rozměr A
        //    //4 -  rozměr B
        //    //3 – tloušťka
        //    //3 – kusy ( počet kusů v  celém výrobním příkazu)
        //    //1 – nevyužito – mezera
        //    //14 - číslo prvku (8 zn. zakázka+4 zn. pozice+2 zn. pořadí)
        //    //37 celkem

        //    string result = string.Empty;
        //    //result = result.PadLeft(barcodeParserOutput.Length(), ' ');            
        //    result = barcodeParserOutput.BarcodeTemplate;

        //    string nazevprogramu = txtNazevProgramu.Text.Trim();
        //    string rozmerA = txtRozmerA.Text.Trim();
        //    string rozmerB = txtRozmerB.Text.Trim();
        //    string tloustka = txtTloustka.Text.Trim();

        //    string pocetks = txtPocetKusu.Text.Trim();

        //    string zakazka = txtZakazka.Text.Trim();
        //    string pozice = txtPozice.Text.Trim();
        //    string poradi = txtPoradi.Text.Trim();

        //    if (nazevprogramu.Length == 0)
        //        throw new Exception("Není zadán název dílce");

        //    try { int.Parse(rozmerA); }
        //    catch (Exception ex) { throw new Exception("Rozměr A není číslo", ex); }

        //    try { int.Parse(rozmerB); }
        //    catch (Exception ex) { throw new Exception("Rozměr B není číslo", ex); }

        //    try { int.Parse(tloustka); }
        //    catch (Exception ex) { throw new Exception("Tloušťka není číslo", ex); }

        //    try { int.Parse(pocetks); }
        //    catch (Exception ex) { throw new Exception("Počet kusů není číslo", ex); }

        //    int len;
        //    len = barcodeParserOutput.Length('n');
        //    //if (len == 0 || len == barcodeParser.Length('n'))
        //    //    nazevprogramu = nazevprogramu.PadRight(barcodeParserOutput.Length('n'), '-');
        //    //else
        //    //    throw new Exception("Počet znaku 'n' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < nazevprogramu.Length)
        //        throw new Exception("Počet znaku 'n' ve výstupním čárovém kódu je menší než požadovaný (" + nazevprogramu.Length + ")");
        //    else
        //        nazevprogramu = nazevprogramu.PadRight(barcodeParserOutput.Length('n'), '-');

        //    len = barcodeParserOutput.Length('a');
        //    //if (len == 0 || len == barcodeParser.Length('a'))
        //    //    rozmerA = rozmerA.PadLeft(barcodeParserOutput.Length('a'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 'a' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < rozmerA.Length)
        //        throw new Exception("Počet znaku 'a' ve výstupním čárovém kódu je menší než požadovaný (" + rozmerA.Length + ")");
        //    else
        //        rozmerA = rozmerA.PadLeft(barcodeParserOutput.Length('a'), '0');


        //    len = barcodeParserOutput.Length('b');
        //    //if (len == 0 || len == barcodeParser.Length('b'))
        //    //    rozmerB = rozmerB.PadLeft(barcodeParserOutput.Length('b'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 'b' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < rozmerB.Length)
        //        throw new Exception("Počet znaku 'b' ve výstupním čárovém kódu je menší než požadovaný (" + rozmerB.Length + ")");
        //    else
        //        rozmerB = rozmerB.PadLeft(barcodeParserOutput.Length('b'), '0');

        //    len = barcodeParserOutput.Length('t');
        //    //if (len == 0 || len == barcodeParser.Length('t'))
        //    //    tloustka = tloustka.PadLeft(barcodeParserOutput.Length('t'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 't' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < tloustka.Length)
        //        throw new Exception("Počet znaku 't' ve výstupním čárovém kódu je menší než požadovaný (" + tloustka.Length + ")");
        //    else
        //        tloustka = tloustka.PadLeft(barcodeParserOutput.Length('t'), '0');

        //    len = barcodeParserOutput.Length('k');
        //    //if (len == 0 || len == barcodeParser.Length('k'))
        //    //    pocetks = pocetks.PadLeft(barcodeParserOutput.Length('k'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 'k' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < pocetks.Length)
        //        throw new Exception("Počet znaku 'k' ve výstupním čárovém kódu je menší než požadovaný (" + pocetks.Length + ")");
        //    else
        //        pocetks = pocetks.PadLeft(barcodeParserOutput.Length('k'), '0');

        //    len = barcodeParserOutput.Length('z');
        //    //if (len == 0 || len == barcodeParser.Length('z'))
        //    //    zakazka = zakazka.PadLeft(barcodeParserOutput.Length('z'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 'z' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < zakazka.Length)
        //        throw new Exception("Počet znaku 'z' ve výstupním čárovém kódu je menší než požadovaný (" + zakazka.Length + ")");
        //    else
        //        zakazka = zakazka.PadLeft(barcodeParserOutput.Length('z'), '0');

        //    len = barcodeParserOutput.Length('p');
        //    //if (len == 0 || len == barcodeParser.Length('p'))
        //    //    pozice = pozice.PadLeft(barcodeParserOutput.Length('p'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 'p' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < pozice.Length)
        //        throw new Exception("Počet znaku 'p' ve výstupním čárovém kódu je menší než požadovaný (" + pozice.Length + ")");
        //    else
        //        pozice = pozice.PadLeft(barcodeParserOutput.Length('p'), '0');

        //    len = barcodeParserOutput.Length('o');
        //    //if (len == 0 || len == barcodeParser.Length('o'))
        //    //    poradi = poradi.PadLeft(barcodeParserOutput.Length('o'), '0');
        //    //else
        //    //    throw new Exception("Počet znaku 'o' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
        //    if (len > 0 && len < poradi.Length)
        //        throw new Exception("Počet znaku 'o' ve výstupním čárovém kódu je menší než požadovaný (" + poradi.Length + ")");
        //    else
        //        poradi = poradi.PadLeft(barcodeParserOutput.Length('o'), '0');

        //    //if (popis.Length > 0)
        //    //{
        //    //    zakazka = zakazka.PadRight(4, '-');
        //    //    popis = popis.PadRight(8, '-');
        //    //} else if (zakazka.Length > 0)
        //    //{
        //    //    zakazka = zakazka.PadRight(4, '-');
        //    //}

        //    int index;
        //    index = barcodeParserOutput.GetIndexOfFirst('n');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, nazevprogramu, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('a');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, rozmerA, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('b');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, rozmerB, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('t');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, tloustka, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('k');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, pocetks, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('z');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, zakazka, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('p');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, pozice, result);
        //    index = barcodeParserOutput.GetIndexOfFirst('o');
        //    if (index >= 0)
        //        result = ReplaceSubstring(index, poradi, result);

        //    Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.BarCodeBuild, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

        //    return result.Trim();
        //}

        private string DataToString()
        {
            string result = string.Empty;

            //result = lblDataHeader.Text;
            //result = String.Format("{0}{1}{2}{3}", lblData08.Text, (char)0x0D, lblData09.Text, (char)0x04);
            result = String.Format("{0}{1}{2}{3}", lblData08.Text, " ", lblData09.Text, (char)0x04);

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeBuild, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            return result.Trim();
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
            string data = string.Empty;
            try
            {
                data = serialPortIN.ReadLine();
            }
            catch 
            {
                data = serialPortIN.ReadExisting();
            }

            //data = data.Trim();

            this.BeginInvoke(new DataReceivedDelegate(DataReceived), new object[] { data });

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
                string log_hlaska = String.Format("Odesilana data {0}", datatosend);
                ExceptionHandler2.Handle(log_hlaska, "Log_RapolInkJet", "txt");

                if (String.IsNullOrEmpty(datatosend))
                {
                    throw new Exception("Data k odeslání jsou prázdná");
                }

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

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Výroba");
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                notifyIconState.ShowBalloonTip(5000, "Chyba odeslání", ex.Message + "\n" + ex.Source, ToolTipIcon.Error);
            }
            finally
            {
                datareadEnable = true;
            }
        }

        private void buttonSaveSPIN_Click(object sender, EventArgs e)
        {
            VrtackaConfig.config.Vrtacka[0].SP_IN_BaudRate = txt_spinBaudRate.Text;
            VrtackaConfig.config.Vrtacka[0].SP_IN_DataBits = txt_spinDataBits.Text;
            VrtackaConfig.config.Vrtacka[0].SP_IN_Parity = txt_spinParity.Text;
            VrtackaConfig.config.Vrtacka[0].SP_IN_PortName = txt_spinPort.Text;
            VrtackaConfig.config.Vrtacka[0].SP_IN_StopBits = txt_spinStopBits.Text;

            VrtackaConfig.Save();

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        private void buttonSaveSPOUT_Click(object sender, EventArgs e)
        {
            VrtackaConfig.config.Vrtacka[0].SP_OUT_BaudRate = txt_spoutBaudRate.Text;
            VrtackaConfig.config.Vrtacka[0].SP_OUT_DataBits = txt_spoutDataBits.Text;
            VrtackaConfig.config.Vrtacka[0].SP_OUT_Parity = txt_spoutParity.Text;
            VrtackaConfig.config.Vrtacka[0].SP_OUT_PortName = txt_spoutPort.Text;
            VrtackaConfig.config.Vrtacka[0].SP_OUT_StopBits = txt_spoutStopBits.Text;

            VrtackaConfig.Save();

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

        private void buttonSaveBarcode_Click(object sender, EventArgs e)
        {
            VrtackaConfig.config.Vrtacka_Barcode[0].BarcodeInput = textBoxBarcodeInput.Text;
            barcodeParser = new BarCodeParser(textBoxBarcodeInput.Text);
            InitTextBoxes();
            VrtackaConfig.Save();
        }

        private void buttonBarcodeSaveOutput_Click(object sender, EventArgs e)
        {
            VrtackaConfig.config.Vrtacka_Barcode[0].BarcodeOutput = textBoxBarcodeOutput.Text;
            barcodeParserOutput = new BarCodeParser(textBoxBarcodeOutput.Text);
            //InitTextBoxes();
            VrtackaConfig.Save();
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
            message = "ok";
            return true;
        }

        #endregion

    }
}