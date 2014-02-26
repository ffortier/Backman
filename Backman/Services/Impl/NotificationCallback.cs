using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Backman.Services.Impl
{
    [ServiceBehavior(UseSynchronizationContext=true)]
    class NotificationCallback : INotificationCallback
    {
        public void ReportProgress(Model.ProgressData data)
        {
            System.Windows.Forms.MessageBox.Show(data.ToString());
        }
    }
}
