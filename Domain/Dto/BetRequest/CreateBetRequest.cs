using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Domain.Entities;

namespace Domain.Dto.BetRequest
{
    public class CreateBetRequest
    {
        public CreateBetsDto Bet { get; set; }
        public CreateBetQuotesDto BetQuote { get; set; }
    }
}
