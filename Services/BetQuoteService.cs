using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using AutoMapper;
using Domain.Dto.BetRequest;
using Domain.Dto.BetableEntity;

namespace Services
{
    public sealed class BetQuoteService : IBetQuoteService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBetsService _betsService;

        public BetQuoteService(DBContext dBContext, IMapper mapper, IBetsService betsService)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _betsService = betsService;
        }

        public async Task<BetQuoteDto> CreateAsync(CreateBetQuotesDto entity, Guid betId)
        {
            BetQuotes newBetQuote = _mapper.Map<BetQuotes>(entity);
            newBetQuote.BetId = betId;
            _dbContext.BetQuotes.Add(newBetQuote);
            await _dbContext.SaveChangesAsync();
            BetQuoteDto newBetQuoteDto = _mapper.Map<BetQuoteDto>(newBetQuote);
            return newBetQuoteDto;
        }

        public async Task<IEnumerable<BetQuoteDto>> GetAllAsync()
        {
            IEnumerable<BetQuotes> entities = await _dbContext.BetQuotes.ToListAsync();
            IEnumerable<BetQuoteDto> entitiesDto = _mapper.Map<IEnumerable<BetQuoteDto>>(entities);
            return entitiesDto;
        }

        public async Task<BetQuoteDto> GetByIdAsync(Guid id)
        {
            BetQuotes entity = await _dbContext.BetQuotes.FindAsync(id);
            BetQuoteDto entityDto = _mapper.Map<BetQuoteDto>(entity);
            return entityDto;
        }

        public async Task<IEnumerable<BetRequestDto>> GetAllFullBetsAsync()
        {
            var query = from bq in _dbContext.Set<BetQuotes>()
                        join b in _dbContext.Set<Bets>()
                            on bq.BetId equals b.Id
                        select new QueryBetRequestDto { bet = b, betQuote = bq };

            IEnumerable<QueryBetRequestDto> queryEntities = await query.ToArrayAsync();
            IEnumerable<BetRequestDto> betEntitiesDto = _mapper.Map<IEnumerable<BetRequestDto>>(queryEntities);
            return betEntitiesDto;
        }

        public async Task<BetQuoteDto> UpdateById(Guid id, UpdateBetQuotesDto newEntity)
        {
            BetQuotes currentEntity = await _dbContext.BetQuotes.FindAsync(id);
            if (currentEntity == null)
            {
                return null;
            }

            _mapper.Map(newEntity, currentEntity);
            await _dbContext.SaveChangesAsync();
            BetQuoteDto entityDto = _mapper.Map<BetQuoteDto>(currentEntity);
            return entityDto;
        }

        public async Task<bool> DeleteFullBetAsync(Guid id)
        {          
            BetQuotes betQuote = await _dbContext.BetQuotes.FindAsync(id);
            if (betQuote == null)
            {
                return false;
            }

            await _betsService.DeleteAsync(betQuote.BetId);
            _dbContext.BetQuotes.Remove(betQuote);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
