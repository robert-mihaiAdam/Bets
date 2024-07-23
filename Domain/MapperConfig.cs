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
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BetQuotes, BetQuotesDto>();
            CreateMap<Bets, BetsDto>();
            CreateMap<PlacedBets, PlacedBetsDto>();
        } 
    }
}
