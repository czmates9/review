using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace Fask.MST_W_Server.ExtensionsSpy
{
	[AttributeUsage(AttributeTargets.Method)]
	public class SpyExtensionAttribute : SoapExtensionAttribute 
	{

		private int m_priority;
		private string m_fileName;

		public SpyExtensionAttribute(string filename)
		{
			m_priority = 1;
			m_fileName = filename;
		}

		public override Type ExtensionType
		{
			get { return typeof(SpySoapExtension); }
		}

		public override int Priority
		{
			get {  return m_priority;  }
			set { m_priority = value; }
		}

		public string FileName
		{
			get { return m_fileName; }
			set { m_fileName = value; }
		} 
	}
}
