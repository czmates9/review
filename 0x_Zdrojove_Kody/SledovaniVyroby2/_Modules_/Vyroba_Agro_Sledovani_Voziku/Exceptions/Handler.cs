using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Exceptions
{
    public class Handler
    {
        #region Osetreni Vyjimek

        public static void ErrorHandle(Exception ex)
        {
           // ErrorLog.Log.Write_ToCustomFile(ex, "log_adam.txt");
        }

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
            //ErrorLog.Log.Write_ToCustomFile(errMessage, errMessageContext, "log_adam.txt");
            //if (showErrorDialog)
            //    System.Windows.Forms.MessageBox.Show(errMessage, errMessageContext, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
        #endregion
    }
}
