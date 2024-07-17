using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Dto;
using Domain.Command;


namespace Services
{
    public class BetsServices : IBetableService<Bets, UpdateBets>
    {
        private readonly DBContext _dbContext;

        public BetsServices() 
        {
            _dbContext = new DBContext(Abstraction.connection_data);
        }

        private async Task<bool> validateBetBody(Bets entity)
        {
            BetableEntityServices service = new();
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

        public async Task<bool> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bets>> GetAll()
        {
            return await _dbContext.Bets.ToListAsync();
        }

        public async Task<Bets> GetById(Guid id)
        {
            return await _dbContext.Bets.FindAsync(id);
        }

        public Task<Bets> Update(Guid id, UpdateBets entity)
        {
            throw new NotImplementedException();
        }
    }

}
