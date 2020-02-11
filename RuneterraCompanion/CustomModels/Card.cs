using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.CustomModels
{
    public class Card : CardImage, ICardAttribute
    {
        public List<object> associatedCards { get; set; }
        public List<object> associatedCardRefs { get; set; }
        public List<Asset> assets { get; set; }
        public string region { get; set; }
        public string regionRef { get; set; }
        public int attack { get; set; }
        public int cost { get; set; }
        public int health { get; set; }
        public string description { get; set; }
        public string descriptionRaw { get; set; }
        public string levelupDescription { get; set; }
        public string levelupDescriptionRaw { get; set; }
        public string flavorText { get; set; }
        public string artistName { get; set; }
        public string name { get; set; }
        public string cardCode { get; set; }
        public List<object> keywords { get; set; }
        public List<object> keywordRefs { get; set; }
        public string spellSpeed { get; set; }
        public string spellSpeedRef { get; set; }
        public string rarity { get; set; }
        public string rarityRef { get; set; }
        public string subtype { get; set; }
        public string supertype { get; set; }
        public string type { get; set; }
        public bool collectible { get; set; }


    }
}
