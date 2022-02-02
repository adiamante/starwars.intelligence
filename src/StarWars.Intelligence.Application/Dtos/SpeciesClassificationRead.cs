using System.Text.Json.Serialization;

namespace StarWars.Intelligence.Application.Dtos
{
    public class SpeciesClassificationRead
    {
        [JsonPropertyName("species")]
        public string Species { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }
    }
}
