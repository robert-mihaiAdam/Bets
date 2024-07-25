﻿using Domain.Dto.BetRequest;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using AutoMapper;
using Services.Interfaces;

namespace Services.Facades
{
    public class BetFacade : IBetFacade
    {
        private readonly IMapper _mapper;
        private readonly IBetsService _betService;
        private readonly IBetQuoteService _betQuoteService;

        public BetFacade(IMapper mapper, IBetsService betService, IBetQuoteService betQuote)
        {
            _mapper = mapper;
            _betService = betService;
            _betQuoteService = betQuote;
        }

        public async Task<BetRequestDto> CreateBetAsync(CreateBetRequestDto newFullBet)
        {
            CreateBetQuotesDto newBetQuote = _mapper.Map<CreateBetQuotesDto>(newFullBet);
            CreateBetsDto newBets = _mapper.Map<CreateBetsDto>(newFullBet);
            BetsDto createdBet = await _betService.CreateAsync(newBets);
            if (createdBet == null)
            {
                return null;
            }

            BetQuoteDto createdBetQuote = await _betQuoteService.CreateAsync(newBetQuote, createdBet.Id);
            if (createdBetQuote == null)
            {
                return null;
            }

            return new BetRequestDto { bet = createdBet, betQuote = createdBetQuote };
        }

        public async Task<BetRequestDto> UpdateFullBetAsync(Guid id, UpdateBetRequestDto updatedFullBet)
        {
            UpdateBetQuotesDto updatedBetQuote = _mapper.Map<UpdateBetQuotesDto>(updatedFullBet);
            UpdateBetsDto updatedBets = _mapper.Map<UpdateBetsDto>(updatedFullBet);
            BetQuoteDto newBetQuote = await _betQuoteService.UpdateById(id, updatedBetQuote);
            if (newBetQuote == null)
            {
                return null;
            }

            BetsDto newBetDto = await _betService.UpdateById(newBetQuote.BetId, updatedBets);
            if (newBetDto == null)
            {
                return null;
            }

            BetRequestDto newFullBet = new BetRequestDto { bet = newBetDto, betQuote = newBetQuote };
            return newFullBet;
        }
        
        public async Task<IEnumerable<BetRequestDto>> GetAllBetsAsync()
        {
            IEnumerable<QueryBetRequestDto> queryEntities = await _betQuoteService.GetAllFullBetsAsync();
            IEnumerable<BetRequestDto> betEntitiesDto = _mapper.Map<IEnumerable<BetRequestDto>>(queryEntities);
            return betEntitiesDto;
        }

        public async Task<BetRequestDto> GetBetByIdAsync(Guid betQuoteId)
        {
            BetQuoteDto currentQuote = await _betQuoteService.GetByIdAsync(betQuoteId);
            if (currentQuote == null)
            {
                return null;
            }

            if (currentQuote.BetId == null)
            {
                return null;
            }

            BetsDto currentBet = await _betService.GetByIdAsync(currentQuote.BetId);
            BetRequestDto betQuoteEntity = new BetRequestDto { bet = currentBet, betQuote = currentQuote };
            return betQuoteEntity;
        }
    }
}
