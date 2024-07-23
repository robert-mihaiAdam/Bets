using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
<<<<<<< HEAD
using Domain.Dto.BetableEntity;
=======
using Domain.Dto.Bets;
using AutoMapper;
>>>>>>> a42b9c1 (Feature: Implement Create and read routes for BetQuote)

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

<<<<<<< HEAD
    private async Task<bool> ValidateBetBodyAsync(Bets entity)
        {
            entity.Date = _timeProvider.GetUtcNow().DateTime;
            BetableEntityDto home = await _betableEntity.GetByIdAsync(entity.BetableEntityA);
=======
        private async Task<bool> ValidateBetBodyAsync(CreateBetsDto entity)
        { 
            BetableEntity home = await _betableEntity.GetByIdAsync(entity.BetableEntityA);
>>>>>>> a42b9c1 (Feature: Implement Create and read routes for BetQuote)
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

        public async Task<Bets> CreateAsync(CreateBetsDto entity)
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
            return newBet;
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
