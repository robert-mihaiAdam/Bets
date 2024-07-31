using Services.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Dto.PlacedBet;
using AutoMapper;
using Domain.ErrorEntities;

namespace Services
{
    public sealed class PlacedBetsService : IPlacedBetsService
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;
        private readonly IMapper _mapper;

        public PlacedBetsService(DBContext dbContext, TimeProvider timeProvider, IBetQuoteService betQuoteService, IMapper mapper)
        {
            _dbContext = dbContext;
            _timeProvider = timeProvider;
            _mapper = mapper;
        }

        public async Task<PlacedBetsDto> CreateAsync(CreatePlacedBetDto newEntity)
        {
            PlacedBets newPlacedBets = _mapper.Map<PlacedBets>(newEntity);
            newPlacedBets.PlacedDate = _timeProvider.GetUtcNow().DateTime;
            newPlacedBets.UserId = new Guid();
            _dbContext.PlacedBets.Add(newPlacedBets);
            await _dbContext.SaveChangesAsync();
            PlacedBetsDto placedBetsDto = _mapper.Map<PlacedBetsDto>(newPlacedBets); 
            return placedBetsDto;
        }

        public IQueryable<PlacedBets> GetAll()
        {
            return  _dbContext.PlacedBets.AsQueryable();
        }

        public async Task<PlacedBetsDto> GetByIdAsync(Guid id)
        {
            PlacedBets currentPlacedBet = await _dbContext.PlacedBets.FindAsync(id);
            PlacedBetsDto currentPlacedBetDto = _mapper.Map<PlacedBetsDto>(currentPlacedBet);
            return currentPlacedBetDto;
        }

        private async Task<PlacedBets> GetByIdVanillaAsync(Guid id)
        {
            PlacedBets entity = await _dbContext.PlacedBets.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Doesn't exists any placed bet with id:{id}");
            }
            return entity;
        }

        public async Task<PlacedBetsDto> UpdateByIdAsync(Guid id, UpdatePlacedBetDto newEntity)
        {
            PlacedBets currentBet = await GetByIdVanillaAsync(id);
            _mapper.Map(newEntity, currentBet);
            currentBet.PlacedDate = _timeProvider.GetUtcNow().DateTime;
            await _dbContext.SaveChangesAsync();
            PlacedBetsDto updatedPlacedBet = _mapper.Map<PlacedBetsDto>(currentBet);
            return updatedPlacedBet;
        }

        public async Task DeletePlacedBetByIdAsync(Guid id)
        {
            PlacedBets currentPlacedBet = await GetByIdVanillaAsync(id);
            _dbContext.PlacedBets.Remove(currentPlacedBet);
            await _dbContext.SaveChangesAsync();
        }
    }
}
