using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Inventura2
{
    public partial class FiltryInfo : System.Windows.Forms.Form
    {


        public FiltryInfo()
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            Cursor.Current = Cursors.Default;
        }

        private void FiltryInfo_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.Text += " " + MST_Global.Inventura2Name.Trim();

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            UpdateForm();

            Cursor.Current = Cursors.Default;
        }

        private void UpdateForm()
        {
            try
            {
                if (Inventura2.Inventura2_Instance.globalObject.active_lokace != null)
                    //df_LOKACE.Data = "" +
                    //    Globals.active_lokace.KLIC_LOK.ToString() + "\n" + 
                    //    Globals.active_lokace.NAZEV.Trim();
                    df_LOKACE.Data = "" +
						"LOC1: " + Inventura2.Inventura2_Instance.globalObject.active_lokace.LOKACE1.Trim().ToString() + "\n" +
						"LOC2: " + Inventura2.Inventura2_Instance.globalObject.active_lokace.LOKACE2.Trim().ToString() + "\n" +
						Inventura2.Inventura2_Instance.globalObject.active_lokace.NAZEV.Trim() + "\n" +
						"EAN : " + Inventura2.Inventura2_Instance.globalObject.active_lokace.EANL.Trim().ToString();
                else
                    df_LOKACE.Data = "-";

				if (Inventura2.Inventura2_Instance.globalObject.active_kancelar != null)
					df_KANCL.Data = Inventura2.Inventura2_Instance.globalObject.active_kancelar.KANCL.Trim() + "\n" + (Inventura2.Inventura2_Instance.globalObject.active_kancelar.IsTEXTNull() ? "-" : Inventura2.Inventura2_Instance.globalObject.active_kancelar.TEXT.Trim());
                else
                    df_KANCL.Data = "-";

				if (Inventura2.Inventura2_Instance.globalObject.active_osoba != null)
                {
                    string sosoba = "" +
						(Inventura2.Inventura2_Instance.globalObject.active_osoba.IsTITULNull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_osoba.TITUL.Trim() + " ") +
						(Inventura2.Inventura2_Instance.globalObject.active_osoba.IsJMENONull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_osoba.JMENO.Trim() + " ") +
						(Inventura2.Inventura2_Instance.globalObject.active_osoba.IsPRIJMENINull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_osoba.PRIJMENI.Trim());

					df_OSOBA.Data = Inventura2.Inventura2_Instance.globalObject.active_osoba.OSOBA_ZODP.ToString() + "\n" + (sosoba.Length == 0 ? "-" : sosoba);
                }
                else
                    df_OSOBA.Data = "-";

				if (Inventura2.Inventura2_Instance.globalObject.active_stredisko != null)
                {
					string sstredisko = (Inventura2.Inventura2_Instance.globalObject.active_stredisko.IsNAZEVNull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_stredisko.NAZEV.Trim());
					df_STRED.Data = Inventura2.Inventura2_Instance.globalObject.active_stredisko.STREDISKO.Trim() + "\n" + (sstredisko.Length == 0 ? "-" : sstredisko);
                }
                else
                    df_STRED.Data = "-";

            }
            catch
            {
                df_LOKACE.Data = "?";
                df_KANCL.Data = "?";
                df_OSOBA.Data = "?";
                df_STRED.Data = "?";
            }
            finally
            {
            }
        }

        private void PerformKonec()
        {
            DialogResult = DialogResult.OK;
        }

        private void menuItemKonec_Click(object sender, EventArgs e)
        {
            PerformKonec();
        }

        private void Nasnimane_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec();
            }
            else
                return;

            e.Handled = true;
        }
    }

}