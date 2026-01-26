using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Provider.Vyroba
{
    public partial class Provider :
       Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr,
       Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr_GetFiltrovanyOdvodEvents
    {
        #region IOdvod_EventsErr


        #region IOdvod_EventsErr_GetFiltrovanyOdvodEvents Members

        public Fask.Interfaces.DataSets.Vyroba EventsErr_GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsErrListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                command.CommandText =
                        "SELECT E.* FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr + " as E " +
                        " WHERE " +
                        " 1=1 ";


                if (filtr.productionGUID != null)
                {

                    //command.CommandText += "and CAST(productionGUID as uniqueidentifier) = CAST(@productionGUID as uniqueidentifier) ";
                    //command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);


                    command.CommandText += "and E.productionGUID = @productionGUID";
                    command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);

                    //command.CommandText += "and productionGUID ='" + filtr.productionGUID.ToString().Trim() + "' ";
                }


                if (filtr.Zpracovane && !filtr.NEZpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NOT NULL ";
                }
                else if (filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL ";
                }
                else if (!filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL and E.IsProcessed is NOT NULL ";

                }




                // hledání podle datumu
                if (filtr.IsProcessed_OD != null && filtr.IsProcessed_DO != null)
                {
                    command.CommandText += " AND E.IsProcessed between @IsProcessedOD and @IsProcessedDO";
                    command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                }
                else
                {
                    if (filtr.IsProcessed_OD != null)
                    {
                        command.CommandText += " AND E.IsProcessed > @IsProcessedOD";
                        command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    }
                    else if (filtr.IsProcessed_DO != null)
                    {
                        command.CommandText += " AND E.IsProcessed < @IsProcessedDO";
                        command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                    }
                }

                if (!string.IsNullOrEmpty(filtr.IsProcessed_TimeVariant))
                {
                    if (!filtr.IsProcessed_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.IsProcessed_TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                        command.CommandText += " AND E.IsProcessed > @IsProcessed_TV";
                        command.Parameters.AddWithValue("@IsProcessed_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (filtr.Dateeve_OD != null && filtr.Dateeve_DO != null)
                {
                    command.CommandText += " AND E.dateeve between @dateeveOD and @dateeveDO";
                    command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                }
                else
                {
                    if (filtr.Dateeve_OD != null)
                    {
                        command.CommandText += " AND E.dateeve > @dateeveOD";
                        command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    }
                    else if (filtr.Dateeve_DO != null)
                    {
                        command.CommandText += " AND E.dateeve < @dateeveDO";
                        command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                    }
                }


                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {
                    if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.Dateeve_TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                        command.CommandText += " AND E.dateeve > @dateeve_TV";
                        command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (!string.IsNullOrEmpty(filtr.MachineID))
                {
                    command.CommandText += " AND E.machineid = @machineid ";
                    command.Parameters.AddWithValue("@machineid", filtr.MachineID);
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    command.CommandText += " AND CAST(E.[description] AS NVARCHAR(MAX)) like '" + filtr.Description + "%'";
                }

                if (!string.IsNullOrEmpty(filtr.status))
                {
                    command.CommandText += " AND E.status = @status ";
                    command.Parameters.AddWithValue("@status", filtr.status);
                }

                if (!string.IsNullOrEmpty(filtr.PackType))
                {
                    command.CommandText += " AND E.PackType = @PackType ";
                    command.Parameters.AddWithValue("@PackType", filtr.PackType);
                }

                if (!string.IsNullOrEmpty(filtr.Razeni_Column))
                {
                    command.CommandText += " order by " + "E." + filtr.Razeni_Column + " " + filtr.asc_desc;
                }
                else
                {
                    command.CommandText += " order by " + "E.dateeve desc";
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_EventsErr);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #endregion


    }
}
