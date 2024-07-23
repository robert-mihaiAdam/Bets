using Domain.Entities;
using Domain.Command;

namespace Services.Interfaces
{
    public interface IPlacedBetsService
    {
        Task<PlacedBets> CreateAsync(PlacedBets entity);

        Task<IEnumerable<PlacedBets>> GetAllAsync();

        Task<PlacedBets> GetByIdAsync(Guid id);

        Task<PlacedBets> UpdateAsync(Guid id, UpdatePlacedBets entity);
    }
}
