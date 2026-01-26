using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_Import_ToXML_Inventura : IInventura2
    {

        StatusInfo ImportToXMLInventura(string CountEntries, string Path);

    }
}
