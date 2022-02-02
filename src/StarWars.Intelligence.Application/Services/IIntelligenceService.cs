using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Intelligence.Application.Dtos;

namespace StarWars.Intelligence.Application.Services
{
    public interface IIntelligenceService
    {
        //1. Luke Skywalker starships
        Task<Result<IEnumerable<StarshipRead>>> GetLukesShips();
        //2. Species classifications from episode 1
        Task<Result<IEnumerable<SpeciesClassificationRead>>> GetSpeciesClassificationsFromEpisodeOne();
        //3. Total population of all planets in the galaxy
        Task<Result<long>> GetGalaxyPopulation();
    }
}
