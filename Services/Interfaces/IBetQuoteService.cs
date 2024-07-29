using Domain.Dto.BetQuote;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBetQuoteService
    {
        Task<BetQuoteDto> CreateAsync(CreateBetQuotesDto entity, Guid betId);

        IQueryable<BetQuotes> GetAll();

        Task<BetQuoteDto> GetByIdAsync(Guid id);

        Task<BetQuoteDto> GetByBetIdAsync(Guid betId);

        Task<BetQuoteDto> UpdateById(Guid id, UpdateBetQuotesDto newEntity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
