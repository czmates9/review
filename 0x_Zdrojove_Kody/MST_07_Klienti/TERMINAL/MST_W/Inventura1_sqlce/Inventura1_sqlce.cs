using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using System.Threading;
using Fask.MST_W.ServerAccess;
using System.Net;

namespace Fask.MST_W.Inventura1_sqlce
{
    public partial class Inventura1_sqlce : System.Windows.Forms.Form
    {
        /// <summary>
        /// Instance modulu inventura1
        /// </summary>
        public static Inventura1_sqlce Inventura1_sqlce_Instance = null;
        /// <summary>
        /// globalni objekt pro udrzovani komunikacnich objektu a objektu instance inventury
        /// </summary>
        public GlobalObject globalObject = new GlobalObject();        
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Inventura1_sqlce()
        {
            InitializeComponent();
        }

        #region Form actions
        
        private void Inventura1classForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            try
            {
                Inventura1_sqlce_Instance = this;

                this.menuItemCiselnikySklady.Enabled = MST_Global.Inventura1PouzitCiselnikSkladu;
                this.buttonStahniSklady.Enabled = MST_Global.Inventura1PouzitCiselnikSkladu;
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                PerformCancel();
                return;
            }

        }

        private void Inventura1classForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Shift && !e.Control)
            {
                if (e.KeyCode == Keys.D1)
                {
                    //buttonZpracujDavku_Click(null, null);
                    zpracujDavku();
                }
                else if (e.KeyCode == Keys.D2)
                {
                    //buttonStahniDavku_Click(null, null);
                    stahniDavku();
                }
                else if (e.KeyCode == Keys.D3)
                {
                    //odesliHotovouDavku_but_Click(null, null);
                    odesliHotovouDavku();
                }
                else if (e.KeyCode == Keys.D4)
                {
                    //vratitDavku_but_Click(null, null);
                    vratitDavku();
                }
                else if (e.KeyCode == Keys.D5)
                {
                    if (MST_Global.Inventura1PouzitCiselnikSkladu)
                        menuItemCiselnikySklady_Click(null, null);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }
            else
                return;

            e.Handled = true;
        }

        #region Ending form actions
        private void PerformCancel()
        {
            if (MST_Global.Inventura1_DialogOpusteniModulu)
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Inventura1SqlceUkonceniPraceSModulemDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
            == DialogResult.No)
                {
                    return;
                }
            }

            finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            this.globalObject.Dispose();
            Inventura1_sqlce_Instance = null;
        }
        #endregion

        #endregion

        #region Buttons actions

        private void buttonKonec_Click_1(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonStahniDavku_Click(object sender, EventArgs e)
        {
            stahniDavku();
        }
        
        private void buttonZpracujDavku_Click(object sender, EventArgs e)
        {
            zpracujDavku();
        }

        private void odesliHotovouDavku_but_Click(object sender, EventArgs e)
        {
            odesliHotovouDavku();
        }

        private void vratitDavku_but_Click(object sender, EventArgs e)
        {
            vratitDavku();
        }

        private void buttonZpracujDavku_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //buttonZpracujDavku_Click(null, null);
                    zpracujDavku();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void buttonStahniDavku_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    stahniDavku();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void odesliHotovouDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //odesliHotovouDavku_but_Click(null, null);
                    odesliHotovouDavku();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void vratitDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //vratitDavku_but_Click(null, null);
                    vratitDavku();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void menuItemCiselnikySklady_Click(object sender, EventArgs e)
        {
            StahnoutCiselnikSklady();
        }

        private void buttonStahniSklady_Click(object sender, EventArgs e)
        {
            StahnoutCiselnikSklady();
        }

        private void buttonStahniSklady_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    buttonStahniSklady_Click(null, null);
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void menuItemKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        #endregion

        #region Main actions 
        private void stahniDavku()
        {
            try
            {
				int Davkatmp = 0;
                Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;

                if (MST_Global.Inventura1PouzitCiselnikSkladu)
                {
                    // vyber skladu, pro ktery chci davky ... 
                    using (FormSkladVyber fsv = new FormSkladVyber())
                    {
                        if (fsv.ShowDialog() == DialogResult.Cancel)
                            return;
                        sklad = fsv.Sklad;
                    }
                }

                Inventura1Service.Inventury1 inventury;
                try
                {
                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Inventura1Inventura1SqlceStahujeSeSeznamInventur);

                    inventury = this.globalObject.inventuraclassService.GetInventury(MST_Global.TerminalID, sklad != null ? sklad.skl_id.Trim() : null);
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message);
                    return;
                }
                finally
                {
                    Program.mstw.mbw.EndPracujiForm();
                }

                using (ListInventury_sqlce listInventury = new ListInventury_sqlce(inventury))
                {
                    listInventury.Owner = this;
                    while (true)
                    {
                        if (listInventury.ShowDialog() == DialogResult.Cancel)
                            return;
                        if (File.Exists(Path.Combine(Main.StorageDir, listInventury.Davka.ToString() + "." + Main.Ext_Inventura1)))
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Inventura1Inventura1SqlceStavkaJeJizStazena, listInventury.Davka), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
					Davkatmp = listInventury.Davka;
                }

				string filenamedavka = Davkatmp.ToString() + "." + Main.Ext_Inventura1;
                string filenamedavkafullpath = Path.Combine(Main.StorageDir, filenamedavka);

                try
                {
                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Inventura1Inventura1SqlcePripravaDatDavkyInventury);

                    // novy stahovaci kod ... 
					if (!this.globalObject.inventuraclassService.PrepareInventuraDB(Davkatmp, MST_Global.TerminalID))
                    {
                        throw new Exception("Pøíprava dat dávky inventury neuspìla");
                    }

                    FileTransfer.Routines.DownloadDecompressDelete(filenamedavkafullpath);

					if (!this.globalObject.inventuraclassService.GetInventuraReceived(Davkatmp, MST_Global.TerminalID))
                    {
                        File.Delete(filenamedavkafullpath);
                        Program.mstw.mbw.EndPracujiForm();
                        MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Inventura1SqlceProblemStazeniInventury, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }

                    Program.mstw.mbw.EndPracujiForm();
                }
                catch (Exception ex)
                {
                    File.Delete(filenamedavkafullpath);
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message);
                    return;
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }

        }

        private void zpracujDavku()
        {
			try
			{

				// najde vsechny davky na disku
				string[] fileNames = null;
				try
				{
					fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura1);
				}
				catch (Exception ex)
				{
					MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
					return;
				}


				while (fileNames.Length == 0)
				{
					if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Inventura1SqlceNaDiskuNejsouZadneDavky, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
						== DialogResult.No)
						return;

					stahniDavku();

					try
					{
						// nacte vsechny davky z disku
						fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura1);
					}
					catch (Exception ex)
					{
						MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
						return;
					}
				}

				using (ListInventury_sqlce listinventury = new ListInventury_sqlce(fileNames))
				{
					listinventury.Owner = this;
					if (listinventury.ShowDialog() == DialogResult.Cancel)
						return;

					this.globalObject.Davka = listinventury.Davka;
				}

				using (ListPolozky_sqlce listpolozky = new ListPolozky_sqlce())
				{
					listpolozky.Owner = this;
					listpolozky.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
			finally
			{
				this.globalObject.Davka = null;
			}
        }

        private void odesliHotovouDavku()
        {
            try
            {

                // najde vsechny davky na disku
                string[] fileNames = null;
                try
                {
                    fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura1);
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }

				int Davkatmp = 0;

                using (ListInventury_sqlce listinv = new ListInventury_sqlce(fileNames))
                {
                    listinv.Owner = this;
                    if (listinv.ShowDialog() == DialogResult.Cancel)
                        return;
                    Davkatmp = listinv.Davka;
                }

                // TODO : kontrola uplnosti davky na velkem mnozstvi dat selhava ... !!!
                //if (stavInventury())
                //{
                //    if (MessageBoxBig.Show("Tato dávka ješte není dokonèena.\nOpravdu ji chcete odeslat?",
                //        "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
                //        return;
                //}

				if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1Inventura1SqlceOdeslatDataDotaz, Davkatmp), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                    return;

				odeslatData(true, Davkatmp);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void vratitDavku()
        {
            try
            {
                // najde vsechny davky na disku
                string[] fileNames = null;
                try
                {
                    fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura1);
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }

				int tmpDavka = 0;
                using (ListInventury_sqlce listinv = new ListInventury_sqlce(fileNames))
                {
                    listinv.Owner = this;
                    if (listinv.ShowDialog() == DialogResult.Cancel)
                        return;
					tmpDavka = listinv.Davka;
                }

				using (GlobalObject go = new GlobalObject())
				{
					go.Davka = tmpDavka;

					if (go.controller_inventura1.dataExistsInI4())
					{
						if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Inventura1SqlceVraceniDavkaObsahujePolozkyDotaz,
							Fask.Localization.Localization.Inventura1Inventura1SqlceDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
							return;
					}

				}

				odeslatData(false, tmpDavka);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }
        #endregion

        #region Processes
        /// <summary>
        /// Odesle data
        /// </summary>
        /// <param name="zpracovat">True - data se zpracuji. False - data se uvolni ke zpracovani na jinem terminalu</param>
        /// <returns></returns>
        private bool odeslatData(bool zpracovat, int Davka)
        {// odeslani dat
            while (true)
            {
                try
                {
                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Inventura1Inventura1SqlceDataSeOdesilaji);
                    Fask.MST_W.Inventura1Service.ProcessInventuraState processState =
                        zpracovat ?
                        Fask.MST_W.Inventura1Service.ProcessInventuraState.Zpracovat :
                        Fask.MST_W.Inventura1Service.ProcessInventuraState.Uvolnit;

                    this.globalObject.inventuraclassService.Timeout = MST_Global.ServiceTimeOut;

                    #region new upload code

                    try
                    {
						string filename = Path.Combine(Main.StorageDir, Davka.ToString() + "." + Main.Ext_Inventura1);
						string filenameZip = filename + ".zip";
						string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

						Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filenameZip);

						if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Inventura1) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };



						//string filename = Path.Combine(Main.StorageDir, Davka.ToString() + "." + Main.Ext_Inventura1);
                        //FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");
                        //Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Inventura1);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(Fask.Localization.Localization.Inventura1Inventura1SqlceOdeslaniDatProblem + " " + ex.Message);
                    }

                    #endregion

					Inventura1Service.StatusObject so = this.globalObject.inventuraclassService.ProcessInventura2(Davka, MST_Global.TerminalID, processState);
                    while (so.StatusText != "OK" || so.Exception)
                    {// nepodarilo se odeslat
                        Program.mstw.mbw.EndPracujiForm();
                        if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1Inventura1SqlceZpracovaniDatProblemopakovatDotaz, so.StatusText), Fask.Localization.Localization.Inventura1Inventura1SqlceChyba, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
                            == DialogResult.Cancel)
                            return false;
                        Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Inventura1Inventura1SqlceOpakujeSeZpracovaniDat);
                    }
                    //else
                    {// odeslani se zdarilo
                        Program.mstw.mbw.EndPracujiForm();

						try
						{
							File.Delete(Path.Combine(Main.StorageDir, Davka + "." + Main.Ext_Inventura1));

						}
						catch (Exception exDelete)
						{
							;
							throw exDelete;
							;
						}
						//this.globalObject.Davka = null;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1Inventura1SqlceOdeslanidatProblemopakovatDotaz, ex.Message), Fask.Localization.Localization.Inventura1Inventura1SqlceChyba, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
                        == DialogResult.Cancel)
                        return false;
                }
                finally
                {
                    this.globalObject.inventuraclassService.Timeout = MST_Global.ServiceTimeOut;
                }
            }
        }

        /// <summary>
        /// Zjisti stav inventury, true=uplna, false=neuplna
        /// </summary>
        /// <returns>True je-li uplna, jinak False</returns>
        public bool stavInventury()
        {
            int? zbyva = Inventura1_sqlce_Instance.globalObject.controller_inventura1.ZbyvaPolozek_queries();
            return (zbyva ?? 0) == 0;
        }



        private void StahnoutCiselnikSklady()
        {
            try
            {
                Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Inventura1Inventura1SqlceStahujeSeCiselnikSkladu);
                _WebRefernces_Globals.CiselnikServiceSession ciselnikService = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
                ciselnikService.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                ciselnikService.Timeout = MST_Global.ServiceTimeOut;
                ciselnikService.UpdateWebServiceCredentials();

                if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Inventura1SqlceProvestExportSkladuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSkladyExport(ciselnikService);
                }

                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSklady(ciselnikService);

            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            Program.mstw.mbw.EndPracujiForm();
        }
        #endregion

    }
}