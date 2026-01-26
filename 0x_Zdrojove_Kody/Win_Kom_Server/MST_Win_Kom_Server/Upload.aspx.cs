using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Fask.MST_W_Server
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = string.Empty;
            try
            {
                StreamReader sr = new StreamReader(Request.InputStream);
                data = sr.ReadToEnd(); 
                sr.Close();
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            foreach (string f in Request.Files.AllKeys)
            {

                try
                {
                HttpPostedFile file = Request.Files[f];

                string path = System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, file.FileName);
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                file.SaveAs(path);

        }
                catch (Exception ex)
                {
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }
        }
    }
}
