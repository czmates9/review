using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Fask.MST_W._WebRefernces_Globals
{
    public class EventsServiceSession : EventsService.EventsService
    {
        #region Session state by cookies ...
        private string cookie = null;
        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            //return base.GetWebRequest(uri);
            WebRequest req = (WebRequest)base.GetWebRequest(uri);
            if (cookie != null)
            {
                req.Headers.Add("Cookie", cookie);
            }
            return req;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            //return base.GetWebResponse(request);
            WebResponse rep = (WebResponse)base.GetWebResponse(request);
            if (rep.Headers["Set-Cookie"] != null)
            {
                cookie = rep.Headers["Set-Cookie"];
            }
            return rep;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            //return base.GetWebResponse(request, result);
            WebResponse rep = (WebResponse)base.GetWebResponse(request, result);
            if (rep.Headers["Set-Cookie"] != null)
            {
                cookie = rep.Headers["Set-Cookie"];
            }
            return rep;
        }
        #endregion
    }
}
