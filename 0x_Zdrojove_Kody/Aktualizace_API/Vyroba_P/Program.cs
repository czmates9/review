using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Fask.Aktualizace_API
{
    static class Program
    {
        public static Forms.FormMain vyrobaMain = null;

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

                string _dirlog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Fask.Logging.ExceptionHandler2.SetPath(_dirlog);
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "StartAplikace");
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Nastavena Cesta :" + _dirlog);
                
                vyrobaMain = new Forms.FormMain();
                Application.Run(vyrobaMain);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Vyroba_P.Program", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Chyba aplikace", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
