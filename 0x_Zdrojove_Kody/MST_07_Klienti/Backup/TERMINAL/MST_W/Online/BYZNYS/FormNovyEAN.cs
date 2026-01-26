using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Fask.MST_W;
using Fask.MST_W.BYZNYS;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Online.BYZNYS
{
    public partial class FormNovyEAN : System.Windows.Forms.Form
    {

        public int Klic_MA { get; set; }
        public string Nazev_MA { get; set; }
        public string NewEAN { get; set; }
        public int? Klic_Odb { get; set; }
        public string Nazev_Odb { get; set; }

        public FormNovyEAN()
        {
            InitializeComponent();
            jEDNOTKYTableAdapter.Connection.ConnectionString = Settings.Online_BYZNYS_ConnectionString;

            if (PlatformDetection.Platform.IsWinCE() || PlatformDetection.Platform.IsCENET())
            {
                this.mainMenu1.Dispose();
                this.mainMenu1 = null;
            }
            else
            {
                panelButtons.Hide();
            }
        }

        private void FormNovyEAN_Load(object sender, EventArgs e)
        {
            this.jEDNOTKYTableAdapter.Fill(this.databaseOnlinePrijem.JEDNOTKY);
            this.FormBorderStyle = Fask.MST_W.MST_Global.FormBorderStyleGlobal;
            this.Size = Fask.MST_W.Forms.FormLocation.ScreenResolution;
            RefreshFormNewEAN();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshFormNewEAN()
        {
            lbl_klicma.Text = Klic_MA.ToString();
            lbl_nazevmat.Text = Nazev_MA;
            lbl_newean.Text = NewEAN;

            cmb_mj.Items.Clear();            
            foreach (DatabaseOnline.JEDNOTKYRow jrow in databaseOnlinePrijem.JEDNOTKY)
            {
                cmb_mj.Items.Add(jrow);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        public void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            gb_ulozit_Click(null, null);
        }

        public void PerformOk()
        {
            DialogResult = DialogResult.OK;
        }

        private void FormNovyEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                gb_ulozit_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                PerformCancel();
            else
                return;

            e.Handled = true;
        }

        private void gb_ulozit_Click(object sender, EventArgs e)
        {
            try
            {
                string mj = null;
                try
                {
                    mj = ((DatabaseOnline.JEDNOTKYRow)cmb_mj.SelectedItem).ZKRATKAMJ.Trim();
                }
                catch { }

                int? prepocet1 = null;
                int? prepocet2 = null;
                try { prepocet1 = Convert.ToInt32(txt_prepocet1.Text); }
                catch { }
                try { prepocet2 = Convert.ToInt32(txt_prepocet2.Text); }
                catch { }

                if (prepocet1 == null || prepocet2 == null || mj == null)
                {
                    if (DialogResult.No == MessageBoxBig.Show("Nejsou zadány všechny parametry přepočtových koeficientů.\n\nChcete vložit defaultní hodnoty?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                        return;

                    prepocet1 = null;
                    prepocet2 = null;
                    mj = null;
                }

                string status = Database.ONL_BWSCANNER_NOVY_EAN(Klic_MA, NewEAN, mj, prepocet1, prepocet2, Klic_Odb);
                if (status != "OK")
                {
                    if (status == "ERROR1")
                        Fask.MST_W.Forms.MessageBoxBig.Show("Klíč materiálu neexistuje!", this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                    else if (status == "ERROR2")
                        Fask.MST_W.Forms.MessageBoxBig.Show("EAN kód již existuje!", this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                    else if (status == "ERROR3")
                        Fask.MST_W.Forms.MessageBoxBig.Show("DMJ neexistuje!", this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                    else
                        Fask.MST_W.Forms.MessageBoxBig.Show(status, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);

                    return;
                }
                else
                {
                    Fask.MST_W.Forms.MessageBoxBig.Show("Uložen nový EAN '" + NewEAN + "'", "Nový EAN", MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Information);
                }
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.Log.Write("CHYBA:\n" + sqlex.Message);
                Fask.MST_W.Forms.MessageBoxBig.Show("CHYBA: " + sqlex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                return;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write("CHYBA: " + ex.Message);
                Fask.MST_W.Forms.MessageBoxBig.Show("CHYBA: " + ex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                return;
            }
            PerformOk();

        }

        private void gb_zpet_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            gb_zpet.Size = bNew;
        }

    }

}