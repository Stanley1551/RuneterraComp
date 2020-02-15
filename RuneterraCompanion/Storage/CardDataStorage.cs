using Newtonsoft.Json;
using RuneterraCompanion.CustomModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using RuneterraCompanion.Common;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RuneterraCompanion.CustomModels.Interfaces;
using RuneterraCompanion.CustomModels.Filter;
using RuneterraCompanion.Factory;

namespace RuneterraCompanion.Storage
{
    public class CardDataStorage<T> where T : ICardAttribute
    {
        private List<T> cards;
        private string GetFullPathDataJson => Path.Combine(Directory.GetCurrentDirectory(), Constants.DataJsonPath);

        public CardDataStorage()
        {
            cards = new List<T>();
            IsInitialized = false;
        }

        public bool IsInitialized { get; private set; }

        //ide lehet kéne majd egy komplex osztály ami több infót ad arról ha nem sikerül valami!
        public bool TryInitializeAsync()
        {
            try
            {
                List<T> tempAttributes = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(
                    GetFullPathDataJson));

                List<Task> tasks = new List<Task>();
                foreach (var temp in tempAttributes)
                {
                    cards.Add(temp);
                }

                IsInitialized = true;
                return true;

            }
            catch(Exception e) { return false; }
        }

        public List<T> GetAll()
        {
            //több helyre bevezetni!
            if (IsInitialized)
            {
                return cards;
            }
            else return new List<T>();
        }

        public T GetByCode(string cardCode)
        {
            return cards.Find(x => x.cardCode == cardCode);
        }

        public async Task<T> GetByCodeAsync(string cardCode)
        {
            return await Task.Run(() =>
            {
                return GetByCode(cardCode);
            });
        }

        //SZAR!
        //public List<T> GetByFilter(CardFilterProperty filter)
        //{
        //    //return cards.FindAll(x => 
        //    //    (filter.attack != -1 ? x.attack == filter.attack : x.attack >= 0) &&
        //    //    filter.cardCode != string.Empty ? x.cardCode == filter.cardCode : x.cardCode.Length > 0 &&
        //    //    filter.cost != -1 ? x.cost == filter.cost : x.cost >= 0 &&
        //    //    filter.health != -1 ? x.health == filter.health : x.health >= 0 &&
        //    //    filter.name != string.Empty ? x.name == filter.name : x.name.Length >= 0 &&
        //    //    filter.rarity != string.Empty ? x.rarity == filter.rarity : x.rarity.Length >= 0 &&
        //    //    filter.region != string.Empty ? x.region == filter.region : x.region.Length >= 0 &&
        //    //    filter.type != string.Empty ? x.type == filter.type : x.type.Length >= 0
        //    //    );
        //    return cards.FindAll(x => x.attack == 9);
        //}

        public List<T> GetByFilter(Predicate<T> match)
        {
            return cards.FindAll(match);
        }
    }
}
