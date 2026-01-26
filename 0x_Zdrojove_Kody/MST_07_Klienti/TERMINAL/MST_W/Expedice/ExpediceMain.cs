using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Expedice
{
    public partial class ExpediceMain : Form
    {
		/// <summary>
		/// Instance modulu inventura1
		/// </summary>
		public static ExpediceMain ExpediceMain_Instance = null;
		/// <summary>
		/// globalni objekt pro udrzovani komunikacnich objektu a objektu instance inventury
		/// </summary>
		public GlobalObject globalObject = new GlobalObject();  

        public ExpediceMain()
        {
            try
            {
                InitializeComponent();
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
                Expedice.Globals.Load(Main.ConfigModulesFileName);

				ExpediceMain_Instance = this;

                btnBaleni.Visible = Globals.ShowButtonExpedice_Baleni;
                btnExpedice.Visible = Globals.ShowButtonExpedice_Expedice;

                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                panel1_Resize(null, null);

                // nacteni konfigurace aplikace


                this.Text = MST_Global.ExpediceName;
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
                if (Globals.Expedice_DialogOpusteniModulu)
                {
                    if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                        return;
                }

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
            Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow hlavicka = null;

            // nacteni skladu
            Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
            if (sklad == null && !string.IsNullOrEmpty(Expedice.Globals.SkladID))
            {
                try
                {
                    //using (Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter())
                    //{
                        //ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
                        Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ExpediceMain_Instance.globalObject.controller_sklady.GetDataBySkl_id(Expedice.Globals.SkladID);
                        if (dt_sklady.Count > 0)
                            sklad = dt_sklady[0];
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, Expedice.Globals.SkladID), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                    //}
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    if (ex.Message.Contains(@"Data\Sklady.sdf"))
                        MessageBoxBigTimeout.Show("Nenalzen číselník 'Sklady.sdf'." + Environment.NewLine + "Pred pokračovaním zaktualizujte číselníky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    else
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                    return;
                }
            }

            if (sklad == null)
            {
                using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                {
                    if (fsv.ShowDialog() == DialogResult.Cancel)
                        return;

                    sklad = fsv.Sklad;

                    if (sklad == null)
                    {
                        Logging.Log.Write("Není vybrán sklad, přestože je vyžadován!");
                        return;
                    }
                }
            }

            // TODO: prepsat ?? ...
            while (true)
            {
                hlavicka = null;
                using (Expedice.ExpediceBaleniVyberHlavickyList expedice = new Fask.MST_W.Expedice.ExpediceBaleniVyberHlavickyList(sklad))
                {
                    expedice.Owner = this;
                    DialogResult dr = expedice.ShowDialog();
                    if (dr != DialogResult.OK)
                        return;

                    hlavicka = expedice._Hlavicka;
                }

                using (Expedice.ExpediceBaleniPolozkyList expedice = new Fask.MST_W.Expedice.ExpediceBaleniPolozkyList(hlavicka, sklad))
                {
                    expedice.Owner = this;
                    expedice.ShowDialog();
                }
            }
        }

        private void btnVydej_Click(object sender, EventArgs e)
        {
            try
            {
                PerformExpedice();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "btnVydej_Click");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformExpedice()
        {
            try
            {
                Fask.MST_W.ExpediceService.ExpediceHlavicky.CZMST_Expedice_HlavickaRow hlavicka = null;

                // nacteni skladu
                Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
                if (sklad == null && !string.IsNullOrEmpty(Expedice.Globals.SkladID))
                {
                    try
                    {
                        //using (Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter())
                        //{
                            //ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
                            Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ExpediceMain_Instance.globalObject.controller_sklady.GetDataBySkl_id(Expedice.Globals.SkladID);
                            if (dt_sklady.Count > 0)
                                sklad = dt_sklady[0];
                            else
                            {
                                MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, Expedice.Globals.SkladID), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        if((ex.Message == @"The database file cannot be found. Check the path to the database. [ Data Source = \Application\mst_w_6.1_alfa_test\Data\Sklady.sdf ]")
                            || (ex.Message == @" [ \Application\mst_w_6.1_alfa_test\Data\Sklady.sdf ]"))
                            MessageBoxBigTimeout.Show("Nenalzen číselník 'Sklady.sdf'." + Environment.NewLine + "Pred pokračovaním zaktualizujte číselníky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        else
                            MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);


                        return;
                    }
                }

                if (sklad == null)
                {
                    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                    {
                        if (fsv.ShowDialog() == DialogResult.Cancel)
                            return;

                        sklad = fsv.Sklad;

                        if (sklad == null)
                        {
                            Logging.Log.Write("Není vybrán sklad, přestože je vyžadován!");
                            return;
                        }
                    }
                }

                // TODO: prepsat ?? ...
                while (true)
                {
                    hlavicka = null;
                    using (Expedice.ExpediceVyberHlavickyList expedice = new Fask.MST_W.Expedice.ExpediceVyberHlavickyList(sklad))
                    {
                        expedice.Owner = this;
                        DialogResult dr = expedice.ShowDialog();
                        if (dr != DialogResult.OK)
                            return;

                        hlavicka = expedice._Hlavicka;
                    }

                    //throw new NotImplementedException("Není implementováno ...");
                    using (Expedice.ExpedicePaletyList expedice = new ExpedicePaletyList(hlavicka, sklad))
                    {
                        expedice.Owner = this;
                        expedice.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PerformExpedice");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
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
    }
}