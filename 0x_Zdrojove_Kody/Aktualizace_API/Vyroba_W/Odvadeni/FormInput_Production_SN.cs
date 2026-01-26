using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Vyroba_W.Forms;
using System.IO;

namespace Fask.Vyroba_W.Odvadeni
{
	public partial class FormInput_Production_SN : Form
	{

		#region Paramtery

		private decimal _qtySN = 0;
		public decimal QTY_SN
		{
			get { return _qtySN; }
		}

		/// <summary>
		/// Lokalni DataTable s Production_SN
		/// </summary>
		private Fask.SQLiteDBs.DataSets.Vyroba.Production_SNDataTable _PSN_Datatable = new Fask.SQLiteDBs.DataSets.Vyroba.Production_SNDataTable();



		private Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow = null;
		/// <summary>
		/// Aktualni vyroba
		/// </summary>
		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow
		{
			set { _productionRow = value; }
		}

		/// <summary>
		/// Vybraný řádek
		/// </summary>
		public Fask.SQLiteDBs.DataSets.Vyroba.Production_SNRow SelectedRow
		{
			get
			{
				try
				{
					return ((DataRowView)this.bs_ProductionSN.Current).Row as Fask.SQLiteDBs.DataSets.Vyroba.Production_SNRow;

				}
				catch
				{
					return null;
				}
			}
			set
			{
				int index = ((DataView)this.bs_ProductionSN.List).Table.Rows.IndexOf(value);
				if (index > 0)
				{
					int i = this.dg_ProductionSN.CurrentRowIndex;
					this.bs_ProductionSN.Position = index;
					this.dg_ProductionSN.UnSelect(i);
					this.dg_ProductionSN.Select(index);
				}
			}
		}

		#endregion

		#region Eventy Formu
		
		public FormInput_Production_SN()
		{
			InitializeComponent();
		}

		private void FormInput_Production_SN_Load(object sender, EventArgs e)
		{
			this.Size = Screen.PrimaryScreen.WorkingArea.Size;
			bs_ProductionSN.DataSource = _PSN_Datatable;
			panelButtons_Resize(null, null);

			ScannerStart();
			UpdateNadpis();
			tb_SN.Focus();
		}


		private void FormInput_Production_SN_KeyDown(object sender, KeyEventArgs e)
		{
			Handle_KeyDown(sender, e);
		}

		private void panelButtons_Resize(object sender, EventArgs e)
		{
			Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
			buttonStorno.Size = nsize;
		}

		#endregion

		#region Button Click
		
		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				PerformOK();
			}
			catch (System.Exception ex)
			{

				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void buttonStorno_Click(object sender, EventArgs e)
		{
			try
			{
				PerformStorno();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void buttonVlozSN_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(tb_SN.Text))
				{
					AddSN(tb_SN.Text.Trim());

					tb_SN.Text = string.Empty;
				}
				else
				{
					MessageBox.Show("Vyplnte hodnotu SN!");
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void mi_Smazat_Click(object sender, EventArgs e)
		{
			try
			{
				PerformDelete();

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		#endregion

		#region Metody

		private void finalize()
		{
			this.ScannerFinalize();
			dg_ProductionSN.Save(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString()));
		}
		
		private void UpdateNadpis()
		{
			try
			{
				_qtySN = _PSN_Datatable.Rows.Count;
				this.Text = string.Format("SN počet: {0}", _qtySN);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}

		}

		private void AddSN(string SN)
		{
			try
			{

				if (_PSN_Datatable.Any(x => x.SERLNMBR == SN))
				{
					MessageBox.Show(
					"SN už existuje: " + SN
					, "O co se snažíš? :D "
					, MessageBoxButtons.OK
					, MessageBoxIcon.Question
					, MessageBoxDefaultButton.Button1
					);

					return;
				}

				var row = _PSN_Datatable.NewProduction_SNRow();

				row.GUID_Production = _productionRow.GUID;
				row.GUID = Guid.NewGuid();
				row.SERLNMBR = SN.Trim();
				row.ITEMNMBR = _productionRow.ITEMNMBR;
				row.QTY = 1;
				row.SetExpiraceNull();
				row.REZ_1 = string.Empty;
				row.REZ_2 = string.Empty;
				row.REZ_3 = string.Empty;
				row.REZ_4 = string.Empty;

				_PSN_Datatable.AddProduction_SNRow(row);

				bs_ProductionSN.DataSource = _PSN_Datatable;
			}
			catch (System.Exception ex)
			{

				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
				UpdateNadpis();
				tb_SN.Focus();
			}
		}

		public void Handle_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Shift && !e.Control && !e.Alt)
			{
				if (e.KeyCode == Keys.Enter)
				{
					PerformOK();
				}
				if (e.KeyCode == Keys.Escape)
				{
					PerformStorno();
				}
				else if (e.KeyCode == Keys.Back)
				{
					PerformDelete();
				}
				else if (e.KeyCode == Keys.F1)
				{
					buttonVlozSN_Click(null, null);
				}
				else
					return;
			}
			else
				return;

			e.Handled = true;
		}

		private void PerformOK()
		{

			if (_PSN_Datatable.Count == 0)
			{
				if (DialogResult.Yes == MessageBox.Show(
					"Nenalezeno žádné nasnimané SN.\n Ukončit?"
					, this.Text
					, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question
					, MessageBoxDefaultButton.Button1
					))
				{
					PerformStorno();
				}
			}
			else
			{
				//V tomto momente se provede insert do Production_SN
				var recordsUpdated = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production_SN(_PSN_Datatable);
				this.finalize();
				DialogResult = DialogResult.OK;
			}
		}

		private void PerformStorno()
		{
			this.finalize();
			DialogResult = DialogResult.Cancel;
		}

		private void PerformDelete()
		{
			var vybranymat = this.SelectedRow;
			if (vybranymat != null)
			{
				if (DialogResult.No == MessageBox.Show(
					"Odstranit ?\n" + "ITEMNMBR: " + vybranymat.ITEMNMBR.Trim() + "\n" + "SN: " + vybranymat.SERLNMBR.ToString()
					, this.Text
					, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question
					, MessageBoxDefaultButton.Button1
					))
					return;

				this._PSN_Datatable.RemoveProduction_SNRow(vybranymat);

				UpdateNadpis();
			}
		}

		#endregion

		#region Scanner

		bool scannerefinalized = false;
		private void ScannerFinalize()
		{
			scannerefinalized = true;
			ScannerStop();
		}

		private void ScannerStart()
		{
			try
			{
				if (scannerefinalized)
					return;

				FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
				FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
				FormMain.Scanner.Enable();
			}
			catch(Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void ScannerStop()
		{
			try
			{
				FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
				FormMain.Scanner.Disable();
			}
			catch(Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void ScannerEventHandlerMethod(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
		{
			try
			{
				if (e.BarcodeData.Trim().Length == 0)
					return;

				AddSN(e.BarcodeData.Trim());
				
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
		{
			this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
		}
		
		#endregion

	}
}