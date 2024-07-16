using Services.Interfaces;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class BetQuoteServices : IBetableService<BetQuotes, UpdateBetQuotes>
    {
        private readonly DBContext _dbContext;

        public BetQuoteServices(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BetQuotes> Create(BetQuotes entity)
        {   
            _dbContext.betQuotes.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BetQuotes>> GetAll()
        {
            return await _dbContext.betQuotes.ToListAsync();
        }

        public async Task<BetQuotes> GetById(Guid id)
        {
            return await _dbContext.betQuotes.FindAsync(id);
        }

        public Task<BetQuotes> Update(Guid id, UpdateBetQuotes entity)
        {
            throw new NotImplementedException();
        }
    }
}
