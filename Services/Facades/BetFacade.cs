using Domain.Dto.BetRequest;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using AutoMapper;
using Services.Interfaces;
using Domain.Entities;
using DataAccess;

namespace Services.Facades
{
    public class BetFacade : IBetFacade
    {
        private readonly IMapper _mapper;
        private readonly IBetsService _betService;
        private readonly IBetQuoteService _betQuoteService;
        private readonly DBContext _dbContext;

        public BetFacade(IMapper mapper, IBetsService betService, IBetQuoteService betQuote, DBContext dbContext)
        {
            _mapper = mapper;
            _betService = betService;
            _betQuoteService = betQuote;
            _dbContext = dbContext;
        }

        public async Task<BetRequestDto> CreateBetAsync(CreateBetRequestDto newFullBet)
        {
            CreateBetQuotesDto newBetQuote = _mapper.Map<CreateBetQuotesDto>(newFullBet);
            CreateBetsDto newBets = _mapper.Map<CreateBetsDto>(newFullBet);

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                BetsDto createdBet = await _betService.CreateAsync(newBets);
                BetQuoteDto createdBetQuote = await _betQuoteService.CreateAsync(newBetQuote, createdBet.Id);
                transaction.Commit();
                return new BetRequestDto { bet = createdBet, betQuote = createdBetQuote };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task<BetRequestDto> UpdateBetAsync(Guid betId, UpdateBetRequestDto updatedFullBet)
        {
            UpdateBetQuotesDto updatedBetQuote = _mapper.Map<UpdateBetQuotesDto>(updatedFullBet);
            UpdateBetsDto updatedBets = _mapper.Map<UpdateBetsDto>(updatedFullBet);

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                BetQuoteDto currentBetQuote = await _betQuoteService.GetByBetIdAsync(betId);
                BetQuoteDto newBetQuote = await _betQuoteService.UpdateById(currentBetQuote.Id, updatedBetQuote);
                BetsDto newBetDto = await _betService.UpdateById(betId, updatedBets);
                transaction.Commit();
                BetRequestDto newFullBet = new BetRequestDto { bet = newBetDto, betQuote = newBetQuote };
                return newFullBet;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }

        public async Task<IEnumerable<BetRequestDto>> GetAllBetsAsync()
        {
            IQueryable<BetQuotes> betQuotesEntities = _betQuoteService.GetAll();
            IQueryable<Bets> betDtoEntities = _betService.GetAll();
            var query = betQuotesEntities.Join(betDtoEntities,
                                                betQuote => betQuote.BetId,
                                                bet => bet.Id,
                                                (betQuote, bet) => new BetRequestDto
                                                {
                                                    bet = _mapper.Map<BetsDto>(bet),
                                                    betQuote = _mapper.Map<BetQuoteDto>(betQuote),
                                                });
            return query.ToList();
        }

        public async Task<BetRequestDto> GetBetByIdAsync(Guid betId)
        {
            BetsDto currentBet = await _betService.GetByIdAsync(betId);
            BetQuoteDto currentQuote = await _betQuoteService.GetByBetIdAsync(betId);
            BetRequestDto betQuoteEntity = new BetRequestDto { bet = currentBet, betQuote = currentQuote };
            return betQuoteEntity;
        }

        public async Task DeleteByIdAsync(Guid betId)
        {
            BetQuoteDto betQuote = await _betQuoteService.GetByBetIdAsync(betId);
            Guid betQuoteId = betQuote.Id;
            await _betService.DeleteByIdAsync(betId);
            await _betQuoteService.DeleteByIdAsync(betQuoteId);
        }
    }
}
