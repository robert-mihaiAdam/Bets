using Domain.Dto.BetableEntity;

namespace Services.Interfaces
{
    public interface IBetableEntityService
    {
        Task<BetableEntityDto> CreateAsync(PlaceBetableEntityDto entity);

        Task<IEnumerable<BetableEntityDto>> GetAllAsync();

        Task<BetableEntityDto> GetByIdAsync(Guid id);

        Task<BetableEntityDto> UpdateEntityByIdAsync(Guid id, UpdateBetableEntityDto entity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
