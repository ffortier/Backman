using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Backman.Services.Model;

namespace Backman.Services.Impl
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant, InstanceContextMode=InstanceContextMode.Single)]
    class NotificationService : INotificationServiceExtended
    {
        private IOperationContextLookUp contextLookUp;
        private List<INotificationCallback> observables = new List<INotificationCallback>();

        public NotificationService(IOperationContextLookUp contextLookUp)
        {
            this.contextLookUp = contextLookUp;
        }

        public void Register()
        {
            var callback = contextLookUp.Current.GetCallbackChannel<INotificationCallback>();

            observables.Add(callback);
        }

        public void Unregister()
        {
            var callback = contextLookUp.Current.GetCallbackChannel<INotificationCallback>();

            observables.Remove(callback);
        }

        public void ReportProgress(ProgressData data)
        {
            observables.ForEach(o => o.ReportProgress(data));
        }
    }
}
