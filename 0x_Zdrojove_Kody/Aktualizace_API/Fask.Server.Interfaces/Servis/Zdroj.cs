using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Servis
{
    public class Zdroj
    {
        public string IDZdroj;
        public string IDStav;
        public string IDCinnost;
        public DateTime Modified;
        public byte IDTerminal;
        public int IDUser;
        public Guid GUID;
        public string CinnostValue;
        public string CinnostType;
        public int? CountEntries;
        public string ODB_ID;
        public string OkruhID;
        public string CinnostOznaceni;
        public double? GPS_X;
        public double? GPS_Y;
        public int? GPS_Z;
    }
}
