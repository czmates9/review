using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server.WebControls.Inventura1
{
    public partial class WebUserControl2 : System.Web.UI.UserControl
    {
        private int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            object o = Session["providerInventura1"];

            this.Button1.Text = o.ToString() + " : " + (count++).ToString();
        }
    }
}