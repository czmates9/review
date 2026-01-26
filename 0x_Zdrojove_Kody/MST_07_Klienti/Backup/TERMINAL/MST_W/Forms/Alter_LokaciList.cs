using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Forms
{
    public partial class Alter_LokaciList : Form
    {

		#region Parametry

		private Fask.MST_W._WebRefernces_Globals.LokaceServiceSession wsLokace = null;

		private BindingSource bs;

		private Fask.MST_W.LokaceService.Location ds = new Fask.MST_W.LokaceService.Location();

		public Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow Radek
		{
			get
			{
				try
				{
					return (dataGrid1.BindingContext[dataGrid1.DataSource].Current as DataRowView).Row as Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow;
				}
				catch
				{
					return null;
				}
			}
		}

		private string _itemnmbr = string.Empty;
		public string ITEMNMBR
		{
			//get { return _itemnmbr; }
			set { _itemnmbr = value; }
		}

		private string _serltnum = string.Empty;
		public string SERLTNUM
		{
			//get { return _serltnum; }
			set { _serltnum = value; }
		}

		private string _skl_id = string.Empty;
		public string SKL_ID
		{
			//get { return _skl_id; }
			set { _skl_id = value; }
		}

		private string _note = string.Empty;
		public string NOTE
		{
			//get { return _note; }
			set { _note = value; }
		}

		private decimal _qty = 1;
		public decimal QTY
		{
			//get { return _qty; }
			set { _qty = value; }
		}

		private string _locncode = string.Empty;
		public string LOCNCODE
		{
			//get { return _locncode; }
			set { _locncode = value; }
		}

		#endregion

		#region c'tor

		public Alter_LokaciList()
		{
			InitializeComponent();
		}

		#endregion

		#region Eventy formu

		private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
		{
			try
			{
				// nacteni lokalizace ze souboru
				Fask.Localization.LocalizationExtensionForm.Localize(this);

				this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
				this.Size = Forms.FormLocation.ScreenResolution;

				wsLokace = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
				wsLokace.Url = MST_Global.ServerAddress + "Lokace.asmx";
				wsLokace.Timeout = MST_Global.ServiceTimeOut;
				wsLokace.UpdateWebServiceCredentials();

				CreateGridStyles();

				InitializeGrid();

				ScannerStart();

				PerformUpdate();

				PoznamkaColumnAdd();

				SelectColumnQTY();

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}

		private void Alter_LokaciList_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				ScannerStop();
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void SelectColumnQTY()
		{
			try
			{
				if (ds.CZMST_SkladLokace_Stav == null || ds.CZMST_SkladLokace_Stav.Count == 0)
					return;

				//Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow rowSel = ds.CZMST_SkladLokace_Stav[0];
				Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow rowSel = null;

				dataGrid1.UnSelectAll();

				Dictionary<int, decimal> tmpDick = new Dictionary<int, decimal>();

				foreach (Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow row in ds.CZMST_SkladLokace_Stav)
				{
					if (row.QTYSHPPD >= this._qty)
					{
						if ((rowSel == null) || (row.QTYSHPPD < rowSel.QTYSHPPD))
						{
							rowSel = row;
						}
					}
				}

				if (rowSel != null)
				{
					List<DataRow> dr = new List<DataRow>();
					dr.Add(rowSel);

					dataGrid1.SelectedRowsSet(dr);
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.Log.Write(ex);
			}
		}

		private void PoznamkaColumnAdd()
		{
			try
			{
				if (!string.IsNullOrEmpty(this._note))
				{
					string poznamkaColumnName = "Poznamka";
					var colPoznamka = ds.CZMST_SkladLokace_Stav.Columns.Add(poznamkaColumnName);
					foreach (Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow colP in ds.CZMST_SkladLokace_Stav)
					{
						colP[poznamkaColumnName] = this._note.Trim();
					}

					var dgPoznamka = this.dataGrid1.TableStyles[0].GridColumnStyles[poznamkaColumnName];
					if (dgPoznamka == null)
					{
						var dg = new Fask.Graphic.DataGrid2TextBoxColumn();
						dg.HeaderText = "Poznámka";
						dg.MappingName = poznamkaColumnName;
						dg.NullText = "-";
						dg.Width = 50;
						this.dataGrid1.TableStyles[0].GridColumnStyles.Add(dg);
					} 
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				//PerformCancel();
				DialogResult = DialogResult.OK;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				PerformCancel();
			}
			else if (e.KeyCode == Keys.F1)
			{
				miNajitLokaci_Click(null, null);
			}
			else if (e.KeyCode == Keys.F2)
			{
				miAktualizovat_Click(null, null);
			}
			else
				return;

			e.Handled = true;
		}

		private void finalize()
		{
			ScannerFinalize();

			this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
		}


		#endregion

		#region Inicialize GRID

		private void CreateGridStyles()
		{
			DataGridTableStyle ts = new DataGridTableStyle();
			ts.MappingName = ds.CZMST_SkladLokace_Stav.TableName;

			Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "Položka Č.";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.ITEMNMBRColumn.ColumnName;
			dg.NullText = "-";
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

			dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "Název";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.ITEMDESCColumn.ColumnName;
			dg.NullText = "-";
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

			dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "Lokace";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.LOCNCODEColumn.ColumnName;
			dg.NullText = "-";
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

			dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "šarže/SN";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.SERLTNUMColumn.ColumnName;
			dg.NullText = "-";
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

			dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "Množství";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.QTYSHPPDColumn.ColumnName;
			dg.NullText = "-";
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

			dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "Sklad ID";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.SKL_IDColumn.ColumnName;
			dg.NullText = "-";
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

			dataGrid1.TableStyles.Add(ts);
		}

		private void InitializeGrid()
		{
			this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
			this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
			this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
		}
		
		#endregion

		#region Perform Metody

		private void PerformCancel()
		{
			finalize();
			DialogResult = DialogResult.Cancel;
		}

		private void PerformUpdate()
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				ds = OnlineGetLokace();

				if (ds != null)
				{
					bs = new BindingSource();
					bs.DataSource = ds.CZMST_SkladLokace_Stav;
					bs.Sort = "QTYSHPPD DESC";
					dataGrid1.DataSource = bs;
				}
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);
				MessageBoxBig.Show("Načtení lokací se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}

			try
			{
				dataGrid1.Focus();
				dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
			}
			catch
			{
			}
		}

		#endregion

		#region Eventy menu

		private void miNajitLokaci_Click(object sender, EventArgs e)
		{
			try
			{
				ScannerStop();

				string kod = string.Empty;
				using (SejmiKodForm skf = new SejmiKodForm("Lokace", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
				{
					skf.Text = "Zadejte lokaci";
					DialogResult dr = skf.ShowDialog();

					if (dr != DialogResult.OK)
					{
						ScannerStart();
						return;
					}

					kod = skf.Kod;
				}

				najdipolozku(kod);
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Vydej.Alter_LokaciList, Najit lokaci");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
			finally
			{
				ScannerStart();
			}
		}

		private void miAktualizovat_Click(object sender, EventArgs e)
		{
			try
			{
				PerformUpdate();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Vydej.Alter_LokaciList, Aktualizovat");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
		}

		private void miKonec_Click(object sender, EventArgs e)
		{
			PerformCancel();
		}
		
		#endregion

        #region scanner + najdipolozku

        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

        delegate void DelegateString(string kod);
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                if (kod.Length <= 0)
                    return;

                this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }


        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod">barcode</param>
        private void najdipolozku(string kod)
        {
            try
            {
                bs.Filter = string.Empty;
                string selectcmd = "LOCNCODE = '" + kod + "'";
                var tables = ds.CZMST_SkladLokace_Stav.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show("Lokace '" + kod + "' nebyla nalezena v seznamu.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNalezenoViceLokaci, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private Fask.MST_W.LokaceService.Location OnlineGetLokace()
        {
            Fask.MST_W.LokaceService.Location lokace;
			try
			{
				lokace = wsLokace.ShowMaterial(this._itemnmbr, this._serltnum, _skl_id, null, false);
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Vydej.Alter_LokaciList, OnlineGetPrijmoveLokace");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return null;
			}
            
            return lokace;
        }



    }

}