using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using IRFIDProvider;

namespace Konzola.Forms
{
    public partial class FormConfig : Form
    {

        //TODO MaR 27.6. 2023 vytvoren enum pro pocet desetinnych mist
        // Definice enumu
        enum MyEnum
        {
            N0, //0 desetinnych mist
            N1,
            N2,
            N3,
            N4,
            N5,
            N6
        }

        public static string KonfiguraceHeslo
        {
            get
            {
                string default_encrypted_konfiguraceheslo = Fask.Encryption.RijndaelWrapper.Encrypt(Encoding.ASCII.GetString(new byte[] { 49, 53, 57 }), Globals.encryptPassword);
                string encrypt_cs = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].KonfiguraceHeslo;

                try
                {
                    return Fask.Encryption.RijndaelWrapper.Decrypt(encrypt_cs, Globals.encryptPassword);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    return null;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    return null;
                }
            }
            set
            {
                string encrypt_cs = Fask.Encryption.RijndaelWrapper.Encrypt(value, Globals.encryptPassword);
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].KonfiguraceHeslo = encrypt_cs;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
        }


        #region Eventy formu

        public FormConfig()
        {
            InitializeComponent();
        }


        private void FormConfig_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            btn_storno.Size = nsize;
        }

        private void FormConfig_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            SettingsLoad();

           

            //TODO prava admin ke konzoly
            //if ((Globals.PracovnikOpravneni != null) && (Globals.PracovnikOpravneni.ADM > 0))
            if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin())
            {
                this.btn_DecryptConnectionString.Visible =
                    this.btn_DecryptConnectionString.Enabled = true;
            }
        }


        #endregion

        #region Click button Eventy

        private void btn_OK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void btn_storno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void btnEncryptConnectionString_Click(object sender, EventArgs e)
        {
            try
            {
                Konzola.MySystem.Encryption.EncryptConnectionString(true, Application.ExecutablePath);
                MessageBox.Show("Connection string úspěšně zašifrován");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecryptConnectionString_Click(object sender, EventArgs e)
        {
            try
            {
                Konzola.MySystem.Encryption.EncryptConnectionString(false, Application.ExecutablePath);
                MessageBox.Show("Connection string úspěšně dešifrován");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Perform Metody


        public void PerformOK()
        {
            //if (!MySystem.LoginTest.UserLoginAdminTest())
            //    return;

            if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin())
            {
                MessageBox.Show(this, "Přihlášený uživatel nemá prava Administrátora." + Environment.NewLine + " Proto je konfigurace pouze pro čtení a nemuže uložit provedené změny.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return;
            }

            if (SettingsSave())
                this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.DialogResult = DialogResult.Cancel;
        }


        #endregion

        #region KeyDown Event metoda volana

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }



        #endregion

        #region Pomocne metody

        private void SettingsLoad()
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                Load_System();
                Load_Ukolovani();
                Load_Planovani();
                Load_Vyroba();
                Load_ITCast();
                Load_StavSkladu();
                Load_Sklad();
                Load_Servis();
                Load_Export();
                Load_Tisk();

                #endregion

                FormConfig_Resize(null, null);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region LOAD SubMetody pro lepší přehled

        private void Load_Ukolovani()
        {
            
            cb_UkolovaniPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].Povolit;
            cb_UkolovaniPovolitBarevneZvyrazneniStavu.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].PovolitBarevneZvyrazneniStavu;

            cb_UkolovaniUkolyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].UkolyPovolit;
            cb_UkolovaniPrehledUkoluPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].UkolyPovolit;
            cb_UkolovaniHistorieUkoluPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].HistorieUkoluPovolit;
            
        }

        private void Load_System()
        {
            tp_SystemTID.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID.ToString();

            cb_SystemFiltryVazatNaUzivatele.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FiltryVazatNaUzivatele;
            cb_SystemDatagridVazatNaUzivatele.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].DataGridVazatNaUzivatele;

            tbox_HesloKonfigurace.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].KonfiguraceHeslo;

            cb_AplikacePracovniciPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].AplikacePracovniciPovolit;



            chB_Log_Tisky_hl.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl;



        }

        private void Load_Planovani()
        {
            cb_PlanyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit ;

            cb_Plany_PlanVV_Povolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_Plany_VV ;
            cb_Plany_Kap_Plan.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany;
            cb_Plany_Kap_Plan_MaterialPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany_Materialy;

            cb_VyrobaPriZaplanovaniVyrobnyPrikaz.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVyrobnyPrikaz;
            cb_VyrobaPriZaplanovaniVydej.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVydej;

            //zobrazeni vyrabene polozky MaR 20.12.2024
            chB_Planovani_Zobrazeni_Vyrab_polozky.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_VyrabenePolozky;

            tb_Plany_PlanVV_MJ_Separator.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani_VV_Params[0].Navrhar_MJ_Separator;
            tb_Plany_PlanVV_MJ_Text.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani_VV_Params[0].Navrhar_MJ_Text;

        }

        private void Load_Vyroba()
        {

            cb_VyrobaPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].Povolit;
            //MaR 7.2.2025 pridano kontrola UDI
            //chB_VH_kontrola_UDI.Checked
            chB_VH_kontrola_UDI.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].kontrolaDatUDI;

            //22.8.2024 MaR ukladani TISK etiket
            cb_VyrobaUkladaniEtiket.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].EtiketaTISKUkladat;

            //zobrazeni vyrabene polozky MaR 20.12.2024
            chB_Vyroba_Rozbory_Zobrazeni_Vyr_Pol.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_VyrabenePolozky;

            cb_VyrobaPouzivatTabulkuZbozi.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].PouzivatTabulkuZbozi ;
            cb_VyrobaVPP_GenerovatSN.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].VyrobniPrikazy_GenerovatSN;

            cbox_TypDovodu.DataSource = Enum.GetValues(typeof(Konzola.Vyroba.TypZpravovaniVyroby));
            cbox_TypDovodu.SelectedItem = (Konzola.Vyroba.TypZpravovaniVyroby)Enum.Parse(typeof(Konzola.Vyroba.TypZpravovaniVyroby), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].OdvodPOHODATyp , true); ;

            #region Ciselniky

            cb_Vyr_Zob_Ciselniky.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit ;

            cb_VyrobaSkupinyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Skupiny ;
            cb_VyrobaZboziPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Zasoby ;
            cb_VyrobaVazbaMaterialy.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_VazbyMaterialy ;
            cb_VyrobaStrojePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Stroje;

            cb_VyrobaModulyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Moduly;
            cb_VyrobaPrevodnikyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Prevodniky;
            #endregion

            #region Transakce

            cb_VyrobaTransakcePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit;

            cb_VyrobaVyrobniPrikazyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit_VyrobniPrikazy ;
            cb_VyrobaOdvodPOHODAPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit_OdvodPohoda ;

            #endregion

            #region Rozbory

            cb_VyrobaRozboryPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit ;

            cb_VyrobaPrehledPlanVyrobyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledPlanVyroby;
            cb_VyrobaPrehledOdvodEvents.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledOdvadeniStroju ;
            cb_VyrobaPrehledVyrobkyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledVyrobky ;
            cb_VyrobaPrehledVyrobkySNSarzePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledVyrobkySNSarze;
            cb_VyrobaPrehledMaterialyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledMaterialy ;
            cb_VyrobaPrehledOdvodEventsErr.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledOdvadeniStrojuErr;


            cb_VyrobaPrehledStavyStrojuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledStavyStroju;

            tbox_VyrobaNazevKSabloneTisku.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku;

            //TODO MaR 27.6. 2023 doplneni comboBoxu hodnotami z enum
            // Naplnění ComboBox hodnotami z enumu
            if(string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist))
            {
                cB_prehled_vyrobky.DataSource = Enum.GetValues(typeof(MyEnum));
            }
            else
            {
                MyEnum selectedEnumValue;
                if (Enum.TryParse(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist, out selectedEnumValue))
                {
                    // Nastavení výčtové hodnoty jako vybrané hodnoty v ComboBoxu
                    cB_prehled_vyrobky.DataSource = Enum.GetValues(typeof(MyEnum));
                    cB_prehled_vyrobky.SelectedItem = selectedEnumValue;
                }
                else
                {
                    MessageBox.Show("Hodnota Počet desetinnych míst není platná");
                    cB_prehled_vyrobky.DataSource = Enum.GetValues(typeof(MyEnum));
                    // Hodnota PocetDesetinnychMist není platná, provedte odpovídající akce
                }
            }


            #endregion

        }

        private void Load_ITCast()
        {
            cb_ITCastPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit;

            cb_ITCastIPTerminaluPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_IP_Terminalu;
            cb_ITCastTestDispPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_Test_Disp;
            cb_ITCastTestDisp2Povolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_TestDisp2;
            cb_ITCastTypyDokladuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_TypyDokladu;
            cb_ITCastRadyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_FaskRady;
            cb_ITCastKonfigPOHODAPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguracePOHODA;
            cb_ITCastKonfigArchivaceZaznamuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguraceArchivaceZaznamu;
            cb_ITCastKonfigArchivaceZaznamu_ProductionPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguraceArchivaceZaznamu_Production;
        }

        private void Load_StavSkladu()
        {
            cb_StavSkladuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit;
            cb_StavSkladuAktualniStavSkladuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_AktualnyStav;
            cb_StavSkladuHistoriePohybuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_HistoriePohybu;

        }

        private void Load_Sklad()
        {

            cb_SkladyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].Povolit;

            #region Ciselniky

            cb_SkladyCiselnikyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit;

            cb_SkladyCiselnikySkladyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Sklady;
            cb_SkladyCiselnikyStrediskaPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Strediska;
            cb_SkladyCiselnikyMapaLokaciPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_MapaLokaci;
            cb_SkladyCiselnikyVariantySortimentuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_VariantaLokaciMaterialu;
            cb_SkladyCiselnikyTypyLokaciPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_TypyLokaci;
            cb_SkladyCiselnikyZboziPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Zasoby;
            cb_SkladyCiselnikyAdresarPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Adresar;
            cb_SkladyCiselnikyLokacePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Lokace;
            #endregion

            #region Transakce

            cb_SkladyTransakcePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit;


            cb_SkladyTransakcePrijemPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem;
            cb_SkladyTransakcePrijemPredloha.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Predloha;
            cb_SkladyTransakcePrijemRizeniPriorit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_RizeniPriorit;
            cb_SkladyTransakcePrijemNasnimane.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Nasnimane;


            cb_SkladyTransakceVydejPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej;
            cb_SkladyTransakceVydejPredloha.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Predloha;
            cb_SkladyTransakceVydejRizeniPriorit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_RizeniPriorit;
            cb_SkladyTransakceVydejNasnimane.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Nasnimane;


            cb_SkladyTransakceExpedicePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice;
            cb_SkladyTransakceExpediceDodaciListy.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_DodaciList;
            cb_SkladyTransakceExpediceBaleniAPalety.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_BaleniaPaletiazace;


            cb_SkladyTransakceVolnyPohyb.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb;
            cb_SkladyTransakceVolnyPohybNasnimane.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane;

            cb_SkladyTransakcePrevod.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prevod;

            cb_SkladyTransakceInventura.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura;
            cb_SkladyTransakceInventuraPredloha.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Predloha;
            cb_SkladyTransakceInventuraNasnimane.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Nasnimane;

            #region Volny pohyb

            cb_SkladyTransakceVolnyPohybNasnimane_Odstranit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Odstranit;
            cb_SkladyTransakceVolnyPohybNasnimane_Upravit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Upravit;
            cb_SkladyTransakceVolnyPohybNasnimane_KlavVystup.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_KlavVystup;
            cb_SkladyTransakceVolnyPohybNasnimane_ImpDavku.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_ImpDavku;

            #endregion

            #endregion

            #region Rozbory

            cb_SkladyRozboryPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit;
            cb_SkladyRozboryPohybyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Pohyby;
            cb_SkladyRozboryStavyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Stavy;
            cb_SkladyRozboryLokMechLokMech.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_LokMech;
            cb_SkladyRozboryLokMechStavSkladuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_LokMech_StavSkladu;
            cb_SkladyRozboryInventura.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Inventura;
            cb_SkladyRozboryInventuraStavyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Inventura_StavInventury;

            #endregion

            #region Importni mustek

            cb_SkladyImportniMustekPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Importni_mustek[0].Povolit;

            #endregion



            cb_SkladyStavAutomatickyAktualizovatData.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].StavAutomatickyAktualizovatData;
            cb_SkladyStavZobrazitAktualniMnozstvi.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].StavZobrazitAktualniMnozstvi;


            cb_SkladyPodledyNaDataArchivniPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PodledyNaDataArchivniPovolit;
            tbox_SkladyPohledAktualni.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualni;
            tbox_SkladyPohledArchivni.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledArchivni;
            tbox_SkladyPohledAktualniAArchivni.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualniAArchivni;

            tbox_SkladyLokMechNazevTiskarny.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].FormSkladLokaceStavList_Tiskarna;

            cb_SkladyInventuraPlneniVariantLokaciPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].InventuraPohybyVariantyLokaciPovolit;


            cbox_RFID_baud.DataSource = Enum.GetValues(typeof(Baudrate));
            cbox_RFID_COM.DataSource = Enum.GetValues(typeof(ComPorty));

            cbox_RFID_COM.SelectedItem = (ComPorty)Enum.Parse(typeof(ComPorty), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].ComPort, true);
            cbox_RFID_baud.SelectedItem = (Baudrate)Enum.Parse(typeof(Baudrate), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Baudrate, true);
            tbox_Edit_CmdComAddr.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Address;
            cb_RFIDEnable.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Enable;

        }

        private void Load_Servis()
        {

            cb_ServisPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit;

            cb_ServisCinnostiPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Cinnosti;
            cb_ServisOdberatelePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Odberatele;
            cb_ServisStavyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Stavy;
            cb_ServisOkruhyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Okruhy;

            #region Stavy

            cb_ServisVazbyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit;

            cb_ServisVazbyStavStavNextPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_StavStavNext;
            cb_ServisVazbyCinnostCinnostNextPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_CinnostCinnostNext;
            cb_ServisVazbyOkruhZdrojSeznamPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_OkruhZdrojSeznam;
            cb_ServisVazbyDynamickeTabulkyPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_DynamickeTabulky;

            #endregion

            cb_ServisZdrojePovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Zdroje;
            cb_ServisStavyZdrojuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_StavyZdroju;
            cb_ServisPohybyZdrojuPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_PohybyZdroju;

            cb_ServisReportSestavaPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_ReportSestava;


            cb_ServisTvorbaZdrojePriraditStavOkruhPovolit.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].TvorbaZdrojePriraditStavOkruhPovolit;
            tbox_ServisUmisteniFotografii.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].ImagesDataFileDirectory;

        }

        private void Load_Export()
        {
            cb_ExportyExcelOtevritPoVygenerovani.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelOtevritPoVygenerovani;
            tbox_ExportyExcelFormatDatum.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelFormatDatum;
        }

        private void Load_Tisk()
        {
            cb_Tisky_Leitz_Tiskarna_Pouzivat.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Leitz_Pouzivat;
            cb_Tisky_Ukladat_Stitky_Moduly_All.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Ukladat_Stitky_Do_Souboru;

            TxB_Tisky_Etiketa_logs_Path.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path;


            TxB_Tisky_Etiketa_Klic.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Klic;



            TxB_Tisky_RDLC_Klic.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC;
            TxB_Tisky_ZPL_Klic.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL;


            TxB_Tisky_ZPL_Vychozi_Tiskarna.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Vychozi_Tiskarna;
            TxB_Tisky_RDLC_Vychozi_Tiskarna.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna;
            TxB_Tisky_ZPL_Pocet_Vytisku.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Pocet_Vytisku;
            TxB_Tisky_RDLC_Pocet_Vytisku.Text = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku;

            // Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path = TxB_Tisky_Etiketa_Path.Text;

        }

        #endregion

        private bool SettingsSave()
        {
            try
            {

                Save_System();
                Save_Ukolovani();
                Save_Planovani();
                Save_Vyroba();
                Save_ITCast();
                Save_StavSkladu();
                Save_Sklady();
                Save_Servis();
                Save_Export();
                Save_Tisk();

                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        #region SAVE SubMetody pro lepší přehled

        private void Save_Ukolovani()
        {

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].Povolit = cb_UkolovaniPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].PovolitBarevneZvyrazneniStavu = cb_UkolovaniPovolitBarevneZvyrazneniStavu.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].UkolyPovolit = cb_UkolovaniUkolyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].PrehledUkoluPovolit = cb_UkolovaniPrehledUkoluPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ukolovani[0].HistorieUkoluPovolit = cb_UkolovaniHistorieUkoluPovolit.Checked;

        }

        private void Save_System()
        {

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID = byte.Parse(tp_SystemTID.Text);

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FiltryVazatNaUzivatele = cb_SystemFiltryVazatNaUzivatele.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].DataGridVazatNaUzivatele = cb_SystemDatagridVazatNaUzivatele.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].KonfiguraceHeslo = tbox_HesloKonfigurace.Text;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].AplikacePracovniciPovolit = cb_AplikacePracovniciPovolit.Checked;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl = chB_Log_Tisky_hl.Checked;



        }

        private void Save_Planovani()
        {
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit = cb_PlanyPovolit.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_Plany_VV = cb_Plany_PlanVV_Povolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany = cb_Plany_Kap_Plan.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_KapPlany_Materialy = cb_Plany_Kap_Plan_MaterialPovolit.Checked;


            //zobrazeni vyrabene polozky MaR 20.12.2024
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].Povolit_VyrabenePolozky = chB_Planovani_Zobrazeni_Vyrab_polozky.Checked;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVyrobnyPrikaz = cb_VyrobaPriZaplanovaniVyrobnyPrikaz.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVydej = cb_VyrobaPriZaplanovaniVydej.Checked ;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani_VV_Params[0].Navrhar_MJ_Separator = tb_Plany_PlanVV_MJ_Separator.Text ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani_VV_Params[0].Navrhar_MJ_Text = tb_Plany_PlanVV_MJ_Text.Text ;

        }

        private void Save_Vyroba()
        {

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].Povolit = cb_VyrobaPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].EtiketaTISKUkladat = cb_VyrobaUkladaniEtiket.Checked;

            //MaR 7.2.2025 pridano kontrola UDI
            //chB_VH_kontrola_UDI.Checked
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].kontrolaDatUDI = chB_VH_kontrola_UDI.Checked;

            //zobrazeni vyrabene polozky MaR 20.12.2024
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_VyrabenePolozky=chB_Vyroba_Rozbory_Zobrazeni_Vyr_Pol.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].PouzivatTabulkuZbozi = cb_VyrobaPouzivatTabulkuZbozi.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].VyrobniPrikazy_GenerovatSN = cb_VyrobaVPP_GenerovatSN.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].OdvodPOHODATyp = ((Konzola.Vyroba.TypZpravovaniVyroby)cbox_TypDovodu.SelectedItem).ToString();

            #region Ciselniky

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit = cb_Vyr_Zob_Ciselniky.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Skupiny = cb_VyrobaSkupinyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Zasoby = cb_VyrobaZboziPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_VazbyMaterialy = cb_VyrobaVazbaMaterialy.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Stroje = cb_VyrobaStrojePovolit.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Moduly = cb_VyrobaModulyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ciselniky[0].Povolit_Prevodniky = cb_VyrobaPrevodnikyPovolit.Checked;
            #endregion

            #region Transakce

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit = cb_VyrobaTransakcePovolit.Checked;

            
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit_VyrobniPrikazy = cb_VyrobaVyrobniPrikazyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Transakce[0].Povolit_OdvodPohoda = cb_VyrobaOdvodPOHODAPovolit.Checked;

            #endregion

            #region Rozbory

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit = cb_VyrobaRozboryPovolit.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledPlanVyroby = cb_VyrobaPrehledPlanVyrobyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledOdvadeniStroju = cb_VyrobaPrehledOdvodEvents.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledVyrobky = cb_VyrobaPrehledVyrobkyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledVyrobkySNSarze = cb_VyrobaPrehledVyrobkySNSarzePovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledMaterialy = cb_VyrobaPrehledMaterialyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledOdvadeniStrojuErr = cb_VyrobaPrehledOdvodEventsErr.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].Povolit_PrehledStavyStroju = cb_VyrobaPrehledStavyStrojuPovolit.Checked;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku = tbox_VyrobaNazevKSabloneTisku.Text;


            //TODO MaR 27.6. 2023 doplneni konfigurace o pocet desetinnych mist hodnotami z enum
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist = cB_prehled_vyrobky.SelectedItem.ToString(); 

            #endregion

        }

        private void Save_ITCast()
        {
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit = cb_ITCastPovolit.Checked ;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_IP_Terminalu = cb_ITCastIPTerminaluPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_Test_Disp = cb_ITCastTestDispPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_TestDisp2 = cb_ITCastTestDisp2Povolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_TypyDokladu = cb_ITCastTypyDokladuPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_FaskRady = cb_ITCastRadyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguracePOHODA = cb_ITCastKonfigPOHODAPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguraceArchivaceZaznamu = cb_ITCastKonfigArchivaceZaznamuPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.IT_Cast[0].Povolit_KonfiguraceArchivaceZaznamu_Production = cb_ITCastKonfigArchivaceZaznamu_ProductionPovolit.Checked;
        }

        private void Save_StavSkladu()
        {
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit = cb_StavSkladuPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_AktualnyStav = cb_StavSkladuAktualniStavSkladuPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.StavSkladu[0].Povolit_HistoriePohybu = cb_StavSkladuHistoriePohybuPovolit.Checked ;

        }

        private void Save_Sklady()
        {

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].Povolit = cb_SkladyPovolit.Checked ;

            #region Ciselniky

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit = cb_SkladyCiselnikyPovolit.Checked ;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Sklady = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Sklady = cb_SkladyCiselnikySkladyPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Strediska = cb_SkladyCiselnikyStrediskaPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_MapaLokaci = cb_SkladyCiselnikyMapaLokaciPovolit.Checked  ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_VariantaLokaciMaterialu = cb_SkladyCiselnikyVariantySortimentuPovolit.Checked  ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_TypyLokaci = cb_SkladyCiselnikyTypyLokaciPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Zasoby = cb_SkladyCiselnikyZboziPovolit.Checked  ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Adresar = cb_SkladyCiselnikyAdresarPovolit.Checked  ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ciselniky[0].Povolit_Lokace = cb_SkladyCiselnikyLokacePovolit.Checked;
            #endregion

            #region Transakce

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit = cb_SkladyTransakcePovolit.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem = cb_SkladyTransakcePrijemPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Predloha = cb_SkladyTransakcePrijemPredloha.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_RizeniPriorit = cb_SkladyTransakcePrijemRizeniPriorit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prijem_Nasnimane = cb_SkladyTransakcePrijemNasnimane.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej = cb_SkladyTransakceVydejPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Predloha = cb_SkladyTransakceVydejPredloha.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_RizeniPriorit = cb_SkladyTransakceVydejRizeniPriorit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Vydej_Nasnimane = cb_SkladyTransakceVydejNasnimane.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice = cb_SkladyTransakceExpedicePovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_DodaciList = cb_SkladyTransakceExpediceDodaciListy.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Expedice_BaleniaPaletiazace = cb_SkladyTransakceExpediceBaleniAPalety.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb = cb_SkladyTransakceVolnyPohyb.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane = cb_SkladyTransakceVolnyPohybNasnimane.Checked ;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Prevod = cb_SkladyTransakcePrevod.Checked ;

             Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura = cb_SkladyTransakceInventura.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Predloha = cb_SkladyTransakceInventuraPredloha.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_Inventura_Nasnimane = cb_SkladyTransakceInventuraNasnimane.Checked ;

            #region Volny pohyb

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Odstranit = cb_SkladyTransakceVolnyPohybNasnimane_Odstranit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Upravit = cb_SkladyTransakceVolnyPohybNasnimane_Upravit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_KlavVystup = cb_SkladyTransakceVolnyPohybNasnimane_KlavVystup.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_ImpDavku = cb_SkladyTransakceVolnyPohybNasnimane_ImpDavku.Checked;
            #endregion


            #endregion

            #region Rozbory

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit = cb_SkladyRozboryPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Pohyby = cb_SkladyRozboryPohybyPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Stavy = cb_SkladyRozboryStavyPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_LokMech = cb_SkladyRozboryLokMechLokMech.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_LokMech_StavSkladu = cb_SkladyRozboryLokMechStavSkladuPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Inventura = cb_SkladyRozboryInventura.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].Povolit_Inventura_StavInventury = cb_SkladyRozboryInventuraStavyPovolit.Checked ;

         


            #endregion

            #region Importni mustek

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Importni_mustek[0].Povolit = cb_SkladyImportniMustekPovolit.Checked;

            #endregion



            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].StavAutomatickyAktualizovatData = cb_SkladyStavAutomatickyAktualizovatData.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].StavZobrazitAktualniMnozstvi = cb_SkladyStavZobrazitAktualniMnozstvi.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PodledyNaDataArchivniPovolit = cb_SkladyPodledyNaDataArchivniPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualni = tbox_SkladyPohledAktualni.Text ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledArchivni = tbox_SkladyPohledArchivni.Text ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualniAArchivni = tbox_SkladyPohledAktualniAArchivni.Text ;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].FormSkladLokaceStavList_Tiskarna = tbox_SkladyLokMechNazevTiskarny.Text;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].InventuraPohybyVariantyLokaciPovolit = cb_SkladyInventuraPlneniVariantLokaciPovolit.Checked ;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Address = tbox_Edit_CmdComAddr.Text;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].ComPort = cbox_RFID_COM.SelectedItem.ToString();
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Baudrate = cbox_RFID_baud.SelectedItem.ToString();
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Enable = cb_RFIDEnable.Checked;
        }

        private void Save_Servis()
        {

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit = cb_ServisPovolit.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Cinnosti = cb_ServisCinnostiPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Odberatele = cb_ServisOdberatelePovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Stavy = cb_ServisStavyPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Okruhy = cb_ServisOkruhyPovolit.Checked ;

            #region Vazby

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit = cb_ServisVazbyPovolit.Checked ;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_StavStavNext = cb_ServisVazbyStavStavNextPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_CinnostCinnostNext = cb_ServisVazbyCinnostCinnostNextPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_OkruhZdrojSeznam = cb_ServisVazbyOkruhZdrojSeznamPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Vazby[0].Povolit_DynamickeTabulky = cb_ServisVazbyDynamickeTabulkyPovolit.Checked ;

            #endregion

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_Zdroje = cb_ServisZdrojePovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_StavyZdroju= cb_ServisStavyZdrojuPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_PohybyZdroju = cb_ServisPohybyZdrojuPovolit.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].Povolit_ReportSestava = cb_ServisReportSestavaPovolit.Checked;

            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].TvorbaZdrojePriraditStavOkruhPovolit = cb_ServisTvorbaZdrojePriraditStavOkruhPovolit.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].ImagesDataFileDirectory = tbox_ServisUmisteniFotografii.Text;

        }

        private void Save_Export()
        {
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelOtevritPoVygenerovani = cb_ExportyExcelOtevritPoVygenerovani.Checked ;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelFormatDatum = tbox_ExportyExcelFormatDatum.Text ;
        }

        private void Save_Tisk()
        {
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Leitz_Pouzivat = cb_Tisky_Leitz_Tiskarna_Pouzivat.Checked;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Ukladat_Stitky_Do_Souboru = cb_Tisky_Ukladat_Stitky_Moduly_All.Checked;
           
            
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path = TxB_Tisky_Etiketa_logs_Path.Text;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Klic = TxB_Tisky_Etiketa_Klic.Text;



            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC = TxB_Tisky_RDLC_Klic.Text;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL = TxB_Tisky_ZPL_Klic.Text;


            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Vychozi_Tiskarna = TxB_Tisky_ZPL_Vychozi_Tiskarna.Text;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna = TxB_Tisky_RDLC_Vychozi_Tiskarna.Text;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Pocet_Vytisku = TxB_Tisky_ZPL_Pocet_Vytisku.Text;
            Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku = TxB_Tisky_RDLC_Pocet_Vytisku.Text;

            //cb_Tisky_Ukladat_Stitky_Moduly_All.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Ukladat_Stitky_Do_Souboru;

        }

        #endregion


        #region Click to Link

        private void linkLabel_Tiskarny_Vyrobci_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel)
                _Support_.LinkProcess.Show(sender as LinkLabel, e);
        }

        #endregion

    }
}
