using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModuleSql
{
    public partial class Provider : 
        Fask.Interfaces.Ukolovani.IUkolovani,
        Fask.Interfaces.Ukolovani.IUkolovani_GetCZ_UKOL_STATE,
        Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL,
        Fask.Interfaces.Ukolovani.IUkolovani_GetDataByUkolIDAndUserID_CZ_UKOL_UZIV,
        Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL_UZIV,
        Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL,
        Fask.Interfaces.Ukolovani.IUkolovani_Delete_CZ_UKOL,
        Fask.Interfaces.Ukolovani.IUkolovani_DeleteByUkolID_CZ_UKOL_UZIV,
        Fask.Interfaces.Ukolovani.IUkolovani_GetDataByState_CZ_UKOL_STATE,
        Fask.Interfaces.Ukolovani.IUkolovani_Update_CZ_UKOL,
        Fask.Interfaces.Ukolovani.IUkolovani_Update_CZ_UKOL_UZIV,
        Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL_STATE,
        Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL_UZIV,
        Fask.Interfaces.Ukolovani.IUkolovani_Update_Row_CZ_UKOL_UZIV,
        Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL,
        Fask.Interfaces.Ukolovani.IUkolovani_Update_Row_CZ_UKOL
    {

        #region IUkolovani_GetCZ_UKOL_STATE Members

        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable GetCZ_UKOL_STATE()
        {
            Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable dtout = new Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable();

            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var dt = ta.GetData();

            foreach (var item in dt)
            {
                var ROW = dtout.NewCZ_UKOL_STATERow();

                ROW.State = string.IsNullOrEmpty(item.State) ? string.Empty : item.State;
                ROW.Description = string.IsNullOrEmpty(item.Description) ? string.Empty : item.Description;
                ROW.IsStart = item.IsStart;
                ROW.IsEnd = item.IsEnd;
                ROW.Color = item.IsColorNull() ? null : item.Color;

                dtout.AddCZ_UKOL_STATERow(ROW);
            }


            return dtout;
        }

        #endregion

        #region IUkolovani_Fill_CZ_UKOL Members

        public bool Fill_CZ_UKOL(Fask.Interfaces.DataSets.Ukolovani ds)
        {

            SQL_Datasets.UkolovaniDataset.CZ_UKOLDataTable dt = new SQL_Datasets.UkolovaniDataset.CZ_UKOLDataTable();
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Fill(dt);

            foreach (var item in dt)
            {

                ds.CZ_UKOL.ImportRow(item);
                //var ROW = ds.CZ_UKOL.NewCZ_UKOLRow();

                ////ROW.State = string.IsNullOrEmpty(item.State) ? string.Empty : item.State;
                ////ROW.Description = string.IsNullOrEmpty(item.Description) ? string.Empty : item.Description;
                ////ROW.IsStart = item.IsStart;
                ////ROW.IsEnd = item.IsEnd;
                ////ROW.Color = item.IsColorNull() ? null : item.Color;

                //ROW.ID = item.ID;
                //ROW.Name = item.Name;
                //ROW.Description = item.Description;
                //ROW.Code = item.IsCodeNull() ? null : item.Code ;
                //ROW.CreatorID = item.CreatorID;
                //ROW.DateCreated = item.DateCreated;
                //ROW.DateFrom = item.IsDateFromNull() ? null : item.DateFrom;
                //ROW.DateTo = item.IsDateToNull() ? null : item.DateTo;
                //ROW.State = item.State;
                //ROW.Kind = item.IsKindNull() ? null : item.Kind;
                //ROW.Type = item.IsTypeNull() ? null : item.Type;
                //ROW.Priority = item.Priority;
                //ROW.PartnerID = item.IsPartnerIDNull() ? null : item.PartnerID;

                //ds.CZ_UKOL.AddCZ_UKOLRow(ROW);
            }


            return true;
        }

        #endregion

        #region IUkolovani_GetDataByUkolIDAndUserID_CZ_UKOL_UZIV Members

        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable GetDataByUkolIDAndUserID(int UkolID, int UserID)
        {



            Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable dtout = new Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable();

            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var dt = ta.GetDataByUkolIDAndUserID( UkolID,  UserID);

            foreach (var item in dt)
            {
                dtout.ImportRow(item);
                //var ROW = dtout.NewCZ_UKOL_STATERow();

                //ROW.State = string.IsNullOrEmpty(item.State) ? string.Empty : item.State;
                //ROW.Description = string.IsNullOrEmpty(item.Description) ? string.Empty : item.Description;
                //ROW.IsStart = item.IsStart;
                //ROW.IsEnd = item.IsEnd;
                //ROW.Color = item.IsColorNull() ? null : item.Color;

                //dtout.AddCZ_UKOL_STATERow(ROW);
            }


            return dtout;

        }

        #endregion

        #region IUkolovani_Insert_CZ_UKOL_UZIV Members

        public void Insert(int UkolID, int UserID, string State, DateTime? DateChanged, int? UserIDChanged, string Note, DateTime? DateNotify, DateTime? DateFinished)
        {


            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var dt = ta.InsertQuery( UkolID,  UserID,  State,  DateChanged,  UserIDChanged,  Note,  DateNotify,  DateFinished);

            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;

            //try
            //{
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();
            //    trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);


            //    string CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZ_UKOL_UZIV + " (UkolID, UserID, State, DateChanged, UserIDChanged, Note, DateNotify, DateFinished) VALUES ";
            //    CommandText +=  " (@UkolID,@UserID,@State,@DateChanged,@UserIDChanged,@Note,@DateNotify,@DateFinished); ";

            //    //CommandText+= "-- vraceni id zaznamu ";
            //    CommandText+= "SELECT SCOPE_IDENTITY()";


            //    //cinnostRow.IsBarcodeNull() ? (object)DBNull.Value : cinnostRow.Barcode

            //    command = new System.Data.SqlClient.SqlCommand(CommandText, connection, trans);
            //    command.Parameters.AddWithValue("@UkolID", UkolID);
            //    command.Parameters.AddWithValue("@UserID", UserID);
            //    command.Parameters.AddWithValue("@State", string.IsNullOrEmpty(State) ? (object)DBNull.Value : State);

            //    command.Parameters.AddWithValue("@DateChanged", DateChanged == null ? (object)DBNull.Value : DateChanged);
            //    command.Parameters.AddWithValue("@UserIDChanged", UserIDChanged == null ? (object)DBNull.Value : UserIDChanged);
            //    command.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note) ? (object)DBNull.Value : Note);
            //    command.Parameters.AddWithValue("@DateNotify", DateNotify == null ? (object)DBNull.Value : DateNotify);
            //    command.Parameters.AddWithValue("@DateFinished", DateFinished == null ? (object)DBNull.Value : DateFinished);

            //    command.ExecuteNonQuery();

            //    if (trans != null)
            //        trans.Commit();

            //   //return true;
            //}
            //catch
            //{
            //    try
            //    {
            //        if (trans != null)
            //            trans.Rollback();
            //    }
            //    catch { }
            //    throw;
            //}
            //finally
            //{
            //    if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
            //        connection.Close();
            //}
        }

        #endregion

        #region IUkolovani_Insert_CZ_UKOL Members

        public int Insert(string Name, string Description, string Code, int CreatorID, DateTime DateCreated, DateTime? DateFrom, DateTime? DateTo, string State, string Kind, string Type, int Priority, string PartnerID)
        {

            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var oout = ta.InsertQuery( Name,  Description,  Code,  CreatorID,  DateCreated,  DateFrom,  DateTo,  State,  Kind,  Type,  Priority,  PartnerID);

            if (oout is int)
            {
                return (int)oout;
            }
            else
            {
                throw new Exception("no insert...");
            }

            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;

            //try
            //{
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();
            //    trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);


            //    string CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZ_UKOL + " (Name, Description, Code, CreatorID, DateCreated, DateFrom, DateTo, State, Kind, Type, Priority, PartnerID) VALUE ";
            //    CommandText += " (@Name,@Description,@Code,@CreatorID,@DateCreated,@DateFrom,@DateTo,@State,@Kind,@Type,@Priority,@PartnerID); ";

            //    //CommandText+= "-- vraceni id zaznamu ";
            //    CommandText += "SELECT SCOPE_IDENTITY()";


            //    //cinnostRow.IsBarcodeNull() ? (object)DBNull.Value : cinnostRow.Barcode

            //    command = new System.Data.SqlClient.SqlCommand(CommandText, connection, trans);
            //    command.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(Name) ? (object)DBNull.Value : Name);
            //    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(Description) ? (object)DBNull.Value : Description);
            //    command.Parameters.AddWithValue("@Code", string.IsNullOrEmpty(Code) ? (object)DBNull.Value : Code);

            //    command.Parameters.AddWithValue("@CreatorID", CreatorID);
            //    command.Parameters.AddWithValue("@DateCreated", DateCreated);

            //    command.Parameters.AddWithValue("@DateFrom", DateFrom == null ? (object)DBNull.Value : DateFrom);
            //    command.Parameters.AddWithValue("@DateTo", DateTo == null ? (object)DBNull.Value : DateTo);


            //    command.Parameters.AddWithValue("@State", string.IsNullOrEmpty(State) ? (object)DBNull.Value : State);
            //    command.Parameters.AddWithValue("@Kind", string.IsNullOrEmpty(Kind) ? (object)DBNull.Value : Kind);
            //    command.Parameters.AddWithValue("@Type", string.IsNullOrEmpty(Type) ? (object)DBNull.Value : Type);

            //    command.Parameters.AddWithValue("@Priority", Priority);

            //    command.Parameters.AddWithValue("@PartnerID", string.IsNullOrEmpty(PartnerID) ? (object)DBNull.Value : PartnerID);

            //    int ID = command.ExecuteNonQuery();

            //    if (trans != null)
            //        trans.Commit();

            //    return ID;
            //}
            //catch
            //{
            //    try
            //    {
            //        if (trans != null)
            //            trans.Rollback();
            //    }
            //    catch { }
            //    throw;
            //}
            //finally
            //{
            //    if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
            //        connection.Close();
            //}
        }

        #endregion

        #region IUkolovani_Delete_CZ_UKOL Members

        public void Delete(int ID)
        {
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Delete(ID);
        }

        #endregion

        #region IUkolovani_DeleteByUkolID_CZ_UKOL_UZIV Members

        public void DeleteByUkolID(int ID)
        {
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.DeleteByUkolID(ID);
        }

        #endregion

        #region IUkolovani_GetDataByState_CZ_UKOL_STATE Members

        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable GetDataByState(string State)
        {
            Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable dtout = new Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable();

            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var dt = ta.GetDataByState(State);

            foreach (var item in dt)
            {
                dtout.ImportRow(item);
            }

            return dtout;
        }

        #endregion

        #region IUkolovani_Update_CZ_UKOL Members

        public void Update(Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLDataTable dt)
        {
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Update(dt.ToArray());
        }

        #endregion

        #region IUkolovani_Update_CZ_UKOL_UZIV Members

        public void Update(Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable dt)
        {
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Update(dt.ToArray());
        }

        #endregion

        #region IUkolovani_Fill_CZ_UKOL_STATE Members

        public bool Fill_CZ_UKOL_STATE(Fask.Interfaces.DataSets.Ukolovani ds)
        {
            SQL_Datasets.UkolovaniDataset.CZ_UKOL_STATEDataTable dt = new SQL_Datasets.UkolovaniDataset.CZ_UKOL_STATEDataTable();
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Fill(dt);

            foreach (var item in dt)
            {

                ds.CZ_UKOL.ImportRow(item);
            }


            return true;
        }

        #endregion

        #region IUkolovani_GetDataByID_CZ_UKOL_UZIV Members

        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable GetDataByID(int ID)
        {
            Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable dtout = new Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable();

            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var dt = ta.GetDataByID(ID);

            foreach (var item in dt)
            {
                dtout.ImportRow(item);                
            }

            return dtout;
        }

        #endregion

        #region IUkolovani_Update_Row_CZ_UKOL_UZIV Members

        public void UpdateRow(Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVRow dt)
        {
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Update(dt);
        }

        #endregion

        #region IUkolovani_GetDataByID_CZ_UKOL Members

        Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLDataTable Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL.GetDataByID(int ID)
        {
            Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLDataTable dtout = new Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLDataTable();

            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            var dt = ta.GetDataByID(ID);

            foreach (var item in dt)
            {
                dtout.ImportRow(item);
            }

            return dtout;
        }

        #endregion

        #region IUkolovani_Update_Row_CZ_UKOL Members

        public void UpdateRow(Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow dt)
        {
            SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter ta = new SQL_Datasets.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            ta.Update(dt);
        }

        #endregion
    }
}
