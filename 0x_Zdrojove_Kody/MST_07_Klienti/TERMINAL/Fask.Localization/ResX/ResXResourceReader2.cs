using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Resources;

namespace Fask.Localization.ResX
{
    public class ResXResourceReader2 : IResourceReader
    {
        #region IResourceReader Members

        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <include file='doc\ResXResourceReader.uex' path='docs/doc[@for="ResXResourceReader.Dispose"]/*' />
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (fileName != null && stream != null)
                {
                    stream.Close();
                    stream = null;
                }

                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
            }
        }

        #endregion
    }
}
