using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.ImportnyMustky
{
    public interface IImportnyMustky_Insert_ImportSKzNC : IImportnyMustky
    {
        int Insert_ImportSKzNC(Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow row);
    }
}
