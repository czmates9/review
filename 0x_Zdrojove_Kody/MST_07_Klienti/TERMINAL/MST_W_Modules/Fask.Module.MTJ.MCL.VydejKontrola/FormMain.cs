using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Module.MTJ.MCL.VydejKontrola.Forms;

namespace Fask.Module.MTJ.MCL.VydejKontrola
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            try
            {
                InitializeComponent();

                try
                {
                    if (System.IO.File.Exists(Globals.ConfigurationFile))
                    {
                        Globals.Configuration.ReadXml(Globals.ConfigurationFile);
                    }
                    
                    //// nacteni barev z konfigurace aplikace
                    //// baleni
                    //string colorcode = Globals.Configuration.ModuleBaleniColor;
                    //int argb = Int32.Parse(colorcode.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);
                    //Color clr = Color.FromArgb(argb);
                    //btnBaleni.BackColor = clr;

                    //// vydej
                    //colorcode = Globals.Configuration.ModuleVydejColor;
                    //argb = Int32.Parse(colorcode.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);
                    //clr = Color.FromArgb(argb);
                    //btnVydej.BackColor = clr;

                    //// prijem zbytku
                    //colorcode = Globals.Configuration.ModulePrijemZbytkuColor;
                    //argb = Int32.Parse(colorcode.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);
                    //clr = Color.FromArgb(argb);
                    //btnPrijemZbytku.BackColor = clr;

                    //// prijem dle podkladu
                    //colorcode = Globals.Configuration.ModulePrijemDlePodkladuColor;
                    //argb = Int32.Parse(colorcode.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);
                    //clr = Color.FromArgb(argb);
                    //btnPrijem.BackColor = clr;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "FormMain, LoadConfiguration");
                }

                //wsBaleni = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni();
                //wsBaleni.Url = Globals.ServerAddress + "Baleni.asmx";
                //wsBaleni.Timeout = Globals.ServerTimeout;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "FormMain");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                panel1_Resize(null, null);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "FormMain_Load");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void ExitModule()
        {
            try
            {
                if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    return;

                if (Globals.Configuration != null)
                    Globals.Configuration.WriteXml(Globals.ConfigurationFile);

                this.Close();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ExitModule");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ExitModule();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PerformOK()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            ExitModule();
        }

        private void btnBaleni_Click(object sender, EventArgs e)
        {
            try
            {
                PerformBaleni();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "btnBaleni_Click");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformBaleni()
        {
            //using (FormBaleniMain frmMain = new FormBaleniMain(btnBaleni.BackColor))
            //{
            //    frmMain.ShowDialog();
            //}
        }

        private void btnVydej_Click(object sender, EventArgs e)
        {
            try
            {
                PerformVydej();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "btnVydej_Click");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformVydej()
        {
            //string objednavka = null;

            //while (true)
            //{
            //    objednavka = null;

            //    using (FormVyberObjednavky frmObj = new FormVyberObjednavky(btnVydej.BackColor))
            //    {
            //        DialogResult dr = frmObj.ShowDialog();

            //        if (dr != DialogResult.OK)
            //            return;

            //        objednavka = frmObj.Objednavka;
            //    }

            //    using (Vydej.NactenePolozkyList frmObj = new Vydej.NactenePolozkyList(objednavka, btnVydej.BackColor))
            //    {
            //        frmObj.ShowDialog();
            //    }
            //}
        }

        private void panel1_Resize(object sender, EventArgs e)
        {            
            // TODO : opravit ...
            // 1) spocitat pocet "Buttonu" v panel1.Controls
            // 2) podle poctu prepocitat velikost pro buttony
            // 3) nastavit velikost buttonu ...

            int countButtons = 0;
            foreach (object i in panel1.Controls)
            {
                if (
                    ((i is Button) || (i is Graphic.GraphicButton))
                    && ((Control)i).Visible
                    )
                {
                    countButtons++;
                }
            }

            Size nsize = nsize = new Size(0, 0);
            if (countButtons > 0) // kvuli moznemu deleni nulou ...
                nsize = new Size(panel1.Width, panel1.Height / countButtons);


            foreach (object i in panel1.Controls)
            {
                if ((i is Button) || (i is Graphic.GraphicButton))
                    ((Control)i).Size = nsize;
            }        
        }

        private void btnPrijemZbytku_Click(object sender, EventArgs e)
        {
            try
            {
                PerformPrijemZbytku();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "btnPrijemZbytku_Click");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformPrijemZbytku()
        {
            //string objednavka = null;

            //while (true)
            //{
            //    objednavka = null;

            //    using (FormVyberObjednavky frmObj = new FormVyberObjednavky(btnPrijemZbytku.BackColor))
            //    {
            //        DialogResult dr = frmObj.ShowDialog();

            //        if (dr != DialogResult.OK)
            //            return;

            //        objednavka = frmObj.Objednavka;
            //    }

            //    using (PrijemZbytku.NactenePolozkyList frmObj = new PrijemZbytku.NactenePolozkyList(objednavka, btnPrijemZbytku.BackColor))
            //    {
            //        frmObj.ShowDialog();
            //    }
            //}
        }

        private void btnPrijem_Click(object sender, EventArgs e)
        {
            try
            {
                PerformPrijem();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "btnPrijem_Click");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformPrijem()
        {
            //string prijemka = null;
            
            //while (true)
            //{
            //    prijemka = null;

            //    using (FormVyberPrijemky frmObj = new FormVyberPrijemky(btnPrijem.BackColor))
            //    {
            //        //frmObj.Text = "Příjem dle podkladu - Výběr ";
            //        DialogResult dr = frmObj.ShowDialog();

            //        if (dr != DialogResult.OK)
            //            return;

            //        prijemka = frmObj.Prijemka;
            //    }

            //    using (Prijem.NactenePolozkyList frmObj = new Prijem.NactenePolozkyList(prijemka, btnPrijem.BackColor))
            //    {
            //        frmObj.ShowDialog();
            //    }
            //}
        }

        private void btnKontrola_Click(object sender, EventArgs e)
        {
            try
            {
                PerformVydejKontrola();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "btnKontrola_Click");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformVydejKontrola()
        {
            using (VydejKontrola.FormVydejKontrola frmObj = new Fask.Module.MTJ.MCL.VydejKontrola.VydejKontrola.FormVydejKontrola())
            {
                DialogResult dr = frmObj.ShowDialog();

                if (dr != DialogResult.OK)
                    return;

            }


        }
        
    }
}