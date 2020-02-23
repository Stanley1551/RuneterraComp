using RuneterraCompanion.Common;
using RuneterraCompanion.Factory;
using RuneterraCompanion.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuneterraCompanion
{
    public partial class MyCardsTab : UserControl
    {
        private MainWindow mainWindow = null; // Reference to the MainWindow

        public MyCardsTab()
        {
            InitializeComponent();
        }

        // get a reference to main windows when it is available.
        // The Loaded Event is set in the XAML code above.
        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            mainWindow = Window.GetWindow(this) as MainWindow;
        }

        private void AccessMainWindowsWidget()
        {

        }

        private void CardScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void StartMatchButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await GameRequestFactory.Get(Enums.RequestType.StaticDeckList) as StaticDeckList;
            ImageList.ItemsSource = result.ConvertToCardList();
            ImageList.Visibility = Visibility.Visible;
        }
    }
}
