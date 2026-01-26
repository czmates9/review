using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.ChipScannersIfc;
using System.Reflection;
using System.IO;
using System.Data.SqlClient;

namespace FASK.SledovaniVyroby.Logging
{
    public partial class frmLogin1 : Form
    {
        //Promenna pro pro praci se cteckou cipu
        IChipScannersConnector chipScannerConnector = null;

        public frmLogin1()
        {
            InitializeComponent();
#if DEBUG
            txtID.Text = "1";
            txtPWD.Text = "1";
#endif

            //Porty
            try { InitializePorts(); }
            catch (Exception ex) { Log.WriteException(ex); }
        }

        private void frmLogin1_Activated(object sender, EventArgs e)
        {
            if (method == 4)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void frmLogin1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Scanner
            if (serialPortIN.IsOpen) serialPortIN.Close();
            //Ctecka cipu
            if (serialPortIN2.IsOpen) serialPortIN2.Close();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }
        //Prijem dat na portu pro scanner
        private void serialPortIN_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            string data = string.Empty;
            try { data = serialPortIN.ReadLine(); }
            catch { data = serialPortIN.ReadExisting(); }

            this.BeginInvoke(new DataReceivedDelegate(DataReceivedIN), new object[] { data.Trim() });
        }

        //Prijem na portu ctecky cipu
        private void serialPortIN2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //HINT: pokud je nastaven limit, tak je mozne zpravy nacitat metodou Readline spusobem nize vzdy, i kdyz zprava nekonci LF, pac po limitu se o to postara ReadExisting
            string data = string.Empty;
            try
            {
                //Pokud kod cipu obsahuje LF, je problem, pak nactu vse po LF a pridam jej
                data = serialPortIN2.ReadLine() + '\n';
                //A pokud je jeste co nacitat, donactu
                if (serialPortIN2.BytesToRead != 0) data += serialPortIN2.ReadExisting();
            }
            //Jinak ctu vse
            catch { data = serialPortIN2.ReadExisting(); }

            this.BeginInvoke(new DataReceivedDelegate(DataReceivedIN2), new object[] { data.Trim() });
        }

        //Delegat pro prijem dat
        delegate void DataReceivedDelegate(string data);

        //Funkce pro zpracovani dat - scanner
        private void DataReceivedIN(string data)
        {
            //Overeni kodu
            string ID, PWD;
            validateBArcode(data, out ID, out PWD);
            //Zobrazeni
            txtID.Text = ID;
            txtPWD.Text = PWD;
            //Konec dialogu
            this.DialogResult = DialogResult.OK;
        }

        //Funkce pro zpracovani dat - ctecka
        private void DataReceivedIN2(string data)
        {
            string note = string.Empty, procData = string.Empty;
            try
            {
                //Zpracovani kodu chipu se nepovedlo
                if (!chipScannerConnector.processAnswer(data, string.Empty, ref note, ref procData))
                {
                    MessageBox.Show("Nepodařilo se zpracovat zprávu od scanner čipů!\n\nDodatečné informace: " + note, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Zpracovani kodu probehlo uspesne, provereni vuci databazi
                else
                {
                    //Ziskani odpovidajicich informaci
                    string ID, PWD;
                    validateChipCode(procData, out ID, out PWD);
                    //Zobrazeni
                    txtID.Text = ID;
                    txtPWD.Text = PWD;
                    //Ukonceni dialogu
                    //INFO: program pokracuje standardni cestou ve funkci LogIn v Config.cs
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepodařilo se zpracovat zprávu od scanner čipů!\n\n" + ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.WriteException(ex);
            }
        }

        //Overeni caroveho kodu vuci databazi
        private void validateBArcode(string procData, out string id, out string pass)
        {
            //Reader
            SqlDataReader reader = null;

            try
            {
                //Pripojeni  .. 
                SqlConnection conn = new SqlConnection(LogConfig.SqlConnectionStringLocal);
                conn.Open();

                //.. a prikaz
                string cmdText = "select * from fask_logins where barcode = @barcode";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@barcode", procData));

                //Vykonani
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable table = new DataTable();
                table.Load(reader);

                //Zpracovani
                id = table.Rows[0]["id"].ToString().Trim();
                pass = table.Rows[0]["psswd"].ToString().Trim();
            }
            catch (Exception ex)
            {
                //Pokud doslo nekde k chybe, nastavim id a pass na prazdny string
                id = pass = string.Empty;
                //Pro jistotu
                Log.WriteException(ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        //Overeni kodu cipu vuci databazi
        private void validateChipCode(string procData, out string id, out string pass)
        {
            //Reader
            SqlDataReader reader = null;

            try
            {
                //Pripojeni  .. 
                SqlConnection conn = new SqlConnection(LogConfig.SqlConnectionStringLocal);
                conn.Open();

                //.. a prikaz
                string cmdText = "select * from fask_logins where rfid = @rfid";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@rfid", procData));

                //Vykonani
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable table = new DataTable();
                table.Load(reader);

                //Zpracovani
                id = table.Rows[0]["id"].ToString().Trim();
                pass = table.Rows[0]["psswd"].ToString().Trim();
            }
            catch (Exception ex)
            {
                //Pokud doslo nekde k chybe, nastavim id a pass na prazdny string
                id = pass = string.Empty;
                //Pro jistotu
                Log.WriteException(ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        //Nastaveni vstupniho portu - scanner
        private void SerialPortINSetup()
        {
            try
            {
                //Pro scanner
                if (serialPortIN.IsOpen) serialPortIN.Close();

                serialPortIN.BaudRate = int.Parse(LogConfig.config.Scanner[0].SP_IN_BaudRate);
                serialPortIN.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), LogConfig.config.Scanner[0].SP_IN_Parity);
                serialPortIN.PortName = LogConfig.config.Scanner[0].SP_IN_PortName;
                serialPortIN.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), LogConfig.config.Scanner[0].SP_IN_StopBits);
                serialPortIN.DataBits = int.Parse(LogConfig.config.Scanner[0].SP_IN_DataBits);

                serialPortIN.Open();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                MessageBox.Show(ex.Message, ex.Source);
            }

        }

        //Nastaveni vstupniho portu - ctecka
        private void SerialPortIN2Setup()
        {
            try
            {
                //Pro ctecku cipu
                if (serialPortIN2.IsOpen) serialPortIN2.Close();

                serialPortIN2.BaudRate = int.Parse(LogConfig.config.ChipScanner[0].SP_IN_BaudRate);
                serialPortIN2.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), LogConfig.config.ChipScanner[0].SP_IN_Parity);
                serialPortIN2.PortName = LogConfig.config.ChipScanner[0].SP_IN_PortName;
                serialPortIN2.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), LogConfig.config.ChipScanner[0].SP_IN_StopBits);
                serialPortIN2.DataBits = int.Parse(LogConfig.config.ChipScanner[0].SP_IN_DataBits);

                serialPortIN2.Open();

                //INFO: pro spravnou funkci ctecky cipu je nutne nastavit spravne kodovani!
                //KODOVANI
                serialPortIN2.Encoding = System.Text.Encoding.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
                Log.WriteException(ex);
            }

            try
            {
                //Dynamicke vytvoreni objektu pro ctecku cipu
                chipScannerConnector = createChipScannerObject(LogConfig.config.ChipScanner[0].Assembly, LogConfig.config.ChipScanner[0].Object);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepodařilo se načíst knihovnu pro scanner čipů. Scanner nebude pracovat správně!\n\n" + ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.WriteException(ex);
            }
        }

        //Dynamicke vytvoreni objektu pro ctecku cipu
        private IChipScannersConnector createChipScannerObject(string ass, string obj)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string binPath = (new Uri(Path.GetDirectoryName(executingAssembly.CodeBase))).AbsolutePath;
            Assembly assembly = Assembly.LoadFrom(Path.Combine(binPath, ass));
            return assembly.CreateInstance(obj) as IChipScannersConnector;
        }

        //Inicializace portu pro prihlasovani
        private int method = 0;

        private void InitializePorts()
        {
            //Port pro scanner
            if (LogConfig.config.Scanner.Rows.Count == 0)
            {
                LogConfig.config.Scanner.AddScannerRow(
                serialPortIN.PortName, serialPortIN.DataBits.ToString(), serialPortIN.Parity.ToString(), serialPortIN.StopBits.ToString(), serialPortIN.BaudRate.ToString());
                LogConfig.Save();
            }

            //Nastaveni - scanner
            method = 0;
            try { method = int.Parse(LogConfig.config.LoginMetod[0].Method); }
            catch { method = 0; }

            //JoZ 28-1-2013: prihlaseni zadanym uzivatelem
            if (method == 4)
            {
                txtID.Text = LogConfig.config.LoginMetod[0].Login;
                txtPWD.Text = LogConfig.config.LoginMetod[0].Password;
            }

            //Nacitat scannerem, nebo obojim
            if (method == 1 || method == 3) SerialPortINSetup();

            //Port pro ctecku cipu
            if (LogConfig.config.ChipScanner.Rows.Count == 0)
            {
                //TODO: tady bude muset byt jiny port, druhy, pridany.
                LogConfig.config.ChipScanner.AddChipScannerRow(
                serialPortIN2.PortName, serialPortIN2.DataBits.ToString(), serialPortIN2.Parity.ToString(), serialPortIN2.StopBits.ToString(), serialPortIN2.BaudRate.ToString(), string.Empty, string.Empty);
                LogConfig.Save();
            }

            //Nastaveni - ctecka
            //Nacitat cteckou, nebo obojim
            if (method == 2 || method == 3) SerialPortIN2Setup();
        }

        private void frmLogin1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new Point(0, 0);
            this.Bounds = Screen.GetBounds(new Point(0, 0));
            this.WindowState = FormWindowState.Maximized;
            panelButtons_Resize(null, null);
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }
    }
}
