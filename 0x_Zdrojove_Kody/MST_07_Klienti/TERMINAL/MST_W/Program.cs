using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Fask.MST_W
{
    static class Program
    {
        //public static Vydej.VydejForm vydejForm;
        public static Main mstw;
        //public static System.Globalization.CultureInfo cultureInfo;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //vydejForm = new Fask.MST_W.Vydej.VydejForm();
            //Application.Run(vydejForm);
            try
            {
                Logging.Log.Enable = true;
                Logging.Log.Write("Aplikace spuštìna");

                // TODO : predelat do mstwmain, pridat konfiguraci jazyka do mstw ...
                //cultureInfo = System.Globalization.CultureInfo.GetCultureInfo("sk");
                //Fask.MST_W.Localization.Localization.Culture = cultureInfo;

                Logging.Log.Write("Inicializace UnhandledException");
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Logging.Log.Write("Konec inicializace UnhandledException");
                
                mstw = new Main();
                Application.Run(mstw);
                Logging.Log.Enable = true;
                Logging.Log.Write("Aplikace ukonèena");
            }
            catch (TypeLoadException tlex)
            {
                Logging.Log.Enable = true;
                Logging.Log.Write(tlex, "Main");
                MessageBox.Show("Critical Error : " + tlex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Application.Exit();
                return;
            }
            catch (Exception ex)
            {
                Logging.Log.Enable = true;
                Logging.Log.Write(ex.Message + ex.StackTrace, "Main");
                MessageBox.Show("Critical Error : " + ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                // TODO :  ???? System.Threading.Thread.CurrentThread.abort 
                Application.Exit();
                return;
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                string messageLog = string.Empty;
                messageLog += "Neošetøená vyjímka:\n";
                messageLog += " ExceptionObject: " + (e.ExceptionObject != null ? e.ExceptionObject.ToString() : "null");
                messageLog += " IsTerminating: " + e.IsTerminating.ToString();
                messageLog += " Sender: " + (sender != null ? sender.ToString() : "null");
                Exception exx = (e.ExceptionObject as Exception);
                messageLog += " Stack:" + (exx == null ? "" : exx.StackTrace);
                Logging.Log.Enable = true;
                Logging.Log.Write(messageLog, "Neošetøená vyjímka");
                // zobrazi se prvnich 30 znaku a zbytek se zaloguje do souboru
                string msg = e.ExceptionObject.ToString().Substring(0, e.ExceptionObject.ToString().Length <= 30 ? e.ExceptionObject.ToString().Length : 30);
                msg += "....\n";
                msg += "Vyjímka zalogována do souboru";
                MessageBox.Show(msg, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unhandled Exception - write");
            }
            
            Application.Exit();
        }
    }
}