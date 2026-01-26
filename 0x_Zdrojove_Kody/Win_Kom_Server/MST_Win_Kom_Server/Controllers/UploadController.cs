using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Fask.MST_W_Server.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        [Route("api/UploadFileWithStream")]
        public HttpResponseMessage UploadFileWithStream()
        {

            var me = JsonConvert.DeserializeObject<Fask.RestSharp.API.UploadFileObject>((HttpContext.Current.Request.Params["model"]).ToString());
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var postedFile = HttpContext.Current.Request.Files[file];

                    string path = System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, me.PathFileOnServer);
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));

                    byte[] data;
                    using (BinaryReader reader = new BinaryReader(postedFile.InputStream))
                    {
                        data = reader.ReadBytes((int)postedFile.InputStream.Length);
                    }

                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        writer.Write(data);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

    }

}