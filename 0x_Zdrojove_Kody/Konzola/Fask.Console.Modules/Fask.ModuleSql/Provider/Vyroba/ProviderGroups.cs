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
            //SQL_Datasets.VyrobaDataSet tmp = new SQL_Datasets.VyrobaDataSet();

            //var taGroups = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //taGroups.Connection = new SqlConnection(ConnectionString);

            //taGroups.Fill(tmp.Groups);

            //foreach (var item in tmp.Groups)
            //{
            //    ds.Groups.ImportRow(item);
            //}

            Vyroba_Groups.Groups_Fill(ConnectionString, ds);

        }

        #endregion

        #region IGroups_GetDataByID Members

        public Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Groups_GetDataByID(string ID)
        {
            //Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            return Vyroba_Groups.Groups_GetData(ConnectionString, ID);
        }

        #endregion

        #region IGroups_Update Members

        public void Groups_Update(Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt)
        {
            //var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //lta.Connection = new SqlConnection(ConnectionString);

            //lta.Update(dt.ToArray());

            Vyroba_Groups.Update(dt, ConnectionString);
        }

        #endregion

        #region IGroups_Update_Row Members

        public void Groups_Update_Row(Fask.Interfaces.DataSets.Vyroba.GroupsRow groupsRow)
        {
            //var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //lta.Connection = new SqlConnection(ConnectionString);

            //lta.Update(groupsRow);

            Vyroba_Groups.Update(groupsRow, ConnectionString);
        }

        #endregion

        #region IGroups_Insert Members

        public void Groups_Insert(string id, string Name, string Description)
        {
            //var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            //lta.Connection = new SqlConnection(ConnectionString);

            //lta.Insert(id, Name, Description);

            Vyroba_Groups.Groups_Insert(ConnectionString,id, Name, Description);
        }

        #endregion
    }
}
