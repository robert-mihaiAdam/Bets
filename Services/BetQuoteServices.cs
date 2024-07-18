using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;


namespace Services
{
    public sealed class BetQuoteServices : IBetQuoteService<BetQuotes, UpdateBetQuotes>
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

        public async Task<IEnumerable<BetQuotes>> GetAll()
        {
            return await _dbContext.BetQuotes.ToListAsync();
        }

        public async Task<BetQuotes> GetById(Guid id)
        {
            return await _dbContext.BetQuotes.FindAsync(id);
        }
    }
}
