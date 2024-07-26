using Domain.Dto.BetQuote;

namespace Services.Interfaces
{
    public interface IBetQuoteService
    {
        Task<BetQuoteDto> CreateAsync(CreateBetQuotesDto entity, Guid betId);

        IQueryable<BetQuoteDto> GetAllAsync();

        Task<BetQuoteDto> GetByIdAsync(Guid id);

        Task<BetQuoteDto> UpdateById(Guid id, UpdateBetQuotesDto newEntity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
