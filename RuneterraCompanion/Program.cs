using System;
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

            // Any additional other configuration

            RunApplication(container);
        }

        private static Container CreateContainer()
        {
            // Container creation
            var container = new Container();

            // Types
            container.Register<IUserConfiguration, UserConfiguration>(Lifestyle.Singleton);
            container.Register<IConfigurationTracker, ConfigurationTracker>(Lifestyle.Singleton);

            // Windows
            container.Register<MainWindow>();

            // Verification
            container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                //app.InitializeComponent();
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