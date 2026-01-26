using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Fask.MST_W.ServerAccess.CertificatesPolicy
{
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        {
        }

        #region ICertificatePolicy Members
        public bool CheckValidationResult(ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            return true;
        }

        #endregion
    }
}
