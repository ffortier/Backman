using NBean;
using System;
using System.Windows.Forms;

namespace Backman.Services
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BeanFactory = new BeanFactory();

#if DEBUG
            BackService bs = new BackService(BeanFactory);

            bs.Start(new String[0]);

            Application.ApplicationExit += (sender, e) => bs.Stop();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static BeanFactory BeanFactory { get; private set; }
    }
}
