using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider  :
                Fask.Console.Interfaces.Vyroba.Login.ILogin,
        Fask.Console.Interfaces.Vyroba.Login.ILogin_FillLogin,
        Fask.Console.Interfaces.Vyroba.Login.ILogin_Update,
        Fask.Console.Interfaces.Vyroba.Login.ILogin_Update_Row,
        Fask.Console.Interfaces.Vyroba.Login.ILogin_Insert,
        Fask.Console.Interfaces.Vyroba.Login.ILogin_GetDataByID
    {

      
        #region ILogin_FillLogin Members

        public void FillLogin(Console.Interfaces.DataSets.Vyroba dt)
        {
            Globals.LoadConfiguration();
            dt.Logins.Clear();

            System.Data.SqlClient.SqlDataAdapter da_filter = new System.Data.SqlClient.SqlDataAdapter();
            da_filter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            da_filter.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);

            da_filter.SelectCommand.CommandText = "Select l.*, g.name groupname " +
                "from Logins l " +
                "left join VLoginsGroups vg on l.id = vg.loginid " +
                "left join Groups g on vg.groupid = g.id";
            da_filter.Fill(dt.Logins);

        }

        #endregion

        #region ILogin_Update Members

        public int Update(Console.Interfaces.DataSets.Vyroba.LoginsDataTable dt)
        {
            Globals.LoadConfiguration();
            var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.LoginsTableAdapter();
            lta.Connection = new SqlConnection(Globals.ConnectionString);

            return lta.Update(dt.ToArray());

        }

        #endregion

        #region ILogin_Update_Row Members

        public int Update_Row(Console.Interfaces.DataSets.Vyroba.LoginsRow row)
        {
            Globals.LoadConfiguration();
            var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.LoginsTableAdapter();
            lta.Connection = new SqlConnection(Globals.ConnectionString);

            return lta.Update(row);
        }

        #endregion

        #region ILogin_Insert Members

        public void Insert(string id, string firstname, string surname, string psswd, byte VS)
        {
            Globals.LoadConfiguration();
            var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.LoginsTableAdapter();
            lta.Connection = new SqlConnection(Globals.ConnectionString);

            lta.Insert(id, firstname, surname, psswd, VS);
        }

        #endregion

        #region ILogin_GetDataByID Members

        public Console.Interfaces.DataSets.Vyroba.LoginsDataTable GetDataByID(string ID)
        {
            Globals.LoadConfiguration();
            Console.Interfaces.DataSets.Vyroba.LoginsDataTable dt = new Console.Interfaces.DataSets.Vyroba.LoginsDataTable();

            var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.LoginsTableAdapter();
            ta.Connection = new SqlConnection(Globals.ConnectionString);

            var tmp = ta.GetDataByID(ID);


            foreach (var item in tmp)
            {
                dt.ImportRow(item);
            }


            return dt;
        }

        #endregion



    }
}
