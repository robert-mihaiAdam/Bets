using Domain.Dto.FullBetView;
using Domain.Dto.PlacedBet;

namespace Services.Interfaces
{
    public interface IPlacedBetFacade
    {
        Task<PlacedBetsDto> CreatePlacedBetAsync(CreatePlacedBetDto newPlacedBet);

        Task<FullBetViewDto> GetPlacedBetByIdAsync(Guid id);

        Task<IEnumerable<FullBetViewDto>> GetAllPlacedBetAsync();

        Task<PlacedBetsDto> UpdatePlacedBetByIdAsync(Guid id, UpdatePlacedBetDto updatePlacedBet);

        Task DeletePlacedBetByIdAsync(Guid id);
    }
}
