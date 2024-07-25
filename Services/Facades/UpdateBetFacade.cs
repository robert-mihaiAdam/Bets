using Domain.Dto.BetRequest;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using AutoMapper;
using Services.Interfaces;

namespace Services.Facades
{
    public class UpdateBetFacade
    {
        private readonly IMapper _mapper;
        private readonly IBetsService _betService;
        private readonly IBetQuoteService _betQuoteService;
        private UpdateBetQuotesDto updatedBetQuote;
        private UpdateBetsDto updatedBets;

        public UpdateBetFacade(IMapper mapper, IBetsService betService, IBetQuoteService betQuote, UpdateBetRequestDto newFullBet)
        {
            _mapper = mapper;
            _betService = betService;
            _betQuoteService = betQuote;
            updatedBetQuote = _mapper.Map<UpdateBetQuotesDto>(newFullBet);
            updatedBets = _mapper.Map<UpdateBetsDto>(newFullBet);
        }

        public async Task<BetRequestDto> UpdateFullBet(Guid id)
        {
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
    }
}
