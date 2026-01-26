using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.Logins
{
	public partial class Uzivatel
	{
		public byte GetUzivatele_VedouciSmeny(FASK.Logins.DataSets.Pristupy.FASK_LoginsRow item)
		{
			byte VS = 0;


			try
			{
				var ds = Komunikace.GetAgednaID("V_VS_");

				foreach (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow row in ds.FASK_Logins)
				{
					if ((item.USERID.Trim() == row.USERID.Trim()) && (item.psswd.Trim() == row.psswd.Trim()))
					{
						return 1;
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return VS;
		}
	}
}
