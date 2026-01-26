using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Lokace;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes
{
    public class Lokace
    {

		#region Public Metody

		/// <summary>
		/// Prijem Polozky
		/// </summary>
		/// <param name="record">Zaznam, ktereho se prijem tyka.</param>
		/// <param name="command">Prikaz</param>
		/// <param name="connection">Pripojeni</param>
		/// <param name="transaction">Transakce</param>
		/// <param name="adapter">Adapter</param>
		public void ProcessPrijem(LokacePohyb record, SqlCommand command, SqlConnection connection, SqlTransaction transaction, SqlDataAdapter adapter)
		{
			if (record == null || adapter == null || command == null || connection == null || transaction == null)
				throw new ApplicationException("Některý z parametrů funkce ProcessPrijem není inicializovaný.");

			// kontrola na existenci lokace
			StatusOverLokace status = OverLokace(record.SKL_ID_SRC, record.LOCNCODE_SRC, command, connection, transaction);
			if (status.State == STATUSOverLokace.ERROR)
				throw new Exception("Lokace '" + record.LOCNCODE_SRC.Trim() + "' neexistuje ve skladu '" + record.SKL_ID_SRC.Trim() + "'!");

			// kontrola aktualniho mnozstvi na lokaci
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND LOCNCODE=@locncode AND SKL_ID=@skl_id";
			command.Connection = connection;
			command.Transaction = transaction;

			command.Parameters.Clear();
			command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
			command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
			command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
			command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);

			Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();
			adapter.SelectCommand = command;
			adapter.Fill(data.CZMST_SkladLokace_Stav);

			decimal oldQuantity = decimal.Zero, newQuantity = decimal.Zero;

			if (data.CZMST_SkladLokace_Stav.Rows.Count > 0)
			{
				// zaznam existuje ... aktualizace mnozstvi
				// zjisteni aktualniho mnozstvi na lokaci
				oldQuantity = data.CZMST_SkladLokace_Stav.First().QTYSHPPD;
				checked { newQuantity = oldQuantity + record.QTYSHPPD; }

				// aktualizace mnozstvi
				UpdateQuantity(newQuantity, record.ITEMNMBR, record.SERLTNUM, record.LOCNCODE_SRC, record.SKL_ID_SRC, command, connection, transaction);
			}
			else
			{
				command.Parameters.Clear();

				// zaznam neexistuje ... je mozne vlozit novy zaznam.
				// vytvoreni prikazu pro vlozeni nove hodnoty mnozstvi materialu do regalu.
				command.CommandText =
					"INSERT INTO " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV +
					" (ITEMNMBR, ITEMDESC, QTYSHPPD_DEF, QTYSHPPD, SERLTNUM, SKL_ID, LOCNCODE, DATECHANGE, EXPIRATION)" +
					" VALUES" +
					" (@itemnmbr, @itemdesc, @qtyshppd_def, @qtyshppd, @serltnum, @skl_id, @locncode, @datechange, @expiration)";

				// \TODO : ? predelat na AddWithValue???
				command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
				command.Parameters.AddWithValue("@itemdesc", record.ITEMDESC);
				//command.Parameters.AddWithValue("@qtyshppd_def", (decimal)0.0));     // qtyshppd_def meni pouze inventura
				command.Parameters.AddWithValue("@qtyshppd_def", (decimal)0);
				//command.Parameters.AddWithValue("@qtyshppd_def", record.QTYSHPPD_DEF));
				command.Parameters.AddWithValue("@qtyshppd", record.QTYSHPPD);
				command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
				command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);
				command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
				command.Parameters.AddWithValue("@datechange", DateTime.Now);
				//command.Parameters.AddWithValue("@expiration", record.Expiration.HasValue ? record.Expiration : null));
				command.Parameters.AddWithValue("@expiration", record.Expiration.HasValue ? record.Expiration : (object)DBNull.Value);

				command.ExecuteNonQuery();
			}

			// zjisteni maximalniho id z tabulky historie pohybu
			int max_id = CheckMaxID(Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, command, connection, transaction);

			// vlozeni zaznamu do tabulky historie pohybu
			AddMovement(Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, record, max_id, command, connection, transaction);
		}

		/// <summary>
		/// Vydej polozky.
		/// </summary>
		/// <param name="Record">Zaznam, ktereho se vydej tyka.</param>
		/// <param name="Command">Prikaz.</param>
		/// <param name="Connection">Pripojeni.</param>
		/// <param name="Transaction">Transakce.</param>
		/// <param name="Adapter">Adapter.</param>
		public void ProcessVydej(LokacePohyb record, SqlCommand Command, SqlConnection Connection, SqlTransaction Transaction, SqlDataAdapter Adapter)
		{
			if (record == null || Adapter == null || Command == null || Connection == null || Transaction == null)
				throw new ApplicationException("Některý z parametrů funkce ProcessVydej není inicializovaný.");

			// kontrola na existenci lokace
			StatusOverLokace status = OverLokace(record.SKL_ID_SRC, record.LOCNCODE_SRC, Command, Connection, Transaction);
			if (status.State == STATUSOverLokace.ERROR)
				throw new Exception("Lokace '" + record.LOCNCODE_SRC.Trim() + "' neexistuje ve skladu '" + record.SKL_ID_SRC.Trim() + "'!");

			Command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND LOCNCODE=@locncode AND SKL_ID=@skl_id";
			Command.Connection = Connection;
			Command.Transaction = Transaction;

			Command.Parameters.Clear();
			Command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
			Command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
			Command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
			Command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);

			Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();
			Adapter.SelectCommand = Command;
			Adapter.Fill(data.CZMST_SkladLokace_Stav);
			// \TODO: pokud je vysledne mnozstvi 0, smazat zaznam z tabulky SKLADLOKACE_STAV?? (aby se pripadne vytvoril novy zaznam s qtyshhpd_def)
			// mnozstvi
			decimal oldQuantity = decimal.Zero, newQuantity = decimal.Zero;

			// nacteni mnozstvi z nalezeneho zaznamu
			if (data.CZMST_SkladLokace_Stav.Rows.Count > 0)
			{
				//Ziskani hodnoty mnozstvi.
				oldQuantity = data.CZMST_SkladLokace_Stav.First().QTYSHPPD;
				record.Expiration = data.CZMST_SkladLokace_Stav.First().IsEXPIRATIONNull() ? null : (DateTime?)data.CZMST_SkladLokace_Stav.First().EXPIRATION;
			}
			// nenalezen zadny material na lokaci
			else if (data.CZMST_SkladLokace_Stav.Rows.Count == 0)
			{
				throw new Exception("Materiál '" + record.ITEMNMBR.Trim() + "' nebyl nalezen na lokaci '" + record.LOCNCODE_SRC.Trim() + "'" + (string.IsNullOrEmpty(record.SKL_ID_SRC.Trim()) ? string.Empty : " ve skladu '" + record.SKL_ID_SRC.Trim() + "'"));
			}

			// nastaveni nove hodnoty mnozstvi (+kontrola preteceni).
			checked { newQuantity = oldQuantity - record.QTYSHPPD; }

			// kontrola noveho mnozstvi - jestli je < 0 (nejde vydavat do minusu).
			if (newQuantity < 0)
			{
				throw new Exception("'" + record.ITEMNMBR.Trim() + "' není možné vydávat do mínusu!\n" + "(Výsledné množství = původní - zadané) : \n" + newQuantity + " = " + oldQuantity + " - " + record.QTYSHPPD);
			}

			// aktualizace mnozstvi
			UpdateQuantity(newQuantity, record.ITEMNMBR, record.SERLTNUM, record.LOCNCODE_SRC, record.SKL_ID_SRC, Command, Connection, Transaction);

			// zjisteni dosavadniho maximalniho id z tabulky historie pohybu
			int max_id = CheckMaxID(Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, Command, Connection, Transaction);

			// vlozeni zaznamu do historie pohybu
			AddMovement(Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, record, max_id, Command, Connection, Transaction);
		}

		#endregion

		#region Private Metody


		private StatusOverLokace OverLokace(string skl_id, string locncode, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
		{
			if (command == null || connection == null || transaction == null)
				throw new ApplicationException("Některý z parametrů funkce OverLokace není inicializovaný.");

			StatusOverLokace status = new StatusOverLokace();

			command.Connection = connection;
			command.Transaction = transaction;
			command.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE skl_id=@skl_id and locncode=@locncode";

			command.Parameters.Clear();
			command.Parameters.AddWithValue("@skl_id", skl_id);
			command.Parameters.AddWithValue("@locncode", locncode);

			//Provedeni
			object rowcount = command.ExecuteScalar();

			// pokud lokace existuje, vraci OK, jinak false
			if ((rowcount != null) && (((int)rowcount) > 0))
				status.State = STATUSOverLokace.OK;
			else
				status.State = STATUSOverLokace.ERROR;

			return status;
			//return rowcount > 0 ? true : false;
		}

		/// <summary>
		/// Uprava hodnoty mnozstvi materialu na lokaci.
		/// </summary>
		/// <param name="Quantity">Nova hodnota mnozstvi.</param>
		/// <param name="IdMat">ID materialu.</param>
		/// <param name="skl_id">ID skladu.</param>
		/// <param name="Command">Prikaz.</param>
		/// <param name="Connection">Pripojeni.</param>
		/// <param name="Transaction">Transakce.</param>
		private void UpdateQuantity(decimal QTYSHPPD, string ITEMNMBR, string serltnum, string LOCNCODE, string skl_id, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
		{
			//Prikaz pro zmenu hodnoty mnozstvi materialu ve cteckove pozici.
			command.CommandText = "UPDATE " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " SET QTYSHPPD=@qtyshppd, DATECHANGE=@datechange WHERE ITEMNMBR=@itemnmbr AND LOCNCODE=@locncode AND SKL_ID=@skl_id AND SERLTNUM=@serltnum";
			command.Connection = connection;
			command.Transaction = transaction;

			command.Parameters.Clear();
			command.Parameters.AddWithValue("@qtyshppd", QTYSHPPD);
			command.Parameters.AddWithValue("@itemnmbr", ITEMNMBR);
			command.Parameters.AddWithValue("@locncode", LOCNCODE);
			command.Parameters.AddWithValue("@skl_id", skl_id);
			command.Parameters.AddWithValue("@datechange", DateTime.Now);
			command.Parameters.AddWithValue("@serltnum", serltnum);

			command.ExecuteNonQuery();
		}

		/// <summary>
		/// Zjisti maximalni index v tabulce a vrati o cislo vetsi. Potreba kvuli jedinecne identifikaci pri logovani.
		/// </summary>
		/// <param name="Table">Nazev tabulky.</param>
		/// <param name="Command">Prikaz.</param>
		/// <param name="Connection">Pripojeni.</param>
		/// <param name="Transaction">Transakce.</param>
		/// <returns>Maximalni index + 1.</returns>
		private int CheckMaxID(string tablename, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
		{
			//Kontrola, jetsli je promenna alokovana.
			if (string.IsNullOrEmpty(tablename) || command == null || connection == null || transaction == null)
				throw new ApplicationException("Některý z parametrů funkce CheckMaxID není inicializovaný.");

			//Inicializace casti prikazu.
			command.CommandText = "SELECT MAX(id) FROM " + tablename;
			command.Connection = connection;
			command.Transaction = transaction;

			command.Parameters.Clear();

			//Provedeni
			object o = command.ExecuteScalar();

			//Kontrola na null v pripade ze v tabulce nebyl zadny zaznam, jinak dosavadni +1.
			if (o is System.DBNull)
				return 1;

			return Convert.ToInt32(o) + 1;
		}

		/// <summary>
		/// Pridani zaznamu do tabulky historie pohybu.
		/// </summary>
		/// <param name="Table">Nazev tabulky</param>
		/// <param name="Record">Data pro zaznam.</param>
		/// <param name="ID">Id zaznamu.</param>
		/// <param name="Command">Prikaz</param>
		private void AddMovement(string tablename, LokacePohyb record, int ID, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
		{
			//Kontrola, jetsli jsou promenne alokovany.
			if (string.IsNullOrEmpty(tablename) || record == null || command == null)
				throw new ApplicationException("Některý z parametrů funkce AddMovement není inicializovaný.");

			command.Connection = connection;
			command.Transaction = transaction;

			#region Zalogovani pohybu

			string msg = string.Empty;

			msg += "ID,timeS,zdroj,CountEntries: " + record.id + "," + record.dateeveS + "," + record.POHYB_SRC + "," + record.CountEntries + Environment.NewLine;
			msg += "guid : " + record.guid.ToString() + Environment.NewLine;
			msg += "terminal,user: " + record.TermID + "," + record.UserID + Environment.NewLine;
			//Typ zaznamu muze byt ruzny od modulu, napr modul D=defragmentace, typy P=prijem a V=vydej
			msg += "type of record(saved to database): " + record.POHYB_TYPE.ToString() + Environment.NewLine;
			msg += "location src,skl_id_src,location dst,skl_id_dst,material,serltnum,quantity: " + record.LOCNCODE_SRC.Trim() + "," + record.SKL_ID_SRC.Trim() + "," + record.LOCNCODE_DST.Trim() + "," + record.SKL_ID_DST.Trim() + "," + record.ITEMNMBR.Trim() + "," + record.SERLTNUM.Trim() + "," + record.QTYSHPPD + Environment.NewLine;

			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Location, msg);

			#endregion

			//Pred zapsanim do db zapis do logu
			record.id = ID;

			//Inicializace prikazu.
			command.CommandText = "INSERT INTO " + tablename +
				"(id, ITEMNMBR, DOCUMENT_NUMBER, POHYB_TYPE, POHYB_SRC, SOURCE, CountEntries, QTYSHPPD, SERLTNUM, SKL_ID_SRC, SKL_ID_DST, LOCNCODE_SRC, LOCNCODE_DST, UserID, TermID, Guid, dateeveS, dateeveT) " +
				"VALUES " +
				"(@id, @itemnmbr, @document_number, @pohyb_type, @pohyb_src, @source, @countentries, @qtyshppd, @serltnum, @skl_id_src, @skl_id_dst, @locncode_src, @locncode_dst, @userid, @termid, @guid, @dateeves, @dateevet)";
			command.Parameters.Clear();
			command.Parameters.AddWithValue("@id", ID);
			command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
			command.Parameters.AddWithValue("@document_number", record.DOCUMENT_NUMBER);
			command.Parameters.AddWithValue("@pohyb_type", record.POHYB_TYPE.ToString());
			command.Parameters.AddWithValue("@pohyb_src", record.POHYB_SRC);
			command.Parameters.AddWithValue("@source", record.SOURCE);
			command.Parameters.AddWithValue("@countentries", record.CountEntries);

			// zmena v pripade vydeje na zapornou hodnotu mnozstvi.
			if (record.POHYB_TYPE == TypeOfRecord.V)
				command.Parameters.AddWithValue("@qtyshppd", -record.QTYSHPPD);
			else
				command.Parameters.AddWithValue("@qtyshppd", record.QTYSHPPD);

			command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
			command.Parameters.AddWithValue("@skl_id_src", record.SKL_ID_SRC);
			command.Parameters.AddWithValue("@skl_id_dst", record.SKL_ID_DST);
			command.Parameters.AddWithValue("@locncode_src", record.LOCNCODE_SRC);
			command.Parameters.AddWithValue("@locncode_dst", record.LOCNCODE_DST);
			command.Parameters.AddWithValue("@userid", record.UserID);
			command.Parameters.AddWithValue("@termid", record.TermID);
			command.Parameters.AddWithValue("@guid", record.guid.ToString());
			command.Parameters.AddWithValue("@dateeves", DateTime.Now);    // datum serveru record.dateeveS));
			command.Parameters.AddWithValue("@dateevet", record.dateeveT);

			command.ExecuteNonQuery();
		}

		#endregion
	}
}
