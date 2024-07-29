using Domain.Dto.BetableEntity;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBetableEntityService
    {
        Task<BetableEntityDto> CreateAsync(CreateBetableEntityDto entity);

        Task<IQueryable<BetableEntity>> GetAllAsync();

        Task<BetableEntityDto> GetByIdAsync(Guid id);

        Task<BetableEntityDto> UpdateEntityByIdAsync(Guid id, UpdateBetableEntityDto entity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
