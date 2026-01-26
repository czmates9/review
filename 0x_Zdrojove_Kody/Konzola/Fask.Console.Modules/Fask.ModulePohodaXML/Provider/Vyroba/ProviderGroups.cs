using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Groups.IGroups,
        Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups,
        Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID,
        Fask.Interfaces.Vyroba.Groups.IGroups_Insert,
        Fask.Interfaces.Vyroba.Groups.IGroups_Update,
        Fask.Interfaces.Vyroba.Groups.IGroups_Update_Row

    {
     
        #region IGroups_FillGroups Members

        public void Groups_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //Globals_V1.LoadConfiguration();
            //ds.Groups.Clear();

            //Pohoda_DataSets.VyrobaDataSet tmp = new Pohoda_DataSets.VyrobaDataSet();

            //var taGroups = new Pohoda_DataSets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //taGroups.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //taGroups.Fill(tmp.Groups);

            //foreach (var item in tmp.Groups)
            //{
            //    ds.Groups.ImportRow(item);
            //}

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Groups.Groups_Fill(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ds);

        }

        #endregion

        #region IGroups_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID.Groups_GetDataByID(string ID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Groups.Groups_GetDataByID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ID);

        }

        #endregion

        #region IGroups_Update Members

        public void Groups_Update(Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt)
        {
            //Globals_V1.LoadConfiguration();
            //var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //lta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //lta.Update(dt.ToArray());

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Groups.Update(dt, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

        }

        #endregion

        #region IGroups_Update_Row Members

        public void Groups_Update_Row(Fask.Interfaces.DataSets.Vyroba.GroupsRow groupsRow)
        {
            //Globals_V1.LoadConfiguration();
            //var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //lta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //lta.Update(groupsRow);

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Groups.Update(groupsRow, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        #endregion

        #region IGroups_Insert Members

        public void Groups_Insert(string id, string Name, string Description)
        {
            //Globals_V1.LoadConfiguration();
            //var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //lta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //lta.Insert(id, Name, Description);


            Globals_V1.LoadConfiguration();
            Database.Vyroba_Groups.Groups_Insert( Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, id, Name, Description);
        }

        #endregion
    }
}
