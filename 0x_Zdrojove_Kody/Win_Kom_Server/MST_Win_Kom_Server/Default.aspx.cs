using Fask.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MST_Win_Konfig_Server
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            object userauthenticated = Session["UserAuthenticated"];

            var text = "user is not authenticated";
            try
            {
                if(userauthenticated != null)
                    text = userauthenticated.ToString();

            } catch { }

            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Default. Prihlaseni Uzivatele Server Autorizace session: " + text);

            if (userauthenticated == null || !((bool)userauthenticated))
            {
                //Server.Transfer("Login.aspx");
                Response.Redirect(@"Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }


            if (Page.IsPostBack)
            {



            }

        }


    }
}