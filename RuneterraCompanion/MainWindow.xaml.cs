using SimpleInjector;
using System;
using System.Windows;
using System.IO;
using RuneterraCompanion.Helpers;

namespace RuneterraCompanion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Container container { get; set; }


        //SimpleInjector only allows 1 constructor
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        public MainWindow(Container container)
        {
            InitializeComponent();

            this.container = container;
            //Tracker = container.GetInstance<IConfigurationTracker>();
            //Configuration = container.GetInstance<IUserConfiguration>();

            //Tracker.Tracker.Track(Configuration);

            if (LocalFilesHelper.IsDownloadNeeded(Directory.GetCurrentDirectory()))
            {
                MessageBox.Show("Download is needed for the application to work properly.\nPlease select Check cards integrity under Settings tab.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
