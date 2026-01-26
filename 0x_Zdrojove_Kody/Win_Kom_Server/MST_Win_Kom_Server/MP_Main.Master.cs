using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server
{
    public partial class MP_Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            object userauthenticated = Session["UserAuthenticated"];
            if (userauthenticated == null || !((bool)userauthenticated))
            {
                //Server.Transfer("~/Login.aspx");
                Response.Redirect("Login.aspx",false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
               

            //DataSets.Pristupy.FASK_LoginsRow uz = (DataSets.Pristupy.FASK_LoginsRow)Session["ActualUser"];
            //lblUser.Text = uz.firstname + " " + uz.surname;

            //Fask.Server.Interfaces.Classes.User uzivatel = (Fask.Server.Interfaces.Classes.User)Session["ActualUser"];
            FASK.Logins.DataSets.Pristupy.FASK_LoginsRow uz = (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow)Session["ActualUser"];

            Fask.Server.Interfaces.Classes.User uzivatel = new Fask.Server.Interfaces.Classes.User();

            uzivatel.ADM = 1;
            uzivatel.barcode = uz.USERID;
            uzivatel.firstname = uz.firstname;
            uzivatel.ID = int.Parse(uz.USERID);
            uzivatel.Login = uz.USERID;
            uzivatel.Password = uz.psswd;
            uzivatel.secondname = uz.surname;


            //lblUser.Text = uzivatel.Login;
            lblUser.Text = uzivatel.firstname + " " + uzivatel.secondname;

        }


        protected void LinkLogOut_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();

            //Server.Transfer("Login.aspx");
            //Response.Redirect("Login.aspx");
            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}