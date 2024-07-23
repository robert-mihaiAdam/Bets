using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Domain
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<BetableEntity, BetableEntityDto>();
            CreateMap<BetQuotes, BetQuotesDto>();
            CreateMap<Bets, BetsDto>();
            CreateMap<PlacedBets, PlacedBetsDto>();
        } 
    }
}
