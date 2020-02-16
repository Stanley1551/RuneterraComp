using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.CustomModels.Filter
{
    public static class FilterExtensionMethods
    {
        public static void SetName(this CardFilterProperty cardFilter, string name)
        {
            cardFilter.name = name;
        }

        public static void SetAttack(this CardFilterProperty cardFilter, int attack)
        {
            cardFilter.attack = attack;
        }

        public static void SetCost(this CardFilterProperty cardFilter, int cost)
        {
            cardFilter.cost = cost;
        }

        public static void SetRegion(this CardFilterProperty cardFilter, string region)
        {
            cardFilter.region = region;
        }

        public static void SetType(this CardFilterProperty cardFilter, string type)
        {
            cardFilter.type = type;
        }

        public static void SetRarity(this CardFilterProperty cardFilter, string rarity)
        {
            cardFilter.rarity = rarity;
        }

        //TODO finish
    }
}
