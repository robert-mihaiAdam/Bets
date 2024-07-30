using Services.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Dto.BetableEntity;
using Domain.Dto.Bets;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public sealed class BetsService : IBetsService
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;
        private readonly IBetableEntityService _betableEntity;
        private readonly IMapper _mapper;

        public BetsService(DBContext dBContext,
                           TimeProvider timeProvider,
                           IBetableEntityService betableEntity,
                           IMapper mapper)
        {
            _dbContext = dBContext;
            _timeProvider = timeProvider;
            _betableEntity = betableEntity;
            _mapper = mapper;
        }


        private async Task ValidateBetBodyAsync(CreateBetsDto entity)
        { 
            BetableEntityDto home = await _betableEntity.GetByIdAsync(entity.BetableEntityA);
            if (home == null)
            {
                throw new Exception($"Betable entity with id: {entity.BetableEntityA} doesn't exists");
            }

            BetableEntityDto away = await _betableEntity.GetByIdAsync(entity.BetableEntityB);
            if (away == null)
            {
                throw new Exception($"Betable entity with id: {entity.BetableEntityB} doesn't exists");
            }
        }

        private async Task<Bets> GetByIdVanillaAsync(Guid id)
        {
            Bets entity = await _dbContext.Bets.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Bet with id:{id} doesn't exists");
            }
            return entity;
        }

        public async Task<BetsDto> CreateAsync(CreateBetsDto entity)
        {
            await ValidateBetBodyAsync(entity);
            Bets newBet = _mapper.Map<Bets>(entity);
            newBet.Date = _timeProvider.GetUtcNow().DateTime;
            _dbContext.Bets.Add(newBet);
            await _dbContext.SaveChangesAsync();
            BetsDto newBetDto = _mapper.Map<BetsDto>(newBet);
            return newBetDto;
        }

        public IQueryable<Bets> GetAll()
        {
            return _dbContext.Bets.AsQueryable();
        }

        public async Task<BetsDto> GetByIdAsync(Guid id)
        {
            Bets entity = await GetByIdVanillaAsync(id);
            BetsDto entityDto = _mapper.Map<BetsDto>(entity);
            return entityDto;
        }

        public async Task<BetsDto> UpdateById(Guid id, UpdateBetsDto newEntity)
        {
            Bets currentEntity = await GetByIdVanillaAsync(id);
            await ValidateBetBodyAsync(_mapper.Map<CreateBetsDto>(newEntity));
            _mapper.Map(newEntity, currentEntity);
            await _dbContext.SaveChangesAsync();
            BetsDto entityDto = _mapper.Map<BetsDto>(currentEntity);
            return entityDto;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Bets foundEntity = await GetByIdVanillaAsync(id);
            _dbContext.Bets.Remove(foundEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
