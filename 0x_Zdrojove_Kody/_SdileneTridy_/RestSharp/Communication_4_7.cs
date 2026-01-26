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
    public class Communication_4_7 : Communication_Base
    {


        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Communication_4_7(
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
                System.Net.ServicePointManager.ServerCertificateValidationCallback += TrustingCallBack;
                client.PreAuthenticate = false;
                request.Credentials = new System.Net.NetworkCredential();
            }
        }

        private bool TrustingCallBack(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // The logic for acceptance of your certificates here
            return true;
        }

    }

}
