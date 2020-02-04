using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RuneterraCompanion.ResponseModels
{
    public class PositionalRectangles
    {
        [JsonProperty("DeckCode")]
        public string DeckCode { get; set; }
        [JsonProperty("CardsInDeck")]
        public Dictionary<string, int> CardsInDeck { get; set; }
    }
}
