#define SPLASH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Konzola
{
    static class Program
    {
        private static SplashScreen splash;

        public static Licence.Licensing _licence = new Licence.Licensing();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                bool stav = true;

                #region Licence

                try
                {

                    if (!_licence.IsLicensed)
                    {
                        string msg = "Licence: Není platná => '" +
                             _licence.Licence +
                             "'" +
                             Environment.NewLine +
                             _licence.Status 
                             ;

                        MessageBox.Show( msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        //stav = false;
                    }

                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    string msg = "Aplkace bude ukončena. Chyba u licence...;";
                    MessageBox.Show( msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    stav = false;
                }

                #endregion


                if (stav)
                {

#if SPLASH

#if !DEBUG
                    ////Celkem už použitelny, aplikovat ked bude Licnece
                    var splash = SplashScreen.Current;
                    splash.SetFade = true;
                    //splash.SetDesktopLocation(System.Environment.
                    splash.SetDateLic = string.Format("Platná do {0}", _licence.Expiration_Date.ToShortDateString());
                    splash.SetZakaznik = _licence.Licence;
                    splash.SetBackgroundImage = Properties.Resources.logo_FASK3;

                    splash.ShowSplashScreen(); 
#endif

#endif

                    Fask.Logging.ExceptionHandler2.SetEnablePrint(true); 
                    string _dirlog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Fask.Logging.ExceptionHandler2.SetPath(_dirlog);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "StartAplikace");
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Nastavena Cesta :" + _dirlog);

                    Fask.ModulePohodaXML.Globals_V1.LoadConfiguration();
                    Konzola.Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();
                    Fask.ModuleSql_API.Globals_V1.LoadConfiguration();
                    Konzola.Konfigurace_Tisky.Globals_Konfig_Tisk.LoadConfiguration();

                    Application.Run(new Forms.FormMain()
                    {
                        Licence = _licence
                    }
                        ); 
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Konzola.Program", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
