﻿using AutoMapper;
using Domain.Dto;
using Domain.Dto.BetableEntity;
using Domain.Dto.BetQuote;
using Domain.Dto.BetRequest;
using Domain.Dto.Bets;
using Domain.Entities;

namespace Domain
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CreateBetableEntityDto, BetableEntity>();
            CreateMap<BetableEntity, BetableEntityDto>();
            CreateMap<UpdateBetableEntityDto, BetableEntity>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateBetQuotesDto, BetQuotes>();
            CreateMap<BetQuotes, BetQuoteDto>();
            CreateMap<UpdateBetQuotesDto, BetQuotes>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.BetId, opt => opt.Ignore());

            CreateMap<CreateBetsDto, Bets>();
            CreateMap<Bets, BetsDto>();
            CreateMap<UpdateBetsDto, CreateBetsDto>();
            CreateMap<UpdateBetsDto, Bets>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<QueryBetRequestDto, BetRequestDto>();

            CreateMap<PlacedBets, PlacedBetsDto>();
        } 
    }
}
