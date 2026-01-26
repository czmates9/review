using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Classes_OnlineKomunikace;
using Fask.DataSets;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Lokace;
using Fask.Interfaces.DataSets;

namespace Fask.Module.ABRA.CarpServise
{
    public partial class Provider : Fask.Server.Interfaces.Prodej.IProdej
    {

		#region NEimplementovane vubec...

		public StatusOverPohyb Over_Pohyb(ProdejPohyb prodejPohyb)
		{
			throw new NotImplementedException();
		}

		public string Prodej_Import_to_IS(Davka davka, ProdejData data, User uzivatel)
		{
			throw new NotImplementedException();
		}

		public Odberatele Prodej_GetDodavatele_External(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod)
		{
			throw new NotImplementedException();
		}

		public Odberatele Prodej_GetDodavatele_ExternalByICO(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad, string ICO)
		{
			throw new NotImplementedException();
		}

		public StatusInfo Prodej_Disponibilita(Disponibilita disponibilita)
		{
			throw new NotImplementedException();
		}

		public VystupniObjekt Online_UniverzalnyDotazNaCokoliv(VstupniObjekt ObjektIN)
		{
			throw new NotImplementedException();
		}

		#endregion


		public STATUS GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
        {
            throw new NotImplementedException();
        }

        public bool Prodej_AfterProcessedAction(Davka davka)
        {
            throw new NotImplementedException();
        }

        public Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty)
        {
            throw new NotImplementedException();
        }

        public StatusOverLokace Prodej_Online_OverLokace(string itemnmbr, string serltnum, DateTime? Expirace, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, TypeOfRecord recordType)
        {
            throw new NotImplementedException();
        }

        public StatusObject Prodej_Process(Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, User uzivatel, ProdejData data)
        {
			Globals_V1.LoadConfiguration();
			string guidDavka = string.Empty;
			if (data.CZMST_DEH.Count > 0)
				guidDavka = data.CZMST_DEH[0].GUID.ToString();
			else
				guidDavka = Guid.NewGuid().ToString();
			string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Prodej[0].StatusObjectsDirectory, guidDavka));

			StatusObject so = new StatusObject(filePath);

			//zjistit zda soubor s danym guid existuje
			if (File.Exists(filePath))
			{ //soubor jiz existuje
				so = StatusObject.Load(filePath);
				if (!so.Exception)
					return so;
			}

			SqlConnection connect = null;
			SqlTransaction iTrans1 = null;

			string pom = string.Empty;
			string idCizimena = string.Empty;

			try
			{
				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return null;

				so.Write("connection");
				connect = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				bool allowInsertData = true;
				connect.Open();

				SqlCommand xselect = new SqlCommand("Select Count(*) as number from " + Fask.SQL.Constants.Common.TABLE_CZMST_DI + " where countentries=" + davka.ID, connect);
				object result = xselect.ExecuteScalar();
				if (result != null && ((int)result) > 0)
				{
					allowInsertData = false;
				}

				if (allowInsertData)
				{
					//Probehne ulozeni dat
					so.Write("insert to db");
					//Fask.DataSets.ProdejData prodejData = new Fask.DataSets.ProdejData();

					iTrans1 = connect.BeginTransaction();

					//SQL_Datasets.ProdejTableAdapters.CZMST_DITableAdapter dita = new Fask.ModuleSql.SQL_Datasets.ProdejTableAdapters.CZMST_DITableAdapter();
					SQL_Datasets.ProdejTableAdapters.CZMST_DITableAdapter dita = new SQL_Datasets.ProdejTableAdapters.CZMST_DITableAdapter();
					dita.Connection = connect;
					dita.Transaction = iTrans1;
					dita.Update(data.CZMST_DI.Select(null, null, DataViewRowState.Added));
					#region Lokace
					// ulozeni do lokacniho mechanismu, pokud je zapnuty ...

					// v davce je jen jeden typ dokladu, urceni, zdali je lokacni mechanismus zapnuty podle tohoto typu dokladu
					if (data.CZMST_DI.Count > 0)
					{
						// nacteni dat typu dokladu
						string commandText092 = "select * from czmst092 where doc_id=@doc_id and doc_id2=@doc_id2";
						SqlCommand command092 = new SqlCommand(commandText092, connect, iTrans1);
						command092.Parameters.Add(new SqlParameter("@doc_id", data.CZMST_DI.First().DOC_ID));
						command092.Parameters.Add(new SqlParameter("@doc_id2", data.CZMST_DI.First().DOC_ID2));
						SqlDataAdapter adapter = new SqlDataAdapter();
						adapter.SelectCommand = command092;
						Fask.DataSets.TypDokladu dstypdokladu = new Fask.DataSets.TypDokladu();
						adapter.Fill(dstypdokladu, dstypdokladu.CZMST092.TableName);

					}

					#endregion

					if (iTrans1 != null)
						iTrans1.Commit();
				}
			}
			catch (Exception ex)
			{
				if (iTrans1 != null)
					iTrans1.Rollback();

				so.Exception = true;
				so.Write(ex.Message);

				throw ex;
			}
			finally
			{
				try
				{
					if (connect != null && (connect.State == System.Data.ConnectionState.Open))
						connect.Close();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			so.SetOK();
			so.Write();

			return so;
		}

        public StatusResult Prodej_ProcessTiskSoupis(ProdejData data, out DSValues dataHeader, out List<DSValues> dataRowList, out DSValues dataFooter)
        {
			Fask.Server.Interfaces.Classes.StatusResult status = new StatusResult();
			status.Status = StatusResultEnum.OK;

			dataHeader = new Fask.Server.Interfaces.DataSets.DSValues();
			dataRowList = new List<Fask.Server.Interfaces.DataSets.DSValues>();
			dataFooter = new Fask.Server.Interfaces.DataSets.DSValues();

			if (data == null)
			{
				data = new Fask.DataSets.ProdejData();
				data.ReadXml(Path.Combine(MyPath.Path.ConfigDirectory, "plprocessdata.xml"));
			}

			dataHeader.Values.AddValuesRow(data.CZMST_DI.CountEntriesColumn.ColumnName.Trim(), data.CZMST_DI[0].CountEntries.ToString());

			foreach (var item in data.CZMST_DI)
			{
				Fask.Server.Interfaces.DataSets.DSValues dataRowValues = new Fask.Server.Interfaces.DataSets.DSValues();
				DotazeniHodnotParams dhp = new DotazeniHodnotParams();
				dhp.itemnmbr = item.ITEMNMBR.Trim();
				dhp.czcarkod = item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod.Trim();
				dhp.vnditnum = item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM.Trim();
				dhp.serltnum = item.SERLTNUM.Trim();
				DotazeniHodnotDo_DI(dhp, item);


				foreach (DataColumn col in data.CZMST_DI.Columns)
				{

					if (!item.IsNull(col))
						dataRowValues.Values.AddValuesRow(col.ColumnName.Trim(), item[col].ToString());
				}

				// dotazeni nazvu ...
				if (item.IsITEMDESCNull())
				{
					string itemdesc = DotazeniNazvu(item.ITEMNMBR.Trim());
					dataRowValues.Values.AddValuesRow("ITEMDESC", itemdesc);
				}
				dataRowList.Add(dataRowValues);
			}


			return status;
		}

		#region Metody k tisku soupisu

		public struct DotazeniHodnotParams
		{
			public string itemnmbr;
			public string itemdesc;
			public string serltnum;
			public string vnditnum;
			public string czcarkod;
		}

		private void DotazeniHodnotDo_DI(DotazeniHodnotParams dhp, Fask.DataSets.ProdejData.CZMST_DIRow dirOUT)
		{
			Globals_V1.LoadConfiguration();
			SqlConnection connect = null;

			connect = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

			if (String.IsNullOrEmpty(dhp.itemnmbr))
			{

				if (string.IsNullOrEmpty(dhp.serltnum))
				{

					string commandText095 = "Select * from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " where CZ_CarKod=@CZ_CarKod or VNDITNUM=@vnditnum";
					SqlCommand command095 = new SqlCommand(commandText095, connect);
					command095.Parameters.Add(new SqlParameter("@CZ_CarKod", dhp.czcarkod ?? string.Empty));
					command095.Parameters.Add(new SqlParameter("@vnditnum", dhp.vnditnum ?? string.Empty));
					SqlDataAdapter adapter095 = new SqlDataAdapter();
					adapter095.SelectCommand = command095;
					Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
					adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

					dirOUT.ITEMNMBR = dsZbozi.FASK_ZASOBY[0].ITEMNMBR.Trim();
					dirOUT.ITEMCODE = dsZbozi.FASK_ZASOBY[0].ITEMCODE.Trim();
					dirOUT.SERLTNUM = string.Empty; 

				}
				else //if (!string.IsNullOrEmpty(dhp.serltnum))
				{
					string commandTextSTAV2 = "SELECT * FROM CZMST_SkladLokace_Stav where SERLTNUM=@SERLTNUM";
					SqlCommand commandSTAV2 = new SqlCommand(commandTextSTAV2, connect);
					commandSTAV2.Parameters.Add(new SqlParameter("@SERLTNUM", dhp.serltnum));
					SqlDataAdapter adapterSTAV2 = new SqlDataAdapter();
					adapterSTAV2.SelectCommand = commandSTAV2;
					DataSet ds = new DataSet();
					adapterSTAV2.Fill(ds);

					string ITEMNMBRtmp = (string)ds.Tables[0].Rows[0]["ITEMNMBR"];
					dirOUT.ITEMNMBR = ITEMNMBRtmp.Trim();

					string ITEMCODEtmp = (string)ds.Tables[0].Rows[0]["ITEMCODE"];
					dirOUT.ITEMCODE = ITEMCODEtmp.Trim();

				}
			}
			else
			{
				dirOUT.ITEMNMBR = dhp.itemnmbr;
				dirOUT.SERLTNUM = dhp.serltnum ?? string.Empty;

			}
		}

		private string DotazeniNazvu(string itemnmbr)
		{
			Globals_V1.LoadConfiguration();
			SqlConnection connect = null;
			connect = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			string commandText095 = "Select * from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " where ITEMNMBR=@itemnmbr";
			SqlCommand command095 = new SqlCommand(commandText095, connect);
			command095.Parameters.Add(new SqlParameter("@itemnmbr", itemnmbr ?? string.Empty));
			SqlDataAdapter adapter095 = new SqlDataAdapter();
			adapter095.SelectCommand = command095;
			Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
			adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

			if (dsZbozi.FASK_ZASOBY != null && dsZbozi.FASK_ZASOBY.Count > 0)
				return dsZbozi.FASK_ZASOBY[0].ITEMDESC.Trim();
			else
				return string.Empty;
		}

        public Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
