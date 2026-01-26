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
using System.Web.Services.Protocols;

namespace Fask.MST_W.Inventura2
{
    public partial class Inventura2 : System.Windows.Forms.Form
    {
        //public static Inventura2 Inventura2_sqlce_Instance = null;
        //private _WebRefernces_Globals.Inventura2ServiceSession inventura2Service;

        //private string davka;

		/// <summary>
		/// Instance modulu inventura1
		/// </summary>
		public static Inventura2 Inventura2_Instance = null;
		/// <summary>
		/// globalni objekt pro udrzovani komunikacnich objektu a objektu instance inventury
		/// </summary>
		public GlobalObject globalObject = new GlobalObject();

		#region Eventy formu + c'tor

		public Inventura2()
		{
			InitializeComponent();
			this.Text = MST_Global.Inventura2Name.Trim();
		}

		private void Inventura2Form_Load(object sender, EventArgs e)
		{
			this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
			this.Size = Forms.FormLocation.ScreenResolution;
			try
			{
				Inventura2_Instance = this;
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				PerformCancel();
				return;
			}

		}

		private void Inventura2Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Alt && !e.Shift && !e.Control)
			{
				if (e.KeyCode == Keys.D1)
				{
					zpracujDavku();
				}
				else if (e.KeyCode == Keys.D2)
				{
					stahniDavku();
				}
				else if (e.KeyCode == Keys.D3)
				{
					odesliHotovouDavku_but_Click(null, null);
				}
				else if (e.KeyCode == Keys.D4)
				{
					vratitDavku_but_Click(null, null);
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


		#endregion

		#region Button Eventy

		private void buttonKonec_Click_1(object sender, EventArgs e)
		{
			PerformCancel();
		}

		private void buttonStahniDavku_Click(object sender, EventArgs e)
		{
			stahniDavku();
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

		private void buttonZpracujDavku_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Alt && !e.Control && !e.Shift)
			{
				if (e.KeyCode == Keys.Enter)
				{
					zpracujDavku();
				}
				else
					return;
			}
			else
				return;

			e.Handled = true;
		}

		private void buttonZpracujDavku_Click(object sender, EventArgs e)
		{
			zpracujDavku();
		}

		private void odesliHotovouDavku_but_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Alt && !e.Control && !e.Shift)
			{
				if (e.KeyCode == Keys.Enter)
				{
					odesliHotovouDavku_but_Click(null, null);
				}
				else
					return;
			}
			else
				return;

			e.Handled = true;
		}

		private void odesliHotovouDavku_but_Click(object sender, EventArgs e)
		{
			OdeslatDavku();
		}

		private void vratitDavku_but_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Alt && !e.Control && !e.Shift)
			{
				if (e.KeyCode == Keys.Enter)
				{
					vratitDavku_but_Click(null, null);
				}
				else
					return;
			}
			else
				return;

			e.Handled = true;
		}

		private void vratitDavku_but_Click(object sender, EventArgs e)
		{
			VratitDavku();
		}

		#endregion

        private void stahniDavku()
        {
			string Davkatmp = "0";
            Inventura2Service.Inventury2 inventury;
            try
            {
                Program.mstw.mbw.BeginPracujiForm("Stahuje se seznam dostupných inventur");
                inventury = this.globalObject.inventuraclassService.GetInventury(MST_Global.TerminalID);
            }
            catch (SoapException soapException)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(soapException, "Inventura2.stahniDavku");
                MessageBoxBig.Show(soapException.Message);
                return;
            }
            catch (WebException webEx)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(webEx, "Inventura2.stahniDavku");
                MessageBoxBig.Show(webEx.Message);
                return;
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex, "Inventura2.stahniDavku");
                MessageBoxBig.Show(ex.Message);
                return;
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }

            using(ListInventury listInventury = new ListInventury(inventury))
            {
                listInventury.Owner = this;
                if (listInventury.ShowDialog() == DialogResult.Cancel)
                    return;
				Davkatmp = listInventury.Davka;
            }

			string filenameDavka = Davkatmp.ToString() + "." + Main.Ext_Inventura2;
            string filenameFullPath = Path.Combine(Main.StorageDir, filenameDavka);

            try
            {
                Program.mstw.mbw.BeginPracujiForm("Příprava dat dávky inventury");

                //inventura2Service.Timeout = MST_Global.ServiceTimeOut;

				if (!this.globalObject.inventuraclassService.PrepareDB(Davkatmp, MST_Global.TerminalID))
                {
                    throw new Exception("Příprava dat dávky inventury neuspěla");
                }

                FileTransfer.Routines.DownloadDecompressDelete(filenameFullPath);

				if (!this.globalObject.inventuraclassService.ReceivedDB(Davkatmp, MST_Global.TerminalID))
                {
                        //MySystem.FileOperations.DBSave(Path.Combine(Main.DataDir, davka.ToString() + "." + Main.Inventura1I1), ref dataDavka);
                        //potvrzeni prijeti davky se povedlo => odstranit priponu tmp souboru
                        File.Delete(filenameFullPath);
                        Program.mstw.mbw.EndPracujiForm();
                        return;   
                }

                Program.mstw.mbw.EndPracujiForm();
            }
            catch (Exception ex)
            {
                File.Delete(filenameFullPath);
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message : ""), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }
			//finally
			//{
			//    //inventura2Service.Timeout = MST_Global.ServiceTimeOut;
			//}
        }

        private void zpracujDavku()
        {
            // najde vsechny davky na disku
            string[] fileNames = null;
            try
            {
                fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura2);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }


            while (fileNames.Length == 0)
            {
                if (MessageBoxBig.Show("Na disku nejsou žádné dávky. Chcete je stáhnout?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                    return;

                stahniDavku();

                try
                {
                    // nacte vsechny davky z disku
                    fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura2);
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
            }

            using (ListInventury listinventury = new ListInventury(fileNames))
            {
                listinventury.Owner = this;
                if (listinventury.ShowDialog() == DialogResult.Cancel)
                    return;

				this.globalObject.Davka = Convert.ToInt32(listinventury.Davka);
            }

			try
			{
				//Globals.Davka = davka;
				//Globals.Initialize();

				DialogResult dr = DialogResult.None;

				if (!MST_Global.Inventura2DotazLokace)
					this.globalObject.active_lokace = null;
				else
				{
					dr = MessageBoxBig.Show("Zadat lokaci?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
					if (dr == DialogResult.Cancel)
						return;
					else if (dr == DialogResult.Yes)
					{
						using (ZmenaLokace zmenaLokace = new ZmenaLokace())
						{
							zmenaLokace.Owner = this;
							zmenaLokace.Lokace = null;
							if (zmenaLokace.ShowDialog() == DialogResult.Cancel)
								return;
							this.globalObject.active_lokace = zmenaLokace.Lokace;
						}
					}
					else if (dr == DialogResult.No)
					{
						this.globalObject.active_lokace = null;
					}
				}

				if (!MST_Global.Inventura2DotazKancl)
					this.globalObject.active_kancelar = null;
				else
				{
					dr = MessageBoxBig.Show("Zadat umístění?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
					if (dr == DialogResult.Cancel)
						return;
					else if (dr == DialogResult.Yes)
					{
						using (ZmenaKancl zmenaKancl = new ZmenaKancl())
						{
							zmenaKancl.Owner = this;
							zmenaKancl.Kancelar = null;
							if (zmenaKancl.ShowDialog() == DialogResult.Cancel)
								return;
							this.globalObject.active_kancelar = zmenaKancl.Kancelar;
						}
					}
					else if (dr == DialogResult.No)
					{
						this.globalObject.active_kancelar = null;
					}
				}

				if (!MST_Global.Inventura2DotazStredisko)
					this.globalObject.active_stredisko = null;
				else
				{
					dr = MessageBoxBig.Show("Zadat středisko?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
					if (dr == DialogResult.Cancel)
						return;
					else if (dr == DialogResult.Yes)
					{
						using (ZmenaStredisko zmenaStredisko = new ZmenaStredisko())
						{
							zmenaStredisko.Owner = this;
							zmenaStredisko.Stredisko = null;
							if (zmenaStredisko.ShowDialog() == DialogResult.Cancel)
								return;
							this.globalObject.active_stredisko = zmenaStredisko.Stredisko;
						}
					}
					else if (dr == DialogResult.No)
					{
						this.globalObject.active_stredisko = null;
					}
				}

				if (!MST_Global.Inventura2DotazOsoba)
					this.globalObject.active_osoba = null;
				else
				{
					dr = MessageBoxBig.Show("Zadat osobu?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
					if (dr == DialogResult.Cancel)
						return;
					else if (dr == DialogResult.Yes)
					{
						using (ZmenaOsoba zmenaOsoba = new ZmenaOsoba())
						{
							zmenaOsoba.Owner = this;
							zmenaOsoba.Osoba = null;
							if (zmenaOsoba.ShowDialog() == DialogResult.Cancel)
								return;
							this.globalObject.active_osoba = zmenaOsoba.Osoba;
						}
					}
					else if (dr == DialogResult.No)
					{
						this.globalObject.active_osoba = null;
					}
				}

				using (ListPolozky listpolozky = new ListPolozky())
				{
					listpolozky.Owner = this;
					listpolozky.ShowDialog();
				}

			}
			catch(Exception ex)
			{
				Logging.Log.Write(ex);
			}
			finally
			{
				this.globalObject.Davka = null;
				this.globalObject.active_kancelar = null;
				this.globalObject. active_lokace = null;
				this.globalObject. active_osoba = null;
				this.globalObject. active_stredisko = null;
				this.globalObject.active_parametry = null;
			}
        }

		private void OdeslatDavku()
		{
			string Davkatmp = "0";
			// najde vsechny davky na disku
			string[] fileNames = null;
			try
			{
				fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura2);
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return;
			}

			using (ListInventury listinv = new ListInventury(fileNames))
			{
				listinv.Owner = this;
				if (listinv.ShowDialog() == DialogResult.Cancel)
					return;
				Davkatmp = listinv.Davka;
			}

			// Kontrola uplnosti davky na velkem mnozstvi dat selhava ... !!!
			// upraveno, pomoci dodatkoveho sloupce na predloze s mnozstvim nasnimanym...
			// TODO : otestovat rychlost odezvy na velkem mnozstvi dat

			Fask.SQLiteDBs.DataSets.Inventura2.ParametryDataTable paramsdt = null;
			bool StavInv = false;
			using (GlobalObject g = new GlobalObject())
			{
				g.Davka = Convert.ToInt32(Davkatmp);
				paramsdt = g.controller_inventura2.GetData_Parametry();

				int? zbyva = (int)g.controller_inventura2.ZbyvaPolozek();

				if ((zbyva ?? 0) == 0)
					StavInv = true;
			}

			//SqlCEDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter paramsta = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter();
			//paramsta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_Inventura2);
			//SqlCEDBs.DataSets.Inventura2.ParametryDataTable paramsdt = paramsta.GetData();

			if (paramsdt != null && paramsdt.Count > 0 && paramsdt[0].CFG_KontrolaUplnosti && StavInv)
			{
				if (MessageBoxBig.Show("Tato dávka ješte není dokončena.\nOpravdu ji chcete odeslat?",
					"Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
					return;
			}
			else
			{
				if (MessageBoxBig.Show("Odeslat data inventury č." + Davkatmp + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
					== DialogResult.No)
					return;
			}

			//if (MessageBoxBig.Show("Odeslat data inventury č." + this.davka + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
			//    == DialogResult.No)
			//    return;


			odeslatData(true, Davkatmp);
		}

		private void VratitDavku()
		{
			string Davkatmp = "0";
			// najde vsechny davky na disku
			string[] fileNames = null;
			try
			{
				fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Inventura2);
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return;
			}

			using (ListInventury listinv = new ListInventury(fileNames))
			{
				listinv.Owner = this;
				if (listinv.ShowDialog() == DialogResult.Cancel)
					return;
				Davkatmp = listinv.Davka;
			}

			if (this.globalObject.controller_inventura2.dataExists())
			{
				if (MessageBoxBig.Show("Tato dávka obsahuje nasnímané položky.\nOpravdu chcete dávku vrátit?",
					"Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
					return;

				//ano chce ji vratit, smazat data???
			}

			odeslatData(false, Davkatmp);
		}


        /// <summary>
        /// Odesle data
        /// </summary>
        /// <param name="zpracovat">True - data se zpracuji. False - data se uvolni ke zpracovani na jinem terminalu</param>
        /// <returns></returns>
        private bool odeslatData(bool zpracovat, string davka)
        {// odeslani dat
            while (true)
            {
                try
                {
                    Program.mstw.mbw.BeginPracujiForm("Odesílají se data inventury");
                    Inventura2Service.ProcessInventuraState processState =
                        zpracovat ?
                        Fask.MST_W.Inventura2Service.ProcessInventuraState.Zpracovat :
                        Fask.MST_W.Inventura2Service.ProcessInventuraState.Uvolnit;

                    try
                    {

						string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_Inventura2);
						string filenameZip = filename + ".zip";
						string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

						Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filenameZip);

						if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Inventura2) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };


						//string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_Inventura2);
						//FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");
						////Fask.MST_W.FileTransfer.Uploading.SendFileWithZip(MST_Global.ServerAddress + "Upload.aspx", filename, MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip");
						//Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Inventura2);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Při odesílání nastaly potíže: " + ex.Message);
                    }
          
                    Program.mstw.mbw.Zprava = "Probíhá zpracování dat";

					Inventura2Service.StatusObject so = this.globalObject.inventuraclassService.ProcessDB2(davka, MST_Global.TerminalID, processState);
                    while (so.StatusText != "OK" || so.Exception)
                    {// nepodarilo se odeslat
                        Program.mstw.mbw.EndPracujiForm();
                        if (MessageBoxBig.Show("Zpracování dat se nezdařilo! " + so.StatusText + "\n Opakovat?", "Chyba", MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
                            == DialogResult.Cancel)
                            return false;
                        Program.mstw.mbw.BeginPracujiForm("Opakuje se zpracování dat");
                    }
                    //else
                    {// odeslani se zdarilo
                        Program.mstw.mbw.EndPracujiForm();
                        File.Delete(Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Inventura2));
                        davka = string.Empty;
                        return true;
                    }
                }
                catch (SoapException soapException)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    Logging.Log.Write(soapException, "Inventura2.odeslatData(zpracovat:" + zpracovat.ToString() + ")");
                    if (MessageBoxBig.Show("Odeslání dat se nezdařilo\n" + soapException.Message + ")! Opakovat?", "Chyba", MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
                        == DialogResult.Cancel)
                        return false;
                }
                catch (WebException webEx)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    Logging.Log.Write(webEx, "Inventura2.odeslatData(zpracovat:" + zpracovat.ToString() + ")");
                    if (MessageBoxBig.Show("Odeslání dat se nezdařilo\n" + webEx.Message + ")! Opakovat?", "Chyba", MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
                        == DialogResult.Cancel)
                        return false;
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    Logging.Log.Write(ex, "Inventura2.odeslatData(zpracovat:" + zpracovat.ToString() + ")");
                    if (MessageBoxBig.Show("Odeslání dat se nezdařilo\n" + ex.Message + ")! Opakovat?", "Chyba", MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
                        == DialogResult.Cancel)
                        return false;
                }
            }
        }

		///// <summary>
		///// Zjisti stav inventury, true=uplna, false=neuplna
		///// </summary>
		///// <returns>True je-li uplna, jinak False</returns>
		//public bool stavInventury()
		//{
		//    //decimal nacist, nacteno;
		//    //foreach (I1 i1row in datai1)
		//    //{
		//    //    nacist = nacteno = 0;
		//    //    nacteno = Nacteno(i1row.ITEMNMBR);
		//    //    if (nacist != nacteno) return false;
		//    //}
		//    //return true;

		//    try
		//    {
		//        Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter ita_q = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter();
		//        //ita_q.Connection = new System.Data.SqlServerCe.SqlCeConnection(
		//        ita_q.Connection = new System.Data.SQLite.SQLiteConnection(
		//                "Data source=" + Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_Inventura2)
		//                );
		//        //int? zbyva = ita_q.ZbyvaPolozek();
		//        //return (zbyva ?? 0) == 0;
		//        int zbyva = (int)ita_q.ZbyvaPolozek();
		//        return zbyva == 0;
		//    }
		//    catch (Exception ex)
		//    {
		//        Logging.Log.Write(ex, "Inventura2.stavInventury");
		//        return false;
		//    }
		//}

        private void PerformCancel()
        {
            if (MST_Global.Inventura2_DialogOpusteniModulu)
            {
                if (MessageBoxBig.Show("Opravdu chcete ukončit práci s modulem?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
            == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }


    }
}