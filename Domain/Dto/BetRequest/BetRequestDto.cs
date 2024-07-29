using Domain.Dto.BetQuote;
using Domain.Dto.Bets;

namespace Domain.Dto.BetRequest
{
    public class BetRequestDto
    {
        public BetsDto bet { get; set; }
        public BetQuoteDto betQuote { get; set; }
    }
}
