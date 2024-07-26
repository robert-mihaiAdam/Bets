using Domain.Dto.Bets;

namespace Services.Interfaces
{
    public interface IBetsService
    {
        Task<BetsDto> CreateAsync(CreateBetsDto entity);

        IQueryable<BetsDto> GetAllAsync();

        Task<BetsDto> GetByIdAsync(Guid id);

        Task<BetsDto> UpdateById(Guid id, UpdateBetsDto newEntity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
