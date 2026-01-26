using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRFIDProvider
{
    public enum ComPorty
    {
        COM1 = 1,
        COM2 = 2,
        COM3 = 3,
        COM4 = 4,
        COM5 = 5,
        COM6 = 6,
        COM7 = 7,
        COM8 = 8,
        COM9 = 9,
        COM10 = 10,
        COM11 = 11,
        COM12 = 12,
    }

    public enum Baudrate
    {
        _9600bps = 0,
        _19200bps = 1,
        _38400bps = 2,
        _56000bps = 4,
        _57600bps = 5,
        _115200bps = 6
    }


    public interface IRFIDProvider
    {
        void Start();
        void Stop();

        bool isOpen();

        event RFIDHandler DataReady;
    }

    public delegate void RFIDHandler(object sender, RFIDEventArgs e);


    public class RFIDEventArgs : EventArgs
    {
        public RFIDEventArgs(List<string> tagIDs)
        {
            _tagIDs = tagIDs;
        }

        private List<string> _tagIDs = null;
        public List<string> TagIDs
        {
            get { return _tagIDs; }

        }
    }


}
