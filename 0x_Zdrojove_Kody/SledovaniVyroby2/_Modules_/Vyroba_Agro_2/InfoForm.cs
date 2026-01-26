using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FASK.SledovaniVyroby.Module.Vyroba_Agro;

namespace Vyroba_Agro
{
    public partial class InfoForm : Form
    {
        private int error_id;

        public InfoForm()
        {
            InitializeComponent();
        }

        public void setID(int error_id)
        {
            try
            {
                this.error_id = error_id;

                string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string filepath = (new Uri(Path.Combine(AssemblyDirectoryPath + @"\Data\", "error_" + error_id + ".html"))).LocalPath;

                FileStream source = new FileStream(filepath, FileMode.Open, FileAccess.Read);

                webBrowser1.DocumentStream = source;

                return;

            }
            catch (Exception e)
            {
                webBrowser1.DocumentText = e.ToString();
            }
        }

        private void zavřítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            InformationUC.infoForm = null;
        }
    }
}
