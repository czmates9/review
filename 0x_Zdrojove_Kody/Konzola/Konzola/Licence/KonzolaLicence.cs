using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konzola
{
    public class KonzolaLicence
    {
        #region Parametry

        #region Ukolovani

        private bool _ukolovani;
        public bool Ukolovani
        {
            get { return _ukolovani; }
            set { _ukolovani = value; }
        }

        #endregion

        #region Planovani

        private bool _planovani;
        public bool Planovani
        {
            get { return _planovani; }
            set { _planovani = value; }
        }

        private bool _planovani_PlanyVV;
        public bool Planovani_PlanyVV
        {
            get { return _planovani_PlanyVV; }
            set { _planovani_PlanyVV = value; }
        }

        private bool _planovani_Kapac;
        public bool Planovani_Kapac
        {
            get { return _planovani_Kapac; }
            set { _planovani_Kapac = value; }
        }

        #endregion

        #region Vyroba

        private bool _vyroba;
        public bool Vyroba
        {
            get { return _vyroba; }
            set { _vyroba = value; }
        }

        #region Ciselniky

        private bool _vyrobaCiselniky;
        public bool VyrobaCiselniky
        {
            get { return _vyrobaCiselniky; }
            set { _vyrobaCiselniky = value; }
        }

        private bool _vyrobaCiselnikySkupiny;
        public bool VyrobaCiselnikySkupiny
        {
            get { return _vyrobaCiselnikySkupiny; }
            set { _vyrobaCiselnikySkupiny = value; }
        }

        private bool _vyrobaCiselnikyZasoby;
        public bool VyrobaCiselnikyZasoby
        {
            get { return _vyrobaCiselnikyZasoby; }
            set { _vyrobaCiselnikyZasoby = value; }
        }

        private bool _vyrobaCiselnikyVazbyMaterialy;
        public bool VyrobaCiselnikyVazbyMaterialy
        {
            get { return _vyrobaCiselnikyVazbyMaterialy; }
            set { _vyrobaCiselnikyVazbyMaterialy = value; }
        }

        #endregion

        #region Rozbory

        private bool _vyrobaRozbory;
        public bool VyrobaRozbory
        {
            get { return _vyrobaRozbory; }
            set { _vyrobaRozbory = value; }
        }

        private bool _vyrobaRozboryPlanVyroby;
        public bool VyrobaRozboryPlanVyroby
        {
            get { return _vyrobaRozboryPlanVyroby; }
            set { _vyrobaRozboryPlanVyroby = value; }
        }

        private bool _vyrobaRozboryOdvadeniStroju;
        public bool VyrobaRozboryOdvadeniStroju
        {
            get { return _vyrobaRozboryOdvadeniStroju; }
            set { _vyrobaRozboryOdvadeniStroju = value; }
        }

        private bool _vyrobaRozboryVyrobky;
        public bool VyrobaRozboryVyrobky
        {
            get { return _vyrobaRozboryVyrobky; }
            set { _vyrobaRozboryVyrobky = value; }
        }

        private bool _vyrobaRozboryVyrobkySN;
        public bool VyrobaRozboryVyrobkySN
        {
            get { return _vyrobaRozboryVyrobkySN; }
            set { _vyrobaRozboryVyrobkySN = value; }
        }

        private bool _vyrobaRozboryMaterialy;
        public bool VyrobaRozboryMaterialy
        {
            get { return _vyrobaRozboryMaterialy; }
            set { _vyrobaRozboryMaterialy = value; }
        }


        #endregion

        #region Transakce

        private bool _vyrobaTransakce;
        public bool VyrobaTransakce
        {
            get { return _vyrobaTransakce; }
            set { _vyrobaTransakce = value; }
        }

        private bool _vyrobaTransakceOdvodPOHODA;
        public bool VyrobaTransakceOdvodPOHODA
        {
            get { return _vyrobaTransakceOdvodPOHODA; }
            set { _vyrobaTransakceOdvodPOHODA = value; }
        }

        private bool _vyrobaTransakceVyrobnyPrikaz;
        public bool VyrobaTransakceVyrobnyPrikaz
        {
            get { return _vyrobaTransakceVyrobnyPrikaz; }
            set { _vyrobaTransakceVyrobnyPrikaz = value; }
        }


        #endregion

        #endregion

        #region Sklady

        private bool _sklady;
        public bool Sklady
        {
            get { return _sklady; }
            set { _sklady = value; }
        }

        #region Ciselniky

        private bool _skladyCiselniky;
        public bool SkladyCiselniky
        {
            get { return _skladyCiselniky; }
            set { _skladyCiselniky = value; }
        }

        private bool _skladyCiselnikySklady;
        public bool SkladyCiselnikySklady
        {
            get { return _skladyCiselnikySklady; }
            set { _skladyCiselnikySklady = value; }
        }

        private bool _skladyCiselnikyStrediska;
        public bool SkladyCiselnikyStrediska
        {
            get { return _skladyCiselnikyStrediska; }
            set { _skladyCiselnikyStrediska = value; }
        }

        private bool _skladyCiselnikyMapaLokaci;
        public bool SkladyCiselnikyMapaLokaci
        {
            get { return _skladyCiselnikyMapaLokaci; }
            set { _skladyCiselnikyMapaLokaci = value; }
        }

        private bool _skladyCiselnikyVariantyLokaciMaterialu;
        public bool SkladyCiselnikyVariantyLokaciMaterialu
        {
            get { return _skladyCiselnikyVariantyLokaciMaterialu; }
            set { _skladyCiselnikyVariantyLokaciMaterialu = value; }
        }

        private bool _skladyCiselnikyTypyLokaci;
        public bool SkladyCiselnikyTypyLokaci
        {
            get { return _skladyCiselnikyTypyLokaci; }
            set { _skladyCiselnikyTypyLokaci = value; }
        }

        private bool _skladyCiselnikyZasoby;
        public bool SkladyCiselnikyZasoby
        {
            get { return _skladyCiselnikyZasoby; }
            set { _skladyCiselnikyZasoby = value; }
        }

        private bool _skladyCiselnikyAdresar;
        public bool SkladyCiselnikyAdresar
        {
            get { return _skladyCiselnikyAdresar; }
            set { _skladyCiselnikyAdresar = value; }
        }

        #endregion

        #region Transakce

        private bool _skladyTransakce;
        public bool SkladyTransakce
        {
            get { return _skladyTransakce; }
            set { _skladyTransakce = value; }
        }

        private bool _skladyTransakcePrP;
        public bool SkladyTransakcePrP
        {
            get { return _skladyTransakcePrP; }
            set { _skladyTransakcePrP = value; }
        }

        private bool _skladyTransakceVyP;
        public bool SkladyTransakceVyP
        {
            get { return _skladyTransakceVyP; }
            set { _skladyTransakceVyP = value; }
        }

        private bool _skladyTransakceExpedice;
        public bool SkladyTransakceExpedice
        {
            get { return _skladyTransakceExpedice; }
            set { _skladyTransakceExpedice = value; }
        }

        private bool _skladyTransakceVolnyPohyb;
        public bool SkladyTransakceVolnyPohyb
        {
            get { return _skladyTransakceVolnyPohyb; }
            set { _skladyTransakceVolnyPohyb = value; }
        }

        private bool _skladyTransakcePrevod;
        public bool SkladyTransakcePrevod
        {
            get { return _skladyTransakcePrevod; }
            set { _skladyTransakcePrevod = value; }
        }

        private bool _skladyTransakceInventura;
        public bool SkladyTransakceInventura
        {
            get { return _skladyTransakceInventura; }
            set { _skladyTransakceInventura = value; }
        }

        #endregion

        #region Rozbory

        private bool _skladyRozbory;
        public bool SkladyRozbory
        {
            get { return _skladyRozbory; }
            set { _skladyRozbory = value; }
        }

        private bool _skladyRozboryPohyby;
        public bool SkladyRozboryPohyby
        {
            get { return _skladyRozboryPohyby; }
            set { _skladyRozboryPohyby = value; }
        }

        private bool _skladyRozboryStavy;
        public bool SkladyRozboryStavy
        {
            get { return _skladyRozboryStavy; }
            set { _skladyRozboryStavy = value; }
        }

        private bool _skladyRozboryInv;
        public bool SkladyRozboryInv
        {
            get { return _skladyRozboryInv; }
            set { _skladyRozboryInv = value; }
        }

        private bool _skladyRozboryInv_Stav;
        public bool SkladyRozboryInv_Stav
        {
            get { return _skladyRozboryInv_Stav; }
            set { _skladyRozboryInv_Stav = value; }
        }

        private bool _skladyRozboryLokMech;
        public bool SkladyRozboryLokMech
        {
            get { return _skladyRozboryLokMech; }
            set { _skladyRozboryLokMech = value; }
        }

        private bool _skladyRozboryLokMec_Stavy;
        public bool SkladyRozboryLokMec_Stavy
        {
            get { return _skladyRozboryLokMec_Stavy; }
            set { _skladyRozboryLokMec_Stavy = value; }
        }

        #endregion


        #endregion

        #region Stav Skladu

        private bool _stavSkladu;
        public bool StavSkladu
        {
            get { return _stavSkladu; }
            set { _stavSkladu = value; }
        }

        #endregion

        #region Servis

        private bool _servis;
        public bool Servis
        {
            get { return _servis; }
            set { _servis = value; }
        }

        #endregion

        #region IT Cast

        private bool _iT_Cast;
        public bool IT_Cast
        {
            get { return _iT_Cast; }
            set { _iT_Cast = value; }
        }

        private bool _iPTerminalu;
        public bool IPTerminalu
        {
            get { return _iPTerminalu; }
            set { _iPTerminalu = value; }
        }

        private bool _typyDokladu;
        public bool TypyDokladu
        {
            get { return _typyDokladu; }
            set { _typyDokladu = value; }
        }

        private bool _fask_Rady;
        public bool Fask_Rady
        {
            get { return _fask_Rady; }
            set { _fask_Rady = value; }
        }

        #endregion

        #region Ostatni

        private bool _ostatni;
        public bool Ostatni
        {
            get { return _ostatni; }
            set { _ostatni = value; }
        }

        #endregion

        #endregion

    }
}
