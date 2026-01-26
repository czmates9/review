using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
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
            //Globals_V1.LoadConfiguration();
            //var taLoginsGroups = new Pohoda_DataSets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //taLoginsGroups.DeleteByLoginID(ID);

            Globals_V1.LoadConfiguration();
            Database.Vyroba_VLoginsGroups.DeleteByLoginID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB,ID);
        }

        #endregion

        #region IVLoginsGroups_Insert Members

        public void Insert(string loginid, string groupid)
        {
            //Globals_V1.LoadConfiguration();
            //var taLoginsGroups = new Pohoda_DataSets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //taLoginsGroups.Insert(loginid, groupid);

            Globals_V1.LoadConfiguration();
            Database.Vyroba_VLoginsGroups.Insert(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, loginid, groupid);
        }

        #endregion

        #region IVLoginsGroups_GetDataByLoginID Members

        public Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable GetDataByLoginID(string LoginID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable();

            //var taLoginsGroups = new Pohoda_DataSets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = taLoginsGroups.GetDataByLoginID(LoginID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;


            Globals_V1.LoadConfiguration();
            return Database.Vyroba_VLoginsGroups.GetDataByLoginID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, LoginID);
        }

        #endregion

        #region IVLoginsGroups_DeleteByGoupID Members

        public void DeleteByGoupID(string ID)
        {
            //Globals_V1.LoadConfiguration();
            //var taLoginsGroups = new Pohoda_DataSets.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
            //taLoginsGroups.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //taLoginsGroups.DeleteByGoupID(ID);


            Globals_V1.LoadConfiguration();
            Database.Vyroba_VLoginsGroups.DeleteByGoupID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ID);
        }

        #endregion
    }
}
