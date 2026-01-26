using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Exceptions
{
    public class ExceptionVizualize
    {
        public static void Show(Exception ex)
        {
            if (ex is System.Web.Services.Protocols.SoapException)
            {
                Show((System.Web.Services.Protocols.SoapException)ex);
            }
            else
            {
                MessageBoxBig.Show(ex.Message, "Chyba", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        public static void Show(System.Web.Services.Protocols.SoapException soapex)
        {
            MessageBoxBig.Show(soapex.Message, "SoapException", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        }
    }
}
