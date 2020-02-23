using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RuneterraCompanion.ResponseModels.Interfaces;

namespace RuneterraCompanion.ResponseModels
{
    public class StaticDeckList : IGameResponse
    {
        [JsonProperty("DeckCode")]
        public string DeckCode { get; set; }
        [JsonProperty("CardsInDeck")]
        public Dictionary<string, int> CardsInDeck { get; set; }
        [JsonIgnore]
        public bool IsSuccess { get; set; }
    }
}
