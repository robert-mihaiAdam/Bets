using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;

namespace Services
{
    public sealed class BetQuoteService : IBetQuoteService
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;

        public BetQuoteService(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<BetQuotes> CreateAsync(BetQuotes entity)
        {   
            _dbContext.BetQuotes.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<BetQuotes>> GetAllAsync()
        {
            return await _dbContext.BetQuotes.ToListAsync();
        }

        public async Task<BetQuotes> GetByIdAsync(Guid id)
        {
            return await _dbContext.BetQuotes.FindAsync(id);
        }
    }
}
