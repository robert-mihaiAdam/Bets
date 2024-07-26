using Services.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Dto.BetQuote;
using AutoMapper;
using System.Collections.Generic;

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

        public IQueryable<BetQuoteDto> GetAllAsync()
        {
            IEnumerable<BetQuoteDto> entities = _mapper.Map<IEnumerable<BetQuoteDto>>(_dbContext.BetQuotes);
            return entities.AsQueryable();
        }

        public async Task<BetQuoteDto> GetByIdAsync(Guid id)
        {
            BetQuotes entity = await _dbContext.BetQuotes.FindAsync(id);
            BetQuoteDto entityDto = _mapper.Map<BetQuoteDto>(entity);
            return entityDto;
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

        public async Task<bool> DeleteByIdAsync(Guid id)
        {          
            BetQuotes betQuote = await _dbContext.BetQuotes.FindAsync(id);
            if (betQuote == null)
            {
                return false;
            }

            _dbContext.BetQuotes.Remove(betQuote);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
