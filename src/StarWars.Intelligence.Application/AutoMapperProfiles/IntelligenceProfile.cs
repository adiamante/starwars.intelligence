using AutoMapper;
using StarWars.Intelligence.Application.Dtos;
using StarWars.Intelligence.Application.Dtos.Swapi;

namespace StarWars.Intelligence.Application.AutoMapperProfiles
{
    public class IntelligenceProfile : Profile
    {
        public IntelligenceProfile()
        {
            CreateMap<StarshipSwapiRead, StarshipRead>();
            CreateMap<SpeciesSwapiRead, SpeciesClassificationRead>().ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Name));
        }
    }
}
