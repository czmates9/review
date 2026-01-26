using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Xml.Linq;

namespace PohodaImportVolitelneParametry
{
    public partial class Form_Main : Form
    {



        public Form_Main()
        {
            InitializeComponent();
        }


        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Update();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //filename = @"d:\_work_\_Vyvoj_Subversion_02\Pohoda_ImportVolitelneParametry\Test.xml";
                string filename = Path.Combine(Settings.PathToInputDirectory, "Test.xml"); // @"d:\_work_\_Vyvoj_Subversion_02\Pohoda_ImportVolitelneParametry\Test.xml";
                                                                                           //ICO = "64086551";

                if (!Directory.Exists(Settings.PathToInputDirectory))
                    Directory.CreateDirectory(Settings.PathToInputDirectory);


                XML.SkladaniXML.SkladaniXMLSeznam(filename, Settings.ICO);


                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                MessageBox.Show("Uspešny import");
            }
            catch (Exception ex)
            {
                Log.Logging.Write(ex);
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                //TODO, je udelane dotahovani ID s seznamu, dodelat skladani XML pro import pro Volitelne parametry

                //Udelat dve varianty, jedna s vkladanim pouze položek do seznamku a Grafiku je nutno dodelat ručne, když už použvají volitelne parametry
                // Uplne vkladani i s rozloženim grafiky... mnelo by to jit...

                string filename = Path.Combine(Settings.PathToInputDirectory, "TestVP.xml"); // @"d:\_work_\_Vyvoj_Subversion_02\Pohoda_ImportVolitelneParametry\Test.xml";

                if (!Directory.Exists(Settings.PathToInputDirectory))
                    Directory.CreateDirectory(Settings.PathToInputDirectory);

                var dt_ID_FXTS = Database.Pohoda.sVPUL_GetData_ByIDS_UsrAgID("FXTS");
                var dt_ID_TIMEMODE = Database.Pohoda.sVPUL_GetData_ByIDS_UsrAgID("TIMEMODE");
                var dt_ID_FPV = Database.Pohoda.sVPUL_GetData_ByIDS_UsrAgID("FPV");


                string ID_FXTS = null;
                string ID_TIMEMODE = null;
                string ID_FPV = null;


                if ((dt_ID_FXTS != null) && (dt_ID_FXTS.sVPUL_UsrAgID.Count > 0))
                {
                    var rowid = dt_ID_FXTS.sVPUL_UsrAgID.First();
                    ID_FXTS = rowid.UsrAgID.ToString();
                }

                if ((dt_ID_TIMEMODE != null) && (dt_ID_TIMEMODE.sVPUL_UsrAgID.Count > 0))
                {
                    var rowid = dt_ID_TIMEMODE.sVPUL_UsrAgID.First();
                    ID_TIMEMODE = rowid.UsrAgID.ToString();
                }

                if ((dt_ID_FPV != null) && (dt_ID_FPV.sVPUL_UsrAgID.Count > 0))
                {
                    var rowid = dt_ID_FPV.sVPUL_UsrAgID.First();
                    ID_FPV = rowid.UsrAgID.ToString();
                }

                if (!string.IsNullOrEmpty(ID_FXTS) && !string.IsNullOrEmpty(ID_TIMEMODE) && !string.IsNullOrEmpty(ID_FPV))
                {
                    XML.SkladaniXML.SkladaniXMLVolitelneParametry(filename, Settings.ICO, ID_FXTS, ID_TIMEMODE, ID_FPV, chb_Grafika.Checked);

                    string responsefilename;
                    if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                    {
                        throw new Exception("Komunikace s Pohodou se nezdařila");
                    }

                    MessageBox.Show("Uspešny import");
                }
                else
                {
                    string text = string.Empty;

                    if (string.IsNullOrEmpty(ID_FXTS))
                    {
                        text += " FXTS ";

                    }

                    if (string.IsNullOrEmpty(ID_TIMEMODE))
                    {
                        text += " TIMEMODE ";
                    }

                    if (string.IsNullOrEmpty(ID_FPV))
                    {
                        text += " FPV ";
                    }

                        MessageBox.Show("Seznam nenalezen:" + text);
                }





            }
            catch (Exception ex)
            {
                Log.Logging.Write(ex);
                MessageBox.Show(ex.Message);
            }

        }

        private void konfiguraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (Form_Settings set = new Form_Settings())
                {
                    set.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                Log.Logging.Write(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
