using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Třída která nese informace o stavu
	/// </summary>
    public class StatusInfo
    {
		/// <summary>
		/// ID Stavu
		/// </summary>
        public int ID { get; set; }

		/// <summary>
		/// Popis
		/// </summary>
        public string Description { get; set; }

		/// <summary>
		/// Dátum vzniku
		/// </summary>
        public DateTime Created { get; set; }

		/// <summary>
		/// Chyba, Výnimka která vznikla
		/// </summary>
        public Exception InnerException { get; set; }

		/// <summary>
		/// Implicit konstruktor
		/// </summary>
        public StatusInfo()
        {
            this.Created = DateTime.Now;
        }

		/// <summary>
		/// Konstruktor s ID
		/// </summary>
		/// <param name="id">ID Stavu</param>
        public StatusInfo(int id) : this()
        {
            this.ID = id;
        }

		/// <summary>
		/// Konstruktor s ID, poznamkou
		/// </summary>
		/// <param name="id">ID stavu</param>
		/// <param name="desc">Poznamka</param>
        public StatusInfo(int id, string desc)
            : this(id)
        {
            this.Description = desc;
        }

		/// <summary>
		/// Konstruktor s ID, poznamkou, výnimka
		/// </summary>
		/// <param name="id">ID Stavu</param>
		/// <param name="desc">Poznamka</param>
		/// <param name="innerex">Výnimka</param>
        public StatusInfo(int id, string desc, Exception innerex)
            : this(id, desc)
        {
            this.InnerException = innerex;
        }

		/// <summary>
		/// Konstruktor s ID, poznamkou, výnimka a custom datum vzniku
		/// </summary>
		/// <param name="id">ID Stavu</param>
		/// <param name="desc">Poznamka</param>
		/// <param name="innerex">Výnimka</param>
		/// <param name="created">custom datum vzniku</param>
        public StatusInfo(int id, string desc, Exception innerex, DateTime created)
            : this(id, desc, innerex)
        {
            this.Created = created;
        }
    }
}
