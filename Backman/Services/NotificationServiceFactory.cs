using System;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Backman.Services
{
    class NotificationServiceFactory : Component, IChannelFactory<INotificationService>
    {
        private DuplexChannelFactory<INotificationService> innerFactory;

        public NotificationServiceFactory()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                this.Init(Program.BeanFactory.Get<Backman.Services.INotificationCallback>("notificationCallback"));
            }
        }

        public NotificationServiceFactory(IContainer container) : this()
        {
            if (container != null)
            {
                container.Add(this);
            }
        }

        private void Init(INotificationCallback callbackObject)
        {
            this.innerFactory = new DuplexChannelFactory<INotificationService>(callbackObject, "NotificationService");
            this.innerFactory.Closed += (sender, e) => this.OnClosed(e);
            this.innerFactory.Closing += (sender, e) => this.OnClosing(e);
            this.innerFactory.Faulted += (sender, e) => this.OnFaulted(e);
            this.innerFactory.Opened += (sender, e) => this.OnOpened(e);
            this.innerFactory.Opening += (sender, e) => this.OnOpening(e);
            this.CallbackObject = callbackObject;
        }

        protected void OnClosed(EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, e);
            }
        }

        protected void OnClosing(EventArgs e)
        {
            if (this.Closing != null)
            {
                this.Closing(this, e);
            }
        }

        protected void OnFaulted(EventArgs e)
        {
            if (this.Faulted != null)
            {
                this.Faulted(this, e);
            }
        }

        protected void OnOpened(EventArgs e)
        {
            if (this.Opened != null)
            {
                this.Opened(this, e);
            }
        }

        protected void OnOpening(EventArgs e)
        {
            if (this.Opening != null)
            {
                this.Opening(this, e);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Services.INotificationCallback CallbackObject { get; private set; }

        INotificationService IChannelFactory<INotificationService>.CreateChannel(EndpointAddress to, Uri via)
        {
            return this.innerFactory.CreateChannel(to, via);
        }

        INotificationService IChannelFactory<INotificationService>.CreateChannel(EndpointAddress to)
        {
            return this.innerFactory.CreateChannel(to);
        }

        public INotificationService CreateChannel()
        {
            return this.innerFactory.CreateChannel();
        }

        public T GetProperty<T>() where T : class
        {
            return this.innerFactory.GetProperty<T>();
        }

        public void Abort()
        {
            this.innerFactory.Abort();
        }

        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return this.innerFactory.BeginClose(timeout, callback, state);
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return this.innerFactory.BeginClose(callback, state);
        }

        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return this.innerFactory.BeginOpen(timeout, callback, state);
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            return this.innerFactory.BeginOpen(callback, state);
        }

        public void Close(TimeSpan timeout)
        {
            this.innerFactory.Close(timeout);
        }

        public void Close()
        {
            this.innerFactory.Close();
        }

        public event EventHandler Closed;

        public event EventHandler Closing;

        public void EndClose(IAsyncResult result)
        {
            this.innerFactory.EndClose(result);
        }

        public void EndOpen(IAsyncResult result)
        {
            this.innerFactory.EndOpen(result);
        }

        public event EventHandler Faulted;

        public void Open(TimeSpan timeout)
        {
            this.innerFactory.Open(timeout);
        }

        public void Open()
        {
            this.innerFactory.Open();
        }

        public event EventHandler Opened;

        public event EventHandler Opening;

        public CommunicationState State
        {
            get { return this.innerFactory.State; }
        }
    }
}
