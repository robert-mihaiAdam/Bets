using Domain.Entities;

namespace Domain.Dto
{
    public class CreateBetRequest
    {
        public BetsDto Bet { get; set; }
        public BetQuotesDto BetQuote { get; set; }
    }
}
