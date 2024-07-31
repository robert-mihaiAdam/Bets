using AutoMapper;
using DataAccess;
using Domain;
using Domain.Dto.BetableEntity;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Domain.Dto.FullBetView;
using Domain.Dto.PlacedBet;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Domain.ErrorEntities;

namespace Services.Facades
{
    public class PlacedBetFacade : IPlacedBetFacade
    {
        private readonly IPlacedBetsService _placedBetsService;
        private readonly IBetQuoteService _betQuoteService;
        private readonly IBetsService _betService;
        private readonly IBetableEntityService _betableEntityService;
        private readonly IMapper _mapper;
        private readonly DBContext _dbContext;


        public PlacedBetFacade(IPlacedBetsService placedBetsService,
                               IBetQuoteService betQuoteService,
                               IBetsService betsService,
                               IBetableEntityService betableService,
                               IMapper mapper,
                               DBContext dBContext) 
        {
            _placedBetsService = placedBetsService;
            _betQuoteService = betQuoteService;
            _betService = betsService;
            _betableEntityService = betableService;
            _mapper = mapper;
            _dbContext = dBContext;
        }

          public async Task<PlacedBetsDto> CreatePlacedBetAsync(CreatePlacedBetDto newPlacedBet)
        {
            Guid quoteId = newPlacedBet.QuoteId;
            BetQuoteDto currentQuote = await _betQuoteService.GetByIdAsync(quoteId);
            if (currentQuote == null)
                throw new NotFoundException($"Doesn't exists a bet quote for bet with id:{quoteId}");

            PlacedBetsDto newPlacedBetsDto = await _placedBetsService.CreateAsync(newPlacedBet);
            return newPlacedBetsDto;
        }

        public async Task<FullBetViewDto> GetPlacedBetByIdAsync(Guid id)
        {
            IQueryable<PlacedBets> placedBetsEntities = _placedBetsService.GetAll();
            IQueryable<BetQuotes> betQuotesEntities = _betQuoteService.GetAll();
            IQueryable<Bets> betDtoEntities = _betService.GetAll();
            IQueryable<BetableEntity> betableEntities = _betableEntityService.GetAll();

            return await (from p in placedBetsEntities
                          join bq in betQuotesEntities on p.QuoteId equals bq.Id
                          join b in betDtoEntities on bq.BetId equals b.Id
                          join beA in betableEntities on b.BetableEntityA equals beA.Id
                          join beB in betableEntities on b.BetableEntityB equals beB.Id
                          where p.Id == id
                          select new FullBetViewDto
                          {
                              PlacedBets = _mapper.Map<PlacedBetsDto>(p),
                              BetQuote = _mapper.Map<BetQuoteDto>(bq),
                              Bet = _mapper.Map<BetsDto>(b),
                              BetableEntityA = _mapper.Map<BetableEntityDto>(beA),
                              BetableEntityB = _mapper.Map<BetableEntityDto>(beB)
                          }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FullBetViewDto>> GetAllPlacedBetAsync()
        {
            IQueryable<PlacedBets> placedBetsEntities = _placedBetsService.GetAll();
            IQueryable<BetQuotes> betQuotesEntities = _betQuoteService.GetAll();
            IQueryable<Bets> betDtoEntities = _betService.GetAll();
            IQueryable<BetableEntity> betableEntities = _betableEntityService.GetAll();

            return await (from p in placedBetsEntities
                               join bq in betQuotesEntities on p.QuoteId equals bq.Id
                               join b in betDtoEntities on bq.BetId equals b.Id
                               join beA in betableEntities on b.BetableEntityA equals beA.Id
                               join beB in betableEntities on b.BetableEntityB equals beB.Id
                               select new FullBetViewDto
                               {
                                   PlacedBets = _mapper.Map<PlacedBetsDto>(p),
                                   BetQuote = _mapper.Map<BetQuoteDto>(bq),
                                   Bet = _mapper.Map<BetsDto>(b),
                                   BetableEntityA = _mapper.Map<BetableEntityDto>(beA),
                                   BetableEntityB = _mapper.Map<BetableEntityDto>(beB)
                               }).ToListAsync();
        }

        public async Task<PlacedBetsDto> UpdatePlacedBetByIdAsync(Guid id, UpdatePlacedBetDto updatePlacedBet)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            PlacedBetsDto updatedPlacedBetDto = await _placedBetsService.UpdateByIdAsync(id, updatePlacedBet);
            transaction.Commit();
            return updatedPlacedBetDto;
        }

        public async Task DeletePlacedBetByIdAsync(Guid id)
        {
            await _placedBetsService.DeletePlacedBetByIdAsync(id);
        }
    }
}
