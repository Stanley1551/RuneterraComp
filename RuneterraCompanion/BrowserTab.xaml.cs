﻿using RuneterraCompanion.Common;
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

        public List<Card> Cards;

        public BrowserTab()
        {
            InitializeComponent();
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

            //InitImages();
        }

        private void InitImages()
        {
            Cards = ((App)Application.Current).Storage.GetAll();

            ImageList.ItemsSource = Cards;
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
    }
}
