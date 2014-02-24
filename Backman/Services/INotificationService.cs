using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Backman.Services.Services
{
    [ServiceContract(CallbackContract=typeof(INotificationCallback))]
    interface INotificationService
    {
        [OperationContract]
        void Register();

        [OperationContract]
        void Unregister();
    }
}
