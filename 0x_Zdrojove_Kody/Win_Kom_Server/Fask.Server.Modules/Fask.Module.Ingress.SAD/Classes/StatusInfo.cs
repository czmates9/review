using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Module.Ingres.SAD.Classes
{
    public class StatusInfo
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Exception InnerException { get; set; }

        public StatusInfo()
        {
            this.Created = DateTime.Now;
        }

        public StatusInfo(int id)
            : this()
        {
            this.ID = id;
        }

        public StatusInfo(int id, string desc)
            : this(id)
        {
            this.Description = desc;
        }

        public StatusInfo(int id, string desc, Exception innerex)
            : this(id, desc)
        {
            this.InnerException = innerex;
        }

        public StatusInfo(int id, string desc, Exception innerex, DateTime created)
            : this(id, desc, innerex)
        {
            this.Created = created;
        }
    }
}
