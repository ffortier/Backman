using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Backman.Services.Impl
{
    class OperationContextLookUp : IOperationContextLookUp
    {
        public OperationContext Current
        {
            get { return OperationContext.Current; }
        }
    }
}
