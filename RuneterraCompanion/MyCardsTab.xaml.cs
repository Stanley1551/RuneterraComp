using RuneterraCompanion.Common;
using RuneterraCompanion.CustomModels;
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
        public List<Card> Cards;

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

        private void CardScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            SubscribeToEvents();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeFromEvents();
        }

        private async void StartMatchButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO handle match not started!
            var result = await GameRequestFactory.Get(Enums.RequestType.StaticDeckList) as StaticDeckList;
            Cards = result.ConvertToCardList();
            ImageList.ItemsSource = Cards;
            ImageList.Visibility = Visibility.Visible;
        }

        private void SubscribeToEvents()
        {
            CardFilterHeaderControl.FilterButton.Click += FilterButton_Click;
        }

        private void UnsubscribeFromEvents()
        {
            CardFilterHeaderControl.FilterButton.Click -= FilterButton_Click;
        }

        //CardFilterHeaderControl dropdownjaira egy selected eventet itt felülirni és tárolni a selected elemeket!
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            Cards = ((App)Application.Current).Storage.GetByFilter(x => CardFilterHeaderControl.GetSelectedRegions().Contains(x.region) &&
                                                                        CardFilterHeaderControl.GetSelectedRarities().Contains(x.rarity) &&
                                                                        CardFilterHeaderControl.GetSelectedTypes().Contains(x.type));
            SortItemSource();

            //TODO miért csak igy frissül?
            ImageList.ItemsSource = Cards;
        }

        private void SortItemSource()
        {
            //TODO
            Cards.Sort((x, y) => x.cost - y.cost);
        }
    }
}
