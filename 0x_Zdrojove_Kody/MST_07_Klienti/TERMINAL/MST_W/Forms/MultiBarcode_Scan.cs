using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Forms
{
	public partial class MultiBarcode_Scan : Form
	{
		#region Params

        private List<string> _barcodes = new List<string>();

        public Fask.Parsing.Codes.BaseCode Kod { set; get; }

		#endregion

		#region Eventy Formu + c'tor

		public MultiBarcode_Scan()
		{
			InitializeComponent();
		}

		private void MultiBarcode_Scan_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				PerformCancel();
			}
			else if (e.KeyCode == Keys.Enter)
			{
				PerformOK();
			}
		}

		private void MultiBarcode_Scan_Load(object sender, EventArgs e)
		{
			// nacteni lokalizace ze souboru
			Fask.Localization.LocalizationExtensionForm.Localize(this);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.OnResize(e);
			this.Size = Screen.PrimaryScreen.WorkingArea.Size;
			this.ScannerStart();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
			bStorno.Size = bNew;
		}

		#endregion

		#region Button events

		private void bOK_Click(object sender, EventArgs e)
		{
			try
			{
				ScannerStop();

				if (this.Kod == null)
				{
					ScannerStart();
					MessageBoxBig.Show("Nenalezen objekt kód ke zpracování!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return;
				}

				if (string.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeBarcode)this.Kod).Barcode))
				{
					ScannerStart();
					MessageBoxBig.Show("Nenalezen č. kód ke zpracování!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return;
				}

				DialogResult = DialogResult.OK;

			}
			catch (System.Exception ex)
			{
				MessageBoxBig.Show(ex.Message, "MultiBarcode Scan", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void bStorno_Click(object sender, EventArgs e)
		{
			PerformCancel();
		}

		private void mi_Delete_Click(object sender, EventArgs e)
		{
			this._barcodes.Clear();

			tb_Actual.Text = 
			L_01.Text =
			L_10.Text = 
			L_17.Text = 
			L_21.Text = 
			L_30.Text = string.Empty;

			L_01.ForeColor =
			L_10.ForeColor =
			L_17.ForeColor =
			L_21.ForeColor =
			L_30.ForeColor = Color.Red;

			l01.ForeColor =
			l10.ForeColor =
			l17.ForeColor =
			l21.ForeColor =
			l30.ForeColor = Color.Red;
		}

		#endregion

		#region Private metody

		private void PerformCancel()
		{
			finalize();
			DialogResult = DialogResult.Cancel;
		}

		private void finalize()
		{
			ScannerFinalize();
		}

		private void PerformOK()
		{
			bOK_Click(null, null);
		}

		private void UploadLabels()
		{
			if (this.Kod == null)
				return;

			string Text = "Nasnímaná hodnota {0}:'{1}' neodpovídá původní:'{2}'." + Environment.NewLine + "Nahradit?";

			#region AI 01

			if (!string.IsNullOrEmpty(L_01.Text))
			{

				if (L_01.Text.Trim() != ((Parsing.Codes.Interfaces.ICodeBarcode)this.Kod).Barcode.Trim())
				{
					string txt = string.Format(Text, "Kódu", ((Parsing.Codes.Interfaces.ICodeBarcode)this.Kod).Barcode.Trim(), L_01.Text.Trim());
					DialogResult dr =  MessageBoxBig.Show(txt, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);

					if (dr == DialogResult.Yes)
					{
						L_01.Text = ((Parsing.Codes.Interfaces.ICodeBarcode)this.Kod).Barcode;
					}
				}
			}
			else
			{
				L_01.Text = ((Parsing.Codes.Interfaces.ICodeBarcode)this.Kod).Barcode;
				if (!string.IsNullOrEmpty(L_01.Text))
				{
					L_01.ForeColor = Color.Green;
					l01.ForeColor = Color.Green;
				}
			}

			#endregion

			#region AI 10

			if (!string.IsNullOrEmpty(L_10.Text))
			{
				if (L_10.Text.Trim() != ((Parsing.Codes.Interfaces.ICodeSarze)this.Kod).Sarze.Trim())
				{
					string txt = string.Format(Text, "Lotu", ((Parsing.Codes.Interfaces.ICodeSarze)this.Kod).Sarze.Trim(), L_10.Text.Trim());
					DialogResult dr = MessageBoxBig.Show(txt, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);

					if (dr == DialogResult.Yes)
					{
						L_10.Text = ((Parsing.Codes.Interfaces.ICodeSarze)this.Kod).Sarze;
					}
				}
			}
			else
			{
				L_10.Text = ((Parsing.Codes.Interfaces.ICodeSarze)this.Kod).Sarze;
				if (!string.IsNullOrEmpty(L_10.Text))
				{
					L_10.ForeColor = Color.Green;
					l10.ForeColor = Color.Green;
				}
			} 

			#endregion

			#region AI 17

			string exp = ((Parsing.Codes.Interfaces.ICodeExpiration)this.Kod).Expiration.HasValue ? ((Parsing.Codes.Interfaces.ICodeExpiration)this.Kod).Expiration.Value.ToString("dd.MM yyyy") : string.Empty;
			if (!string.IsNullOrEmpty(L_17.Text))
			{
				if (L_17.Text.Trim() != exp.Trim())
				{
					string txt = string.Format(Text, "Expirace", ((Parsing.Codes.Interfaces.ICodeExpiration)this.Kod).Expiration.Value.ToString("dd.MM yyyy"), L_17.Text.Trim());
					DialogResult dr = MessageBoxBig.Show(txt, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);

					if (dr == DialogResult.Yes)
					{
						L_17.Text = exp;
					}
				}
			}
			else
			{
				L_17.Text = exp;
				if (!string.IsNullOrEmpty(L_17.Text))
				{
					L_17.ForeColor = Color.Green;
					l17.ForeColor = Color.Green;
				}
			} 

			#endregion

			#region AI 21

			if (!string.IsNullOrEmpty(L_21.Text))
			{
				if (L_21.Text.Trim() != ((Parsing.Codes.Interfaces.ICodeSerialNumber)this.Kod).SN.Trim())
				{
					string txt = string.Format(Text, "SN", ((Parsing.Codes.Interfaces.ICodeSerialNumber)this.Kod).SN.Trim(), L_21.Text.Trim());
					DialogResult dr = MessageBoxBig.Show(txt, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);

					if (dr == DialogResult.Yes)
					{
						L_21.Text = ((Parsing.Codes.Interfaces.ICodeSerialNumber)this.Kod).SN;
					}
				}
			}
			else
			{

				try
				{
					if (this.Kod is Parsing.Codes.Interfaces.ICodeSerialNumber)
					{
						L_21.Text = ((Parsing.Codes.Interfaces.ICodeSerialNumber)this.Kod).SN;
						if (!string.IsNullOrEmpty(L_21.Text))
						{
							L_21.ForeColor = Color.Green;
							l21.ForeColor = Color.Green;
						}
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}
			} 

			#endregion

			#region AI 30

			string qty = ((Parsing.Codes.Interfaces.ICodeQuantity)this.Kod).Quantity.HasValue ? ((Parsing.Codes.Interfaces.ICodeQuantity)this.Kod).Quantity.Value.ToString() : string.Empty;
			if (!string.IsNullOrEmpty(L_30.Text))
			{
				if (L_30.Text.Trim() != qty.Trim())
				{
					string txt = string.Format(Text, "Množství", qty, L_30.Text.Trim());
					DialogResult dr = MessageBoxBig.Show(txt, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);

					if (dr == DialogResult.Yes)
					{
						L_30.Text = qty;
					}
				}
			}
			else
			{
				L_30.Text = qty;
				if (!string.IsNullOrEmpty(L_30.Text))
				{
					L_30.ForeColor = Color.Green;
					l30.ForeColor = Color.Green;
				}
			} 
			
			#endregion

		}

		#endregion

		#region Scanner

		private bool restartscanner = true;
		private void ScannerFinalize()
		{
			ScannerStop();
			restartscanner = false;
		}

		private void ScannerStart()
		{
			if (!restartscanner)
				return;

			if (Program.mstw.Scanner != null)
			{
				Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
				Program.mstw.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
				Program.mstw.Scanner.Enable();
			}
		}


		private void ScannerStop()
		{
			if (Program.mstw.Scanner != null)
			{
				Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
				Program.mstw.Scanner.Disable();
			}
		}



		void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
		{
			this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });

		}

		private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

		private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
		{
            ParseBarcode(e.BarcodeData.Trim());
		}

        public void ParseBarcode(string kod)
        {
            try
            {                
                tb_Actual.Text = kod;

                //var codeTmp = Fask.Parsing.ParsingFactory.Parse(kod, Settings.Parsing_Config);
				//if (codeTmp is Parsing.Codes.GS1)
				//{

                if (!this._barcodes.Contains(kod))
                    this._barcodes.Add(kod);

				var code = Fask.Parsing.ParsingFactory.Parse(this._barcodes, Settings.Parsing_Config);
					//if (code is Fask.Parsing.Codes.GS1)
					//    this.Kod = (Fask.Parsing.Codes.GS1)code;
				this.Kod = code;
				//}
				//else
				//{
				//    //MySystem.Audio.PlaySound
				//}

                this.UploadLabels();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

		#endregion



	}
}