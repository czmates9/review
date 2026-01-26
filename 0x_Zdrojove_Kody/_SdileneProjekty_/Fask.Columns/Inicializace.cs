using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public sealed class Inicializace
	{
		//Fask.Columns.Init.Vydej.

		public static Inicializace InitInstance = null;

		public Zbozi Zbozi = null;
		public Vyroba Vyroba = null;
		public Vydej Vydej = null;
		public Uzivatele Uzivatele = null;
		public Ukoly Ukoly = null;
		public TypDokladu TypDokladu = null;
		public Tiskarny Tiskarny = null;
		public Strediska Strediska = null;
		public Sklady Sklady = null;
		public Servis_ZdrojeStav Servis_ZdrojeStav = null;
		public Servis_ZdrojePohyb Servis_ZdrojePohyb = null;
		public Servis_Ciselniky Servis_Ciselniky = null;
		public Prodej Prodej = null;
		public Prijem Prijem = null;
		public Pracovnici Pracovnici = null;
		public Odberatele Odberatele = null;
		public Meny Meny = null;
		public Lokace Lokace = null;
		public Inventura1 Inventura1 = null;
		public Inventura2 Inventura2 = null;
		public EventsUser EventsUser = null;
		public EventsTypes EventsTypes = null;

		public Inicializace(string Path)
		{

			DS_Information ds = Fask.Columns.CreateSQL.SQLLoad.SQLLoadData(Path);

			
			Zbozi = new Zbozi(ds);
			Vyroba = new Vyroba(ds);
			Vydej = new Vydej(ds);
			Uzivatele = new Uzivatele(ds);
			Ukoly = new Ukoly(ds);
			TypDokladu = new TypDokladu(ds);
			Tiskarny = new Tiskarny(ds);
			Strediska = new Strediska(ds);
			Sklady = new Sklady(ds);
			Servis_ZdrojeStav = new Servis_ZdrojeStav(ds);
			Servis_ZdrojePohyb = new Servis_ZdrojePohyb(ds);
			Servis_Ciselniky = new Servis_Ciselniky(ds);
			Prodej = new Prodej(ds);
			Prijem = new Prijem(ds);
			Pracovnici = new Pracovnici(ds);
			Odberatele = new Odberatele(ds);
			Meny = new Meny(ds);
			Lokace = new Lokace(ds);
			Inventura1 = new Inventura1(ds);
			Inventura2 = new Inventura2(ds);
			EventsUser = new EventsUser(ds);
			EventsTypes = new EventsTypes(ds);


		}



	}
}
