using RuneterraCompanion.CustomModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.CustomModels.Filter
{
    public class CardFilterProperty : ICardAttribute
    {
        public string name { get; set; }
        public string cardCode { get; set; }
        public string type { get; set; }
        public int health { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public string region { get; set; }
        public string rarity { get; set; }

        public CardFilterProperty()
        {
            name = nameInitValue;
            cardCode = cardCodeInitValue;
            type = typeInitValue;
            health = healthInitValue;
            cost = costInitValue;
            attack = attackInitValue;
            region = regionInitValue;
            rarity = rarityInitValue;
        }

        //public Predicate<ICardAttribute> ToPredicate()
        //{
        //    Predicate<ICardAttribute> pred = 
        //    pred.
        //}

        private const string nameInitValue = "";
        private const string cardCodeInitValue = "";
        private const string typeInitValue = "";
        private const int costInitValue = -1;
        private const int healthInitValue = -1;
        private const int attackInitValue = -1;
        private const string regionInitValue = "";
        private const string rarityInitValue = "";
    }
}
