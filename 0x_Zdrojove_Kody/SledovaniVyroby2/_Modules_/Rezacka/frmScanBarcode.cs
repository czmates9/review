using System;
using System.Windows.Forms;
using System.IO.Ports;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using ICommDatabase;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    /// <summary>
    /// Scanovani dodatecnych carovych kodu
    /// </summary>
    public partial class frmScanBarcode : Form
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public frmScanBarcode(SerialPort port)
        {
            InitializeComponent();
            initPort(port);
            setPosition();
        }

        /// <summary>
        /// Nastavi pozici okna na stred rodicovskeho
        /// </summary>
        private void setPosition()
        {
            StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Inicializuje port
        /// </summary>
        /// <param name="port">port</param>
        private void initPort(SerialPort port)
        {
            serialPortIN.Parity = port.Parity;
            serialPortIN.PortName = port.PortName;
            serialPortIN.BaudRate = port.BaudRate;
            serialPortIN.StopBits = port.StopBits;
            serialPortIN.DataBits = port.DataBits;
        }

        /// <summary>
        /// Pozadovana delka snimaneho CK
        /// </summary>
        public int BarcodeLength{ get; set; }

        //Prijem dat - carovy kod
        private void BarcodeReceived(string data)
        {
            //Log-event
            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.BarCodeRead, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            //Kontrola
            if (checkBarcode(data))
            {
                //Zobrazeni
                txtBarcode.Text = data;
                //Ok
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Kontrola caroveho kodu na delku
        /// </summary>
        /// <param name="data">nacteny kod</param>
        /// <returns>Uspech, neuspech</returns>
        private bool checkBarcode(string data)
        {
            //INFO: prvni uprava.
            //Pokud je zadana delka 255, nemusi se overovat, ale nesmi byt prazdna
            if (BarcodeLength == 255 && data.Length == 0)
            {
                DialogResult result = MessageBox.Show("Načtený čárový kód je prázdný. Přejete přesto pokračovat?", "Snímání čárového kódu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }
            else if (BarcodeLength != 255 && data.Length != BarcodeLength)
            {
                DialogResult result = MessageBox.Show("Načtený čárový kód " + data + " nemá požadovanou délku " + BarcodeLength + " znaků. Přejete přesto pokračovat?", "Snímání čárového kódu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }
            
            //Ok
            return true;
        }

        /// <summary>
        /// Delegat pro prijem na portu
        /// </summary>
        /// <param name="data">Data = barcode</param>
        delegate void DataReceivedDelegate(string data);

        /// <summary>
        /// Prijem na seriovem portu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void serialPortIN_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            //Nacteni dat
            string data = string.Empty;

            try
            {
                try { data = serialPortIN.ReadLine(); }
                catch { data = serialPortIN.ReadExisting(); }
            }
            //INFO: Pokud uz byl kod nacten -> dialog uzavren -> port uzavren, tak dochazi k vyjimce kvuli zavrenemu portu
            catch { return; }

            //Volani funkce formu pro zobrazeni
            Invoke(new DataReceivedDelegate(BarcodeReceived), data.Trim());
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void btnOk_Click_1(object sender, EventArgs e)
        {
            if(checkBarcode(txtBarcode.Text.Trim()))
            {
                //Ok
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Vrati nacteny carovy kod
        /// </summary>
        public string Barcode
        {
            get { return txtBarcode.Text.Trim(); }
        }

        /// <summary>
        /// Zobrazeni popisku
        /// </summary>
        public string LabelMessage
        {
            set { lblInfo.Text = value; }
        }

        /// <summary>
        /// Nacteni formu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void frmScanBarcode_Load(object sender, EventArgs e)
        {
            //Pri nacteni vzdy vymazu predchozi kod..
            txtBarcode.Text = string.Empty;
            //Otevru port
            if (serialPortIN != null && !serialPortIN.IsOpen) serialPortIN.Open();
        }

        /// <summary>
        /// Uzavreni formu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void frmScanBarcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPortIN != null && serialPortIN.IsOpen) serialPortIN.Close();
        }
    }
}
