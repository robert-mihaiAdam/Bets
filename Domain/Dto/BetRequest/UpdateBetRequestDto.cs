using Domain.Dto.BetQuote;
using Domain.Dto.Bets;

namespace Domain.Dto.BetRequest
{
    public class UpdateBetRequestDto
    {
        public UpdateBetQuotesDto betQuote { get; set; }
        public UpdateBetsDto bets { get; set; }
    }
}
