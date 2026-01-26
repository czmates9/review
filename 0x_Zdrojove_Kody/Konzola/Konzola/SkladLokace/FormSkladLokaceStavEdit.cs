using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.SkladLokace
{
    public partial class FormSkladLokaceStavEdit : Form
    {
        #region Parametry

        private Fask.Interfaces.IMES providerMapa = null;
        private Fask.Interfaces.IMES providerSklady = null;
        private Fask.Interfaces.IMES providerZbozi = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;
        private Fask.Interfaces.IMES providerSkladLokacePohyb = null;

        //RFID
        public IRFIDProvider.IRFIDProvider RFID = null;

        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_StavRow returnrow { get; set; }
        /// <summary>
        /// Zde neprobiha editace zaznamu. Pokazde dochazi k pridani noveho pohybu.
        /// </summary>
        //public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow lokacerow { get; set; }


        #region Predvyplneni

        private bool _typPohybuFlag = true;
        private Fask.Interfaces.SkladLokace.TypeOfRecord _typPohyby;
        public Fask.Interfaces.SkladLokace.TypeOfRecord TypPohyby
        {
            set
            {

                _typPohyby = value;

                LoadData(_typPohyby);

                cbPohybType.SelectedValue = _typPohyby.ToString();
                _typPohybuFlag = false;
            }
            get 
            {
                return _typPohyby;
            }
        }

        public string ITEMNMBR
        {
            set
            {
                tbItemnmbr.Text = value;
            }
        }

        public string ITEMDESC
        {
            set
            {
                tbITEMDESC.Text = value;
            }
        }

        public string SKL_ID
        {
            set
            {
                tbSklID.Text = value;
            }
        }

        public string LOCNCODE
        {
            set
            {
                tbLocncode.Text = value;
            }
        }

        public string SERLTNUM
        {
            set
            {
                tbSerltnum.Text = value;
            }
        }

        public byte CZ_SerNum_Track
        {
            set
            {
                if (value == 0)
                {
                    tbSerltnum.Enabled = false;
                    label2.Enabled = false;
                }
                else
                {
                    tbSerltnum.Enabled = true;
                    label2.Enabled = true;
                }
            }
        }


        public string USER_ID
        {
            set
            {
                tbUserID.Text = value;
            }
        }

        #endregion


        #endregion

        #region Eventy formu

        public FormSkladLokaceStavEdit()
        {
            try
            {
                InitializeComponent();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (_typPohybuFlag)
                {
                    LoadData(Fask.Interfaces.SkladLokace.TypeOfRecord.P);
                }

                if (_typPohyby == Fask.Interfaces.SkladLokace.TypeOfRecord.D)
                {
                    tbSklIDCIL.Text = tbSklID.Text;
                }

                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerMapa == null)
                    throw new Exception("Provider 'Mapa lokací' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zboží' není inicializován");

                if (providerSkladLokacePohyb == null)
                    throw new Exception("Provider 'SkladLokacePohyb' není inicializován");

                //if (providerUzivatele == null)
                //    throw new Exception("Provider 'Uživatelé' není inicializován");

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Enable)
                {
                    RFID = RFIDFactory.RFIDFactory.Init();

                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    //RFID.Stop();
                    btn_RFID_Load.BackColor = Color.Red;
                    btn_RFID_Load.Text = "Start Read";
                }
                else
                {
                    btn_RFID_Load.Visible = false;
                }




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    this.PerformVlozZaznam();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {
        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormSkladLokaceStavEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Enable)
            {
                RFIDStop();
            }
        }

        #endregion

        #region InitProvider

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerMapa == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2).IsAssignableFrom(t))
                            {
                                providerMapa = (Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerMapa != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerMapa.InitProvider();
             

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {

                if (providerZbozi == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
                            {
                                providerZbozi = (Fask.Interfaces.Ciselniky.Zbozi.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerZbozi != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerZbozi.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {

                if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                            {
                                providerSklady = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSklady != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSklady.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {

                if (providerSkladLokacePohyb == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.SkladLokace.ISkladLokace2).IsAssignableFrom(t))
                            {
                                providerSkladLokacePohyb = (Fask.Interfaces.SkladLokace.ISkladLokace2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSkladLokacePohyb != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSkladLokacePohyb.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //try
            //{

            //    if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //    {
            //        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
            //        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //        Type[] types = providerAssemlby.GetTypes();
            //        foreach (Type t in types)
            //        {
            //            try
            //            {
            //                if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
            //                {
            //                    providerUzivatele = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
            //                    if (providerUzivatele != null)
            //                        break;
            //                }
            //            }
            //            catch { }
            //        }
            //        //return config;
            //    }

            //    // nastaveni connection stringu
            //   // if (providerUzivatele != null)
            //  //      ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            //    if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //        ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.Uzivatele");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion

        #region Nastaveni Typu Pohybu

        private void LoadData(Fask.Interfaces.SkladLokace.TypeOfRecord Type)
        {
            // naplneni comboboxu
            cbPohybType.DisplayMember = "Text";
            cbPohybType.ValueMember = "Value";



            if (Type == Fask.Interfaces.SkladLokace.TypeOfRecord.D)
            {
                var items = new[] { new { Text = "Převod", Value = "D" } };

                cbPohybType.DataSource = items;
                cbPohybType.SelectedItem = null;

                label1.Text = "ID lokace Zdroj:";
                label3.Text = "ID skladu Zdroj:";

                tbSklID.Enabled = false;
                tbLocncode.Enabled = false;
                btnSkladLokaceVyhledat.Enabled = false;

                tbSklIDCIL.Enabled = false;

                tbItemnmbr.Enabled = false;

            }
            else if ((Type == Fask.Interfaces.SkladLokace.TypeOfRecord.V) || (Type == Fask.Interfaces.SkladLokace.TypeOfRecord.P))
            {
                var items = new[] {
                new { Text = "Příjem", Value = "P" },
                new { Text = "Výdej", Value = "V" }
                                };

                cbPohybType.DataSource = items;
                cbPohybType.SelectedItem = null;

                tbLocncodeCIL.Enabled = false;
                tbSklIDCIL.Enabled = false;
                btnSkladLokace_CIL_Vyhledat.Enabled = false;

                label12.Enabled = false;
                label13.Enabled = false;
            }



        }


        #endregion

        #region Button Click

        private void btnItemnmbrVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledatPolozku();
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.PerformVlozZaznam();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void btnPracovnikVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyberPracovnika();
        }

        private void btnSkladLokaceVyhledat_Click(object sender, EventArgs e)
        {
            PerformSKladLokaceVyber();
        }

        private void btnSkladLokace_CIL_Vyhledat_Click(object sender, EventArgs e)
        {
            PerformSKladLokaceVyberCil();
        }

        private void btnUserVyhledat_Click(object sender, EventArgs e)
        {

            //TODO Velke TODO ale, je potřeba vymyslet a doimplementovat....


            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;
                //TODO Vyhledat uživatele?
                //using (Ciselniky.FormUzivateleList frm = new Ciselniky.FormUzivateleList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                //{
                //    frm.Text = "Výběr uživatele";

                //    if (frm.ShowDialog(this) != DialogResult.OK)
                //        return;

                //    tbUserID.Text = frm.SelectedRow.ID.ToString();
                //}
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Perform Metody

        private void PerformVyhledatPolozku()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Ciselniky.FormZboziList frmzbozi = new Ciselniky.FormZboziList(false, null, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frmzbozi.Text = "Výběr materiálu";

                    if (frmzbozi.ShowDialog(this) != DialogResult.OK)
                        return;

                    tbItemnmbr.Text = frmzbozi.FASK_ZASOBY_selectedRow.ITEMNMBR.Trim();
                    tbITEMDESC.Text = (frmzbozi.FASK_ZASOBY_selectedRow.IsITEMDESCNull() ? string.Empty : frmzbozi.FASK_ZASOBY_selectedRow.ITEMDESC.Trim()) + (frmzbozi.FASK_ZASOBY_selectedRow.IsITEMDESCNull() ? string.Empty : (" (" + frmzbozi.FASK_ZASOBY_selectedRow.ITEMCODE.Trim() + ")"));

                    if (frmzbozi.FASK_ZASOBY_selectedRow.CZ_SerNum_Track == 0)
                    {
                        //mnozstvi
                        tbSerltnum.Enabled = false;
                        btn_RFID_Load.Enabled = false;

                    }
                    else if (frmzbozi.FASK_ZASOBY_selectedRow.CZ_SerNum_Track == 1)
                    {
                        //SN(1) a sarze(2)
                        tbQtyshppd.Text = "1";
                        tbQtyshppd.Enabled = false;
                        tbQTY_owner.Text = "1";
                        tbQTY_owner.Enabled = false;


                        if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_RFID[0].Enable)
                        {
                            if (!RFID.isOpen())
                            {
                                RFIDStart();
                            }
                        }
                    }
                    else if (frmzbozi.FASK_ZASOBY_selectedRow.CZ_SerNum_Track == 2)
                    {
                        btn_RFID_Load.Enabled = false;
                        tbSerltnum.Enabled = true;
                        tbQtyshppd.Enabled = true;
                        tbQTY_owner.Enabled = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformVyberPracovnika()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Ciselniky.FormPracovniciList frm = new Ciselniky.FormPracovniciList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frm.Text = "Výběr pracovnika";

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    tbPracovnikID.Text = frm.CZMST096_selectedRow.prac_id.ToString();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformSKladLokaceVyber()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Ciselniky.FormLokaceMapaList frm = new Ciselniky.FormLokaceMapaList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frm.Text = "Výběr skladu a lokace";

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    tbSklID.Text = frm.Lokace_Mapa_selectedRow.IsSKL_IDNull() ? string.Empty : frm.Lokace_Mapa_selectedRow.SKL_ID.Trim();
                    tbLocncode.Text = frm.Lokace_Mapa_selectedRow.IsLOCNCODENull() ? string.Empty : frm.Lokace_Mapa_selectedRow.LOCNCODE.Trim();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformSKladLokaceVyberCil()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Ciselniky.FormLokaceMapaList frm = new Ciselniky.FormLokaceMapaList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frm.Text = "Výběr skladu a lokace";

                    frm.SKL_ID_Filtr = tbSklIDCIL.Text;

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    tbSklIDCIL.Text = frm.Lokace_Mapa_selectedRow.IsSKL_IDNull() ? string.Empty : frm.Lokace_Mapa_selectedRow.SKL_ID.Trim();
                    tbLocncodeCIL.Text = frm.Lokace_Mapa_selectedRow.IsLOCNCODENull() ? string.Empty : frm.Lokace_Mapa_selectedRow.LOCNCODE.Trim();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformOK()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!ValidateData())
                    return;

                // porovnani s existujicim zaznamem
                // TODO: do transakce s editaci/pridanim??
                //if (lokacerow != null)
                //{
                //    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow stav_test = providerMapa.GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());

                //    if (stav_test == null)
                //    {
                //        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        // kvuli refreshi
                //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //        return;
                //    }
                //    else
                //    {
                //        IEqualityComparer<Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow> comparer = DataRowComparer.Default;
                //        bool isMatch = comparer.Equals(lokacerow, stav_test);
                //        if (!isMatch)
                //        {
                //            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //            {
                //                // kvuli refreshi
                //                this.DialogResult = DialogResult.OK;
                //                return;
                //            }
                //        }
                //    }
                //}

                Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();
                Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow newMapaRow = ds2.CZMST_SkladLokace_Mapa.NewCZMST_SkladLokace_MapaRow();

                newMapaRow.SKL_ID = tbSklID.Text.Trim();
                newMapaRow.LOCNCODE = tbLocncode.Text.Trim();
                newMapaRow.Barcode = tbLocncode.Text.Trim();

                if (string.IsNullOrEmpty(tbQtyshppd.Text.Trim()))
                    newMapaRow.SetDescriptionNull();
                else
                    newMapaRow.Description = tbQtyshppd.Text.Trim();

                if (string.IsNullOrEmpty(tbSerltnum.Text.Trim()))
                    newMapaRow.SetTYPENull();
                else
                    newMapaRow.TYPE = tbSerltnum.Text.Trim();


                Fask.Interfaces.SkladLokace.LokacePohyb record = new Fask.Interfaces.SkladLokace.LokacePohyb();
                record.ITEMNMBR = tbItemnmbr.Text.Trim();
                record.DOCUMENT_NUMBER = tbDocumentNumber.Text.Trim();
                record.POHYB_TYPE = (Fask.Interfaces.SkladLokace.TypeOfRecord)Enum.Parse(typeof(Fask.Interfaces.SkladLokace.TypeOfRecord), cbPohybType.SelectedValue.ToString());
                record.POHYB_SRC = record.POHYB_TYPE.ToString();
                record.SOURCE = "K";    // Konzola
                record.QTYSHPPD_DEF = 0;
                record.QTYSHPPD = Convert.ToDecimal(tbQtyshppd.Text, System.Globalization.CultureInfo.InvariantCulture);
                record.SERLTNUM = tbSerltnum.Text.Trim();
                record.SKL_ID_SRC = tbSklID.Text.Trim();
                record.LOCNCODE_SRC = tbLocncode.Text.Trim();
                record.SKL_ID_DST = string.Empty;
                record.LOCNCODE_DST = string.Empty;
                record.UserID = Convert.ToInt32(tbUserID.Text.Trim());
                record.TermID = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID;
                record.guid = Guid.NewGuid();
                DateTime dtnow = DateTime.Now;
                record.dateeveS = record.dateeveT = dtnow;
                record.Expiration = null;
                record.ITEMDESC = string.Empty;
                record.CountEntries = null;
                record.PRAC_ID_OWNER = string.IsNullOrEmpty(tbPracovnikID.Text.Trim()) ? string.Empty : tbPracovnikID.Text.Trim();
                record.QTY_OWNER = string.IsNullOrEmpty(tbQTY_owner.Text.Trim()) ? 0 : Convert.ToDecimal(tbQTY_owner.Text, System.Globalization.CultureInfo.InvariantCulture);

                //((Fask.Interfaces.SkladLokace.ISkladLokace)providerSkladLokacePohyb).InsertStavPohyb(record);

                if ((providerSkladLokacePohyb != null) && (providerSkladLokacePohyb is Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb))
                    ((Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb)providerSkladLokacePohyb).InsertStavPohyb(record);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_InsertStavPohyb.");

                // nacteni zaznamu vcetne dat z joinu ... pokud neprojde, tak alespon nove vytvoreny zaznam
                try
                {
                    Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();
                    filtr.MaterialITEMNMBR = record.ITEMNMBR.Trim();
                    filtr.MaterialLocncode = record.LOCNCODE_SRC.Trim();
                    filtr.MaterialSklID = record.SKL_ID_SRC.Trim();
                    filtr.MaterialSerltnum = record.SERLTNUM.Trim();
                    //Fask.Interfaces.DataSets.SkladLokace data = ((Fask.Interfaces.SkladLokace.ISkladLokace)providerSkladLokacePohyb).GetFiltrovanySkladLokaceStav(filtr);
                    Fask.Interfaces.DataSets.SkladLokace data;

                    if ((providerSkladLokacePohyb != null) && (providerSkladLokacePohyb is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav))
                        data = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav)providerSkladLokacePohyb).GetFiltrovanySkladLokaceStav(filtr);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFiltrovanySkladLokaceStav.");



                    returnrow = data.CZMST_SkladLokace_Stav.Count > 0 ? data.CZMST_SkladLokace_Stav.First() : null;
                }
                catch { }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVlozZaznam()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!ValidateData())
                    return;

                PerformADD();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformADD()
        {
            try
            {

                Fask.Interfaces.SkladLokace.LokacePohyb record = new Fask.Interfaces.SkladLokace.LokacePohyb();


                record.ITEMNMBR = tbItemnmbr.Text.Trim();
                record.DOCUMENT_NUMBER = tbDocumentNumber.Text.Trim();
                record.POHYB_TYPE = (Fask.Interfaces.SkladLokace.TypeOfRecord)Enum.Parse(typeof(Fask.Interfaces.SkladLokace.TypeOfRecord), cbPohybType.SelectedValue.ToString());
                record.POHYB_SRC = record.POHYB_TYPE.ToString();
                record.SOURCE = "K";    // Konzola
                record.QTYSHPPD_DEF = 0;
                record.QTYSHPPD = Convert.ToDecimal(tbQtyshppd.Text, System.Globalization.CultureInfo.InvariantCulture);
                record.SERLTNUM = tbSerltnum.Text.Trim();
                record.SKL_ID_SRC = tbSklID.Text.Trim();
                record.LOCNCODE_SRC = tbLocncode.Text.Trim();
                record.SKL_ID_DST = tbSklIDCIL.Text.Trim(); 
                record.LOCNCODE_DST = tbLocncodeCIL.Text.Trim();
                record.UserID = Convert.ToInt32(tbUserID.Text.Trim());
                record.TermID = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID;
                record.guid = Guid.NewGuid();
                DateTime dtnow = DateTime.Now;
                record.dateeveS = record.dateeveT = dtnow;
                record.Expiration = null;
                record.ITEMDESC = string.Empty;
                record.CountEntries = null;
                record.PRAC_ID_OWNER = string.IsNullOrEmpty(tbPracovnikID.Text.Trim()) ? string.Empty : tbPracovnikID.Text.Trim();
                record.QTY_OWNER = string.IsNullOrEmpty(tbQTY_owner.Text.Trim()) ? 0 : Convert.ToDecimal(tbQTY_owner.Text, System.Globalization.CultureInfo.InvariantCulture);


                if ((providerSkladLokacePohyb != null) && (providerSkladLokacePohyb is Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb))
                    ((Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb)providerSkladLokacePohyb).InsertStavPohyb(record);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_InsertStavPohyb.");

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region  Validace Vyplnenich dat

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                ///////////////////////////
                // 1) kontrola, zdali je mnozstvi cislo a je > 0
                // 2) kontrola existence skladu
                // 2.1) kontrola existence skladu
                // 3) kontrola existence lokace a skladu v mape lokaci
                // 3.1) kontrola existence lokace a skladu v mape lokaci
                // 4) kontrola existence zbozi
                // 5) kontrola vybraneho typu pohybu
                // -> pokud vydej, tak kontrola, zdali je uvedene mnozstvi vubec na sklade
                // 6) kontrola existence uzivatele
                // 7) kontrola poctu znaku v document number
                // 8) kontrola mnozstvi pro pracovnika
                // 9) kontrola zadani SN
                ///////////////////////////

                // 1) kontrola, zdali je mnozstvi cislo a je > 0
                decimal qty = 0;
                if (string.IsNullOrEmpty(tbQtyshppd.Text.Trim()))
                    errorProvider1.SetError(tbQtyshppd, "Musíte zadat množství");
                else
                {                    
                    bool res = Decimal.TryParse(tbQtyshppd.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out qty);
                    if (!res)
                    {
                        errorProvider1.SetError(tbQtyshppd, "Množství musí být číslo");
                    }
                    else if (qty <= 0)
                    {
                        errorProvider1.SetError(tbQtyshppd, "Množství musí být kladné číslo");
                    }
                }

                // 2) kontrola existence skladu
                if (string.IsNullOrEmpty(tbSklID.Text.Trim()))
                    errorProvider1.SetError(tbSklID, "Musíte zadat id skladu");
                else
                {
                    // kontrola existence skladu
                    //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                    Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad;


                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                        sklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                    if (sklad == null)
                        errorProvider1.SetError(tbSklID, "Sklad s ID '" + tbSklID.Text.Trim() + "' neexistuje v číselníku skladů");
                }

                if (cbPohybType.SelectedValue.ToString() == "D")
                {
                    // 2.1) kontrola existence skladu
                    if (string.IsNullOrEmpty(tbSklIDCIL.Text.Trim()))
                        errorProvider1.SetError(tbSklIDCIL, "Musíte zadat id skladu");
                    else
                    {
                        // kontrola existence skladu
                        //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                        Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad;


                        if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                            sklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(tbSklIDCIL.Text.Trim());
                        else
                            throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                        if (sklad == null)
                            errorProvider1.SetError(tbSklIDCIL, "Sklad s ID '" + tbSklIDCIL.Text.Trim() + "' neexistuje v číselníku skladů");
                    } 
                }

                // 3) kontrola existence lokace a skladu v mape lokaci
                if (string.IsNullOrEmpty(tbLocncode.Text.Trim()))
                    errorProvider1.SetError(tbLocncode, "Musíte zadat id lokace");
                else
                {
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow lokace = ((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow lokace;

                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode))
                        lokace = ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode.");

                    
                    if (lokace == null)
                        errorProvider1.SetError(tbLocncode, "Lokace '" + tbLocncode.Text.Trim() + "' ve skladu '" + tbSklID.Text.Trim() + "' neexistuje existuje");
                }

                if (cbPohybType.SelectedValue.ToString() == "D")
                {
                    // 3.1) kontrola existence lokace a skladu v mape lokaci
                    if (string.IsNullOrEmpty(tbLocncodeCIL.Text.Trim()))
                        errorProvider1.SetError(tbLocncodeCIL, "Musíte zadat id lokace");
                    else
                    {
                        //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow lokace = ((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                        Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow lokace;

                        if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode))
                            lokace = ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklIDCIL.Text.Trim(), tbLocncodeCIL.Text.Trim());
                        else
                            throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode.");


                        if (lokace == null)
                            errorProvider1.SetError(tbLocncodeCIL, "Lokace '" + tbLocncodeCIL.Text.Trim() + "' ve skladu '" + tbSklIDCIL.Text.Trim() + "' neexistuje existuje");
                    } 
                }

                // 4) kontrola existence zbozi
                if (string.IsNullOrEmpty(tbItemnmbr.Text.Trim()))
                    errorProvider1.SetError(tbItemnmbr, "Musíte zadat id materiálu");
                else
                {
                    // kontrola existence zbozi
                    Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zbozi = null;

                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID))
                    {
                        zbozi = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID)providerZbozi).GetZboziByID(tbItemnmbr.Text.Trim());
                    }
                    else
                    {
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetZboziByID");
                    }

                    if (zbozi == null)
                        errorProvider1.SetError(tbItemnmbr, "Materiál s ID '" + tbItemnmbr.Text.Trim() + "' neexistuje v číselníku zboží");
                }

                // 5) kontrola vybraneho typu pohybu
                // -> pokud vydej, tak kontrola, zdali je uvedene mnozstvi vubec na sklade
                if (cbPohybType.SelectedItem == null)
                    errorProvider1.SetError(cbPohybType, "Musíte zvolit typ pohybu");
                else
                {
                    if (IsAllValid())
                    {
                        Fask.Interfaces.SkladLokace.TypeOfRecord record = (Fask.Interfaces.SkladLokace.TypeOfRecord)Enum.Parse(typeof(Fask.Interfaces.SkladLokace.TypeOfRecord), cbPohybType.SelectedValue.ToString());
                        // -> pokud vydej, tak kontrola, zdali je uvedene mnozstvi vubec na sklade
                        if ((record == Fask.Interfaces.SkladLokace.TypeOfRecord.V) || (record == Fask.Interfaces.SkladLokace.TypeOfRecord.D ))
                        {
                            Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();
                            filtr.MaterialITEMNMBR = tbItemnmbr.Text.Trim();
                            filtr.MaterialLocncode = tbLocncode.Text.Trim();
                            filtr.MaterialSklID = tbSklID.Text.Trim();
                            filtr.MaterialSerltnum = tbSerltnum.Text.Trim();
                            //Fask.Interfaces.DataSets.SkladLokace data = ((Fask.Interfaces.SkladLokace.ISkladLokace)providerSkladLokacePohyb).GetFiltrovanySkladLokaceStav(filtr);

                            Fask.Interfaces.DataSets.SkladLokace data;

                            if ((providerSkladLokacePohyb != null) && (providerSkladLokacePohyb is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav))
                                data = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav)providerSkladLokacePohyb).GetFiltrovanySkladLokaceStav(filtr);
                            else
                                throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFiltrovanySkladLokaceStav.");


                            Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_StavRow row = data.CZMST_SkladLokace_Stav.Count > 0 ? data.CZMST_SkladLokace_Stav.First() : null;
                            if(row == null || row.QTYSHPPD < qty)
                                errorProvider1.SetError(tbQtyshppd, "Není možné vydávat do mínusu.");
                        }
                    }
                }

                // 6) kontrola existence uzivatele
                int userid = 0;
                if (string.IsNullOrEmpty(tbUserID.Text.Trim()))
                    errorProvider1.SetError(tbUserID, "Musíte vybrat uživatele");
                else
                {
                    bool res = Int32.TryParse(tbUserID.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out userid);
                    if (!res)
                    {
                        errorProvider1.SetError(tbUserID, "ID uživatele musí být číslo");
                    }
                    else
                    {
                        // kontrola existence uzivatele
                        //Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow user = ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatelByID(Convert.ToInt32(tbUserID.Text.Trim()));
                        //Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow user;

                        //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID))
                        //    user = ((Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID)providerUzivatele).GetUzivatelByID(Convert.ToInt32(tbUserID.Text.Trim()));
                        //else
                        //    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatelByID.");

                        var _validUserid = FASK.Logins.Uzivatel.Instance.Komunikace.GetLoginsByID(tbUserID.Text.Trim());

                        if (_validUserid == null || _validUserid.Count == 0)
                            errorProvider1.SetError(tbUserID, "Uživatel s ID '" + tbUserID.Text.Trim() + "' neexistuje");
                    }
                }

                // 7) kontrola poctu znaku v document number
                // TODO: dotahnout delku z datasetu
                if(tbDocumentNumber.Text.Trim().Length > 17)
                    errorProvider1.SetError(tbDocumentNumber, "Je možné zadat maximálně 17 znaků.");

                // 8) kontrola mnozstvi pro pracovnika
                decimal qty_owner = 0;
                if (!string.IsNullOrEmpty(tbQTY_owner.Text))
                {
                    bool res = decimal.TryParse(tbQTY_owner.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out qty_owner);
                    if (!res)
                    {
                        errorProvider1.SetError(tbQTY_owner, "Počet musí být číslo");
                    }
                }
                //else 
                //{
                //    errorProvider1.SetError(tbQTY_owner, "Zadejte počet.");
                //}

                //9 kontrola zadani SN
                // 27.9.2018 JiS 
                // => Pokud neni sn zadano, tak nevadi, pro PneuSafr umoznit provest i kdyz neni sn zadane, kvuli chybam pri nabehu
                // => nevadi, pokud nebude SN nalezeno na stavu skladu, tak to stejne neudela ... 
                //if (string.IsNullOrEmpty(tbSerltnum.Text.Trim()))
                //    errorProvider1.SetError(tbSerltnum, "Musíte zadat SN");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        #endregion

        #region Poznamka + Kontrola ITEMNMBR po zadani ručně

        private void formZdrojeEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    if (tbDocumentNumber == tb)
                    {
                        text =
                            "Číslo dokumentu (objednávky vydané, faktury, ...), případně poznámka.\n" +
                            "Maximálně je možné zadat 17 znaků";
                    }
                    else if (tbItemnmbr == tb)
                    {
                        text = "Identifikátor zboží.";
                    }
                    else if (tbQtyshppd == tb)  // oznaceni
                    {
                        text = "Množství, které se bude přijímat/vydávat.";
                    }
                    else if (tbSklID == tb)
                    {
                        text = "Identifikátor skladu.";
                    }
                    else if (tbLocncode == tb)  // oznaceni
                    {
                        text = "Identifikátor lokace.";
                    }
                    else if (tbUserID == tb)  // oznaceni
                    {
                        text = "Identifikátor uživatele.";
                    }
                    else if (tbSerltnum == tb)  // oznaceni
                    {
                        text = "Šarže, pokud je materiál sledován na šarže.";
                    }
                    else if (tbPracovnikID == tb)  // oznaceni
                    {
                        text = "Identifikátor pracovníka.";
                    }
                    else if (tbQTY_owner == tb)  // oznaceni
                    {
                        text = "Počet přiřazen pracovníkovi.";
                    }


                }
                else if (sender is ComboBox)
                {
                    ComboBox cb = ((ComboBox)sender);
                    if (cbPohybType == cb)  // typ pohybu
                    {
                        if (_typPohybuFlag)
                        {
                            text =
                                "Typ pohybu, který se má provést.\n" +
                                "Příjem - příjem na sklad\n" +
                                "Výdej - výdej ze skladu";
                        }
                        else
                        {
                            text =
                                "Typ pohybu, který se má provést.\n" +
                                "Převod - výdej ze skladu a příjem na sklad";
                        }
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }

        private void tbItemnmbr_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbItemnmbr.Text.Trim()))
                    return;

                Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zbozi = null;

                if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID))
                {
                    zbozi = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID)providerZbozi).GetZboziByID(tbItemnmbr.Text.Trim());
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetZboziByID");
                }

                if (zbozi != null)
                {
                    // nalezeno, vyplnit nazev
                    tbITEMDESC.Text = (zbozi.IsITEMDESCNull() ? string.Empty : zbozi.ITEMDESC.Trim()) + (zbozi.IsITEMDESCNull() ? string.Empty : (" (" + zbozi.ITEMCODE.Trim() + ")"));
                    errorProvider1.Clear();
                }
                else
                {
                    // nenalezeno, zobrazit chybovou hlasku
                    tbITEMDESC.Text = string.Empty;
                    errorProvider1.SetError(tbItemnmbr, "Materiál s ID '" + tbItemnmbr.Text.Trim() + "' neexistuje v číselníku zboží");
                }
            }
            catch { tbITEMDESC.Text = string.Empty; }
        }


        #endregion

        #region RFID logika pro načtení SN z Tagu, řešeno pro ZZSJMK

        private void btn_RFID_Load_Click(object sender, EventArgs e)
        {
            try
            {
                //Timer_Test_.Enabled = !Timer_Test_.Enabled;
                if (RFID.isOpen())
                {
                    RFIDStop(); // CloseRFID();
                }
                else
                {
                    RFIDStart(); // OpenRFID();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #region RFID

        private void RFIDStop()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Stop();
                    btn_RFID_Load.BackColor = Color.Red;
                    btn_RFID_Load.Text = "Start Read";
                }

            }
            catch (Exception ex)
            {
                btn_RFID_Load.BackColor = Color.Red;
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        private void RFIDStart()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.DataReady += new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Start();

                    btn_RFID_Load.BackColor = Color.Green;
                    btn_RFID_Load.Text = "Stop Read";
                }

            }
            catch (Exception ex)
            {
                btn_RFID_Load.BackColor = Color.Red;
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        void RFID_DataReady(object sender, IRFIDProvider.RFIDEventArgs e)
        {
            AddData(e.TagIDs);
        }

        public void AddData(List<string> list)
        {
            if (list == null)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { this.AddData(list); });
                return;
            }

            try
            {
                List<string> l = list;

                if (list.Count > 0)
                {

                    string epc = list[0];

                    Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();
                    filtr.MaterialSerltnum = epc.Trim();

                    Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();


                    //ds = ((Fask.Interfaces.SkladLokace.ISkladLokace)providerSkladLokacePohyb).GetFiltrovanySkladLokaceStav(filtr);

                    if ((providerSkladLokacePohyb != null) && (providerSkladLokacePohyb is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav))
                        ds = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav)providerSkladLokacePohyb).GetFiltrovanySkladLokaceStav(filtr);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFiltrovanySkladLokaceStav.");


                    if (ds != null && ds.CZMST_SkladLokace_Stav.Count == 0)
                    {
                        RFIDStop();
                        tbSerltnum.Text = epc;
                    }
                    else
                    {
                        RFIDStop();
                        if (MessageBox.Show(this, "Položka nalezena v lokačním mechanizmu", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK)
                        {
                            RFIDStart();
                        }
                    }

                }

                //nasnimaneKody.AddRange(list);
                //PridejNasnimanePolozky();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #endregion

        #endregion
    }
}
