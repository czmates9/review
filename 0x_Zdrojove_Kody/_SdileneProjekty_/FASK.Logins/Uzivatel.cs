using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FASK.Logins
{

    public partial class Uzivatel
    {

        public Komunikace.ICommans Komunikace = null;


        public static Uzivatel Instance;
        //{
        //    get
        //    {
        //        if (Instance == null)
        //        {
        //            throw new Exception("Instance not created");
        //        }
        //        return Instance;
        //    }

        //    set { Instance = value; }
        //}


        //public static Uzivatel Instance
        //{
        //    get
        //    {
        //        if (Instance == null)
        //        {
        //            throw new Exception("Instance not created");
        //        }
        //        return Instance;
        //    }

        //    set { Instance = value; }
        //}



        private string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        private List<Classes.IPrava> ListinaPravASlobod;


        private string _firstName = null;
        public string FirstName
        {
            //set { _firstName = value; }
            get { return _firstName; }
        }


        private string _surName = null;
        public string SurName
        {
            //set { _surName = value; }
            get { return _surName; }
        }

        private string _userID = null;
        public string UserID
        {
            //set { _userID = value; }
            get { return _userID; }
        }


        private string _Heslo = null;
        public string Heslo
        {
            //set { _userID = value; }
            get { return _Heslo; }
        }


        /// <summary>
        /// Konstruktor s parametrem ConnectionString a ID u6ivatele, slouží pro ověřovaní uživatele
        /// </summary>
        /// <param name="ConnectionString">Connection String pro pripojeni do SQL databaze</param>
        public Uzivatel(string ConnectionString, string USERID, string TID, string TID_typ)
        {
            _ConnectionString = ConnectionString;

            if (ListinaPravASlobod == null)
                ListinaPravASlobod = new List<Classes.IPrava>();
            else
                ListinaPravASlobod.Clear();

            Komunikace = new Komunikace.SQL_Commans(_ConnectionString);
            InitPrava(USERID);
           
        }


		public Uzivatel(string ConnectionString)
		{
			_ConnectionString = ConnectionString;
            Komunikace = new Komunikace.SQL_Commans(_ConnectionString);
        }

        public Uzivatel(string adresa,
            string autorizace_DoAPI,
            string aliasDB,
            bool ishttps,
            int timeout,
            string USERID,
            string TID,
            string TID_typ)
        {
           // _ConnectionString = ConnectionString;

            if (ListinaPravASlobod == null)
                ListinaPravASlobod = new List<Classes.IPrava>();
            else
                ListinaPravASlobod.Clear();


            //TODO MaR rozsirit konstruktor o parametry terminalu id !!
            Komunikace = new Komunikace.API_Commans(adresa, autorizace_DoAPI, aliasDB, ishttps, timeout,  TID,  TID_typ);
            InitPrava(USERID);
            
        }


        #region OLD
        ///// <summary>
        ///// Metoda pro dotažení všech práv pro uživatele
        ///// </summary>
        ///// <param name="USERID">ID uživatele</param>
        ///// <returns>Listina práv a slobod</returns>
        ////private List<PravaPristupu> GetPrava(string USERID)
        ////{
        ////    List<PravaPristupu> pravaList = new List<PravaPristupu>();

        ////    var data = GetViewData(USERID);

        ////    if ((data != null) && (data.FASK_Logins_View_Prava.Count > 0))
        ////    {

        ////        foreach (string item in Enum.GetNames(typeof(PravaPristupu)))
        ////        {
        ////            string tmp = item.Replace("_", ".");
        ////            tmp = tmp + ".";

        ////            var PravaDT = data.FASK_Logins_View_Prava.Where(x => x.AGENDAID.Trim() == tmp.Trim());

        ////            foreach (var row in PravaDT)
        ////            {
        ////                string steckama = row.AGENDAID.Remove(row.AGENDAID.Count() - 1);
        ////                steckama = steckama.Replace(".", "_");

        ////                pravaList.Add((PravaPristupu)Enum.Parse(typeof(PravaPristupu), steckama, true));
        ////            }
        ////        }
        ////        return pravaList;
        ////    }
        ////    else
        ////        return null;
        ////} 
        #endregion


        private void InitPrava(string USERID)
        {

            var data = Komunikace.GetViewData(USERID);

            if ((data != null) && (data.FASK_Logins_View_Prava.Count > 0))
            {

                var Row =  data.FASK_Logins_View_Prava.First();

                this._firstName = Row.firstname.Trim();
                this._surName = Row.surname.Trim();
                this._userID = Row.USERID.Trim();
                this._Heslo = Row.psswd.Trim();


                foreach (var item in data.FASK_Logins_View_Prava)
                {
                    object tmpObject = GetInstance(item.AGENDAID.Trim());

                    if (tmpObject != null)
                    {
                        if (tmpObject is Classes.IPrava)
                            ListinaPravASlobod.Add((Classes.IPrava)tmpObject);
                    }
                    else
                    {
                        //Log.Write("Instance objektu práv nenalezena");
						throw new Exception("Instance objektu práv nenalezena");
                    }
                }
            }
            else
            {
                //Log.Write("Práva pro uživatele:'" + USERID + "' nenalezena!");
				throw new Exception("Práva pro uživatele:'" + USERID + "' nenalezena!");
            }
        }

        private object GetInstance(string strFullyQualifiedName)
        {
            try
            {
                string tmp = "FASK.Logins.Classes." + strFullyQualifiedName;
                Type t = Type.GetType(tmp);
                return Activator.CreateInstance(t);
            }
            catch(Exception ex)
            {
                //Log.Write(ex);
				//throw ex;
				//Fask.Logging.ExceptionHandler2.HandleErrorLog(ex);
                return null;
            }
        }

		/// <summary>
		/// Kontextové vraceni uživatelu s opravnenim začinajicim XXX% anebo _
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public FASK.Logins.DataSets.Pristupy GetUzivateleSOpravnenim(string AgendaID)
		{
			return Komunikace.GetLikeAgednaID(AgendaID);
		}       

    }
}
