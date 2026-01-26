using System;
//using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Logging
{
	public class ExceptionHandler2
	{
		/// <summary>
		/// Zpracovava vyjimky v systemu
		/// </summary>
		/// <param name="ex">Vyjimka</param>
		/// <remarks>Zobrazuje dialog s chybovym hlasenim</remarks>
		public static bool Handle(Exception exception)
		{
			return Handle(exception, false);
		}
		/// <summary>
		/// Zpracovava vyjimky v systemu
		/// </summary>
		/// <param name="ex">Vyjimka</param>
		/// <returns>Zda byla zpracovana. True = zpracovano uspeseni; False = nezpracovano uspesne</returns>
		public static bool Handle(Exception exeption, bool messageBoxShow)
		{
			System.Windows.Forms.DialogResult msgResult;
			return Handle(exeption, messageBoxShow, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, out msgResult);
		}

		/// <summary>
		/// Zpracovava vyjimky v systemu
		/// </summary>
		/// <param name="ex">Vyjimka</param>
		/// <returns>Zda byla zpracovana. True = zpracovano uspeseni; False = nezpracovano uspesne</returns>
		public static bool Handle(Exception exeption, bool msgShow, System.Windows.Forms.MessageBoxButtons msgButtons, System.Windows.Forms.MessageBoxIcon msgIcon, out System.Windows.Forms.DialogResult msgResult)
		{
			msgResult = System.Windows.Forms.DialogResult.None;
			try
			{
				Logging.Log.Write(exeption);
				if (msgShow)
				{
					msgResult = System.Windows.Forms.MessageBox.Show(exeption.Message, "Chyba", msgButtons, msgIcon, System.Windows.Forms.MessageBoxDefaultButton.Button1);
				}
				return true;
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				return false;
			}
			finally
			{
			}
		}
	}
}
