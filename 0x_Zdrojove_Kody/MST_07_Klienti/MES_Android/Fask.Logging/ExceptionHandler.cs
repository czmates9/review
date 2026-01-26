using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Logging.Exceptions
{
    public class ExceptionHandler
    {


        public static bool Handle(LogLevel logLevel ,string exception)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return Handle("Trace" + Environment.NewLine + exception);
                case LogLevel.Debug:
                    return Handle("Debug" + Environment.NewLine + exception);
                case LogLevel.Info:
                    return Handle("Info" + Environment.NewLine + exception);
                case LogLevel.Warn:
                    return Handle("Warning" + Environment.NewLine + exception);
                case LogLevel.Error:
                    return Handle(new Exception(exception), true);
                case LogLevel.Fatal:
                    return Handle("Fatal" + Environment.NewLine + exception);
                default:
                    break;
            }

            return true;
        }


        public static bool Handle(string exception)
        {
            Logging.Log.writeErrorLog(exception);
            return true;
        }


            /// <summary>
            /// Zpracovava vyjimky v systemu
            /// </summary>
            /// <param name="ex">Vyjimka</param>
            /// <remarks>Zobrazuje dialog s chybovym hlasenim</remarks>
            public static bool Handle(Exception exception)
        {
            return Handle(exception, true);
        }
        /// <summary>
        /// Zpracovava vyjimky v systemu
        /// </summary>
        /// <param name="ex">Vyjimka</param>
        /// <returns>Zda byla zpracovana. True = zpracovano uspeseni; False = nezpracovano uspesne</returns>
        public static bool Handle(Exception exeption, bool messageBoxShow)
        {
            System.Windows.Forms.DialogResult msgResult;
            return Handle(LogLevel.Error ,exeption, messageBoxShow, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, out msgResult);
        }

        /// <summary>
        /// Zpracovava vyjimky v systemu
        /// </summary>
        /// <param name="ex">Vyjimka</param>
        /// <returns>Zda byla zpracovana. True = zpracovano uspeseni; False = nezpracovano uspesne</returns>
        public static bool Handle(LogLevel logLevel, Exception exeption, bool msgShow, System.Windows.Forms.MessageBoxButtons msgButtons, System.Windows.Forms.MessageBoxIcon msgIcon, out System.Windows.Forms.DialogResult msgResult)
        {
            msgResult = System.Windows.Forms.DialogResult.None;
            try
            {
                //Logging.Log.writeErrorLog(exeption);
                if (msgShow)
                {
                    msgResult = System.Windows.Forms.MessageBox.Show(exeption.Message, "Chyba", msgButtons, msgIcon, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.writeErrorLog(ex);
                return false;
            }
            finally
            {
            }
        }
    }
}
