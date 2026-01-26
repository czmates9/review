using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Classes_Vyroba
{
    public class Report_UserDay
    {
        /// <summary>
        /// Pro jakeho uzivatele se vypocty provedly
        /// </summary>
        public string UserID;

        /// <summary>
        /// Seznam stroju 
        /// </summary>
        public List<string> Machines = new List<string>();

        /// <summary>
        /// Normovany cas v hodinach
        /// </summary>
        public decimal TimeNorm = 0;

        /// <summary>
        /// Skutecny cas v hodinach
        /// </summary>
        public decimal TimeReal = 0;

        /// <summary>
        /// Cas korekci v hodinach
        /// </summary>
        public decimal TimeCorrects = 0;

        /// <summary>
        /// Cas prihlaseni uzivatele
        /// </summary>
        public DateTime UserLogin;

        /// <summary>
        /// Cas posledni operace uzivatele
        /// </summary>
        public DateTime UserLastOperation;
    }
}
