using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Vyroba_W
{
    public class Globals
    {
        #region Informace o smene
        public static DateTime? _PracovnikVedouciSmenyLoginDateTime = null;



		public static Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _PracovnikVedouciSmeny = null;
		public static Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow PracovnikVedouciSmeny
        {
            get { return _PracovnikVedouciSmeny; }
            set
            {
                _PracovnikVedouciSmeny = value;
                if (value == null)
                    _PracovnikVedouciSmenyLoginDateTime = null;
                else
                    _PracovnikVedouciSmenyLoginDateTime = DateTime.Now;
            }
        }
        #endregion

        #region Informace o pracovnikovi
        public static DateTime? _PracovnikLoginDateTime = null;
		public static Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _Pracovnik = null;
		public static Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _Pracovnik; }
            set
            {
                _Pracovnik = value;
                if (_Pracovnik == null)
                    _PracovnikLoginDateTime = null;
                else
                    _PracovnikLoginDateTime = DateTime.Now;
            }
        }
        #endregion

		public static Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow Zakazka = null;

		#region Certifikaty pro HTTPs 

		public enum ServerAccessCertificatesTrustType
		{
			OnlyInstalled,
			TrustAll,
			TrustQuery
		}

		private static ServerAccessCertificatesTrustType _serverAccessCertificateTrust = ServerAccessCertificatesTrustType.TrustAll;
		public static ServerAccessCertificatesTrustType ServerAccessCertificateTrust
		{
			get { return _serverAccessCertificateTrust; }
			set
			{
				_serverAccessCertificateTrust = value;

				switch (_serverAccessCertificateTrust)
				{
					case Globals.ServerAccessCertificatesTrustType.TrustAll:
						System.Net.ServicePointManager.CertificatePolicy = new Fask.Vyroba_W.ServerAccess.CertificatesPolicy.TrustAllCertificatePolicy();
						break;
					case Globals.ServerAccessCertificatesTrustType.TrustQuery:
						System.Net.ServicePointManager.CertificatePolicy = new Fask.Vyroba_W.ServerAccess.CertificatesPolicy.QueryTrustCertificatePolicy();
						break;
					case Globals.ServerAccessCertificatesTrustType.OnlyInstalled:
					default:
						System.Net.ServicePointManager.CertificatePolicy = null;    // TODO : ?? je toto spravne ??  => otestovat .. .!!!
						break;
				}

			}
		}

		
		#endregion

    }
}
