using Domain.Dto.Bets;

namespace Services.Interfaces
{
    public interface IBetsService
    {
        Task<BetsDto> CreateAsync(CreateBetsDto entity);

        Task<IEnumerable<BetsDto>> GetAllAsync();

        Task<BetsDto> GetByIdAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}
