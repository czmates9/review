using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

using System.Collections;
using System.ComponentModel.Design;
using System.Resources;
using System.Diagnostics;

namespace Fask.MST_W_Server.Konfigurace.Classes
{
    public class Name_Value_Comment
    {
        public string Name;
        public string Value;
        public string Comment;

        public Name_Value_Comment(string Name, string Value, string Comment)
        {
            this.Name = Name;
            this.Value = Value;
            this.Comment = Comment;
        }

    }

    public static class Page_Extensions
    {

        public static Name_Value_Comment GetLocalResource_Value_Comment(this System.Web.UI.Page page, string ColumnName)
        {

            Name_Value_Comment v = null;

            try
            {
                string Page = string.Empty;
                Page =  page.GetType().Name.ToUpper().Replace("_ASPX", ".aspx") + ".resx";
                Page = Page.Replace("KONFIGURACE_STRANKY_", "");


                string FilePath = string.Empty;

                if (Path.IsPathRooted(Page))
                    FilePath = Page;
                else
                {
                    string rootpath = HttpContext.Current.Server.MapPath(@"~\Konfigurace\Stranky\App_LocalResources");
                    FilePath = Path.Combine(rootpath, Page);
                }

                ResXResourceReader rr = new ResXResourceReader(FilePath);
                rr.UseResXDataNodes = true;

                IDictionaryEnumerator dict = rr.GetEnumerator();


                while (dict.MoveNext())
                {
                    ResXDataNode node = (ResXDataNode)dict.Value;

                    //Debug.WriteLine("Name: " + node.Name);
                    //Debug.WriteLine("Value: " + node.GetValue((ITypeResolutionService)null));
                    //Debug.WriteLine("Koment: " + node.Comment);
                    if (ColumnName == node.Name)
                    {
                        v = new Name_Value_Comment(
                            node.Name, 
                            (string)node.GetValue((ITypeResolutionService)null),
                            !String.IsNullOrEmpty(node.Comment) ? node.Comment : string.Empty);

                        break;
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return v;

        }

    }
}