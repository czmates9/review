using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.IO;
using System.Web.Configuration;
using System.Security.Cryptography;
using Fask;
using Fask.Logging;
using Fask.MST_W_Server.Constants;

namespace Vyroba
{
    /// <summary>
	/// Služba přenosu dat FileTransfer
    /// </summary>
	[WebService(Namespace = "http://FileTransfer.fask.cz/", Description = "Služba přenosu dat FileTransfer")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FileTransfer : System.Web.Services.WebService
    {

		#region lokalne promenne

		private string PathSQLiteDBs;
		
		#endregion


		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public FileTransfer()
		{

			PathSQLiteDBs = Path.Combine(Server.MapPath("~"),  Fask.MyPath.Path.SQLiteDBsDirectory + Common.Backslash);
			if (!Directory.Exists(PathSQLiteDBs))
			{
				Directory.CreateDirectory(PathSQLiteDBs);
			}
		} 
		#endregion


		#region WebMetody

		/// <summary>
		/// Fiktivní metoda ke kontrole připojení k webové službě
		/// </summary>
		[WebMethod(Description = "Fiktivní metoda ke kontrole připojení k webové službě")]
		public void Ping()
		{
		}

		/// <summary>
		/// Metoda která vrát seznam souboru v složce pro daný terminal
		/// </summary>
		/// <param name="TerminalID">ID Terminalu</param>
		/// <returns>List souboru v složce</returns>
		[WebMethod(Description = "Metoda která vrát seznam souboru v složce pro daný terminal")]
		public List<string> GetFilesList(byte TerminalID)
		{

			string FilePath = Path.Combine(PathSQLiteDBs, TerminalID.ToString());

			List<string> files = new List<string>();
			foreach (string s in Directory.GetFiles(FilePath))
				files.Add(Path.GetFileName(s));
			return files;
		}

		/// <summary>
		/// Metoda která smaže zadaný soubor v složce zadaneho terminalu
		/// </summary>
		/// <param name="TerminalID">ID Terminalu</param>
		/// <param name="FileName">Nazev souboru</param>
		/// <returns>True - smazano, False - chyba</returns>
		[WebMethod(Description = "Metoda která smaže zadaný soubor v složce zadaneho terminalu")]
		public bool Delete(byte TerminalID, string FileName)
		{
			try
			{
				string FilePath = Path.Combine(PathSQLiteDBs, TerminalID.ToString());
				if (!Directory.Exists(FilePath))
					Directory.CreateDirectory(FilePath);
				FilePath = Path.Combine(FilePath, Path.GetFileName(FileName));

				if (!File.Exists(FilePath))
					return true;

				File.Delete(FilePath);

				return true;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}
		}

		/// <summary>
		/// Metoda která vypočita MD5 HASH pro zadaný soubor v složce zadaneho terminalu
		/// </summary>
		/// <param name="TerminalID">ID Terminalu</param>
		/// <param name="FileName">Nazev soubou</param>
		/// <returns>HASH vypočitany</returns>
		[WebMethod(Description = "Metoda která vypočita MD5 HASH pro zadaný soubor v složce zadaneho terminalu")]
		public string CheckFileHash(byte TerminalID, string FileName)
		{

			string FilePath = Path.Combine(PathSQLiteDBs, TerminalID.ToString());
			FilePath = Path.Combine(FilePath, Path.GetFileName(FileName));

			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] hash;
			using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
				hash = md5.ComputeHash(fs);
			return BitConverter.ToString(hash);
		}

		/// <summary>
		/// Metoda pomoci ktere se ukladaji LOG a TRACE soubory na server
		/// </summary>
		/// <param name="TerminalID">ID Terminalu</param>
		/// <param name="filenamezip">Nazev souboru, ktery posila terminal na server (nazev souboru, ktery ctecka odeslala na server)</param>
		[WebMethod(Description = "Metoda pomoci ktere se ukladaji LOG a TRACE soubory na server")]
		public void SaveLog2(byte TerminalID, string filenamezip)
		{
			string directory_path = Path.Combine(Server.MapPath("~"), Fask.MyPath.Path.LogsDirectory + Common.Backslash);
			if (!Directory.Exists(directory_path))
				Directory.CreateDirectory(directory_path);

			string file_path_txt_tmp_zip = System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, TerminalID.ToString() + "\\" + Path.GetFileName(filenamezip));
			string file_path_txt_tmp = System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, TerminalID.ToString() + "\\" + Path.GetFileNameWithoutExtension(file_path_txt_tmp_zip));
			string file_path_txt = System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, TerminalID.ToString() + "\\" + Path.GetFileNameWithoutExtension(file_path_txt_tmp));
			string file_path_Out = System.IO.Path.Combine(directory_path, Path.GetFileNameWithoutExtension(file_path_txt) + TerminalID.ToString() + ".txt");

			Fask.Compressing.Zip.Decompress(file_path_txt_tmp_zip);

			using (StreamWriter w = File.AppendText(file_path_Out))
			{
				var all = File.ReadAllLines(file_path_txt_tmp);
				foreach (string item in all)
				{
					w.WriteLine(item);
				}
				w.Flush();
			}

			File.Delete(file_path_txt_tmp);
		}

		#endregion

		#region Nezname...


		/// <summary>
		/// Throws a soap exception.  It is formatted in a way that is more readable to the client, after being put through the xml serialisation process
		/// Typed exceptions don't work well across web services, so these exceptions are sent in such a way that the client
		/// can determine the 'name' or type of the exception thrown, and any message that went with it, appended after a : character.
		/// </summary>
		/// <param name="exceptionName"></param>
		/// <param name="message"></param>
		public static void CustomSoapException(string exceptionName, string message)
		{
			throw new System.Web.Services.Protocols.SoapException(exceptionName + ": " + message, new System.Xml.XmlQualifiedName("BufferedUpload"));
		}

		
		#endregion

    }
}
