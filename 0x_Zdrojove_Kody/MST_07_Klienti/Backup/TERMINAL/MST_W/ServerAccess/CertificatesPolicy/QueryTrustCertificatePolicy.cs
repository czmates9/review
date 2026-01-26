using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace Fask.MST_W.ServerAccess.CertificatesPolicy
{
    public class QueryTrustCertificatePolicy : ICertificatePolicy
    {
        private const uint CERT_E_UNTRUSTEDROOT = 0x800B0109;
        private const uint CERT_E_CN_NO_MATCH = 0x800B010F;
        private const uint CERT_E_EXPIRED = 0x800B0101;
        private const uint CERT_E_WRONG_USAGE = 0x800B0110;

        public QueryTrustCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
        {

            bool returnValue = problem == 0;

            if ((uint)problem == CERT_E_UNTRUSTEDROOT)
            {
                if (MessageBox.Show("The security cetificate is not from a trusted" +
                    " certification authority (" + cert.GetIssuerName() + ").\n" +
                    " Do you want to proceed?", "Security Alert",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    returnValue = true;
            }
            else if ((uint)problem == CERT_E_CN_NO_MATCH)
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}
