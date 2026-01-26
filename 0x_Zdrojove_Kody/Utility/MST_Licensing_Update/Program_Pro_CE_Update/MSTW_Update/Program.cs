using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MSTW_Update
{
    static class Program
    {
        public static string[] commandLineArguments;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            try
            {
                commandLineArguments = args;

                Application.Run(new Notification());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}