using System;
using System.Text;
using System.IO;
using System.Xml;
//using ICSharpCode.SharpZipLib.Checksums;
//using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.GZip;

namespace Interval.SoapExtensions.Utilities
{
	/// <summary>
	/// Pomocná tøída pro kompresi/dekompresi
	/// </summary>
	public class ZipUtilities
	{
		
		
		#region lokalne promenne
		
		//Promìnná pro uložení stupnì komprimace
		private int m_level;
		private const int BUFFER = 2048;

		#endregion private fields


		#region Konstruktory


		/// <summary>
		/// Standardní konstruktor
		/// </summary>
		public ZipUtilities()
		{
			m_level = 9;
		}

		/// <summary>
		/// Konstruktor s levelem komprese
		/// </summary>
		/// <param name="level"></param>
		public ZipUtilities(int level)
		{
			if ((level < 1) || (level > 9))
				throw new ArgumentOutOfRangeException("level", "level must be between values 1 and 9");

			m_level = level;
		} 

		#endregion
		
		
		#region Verejne promenne
		
		/// <summary>
		/// Stupeò komprimace
		/// </summary>
		/// <remarks>Hodnota musí být v intervalu 1-9, standardnì´je nastaveno 9</remarks>
		public int ZipLevel
		{
			get
			{
				return m_level;
			}
			set
			{
				m_level = value;
			}
		}

		#endregion 
		
		#region Public Method

		/// <summary>
		/// Zkomprimuje  vše v elementu SOAP Body
		/// </summary>
		/// <param name="inputStream">Vstupní stream</param>
		/// <returns>Komprimovaný stream</returns>
		/// <remarks>Ke komprimaci dojde pouze tehdy, pokud element SOAP BODY není prázdný</remarks>
		public virtual Stream ZipSoapBody (Stream inputStream)
		{
			XmlTextReader myReader = new XmlTextReader(inputStream);
			XmlDocument myDocument = new XmlDocument();
			myDocument.Load(myReader);
			
			XmlNamespaceManager myManager = new XmlNamespaceManager(myDocument.NameTable);
			myManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
			XmlNode bodyNode = myDocument.SelectSingleNode(@"//soap:Body",myManager);

			String originXML = bodyNode.InnerXml;
			
			//Nic ke komprimaci
			if(originXML.Length == 0)
			{
				return inputStream;

			}
			
			XmlNode headerNode = myDocument.SelectSingleNode(@"//soap:Header",myManager);
			
			if (headerNode == null)
			{
				headerNode = myDocument.CreateElement("soap:Header", "http://schemas.xmlsoap.org/soap/envelope/");
				bodyNode.ParentNode.InsertBefore(headerNode,bodyNode);
			}
			
			XmlElement compressedHeader = myDocument.CreateElement("IsCompressed");
			headerNode.AppendChild(compressedHeader);
			
			string zipXML = this.zipString(originXML);
//			StreamWriter myw = File.CreateText(@"c:\origin.txt");
//			myw.WriteLine(originXML);
//			myw.Close();
//			
//			myw = File.CreateText(@"c:\compress.txt");
//			myw.WriteLine(zipXML);
//			myw.Close();
			bodyNode.InnerXml = zipXML;

			MemoryStream retStream = new MemoryStream();
			myDocument.Save(retStream);
			
			return retStream;

	
		}
		

		/// <summary>
		/// dekomprimuje vše v elementu SOAP Body
		/// </summary>
		/// <param name="inputStream">Vstupní zkomprimomovaný stream</param>
		/// <param name="isCompressed">Vrátí hodnotu udávající, zda byl vstupní stream komprimován</param>
		/// <returns>Dekomprimovaný stream</returns>
		/// <remarks>K dekomprimaci dojde pouze tehdy, pokud bude nalezen SOAP Header s názvem IsCompressed</remarks>
		public virtual Stream UnzipSoapBody (Stream inputStream, out bool isCompressed)
		{
			isCompressed = false;
			XmlTextReader myReader = new XmlTextReader(inputStream);
			XmlDocument myDocument = new XmlDocument();
			myDocument.Load(myReader);

			
			XmlNodeList mylist = myDocument.GetElementsByTagName("IsCompressed");
			//Stream není zkomprimován
			if (mylist.Count == 0)
				return inputStream;
				//Vyhození hlavièky o komprimaci
			else
				mylist[0].ParentNode.RemoveChild(mylist[0]);
			
			
			XmlNamespaceManager myManager = new XmlNamespaceManager(myDocument.NameTable);
			myManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
			XmlNode bodyNode = myDocument.SelectSingleNode(@"//soap:Body",myManager);
			
			string compressedString = bodyNode.InnerXml;
			//Nic k dekomprimaci
			if (compressedString.Length == 0)
				return inputStream;
			
			isCompressed = true;
			string decXml = unzipString(compressedString);
			bodyNode.InnerXml = decXml;
			
			MemoryStream retStream = new MemoryStream();
			retStream.Position = 0;
			myDocument.Save(retStream);
			return retStream;


		}
		
		#endregion Public Method

		#region private methods
		/// <summary>
		/// Komprimace dat
		/// </summary>
		/// <param name="inputString">Øetìzec, který se má komprimovat</param>
		/// <returns>String s komprimovaným øetìzcem a v kódováni Base64</returns>
		private string zipString(string inputString)
		{
			byte[] inputArray = Encoding.UTF8.GetBytes(inputString);
			
			MemoryStream retStream = new MemoryStream();

			//ZipOutputStream retZip = new ZipOutputStream(retStream) ;


			using (System.IO.Compression.GZipStream retZip = new System.IO.Compression.GZipStream(retStream, System.IO.Compression.CompressionMode.Compress))
			{
				int read = 0;

				while (read < inputArray.Length)
				{
					if ((read + BUFFER) <= inputArray.Length)
						retZip.Write(inputArray, read, BUFFER);
					else
						retZip.Write(inputArray, read, inputArray.Length - read);

					read += BUFFER;
				}
			}

			//retZip.PutNextEntry(new ZipEntry("ZipContent"));
			
			//retZip.SetLevel(m_level);
			

			//retZip.Finish();
			//retZip.Close();

			return Convert.ToBase64String(retStream.ToArray());

		}
		
		/// <summary>
		/// Dekomprimace dat
		/// </summary>
		/// <param name="inputBase64String">Øetìzec, který se má dekomprimovat V kódování BASE64</param>
		/// <returns>Pole bajtù s dekomprimovaným øetìzcem</returns>
		private string unzipString(string inputBase64String)
		{

			byte[] inputArray = Convert.FromBase64String(inputBase64String) ;
			MemoryStream zippedStream = new MemoryStream(inputArray) ;
			MemoryStream retStream = new MemoryStream();

			System.IO.Compression.GZipStream zipInput = new System.IO.Compression.GZipStream(zippedStream, System.IO.Compression.CompressionMode.Decompress);

			//ZipInputStream zipInput = new ZipInputStream(zippedStream);
			//ZipEntry compEntry = zipInput. GetNextEntry();
			Byte[] buffer = new Byte[BUFFER] ;
			
			int size = zipInput.Read(buffer, 0, BUFFER);
			
			while (size > 0)
			{
				
				
				retStream.Write(buffer, 0, size);
				size = zipInput.Read(buffer, 0, BUFFER);
				
				
			}
			retStream.Position = 0;
			StreamReader myr = new StreamReader(retStream);
			
			return myr.ReadToEnd();


		}

		#endregion Private methods
	}
}


