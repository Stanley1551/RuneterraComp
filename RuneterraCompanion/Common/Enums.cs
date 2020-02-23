using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Common
{
    public static class Enums
    {
        public enum RequestType
        {
            StaticDeckList = 1,
            PositionalRectangles = 2
        };

        public enum Regions
        {
            Demacia = 1,
            Freljord = 2,
            Ionia = 3,
            Noxus = 4,
            Piltover = 5,
            Isles = 6,
            All = 7
        };

        public enum CardType
        {
            Spell = 1,
            Unit = 2
        };

        public enum Rarity
        {
            Common = 1,
            Rare = 2,
            Epic = 3,
            Champion = 4
        };
    }
}
