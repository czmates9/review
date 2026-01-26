using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Exceptions
{
    public class Handler
    {
        #region Osetreni Vyjimek
        public static void ErrorHandle(string errMessage)
        {
            ErrorHandle(errMessage, string.Empty, false);
        }
        public static void ErrorHandle(string errMessage, string errMessageContext)
        {
            ErrorHandle(errMessage, errMessageContext, false);
        }
        public static void ErrorHandle(string errMessage, string errMessageContext, bool showErrorDialog)
        {
            ErrorLog.Log.Write(errMessage, errMessageContext);
            if (showErrorDialog)
                System.Windows.Forms.MessageBox.Show(errMessage, errMessageContext, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
        #endregion
    }
}
