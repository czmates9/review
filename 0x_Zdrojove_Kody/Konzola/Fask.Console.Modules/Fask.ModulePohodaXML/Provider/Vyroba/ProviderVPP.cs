using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.Classes;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.VPP.IVPP,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP,
        Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE,
        Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPP.IVPP_Insert,
        Fask.Interfaces.Vyroba.VPP.IVPP_Update,
        Fask.Interfaces.Vyroba.VPP.IVPP_Fill,
        Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList,
        Fask.Interfaces.Vyroba.VPP.IVPP_GenerovatSN_CZPRO_VPP
    {

        #region IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSOPNUMBEITEMNMBR(int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPP.GetDataByCountEntriesSOPNUMBEITEMNMBR(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, CountEntries, SOPNUMBE, ITEMNMBR);
        }

        #endregion

        #region IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(int CountEntries, string SOPNUMBE, string ITEMNMBR, string BarcodeP)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPP.GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, CountEntries, SOPNUMBE, ITEMNMBR, BarcodeP);
        }

        #endregion

        #region IVPP_FillByCountEntriesAndSOPNUMBE Members

        public int FillByCountEntriesAndSOPNUMBE(Fask.Interfaces.DataSets.Vyroba ds, string TypeORDERBY, int CountEntries, string SOPNUMBE)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPP.FillByCountEntriesAndSOPNUMBE(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ds, TypeORDERBY, CountEntries, SOPNUMBE);
        }

        #endregion

        #region IVPP_DeleteByCountEntriesSOPNUMBE Members

        public void DeleteByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {

            Globals_V1.LoadConfiguration();
            Database.Vyroba_CZPRO_VPP.DeleteByCountEntriesSOPNUMBE(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, CountEntries, SOPNUMBE);
        }

        #endregion

        #region IVPP_Insert Members

        public void VPP_Insert_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Database.Vyroba_CZPRO_VPP.Update(row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw;
            }
        }


        #endregion

        #region IVPP_Update Members


        public void VPP_Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Database.Vyroba_CZPRO_VPP.Update(dt, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region IVPP_Fill Members

        public void VPP_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Globals_V1.LoadConfiguration();
            Database.Vyroba_CZPRO_VPP.VPP_Fill( Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ds);
        }

        #endregion

        #region IVPP_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPPList(Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPP.GetFiltrovanyVPPList(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, filtr);
        }

        #endregion

        #region IVPP_Update_Row Members

        public void VPP_Update_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Database.Vyroba_CZPRO_VPP.Update(row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region IVPP_GenerovatSN_CZPRO_VPP Members

        public void GenerovatSN_CZPRO_VPP(GenerovaniSN gl)
        {
            Database.Vyroba_CZPRO_VPP.GenerujSN(gl);
        }

        #endregion
    }
}
