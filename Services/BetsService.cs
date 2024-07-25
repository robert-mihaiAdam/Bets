using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Dto.BetableEntity;
using Domain.Dto.Bets;
using AutoMapper;

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


        private async Task<bool> ValidateBetBodyAsync(CreateBetsDto entity)
        { 
            BetableEntityDto home = await _betableEntity.GetByIdAsync(entity.BetableEntityA);
            if (home == null)
            {
                return false;
            }

            BetableEntityDto away = await _betableEntity.GetByIdAsync(entity.BetableEntityB);
            if (away == null)
            {
                return false;
            }

            return true;
        }

        public async Task<BetsDto> CreateAsync(CreateBetsDto entity)
        {
            bool verdict = await ValidateBetBodyAsync(entity);
            if (!verdict)
            {
                return null;
            }
            Bets newBet = _mapper.Map<Bets>(entity);
            newBet.Date = _timeProvider.GetUtcNow().DateTime;
            _dbContext.Bets.Add(newBet);
            await _dbContext.SaveChangesAsync();
            BetsDto newBetDto = _mapper.Map<BetsDto>(newBet);
            return newBetDto;
        }

        public async Task<IEnumerable<BetsDto>> GetAllAsync()
        {
            IEnumerable<Bets> entities = await _dbContext.Bets.ToListAsync();
            IEnumerable<BetsDto> entitiesDto = _mapper.Map<IEnumerable<BetsDto>>(entities);
            return entitiesDto;
        }

        public async Task<BetsDto> GetByIdAsync(Guid id)
        {
            Bets entities = await _dbContext.Bets.FindAsync(id);
            BetsDto entitiesDto = _mapper.Map<BetsDto>(entities);
            return entitiesDto;
        }

        public async Task<BetsDto> UpdateById(Guid id, UpdateBetsDto newEntity)
        {
            Bets currentEntity = await _dbContext.Bets.FindAsync(id);
            if (currentEntity == null)
            {
                return null;
            }

            bool verdict = await ValidateBetBodyAsync(_mapper.Map<CreateBetsDto>(newEntity));
            if (!verdict)
            {
                return null;
            }

            _mapper.Map(newEntity, currentEntity);
            await _dbContext.SaveChangesAsync();
            BetsDto entityDto = _mapper.Map<BetsDto>(currentEntity);
            return entityDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Bets foundEntity = await _dbContext.Bets.FindAsync(id);
            if (foundEntity == null)
            {
                return false;
            }
           
            _dbContext.Bets.Remove(foundEntity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
