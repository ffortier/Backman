using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Backman.Services.Services.Model;
namespace Backman.Services.Services
{
    interface INotificationCallback
    {
        [OperationContract(IsOneWay=true)]
        void ReportProgress(ProgressData data);
    }
}
