using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModuleSql.Classes
{
    public class StatusInfo_Dispo
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Exception InnerException { get; set; }

        public DataTable dt { get; set; }

        public StatusInfo_Dispo()
        {
            this.Created = DateTime.Now;
        }

        public StatusInfo_Dispo(int id)
            : this()
        {
            this.ID = id;
        }

        public StatusInfo_Dispo(int id, string desc)
            : this(id)
        {
            this.Description = desc;
        }

        public StatusInfo_Dispo(int id, string desc, DataTable DT)
            : this(id)
        {
            this.Description = desc;
            this.dt = DT;
        }

        public StatusInfo_Dispo(int id, string desc, Exception innerex)
            : this(id, desc)
        {
            this.InnerException = innerex;
        }

        public StatusInfo_Dispo(int id, string desc, Exception innerex, DateTime created)
            : this(id, desc, innerex)
        {
            this.Created = created;
        }
    }
}
