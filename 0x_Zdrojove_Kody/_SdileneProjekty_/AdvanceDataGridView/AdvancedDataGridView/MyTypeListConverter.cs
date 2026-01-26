using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zuby
{

    
    //public class MyTypeListConverter : TypeListConverter
    //{
    //    public MyTypeListConverter()
    //        : base(new Type[] { 
    //            //Type.GetType("System.Int32"), 
    //            typeof(System.Int32),
    //            Type.GetType("System.Int64"), 
    //            Type.GetType("System.String"),
    //            Type.GetType("System.Decimal") 
    //        })
    //    {
    //    }
    //}


    //public class MyConverter : TypeConverter
    //{

    //    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //    {
    //        return typeof(Type) == destinationType;
    //    }

    //    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (destinationType == typeof(Type))
    //        {
    //            Type m = value as Type;
    //            return m.FullName;
    //        }

    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }

    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        return typeof(string) == sourceType;
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    //    {
    //        string str = value as string;
    //        if (str != null)
    //        {

    //            Type m = Type.GetType(str);

    //            return m;
    //            //var tmp = context.Instance;
    //            //Type m;
    //            //if (str.EndsWith("mm"))
    //            //{
    //            //    m.Millimeters = double.Parse(str.Substring(0, str.Length - 2));
    //            //}
    //            //else if (str.EndsWith("in"))
    //            //{
    //            //    m.Inches = double.Parse(str.Substring(0, str.Length - 2));
    //            //}
    //            //else //assume mm
    //            //{
    //            //    try
    //            //    {
    //            //        m.Millimeters = double.Parse(str);
    //            //    }
    //            //    catch { }
    //            //}
    //            //return m;



    //        }
    //        return base.ConvertFrom(context, culture, value);
    //    }


    //}


   

}
