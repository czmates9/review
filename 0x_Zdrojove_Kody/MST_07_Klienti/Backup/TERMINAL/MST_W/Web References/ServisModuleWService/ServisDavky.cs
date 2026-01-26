namespace Fask.MST_W.ServisModuleWService
{
    public partial class ServisDavky : global::System.Data.DataSet
    {
        public void ReadXmlSchemaDynamic(string filename)
        {
            this.Reset();
            global::System.Data.DataSet ds = new global::System.Data.DataSet();
            ds.ReadXmlSchema(filename);
            if ((ds.Tables["Hlavicky"] != null))
            {
                this.Tables.Add(new HlavickyDataTable(ds.Tables["Hlavicky"]));
            }
            this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
    }
}
