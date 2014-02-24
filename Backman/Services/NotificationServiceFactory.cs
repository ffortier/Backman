using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backman.Services
{
    class NotificationServiceFactory : System.ServiceModel.DuplexChannelFactory<Backman.Services.Services.INotificationService>
    {
        static NotificationServiceFactory()
        {
            Injector.Infer<Backman.Services.Services.INotificationCallback>(new Backman.Services.Services.Impl.NotificationCallback());
        }

        public NotificationServiceFactory()
            : this(Injector.Get<Backman.Services.Services.INotificationCallback>())
        {

        }

        public NotificationServiceFactory(Backman.Services.Services.INotificationCallback callbackObject)
            : base(callbackObject, "NotificationService")
        {
            this.CallbackObject = callbackObject;
        }

        public Services.INotificationCallback CallbackObject { get; private set; }
    }
}
