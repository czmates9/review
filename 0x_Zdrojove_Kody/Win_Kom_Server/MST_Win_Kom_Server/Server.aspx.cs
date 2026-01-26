using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server
{
    public partial class Server : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            object userauthenticated = Session["UserAuthenticated"];
            if (userauthenticated == null || !((bool)userauthenticated))
            {
                //Server.Transfer("Login.aspx");
                Response.Redirect("Login.aspx");
            }


            if (Page.IsPostBack)
            {



            }
        }
    }
}