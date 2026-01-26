using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Keyboard
{
    public partial class UCKeyboard : UserControl
    //public partial class UCKeyboard : Panel
    {
        public UCKeyboard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, false);
        }

        private void label_Click(object sender, EventArgs e)
        {
            char X;
            Label l = sender as Label;
            if (l == null)
                return;

            X = l.Text.Trim()[0];
         
            SendKeys.Send(X.ToString());

        }

    }
}
