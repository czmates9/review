using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Routines
{
    public class Exceptions
    {
        #region Exception Handling
        /// <summary>
        /// Throws a soap exception.  It is formatted in a way that is more readable to the client, after being put through the xml serialisation process
        /// Typed exceptions don't work well across web services, so these exceptions are sent in such a way that the client
        /// can determine the 'name' or type of the exception thrown, and any message that went with it, appended after a : character.
        /// </summary>
        /// <param name="exceptionName">Name of exception</param>
        /// <param name="message">message of exception</param>
        /// <param name="qualifiedname">qualified name of the exception</param>
        public static System.Web.Services.Protocols.SoapException CustomSoapException(string exceptionName, string message, string qualifiedname)
        {
            System.Web.Services.Protocols.SoapException soapException =
                new System.Web.Services.Protocols.SoapException(
                (exceptionName == null ? string.Empty : exceptionName + ": ") + message, 
                new System.Xml.XmlQualifiedName(qualifiedname)
                );

            return soapException;
        }

        #endregion
    }
}
