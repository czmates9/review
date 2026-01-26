using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services.Protocols;
using System.IO;

namespace Fask.MST_W_Server.ExtensionsSpy
{
	public class SpySoapExtension : SoapExtension 
	{

		private string m_fileName;
		private System.IO.Stream m_oldStream;
		private System.IO.Stream m_newStream;

		public override object GetInitializer(Type serviceType)
		{
			//Vrácení názvu souboru, do kterého budou uloženy SOAP zprávy
			return (serviceType.Assembly.FullName + ".spy"); 
		}

		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			//Přetypování na SpyExtensionAttribute
			SpyExtensionAttribute spyAttribute = attribute as SpyExtensionAttribute;
			//Vrácení názvu souboru, do kterého budou uloženy SOAP zprávy
			return spyAttribute.FileName; 
		}

		public override void Initialize(object initializer)
		{
			//Uložení názvu souboru do privátní proměnné
			m_fileName = (string)initializer; 
		}

		public override void ProcessMessage(SoapMessage message)
		{
			switch (message.Stage)
			{
				//Uložíme SOAP zprávu, která přišla od klienta
				case SoapMessageStage.BeforeDeserialize:
					{
						WriteClientRequest();
						break;
					}
				//V této fázi nepotřebujeme nic zpracovávat
				case SoapMessageStage.AfterDeserialize:
					{
						break;
					}
				//V této fázi nepotřebujeme nic zpracovávat
				case SoapMessageStage.BeforeSerialize:
					{
						break;
					}
				//Uložíme odpověď ze serveru
				case SoapMessageStage.AfterSerialize:
					{
						WriteServerResponse();
						break;
					}
			} 
		}

		public override System.IO.Stream ChainStream(System.IO.Stream stream)
		{
			m_oldStream = stream;
			m_newStream = new MemoryStream();
			return m_newStream; 
		}

		private void WriteClientRequest()
		{
			//Reset pozice
			m_oldStream.Position = 0;
			m_newStream.Position = 0;
			//Překopírujeme obsah původního streamu do nového streamu
			CopyStream(m_oldStream, m_newStream, false);
			//Přesun na začátek streamu
			m_newStream.Position = 0;
			//Otevření souboru, do kterého bude přidána SOAP zpráva
			FileStream fs = new FileStream(m_fileName, FileMode.Append);
			//Zkopírování streamu do souboru
			CopyStream(m_newStream, fs, true);
			//Uzavření souboru
			fs.Close();
			//Opět reset pozice, aby ze streamu mohl číst ASP.NET runtime
			m_newStream.Position = 0;
		}


		private void WriteServerResponse()
		{
			//Přesun na začátek streamu
			m_newStream.Position = 0;
			//Otevření souboru, do kterého bude přidána SOAP zpráva
			FileStream fs = new FileStream(m_fileName, FileMode.Append);
			//Zkopírování streamu do souboru
			CopyStream(m_newStream, fs, true);
			//Uzavření souboru
			fs.Close();
			//Reset pozice
			m_newStream.Position = 0;
			//ASP.NET runtime čte z původního streamu
			CopyStream(m_newStream, m_oldStream, false);
		} 

		private void CopyStream (Stream from, Stream to, bool writeSeparator)
		{
		  TextReader sr = new StreamReader(from);
		  TextWriter sw = new StreamWriter (to);
		  if (writeSeparator)
		  {
			sw.WriteLine();
			sw.WriteLine(new String('-', 60));
		  }
		  sw.Write(sr.ReadToEnd());
		  sw.Flush();
		} 

	}
}
