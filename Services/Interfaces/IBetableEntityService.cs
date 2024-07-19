using Domain.Dto;

namespace Services.Interfaces
{
    public interface IBetableEntityService
    {
        Task<BetableEntity> CreateAsync(BetableEntity entity);

        Task<IEnumerable<BetableEntity>> GetAllAsync();

        Task<BetableEntity> GetByIdAsync(Guid id);
    }
}
