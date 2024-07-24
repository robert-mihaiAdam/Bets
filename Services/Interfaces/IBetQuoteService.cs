using Domain.Dto.BetQuote;
using Domain.Dto.BetRequest;

namespace Services.Interfaces
{
    public interface IBetQuoteService
    {
        Task<BetQuoteDto> CreateAsync(CreateBetQuotesDto entity, Guid betId);

        Task<IEnumerable<BetQuoteDto>> GetAllAsync();

        Task<BetQuoteDto> GetByIdAsync(Guid id);

        Task<IEnumerable<BetRequestDto>> GetAllFullBetsAsync();

        Task<bool> DeleteFullBetAsync(Guid id);
    }
}
