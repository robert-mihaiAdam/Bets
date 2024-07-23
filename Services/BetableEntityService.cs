using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using AutoMapper;
using Domain.Dto.BetableEntity;

namespace Services
{
    public sealed class BetableEntityService : IBetableEntityService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public BetableEntityService(DBContext dBContext, IMapper mapper) 
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<BetableEntity> CreateAsync(PlaceBetableEntityDto entity)
        {
            BetableEntity newBetableEntity = _mapper.Map<BetableEntity>(entity);
            _dbContext.BetableEntity.Add(newBetableEntity);
            await _dbContext.SaveChangesAsync();
            return newBetableEntity;
        }

        public async Task<IEnumerable<BetableEntity?>> GetAllAsync()
        {
            return await _dbContext.BetableEntity.ToListAsync();
        }

        public async Task<BetableEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.BetableEntity.FindAsync(id);
        }

        public async Task<BetableEntity?> UpdateEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            BetableEntity? currentEntity = await _dbContext.BetableEntity.FindAsync(id);
            if (currentEntity == null)
            {
                return currentEntity;
            }

            _mapper.Map(newEntity, currentEntity);
            _dbContext.Entry(currentEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return currentEntity;
        }


        public async Task<BetableEntity?> DeleteByIdAsync(Guid id)
        {
            BetableEntity? item = await GetByIdAsync(id);

            if (item == null)
            {
                return null;
            }

            _dbContext.BetableEntity.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
