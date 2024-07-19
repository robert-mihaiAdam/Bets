using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;

namespace Services
{
    public sealed class PlacedBetsService : IPlacedBetsService
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;
        private readonly IBetQuoteService _betQuoteService;

        public PlacedBetsService(DBContext dbContext, TimeProvider timeProvider, IBetQuoteService betQuoteService)
        {
            _dbContext = dbContext;
            _timeProvider = timeProvider;
            _betQuoteService = betQuoteService;
        }

        public async Task<PlacedBets> CreateAsync(PlacedBets entity)
        {
            entity.PlacedDate = _timeProvider.GetUtcNow().DateTime;
            Guid quoteId = entity.QuoteId;
            BetQuotes currentQuote = await _betQuoteService.GetByIdAsync(quoteId);
            if (currentQuote == null)
                return null;
            _dbContext.PlacedBets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<PlacedBets>> GetAllAsync()
        {
            return await _dbContext.PlacedBets.ToListAsync();
        }

        public async Task<PlacedBets?> GetByIdAsync(Guid id)
        {
            return await _dbContext.PlacedBets.FindAsync(id);
        }

        public async Task<PlacedBets> UpdateAsync(Guid id, UpdatePlacedBets newEntity)
        {
            PlacedBets currentBet = await _dbContext.PlacedBets.FindAsync(id);
            if (currentBet == null)
            {
                return null;
            }
            currentBet.PlacedDate = _timeProvider.GetUtcNow().DateTime;
            currentBet.Type = newEntity.Type;
            await _dbContext.SaveChangesAsync();
            return currentBet;
        }
    }
}
