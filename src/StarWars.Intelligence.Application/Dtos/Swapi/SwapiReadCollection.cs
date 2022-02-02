using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StarWars.Intelligence.Application.Dtos.Swapi
{
    public class SwapiReadCollection<TResult> where TResult : ISwapiRead
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("results")]
        public List<TResult> Results { get; set; }
    }
}
