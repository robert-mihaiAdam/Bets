using Domain.Entities;
using Domain.Dto.PlacedBet;

namespace Services.Interfaces
{
    public interface IPlacedBetsService
    {
        Task<PlacedBetsDto> CreateAsync(CreatePlacedBetDto entity);

        IQueryable<PlacedBets> GetAll();

        Task<PlacedBetsDto> GetByIdAsync(Guid id);

        Task<PlacedBetsDto> UpdateByIdAsync(Guid id, UpdatePlacedBetDto entity);

        Task<bool> DeletePlacedBetByIdAsync(Guid id);
    }
}
