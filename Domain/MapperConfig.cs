using AutoMapper;
using Domain.Dto;
using Domain.Dto.BetableEntity;
using Domain.Entities;

namespace Domain
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PlaceBetableEntityDto, BetableEntity>();
            CreateMap<BetableEntity, BetableEntityDto>();
            CreateMap<UpdateBetableEntityDto, BetableEntity>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<BetQuotes, BetQuotesDto>();
            CreateMap<Bets, BetsDto>();
            CreateMap<PlacedBets, PlacedBetsDto>();
        } 
    }
}
