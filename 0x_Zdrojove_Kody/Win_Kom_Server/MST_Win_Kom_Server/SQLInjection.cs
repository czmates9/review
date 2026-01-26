using System;

namespace Fask
{
	/// <summary>
	/// Summary description for SQLInjectionTest.
	/// </summary>
	public class SQLInjection
	{
		private SQLInjection()
		{
		}

		/// <summary>
		/// filtruje uzivatelsky vstup proti sql-injection
		/// </summary>
		/// <param name="userinput"></param>
		/// <returns></returns>
		public static string Filter(string userinput)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(userinput.Trim());
			sb.Replace("'", "");
			sb.Replace("%", "");
			sb.Replace("-", "");
			sb.Replace(" ", "");
			return sb.ToString();
		}
	}
}
