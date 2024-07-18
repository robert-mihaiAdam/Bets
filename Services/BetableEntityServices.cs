using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;

namespace Services
{
    public sealed class BetableEntityServices : IBetableEntityService<BetableEntity, UpdateBetableEntity>
    {
        private readonly DBContext _dbContext;

        public BetableEntityServices(DBContext dBContext) 
        {
            this._dbContext = dBContext;
        }

        public async Task<BetableEntity> Create(BetableEntity entity)
        {
            _dbContext.BetableEntity.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<BetableEntity>> GetAll()
        {
            return await _dbContext.BetableEntity.ToListAsync();
        }

        public async Task<BetableEntity> GetById(Guid id)
        {
            return await _dbContext.BetableEntity.FindAsync(id);
        }
    }
}
