using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;

namespace Fask.RestSharp.API
{
    /// <summary>
    /// Jedná se o třidku pomocí které se komunikuje s WebAPI cez REST
    /// </summary>
    public class Communication_3_5 : Communication_Base
    {


        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
		public Communication_3_5(
            string Adresa, 
            string Autorizace, 
            string RouteKonstant, 
            bool isHttps, 
            int TimeOut,
            string FASK_TID,
            string FASK_TYPE_KLIENT
            ) : base (Adresa, Autorizace, RouteKonstant, isHttps, TimeOut, FASK_TID, FASK_TYPE_KLIENT)
        {

        }

        #endregion


        public override void IgnoreCert(RestRequest request, RestClient client)
        {
            if (isHTTPS)
            {
				System.Net.ServicePointManager.CertificatePolicy = new Fask.RestSharp.API.TrustAllCertificatePolicy();
                client.PreAuthenticate = false;
				request.Credentials = new System.Net.NetworkCredential(); 
            }
        }
    }
}
