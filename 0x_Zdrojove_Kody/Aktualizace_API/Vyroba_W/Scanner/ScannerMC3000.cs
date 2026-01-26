using System;
using System.Collections.Generic;
using System.Text;
//using Symbol.Generic;
using Symbol.Barcode;
using System.Collections;
using System.Xml.Serialization;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Fask.MST_W.Scanner
{
    public sealed class Symbol_MC3000 : ScannerBase
    {
        private Symbol.Barcode.Reader _reader = null;
        private Symbol.Barcode.ReaderData _readerData = null;

        public Symbol_MC3000()
        {
            try
            {
                this.InitializeScanner();
            }
            catch
            {
            }
        }

        protected override void InitializeScanner()
        {
            //base.InitializeScanner();
            try
            {
                // If the scanner is already present, fail to initialize
                if (_reader != null)
                    return;

                // Create a new scanner; use the first available scanner
                _reader = new Symbol.Barcode.Reader();

                // Create the scanner data
                _readerData = new Symbol.Barcode.ReaderData(ReaderDataTypes.Text, ReaderDataLengths.DefaultText);

                // Create the event handler delegate
                _reader.ReadNotify += new EventHandler(_reader_ReadNotify);

                // Enable the scanner with a wait cursor
                _reader.Actions.Enable();

                // Set up the scanner
                //_reader.Parameters.Feedback.Success.BeepTime = 0;
                //_reader.Parameters.Feedback.Success.WaveFile = "\\windows\\alarm3.wav";
                //_reader.Parameters.ScanType = ScanTypes.Background;

                this.EnableAllBarcodes();

                this.ScannerSettingLoad();

                return;
            }
            catch
            {
            }
        }

        public override void TerminateScanner()
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

        public override event ScannerEventHandler DataReady;

        void _reader_ReadNotify(object sender, EventArgs e)
        {
            ReaderData readerData = _reader.GetNextReaderData();

            // If successful, scan
            if (readerData.Result == Symbol.Results.SUCCESS)
            {
                // Raise the scan event to the caller (with data)
                if (DataReady != null)
                {
                    DataReady(sender, new ScannerEventArgs(readerData.Text, (uint)readerData.Type, readerData.Type.ToString(), (uint)readerData.Length));
                }

                // Start the next scan
                this.Enable();
            }
        }

        public override void Enable()
        {
            //base.Enable();
            try
            {
                // If you have both a scanner and data
                if ((_reader != null) && (_readerData != null))
                {
                    // Submit a scan
                    _reader.Actions.Read(_readerData);
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        public override void Disable()
        {
            //base.Disable();
            // If you have a scanner
            if (_reader != null)
                // Cancel all pending scans
                _reader.Actions.Flush();
        }

        public override void EnableAllBarcodes()
        {
            this._reader.Decoders.EnableAll();
        }

        public override void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            //Symbol.StandardForms.ScanParamsForm.Run(this._reader);
            Symbol.StandardForms.EnabledDecoders.Run(this._reader);

            this.ScannerSettingSave();
        }

        public override void ScannerSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            Symbol.StandardForms.ScanParamsForm.Run(this._reader);

            this.ScannerSettingSave();
        }

        public override void ScannerSettingSave()
        {
            System.Xml.XmlWriter xmlwrite = null;
            try
            {
                xmlwrite = System.Xml.XmlWriter.Create(Path.Combine(Fask.Vyroba_W.MySystem.MyPath.CurrentDirectory, Vyroba_W.Constants.ConfigScanner));

                xmlwrite.WriteStartElement("Scanner");

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

                xmlwrite.WriteEndElement(); //Scanner
                xmlwrite.Close();
                xmlwrite = null;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message + ex.StackTrace, Scanner.ScannerTypes.Symbol_MC3000.ToString() + " : ScannerSettingsSave");
                MessageBox.Show(ex.Message, Scanner.ScannerTypes.Symbol_MC3000.ToString(), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        public override void ScannerSettingLoad()
        {
            if (!System.IO.File.Exists(Path.Combine(Fask.Vyroba_W.MySystem.MyPath.CurrentDirectory, Vyroba_W.Constants.ConfigScanner)))
                return;

            try
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                xmldoc.Load(Path.Combine(Fask.Vyroba_W.MySystem.MyPath.CurrentDirectory, Vyroba_W.Constants.ConfigScanner));
                XmlNode node = xmldoc.SelectSingleNode("Scanner/CODE39");
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

                node = xmldoc.SelectSingleNode("Scanner/I2OF5");
                if (node != null && this._reader.Decoders.I2OF5.IsSupported)
                {
                    foreach (XmlNode chnode in node.ChildNodes)
                    {
                        switch (chnode.LocalName)
                        {
                            case "ConvertToEAN13" : this._reader.Decoders.I2OF5.ConvertToEAN13 = bool.Parse(chnode.InnerText); break;
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

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message + ex.StackTrace, Scanner.ScannerTypes.Symbol_MC3000.ToString() + " : ScannerSettingsLoad");
                //MessageBoxBig.Show(ex.Message, Scanner.ScannerTypes.Symbol_MC3000.ToString(), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }

        }

    }
}
