using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Services
{
    public sealed class BetsService : IBetsService
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;
        private readonly IBetableEntityService _betableEntity;

        public BetsService(DBContext dBContext, TimeProvider timeProvider, IBetableEntityService betableEntity)
        {
            _dbContext = dBContext;
            _timeProvider = timeProvider;
            _betableEntity = betableEntity;
            
        }

    private async Task<bool> ValidateBetBodyAsync(Bets entity)
        {
            entity.Date = _timeProvider.GetUtcNow().DateTime;
            BetableEntity home = await _betableEntity.GetByIdAsync(entity.BetableEntityA);
            if (home == null)
            {
                return false;
            }

            BetableEntity away = await _betableEntity.GetByIdAsync(entity.BetableEntityB);
            if (away == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Bets> CreateAsync(Bets entity)
        {
            bool verdict = await ValidateBetBodyAsync(entity);
            if (!verdict)
            {
                return null;
            }
            _dbContext.Bets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Bets>> GetAllAsync()
        {
            return await _dbContext.Bets.ToListAsync();
        }

        public async Task<Bets> GetByIdAsync(Guid id)
        {
            return await _dbContext.Bets.FindAsync(id);
        }
    }

}
