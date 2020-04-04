using RuneterraCompanion.CustomModels;
using RuneterraCompanion.CustomModels.Interfaces;
using RuneterraCompanion.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RuneterraCompanion.ResponseModels
{
    public static class GameResponseExtensionMethods
    {
        public static List<Card> ConvertToCardList(this StaticDeckList list)
        {
            ListResult.Clear();

            if (!list.IsSuccess)
            {
                return ListResult;
            }
            else
            {
                return GetCardList(list.CardsInDeck);
            }
        }

        public static List<Card> ConvertToCardList(this Dictionary<string,int> dict)
        {
            return GetCardList(dict);
        }

        private static List<Card> GetCardList(Dictionary<string,int> dict)
        {
            ListResult.Clear();

            var enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            if (dict.Count > 0)
            {
                do
                {
                    do
                    {
                        ListResult.Add(Storage.GetByCode(enumerator.Current.Key));
                    }
                    while (enumerator.Current.Value > ListResult.FindAll(x => x.cardCode == enumerator.Current.Key).Count);
                }
                while (enumerator.MoveNext());
            }
            return ListResult;
        }

        private static CardDataStorage<Card> Storage => ((App)Application.Current).Storage;
        private static List<Card> ListResult = new List<Card>();
    }
}
