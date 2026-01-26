using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zuby
{
    public class DataGridViewTextBoxValueTypeColumn : System.Windows.Forms.DataGridViewTextBoxColumn
    {


        public DataGridViewTextBoxValueTypeColumn()
            : base()
        {
        }



        //private string _valueTypeDefinition = string.Empty;
        //public string ValueTypeDefinition
        //{
        //    get { return _valueTypeDefinition; }
        //    set 
        //    {
        //        _valueTypeDefinition = value;
        //        Type t = Type.GetType(_valueTypeDefinition);
        //        this.ValueType = t;
        //    }
        //}


        //[System.ComponentModel.TypeConverter(typeof(MyConverter))]
        //[System.ComponentModel.DefaultValue("System.String")]
        //private string _valueTypeDefinition = string.Empty;
        //public string ValueTypeDefinition
        //{
        //    get { return _valueTypeDefinition; }
        //    set { _valueTypeDefinition = value; }
        //}

        //private Type typeVal;

        //[System.Configuration.ConfigurationProperty("type")]
        //[System.ComponentModel.TypeConverter(typeof(TypeTypeConverter))]
        //public Type TypeVal
        //{
        //    get
        //    {
        //        TypeAndName typeName = (TypeAndName)this[this.typeVal];
        //        if (typeName != null)
        //        {
        //            return typeName.type;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set { this.typeVal = new TypeAndName(value); }
        //}

        //private Type _MyProperty = null;
        //[TypeConverter(typeof(ColumnTypeConverter))]
        //public Type MyProperty
        //{
        //    get { return _MyProperty; }
        //    set
        //    {
        //        if (_MyProperty != value)
        //        {
        //            _MyProperty = value;
        //            OnMyPropertyChanged();
        //        }
        //    }
        //}



        //private Type _MyProperty = typeof(string);
        private Type _MyProperty;

        //[DefaultValue(typeof(string))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [TypeConverter(typeof(ColumnTypeConverter))]
        public Type MyProperty
        {
            get { return _MyProperty; }
            set
            {
                //if (_MyProperty != value)
                //{
                //    _MyProperty = value;
                //    SetType(_MyProperty);
                //    OnMyPropertyChanged();
                //}
                _MyProperty = value;
            }
        }


        //private void SetType(Type t)
        //{
        //    this.ValueType = t;
        //}



        //[DefaultValue(typeof(String))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[Browsable(true)]
        //[TypeConverter(typeof(ColumnTypeConverter))]
        //new public Type ValueType
        //{
        //    get { return base.ValueType; }
        //    set
        //    {
        //        base.ValueType = value;
        //        OnMyPropertyChanged();
        //    }
        //}

        ////event System.Threading.ThreadStart MyPropertyChangedEvent;
        //internal void OnMyPropertyChanged()
        //{
        //    //var o = this.HeaderCell;
        //    if (this.DataGridView is ADGV.AdvancedDataGridView)
        //    {
        //        var advdgv = this.DataGridView as ADGV.AdvancedDataGridView;
        //        advdgv.OnMyPropertyChanged(new System.Windows.Forms.DataGridViewColumnEventArgs(this));
        //    }
        //    //if (MyPropertyChangedEvent != null)
        //    //{
        //    //    MyPropertyChangedEvent();
        //    //}
        //}

        //public enum Types { Int16, Int32, Int64, String, Decimal, Bool, GUID, Byte };
        //private Types _valueTypeDefinition = Types.String;

        //[System.ComponentModel.DefaultValue(Types.String)]
        //public Types ValueTypeDefinition
        //{
        //    get { return _valueTypeDefinition; }
        //    set { 
        //        _valueTypeDefinition = value;
        //        SetValue();
        //        }
        //}

        //private void SetValue()
        //{
        //    Type t;

        //    switch (ValueTypeDefinition)
        //    {
        //        case Types.Int16:
        //            t = Type.GetType("System.Int16");
        //            break;
        //        case Types.Int32:
        //            t = Type.GetType("System.Int32");
        //            break;
        //        case Types.Int64:
        //            t = Type.GetType("System.Int64");
        //            break;
        //        case Types.Decimal:
        //            t = Type.GetType("System.Decimal");
        //            break;
        //        case Types.String:
        //            t = Type.GetType("System.String");
        //            break;
        //        case Types.Bool:
        //            t = Type.GetType("System.Boolean"); 
        //            break;
        //        case Types.GUID:
        //            t = Type.GetType("System.Guid");
        //            break;
        //        case Types.Byte:
        //            t = Type.GetType("System.Byte");
        //            break;
        //        default:
        //            t = Type.GetType("System.String");
        //        break;
        //    }

        //    this.ValueType = t;
        //}


        public override object Clone()
        {
            var column = base.Clone() as DataGridViewTextBoxValueTypeColumn;
            if (column != null)
            {
                //column.ValueTypeDefinition = this.ValueTypeDefinition;
                //column.ValueType = this.ValueType;
                column.MyProperty = this.MyProperty;
            }
            return column;
        } 



        ////[System.ComponentModel.DefaultValue(typeof(string))]
        //////[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.TypeListConverter))]
        ////[System.ComponentModel.TypeConverter(typeof(MyTypeListConverter))]
        ////[System.ComponentModel.Browsable(true)]
        ////[System.ComponentModel.Category("Type")]
        ////[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        ////new public Type ValueType
        ////{
        ////    get { return base.ValueType; }
        ////    set { base.ValueType = value; }
        ////}

        //[System.ComponentModel.DefaultValue("XXXXX")]
        ////[System.ComponentModel.TypeConverter(typeof(MyTypeListConverter))]
        ////[System.ComponentModel.Browsable(true)]
        //[System.ComponentModel.Category("Type")]
        ////[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        //public string ValueTypeDefinition
        //{
        //    get;
        //    set;
        //}

        //private bool _xxx = true;
        //[System.ComponentModel.Browsable(true)]
        //[System.ComponentModel.Category("Type")]
        //public bool XXX
        //{
        //    get { return _xxx; }
        //    set { _xxx = value; }
        //}


        //class TypeTypeConverter : TypeConverter
        //{
        //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        //    {
        //        if (sourceType == typeof(string))
        //        {
        //            return true;
        //        }
        //        return base.CanConvertFrom(context, sourceType);
        //    }

        //    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        //    {
        //        if (value is string)
        //        {
        //            return new TypeAndName((string)value);
        //        }

        //        return base.ConvertFrom(context, culture, value);
        //    }

        //    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        //    {
        //        if (destinationType == typeof(string))
        //        {
        //            TypeAndName castedValue = (TypeAndName)value;
        //            return castedValue.name == null ? castedValue.type.AssemblyQualifiedName : castedValue.name;
        //        }

        //        return base.ConvertTo(context, culture, value, destinationType);
        //    }
        //}

        //class TypeAndName
        //{
        //    public TypeAndName(string name)
        //    {
        //        this.type = Type.GetType(name, true, true);
        //        this.name = name;
        //    }

        //    public TypeAndName(Type type)
        //    {
        //        this.type = type;
        //    }

        //    public override int GetHashCode()
        //    {
        //        return type.GetHashCode();
        //    }

        //    public override bool Equals(object comparand)
        //    {
        //        return type.Equals(((TypeAndName)comparand).type);
        //    }

        //    public readonly Type type;
        //    public readonly string name;
        //}
    }
}
