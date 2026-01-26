using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.ModuleSql.Database;

namespace Fask.ModuleSql
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Production.IProduction,
        Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList,
        Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_Update,

         Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby

    {

        #region IProduction_GetFiltrovanyProductionList Members

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionList(Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            return Vyroba_Production.GetFiltrovanyProductionList(ConnectionString, filtr);
        }

        #endregion

        #region IProduction_FillByCORRGUID Members

        public void Production_FillByCORRGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID)
        {
            Vyroba_Production.Production_FillByCORRGUID(ConnectionString, ds, CORRGUID);
        }

        #endregion

        #region IProduction_FillBySOUBEHGUID Members

        public void Production_FillBySOUBEHGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID)
        {
            Vyroba_Production.Production_FillBySOUBEHGUID(ConnectionString, ds, SOUBEHGUID);
        }

        #endregion

        #region IProduction_GetDataByCORRGUID Members

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCORRGUID(Guid CORRGUID)
        {
            return Vyroba_Production.Production_GetDataByCORRGUID(ConnectionString, CORRGUID);
        }

        #endregion

        #region IProduction_GetDataBySOUBEHGUID Members

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID(Guid SOUBEHGUID)
        {
            return Vyroba_Production.Production_GetDataBySOUBEHGUID(ConnectionString, SOUBEHGUID);
        }

        #endregion

        #region IProduction_Update Members

        public void Production_Update(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt)
        {
            Vyroba_Production.Update(dt, ConnectionString);
        }

        #endregion

        #region IProduction_GetFiltrovanyProductionVazby Members

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            return Vyroba_Production.GetFiltrovanyProductionVazby(ConnectionString, filtr);
        }

        #endregion
    }
}
