using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL.Classes
{
    public class Pohoda
    {
        public class Sazby
        {

            private const string none = "none";
            private const string low = "low";
            private const string high = "high";
            private const string third = "third";
            private const string historyLow = "historyLow";
            private const string historyHigh = "historyHigh";
            private const string historyThird = "historyThird";
            //private const string historyLow = "historyLow";
            //private const string historyHigh = "historyHigh";
            //private const string historyThird = "historyThird";


            public static string GetName(int sazba)
            {
                switch (sazba)
                {
                    case 0:
                        return none;
                    case 1:
                        return low;
                    case 2:
                        return high;
                    case 3:
                        return third;
                    case 4:
                        return historyLow;
                    case 5:
                        return historyHigh;
                    case 6:
                        return historyThird;
                    case 101:
                        return historyLow;
                    case 102:
                        return historyHigh;
                    case 103:
                        return historyThird;
					default:
						{
							Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Sazba DPH nenalezena výchozí HIGH. Obsahuje :" + sazba.ToString());
							return high;
						}
                }
            }
        }

        //public enum Sazby
        //{
        //    none = 0,
        //    low = 1,
        //    high = 2,
        //    third = 3,
        //    historyLow = 4,
        //    historyHigh = 5,
        //    historyThird = 6
        //    // historyLow = 101
        //    // historyHigh = 102
        //    // historyThird = 103
        //}

		public enum TypAgendy
		{
			Inventura,
			Prijem,
			Vydej,
			Prodej

		}

        public static Sazby FromString(string s)
        {
            return (Sazby)Enum.Parse(typeof(Sazby), s, true);
        }

        /// <summary>
        /// Metoda ktera určite jaky typ sledovani se použije anebo nepoužije
        /// </summary>
        /// <param name="relskzvc">Parameter ktery nese informaci o typu sledovani</param>
        /// <param name="dotahovat">Parameter ktery určuje zda použit zadany typ sledovani</param>
        /// <returns></returns>
        public static byte GetSerNumTrack(int? relskzvc, bool dotahovat)
        {

            if (!dotahovat)
                return 0;

            byte sernumtrack = 0; //defaultne jen na mnozstvi
            if (relskzvc.HasValue)
            {
                switch (relskzvc)
                {
                    case 2: // na sarze
                        sernumtrack = (byte)(Globals_V1.Konfigurace.Sdilene[0].EvidenceSarzi ? relskzvc.Value : 0);
                        break;
                    case 1: // na Vyr č. anebo seriove čisla (Zale6i podle terminologie)
                        sernumtrack = (byte)(Globals_V1.Konfigurace.Sdilene[0].EvidenceVyrobnichCisel ? relskzvc.Value : 0);
                        break;
                    case 0:
                    default:
                        sernumtrack = 0; // namnozstvi
                        break;
                }
            }
            return sernumtrack;
        }

		/// <summary>
		/// Tady se dotahuje z FASK_ZASOBY_PARAMS
		/// </summary>
		/// <param name="RelSKzVC"></param>
		/// <param name="Row"></param>
		/// <param name="typAgendy"></param>
		/// <returns></returns>
		public static byte GetPriznakSledovani(int? RelSKzVC, Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow Row, TypAgendy typAgendy)
		{

			byte _sernumtrack = 0; //defaultne jen na mnozstvi

			if (RelSKzVC.HasValue)
			{
				_sernumtrack = (byte)RelSKzVC.Value;
			}


			if (_sernumtrack == 0)
			{

				if (Row == null)
					return _sernumtrack;

				if (!Row.IsVPrFXTSNull() && Row.VPrFXTS)
				{
					_sernumtrack = Classes.Pohoda.GetSerNumTrack((int?)Row.RefVPrFXTS, true);
				}
				else
				{
					switch (typAgendy)
					{
						case TypAgendy.Inventura:
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row.IsRefVPrFITSNull()) ? (int?)null : ((int?)Row.RefVPrFITS), Row.IsVPrFITSNull() ? false : Row.VPrFITS);
							break;
						case TypAgendy.Prijem:
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row.IsRefVPrFPTSNull()) ? (int?)null : ((int?)Row.RefVPrFPTS), Row.IsVPrFPTSNull() ? false : Row.VPrFPTS);
							break;
						case TypAgendy.Vydej:
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row.IsRefVPrFVTSNull()) ? (int?)null : ((int?)Row.RefVPrFVTS), Row.IsVPrFVTSNull() ? false : Row.VPrFVTS);
							break;
						case TypAgendy.Prodej:
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row.IsRefVPrFDTSNull()) ? (int?)null : ((int?)Row.RefVPrFDTS), Row.IsVPrFDTSNull() ? false : Row.VPrFDTS);
							break;
						default:
							break;
					}

				}
			}

			return _sernumtrack;
		}

		/// <summary>
		/// Tady se dotahuje z SKz
		/// </summary>
		/// <param name="RelSKzVC"></param>
		/// <param name="Row"></param>
		/// <returns></returns>
		public static byte GetPriznakSledovani(int? RelSKzVC, System.Data.DataRow Row, TypAgendy typAgendy)
		{

			byte _sernumtrack = 0; //defaultne jen na mnozstvi

			if (RelSKzVC.HasValue)
			{
				_sernumtrack = (byte)RelSKzVC.Value;
			}


			if (_sernumtrack == 0)
			{
				if (Row == null)
					return _sernumtrack;

				if(typAgendy == TypAgendy.Inventura)
				{
					#region Inventura

					if (Row is Datasets.Inventura.SKzInvRow)
					{
						if (!((Datasets.Inventura.SKzInvRow)Row).IsSKz_VPrFXTSNull() && ((Datasets.Inventura.SKzInvRow)Row).SKz_VPrFXTS)
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.Inventura.SKzInvRow)Row).IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)((Datasets.Inventura.SKzInvRow)Row).SKz_RefVPrFXTS - 1), ((Datasets.Inventura.SKzInvRow)Row).IsSKz_VPrFXTSNull() ? false : ((Datasets.Inventura.SKzInvRow)Row).SKz_VPrFXTS);
						}
						else
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.Inventura.SKzInvRow)Row).IsSKz_RefVPrFITSNull()) ? (int?)null : ((int?)((Datasets.Inventura.SKzInvRow)Row).SKz_RefVPrFITS - 1), ((Datasets.Inventura.SKzInvRow)Row).IsSKz_VPrFITSNull() ? false : ((Datasets.Inventura.SKzInvRow)Row).SKz_VPrFITS);
						}
					}
					else
					{
						return _sernumtrack;
					}

					#endregion

				}
				else if(typAgendy == TypAgendy.Prijem)
				{
					if (Row is Datasets.DatabasePohoda.SKzRow)
					{
						if (!((Datasets.DatabasePohoda.SKzRow)Row).IsVPrFXTSNull() && ((Datasets.DatabasePohoda.SKzRow)Row).VPrFXTS)
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.DatabasePohoda.SKzRow)Row).IsRefVPrFXTSNull()) ? (int?)null : ((int?)((Datasets.DatabasePohoda.SKzRow)Row).RefVPrFXTS - 1), ((Datasets.DatabasePohoda.SKzRow)Row).IsVPrFXTSNull() ? false : ((Datasets.DatabasePohoda.SKzRow)Row).VPrFXTS);
						}
						else
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.DatabasePohoda.SKzRow)Row).IsRefVPrFPTSNull()) ? (int?)null : ((int?)((Datasets.DatabasePohoda.SKzRow)Row).RefVPrFPTS - 1), ((Datasets.DatabasePohoda.SKzRow)Row).IsVPrFPTSNull() ? false : ((Datasets.DatabasePohoda.SKzRow)Row).VPrFPTS);
						}
					}
					else
					{
						return _sernumtrack;
					}
				}
				else if(typAgendy == TypAgendy.Vydej)
				{
					#region Vydejka

					if (Row is Datasets.DatabasePohoda.SKzRow)
					{
						if (!((Datasets.DatabasePohoda.SKzRow)Row).IsVPrFXTSNull() && ((Datasets.DatabasePohoda.SKzRow)Row).VPrFXTS)
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.DatabasePohoda.SKzRow)Row).IsRefVPrFXTSNull()) ? (int?)null : ((int?)((Datasets.DatabasePohoda.SKzRow)Row).RefVPrFXTS - 1), ((Datasets.DatabasePohoda.SKzRow)Row).IsVPrFXTSNull() ? false : ((Datasets.DatabasePohoda.SKzRow)Row).VPrFXTS);
						}
						else
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.DatabasePohoda.SKzRow)Row).IsRefVPrFVTSNull()) ? (int?)null : ((int?)((Datasets.DatabasePohoda.SKzRow)Row).RefVPrFVTS - 1), ((Datasets.DatabasePohoda.SKzRow)Row).IsVPrFVTSNull() ? false : ((Datasets.DatabasePohoda.SKzRow)Row).VPrFVTS);
						}
					}

					#endregion

					#region Faktura do vydejky

					else if (Row is Datasets.DatabasePohoda.FakturaCisloRow)
					{
						if (!((Datasets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_VPrFXTSNull() && ((Datasets.DatabasePohoda.FakturaCisloRow)Row).SKz_VPrFXTS)
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)((Datasets.DatabasePohoda.FakturaCisloRow)Row).SKz_RefVPrFXTS - 1), ((Datasets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_VPrFXTSNull() ? false : ((Datasets.DatabasePohoda.FakturaCisloRow)Row).SKz_VPrFXTS);
						}
						else
						{
							_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Datasets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_RefVPrFVTSNull()) ? (int?)null : ((int?)((Datasets.DatabasePohoda.FakturaCisloRow)Row).SKz_RefVPrFVTS - 1), ((Datasets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_VPrFVTSNull() ? false : ((Datasets.DatabasePohoda.FakturaCisloRow)Row).SKz_VPrFVTS);
						}
					}

					else
					{
						return _sernumtrack;
					}

					#endregion

				}
				else 
				{
					return _sernumtrack;
				}
			}

			return _sernumtrack;
		}

		/// <summary>
		/// Tady se dotahuje z SKz
		/// </summary>
		/// <param name="RelSKzVC"></param>
		/// <param name="Row"></param>
		/// <returns></returns>
		public static byte GetPriznakSledovani_Zbozi(int? RelSKzVC, Datasets.DatabasePohoda.SKzRow Row)
		{

			byte _sernumtrack = 0; //defaultne jen na mnozstvi

			if (RelSKzVC.HasValue)
			{
				_sernumtrack = (byte)RelSKzVC.Value;
			}


			if (_sernumtrack == 0)
			{
				if (Row == null)
					return _sernumtrack;

				if (!Row.IsVPrFXTSNull() && Row.VPrFXTS)
				{
					_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (Row.IsRefVPrFXTSNull()) ? (int?)null : ((int?)Row.RefVPrFXTS - 1), Row.IsVPrFXTSNull() ? false : Row.VPrFXTS);
				}
				else
				{
					_sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (Row.IsRefVPrFDTSNull()) ? (int?)null : ((int?)Row.RefVPrFDTS - 1), Row.IsVPrFDTSNull() ? false : Row.VPrFDTS);
				}
			}

			return _sernumtrack;
		}

		public static bool Update_AttributeToSN_Prevod(string Cislo)
		{
            try
            {
				Globals_V1.LoadConfiguration();

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{

					Datasets.DatabasePohoda.SKMPPol_SarzeDataTable dt = Database.Pohoda.getSKMP_ByCislo(Cislo);

					if (dt != null && dt.Count > 0)
					{

						System.Data.OleDb.OleDbTransaction transPOH = null;
						System.Data.OleDb.OleDbConnection connPOH = null;

						try
						{
							using (connPOH = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB))
							{
								connPOH.Open();
								transPOH = connPOH.BeginTransaction();

								foreach (Datasets.DatabasePohoda.SKMPPol_SarzeRow pol in dt)
								{
									if (!pol.IsVPrSarzeKSNNull() && !string.IsNullOrEmpty(pol.VPrSarzeKSN))
									{
										Database.Pohoda.Update_SKzVC(connPOH, transPOH, pol.ID, pol.VPrSarzeKSN);
									}

									if (!pol.IsVPrExspiraceKSNNull())
									{
										Database.Pohoda.Update_SKzVC(connPOH, transPOH, pol.ID,  pol.VPrExspiraceKSN);
									}
								}

								if (transPOH != null)
									transPOH.Commit();
							}

						}
						catch (Exception ex)
						{
							if (transPOH != null)
								transPOH.Rollback();

							throw ex;
						}

					}
				}

				return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
			

		}
    }

	/// <summary>
	/// Jedna se o enum ktery obsahuje všechny agenty pro tisk z pohody
	/// </summary>
	public enum printAgendaType
	{
		adresar,
		interni_doklady,
		vydane_nabidky,
		prijate_nabidky,
		ostatni_pohledavky,
		ostatni_zavazky,
		pokladna,
		vydane_poptavky,
		prijate_poptavky,
		prevod,
		prijemky,
		prijate_faktury,
		prijate_objednavky,
		prijate_zalohove_faktury,
		prodejky,
		vydane_faktury,
		vydane_objednavky,
		vydane_zalohove_faktury,
		vydejky,
		vyroba,
		zakazky,
		zasoby,
	}

}
