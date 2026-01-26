using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL
{
    public partial class Provider
    {
        /// <summary>
        /// Vstupni bod ...
        /// </summary>
        public Provider()
        {
            try
            {
                Globals_V1.LoadConfiguration();

                Fask.Server.Interfaces.Configuration.IConfiguration iconfig = this as Fask.Server.Interfaces.Configuration.IConfiguration;
                if (iconfig != null)
                    iconfig.LoadConfiguration();

                this.InitializeDataAdapters();

            }
            catch (Exception ex)
            {
				// \TODO : zalogovat chybu, poslat chybu logovacim mechanismem ... 
                throw ex;
            }
        }

        private string TABLE_CZMST_PE = "CZMST_PE";
        private string TABLE_CZMST_PI = "CZMST_PI";
        private string TABLE_CZMST_PIH = "CZMST_PIH";

        private System.Data.SqlClient.SqlDataAdapter daPI;
        private System.Data.SqlClient.SqlCommand selectCommandPI;
        private System.Data.SqlClient.SqlCommand insertCommandPI;
        private System.Data.SqlClient.SqlConnection connection1;

        private System.Data.SqlClient.SqlDataAdapter daPIH;
        private System.Data.SqlClient.SqlCommand selectCommandPIH;
        private System.Data.SqlClient.SqlCommand insertCommandPIH;


        //private bool dexrowidInsert = false;

        private void InitializeDataAdapters()
        {
            this.daPI = new System.Data.SqlClient.SqlDataAdapter();
            this.selectCommandPI = new System.Data.SqlClient.SqlCommand();
            this.insertCommandPI = new System.Data.SqlClient.SqlCommand();
            this.connection1 = new System.Data.SqlClient.SqlConnection();

            this.daPIH = new System.Data.SqlClient.SqlDataAdapter();
            this.selectCommandPIH = new System.Data.SqlClient.SqlCommand();
            this.insertCommandPIH = new System.Data.SqlClient.SqlCommand();

            // 
            // sqldaPI
            // 
            this.daPI.InsertCommand = this.insertCommandPI;
            this.daPI.SelectCommand = this.selectCommandPI;
            System.Data.ITableMapping tblmapping = this.daPI.TableMappings.Add("Table", "CZMST_PI"); //" + TABLE_CZMST_PI + "
            tblmapping.ColumnMappings.Add("CountEntries", "CountEntries");
            tblmapping.ColumnMappings.Add("PONUMBER", "PONUMBER");
            tblmapping.ColumnMappings.Add("ORD", "ORD");
            tblmapping.ColumnMappings.Add("ITEMNMBR", "ITEMNMBR");
            tblmapping.ColumnMappings.Add("VNDDOCNM", "VNDDOCNM");
            tblmapping.ColumnMappings.Add("VNDITNUM", "VNDITNUM");
            tblmapping.ColumnMappings.Add("LOCNCODE", "LOCNCODE");
            tblmapping.ColumnMappings.Add("QTYSHPPD", "QTYSHPPD");
            tblmapping.ColumnMappings.Add("QTYPACK", "QTYPACK");
            tblmapping.ColumnMappings.Add("SERLTNUM", "SERLTNUM");
            tblmapping.ColumnMappings.Add("KOD_SW", "KOD_SW");
            tblmapping.ColumnMappings.Add("DAT_VYROBY", "DAT_VYROBY");
            tblmapping.ColumnMappings.Add("DATEDONE", "DATEDONE");
            tblmapping.ColumnMappings.Add("TIMEDONE", "TIMEDONE");
            tblmapping.ColumnMappings.Add("CZ_CarKod", "CZ_CarKod");
            tblmapping.ColumnMappings.Add("REZ_1", "REZ_1");
            tblmapping.ColumnMappings.Add("REZ_2", "REZ_2");
            tblmapping.ColumnMappings.Add("USER_ID", "USER_ID");
            tblmapping.ColumnMappings.Add("DEX_ROW_ID", "DEX_ROW_ID");
            tblmapping.ColumnMappings.Add("GUID", "GUID");
            tblmapping.ColumnMappings.Add("INPUT_MODE", "INPUT_MODE");
            tblmapping.ColumnMappings.Add("ID_TERMINAL", "ID_TERMINAL");
            tblmapping.ColumnMappings.Add("MJ", "MJ");
            tblmapping.ColumnMappings.Add("QTYSHPPDMJ", "QTYSHPPDMJ");
            // 
            // sqlSelectCommandPI
            // 
            this.selectCommandPI.CommandText = "SELECT CountEntries, PONUMBER, ORD, ITEMNMBR, VNDDOCNM, VNDITNUM, LOCNCODE, QTYSH" +
                "PPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, DATEDONE, TIMEDONE, CZ_CarKod, REZ_1" +
                ", REZ_2, USER_ID, DEX_ROW_ID, GUID, INPUT_MODE, ID_TERMINAL, MJ, QTYSHPPDMJ  FROM " + TABLE_CZMST_PI + "";
            this.selectCommandPI.Connection = this.connection1;
            // 
            // sqlInsertCommandPI
            // 
            this.insertCommandPI.CommandText =
                @"INSERT INTO " + TABLE_CZMST_PI +
                " (CountEntries, PONUMBER, ORD, ITEMNMBR, VNDDOCNM, VNDITNUM, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, DATEDONE, TIMEDONE, CZ_CarKod, REZ_1, REZ_2, USER_ID" +
                //(dexrowidInsert ? ", DEX_ROW_ID" : "") +
                ", GUID, INPUT_MODE, ID_TERMINAL, MJ, QTYSHPPDMJ " +
                ") VALUES (@CountEntries, @PONUMBER, @ORD, @ITEMNMBR, @VNDDOCNM, @VNDITNUM, @LOCNCODE, @QTYSHPPD, @QTYPACK, @SERLTNUM, @KOD_SW, @DAT_VYROBY, @DATEDONE, @TIMEDONE, @CZ_CarKod, @REZ_1, @REZ_2, @USER_ID" +
                //(dexrowidInsert ? ", @DEX_ROW_ID" : "") +
                ", @GUID, @INPUT_MODE, @ID_TERMINAL, @MJ, @QTYSHPPDMJ" +
                ")";

            this.insertCommandPI.Connection = this.connection1;
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@CountEntries", System.Data.SqlDbType.Int, 4, "CountEntries")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@PONUMBER", System.Data.SqlDbType.VarChar, 17, "PONUMBER")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@ORD", System.Data.SqlDbType.Int, 4, "ORD")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@ITEMNMBR", System.Data.SqlDbType.VarChar, 31, "ITEMNMBR")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@VNDDOCNM", System.Data.SqlDbType.VarChar, 21, "VNDDOCNM")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@VNDITNUM", System.Data.SqlDbType.VarChar, 31, "VNDITNUM")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@LOCNCODE", System.Data.SqlDbType.VarChar, 11, "LOCNCODE")));

            //this.insertCommandPI.Parameters.Add(new SqlParameter(System.Data.SqlDbType, "@QTYSHPPD", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(19)), ((System.Byte)(5)), "QTYSHPPD", System.Data.DataRowVersion.Current, null));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@QTYSHPPD", System.Data.SqlDbType.Decimal, 9, "QTYSHPPD")));
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYSHPPD"]).Direction = System.Data.ParameterDirection.Input;
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYSHPPD"]).SourceVersion = System.Data.DataRowVersion.Current;
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYSHPPD"]).Value = null;
            //this.insertCommandPI.Parameters.Add(new System.Data.SqlClient.SqlParameter("@QTYPACK", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(19)), ((System.Byte)(5)), "QTYPACK", System.Data.DataRowVersion.Current, null));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@QTYPACK", System.Data.SqlDbType.Decimal, 9, "QTYPACK")));
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYPACK"]).Direction = System.Data.ParameterDirection.Input;
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYPACK"]).SourceVersion = System.Data.DataRowVersion.Current;
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYPACK"]).Value = null;

            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@SERLTNUM", System.Data.SqlDbType.VarChar, 21, "SERLTNUM")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@KOD_SW", System.Data.SqlDbType.VarChar, 11, "KOD_SW")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@DAT_VYROBY", System.Data.SqlDbType.VarChar, 11, "DAT_VYROBY")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@DATEDONE", System.Data.SqlDbType.VarChar, 8, "DATEDONE")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@TIMEDONE", System.Data.SqlDbType.VarChar, 6, "TIMEDONE")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@CZ_CarKod", System.Data.SqlDbType.VarChar, 31, "CZ_CarKod")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@REZ_1", System.Data.SqlDbType.VarChar, 21, "REZ_1")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@REZ_2", System.Data.SqlDbType.VarChar, 21, "REZ_2")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int, 4, "USER_ID")));
            //if (dexrowidInsert)
            //{
            //    this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@DEX_ROW_ID", System.Data.SqlDbType.Int, 4, "DEX_ROW_ID")));
            //}
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@GUID", System.Data.SqlDbType.UniqueIdentifier, 4, "GUID")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@INPUT_MODE", System.Data.SqlDbType.TinyInt, 1, "INPUT_MODE")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@ID_TERMINAL", System.Data.SqlDbType.Int, 4, "ID_TERMINAL")));

            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@MJ", System.Data.SqlDbType.NVarChar, 10, "MJ")));
            this.insertCommandPI.Parameters.Add((new System.Data.SqlClient.SqlParameter("@QTYSHPPDMJ", System.Data.SqlDbType.Decimal, 9, "QTYSHPPDMJ")));
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYSHPPDMJ"]).Direction = System.Data.ParameterDirection.Input;
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYSHPPDMJ"]).SourceVersion = System.Data.DataRowVersion.Current;
            ((System.Data.Common.DbParameter)this.insertCommandPI.Parameters["@QTYSHPPDMJ"]).Value = null;

            // 
            // sqldaPIH
            // 
            this.daPIH.InsertCommand = this.insertCommandPIH;
            this.daPIH.SelectCommand = this.selectCommandPIH;

            this.selectCommandPIH.CommandText = "SELECT * FROM " + TABLE_CZMST_PIH;
            this.insertCommandPIH.CommandText = "INSERT INTO " + TABLE_CZMST_PIH +
                " (CountEntries, DATUMDOKLADU)" +
                " VALUES" +
                " (@CountEntries, @DATUMDOKLADU)";
            this.insertCommandPIH.Parameters.Add((new System.Data.SqlClient.SqlParameter("@CountEntries", System.Data.SqlDbType.Int, 4, "CountEntries")));
            this.insertCommandPIH.Parameters.Add((new System.Data.SqlClient.SqlParameter("@DATUMDOKLADU", System.Data.SqlDbType.DateTime, 8, "DATUMDOKLADU")));

            this.selectCommandPIH.Connection = this.connection1;
            this.insertCommandPIH.Connection = this.connection1;

        }

    }
}
