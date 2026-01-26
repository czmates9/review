using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Inventura
{
    public enum TypeFile 
    {
        XML,
        CSV, // třeba do budoucna...
        Unknow

    }

    public interface IInventura2_Generate_FromFile_Inventura : IInventura2
    {
        StatusInfo Genetare_FromFile_Inventura(TypeFile typSouboru,string CountEntries, string Desc, string Path);
    }
}
