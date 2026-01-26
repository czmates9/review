using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramVersion.UzivatelskeComponenty
{
    public partial class Panel_Licence : UserControl
    {

        #region Parametry

        #region Ukolovani

        public bool Ukolovani
        {
            get { return cb_Ukolovani.Checked; }
            set { cb_Ukolovani.Checked = value; }
        }

        #endregion

        #region Planovani

        public bool Planovani
        {
            get { return cb_Planovani.Checked; }
            set { cb_Planovani.Checked = value; }
        }

        public bool Planovani_PlanyVV
        {
            get { return cb_Planovani_PlanovaniVV.Checked; }
            set { cb_Planovani_PlanovaniVV.Checked = value; }
        }

        public bool Planovani_Kapac
        {
            get { return cb_Planovani_KapPlanovani.Checked; }
            set { cb_Planovani_KapPlanovani.Checked = value; }
        }

        #endregion

        #region Vyroba

        public bool Vyroba
        {
            get { return cb_Vyroba.Checked; }
            set { cb_Vyroba.Checked = value; }
        }

        #region Ciselniky

        public bool VyrobaCiselniky
        {
            get { return cb_Vyroba_Ciselniky.Checked; }
            set { cb_Vyroba_Ciselniky.Checked = value; }
        }

        public bool VyrobaCiselnikySkupiny
        {
            get { return cb_Vyroba_Ciselniky_Skupina.Checked; }
            set { cb_Vyroba_Ciselniky_Skupina.Checked = value; }
        }


        public bool VyrobaCiselnikyZasoby
        {
            get { return cb_Vyroba_Ciselniky_Zasoby.Checked; }
            set { cb_Vyroba_Ciselniky_Zasoby.Checked = value; }
        }


        public bool VyrobaCiselnikyVazbyMaterialy
        {
            get { return cb_Vyroba_Ciselniky_VazMaterialy.Checked; }
            set { cb_Vyroba_Ciselniky_VazMaterialy.Checked = value; }
        }

        #endregion

        #region Rozbory

        public bool VyrobaRozbory
        {
            get { return cb_Vyroba_Rozbory.Checked; }
            set { cb_Vyroba_Rozbory.Checked = value; }
        }

        public bool VyrobaRozboryPlanVyroby
        {
            get { return cb_Vyroba_Rozbory_PlanVyroby.Checked; }
            set { cb_Vyroba_Rozbory_PlanVyroby.Checked = value; }
        }

        public bool VyrobaRozboryOdvadeniStroju
        {
            get { return cb_Vyroba_Rozbory_OdvadeniStroju.Checked; }
            set { cb_Vyroba_Rozbory_OdvadeniStroju.Checked = value; }
        }

        public bool VyrobaRozboryVyrobky
        {
            get { return cb_Vyroba_Rozbory_Vyrobky.Checked; }
            set { cb_Vyroba_Rozbory_Vyrobky.Checked = value; }
        }

        public bool VyrobaRozboryVyrobkySN
        {
            get { return cb_Vyroba_Rozbory_VyrobkySN.Checked; }
            set { cb_Vyroba_Rozbory_VyrobkySN.Checked = value; }
        }

        public bool VyrobaRozboryMaterialy
        {
            get { return cb_Vyroba_Rozbory_Materialy.Checked; }
            set { cb_Vyroba_Rozbory_Materialy.Checked = value; }
        }


        #endregion

        #region Transakce

        public bool VyrobaTransakce
        {
            get { return cb_Vyroba_Transakce.Checked; }
            set { cb_Vyroba_Transakce.Checked = value; }
        }

        public bool VyrobaTransakceOdvodPOHODA
        {
            get { return cb_Vyroba_Transakce_OdvodPOHODA.Checked; }
            set { cb_Vyroba_Transakce_OdvodPOHODA.Checked = value; }
        }

        public bool VyrobaTransakceVyrobnyPrikaz
        {
            get { return cb_Vyroba_Transakce_VyrobnyPrikaz.Checked; }
            set { cb_Vyroba_Transakce_VyrobnyPrikaz.Checked = value; }
        }


        #endregion

        #endregion

        #region Sklady

        public bool Sklady
        {
            get { return cb_Sklady.Checked; }
            set { cb_Sklady.Checked = value; }
        }

        #region Ciselniky


        public bool SkladyCiselniky
        {
            get { return cb_Sklady_Ciselniky.Checked; }
            set { cb_Sklady_Ciselniky.Checked = value; }
        }

        public bool SkladyCiselnikySklady
        {
            get { return cb_Sklady_C_Sklady.Checked; }
            set { cb_Sklady_C_Sklady.Checked = value; }
        }

        public bool SkladyCiselnikyStrediska
        {
            get { return cb_Sklady_C_Strediska.Checked; }
            set { cb_Sklady_C_Strediska.Checked = value; }
        }

        public bool SkladyCiselnikyMapaLokaci
        {
            get { return cb_Sklady_C_MapaLokaci.Checked; }
            set { cb_Sklady_C_MapaLokaci.Checked = value; }
        }

        public bool SkladyCiselnikyVariantyLokaciMaterialu
        {
            get { return cb_Sklady_C_VarLokMat.Checked; }
            set { cb_Sklady_C_VarLokMat.Checked = value; }
        }

        public bool SkladyCiselnikyTypyLokaci
        {
            get { return cb_Sklady_C_TypyLokaci.Checked; }
            set { cb_Sklady_C_TypyLokaci.Checked = value; }
        }

        public bool SkladyCiselnikyZasoby
        {
            get { return cb_Sklady_C_Zasoby.Checked; }
            set { cb_Sklady_C_Zasoby.Checked = value; }
        }

        public bool SkladyCiselnikyAdresar
        {
            get { return cb_Sklady_C_Adresar.Checked; }
            set { cb_Sklady_C_Adresar.Checked = value; }
        }

        #endregion

        #region Transakce

        public bool SkladyTransakce
        {
            get { return cb_Sklady_Tran.Checked; }
            set { cb_Sklady_Tran.Checked = value; }
        }

        public bool SkladyTransakcePrP
        {
            get { return cb_Sklady_Tran_PrP.Checked; }
            set { cb_Sklady_Tran_PrP.Checked = value; }
        }

        public bool SkladyTransakceVyP
        {
            get { return cb_Sklady_Tran_VyPr.Checked; }
            set { cb_Sklady_Tran_VyPr.Checked = value; }
        }

        public bool SkladyTransakceExpedice
        {
            get { return cb_Sklady_Tran_Expe.Checked; }
            set { cb_Sklady_Tran_Expe.Checked = value; }
        }

        public bool SkladyTransakceVolnyPohyb
        {
            get { return cb_Sklady_Tran_VolnPohyb.Checked; }
            set { cb_Sklady_Tran_VolnPohyb.Checked = value; }
        }

        public bool SkladyTransakcePrevod
        {
            get { return cb_Sklady_Tran_Prevod.Checked; }
            set { cb_Sklady_Tran_Prevod.Checked = value; }
        }

        public bool SkladyTransakceInventura
        {
            get { return cb_Sklady_Tran_Inv.Checked; }
            set { cb_Sklady_Tran_Inv.Checked = value; }
        }

        #endregion

        #region Rozbory

        public bool SkladyRozbory
        {
            get { return cb_Sklady_Rozbory.Checked; }
            set { cb_Sklady_Rozbory.Checked = value; }
        }

        public bool SkladyRozboryPohyby
        {
            get { return cb_Sklady_R_Pohyby.Checked; }
            set { cb_Sklady_R_Pohyby.Checked = value; }
        }

        public bool SkladyRozboryStavy
        {
            get { return cb_Sklady_R_Stavy.Checked; }
            set { cb_Sklady_R_Stavy.Checked = value; }
        }

        public bool SkladyRozboryInv
        {
            get { return cb_Sklady_R_INV.Checked; }
            set { cb_Sklady_R_INV.Checked = value; }
        }

        public bool SkladyRozboryInv_Stav
        {
            get { return cb_Sklady_R_INV_Stav.Checked; }
            set { cb_Sklady_R_INV_Stav.Checked = value; }
        }

        public bool SkladyRozboryLokMech
        {
            get { return cb_Sklady_R_LokMech.Checked; }
            set { cb_Sklady_R_LokMech.Checked = value; }
        }

        public bool SkladyRozboryLokMec_Stavy
        {
            get { return cb_Sklady_R_LokMech_Stav.Checked; }
            set { cb_Sklady_R_LokMech_Stav.Checked = value; }
        }

        #endregion


        #endregion

        #region Stav Skladu

        public bool StavSkladu
        {
            get { return cb_StavSkladu.Checked; }
            set { cb_StavSkladu.Checked = value; }
        }

        #endregion

        #region Servis

        public bool Servis
        {
            get { return cb_Servis.Checked; }
            set { cb_Servis.Checked = value; }
        }

        #endregion

        #region IT Cast

        public bool IT_Cast
        {
            get { return cb_IT.Checked; }
            set { cb_IT.Checked = value; }
        }

        public bool IPTerminalu
        {
            get { return cb_IT_IPterm.Checked; }
            set { cb_IT_IPterm.Checked = value; }
        }

        public bool TypyDokladu
        {
            get { return cb_IT_TypyDokladu.Checked; }
            set { cb_IT_TypyDokladu.Checked = value; }
        }

        public bool Fask_Rady
        {
            get { return cb_IT_Rady.Checked; }
            set { cb_IT_Rady.Checked = value; }
        }

        #endregion

        #region Ostatni

        public bool Ostatni
        {
            get { return cb_Ostatni.Checked; }
            set { cb_Ostatni.Checked = value; }
        }

        #endregion

        #endregion

        public Panel_Licence()
        {
            InitializeComponent();
        }

        public void ClearAll()
        {
            Ukolovani = false;
            
            Planovani = false;
            Planovani_PlanyVV = false;
            Planovani_Kapac = false;

            Vyroba = false;

            VyrobaCiselniky = false;
            VyrobaCiselnikySkupiny = false;
            VyrobaCiselnikyZasoby = false;
            VyrobaCiselnikyVazbyMaterialy = false;

            VyrobaRozbory = false;

            VyrobaRozboryPlanVyroby = false;
            VyrobaRozboryOdvadeniStroju = false;
            VyrobaRozboryVyrobky = false;
            VyrobaRozboryVyrobkySN = false;
            VyrobaRozboryMaterialy = false;

            VyrobaTransakce = false;
            VyrobaTransakceOdvodPOHODA = false;
            VyrobaTransakceVyrobnyPrikaz = false;

            Sklady = false;

            SkladyCiselniky = false;
            SkladyCiselnikySklady = false;
            SkladyCiselnikyStrediska = false;
            SkladyCiselnikyMapaLokaci = false;
            SkladyCiselnikyVariantyLokaciMaterialu = false;
            SkladyCiselnikyTypyLokaci = false;
            SkladyCiselnikyZasoby = false;
            SkladyCiselnikyAdresar = false;

            SkladyTransakce = false;
            SkladyTransakcePrP = false;
            SkladyTransakceVyP = false;
            SkladyTransakceExpedice = false;
            SkladyTransakceVolnyPohyb = false;
            SkladyTransakcePrevod = false;
            SkladyTransakceInventura = false;

            SkladyRozbory = false;
            SkladyRozboryPohyby = false;
            SkladyRozboryStavy = false;
            SkladyRozboryInv = false;
            SkladyRozboryInv_Stav = false;
            SkladyRozboryLokMech = false;
            SkladyRozboryLokMec_Stavy = false;

            StavSkladu = false;
            Servis = false;

            IT_Cast = false;
            IPTerminalu = false;
            TypyDokladu = false;
            Fask_Rady = false;

            Ostatni = false;

        }

        public void SelectAll()
        {
            Ukolovani = true;

            Planovani = true;
            Planovani_PlanyVV = true;
            Planovani_Kapac = true;

            Vyroba = true;

            VyrobaCiselniky = true;
            VyrobaCiselnikySkupiny = true;
            VyrobaCiselnikyZasoby = true;
            VyrobaCiselnikyVazbyMaterialy = true;

            VyrobaRozbory = true;

            VyrobaRozboryPlanVyroby = true;
            VyrobaRozboryOdvadeniStroju = true;
            VyrobaRozboryVyrobky = true;
            VyrobaRozboryVyrobkySN = true;
            VyrobaRozboryMaterialy = true;

            VyrobaTransakce = true;
            VyrobaTransakceOdvodPOHODA = true;
            VyrobaTransakceVyrobnyPrikaz = true;

            Sklady = true;

            SkladyCiselniky = true;
            SkladyCiselnikySklady = true;
            SkladyCiselnikyStrediska = true;
            SkladyCiselnikyMapaLokaci = true;
            SkladyCiselnikyVariantyLokaciMaterialu = true;
            SkladyCiselnikyTypyLokaci = true;
            SkladyCiselnikyZasoby = true;
            SkladyCiselnikyAdresar = true;

            SkladyTransakce = true;
            SkladyTransakcePrP = true;
            SkladyTransakceVyP = true;
            SkladyTransakceExpedice = true;
            SkladyTransakceVolnyPohyb = true;
            SkladyTransakcePrevod = true;
            SkladyTransakceInventura = true;

            SkladyRozbory = true;
            SkladyRozboryPohyby = true;
            SkladyRozboryStavy = true;
            SkladyRozboryInv = true;
            SkladyRozboryInv_Stav = true;
            SkladyRozboryLokMech = true;
            SkladyRozboryLokMec_Stavy = true;

            StavSkladu = true;
            Servis = true;

            IT_Cast = true;
            IPTerminalu = true;
            TypyDokladu = true;
            Fask_Rady = true;

            Ostatni = true;

        }


        public void All_SetGray()
        {
            cb_Ukolovani.ForeColor = Color.Gray;

            cb_Planovani.ForeColor = Color.Gray;
            cb_Planovani_PlanovaniVV.ForeColor = Color.Gray;
            cb_Planovani_KapPlanovani.ForeColor = Color.Gray;

            cb_Vyroba.ForeColor = Color.Gray;

            cb_Vyroba_Ciselniky.ForeColor = Color.Gray;
            cb_Vyroba_Ciselniky_Skupina.ForeColor = Color.Gray;
            cb_Vyroba_Ciselniky_VazMaterialy.ForeColor = Color.Gray;
            cb_Vyroba_Ciselniky_Zasoby.ForeColor = Color.Gray;

            cb_Vyroba_Rozbory.ForeColor = Color.Gray;

            cb_Vyroba_Rozbory_PlanVyroby.ForeColor = Color.Gray;
            cb_Vyroba_Rozbory_OdvadeniStroju.ForeColor = Color.Gray;
            cb_Vyroba_Rozbory_Vyrobky.ForeColor = Color.Gray;
            cb_Vyroba_Rozbory_VyrobkySN.ForeColor = Color.Gray;
            cb_Vyroba_Rozbory_Materialy.ForeColor = Color.Gray;

            cb_Vyroba_Transakce.ForeColor = Color.Gray;
            cb_Vyroba_Transakce_OdvodPOHODA.ForeColor = Color.Gray;
            cb_Vyroba_Transakce_VyrobnyPrikaz.ForeColor = Color.Gray;

            cb_Sklady.ForeColor = Color.Gray;

            cb_Sklady_Ciselniky.ForeColor = Color.Gray;
            cb_Sklady_C_Sklady.ForeColor = Color.Gray;
            cb_Sklady_C_Strediska.ForeColor = Color.Gray;
            cb_Sklady_C_MapaLokaci.ForeColor = Color.Gray;
            cb_Sklady_C_VarLokMat.ForeColor = Color.Gray;
            cb_Sklady_C_TypyLokaci.ForeColor = Color.Gray;
            cb_Sklady_C_Zasoby.ForeColor = Color.Gray;
            cb_Sklady_C_Adresar.ForeColor = Color.Gray;

            cb_Sklady_Tran.ForeColor = Color.Gray;
            cb_Sklady_Tran_PrP.ForeColor = Color.Gray;
            cb_Sklady_Tran_VyPr.ForeColor = Color.Gray;
            cb_Sklady_Tran_Expe.ForeColor = Color.Gray;
            cb_Sklady_Tran_VolnPohyb.ForeColor = Color.Gray;
            cb_Sklady_Tran_Prevod.ForeColor = Color.Gray;
            cb_Sklady_Tran_Inv.ForeColor = Color.Gray;

            cb_Sklady_Rozbory.ForeColor = Color.Gray;
            cb_Sklady_R_Pohyby.ForeColor = Color.Gray;
            cb_Sklady_R_Stavy.ForeColor = Color.Gray;
            cb_Sklady_R_INV.ForeColor = Color.Gray;
            cb_Sklady_R_INV_Stav.ForeColor = Color.Gray;
            cb_Sklady_R_LokMech.ForeColor = Color.Gray;
            cb_Sklady_R_LokMech_Stav.ForeColor = Color.Gray;

            cb_StavSkladu.ForeColor = Color.Gray;
            cb_Servis.ForeColor = Color.Gray;

            cb_IT.ForeColor = Color.Gray;
            cb_IT_IPterm.ForeColor = Color.Gray;
            cb_IT_TypyDokladu.ForeColor = Color.Gray;
            cb_IT_Rady.ForeColor = Color.Gray;

            cb_Ostatni.ForeColor = Color.Gray;

        }

        public void All_AutoCheck()
        {
            cb_Ukolovani.AutoCheck = false;

            cb_Planovani.AutoCheck = false;
            cb_Planovani_PlanovaniVV.AutoCheck = false;
            cb_Planovani_KapPlanovani.AutoCheck = false;

            cb_Vyroba.AutoCheck = false;

            cb_Vyroba_Ciselniky.AutoCheck = false;
            cb_Vyroba_Ciselniky_Skupina.AutoCheck = false;
            cb_Vyroba_Ciselniky_VazMaterialy.AutoCheck = false;
            cb_Vyroba_Ciselniky_Zasoby.AutoCheck = false;

            cb_Vyroba_Rozbory.AutoCheck = false;

            cb_Vyroba_Rozbory_PlanVyroby.AutoCheck = false;
            cb_Vyroba_Rozbory_OdvadeniStroju.AutoCheck = false;
            cb_Vyroba_Rozbory_Vyrobky.AutoCheck = false;
            cb_Vyroba_Rozbory_VyrobkySN.AutoCheck = false;
            cb_Vyroba_Rozbory_Materialy.AutoCheck = false;

            cb_Vyroba_Transakce.AutoCheck = false;
            cb_Vyroba_Transakce_OdvodPOHODA.AutoCheck = false;
            cb_Vyroba_Transakce_VyrobnyPrikaz.AutoCheck = false;

            cb_Sklady.AutoCheck = false;

            cb_Sklady_Ciselniky.AutoCheck = false;
            cb_Sklady_C_Sklady.AutoCheck = false;
            cb_Sklady_C_Strediska.AutoCheck = false;
            cb_Sklady_C_MapaLokaci.AutoCheck = false;
            cb_Sklady_C_VarLokMat.AutoCheck = false;
            cb_Sklady_C_TypyLokaci.AutoCheck = false;
            cb_Sklady_C_Zasoby.AutoCheck = false;
            cb_Sklady_C_Adresar.AutoCheck = false;

            cb_Sklady_Tran.AutoCheck = false;
            cb_Sklady_Tran_PrP.AutoCheck = false;
            cb_Sklady_Tran_VyPr.AutoCheck = false;
            cb_Sklady_Tran_Expe.AutoCheck = false;
            cb_Sklady_Tran_VolnPohyb.AutoCheck = false;
            cb_Sklady_Tran_Prevod.AutoCheck = false;
            cb_Sklady_Tran_Inv.AutoCheck = false;

            cb_Sklady_Rozbory.AutoCheck = false;
            cb_Sklady_R_Pohyby.AutoCheck = false;
            cb_Sklady_R_Stavy.AutoCheck = false;
            cb_Sklady_R_INV.AutoCheck = false;
            cb_Sklady_R_INV_Stav.AutoCheck = false;
            cb_Sklady_R_LokMech.AutoCheck = false;
            cb_Sklady_R_LokMech_Stav.AutoCheck = false;

            cb_StavSkladu.AutoCheck = false;
            cb_Servis.AutoCheck = false;

            cb_IT.AutoCheck = false;
            cb_IT_IPterm.AutoCheck = false;
            cb_IT_TypyDokladu.AutoCheck = false;
            cb_IT_Rady.AutoCheck = false;

            cb_Ostatni.AutoCheck = false;

        }

    }
}
