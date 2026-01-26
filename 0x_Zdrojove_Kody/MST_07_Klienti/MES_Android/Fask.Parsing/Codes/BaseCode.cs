using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
    public class BaseCode
    {
        public virtual string Nazev { get; }

        List<string> codes;

        public BaseCode(List<string> codes)
        {
            this.codes = codes;
        }
    }
}
