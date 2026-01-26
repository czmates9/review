using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Tracing
{
    public class TracId
    {
        // jedinecny identifikator ...
        private Guid identificator = Guid.NewGuid();
        /// <summary>
        /// Jedinecny identifikator tracid objektu
        /// </summary>
        public Guid Identificator { get { return identificator; } }

        /// <summary>
        /// ID uzivatele (nevyplneno = -1)
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Terminal ID (nevyplneno = -1)
        /// </summary>
        public int? TerminalId { get; set; }
        /// <summary>
        /// Cislo davky (nevyplneno = -1)
        /// </summary>
        public int? Davka { get; set; }

        /// <summary>
        /// Lze pouzit pro ulozeni contextu idcka ...
        /// </summary>
        public string Context { get; set; }

        public TracId()
        {            
        }

        public TracId(int? UserId, int? TerminalId, int? Davka)
            : this()
        {
            this.UserId = UserId;
            this.TerminalId = TerminalId;
            this.Davka = Davka;
        }

        public TracId(int? UserId, int? TerminalId, int? Davka, string Context)
            : this(UserId, TerminalId, Davka)
        {
            this.Context = Context;
        }

        public override string ToString()
        {
            return "ID:" + this.Identificator.ToString("B") + " U:" + (UserId ?? -1) + ",T:" + (TerminalId ?? -1) + ",D:" + (Davka ?? -1);
        }
    }
}
