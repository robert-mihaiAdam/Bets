using Domain.Dto.BetQuote;
using Domain.Dto.Bets;

namespace Domain.Dto.BetRequest
{
    public class GetBetRequest
    {
        public GetBetsDto bet { get; set; }
        public GetBetQuoteDto betQuote { get; set; }
    }
}
