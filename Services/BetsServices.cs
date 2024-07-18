using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;


namespace Services
{
    public sealed class BetsServices : IBetsService<Bets, UpdateBets>
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;

        public BetsServices(DBContext dBContext, TimeProvider timeProvider)
        {
            Console.WriteLine("Test vtm din Bets Service cu time provider");
            _dbContext = dBContext;
            _timeProvider = timeProvider;
        }

        private async Task<bool> validateBetBody(Bets entity)
        {
            entity.SetTime(_timeProvider);
            BetableEntityServices service = new(_dbContext, _timeProvider);
            BetableEntity home = await service.GetById(entity.BetableEntityA);
            if (home == null)
            {
                return false;
            }

            BetableEntity away = await service.GetById(entity.BetableEntityB);
            if (away == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Bets> Create(Bets entity)
        {
            bool verdict = await validateBetBody(entity);
            if (verdict)
            {
                return null;
            }

            _dbContext.Bets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Bets>> GetAll()
        {
            return await _dbContext.Bets.ToListAsync();
        }

        public async Task<Bets> GetById(Guid id)
        {
            return await _dbContext.Bets.FindAsync(id);
        }
    }

}
