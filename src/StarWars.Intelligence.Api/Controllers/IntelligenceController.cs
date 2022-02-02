using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StarWars.Intelligence.Application.Services;

namespace StarWars.Intelligence.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntelligenceController : ControllerBase
    {
        IIntelligenceService _intelligenceService;
        public IntelligenceController(IIntelligenceService intelligenceService)
        {
            _intelligenceService = intelligenceService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLukesShips()
        {
            var result = await _intelligenceService.GetLukesShips();

            if (result.Success)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSpeciesClassificationsFromEpisodeOne()
        {
            var result = await _intelligenceService.GetSpeciesClassificationsFromEpisodeOne();

            if (result.Success)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGalaxyPopulation()
        {
            var result = await _intelligenceService.GetGalaxyPopulation();

            if (result.Success)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.Error);
            }
        }
    }
}
