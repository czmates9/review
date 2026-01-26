using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Controls
{
    public partial class ucDetail : UserControl
    {
        public ucDetail()
        {
            InitializeComponent();
        }

        public ucDetail(object detailObject)
            : this()
        {
            DetialObject = detailObject;
        }

        public object DetialObject
        {
            get { return propertyGrid1.SelectedObject; }
            set { propertyGrid1.SelectedObject = value; }
        }
    }
}
