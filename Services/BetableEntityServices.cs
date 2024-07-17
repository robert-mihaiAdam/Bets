using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;

namespace Services
{
    public class BetableEntityServices : IBetableService<BetableEntity, UpdateBetableEntity>
    {
        private readonly DBContext _dbContext;

        public BetableEntityServices() 
        {
            _dbContext = new DBContext(Abstraction.connection_data);
        }

        public async Task<BetableEntity> Create(BetableEntity entity)
        {
            _dbContext.BetableEntity.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BetableEntity>> GetAll()
        {
            return await _dbContext.BetableEntity.ToListAsync();
        }

        public async Task<BetableEntity> GetById(Guid id)
        {
            return await _dbContext.BetableEntity.FindAsync(id);
        }

        public Task<BetableEntity> Update(Guid id, UpdateBetableEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
