using SimpleInjector;
using System;
using System.Windows.Forms;

namespace SimpleReportSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Ioc.Container = new Container();
            Ioc.Container.Register<DataProvider>(Lifestyle.Singleton);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
