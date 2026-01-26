using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server
{
    public partial class Inventura_Page : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            FASK.Logins.DataSets.Pristupy.FASK_LoginsRow uz = (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow)Session["ActualUser"];

            if (uz == null)
            {
                Response.Redirect(@"..\Default.aspx");
            }

            Fask.Server.Interfaces.Classes.User uzivatel = new Fask.Server.Interfaces.Classes.User();

            uzivatel.ADM = 1;
            uzivatel.barcode = uz.USERID;
            uzivatel.firstname = uz.firstname;
            uzivatel.ID = int.Parse(uz.USERID);
            uzivatel.Login = uz.USERID;
            uzivatel.Password = uz.psswd;
            uzivatel.secondname = uz.surname;

            if (Session["providerInventura1"] != null)
                CreateTreeMenuInventura1(uzivatel);

            if (Page.IsPostBack)
            {
                TreeNode tn = null;

                //if (Session["selectedMenuItem"] != null)
                //    tn = (TreeNode)Session["selectedMenuItem"];

                tn = TreeMenu.SelectedNode;


                if (tn != null && tn.Parent != null && tn.Parent.Value == "Inventura1" && Session["providerInventura1"] != null)
                {
                    PlaceHolder actualObject = null;

                    actualObject = (PlaceHolder)((Fask.Server.Interfaces.WebControl.IWebControl)Session["providerInventura1"]).getAction(tn, this.Page);

                    if (actualObject != null)
                    {
                        PlaceHolder1.Controls.Clear();
                        PlaceHolder1.EnableViewState = false;
                        PlaceHolder1.Controls.Add(actualObject);
                    }
                }


            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //http://msmvps.com/blogs/shahed/archive/2008/06/26/asp-net-tips-golden-rules-for-dynamic-controls.aspx
            //http://www.aspnet.cz/Articles/27-dynamicke-vytvareni-asp-net-server-controls.aspx

            #region  nacteni provideru
            if (Session["providerInventura1"] == null)
            {
                try
                {
                    Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                    string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Inventura1;
                    string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                    if (String.IsNullOrEmpty(providerAssemblyPath))
                        providerAssemblyPath = providerAssemblyPathGlobal;

                    if (!String.IsNullOrEmpty(providerAssemblyPath))
                    {
                        string PathToAssambly = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, providerAssemblyPath);

                        // = Server.MapPath(providerAssemblyPath);
                        Assembly providerAssemlby = Assembly.LoadFrom(PathToAssambly);
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Server.Interfaces.WebControl.IWebControl).IsAssignableFrom(t))
                                {
                                    Session["providerInventura1"] = (Fask.Server.Interfaces.WebControl.IWebControl)providerAssemlby.CreateInstance(t.FullName);
                                    if (Session["providerInventura1"] != null)
                                    {

                                        break;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.ExceptionHandler2.Handle(ex);
                            }
                        }
                    }
                }
                catch(Exception ex) 
                {
                    Logging.ExceptionHandler2.Handle(ex);
                }
            }

            #endregion
        }


        private bool CreateTreeMenuInventura1(Fask.Server.Interfaces.Classes.User uzivatel)
        {


            List<TreeNode> list = ((Fask.Server.Interfaces.WebControl.IWebControl)Session["providerInventura1"]).getActions(uzivatel);

            TreeNode tnI1 = TreeMenu.FindNode("Inventura1");
            if (tnI1 == null)
                return false;

            foreach (TreeNode item in list)
            {
                if (TreeMenu.FindNode(tnI1.ValuePath + "/" + item.Value) == null)
                    tnI1.ChildNodes.Add(item);
            }

            return true;
        }

    }
}