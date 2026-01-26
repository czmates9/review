using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Vaha.RAVAS.Komunikace
{
    internal class TCP_Client : IKomunikator
    {

        System.Net.Sockets.TcpClient tcpclient = null;
        System.Net.Sockets.NetworkStream stream = null;

        public void ConnetToServer(string ipAddress, int port)
        {

            tcpclient = new System.Net.Sockets.TcpClient();
            tcpclient.Connect(ipAddress, port);
            stream = tcpclient.GetStream();

        }

        public bool IsConnected()
        {
            if (tcpclient == null)
                return false;
            else
                return tcpclient.Connected;
        }

        public void Disconnect()
        {
            if (tcpclient == null)
            {
                return;
            }

            tcpclient.Close();
            stream.Close();

            tcpclient = null;
            stream = null;

        }

        public void Write_RAW_Data(byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }
        
        public (byte[], int) Read_RAW_Data()
        {
            var data = new Byte[512];
            Int32 bytes = stream.Read(data, 0, data.Length);
            return (data, bytes);
        }

        public ReadData Read_Data()
        {
            var ReadData = new ReadData();
            ReadData.Length = stream.Read(ReadData.Data, 0, ReadData.Data.Length);
            return ReadData;
        }


    }
}
