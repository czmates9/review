using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.Modules
{
    public class ModulBase
    {
        private string _IP;
        public string IP
        {
            get { return _IP;}
            set { _IP = value;}
        }

        private int _PORT_P2P;
        public int PORT_P2P
        {
            get { return _PORT_P2P; }
            set { _PORT_P2P = value; }
        }

        public ModulBase(string ip)
        {
            this.IP = ip;
            this.Initialize();
        }

        public ModulBase(string ip, int port) : this(ip)
        {
            this.PORT_P2P = port;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Terminate()
        {

        }

        public virtual bool ReadStates()
        {
            return false;
        }

        public virtual bool SaveStates()
        {
            return false;
        }

    }
}
