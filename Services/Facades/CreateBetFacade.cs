using Domain.Dto.BetRequest;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using AutoMapper;
using Services.Interfaces;

namespace Services.Facades
{
    public class CreateBetFacade
    {
        private readonly IMapper _mapper;
        private readonly IBetsService _betsService;
        private readonly IBetQuoteService _betQuoteService;
        private CreateBetQuotesDto newBetQuote;
        private CreateBetsDto newBets;

        public CreateBetFacade(IMapper mapper, IBetsService betsService, IBetQuoteService betQuote, CreateBetRequestDto newFullBet)
        {
            _mapper = mapper;
            _betsService = betsService;
            _betQuoteService = betQuote;
            newBetQuote = _mapper.Map<CreateBetQuotesDto>(newFullBet);
            newBets = _mapper.Map<CreateBetsDto>(newFullBet);
        }

        public async Task<BetRequestDto> CreateBet()
        {
            BetsDto createdBet = await _betsService.CreateAsync(newBets);
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
    }
}
