using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FASK.Vaha.RAVAS
{
    public class RAVAS_3200_Client
    {

        #region Parametry

        private Komunikace.IKomunikator _komunikator = null;

        public bool IsConnect
        {
            get
            {
                if (_komunikator == null)
                    return false;
                else
                    return _komunikator.IsConnected();
            }
        }

        private int _port;
        public int Port
        {
            set { _port = value; }
            get { return _port; }
        }

        private string _ip;
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }

        #endregion

        #region Eventy contunuous

        #region GROSS

        public event ClientHandleWeightGROSS OnDataReceived_Gross;
        public delegate void ClientHandleWeightGROSS(decimal? data);

        private Thread t_GROSS;
        public bool Started_gross = false;

        #endregion

        #region NET

        public event ClientHandleWeightNET OnDataReceived_Net;
        public delegate void ClientHandleWeightNET(decimal? data);

        private Thread t_NET;
        public bool Started_net = false;

        #endregion

        #region WEIGHT

        public event ClientHandleWeightWEIGHT OnDataReceived_Weight;
        public delegate void ClientHandleWeightWEIGHT(Komunikace.WeightdData data);

        private Thread t_WEIGHT;
        public bool Started_weight = false;

        #endregion

        #endregion

        #region c'tor

        public RAVAS_3200_Client()
        {
            _komunikator = new Komunikace.TCP_Client();
        }


        public RAVAS_3200_Client(string IP, int port) : this()
        {


            _komunikator = new Komunikace.TCP_Client();
            _komunikator.ConnetToServer(IP, port);
        }

        #endregion

        #region InterFace metody

        public void Connect()
        {
            _komunikator.ConnetToServer(_ip, _port);
        }

        public void Disconnect()
        {
            _komunikator.Disconnect();
        }

        public string RW_Data(string message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            _komunikator.Write_RAW_Data(data);
            var data_out = _komunikator.Read_Data();

            return data_out.GetDataASCII();
        }

        #endregion

        #region public Konkretni přikazy

        public decimal? Get_Tare()
        {
            return Get_Tare_Gross_Net(Constants.Common.COMMAND_GT, "T");
        }

        public decimal? Get_Gross()
        {
            return Get_Tare_Gross_Net(Constants.Common.COMMAND_GG, "G");
        }

        public decimal? Get_Net()
        {
            return Get_Tare_Gross_Net(Constants.Common.COMMAND_GN, "N");
        }

        public Komunikace.WeightdData Get_Weight()
        {
            var x = Get_Command(Constants.Common.COMMAND_GW);
            return Parse_WEIGHT_Value(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>OK / ERR</returns>
        public string Set_ZeroValue()
        {
            var x = Get_Command(Constants.Common.COMMAND_SZ);
            var status = x.Replace("\r", "");
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>OK / ERR</returns>
        public string Reset_ZeroValue()
        {
            var x = Get_Command(Constants.Common.COMMAND_RZ);
            var status = x.Replace("\r", "");
            return status;
        }

        /// <summary>
        /// Rozsah 0.1/0.2/0.5 >> SP0001.5<CR>
        /// Rozsah 1/2/5/10/20/50 >> SP00150.<CR>
        /// </summary>
        /// <param name="tare"></param>
        /// <returns></returns>
        public string Reset_SetTaraValue(string tare)
        {

            string com = string.Format(Constants.Common.COMMAND_SP, tare);
            var x = Get_Command(com);
            var status = x.Replace("\r", "");
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>OK / ERR</returns>
        public string Reset_PresetTare()
        {
            var x = Get_Command(Constants.Common.COMMAND_RP);
            var status = x.Replace("\r", "");
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>OK / ERR</returns>
        public string Reset_Tare()
        {
            var x = Get_Command(Constants.Common.COMMAND_RT);
            var status = x.Replace("\r", "");
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>OK / ERR</returns>
        public string Set_Tare()
        {
            var x = Get_Command(Constants.Common.COMMAND_ST);
            var status = x.Replace("\r", "");
            return status;
        }

        /// <summary>
        /// Toto je speciální příkaz tare, který se používá hlavně s aplikacemi pro výběr objednávek. 
        /// Zruší předchozí tare a nastaví novou hodnotu tare, která zahrnuje starou hodnotu tare a přidanou čistou hmotnost. 
        /// Pokud se hmotnost do 5 sekund nedostane stabilní, bude vygenerována chyba.
        /// </summary>
        /// <returns>OK / ERR</returns>
        public string Set_Tare_asPrev()
        {
            var x = Get_Command(Constants.Common.COMMAND_SR);
            var status = x.Replace("\r", "");
            return status;
        }

        public void Get_Gross_Continuosly_START()
        {
            t_GROSS = new Thread(new ThreadStart(ListenForWeight_GROSS));
            t_GROSS.Name = "Get_Gross_Continuosly";
            Started_gross = true;
            t_GROSS.Start();

            var x = Get_Command(Constants.Common.COMMAND_SG);
        }

        public void Get_Gross_Continuosly_STOP()
        {
            t_GROSS.Abort();
            Thread.Sleep(1000);
            t_GROSS = null;

            var x = Get_Command(Constants.Common.COMMAND_GG);
            Started_gross = false;
        }

        public void Get_Net_Continuosly_START()
        {
            t_NET = new Thread(new ThreadStart(ListenForWeight_NET));
            t_NET.Name = "Get_Net_Continuosly_START";
            Started_net = true;
            t_NET.Start();

            var x = Get_Command(Constants.Common.COMMAND_SN);
        }

        public void Get_Net_Continuosly_STOP()
        {
            t_NET.Abort();
            Thread.Sleep(1000);
            t_NET = null;

            var x = Get_Command(Constants.Common.COMMAND_GN);
            Started_net = false;
        }

        public void Get_Weight_Continuosly_START()
        {
            t_WEIGHT = new Thread(new ThreadStart(ListenForWeight_WEIGHT));
            t_WEIGHT.Name = "Get_Weight_Continuosly_START";
            Started_weight = true;
            t_WEIGHT.Start();

            var x = Get_Command(Constants.Common.COMMAND_SW);
        }

        public void Get_Weight_Continuosly_STOP()
        {
            t_WEIGHT.Abort();
            Thread.Sleep(1000);
            t_WEIGHT = null;

            var x = Get_Command(Constants.Common.COMMAND_GW);
            Started_weight = false;
        }

        #endregion

        #region Private pomocnne metody

        private string Get_Command(string command)
        {
            var com = command + Constants.Common.FASK_CR;

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(com);

            _komunikator.Write_RAW_Data(data);
            var data_out = _komunikator.Read_Data();

            return data_out.GetDataASCII();
        }


        private decimal? Get_Tare_Gross_Net(string command, string prefix)
        {

            var data = Get_Command(command);

            return Parse_One_Value(prefix, data);
        }

        #region Parsovani hodnot

        private decimal? Parse_One_Value(string prefix, string data)
        {
            if (data.StartsWith(prefix))
            {
                decimal dec;
                var tmp = data.Substring(1);

                var znamenko = tmp.Substring(0, 1);

                var cislo = tmp.Substring(1).Replace("\r", "");

                var style = System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands;
                var invC = System.Globalization.CultureInfo.InvariantCulture;

                if (decimal.TryParse(cislo, style, invC, out dec))
                {
                    if (znamenko == "-")
                        return -1 * dec;
                    else
                        return dec;
                }
                else
                    return null;

            }
            else
                return null;
        }

        private Komunikace.WeightdData Parse_WEIGHT_Value(string data)
        {
            Komunikace.WeightdData dataOUT = new Komunikace.WeightdData();


            try
            {

                int _NET;
                int _Gross;
               

                if (data.StartsWith("W"))
                {
                    var Odstranene_W = data.Substring(1);

                    var NET_Hodnota = Odstranene_W.Substring(0, 6);
                    var Zbytek_1 = Odstranene_W.Substring(6);
                    var Gross_Hodnota = Zbytek_1.Substring(0, 6);
                    var Zbytek_2 = Zbytek_1.Substring(6);
                    var Status_Hodnota = Zbytek_2.Substring(0, 2);
                    var Zbytek_3 = Zbytek_2.Substring(2);
                    var CheckSum_Hodnota = Zbytek_3.Substring(0, 2);
                    var Zbytek_4 = Zbytek_3.Substring(2);

                    if (Zbytek_4 != "\r")
                        throw new Exception("Neco je špatne s weight. Nekončí <CR>");


                    #region NET
                    var znamenko_NET = NET_Hodnota.Substring(0, 1);

                    if (int.TryParse(NET_Hodnota.Substring(1), out _NET))
                    {
                        if (znamenko_NET == "-")
                            dataOUT.NET = -1 * _NET;
                        else
                            dataOUT.NET = _NET;
                    }
                    else
                        return null;
                    #endregion

                    #region GROSS
                    var znamenko_GROSS = Gross_Hodnota.Substring(0, 1);

                    if (int.TryParse(Gross_Hodnota.Substring(1), out _Gross))
                    {
                        if (znamenko_GROSS == "-")
                            dataOUT.GROSS = -1 * _Gross;
                        else
                            dataOUT.GROSS = _Gross;
                    }
                    else
                        return null;
                    #endregion


                    return dataOUT;
                }
                else
                    return null;
            }
            catch (System.Exception ex)
            {
                dataOUT.Message = ex.Message;
                dataOUT.Exception = true;
                return dataOUT;
            }
        }


        #endregion

        #region Eventy pro continous snimani

        private void ListenForWeight_GROSS()
        {
            Komunikace.ReadData data_out;
            string dataString;
            decimal? dec;

            while (Started_gross)
            {

                try
                {
                    data_out = _komunikator.Read_Data();
                    dataString = data_out.GetDataASCII();
                    dec = Parse_One_Value("G", dataString);
                }
                catch
                {
                    break;
                }

                if (data_out.Length == 0)
                {
                    break;
                }

                if (OnDataReceived_Gross != null)
                {
                    //Send off the data for other classes to handle
                    OnDataReceived_Gross(dec);
                }

                Thread.Sleep(15);
            }

            Started_gross = false;
        }

        private void ListenForWeight_NET()
        {
            Komunikace.ReadData data_out;
            string dataString;
            decimal? dec;

            while (Started_net)
            {

                try
                {
                    data_out = _komunikator.Read_Data();
                    dataString = data_out.GetDataASCII();
                    dec = Parse_One_Value("N", dataString);
                }
                catch
                {
                    break;
                }

                if (data_out.Length == 0)
                {
                    break;
                }

                if (OnDataReceived_Net != null)
                {
                    //Send off the data for other classes to handle
                    OnDataReceived_Net(dec);
                }

                Thread.Sleep(15);
            }

            Started_net = false;
        }

        private void ListenForWeight_WEIGHT()
        {
            Komunikace.ReadData data_out;
            string dataString;

            while (Started_weight)
            {

                try
                {
                    data_out = _komunikator.Read_Data();
                    dataString = data_out.GetDataASCII();

                }
                catch
                {
                    break;
                }

                if (data_out.Length == 0)
                {
                    break;
                }

                var dspar =  Parse_WEIGHT_Value(dataString);
                

                if (OnDataReceived_Weight != null)
                {
                    OnDataReceived_Weight(dspar);
                }

                Thread.Sleep(15);
            }

            Started_weight = false;
        }

        #endregion

        #endregion

    }
}
