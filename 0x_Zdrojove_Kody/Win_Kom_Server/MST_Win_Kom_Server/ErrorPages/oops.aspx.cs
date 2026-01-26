using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server.ErrorPages
{
    public partial class oops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"..\Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }


}