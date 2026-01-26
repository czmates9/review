namespace Fask.ModuleSql.SQL_Datasets
{
    public partial class VyrobaDataSet
    {
        partial class ProductionDataTable
        {
        }
    }
}


namespace Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters
{
    public partial class QueriesTableAdapter
    {
        public void ConncetionStringChange(string connectionstring)
        {
            foreach (var command in this.CommandCollection)
            {
                command.Connection.ConnectionString = connectionstring;
            }
        }

        public object mtj_fask_production_CheckOp_GetReturnValue()
        {
            global::System.Data.SqlClient.SqlCommand command = ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[0]));
            return command.Parameters[0].Value;
        }
    }

    public partial class mtj_fask_production_CheckOpTableAdapter
    {
        public object mtj_fask_production_CheckOp_GetReturnValue()
        {
            global::System.Data.SqlClient.SqlCommand command = ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[0]));
            return command.Parameters[0].Value;
        }
    }

}
