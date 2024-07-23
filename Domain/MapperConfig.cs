using AutoMapper;
using Domain.Dto;
<<<<<<< HEAD
using Domain.Dto.BetableEntity;
=======
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
>>>>>>> a42b9c1 (Feature: Implement Create and read routes for BetQuote)
using Domain.Entities;

namespace Domain
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PlaceBetableEntityDto, BetableEntity>();
            CreateMap<BetableEntity, BetableEntityDto>();
<<<<<<< HEAD
            CreateMap<UpdateBetableEntityDto, BetableEntity>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<BetQuotes, BetQuotesDto>();
            CreateMap<Bets, BetsDto>();
=======

            CreateMap<CreateBetQuotesDto, BetQuotes>();
            CreateMap<BetQuotes, GetBetQuoteDto>();

            CreateMap<CreateBetsDto, Bets>();
            CreateMap<Bets, GetBetsDto>();

>>>>>>> a42b9c1 (Feature: Implement Create and read routes for BetQuote)
            CreateMap<PlacedBets, PlacedBetsDto>();
        } 
    }
}
