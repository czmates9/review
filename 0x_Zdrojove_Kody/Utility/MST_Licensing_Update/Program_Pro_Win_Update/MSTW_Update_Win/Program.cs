using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSTW_Update
{
    static class Program
    {

        public static string[] commandLineArguments;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {



            try
            {
                commandLineArguments = args;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Notification());
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
