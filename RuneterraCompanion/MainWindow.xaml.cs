using RuneterraCompanion.Configuration.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuneterraCompanion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Container container { get; set; }
        public IConfigurationTracker Tracker { get; set; }
        public IUserConfiguration Configuration { get; set; }

        //SimpleInjector only allows 1 constructor
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        public MainWindow(Container container)
        {
            InitializeComponent();

            this.container = container;
            Tracker = container.GetInstance<IConfigurationTracker>();
            Configuration = container.GetInstance<IUserConfiguration>();

            Tracker.Tracker.Track(Configuration);
        }
    }
}
