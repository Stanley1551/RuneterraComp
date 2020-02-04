using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RuneterraCompanion.ResponseModels
{
    public class StaticDeckList
    {
        [JsonProperty("PlayerName")]
        public string PlayerName { get; set; }
        [JsonProperty("OpponentName")]
        public string OpponentName { get; set; }
        [JsonProperty("GameState")]
        public string GameState { get; set; }
        [JsonProperty("Screen")]
        public Screen Screen { get; set; }
        [JsonProperty("Rectangles")]
        public List<Rectangle> Rectangles { get; set; }
    }

    [JsonObject("Screen")]
    public class Screen
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
    }

    [JsonObject("Rectangle")]
    public class Rectangle
    {
        public int CardID { get; set; }
        public string CardCode { get; set; }
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool LocalPlayer { get; set; }
    }

}
