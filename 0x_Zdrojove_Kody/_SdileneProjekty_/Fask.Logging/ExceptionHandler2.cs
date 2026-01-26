using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Logging
{
    public class ExceptionHandler2
    {

		private static bool _enablePrintLogging = true;
		/// <summary>
		/// Povoluje logovani tiskovych operaci
		/// </summary>
		public static bool EnablePrintLogging
		{
			get { return _enablePrintLogging; }
			set { _enablePrintLogging = value; }
		}

		private static bool _sendErrorEmail = true;
		/// <summary>
		/// Povoluje logovani emailem
		/// </summary>
		public static bool SendErrorEmail
		{
			get { return _sendErrorEmail; }
			set { _sendErrorEmail = value; }
		}

        private static ILog2 LogStatic = new Log3();


        public static void SetPath(string Path)
        {
            LogStatic.SetPath(Path);
        }

        public static void SetEnablePrint(bool enable)
        {
            LogStatic.EnablePrint = enable;
        }

        #region Exception

        /// <summary>
        /// Varianta pro zalogování Exception. Chyba je pouze zalogovana a uživatel neni nijak upozornen
        /// </summary>
        /// <param name="exeption">Vynimka</param>
        /// <returns></returns>
        public static bool Handle(Exception exeption)
        {
            return ExceptionHandler2.LogStatic.write(exeption);
        }

		/// <summary>
		/// Varianta pro zalogování Exception s specifikaci modulu a metody. Chyba je pouze zalogovana a uživatel neni nijak upozornen
		/// </summary>
		/// <param name="modul"> Nazev modulu</param>
		/// <param name="method">Nazev metody</param>
		/// <param name="exeption">Vynimka</param>
		/// <returns></returns>
		public static bool Handle(string modul, string method, Exception exeption)
		{
			return ExceptionHandler2.LogStatic.write(exeption, modul, method);
		}

        /// <summary>
        /// Varianta pro zalogování Exception s specifikaci modulu a metody. Chyba je pouze zalogovana a uživatel neni nijak upozornen
        /// </summary>
        /// <param name="modul"> Nazev modulu</param>
        /// <param name="method">Nazev metody</param>
        /// <param name="messageBoxShow">Oznamovací okno</param>
        /// <returns></returns>
        public static bool Handle(string modul, string method, bool messageBoxShow)
        {
            System.Windows.Forms.DialogResult msgResult;
            return Handle(new Exception(modul + method), messageBoxShow, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, out msgResult);
        }

        /// <summary>
        /// Varianta pro zalogování správy s specifikaci modulu a metody. Správa je pouze zalogovana a uživatel neni nijak upozornen
        /// </summary>
        /// <param name="level"> Slouží pro typ logovane informace, a kam se má uložit</param>
        /// <param name="modul"> Nazev modulu</param>
        /// <param name="method">Nazev metody</param>
        /// <param name="message">Správa</param>
        /// <returns></returns>
        public static bool Handle(LogLevel level, string modul, string method, string message)
		{
			return ExceptionHandler2.LogStatic.write(level,modul,method, message);
		}



        /// <summary>
        /// Varianta pro zalogování Exception. Chyba je zalogovana a uživatel je upozornen podle tho či to je vyžadovani anebo ne, třeba konfiguračne potlačni atd.
        /// </summary>
        /// <param name="exeption"> Vynimka</param>
        /// <param name="messageBoxShow">True - Zobrazi se messageBox</param>
        /// <param name="messageBoxShow">False - NEzobrazi se messageBox</param>
        /// <returns></returns>
        public static bool Handle(Exception exeption, bool messageBoxShow)
        {
            System.Windows.Forms.DialogResult msgResult;
            return Handle(exeption, messageBoxShow, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, out msgResult);
        }


        /// <summary>
        /// Jedná se o metodu ktera Zaloguje vynimku a ma možnost zobrazit MessageMox
        /// </summary>
        /// <param name="exeption"> Vynimka</param>
        /// <param name="msgShow">True - zobrazi se, False- Nezobrazi se</param>
        /// <param name="msgButtons"> Volba tlačitek</param>
        /// <param name="msgIcon">Volba ikony</param>
        /// <param name="msgResult">Vracena odpoved od MessageBox</param>
        /// <returns></returns>
        public static bool Handle(Exception exeption, bool msgShow, System.Windows.Forms.MessageBoxButtons msgButtons, System.Windows.Forms.MessageBoxIcon msgIcon, out System.Windows.Forms.DialogResult msgResult)
        {
            msgResult = System.Windows.Forms.DialogResult.None;
            try
            {
                ExceptionHandler2.LogStatic.write(exeption);
                if (msgShow)
                {
                    ShowMessageBox(exeption.Message, msgButtons, msgIcon, out msgResult);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.LogStatic.write(ex);
                return false;
            }
        }

        #endregion

        #region String
        /// <summary>
        /// Metoda slouží pro uloženi textové informace do Logu
        /// </summary>
        /// <param name="level"> Slouží pro typ logovane informace, a kam se má uložit</param>
        /// <param name="Message">Textová zpráva co se má uložit</param>
        /// <returns></returns>
        public static bool Handle(LogLevel level, string Message)
        {
            return ExceptionHandler2.LogStatic.write(level, Message);
        }

        /// <summary>
        /// Možnost zalogovat Text s učitym levelem
        /// </summary>
        /// <param name="lvl">Typ logovane informace</param>
        /// <param name="Message">Logovana informace</param>
        /// <param name="messageBoxShow">Zda zobrazit messageBox</param>
        /// <returns></returns>
        public static bool Handle(LogLevel lvl, string Message, bool messageBoxShow)
        {
            System.Windows.Forms.DialogResult msgResult;
            return Handle(lvl, Message, messageBoxShow, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, out msgResult);
        }


        /// <summary>
        /// Možnost logovat text a zobrazenim MessageBox
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="msg"></param>
        /// <param name="msgShow"></param>
        /// <param name="msgButtons"></param>
        /// <param name="msgIcon"></param>
        /// <param name="msgResult"></param>
        /// <returns></returns>
        public static bool Handle(LogLevel lvl, string msg, bool msgShow, System.Windows.Forms.MessageBoxButtons msgButtons, System.Windows.Forms.MessageBoxIcon msgIcon, out System.Windows.Forms.DialogResult msgResult)
        {
            msgResult = System.Windows.Forms.DialogResult.None;
            try
            {
                ExceptionHandler2.LogStatic.write(lvl, msg);
                if (msgShow)
                {
                    ShowMessageBox(msg, msgButtons, msgIcon, out msgResult);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.LogStatic.write(ex);
                return false;
            }
        } 
        #endregion

		#region Save File

		/// <summary>
		/// Metoda pro uložení souboru jak pole bytu do ErrorDataFileDirectory složky
		/// </summary>
		/// <param name="data">byte pole, soubor</param>
		/// <param name="specification">upřesneni, specifikace souboru</param>
		/// <returns>Cesta k uloženemu souboru</returns>
		public static string Handle(byte[] data, string specification)
		{
			return ExceptionHandler2.LogStatic.write(data, specification);
		}

        /// <summary>
        /// Uložení textu do custom souboru...
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="PathToFile"></param>
        /// <returns></returns>
        public static bool Handle(string msg, string PathToFile)
        {
            return ExceptionHandler2.LogStatic.writeToFile(msg, PathToFile);
        }

        /// <summary>
        /// Uložení textu do custom souboru...
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="PathToFile"></param>
        /// <returns></returns>
        public static bool Handle(string msg, string FileName, string FileExtension)
        {
            return ExceptionHandler2.LogStatic.writeToFile(msg, FileName, FileExtension);
        }

        #endregion

        #region Email

        public static bool Handle_Email(string Message)
		{
			return ExceptionHandler2.LogStatic.write_Email(Message);
		}
		
		#endregion

        #region Private methody
        /// <summary>
        /// Privatna metoda pro možnost zobrazit MessageBox
        /// </summary>
        /// <param name="Message"> Zpráva co se zobrazi</param>
        /// <param name="msgButtons"> Volba tlačitek</param>
        /// <param name="msgIcon">Volba ikony</param>
        /// <param name="msgResult">Vracena odpoved od MessageBox</param>
        private static void ShowMessageBox(string Message, System.Windows.Forms.MessageBoxButtons msgButtons, System.Windows.Forms.MessageBoxIcon msgIcon, out System.Windows.Forms.DialogResult msgResult)
        {
            msgResult = System.Windows.Forms.DialogResult.None;
            msgResult = System.Windows.Forms.MessageBox.Show(Message, "Chyba", msgButtons, msgIcon, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        }
        #endregion


        #region System.Data.DataSet , System.Data.DataTable

        public static bool Handle(System.Data.DataSet DS)
        {
            return ExceptionHandler2.LogStatic.write(DS);
        }

        public static bool Handle(System.Data.DataTable DT)
        {
            return ExceptionHandler2.LogStatic.write(DT);
        }


        #endregion

		public static bool Handle_Delete(LogLevel level)
		{
			return ExceptionHandler2.LogStatic.DeleteLog(level);
		}

        public static void HandleCSV(int message, string ean)
        {
             ExceptionHandler2.LogStatic.WriteScannerCSV( message,  ean);
        }

        public static void HandleCSV(string login, decimal qty, decimal qtyreal, string barcode)
        {
             ExceptionHandler2.LogStatic.WriteDataStoreCSV( login,  qty,  qtyreal,  barcode);
        }

        public static string Handle_GetByFileName(string FileName)
        {
            return ExceptionHandler2.LogStatic.GetLog(FileName);
        }

    }
}
