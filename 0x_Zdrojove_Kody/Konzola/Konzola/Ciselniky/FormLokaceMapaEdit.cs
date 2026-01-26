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

namespace Konzola.Ciselniky
{
    public partial class FormLokaceMapaEdit : Form
    {
        private Fask.Interfaces.IMES providerMapa = null;
        private Fask.Interfaces.IMES providerTypy = null;
        private Fask.Interfaces.IMES providerSklady = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow returnrow { get; set; }
        /// <summary>
        /// Lokace, která se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow lokacerow { get; set; }

        public FormLokaceMapaEdit()
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
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerMapa == null)
                    throw new Exception("Provider 'Mapa lokací' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                if(providerTypy == null)
                    throw new Exception("Provider 'Typy lokací' není inicializován");

                LoadData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
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
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex, true);
            }

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerTypy == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2).IsAssignableFrom(t))
                                {
                                    providerTypy = (Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerTypy != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerTypy.InitProvider();
               
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
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
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            // je úprava záznamu, dojde k načtení dat
            if (lokacerow != null)
            {
                tbSklID.Enabled = false;
                tbSklID.Text = lokacerow.IsSKL_IDNull() ? string.Empty : lokacerow.SKL_ID.Trim();
                tbLocncode.Enabled = false;
                tbLocncode.Text = lokacerow.IsLOCNCODENull() ? string.Empty : lokacerow.LOCNCODE.Trim();

                tbBarcode.Text = lokacerow.IsBarcodeNull() ? string.Empty : lokacerow.Barcode.Trim();
                tbDescription.Text = lokacerow.IsDescriptionNull() ? string.Empty : lokacerow.Description.Trim();
                tbType.Text = lokacerow.IsTYPENull() ? string.Empty : lokacerow.TYPE.Trim();

                btn_SKL_ID.Enabled = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
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
                if (lokacerow != null)
                {
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow stav_test = ((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow stav_test;


                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode))
                        stav_test = ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode.");


                    if (stav_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(lokacerow, stav_test);
                        if (!isMatch)
                        {
                            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                // kvuli refreshi
                                this.DialogResult = DialogResult.OK;
                                return;
                            }
                        }
                    }
                }

                Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();
                Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow newMapaRow = ds2.CZMST_SkladLokace_Mapa.NewCZMST_SkladLokace_MapaRow();

                newMapaRow.SKL_ID = tbSklID.Text.Trim();
                newMapaRow.LOCNCODE = tbLocncode.Text.Trim();
                newMapaRow.Barcode = tbBarcode.Text.Trim();

                if (string.IsNullOrEmpty(tbDescription.Text.Trim()))
                    newMapaRow.SetDescriptionNull();
                else
                    newMapaRow.Description= tbDescription.Text.Trim();

                if (string.IsNullOrEmpty(tbType.Text.Trim()))
                    newMapaRow.SetTYPENull();
                else
                    newMapaRow.TYPE = tbType.Text.Trim();

                // je úprava záznamu
                if (lokacerow != null)
                {
                    //((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).UpdateSkladLokace_Mapa(newMapaRow);

                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_UpdateSkladLokace_Mapa))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_UpdateSkladLokace_Mapa)providerMapa).UpdateSkladLokace_Mapa(newMapaRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_UpdateSkladLokace_Mapa.");
                
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).InsertSkladLokace_Mapa(newMapaRow);

                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_InsertSkladLokace_Mapa))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_InsertSkladLokace_Mapa)providerMapa).InsertSkladLokace_Mapa(newMapaRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_InsertSkladLokace_Mapa.");
                
                }

                
                // nacteni zaznamu vcetne dat z joinu ... pokud neprojde, tak alespon nove vytvoreny zaznam
                try
                {
                    //returnrow = ((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(newMapaRow.SKL_ID, newMapaRow.LOCNCODE);

                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode))
                        returnrow = ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(newMapaRow.SKL_ID, newMapaRow.LOCNCODE);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode.");
                
                }
                catch
                {
                    try
                    {
                        ds2.CZMST_SkladLokace_Mapa.AddCZMST_SkladLokace_MapaRow(newMapaRow);
                        ds2.CZMST_SkladLokace_Mapa.AcceptChanges();
                        returnrow = newMapaRow;
                    }
                    catch { }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

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

                    if(sklad == null)
                        errorProvider1.SetError(tbSklID, "Sklad s ID '" + tbSklID.Text.Trim() + "' neexistuje v číselníku skladů");
                }

                if (string.IsNullOrEmpty(tbLocncode.Text.Trim()))
                    errorProvider1.SetError(tbLocncode, "Musíte zadat id lokace");

                //if (string.IsNullOrEmpty(tbBarcode.Text.Trim()))
                //    errorProvider1.SetError(tbBarcode, "Musíte zadat č. kod lokace");

                // kontrola existence typu, pokud je vyplnen
                if (!string.IsNullOrEmpty(tbType.Text.Trim()))
                {
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow typ = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypy).GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow typ;

                    if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType))
                        typ = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType)providerTypy).GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType.");
                    
                    if(typ == null)
                        errorProvider1.SetError(tbType, "Vybraný typ lokace není zaveden v číselníku typů lokací.");
                }

                if (lokacerow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow stav = ((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow stav;


                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode))
                        stav = ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSklID.Text.Trim(), tbLocncode.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode.");
                    
                    if (stav != null)
                    {
                        tbSklID.Focus();
                        errorProvider1.SetError(tbLocncode, "Lokace '" + tbLocncode.Text.Trim() + "' ve skladu '" + tbSklID.Text.Trim() + "' již existuje");
                    }
                }
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

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {
        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void formZdrojeEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    if (tbSklID == tb)
                    {
                        text = "Identifikátor skladu.";
                    }
                    else if (tbLocncode == tb)  // oznaceni
                    {
                        text = "Identifikátor lokace.";
                    }
                    else if (tbType == tb)  // oznaceni
                    {
                        text = "Typ lokace z číselníku typů lokací.";
                    }
                    else if (tbDescription == tb)  // oznaceni
                    {
                        text = "Označení lokace.";
                    }
                    else if (tbBarcode == tb)  // oznaceni
                    {
                        text = "Čárový kód lokace.";
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }

        private void btn_SKL_ID_Click(object sender, EventArgs e)
        {
            Hledat_ID_Skladu();
        }

        private void Hledat_ID_Skladu()
        {
            using (Konzola.Ciselniky.FormSkladyList fpeck = new FormSkladyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
            {
                fpeck.Text = "Výběr ID skladu";
                if (fpeck.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                tbSklID.Text = fpeck.CZMST093_selectedRow.skl_id.Trim();

            }
        }

        private void btn_Typ_Click(object sender, EventArgs e)
        {
            Hledat_Typ_Lokace();
        }

        private void Hledat_Typ_Lokace()
        {
            using (Konzola.Ciselniky.FormLokaceTypyList fpeck = new FormLokaceTypyList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
            {
                fpeck.Text = "Výběr typu lokace";
                if (fpeck.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                tbType.Text = fpeck.CZMST_SkladLokace_LokaceTypy_selectedRow.TYPE.Trim();

            }
        }
    }
}
