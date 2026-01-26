using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.ModuleSql.Database;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;

namespace Fask.ModuleSql
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Machines.IMachines,
        Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID,
        Fask.Interfaces.Vyroba.Machines.IMachines_Fill,
        Fask.Interfaces.Vyroba.Machines.IMachines_GetFilterData,
        Fask.Interfaces.Vyroba.Machines.IMachines_IudCommand
    {
  
        #region IMachines_GetDataByID Members

        public Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Machines_GetDataByID(string ID)
        {
            //Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            return Vyroba_Machines.Machines_GetDataByID(ConnectionString, ID);
        }

        #endregion

        #region IMachines_Fill Members

        public void Machines_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //SQL_Datasets.VyrobaDataSet tmp = new SQL_Datasets.VyrobaDataSet();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //ta.Fill(tmp.Machines);

            //foreach (var item in tmp.Machines)
            //{
            //    ds.Machines.ImportRow(item);
            //}

            Vyroba_Machines.Machines_Fill(ConnectionString, ds);
        }

        public Fask.Interfaces.DataSets.Vyroba Machines_GetFilterData(Odvod_MachineStateSetListFiltr filtr)
        {
            return Vyroba_Machines.Machines_GetFilterData(ConnectionString, filtr);
        }



        #endregion

        #region IUD

        public bool DeleteMachines(Vyroba.MachinesRow MachinesRow)
        {
            int pocetUpdateRadku = 0;
            pocetUpdateRadku = Vyroba_Machines.DeleteById(MachinesRow.id, ConnectionString);

            if (pocetUpdateRadku > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertMachines(Vyroba.MachinesRow MachinesRow)
        {
            int pocetUpdateRadku = 0;
            pocetUpdateRadku = Vyroba_Machines.Update(MachinesRow, ConnectionString);

            if (pocetUpdateRadku > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateMachines(Vyroba.MachinesRow MachinesRow)
        {
            int pocetUpdateRadku = 0;
            pocetUpdateRadku= Vyroba_Machines.Update(MachinesRow,ConnectionString);

            if (pocetUpdateRadku >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
