using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace MainForm
{
    static class Program
    {
        static System.Threading.Mutex m;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool ok;
                m = new System.Threading.Mutex(true, "MST_WINDOWS", out ok);
                if (!ok)
                {
                    MessageBox.Show("Aplikace sledování výroby je již spuštìna");
                    return;
                }
#if !DEBUG
                Process.Start("taskkill", "/F /IM explorer.exe");
#endif
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FASK.MST_WINDOWS.Main.frmMainApp());

                GC.KeepAlive(m);                // important!
#if !DEBUG
                Process.Start("explorer.exe");
#endif
            }
                 catch (Exception ex)
            {
                #if !DEBUG
                Process.Start("explorer.exe");
                #endif
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException(ex);
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
                FASK.MST_WINDOWS.ErrorLog.Log.WriteException(messageLog, "Unhandled Exception");
                // na prvnich 30 znaku, par tecek a na dalsi radek vyjimka zalogovana do souoru
                //message chyba
                string msg = e.ExceptionObject.ToString().Substring(0, e.ExceptionObject.ToString().Length <= 30 ? e.ExceptionObject.ToString().Length : 30);
                msg += "....\n";
                msg += "Výjimka zalogována do souboru";
                MessageBox.Show(msg, "Neošetøená vyjímka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                try
                {
                    Fask.Emailing.Email.LoadConfiguration();
                    Fask.Emailing.Email.Body = messageLog;
                    Fask.Emailing.Email.CreateEmailMessage();
                    Fask.Emailing.Email.SendEmailMessageAsynch();

                }
                catch (Exception exEmail)
                {
                    FASK.MST_WINDOWS.ErrorLog.Log.WriteException(exEmail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neošetøená vyjímka");
            }
        }
    }
}