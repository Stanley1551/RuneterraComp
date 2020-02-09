using System;
using System.Net;
using System.Windows;
using RuneterraCompanion;
using RuneterraCompanion.Configuration;
using RuneterraCompanion.Configuration.Interfaces;
using SimpleInjector;

namespace RuneterraCompanion
{

    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = CreateContainer();

            RunApplication(container);
        }

        private static Container CreateContainer()
        {
            // Container creation
            var container = new Container();

            // Types
            container.Register<IUserConfiguration>(() => new UserConfiguration(), Lifestyle.Singleton);
            container.Register<IConfigurationTracker>(() => new ConfigurationTracker(), Lifestyle.Singleton);

            // Windows
            container.Register(() => new MainWindow(container), Lifestyle.Singleton);

            //Services
            //container.Register<WebClient>();

            // Verification
            container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                var mainWindow = container.GetInstance<MainWindow>();
                //set a reference?
                mainWindow.container = container;
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                //Log the exception and exit
            }
        }
    }
}