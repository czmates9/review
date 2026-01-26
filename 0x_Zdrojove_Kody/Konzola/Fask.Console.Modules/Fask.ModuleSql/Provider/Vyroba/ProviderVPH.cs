using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;

namespace Fask.ModuleSql
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.VPH.IVPH,
        Fask.Interfaces.Vyroba.VPH.IVPH_Fill,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPH.IVPH_Insert,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList 

    {
        #region IVPH_Fill Members

        public void VPH_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Database.Vyroba_CZPRO_VPH.Fill_VPH(ConnectionString, ds);
        }

        #endregion

        #region IVPH_Update Members

        public int Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt)
        {
            return Database.Vyroba_CZPRO_VPH.Update(dt, ConnectionString);
        }

        #endregion

        #region IVPH_GetDataByCountEntriesSOPNUMBE Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {
            return Database.Vyroba_CZPRO_VPH.Get_VPH_ByCountEntriesSOPNUMBE(ConnectionString, CountEntries, SOPNUMBE);

        }

        #endregion

        #region IVPH_Insert Members

        public void Insert(Vyroba.CZPRO_VPHRow row)
        {
            Database.Vyroba_CZPRO_VPH.Insert_VPH(ConnectionString, row);
        }

        #endregion

        #region IVPH_Update_Row Members

        public int Update_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow Row)
        {
            return Database.Vyroba_CZPRO_VPH.Update(Row, ConnectionString);
        }

        #endregion

        #region IVPH_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPHList(Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            return Database.Vyroba_CZPRO_VPH.Get_VPHByFilter(ConnectionString, filtr);
        }
            
        #endregion
}
}
