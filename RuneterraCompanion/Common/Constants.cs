using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Common
{
    internal static class Constants
    {
        internal const string Protocol = @"http://";
        internal const string Host = @"localhost";
        internal const string PathToStaticDeckList = @"/static-decklist";
        internal const string PathToPositionalRectangles = @"/positional-rectangles";
        internal const string assetsUrl = @"https://dd.b.pvp.net/datadragon-set1-lite-en_us.zip";
        internal const string assetsFile = @"datadragon-set1-lite-en_us.zip";
        internal const string assetsDirectoryName = @"Assets";
        internal const string cardImgPath = @"Assets\en_us\img\cards";
        internal const string cardThumbnailPath = @"Assets\en_us\img\thumbnails";
        internal const string DataJsonPath = @"Assets\en_us\data\set1-en_us.json";

        internal const int GameStatePollFrequency = 1000;

        internal static class GameStates
        {
            internal const string InProgress = @"InProgress";
        }
    }
}
