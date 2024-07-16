using Services.Interfaces;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner.Generators;

namespace Services
{
    public class PlacedBetsService : IBetableService<PlacedBets, UpdatePlacedBets>
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

            _dbContext.placedBets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PlacedBets>> GetAll()
        {
            return await _dbContext.placedBets.ToListAsync();
        }

        public async Task<PlacedBets?> GetById(Guid id)
        {
            return await _dbContext.placedBets.FindAsync(id);
        }

        public async Task<PlacedBets> Update(Guid id, UpdatePlacedBets newEntity)
        {
            PlacedBets currentBet = await _dbContext.placedBets.FindAsync(id);
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
