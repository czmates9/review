using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Fask.Logging;

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
                m = new System.Threading.Mutex(true, "SledovaniVyroby", out ok);
                string x = null;
                object a = x?.ToString();
                if (!ok)
                {
                    MessageBox.Show("Aplikace sledování výroby je již spuštěna");
                    return;
                }
#if !DEBUG
                //Process.Start("taskkill", "/F /IM explorer.exe");
#endif
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                string _dirlog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Fask.Logging.ExceptionHandler2.SetPath(_dirlog);
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "StartAplikace");
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Nastavena Cesta :" + _dirlog);


                Application.Run(new FASK.SledovaniVyroby.Main.frmMainApp());

            }
            catch (Exception ex)
            {
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message,"Fatal error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
            }
            finally
            {
                GC.KeepAlive(m);                // important!
#if !DEBUG
                //Process.Start("explorer.exe");
#endif
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                string messageLog = string.Empty;
                messageLog += "Neošetřená vyjímka:\n";
                messageLog += " ExceptionObject: " + (e.ExceptionObject != null ? e.ExceptionObject.ToString() : "null");
                messageLog += " IsTerminating: " + e.IsTerminating.ToString();
                messageLog += " Sender: " + (sender != null ? sender.ToString() : "null");
                //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(messageLog, "Unhandled Exception");
               string log_hlaska = string.Format(messageLog + "Unhandled Exception");
                ExceptionHandler2.Handle(log_hlaska, "Log_Vyroba", "txt");

                // na prvnich 30 znaku, par tecek a na dalsi radek vyjimka zalogovana do souoru
                //message chyba
                string msg = e.ExceptionObject.ToString().Substring(0, e.ExceptionObject.ToString().Length <= 30 ? e.ExceptionObject.ToString().Length : 30);
                msg += "....\n";
                msg += "Výjimka zalogována do souboru";
                MessageBox.Show(msg, "Neošetřená vyjímka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                try
                {
                    Fask.Emailing.Email.LoadConfiguration();
                    Fask.Emailing.Email.Body = messageLog;
                    Fask.Emailing.Email.CreateEmailMessage();
                    Fask.Emailing.Email.SendEmailMessageAsynch();

                }
                catch (Exception exEmail)
                {
                    //FASK.SledovaniVyroby.ErrorLog.Log.WriteException(exEmail);
                    ExceptionHandler2.Handle(exEmail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neošetřená vyjímka");
            }
        }
    }
}