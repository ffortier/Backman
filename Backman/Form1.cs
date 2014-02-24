using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using Backman.Services.Services;
using Backman.Services.Services.Impl;

namespace Backman.Services
{
    public partial class Form1 : Form
    {
        static Form1()
        {
            Injector.Infer<NotificationServiceFactory>(() => new NotificationServiceFactory());
        }

        private NotificationServiceFactory factory;
        private INotificationService channel;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            factory = Injector.Get<NotificationServiceFactory>();
            channel = factory.CreateChannel();
            channel.Register();
        }
    }
}
