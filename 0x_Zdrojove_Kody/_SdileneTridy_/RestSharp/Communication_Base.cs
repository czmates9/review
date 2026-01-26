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
    public abstract class Communication_Base
    {

        #region Vstupní parametry

        private string _FASK_TID = null;
        public string FASK_TID
        {
            get { return _FASK_TID; }
            set { _FASK_TID = value; }
        }

        private string _FASK_TYPE_KLIENT = null;
        public string FASK_TYPE_KLIENT
        {
            get { return _FASK_TYPE_KLIENT; }
            set { _FASK_TYPE_KLIENT = value; }
        }

        private string _autorizace = null;
        public string Autorizace
        {
            get { return _autorizace; }
            set { _autorizace = value; }
        }

        private string _adresa = null;
        public string Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private string _routeKonstant = null;
        public string RouteKonstant
        {
            get { return _routeKonstant; }
            set { _routeKonstant = value; }
        }

        private bool _isHTTPS = false;
        public bool isHTTPS
        {
            get { return _isHTTPS; }
            set { _isHTTPS = value; }
        }

        private int _timeOut = -1;
        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Communication_Base(
            string Adresa, 
            string Autorizace, 
            string RouteKonstant, 
            bool isHttps, 
            int TimeOut,
            string FASK_TID,
            string FASK_TYPE_KLIENT
            )
        {
            _adresa = Adresa;
            _autorizace = Autorizace;
            _routeKonstant = RouteKonstant;
            _isHTTPS = isHttps;
            _timeOut = TimeOut;
            _FASK_TID = FASK_TID;
            _FASK_TYPE_KLIENT = FASK_TYPE_KLIENT;
        }

        #endregion

        public string URL
        {
            get
            {
                // TODO WTF
                if (isHTTPS)
                {
                    if ( string.IsNullOrEmpty(_routeKonstant))
                        return string.Format("https://{0}/", _adresa) + "{0}";
                    else
                        return string.Format("https://{0}/{1}/", _adresa, _routeKonstant) + "{0}";
                }
                else
                {
                     if (string.IsNullOrEmpty(_routeKonstant))
                        return string.Format("http://{0}/", _adresa) + "{0}";
                    else
                        return string.Format("http://{0}/{1}/", _adresa, _routeKonstant) + "{0}";
                }
                  
            }
        }

        public Uri GetUri(string Param)
        {
            return new Uri(string.Format(URL, Param));
        }



        public void AutorizaceInit(RestRequest request)
        {
            if (!string.IsNullOrEmpty(_autorizace))
            {
                request.AddHeader("Authorization", Autorizace_Basic);
            }
        }

        public void Info_Init(RestRequest request)
        {
            if (!string.IsNullOrEmpty(_FASK_TID))
            {
                request.AddHeader("FASK_TID", _FASK_TID);
            }

            if (!string.IsNullOrEmpty(_FASK_TYPE_KLIENT))
            {
                request.AddHeader("FASK_TYPE_KLIENT", _FASK_TYPE_KLIENT);
            }
        }

        public string Autorizace_Basic
        {
            get
            {
                return "Basic " + Autorizace;
            }
        }

		/// <summary>
		/// Metoda pro ignorovani certifikatu HTTPS
		/// </summary>
		/// <param name="request"></param>
		/// <param name="client"></param>
		public virtual void IgnoreCert(RestRequest request, RestClient client)
		{

		}

		//private bool TrustingCallBack(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		//{
		//    // The logic for acceptance of your certificates here
		//    return true;
		//}


        #region Metody

        /// <summary>
        /// GET metoda 
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IRestResponse REST_GET(string Param)
        {
            try
            {
                IRestResponse response = null;

                var uri = GetUri(Param);

                var client = new RestClient(uri);
                client.Timeout = -1;				
                var request = new RestRequest(Method.GET);
				IgnoreCert(request,client);
                AutorizaceInit(request);
                Info_Init(request);
                response = client.Execute(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// POST metoda
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="Obsah"></param>
        /// <returns></returns>
        public IRestResponse REST_POST(string Param, string Obsah)
        {
            try
            {
                IRestResponse response = null;

                var uri = GetUri(Param);

                var client = new RestClient(uri);
				client.Timeout = -1;
				//client.ClientCertificates RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                var request = new RestRequest(Method.POST);
				IgnoreCert(request, client);
                AutorizaceInit(request);
                Info_Init(request);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", Obsah, ParameterType.RequestBody);
                response = client.Execute(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// PUT metoda
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="Obsah"></param>
        /// <returns></returns>
        public IRestResponse REST_PUT(string Param, string Obsah)
        {
            try
            {
                IRestResponse response = null;

                var uri = GetUri(Param);

                var client = new RestClient(uri);
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
				IgnoreCert(request, client);
                AutorizaceInit(request);
                Info_Init(request);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", Obsah, ParameterType.RequestBody);
                response = client.Execute(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// DELETE metoda
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IRestResponse REST_DELETE(string Param)
        {
            try
            {
                IRestResponse response = null;

                var uri = GetUri(Param);

                var client = new RestClient(uri);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
				IgnoreCert(request, client);
                AutorizaceInit(request);
                Info_Init(request);
                response = client.Execute(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath_Z_kama">Je plná cesta k souboru který se chce poslat</param>
        /// <param name="filePath_kam"> je cesta kam se to na serveru uloží. formát je   IDTerminalu/NazevSouboru</param>
        /// <param name="JSON"></param>
        /// <returns></returns>
        public IRestResponse REST_POST_SendFile(string filePath_Z_kama, string filePath_kam)
        {
            IRestResponse response = null;
            try
            {

                if (File.Exists(filePath_Z_kama))
                {
                    #region Reading to binary

                    byte[] data;
                    var stream = File.Open(filePath_Z_kama, FileMode.Open);
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        data = reader.ReadBytes((int)stream.Length);
                    }

                    #endregion

                    var apiUrl = GetUri("UploadFileWithStream");
                    //var apiUrl = Settings.WebServiceAddressVyroba + "api/UploadFileWithStream";
                    var client = new RestClient(apiUrl);
                    client.Timeout = -1;
                    
                    client.ClearHandlers();

                    var request = new RestRequest(Method.POST);
                    
                    request.AddHeader("Accept", "application/json");
                    request.Parameters.Clear();
                    
                    request.AddParameter("model", Newtonsoft.Json.JsonConvert.SerializeObject(new UploadFileObject()
                    {
                        PathFileOnServer = filePath_kam
                    }));

                    request.AddHeader("Content-Type", "multipart/form-data");
					Fask_Stream newStream = new Fask_Stream(data);
                    request.Files.Add(new FileParameter
                    {
                        Name = "file",
                        Writer = (s) =>
                        {
							newStream.Fask_CopyTo(s);
                        },
                        FileName = filePath_Z_kama,
                        ContentLength = newStream.Length
                    });

					IgnoreCert(request, client);
                    AutorizaceInit(request);
                    Info_Init(request);

                    response = client.Execute(request);
                }
                else
                    throw new Exception("Soubor pro odeslani neexistuje");

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public string SendFile_API(string filePath_Z_kama, string filePath_kam)
        {

            try
            {
                
                REST_POST_SendFile(filePath_Z_kama, filePath_kam);

                if (File.Exists(filePath_Z_kama))
                {
                    File.Delete(filePath_Z_kama);
                }

                return "OK";
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }

    public class UploadFileObject
    {
        public string PathFileOnServer { get; set; }

    }

	public class Fask_Stream : System.IO.MemoryStream
	{
		private const int _DefaultCopyBufferSize = 81920;

		public Fask_Stream(byte[] arr) : base(arr)
		{
 
		}

		public void Fask_CopyTo(Stream destination)
		{
			if (destination == null)
				throw new ArgumentNullException("destination");
			if (!CanRead && !CanWrite)
				throw new ObjectDisposedException(null, "ObjectDisposed_StreamClosed");
			if (!destination.CanRead && !destination.CanWrite)
				throw new ObjectDisposedException("destination", "ObjectDisposed_StreamClosed");
			if (!CanRead)
				throw new NotSupportedException("NotSupported_UnreadableStream");
			if (!destination.CanWrite)
				throw new NotSupportedException("NotSupported_UnwritableStream");

            Fask_InternalCopyTo(destination, _DefaultCopyBufferSize);
		}

		private void Fask_InternalCopyTo(Stream destination, int bufferSize)
		{
			if (destination == null)
				throw new Exception("Cil nenalezen");

			if (!CanRead)
				throw new Exception("Nelze číst zdroj");

			if (!destination.CanWrite)
				throw new Exception("Nelze zapisovat cíl");

			if (bufferSize <= 0)
				throw new Exception("špatná velkost bufferu");

			byte[] buffer = new byte[bufferSize];
			int read;
			while ((read = Read(buffer, 0, buffer.Length)) != 0)
				destination.Write(buffer, 0, read);
		}
	}
}
