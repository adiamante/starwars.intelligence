using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using StarWars.Intelligence.Application.Dtos;
using StarWars.Intelligence.Application.Dtos.Swapi;

namespace StarWars.Intelligence.Application.Services
{
    public class ApiIntelligenceService : IIntelligenceService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public ApiIntelligenceService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        //1. Luke Skywalker starships, people call id 1 (can optionally search by name) => starships urls 
        public async Task<Result<IEnumerable<StarshipRead>>> GetLukesShips()
        {
            try
            {
                HttpClient swapiClient = _httpClientFactory.CreateClient("swapiClient");
                var response = await swapiClient.GetAsync("/api/people/1/");

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Fail<IEnumerable<StarshipRead>>(response.ReasonPhrase);
                }

                var luke = JsonSerializer.Deserialize<PeopleSwapiRead>(response.Content.ReadAsStringAsync().Result);
                List<StarshipRead> starShips = new List<StarshipRead>();

                foreach (var starshipLink in luke.Starships)
                {
                    //full url link might not work so might want to use another httpClient
                    var starshipResponse = await swapiClient.GetAsync(starshipLink);
                    //can opt to check response status here and handle accordingly
                    var swapiStarShip = JsonSerializer.Deserialize<StarshipSwapiRead>(starshipResponse.Content.ReadAsStringAsync().Result);
                    starShips.Add(_mapper.Map<StarshipRead>(swapiStarShip));
                }

                return Result.Ok<IEnumerable<StarshipRead>>(starShips);
            }
            catch (Exception ex)
            {
                return Result.Fail<IEnumerable<StarshipRead>>(ex);
            }
        }

        //2. Species classifications from episode 1, films call id 1 (can optionally search by title) => species urls
        public async Task<Result<IEnumerable<SpeciesClassificationRead>>> GetSpeciesClassificationsFromEpisodeOne()
        {
            try
            {
                HttpClient swapiClient = _httpClientFactory.CreateClient("swapiClient");
                var response = await swapiClient.GetAsync("/api/films/1/");

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Fail<IEnumerable<SpeciesClassificationRead>>(response.ReasonPhrase);
                }

                var episode1 = JsonSerializer.Deserialize<FilmSwapiRead>(response.Content.ReadAsStringAsync().Result);
                List<SpeciesClassificationRead> speciesClassifications = new List<SpeciesClassificationRead>();

                foreach (var speciesLink in episode1.Species)
                {
                    //full url link might not work so might want to use another httpClient
                    var speciesResponse = await swapiClient.GetAsync(speciesLink);
                    //can opt to check response status here and handle accordingly
                    var species = JsonSerializer.Deserialize<SpeciesSwapiRead>(speciesResponse.Content.ReadAsStringAsync().Result);
                    speciesClassifications.Add(_mapper.Map<SpeciesClassificationRead>(species));
                }

                return Result.Ok<IEnumerable<SpeciesClassificationRead>>(speciesClassifications);
            }
            catch (Exception ex)
            {
                return Result.Fail<IEnumerable<SpeciesClassificationRead>>(ex);
            }
        }

        //3. Total population of all planets in the galaxy, planets call => aggregate population
        public async Task<Result<long>> GetGalaxyPopulation()
        {
            try
            {
                HttpClient swapiClient = _httpClientFactory.CreateClient("swapiClient");
                
                SwapiReadCollection<PlanetSwapiRead> planets = new SwapiReadCollection<PlanetSwapiRead>() { Next = "/api/planets" } ;
                long totalPopulation = 0;
                bool atLeastOneSuccess = false;
                string failMessage = "";

                //iterate through all planets to get total population of the galaxy
                while (!String.IsNullOrEmpty(planets.Next))
                {
                    var response = await swapiClient.GetAsync(planets.Next);

                    if (response.IsSuccessStatusCode)
                    {
                        atLeastOneSuccess = true;
                        planets = JsonSerializer.Deserialize<SwapiReadCollection<PlanetSwapiRead>>(response.Content.ReadAsStringAsync().Result);
                        foreach (var planet in planets.Results)
                        {
                            if (long.TryParse(planet.Population, out long population))
                            {
                                totalPopulation += population;
                            }
                        }
                    }
                    else
                    {
                        failMessage = response.ReasonPhrase;
                    }
                }

                if (atLeastOneSuccess)
                {
                    return Result.Ok<long>(totalPopulation);
                }
                else
                {
                    return Result.Fail<long>(failMessage);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<long>(ex);
            }
        }
    }
}
