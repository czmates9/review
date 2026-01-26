using Fask.Logging;
using FASK.Logins.DataSets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FASK.Logins.Komunikace
{
    public class SQL_Commans : ICommans
    {
		string _ConnectionString;

		public SQL_Commans() { }

		public SQL_Commans(string CS)
        {
			_ConnectionString = CS;

		}

		public Pristupy GetViewData(string USERID)
        {
            Pristupy prava = new Pristupy();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "Select * from FASK_Logins_View_Prava"
                //+ " Where USERID=@USERID AND psswd = @psswd";
                + " Where USERID=@USERID ";
                da.SelectCommand.Parameters.AddWithValue("@USERID", USERID);
                //da.SelectCommand.Parameters.AddWithValue("@psswd", Heslo);

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(this._ConnectionString);

                da.Fill(prava.FASK_Logins_View_Prava);
            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(ex);
                //Fask.Logging.ExceptionHandler2.Handle(prava);
				throw ex;
                //return null;
            }

            return prava;

        }

		public DataSets.Pristupy GetLikeAgednaID(string AgendaID)
		{
			DataSets.Pristupy prava = new DataSets.Pristupy();

			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.SqlClient.SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID "
                + " WHERE A.AGENDAID LIKE  '" + AgendaID + "%' OR AGENDAID='_'"
                + " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, L.RFID ";


                //da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID "
                //+ " WHERE A.AGENDAID LIKE  '" + AgendaID + "%' OR AGENDAID='_'"
                //+ " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO ";


                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(this._ConnectionString);

				da.Fill(prava.FASK_Logins);
			}
			catch (Exception ex)
			{
				//Fask.Logging.ExceptionHandler2.Handle(ex);
				//Fask.Logging.ExceptionHandler2.Handle(prava);
				throw ex;
				//return null;
			}

			return prava;

		}

		public DataSets.Pristupy GetAgednaID(string AgendaID)
		{
			DataSets.Pristupy prava = new DataSets.Pristupy();

			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.SqlClient.SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID "
				+ " WHERE A.AGENDAID =  '" + AgendaID + "'"
				+ " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, L.RFID ";

				da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(this._ConnectionString);

				da.Fill(prava.FASK_Logins);
			}
			catch (Exception ex)
			{
				//Fask.Logging.ExceptionHandler2.Handle(ex);
				//Fask.Logging.ExceptionHandler2.Handle(prava);
				throw ex;
				//return null;
			}

			return prava;

		}

        public DataSets.Pristupy.FASK_LoginsDataTable GetLoginsByID(string AgendaID)
        {
            DataSets.Pristupy.FASK_LoginsDataTable prava = new DataSets.Pristupy.FASK_LoginsDataTable();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT * FROM FASK_Logins "
                + " WHERE USERID =  '" + AgendaID + "'";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(this._ConnectionString);

                da.Fill(prava);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prava;

        }

        public DataSets.Pristupy.FASK_LoginsDataTable GetLogins()
        {
            DataSets.Pristupy.FASK_LoginsDataTable prava = new DataSets.Pristupy.FASK_LoginsDataTable();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT * FROM FASK_Logins ";
                //+ " WHERE AGENDAID =  '" + AgendaID + "'";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(this._ConnectionString);

                da.Fill(prava);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prava;

        }

		public DataSets.Pristupy.FASK_LoginsDataTable GetOverLogins(string USERID, string PWD, string AGENDA)
		{
			DataSets.Pristupy.FASK_LoginsDataTable prava = new DataSets.Pristupy.FASK_LoginsDataTable();


			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.SqlClient.SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				//new---------------------------------------------------------
				da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID " +
								 "WHERE  1=1 AND L.USERID = @USERID AND L.PSSWD = @PWD";

				if (!string.IsNullOrEmpty(AGENDA))
				{
					da.SelectCommand.CommandText += " AND A.AGENDAID LIKE @AGENDA";
					da.SelectCommand.Parameters.AddWithValue("@AGENDA", AGENDA + "%");
				}

				da.SelectCommand.CommandText += " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, L.RFID ";
				da.SelectCommand.Parameters.AddWithValue("@USERID", USERID);
				da.SelectCommand.Parameters.AddWithValue("@PWD", PWD);


				using (SqlConnection connection = new SqlConnection(this._ConnectionString))
				{
					da.SelectCommand.Connection = connection;
					connection.Open();
					da.Fill(prava);
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn, ex.Message);

				throw ex;
			}

			return prava;

		}
    }
}
