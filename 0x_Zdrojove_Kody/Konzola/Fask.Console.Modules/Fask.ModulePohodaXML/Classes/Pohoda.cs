using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;

namespace Fask.ModulePohodaXML.Classes
{
    public class Pohoda
    {
        public enum Sazby
        {
            none = 0,
            low = 1,
            high = 2,
            third = 3,
            historyLow = 4,
            historyHigh = 5,
            historyThird = 6
        }

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

        //public static byte GetSerNumTrack(int? relskzvc)
        //{
        //    byte sernumtrack = 0; //defaultne jen na mnozstvi
        //    if (relskzvc.HasValue)
        //    {
        //        switch (relskzvc)
        //        {
        //            case 2: // na sarze
        //                sernumtrack = (byte)(Properties.Settings.Default.EvidenceSarzi ? relskzvc.Value : 0);
        //                break;
        //            case 1: // na sarze
        //                sernumtrack = (byte)(Properties.Settings.Default.EvidenceVyrobnichCisel ? relskzvc.Value : 0);
        //                break;
        //            case 0:
        //            default:
        //                sernumtrack = 0; // namnozstvi
        //                break;
        //        }
        //    }
        //    return sernumtrack;
        //}

        private static byte GetSerNumTrack(int? relskzvc, bool dotahovat)
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
                    case 1: // na Vyr č. anebo seriove čisla (Zaleži podle terminologie)
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
        public static byte GetPriznakSledovani(int? RelSKzVC, System.Data.DataRow Row, TypAgendy typAgendy)
        {

            byte _sernumtrack = 0; //defaultne jen na mnozstvi

            if (RelSKzVC.HasValue)
            {
                _sernumtrack = (byte)RelSKzVC.Value;
            }


            if (_sernumtrack == 0)
            {

                if (Row is Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)
                {
                    if (!((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsVPrFXTSNull() && ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).VPrFXTS)
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsRefVPrFXTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_RefVPrFXTS), ((Pohoda_DataSets.Inventura.SKzInvRow)Row).IsSKz_VPrFXTSNull() ? false : ((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_VPrFXTS);
                    }
                    else
                    {
                        switch (typAgendy)
                        {
                            case TypAgendy.Inventura:
                                _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsRefVPrFITSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).RefVPrFITS), ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsVPrFITSNull() ? false : ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).VPrFITS);
                                break;
                            case TypAgendy.Prijem:
                                _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsRefVPrFPTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).RefVPrFPTS), ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsVPrFPTSNull() ? false : ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).VPrFPTS);
                                break;
                            case TypAgendy.Vydej:
                                _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsRefVPrFVTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).RefVPrFVTS), ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsVPrFVTSNull() ? false : ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).VPrFVTS);
                                break;
                            case TypAgendy.Prodej:
                                _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsRefVPrFDTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).RefVPrFDTS ), ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).IsVPrFDTSNull() ? false : ((Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow)Row).VPrFDTS);
                                break;
                            default:
                                break;
                        }
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
        public static byte GetPriznakSledovani(int? RelSKzVC, System.Data.DataRow Row)
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

                #region Inventura
                if (Row is Pohoda_DataSets.Inventura.SKzInvRow)
                {
                    if (!((Pohoda_DataSets.Inventura.SKzInvRow)Row).IsSKz_VPrFXTSNull() && ((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_VPrFXTS)
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Inventura.SKzInvRow)Row).IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_RefVPrFXTS - 1), ((Pohoda_DataSets.Inventura.SKzInvRow)Row).IsSKz_VPrFXTSNull() ? false : ((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_VPrFXTS);
                    }
                    else
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.Inventura.SKzInvRow)Row).IsSKz_RefVPrFITSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_RefVPrFITS - 1), ((Pohoda_DataSets.Inventura.SKzInvRow)Row).IsSKz_VPrFITSNull() ? false : ((Pohoda_DataSets.Inventura.SKzInvRow)Row).SKz_VPrFITS);
                    }
                }
                #endregion
                #region Vydejka
                else if (Row is Pohoda_DataSets.DatabasePohoda.SKzRow)
                {
                    if (!((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).IsVPrFXTSNull() && ((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).VPrFXTS)
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).IsRefVPrFXTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).RefVPrFXTS - 1), ((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).IsVPrFXTSNull() ? false : ((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).VPrFXTS);
                    }
                    else
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).IsRefVPrFVTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).RefVPrFVTS - 1), ((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).IsVPrFVTSNull() ? false : ((Pohoda_DataSets.DatabasePohoda.SKzRow)Row).VPrFVTS);
                    }
                }
                #endregion
                #region Faktura do vydejky
                else if (Row is Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)
                {
                    if (!((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_VPrFXTSNull() && ((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).SKz_VPrFXTS)
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).SKz_RefVPrFXTS - 1), ((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_VPrFXTSNull() ? false : ((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).SKz_VPrFXTS);
                    }
                    else
                    {
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_RefVPrFVTSNull()) ? (int?)null : ((int?)((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).SKz_RefVPrFVTS - 1), ((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).IsSKz_VPrFVTSNull() ? false : ((Pohoda_DataSets.DatabasePohoda.FakturaCisloRow)Row).SKz_VPrFVTS);
                    }
                }
                #endregion
            }

            return _sernumtrack;
        }

        /// <summary>
        /// Tady se dotahuje z SKz
        /// </summary>
        /// <param name="RelSKzVC"></param>
        /// <param name="Row"></param>
        /// <returns></returns>
        public static byte GetPriznakSledovani_Zbozi(int? RelSKzVC, Pohoda_DataSets.DatabasePohoda.SKzRow Row)
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
                        _sernumtrack = Classes.Pohoda.GetSerNumTrack((Row == null) || (Row.IsRefVPrFDTSNull()) ? (int?)null : ((int?)Row.RefVPrFDTS - 1), Row.IsVPrFDTSNull() ? false : Row.VPrFVTS);
                    }
            }

            return _sernumtrack;
        }

    }
}

