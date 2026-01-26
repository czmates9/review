using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.ImportnyMustky
{
    public interface IImportnyMustky_ImportPohoda_SKzNC_EXIST : IImportnyMustky
    {

        bool EXIST(string ColumnName, string TableName, object value);
    }
}
