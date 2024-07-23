using Domain.Dto.Bets;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBetsService
    {
        Task<Bets> CreateAsync(CreateBetsDto entity);

        Task<IEnumerable<Bets>> GetAllAsync();

        Task<Bets> GetByIdAsync(Guid id);
    }
}
