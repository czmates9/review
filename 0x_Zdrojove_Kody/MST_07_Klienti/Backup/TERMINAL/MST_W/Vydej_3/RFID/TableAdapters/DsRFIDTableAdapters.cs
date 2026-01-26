namespace Fask.MST_W.Vydej_3.RFID.DsRFIDTableAdapters
{


    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    public partial class PredlohaTableAdapter : global::System.ComponentModel.Component
    {

        private global::System.Data.SQLite.SQLiteDataAdapter _adapter;

        private global::System.Data.SQLite.SQLiteConnection _connection;

        private global::System.Data.SQLite.SQLiteCommand[] _commandCollection;

        private bool _clearBeforeFill;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public PredlohaTableAdapter()
        {
            this.ClearBeforeFill = true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Data.SQLite.SQLiteDataAdapter Adapter
        {
            get
            {
                if ((this._adapter == null))
                {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteConnection Connection
        {
            get
            {
                if ((this._connection == null))
                {
                    this.InitConnection();
                }
                return this._connection;
            }
            set
            {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null))
                {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null))
                {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null))
                {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1))
                {
                    if ((this.CommandCollection[i] != null))
                    {
                        ((global::System.Data.SQLite.SQLiteCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected global::System.Data.SQLite.SQLiteCommand[] CommandCollection
        {
            get
            {
                if ((this._commandCollection == null))
                {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool ClearBeforeFill
        {
            get
            {
                return this._clearBeforeFill;
            }
            set
            {
                this._clearBeforeFill = value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitAdapter()
        {
            this._adapter = new global::System.Data.SQLite.SQLiteDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Predloha";
            tableMapping.ColumnMappings.Add("CountEntries", "CountEntries");
            tableMapping.ColumnMappings.Add("SOPNUMBE", "SOPNUMBE");
            tableMapping.ColumnMappings.Add("ITEMNMBR", "ITEMNMBR");
            tableMapping.ColumnMappings.Add("ORD", "ORD");
            tableMapping.ColumnMappings.Add("mnozstvi", "mnozstvi");
            tableMapping.ColumnMappings.Add("ITEMDESC", "ITEMDESC");
            tableMapping.ColumnMappings.Add("mnozstviRFID", "mnozstviRFID");
            this._adapter.TableMappings.Add(tableMapping);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitConnection()
        {
            this._connection = new global::System.Data.SQLite.SQLiteConnection();
            this._connection.ConnectionString = "data source=D:\\_work_\\_Vyvoj_Subversion_04_cipisek\\MST_07\\TERMINAL\\MST_W\\SqlCEDBs\\tm" +
                "p.sqlite";
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitCommandCollection()
        {
            this._commandCollection = new global::System.Data.SQLite.SQLiteCommand[1];
            this._commandCollection[0] = new global::System.Data.SQLite.SQLiteCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "select countentries, sopnumbe, itemnmbr, ord, sum(qtyshppd) as mnozstvi, itemdesc" +
                ", 0 as mnozstviRFID\r\nfrom czmst_se \r\nwhere qtypack=0\r\ngroup by countentries, sop" +
                "numbe, itemnmbr, ord, itemdesc";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(DsRFID.PredlohaDataTable dataTable)
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true))
            {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual DsRFID.PredlohaDataTable GetData()
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            DsRFID.PredlohaDataTable dataTable = new DsRFID.PredlohaDataTable(true);
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
    }

    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    public partial class RfidTableAdapter : global::System.ComponentModel.Component
    {

        private global::System.Data.SQLite.SQLiteDataAdapter _adapter;

        private global::System.Data.SQLite.SQLiteConnection _connection;

        private global::System.Data.SQLite.SQLiteCommand[] _commandCollection;

        private bool _clearBeforeFill;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public RfidTableAdapter()
        {
            this.ClearBeforeFill = true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Data.SQLite.SQLiteDataAdapter Adapter
        {
            get
            {
                if ((this._adapter == null))
                {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteConnection Connection
        {
            get
            {
                if ((this._connection == null))
                {
                    this.InitConnection();
                }
                return this._connection;
            }
            set
            {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null))
                {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null))
                {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null))
                {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1))
                {
                    if ((this.CommandCollection[i] != null))
                    {
                        ((global::System.Data.SQLite.SQLiteCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected global::System.Data.SQLite.SQLiteCommand[] CommandCollection
        {
            get
            {
                if ((this._commandCollection == null))
                {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool ClearBeforeFill
        {
            get
            {
                return this._clearBeforeFill;
            }
            set
            {
                this._clearBeforeFill = value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitAdapter()
        {
            this._adapter = new global::System.Data.SQLite.SQLiteDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Rfid";
            tableMapping.ColumnMappings.Add("CountEntries", "CountEntries");
            tableMapping.ColumnMappings.Add("SOPNUMBE", "SOPNUMBE");
            tableMapping.ColumnMappings.Add("ITEMNMBR", "ITEMNMBR");
            tableMapping.ColumnMappings.Add("ORD", "ORD");
            tableMapping.ColumnMappings.Add("mnozstvi", "mnozstvi");
            this._adapter.TableMappings.Add(tableMapping);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitConnection()
        {
            this._connection = new global::System.Data.SQLite.SQLiteConnection();
            this._connection.ConnectionString = "data source=D:\\_work_\\_Vyvoj_Subversion_04_cipisek\\MST_07\\TERMINAL\\MST_W\\SqlCEDBs\\tm" +
                "p.sqlite";
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitCommandCollection()
        {
            this._commandCollection = new global::System.Data.SQLite.SQLiteCommand[1];
            this._commandCollection[0] = new global::System.Data.SQLite.SQLiteCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "select countentries, documentnmbr as SOPNUMBE, itemnmbr, ord, count(*) as mnozstv" +
                "i\r\nfrom czmst_si_rfid\r\ngroup by countentries, documentnmbr, itemnmbr, ord";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(DsRFID.RfidDataTable dataTable)
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true))
            {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual DsRFID.RfidDataTable GetData()
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            DsRFID.RfidDataTable dataTable = new DsRFID.RfidDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
