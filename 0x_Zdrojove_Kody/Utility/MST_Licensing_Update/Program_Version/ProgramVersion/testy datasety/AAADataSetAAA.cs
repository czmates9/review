using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ProgramVersion.testy_datasety
{
}

namespace ProgramVersion.testy_datasety
{
    class AAADataSetAAA
    {
        /// <summary>
        ///Represents a strongly typed in-memory cache of data.
        ///</summary>
        [global::System.Serializable()]
        [global::System.ComponentModel.DesignerCategoryAttribute("code")]
        [global::System.ComponentModel.ToolboxItem(true)]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
        [global::System.Xml.Serialization.XmlRootAttribute("AAAdataSetAAA")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
        public partial class AAAdataSetAAA : global::System.Data.DataSet
        {

            private CZMST_I1DataTable tableCZMST_I1;

            private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public AAAdataSetAAA()
            {
                this.BeginInit();
                this.InitClass();
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                base.Tables.CollectionChanged += schemaChangedHandler;
                base.Relations.CollectionChanged += schemaChangedHandler;
                this.EndInit();
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected AAAdataSetAAA(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
                base(info, context, false)
            {
                if ((this.IsBinarySerialized(info, context) == true))
                {
                    this.InitVars(false);
                    global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                    this.Tables.CollectionChanged += schemaChangedHandler1;
                    this.Relations.CollectionChanged += schemaChangedHandler1;
                    return;
                }
                string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
                if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema))
                {
                    global::System.Data.DataSet ds = new global::System.Data.DataSet();
                    ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                    if ((ds.Tables["CZMST_I1"] != null))
                    {
                        base.Tables.Add(new CZMST_I1DataTable(ds.Tables["CZMST_I1"]));
                    }
                    this.DataSetName = ds.DataSetName;
                    this.Prefix = ds.Prefix;
                    this.Namespace = ds.Namespace;
                    this.Locale = ds.Locale;
                    this.CaseSensitive = ds.CaseSensitive;
                    this.EnforceConstraints = ds.EnforceConstraints;
                    this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                    this.InitVars();
                }
                else
                {
                    this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                }
                this.GetSerializationData(info, context);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                base.Tables.CollectionChanged += schemaChangedHandler;
                this.Relations.CollectionChanged += schemaChangedHandler;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
            public CZMST_I1DataTable CZMST_I1
            {
                get
                {
                    return this.tableCZMST_I1;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.BrowsableAttribute(true)]
            [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
            public override global::System.Data.SchemaSerializationMode SchemaSerializationMode
            {
                get
                {
                    return this._schemaSerializationMode;
                }
                set
                {
                    this._schemaSerializationMode = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public new global::System.Data.DataTableCollection Tables
            {
                get
                {
                    return base.Tables;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public new global::System.Data.DataRelationCollection Relations
            {
                get
                {
                    return base.Relations;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void InitializeDerivedDataSet()
            {
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public override global::System.Data.DataSet Clone()
            {
                AAAdataSetAAA cln = ((AAAdataSetAAA)(base.Clone()));
                cln.InitVars();
                cln.SchemaSerializationMode = this.SchemaSerializationMode;
                return cln;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override bool ShouldSerializeTables()
            {
                return false;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override bool ShouldSerializeRelations()
            {
                return false;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader)
            {
                if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema))
                {
                    this.Reset();
                    global::System.Data.DataSet ds = new global::System.Data.DataSet();
                    ds.ReadXml(reader);
                    if ((ds.Tables["CZMST_I1"] != null))
                    {
                        base.Tables.Add(new CZMST_I1DataTable(ds.Tables["CZMST_I1"]));
                    }
                    this.DataSetName = ds.DataSetName;
                    this.Prefix = ds.Prefix;
                    this.Namespace = ds.Namespace;
                    this.Locale = ds.Locale;
                    this.CaseSensitive = ds.CaseSensitive;
                    this.EnforceConstraints = ds.EnforceConstraints;
                    this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                    this.InitVars();
                }
                else
                {
                    this.ReadXml(reader);
                    this.InitVars();
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable()
            {
                global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
                this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
                stream.Position = 0;
                return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal void InitVars()
            {
                this.InitVars(true);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal void InitVars(bool initTable)
            {
                this.tableCZMST_I1 = ((CZMST_I1DataTable)(base.Tables["CZMST_I1"]));
                if ((initTable == true))
                {
                    if ((this.tableCZMST_I1 != null))
                    {
                        this.tableCZMST_I1.InitVars();
                    }
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private void InitClass()
            {
                this.DataSetName = "AAAdataSetAAA";
                this.Prefix = "";
                this.Namespace = "http://tempuri.org/AAAdataSetAAA.xsd";
                this.EnforceConstraints = true;
                this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
                this.tableCZMST_I1 = new CZMST_I1DataTable();
                base.Tables.Add(this.tableCZMST_I1);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private bool ShouldSerializeCZMST_I1()
            {
                return false;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e)
            {
                if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove))
                {
                    this.InitVars();
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs)
            {
                AAAdataSetAAA ds = new AAAdataSetAAA();
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
                any.Namespace = ds.Namespace;
                sequence.Items.Add(any);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace))
                {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try
                    {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
                        {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length))
                            {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length)
                                            && (s1.ReadByte() == s2.ReadByte())); )
                                {
                                    ;
                                }
                                if ((s1.Position == s1.Length))
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if ((s1 != null))
                        {
                            s1.Close();
                        }
                        if ((s2 != null))
                        {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public delegate void CZMST_I1RowChangeEventHandler(object sender, CZMST_I1RowChangeEvent e);

            /// <summary>
            ///Represents the strongly named DataTable class.
            ///</summary>
            [global::System.Serializable()]
            [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
            public partial class CZMST_I1DataTable : global::System.Data.TypedTableBase<CZMST_I1Row>
            {

                private global::System.Data.DataColumn columnCountEntries;

                private global::System.Data.DataColumn columnITEMNMBR;

                private global::System.Data.DataColumn columnCZ_CarKod;

                private global::System.Data.DataColumn columnITEMDESC;

                private global::System.Data.DataColumn columnLOCNCODE;

                private global::System.Data.DataColumn columnSKL_ID;

                private global::System.Data.DataColumn columnQUANTITY;

                private global::System.Data.DataColumn columnDMJ;

                private global::System.Data.DataColumn columnDATEDONE;

                private global::System.Data.DataColumn columnIntegerValue;

                private global::System.Data.DataColumn columnTIMESPRT;

                private global::System.Data.DataColumn columnCZ_SerNum_Track;

                private global::System.Data.DataColumn columnCZ_SerNum_Find;

                private global::System.Data.DataColumn columnDEX_ROW_ID;

                private global::System.Data.DataColumn columnTerminalID;

                private global::System.Data.DataColumn columnO_TID;

                private global::System.Data.DataColumn columnREZ_1;

                private global::System.Data.DataColumn columnREZ_2;

                private global::System.Data.DataColumn columnITEMCODE;

                private global::System.Data.DataColumn columnCZ_REZ_1_Track;

                private global::System.Data.DataColumn columnCZ_REZ_2_Track;

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1DataTable()
                {
                    this.TableName = "CZMST_I1";
                    this.BeginInit();
                    this.InitClass();
                    this.EndInit();
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                internal CZMST_I1DataTable(global::System.Data.DataTable table)
                {
                    this.TableName = table.TableName;
                    if ((table.CaseSensitive != table.DataSet.CaseSensitive))
                    {
                        this.CaseSensitive = table.CaseSensitive;
                    }
                    if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
                    {
                        this.Locale = table.Locale;
                    }
                    if ((table.Namespace != table.DataSet.Namespace))
                    {
                        this.Namespace = table.Namespace;
                    }
                    this.Prefix = table.Prefix;
                    this.MinimumCapacity = table.MinimumCapacity;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected CZMST_I1DataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
                    base(info, context)
                {
                    this.InitVars();
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn CountEntriesColumn
                {
                    get
                    {
                        return this.columnCountEntries;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn ITEMNMBRColumn
                {
                    get
                    {
                        return this.columnITEMNMBR;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn CZ_CarKodColumn
                {
                    get
                    {
                        return this.columnCZ_CarKod;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn ITEMDESCColumn
                {
                    get
                    {
                        return this.columnITEMDESC;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn LOCNCODEColumn
                {
                    get
                    {
                        return this.columnLOCNCODE;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn SKL_IDColumn
                {
                    get
                    {
                        return this.columnSKL_ID;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn QUANTITYColumn
                {
                    get
                    {
                        return this.columnQUANTITY;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn DMJColumn
                {
                    get
                    {
                        return this.columnDMJ;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn DATEDONEColumn
                {
                    get
                    {
                        return this.columnDATEDONE;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn IntegerValueColumn
                {
                    get
                    {
                        return this.columnIntegerValue;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn TIMESPRTColumn
                {
                    get
                    {
                        return this.columnTIMESPRT;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn CZ_SerNum_TrackColumn
                {
                    get
                    {
                        return this.columnCZ_SerNum_Track;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn CZ_SerNum_FindColumn
                {
                    get
                    {
                        return this.columnCZ_SerNum_Find;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn DEX_ROW_IDColumn
                {
                    get
                    {
                        return this.columnDEX_ROW_ID;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn TerminalIDColumn
                {
                    get
                    {
                        return this.columnTerminalID;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn O_TIDColumn
                {
                    get
                    {
                        return this.columnO_TID;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn REZ_1Column
                {
                    get
                    {
                        return this.columnREZ_1;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn REZ_2Column
                {
                    get
                    {
                        return this.columnREZ_2;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn ITEMCODEColumn
                {
                    get
                    {
                        return this.columnITEMCODE;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn CZ_REZ_1_TrackColumn
                {
                    get
                    {
                        return this.columnCZ_REZ_1_Track;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataColumn CZ_REZ_2_TrackColumn
                {
                    get
                    {
                        return this.columnCZ_REZ_2_Track;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                [global::System.ComponentModel.Browsable(false)]
                public int Count
                {
                    get
                    {
                        return this.Rows.Count;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1Row this[int index]
                {
                    get
                    {
                        return ((CZMST_I1Row)(this.Rows[index]));
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public event CZMST_I1RowChangeEventHandler CZMST_I1RowChanging;

                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public event CZMST_I1RowChangeEventHandler CZMST_I1RowChanged;

                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public event CZMST_I1RowChangeEventHandler CZMST_I1RowDeleting;

                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public event CZMST_I1RowChangeEventHandler CZMST_I1RowDeleted;

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void AddCZMST_I1Row(CZMST_I1Row row)
                {
                    this.Rows.Add(row);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1Row AddCZMST_I1Row(
                            int CountEntries,
                            string ITEMNMBR,
                            string CZ_CarKod,
                            string ITEMDESC,
                            string LOCNCODE,
                            string SKL_ID,
                            decimal QUANTITY,
                            string DMJ,
                            System.DateTime DATEDONE,
                            short IntegerValue,
                            short TIMESPRT,
                            byte CZ_SerNum_Track,
                            byte CZ_SerNum_Find,
                            byte TerminalID,
                            byte O_TID,
                            string REZ_1,
                            string REZ_2,
                            string ITEMCODE,
                            byte CZ_REZ_1_Track,
                            byte CZ_REZ_2_Track)
                {
                    CZMST_I1Row rowCZMST_I1Row = ((CZMST_I1Row)(this.NewRow()));
                    object[] columnValuesArray = new object[] {
                        CountEntries,
                        ITEMNMBR,
                        CZ_CarKod,
                        ITEMDESC,
                        LOCNCODE,
                        SKL_ID,
                        QUANTITY,
                        DMJ,
                        DATEDONE,
                        IntegerValue,
                        TIMESPRT,
                        CZ_SerNum_Track,
                        CZ_SerNum_Find,
                        null,
                        TerminalID,
                        O_TID,
                        REZ_1,
                        REZ_2,
                        ITEMCODE,
                        CZ_REZ_1_Track,
                        CZ_REZ_2_Track};
                    rowCZMST_I1Row.ItemArray = columnValuesArray;
                    this.Rows.Add(rowCZMST_I1Row);
                    return rowCZMST_I1Row;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1Row FindByDEX_ROW_ID(int DEX_ROW_ID)
                {
                    return ((CZMST_I1Row)(this.Rows.Find(new object[] {
                            DEX_ROW_ID})));
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public override global::System.Data.DataTable Clone()
                {
                    CZMST_I1DataTable cln = ((CZMST_I1DataTable)(base.Clone()));
                    cln.InitVars();
                    return cln;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override global::System.Data.DataTable CreateInstance()
                {
                    return new CZMST_I1DataTable();
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                internal void InitVars()
                {
                    this.columnCountEntries = base.Columns["CountEntries"];
                    this.columnITEMNMBR = base.Columns["ITEMNMBR"];
                    this.columnCZ_CarKod = base.Columns["CZ_CarKod"];
                    this.columnITEMDESC = base.Columns["ITEMDESC"];
                    this.columnLOCNCODE = base.Columns["LOCNCODE"];
                    this.columnSKL_ID = base.Columns["SKL_ID"];
                    this.columnQUANTITY = base.Columns["QUANTITY"];
                    this.columnDMJ = base.Columns["DMJ"];
                    this.columnDATEDONE = base.Columns["DATEDONE"];
                    this.columnIntegerValue = base.Columns["IntegerValue"];
                    this.columnTIMESPRT = base.Columns["TIMESPRT"];
                    this.columnCZ_SerNum_Track = base.Columns["CZ_SerNum_Track"];
                    this.columnCZ_SerNum_Find = base.Columns["CZ_SerNum_Find"];
                    this.columnDEX_ROW_ID = base.Columns["DEX_ROW_ID"];
                    this.columnTerminalID = base.Columns["TerminalID"];
                    this.columnO_TID = base.Columns["O_TID"];
                    this.columnREZ_1 = base.Columns["REZ_1"];
                    this.columnREZ_2 = base.Columns["REZ_2"];
                    this.columnITEMCODE = base.Columns["ITEMCODE"];
                    this.columnCZ_REZ_1_Track = base.Columns["CZ_REZ_1_Track"];
                    this.columnCZ_REZ_2_Track = base.Columns["CZ_REZ_2_Track"];
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                private void InitClass()
                {
                    this.columnCountEntries = new global::System.Data.DataColumn("CountEntries", typeof(int), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnCountEntries);
                    this.columnITEMNMBR = new global::System.Data.DataColumn("ITEMNMBR", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnITEMNMBR);
                    this.columnCZ_CarKod = new global::System.Data.DataColumn("CZ_CarKod", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnCZ_CarKod);
                    this.columnITEMDESC = new global::System.Data.DataColumn("ITEMDESC", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnITEMDESC);
                    this.columnLOCNCODE = new global::System.Data.DataColumn("LOCNCODE", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnLOCNCODE);
                    this.columnSKL_ID = new global::System.Data.DataColumn("SKL_ID", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnSKL_ID);
                    this.columnQUANTITY = new global::System.Data.DataColumn("QUANTITY", typeof(decimal), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnQUANTITY);
                    this.columnDMJ = new global::System.Data.DataColumn("DMJ", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnDMJ);
                    this.columnDATEDONE = new global::System.Data.DataColumn("DATEDONE", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnDATEDONE);
                    this.columnIntegerValue = new global::System.Data.DataColumn("IntegerValue", typeof(short), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnIntegerValue);
                    this.columnTIMESPRT = new global::System.Data.DataColumn("TIMESPRT", typeof(short), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnTIMESPRT);
                    this.columnCZ_SerNum_Track = new global::System.Data.DataColumn("CZ_SerNum_Track", typeof(byte), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnCZ_SerNum_Track);
                    this.columnCZ_SerNum_Find = new global::System.Data.DataColumn("CZ_SerNum_Find", typeof(byte), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnCZ_SerNum_Find);
                    this.columnDEX_ROW_ID = new global::System.Data.DataColumn("DEX_ROW_ID", typeof(int), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnDEX_ROW_ID);
                    this.columnTerminalID = new global::System.Data.DataColumn("TerminalID", typeof(byte), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnTerminalID);
                    this.columnO_TID = new global::System.Data.DataColumn("O_TID", typeof(byte), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnO_TID);
                    this.columnREZ_1 = new global::System.Data.DataColumn("REZ_1", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnREZ_1);
                    this.columnREZ_2 = new global::System.Data.DataColumn("REZ_2", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnREZ_2);
                    this.columnITEMCODE = new global::System.Data.DataColumn("ITEMCODE", typeof(string), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnITEMCODE);
                    this.columnCZ_REZ_1_Track = new global::System.Data.DataColumn("CZ_REZ_1_Track", typeof(byte), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnCZ_REZ_1_Track);
                    this.columnCZ_REZ_2_Track = new global::System.Data.DataColumn("CZ_REZ_2_Track", typeof(byte), null, global::System.Data.MappingType.Element);
                    base.Columns.Add(this.columnCZ_REZ_2_Track);
                    this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnDEX_ROW_ID}, true));
                    this.columnCountEntries.AllowDBNull = false;
                    this.columnITEMNMBR.AllowDBNull = false;
                    this.columnITEMNMBR.MaxLength = 31;
                    this.columnCZ_CarKod.AllowDBNull = false;
                    this.columnCZ_CarKod.MaxLength = 31;
                    this.columnITEMDESC.AllowDBNull = false;
                    this.columnITEMDESC.MaxLength = 51;
                    this.columnLOCNCODE.AllowDBNull = false;
                    this.columnLOCNCODE.MaxLength = 11;
                    this.columnSKL_ID.AllowDBNull = false;
                    this.columnSKL_ID.MaxLength = 20;
                    this.columnQUANTITY.AllowDBNull = false;
                    this.columnDMJ.MaxLength = 3;
                    this.columnDATEDONE.AllowDBNull = false;
                    this.columnIntegerValue.AllowDBNull = false;
                    this.columnTIMESPRT.AllowDBNull = false;
                    this.columnCZ_SerNum_Track.AllowDBNull = false;
                    this.columnCZ_SerNum_Find.AllowDBNull = false;
                    this.columnDEX_ROW_ID.AutoIncrement = true;
                    this.columnDEX_ROW_ID.AutoIncrementSeed = -1;
                    this.columnDEX_ROW_ID.AutoIncrementStep = -1;
                    this.columnDEX_ROW_ID.AllowDBNull = false;
                    this.columnDEX_ROW_ID.ReadOnly = true;
                    this.columnDEX_ROW_ID.Unique = true;
                    this.columnTerminalID.AllowDBNull = false;
                    this.columnREZ_1.MaxLength = 15;
                    this.columnREZ_2.MaxLength = 15;
                    this.columnITEMCODE.MaxLength = 50;
                    this.columnCZ_REZ_1_Track.AllowDBNull = false;
                    this.columnCZ_REZ_2_Track.AllowDBNull = false;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1Row NewCZMST_I1Row()
                {
                    return ((CZMST_I1Row)(this.NewRow()));
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder)
                {
                    return new CZMST_I1Row(builder);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override global::System.Type GetRowType()
                {
                    return typeof(CZMST_I1Row);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e)
                {
                    base.OnRowChanged(e);
                    if ((this.CZMST_I1RowChanged != null))
                    {
                        this.CZMST_I1RowChanged(this, new CZMST_I1RowChangeEvent(((CZMST_I1Row)(e.Row)), e.Action));
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e)
                {
                    base.OnRowChanging(e);
                    if ((this.CZMST_I1RowChanging != null))
                    {
                        this.CZMST_I1RowChanging(this, new CZMST_I1RowChangeEvent(((CZMST_I1Row)(e.Row)), e.Action));
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e)
                {
                    base.OnRowDeleted(e);
                    if ((this.CZMST_I1RowDeleted != null))
                    {
                        this.CZMST_I1RowDeleted(this, new CZMST_I1RowChangeEvent(((CZMST_I1Row)(e.Row)), e.Action));
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e)
                {
                    base.OnRowDeleting(e);
                    if ((this.CZMST_I1RowDeleting != null))
                    {
                        this.CZMST_I1RowDeleting(this, new CZMST_I1RowChangeEvent(((CZMST_I1Row)(e.Row)), e.Action));
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void RemoveCZMST_I1Row(CZMST_I1Row row)
                {
                    this.Rows.Remove(row);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
                {
                    global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                    global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                    AAAdataSetAAA ds = new AAAdataSetAAA();
                    global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                    any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                    any1.MinOccurs = new decimal(0);
                    any1.MaxOccurs = decimal.MaxValue;
                    any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                    sequence.Items.Add(any1);
                    global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                    any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                    any2.MinOccurs = new decimal(1);
                    any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                    sequence.Items.Add(any2);
                    global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                    attribute1.Name = "namespace";
                    attribute1.FixedValue = ds.Namespace;
                    type.Attributes.Add(attribute1);
                    global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                    attribute2.Name = "tableTypeName";
                    attribute2.FixedValue = "CZMST_I1DataTable";
                    type.Attributes.Add(attribute2);
                    type.Particle = sequence;
                    global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                    if (xs.Contains(dsSchema.TargetNamespace))
                    {
                        global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                        global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                        try
                        {
                            global::System.Xml.Schema.XmlSchema schema = null;
                            dsSchema.Write(s1);
                            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
                            {
                                schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                                s2.SetLength(0);
                                schema.Write(s2);
                                if ((s1.Length == s2.Length))
                                {
                                    s1.Position = 0;
                                    s2.Position = 0;
                                    for (; ((s1.Position != s1.Length)
                                                && (s1.ReadByte() == s2.ReadByte())); )
                                    {
                                        ;
                                    }
                                    if ((s1.Position == s1.Length))
                                    {
                                        return type;
                                    }
                                }
                            }
                        }
                        finally
                        {
                            if ((s1 != null))
                            {
                                s1.Close();
                            }
                            if ((s2 != null))
                            {
                                s2.Close();
                            }
                        }
                    }
                    xs.Add(dsSchema);
                    return type;
                }
            }

            /// <summary>
            ///Represents strongly named DataRow class.
            ///</summary>
            public partial class CZMST_I1Row : global::System.Data.DataRow
            {

                private CZMST_I1DataTable tableCZMST_I1;

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                internal CZMST_I1Row(global::System.Data.DataRowBuilder rb) :
                    base(rb)
                {
                    this.tableCZMST_I1 = ((CZMST_I1DataTable)(this.Table));
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public int CountEntries
                {
                    get
                    {
                        return ((int)(this[this.tableCZMST_I1.CountEntriesColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.CountEntriesColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string ITEMNMBR
                {
                    get
                    {
                        return ((string)(this[this.tableCZMST_I1.ITEMNMBRColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.ITEMNMBRColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string CZ_CarKod
                {
                    get
                    {
                        return ((string)(this[this.tableCZMST_I1.CZ_CarKodColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.CZ_CarKodColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string ITEMDESC
                {
                    get
                    {
                        return ((string)(this[this.tableCZMST_I1.ITEMDESCColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.ITEMDESCColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string LOCNCODE
                {
                    get
                    {
                        return ((string)(this[this.tableCZMST_I1.LOCNCODEColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.LOCNCODEColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string SKL_ID
                {
                    get
                    {
                        return ((string)(this[this.tableCZMST_I1.SKL_IDColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.SKL_IDColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public decimal QUANTITY
                {
                    get
                    {
                        return ((decimal)(this[this.tableCZMST_I1.QUANTITYColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.QUANTITYColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string DMJ
                {
                    get
                    {
                        try
                        {
                            return ((string)(this[this.tableCZMST_I1.DMJColumn]));
                        }
                        catch (global::System.InvalidCastException e)
                        {
                            throw new global::System.Data.StrongTypingException("The value for column \'DMJ\' in table \'CZMST_I1\' is DBNull.", e);
                        }
                    }
                    set
                    {
                        this[this.tableCZMST_I1.DMJColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public System.DateTime DATEDONE
                {
                    get
                    {
                        return ((global::System.DateTime)(this[this.tableCZMST_I1.DATEDONEColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.DATEDONEColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public short IntegerValue
                {
                    get
                    {
                        return ((short)(this[this.tableCZMST_I1.IntegerValueColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.IntegerValueColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public short TIMESPRT
                {
                    get
                    {
                        return ((short)(this[this.tableCZMST_I1.TIMESPRTColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.TIMESPRTColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public byte CZ_SerNum_Track
                {
                    get
                    {
                        return ((byte)(this[this.tableCZMST_I1.CZ_SerNum_TrackColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.CZ_SerNum_TrackColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public byte CZ_SerNum_Find
                {
                    get
                    {
                        return ((byte)(this[this.tableCZMST_I1.CZ_SerNum_FindColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.CZ_SerNum_FindColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public int DEX_ROW_ID
                {
                    get
                    {
                        return ((int)(this[this.tableCZMST_I1.DEX_ROW_IDColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.DEX_ROW_IDColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public byte TerminalID
                {
                    get
                    {
                        return ((byte)(this[this.tableCZMST_I1.TerminalIDColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.TerminalIDColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public byte O_TID
                {
                    get
                    {
                        try
                        {
                            return ((byte)(this[this.tableCZMST_I1.O_TIDColumn]));
                        }
                        catch (global::System.InvalidCastException e)
                        {
                            throw new global::System.Data.StrongTypingException("The value for column \'O_TID\' in table \'CZMST_I1\' is DBNull.", e);
                        }
                    }
                    set
                    {
                        this[this.tableCZMST_I1.O_TIDColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string REZ_1
                {
                    get
                    {
                        try
                        {
                            return ((string)(this[this.tableCZMST_I1.REZ_1Column]));
                        }
                        catch (global::System.InvalidCastException e)
                        {
                            throw new global::System.Data.StrongTypingException("The value for column \'REZ_1\' in table \'CZMST_I1\' is DBNull.", e);
                        }
                    }
                    set
                    {
                        this[this.tableCZMST_I1.REZ_1Column] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string REZ_2
                {
                    get
                    {
                        try
                        {
                            return ((string)(this[this.tableCZMST_I1.REZ_2Column]));
                        }
                        catch (global::System.InvalidCastException e)
                        {
                            throw new global::System.Data.StrongTypingException("The value for column \'REZ_2\' in table \'CZMST_I1\' is DBNull.", e);
                        }
                    }
                    set
                    {
                        this[this.tableCZMST_I1.REZ_2Column] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public string ITEMCODE
                {
                    get
                    {
                        try
                        {
                            return ((string)(this[this.tableCZMST_I1.ITEMCODEColumn]));
                        }
                        catch (global::System.InvalidCastException e)
                        {
                            throw new global::System.Data.StrongTypingException("The value for column \'ITEMCODE\' in table \'CZMST_I1\' is DBNull.", e);
                        }
                    }
                    set
                    {
                        this[this.tableCZMST_I1.ITEMCODEColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public byte CZ_REZ_1_Track
                {
                    get
                    {
                        return ((byte)(this[this.tableCZMST_I1.CZ_REZ_1_TrackColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.CZ_REZ_1_TrackColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public byte CZ_REZ_2_Track
                {
                    get
                    {
                        return ((byte)(this[this.tableCZMST_I1.CZ_REZ_2_TrackColumn]));
                    }
                    set
                    {
                        this[this.tableCZMST_I1.CZ_REZ_2_TrackColumn] = value;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public bool IsDMJNull()
                {
                    return this.IsNull(this.tableCZMST_I1.DMJColumn);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void SetDMJNull()
                {
                    this[this.tableCZMST_I1.DMJColumn] = global::System.Convert.DBNull;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public bool IsO_TIDNull()
                {
                    return this.IsNull(this.tableCZMST_I1.O_TIDColumn);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void SetO_TIDNull()
                {
                    this[this.tableCZMST_I1.O_TIDColumn] = global::System.Convert.DBNull;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public bool IsREZ_1Null()
                {
                    return this.IsNull(this.tableCZMST_I1.REZ_1Column);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void SetREZ_1Null()
                {
                    this[this.tableCZMST_I1.REZ_1Column] = global::System.Convert.DBNull;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public bool IsREZ_2Null()
                {
                    return this.IsNull(this.tableCZMST_I1.REZ_2Column);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void SetREZ_2Null()
                {
                    this[this.tableCZMST_I1.REZ_2Column] = global::System.Convert.DBNull;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public bool IsITEMCODENull()
                {
                    return this.IsNull(this.tableCZMST_I1.ITEMCODEColumn);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public void SetITEMCODENull()
                {
                    this[this.tableCZMST_I1.ITEMCODEColumn] = global::System.Convert.DBNull;
                }
            }

            /// <summary>
            ///Row event argument class
            ///</summary>
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public class CZMST_I1RowChangeEvent : global::System.EventArgs
            {

                private CZMST_I1Row eventRow;

                private global::System.Data.DataRowAction eventAction;

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1RowChangeEvent(CZMST_I1Row row, global::System.Data.DataRowAction action)
                {
                    this.eventRow = row;
                    this.eventAction = action;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public CZMST_I1Row Row
                {
                    get
                    {
                        return this.eventRow;
                    }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
                public global::System.Data.DataRowAction Action
                {
                    get
                    {
                        return this.eventAction;
                    }
                }
            }
        }
    }
}




namespace ProgramVersion.testy_datasety.AAAdataSetAAATableAdapters
{


    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    //[global::System.ComponentModel.DesignerCategoryAttribute("code")]
    //[global::System.ComponentModel.ToolboxItem(true)]
    //[global::System.ComponentModel.DataObjectAttribute(true)]
    //[global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
    //    ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
   // [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class CZMST_I1TableAdapter : global::System.ComponentModel.Component
    {

        private global::System.Data.SqlClient.SqlDataAdapter _adapter;

        private global::System.Data.SqlClient.SqlConnection _connection;

        private global::System.Data.SqlClient.SqlTransaction _transaction;

        private global::System.Data.SqlClient.SqlCommand[] _commandCollection;

        private bool _clearBeforeFill;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public CZMST_I1TableAdapter()
        {
            this.ClearBeforeFill = true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected internal global::System.Data.SqlClient.SqlDataAdapter Adapter
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.SqlClient.SqlConnection Connection
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
                        ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.SqlClient.SqlTransaction Transaction
        {
            get
            {
                return this._transaction;
            }
            set
            {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1))
                {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null)
                            && (this.Adapter.DeleteCommand != null)))
                {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null)
                            && (this.Adapter.InsertCommand != null)))
                {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null)
                            && (this.Adapter.UpdateCommand != null)))
                {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected global::System.Data.SqlClient.SqlCommand[] CommandCollection
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitAdapter()
        {
            this._adapter = new global::System.Data.SqlClient.SqlDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "CZMST_I1";
            tableMapping.ColumnMappings.Add("CountEntries", "CountEntries");
            tableMapping.ColumnMappings.Add("ITEMNMBR", "ITEMNMBR");
            tableMapping.ColumnMappings.Add("CZ_CarKod", "CZ_CarKod");
            tableMapping.ColumnMappings.Add("ITEMDESC", "ITEMDESC");
            tableMapping.ColumnMappings.Add("LOCNCODE", "LOCNCODE");
            tableMapping.ColumnMappings.Add("SKL_ID", "SKL_ID");
            tableMapping.ColumnMappings.Add("QUANTITY", "QUANTITY");
            tableMapping.ColumnMappings.Add("DMJ", "DMJ");
            tableMapping.ColumnMappings.Add("DATEDONE", "DATEDONE");
            tableMapping.ColumnMappings.Add("IntegerValue", "IntegerValue");
            tableMapping.ColumnMappings.Add("TIMESPRT", "TIMESPRT");
            tableMapping.ColumnMappings.Add("CZ_SerNum_Track", "CZ_SerNum_Track");
            tableMapping.ColumnMappings.Add("CZ_SerNum_Find", "CZ_SerNum_Find");
            tableMapping.ColumnMappings.Add("DEX_ROW_ID", "DEX_ROW_ID");
            tableMapping.ColumnMappings.Add("TerminalID", "TerminalID");
            tableMapping.ColumnMappings.Add("O_TID", "O_TID");
            tableMapping.ColumnMappings.Add("REZ_1", "REZ_1");
            tableMapping.ColumnMappings.Add("REZ_2", "REZ_2");
            tableMapping.ColumnMappings.Add("ITEMCODE", "ITEMCODE");
            tableMapping.ColumnMappings.Add("CZ_REZ_1_Track", "CZ_REZ_1_Track");
            tableMapping.ColumnMappings.Add("CZ_REZ_2_Track", "CZ_REZ_2_Track");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = @"DELETE FROM [CZMST_I1] WHERE (([CountEntries] = @Original_CountEntries) AND ([ITEMNMBR] = @Original_ITEMNMBR) AND ([CZ_CarKod] = @Original_CZ_CarKod) AND ([ITEMDESC] = @Original_ITEMDESC) AND ([LOCNCODE] = @Original_LOCNCODE) AND ([SKL_ID] = @Original_SKL_ID) AND ([QUANTITY] = @Original_QUANTITY) AND ((@IsNull_DMJ = 1 AND [DMJ] IS NULL) OR ([DMJ] = @Original_DMJ)) AND ([DATEDONE] = @Original_DATEDONE) AND ([IntegerValue] = @Original_IntegerValue) AND ([TIMESPRT] = @Original_TIMESPRT) AND ([CZ_SerNum_Track] = @Original_CZ_SerNum_Track) AND ([CZ_SerNum_Find] = @Original_CZ_SerNum_Find) AND ([DEX_ROW_ID] = @Original_DEX_ROW_ID) AND ([TerminalID] = @Original_TerminalID) AND ((@IsNull_O_TID = 1 AND [O_TID] IS NULL) OR ([O_TID] = @Original_O_TID)) AND ((@IsNull_REZ_1 = 1 AND [REZ_1] IS NULL) OR ([REZ_1] = @Original_REZ_1)) AND ((@IsNull_REZ_2 = 1 AND [REZ_2] IS NULL) OR ([REZ_2] = @Original_REZ_2)) AND ((@IsNull_ITEMCODE = 1 AND [ITEMCODE] IS NULL) OR ([ITEMCODE] = @Original_ITEMCODE)) AND ([CZ_REZ_1_Track] = @Original_CZ_REZ_1_Track) AND ([CZ_REZ_2_Track] = @Original_CZ_REZ_2_Track))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CountEntries", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountEntries", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ITEMNMBR", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMNMBR", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_CarKod", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_CarKod", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ITEMDESC", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMDESC", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LOCNCODE", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LOCNCODE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_SKL_ID", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SKL_ID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_QUANTITY", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 19, 5, "QUANTITY", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_DMJ", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DMJ", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DMJ", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DMJ", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DATEDONE", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DATEDONE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_IntegerValue", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IntegerValue", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TIMESPRT", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TIMESPRT", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_SerNum_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Track", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_SerNum_Find", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Find", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DEX_ROW_ID", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DEX_ROW_ID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TerminalID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TerminalID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_O_TID", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "O_TID", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_O_TID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "O_TID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_REZ_1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_1", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_REZ_1", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_1", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_REZ_2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_2", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_REZ_2", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_2", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ITEMCODE", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMCODE", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ITEMCODE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMCODE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_REZ_1_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_1_Track", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_REZ_2_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_2_Track", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = @"INSERT INTO [CZMST_I1] ([CountEntries], [ITEMNMBR], [CZ_CarKod], [ITEMDESC], [LOCNCODE], [SKL_ID], [QUANTITY], [DMJ], [DATEDONE], [IntegerValue], [TIMESPRT], [CZ_SerNum_Track], [CZ_SerNum_Find], [TerminalID], [O_TID], [REZ_1], [REZ_2], [ITEMCODE], [CZ_REZ_1_Track], [CZ_REZ_2_Track]) VALUES (@CountEntries, @ITEMNMBR, @CZ_CarKod, @ITEMDESC, @LOCNCODE, @SKL_ID, @QUANTITY, @DMJ, @DATEDONE, @IntegerValue, @TIMESPRT, @CZ_SerNum_Track, @CZ_SerNum_Find, @TerminalID, @O_TID, @REZ_1, @REZ_2, @ITEMCODE, @CZ_REZ_1_Track, @CZ_REZ_2_Track);
SELECT CountEntries, ITEMNMBR, CZ_CarKod, ITEMDESC, LOCNCODE, SKL_ID, QUANTITY, DMJ, DATEDONE, IntegerValue, TIMESPRT, CZ_SerNum_Track, CZ_SerNum_Find, DEX_ROW_ID, TerminalID, O_TID, REZ_1, REZ_2, ITEMCODE, CZ_REZ_1_Track, CZ_REZ_2_Track FROM CZMST_I1 WHERE (DEX_ROW_ID = SCOPE_IDENTITY())";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountEntries", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountEntries", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ITEMNMBR", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMNMBR", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_CarKod", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_CarKod", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ITEMDESC", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMDESC", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LOCNCODE", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LOCNCODE", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SKL_ID", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SKL_ID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@QUANTITY", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 19, 5, "QUANTITY", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DMJ", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DMJ", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DATEDONE", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DATEDONE", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IntegerValue", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IntegerValue", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TIMESPRT", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TIMESPRT", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_SerNum_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Track", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_SerNum_Find", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Find", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TerminalID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TerminalID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@O_TID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "O_TID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@REZ_1", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@REZ_2", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ITEMCODE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMCODE", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_REZ_1_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_1_Track", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_REZ_2_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_2_Track", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = "UPDATE [CZMST_I1] SET [CountEntries] = @CountEntries, [ITEMNMBR] = @ITEMNMBR, [CZ" +
                "_CarKod] = @CZ_CarKod, [ITEMDESC] = @ITEMDESC, [LOCNCODE] = @LOCNCODE, [SKL_ID] " +
                "= @SKL_ID, [QUANTITY] = @QUANTITY, [DMJ] = @DMJ, [DATEDONE] = @DATEDONE, [Intege" +
                "rValue] = @IntegerValue, [TIMESPRT] = @TIMESPRT, [CZ_SerNum_Track] = @CZ_SerNum_" +
                "Track, [CZ_SerNum_Find] = @CZ_SerNum_Find, [TerminalID] = @TerminalID, [O_TID] =" +
                " @O_TID, [REZ_1] = @REZ_1, [REZ_2] = @REZ_2, [ITEMCODE] = @ITEMCODE, [CZ_REZ_1_T" +
                "rack] = @CZ_REZ_1_Track, [CZ_REZ_2_Track] = @CZ_REZ_2_Track WHERE (([CountEntrie" +
                "s] = @Original_CountEntries) AND ([ITEMNMBR] = @Original_ITEMNMBR) AND ([CZ_CarK" +
                "od] = @Original_CZ_CarKod) AND ([ITEMDESC] = @Original_ITEMDESC) AND ([LOCNCODE]" +
                " = @Original_LOCNCODE) AND ([SKL_ID] = @Original_SKL_ID) AND ([QUANTITY] = @Orig" +
                "inal_QUANTITY) AND ((@IsNull_DMJ = 1 AND [DMJ] IS NULL) OR ([DMJ] = @Original_DM" +
                "J)) AND ([DATEDONE] = @Original_DATEDONE) AND ([IntegerValue] = @Original_Intege" +
                "rValue) AND ([TIMESPRT] = @Original_TIMESPRT) AND ([CZ_SerNum_Track] = @Original" +
                "_CZ_SerNum_Track) AND ([CZ_SerNum_Find] = @Original_CZ_SerNum_Find) AND ([DEX_RO" +
                "W_ID] = @Original_DEX_ROW_ID) AND ([TerminalID] = @Original_TerminalID) AND ((@I" +
                "sNull_O_TID = 1 AND [O_TID] IS NULL) OR ([O_TID] = @Original_O_TID)) AND ((@IsNu" +
                "ll_REZ_1 = 1 AND [REZ_1] IS NULL) OR ([REZ_1] = @Original_REZ_1)) AND ((@IsNull_" +
                "REZ_2 = 1 AND [REZ_2] IS NULL) OR ([REZ_2] = @Original_REZ_2)) AND ((@IsNull_ITE" +
                "MCODE = 1 AND [ITEMCODE] IS NULL) OR ([ITEMCODE] = @Original_ITEMCODE)) AND ([CZ" +
                "_REZ_1_Track] = @Original_CZ_REZ_1_Track) AND ([CZ_REZ_2_Track] = @Original_CZ_R" +
                "EZ_2_Track));\r\nSELECT CountEntries, ITEMNMBR, CZ_CarKod, ITEMDESC, LOCNCODE, SKL" +
                "_ID, QUANTITY, DMJ, DATEDONE, IntegerValue, TIMESPRT, CZ_SerNum_Track, CZ_SerNum" +
                "_Find, DEX_ROW_ID, TerminalID, O_TID, REZ_1, REZ_2, ITEMCODE, CZ_REZ_1_Track, CZ" +
                "_REZ_2_Track FROM CZMST_I1 WHERE (DEX_ROW_ID = @DEX_ROW_ID)";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountEntries", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountEntries", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ITEMNMBR", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMNMBR", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_CarKod", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_CarKod", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ITEMDESC", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMDESC", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LOCNCODE", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LOCNCODE", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SKL_ID", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SKL_ID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@QUANTITY", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 19, 5, "QUANTITY", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DMJ", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DMJ", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DATEDONE", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DATEDONE", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IntegerValue", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IntegerValue", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TIMESPRT", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TIMESPRT", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_SerNum_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Track", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_SerNum_Find", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Find", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TerminalID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TerminalID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@O_TID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "O_TID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@REZ_1", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@REZ_2", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ITEMCODE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMCODE", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_REZ_1_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_1_Track", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CZ_REZ_2_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_2_Track", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CountEntries", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountEntries", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ITEMNMBR", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMNMBR", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_CarKod", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_CarKod", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ITEMDESC", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMDESC", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LOCNCODE", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LOCNCODE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_SKL_ID", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SKL_ID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_QUANTITY", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 19, 5, "QUANTITY", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_DMJ", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DMJ", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DMJ", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DMJ", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DATEDONE", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DATEDONE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_IntegerValue", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IntegerValue", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TIMESPRT", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TIMESPRT", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_SerNum_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Track", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_SerNum_Find", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_SerNum_Find", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DEX_ROW_ID", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DEX_ROW_ID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TerminalID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TerminalID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_O_TID", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "O_TID", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_O_TID", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "O_TID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_REZ_1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_1", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_REZ_1", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_1", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_REZ_2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_2", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_REZ_2", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "REZ_2", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ITEMCODE", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMCODE", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ITEMCODE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ITEMCODE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_REZ_1_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_1_Track", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CZ_REZ_2_Track", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CZ_REZ_2_Track", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DEX_ROW_ID", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "DEX_ROW_ID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitConnection()
        {
            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = global::ProgramVersion.Properties.Settings.Default.TaD_ConnectionString;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitCommandCollection()
        {
            this._commandCollection = new global::System.Data.SqlClient.SqlCommand[1];
            this._commandCollection[0] = new global::System.Data.SqlClient.SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = @"SELECT     CountEntries, ITEMNMBR, CZ_CarKod, ITEMDESC, LOCNCODE, SKL_ID, QUANTITY, DMJ, DATEDONE, IntegerValue, TIMESPRT, CZ_SerNum_Track, CZ_SerNum_Find,
                       DEX_ROW_ID, TerminalID, O_TID, REZ_1, REZ_2, ITEMCODE, CZ_REZ_1_Track, CZ_REZ_2_Track
FROM         CZMST_I1";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable dataTable)
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable GetData()
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable dataTable = new ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable dataTable)
        {
            return this.Adapter.Update(dataTable);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA dataSet)
        {
            return this.Adapter.Update(dataSet, "CZMST_I1");
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow)
        {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows)
        {
            return this.Adapter.Update(dataRows);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(
                    int Original_CountEntries,
                    string Original_ITEMNMBR,
                    string Original_CZ_CarKod,
                    string Original_ITEMDESC,
                    string Original_LOCNCODE,
                    string Original_SKL_ID,
                    decimal Original_QUANTITY,
                    string Original_DMJ,
                    System.DateTime Original_DATEDONE,
                    short Original_IntegerValue,
                    short Original_TIMESPRT,
                    byte Original_CZ_SerNum_Track,
                    byte Original_CZ_SerNum_Find,
                    int Original_DEX_ROW_ID,
                    byte Original_TerminalID,
                    global::System.Nullable<byte> Original_O_TID,
                    string Original_REZ_1,
                    string Original_REZ_2,
                    string Original_ITEMCODE,
                    byte Original_CZ_REZ_1_Track,
                    byte Original_CZ_REZ_2_Track)
        {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_CountEntries));
            if ((Original_ITEMNMBR == null))
            {
                throw new global::System.ArgumentNullException("Original_ITEMNMBR");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original_ITEMNMBR));
            }
            if ((Original_CZ_CarKod == null))
            {
                throw new global::System.ArgumentNullException("Original_CZ_CarKod");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original_CZ_CarKod));
            }
            if ((Original_ITEMDESC == null))
            {
                throw new global::System.ArgumentNullException("Original_ITEMDESC");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((string)(Original_ITEMDESC));
            }
            if ((Original_LOCNCODE == null))
            {
                throw new global::System.ArgumentNullException("Original_LOCNCODE");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[4].Value = ((string)(Original_LOCNCODE));
            }
            if ((Original_SKL_ID == null))
            {
                throw new global::System.ArgumentNullException("Original_SKL_ID");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[5].Value = ((string)(Original_SKL_ID));
            }
            this.Adapter.DeleteCommand.Parameters[6].Value = ((decimal)(Original_QUANTITY));
            if ((Original_DMJ == null))
            {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[8].Value = ((string)(Original_DMJ));
            }
            this.Adapter.DeleteCommand.Parameters[9].Value = ((System.DateTime)(Original_DATEDONE));
            this.Adapter.DeleteCommand.Parameters[10].Value = ((short)(Original_IntegerValue));
            this.Adapter.DeleteCommand.Parameters[11].Value = ((short)(Original_TIMESPRT));
            this.Adapter.DeleteCommand.Parameters[12].Value = ((byte)(Original_CZ_SerNum_Track));
            this.Adapter.DeleteCommand.Parameters[13].Value = ((byte)(Original_CZ_SerNum_Find));
            this.Adapter.DeleteCommand.Parameters[14].Value = ((int)(Original_DEX_ROW_ID));
            this.Adapter.DeleteCommand.Parameters[15].Value = ((byte)(Original_TerminalID));
            if ((Original_O_TID.HasValue == true))
            {
                this.Adapter.DeleteCommand.Parameters[16].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[17].Value = ((byte)(Original_O_TID.Value));
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[16].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[17].Value = global::System.DBNull.Value;
            }
            if ((Original_REZ_1 == null))
            {
                this.Adapter.DeleteCommand.Parameters[18].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[19].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[18].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[19].Value = ((string)(Original_REZ_1));
            }
            if ((Original_REZ_2 == null))
            {
                this.Adapter.DeleteCommand.Parameters[20].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[21].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[20].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[21].Value = ((string)(Original_REZ_2));
            }
            if ((Original_ITEMCODE == null))
            {
                this.Adapter.DeleteCommand.Parameters[22].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[23].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[22].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[23].Value = ((string)(Original_ITEMCODE));
            }
            this.Adapter.DeleteCommand.Parameters[24].Value = ((byte)(Original_CZ_REZ_1_Track));
            this.Adapter.DeleteCommand.Parameters[25].Value = ((byte)(Original_CZ_REZ_2_Track));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(
                    int CountEntries,
                    string ITEMNMBR,
                    string CZ_CarKod,
                    string ITEMDESC,
                    string LOCNCODE,
                    string SKL_ID,
                    decimal QUANTITY,
                    string DMJ,
                    System.DateTime DATEDONE,
                    short IntegerValue,
                    short TIMESPRT,
                    byte CZ_SerNum_Track,
                    byte CZ_SerNum_Find,
                    byte TerminalID,
                    global::System.Nullable<byte> O_TID,
                    string REZ_1,
                    string REZ_2,
                    string ITEMCODE,
                    byte CZ_REZ_1_Track,
                    byte CZ_REZ_2_Track)
        {
            this.Adapter.InsertCommand.Parameters[0].Value = ((int)(CountEntries));
            if ((ITEMNMBR == null))
            {
                throw new global::System.ArgumentNullException("ITEMNMBR");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(ITEMNMBR));
            }
            if ((CZ_CarKod == null))
            {
                throw new global::System.ArgumentNullException("CZ_CarKod");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(CZ_CarKod));
            }
            if ((ITEMDESC == null))
            {
                throw new global::System.ArgumentNullException("ITEMDESC");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((string)(ITEMDESC));
            }
            if ((LOCNCODE == null))
            {
                throw new global::System.ArgumentNullException("LOCNCODE");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(LOCNCODE));
            }
            if ((SKL_ID == null))
            {
                throw new global::System.ArgumentNullException("SKL_ID");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((string)(SKL_ID));
            }
            this.Adapter.InsertCommand.Parameters[6].Value = ((decimal)(QUANTITY));
            if ((DMJ == null))
            {
                this.Adapter.InsertCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[7].Value = ((string)(DMJ));
            }
            this.Adapter.InsertCommand.Parameters[8].Value = ((System.DateTime)(DATEDONE));
            this.Adapter.InsertCommand.Parameters[9].Value = ((short)(IntegerValue));
            this.Adapter.InsertCommand.Parameters[10].Value = ((short)(TIMESPRT));
            this.Adapter.InsertCommand.Parameters[11].Value = ((byte)(CZ_SerNum_Track));
            this.Adapter.InsertCommand.Parameters[12].Value = ((byte)(CZ_SerNum_Find));
            this.Adapter.InsertCommand.Parameters[13].Value = ((byte)(TerminalID));
            if ((O_TID.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[14].Value = ((byte)(O_TID.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[14].Value = global::System.DBNull.Value;
            }
            if ((REZ_1 == null))
            {
                this.Adapter.InsertCommand.Parameters[15].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[15].Value = ((string)(REZ_1));
            }
            if ((REZ_2 == null))
            {
                this.Adapter.InsertCommand.Parameters[16].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[16].Value = ((string)(REZ_2));
            }
            if ((ITEMCODE == null))
            {
                this.Adapter.InsertCommand.Parameters[17].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[17].Value = ((string)(ITEMCODE));
            }
            this.Adapter.InsertCommand.Parameters[18].Value = ((byte)(CZ_REZ_1_Track));
            this.Adapter.InsertCommand.Parameters[19].Value = ((byte)(CZ_REZ_2_Track));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(
                    int CountEntries,
                    string ITEMNMBR,
                    string CZ_CarKod,
                    string ITEMDESC,
                    string LOCNCODE,
                    string SKL_ID,
                    decimal QUANTITY,
                    string DMJ,
                    System.DateTime DATEDONE,
                    short IntegerValue,
                    short TIMESPRT,
                    byte CZ_SerNum_Track,
                    byte CZ_SerNum_Find,
                    byte TerminalID,
                    global::System.Nullable<byte> O_TID,
                    string REZ_1,
                    string REZ_2,
                    string ITEMCODE,
                    byte CZ_REZ_1_Track,
                    byte CZ_REZ_2_Track,
                    int Original_CountEntries,
                    string Original_ITEMNMBR,
                    string Original_CZ_CarKod,
                    string Original_ITEMDESC,
                    string Original_LOCNCODE,
                    string Original_SKL_ID,
                    decimal Original_QUANTITY,
                    string Original_DMJ,
                    System.DateTime Original_DATEDONE,
                    short Original_IntegerValue,
                    short Original_TIMESPRT,
                    byte Original_CZ_SerNum_Track,
                    byte Original_CZ_SerNum_Find,
                    int Original_DEX_ROW_ID,
                    byte Original_TerminalID,
                    global::System.Nullable<byte> Original_O_TID,
                    string Original_REZ_1,
                    string Original_REZ_2,
                    string Original_ITEMCODE,
                    byte Original_CZ_REZ_1_Track,
                    byte Original_CZ_REZ_2_Track,
                    int DEX_ROW_ID)
        {
            this.Adapter.UpdateCommand.Parameters[0].Value = ((int)(CountEntries));
            if ((ITEMNMBR == null))
            {
                throw new global::System.ArgumentNullException("ITEMNMBR");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(ITEMNMBR));
            }
            if ((CZ_CarKod == null))
            {
                throw new global::System.ArgumentNullException("CZ_CarKod");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(CZ_CarKod));
            }
            if ((ITEMDESC == null))
            {
                throw new global::System.ArgumentNullException("ITEMDESC");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(ITEMDESC));
            }
            if ((LOCNCODE == null))
            {
                throw new global::System.ArgumentNullException("LOCNCODE");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(LOCNCODE));
            }
            if ((SKL_ID == null))
            {
                throw new global::System.ArgumentNullException("SKL_ID");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(SKL_ID));
            }
            this.Adapter.UpdateCommand.Parameters[6].Value = ((decimal)(QUANTITY));
            if ((DMJ == null))
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((string)(DMJ));
            }
            this.Adapter.UpdateCommand.Parameters[8].Value = ((System.DateTime)(DATEDONE));
            this.Adapter.UpdateCommand.Parameters[9].Value = ((short)(IntegerValue));
            this.Adapter.UpdateCommand.Parameters[10].Value = ((short)(TIMESPRT));
            this.Adapter.UpdateCommand.Parameters[11].Value = ((byte)(CZ_SerNum_Track));
            this.Adapter.UpdateCommand.Parameters[12].Value = ((byte)(CZ_SerNum_Find));
            this.Adapter.UpdateCommand.Parameters[13].Value = ((byte)(TerminalID));
            if ((O_TID.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((byte)(O_TID.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[14].Value = global::System.DBNull.Value;
            }
            if ((REZ_1 == null))
            {
                this.Adapter.UpdateCommand.Parameters[15].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[15].Value = ((string)(REZ_1));
            }
            if ((REZ_2 == null))
            {
                this.Adapter.UpdateCommand.Parameters[16].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[16].Value = ((string)(REZ_2));
            }
            if ((ITEMCODE == null))
            {
                this.Adapter.UpdateCommand.Parameters[17].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[17].Value = ((string)(ITEMCODE));
            }
            this.Adapter.UpdateCommand.Parameters[18].Value = ((byte)(CZ_REZ_1_Track));
            this.Adapter.UpdateCommand.Parameters[19].Value = ((byte)(CZ_REZ_2_Track));
            this.Adapter.UpdateCommand.Parameters[20].Value = ((int)(Original_CountEntries));
            if ((Original_ITEMNMBR == null))
            {
                throw new global::System.ArgumentNullException("Original_ITEMNMBR");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[21].Value = ((string)(Original_ITEMNMBR));
            }
            if ((Original_CZ_CarKod == null))
            {
                throw new global::System.ArgumentNullException("Original_CZ_CarKod");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[22].Value = ((string)(Original_CZ_CarKod));
            }
            if ((Original_ITEMDESC == null))
            {
                throw new global::System.ArgumentNullException("Original_ITEMDESC");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[23].Value = ((string)(Original_ITEMDESC));
            }
            if ((Original_LOCNCODE == null))
            {
                throw new global::System.ArgumentNullException("Original_LOCNCODE");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[24].Value = ((string)(Original_LOCNCODE));
            }
            if ((Original_SKL_ID == null))
            {
                throw new global::System.ArgumentNullException("Original_SKL_ID");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[25].Value = ((string)(Original_SKL_ID));
            }
            this.Adapter.UpdateCommand.Parameters[26].Value = ((decimal)(Original_QUANTITY));
            if ((Original_DMJ == null))
            {
                this.Adapter.UpdateCommand.Parameters[27].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[28].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[27].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[28].Value = ((string)(Original_DMJ));
            }
            this.Adapter.UpdateCommand.Parameters[29].Value = ((System.DateTime)(Original_DATEDONE));
            this.Adapter.UpdateCommand.Parameters[30].Value = ((short)(Original_IntegerValue));
            this.Adapter.UpdateCommand.Parameters[31].Value = ((short)(Original_TIMESPRT));
            this.Adapter.UpdateCommand.Parameters[32].Value = ((byte)(Original_CZ_SerNum_Track));
            this.Adapter.UpdateCommand.Parameters[33].Value = ((byte)(Original_CZ_SerNum_Find));
            this.Adapter.UpdateCommand.Parameters[34].Value = ((int)(Original_DEX_ROW_ID));
            this.Adapter.UpdateCommand.Parameters[35].Value = ((byte)(Original_TerminalID));
            if ((Original_O_TID.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[36].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[37].Value = ((byte)(Original_O_TID.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[36].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[37].Value = global::System.DBNull.Value;
            }
            if ((Original_REZ_1 == null))
            {
                this.Adapter.UpdateCommand.Parameters[38].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[39].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[38].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[39].Value = ((string)(Original_REZ_1));
            }
            if ((Original_REZ_2 == null))
            {
                this.Adapter.UpdateCommand.Parameters[40].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[41].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[40].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[41].Value = ((string)(Original_REZ_2));
            }
            if ((Original_ITEMCODE == null))
            {
                this.Adapter.UpdateCommand.Parameters[42].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[43].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[42].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[43].Value = ((string)(Original_ITEMCODE));
            }
            this.Adapter.UpdateCommand.Parameters[44].Value = ((byte)(Original_CZ_REZ_1_Track));
            this.Adapter.UpdateCommand.Parameters[45].Value = ((byte)(Original_CZ_REZ_2_Track));
            this.Adapter.UpdateCommand.Parameters[46].Value = ((int)(DEX_ROW_ID));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(
                    int CountEntries,
                    string ITEMNMBR,
                    string CZ_CarKod,
                    string ITEMDESC,
                    string LOCNCODE,
                    string SKL_ID,
                    decimal QUANTITY,
                    string DMJ,
                    System.DateTime DATEDONE,
                    short IntegerValue,
                    short TIMESPRT,
                    byte CZ_SerNum_Track,
                    byte CZ_SerNum_Find,
                    byte TerminalID,
                    global::System.Nullable<byte> O_TID,
                    string REZ_1,
                    string REZ_2,
                    string ITEMCODE,
                    byte CZ_REZ_1_Track,
                    byte CZ_REZ_2_Track,
                    int Original_CountEntries,
                    string Original_ITEMNMBR,
                    string Original_CZ_CarKod,
                    string Original_ITEMDESC,
                    string Original_LOCNCODE,
                    string Original_SKL_ID,
                    decimal Original_QUANTITY,
                    string Original_DMJ,
                    System.DateTime Original_DATEDONE,
                    short Original_IntegerValue,
                    short Original_TIMESPRT,
                    byte Original_CZ_SerNum_Track,
                    byte Original_CZ_SerNum_Find,
                    int Original_DEX_ROW_ID,
                    byte Original_TerminalID,
                    global::System.Nullable<byte> Original_O_TID,
                    string Original_REZ_1,
                    string Original_REZ_2,
                    string Original_ITEMCODE,
                    byte Original_CZ_REZ_1_Track,
                    byte Original_CZ_REZ_2_Track)
        {
            return this.Update(CountEntries, ITEMNMBR, CZ_CarKod, ITEMDESC, LOCNCODE, SKL_ID, QUANTITY, DMJ, DATEDONE, IntegerValue, TIMESPRT, CZ_SerNum_Track, CZ_SerNum_Find, TerminalID, O_TID, REZ_1, REZ_2, ITEMCODE, CZ_REZ_1_Track, CZ_REZ_2_Track, Original_CountEntries, Original_ITEMNMBR, Original_CZ_CarKod, Original_ITEMDESC, Original_LOCNCODE, Original_SKL_ID, Original_QUANTITY, Original_DMJ, Original_DATEDONE, Original_IntegerValue, Original_TIMESPRT, Original_CZ_SerNum_Track, Original_CZ_SerNum_Find, Original_DEX_ROW_ID, Original_TerminalID, Original_O_TID, Original_REZ_1, Original_REZ_2, Original_ITEMCODE, Original_CZ_REZ_1_Track, Original_CZ_REZ_2_Track, Original_DEX_ROW_ID);
        }
    }

    /// <summary>
    ///TableAdapterManager is used to coordinate TableAdapters in the dataset to enable Hierarchical Update scenarios
    ///</summary>
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSD" +
        "esigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapterManager")]
    public partial class TableAdapterManager : global::System.ComponentModel.Component
    {

        private UpdateOrderOption _updateOrder;

        private CZMST_I1TableAdapter _cZMST_I1TableAdapter;

        private bool _backupDataSetBeforeUpdate;

        private global::System.Data.IDbConnection _connection;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public UpdateOrderOption UpdateOrder
        {
            get
            {
                return this._updateOrder;
            }
            set
            {
                this._updateOrder = value;
            }
        }

        //[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        //[global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
        //    "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
        //    "a", "System.Drawing.Design.UITypeEditor")]
        public CZMST_I1TableAdapter CZMST_I1TableAdapter
        {
            get
            {
                return this._cZMST_I1TableAdapter;
            }
            set
            {
                this._cZMST_I1TableAdapter = value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool BackupDataSetBeforeUpdate
        {
            get
            {
                return this._backupDataSetBeforeUpdate;
            }
            set
            {
                this._backupDataSetBeforeUpdate = value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        public global::System.Data.IDbConnection Connection
        {
            get
            {
                if ((this._connection != null))
                {
                    return this._connection;
                }
                if (((this._cZMST_I1TableAdapter != null)
                            && (this._cZMST_I1TableAdapter.Connection != null)))
                {
                    return this._cZMST_I1TableAdapter.Connection;
                }
                return null;
            }
            set
            {
                this._connection = value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        public int TableAdapterInstanceCount
        {
            get
            {
                int count = 0;
                if ((this._cZMST_I1TableAdapter != null))
                {
                    count = (count + 1);
                }
                return count;
            }
        }

        /// <summary>
        ///Update rows in top-down order.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private int UpdateUpdatedRows(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows)
        {
            int result = 0;
            if ((this._cZMST_I1TableAdapter != null))
            {
                global::System.Data.DataRow[] updatedRows = dataSet.CZMST_I1.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null)
                            && (0 < updatedRows.Length)))
                {
                    result = (result + this._cZMST_I1TableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            return result;
        }

        /// <summary>
        ///Insert rows in top-down order.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private int UpdateInsertedRows(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows)
        {
            int result = 0;
            if ((this._cZMST_I1TableAdapter != null))
            {
                global::System.Data.DataRow[] addedRows = dataSet.CZMST_I1.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null)
                            && (0 < addedRows.Length)))
                {
                    result = (result + this._cZMST_I1TableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            return result;
        }

        /// <summary>
        ///Delete rows in bottom-up order.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private int UpdateDeletedRows(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows)
        {
            int result = 0;
            if ((this._cZMST_I1TableAdapter != null))
            {
                global::System.Data.DataRow[] deletedRows = dataSet.CZMST_I1.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null)
                            && (0 < deletedRows.Length)))
                {
                    result = (result + this._cZMST_I1TableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            return result;
        }

        /// <summary>
        ///Remove inserted rows that become updated rows after calling TableAdapter.Update(inserted rows) first
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private global::System.Data.DataRow[] GetRealUpdatedRows(global::System.Data.DataRow[] updatedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows)
        {
            if (((updatedRows == null)
                        || (updatedRows.Length < 1)))
            {
                return updatedRows;
            }
            if (((allAddedRows == null)
                        || (allAddedRows.Count < 1)))
            {
                return updatedRows;
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> realUpdatedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            for (int i = 0; (i < updatedRows.Length); i = (i + 1))
            {
                global::System.Data.DataRow row = updatedRows[i];
                if ((allAddedRows.Contains(row) == false))
                {
                    realUpdatedRows.Add(row);
                }
            }
            return realUpdatedRows.ToArray();
        }

        /// <summary>
        ///Update all changes to the dataset.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public virtual int UpdateAll(ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA dataSet)
        {
            if ((dataSet == null))
            {
                throw new global::System.ArgumentNullException("dataSet");
            }
            if ((dataSet.HasChanges() == false))
            {
                return 0;
            }
            if (((this._cZMST_I1TableAdapter != null)
                        && (this.MatchTableAdapterConnection(this._cZMST_I1TableAdapter.Connection) == false)))
            {
                throw new global::System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" +
                        "tring.");
            }
            global::System.Data.IDbConnection workConnection = this.Connection;
            if ((workConnection == null))
            {
                throw new global::System.ApplicationException("TableAdapterManager contains no connection information. Set each TableAdapterMana" +
                        "ger TableAdapter property to a valid TableAdapter instance.");
            }
            bool workConnOpened = false;
            if (((workConnection.State & global::System.Data.ConnectionState.Broken)
                        == global::System.Data.ConnectionState.Broken))
            {
                workConnection.Close();
            }
            if ((workConnection.State == global::System.Data.ConnectionState.Closed))
            {
                workConnection.Open();
                workConnOpened = true;
            }
            global::System.Data.IDbTransaction workTransaction = workConnection.BeginTransaction();
            if ((workTransaction == null))
            {
                throw new global::System.ApplicationException("The transaction cannot begin. The current data connection does not support transa" +
                        "ctions or the current state is not allowing the transaction to begin.");
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter> adaptersWithAcceptChangesDuringUpdate = new global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter>();
            global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection> revertConnections = new global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection>();
            int result = 0;
            global::System.Data.DataSet backupDataSet = null;
            if (this.BackupDataSetBeforeUpdate)
            {
                backupDataSet = new global::System.Data.DataSet();
                backupDataSet.Merge(dataSet);
            }
            try
            {
                // ---- Prepare for update -----------
                //
                if ((this._cZMST_I1TableAdapter != null))
                {
                    revertConnections.Add(this._cZMST_I1TableAdapter, this._cZMST_I1TableAdapter.Connection);
                    this._cZMST_I1TableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(workConnection));
                    this._cZMST_I1TableAdapter.Transaction = ((global::System.Data.SqlClient.SqlTransaction)(workTransaction));
                    if (this._cZMST_I1TableAdapter.Adapter.AcceptChangesDuringUpdate)
                    {
                        this._cZMST_I1TableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._cZMST_I1TableAdapter.Adapter);
                    }
                }
                // 
                //---- Perform updates -----------
                //
                if ((this.UpdateOrder == UpdateOrderOption.UpdateInsertDelete))
                {
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                }
                else
                {
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                }
                result = (result + this.UpdateDeletedRows(dataSet, allChangedRows));
                // 
                //---- Commit updates -----------
                //
                workTransaction.Commit();
                if ((0 < allAddedRows.Count))
                {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                    allAddedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1))
                    {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
                if ((0 < allChangedRows.Count))
                {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allChangedRows.Count];
                    allChangedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1))
                    {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
            }
            catch (global::System.Exception ex)
            {
                workTransaction.Rollback();
                // ---- Restore the dataset -----------
                if (this.BackupDataSetBeforeUpdate)
                {
                    global::System.Diagnostics.Debug.Assert((backupDataSet != null));
                    dataSet.Clear();
                    dataSet.Merge(backupDataSet);
                }
                else
                {
                    if ((0 < allAddedRows.Count))
                    {
                        global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                        allAddedRows.CopyTo(rows);
                        for (int i = 0; (i < rows.Length); i = (i + 1))
                        {
                            global::System.Data.DataRow row = rows[i];
                            row.AcceptChanges();
                            row.SetAdded();
                        }
                    }
                }
                throw ex;
            }
            finally
            {
                if (workConnOpened)
                {
                    workConnection.Close();
                }
                if ((this._cZMST_I1TableAdapter != null))
                {
                    this._cZMST_I1TableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(revertConnections[this._cZMST_I1TableAdapter]));
                    this._cZMST_I1TableAdapter.Transaction = null;
                }
                if ((0 < adaptersWithAcceptChangesDuringUpdate.Count))
                {
                    global::System.Data.Common.DataAdapter[] adapters = new System.Data.Common.DataAdapter[adaptersWithAcceptChangesDuringUpdate.Count];
                    adaptersWithAcceptChangesDuringUpdate.CopyTo(adapters);
                    for (int i = 0; (i < adapters.Length); i = (i + 1))
                    {
                        global::System.Data.Common.DataAdapter adapter = adapters[i];
                        adapter.AcceptChangesDuringUpdate = true;
                    }
                }
            }
            return result;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected virtual void SortSelfReferenceRows(global::System.Data.DataRow[] rows, global::System.Data.DataRelation relation, bool childFirst)
        {
            global::System.Array.Sort<global::System.Data.DataRow>(rows, new SelfReferenceComparer(relation, childFirst));
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected virtual bool MatchTableAdapterConnection(global::System.Data.IDbConnection inputConnection)
        {
            if ((this._connection != null))
            {
                return true;
            }
            if (((this.Connection == null)
                        || (inputConnection == null)))
            {
                return true;
            }
            if (string.Equals(this.Connection.ConnectionString, inputConnection.ConnectionString, global::System.StringComparison.Ordinal))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///Update Order Option
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public enum UpdateOrderOption
        {

            InsertUpdateDelete = 0,

            UpdateInsertDelete = 1,
        }

        /// <summary>
        ///Used to sort self-referenced table's rows
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private class SelfReferenceComparer : object, global::System.Collections.Generic.IComparer<global::System.Data.DataRow>
        {

            private global::System.Data.DataRelation _relation;

            private int _childFirst;

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal SelfReferenceComparer(global::System.Data.DataRelation relation, bool childFirst)
            {
                this._relation = relation;
                if (childFirst)
                {
                    this._childFirst = -1;
                }
                else
                {
                    this._childFirst = 1;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private global::System.Data.DataRow GetRoot(global::System.Data.DataRow row, out int distance)
            {
                global::System.Diagnostics.Debug.Assert((row != null));
                global::System.Data.DataRow root = row;
                distance = 0;

                global::System.Collections.Generic.IDictionary<global::System.Data.DataRow, global::System.Data.DataRow> traversedRows = new global::System.Collections.Generic.Dictionary<global::System.Data.DataRow, global::System.Data.DataRow>();
                traversedRows[row] = row;

                global::System.Data.DataRow parent = row.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                for (
                ; ((parent != null)
                            && (traversedRows.ContainsKey(parent) == false));
                )
                {
                    distance = (distance + 1);
                    root = parent;
                    traversedRows[parent] = parent;
                    parent = parent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                }

                if ((distance == 0))
                {
                    traversedRows.Clear();
                    traversedRows[row] = row;
                    parent = row.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    for (
                    ; ((parent != null)
                                && (traversedRows.ContainsKey(parent) == false));
                    )
                    {
                        distance = (distance + 1);
                        root = parent;
                        traversedRows[parent] = parent;
                        parent = parent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    }
                }

                return root;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int Compare(global::System.Data.DataRow row1, global::System.Data.DataRow row2)
            {
                if (object.ReferenceEquals(row1, row2))
                {
                    return 0;
                }
                if ((row1 == null))
                {
                    return -1;
                }
                if ((row2 == null))
                {
                    return 1;
                }

                int distance1 = 0;
                global::System.Data.DataRow root1 = this.GetRoot(row1, out distance1);

                int distance2 = 0;
                global::System.Data.DataRow root2 = this.GetRoot(row2, out distance2);

                if (object.ReferenceEquals(root1, root2))
                {
                    return (this._childFirst * distance1.CompareTo(distance2));
                }
                else
                {
                    global::System.Diagnostics.Debug.Assert(((root1.Table != null)
                                    && (root2.Table != null)));
                    if ((root1.Table.Rows.IndexOf(root1) < root2.Table.Rows.IndexOf(root2)))
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }
}

