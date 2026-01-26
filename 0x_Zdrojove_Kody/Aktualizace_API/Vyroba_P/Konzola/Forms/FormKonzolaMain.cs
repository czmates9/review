using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fask.Aktualizace_API.Konzola.Data;
using System.IO;
using JR.Utils.GUI.Forms;
using System.IO.Compression;
using Ionic.Zip;
using System.Diagnostics;
using Fask.Aktualizace_API.FileTransfer;
using System.Xml.Linq;

namespace Fask.Aktualizace_API.Konzola.Forms
{
    public partial class FormKonzolaMain : Form
    {

        private string userPermissions;
        private string pathLocal = string.Empty;
        private string pathZaloha = string.Empty;
        private string pathAktualizace = string.Empty;
        private string pathZalohaFile = string.Empty;
        private string pathAktualizovanyFile = string.Empty;
        private string pathObnova = string.Empty;


        public enum Prava
        {
            Zaloha,
            ZalohaSAktualizaci,
            AktualizaceBezZalohy,
            ZobrazeniObsahuLokalnihoAdresare,
            DefaultniPravoProVsechno
        }

        public FormKonzolaMain()
        {
            InitializeComponent();
            SetButtonWidth();
        }

        #region konstruktor
        // Konstruktor s vstupním parametrem pro práva uživatele
        public FormKonzolaMain(string permissions)
        {
            InitializeComponent();
            userPermissions = permissions;

            SetButtonWidth();

            // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
            DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
            SetParametry(initialDataTable);
        }
        #endregion


        private void SetButtonWidth()
        {
            // Získáme šířku primárního monitoru
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Nastavíme šířku tlačítka na 80% šířky monitoru
            //int buttonWidth = (int)(screenWidth * 0.33);
            int buttonWidth = (int)(screenWidth * 0.25);

            #region old
            //// Nastavíme šířku tlačítka
            //btn_ukonceni.Location = new Point(0 * buttonWidth, btn_ukonceni.Location.Y);
            //btn_ukonceni.MinimumSize = new Size(buttonWidth, 0);
            //// btn_ukonceni.MaximumSize = new Size(buttonWidth, 0);

            ////buttonStorno.Width = buttonWidth;
            //btn_zaloha.Location = new Point(1 * buttonWidth, btn_ukonceni.Location.Y);
            //btn_zaloha.MinimumSize = new Size(buttonWidth, 0);
            ////btn_vlozit.Width = buttonWidth;
            //btn_obnova.Location = new Point(2 * buttonWidth, btn_ukonceni.Location.Y);
            //btn_obnova.MinimumSize = new Size(buttonWidth, 0);


            //btn_aktualizace.Location = new Point(3 * buttonWidth, btn_ukonceni.Location.Y);
            //btn_aktualizace.MinimumSize = new Size(buttonWidth, 0);
            ////buttonOK.Width = buttonWidth; 
            #endregion


            #region new 23.8. 2024
            // Nastavíme šířku tlačítka
            btn_ukonceni.Location = new Point(0 * buttonWidth, btn_ukonceni.Location.Y);
            btn_ukonceni.MinimumSize = new Size(buttonWidth, 0);
            // btn_ukonceni.MaximumSize = new Size(buttonWidth, 0);

            //buttonStorno.Width = buttonWidth;
            btn_zaloha.Location = new Point(1 * buttonWidth, btn_ukonceni.Location.Y);
            btn_zaloha.MinimumSize = new Size(buttonWidth, 0);
            //btn_vlozit.Width = buttonWidth;
            btn_obnova.Location = new Point(3 * buttonWidth, btn_ukonceni.Location.Y);
            btn_obnova.MinimumSize = new Size(buttonWidth, 0);


            btn_aktualizace.Location = new Point(2 * buttonWidth, btn_ukonceni.Location.Y);
            btn_aktualizace.MinimumSize = new Size(buttonWidth, 0);
            //buttonOK.Width = buttonWidth;
            #endregion

        }

        // Event handler pro událost FormClosing
        private void FormKonzolaMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public static void AdjustControlsToScreen(Form form)
        {
            // Procházíme všechny ovládací prvky na formuláři
            foreach (Control control in form.Controls)
            {
                // Nastavíme vlastnost Anchor tak, aby se ovládací prvky přizpůsobily změně velikosti okna
                control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void AdjustControlsToPanelSize(Panel panel)
        {
            // Procházíme všechny ovládací prvky v panelu
            foreach (Control control in panel.Controls)
            {
                // Aktualizujeme velikost a umístění ovládacích prvků v závislosti na velikosti panelu
                control.Size = new Size(panel.ClientSize.Width - control.Margin.Horizontal, control.Height);
                control.Location = new Point(control.Margin.Left, control.Top);
            }
        }

        private void AdjustAllGroupBoxesToFormSize(Panel panel, Form form)
        {
            // Procházíme všechny ovládací prvky ve formuláři
            foreach (Control control in panel.Controls)
            {
                // Pokud je prvek typu GroupBox, aplikujeme na něj nastavení velikosti
                if (control is GroupBox groupBox)
                {
                    // Aktualizujeme velikost a umístění GroupBox v závislosti na velikosti formuláře
                    groupBox.Width = form.ClientSize.Width - groupBox.Margin.Horizontal;
                    groupBox.Height = form.ClientSize.Height - groupBox.Margin.Vertical;
                    groupBox.Location = new Point(groupBox.Margin.Left, groupBox.Margin.Top);
                }
            }
        }

        // Event handler pro událost Load formuláře
        private void FormKonzolaMain_Load(object sender, EventArgs e)
        {
            // Zde můžete provést inicializaci formuláře nebo jiné akce po jeho načtení
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);

           // AdjustAllGroupBoxesToFormSize(panelTop,this); // this je váš formulář
                                                 // AdjustControlsToPanelSize(panelTop); // panel1 je váš panel
                                                 // AdjustControlsToScreen(this);

            txtLocalPath.Enabled = false;

            chB_Akt_vse.Visible = false;

             pathLocal = string.Empty;
             pathZaloha = string.Empty;
             pathZalohaFile = string.Empty;
             pathAktualizace = string.Empty;
             pathObnova = string.Empty;

#if DEBUG
            txtLocalPath.Text = @"C:\FASK\Konzola_aktualizace\Konzole_old\Konzole";
            txtlPathZaloha.Text = @"C:\FASK\Konzola_aktualizace\20240314_Konzola_zaloha";
            txtlPathZdrojAkt.Text = @"C:\FASK\Konzola_aktualizace\Konzole_aktualizace\Konzole";
            pathAktualizace = @"C:\FASK\Konzola_aktualizace\Konzole_aktualizace\Konzole";
#endif


            SettingsLoadOld();
            DisableButton(true);
            //panelButtons_Resize(null, null);
            // Inicializace ProgressBaru před začátkem operace.
            toolStripProgressBar1.Value = 0; // Nastaví hodnotu na začátek.
            toolStripProgressBar1.Visible = false;
        }

        private void DisableButton(bool aktivace)
        {
            if(aktivace)
            {
                btn_aktualizace.Enabled = 
                btn_localPath.Enabled = 
                btn_PathAktualizace.Enabled =
                btn_obnovaPath.Enabled =
                //btn_PathAutomat.Enabled = true;
                btn_PathZaloha.Enabled = 
                btn_zaloha.Enabled =
                btn_PathNacist.Enabled =
                btn_ulozPath.Enabled =
                btn_obnova.Enabled =
                btn_ukonceni.Enabled = true;
            }
            else
            {
                btn_aktualizace.Enabled = 
                btn_localPath.Enabled = 
                btn_PathAktualizace.Enabled =
                btn_obnovaPath.Enabled =
                btn_obnova.Enabled =
                //btn_PathAutomat.Enabled = true;
                btn_PathZaloha.Enabled = 
                btn_zaloha.Enabled =
                btn_PathNacist.Enabled =
                btn_ulozPath.Enabled =
                btn_ukonceni.Enabled = false;
            }

#if !DEBUG
            btn_obnovaPath.Enabled =
          btn_obnova.Enabled = false; 
#endif

        }


        #region parametry konzole

        private DS_konzola.Konzola_parametryDataTable dt_parametry = null;

        // Veřejná metoda pro získání hodnoty dt_parametry
        public DS_konzola.Konzola_parametryDataTable GetParametry()
        {
            return dt_parametry;
        }

        // Veřejná metoda pro nastavení hodnoty dt_parametry
        public void SetParametry(DS_konzola.Konzola_parametryDataTable dt)
        {
            dt_parametry = dt;
        }
        #endregion

        #region vyber cest adresaru

        // Metoda pro otevření dialogu pro výběr adresáře a zápis cesty do proměnné dt_parametry
        public void VyberAdresarLocal()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                // Nastavení výchozího adresáře pro dialog na MyDocuments
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                dialog.Description = "Vyberte cestu k adresáři";

                // Zobrazení dialogu a po potvrzení výběru adresáře zápis cesty do proměnné dt_parametry
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string vybranaCesta = dialog.SelectedPath;
                    pathLocal = vybranaCesta;

                    // Kontrola, zda je dt_parametry inicializována a prázdná
                    if (dt_parametry == null)
                    {
                        // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
                        DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
                        SetParametry(initialDataTable);
                        PridejLocalCestuDoTabulky(vybranaCesta);
                    }
                    // Pokud proměnná dt_parametry již obsahuje data, ponecháme ji tak, jak je
                    else
                    {
                        PridejLocalCestuDoTabulky(vybranaCesta);
                    }
                }
            }
        }


        // Metoda pro otevření dialogu pro výběr adresáře a zápis cesty do proměnné dt_parametry
        public void VyberAdresarZaloha()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                // Nastavení výchozího adresáře pro dialog na MyDocuments
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                dialog.Description = "Vyberte cestu k adresáři zálohy";

                // Zobrazení dialogu a po potvrzení výběru adresáře zápis cesty do proměnné dt_parametry
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string vybranaCesta = dialog.SelectedPath;
                    pathZaloha = vybranaCesta;

                    // Kontrola, zda je dt_parametry inicializována a prázdná
                    if (dt_parametry == null)
                    {
                        // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
                        DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
                        SetParametry(initialDataTable);
                        PridejZalohaCestuDoTabulky(vybranaCesta);
                    }
                    // Pokud proměnná dt_parametry již obsahuje data, ponecháme ji tak, jak je
                    else
                    {
                        PridejZalohaCestuDoTabulky(vybranaCesta);
                    }
                }
            }
        }

        // Metoda pro otevření dialogu pro výběr adresáře a zápis cesty do proměnné dt_parametry
        public void VyberAdresarAktualizace()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                // Nastavení výchozího adresáře pro dialog na MyDocuments
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                dialog.Description = "Vyberte cestu k adresáři aktualizace";

                // Zobrazení dialogu a po potvrzení výběru adresáře zápis cesty do proměnné dt_parametry
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string vybranaCesta = dialog.SelectedPath;
                    pathAktualizace = vybranaCesta;

                    // Kontrola, zda je dt_parametry inicializována a prázdná
                    if (dt_parametry == null)
                    {
                        // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
                        DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
                        SetParametry(initialDataTable);
                        PridejAktualizaceCestuDoTabulky(vybranaCesta);
                    }
                    // Pokud proměnná dt_parametry již obsahuje data, ponecháme ji tak, jak je
                    else
                    {
                        PridejAktualizaceCestuDoTabulky(vybranaCesta);
                    }
                }
            }
        }

        public void VyberAdresarObnova()
        {
            #region new

            using (var dialog = new OpenFileDialog())
            {
                // Nastavení výchozího adresáře pro dialog na MyDocuments
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                dialog.Title = "Vyberte cestu k zazipovanému souboru obnovy";

                // Zobrazení dialogu a po potvrzení výběru adresáře/souboru zápis cesty do proměnné pathObnova
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //string vybranaCesta = Path.GetFileName(dialog.FileName);// Path.GetDirectoryName(dialog.FileName);
                    string vybranaCesta = dialog.FileName;
                    pathObnova = vybranaCesta;

                    // Kontrola, zda je dt_parametry inicializována a prázdná
                    if (dt_parametry == null)
                    {
                        // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
                        DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
                        SetParametry(initialDataTable);
                        PridejObnovaCestuDoTabulky(vybranaCesta);
                    }
                    // Pokud proměnná dt_parametry již obsahuje data, ponecháme ji tak, jak je
                    else
                    {
                        PridejObnovaCestuDoTabulky(vybranaCesta);
                    }
                }
            }
            #endregion


            #region old
            //using (var dialog = new FolderBrowserDialog())
            //{
            //    // Nastavení výchozího adresáře pro dialog na MyDocuments
            //    dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            //    dialog.Description = "Vyberte cestu k adresáři obnovy";

            //    // Zobrazení dialogu a po potvrzení výběru adresáře zápis cesty do proměnné dt_parametry
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string vybranaCesta = dialog.SelectedPath;
            //        pathObnova = vybranaCesta;

            //        // Kontrola, zda je dt_parametry inicializována a prázdná
            //        if (dt_parametry == null)
            //        {
            //            // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
            //            DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
            //            SetParametry(initialDataTable);
            //            PridejObnovaCestuDoTabulky(vybranaCesta);
            //        }
            //        // Pokud proměnná dt_parametry již obsahuje data, ponecháme ji tak, jak je
            //        else
            //        {
            //            PridejObnovaCestuDoTabulky(vybranaCesta);
            //        }
            //    }
            //} 
            #endregion
        }
        #endregion

        #region pridani cest do tabulky dt
        // Metoda pro vytvoření nového řádku s cestou a jeho přidání do tabulky dt_parametry
        public void PridejLocalCestuDoTabulky(string vybranaCesta)
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count == 0)
            {
                // Vytvoření nového řádku
                DS_konzola.Konzola_parametryRow row = dt_parametry.NewKonzola_parametryRow();
                // Přiřazení cesty k novému řádku
                row.path_lokal_konzola = vybranaCesta;
                // Přidání nového řádku do tabulky
                dt_parametry.AddKonzola_parametryRow(row);
            }
            else if (dt_parametry != null && dt_parametry.Rows.Count == 1)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Přiřazení cesty k řádku
                row.path_lokal_konzola = vybranaCesta;
            }
        }

        public void PridejZalohaCestuDoTabulky(string vybranaCesta)
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count == 0)
            {
                // Vytvoření nového řádku
                DS_konzola.Konzola_parametryRow row = dt_parametry.NewKonzola_parametryRow();
                // Přiřazení cesty k novému řádku
                row.path_server_zaloha = vybranaCesta;
                // Přidání nového řádku do tabulky
                dt_parametry.AddKonzola_parametryRow(row);
            }
            else if (dt_parametry != null && dt_parametry.Rows.Count == 1)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Přiřazení cesty k řádku
                row.path_server_zaloha = vybranaCesta;
            }
        }

        public void PridejAktualizaceCestuDoTabulky(string vybranaCesta)
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count == 0)
            {
                // Vytvoření nového řádku
                DS_konzola.Konzola_parametryRow row = dt_parametry.NewKonzola_parametryRow();
                // Přiřazení cesty k novému řádku
                row.path_server_aktualizace = vybranaCesta;
                // Přidání nového řádku do tabulky
                dt_parametry.AddKonzola_parametryRow(row);
            }
            else if (dt_parametry != null && dt_parametry.Rows.Count == 1)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Přiřazení cesty k řádku
                row.path_server_aktualizace = vybranaCesta;
            }
        }


        public void PridejObnovaCestuDoTabulky(string vybranaCesta)
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count == 0)
            {
                // Vytvoření nového řádku
                DS_konzola.Konzola_parametryRow row = dt_parametry.NewKonzola_parametryRow();
                // Přiřazení cesty k novému řádku
                row.path_obnova = vybranaCesta;
                // Přidání nového řádku do tabulky
                dt_parametry.AddKonzola_parametryRow(row);
            }
            else if (dt_parametry != null && dt_parametry.Rows.Count == 1)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Přiřazení cesty k řádku
                row.path_obnova = vybranaCesta;
            }
        }
        #endregion

        // Metoda pro vypsání první cesty z dt_parametry do textového pole
        public void VypisCestyDoTextovehoPole()
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count > 0)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Vypsání hodnoty vlastnosti path_lokal_konzola do textového pole
                txtLocalPath.Text = row.Ispath_lokal_konzolaNull() ? string.Empty : row.path_lokal_konzola;
                txtlPathZaloha.Text = row.Ispath_server_zalohaNull() ? string.Empty : row.path_server_zaloha;
                txtlPathZdrojAkt.Text = row.Ispath_server_aktualizaceNull() ? string.Empty : row.path_server_aktualizace;
                txtlPathObnova.Text = row.Ispath_obnovaNull() ? string.Empty : row.path_obnova;
            }
            else
            {
                // Pokud dt_parametry není inicializováno nebo neobsahuje žádné řádky, můžeme provést další kroky nebo zobrazit chybu
                // MessageBox.Show("Datová tabulka dt_parametry není inicializována nebo neobsahuje žádné řádky.");
            }
        }


        #region akce zalohy
        // Metoda pro vypsání první cesty z dt_parametry do textového pole
        public bool OverCestProZalohu()
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count > 0)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Vypsání hodnoty vlastnosti path_lokal_konzola do textového pole
                txtLocalPath.Text = row.Ispath_lokal_konzolaNull() ? string.Empty : row.path_lokal_konzola;
                txtlPathZaloha.Text = row.Ispath_server_zalohaNull() ? string.Empty : row.path_server_zaloha;
            }

            // Kontrola, zda je inicializována proměnná dt_parametry
            if (!string.IsNullOrEmpty(txtLocalPath.Text) && !string.IsNullOrEmpty(txtlPathZaloha.Text))
            {

                pathLocal = txtLocalPath.Text;
                pathZaloha = txtlPathZaloha.Text;
                return true;
            }

            return false;
        }

        public bool Zalohovani()
        {

            try
            {

                if (OverCestProZalohu())
                {
                    ZalohovatAdresar(pathLocal, pathZaloha);
                    pathLocal = string.Empty;
                    pathZaloha = string.Empty;
                }
                else
                {
                    MessageBox.Show("Nejsou vybrány cesty k adresářům!");
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
                return false;
                //MessageBox.Show("Zálohování selhalo!");
            }
        }


        public void ZalohovatAdresar(string cestaKAdresari, string cestaZaloha)
        {
            try
            {
                // Kontrola, zda existuje zadaný adresář
                if (!Directory.Exists(cestaKAdresari))
                {
                    MessageBox.Show("Adresář neexistuje.");
                    return;
                }

                // Extrahování názvu adresáře z cesty
                string nazevAdresare = new DirectoryInfo(cestaKAdresari).Name;

                // Vytvoření názvu zip souboru
                string zipSoubor = Path.Combine(cestaZaloha, $"{DateTime.Now:yyyyMMdd_HHmm}_{nazevAdresare}.zip");

                // Vytvoření názvu zip souboru
                string zipSouborAktualizace = Path.Combine(cestaZaloha, $"{DateTime.Now:yyyyMMdd_HHmm}_{nazevAdresare}_po.zip");

                // Kontrola, zda již existuje zazipovaný soubor
                int verze = 0;
                while (File.Exists(zipSoubor))
                {
                    // Pokud soubor existuje, zvýšíme verzi
                    verze++;
                    //zipSoubor = Path.Combine(cestaZaloha, $"{DateTime.Now:yyyyMMdd}_{nazevAdresare}_verze{verze}.zip");

                    // Dotázání uživatele, zda přepsat existující soubor nebo vytvořit nový s přídavkem verze
                    DialogResult dialogResult = MessageBox.Show($"Soubor {zipSoubor} již existuje. Přejete si přepsat existující soubor?", "Přepsat soubor?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        // Uživatel zrušil operaci
                        return;
                    }
                    else if (dialogResult == DialogResult.Yes)
                    {
                        // Uživatel souhlasí s přepsáním existujícího souboru
                        break;
                    }
                }

                // Zazipování adresáře
                //using (ZipFile zip = new ZipFile())
                //{
                //    zip.AddDirectory(cestaKAdresari); // Přidání adresáře do zip souboru
                //    zip.Save(zipSoubor); // Uložení zip souboru
                //    zip.Dispose();
                //}

                string sourceDirectory = @"C:\Path\To\Your\Directory";
                string zipFilePath = @"C:\Path\To\Your\ZipFile.zip";

                 sourceDirectory = cestaKAdresari;
                 zipFilePath = zipSoubor;

                try
                {
                    CreateZipFromDirectory(sourceDirectory, zipFilePath);
                    Console.WriteLine("Adresář byl úspěšně zazipován.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při zazipování adresáře: {ex.Message}");
                }

                // Vypsání zprávy o dokončení zálohování
                //MessageBox.Show($"Adresář byl úspěšně zazipován a zkopírován do {zipSoubor}.");

                // Uložení cesty k záloze
                pathZalohaFile = zipSoubor;

                pathAktualizovanyFile = zipSouborAktualizace;

                MessageBox.Show($"Úspěšně zálohováno: {zipSoubor}");
            }
            catch (Exception ex)
            {
                // Vypsání chybové zprávy při chybě zálohování
                MessageBox.Show($"Chyba při zálohování adresáře: {ex.Message}");
            }
        }

        static void CreateZipFromDirectory(string sourceDirectory, string zipFilePath)
        {
            // Vytvoření nového zip archivu
            using (FileStream zipFileStream = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                {
                    // Rekurzivní procházení adresáře a přidání všech souborů a podadresářů do zip archivu
                    AddDirectoryToZip(sourceDirectory, zipArchive, sourceDirectory.Length);
                }
            }
        }

        static void AddDirectoryToZip(string sourceDirectory, ZipArchive zipArchive, int rootLength)
        {
            try
            {
                foreach (string filePath in Directory.GetFiles(sourceDirectory))
                {
                    string entryName = filePath.Substring(rootLength).Replace("\\", "/");
                    ZipArchiveEntry entry = zipArchive.CreateEntry(entryName);

                    using (Stream entryStream = entry.Open())
                    {
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                }

                foreach (string directoryPath in Directory.GetDirectories(sourceDirectory))
                {
                    AddDirectoryToZip(directoryPath, zipArchive, rootLength);
                }
            }
            catch (Exception ex)
            {

                // Vypsání chybové zprávy při chybě zálohování
                MessageBox.Show($"Chyba při zálohování adresáře, vytváření zip souboru pod adresáře: {ex.Message}");
            }
        }

        #endregion

        #region akce aktualizace
        // Metoda pro vypsání první cesty z dt_parametry do textového pole
        public bool OverExistenceZalohy()
        {


            // Kontrola, zda existuje zadaný adresář
            if (File.Exists(pathZalohaFile))
            {
                return true;
            }

            return false;
        }

        public bool OverExistenceAktualizace()
        {


            // Kontrola, zda existuje zadaný adresář
            if (Directory.Exists(pathAktualizace))
            {
                return true;
            }

            return false;
        }

    public bool OverCestProAktualizaci()
    {
        // Kontrola, zda je inicializována proměnná dt_parametry a má alespoň jeden řádek
        if (dt_parametry != null && dt_parametry.Rows.Count > 0)
        {
            var row = dt_parametry[0] as DS_konzola.Konzola_parametryRow;

            if (row != null)
            {
                txtLocalPath.Text = row.Ispath_lokal_konzolaNull() ? string.Empty : row.path_lokal_konzola;
                txtlPathZdrojAkt.Text = row.Ispath_server_aktualizaceNull() ? string.Empty : row.path_server_aktualizace;
            }
        }

        // Kontrola, zda textová pole nejsou prázdná nebo whitespace
        if (!string.IsNullOrWhiteSpace(txtLocalPath.Text) && !string.IsNullOrWhiteSpace(txtlPathZdrojAkt.Text))
        {
            pathLocal = txtLocalPath.Text;
            pathAktualizace = txtlPathZdrojAkt.Text;


                bool localExists = Directory.Exists(pathLocal);
                bool aktualizaceExists = Directory.Exists(pathAktualizace);

                if (localExists && aktualizaceExists)
                {
                    return true;
                }

                if (!localExists)
                {
                    string msg = $"Adresář konzola stará verze neexistuje: {pathLocal}";
                    MessageBox.Show(msg, "Chyba cesty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //ZapisDoErrorLogu(msg);
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);

                    //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }

                if (!aktualizaceExists)
                {
                    string msg = $"Adresář konzola nová verze neexistuje: {pathAktualizace}";
                    MessageBox.Show(msg, "Chyba cesty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //ZapisDoErrorLogu(msg);
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                }
            }

        // Pokud podmínky nejsou splněny, vrať false
        return false;
    }

    #region 16.6. MaR zakomentoval
    //// Metoda pro vypsání první cesty z dt_parametry do textového pole
    //public bool OverCestProAktualizaci()
    //{
    //    // Kontrola, zda je inicializována proměnná dt_parametry
    //    if (dt_parametry != null && dt_parametry.Rows.Count > 0)
    //    {
    //        // Získání prvního řádku z dt_parametry
    //        DS_konzola.Konzola_parametryRow row = dt_parametry[0];
    //        // Vypsání hodnoty vlastnosti path_lokal_konzola do textového pole
    //        txtLocalPath.Text = row.Ispath_lokal_konzolaNull() ? string.Empty : row.path_lokal_konzola;
    //        txtlPathZdrojAkt.Text = row.Ispath_server_aktualizaceNull() ? string.Empty : row.path_server_aktualizace;
    //    }

    //    // Kontrola, zda je inicializována proměnná dt_parametry
    //    if (!string.IsNullOrEmpty(txtLocalPath.Text) && !string.IsNullOrEmpty(txtlPathZdrojAkt.Text))
    //    {

    //        pathLocal = txtLocalPath.Text;
    //        pathAktualizace = txtlPathZdrojAkt.Text;
    //        return true;
    //    }

    //    return false;
    //} 
    #endregion

    public bool Aktualizace()
        {

            try
            {


                if (OverCestProAktualizaci())
                {
                    VytvorAktualizovanyAdresar(pathLocal, pathAktualizace);
                    pathLocal = string.Empty;
                    pathAktualizace = string.Empty;
                }
                else
                {
                   // MessageBox.Show("Nejsou vybrány cesty k adresářům!");
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
                return false;
                //MessageBox.Show("Zálohování selhalo!");
            }
        }



        #region predpriprava aktualizace Mar 14.3.2024

        /// <summary>
        /// Najde vsechny spustene Konzoli
        /// </summary>
        /// <returns>true pokud je Konzola spustena</returns>
        public bool UkonciProces()
        {

            string processName = "Konzola";

            // Najdeme proces podle názvu exe souboru
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Count() > 0)
            {
                //// Pokud je proces nalezen, ukončíme ho
                //foreach (Process process in processes)
                //{
                //    process.Kill();
                //    Console.WriteLine($"Proces {process.ProcessName} byl ukončen.");
                //}

                return true;
            }
            else
            {
                Console.WriteLine("Proces nebyl nalezen.");
                //Logging.ExceptionHandler2.Handle("asda");
                return false;
            }


        }


        #region old
        //public void UkonciProces()
        //{

        //    string processName = "Konzola";

        //    // Najdeme proces podle názvu exe souboru
        //    Process[] processes = Process.GetProcessesByName(processName);

        //    if (processes.Count() > 0)
        //    {
        //        // Pokud je proces nalezen, ukončíme ho
        //        foreach (Process process in processes)
        //        {
        //            process.Kill();
        //            Console.WriteLine($"Proces {process.ProcessName} byl ukončen.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Proces nebyl nalezen.");
        //        //Logging.ExceptionHandler2.Handle("asda");
        //    }


        //} 
        #endregion

        public static string GetParentDirectory(string directoryPath)
        {
            // Získání rodičovského adresáře
           // DirectoryInfo parentDirectory = Directory.GetParent(directoryPath);

            // Získání informací o cestě
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            // Získání rodičovského adresáře
            DirectoryInfo parentDirectory = directoryInfo.Parent;

            string ziskanaCesta = string.Empty;
            ziskanaCesta = parentDirectory != null ? parentDirectory.FullName : string.Empty;
            // Pokud rodičovský adresář existuje a není kořenový adresář, vrátíme jeho cestu,
            // jinak vrátíme prázdný řetězec
            return ziskanaCesta;
        }


        public void VytvorAktualizovanyAdresar(string cestaKAdresari, string cestaKAdresariKdeJeAktualizace)
        {
            try
            {
                // Kontrola, zda existuje zadaný adresář
                if (!Directory.Exists(cestaKAdresari))
                {
                    MessageBox.Show("Adresář neexistuje.");
                    return;
                }

                // Extrahování názvu adresáře z cesty
                string nazevAdresare = new DirectoryInfo(cestaKAdresari).Name;

                string cestaKAdresariVys = GetParentDirectory(cestaKAdresari);

                if (string.IsNullOrEmpty(cestaKAdresariVys))
                {
                    throw new Exception("chyba v tvorbe aktualizacniho adresare");
                }
                // Vytvoření názvu zip souboru
                string AktualizacniAdresar = Path.Combine(cestaKAdresariVys, $"{nazevAdresare}_Aktualizace");


                // Kontrola, zda existuje zadaný adresář
                if (!Directory.Exists(AktualizacniAdresar))
                {
                    Directory.CreateDirectory(AktualizacniAdresar);
                }

                //kopirovani z lokal adresare cestaKAdresari do aktualizacniho adresare AktualizacniAdresar
                CopyFilesStream(cestaKAdresari, AktualizacniAdresar);

               bool vysledekSmazani = DeleteDirectorySafely(cestaKAdresari);

                if(vysledekSmazani)
                {

                    CopyFilesStream(cestaKAdresariKdeJeAktualizace, AktualizacniAdresar);
                }
                else
                {
                    MessageBox.Show($"Z důvodu selhání práce s lokální složkou neproběhla aktualizace.");
                    return;
                }


                RenameSecondDirectoryToMatchFirst(cestaKAdresari, AktualizacniAdresar);

              

                MessageBox.Show($"Úspěšně zaktualizováno!");
            }
            catch (Exception ex)
            {
                // Vypsání chybové zprávy při chybě zálohování
                MessageBox.Show($"Chyba při zálohování adresáře: {ex.Message}");
            }
        }

        public static void RenameSecondDirectoryToMatchFirst(string firstDirectory, string secondDirectory)
        {
            try
            {
                // Získání názvu prvního adresáře
                string firstName = new DirectoryInfo(firstDirectory).Name;

                // Získání cesty k rodičovskému adresáři druhého adresáře
                string parentDirectory = Directory.GetParent(secondDirectory).FullName;

                // Vytvoření nové cesty s novým názvem druhého adresáře
                string newSecondDirectoryName = Path.Combine(parentDirectory, firstName);

                // Pokud cílový adresář již existuje, nebudeme pokračovat
                if (Directory.Exists(newSecondDirectoryName))
                {
                    Console.WriteLine("Cílový adresář již existuje.");
                    return;
                }

                // Přejmenování druhého adresáře
                Directory.Move(secondDirectory, newSecondDirectoryName);

                //Console.WriteLine($"Druhý adresář byl úspěšně přejmenován na {firstName}.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Nastala chyba při přejmenování druhého adresáře: {ex.Message}");
                throw ex;
            }
        }

        public bool DeleteDirectorySafely(string targetDirectory, bool smazatAdresar = true)
        {
            try
            {
                bool neuspech = false;

                // Pokud cílový adresář neexistuje, nemáme co mazat
                if (!Directory.Exists(targetDirectory))
                {
                    return false;
                }

                // Načtení obsahu adresáře do paměti
                Dictionary<string, byte[]> directoryContents = new Dictionary<string, byte[]>();
                foreach (string file in Directory.GetFiles(targetDirectory))
                {
                    byte[] fileData = File.ReadAllBytes(file);
                    string fileName = Path.GetFileName(file);
                    directoryContents.Add(fileName, fileData);
                }

                try
                {
                    if(smazatAdresar)
                    {

                        // Smazání adresáře
                        Directory.Delete(targetDirectory, true);
                    }
                    else
                    {
                        // Získání seznamu souborů v adresáři
                        string[] files = Directory.GetFiles(targetDirectory);
                        foreach (string file in files)
                        {
                            File.Delete(file); // Smazání souboru
                        }

                        // Získání seznamu podadresářů v adresáři
                        string[] subDirectories = Directory.GetDirectories(targetDirectory);
                        foreach (string subDirectory in subDirectories)
                        {
                            Directory.Delete(subDirectory, true); // Smazání podadresáře
                        }
                    }
                }
                catch (Exception ex)
                {

                    neuspech = true;
                }

                if(neuspech)
                {
                    // Obnovení obsahu z paměti, pokud došlo k chybě
                    foreach (var entry in directoryContents)
                    {
                        string filePath = Path.Combine(targetDirectory, entry.Key);
                        File.WriteAllBytes(filePath, entry.Value);
                    }
                }

                return true;
               
            }
            catch (Exception ex)
            {
                // Nyní můžeme vyvolat výjimku nebo provést jinou obsluhu chyby
                MessageBox.Show($"Chyba při smazání lokálního adresáře: {ex.Message}");
                return false;
            }
        }

        public static void CopyFiles(string sourceDirectory, string targetDirectory)
        {
            try
            {
                // Vytvoříme cílový adresář, pokud ještě neexistuje
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // Získání všech souborů a podadresářů v zdrojovém adresáři
                string[] files = Directory.GetFiles(sourceDirectory);
                string[] subDirectories = Directory.GetDirectories(sourceDirectory);

                // Procházíme všechny soubory a kopírujeme je do cílového adresáře
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(targetDirectory, fileName);
                    File.Copy(file, destFile, true); // Pokud soubor již existuje, bude přepsán
                }

                // Pro každý podadresář zavoláme rekurentně tuto metodu
                foreach (string subDirectory in subDirectories)
                {
                    string subDirectoryName = Path.GetFileName(subDirectory);
                    string targetSubDirectory = Path.Combine(targetDirectory, subDirectoryName);
                    CopyFiles(subDirectory, targetSubDirectory);
                }
            }
            catch (Exception ex)
            {
                // Nyní můžeme vyvolat výjimku nebo provést jinou obsluhu chyby
                throw ex;
            }
        }
        #region kopirovani souboru rozsireni

        //public static void CopyFilesStream(string sourceDirectory, string targetDirectory)
        //{
        //    try
        //    {
        //        if (!Directory.Exists(targetDirectory))
        //        {
        //            Directory.CreateDirectory(targetDirectory);
        //        }

        //        string[] files = Directory.GetFiles(sourceDirectory);
        //        string[] subDirectories = Directory.GetDirectories(sourceDirectory);

        //        foreach (string file in files)
        //        {
        //            string fileName = Path.GetFileName(file);
        //            string destFile = Path.Combine(targetDirectory, fileName);

        //            using (FileStream sourceStream = File.OpenRead(file))
        //            using (FileStream destinationStream = File.Create(destFile))
        //            {
        //                // Kopírování dat ze zdrojového souboru do cílového souboru
        //                sourceStream.CopyTo(destinationStream);
        //            }
        //        }

        //        foreach (string subDirectory in subDirectories)
        //        {
        //            string subDirectoryName = Path.GetFileName(subDirectory);
        //            string targetSubDirectory = Path.Combine(targetDirectory, subDirectoryName);
        //            CopyFiles(subDirectory, targetSubDirectory);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static void CopyFilesStream(string sourceDirectory, string targetDirectory)
        {
            try
            {
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string[] files = Directory.GetFiles(sourceDirectory);
                string[] subDirectories = Directory.GetDirectories(sourceDirectory);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(targetDirectory, fileName);

                    try
                    {
                        using (FileStream sourceStream = File.OpenRead(file))
                        using (FileStream destinationStream = File.Create(destFile))
                        {
                            // Kopírování dat ze zdrojového souboru do cílového souboru
                            sourceStream.CopyTo(destinationStream);
                        }
                    }
                    catch (IOException ex)
                    {
                        // Pokud se soubor nepodaří zkopírovat, zobrazíme dialogové okno s upozorněním
                        DialogResult result = MessageBox.Show($"Soubor {fileName} nelze zkopírovat. Chcete tento soubor přeskočit?", "Chyba", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.No)
                        {
                            // Pokud uživatel nechce přeskočit soubor, vyhodíme výjimku
                            throw ex;
                        }
                    }
                }

                foreach (string subDirectory in subDirectories)
                {
                    string subDirectoryName = Path.GetFileName(subDirectory);
                    string targetSubDirectory = Path.Combine(targetDirectory, subDirectoryName);
                    CopyFilesStream(subDirectory, targetSubDirectory);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #endregion


        private async void btn_aktualizace_Click(object sender, EventArgs e)
        {
            DisableButton(false);

            // Resetuje ProgressBar před zahájením operace.
            toolStripProgressBar1.Value = 0; // Nastaví hodnotu na začátek.
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks; // Přepne styl na bloky, aby bylo vidět změnu hodnoty.
            toolStripProgressBar1.Visible = true;


            if (UkonciProces())
            {
                toolStripProgressBar1.Visible = false; // Skryje načítací indikátor.
                MessageBox.Show("Máte spuštěnou Konzoli, prosím ukončete ji! Následně spustě aktualizaci znovu.");
            }
            else
            {
                // Aktualizace();
                // Před spuštěním metody nastavíme styl na Marquee, pokud se hodí pro vizuální indikaci načítání.
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                await Task.Run(() => Aktualizace());
                // Znovu přepne styl na procentuální, pokud potřebuješ ukazovat postup.
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            }

            toolStripProgressBar1.Visible = false; // Skryje načítací indikátor.
            DisableButton(true);


        }
        #endregion

        #region akce obnovy
        // Metoda pro vypsání první cesty z dt_parametry do textového pole
        public bool OverCestProObnovu()
        {
            // Kontrola, zda je inicializována proměnná dt_parametry
            if (dt_parametry != null && dt_parametry.Rows.Count > 0)
            {
                // Získání prvního řádku z dt_parametry
                DS_konzola.Konzola_parametryRow row = dt_parametry[0];
                // Vypsání hodnoty vlastnosti path_lokal_konzola do textového pole
                txtLocalPath.Text = row.Ispath_lokal_konzolaNull() ? string.Empty : row.path_lokal_konzola;
                txtlPathObnova.Text = row.Ispath_obnovaNull() ? string.Empty : row.path_obnova;
            }

            // Kontrola, zda je inicializována proměnná dt_parametry
            if (!string.IsNullOrEmpty(txtLocalPath.Text) && !string.IsNullOrEmpty(txtlPathObnova.Text))
            {

                pathLocal = txtLocalPath.Text;
                pathObnova = txtlPathObnova.Text;
                return true;
            }

            return false;
        }

        public bool Obnovovani()
        {

            try
            {

                if (OverCestProObnovu())
                {
                    ObnovitAdresar(pathLocal, pathObnova);
                    pathLocal = string.Empty;
                    pathObnova = string.Empty;
                }
                else
                {
                    MessageBox.Show("Nejsou vybrány cesty k adresářům!");
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
                return false;
                //MessageBox.Show("Zálohování selhalo!");
            }
        }


        public void ObnovitAdresar(string cestaKAdresari, string zipSoubor)
        {
            try
            {
                // Kontrola, zda existuje zadaný adresář
                if (!Directory.Exists(cestaKAdresari))
                {
                    MessageBox.Show("Adresář neexistuje.");
                    return;
                }

                // Extrahování názvu adresáře z cesty
                string nazevAdresare = new DirectoryInfo(cestaKAdresari).Name;
                string nazevSouboru = new DirectoryInfo(zipSoubor).Name;

                // Kontrola, zda již existuje zazipovaný soubor
                if (File.Exists(zipSoubor))
                {
                    //CompressFile.DeCompressFromZip(zipSoubor, cestaKAdresari);

                    bool vysledekSmazani = DeleteDirectorySafely(cestaKAdresari, false);

                    if (vysledekSmazani)
                    {


                        //TODO MaR dopsat logiku obnoveni. 23.4.2024 
                        if(!CompressFile.DeCompressFromZip(zipSoubor, cestaKAdresari))
                        {
                            MessageBox.Show($"Při rozzipování souboru nastala chyba. Obnov soubor ručně.");
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show($"Z důvodu selhání práce s lokální složkou neproběhlo obnovení.");
                        return;
                    }


                }
                else
                {
                    MessageBox.Show($"Zazipovaný soubor pro obnovení: {nazevSouboru} neexistuje!");
                    return;
                }

                MessageBox.Show($"Úspěšně obnoven: {nazevAdresare}");
            }
            catch (Exception ex)
            {
                // Vypsání chybové zprávy při chybě zálohování
                MessageBox.Show($"Chyba při obnovování souboru: {ex.Message}");
            }
        }

        #endregion

        public void RefreshForm()
        {
            VypisCestyDoTextovehoPole();
        }

        private void btn_localPath_Click(object sender, EventArgs e)
        {
            VyberAdresarLocal();
            RefreshForm();
        }

        private void btn_ukonceni_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_PathZaloha_Click(object sender, EventArgs e)
        {
            VyberAdresarZaloha();
            RefreshForm();
        }

        private void btn_PathAktualizace_Click(object sender, EventArgs e)
        {
            VyberAdresarAktualizace();

            RefreshForm();
        }

        #region old bez indikatoru
        //private void btn_zaloha_Click(object sender, EventArgs e)
        //{
        //    DisableButton(false);

        //    if (UkonciProces())
        //    {
        //        MessageBox.Show("Máte spuštěnou Konzoli, prosím ukončete ji! Následně spustě zálohu znovu.");
        //    }
        //    else
        //    {
        //        Zalohovani();
        //    }

        //    //Zalohovani();
        //    DisableButton(true);
        //} 
        #endregion

        #region 26.8.2024 MaR nacitaci indikator old
        //private async void btn_zaloha_Click(object sender, EventArgs e)
        //{
        //    DisableButton(false);

        //    updateProgressBarDownloadDelegate callback = updateProgressBarDownload;

        //    if (UkonciProces())
        //    {
        //        MessageBox.Show("Máte spuštěnou Konzoli, prosím ukončete ji! Následně spustě zálohu znovu.");
        //    }
        //    else
        //    {
        //        // Spustíš zálohování na pozadí.
        //        await Task.Run(() => Zalohovani());
        //    }

        //    DisableButton(true);
        //}


        //public delegate void updateProgressBarDownloadDelegate(int ProgressPercentage, string Text);

        //void updateProgressBarDownload(int ProgressPercentage, string Text)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
        //        this.BeginInvoke((System.Threading.ThreadStart)delegate () { this.updateProgressBarDownload(ProgressPercentage, Text); });
        //        return;
        //    }

        //    toolStripProgressBar1.Value = ProgressPercentage;
        //    //statusBarInfo.Text = Text;
        //}

        //void fileDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    this.BeginInvoke(
        //        new updateProgressBarDownloadDelegate(updateProgressBarDownload),
        //        new object[] { e.ProgressPercentage, "" }
        //        );
        //}
        #endregion

        #region 26.8.2024 MaR nacitaci indikator new
        private async void btn_zaloha_Click(object sender, EventArgs e)
        {
            DisableButton(false);

            // Resetuje ProgressBar před zahájením operace.
            toolStripProgressBar1.Value = 0; // Nastaví hodnotu na začátek.
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks; // Přepne styl na bloky, aby bylo vidět změnu hodnoty.
            toolStripProgressBar1.Visible = true;

            if (UkonciProces())
            {
                toolStripProgressBar1.Visible = false; // Skryje načítací indikátor.
                MessageBox.Show("Máte spuštěnou Konzoli, prosím ukončete ji! Následně spustě zálohu znovu.");
            }
            else
            {
                // Před spuštěním metody nastavíme styl na Marquee, pokud se hodí pro vizuální indikaci načítání.
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                // Spustíš zálohování na pozadí.
                await Task.Run(() => Zalohovani());
                //Zalohovani();

                // Znovu přepne styl na procentuální, pokud potřebuješ ukazovat postup.
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;

            }

            toolStripProgressBar1.Visible = false; // Skryje načítací indikátor.
            DisableButton(true);
        }

        public delegate void updateProgressBarDownloadDelegate(int ProgressPercentage, string Text);

        void updateProgressBarDownload(int ProgressPercentage, string Text)
        {
            if (this.InvokeRequired)
            {
                // Pokud je potřeba volat z jiného vlákna, použije se BeginInvoke.
                this.BeginInvoke(new updateProgressBarDownloadDelegate(updateProgressBarDownload), ProgressPercentage, Text);
            }
            else
            {
                // Přepnutí na procentuální styl a aktualizace hodnoty.
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks; // Procentuální styl.
                toolStripProgressBar1.Value = ProgressPercentage;
                //statusBarInfo.Text = Text; // Pokud chceš aktualizovat text v jiném prvku.
            }
        }

        void fileDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Zajistí, že změny budou aktualizovány na správném vlákně.
            this.BeginInvoke(new updateProgressBarDownloadDelegate(updateProgressBarDownload), e.ProgressPercentage, "");
        }

        #endregion

        private void btn_obnova_Click(object sender, EventArgs e)
        {
            DisableButton(false);
            if (UkonciProces())
            {
                MessageBox.Show("Máte spuštěnou Konzoli, prosím ukončete ji! Následně spustě obnovení znovu.");
            }
            else
            {
                Obnovovani();
            }
            //UkonciProces();
            //Obnovovani();
            DisableButton(true);
        }




        private void btn_PathNacist_Click(object sender, EventArgs e)
        {
            SettingsLoad();

        }

        private string APIAktualizacePath_staraVerze = string.Empty;
        private string APIAktualizacePath_novaVerze = string.Empty;
        private string APIAktualizacePath_zalohaVerze = string.Empty;
        private string APIAktualizacePath_obnovaVerze = string.Empty;


        private void SettingsLoad()
        {
            try
            {
                string defaultPath = Application.StartupPath;
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = defaultPath,
                    Filter = "XML soubory (*.xml)|*.xml|Konfigurační soubory (*.config)|*.config|Všechny soubory (*.*)|*.*",
                    Title = "Vyberte konfigurační soubor"
                };

                string pathStaraVerze = null;
                string pathNovaVerze = null;
                string pathZalohaVerze = null;
                string pathObnovaVerze = null;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XDocument doc = XDocument.Load(openFileDialog.FileName);
                    var settings = doc.Descendants("appSettings").FirstOrDefault();

                    if (settings != null)
                    {
                        pathStaraVerze = GetAppSettingValue(settings, "APIAktualizacePath_staraVerze", Settings.APIAktualizacePath_staraVerze);
                        pathNovaVerze = GetAppSettingValue(settings, "APIAktualizacePath_novaVerze", Settings.APIAktualizacePath_novaVerze);
                        pathZalohaVerze = GetAppSettingValue(settings, "APIAktualizacePath_zalohaVerze", Settings.APIAktualizacePath_zalohaVerze);
                        pathObnovaVerze = GetAppSettingValue(settings, "APIAktualizacePath_obnovaVerze", Settings.APIAktualizacePath_obnovaVerze);
                    }
                    else
                    {
                        MessageBox.Show("Soubor neobsahuje sekci <appSettings>.\nPoužívají se výchozí cesty ze Settings.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Přiřazení s fallbackem na výchozí Settings
                APIAktualizacePath_staraVerze = pathStaraVerze ?? Settings.APIAktualizacePath_staraVerze;
                APIAktualizacePath_novaVerze = pathNovaVerze ?? Settings.APIAktualizacePath_novaVerze;
                APIAktualizacePath_zalohaVerze = pathZalohaVerze ?? Settings.APIAktualizacePath_zalohaVerze;
                APIAktualizacePath_obnovaVerze = pathObnovaVerze ?? Settings.APIAktualizacePath_obnovaVerze;

                if (dt_parametry == null)
                {
                    DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
                    SetParametry(initialDataTable);
                }

                PridejAktualizaceCestuDoTabulky(APIAktualizacePath_novaVerze);
                PridejLocalCestuDoTabulky(APIAktualizacePath_staraVerze);
                PridejZalohaCestuDoTabulky(APIAktualizacePath_zalohaVerze);
                PridejObnovaCestuDoTabulky(APIAktualizacePath_obnovaVerze);

                RefreshForm();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private string GetAppSettingValue(XElement settings, string key, string defaultValue)
        {
            var element = settings.Elements("add").FirstOrDefault(x => (string)x.Attribute("key") == key);
            if (element == null)
            {
                MessageBox.Show($"Chybí klíč '{key}' v konfiguračním souboru.\nPoužívá se výchozí hodnota.", "Chybějící klíč", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return defaultValue;
            }
            return element.Attribute("value")?.Value ?? defaultValue;
        }


        //MaR 12.6.2025 uprava funkcionality
        private void SettingsLoadOld()
        {
            try
            {

                APIAktualizacePath_staraVerze = Settings.APIAktualizacePath_staraVerze;
                APIAktualizacePath_novaVerze = Settings.APIAktualizacePath_novaVerze;
                APIAktualizacePath_zalohaVerze = Settings.APIAktualizacePath_zalohaVerze;
                APIAktualizacePath_obnovaVerze = Settings.APIAktualizacePath_obnovaVerze;

                  if (dt_parametry == null)
                    {
                        // Inicializace proměnné dt_parametry pomocí veřejné metody SetParametry
                        DS_konzola.Konzola_parametryDataTable initialDataTable = new DS_konzola.Konzola_parametryDataTable();
                        SetParametry(initialDataTable);
                    PridejAktualizaceCestuDoTabulky(APIAktualizacePath_novaVerze);
                    PridejLocalCestuDoTabulky(APIAktualizacePath_staraVerze);
                    PridejZalohaCestuDoTabulky(APIAktualizacePath_zalohaVerze);
                    PridejObnovaCestuDoTabulky(APIAktualizacePath_obnovaVerze);
                }
                    // Pokud proměnná dt_parametry již obsahuje data, ponecháme ji tak, jak je
                    else
                    {
                    PridejAktualizaceCestuDoTabulky(APIAktualizacePath_novaVerze);
                    PridejLocalCestuDoTabulky(APIAktualizacePath_staraVerze);
                    PridejZalohaCestuDoTabulky(APIAktualizacePath_zalohaVerze);
                    PridejObnovaCestuDoTabulky(APIAktualizacePath_obnovaVerze);
                }


                RefreshForm();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btn_ulozPath_Click(object sender, EventArgs e)
        {
            SettingsSave();
        }

        private void SettingsSave()
        {
            try
            {


                 Settings.APIAktualizacePath_staraVerze = txtLocalPath.Text;
                Settings.APIAktualizacePath_novaVerze = txtlPathZdrojAkt.Text;
                Settings.APIAktualizacePath_zalohaVerze = txtlPathZaloha.Text;
                Settings.APIAktualizacePath_obnovaVerze = txtlPathObnova.Text;
                //Settings.APIAktualizacePath_obnovaVerze = txtLocalPath.Text;



                Settings.Update();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btn_obnovaPath_Click(object sender, EventArgs e)
        {
            VyberAdresarObnova();

            RefreshForm();
        }
    }
}
