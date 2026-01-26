using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProgramVersion
{
    static class Program
    {
        public static string[] argumentz;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            argumentz = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
