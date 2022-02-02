# StarWars Intelligence Api

Api built for gathering information in order to defeat the galactic empire.

## Setup

Clone project
* `git clone https://github.com/adiamante/starwars.intelligence.git`

## How to run

### Visual Studio

* Open src/StarWars.Intelligence.sln
* Set StarWars.Intelligence.Api as startup project
* Run (F5) and a browser should open to swagger UI

### Command Line (src folder as context)

* `dotnet build src/StarWars.Intelligence.sln`
* `dotnet run src/StarWars.Intelligence.Api/StarWars.Intelligence.Api.csproj`
* `use browser and visit https://localhost:5001/swagger/index.html`

## How to Test

### Visual Studio

* Run all tests (Ctrl + R, A)
* Alternatively open Test Explorer and run using buttons

### Command Line (src folder as context)

* `dotnet test`

## Projects

### StarWars.Intelligence.Api
* /Intelligence/GetLukesShips 
    * returns starships related to Luke Skywalker
* /Intelligence/GetSpeciesClassificationsFromEpisodeOne
    * returns species classifications from the first Star Wars movie
* /Intelligence/GetGalaxyPopulation
    * returns the total population of the galaxy

### StarWars.Intelligence.Applications
* AutoMapper Profile for data transfer objects
* Data Transfer Objects
* ServiceCollection Extensions for adding IntelligenceService
* IntelligenceService related classes

### StarWars.Intelligence.Applications.Tests
* Tests for ApiIntelligenceService
* Files for testing