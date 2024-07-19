using Domain.Dto;

namespace Services.Interfaces
{
    public interface IBetsService
    {
        Task<Bets> CreateAsync(Bets entity);

        Task<IEnumerable<Bets>> GetAllAsync();

        Task<Bets> GetByIdAsync(Guid id);
    }
}
