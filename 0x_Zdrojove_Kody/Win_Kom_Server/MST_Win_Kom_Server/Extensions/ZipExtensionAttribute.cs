using System;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Interval.SoapExtensions
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ZipExtensionAttribute : SoapExtensionAttribute
	{

		
		public ZipExtensionAttribute()
		{
			m_priority = 2;
			m_zipLevel = 9;
		}
		private int m_priority;
		private int m_zipLevel;

		public override Type ExtensionType
		{
			get { return typeof(ZipExtension); }
		}

		public override int Priority
		{
			get
			{
				return m_priority;
			}
			set
			{
				m_priority = value;
			}
		}
		
		/// <summary>
		/// Stupeò komprimace
		/// </summary>
		public int ZipLevel
		{
			get
			{
				return m_zipLevel;
			}
			set
			{
				m_zipLevel = value;
			}
		}

	}
}


