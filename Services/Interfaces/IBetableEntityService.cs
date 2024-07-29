using Domain.Dto.BetableEntity;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBetableEntityService
    {
        Task<BetableEntityDto> CreateAsync(CreateBetableEntityDto entity);

        IQueryable<BetableEntity> GetAll();

        Task<BetableEntityDto> GetByIdAsync(Guid id);

        Task<BetableEntityDto> UpdateEntityByIdAsync(Guid id, UpdateBetableEntityDto entity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
