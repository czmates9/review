using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.ScannerProvider;
using Symbol.Barcode;
using System.Xml;
using System.Globalization;
using System.IO;

namespace Fask.ScannerProviderMC3090
{
	public sealed class ScannerProviderMC3090 : Fask.ScannerProvider.IScannerProvider
	{

		private bool _continousRead = false;
		public bool ContinuousRead
		{
			get { return _continousRead; }
			set
			{
				_continousRead = value;
				_reader.Info.SoftTrigger = _continousRead;
				if (_continousRead)
					AimType = AIMTYPE.CONTINUOUS_READ;
				else
					AimType = AIMTYPE.TRIGGER;
			}
		}
		public Symbol.Barcode.Reader _reader = null;
		public Symbol.Barcode.ReaderData _readerData = null;
		private string _configScanner;

		public ScannerProviderMC3090()
		{
			//try
			//{
			//    this.InitializeScanner();
			//}
			//catch
			//{
			//}
		}

		public void SetForm(System.Windows.Forms.Form topLevelForm)
		{
			//tady se nic nedela...
		}

		public void InitializeScanner()
		{
			//base.InitializeScanner();
			try
			{
				Logging.Log.Write("ConfigScanner", "InitializeScanner()");
				_configScanner = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Scanner.xml");

				Logging.Log.Write("_reader != null", "InitializeScanner()");
				// If the scanner is already present, fail to initialize
				if (_reader != null)
					return;

				Logging.Log.Write("_reader = new", "InitializeScanner()");
				// Create a new scanner; use the first available scanner
				_reader = new Symbol.Barcode.Reader();

				Logging.Log.Write("_readerData = new", "InitializeScanner()");
				// Create the scanner data
				_readerData = new Symbol.Barcode.ReaderData(ReaderDataTypes.Text, ReaderDataLengths.MaximumLabel);

				Logging.Log.Write("_reader.ReadNotify += new ", "InitializeScanner()");
				// Create the event handler delegate
				_reader.ReadNotify += new EventHandler(_reader_ReadNotify);



				Logging.Log.Write("_reader.Actions.Enable", "InitializeScanner()");
				// Enable the scanner with a wait cursor
				_reader.Actions.Enable();

				// Set up the scanner
				//_reader.Parameters.Feedback.Success.BeepTime = 0;
				//_reader.Parameters.Feedback.Success.WaveFile = "\\windows\\alarm3.wav";
				//_reader.Parameters.ScanType = ScanTypes.Background;

				Logging.Log.Write("EnableAllBarcodes()", "InitializeScanner()");
				this.EnableAllBarcodes();

				Logging.Log.Write("ScannerSettingLoad", "InitializeScanner()");
				this.ScannerSettingLoad();

				Logging.Log.Write("_reader.StatusNotify += new ", "InitializeScanner()");
				// Create the event handler delegate
				_reader.StatusNotify += new EventHandler(_reader_StatusNotify);

				Logging.Log.Write("return", "InitializeScanner()");
				return;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private Symbol.Barcode.States prevState = States.IDLE;
		private void _reader_StatusNotify(object sender, EventArgs e)
		{
			// Get ReaderData
			Symbol.Barcode.BarcodeStatus TheStatusData = _reader.GetNextStatus();

			switch (TheStatusData.State)
			{
				case Symbol.Barcode.States.WAITING:

					//READY->WAITING = Beam timed out.
					//Need to issue the read again to have the support for continuous scanning.
					//if ((AimType == AIMTYPE.CONTINUOUS_READ) && (prevState == Symbol.Barcode.States.READY))
					if ((ContinuousRead) && (prevState == Symbol.Barcode.States.READY))
					{
						this.Disable();
						this.Enable();
					}
					break;
				default:
					break;
			}
			prevState = TheStatusData.State;
		}
		public void TerminateScanner()
		{
			try
			{
				//base.TerminateScanner();
				// If you have a scanner
				if (_reader != null)
				{
					// Disable the scanner
					_reader.Actions.Disable();

					// Free it up
					_reader.Dispose();

					// Indicate that you no longer have a scanner
					_reader = null;
				}

				// If you have a scanner data object
				if (_readerData != null)
				{
					// Free it up
					_readerData.Dispose();

					// Indicate that you no longer have a scanner
					_readerData = null;
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public event ScannerEventHandler DataReady;

		void _reader_ReadNotify(object sender, EventArgs e)
		{
			try
			{
				ReaderData readerData = _reader.GetNextReaderData();


				if (readerData.Result == Symbol.Results.SUCCESS)
				{
					// Raise the scan event to the caller (with data)
					if (DataReady != null)
					{
						DataReady(sender, new ScannerEventArgs(readerData.Text, GetBarcodeType(readerData.Type), readerData.Type.ToString(), (uint)readerData.Length));
					}

					// Start the next scan
					this.Enable();
					//if (this.AimType == AIMTYPE.CONTINUOUS_READ)
					if (this.ContinuousRead)
					{
						this._reader.Actions.ToggleSoftTrigger();
					}
				}
				else if (readerData.Result == Symbol.Results.E_SCN_READTIMEOUT)
				{
					//if (AimType == AIMTYPE.CONTINUOUS_READ)
					if (ContinuousRead)
					{
						// Start the next scan
						this.Enable();
					}
				}
				//else if (readerData.Result == Symbol.Results.E_SCN_BUFFERTOOSMALL)
				//{
				//    //TaD Tohle zapespečí že scanner neumře...
				//    if (DataReady != null)
				//    {
				//        DataReady(sender, new ScannerEventArgs("Kód přetekl buffer.", BarcodeType.Unknown , "ERR", 0));
				//    }

				//    // Start the next scan
				//    this.Enable();

				//    //if (this.AimType == AIMTYPE.CONTINUOUS_READ)
				//    if (this.ContinuousRead)
				//    {
				//        this._reader.Actions.ToggleSoftTrigger();
				//    }
 
				//}
				else
				{
					;
				}


				//switch (readerData.Result)
				//{
				//    case Symbol.Results.E_SCN_READTIMEOUT:
				//        if (ContinuousRead)
				//        {
				//            // Start the next scan
				//            this.Enable();
				//        }
				//        break;
				//    case Symbol.Results.SUCCESS:
				//        // Raise the scan event to the caller (with data)
				//        if (DataReady != null)
				//        {
				//            DataReady(sender, new ScannerEventArgs(readerData.Text, GetBarcodeType(readerData.Type), readerData.Type.ToString(), (uint)readerData.Length));
				//        }

				//        // Start the next scan
				//        this.Enable();
				//        if (ContinuousRead)
				//        {
				//            // Start the next scan
				//            this.Enable();
				//        }
				//        break;
				//    default:
				//        break;
				//}


				//// If successful, scan
				//if (readerData.Result == Symbol.Results.SUCCESS)
				//{
				//    // Raise the scan event to the caller (with data)
				//    if (DataReady != null)
				//    {
				//        DataReady(sender, new ScannerEventArgs(readerData.Text, GetBarcodeType(readerData.Type), readerData.Type.ToString(), (uint)readerData.Length));
				//    }

				//    // Start the next scan
				//    this.Enable();
				//}

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		private BarcodeType GetBarcodeType(DecoderTypes dType)
		{
			switch (dType)
			{
				case DecoderTypes.EAN128:
					return BarcodeType.EAN128;
				case DecoderTypes.EAN13:
					return BarcodeType.EAN13;
				case DecoderTypes.EAN8:
					return BarcodeType.EAN8;
				case DecoderTypes.CODE128:
					return BarcodeType.CODE128;
				case DecoderTypes.CODE32:
					return BarcodeType.CODE32;
				case DecoderTypes.CODE39:
					return BarcodeType.CODE39;
				case DecoderTypes.CODE93:
					return BarcodeType.CODE93;
				case DecoderTypes.I2OF5:
					return BarcodeType.I2OF5;
				case DecoderTypes.UPCA:
					return BarcodeType.UPCA;

				// TODO : doplnit jine typy???
				default: return BarcodeType.Unknown;
			}
		}

		public void Enable()
		{
			//base.Enable();
			try
			{
				// If you have both a scanner and data
				if ((_reader != null) && (_readerData != null))
				{
					// Submit a scan
					_reader.Actions.Read(_readerData);

					_enabled = true;
				}
			}
			catch (Exception ex)
			{
				string a = ex.Message;
			}
		}

		//public void Enable(bool toggleSoftTrigger)
		//{
		//base.Enable();
		//try
		//{
		// If you have both a scanner and data
		//if ((_reader != null) && (_readerData != null))
		//{
		//if (!_readerData.IsPending)
		//{
		// Submit a scan
		//_reader.Actions.Read(_readerData);

		//if (toggleSoftTrigger && _reader.Info.SoftTrigger == false)
		//{
		//_reader.Info.SoftTrigger = true;
		//}

		//_enabled = true; 
		//}
		//}
		//}
		//catch (Exception ex)
		//{
		//string a = ex.Message;
		//}
		//}

		public void Disable()
		{
			//base.Disable();
			// If you have a scanner
			if (_reader != null)
			{

				//if (_reader.Info.SoftTrigger == true)
				//{
				//_reader.Info.SoftTrigger = false;
				//}

				// Cancel all pending scans
				_reader.Actions.Flush();
			}
			_enabled = false;
		}

		public void EnableAllBarcodes()
		{
			this._reader.Decoders.EnableAll();
		}
		/// <summary>
		/// 
		/// </summary>
		public void BarcodeSetting()
		{
			//throw new Exception("The method or operation is not implemented.");
			// TODO: Scanner Jarda snad je tato reference v nove verzi EMDK 
			//Symbol.StandardForms.GenericUI.Run(this._reader);
			// TODO: Scanner Zakomentovano Jarda v puvodni verzi EMDK 
			Symbol.StandardForms.EnabledDecoders.Run(this._reader);

			this.ScannerSettingSave();
		}

		public void ScannerSetting()
		{
			//throw new Exception("The method or operation is not implemented.");
			// TODO: Scanner zakomentovano Jarda neni reference v nove verzi EMDK 
			Symbol.StandardForms.ScanParamsForm.Run(this._reader);

			this.ScannerSettingSave();
		}

		public void ScannerSettingSave()
		{
			System.Xml.XmlWriter xmlwrite = null;
			try
			{
				xmlwrite = System.Xml.XmlWriter.Create(_configScanner);
				xmlwrite.WriteStartElement("Scanner");

				#region Puvodni


				#region KODY

				#region ALL OK
				//if (this._reader.Decoders.ALL.IsSupported)
				//{
				//    xmlwrite.WriteStartElement("ALL");
				//    xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.ALL.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
				//    xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.ALL.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
				//    xmlwrite.WriteElementString("Enabled", this._reader.Decoders.ALL.Enabled.ToString());
				//    xmlwrite.WriteEndElement(); //ALL
				//}
				#endregion

				#region AUSPOSTAL OK
				if (this._reader.Decoders.AUSPOSTAL.IsSupported)
				{
					xmlwrite.WriteStartElement("AUSPOSTAL");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.AUSPOSTAL.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.AUSPOSTAL.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.AUSPOSTAL.Enabled.ToString());
					xmlwrite.WriteEndElement(); //AUSPOSTAL
				}
				#endregion

				#region AZTEC OK
				if (this._reader.Decoders.AZTEC.IsSupported)
				{
					xmlwrite.WriteStartElement("AZTEC");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.AZTEC.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.AZTEC.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.AZTEC.Enabled.ToString());
					xmlwrite.WriteEndElement(); //AZTEC
				}
				#endregion

				#region CANPOSTAL OK
				if (this._reader.Decoders.CANPOSTAL.IsSupported)
				{
					xmlwrite.WriteStartElement("CANPOSTAL");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CANPOSTAL.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CANPOSTAL.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CANPOSTAL.Enabled.ToString());
					xmlwrite.WriteEndElement(); //CANPOSTAL
				}
				#endregion

				#region CODABAR OK
				if (this._reader.Decoders.CODABAR.IsSupported)
				{
					xmlwrite.WriteStartElement("CODABAR");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CODABAR.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CODABAR.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CODABAR.Enabled.ToString());
					xmlwrite.WriteElementString("ClsiEditing", this._reader.Decoders.CODABAR.ClsiEditing.ToString());
					xmlwrite.WriteElementString("NotisEditing", this._reader.Decoders.CODABAR.NotisEditing.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.CODABAR.Redundancy.ToString());
					xmlwrite.WriteEndElement(); //CODABAR
				}
				#endregion

				#region CODE11 OK
				if (this._reader.Decoders.CODE11.IsSupported)
				{
					xmlwrite.WriteStartElement("CODE11");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CODE11.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CODE11.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CODE11.Enabled.ToString());
					xmlwrite.WriteElementString("CheckDigitCount", this._reader.Decoders.CODE11.CheckDigitCount.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.CODE11.Redundancy.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.CODE11.ReportCheckDigit.ToString());
					xmlwrite.WriteEndElement(); //CODE11
				}
				#endregion

				#region CODE128 OK
				if (this._reader.Decoders.CODE128.IsSupported)
				{
					xmlwrite.WriteStartElement("CODE128");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CODE128.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CODE128.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CODE128.Enabled.ToString());
					xmlwrite.WriteElementString("EAN128", this._reader.Decoders.CODE128.EAN128.ToString());
					xmlwrite.WriteElementString("CheckISBTTable", this._reader.Decoders.CODE128.CheckISBTTable.ToString());
					xmlwrite.WriteElementString("ISBT128", this._reader.Decoders.CODE128.ISBT128.ToString());
					xmlwrite.WriteElementString("ISBT128ConcatMode", this._reader.Decoders.CODE128.ISBT128ConcatMode.ToString());
					xmlwrite.WriteElementString("Other128", this._reader.Decoders.CODE128.Other128.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.CODE128.Redundancy.ToString());
					xmlwrite.WriteElementString("SecurityLevel", this._reader.Decoders.CODE128.SecurityLevel.ToString());
					xmlwrite.WriteEndElement(); //CODE128
				}
				#endregion

				#region CODE39 OK
				if (this._reader.Decoders.CODE39.IsSupported)
				{
					xmlwrite.WriteStartElement("CODE39");
					xmlwrite.WriteElementString("Code32Prefix", this._reader.Decoders.CODE39.Code32Prefix.ToString());
					xmlwrite.WriteElementString("Concatenation", this._reader.Decoders.CODE39.Concatenation.ToString());
					xmlwrite.WriteElementString("ConvertToCode32", this._reader.Decoders.CODE39.ConvertToCode32.ToString());
					xmlwrite.WriteElementString("FullAscii", this._reader.Decoders.CODE39.FullAscii.ToString());
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CODE39.Enabled.ToString());
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CODE39.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CODE39.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.CODE39.Redundancy.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.CODE39.ReportCheckDigit.ToString());
					xmlwrite.WriteElementString("VerifyCheckDigit", this._reader.Decoders.CODE39.VerifyCheckDigit.ToString());
					xmlwrite.WriteEndElement(); //CODE39
				}
				#endregion

				#region CODE93 OK
				if (this._reader.Decoders.CODE93.IsSupported)
				{
					xmlwrite.WriteStartElement("CODE93");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CODE93.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CODE93.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CODE93.Enabled.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.CODE93.Redundancy.ToString());
					xmlwrite.WriteEndElement(); //CODE93
				}
				#endregion

				#region COMPOSITE_AB OK
				if (this._reader.Decoders.COMPOSITE_AB.IsSupported)
				{
					xmlwrite.WriteStartElement("COMPOSITE_AB");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.COMPOSITE_AB.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.COMPOSITE_AB.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.COMPOSITE_AB.Enabled.ToString());
					xmlwrite.WriteElementString("UCCLinkMode", this._reader.Decoders.COMPOSITE_AB.UCCLinkMode.ToString());
					xmlwrite.WriteElementString("UseUPCPreambleCheckDigitRules", this._reader.Decoders.COMPOSITE_AB.UseUPCPreambleCheckDigitRules.ToString());
					xmlwrite.WriteEndElement(); //COMPOSITE_AB
				}
				#endregion

				#region COMPOSITE_C OK
				if (this._reader.Decoders.COMPOSITE_C.IsSupported)
				{
					xmlwrite.WriteStartElement("COMPOSITE_C");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.COMPOSITE_C.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.COMPOSITE_C.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.COMPOSITE_C.Enabled.ToString());
					xmlwrite.WriteEndElement(); //COMPOSITE_C
				}
				#endregion

				#region CUECODE OK
				if (this._reader.Decoders.CUECODE.IsSupported)
				{
					xmlwrite.WriteStartElement("CUECODE");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CUECODE.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CUECODE.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CUECODE.Enabled.ToString());
					xmlwrite.WriteEndElement(); //CUECODE
				}
				#endregion

				#region D2OF5 OK
				if (this._reader.Decoders.D2OF5.IsSupported)
				{
					xmlwrite.WriteStartElement("D2OF5");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.D2OF5.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.D2OF5.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.D2OF5.Enabled.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.D2OF5.Redundancy.ToString());
					xmlwrite.WriteEndElement(); //D2OF5
				}
				#endregion

				#region DATAMATRIX OK
				if (this._reader.Decoders.DATAMATRIX.IsSupported)
				{
					xmlwrite.WriteStartElement("DATAMATRIX");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.DATAMATRIX.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.DATAMATRIX.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.DATAMATRIX.Enabled.ToString());
					xmlwrite.WriteEndElement(); //DATAMATRIX
				}
				#endregion

				#region DEFAULT OK
				//if (this._reader.Decoders.DEFAULT.IsSupported)
				//{
				//    xmlwrite.WriteStartElement("DEFAULT");
				//    xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.DEFAULT.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
				//    xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.DEFAULT.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
				//    xmlwrite.WriteElementString("Enabled", this._reader.Decoders.DEFAULT.Enabled.ToString());
				//    xmlwrite.WriteEndElement(); //DEFAULT
				//}
				#endregion

				#region DUTCHPOSTAL OK
				if (this._reader.Decoders.DUTCHPOSTAL.IsSupported)
				{
					xmlwrite.WriteStartElement("DUTCHPOSTAL");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.DUTCHPOSTAL.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.DUTCHPOSTAL.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.DUTCHPOSTAL.Enabled.ToString());
					xmlwrite.WriteEndElement(); //DUTCHPOSTAL
				}
				#endregion

				#region EAN13 OK
				if (this._reader.Decoders.EAN13.IsSupported)
				{
					xmlwrite.WriteStartElement("EAN13");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.EAN13.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.EAN13.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.EAN13.Enabled.ToString());
					xmlwrite.WriteEndElement(); //EAN13
				}
				#endregion

				#region EAN8 OK
				if (this._reader.Decoders.EAN8.IsSupported)
				{
					xmlwrite.WriteStartElement("EAN8");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.EAN8.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.EAN8.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.EAN8.Enabled.ToString());
					xmlwrite.WriteElementString("ConvertToEAN13", this._reader.Decoders.EAN8.ConvertToEAN13.ToString());
					xmlwrite.WriteEndElement(); //EAN8
				}
				#endregion

				#region CHINESE_2OF5 OK
				if (this._reader.Decoders.CHINESE_2OF5.IsSupported)
				{
					xmlwrite.WriteStartElement("CHINESE_2OF5");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.CHINESE_2OF5.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.CHINESE_2OF5.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.CHINESE_2OF5.Enabled.ToString());
					xmlwrite.WriteEndElement(); //CHINESE_2OF5
				}
				#endregion

				#region I2OF5 OK
				if (this._reader.Decoders.I2OF5.IsSupported)
				{
					xmlwrite.WriteStartElement("I2OF5");
					xmlwrite.WriteElementString("ConvertToEAN13", this._reader.Decoders.I2OF5.ConvertToEAN13.ToString());
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.I2OF5.Enabled.ToString());
					xmlwrite.WriteElementString("CheckDigitScheme", this._reader.Decoders.I2OF5.CheckDigitScheme.ToString());
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.I2OF5.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.I2OF5.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.I2OF5.Redundancy.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.I2OF5.ReportCheckDigit.ToString());
					xmlwrite.WriteEndElement(); //I2OF5
				}
				#endregion

				#region JAPPOSTAL
				if (this._reader.Decoders.JAPPOSTAL.IsSupported)
				{
					xmlwrite.WriteStartElement("JAPPOSTAL");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.JAPPOSTAL.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.JAPPOSTAL.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.JAPPOSTAL.Enabled.ToString());
					xmlwrite.WriteEndElement(); //JAPPOSTAL
				}
				#endregion

				#region KOREAN_3OF5 OK
				if (this._reader.Decoders.KOREAN_3OF5.IsSupported)
				{
					xmlwrite.WriteStartElement("KOREAN_3OF5");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.KOREAN_3OF5.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.KOREAN_3OF5.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.KOREAN_3OF5.Enabled.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.KOREAN_3OF5.Redundancy.ToString());
					xmlwrite.WriteEndElement(); //KOREAN_3OF5
				}
				#endregion

				#region MACROMICROPDF OK
				if (this._reader.Decoders.MACROMICROPDF.IsSupported)
				{
					xmlwrite.WriteStartElement("MACROMICROPDF");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MACROMICROPDF.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MACROMICROPDF.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MACROMICROPDF.Enabled.ToString());
					xmlwrite.WriteElementString("BufferLabels", this._reader.Decoders.MACROMICROPDF.BufferLabels.ToString());
					xmlwrite.WriteElementString("ConvertToPDF417", this._reader.Decoders.MACROMICROPDF.ConvertToPDF417.ToString());
					xmlwrite.WriteElementString("Exclusive", this._reader.Decoders.MACROMICROPDF.Exclusive.ToString());
					xmlwrite.WriteElementString("ReportAppendInfo", this._reader.Decoders.MACROMICROPDF.ReportAppendInfo.ToString());
					xmlwrite.WriteEndElement(); //MACROMICROPDF

				}
				#endregion

				#region MACROPDF OK
				if (this._reader.Decoders.MACROPDF.IsSupported)
				{
					xmlwrite.WriteStartElement("MACROPDF");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MACROPDF.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MACROPDF.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MACROPDF.Enabled.ToString());
					xmlwrite.WriteElementString("BufferLabels", this._reader.Decoders.MACROPDF.BufferLabels.ToString());
					xmlwrite.WriteElementString("ConvertToPDF417", this._reader.Decoders.MACROPDF.ConvertToPDF417.ToString());
					xmlwrite.WriteElementString("Exclusive", this._reader.Decoders.MACROPDF.Exclusive.ToString());
					xmlwrite.WriteElementString("ReportAppendInfo", this._reader.Decoders.MACROPDF.ReportAppendInfo.ToString());
					xmlwrite.WriteEndElement(); //MACROPDF
				}
				#endregion

				#region MATRIX_2OF5 OK
				if (this._reader.Decoders.MATRIX_2OF5.IsSupported)
				{
					xmlwrite.WriteStartElement("MATRIX_2OF5");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MATRIX_2OF5.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MATRIX_2OF5.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MATRIX_2OF5.Enabled.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.MATRIX_2OF5.ReportCheckDigit.ToString());
					xmlwrite.WriteElementString("VerifyCheckDigit", this._reader.Decoders.MATRIX_2OF5.VerifyCheckDigit.ToString());
					xmlwrite.WriteEndElement(); //MATRIX_2OF5
				}
				#endregion

				#region MAXICODE OK
				if (this._reader.Decoders.MAXICODE.IsSupported)
				{
					xmlwrite.WriteStartElement("MAXICODE");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MAXICODE.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MAXICODE.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MAXICODE.Enabled.ToString());
					xmlwrite.WriteEndElement(); //MAXICODE
				}
				#endregion

				#region MICROPDF OK
				if (this._reader.Decoders.MICROPDF.IsSupported)
				{
					xmlwrite.WriteStartElement("MICROPDF");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MICROPDF.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MICROPDF.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MICROPDF.Enabled.ToString());
					xmlwrite.WriteEndElement(); //MICROPDF
				}
				#endregion

				#region MICROQR OK
				if (this._reader.Decoders.MICROQR.IsSupported)
				{
					xmlwrite.WriteStartElement("MICROQR");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MICROQR.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MICROQR.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MICROQR.Enabled.ToString());
					xmlwrite.WriteEndElement(); //MICROQR
				}
				#endregion

				#region MSI OK
				if (this._reader.Decoders.MSI.IsSupported)
				{
					xmlwrite.WriteStartElement("MSI");
					xmlwrite.WriteElementString("CheckDigitCount", this._reader.Decoders.MSI.CheckDigitCount.ToString());
					xmlwrite.WriteElementString("CheckDigitScheme", this._reader.Decoders.MSI.CheckDigitScheme.ToString());
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.MSI.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.MSI.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.MSI.Redundancy.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.MSI.ReportCheckDigit.ToString());
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.MSI.Enabled.ToString());
					xmlwrite.WriteEndElement(); //MSI
				}
				#endregion

				#region PDF417 OK
				if (this._reader.Decoders.PDF417.IsSupported)
				{
					xmlwrite.WriteStartElement("PDF417");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.PDF417.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.PDF417.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.PDF417.Enabled.ToString());
					xmlwrite.WriteEndElement(); //PDF417
				}
				#endregion

				#region POINTER OK
				if (this._reader.Decoders.POINTER.IsSupported)
				{
					xmlwrite.WriteStartElement("POINTER");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.POINTER.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.POINTER.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.POINTER.Enabled.ToString());
					xmlwrite.WriteEndElement(); //POINTER
				}
				#endregion

				#region QRCODE OK
				if (this._reader.Decoders.QRCODE.IsSupported)
				{
					xmlwrite.WriteStartElement("QRCODE");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.QRCODE.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.QRCODE.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.QRCODE.Enabled.ToString());
					xmlwrite.WriteEndElement(); //QRCODE
				}
				#endregion

				#region RSS14 OK
				if (this._reader.Decoders.RSS14.IsSupported)
				{
					xmlwrite.WriteStartElement("RSS14");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.RSS14.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.RSS14.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.RSS14.Enabled.ToString());
					xmlwrite.WriteEndElement(); //RSS14
				}
				#endregion

				#region RSSEXP OK
				if (this._reader.Decoders.RSSEXP.IsSupported)
				{
					xmlwrite.WriteStartElement("RSSEXP");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.RSSEXP.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.RSSEXP.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.RSSEXP.Enabled.ToString());
					xmlwrite.WriteEndElement(); //RSSEXP
				}
				#endregion

				#region RSSLIM OK
				if (this._reader.Decoders.RSSLIM.IsSupported)
				{
					xmlwrite.WriteStartElement("RSSLIM");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.RSSLIM.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.RSSLIM.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.RSSLIM.Enabled.ToString());
					xmlwrite.WriteEndElement(); //RSSLIM
				}
				#endregion

				#region SIGNATURE OK
				if (this._reader.Decoders.SIGNATURE.IsSupported)
				{
					xmlwrite.WriteStartElement("SIGNATURE");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.SIGNATURE.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.SIGNATURE.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.SIGNATURE.Enabled.ToString());
					xmlwrite.WriteElementString("ImageFormat", this._reader.Decoders.SIGNATURE.ImageFormat.ToString());
					xmlwrite.WriteElementString("ImageHeight", this._reader.Decoders.SIGNATURE.ImageHeight.ToString());
					xmlwrite.WriteElementString("ImageQuality", this._reader.Decoders.SIGNATURE.ImageQuality.ToString());
					xmlwrite.WriteElementString("ImageSize", this._reader.Decoders.SIGNATURE.ImageSize.ToString());
					xmlwrite.WriteElementString("ImageWidth", this._reader.Decoders.SIGNATURE.ImageWidth.ToString());
					xmlwrite.WriteEndElement(); //SIGNATURE
				}
				#endregion

				#region TLC39 OK
				if (this._reader.Decoders.TLC39.IsSupported)
				{
					xmlwrite.WriteStartElement("TLC39");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.TLC39.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.TLC39.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.TLC39.Enabled.ToString());
					xmlwrite.WriteEndElement(); //TLC39
				}
				#endregion

				#region TRIOPTIC39 OK
				if (this._reader.Decoders.TRIOPTIC39.IsSupported)
				{
					xmlwrite.WriteStartElement("TRIOPTIC39");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.TRIOPTIC39.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.TRIOPTIC39.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.TRIOPTIC39.Enabled.ToString());
					xmlwrite.WriteElementString("Redundancy", this._reader.Decoders.TRIOPTIC39.Redundancy.ToString());
					xmlwrite.WriteEndElement(); //TRIOPTIC39
				}
				#endregion

				#region UKPOSTAL OK
				if (this._reader.Decoders.UKPOSTAL.IsSupported)
				{
					xmlwrite.WriteStartElement("UKPOSTAL");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.UKPOSTAL.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.UKPOSTAL.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.UKPOSTAL.Enabled.ToString());
					xmlwrite.WriteEndElement(); //UKPOSTAL
				}
				#endregion

				#region UPCA OK
				if (this._reader.Decoders.UPCA.IsSupported)
				{
					xmlwrite.WriteStartElement("UPCA");
					//Taty parametry sou  read only
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.UPCA.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.UPCA.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.UPCA.ReportCheckDigit.ToString());
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.UPCA.Enabled.ToString());
					xmlwrite.WriteElementString("Preamble", this._reader.Decoders.UPCA.Preamble.ToString());
					xmlwrite.WriteEndElement(); //MSI
				}
				#endregion

				#region UPCE0 OK
				if (this._reader.Decoders.UPCE0.IsSupported)
				{
					xmlwrite.WriteStartElement("UPCE0");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.UPCE0.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.UPCE0.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.UPCE0.Enabled.ToString());
					xmlwrite.WriteElementString("ConvertToUPCA", this._reader.Decoders.UPCE0.ConvertToUPCA.ToString());
					xmlwrite.WriteElementString("Preamble", this._reader.Decoders.UPCE0.Preamble.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.UPCE0.ReportCheckDigit.ToString());
					xmlwrite.WriteEndElement(); //UPCE0
				}
				#endregion

				#region UPCE1 OK
				if (this._reader.Decoders.UPCE1.IsSupported)
				{
					xmlwrite.WriteStartElement("UPCE1");
					//xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.UPCE1.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					//xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.UPCE1.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.UPCE1.Enabled.ToString());
					xmlwrite.WriteElementString("ConvertToUPCA", this._reader.Decoders.UPCE1.ConvertToUPCA.ToString());
					xmlwrite.WriteElementString("Preamble", this._reader.Decoders.UPCE1.Preamble.ToString());
					xmlwrite.WriteElementString("ReportCheckDigit", this._reader.Decoders.UPCE1.ReportCheckDigit.ToString());
					xmlwrite.WriteEndElement(); //UPCE1
				}
				#endregion

				#region US4STATE OK
				if (this._reader.Decoders.US4STATE.IsSupported)
				{
					xmlwrite.WriteStartElement("US4STATE");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.US4STATE.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.US4STATE.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.US4STATE.Enabled.ToString());
					xmlwrite.WriteEndElement(); //US4STATE
				}
				#endregion

				#region US4STATE_FICS OK
				if (this._reader.Decoders.US4STATE_FICS.IsSupported)
				{
					xmlwrite.WriteStartElement("US4STATE_FICS");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.US4STATE_FICS.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.US4STATE_FICS.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.US4STATE_FICS.Enabled.ToString());
					xmlwrite.WriteEndElement(); //US4STATE_FICS
				}
				#endregion

				#region USPLANET OK
				if (this._reader.Decoders.USPLANET.IsSupported)
				{
					xmlwrite.WriteStartElement("USPLANET");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.USPLANET.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.USPLANET.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.USPLANET.Enabled.ToString());
					xmlwrite.WriteEndElement(); //USPLANET
				}
				#endregion

				#region USPOSTNET OK
				if (this._reader.Decoders.USPOSTNET.IsSupported)
				{
					xmlwrite.WriteStartElement("USPOSTNET");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.USPOSTNET.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.USPOSTNET.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.USPOSTNET.Enabled.ToString());
					xmlwrite.WriteEndElement(); //USPOSTNET
				}
				#endregion

				#region WEBCODE OK
				if (this._reader.Decoders.WEBCODE.IsSupported)
				{
					xmlwrite.WriteStartElement("WEBCODE");
					xmlwrite.WriteElementString("MaximumLength", this._reader.Decoders.WEBCODE.MaximumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("MinimumLength", this._reader.Decoders.WEBCODE.MinimumLength.ToString(NumberFormatInfo.InvariantInfo));
					xmlwrite.WriteElementString("Enabled", this._reader.Decoders.WEBCODE.Enabled.ToString());
					xmlwrite.WriteElementString("GTWebcode", this._reader.Decoders.WEBCODE.GTWebcode.ToString());
					xmlwrite.WriteEndElement(); //WEBCODE
				}
				#endregion

				#endregion


				#endregion

				xmlwrite.WriteEndElement(); //Scanner
				xmlwrite.Close();
				xmlwrite = null;
			}
			catch (Exception ex)
			{
				Fask.Logging.Log.Write(ex);
				//Log.Write(ex.Message + ex.StackTrace, Fask.ScannerProvider.ScannerTypes.Symbol_MC3000.ToString() + " : ScannerSettingsSave");
				//mesagg.Show(ex.Message, Fask.ScannerProvider.ScannerTypes.Symbol_MC3000.ToString(), System.Windows.Forms.MessageBoxButtons.OK, MsgBoxIcon.Critical, MessageBoxDefaultButton.Button1);
			}
		}

		public void ScannerSettingLoad()
		{
			if (!System.IO.File.Exists(_configScanner))
				return;

			try
			{
				System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

				xmldoc.Load(_configScanner);


				#region KODY

				XmlNode node;

				#region ALL OK NOT
				//node = xmldoc.SelectSingleNode("Scanner/ALL");
				//if (node != null && this._reader.Decoders.ALL.IsSupported)
				//{
				//    foreach (XmlNode chnode in node.ChildNodes)
				//    {
				//        switch (chnode.LocalName)
				//        {
				//            case "Enabled": this._reader.Decoders.SetEnabled( DecoderTypes., bool.Parse(chnode.InnerText)); break;
				//            //case "MaximumLength": this._reader.Decoders.ALL.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
				//            //case "MinimumLength": this._reader.Decoders.ALL.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
				//            default: break;
				//        }
				//    }
				//} 
				#endregion

				#region AUSPOSTAL OK OK
				node = xmldoc.SelectSingleNode("Scanner/AUSPOSTAL");
				if (node != null && this._reader.Decoders.AUSPOSTAL.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.AUSPOSTAL, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.AUSPOSTAL.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.AUSPOSTAL.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region AZTEC OK OK
				node = xmldoc.SelectSingleNode("Scanner/AZTEC");
				if (node != null && this._reader.Decoders.AZTEC.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.AZTEC, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.AZTEC.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.AZTEC.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region CANPOSTAL OK OK
				node = xmldoc.SelectSingleNode("Scanner/CANPOSTAL");
				if (node != null && this._reader.Decoders.CANPOSTAL.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CANPOSTAL, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CANPOSTAL.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CANPOSTAL.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region CODABAR OK OK

				node = xmldoc.SelectSingleNode("Scanner/CODABAR");
				if (node != null && this._reader.Decoders.CODABAR.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CODABAR, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CODABAR.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CODABAR.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ClsiEditing": this._reader.Decoders.CODABAR.ClsiEditing = bool.Parse(chnode.InnerText); break;
							case "NotisEditing": this._reader.Decoders.CODABAR.NotisEditing = bool.Parse(chnode.InnerText); break;
							case "Redundancy": this._reader.Decoders.CODABAR.Redundancy = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region CODE11 OK OK

				node = xmldoc.SelectSingleNode("Scanner/CODE11");
				if (node != null && this._reader.Decoders.CODE11.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CODE11, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CODE11.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CODE11.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.CODE11.Redundancy = bool.Parse(chnode.InnerText); break;
							case "ReportCheckDigit": this._reader.Decoders.CODE11.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							case "CheckDigitCount": this._reader.Decoders.CODE11.CheckDigitCount = (CODE11.CheckDigitCounts)Enum.Parse(typeof(CODE11.CheckDigitCounts), chnode.InnerText, false); break;

							default: break;
						}
					}
				}

				#endregion

				#region CODE128 OK OK

				node = xmldoc.SelectSingleNode("Scanner/CODE128");
				if (node != null && this._reader.Decoders.CODE128.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CODE128, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CODE128.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CODE128.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "EAN128": this._reader.Decoders.CODE128.EAN128 = bool.Parse(chnode.InnerText); break;
							case "ISBT128": this._reader.Decoders.CODE128.ISBT128 = bool.Parse(chnode.InnerText); break;
							case "Other128": this._reader.Decoders.CODE128.Other128 = bool.Parse(chnode.InnerText); break;
							case "Redundancy": this._reader.Decoders.CODE128.Redundancy = bool.Parse(chnode.InnerText); break;
							case "CheckISBTTable": this._reader.Decoders.CODE128.CheckISBTTable = bool.Parse(chnode.InnerText); break;
							case "ISBT128ConcatMode": this._reader.Decoders.CODE128.ISBT128ConcatMode = (ISBT128_CONCAT_MODE)Enum.Parse(typeof(ISBT128_CONCAT_MODE), chnode.InnerText, false); break;
							case "SecurityLevel": this._reader.Decoders.CODE128.SecurityLevel = (CODE128.SECURITYLEVEL)Enum.Parse(typeof(CODE128.SECURITYLEVEL), chnode.InnerText, false); break;


							default: break;
						}
					}
				}
				#endregion

				#region CODE39 OK OK
				node = xmldoc.SelectSingleNode("Scanner/CODE39");
				if (node != null && this._reader.Decoders.CODE39.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Code32Prefix": this._reader.Decoders.CODE39.Code32Prefix = bool.Parse(chnode.InnerText); break;
							case "Concatenation": this._reader.Decoders.CODE39.Concatenation = bool.Parse(chnode.InnerText); break;
							case "ConvertToCode32": this._reader.Decoders.CODE39.ConvertToCode32 = bool.Parse(chnode.InnerText); break;
							case "FullAscii": this._reader.Decoders.CODE39.FullAscii = bool.Parse(chnode.InnerText); break;
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CODE39, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CODE39.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CODE39.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.CODE39.Redundancy = bool.Parse(chnode.InnerText); break;
							case "ReportCheckDigit": this._reader.Decoders.CODE39.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							case "VerifyCheckDigit": this._reader.Decoders.CODE39.VerifyCheckDigit = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}

				node = xmldoc.SelectSingleNode("Scanner/AZTEC");
				if (node != null && this._reader.Decoders.AZTEC.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.AZTEC, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.AZTEC.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.AZTEC.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region CODE93 OK OK
				node = xmldoc.SelectSingleNode("Scanner/CODE93");
				if (node != null && this._reader.Decoders.CODE93.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CODE93, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CODE93.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CODE93.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.CODE93.Redundancy = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region COMPOSITE_AB OK OK

				node = xmldoc.SelectSingleNode("Scanner/COMPOSITE_AB");
				if (node != null && this._reader.Decoders.COMPOSITE_AB.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.COMPOSITE_AB, bool.Parse(chnode.InnerText)); break;
							case "UCCLinkMode": this._reader.Decoders.COMPOSITE_AB.UCCLinkMode = (COMPOSITE_AB.UCCLinkMode)Enum.Parse(typeof(COMPOSITE_AB.UCCLinkMode), chnode.InnerText, false); break;
							case "UseUPCPreambleCheckDigitRules": this._reader.Decoders.COMPOSITE_AB.UseUPCPreambleCheckDigitRules = (DisabledEnabled)Enum.Parse(typeof(DisabledEnabled), chnode.InnerText, false); break;

							default: break;
						}
					}
				}
				#endregion

				#region COMPOSITE_C OK OK
				node = xmldoc.SelectSingleNode("Scanner/COMPOSITE_C");
				if (node != null && this._reader.Decoders.COMPOSITE_C.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.COMPOSITE_C, bool.Parse(chnode.InnerText)); break;
							//case "MaximumLength": this._reader.Decoders.COMPOSITE_C.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							//case "MinimumLength": this._reader.Decoders.COMPOSITE_C.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region CUECODE OK OK
				node = xmldoc.SelectSingleNode("Scanner/CUECODE");
				if (node != null && this._reader.Decoders.CUECODE.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CUECODE, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CUECODE.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CUECODE.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region D2OF5 OK OK
				node = xmldoc.SelectSingleNode("Scanner/D2OF5");
				if (node != null && this._reader.Decoders.D2OF5.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.D2OF5, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.D2OF5.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.D2OF5.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.D2OF5.Redundancy = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region DATAMATRIX OK OK
				node = xmldoc.SelectSingleNode("Scanner/DATAMATRIX");
				if (node != null && this._reader.Decoders.DATAMATRIX.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.DATAMATRIX, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.DATAMATRIX.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.DATAMATRIX.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region DEFAULT OK NOT
				//node = xmldoc.SelectSingleNode("Scanner/DEFAULT");
				//if (node != null && this._reader.Decoders.AUSPOSTAL.IsSupported)
				//{
				//    foreach (XmlNode chnode in node.ChildNodes)
				//    {
				//        switch (chnode.LocalName)
				//        {
				//            case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.DEFAULT, bool.Parse(chnode.InnerText)); break;
				//            case "MaximumLength": this._reader.Decoders.DEFAULT.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
				//            case "MinimumLength": this._reader.Decoders.DEFAULT.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
				//            default: break;
				//        }
				//    }
				//} 
				#endregion

				#region DUTCHPOSTAL OK OK
				node = xmldoc.SelectSingleNode("Scanner/DUTCHPOSTAL");
				if (node != null && this._reader.Decoders.DUTCHPOSTAL.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.DUTCHPOSTAL, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.DUTCHPOSTAL.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.DUTCHPOSTAL.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region EAN13 OK OK
				node = xmldoc.SelectSingleNode("Scanner/EAN13");
				if (node != null && this._reader.Decoders.EAN13.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.EAN13, bool.Parse(chnode.InnerText)); break;
							//case "MaximumLength": this._reader.Decoders.EAN13.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							//case "MinimumLength": this._reader.Decoders.EAN13.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region EAN8 OK OK
				node = xmldoc.SelectSingleNode("Scanner/EAN8");
				if (node != null && this._reader.Decoders.EAN8.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.EAN8, bool.Parse(chnode.InnerText)); break;
							//case "MaximumLength": this._reader.Decoders.EAN8.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							//case "MinimumLength": this._reader.Decoders.EAN8.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ConvertToEAN13": this._reader.Decoders.EAN8.ConvertToEAN13 = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region CHINESE_2OF5 OK OK
				node = xmldoc.SelectSingleNode("Scanner/CHINESE_2OF5");
				if (node != null && this._reader.Decoders.CHINESE_2OF5.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.CHINESE_2OF5, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.CHINESE_2OF5.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.CHINESE_2OF5.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region I2OF5 OK OK
				node = xmldoc.SelectSingleNode("Scanner/I2OF5");
				if (node != null && this._reader.Decoders.I2OF5.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "ConvertToEAN13": this._reader.Decoders.I2OF5.ConvertToEAN13 = bool.Parse(chnode.InnerText); break;
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.I2OF5, bool.Parse(chnode.InnerText)); break;
							case "CheckDigitScheme": this._reader.Decoders.I2OF5.CheckDigitScheme = (I2OF5.CheckDigitSchemes)Enum.Parse(typeof(I2OF5.CheckDigitSchemes), chnode.InnerText, true); break;
							case "MaximumLength": this._reader.Decoders.I2OF5.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.I2OF5.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.I2OF5.Redundancy = bool.Parse(chnode.InnerText); break;
							case "ReportCheckDigit": this._reader.Decoders.I2OF5.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region JAPPOSTAL OK OK
				node = xmldoc.SelectSingleNode("Scanner/JAPPOSTAL");
				if (node != null && this._reader.Decoders.JAPPOSTAL.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.JAPPOSTAL, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.JAPPOSTAL.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.JAPPOSTAL.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region KOREAN_3OF5 OK OK
				node = xmldoc.SelectSingleNode("Scanner/KOREAN_3OF5");
				if (node != null && this._reader.Decoders.KOREAN_3OF5.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.KOREAN_3OF5, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.KOREAN_3OF5.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.KOREAN_3OF5.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.KOREAN_3OF5.Redundancy = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region MACROMICROPDF OK

				node = xmldoc.SelectSingleNode("Scanner/MACROMICROPDF");
				if (node != null && this._reader.Decoders.MACROMICROPDF.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MACROMICROPDF, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.MACROMICROPDF.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MACROMICROPDF.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "BufferLabels": this._reader.Decoders.MACROMICROPDF.BufferLabels = bool.Parse(chnode.InnerText); break;
							case "ConvertToPDF417": this._reader.Decoders.MACROMICROPDF.ConvertToPDF417 = bool.Parse(chnode.InnerText); break;
							case "Exclusive": this._reader.Decoders.MACROMICROPDF.Exclusive = bool.Parse(chnode.InnerText); break;
							case "ReportAppendInfo": this._reader.Decoders.MACROMICROPDF.ReportAppendInfo = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region MACROPDF OK OK

				node = xmldoc.SelectSingleNode("Scanner/MACROPDF");
				if (node != null && this._reader.Decoders.MACROPDF.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MACROPDF, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.MACROPDF.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MACROPDF.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "BufferLabels": this._reader.Decoders.MACROPDF.BufferLabels = bool.Parse(chnode.InnerText); break;
							case "ConvertToPDF417": this._reader.Decoders.MACROPDF.ConvertToPDF417 = bool.Parse(chnode.InnerText); break;
							case "Exclusive": this._reader.Decoders.MACROPDF.Exclusive = bool.Parse(chnode.InnerText); break;
							case "ReportAppendInfo": this._reader.Decoders.MACROPDF.ReportAppendInfo = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region MATRIX_2OF5 OK OK

				node = xmldoc.SelectSingleNode("Scanner/MATRIX_2OF5");
				if (node != null && this._reader.Decoders.MATRIX_2OF5.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MATRIX_2OF5, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.MATRIX_2OF5.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MATRIX_2OF5.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ReportCheckDigit": this._reader.Decoders.MATRIX_2OF5.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							case "VerifyCheckDigit": this._reader.Decoders.MATRIX_2OF5.VerifyCheckDigit = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region MAXICODE OK OK
				node = xmldoc.SelectSingleNode("Scanner/MAXICODE");
				if (node != null && this._reader.Decoders.MAXICODE.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MAXICODE, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.MAXICODE.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MAXICODE.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region MICROPDF OK OK
				node = xmldoc.SelectSingleNode("Scanner/MICROPDF");
				if (node != null && this._reader.Decoders.MICROPDF.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MICROPDF, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.MICROPDF.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MICROPDF.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region MICROQR OK OK
				node = xmldoc.SelectSingleNode("Scanner/MICROQR");
				if (node != null && this._reader.Decoders.MICROQR.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MICROQR, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.MICROQR.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MICROQR.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region MSI OK OK
				node = xmldoc.SelectSingleNode("Scanner/MSI");
				if (node != null && this._reader.Decoders.MSI.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.MSI, bool.Parse(chnode.InnerText)); break;
							case "CheckDigitCounts": this._reader.Decoders.MSI.CheckDigitCount = (MSI.CheckDigitCounts)Enum.Parse(typeof(MSI.CheckDigitCounts), chnode.InnerText, true); break;
							case "CheckDigitSchemes": this._reader.Decoders.MSI.CheckDigitScheme = (MSI.CheckDigitSchemes)Enum.Parse(typeof(MSI.CheckDigitSchemes), chnode.InnerText, true); break;
							case "MaximumLength": this._reader.Decoders.MSI.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.MSI.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.MSI.Redundancy = bool.Parse(chnode.InnerText); break;
							case "ReportCheckDigit": this._reader.Decoders.MSI.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region PDF417 OK OK
				node = xmldoc.SelectSingleNode("Scanner/PDF417");
				if (node != null && this._reader.Decoders.PDF417.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.PDF417, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.PDF417.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.PDF417.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region POINTER OK OK
				node = xmldoc.SelectSingleNode("Scanner/POINTER");
				if (node != null && this._reader.Decoders.POINTER.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.POINTER, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.POINTER.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.POINTER.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region QRCODE OK OK
				node = xmldoc.SelectSingleNode("Scanner/QRCODE");
				if (node != null && this._reader.Decoders.QRCODE.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.QRCODE, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.QRCODE.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.QRCODE.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region RSS14 OK OK
				node = xmldoc.SelectSingleNode("Scanner/RSS14");
				if (node != null && this._reader.Decoders.RSS14.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.RSS14, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.RSS14.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.RSS14.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region RSSEXP OK OK
				node = xmldoc.SelectSingleNode("Scanner/RSSEXP");
				if (node != null && this._reader.Decoders.RSSEXP.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.RSSEXP, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.RSSEXP.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.RSSEXP.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region RSSLIM OK OK
				node = xmldoc.SelectSingleNode("Scanner/RSSLIM");
				if (node != null && this._reader.Decoders.RSSLIM.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.RSSLIM, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.RSSLIM.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.RSSLIM.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region SIGNATURE OK OK

				node = xmldoc.SelectSingleNode("Scanner/SIGNATURE");
				if (node != null && this._reader.Decoders.SIGNATURE.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.SIGNATURE, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.SIGNATURE.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.SIGNATURE.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ImageFormat": this._reader.Decoders.SIGNATURE.ImageFormat = (SIGNATURE.ImageFormat)Enum.Parse(typeof(SIGNATURE.ImageFormat), chnode.InnerText, false); break;
							case "ImageHeight": this._reader.Decoders.SIGNATURE.ImageHeight = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ImageQuality": this._reader.Decoders.SIGNATURE.ImageQuality = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ImageSize": this._reader.Decoders.SIGNATURE.ImageSize = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "ImageWidth": this._reader.Decoders.SIGNATURE.ImageWidth = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region TLC39 OK OK
				node = xmldoc.SelectSingleNode("Scanner/TLC39");
				if (node != null && this._reader.Decoders.TLC39.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.TLC39, bool.Parse(chnode.InnerText)); break;
							//case "MaximumLength": this._reader.Decoders.TLC39.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							//case "MinimumLength": this._reader.Decoders.TLC39.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region TRIOPTIC39 OK OK
				node = xmldoc.SelectSingleNode("Scanner/TRIOPTIC39");
				if (node != null && this._reader.Decoders.TRIOPTIC39.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.TRIOPTIC39, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.TRIOPTIC39.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.TRIOPTIC39.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "Redundancy": this._reader.Decoders.TRIOPTIC39.Redundancy = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region UKPOSTAL OK OK
				node = xmldoc.SelectSingleNode("Scanner/UKPOSTAL");
				if (node != null && this._reader.Decoders.UKPOSTAL.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.UKPOSTAL, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.UKPOSTAL.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.UKPOSTAL.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region UPCA OK OK
				node = xmldoc.SelectSingleNode("Scanner/UPCA");
				if (node != null && this._reader.Decoders.UPCA.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.UPCA, bool.Parse(chnode.InnerText)); break;
							case "Preamble": this._reader.Decoders.UPCA.Preamble = (UPC.Preambles)Enum.Parse(typeof(UPC.Preambles), chnode.InnerText, false); break;
							case "ReportCheckDigit": this._reader.Decoders.UPCA.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#region UPCE0 OK OK

				node = xmldoc.SelectSingleNode("Scanner/UPCE0");
				if (node != null && this._reader.Decoders.UPCE0.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.UPCE0, bool.Parse(chnode.InnerText)); break;
							case "ConvertToUPCA": this._reader.Decoders.UPCE0.ConvertToUPCA = bool.Parse(chnode.InnerText); break;
							case "ReportCheckDigit": this._reader.Decoders.UPCE0.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							case "Preamble": this._reader.Decoders.UPCE0.Preamble = (UPCE0.Preambles)Enum.Parse(typeof(UPCE0.Preambles), chnode.InnerText, false); break;
							default: break;
						}
					}
				}
				#endregion

				#region UPCE1 OK OK

				node = xmldoc.SelectSingleNode("Scanner/UPCE1");
				if (node != null && this._reader.Decoders.UPCE1.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.UPCE1, bool.Parse(chnode.InnerText)); break;
							case "ConvertToUPCA": this._reader.Decoders.UPCE1.ConvertToUPCA = bool.Parse(chnode.InnerText); break;
							case "ReportCheckDigit": this._reader.Decoders.UPCE1.ReportCheckDigit = bool.Parse(chnode.InnerText); break;
							case "Preamble": this._reader.Decoders.UPCE1.Preamble = (UPCE1.Preambles)Enum.Parse(typeof(UPCE1.Preambles), chnode.InnerText, false); break;
							default: break;
						}
					}
				}
				#endregion

				#region US4STATE OK OK
				node = xmldoc.SelectSingleNode("Scanner/US4STATE");
				if (node != null && this._reader.Decoders.US4STATE.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.US4STATE, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.US4STATE.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.US4STATE.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region US4STATE_FICS OK
				node = xmldoc.SelectSingleNode("Scanner/US4STATE_FICS");
				if (node != null && this._reader.Decoders.US4STATE_FICS.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.US4STATE_FICS, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.US4STATE_FICS.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.US4STATE_FICS.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region USPLANET OK OK
				node = xmldoc.SelectSingleNode("Scanner/USPLANET");
				if (node != null && this._reader.Decoders.USPLANET.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.USPLANET, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.USPLANET.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.USPLANET.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region USPOSTNET OK OK
				node = xmldoc.SelectSingleNode("Scanner/USPOSTNET");
				if (node != null && this._reader.Decoders.USPOSTNET.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.USPOSTNET, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.USPOSTNET.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.USPOSTNET.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							default: break;
						}
					}
				}
				#endregion

				#region WEBCODE OK OK

				node = xmldoc.SelectSingleNode("Scanner/WEBCODE");
				if (node != null && this._reader.Decoders.WEBCODE.IsSupported)
				{
					foreach (XmlNode chnode in node.ChildNodes)
					{
						switch (chnode.LocalName)
						{
							case "Enabled": this._reader.Decoders.SetEnabled(DecoderTypes.WEBCODE, bool.Parse(chnode.InnerText)); break;
							case "MaximumLength": this._reader.Decoders.WEBCODE.MaximumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "MinimumLength": this._reader.Decoders.WEBCODE.MinimumLength = int.Parse(chnode.InnerText, NumberFormatInfo.InvariantInfo); break;
							case "GTWebcode": this._reader.Decoders.WEBCODE.GTWebcode = bool.Parse(chnode.InnerText); break;
							default: break;
						}
					}
				}
				#endregion

				#endregion





			}
			catch
			{
				//Log.Write(ex.Message + ex.StackTrace, Fask.IScanner.ScannerTypes.Symbol_MC3000.ToString() + " : ScannerSettingsLoad");
				//Fask.MST_W.Forms.MsgBox.Show(ex.Message, Scanner.ScannerTypes.Symbol_MC3000.ToString(), System.Windows.Forms.MessageBoxButtons.OK, MsgBoxIcon.Critical);
			}

		}

		private bool _enabled = false;
		public bool Enabled
		{
			get { return _enabled; }
		}

		public string ConfigScanner
		{
			get
			{
				return _configScanner;
			}
			set
			{
				_configScanner = value;
			}
		}

		public AIMTYPE AimType
		{
			set
			{
				if (_reader == null)
					return;

				ReaderParams readerParams = _reader.ReaderParameters;
				switch (readerParams.ReaderType)
				{
					case READER_TYPE.READER_TYPE_IMAGER:
						readerParams.ReaderSpecific.ImagerSpecific.AimType = (AIM_TYPE)value;
						break;
					case READER_TYPE.READER_TYPE_LASER:
						readerParams.ReaderSpecific.LaserSpecific.AimType = (AIM_TYPE)value;
						break;
					case READER_TYPE.READER_TYPE_CONTACT:
					default:
						break;
				}

				//this.Disable();
				//this.Enable();
			}
			get
			{
				if (_reader == null)
					return AIMTYPE.UNKNOWN;

				ReaderParams readerParameters = _reader.ReaderParameters;
				if (readerParameters.ReaderType == READER_TYPE.READER_TYPE_LASER)
				{
					return (AIMTYPE)readerParameters.ReaderSpecific.LaserSpecific.AimType;
				}
				else if (readerParameters.ReaderType == READER_TYPE.READER_TYPE_IMAGER)
				{
					return (AIMTYPE)readerParameters.ReaderSpecific.ImagerSpecific.AimType;
				}
				else if (readerParameters.ReaderType == READER_TYPE.READER_TYPE_CONTACT)
				{
					return AIMTYPE.UNKNOWN;
				}
				else
					return AIMTYPE.UNKNOWN;
			}
		}

		public Delegate[] InvocationList()
		{
			if (this.DataReady != null)
				return this.DataReady.GetInvocationList();

			return new Delegate[] { };
		}

		public void Log_DataReady_Events()
		{
			try
			{
				Logging.Log.Write("MC3090 Scanner Target InvocationList ... Start");
				foreach (var dlgt in this.DataReady.GetInvocationList())
				{
					try
					{
						Logging.Log.Write(
							String.Format("{0},{1},{2},{3}",
							dlgt.ToString(),
							dlgt.Method,
							dlgt.Target.ToString(),
							dlgt.Target.GetType().ToString()
							)
						);

					}
					catch (Exception exLogging)
					{
						Logging.Log.Write(exLogging);
					}
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
			finally
			{
				Logging.Log.Write("MC3090 Scanner Target InvocationList ... End");
			}
		}

		public int SuccessBeepTime
		{
			get
			{
				if (this._reader == null)
					return 0;

				return this._reader.Parameters.Feedback.Success.BeepTime;
			}
			set
			{
				if (this._reader == null)
					return;

				this._reader.Parameters.Feedback.Success.BeepTime = value;
			}
		}
	}
}
