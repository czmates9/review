namespace Fask.ModuleSql.SQL_Datasets
{


    public partial class UkolovaniDataset
    {
        partial class CZ_UKOL_UZIVDataTable
        {
        }

        public partial class CZMSTPWDRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.SECONDNAME.Trim() + " " + this.FIRSTNAME.Trim() + " (" + this.LOGIN.Trim() + ")";
            }
        }

        public partial class CZ_UKOL_STATERow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.Description.Trim() + " (" + this.State.Trim() + ")";
            }
        }

        public partial class CZ_UKOLRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.Name.Trim() + " (" + this.Description.Trim() + ")";
            }
        }
    }


}

namespace Fask.ModuleSql.SQL_Datasets.UkolovaniDatasetTableAdapters
{
    public partial class CZ_UKOL_UZIVTableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction;}
            set { this.Transaction = value;
            //this.Adapter.SelectCommand.Transaction = value;
            }
        }
    }

    public partial class CZ_UKOLTableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction; }
            set
            {
                this.Transaction = value;
                //this.Adapter.SelectCommand.Transaction = value;
            }
        }
    }

    public partial class CZMSTPWDTableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction; }
            set
            {
                this.Transaction = value;
                //this.Adapter.SelectCommand.Transaction = value;
            }
        }
    }
}

