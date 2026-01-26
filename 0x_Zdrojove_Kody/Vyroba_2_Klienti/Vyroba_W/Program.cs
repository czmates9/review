using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Fask.Vyroba_W
{
    public static class Program
    {
        public static Forms.FormMain mainApp = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        public static void Main()
        {
            try
			{
				mainApp = new Forms.FormMain();
				Application.Run(mainApp);
				mainApp = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fask.Vyroba_W.Program", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex.Message, "Fask.Vyroba_W.Program");
            }
        }
    }
}