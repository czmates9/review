using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Drawing.Printing;
using System.ComponentModel;
using Fask.Logging;
using Fask.MyPath;


namespace MST_Print_Server_ZPL_Printing
{
    public class RAW_Printing
    {
        private string jobName = "MST Print Server job";
        public string JobName
        {
            get { return jobName; }
            set { jobName = value; }
        }

        private TiskParams printParams = null;
        /// <summary>
        /// parametre tisku
        /// </summary>
        public TiskParams PrintParams
        {
            get { return printParams; }
            set { printParams = value; }
        }

        public int PrintEncodingPage { get; set; }

        public int TIMEOUT = 0;
        //pocet vytisknutych do timeoutu
        public int COUNTS_TO_TIMEOUT = 200;

        #region Structure and API declarions:

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        [DllImport("winspool.Drv", EntryPoint = "ReadPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ReadPrinter(IntPtr hPrinter, IntPtr pBuf, Int32 cbBuf, ref Int32 pNoBytesRead);

        #endregion


        public bool PrintName(string msg)
        {
            string debugprintlabel = string.Empty;
            StringBuilder logstring = new StringBuilder();
            

            //byte[] sourcebytes = Encoding.Default.GetBytes(msg);
            //byte[] destinbytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(PrintEncodingPage), sourcebytes);
            byte[] destinbytes = Encoding.GetEncoding(PrintEncodingPage).GetBytes(msg);
			// \TODO: test

            // From byte array to string
            //string s = Encoding.GetEncoding(PrintEncodingPage).GetString(destinbytes, 0, destinbytes.Length);
            //byte[] testbytes = Encoding.Convert(Encoding.GetEncoding(PrintEncodingPage), Encoding.UTF8, destinbytes);
            //string pomm = Encoding.UTF8.GetString(testbytes);

            // konec test
			if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
            {
                debugprintlabel = "PL_Label_" + Guid.NewGuid().ToString() + ".prn";

                //StreamWriter sw = new StreamWriter(Path.Combine(Log.Directory, debugprintlabel));
                //sw.Write(msg);
                //sw.Flush();
                //sw.Close();

                if (!Directory.Exists(Fask.MyPath.Path.PrintLogDirectory))
                    Directory.CreateDirectory(Fask.MyPath.Path.PrintLogDirectory);


                FileStream sw = new FileStream(System.IO.Path.Combine(Fask.MyPath.Path.PrintLogDirectory , debugprintlabel), FileMode.Create);
                sw.Write(destinbytes, 0, destinbytes.Length);
                sw.Flush();
                sw.Close();


                logstring.AppendLine("========== Printers =========");
                foreach (string pname in PrinterSettings.InstalledPrinters)
                {
                    PrinterSettings ps = new PrinterSettings();
                    ps.PrinterName = pname;
                    bool same = pname == this.printParams.CONFIG_NAME;
                    logstring.AppendLine(pname + " : Valid=" + ps.IsValid + ", ==" + same);
                }
            }
            bool pom = SendStringToPrinter2(printParams.CONFIG_NAME, destinbytes);

			if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
            {
                logstring.AppendLine(debugprintlabel + ", printed: " + pom + ", printer: " + printParams.CONFIG_NAME);

				Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo ,logstring.ToString());
                //Log.Write(logstring.ToString());
            }

            return pom;
        }

        /// <summary>
        /// Tlac cez IP, verzia 1
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="zaznamy"></param>
        /// <returns></returns>
        public bool PrintIP(string[] SN, string msg)
        {
            TcpClient printer = null;
            NetworkStream strm = null;
            bool succed = false;

            try
            {
                string zkouska = msg;

                printer = new TcpClient(printParams.CONFIG_IP, Convert.ToInt32(printParams.CONFIG_IP_PORT));
                strm = printer.GetStream();

                byte[] sendBytes = Encoding.ASCII.GetBytes(zkouska);
                int strlength = zkouska.Length;
                strm.Write(sendBytes, 0, strlength);

                for (int i = 0; i < SN.Length - 1; i++)
                {
                    zkouska = zkouska.Replace(SN[i], SN[i + 1]);
                    sendBytes = Encoding.ASCII.GetBytes(zkouska);
                    strlength = zkouska.Length;
                    strm.Write(sendBytes, 0, strlength);

                    //po COUNTS_TO_TIMEOUT stitcich pockame 2 sekundy
                    if ((i + 1) % COUNTS_TO_TIMEOUT == 0)
                        System.Threading.Thread.Sleep(TIMEOUT);
                }

                strm.Close();
                printer.Close();
                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return succed;
        }


        /// <summary>
        /// Tlac cez IP, verzia 1
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="zaznamy"></param>
        /// <returns></returns>
        public bool PrintIP(string msg)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(msg);
            return PrintIP(sendBytes);
            //return PrintIPbyName(msg);
        }

        /// <summary>
        /// Tlac cez IP, binarne data
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool PrintIP(byte[] msg)
        {
            TcpClient printer = null;
            NetworkStream strm = null;
            bool succed = false;

            try
            {
                printer = new TcpClient(printParams.CONFIG_IP, Convert.ToInt32(printParams.CONFIG_IP_PORT));
                strm = printer.GetStream();

                int strlength = msg.Length;
                strm.Write(msg, 0, strlength);

                strm.Close();
                printer.Close();
                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return succed;
        }

        /// <summary>
        /// Tlac cez IP, verzia 2
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="zaznamy"></param>
        /// <returns></returns>
        public bool PrintIP2(string[] SN, string msg)
        {
            TcpClient client = null;
            NetworkStream ns = null;
            IPEndPoint remoteIP;
            Socket sock = null;
            byte[] bytes;
            int bytesRead;
            bool succed = false;

            try
            {
                string zkouska = msg;

                remoteIP = new IPEndPoint(IPAddress.Parse(printParams.CONFIG_IP), Convert.ToInt32(printParams.CONFIG_IP_PORT));
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(remoteIP);
                ns = new NetworkStream(sock);

                if (ns.DataAvailable)
                {
                    bytes = new byte[client.ReceiveBufferSize];
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                }

                byte[] toSend = Encoding.ASCII.GetBytes(zkouska);
                ns.Write(toSend, 0, toSend.Length);

                if (ns.DataAvailable)
                {
                    bytes = new byte[client.ReceiveBufferSize];
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                }

                for (int i = 0; i < SN.Length - 1; i++)
                {
                    zkouska = zkouska.Replace(SN[i], SN[i + 1]);
                    toSend = Encoding.ASCII.GetBytes(zkouska);
                    ns.Write(toSend, 0, toSend.Length);

                    if (ns.DataAvailable)
                    {
                        bytes = new byte[client.ReceiveBufferSize];
                        bytesRead = ns.Read(bytes, 0, bytes.Length);
                    }
                    //po COUNTS_TO_TIMEOUT stitcich pockame 2 sekundy
                    if ((i + 1) % COUNTS_TO_TIMEOUT == 0)
                        System.Threading.Thread.Sleep(TIMEOUT);
                }

                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if (ns != null)
                    ns.Close();

                if (sock != null && sock.Connected)
                    sock.Close();

                if (client != null)
                    client.Close();
            }

            return succed;
        }

        /// <summary>
        /// Tlac cez IP, verzia 2
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="zaznamy"></param>
        /// <returns></returns>
        public bool PrintIP2(string msg)
        {
            TcpClient client = null;
            NetworkStream ns = null;
            IPEndPoint remoteIP;
            Socket sock = null;
            byte[] bytes;
            int bytesRead;
            bool succed = false;

            try
            {
                string zkouska = msg;

                remoteIP = new IPEndPoint(IPAddress.Parse(printParams.CONFIG_IP), Convert.ToInt32(printParams.CONFIG_IP_PORT));
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(remoteIP);
                ns = new NetworkStream(sock);

                if (ns.DataAvailable)
                {
                    bytes = new byte[client.ReceiveBufferSize];
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                }

                byte[] toSend = Encoding.ASCII.GetBytes(zkouska);
                ns.Write(toSend, 0, toSend.Length);

                if (ns.DataAvailable)
                {
                    bytes = new byte[client.ReceiveBufferSize];
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                }

                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if (ns != null)
                    ns.Close();

                if (sock != null && sock.Connected)
                    sock.Close();

                if (client != null)
                    client.Close();
            }

            return succed;
        }

        /// <summary>
        /// Tlac cez IP, verzia 3 ( zebra web )
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="zaznamy"></param>
        /// <returns></returns>
        public bool PrintIP3(string[] SN, string msg)
        {   //upraveny z orig. webu Zebra
            bool succed = false;
            System.Net.Sockets.TcpClient Zebraclient = new TcpClient();

            try
            {
                Zebraclient.SendTimeout = 1500;
                Zebraclient.ReceiveTimeout = 1500;
                Zebraclient.Connect(printParams.CONFIG_IP, Convert.ToInt32(printParams.CONFIG_IP_PORT));
                //Zebraclient.Connect("10.17.50.202",9100);   
            }
            catch
            {
                //MessageBox.Show("Not connected, verify connection");
                return false;
            }
            if (Zebraclient.Connected == true)
            {
                NetworkStream mynetworkstream;
                StreamReader mystreamreader;
                StreamWriter mystreamwriter;
                mynetworkstream = Zebraclient.GetStream();
                mystreamreader = new StreamReader(mynetworkstream);
                mystreamwriter = new StreamWriter(mynetworkstream);


                string zkouska = msg;
                mystreamwriter.WriteLine(zkouska);
                mystreamwriter.Flush();
                char[] mk = null;
                mk = new char[20];
                mystreamreader.Read(mk, 0, mk.Length);

                for (int i = 0; i < SN.Length - 1; i++)
                {
                    zkouska = zkouska.Replace(SN[i], SN[i + 1]);
                    mystreamwriter.WriteLine(zkouska);
                    mystreamwriter.Flush();
                    mk = null;
                    mk = new char[20];
                    mystreamreader.Read(mk, 0, mk.Length);

                    //po COUNTS_TO_TIMEOUT stitcich pockame TIMEOUT sekundy
                    if ((i + 1) % COUNTS_TO_TIMEOUT == 0)
                        System.Threading.Thread.Sleep(TIMEOUT);

                }
                //string data1 = new string(mk);
                //textBox1.Text = data1;
                Zebraclient.Close();
                succed = true;
            }

            return succed;
        }


        /// <summary>
        /// Tlac cez IP, verzia 3 ( zebra web )
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="zaznamy"></param>
        /// <returns></returns>
        public bool PrintIP3(string msg)
        {   //upraveny z orig. webu Zebra
            bool succed = false;
            System.Net.Sockets.TcpClient Zebraclient = new TcpClient();

            try
            {
                Zebraclient.SendTimeout = 1500;
                Zebraclient.ReceiveTimeout = 1500;
                Zebraclient.Connect(printParams.CONFIG_IP, Convert.ToInt32(printParams.CONFIG_IP_PORT));
                //Zebraclient.Connect("10.17.50.202",9100);   
            }
            catch
            {
                //MessageBox.Show("Not connected, verify connection");
                return false;
            }
            if (Zebraclient.Connected == true)
            {
                NetworkStream mynetworkstream;
                StreamReader mystreamreader;
                StreamWriter mystreamwriter;
                mynetworkstream = Zebraclient.GetStream();
                mystreamreader = new StreamReader(mynetworkstream);
                mystreamwriter = new StreamWriter(mynetworkstream);


                string zkouska = msg;
                mystreamwriter.WriteLine(zkouska);
                mystreamwriter.Flush();
                char[] mk = null;
                mk = new char[20];
                mystreamreader.Read(mk, 0, mk.Length);

                //string data1 = new string(mk);
                //textBox1.Text = data1;
                Zebraclient.Close();
                succed = true;
            }

            return succed;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Microsoft.Win32.SafeHandles.SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess,
        uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition,
        uint dwFlagsAndAttributes, IntPtr hTemplateFile);


        public bool PrintLPT(string[] SN, string msg)
        {
            bool succed = false;

            try
            {

                string command = msg;

                Byte[] buffer = new byte[command.Length];

                buffer = System.Text.Encoding.ASCII.GetBytes(command.ToString());

                Microsoft.Win32.SafeHandles.SafeFileHandle printer = CreateFile("LPT1:", FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
                if (printer.IsInvalid == true)
                {
                    return succed;
                }

                FileStream lpt1 = new FileStream(printer, FileAccess.ReadWrite);
                lpt1.Write(buffer, 0, buffer.Length);


                for (int i = 0; i < SN.Length - 1; i++)
                {
                    command = command.Replace(SN[i], SN[i + 1]);
                    buffer = null;
                    buffer = new byte[command.Length];
                    buffer = System.Text.Encoding.ASCII.GetBytes(command.ToString());
                    lpt1.Write(buffer, 0, buffer.Length);
                }

                lpt1.Close();
                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return succed;
        }

        public bool PrintLPT(string msg)
        {
            bool succed = false;

            try
            {

                string command = msg;

                Byte[] buffer = new byte[command.Length];

                buffer = System.Text.Encoding.ASCII.GetBytes(command.ToString());

                Microsoft.Win32.SafeHandles.SafeFileHandle printer = CreateFile("LPT1:", FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
                if (printer.IsInvalid == true)
                {
                    return succed;
                }

                FileStream lpt1 = new FileStream(printer, FileAccess.ReadWrite);
                lpt1.Write(buffer, 0, buffer.Length);


                lpt1.Close();
                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return succed;
        }

        public bool PrintCOM(string[] SN, string msg)
        {
            bool succed = false;
            try
            {
                System.IO.Ports.SerialPort comPort1 = new System.IO.Ports.SerialPort(printParams.CONFIG_COM, 57600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                comPort1.Open();

                string zkouska = msg;
                comPort1.WriteLine(zkouska);

                for (int i = 0; i < SN.Length - 1; i++)
                {
                    zkouska = zkouska.Replace(SN[i], SN[i + 1]);
                    comPort1.WriteLine(zkouska);

                    //po COUNTS_TO_TIMEOUT stitcich pockame TIMEOUT sekundy
                    if ((i + 1) % COUNTS_TO_TIMEOUT == 0)
                        System.Threading.Thread.Sleep(TIMEOUT);
                }

                comPort1.Close();

                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return succed;
        }

        public bool PrintCOM(string msg)
        {
            bool succed = false;
            try
            {
                System.IO.Ports.SerialPort comPort1 = new System.IO.Ports.SerialPort(printParams.CONFIG_COM, 57600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                comPort1.Open();

                string zkouska = msg;
                comPort1.WriteLine(zkouska);

                comPort1.Close();

                succed = true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return succed;
        }

        /// <summary>
        /// When the function is given a printer name and an unmanaged array
        /// of bytes, the function sends those bytes to the print queue.
        /// </summary>
        /// <param name="szPrinterName"></param>
        /// <param name="pBytes"></param>
        /// <param name="dwCount"></param>
        /// <returns>Returns true on success, false on failure.</returns>
        public bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.
            di.pDocName = jobName;
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }

            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
                Win32Exception win32ex = new Win32Exception(dwError);
                //Log.writeErrorData("Print error : " + dwError.ToString() + ", Writen bytes: " + dwWritten + ", " + win32ex.Message, "SendBytesToPrinter");
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Print error : " + dwError.ToString() + ", Writen bytes: " + dwWritten + ", " + win32ex.Message);
                throw new Win32Exception(dwError);
            }
            else //bSuccess == true
            {
				if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    //Log.writeErrorData("Sended bytes: " + dwCount + ", " + "Writen bytes: " + dwWritten, "SendBytesToPrinter");
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "Sended bytes: " + dwCount + ", " + "Writen bytes: " + dwWritten);
            }
        }

            return bSuccess;
        }

        /// <summary>
        /// Send file to printer
        /// </summary>
        /// <param name="szPrinterName"></param>
        /// <param name="szFileName"></param>
        /// <returns>Returns true on success, false on failure.</returns>
        public bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);

            if (br != null)
            {
                br.Close();
                br = null;
            }
            if (fs != null)
            {
                fs.Close();
                fs = null;
            }

            return bSuccess;
        }

        /// <summary>
        /// Send string to printer
        /// </summary>
        /// <param name="szPrinterName"></param>
        /// <param name="szString"></param>
        /// <returns>Returns true on success, false on failure.</returns>
        public bool SendStringToPrinter2(string szPrinterName, byte[] destinbytes)
        {

            bool succed = false;
            IntPtr pBytes;
            Int32 dwCount;
            //// How many characters are in the string?
            //dwCount = szString.Length;
            //// dwCount = (szString.Length + 1) * Marshal.SystemMaxDBCSCharSize;

            //// Assume that the printer is expecting ANSI text, and then convert
            //// the string to ANSI text.
            //pBytes = Marshal.StringToCoTaskMemAnsi(szString);

            pBytes = Marshal.AllocHGlobal(destinbytes.Length);
            dwCount = destinbytes.Length;
            Marshal.Copy(destinbytes, 0, pBytes, dwCount);
            
            
            // Send the converted ANSI string to the printer.
            succed = SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            
            //Marshal.FreeCoTaskMem(pBytes);
            // Call unmanaged code
            Marshal.FreeHGlobal(pBytes);
            
            return succed;
        }

        /// <summary>
        /// Send string to printer
        /// </summary>
        /// <param name="szPrinterName"></param>
        /// <param name="szString"></param>
        /// <returns>Returns true on success, false on failure.</returns>
        public bool SendStringToPrinter(string szPrinterName, string szString)
        {

            bool succed = false;
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // dwCount = (szString.Length + 1) * Marshal.SystemMaxDBCSCharSize;

            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);


            // Send the converted ANSI string to the printer.
            succed = SendBytesToPrinter(szPrinterName, pBytes, dwCount);

            Marshal.FreeCoTaskMem(pBytes);

            return succed;
        }
    }
}
