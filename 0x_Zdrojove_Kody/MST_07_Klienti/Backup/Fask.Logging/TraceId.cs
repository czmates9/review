using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.Logging
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
        /// Název metody (nevyplneno = -1)
        /// </summary>
        public string MetodaName { get; set; }


        /// <summary>
        /// Lze pouzit pro ulozeni contextu idcka ... (Nazev formu pro dohledani)
        /// </summary>
        public string FormName { get; set; }



        public DateTime TimeStart { get; set; }

        public DateTime TimeStop { get; set; }

        public int TickStart { get; set; }

        public int TickStop { get; set; }


        public TracId()
        {            
        }


        // TrackID // "UserID", "TerminalID", "Form", "Nazev metody" , "davka",  generuje sa >> "GUID"
        public TracId(int? UserId, int? TerminalId, int? Davka, string FormName, string MetodaName)
            : this()
        {
            Start();
            this.UserId = UserId;
            this.TerminalId = TerminalId;
            this.Davka = Davka;
            this.FormName = FormName;
            this.MetodaName = MetodaName;


        }

        private void Start()
        {
            this.TimeStart = DateTime.Now;
            this.TickStart = Environment.TickCount;
        }

        public void Stop() 
        {
            this.TimeStop = DateTime.Now;
            this.TickStop = Environment.TickCount;
        
        }

        public void StopToStart() 
        {
            this.TickStart = this.TickStop;
            this.TimeStart = this.TimeStop;
        }


    }
}
