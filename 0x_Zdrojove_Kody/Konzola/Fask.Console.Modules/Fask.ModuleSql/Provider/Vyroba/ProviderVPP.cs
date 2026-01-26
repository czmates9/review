using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;

namespace Fask.ModuleSql
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
        Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList
    {

        #region IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSOPNUMBEITEMNMBR(int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {
            return Database.Vyroba_CZPRO_VPP.GetDataByCountEntriesSOPNUMBEITEMNMBR(ConnectionString, CountEntries, SOPNUMBE, ITEMNMBR);
        }

        #endregion

        #region IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(int CountEntries, string SOPNUMBE, string ITEMNMBR, string BarcodeP)
        {
            return Database.Vyroba_CZPRO_VPP.GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(ConnectionString, CountEntries, SOPNUMBE, ITEMNMBR, BarcodeP);
        }

        #endregion

        #region IVPP_FillByCountEntriesAndSOPNUMBE Members

        public int FillByCountEntriesAndSOPNUMBE(Fask.Interfaces.DataSets.Vyroba ds, string TypeORDERBY, int CountEntries, string SOPNUMBE)
        {
            return Database.Vyroba_CZPRO_VPP.FillByCountEntriesAndSOPNUMBE(ConnectionString, ds, TypeORDERBY, CountEntries, SOPNUMBE);
        }

        #endregion

        #region IVPP_DeleteByCountEntriesSOPNUMBE Members

        public void DeleteByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {
            Database.Vyroba_CZPRO_VPP.DeleteByCountEntriesSOPNUMBE(ConnectionString, CountEntries, SOPNUMBE);
        }

        #endregion

        #region IVPP_Insert Members
        public void VPP_Insert_Row(Vyroba.CZPRO_VPPRow row)
        {

            try
            {
                Database.Vyroba_CZPRO_VPP.Update(row, ConnectionString);
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
                Database.Vyroba_CZPRO_VPP.Update(dt, ConnectionString);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw;
            }
        }

        #endregion

        #region IVPP_Fill Members

        public void VPP_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Database.Vyroba_CZPRO_VPP.VPP_Fill(ConnectionString, ds);
        }

        #endregion

        #region IVPP_Update_Row Members

        public void VPP_Update_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row)
        {
            Database.Vyroba_CZPRO_VPP.Update(row, ConnectionString);
        }

        #endregion

        #region IVPP_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPPList(Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            return Database.Vyroba_CZPRO_VPP.GetFiltrovanyVPPList(ConnectionString, filtr);
        }

        #endregion

    }
}
