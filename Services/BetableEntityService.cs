using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using AutoMapper;
using Domain.Dto.BetableEntity;
using Domain.ErrorEntities;

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

        public async Task<BetableEntityDto> CreateAsync(CreateBetableEntityDto entity)
        {
            BetableEntity newBetableEntity = _mapper.Map<BetableEntity>(entity);
            _dbContext.BetableEntity.Add(newBetableEntity);
            await _dbContext.SaveChangesAsync();
            BetableEntityDto newBetableEntityDto = _mapper.Map<BetableEntityDto>(newBetableEntity);
            return newBetableEntityDto;
        }

        public IQueryable<BetableEntity> GetAll()
        {
            return _dbContext.BetableEntity.AsQueryable();
        }

        public async Task<IEnumerable<BetableEntityDto>> GetAllAsync()
        {
            IEnumerable<BetableEntity> betableEntities = await _dbContext.BetableEntity.ToListAsync();
            IEnumerable<BetableEntityDto> dtoEntities = _mapper.Map<IEnumerable<BetableEntityDto>>(betableEntities);
            return dtoEntities;
        }

        private async Task<BetableEntity> GetByIdVanillaAsync(Guid id)
        {
            BetableEntity betableEntity = await _dbContext.BetableEntity.FindAsync(id);
            if (betableEntity == null)
            {
                throw new NotFoundException($"Bet Entity with id:{id} doesn't exist");
            }
            return betableEntity;
        }

        public async Task<BetableEntityDto> GetByIdAsync(Guid id)
        {
            BetableEntity betableEntity = await GetByIdVanillaAsync(id);
            BetableEntityDto betableEntityDto = _mapper.Map<BetableEntityDto>(betableEntity);
            return betableEntityDto;
        }

        public async Task<BetableEntityDto> UpdateEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            BetableEntity currentEntity = await GetByIdVanillaAsync(id);
            _mapper.Map(newEntity, currentEntity);
            await _dbContext.SaveChangesAsync();
            BetableEntityDto entityDto = _mapper.Map<BetableEntityDto>(currentEntity);
            return entityDto;
        }


        public async Task DeleteByIdAsync(Guid id)
        {
            BetableEntity item = await GetByIdVanillaAsync(id);
            _dbContext.BetableEntity.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
