using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.SystemTime
{
    public interface IMethods
    {
        bool Synchronize();
        DateTime GetDateTimeFromServer();
        bool SetDateTime(DateTime dt);
        //void LoadConfig();
        void SaveConfig();
    }
}
