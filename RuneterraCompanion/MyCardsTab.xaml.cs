﻿using RuneterraCompanion.Common;
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
        private MainWindow mainWindow = null; // Reference to the MainWindow

        public MyCardsTab()
        {
            InitializeComponent();
        }

        public List<Card> Cards {
            get {
                return (List<Card>)ImageList.ItemsSource;
            }
            set {
                if(value.Count > 0)
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
            
            await Task.Run(PollGameState);
        }

        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cards.Count > 0)
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

        private void SubscribeToEvents()
        {
            CardFilterHeaderControl.FilterButton.Click += FilterButton_Click;
            CardFilterHeaderControl.SortingComboBox.SelectionChanged += SortingComboBox_SelectionChanged;
            MatchControllHeader.StartStopButton.Click += StartMatchButton_Click;
        }

        private void UnsubscribeFromEvents()
        {
            CardFilterHeaderControl.FilterButton.Click -= FilterButton_Click;
            CardFilterHeaderControl.SortingComboBox.SelectionChanged -= SortingComboBox_SelectionChanged;
            MatchControllHeader.StartStopButton.Click -= StartMatchButton_Click;
        }

        private void ManualImageListRefresh()
        {
            ImageList.Items.Refresh();
        }

        //CardFilterHeaderControl dropdownjaira egy selected eventet itt felülirni és tárolni a selected elemeket!
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            //Cards = Cards.Select(x => x.)
        }

        private async Task PollGameState()
        {
            string gameState = string.Empty;

            var initial = await GameRequestFactory.Get(Enums.RequestType.PositionalRectangles) as PositionalRectangles;
            gameState = initial.GameState;

            if (initial.IsSuccess)
            {
                var staticResult = await GameRequestFactory.Get(Enums.RequestType.StaticDeckList) as StaticDeckList;
                Dispatcher.Invoke(() => Cards = staticResult.ConvertToCardList());
                //cardID <-> cardCode
                Dictionary<int,string> usedCards = new Dictionary<int,string>();

                while(gameState == Constants.GameStates.InProgress)
                {
                    UpdateHeaderText(@"In a match against " + initial.OpponentName);

                    Thread.Sleep(Constants.GameStatePollFrequency);

                    var posResult = await GameRequestFactory.Get(Enums.RequestType.PositionalRectangles) as PositionalRectangles;
                    gameState = posResult.GameState;

                    if(posResult.Rectangles != null && posResult.Rectangles.Count > 0)
                    {
                        posResult.Rectangles.ForEach(x => {
                            if (!usedCards.ContainsKey(x.CardID) && x.LocalPlayer) usedCards.Add(x.CardID, x.CardCode);
                        });

                        await Dispatcher.InvokeAsync(() =>
                        {
                            var usedCardCodes = usedCards.Values;
                            //usedCardCodes.
                            //Cards.Remove

                            ManualImageListRefresh();
                        });
                    }

                }
                {
                    UpdateHeaderText("Match is not in progress.");
                }
            }

            
        }

        private void UpdateHeaderText(string newValue)
        {
            Dispatcher.Invoke(() =>
            {
                MatchControllHeader.MatchStateText.Content = newValue;
            });
        }
    }
}
