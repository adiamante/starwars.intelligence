using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StarWars.Intelligence.Application.Dtos.Swapi
{
    public class FilmSwapiRead : ISwapiRead
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("episode_id")]
        public int EpisodeId { get; set; }

        [JsonPropertyName("opening_crawl")]
        public string OpeningCrawl { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("producer")]
        public string Producer { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("characters")]
        public List<string> Characters { get; set; }

        [JsonPropertyName("planets")]
        public List<string> Planets { get; set; }

        [JsonPropertyName("starships")]
        public List<string> Starships { get; set; }

        [JsonPropertyName("vehicles")]
        public List<string> Vehicles { get; set; }

        [JsonPropertyName("species")]
        public List<string> Species { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
