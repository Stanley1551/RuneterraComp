using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.CustomModels.Interfaces
{
    public interface ICardAttribute
    {
        public string name { get; set; }
        public string cardCode { get; set; }
        public string type { get; set; }
        public int health { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public string region { get; set; }
        public string rarity { get; set; }
    }
}
