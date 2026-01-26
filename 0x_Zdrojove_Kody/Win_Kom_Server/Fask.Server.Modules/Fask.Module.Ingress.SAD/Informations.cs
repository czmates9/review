using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD
{
    public partial class Provider : Fask.Server.Interfaces.Informations.IInformations2,
        Fask.Server.Interfaces.Informations.IInformations2_Command1,
        Fask.Server.Interfaces.Informations.IInformations2_DetailItemnumber,
        Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade,
         Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade_Itemnumber_Location
    {
        public DataSet Command1(string param1, string param2)
        {
            try
            {
                Globals.LoadConfiguration();

                IngresConnection xconn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                IngresCommand xcomm = new IngresCommand(Globals.Konfigurace.Informations[0].command1_proc, xconn);
                xcomm.CommandType = CommandType.StoredProcedure;
                xcomm.Parameters.Add((new IngresParameter(Globals.Konfigurace.Informations[0].command1_paramName1, param1)));
                xcomm.Parameters.Add((new IngresParameter(Globals.Konfigurace.Informations[0].command1_paramName2, param2)));

                DataSet ds = new DataSet();
                ds.DataSetName = Globals.Konfigurace.Informations[0].command1_DataSetName;
                ds.Locale = new System.Globalization.CultureInfo("en");
                IngresDataAdapter xda = new IngresDataAdapter();
                xda.SelectCommand = xcomm;
                xda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: Command1[" + param1 + "," + param2 + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public DataSet DetailItemnumber(string itemnumber, string doklad)
        {
            try
            {
                Globals.LoadConfiguration();

                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].DetailItemnumber_CommandType);
                string detailItemnumber_proc = Globals.Konfigurace.Informations[0].DetailItemnumber;
                string detailItemnumber_proc_paramName1 = Globals.Konfigurace.Informations[0].DetailItemnumber_paramName1;
                string detailItemnumber_proc_paramName2 = Globals.Konfigurace.Informations[0].DetailItemnumber_paramName2;

                IngresConnection xconn = null;
                IngresCommand xcomm = null;
                xconn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new IngresCommand();
                xcomm.CommandType = commandType;
                if (commandType == CommandType.StoredProcedure)
                {
                    xcomm.CommandText = detailItemnumber_proc;
                    xcomm.Parameters.Add(new IngresParameter(detailItemnumber_proc_paramName1, itemnumber));
                    xcomm.Parameters.Add(new IngresParameter(detailItemnumber_proc_paramName2, doklad));
                }
                else if (commandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + detailItemnumber_proc + " where " + detailItemnumber_proc_paramName1 + "='" + itemnumber + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }

                xcomm.Connection = xconn;

                DataSet data = new DataSet();
                IngresDataAdapter xda = new IngresDataAdapter();
                xda.SelectCommand = xcomm;
                xda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: DetailItemnumber[" + itemnumber + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public float MnozstviNaSklade(string ItemNumber)
        {
            float mnozstvi = 0f;
            IngresConnection xconn = null;
            IngresCommand xcomm = null;
            try
            {
                Globals.LoadConfiguration();

                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].MnozstviNaSklade_CommandType);
                string mnozstvinasklade_proc_paramName = Globals.Konfigurace.Informations[0].MnozstviNaSklade_paramName ;
                string mnozstvinasklade_proc = Globals.Konfigurace.Informations[0].MnozstviNaSklade;

                xconn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new IngresCommand();
                xcomm.CommandType = commandType;
                if (commandType == CommandType.StoredProcedure)
                {
                    xcomm.Parameters.Add((new IngresParameter(mnozstvinasklade_proc_paramName, ItemNumber)));
                    xcomm.CommandText = mnozstvinasklade_proc;
                }
                else if (commandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + mnozstvinasklade_proc + " where " + mnozstvinasklade_proc_paramName + "='" + ItemNumber + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }
                xcomm.Connection = xconn;
                xconn.Open();
                object o = xcomm.ExecuteScalar();
                if (o is System.DBNull)
                    mnozstvi = 0;
                else
                    mnozstvi = Convert.ToSingle(o);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: MnozstviNaSklade[" + ItemNumber + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
            finally
            {
                if (xconn != null && xconn.State == ConnectionState.Open)
                {
                    xconn.Close();
                }
            }
            return mnozstvi;
        }

        public float? MnozstviNaSklade_Itemnumber_Location(string ItemNumber, string Location)
        {
            float? mnozstvi = null;
            IngresConnection xconn = null;
            IngresCommand xcomm = null;
            try
            {
                Globals.LoadConfiguration();
                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].MnozstviNaSklade_I_L_CommandType);
                string mnozstvinasklade_proc2 = Globals.Konfigurace.Informations[0].MnozstviNaSklade2;
                string mnozstvinasklade_proc2_paramName1 = Globals.Konfigurace.Informations[0].MnozstviNaSklade2_paramName1;
                string mnozstvinasklade_proc2_paramName2 = Globals.Konfigurace.Informations[0].MnozstviNaSklade2_paramName2;

                xconn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new IngresCommand();
                xcomm.CommandType = commandType;
                if (commandType == CommandType.StoredProcedure)
                {
                    xcomm.CommandText = mnozstvinasklade_proc2;
                    xcomm.Parameters.Add((new IngresParameter(mnozstvinasklade_proc2_paramName1, ItemNumber)));
                    xcomm.Parameters.Add((new IngresParameter(mnozstvinasklade_proc2_paramName2, Location)));
                }
                else if (commandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + mnozstvinasklade_proc2 + " where " + mnozstvinasklade_proc2_paramName1 + "='" + ItemNumber + "' and " + mnozstvinasklade_proc2_paramName2 + "='" + Location + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }
                xcomm.Connection = xconn;
                xconn.Open();
                object o = xcomm.ExecuteScalar();
                if (o is System.DBNull || o == null)
                    mnozstvi = null;
                else
                    mnozstvi = Convert.ToSingle(o);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: MnozstviNaSklade[" + ItemNumber + "," + Location + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
            finally
            {
                if (xconn != null && xconn.State == ConnectionState.Open)
                {
                    xconn.Close();
                }
            }
            return mnozstvi;
        }
    }
}
