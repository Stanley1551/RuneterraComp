using RuneterraCompanion.Common;
using RuneterraCompanion.CustomModels;
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
    public partial class BrowserTab : UserControl
    {
        private MainWindow mainWindow = null; // Reference to the MainWindow

        private string[] InitialElementNames = { "CheckCardsButton" };
        private string[] BrowserelementNames = { "ImageList" };

        public List<Card> Cards {
            get {
                return (List<Card>)ImageList.ItemsSource;
            }
            set {
                if (value.Count > 0)
                {
                    ImageList.ItemsSource = value;
                    CardFilterHeaderControl.SortingComboBox.IsEnabled = true;
                }
                else
                {
                    ImageList.Items.Clear();
                }
                ImageList.Items.Refresh();
            }
        }

        public BrowserTab()
        {
            InitializeComponent();
        }

        private void ManualImageListRefresh()
        {
            ImageList.Items.Refresh();
        }

        private void SubscribeToEvents()
        {
            CardFilterHeaderControl.FilterButton.Click += FilterButton_Click;
            CardFilterHeaderControl.SortingComboBox.SelectionChanged += SortingComboBox_SelectionChanged;
        }

        private void UnsubscribeFromEvents()
        {
            CardFilterHeaderControl.FilterButton.Click -= FilterButton_Click;
            CardFilterHeaderControl.SortingComboBox.SelectionChanged -= SortingComboBox_SelectionChanged;
        }

        //CardFilterHeaderControl dropdownjaira egy selected eventet itt felülirni és tárolni a selected elemeket!
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            Cards = ((App)Application.Current).Storage.GetByFilter(x => CardFilterHeaderControl.GetSelectedRegions().Contains(x.region) &&
                                                                        CardFilterHeaderControl.GetSelectedRarities().Contains(x.rarity) &&
                                                                        CardFilterHeaderControl.GetSelectedTypes().Contains(x.type));
        }

        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Cards.Count > 0)
            {
                switch (e.AddedItems[0].ToString())
                {
                    case "Health":
                        Cards.Sort((x, y) => x.health - y.health);
                        break;
                    case "Cost":
                        Cards.Sort((x, y) => x.cost - y.cost);
                        break;
                    case "Attack":
                        Cards.Sort((x, y) => x.attack - y.attack);
                        break;
                }
                ManualImageListRefresh();
            }
        }

        private void CheckCardsButton_Click(object sender, RoutedEventArgs e)
        {
            //bevezetni valahova és megnézni a kártyákat is
            if(Directory.Exists(Constants.assetsDirectoryName))
            {
                HideInitialElements();
                ShowBrowserElements();
            }
        }

        private void HideInitialElements()
        {
            foreach(var elementName in InitialElementNames)
            {
                var element = this.FindName(elementName) as FrameworkElement;
                if(element != null)
                {
                    element.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ShowBrowserElements()
        {
            foreach (var elementName in BrowserelementNames)
            {
                var element = this.FindName(elementName) as FrameworkElement;
                if (element != null)
                {
                    element.Visibility = Visibility.Visible;
                }
            }
        }

        private void CardScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            //quick workaround
            if (mainWindow == null)
            {
                mainWindow = (MainWindow)Application.Current.MainWindow;
            }

            //bevezetni valahova és megnézni a kártyákat is
            if (Directory.Exists(Constants.assetsDirectoryName))
            {
                HideInitialElements();
                ShowBrowserElements();
            }

            SubscribeToEvents();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeFromEvents();
        }

        //paraméterek hiányában így nem jó...
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
