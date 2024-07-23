using Domain.Entities;
using Domain.Dto.BetableEntity;

namespace Services.Interfaces
{
    public interface IBetableEntityService
    {
        Task<BetableEntity> CreateAsync(PlaceBetableEntityDto entity);

        Task<IEnumerable<BetableEntity?>> GetAllAsync();

        Task<BetableEntity?> GetByIdAsync(Guid id);

        Task<BetableEntity?> UpdateEntityByIdAsync(Guid id, UpdateBetableEntityDto entity);

        Task<BetableEntity?> DeleteByIdAsync(Guid id);
    }
}
