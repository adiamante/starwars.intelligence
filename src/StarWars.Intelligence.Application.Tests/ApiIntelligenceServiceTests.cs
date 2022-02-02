using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using FluentAssertions;
using StarWars.Intelligence.Application.Dtos;
using StarWars.Intelligence.Application.Services;
using StarWars.Intelligence.Application.ServiceCollectionExtensions;

namespace StarWars.Intelligence.Application.Tests
{
    public class ApiIntelligenceServiceTests
    {
        IIntelligenceService _intelligenceService, _intelligenceServiceWrongUrl;

        [SetUp]
        public void Setup()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddIntelligence("https://swapi.dev");
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _intelligenceService = serviceProvider.GetRequiredService<IIntelligenceService>();

            ServiceCollection serviceCollection2 = new ServiceCollection();
            serviceCollection2.AddIntelligence("https://swapiwrong.wrongurl");
            var serviceProvider2 = serviceCollection2.BuildServiceProvider();
            _intelligenceServiceWrongUrl = serviceProvider2.GetRequiredService<IIntelligenceService>();
        }

        [Test]
        public async Task GetLukesShips_ReturnsTwoItems()
        {
            var lukesShipsResult = await _intelligenceService.GetLukesShips();

            lukesShipsResult.Success.Should().BeTrue();
            lukesShipsResult.Value.Should().HaveCount(2);
        }

        [Test]
        public async Task GetLukesShips_ReturnsTheCorrectShips()
        {
            var lukesShipsResult = await _intelligenceService.GetLukesShips();
            var correctShips = JsonSerializer.Deserialize<IEnumerable<StarshipRead>>(File.ReadAllText("lukesStarships.json"));

            lukesShipsResult.Success.Should().BeTrue();
            lukesShipsResult.Value.Should().BeEquivalentTo(correctShips);
        }

        [Test]
        public async Task GetLukesShips_WithWrongUrl_Fails()
        {
            var lukesShipsResult = await _intelligenceServiceWrongUrl.GetLukesShips();

            lukesShipsResult.Success.Should().BeFalse();
        }

        [Test]
        public async Task GetSpeciesClassificationsFromEpisodeOne_ReturnsFiveItems()
        {
            var speciesClassificationsResult = await _intelligenceService.GetSpeciesClassificationsFromEpisodeOne();

            speciesClassificationsResult.Success.Should().BeTrue();
            speciesClassificationsResult.Value.Should().HaveCount(5);
        }

        [Test]
        public async Task GetSpeciesClassificationsFromEpisodeOne_ReturnsTheCorrectShips()
        {
            var speciesClassificationsResult = await _intelligenceService.GetSpeciesClassificationsFromEpisodeOne();
            var correctSpeciesClassifications = JsonSerializer.Deserialize<IEnumerable<SpeciesClassificationRead>>(File.ReadAllText("episodeOneSpeciesClassifications.json"));

            speciesClassificationsResult.Success.Should().BeTrue();
            speciesClassificationsResult.Value.Should().BeEquivalentTo(correctSpeciesClassifications);
        }

        [Test]
        public async Task GetSpeciesClassificationsFromEpisodeOne_WithWrongUrl_Fails()
        {
            var speciesClassificationsResult = await _intelligenceServiceWrongUrl.GetSpeciesClassificationsFromEpisodeOne();

            speciesClassificationsResult.Success.Should().BeFalse();
        }

        [Test]
        public async Task GetGalaxyPopulation_ReturnsCorrectValue()
        {
            var galaxyPopulationResult = await _intelligenceService.GetGalaxyPopulation();

            galaxyPopulationResult.Success.Should().BeTrue();
            galaxyPopulationResult.Value.Should().Be(1711401432500);
        }

        [Test]
        public async Task GetGalaxyPopulation_WithWrongUrl_Fails()
        {
            var galaxyPopulationResult = await _intelligenceServiceWrongUrl.GetGalaxyPopulation();

            galaxyPopulationResult.Success.Should().BeFalse();
        }
    }
}