﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Backman.Services.Model;
namespace Backman.Services
{
    interface INotificationCallback
    {
        [OperationContract(IsOneWay=true)]
        void ReportProgress(ProgressData data);
    }
}
