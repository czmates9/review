using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.ImportnyMustky
{
    public interface IImportnyMustky_GetImportPohoda_SKzNC : IImportnyMustky
    {
        DataSets_Import.ImportPOHODA_FromExcel GetImportPohoda_SKzNC( Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr filtry);
    }
}
