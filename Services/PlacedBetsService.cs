using Services.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Dto.BetQuote;
using Domain.Dto.PlacedBet;
using AutoMapper;

namespace Services
{
    public sealed class PlacedBetsService : IPlacedBetsService
    {
        private readonly DBContext _dbContext;
        private readonly TimeProvider _timeProvider;
        private readonly IBetQuoteService _betQuoteService;
        private readonly IMapper _mapper;

        public PlacedBetsService(DBContext dbContext, TimeProvider timeProvider, IBetQuoteService betQuoteService, IMapper mapper)
        {
            _dbContext = dbContext;
            _timeProvider = timeProvider;
            _betQuoteService = betQuoteService;
            _mapper = mapper;
        }

        public async Task<PlacedBetsDto> CreateAsync(CreatePlacedBetDto newEntity)
        {
            Guid quoteId = newEntity.QuoteId;
            BetQuoteDto currentQuote = await _betQuoteService.GetByIdAsync(quoteId);
            if (currentQuote == null)
                return null;

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

        public async Task<PlacedBetsDto> UpdateByIdAsync(Guid id, UpdatePlacedBetDto newEntity)
        {
            PlacedBets currentBet = await _dbContext.PlacedBets.FindAsync(id);
            if (currentBet == null)
            {
                return null;
            }

         
            _mapper.Map(newEntity, currentBet);
            currentBet.PlacedDate = _timeProvider.GetUtcNow().DateTime;
            await _dbContext.SaveChangesAsync();
            PlacedBetsDto updatedPlacedBet = _mapper.Map<PlacedBetsDto>(currentBet);
            return updatedPlacedBet;
        }

        public async Task<bool> DeletePlacedBetByIdAsync(Guid id)
        {
            PlacedBets currentPlacedBet = await _dbContext.PlacedBets.FindAsync(id);
            if (currentPlacedBet == null)
            {
                return false;
            }

            _dbContext.PlacedBets.Remove(currentPlacedBet);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
