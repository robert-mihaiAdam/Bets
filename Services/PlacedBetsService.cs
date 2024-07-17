using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;

namespace Services
{
    public class PlacedBetsService : IBetableService<PlacedBets, UpdatePlacedBets>
    {
        private readonly DBContext _dbContext;

        public PlacedBetsService()
        { 
            _dbContext = new DBContext(Abstraction.connection_data);
        }

        public async Task<PlacedBets?> Create(PlacedBets entity)
        {
            Guid quoteId = entity.QuoteId;
            BetQuoteServices betQuoteService = new BetQuoteServices();
            BetQuotes currentQuote = await betQuoteService.GetById(quoteId);
            if (currentQuote == null)
                return null;

            _dbContext.PlacedBets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
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
