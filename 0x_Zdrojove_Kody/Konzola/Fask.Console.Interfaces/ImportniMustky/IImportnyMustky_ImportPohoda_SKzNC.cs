using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.ImportnyMustky
{
    public interface IImportnyMustky_ImportPohoda_SKzNC : IImportnyMustky
    {
        bool ImportPohoda_SKzNC(Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable dt);
    }
}
