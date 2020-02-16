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
        private List<string> regions;
        private List<string> types = new List<string> { "Unit", "Spell" }; //jobb esetben :)
        private List<string> rarities;

        public CardDataStorage()
        {
            cards = new List<T>();
            IsInitialized = false;
            regions = new List<string>();
            rarities = new List<string>();
        }

        public bool IsInitialized { get; private set; }

        public List<string> Regions { get {
                if(regions.Count != 0)
                {
                    return regions;
                }
                else
                {
                    InitializeRegions();
                    return regions;
                }
            } }

        public List<string> Types { get {
                return types;
            } }

        public List<string> Rarities { get {
                if(rarities.Count != 0)
                {
                    return rarities;
                }
                else
                {
                    InitializeRarities();
                    return rarities;
                }
            } }

        //TODO: add cost property

        //ide lehet kéne majd egy komplex osztály ami több infót ad arról ha nem sikerül valami!
        public bool TryInitializeAsync()
        {
            //ez is csúnya
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
            else
            {
                TryInitializeAsync();
                return cards;
            };
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

        public List<T> GetByFilter(Predicate<T> match)
        {
            return cards.FindAll(match);
        }

        private void InitializeRegions()
        {
            foreach(var card in cards)
            {
                if(!regions.Contains(card.region))
                {
                    regions.Add(card.region);
                }
            }
        }

        private void InitializeRarities()
        {
            foreach (var card in cards)
            {
                if (!rarities.Contains(card.rarity))
                {
                    rarities.Add(card.rarity);
                }
            }
        }
    }
}
