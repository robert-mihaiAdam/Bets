using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;


namespace Services
{
    public sealed class BetQuoteServices : IBetableService<BetQuotes, UpdateBetQuotes>
    {
        private readonly DBContext _dbContext;

        public BetQuoteServices(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<BetQuotes> Create(BetQuotes entity)
        {   
            _dbContext.BetQuotes.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BetQuotes>> GetAll()
        {
            return await _dbContext.BetQuotes.ToListAsync();
        }

        public async Task<BetQuotes> GetById(Guid id)
        {
            return await _dbContext.BetQuotes.FindAsync(id);
        }

        public async Task<IEnumerable<BetQuotes>> GetBets(Guid id)
        {
            Console.WriteLine(id);
            return await _dbContext.BetQuotes
                                    .Where(quote => quote.BetId == id)
                                    .ToListAsync();
        }

        public Task<BetQuotes> Update(Guid id, UpdateBetQuotes entity)
        {
            throw new NotImplementedException();
        }
    }
}
