using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;

namespace Services
{
    public sealed class PlacedBetsService : IPlacedBetsService<PlacedBets, UpdatePlacedBets>
    {
        private readonly DBContext _dbContext;

        public PlacedBetsService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PlacedBets?> Create(PlacedBets entity)
        {
            Guid quoteId = entity.QuoteId;
            BetQuoteServices betQuoteService = new BetQuoteServices(_dbContext);
            BetQuotes currentQuote = await betQuoteService.GetById(quoteId);
            if (currentQuote == null)
                return null;

            _dbContext.PlacedBets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<PlacedBets>> GetAll()
        {
            return await _dbContext.PlacedBets.ToListAsync();
        }

        public async Task<PlacedBets?> GetById(Guid id)
        {
            return await _dbContext.PlacedBets.FindAsync(id);
        }

        public async Task<PlacedBets> Update(Guid id, UpdatePlacedBets newEntity)
        {
            PlacedBets currentBet = await _dbContext.PlacedBets.FindAsync(id);
            if (currentBet == null)
            {
                return null;
            }

            currentBet.Type = newEntity.Type;
            await _dbContext.SaveChangesAsync();
            return currentBet;
        }
    }
}
