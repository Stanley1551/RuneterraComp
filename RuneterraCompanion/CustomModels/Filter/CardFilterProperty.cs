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
            name = string.Empty;
            cardCode = string.Empty;
            type = string.Empty;
            health = -1;
            cost = -1;
            attack = -1;
            region = string.Empty;
            rarity = string.Empty;
        }
    }
}
