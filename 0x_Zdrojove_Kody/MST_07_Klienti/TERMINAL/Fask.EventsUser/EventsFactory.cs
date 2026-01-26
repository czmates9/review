using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Events
{
    public class EventsFactory : IEvents
    {
        private static EventsFactory sharedInstance = null;
        public static EventsFactory SharedInstance
        {
            get
            {
                if (sharedInstance == null)
                    sharedInstance = new EventsFactory();
                    
                return sharedInstance;
            }
        }

        #region IEventsUser Members

        public bool synchronize()
        {
            //throw new NotImplementedException();
            return true;
        }

        public bool add(Event euser)
        {
            //throw new NotImplementedException();
            return true;
        }

        #endregion
    }
}
