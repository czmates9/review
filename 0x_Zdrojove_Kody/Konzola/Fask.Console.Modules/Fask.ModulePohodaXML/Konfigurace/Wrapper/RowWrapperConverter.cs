using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.ModulePohodaXML
{
    public class RowWrapperConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override PropertyDescriptorCollection GetProperties( ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            RowWrapper rw = (RowWrapper)value;

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(rw.GetRowView(), attributes);

            List<PropertyDescriptor> result = new List<PropertyDescriptor>(props.Count);

            foreach (PropertyDescriptor prop in props)
            {
                if (rw.Exclude.Contains(prop.Name))
                { 
                    continue; 
                }

                result.Add(new RowWrapperDescriptor(prop, rw.Category, rw));
            }
            return new PropertyDescriptorCollection(result.ToArray());
        }
    }
}
