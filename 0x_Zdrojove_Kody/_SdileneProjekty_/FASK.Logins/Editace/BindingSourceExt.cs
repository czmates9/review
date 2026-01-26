using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace FASK.Logins.Extensions
{
    public class Key
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
    }

    public static class BindingSourceExt
    {
        public static void FindAndSelect(this BindingSource source, params Key[] keys)
        {
            try
            {
                source.Position = source.Find(keys);

            }
            catch //(Exception ex)
            {
				//Fask.Logging.ExceptionHandler2.HandleErrorLog(ex);
            }
        }
        //public static int Find(this BindingSource source, params Key[] keys)
        public static int Find(this BindingSource source, params Key[] keys)
        {
            PropertyDescriptor[] properties = new PropertyDescriptor[keys.Length];

            ITypedList typedList = source as ITypedList;

            if (source.Count <= 0) return -1;

            PropertyDescriptorCollection props;

            if (typedList != null) // obtain the PropertyDescriptors from the list
            {
                props = typedList.GetItemProperties(null);
            }
            else // use the TypeDescriptor on the first element of the list
            {
                props = TypeDescriptor.GetProperties(source[0]);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                properties[i] = props.Find(keys[i].PropertyName, true); // will throw if the property isn't found
            }

            for (int i = 0; i < source.Count; i++)
            {
                object row = source[i];
                bool match = true;

                for (int p = 0; p < keys.Count(); p++)
                {
                    //if (properties[p].GetValue(row) != keys[p].Value)
                    if (properties[p].GetValue(row).ToString() != keys[p].Value.ToString())
                    {
                        match = false;
                        break;
                    }
                }

                if (match) return i;
            }

            return -1;
        }
    }
}
