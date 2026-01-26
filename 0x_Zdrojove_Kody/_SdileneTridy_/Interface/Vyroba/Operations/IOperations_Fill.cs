using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Operations
{
    public interface IOperations_Fill : IOperations
    {
        void Operations_Fill(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
