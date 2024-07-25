using Domain.Dto.BetRequest;

namespace Services.Interfaces
{
    public interface IBetFacade
    {
        Task<BetRequestDto> CreateBetAsync(CreateBetRequestDto newFullBet);

        Task<BetRequestDto> UpdateFullBetAsync(Guid id, UpdateBetRequestDto updatedFullBet);

        Task<IEnumerable<BetRequestDto>> GetAllBetsAsync();

        Task<BetRequestDto> GetBetByIdAsync(Guid betQuoteId);
    }
}
