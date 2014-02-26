using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backman.Services.Model;

namespace Backman.Services
{
    interface INotificationServiceExtended : INotificationService
    {
        void ReportProgress(ProgressData data);
    }
}
