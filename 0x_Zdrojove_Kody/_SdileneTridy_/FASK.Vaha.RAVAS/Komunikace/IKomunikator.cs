using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Vaha.RAVAS.Komunikace
{
    internal interface IKomunikator
    {

        void ConnetToServer(string ipAddress, int port);

        void Disconnect();

        bool IsConnected();

        void Write_RAW_Data(byte[] data);

        (byte[], int) Read_RAW_Data();

        ReadData Read_Data();

    }

    public class ReadData
    {
        private int _length;
        public int Length
        {
            set { _length = value; }
            get { return _length; }
        }

        private byte[] _data;
        public byte[] Data
        {
            set { _data = value; }
            get { return _data; }
        }
        
        public ReadData()
        {
            Data = new byte[512];
            _length = -1;
        }
        

        public string GetDataASCII()
        {
            return System.Text.Encoding.ASCII.GetString(_data, 0, _length);
        }
    }

    public class WeightdData
    {
        private int? _net;
        public int? NET
        {
            set { _net = value; }
            get { return _net; }
        }

        private int? _gross;
        public int? GROSS
        {
            set { _gross = value; }
            get { return _gross; }
        }

        private string _Message;
        public string Message
        {
            set { _Message = value; }
            get { return _Message; }
        }

        private bool _exception = false;
        public bool Exception
        {
            set { _exception = value; }
            get { return _exception; }
        }

        public WeightdData()
        {

        }


        
    }
}
