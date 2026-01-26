using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModuleSql
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.VLoginsGroups.IVLoginsGroups,
        Fask.Interfaces.Vyroba.VLoginsGroups.IVLoginsGroups_DeleteByLoginID,
        Fask.Interfaces.Vyroba.VLoginsGroups.IVLoginsGroups_GetDataByLoginID,
        Fask.Interfaces.Vyroba.VLoginsGroups.IVLoginsGroups_Insert,
        Fask.Interfaces.Vyroba.VLoginsGroups.IVLoginsGroups_DeleteByGoupID
    {

        #region IVLoginsGroups_DeleteByLoginID Members

        public void DeleteByLoginID(string ID)
        {
            //var taLoginsGroups = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(ConnectionString);
            //taLoginsGroups.DeleteByLoginID(ID);

            Database.Vyroba_VLoginsGroups.DeleteByLoginID(ConnectionString, ID);
        }

        #endregion

        #region IVLoginsGroups_Insert Members

        public void Insert(string loginid, string groupid)
        {
            //var taLoginsGroups = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(ConnectionString);
            //taLoginsGroups.Insert(loginid, groupid);

            Database.Vyroba_VLoginsGroups.Insert(ConnectionString, loginid, groupid);
        }

        #endregion

        #region IVLoginsGroups_GetDataByLoginID Members

        public Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable GetDataByLoginID(string LoginID)
        {

            //Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable();

            //var taLoginsGroups = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(ConnectionString);

            //var tmp = taLoginsGroups.GetDataByLoginID(LoginID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}

            //return dt;

            return Database.Vyroba_VLoginsGroups.GetDataByLoginID(ConnectionString, LoginID);

        }

        #endregion

        #region IVLoginsGroups_DeleteByGoupID Members

        public void DeleteByGoupID(string ID)
        {
            //var taLoginsGroups = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(ConnectionString);
            //taLoginsGroups.DeleteByGoupID(ID);

            Database.Vyroba_VLoginsGroups.DeleteByGoupID(ConnectionString, ID);
        }

        #endregion
    }
}
