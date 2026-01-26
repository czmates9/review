using Fask.DataSets;
using Fask.Interfaces.DataSets;
using Fask.Module.ABRA.SAB.Classes;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Classes_OnlineKomunikace;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Lokace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider : Fask.Server.Interfaces.Prodej.IProdej
    {

		#region Implementovani a neco delá

		public StatusObject Prodej_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User uzivatel, Fask.DataSets.ProdejData data)
		{
			string pom = Globals_V1.LoadConfiguration();

			if (pom != "OK")
				return null;

			Guid guidDavka = Guid.Empty;

			if (data.CZMST_DEH.Count > 0)
				guidDavka = data.CZMST_DEH[0].GUID;
			else
				guidDavka = Guid.NewGuid();

			string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Prodej[0].StatusObjectsDirectory, guidDavka.ToString()));

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

			Doklad typDoklad = null;

			try
			{
				#region TypDokladu

				Fask.Rady.DS_Rady.FASK_RADYRow row = null;

				string skladID = data.CZMST_DI[0].SKL_ID.Trim();
				string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
				string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

				try
				{
					Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
					row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.Module.ABRA.SAB", " Rada,dotaženi", ex);
				}

				typDoklad = new Doklad(row);


				#endregion

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);

			}

			try
			{

				so.Write("connection");

				bool allowInsertData = true;

				using (connect = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					connect.Open();

					int? result = Database.Prodej.CZMST_DI_DavkaExist(connect, davka.ID);

					if (result != null && ((int)result) > 0)
					{
						allowInsertData = false;
					}

					if (allowInsertData)
					{
						so.Write("insert to db");
						iTrans1 = connect.BeginTransaction();



						Database.Prodej.Update_CZMST_DI(
							data.CZMST_DI.Select(null, null, DataViewRowState.Added),
							connect,
							iTrans1
							);

						#region Lok. Mech

						#region Lokace
						// ulozeni do lokacniho mechanismu, pokud je zapnuty ...

						// v davce je jen jeden typ dokladu, urceni, zdali je lokacni mechanismus zapnuty podle tohoto typu dokladu
						if (data.CZMST_DI.Count > 0)
						{
							// nacteni dat typu dokladu
							Fask.DataSets.TypDokladu dstypdokladu = Database.Prodej.Get_CZMST_DI(data.CZMST_DI.First().DOC_ID, data.CZMST_DI.First().DOC_ID2);

							if (dstypdokladu != null && dstypdokladu.CZMST092.Count > 0 && !dstypdokladu.CZMST092.First().Iscfg_lok_mechNull() && dstypdokladu.CZMST092.First().cfg_lok_mech > 0)
							{
								Fask.DataSets.TypDokladu.CZMST092Row _typdokladu = dstypdokladu.CZMST092.First();

								foreach (Fask.DataSets.ProdejData.CZMST_DIRow dirow in data.CZMST_DI)
								{
									string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
									Fask.Server.Interfaces.Lokace.TypeOfRecord recordType = (Fask.Server.Interfaces.Lokace.TypeOfRecord)Enum.Parse(typeof(Fask.Server.Interfaces.Lokace.TypeOfRecord), pohyb_type, true);

									int guidcount = Database.Lokace.Count_STAVPOHYB(connect, dirow.guid);

									// \TODO: Co kdyz je jiny recordtype??
									if (
										((guidcount % 2 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) ||
										((guidcount % 2 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.V) ||
										((guidcount % 4 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.D)
										)
									{
										DateTime dtnow = DateTime.Now;
										Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
										pohybrow.ITEMNMBR = dirow.ITEMNMBR;
										pohybrow.DOCUMENT_NUMBER = _typdokladu.doc_id;      // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS), pokud prodej, tak doc_id
										pohybrow.POHYB_TYPE = recordType;
										pohybrow.POHYB_SRC = "R";
										pohybrow.SOURCE = "S";      // doplneni ze serveru ...
										pohybrow.QTYSHPPD = dirow.QTYSHPPD;
										pohybrow.SERLTNUM = dirow.SERLTNUM;
										pohybrow.SKL_ID_SRC = dirow.IsSKL_IDNull() ? string.Empty : dirow.SKL_ID;
										pohybrow.SKL_ID_DST = dirow.IsSKL_ID_DESTNull() ? string.Empty : dirow.SKL_ID_DEST;
										pohybrow.LOCNCODE_SRC = dirow.IsLOCNCODENull() ? string.Empty : dirow.LOCNCODE;
										pohybrow.LOCNCODE_DST = dirow.IsLOCNCODEDESTNull() ? string.Empty : dirow.LOCNCODEDEST;
										pohybrow.UserID = dirow.USER_ID;
										pohybrow.TermID = terminal.ID;
										pohybrow.guid = dirow.guid;
										pohybrow.Expiration = dirow.IsEXPIRACENull() ? (DateTime?)null : dirow.EXPIRACE;
										pohybrow.ITEMDESC = string.Empty;   // dotahnout nazev??
										pohybrow.CountEntries = dirow.CountEntries;
										pohybrow.dateeveS = dtnow;
										if (dirow.IsDATEDONENull() || dirow.IsTIMEDONENull())
											pohybrow.dateeveS = dtnow;
										else
											pohybrow.dateeveT = DateTime.ParseExact(dirow.DATEDONE + " " + dirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

										using (var com = new SqlCommand())
										using (var ada = new SqlDataAdapter())
										{
											Lokace_MoveItem(pohybrow, com, connect, iTrans1, ada);
										}
									}
								}
							}
						}

						#endregion
						#endregion

						if (iTrans1 != null)
							iTrans1.Commit();
					}
				}
			}
			catch (Exception ex)
			{
				if (iTrans1 != null)
					iTrans1.Rollback();

				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}

			#region Importu to IS ABRA

			try
			{
				if (typDoklad.Import_Doklad_IS)
				{
					Fask.DataSets.ProdejData dt_di = null;

					//TODO: Grupovat??
					//if (Globals_V1.Konfigurace.Prodej[0].GrupujDataImport)
					//	dt_di = Database.Prodej2.GETDATA_CZMSTDI_DS_GroupBy_CountEntries(davka.ID);
					//else
					//	dt_di = Database.Prodej2.GETDATA_CZMSTDI_DS(davka.ID);



					if (dt_di.CZMST_DI.Count > 0)
					{
						so.Write("Generovani Dokladu");
						string vysledek = this.Prodej_Import_to_IS(davka, dt_di, uzivatel);

						if (vysledek != "OK")
						{
							so.StatusText = vysledek;
							so.Exception = true;
							so.Write();
							return so;
						}
					}
				}
			}
			catch (Exception exImport)
			{
				so.Exception = true;
				so.Write(exImport.Message);
				return so;
			}

			#endregion

			so.SetOK();
			so.Write();

			return so;
		}

		public Fask.Server.Interfaces.Classes.StatusOverLokace Prodej_Online_OverLokace(string itemnmbr, string serltnum, DateTime? Expirace, string locncode, string skl_id, decimal qtyshppd, string doc_id, Fask.Server.Interfaces.Classes.TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)
		{
			return Lokace_OverLokace(itemnmbr, serltnum, locncode, skl_id, qtyshppd, doc_id, locationType, recordType);
		}

		public Fask.Server.Interfaces.DataSets.Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty)
		{
			return Lokace_ShowMaterial(itemnmbr, serltnum, skl_id, null, ShowEmpty);
		}

		public string Prodej_Import_to_IS(Davka davka, ProdejData data, User uzivatel)
		{
			#region TypDokladu

			Classes.TypDokladu td = Classes.TypDokladu.Unknow;
			Fask.Rady.DS_Rady.FASK_RADYRow row = null;
			Doklad typDoklad = null;
			Fask.Rady.NumericalSeries NS = null;

			string skladID = data.CZMST_DI[0].SKL_ID.Trim();
			string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

			try
			{
				Globals_V1.LoadConfiguration();
				NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);

				td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), row.Modul_Funkce, true);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.Module.ABRA.SAB", " Rada,dotaženi pro DOC_ID:  " + dokladID, ex);

				if (NS == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.Module.ABRA.SAB", " Rada,objekt NS", " Objekt NS is NULL");

				if (row == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.Module.ABRA.SAB", " Rada,objekt row", " Objekt row is NULL");

				if (row.Modul_Funkce == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.Module.ABRA.SAB", " Rada,objekt Modul_Funkce", " Objekt Modul_Funkce is NULL");

			}

			typDoklad = new Doklad(row);


			#endregion

			string Status = "OK";

			switch (td)
			{
				//case Classes.TypDokladu.vyd:
				//	Status = Prodej_Import_Vyd_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.pri:
				//	Status = Prodej_Import_Pri_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.pro:
				//	Status = Prodej_Import_Pro_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objv:
				//	Status = Prodej_Import_Objv_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objvcm:
				//	Status = Prodej_Import_Objvcm_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objvm:
				//	Status = Prodej_Import_Objvm_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objbezodb:
				//	Status = Prodej_Import_Objbezodb_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objp:
				//	Status = Prodej_Import_Objp_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objpcm:
				//	Status = Prodej_Import_Objpcm_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.objpm:
				//	Status = Prodej_Import_Objpm_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				//case Classes.TypDokladu.expedice:
				//	Status = Prodej_Import_Expedice_Pohoda(filename, data, typDoklad, uzivatel);
				//	break;
				case Classes.TypDokladu.pre:
					Status = Classes.ABRA.Prodej_Import_Pre_ABRA(data, typDoklad, uzivatel);
					break;
				default:
					break;
			}

			return Status;

		}

		#endregion

		#region Implementovani, ale nic nedela

		public bool Prodej_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
		{
			return true;
		}

		#endregion


		#region NEimplementovane vubec...

		public StatusOverPohyb Over_Pohyb(ProdejPohyb prodejPohyb)
        {
            throw new NotImplementedException();
        }

		public Odberatele Prodej_GetDodavatele_External(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod)
        {
            throw new NotImplementedException();
        }

        public Odberatele Prodej_GetDodavatele_ExternalByICO(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, string ICO)
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

        public STATUS GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
        {
            throw new NotImplementedException();
        }

		public StatusResult Prodej_ProcessTiskSoupis(ProdejData data, out DSValues dataHeader, out List<DSValues> dataRowList, out DSValues dataFooter)
		{
			throw new NotImplementedException();
		}

        #endregion



    }
}
