using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PohodaImportVolitelneParametry
{
    static class Program
    {
        private static Form_Main frm = null;
        //private static XML_Transformace.Form_Test frm = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string c =  Settings.Catalog;

            frm = new Form_Main();
            //frm = new XML_Transformace.Form_Test();

            Application.Run(frm);

        }
    }
}
