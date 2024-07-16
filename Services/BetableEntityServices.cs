using Services.Interfaces;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class BetableEntityServices : IBetableService<BetableEntity, UpdateBetableEntity>
    {
        private readonly DBContext _dbContext;

        public BetableEntityServices(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BetableEntity> Create(BetableEntity entity)
        {
            _dbContext.betableEntity.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BetableEntity>> GetAll()
        {
            return await _dbContext.betableEntity.ToListAsync();
        }

        public async Task<BetableEntity> GetById(Guid id)
        {
            return await _dbContext.betableEntity.FindAsync(id);
        }

        public Task<BetableEntity> Update(Guid id, UpdateBetableEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
