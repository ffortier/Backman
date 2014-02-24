using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Backman.Services.Services.Model;

namespace Backman.Services.Services.Impl
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant, InstanceContextMode=InstanceContextMode.Single)]
    class NotificationService : INotificationService
    {
        static NotificationService()
        {
            Injector.Infer<OperationContext>("Current", () => OperationContext.Current);
        }

        private List<INotificationCallback> observables = new List<INotificationCallback>();

        public void Register()
        {
            var callback = Injector.Get<OperationContext>("Current").GetCallbackChannel<INotificationCallback>();

            observables.Add(callback);
        }

        public void Unregister()
        {
            var callback = Injector.Get<OperationContext>("Current").GetCallbackChannel<INotificationCallback>();

            observables.Remove(callback);
        }

        public void ReportProgress(ProgressData data)
        {
            observables.ForEach(o => o.ReportProgress(data));
        }
    }
}
