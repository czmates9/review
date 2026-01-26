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
using Fask.Logging;
using ICommDatabase;

namespace FASK.SledovaniVyroby.Module.Vrtacka_stara
{
    public partial class frmVrtackaStara : Form, IModuleConnector
    {
        private const string defaultBarcodeFormat = "nnnnnnnnaaaabbbbtttvvvkkzzzzpppppppp";
        BarCodeParser barcodeParser;
        BarCodeParser barcodeParserOutput;

        //Zakazani nacteni caroveho kodu ze scanneru, pokud je zobrazeno okno pro potvrzeni
        private bool datareadEnable = true;

        private NotifyIcon notifyIconState;

        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        public frmVrtackaStara()
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
            if (VrtackaStaraConfig.config.Vrtacka_Stara_Barcode.Rows.Count == 0)
            {
                VrtackaStaraConfig.config.Vrtacka_Stara_Barcode.AddVrtacka_Stara_BarcodeRow(defaultBarcodeFormat, defaultBarcodeFormat);
                VrtackaStaraConfig.Save();
            }

            barcodeParser = new BarCodeParser(VrtackaStaraConfig.config.Vrtacka_Stara_Barcode[0].BarcodeInput);
            barcodeParserOutput = new BarCodeParser(VrtackaStaraConfig.config.Vrtacka_Stara_Barcode[0].BarcodeOutput);
            textBoxBarcode.Text = VrtackaStaraConfig.config.Vrtacka_Stara_Barcode[0].BarcodeInput;
            textBoxBarcodeOutput.Text = VrtackaStaraConfig.config.Vrtacka_Stara_Barcode[0].BarcodeOutput;
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

                    serialPortIN.BaudRate = int.Parse(VrtackaStaraConfig.config.Vrtacka[0].SP_IN_BaudRate);
                    serialPortIN.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), VrtackaStaraConfig.config.Vrtacka[0].SP_IN_Parity);
                    serialPortIN.PortName = VrtackaStaraConfig.config.Vrtacka[0].SP_IN_PortName;
                    serialPortIN.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), VrtackaStaraConfig.config.Vrtacka[0].SP_IN_StopBits);
                    serialPortIN.DataBits = int.Parse(VrtackaStaraConfig.config.Vrtacka[0].SP_IN_DataBits);

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
                    serialPortOUT.BaudRate = int.Parse(VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_BaudRate);
                    serialPortOUT.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_Parity);
                    serialPortOUT.PortName = VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_PortName;
                    serialPortOUT.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_StopBits);
                    serialPortOUT.DataBits = int.Parse(VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_DataBits);

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
            if (VrtackaStaraConfig.config.Vrtacka.Rows.Count == 0)
            {
                VrtackaStaraConfig.config.Vrtacka.AddVrtackaRow(
                serialPortIN.PortName, serialPortOUT.PortName,
                serialPortOUT.DataBits.ToString(), serialPortIN.DataBits.ToString(),
                serialPortOUT.Parity.ToString(), serialPortIN.Parity.ToString(),
                serialPortOUT.StopBits.ToString(), serialPortIN.StopBits.ToString(),
                serialPortOUT.BaudRate.ToString(), serialPortIN.BaudRate.ToString()
                );
                VrtackaStaraConfig.Save();
            }

            try
            {
                txt_spinBaudRate.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_IN_BaudRate; //serialPortIN.BaudRate.ToString();
                txt_spinParity.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_IN_Parity; //serialPortIN.Parity.ToString();
                txt_spinPort.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_IN_PortName; //serialPortIN.PortName;
                txt_spinStopBits.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_IN_StopBits; //serialPortIN.StopBits.ToString();
                txt_spinDataBits.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_IN_DataBits; //serialPortIN.DataBits.ToString();
            }
            catch
            {
            }

            try
            {
                txt_spoutBaudRate.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_BaudRate; //serialPortOUT.BaudRate.ToString();
                txt_spoutParity.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_Parity; //serialPortOUT.Parity.ToString();
                txt_spoutPort.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_PortName; //serialPortOUT.PortName;
                txt_spoutStopBits.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_StopBits; //serialPortOUT.StopBits.ToString();
                txt_spoutDataBits.Text = VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_DataBits; //serialPortOUT.DataBits.ToString();
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
            txtBarcode.MaxLength = barcodeParser.Length();

            txtNazevDilce.MaxLength = barcodeParser.Length('n');
            txtRozmerA.MaxLength = barcodeParser.Length('a');
            txtRozmerB.MaxLength = barcodeParser.Length('b');
            txtTloustka.MaxLength = barcodeParser.Length('t');
            txtTextProVyrobu.MaxLength = barcodeParser.Length('v');
            txtPocetKusu.MaxLength = barcodeParser.Length('k');
            txtZakazka.MaxLength = barcodeParser.Length('z');
            txtPopis.MaxLength = barcodeParser.Length('p');

        }

        delegate void DataReceivedDelegate(string data);

        private void DataReceived(string data)
        {
            if (datareadEnable) //Pokud neni zobrazeno okno pro potvrzeni, tak muzu nacist dalsi kod
            {
                txtBarcode.Text = data;
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeRead, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                DataParse(data);

                SendData();
            }
            else //jinak co?
            {
            }
        }

        private void DataClear()
        {
            txtNazevDilce.Clear();
            txtRozmerA.Clear();
            txtRozmerB.Clear();
            txtTloustka.Clear();
            txtTextProVyrobu.Clear();
            txtPocetKusu.Clear();
            txtZakazka.Clear();
            txtPopis.Clear();
            //txtPoznamka.Clear();
        }

        private void DataParse(string data)
        {
            DataClear();            

            //Jako podklady posílám ještě čárový kód a jeho specifikaci :
            //8 zn - název dílce
            //4 zn - rozměr A
            //4 zn - rozměr B
            //3 zn - tloušťka
            //3 zn - text pro výrobu ( 1zn alberti 1 nebo 0 , 2 zn omal 1 nebo 0 , 3 zn zrcadlení 1 nebo 0 )
            //2 zn - počet ks
            //4 zn - zakázka
            //8 zn - popis

            
            try
            {
                /*
                txtNazevDilce.Text = data.Substring(0, 8);
                txtRozmerA.Text = data.Substring(8, 4); 
                txtRozmerB.Text = data.Substring(12, 4); 
                txtTloustka.Text = data.Substring(16, 3); 
                txtTextProVyrobu.Text = data.Substring(19, 3); 
                txtPocetKusu.Text = data.Substring(22, 2);
                try { txtZakazka.Text = data.Substring(24, 4); }
                catch { }
                try { txtPopis.Text = data.Substring(28, 8); }
                catch { }
                //txtPoznamka.Text = "";
                */

                try
                {
                    txtNazevDilce.Text = data.Substring(barcodeParser.GetIndexOfFirst('n'), barcodeParser.Length('n'));
                    txtRozmerA.Text = data.Substring(barcodeParser.GetIndexOfFirst('a'), barcodeParser.Length('a'));
                    txtRozmerB.Text = data.Substring(barcodeParser.GetIndexOfFirst('b'), barcodeParser.Length('b'));
                    txtTloustka.Text = data.Substring(barcodeParser.GetIndexOfFirst('t'), barcodeParser.Length('t'));
                    txtTextProVyrobu.Text = data.Substring(barcodeParser.GetIndexOfFirst('v'), barcodeParser.Length('v'));
                    txtPocetKusu.Text = data.Substring(barcodeParser.GetIndexOfFirst('k'), barcodeParser.Length('k'));

                    try { txtZakazka.Text = data.Substring(barcodeParser.GetIndexOfFirst('z'), barcodeParser.Length('z'));}
                    catch { }
                    try { txtPopis.Text = data.Substring(barcodeParser.GetIndexOfFirst('p'), barcodeParser.Length('p')); }
                    catch { }
                    //txtPoznamka.Text = "";
                }
                catch (Exception ex)
                {
                    //Log.WriteException(ex);
                    ExceptionHandler2.Handle(ex);
                }

                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeParse, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
            }
            catch (Exception ex)
            {
                notifyIconState.ShowBalloonTip(2500, ex.Source, ex.Message, ToolTipIcon.Warning);
                //MessageBox.Show(ex.Message, ex.Source);
            }
        }

       
        private string DataToString()
        {
            //Jako podklady posílám ještě čárový kód a jeho specifikaci :
            //8 zn - název dílce
            //4 zn - rozměr A
            //4 zn - rozměr B
            //3 zn - tloušťka
            //3 zn - text pro výrobu ( 1zn alberti 1 nebo 0 , 2 zn omal 1 nebo 0 , 3 zn zrcadlení 1 nebo 0 )
            //2 zn - počet ks
            //4 zn - zakázka
            //8 zn - popis
            string result = string.Empty;
            //result = result.PadLeft(barcodeParserOutput.Length(), ' ');
            result = barcodeParserOutput.BarcodeTemplate;

            string nazevdilce = txtNazevDilce.Text.Trim();
            string rozmerA = txtRozmerA.Text.Trim();
            string rozmerB = txtRozmerB.Text.Trim();
            string tloustka = txtTloustka.Text.Trim();
            string textprovyrobu = txtTextProVyrobu.Text.Trim();
            string pocetks = txtPocetKusu.Text.Trim();
            string zakazka = txtZakazka.Text.Trim();
            string popis = txtPopis.Text.Trim();

            if (nazevdilce.Length == 0)
                throw new Exception("Není zadán název dílce");

            try { int.Parse(rozmerA); }
            catch (Exception ex) { throw new Exception("Rozměr A není číslo", ex); }

            try { int.Parse(rozmerB); }
            catch (Exception ex) { throw new Exception("Rozměr B není číslo", ex); }

            try { int.Parse(tloustka); }
            catch (Exception ex) { throw new Exception("Tloušťka není číslo", ex); }

            try { int.Parse(pocetks); }
            catch (Exception ex) { throw new Exception("Počet kusů není číslo", ex); }


            int len;
            len = barcodeParserOutput.Length('n');
            //if (len == 0 || len == barcodeParser.Length('n'))
            //    nazevdilce = nazevdilce.PadRight(barcodeParserOutput.Length('n'), '-');
            //else
            //    throw new Exception("Počet znaku 'n' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < nazevdilce.Length)
                throw new Exception("Počet znaku 'n' ve výstupním čárovém kódu je menší než požadovaný (" + nazevdilce.Length + ")");
            else
                nazevdilce = nazevdilce.PadRight(barcodeParserOutput.Length('n'), '-');

            len = barcodeParserOutput.Length('a');
            //if (len == 0 || len == barcodeParser.Length('a'))
            //    rozmerA = rozmerA.PadLeft(barcodeParserOutput.Length('a'), '0');
            //else
            //    throw new Exception("Počet znaku 'a' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < rozmerA.Length)
                throw new Exception("Počet znaku 'a' ve výstupním čárovém kódu je menší než požadovaný (" + rozmerA.Length + ")");
            else
                rozmerA = rozmerA.PadLeft(barcodeParserOutput.Length('a'), '0');


            len = barcodeParserOutput.Length('b');
            //if (len == 0 || len == barcodeParser.Length('b'))
            //    rozmerB = rozmerB.PadLeft(barcodeParserOutput.Length('b'), '0');
            //else
            //    throw new Exception("Počet znaku 'b' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < rozmerB.Length)
                throw new Exception("Počet znaku 'b' ve výstupním čárovém kódu je menší než požadovaný (" + rozmerB.Length + ")");
            else
                rozmerB = rozmerB.PadLeft(barcodeParserOutput.Length('b'), '0');


            len = barcodeParserOutput.Length('t');
            //if (len == 0 || len == barcodeParser.Length('t'))
            //    tloustka = tloustka.PadLeft(barcodeParserOutput.Length('t'), '0');
            //else
            //    throw new Exception("Počet znaku 't' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < tloustka.Length)
                throw new Exception("Počet znaku 't' ve výstupním čárovém kódu je menší než požadovaný (" + tloustka.Length + ")");
            else
                tloustka = tloustka.PadLeft(barcodeParserOutput.Length('t'), '0');

            len = barcodeParserOutput.Length('v');
            if (len > 0 && len < textprovyrobu.Length)
                throw new Exception("Počet znaku 'v' ve výstupním čárovém kódu je menší než požadovaný (" + textprovyrobu.Length + ")");
            else
                textprovyrobu = textprovyrobu.PadLeft(barcodeParserOutput.Length('v'), '0');

            len = barcodeParserOutput.Length('k');
            //if (len == 0 || len == barcodeParser.Length('k'))
            //    pocetks = pocetks.PadLeft(barcodeParserOutput.Length('k'), '0');
            //else
            //    throw new Exception("Počet znaku 'k' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < pocetks.Length)
                throw new Exception("Počet znaku 'k' ve výstupním čárovém kódu je menší než požadovaný (" + pocetks.Length + ")");
            else
                pocetks = pocetks.PadLeft(barcodeParserOutput.Length('k'), '0');

            

            len = barcodeParserOutput.Length('z');
            //if (len == 0 || len == barcodeParser.Length('z'))
            //    zakazka = zakazka.PadLeft(barcodeParserOutput.Length('z'), '0');
            //else
            //    throw new Exception("Počet znaku 'z' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < zakazka.Length)
                throw new Exception("Počet znaku 'z' ve výstupním čárovém kódu je menší než požadovaný (" + zakazka.Length + ")");
            else
                zakazka = zakazka.PadLeft(barcodeParserOutput.Length('z'), '0');            

            len = barcodeParserOutput.Length('p');
            //if (len == 0 || len == barcodeParser.Length('p'))
            //    popis = popis.PadLeft(barcodeParserOutput.Length('p'), '0');
            //else
            //    throw new Exception("Počet znaku 'p' ve výstupním čárovém kódu neodpovídá vstupnímu čárovému kódu");
            if (len > 0 && len < popis.Length)
                throw new Exception("Počet znaku 'p' ve výstupním čárovém kódu je menší než požadovaný (" + popis.Length + ")");
            else
                popis = popis.PadLeft(barcodeParserOutput.Length('p'), '0');
            
            //if (popis.Length > 0)
            //{
            //    zakazka = zakazka.PadRight(4, '-');
            //    popis = popis.PadRight(8, '-');
            //} else if (zakazka.Length > 0)
            //{
            //    zakazka = zakazka.PadRight(4, '-');
            //}

            //string data = nazevdilce + rozmerA + rozmerB + tloustka + textprovyrobu + pocetks; // +zakazka + popis;

            int index;
            index = barcodeParserOutput.GetIndexOfFirst('n');
            if (index >= 0)
                result = ReplaceSubstring(index, nazevdilce, result);
            index = barcodeParserOutput.GetIndexOfFirst('a');
            if (index >= 0)
                result = ReplaceSubstring(index, rozmerA, result);
            index = barcodeParserOutput.GetIndexOfFirst('b');
            if (index >= 0)
                result = ReplaceSubstring(index, rozmerB, result);
            index = barcodeParserOutput.GetIndexOfFirst('t');
            if (index >= 0)
                result = ReplaceSubstring(index, tloustka, result);
            index = barcodeParserOutput.GetIndexOfFirst('v');
            if (index >= 0)
                result = ReplaceSubstring(index, textprovyrobu, result);
            index = barcodeParserOutput.GetIndexOfFirst('k');
            if (index >= 0)
                result = ReplaceSubstring(index, pocetks, result);
            index = barcodeParserOutput.GetIndexOfFirst('z');
            if (index >= 0)
                result = ReplaceSubstring(index, zakazka, result);
            index = barcodeParserOutput.GetIndexOfFirst('p');
            if (index >= 0)
                result = ReplaceSubstring(index, popis, result);


            //string data = string.Empty;
            //data += txtNazevDilce.Text;
            //data += txtRozmerA.Text;
            //data += txtTloustka.Text;
            //data += txtTextProVyrobu.Text;
            //data += txtPocetKusu.Text;
            //data += txtZakazka.Text;
            //data += txtPopis.Text;

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

            data = data.Trim();

            this.BeginInvoke(new DataReceivedDelegate(DataReceived), new object[] { data });

        }

        private void buttonReadParams_Click(object sender, EventArgs e)
        {
            DataParse(txtBarcode.Text);
        }

        private void buttonSendParams_Click(object sender, EventArgs e)
        {
            SendData();
        }

        private void SendData()
        {
            try
            {
                datareadEnable = false;

                string datatosend = DataToString();

                serialPortOUT.WriteLine(datatosend + "\r");
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeSendToPort, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                frmPotvrzeniKusuStara pks = new frmPotvrzeniKusuStara();
                //pks.StartPosition = FormStartPosition.CenterParent;
                pks.PocetKusu = int.Parse(txtPocetKusu.Text);
                if (pks.ShowDialog() == DialogResult.Cancel)
                {
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfirmQTYCanceled, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
                    return;
                }
                else
                {
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfirmQTYOK, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
                }


                // //Ulozeni odeslanych dat na server ... 
                // Database.Vyroba vyroba = new Database.Vyroba();
                // vyroba.FASK_Events.AddFASK_EventsRow(
                //     LogConfig.LoginID,
                //     LogConfig.MachineID,
                //     DateTime.Now,
                //     decimal.Parse(txtPocetKusu.Text),
                //     (decimal)pks.PocetKusu,
                //     pks.Poznamka,
                //     datatosend,
                //     datatosend,
                //     this.txtZakazka.Text.Trim(),
                //     this.txtPopis.Text.Trim(),
                //     Guid.NewGuid(),
                //     "D",
                //     //Doplneni nepouzivanych sloupcu ve vrtacce na NULL
                //     null,null,null,null,null,null,
                //     string.Empty, -1, string.Empty, string.Empty, string.Empty, 0,
                //     Guid.NewGuid(),
                //     0,
                //     string.Empty,
                //     0,
                //     1,
                //     null,
                //     null,
                //     null,
                //     null,
                //     null
                //     );

                ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();
                var row = vyroba.FASK_Events.NewFASK_EventsRow();


                row.loginid = LogConfig.LoginID;
                row.dateeve = DateTime.Now;
                row.qty = decimal.Parse(txtPocetKusu.Text);
                row.qtyReal = (decimal)pks.PocetKusu;
                row.description = pks.Poznamka;
                row.barcodeReaded = datatosend;
                row.barcodeSended = datatosend;
                row.zakazka = this.txtZakazka.Text.Trim();
                row.popis = this.txtPopis.Text.Trim();
                row.faskGUID = Guid.NewGuid();
                row.reportType = "D";
                row.SetisProcessedNull();
                row.SetIDONull();
                row.Setscan1Null();
                row.Setscan2Null();
                row.Setscan3Null();
                row.SetsensorNull();
                row.SetmaterialNull();
                row.machineid = LogConfig.MachineID;
                row.SetVPHNull();
                row.SetVPPolNull();
                row.SetEAN_ISNull();
                row.SetIS_IDNull();
                row.SetNMBRPALNull();
                row.SetstatusNull();
                row.SetproductionGuidNull();
                row.QTYPACK = 0;
                row.PackType = string.Empty;
                row.SetWEIGHTNull();
                vyroba.FASK_Events.AddFASK_EventsRow(row);

                //Ulozeni odeslanych dat na server ... 
                
                //vyroba.FASK_Events.AddFASK_EventsRow(
                //    LogConfig.LoginID,
                //    LogConfig.MachineID,
                //    DateTime.Now,
                //    decimal.Parse(txtPocetKusu.Text),
                //    (decimal)pks.PocetKusu,
                //    pks.Poznamka,
                //    datatosend,
                //    datatosend,
                //    this.txtZakazka.Text.Trim(),
                //    this.txtPopis.Text.Trim(),
                //    Guid.NewGuid(),
                //    "D",
                //    //Doplneni nepouzivanych sloupcu ve vrtacce na NULL
                //    null,null,null,null,null,null,
                //    string.Empty, -1, string.Empty, string.Empty, string.Empty, 0,
                //    Guid.NewGuid(),
                //    0,
                //    string.Empty,
                //    0
                //    );

                //Database.VyrobaTableAdapters.FASK_EventsTableAdapter eta = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
                //eta.Connection.ConnectionString = LogConfig.SqlConnectionStringLocal;
                //eta.Update(vyroba);
                Database.Classes.Vyroba_Local.upload_FASK_Events(vyroba, LogConfig.SqlConnectionStringLocal);

                DataClear();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Výroba");
                notifyIconState.ShowBalloonTip(5000, "Chyba načtení", ex.Message + "\n" + ex.Source, ToolTipIcon.Error);
            }
            finally
            {
                datareadEnable = true;
            }
        }

        private void buttonSaveSPIN_Click(object sender, EventArgs e)
        {
            VrtackaStaraConfig.config.Vrtacka[0].SP_IN_BaudRate = txt_spinBaudRate.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_IN_DataBits = txt_spinDataBits.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_IN_Parity = txt_spinParity.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_IN_PortName = txt_spinPort.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_IN_StopBits = txt_spinStopBits.Text;

            VrtackaStaraConfig.Save();

            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.ConfigModChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        }

        private void buttonSaveSPOUT_Click(object sender, EventArgs e)
        {
            VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_BaudRate = txt_spoutBaudRate.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_DataBits = txt_spoutDataBits.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_Parity = txt_spoutParity.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_PortName = txt_spoutPort.Text;
            VrtackaStaraConfig.config.Vrtacka[0].SP_OUT_StopBits = txt_spoutStopBits.Text;

            VrtackaStaraConfig.Save();

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
            VrtackaStaraConfig.config.Vrtacka_Stara_Barcode[0].BarcodeInput = textBoxBarcode.Text;
            barcodeParser = new BarCodeParser(textBoxBarcode.Text);
            InitTextBoxes();
            VrtackaStaraConfig.Save();
        }

        private void buttonSavebarcodeOutput_Click(object sender, EventArgs e)
        {
            VrtackaStaraConfig.config.Vrtacka_Stara_Barcode[0].BarcodeOutput = textBoxBarcodeOutput.Text;
            barcodeParserOutput = new BarCodeParser(textBoxBarcodeOutput.Text);
            VrtackaStaraConfig.Save();
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
            throw new NotImplementedException();
        }

        #endregion

        public bool IsReadyToShow(out string message)
        {
            //throw new NotImplementedException();
            message = "ok";
            return true;
        }
    }
}