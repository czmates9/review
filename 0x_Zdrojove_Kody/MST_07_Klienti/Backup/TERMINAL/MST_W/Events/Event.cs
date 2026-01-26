using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Events
{
    public class Event
    {

        public const string etype_print = "print";
        //public const string etype_useraction = "useraction";

        public Guid eguid;
        public string eid;
        public string etype;
        public DateTime etime;
        public int termid;
        public int userid;
        public string loginid;
        public string machineid;
        public string modul;
        public int? countentries;
        public string docnmbr;
        public string itemnmbr;
        public string REZ1;
        public string REZ2;

        public Event()
        {
        }
        public Event(Guid eguid, string eid, string etype, DateTime etime, int termid, int userid)
            : this()
        {
            this.eguid = eguid;
            this.eid = eid;
            this.etype = etype;
            this.etime = etime;
            this.termid = termid;
            this.userid = userid;
        }
        public Event(Guid eguid, string eid, string etype, DateTime etime, int termid, int userid, string loginid, string machineid, string modul, int? countentries, string docnmbr, string itemnmbr, string REZ1, string REZ2
            )
            : this(eguid, eid, etype, etime, termid, userid)
        {
            this.loginid = loginid;
            this.machineid = machineid;
            this.modul = modul;
            this.countentries = countentries;
            this.docnmbr = docnmbr;
            this.itemnmbr = itemnmbr;
            this.REZ1 = REZ1;
            this.REZ2 = REZ2;
        }

        //public Event(Guid eguid, string eid, string etype, DateTime etime, int termid, int userid, string loginid, string machineid, string modul, string countentries_str, string docnmbr, string itemnmbr, string REZ1, string REZ2
        //    )
        //    : this(eguid, eid, etype, etime, termid, userid)
        //{
        //    this.loginid = loginid;
        //    this.machineid = machineid;
        //    this.modul = modul;
        //    this.countentries = 0;
        //    try { this.countentries = int.Parse(countentries_str); }
        //    catch { }
        //    this.docnmbr = docnmbr;
        //    this.itemnmbr = itemnmbr;
        //    this.REZ1 = REZ1;
        //    this.REZ2 = REZ2;
        //}


    }
}
