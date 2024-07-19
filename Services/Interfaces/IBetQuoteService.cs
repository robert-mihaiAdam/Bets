using Domain.Dto;

namespace Services.Interfaces
{
    public interface IBetQuoteService
    {
        Task<BetQuotes> CreateAsync(BetQuotes entity);

        Task<IEnumerable<BetQuotes>> GetAllAsync();

        Task<BetQuotes> GetByIdAsync(Guid id);
    }
}
