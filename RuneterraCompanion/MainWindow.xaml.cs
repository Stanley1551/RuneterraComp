using SimpleInjector;
using System;
using System.Windows;

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
        }
    }
}
