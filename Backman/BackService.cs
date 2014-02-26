using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;
using Backman.Services;
using NBean;

namespace Backman.Services
{
    class BackService : System.ServiceProcess.ServiceBase
    {
        private ServiceHost host;
        private INotificationServiceExtended singleton;

        public BackService(BeanFactory beanFactory)
        {
            this.singleton = beanFactory.Get<INotificationServiceExtended>("notificationService");
            this.host = new ServiceHost(singleton);
            this.host.Opened += host_Opened;
        }

#if DEBUG
        private Thread t;

        public void Start(String[] args)
        {
            ParameterizedThreadStart fn = delegate(Object o)
            {
                BackService bs = (BackService)o;

                bs.OnStart(args);

                lock (this)
                {
                    Monitor.Wait(bs);

                    bs.OnStop();
                }
            };
            
            t = new Thread(fn);
            
            t.Start(this);
        }

        public new void Stop()
        {
            lock (this)
            {
                Monitor.PulseAll(this);
            }

            t.Join();
        }
#endif

        protected override void Dispose(bool disposing)
        {
            ((IDisposable)this.host).Dispose();

            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Starting");
            this.host.Open();
            base.OnStart(args);
            Debug.WriteLine("Started");
        }

        void host_Opened(object sender, EventArgs e)
        {
            Timer timer = null;

            TimerCallback callback = delegate(object state) {
                singleton.ReportProgress(new Services.Model.ProgressData());
                timer.Dispose();
            };

            timer = new Timer(callback, null, 2000, Timeout.Infinite);
        }

        protected override void OnStop()
        {
            Debug.WriteLine("Stopping");
            this.host.Close();
            base.OnStop();
            Debug.WriteLine("Stopped");
        }
    }
}
