using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;

namespace Services
{
    public sealed class BetableEntityService : IBetableEntityService
    {
        private readonly DBContext _dbContext;
        public BetableEntityService(DBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public async Task<BetableEntity> CreateAsync(BetableEntity entity)
        {
            _dbContext.BetableEntity.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<BetableEntity>> GetAllAsync()
        {
            return await _dbContext.BetableEntity.ToListAsync();
        }

        public async Task<BetableEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.BetableEntity.FindAsync(id);
        }
    }
}
