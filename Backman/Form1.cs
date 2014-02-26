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
using Backman.Services;
using Backman.Services.Impl;

namespace Backman.Services
{
    public partial class Form1 : Form
    {
        private INotificationService channel;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            channel = this.notificationServiceFactory1.CreateChannel();
            channel.Register();
        }
    }
}
