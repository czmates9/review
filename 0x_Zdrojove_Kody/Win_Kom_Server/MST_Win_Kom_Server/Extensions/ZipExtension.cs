using System;
using System.Web.Services.Protocols;
using System.IO;
using System.Diagnostics;
using Interval.SoapExtensions.Utilities;


namespace Interval.SoapExtensions
{
	/// <summary>
	/// SOAP Extenze pro komprimaci pøenášených dat
	/// </summary>
	public class ZipExtension : SoapExtension
	{
		//System.Web.Services.Protocols.SoapHttpClientProtocol
		
		#region Private fields
		private Stream oldStream;
		private Stream newStream;
		private ZipUtilities zipUtil;
		#endregion Private fields

		#region Public methods
		public override object GetInitializer(LogicalMethodInfo methodInfo,
			SoapExtensionAttribute attribute)
		{
			return attribute;
		}

		public override object GetInitializer(Type t)
		{
			return new ZipExtensionAttribute();
		}

		public override void Initialize(object initializer)
		{
			ZipExtensionAttribute attribute = (ZipExtensionAttribute) initializer;
			zipUtil = new ZipUtilities();
			
			if(attribute.ZipLevel  != -1)
			{
				zipUtil.ZipLevel = attribute.ZipLevel;
			}
			
			
			return;
		}

		public override void ProcessMessage(SoapMessage message)
		{
			Debug.WriteLine("ProcessMessage");
			switch (message.Stage)
			{


				case SoapMessageStage.BeforeSerialize:
					break;

				case SoapMessageStage.AfterSerialize:
					
					
					ZipMessage();

					break;

				case SoapMessageStage.BeforeDeserialize:
					
					UnzipMessage();
					break;

				case SoapMessageStage.AfterDeserialize:
					break;

				default:
					throw new Exception("invalid stage");
			}
		}

		public override Stream ChainStream( Stream stream )
		{
			oldStream = stream;
			newStream = new MemoryStream();
			return newStream;
		}

	

		#endregion Public methods
		
		#region private methods
		
		private void ZipMessage()
		{	
			newStream.Position = 0;
			Stream retStream = zipUtil.ZipSoapBody(newStream);
			retStream.Position = 0;
			copyStreams(retStream, oldStream);
		}
		
		private void UnzipMessage()
		{	
			
			MemoryStream copyOldStream = new MemoryStream();
			//oldStream.Position = 0;
			copyStreams(oldStream, copyOldStream);
			copyOldStream.Position = 0;
			bool isCompresed;
			Stream retStream = zipUtil.UnzipSoapBody(copyOldStream, out isCompresed);
			retStream.Position = 0;
			newStream.Position = 0;
			copyStreams(retStream, newStream);
			newStream.Position = 0;

		}

		
		private void copyStreams(Stream from, Stream to)
		{
			TextReader reader = new StreamReader(from);
			TextWriter writer = new StreamWriter(to);
			writer.WriteLine(reader.ReadToEnd());
			writer.Flush();
		}

		
		#endregion private methods
	}
}
