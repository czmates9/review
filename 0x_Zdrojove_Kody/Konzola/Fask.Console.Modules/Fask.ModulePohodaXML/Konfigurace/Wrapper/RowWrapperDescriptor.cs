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
    class RowWrapperDescriptor : PropertyDescriptor
    {
        static Attribute[] GetAttribs(AttributeCollection value)
        {
            if (value == null) return null;
            Attribute[] result = new Attribute[value.Count];
            value.CopyTo(result, 0);
            return result;
        }

        readonly PropertyDescriptor innerProp;

        private string Cat = string.Empty;
        private RowWrapper rw = null;

        public RowWrapperDescriptor(PropertyDescriptor innerProperty, string Category, RowWrapper rw) : base(innerProperty.Name, GetAttribs(innerProperty.Attributes))
        {
            this.innerProp = innerProperty;
            this.Cat = Category;
            this.rw = rw;
        }


        public override bool ShouldSerializeValue(object component)
        {
            return innerProp.ShouldSerializeValue(this.rw.GetRowView(component));
        }
        public override void ResetValue(object component)
        {
            innerProp.ResetValue(this.rw.GetRowView(component));
        }
        public override bool CanResetValue(object component)
        {
            return innerProp.CanResetValue(this.rw.GetRowView(component));
        }
        public override void SetValue(object component, object value)
        {
            innerProp.SetValue(this.rw.GetRowView(component), value);
        }
        public override object GetValue(object component)
        {
            return innerProp.GetValue(this.rw.GetRowView(component));
        }
        public override Type PropertyType
        {
            get { return innerProp.PropertyType; }
        }
        public override Type ComponentType
        {
            get { return typeof(RowWrapper); }
        }
        public override bool IsReadOnly
        {
            get { return innerProp.IsReadOnly; }
        }

        /// <summary>
        /// TaD Override, skoušeni kategorie
        /// </summary>
        public override string Category
        {
            get
            {
                return this.Cat;
            }

        }
    }
}
