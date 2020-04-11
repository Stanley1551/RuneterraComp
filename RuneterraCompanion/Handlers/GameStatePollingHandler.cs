using RuneterraCompanion.Common;
using RuneterraCompanion.Common.CustomEventArgs;
using RuneterraCompanion.Factory;
using RuneterraCompanion.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RuneterraCompanion.Handlers
{
    internal class GameStatePollingHandler
    {
        internal GameStatePollingHandler(EventHandler<GameStateTextUpdatedEventArgs> gameStateTextHandler,
            EventHandler<RemainingCardsUpdatedEventArgs> remainingCardsHandler)
        {
            usedCards = new Dictionary<int, string>();
            remainingCards = new Dictionary<string, int>();

            GameStateTextUpdated = gameStateTextHandler;
            RemainingCardsUpdated = remainingCardsHandler;
        }
        
        internal async Task StartPolling()
        {
            string gameState = string.Empty;

            var initial = await GameRequestFactory.Get(Enums.RequestType.PositionalRectangles) as PositionalRectangles;
            gameState = initial.GameState;

            if (initial.IsSuccess)
            {
                var staticResult = await GameRequestFactory.Get(Enums.RequestType.StaticDeckList) as StaticDeckList;
                //staticResult !IsSuccess?
                activeDeck = staticResult.CardsInDeck;
                remainingCards = staticResult.CardsInDeck;

                OnRemainingCardsUpdated(new RemainingCardsUpdatedEventArgs(staticResult.CardsInDeck));
                

                while (gameState == Constants.GameStates.InProgress)
                {
                    OnGameStateTextChanged(new GameStateTextUpdatedEventArgs(@"In a match against " + initial.OpponentName));

                    Thread.Sleep(Constants.GameStatePollFrequency);

                    var posResult = await GameRequestFactory.Get(Enums.RequestType.PositionalRectangles) as PositionalRectangles;
                    gameState = posResult.GameState;

                    bool changed = false;

                    if (posResult.Rectangles != null && posResult.Rectangles.Count > 0)
                    {

                        posResult.Rectangles.ForEach(x => {
                            if (!usedCards.ContainsKey(x.CardID) && x.LocalPlayer /* jesus christ.... */ && x.CardCode != "face"
                            && activeDeck.ContainsKey(x.CardCode))
                            {
                                usedCards.Add(x.CardID, x.CardCode);
                                RemoveFromRemainingCards(x.CardCode);
                                changed = true;
                            }

                        });
                    }

                    if (changed)
                    {
                        OnRemainingCardsUpdated(new RemainingCardsUpdatedEventArgs(remainingCards));
                    }

                }
                {
                    OnGameStateTextChanged(new GameStateTextUpdatedEventArgs("Match is not in progress."));
                }
            }
        }

       

        //cardID <-> cardCode
        private Dictionary<int, string> usedCards;
        //cardCode <-> quantity
        private Dictionary<string, int> activeDeck;
        private Dictionary<string, int> remainingCards;

        private void RemoveFromRemainingCards(string cardCode)
        {
            if (!remainingCards.ContainsKey(cardCode))
                return;

            if (remainingCards[cardCode] > 1)
            {
                remainingCards[cardCode]--;
            }
            else if (remainingCards[cardCode] == 1)
            {
                remainingCards.Remove(cardCode);
            }
        }

        private void OnGameStateTextChanged(GameStateTextUpdatedEventArgs e)
        {
            var handler = GameStateTextUpdated;
            handler?.Invoke(this, e);
        }

        private void OnRemainingCardsUpdated(RemainingCardsUpdatedEventArgs e)
        {
            var handler = RemainingCardsUpdated;
            handler?.Invoke(this, e);
        }

        internal event EventHandler<GameStateTextUpdatedEventArgs> GameStateTextUpdated;
        internal event EventHandler<RemainingCardsUpdatedEventArgs> RemainingCardsUpdated;

    }
}
